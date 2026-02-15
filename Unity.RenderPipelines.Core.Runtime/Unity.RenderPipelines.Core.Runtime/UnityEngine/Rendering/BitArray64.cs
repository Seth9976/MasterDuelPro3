using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x020001AA RID: 426
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray64 : IBitArray
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0002B94B File Offset: 0x00029B4B
		public uint capacity
		{
			get
			{
				return 64U;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002B94F File Offset: 0x00029B4F
		public bool allFalse
		{
			get
			{
				return this.data == 0UL;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0002B95B File Offset: 0x00029B5B
		public bool allTrue
		{
			get
			{
				return this.data == ulong.MaxValue;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002B968 File Offset: 0x00029B68
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((long)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x17000191 RID: 401
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get64(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set64(index, ref this.data, value);
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002B9DF File Offset: 0x00029BDF
		public BitArray64(ulong initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002B9E8 File Offset: 0x00029BE8
		public BitArray64(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0UL;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int index = bitIndexTrue.Count<uint>() - 1; index >= 0; index--)
			{
				uint bitIndex = bitIndexTrue.ElementAt(index);
				if (bitIndex < this.capacity)
				{
					this.data |= 1UL << (int)bitIndex;
				}
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002BA39 File Offset: 0x00029C39
		public static BitArray64 operator ~(BitArray64 a)
		{
			return new BitArray64(~a.data);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002BA47 File Offset: 0x00029C47
		public static BitArray64 operator |(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data | b.data);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002BA5B File Offset: 0x00029C5B
		public static BitArray64 operator &(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data & b.data);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002BA6F File Offset: 0x00029C6F
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray64)other;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002BA87 File Offset: 0x00029C87
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray64)other;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002BA9F File Offset: 0x00029C9F
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002BAB1 File Offset: 0x00029CB1
		public static bool operator ==(BitArray64 a, BitArray64 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002BAC1 File Offset: 0x00029CC1
		public static bool operator !=(BitArray64 a, BitArray64 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public override bool Equals(object obj)
		{
			if (obj is BitArray64)
			{
				BitArray64 ba64 = (BitArray64)obj;
				return ba64.data == this.data;
			}
			return false;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002BB00 File Offset: 0x00029D00
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x0400082E RID: 2094
		[SerializeField]
		private ulong data;
	}
}
