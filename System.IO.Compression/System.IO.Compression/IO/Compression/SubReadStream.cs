using System;

namespace System.IO.Compression
{
	// Token: 0x0200002A RID: 42
	internal sealed class SubReadStream : Stream
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00007C24 File Offset: 0x00005E24
		public SubReadStream(Stream superStream, long startPosition, long maxLength)
		{
			this._startInSuperStream = startPosition;
			this._positionInSuperStream = startPosition;
			this._endInSuperStream = startPosition + maxLength;
			this._superStream = superStream;
			this._canRead = true;
			this._isDisposed = false;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007C58 File Offset: 0x00005E58
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._endInSuperStream - this._startInSuperStream;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007C6D File Offset: 0x00005E6D
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007C82 File Offset: 0x00005E82
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._positionInSuperStream - this._startInSuperStream;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007C94 File Offset: 0x00005E94
		public override bool CanRead
		{
			get
			{
				return this._superStream.CanRead && this._canRead;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00002273 File Offset: 0x00000473
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00002273 File Offset: 0x00000473
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007CAB File Offset: 0x00005EAB
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007B08 File Offset: 0x00005D08
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007CCC File Offset: 0x00005ECC
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			if (this._superStream.Position != this._positionInSuperStream)
			{
				this._superStream.Seek(this._positionInSuperStream, SeekOrigin.Begin);
			}
			if (this._positionInSuperStream + (long)count > this._endInSuperStream)
			{
				count = (int)(this._endInSuperStream - this._positionInSuperStream);
			}
			int num = this._superStream.Read(buffer, offset, count);
			this._positionInSuperStream += (long)num;
			return num;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007C82 File Offset: 0x00005E82
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007D4B File Offset: 0x00005F4B
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007D5D File Offset: 0x00005F5D
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007D5D File Offset: 0x00005F5D
		public override void Flush()
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007D6F File Offset: 0x00005F6F
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				this._canRead = false;
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000111 RID: 273
		private readonly long _startInSuperStream;

		// Token: 0x04000112 RID: 274
		private long _positionInSuperStream;

		// Token: 0x04000113 RID: 275
		private readonly long _endInSuperStream;

		// Token: 0x04000114 RID: 276
		private readonly Stream _superStream;

		// Token: 0x04000115 RID: 277
		private bool _canRead;

		// Token: 0x04000116 RID: 278
		private bool _isDisposed;
	}
}
