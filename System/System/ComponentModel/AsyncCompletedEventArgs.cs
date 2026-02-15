using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodNameCompleted event.</summary>
	// Token: 0x020002B3 RID: 691
	public class AsyncCompletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class. </summary>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		/// <param name="userState">The optional user-supplied state object passed to the <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" /> method.</param>
		// Token: 0x06001059 RID: 4185 RVA: 0x000443D7 File Offset: 0x000425D7
		public AsyncCompletedEventArgs(Exception error, bool cancelled, object userState)
		{
			this.error = error;
			this.cancelled = cancelled;
			this.userState = userState;
		}

		/// <summary>Gets a value indicating whether an asynchronous operation has been canceled.</summary>
		/// <returns>true if the background operation has been canceled; otherwise false. The default is false.</returns>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x000443F4 File Offset: 0x000425F4
		[SRDescription("True if operation was cancelled.")]
		public bool Cancelled
		{
			get
			{
				return this.cancelled;
			}
		}

		/// <summary>Gets a value indicating which error occurred during an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> instance, if an error occurred during an asynchronous operation; otherwise null.</returns>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x000443FC File Offset: 0x000425FC
		[SRDescription("Exception that occurred during operation.  Null if no error.")]
		public Exception Error
		{
			get
			{
				return this.error;
			}
		}

		/// <summary>Raises a user-supplied exception if an asynchronous operation failed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> property is true. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> property has been set by the asynchronous operation. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />. </exception>
		// Token: 0x0600105C RID: 4188 RVA: 0x00044404 File Offset: 0x00042604
		protected void RaiseExceptionIfNecessary()
		{
			if (this.Error != null)
			{
				throw new TargetInvocationException(SR.GetString("An exception occurred during the operation, making the result invalid.  Check InnerException for exception details."), this.Error);
			}
			if (this.Cancelled)
			{
				throw new InvalidOperationException(SR.GetString("Operation has been cancelled."));
			}
		}

		// Token: 0x04000A5A RID: 2650
		private readonly Exception error;

		// Token: 0x04000A5B RID: 2651
		private readonly bool cancelled;

		// Token: 0x04000A5C RID: 2652
		private readonly object userState;
	}
}
