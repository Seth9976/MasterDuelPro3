using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x020000CA RID: 202
	public class BZip2InputStream : Stream
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0001BB18 File Offset: 0x00019D18
		public BZip2InputStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			for (int i = 0; i < 6; i++)
			{
				this.limit[i] = new int[258];
				this.baseArray[i] = new int[258];
				this.perm[i] = new int[258];
			}
			this.baseStream = stream;
			this.bsLive = 0;
			this.bsBuff = 0;
			this.Initialize();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001BC52 File Offset: 0x00019E52
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0001BC5A File Offset: 0x00019E5A
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001BC63 File Offset: 0x00019E63
		public override bool CanRead
		{
			get
			{
				return this.baseStream.CanRead;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001BC70 File Offset: 0x00019E70
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001BC7D File Offset: 0x00019E7D
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001BC8A File Offset: 0x00019E8A
		public override long Position
		{
			get
			{
				return this.baseStream.Position;
			}
			set
			{
				throw new NotSupportedException("BZip2InputStream position cannot be set");
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001BC96 File Offset: 0x00019E96
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001BCA3 File Offset: 0x00019EA3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("BZip2InputStream Seek not supported");
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001BCAF File Offset: 0x00019EAF
		public override void SetLength(long value)
		{
			throw new NotSupportedException("BZip2InputStream SetLength not supported");
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001BCBB File Offset: 0x00019EBB
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("BZip2InputStream Write not supported");
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001BCC7 File Offset: 0x00019EC7
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("BZip2InputStream WriteByte not supported");
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < count; i++)
			{
				int num = this.ReadByte();
				if (num == -1)
				{
					return i;
				}
				buffer[offset + i] = (byte)num;
			}
			return count;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001BD10 File Offset: 0x00019F10
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.IsStreamOwner)
			{
				this.baseStream.Dispose();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001BD28 File Offset: 0x00019F28
		public override int ReadByte()
		{
			if (this.streamEnd)
			{
				return -1;
			}
			int num = this.currentChar;
			switch (this.currentState)
			{
			case 3:
				this.SetupRandPartB();
				break;
			case 4:
				this.SetupRandPartC();
				break;
			case 6:
				this.SetupNoRandPartB();
				break;
			case 7:
				this.SetupNoRandPartC();
				break;
			}
			return num;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001BD94 File Offset: 0x00019F94
		private void MakeMaps()
		{
			this.nInUse = 0;
			for (int i = 0; i < 256; i++)
			{
				if (this.inUse[i])
				{
					this.seqToUnseq[this.nInUse] = (byte)i;
					this.unseqToSeq[i] = (byte)this.nInUse;
					this.nInUse++;
				}
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		private void Initialize()
		{
			int num = (int)this.BsGetUChar();
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			char c3 = this.BsGetUChar();
			if (num != 66 || c != 'Z' || c2 != 'h' || c3 < '1' || c3 > '9')
			{
				this.streamEnd = true;
				return;
			}
			this.SetDecompressStructureSizes((int)(c3 - '0'));
			this.computedCombinedCRC = 0U;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001BE4C File Offset: 0x0001A04C
		private void InitBlock()
		{
			char c = this.BsGetUChar();
			char c2 = this.BsGetUChar();
			char c3 = this.BsGetUChar();
			char c4 = this.BsGetUChar();
			char c5 = this.BsGetUChar();
			char c6 = this.BsGetUChar();
			if (c == '\u0017' && c2 == 'r' && c3 == 'E' && c4 == '8' && c5 == 'P' && c6 == '\u0090')
			{
				this.Complete();
				return;
			}
			if (c != '1' || c2 != 'A' || c3 != 'Y' || c4 != '&' || c5 != 'S' || c6 != 'Y')
			{
				BZip2InputStream.BadBlockHeader();
				this.streamEnd = true;
				return;
			}
			this.storedBlockCRC = this.BsGetInt32();
			this.blockRandomised = this.BsR(1) == 1;
			this.GetAndMoveToFrontDecode();
			this.mCrc.Reset();
			this.currentState = 1;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001BF10 File Offset: 0x0001A110
		private void EndBlock()
		{
			this.computedBlockCRC = (int)this.mCrc.Value;
			if (this.storedBlockCRC != this.computedBlockCRC)
			{
				BZip2InputStream.CrcError();
			}
			this.computedCombinedCRC = ((this.computedCombinedCRC << 1) & uint.MaxValue) | (this.computedCombinedCRC >> 31);
			this.computedCombinedCRC ^= (uint)this.computedBlockCRC;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001BF6F File Offset: 0x0001A16F
		private void Complete()
		{
			this.storedCombinedCRC = this.BsGetInt32();
			if (this.storedCombinedCRC != (int)this.computedCombinedCRC)
			{
				BZip2InputStream.CrcError();
			}
			this.streamEnd = true;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001BF98 File Offset: 0x0001A198
		private void FillBuffer()
		{
			int num = 0;
			try
			{
				num = this.baseStream.ReadByte();
			}
			catch (Exception)
			{
				BZip2InputStream.CompressedStreamEOF();
			}
			if (num == -1)
			{
				BZip2InputStream.CompressedStreamEOF();
			}
			this.bsBuff = (this.bsBuff << 8) | (num & 255);
			this.bsLive += 8;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		private int BsR(int n)
		{
			while (this.bsLive < n)
			{
				this.FillBuffer();
			}
			int num = (this.bsBuff >> this.bsLive - n) & ((1 << n) - 1);
			this.bsLive -= n;
			return num;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001C038 File Offset: 0x0001A238
		private char BsGetUChar()
		{
			return (char)this.BsR(8);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001C042 File Offset: 0x0001A242
		private int BsGetIntVS(int numBits)
		{
			return this.BsR(numBits);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001C04B File Offset: 0x0001A24B
		private int BsGetInt32()
		{
			return (((((this.BsR(8) << 8) | this.BsR(8)) << 8) | this.BsR(8)) << 8) | this.BsR(8);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001C074 File Offset: 0x0001A274
		private void RecvDecodingTables()
		{
			char[][] array = new char[6][];
			for (int i = 0; i < 6; i++)
			{
				array[i] = new char[258];
			}
			bool[] array2 = new bool[16];
			for (int j = 0; j < 16; j++)
			{
				array2[j] = this.BsR(1) == 1;
			}
			for (int k = 0; k < 16; k++)
			{
				if (array2[k])
				{
					for (int l = 0; l < 16; l++)
					{
						this.inUse[k * 16 + l] = this.BsR(1) == 1;
					}
				}
				else
				{
					for (int m = 0; m < 16; m++)
					{
						this.inUse[k * 16 + m] = false;
					}
				}
			}
			this.MakeMaps();
			int num = this.nInUse + 2;
			int num2 = this.BsR(3);
			int num3 = this.BsR(15);
			for (int n = 0; n < num3; n++)
			{
				int num4 = 0;
				while (this.BsR(1) == 1)
				{
					num4++;
				}
				this.selectorMtf[n] = (byte)num4;
			}
			byte[] array3 = new byte[6];
			for (int num5 = 0; num5 < num2; num5++)
			{
				array3[num5] = (byte)num5;
			}
			for (int num6 = 0; num6 < num3; num6++)
			{
				int num7 = (int)this.selectorMtf[num6];
				byte b = array3[num7];
				while (num7 > 0)
				{
					array3[num7] = array3[num7 - 1];
					num7--;
				}
				array3[0] = b;
				this.selector[num6] = b;
			}
			for (int num8 = 0; num8 < num2; num8++)
			{
				int num9 = this.BsR(5);
				for (int num10 = 0; num10 < num; num10++)
				{
					while (this.BsR(1) == 1)
					{
						if (this.BsR(1) == 0)
						{
							num9++;
						}
						else
						{
							num9--;
						}
					}
					array[num8][num10] = (char)num9;
				}
			}
			for (int num11 = 0; num11 < num2; num11++)
			{
				int num12 = 32;
				int num13 = 0;
				for (int num14 = 0; num14 < num; num14++)
				{
					num13 = Math.Max(num13, (int)array[num11][num14]);
					num12 = Math.Min(num12, (int)array[num11][num14]);
				}
				BZip2InputStream.HbCreateDecodeTables(this.limit[num11], this.baseArray[num11], this.perm[num11], array[num11], num12, num13, num);
				this.minLens[num11] = num12;
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		private void GetAndMoveToFrontDecode()
		{
			byte[] array = new byte[256];
			int num = 100000 * this.blockSize100k;
			this.origPtr = this.BsGetIntVS(24);
			this.RecvDecodingTables();
			int num2 = this.nInUse + 1;
			int num3 = -1;
			int num4 = 0;
			for (int i = 0; i <= 255; i++)
			{
				this.unzftab[i] = 0;
			}
			for (int j = 0; j <= 255; j++)
			{
				array[j] = (byte)j;
			}
			this.last = -1;
			if (num4 == 0)
			{
				num3++;
				num4 = 50;
			}
			num4--;
			int num5 = (int)this.selector[num3];
			int num6 = this.minLens[num5];
			int k;
			int num7;
			for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1) | num7)
			{
				if (num6 > 20)
				{
					throw new BZip2Exception("Bzip data error");
				}
				num6++;
				while (this.bsLive < 1)
				{
					this.FillBuffer();
				}
				num7 = (this.bsBuff >> this.bsLive - 1) & 1;
				this.bsLive--;
			}
			if (k - this.baseArray[num5][num6] < 0 || k - this.baseArray[num5][num6] >= 258)
			{
				throw new BZip2Exception("Bzip data error");
			}
			int num8 = this.perm[num5][k - this.baseArray[num5][num6]];
			while (num8 != num2)
			{
				if (num8 == 0 || num8 == 1)
				{
					int l = -1;
					int num9 = 1;
					do
					{
						if (num8 == 0)
						{
							l += num9;
						}
						else if (num8 == 1)
						{
							l += 2 * num9;
						}
						num9 <<= 1;
						if (num4 == 0)
						{
							num3++;
							num4 = 50;
						}
						num4--;
						num5 = (int)this.selector[num3];
						num6 = this.minLens[num5];
						for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1) | num7)
						{
							num6++;
							while (this.bsLive < 1)
							{
								this.FillBuffer();
							}
							num7 = (this.bsBuff >> this.bsLive - 1) & 1;
							this.bsLive--;
						}
						num8 = this.perm[num5][k - this.baseArray[num5][num6]];
					}
					while (num8 == 0 || num8 == 1);
					l++;
					byte b = this.seqToUnseq[(int)array[0]];
					this.unzftab[(int)b] += l;
					while (l > 0)
					{
						this.last++;
						this.ll8[this.last] = b;
						l--;
					}
					if (this.last >= num)
					{
						BZip2InputStream.BlockOverrun();
					}
				}
				else
				{
					this.last++;
					if (this.last >= num)
					{
						BZip2InputStream.BlockOverrun();
					}
					byte b2 = array[num8 - 1];
					this.unzftab[(int)this.seqToUnseq[(int)b2]]++;
					this.ll8[this.last] = this.seqToUnseq[(int)b2];
					int m = num8 - 1;
					while (m > 0)
					{
						array[m] = array[--m];
					}
					array[0] = b2;
					if (num4 == 0)
					{
						num3++;
						num4 = 50;
					}
					num4--;
					num5 = (int)this.selector[num3];
					num6 = this.minLens[num5];
					for (k = this.BsR(num6); k > this.limit[num5][num6]; k = (k << 1) | num7)
					{
						num6++;
						while (this.bsLive < 1)
						{
							this.FillBuffer();
						}
						num7 = (this.bsBuff >> this.bsLive - 1) & 1;
						this.bsLive--;
					}
					num8 = this.perm[num5][k - this.baseArray[num5][num6]];
				}
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001C698 File Offset: 0x0001A898
		private void SetupBlock()
		{
			int[] array = new int[257];
			array[0] = 0;
			Array.Copy(this.unzftab, 0, array, 1, 256);
			for (int i = 1; i <= 256; i++)
			{
				array[i] += array[i - 1];
			}
			for (int j = 0; j <= this.last; j++)
			{
				byte b = this.ll8[j];
				this.tt[array[(int)b]] = j;
				array[(int)b]++;
			}
			this.tPos = this.tt[this.origPtr];
			this.count = 0;
			this.i2 = 0;
			this.ch2 = 256;
			if (this.blockRandomised)
			{
				this.rNToGo = 0;
				this.rTPos = 0;
				this.SetupRandPartA();
				return;
			}
			this.SetupNoRandPartA();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001C76C File Offset: 0x0001A96C
		private void SetupRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.ch2 ^= ((this.rNToGo == 1) ? 1 : 0);
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 3;
				this.mCrc.Update(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001C868 File Offset: 0x0001AA68
		private void SetupNoRandPartA()
		{
			if (this.i2 <= this.last)
			{
				this.chPrev = this.ch2;
				this.ch2 = (int)this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.i2++;
				this.currentChar = this.ch2;
				this.currentState = 6;
				this.mCrc.Update(this.ch2);
				return;
			}
			this.EndBlock();
			this.InitBlock();
			this.SetupBlock();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		private void SetupRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 2;
				this.count = 1;
				this.SetupRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				if (this.rNToGo == 0)
				{
					this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
					this.rTPos++;
					if (this.rTPos == 512)
					{
						this.rTPos = 0;
					}
				}
				this.rNToGo--;
				this.z ^= ((this.rNToGo == 1) ? 1 : 0);
				this.j2 = 0;
				this.currentState = 4;
				this.SetupRandPartC();
				return;
			}
			this.currentState = 2;
			this.SetupRandPartA();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001C9F4 File Offset: 0x0001ABF4
		private void SetupRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.Update(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 2;
			this.i2++;
			this.count = 0;
			this.SetupRandPartA();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001CA60 File Offset: 0x0001AC60
		private void SetupNoRandPartB()
		{
			if (this.ch2 != this.chPrev)
			{
				this.currentState = 5;
				this.count = 1;
				this.SetupNoRandPartA();
				return;
			}
			this.count++;
			if (this.count >= 4)
			{
				this.z = this.ll8[this.tPos];
				this.tPos = this.tt[this.tPos];
				this.currentState = 7;
				this.j2 = 0;
				this.SetupNoRandPartC();
				return;
			}
			this.currentState = 5;
			this.SetupNoRandPartA();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		private void SetupNoRandPartC()
		{
			if (this.j2 < (int)this.z)
			{
				this.currentChar = this.ch2;
				this.mCrc.Update(this.ch2);
				this.j2++;
				return;
			}
			this.currentState = 5;
			this.i2++;
			this.count = 0;
			this.SetupNoRandPartA();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		private void SetDecompressStructureSizes(int newSize100k)
		{
			if (0 > newSize100k || newSize100k > 9 || 0 > this.blockSize100k || this.blockSize100k > 9)
			{
				throw new BZip2Exception("Invalid block size");
			}
			this.blockSize100k = newSize100k;
			if (newSize100k == 0)
			{
				return;
			}
			int num = 100000 * newSize100k;
			this.ll8 = new byte[num];
			this.tt = new int[num];
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001CBBB File Offset: 0x0001ADBB
		private static void CompressedStreamEOF()
		{
			throw new EndOfStreamException("BZip2 input stream end of compressed stream");
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001CBC7 File Offset: 0x0001ADC7
		private static void BlockOverrun()
		{
			throw new BZip2Exception("BZip2 input stream block overrun");
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001CBD3 File Offset: 0x0001ADD3
		private static void BadBlockHeader()
		{
			throw new BZip2Exception("BZip2 input stream bad block header");
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001CBDF File Offset: 0x0001ADDF
		private static void CrcError()
		{
			throw new BZip2Exception("BZip2 input stream crc error");
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		private static void HbCreateDecodeTables(int[] limit, int[] baseArray, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
		{
			int num = 0;
			for (int i = minLen; i <= maxLen; i++)
			{
				for (int j = 0; j < alphaSize; j++)
				{
					if ((int)length[j] == i)
					{
						perm[num] = j;
						num++;
					}
				}
			}
			for (int k = 0; k < 23; k++)
			{
				baseArray[k] = 0;
			}
			for (int l = 0; l < alphaSize; l++)
			{
				baseArray[(int)(length[l] + '\u0001')]++;
			}
			for (int m = 1; m < 23; m++)
			{
				baseArray[m] += baseArray[m - 1];
			}
			for (int n = 0; n < 23; n++)
			{
				limit[n] = 0;
			}
			int num2 = 0;
			for (int num3 = minLen; num3 <= maxLen; num3++)
			{
				num2 += baseArray[num3 + 1] - baseArray[num3];
				limit[num3] = num2 - 1;
				num2 <<= 1;
			}
			for (int num4 = minLen + 1; num4 <= maxLen; num4++)
			{
				baseArray[num4] = (limit[num4 - 1] + 1 << 1) - baseArray[num4];
			}
		}

		// Token: 0x04000472 RID: 1138
		private const int START_BLOCK_STATE = 1;

		// Token: 0x04000473 RID: 1139
		private const int RAND_PART_A_STATE = 2;

		// Token: 0x04000474 RID: 1140
		private const int RAND_PART_B_STATE = 3;

		// Token: 0x04000475 RID: 1141
		private const int RAND_PART_C_STATE = 4;

		// Token: 0x04000476 RID: 1142
		private const int NO_RAND_PART_A_STATE = 5;

		// Token: 0x04000477 RID: 1143
		private const int NO_RAND_PART_B_STATE = 6;

		// Token: 0x04000478 RID: 1144
		private const int NO_RAND_PART_C_STATE = 7;

		// Token: 0x04000479 RID: 1145
		private int last;

		// Token: 0x0400047A RID: 1146
		private int origPtr;

		// Token: 0x0400047B RID: 1147
		private int blockSize100k;

		// Token: 0x0400047C RID: 1148
		private bool blockRandomised;

		// Token: 0x0400047D RID: 1149
		private int bsBuff;

		// Token: 0x0400047E RID: 1150
		private int bsLive;

		// Token: 0x0400047F RID: 1151
		private IChecksum mCrc = new BZip2Crc();

		// Token: 0x04000480 RID: 1152
		private bool[] inUse = new bool[256];

		// Token: 0x04000481 RID: 1153
		private int nInUse;

		// Token: 0x04000482 RID: 1154
		private byte[] seqToUnseq = new byte[256];

		// Token: 0x04000483 RID: 1155
		private byte[] unseqToSeq = new byte[256];

		// Token: 0x04000484 RID: 1156
		private byte[] selector = new byte[18002];

		// Token: 0x04000485 RID: 1157
		private byte[] selectorMtf = new byte[18002];

		// Token: 0x04000486 RID: 1158
		private int[] tt;

		// Token: 0x04000487 RID: 1159
		private byte[] ll8;

		// Token: 0x04000488 RID: 1160
		private int[] unzftab = new int[256];

		// Token: 0x04000489 RID: 1161
		private int[][] limit = new int[6][];

		// Token: 0x0400048A RID: 1162
		private int[][] baseArray = new int[6][];

		// Token: 0x0400048B RID: 1163
		private int[][] perm = new int[6][];

		// Token: 0x0400048C RID: 1164
		private int[] minLens = new int[6];

		// Token: 0x0400048D RID: 1165
		private readonly Stream baseStream;

		// Token: 0x0400048E RID: 1166
		private bool streamEnd;

		// Token: 0x0400048F RID: 1167
		private int currentChar = -1;

		// Token: 0x04000490 RID: 1168
		private int currentState = 1;

		// Token: 0x04000491 RID: 1169
		private int storedBlockCRC;

		// Token: 0x04000492 RID: 1170
		private int storedCombinedCRC;

		// Token: 0x04000493 RID: 1171
		private int computedBlockCRC;

		// Token: 0x04000494 RID: 1172
		private uint computedCombinedCRC;

		// Token: 0x04000495 RID: 1173
		private int count;

		// Token: 0x04000496 RID: 1174
		private int chPrev;

		// Token: 0x04000497 RID: 1175
		private int ch2;

		// Token: 0x04000498 RID: 1176
		private int tPos;

		// Token: 0x04000499 RID: 1177
		private int rNToGo;

		// Token: 0x0400049A RID: 1178
		private int rTPos;

		// Token: 0x0400049B RID: 1179
		private int i2;

		// Token: 0x0400049C RID: 1180
		private int j2;

		// Token: 0x0400049D RID: 1181
		private byte z;
	}
}
