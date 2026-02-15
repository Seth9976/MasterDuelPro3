using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D5 RID: 213
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf : DocumentXPathNodeIterator_ElemChildren
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0003A1B4 File Offset: 0x000383B4
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNavigator nav, string localNameAtom, string nsAtom)
			: base(nav, localNameAtom, nsAtom)
		{
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0003A1BF File Offset: 0x000383BF
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNodeIterator_ElemChildren_AndSelf other)
			: base(other)
		{
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0003A1C8 File Offset: 0x000383C8
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0003A1D0 File Offset: 0x000383D0
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
