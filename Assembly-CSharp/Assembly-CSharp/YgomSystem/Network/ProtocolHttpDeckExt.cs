using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomSystem.Network
{
	// Token: 0x0200071E RID: 1822
	public class ProtocolHttpDeckExt : Protocol
	{
		// Token: 0x06003913 RID: 14611 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetUrl(string cmd)
		{
			return null;
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetUrls()
		{
			return null;
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainsUrl(string cmd)
		{
			return false;
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCommandName(string cmd)
		{
			return null;
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ApplicationQuitAbort()
		{
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetJobCount()
		{
			return 0;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator Exec(NetworkMain.RequestStructure data)
		{
			return null;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x0000216D File Offset: 0x0000036D
		private void AbortRequest(NetworkMain.RequestStructure chaindata, Status states = Status.FAILED)
		{
		}

		// Token: 0x040032D1 RID: 13009
		private bool appQuitAbort;

		// Token: 0x040032D2 RID: 13010
		private int jobCount;
	}
}
