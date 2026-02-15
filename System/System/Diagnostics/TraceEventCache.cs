using System;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides trace event data specific to a thread and a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000161 RID: 353
	public class TraceEventCache
	{
		/// <summary>Gets the call stack for the current thread.</summary>
		/// <returns>A string containing stack trace information. This value can be an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" />
		/// </PermissionSet>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0002C920 File Offset: 0x0002AB20
		public string Callstack
		{
			get
			{
				if (this.stackTrace == null)
				{
					this.stackTrace = Environment.StackTrace;
				}
				return this.stackTrace;
			}
		}

		/// <summary>Gets the correlation data, contained in a stack. </summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> containing correlation data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0002C93B File Offset: 0x0002AB3B
		public Stack LogicalOperationStack
		{
			get
			{
				return Trace.CorrelationManager.LogicalOperationStack;
			}
		}

		/// <summary>Gets the date and time at which the event trace occurred.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure whose value is a date and time expressed in Coordinated Universal Time (UTC).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0002C947 File Offset: 0x0002AB47
		public DateTime DateTime
		{
			get
			{
				if (this.dateTime == DateTime.MinValue)
				{
					this.dateTime = DateTime.UtcNow;
				}
				return this.dateTime;
			}
		}

		/// <summary>Gets the unique identifier of the current process.</summary>
		/// <returns>The system-generated unique identifier of the current process.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0002C96C File Offset: 0x0002AB6C
		public int ProcessId
		{
			get
			{
				return TraceEventCache.GetProcessId();
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.  </summary>
		/// <returns>A string that represents a unique integer identifier for this managed thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0002C974 File Offset: 0x0002AB74
		public string ThreadId
		{
			get
			{
				return TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>The tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0002C993 File Offset: 0x0002AB93
		public long Timestamp
		{
			get
			{
				if (this.timeStamp == -1L)
				{
					this.timeStamp = Stopwatch.GetTimestamp();
				}
				return this.timeStamp;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
		private static void InitProcessInfo()
		{
			if (TraceEventCache.processName == null)
			{
				Process currentProcess = Process.GetCurrentProcess();
				try
				{
					TraceEventCache.processId = currentProcess.Id;
					TraceEventCache.processName = currentProcess.ProcessName;
				}
				finally
				{
					currentProcess.Dispose();
				}
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002CA00 File Offset: 0x0002AC00
		internal static int GetProcessId()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processId;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002CA0E File Offset: 0x0002AC0E
		internal static int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x04000644 RID: 1604
		private static volatile int processId;

		// Token: 0x04000645 RID: 1605
		private static volatile string processName;

		// Token: 0x04000646 RID: 1606
		private long timeStamp = -1L;

		// Token: 0x04000647 RID: 1607
		private DateTime dateTime = DateTime.MinValue;

		// Token: 0x04000648 RID: 1608
		private string stackTrace;
	}
}
