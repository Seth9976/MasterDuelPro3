using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web request modules. This class cannot be inherited.</summary>
	// Token: 0x0200049D RID: 1181
	public sealed class WebRequestModulesSection : ConfigurationSection
	{
		// Token: 0x06001CC6 RID: 7366 RVA: 0x0007D380 File Offset: 0x0007B580
		static WebRequestModulesSection()
		{
			WebRequestModulesSection.properties.Add(WebRequestModulesSection.webRequestModulesProp);
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0007D3B6 File Offset: 0x0007B5B6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModulesSection.properties;
			}
		}

		/// <summary>Gets the collection of Web request modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModuleElementCollection" /> containing the registered Web request modules. </returns>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0007D3BD File Offset: 0x0007B5BD
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public WebRequestModuleElementCollection WebRequestModules
		{
			get
			{
				return (WebRequestModuleElementCollection)base[WebRequestModulesSection.webRequestModulesProp];
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00002FA0 File Offset: 0x000011A0
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x00002FA0 File Offset: 0x000011A0
		[MonoTODO]
		protected override void InitializeDefault()
		{
		}

		// Token: 0x040013D5 RID: 5077
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040013D6 RID: 5078
		private static ConfigurationProperty webRequestModulesProp = new ConfigurationProperty("", typeof(WebRequestModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
