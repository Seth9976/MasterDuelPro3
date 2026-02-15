using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002C2 RID: 706
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x00061378 File Offset: 0x0005F578
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext)
			: base(action, flowExecutionContext)
		{
			this.m_syncContext = context;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00061389 File Offset: 0x0005F589
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.Current)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000613C4 File Offset: 0x0005F5C4
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000613F0 File Offset: 0x0005F5F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return contextCallback;
		}

		// Token: 0x04000C1B RID: 3099
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04000C1C RID: 3100
		private static ContextCallback s_postActionCallback;

		// Token: 0x04000C1D RID: 3101
		private readonly SynchronizationContext m_syncContext;
	}
}
