using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A9 RID: 425
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray32 : IBitArray
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002B773 File Offset: 0x00029973
		public uint capacity
		{
			get
			{
				return 32U;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0002B777 File Offset: 0x00029977
		public bool allFalse
		{
			get
			{
				return this.data == 0U;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0002B782 File Offset: 0x00029982
		public bool allTrue
		{
			get
			{
				return this.data == uint.MaxValue;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002B78D File Offset: 0x0002998D
		private string humanizedVersion
		{
			get
			{
				return Convert.ToString((long)((ulong)this.data), 2);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002B79C File Offset: 0x0002999C
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((long)((ulong)this.data), 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x1700018C RID: 396
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get32(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set32(index, ref this.data, value);
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002B814 File Offset: 0x00029A14
		public BitArray32(uint initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002B820 File Offset: 0x00029A20
		public BitArray32(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0U;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int index = bitIndexTrue.Count<uint>() - 1; index >= 0; index--)
			{
				uint bitIndex = bitIndexTrue.ElementAt(index);
				if (bitIndex < this.capacity)
				{
					this.data |= 1U << (int)bitIndex;
				}
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002B86F File Offset: 0x00029A6F
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray32)other;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002B887 File Offset: 0x00029A87
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray32)other;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002B89F File Offset: 0x00029A9F
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002B8B1 File Offset: 0x00029AB1
		public static BitArray32 operator ~(BitArray32 a)
		{
			return new BitArray32(~a.data);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002B8BF File Offset: 0x00029ABF
		public static BitArray32 operator |(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data | b.data);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002B8D3 File Offset: 0x00029AD3
		public static BitArray32 operator &(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data & b.data);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002B8E7 File Offset: 0x00029AE7
		public static bool operator ==(BitArray32 a, BitArray32 b)
		{
			return a.data == b.data;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002B8F7 File Offset: 0x00029AF7
		public static bool operator !=(BitArray32 a, BitArray32 b)
		{
			return a.data != b.data;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002B90C File Offset: 0x00029B0C
		public override bool Equals(object obj)
		{
			if (obj is BitArray32)
			{
				BitArray32 ba32 = (BitArray32)obj;
				return ba32.data == this.data;
			}
			return false;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002B938 File Offset: 0x00029B38
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x0400082D RID: 2093
		[SerializeField]
		private uint data;
	}
}
