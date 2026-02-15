using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002BD RID: 701
	internal sealed class ContinuationTaskFromTask : Task
	{
		// Token: 0x06001979 RID: 6521 RVA: 0x00061044 File Offset: 0x0005F244
		public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
			: base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00061078 File Offset: 0x0005F278
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task> action = this.m_action as Action<Task>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task, object> action2 = this.m_action as Action<Task, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04000C15 RID: 3093
		private Task m_antecedent;
	}
}
