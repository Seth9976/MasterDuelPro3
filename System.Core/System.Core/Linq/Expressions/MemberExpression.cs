using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents accessing a field or property.</summary>
	// Token: 0x020000B6 RID: 182
	[DebuggerTypeProxy(typeof(Expression.MemberExpressionProxy))]
	public class MemberExpression : Expression
	{
		/// <summary>Gets the field or property to be accessed.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be accessed.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x000160F0 File Offset: 0x000142F0
		public MemberInfo Member
		{
			get
			{
				return this.GetMember();
			}
		}

		/// <summary>Gets the containing object of the field or property.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the containing object of the field or property.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000160F8 File Offset: 0x000142F8
		public Expression Expression { get; }

		// Token: 0x060005FE RID: 1534 RVA: 0x00016100 File Offset: 0x00014300
		internal MemberExpression(Expression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001610F File Offset: 0x0001430F
		internal static PropertyExpression Make(Expression expression, PropertyInfo property)
		{
			return new PropertyExpression(expression, property);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00016118 File Offset: 0x00014318
		internal static FieldExpression Make(Expression expression, FieldInfo field)
		{
			return new FieldExpression(expression, field);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00016124 File Offset: 0x00014324
		internal static MemberExpression Make(Expression expression, MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (!(fieldInfo == null))
			{
				return MemberExpression.Make(expression, fieldInfo);
			}
			return MemberExpression.Make(expression, (PropertyInfo)member);
		}

		/// <summary>Returns the node type of this <see cref="P:System.Linq.Expressions.MemberExpression.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00016155 File Offset: 0x00014355
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.MemberAccess;
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		internal virtual MemberInfo GetMember()
		{
			throw ContractUtils.Unreachable;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06000604 RID: 1540 RVA: 0x00016159 File Offset: 0x00014359
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMember(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property of the result.</param>
		// Token: 0x06000605 RID: 1541 RVA: 0x00016162 File Offset: 0x00014362
		public MemberExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.MakeMemberAccess(expression, this.Member);
		}
	}
}
