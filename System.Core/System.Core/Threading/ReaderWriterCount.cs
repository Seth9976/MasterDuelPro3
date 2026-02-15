using System;

namespace System.Threading
{
	// Token: 0x02000160 RID: 352
	internal class ReaderWriterCount
	{
		// Token: 0x0400038B RID: 907
		public long lockID;

		// Token: 0x0400038C RID: 908
		public int readercount;

		// Token: 0x0400038D RID: 909
		public int writercount;

		// Token: 0x0400038E RID: 910
		public int upgradecount;

		// Token: 0x0400038F RID: 911
		public ReaderWriterCount next;
	}
}
