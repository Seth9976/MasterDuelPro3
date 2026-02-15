using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Represents a catch statement in a try block.</summary>
	// Token: 0x02000089 RID: 137
	[DebuggerTypeProxy(typeof(Expression.CatchBlockProxy))]
	public sealed class CatchBlock
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x00012FDE File Offset: 0x000111DE
		internal CatchBlock(Type test, ParameterExpression variable, Expression body, Expression filter)
		{
			this.Test = test;
			this.Variable = variable;
			this.Body = body;
			this.Filter = filter;
		}

		/// <summary>Gets a reference to the <see cref="T:System.Exception" /> object caught by this handler.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ParameterExpression" /> object representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00013003 File Offset: 0x00011203
		public ParameterExpression Variable { get; }

		/// <summary>Gets the type of <see cref="T:System.Exception" /> this handler catches.</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of <see cref="T:System.Exception" /> this handler catches.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001300B File Offset: 0x0001120B
		public Type Test { get; }

		/// <summary>Gets the body of the catch block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the catch body.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00013013 File Offset: 0x00011213
		public Expression Body { get; }

		/// <summary>Gets the body of the <see cref="T:System.Linq.Expressions.CatchBlock" /> filter.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the body of the <see cref="T:System.Linq.Expressions.CatchBlock" /> filter.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001301B File Offset: 0x0001121B
		public Expression Filter { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06000432 RID: 1074 RVA: 0x00013023 File Offset: 0x00011223
		public override string ToString()
		{
			return ExpressionStringBuilder.CatchBlockToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="variable">The <see cref="P:System.Linq.Expressions.CatchBlock.Variable" /> property of the result.</param>
		/// <param name="filter">The <see cref="P:System.Linq.Expressions.CatchBlock.Filter" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.CatchBlock.Body" /> property of the result.</param>
		// Token: 0x06000433 RID: 1075 RVA: 0x0001302B File Offset: 0x0001122B
		public CatchBlock Update(ParameterExpression variable, Expression filter, Expression body)
		{
			if (variable == this.Variable && filter == this.Filter && body == this.Body)
			{
				return this;
			}
			return Expression.MakeCatchBlock(this.Test, variable, body, filter);
		}
	}
}
