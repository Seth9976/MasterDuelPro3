using System;

namespace System.Reflection
{
	/// <summary>Marks each type of member that is defined as a derived class of MemberInfo.</summary>
	// Token: 0x02000609 RID: 1545
	[Flags]
	public enum MemberTypes
	{
		/// <summary>Specifies that the member is a constructor, representing a <see cref="T:System.Reflection.ConstructorInfo" /> member. Hexadecimal value of 0x01.</summary>
		// Token: 0x0400171F RID: 5919
		Constructor = 1,
		/// <summary>Specifies that the member is an event, representing an <see cref="T:System.Reflection.EventInfo" /> member. Hexadecimal value of 0x02.</summary>
		// Token: 0x04001720 RID: 5920
		Event = 2,
		/// <summary>Specifies that the member is a field, representing a <see cref="T:System.Reflection.FieldInfo" /> member. Hexadecimal value of 0x04.</summary>
		// Token: 0x04001721 RID: 5921
		Field = 4,
		/// <summary>Specifies that the member is a method, representing a <see cref="T:System.Reflection.MethodInfo" /> member. Hexadecimal value of 0x08.</summary>
		// Token: 0x04001722 RID: 5922
		Method = 8,
		/// <summary>Specifies that the member is a property, representing a <see cref="T:System.Reflection.PropertyInfo" /> member. Hexadecimal value of 0x10.</summary>
		// Token: 0x04001723 RID: 5923
		Property = 16,
		/// <summary>Specifies that the member is a type, representing a <see cref="F:System.Reflection.MemberTypes.TypeInfo" /> member. Hexadecimal value of 0x20.</summary>
		// Token: 0x04001724 RID: 5924
		TypeInfo = 32,
		/// <summary>Specifies that the member is a custom member type. Hexadecimal value of 0x40.</summary>
		// Token: 0x04001725 RID: 5925
		Custom = 64,
		/// <summary>Specifies that the member is a nested type, extending <see cref="T:System.Reflection.MemberInfo" />.</summary>
		// Token: 0x04001726 RID: 5926
		NestedType = 128,
		/// <summary>Specifies all member types.</summary>
		// Token: 0x04001727 RID: 5927
		All = 191
	}
}
