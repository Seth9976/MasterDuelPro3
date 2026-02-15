using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000182 RID: 386
	internal class XmlAttributeComparer : IComparer
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x00057334 File Offset: 0x00055534
		public int Compare(object o1, object o2)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)o1;
			XmlAttribute xmlAttribute2 = (XmlAttribute)o2;
			int num = string.Compare(xmlAttribute.NamespaceURI, xmlAttribute2.NamespaceURI, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlAttribute.Name, xmlAttribute2.Name, StringComparison.Ordinal);
			}
			return num;
		}
	}
}
