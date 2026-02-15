using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Allows the control and customization of security behavior for application domains.</summary>
	// Token: 0x0200031F RID: 799
	[ComVisible(true)]
	[Serializable]
	public class HostSecurityManager
	{
		/// <summary>Gets the flag representing the security policy components of concern to the host.</summary>
		/// <returns>One of the enumeration values that specifies security policy components. The default is <see cref="F:System.Security.HostSecurityManagerOptions.AllFlags" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0006F0E0 File Offset: 0x0006D2E0
		public virtual HostSecurityManagerOptions Flags
		{
			get
			{
				return HostSecurityManagerOptions.AllFlags;
			}
		}

		/// <summary>Provides the assembly evidence for an assembly being loaded.</summary>
		/// <returns>The evidence to be used for the assembly.</returns>
		/// <param name="loadedAssembly">The loaded assembly. </param>
		/// <param name="inputEvidence">Additional evidence to add to the assembly evidence.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001C7A RID: 7290 RVA: 0x0006565F File Offset: 0x0006385F
		public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
		{
			return inputEvidence;
		}
	}
}
