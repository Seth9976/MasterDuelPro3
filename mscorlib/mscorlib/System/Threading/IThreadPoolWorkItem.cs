using System;

namespace System.Threading
{
	// Token: 0x02000266 RID: 614
	internal interface IThreadPoolWorkItem
	{
		// Token: 0x060016E1 RID: 5857
		void ExecuteWorkItem();

		// Token: 0x060016E2 RID: 5858
		void MarkAborted(ThreadAbortException tae);
	}
}
