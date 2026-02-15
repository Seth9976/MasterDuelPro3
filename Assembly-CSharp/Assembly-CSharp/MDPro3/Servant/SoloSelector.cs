using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using UnityEngine;
using WindBot;

namespace MDPro3.Servant
{
	// Token: 0x02001308 RID: 4872
	public class SoloSelector : Servant
	{
		// Token: 0x06008EB2 RID: 36530 RVA: 0x00131957 File Offset: 0x0012FB57
		public void SwitchCondition(SoloSelector.Condition condition)
		{
			SoloSelector.condition = condition;
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06008EB3 RID: 36531 RVA: 0x00126FFA File Offset: 0x001251FA
		public override int Depth
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06008EB4 RID: 36532 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008EB5 RID: 36533 RVA: 0x0013195F File Offset: 0x0012FB5F
		public override void Initialize()
		{
			this.returnServant = Program.instance.menu;
			base.Initialize();
			this.LoadBots();
		}

		// Token: 0x06008EB6 RID: 36534 RVA: 0x0013197D File Offset: 0x0012FB7D
		protected override void FirstLoadEvent()
		{
			base.FirstLoadEvent();
			this.Print();
		}

		// Token: 0x06008EB7 RID: 36535 RVA: 0x0013198C File Offset: 0x0012FB8C
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (this.servantUI == null)
			{
				return;
			}
			if (this.lastSoloItem != null)
			{
				this.lastSoloItem.GetSelectable().Select();
				return;
			}
			this.servantUI.SelectDefaultSelectable();
		}

		// Token: 0x06008EB8 RID: 36536 RVA: 0x001319DD File Offset: 0x0012FBDD
		public void LoadBots()
		{
			this.ReadBots("Data/locales/" + Language.GetConfig() + "/bot.conf");
			this.Print();
		}

