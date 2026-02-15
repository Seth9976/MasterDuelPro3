using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.ComponentModel
{
	/// <summary>Executes an operation on a separate thread.</summary>
	// Token: 0x0200023C RID: 572
	[DefaultEvent("DoWork")]
	public class BackgroundWorker : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0003DF74 File Offset: 0x0003C174
		public BackgroundWorker()
		{
			this._operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
			this._progressReporter = new SendOrPostCallback(this.ProgressReporter);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003DFA0 File Offset: 0x0003C1A0
		private void AsyncOperationCompleted(object arg)
		{
			this._isRunning = false;
			this._cancellationPending = false;
			this.OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync" /> is called.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000DBA RID: 3514 RVA: 0x0003DFBC File Offset: 0x0003C1BC
		// (remove) Token: 0x06000DBB RID: 3515 RVA: 0x0003DFF4 File Offset: 0x0003C1F4
		public event DoWorkEventHandler DoWork;

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation.</summary>
		/// <returns>true, if the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation; otherwise, false.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0003E029 File Offset: 0x0003C229
		public bool IsBusy
		{
			get
			{
				return this._isRunning;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000DBD RID: 3517 RVA: 0x0003E034 File Offset: 0x0003C234
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWork = this.DoWork;
			if (doWork != null)
			{
				doWork(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DBE RID: 3518 RVA: 0x0003E054 File Offset: 0x0003C254
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompleted = this.RunWorkerCompleted;
			if (runWorkerCompleted != null)
			{
				runWorkerCompleted(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DBF RID: 3519 RVA: 0x0003E074 File Offset: 0x0003C274
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChanged = this.ProgressChanged;
			if (progressChanged != null)
			{
				progressChanged(this, e);
			}
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)" /> is called.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000DC0 RID: 3520 RVA: 0x0003E094 File Offset: 0x0003C294
		// (remove) Token: 0x06000DC1 RID: 3521 RVA: 0x0003E0CC File Offset: 0x0003C2CC
		public event ProgressChangedEventHandler ProgressChanged;

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003E101 File Offset: 0x0003C301
		private void ProgressReporter(object arg)
		{
			this.OnProgressChanged((ProgressChangedEventArgs)arg);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003E10F File Offset: 0x0003C30F
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <param name="userState">The state object passed to <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003E11C File Offset: 0x0003C31C
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException("This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress.");
			}
			ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(percentProgress, userState);
			if (this._asyncOperation != null)
			{
				this._asyncOperation.Post(this._progressReporter, progressChangedEventArgs);
				return;
			}
			this._progressReporter(progressChangedEventArgs);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <param name="argument">A parameter for use by the background operation to be executed in the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true. </exception>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003E16C File Offset: 0x0003C36C
		public void RunWorkerAsync(object argument)
		{
			if (this._isRunning)
			{
				throw new InvalidOperationException("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.");
			}
			this._isRunning = true;
			this._cancellationPending = false;
			this._asyncOperation = AsyncOperationManager.CreateOperation(null);
			Task.Factory.StartNew(delegate(object arg)
			{
				this.WorkerThreadStart(arg);
			}, argument, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000DC6 RID: 3526 RVA: 0x0003E1CC File Offset: 0x0003C3CC
		// (remove) Token: 0x06000DC7 RID: 3527 RVA: 0x0003E204 File Offset: 0x0003C404
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> can report progress updates.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports progress updates; otherwise false. The default is false.</returns>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0003E239 File Offset: 0x0003C439
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0003E241 File Offset: 0x0003C441
		public bool WorkerReportsProgress
		{
			get
			{
				return this._workerReportsProgress;
			}
			set
			{
				this._workerReportsProgress = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports asynchronous cancellation.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports cancellation; otherwise false. The default is false.</returns>
		// Token: 0x170002E9 RID: 745
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0003E24A File Offset: 0x0003C44A
		public bool WorkerSupportsCancellation
		{
			set
			{
				this._canCancelWorker = value;
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003E254 File Offset: 0x0003C454
		private void WorkerThreadStart(object argument)
		{
			object obj = null;
			Exception ex = null;
			bool flag = false;
			try
			{
				DoWorkEventArgs doWorkEventArgs = new DoWorkEventArgs(argument);
				this.OnDoWork(doWorkEventArgs);
				if (doWorkEventArgs.Cancel)
				{
					flag = true;
				}
				else
				{
					obj = doWorkEventArgs.Result;
				}
			}
			catch (Exception ex)
			{
			}
			RunWorkerCompletedEventArgs runWorkerCompletedEventArgs = new RunWorkerCompletedEventArgs(obj, ex, flag);
			this._asyncOperation.PostOperationCompleted(this._operationCompleted, runWorkerCompletedEventArgs);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x04000979 RID: 2425
		private bool _canCancelWorker;

		// Token: 0x0400097A RID: 2426
		private bool _workerReportsProgress;

		// Token: 0x0400097B RID: 2427
		private bool _cancellationPending;

		// Token: 0x0400097C RID: 2428
		private bool _isRunning;

		// Token: 0x0400097D RID: 2429
		private AsyncOperation _asyncOperation;

		// Token: 0x0400097E RID: 2430
		private readonly SendOrPostCallback _operationCompleted;

		// Token: 0x0400097F RID: 2431
		private readonly SendOrPostCallback _progressReporter;
	}
}
