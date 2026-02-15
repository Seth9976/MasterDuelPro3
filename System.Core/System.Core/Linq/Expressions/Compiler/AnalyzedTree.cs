using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000DE RID: 222
	internal sealed class AnalyzedTree
	{
		// Token: 0x04000236 RID: 566
		internal readonly Dictionary<object, CompilerScope> Scopes = new Dictionary<object, CompilerScope>();

		// Token: 0x04000237 RID: 567
		internal readonly Dictionary<LambdaExpression, BoundConstants> Constants = new Dictionary<LambdaExpression, BoundConstants>();
	}
}
