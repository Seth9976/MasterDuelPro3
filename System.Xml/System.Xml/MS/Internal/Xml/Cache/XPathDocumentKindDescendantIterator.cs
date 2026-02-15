using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200037F RID: 895
	internal class XPathDocumentKindDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06002755 RID: 10069 RVA: 0x000DA361 File Offset: 0x000D8561
		public XPathDocumentKindDescendantIterator(XPathDocumentNavigator root, XPathNodeType typ, bool matchSelf)
			: base(root)
		{
			this._typ = typ;
			this._matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this._end = new XPathDocumentNavigator(root);
				this._end.MoveToNonDescendant();
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000DA398 File Offset: 0x000D8598
		public XPathDocumentKindDescendantIterator(XPathDocumentKindDescendantIterator iter)
			: base(iter)
		{
			this._end = iter._end;
			this._typ = iter._typ;
			this._matchSelf = iter._matchSelf;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000DA3C5 File Offset: 0x000D85C5
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindDescendantIterator(this);
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000DA3D0 File Offset: 0x000D85D0
		public override bool MoveNext()
		{
			if (this._matchSelf)
			{
				this._matchSelf = false;
				if (this.ctxt.IsKindMatch(this._typ))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this._typ, this._end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x040012E5 RID: 4837
		private XPathDocumentNavigator _end;

		// Token: 0x040012E6 RID: 4838
		private XPathNodeType _typ;

		// Token: 0x040012E7 RID: 4839
		private bool _matchSelf;
	}
}
