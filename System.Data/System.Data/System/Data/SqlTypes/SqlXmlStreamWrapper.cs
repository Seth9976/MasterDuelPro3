using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x020000D5 RID: 213
	internal sealed class SqlXmlStreamWrapper : Stream
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x0003E51D File Offset: 0x0003C71D
		internal SqlXmlStreamWrapper(Stream stream)
		{
			this._stream = stream;
			this._lPosition = 0L;
			this._isClosed = false;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0003E53B File Offset: 0x0003C73B
		public override bool CanRead
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanRead;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0003E552 File Offset: 0x0003C752
		public override bool CanSeek
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanSeek;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0003E569 File Offset: 0x0003C769
		public override bool CanWrite
		{
			get
			{
				return !this.IsStreamClosed() && this._stream.CanWrite;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0003E580 File Offset: 0x0003C780
		public override long Length
		{
			get
			{
				this.ThrowIfStreamClosed("get_Length");
				this.ThrowIfStreamCannotSeek("get_Length");
				return this._stream.Length;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0003E5A3 File Offset: 0x0003C7A3
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0003E5C1 File Offset: 0x0003C7C1
		public override long Position
		{
			get
			{
				this.ThrowIfStreamClosed("get_Position");
				this.ThrowIfStreamCannotSeek("get_Position");
				return this._lPosition;
			}
			set
			{
				this.ThrowIfStreamClosed("set_Position");
				this.ThrowIfStreamCannotSeek("set_Position");
				if (value < 0L || value > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0003E600 File Offset: 0x0003C800
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfStreamClosed("Seek");
			this.ThrowIfStreamCannotSeek("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._stream.Length + offset;
				if (num < 0L || num > this._stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return this._lPosition;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0003E6DC File Offset: 0x0003C8DC
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Read");
			this.ThrowIfStreamCannotRead("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			int num = this._stream.Read(buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0003E78C File Offset: 0x0003C98C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Write");
			this.ThrowIfStreamCannotWrite("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			this._stream.Write(buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0003E83C File Offset: 0x0003CA3C
		public override int ReadByte()
		{
			this.ThrowIfStreamClosed("ReadByte");
			this.ThrowIfStreamCannotRead("ReadByte");
			if (this._stream.CanSeek && this._lPosition >= this._stream.Length)
			{
				return -1;
			}
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			int num = this._stream.ReadByte();
			this._lPosition += 1L;
			return num;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		public override void WriteByte(byte value)
		{
			this.ThrowIfStreamClosed("WriteByte");
			this.ThrowIfStreamCannotWrite("WriteByte");
			if (this._stream.CanSeek && this._stream.Position != this._lPosition)
			{
				this._stream.Seek(this._lPosition, SeekOrigin.Begin);
			}
			this._stream.WriteByte(value);
			this._lPosition += 1L;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0003E941 File Offset: 0x0003CB41
		public override void SetLength(long value)
		{
			this.ThrowIfStreamClosed("SetLength");
			this.ThrowIfStreamCannotSeek("SetLength");
			this._stream.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0003E975 File Offset: 0x0003CB75
		public override void Flush()
		{
			if (this._stream != null)
			{
				this._stream.Flush();
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0003E98C File Offset: 0x0003CB8C
		protected override void Dispose(bool disposing)
		{
			try
			{
				this._isClosed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003E9BC File Offset: 0x0003CBBC
		private void ThrowIfStreamCannotSeek(string method)
		{
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonSeekable(method));
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0003E9D7 File Offset: 0x0003CBD7
		private void ThrowIfStreamCannotRead(string method)
		{
			if (!this._stream.CanRead)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonReadable(method));
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003E9F2 File Offset: 0x0003CBF2
		private void ThrowIfStreamCannotWrite(string method)
		{
			if (!this._stream.CanWrite)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonWritable(method));
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003EA0D File Offset: 0x0003CC0D
		private void ThrowIfStreamClosed(string method)
		{
			if (this.IsStreamClosed())
			{
				throw new ObjectDisposedException(SQLResource.InvalidOpStreamClosed(method));
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0003EA23 File Offset: 0x0003CC23
		private bool IsStreamClosed()
		{
			return this._isClosed || this._stream == null || (!this._stream.CanRead && !this._stream.CanWrite && !this._stream.CanSeek);
		}

		// Token: 0x0400048C RID: 1164
		private Stream _stream;

		// Token: 0x0400048D RID: 1165
		private long _lPosition;

		// Token: 0x0400048E RID: 1166
		private bool _isClosed;
	}
}
