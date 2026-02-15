using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200031B RID: 795
	internal class XmlAnyListConverter : XmlListConverter
	{
		// Token: 0x06002422 RID: 9250 RVA: 0x000CBE97 File Offset: 0x000CA097
		protected XmlAnyListConverter(XmlBaseConverter atomicConverter)
			: base(atomicConverter)
		{
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000CBEA0 File Offset: 0x000CA0A0
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(value is IEnumerable) || value.GetType() == XmlBaseConverter.StringType || value.GetType() == XmlBaseConverter.ByteArrayType)
			{
				value = new object[] { value };
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x040010B2 RID: 4274
		public static readonly XmlValueConverter ItemList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.Item);

		// Token: 0x040010B3 RID: 4275
		public static readonly XmlValueConverter AnyAtomicList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.AnyAtomic);
	}
}
