using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Interop;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200002F RID: 47
	internal abstract class DiagnosticsEventProvider : IDisposable
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000045E4 File Offset: 0x000027E4
		protected DiagnosticsEventProvider(Guid providerGuid)
		{
			this.providerId = providerGuid;
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128)
			{
				return;
			}
			this.EtwRegister();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000461C File Offset: 0x0000281C
		private void EtwRegister()
		{
			this.etwCallback = new UnsafeNativeMethods.EtwEnableCallback(this.EtwEnableCallBack);
			uint num = UnsafeNativeMethods.EventRegister(ref this.providerId, this.etwCallback, null, ref this.traceRegistrationHandle);
			if (num != 0U)
			{
				throw new InvalidOperationException(InternalSR.EtwRegistrationFailed(num.ToString("x", CultureInfo.CurrentCulture)));
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004674 File Offset: 0x00002874
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004683 File Offset: 0x00002883
		protected virtual void Dispose(bool disposing)
		{
			if (this.isDisposed != 1 && Interlocked.Exchange(ref this.isDisposed, 1) == 0)
			{
				this.isProviderEnabled = false;
				this.Deregister();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000046AC File Offset: 0x000028AC
		~DiagnosticsEventProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000046DC File Offset: 0x000028DC
		private void Deregister()
		{
			if (this.traceRegistrationHandle != 0L)
			{
				UnsafeNativeMethods.EventUnregister(this.traceRegistrationHandle);
				this.traceRegistrationHandle = 0L;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000046FA File Offset: 0x000028FA
		private unsafe void EtwEnableCallBack([In] ref Guid sourceId, [In] int isEnabled, [In] byte setLevel, [In] long anyKeyword, [In] long allKeyword, [In] void* filterData, [In] void* callbackContext)
		{
			this.isProviderEnabled = isEnabled != 0;
			this.currentTraceLevel = setLevel;
			this.anyKeywordMask = anyKeyword;
			this.allKeywordMask = allKeyword;
			this.OnControllerCommand();
		}

		// Token: 0x060000D2 RID: 210
		protected abstract void OnControllerCommand();

		// Token: 0x060000D3 RID: 211 RVA: 0x00004723 File Offset: 0x00002923
		public bool IsEnabled()
		{
			return this.isProviderEnabled;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000472B File Offset: 0x0000292B
		public bool IsEnabled(byte level, long keywords)
		{
			return this.isProviderEnabled && (level <= this.currentTraceLevel || this.currentTraceLevel == 0) && (keywords == 0L || ((keywords & this.anyKeywordMask) != 0L && (keywords & this.allKeywordMask) == this.allKeywordMask));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004766 File Offset: 0x00002966
		public bool IsEventEnabled(ref EventDescriptor eventDescriptor)
		{
			return this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords) && UnsafeNativeMethods.EventEnabled(this.traceRegistrationHandle, ref eventDescriptor);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000478A File Offset: 0x0000298A
		private static void SetLastError(int error)
		{
			if (error != 8)
			{
				if (error == 234 || error == 534)
				{
					DiagnosticsEventProvider.errorCode = DiagnosticsEventProvider.WriteEventErrorCode.EventTooBig;
					return;
				}
			}
			else
			{
				DiagnosticsEventProvider.errorCode = DiagnosticsEventProvider.WriteEventErrorCode.NoFreeBuffers;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000047B0 File Offset: 0x000029B0
		public unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, EventTraceActivity eventTraceActivity, string data)
		{
			uint num = 0U;
			data = data ?? string.Empty;
			if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				if (data.Length > 32724)
				{
					DiagnosticsEventProvider.errorCode = DiagnosticsEventProvider.WriteEventErrorCode.EventTooBig;
					return false;
				}
				if (eventTraceActivity != null)
				{
					DiagnosticsEventProvider.SetActivityId(ref eventTraceActivity.ActivityId);
				}
				UnsafeNativeMethods.EventData eventData;
				eventData.Size = (uint)((data.Length + 1) * 2);
				eventData.Reserved = 0;
				fixed (string text = data)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					eventData.DataPointer = ptr;
					num = UnsafeNativeMethods.EventWrite(this.traceRegistrationHandle, ref eventDescriptor, 1U, &eventData);
				}
			}
			if (num != 0U)
			{
				DiagnosticsEventProvider.SetLastError((int)num);
				return false;
			}
			return true;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004854 File Offset: 0x00002A54
		protected internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, EventTraceActivity eventTraceActivity, int dataCount, IntPtr data)
		{
			if (eventTraceActivity != null)
			{
				DiagnosticsEventProvider.SetActivityId(ref eventTraceActivity.ActivityId);
			}
			uint num = UnsafeNativeMethods.EventWrite(this.traceRegistrationHandle, ref eventDescriptor, (uint)dataCount, (UnsafeNativeMethods.EventData*)(void*)data);
			if (num != 0U)
			{
				DiagnosticsEventProvider.SetLastError((int)num);
				return false;
			}
			return true;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004892 File Offset: 0x00002A92
		public static void SetActivityId(ref Guid id)
		{
			UnsafeNativeMethods.EventActivityIdControl(2, ref id);
		}

		// Token: 0x04000063 RID: 99
		private UnsafeNativeMethods.EtwEnableCallback etwCallback;

		// Token: 0x04000064 RID: 100
		private long traceRegistrationHandle;

		// Token: 0x04000065 RID: 101
		private byte currentTraceLevel;

		// Token: 0x04000066 RID: 102
		private long anyKeywordMask;

		// Token: 0x04000067 RID: 103
		private long allKeywordMask;

		// Token: 0x04000068 RID: 104
		private bool isProviderEnabled;

		// Token: 0x04000069 RID: 105
		private Guid providerId;

		// Token: 0x0400006A RID: 106
		private int isDisposed;

		// Token: 0x0400006B RID: 107
		[ThreadStatic]
		private static DiagnosticsEventProvider.WriteEventErrorCode errorCode;

		// Token: 0x02000030 RID: 48
		public enum WriteEventErrorCode
		{
			// Token: 0x0400006D RID: 109
			NoError,
			// Token: 0x0400006E RID: 110
			NoFreeBuffers,
			// Token: 0x0400006F RID: 111
			EventTooBig
		}
	}
}
