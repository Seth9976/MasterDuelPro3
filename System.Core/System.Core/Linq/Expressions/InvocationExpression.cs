using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that applies a delegate or lambda expression to a list of argument expressions.</summary>
	// Token: 0x0200009F RID: 159
	[DebuggerTypeProxy(typeof(Expression.InvocationExpressionProxy))]
	public class InvocationExpression : Expression, IArgumentProvider
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000157E6 File Offset: 0x000139E6
		internal InvocationExpression(Expression expression, Type returnType)
		{
			this.Expression = expression;
			this.Type = returnType;
		}

		/// <summary>Gets the static type of the expression that this <see cref="P:System.Linq.Expressions.InvocationExpression.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.InvocationExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000157FC File Offset: 0x000139FC
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00015804 File Offset: 0x00013A04
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Invoke;
			}
		}

		/// <summary>Gets the delegate or lambda expression to be applied.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate to be applied.</returns>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00015808 File Offset: 0x00013A08
		public Expression Expression { get; }

		// Token: 0x0600057F RID: 1407 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public virtual Expression GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public virtual int ArgumentCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00015810 File Offset: 0x00013A10
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitInvocation(this);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00015819 File Offset: 0x00013A19
		internal LambdaExpression LambdaOperand
		{
			get
			{
				if (this.Expression.NodeType != ExpressionType.Quote)
				{
					return this.Expression as LambdaExpression;
				}
				return (LambdaExpression)((UnaryExpression)this.Expression).Operand;
			}
		}
	}
}
