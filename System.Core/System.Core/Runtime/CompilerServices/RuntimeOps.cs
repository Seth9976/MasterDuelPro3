using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Linq.Expressions.Compiler;
using System.Reflection;

namespace System.Runtime.CompilerServices
{
	/// <summary>Contains helper methods called from dynamically generated methods.</summary>
	// Token: 0x0200010C RID: 268
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	public static class RuntimeOps
	{
		/// <summary>Gets the value of an item in an expando object.</summary>
		/// <returns>True if the member exists in the expando object, otherwise false.</returns>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		/// <param name="value">The out parameter containing the value of the member.</param>
		// Token: 0x0600093A RID: 2362 RVA: 0x0002469D File Offset: 0x0002289D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static bool ExpandoTryGetValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			return expando.TryGetValue(indexClass, index, name, ignoreCase, out value);
		}

		/// <summary>Sets the value of an item in an expando object.</summary>
		/// <returns>Returns the index for the set member.</returns>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="value">The value of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x0600093B RID: 2363 RVA: 0x000246AC File Offset: 0x000228AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static object ExpandoTrySetValue(ExpandoObject expando, object indexClass, int index, object value, string name, bool ignoreCase)
		{
			expando.TrySetValue(indexClass, index, value, name, ignoreCase, false);
			return value;
		}

		/// <summary>Deletes the value of an item in an expando object.</summary>
		/// <returns>true if the item was successfully removed; otherwise, false.</returns>
		/// <param name="expando">The expando object.</param>
		/// <param name="indexClass">The class of the expando object.</param>
		/// <param name="index">The index of the member.</param>
		/// <param name="name">The name of the member.</param>
		/// <param name="ignoreCase">true if the name should be matched ignoring case; false otherwise.</param>
		// Token: 0x0600093C RID: 2364 RVA: 0x000246BD File Offset: 0x000228BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static bool ExpandoTryDeleteValue(ExpandoObject expando, object indexClass, int index, string name, bool ignoreCase)
		{
			return expando.TryDeleteValue(indexClass, index, name, ignoreCase, ExpandoObject.Uninitialized);
		}

		/// <summary>Checks the version of the Expando object.</summary>
		/// <returns>Returns true if the version is equal; otherwise, false.</returns>
		/// <param name="expando">The Expando object.</param>
		/// <param name="version">The version to check.</param>
		// Token: 0x0600093D RID: 2365 RVA: 0x000246CF File Offset: 0x000228CF
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool ExpandoCheckVersion(ExpandoObject expando, object version)
		{
			return expando.Class == version;
		}

		/// <summary>Promotes an Expando object from one class to a new class.</summary>
		/// <param name="expando">The Expando object.</param>
		/// <param name="oldClass">The old class of the Expando object.</param>
		/// <param name="newClass">The new class of the Expando object.</param>
		// Token: 0x0600093E RID: 2366 RVA: 0x000246DA File Offset: 0x000228DA
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void ExpandoPromoteClass(ExpandoObject expando, object oldClass, object newClass)
		{
			expando.PromoteClass(oldClass, newClass);
		}

		/// <summary>Quotes the provided expression tree.</summary>
		/// <returns>The quoted expression.</returns>
		/// <param name="expression">The expression to quote.</param>
		/// <param name="hoistedLocals">The hoisted local state provided by the compiler.</param>
		/// <param name="locals">The actual hoisted local values.</param>
		// Token: 0x0600093F RID: 2367 RVA: 0x000246E4 File Offset: 0x000228E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static Expression Quote(Expression expression, object hoistedLocals, object[] locals)
		{
			return new RuntimeOps.ExpressionQuoter((HoistedLocals)hoistedLocals, locals).Visit(expression);
		}

		/// <summary>Creates an interface that can be used to modify closed over variables at runtime.</summary>
		/// <returns>An interface to access variables.</returns>
		/// <param name="data">The closure array.</param>
		/// <param name="indexes">An array of indicies into the closure array where variables are found.</param>
		// Token: 0x06000940 RID: 2368 RVA: 0x000246F8 File Offset: 0x000228F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("do not use this method", true)]
		public static IRuntimeVariables CreateRuntimeVariables(object[] data, long[] indexes)
		{
			return new RuntimeOps.RuntimeVariableList(data, indexes);
		}

