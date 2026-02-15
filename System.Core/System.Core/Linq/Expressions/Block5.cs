using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000081 RID: 129
	internal sealed class Block5 : BlockExpression
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x00012BB0 File Offset: 0x00010DB0
		internal Block5(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00012BE0 File Offset: 0x00010DE0
		internal override Expression GetExpression(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			case 3:
				return this._arg3;
			case 4:
				return this._arg4;
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00012C3B File Offset: 0x00010E3B
		internal override int ExpressionCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00012C3E File Offset: 0x00010E3E
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012C4C File Offset: 0x00010E4C
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block5(args[0], args[1], args[2], args[3], args[4]);
		}

		// Token: 0x04000126 RID: 294
		private object _arg0;

		// Token: 0x04000127 RID: 295
		private readonly Expression _arg1;

		// Token: 0x04000128 RID: 296
		private readonly Expression _arg2;

		// Token: 0x04000129 RID: 297
		private readonly Expression _arg3;

		// Token: 0x0400012A RID: 298
		private readonly Expression _arg4;
	}
}
