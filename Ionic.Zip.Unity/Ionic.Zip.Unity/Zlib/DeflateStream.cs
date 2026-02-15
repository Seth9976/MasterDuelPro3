using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x02000054 RID: 84
	public class DeflateStream : Stream
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00017DBE File Offset: 0x00015FBE
		public DeflateStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00017DCA File Offset: 0x00015FCA
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00017DD6 File Offset: 0x00015FD6
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00017DE2 File Offset: 0x00015FE2
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00017E06 File Offset: 0x00016006
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00017E13 File Offset: 0x00016013
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
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00017E34 File Offset: 0x00016034
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00017E44 File Offset: 0x00016044
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
					throw new ObjectDisposedException("DeflateStream");
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00017EB0 File Offset: 0x000160B0
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00017EBD File Offset: 0x000160BD
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00017EDE File Offset: 0x000160DE
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00017EF0 File Offset: 0x000160F0
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00017F04 File Offset: 0x00016104
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00017F50 File Offset: 0x00016150
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00003B06 File Offset: 0x00001D06
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00017F75 File Offset: 0x00016175
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00017F9A File Offset: 0x0001619A
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00017FBC File Offset: 0x000161BC
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00003AE5 File Offset: 0x00001CE5
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00018008 File Offset: 0x00016208
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001802B File Offset: 0x0001622B
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00018050 File Offset: 0x00016250
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00018098 File Offset: 0x00016298
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000180E0 File Offset: 0x000162E0
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00018124 File Offset: 0x00016324
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x040002B0 RID: 688
		internal ZlibBaseStream _baseStream;

		// Token: 0x040002B1 RID: 689
		internal Stream _innerStream;

		// Token: 0x040002B2 RID: 690
		private bool _disposed;
	}
}
