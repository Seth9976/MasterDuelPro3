using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D0 RID: 208
	internal class DocumentXPathNodeIterator_AllElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x0003A016 File Offset: 0x00038216
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNavigator nav)
			: base(nav)
		{
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0003A01F File Offset: 0x0003821F
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNodeIterator_AllElemChildren other)
			: base(other)
		{
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0003A028 File Offset: 0x00038228
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0003A030 File Offset: 0x00038230
		protected override bool Match(XmlNode node)
		{
			return node.NodeType == XmlNodeType.Element;
		}
	}
}
