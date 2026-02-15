using System;

namespace System.Linq.Expressions
{
	// Token: 0x020000CF RID: 207
	internal sealed class ByRefParameterExpression : TypedParameterExpression
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x00016CD3 File Offset: 0x00014ED3
		internal ByRefParameterExpression(Type type, string name)
			: base(type, name)
		{
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00009F9F File Offset: 0x0000819F
		internal override bool GetIsByRef()
		{
			return true;
		}
	}
}