		/// <summary>Creates an interface that can be used to modify closed over variables at runtime.</summary>
		/// <returns>An interface to access variables.</returns>
		// Token: 0x06000941 RID: 2369 RVA: 0x00024701 File Offset: 0x00022901
		[Obsolete("do not use this method", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IRuntimeVariables CreateRuntimeVariables()
		{
			return new RuntimeOps.EmptyRuntimeVariables();
		}

		// Token: 0x0200010D RID: 269
		private sealed class ExpressionQuoter : ExpressionVisitor
		{
			// Token: 0x06000942 RID: 2370 RVA: 0x00024708 File Offset: 0x00022908
			internal ExpressionQuoter(HoistedLocals scope, object[] locals)
			{
				this._scope = scope;
				this._locals = locals;
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x0002472C File Offset: 0x0002292C
			protected internal override Expression VisitLambda<T>(Expression<T> node)
			{
				if (node.ParameterCount > 0)
				{
					HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
					int i = 0;
					int parameterCount = node.ParameterCount;
					while (i < parameterCount)
					{
						hashSet.Add(node.GetParameter(i));
						i++;
					}
					this._shadowedVars.Push(hashSet);
				}
				Expression expression = this.Visit(node.Body);
				if (node.ParameterCount > 0)
				{
					this._shadowedVars.Pop();
				}
				if (expression == node.Body)
				{
					return node;
				}
				return node.Rewrite(expression, null);
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x000247AC File Offset: 0x000229AC
			protected internal override Expression VisitBlock(BlockExpression node)
			{
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Push(new HashSet<ParameterExpression>(node.Variables));
				}
				Expression[] array = ExpressionVisitorUtils.VisitBlockExpressions(this, node);
				if (node.Variables.Count > 0)
				{
					this._shadowedVars.Pop();
				}
				if (array == null)
				{
					return node;
				}
				return node.Rewrite(node.Variables, array);
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x00024814 File Offset: 0x00022A14
			protected override CatchBlock VisitCatchBlock(CatchBlock node)
			{
				if (node.Variable != null)
				{
					this._shadowedVars.Push(new HashSet<ParameterExpression> { node.Variable });
				}
				Expression expression = this.Visit(node.Body);
				Expression expression2 = this.Visit(node.Filter);
				if (node.Variable != null)
				{
					this._shadowedVars.Pop();
				}
				if (expression == node.Body && expression2 == node.Filter)
				{
					return node;
				}
				return Expression.MakeCatchBlock(node.Test, node.Variable, expression, expression2);
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x0002489C File Offset: 0x00022A9C
			protected internal override Expression VisitParameter(ParameterExpression node)
			{
				IStrongBox box = this.GetBox(node);
				if (box == null)
				{
					return node;
				}
				return Expression.Field(Expression.Constant(box), "Value");
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x000248C8 File Offset: 0x00022AC8
			private IStrongBox GetBox(ParameterExpression variable)
			{
				using (Stack<HashSet<ParameterExpression>>.Enumerator enumerator = this._shadowedVars.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Contains(variable))
						{
							return null;
						}
					}
				}
				HoistedLocals hoistedLocals = this._scope;
				object[] array = this._locals;
				int num;
				while (!hoistedLocals.Indexes.TryGetValue(variable, out num))
				{
					hoistedLocals = hoistedLocals.Parent;
					if (hoistedLocals == null)
					{
						throw ContractUtils.Unreachable;
					}
					array = HoistedLocals.GetParent(array);
				}
				return (IStrongBox)array[num];
			}

			// Token: 0x040002DA RID: 730
			private readonly HoistedLocals _scope;

			// Token: 0x040002DB RID: 731
			private readonly object[] _locals;

			// Token: 0x040002DC RID: 732
			private readonly Stack<HashSet<ParameterExpression>> _shadowedVars = new Stack<HashSet<ParameterExpression>>();
		}

		// Token: 0x0200010E RID: 270
		private sealed class EmptyRuntimeVariables : IRuntimeVariables
		{
		}

		// Token: 0x0200010F RID: 271
		[DefaultMember("Item")]
		private sealed class RuntimeVariableList : IRuntimeVariables
		{
			// Token: 0x06000949 RID: 2377 RVA: 0x00024964 File Offset: 0x00022B64
			internal RuntimeVariableList(object[] data, long[] indexes)
			{
				this._data = data;
				this._indexes = indexes;
			}

			// Token: 0x040002DD RID: 733
			private readonly object[] _data;

			// Token: 0x040002DE RID: 734
			private readonly long[] _indexes;
		}
	}
}
