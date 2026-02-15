using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents a set of binding restrictions on the <see cref="T:System.Dynamic.DynamicMetaObject" /> under which the dynamic binding is valid.</summary>
	// Token: 0x0200011F RID: 287
	[DebuggerTypeProxy(typeof(BindingRestrictions.BindingRestrictionsProxy))]
	[DebuggerDisplay("{DebugView}")]
	public abstract class BindingRestrictions
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00009F1D File Offset: 0x0000811D
		private BindingRestrictions()
		{
		}

		// Token: 0x060009AD RID: 2477
		internal abstract Expression GetExpression();

		/// <summary>Merges the set of binding restrictions with the current binding restrictions.</summary>
		/// <returns>The new set of binding restrictions.</returns>
		/// <param name="restrictions">The set of restrictions with which to merge the current binding restrictions.</param>
		// Token: 0x060009AE RID: 2478 RVA: 0x00026096 File Offset: 0x00024296
		public BindingRestrictions Merge(BindingRestrictions restrictions)
		{
			ContractUtils.RequiresNotNull(restrictions, "restrictions");
			if (this == BindingRestrictions.Empty)
			{
				return restrictions;
			}
			if (restrictions == BindingRestrictions.Empty)
			{
				return this;
			}
			return new BindingRestrictions.MergedRestriction(this, restrictions);
		}

		/// <summary>Creates the binding restriction that check the expression for runtime type identity.</summary>
		/// <returns>The new binding restrictions.</returns>
		/// <param name="expression">The expression to test.</param>
		/// <param name="type">The exact type to test.</param>
		// Token: 0x060009AF RID: 2479 RVA: 0x000260BE File Offset: 0x000242BE
		public static BindingRestrictions GetTypeRestriction(Expression expression, Type type)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			return new BindingRestrictions.TypeRestriction(expression, type);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000260DD File Offset: 0x000242DD
		internal static BindingRestrictions GetTypeRestriction(DynamicMetaObject obj)
		{
			if (obj.Value == null && obj.HasValue)
			{
				return BindingRestrictions.GetInstanceRestriction(obj.Expression, null);
			}
			return BindingRestrictions.GetTypeRestriction(obj.Expression, obj.LimitType);
		}

		/// <summary>Creates the binding restriction that checks the expression for object instance identity.</summary>
		/// <returns>The new binding restrictions.</returns>
		/// <param name="expression">The expression to test.</param>
		/// <param name="instance">The exact object instance to test.</param>
		// Token: 0x060009B1 RID: 2481 RVA: 0x0002610D File Offset: 0x0002430D
		public static BindingRestrictions GetInstanceRestriction(Expression expression, object instance)
		{
			ContractUtils.RequiresNotNull(expression, "expression");
			return new BindingRestrictions.InstanceRestriction(expression, instance);
		}

		/// <summary>Creates the <see cref="T:System.Linq.Expressions.Expression" /> representing the binding restrictions.</summary>
		/// <returns>The expression tree representing the restrictions.</returns>
		// Token: 0x060009B2 RID: 2482 RVA: 0x00026121 File Offset: 0x00024321
		public Expression ToExpression()
		{
			return this.GetExpression();
		}

		/// <summary>Represents an empty set of binding restrictions. This field is read only.</summary>
		// Token: 0x040002FC RID: 764
		public static readonly BindingRestrictions Empty = new BindingRestrictions.CustomRestriction(Utils.Constant(true));

		// Token: 0x02000120 RID: 288
		private sealed class TestBuilder
		{
			// Token: 0x060009B4 RID: 2484 RVA: 0x0002613B File Offset: 0x0002433B
			internal void Append(BindingRestrictions restrictions)
			{
				if (this._unique.Add(restrictions))
				{
					this.Push(restrictions.GetExpression(), 0);
				}
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x00026158 File Offset: 0x00024358
			internal Expression ToExpression()
			{
				Expression expression = this._tests.Pop().Node;
				while (this._tests.Count > 0)
				{
					expression = Expression.AndAlso(this._tests.Pop().Node, expression);
				}
				return expression;
			}

			// Token: 0x060009B6 RID: 2486 RVA: 0x000261A0 File Offset: 0x000243A0
			private void Push(Expression node, int depth)
			{
				while (this._tests.Count > 0 && this._tests.Peek().Depth == depth)
				{
					node = Expression.AndAlso(this._tests.Pop().Node, node);
					depth++;
				}
				this._tests.Push(new BindingRestrictions.TestBuilder.AndNode
				{
					Node = node,
					Depth = depth
				});
			}

			// Token: 0x040002FD RID: 765
			private readonly HashSet<BindingRestrictions> _unique = new HashSet<BindingRestrictions>();

			// Token: 0x040002FE RID: 766
			private readonly Stack<BindingRestrictions.TestBuilder.AndNode> _tests = new Stack<BindingRestrictions.TestBuilder.AndNode>();

			// Token: 0x02000121 RID: 289
			private struct AndNode
			{
				// Token: 0x040002FF RID: 767
				internal int Depth;

				// Token: 0x04000300 RID: 768
				internal Expression Node;
			}
		}

		// Token: 0x02000122 RID: 290
		private sealed class MergedRestriction : BindingRestrictions
		{
			// Token: 0x060009B8 RID: 2488 RVA: 0x0002622F File Offset: 0x0002442F
			internal MergedRestriction(BindingRestrictions left, BindingRestrictions right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x060009B9 RID: 2489 RVA: 0x00026248 File Offset: 0x00024448
			internal override Expression GetExpression()
			{
				BindingRestrictions.TestBuilder testBuilder = new BindingRestrictions.TestBuilder();
				Stack<BindingRestrictions> stack = new Stack<BindingRestrictions>();
				BindingRestrictions bindingRestrictions = this;
				for (;;)
				{
					BindingRestrictions.MergedRestriction mergedRestriction = bindingRestrictions as BindingRestrictions.MergedRestriction;
					if (mergedRestriction != null)
					{
						stack.Push(mergedRestriction.Right);
						bindingRestrictions = mergedRestriction.Left;
					}
					else
					{
						testBuilder.Append(bindingRestrictions);
						if (stack.Count == 0)
						{
							break;
						}
						bindingRestrictions = stack.Pop();
					}
				}
				return testBuilder.ToExpression();
			}

			// Token: 0x04000301 RID: 769
			internal readonly BindingRestrictions Left;

			// Token: 0x04000302 RID: 770
			internal readonly BindingRestrictions Right;
		}

		// Token: 0x02000123 RID: 291
		private sealed class CustomRestriction : BindingRestrictions
		{
			// Token: 0x060009BA RID: 2490 RVA: 0x000262A0 File Offset: 0x000244A0
			internal CustomRestriction(Expression expression)
			{
				this._expression = expression;
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x000262AF File Offset: 0x000244AF
			public override bool Equals(object obj)
			{
				BindingRestrictions.CustomRestriction customRestriction = obj as BindingRestrictions.CustomRestriction;
				return ((customRestriction != null) ? customRestriction._expression : null) == this._expression;
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x000262CB File Offset: 0x000244CB
			public override int GetHashCode()
			{
				return 613566756 ^ this._expression.GetHashCode();
			}

			// Token: 0x060009BD RID: 2493 RVA: 0x000262DE File Offset: 0x000244DE
			internal override Expression GetExpression()
			{
				return this._expression;
			}

			// Token: 0x04000303 RID: 771
			private readonly Expression _expression;
		}

		// Token: 0x02000124 RID: 292
		private sealed class TypeRestriction : BindingRestrictions
		{
			// Token: 0x060009BE RID: 2494 RVA: 0x000262E6 File Offset: 0x000244E6
			internal TypeRestriction(Expression parameter, Type type)
			{
				this._expression = parameter;
				this._type = type;
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x000262FC File Offset: 0x000244FC
			public override bool Equals(object obj)
			{
				BindingRestrictions.TypeRestriction typeRestriction = obj as BindingRestrictions.TypeRestriction;
				return ((typeRestriction != null) ? typeRestriction._expression : null) == this._expression && TypeUtils.AreEquivalent(typeRestriction._type, this._type);
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x00026337 File Offset: 0x00024537
			public override int GetHashCode()
			{
				return 1227133513 ^ this._expression.GetHashCode() ^ this._type.GetHashCode();
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x00026356 File Offset: 0x00024556
			internal override Expression GetExpression()
			{
				return Expression.TypeEqual(this._expression, this._type);
			}

			// Token: 0x04000304 RID: 772
			private readonly Expression _expression;

			// Token: 0x04000305 RID: 773
			private readonly Type _type;
		}

		// Token: 0x02000125 RID: 293
		private sealed class InstanceRestriction : BindingRestrictions
		{
			// Token: 0x060009C2 RID: 2498 RVA: 0x00026369 File Offset: 0x00024569
			internal InstanceRestriction(Expression parameter, object instance)
			{
				this._expression = parameter;
				this._instance = instance;
			}

			// Token: 0x060009C3 RID: 2499 RVA: 0x00026380 File Offset: 0x00024580
			public override bool Equals(object obj)
			{
				BindingRestrictions.InstanceRestriction instanceRestriction = obj as BindingRestrictions.InstanceRestriction;
				return ((instanceRestriction != null) ? instanceRestriction._expression : null) == this._expression && instanceRestriction._instance == this._instance;
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x000263B8 File Offset: 0x000245B8
			public override int GetHashCode()
			{
				return -1840700270 ^ RuntimeHelpers.GetHashCode(this._instance) ^ this._expression.GetHashCode();
			}

			// Token: 0x060009C5 RID: 2501 RVA: 0x000263D8 File Offset: 0x000245D8
			internal override Expression GetExpression()
			{
				if (this._instance == null)
				{
					return Expression.Equal(Expression.Convert(this._expression, typeof(object)), Utils.Null);
				}
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					Expression.Assign(parameterExpression, Expression.Constant(this._instance, typeof(object))),
					Expression.AndAlso(Expression.NotEqual(parameterExpression, Utils.Null), Expression.Equal(Expression.Convert(this._expression, typeof(object)), parameterExpression))
				}));
			}

			// Token: 0x04000306 RID: 774
			private readonly Expression _expression;

			// Token: 0x04000307 RID: 775
			private readonly object _instance;
		}

		// Token: 0x02000126 RID: 294
		private sealed class BindingRestrictionsProxy
		{
		}
	}
}
