using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides configuration and statistical information for a network interface.</summary>
	// Token: 0x0200045B RID: 1115
	public abstract class NetworkInterface
	{
		/// <summary>Returns objects that describe the network interfaces on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array that contains objects that describe the available network interfaces, or an empty array if no interfaces are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Windows system function call failed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Net.NetworkInformation.NetworkInformationPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Access="Read" />
		/// </PermissionSet>
		// Token: 0x06001C3C RID: 7228 RVA: 0x0007BEB2 File Offset: 0x0007A0B2
		public static NetworkInterface[] GetAllNetworkInterfaces()
		{
			return SystemNetworkInterface.GetNetworkInterfaces();
		}

		/// <summary>Returns an object that describes the configuration of this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPInterfaceProperties" /> object that describes this network interface.</returns>
		// Token: 0x06001C3D RID: 7229 RVA: 0x0000A4AB File Offset: 0x000086AB
		public virtual IPInterfaceProperties GetIPProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the interface type.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.NetworkInterfaceType" /> value that specifies the network interface type.</returns>
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0000A4AB File Offset: 0x000086AB
		public virtual NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
