using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for connection management. This class cannot be inherited.</summary>
	// Token: 0x0200048C RID: 1164
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		// Token: 0x06001C91 RID: 7313 RVA: 0x0007CB45 File Offset: 0x0007AD45
		static ConnectionManagementSection()
		{
			ConnectionManagementSection.properties.Add(ConnectionManagementSection.connectionManagementProp);
		}

		/// <summary>Gets the collection of connection management objects in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> that contains the connection management information for the local computer. </returns>
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0007CB7B File Offset: 0x0007AD7B
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[ConnectionManagementSection.connectionManagementProp];
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0007CB8D File Offset: 0x0007AD8D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConnectionManagementSection.properties;
			}
		}

		// Token: 0x0400139A RID: 5018
		private static ConfigurationProperty connectionManagementProp = new ConfigurationProperty("ConnectionManagement", typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x0400139B RID: 5019
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
