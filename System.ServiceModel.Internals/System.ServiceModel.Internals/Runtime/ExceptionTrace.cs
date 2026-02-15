using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Diagnostics;

namespace System.Runtime
{
	// Token: 0x0200000F RID: 15
	internal class ExceptionTrace
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000283E File Offset: 0x00000A3E
		public ExceptionTrace(string eventSourceName, EtwDiagnosticTrace diagnosticTrace)
		{
			this.eventSourceName = eventSourceName;
			this.diagnosticTrace = diagnosticTrace;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002854 File Offset: 0x00000A54
		public Exception AsError(Exception exception)
		{
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				return this.AsError<Exception>(ex);
			}
			TargetInvocationException ex2 = exception as TargetInvocationException;
			if (ex2 != null && ex2.InnerException != null)
			{
				return this.AsError(ex2.InnerException);
			}
			return this.TraceException<Exception>(exception);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002899 File Offset: 0x00000A99
		public Exception AsError<TPreferredException>(AggregateException aggregateException)
		{
			return this.AsError<TPreferredException>(aggregateException, this.eventSourceName);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028A8 File Offset: 0x00000AA8
		public Exception AsError<TPreferredException>(AggregateException aggregateException, string eventSource)
		{
			if (Fx.IsFatal(aggregateException))
			{
				return aggregateException;
			}
			ReadOnlyCollection<Exception> innerExceptions = aggregateException.Flatten().InnerExceptions;
			if (innerExceptions.Count == 0)
			{
				return this.TraceException<AggregateException>(aggregateException, eventSource);
			}
			Exception ex = null;
			foreach (Exception ex2 in innerExceptions)
			{
				TargetInvocationException ex3 = ex2 as TargetInvocationException;
				Exception ex4 = ((ex3 != null && ex3.InnerException != null) ? ex3.InnerException : ex2);
				if (ex4 is TPreferredException && ex == null)
				{
					ex = ex4;
				}
				this.TraceException<Exception>(ex4, eventSource);
			}
			if (ex == null)
			{
				ex = innerExceptions[0];
			}
			return ex;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002958 File Offset: 0x00000B58
		public ArgumentException Argument(string paramName, string message)
		{
			return this.TraceException<ArgumentException>(new ArgumentException(message, paramName));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002967 File Offset: 0x00000B67
		public ArgumentNullException ArgumentNull(string paramName)
		{
			return this.TraceException<ArgumentNullException>(new ArgumentNullException(paramName));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002975 File Offset: 0x00000B75
		public ArgumentOutOfRangeException ArgumentOutOfRange(string paramName, object actualValue, string message)
		{
			return this.TraceException<ArgumentOutOfRangeException>(new ArgumentOutOfRangeException(paramName, actualValue, message));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002985 File Offset: 0x00000B85
		public void TraceUnhandledException(Exception exception)
		{
			TraceCore.UnhandledException(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void TraceHandledException(Exception exception, TraceEventType traceEventType)
		{
			if (traceEventType != TraceEventType.Error)
			{
				if (traceEventType != TraceEventType.Warning)
				{
					if (traceEventType != TraceEventType.Verbose)
					{
						if (TraceCore.HandledExceptionIsEnabled(this.diagnosticTrace))
						{
							TraceCore.HandledException(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
						}
					}
					else if (TraceCore.HandledExceptionVerboseIsEnabled(this.diagnosticTrace))
					{
						TraceCore.HandledExceptionVerbose(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
						return;
					}
				}
				else if (TraceCore.HandledExceptionWarningIsEnabled(this.diagnosticTrace))
				{
					TraceCore.HandledExceptionWarning(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
					return;
				}
			}
			else if (TraceCore.HandledExceptionErrorIsEnabled(this.diagnosticTrace))
			{
				TraceCore.HandledExceptionError(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
				return;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A6D File Offset: 0x00000C6D
		private TException TraceException<TException>(TException exception) where TException : Exception
		{
			return this.TraceException<TException>(exception, this.eventSourceName);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A7C File Offset: 0x00000C7C
		private TException TraceException<TException>(TException exception, string eventSource) where TException : Exception
		{
			if (TraceCore.ThrowingExceptionIsEnabled(this.diagnosticTrace))
			{
				TraceCore.ThrowingException(this.diagnosticTrace, eventSource, (exception != null) ? exception.ToString() : string.Empty, exception);
			}
			this.BreakOnException(exception);
			return exception;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002ACF File Offset: 0x00000CCF
		private void BreakOnException(Exception exception)
		{
		}

		// Token: 0x04000026 RID: 38
		private string eventSourceName;

		// Token: 0x04000027 RID: 39
		private readonly EtwDiagnosticTrace diagnosticTrace;
	}
}
