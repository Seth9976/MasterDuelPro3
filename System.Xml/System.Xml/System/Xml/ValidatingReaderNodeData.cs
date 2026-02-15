using System;

namespace System.Xml
{
	// Token: 0x02000044 RID: 68
	internal class ValidatingReaderNodeData
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000D0BB File Offset: 0x0000B2BB
		public ValidatingReaderNodeData()
		{
			this.Clear(XmlNodeType.None);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		public ValidatingReaderNodeData(XmlNodeType nodeType)
		{
			this.Clear(nodeType);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000D0D9 File Offset: 0x0000B2D9
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000D0E1 File Offset: 0x0000B2E1
		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000D0EA File Offset: 0x0000B2EA
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		public string Namespace
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000D0FB File Offset: 0x0000B2FB
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000D103 File Offset: 0x0000B303
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D10C File Offset: 0x0000B30C
		public string GetAtomizedNameWPrefix(XmlNameTable nameTable)
		{
			if (this.nameWPrefix == null)
			{
				if (this.prefix.Length == 0)
				{
					this.nameWPrefix = this.localName;
				}
				else
				{
					this.nameWPrefix = nameTable.Add(this.prefix + ":" + this.localName);
				}
			}
			return this.nameWPrefix;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000D164 File Offset: 0x0000B364
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000D16C File Offset: 0x0000B36C
		public int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000D175 File Offset: 0x0000B375
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000D17D File Offset: 0x0000B37D
		public string RawValue
		{
			get
			{
				return this.rawValue;
			}
			set
			{
				this.rawValue = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000D186 File Offset: 0x0000B386
		public string OriginalStringValue
		{
			get
			{
				return this.originalStringValue;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000D18E File Offset: 0x0000B38E
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000D196 File Offset: 0x0000B396
		public XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000D19F File Offset: 0x0000B39F
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000D1A7 File Offset: 0x0000B3A7
		public AttributePSVIInfo AttInfo
		{
			get
			{
				return this.attributePSVIInfo;
			}
			set
			{
				this.attributePSVIInfo = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		public int LineNumber
		{
			get
			{
				return this.lineNo;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		internal void Clear(XmlNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.localName = string.Empty;
			this.prefix = string.Empty;
			this.namespaceUri = string.Empty;
			this.rawValue = string.Empty;
			if (this.attributePSVIInfo != null)
			{
				this.attributePSVIInfo.Reset();
			}
			this.nameWPrefix = null;
			this.lineNo = 0;
			this.linePos = 0;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D228 File Offset: 0x0000B428
		internal void SetLineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D238 File Offset: 0x0000B438
		internal void SetLineInfo(IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				this.lineNo = lineInfo.LineNumber;
				this.linePos = lineInfo.LinePosition;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D255 File Offset: 0x0000B455
		internal void SetItemData(string localName, string prefix, string ns, int depth)
		{
			this.localName = localName;
			this.prefix = prefix;
			this.namespaceUri = ns;
			this.depth = depth;
			this.rawValue = string.Empty;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D27F File Offset: 0x0000B47F
		internal void SetItemData(string value)
		{
			this.SetItemData(value, value);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D289 File Offset: 0x0000B489
		internal void SetItemData(string value, string originalStringValue)
		{
			this.rawValue = value;
			this.originalStringValue = originalStringValue;
		}

		// Token: 0x0400015D RID: 349
		private string localName;

		// Token: 0x0400015E RID: 350
		private string namespaceUri;

		// Token: 0x0400015F RID: 351
		private string prefix;

		// Token: 0x04000160 RID: 352
		private string nameWPrefix;

		// Token: 0x04000161 RID: 353
		private string rawValue;

		// Token: 0x04000162 RID: 354
		private string originalStringValue;

		// Token: 0x04000163 RID: 355
		private int depth;

		// Token: 0x04000164 RID: 356
		private AttributePSVIInfo attributePSVIInfo;

		// Token: 0x04000165 RID: 357
		private XmlNodeType nodeType;

		// Token: 0x04000166 RID: 358
		private int lineNo;

		// Token: 0x04000167 RID: 359
		private int linePos;
	}
}
