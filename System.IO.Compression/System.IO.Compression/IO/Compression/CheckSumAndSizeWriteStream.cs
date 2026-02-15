using System;

namespace System.IO.Compression
{
	// Token: 0x0200002B RID: 43
	internal sealed class CheckSumAndSizeWriteStream : Stream
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00007D94 File Offset: 0x00005F94
		public CheckSumAndSizeWriteStream(Stream baseStream, Stream baseBaseStream, bool leaveOpenOnClose, ZipArchiveEntry entry, EventHandler onClose, Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> saveCrcAndSizes)
		{
			this._baseStream = baseStream;
			this._baseBaseStream = baseBaseStream;
			this._position = 0L;
			this._checksum = 0U;
			this._leaveOpenOnClose = leaveOpenOnClose;
			this._canWrite = true;
			this._isDisposed = false;
			this._initialPosition = 0L;
			this._zipArchiveEntry = entry;
			this._onClose = onClose;
			this._saveCrcAndSizes = saveCrcAndSizes;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00007DF9 File Offset: 0x00005FF9
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007E0B File Offset: 0x0000600B
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00007DF9 File Offset: 0x00005FF9
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._position;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002273 File Offset: 0x00000473
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002273 File Offset: 0x00000473
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007E19 File Offset: 0x00006019
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007E21 File Offset: 0x00006021
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007E41 File Offset: 0x00006041
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007DF9 File Offset: 0x00005FF9
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007E53 File Offset: 0x00006053
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007E68 File Offset: 0x00006068
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "The argument must be non-negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "The argument must be non-negative.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("The offset and length parameters are not valid for the array that was given.");
			}
			this.ThrowIfDisposed();
			if (count == 0)
			{
				return;
			}
			if (!this._everWritten)
			{
				this._initialPosition = this._baseBaseStream.Position;
				this._everWritten = true;
			}
			this._checksum = Crc32Helper.UpdateCrc32(this._checksum, buffer, offset, count);
			this._baseStream.Write(buffer, offset, count);
			this._position += (long)count;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007F19 File Offset: 0x00006119
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this._baseStream.Flush();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007F2C File Offset: 0x0000612C
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				if (!this._everWritten)
				{
					this._initialPosition = this._baseBaseStream.Position;
				}
				if (!this._leaveOpenOnClose)
				{
					this._baseStream.Dispose();
				}
				Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> saveCrcAndSizes = this._saveCrcAndSizes;
				if (saveCrcAndSizes != null)
				{
					saveCrcAndSizes(this._initialPosition, this.Position, this._checksum, this._baseBaseStream, this._zipArchiveEntry, this._onClose);
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000117 RID: 279
		private readonly Stream _baseStream;

		// Token: 0x04000118 RID: 280
		private readonly Stream _baseBaseStream;

		// Token: 0x04000119 RID: 281
		private long _position;

		// Token: 0x0400011A RID: 282
		private uint _checksum;

		// Token: 0x0400011B RID: 283
		private readonly bool _leaveOpenOnClose;

		// Token: 0x0400011C RID: 284
		private bool _canWrite;

		// Token: 0x0400011D RID: 285
		private bool _isDisposed;

		// Token: 0x0400011E RID: 286
		private bool _everWritten;

		// Token: 0x0400011F RID: 287
		private long _initialPosition;

		// Token: 0x04000120 RID: 288
		private readonly ZipArchiveEntry _zipArchiveEntry;

		// Token: 0x04000121 RID: 289
		private readonly EventHandler _onClose;

		// Token: 0x04000122 RID: 290
		private readonly Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> _saveCrcAndSizes;
	}
}
