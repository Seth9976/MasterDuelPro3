using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a constant value.</summary>
	// Token: 0x02000090 RID: 144
	[DebuggerTypeProxy(typeof(Expression.ConstantExpressionProxy))]
	public class ConstantExpression : Expression
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0001324B File Offset: 0x0001144B
		internal ConstantExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ConstantExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001325A File Offset: 0x0001145A
		public override Type Type
		{
			get
			{
				if (this.Value == null)
				{
					return typeof(object);
				}
				return this.Value.GetType();
			}
		}

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001327A File Offset: 0x0001147A
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Constant;
			}
		}

		/// <summary>Gets the value of the constant expression.</summary>
		/// <returns>An <see cref="T:System.Object" /> equal to the value of the represented expression.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001327E File Offset: 0x0001147E
		public object Value { get; }

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x0600044A RID: 1098 RVA: 0x00013286 File Offset: 0x00011486
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConstant(this);
		}
	}
}
