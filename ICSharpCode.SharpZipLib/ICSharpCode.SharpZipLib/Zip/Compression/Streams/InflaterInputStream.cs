using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000067 RID: 103
	public class InflaterInputStream : Stream
	{
		// Token: 0x0600034A RID: 842 RVA: 0x00010E1E File Offset: 0x0000F01E
		public InflaterInputStream(Stream baseInputStream)
			: this(baseInputStream, new Inflater(), 4096)
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010E31 File Offset: 0x0000F031
		public InflaterInputStream(Stream baseInputStream, Inflater inf)
			: this(baseInputStream, inf, 4096)
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010E40 File Offset: 0x0000F040
		public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
		{
			if (baseInputStream == null)
			{
				throw new ArgumentNullException("baseInputStream");
			}
			if (inflater == null)
			{
				throw new ArgumentNullException("inflater");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseInputStream = baseInputStream;
			this.inf = inflater;
			this.inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x0600034F RID: 847 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public long Skip(long count)
		{
			if (count <= 0L)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.baseInputStream.CanSeek)
			{
				this.baseInputStream.Seek(count, SeekOrigin.Current);
				return count;
			}
			int num = 2048;
			if (count < (long)num)
			{
				num = (int)count;
			}
			byte[] array = new byte[num];
			int num2 = 1;
			long num3 = count;
			while (num3 > 0L && num2 > 0)
			{
				if (num3 < (long)num)
				{
					num = (int)num3;
				}
				num2 = this.baseInputStream.Read(array, 0, num);
				num3 -= (long)num2;
			}
			return count - num3;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010F31 File Offset: 0x0000F131
		protected void StopDecrypting()
		{
			this.inputBuffer.CryptoTransform = null;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00010F3F File Offset: 0x0000F13F
		public virtual int Available
		{
			get
			{
				if (!this.inf.IsFinished)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010F54 File Offset: 0x0000F154
		protected void Fill()
		{
			if (this.inputBuffer.Available <= 0)
			{
				this.inputBuffer.Fill();
				if (this.inputBuffer.Available <= 0)
				{
					throw new SharpZipBaseException("Unexpected EOF");
				}
			}
			this.inputBuffer.SetInflaterInput(this.inf);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00010FB1 File Offset: 0x0000F1B1
		public override long Length
		{
			get
			{
				throw new NotSupportedException("InflaterInputStream Length is not supported");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00010FBD File Offset: 0x0000F1BD
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00010FCA File Offset: 0x0000F1CA
		public override long Position
		{
			get
			{
				return this.baseInputStream.Position;
			}
			set
			{
				throw new NotSupportedException("InflaterInputStream Position not supported");
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00010FE3 File Offset: 0x0000F1E3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00010FEF File Offset: 0x0000F1EF
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00010FFB File Offset: 0x0000F1FB
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011007 File Offset: 0x0000F207
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011013 File Offset: 0x0000F213
		protected override void Dispose(bool disposing)
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				if (this.IsStreamOwner)
				{
					this.baseInputStream.Dispose();
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011038 File Offset: 0x0000F238
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.inf.IsNeedingDictionary)
			{
				throw new SharpZipBaseException("Need a dictionary");
			}
			int num = count;
			for (;;)
			{
				int num2 = this.inf.Inflate(buffer, offset, num);
				offset += num2;
				num -= num2;
				if (num == 0 || this.inf.IsFinished)
				{
					goto IL_0065;
				}
				if (this.inf.IsNeedingInput)
				{
					this.Fill();
				}
				else if (num2 == 0)
				{
					break;
				}
			}
			throw new ZipException("Invalid input data");
			IL_0065:
			return count - num;
		}

		// Token: 0x04000273 RID: 627
		protected Inflater inf;

		// Token: 0x04000274 RID: 628
		protected InflaterInputBuffer inputBuffer;

		// Token: 0x04000275 RID: 629
		private Stream baseInputStream;

		// Token: 0x04000276 RID: 630
		protected long csize;

		// Token: 0x04000277 RID: 631
		private bool isClosed;
	}
}
