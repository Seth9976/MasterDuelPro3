using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x02000164 RID: 356
	internal static class TraceInternal
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0002CA78 File Offset: 0x0002AC78
		public static TraceListenerCollection Listeners
		{
			get
			{
				TraceInternal.InitializeSettings();
				if (TraceInternal.listeners == null)
				{
					object obj = TraceInternal.critSec;
					lock (obj)
					{
						if (TraceInternal.listeners == null)
						{
							SystemDiagnosticsSection systemDiagnosticsSection = DiagnosticsConfiguration.SystemDiagnosticsSection;
							if (systemDiagnosticsSection != null)
							{
								TraceInternal.listeners = systemDiagnosticsSection.Trace.Listeners.GetRuntimeObject();
							}
							else
							{
								TraceInternal.listeners = new TraceListenerCollection();
								TraceListener traceListener = new DefaultTraceListener();
								traceListener.IndentLevel = TraceInternal.indentLevel;
								traceListener.IndentSize = TraceInternal.indentSize;
								TraceInternal.listeners.Add(traceListener);
							}
						}
					}
				}
				return TraceInternal.listeners;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0002CB2C File Offset: 0x0002AD2C
		public static bool AutoFlush
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.autoFlush;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0002CB3A File Offset: 0x0002AD3A
		public static bool UseGlobalLock
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.useGlobalLock;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0002CB48 File Offset: 0x0002AD48
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.indentLevel;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0002CB4F File Offset: 0x0002AD4F
		public static int IndentSize
		{
			get
			{
				TraceInternal.InitializeSettings();
				return TraceInternal.indentSize;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002CB60 File Offset: 0x0002AD60
		private static void SetIndentSize(int value)
		{
			object obj = TraceInternal.critSec;
			lock (obj)
			{
				if (value < 0)
				{
					value = 0;
				}
				TraceInternal.indentSize = value;
				if (TraceInternal.listeners != null)
				{
					foreach (object obj2 in TraceInternal.Listeners)
					{
						((TraceListener)obj2).IndentSize = TraceInternal.indentSize;
					}
				}
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002CBFC File Offset: 0x0002ADFC
		public static void Assert(bool condition, string message)
		{
			if (condition)
			{
				return;
			}
			TraceInternal.Fail(message);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002CC08 File Offset: 0x0002AE08
		public static void Fail(string message)
		{
			if (TraceInternal.UseGlobalLock)
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					using (IEnumerator enumerator = TraceInternal.Listeners.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							TraceListener traceListener = (TraceListener)obj2;
							traceListener.Fail(message);
							if (TraceInternal.AutoFlush)
							{
								traceListener.Flush();
							}
						}
						return;
					}
				}
			}
			foreach (object obj3 in TraceInternal.Listeners)
			{
				TraceListener traceListener2 = (TraceListener)obj3;
				if (!traceListener2.IsThreadSafe)
				{
					TraceListener traceListener3 = traceListener2;
					lock (traceListener3)
					{
						traceListener2.Fail(message);
						if (TraceInternal.AutoFlush)
						{
							traceListener2.Flush();
						}
						continue;
					}
				}
				traceListener2.Fail(message);
				if (TraceInternal.AutoFlush)
				{
					traceListener2.Flush();
				}
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002CD44 File Offset: 0x0002AF44
		private static void InitializeSettings()
		{
			if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
			{
				object obj = TraceInternal.critSec;
				lock (obj)
				{
					if (!TraceInternal.settingsInitialized || (TraceInternal.defaultInitialized && DiagnosticsConfiguration.IsInitialized()))
					{
						TraceInternal.defaultInitialized = DiagnosticsConfiguration.IsInitializing();
						TraceInternal.SetIndentSize(DiagnosticsConfiguration.IndentSize);
						TraceInternal.autoFlush = DiagnosticsConfiguration.AutoFlush;
						TraceInternal.useGlobalLock = DiagnosticsConfiguration.UseGlobalLock;
						TraceInternal.settingsInitialized = true;
					}
				}
			}
		}

		// Token: 0x04000655 RID: 1621
		private static volatile string appName = null;

		// Token: 0x04000656 RID: 1622
		private static volatile TraceListenerCollection listeners;

		// Token: 0x04000657 RID: 1623
		private static volatile bool autoFlush;

		// Token: 0x04000658 RID: 1624
		private static volatile bool useGlobalLock;

		// Token: 0x04000659 RID: 1625
		[ThreadStatic]
		private static int indentLevel;

		// Token: 0x0400065A RID: 1626
		private static volatile int indentSize;

		// Token: 0x0400065B RID: 1627
		private static volatile bool settingsInitialized;

		// Token: 0x0400065C RID: 1628
		private static volatile bool defaultInitialized;

		// Token: 0x0400065D RID: 1629
		internal static readonly object critSec = new object();
	}
}
