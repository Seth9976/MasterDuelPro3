using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Identifies the configuration settings for Web proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000493 RID: 1171
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x06001CA9 RID: 7337 RVA: 0x0007CE64 File Offset: 0x0007B064
		static ProxyElement()
		{
			ProxyElement.properties.Add(ProxyElement.autoDetectProp);
			ProxyElement.properties.Add(ProxyElement.bypassOnLocalProp);
			ProxyElement.properties.Add(ProxyElement.proxyAddressProp);
			ProxyElement.properties.Add(ProxyElement.scriptLocationProp);
			ProxyElement.properties.Add(ProxyElement.useSystemDefaultProp);
		}

		/// <summary>Gets or sets a value that indicates whether local resources are retrieved by using a Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ProxyElement.BypassOnLocalValues" />.Avalue that indicates whether local resources are retrieved by using a Web proxy server.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0007CF57 File Offset: 0x0007B157
		[ConfigurationProperty("bypassonlocal", DefaultValue = "Unspecified")]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)base[ProxyElement.bypassOnLocalProp];
			}
		}

		/// <summary>Gets or sets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a URI.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0007CF69 File Offset: 0x0007B169
		[ConfigurationProperty("proxyaddress")]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base[ProxyElement.proxyAddressProp];
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the Internet Explorer Web proxy settings are used.</summary>
		/// <returns>true if the Internet Explorer LAN settings are used to detect and configure the default <see cref="T:System.Net.WebProxy" /> used for requests; otherwise, false.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0007CF7B File Offset: 0x0007B17B
		[ConfigurationProperty("usesystemdefault", DefaultValue = "Unspecified")]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)base[ProxyElement.useSystemDefaultProp];
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0007CF8D File Offset: 0x0007B18D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProxyElement.properties;
			}
		}

		// Token: 0x040013AD RID: 5037
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040013AE RID: 5038
		private static ConfigurationProperty autoDetectProp = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified);

		// Token: 0x040013AF RID: 5039
		private static ConfigurationProperty bypassOnLocalProp = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified);

		// Token: 0x040013B0 RID: 5040
		private static ConfigurationProperty proxyAddressProp = new ConfigurationProperty("proxyaddress", typeof(Uri), null);

		// Token: 0x040013B1 RID: 5041
		private static ConfigurationProperty scriptLocationProp = new ConfigurationProperty("scriptLocation", typeof(Uri), null);

		// Token: 0x040013B2 RID: 5042
		private static ConfigurationProperty useSystemDefaultProp = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified);

		/// <summary>Specifies whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000494 RID: 1172
		public enum BypassOnLocalValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x040013B4 RID: 5044
			Unspecified = -1,
			/// <summary>Access local resources directly.</summary>
			// Token: 0x040013B5 RID: 5045
			True = 1,
			/// <summary>All requests for local resources should go through the proxy</summary>
			// Token: 0x040013B6 RID: 5046
			False = 0
		}

		/// <summary>Specifies whether to use the local system proxy settings to determine whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000495 RID: 1173
		public enum UseSystemDefaultValues
		{
			/// <summary>The system default proxy setting is unspecified.</summary>
			// Token: 0x040013B8 RID: 5048
			Unspecified = -1,
			/// <summary>Use system default proxy setting values.</summary>
			// Token: 0x040013B9 RID: 5049
			True = 1,
			/// <summary>Do not use system default proxy setting values</summary>
			// Token: 0x040013BA RID: 5050
			False = 0
		}

		/// <summary>Specifies whether the proxy is automatically detected.</summary>
		// Token: 0x02000496 RID: 1174
		public enum AutoDetectValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x040013BC RID: 5052
			Unspecified = -1,
			/// <summary>The proxy is automatically detected.</summary>
			// Token: 0x040013BD RID: 5053
			True = 1,
			/// <summary>The proxy is not automatically detected.</summary>
			// Token: 0x040013BE RID: 5054
			False = 0
		}
	}
}
