using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000AD RID: 173
	internal sealed class Expression2<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x00015DD7 File Offset: 0x00013FD7
		public Expression2(Expression body, ParameterExpression par0, ParameterExpression par1)
			: base(body)
		{
			this._par0 = par0;
			this._par1 = par1;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00012A81 File Offset: 0x00010C81
		internal override int ParameterCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00015DEE File Offset: 0x00013FEE
		internal override ParameterExpression GetParameter(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			}
			if (index != 1)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return this._par1;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00015E16 File Offset: 0x00014016
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00015E24 File Offset: 0x00014024
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[]
			{
				ExpressionUtils.ReturnObject<ParameterExpression>(this._par0),
				this._par1
			});
		}

		// Token: 0x040001C4 RID: 452
		private object _par0;

		// Token: 0x040001C5 RID: 453
		private readonly ParameterExpression _par1;
	}
}
