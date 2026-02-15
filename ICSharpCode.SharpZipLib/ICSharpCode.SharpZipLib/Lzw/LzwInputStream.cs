using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x0200008D RID: 141
	public class LzwInputStream : Stream
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000175F2 File Offset: 0x000157F2
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x000175FA File Offset: 0x000157FA
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x060004BF RID: 1215 RVA: 0x00017604 File Offset: 0x00015804
		public LzwInputStream(Stream baseInputStream)
		{
			this.baseInputStream = baseInputStream;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00017651 File Offset: 0x00015851
		public override int ReadByte()
		{
			if (this.Read(this.one, 0, 1) == 1)
			{
				return (int)(this.one[0] & byte.MaxValue);
			}
			return -1;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00017674 File Offset: 0x00015874
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.headerParsed)
			{
				this.ParseHeader();
			}
			if (this.eof)
			{
				return 0;
			}
			int num = offset;
			int[] array = this.tabPrefix;
			byte[] array2 = this.tabSuffix;
			byte[] array3 = this.stack;
			int num2 = this.nBits;
			int num3 = this.maxCode;
			int num4 = this.maxMaxCode;
			int num5 = this.bitMask;
			int num6 = this.oldCode;
			byte b = this.finChar;
			int num7 = this.stackP;
			int num8 = this.freeEnt;
			byte[] array4 = this.data;
			int i = this.bitPos;
			int num9 = array3.Length - num7;
			if (num9 > 0)
			{
				int num10 = ((num9 >= count) ? count : num9);
				Array.Copy(array3, num7, buffer, offset, num10);
				offset += num10;
				count -= num10;
				num7 += num10;
			}
			if (count == 0)
			{
				this.stackP = num7;
				return offset - num;
			}
			int j;
			for (;;)
			{
				IL_00C6:
				if (this.end < 64)
				{
					this.Fill();
				}
				int num11 = ((this.got > 0) ? (this.end - this.end % num2 << 3) : ((this.end << 3) - (num2 - 1)));
				while (i < num11)
				{
					if (count == 0)
					{
						goto Block_8;
					}
					if (num8 > num3)
					{
						int num12 = num2 << 3;
						i = i - 1 + num12 - (i - 1 + num12) % num12;
						num2++;
						num3 = ((num2 == this.maxBits) ? num4 : ((1 << num2) - 1));
						num5 = (1 << num2) - 1;
						i = this.ResetBuf(i);
						goto IL_00C6;
					}
					int num13 = i >> 3;
					j = (((int)(array4[num13] & byte.MaxValue) | ((int)(array4[num13 + 1] & byte.MaxValue) << 8) | ((int)(array4[num13 + 2] & byte.MaxValue) << 16)) >> (i & 7)) & num5;
					i += num2;
					if (num6 == -1)
					{
						if (j >= 256)
						{
							goto Block_12;
						}
						b = (byte)(num6 = j);
						buffer[offset++] = b;
						count--;
					}
					else
					{
						if (j == 256 && this.blockMode)
						{
							Array.Copy(this.zeros, 0, array, 0, this.zeros.Length);
							num8 = 256;
							int num14 = num2 << 3;
							i = i - 1 + num14 - (i - 1 + num14) % num14;
							num2 = 9;
							num3 = (1 << num2) - 1;
							num5 = num3;
							i = this.ResetBuf(i);
							goto IL_00C6;
						}
						int num15 = j;
						num7 = array3.Length;
						if (j >= num8)
						{
							if (j > num8)
							{
								goto Block_16;
							}
							array3[--num7] = b;
							j = num6;
						}
						while (j >= 256)
						{
							array3[--num7] = array2[j];
							j = array[j];
						}
						b = array2[j];
						buffer[offset++] = b;
						count--;
						num9 = array3.Length - num7;
						int num16 = ((num9 >= count) ? count : num9);
						Array.Copy(array3, num7, buffer, offset, num16);
						offset += num16;
						count -= num16;
						num7 += num16;
						if (num8 < num4)
						{
							array[num8] = num6;
							array2[num8] = b;
							num8++;
						}
						num6 = num15;
						if (count == 0)
						{
							goto Block_20;
						}
					}
				}
				i = this.ResetBuf(i);
				if (this.got <= 0)
				{
					goto Block_22;
				}
			}
			Block_8:
			this.nBits = num2;
			this.maxCode = num3;
			this.maxMaxCode = num4;
			this.bitMask = num5;
			this.oldCode = num6;
			this.finChar = b;
			this.stackP = num7;
			this.freeEnt = num8;
			this.bitPos = i;
			return offset - num;
			Block_12:
			throw new LzwException("corrupt input: " + j.ToString() + " > 255");
			Block_16:
			throw new LzwException("corrupt input: code=" + j.ToString() + ", freeEnt=" + num8.ToString());
			Block_20:
			this.nBits = num2;
			this.maxCode = num3;
			this.bitMask = num5;
			this.oldCode = num6;
			this.finChar = b;
			this.stackP = num7;
			this.freeEnt = num8;
			this.bitPos = i;
			return offset - num;
			Block_22:
			this.nBits = num2;
			this.maxCode = num3;
			this.bitMask = num5;
			this.oldCode = num6;
			this.finChar = b;
			this.stackP = num7;
			this.freeEnt = num8;
			this.bitPos = i;
			this.eof = true;
			return offset - num;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017AA0 File Offset: 0x00015CA0
		private int ResetBuf(int bitPosition)
		{
			int num = bitPosition >> 3;
			Array.Copy(this.data, num, this.data, 0, this.end - num);
			this.end -= num;
			return 0;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00017ADC File Offset: 0x00015CDC
		private void Fill()
		{
			this.got = this.baseInputStream.Read(this.data, this.end, this.data.Length - 1 - this.end);
			if (this.got > 0)
			{
				this.end += this.got;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00017B34 File Offset: 0x00015D34
		private void ParseHeader()
		{
			this.headerParsed = true;
			byte[] array = new byte[3];
			if (this.baseInputStream.Read(array, 0, array.Length) < 0)
			{
				throw new LzwException("Failed to read LZW header");
			}
			if (array[0] != 31 || array[1] != 157)
			{
				throw new LzwException(string.Format("Wrong LZW header. Magic bytes don't match. 0x{0:x2} 0x{1:x2}", array[0], array[1]));
			}
			this.blockMode = (array[2] & 128) > 0;
			this.maxBits = (int)(array[2] & 31);
			if (this.maxBits > 16)
			{
				throw new LzwException(string.Concat(new string[]
				{
					"Stream compressed with ",
					this.maxBits.ToString(),
					" bits, but decompression can only handle ",
					16.ToString(),
					" bits."
				}));
			}
			if ((array[2] & 96) > 0)
			{
				throw new LzwException("Unsupported bits set in the header.");
			}
			this.maxMaxCode = 1 << this.maxBits;
			this.nBits = 9;
			this.maxCode = (1 << this.nBits) - 1;
			this.bitMask = this.maxCode;
			this.oldCode = -1;
			this.finChar = 0;
			this.freeEnt = (this.blockMode ? 257 : 256);
			this.tabPrefix = new int[1 << this.maxBits];
			this.tabSuffix = new byte[1 << this.maxBits];
			this.stack = new byte[1 << this.maxBits];
			this.stackP = this.stack.Length;
			for (int i = 255; i >= 0; i--)
			{
				this.tabSuffix[i] = (byte)i;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00017CE8 File Offset: 0x00015EE8
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00017CF5 File Offset: 0x00015EF5
		public override long Length
		{
			get
			{
				return (long)this.got;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00017CFE File Offset: 0x00015EFE
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00010FCA File Offset: 0x0000F1CA
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

		// Token: 0x060004CB RID: 1227 RVA: 0x00017D0B File Offset: 0x00015F0B
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00010FE3 File Offset: 0x0000F1E3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00010FEF File Offset: 0x0000F1EF
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00010FFB File Offset: 0x0000F1FB
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00011007 File Offset: 0x0000F207
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00017D18 File Offset: 0x00015F18
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

		// Token: 0x0400039F RID: 927
		private Stream baseInputStream;

		// Token: 0x040003A0 RID: 928
		private bool isClosed;

		// Token: 0x040003A1 RID: 929
		private readonly byte[] one = new byte[1];

		// Token: 0x040003A2 RID: 930
		private bool headerParsed;

		// Token: 0x040003A3 RID: 931
		private const int TBL_CLEAR = 256;

		// Token: 0x040003A4 RID: 932
		private const int TBL_FIRST = 257;

		// Token: 0x040003A5 RID: 933
		private int[] tabPrefix;

		// Token: 0x040003A6 RID: 934
		private byte[] tabSuffix;

		// Token: 0x040003A7 RID: 935
		private readonly int[] zeros = new int[256];

		// Token: 0x040003A8 RID: 936
		private byte[] stack;

		// Token: 0x040003A9 RID: 937
		private bool blockMode;

		// Token: 0x040003AA RID: 938
		private int nBits;

		// Token: 0x040003AB RID: 939
		private int maxBits;

		// Token: 0x040003AC RID: 940
		private int maxMaxCode;

		// Token: 0x040003AD RID: 941
		private int maxCode;

		// Token: 0x040003AE RID: 942
		private int bitMask;

		// Token: 0x040003AF RID: 943
		private int oldCode;

		// Token: 0x040003B0 RID: 944
		private byte finChar;

		// Token: 0x040003B1 RID: 945
		private int stackP;

		// Token: 0x040003B2 RID: 946
		private int freeEnt;

		// Token: 0x040003B3 RID: 947
		private readonly byte[] data = new byte[8192];

		// Token: 0x040003B4 RID: 948
		private int bitPos;

		// Token: 0x040003B5 RID: 949
		private int end;

		// Token: 0x040003B6 RID: 950
		private int got;

		// Token: 0x040003B7 RID: 951
		private bool eof;

		// Token: 0x040003B8 RID: 952
		private const int EXTRA = 64;
	}
}
