using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace SCP008X.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Cure : ICommand
    {
        public string Command { get; } = "cure";

        public string[] Aliases { get; } = null;

        public string Description { get; } = "Forcefully cure a player from SCP-008";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Player.Get(sender).CheckPermission("scp008.cure"))
            {
                response = "Missing permissions.";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Please specify a player to cure.";
                return false;
            }

            if (Player.Get(arguments.At(0)) is not Player ply)
            {
                response = "Invalid player ID or name. Please provide a valid player ID or name.";
                return false;
            }

            bool cured = Methods.Cure(ply);
            response = cured ? $"{ply.Nickname} has been cured." : $"{ply.Nickname} is not infected with SCP-008.";
            return cured;
        }
    }
}
