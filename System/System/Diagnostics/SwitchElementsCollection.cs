using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200015B RID: 347
	[ConfigurationCollection(typeof(SwitchElement))]
	internal class SwitchElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000145 RID: 325
		public SwitchElement this[string name]
		{
			get
			{
				return (SwitchElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002C413 File Offset: 0x0002A613
		protected override ConfigurationElement CreateNewElement()
		{
			return new SwitchElement();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0002C41A File Offset: 0x0002A61A
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SwitchElement)element).Name;
		}
	}
}
