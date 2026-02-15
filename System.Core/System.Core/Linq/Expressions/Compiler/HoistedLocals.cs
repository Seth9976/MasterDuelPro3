using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000EE RID: 238
	internal sealed class HoistedLocals
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x000197F8 File Offset: 0x000179F8
		internal HoistedLocals(HoistedLocals parent, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (parent != null)
			{
				vars = vars.AddFirst(parent.SelfVariable);
			}
			Dictionary<Expression, int> dictionary = new Dictionary<Expression, int>(vars.Count);
			for (int i = 0; i < vars.Count; i++)
			{
				dictionary.Add(vars[i], i);
			}
			this.SelfVariable = Expression.Variable(typeof(object[]), null);
			this.Parent = parent;
			this.Variables = vars;
			this.Indexes = new ReadOnlyDictionary<Expression, int>(dictionary);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00019877 File Offset: 0x00017A77
		internal ParameterExpression ParentVariable
		{
			get
			{
				HoistedLocals parent = this.Parent;
				if (parent == null)
				{
					return null;
				}
				return parent.SelfVariable;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001988A File Offset: 0x00017A8A
		internal static object[] GetParent(object[] locals)
		{
			return ((StrongBox<object[]>)locals[0]).Value;
		}

		// Token: 0x04000263 RID: 611
		internal readonly HoistedLocals Parent;

		// Token: 0x04000264 RID: 612
		internal readonly ReadOnlyDictionary<Expression, int> Indexes;

		// Token: 0x04000265 RID: 613
		internal readonly ReadOnlyCollection<ParameterExpression> Variables;

		// Token: 0x04000266 RID: 614
		internal readonly ParameterExpression SelfVariable;
	}
}
