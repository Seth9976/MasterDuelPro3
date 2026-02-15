using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000381 RID: 897
	internal struct XPathNode
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000DB158 File Offset: 0x000D9358
		public XPathNodeType NodeType
		{
			get
			{
				return (XPathNodeType)(this._props & 15U);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x000DB163 File Offset: 0x000D9363
		public string Prefix
		{
			get
			{
				return this._info.Prefix;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000DB170 File Offset: 0x000D9370
		public string LocalName
		{
			get
			{
				return this._info.LocalName;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x000DB17D File Offset: 0x000D937D
		public string Name
		{
			get
			{
				if (this.Prefix.Length == 0)
				{
					return this.LocalName;
				}
				return this.Prefix + ":" + this.LocalName;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x000DB1A9 File Offset: 0x000D93A9
		public string NamespaceUri
		{
			get
			{
				return this._info.NamespaceUri;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x000DB1B6 File Offset: 0x000D93B6
		public XPathDocument Document
		{
			get
			{
				return this._info.Document;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x000DB1C3 File Offset: 0x000D93C3
		public string BaseUri
		{
			get
			{
				return this._info.BaseUri;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x000DB1D0 File Offset: 0x000D93D0
		public int LineNumber
		{
			get
			{
				return this._info.LineNumberBase + (int)((this._props & 16776192U) >> 10);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x000DB1ED File Offset: 0x000D93ED
		public int LinePosition
		{
			get
			{
				return this._info.LinePositionBase + (int)this._posOffset;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x000DB201 File Offset: 0x000D9401
		public int CollapsedLinePosition
		{
			get
			{
				return this.LinePosition + (int)(this._props >> 24);
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x000DB213 File Offset: 0x000D9413
		public XPathNodePageInfo PageInfo
		{
			get
			{
				return this._info.PageInfo;
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000DB220 File Offset: 0x000D9420
		public int GetRoot(out XPathNode[] pageNode)
		{
			return this._info.Document.GetRootNode(out pageNode);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000DB233 File Offset: 0x000D9433
		public int GetParent(out XPathNode[] pageNode)
		{
			pageNode = this._info.ParentPage;
			return (int)this._idxParent;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000DB248 File Offset: 0x000D9448
		public int GetSibling(out XPathNode[] pageNode)
		{
			pageNode = this._info.SiblingPage;
			return (int)this._idxSibling;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000DB25D File Offset: 0x000D945D
		public int GetSimilarElement(out XPathNode[] pageNode)
		{
			pageNode = this._info.SimilarElementPage;
			return (int)this._idxSimilar;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000DB272 File Offset: 0x000D9472
		public bool NameMatch(string localName, string namespaceName)
		{
			return this._info.LocalName == localName && this._info.NamespaceUri == namespaceName;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000DB295 File Offset: 0x000D9495
		public bool ElementMatch(string localName, string namespaceName)
		{
			return this.NodeType == XPathNodeType.Element && this._info.LocalName == localName && this._info.NamespaceUri == namespaceName;
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x000DB2C4 File Offset: 0x000D94C4
		public bool IsXmlNamespaceNode
		{
			get
			{
				string localName = this._info.LocalName;
				return this.NodeType == XPathNodeType.Namespace && localName.Length == 3 && localName == "xml";
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x000DB2FC File Offset: 0x000D94FC
		public bool HasSibling
		{
			get
			{
				return this._idxSibling > 0;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000DB307 File Offset: 0x000D9507
		public bool HasCollapsedText
		{
			get
			{
				return (this._props & 128U) > 0U;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x000DB318 File Offset: 0x000D9518
		public bool HasAttribute
		{
			get
			{
				return (this._props & 16U) > 0U;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000DB326 File Offset: 0x000D9526
		public bool HasContentChild
		{
			get
			{
				return (this._props & 32U) > 0U;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x000DB334 File Offset: 0x000D9534
		public bool HasElementChild
		{
			get
			{
				return (this._props & 64U) > 0U;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000DB344 File Offset: 0x000D9544
		public bool IsAttrNmsp
		{
			get
			{
				XPathNodeType nodeType = this.NodeType;
				return nodeType == XPathNodeType.Attribute || nodeType == XPathNodeType.Namespace;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x000DB362 File Offset: 0x000D9562
		public bool IsText
		{
			get
			{
				return XPathNavigator.IsText(this.NodeType);
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000DB36F File Offset: 0x000D956F
		public bool HasNamespaceDecls
		{
			get
			{
				return (this._props & 512U) > 0U;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000DB380 File Offset: 0x000D9580
		public bool AllowShortcutTag
		{
			get
			{
				return (this._props & 256U) > 0U;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000DB391 File Offset: 0x000D9591
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040012ED RID: 4845
		private XPathNodeInfoAtom _info;

		// Token: 0x040012EE RID: 4846
		private ushort _idxSibling;

		// Token: 0x040012EF RID: 4847
		private ushort _idxParent;

		// Token: 0x040012F0 RID: 4848
		private ushort _idxSimilar;

		// Token: 0x040012F1 RID: 4849
		private ushort _posOffset;

		// Token: 0x040012F2 RID: 4850
		private uint _props;

		// Token: 0x040012F3 RID: 4851
		private string _value;
	}
}
