using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents creating a new array and possibly initializing the elements of the new array.</summary>
	// Token: 0x020000CA RID: 202
	[DebuggerTypeProxy(typeof(Expression.NewArrayExpressionProxy))]
	public class NewArrayExpression : Expression
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x00016A09 File Offset: 0x00014C09
		internal NewArrayExpression(Type type, ReadOnlyCollection<Expression> expressions)
		{
			this.Expressions = expressions;
			this.Type = type;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00016A1F File Offset: 0x00014C1F
		internal static NewArrayExpression Make(ExpressionType nodeType, Type type, ReadOnlyCollection<Expression> expressions)
		{
			if (nodeType == ExpressionType.NewArrayInit)
			{
				return new NewArrayInitExpression(type, expressions);
			}
			return new NewArrayBoundsExpression(type, expressions);
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.NewArrayExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00016A35 File Offset: 0x00014C35
		public sealed override Type Type { get; }

		/// <summary>Gets the bounds of the array if the value of the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayBounds" />, or the values to initialize the elements of the new array if the value of the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayInit" />.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent either the bounds of the array or the initialization values.</returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00016A3D File Offset: 0x00014C3D
		public ReadOnlyCollection<Expression> Expressions { get; }

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06000660 RID: 1632 RVA: 0x00016A45 File Offset: 0x00014C45
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNewArray(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expressions">The <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property of the result.</param>
		// Token: 0x06000661 RID: 1633 RVA: 0x00016A50 File Offset: 0x00014C50
		public NewArrayExpression Update(IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			if (ExpressionUtils.SameElements<Expression>(ref expressions, this.Expressions))
			{
				return this;
			}
			if (this.NodeType != ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayBounds(this.Type.GetElementType(), expressions);
			}
			return Expression.NewArrayInit(this.Type.GetElementType(), expressions);
		}
	}
}
