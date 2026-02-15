using System;

namespace Microsoft.Win32
{
	/// <summary>Represents the possible values for a top-level node on a foreign machine.</summary>
	// Token: 0x0200007E RID: 126
	public enum RegistryHive
	{
		/// <summary>Represents the HKEY_CLASSES_ROOT base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x0400023F RID: 575
		ClassesRoot = -2147483648,
		/// <summary>Represents the HKEY_CURRENT_USER base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x04000240 RID: 576
		CurrentUser,
		/// <summary>Represents the HKEY_LOCAL_MACHINE base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x04000241 RID: 577
		LocalMachine,
		/// <summary>Represents the HKEY_USERS base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x04000242 RID: 578
		Users,
		/// <summary>Represents the HKEY_PERFORMANCE_DATA base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x04000243 RID: 579
		PerformanceData,
		/// <summary>Represents the HKEY_CURRENT_CONFIG base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x04000244 RID: 580
		CurrentConfig,
		/// <summary>Represents the HKEY_DYN_DATA base key on another computer. This value can be passed to the <see cref="M:Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive,System.String)" /> method, to open this node remotely.</summary>
		// Token: 0x04000245 RID: 581
		DynData
	}
}
