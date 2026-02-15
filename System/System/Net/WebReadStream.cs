using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000445 RID: 1093
	internal abstract class WebReadStream : Stream
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x00078CD2 File Offset: 0x00076ED2
		public WebOperation Operation { get; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00078CDA File Offset: 0x00076EDA
		protected Stream InnerStream { get; }

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00078CE2 File Offset: 0x00076EE2
		public WebReadStream(WebOperation operation, Stream innerStream)
		{
			this.Operation = operation;
			this.InnerStream = innerStream;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00003132 File Offset: 0x00001332
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x06001BC5 RID: 7109 RVA: 0x00003132 File Offset: 0x00001332
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00003132 File Offset: 0x00001332
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00003132 File Offset: 0x00001332
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00003132 File Offset: 0x00001332
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00003132 File Offset: 0x00001332
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00078CF8 File Offset: 0x00076EF8
		protected Exception GetException(Exception e)
		{
			e = HttpWebRequest.FlattenException(e);
			if (e is WebException)
			{
				return e;
			}
			if (this.Operation.Aborted || e is OperationCanceledException || e is ObjectDisposedException)
			{
				return HttpWebRequest.CreateRequestAbortedException();
			}
			return e;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00078D30 File Offset: 0x00076F30
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int result;
			try
			{
				result = this.ReadAsync(buffer, offset, size, CancellationToken.None).Result;
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return result;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00078DC8 File Offset: 0x00076FC8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, size, CancellationToken.None), cb, state);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00078E44 File Offset: 0x00077044
		public override int EndRead(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			int num;
			try
			{
				num = TaskToApm.End<int>(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return num;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00078E84 File Offset: 0x00077084
		public sealed override async Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			this.Operation.ThrowIfDisposed(cancellationToken);
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || num - offset < size)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int num3;
			try
			{
				int num2 = await this.ProcessReadAsync(buffer, offset, size, cancellationToken).ConfigureAwait(false);
				if (num2 != 0)
				{
					num3 = num2;
				}
				else
				{
					await this.FinishReading(cancellationToken).ConfigureAwait(false);
					num3 = 0;
				}
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
			}
			return num3;
		}

		// Token: 0x06001BD2 RID: 7122
		protected abstract Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken);

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00078EE8 File Offset: 0x000770E8
		internal virtual Task FinishReading(CancellationToken cancellationToken)
		{
			this.Operation.ThrowIfDisposed(cancellationToken);
			WebReadStream webReadStream = this.InnerStream as WebReadStream;
			if (webReadStream != null)
			{
				return webReadStream.FinishReading(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00078F1D File Offset: 0x0007711D
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				this.disposed = true;
				if (this.InnerStream != null)
				{
					this.InnerStream.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001244 RID: 4676
		private bool disposed;
	}
}
