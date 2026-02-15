using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Emits or clears a sequence point for debug information. This allows the debugger to highlight the correct source code when debugging.</summary>
	// Token: 0x02000092 RID: 146
	[DebuggerTypeProxy(typeof(Expression.DebugInfoExpressionProxy))]
	public class DebugInfoExpression : Expression
	{
	}
}
