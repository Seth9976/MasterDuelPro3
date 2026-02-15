using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing members of a member of a newly created object.</summary>
	// Token: 0x020000BB RID: 187
	public sealed class MemberMemberBinding : MemberBinding
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x000163EB File Offset: 0x000145EB
		internal MemberMemberBinding(MemberInfo member, ReadOnlyCollection<MemberBinding> bindings)
			: base(MemberBindingType.MemberBinding, member)
		{
			this.Bindings = bindings;
		}

		/// <summary>Gets the bindings that describe how to initialize the members of a member.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects that describe how to initialize the members of the member.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000163FC File Offset: 0x000145FC
		public ReadOnlyCollection<MemberBinding> Bindings { get; }

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="bindings">The <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> property of the result.</param>
		// Token: 0x0600061E RID: 1566 RVA: 0x00016404 File Offset: 0x00014604
		public MemberMemberBinding Update(IEnumerable<MemberBinding> bindings)
		{
			if (bindings != null && ExpressionUtils.SameElements<MemberBinding>(ref bindings, this.Bindings))
			{
				return this;
			}
			return Expression.MemberBind(base.Member, bindings);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000A01D File Offset: 0x0000821D
		internal override void ValidateAsDefinedHere(int index)
		{
		}
	}
}
