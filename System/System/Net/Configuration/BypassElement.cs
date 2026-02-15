using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the address information for resources that are not retrieved using a proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000487 RID: 1159
	public sealed class BypassElement : ConfigurationElement
	{
		// Token: 0x06001C7E RID: 7294 RVA: 0x0007C920 File Offset: 0x0007AB20
		static BypassElement()
		{
			BypassElement.properties.Add(BypassElement.addressProp);
		}

		/// <summary>Gets or sets the addresses of resources that bypass the proxy server.</summary>
		/// <returns>A string that identifies a resource.</returns>
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0007C956 File Offset: 0x0007AB56
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Address
		{
			get
			{
				return (string)base[BypassElement.addressProp];
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0007C968 File Offset: 0x0007AB68
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BypassElement.properties;
			}
		}

		// Token: 0x04001394 RID: 5012
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001395 RID: 5013
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
