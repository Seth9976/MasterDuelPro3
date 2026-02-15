using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Interop;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000038 RID: 56
	internal sealed class EventLogger
	{
		// Token: 0x06000139 RID: 313 RVA: 0x000062FE File Offset: 0x000044FE
		private EventLogger()
		{
			this.isInPartialTrust = this.IsInPartialTrust();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006314 File Offset: 0x00004514
		[Obsolete("For System.Runtime.dll use only. Call FxTrace.EventLog instead")]
		public EventLogger(string eventLogSourceName, DiagnosticTraceBase diagnosticTrace)
		{
			try
			{
				this.diagnosticTrace = diagnosticTrace;
				if (EventLogger.canLogEvent)
				{
					this.SafeSetLogSourceName(eventLogSourceName);
				}
			}
			catch (SecurityException)
			{
				EventLogger.canLogEvent = false;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006358 File Offset: 0x00004558
		public static EventLogger UnsafeCreateEventLogger(string eventLogSourceName, DiagnosticTraceBase diagnosticTrace)
		{
			EventLogger eventLogger = new EventLogger();
			eventLogger.SetLogSourceName(eventLogSourceName, diagnosticTrace);
			return eventLogger;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006368 File Offset: 0x00004568
		public void UnsafeLogEvent(TraceEventType type, ushort eventLogCategory, uint eventId, bool shouldTrace, params string[] values)
		{
			if (EventLogger.logCountForPT < 5)
			{
				try
				{
					int num = 0;
					string[] array = new string[values.Length + 2];
					for (int i = 0; i < values.Length; i++)
					{
						string text = values[i];
						if (!string.IsNullOrEmpty(text))
						{
							text = EventLogger.NormalizeEventLogParameter(text);
						}
						else
						{
							text = string.Empty;
						}
						array[i] = text;
						num += text.Length + 1;
					}
					string text2 = EventLogger.NormalizeEventLogParameter(this.UnsafeGetProcessName());
					array[array.Length - 2] = text2;
					num += text2.Length + 1;
					string text3 = this.UnsafeGetProcessId().ToString(CultureInfo.InvariantCulture);
					array[array.Length - 1] = text3;
					num += text3.Length + 1;
					if (num > 25600)
					{
						int num2 = 25600 / array.Length - 1;
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j].Length > num2)
							{
								array[j] = array[j].Substring(0, num2);
							}
						}
					}
					SecurityIdentifier user = WindowsIdentity.GetCurrent().User;
					byte[] array2 = new byte[user.BinaryLength];
					user.GetBinaryForm(array2, 0);
					IntPtr[] array3 = new IntPtr[array.Length];
					GCHandle gchandle = default(GCHandle);
					GCHandle[] array4 = null;
					try
					{
						gchandle = GCHandle.Alloc(array3, GCHandleType.Pinned);
						array4 = new GCHandle[array.Length];
						for (int k = 0; k < array.Length; k++)
						{
							array4[k] = GCHandle.Alloc(array[k], GCHandleType.Pinned);
							array3[k] = array4[k].AddrOfPinnedObject();
						}
						this.UnsafeWriteEventLog(type, eventLogCategory, eventId, array, array2, gchandle);
					}
					finally
					{
						if (gchandle.AddrOfPinnedObject() != IntPtr.Zero)
						{
							gchandle.Free();
						}
						if (array4 != null)
						{
							foreach (GCHandle gchandle2 in array4)
							{
								gchandle2.Free();
							}
						}
					}
					if (shouldTrace && this.diagnosticTrace != null && this.diagnosticTrace.IsEnabled())
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>(array.Length + 4);
						dictionary["CategoryID.Name"] = "EventLogCategory";
						dictionary["CategoryID.Value"] = eventLogCategory.ToString(CultureInfo.InvariantCulture);
						dictionary["InstanceID.Name"] = "EventId";
						dictionary["InstanceID.Value"] = eventId.ToString(CultureInfo.InvariantCulture);
						for (int m = 0; m < values.Length; m++)
						{
							dictionary.Add("Value" + m.ToString(CultureInfo.InvariantCulture), (values[m] == null) ? string.Empty : DiagnosticTraceBase.XmlEncode(values[m]));
						}
						this.diagnosticTrace.TraceEventLogEvent(type, new DictionaryTraceRecord(dictionary));
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
				}
				if (this.isInPartialTrust)
				{
					EventLogger.logCountForPT++;
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006668 File Offset: 0x00004868
		public void LogEvent(TraceEventType type, ushort eventLogCategory, uint eventId, bool shouldTrace, params string[] values)
		{
			if (EventLogger.canLogEvent)
			{
				try
				{
					this.SafeLogEvent(type, eventLogCategory, eventId, shouldTrace, values);
				}
				catch (SecurityException ex)
				{
					EventLogger.canLogEvent = false;
					if (shouldTrace)
					{
						Fx.Exception.TraceHandledException(ex, TraceEventType.Information);
					}
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000066B4 File Offset: 0x000048B4
		private static EventLogEntryType EventLogEntryTypeFromEventType(TraceEventType type)
		{
			EventLogEntryType eventLogEntryType = EventLogEntryType.Information;
			if (type - TraceEventType.Critical > 1)
			{
				if (type == TraceEventType.Warning)
				{
					eventLogEntryType = EventLogEntryType.Warning;
				}
			}
			else
			{
				eventLogEntryType = EventLogEntryType.Error;
			}
			return eventLogEntryType;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000066D6 File Offset: 0x000048D6
		private void SafeLogEvent(TraceEventType type, ushort eventLogCategory, uint eventId, bool shouldTrace, params string[] values)
		{
			this.UnsafeLogEvent(type, eventLogCategory, eventId, shouldTrace, values);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000066E5 File Offset: 0x000048E5
		private void SafeSetLogSourceName(string eventLogSourceName)
		{
			this.eventLogSourceName = eventLogSourceName;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000066EE File Offset: 0x000048EE
		private void SetLogSourceName(string eventLogSourceName, DiagnosticTraceBase diagnosticTrace)
		{
			this.eventLogSourceName = eventLogSourceName;
			this.diagnosticTrace = diagnosticTrace;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006700 File Offset: 0x00004900
		private bool IsInPartialTrust()
		{
			bool flag = false;
			try
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					flag = string.IsNullOrEmpty(currentProcess.ProcessName);
				}
			}
			catch (SecurityException)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006754 File Offset: 0x00004954
		private void UnsafeWriteEventLog(TraceEventType type, ushort eventLogCategory, uint eventId, string[] logValues, byte[] sidBA, GCHandle stringsRootHandle)
		{
			using (SafeEventLogWriteHandle safeEventLogWriteHandle = SafeEventLogWriteHandle.RegisterEventSource(null, this.eventLogSourceName))
			{
				if (safeEventLogWriteHandle != null)
				{
					HandleRef handleRef = new HandleRef(safeEventLogWriteHandle, stringsRootHandle.AddrOfPinnedObject());
					UnsafeNativeMethods.ReportEvent(safeEventLogWriteHandle, (ushort)EventLogger.EventLogEntryTypeFromEventType(type), eventLogCategory, eventId, sidBA, (ushort)logValues.Length, 0U, handleRef, null);
				}
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000067B8 File Offset: 0x000049B8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string UnsafeGetProcessName()
		{
			string text = null;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				text = currentProcess.ProcessName;
			}
			return text;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000067F4 File Offset: 0x000049F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private int UnsafeGetProcessId()
		{
			int num = -1;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				num = currentProcess.Id;
			}
			return num;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006830 File Offset: 0x00004A30
		internal static string NormalizeEventLogParameter(string eventLogParameter)
		{
			if (eventLogParameter.IndexOf('%') < 0)
			{
				return eventLogParameter;
			}
			StringBuilder stringBuilder = null;
			int length = eventLogParameter.Length;
			for (int i = 0; i < length; i++)
			{
				char c = eventLogParameter[i];
				if (c != '%')
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else if (i + 1 >= length)
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else if (eventLogParameter[i + 1] < '0' || eventLogParameter[i + 1] > '9')
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 2);
						for (int j = 0; j < i; j++)
						{
							stringBuilder.Append(eventLogParameter[j]);
						}
					}
					stringBuilder.Append(c);
					stringBuilder.Append(' ');
				}
			}
			if (stringBuilder == null)
			{
				return eventLogParameter;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400008D RID: 141
		private static int logCountForPT;

		// Token: 0x0400008E RID: 142
		private static bool canLogEvent = true;

		// Token: 0x0400008F RID: 143
		private DiagnosticTraceBase diagnosticTrace;

		// Token: 0x04000090 RID: 144
		private string eventLogSourceName;

		// Token: 0x04000091 RID: 145
		private bool isInPartialTrust;
	}
}
