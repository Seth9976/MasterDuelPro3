using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the network connectivity of the local computer.</summary>
	// Token: 0x02000457 RID: 1111
	public abstract class IPGlobalProperties
	{
		/// <summary>Gets an object that provides information about the local computer's network connectivity and traffic statistics.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> object that contains information about the local computer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Net.NetworkInformation.NetworkInformationPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Access="Read" />
		/// </PermissionSet>
		// Token: 0x06001C31 RID: 7217 RVA: 0x0007BE9B File Offset: 0x0007A09B
		public static IPGlobalProperties GetIPGlobalProperties()
		{
			return IPGlobalPropertiesFactoryPal.Create();
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0007BEA2 File Offset: 0x0007A0A2
		internal static IPGlobalProperties InternalGetIPGlobalProperties()
		{
			return IPGlobalProperties.GetIPGlobalProperties();
		}

		/// <summary>Returns endpoint information about the Internet Protocol version 4 (IPv4) and IPv6 Transmission Control Protocol (TCP) listeners on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the active TCP listeners, or an empty array, if no active TCP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function GetTcpTable failed. </exception>
		// Token: 0x06001C33 RID: 7219
		public abstract IPEndPoint[] GetActiveTcpListeners();

		/// <summary>Gets the domain in which the local computer is registered.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's domain name. If the computer does not belong to a domain, returns <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed. </exception>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001C34 RID: 7220
		public abstract string DomainName { get; }
	}
}
