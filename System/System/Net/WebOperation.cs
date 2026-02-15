using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000443 RID: 1091
	internal class WebOperation
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x000784D6 File Offset: 0x000766D6
		public HttpWebRequest Request { get; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x000784DE File Offset: 0x000766DE
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x000784E6 File Offset: 0x000766E6
		public WebConnection Connection { get; private set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000784EF File Offset: 0x000766EF
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x000784F7 File Offset: 0x000766F7
		public ServicePoint ServicePoint { get; private set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00078500 File Offset: 0x00076700
		public BufferOffsetSize WriteBuffer { get; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x00078508 File Offset: 0x00076708
		public bool IsNtlmChallenge { get; }

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00078510 File Offset: 0x00076710
		public WebOperation(HttpWebRequest request, BufferOffsetSize writeBuffer, bool isNtlmChallenge, CancellationToken cancellationToken)
		{
			this.Request = request;
			this.WriteBuffer = writeBuffer;
			this.IsNtlmChallenge = isNtlmChallenge;
			this.cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this.requestTask = new WebCompletionSource<WebRequestStream>(true);
			this.requestWrittenTask = new WebCompletionSource<WebRequestStream>(true);
			this.responseTask = new WebCompletionSource<WebResponseStream>(true);
			this.finishedTask = new WebCompletionSource<ValueTuple<bool, WebOperation>>(true);
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00078575 File Offset: 0x00076775
		public bool Aborted
		{
			get
			{
				return this.disposedInfo != null || this.Request.Aborted || (this.cts != null && this.cts.IsCancellationRequested);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000785A6 File Offset: 0x000767A6
		public bool Closed
		{
			get
			{
				return this.Aborted || this.closedInfo != null;
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000785BB File Offset: 0x000767BB
		public void Abort()
		{
			if (!this.SetDisposed(ref this.disposedInfo).Item2)
			{
				return;
			}
			CancellationTokenSource cancellationTokenSource = this.cts;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this.SetCanceled();
			this.Close();
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000785F0 File Offset: 0x000767F0
		public void Close()
		{
			if (!this.SetDisposed(ref this.closedInfo).Item2)
			{
				return;
			}
			WebRequestStream webRequestStream = Interlocked.Exchange<WebRequestStream>(ref this.writeStream, null);
			if (webRequestStream != null)
			{
				try
				{
					webRequestStream.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0007863C File Offset: 0x0007683C
		private void SetCanceled()
		{
			OperationCanceledException ex = new OperationCanceledException();
			this.requestTask.TrySetCanceled(ex);
			this.requestWrittenTask.TrySetCanceled(ex);
			this.responseTask.TrySetCanceled(ex);
			this.Finish(false, ex);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0007867E File Offset: 0x0007687E
		private void SetError(Exception error)
		{
			this.requestTask.TrySetException(error);
			this.requestWrittenTask.TrySetException(error);
			this.responseTask.TrySetException(error);
			this.Finish(false, error);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000786B0 File Offset: 0x000768B0
		private ValueTuple<ExceptionDispatchInfo, bool> SetDisposed(ref ExceptionDispatchInfo field)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new WebException(SR.GetString("The request was canceled"), WebExceptionStatus.RequestCanceled));
			ExceptionDispatchInfo exceptionDispatchInfo2 = Interlocked.CompareExchange<ExceptionDispatchInfo>(ref field, exceptionDispatchInfo, null);
			return new ValueTuple<ExceptionDispatchInfo, bool>(exceptionDispatchInfo2 ?? exceptionDispatchInfo, exceptionDispatchInfo2 == null);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000786EB File Offset: 0x000768EB
		internal ExceptionDispatchInfo CheckDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				return this.CheckThrowDisposed(false, ref this.disposedInfo);
			}
			return null;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0007870D File Offset: 0x0007690D
		internal void ThrowIfDisposed()
		{
			this.ThrowIfDisposed(CancellationToken.None);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0007871A File Offset: 0x0007691A
		internal void ThrowIfDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.disposedInfo);
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0007873B File Offset: 0x0007693B
		internal void ThrowIfClosedOrDisposed()
		{
			this.ThrowIfClosedOrDisposed(CancellationToken.None);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00078748 File Offset: 0x00076948
		internal void ThrowIfClosedOrDisposed(CancellationToken cancellationToken)
		{
			if (this.Closed || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.closedInfo);
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0007876C File Offset: 0x0007696C
		private ExceptionDispatchInfo CheckThrowDisposed(bool throwIt, ref ExceptionDispatchInfo field)
		{
			ValueTuple<ExceptionDispatchInfo, bool> valueTuple = this.SetDisposed(ref field);
			ExceptionDispatchInfo item = valueTuple.Item1;
			if (valueTuple.Item2)
			{
				CancellationTokenSource cancellationTokenSource = this.cts;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
			}
			if (throwIt)
			{
				item.Throw();
			}
			return item;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000787AC File Offset: 0x000769AC
		internal void RegisterRequest(ServicePoint servicePoint, WebConnection connection)
		{
			if (servicePoint == null)
			{
				throw new ArgumentNullException("servicePoint");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			lock (this)
			{
				if (Interlocked.CompareExchange(ref this.requestSent, 1, 0) != 0)
				{
					throw new InvalidOperationException("Invalid nested call.");
				}
				this.ServicePoint = servicePoint;
				this.Connection = connection;
			}
			this.cts.Token.Register(delegate
			{
				this.Request.FinishedReading = true;
				this.SetDisposed(ref this.disposedInfo);
			});
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00078848 File Offset: 0x00076A48
		public void SetPriorityRequest(WebOperation operation)
		{
			lock (this)
			{
				if (this.requestSent != 1 || this.ServicePoint == null || this.finished != 0)
				{
					throw new InvalidOperationException("Should never happen.");
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.priorityRequest, operation, null) != null)
				{
					throw new InvalidOperationException("Invalid nested request.");
				}
			}
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000788BC File Offset: 0x00076ABC
		internal Task<WebRequestStream> GetRequestStreamInternal()
		{
			return this.requestTask.WaitForCompletion();
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x000788C9 File Offset: 0x00076AC9
		public WebRequestStream WriteStream
		{
			get
			{
				this.ThrowIfDisposed();
				return this.writeStream;
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000788D7 File Offset: 0x00076AD7
		public Task<WebResponseStream> GetResponseStream()
		{
			return this.responseTask.WaitForCompletion();
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x000788E4 File Offset: 0x00076AE4
		internal WebCompletionSource<ValueTuple<bool, WebOperation>> Finished
		{
			get
			{
				return this.finishedTask;
			}
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000788EC File Offset: 0x00076AEC
		internal async void Run()
		{
			try
			{
				this.ThrowIfClosedOrDisposed();
				WebRequestStream webRequestStream = await this.Connection.InitConnection(this, this.cts.Token).ConfigureAwait(false);
				WebRequestStream requestStream = webRequestStream;
				this.ThrowIfClosedOrDisposed();
				this.writeStream = requestStream;
				await requestStream.Initialize(this.cts.Token).ConfigureAwait(false);
				this.ThrowIfClosedOrDisposed();
				this.requestTask.TrySetCompleted(requestStream);
				WebResponseStream stream = new WebResponseStream(requestStream);
				this.responseStream = stream;
				await stream.InitReadAsync(this.cts.Token).ConfigureAwait(false);
				this.responseTask.TrySetCompleted(stream);
				requestStream = null;
				stream = null;
			}
			catch (OperationCanceledException)
			{
				this.SetCanceled();
			}
			catch (Exception ex)
			{
				this.SetError(ex);
			}
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00078923 File Offset: 0x00076B23
		internal void CompleteRequestWritten(WebRequestStream stream, Exception error = null)
		{
			if (error != null)
			{
				this.SetError(error);
				return;
			}
			this.requestWrittenTask.TrySetCompleted(stream);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00078940 File Offset: 0x00076B40
		internal void Finish(bool ok, Exception error = null)
		{
			if (Interlocked.CompareExchange(ref this.finished, 1, 0) != 0)
			{
				return;
			}
			WebResponseStream webResponseStream;
			WebOperation webOperation;
			lock (this)
			{
				webResponseStream = Interlocked.Exchange<WebResponseStream>(ref this.responseStream, null);
				webOperation = Interlocked.Exchange<WebOperation>(ref this.priorityRequest, null);
				this.Request.FinishedReading = true;
			}
			if (error != null)
			{
				if (webOperation != null)
				{
					webOperation.SetError(error);
				}
				this.finishedTask.TrySetException(error);
				return;
			}
			bool flag2 = !this.Aborted && ok && webResponseStream != null && webResponseStream.KeepAlive;
			if (webOperation != null && webOperation.Aborted)
			{
				webOperation = null;
				flag2 = false;
			}
			this.finishedTask.TrySetCompleted(new ValueTuple<bool, WebOperation>(flag2, webOperation));
		}

		// Token: 0x0400122F RID: 4655
		private CancellationTokenSource cts;

		// Token: 0x04001230 RID: 4656
		private WebCompletionSource<WebRequestStream> requestTask;

		// Token: 0x04001231 RID: 4657
		private WebCompletionSource<WebRequestStream> requestWrittenTask;

		// Token: 0x04001232 RID: 4658
		private WebCompletionSource<WebResponseStream> responseTask;

		// Token: 0x04001233 RID: 4659
		private WebCompletionSource<ValueTuple<bool, WebOperation>> finishedTask;

		// Token: 0x04001234 RID: 4660
		private WebRequestStream writeStream;

		// Token: 0x04001235 RID: 4661
		private WebResponseStream responseStream;

		// Token: 0x04001236 RID: 4662
		private ExceptionDispatchInfo disposedInfo;

		// Token: 0x04001237 RID: 4663
		private ExceptionDispatchInfo closedInfo;

		// Token: 0x04001238 RID: 4664
		private WebOperation priorityRequest;

		// Token: 0x04001239 RID: 4665
		private int requestSent;

		// Token: 0x0400123A RID: 4666
		private int finished;
	}
}
