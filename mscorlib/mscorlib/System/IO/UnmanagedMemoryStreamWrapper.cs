using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020007B5 RID: 1973
	internal sealed class UnmanagedMemoryStreamWrapper : MemoryStream
	{
		// Token: 0x06003EBF RID: 16063 RVA: 0x000F2110 File Offset: 0x000F0310
		internal UnmanagedMemoryStreamWrapper(UnmanagedMemoryStream stream)
		{
			this._unmanagedStream = stream;
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x000F211F File Offset: 0x000F031F
		public override bool CanRead
		{
			get
			{
				return this._unmanagedStream.CanRead;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06003EC1 RID: 16065 RVA: 0x000F212C File Offset: 0x000F032C
		public override bool CanSeek
		{
			get
			{
				return this._unmanagedStream.CanSeek;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000F2139 File Offset: 0x000F0339
		public override bool CanWrite
		{
			get
			{
				return this._unmanagedStream.CanWrite;
			}
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x000F2148 File Offset: 0x000F0348
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._unmanagedStream.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x000F2180 File Offset: 0x000F0380
		public override void Flush()
		{
			this._unmanagedStream.Flush();
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x000F218D File Offset: 0x000F038D
		public override byte[] GetBuffer()
		{
			throw new UnauthorizedAccessException("MemoryStream's internal buffer cannot be accessed.");
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000F2199 File Offset: 0x000F0399
		// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x000F21A7 File Offset: 0x000F03A7
		public override int Capacity
		{
			get
			{
				return (int)this._unmanagedStream.Capacity;
			}
			set
			{
				throw new IOException("Unable to expand length of this stream beyond its capacity.");
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x000F21B3 File Offset: 0x000F03B3
		public override long Length
		{
			get
			{
				return this._unmanagedStream.Length;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000F21C0 File Offset: 0x000F03C0
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x000F21CD File Offset: 0x000F03CD
		public override long Position
		{
			get
			{
				return this._unmanagedStream.Position;
			}
			set
			{
				this._unmanagedStream.Position = value;
			}
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x000F21DB File Offset: 0x000F03DB
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._unmanagedStream.Read(buffer, offset, count);
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x000F21EB File Offset: 0x000F03EB
		public override int Read(Span<byte> buffer)
		{
			return this._unmanagedStream.Read(buffer);
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000F21F9 File Offset: 0x000F03F9
		public override int ReadByte()
		{
			return this._unmanagedStream.ReadByte();
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000F2206 File Offset: 0x000F0406
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this._unmanagedStream.Seek(offset, loc);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000F2218 File Offset: 0x000F0418
		public override byte[] ToArray()
		{
			byte[] array = new byte[this._unmanagedStream.Length];
			this._unmanagedStream.Read(array, 0, (int)this._unmanagedStream.Length);
			return array;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000F2252 File Offset: 0x000F0452
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._unmanagedStream.Write(buffer, offset, count);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000F2262 File Offset: 0x000F0462
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this._unmanagedStream.Write(buffer);
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000F2270 File Offset: 0x000F0470
		public override void WriteByte(byte value)
		{
			this._unmanagedStream.WriteByte(value);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000F227E File Offset: 0x000F047E
		public override void SetLength(long value)
		{
			base.SetLength(value);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000F2288 File Offset: 0x000F0488
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", "Cannot access a closed Stream.");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
			return this._unmanagedStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000F2327 File Offset: 0x000F0527
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._unmanagedStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000F2335 File Offset: 0x000F0535
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000F2347 File Offset: 0x000F0547
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.ReadAsync(buffer, cancellationToken);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000F2356 File Offset: 0x000F0556
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000F2368 File Offset: 0x000F0568
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.WriteAsync(buffer, cancellationToken);
		}

		// Token: 0x04002024 RID: 8228
		private UnmanagedMemoryStream _unmanagedStream;
	}
}
