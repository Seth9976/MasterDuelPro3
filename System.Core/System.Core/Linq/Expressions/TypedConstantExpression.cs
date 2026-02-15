using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000091 RID: 145
	internal class TypedConstantExpression : ConstantExpression
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0001328F File Offset: 0x0001148F
		internal TypedConstantExpression(object value, Type type)
			: base(value)
		{
			this.Type = type;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001329F File Offset: 0x0001149F
		public sealed override Type Type { get; }
	}
}
