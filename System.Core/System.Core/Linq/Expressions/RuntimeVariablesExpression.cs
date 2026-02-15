using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>An expression that provides runtime read/write permission for variables.</summary>
	// Token: 0x020000D2 RID: 210
	[DebuggerTypeProxy(typeof(Expression.RuntimeVariablesExpressionProxy))]
	public sealed class RuntimeVariablesExpression : Expression
	{
		/// <summary>The variables or parameters to which to provide runtime access.</summary>
		/// <returns>The read-only collection containing parameters that will be provided the runtime access.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00016CFE File Offset: 0x00014EFE
		public ReadOnlyCollection<ParameterExpression> Variables { get; }
	}
}
