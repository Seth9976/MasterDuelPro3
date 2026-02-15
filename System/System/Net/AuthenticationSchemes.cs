using System;

namespace System.Net
{
	/// <summary>Specifies protocols for authentication.</summary>
	// Token: 0x0200039F RID: 927
	[Flags]
	public enum AuthenticationSchemes
	{
		/// <summary>No authentication is allowed. A client requesting an <see cref="T:System.Net.HttpListener" /> object with this flag set will always receive a 403 Forbidden status. Use this flag when a resource should never be served to a client.</summary>
		// Token: 0x04000E5A RID: 3674
		None = 0,
		/// <summary>Specifies digest authentication.</summary>
		// Token: 0x04000E5B RID: 3675
		Digest = 1,
		/// <summary>Negotiates with the client to determine the authentication scheme. If both client and server support Kerberos, it is used; otherwise, NTLM is used.</summary>
		// Token: 0x04000E5C RID: 3676
		Negotiate = 2,
		/// <summary>Specifies NTLM authentication.</summary>
		// Token: 0x04000E5D RID: 3677
		Ntlm = 4,
		/// <summary>Specifies basic authentication. </summary>
		// Token: 0x04000E5E RID: 3678
		Basic = 8,
		/// <summary>Specifies anonymous authentication.</summary>
		// Token: 0x04000E5F RID: 3679
		Anonymous = 32768,
		/// <summary>Specifies Windows authentication.</summary>
		// Token: 0x04000E60 RID: 3680
		IntegratedWindowsAuthentication = 6
	}
}
