using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4) or Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x02000458 RID: 1112
	public abstract class IPInterfaceProperties
	{
		/// <summary>Gets the addresses of Domain Name System (DNS) servers for this interface.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the DNS server addresses.</returns>
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001C36 RID: 7222
		public abstract IPAddressCollection DnsAddresses { get; }
	}
}
