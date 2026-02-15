using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020F RID: 527
	internal sealed class BitSet
	{
		// Token: 0x06001A4A RID: 6730 RVA: 0x00002127 File Offset: 0x00000327
		private BitSet()
		{
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00099520 File Offset: 0x00097720
		public BitSet(int count)
		{
			this.count = count;
			this.bits = new uint[this.Subscript(count + 31)];
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00099544 File Offset: 0x00097744
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170005E6 RID: 1510
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00099558 File Offset: 0x00097758
		public void Clear()
		{
			int num = this.bits.Length;
			while (num-- > 0)
			{
				this.bits[num] = 0U;
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00099584 File Offset: 0x00097784
		public void Set(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] |= 1U << index;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000995BC File Offset: 0x000977BC
		public bool Get(int index)
		{
			bool flag = false;
			if (index < this.count)
			{
				int num = this.Subscript(index);
				flag = ((ulong)this.bits[num] & (ulong)(1L << (index & 31 & 31))) > 0UL;
			}
			return flag;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000995F8 File Offset: 0x000977F8
		public int NextSet(int startFrom)
		{
			int num = startFrom + 1;
			if (num == this.count)
			{
				return -1;
			}
			int num2 = this.Subscript(num);
			num &= 31;
			uint num3;
			for (num3 = this.bits[num2] >> num; num3 == 0U; num3 = this.bits[num2])
			{
				if (++num2 == this.bits.Length)
				{
					return -1;
				}
				num = 0;
			}
			while ((num3 & 1U) == 0U)
			{
				num3 >>= 1;
				num++;
			}
			return (num2 << 5) + num;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00099664 File Offset: 0x00097864
		public void And(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = this.bits.Length;
			int num2 = other.bits.Length;
			int i = ((num > num2) ? num2 : num);
			int num3 = i;
			while (num3-- > 0)
			{
				this.bits[num3] &= other.bits[num3];
			}
			while (i < num)
			{
				this.bits[i] = 0U;
				i++;
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000996C8 File Offset: 0x000978C8
		public void Or(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = other.bits.Length;
			this.EnsureLength(num);
			int num2 = num;
			while (num2-- > 0)
			{
				this.bits[num2] |= other.bits[num2];
			}
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00099710 File Offset: 0x00097910
		public override int GetHashCode()
		{
			int num = 1234;
			int num2 = this.bits.Length;
			while (--num2 >= 0)
			{
				num ^= (int)(this.bits[num2] * (uint)(num2 + 1));
			}
			return num ^ num;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00099748 File Offset: 0x00097948
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			BitSet bitSet = (BitSet)obj;
			int num = this.bits.Length;
			int num2 = bitSet.bits.Length;
			int num3 = ((num > num2) ? num2 : num);
			int num4 = num3;
			while (num4-- > 0)
			{
				if (this.bits[num4] != bitSet.bits[num4])
				{
					return false;
				}
			}
			if (num > num3)
			{
				int num5 = num;
				while (num5-- > num3)
				{
					if (this.bits[num5] != 0U)
					{
						return false;
					}
				}
			}
			else
			{
				int num6 = num2;
				while (num6-- > num3)
				{
					if (bitSet.bits[num6] != 0U)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000997E9 File Offset: 0x000979E9
		public BitSet Clone()
		{
			return new BitSet
			{
				count = this.count,
				bits = (uint[])this.bits.Clone()
			};
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x00099814 File Offset: 0x00097A14
		public bool IsEmpty
		{
			get
			{
				uint num = 0U;
				for (int i = 0; i < this.bits.Length; i++)
				{
					num |= this.bits[i];
				}
				return num == 0U;
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00099848 File Offset: 0x00097A48
		public bool Intersects(BitSet other)
		{
			int num = Math.Min(this.bits.Length, other.bits.Length);
			while (--num >= 0)
			{
				if ((this.bits[num] & other.bits[num]) != 0U)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0009988B File Offset: 0x00097A8B
		private int Subscript(int bitIndex)
		{
			return bitIndex >> 5;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00099890 File Offset: 0x00097A90
		private void EnsureLength(int nRequiredLength)
		{
			if (nRequiredLength > this.bits.Length)
			{
				int num = 2 * this.bits.Length;
				if (num < nRequiredLength)
				{
					num = nRequiredLength;
				}
				uint[] array = new uint[num];
				Array.Copy(this.bits, array, this.bits.Length);
				this.bits = array;
			}
		}

		// Token: 0x04000B30 RID: 2864
		private int count;

		// Token: 0x04000B31 RID: 2865
		private uint[] bits;
	}
}
