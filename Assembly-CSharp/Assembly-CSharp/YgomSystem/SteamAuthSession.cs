using System;
using System.Collections.Generic;

namespace YgomSystem
{
	// Token: 0x020004D6 RID: 1238
	public class SteamAuthSession
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x0000216A File Offset: 0x0000036A
		[Obsolete]
		public string GetSession()
		{
			return null;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool RequestSession(Action<string> callback)
		{
			return false;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x0000216D File Offset: 0x0000036D
		public void CancelSession()
		{
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetDialogParam(SteamAuthSession.DIALOG_TYPE type)
		{
			return null;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0000216D File Offset: 0x0000036D
		private void GetSessionTicket()
		{
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RequestSessionTicket(Action<string> callback)
		{
			return false;
		}

		// Token: 0x04002867 RID: 10343
		private string steam_auth_session;

		// Token: 0x04002868 RID: 10344
		private uint m_pcbTicket;

		// Token: 0x04002869 RID: 10345
		private const int m_tokenLength = 1024;

		// Token: 0x0400286A RID: 10346
		private byte[] m_token;

		// Token: 0x0400286B RID: 10347
		private Action<string> m_callback;

		// Token: 0x0400286C RID: 10348
		private Dictionary<SteamAuthSession.DIALOG_TYPE, Dictionary<string, object>> dialogParam;

		// Token: 0x020004D7 RID: 1239
		public enum DIALOG_TYPE
		{
			// Token: 0x0400286E RID: 10350
			NO_STEAMMANAGER_INITIALIZE,
			// Token: 0x0400286F RID: 10351
			NEED_REAUTH_REBOOT
		}
	}
}
