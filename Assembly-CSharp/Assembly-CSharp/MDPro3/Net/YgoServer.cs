using System;
using System.Threading;

namespace MDPro3.Net
{
	// Token: 0x0200130E RID: 4878
	public class YgoServer
	{
		// Token: 0x06008ECB RID: 36555 RVA: 0x00132087 File Offset: 0x00130287
		public static void StartServer(string args)
		{
			if (YgoServer.ServerRunning())
			{
				YgoServer.StopServer();
			}
			YgoServer.serverThread = new Thread(delegate
			{
				Dll.start_server(args);
			});
			YgoServer.serverThread.Start();
		}

		// Token: 0x06008ECC RID: 36556 RVA: 0x001320C0 File Offset: 0x001302C0
		public static void StopServer()
		{
			Dll.stop_server();
			Thread thread = YgoServer.serverThread;
			if (thread == null)
			{
				return;
			}
			thread.Abort();
		}

		// Token: 0x06008ECD RID: 36557 RVA: 0x001320D6 File Offset: 0x001302D6
		public static bool ServerRunning()
		{
			return YgoServer.serverThread != null && YgoServer.serverThread.IsAlive;
		}

		// Token: 0x0400CC83 RID: 52355
		public static Thread serverThread;
	}
}
