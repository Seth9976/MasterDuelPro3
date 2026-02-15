using System;

namespace System
{
	/// <summary>Defines host name types for the <see cref="M:System.Uri.CheckHostName(System.String)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FF RID: 255
	public enum UriHostNameType
	{
		/// <summary>The type of the host name is not supplied.</summary>
		// Token: 0x0400043A RID: 1082
		Unknown,
		/// <summary>The host is set, but the type cannot be determined.</summary>
		// Token: 0x0400043B RID: 1083
		Basic,
		/// <summary>The host name is a domain name system (DNS) style host name.</summary>
		// Token: 0x0400043C RID: 1084
		Dns,
		/// <summary>The host name is an Internet Protocol (IP) version 4 host address.</summary>
		// Token: 0x0400043D RID: 1085
		IPv4,
		/// <summary>The host name is an Internet Protocol (IP) version 6 host address.</summary>
		// Token: 0x0400043E RID: 1086
		IPv6
	}
}
