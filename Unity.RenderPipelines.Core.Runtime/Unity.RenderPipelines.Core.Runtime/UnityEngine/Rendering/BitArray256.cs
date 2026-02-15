using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x020001AC RID: 428
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray256 : IBitArray
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002BE4D File Offset: 0x0002A04D
		public uint capacity
		{
			get
			{
				return 256U;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002BE54 File Offset: 0x0002A054
		public bool allFalse
		{
			get
			{
				return this.data1 == 0UL && this.data2 == 0UL && this.data3 == 0UL && this.data4 == 0UL;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0002BE7A File Offset: 0x0002A07A
		public bool allTrue
		{
			get
			{
				return this.data1 == ulong.MaxValue && this.data2 == ulong.MaxValue && this.data3 == ulong.MaxValue && this.data4 == ulong.MaxValue;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data4, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data3, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data2, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data1, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x1700019B RID: 411
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get256(index, this.data1, this.data2, this.data3, this.data4);
			}
			set
			{
				BitArrayUtilities.Set256(index, ref this.data1, ref this.data2, ref this.data3, ref this.data4, value);
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002C00A File Offset: 0x0002A20A
		public BitArray256(ulong initValue1, ulong initValue2, ulong initValue3, ulong initValue4)
		{
			this.data1 = initValue1;
			this.data2 = initValue2;
			this.data3 = initValue3;
			this.data4 = initValue4;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002C02C File Offset: 0x0002A22C
		public BitArray256(IEnumerable<uint> bitIndexTrue)
		{
			this.data1 = (this.data2 = (this.data3 = (this.data4 = 0UL)));
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int index = bitIndexTrue.Count<uint>() - 1; index >= 0; index--)
			{
				uint bitIndex = bitIndexTrue.ElementAt(index);
				if (bitIndex < 64U)
				{
					this.data1 |= 1UL << (int)bitIndex;
				}
				else if (bitIndex < 128U)
				{
					this.data2 |= 1UL << (int)(bitIndex - 64U);
				}
				else if (bitIndex < 192U)
				{
					this.data3 |= 1UL << (int)(bitIndex - 128U);
				}
				else if (bitIndex < this.capacity)
				{
					this.data4 |= 1UL << (int)(bitIndex - 192U);
				}
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002C104 File Offset: 0x0002A304
		public static BitArray256 operator ~(BitArray256 a)
		{
			return new BitArray256(~a.data1, ~a.data2, ~a.data3, ~a.data4);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002C127 File Offset: 0x0002A327
		public static BitArray256 operator |(BitArray256 a, BitArray256 b)
		{
			return new BitArray256(a.data1 | b.data1, a.data2 | b.data2, a.data3 | b.data3, a.data4 | b.data4);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002C162 File Offset: 0x0002A362
		public static BitArray256 operator &(BitArray256 a, BitArray256 b)
		{
			return new BitArray256(a.data1 & b.data1, a.data2 & b.data2, a.data3 & b.data3, a.data4 & b.data4);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002C19D File Offset: 0x0002A39D
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray256)other;
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002C1B5 File Offset: 0x0002A3B5
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray256)other;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002C1CD File Offset: 0x0002A3CD
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002C1DF File Offset: 0x0002A3DF
		public static bool operator ==(BitArray256 a, BitArray256 b)
		{
			return a.data1 == b.data1 && a.data2 == b.data2 && a.data3 == b.data3 && a.data4 == b.data4;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002C21B File Offset: 0x0002A41B
		public static bool operator !=(BitArray256 a, BitArray256 b)
		{
			return a.data1 != b.data1 || a.data2 != b.data2 || a.data3 != b.data3 || a.data4 != b.data4;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002C25C File Offset: 0x0002A45C
		public override bool Equals(object obj)
		{
			if (obj is BitArray256)
			{
				BitArray256 ba256 = (BitArray256)obj;
				if (this.data1.Equals(ba256.data1) && this.data2.Equals(ba256.data2) && this.data3.Equals(ba256.data3))
				{
					return this.data4.Equals(ba256.data4);
				}
			}
			return false;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
		public override int GetHashCode()
		{
			return (((1870826326 * -1521134295 + this.data1.GetHashCode()) * -1521134295 + this.data2.GetHashCode()) * -1521134295 + this.data3.GetHashCode()) * -1521134295 + this.data4.GetHashCode();
		}

		// Token: 0x04000831 RID: 2097
		[SerializeField]
		private ulong data1;

		// Token: 0x04000832 RID: 2098
		[SerializeField]
		private ulong data2;

		// Token: 0x04000833 RID: 2099
		[SerializeField]
		private ulong data3;

		// Token: 0x04000834 RID: 2100
		[SerializeField]
		private ulong data4;
	}
}
