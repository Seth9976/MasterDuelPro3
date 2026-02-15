using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200007F RID: 127
	internal sealed class Block3 : BlockExpression
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00012A9F File Offset: 0x00010C9F
		internal Block3(Expression arg0, Expression arg1, Expression arg2)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00012ABC File Offset: 0x00010CBC
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
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00012AF6 File Offset: 0x00010CF6
		internal override int ExpressionCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00012AF9 File Offset: 0x00010CF9
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00012B07 File Offset: 0x00010D07
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block3(args[0], args[1], args[2]);
		}

		// Token: 0x0400011F RID: 287
		private object _arg0;

		// Token: 0x04000120 RID: 288
		private readonly Expression _arg1;

		// Token: 0x04000121 RID: 289
		private readonly Expression _arg2;
	}
}
