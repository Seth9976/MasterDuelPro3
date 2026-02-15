using System;
using System.IO;

namespace Better.StreamingAssets
{
	// Token: 0x02000009 RID: 9
	internal class SubReadOnlyStream : Stream
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000273B File Offset: 0x0000093B
		public SubReadOnlyStream(Stream actualStream, bool leaveOpen = false)
		{
			if (actualStream == null)
			{
				throw new ArgumentNullException("superStream");
			}
			this.m_actualStream = actualStream;
			this.m_leaveOpen = leaveOpen;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002760 File Offset: 0x00000960
		public SubReadOnlyStream(Stream actualStream, long offset, long length, bool leaveOpen = false)
			: this(actualStream, leaveOpen)
		{
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.m_offset = offset;
			this.m_position = offset;
			this.m_length = new long?(length);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000027B0 File Offset: 0x000009B0
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_length == null)
				{
					this.m_length = new long?(this.m_actualStream.Length - this.m_offset);
				}
				return this.m_length.Value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027ED File Offset: 0x000009ED
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002802 File Offset: 0x00000A02
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this.m_position - this.m_offset;
			}
			set
			{
				this.ThrowIfDisposed();
				this.m_position = this.m_offset + value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002818 File Offset: 0x00000A18
		public override bool CanRead
		{
			get
			{
				return this.m_actualStream.CanRead;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002825 File Offset: 0x00000A25
		public override bool CanSeek
		{
			get
			{
				return this.m_actualStream.CanSeek;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002832 File Offset: 0x00000A32
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002838 File Offset: 0x00000A38
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfCantRead();
			this.ThrowIfDisposed();
			if (this.m_actualStream.Position != this.m_position)
			{
				this.m_actualStream.Seek(this.m_position, SeekOrigin.Begin);
			}
			if (this.m_length != null)
			{
				long endPosition = this.m_offset + this.m_length.Value;
				if (this.m_position + (long)count > endPosition)
				{
					count = (int)(endPosition - this.m_position);
				}
			}
			int bytesRead = this.m_actualStream.Read(buffer, offset, count);
			this.m_position += (long)bytesRead;
			return bytesRead;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028D0 File Offset: 0x00000AD0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			if (origin == SeekOrigin.Begin)
			{
				this.m_position = this.m_actualStream.Seek(this.m_offset + offset, SeekOrigin.Begin);
			}
			else if (origin == SeekOrigin.End)
			{
				this.m_position = this.m_actualStream.Seek(this.m_offset + this.Length + offset, SeekOrigin.Begin);
			}
			else
			{
				this.m_position = this.m_actualStream.Seek(offset, SeekOrigin.Current);
			}
			return this.m_position - this.m_offset;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002949 File Offset: 0x00000B49
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002949 File Offset: 0x00000B49
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002949 File Offset: 0x00000B49
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002950 File Offset: 0x00000B50
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.m_actualStream != null)
			{
				if (!this.m_leaveOpen)
				{
					this.m_actualStream.Dispose();
				}
				this.m_actualStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000297E File Offset: 0x00000B7E
		private void ThrowIfDisposed()
		{
			if (this.m_actualStream == null)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "");
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000299E File Offset: 0x00000B9E
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000011 RID: 17
		private readonly long m_offset;

		// Token: 0x04000012 RID: 18
		private readonly bool m_leaveOpen;

		// Token: 0x04000013 RID: 19
		private long? m_length;

		// Token: 0x04000014 RID: 20
		private Stream m_actualStream;

		// Token: 0x04000015 RID: 21
		private long m_position;
	}
}
