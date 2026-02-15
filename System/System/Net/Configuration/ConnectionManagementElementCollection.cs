using System;
using System.Configuration;
using System.Reflection;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for connection management configuration elements. This class cannot be inherited.</summary>
	// Token: 0x0200048A RID: 1162
	[DefaultMember("Item")]
	[ConfigurationCollection(typeof(ConnectionManagementElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		// Token: 0x06001C8C RID: 7308 RVA: 0x0007CA33 File Offset: 0x0007AC33
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0007CA3A File Offset: 0x0007AC3A
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is ConnectionManagementElement))
			{
				throw new ArgumentException("element");
			}
			return ((ConnectionManagementElement)element).Address;
		}
	}
}
