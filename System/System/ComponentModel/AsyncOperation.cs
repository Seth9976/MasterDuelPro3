using System;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Tracks the lifetime of an asynchronous operation.</summary>
	// Token: 0x0200023A RID: 570
	public sealed class AsyncOperation
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x0003DE35 File Offset: 0x0003C035
		private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			this._userSuppliedState = userSuppliedState;
			this._syncContext = syncContext;
			this._alreadyCompleted = false;
			this._syncContext.OperationStarted();
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003DE60 File Offset: 0x0003C060
		~AsyncOperation()
		{
			if (!this._alreadyCompleted && this._syncContext != null)
			{
				this._syncContext.OperationCompleted();
			}
		}

		/// <summary>Invokes a delegate on the thread or context appropriate for the application model.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.ComponentModel.AsyncOperation.PostOperationCompleted(System.Threading.SendOrPostCallback,System.Object)" /> method has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06000DAF RID: 3503 RVA: 0x0003DEA4 File Offset: 0x0003C0A4
		public void Post(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, false);
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003DEAF File Offset: 0x0003C0AF
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			this.PostCore(d, arg, true);
			this.OperationCompletedCore();
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
		private void PostCore(SendOrPostCallback d, object arg, bool markCompleted)
		{
			this.VerifyNotCompleted();
			this.VerifyDelegateNotNull(d);
			if (markCompleted)
			{
				this._alreadyCompleted = true;
			}
			this._syncContext.Post(d, arg);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003DEE8 File Offset: 0x0003C0E8
		private void OperationCompletedCore()
		{
			try
			{
				this._syncContext.OperationCompleted();
			}
			finally
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0003DF1C File Offset: 0x0003C11C
		private void VerifyNotCompleted()
		{
			if (this._alreadyCompleted)
			{
				throw new InvalidOperationException("This operation has already had OperationCompleted called on it and further calls are illegal.");
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003DF31 File Offset: 0x0003C131
		private void VerifyDelegateNotNull(SendOrPostCallback d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "A non-null SendOrPostCallback must be supplied.");
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0003DF46 File Offset: 0x0003C146
		internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			return new AsyncOperation(userSuppliedState, syncContext);
		}

		// Token: 0x04000976 RID: 2422
		private readonly SynchronizationContext _syncContext;

		// Token: 0x04000977 RID: 2423
		private readonly object _userSuppliedState;

		// Token: 0x04000978 RID: 2424
		private bool _alreadyCompleted;
	}
}
