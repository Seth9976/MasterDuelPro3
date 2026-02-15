using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000109 RID: 265
	public class DiagnosticsTraceWriter : ITraceWriter
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00026950 File Offset: 0x00024B50
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00026958 File Offset: 0x00024B58
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x060007B0 RID: 1968 RVA: 0x00026961 File Offset: 0x00024B61
		private TraceEventType GetTraceEventType(TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				return TraceEventType.Error;
			case TraceLevel.Warning:
				return TraceEventType.Warning;
			case TraceLevel.Info:
				return TraceEventType.Information;
			case TraceLevel.Verbose:
				return TraceEventType.Verbose;
			default:
				throw new ArgumentOutOfRangeException("level");
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00026990 File Offset: 0x00024B90
		[NullableContext(1)]
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			if (level == TraceLevel.Off)
			{
				return;
			}
			TraceEventCache traceEventCache = new TraceEventCache();
			TraceEventType traceEventType = this.GetTraceEventType(level);
			foreach (object obj in global::System.Diagnostics.Trace.Listeners)
			{
				TraceListener traceListener = (TraceListener)obj;
				if (!traceListener.IsThreadSafe)
				{
					TraceListener traceListener2 = traceListener;
					lock (traceListener2)
					{
						traceListener.TraceEvent(traceEventCache, "Newtonsoft.Json", traceEventType, 0, message);
						goto IL_006E;
					}
					goto IL_005F;
				}
				goto IL_005F;
				IL_006E:
				if (global::System.Diagnostics.Trace.AutoFlush)
				{
					traceListener.Flush();
					continue;
				}
				continue;
				IL_005F:
				traceListener.TraceEvent(traceEventCache, "Newtonsoft.Json", traceEventType, 0, message);
				goto IL_006E;
			}
		}
	}
}
