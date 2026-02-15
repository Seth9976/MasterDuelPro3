using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A8 RID: 424
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray16 : IBitArray
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002B5A3 File Offset: 0x000297A3
		public uint capacity
		{
			get
			{
				return 16U;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002B5A7 File Offset: 0x000297A7
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002B5B2 File Offset: 0x000297B2
		public bool allTrue
		{
			get
			{
				return this.data == ushort.MaxValue;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002B5C4 File Offset: 0x000297C4
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((int)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x17000186 RID: 390
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get16(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set16(index, ref this.data, value);
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002B63B File Offset: 0x0002983B
		public BitArray16(ushort initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002B644 File Offset: 0x00029844
		public BitArray16(IEnumerable<uint> bitIndexTrue)
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
					this.data |= (ushort)(1 << (int)bitIndex);
				}
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002B695 File Offset: 0x00029895
		public static BitArray16 operator ~(BitArray16 a)
		{
			return new BitArray16(~a.data);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public static BitArray16 operator |(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data | b.data);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002B6B9 File Offset: 0x000298B9
		public static BitArray16 operator &(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data & b.data);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002B6CE File Offset: 0x000298CE
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray16)other;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002B6E6 File Offset: 0x000298E6
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray16)other;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002B6FE File Offset: 0x000298FE
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002B710 File Offset: 0x00029910
		public static bool operator ==(BitArray16 a, BitArray16 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002B720 File Offset: 0x00029920
		public static bool operator !=(BitArray16 a, BitArray16 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002B734 File Offset: 0x00029934
		public override bool Equals(object obj)
		{
			if (obj is BitArray16)
			{
				BitArray16 ba16 = (BitArray16)obj;
				return ba16.data == this.data;
			}
			return false;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002B760 File Offset: 0x00029960
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x0400082C RID: 2092
		[SerializeField]
		private ushort data;
	}
}
