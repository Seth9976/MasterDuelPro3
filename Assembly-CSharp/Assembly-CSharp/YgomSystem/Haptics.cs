using System;

namespace YgomSystem
{
	// Token: 0x020004A6 RID: 1190
	public static class Haptics
	{
		// Token: 0x0600266A RID: 9834 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetVibrateFlag(bool bEnable, int Pad)
		{
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetVibrateFlagAll(bool bEnable)
		{
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Vibrate(GamePad.VIBRATION Id, int Pad)
		{
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x0000216D File Offset: 0x0000036D
		public static void VibrateForce(GamePad.VIBRATION Id, int Pad)
		{
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Stop(int Pad)
		{
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StopAll()
		{
		}

		// Token: 0x0400278E RID: 10126
		public static readonly int MAX;

		// Token: 0x0400278F RID: 10127
		private static bool[] s_bEnable;
	}
}
