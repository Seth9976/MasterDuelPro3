using System;
using System.Collections.Generic;
using MS.Internal.Xml.Cache;

namespace System.Xml.XPath
{
	/// <summary>Provides a fast, read-only, in-memory representation of an XML document by using the XPath data model.</summary>
	// Token: 0x02000137 RID: 311
	public class XPathDocument
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0004CCD4 File Offset: 0x0004AED4
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0004CCDC File Offset: 0x0004AEDC
		internal bool HasLineInfo
		{
			get
			{
				return this.hasLineInfo;
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0004CCE4 File Offset: 0x0004AEE4
		internal int GetCollapsedTextNode(out XPathNode[] pageText)
		{
			pageText = this.pageText;
			return this.idxText;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0004CCF4 File Offset: 0x0004AEF4
		internal int GetRootNode(out XPathNode[] pageRoot)
		{
			pageRoot = this.pageRoot;
			return this.idxRoot;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0004CD04 File Offset: 0x0004AF04
		internal int GetXmlNamespaceNode(out XPathNode[] pageXmlNmsp)
		{
			pageXmlNmsp = this.pageXmlNmsp;
			return this.idxXmlNmsp;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0004CD14 File Offset: 0x0004AF14
		internal int LookupNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			XPathNodeRef xpathNodeRef = new XPathNodeRef(pageElem, idxElem);
			if (this.mapNmsp == null || !this.mapNmsp.ContainsKey(xpathNodeRef))
			{
				pageNmsp = null;
				return 0;
			}
			xpathNodeRef = this.mapNmsp[xpathNodeRef];
			pageNmsp = xpathNodeRef.Page;
			return xpathNodeRef.Index;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0004CD64 File Offset: 0x0004AF64
		internal int LookupIdElement(string id, out XPathNode[] pageElem)
		{
			if (this.idValueMap == null || !this.idValueMap.ContainsKey(id))
			{
				pageElem = null;
				return 0;
			}
			XPathNodeRef xpathNodeRef = this.idValueMap[id];
			pageElem = xpathNodeRef.Page;
			return xpathNodeRef.Index;
		}

		// Token: 0x04000786 RID: 1926
		private XPathNode[] pageText;

		// Token: 0x04000787 RID: 1927
		private XPathNode[] pageRoot;

		// Token: 0x04000788 RID: 1928
		private XPathNode[] pageXmlNmsp;

		// Token: 0x04000789 RID: 1929
		private int idxText;

		// Token: 0x0400078A RID: 1930
		private int idxRoot;

		// Token: 0x0400078B RID: 1931
		private int idxXmlNmsp;

		// Token: 0x0400078C RID: 1932
		private XmlNameTable nameTable;

		// Token: 0x0400078D RID: 1933
		private bool hasLineInfo;

		// Token: 0x0400078E RID: 1934
		private Dictionary<XPathNodeRef, XPathNodeRef> mapNmsp;

		// Token: 0x0400078F RID: 1935
		private Dictionary<string, XPathNodeRef> idValueMap;
	}
}
