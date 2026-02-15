using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A7 RID: 423
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray8 : IBitArray
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002B3E8 File Offset: 0x000295E8
		public uint capacity
		{
			get
			{
				return 8U;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002B3EB File Offset: 0x000295EB
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002B3F6 File Offset: 0x000295F6
		public bool allTrue
		{
			get
			{
				return this.data == byte.MaxValue;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0002B408 File Offset: 0x00029608
		public string humanizedData
		{
			get
			{
				return string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString(this.data, 2)).Replace(' ', '0');
			}
		}

		// Token: 0x17000181 RID: 385
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get8(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set8(index, ref this.data, value);
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002B469 File Offset: 0x00029669
		public BitArray8(byte initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002B474 File Offset: 0x00029674
		public BitArray8(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int index = bitIndexTrue.Count<uint>() - 1; index >= 0; index--)
			{
				uint bitIndex = bitIndexTrue.ElementAt(index);
				if (bitIndex < this.capacity)
				{
					this.data |= (byte)(1 << (int)bitIndex);
				}
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002B4C5 File Offset: 0x000296C5
		public static BitArray8 operator ~(BitArray8 a)
		{
			return new BitArray8(~a.data);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002B4D4 File Offset: 0x000296D4
		public static BitArray8 operator |(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data | b.data);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002B4E9 File Offset: 0x000296E9
		public static BitArray8 operator &(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data & b.data);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002B4FE File Offset: 0x000296FE
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray8)other;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002B516 File Offset: 0x00029716
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray8)other;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002B52E File Offset: 0x0002972E
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002B540 File Offset: 0x00029740
		public static bool operator ==(BitArray8 a, BitArray8 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002B550 File Offset: 0x00029750
		public static bool operator !=(BitArray8 a, BitArray8 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002B564 File Offset: 0x00029764
		public override bool Equals(object obj)
		{
			if (obj is BitArray8)
			{
				BitArray8 ba8 = (BitArray8)obj;
				return ba8.data == this.data;
			}
			return false;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002B590 File Offset: 0x00029790
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x0400082B RID: 2091
		[SerializeField]
		private byte data;
	}
}
