using System;

namespace System.Net
{
	/// <summary>Provides storage for multiple credentials.</summary>
	// Token: 0x020003A2 RID: 930
	public class CredentialCache
	{
		/// <summary>Gets the system credentials of the application.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that represents the system credentials of the application.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00063ED8 File Offset: 0x000620D8
		public static ICredentials DefaultCredentials
		{
			get
			{
				return SystemNetworkCredential.defaultCredential;
			}
		}

		/// <summary>Gets the network credentials of the current security context.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkCredential" /> that represents the network credentials of the current user or application.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00063ED8 File Offset: 0x000620D8
		public static NetworkCredential DefaultNetworkCredentials
		{
			get
			{
				return SystemNetworkCredential.defaultCredential;
			}
		}
	}
}
