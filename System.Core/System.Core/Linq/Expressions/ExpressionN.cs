using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000AF RID: 175
	internal class ExpressionN<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x00015EF4 File Offset: 0x000140F4
		public ExpressionN(Expression body, IReadOnlyList<ParameterExpression> parameters)
			: base(body)
		{
			this._parameters = parameters;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00015F04 File Offset: 0x00014104
		internal override int ParameterCount
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00015F11 File Offset: 0x00014111
		internal override ParameterExpression GetParameter(int index)
		{
			return this._parameters[index];
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00015F1F File Offset: 0x0001411F
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return ExpressionUtils.ReturnReadOnly<ParameterExpression>(ref this._parameters);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00015F2C File Offset: 0x0001412C
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, base.Name, base.TailCall, parameters ?? this._parameters);
		}

		// Token: 0x040001C9 RID: 457
		private IReadOnlyList<ParameterExpression> _parameters;
	}
}
