using System;
using System.Collections.Generic;

namespace System.Linq.Expressions
{
	// Token: 0x020000B0 RID: 176
	internal sealed class FullExpression<TDelegate> : ExpressionN<TDelegate>
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00015F58 File Offset: 0x00014158
		public FullExpression(Expression body, string name, bool tailCall, IReadOnlyList<ParameterExpression> parameters)
			: base(body, parameters)
		{
			this.NameCore = name;
			this.TailCallCore = tailCall;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00015F71 File Offset: 0x00014171
		internal override string NameCore { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00015F79 File Offset: 0x00014179
		internal override bool TailCallCore { get; }
	}
}
