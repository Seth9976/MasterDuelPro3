using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200037C RID: 892
	internal class XPathDocumentElementChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06002749 RID: 10057 RVA: 0x000DA117 File Offset: 0x000D8317
		public XPathDocumentElementChildIterator(XPathDocumentNavigator parent, string name, string namespaceURI)
			: base(parent)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this._localName = parent.NameTable.Get(name);
			this._namespaceUri = namespaceURI;
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000DA147 File Offset: 0x000D8347
		public XPathDocumentElementChildIterator(XPathDocumentElementChildIterator iter)
			: base(iter)
		{
			this._localName = iter._localName;
			this._namespaceUri = iter._namespaceUri;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000DA168 File Offset: 0x000D8368
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementChildIterator(this);
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000DA170 File Offset: 0x000D8370
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this._localName, this._namespaceUri))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this._localName, this._namespaceUri))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x040012DE RID: 4830
		private string _localName;

		// Token: 0x040012DF RID: 4831
		private string _namespaceUri;
	}
}
