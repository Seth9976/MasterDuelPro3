using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;

namespace System.Threading
{
	// Token: 0x02000256 RID: 598
	internal struct ExecutionContextSwitcher
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x00057B68 File Offset: 0x00055D68
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00057B98 File Offset: 0x00055D98
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void Undo()
		{
			if (this.thread == null)
			{
				return;
			}
			Thread thread = this.thread;
			ExecutionContext.Reader executionContextReader = thread.GetExecutionContextReader();
			thread.SetExecutionContext(this.outerEC, this.outerECBelongsToScope);
			this.thread = null;
			ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), this.outerEC.DangerousGetRawExecutionContext());
		}

		// Token: 0x04000AB8 RID: 2744
		internal ExecutionContext.Reader outerEC;

		// Token: 0x04000AB9 RID: 2745
		internal bool outerECBelongsToScope;

		// Token: 0x04000ABA RID: 2746
		internal object hecsw;

		// Token: 0x04000ABB RID: 2747
		internal Thread thread;
	}
}
