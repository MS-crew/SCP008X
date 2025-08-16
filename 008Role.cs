using UnityEngine;
using PlayerRoles;
using System.Linq;
using Exiled.API.Enums;
using SCP008X.Components;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using System.Collections.Generic;
using Exiled.API.Features.Attributes;
using Exiled.Events.EventArgs.Player;
using Exiled.CustomRoles.API.Features;

using Random = UnityEngine.Random;

namespace SCP008X
{
    [CustomRole(RoleTypeId.Scp0492)]
    public class Scp008Role : CustomRole
    {
        private readonly HashSet<ushort> infectedItems = new();

        bool scp008Breached = false;

        public static Scp008Role Scp008;

        public static int Scp008Victims = 0;

        public float Damage { get; set; } = 40f; 

        public int MinPlayerForSpawn { get; set; } = 20;

        public override uint Id { get; set; } = 030;

        public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;

        public override int MaxHealth { get; set; } = 600;

        public override string Name { get; set; } = "SCP-008";

        public override string Description { get; set; } = "An instance of SCP-008 that spreads the infection with each hit.";

        public override string CustomInfo { get; set; } = "SCP-008";

        public override bool IgnoreSpawnSystem { get; set; } = true;

        public override bool KeepPositionOnSpawn { get; set; } = true;

        public override bool KeepInventoryOnSpawn { get; set; } = true;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            RoomSpawnPoints =
            [
                new()
                {
                    Room = RoomType.HczHid,
                    Chance = 100
                }
            ]
        };

        public override void Init()
        {
            base.Init();
            Scp008 = this;
        }

        public override void Destroy()
        {
            Scp008 = null;
            base.Destroy();
        }

        protected override void RoleAdded(Player player)
        {
            Scp008Victims++;

            if (Plugin.Instance.Config.Virus.InfectItems)
            {
                foreach (Item item in player.Items)
                    infectedItems.Add(item.Serial);
            }

            player.DropHeldItem();

            if (!Plugin.Instance.Config.Breached.CassieAnnounce || scp008Breached)
                return;

            scp008Breached = true;
            Cassie.Message(Plugin.Instance.Translation.Announcement, false, false, true);
            if (Plugin.Instance.Config.Breached.TurnOffLights) Map.TurnOffAllLights(7f);
        }

        protected override void RoleRemoved(Player player)
        {
            int check = 0;
            foreach (Player ply in Player.List)
            {
                if (ply.GameObject.TryGetComponent(out SCP008 scp008s))
                {
                    check++;
                    Log.Debug($"SCP-008 instance found for {ply.Nickname}.");
                }
            }
            int sum = check + TrackedPlayers.Count;
            Log.Debug($"SCP-008 check completed, found {sum} instances.");

            if (sum > 0)
                return;

            scp008Breached = false;
            Cassie.Message(Plugin.Instance.Translation.ConteimentAnnoc, true, true, true);
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem += OnAdded;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Died -= OnDied;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnAdded;
            infectedItems.Clear();

            base.UnsubscribeEvents();
        }

        private void OnAdded(PickingUpItemEventArgs ev)
        {
            if (!infectedItems.Contains(ev.Pickup.Serial))
                return;

            if (Check(ev.Player))
                return;

            ev.Player.Infect();
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || !Check(ev.Attacker))
                return;

            ev.Amount = Damage;

            if (Plugin.Instance.Config.Scp008Buff.Enabled && ev.Attacker.ArtificialHealth < Plugin.Instance.Config.Scp008Buff.MaxGain)
            {
                float oldAhp = ev.Attacker.ArtificialHealth;
                float gainAmount = Plugin.Instance.Config.Scp008Buff.GainAhp;
                ev.Attacker.AddAhp(oldAhp + gainAmount, Plugin.Instance.Config.Scp008Buff.MaxGain);
            }

            if (Random.Range(0, 100) > Plugin.Instance.Config.Virus.Chance)
                return;

            if (ev.Player.GameObject.TryGetComponent(out SCP008 _) || !ev.Player.IsAlive)
                return;

            ev.Player.Infect();
            Log.Debug($"Successfully infected {ev.Player} with random chance.");
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (!Plugin.Instance.Config.AoeInfection.Enabled)
                return;

            if (!Check(ev.Player))
                return;

            Log.Debug($"AOE infection enabled, running check...");

            IEnumerable<Player> targets = Player.List.Where(x =>
                x.IsHuman &&
                x != ev.Player && 
                x.CurrentRoom == ev.Player.CurrentRoom && 
                Vector3.Distance(x.Position, ev.Player.Position) < 10);

            foreach (Player ply in targets)
            {
                if(ply == ev.Player) continue;

                if (Random.Range(1, 100) >= Plugin.Instance.Config.AoeInfection.Chance)
                    continue;

                ply.Infect();
                Log.Debug($"Called Infect for {ev.Player} due to AOE.");
            }
        }

        public Vector3 GetSpawnPositionPublic()
        {
            if (SpawnProperties == null || SpawnProperties.Count() == 0)
            {
                return Vector3.zero;
            }

            float totalchance = 0f;
            List<(float chance, Vector3 pos)> spawnPointPool = new List<(float, Vector3)>(4);
            if (!SpawnProperties.StaticSpawnPoints.IsEmpty())
            {
                foreach (StaticSpawnPoint staticSpawnPoint in SpawnProperties.StaticSpawnPoints)
                {
                    Add(staticSpawnPoint.Position, staticSpawnPoint.Chance);
                }
            }

            if (!SpawnProperties.DynamicSpawnPoints.IsEmpty())
            {
                foreach (DynamicSpawnPoint dynamicSpawnPoint in SpawnProperties.DynamicSpawnPoints)
                {
                    Add(dynamicSpawnPoint.Position, dynamicSpawnPoint.Chance);
                }
            }

            if (!SpawnProperties.RoleSpawnPoints.IsEmpty())
            {
                foreach (RoleSpawnPoint roleSpawnPoint in SpawnProperties.RoleSpawnPoints)
                {
                    Add(roleSpawnPoint.Position, roleSpawnPoint.Chance);
                }
            }

            if (!SpawnProperties.RoomSpawnPoints.IsEmpty())
            {
                foreach (RoomSpawnPoint roomSpawnPoint in SpawnProperties.RoomSpawnPoints)
                {
                    Add(roomSpawnPoint.Position, roomSpawnPoint.Chance);
                }
            }

            if (spawnPointPool.Count == 0 || totalchance <= 0f)
            {
                return Vector3.zero;
            }

            float num = (float)(Exiled.Loader.Loader.Random.NextDouble() * (double)totalchance);
            foreach (var (num2, result) in spawnPointPool)
            {
                if (num < num2)
                {
                    return result;
                }

                num -= num2;
            }

            return Vector3.zero;
            void Add(Vector3 pos, float chance)
            {
                if (!(chance <= 0f))
                {
                    spawnPointPool.Add((chance, pos));
                    totalchance += chance;
                }
            }
        }
    }
}
