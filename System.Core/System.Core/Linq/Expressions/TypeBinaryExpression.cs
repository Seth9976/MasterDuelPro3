using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	/// <summary>Represents an operation between an expression and a type.</summary>
	// Token: 0x020000DB RID: 219
	[DebuggerTypeProxy(typeof(Expression.TypeBinaryExpressionProxy))]
	public sealed class TypeBinaryExpression : Expression
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x0001777E File Offset: 0x0001597E
		internal TypeBinaryExpression(Expression expression, Type typeOperand, ExpressionType nodeType)
		{
			this.Expression = expression;
			this.TypeOperand = typeOperand;
			this.NodeType = nodeType;
		}

		/// <summary>Gets the static type of the expression that this <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		public sealed override Type Type
		{
			get
			{
				return typeof(bool);
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001779B File Offset: 0x0001599B
		public sealed override ExpressionType NodeType { get; }

		/// <summary>Gets the expression operand of a type test operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression operand of a type test operation.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x000177A3 File Offset: 0x000159A3
		public Expression Expression { get; }

		/// <summary>Gets the type operand of a type test operation.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type operand of a type test operation.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000177AB File Offset: 0x000159AB
		public Type TypeOperand { get; }

		// Token: 0x06000748 RID: 1864 RVA: 0x000177B4 File Offset: 0x000159B4
		internal Expression ReduceTypeEqual()
		{
			Type type = this.Expression.Type;
			if (type.IsValueType || this.TypeOperand.IsPointer)
			{
				if (!type.IsNullableType())
				{
					return Expression.Block(this.Expression, Utils.Constant(type == this.TypeOperand.GetNonNullableType()));
				}
				if (type.GetNonNullableType() != this.TypeOperand.GetNonNullableType())
				{
					return Expression.Block(this.Expression, Utils.Constant(false));
				}
				return Expression.NotEqual(this.Expression, Expression.Constant(null, this.Expression.Type));
			}
			else
			{
				if (this.Expression.NodeType == ExpressionType.Constant)
				{
					return this.ReduceConstantTypeEqual();
				}
				ParameterExpression parameterExpression = this.Expression as ParameterExpression;
				if (parameterExpression != null && !parameterExpression.IsByRef)
				{
					return this.ByValParameterTypeEqual(parameterExpression);
				}
				parameterExpression = Expression.Parameter(typeof(object));
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, this.Expression),
					this.ByValParameterTypeEqual(parameterExpression)
				}));
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000178D0 File Offset: 0x00015AD0
		private Expression ByValParameterTypeEqual(ParameterExpression value)
		{
			Expression expression = Expression.Call(value, CachedReflectionInfo.Object_GetType);
			if (this.TypeOperand.IsInterface)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(Type));
				expression = Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, expression),
					parameterExpression
				}));
			}
			return Expression.AndAlso(Expression.ReferenceNotEqual(value, Utils.Null), Expression.ReferenceEqual(expression, Expression.Constant(this.TypeOperand.GetNonNullableType(), typeof(Type))));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00017964 File Offset: 0x00015B64
		private Expression ReduceConstantTypeEqual()
		{
			ConstantExpression constantExpression = this.Expression as ConstantExpression;
			if (constantExpression.Value == null)
			{
				return Utils.Constant(false);
			}
			return Utils.Constant(this.TypeOperand.GetNonNullableType() == constantExpression.Value.GetType());
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000179AC File Offset: 0x00015BAC
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTypeBinary(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> property of the result.</param>
		// Token: 0x0600074C RID: 1868 RVA: 0x000179B5 File Offset: 0x00015BB5
		public TypeBinaryExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			if (this.NodeType == ExpressionType.TypeIs)
			{
				return Expression.TypeIs(expression, this.TypeOperand);
			}
			return Expression.TypeEqual(expression, this.TypeOperand);
		}
	}
}
