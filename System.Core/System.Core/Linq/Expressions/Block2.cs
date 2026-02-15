using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200007E RID: 126
	internal sealed class Block2 : BlockExpression
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x00012A43 File Offset: 0x00010C43
		internal Block2(Expression arg0, Expression arg1)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00012A59 File Offset: 0x00010C59
		internal override Expression GetExpression(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return this._arg1;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00012A81 File Offset: 0x00010C81
		internal override int ExpressionCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00012A84 File Offset: 0x00010C84
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00012A92 File Offset: 0x00010C92
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block2(args[0], args[1]);
		}

		// Token: 0x0400011D RID: 285
		private object _arg0;

		// Token: 0x0400011E RID: 286
		private readonly Expression _arg1;
	}
}
