using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents a constructor call.</summary>
	// Token: 0x020000CD RID: 205
	[DebuggerTypeProxy(typeof(Expression.NewExpressionProxy))]
	public class NewExpression : Expression, IArgumentProvider
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x00016AB8 File Offset: 0x00014CB8
		internal NewExpression(ConstructorInfo constructor, IReadOnlyList<Expression> arguments, ReadOnlyCollection<MemberInfo> members)
		{
			this.Constructor = constructor;
			this._arguments = arguments;
			this.Members = members;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.NewExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00016AD5 File Offset: 0x00014CD5
		public override Type Type
		{
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00016AE2 File Offset: 0x00014CE2
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.New;
			}
		}

		/// <summary>Gets the called constructor.</summary>
		/// <returns>The <see cref="T:System.Reflection.ConstructorInfo" /> that represents the called constructor.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00016AE6 File Offset: 0x00014CE6
		public ConstructorInfo Constructor { get; }

		/// <summary>Gets the arguments to the constructor.</summary>
		/// <returns>A collection of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments to the constructor.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00016AEE File Offset: 0x00014CEE
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return ExpressionUtils.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00016AFB File Offset: 0x00014CFB
		public Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00016B09 File Offset: 0x00014D09
		public int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		/// <summary>Gets the members that can retrieve the values of the fields that were initialized with constructor arguments.</summary>
		/// <returns>A collection of <see cref="T:System.Reflection.MemberInfo" /> objects that represent the members that can retrieve the values of the fields that were initialized with constructor arguments.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00016B16 File Offset: 0x00014D16
		public ReadOnlyCollection<MemberInfo> Members { get; }

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x0600066E RID: 1646 RVA: 0x00016B1E File Offset: 0x00014D1E
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNew(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="arguments">The <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> property of the result.</param>
		// Token: 0x0600066F RID: 1647 RVA: 0x00016B27 File Offset: 0x00014D27
		public NewExpression Update(IEnumerable<Expression> arguments)
		{
			if (ExpressionUtils.SameElements<Expression>(ref arguments, this.Arguments))
			{
				return this;
			}
			if (this.Members == null)
			{
				return Expression.New(this.Constructor, arguments);
			}
			return Expression.New(this.Constructor, arguments, this.Members);
		}

		// Token: 0x040001FA RID: 506
		private IReadOnlyList<Expression> _arguments;
	}
}
