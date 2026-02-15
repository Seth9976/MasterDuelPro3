using System;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000093 RID: 147
	internal abstract class TaskTraceCallbacks
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002B9 RID: 697
		public abstract bool Enabled { get; }

		// Token: 0x060002BA RID: 698
		public abstract void TaskWaitBegin_Asynchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x060002BB RID: 699
		public abstract void TaskWaitBegin_Synchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x060002BC RID: 700
		public abstract void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x060002BD RID: 701
		public abstract void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions);
	}
}
