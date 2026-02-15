using System;

namespace System.Configuration
{
	/// <summary>Provides runtime versions 1.0 and 1.1 support for reading configuration sections and common configuration settings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011E RID: 286
	public sealed class ConfigurationSettings
	{
		/// <summary>Returns the <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object for the passed configuration section name and path.NoteThe <see cref="T:System.Configuration.ConfigurationSettings" /> class provides backward compatibility only. You should use the <see cref="T:System.Configuration.ConfigurationManager" /> class or <see cref="T:System.Web.Configuration.WebConfigurationManager" /> class instead.</returns>
		/// <param name="sectionName">A configuration name and path, such as "system.net/settings".</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">Unable to retrieve the requested section.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000584 RID: 1412 RVA: 0x0001C4DD File Offset: 0x0001A6DD
		[Obsolete("This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.GetSection")]
		public static object GetConfig(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}

		// Token: 0x040004B5 RID: 1205
		private static IConfigurationSystem config = DefaultConfig.GetInstance();

		// Token: 0x040004B6 RID: 1206
		private static object lockobj = new object();
	}
}
