using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x020000AB RID: 171
	internal sealed class Expression0<TDelegate> : Expression<TDelegate>
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x00015D59 File Offset: 0x00013F59
		public Expression0(Expression body)
			: base(body)
		{
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000B252 File Offset: 0x00009452
		internal override int ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015D62 File Offset: 0x00013F62
		internal override ParameterExpression GetParameter(int index)
		{
			throw Error.ArgumentOutOfRange("index");
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00012A09 File Offset: 0x00010C09
		internal override ReadOnlyCollection<ParameterExpression> GetOrMakeParameters()
		{
			return EmptyReadOnlyCollection<ParameterExpression>.Instance;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00015D6E File Offset: 0x00013F6E
		internal override Expression<TDelegate> Rewrite(Expression body, ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, parameters);
		}
	}
}
