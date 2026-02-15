using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000385 RID: 901
	internal sealed class XPathNodeInfoAtom
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x000DBA87 File Offset: 0x000D9C87
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this._pageInfo;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x000DBA8F File Offset: 0x000D9C8F
		public string LocalName
		{
			get
			{
				return this._localName;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x000DBA97 File Offset: 0x000D9C97
		public string NamespaceUri
		{
			get
			{
				return this._namespaceUri;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x000DBA9F File Offset: 0x000D9C9F
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000DBAA7 File Offset: 0x000D9CA7
		public string BaseUri
		{
			get
			{
				return this._baseUri;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000DBAAF File Offset: 0x000D9CAF
		public XPathNode[] SiblingPage
		{
			get
			{
				return this._pageSibling;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000DBAB7 File Offset: 0x000D9CB7
		public XPathNode[] SimilarElementPage
		{
			get
			{
				return this._pageSimilar;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000DBABF File Offset: 0x000D9CBF
		public XPathNode[] ParentPage
		{
			get
			{
				return this._pageParent;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000DBAC7 File Offset: 0x000D9CC7
		public XPathDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000DBACF File Offset: 0x000D9CCF
		public int LineNumberBase
		{
			get
			{
				return this._lineNumBase;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000DBAD7 File Offset: 0x000D9CD7
		public int LinePositionBase
		{
			get
			{
				return this._linePosBase;
			}
		}

		// Token: 0x040012F9 RID: 4857
		private string _localName;

		// Token: 0x040012FA RID: 4858
		private string _namespaceUri;

		// Token: 0x040012FB RID: 4859
		private string _prefix;

		// Token: 0x040012FC RID: 4860
		private string _baseUri;

		// Token: 0x040012FD RID: 4861
		private XPathNode[] _pageParent;

		// Token: 0x040012FE RID: 4862
		private XPathNode[] _pageSibling;

		// Token: 0x040012FF RID: 4863
		private XPathNode[] _pageSimilar;

		// Token: 0x04001300 RID: 4864
		private XPathDocument _doc;

		// Token: 0x04001301 RID: 4865
		private int _lineNumBase;

		// Token: 0x04001302 RID: 4866
		private int _linePosBase;

		// Token: 0x04001303 RID: 4867
		private XPathNodePageInfo _pageInfo;
	}
}
