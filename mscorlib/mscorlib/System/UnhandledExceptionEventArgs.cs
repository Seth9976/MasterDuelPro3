using System;

namespace System
{
	/// <summary>Provides data for the event that is raised when there is an exception that is not handled in any application domain.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000160 RID: 352
	[Serializable]
	public class UnhandledExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnhandledExceptionEventArgs" /> class with the exception object and a common language runtime termination flag.</summary>
		/// <param name="exception">The exception that is not handled. </param>
		/// <param name="isTerminating">true if the runtime is terminating; otherwise, false. </param>
		// Token: 0x06000CB3 RID: 3251 RVA: 0x000347BA File Offset: 0x000329BA
		public UnhandledExceptionEventArgs(object exception, bool isTerminating)
		{
			this._exception = exception;
			this._isTerminating = isTerminating;
		}

		/// <summary>Gets the unhandled exception object.</summary>
		/// <returns>The unhandled exception object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000347D0 File Offset: 0x000329D0
		public object ExceptionObject
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Indicates whether the common language runtime is terminating.</summary>
		/// <returns>true if the runtime is terminating; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x000347D8 File Offset: 0x000329D8
		public bool IsTerminating
		{
			get
			{
				return this._isTerminating;
			}
		}

		// Token: 0x040004C1 RID: 1217
		private object _exception;

		// Token: 0x040004C2 RID: 1218
		private bool _isTerminating;
	}
}
