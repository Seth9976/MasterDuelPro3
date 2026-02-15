using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing the elements of a collection member of a newly created object.</summary>
	// Token: 0x020000BA RID: 186
	public sealed class MemberListBinding : MemberBinding
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x000163B0 File Offset: 0x000145B0
		internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers)
			: base(MemberBindingType.ListBinding, member)
		{
			this.Initializers = initializers;
		}

		/// <summary>Gets the element initializers for initializing a collection member of a newly created object.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection member with.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000163C1 File Offset: 0x000145C1
		public ReadOnlyCollection<ElementInit> Initializers { get; }

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="initializers">The <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> property of the result.</param>
		// Token: 0x0600061A RID: 1562 RVA: 0x000163C9 File Offset: 0x000145C9
		public MemberListBinding Update(IEnumerable<ElementInit> initializers)
		{
			if (initializers != null && ExpressionUtils.SameElements<ElementInit>(ref initializers, this.Initializers))
			{
				return this;
			}
			return Expression.ListBind(base.Member, initializers);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000A01D File Offset: 0x0000821D
		internal override void ValidateAsDefinedHere(int index)
		{
		}
	}
}
