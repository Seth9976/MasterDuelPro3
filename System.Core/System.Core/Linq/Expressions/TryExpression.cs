using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents a try/catch/finally/fault block.</summary>
	// Token: 0x020000DA RID: 218
	[DebuggerTypeProxy(typeof(Expression.TryExpressionProxy))]
	public sealed class TryExpression : Expression
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x000176CC File Offset: 0x000158CC
		internal TryExpression(Type type, Expression body, Expression @finally, Expression fault, ReadOnlyCollection<CatchBlock> handlers)
		{
			this.Type = type;
			this.Body = body;
			this.Handlers = handlers;
			this.Finally = @finally;
			this.Fault = fault;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.TryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000176F9 File Offset: 0x000158F9
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00017701 File Offset: 0x00015901
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Try;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the body of the try block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the body of the try block.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00017705 File Offset: 0x00015905
		public Expression Body { get; }

		/// <summary>Gets the collection of <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions associated with the try block.</summary>
		/// <returns>The collection of <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions associated with the try block.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001770D File Offset: 0x0001590D
		public ReadOnlyCollection<CatchBlock> Handlers { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the finally block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the finally block.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00017715 File Offset: 0x00015915
		public Expression Finally { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> representing the fault block.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> representing the fault block.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001771D File Offset: 0x0001591D
		public Expression Fault { get; }

		// Token: 0x06000741 RID: 1857 RVA: 0x00017725 File Offset: 0x00015925
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTry(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.TryExpression.Body" /> property of the result.</param>
		/// <param name="handlers">The <see cref="P:System.Linq.Expressions.TryExpression.Handlers" /> property of the result.</param>
		/// <param name="finally">The <see cref="P:System.Linq.Expressions.TryExpression.Finally" /> property of the result.</param>
		/// <param name="fault">The <see cref="P:System.Linq.Expressions.TryExpression.Fault" /> property of the result.</param>
		// Token: 0x06000742 RID: 1858 RVA: 0x00017730 File Offset: 0x00015930
		public TryExpression Update(Expression body, IEnumerable<CatchBlock> handlers, Expression @finally, Expression fault)
		{
			if (((body == this.Body) & (@finally == this.Finally) & (fault == this.Fault)) && ExpressionUtils.SameElements<CatchBlock>(ref handlers, this.Handlers))
			{
				return this;
			}
			return Expression.MakeTry(this.Type, body, @finally, fault, handlers);
		}
	}
}
