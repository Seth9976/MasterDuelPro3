using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading;
using MDPro3;
using MDPro3.Utility;
using WindBot.Game;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper;

namespace WindBot
{
	// Token: 0x020001EA RID: 490
	public class Program
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x00028008 File Offset: 0x00026208
		internal static void Main(string[] args)
		{
			Logger.WriteLine("WindBot starting...");
			Config.Load(args);
			Program.InitDatas(Config.GetString("DbPath", "cards.cdb"));
			if (Config.GetBool("ServerMode", false))
			{
				Program.RunAsServer(Config.GetInt("ServerPort", 2399));
				return;
			}
			if (args.Length == 0)
			{
				Logger.WriteErrorLine("=== WARN ===");
				Logger.WriteLine("No input found, tring to connect to localhost YGOPro host.");
				Logger.WriteLine("If it fail, the program will quit sliently.");
			}
			Program.RunFromArgs();
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00028084 File Offset: 0x00026284
		public static void InitDatas(string databasePath)
		{
			Program.Rand = new Random();
			DecksManager.Init();
			string absolutePath = Path.GetFullPath(databasePath);
			absolutePath = Path.GetFullPath("Data/locales/" + Language.GetConfig() + "/" + databasePath);
			if (!File.Exists(absolutePath))
			{
				Logger.WriteErrorLine("Can't find cards database file.");
				Logger.WriteErrorLine("Please place cards.cdb next to WindBot.exe or Bot.exe .");
				Logger.WriteLine("Press any key to quit...");
				Console.ReadKey();
				Environment.Exit(1);
			}
			List<string> paths = ZipHelper.GetAllCdbTempPath();
			paths.Add(absolutePath);
			foreach (string cdb in Directory.GetFiles("Expansions/", "*.cdb"))
			{
				paths.Add(cdb);
			}
			NamedCardsManager.InitForMulti(paths);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00028134 File Offset: 0x00026334
		private static void RunFromArgs()
		{
			WindBotInfo Info = new WindBotInfo();
			Info.Name = Config.GetString("Name", Info.Name);
			Info.Deck = Config.GetString("Deck", Info.Deck);
			Info.DeckFile = Config.GetString("DeckFile", Info.DeckFile);
			Info.Dialog = Config.GetString("Dialog", Info.Dialog);
			Info.Host = Config.GetString("Host", Info.Host);
			Info.Port = Config.GetInt("Port", Info.Port);
			Info.HostInfo = Config.GetString("HostInfo", Info.HostInfo);
			Info.Version = Config.GetInt("Version", Info.Version);
			Info.Hand = Config.GetInt("Hand", Info.Hand);
			Info.Debug = Config.GetBool("Debug", Info.Debug);
			Info.Chat = Config.GetBool("Chat", Info.Chat);
			Program.Run(Info);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00028240 File Offset: 0x00026440
		private static void RunAsServer(int ServerPort)
		{
			HttpListener MainServer = new HttpListener();
			try
			{
				MainServer.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
				MainServer.Prefixes.Add("http://+:" + ServerPort.ToString() + "/");
				MainServer.Start();
				Logger.WriteLine("WindBot server start successed.");
				Logger.WriteLine("HTTP GET http://127.0.0.1:" + ServerPort.ToString() + "/?name=WindBot&host=127.0.0.1&port=7911 to call the bot.");
				for (;;)
				{
					try
					{
						HttpListenerContext ctx = MainServer.GetContext();
						WindBotInfo Info = new WindBotInfo();
						NameValueCollection queryStringParser = QueryStringParser.ParseQueryString(Path.GetFileName(ctx.Request.RawUrl));
						Info.Name = queryStringParser["name"];
						Info.Deck = queryStringParser["deck"];
						Info.Host = queryStringParser["host"];
						string port = queryStringParser["port"];
						if (port != null)
						{
							Info.Port = int.Parse(port);
						}
						string deckfile = queryStringParser["deckfile"];
						if (deckfile != null)
						{
							Info.DeckFile = deckfile;
						}
						string dialog = queryStringParser["dialog"];
						if (dialog != null)
						{
							Info.Dialog = dialog;
						}
						string version = queryStringParser["version"];
						if (version != null)
						{
							Info.Version = (int)short.Parse(version);
						}
						string password = queryStringParser["password"];
						if (password != null)
						{
							Info.HostInfo = password;
						}
						string hand = queryStringParser["hand"];
						if (hand != null)
						{
							Info.Hand = int.Parse(hand);
						}
						string debug = queryStringParser["debug"];
						if (debug != null)
						{
							Info.Debug = bool.Parse(debug);
						}
						string chat = queryStringParser["chat"];
						if (chat != null)
						{
							Info.Chat = bool.Parse(chat);
						}
						if (Info.Name == null || Info.Host == null || port == null)
						{
							ctx.Response.StatusCode = 400;
							ctx.Response.Close();
						}
						else
						{
							try
							{
								new Thread(new ParameterizedThreadStart(Program.Run)).Start(Info);
							}
							catch (Exception ex)
							{
								string text = "Start Thread Error: ";
								Exception ex3 = ex;
								Logger.WriteErrorLine(text + ((ex3 != null) ? ex3.ToString() : null));
							}
							ctx.Response.StatusCode = 200;
							ctx.Response.Close();
						}
					}
					catch (Exception ex2)
					{
						string text2 = "Parse Http Request Error: ";
						Exception ex4 = ex2;
						Logger.WriteErrorLine(text2 + ((ex4 != null) ? ex4.ToString() : null));
					}
				}
			}
			finally
			{
				if (MainServer != null)
				{
					((IDisposable)MainServer).Dispose();
					goto IL_024E;
				}
				goto IL_024E;
				IL_024E:;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000284E8 File Offset: 0x000266E8
		private static void Run(object o)
		{
			try
			{
				GameClient client = new GameClient((WindBotInfo)o);
				client.Start();
				Logger.DebugWriteLine(client.Username + " started.");
				while (client.Connection.IsConnected)
				{
					try
					{
						client.Tick();
						Thread.Sleep(30);
					}
					catch (Exception ex)
					{
						string text = "Tick Error: ";
						Exception ex3 = ex;
						Logger.WriteErrorLine(text + ((ex3 != null) ? ex3.ToString() : null));
					}
				}
				Logger.DebugWriteLine(client.Username + " end.");
			}
			catch (Exception ex2)
			{
				string text2 = "Run Error: ";
				Exception ex4 = ex2;
				Logger.WriteErrorLine(text2 + ((ex4 != null) ? ex4.ToString() : null));
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000285AC File Offset: 0x000267AC
		public static FileStream ReadFile(string directory, string filename, string extension)
		{
			string tryfilename = filename + "." + extension;
			string fullpath = Path.Combine(directory, tryfilename);
			if (!File.Exists(fullpath))
			{
				fullpath = filename;
			}
			if (!File.Exists(fullpath))
			{
				fullpath = Path.Combine("../", filename);
			}
			if (!File.Exists(fullpath))
			{
				fullpath = Path.Combine("../deck/", filename);
			}
			if (!File.Exists(fullpath))
			{
				fullpath = Path.Combine("../", tryfilename);
			}
			if (!File.Exists(fullpath))
			{
				fullpath = Path.Combine("../deck/", tryfilename);
			}
			if (!File.Exists(fullpath))
			{
				fullpath = Path.Combine("Data/Windbot/" + directory, tryfilename);
			}
			if (!File.Exists(fullpath))
			{
				fullpath = Path.Combine("Deck/", tryfilename);
			}
			return new FileStream(fullpath, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x04000D2E RID: 3374
		internal static Random Rand;
	}
}
