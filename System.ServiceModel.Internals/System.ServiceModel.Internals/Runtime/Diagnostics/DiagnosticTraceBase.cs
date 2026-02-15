using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000031 RID: 49
	internal abstract class DiagnosticTraceBase
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000489C File Offset: 0x00002A9C
		public DiagnosticTraceBase(string traceSourceName)
		{
			this.thisLock = new object();
			this.TraceSourceName = traceSourceName;
			this.LastFailure = DateTime.MinValue;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000048C8 File Offset: 0x00002AC8
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000048D0 File Offset: 0x00002AD0
		protected DateTime LastFailure { get; set; }

		// Token: 0x060000DD RID: 221 RVA: 0x000048D9 File Offset: 0x00002AD9
		private static void UnsafeRemoveDefaultTraceListener(TraceSource traceSource)
		{
			traceSource.Listeners.Remove("Default");
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000048EB File Offset: 0x00002AEB
		public TraceSource TraceSource
		{
			get
			{
				return this.traceSource;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000048F3 File Offset: 0x00002AF3
		protected void SetTraceSource(TraceSource traceSource)
		{
			if (traceSource != null)
			{
				DiagnosticTraceBase.UnsafeRemoveDefaultTraceListener(traceSource);
				this.traceSource = traceSource;
				this.haveListeners = this.traceSource.Listeners.Count > 0;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000491E File Offset: 0x00002B1E
		public bool HaveListeners
		{
			get
			{
				return this.haveListeners;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004928 File Offset: 0x00002B28
		private SourceLevels FixLevel(SourceLevels level)
		{
			if ((level & (SourceLevels)(-16) & SourceLevels.Verbose) != SourceLevels.Off)
			{
				level |= SourceLevels.Verbose;
			}
			else if ((level & (SourceLevels)(-8) & SourceLevels.Information) != SourceLevels.Off)
			{
				level |= SourceLevels.Information;
			}
			else if ((level & (SourceLevels)(-4) & SourceLevels.Warning) != SourceLevels.Off)
			{
				level |= SourceLevels.Warning;
			}
			if ((level & ~SourceLevels.Critical & SourceLevels.Error) != SourceLevels.Off)
			{
				level |= SourceLevels.Error;
			}
			if ((level & SourceLevels.Critical) != SourceLevels.Off)
			{
				level |= SourceLevels.Critical;
			}
			if (level == SourceLevels.ActivityTracing)
			{
				level = SourceLevels.Off;
			}
			return level;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002ACF File Offset: 0x00000CCF
		protected virtual void OnSetLevel(SourceLevels level)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004988 File Offset: 0x00002B88
		private void SetLevel(SourceLevels level)
		{
			SourceLevels sourceLevels = this.FixLevel(level);
			this.level = sourceLevels;
			if (this.TraceSource != null)
			{
				this.haveListeners = this.TraceSource.Listeners.Count > 0;
				this.OnSetLevel(level);
				this.tracingEnabled = this.HaveListeners && level > SourceLevels.Off;
				this.TraceSource.Switch.Level = level;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000049F2 File Offset: 0x00002BF2
		public SourceLevels Level
		{
			get
			{
				if (this.TraceSource != null && this.TraceSource.Switch.Level != this.level)
				{
					this.level = this.TraceSource.Switch.Level;
				}
				return this.level;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004A30 File Offset: 0x00002C30
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00004A38 File Offset: 0x00002C38
		protected string EventSourceName
		{
			get
			{
				return this.eventSourceName;
			}
			set
			{
				this.eventSourceName = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004A41 File Offset: 0x00002C41
		public bool TracingEnabled
		{
			get
			{
				return this.tracingEnabled && this.traceSource != null;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004A58 File Offset: 0x00002C58
		protected static string ProcessName
		{
			get
			{
				string text = null;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					text = currentProcess.ProcessName;
				}
				return text;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004A94 File Offset: 0x00002C94
		protected static int ProcessId
		{
			get
			{
				int num = -1;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					num = currentProcess.Id;
				}
				return num;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public virtual bool ShouldTrace(TraceEventLevel level)
		{
			return this.ShouldTraceToTraceSource(level);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004AD9 File Offset: 0x00002CD9
		public bool ShouldTrace(TraceEventType type)
		{
			return this.TracingEnabled && this.HaveListeners && this.TraceSource != null && (type & (TraceEventType)this.Level) > (TraceEventType)0;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004B00 File Offset: 0x00002D00
		public bool ShouldTraceToTraceSource(TraceEventLevel level)
		{
			return this.ShouldTrace(TraceLevelHelper.GetTraceEventType(level));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004B10 File Offset: 0x00002D10
		public static string XmlEncode(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(length + 8);
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c != '&')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							stringBuilder.Append(c);
						}
						else
						{
							stringBuilder.Append("&gt;");
						}
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&amp;");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004B94 File Offset: 0x00002D94
		protected void AddDomainEventHandlersForCleanup()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			if (this.TraceSource != null)
			{
				this.haveListeners = this.TraceSource.Listeners.Count > 0;
			}
			this.tracingEnabled = this.haveListeners;
			if (this.TracingEnabled)
			{
				currentDomain.UnhandledException += this.UnhandledExceptionHandler;
				this.SetLevel(this.TraceSource.Switch.Level);
				currentDomain.DomainUnload += this.ExitOrUnloadEventHandler;
				currentDomain.ProcessExit += this.ExitOrUnloadEventHandler;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004C28 File Offset: 0x00002E28
		private void ExitOrUnloadEventHandler(object sender, EventArgs e)
		{
			this.ShutdownTracing();
		}

		// Token: 0x060000F0 RID: 240
		protected abstract void OnUnhandledException(Exception exception);

		// Token: 0x060000F1 RID: 241 RVA: 0x00004C30 File Offset: 0x00002E30
		protected void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception ex = (Exception)args.ExceptionObject;
			this.OnUnhandledException(ex);
			this.ShutdownTracing();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004C58 File Offset: 0x00002E58
		protected static string CreateSourceString(object source)
		{
			ITraceSourceStringProvider traceSourceStringProvider = source as ITraceSourceStringProvider;
			if (traceSourceStringProvider != null)
			{
				return traceSourceStringProvider.GetSourceString();
			}
			return DiagnosticTraceBase.CreateDefaultSourceString(source);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004C7C File Offset: 0x00002E7C
		internal static string CreateDefaultSourceString(object source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Format(CultureInfo.CurrentCulture, "{0}/{1}", source.GetType().ToString(), source.GetHashCode());
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004CB4 File Offset: 0x00002EB4
		protected static string StackTraceString(Exception exception)
		{
			string text = exception.StackTrace;
			if (string.IsNullOrEmpty(text))
			{
				StackFrame[] frames = new StackTrace(false).GetFrames();
				int num = 0;
				bool flag = false;
				StackFrame[] array = frames;
				for (int i = 0; i < array.Length; i++)
				{
					string name = array[i].GetMethod().Name;
					if (name == "StackTraceString" || name == "AddExceptionToTraceString" || name == "BuildTrace" || name == "TraceEvent" || name == "TraceException" || name == "GetAdditionalPayload")
					{
						num++;
					}
					else if (name.StartsWith("ThrowHelper", StringComparison.Ordinal))
					{
						num++;
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						break;
					}
				}
				text = new StackTrace(num, false).ToString();
			}
			return text;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004D90 File Offset: 0x00002F90
		protected void LogTraceFailure(string traceString, Exception exception)
		{
			TimeSpan timeSpan = TimeSpan.FromMinutes(10.0);
			try
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (DateTime.UtcNow.Subtract(this.LastFailure) >= timeSpan)
					{
						this.LastFailure = DateTime.UtcNow;
						EventLogger eventLogger = EventLogger.UnsafeCreateEventLogger(this.eventSourceName, this);
						if (exception == null)
						{
							eventLogger.UnsafeLogEvent(TraceEventType.Error, 4, 3221291112U, false, new string[] { traceString });
						}
						else
						{
							eventLogger.UnsafeLogEvent(TraceEventType.Error, 4, 3221291113U, false, new string[]
							{
								traceString,
								exception.ToString()
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060000F6 RID: 246
		protected abstract void OnShutdownTracing();

		// Token: 0x060000F7 RID: 247 RVA: 0x00004E68 File Offset: 0x00003068
		private void ShutdownTracing()
		{
			if (!this.calledShutdown)
			{
				this.calledShutdown = true;
				try
				{
					this.OnShutdownTracing();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.LogTraceFailure(null, ex);
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004EB4 File Offset: 0x000030B4
		protected bool CalledShutdown
		{
			get
			{
				return this.calledShutdown;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004EBC File Offset: 0x000030BC
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004EE8 File Offset: 0x000030E8
		public static Guid ActivityId
		{
			get
			{
				object obj = Trace.CorrelationManager.ActivityId;
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				Trace.CorrelationManager.ActivityId = value;
			}
		}

		// Token: 0x060000FB RID: 251
		public abstract bool IsEnabled();

		// Token: 0x060000FC RID: 252
		public abstract void TraceEventLogEvent(TraceEventType type, TraceRecord traceRecord);

		// Token: 0x04000070 RID: 112
		protected static string AppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;

		// Token: 0x04000071 RID: 113
		private object thisLock;

		// Token: 0x04000072 RID: 114
		private bool tracingEnabled = true;

		// Token: 0x04000073 RID: 115
		private bool calledShutdown;

		// Token: 0x04000074 RID: 116
		private bool haveListeners;

		// Token: 0x04000075 RID: 117
		private SourceLevels level;

		// Token: 0x04000076 RID: 118
		protected string TraceSourceName;

		// Token: 0x04000077 RID: 119
		private TraceSource traceSource;

		// Token: 0x04000078 RID: 120
		private string eventSourceName;
	}
}
