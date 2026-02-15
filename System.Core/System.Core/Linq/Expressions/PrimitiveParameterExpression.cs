using System;

namespace System.Linq.Expressions
{
	// Token: 0x020000D1 RID: 209
	internal sealed class PrimitiveParameterExpression<T> : ParameterExpression
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x00016CF5 File Offset: 0x00014EF5
		internal PrimitiveParameterExpression(string name)
			: base(name)
		{
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00015C90 File Offset: 0x00013E90
		public sealed override Type Type
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
