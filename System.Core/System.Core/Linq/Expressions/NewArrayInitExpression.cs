using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x020000CB RID: 203
	internal sealed class NewArrayInitExpression : NewArrayExpression
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x00016AA6 File Offset: 0x00014CA6
		internal NewArrayInitExpression(Type type, ReadOnlyCollection<Expression> expressions)
			: base(type, expressions)
		{
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayInit;
			}
		}
	}
}
