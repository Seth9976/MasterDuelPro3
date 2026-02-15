using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WindBot
{
	// Token: 0x020001E5 RID: 485
	public class BotWrapper
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x00027ABD File Offset: 0x00025CBD
		public static void Main(string[] args)
		{
			new ProcessStartInfo();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00027AC8 File Offset: 0x00025CC8
		private static void ReadBots()
		{
			using (StreamReader reader = new StreamReader("bot.conf"))
			{
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine().Trim();
					if (line.Length > 0 && line[0] == '!')
					{
						BotWrapper.BotInfo newBot = new BotWrapper.BotInfo();
						newBot.name = line;
						newBot.command = reader.ReadLine().Trim();
						newBot.desc = reader.ReadLine().Trim();
						line = reader.ReadLine().Trim();
						newBot.flags = line.Split(' ', StringSplitOptions.None);
						BotWrapper.Bots.Add(newBot);
					}
				}
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00027B7C File Offset: 0x00025D7C
		private static string GetRandomBot(string flag)
		{
			IList<BotWrapper.BotInfo> foundBots = BotWrapper.Bots.Where((BotWrapper.BotInfo bot) => bot.flags.Contains(flag)).ToList<BotWrapper.BotInfo>();
			if (foundBots.Count > 0)
			{
				Random rand = new Random();
				return foundBots[rand.Next(foundBots.Count)].command;
			}
			return "";
		}

		// Token: 0x04000D21 RID: 3361
		private const int MB_ICONERRPR = 16;

		// Token: 0x04000D22 RID: 3362
		public static IList<BotWrapper.BotInfo> Bots = new List<BotWrapper.BotInfo>();

		// Token: 0x020001E6 RID: 486
		public class BotInfo
		{
			// Token: 0x04000D23 RID: 3363
			public string name;

			// Token: 0x04000D24 RID: 3364
			public string command;

			// Token: 0x04000D25 RID: 3365
			public string desc;

			// Token: 0x04000D26 RID: 3366
			public string[] flags;
		}
	}
}
