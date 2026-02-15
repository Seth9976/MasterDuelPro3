using System;
using Internal.Runtime.Augments;

namespace Internal.Threading.Tasks.Tracing
{
	// Token: 0x02000091 RID: 145
	internal static class TaskTrace
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00010854 File Offset: 0x0000EA54
		public static bool Enabled
		{
			get
			{
				TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
				return taskTraceCallbacks != null && taskTraceCallbacks.Enabled;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00010878 File Offset: 0x0000EA78
		public static void TaskWaitBegin_Asynchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitBegin_Asynchronous(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00010898 File Offset: 0x0000EA98
		public static void TaskWaitBegin_Synchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitBegin_Synchronous(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000108B8 File Offset: 0x0000EAB8
		public static void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitEnd(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000108D8 File Offset: 0x0000EAD8
		public static void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskScheduled(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID, CreatingTaskID, TaskCreationOptions);
		}

		// Token: 0x04000271 RID: 625
		private static TaskTraceCallbacks s_callbacks;
	}
}
