using System;
using System.Collections;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000532 RID: 1330
	public static class NativeToast
	{
		// Token: 0x06002A9D RID: 10909 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string message)
		{
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator yOpen(string message)
		{
			return null;
		}

		// Token: 0x040029C0 RID: 10688
		private static WaitForSeconds waitTime;

		// Token: 0x040029C1 RID: 10689
		private static IEnumerator yCoroutine;
	}
}
