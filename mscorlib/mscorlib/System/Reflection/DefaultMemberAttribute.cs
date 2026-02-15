using System;

namespace System.Reflection
{
	/// <summary>Defines the member of a type that is the default member used by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />. </summary>
	// Token: 0x020005F6 RID: 1526
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class DefaultMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.DefaultMemberAttribute" /> class.</summary>
		/// <param name="memberName">A String containing the name of the member to invoke. This may be a constructor, method, property, or field. A suitable invocation attribute must be specified when the member is invoked. The default member of a class can be specified by passing an empty String as the name of the member.The default member of a type is marked with the DefaultMemberAttribute custom attribute or marked in COM in the usual way. </param>
		// Token: 0x06002CAB RID: 11435 RVA: 0x000B18D3 File Offset: 0x000AFAD3
		public DefaultMemberAttribute(string memberName)
		{
			this.MemberName = memberName;
		}

		/// <summary>Gets the name from the attribute.</summary>
		/// <returns>A string representing the member name.</returns>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000B18E2 File Offset: 0x000AFAE2
		public string MemberName { get; }
	}
}
