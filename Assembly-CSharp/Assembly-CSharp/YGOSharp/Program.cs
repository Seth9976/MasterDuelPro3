using System;
using System.IO;
using System.Threading;
using YGOSharp.OCGWrapper;

namespace YGOSharp
{
	// Token: 0x020001BF RID: 447
	public class Program
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x000250D0 File Offset: 0x000232D0
		public static void Main(string[] args)
		{
			try
			{
				Config.Load(args);
				BanlistManager.Init(Config.GetString("BanlistFile", "lflist.conf"));
				Api.Init(Config.GetString("RootPath", "."), Config.GetString("ScriptDirectory", "script"), Config.GetString("DatabaseFile", "cards.cdb"));
				Program.ClientVersion = Config.GetUInt("ClientVersion", Program.ClientVersion);
				CoreServer server = new CoreServer();
				server.Start();
				while (server.IsRunning)
				{
					server.Tick();
					Thread.Sleep(1);
				}
			}
			catch (Exception ex)
			{
				File.WriteAllText("crash_" + DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt", ex.ToString());
			}
		}

		// Token: 0x04000B91 RID: 2961
		public static uint ClientVersion = 4937U;
	}
}
