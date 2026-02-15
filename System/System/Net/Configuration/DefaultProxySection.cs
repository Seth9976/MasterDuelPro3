using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web proxy server usage. This class cannot be inherited.</summary>
	// Token: 0x0200048D RID: 1165
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x06001C95 RID: 7317 RVA: 0x0007CB94 File Offset: 0x0007AD94
		static DefaultProxySection()
		{
			DefaultProxySection.properties.Add(DefaultProxySection.bypassListProp);
			DefaultProxySection.properties.Add(DefaultProxySection.enabledProp);
			DefaultProxySection.properties.Add(DefaultProxySection.moduleProp);
			DefaultProxySection.properties.Add(DefaultProxySection.proxyProp);
			DefaultProxySection.properties.Add(DefaultProxySection.useDefaultCredentialsProp);
		}

		/// <summary>Gets the collection of resources that are not obtained using the Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.BypassElementCollection" /> that contains the addresses of resources that bypass the Web proxy server. </returns>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x0007CC82 File Offset: 0x0007AE82
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[DefaultProxySection.bypassListProp];
			}
		}

		/// <summary>Gets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement" />. The URI that identifies the Web proxy server.</returns>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0007CC94 File Offset: 0x0007AE94
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[DefaultProxySection.proxyProp];
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0007CCA6 File Offset: 0x0007AEA6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultProxySection.properties;
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00002FA0 File Offset: 0x000011A0
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00002FA0 File Offset: 0x000011A0
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x0400139C RID: 5020
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400139D RID: 5021
		private static ConfigurationProperty bypassListProp = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null);

		// Token: 0x0400139E RID: 5022
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x0400139F RID: 5023
		private static ConfigurationProperty moduleProp = new ConfigurationProperty("module", typeof(ModuleElement), null);

		// Token: 0x040013A0 RID: 5024
		private static ConfigurationProperty proxyProp = new ConfigurationProperty("proxy", typeof(ProxyElement), null);

		// Token: 0x040013A1 RID: 5025
		private static ConfigurationProperty useDefaultCredentialsProp = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false);
	}
}
