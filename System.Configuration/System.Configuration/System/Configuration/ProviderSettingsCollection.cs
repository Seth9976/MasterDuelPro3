using System;
using System.Reflection;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	// Token: 0x0200003D RID: 61
	[DefaultMember("Item")]
	[ConfigurationCollection(typeof(ProviderSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProviderSettingsCollection : ConfigurationElementCollection
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00006A18 File Offset: 0x00004C18
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderSettings();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006A1F File Offset: 0x00004C1F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderSettings)element).Name;
		}

		// Token: 0x040000C5 RID: 197
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
