using System;
using System.Collections.Generic;
using LogicAPI.Networking;
using LogicWorld.Server;

namespace CustomChatManager.Server.Commands
{
	public class CommandList : ICommand
	{
		public string name => "List";
		public string shortDescription => "Lists all connected players.";

		private readonly IPlayerManager playerManager;

		public CommandList()
		{
			playerManager = Program.Get<IPlayerManager>();
			if(playerManager == null)
			{
				throw new Exception("Could not get IPlayerManager. /list will break");
			}
		}

		public void execute(CommandSender sender, string arguments)
		{
			List<string> names = new List<string>();
			foreach(Connection connection in playerManager.AllConnections)
			{
				names.Add(playerManager.GetPlayerIDFromConnection(connection).Name);
			}
			sender.sendMessage("There are " + names.Count + " players online:\n" + ChatColors.highlight + String.Join(ChatColors.close + ", " + ChatColors.highlight, names) + ChatColors.close);
		}
	}
}
