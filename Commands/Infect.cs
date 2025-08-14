using System;
using PlayerRoles;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace SCP008X.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Infect : ICommand
    {
        public string Command { get; } = "infect";

        public string[] Aliases { get; } = null;

        public string Description { get; } = "Forcefully infect a player with SCP-008";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.Get(sender).CheckPermission("scp008.infect"))
            {
                response = "Missing permissions.";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Please specify a player to infect.";
                return false;
            }

            if (Player.Get(arguments.At(0)) is not Player ply)
            {
                response = "Invalid player ID or name. Please provide a valid player ID or name.";
                return false;
            }

            if (ply.Role.Team is Team.SCPs or Team.OtherAlive or Team.Dead)
            {
                response = "You can not infect this class.";
                return false;
            }

            bool infected = ply.Infect();

            response = infected ? "This player is already infected." : $"{ply.Nickname} has been infected.";
            return infected;
        }
    }
}
