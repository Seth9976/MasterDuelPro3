using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000D6 RID: 214
	internal class DomNameTable
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x0003A21C File Offset: 0x0003841C
		public DomNameTable(XmlDocument document)
		{
			this.ownerDocument = document;
			this.nameTable = document.NameTable;
			this.entries = new XmlName[64];
			this.mask = 63;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0003A24C File Offset: 0x0003844C
		public XmlName GetName(string prefix, string localName, string ns, IXmlSchemaInfo schemaInfo)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int hashCode = XmlName.GetHashCode(localName);
			for (XmlName xmlName = this.entries[hashCode & this.mask]; xmlName != null; xmlName = xmlName.next)
			{
				if (xmlName.HashCode == hashCode && (xmlName.LocalName == localName || xmlName.LocalName.Equals(localName)) && (xmlName.Prefix == prefix || xmlName.Prefix.Equals(prefix)) && (xmlName.NamespaceURI == ns || xmlName.NamespaceURI.Equals(ns)) && xmlName.Equals(schemaInfo))
				{
					return xmlName;
				}
			}
			return null;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0003A2EC File Offset: 0x000384EC
		public XmlName AddName(string prefix, string localName, string ns, IXmlSchemaInfo schemaInfo)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int hashCode = XmlName.GetHashCode(localName);
			for (XmlName xmlName = this.entries[hashCode & this.mask]; xmlName != null; xmlName = xmlName.next)
			{
				if (xmlName.HashCode == hashCode && (xmlName.LocalName == localName || xmlName.LocalName.Equals(localName)) && (xmlName.Prefix == prefix || xmlName.Prefix.Equals(prefix)) && (xmlName.NamespaceURI == ns || xmlName.NamespaceURI.Equals(ns)) && xmlName.Equals(schemaInfo))
				{
					return xmlName;
				}
			}
			prefix = this.nameTable.Add(prefix);
			localName = this.nameTable.Add(localName);
			ns = this.nameTable.Add(ns);
			int num = hashCode & this.mask;
			XmlName xmlName2 = XmlName.Create(prefix, localName, ns, hashCode, this.ownerDocument, this.entries[num], schemaInfo);
			this.entries[num] = xmlName2;
			int num2 = this.count;
			this.count = num2 + 1;
			if (num2 == this.mask)
			{
				this.Grow();
			}
			return xmlName2;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0003A404 File Offset: 0x00038604
		private void Grow()
		{
			int num = this.mask * 2 + 1;
			XmlName[] array = this.entries;
			XmlName[] array2 = new XmlName[num + 1];
			foreach (XmlName xmlName in array)
			{
				while (xmlName != null)
				{
					int num2 = xmlName.HashCode & num;
					XmlName next = xmlName.next;
					xmlName.next = array2[num2];
					array2[num2] = xmlName;
					xmlName = next;
				}
			}
			this.entries = array2;
			this.mask = num;
		}

		// Token: 0x040005EA RID: 1514
		private XmlName[] entries;

		// Token: 0x040005EB RID: 1515
		private int count;

		// Token: 0x040005EC RID: 1516
		private int mask;

		// Token: 0x040005ED RID: 1517
		private XmlDocument ownerDocument;

		// Token: 0x040005EE RID: 1518
		private XmlNameTable nameTable;
	}
}
