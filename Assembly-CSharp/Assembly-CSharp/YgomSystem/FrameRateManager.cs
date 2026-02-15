using System;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x0200049E RID: 1182
	public class FrameRateManager : MonoBehaviour
	{
		// Token: 0x0600263B RID: 9787 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetMode(FrameRateManager.Mode m)
		{
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x0000216D File Offset: 0x0000036D
		public static void KeepHighRateMomentary(float time = 1E-45f)
		{
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x04002743 RID: 10051
		private static FrameRateManager.Mode mode;

		// Token: 0x04002744 RID: 10052
		private static float highPerfReq;

		// Token: 0x04002745 RID: 10053
		private static float rate;

		// Token: 0x04002746 RID: 10054
		private static float rate2;

		// Token: 0x0200049F RID: 1183
		public enum Mode
		{
			// Token: 0x04002748 RID: 10056
			FullFrame,
			// Token: 0x04002749 RID: 10057
			Var60,
			// Token: 0x0400274A RID: 10058
			Fix30,
			// Token: 0x0400274B RID: 10059
			Var30,
			// Token: 0x0400274C RID: 10060
			Fix20
		}
	}
}
