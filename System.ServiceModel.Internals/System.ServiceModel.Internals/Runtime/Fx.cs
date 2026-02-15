using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000011 RID: 17
	internal static class Fx
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002AED File Offset: 0x00000CED
		public static ExceptionTrace Exception
		{
			get
			{
				if (Fx.exceptionTrace == null)
				{
					Fx.exceptionTrace = new ExceptionTrace("System.Runtime", Fx.Trace);
				}
				return Fx.exceptionTrace;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002B0F File Offset: 0x00000D0F
		public static EtwDiagnosticTrace Trace
		{
			get
			{
				if (Fx.diagnosticTrace == null)
				{
					Fx.diagnosticTrace = Fx.InitializeTracing();
				}
				return Fx.diagnosticTrace;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B28 File Offset: 0x00000D28
		private static EtwDiagnosticTrace InitializeTracing()
		{
			EtwDiagnosticTrace etwDiagnosticTrace = new EtwDiagnosticTrace("System.Runtime", EtwDiagnosticTrace.DefaultEtwProviderId);
			if (etwDiagnosticTrace.EtwProvider != null)
			{
				EtwDiagnosticTrace etwDiagnosticTrace2 = etwDiagnosticTrace;
				etwDiagnosticTrace2.RefreshState = (Action)Delegate.Combine(etwDiagnosticTrace2.RefreshState, new Action(delegate
				{
					Fx.UpdateLevel();
				}));
			}
			Fx.UpdateLevel(etwDiagnosticTrace);
			return etwDiagnosticTrace;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002B89 File Offset: 0x00000D89
		public static Fx.ExceptionHandler AsynchronousThreadExceptionHandler
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return Fx.asynchronousThreadExceptionHandler;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B90 File Offset: 0x00000D90
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndThrow(string description)
		{
			TraceCore.ShipAssertExceptionMessage(Fx.Trace, description);
			throw new Fx.InternalException(description);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BA3 File Offset: 0x00000DA3
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndThrowFatal(string description)
		{
			TraceCore.ShipAssertExceptionMessage(Fx.Trace, description);
			throw new Fx.FatalInternalException(description);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public static bool IsFatal(Exception exception)
		{
			while (exception != null)
			{
				if (exception is FatalException || (exception is OutOfMemoryException && !(exception is InsufficientMemoryException)) || exception is ThreadAbortException || exception is Fx.FatalInternalException)
				{
					return true;
				}
				if (exception is TypeInitializationException || exception is TargetInvocationException)
				{
					exception = exception.InnerException;
				}
				else
				{
					if (exception is AggregateException)
					{
						using (IEnumerator<Exception> enumerator = ((AggregateException)exception).InnerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (Fx.IsFatal(enumerator.Current))
								{
									return true;
								}
							}
							break;
						}
						continue;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C68 File Offset: 0x00000E68
		public static AsyncCallback ThunkCallback(AsyncCallback callback)
		{
			return new Fx.AsyncThunk(callback).ThunkFrame;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C75 File Offset: 0x00000E75
		public static IOCompletionCallback ThunkCallback(IOCompletionCallback callback)
		{
			return new Fx.IOCompletionThunk(callback).ThunkFrame;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C84 File Offset: 0x00000E84
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void TraceExceptionNoThrow(Exception exception)
		{
			try
			{
				Fx.Exception.TraceUnhandledException(exception);
			}
			catch
			{
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CB4 File Offset: 0x00000EB4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static bool HandleAtThreadBase(Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			Fx.TraceExceptionNoThrow(exception);
			try
			{
				Fx.ExceptionHandler exceptionHandler = Fx.AsynchronousThreadExceptionHandler;
				return exceptionHandler != null && exceptionHandler.HandleException(exception);
			}
			catch (Exception ex)
			{
				Fx.TraceExceptionNoThrow(ex);
			}
			return false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CFC File Offset: 0x00000EFC
		private static void UpdateLevel(EtwDiagnosticTrace trace)
		{
			if (trace == null)
			{
				return;
			}
			if (TraceCore.ActionItemCallbackInvokedIsEnabled(trace) || TraceCore.ActionItemScheduledIsEnabled(trace))
			{
				trace.SetEnd2EndActivityTracingEnabled(true);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D19 File Offset: 0x00000F19
		private static void UpdateLevel()
		{
			Fx.UpdateLevel(Fx.Trace);
		}

		// Token: 0x04000028 RID: 40
		private static ExceptionTrace exceptionTrace;

		// Token: 0x04000029 RID: 41
		private static EtwDiagnosticTrace diagnosticTrace;

		// Token: 0x0400002A RID: 42
		private static Fx.ExceptionHandler asynchronousThreadExceptionHandler;

		// Token: 0x02000012 RID: 18
		public abstract class ExceptionHandler
		{
			// Token: 0x06000052 RID: 82
			public abstract bool HandleException(Exception exception);
		}

		// Token: 0x02000013 RID: 19
		private abstract class Thunk<T> where T : class
		{
			// Token: 0x06000053 RID: 83 RVA: 0x00002D25 File Offset: 0x00000F25
			protected Thunk(T callback)
			{
				this.callback = callback;
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D34 File Offset: 0x00000F34
			internal T Callback
			{
				get
				{
					return this.callback;
				}
			}

			// Token: 0x0400002B RID: 43
			private T callback;
		}

		// Token: 0x02000014 RID: 20
		private sealed class AsyncThunk : Fx.Thunk<AsyncCallback>
		{
			// Token: 0x06000055 RID: 85 RVA: 0x00002D3C File Offset: 0x00000F3C
			public AsyncThunk(AsyncCallback callback)
				: base(callback)
			{
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D45 File Offset: 0x00000F45
			public AsyncCallback ThunkFrame
			{
				get
				{
					return new AsyncCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002D54 File Offset: 0x00000F54
			private void UnhandledExceptionFrame(IAsyncResult result)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(result);
				}
				catch (Exception ex)
				{
					if (!Fx.HandleAtThreadBase(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000015 RID: 21
		private sealed class IOCompletionThunk
		{
			// Token: 0x06000058 RID: 88 RVA: 0x00002D90 File Offset: 0x00000F90
			public IOCompletionThunk(IOCompletionCallback callback)
			{
				this.callback = callback;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000059 RID: 89 RVA: 0x00002D9F File Offset: 0x00000F9F
			public IOCompletionCallback ThunkFrame
			{
				get
				{
					return new IOCompletionCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002DB0 File Offset: 0x00000FB0
			private unsafe void UnhandledExceptionFrame(uint error, uint bytesRead, NativeOverlapped* nativeOverlapped)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this.callback(error, bytesRead, nativeOverlapped);
				}
				catch (Exception ex)
				{
					if (!Fx.HandleAtThreadBase(ex))
					{
						throw;
					}
				}
			}

			// Token: 0x0400002C RID: 44
			private IOCompletionCallback callback;
		}

		// Token: 0x02000016 RID: 22
		[Serializable]
		private class InternalException : SystemException
		{
			// Token: 0x0600005B RID: 91 RVA: 0x00002DF0 File Offset: 0x00000FF0
			public InternalException(string description)
				: base(InternalSR.ShipAssertExceptionMessage(description))
			{
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002AE3 File Offset: 0x00000CE3
			protected InternalException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}

		// Token: 0x02000017 RID: 23
		[Serializable]
		private class FatalInternalException : Fx.InternalException
		{
			// Token: 0x0600005D RID: 93 RVA: 0x00002DFE File Offset: 0x00000FFE
			public FatalInternalException(string description)
				: base(description)
			{
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002E07 File Offset: 0x00001007
			protected FatalInternalException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}
	}
}
