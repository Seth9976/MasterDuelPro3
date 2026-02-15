using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Provides a managed equivalent of an unmanaged host.</summary>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. See the Requirements section.</exception>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C0 RID: 448
	[ComVisible(true)]
	public class AppDomainManager : MarshalByRefObject
	{
		/// <summary>Gets the host security manager that participates in security decisions for the application domain.</summary>
		/// <returns>The host security manager.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x000082D2 File Offset: 0x000064D2
		public virtual HostSecurityManager HostSecurityManager
		{
			get
			{
				return null;
			}
		}
	}
}
