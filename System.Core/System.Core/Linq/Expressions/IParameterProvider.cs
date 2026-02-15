using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200009D RID: 157
	internal interface IParameterProvider
	{
		// Token: 0x06000570 RID: 1392
		ParameterExpression GetParameter(int index);

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000571 RID: 1393
		int ParameterCount { get; }
	}
}
