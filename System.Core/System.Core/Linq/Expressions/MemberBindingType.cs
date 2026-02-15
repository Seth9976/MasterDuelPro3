using System;

namespace System.Linq.Expressions
{
	/// <summary>Describes the binding types that are used in <see cref="T:System.Linq.Expressions.MemberInitExpression" /> objects.</summary>
	// Token: 0x020000B4 RID: 180
	public enum MemberBindingType
	{
		/// <summary>A binding that represents initializing a member with the value of an expression.</summary>
		// Token: 0x040001D3 RID: 467
		Assignment,
		/// <summary>A binding that represents recursively initializing members of a member.</summary>
		// Token: 0x040001D4 RID: 468
		MemberBinding,
		/// <summary>A binding that represents initializing a member of type <see cref="T:System.Collections.IList" /> or <see cref="T:System.Collections.Generic.ICollection`1" /> from a list of elements.</summary>
		// Token: 0x040001D5 RID: 469
		ListBinding
	}
}
