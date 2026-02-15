using System;

namespace Microsoft.Win32
{
	/// <summary>Provides <see cref="T:Microsoft.Win32.RegistryKey" /> objects that represent the root keys in the Windows registry, and static methods to access key/value pairs.</summary>
	// Token: 0x0200007D RID: 125
	public static class Registry
	{
		/// <summary>Retrieves the value associated with the specified name, in the specified registry key. If the name is not found in the specified key, returns a default value that you provide, or null if the specified key does not exist. </summary>
		/// <returns>null if the subkey specified by <paramref name="keyName" /> does not exist; otherwise, the value associated with <paramref name="valueName" />, or <paramref name="defaultValue" /> if <paramref name="valueName" /> is not found.</returns>
		/// <param name="keyName">The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".</param>
		/// <param name="valueName">The name of the name/value pair.</param>
		/// <param name="defaultValue">The value to return if <paramref name="valueName" /> does not exist.</param>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry key. </exception>
		/// <exception cref="T:System.IO.IOException">The <see cref="T:Microsoft.Win32.RegistryKey" /> that contains the specified value has been marked for deletion. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keyName" /> does not begin with a valid registry root. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="\" />
		/// </PermissionSet>
		// Token: 0x0600025B RID: 603 RVA: 0x0000F354 File Offset: 0x0000D554
		public static object GetValue(string keyName, string valueName, object defaultValue)
		{
			string text;
			object obj;
			using (RegistryKey registryKey = Registry.GetBaseKeyFromKeyName(keyName, out text).OpenSubKey(text))
			{
				obj = ((registryKey != null) ? registryKey.GetValue(valueName, defaultValue) : null);
			}
			return obj;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000F39C File Offset: 0x0000D59C
		private static RegistryKey GetBaseKeyFromKeyName(string keyName, out string subKeyName)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			int num = keyName.IndexOf('\\');
			int num2 = ((num != -1) ? num : keyName.Length);
			RegistryKey registryKey = null;
			if (num2 != 10)
			{
				switch (num2)
				{
				case 17:
					registryKey = ((char.ToUpperInvariant(keyName[6]) == 'L') ? Registry.ClassesRoot : Registry.CurrentUser);
					break;
				case 18:
					registryKey = Registry.LocalMachine;
					break;
				case 19:
					registryKey = Registry.CurrentConfig;
					break;
				case 21:
					registryKey = Registry.PerformanceData;
					break;
				}
			}
			else
			{
				registryKey = Registry.Users;
			}
			if (registryKey != null && keyName.StartsWith(registryKey.Name, StringComparison.OrdinalIgnoreCase))
			{
				subKeyName = ((num == -1 || num == keyName.Length) ? string.Empty : keyName.Substring(num + 1, keyName.Length - num - 1));
				return registryKey;
			}
			throw new ArgumentException(SR.Format("Registry key name must start with a valid base key name.", "keyName"), "keyName");
		}

		/// <summary>Contains information about the current user preferences. This field reads the Windows registry base key HKEY_CURRENT_USER </summary>
		// Token: 0x04000237 RID: 567
		public static readonly RegistryKey CurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);

		/// <summary>Contains the configuration data for the local machine. This field reads the Windows registry base key HKEY_LOCAL_MACHINE.</summary>
		// Token: 0x04000238 RID: 568
		public static readonly RegistryKey LocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);

		/// <summary>Defines the types (or classes) of documents and the properties associated with those types. This field reads the Windows registry base key HKEY_CLASSES_ROOT.</summary>
		// Token: 0x04000239 RID: 569
		public static readonly RegistryKey ClassesRoot = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default);

		/// <summary>Contains information about the default user configuration. This field reads the Windows registry base key HKEY_USERS.</summary>
		// Token: 0x0400023A RID: 570
		public static readonly RegistryKey Users = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default);

		/// <summary>Contains performance information for software components. This field reads the Windows registry base key HKEY_PERFORMANCE_DATA.</summary>
		// Token: 0x0400023B RID: 571
		public static readonly RegistryKey PerformanceData = RegistryKey.OpenBaseKey(RegistryHive.PerformanceData, RegistryView.Default);

		/// <summary>Contains configuration information pertaining to the hardware that is not specific to the user. This field reads the Windows registry base key HKEY_CURRENT_CONFIG.</summary>
		// Token: 0x0400023C RID: 572
		public static readonly RegistryKey CurrentConfig = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Default);

		/// <summary>Contains dynamic registry data. This field reads the Windows registry base key HKEY_DYN_DATA.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The operating system does not support dynamic data; that is, it is not Windows 98, Windows 98 Second Edition, or Windows Millennium Edition (Windows Me).</exception>
		// Token: 0x0400023D RID: 573
		[Obsolete("Use PerformanceData instead")]
		public static readonly RegistryKey DynData = RegistryKey.OpenBaseKey(RegistryHive.DynData, RegistryView.Default);
	}
}
