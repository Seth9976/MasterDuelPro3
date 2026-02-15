using System;
using System.Text;

namespace System
{
	// Token: 0x020001F3 RID: 499
	internal class ArraySpec : ModifierSpec
	{
		// Token: 0x06001322 RID: 4898 RVA: 0x0004DFAF File Offset: 0x0004C1AF
		internal ArraySpec(int dimensions, bool bound)
		{
			this.dimensions = dimensions;
			this.bound = bound;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004DFC5 File Offset: 0x0004C1C5
		public Type Resolve(Type type)
		{
			if (this.bound)
			{
				return type.MakeArrayType(1);
			}
			if (this.dimensions == 1)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.dimensions);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004DFF3 File Offset: 0x0004C1F3
		public StringBuilder Append(StringBuilder sb)
		{
			if (this.bound)
			{
				return sb.Append("[*]");
			}
			return sb.Append('[').Append(',', this.dimensions - 1).Append(']');
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004E027 File Offset: 0x0004C227
		public override string ToString()
		{
			return this.Append(new StringBuilder()).ToString();
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004E039 File Offset: 0x0004C239
		public int Rank
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0004E041 File Offset: 0x0004C241
		public bool IsBound
		{
			get
			{
				return this.bound;
			}
		}

		// Token: 0x0400096E RID: 2414
		private int dimensions;

		// Token: 0x0400096F RID: 2415
		private bool bound;
	}
}
