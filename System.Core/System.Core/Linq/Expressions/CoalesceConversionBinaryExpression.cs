using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200005D RID: 93
	internal sealed class CoalesceConversionBinaryExpression : BinaryExpression
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000CA5A File Offset: 0x0000AC5A
		internal CoalesceConversionBinaryExpression(Expression left, Expression right, LambdaExpression conversion)
			: base(left, right)
		{
			this._conversion = conversion;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000CA73 File Offset: 0x0000AC73
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Coalesce;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000CA76 File Offset: 0x0000AC76
		public sealed override Type Type
		{
			get
			{
				return base.Right.Type;
			}
		}

		// Token: 0x04000113 RID: 275
		private readonly LambdaExpression _conversion;
	}
}
