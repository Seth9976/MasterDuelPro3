using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for authentication modules. This class cannot be inherited.</summary>
	// Token: 0x02000486 RID: 1158
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		// Token: 0x06001C7C RID: 7292 RVA: 0x0007C8D8 File Offset: 0x0007AAD8
		static AuthenticationModulesSection()
		{
			AuthenticationModulesSection.properties.Add(AuthenticationModulesSection.authenticationModulesProp);
		}

		/// <summary>Gets the collection of authentication modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> that contains the registered authentication modules. </returns>
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0007C90E File Offset: 0x0007AB0E
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[AuthenticationModulesSection.authenticationModulesProp];
			}
		}

		// Token: 0x04001392 RID: 5010
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001393 RID: 5011
		private static ConfigurationProperty authenticationModulesProp = new ConfigurationProperty("", typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
