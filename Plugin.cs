using System;
using Exiled.API.Features;
using Exiled.Events.Handlers;
using Exiled.CustomRoles.API;
using Server = Exiled.Events.Handlers.Server;

namespace SCP008X
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        private EventHandlers eventHandlers;

        public override string Name => "Scp008X";

        public override string Prefix => "Scp008X";

        public override string Author => "Zurna Sever";

        public override Version Version { get; } = new Version(3, 5, 1);

        public override Version RequiredExiledVersion { get; } = new Version(9, 8, 0);
        
        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers();

            Config.Scp008role.Register();

            Server.RoundEnded += eventHandlers.OnRoundEnd;
            Scp049.FinishingRecall += eventHandlers.OnRevived;

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Config.Scp008role.Unregister();

            Server.RoundEnded -= eventHandlers.OnRoundEnd;
            Scp049.FinishingRecall -= eventHandlers.OnRevived;

            eventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}