using System;
using System.Collections;
using YgomSystem.Utility;

namespace YgomSystem.Network
{
	// Token: 0x0200071D RID: 1821
	public class ProtocolHttp : Protocol
	{
		// Token: 0x0600390B RID: 14603 RVA: 0x000F3622 File Offset: 0x000F1822
		public static void GetServerDefaultUrl(RuntimeEnvironment.ServerType type, out string url, out string pollingUrl)
		{
			url = null;
			pollingUrl = null;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ApplicationQuitAbort()
		{
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetJobCount()
		{
			return 0;
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator Exec(NetworkMain.RequestStructure data)
		{
			return null;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x0000216D File Offset: 0x0000036D
		private void AbortRequest(NetworkMain.RequestStructure chaindata, Status states = Status.FAILED)
		{
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isRebootableCode(int code)
		{
			return false;
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x0000216D File Offset: 0x0000036D
		private static void postProcessRequestData(NetworkMain.RequestStructure data)
		{
		}

		// Token: 0x040032CF RID: 13007
		private bool appQuitAbort;

		// Token: 0x040032D0 RID: 13008
		private int jobCount;
	}
}
