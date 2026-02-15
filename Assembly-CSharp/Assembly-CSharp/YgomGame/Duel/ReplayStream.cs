using System;

namespace YgomGame.Duel
{
	// Token: 0x02000EE5 RID: 3813
	public class ReplayStream : ReplayBase
	{
		// Token: 0x06006F46 RID: 28486 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x06006F47 RID: 28487 RVA: 0x0000216D File Offset: 0x0000036D
		public override void InitReplay()
		{
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsQueued()
		{
			return false;
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x0000216D File Offset: 0x0000036D
		public void Add(byte[] data)
		{
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x000F61B6 File Offset: 0x000F43B6
		public static IntPtr NowRecord()
		{
			return (IntPtr)0;
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecordNext()
		{
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecordBegin()
		{
		}

		// Token: 0x06006F4D RID: 28493 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int IsRecordEnd()
		{
			return 0;
		}

		// Token: 0x0400AA3C RID: 43580
		public static ReplayStream s_instance;
	}
}
