using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	// Token: 0x0200001A RID: 26
	internal sealed class PositionPreservingWriteOnlyStreamWrapper : Stream
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00004F44 File Offset: 0x00003144
		public PositionPreservingWriteOnlyStreamWrapper(Stream stream)
		{
			this._stream = stream;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002273 File Offset: 0x00000473
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002273 File Offset: 0x00000473
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004F53 File Offset: 0x00003153
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004F56 File Offset: 0x00003156
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002276 File Offset: 0x00000476
		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004F5E File Offset: 0x0000315E
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._position += (long)count;
			this._stream.Write(buffer, offset, count);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004F7D File Offset: 0x0000317D
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this._position += (long)count;
			return this._stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004FA0 File Offset: 0x000031A0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._stream.EndWrite(asyncResult);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004FAE File Offset: 0x000031AE
		public override void WriteByte(byte value)
		{
			this._position += 1L;
			this._stream.WriteByte(value);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004FCB File Offset: 0x000031CB
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this._position += (long)count;
			return this._stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004FEC File Offset: 0x000031EC
		public override bool CanTimeout
		{
			get
			{
				return this._stream.CanTimeout;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004FF9 File Offset: 0x000031F9
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00005006 File Offset: 0x00003206
		public override int ReadTimeout
		{
			get
			{
				return this._stream.ReadTimeout;
			}
			set
			{
				this._stream.ReadTimeout = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005014 File Offset: 0x00003214
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00005021 File Offset: 0x00003221
		public override int WriteTimeout
		{
			get
			{
				return this._stream.WriteTimeout;
			}
			set
			{
				this._stream.WriteTimeout = value;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000502F File Offset: 0x0000322F
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000503C File Offset: 0x0000323C
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._stream.FlushAsync(cancellationToken);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000504A File Offset: 0x0000324A
		public override void Close()
		{
			this._stream.Close();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005057 File Offset: 0x00003257
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._stream.Dispose();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002276 File Offset: 0x00000476
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002276 File Offset: 0x00000476
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002276 File Offset: 0x00000476
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002276 File Offset: 0x00000476
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x04000097 RID: 151
		private readonly Stream _stream;

		// Token: 0x04000098 RID: 152
		private long _position;
	}
}
