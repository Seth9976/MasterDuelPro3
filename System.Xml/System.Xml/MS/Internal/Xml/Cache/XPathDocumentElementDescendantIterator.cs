using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200037E RID: 894
	internal class XPathDocumentElementDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06002751 RID: 10065 RVA: 0x000DA248 File Offset: 0x000D8448
		public XPathDocumentElementDescendantIterator(XPathDocumentNavigator root, string name, string namespaceURI, bool matchSelf)
			: base(root)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this._localName = root.NameTable.Get(name);
			this._namespaceUri = namespaceURI;
			this._matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this._end = new XPathDocumentNavigator(root);
				this._end.MoveToNonDescendant();
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000DA2AB File Offset: 0x000D84AB
		public XPathDocumentElementDescendantIterator(XPathDocumentElementDescendantIterator iter)
			: base(iter)
		{
			this._end = iter._end;
			this._localName = iter._localName;
			this._namespaceUri = iter._namespaceUri;
			this._matchSelf = iter._matchSelf;
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000DA2E4 File Offset: 0x000D84E4
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementDescendantIterator(this);
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000DA2EC File Offset: 0x000D84EC
		public override bool MoveNext()
		{
			if (this._matchSelf)
			{
				this._matchSelf = false;
				if (this.ctxt.IsElementMatch(this._localName, this._namespaceUri))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this._localName, this._namespaceUri, this._end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x040012E1 RID: 4833
		private XPathDocumentNavigator _end;

		// Token: 0x040012E2 RID: 4834
		private string _localName;

		// Token: 0x040012E3 RID: 4835
		private string _namespaceUri;

		// Token: 0x040012E4 RID: 4836
		private bool _matchSelf;
	}
}
