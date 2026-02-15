using System;
using System.Collections;
using YgomSystem.Network;

namespace YgomGame
{
	// Token: 0x020007D9 RID: 2009
	public static class ServerConnection
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isConnecting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000029CC File Offset: 0x00000BCC
		public static ServerConnection.Status status
		{
			get
			{
				return ServerConnection.Status.None;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06003E95 RID: 16021 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isSuccess
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000029CC File Offset: 0x00000BCC
		private static ServerConnection.Status codeToStatus(ErrorCode code)
		{
			return ServerConnection.Status.None;
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isFatalError(ServerConnection.Status st)
		{
			return false;
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator showFatalErrorDialog()
		{
			return null;
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Reset()
		{
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartConnect()
		{
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator connectCoroutine()
		{
			return null;
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x0000216D File Offset: 0x0000036D
		private static void timeLog(string msg)
		{
		}

		// Token: 0x04003788 RID: 14216
		private static ServerConnection.Status m_status;

		// Token: 0x04003789 RID: 14217
		private static IEnumerator m_connectCoroutine;

		// Token: 0x020007DA RID: 2010
		public enum Status
		{
			// Token: 0x0400378B RID: 14219
			None,
			// Token: 0x0400378C RID: 14220
			Success,
			// Token: 0x0400378D RID: 14221
			Maintenance,
			// Token: 0x0400378E RID: 14222
			VersionUpRequired,
			// Token: 0x0400378F RID: 14223
			Failed
		}
	}
}
