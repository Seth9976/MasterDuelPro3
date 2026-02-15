using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Represents the default value of a type or an empty expression.</summary>
	// Token: 0x02000093 RID: 147
	[DebuggerTypeProxy(typeof(Expression.DefaultExpressionProxy))]
	public sealed class DefaultExpression : Expression
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x000132A7 File Offset: 0x000114A7
		internal DefaultExpression(Type type)
		{
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.DefaultExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000132B6 File Offset: 0x000114B6
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000132BE File Offset: 0x000114BE
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Default;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000132C2 File Offset: 0x000114C2
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDefault(this);
		}
	}
}
