using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000DE RID: 222
	internal class XmlChildNodes : XmlNodeList
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x0003B30F File Offset: 0x0003950F
		public XmlChildNodes(XmlNode container)
		{
			this.container = container;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0003B320 File Offset: 0x00039520
		public override XmlNode Item(int i)
		{
			if (i < 0)
			{
				return null;
			}
			XmlNode xmlNode = this.container.FirstChild;
			while (xmlNode != null)
			{
				if (i == 0)
				{
					return xmlNode;
				}
				xmlNode = xmlNode.NextSibling;
				i--;
			}
			return null;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0003B358 File Offset: 0x00039558
		public override int Count
		{
			get
			{
				int num = 0;
				for (XmlNode xmlNode = this.container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0003B384 File Offset: 0x00039584
		public override IEnumerator GetEnumerator()
		{
			if (this.container.FirstChild == null)
			{
				return XmlDocument.EmptyEnumerator;
			}
			return new XmlChildEnumerator(this.container);
		}

		// Token: 0x040005FC RID: 1532
		private XmlNode container;
	}
}
