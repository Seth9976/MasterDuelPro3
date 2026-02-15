using System;

namespace System.Threading.Tasks
{
	/// <summary>Represents the current stage in the lifecycle of a <see cref="T:System.Threading.Tasks.Task" />.</summary>
	// Token: 0x020002AD RID: 685
	public enum TaskStatus
	{
		/// <summary>The task has been initialized but has not yet been scheduled.</summary>
		// Token: 0x04000BC2 RID: 3010
		Created,
		/// <summary>The task is waiting to be activated and scheduled internally by the .NET Framework infrastructure.</summary>
		// Token: 0x04000BC3 RID: 3011
		WaitingForActivation,
		/// <summary>The task has been scheduled for execution but has not yet begun executing.</summary>
		// Token: 0x04000BC4 RID: 3012
		WaitingToRun,
		/// <summary>The task is running but has not yet completed.</summary>
		// Token: 0x04000BC5 RID: 3013
		Running,
		/// <summary>The task has finished executing and is implicitly waiting for attached child tasks to complete.</summary>
		// Token: 0x04000BC6 RID: 3014
		WaitingForChildrenToComplete,
		/// <summary>The task completed execution successfully.</summary>
		// Token: 0x04000BC7 RID: 3015
		RanToCompletion,
		/// <summary>The task acknowledged cancellation by throwing an OperationCanceledException with its own CancellationToken while the token was in signaled state, or the task's CancellationToken was already signaled before the task started executing. For more information, see Task Cancellation.</summary>
		// Token: 0x04000BC8 RID: 3016
		Canceled,
		/// <summary>The task completed due to an unhandled exception.</summary>
		// Token: 0x04000BC9 RID: 3017
		Faulted
	}
}
