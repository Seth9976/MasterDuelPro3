using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000156 RID: 342
	[ConfigurationCollection(typeof(SourceElement), AddItemName = "source", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SourceElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000136 RID: 310
		public SourceElement this[string name]
		{
			get
			{
				return (SourceElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0002BC74 File Offset: 0x00029E74
		protected override string ElementName
		{
			get
			{
				return "source";
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000028AE File Offset: 0x00000AAE
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002BC7B File Offset: 0x00029E7B
		protected override ConfigurationElement CreateNewElement()
		{
			SourceElement sourceElement = new SourceElement();
			sourceElement.Listeners.InitializeDefaultInternal();
			return sourceElement;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002BC8D File Offset: 0x00029E8D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SourceElement)element).Name;
		}
	}
}