		// Token: 0x06008EB9 RID: 36537 RVA: 0x00131A00 File Offset: 0x0012FC00
		private void ReadBots(string confPath)
		{
			SoloSelector.bots.Clear();
			StreamReader reader = new StreamReader(new FileStream(confPath, FileMode.Open, FileAccess.Read));
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine().Trim();
				if (line.Length > 0 && line[0] == '!')
				{
					SoloSelector.BotInfo newBot = new SoloSelector.BotInfo
					{
						name = line.TrimStart('!'),
						command = reader.ReadLine().Trim(),
						desc = reader.ReadLine().Trim()
					};
					line = reader.ReadLine().Trim();
					newBot.flags = line.Split(' ', StringSplitOptions.None);
					newBot.main0 = 5990062;
					try
					{
						string deckName = string.Empty;
						deckName = newBot.command.Split(new string[] { "Deck=", " Dialog=" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace("'", string.Empty).Replace(" ", string.Empty);
						if (File.Exists("Data/WindBot/Decks/Ai_" + deckName + ".ydk"))
						{
							Deck aiDeck = new Deck("Data/WindBot/Decks/Ai_" + deckName + ".ydk");
							if (aiDeck.Main.Count > 0)
							{
								newBot.main0 = aiDeck.Main[0];
							}
						}
					}
					catch
					{
					}
					SoloSelector.bots.Add(newBot);
				}
			}
		}

		// Token: 0x06008EBA RID: 36538 RVA: 0x00131B70 File Offset: 0x0012FD70
		private void Print()
		{
			if (this.servantUI == null)
			{
				return;
			}
			this.GetUI<SoloSelectorUI>().Print();
		}

		// Token: 0x06008EBB RID: 36539 RVA: 0x00131B8C File Offset: 0x0012FD8C
		private string GetWindBotCommand(int aiCode, bool diyDeck)
		{
			string aiCommand = SoloSelector.bots[aiCode].command;
			if (diyDeck)
			{
				string selectedDeck = this.GetUI<SoloSelectorUI>().GetAIDeck();
				if (!File.Exists("Deck/" + selectedDeck + ".ydk"))
				{
					MessageManager.Cast(InterString.Get("请先为AI选择有效的卡组。", 0));
					return string.Empty;
				}
				aiCommand = aiCommand + " DeckFile=\"" + selectedDeck + "\"";
			}
			Match match = Regex.Match(aiCommand, "Random=(\\w+)");
			if (match.Success)
			{
				string randomFlag = match.Groups[1].Value;
				string command = this.GetRandomBot(randomFlag);
				if (command != string.Empty)
				{
					aiCommand = command;
				}
			}
			return aiCommand;
		}

		// Token: 0x06008EBC RID: 36540 RVA: 0x00131C3C File Offset: 0x0012FE3C
		public void StartAIForSolo(int aiCode, bool diyDeck)
		{
			string aiCommand = this.GetWindBotCommand(aiCode, diyDeck);
			if (!string.IsNullOrEmpty(aiCommand))
			{
				this.Launch(aiCommand, this.GetUI<SoloSelectorUI>().IsLockHand(), this.GetUI<SoloSelectorUI>().IsNoCheck(), this.GetUI<SoloSelectorUI>().IsNoShuffle());
			}
		}

		// Token: 0x06008EBD RID: 36541 RVA: 0x00131C84 File Offset: 0x0012FE84
		public void StartAIForRoom(int aiCode, bool diyDeck)
		{
			string aiCommand = this.GetWindBotCommand(aiCode, diyDeck);
			if (!string.IsNullOrEmpty(aiCommand))
			{
				this.StartWindBot(aiCommand, TcpHelper.joinedAddress, TcpHelper.joinedPort, TcpHelper.joinedPassword, this.GetUI<SoloSelectorUI>().IsLockHand(), 600);
				Program.instance.ShiftToServant(Program.instance.room);
			}
		}

		// Token: 0x06008EBE RID: 36542 RVA: 0x00131CDC File Offset: 0x0012FEDC
		public void StartAIForHandTest(int port)
		{
			string aiCommand = this.GetWindBotCommand(0, false);
			if (!string.IsNullOrEmpty(aiCommand))
			{
				this.StartWindBot(aiCommand, "127.0.0.1", port.ToString(), string.Empty, true, 0);
			}
		}

		// Token: 0x06008EBF RID: 36543 RVA: 0x00131D14 File Offset: 0x0012FF14
		private string GetRandomBot(string flag)
		{
			IList<SoloSelector.BotInfo> foundBots = new List<SoloSelector.BotInfo>();
			foreach (SoloSelector.BotInfo bot in SoloSelector.bots)
			{
				if (Array.IndexOf<string>(bot.flags, flag) >= 0)
				{
					foundBots.Add(bot);
				}
			}
			if (foundBots.Count > 0)
			{
				global::System.Random rand = new global::System.Random();
				return foundBots[rand.Next(foundBots.Count)].command;
			}
			return "";
		}

		// Token: 0x06008EC0 RID: 36544 RVA: 0x00131DA8 File Offset: 0x0012FFA8
		public void StartWindBot(string command, string ip, string port, string password, bool lockHand, int delay)
		{
			command = command.Replace("'", "\"");
			if (lockHand)
			{
				command += " Hand=1";
			}
			command = command + " Host=" + ip;
			command = command + " Port=" + port;
			command = command + " HostInfo=" + password;
			string[] args = Tools.SplitWithPreservedQuotes(command);
			int i = 0;
			while (i < args.Length)
			{
				if (args[i].StartsWith("Dialog="))
				{
					string text = args[i];
					string path = text.Substring(7, text.Length - 7);
					if (!File.Exists("Data/Windbot/Dialogs/" + path + ".json"))
					{
						string config = Language.GetConfig();
						if (config == "en-US")
						{
							config = "default";
						}
						args[i] = "Dialog=" + config;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			new Thread(delegate
			{
				Thread.Sleep(delay);
				Program.Main(args);
			}).Start();
		}

		// Token: 0x06008EC1 RID: 36545 RVA: 0x00131EC0 File Offset: 0x001300C0
		public void Launch(string command, bool lockHand, bool noCheck, bool noShuffle)
		{
			SoloSelector.port = this.GetUI<SoloSelectorUI>().GetPort().ToString();
			string lp = this.GetUI<SoloSelectorUI>().GetLP().ToString();
			string hand = this.GetUI<SoloSelectorUI>().GetHand().ToString();
			string draw = this.GetUI<SoloSelectorUI>().GetDraw().ToString();
			string args = string.Concat(new string[]
			{
				SoloSelector.port,
				" -1 5 0 F ",
				noCheck ? "T " : "F ",
				noShuffle ? "T " : "F ",
				lp,
				" ",
				hand,
				" ",
				draw,
				" 0 0"
			});
			if (TcpHelper.IsPortAvailable(int.Parse(SoloSelector.port)))
			{
				YgoServer.StartServer(args);
				RoomServant.FromSolo = true;
				if (lockHand)
				{
					RoomServant.SoloLockHand = true;
				}
				else
				{
					RoomServant.SoloLockHand = false;
				}
				RoomServant.FromLocalHost = false;
				RoomServant.FromHandTest = false;
				TcpHelper.LinkStart("127.0.0.1", Config.Get("DuelPlayerName0", "@ui"), SoloSelector.port, string.Empty, true, delegate
				{
					this.StartWindBot(command, "127.0.0.1", SoloSelector.port, string.Empty, lockHand, 0);
				});
				return;
			}
			MessageManager.messageFromSubString = InterString.Get("端口被占用， 请尝试修改端口后再尝试。端口号应大于0，小于65535。", 0);
		}

		// Token: 0x0400CC6F RID: 52335
		public static readonly List<SoloSelector.BotInfo> bots = new List<SoloSelector.BotInfo>();

		// Token: 0x0400CC70 RID: 52336
		private const string WINDBOT_DIALOG_PATH = "Data/Windbot/Dialogs/";

		// Token: 0x0400CC71 RID: 52337
		private const string DECK_PREFIX = "Data/WindBot/Decks/Ai_";

		// Token: 0x0400CC72 RID: 52338
		private const int DEFAULT_CARD = 5990062;

		// Token: 0x0400CC73 RID: 52339
		public static string port = "7911";

		// Token: 0x0400CC74 RID: 52340
		public static SoloSelector.Condition condition = SoloSelector.Condition.ForSolo;

		// Token: 0x0400CC75 RID: 52341
		[HideInInspector]
		public SelectionToggle_Solo lastSoloItem;

		// Token: 0x02001309 RID: 4873
		public class BotInfo
		{
			// Token: 0x0400CC76 RID: 52342
			public string name;

			// Token: 0x0400CC77 RID: 52343
			public string command;

			// Token: 0x0400CC78 RID: 52344
			public string desc;

			// Token: 0x0400CC79 RID: 52345
			public string[] flags;

			// Token: 0x0400CC7A RID: 52346
			public int main0;
		}

		// Token: 0x0200130A RID: 4874
		public enum Condition
		{
			// Token: 0x0400CC7C RID: 52348
			ForSolo,
			// Token: 0x0400CC7D RID: 52349
			ForRoom
		}
	}
}
