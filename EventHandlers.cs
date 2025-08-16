using UnityEngine;
using Exiled.API.Features;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Scp049;
using Exiled.Events.EventArgs.Server;

namespace SCP008X
{
    public class EventHandlers
    {

        public void OnAllPlayerSpawned()
        {
            if (Random.Range(0, 100f) > Scp008Role.Scp008.SpawnChance)
                return;

            if (Scp008Role.Scp008.MinPlayerForSpawn < Server.PlayerCount)
                return;

            Player selectedPlayer = Player.List.GetRandomValue(p => p.Role.Team == PlayerRoles.Team.SCPs);

            if (selectedPlayer == null)
            {
                Log.Debug("No suitable player found for SCP-008 role assignment.");
                return;
            }

            Scp008Role.Scp008.AddRole(selectedPlayer);
            Vector3 spawnPos = Scp008Role.Scp008.GetSpawnPositionPublic();

            if(spawnPos != Vector3.zero)
            {
                selectedPlayer.Position = Scp008Role.Scp008.GetSpawnPositionPublic();
            }
            
            Log.Debug($"Player {selectedPlayer.DisplayNickname} spawned as SCP-008,Assigning SCP-008 role");
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (!Plugin.Instance.Config.RoundSummary.SummaryStats)
                return;

            if (Scp008Role.Scp008Victims == 0)
                return;

            Map.ShowHint(string.Format(Plugin.Instance.Translation.RoundEnd, Scp008Role.Scp008Victims, Round.KillsByScp), 30f);
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
