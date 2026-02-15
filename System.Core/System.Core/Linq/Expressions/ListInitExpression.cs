using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents a constructor call that has a collection initializer.</summary>
	// Token: 0x020000B1 RID: 177
	[DebuggerTypeProxy(typeof(Expression.ListInitExpressionProxy))]
	public sealed class ListInitExpression : Expression
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x00015F81 File Offset: 0x00014181
		internal ListInitExpression(NewExpression newExpression, ReadOnlyCollection<ElementInit> initializers)
		{
			this.NewExpression = newExpression;
			this.Initializers = initializers;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00015F97 File Offset: 0x00014197
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.ListInit;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ListInitExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00015F9B File Offset: 0x0001419B
		public sealed override Type Type
		{
			get
			{
				return this.NewExpression.Type;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00009F9F File Offset: 0x0000819F
		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the expression that contains a call to the constructor of a collection type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that represents the call to the constructor of a collection type.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00015FA8 File Offset: 0x000141A8
		public NewExpression NewExpression { get; }

		/// <summary>Gets the element initializers that are used to initialize a collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects which represent the elements that are used to initialize the collection.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00015FB0 File Offset: 0x000141B0
		public ReadOnlyCollection<ElementInit> Initializers { get; }

		// Token: 0x060005E8 RID: 1512 RVA: 0x00015FB8 File Offset: 0x000141B8
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitListInit(this);
		}

		/// <summary>Reduces the binary expression node to a simpler expression.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x060005E9 RID: 1513 RVA: 0x00015FC1 File Offset: 0x000141C1
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceListInit(this.NewExpression, this.Initializers, true);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="newExpression">The <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property of the result.</param>
		/// <param name="initializers">The <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> property of the result.</param>
		// Token: 0x060005EA RID: 1514 RVA: 0x00015FD5 File Offset: 0x000141D5
		public ListInitExpression Update(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			if (((newExpression == this.NewExpression) & (initializers != null)) && ExpressionUtils.SameElements<ElementInit>(ref initializers, this.Initializers))
			{
				return this;
			}
			return Expression.ListInit(newExpression, initializers);
		}
	}
}
