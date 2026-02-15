using System;

namespace System.IO.Compression
{
	// Token: 0x02000029 RID: 41
	internal sealed class WrappedStream : Stream
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00007A1F File Offset: 0x00005C1F
		internal WrappedStream(Stream baseStream, bool closeBaseStream)
			: this(baseStream, closeBaseStream, null, null)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007A2B File Offset: 0x00005C2B
		private WrappedStream(Stream baseStream, bool closeBaseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed)
		{
			this._baseStream = baseStream;
			this._closeBaseStream = closeBaseStream;
			this._onClosed = onClosed;
			this._zipArchiveEntry = entry;
			this._isDisposed = false;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007A57 File Offset: 0x00005C57
		internal WrappedStream(Stream baseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed)
			: this(baseStream, false, entry, onClosed)
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007A63 File Offset: 0x00005C63
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Length;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007A76 File Offset: 0x00005C76
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00007A89 File Offset: 0x00005C89
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfCantSeek();
				this._baseStream.Position = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007AA3 File Offset: 0x00005CA3
		public override bool CanRead
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanRead;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00007ABA File Offset: 0x00005CBA
		public override bool CanSeek
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanSeek;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00007AD1 File Offset: 0x00005CD1
		public override bool CanWrite
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanWrite;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007AE8 File Offset: 0x00005CE8
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007B08 File Offset: 0x00005D08
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007B1D File Offset: 0x00005D1D
		private void ThrowIfCantWrite()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007B32 File Offset: 0x00005D32
		private void ThrowIfCantSeek()
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007B47 File Offset: 0x00005D47
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007B63 File Offset: 0x00005D63
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			return this._baseStream.Seek(offset, origin);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007B7E File Offset: 0x00005D7E
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			this.ThrowIfCantWrite();
			this._baseStream.SetLength(value);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007B9E File Offset: 0x00005D9E
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007BBA File Offset: 0x00005DBA
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Flush();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007BD4 File Offset: 0x00005DD4
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				Action<ZipArchiveEntry> onClosed = this._onClosed;
				if (onClosed != null)
				{
					onClosed(this._zipArchiveEntry);
				}
				if (this._closeBaseStream)
				{
					this._baseStream.Dispose();
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400010C RID: 268
		private readonly Stream _baseStream;

		// Token: 0x0400010D RID: 269
		private readonly bool _closeBaseStream;

		// Token: 0x0400010E RID: 270
		private readonly Action<ZipArchiveEntry> _onClosed;

		// Token: 0x0400010F RID: 271
		private readonly ZipArchiveEntry _zipArchiveEntry;

		// Token: 0x04000110 RID: 272
		private bool _isDisposed;
	}
}
