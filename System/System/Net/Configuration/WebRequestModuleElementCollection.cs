using System;
using System.Configuration;
using System.Reflection;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for Web request module configuration elements. This class cannot be inherited.</summary>
	// Token: 0x0200049C RID: 1180
	[ConfigurationCollection(typeof(WebRequestModuleElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	[DefaultMember("Item")]
	public sealed class WebRequestModuleElementCollection : ConfigurationElementCollection
	{
		// Token: 0x06001CC4 RID: 7364 RVA: 0x0007D359 File Offset: 0x0007B559
		protected override ConfigurationElement CreateNewElement()
		{
			return new WebRequestModuleElement();
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0007D360 File Offset: 0x0007B560
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is WebRequestModuleElement))
			{
				throw new ArgumentException("element");
			}
			return ((WebRequestModuleElement)element).Prefix;
		}
	}
}
