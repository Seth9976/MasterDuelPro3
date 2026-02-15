using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002BF RID: 703
	internal sealed class ContinuationResultTaskFromResultTask<TAntecedentResult, TResult> : Task<TResult>
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x0006115C File Offset: 0x0005F35C
		public ContinuationResultTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
			: base(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00061190 File Offset: 0x0005F390
		internal override void InnerInvoke()
		{
			Task<TAntecedentResult> antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task<TAntecedentResult>, TResult> func = this.m_action as Func<Task<TAntecedentResult>, TResult>;
			if (func != null)
			{
				this.m_result = func(antecedent);
				return;
			}
			Func<Task<TAntecedentResult>, object, TResult> func2 = this.m_action as Func<Task<TAntecedentResult>, object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04000C17 RID: 3095
		private Task<TAntecedentResult> m_antecedent;
	}
}
