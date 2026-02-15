using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200009B RID: 155
	public interface IArgumentProvider
	{
		// Token: 0x0600056B RID: 1387
		Expression GetArgument(int index);

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600056C RID: 1388
		int ArgumentCount { get; }
	}
}
