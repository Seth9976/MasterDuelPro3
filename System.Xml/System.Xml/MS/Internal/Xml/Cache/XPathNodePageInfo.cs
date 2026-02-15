using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000384 RID: 900
	internal sealed class XPathNodePageInfo
	{
		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000DBA6F File Offset: 0x000D9C6F
		public int PageNumber
		{
			get
			{
				return this._pageNum;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x000DBA77 File Offset: 0x000D9C77
		public int NodeCount
		{
			get
			{
				return this._nodeCount;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000DBA7F File Offset: 0x000D9C7F
		public XPathNode[] NextPage
		{
			get
			{
				return this._pageNext;
			}
		}

		// Token: 0x040012F6 RID: 4854
		private int _pageNum;

		// Token: 0x040012F7 RID: 4855
		private int _nodeCount;

		// Token: 0x040012F8 RID: 4856
		private XPathNode[] _pageNext;
	}
}
