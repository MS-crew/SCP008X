using MEC;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.API.Features;
using System.Collections.Generic;
using Exiled.Events.EventArgs.Player;
using Playerh = Exiled.Events.Handlers.Player;

namespace SCP008X.Components
{
    public class SCP008 : MonoBehaviour
    {
        Player ply;
        CoroutineHandle s008;
        void Start()
        {
            ply = Player.Get(gameObject);
            s008 = Timing.RunCoroutine(Infection());

            Playerh.Dying += OnDying;
            Playerh.UsedItem += OnHealed;
            Playerh.Spawned += WhenRoleChange;

            ply.ShowHint($"<color=yellow><b>SCP-008</b></color>\n{Plugin.Instance.Config.Virus.InfectionAlert}", 10f);
        }
        void OnDestroy()
        {
            Playerh.Dying -= OnDying;
            Playerh.UsedItem -= OnHealed;
            Playerh.Spawned -= WhenRoleChange;

            Timing.KillCoroutines(s008);
        }

        public void WhenRoleChange(SpawnedEventArgs ev)
        {
            if (ev.Player != ply)
                return;

            if (ev.Reason == SpawnReason.Escaped)
                return;

            ply.Cure();
        }

        public void OnHealed(UsedItemEventArgs ev)
        {
            if (ev.Player != ply)
                return;

            if (ev.Item.Category is not ItemCategory.Medical and ItemCategory.SCPItem)
                return;

            if (!ev.Player.ReferenceHub.TryGetComponent(out SCP008 _))
                return;

            switch (ev.Item.Type)
            {
                case ItemType.SCP500:
                    Cured(ev.Player);
                    break;
                case ItemType.Medkit:
                    if (Random.Range(0, 100) < Plugin.Instance.Config.Virus.CureChance)
                    {
                        Cured(ev.Player);
                    }
                    break;

                default:
                    break;
            }
        }

        private void Cured(Player player)
        {
            player.Cure();
            Log.Debug($"{player} successfully cured themselves.");
            player.Broadcast(3, message: "You cured your self", shouldClearPrevious: true);
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Player != ply)
                return;

            if (ev.DamageHandler.Type == DamageType.Warhead)
            {
                ply.Cure();
                return;
            }

            ply.Cure();
            ev.IsAllowed = false;
            Scp008Role.Scp008.AddRole(ply);
        }

        public IEnumerator<float> Infection()
        {
            while (ply.IsHuman)
            {
                ply.Hurt(Plugin.Instance.Config.Virus.InfectionDamagePerSeconds, DamageType.Poison);
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
