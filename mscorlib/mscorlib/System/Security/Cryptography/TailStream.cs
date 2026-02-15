using System;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x0200039D RID: 925
	internal sealed class TailStream : Stream
	{
		// Token: 0x06001FB4 RID: 8116 RVA: 0x0007D65F File Offset: 0x0007B85F
		public TailStream(int bufferSize)
		{
			this._Buffer = new byte[bufferSize];
			this._BufferSize = bufferSize;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0007A955 File Offset: 0x00078B55
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0007D67C File Offset: 0x0007B87C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._Buffer != null)
					{
						Array.Clear(this._Buffer, 0, this._Buffer.Length);
					}
					this._Buffer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0007D6CC File Offset: 0x0007B8CC
		public byte[] Buffer
		{
			get
			{
				return (byte[])this._Buffer.Clone();
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0007D6DE File Offset: 0x0007B8DE
		public override bool CanWrite
		{
			get
			{
				return this._Buffer != null;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0007D6E9 File Offset: 0x0007B8E9
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0007D6E9 File Offset: 0x0007B8E9
		// (set) Token: 0x06001FBD RID: 8125 RVA: 0x0007D6E9 File Offset: 0x0007B8E9
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00002C89 File Offset: 0x00000E89
		public override void Flush()
		{
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0007D6E9 File Offset: 0x0007B8E9
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0007D6E9 File Offset: 0x0007B8E9
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0007D6FA File Offset: 0x0007B8FA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0007D70C File Offset: 0x0007B90C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._Buffer == null)
			{
				throw new ObjectDisposedException("TailStream");
			}
			if (count == 0)
			{
				return;
			}
			if (this._BufferFull)
			{
				if (count > this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					return;
				}
				global::System.Buffer.InternalBlockCopy(this._Buffer, this._BufferSize - count, this._Buffer, 0, this._BufferSize - count);
				global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferSize - count, count);
				return;
			}
			else
			{
				if (count > this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					this._BufferFull = true;
					return;
				}
				if (count + this._BufferIndex >= this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(this._Buffer, this._BufferIndex + count - this._BufferSize, this._Buffer, 0, this._BufferSize - count);
					global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
					this._BufferFull = true;
					return;
				}
				global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
				this._BufferIndex += count;
				return;
			}
		}

		// Token: 0x04000EE6 RID: 3814
		private byte[] _Buffer;

		// Token: 0x04000EE7 RID: 3815
		private int _BufferSize;

		// Token: 0x04000EE8 RID: 3816
		private int _BufferIndex;

		// Token: 0x04000EE9 RID: 3817
		private bool _BufferFull;
	}
}
