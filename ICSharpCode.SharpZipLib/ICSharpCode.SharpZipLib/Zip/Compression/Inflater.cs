using System;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x0200005C RID: 92
	public class Inflater
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000E9F2 File Offset: 0x0000CBF2
		public Inflater()
			: this(false)
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E9FB File Offset: 0x0000CBFB
		public Inflater(bool noHeader)
		{
			this.noHeader = noHeader;
			if (!noHeader)
			{
				this.adler = new Adler32();
			}
			this.input = new StreamManipulator();
			this.outputWindow = new OutputWindow();
			this.mode = (noHeader ? 2 : 0);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		public void Reset()
		{
			this.mode = (this.noHeader ? 2 : 0);
			this.totalIn = 0L;
			this.totalOut = 0L;
			this.input.Reset();
			this.outputWindow.Reset();
			this.dynHeader = null;
			this.litlenTree = null;
			this.distTree = null;
			this.isLastBlock = false;
			Adler32 adler = this.adler;
			if (adler == null)
			{
				return;
			}
			adler.Reset();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		private bool DecodeHeader()
		{
			int num = this.input.PeekBits(16);
			if (num < 0)
			{
				return false;
			}
			this.input.DropBits(16);
			num = ((num << 8) | (num >> 8)) & 65535;
			if (num % 31 != 0)
			{
				throw new SharpZipBaseException("Header checksum illegal");
			}
			if ((num & 3840) != 2048)
			{
				throw new SharpZipBaseException("Compression Method unknown");
			}
			if ((num & 32) == 0)
			{
				this.mode = 2;
			}
			else
			{
				this.mode = 1;
				this.neededBits = 32;
			}
			return true;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000EB38 File Offset: 0x0000CD38
		private bool DecodeDict()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				if (num < 0)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8) | num;
				this.neededBits -= 8;
			}
			return false;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000EB90 File Offset: 0x0000CD90
		private bool DecodeHuffman()
		{
			int i = this.outputWindow.GetFreeSpace();
			while (i >= 258)
			{
				int num;
				switch (this.mode)
				{
				case 7:
					while (((num = this.litlenTree.GetSymbol(this.input)) & -256) == 0)
					{
						this.outputWindow.Write(num);
						if (--i < 258)
						{
							return true;
						}
					}
					if (num >= 257)
					{
						try
						{
							this.repLength = Inflater.CPLENS[num - 257];
							this.neededBits = Inflater.CPLEXT[num - 257];
						}
						catch (Exception)
						{
							throw new SharpZipBaseException("Illegal rep length code");
						}
						goto IL_00C4;
					}
					if (num < 0)
					{
						return false;
					}
					this.distTree = null;
					this.litlenTree = null;
					this.mode = 2;
					return true;
				case 8:
					goto IL_00C4;
				case 9:
					goto IL_0113;
				case 10:
					break;
				default:
					throw new SharpZipBaseException("Inflater unknown mode");
				}
				IL_0154:
				if (this.neededBits > 0)
				{
					this.mode = 10;
					int num2 = this.input.PeekBits(this.neededBits);
					if (num2 < 0)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repDist += num2;
				}
				this.outputWindow.Repeat(this.repLength, this.repDist);
				i -= this.repLength;
				this.mode = 7;
				continue;
				IL_0113:
				num = this.distTree.GetSymbol(this.input);
				if (num < 0)
				{
					return false;
				}
				try
				{
					this.repDist = Inflater.CPDIST[num];
					this.neededBits = Inflater.CPDEXT[num];
				}
				catch (Exception)
				{
					throw new SharpZipBaseException("Illegal rep dist code");
				}
				goto IL_0154;
				IL_00C4:
				if (this.neededBits > 0)
				{
					this.mode = 8;
					int num3 = this.input.PeekBits(this.neededBits);
					if (num3 < 0)
					{
						return false;
					}
					this.input.DropBits(this.neededBits);
					this.repLength += num3;
				}
				this.mode = 9;
				goto IL_0113;
			}
			return true;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000ED98 File Offset: 0x0000CF98
		private bool DecodeChksum()
		{
			while (this.neededBits > 0)
			{
				int num = this.input.PeekBits(8);
				if (num < 0)
				{
					return false;
				}
				this.input.DropBits(8);
				this.readAdler = (this.readAdler << 8) | num;
				this.neededBits -= 8;
			}
			Adler32 adler = this.adler;
			if ((int)((adler != null) ? new long?(adler.Value) : null).Value != this.readAdler)
			{
				string text = "Adler chksum doesn't match: ";
				Adler32 adler2 = this.adler;
				throw new SharpZipBaseException(text + ((int)((adler2 != null) ? new long?(adler2.Value) : null).Value).ToString() + " vs. " + this.readAdler.ToString());
			}
			this.mode = 12;
			return false;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000EE78 File Offset: 0x0000D078
		private bool Decode()
		{
			switch (this.mode)
			{
			case 0:
				return this.DecodeHeader();
			case 1:
				return this.DecodeDict();
			case 2:
				if (this.isLastBlock)
				{
					if (this.noHeader)
					{
						this.mode = 12;
						return false;
					}
					this.input.SkipToByteBoundary();
					this.neededBits = 32;
					this.mode = 11;
					return true;
				}
				else
				{
					int num = this.input.PeekBits(3);
					if (num < 0)
					{
						return false;
					}
					this.input.DropBits(3);
					this.isLastBlock |= (num & 1) != 0;
					switch (num >> 1)
					{
					case 0:
						this.input.SkipToByteBoundary();
						this.mode = 3;
						break;
					case 1:
						this.litlenTree = InflaterHuffmanTree.defLitLenTree;
						this.distTree = InflaterHuffmanTree.defDistTree;
						this.mode = 7;
						break;
					case 2:
						this.dynHeader = new InflaterDynHeader(this.input);
						this.mode = 6;
						break;
					default:
						throw new SharpZipBaseException("Unknown block type " + num.ToString());
					}
					return true;
				}
				break;
			case 3:
				if ((this.uncomprLen = this.input.PeekBits(16)) < 0)
				{
					return false;
				}
				this.input.DropBits(16);
				this.mode = 4;
				break;
			case 4:
				break;
			case 5:
				goto IL_01B3;
			case 6:
				if (!this.dynHeader.AttemptRead())
				{
					return false;
				}
				this.litlenTree = this.dynHeader.LiteralLengthTree;
				this.distTree = this.dynHeader.DistanceTree;
				this.mode = 7;
				goto IL_0233;
			case 7:
			case 8:
			case 9:
			case 10:
				goto IL_0233;
			case 11:
				return this.DecodeChksum();
			case 12:
				return false;
			default:
				throw new SharpZipBaseException("Inflater.Decode unknown mode");
			}
			int num2 = this.input.PeekBits(16);
			if (num2 < 0)
			{
				return false;
			}
			this.input.DropBits(16);
			if (num2 != (this.uncomprLen ^ 65535))
			{
				throw new SharpZipBaseException("broken uncompressed block");
			}
			this.mode = 5;
			IL_01B3:
			int num3 = this.outputWindow.CopyStored(this.input, this.uncomprLen);
			this.uncomprLen -= num3;
			if (this.uncomprLen == 0)
			{
				this.mode = 2;
				return true;
			}
			return !this.input.IsNeedingInput;
			IL_0233:
			return this.DecodeHuffman();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000F0CB File Offset: 0x0000D2CB
		public void SetDictionary(byte[] buffer)
		{
			this.SetDictionary(buffer, 0, buffer.Length);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		public void SetDictionary(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (!this.IsNeedingDictionary)
			{
				throw new InvalidOperationException("Dictionary is not needed");
			}
			Adler32 adler = this.adler;
			if (adler != null)
			{
				adler.Update(new ArraySegment<byte>(buffer, index, count));
			}
			if (this.adler != null && (int)this.adler.Value != this.readAdler)
			{
				throw new SharpZipBaseException("Wrong adler checksum");
			}
			Adler32 adler2 = this.adler;
			if (adler2 != null)
			{
				adler2.Reset();
			}
			this.outputWindow.CopyDict(buffer, index, count);
			this.mode = 2;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000F18A File Offset: 0x0000D38A
		public void SetInput(byte[] buffer)
		{
			this.SetInput(buffer, 0, buffer.Length);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000F197 File Offset: 0x0000D397
		public void SetInput(byte[] buffer, int index, int count)
		{
			this.input.SetInput(buffer, index, count);
			this.totalIn += (long)count;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000F1B6 File Offset: 0x0000D3B6
		public int Inflate(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Inflate(buffer, 0, buffer.Length);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		public int Inflate(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "count cannot be negative");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset cannot be negative");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("count exceeds buffer bounds");
			}
			if (count == 0)
			{
				if (!this.IsFinished)
				{
					this.Decode();
				}
				return 0;
			}
			int num = 0;
			for (;;)
			{
				if (this.mode != 11)
				{
					int num2 = this.outputWindow.CopyOutput(buffer, offset, count);
					if (num2 > 0)
					{
						Adler32 adler = this.adler;
						if (adler != null)
						{
							adler.Update(new ArraySegment<byte>(buffer, offset, num2));
						}
						offset += num2;
						num += num2;
						this.totalOut += (long)num2;
						count -= num2;
						if (count == 0)
						{
							break;
						}
					}
				}
				if (!this.Decode() && (this.outputWindow.GetAvailable() <= 0 || this.mode == 11))
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000F2B9 File Offset: 0x0000D4B9
		public bool IsNeedingInput
		{
			get
			{
				return this.input.IsNeedingInput;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000F2C6 File Offset: 0x0000D4C6
		public bool IsNeedingDictionary
		{
			get
			{
				return this.mode == 1 && this.neededBits == 0;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public bool IsFinished
		{
			get
			{
				return this.mode == 12 && this.outputWindow.GetAvailable() == 0;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		public int Adler
		{
			get
			{
				if (this.IsNeedingDictionary)
				{
					return this.readAdler;
				}
				if (this.adler != null)
				{
					return (int)this.adler.Value;
				}
				return 0;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000F31F File Offset: 0x0000D51F
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000F327 File Offset: 0x0000D527
		public long TotalIn
		{
			get
			{
				return this.totalIn - (long)this.RemainingInput;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000F337 File Offset: 0x0000D537
		public int RemainingInput
		{
			get
			{
				return this.input.AvailableBytes;
			}
		}

		// Token: 0x04000209 RID: 521
		private static readonly int[] CPLENS = new int[]
		{
			3, 4, 5, 6, 7, 8, 9, 10, 11, 13,
			15, 17, 19, 23, 27, 31, 35, 43, 51, 59,
			67, 83, 99, 115, 131, 163, 195, 227, 258
		};

		// Token: 0x0400020A RID: 522
		private static readonly int[] CPLEXT = new int[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 1, 1,
			1, 1, 2, 2, 2, 2, 3, 3, 3, 3,
			4, 4, 4, 4, 5, 5, 5, 5, 0
		};

		// Token: 0x0400020B RID: 523
		private static readonly int[] CPDIST = new int[]
		{
			1, 2, 3, 4, 5, 7, 9, 13, 17, 25,
			33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
			1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577
		};

		// Token: 0x0400020C RID: 524
		private static readonly int[] CPDEXT = new int[]
		{
			0, 0, 0, 0, 1, 1, 2, 2, 3, 3,
			4, 4, 5, 5, 6, 6, 7, 7, 8, 8,
			9, 9, 10, 10, 11, 11, 12, 12, 13, 13
		};

		// Token: 0x0400020D RID: 525
		private const int DECODE_HEADER = 0;

		// Token: 0x0400020E RID: 526
		private const int DECODE_DICT = 1;

		// Token: 0x0400020F RID: 527
		private const int DECODE_BLOCKS = 2;

		// Token: 0x04000210 RID: 528
		private const int DECODE_STORED_LEN1 = 3;

		// Token: 0x04000211 RID: 529
		private const int DECODE_STORED_LEN2 = 4;

		// Token: 0x04000212 RID: 530
		private const int DECODE_STORED = 5;

		// Token: 0x04000213 RID: 531
		private const int DECODE_DYN_HEADER = 6;

		// Token: 0x04000214 RID: 532
		private const int DECODE_HUFFMAN = 7;

		// Token: 0x04000215 RID: 533
		private const int DECODE_HUFFMAN_LENBITS = 8;

		// Token: 0x04000216 RID: 534
		private const int DECODE_HUFFMAN_DIST = 9;

		// Token: 0x04000217 RID: 535
		private const int DECODE_HUFFMAN_DISTBITS = 10;

		// Token: 0x04000218 RID: 536
		private const int DECODE_CHKSUM = 11;

		// Token: 0x04000219 RID: 537
		private const int FINISHED = 12;

		// Token: 0x0400021A RID: 538
		private int mode;

		// Token: 0x0400021B RID: 539
		private int readAdler;

		// Token: 0x0400021C RID: 540
		private int neededBits;

		// Token: 0x0400021D RID: 541
		private int repLength;

		// Token: 0x0400021E RID: 542
		private int repDist;

		// Token: 0x0400021F RID: 543
		private int uncomprLen;

		// Token: 0x04000220 RID: 544
		private bool isLastBlock;

		// Token: 0x04000221 RID: 545
		private long totalOut;

		// Token: 0x04000222 RID: 546
		private long totalIn;

		// Token: 0x04000223 RID: 547
		private bool noHeader;

		// Token: 0x04000224 RID: 548
		private readonly StreamManipulator input;

		// Token: 0x04000225 RID: 549
		private OutputWindow outputWindow;

		// Token: 0x04000226 RID: 550
		private InflaterDynHeader dynHeader;

		// Token: 0x04000227 RID: 551
		private InflaterHuffmanTree litlenTree;

		// Token: 0x04000228 RID: 552
		private InflaterHuffmanTree distTree;

		// Token: 0x04000229 RID: 553
		private Adler32 adler;
	}
}
