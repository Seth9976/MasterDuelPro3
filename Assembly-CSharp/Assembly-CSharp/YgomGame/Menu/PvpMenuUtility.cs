using System;
using System.Collections;
using YgomSystem.Network;

namespace YgomGame.Menu
{
	// Token: 0x02000AE9 RID: 2793
	public class PvpMenuUtility
	{
		// Token: 0x06005155 RID: 20821 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsRetryableMatchingResult(int code)
		{
			return false;
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GetErrorDialogText(PvPCode errorCode, ref string title, ref string msg)
		{
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator yShowMatchingErrorDialog(PvPCode errorCode)
		{
			return null;
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator yShowMatchingErrorToast(PvPCode errorCode)
		{
			return null;
		}
	}
}
