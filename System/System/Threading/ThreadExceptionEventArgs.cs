using System;

namespace System.Threading
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000110 RID: 272
	public class ThreadExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that occurred. </param>
		// Token: 0x0600055F RID: 1375 RVA: 0x0001C4CE File Offset: 0x0001A6CE
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		// Token: 0x04000491 RID: 1169
		private Exception exception;
	}
}
