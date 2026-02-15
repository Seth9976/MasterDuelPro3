using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000059 RID: 89
	public class DeflaterHuffman
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		static DeflaterHuffman()
		{
			int i = 0;
			while (i < 144)
			{
				DeflaterHuffman.staticLCodes[i] = DeflaterHuffman.BitReverse(48 + i << 8);
				DeflaterHuffman.staticLLength[i++] = 8;
			}
			while (i < 256)
			{
				DeflaterHuffman.staticLCodes[i] = DeflaterHuffman.BitReverse(256 + i << 7);
				DeflaterHuffman.staticLLength[i++] = 9;
			}
			while (i < 280)
			{
				DeflaterHuffman.staticLCodes[i] = DeflaterHuffman.BitReverse(-256 + i << 9);
				DeflaterHuffman.staticLLength[i++] = 7;
			}
			while (i < 286)
			{
				DeflaterHuffman.staticLCodes[i] = DeflaterHuffman.BitReverse(-88 + i << 8);
				DeflaterHuffman.staticLLength[i++] = 8;
			}
			DeflaterHuffman.staticDCodes = new short[30];
			DeflaterHuffman.staticDLength = new byte[30];
			for (i = 0; i < 30; i++)
			{
				DeflaterHuffman.staticDCodes[i] = DeflaterHuffman.BitReverse(i << 11);
				DeflaterHuffman.staticDLength[i] = 5;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000DBEC File Offset: 0x0000BDEC
		public DeflaterHuffman(DeflaterPending pending)
		{
			this.pending = pending;
			this.literalTree = new DeflaterHuffman.Tree(this, 286, 257, 15);
			this.distTree = new DeflaterHuffman.Tree(this, 30, 1, 15);
			this.blTree = new DeflaterHuffman.Tree(this, 19, 4, 7);
			this.d_buf = new short[16384];
			this.l_buf = new byte[16384];
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000DC5F File Offset: 0x0000BE5F
		public void Reset()
		{
			this.last_lit = 0;
			this.extra_bits = 0;
			this.literalTree.Reset();
			this.distTree.Reset();
			this.blTree.Reset();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000DC90 File Offset: 0x0000BE90
		public void SendAllTrees(int blTreeCodes)
		{
			this.blTree.BuildCodes();
			this.literalTree.BuildCodes();
			this.distTree.BuildCodes();
			this.pending.WriteBits(this.literalTree.numCodes - 257, 5);
			this.pending.WriteBits(this.distTree.numCodes - 1, 5);
			this.pending.WriteBits(blTreeCodes - 4, 4);
			for (int i = 0; i < blTreeCodes; i++)
			{
				this.pending.WriteBits((int)this.blTree.length[DeflaterHuffman.BL_ORDER[i]], 3);
			}
			this.literalTree.WriteTree(this.blTree);
			this.distTree.WriteTree(this.blTree);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000DD50 File Offset: 0x0000BF50
		public void CompressBlock()
		{
			for (int i = 0; i < this.last_lit; i++)
			{
				int num = (int)(this.l_buf[i] & byte.MaxValue);
				int num2 = (int)this.d_buf[i];
				if (num2-- != 0)
				{
					int num3 = DeflaterHuffman.Lcode(num);
					this.literalTree.WriteSymbol(num3);
					int num4 = (num3 - 261) / 4;
					if (num4 > 0 && num4 <= 5)
					{
						this.pending.WriteBits(num & ((1 << num4) - 1), num4);
					}
					int num5 = DeflaterHuffman.Dcode(num2);
					this.distTree.WriteSymbol(num5);
					num4 = num5 / 2 - 1;
					if (num4 > 0)
					{
						this.pending.WriteBits(num2 & ((1 << num4) - 1), num4);
					}
				}
				else
				{
					this.literalTree.WriteSymbol(num);
				}
			}
			this.literalTree.WriteSymbol(256);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public void FlushStoredBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
		{
			this.pending.WriteBits(lastBlock ? 1 : 0, 3);
			this.pending.AlignToByte();
			this.pending.WriteShort(storedLength);
			this.pending.WriteShort(~storedLength);
			this.pending.WriteBlock(stored, storedOffset, storedLength);
			this.Reset();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000DE88 File Offset: 0x0000C088
		public void FlushBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
		{
			short[] freqs = this.literalTree.freqs;
			int num = 256;
			freqs[num] += 1;
			this.literalTree.BuildTree();
			this.distTree.BuildTree();
			this.literalTree.CalcBLFreq(this.blTree);
			this.distTree.CalcBLFreq(this.blTree);
			this.blTree.BuildTree();
			int num2 = 4;
			for (int i = 18; i > num2; i--)
			{
				if (this.blTree.length[DeflaterHuffman.BL_ORDER[i]] > 0)
				{
					num2 = i + 1;
				}
			}
			int num3 = 14 + num2 * 3 + this.blTree.GetEncodedLength() + this.literalTree.GetEncodedLength() + this.distTree.GetEncodedLength() + this.extra_bits;
			int num4 = this.extra_bits;
			for (int j = 0; j < 286; j++)
			{
				num4 += (int)(this.literalTree.freqs[j] * (short)DeflaterHuffman.staticLLength[j]);
			}
			for (int k = 0; k < 30; k++)
			{
				num4 += (int)(this.distTree.freqs[k] * (short)DeflaterHuffman.staticDLength[k]);
			}
			if (num3 >= num4)
			{
				num3 = num4;
			}
			if (storedOffset >= 0 && storedLength + 4 < num3 >> 3)
			{
				this.FlushStoredBlock(stored, storedOffset, storedLength, lastBlock);
				return;
			}
			if (num3 == num4)
			{
				this.pending.WriteBits(2 + (lastBlock ? 1 : 0), 3);
				this.literalTree.SetStaticCodes(DeflaterHuffman.staticLCodes, DeflaterHuffman.staticLLength);
				this.distTree.SetStaticCodes(DeflaterHuffman.staticDCodes, DeflaterHuffman.staticDLength);
				this.CompressBlock();
				this.Reset();
				return;
			}
			this.pending.WriteBits(4 + (lastBlock ? 1 : 0), 3);
			this.SendAllTrees(num2);
			this.CompressBlock();
			this.Reset();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000E046 File Offset: 0x0000C246
		public bool IsFull()
		{
			return this.last_lit >= 16384;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E058 File Offset: 0x0000C258
		public bool TallyLit(int literal)
		{
			this.d_buf[this.last_lit] = 0;
			byte[] array = this.l_buf;
			int num = this.last_lit;
			this.last_lit = num + 1;
			array[num] = (byte)literal;
			short[] freqs = this.literalTree.freqs;
			freqs[literal] += 1;
			return this.IsFull();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		public bool TallyDist(int distance, int length)
		{
			this.d_buf[this.last_lit] = (short)distance;
			byte[] array = this.l_buf;
			int num = this.last_lit;
			this.last_lit = num + 1;
			array[num] = (byte)(length - 3);
			int num2 = DeflaterHuffman.Lcode(length - 3);
			short[] freqs = this.literalTree.freqs;
			int num3 = num2;
			freqs[num3] += 1;
			if (num2 >= 265 && num2 < 285)
			{
				this.extra_bits += (num2 - 261) / 4;
			}
			int num4 = DeflaterHuffman.Dcode(distance - 1);
			short[] freqs2 = this.distTree.freqs;
			int num5 = num4;
			freqs2[num5] += 1;
			if (num4 >= 4)
			{
				this.extra_bits += num4 / 2 - 1;
			}
			return this.IsFull();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E166 File Offset: 0x0000C366
		public static short BitReverse(int toReverse)
		{
			return (short)(((int)DeflaterHuffman.bit4Reverse[toReverse & 15] << 12) | ((int)DeflaterHuffman.bit4Reverse[(toReverse >> 4) & 15] << 8) | ((int)DeflaterHuffman.bit4Reverse[(toReverse >> 8) & 15] << 4) | (int)DeflaterHuffman.bit4Reverse[toReverse >> 12]);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		private static int Lcode(int length)
		{
			if (length == 255)
			{
				return 285;
			}
			int num = 257;
			while (length >= 8)
			{
				num += 4;
				length >>= 1;
			}
			return num + length;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		private static int Dcode(int distance)
		{
			int num = 0;
			while (distance >= 4)
			{
				num += 2;
				distance >>= 1;
			}
			return num + distance;
		}

		// Token: 0x040001EB RID: 491
		private const int BUFSIZE = 16384;

		// Token: 0x040001EC RID: 492
		private const int LITERAL_NUM = 286;

		// Token: 0x040001ED RID: 493
		private const int DIST_NUM = 30;

		// Token: 0x040001EE RID: 494
		private const int BITLEN_NUM = 19;

		// Token: 0x040001EF RID: 495
		private const int REP_3_6 = 16;

		// Token: 0x040001F0 RID: 496
		private const int REP_3_10 = 17;

		// Token: 0x040001F1 RID: 497
		private const int REP_11_138 = 18;

		// Token: 0x040001F2 RID: 498
		private const int EOF_SYMBOL = 256;

		// Token: 0x040001F3 RID: 499
		private static readonly int[] BL_ORDER = new int[]
		{
			16, 17, 18, 0, 8, 7, 9, 6, 10, 5,
			11, 4, 12, 3, 13, 2, 14, 1, 15
		};

		// Token: 0x040001F4 RID: 500
		private static readonly byte[] bit4Reverse = new byte[]
		{
			0, 8, 4, 12, 2, 10, 6, 14, 1, 9,
			5, 13, 3, 11, 7, 15
		};

		// Token: 0x040001F5 RID: 501
		private static short[] staticLCodes = new short[286];

		// Token: 0x040001F6 RID: 502
		private static byte[] staticLLength = new byte[286];

		// Token: 0x040001F7 RID: 503
		private static short[] staticDCodes;

		// Token: 0x040001F8 RID: 504
		private static byte[] staticDLength;

		// Token: 0x040001F9 RID: 505
		public DeflaterPending pending;

		// Token: 0x040001FA RID: 506
		private DeflaterHuffman.Tree literalTree;

		// Token: 0x040001FB RID: 507
		private DeflaterHuffman.Tree distTree;

		// Token: 0x040001FC RID: 508
		private DeflaterHuffman.Tree blTree;

		// Token: 0x040001FD RID: 509
		private short[] d_buf;

		// Token: 0x040001FE RID: 510
		private byte[] l_buf;

		// Token: 0x040001FF RID: 511
		private int last_lit;

		// Token: 0x04000200 RID: 512
		private int extra_bits;

		// Token: 0x0200005A RID: 90
		private class Tree
		{
			// Token: 0x060002D0 RID: 720 RVA: 0x0000E1F5 File Offset: 0x0000C3F5
			public Tree(DeflaterHuffman dh, int elems, int minCodes, int maxLength)
			{
				this.dh = dh;
				this.minNumCodes = minCodes;
				this.maxLength = maxLength;
				this.freqs = new short[elems];
				this.bl_counts = new int[maxLength];
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000E22C File Offset: 0x0000C42C
			public void Reset()
			{
				for (int i = 0; i < this.freqs.Length; i++)
				{
					this.freqs[i] = 0;
				}
				this.codes = null;
				this.length = null;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x0000E263 File Offset: 0x0000C463
			public void WriteSymbol(int code)
			{
				this.dh.pending.WriteBits((int)this.codes[code] & 65535, (int)this.length[code]);
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000E28C File Offset: 0x0000C48C
			public void CheckEmpty()
			{
				bool flag = true;
				for (int i = 0; i < this.freqs.Length; i++)
				{
					flag &= this.freqs[i] == 0;
				}
				if (!flag)
				{
					throw new SharpZipBaseException("!Empty");
				}
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0000E2CA File Offset: 0x0000C4CA
			public void SetStaticCodes(short[] staticCodes, byte[] staticLengths)
			{
				this.codes = staticCodes;
				this.length = staticLengths;
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x0000E2DC File Offset: 0x0000C4DC
			public void BuildCodes()
			{
				int num = this.freqs.Length;
				int[] array = new int[this.maxLength];
				int num2 = 0;
				this.codes = new short[this.freqs.Length];
				for (int i = 0; i < this.maxLength; i++)
				{
					array[i] = num2;
					num2 += this.bl_counts[i] << 15 - i;
				}
				for (int j = 0; j < this.numCodes; j++)
				{
					int num3 = (int)this.length[j];
					if (num3 > 0)
					{
						this.codes[j] = DeflaterHuffman.BitReverse(array[num3 - 1]);
						array[num3 - 1] += 1 << 16 - num3;
					}
				}
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x0000E388 File Offset: 0x0000C588
			public void BuildTree()
			{
				int num = this.freqs.Length;
				int[] array = new int[num];
				int i = 0;
				int num2 = 0;
				for (int j = 0; j < num; j++)
				{
					int num3 = (int)this.freqs[j];
					if (num3 != 0)
					{
						int num4 = i++;
						int num5;
						while (num4 > 0 && (int)this.freqs[array[num5 = (num4 - 1) / 2]] > num3)
						{
							array[num4] = array[num5];
							num4 = num5;
						}
						array[num4] = j;
						num2 = j;
					}
				}
				while (i < 2)
				{
					int num6 = ((num2 < 2) ? (++num2) : 0);
					array[i++] = num6;
				}
				this.numCodes = Math.Max(num2 + 1, this.minNumCodes);
				int num7 = i;
				int[] array2 = new int[4 * i - 2];
				int[] array3 = new int[2 * i - 1];
				int num8 = num7;
				for (int k = 0; k < i; k++)
				{
					int num9 = array[k];
					array2[2 * k] = num9;
					array2[2 * k + 1] = -1;
					array3[k] = (int)this.freqs[num9] << 8;
					array[k] = k;
				}
				do
				{
					int num10 = array[0];
					int num11 = array[--i];
					int num12 = 0;
					int l;
					for (l = 1; l < i; l = l * 2 + 1)
					{
						if (l + 1 < i && array3[array[l]] > array3[array[l + 1]])
						{
							l++;
						}
						array[num12] = array[l];
						num12 = l;
					}
					int num13 = array3[num11];
					while ((l = num12) > 0 && array3[array[num12 = (l - 1) / 2]] > num13)
					{
						array[l] = array[num12];
					}
					array[l] = num11;
					int num14 = array[0];
					num11 = num8++;
					array2[2 * num11] = num10;
					array2[2 * num11 + 1] = num14;
					int num15 = Math.Min(array3[num10] & 255, array3[num14] & 255);
					num13 = (array3[num11] = array3[num10] + array3[num14] - num15 + 1);
					num12 = 0;
					for (l = 1; l < i; l = num12 * 2 + 1)
					{
						if (l + 1 < i && array3[array[l]] > array3[array[l + 1]])
						{
							l++;
						}
						array[num12] = array[l];
						num12 = l;
					}
					while ((l = num12) > 0 && array3[array[num12 = (l - 1) / 2]] > num13)
					{
						array[l] = array[num12];
					}
					array[l] = num11;
				}
				while (i > 1);
				if (array[0] != array2.Length / 2 - 1)
				{
					throw new SharpZipBaseException("Heap invariant violated");
				}
				this.BuildLength(array2);
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
			public int GetEncodedLength()
			{
				int num = 0;
				for (int i = 0; i < this.freqs.Length; i++)
				{
					num += (int)(this.freqs[i] * (short)this.length[i]);
				}
				return num;
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000E62C File Offset: 0x0000C82C
			public void CalcBLFreq(DeflaterHuffman.Tree blTree)
			{
				int num = -1;
				int i = 0;
				while (i < this.numCodes)
				{
					int num2 = 1;
					int num3 = (int)this.length[i];
					int num4;
					int num5;
					if (num3 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else
					{
						num4 = 6;
						num5 = 3;
						if (num != num3)
						{
							short[] array = blTree.freqs;
							int num6 = num3;
							array[num6] += 1;
							num2 = 0;
						}
					}
					num = num3;
					i++;
					while (i < this.numCodes && num == (int)this.length[i])
					{
						i++;
						if (++num2 >= num4)
						{
							break;
						}
					}
					if (num2 < num5)
					{
						short[] array2 = blTree.freqs;
						int num7 = num;
						array2[num7] += (short)num2;
					}
					else if (num != 0)
					{
						short[] array3 = blTree.freqs;
						int num8 = 16;
						array3[num8] += 1;
					}
					else if (num2 <= 10)
					{
						short[] array4 = blTree.freqs;
						int num9 = 17;
						array4[num9] += 1;
					}
					else
					{
						short[] array5 = blTree.freqs;
						int num10 = 18;
						array5[num10] += 1;
					}
				}
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x0000E718 File Offset: 0x0000C918
			public void WriteTree(DeflaterHuffman.Tree blTree)
			{
				int num = -1;
				int i = 0;
				while (i < this.numCodes)
				{
					int num2 = 1;
					int num3 = (int)this.length[i];
					int num4;
					int num5;
					if (num3 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else
					{
						num4 = 6;
						num5 = 3;
						if (num != num3)
						{
							blTree.WriteSymbol(num3);
							num2 = 0;
						}
					}
					num = num3;
					i++;
					while (i < this.numCodes && num == (int)this.length[i])
					{
						i++;
						if (++num2 >= num4)
						{
							break;
						}
					}
					if (num2 < num5)
					{
						while (num2-- > 0)
						{
							blTree.WriteSymbol(num);
						}
					}
					else if (num != 0)
					{
						blTree.WriteSymbol(16);
						this.dh.pending.WriteBits(num2 - 3, 2);
					}
					else if (num2 <= 10)
					{
						blTree.WriteSymbol(17);
						this.dh.pending.WriteBits(num2 - 3, 3);
					}
					else
					{
						blTree.WriteSymbol(18);
						this.dh.pending.WriteBits(num2 - 11, 7);
					}
				}
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000E814 File Offset: 0x0000CA14
			private void BuildLength(int[] childs)
			{
				this.length = new byte[this.freqs.Length];
				int num = childs.Length / 2;
				int num2 = (num + 1) / 2;
				int num3 = 0;
				for (int i = 0; i < this.maxLength; i++)
				{
					this.bl_counts[i] = 0;
				}
				int[] array = new int[num];
				array[num - 1] = 0;
				for (int j = num - 1; j >= 0; j--)
				{
					if (childs[2 * j + 1] != -1)
					{
						int num4 = array[j] + 1;
						if (num4 > this.maxLength)
						{
							num4 = this.maxLength;
							num3++;
						}
						array[childs[2 * j]] = (array[childs[2 * j + 1]] = num4);
					}
					else
					{
						int num5 = array[j];
						this.bl_counts[num5 - 1]++;
						this.length[childs[2 * j]] = (byte)array[j];
					}
				}
				if (num3 == 0)
				{
					return;
				}
				int num6 = this.maxLength - 1;
				for (;;)
				{
					if (this.bl_counts[--num6] != 0)
					{
						do
						{
							this.bl_counts[num6]--;
							this.bl_counts[++num6]++;
							num3 -= 1 << this.maxLength - 1 - num6;
						}
						while (num3 > 0 && num6 < this.maxLength - 1);
						if (num3 <= 0)
						{
							break;
						}
					}
				}
				this.bl_counts[this.maxLength - 1] += num3;
				this.bl_counts[this.maxLength - 2] -= num3;
				int num7 = 2 * num2;
				for (int num8 = this.maxLength; num8 != 0; num8--)
				{
					int k = this.bl_counts[num8 - 1];
					while (k > 0)
					{
						int num9 = 2 * childs[num7++];
						if (childs[num9 + 1] == -1)
						{
							this.length[childs[num9]] = (byte)num8;
							k--;
						}
					}
				}
			}

			// Token: 0x04000201 RID: 513
			public short[] freqs;

			// Token: 0x04000202 RID: 514
			public byte[] length;

			// Token: 0x04000203 RID: 515
			public int minNumCodes;

			// Token: 0x04000204 RID: 516
			public int numCodes;

			// Token: 0x04000205 RID: 517
			private short[] codes;

			// Token: 0x04000206 RID: 518
			private readonly int[] bl_counts;

			// Token: 0x04000207 RID: 519
			private readonly int maxLength;

			// Token: 0x04000208 RID: 520
			private DeflaterHuffman dh;
		}
	}
}
