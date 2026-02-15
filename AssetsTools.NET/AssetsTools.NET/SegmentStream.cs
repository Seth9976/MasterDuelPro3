using System;
using System.IO;

namespace AssetsTools.NET
{
	// Token: 0x02000073 RID: 115
	public class SegmentStream : Stream
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x000179D9 File Offset: 0x00015BD9
		public SegmentStream(Stream baseStream, long baseOffset)
			: this(baseStream, baseOffset, -1L)
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000179E8 File Offset: 0x00015BE8
		public SegmentStream(Stream baseStream, long baseOffset, long length)
		{
			bool flag = baseOffset < 0L || baseOffset > baseStream.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("baseOffset");
			}
			bool flag2 = length >= 0L && baseOffset + length > baseStream.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.BaseStream = baseStream;
			this.BaseOffset = baseOffset;
			this.length = length;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00017A54 File Offset: 0x00015C54
		public Stream BaseStream { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00017A5C File Offset: 0x00015C5C
		public long BaseOffset { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00017A64 File Offset: 0x00015C64
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00017A6C File Offset: 0x00015C6C
		public override long Position { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00017A78 File Offset: 0x00015C78
		public override long Length
		{
			get
			{
				return (this.length >= 0L) ? this.length : (this.BaseStream.Length - this.BaseOffset);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00017AAE File Offset: 0x00015CAE
		public override void Flush()
		{
			this.BaseStream.Flush();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00017AC0 File Offset: 0x00015CC0
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.BaseStream.Position = this.BaseOffset + this.Position;
			count = this.BaseStream.Read(buffer, offset, (int)Math.Min((long)count, this.Length - this.Position));
			this.Position += (long)count;
			return count;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00017B20 File Offset: 0x00015D20
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Position + this.Length + offset;
				break;
			default:
				throw new ArgumentException();
			}
			bool flag = num < 0L || num > this.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this.Position = num;
			return this.Position;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00017925 File Offset: 0x00015B25
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00017BA4 File Offset: 0x00015DA4
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = this.length >= 0L && (long)count > this.Length - this.Position;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.BaseStream.Position = this.BaseOffset + this.Position;
			this.BaseStream.Write(buffer, offset, count);
			this.Position += (long)count;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00017C17 File Offset: 0x00015E17
		public override bool CanRead
		{
			get
			{
				return this.BaseStream.CanRead;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00017C24 File Offset: 0x00015E24
		public override bool CanSeek
		{
			get
			{
				return this.BaseStream.CanSeek;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00017C31 File Offset: 0x00015E31
		public override bool CanWrite
		{
			get
			{
				return this.BaseStream.CanWrite;
			}
		}

		// Token: 0x04000273 RID: 627
		private readonly long length;
	}
}
