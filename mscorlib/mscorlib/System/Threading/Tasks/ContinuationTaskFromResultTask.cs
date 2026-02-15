using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002BE RID: 702
	internal sealed class ContinuationTaskFromResultTask<TAntecedentResult> : Task
	{
		// Token: 0x0600197B RID: 6523 RVA: 0x000610D0 File Offset: 0x0005F2D0
		public ContinuationTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
			: base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00061104 File Offset: 0x0005F304
		internal override void InnerInvoke()
		{
			Task<TAntecedentResult> antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task<TAntecedentResult>> action = this.m_action as Action<Task<TAntecedentResult>>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task<TAntecedentResult>, object> action2 = this.m_action as Action<Task<TAntecedentResult>, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04000C16 RID: 3094
		private Task<TAntecedentResult> m_antecedent;
	}
}
