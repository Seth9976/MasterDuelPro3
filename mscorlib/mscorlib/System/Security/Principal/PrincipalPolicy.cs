using System;

namespace System.Security.Principal
{
	/// <summary>Specifies how principal and identity objects should be created for an application domain. The default is UnauthenticatedPrincipal.</summary>
	// Token: 0x020003D2 RID: 978
	public enum PrincipalPolicy
	{
		/// <summary>Principal and identity objects for the unauthenticated entity should be created. An unauthenticated entity has <see cref="P:System.Security.Principal.GenericIdentity.Name" /> set to the empty string ("") and <see cref="P:System.Security.Principal.GenericIdentity.IsAuthenticated" /> set to false.</summary>
		// Token: 0x04000F96 RID: 3990
		UnauthenticatedPrincipal,
		/// <summary>No principal or identity objects should be created.</summary>
		// Token: 0x04000F97 RID: 3991
		NoPrincipal,
		/// <summary>Principal and identity objects that reflect the operating system token associated with the current execution thread should be created, and the associated operating system groups should be mapped into roles.</summary>
		// Token: 0x04000F98 RID: 3992
		WindowsPrincipal
	}
}
