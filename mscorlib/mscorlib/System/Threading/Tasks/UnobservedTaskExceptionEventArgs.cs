using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides data for the event that is raised when a faulted <see cref="T:System.Threading.Tasks.Task" />'s exception goes unobserved.</summary>
	// Token: 0x020002CE RID: 718
	public class UnobservedTaskExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.UnobservedTaskExceptionEventArgs" /> class with the unobserved exception.</summary>
		/// <param name="exception">The Exception that has gone unobserved.</param>
		// Token: 0x060019D3 RID: 6611 RVA: 0x00061E9F File Offset: 0x0006009F
		public UnobservedTaskExceptionEventArgs(AggregateException exception)
		{
			this.m_exception = exception;
		}

		// Token: 0x04000C37 RID: 3127
		private AggregateException m_exception;

		// Token: 0x04000C38 RID: 3128
		internal bool m_observed;
	}
}
