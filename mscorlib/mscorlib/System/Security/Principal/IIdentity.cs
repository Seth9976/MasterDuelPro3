using System;

namespace System.Security.Principal
{
	/// <summary>Defines the basic functionality of an identity object.</summary>
	// Token: 0x020003D0 RID: 976
	public interface IIdentity
	{
		/// <summary>Gets the name of the current user.</summary>
		/// <returns>The name of the user on whose behalf the code is running.</returns>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06002151 RID: 8529
		string Name { get; }

		/// <summary>Gets the type of authentication used.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06002152 RID: 8530
		string AuthenticationType { get; }
	}
}
