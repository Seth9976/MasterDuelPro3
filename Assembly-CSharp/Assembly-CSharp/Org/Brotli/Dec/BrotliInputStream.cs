using System;
using System.IO;

namespace Org.Brotli.Dec
{
	// Token: 0x02000075 RID: 117
	public class BrotliInputStream : Stream
	{
		// Token: 0x06000236 RID: 566 RVA: 0x000073C3 File Offset: 0x000055C3
		public BrotliInputStream(Stream source)
			: this(source, 16384, null)
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000073D2 File Offset: 0x000055D2
		public BrotliInputStream(Stream source, int byteReadBufferSize)
			: this(source, byteReadBufferSize, null)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000073E0 File Offset: 0x000055E0
		public BrotliInputStream(Stream source, int byteReadBufferSize, byte[] customDictionary)
		{
			if (byteReadBufferSize <= 0)
			{
				throw new ArgumentException("Bad buffer size:" + byteReadBufferSize.ToString());
			}
			if (source == null)
			{
				throw new ArgumentException("source is null");
			}
			this.buffer = new byte[byteReadBufferSize];
			this.remainingBufferBytes = 0;
			this.bufferOffset = 0;
			try
			{
				State.SetInput(this.state, source);
			}
			catch (BrotliRuntimeException ex)
			{
				throw new IOException("Brotli decoder initialization failed", ex);
			}
			if (customDictionary != null)
			{
				Decode.SetCustomDictionary(this.state, customDictionary);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000747C File Offset: 0x0000567C
		public override void Close()
		{
			State.Close(this.state);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000748C File Offset: 0x0000568C
		public override int ReadByte()
		{
			if (this.bufferOffset >= this.remainingBufferBytes)
			{
				this.remainingBufferBytes = this.Read(this.buffer, 0, this.buffer.Length);
				this.bufferOffset = 0;
				if (this.remainingBufferBytes == -1)
				{
					return -1;
				}
			}
			byte[] array = this.buffer;
			int num = this.bufferOffset;
			this.bufferOffset = num + 1;
			return array[num] & 255;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000074F4 File Offset: 0x000056F4
		public override int Read(byte[] destBuffer, int destOffset, int destLen)
		{
			if (destOffset < 0)
			{
				throw new ArgumentException("Bad offset: " + destOffset.ToString());
			}
			if (destLen < 0)
			{
				throw new ArgumentException("Bad length: " + destLen.ToString());
			}
			int num;
			if (destOffset + destLen > destBuffer.Length)
			{
				string text = "Buffer overflow: ";
				num = destOffset + destLen;
				string text2 = num.ToString();
				string text3 = " > ";
				num = destBuffer.Length;
				throw new ArgumentException(text + text2 + text3 + num.ToString());
			}
			if (destLen == 0)
			{
				return 0;
			}
			int copyLen = Math.Max(this.remainingBufferBytes - this.bufferOffset, 0);
			if (copyLen != 0)
			{
				copyLen = Math.Min(copyLen, destLen);
				Array.Copy(this.buffer, this.bufferOffset, destBuffer, destOffset, copyLen);
				this.bufferOffset += copyLen;
				destOffset += copyLen;
				destLen -= copyLen;
				if (destLen == 0)
				{
					return copyLen;
				}
			}
			try
			{
				this.state.output = destBuffer;
				this.state.outputOffset = destOffset;
				this.state.outputLength = destLen;
				this.state.outputUsed = 0;
				Decode.Decompress(this.state);
				if (this.state.outputUsed == 0)
				{
					num = 0;
				}
				else
				{
					num = this.state.outputUsed + copyLen;
				}
			}
			catch (BrotliRuntimeException ex)
			{
				throw new IOException("Brotli stream decoding failed", ex);
			}
			return num;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000055AE File Offset: 0x000037AE
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000055AE File Offset: 0x000037AE
		// (set) Token: 0x06000240 RID: 576 RVA: 0x000055AE File Offset: 0x000037AE
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

		// Token: 0x06000241 RID: 577 RVA: 0x000055AE File Offset: 0x000037AE
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000055AE File Offset: 0x000037AE
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000055AE File Offset: 0x000037AE
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000055AE File Offset: 0x000037AE
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Flush()
		{
		}

		// Token: 0x040002CE RID: 718
		public const int DefaultInternalBufferSize = 16384;

		// Token: 0x040002CF RID: 719
		private byte[] buffer;

		// Token: 0x040002D0 RID: 720
		private int remainingBufferBytes;

		// Token: 0x040002D1 RID: 721
		private int bufferOffset;

		// Token: 0x040002D2 RID: 722
		private readonly State state = new State();
	}
}
