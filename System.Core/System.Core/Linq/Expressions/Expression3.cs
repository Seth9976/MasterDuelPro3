using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000AE RID: 174
	internal sealed class Expression3<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x00015E54 File Offset: 0x00014054
		public Expression3(Expression body, ParameterExpression par0, ParameterExpression par1, ParameterExpression par2)
			: base(body)
		{
			this._par0 = par0;
			this._par1 = par1;
			this._par2 = par2;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00012AF6 File Offset: 0x00010CF6
		internal override int ParameterCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00015E73 File Offset: 0x00014073
		internal override ParameterExpression GetParameter(int index)
		{
			switch (index)
			{
			case 0:
				return ExpressionUtils.ReturnObject<ParameterExpression>(this._par0);
			case 1:
				return this._par1;
			case 2:
				return this._par2;
			default:
				throw Error.ArgumentOutOfRange("index");
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00015EAD File Offset: 0x000140AD
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly(this, ref this._par0);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00015EBB File Offset: 0x000140BB
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			if (parameters != null)
			{
				return Expression.Lambda<TDelegate>(body, parameters);
			}
			return Expression.Lambda<TDelegate>(body, new ParameterExpression[]
			{
				ExpressionUtils.ReturnObject<ParameterExpression>(this._par0),
				this._par1,
				this._par2
			});
		}

		// Token: 0x040001C6 RID: 454
		private object _par0;

		// Token: 0x040001C7 RID: 455
		private readonly ParameterExpression _par1;

		// Token: 0x040001C8 RID: 456
		private readonly ParameterExpression _par2;
	}
}
