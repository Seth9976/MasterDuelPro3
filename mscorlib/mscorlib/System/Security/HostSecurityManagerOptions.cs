using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the security policy components to be used by the host security manager.</summary>
	// Token: 0x02000320 RID: 800
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		/// <summary>Use none of the security policy components.</summary>
		// Token: 0x04000D16 RID: 3350
		None = 0,
		/// <summary>Use the application domain evidence.</summary>
		// Token: 0x04000D17 RID: 3351
		HostAppDomainEvidence = 1,
		/// <summary>Use the policy level specified in the <see cref="P:System.Security.HostSecurityManager.DomainPolicy" /> property.</summary>
		// Token: 0x04000D18 RID: 3352
		HostPolicyLevel = 2,
		/// <summary>Use the assembly evidence.</summary>
		// Token: 0x04000D19 RID: 3353
		HostAssemblyEvidence = 4,
		/// <summary>Route calls to the <see cref="M:System.Security.Policy.ApplicationSecurityManager.DetermineApplicationTrust(System.ActivationContext,System.Security.Policy.TrustManagerContext)" /> method to the <see cref="M:System.Security.HostSecurityManager.DetermineApplicationTrust(System.Security.Policy.Evidence,System.Security.Policy.Evidence,System.Security.Policy.TrustManagerContext)" /> method first.</summary>
		// Token: 0x04000D1A RID: 3354
		HostDetermineApplicationTrust = 8,
		/// <summary>Use the <see cref="M:System.Security.HostSecurityManager.ResolvePolicy(System.Security.Policy.Evidence)" /> method to resolve the application evidence.</summary>
		// Token: 0x04000D1B RID: 3355
		HostResolvePolicy = 16,
		/// <summary>Use all security policy components.</summary>
		// Token: 0x04000D1C RID: 3356
		AllFlags = 31
	}
}
