using System;

namespace System.Linq.Expressions
{
	// Token: 0x020000D0 RID: 208
	internal class TypedParameterExpression : ParameterExpression
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00016CDD File Offset: 0x00014EDD
		internal TypedParameterExpression(Type type, string name)
			: base(name)
		{
			this.Type = type;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00016CED File Offset: 0x00014EED
		public sealed override Type Type { get; }
	}
}
