using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000015 RID: 21
	internal sealed class XmlSqlBinaryReader : XmlReader, IXmlNamespaceResolver
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00004188 File Offset: 0x00002388
		public XmlSqlBinaryReader(Stream stream, byte[] data, int len, string baseUri, bool closeInput, XmlReaderSettings settings)
		{
			this.unicode = Encoding.Unicode;
			this.xmlCharType = XmlCharType.Instance;
			this.xnt = settings.NameTable;
			if (this.xnt == null)
			{
				this.xnt = new NameTable();
				this.xntFromSettings = false;
			}
			else
			{
				this.xntFromSettings = true;
			}
			this.xml = this.xnt.Add("xml");
			this.xmlns = this.xnt.Add("xmlns");
			this.nsxmlns = this.xnt.Add("http://www.w3.org/2000/xmlns/");
			this.baseUri = baseUri;
			this.state = XmlSqlBinaryReader.ScanState.Init;
			this.nodetype = XmlNodeType.None;
			this.token = BinXmlToken.Error;
			this.elementStack = new XmlSqlBinaryReader.ElemInfo[16];
			this.attributes = new XmlSqlBinaryReader.AttrInfo[8];
			this.attrHashTbl = new int[8];
			this.symbolTables.Init();
			this.qnameOther.Clear();
			this.qnameElement.Clear();
			this.xmlspacePreserve = false;
			this.hasher = new SecureStringHasher();
			this.namespaces = new Dictionary<string, XmlSqlBinaryReader.NamespaceDecl>(this.hasher);
			this.AddInitNamespace(string.Empty, string.Empty);
			this.AddInitNamespace(this.xml, this.xnt.Add("http://www.w3.org/XML/1998/namespace"));
			this.AddInitNamespace(this.xmlns, this.nsxmlns);
			this.valueType = XmlSqlBinaryReader.TypeOfString;
			this.inStrm = stream;
			if (data != null)
			{
				this.data = data;
				this.end = len;
				this.pos = 2;
				this.sniffed = true;
			}
			else
			{
				this.data = new byte[4096];
				this.end = stream.Read(this.data, 0, 4096);
				this.pos = 0;
				this.sniffed = false;
			}
			this.mark = -1;
			this.eof = this.end == 0;
			this.offset = 0L;
			this.closeInput = closeInput;
			switch (settings.ConformanceLevel)
			{
			case ConformanceLevel.Auto:
				this.docState = 0;
				break;
			case ConformanceLevel.Fragment:
				this.docState = 9;
				break;
			case ConformanceLevel.Document:
				this.docState = 1;
				break;
			}
			this.checkCharacters = settings.CheckCharacters;
			this.dtdProcessing = settings.DtdProcessing;
			this.ignoreWhitespace = settings.IgnoreWhitespace;
			this.ignorePIs = settings.IgnoreProcessingInstructions;
			this.ignoreComments = settings.IgnoreComments;
			if (XmlSqlBinaryReader.TokenTypeMap == null)
			{
				this.GenerateTokenTypeMap();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000043FC File Offset: 0x000025FC
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				if (this.xntFromSettings)
				{
					xmlReaderSettings.NameTable = this.xnt;
				}
				int num = this.docState;
				if (num != 0)
				{
					if (num != 9)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
					}
					else
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
					}
				}
				else
				{
					xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
				}
				xmlReaderSettings.CheckCharacters = this.checkCharacters;
				xmlReaderSettings.IgnoreWhitespace = this.ignoreWhitespace;
				xmlReaderSettings.IgnoreProcessingInstructions = this.ignorePIs;
				xmlReaderSettings.IgnoreComments = this.ignoreComments;
				xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				xmlReaderSettings.CloseInput = this.closeInput;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000449D File Offset: 0x0000269D
		public override XmlNodeType NodeType
		{
			get
			{
				return this.nodetype;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000044A5 File Offset: 0x000026A5
		public override string LocalName
		{
			get
			{
				return this.qnameOther.localname;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000044B2 File Offset: 0x000026B2
		public override string NamespaceURI
		{
			get
			{
				return this.qnameOther.namespaceUri;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000044BF File Offset: 0x000026BF
		public override string Prefix
		{
			get
			{
				return this.qnameOther.prefix;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000044CC File Offset: 0x000026CC
		public override bool HasValue
		{
			get
			{
				if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
				{
					return this.textXmlReader.HasValue;
				}
				return XmlReader.HasValueInternal(this.nodetype);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000044F0 File Offset: 0x000026F0
		public override string Value
		{
			get
			{
				if (this.stringValue != null)
				{
					return this.stringValue;
				}
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
					switch (this.nodetype)
					{
					case XmlNodeType.Text:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						return this.stringValue = this.ValueAsString(this.token);
					case XmlNodeType.CDATA:
						return this.stringValue = this.CDATAValue();
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.DocumentType:
						return this.stringValue = this.GetString(this.tokDataPos, this.tokLen);
					case XmlNodeType.XmlDeclaration:
						return this.stringValue = this.XmlDeclValue();
					}
					break;
				case XmlSqlBinaryReader.ScanState.XmlText:
					return this.textXmlReader.Value;
				case XmlSqlBinaryReader.ScanState.Attr:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					return this.stringValue = this.GetAttributeText(this.attrIndex - 1);
				case XmlSqlBinaryReader.ScanState.AttrVal:
					return this.stringValue = this.ValueAsString(this.token);
				}
				return string.Empty;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004614 File Offset: 0x00002814
		public override int Depth
		{
			get
			{
				int num = 0;
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
					if (this.nodetype == XmlNodeType.Element || this.nodetype == XmlNodeType.EndElement)
					{
						num = -1;
					}
					break;
				case XmlSqlBinaryReader.ScanState.XmlText:
					num = this.textXmlReader.Depth;
					break;
				case XmlSqlBinaryReader.ScanState.Attr:
					if (this.parentNodeType != XmlNodeType.Element)
					{
						num = 1;
					}
					break;
				case XmlSqlBinaryReader.ScanState.AttrVal:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					if (this.parentNodeType != XmlNodeType.Element)
					{
						num = 1;
					}
					num++;
					break;
				default:
					return 0;
				}
				return this.elemDepth + num;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004693 File Offset: 0x00002893
		public override string BaseURI
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000469C File Offset: 0x0000289C
		public override bool IsEmptyElement
		{
			get
			{
				XmlSqlBinaryReader.ScanState scanState = this.state;
				return scanState <= XmlSqlBinaryReader.ScanState.XmlText && this.isEmpty;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000046BC File Offset: 0x000028BC
		public override XmlSpace XmlSpace
		{
			get
			{
				if (XmlSqlBinaryReader.ScanState.XmlText != this.state)
				{
					for (int i = this.elemDepth; i >= 0; i--)
					{
						XmlSpace xmlSpace = this.elementStack[i].xmlSpace;
						if (xmlSpace != XmlSpace.None)
						{
							return xmlSpace;
						}
					}
					return XmlSpace.None;
				}
				return this.textXmlReader.XmlSpace;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004708 File Offset: 0x00002908
		public override string XmlLang
		{
			get
			{
				if (XmlSqlBinaryReader.ScanState.XmlText != this.state)
				{
					for (int i = this.elemDepth; i >= 0; i--)
					{
						string xmlLang = this.elementStack[i].xmlLang;
						if (xmlLang != null)
						{
							return xmlLang;
						}
					}
					return string.Empty;
				}
				return this.textXmlReader.XmlLang;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004757 File Offset: 0x00002957
		public override Type ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004760 File Offset: 0x00002960
		public override int AttributeCount
		{
			get
			{
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
				case XmlSqlBinaryReader.ScanState.Attr:
				case XmlSqlBinaryReader.ScanState.AttrVal:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					return this.attrCount;
				case XmlSqlBinaryReader.ScanState.XmlText:
					return this.textXmlReader.AttributeCount;
				default:
					return 0;
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000047A4 File Offset: 0x000029A4
		public override string GetAttribute(string name, string ns)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.GetAttribute(name, ns);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int num = this.LocateAttribute(name, ns);
			if (-1 == num)
			{
				return null;
			}
			return this.GetAttribute(num);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000047F8 File Offset: 0x000029F8
		public override string GetAttribute(string name)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.GetAttribute(name);
			}
			int num = this.LocateAttribute(name);
			if (-1 == num)
			{
				return null;
			}
			return this.GetAttribute(num);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004830 File Offset: 0x00002A30
		public override string GetAttribute(int i)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.GetAttribute(i);
			}
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.GetAttributeText(i);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004868 File Offset: 0x00002A68
		public override bool MoveToAttribute(string name, string ns)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToAttribute(name, ns));
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			int num = this.LocateAttribute(name, ns);
			if (-1 != num && this.state < XmlSqlBinaryReader.ScanState.Init)
			{
				this.PositionOnAttribute(num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000048CC File Offset: 0x00002ACC
		public override bool MoveToAttribute(string name)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToAttribute(name));
			}
			int num = this.LocateAttribute(name);
			if (-1 != num && this.state < XmlSqlBinaryReader.ScanState.Init)
			{
				this.PositionOnAttribute(num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004918 File Offset: 0x00002B18
		public override void MoveToAttribute(int i)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				this.textXmlReader.MoveToAttribute(i);
				this.UpdateFromTextReader(true);
				return;
			}
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.PositionOnAttribute(i + 1);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004964 File Offset: 0x00002B64
		public override bool MoveToFirstAttribute()
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToFirstAttribute());
			}
			if (this.attrCount == 0)
			{
				return false;
			}
			this.PositionOnAttribute(1);
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004994 File Offset: 0x00002B94
		public override bool MoveToNextAttribute()
		{
			switch (this.state)
			{
			case XmlSqlBinaryReader.ScanState.Doc:
			case XmlSqlBinaryReader.ScanState.Attr:
			case XmlSqlBinaryReader.ScanState.AttrVal:
			case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
			{
				if (this.attrIndex >= this.attrCount)
				{
					return false;
				}
				int num = this.attrIndex + 1;
				this.attrIndex = num;
				this.PositionOnAttribute(num);
				return true;
			}
			case XmlSqlBinaryReader.ScanState.XmlText:
				return this.UpdateFromTextReader(this.textXmlReader.MoveToNextAttribute());
			default:
				return false;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004A00 File Offset: 0x00002C00
		public override bool MoveToElement()
		{
			XmlSqlBinaryReader.ScanState scanState = this.state;
			if (scanState == XmlSqlBinaryReader.ScanState.XmlText)
			{
				return this.UpdateFromTextReader(this.textXmlReader.MoveToElement());
			}
			if (scanState - XmlSqlBinaryReader.ScanState.Attr <= 2)
			{
				this.attrIndex = 0;
				this.qnameOther = this.qnameElement;
				if (XmlNodeType.Element == this.parentNodeType)
				{
					this.token = BinXmlToken.Element;
				}
				else if (XmlNodeType.XmlDeclaration == this.parentNodeType)
				{
					this.token = BinXmlToken.XmlDecl;
				}
				else if (XmlNodeType.DocumentType == this.parentNodeType)
				{
					this.token = BinXmlToken.DocType;
				}
				this.nodetype = this.parentNodeType;
				this.state = XmlSqlBinaryReader.ScanState.Doc;
				this.pos = this.posAfterAttrs;
				this.stringValue = null;
				return true;
			}
			return false;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public override bool EOF
		{
			get
			{
				return this.state == XmlSqlBinaryReader.ScanState.EOF;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public override bool ReadAttributeValue()
		{
			this.stringValue = null;
			switch (this.state)
			{
			case XmlSqlBinaryReader.ScanState.XmlText:
				return this.UpdateFromTextReader(this.textXmlReader.ReadAttributeValue());
			case XmlSqlBinaryReader.ScanState.Attr:
				if (this.attributes[this.attrIndex - 1].val == null)
				{
					this.pos = this.attributes[this.attrIndex - 1].contentPos;
					BinXmlToken binXmlToken = this.RescanNextToken();
					if (BinXmlToken.Attr == binXmlToken || BinXmlToken.EndAttrs == binXmlToken)
					{
						return false;
					}
					this.token = binXmlToken;
					this.ReScanOverValue(binXmlToken);
					this.valueType = this.GetValueType(binXmlToken);
					this.state = XmlSqlBinaryReader.ScanState.AttrVal;
				}
				else
				{
					this.token = BinXmlToken.Error;
					this.valueType = XmlSqlBinaryReader.TypeOfString;
					this.state = XmlSqlBinaryReader.ScanState.AttrValPseudoValue;
				}
				this.qnameOther.Clear();
				this.nodetype = XmlNodeType.Text;
				return true;
			case XmlSqlBinaryReader.ScanState.AttrVal:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004BAC File Offset: 0x00002DAC
		public override void Close()
		{
			this.state = XmlSqlBinaryReader.ScanState.Closed;
			this.nodetype = XmlNodeType.None;
			this.token = BinXmlToken.Error;
			this.stringValue = null;
			if (this.textXmlReader != null)
			{
				this.textXmlReader.Close();
				this.textXmlReader = null;
			}
			if (this.inStrm != null && this.closeInput)
			{
				this.inStrm.Close();
			}
			this.inStrm = null;
			this.pos = (this.end = 0);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004C21 File Offset: 0x00002E21
		public override XmlNameTable NameTable
		{
			get
			{
				return this.xnt;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004C2C File Offset: 0x00002E2C
		public override string LookupNamespace(string prefix)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return this.textXmlReader.LookupNamespace(prefix);
			}
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl;
			if (prefix != null && this.namespaces.TryGetValue(prefix, out namespaceDecl))
			{
				return namespaceDecl.uri;
			}
			return null;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004C6A File Offset: 0x00002E6A
		public override void ResolveEntity()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004C71 File Offset: 0x00002E71
		public override ReadState ReadState
		{
			get
			{
				return XmlSqlBinaryReader.ScanState2ReadState[(int)this.state];
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004C80 File Offset: 0x00002E80
		public override bool Read()
		{
			bool flag;
			try
			{
				switch (this.state)
				{
				case XmlSqlBinaryReader.ScanState.Doc:
					break;
				case XmlSqlBinaryReader.ScanState.XmlText:
					if (this.textXmlReader.Read())
					{
						return this.UpdateFromTextReader(true);
					}
					this.state = XmlSqlBinaryReader.ScanState.Doc;
					this.nodetype = XmlNodeType.None;
					this.isEmpty = false;
					break;
				case XmlSqlBinaryReader.ScanState.Attr:
				case XmlSqlBinaryReader.ScanState.AttrVal:
				case XmlSqlBinaryReader.ScanState.AttrValPseudoValue:
					this.MoveToElement();
					break;
				case XmlSqlBinaryReader.ScanState.Init:
					return this.ReadInit(false);
				default:
					return false;
				}
				flag = this.ReadDoc();
			}
			catch (OverflowException ex)
			{
				this.state = XmlSqlBinaryReader.ScanState.Error;
				throw new XmlException(ex.Message, ex);
			}
			catch
			{
				this.state = XmlSqlBinaryReader.ScanState.Error;
				throw;
			}
			return flag;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004D40 File Offset: 0x00002F40
		private bool SetupContentAsXXX(string name)
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException(name);
			}
			switch (this.state)
			{
			case XmlSqlBinaryReader.ScanState.Doc:
				if (this.NodeType == XmlNodeType.EndElement)
				{
					return true;
				}
				if (this.NodeType == XmlNodeType.ProcessingInstruction || this.NodeType == XmlNodeType.Comment)
				{
					while (this.Read() && (this.NodeType == XmlNodeType.ProcessingInstruction || this.NodeType == XmlNodeType.Comment))
					{
					}
					if (this.NodeType == XmlNodeType.EndElement)
					{
						return true;
					}
				}
				if (this.hasTypedValue)
				{
					return true;
				}
				break;
			case XmlSqlBinaryReader.ScanState.Attr:
			{
				this.pos = this.attributes[this.attrIndex - 1].contentPos;
				BinXmlToken binXmlToken = this.RescanNextToken();
				if (BinXmlToken.Attr != binXmlToken && BinXmlToken.EndAttrs != binXmlToken)
				{
					this.token = binXmlToken;
					this.ReScanOverValue(binXmlToken);
					return true;
				}
				break;
			}
			case XmlSqlBinaryReader.ScanState.AttrVal:
				return true;
			}
			return false;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004E1C File Offset: 0x0000301C
		private int FinishContentAsXXX(int origPos)
		{
			if (this.state == XmlSqlBinaryReader.ScanState.Doc)
			{
				if (this.NodeType != XmlNodeType.Element && this.NodeType != XmlNodeType.EndElement)
				{
					while (this.Read())
					{
						XmlNodeType nodeType = this.NodeType;
						if (nodeType == XmlNodeType.Element)
						{
							break;
						}
						if (nodeType - XmlNodeType.ProcessingInstruction > 1)
						{
							if (nodeType != XmlNodeType.EndElement)
							{
								throw this.ThrowNotSupported("Lists of BinaryXml value tokens not supported.");
							}
							break;
						}
					}
				}
				return this.pos;
			}
			return origPos;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004E78 File Offset: 0x00003078
		public override bool ReadContentAsBoolean()
		{
			int num = this.pos;
			bool flag = false;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsBoolean"))
				{
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_SMALLMONEY:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							break;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_0187;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_0143;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
							case BinXmlToken.XSD_QNAME:
								break;
							case (BinXmlToken)128:
								goto IL_0143;
							case BinXmlToken.XSD_BOOLEAN:
								flag = this.data[this.tokDataPos] > 0;
								goto IL_0171;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_0143;
								}
								return XmlConvert.ToBoolean(string.Empty);
							}
							break;
						}
						throw new InvalidCastException(Res.GetString("Token '{0}' does not support a conversion to Clr type '{1}'.", new object[] { this.token, "Boolean" }));
						IL_0143:
						goto IL_0187;
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex2, null);
					}
					IL_0171:
					num = this.FinishContentAsXXX(num);
					return flag;
				}
			}
			finally
			{
				this.pos = num;
			}
			IL_0187:
			return base.ReadContentAsBoolean();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005060 File Offset: 0x00003260
		public override DateTime ReadContentAsDateTime()
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsDateTime"))
				{
					DateTime dateTime;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_SMALLMONEY:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_00FC;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_0191;
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
							break;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_0138;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
								break;
							case (BinXmlToken)128:
								goto IL_0138;
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
							case BinXmlToken.XSD_QNAME:
								goto IL_00FC;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_0138;
								}
								return XmlConvert.ToDateTime(string.Empty, XmlDateTimeSerializationMode.RoundtripKind);
							}
							break;
						}
						dateTime = this.ValueAsDateTime();
						goto IL_017B;
						IL_00FC:
						throw new InvalidCastException(Res.GetString("Token '{0}' does not support a conversion to Clr type '{1}'.", new object[] { this.token, "DateTime" }));
						IL_0138:
						goto IL_0191;
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex3, null);
					}
					IL_017B:
					num = this.FinishContentAsXXX(num);
					return dateTime;
				}
			}
			finally
			{
				this.pos = num;
			}
			IL_0191:
			return base.ReadContentAsDateTime();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000526C File Offset: 0x0000346C
		public override double ReadContentAsDouble()
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsDouble"))
				{
					double num2;
					try
					{
						BinXmlToken binXmlToken = this.token;
						if (binXmlToken <= BinXmlToken.XSD_KATMAI_DATE)
						{
							switch (binXmlToken)
							{
							case BinXmlToken.SQL_SMALLINT:
							case BinXmlToken.SQL_INT:
							case BinXmlToken.SQL_MONEY:
							case BinXmlToken.SQL_BIT:
							case BinXmlToken.SQL_TINYINT:
							case BinXmlToken.SQL_BIGINT:
							case BinXmlToken.SQL_UUID:
							case BinXmlToken.SQL_DECIMAL:
							case BinXmlToken.SQL_NUMERIC:
							case BinXmlToken.SQL_BINARY:
							case BinXmlToken.SQL_VARBINARY:
							case BinXmlToken.SQL_DATETIME:
							case BinXmlToken.SQL_SMALLDATETIME:
							case BinXmlToken.SQL_SMALLMONEY:
							case BinXmlToken.SQL_IMAGE:
							case BinXmlToken.SQL_UDT:
								break;
							case BinXmlToken.SQL_REAL:
							case BinXmlToken.SQL_FLOAT:
								num2 = this.ValueAsDouble();
								goto IL_013E;
							case BinXmlToken.SQL_CHAR:
							case BinXmlToken.SQL_NCHAR:
							case BinXmlToken.SQL_VARCHAR:
							case BinXmlToken.SQL_NVARCHAR:
							case BinXmlToken.SQL_TEXT:
							case BinXmlToken.SQL_NTEXT:
								goto IL_0154;
							case (BinXmlToken)21:
							case (BinXmlToken)25:
							case (BinXmlToken)26:
								goto IL_00FB;
							default:
								if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5)
								{
									goto IL_00FB;
								}
								break;
							}
						}
						else if (binXmlToken - BinXmlToken.XSD_TIME > 11)
						{
							if (binXmlToken - BinXmlToken.EndElem > 1)
							{
								goto IL_00FB;
							}
							return XmlConvert.ToDouble(string.Empty);
						}
						throw new InvalidCastException(Res.GetString("Token '{0}' does not support a conversion to Clr type '{1}'.", new object[] { this.token, "Double" }));
						IL_00FB:
						goto IL_0154;
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex3, null);
					}
					IL_013E:
					num = this.FinishContentAsXXX(num);
					return num2;
				}
			}
			finally
			{
				this.pos = num;
			}
			IL_0154:
			return base.ReadContentAsDouble();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000543C File Offset: 0x0000363C
		public override float ReadContentAsFloat()
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsFloat"))
				{
					float num2;
					try
					{
						BinXmlToken binXmlToken = this.token;
						if (binXmlToken <= BinXmlToken.XSD_KATMAI_DATE)
						{
							switch (binXmlToken)
							{
							case BinXmlToken.SQL_SMALLINT:
							case BinXmlToken.SQL_INT:
							case BinXmlToken.SQL_MONEY:
							case BinXmlToken.SQL_BIT:
							case BinXmlToken.SQL_TINYINT:
							case BinXmlToken.SQL_BIGINT:
							case BinXmlToken.SQL_UUID:
							case BinXmlToken.SQL_DECIMAL:
							case BinXmlToken.SQL_NUMERIC:
							case BinXmlToken.SQL_BINARY:
							case BinXmlToken.SQL_VARBINARY:
							case BinXmlToken.SQL_DATETIME:
							case BinXmlToken.SQL_SMALLDATETIME:
							case BinXmlToken.SQL_SMALLMONEY:
							case BinXmlToken.SQL_IMAGE:
							case BinXmlToken.SQL_UDT:
								break;
							case BinXmlToken.SQL_REAL:
							case BinXmlToken.SQL_FLOAT:
								num2 = (float)this.ValueAsDouble();
								goto IL_013F;
							case BinXmlToken.SQL_CHAR:
							case BinXmlToken.SQL_NCHAR:
							case BinXmlToken.SQL_VARCHAR:
							case BinXmlToken.SQL_NVARCHAR:
							case BinXmlToken.SQL_TEXT:
							case BinXmlToken.SQL_NTEXT:
								goto IL_0155;
							case (BinXmlToken)21:
							case (BinXmlToken)25:
							case (BinXmlToken)26:
								goto IL_00FC;
							default:
								if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5)
								{
									goto IL_00FC;
								}
								break;
							}
						}
						else if (binXmlToken - BinXmlToken.XSD_TIME > 11)
						{
							if (binXmlToken - BinXmlToken.EndElem > 1)
							{
								goto IL_00FC;
							}
							return XmlConvert.ToSingle(string.Empty);
						}
						throw new InvalidCastException(Res.GetString("Token '{0}' does not support a conversion to Clr type '{1}'.", new object[] { this.token, "Float" }));
						IL_00FC:
						goto IL_0155;
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex3, null);
					}
					IL_013F:
					num = this.FinishContentAsXXX(num);
					return num2;
				}
			}
			finally
			{
				this.pos = num;
			}
			IL_0155:
			return base.ReadContentAsFloat();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000560C File Offset: 0x0000380C
		public override decimal ReadContentAsDecimal()
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsDecimal"))
				{
					decimal num2;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_SMALLMONEY:
							break;
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_00FC;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_0190;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_0137;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_QNAME:
								goto IL_00FC;
							case (BinXmlToken)128:
								goto IL_0137;
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
								break;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_0137;
								}
								return XmlConvert.ToDecimal(string.Empty);
							}
							break;
						}
						num2 = this.ValueAsDecimal();
						goto IL_017A;
						IL_00FC:
						throw new InvalidCastException(Res.GetString("Token '{0}' does not support a conversion to Clr type '{1}'.", new object[] { this.token, "Decimal" }));
						IL_0137:
						goto IL_0190;
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex3, null);
					}
					IL_017A:
					num = this.FinishContentAsXXX(num);
					return num2;
				}
			}
			finally
			{
				this.pos = num;
			}
			IL_0190:
			return base.ReadContentAsDecimal();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005818 File Offset: 0x00003A18
		public override int ReadContentAsInt()
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsInt"))
				{
					int num2;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_SMALLMONEY:
							break;
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_00FD;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_0191;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_0138;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_QNAME:
								goto IL_00FD;
							case (BinXmlToken)128:
								goto IL_0138;
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
								break;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_0138;
								}
								return XmlConvert.ToInt32(string.Empty);
							}
							break;
						}
						num2 = checked((int)this.ValueAsLong());
						goto IL_017B;
						IL_00FD:
						throw new InvalidCastException(Res.GetString("Token '{0}' does not support a conversion to Clr type '{1}'.", new object[] { this.token, "Int32" }));
						IL_0138:
						goto IL_0191;
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Int32", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Int32", ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Int32", ex3, null);
					}
					IL_017B:
					num = this.FinishContentAsXXX(num);
					return num2;
				}
			}
			finally
			{
				this.pos = num;
			}
			IL_0191:
			return base.ReadContentAsInt();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005A24 File Offset: 0x00003C24
		public override long ReadContentAsLong()
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsLong"))
				{
					long num2;
					try
					{
						BinXmlToken binXmlToken = this.token;
						switch (binXmlToken)
						{
						case BinXmlToken.SQL_SMALLINT:
						case BinXmlToken.SQL_INT:
						case BinXmlToken.SQL_MONEY:
						case BinXmlToken.SQL_BIT:
						case BinXmlToken.SQL_TINYINT:
						case BinXmlToken.SQL_BIGINT:
						case BinXmlToken.SQL_DECIMAL:
						case BinXmlToken.SQL_NUMERIC:
						case BinXmlToken.SQL_SMALLMONEY:
							break;
						case BinXmlToken.SQL_REAL:
						case BinXmlToken.SQL_FLOAT:
						case BinXmlToken.SQL_UUID:
						case BinXmlToken.SQL_BINARY:
						case BinXmlToken.SQL_VARBINARY:
						case BinXmlToken.SQL_DATETIME:
						case BinXmlToken.SQL_SMALLDATETIME:
						case BinXmlToken.SQL_IMAGE:
						case BinXmlToken.SQL_UDT:
							goto IL_00FC;
						case BinXmlToken.SQL_CHAR:
						case BinXmlToken.SQL_NCHAR:
						case BinXmlToken.SQL_VARCHAR:
						case BinXmlToken.SQL_NVARCHAR:
						case BinXmlToken.SQL_TEXT:
						case BinXmlToken.SQL_NTEXT:
							goto IL_0190;
						case (BinXmlToken)21:
						case (BinXmlToken)25:
						case (BinXmlToken)26:
							goto IL_0137;
						default:
							switch (binXmlToken)
							{
							case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
							case BinXmlToken.XSD_KATMAI_DATEOFFSET:
							case BinXmlToken.XSD_KATMAI_TIME:
							case BinXmlToken.XSD_KATMAI_DATETIME:
							case BinXmlToken.XSD_KATMAI_DATE:
							case BinXmlToken.XSD_TIME:
							case BinXmlToken.XSD_DATETIME:
							case BinXmlToken.XSD_DATE:
							case BinXmlToken.XSD_BINHEX:
							case BinXmlToken.XSD_BASE64:
							case BinXmlToken.XSD_BOOLEAN:
							case BinXmlToken.XSD_QNAME:
								goto IL_00FC;
							case (BinXmlToken)128:
								goto IL_0137;
							case BinXmlToken.XSD_DECIMAL:
							case BinXmlToken.XSD_BYTE:
							case BinXmlToken.XSD_UNSIGNEDSHORT:
							case BinXmlToken.XSD_UNSIGNEDINT:
							case BinXmlToken.XSD_UNSIGNEDLONG:
								break;
							default:
								if (binXmlToken - BinXmlToken.EndElem > 1)
								{
									goto IL_0137;
								}
								return XmlConvert.ToInt64(string.Empty);
							}
							break;
						}
						num2 = this.ValueAsLong();
						goto IL_017A;
						IL_00FC:
						throw new InvalidCastException(Res.GetString("Token '{0}' does not support a conversion to Clr type '{1}'.", new object[] { this.token, "Int64" }));
						IL_0137:
						goto IL_0190;
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Int64", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Int64", ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Int64", ex3, null);
					}
					IL_017A:
					num = this.FinishContentAsXXX(num);
					return num2;
				}
			}
			finally
			{
				this.pos = num;
			}
			IL_0190:
			return base.ReadContentAsLong();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005C30 File Offset: 0x00003E30
		public override object ReadContentAsObject()
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAsObject"))
				{
					object obj;
					try
					{
						if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
						{
							obj = string.Empty;
						}
						else
						{
							obj = this.ValueAsObject(this.token, false);
						}
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Object", ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Object", ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", "Object", ex3, null);
					}
					num = this.FinishContentAsXXX(num);
					return obj;
				}
			}
			finally
			{
				this.pos = num;
			}
			return base.ReadContentAsObject();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005D0C File Offset: 0x00003F0C
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			int num = this.pos;
			try
			{
				if (this.SetupContentAsXXX("ReadContentAs"))
				{
					object obj;
					try
					{
						if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
						{
							obj = string.Empty;
						}
						else if (returnType == this.ValueType || returnType == typeof(object))
						{
							obj = this.ValueAsObject(this.token, false);
						}
						else
						{
							obj = this.ValueAs(this.token, returnType, namespaceResolver);
						}
					}
					catch (InvalidCastException ex)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, null);
					}
					catch (FormatException ex2)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, null);
					}
					catch (OverflowException ex3)
					{
						throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex3, null);
					}
					num = this.FinishContentAsXXX(num);
					return obj;
				}
			}
			finally
			{
				this.pos = num;
			}
			return base.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005E20 File Offset: 0x00004020
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return ((IXmlNamespaceResolver)this.textXmlReader).GetNamespacesInScope(scope);
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (XmlNamespaceScope.Local == scope)
			{
				if (this.elemDepth > 0)
				{
					for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.elementStack[this.elemDepth].nsdecls; namespaceDecl != null; namespaceDecl = namespaceDecl.scopeLink)
					{
						dictionary.Add(namespaceDecl.prefix, namespaceDecl.uri);
					}
				}
			}
			else
			{
				foreach (XmlSqlBinaryReader.NamespaceDecl namespaceDecl2 in this.namespaces.Values)
				{
					if ((namespaceDecl2.scope != -1 || (scope == XmlNamespaceScope.All && "xml" == namespaceDecl2.prefix)) && (namespaceDecl2.prefix.Length > 0 || namespaceDecl2.uri.Length > 0))
					{
						dictionary.Add(namespaceDecl2.prefix, namespaceDecl2.uri);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005F2C File Offset: 0x0000412C
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (XmlSqlBinaryReader.ScanState.XmlText == this.state)
			{
				return ((IXmlNamespaceResolver)this.textXmlReader).LookupPrefix(namespaceName);
			}
			if (namespaceName == null)
			{
				return null;
			}
			namespaceName = this.xnt.Get(namespaceName);
			if (namespaceName == null)
			{
				return null;
			}
			for (int i = this.elemDepth; i >= 0; i--)
			{
				for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.elementStack[i].nsdecls; namespaceDecl != null; namespaceDecl = namespaceDecl.scopeLink)
				{
					if (namespaceDecl.uri == namespaceName)
					{
						return namespaceDecl.prefix;
					}
				}
			}
			return null;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005FAC File Offset: 0x000041AC
		private void VerifyVersion(int requiredVersion, BinXmlToken token)
		{
			if ((int)this.version < requiredVersion)
			{
				throw this.ThrowUnexpectedToken(token);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005FC0 File Offset: 0x000041C0
		private void AddInitNamespace(string prefix, string uri)
		{
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl = new XmlSqlBinaryReader.NamespaceDecl(prefix, uri, this.elementStack[0].nsdecls, null, -1, true);
			this.elementStack[0].nsdecls = namespaceDecl;
			this.namespaces.Add(prefix, namespaceDecl);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006008 File Offset: 0x00004208
		private void AddName()
		{
			string text = this.ParseText();
			int symCount = this.symbolTables.symCount;
			this.symbolTables.symCount = symCount + 1;
			int num = symCount;
			string[] array = this.symbolTables.symtable;
			if (num == array.Length)
			{
				string[] array2 = new string[checked(num * 2)];
				Array.Copy(array, 0, array2, 0, num);
				array = (this.symbolTables.symtable = array2);
			}
			array[num] = this.xnt.Add(text);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006078 File Offset: 0x00004278
		private void AddQName()
		{
			int num = this.ReadNameRef();
			int num2 = this.ReadNameRef();
			int num3 = this.ReadNameRef();
			int qnameCount = this.symbolTables.qnameCount;
			this.symbolTables.qnameCount = qnameCount + 1;
			int num4 = qnameCount;
			XmlSqlBinaryReader.QName[] array = this.symbolTables.qnametable;
			if (num4 == array.Length)
			{
				XmlSqlBinaryReader.QName[] array2 = new XmlSqlBinaryReader.QName[checked(num4 * 2)];
				Array.Copy(array, 0, array2, 0, num4);
				array = (this.symbolTables.qnametable = array2);
			}
			string[] symtable = this.symbolTables.symtable;
			string text = symtable[num2];
			string text2;
			string text3;
			if (num3 == 0)
			{
				if (num2 == 0 && num == 0)
				{
					return;
				}
				if (text.StartsWith("xmlns", StringComparison.Ordinal))
				{
					if (5 < text.Length)
					{
						if (6 == text.Length || ':' != text[5])
						{
							goto IL_0106;
						}
						text2 = this.xnt.Add(text.Substring(6));
						text = this.xmlns;
					}
					else
					{
						text2 = text;
						text = string.Empty;
					}
					text3 = this.nsxmlns;
					goto IL_00F2;
				}
				IL_0106:
				throw new XmlException("Invalid namespace declaration.", null);
			}
			else
			{
				text2 = symtable[num3];
				text3 = symtable[num];
			}
			IL_00F2:
			array[num4].Set(text, text2, text3);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006198 File Offset: 0x00004398
		private void NameFlush()
		{
			this.symbolTables.symCount = (this.symbolTables.qnameCount = 1);
			Array.Clear(this.symbolTables.symtable, 1, this.symbolTables.symtable.Length - 1);
			Array.Clear(this.symbolTables.qnametable, 0, this.symbolTables.qnametable.Length);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006200 File Offset: 0x00004400
		private void SkipExtn()
		{
			int num = this.ParseMB32();
			checked
			{
				this.pos += num;
				this.Fill(-1);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000622C File Offset: 0x0000442C
		private int ReadQNameRef()
		{
			int num = this.ParseMB32();
			if (num < 0 || num >= this.symbolTables.qnameCount)
			{
				throw new XmlException("Invalid QName ID.", string.Empty);
			}
			return num;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006264 File Offset: 0x00004464
		private int ReadNameRef()
		{
			int num = this.ParseMB32();
			if (num < 0 || num >= this.symbolTables.symCount)
			{
				throw new XmlException("Invalid QName ID.", string.Empty);
			}
			return num;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000629C File Offset: 0x0000449C
		private bool FillAllowEOF()
		{
			if (this.eof)
			{
				return false;
			}
			byte[] array = this.data;
			int num = this.pos;
			int num2 = this.mark;
			int num3 = this.end;
			if (num2 == -1)
			{
				num2 = num;
			}
			if (num2 >= 0 && num2 < num3)
			{
				int num4 = num3 - num2;
				if (num4 > 7 * (array.Length / 8))
				{
					byte[] array2 = new byte[checked(array.Length * 2)];
					Array.Copy(array, num2, array2, 0, num4);
					array = (this.data = array2);
				}
				else
				{
					Array.Copy(array, num2, array, 0, num4);
				}
				num -= num2;
				num3 -= num2;
				this.tokDataPos -= num2;
				for (int i = 0; i < this.attrCount; i++)
				{
					this.attributes[i].AdjustPosition(-num2);
				}
				this.pos = num;
				this.mark = 0;
				this.offset += (long)num2;
			}
			else
			{
				this.pos -= num3;
				this.mark -= num3;
				this.offset += (long)num3;
				this.tokDataPos -= num3;
				num3 = 0;
			}
			int num5 = array.Length - num3;
			int num6 = this.inStrm.Read(array, num3, num5);
			this.end = num3 + num6;
			this.eof = num6 <= 0;
			return num6 > 0;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000063F0 File Offset: 0x000045F0
		private void Fill_(int require)
		{
			while (this.FillAllowEOF() && this.pos + require >= this.end)
			{
			}
			if (this.pos + require >= this.end)
			{
				throw this.ThrowXmlException("Unexpected end of file has occurred.");
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006426 File Offset: 0x00004626
		private void Fill(int require)
		{
			if (this.pos + require >= this.end)
			{
				this.Fill_(require);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006440 File Offset: 0x00004640
		private byte ReadByte()
		{
			this.Fill(0);
			byte[] array = this.data;
			int num = this.pos;
			this.pos = num + 1;
			return array[num];
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000646C File Offset: 0x0000466C
		private ushort ReadUShort()
		{
			this.Fill(1);
			int num = this.pos;
			byte[] array = this.data;
			ushort num2 = (ushort)((int)array[num] + ((int)array[num + 1] << 8));
			this.pos += 2;
			return num2;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000064A8 File Offset: 0x000046A8
		private int ParseMB32()
		{
			byte b = this.ReadByte();
			if (b > 127)
			{
				return this.ParseMB32_(b);
			}
			return (int)b;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000064CC File Offset: 0x000046CC
		private int ParseMB32_(byte b)
		{
			uint num = (uint)(b & 127);
			b = this.ReadByte();
			uint num2 = (uint)(b & 127);
			num += num2 << 7;
			if (b > 127)
			{
				b = this.ReadByte();
				num2 = (uint)(b & 127);
				num += num2 << 14;
				if (b > 127)
				{
					b = this.ReadByte();
					num2 = (uint)(b & 127);
					num += num2 << 21;
					if (b > 127)
					{
						b = this.ReadByte();
						num2 = (uint)(b & 7);
						if (b > 7)
						{
							throw this.ThrowXmlException("The value is too big to fit into an Int32. The arithmetic operation resulted in an overflow.");
						}
						num += num2 << 28;
					}
				}
			}
			return (int)num;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000654C File Offset: 0x0000474C
		private int ParseMB32(int pos)
		{
			byte[] array = this.data;
			byte b = array[pos++];
			uint num = (uint)(b & 127);
			if (b > 127)
			{
				byte b2 = array[pos++];
				uint num2 = (uint)(b2 & 127);
				num += num2 << 7;
				if (b2 > 127)
				{
					byte b3 = array[pos++];
					num2 = (uint)(b3 & 127);
					num += num2 << 14;
					if (b3 > 127)
					{
						byte b4 = array[pos++];
						num2 = (uint)(b4 & 127);
						num += num2 << 21;
						if (b4 > 127)
						{
							byte b5 = array[pos++];
							num2 = (uint)(b5 & 7);
							if (b5 > 7)
							{
								throw this.ThrowXmlException("The value is too big to fit into an Int32. The arithmetic operation resulted in an overflow.");
							}
							num += num2 << 28;
						}
					}
				}
			}
			return (int)num;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000065DC File Offset: 0x000047DC
		private int ParseMB64()
		{
			byte b = this.ReadByte();
			if (b > 127)
			{
				return this.ParseMB32_(b);
			}
			return (int)b;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000065FE File Offset: 0x000047FE
		private BinXmlToken PeekToken()
		{
			while (this.pos >= this.end && this.FillAllowEOF())
			{
			}
			if (this.pos >= this.end)
			{
				return BinXmlToken.EOF;
			}
			return (BinXmlToken)this.data[this.pos];
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006634 File Offset: 0x00004834
		private BinXmlToken ReadToken()
		{
			while (this.pos >= this.end && this.FillAllowEOF())
			{
			}
			if (this.pos >= this.end)
			{
				return BinXmlToken.EOF;
			}
			byte[] array = this.data;
			int num = this.pos;
			this.pos = num + 1;
			return array[num];
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006680 File Offset: 0x00004880
		private BinXmlToken NextToken2(BinXmlToken token)
		{
			for (;;)
			{
				if (token <= BinXmlToken.Extn)
				{
					if (token != BinXmlToken.NmFlush)
					{
						if (token != BinXmlToken.Extn)
						{
							break;
						}
						this.SkipExtn();
					}
					else
					{
						this.NameFlush();
					}
				}
				else if (token != BinXmlToken.QName)
				{
					if (token != BinXmlToken.Name)
					{
						break;
					}
					this.AddName();
				}
				else
				{
					this.AddQName();
				}
				token = this.ReadToken();
			}
			return token;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000066E4 File Offset: 0x000048E4
		private BinXmlToken NextToken1()
		{
			int num = this.pos;
			BinXmlToken binXmlToken;
			if (num >= this.end)
			{
				binXmlToken = this.ReadToken();
			}
			else
			{
				binXmlToken = (BinXmlToken)this.data[num];
				this.pos = num + 1;
			}
			if (binXmlToken >= BinXmlToken.NmFlush && binXmlToken <= BinXmlToken.Name)
			{
				return this.NextToken2(binXmlToken);
			}
			return binXmlToken;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006738 File Offset: 0x00004938
		private BinXmlToken NextToken()
		{
			int num = this.pos;
			if (num < this.end)
			{
				BinXmlToken binXmlToken = (BinXmlToken)this.data[num];
				if (binXmlToken < BinXmlToken.NmFlush || binXmlToken > BinXmlToken.Name)
				{
					this.pos = num + 1;
					return binXmlToken;
				}
			}
			return this.NextToken1();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006780 File Offset: 0x00004980
		private BinXmlToken PeekNextToken()
		{
			BinXmlToken binXmlToken = this.NextToken();
			if (BinXmlToken.EOF != binXmlToken)
			{
				this.pos--;
			}
			return binXmlToken;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000067A8 File Offset: 0x000049A8
		private BinXmlToken RescanNextToken()
		{
			checked
			{
				BinXmlToken binXmlToken;
				for (;;)
				{
					binXmlToken = this.ReadToken();
					if (binXmlToken <= BinXmlToken.Extn)
					{
						if (binXmlToken != BinXmlToken.NmFlush)
						{
							if (binXmlToken != BinXmlToken.Extn)
							{
								break;
							}
							int num = this.ParseMB32();
							this.pos += num;
						}
					}
					else if (binXmlToken != BinXmlToken.QName)
					{
						if (binXmlToken != BinXmlToken.Name)
						{
							break;
						}
						int num2 = this.ParseMB32();
						this.pos += 2 * num2;
					}
					else
					{
						this.ParseMB32();
						this.ParseMB32();
						this.ParseMB32();
					}
				}
				return binXmlToken;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006830 File Offset: 0x00004A30
		private string ParseText()
		{
			int num = this.mark;
			string @string;
			try
			{
				if (num < 0)
				{
					this.mark = this.pos;
				}
				int num3;
				int num2 = this.ScanText(out num3);
				@string = this.GetString(num3, num2);
			}
			finally
			{
				if (num < 0)
				{
					this.mark = -1;
				}
			}
			return @string;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006888 File Offset: 0x00004A88
		private int ScanText(out int start)
		{
			int num = this.ParseMB32();
			int num2 = this.mark;
			int num3 = this.pos;
			checked
			{
				this.pos += num * 2;
				if (this.pos > this.end)
				{
					this.Fill(-1);
				}
			}
			start = num3 - (num2 - this.mark);
			return num;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000068DC File Offset: 0x00004ADC
		private string GetString(int pos, int cch)
		{
			checked
			{
				if (pos + cch * 2 > this.end)
				{
					throw new XmlException("Unexpected end of file has occurred.", null);
				}
				if (cch == 0)
				{
					return string.Empty;
				}
				if ((pos & 1) == 0)
				{
					return this.GetStringAligned(this.data, pos, cch);
				}
				return this.unicode.GetString(this.data, pos, cch * 2);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006934 File Offset: 0x00004B34
		private unsafe string GetStringAligned(byte[] data, int offset, int cch)
		{
			byte* ptr;
			if (data == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &data[0];
			}
			char* ptr2 = (char*)(ptr + offset);
			return new string(ptr2, 0, cch);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006968 File Offset: 0x00004B68
		private string GetAttributeText(int i)
		{
			string val = this.attributes[i].val;
			if (val != null)
			{
				return val;
			}
			int num = this.pos;
			string text;
			try
			{
				this.pos = this.attributes[i].contentPos;
				BinXmlToken binXmlToken = this.RescanNextToken();
				if (BinXmlToken.Attr == binXmlToken || BinXmlToken.EndAttrs == binXmlToken)
				{
					text = "";
				}
				else
				{
					this.token = binXmlToken;
					this.ReScanOverValue(binXmlToken);
					text = this.ValueAsString(binXmlToken);
				}
			}
			finally
			{
				this.pos = num;
			}
			return text;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000069FC File Offset: 0x00004BFC
		private int LocateAttribute(string name, string ns)
		{
			for (int i = 0; i < this.attrCount; i++)
			{
				if (this.attributes[i].name.MatchNs(name, ns))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006A38 File Offset: 0x00004C38
		private int LocateAttribute(string name)
		{
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			for (int i = 0; i < this.attrCount; i++)
			{
				if (this.attributes[i].name.MatchPrefix(text, text2))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006A80 File Offset: 0x00004C80
		private void PositionOnAttribute(int i)
		{
			this.attrIndex = i;
			this.qnameOther = this.attributes[i - 1].name;
			if (this.state == XmlSqlBinaryReader.ScanState.Doc)
			{
				this.parentNodeType = this.nodetype;
			}
			this.token = BinXmlToken.Attr;
			this.nodetype = XmlNodeType.Attribute;
			this.state = XmlSqlBinaryReader.ScanState.Attr;
			this.valueType = XmlSqlBinaryReader.TypeOfObject;
			this.stringValue = null;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006AEC File Offset: 0x00004CEC
		private void GrowElements()
		{
			XmlSqlBinaryReader.ElemInfo[] array = new XmlSqlBinaryReader.ElemInfo[this.elementStack.Length * 2];
			Array.Copy(this.elementStack, 0, array, 0, this.elementStack.Length);
			this.elementStack = array;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006B28 File Offset: 0x00004D28
		private void GrowAttributes()
		{
			XmlSqlBinaryReader.AttrInfo[] array = new XmlSqlBinaryReader.AttrInfo[this.attributes.Length * 2];
			Array.Copy(this.attributes, 0, array, 0, this.attrCount);
			this.attributes = array;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006B60 File Offset: 0x00004D60
		private void ClearAttributes()
		{
			if (this.attrCount != 0)
			{
				this.attrCount = 0;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006B74 File Offset: 0x00004D74
		private void PushNamespace(string prefix, string ns, bool implied)
		{
			if (prefix == "xml")
			{
				return;
			}
			int num = this.elemDepth;
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl;
			this.namespaces.TryGetValue(prefix, out namespaceDecl);
			if (namespaceDecl != null)
			{
				if (namespaceDecl.uri == ns)
				{
					if (!implied && namespaceDecl.implied && namespaceDecl.scope == num)
					{
						namespaceDecl.implied = false;
					}
					return;
				}
				this.qnameElement.CheckPrefixNS(prefix, ns);
				if (prefix.Length != 0)
				{
					for (int i = 0; i < this.attrCount; i++)
					{
						if (this.attributes[i].name.prefix.Length != 0)
						{
							this.attributes[i].name.CheckPrefixNS(prefix, ns);
						}
					}
				}
			}
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl2 = new XmlSqlBinaryReader.NamespaceDecl(prefix, ns, this.elementStack[num].nsdecls, namespaceDecl, num, implied);
			this.elementStack[num].nsdecls = namespaceDecl2;
			this.namespaces[prefix] = namespaceDecl2;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006C6C File Offset: 0x00004E6C
		private void PopNamespaces(XmlSqlBinaryReader.NamespaceDecl firstInScopeChain)
		{
			XmlSqlBinaryReader.NamespaceDecl scopeLink;
			for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = firstInScopeChain; namespaceDecl != null; namespaceDecl = scopeLink)
			{
				if (namespaceDecl.prevLink == null)
				{
					this.namespaces.Remove(namespaceDecl.prefix);
				}
				else
				{
					this.namespaces[namespaceDecl.prefix] = namespaceDecl.prevLink;
				}
				scopeLink = namespaceDecl.scopeLink;
				namespaceDecl.prevLink = null;
				namespaceDecl.scopeLink = null;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006CC8 File Offset: 0x00004EC8
		private void GenerateImpliedXmlnsAttrs()
		{
			for (XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.elementStack[this.elemDepth].nsdecls; namespaceDecl != null; namespaceDecl = namespaceDecl.scopeLink)
			{
				if (namespaceDecl.implied)
				{
					if (this.attrCount == this.attributes.Length)
					{
						this.GrowAttributes();
					}
					XmlSqlBinaryReader.QName qname;
					if (namespaceDecl.prefix.Length == 0)
					{
						qname = new XmlSqlBinaryReader.QName(string.Empty, this.xmlns, this.nsxmlns);
					}
					else
					{
						qname = new XmlSqlBinaryReader.QName(this.xmlns, this.xnt.Add(namespaceDecl.prefix), this.nsxmlns);
					}
					this.attributes[this.attrCount].Set(qname, namespaceDecl.uri);
					this.attrCount++;
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006D98 File Offset: 0x00004F98
		private bool ReadInit(bool skipXmlDecl)
		{
			string text;
			if (!this.sniffed && this.ReadUShort() != 65503)
			{
				text = "Invalid BinaryXml signature.";
			}
			else
			{
				this.version = this.ReadByte();
				if (this.version != 1 && this.version != 2)
				{
					text = "Invalid BinaryXml protocol version.";
				}
				else
				{
					if (1200 == this.ReadUShort())
					{
						this.state = XmlSqlBinaryReader.ScanState.Doc;
						if (BinXmlToken.XmlDecl == this.PeekToken())
						{
							this.pos++;
							this.attributes[0].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("version"), string.Empty), this.ParseText());
							this.attrCount = 1;
							if (BinXmlToken.Encoding == this.PeekToken())
							{
								this.pos++;
								this.attributes[1].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("encoding"), string.Empty), this.ParseText());
								this.attrCount++;
							}
							byte b = this.ReadByte();
							if (b != 0)
							{
								if (b - 1 > 1)
								{
									text = "Invalid BinaryXml standalone token.";
									goto IL_01E2;
								}
								this.attributes[this.attrCount].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("standalone"), string.Empty), (b == 1) ? "yes" : "no");
								this.attrCount++;
							}
							if (!skipXmlDecl)
							{
								XmlSqlBinaryReader.QName qname = new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("xml"), string.Empty);
								this.qnameOther = (this.qnameElement = qname);
								this.nodetype = XmlNodeType.XmlDeclaration;
								this.posAfterAttrs = this.pos;
								return true;
							}
						}
						return this.ReadDoc();
					}
					text = "Unsupported BinaryXml codepage.";
				}
			}
			IL_01E2:
			this.state = XmlSqlBinaryReader.ScanState.Error;
			throw new XmlException(text, null);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006F98 File Offset: 0x00005198
		private void ScanAttributes()
		{
			int num = -1;
			int num2 = -1;
			this.mark = this.pos;
			string text = null;
			bool flag = false;
			BinXmlToken binXmlToken;
			while (BinXmlToken.EndAttrs != (binXmlToken = this.NextToken()))
			{
				if (BinXmlToken.Attr == binXmlToken)
				{
					if (text != null)
					{
						this.PushNamespace(text, string.Empty, false);
						text = null;
					}
					if (this.attrCount == this.attributes.Length)
					{
						this.GrowAttributes();
					}
					XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[this.ReadQNameRef()];
					this.attributes[this.attrCount].Set(qname, this.pos);
					if (qname.prefix == "xml")
					{
						if (qname.localname == "lang")
						{
							num2 = this.attrCount;
						}
						else if (qname.localname == "space")
						{
							num = this.attrCount;
						}
					}
					else if (Ref.Equal(qname.namespaceUri, this.nsxmlns))
					{
						text = qname.localname;
						if (text == "xmlns")
						{
							text = string.Empty;
						}
					}
					else if (qname.prefix.Length != 0)
					{
						if (qname.namespaceUri.Length == 0)
						{
							throw new XmlException("Cannot use a prefix with an empty namespace.", string.Empty);
						}
						this.PushNamespace(qname.prefix, qname.namespaceUri, true);
					}
					else if (qname.namespaceUri.Length != 0)
					{
						throw this.ThrowXmlException("Attribute '{0}' has namespace '{1}' but no prefix.", qname.localname, qname.namespaceUri);
					}
					this.attrCount++;
					flag = false;
				}
				else
				{
					this.ScanOverValue(binXmlToken, true, true);
					if (flag)
					{
						throw this.ThrowNotSupported("Lists of BinaryXml value tokens not supported.");
					}
					string text2 = this.stringValue;
					if (text2 != null)
					{
						this.attributes[this.attrCount - 1].val = text2;
						this.stringValue = null;
					}
					if (text != null)
					{
						string text3 = this.xnt.Add(this.ValueAsString(binXmlToken));
						this.PushNamespace(text, text3, false);
						text = null;
					}
					flag = true;
				}
			}
			if (num != -1)
			{
				string attributeText = this.GetAttributeText(num);
				XmlSpace xmlSpace = XmlSpace.None;
				if (attributeText == "preserve")
				{
					xmlSpace = XmlSpace.Preserve;
				}
				else if (attributeText == "default")
				{
					xmlSpace = XmlSpace.Default;
				}
				this.elementStack[this.elemDepth].xmlSpace = xmlSpace;
				this.xmlspacePreserve = XmlSpace.Preserve == xmlSpace;
			}
			if (num2 != -1)
			{
				this.elementStack[this.elemDepth].xmlLang = this.GetAttributeText(num2);
			}
			if (this.attrCount < 200)
			{
				this.SimpleCheckForDuplicateAttributes();
				return;
			}
			this.HashCheckForDuplicateAttributes();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000723C File Offset: 0x0000543C
		private void SimpleCheckForDuplicateAttributes()
		{
			for (int i = 0; i < this.attrCount; i++)
			{
				string text;
				string text2;
				this.attributes[i].GetLocalnameAndNamespaceUri(out text, out text2);
				for (int j = i + 1; j < this.attrCount; j++)
				{
					if (this.attributes[j].MatchNS(text, text2))
					{
						throw new XmlException("'{0}' is a duplicate attribute name.", this.attributes[i].name.ToString());
					}
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000072C0 File Offset: 0x000054C0
		private void HashCheckForDuplicateAttributes()
		{
			int i;
			checked
			{
				for (i = 256; i < this.attrCount; i *= 2)
				{
				}
				if (this.attrHashTbl.Length < i)
				{
					this.attrHashTbl = new int[i];
				}
			}
			for (int j = 0; j < this.attrCount; j++)
			{
				string text;
				string text2;
				int localnameAndNamespaceUriAndHash = this.attributes[j].GetLocalnameAndNamespaceUriAndHash(this.hasher, out text, out text2);
				int num = localnameAndNamespaceUriAndHash & (i - 1);
				int num2 = this.attrHashTbl[num];
				this.attrHashTbl[num] = j + 1;
				this.attributes[j].prevHash = num2;
				while (num2 != 0)
				{
					num2--;
					if (this.attributes[num2].MatchHashNS(localnameAndNamespaceUriAndHash, text, text2))
					{
						throw new XmlException("'{0}' is a duplicate attribute name.", this.attributes[j].name.ToString());
					}
					num2 = this.attributes[num2].prevHash;
				}
			}
			Array.Clear(this.attrHashTbl, 0, i);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000073CC File Offset: 0x000055CC
		private string XmlDeclValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.attrCount; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(this.attributes[i].name.localname);
				stringBuilder.Append("=\"");
				stringBuilder.Append(this.attributes[i].val);
				stringBuilder.Append('"');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007450 File Offset: 0x00005650
		private string CDATAValue()
		{
			string text = this.GetString(this.tokDataPos, this.tokLen);
			StringBuilder stringBuilder = null;
			while (this.PeekToken() == BinXmlToken.CData)
			{
				this.pos++;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(text.Length + text.Length / 2);
					stringBuilder.Append(text);
				}
				stringBuilder.Append(this.ParseText());
			}
			if (stringBuilder != null)
			{
				text = stringBuilder.ToString();
			}
			this.stringValue = text;
			return text;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000074D0 File Offset: 0x000056D0
		private void FinishCDATA()
		{
			for (;;)
			{
				BinXmlToken binXmlToken = this.PeekToken();
				if (binXmlToken == BinXmlToken.EndCData)
				{
					break;
				}
				if (binXmlToken != BinXmlToken.CData)
				{
					goto IL_003F;
				}
				this.pos++;
				int num;
				this.ScanText(out num);
			}
			this.pos++;
			return;
			IL_003F:
			throw new XmlException("CDATA end token is missing.");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007528 File Offset: 0x00005728
		private void FinishEndElement()
		{
			XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.elementStack[this.elemDepth].Clear();
			this.PopNamespaces(namespaceDecl);
			this.elemDepth--;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007564 File Offset: 0x00005764
		private bool ReadDoc()
		{
			XmlNodeType xmlNodeType = this.nodetype;
			if (xmlNodeType != XmlNodeType.Element)
			{
				if (xmlNodeType != XmlNodeType.CDATA)
				{
					if (xmlNodeType == XmlNodeType.EndElement)
					{
						this.FinishEndElement();
					}
				}
				else
				{
					this.FinishCDATA();
				}
			}
			else if (this.isEmpty)
			{
				this.FinishEndElement();
				this.isEmpty = false;
			}
			for (;;)
			{
				this.nodetype = XmlNodeType.None;
				this.mark = -1;
				if (this.qnameOther.localname.Length != 0)
				{
					this.qnameOther.Clear();
				}
				this.ClearAttributes();
				this.attrCount = 0;
				this.valueType = XmlSqlBinaryReader.TypeOfString;
				this.stringValue = null;
				this.hasTypedValue = false;
				this.token = this.NextToken();
				BinXmlToken binXmlToken = this.token;
				if (binXmlToken <= BinXmlToken.XSD_KATMAI_DATE)
				{
					switch (binXmlToken)
					{
					case BinXmlToken.EOF:
						goto IL_0191;
					case BinXmlToken.Error:
					case (BinXmlToken)21:
					case (BinXmlToken)25:
					case (BinXmlToken)26:
						goto IL_027C;
					case BinXmlToken.SQL_SMALLINT:
					case BinXmlToken.SQL_INT:
					case BinXmlToken.SQL_REAL:
					case BinXmlToken.SQL_FLOAT:
					case BinXmlToken.SQL_MONEY:
					case BinXmlToken.SQL_BIT:
					case BinXmlToken.SQL_TINYINT:
					case BinXmlToken.SQL_BIGINT:
					case BinXmlToken.SQL_UUID:
					case BinXmlToken.SQL_DECIMAL:
					case BinXmlToken.SQL_NUMERIC:
					case BinXmlToken.SQL_BINARY:
					case BinXmlToken.SQL_CHAR:
					case BinXmlToken.SQL_NCHAR:
					case BinXmlToken.SQL_VARBINARY:
					case BinXmlToken.SQL_VARCHAR:
					case BinXmlToken.SQL_NVARCHAR:
					case BinXmlToken.SQL_DATETIME:
					case BinXmlToken.SQL_SMALLDATETIME:
					case BinXmlToken.SQL_SMALLMONEY:
					case BinXmlToken.SQL_TEXT:
					case BinXmlToken.SQL_IMAGE:
					case BinXmlToken.SQL_NTEXT:
					case BinXmlToken.SQL_UDT:
						break;
					default:
						if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5)
						{
							goto Block_8;
						}
						break;
					}
				}
				else if (binXmlToken - BinXmlToken.XSD_TIME > 11)
				{
					switch (binXmlToken)
					{
					case BinXmlToken.EndNest:
						goto IL_022D;
					case BinXmlToken.Nest:
						goto IL_0218;
					case BinXmlToken.XmlText:
						goto IL_0242;
					case (BinXmlToken)238:
					case BinXmlToken.QName:
					case BinXmlToken.Name:
					case BinXmlToken.EndCData:
					case BinXmlToken.EndAttrs:
					case BinXmlToken.Attr:
						goto IL_027C;
					case BinXmlToken.CData:
						goto IL_0210;
					case BinXmlToken.Comment:
						this.ImplReadComment();
						if (this.ignoreComments)
						{
							continue;
						}
						return true;
					case BinXmlToken.PI:
						this.ImplReadPI();
						if (this.ignorePIs)
						{
							continue;
						}
						return true;
					case BinXmlToken.EndElem:
						goto IL_01BA;
					case BinXmlToken.Element:
						goto IL_01AF;
					default:
						if (binXmlToken != BinXmlToken.DocType)
						{
							goto Block_11;
						}
						this.ImplReadDoctype();
						if (this.dtdProcessing == DtdProcessing.Ignore)
						{
							continue;
						}
						if (this.prevNameInfo != null)
						{
							continue;
						}
						return true;
					}
				}
				this.ImplReadData(this.token);
				if (XmlNodeType.Text == this.nodetype)
				{
					goto Block_18;
				}
				if (!this.ignoreWhitespace || this.xmlspacePreserve)
				{
					return true;
				}
			}
			Block_8:
			Block_11:
			goto IL_027C;
			IL_0191:
			if (this.elemDepth > 0)
			{
				throw new XmlException("Unexpected end of file has occurred.", null);
			}
			this.state = XmlSqlBinaryReader.ScanState.EOF;
			return false;
			IL_01AF:
			this.ImplReadElement();
			return true;
			IL_01BA:
			this.ImplReadEndElement();
			return true;
			IL_0210:
			this.ImplReadCDATA();
			return true;
			IL_0218:
			this.ImplReadNest();
			this.sniffed = false;
			return this.ReadInit(true);
			IL_022D:
			if (this.prevNameInfo != null)
			{
				this.ImplReadEndNest();
				return this.ReadDoc();
			}
			goto IL_027C;
			IL_0242:
			this.ImplReadXmlText();
			return true;
			Block_18:
			this.CheckAllowContent();
			return true;
			IL_027C:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000077FC File Offset: 0x000059FC
		private void ImplReadData(BinXmlToken tokenType)
		{
			this.mark = this.pos;
			if (tokenType <= BinXmlToken.SQL_NVARCHAR)
			{
				if (tokenType - BinXmlToken.SQL_CHAR > 1 && tokenType - BinXmlToken.SQL_VARCHAR > 1)
				{
					goto IL_003F;
				}
			}
			else if (tokenType != BinXmlToken.SQL_TEXT && tokenType != BinXmlToken.SQL_NTEXT)
			{
				goto IL_003F;
			}
			this.valueType = XmlSqlBinaryReader.TypeOfString;
			this.hasTypedValue = false;
			goto IL_0058;
			IL_003F:
			this.valueType = this.GetValueType(this.token);
			this.hasTypedValue = true;
			IL_0058:
			this.nodetype = this.ScanOverValue(this.token, false, true);
			BinXmlToken binXmlToken = this.PeekNextToken();
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
			case BinXmlToken.SQL_INT:
			case BinXmlToken.SQL_REAL:
			case BinXmlToken.SQL_FLOAT:
			case BinXmlToken.SQL_MONEY:
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
			case BinXmlToken.SQL_BIGINT:
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
			case BinXmlToken.SQL_SMALLMONEY:
			case BinXmlToken.SQL_TEXT:
			case BinXmlToken.SQL_IMAGE:
			case BinXmlToken.SQL_NTEXT:
			case BinXmlToken.SQL_UDT:
				break;
			case (BinXmlToken)21:
			case (BinXmlToken)25:
			case (BinXmlToken)26:
				return;
			default:
				if (binXmlToken - BinXmlToken.XSD_KATMAI_TIMEOFFSET > 5 && binXmlToken - BinXmlToken.XSD_TIME > 11)
				{
					return;
				}
				break;
			}
			throw this.ThrowNotSupported("Lists of BinaryXml value tokens not supported.");
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007910 File Offset: 0x00005B10
		private void ImplReadElement()
		{
			if (3 != this.docState || 9 != this.docState)
			{
				switch (this.docState)
				{
				case -1:
					throw this.ThrowUnexpectedToken(this.token);
				case 0:
					this.docState = 9;
					break;
				case 1:
				case 2:
					this.docState = 3;
					break;
				}
			}
			this.elemDepth++;
			if (this.elemDepth == this.elementStack.Length)
			{
				this.GrowElements();
			}
			XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[this.ReadQNameRef()];
			this.qnameOther = (this.qnameElement = qname);
			this.elementStack[this.elemDepth].Set(qname, this.xmlspacePreserve);
			this.PushNamespace(qname.prefix, qname.namespaceUri, true);
			BinXmlToken binXmlToken = this.PeekNextToken();
			if (BinXmlToken.Attr == binXmlToken)
			{
				this.ScanAttributes();
				binXmlToken = this.PeekNextToken();
			}
			this.GenerateImpliedXmlnsAttrs();
			if (BinXmlToken.EndElem == binXmlToken)
			{
				this.NextToken();
				this.isEmpty = true;
			}
			else if (BinXmlToken.SQL_NVARCHAR == binXmlToken)
			{
				if (this.mark < 0)
				{
					this.mark = this.pos;
				}
				this.pos++;
				if (this.ReadByte() == 0)
				{
					if (247 != this.ReadByte())
					{
						this.pos -= 3;
					}
					else
					{
						this.pos--;
					}
				}
				else
				{
					this.pos -= 2;
				}
			}
			this.nodetype = XmlNodeType.Element;
			this.valueType = XmlSqlBinaryReader.TypeOfObject;
			this.posAfterAttrs = this.pos;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007AB0 File Offset: 0x00005CB0
		private void ImplReadEndElement()
		{
			if (this.elemDepth == 0)
			{
				throw this.ThrowXmlException("Unexpected end tag.");
			}
			int num = this.elemDepth;
			if (1 == num && 3 == this.docState)
			{
				this.docState = -1;
			}
			this.qnameOther = this.elementStack[num].name;
			this.xmlspacePreserve = this.elementStack[num].xmlspacePreserve;
			this.nodetype = XmlNodeType.EndElement;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007B24 File Offset: 0x00005D24
		private void ImplReadDoctype()
		{
			if (this.dtdProcessing == DtdProcessing.Prohibit)
			{
				throw this.ThrowXmlException("DTD is prohibited in this XML document.");
			}
			int num = this.docState;
			if (num <= 1)
			{
				this.docState = 2;
				this.qnameOther.localname = this.ParseText();
				if (BinXmlToken.System == this.PeekToken())
				{
					this.pos++;
					XmlSqlBinaryReader.AttrInfo[] array = this.attributes;
					num = this.attrCount;
					this.attrCount = num + 1;
					array[num].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("SYSTEM"), string.Empty), this.ParseText());
				}
				if (BinXmlToken.Public == this.PeekToken())
				{
					this.pos++;
					XmlSqlBinaryReader.AttrInfo[] array2 = this.attributes;
					num = this.attrCount;
					this.attrCount = num + 1;
					array2[num].Set(new XmlSqlBinaryReader.QName(string.Empty, this.xnt.Add("PUBLIC"), string.Empty), this.ParseText());
				}
				if (BinXmlToken.Subset == this.PeekToken())
				{
					this.pos++;
					this.mark = this.pos;
					this.tokLen = this.ScanText(out this.tokDataPos);
				}
				else
				{
					this.tokLen = (this.tokDataPos = 0);
				}
				this.nodetype = XmlNodeType.DocumentType;
				this.posAfterAttrs = this.pos;
				return;
			}
			if (num == 9)
			{
				throw this.ThrowXmlException("DTD is not allowed in XML fragments.");
			}
			throw this.ThrowXmlException("Unexpected DTD declaration.");
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007CA8 File Offset: 0x00005EA8
		private void ImplReadPI()
		{
			this.qnameOther.localname = this.symbolTables.symtable[this.ReadNameRef()];
			this.mark = this.pos;
			this.tokLen = this.ScanText(out this.tokDataPos);
			this.nodetype = XmlNodeType.ProcessingInstruction;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007CF7 File Offset: 0x00005EF7
		private void ImplReadComment()
		{
			this.nodetype = XmlNodeType.Comment;
			this.mark = this.pos;
			this.tokLen = this.ScanText(out this.tokDataPos);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007D1E File Offset: 0x00005F1E
		private void ImplReadCDATA()
		{
			this.CheckAllowContent();
			this.nodetype = XmlNodeType.CDATA;
			this.mark = this.pos;
			this.tokLen = this.ScanText(out this.tokDataPos);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007D4B File Offset: 0x00005F4B
		private void ImplReadNest()
		{
			this.CheckAllowContent();
			this.prevNameInfo = new XmlSqlBinaryReader.NestedBinXml(this.symbolTables, this.docState, this.prevNameInfo);
			this.symbolTables.Init();
			this.docState = 0;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007D84 File Offset: 0x00005F84
		private void ImplReadEndNest()
		{
			XmlSqlBinaryReader.NestedBinXml nestedBinXml = this.prevNameInfo;
			this.symbolTables = nestedBinXml.symbolTables;
			this.docState = nestedBinXml.docState;
			this.prevNameInfo = nestedBinXml.next;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007DBC File Offset: 0x00005FBC
		private void ImplReadXmlText()
		{
			this.CheckAllowContent();
			string text = this.ParseText();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.xnt);
			foreach (XmlSqlBinaryReader.NamespaceDecl namespaceDecl in this.namespaces.Values)
			{
				if (namespaceDecl.scope > 0)
				{
					xmlNamespaceManager.AddNamespace(namespaceDecl.prefix, namespaceDecl.uri);
				}
			}
			XmlReaderSettings settings = this.Settings;
			settings.ReadOnly = false;
			settings.NameTable = this.xnt;
			settings.DtdProcessing = DtdProcessing.Prohibit;
			if (this.elemDepth != 0)
			{
				settings.ConformanceLevel = ConformanceLevel.Fragment;
			}
			settings.ReadOnly = true;
			XmlParserContext xmlParserContext = new XmlParserContext(this.xnt, xmlNamespaceManager, this.XmlLang, this.XmlSpace);
			this.textXmlReader = new XmlTextReaderImpl(text, xmlParserContext, settings);
			if (!this.textXmlReader.Read() || (this.textXmlReader.NodeType == XmlNodeType.XmlDeclaration && !this.textXmlReader.Read()))
			{
				this.state = XmlSqlBinaryReader.ScanState.Doc;
				this.ReadDoc();
				return;
			}
			this.state = XmlSqlBinaryReader.ScanState.XmlText;
			this.UpdateFromTextReader();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007EEC File Offset: 0x000060EC
		private void UpdateFromTextReader()
		{
			XmlReader xmlReader = this.textXmlReader;
			this.nodetype = xmlReader.NodeType;
			this.qnameOther.prefix = xmlReader.Prefix;
			this.qnameOther.localname = xmlReader.LocalName;
			this.qnameOther.namespaceUri = xmlReader.NamespaceURI;
			this.valueType = xmlReader.ValueType;
			this.isEmpty = xmlReader.IsEmptyElement;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007F57 File Offset: 0x00006157
		private bool UpdateFromTextReader(bool needUpdate)
		{
			if (needUpdate)
			{
				this.UpdateFromTextReader();
			}
			return needUpdate;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007F64 File Offset: 0x00006164
		private void CheckAllowContent()
		{
			int num = this.docState;
			if (num == 0)
			{
				this.docState = 9;
				return;
			}
			if (num != 3 && num != 9)
			{
				throw this.ThrowXmlException("Data at the root level is invalid.");
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007F9C File Offset: 0x0000619C
		private void GenerateTokenTypeMap()
		{
			Type[] array = new Type[256];
			array[134] = typeof(bool);
			array[7] = typeof(byte);
			array[136] = typeof(sbyte);
			array[1] = typeof(short);
			array[137] = typeof(ushort);
			array[138] = typeof(uint);
			array[3] = typeof(float);
			array[4] = typeof(double);
			array[8] = typeof(long);
			array[139] = typeof(ulong);
			array[140] = typeof(XmlQualifiedName);
			Type typeFromHandle = typeof(int);
			array[6] = typeFromHandle;
			array[2] = typeFromHandle;
			Type typeFromHandle2 = typeof(decimal);
			array[20] = typeFromHandle2;
			array[5] = typeFromHandle2;
			array[10] = typeFromHandle2;
			array[11] = typeFromHandle2;
			array[135] = typeFromHandle2;
			Type typeFromHandle3 = typeof(DateTime);
			array[19] = typeFromHandle3;
			array[18] = typeFromHandle3;
			array[129] = typeFromHandle3;
			array[130] = typeFromHandle3;
			array[131] = typeFromHandle3;
			array[127] = typeFromHandle3;
			array[126] = typeFromHandle3;
			array[125] = typeFromHandle3;
			Type typeFromHandle4 = typeof(DateTimeOffset);
			array[124] = typeFromHandle4;
			array[123] = typeFromHandle4;
			array[122] = typeFromHandle4;
			Type typeFromHandle5 = typeof(byte[]);
			array[15] = typeFromHandle5;
			array[12] = typeFromHandle5;
			array[23] = typeFromHandle5;
			array[27] = typeFromHandle5;
			array[132] = typeFromHandle5;
			array[133] = typeFromHandle5;
			array[13] = XmlSqlBinaryReader.TypeOfString;
			array[16] = XmlSqlBinaryReader.TypeOfString;
			array[22] = XmlSqlBinaryReader.TypeOfString;
			array[14] = XmlSqlBinaryReader.TypeOfString;
			array[17] = XmlSqlBinaryReader.TypeOfString;
			array[24] = XmlSqlBinaryReader.TypeOfString;
			array[9] = XmlSqlBinaryReader.TypeOfString;
			if (XmlSqlBinaryReader.TokenTypeMap == null)
			{
				XmlSqlBinaryReader.TokenTypeMap = array;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008174 File Offset: 0x00006374
		private Type GetValueType(BinXmlToken token)
		{
			Type type = XmlSqlBinaryReader.TokenTypeMap[(int)token];
			if (type == null)
			{
				throw this.ThrowUnexpectedToken(token);
			}
			return type;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008190 File Offset: 0x00006390
		private void ReScanOverValue(BinXmlToken token)
		{
			this.ScanOverValue(token, true, false);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000819C File Offset: 0x0000639C
		private XmlNodeType ScanOverValue(BinXmlToken token, bool attr, bool checkChars)
		{
			if (token != BinXmlToken.SQL_NVARCHAR)
			{
				return this.ScanOverAnyValue(token, attr, checkChars);
			}
			if (this.mark < 0)
			{
				this.mark = this.pos;
			}
			this.tokLen = this.ParseMB32();
			this.tokDataPos = this.pos;
			checked
			{
				this.pos += this.tokLen * 2;
				this.Fill(-1);
				if (checkChars && this.checkCharacters)
				{
					return this.CheckText(attr);
				}
				if (!attr)
				{
					return this.CheckTextIsWS();
				}
				return XmlNodeType.Text;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008220 File Offset: 0x00006420
		private XmlNodeType ScanOverAnyValue(BinXmlToken token, bool attr, bool checkChars)
		{
			if (this.mark < 0)
			{
				this.mark = this.pos;
			}
			checked
			{
				switch (token)
				{
				case BinXmlToken.SQL_SMALLINT:
					goto IL_0109;
				case BinXmlToken.SQL_INT:
				case BinXmlToken.SQL_REAL:
				case BinXmlToken.SQL_SMALLDATETIME:
				case BinXmlToken.SQL_SMALLMONEY:
					goto IL_012F;
				case BinXmlToken.SQL_FLOAT:
				case BinXmlToken.SQL_MONEY:
				case BinXmlToken.SQL_BIGINT:
				case BinXmlToken.SQL_DATETIME:
					goto IL_0155;
				case BinXmlToken.SQL_BIT:
				case BinXmlToken.SQL_TINYINT:
					break;
				case BinXmlToken.SQL_UUID:
					this.tokDataPos = this.pos;
					this.tokLen = 16;
					this.pos += 16;
					goto IL_02BA;
				case BinXmlToken.SQL_DECIMAL:
				case BinXmlToken.SQL_NUMERIC:
					goto IL_01A3;
				case BinXmlToken.SQL_BINARY:
				case BinXmlToken.SQL_VARBINARY:
				case BinXmlToken.SQL_IMAGE:
				case BinXmlToken.SQL_UDT:
					goto IL_01D3;
				case BinXmlToken.SQL_CHAR:
				case BinXmlToken.SQL_VARCHAR:
				case BinXmlToken.SQL_TEXT:
					this.tokLen = this.ParseMB64();
					this.tokDataPos = this.pos;
					this.pos += this.tokLen;
					if (checkChars && this.checkCharacters)
					{
						this.Fill(-1);
						string text = this.ValueAsString(token);
						XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException, ExceptionType.XmlException);
						this.stringValue = text;
						goto IL_02BA;
					}
					goto IL_02BA;
				case BinXmlToken.SQL_NCHAR:
				case BinXmlToken.SQL_NVARCHAR:
				case BinXmlToken.SQL_NTEXT:
					return this.ScanOverValue(BinXmlToken.SQL_NVARCHAR, attr, checkChars);
				case (BinXmlToken)21:
				case (BinXmlToken)25:
				case (BinXmlToken)26:
					goto IL_02B2;
				default:
					switch (token)
					{
					case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					case BinXmlToken.XSD_KATMAI_TIME:
					case BinXmlToken.XSD_KATMAI_DATETIME:
					case BinXmlToken.XSD_KATMAI_DATE:
						this.VerifyVersion(2, token);
						this.tokDataPos = this.pos;
						this.tokLen = this.GetXsdKatmaiTokenLength(token);
						this.pos += this.tokLen;
						goto IL_02BA;
					case (BinXmlToken)128:
						goto IL_02B2;
					case BinXmlToken.XSD_TIME:
					case BinXmlToken.XSD_DATETIME:
					case BinXmlToken.XSD_DATE:
					case BinXmlToken.XSD_UNSIGNEDLONG:
						goto IL_0155;
					case BinXmlToken.XSD_BINHEX:
					case BinXmlToken.XSD_BASE64:
						goto IL_01D3;
					case BinXmlToken.XSD_BOOLEAN:
					case BinXmlToken.XSD_BYTE:
						break;
					case BinXmlToken.XSD_DECIMAL:
						goto IL_01A3;
					case BinXmlToken.XSD_UNSIGNEDSHORT:
						goto IL_0109;
					case BinXmlToken.XSD_UNSIGNEDINT:
						goto IL_012F;
					case BinXmlToken.XSD_QNAME:
						this.tokDataPos = this.pos;
						this.ParseMB32();
						goto IL_02BA;
					default:
						goto IL_02B2;
					}
					break;
				}
				this.tokDataPos = this.pos;
				this.tokLen = 1;
				this.pos++;
				goto IL_02BA;
			}
			IL_0109:
			this.tokDataPos = this.pos;
			this.tokLen = 2;
			checked
			{
				this.pos += 2;
				goto IL_02BA;
			}
			IL_012F:
			this.tokDataPos = this.pos;
			this.tokLen = 4;
			checked
			{
				this.pos += 4;
				goto IL_02BA;
			}
			IL_0155:
			this.tokDataPos = this.pos;
			this.tokLen = 8;
			checked
			{
				this.pos += 8;
				goto IL_02BA;
			}
			IL_01A3:
			this.tokDataPos = this.pos;
			this.tokLen = this.ParseMB64();
			checked
			{
				this.pos += this.tokLen;
				goto IL_02BA;
			}
			IL_01D3:
			this.tokLen = this.ParseMB64();
			this.tokDataPos = this.pos;
			checked
			{
				this.pos += this.tokLen;
				goto IL_02BA;
			}
			IL_02B2:
			throw this.ThrowUnexpectedToken(token);
			IL_02BA:
			this.Fill(-1);
			return XmlNodeType.Text;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000084F0 File Offset: 0x000066F0
		private unsafe XmlNodeType CheckText(bool attr)
		{
			XmlCharType xmlCharType = this.xmlCharType;
			byte[] array;
			byte* ptr;
			if ((array = this.data) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int num = this.pos;
			int num2 = this.tokDataPos;
			if (!attr)
			{
				for (;;)
				{
					int num3 = num2 + 2;
					if (num3 > num)
					{
						break;
					}
					if (ptr[num2 + 1] != 0 || (xmlCharType.charProperties[(int)ptr[num2]] & 1) == 0)
					{
						goto IL_006E;
					}
					num2 = num3;
				}
				if (!this.xmlspacePreserve)
				{
					return XmlNodeType.Whitespace;
				}
				return XmlNodeType.SignificantWhitespace;
			}
			char c;
			char c2;
			for (;;)
			{
				IL_006E:
				int num4 = num2 + 2;
				if (num4 > num)
				{
					break;
				}
				c = (char)((int)ptr[num2] | ((int)ptr[num2 + 1] << 8));
				if ((xmlCharType.charProperties[(int)c] & 16) != 0)
				{
					num2 = num4;
				}
				else
				{
					if (!XmlCharType.IsHighSurrogate((int)c))
					{
						goto Block_8;
					}
					if (num2 + 4 > num)
					{
						goto Block_9;
					}
					c2 = (char)((int)ptr[num2 + 2] | ((int)ptr[num2 + 3] << 8));
					if (!XmlCharType.IsLowSurrogate((int)c2))
					{
						goto Block_10;
					}
					num2 += 4;
				}
			}
			return XmlNodeType.Text;
			Block_8:
			throw XmlConvert.CreateInvalidCharException(c, '\0', ExceptionType.XmlException);
			Block_9:
			throw this.ThrowXmlException("The surrogate pair is invalid. Missing a low surrogate character.");
			Block_10:
			throw XmlConvert.CreateInvalidSurrogatePairException(c, c2);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000085F8 File Offset: 0x000067F8
		private XmlNodeType CheckTextIsWS()
		{
			byte[] array = this.data;
			int i = this.tokDataPos;
			while (i < this.pos)
			{
				if (array[i + 1] == 0)
				{
					byte b = array[i];
					if (b - 9 <= 1 || b == 13 || b == 32)
					{
						i += 2;
						continue;
					}
				}
				return XmlNodeType.Text;
			}
			if (this.xmlspacePreserve)
			{
				return XmlNodeType.SignificantWhitespace;
			}
			return XmlNodeType.Whitespace;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000864D File Offset: 0x0000684D
		private void CheckValueTokenBounds()
		{
			if (this.end - this.tokDataPos < this.tokLen)
			{
				throw this.ThrowXmlException("Unexpected end of file has occurred.");
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008670 File Offset: 0x00006870
		private int GetXsdKatmaiTokenLength(BinXmlToken token)
		{
			switch (token)
			{
			case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
			case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
			case BinXmlToken.XSD_KATMAI_DATEOFFSET:
			{
				this.Fill(0);
				byte b = this.data[this.pos];
				return 6 + this.XsdKatmaiTimeScaleToValueLength(b);
			}
			case BinXmlToken.XSD_KATMAI_TIME:
			case BinXmlToken.XSD_KATMAI_DATETIME:
			{
				this.Fill(0);
				byte b = this.data[this.pos];
				return 4 + this.XsdKatmaiTimeScaleToValueLength(b);
			}
			case BinXmlToken.XSD_KATMAI_DATE:
				return 3;
			default:
				throw this.ThrowUnexpectedToken(this.token);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000086EC File Offset: 0x000068EC
		private int XsdKatmaiTimeScaleToValueLength(byte scale)
		{
			if (scale > 7)
			{
				throw new XmlException("Arithmetic Overflow.", null);
			}
			return (int)XmlSqlBinaryReader.XsdKatmaiTimeScaleToValueLengthMap[(int)scale];
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008708 File Offset: 0x00006908
		private long ValueAsLong()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
				return (long)this.GetInt16(this.tokDataPos);
			case BinXmlToken.SQL_INT:
				return (long)this.GetInt32(this.tokDataPos);
			case BinXmlToken.SQL_REAL:
			case BinXmlToken.SQL_FLOAT:
				return (long)this.ValueAsDouble();
			case BinXmlToken.SQL_MONEY:
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
			case BinXmlToken.SQL_SMALLMONEY:
				break;
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
				return (long)((ulong)this.data[this.tokDataPos]);
			case BinXmlToken.SQL_BIGINT:
				return this.GetInt64(this.tokDataPos);
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_0110;
			default:
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_DECIMAL:
					break;
				case BinXmlToken.XSD_BYTE:
					return (long)((sbyte)this.data[this.tokDataPos]);
				case BinXmlToken.XSD_UNSIGNEDSHORT:
					return (long)((ulong)this.GetUInt16(this.tokDataPos));
				case BinXmlToken.XSD_UNSIGNEDINT:
					return (long)((ulong)this.GetUInt32(this.tokDataPos));
				case BinXmlToken.XSD_UNSIGNEDLONG:
					return checked((long)this.GetUInt64(this.tokDataPos));
				default:
					goto IL_0110;
				}
				break;
			}
			return (long)this.ValueAsDecimal();
			IL_0110:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008831 File Offset: 0x00006A31
		private ulong ValueAsULong()
		{
			if (BinXmlToken.XSD_UNSIGNEDLONG == this.token)
			{
				this.CheckValueTokenBounds();
				return this.GetUInt64(this.tokDataPos);
			}
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008860 File Offset: 0x00006A60
		private decimal ValueAsDecimal()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
			case BinXmlToken.SQL_INT:
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
			case BinXmlToken.SQL_BIGINT:
				break;
			case BinXmlToken.SQL_REAL:
				return new decimal(this.GetSingle(this.tokDataPos));
			case BinXmlToken.SQL_FLOAT:
				return new decimal(this.GetDouble(this.tokDataPos));
			case BinXmlToken.SQL_MONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney = new BinXmlSqlMoney(this.GetInt64(this.tokDataPos));
				return binXmlSqlMoney.ToDecimal();
			}
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_0124;
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
				goto IL_00FC;
			case BinXmlToken.SQL_SMALLMONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney2 = new BinXmlSqlMoney(this.GetInt32(this.tokDataPos));
				return binXmlSqlMoney2.ToDecimal();
			}
			default:
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_DECIMAL:
					goto IL_00FC;
				case BinXmlToken.XSD_BYTE:
				case BinXmlToken.XSD_UNSIGNEDSHORT:
				case BinXmlToken.XSD_UNSIGNEDINT:
					break;
				case BinXmlToken.XSD_UNSIGNEDLONG:
					return new decimal(this.ValueAsULong());
				default:
					goto IL_0124;
				}
				break;
			}
			return new decimal(this.ValueAsLong());
			IL_00FC:
			BinXmlSqlDecimal binXmlSqlDecimal = new BinXmlSqlDecimal(this.data, this.tokDataPos, this.token == BinXmlToken.XSD_DECIMAL);
			return binXmlSqlDecimal.ToDecimal();
			IL_0124:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000089A0 File Offset: 0x00006BA0
		private double ValueAsDouble()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			switch (binXmlToken)
			{
			case BinXmlToken.SQL_SMALLINT:
			case BinXmlToken.SQL_INT:
			case BinXmlToken.SQL_BIT:
			case BinXmlToken.SQL_TINYINT:
			case BinXmlToken.SQL_BIGINT:
				break;
			case BinXmlToken.SQL_REAL:
				return (double)this.GetSingle(this.tokDataPos);
			case BinXmlToken.SQL_FLOAT:
				return this.GetDouble(this.tokDataPos);
			case BinXmlToken.SQL_MONEY:
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
			case BinXmlToken.SQL_SMALLMONEY:
				goto IL_00B3;
			case BinXmlToken.SQL_UUID:
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_00C0;
			default:
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_DECIMAL:
					goto IL_00B3;
				case BinXmlToken.XSD_BYTE:
				case BinXmlToken.XSD_UNSIGNEDSHORT:
				case BinXmlToken.XSD_UNSIGNEDINT:
					break;
				case BinXmlToken.XSD_UNSIGNEDLONG:
					return this.ValueAsULong();
				default:
					goto IL_00C0;
				}
				break;
			}
			return (double)this.ValueAsLong();
			IL_00B3:
			return (double)this.ValueAsDecimal();
			IL_00C0:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008A7C File Offset: 0x00006C7C
		private DateTime ValueAsDateTime()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			if (binXmlToken == BinXmlToken.SQL_DATETIME)
			{
				int num = this.tokDataPos;
				int @int = this.GetInt32(num);
				uint @uint = this.GetUInt32(num + 4);
				return BinXmlDateTime.SqlDateTimeToDateTime(@int, @uint);
			}
			if (binXmlToken != BinXmlToken.SQL_SMALLDATETIME)
			{
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateOffsetToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_TIME:
					return BinXmlDateTime.XsdKatmaiTimeToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIME:
					return BinXmlDateTime.XsdKatmaiDateTimeToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATE:
					return BinXmlDateTime.XsdKatmaiDateToDateTime(this.data, this.tokDataPos);
				case BinXmlToken.XSD_TIME:
					return BinXmlDateTime.XsdTimeToDateTime(this.GetInt64(this.tokDataPos));
				case BinXmlToken.XSD_DATETIME:
					return BinXmlDateTime.XsdDateTimeToDateTime(this.GetInt64(this.tokDataPos));
				case BinXmlToken.XSD_DATE:
					return BinXmlDateTime.XsdDateToDateTime(this.GetInt64(this.tokDataPos));
				}
				throw this.ThrowUnexpectedToken(this.token);
			}
			int num2 = this.tokDataPos;
			short int2 = this.GetInt16(num2);
			ushort uint2 = this.GetUInt16(num2 + 2);
			return BinXmlDateTime.SqlSmallDateTimeToDateTime(int2, uint2);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008BC4 File Offset: 0x00006DC4
		private DateTimeOffset ValueAsDateTimeOffset()
		{
			this.CheckValueTokenBounds();
			switch (this.token)
			{
			case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
				return BinXmlDateTime.XsdKatmaiTimeOffsetToDateTimeOffset(this.data, this.tokDataPos);
			case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
				return BinXmlDateTime.XsdKatmaiDateTimeOffsetToDateTimeOffset(this.data, this.tokDataPos);
			case BinXmlToken.XSD_KATMAI_DATEOFFSET:
				return BinXmlDateTime.XsdKatmaiDateOffsetToDateTimeOffset(this.data, this.tokDataPos);
			default:
				throw this.ThrowUnexpectedToken(this.token);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008C38 File Offset: 0x00006E38
		private string ValueAsDateTimeString()
		{
			this.CheckValueTokenBounds();
			BinXmlToken binXmlToken = this.token;
			if (binXmlToken == BinXmlToken.SQL_DATETIME)
			{
				int num = this.tokDataPos;
				int @int = this.GetInt32(num);
				uint @uint = this.GetUInt32(num + 4);
				return BinXmlDateTime.SqlDateTimeToString(@int, @uint);
			}
			if (binXmlToken != BinXmlToken.SQL_SMALLDATETIME)
			{
				switch (binXmlToken)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiTimeOffsetToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateTimeOffsetToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return BinXmlDateTime.XsdKatmaiDateOffsetToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_TIME:
					return BinXmlDateTime.XsdKatmaiTimeToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATETIME:
					return BinXmlDateTime.XsdKatmaiDateTimeToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_KATMAI_DATE:
					return BinXmlDateTime.XsdKatmaiDateToString(this.data, this.tokDataPos);
				case BinXmlToken.XSD_TIME:
					return BinXmlDateTime.XsdTimeToString(this.GetInt64(this.tokDataPos));
				case BinXmlToken.XSD_DATETIME:
					return BinXmlDateTime.XsdDateTimeToString(this.GetInt64(this.tokDataPos));
				case BinXmlToken.XSD_DATE:
					return BinXmlDateTime.XsdDateToString(this.GetInt64(this.tokDataPos));
				}
				throw this.ThrowUnexpectedToken(this.token);
			}
			int num2 = this.tokDataPos;
			short int2 = this.GetInt16(num2);
			ushort uint2 = this.GetUInt16(num2 + 2);
			return BinXmlDateTime.SqlSmallDateTimeToString(int2, uint2);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008D80 File Offset: 0x00006F80
		private string ValueAsString(BinXmlToken token)
		{
			try
			{
				this.CheckValueTokenBounds();
				switch (token)
				{
				case BinXmlToken.SQL_SMALLINT:
				case BinXmlToken.SQL_INT:
				case BinXmlToken.SQL_BIT:
				case BinXmlToken.SQL_TINYINT:
				case BinXmlToken.SQL_BIGINT:
					break;
				case BinXmlToken.SQL_REAL:
					return XmlConvert.ToString(this.GetSingle(this.tokDataPos));
				case BinXmlToken.SQL_FLOAT:
					return XmlConvert.ToString(this.GetDouble(this.tokDataPos));
				case BinXmlToken.SQL_MONEY:
				{
					BinXmlSqlMoney binXmlSqlMoney = new BinXmlSqlMoney(this.GetInt64(this.tokDataPos));
					return binXmlSqlMoney.ToString();
				}
				case BinXmlToken.SQL_UUID:
				{
					int num = this.tokDataPos;
					int @int = this.GetInt32(num);
					short int2 = this.GetInt16(num + 4);
					short int3 = this.GetInt16(num + 6);
					Guid guid = new Guid(@int, int2, int3, this.data[num + 8], this.data[num + 9], this.data[num + 10], this.data[num + 11], this.data[num + 12], this.data[num + 13], this.data[num + 14], this.data[num + 15]);
					return guid.ToString();
				}
				case BinXmlToken.SQL_DECIMAL:
				case BinXmlToken.SQL_NUMERIC:
					goto IL_0264;
				case BinXmlToken.SQL_BINARY:
				case BinXmlToken.SQL_VARBINARY:
				case BinXmlToken.SQL_IMAGE:
				case BinXmlToken.SQL_UDT:
					goto IL_02C4;
				case BinXmlToken.SQL_CHAR:
				case BinXmlToken.SQL_VARCHAR:
				case BinXmlToken.SQL_TEXT:
				{
					int num2 = this.tokDataPos;
					return Encoding.GetEncoding(this.GetInt32(num2)).GetString(this.data, num2 + 4, this.tokLen - 4);
				}
				case BinXmlToken.SQL_NCHAR:
				case BinXmlToken.SQL_NVARCHAR:
				case BinXmlToken.SQL_NTEXT:
					return this.GetString(this.tokDataPos, this.tokLen);
				case BinXmlToken.SQL_DATETIME:
				case BinXmlToken.SQL_SMALLDATETIME:
					goto IL_02FE;
				case BinXmlToken.SQL_SMALLMONEY:
				{
					BinXmlSqlMoney binXmlSqlMoney2 = new BinXmlSqlMoney(this.GetInt32(this.tokDataPos));
					return binXmlSqlMoney2.ToString();
				}
				case (BinXmlToken)21:
				case (BinXmlToken)25:
				case (BinXmlToken)26:
					goto IL_0383;
				default:
					switch (token)
					{
					case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
					case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					case BinXmlToken.XSD_KATMAI_TIME:
					case BinXmlToken.XSD_KATMAI_DATETIME:
					case BinXmlToken.XSD_KATMAI_DATE:
					case BinXmlToken.XSD_TIME:
					case BinXmlToken.XSD_DATETIME:
					case BinXmlToken.XSD_DATE:
						goto IL_02FE;
					case (BinXmlToken)128:
						goto IL_0383;
					case BinXmlToken.XSD_BINHEX:
						return BinHexEncoder.Encode(this.data, this.tokDataPos, this.tokLen);
					case BinXmlToken.XSD_BASE64:
						goto IL_02C4;
					case BinXmlToken.XSD_BOOLEAN:
						if (this.data[this.tokDataPos] == 0)
						{
							return "false";
						}
						return "true";
					case BinXmlToken.XSD_DECIMAL:
						goto IL_0264;
					case BinXmlToken.XSD_BYTE:
					case BinXmlToken.XSD_UNSIGNEDSHORT:
					case BinXmlToken.XSD_UNSIGNEDINT:
						break;
					case BinXmlToken.XSD_UNSIGNEDLONG:
						return this.ValueAsULong().ToString(CultureInfo.InvariantCulture);
					case BinXmlToken.XSD_QNAME:
					{
						int num3 = this.ParseMB32(this.tokDataPos);
						if (num3 < 0 || num3 >= this.symbolTables.qnameCount)
						{
							throw new XmlException("Invalid QName ID.", string.Empty);
						}
						XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[num3];
						if (qname.prefix.Length == 0)
						{
							return qname.localname;
						}
						return qname.prefix + ":" + qname.localname;
					}
					default:
						goto IL_0383;
					}
					break;
				}
				return this.ValueAsLong().ToString(CultureInfo.InvariantCulture);
				IL_0264:
				BinXmlSqlDecimal binXmlSqlDecimal = new BinXmlSqlDecimal(this.data, this.tokDataPos, token == BinXmlToken.XSD_DECIMAL);
				return binXmlSqlDecimal.ToString();
				IL_02C4:
				return Convert.ToBase64String(this.data, this.tokDataPos, this.tokLen);
				IL_02FE:
				return this.ValueAsDateTimeString();
				IL_0383:
				throw this.ThrowUnexpectedToken(this.token);
			}
			catch
			{
				this.state = XmlSqlBinaryReader.ScanState.Error;
				throw;
			}
			string text;
			return text;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00009144 File Offset: 0x00007344
		private object ValueAsObject(BinXmlToken token, bool returnInternalTypes)
		{
			this.CheckValueTokenBounds();
			switch (token)
			{
			case BinXmlToken.SQL_SMALLINT:
				return this.GetInt16(this.tokDataPos);
			case BinXmlToken.SQL_INT:
				return this.GetInt32(this.tokDataPos);
			case BinXmlToken.SQL_REAL:
				return this.GetSingle(this.tokDataPos);
			case BinXmlToken.SQL_FLOAT:
				return this.GetDouble(this.tokDataPos);
			case BinXmlToken.SQL_MONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney = new BinXmlSqlMoney(this.GetInt64(this.tokDataPos));
				if (returnInternalTypes)
				{
					return binXmlSqlMoney;
				}
				return binXmlSqlMoney.ToDecimal();
			}
			case BinXmlToken.SQL_BIT:
				return (int)this.data[this.tokDataPos];
			case BinXmlToken.SQL_TINYINT:
				return this.data[this.tokDataPos];
			case BinXmlToken.SQL_BIGINT:
				return this.GetInt64(this.tokDataPos);
			case BinXmlToken.SQL_UUID:
			{
				int num = this.tokDataPos;
				int @int = this.GetInt32(num);
				short int2 = this.GetInt16(num + 4);
				short int3 = this.GetInt16(num + 6);
				Guid guid = new Guid(@int, int2, int3, this.data[num + 8], this.data[num + 9], this.data[num + 10], this.data[num + 11], this.data[num + 12], this.data[num + 13], this.data[num + 14], this.data[num + 15]);
				return guid.ToString();
			}
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
				break;
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_IMAGE:
			case BinXmlToken.SQL_UDT:
				goto IL_030F;
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_TEXT:
			{
				int num2 = this.tokDataPos;
				return Encoding.GetEncoding(this.GetInt32(num2)).GetString(this.data, num2 + 4, this.tokLen - 4);
			}
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_NTEXT:
				return this.GetString(this.tokDataPos, this.tokLen);
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_0339;
			case BinXmlToken.SQL_SMALLMONEY:
			{
				BinXmlSqlMoney binXmlSqlMoney2 = new BinXmlSqlMoney(this.GetInt32(this.tokDataPos));
				if (returnInternalTypes)
				{
					return binXmlSqlMoney2;
				}
				return binXmlSqlMoney2.ToDecimal();
			}
			case (BinXmlToken)21:
			case (BinXmlToken)25:
			case (BinXmlToken)26:
				goto IL_03AB;
			default:
				switch (token)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return this.ValueAsDateTimeOffset();
				case BinXmlToken.XSD_KATMAI_TIME:
				case BinXmlToken.XSD_KATMAI_DATETIME:
				case BinXmlToken.XSD_KATMAI_DATE:
				case BinXmlToken.XSD_TIME:
				case BinXmlToken.XSD_DATETIME:
				case BinXmlToken.XSD_DATE:
					goto IL_0339;
				case (BinXmlToken)128:
					goto IL_03AB;
				case BinXmlToken.XSD_BINHEX:
				case BinXmlToken.XSD_BASE64:
					goto IL_030F;
				case BinXmlToken.XSD_BOOLEAN:
					return this.data[this.tokDataPos] > 0;
				case BinXmlToken.XSD_DECIMAL:
					break;
				case BinXmlToken.XSD_BYTE:
					return (sbyte)this.data[this.tokDataPos];
				case BinXmlToken.XSD_UNSIGNEDSHORT:
					return this.GetUInt16(this.tokDataPos);
				case BinXmlToken.XSD_UNSIGNEDINT:
					return this.GetUInt32(this.tokDataPos);
				case BinXmlToken.XSD_UNSIGNEDLONG:
					return this.GetUInt64(this.tokDataPos);
				case BinXmlToken.XSD_QNAME:
				{
					int num3 = this.ParseMB32(this.tokDataPos);
					if (num3 < 0 || num3 >= this.symbolTables.qnameCount)
					{
						throw new XmlException("Invalid QName ID.", string.Empty);
					}
					XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[num3];
					return new XmlQualifiedName(qname.localname, qname.namespaceUri);
				}
				default:
					goto IL_03AB;
				}
				break;
			}
			BinXmlSqlDecimal binXmlSqlDecimal = new BinXmlSqlDecimal(this.data, this.tokDataPos, token == BinXmlToken.XSD_DECIMAL);
			if (returnInternalTypes)
			{
				return binXmlSqlDecimal;
			}
			return binXmlSqlDecimal.ToDecimal();
			IL_030F:
			byte[] array = new byte[this.tokLen];
			Array.Copy(this.data, this.tokDataPos, array, 0, this.tokLen);
			return array;
			IL_0339:
			return this.ValueAsDateTime();
			IL_03AB:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009508 File Offset: 0x00007708
		private XmlValueConverter GetValueConverter(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.GetSimpleTypeFromTypeCode(typeCode).ValueConverter;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009518 File Offset: 0x00007718
		private object ValueAs(BinXmlToken token, Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.CheckValueTokenBounds();
			switch (token)
			{
			case BinXmlToken.SQL_SMALLINT:
			{
				int @int = (int)this.GetInt16(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Short).ChangeType(@int, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_INT:
			{
				int int2 = this.GetInt32(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Int).ChangeType(int2, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_REAL:
			{
				float single = this.GetSingle(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Float).ChangeType(single, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_FLOAT:
			{
				double @double = this.GetDouble(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Double).ChangeType(@double, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_MONEY:
				return this.GetValueConverter(XmlTypeCode.Decimal).ChangeType(new BinXmlSqlMoney(this.GetInt64(this.tokDataPos)).ToDecimal(), returnType, namespaceResolver);
			case BinXmlToken.SQL_BIT:
				return this.GetValueConverter(XmlTypeCode.NonNegativeInteger).ChangeType((int)this.data[this.tokDataPos], returnType, namespaceResolver);
			case BinXmlToken.SQL_TINYINT:
				return this.GetValueConverter(XmlTypeCode.UnsignedByte).ChangeType(this.data[this.tokDataPos], returnType, namespaceResolver);
			case BinXmlToken.SQL_BIGINT:
			{
				long int3 = this.GetInt64(this.tokDataPos);
				return this.GetValueConverter(XmlTypeCode.Long).ChangeType(int3, returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_UUID:
				return this.GetValueConverter(XmlTypeCode.String).ChangeType(this.ValueAsString(token), returnType, namespaceResolver);
			case BinXmlToken.SQL_DECIMAL:
			case BinXmlToken.SQL_NUMERIC:
				break;
			case BinXmlToken.SQL_BINARY:
			case BinXmlToken.SQL_VARBINARY:
			case BinXmlToken.SQL_IMAGE:
			case BinXmlToken.SQL_UDT:
				goto IL_03F0;
			case BinXmlToken.SQL_CHAR:
			case BinXmlToken.SQL_VARCHAR:
			case BinXmlToken.SQL_TEXT:
			{
				int num = this.tokDataPos;
				Encoding encoding = Encoding.GetEncoding(this.GetInt32(num));
				return this.GetValueConverter(XmlTypeCode.UntypedAtomic).ChangeType(encoding.GetString(this.data, num + 4, this.tokLen - 4), returnType, namespaceResolver);
			}
			case BinXmlToken.SQL_NCHAR:
			case BinXmlToken.SQL_NVARCHAR:
			case BinXmlToken.SQL_NTEXT:
				return this.GetValueConverter(XmlTypeCode.UntypedAtomic).ChangeType(this.GetString(this.tokDataPos, this.tokLen), returnType, namespaceResolver);
			case BinXmlToken.SQL_DATETIME:
			case BinXmlToken.SQL_SMALLDATETIME:
				goto IL_043A;
			case BinXmlToken.SQL_SMALLMONEY:
				return this.GetValueConverter(XmlTypeCode.Decimal).ChangeType(new BinXmlSqlMoney(this.GetInt32(this.tokDataPos)).ToDecimal(), returnType, namespaceResolver);
			case (BinXmlToken)21:
			case (BinXmlToken)25:
			case (BinXmlToken)26:
				goto IL_0522;
			default:
				switch (token)
				{
				case BinXmlToken.XSD_KATMAI_TIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATETIMEOFFSET:
				case BinXmlToken.XSD_KATMAI_DATEOFFSET:
					return this.GetValueConverter(XmlTypeCode.DateTime).ChangeType(this.ValueAsDateTimeOffset(), returnType, namespaceResolver);
				case BinXmlToken.XSD_KATMAI_TIME:
				case BinXmlToken.XSD_KATMAI_DATETIME:
				case BinXmlToken.XSD_KATMAI_DATE:
				case BinXmlToken.XSD_DATETIME:
					goto IL_043A;
				case (BinXmlToken)128:
					goto IL_0522;
				case BinXmlToken.XSD_TIME:
					return this.GetValueConverter(XmlTypeCode.Time).ChangeType(this.ValueAsDateTime(), returnType, namespaceResolver);
				case BinXmlToken.XSD_DATE:
					return this.GetValueConverter(XmlTypeCode.Date).ChangeType(this.ValueAsDateTime(), returnType, namespaceResolver);
				case BinXmlToken.XSD_BINHEX:
				case BinXmlToken.XSD_BASE64:
					goto IL_03F0;
				case BinXmlToken.XSD_BOOLEAN:
					return this.GetValueConverter(XmlTypeCode.Boolean).ChangeType(this.data[this.tokDataPos] > 0, returnType, namespaceResolver);
				case BinXmlToken.XSD_DECIMAL:
					break;
				case BinXmlToken.XSD_BYTE:
					return this.GetValueConverter(XmlTypeCode.Byte).ChangeType((int)((sbyte)this.data[this.tokDataPos]), returnType, namespaceResolver);
				case BinXmlToken.XSD_UNSIGNEDSHORT:
				{
					int @uint = (int)this.GetUInt16(this.tokDataPos);
					return this.GetValueConverter(XmlTypeCode.UnsignedShort).ChangeType(@uint, returnType, namespaceResolver);
				}
				case BinXmlToken.XSD_UNSIGNEDINT:
				{
					long num2 = (long)((ulong)this.GetUInt32(this.tokDataPos));
					return this.GetValueConverter(XmlTypeCode.UnsignedInt).ChangeType(num2, returnType, namespaceResolver);
				}
				case BinXmlToken.XSD_UNSIGNEDLONG:
				{
					decimal num3 = this.GetUInt64(this.tokDataPos);
					return this.GetValueConverter(XmlTypeCode.UnsignedLong).ChangeType(num3, returnType, namespaceResolver);
				}
				case BinXmlToken.XSD_QNAME:
				{
					int num4 = this.ParseMB32(this.tokDataPos);
					if (num4 < 0 || num4 >= this.symbolTables.qnameCount)
					{
						throw new XmlException("Invalid QName ID.", string.Empty);
					}
					XmlSqlBinaryReader.QName qname = this.symbolTables.qnametable[num4];
					return this.GetValueConverter(XmlTypeCode.QName).ChangeType(new XmlQualifiedName(qname.localname, qname.namespaceUri), returnType, namespaceResolver);
				}
				default:
					goto IL_0522;
				}
				break;
			}
			return this.GetValueConverter(XmlTypeCode.Decimal).ChangeType(new BinXmlSqlDecimal(this.data, this.tokDataPos, token == BinXmlToken.XSD_DECIMAL).ToDecimal(), returnType, namespaceResolver);
			IL_03F0:
			byte[] array = new byte[this.tokLen];
			Array.Copy(this.data, this.tokDataPos, array, 0, this.tokLen);
			return this.GetValueConverter((token == BinXmlToken.XSD_BINHEX) ? XmlTypeCode.HexBinary : XmlTypeCode.Base64Binary).ChangeType(array, returnType, namespaceResolver);
			IL_043A:
			return this.GetValueConverter(XmlTypeCode.DateTime).ChangeType(this.ValueAsDateTime(), returnType, namespaceResolver);
			IL_0522:
			throw this.ThrowUnexpectedToken(this.token);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009A58 File Offset: 0x00007C58
		private short GetInt16(int pos)
		{
			byte[] array = this.data;
			return (short)((int)array[pos] | ((int)array[pos + 1] << 8));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009A78 File Offset: 0x00007C78
		private ushort GetUInt16(int pos)
		{
			byte[] array = this.data;
			return (ushort)((int)array[pos] | ((int)array[pos + 1] << 8));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009A98 File Offset: 0x00007C98
		private int GetInt32(int pos)
		{
			byte[] array = this.data;
			return (int)array[pos] | ((int)array[pos + 1] << 8) | ((int)array[pos + 2] << 16) | ((int)array[pos + 3] << 24);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009ACC File Offset: 0x00007CCC
		private uint GetUInt32(int pos)
		{
			byte[] array = this.data;
			return (uint)((int)array[pos] | ((int)array[pos + 1] << 8) | ((int)array[pos + 2] << 16) | ((int)array[pos + 3] << 24));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009B00 File Offset: 0x00007D00
		private long GetInt64(int pos)
		{
			byte[] array = this.data;
			uint num = (uint)((int)array[pos] | ((int)array[pos + 1] << 8) | ((int)array[pos + 2] << 16) | ((int)array[pos + 3] << 24));
			return (long)(((ulong)((int)array[pos + 4] | ((int)array[pos + 5] << 8) | ((int)array[pos + 6] << 16) | ((int)array[pos + 7] << 24)) << 32) | (ulong)num);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009B58 File Offset: 0x00007D58
		private ulong GetUInt64(int pos)
		{
			byte[] array = this.data;
			uint num = (uint)((int)array[pos] | ((int)array[pos + 1] << 8) | ((int)array[pos + 2] << 16) | ((int)array[pos + 3] << 24));
			return ((ulong)((int)array[pos + 4] | ((int)array[pos + 5] << 8) | ((int)array[pos + 6] << 16) | ((int)array[pos + 7] << 24)) << 32) | (ulong)num;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009BB0 File Offset: 0x00007DB0
		private unsafe float GetSingle(int offset)
		{
			byte[] array = this.data;
			uint num = (uint)((int)array[offset] | ((int)array[offset + 1] << 8) | ((int)array[offset + 2] << 16) | ((int)array[offset + 3] << 24));
			return *(float*)(&num);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00009BE8 File Offset: 0x00007DE8
		private unsafe double GetDouble(int offset)
		{
			uint num = (uint)((int)this.data[offset] | ((int)this.data[offset + 1] << 8) | ((int)this.data[offset + 2] << 16) | ((int)this.data[offset + 3] << 24));
			ulong num2 = ((ulong)((int)this.data[offset + 4] | ((int)this.data[offset + 5] << 8) | ((int)this.data[offset + 6] << 16) | ((int)this.data[offset + 7] << 24)) << 32) | (ulong)num;
			return *(double*)(&num2);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00009C66 File Offset: 0x00007E66
		private Exception ThrowUnexpectedToken(BinXmlToken token)
		{
			return this.ThrowXmlException("Unexpected BinaryXml token.");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009C73 File Offset: 0x00007E73
		private Exception ThrowXmlException(string res)
		{
			this.state = XmlSqlBinaryReader.ScanState.Error;
			return new XmlException(res, null);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009C83 File Offset: 0x00007E83
		private Exception ThrowXmlException(string res, string arg1, string arg2)
		{
			this.state = XmlSqlBinaryReader.ScanState.Error;
			return new XmlException(res, new string[] { arg1, arg2 });
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009CA0 File Offset: 0x00007EA0
		private Exception ThrowNotSupported(string res)
		{
			this.state = XmlSqlBinaryReader.ScanState.Error;
			return new NotSupportedException(Res.GetString(res));
		}

		// Token: 0x0400008F RID: 143
		internal static readonly Type TypeOfObject = typeof(object);

		// Token: 0x04000090 RID: 144
		internal static readonly Type TypeOfString = typeof(string);

		// Token: 0x04000091 RID: 145
		private static volatile Type[] TokenTypeMap = null;

		// Token: 0x04000092 RID: 146
		private static byte[] XsdKatmaiTimeScaleToValueLengthMap = new byte[] { 3, 3, 3, 4, 4, 5, 5, 5 };

		// Token: 0x04000093 RID: 147
		private static ReadState[] ScanState2ReadState = new ReadState[]
		{
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Interactive,
			ReadState.Initial,
			ReadState.Error,
			ReadState.EndOfFile,
			ReadState.Closed
		};

		// Token: 0x04000094 RID: 148
		private Stream inStrm;

		// Token: 0x04000095 RID: 149
		private byte[] data;

		// Token: 0x04000096 RID: 150
		private int pos;

		// Token: 0x04000097 RID: 151
		private int mark;

		// Token: 0x04000098 RID: 152
		private int end;

		// Token: 0x04000099 RID: 153
		private long offset;

		// Token: 0x0400009A RID: 154
		private bool eof;

		// Token: 0x0400009B RID: 155
		private bool sniffed;

		// Token: 0x0400009C RID: 156
		private bool isEmpty;

		// Token: 0x0400009D RID: 157
		private int docState;

		// Token: 0x0400009E RID: 158
		private XmlSqlBinaryReader.SymbolTables symbolTables;

		// Token: 0x0400009F RID: 159
		private XmlNameTable xnt;

		// Token: 0x040000A0 RID: 160
		private bool xntFromSettings;

		// Token: 0x040000A1 RID: 161
		private string xml;

		// Token: 0x040000A2 RID: 162
		private string xmlns;

		// Token: 0x040000A3 RID: 163
		private string nsxmlns;

		// Token: 0x040000A4 RID: 164
		private string baseUri;

		// Token: 0x040000A5 RID: 165
		private XmlSqlBinaryReader.ScanState state;

		// Token: 0x040000A6 RID: 166
		private XmlNodeType nodetype;

		// Token: 0x040000A7 RID: 167
		private BinXmlToken token;

		// Token: 0x040000A8 RID: 168
		private int attrIndex;

		// Token: 0x040000A9 RID: 169
		private XmlSqlBinaryReader.QName qnameOther;

		// Token: 0x040000AA RID: 170
		private XmlSqlBinaryReader.QName qnameElement;

		// Token: 0x040000AB RID: 171
		private XmlNodeType parentNodeType;

		// Token: 0x040000AC RID: 172
		private XmlSqlBinaryReader.ElemInfo[] elementStack;

		// Token: 0x040000AD RID: 173
		private int elemDepth;

		// Token: 0x040000AE RID: 174
		private XmlSqlBinaryReader.AttrInfo[] attributes;

		// Token: 0x040000AF RID: 175
		private int[] attrHashTbl;

		// Token: 0x040000B0 RID: 176
		private int attrCount;

		// Token: 0x040000B1 RID: 177
		private int posAfterAttrs;

		// Token: 0x040000B2 RID: 178
		private bool xmlspacePreserve;

		// Token: 0x040000B3 RID: 179
		private int tokLen;

		// Token: 0x040000B4 RID: 180
		private int tokDataPos;

		// Token: 0x040000B5 RID: 181
		private bool hasTypedValue;

		// Token: 0x040000B6 RID: 182
		private Type valueType;

		// Token: 0x040000B7 RID: 183
		private string stringValue;

		// Token: 0x040000B8 RID: 184
		private Dictionary<string, XmlSqlBinaryReader.NamespaceDecl> namespaces;

		// Token: 0x040000B9 RID: 185
		private XmlSqlBinaryReader.NestedBinXml prevNameInfo;

		// Token: 0x040000BA RID: 186
		private XmlReader textXmlReader;

		// Token: 0x040000BB RID: 187
		private bool closeInput;

		// Token: 0x040000BC RID: 188
		private bool checkCharacters;

		// Token: 0x040000BD RID: 189
		private bool ignoreWhitespace;

		// Token: 0x040000BE RID: 190
		private bool ignorePIs;

		// Token: 0x040000BF RID: 191
		private bool ignoreComments;

		// Token: 0x040000C0 RID: 192
		private DtdProcessing dtdProcessing;

		// Token: 0x040000C1 RID: 193
		private SecureStringHasher hasher;

		// Token: 0x040000C2 RID: 194
		private XmlCharType xmlCharType;

		// Token: 0x040000C3 RID: 195
		private Encoding unicode;

		// Token: 0x040000C4 RID: 196
		private byte version;

		// Token: 0x02000016 RID: 22
		private enum ScanState
		{
			// Token: 0x040000C6 RID: 198
			Doc,
			// Token: 0x040000C7 RID: 199
			XmlText,
			// Token: 0x040000C8 RID: 200
			Attr,
			// Token: 0x040000C9 RID: 201
			AttrVal,
			// Token: 0x040000CA RID: 202
			AttrValPseudoValue,
			// Token: 0x040000CB RID: 203
			Init,
			// Token: 0x040000CC RID: 204
			Error,
			// Token: 0x040000CD RID: 205
			EOF,
			// Token: 0x040000CE RID: 206
			Closed
		}

		// Token: 0x02000017 RID: 23
		internal struct QName
		{
			// Token: 0x060000FE RID: 254 RVA: 0x00009D14 File Offset: 0x00007F14
			public QName(string prefix, string lname, string nsUri)
			{
				this.prefix = prefix;
				this.localname = lname;
				this.namespaceUri = nsUri;
			}

			// Token: 0x060000FF RID: 255 RVA: 0x00009D14 File Offset: 0x00007F14
			public void Set(string prefix, string lname, string nsUri)
			{
				this.prefix = prefix;
				this.localname = lname;
				this.namespaceUri = nsUri;
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00009D2C File Offset: 0x00007F2C
			public void Clear()
			{
				this.prefix = (this.localname = (this.namespaceUri = string.Empty));
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00009D56 File Offset: 0x00007F56
			public bool MatchNs(string lname, string nsUri)
			{
				return lname == this.localname && nsUri == this.namespaceUri;
			}

			// Token: 0x06000102 RID: 258 RVA: 0x00009D74 File Offset: 0x00007F74
			public bool MatchPrefix(string prefix, string lname)
			{
				return lname == this.localname && prefix == this.prefix;
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00009D92 File Offset: 0x00007F92
			public void CheckPrefixNS(string prefix, string namespaceUri)
			{
				if (this.prefix == prefix && this.namespaceUri != namespaceUri)
				{
					throw new XmlException("Prefix '{0}' is already assigned to namespace '{1}' and cannot be reassigned to '{2}' on this tag.", new string[] { prefix, this.namespaceUri, namespaceUri });
				}
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00009DD2 File Offset: 0x00007FD2
			public override int GetHashCode()
			{
				return this.prefix.GetHashCode() ^ this.localname.GetHashCode();
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00009DEB File Offset: 0x00007FEB
			public int GetNSHashCode(SecureStringHasher hasher)
			{
				return hasher.GetHashCode(this.namespaceUri) ^ hasher.GetHashCode(this.localname);
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00009E08 File Offset: 0x00008008
			public override bool Equals(object other)
			{
				if (other is XmlSqlBinaryReader.QName)
				{
					XmlSqlBinaryReader.QName qname = (XmlSqlBinaryReader.QName)other;
					return this == qname;
				}
				return false;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x00009E32 File Offset: 0x00008032
			public override string ToString()
			{
				if (this.prefix.Length == 0)
				{
					return this.localname;
				}
				return this.prefix + ":" + this.localname;
			}

			// Token: 0x06000108 RID: 264 RVA: 0x00009E5E File Offset: 0x0000805E
			public static bool operator ==(XmlSqlBinaryReader.QName a, XmlSqlBinaryReader.QName b)
			{
				return a.prefix == b.prefix && a.localname == b.localname && a.namespaceUri == b.namespaceUri;
			}

			// Token: 0x040000CF RID: 207
			public string prefix;

			// Token: 0x040000D0 RID: 208
			public string localname;

			// Token: 0x040000D1 RID: 209
			public string namespaceUri;
		}

		// Token: 0x02000018 RID: 24
		private struct ElemInfo
		{
			// Token: 0x06000109 RID: 265 RVA: 0x00009E99 File Offset: 0x00008099
			public void Set(XmlSqlBinaryReader.QName name, bool xmlspacePreserve)
			{
				this.name = name;
				this.xmlLang = null;
				this.xmlSpace = XmlSpace.None;
				this.xmlspacePreserve = xmlspacePreserve;
			}

			// Token: 0x0600010A RID: 266 RVA: 0x00009EB7 File Offset: 0x000080B7
			public XmlSqlBinaryReader.NamespaceDecl Clear()
			{
				XmlSqlBinaryReader.NamespaceDecl namespaceDecl = this.nsdecls;
				this.nsdecls = null;
				return namespaceDecl;
			}

			// Token: 0x040000D2 RID: 210
			public XmlSqlBinaryReader.QName name;

			// Token: 0x040000D3 RID: 211
			public string xmlLang;

			// Token: 0x040000D4 RID: 212
			public XmlSpace xmlSpace;

			// Token: 0x040000D5 RID: 213
			public bool xmlspacePreserve;

			// Token: 0x040000D6 RID: 214
			public XmlSqlBinaryReader.NamespaceDecl nsdecls;
		}

		// Token: 0x02000019 RID: 25
		private struct AttrInfo
		{
			// Token: 0x0600010B RID: 267 RVA: 0x00009EC6 File Offset: 0x000080C6
			public void Set(XmlSqlBinaryReader.QName n, string v)
			{
				this.name = n;
				this.val = v;
				this.contentPos = 0;
				this.hashCode = 0;
				this.prevHash = 0;
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00009EEB File Offset: 0x000080EB
			public void Set(XmlSqlBinaryReader.QName n, int pos)
			{
				this.name = n;
				this.val = null;
				this.contentPos = pos;
				this.hashCode = 0;
				this.prevHash = 0;
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00009F10 File Offset: 0x00008110
			public void GetLocalnameAndNamespaceUri(out string localname, out string namespaceUri)
			{
				localname = this.name.localname;
				namespaceUri = this.name.namespaceUri;
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00009F2C File Offset: 0x0000812C
			public int GetLocalnameAndNamespaceUriAndHash(SecureStringHasher hasher, out string localname, out string namespaceUri)
			{
				localname = this.name.localname;
				namespaceUri = this.name.namespaceUri;
				return this.hashCode = this.name.GetNSHashCode(hasher);
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00009F68 File Offset: 0x00008168
			public bool MatchNS(string localname, string namespaceUri)
			{
				return this.name.MatchNs(localname, namespaceUri);
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00009F77 File Offset: 0x00008177
			public bool MatchHashNS(int hash, string localname, string namespaceUri)
			{
				return this.hashCode == hash && this.name.MatchNs(localname, namespaceUri);
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00009F91 File Offset: 0x00008191
			public void AdjustPosition(int adj)
			{
				if (this.contentPos != 0)
				{
					this.contentPos += adj;
				}
			}

			// Token: 0x040000D7 RID: 215
			public XmlSqlBinaryReader.QName name;

			// Token: 0x040000D8 RID: 216
			public string val;

			// Token: 0x040000D9 RID: 217
			public int contentPos;

			// Token: 0x040000DA RID: 218
			public int hashCode;

			// Token: 0x040000DB RID: 219
			public int prevHash;
		}

		// Token: 0x0200001A RID: 26
		private class NamespaceDecl
		{
			// Token: 0x06000112 RID: 274 RVA: 0x00009FA9 File Offset: 0x000081A9
			public NamespaceDecl(string prefix, string nsuri, XmlSqlBinaryReader.NamespaceDecl nextInScope, XmlSqlBinaryReader.NamespaceDecl prevDecl, int scope, bool implied)
			{
				this.prefix = prefix;
				this.uri = nsuri;
				this.scopeLink = nextInScope;
				this.prevLink = prevDecl;
				this.scope = scope;
				this.implied = implied;
			}

			// Token: 0x040000DC RID: 220
			public string prefix;

			// Token: 0x040000DD RID: 221
			public string uri;

			// Token: 0x040000DE RID: 222
			public XmlSqlBinaryReader.NamespaceDecl scopeLink;

			// Token: 0x040000DF RID: 223
			public XmlSqlBinaryReader.NamespaceDecl prevLink;

			// Token: 0x040000E0 RID: 224
			public int scope;

			// Token: 0x040000E1 RID: 225
			public bool implied;
		}

		// Token: 0x0200001B RID: 27
		private struct SymbolTables
		{
			// Token: 0x06000113 RID: 275 RVA: 0x00009FDE File Offset: 0x000081DE
			public void Init()
			{
				this.symtable = new string[64];
				this.qnametable = new XmlSqlBinaryReader.QName[16];
				this.symtable[0] = string.Empty;
				this.symCount = 1;
				this.qnameCount = 1;
			}

			// Token: 0x040000E2 RID: 226
			public string[] symtable;

			// Token: 0x040000E3 RID: 227
			public int symCount;

			// Token: 0x040000E4 RID: 228
			public XmlSqlBinaryReader.QName[] qnametable;

			// Token: 0x040000E5 RID: 229
			public int qnameCount;
		}

		// Token: 0x0200001C RID: 28
		private class NestedBinXml
		{
			// Token: 0x06000114 RID: 276 RVA: 0x0000A015 File Offset: 0x00008215
			public NestedBinXml(XmlSqlBinaryReader.SymbolTables symbolTables, int docState, XmlSqlBinaryReader.NestedBinXml next)
			{
				this.symbolTables = symbolTables;
				this.docState = docState;
				this.next = next;
			}

			// Token: 0x040000E6 RID: 230
			public XmlSqlBinaryReader.SymbolTables symbolTables;

			// Token: 0x040000E7 RID: 231
			public int docState;

			// Token: 0x040000E8 RID: 232
			public XmlSqlBinaryReader.NestedBinXml next;
		}
	}
}
