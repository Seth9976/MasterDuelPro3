using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034D RID: 845
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.ErrorEventArgs" /> class.</summary>
		/// <param name="exception">An <see cref="T:System.Exception" /> that represents the error that occurred. </param>
		// Token: 0x060014FF RID: 5375 RVA: 0x0005A1D6 File Offset: 0x000583D6
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x04000C4E RID: 3150
		private Exception exception;
	}
}
