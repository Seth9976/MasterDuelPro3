using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Threading;

namespace System.Linq.Expressions
{
	/// <summary>Represents a block that contains a sequence of expressions where variables can be defined.</summary>
	// Token: 0x0200007D RID: 125
	[DebuggerTypeProxy(typeof(Expression.BlockExpressionProxy))]
	public class BlockExpression : Expression
	{
		/// <summary>Gets the expressions in this block.</summary>
		/// <returns>The read-only collection containing all the expressions in this block.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x000129C8 File Offset: 0x00010BC8
		public ReadOnlyCollection<Expression> Expressions
		{
			get
			{
				return this.GetOrMakeExpressions();
			}
		}

		/// <summary>Gets the variables defined in this block.</summary>
		/// <returns>The read-only collection containing all the variables defined in this block.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000129D0 File Offset: 0x00010BD0
		public ReadOnlyCollection<ParameterExpression> Variables
		{
			get
			{
				return this.GetOrMakeVariables();
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000129D8 File Offset: 0x00010BD8
		internal BlockExpression()
		{
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x060003E3 RID: 995 RVA: 0x000129E0 File Offset: 0x00010BE0
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitBlock(this);
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000129E9 File Offset: 0x00010BE9
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Block;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.BlockExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000129ED File Offset: 0x00010BED
		public override Type Type
		{
			get
			{
				return this.GetExpression(this.ExpressionCount - 1).Type;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual Expression GetExpression(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual int ExpressionCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012A09 File Offset: 0x00010C09
		internal virtual ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return EmptyReadOnlyCollection<ParameterExpression>.Instance;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00012A10 File Offset: 0x00010C10
		internal static ReadOnlyCollection<Expression> ReturnReadOnlyExpressions(BlockExpression provider, ref object collection)
		{
			Expression expression = collection as Expression;
			if (expression != null)
			{
				Interlocked.CompareExchange(ref collection, new ReadOnlyCollection<Expression>(new BlockExpressionList(provider, expression)), expression);
			}
			return (ReadOnlyCollection<Expression>)collection;
		}
	}
}
