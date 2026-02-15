using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D2 RID: 210
	internal class DocumentXPathNodeIterator_ElemChildren_NoLocalName : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x0003A0A4 File Offset: 0x000382A4
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNavigator nav, string nsAtom)
			: base(nav)
		{
			this.nsAtom = nsAtom;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0003A0B4 File Offset: 0x000382B4
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNodeIterator_ElemChildren_NoLocalName other)
			: base(other)
		{
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0003A0C9 File Offset: 0x000382C9
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0003A0D1 File Offset: 0x000382D1
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x040005E7 RID: 1511
		private string nsAtom;
	}
}
