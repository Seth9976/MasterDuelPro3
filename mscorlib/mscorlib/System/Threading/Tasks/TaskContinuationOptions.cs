using System;

namespace System.Threading.Tasks
{
	/// <summary>Specifies the behavior for a task that is created by using the <see cref="M:System.Threading.Tasks.Task.ContinueWith(System.Action{System.Threading.Tasks.Task},System.Threading.CancellationToken,System.Threading.Tasks.TaskContinuationOptions,System.Threading.Tasks.TaskScheduler)" /> or <see cref="M:System.Threading.Tasks.Task`1.ContinueWith(System.Action{System.Threading.Tasks.Task{`0}},System.Threading.Tasks.TaskContinuationOptions)" /> method.</summary>
	// Token: 0x020002B7 RID: 695
	[Flags]
	public enum TaskContinuationOptions
	{
		/// <summary>Default = "Continue on any, no task options, run asynchronously" Specifies that the default behavior should be used. Continuations, by default, will be scheduled when the antecedent task completes, regardless of the task's final <see cref="T:System.Threading.Tasks.TaskStatus" />.</summary>
		// Token: 0x04000C01 RID: 3073
		None = 0,
		/// <summary>A hint to a <see cref="T:System.Threading.Tasks.TaskScheduler" /> to schedule a task in as fair a manner as possible, meaning that tasks scheduled sooner will be more likely to be run sooner, and tasks scheduled later will be more likely to be run later.</summary>
		// Token: 0x04000C02 RID: 3074
		PreferFairness = 1,
		/// <summary>Specifies that a task will be a long-running, course-grained operation. It provides a hint to the <see cref="T:System.Threading.Tasks.TaskScheduler" /> that oversubscription may be warranted.</summary>
		// Token: 0x04000C03 RID: 3075
		LongRunning = 2,
		/// <summary>Specifies that a task is attached to a parent in the task hierarchy.</summary>
		// Token: 0x04000C04 RID: 3076
		AttachedToParent = 4,
		/// <summary>Specifies that an <see cref="T:System.InvalidOperationException" /> will be thrown if an attempt is made to attach a child task to the created task.</summary>
		// Token: 0x04000C05 RID: 3077
		DenyChildAttach = 8,
		/// <summary>Prevents the ambient scheduler from being seen as the current scheduler in the created task. This means that operations like StartNew or ContinueWith that are performed in the created task will see <see cref="P:System.Threading.Tasks.TaskScheduler.Default" /> as the current scheduler.</summary>
		// Token: 0x04000C06 RID: 3078
		HideScheduler = 16,
		/// <summary>In the case of continuation cancellation, prevents completion of the continuation until the antecedent has completed.</summary>
		// Token: 0x04000C07 RID: 3079
		LazyCancellation = 32,
		// Token: 0x04000C08 RID: 3080
		RunContinuationsAsynchronously = 64,
		/// <summary>Specifies that the continuation task should not be scheduled if its antecedent ran to completion. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04000C09 RID: 3081
		NotOnRanToCompletion = 65536,
		/// <summary>Specifies that the continuation task should not be scheduled if its antecedent threw an unhandled exception. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04000C0A RID: 3082
		NotOnFaulted = 131072,
		/// <summary>Specifies that the continuation task should not be scheduled if its antecedent was canceled. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04000C0B RID: 3083
		NotOnCanceled = 262144,
		/// <summary>Specifies that the continuation task should be scheduled only if its antecedent ran to completion. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04000C0C RID: 3084
		OnlyOnRanToCompletion = 393216,
		/// <summary>Specifies that the continuation task should be scheduled only if its antecedent threw an unhandled exception. This option is not valid for multi-task continuations.</summary>
		// Token: 0x04000C0D RID: 3085
		OnlyOnFaulted = 327680,
		/// <summary>Specifies that the continuation task should be scheduled only if its antecedent was canceled.  This option is not valid for multi-task continuations.</summary>
		// Token: 0x04000C0E RID: 3086
		OnlyOnCanceled = 196608,
		/// <summary>Specifies that the continuation task should be executed synchronously. With this option specified, the continuation will be run on the same thread that causes the antecedent task to transition into its final state. If the antecedent is already complete when the continuation is created, the continuation will run on the thread creating the continuation. Only very short-running continuations should be executed synchronously.</summary>
		// Token: 0x04000C0F RID: 3087
		ExecuteSynchronously = 524288
	}
}
