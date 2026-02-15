using System;
using System.Collections.Specialized;
using System.Configuration.Internal;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Configuration
{
	/// <summary>Provides access to configuration files for client applications. This class cannot be inherited.</summary>
	// Token: 0x02000019 RID: 25
	public static class ConfigurationManager
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004E54 File Offset: 0x00003054
		[MonoTODO("Evidence and version still needs work")]
		private static string GetAssemblyInfo(Assembly a)
		{
			object[] array = a.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
			string text;
			if (array != null && array.Length != 0)
			{
				text = ((AssemblyProductAttribute)array[0]).Product;
			}
			else
			{
				text = AppDomain.CurrentDomain.FriendlyName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("evidencehere");
			string text2 = stringBuilder.ToString();
			array = a.GetCustomAttributes(typeof(AssemblyVersionAttribute), false);
			string text3;
			if (array != null && array.Length != 0)
			{
				text3 = ((AssemblyVersionAttribute)array[0]).Version;
			}
			else
			{
				text3 = "1.0.0.0";
			}
			return Path.Combine(string.Format("{0}_{1}", text, text2), text3);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004EF0 File Offset: 0x000030F0
		internal static Configuration OpenExeConfigurationInternal(ConfigurationUserLevel userLevel, Assembly calling_assembly, string exePath)
		{
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			if (userLevel != ConfigurationUserLevel.None)
			{
				if (userLevel != ConfigurationUserLevel.PerUserRoaming)
				{
					if (userLevel != ConfigurationUserLevel.PerUserRoamingAndLocal)
					{
						goto IL_00EA;
					}
					exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigurationManager.GetAssemblyInfo(calling_assembly));
					exeConfigurationFileMap.LocalUserConfigFilename = Path.Combine(exeConfigurationFileMap.LocalUserConfigFilename, "user.config");
				}
				exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ConfigurationManager.GetAssemblyInfo(calling_assembly));
				exeConfigurationFileMap.RoamingUserConfigFilename = Path.Combine(exeConfigurationFileMap.RoamingUserConfigFilename, "user.config");
			}
			if (exePath == null || exePath.Length == 0)
			{
				exeConfigurationFileMap.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
			else
			{
				if (!Path.IsPathRooted(exePath))
				{
					exePath = Path.GetFullPath(exePath);
				}
				if (!File.Exists(exePath))
				{
					Exception ex = new ArgumentException("The specified path does not exist.", "exePath");
					throw new ConfigurationErrorsException("Error Initializing the configuration system:", ex);
				}
				exeConfigurationFileMap.ExeConfigFilename = exePath + ".config";
			}
			IL_00EA:
			return ConfigurationManager.ConfigurationFactory.Create(typeof(ExeConfigurationHost), new object[] { exeConfigurationFileMap, userLevel });
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000500E File Offset: 0x0000320E
		internal static IInternalConfigConfigurationFactory ConfigurationFactory
		{
			get
			{
				return ConfigurationManager.configFactory;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005015 File Offset: 0x00003215
		internal static IInternalConfigSystem ConfigurationSystem
		{
			get
			{
				return ConfigurationManager.configSystem;
			}
		}

		/// <summary>Retrieves a specified configuration section for the current application's default configuration.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object, or null if the section does not exist.</returns>
		/// <param name="sectionName">The configuration section path and name.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration file could not be loaded.</exception>
		// Token: 0x060000CC RID: 204 RVA: 0x0000501C File Offset: 0x0000321C
		public static object GetSection(string sectionName)
		{
			object section = ConfigurationManager.ConfigurationSystem.GetSection(sectionName);
			if (section is ConfigurationSection)
			{
				return ((ConfigurationSection)section).GetRuntimeObject();
			}
			return section;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.AppSettingsSection" /> data for the current application's default configuration.</summary>
		/// <returns>Returns a <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the contents of the <see cref="T:System.Configuration.AppSettingsSection" /> object for the current application's default configuration. </returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not retrieve a <see cref="T:System.Collections.Specialized.NameValueCollection" /> object with the application settings data.</exception>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000504A File Offset: 0x0000324A
		public static NameValueCollection AppSettings
		{
			get
			{
				return (NameValueCollection)ConfigurationManager.GetSection("appSettings");
			}
		}

		// Token: 0x04000062 RID: 98
		private static InternalConfigurationFactory configFactory = new InternalConfigurationFactory();

		// Token: 0x04000063 RID: 99
		private static IInternalConfigSystem configSystem = new ClientConfigurationSystem();

		// Token: 0x04000064 RID: 100
		private static object lockobj = new object();
	}
}
