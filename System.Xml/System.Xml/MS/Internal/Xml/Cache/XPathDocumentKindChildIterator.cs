using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200037D RID: 893
	internal class XPathDocumentKindChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x0600274D RID: 10061 RVA: 0x000DA1CA File Offset: 0x000D83CA
		public XPathDocumentKindChildIterator(XPathDocumentNavigator parent, XPathNodeType typ)
			: base(parent)
		{
			this._typ = typ;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000DA1DA File Offset: 0x000D83DA
		public XPathDocumentKindChildIterator(XPathDocumentKindChildIterator iter)
			: base(iter)
		{
			this._typ = iter._typ;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000DA1EF File Offset: 0x000D83EF
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindChildIterator(this);
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000DA1F8 File Offset: 0x000D83F8
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this._typ))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this._typ))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x040012E0 RID: 4832
		private XPathNodeType _typ;
	}
}
