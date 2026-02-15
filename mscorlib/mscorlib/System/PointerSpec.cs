using System;
using System.Text;

namespace System
{
	// Token: 0x020001F4 RID: 500
	internal class PointerSpec : ModifierSpec
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x0004E049 File Offset: 0x0004C249
		internal PointerSpec(int pointer_level)
		{
			this.pointer_level = pointer_level;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004E058 File Offset: 0x0004C258
		public Type Resolve(Type type)
		{
			for (int i = 0; i < this.pointer_level; i++)
			{
				type = type.MakePointerType();
			}
			return type;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004E07F File Offset: 0x0004C27F
		public StringBuilder Append(StringBuilder sb)
		{
			return sb.Append('*', this.pointer_level);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004E08F File Offset: 0x0004C28F
		public override string ToString()
		{
			return this.Append(new StringBuilder()).ToString();
		}

		// Token: 0x04000970 RID: 2416
		private int pointer_level;
	}
}
