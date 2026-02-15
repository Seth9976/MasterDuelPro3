using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x0200006F RID: 111
	public class ZlibStream : Stream
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x0001E14E File Offset: 0x0001C34E
		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001E15A File Offset: 0x0001C35A
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001E166 File Offset: 0x0001C366
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001E172 File Offset: 0x0001C372
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001E18F File Offset: 0x0001C38F
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0001E19C File Offset: 0x0001C39C
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001E1BD File Offset: 0x0001C3BD
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001E1CC File Offset: 0x0001C3CC
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001E238 File Offset: 0x0001C438
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001E24A File Offset: 0x0001C44A
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001E25C File Offset: 0x0001C45C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00003B06 File Offset: 0x00001D06
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001E2CD File Offset: 0x0001C4CD
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001E2F2 File Offset: 0x0001C4F2
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x000052C3 File Offset: 0x000034C3
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0001E314 File Offset: 0x0001C514
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000052C3 File Offset: 0x000034C3
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001E360 File Offset: 0x0001C560
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000052C3 File Offset: 0x000034C3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000052C3 File Offset: 0x000034C3
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001E383 File Offset: 0x0001C583
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001E3A8 File Offset: 0x0001C5A8
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001E3F0 File Offset: 0x0001C5F0
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001E438 File Offset: 0x0001C638
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001E47C File Offset: 0x0001C67C
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x040003D4 RID: 980
		internal ZlibBaseStream _baseStream;

		// Token: 0x040003D5 RID: 981
		private bool _disposed;
	}
}
