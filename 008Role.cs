using System;
using UnityEngine;
using PlayerRoles;
using System.Linq;
using Exiled.API.Enums;
using SCP008X.Components;
using Exiled.API.Features;
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
        bool scp008Breached = false;

        public static Scp008Role Scp008;

        public static int Scp008Victims = 0;

        public override uint Id { get; set; } = 030;
        public float Damage { get; set; } = 40f;

        public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;

        public override int MaxHealth { get; set; } = 600;

        public override string Name { get; set; } = "SCP-008";

        public override string Description { get; set; } = "An instance of SCP-008 that spreads the infection with each hit.";

        public override string CustomInfo { get; set; } = "SCP-008";

        public override bool IgnoreSpawnSystem { get; set; } = false;

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

            if (player.CurrentItem != null)
                player.DropHeldItem();

            if (!Plugin.Instance.Config.Breached.CassieAnnounce || scp008Breached)
                return;

            scp008Breached = true;
            Cassie.Message(Plugin.Instance.Config.Breached.Announcement, false, false, true);
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

            if (check + TrackedPlayers.Count <= 0)
                return;

            Log.Debug($"SCP-008 check completed, found {check} instances.");

            scp008Breached = false;
            Cassie.Message(Plugin.Instance.Config.Breached.ConteimentAnnoc, true, true, true);
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.Died += OnDied;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.Died -= OnDied;
            base.UnsubscribeEvents();
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null && !Check(ev.Attacker))
                return;

            ev.Amount = Damage;

            if (Plugin.Instance.Config.Scp008Buff.Enabled && ev.Attacker.ArtificialHealth < Plugin.Instance.Config.Scp008Buff.MaxGain)
            {
                float oldAhp = ev.Attacker.ArtificialHealth;
                float gainAmount = Plugin.Instance.Config.Scp008Buff.GainAhp;
                ev.Attacker.HumeShield = Math.Min(oldAhp + gainAmount, Plugin.Instance.Config.Scp008Buff.MaxGain);
            }

            if (Random.Range(0, 100) > Plugin.Instance.Config.Virus.Chance)
                return;

            if (ev.Player.GameObject.TryGetComponent(out SCP008 _) || !ev.Player.IsAlive)
                return;

            Methods.Infect(ev.Player);
            Log.Debug($"Successfully infected {ev.Player} with random chance.");
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (!Plugin.Instance.Config.AoeInfection.Enabled)
                return;

            Log.Debug($"AOE infection enabled, running check...");

            IEnumerable<Player> targets = Player.List
                .Where(x =>
                x.IsHuman &&
                x != ev.Player && 
                x.CurrentRoom == ev.Player.CurrentRoom && 
                Vector3.Distance(x.Position, ev.Player.Position) < 10);

            foreach (Player ply in targets)
            {
                if (Random.Range(1, 100) >= Plugin.Instance.Config.AoeInfection.Chance)
                    continue;

                Methods.Infect(ply);
                Log.Debug($"Called Infect() method for {ev.Player} due to AOE.");
            }
        }
    }
}
