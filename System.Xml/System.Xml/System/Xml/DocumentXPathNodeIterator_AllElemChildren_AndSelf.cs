using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D1 RID: 209
	internal sealed class DocumentXPathNodeIterator_AllElemChildren_AndSelf : DocumentXPathNodeIterator_AllElemChildren
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x0003A03B File Offset: 0x0003823B
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNavigator nav)
			: base(nav)
		{
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0003A044 File Offset: 0x00038244
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNodeIterator_AllElemChildren_AndSelf other)
			: base(other)
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0003A04D File Offset: 0x0003824D
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0003A058 File Offset: 0x00038258
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
