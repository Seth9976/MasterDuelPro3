using System;
using System.Configuration;
using System.Reflection;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for the addresses of resources that bypass the proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000488 RID: 1160
	[DefaultMember("Item")]
	[ConfigurationCollection(typeof(BypassElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BypassElementCollection : ConfigurationElementCollection
	{
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000028AE File Offset: 0x00000AAE
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0007C96F File Offset: 0x0007AB6F
		protected override ConfigurationElement CreateNewElement()
		{
			return new BypassElement();
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0007C976 File Offset: 0x0007AB76
		[MonoTODO("argument exception?")]
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is BypassElement))
			{
				throw new ArgumentException("element");
			}
			return ((BypassElement)element).Address;
		}
	}
}
