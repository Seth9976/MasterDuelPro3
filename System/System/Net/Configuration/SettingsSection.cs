using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
	// Token: 0x02000498 RID: 1176
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06001CB3 RID: 7347 RVA: 0x0007D0CC File Offset: 0x0007B2CC
		static SettingsSection()
		{
			SettingsSection.properties.Add(SettingsSection.httpWebRequestProp);
			SettingsSection.properties.Add(SettingsSection.ipv6Prop);
			SettingsSection.properties.Add(SettingsSection.performanceCountersProp);
			SettingsSection.properties.Add(SettingsSection.servicePointManagerProp);
			SettingsSection.properties.Add(SettingsSection.socketProp);
			SettingsSection.properties.Add(SettingsSection.webProxyScriptProp);
		}

		/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.Ipv6Element" />.The configuration element that controls setting used by IPv6.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0007D1D3 File Offset: 0x0007B3D3
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[SettingsSection.ipv6Prop];
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0007D1E5 File Offset: 0x0007B3E5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingsSection.properties;
			}
		}

		// Token: 0x040013C6 RID: 5062
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040013C7 RID: 5063
		private static ConfigurationProperty httpWebRequestProp = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement));

		// Token: 0x040013C8 RID: 5064
		private static ConfigurationProperty ipv6Prop = new ConfigurationProperty("ipv6", typeof(Ipv6Element));

		// Token: 0x040013C9 RID: 5065
		private static ConfigurationProperty performanceCountersProp = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement));

		// Token: 0x040013CA RID: 5066
		private static ConfigurationProperty servicePointManagerProp = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement));

		// Token: 0x040013CB RID: 5067
		private static ConfigurationProperty webProxyScriptProp = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement));

		// Token: 0x040013CC RID: 5068
		private static ConfigurationProperty socketProp = new ConfigurationProperty("socket", typeof(SocketElement));
	}
}
