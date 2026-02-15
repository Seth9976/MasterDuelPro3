using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200043E RID: 1086
	internal abstract class WebConnectionStream : Stream
	{
		// Token: 0x06001B69 RID: 7017 RVA: 0x00077754 File Offset: 0x00075954
		protected WebConnectionStream(WebConnection cnc, WebOperation operation)
		{
			this.Connection = cnc;
			this.Operation = operation;
			this.Request = operation.Request;
			this.read_timeout = this.Request.ReadWriteTimeout;
			this.write_timeout = this.read_timeout;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000777A9 File Offset: 0x000759A9
		internal HttpWebRequest Request { get; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000777B1 File Offset: 0x000759B1
		internal WebConnection Connection { get; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x000777B9 File Offset: 0x000759B9
		internal WebOperation Operation { get; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x000777C1 File Offset: 0x000759C1
		internal ServicePoint ServicePoint
		{
			get
			{
				return this.Connection.ServicePoint;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x000777CE File Offset: 0x000759CE
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x000777D6 File Offset: 0x000759D6
		public override int ReadTimeout
		{
			get
			{
				return this.read_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.read_timeout = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000777EE File Offset: 0x000759EE
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x000777F6 File Offset: 0x000759F6
		public override int WriteTimeout
		{
			get
			{
				return this.write_timeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.write_timeout = value;
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0007780E File Offset: 0x00075A0E
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

		// Token: 0x06001B74 RID: 7028
		protected abstract bool TryReadFromBufferedContent(byte[] buffer, int offset, int count, out int result);

		// Token: 0x06001B75 RID: 7029 RVA: 0x00077848 File Offset: 0x00075A48
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num2;
			if (this.TryReadFromBufferedContent(buffer, offset, count, out num2))
			{
				return num2;
			}
			this.Operation.ThrowIfClosedOrDisposed();
			int result;
			try
			{
				result = this.ReadAsync(buffer, offset, count, CancellationToken.None).Result;
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
			return result;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000778EC File Offset: 0x00075AEC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
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
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00077968 File Offset: 0x00075B68
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

		// Token: 0x06001B78 RID: 7032 RVA: 0x000779A8 File Offset: 0x00075BA8
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cb, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), cb, state);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00077A24 File Offset: 0x00075C24
		public override void EndWrite(IAsyncResult r)
		{
			if (r == null)
			{
				throw new ArgumentNullException("r");
			}
			try
			{
				TaskToApm.End(r);
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00077A64 File Offset: 0x00075C64
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = buffer.Length;
			if (offset < 0 || num < offset)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || num - offset < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing.");
			}
			this.Operation.ThrowIfClosedOrDisposed();
			try
			{
				base.WriteAsync(buffer, offset, count).Wait();
			}
			catch (Exception ex)
			{
				throw this.GetException(ex);
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void Flush()
		{
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00077AF4 File Offset: 0x00075CF4
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00077B0B File Offset: 0x00075D0B
		internal void InternalClose()
		{
			this.disposed = true;
		}

		// Token: 0x06001B7E RID: 7038
		protected abstract void Close_internal(ref bool disposed);

		// Token: 0x06001B7F RID: 7039 RVA: 0x00077B14 File Offset: 0x00075D14
		public override void Close()
		{
			this.Close_internal(ref this.disposed);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00077B22 File Offset: 0x00075D22
		public override long Seek(long a, SeekOrigin b)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00077B22 File Offset: 0x00075D22
		public override void SetLength(long a)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x00077B22 File Offset: 0x00075D22
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x00077B22 File Offset: 0x00075D22
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x00077B22 File Offset: 0x00075D22
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
			set
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		// Token: 0x04001200 RID: 4608
		protected bool closed;

		// Token: 0x04001201 RID: 4609
		private bool disposed;

		// Token: 0x04001202 RID: 4610
		private object locker = new object();

		// Token: 0x04001203 RID: 4611
		private int read_timeout;

		// Token: 0x04001204 RID: 4612
		private int write_timeout;
	}
}
