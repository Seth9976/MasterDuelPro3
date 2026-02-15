using System;

namespace System.Security.Policy
{
	/// <summary>Defines the method that creates a new identity permission.</summary>
	// Token: 0x02000333 RID: 819
	public interface IIdentityPermissionFactory
	{
		/// <summary>Creates a new identity permission for the specified evidence.</summary>
		/// <returns>The new identity permission.</returns>
		/// <param name="evidence">The evidence from which to create the new identity permission. </param>
		// Token: 0x06001D0F RID: 7439
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}
