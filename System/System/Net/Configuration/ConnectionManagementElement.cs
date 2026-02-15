using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum number of connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x02000489 RID: 1161
	public sealed class ConnectionManagementElement : ConfigurationElement
	{
		// Token: 0x06001C86 RID: 7302 RVA: 0x0007C998 File Offset: 0x0007AB98
		static ConnectionManagementElement()
		{
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.addressProp);
			ConnectionManagementElement.properties.Add(ConnectionManagementElement.maxConnectionProp);
		}

		/// <summary>Gets or sets the address for remote computers.</summary>
		/// <returns>A string that contains a regular expression describing an IP address or DNS name.</returns>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0007CA08 File Offset: 0x0007AC08
		[ConfigurationProperty("address", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Address
		{
			get
			{
				return (string)base[ConnectionManagementElement.addressProp];
			}
		}

		/// <summary>Gets or sets the maximum number of connections that can be made to a remote computer.</summary>
		/// <returns>An integer that specifies the maximum number of connections.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0007CA1A File Offset: 0x0007AC1A
		[ConfigurationProperty("maxconnection", DefaultValue = "6", Options = ConfigurationPropertyOptions.IsRequired)]
		public int MaxConnection
		{
			get
			{
				return (int)base[ConnectionManagementElement.maxConnectionProp];
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0007CA2C File Offset: 0x0007AC2C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementElement.properties;
			}
		}

		// Token: 0x04001396 RID: 5014
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001397 RID: 5015
		private static ConfigurationProperty addressProp = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001398 RID: 5016
		private static ConfigurationProperty maxConnectionProp = new ConfigurationProperty("maxconnection", typeof(int), 1, ConfigurationPropertyOptions.IsRequired);
	}
}
