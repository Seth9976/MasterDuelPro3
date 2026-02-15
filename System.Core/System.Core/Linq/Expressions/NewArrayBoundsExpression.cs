using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x020000CC RID: 204
	internal sealed class NewArrayBoundsExpression : NewArrayExpression
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00016AA6 File Offset: 0x00014CA6
		internal NewArrayBoundsExpression(Type type, ReadOnlyCollection<Expression> expressions)
			: base(type, expressions)
		{
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00016AB4 File Offset: 0x00014CB4
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayBounds;
			}
		}
	}
}
