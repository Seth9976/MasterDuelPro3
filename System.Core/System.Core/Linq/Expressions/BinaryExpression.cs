using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a binary operator.</summary>
	// Token: 0x02000059 RID: 89
	[DebuggerTypeProxy(typeof(Expression.BinaryExpressionProxy))]
	public class BinaryExpression : Expression
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		internal BinaryExpression(Expression left, Expression right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if the expression tree node can be reduced, otherwise false.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C2DD File Offset: 0x0000A4DD
		public override bool CanReduce
		{
			get
			{
				return BinaryExpression.IsOpAssignment(this.NodeType);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000C2EA File Offset: 0x0000A4EA
		private static bool IsOpAssignment(ExpressionType op)
		{
			return op - ExpressionType.AddAssign <= 13;
		}

		/// <summary>Gets the right operand of the binary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand of the binary operation.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C2F7 File Offset: 0x0000A4F7
		public Expression Right { get; }

		/// <summary>Gets the left operand of the binary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand of the binary operation.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000C2FF File Offset: 0x0000A4FF
		public Expression Left { get; }

		/// <summary>Gets the implementing method for the binary operation.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C307 File Offset: 0x0000A507
		public MethodInfo Method
		{
			get
			{
				return this.GetMethod();
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C30F File Offset: 0x0000A50F
		internal virtual MethodInfo GetMethod()
		{
			return null;
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="left">The <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property of the result. </param>
		/// <param name="conversion">The <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property of the result.</param>
		/// <param name="right">The <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property of the result. </param>
		// Token: 0x060002D3 RID: 723 RVA: 0x0000C314 File Offset: 0x0000A514
		public BinaryExpression Update(Expression left, LambdaExpression conversion, Expression right)
		{
			if (left == this.Left && right == this.Right && conversion == this.Conversion)
			{
				return this;
			}
			if (!this.IsReferenceComparison)
			{
				return Expression.MakeBinary(this.NodeType, left, right, this.IsLiftedToNull, this.Method, conversion);
			}
			if (this.NodeType == ExpressionType.Equal)
			{
				return Expression.ReferenceEqual(left, right);
			}
			return Expression.ReferenceNotEqual(left, right);
		}

		/// <summary>Reduces the binary expression node to a simpler expression.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x060002D4 RID: 724 RVA: 0x0000C37C File Offset: 0x0000A57C
		public override Expression Reduce()
		{
			if (!BinaryExpression.IsOpAssignment(this.NodeType))
			{
				return this;
			}
			ExpressionType nodeType = this.Left.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return this.ReduceMember();
			}
			if (nodeType != ExpressionType.Index)
			{
				return this.ReduceVariable();
			}
			return this.ReduceIndex();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		private static ExpressionType GetBinaryOpFromAssignmentOp(ExpressionType op)
		{
			switch (op)
			{
			case ExpressionType.AddAssign:
				return ExpressionType.Add;
			case ExpressionType.AndAssign:
				return ExpressionType.And;
			case ExpressionType.DivideAssign:
				return ExpressionType.Divide;
			case ExpressionType.ExclusiveOrAssign:
				return ExpressionType.ExclusiveOr;
			case ExpressionType.LeftShiftAssign:
				return ExpressionType.LeftShift;
			case ExpressionType.ModuloAssign:
				return ExpressionType.Modulo;
			case ExpressionType.MultiplyAssign:
				return ExpressionType.Multiply;
			case ExpressionType.OrAssign:
				return ExpressionType.Or;
			case ExpressionType.PowerAssign:
				return ExpressionType.Power;
			case ExpressionType.RightShiftAssign:
				return ExpressionType.RightShift;
			case ExpressionType.SubtractAssign:
				return ExpressionType.Subtract;
			case ExpressionType.AddAssignChecked:
				return ExpressionType.AddChecked;
			case ExpressionType.MultiplyAssignChecked:
				return ExpressionType.MultiplyChecked;
			case ExpressionType.SubtractAssignChecked:
				return ExpressionType.SubtractChecked;
			default:
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C440 File Offset: 0x0000A640
		private Expression ReduceVariable()
		{
			Expression expression = Expression.MakeBinary(BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType), this.Left, this.Right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression = Expression.Invoke(conversion, expression);
			}
			return Expression.Assign(this.Left, expression);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C490 File Offset: 0x0000A690
		private Expression ReduceMember()
		{
			MemberExpression memberExpression = (MemberExpression)this.Left;
			if (memberExpression.Expression == null)
			{
				return this.ReduceVariable();
			}
			ParameterExpression parameterExpression = Expression.Variable(memberExpression.Expression.Type, "temp1");
			Expression expression = Expression.Assign(parameterExpression, memberExpression.Expression);
			Expression expression2 = Expression.MakeBinary(BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType), Expression.MakeMemberAccess(parameterExpression, memberExpression.Member), this.Right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression2 = Expression.Invoke(conversion, expression2);
			}
			ParameterExpression parameterExpression2 = Expression.Variable(expression2.Type, "temp2");
			expression2 = Expression.Assign(parameterExpression2, expression2);
			Expression expression3 = Expression.Assign(Expression.MakeMemberAccess(parameterExpression, memberExpression.Member), parameterExpression2);
			Expression expression4 = parameterExpression2;
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression, parameterExpression2 }), new TrueReadOnlyCollection<Expression>(new Expression[] { expression, expression2, expression3, expression4 }));
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000C584 File Offset: 0x0000A784
		private Expression ReduceIndex()
		{
			IndexExpression indexExpression = (IndexExpression)this.Left;
			global::System.Collections.Generic.ArrayBuilder<ParameterExpression> arrayBuilder = new global::System.Collections.Generic.ArrayBuilder<ParameterExpression>(indexExpression.ArgumentCount + 2);
			global::System.Collections.Generic.ArrayBuilder<Expression> arrayBuilder2 = new global::System.Collections.Generic.ArrayBuilder<Expression>(indexExpression.ArgumentCount + 3);
			ParameterExpression parameterExpression = Expression.Variable(indexExpression.Object.Type, "tempObj");
			arrayBuilder.UncheckedAdd(parameterExpression);
			arrayBuilder2.UncheckedAdd(Expression.Assign(parameterExpression, indexExpression.Object));
			int argumentCount = indexExpression.ArgumentCount;
			global::System.Collections.Generic.ArrayBuilder<Expression> arrayBuilder3 = new global::System.Collections.Generic.ArrayBuilder<Expression>(argumentCount);
			for (int i = 0; i < argumentCount; i++)
			{
				Expression argument = indexExpression.GetArgument(i);
				ParameterExpression parameterExpression2 = Expression.Variable(argument.Type, "tempArg" + i.ToString());
				arrayBuilder.UncheckedAdd(parameterExpression2);
				arrayBuilder3.UncheckedAdd(parameterExpression2);
				arrayBuilder2.UncheckedAdd(Expression.Assign(parameterExpression2, argument));
			}
			IndexExpression indexExpression2 = Expression.MakeIndex(parameterExpression, indexExpression.Indexer, arrayBuilder3.ToReadOnly<Expression>());
			Expression expression = Expression.MakeBinary(BinaryExpression.GetBinaryOpFromAssignmentOp(this.NodeType), indexExpression2, this.Right, false, this.Method);
			LambdaExpression conversion = this.GetConversion();
			if (conversion != null)
			{
				expression = Expression.Invoke(conversion, expression);
			}
			ParameterExpression parameterExpression3 = Expression.Variable(expression.Type, "tempValue");
			arrayBuilder.UncheckedAdd(parameterExpression3);
			arrayBuilder2.UncheckedAdd(Expression.Assign(parameterExpression3, expression));
			arrayBuilder2.UncheckedAdd(Expression.Assign(indexExpression2, parameterExpression3));
			return Expression.Block(arrayBuilder.ToReadOnly<ParameterExpression>(), arrayBuilder2.ToReadOnly<Expression>());
		}

		/// <summary>Gets the type conversion function that is used by a coalescing or compound assignment operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that represents a type conversion function.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C6F3 File Offset: 0x0000A8F3
		public LambdaExpression Conversion
		{
			get
			{
				return this.GetConversion();
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000C30F File Offset: 0x0000A50F
		internal virtual LambdaExpression GetConversion()
		{
			return null;
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator.</summary>
		/// <returns>true if the node represents a lifted call; otherwise, false.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public bool IsLifted
		{
			get
			{
				if (this.NodeType == ExpressionType.Coalesce || this.NodeType == ExpressionType.Assign)
				{
					return false;
				}
				if (this.Left.Type.IsNullableType())
				{
					MethodInfo method = this.GetMethod();
					return method == null || !TypeUtils.AreEquivalent(method.GetParametersCached()[0].ParameterType.GetNonRefType(), this.Left.Type);
				}
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator whose return type is lifted to a nullable type.</summary>
		/// <returns>true if the operator's return type is lifted to a nullable type; otherwise, false.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000C769 File Offset: 0x0000A969
		public bool IsLiftedToNull
		{
			get
			{
				return this.IsLifted && this.Type.IsNullableType();
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x060002DD RID: 733 RVA: 0x0000C780 File Offset: 0x0000A980
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitBinary(this);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C78C File Offset: 0x0000A98C
		internal static BinaryExpression Create(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method, LambdaExpression conversion)
		{
			if (conversion != null)
			{
				return new CoalesceConversionBinaryExpression(left, right, conversion);
			}
			if (method != null)
			{
				return new MethodBinaryExpression(nodeType, left, right, type, method);
			}
			if (type == typeof(bool))
			{
				return new LogicalBinaryExpression(nodeType, left, right);
			}
			return new SimpleBinaryExpression(nodeType, left, right, type);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		internal bool IsLiftedLogical
		{
			get
			{
				Type type = this.Left.Type;
				Type type2 = this.Right.Type;
				MethodInfo method = this.GetMethod();
				ExpressionType nodeType = this.NodeType;
				return (nodeType == ExpressionType.AndAlso || nodeType == ExpressionType.OrElse) && TypeUtils.AreEquivalent(type2, type) && type.IsNullableType() && method != null && TypeUtils.AreEquivalent(method.ReturnType, type.GetNonNullableType());
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000C850 File Offset: 0x0000AA50
		internal bool IsReferenceComparison
		{
			get
			{
				Type type = this.Left.Type;
				Type type2 = this.Right.Type;
				MethodInfo method = this.GetMethod();
				ExpressionType nodeType = this.NodeType;
				return (nodeType == ExpressionType.Equal || nodeType == ExpressionType.NotEqual) && method == null && !type.IsValueType && !type2.IsValueType;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		internal Expression ReduceUserdefinedLifted()
		{
			ParameterExpression parameterExpression = Expression.Parameter(this.Left.Type, "left");
			ParameterExpression parameterExpression2 = Expression.Parameter(this.Right.Type, "right");
			string text = ((this.NodeType == ExpressionType.AndAlso) ? "op_False" : "op_True");
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(this.Method.DeclaringType, text);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				Expression.Assign(parameterExpression, this.Left),
				Expression.Condition(Expression.Property(parameterExpression, "HasValue"), Expression.Condition(Expression.Call(booleanOperator, Expression.Call(parameterExpression, "GetValueOrDefault", null, Array.Empty<Expression>())), parameterExpression, Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression2 }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression2, this.Right),
					Expression.Condition(Expression.Property(parameterExpression2, "HasValue"), Expression.Convert(Expression.Call(this.Method, Expression.Call(parameterExpression, "GetValueOrDefault", null, Array.Empty<Expression>()), Expression.Call(parameterExpression2, "GetValueOrDefault", null, Array.Empty<Expression>())), this.Type), Expression.Constant(null, this.Type))
				}))), Expression.Constant(null, this.Type))
			}));
		}
	}
}
