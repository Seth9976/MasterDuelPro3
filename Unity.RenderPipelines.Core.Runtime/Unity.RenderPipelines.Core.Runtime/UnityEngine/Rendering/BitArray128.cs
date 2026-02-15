using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x020001AB RID: 427
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray128 : IBitArray
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00013844 File Offset: 0x00011A44
		public uint capacity
		{
			get
			{
				return 128U;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0002BB13 File Offset: 0x00029D13
		public bool allFalse
		{
			get
			{
				return this.data1 == 0UL && this.data2 == 0UL;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0002BB29 File Offset: 0x00029D29
		public bool allTrue
		{
			get
			{
				return this.data1 == ulong.MaxValue && this.data2 == ulong.MaxValue;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0002BB44 File Offset: 0x00029D44
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data2, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data1, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x17000196 RID: 406
		public bool this[uint index]
		{
			get
			{
				if (index >= 64U)
				{
					return (this.data2 & (1UL << (int)(index - 64U))) > 0UL;
				}
				return (this.data1 & (1UL << (int)index)) > 0UL;
			}
			set
			{
				if (index < 64U)
				{
					this.data1 = (value ? (this.data1 | (1UL << (int)index)) : (this.data1 & ~(1UL << (int)index)));
					return;
				}
				this.data2 = (value ? (this.data2 | (1UL << (int)(index - 64U))) : (this.data2 & ~(1UL << (int)(index - 64U))));
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002BC79 File Offset: 0x00029E79
		public BitArray128(ulong initValue1, ulong initValue2)
		{
			this.data1 = initValue1;
			this.data2 = initValue2;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002BC8C File Offset: 0x00029E8C
		public BitArray128(IEnumerable<uint> bitIndexTrue)
		{
			this.data1 = (this.data2 = 0UL);
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
				else if (bitIndex < this.capacity)
				{
					this.data2 |= 1UL << (int)(bitIndex - 64U);
				}
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002BD04 File Offset: 0x00029F04
		public static BitArray128 operator ~(BitArray128 a)
		{
			return new BitArray128(~a.data1, ~a.data2);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002BD19 File Offset: 0x00029F19
		public static BitArray128 operator |(BitArray128 a, BitArray128 b)
		{
			return new BitArray128(a.data1 | b.data1, a.data2 | b.data2);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002BD3A File Offset: 0x00029F3A
		public static BitArray128 operator &(BitArray128 a, BitArray128 b)
		{
			return new BitArray128(a.data1 & b.data1, a.data2 & b.data2);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002BD5B File Offset: 0x00029F5B
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray128)other;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002BD73 File Offset: 0x00029F73
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray128)other;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002BD8B File Offset: 0x00029F8B
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002BD9D File Offset: 0x00029F9D
		public static bool operator ==(BitArray128 a, BitArray128 b)
		{
			return a.data1 == b.data1 && a.data2 == b.data2;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002BDBD File Offset: 0x00029FBD
		public static bool operator !=(BitArray128 a, BitArray128 b)
		{
			return a.data1 != b.data1 || a.data2 != b.data2;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		public override bool Equals(object obj)
		{
			if (obj is BitArray128)
			{
				BitArray128 ba128 = (BitArray128)obj;
				if (this.data1.Equals(ba128.data1))
				{
					return this.data2.Equals(ba128.data2);
				}
			}
			return false;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002BE22 File Offset: 0x0002A022
		public override int GetHashCode()
		{
			return (1755735569 * -1521134295 + this.data1.GetHashCode()) * -1521134295 + this.data2.GetHashCode();
		}

		// Token: 0x0400082F RID: 2095
		[SerializeField]
		private ulong data1;

		// Token: 0x04000830 RID: 2096
		[SerializeField]
		private ulong data2;
	}
}
