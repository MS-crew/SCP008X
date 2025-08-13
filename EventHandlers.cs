using UnityEngine;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp049;
using Exiled.Events.EventArgs.Server;

namespace SCP008X
{
    public class EventHandlers
    {
        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!Plugin.Instance.Config.RoundSummary.SummaryStats)
                return;

            if (Scp008Role.Scp008Victims == 0)
                return;

            Map.ShowHint(string.Format(Plugin.Instance.Config.RoundSummary.RoundEnd, Scp008Role.Scp008Victims, Round.ChangedIntoZombies), 30f);
        }

        public void OnRevived(FinishingRecallEventArgs ev)
        {
            if (Random.Range(1, 100) >= Plugin.Instance.Config.RecallZombieScp008Chance)
                return;

            ev.IsAllowed = false;
            ev.Player.Position = ev.Ragdoll.Position;
            Scp008Role.Scp008.AddRole(ev.Target);
            Log.Debug($"Player {ev.Target} revived by 008,Assigning SCP-008 role");
        }
    }
}
