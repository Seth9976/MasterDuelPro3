using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000371 RID: 881
	internal class XPathChildIterator : XPathAxisIterator
	{
		// Token: 0x060026E9 RID: 9961 RVA: 0x000D87E5 File Offset: 0x000D69E5
		public XPathChildIterator(XPathNavigator nav, XPathNodeType type)
			: base(nav, type, false)
		{
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000D87F0 File Offset: 0x000D69F0
		public XPathChildIterator(XPathNavigator nav, string name, string namespaceURI)
			: base(nav, name, namespaceURI, false)
		{
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000D87FC File Offset: 0x000D69FC
		public XPathChildIterator(XPathChildIterator it)
			: base(it)
		{
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000D8805 File Offset: 0x000D6A05
		public override XPathNodeIterator Clone()
		{
			return new XPathChildIterator(this);
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000D8810 File Offset: 0x000D6A10
		public override bool MoveNext()
		{
			while (this.first ? this.nav.MoveToFirstChild() : this.nav.MoveToNext())
			{
				this.first = false;
				if (this.Matches)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}
	}
}
