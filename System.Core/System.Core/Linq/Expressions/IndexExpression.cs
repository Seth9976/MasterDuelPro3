using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents indexing a property or array.</summary>
	// Token: 0x0200009E RID: 158
	[DebuggerTypeProxy(typeof(Expression.IndexExpressionProxy))]
	public sealed class IndexExpression : Expression, IArgumentProvider
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x00015737 File Offset: 0x00013937
		internal IndexExpression(Expression instance, PropertyInfo indexer, IReadOnlyList<Expression> arguments)
		{
			indexer == null;
			this.Object = instance;
			this.Indexer = indexer;
			this._arguments = arguments;
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001575C File Offset: 0x0001395C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Index;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.IndexExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00015760 File Offset: 0x00013960
		public sealed override Type Type
		{
			get
			{
				if (this.Indexer != null)
				{
					return this.Indexer.PropertyType;
				}
				return this.Object.Type.GetElementType();
			}
		}

		/// <summary>An object to index.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the object to index.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001578C File Offset: 0x0001398C
		public Expression Object { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.PropertyInfo" /> for the property if the expression represents an indexed property, returns null otherwise.</summary>
		/// <returns>The <see cref="T:System.Reflection.PropertyInfo" /> for the property if the expression represents an indexed property, otherwise null.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00015794 File Offset: 0x00013994
		public PropertyInfo Indexer { get; }

		// Token: 0x06000577 RID: 1399 RVA: 0x0001579C File Offset: 0x0001399C
		public Expression GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000157AA File Offset: 0x000139AA
		public int ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000157B7 File Offset: 0x000139B7
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitIndex(this);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000157C0 File Offset: 0x000139C0
		internal Expression Rewrite(Expression instance, Expression[] arguments)
		{
			return Expression.MakeIndex(instance, this.Indexer, arguments ?? this._arguments);
		}

		// Token: 0x040001A9 RID: 425
		private IReadOnlyList<Expression> _arguments;
	}
}
