using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000382 RID: 898
	internal struct XPathNodeRef
	{
		// Token: 0x060027A3 RID: 10147 RVA: 0x000DB399 File Offset: 0x000D9599
		public XPathNodeRef(XPathNode[] page, int idx)
		{
			this._page = page;
			this._idx = idx;
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x000DB3A9 File Offset: 0x000D95A9
		public XPathNode[] Page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000DB3B1 File Offset: 0x000D95B1
		public int Index
		{
			get
			{
				return this._idx;
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000DB3B9 File Offset: 0x000D95B9
		public override int GetHashCode()
		{
			return XPathNodeHelper.GetLocation(this._page, this._idx);
		}

		// Token: 0x040012F4 RID: 4852
		private XPathNode[] _page;

		// Token: 0x040012F5 RID: 4853
		private int _idx;
	}
}
