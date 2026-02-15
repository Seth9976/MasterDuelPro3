using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000153 RID: 339
	[ConfigurationCollection(typeof(ListenerElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SharedListenerElementsCollection : ListenerElementsCollection
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000028AE File Offset: 0x00000AAE
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002B764 File Offset: 0x00029964
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(false);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0002B76C File Offset: 0x0002996C
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}
	}
}
