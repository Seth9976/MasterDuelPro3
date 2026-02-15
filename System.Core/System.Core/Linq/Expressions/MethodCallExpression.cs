using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents a call to either static or an instance method.</summary>
	// Token: 0x020000BC RID: 188
	[DebuggerTypeProxy(typeof(Expression.MethodCallExpressionProxy))]
	public class MethodCallExpression : Expression, IArgumentProvider
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00016426 File Offset: 0x00014626
		internal MethodCallExpression(MethodInfo method)
		{
			this.Method = method;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000C30F File Offset: 0x0000A50F
		internal virtual Expression GetInstance()
		{
			return null;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00016435 File Offset: 0x00014635
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Call;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.MethodCallExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00016438 File Offset: 0x00014638
		public sealed override Type Type
		{
			get
			{
				return this.Method.ReturnType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodInfo" /> for the method to be called.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the called method.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00016445 File Offset: 0x00014645
		public MethodInfo Method { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> that represents the instance for instance method calls or null for static method calls.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the receiving object of the method.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001644D File Offset: 0x0001464D
		public Expression Object
		{
			get
			{
				return this.GetInstance();
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06000626 RID: 1574 RVA: 0x00016455 File Offset: 0x00014655
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMethodCall(this);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual MethodCallExpression Rewrite(Expression instance, IReadOnlyList<Expression> args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public virtual Expression GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public virtual int ArgumentCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}
	}
}
