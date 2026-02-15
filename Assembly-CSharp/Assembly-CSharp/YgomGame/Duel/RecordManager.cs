using System;

namespace YgomGame.Duel
{
	// Token: 0x02000EE2 RID: 3810
	public class RecordManager : ReplayBase
	{
		// Token: 0x06006F1D RID: 28445 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x06006F1E RID: 28446 RVA: 0x0000216D File Offset: 0x0000036D
		public override void InitReplay()
		{
		}

		// Token: 0x06006F1F RID: 28447 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetReplay(byte[] data)
		{
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddRecord(IntPtr ptr, int size)
		{
		}

		// Token: 0x06006F21 RID: 28449 RVA: 0x000F61B6 File Offset: 0x000F43B6
		public static IntPtr NowRecord()
		{
			return (IntPtr)0;
		}

		// Token: 0x06006F22 RID: 28450 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecordNext()
		{
		}

		// Token: 0x06006F23 RID: 28451 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecordBegin()
		{
		}

		// Token: 0x06006F24 RID: 28452 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int IsRecordEnd()
		{
			return 0;
		}

		// Token: 0x0400AA2B RID: 43563
		public static RecordManager instance;
	}
}
