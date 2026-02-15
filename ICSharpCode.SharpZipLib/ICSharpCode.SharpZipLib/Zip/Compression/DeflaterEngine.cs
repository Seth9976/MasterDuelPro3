using System;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000058 RID: 88
	public class DeflaterEngine
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public DeflaterEngine(DeflaterPending pending)
			: this(pending, false)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000C9EC File Offset: 0x0000ABEC
		public DeflaterEngine(DeflaterPending pending, bool noAdlerCalculation)
		{
			this.pending = pending;
			this.huffman = new DeflaterHuffman(pending);
			if (!noAdlerCalculation)
			{
				this.adler = new Adler32();
			}
			this.window = new byte[65536];
			this.head = new short[32768];
			this.prev = new short[32768];
			this.blockStart = (this.strstart = 1);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public bool Deflate(bool flush, bool finish)
		{
			for (;;)
			{
				this.FillWindow();
				bool flag = flush && this.inputOff == this.inputEnd;
				bool flag2;
				switch (this.compressionFunction)
				{
				case 0:
					flag2 = this.DeflateStored(flag, finish);
					goto IL_0062;
				case 1:
					flag2 = this.DeflateFast(flag, finish);
					goto IL_0062;
				case 2:
					flag2 = this.DeflateSlow(flag, finish);
					goto IL_0062;
				}
				break;
				IL_0062:
				if (!this.pending.IsFlushed || !flag2)
				{
					return flag2;
				}
			}
			throw new InvalidOperationException("unknown compressionFunction");
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public void SetInput(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.inputOff < this.inputEnd)
			{
				throw new InvalidOperationException("Old input was not completely processed");
			}
			int num = offset + count;
			if (offset > num || num > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.inputBuf = buffer;
			this.inputOff = offset;
			this.inputEnd = num;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CB60 File Offset: 0x0000AD60
		public bool NeedsInput()
		{
			return this.inputEnd == this.inputOff;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public void SetDictionary(byte[] buffer, int offset, int length)
		{
			Adler32 adler = this.adler;
			if (adler != null)
			{
				adler.Update(new ArraySegment<byte>(buffer, offset, length));
			}
			if (length < 3)
			{
				return;
			}
			if (length > 32506)
			{
				offset += length - 32506;
				length = 32506;
			}
			Array.Copy(buffer, offset, this.window, this.strstart, length);
			this.UpdateHash();
			length--;
			while (--length > 0)
			{
				this.InsertString();
				this.strstart++;
			}
			this.strstart += 2;
			this.blockStart = this.strstart;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public void Reset()
		{
			this.huffman.Reset();
			Adler32 adler = this.adler;
			if (adler != null)
			{
				adler.Reset();
			}
			this.blockStart = (this.strstart = 1);
			this.lookahead = 0;
			this.totalIn = 0L;
			this.prevAvailable = false;
			this.matchLen = 2;
			for (int i = 0; i < 32768; i++)
			{
				this.head[i] = 0;
			}
			for (int j = 0; j < 32768; j++)
			{
				this.prev[j] = 0;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public void ResetAdler()
		{
			Adler32 adler = this.adler;
			if (adler == null)
			{
				return;
			}
			adler.Reset();
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CCAA File Offset: 0x0000AEAA
		public int Adler
		{
			get
			{
				if (this.adler == null)
				{
					return 0;
				}
				return (int)this.adler.Value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000CCC2 File Offset: 0x0000AEC2
		public long TotalIn
		{
			get
			{
				return this.totalIn;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000CCCA File Offset: 0x0000AECA
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000CCD2 File Offset: 0x0000AED2
		public DeflateStrategy Strategy
		{
			get
			{
				return this.strategy;
			}
			set
			{
				this.strategy = value;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		public void SetLevel(int level)
		{
			if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.goodLength = DeflaterConstants.GOOD_LENGTH[level];
			this.max_lazy = DeflaterConstants.MAX_LAZY[level];
			this.niceLength = DeflaterConstants.NICE_LENGTH[level];
			this.max_chain = DeflaterConstants.MAX_CHAIN[level];
			if (DeflaterConstants.COMPR_FUNC[level] != this.compressionFunction)
			{
				switch (this.compressionFunction)
				{
				case 0:
					if (this.strstart > this.blockStart)
					{
						this.huffman.FlushStoredBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					this.UpdateHash();
					break;
				case 1:
					if (this.strstart > this.blockStart)
					{
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					break;
				case 2:
					if (this.prevAvailable)
					{
						this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
					}
					if (this.strstart > this.blockStart)
					{
						this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
						this.blockStart = this.strstart;
					}
					this.prevAvailable = false;
					this.matchLen = 2;
					break;
				}
				this.compressionFunction = DeflaterConstants.COMPR_FUNC[level];
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000CE74 File Offset: 0x0000B074
		public void FillWindow()
		{
			if (this.strstart >= 65274)
			{
				this.SlideWindow();
			}
			if (this.lookahead < 262 && this.inputOff < this.inputEnd)
			{
				int num = 65536 - this.lookahead - this.strstart;
				if (num > this.inputEnd - this.inputOff)
				{
					num = this.inputEnd - this.inputOff;
				}
				Array.Copy(this.inputBuf, this.inputOff, this.window, this.strstart + this.lookahead, num);
				Adler32 adler = this.adler;
				if (adler != null)
				{
					adler.Update(new ArraySegment<byte>(this.inputBuf, this.inputOff, num));
				}
				this.inputOff += num;
				this.totalIn += (long)num;
				this.lookahead += num;
			}
			if (this.lookahead >= 3)
			{
				this.UpdateHash();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000CF69 File Offset: 0x0000B169
		private void UpdateHash()
		{
			this.ins_h = ((int)this.window[this.strstart] << 5) ^ (int)this.window[this.strstart + 1];
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000CF90 File Offset: 0x0000B190
		private int InsertString()
		{
			int num = ((this.ins_h << 5) ^ (int)this.window[this.strstart + 2]) & 32767;
			short num2 = (this.prev[this.strstart & 32767] = this.head[num]);
			this.head[num] = (short)this.strstart;
			this.ins_h = num;
			return (int)num2 & 65535;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		private void SlideWindow()
		{
			Array.Copy(this.window, 32768, this.window, 0, 32768);
			this.matchStart -= 32768;
			this.strstart -= 32768;
			this.blockStart -= 32768;
			for (int i = 0; i < 32768; i++)
			{
				int num = (int)this.head[i] & 65535;
				this.head[i] = (short)((num >= 32768) ? (num - 32768) : 0);
			}
			for (int j = 0; j < 32768; j++)
			{
				int num2 = (int)this.prev[j] & 65535;
				this.prev[j] = (short)((num2 >= 32768) ? (num2 - 32768) : 0);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		private bool FindLongestMatch(int curMatch)
		{
			int num = this.strstart;
			int num2 = num + Math.Min(258, this.lookahead) - 1;
			int num3 = Math.Max(num - 32506, 0);
			byte[] array = this.window;
			short[] array2 = this.prev;
			int num4 = this.max_chain;
			int num5 = Math.Min(this.niceLength, this.lookahead);
			this.matchLen = Math.Max(this.matchLen, 2);
			if (num + this.matchLen > num2)
			{
				return false;
			}
			byte b = array[num + this.matchLen - 1];
			byte b2 = array[num + this.matchLen];
			if (this.matchLen >= this.goodLength)
			{
				num4 >>= 2;
			}
			do
			{
				int num6 = curMatch;
				num = this.strstart;
				if (array[num6 + this.matchLen] == b2 && array[num6 + this.matchLen - 1] == b && array[num6] == array[num] && array[++num6] == array[++num])
				{
					switch ((num2 - num) % 8)
					{
					case 1:
						if (array[++num] == array[++num6])
						{
						}
						break;
					case 2:
						if (array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 3:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 4:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 5:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 6:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
						}
						break;
					case 7:
						if (array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6] && array[++num] == array[++num6])
						{
							byte b3 = array[++num];
							byte b4 = array[++num6];
						}
						break;
					}
					if (array[num] == array[num6])
					{
						while (num != num2)
						{
							if (array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6] || array[++num] != array[++num6])
							{
								goto IL_042C;
							}
						}
						num++;
						num6++;
					}
					IL_042C:
					if (num - this.strstart > this.matchLen)
					{
						this.matchStart = curMatch;
						this.matchLen = num - this.strstart;
						if (this.matchLen >= num5)
						{
							break;
						}
						b = array[num - 1];
						b2 = array[num];
					}
				}
			}
			while ((curMatch = (int)array2[curMatch & 32767] & 65535) > num3 && --num4 != 0);
			return this.matchLen >= 3;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D570 File Offset: 0x0000B770
		private bool DeflateStored(bool flush, bool finish)
		{
			if (!flush && this.lookahead == 0)
			{
				return false;
			}
			this.strstart += this.lookahead;
			this.lookahead = 0;
			int num = this.strstart - this.blockStart;
			if (num >= DeflaterConstants.MAX_BLOCK_SIZE || (this.blockStart < 32768 && num >= 32506) || flush)
			{
				bool flag = finish;
				if (num > DeflaterConstants.MAX_BLOCK_SIZE)
				{
					num = DeflaterConstants.MAX_BLOCK_SIZE;
					flag = false;
				}
				this.huffman.FlushStoredBlock(this.window, this.blockStart, num, flag);
				this.blockStart += num;
				return !flag && num != 0;
			}
			return true;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D620 File Offset: 0x0000B820
		private bool DeflateFast(bool flush, bool finish)
		{
			if (this.lookahead < 262 && !flush)
			{
				return false;
			}
			while (this.lookahead >= 262 || flush)
			{
				if (this.lookahead == 0)
				{
					this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
					this.blockStart = this.strstart;
					return false;
				}
				if (this.strstart > 65274)
				{
					this.SlideWindow();
				}
				int num;
				if (this.lookahead >= 3 && (num = this.InsertString()) != 0 && this.strategy != DeflateStrategy.HuffmanOnly && this.strstart - num <= 32506 && this.FindLongestMatch(num))
				{
					bool flag = this.huffman.TallyDist(this.strstart - this.matchStart, this.matchLen);
					this.lookahead -= this.matchLen;
					if (this.matchLen <= this.max_lazy && this.lookahead >= 3)
					{
						for (;;)
						{
							int num2 = this.matchLen - 1;
							this.matchLen = num2;
							if (num2 <= 0)
							{
								break;
							}
							this.strstart++;
							this.InsertString();
						}
						this.strstart++;
					}
					else
					{
						this.strstart += this.matchLen;
						if (this.lookahead >= 2)
						{
							this.UpdateHash();
						}
					}
					this.matchLen = 2;
					if (!flag)
					{
						continue;
					}
				}
				else
				{
					this.huffman.TallyLit((int)(this.window[this.strstart] & byte.MaxValue));
					this.strstart++;
					this.lookahead--;
				}
				if (this.huffman.IsFull())
				{
					bool flag2 = finish && this.lookahead == 0;
					this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, flag2);
					this.blockStart = this.strstart;
					return !flag2;
				}
			}
			return true;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D834 File Offset: 0x0000BA34
		private bool DeflateSlow(bool flush, bool finish)
		{
			if (this.lookahead < 262 && !flush)
			{
				return false;
			}
			while (this.lookahead >= 262 || flush)
			{
				if (this.lookahead == 0)
				{
					if (this.prevAvailable)
					{
						this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
					}
					this.prevAvailable = false;
					this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
					this.blockStart = this.strstart;
					return false;
				}
				if (this.strstart >= 65274)
				{
					this.SlideWindow();
				}
				int num = this.matchStart;
				int num2 = this.matchLen;
				if (this.lookahead >= 3)
				{
					int num3 = this.InsertString();
					if (this.strategy != DeflateStrategy.HuffmanOnly && num3 != 0 && this.strstart - num3 <= 32506 && this.FindLongestMatch(num3) && this.matchLen <= 5 && (this.strategy == DeflateStrategy.Filtered || (this.matchLen == 3 && this.strstart - this.matchStart > 4096)))
					{
						this.matchLen = 2;
					}
				}
				if (num2 >= 3 && this.matchLen <= num2)
				{
					this.huffman.TallyDist(this.strstart - 1 - num, num2);
					num2 -= 2;
					do
					{
						this.strstart++;
						this.lookahead--;
						if (this.lookahead >= 3)
						{
							this.InsertString();
						}
					}
					while (--num2 > 0);
					this.strstart++;
					this.lookahead--;
					this.prevAvailable = false;
					this.matchLen = 2;
				}
				else
				{
					if (this.prevAvailable)
					{
						this.huffman.TallyLit((int)(this.window[this.strstart - 1] & byte.MaxValue));
					}
					this.prevAvailable = true;
					this.strstart++;
					this.lookahead--;
				}
				if (this.huffman.IsFull())
				{
					int num4 = this.strstart - this.blockStart;
					if (this.prevAvailable)
					{
						num4--;
					}
					bool flag = finish && this.lookahead == 0 && !this.prevAvailable;
					this.huffman.FlushBlock(this.window, this.blockStart, num4, flag);
					this.blockStart += num4;
					return !flag;
				}
			}
			return true;
		}

		// Token: 0x040001D3 RID: 467
		private const int TooFar = 4096;

		// Token: 0x040001D4 RID: 468
		private int ins_h;

		// Token: 0x040001D5 RID: 469
		private short[] head;

		// Token: 0x040001D6 RID: 470
		private short[] prev;

		// Token: 0x040001D7 RID: 471
		private int matchStart;

		// Token: 0x040001D8 RID: 472
		private int matchLen;

		// Token: 0x040001D9 RID: 473
		private bool prevAvailable;

		// Token: 0x040001DA RID: 474
		private int blockStart;

		// Token: 0x040001DB RID: 475
		private int strstart;

		// Token: 0x040001DC RID: 476
		private int lookahead;

		// Token: 0x040001DD RID: 477
		private byte[] window;

		// Token: 0x040001DE RID: 478
		private DeflateStrategy strategy;

		// Token: 0x040001DF RID: 479
		private int max_chain;

		// Token: 0x040001E0 RID: 480
		private int max_lazy;

		// Token: 0x040001E1 RID: 481
		private int niceLength;

		// Token: 0x040001E2 RID: 482
		private int goodLength;

		// Token: 0x040001E3 RID: 483
		private int compressionFunction;

		// Token: 0x040001E4 RID: 484
		private byte[] inputBuf;

		// Token: 0x040001E5 RID: 485
		private long totalIn;

		// Token: 0x040001E6 RID: 486
		private int inputOff;

		// Token: 0x040001E7 RID: 487
		private int inputEnd;

		// Token: 0x040001E8 RID: 488
		private DeflaterPending pending;

		// Token: 0x040001E9 RID: 489
		private DeflaterHuffman huffman;

		// Token: 0x040001EA RID: 490
		private Adler32 adler;
	}
}
