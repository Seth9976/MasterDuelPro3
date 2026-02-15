using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D3 RID: 211
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName : DocumentXPathNodeIterator_ElemChildren_NoLocalName
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0003A0E4 File Offset: 0x000382E4
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNavigator nav, string nsAtom)
			: base(nav, nsAtom)
		{
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0003A0EE File Offset: 0x000382EE
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName other)
			: base(other)
		{
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0003A0F7 File Offset: 0x000382F7
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0003A100 File Offset: 0x00038300
		public override bool MoveNext()
		{
			if (this.CurrentPosition == 0)
			{
				XmlNode xmlNode = (XmlNode)((DocumentXPathNavigator)this.Current).UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					base.SetPosition(1);
					return true;
				}
			}
			return base.MoveNext();
		}
	}
}
