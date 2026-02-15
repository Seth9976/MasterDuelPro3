using System;

namespace System.Threading
{
	// Token: 0x02000261 RID: 609
	internal class ThreadHelper
	{
		// Token: 0x06001642 RID: 5698 RVA: 0x00058BA9 File Offset: 0x00056DA9
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00058BB8 File Offset: 0x00056DB8
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00058BC4 File Offset: 0x00056DC4
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			if (threadHelper._start is ThreadStart)
			{
				((ThreadStart)threadHelper._start)();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00058C0C File Offset: 0x00056E0C
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00058C40 File Offset: 0x00056E40
		internal void ThreadStart()
		{
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ThreadStart)this._start)();
		}

		// Token: 0x04000ADB RID: 2779
		private Delegate _start;

		// Token: 0x04000ADC RID: 2780
		private object _startArg;

		// Token: 0x04000ADD RID: 2781
		private ExecutionContext _executionContext;

		// Token: 0x04000ADE RID: 2782
		internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
