using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D7 RID: 215
	internal class XPathNodeList : XmlNodeList
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0003A478 File Offset: 0x00038678
		public XPathNodeList(XPathNodeIterator nodeIterator)
		{
			this.nodeIterator = nodeIterator;
			this.list = new List<XmlNode>();
			this.done = false;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0003A499 File Offset: 0x00038699
		public override int Count
		{
			get
			{
				if (!this.done)
				{
					this.ReadUntil(int.MaxValue);
				}
				return this.list.Count;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0003A4BA File Offset: 0x000386BA
		private XmlNode GetNode(XPathNavigator n)
		{
			return ((IHasXmlNode)n).GetNode();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0003A4C8 File Offset: 0x000386C8
		internal int ReadUntil(int index)
		{
			int num = this.list.Count;
			while (!this.done && num <= index)
			{
				if (!this.nodeIterator.MoveNext())
				{
					this.done = true;
					break;
				}
				XmlNode node = this.GetNode(this.nodeIterator.Current);
				if (node != null)
				{
					this.list.Add(node);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0003A52D File Offset: 0x0003872D
		public override XmlNode Item(int index)
		{
			if (this.list.Count <= index)
			{
				this.ReadUntil(index);
			}
			if (index < 0 || this.list.Count <= index)
			{
				return null;
			}
			return this.list[index];
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003A565 File Offset: 0x00038765
		public override IEnumerator GetEnumerator()
		{
			return new XmlNodeListEnumerator(this);
		}

		// Token: 0x040005EF RID: 1519
		private List<XmlNode> list;

		// Token: 0x040005F0 RID: 1520
		private XPathNodeIterator nodeIterator;

		// Token: 0x040005F1 RID: 1521
		private bool done;

		// Token: 0x040005F2 RID: 1522
		private static readonly object[] nullparams = new object[0];
	}
}
