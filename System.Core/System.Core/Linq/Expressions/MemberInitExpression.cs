using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents calling a constructor and initializing one or more members of the new object.</summary>
	// Token: 0x020000B9 RID: 185
	[DebuggerTypeProxy(typeof(Expression.MemberInitExpressionProxy))]
	public sealed class MemberInitExpression : Expression
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x000161C5 File Offset: 0x000143C5
		internal MemberInitExpression(NewExpression newExpression, ReadOnlyCollection<MemberBinding> bindings)
		{
			this.NewExpression = newExpression;
			this.Bindings = bindings;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.MemberInitExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000161DB File Offset: 0x000143DB
		public sealed override Type Type
		{
			get
			{
				return this.NewExpression.Type;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00009F9F File Offset: 0x0000819F
		public override bool CanReduce
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000161E8 File Offset: 0x000143E8
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.MemberInit;
			}
		}

		/// <summary>Gets the expression that represents the constructor call.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that represents the constructor call.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000161EC File Offset: 0x000143EC
		public NewExpression NewExpression { get; }

		/// <summary>Gets the bindings that describe how to initialize the members of the newly created object.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects which describe how to initialize the members.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x000161F4 File Offset: 0x000143F4
		public ReadOnlyCollection<MemberBinding> Bindings { get; }

		// Token: 0x06000612 RID: 1554 RVA: 0x000161FC File Offset: 0x000143FC
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMemberInit(this);
		}

		/// <summary>Reduces the <see cref="T:System.Linq.Expressions.MemberInitExpression" /> to a simpler expression. </summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000613 RID: 1555 RVA: 0x00016205 File Offset: 0x00014405
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceMemberInit(this.NewExpression, this.Bindings, true);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001621C File Offset: 0x0001441C
		private static Expression ReduceMemberInit(Expression objExpression, ReadOnlyCollection<MemberBinding> bindings, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(objExpression.Type);
			int count = bindings.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, objExpression);
			for (int i = 0; i < count; i++)
			{
				array[i + 1] = MemberInitExpression.ReduceMemberBinding(parameterExpression, bindings[i]);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Utils.Empty);
			return Expression.Block(new ParameterExpression[] { parameterExpression }, array);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00016290 File Offset: 0x00014490
		internal static Expression ReduceListInit(Expression listExpression, ReadOnlyCollection<ElementInit> initializers, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(listExpression.Type);
			int count = initializers.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, listExpression);
			for (int i = 0; i < count; i++)
			{
				ElementInit elementInit = initializers[i];
				array[i + 1] = Expression.Call(parameterExpression, elementInit.AddMethod, elementInit.Arguments);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Utils.Empty);
			return Expression.Block(new ParameterExpression[] { parameterExpression }, array);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00016314 File Offset: 0x00014514
		internal static Expression ReduceMemberBinding(ParameterExpression objVar, MemberBinding binding)
		{
			MemberExpression memberExpression = Expression.MakeMemberAccess(objVar, binding.Member);
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return Expression.Assign(memberExpression, ((MemberAssignment)binding).Expression);
			case MemberBindingType.MemberBinding:
				return MemberInitExpression.ReduceMemberInit(memberExpression, ((MemberMemberBinding)binding).Bindings, false);
			case MemberBindingType.ListBinding:
				return MemberInitExpression.ReduceListInit(memberExpression, ((MemberListBinding)binding).Initializers, false);
			default:
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="newExpression">The <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> property of the result.</param>
		/// <param name="bindings">The <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> property of the result.</param>
		// Token: 0x06000617 RID: 1559 RVA: 0x00016386 File Offset: 0x00014586
		public MemberInitExpression Update(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			if (((newExpression == this.NewExpression) & (bindings != null)) && ExpressionUtils.SameElements<MemberBinding>(ref bindings, this.Bindings))
			{
				return this;
			}
			return Expression.MemberInit(newExpression, bindings);
		}
	}
}
