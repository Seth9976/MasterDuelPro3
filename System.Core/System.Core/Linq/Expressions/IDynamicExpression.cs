using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200009C RID: 156
	public interface IDynamicExpression : IArgumentProvider
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600056D RID: 1389
		Type DelegateType { get; }

		// Token: 0x0600056E RID: 1390
		Expression Rewrite(Expression[] args);

		// Token: 0x0600056F RID: 1391
		object CreateCallSite();
	}
}
