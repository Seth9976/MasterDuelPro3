using System;
using System.Diagnostics.Tracing;
using System.Threading;

namespace System.Data
{
	// Token: 0x0200002A RID: 42
	[EventSource(Name = "System.Data.DataCommonEventSource")]
	internal class DataCommonEventSource : EventSource
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00012322 File Offset: 0x00010522
		[Event(1, Level = EventLevel.Informational)]
		internal void Trace(string message)
		{
			base.WriteEvent(1, message);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001232C File Offset: 0x0001052C
		[NonEvent]
		internal void Trace<T0>(string format, T0 arg0)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001234D File Offset: 0x0001054D
		[NonEvent]
		internal void Trace<T0, T1>(string format, T0 arg0, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1));
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00012374 File Offset: 0x00010574
		[NonEvent]
		internal void Trace<T0, T1, T2>(string format, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, arg0, arg1, arg2));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000123A4 File Offset: 0x000105A4
		[NonEvent]
		internal void Trace<T0, T1, T2, T3>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000123F8 File Offset: 0x000105F8
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00012454 File Offset: 0x00010654
		[NonEvent]
		internal void Trace<T0, T1, T2, T3, T4, T5, T6>(string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return;
			}
			this.Trace(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6 }));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000124C4 File Offset: 0x000106C4
		[Event(2, Level = EventLevel.Verbose)]
		internal long EnterScope(string message)
		{
			long num = 0L;
			if (DataCommonEventSource.Log.IsEnabled())
			{
				num = Interlocked.Increment(ref DataCommonEventSource.s_nextScopeId);
				base.WriteEvent(2, num, message);
			}
			return num;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000124F5 File Offset: 0x000106F5
		[NonEvent]
		internal long EnterScope<T1>(string format, T1 arg1)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00012518 File Offset: 0x00010718
		[NonEvent]
		internal long EnterScope<T1, T2>(string format, T1 arg1, T2 arg2)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00012541 File Offset: 0x00010741
		[NonEvent]
		internal long EnterScope<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, arg1, arg2, arg3));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00012574 File Offset: 0x00010774
		[NonEvent]
		internal long EnterScope<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (!DataCommonEventSource.Log.IsEnabled())
			{
				return 0L;
			}
			return this.EnterScope(string.Format(format, new object[] { arg1, arg2, arg3, arg4 }));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000125C8 File Offset: 0x000107C8
		[Event(3, Level = EventLevel.Verbose)]
		internal void ExitScope(long scopeId)
		{
			base.WriteEvent(3, scopeId);
		}

		// Token: 0x040000F9 RID: 249
		internal static readonly DataCommonEventSource Log = new DataCommonEventSource();

		// Token: 0x040000FA RID: 250
		private static long s_nextScopeId = 0L;
	}
}
