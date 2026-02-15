using System;
using System.Diagnostics;

namespace System.Runtime
{
	// Token: 0x02000026 RID: 38
	internal class TraceLevelHelper
	{
		// Token: 0x06000087 RID: 135 RVA: 0x0000392F File Offset: 0x00001B2F
		internal static TraceEventType GetTraceEventType(TraceEventLevel level)
		{
			return TraceLevelHelper.EtwLevelToTraceEventType[(int)level];
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003938 File Offset: 0x00001B38
		internal static string LookupSeverity(TraceEventLevel level, TraceEventOpcode opcode)
		{
			if (opcode <= TraceEventOpcode.Stop)
			{
				if (opcode == TraceEventOpcode.Start)
				{
					return "Start";
				}
				if (opcode == TraceEventOpcode.Stop)
				{
					return "Stop";
				}
			}
			else
			{
				if (opcode == TraceEventOpcode.Resume)
				{
					return "Resume";
				}
				if (opcode == TraceEventOpcode.Suspend)
				{
					return "Suspend";
				}
			}
			string text;
			switch (level)
			{
			case TraceEventLevel.Critical:
				text = "Critical";
				break;
			case TraceEventLevel.Error:
				text = "Error";
				break;
			case TraceEventLevel.Warning:
				text = "Warning";
				break;
			case TraceEventLevel.Informational:
				text = "Information";
				break;
			case TraceEventLevel.Verbose:
				text = "Verbose";
				break;
			default:
				text = level.ToString();
				break;
			}
			return text;
		}

		// Token: 0x04000054 RID: 84
		private static TraceEventType[] EtwLevelToTraceEventType = new TraceEventType[]
		{
			TraceEventType.Critical,
			TraceEventType.Critical,
			TraceEventType.Error,
			TraceEventType.Warning,
			TraceEventType.Information,
			TraceEventType.Verbose
		};
	}
}
