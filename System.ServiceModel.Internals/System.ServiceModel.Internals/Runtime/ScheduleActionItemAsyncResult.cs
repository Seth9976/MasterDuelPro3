using System;

namespace System.Runtime
{
	// Token: 0x02000020 RID: 32
	internal abstract class ScheduleActionItemAsyncResult : AsyncResult
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000037CC File Offset: 0x000019CC
		protected ScheduleActionItemAsyncResult(AsyncCallback callback, object state)
			: base(callback, state)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000037D6 File Offset: 0x000019D6
		protected void Schedule()
		{
			ActionItem.Schedule(ScheduleActionItemAsyncResult.doWork, this);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000037E4 File Offset: 0x000019E4
		private static void DoWork(object state)
		{
			ScheduleActionItemAsyncResult scheduleActionItemAsyncResult = (ScheduleActionItemAsyncResult)state;
			Exception ex = null;
			try
			{
				scheduleActionItemAsyncResult.OnDoWork();
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			scheduleActionItemAsyncResult.Complete(false, ex);
		}

		// Token: 0x06000081 RID: 129
		protected abstract void OnDoWork();

		// Token: 0x06000082 RID: 130 RVA: 0x00003828 File Offset: 0x00001A28
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ScheduleActionItemAsyncResult>(result);
		}

		// Token: 0x0400003A RID: 58
		private static Action<object> doWork = new Action<object>(ScheduleActionItemAsyncResult.DoWork);
	}
}
