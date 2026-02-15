using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents a control expression that handles multiple selections by passing control to <see cref="T:System.Linq.Expressions.SwitchCase" />.</summary>
	// Token: 0x020000D9 RID: 217
	[DebuggerTypeProxy(typeof(Expression.SwitchExpressionProxy))]
	public sealed class SwitchExpression : Expression
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x000175C1 File Offset: 0x000157C1
		internal SwitchExpression(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, ReadOnlyCollection<SwitchCase> cases)
		{
			this.Type = type;
			this.SwitchValue = switchValue;
			this.DefaultBody = defaultBody;
			this.Comparison = comparison;
			this.Cases = cases;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.SwitchExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000175EE File Offset: 0x000157EE
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this Expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000175F6 File Offset: 0x000157F6
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Switch;
			}
		}

		/// <summary>Gets the test for the switch.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the test for the switch.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000175FA File Offset: 0x000157FA
		public Expression SwitchValue { get; }

		/// <summary>Gets the collection of <see cref="T:System.Linq.Expressions.SwitchCase" /> objects for the switch.</summary>
		/// <returns>The collection of <see cref="T:System.Linq.Expressions.SwitchCase" /> objects.</returns>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00017602 File Offset: 0x00015802
		public ReadOnlyCollection<SwitchCase> Cases { get; }

		/// <summary>Gets the test for the switch.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the test for the switch.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001760A File Offset: 0x0001580A
		public Expression DefaultBody { get; }

		/// <summary>Gets the equality comparison method, if any.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> object representing the equality comparison method.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00017612 File Offset: 0x00015812
		public MethodInfo Comparison { get; }

		// Token: 0x06000737 RID: 1847 RVA: 0x0001761A File Offset: 0x0001581A
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitSwitch(this);
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00017624 File Offset: 0x00015824
		internal bool IsLifted
		{
			get
			{
				return this.SwitchValue.Type.IsNullableType() && (this.Comparison == null || !TypeUtils.AreEquivalent(this.SwitchValue.Type, this.Comparison.GetParametersCached()[0].ParameterType.GetNonRefType()));
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="switchValue">The <see cref="P:System.Linq.Expressions.SwitchExpression.SwitchValue" /> property of the result.</param>
		/// <param name="cases">The <see cref="P:System.Linq.Expressions.SwitchExpression.Cases" /> property of the result.</param>
		/// <param name="defaultBody">The <see cref="P:System.Linq.Expressions.SwitchExpression.DefaultBody" /> property of the result.</param>
		// Token: 0x06000739 RID: 1849 RVA: 0x00017680 File Offset: 0x00015880
		public SwitchExpression Update(Expression switchValue, IEnumerable<SwitchCase> cases, Expression defaultBody)
		{
			if (((switchValue == this.SwitchValue) & (defaultBody == this.DefaultBody) & (cases != null)) && ExpressionUtils.SameElements<SwitchCase>(ref cases, this.Cases))
			{
				return this;
			}
			return Expression.Switch(this.Type, switchValue, defaultBody, this.Comparison, cases);
		}
	}
}
