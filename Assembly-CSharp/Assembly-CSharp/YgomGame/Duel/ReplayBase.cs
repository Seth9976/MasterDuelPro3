using System;

namespace YgomGame.Duel
{
	// Token: 0x02000EE3 RID: 3811
	public class ReplayBase
	{
		// Token: 0x06006F26 RID: 28454 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void InitReplay()
		{
		}

		// Token: 0x06006F27 RID: 28455 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitRecord()
		{
		}

		// Token: 0x06006F28 RID: 28456 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRecord(byte[] dat = null)
		{
		}

		// Token: 0x06006F29 RID: 28457 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndData()
		{
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x0000216D File Offset: 0x0000036D
		public void Finish()
		{
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x0000216A File Offset: 0x0000036A
		public byte[] GetData()
		{
			return null;
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AllocMemory()
		{
		}

		// Token: 0x06006F2D RID: 28461 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ReleaseMemory()
		{
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ReleaseRecordQueue()
		{
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ReleaseReplayQueue()
		{
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool AddQueueFromData(byte[] data)
		{
			return false;
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddRecordImpl(IntPtr ptr, int size)
		{
		}

		// Token: 0x06006F32 RID: 28466 RVA: 0x000F61B6 File Offset: 0x000F43B6
		protected IntPtr NowRecordImpl()
		{
			return (IntPtr)0;
		}

		// Token: 0x06006F33 RID: 28467 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RecordNextImpl()
		{
		}

		// Token: 0x06006F34 RID: 28468 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RecordBeginImpl()
		{
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int IsRecordEndImpl()
		{
			return 0;
		}

		// Token: 0x0400AA2C RID: 43564
		protected const int ParamSize = 8;

		// Token: 0x0400AA2D RID: 43565
		protected IntPtr recordPtr;

		// Token: 0x0400AA2E RID: 43566
		protected BlockingQueue<byte[]> replayQueue;

		// Token: 0x0400AA2F RID: 43567
		protected BlockingQueue<byte[]> recordQueue;

		// Token: 0x0400AA30 RID: 43568
		protected int recordSize;

		// Token: 0x0400AA31 RID: 43569
		protected bool isEndReplay;

		// Token: 0x0400AA32 RID: 43570
		protected bool dataEnd;
	}
}
