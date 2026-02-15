using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x0200065B RID: 1627
	[StructLayout(LayoutKind.Sequential)]
	internal class ArrayType : SymbolType
	{
		// Token: 0x06003174 RID: 12660 RVA: 0x000BA786 File Offset: 0x000B8986
		internal ArrayType(Type elementType, int rank)
			: base(elementType)
		{
			this.rank = rank;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000BA798 File Offset: 0x000B8998
		internal override Type InternalResolve()
		{
			Type type = this.m_baseType.InternalResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000BA7CC File Offset: 0x000B89CC
		internal override Type RuntimeResolve()
		{
			Type type = this.m_baseType.RuntimeResolve();
			if (this.rank == 0)
			{
				return type.MakeArrayType();
			}
			return type.MakeArrayType(this.rank);
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x0000C091 File Offset: 0x0000A291
		protected override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x000BA800 File Offset: 0x000B8A00
		public override int GetArrayRank()
		{
			if (this.rank != 0)
			{
				return this.rank;
			}
			return 1;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000BA814 File Offset: 0x000B8A14
		internal override string FormatName(string elementName)
		{
			if (elementName == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(elementName);
			stringBuilder.Append("[");
			for (int i = 1; i < this.rank; i++)
			{
				stringBuilder.Append(",");
			}
			if (this.rank == 1)
			{
				stringBuilder.Append("*");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001937 RID: 6455
		private int rank;
	}
}
