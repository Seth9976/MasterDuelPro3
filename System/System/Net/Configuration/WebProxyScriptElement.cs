using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure Web proxy scripts. This class cannot be inherited.</summary>
	// Token: 0x0200049A RID: 1178
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		// Token: 0x06001CBA RID: 7354 RVA: 0x0007D26C File Offset: 0x0007B46C
		static WebProxyScriptElement()
		{
			WebProxyScriptElement.properties.Add(WebProxyScriptElement.downloadTimeoutProp);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected override void PostDeserialize()
		{
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0007D2B9 File Offset: 0x0007B4B9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebProxyScriptElement.properties;
			}
		}

		// Token: 0x040013D0 RID: 5072
		private static ConfigurationProperty downloadTimeoutProp = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), new TimeSpan(0, 0, 2, 0));

		// Token: 0x040013D1 RID: 5073
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
