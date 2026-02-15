using System;
using System.Diagnostics;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Represents an exception whose state is captured at a certain point in code. </summary>
	// Token: 0x02000571 RID: 1393
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x06002ABE RID: 10942 RVA: 0x000AA5E4 File Offset: 0x000A87E4
		private ExceptionDispatchInfo(Exception exception)
		{
			this.m_Exception = exception;
			StackTrace[] captured_traces = exception.captured_traces;
			int num = ((captured_traces == null) ? 0 : captured_traces.Length);
			StackTrace[] array = new StackTrace[num + 1];
			if (num != 0)
			{
				Array.Copy(captured_traces, 0, array, 0, num);
			}
			array[num] = new StackTrace(exception, 0, true);
			this.m_stackTrace = array;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000AA637 File Offset: 0x000A8837
		internal object BinaryStackTraceArray
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		/// <summary>Creates an <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object that represents the specified exception at the current point in code. </summary>
		/// <returns>An object that represents the specified exception at the current point in code. </returns>
		/// <param name="source">The exception whose state is captured, and which is represented by the returned object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is null. </exception>
		// Token: 0x06002AC0 RID: 10944 RVA: 0x000AA63F File Offset: 0x000A883F
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Environment.GetResourceString("Object cannot be null."));
			}
			return new ExceptionDispatchInfo(source);
		}

		/// <summary>Gets the exception that is represented by the current instance. </summary>
		/// <returns>The exception that is represented by the current instance. </returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000AA65F File Offset: 0x000A885F
		public Exception SourceException
		{
			get
			{
				return this.m_Exception;
			}
		}

		/// <summary>Throws the exception that is represented by the current <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object, after restoring the state that was saved when the exception was captured. </summary>
		// Token: 0x06002AC2 RID: 10946 RVA: 0x000AA667 File Offset: 0x000A8867
		[StackTraceHidden]
		public void Throw()
		{
			this.m_Exception.RestoreExceptionDispatchInfo(this);
			throw this.m_Exception;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000108FB File Offset: 0x0000EAFB
		[StackTraceHidden]
		public static void Throw(Exception source)
		{
			ExceptionDispatchInfo.Capture(source).Throw();
		}

		// Token: 0x040015B4 RID: 5556
		private Exception m_Exception;

		// Token: 0x040015B5 RID: 5557
		private object m_stackTrace;
	}
}
