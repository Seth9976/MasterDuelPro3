using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D4 RID: 212
	internal class DocumentXPathNodeIterator_ElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003A14C File Offset: 0x0003834C
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNavigator nav, string localNameAtom, string nsAtom)
			: base(nav)
		{
			this.localNameAtom = localNameAtom;
			this.nsAtom = nsAtom;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0003A163 File Offset: 0x00038363
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNodeIterator_ElemChildren other)
			: base(other)
		{
			this.localNameAtom = other.localNameAtom;
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0003A184 File Offset: 0x00038384
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren(this);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0003A18C File Offset: 0x0003838C
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.LocalName, this.localNameAtom) && Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x040005E8 RID: 1512
		protected string localNameAtom;

		// Token: 0x040005E9 RID: 1513
		protected string nsAtom;
	}
}
