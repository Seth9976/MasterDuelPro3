using System;

namespace System.Xml.Schema
{
	// Token: 0x02000298 RID: 664
	internal sealed class SchemaEntity : IDtdEntityInfo
	{
		// Token: 0x06001E66 RID: 7782 RVA: 0x000B2BB0 File Offset: 0x000B0DB0
		internal SchemaEntity(XmlQualifiedName qname, bool isParameter)
		{
			this.qname = qname;
			this.isParameter = isParameter;
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x000B2BD1 File Offset: 0x000B0DD1
		string IDtdEntityInfo.Name
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000B2BDE File Offset: 0x000B0DDE
		bool IDtdEntityInfo.IsExternal
		{
			get
			{
				return this.IsExternal;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000B2BE6 File Offset: 0x000B0DE6
		bool IDtdEntityInfo.IsDeclaredInExternal
		{
			get
			{
				return this.DeclaredInExternal;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000B2BEE File Offset: 0x000B0DEE
		bool IDtdEntityInfo.IsUnparsedEntity
		{
			get
			{
				return !this.NData.IsEmpty;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000B2BFE File Offset: 0x000B0DFE
		bool IDtdEntityInfo.IsParameterEntity
		{
			get
			{
				return this.isParameter;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x000B2C06 File Offset: 0x000B0E06
		string IDtdEntityInfo.BaseUriString
		{
			get
			{
				return this.BaseURI;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000B2C0E File Offset: 0x000B0E0E
		string IDtdEntityInfo.DeclaredUriString
		{
			get
			{
				return this.DeclaredURI;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000B2C16 File Offset: 0x000B0E16
		string IDtdEntityInfo.SystemId
		{
			get
			{
				return this.Url;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000B2C1E File Offset: 0x000B0E1E
		string IDtdEntityInfo.PublicId
		{
			get
			{
				return this.Pubid;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000B2C26 File Offset: 0x000B0E26
		string IDtdEntityInfo.Text
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000B2C2E File Offset: 0x000B0E2E
		int IDtdEntityInfo.LineNumber
		{
			get
			{
				return this.Line;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000B2C36 File Offset: 0x000B0E36
		int IDtdEntityInfo.LinePosition
		{
			get
			{
				return this.Pos;
			}
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000B2C40 File Offset: 0x000B0E40
		internal static bool IsPredefinedEntity(string n)
		{
			return n == "lt" || n == "gt" || n == "amp" || n == "apos" || n == "quot";
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000B2C8E File Offset: 0x000B0E8E
		internal XmlQualifiedName Name
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x000B2C96 File Offset: 0x000B0E96
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x000B2C9E File Offset: 0x000B0E9E
		internal string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
				this.isExternal = true;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x000B2CAE File Offset: 0x000B0EAE
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x000B2CB6 File Offset: 0x000B0EB6
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x000B2CBF File Offset: 0x000B0EBF
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x000B2CC7 File Offset: 0x000B0EC7
		internal bool IsExternal
		{
			get
			{
				return this.isExternal;
			}
			set
			{
				this.isExternal = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x000B2CD0 File Offset: 0x000B0ED0
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x000B2CD8 File Offset: 0x000B0ED8
		internal bool DeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x000B2CE1 File Offset: 0x000B0EE1
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x000B2CE9 File Offset: 0x000B0EE9
		internal XmlQualifiedName NData
		{
			get
			{
				return this.ndata;
			}
			set
			{
				this.ndata = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x000B2CF2 File Offset: 0x000B0EF2
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x000B2CFA File Offset: 0x000B0EFA
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.isExternal = false;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x000B2D0A File Offset: 0x000B0F0A
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x000B2D12 File Offset: 0x000B0F12
		internal int Line
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x000B2D1B File Offset: 0x000B0F1B
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x000B2D23 File Offset: 0x000B0F23
		internal int Pos
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x000B2D2C File Offset: 0x000B0F2C
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x000B2D42 File Offset: 0x000B0F42
		internal string BaseURI
		{
			get
			{
				if (this.baseURI != null)
				{
					return this.baseURI;
				}
				return string.Empty;
			}
			set
			{
				this.baseURI = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x000B2D4B File Offset: 0x000B0F4B
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x000B2D53 File Offset: 0x000B0F53
		internal bool ParsingInProgress
		{
			get
			{
				return this.parsingInProgress;
			}
			set
			{
				this.parsingInProgress = value;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000B2D5C File Offset: 0x000B0F5C
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x000B2D72 File Offset: 0x000B0F72
		internal string DeclaredURI
		{
			get
			{
				if (this.declaredURI != null)
				{
					return this.declaredURI;
				}
				return string.Empty;
			}
			set
			{
				this.declaredURI = value;
			}
		}

		// Token: 0x04000D09 RID: 3337
		private XmlQualifiedName qname;

		// Token: 0x04000D0A RID: 3338
		private string url;

		// Token: 0x04000D0B RID: 3339
		private string pubid;

		// Token: 0x04000D0C RID: 3340
		private string text;

		// Token: 0x04000D0D RID: 3341
		private XmlQualifiedName ndata = XmlQualifiedName.Empty;

		// Token: 0x04000D0E RID: 3342
		private int lineNumber;

		// Token: 0x04000D0F RID: 3343
		private int linePosition;

		// Token: 0x04000D10 RID: 3344
		private bool isParameter;

		// Token: 0x04000D11 RID: 3345
		private bool isExternal;

		// Token: 0x04000D12 RID: 3346
		private bool parsingInProgress;

		// Token: 0x04000D13 RID: 3347
		private bool isDeclaredInExternal;

		// Token: 0x04000D14 RID: 3348
		private string baseURI;

		// Token: 0x04000D15 RID: 3349
		private string declaredURI;
	}
}
