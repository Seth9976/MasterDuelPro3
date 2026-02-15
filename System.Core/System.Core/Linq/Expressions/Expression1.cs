using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000AC RID: 172
	internal sealed class Expression1<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00015D77 File Offset: 0x00013F77
		public Expression1(Expression body, ParameterExpression par0)
			: base(body)
		{
			this._par0 = par0;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00009F9F File Offset: 0x0000819F
		internal override int ParameterCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00015D87 File Offset: 0x00013F87
		internal override ParameterExpression GetParameter(int index)
		{
			if (index == 0)
			{
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			}
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00015DA2 File Offset: 0x00013FA2
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00015DB0 File Offset: 0x00013FB0
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[] { ExpressionUtils.ReturnObject<ParameterExpression>(this._par0) });
		}

		// Token: 0x040001C3 RID: 451
		private object _par0;
	}
}
