using System;
using System.Text;

namespace System.Xml
{
	/// <summary>Provides all the context information required by the <see cref="T:System.Xml.XmlReader" /> to parse an XML fragment.</summary>
	// Token: 0x02000066 RID: 102
	public class XmlParserContext
	{
		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, xml:lang, and xml:space values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x06000407 RID: 1031 RVA: 0x000150CC File Offset: 0x000132CC
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string xmlLang, XmlSpace xmlSpace)
			: this(nt, nsMgr, null, null, null, null, string.Empty, xmlLang, xmlSpace)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, base URI, xml:lang, xml:space, and document type values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="docTypeName">The name of the document type declaration. </param>
		/// <param name="pubId">The public identifier. </param>
		/// <param name="sysId">The system identifier. </param>
		/// <param name="internalSubset">The internal DTD subset. The DTD subset is used for entity resolution, not for document validation.</param>
		/// <param name="baseURI">The base URI for the XML fragment (the location from which the fragment was loaded). </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x06000408 RID: 1032 RVA: 0x000150F0 File Offset: 0x000132F0
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace)
			: this(nt, nsMgr, docTypeName, pubId, sysId, internalSubset, baseURI, xmlLang, xmlSpace, null)
		{
		}

		/// <summary>Initializes a new instance of the XmlParserContext class with the specified <see cref="T:System.Xml.XmlNameTable" />, <see cref="T:System.Xml.XmlNamespaceManager" />, base URI, xml:lang, xml:space, encoding, and document type values.</summary>
		/// <param name="nt">The <see cref="T:System.Xml.XmlNameTable" /> to use to atomize strings. If this is null, the name table used to construct the <paramref name="nsMgr" /> is used instead. For more information about atomized strings, see <see cref="T:System.Xml.XmlNameTable" />. </param>
		/// <param name="nsMgr">The <see cref="T:System.Xml.XmlNamespaceManager" /> to use for looking up namespace information, or null. </param>
		/// <param name="docTypeName">The name of the document type declaration. </param>
		/// <param name="pubId">The public identifier. </param>
		/// <param name="sysId">The system identifier. </param>
		/// <param name="internalSubset">The internal DTD subset. The DTD is used for entity resolution, not for document validation.</param>
		/// <param name="baseURI">The base URI for the XML fragment (the location from which the fragment was loaded). </param>
		/// <param name="xmlLang">The xml:lang scope. </param>
		/// <param name="xmlSpace">An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope. </param>
		/// <param name="enc">An <see cref="T:System.Text.Encoding" /> object indicating the encoding setting. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="nt" /> is not the same XmlNameTable used to construct <paramref name="nsMgr" />. </exception>
		// Token: 0x06000409 RID: 1033 RVA: 0x00015114 File Offset: 0x00013314
		public XmlParserContext(XmlNameTable nt, XmlNamespaceManager nsMgr, string docTypeName, string pubId, string sysId, string internalSubset, string baseURI, string xmlLang, XmlSpace xmlSpace, Encoding enc)
		{
			if (nsMgr != null)
			{
				if (nt == null)
				{
					this._nt = nsMgr.NameTable;
				}
				else
				{
					if (nt != nsMgr.NameTable)
					{
						throw new XmlException("Not the same name table.", string.Empty);
					}
					this._nt = nt;
				}
			}
			else
			{
				this._nt = nt;
			}
			this._nsMgr = nsMgr;
			this._docTypeName = ((docTypeName == null) ? string.Empty : docTypeName);
			this._pubId = ((pubId == null) ? string.Empty : pubId);
			this._sysId = ((sysId == null) ? string.Empty : sysId);
			this._internalSubset = ((internalSubset == null) ? string.Empty : internalSubset);
			this._baseURI = ((baseURI == null) ? string.Empty : baseURI);
			this._xmlLang = ((xmlLang == null) ? string.Empty : xmlLang);
			this._xmlSpace = xmlSpace;
			this._encoding = enc;
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> used to atomize strings. For more information on atomized strings, see <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <returns>The XmlNameTable.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001522D File Offset: 0x0001342D
		public XmlNameTable NameTable
		{
			get
			{
				return this._nt;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlNamespaceManager" />.</summary>
		/// <returns>The XmlNamespaceManager.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00015235 File Offset: 0x00013435
		public XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this._nsMgr;
			}
		}

		/// <summary>Gets or sets the name of the document type declaration.</summary>
		/// <returns>The name of the document type declaration.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001523D File Offset: 0x0001343D
		public string DocTypeName
		{
			get
			{
				return this._docTypeName;
			}
		}

		/// <summary>Gets or sets the public identifier.</summary>
		/// <returns>The public identifier.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00015245 File Offset: 0x00013445
		public string PublicId
		{
			get
			{
				return this._pubId;
			}
		}

		/// <summary>Gets or sets the system identifier.</summary>
		/// <returns>The system identifier.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001524D File Offset: 0x0001344D
		public string SystemId
		{
			get
			{
				return this._sysId;
			}
		}

		/// <summary>Gets or sets the base URI.</summary>
		/// <returns>The base URI to use to resolve the DTD file.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00015255 File Offset: 0x00013455
		public string BaseURI
		{
			get
			{
				return this._baseURI;
			}
		}

		/// <summary>Gets or sets the internal DTD subset.</summary>
		/// <returns>The internal DTD subset. For example, this property returns everything between the square brackets &lt;!DOCTYPE doc [...]&gt;.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001525D File Offset: 0x0001345D
		public string InternalSubset
		{
			get
			{
				return this._internalSubset;
			}
		}

		/// <summary>Gets or sets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope. If there is no xml:lang in scope, String.Empty is returned.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00015265 File Offset: 0x00013465
		public string XmlLang
		{
			get
			{
				return this._xmlLang;
			}
		}

		/// <summary>Gets or sets the current xml:space scope.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlSpace" /> value indicating the xml:space scope.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001526D File Offset: 0x0001346D
		public XmlSpace XmlSpace
		{
			get
			{
				return this._xmlSpace;
			}
		}

		/// <summary>Gets or sets the encoding type.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object indicating the encoding type.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00015275 File Offset: 0x00013475
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0001527D File Offset: 0x0001347D
		internal bool HasDtdInfo
		{
			get
			{
				return this._internalSubset != string.Empty || this._pubId != string.Empty || this._sysId != string.Empty;
			}
		}

		// Token: 0x04000252 RID: 594
		private XmlNameTable _nt;

		// Token: 0x04000253 RID: 595
		private XmlNamespaceManager _nsMgr;

		// Token: 0x04000254 RID: 596
		private string _docTypeName = string.Empty;

		// Token: 0x04000255 RID: 597
		private string _pubId = string.Empty;

		// Token: 0x04000256 RID: 598
		private string _sysId = string.Empty;

		// Token: 0x04000257 RID: 599
		private string _internalSubset = string.Empty;

		// Token: 0x04000258 RID: 600
		private string _xmlLang = string.Empty;

		// Token: 0x04000259 RID: 601
		private XmlSpace _xmlSpace;

		// Token: 0x0400025A RID: 602
		private string _baseURI = string.Empty;

		// Token: 0x0400025B RID: 603
		private Encoding _encoding;
	}
}
