using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000080 RID: 128
	internal sealed class Block4 : BlockExpression
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x00012B17 File Offset: 0x00010D17
		internal Block4(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00012B3C File Offset: 0x00010D3C
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
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00012B8C File Offset: 0x00010D8C
		internal override int ExpressionCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012B8F File Offset: 0x00010D8F
		internal override ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			return BlockExpression.ReturnReadOnlyExpressions(this, ref this._arg0);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00012B9D File Offset: 0x00010D9D
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			return new Block4(args[0], args[1], args[2], args[3]);
		}

		// Token: 0x04000122 RID: 290
		private object _arg0;

		// Token: 0x04000123 RID: 291
		private readonly Expression _arg1;

		// Token: 0x04000124 RID: 292
		private readonly Expression _arg2;

		// Token: 0x04000125 RID: 293
		private readonly Expression _arg3;
	}
}
