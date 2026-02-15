using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Diagnostics;

namespace System.Runtime
{
	// Token: 0x02000029 RID: 41
	internal class TraceCore
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003A38 File Offset: 0x00001C38
		private static ResourceManager ResourceManager
		{
			get
			{
				if (TraceCore.resourceManager == null)
				{
					TraceCore.resourceManager = new ResourceManager("System.Runtime.TraceCore", typeof(TraceCore).Assembly);
				}
				return TraceCore.resourceManager;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003A64 File Offset: 0x00001C64
		internal static CultureInfo Culture
		{
			get
			{
				return TraceCore.resourceCulture;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003A6B File Offset: 0x00001C6B
		internal static bool AppDomainUnloadIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 0);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A80 File Offset: 0x00001C80
		internal static void AppDomainUnload(EtwDiagnosticTrace trace, string appdomainName, string processName, string processId)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 0))
			{
				TraceCore.WriteEtwEvent(trace, 0, null, appdomainName, processName, processId, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("AppDomainUnload", TraceCore.Culture), appdomainName, processName, processId);
				TraceCore.WriteTraceSource(trace, 0, text, serializedPayload);
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003AE7 File Offset: 0x00001CE7
		internal static bool HandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 1);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003AFC File Offset: 0x00001CFC
		internal static void HandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 1))
			{
				TraceCore.WriteEtwEvent(trace, 1, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledException", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 1, text, serializedPayload);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B68 File Offset: 0x00001D68
		internal static void ShipAssertExceptionMessage(EtwDiagnosticTrace trace, string param0)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 2))
			{
				TraceCore.WriteEtwEvent(trace, 2, null, param0, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ShipAssertExceptionMessage", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 2, text, serializedPayload);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003BCB File Offset: 0x00001DCB
		internal static bool ThrowingExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 3);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003BE0 File Offset: 0x00001DE0
		internal static void ThrowingException(EtwDiagnosticTrace trace, string param0, string param1, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 3))
			{
				TraceCore.WriteEtwEvent(trace, 3, null, param0, param1, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("ThrowingException", TraceCore.Culture), param0, param1);
				TraceCore.WriteTraceSource(trace, 3, text, serializedPayload);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003C4C File Offset: 0x00001E4C
		internal static bool UnhandledExceptionIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Critical) || TraceCore.IsEtwEventEnabled(trace, 4);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C60 File Offset: 0x00001E60
		internal static void UnhandledException(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 4))
			{
				TraceCore.WriteEtwEvent(trace, 4, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Critical))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("UnhandledException", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 4, text, serializedPayload);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003CCA File Offset: 0x00001ECA
		internal static bool TraceCodeEventLogCriticalIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Critical) || TraceCore.IsEtwEventEnabled(trace, 5);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003CE0 File Offset: 0x00001EE0
		internal static void TraceCodeEventLogCritical(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 5))
			{
				TraceCore.WriteEtwEvent(trace, 5, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Critical))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogCritical", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 5, text, serializedPayload);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D4D File Offset: 0x00001F4D
		internal static bool TraceCodeEventLogErrorIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 6);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D64 File Offset: 0x00001F64
		internal static void TraceCodeEventLogError(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 6))
			{
				TraceCore.WriteEtwEvent(trace, 6, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogError", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 6, text, serializedPayload);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003DD1 File Offset: 0x00001FD1
		internal static bool TraceCodeEventLogInfoIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Informational) || TraceCore.IsEtwEventEnabled(trace, 7);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003DE8 File Offset: 0x00001FE8
		internal static void TraceCodeEventLogInfo(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 7))
			{
				TraceCore.WriteEtwEvent(trace, 7, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Informational))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogInfo", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 7, text, serializedPayload);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003E55 File Offset: 0x00002055
		internal static bool TraceCodeEventLogVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 8);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003E6C File Offset: 0x0000206C
		internal static void TraceCodeEventLogVerbose(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 8))
			{
				TraceCore.WriteEtwEvent(trace, 8, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogVerbose", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 8, text, serializedPayload);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003ED9 File Offset: 0x000020D9
		internal static bool TraceCodeEventLogWarningIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 9);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003EF0 File Offset: 0x000020F0
		internal static void TraceCodeEventLogWarning(EtwDiagnosticTrace trace, TraceRecord traceRecord)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, traceRecord, null);
			if (TraceCore.IsEtwEventEnabled(trace, 9))
			{
				TraceCore.WriteEtwEvent(trace, 9, null, serializedPayload.ExtendedData, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("TraceCodeEventLogWarning", TraceCore.Culture), Array.Empty<object>());
				TraceCore.WriteTraceSource(trace, 9, text, serializedPayload);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003F60 File Offset: 0x00002160
		internal static bool HandledExceptionWarningIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Warning) || TraceCore.IsEtwEventEnabled(trace, 10);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F78 File Offset: 0x00002178
		internal static void HandledExceptionWarning(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 10))
			{
				TraceCore.WriteEtwEvent(trace, 10, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Warning))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionWarning", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 10, text, serializedPayload);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003FE5 File Offset: 0x000021E5
		internal static bool ActionItemScheduledIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 13);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FF0 File Offset: 0x000021F0
		internal static void ActionItemScheduled(EtwDiagnosticTrace trace, EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 13))
			{
				TraceCore.WriteEtwEvent(trace, 13, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004022 File Offset: 0x00002222
		internal static bool ActionItemCallbackInvokedIsEnabled(EtwDiagnosticTrace trace)
		{
			return TraceCore.IsEtwEventEnabled(trace, 14);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000402C File Offset: 0x0000222C
		internal static void ActionItemCallbackInvoked(EtwDiagnosticTrace trace, EventTraceActivity eventTraceActivity)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, null);
			if (TraceCore.IsEtwEventEnabled(trace, 14))
			{
				TraceCore.WriteEtwEvent(trace, 14, eventTraceActivity, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000405E File Offset: 0x0000225E
		internal static bool HandledExceptionErrorIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Error) || TraceCore.IsEtwEventEnabled(trace, 15);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004074 File Offset: 0x00002274
		internal static void HandledExceptionError(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 15))
			{
				TraceCore.WriteEtwEvent(trace, 15, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Error))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionError", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 15, text, serializedPayload);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000040E1 File Offset: 0x000022E1
		internal static bool HandledExceptionVerboseIsEnabled(EtwDiagnosticTrace trace)
		{
			return trace.ShouldTrace(TraceEventLevel.Verbose) || TraceCore.IsEtwEventEnabled(trace, 16);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000040F8 File Offset: 0x000022F8
		internal static void HandledExceptionVerbose(EtwDiagnosticTrace trace, string param0, Exception exception)
		{
			TracePayload serializedPayload = trace.GetSerializedPayload(null, null, exception);
			if (TraceCore.IsEtwEventEnabled(trace, 16))
			{
				TraceCore.WriteEtwEvent(trace, 16, null, param0, serializedPayload.SerializedException, serializedPayload.AppDomainFriendlyName);
			}
			if (trace.ShouldTraceToTraceSource(TraceEventLevel.Verbose))
			{
				string text = string.Format(TraceCore.Culture, TraceCore.ResourceManager.GetString("HandledExceptionVerbose", TraceCore.Culture), param0);
				TraceCore.WriteTraceSource(trace, 16, text, serializedPayload);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004168 File Offset: 0x00002368
		private static void CreateEventDescriptors()
		{
			TraceCore.eventDescriptors = new EventDescriptor[]
			{
				new EventDescriptor(57393, 0, 19, 4, 0, 0, 1152921504606912512L),
				new EventDescriptor(57394, 0, 18, 4, 0, 0, 2305843009213759488L),
				new EventDescriptor(57395, 0, 18, 2, 0, 0, 2305843009213759488L),
				new EventDescriptor(57396, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(57397, 0, 17, 1, 0, 0, 4611686018427453440L),
				new EventDescriptor(57399, 0, 19, 1, 0, 0, 1152921504606912512L),
				new EventDescriptor(57400, 0, 19, 2, 0, 0, 1152921504606912512L),
				new EventDescriptor(57401, 0, 19, 4, 0, 0, 1152921504606912512L),
				new EventDescriptor(57402, 0, 19, 5, 0, 0, 1152921504606912512L),
				new EventDescriptor(57403, 0, 19, 3, 0, 0, 1152921504606912512L),
				new EventDescriptor(57404, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(131, 0, 19, 5, 12, 2509, 1152921504606912512L),
				new EventDescriptor(132, 0, 19, 5, 13, 2509, 1152921504606912512L),
				new EventDescriptor(133, 0, 19, 5, 1, 2593, 1152921504608944128L),
				new EventDescriptor(134, 0, 19, 5, 2, 2593, 1152921504608944128L),
				new EventDescriptor(57405, 0, 17, 2, 0, 0, 4611686018427453440L),
				new EventDescriptor(57406, 0, 18, 5, 0, 0, 2305843009213759488L),
				new EventDescriptor(57408, 0, 17, 1, 0, 0, 4611686018427453440L),
				new EventDescriptor(57410, 0, 18, 3, 0, 0, 2305843009213759488L),
				new EventDescriptor(57409, 0, 18, 5, 0, 0, 2305843009213759488L),
				new EventDescriptor(57407, 0, 18, 5, 0, 0, 2305843009213759488L)
			};
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004440 File Offset: 0x00002640
		private static void EnsureEventDescriptors()
		{
			if (TraceCore.eventDescriptorsCreated)
			{
				return;
			}
			lock (TraceCore.syncLock)
			{
				if (!TraceCore.eventDescriptorsCreated)
				{
					TraceCore.CreateEventDescriptors();
					TraceCore.eventDescriptorsCreated = true;
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004498 File Offset: 0x00002698
		private static bool IsEtwEventEnabled(EtwDiagnosticTrace trace, int eventIndex)
		{
			if (trace.IsEtwProviderEnabled)
			{
				TraceCore.EnsureEventDescriptors();
				return trace.IsEtwEventEnabled(ref TraceCore.eventDescriptors[eventIndex], false);
			}
			return false;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000044BB File Offset: 0x000026BB
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000044E0 File Offset: 0x000026E0
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004503 File Offset: 0x00002703
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004524 File Offset: 0x00002724
		private static bool WriteEtwEvent(EtwDiagnosticTrace trace, int eventIndex, EventTraceActivity eventParam0, string eventParam1)
		{
			TraceCore.EnsureEventDescriptors();
			return trace.EtwProvider.WriteEvent(ref TraceCore.eventDescriptors[eventIndex], eventParam0, eventParam1);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004543 File Offset: 0x00002743
		private static void WriteTraceSource(EtwDiagnosticTrace trace, int eventIndex, string description, TracePayload payload)
		{
			TraceCore.EnsureEventDescriptors();
			trace.WriteTraceSource(ref TraceCore.eventDescriptors[eventIndex], description, payload);
		}

		// Token: 0x0400005B RID: 91
		private static ResourceManager resourceManager;

		// Token: 0x0400005C RID: 92
		private static CultureInfo resourceCulture;

		// Token: 0x0400005D RID: 93
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x0400005E RID: 94
		private static object syncLock = new object();

		// Token: 0x0400005F RID: 95
		private static volatile bool eventDescriptorsCreated;
	}
}
