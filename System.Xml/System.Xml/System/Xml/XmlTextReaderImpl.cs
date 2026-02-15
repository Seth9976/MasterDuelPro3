using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;

namespace System.Xml
{
	// Token: 0x02000070 RID: 112
	internal class XmlTextReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0001965C File Offset: 0x0001785C
		internal XmlTextReaderImpl(XmlNameTable nt)
		{
			this.v1Compat = true;
			this.outerReader = this;
			this.nameTable = nt;
			nt.Add(string.Empty);
			if (!XmlReaderSettings.EnableLegacyXmlSettings())
			{
				this.xmlResolver = null;
			}
			else
			{
				this.xmlResolver = new XmlUrlResolver();
			}
			this.Xml = nt.Add("xml");
			this.XmlNs = nt.Add("xmlns");
			this.nodes = new XmlTextReaderImpl.NodeData[8];
			this.nodes[0] = new XmlTextReaderImpl.NodeData();
			this.curNode = this.nodes[0];
			this.stringBuilder = new StringBuilder();
			this.xmlContext = new XmlTextReaderImpl.XmlContext();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			this.entityHandling = EntityHandling.ExpandCharEntities;
			this.whitespaceHandling = WhitespaceHandling.All;
			this.closeInput = true;
			this.maxCharactersInDocument = 0L;
			this.maxCharactersFromEntities = 10000000L;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.ps.lineNo = 1;
			this.ps.lineStartPos = -1;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000197CC File Offset: 0x000179CC
		private XmlTextReaderImpl(XmlResolver resolver, XmlReaderSettings settings, XmlParserContext context)
		{
			this.useAsync = settings.Async;
			this.v1Compat = false;
			this.outerReader = this;
			this.xmlContext = new XmlTextReaderImpl.XmlContext();
			XmlNameTable xmlNameTable = settings.NameTable;
			if (context == null)
			{
				if (xmlNameTable == null)
				{
					xmlNameTable = new NameTable();
				}
				else
				{
					this.nameTableFromSettings = true;
				}
				this.nameTable = xmlNameTable;
				this.namespaceManager = new XmlNamespaceManager(xmlNameTable);
			}
			else
			{
				this.SetupFromParserContext(context, settings);
				xmlNameTable = this.nameTable;
			}
			xmlNameTable.Add(string.Empty);
			this.Xml = xmlNameTable.Add("xml");
			this.XmlNs = xmlNameTable.Add("xmlns");
			this.xmlResolver = resolver;
			this.nodes = new XmlTextReaderImpl.NodeData[8];
			this.nodes[0] = new XmlTextReaderImpl.NodeData();
			this.curNode = this.nodes[0];
			this.stringBuilder = new StringBuilder();
			this.entityHandling = EntityHandling.ExpandEntities;
			this.xmlResolverIsSet = settings.IsXmlResolverSet;
			this.whitespaceHandling = (settings.IgnoreWhitespace ? WhitespaceHandling.Significant : WhitespaceHandling.All);
			this.normalize = true;
			this.ignorePIs = settings.IgnoreProcessingInstructions;
			this.ignoreComments = settings.IgnoreComments;
			this.checkCharacters = settings.CheckCharacters;
			this.lineNumberOffset = settings.LineNumberOffset;
			this.linePositionOffset = settings.LinePositionOffset;
			this.ps.lineNo = this.lineNumberOffset + 1;
			this.ps.lineStartPos = -this.linePositionOffset - 1;
			this.curNode.SetLineInfo(this.ps.LineNo - 1, this.ps.LinePos - 1);
			this.dtdProcessing = settings.DtdProcessing;
			this.maxCharactersInDocument = settings.MaxCharactersInDocument;
			this.maxCharactersFromEntities = settings.MaxCharactersFromEntities;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.fragmentParserContext = context;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			switch (settings.ConformanceLevel)
			{
			case ConformanceLevel.Auto:
				this.fragmentType = XmlNodeType.None;
				this.fragment = true;
				return;
			case ConformanceLevel.Fragment:
				this.fragmentType = XmlNodeType.Element;
				this.fragment = true;
				return;
			}
			this.fragmentType = XmlNodeType.Document;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00019A49 File Offset: 0x00017C49
		internal XmlTextReaderImpl(Stream input)
			: this(string.Empty, input, new NameTable())
		{
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00019A5C File Offset: 0x00017C5C
		internal XmlTextReaderImpl(string url, Stream input, XmlNameTable nt)
			: this(nt)
		{
			this.namespaceManager = new XmlNamespaceManager(nt);
			if (url == null || url.Length == 0)
			{
				this.InitStreamInput(input, null);
			}
			else
			{
				this.InitStreamInput(url, input, null);
			}
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00019ABC File Offset: 0x00017CBC
		internal XmlTextReaderImpl(TextReader input)
			: this(string.Empty, input, new NameTable())
		{
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00019ACF File Offset: 0x00017CCF
		internal XmlTextReaderImpl(TextReader input, XmlNameTable nt)
			: this(string.Empty, input, nt)
		{
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00019AE0 File Offset: 0x00017CE0
		internal XmlTextReaderImpl(string url, TextReader input, XmlNameTable nt)
			: this(nt)
		{
			this.namespaceManager = new XmlNamespaceManager(nt);
			this.reportedBaseUri = ((url != null) ? url : string.Empty);
			this.InitTextReaderInput(this.reportedBaseUri, input);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00019B30 File Offset: 0x00017D30
		internal XmlTextReaderImpl(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
			: this((context == null || context.NameTable == null) ? new NameTable() : context.NameTable)
		{
			if (xmlFragment == null)
			{
				xmlFragment = string.Empty;
			}
			if (context == null)
			{
				this.InitStringInput(string.Empty, Encoding.Unicode, xmlFragment);
			}
			else
			{
				this.reportedBaseUri = context.BaseURI;
				this.InitStringInput(context.BaseURI, Encoding.Unicode, xmlFragment);
			}
			this.InitFragmentReader(fragType, context, false);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00019BB4 File Offset: 0x00017DB4
		internal XmlTextReaderImpl(string xmlFragment, XmlParserContext context)
			: this((context == null || context.NameTable == null) ? new NameTable() : context.NameTable)
		{
			this.InitStringInput((context == null) ? string.Empty : context.BaseURI, Encoding.Unicode, "<?xml " + xmlFragment + "?>");
			this.InitFragmentReader(XmlNodeType.XmlDeclaration, context, true);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00019C14 File Offset: 0x00017E14
		public XmlTextReaderImpl(string url, XmlNameTable nt)
			: this(nt)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The URL cannot be empty."), "url");
			}
			this.namespaceManager = new XmlNamespaceManager(nt);
			this.url = url;
			this.ps.baseUri = this.GetTempResolver().ResolveUri(null, url);
			this.ps.baseUriStr = this.ps.baseUri.ToString();
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.OpenUrl;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00019CB4 File Offset: 0x00017EB4
		private void FinishInitUriString()
		{
			Stream stream = null;
			if (this.laterInitParam.useAsync)
			{
				Task<object> entityAsync = this.laterInitParam.inputUriResolver.GetEntityAsync(this.laterInitParam.inputbaseUri, string.Empty, typeof(Stream));
				entityAsync.Wait();
				stream = (Stream)entityAsync.Result;
			}
			else
			{
				stream = (Stream)this.laterInitParam.inputUriResolver.GetEntity(this.laterInitParam.inputbaseUri, string.Empty, typeof(Stream));
			}
			if (stream == null)
			{
				throw new XmlException("Cannot resolve '{0}'.", this.laterInitParam.inputUriStr);
			}
			Encoding encoding = null;
			if (this.laterInitParam.inputContext != null)
			{
				encoding = this.laterInitParam.inputContext.Encoding;
			}
			try
			{
				this.InitStreamInput(this.laterInitParam.inputbaseUri, this.reportedBaseUri, stream, null, 0, encoding);
				this.reportedEncoding = this.ps.encoding;
				if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
				{
					this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
				}
			}
			catch
			{
				stream.Close();
				throw;
			}
			this.laterInitParam = null;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00019DF8 File Offset: 0x00017FF8
		internal XmlTextReaderImpl(Stream stream, byte[] bytes, int byteCount, XmlReaderSettings settings, Uri baseUri, string baseUriStr, XmlParserContext context, bool closeInput)
			: this(settings.GetXmlResolver(), settings, context)
		{
			if (context != null && context.BaseURI != null && context.BaseURI.Length > 0 && !this.UriEqual(baseUri, baseUriStr, context.BaseURI, settings.GetXmlResolver()))
			{
				if (baseUriStr.Length > 0)
				{
					this.Throw("BaseUri must be specified either as an argument of XmlReader.Create or on the XmlParserContext. If it is specified on both, it must be the same base URI.");
				}
				baseUriStr = context.BaseURI;
			}
			this.reportedBaseUri = baseUriStr;
			this.closeInput = closeInput;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputStream = stream;
			this.laterInitParam.inputBytes = bytes;
			this.laterInitParam.inputByteCount = byteCount;
			this.laterInitParam.inputbaseUri = baseUri;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.Stream;
			if (!settings.Async)
			{
				this.FinishInitStream();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00019EEC File Offset: 0x000180EC
		private void FinishInitStream()
		{
			Encoding encoding = null;
			if (this.laterInitParam.inputContext != null)
			{
				encoding = this.laterInitParam.inputContext.Encoding;
			}
			this.InitStreamInput(this.laterInitParam.inputbaseUri, this.reportedBaseUri, this.laterInitParam.inputStream, this.laterInitParam.inputBytes, this.laterInitParam.inputByteCount, encoding);
			this.reportedEncoding = this.ps.encoding;
			if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
			{
				this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
			}
			this.laterInitParam = null;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00019F9C File Offset: 0x0001819C
		internal XmlTextReaderImpl(TextReader input, XmlReaderSettings settings, string baseUriStr, XmlParserContext context)
			: this(settings.GetXmlResolver(), settings, context)
		{
			if (context != null && context.BaseURI != null)
			{
				baseUriStr = context.BaseURI;
			}
			this.reportedBaseUri = baseUriStr;
			this.closeInput = settings.CloseInput;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputTextReader = input;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.TextReader;
			if (!settings.Async)
			{
				this.FinishInitTextReader();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A02C File Offset: 0x0001822C
		private void FinishInitTextReader()
		{
			this.InitTextReaderInput(this.reportedBaseUri, this.laterInitParam.inputTextReader);
			this.reportedEncoding = this.ps.encoding;
			if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
			{
				this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
			}
			this.laterInitParam = null;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A098 File Offset: 0x00018298
		internal XmlTextReaderImpl(string xmlFragment, XmlParserContext context, XmlReaderSettings settings)
			: this(null, settings, context)
		{
			this.InitStringInput(string.Empty, Encoding.Unicode, xmlFragment);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001A0D8 File Offset: 0x000182D8
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				if (this.nameTableFromSettings)
				{
					xmlReaderSettings.NameTable = this.nameTable;
				}
				XmlNodeType xmlNodeType = this.fragmentType;
				if (xmlNodeType != XmlNodeType.None)
				{
					if (xmlNodeType == XmlNodeType.Element)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
						goto IL_0046;
					}
					if (xmlNodeType == XmlNodeType.Document)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
						goto IL_0046;
					}
				}
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
				IL_0046:
				xmlReaderSettings.CheckCharacters = this.checkCharacters;
				xmlReaderSettings.LineNumberOffset = this.lineNumberOffset;
				xmlReaderSettings.LinePositionOffset = this.linePositionOffset;
				xmlReaderSettings.IgnoreWhitespace = this.whitespaceHandling == WhitespaceHandling.Significant;
				xmlReaderSettings.IgnoreProcessingInstructions = this.ignorePIs;
				xmlReaderSettings.IgnoreComments = this.ignoreComments;
				xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				xmlReaderSettings.MaxCharactersInDocument = this.maxCharactersInDocument;
				xmlReaderSettings.MaxCharactersFromEntities = this.maxCharactersFromEntities;
				if (!XmlReaderSettings.EnableLegacyXmlSettings())
				{
					xmlReaderSettings.XmlResolver = this.xmlResolver;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001A1B5 File Offset: 0x000183B5
		public override XmlNodeType NodeType
		{
			get
			{
				return this.curNode.type;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001A1C2 File Offset: 0x000183C2
		public override string Name
		{
			get
			{
				return this.curNode.GetNameWPrefix(this.nameTable);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001A1D5 File Offset: 0x000183D5
		public override string LocalName
		{
			get
			{
				return this.curNode.localName;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001A1E2 File Offset: 0x000183E2
		public override string NamespaceURI
		{
			get
			{
				return this.curNode.ns;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001A1EF File Offset: 0x000183EF
		public override string Prefix
		{
			get
			{
				return this.curNode.prefix;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001A1FC File Offset: 0x000183FC
		public override string Value
		{
			get
			{
				if (this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.PartialTextValue)
				{
					if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
					{
						this.FinishPartialValue();
						this.parsingFunction = this.nextParsingFunction;
					}
					else
					{
						this.FinishOtherValueIterator();
					}
				}
				return this.curNode.StringValue;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001A237 File Offset: 0x00018437
		public override int Depth
		{
			get
			{
				return this.curNode.depth;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001A244 File Offset: 0x00018444
		public override string BaseURI
		{
			get
			{
				return this.reportedBaseUri;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001A24C File Offset: 0x0001844C
		public override bool IsEmptyElement
		{
			get
			{
				return this.curNode.IsEmptyElement;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001A259 File Offset: 0x00018459
		public override bool IsDefault
		{
			get
			{
				return this.curNode.IsDefaultAttribute;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001A266 File Offset: 0x00018466
		public override char QuoteChar
		{
			get
			{
				if (this.curNode.type != XmlNodeType.Attribute)
				{
					return '"';
				}
				return this.curNode.quoteChar;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001A284 File Offset: 0x00018484
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlContext.xmlSpace;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001A291 File Offset: 0x00018491
		public override string XmlLang
		{
			get
			{
				return this.xmlContext.xmlLang;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001A29E File Offset: 0x0001849E
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001A2A6 File Offset: 0x000184A6
		public override bool EOF
		{
			get
			{
				return this.parsingFunction == XmlTextReaderImpl.ParsingFunction.Eof;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001A2B2 File Offset: 0x000184B2
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001A2BA File Offset: 0x000184BA
		public override int AttributeCount
		{
			get
			{
				return this.attrCount;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001A2C4 File Offset: 0x000184C4
		public override string GetAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetIndexOfAttributeWithoutPrefix(name);
			}
			else
			{
				num = this.GetIndexOfAttributeWithPrefix(name);
			}
			if (num < 0)
			{
				return null;
			}
			return this.nodes[num].StringValue;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001A304 File Offset: 0x00018504
		public override string GetAttribute(string localName, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.nameTable.Get(namespaceURI));
			localName = this.nameTable.Get(localName);
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, localName) && Ref.Equal(this.nodes[i].ns, namespaceURI))
				{
					return this.nodes[i].StringValue;
				}
			}
			return null;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001A391 File Offset: 0x00018591
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.nodes[this.index + i + 1].StringValue;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001A3C4 File Offset: 0x000185C4
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetIndexOfAttributeWithoutPrefix(name);
			}
			else
			{
				num = this.GetIndexOfAttributeWithPrefix(name);
			}
			if (num >= 0)
			{
				if (this.InAttributeValueIterator)
				{
					this.FinishAttributeValueIterator();
				}
				this.curAttrIndex = num - this.index - 1;
				this.curNode = this.nodes[num];
				return true;
			}
			return false;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001A424 File Offset: 0x00018624
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.nameTable.Get(namespaceURI));
			localName = this.nameTable.Get(localName);
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, localName) && Ref.Equal(this.nodes[i].ns, namespaceURI))
				{
					this.curAttrIndex = i - this.index - 1;
					this.curNode = this.nodes[i];
					if (this.InAttributeValueIterator)
					{
						this.FinishAttributeValueIterator();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001A4D4 File Offset: 0x000186D4
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			this.curAttrIndex = i;
			this.curNode = this.nodes[this.index + 1 + this.curAttrIndex];
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001A52A File Offset: 0x0001872A
		public override bool MoveToFirstAttribute()
		{
			if (this.attrCount == 0)
			{
				return false;
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			this.curAttrIndex = 0;
			this.curNode = this.nodes[this.index + 1];
			return true;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001A564 File Offset: 0x00018764
		public override bool MoveToNextAttribute()
		{
			if (this.curAttrIndex + 1 < this.attrCount)
			{
				if (this.InAttributeValueIterator)
				{
					this.FinishAttributeValueIterator();
				}
				XmlTextReaderImpl.NodeData[] array = this.nodes;
				int num = this.index + 1;
				int num2 = this.curAttrIndex + 1;
				this.curAttrIndex = num2;
				this.curNode = array[num + num2];
				return true;
			}
			return false;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001A5B9 File Offset: 0x000187B9
		public override bool MoveToElement()
		{
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			else if (this.curNode.type != XmlNodeType.Attribute)
			{
				return false;
			}
			this.curAttrIndex = -1;
			this.curNode = this.nodes[this.index];
			return true;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001A5F8 File Offset: 0x000187F8
		private void FinishInit()
		{
			switch (this.laterInitParam.initType)
			{
			case XmlTextReaderImpl.InitInputType.UriString:
				this.FinishInitUriString();
				return;
			case XmlTextReaderImpl.InitInputType.Stream:
				this.FinishInitStream();
				return;
			case XmlTextReaderImpl.InitInputType.TextReader:
				this.FinishInitTextReader();
				return;
			default:
				return;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001A638 File Offset: 0x00018838
		public override bool Read()
		{
			if (this.laterInitParam != null)
			{
				this.FinishInit();
			}
			for (;;)
			{
				switch (this.parsingFunction)
				{
				case XmlTextReaderImpl.ParsingFunction.ElementContent:
					goto IL_0085;
				case XmlTextReaderImpl.ParsingFunction.NoData:
					goto IL_02E7;
				case XmlTextReaderImpl.ParsingFunction.OpenUrl:
					this.OpenUrl();
					break;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractive:
					this.readState = ReadState.Interactive;
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl:
					break;
				case XmlTextReaderImpl.ParsingFunction.DocumentContent:
					goto IL_008C;
				case XmlTextReaderImpl.ParsingFunction.MoveToElementContent:
					this.ResetAttributes();
					this.index++;
					this.curNode = this.AddNode(this.index, this.index);
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ElementContent;
					continue;
				case XmlTextReaderImpl.ParsingFunction.PopElementContext:
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext:
					this.curNode = this.nodes[this.index];
					this.curNode.IsEmptyElement = false;
					this.ResetAttributes();
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel:
					this.ResetAttributes();
					this.curNode = this.nodes[this.index];
					this.parsingFunction = ((this.index == 0) ? XmlTextReaderImpl.ParsingFunction.DocumentContent : XmlTextReaderImpl.ParsingFunction.ElementContent);
					continue;
				case XmlTextReaderImpl.ParsingFunction.Error:
				case XmlTextReaderImpl.ParsingFunction.Eof:
				case XmlTextReaderImpl.ParsingFunction.ReaderClosed:
					return false;
				case XmlTextReaderImpl.ParsingFunction.EntityReference:
					goto IL_01B3;
				case XmlTextReaderImpl.ParsingFunction.InIncrementalRead:
					goto IL_02BE;
				case XmlTextReaderImpl.ParsingFunction.FragmentAttribute:
					goto IL_02C6;
				case XmlTextReaderImpl.ParsingFunction.ReportEndEntity:
					goto IL_01C7;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent:
					this.curNode = this.AddNode(this.index, this.index);
					this.reportedEncoding = this.ps.encoding;
					this.reportedBaseUri = this.ps.baseUriStr;
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent:
					goto IL_0226;
				case XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment:
					goto IL_02CD;
				case XmlTextReaderImpl.ParsingFunction.GoToEof:
					goto IL_02DD;
				case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
					this.SkipPartialTextValue();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
					this.FinishAttributeValueIterator();
					this.curNode = this.nodes[this.index];
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
					this.FinishReadValueChunk();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
					this.FinishReadContentAsBinary();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
					this.FinishReadElementContentAsBinary();
					continue;
				default:
					continue;
				}
				this.readState = ReadState.Interactive;
				this.parsingFunction = this.nextParsingFunction;
				if (this.ParseXmlDeclaration(false))
				{
					goto Block_3;
				}
				this.reportedEncoding = this.ps.encoding;
			}
			IL_0085:
			return this.ParseElementContent();
			IL_008C:
			return this.ParseDocumentContent();
			Block_3:
			this.reportedEncoding = this.ps.encoding;
			return true;
			IL_01B3:
			this.parsingFunction = this.nextParsingFunction;
			this.ParseEntityReference();
			return true;
			IL_01C7:
			this.SetupEndEntityNodeInContent();
			this.parsingFunction = this.nextParsingFunction;
			return true;
			IL_0226:
			this.curNode = this.AddNode(this.index, this.index);
			this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
			this.curNode.SetLineInfo(this.ps.lineNo, this.ps.LinePos);
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = this.nextParsingFunction;
			return true;
			IL_02BE:
			this.FinishIncrementalRead();
			return true;
			IL_02C6:
			return this.ParseFragmentAttribute();
			IL_02CD:
			this.ParseXmlDeclarationFragment();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.GoToEof;
			return true;
			IL_02DD:
			this.OnEof();
			return false;
			IL_02E7:
			this.ThrowWithoutLineInfo("Root element is missing.");
			return false;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001A964 File Offset: 0x00018B64
		public override void Close()
		{
			this.Close(this.closeInput);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001A974 File Offset: 0x00018B74
		public override void Skip()
		{
			if (this.readState != ReadState.Interactive)
			{
				return;
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
				this.curNode = this.nodes[this.index];
			}
			else
			{
				XmlTextReaderImpl.ParsingFunction parsingFunction = this.parsingFunction;
				if (parsingFunction != XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
				{
					switch (parsingFunction)
					{
					case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
						this.SkipPartialTextValue();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
						this.FinishReadValueChunk();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
						this.FinishReadContentAsBinary();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
						this.FinishReadElementContentAsBinary();
						break;
					}
				}
				else
				{
					this.FinishIncrementalRead();
				}
			}
			XmlNodeType type = this.curNode.type;
			if (type != XmlNodeType.Element)
			{
				if (type != XmlNodeType.Attribute)
				{
					goto IL_00DC;
				}
				this.outerReader.MoveToElement();
			}
			if (!this.curNode.IsEmptyElement)
			{
				int num = this.index;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipContent;
				while (this.outerReader.Read() && this.index > num)
				{
				}
				this.parsingMode = XmlTextReaderImpl.ParsingMode.Full;
			}
			IL_00DC:
			this.outerReader.Read();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001AA69 File Offset: 0x00018C69
		public override string LookupNamespace(string prefix)
		{
			if (!this.supportNamespaces)
			{
				return null;
			}
			return this.namespaceManager.LookupNamespace(prefix);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001AA84 File Offset: 0x00018C84
		public override bool ReadAttributeValue()
		{
			if (this.parsingFunction != XmlTextReaderImpl.ParsingFunction.InReadAttributeValue)
			{
				if (this.curNode.type != XmlNodeType.Attribute)
				{
					return false;
				}
				if (this.readState != ReadState.Interactive || this.curAttrIndex < 0)
				{
					return false;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
				{
					this.FinishReadValueChunk();
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					this.FinishReadContentAsBinary();
				}
				if (this.curNode.nextAttrValueChunk == null || this.entityHandling == EntityHandling.ExpandEntities)
				{
					XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, this.curNode.depth + 1);
					nodeData.SetValueNode(XmlNodeType.Text, this.curNode.StringValue);
					nodeData.lineInfo = this.curNode.lineInfo2;
					nodeData.depth = this.curNode.depth + 1;
					this.curNode = nodeData;
					nodeData.nextAttrValueChunk = null;
				}
				else
				{
					this.curNode = this.curNode.nextAttrValueChunk;
					this.AddNode(this.index + this.attrCount + 1, this.index + 2);
					this.nodes[this.index + this.attrCount + 1] = this.curNode;
					this.fullAttrCleanup = true;
				}
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
				this.attributeValueBaseEntityId = this.ps.entityId;
				return true;
			}
			else
			{
				if (this.ps.entityId != this.attributeValueBaseEntityId)
				{
					return this.ParseAttributeValueChunk();
				}
				if (this.curNode.nextAttrValueChunk != null)
				{
					this.curNode = this.curNode.nextAttrValueChunk;
					this.nodes[this.index + this.attrCount + 1] = this.curNode;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001AC34 File Offset: 0x00018E34
		public override void ResolveEntity()
		{
			if (this.curNode.type != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadAttributeValue || this.parsingFunction == XmlTextReaderImpl.ParsingFunction.FragmentAttribute)
			{
				switch (this.HandleGeneralEntityReference(this.curNode.localName, true, true, this.curNode.LinePos))
				{
				case XmlTextReaderImpl.EntityType.Expanded:
				case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
					if (this.ps.charsUsed - this.ps.charPos == 0)
					{
						this.emptyEntityInAttributeResolved = true;
						goto IL_0164;
					}
					goto IL_0164;
				case XmlTextReaderImpl.EntityType.FakeExpanded:
					this.emptyEntityInAttributeResolved = true;
					goto IL_0164;
				}
				throw new XmlException("An internal error has occurred.", string.Empty);
			}
			switch (this.HandleGeneralEntityReference(this.curNode.localName, false, true, this.curNode.LinePos))
			{
			case XmlTextReaderImpl.EntityType.Expanded:
			case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
				this.nextParsingFunction = this.parsingFunction;
				if (this.ps.charsUsed - this.ps.charPos == 0 && !this.ps.entity.IsExternal)
				{
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent;
					goto IL_0164;
				}
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent;
				goto IL_0164;
			case XmlTextReaderImpl.EntityType.FakeExpanded:
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent;
				goto IL_0164;
			}
			throw new XmlException("An internal error has occurred.", string.Empty);
			IL_0164:
			this.ps.entityResolvedManually = true;
			this.index++;
		}

		// Token: 0x1700011D RID: 285
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0001ADBF File Offset: 0x00018FBF
		internal XmlReader OuterReader
		{
			set
			{
				this.outerReader = value;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction == XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001ADFF File Offset: 0x00018FFF
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001AE10 File Offset: 0x00019010
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				}
				if (!this.InitReadContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBase64Decoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001AEDC File Offset: 0x000190DC
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.binHexDecoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBinHex");
				}
				if (!this.InitReadContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBinHexDecoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitReadElementContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBase64Decoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001B070 File Offset: 0x00019270
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.binHexDecoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitReadElementContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBinHexDecoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001B138 File Offset: 0x00019338
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			if (!XmlReader.HasValueInternal(this.curNode.type))
			{
				throw new InvalidOperationException(Res.GetString("The ReadValueAsChunk method is not supported on node type {0}.", new object[] { this.curNode.type }));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction != XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
				{
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue;
				}
				else
				{
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
					this.nextNextParsingFunction = this.nextParsingFunction;
					this.nextParsingFunction = this.parsingFunction;
				}
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadValueChunk;
				this.readValueOffset = 0;
			}
			if (count == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = this.curNode.CopyTo(this.readValueOffset, buffer, index + num, count - num);
			num += num2;
			this.readValueOffset += num2;
			if (num == count)
			{
				if (XmlCharType.IsHighSurrogate((int)buffer[index + count - 1]))
				{
					num--;
					this.readValueOffset--;
					if (num == 0)
					{
						this.Throw("The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.");
					}
				}
				return num;
			}
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				this.curNode.SetValue(string.Empty);
				bool flag = false;
				int num3 = 0;
				int num4 = 0;
				while (num < count && !flag)
				{
					int num5 = 0;
					flag = this.ParseText(out num3, out num4, ref num5);
					int num6 = count - num;
					if (num6 > num4 - num3)
					{
						num6 = num4 - num3;
					}
					XmlTextReaderImpl.BlockCopyChars(this.ps.chars, num3, buffer, index + num, num6);
					num += num6;
					num3 += num6;
				}
				this.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue);
				if (num == count && XmlCharType.IsHighSurrogate((int)buffer[index + count - 1]))
				{
					num--;
					num3--;
					if (num == 0)
					{
						this.Throw("The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.");
					}
				}
				this.readValueOffset = 0;
				this.curNode.SetValue(this.ps.chars, num3, num4 - num3);
			}
			return num;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001B348 File Offset: 0x00019548
		public int LineNumber
		{
			get
			{
				return this.curNode.LineNo;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001B355 File Offset: 0x00019555
		public int LinePosition
		{
			get
			{
				return this.curNode.LinePos;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001B362 File Offset: 0x00019562
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001B36B File Offset: 0x0001956B
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001B374 File Offset: 0x00019574
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001B37D File Offset: 0x0001957D
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.namespaceManager.GetNamespacesInScope(scope);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001B38B File Offset: 0x0001958B
		internal string LookupPrefix(string namespaceName)
		{
			return this.namespaceManager.LookupPrefix(namespaceName);
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001B399 File Offset: 0x00019599
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001B3A4 File Offset: 0x000195A4
		internal bool Namespaces
		{
			get
			{
				return this.supportNamespaces;
			}
			set
			{
				if (this.readState != ReadState.Initial)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				this.supportNamespaces = value;
				if (value)
				{
					if (this.namespaceManager is XmlTextReaderImpl.NoNamespaceManager)
					{
						if (this.fragment && this.fragmentParserContext != null && this.fragmentParserContext.NamespaceManager != null)
						{
							this.namespaceManager = this.fragmentParserContext.NamespaceManager;
						}
						else
						{
							this.namespaceManager = new XmlNamespaceManager(this.nameTable);
						}
					}
					this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
					return;
				}
				if (!(this.namespaceManager is XmlTextReaderImpl.NoNamespaceManager))
				{
					this.namespaceManager = new XmlTextReaderImpl.NoNamespaceManager();
				}
				this.xmlContext.defaultNamespace = string.Empty;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001B465 File Offset: 0x00019665
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0001B470 File Offset: 0x00019670
		internal bool Normalization
		{
			get
			{
				return this.normalize;
			}
			set
			{
				if (this.readState == ReadState.Closed)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				this.normalize = value;
				if (this.ps.entity == null || this.ps.entity.IsExternal)
				{
					this.ps.eolNormalized = !value;
				}
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001B4CB File Offset: 0x000196CB
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001B4D3 File Offset: 0x000196D3
		internal WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.whitespaceHandling;
			}
			set
			{
				if (this.readState == ReadState.Closed)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				if (value > WhitespaceHandling.None)
				{
					throw new XmlException("Expected WhitespaceHandling.None, or WhitespaceHandling.All, or WhitespaceHandling.Significant.", string.Empty);
				}
				this.whitespaceHandling = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x0001B509 File Offset: 0x00019709
		internal DtdProcessing DtdProcessing
		{
			set
			{
				if (value > DtdProcessing.Parse)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.dtdProcessing = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0001B521 File Offset: 0x00019721
		internal EntityHandling EntityHandling
		{
			set
			{
				if (value != EntityHandling.ExpandEntities && value != EntityHandling.ExpandCharEntities)
				{
					throw new XmlException("Expected EntityHandling.ExpandEntities or EntityHandling.ExpandCharEntities.", string.Empty);
				}
				this.entityHandling = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001B542 File Offset: 0x00019742
		internal bool IsResolverSet
		{
			get
			{
				return this.xmlResolverIsSet;
			}
		}

		// Token: 0x17000128 RID: 296
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0001B54C File Offset: 0x0001974C
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
				this.xmlResolverIsSet = true;
				this.ps.baseUri = null;
				for (int i = 0; i <= this.parsingStatesStackTop; i++)
				{
					this.parsingStatesStack[i].baseUri = null;
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0001A2B2 File Offset: 0x000184B2
		internal XmlNameTable DtdParserProxy_NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001B596 File Offset: 0x00019796
		internal IXmlNamespaceResolver DtdParserProxy_NamespaceResolver
		{
			get
			{
				return this.namespaceManager;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001B59E File Offset: 0x0001979E
		internal bool DtdParserProxy_DtdValidation
		{
			get
			{
				return this.DtdValidation;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001B465 File Offset: 0x00019665
		internal bool DtdParserProxy_Normalization
		{
			get
			{
				return this.normalize;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001B399 File Offset: 0x00019599
		internal bool DtdParserProxy_Namespaces
		{
			get
			{
				return this.supportNamespaces;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001B5A6 File Offset: 0x000197A6
		internal bool DtdParserProxy_V1CompatibilityMode
		{
			get
			{
				return this.v1Compat;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001B5B0 File Offset: 0x000197B0
		internal Uri DtdParserProxy_BaseUri
		{
			get
			{
				if (this.ps.baseUriStr.Length > 0 && this.ps.baseUri == null && this.xmlResolver != null)
				{
					this.ps.baseUri = this.xmlResolver.ResolveUri(null, this.ps.baseUriStr);
				}
				return this.ps.baseUri;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001B618 File Offset: 0x00019818
		internal bool DtdParserProxy_IsEof
		{
			get
			{
				return this.ps.isEof;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001B625 File Offset: 0x00019825
		internal char[] DtdParserProxy_ParsingBuffer
		{
			get
			{
				return this.ps.chars;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001B632 File Offset: 0x00019832
		internal int DtdParserProxy_ParsingBufferLength
		{
			get
			{
				return this.ps.charsUsed;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001B63F File Offset: 0x0001983F
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0001B64C File Offset: 0x0001984C
		internal int DtdParserProxy_CurrentPosition
		{
			get
			{
				return this.ps.charPos;
			}
			set
			{
				this.ps.charPos = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001B65A File Offset: 0x0001985A
		internal int DtdParserProxy_EntityStackLength
		{
			get
			{
				return this.parsingStatesStackTop + 1;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001B664 File Offset: 0x00019864
		internal bool DtdParserProxy_IsEntityEolNormalized
		{
			get
			{
				return this.ps.eolNormalized;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001B671 File Offset: 0x00019871
		internal IValidationEventHandling DtdParserProxy_ValidationEventHandling
		{
			get
			{
				return this.validationEventHandling;
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001B679 File Offset: 0x00019879
		internal void DtdParserProxy_OnNewLine(int pos)
		{
			this.OnNewLine(pos);
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001B682 File Offset: 0x00019882
		internal int DtdParserProxy_LineNo
		{
			get
			{
				return this.ps.LineNo;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001B68F File Offset: 0x0001988F
		internal int DtdParserProxy_LineStartPosition
		{
			get
			{
				return this.ps.lineStartPos;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001B69C File Offset: 0x0001989C
		internal int DtdParserProxy_ReadData()
		{
			return this.ReadData();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001B6A4 File Offset: 0x000198A4
		internal int DtdParserProxy_ParseNumericCharRef(StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.EntityType entityType;
			return this.ParseNumericCharRef(true, internalSubsetBuilder, out entityType);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001B6BB File Offset: 0x000198BB
		internal int DtdParserProxy_ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
		{
			return this.ParseNamedCharRef(expand, internalSubsetBuilder);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001B6C8 File Offset: 0x000198C8
		internal void DtdParserProxy_ParsePI(StringBuilder sb)
		{
			if (sb == null)
			{
				XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
				this.ParsePI(null);
				this.parsingMode = parsingMode;
				return;
			}
			this.ParsePI(sb);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001B700 File Offset: 0x00019900
		internal void DtdParserProxy_ParseComment(StringBuilder sb)
		{
			try
			{
				if (sb == null)
				{
					XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
					this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
					this.ParseCDataOrComment(XmlNodeType.Comment);
					this.parsingMode = parsingMode;
				}
				else
				{
					XmlTextReaderImpl.NodeData nodeData = this.curNode;
					this.curNode = this.AddNode(this.index + this.attrCount + 1, this.index);
					this.ParseCDataOrComment(XmlNodeType.Comment);
					this.curNode.CopyTo(0, sb);
					this.curNode = nodeData;
				}
			}
			catch (XmlException ex)
			{
				if (!(ex.ResString == "Unexpected end of file while parsing {0} has occurred.") || this.ps.entity == null)
				{
					throw;
				}
				this.SendValidationEvent(XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", null, this.ps.LineNo, this.ps.LinePos);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001B7CC File Offset: 0x000199CC
		private bool IsResolverNull
		{
			get
			{
				return this.xmlResolver == null || (XmlReaderSection.ProhibitDefaultUrlResolver && !this.xmlResolverIsSet);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001B7EA File Offset: 0x000199EA
		private XmlResolver GetTempResolver()
		{
			if (this.xmlResolver != null)
			{
				return this.xmlResolver;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001B800 File Offset: 0x00019A00
		internal bool DtdParserProxy_PushEntity(IDtdEntityInfo entity, out int entityId)
		{
			bool flag;
			if (entity.IsExternal)
			{
				if (this.IsResolverNull)
				{
					entityId = -1;
					return false;
				}
				flag = this.PushExternalEntity(entity);
			}
			else
			{
				this.PushInternalEntity(entity);
				flag = true;
			}
			entityId = this.ps.entityId;
			return flag;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001B843 File Offset: 0x00019A43
		internal bool DtdParserProxy_PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId)
		{
			if (this.parsingStatesStackTop == -1)
			{
				oldEntity = null;
				newEntityId = -1;
				return false;
			}
			oldEntity = this.ps.entity;
			this.PopEntity();
			newEntityId = this.ps.entityId;
			return true;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001B878 File Offset: 0x00019A78
		internal bool DtdParserProxy_PushExternalSubset(string systemId, string publicId)
		{
			if (this.IsResolverNull)
			{
				return false;
			}
			if (this.ps.baseUri == null && !string.IsNullOrEmpty(this.ps.baseUriStr))
			{
				this.ps.baseUri = this.xmlResolver.ResolveUri(null, this.ps.baseUriStr);
			}
			this.PushExternalEntityOrSubset(publicId, systemId, this.ps.baseUri, null);
			this.ps.entity = null;
			this.ps.entityId = 0;
			int charPos = this.ps.charPos;
			if (this.v1Compat)
			{
				this.EatWhitespaces(null);
			}
			if (!this.ParseXmlDeclaration(true))
			{
				this.ps.charPos = charPos;
			}
			return true;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001B934 File Offset: 0x00019B34
		internal void DtdParserProxy_PushInternalDtd(string baseUri, string internalDtd)
		{
			this.PushParsingState();
			this.RegisterConsumedCharacters((long)internalDtd.Length, false);
			this.InitStringInput(baseUri, Encoding.Unicode, internalDtd);
			this.ps.entity = null;
			this.ps.entityId = 0;
			this.ps.eolNormalized = false;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001B986 File Offset: 0x00019B86
		internal void DtdParserProxy_Throw(Exception e)
		{
			this.Throw(e);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001B98F File Offset: 0x00019B8F
		internal void DtdParserProxy_OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttributeNoChecks("SYSTEM", this.index + 1);
			nodeData.SetValue(systemId);
			nodeData.lineInfo = keywordLineInfo;
			nodeData.lineInfo2 = systemLiteralLineInfo;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		internal void DtdParserProxy_OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttributeNoChecks("PUBLIC", this.index + 1);
			nodeData.SetValue(publicId);
			nodeData.lineInfo = keywordLineInfo;
			nodeData.lineInfo2 = publicLiteralLineInfo;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001B9E1 File Offset: 0x00019BE1
		private void Throw(int pos, string res, string arg)
		{
			this.ps.charPos = pos;
			this.Throw(res, arg);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001B9F7 File Offset: 0x00019BF7
		private void Throw(int pos, string res, string[] args)
		{
			this.ps.charPos = pos;
			this.Throw(res, args);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001BA0D File Offset: 0x00019C0D
		private void Throw(int pos, string res)
		{
			this.ps.charPos = pos;
			this.Throw(res, string.Empty);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001BA27 File Offset: 0x00019C27
		private void Throw(string res)
		{
			this.Throw(res, string.Empty);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001BA35 File Offset: 0x00019C35
		private void Throw(string res, int lineNo, int linePos)
		{
			this.Throw(new XmlException(res, string.Empty, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001BA55 File Offset: 0x00019C55
		private void Throw(string res, string arg)
		{
			this.Throw(new XmlException(res, arg, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001BA85 File Offset: 0x00019C85
		private void Throw(string res, string arg, int lineNo, int linePos)
		{
			this.Throw(new XmlException(res, arg, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001BAA2 File Offset: 0x00019CA2
		private void Throw(string res, string[] args)
		{
			this.Throw(new XmlException(res, args, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001BAD2 File Offset: 0x00019CD2
		private void Throw(string res, string arg, Exception innerException)
		{
			this.Throw(res, new string[] { arg }, innerException);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001BAE6 File Offset: 0x00019CE6
		private void Throw(string res, string[] args, Exception innerException)
		{
			this.Throw(new XmlException(res, args, innerException, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001BB18 File Offset: 0x00019D18
		private void Throw(Exception e)
		{
			this.SetErrorState();
			XmlException ex = e as XmlException;
			if (ex != null)
			{
				this.curNode.SetLineInfo(ex.LineNumber, ex.LinePosition);
			}
			throw e;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001BB4D File Offset: 0x00019D4D
		private void ReThrow(Exception e, int lineNo, int linePos)
		{
			this.Throw(new XmlException(e.Message, null, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001BB6E File Offset: 0x00019D6E
		private void ThrowWithoutLineInfo(string res)
		{
			this.Throw(new XmlException(res, string.Empty, this.ps.baseUriStr));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001BB8C File Offset: 0x00019D8C
		private void ThrowWithoutLineInfo(string res, string arg)
		{
			this.Throw(new XmlException(res, arg, this.ps.baseUriStr));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001BBA6 File Offset: 0x00019DA6
		private void ThrowWithoutLineInfo(string res, string[] args, Exception innerException)
		{
			this.Throw(new XmlException(res, args, innerException, 0, 0, this.ps.baseUriStr));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001BBC3 File Offset: 0x00019DC3
		private void ThrowInvalidChar(char[] data, int length, int invCharPos)
		{
			this.Throw(invCharPos, "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, length, invCharPos));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001BBD9 File Offset: 0x00019DD9
		private void SetErrorState()
		{
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.Error;
			this.readState = ReadState.Error;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001BBEA File Offset: 0x00019DEA
		private void SendValidationEvent(XmlSeverityType severity, string code, string arg, int lineNo, int linePos)
		{
			this.SendValidationEvent(severity, new XmlSchemaException(code, arg, this.ps.baseUriStr, lineNo, linePos));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001BC09 File Offset: 0x00019E09
		private void SendValidationEvent(XmlSeverityType severity, XmlSchemaException exception)
		{
			if (this.validationEventHandling != null)
			{
				this.validationEventHandling.SendEvent(exception, severity);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001BC20 File Offset: 0x00019E20
		private bool InAttributeValueIterator
		{
			get
			{
				return this.attrCount > 0 && this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001BC3C File Offset: 0x00019E3C
		private void FinishAttributeValueIterator()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				this.FinishReadValueChunk();
			}
			else if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				this.FinishReadContentAsBinary();
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadAttributeValue)
			{
				while (this.ps.entityId != this.attributeValueBaseEntityId)
				{
					this.HandleEntityEnd(false);
				}
				this.emptyEntityInAttributeResolved = false;
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = ((this.index > 0) ? XmlTextReaderImpl.ParsingFunction.ElementContent : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001BCB8 File Offset: 0x00019EB8
		private bool DtdValidation
		{
			get
			{
				return this.validationEventHandling != null;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001BCC3 File Offset: 0x00019EC3
		private void InitStreamInput(Stream stream, Encoding encoding)
		{
			this.InitStreamInput(null, string.Empty, stream, null, 0, encoding);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001BCD5 File Offset: 0x00019ED5
		private void InitStreamInput(string baseUriStr, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(null, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001BCE3 File Offset: 0x00019EE3
		private void InitStreamInput(Uri baseUri, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(baseUri, baseUri.ToString(), stream, null, 0, encoding);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001BCF6 File Offset: 0x00019EF6
		private void InitStreamInput(Uri baseUri, string baseUriStr, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(baseUri, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001BD08 File Offset: 0x00019F08
		private void InitStreamInput(Uri baseUri, string baseUriStr, Stream stream, byte[] bytes, int byteCount, Encoding encoding)
		{
			this.ps.stream = stream;
			this.ps.baseUri = baseUri;
			this.ps.baseUriStr = baseUriStr;
			int num;
			if (bytes != null)
			{
				this.ps.bytes = bytes;
				this.ps.bytesUsed = byteCount;
				num = this.ps.bytes.Length;
			}
			else
			{
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					num = 65536;
				}
				else
				{
					num = XmlReader.CalcBufferSize(stream);
				}
				if (this.ps.bytes == null || this.ps.bytes.Length < num)
				{
					this.ps.bytes = new byte[num];
				}
			}
			if (this.ps.chars == null || this.ps.chars.Length < num + 1)
			{
				this.ps.chars = new char[num + 1];
			}
			this.ps.bytePos = 0;
			while (this.ps.bytesUsed < 4 && this.ps.bytes.Length - this.ps.bytesUsed > 0)
			{
				int num2 = stream.Read(this.ps.bytes, this.ps.bytesUsed, this.ps.bytes.Length - this.ps.bytesUsed);
				if (num2 == 0)
				{
					this.ps.isStreamEof = true;
					break;
				}
				this.ps.bytesUsed = this.ps.bytesUsed + num2;
			}
			if (encoding == null)
			{
				encoding = this.DetectEncoding();
			}
			this.SetupEncoding(encoding);
			byte[] preamble = this.ps.encoding.GetPreamble();
			int num3 = preamble.Length;
			int num4 = 0;
			while (num4 < num3 && num4 < this.ps.bytesUsed && this.ps.bytes[num4] == preamble[num4])
			{
				num4++;
			}
			if (num4 == num3)
			{
				this.ps.bytePos = num3;
			}
			this.documentStartBytePos = this.ps.bytePos;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			this.ReadData();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001BF1D File Offset: 0x0001A11D
		private void InitTextReaderInput(string baseUriStr, TextReader input)
		{
			this.InitTextReaderInput(baseUriStr, null, input);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001BF28 File Offset: 0x0001A128
		private void InitTextReaderInput(string baseUriStr, Uri baseUri, TextReader input)
		{
			this.ps.textReader = input;
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = baseUri;
			if (this.ps.chars == null)
			{
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					this.ps.chars = new char[65537];
				}
				else
				{
					this.ps.chars = new char[4097];
				}
			}
			this.ps.encoding = Encoding.Unicode;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			this.ReadData();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		private void InitStringInput(string baseUriStr, Encoding originalEncoding, string str)
		{
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = null;
			int length = str.Length;
			this.ps.chars = new char[length + 1];
			str.CopyTo(0, this.ps.chars, 0, str.Length);
			this.ps.charsUsed = length;
			this.ps.chars[length] = '\0';
			this.ps.encoding = originalEncoding;
			this.ps.eolNormalized = !this.normalize;
			this.ps.isEof = true;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001C080 File Offset: 0x0001A280
		private void InitFragmentReader(XmlNodeType fragmentType, XmlParserContext parserContext, bool allowXmlDeclFragment)
		{
			this.fragmentParserContext = parserContext;
			if (parserContext != null)
			{
				if (parserContext.NamespaceManager != null)
				{
					this.namespaceManager = parserContext.NamespaceManager;
					this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
				}
				else
				{
					this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				}
				this.ps.baseUriStr = parserContext.BaseURI;
				this.ps.baseUri = null;
				this.xmlContext.xmlLang = parserContext.XmlLang;
				this.xmlContext.xmlSpace = parserContext.XmlSpace;
			}
			else
			{
				this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				this.ps.baseUriStr = string.Empty;
				this.ps.baseUri = null;
			}
			this.reportedBaseUri = this.ps.baseUriStr;
			if (fragmentType <= XmlNodeType.Attribute)
			{
				if (fragmentType == XmlNodeType.Element)
				{
					this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
					goto IL_0147;
				}
				if (fragmentType == XmlNodeType.Attribute)
				{
					this.ps.appendMode = false;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractive;
					this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.FragmentAttribute;
					goto IL_0147;
				}
			}
			else
			{
				if (fragmentType == XmlNodeType.Document)
				{
					goto IL_0147;
				}
				if (fragmentType == XmlNodeType.XmlDeclaration)
				{
					if (allowXmlDeclFragment)
					{
						this.ps.appendMode = false;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractive;
						this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment;
						goto IL_0147;
					}
				}
			}
			this.Throw("XmlNodeType {0} is not supported for partial content parsing.", fragmentType.ToString());
			return;
			IL_0147:
			this.fragmentType = fragmentType;
			this.fragment = true;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
		private void ProcessDtdFromParserContext(XmlParserContext context)
		{
			switch (this.dtdProcessing)
			{
			case DtdProcessing.Prohibit:
				this.ThrowWithoutLineInfo("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.");
				return;
			case DtdProcessing.Ignore:
				break;
			case DtdProcessing.Parse:
				this.ParseDtdFromParserContext();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001C220 File Offset: 0x0001A420
		private void OpenUrl()
		{
			XmlResolver tempResolver = this.GetTempResolver();
			if (!(this.ps.baseUri != null))
			{
				this.ps.baseUri = tempResolver.ResolveUri(null, this.url);
				this.ps.baseUriStr = this.ps.baseUri.ToString();
			}
			try
			{
				this.OpenUrlDelegate(tempResolver);
			}
			catch
			{
				this.SetErrorState();
				throw;
			}
			if (this.ps.stream == null)
			{
				this.ThrowWithoutLineInfo("Cannot resolve '{0}'.", this.ps.baseUriStr);
			}
			this.InitStreamInput(this.ps.baseUri, this.ps.baseUriStr, this.ps.stream, null);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		private void OpenUrlDelegate(object xmlResolver)
		{
			this.ps.stream = (Stream)this.GetTempResolver().GetEntity(this.ps.baseUri, null, typeof(Stream));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001C330 File Offset: 0x0001A530
		private Encoding DetectEncoding()
		{
			if (this.ps.bytesUsed < 2)
			{
				return null;
			}
			int num = ((int)this.ps.bytes[0] << 8) | (int)this.ps.bytes[1];
			int num2 = ((this.ps.bytesUsed >= 4) ? (((int)this.ps.bytes[2] << 8) | (int)this.ps.bytes[3]) : 0);
			if (num <= 15360)
			{
				if (num != 0)
				{
					if (num != 60)
					{
						if (num == 15360)
						{
							if (num2 == 0)
							{
								return Ucs4Encoding.UCS4_Littleendian;
							}
							return Encoding.Unicode;
						}
					}
					else
					{
						if (num2 == 0)
						{
							return Ucs4Encoding.UCS4_3412;
						}
						return Encoding.BigEndianUnicode;
					}
				}
				else if (num2 <= 15360)
				{
					if (num2 == 60)
					{
						return Ucs4Encoding.UCS4_Bigendian;
					}
					if (num2 == 15360)
					{
						return Ucs4Encoding.UCS4_2143;
					}
				}
				else
				{
					if (num2 == 65279)
					{
						return Ucs4Encoding.UCS4_Bigendian;
					}
					if (num2 == 65534)
					{
						return Ucs4Encoding.UCS4_2143;
					}
				}
			}
			else if (num <= 61371)
			{
				if (num != 19567)
				{
					if (num == 61371)
					{
						if ((num2 & 65280) == 48896)
						{
							return new UTF8Encoding(true, true);
						}
					}
				}
				else if (num2 == 42900)
				{
					this.Throw("System does not support '{0}' encoding.", "ebcdic");
				}
			}
			else if (num != 65279)
			{
				if (num == 65534)
				{
					if (num2 == 0)
					{
						return Ucs4Encoding.UCS4_Littleendian;
					}
					return Encoding.Unicode;
				}
			}
			else
			{
				if (num2 == 0)
				{
					return Ucs4Encoding.UCS4_3412;
				}
				return Encoding.BigEndianUnicode;
			}
			return null;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001C4AC File Offset: 0x0001A6AC
		private void SetupEncoding(Encoding encoding)
		{
			if (encoding == null)
			{
				this.ps.encoding = Encoding.UTF8;
				this.ps.decoder = new SafeAsciiDecoder();
				return;
			}
			this.ps.encoding = encoding;
			string webName = this.ps.encoding.WebName;
			if (webName == "utf-16")
			{
				this.ps.decoder = new UTF16Decoder(false);
				return;
			}
			if (!(webName == "utf-16BE"))
			{
				this.ps.decoder = encoding.GetDecoder();
				return;
			}
			this.ps.decoder = new UTF16Decoder(true);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001C54C File Offset: 0x0001A74C
		private void SwitchEncoding(Encoding newEncoding)
		{
			if ((newEncoding.WebName != this.ps.encoding.WebName || this.ps.decoder is SafeAsciiDecoder) && !this.afterResetState)
			{
				this.UnDecodeChars();
				this.ps.appendMode = false;
				this.SetupEncoding(newEncoding);
				this.ReadData();
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		private Encoding CheckEncoding(string newEncodingName)
		{
			if (this.ps.stream == null)
			{
				return this.ps.encoding;
			}
			if (string.Compare(newEncodingName, "ucs-2", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "utf-16", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "iso-10646-ucs-2", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "ucs-4", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (this.ps.encoding.WebName != "utf-16BE" && this.ps.encoding.WebName != "utf-16" && string.Compare(newEncodingName, "ucs-4", StringComparison.OrdinalIgnoreCase) != 0)
				{
					if (this.afterResetState)
					{
						this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", newEncodingName);
					}
					else
					{
						this.ThrowWithoutLineInfo("There is no Unicode byte order mark. Cannot switch to Unicode.");
					}
				}
				return this.ps.encoding;
			}
			Encoding encoding = null;
			if (string.Compare(newEncodingName, "utf-8", StringComparison.OrdinalIgnoreCase) == 0)
			{
				encoding = new UTF8Encoding(true, true);
			}
			else
			{
				try
				{
					encoding = Encoding.GetEncoding(newEncodingName);
				}
				catch (NotSupportedException ex)
				{
					this.Throw("System does not support '{0}' encoding.", newEncodingName, ex);
				}
				catch (ArgumentException ex2)
				{
					this.Throw("System does not support '{0}' encoding.", newEncodingName, ex2);
				}
			}
			if (this.afterResetState && this.ps.encoding.WebName != encoding.WebName)
			{
				this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", newEncodingName);
			}
			return encoding;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001C714 File Offset: 0x0001A914
		private void UnDecodeChars()
		{
			if (this.maxCharactersInDocument > 0L)
			{
				this.charactersInDocument -= (long)(this.ps.charsUsed - this.ps.charPos);
			}
			if (this.maxCharactersFromEntities > 0L && this.InEntity)
			{
				this.charactersFromEntities -= (long)(this.ps.charsUsed - this.ps.charPos);
			}
			this.ps.bytePos = this.documentStartBytePos;
			if (this.ps.charPos > 0)
			{
				this.ps.bytePos = this.ps.bytePos + this.ps.encoding.GetByteCount(this.ps.chars, 0, this.ps.charPos);
			}
			this.ps.charsUsed = this.ps.charPos;
			this.ps.isEof = false;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001C7FE File Offset: 0x0001A9FE
		private void SwitchEncodingToUTF8()
		{
			this.SwitchEncoding(new UTF8Encoding(true, true));
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001C810 File Offset: 0x0001AA10
		private int ReadData()
		{
			if (this.ps.isEof)
			{
				return 0;
			}
			int num;
			if (this.ps.appendMode)
			{
				if (this.ps.charsUsed == this.ps.chars.Length - 1)
				{
					for (int i = 0; i < this.attrCount; i++)
					{
						this.nodes[this.index + i + 1].OnBufferInvalidated();
					}
					char[] array = new char[this.ps.chars.Length * 2];
					XmlTextReaderImpl.BlockCopyChars(this.ps.chars, 0, array, 0, this.ps.chars.Length);
					this.ps.chars = array;
				}
				if (this.ps.stream != null && this.ps.bytesUsed - this.ps.bytePos < 6 && this.ps.bytes.Length - this.ps.bytesUsed < 6)
				{
					byte[] array2 = new byte[this.ps.bytes.Length * 2];
					XmlTextReaderImpl.BlockCopy(this.ps.bytes, 0, array2, 0, this.ps.bytesUsed);
					this.ps.bytes = array2;
				}
				num = this.ps.chars.Length - this.ps.charsUsed - 1;
				if (num > 80)
				{
					num = 80;
				}
			}
			else
			{
				int num2 = this.ps.chars.Length;
				if (num2 - this.ps.charsUsed <= num2 / 2)
				{
					for (int j = 0; j < this.attrCount; j++)
					{
						this.nodes[this.index + j + 1].OnBufferInvalidated();
					}
					int num3 = this.ps.charsUsed - this.ps.charPos;
					if (num3 < num2 - 1)
					{
						this.ps.lineStartPos = this.ps.lineStartPos - this.ps.charPos;
						if (num3 > 0)
						{
							XmlTextReaderImpl.BlockCopyChars(this.ps.chars, this.ps.charPos, this.ps.chars, 0, num3);
						}
						this.ps.charPos = 0;
						this.ps.charsUsed = num3;
					}
					else
					{
						char[] array3 = new char[this.ps.chars.Length * 2];
						XmlTextReaderImpl.BlockCopyChars(this.ps.chars, 0, array3, 0, this.ps.chars.Length);
						this.ps.chars = array3;
					}
				}
				if (this.ps.stream != null)
				{
					int num4 = this.ps.bytesUsed - this.ps.bytePos;
					if (num4 <= 128)
					{
						if (num4 == 0)
						{
							this.ps.bytesUsed = 0;
						}
						else
						{
							XmlTextReaderImpl.BlockCopy(this.ps.bytes, this.ps.bytePos, this.ps.bytes, 0, num4);
							this.ps.bytesUsed = num4;
						}
						this.ps.bytePos = 0;
					}
				}
				num = this.ps.chars.Length - this.ps.charsUsed - 1;
			}
			if (this.ps.stream != null)
			{
				if (!this.ps.isStreamEof && this.ps.bytePos == this.ps.bytesUsed && this.ps.bytes.Length - this.ps.bytesUsed > 0)
				{
					int num5 = this.ps.stream.Read(this.ps.bytes, this.ps.bytesUsed, this.ps.bytes.Length - this.ps.bytesUsed);
					if (num5 == 0)
					{
						this.ps.isStreamEof = true;
					}
					this.ps.bytesUsed = this.ps.bytesUsed + num5;
				}
				int bytePos = this.ps.bytePos;
				num = this.GetChars(num);
				if (num == 0 && this.ps.bytePos != bytePos)
				{
					return this.ReadData();
				}
			}
			else if (this.ps.textReader != null)
			{
				num = this.ps.textReader.Read(this.ps.chars, this.ps.charsUsed, this.ps.chars.Length - this.ps.charsUsed - 1);
				this.ps.charsUsed = this.ps.charsUsed + num;
			}
			else
			{
				num = 0;
			}
			this.RegisterConsumedCharacters((long)num, this.InEntity);
			if (num == 0)
			{
				this.ps.isEof = true;
			}
			this.ps.chars[this.ps.charsUsed] = '\0';
			return num;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		private int GetChars(int maxCharsCount)
		{
			int num = this.ps.bytesUsed - this.ps.bytePos;
			if (num == 0)
			{
				return 0;
			}
			int num2;
			try
			{
				bool flag;
				this.ps.decoder.Convert(this.ps.bytes, this.ps.bytePos, num, this.ps.chars, this.ps.charsUsed, maxCharsCount, false, out num, out num2, out flag);
			}
			catch (ArgumentException)
			{
				this.InvalidCharRecovery(ref num, out num2);
			}
			this.ps.bytePos = this.ps.bytePos + num;
			this.ps.charsUsed = this.ps.charsUsed + num2;
			return num2;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001CD5C File Offset: 0x0001AF5C
		private void InvalidCharRecovery(ref int bytesCount, out int charsCount)
		{
			int num = 0;
			int i = 0;
			try
			{
				while (i < bytesCount)
				{
					int num2;
					int num3;
					bool flag;
					this.ps.decoder.Convert(this.ps.bytes, this.ps.bytePos + i, 1, this.ps.chars, this.ps.charsUsed + num, 1, false, out num2, out num3, out flag);
					num += num3;
					i += num2;
				}
			}
			catch (ArgumentException)
			{
			}
			if (num == 0)
			{
				this.Throw(this.ps.charsUsed, "Invalid character in the given encoding.");
			}
			charsCount = num;
			bytesCount = i;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001CDFC File Offset: 0x0001AFFC
		internal void Close(bool closeInput)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReaderClosed)
			{
				return;
			}
			while (this.InEntity)
			{
				this.PopParsingState();
			}
			this.ps.Close(closeInput);
			this.curNode = XmlTextReaderImpl.NodeData.None;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ReaderClosed;
			this.reportedEncoding = null;
			this.reportedBaseUri = string.Empty;
			this.readState = ReadState.Closed;
			this.fullAttrCleanup = false;
			this.ResetAttributes();
			this.laterInitParam = null;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001CE6E File Offset: 0x0001B06E
		private void ShiftBuffer(int sourcePos, int destPos, int count)
		{
			XmlTextReaderImpl.BlockCopyChars(this.ps.chars, sourcePos, this.ps.chars, destPos, count);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001CE90 File Offset: 0x0001B090
		private bool ParseXmlDeclaration(bool isTextDecl)
		{
			while (this.ps.charsUsed - this.ps.charPos < 6)
			{
				if (this.ReadData() == 0)
				{
					IL_07E0:
					if (!isTextDecl)
					{
						this.parsingFunction = this.nextParsingFunction;
					}
					if (this.afterResetState)
					{
						string webName = this.ps.encoding.WebName;
						if (webName != "utf-8" && webName != "utf-16" && webName != "utf-16BE" && !(this.ps.encoding is Ucs4Encoding))
						{
							this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", (this.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
						}
					}
					if (this.ps.decoder is SafeAsciiDecoder)
					{
						this.SwitchEncodingToUTF8();
					}
					this.ps.appendMode = false;
					return false;
				}
			}
			if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 5, "<?xml") && !this.xmlCharType.IsNameSingleChar(this.ps.chars[this.ps.charPos + 5]))
			{
				if (!isTextDecl)
				{
					this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos + 2);
					this.curNode.SetNamedNode(XmlNodeType.XmlDeclaration, this.Xml);
				}
				this.ps.charPos = this.ps.charPos + 5;
				StringBuilder stringBuilder = (isTextDecl ? new StringBuilder() : this.stringBuilder);
				int num = 0;
				Encoding encoding = null;
				for (;;)
				{
					int length = stringBuilder.Length;
					int num2 = this.EatWhitespaces((num == 0) ? null : stringBuilder);
					if (this.ps.chars[this.ps.charPos] == '?')
					{
						stringBuilder.Length = length;
						if (this.ps.chars[this.ps.charPos + 1] == '>')
						{
							break;
						}
						if (this.ps.charPos + 1 == this.ps.charsUsed)
						{
							goto IL_07B8;
						}
						this.ThrowUnexpectedToken("'>'");
					}
					if (num2 == 0 && num != 0)
					{
						this.ThrowUnexpectedToken("?>");
					}
					int num3 = this.ParseName();
					XmlTextReaderImpl.NodeData nodeData = null;
					char c = this.ps.chars[this.ps.charPos];
					if (c != 'e')
					{
						if (c != 's')
						{
							if (c != 'v' || !XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "version") || num != 0)
							{
								goto IL_03B5;
							}
							if (!isTextDecl)
							{
								nodeData = this.AddAttributeNoChecks("version", 1);
							}
						}
						else
						{
							if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "standalone") || (num != 1 && num != 2) || isTextDecl)
							{
								goto IL_03B5;
							}
							if (!isTextDecl)
							{
								nodeData = this.AddAttributeNoChecks("standalone", 1);
							}
							num = 2;
						}
					}
					else
					{
						if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "encoding") || (num != 1 && (!isTextDecl || num != 0)))
						{
							goto IL_03B5;
						}
						if (!isTextDecl)
						{
							nodeData = this.AddAttributeNoChecks("encoding", 1);
						}
						num = 1;
					}
					IL_03CA:
					if (!isTextDecl)
					{
						nodeData.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
					}
					stringBuilder.Append(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos);
					this.ps.charPos = num3;
					if (this.ps.chars[this.ps.charPos] != '=')
					{
						this.EatWhitespaces(stringBuilder);
						if (this.ps.chars[this.ps.charPos] != '=')
						{
							this.ThrowUnexpectedToken("=");
						}
					}
					stringBuilder.Append('=');
					this.ps.charPos = this.ps.charPos + 1;
					char c2 = this.ps.chars[this.ps.charPos];
					if (c2 != '"' && c2 != '\'')
					{
						this.EatWhitespaces(stringBuilder);
						c2 = this.ps.chars[this.ps.charPos];
						if (c2 != '"' && c2 != '\'')
						{
							this.ThrowUnexpectedToken("\"", "'");
						}
					}
					stringBuilder.Append(c2);
					this.ps.charPos = this.ps.charPos + 1;
					if (!isTextDecl)
					{
						nodeData.quoteChar = c2;
						nodeData.SetLineInfo2(this.ps.LineNo, this.ps.LinePos);
					}
					int num4 = this.ps.charPos;
					char[] chars;
					for (;;)
					{
						chars = this.ps.chars;
						while ((this.xmlCharType.charProperties[(int)chars[num4]] & 128) != 0)
						{
							num4++;
						}
						if (this.ps.chars[num4] == c2)
						{
							break;
						}
						if (num4 != this.ps.charsUsed)
						{
							goto IL_07A3;
						}
						if (this.ReadData() == 0)
						{
							goto Block_57;
						}
					}
					switch (num)
					{
					case 0:
						if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "1.0"))
						{
							if (!isTextDecl)
							{
								nodeData.SetValue(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
							}
							num = 1;
						}
						else
						{
							string text = new string(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
							this.Throw("Version number '{0}' is invalid.", text);
						}
						break;
					case 1:
					{
						string text2 = new string(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
						encoding = this.CheckEncoding(text2);
						if (!isTextDecl)
						{
							nodeData.SetValue(text2);
						}
						num = 2;
						break;
					}
					case 2:
						if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "yes"))
						{
							this.standalone = true;
						}
						else if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "no"))
						{
							this.standalone = false;
						}
						else
						{
							this.Throw("Syntax for an XML declaration is invalid.", this.ps.LineNo, this.ps.LinePos - 1);
						}
						if (!isTextDecl)
						{
							nodeData.SetValue(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
						}
						num = 3;
						break;
					}
					stringBuilder.Append(chars, this.ps.charPos, num4 - this.ps.charPos);
					stringBuilder.Append(c2);
					this.ps.charPos = num4 + 1;
					continue;
					Block_57:
					this.Throw("There is an unclosed literal string.");
					goto IL_07B8;
					IL_07A3:
					this.Throw(isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
					goto IL_07B8;
					IL_03B5:
					this.Throw(isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
					goto IL_03CA;
					IL_07B8:
					if (this.ps.isEof || this.ReadData() == 0)
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				if (num == 0)
				{
					this.Throw(isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
				}
				this.ps.charPos = this.ps.charPos + 2;
				if (!isTextDecl)
				{
					this.curNode.SetValue(stringBuilder.ToString());
					stringBuilder.Length = 0;
					this.nextParsingFunction = this.parsingFunction;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
				}
				if (encoding == null)
				{
					if (isTextDecl)
					{
						this.Throw("Invalid text declaration.");
					}
					if (this.afterResetState)
					{
						string webName2 = this.ps.encoding.WebName;
						if (webName2 != "utf-8" && webName2 != "utf-16" && webName2 != "utf-16BE" && !(this.ps.encoding is Ucs4Encoding))
						{
							this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", (this.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
						}
					}
					if (this.ps.decoder is SafeAsciiDecoder)
					{
						this.SwitchEncodingToUTF8();
					}
				}
				else
				{
					this.SwitchEncoding(encoding);
				}
				this.ps.appendMode = false;
				return true;
			}
			goto IL_07E0;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001D738 File Offset: 0x0001B938
		private bool ParseDocumentContent()
		{
			bool flag = false;
			int num;
			for (;;)
			{
				bool flag2 = false;
				num = this.ps.charPos;
				char[] array = this.ps.chars;
				if (array[num] == '<')
				{
					flag2 = true;
					if (this.ps.charsUsed - num >= 4)
					{
						num++;
						char c = array[num];
						if (c != '!')
						{
							if (c != '/')
							{
								if (c != '?')
								{
									goto IL_01D3;
								}
								this.ps.charPos = num + 1;
								if (this.ParsePI())
								{
									break;
								}
								continue;
							}
							else
							{
								this.Throw(num + 1, "Unexpected end tag.");
							}
						}
						else
						{
							num++;
							if (this.ps.charsUsed - num >= 2)
							{
								if (array[num] == '-')
								{
									if (array[num + 1] == '-')
									{
										this.ps.charPos = num + 2;
										if (this.ParseComment())
										{
											return true;
										}
										continue;
									}
									else
									{
										this.ThrowUnexpectedToken(num + 1, "-");
									}
								}
								else if (array[num] == '[')
								{
									if (this.fragmentType != XmlNodeType.Document)
									{
										num++;
										if (this.ps.charsUsed - num >= 6)
										{
											if (XmlConvert.StrEqual(array, num, 6, "CDATA["))
											{
												goto Block_14;
											}
											this.ThrowUnexpectedToken(num, "CDATA[");
										}
									}
									else
									{
										this.Throw(this.ps.charPos, "Data at the root level is invalid.");
									}
								}
								else if (this.fragmentType == XmlNodeType.Document || this.fragmentType == XmlNodeType.None)
								{
									this.fragmentType = XmlNodeType.Document;
									this.ps.charPos = num;
									if (this.ParseDoctypeDecl())
									{
										return true;
									}
									continue;
								}
								else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
								{
									this.Throw("Unexpected DTD declaration.");
								}
								else
								{
									this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
								}
							}
						}
					}
				}
				else if (array[num] == '&')
				{
					if (this.fragmentType == XmlNodeType.Document)
					{
						this.Throw(num, "Data at the root level is invalid.");
					}
					else
					{
						if (this.fragmentType == XmlNodeType.None)
						{
							this.fragmentType = XmlNodeType.Element;
						}
						int num2;
						XmlTextReaderImpl.EntityType entityType = this.HandleEntityReference(false, XmlTextReaderImpl.EntityExpandType.OnlyGeneral, out num2);
						if (entityType > XmlTextReaderImpl.EntityType.CharacterNamed)
						{
							if (entityType == XmlTextReaderImpl.EntityType.Unexpanded)
							{
								goto Block_26;
							}
							array = this.ps.chars;
							num = this.ps.charPos;
							continue;
						}
						else
						{
							if (this.ParseText())
							{
								return true;
							}
							continue;
						}
					}
				}
				else if (num != this.ps.charsUsed && ((!this.v1Compat && !flag) || array[num] != '\0'))
				{
					if (this.fragmentType == XmlNodeType.Document)
					{
						if (this.ParseRootLevelWhitespace())
						{
							return true;
						}
						continue;
					}
					else
					{
						if (this.ParseText())
						{
							goto Block_33;
						}
						continue;
					}
				}
				if (this.ReadData() != 0)
				{
					num = this.ps.charPos;
					num = this.ps.charPos;
					array = this.ps.chars;
				}
				else
				{
					if (flag2)
					{
						this.Throw("Data at the root level is invalid.");
					}
					if (!this.InEntity)
					{
						goto IL_034B;
					}
					if (this.HandleEntityEnd(true))
					{
						goto Block_39;
					}
				}
			}
			return true;
			Block_14:
			this.ps.charPos = num + 6;
			this.ParseCData();
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return true;
			IL_01D3:
			if (this.rootElementParsed)
			{
				if (this.fragmentType == XmlNodeType.Document)
				{
					this.Throw(num, "There are multiple root elements.");
				}
				if (this.fragmentType == XmlNodeType.None)
				{
					this.fragmentType = XmlNodeType.Element;
				}
			}
			this.ps.charPos = num;
			this.rootElementParsed = true;
			this.ParseElement();
			return true;
			Block_26:
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextParsingFunction;
			}
			this.ParseEntityReference();
			return true;
			Block_33:
			if (this.fragmentType == XmlNodeType.None && this.curNode.type == XmlNodeType.Text)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return true;
			Block_39:
			this.SetupEndEntityNodeInContent();
			return true;
			IL_034B:
			if (!this.rootElementParsed && this.fragmentType == XmlNodeType.Document)
			{
				this.ThrowWithoutLineInfo("Root element is missing.");
			}
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = (this.rootElementParsed ? XmlNodeType.Document : XmlNodeType.Element);
			}
			this.OnEof();
			return false;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001DAEC File Offset: 0x0001BCEC
		private bool ParseElementContent()
		{
			int num;
			for (;;)
			{
				num = this.ps.charPos;
				char[] chars = this.ps.chars;
				char c = chars[num];
				if (c != '&')
				{
					if (c == '<')
					{
						char c2 = chars[num + 1];
						if (c2 != '!')
						{
							if (c2 == '/')
							{
								goto IL_013B;
							}
							if (c2 == '?')
							{
								this.ps.charPos = num + 2;
								if (this.ParsePI())
								{
									break;
								}
								continue;
							}
							else if (num + 1 != this.ps.charsUsed)
							{
								goto Block_14;
							}
						}
						else
						{
							num += 2;
							if (this.ps.charsUsed - num >= 2)
							{
								if (chars[num] == '-')
								{
									if (chars[num + 1] == '-')
									{
										this.ps.charPos = num + 2;
										if (this.ParseComment())
										{
											return true;
										}
										continue;
									}
									else
									{
										this.ThrowUnexpectedToken(num + 1, "-");
									}
								}
								else if (chars[num] == '[')
								{
									num++;
									if (this.ps.charsUsed - num >= 6)
									{
										if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
										{
											goto Block_12;
										}
										this.ThrowUnexpectedToken(num, "CDATA[");
									}
								}
								else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
								{
									this.Throw("Unexpected DTD declaration.");
								}
								else
								{
									this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
								}
							}
						}
					}
					else if (num != this.ps.charsUsed)
					{
						if (this.ParseText())
						{
							return true;
						}
						continue;
					}
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos != 0)
						{
							this.ThrowUnclosedElements();
						}
						if (!this.InEntity)
						{
							if (this.index == 0 && this.fragmentType != XmlNodeType.Document)
							{
								goto Block_22;
							}
							this.ThrowUnclosedElements();
						}
						if (this.HandleEntityEnd(true))
						{
							goto Block_23;
						}
					}
				}
				else if (this.ParseText())
				{
					return true;
				}
			}
			return true;
			Block_12:
			this.ps.charPos = num + 6;
			this.ParseCData();
			return true;
			IL_013B:
			this.ps.charPos = num + 2;
			this.ParseEndElement();
			return true;
			Block_14:
			this.ps.charPos = num + 1;
			this.ParseElement();
			return true;
			Block_22:
			this.OnEof();
			return false;
			Block_23:
			this.SetupEndEntityNodeInContent();
			return true;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001DD00 File Offset: 0x0001BF00
		private void ThrowUnclosedElements()
		{
			if (this.index == 0 && this.curNode.type != XmlNodeType.Element)
			{
				this.Throw(this.ps.charsUsed, "Unexpected end of file has occurred.");
				return;
			}
			int i = ((this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead) ? this.index : (this.index - 1));
			this.stringBuilder.Length = 0;
			while (i >= 0)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				if (nodeData.type == XmlNodeType.Element)
				{
					this.stringBuilder.Append(nodeData.GetNameWPrefix(this.nameTable));
					if (i > 0)
					{
						this.stringBuilder.Append(", ");
					}
					else
					{
						this.stringBuilder.Append(".");
					}
				}
				i--;
			}
			this.Throw(this.ps.charsUsed, "Unexpected end of file has occurred. The following elements are not closed: {0}", this.stringBuilder.ToString());
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001DDE0 File Offset: 0x0001BFE0
		private void ParseElement()
		{
			int num = this.ps.charPos;
			char[] array = this.ps.chars;
			int num2 = -1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			while ((this.xmlCharType.charProperties[(int)array[num]] & 4) != 0)
			{
				num++;
				for (;;)
				{
					if ((this.xmlCharType.charProperties[(int)array[num]] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (array[num] != ':')
						{
							goto IL_00A2;
						}
						if (num2 == -1)
						{
							break;
						}
						if (this.supportNamespaces)
						{
							goto Block_5;
						}
						num++;
					}
				}
				num2 = num;
				num++;
				continue;
				Block_5:
				this.Throw(num, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				break;
				IL_00A2:
				if (num + 1 >= this.ps.charsUsed)
				{
					break;
				}
				IL_00C7:
				this.namespaceManager.PushScope();
				if (num2 == -1 || !this.supportNamespaces)
				{
					this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(array, this.ps.charPos, num - this.ps.charPos));
				}
				else
				{
					int charPos = this.ps.charPos;
					int num3 = num2 - charPos;
					if (num3 == this.lastPrefix.Length && XmlConvert.StrEqual(array, charPos, num3, this.lastPrefix))
					{
						this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(array, num2 + 1, num - num2 - 1), this.lastPrefix, null);
					}
					else
					{
						this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(array, num2 + 1, num - num2 - 1), this.nameTable.Add(array, this.ps.charPos, num3), null);
						this.lastPrefix = this.curNode.prefix;
					}
				}
				char c = array[num];
				if ((this.xmlCharType.charProperties[(int)c] & 1) > 0)
				{
					this.ps.charPos = num;
					this.ParseAttributes();
					return;
				}
				if (c == '>')
				{
					this.ps.charPos = num + 1;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
				}
				else if (c == '/')
				{
					if (num + 1 == this.ps.charsUsed)
					{
						this.ps.charPos = num;
						if (this.ReadData() == 0)
						{
							this.Throw(num, "Unexpected end of file while parsing {0} has occurred.", ">");
						}
						num = this.ps.charPos;
						array = this.ps.chars;
					}
					if (array[num + 1] == '>')
					{
						this.curNode.IsEmptyElement = true;
						this.nextParsingFunction = this.parsingFunction;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
						this.ps.charPos = num + 2;
					}
					else
					{
						this.ThrowUnexpectedToken(num, ">");
					}
				}
				else
				{
					this.Throw(num, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(array, this.ps.charsUsed, num));
				}
				if (this.addDefaultAttributesAndNormalize)
				{
					this.AddDefaultAttributesAndNormalize();
				}
				this.ElementNamespaceLookup();
				return;
			}
			num = this.ParseQName(out num2);
			array = this.ps.chars;
			goto IL_00C7;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		private void AddDefaultAttributesAndNormalize()
		{
			IDtdAttributeListInfo dtdAttributeListInfo = this.dtdInfo.LookupAttributeList(this.curNode.localName, this.curNode.prefix);
			if (dtdAttributeListInfo == null)
			{
				return;
			}
			if (this.normalize && dtdAttributeListInfo.HasNonCDataAttributes)
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
					IDtdAttributeInfo dtdAttributeInfo = dtdAttributeListInfo.LookupAttribute(nodeData.prefix, nodeData.localName);
					if (dtdAttributeInfo != null && dtdAttributeInfo.IsNonCDataType)
					{
						if (this.DtdValidation && this.standalone && dtdAttributeInfo.IsDeclaredInExternal)
						{
							string stringValue = nodeData.StringValue;
							nodeData.TrimSpacesInValue();
							if (stringValue != nodeData.StringValue)
							{
								this.SendValidationEvent(XmlSeverityType.Error, "StandAlone is 'yes' and the value of the attribute '{0}' contains a definition in an external document that changes on normalization.", nodeData.GetNameWPrefix(this.nameTable), nodeData.LineNo, nodeData.LinePos);
							}
						}
						else
						{
							nodeData.TrimSpacesInValue();
						}
					}
				}
			}
			IEnumerable<IDtdDefaultAttributeInfo> enumerable = dtdAttributeListInfo.LookupDefaultAttributes();
			if (enumerable != null)
			{
				int num = this.attrCount;
				XmlTextReaderImpl.NodeData[] array = null;
				if (this.attrCount >= 250)
				{
					array = new XmlTextReaderImpl.NodeData[this.attrCount];
					Array.Copy(this.nodes, this.index + 1, array, 0, this.attrCount);
					object[] array2 = array;
					Array.Sort<object>(array2, XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.Instance);
				}
				foreach (IDtdDefaultAttributeInfo dtdDefaultAttributeInfo in enumerable)
				{
					if (this.AddDefaultAttributeDtd(dtdDefaultAttributeInfo, true, array) && this.DtdValidation && this.standalone && dtdDefaultAttributeInfo.IsDeclaredInExternal)
					{
						string prefix = dtdDefaultAttributeInfo.Prefix;
						string text = ((prefix.Length == 0) ? dtdDefaultAttributeInfo.LocalName : (prefix + ":" + dtdDefaultAttributeInfo.LocalName));
						this.SendValidationEvent(XmlSeverityType.Error, "Markup for unspecified default attribute '{0}' is external and standalone='yes'.", text, this.curNode.LineNo, this.curNode.LinePos);
					}
				}
				if (num == 0 && this.attrNeedNamespaceLookup)
				{
					this.AttributeNamespaceLookup();
					this.attrNeedNamespaceLookup = false;
				}
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001E2EC File Offset: 0x0001C4EC
		private void ParseEndElement()
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[this.index - 1];
			int length = nodeData.prefix.Length;
			int length2 = nodeData.localName.Length;
			while (this.ps.charsUsed - this.ps.charPos < length + length2 + 1 && this.ReadData() != 0)
			{
			}
			char[] array = this.ps.chars;
			int num;
			if (nodeData.prefix.Length == 0)
			{
				if (!XmlConvert.StrEqual(array, this.ps.charPos, length2, nodeData.localName))
				{
					this.ThrowTagMismatch(nodeData);
				}
				num = length2;
			}
			else
			{
				int num2 = this.ps.charPos + length;
				if (!XmlConvert.StrEqual(array, this.ps.charPos, length, nodeData.prefix) || array[num2] != ':' || !XmlConvert.StrEqual(array, num2 + 1, length2, nodeData.localName))
				{
					this.ThrowTagMismatch(nodeData);
				}
				num = length2 + length + 1;
			}
			LineInfo lineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			int num3;
			for (;;)
			{
				num3 = this.ps.charPos + num;
				array = this.ps.chars;
				if (num3 != this.ps.charsUsed)
				{
					if ((this.xmlCharType.charProperties[(int)array[num3]] & 8) != 0 || array[num3] == ':')
					{
						this.ThrowTagMismatch(nodeData);
					}
					if (array[num3] != '>')
					{
						char c;
						while (this.xmlCharType.IsWhiteSpace(c = array[num3]))
						{
							num3++;
							if (c != '\n')
							{
								if (c == '\r')
								{
									if (array[num3] == '\n')
									{
										num3++;
									}
									else if (num3 == this.ps.charsUsed && !this.ps.isEof)
									{
										continue;
									}
									this.OnNewLine(num3);
								}
							}
							else
							{
								this.OnNewLine(num3);
							}
						}
					}
					if (array[num3] == '>')
					{
						break;
					}
					if (num3 != this.ps.charsUsed)
					{
						this.ThrowUnexpectedToken(num3, ">");
					}
				}
				if (this.ReadData() == 0)
				{
					this.ThrowUnclosedElements();
				}
			}
			this.index--;
			this.curNode = this.nodes[this.index];
			nodeData.lineInfo = lineInfo;
			nodeData.type = XmlNodeType.EndElement;
			this.ps.charPos = num3 + 1;
			this.nextParsingFunction = ((this.index > 0) ? this.parsingFunction : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001E55C File Offset: 0x0001C75C
		private void ThrowTagMismatch(XmlTextReaderImpl.NodeData startTag)
		{
			if (startTag.type == XmlNodeType.Element)
			{
				int num2;
				int num = this.ParseQName(out num2);
				this.Throw("The '{0}' start tag on line {1} position {2} does not match the end tag of '{3}'.", new string[]
				{
					startTag.GetNameWPrefix(this.nameTable),
					startTag.lineInfo.lineNo.ToString(CultureInfo.InvariantCulture),
					startTag.lineInfo.linePos.ToString(CultureInfo.InvariantCulture),
					new string(this.ps.chars, this.ps.charPos, num - this.ps.charPos)
				});
				return;
			}
			this.Throw("Unexpected end tag.");
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001E608 File Offset: 0x0001C808
		private void ParseAttributes()
		{
			int num = this.ps.charPos;
			char[] array = this.ps.chars;
			for (;;)
			{
				IL_001A:
				int num2 = 0;
				char c;
				while ((this.xmlCharType.charProperties[(int)(c = array[num])] & 1) != 0)
				{
					if (c == '\n')
					{
						this.OnNewLine(num + 1);
						num2++;
					}
					else if (c == '\r')
					{
						if (array[num + 1] == '\n')
						{
							this.OnNewLine(num + 2);
							num2++;
							num++;
						}
						else if (num + 1 != this.ps.charsUsed)
						{
							this.OnNewLine(num + 1);
							num2++;
						}
						else
						{
							this.ps.charPos = num;
							IL_042C:
							this.ps.lineNo = this.ps.lineNo - num2;
							if (this.ReadData() != 0)
							{
								num = this.ps.charPos;
								array = this.ps.chars;
								goto IL_001A;
							}
							this.ThrowUnclosedElements();
							goto IL_001A;
						}
					}
					num++;
				}
				int num3 = 0;
				char c2;
				if ((this.xmlCharType.charProperties[(int)(c2 = array[num])] & 4) != 0)
				{
					num3 = 1;
				}
				if (num3 == 0)
				{
					if (c2 == '>')
					{
						break;
					}
					if (c2 == '/')
					{
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_042C;
						}
						if (array[num + 1] == '>')
						{
							goto Block_11;
						}
						this.ThrowUnexpectedToken(num + 1, ">");
					}
					else
					{
						if (num == this.ps.charsUsed)
						{
							goto IL_042C;
						}
						if (c2 != ':' || this.supportNamespaces)
						{
							this.Throw(num, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(array, this.ps.charsUsed, num));
						}
					}
				}
				if (num == this.ps.charPos)
				{
					this.ThrowExpectingWhitespace(num);
				}
				this.ps.charPos = num;
				int linePos = this.ps.LinePos;
				int num4 = -1;
				num += num3;
				for (;;)
				{
					char c3;
					if ((this.xmlCharType.charProperties[(int)(c3 = array[num])] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (c3 != ':')
						{
							goto IL_023E;
						}
						if (num4 != -1)
						{
							if (this.supportNamespaces)
							{
								goto Block_18;
							}
							num++;
						}
						else
						{
							num4 = num;
							num++;
							if ((this.xmlCharType.charProperties[(int)array[num]] & 4) == 0)
							{
								goto IL_0227;
							}
							num++;
						}
					}
				}
				IL_0263:
				XmlTextReaderImpl.NodeData nodeData = this.AddAttribute(num, num4);
				nodeData.SetLineInfo(this.ps.LineNo, linePos);
				if (array[num] != '=')
				{
					this.ps.charPos = num;
					this.EatWhitespaces(null);
					num = this.ps.charPos;
					if (array[num] != '=')
					{
						this.ThrowUnexpectedToken("=");
					}
				}
				num++;
				char c4 = array[num];
				if (c4 != '"' && c4 != '\'')
				{
					this.ps.charPos = num;
					this.EatWhitespaces(null);
					num = this.ps.charPos;
					c4 = array[num];
					if (c4 != '"' && c4 != '\'')
					{
						this.ThrowUnexpectedToken("\"", "'");
					}
				}
				num++;
				this.ps.charPos = num;
				nodeData.quoteChar = c4;
				nodeData.SetLineInfo2(this.ps.LineNo, this.ps.LinePos);
				char c5;
				while ((this.xmlCharType.charProperties[(int)(c5 = array[num])] & 128) != 0)
				{
					num++;
				}
				if (c5 == c4)
				{
					nodeData.SetValue(array, this.ps.charPos, num - this.ps.charPos);
					num++;
					this.ps.charPos = num;
				}
				else
				{
					this.ParseAttributeValueSlow(num, c4, nodeData);
					num = this.ps.charPos;
					array = this.ps.chars;
				}
				if (nodeData.prefix.Length == 0)
				{
					if (Ref.Equal(nodeData.localName, this.XmlNs))
					{
						this.OnDefaultNamespaceDecl(nodeData);
						continue;
					}
					continue;
				}
				else
				{
					if (Ref.Equal(nodeData.prefix, this.XmlNs))
					{
						this.OnNamespaceDecl(nodeData);
						continue;
					}
					if (Ref.Equal(nodeData.prefix, this.Xml))
					{
						this.OnXmlReservedAttribute(nodeData);
						continue;
					}
					continue;
				}
				Block_18:
				this.Throw(num, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				goto IL_0263;
				IL_0227:
				num = this.ParseQName(out num4);
				array = this.ps.chars;
				goto IL_0263;
				IL_023E:
				if (num + 1 >= this.ps.charsUsed)
				{
					num = this.ParseQName(out num4);
					array = this.ps.chars;
					goto IL_0263;
				}
				goto IL_0263;
			}
			this.ps.charPos = num + 1;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
			goto IL_046C;
			Block_11:
			this.ps.charPos = num + 2;
			this.curNode.IsEmptyElement = true;
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
			IL_046C:
			if (this.addDefaultAttributesAndNormalize)
			{
				this.AddDefaultAttributesAndNormalize();
			}
			this.ElementNamespaceLookup();
			if (this.attrNeedNamespaceLookup)
			{
				this.AttributeNamespaceLookup();
				this.attrNeedNamespaceLookup = false;
			}
			if (this.attrDuplWalkCount >= 250)
			{
				this.AttributeDuplCheck();
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		private void ElementNamespaceLookup()
		{
			if (this.curNode.prefix.Length == 0)
			{
				this.curNode.ns = this.xmlContext.defaultNamespace;
				return;
			}
			this.curNode.ns = this.LookupNamespace(this.curNode);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001EB10 File Offset: 0x0001CD10
		private void AttributeNamespaceLookup()
		{
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				if (nodeData.type == XmlNodeType.Attribute && nodeData.prefix.Length > 0)
				{
					nodeData.ns = this.LookupNamespace(nodeData);
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		private void AttributeDuplCheck()
		{
			if (this.attrCount < 250)
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
					for (int j = i + 1; j < this.index + 1 + this.attrCount; j++)
					{
						if (Ref.Equal(nodeData.localName, this.nodes[j].localName) && Ref.Equal(nodeData.ns, this.nodes[j].ns))
						{
							this.Throw("'{0}' is a duplicate attribute name.", this.nodes[j].GetNameWPrefix(this.nameTable), this.nodes[j].LineNo, this.nodes[j].LinePos);
						}
					}
				}
				return;
			}
			if (this.attrDuplSortingArray == null || this.attrDuplSortingArray.Length < this.attrCount)
			{
				this.attrDuplSortingArray = new XmlTextReaderImpl.NodeData[this.attrCount];
			}
			Array.Copy(this.nodes, this.index + 1, this.attrDuplSortingArray, 0, this.attrCount);
			Array.Sort<XmlTextReaderImpl.NodeData>(this.attrDuplSortingArray, 0, this.attrCount);
			XmlTextReaderImpl.NodeData nodeData2 = this.attrDuplSortingArray[0];
			for (int k = 1; k < this.attrCount; k++)
			{
				XmlTextReaderImpl.NodeData nodeData3 = this.attrDuplSortingArray[k];
				if (Ref.Equal(nodeData2.localName, nodeData3.localName) && Ref.Equal(nodeData2.ns, nodeData3.ns))
				{
					this.Throw("'{0}' is a duplicate attribute name.", nodeData3.GetNameWPrefix(this.nameTable), nodeData3.LineNo, nodeData3.LinePos);
				}
				nodeData2 = nodeData3;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001ED1C File Offset: 0x0001CF1C
		private void OnDefaultNamespaceDecl(XmlTextReaderImpl.NodeData attr)
		{
			if (!this.supportNamespaces)
			{
				return;
			}
			string text = this.nameTable.Add(attr.StringValue);
			attr.ns = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			if (!this.curNode.xmlContextPushed)
			{
				this.PushXmlContext();
			}
			this.xmlContext.defaultNamespace = text;
			this.AddNamespace(string.Empty, text, attr);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001ED88 File Offset: 0x0001CF88
		private void OnNamespaceDecl(XmlTextReaderImpl.NodeData attr)
		{
			if (!this.supportNamespaces)
			{
				return;
			}
			string text = this.nameTable.Add(attr.StringValue);
			if (text.Length == 0)
			{
				this.Throw("Invalid namespace declaration.", attr.lineInfo2.lineNo, attr.lineInfo2.linePos - 1);
			}
			this.AddNamespace(attr.localName, text, attr);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		private void OnXmlReservedAttribute(XmlTextReaderImpl.NodeData attr)
		{
			string localName = attr.localName;
			if (!(localName == "space"))
			{
				if (!(localName == "lang"))
				{
					return;
				}
				if (!this.curNode.xmlContextPushed)
				{
					this.PushXmlContext();
				}
				this.xmlContext.xmlLang = attr.StringValue;
				return;
			}
			else
			{
				if (!this.curNode.xmlContextPushed)
				{
					this.PushXmlContext();
				}
				string text = XmlConvert.TrimString(attr.StringValue);
				if (text == "preserve")
				{
					this.xmlContext.xmlSpace = XmlSpace.Preserve;
					return;
				}
				if (!(text == "default"))
				{
					this.Throw("'{0}' is an invalid xml:space value.", attr.StringValue, attr.lineInfo.lineNo, attr.lineInfo.linePos);
					return;
				}
				this.xmlContext.xmlSpace = XmlSpace.Default;
				return;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		private void ParseAttributeValueSlow(int curPos, char quoteChar, XmlTextReaderImpl.NodeData attr)
		{
			int num = curPos;
			char[] array = this.ps.chars;
			int entityId = this.ps.entityId;
			int num2 = 0;
			LineInfo lineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			XmlTextReaderImpl.NodeData nodeData = null;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)array[num]] & 128) == 0)
				{
					if (num - this.ps.charPos > 0)
					{
						this.stringBuilder.Append(array, this.ps.charPos, num - this.ps.charPos);
						this.ps.charPos = num;
					}
					if (array[num] == quoteChar && entityId == this.ps.entityId)
					{
						goto IL_063F;
					}
					char c = array[num];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							num++;
							if (this.normalize)
							{
								this.stringBuilder.Append(' ');
								this.ps.charPos = this.ps.charPos + 1;
								continue;
							}
							continue;
						case '\n':
							num++;
							this.OnNewLine(num);
							if (this.normalize)
							{
								this.stringBuilder.Append(' ');
								this.ps.charPos = this.ps.charPos + 1;
								continue;
							}
							continue;
						case '\v':
						case '\f':
							goto IL_04F8;
						case '\r':
							if (array[num + 1] == '\n')
							{
								num += 2;
								if (this.normalize)
								{
									this.stringBuilder.Append(this.ps.eolNormalized ? "  " : " ");
									this.ps.charPos = num;
								}
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_054A;
								}
								num++;
								if (this.normalize)
								{
									this.stringBuilder.Append(' ');
									this.ps.charPos = num;
								}
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c != '"')
							{
								if (c != '&')
								{
									goto IL_04F8;
								}
								if (num - this.ps.charPos > 0)
								{
									this.stringBuilder.Append(array, this.ps.charPos, num - this.ps.charPos);
								}
								this.ps.charPos = num;
								int entityId2 = this.ps.entityId;
								LineInfo lineInfo2 = new LineInfo(this.ps.lineNo, this.ps.LinePos + 1);
								switch (this.HandleEntityReference(true, XmlTextReaderImpl.EntityExpandType.All, out num))
								{
								case XmlTextReaderImpl.EntityType.CharacterDec:
								case XmlTextReaderImpl.EntityType.CharacterHex:
								case XmlTextReaderImpl.EntityType.CharacterNamed:
									break;
								case XmlTextReaderImpl.EntityType.Expanded:
								case XmlTextReaderImpl.EntityType.Skipped:
								case XmlTextReaderImpl.EntityType.FakeExpanded:
									goto IL_04DB;
								case XmlTextReaderImpl.EntityType.Unexpanded:
									if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && this.ps.entityId == entityId)
									{
										int num3 = this.stringBuilder.Length - num2;
										if (num3 > 0)
										{
											XmlTextReaderImpl.NodeData nodeData2 = new XmlTextReaderImpl.NodeData();
											nodeData2.lineInfo = lineInfo;
											nodeData2.depth = attr.depth + 1;
											nodeData2.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num3));
											this.AddAttributeChunkToList(attr, nodeData2, ref nodeData);
										}
										this.ps.charPos = this.ps.charPos + 1;
										string text = this.ParseEntityName();
										XmlTextReaderImpl.NodeData nodeData3 = new XmlTextReaderImpl.NodeData();
										nodeData3.lineInfo = lineInfo2;
										nodeData3.depth = attr.depth + 1;
										nodeData3.SetNamedNode(XmlNodeType.EntityReference, text);
										this.AddAttributeChunkToList(attr, nodeData3, ref nodeData);
										this.stringBuilder.Append('&');
										this.stringBuilder.Append(text);
										this.stringBuilder.Append(';');
										num2 = this.stringBuilder.Length;
										lineInfo.Set(this.ps.LineNo, this.ps.LinePos);
										this.fullAttrCleanup = true;
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
										this.ParseEntityName();
									}
									num = this.ps.charPos;
									break;
								case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
									if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && entityId2 == entityId)
									{
										int num4 = this.stringBuilder.Length - num2;
										if (num4 > 0)
										{
											XmlTextReaderImpl.NodeData nodeData4 = new XmlTextReaderImpl.NodeData();
											nodeData4.lineInfo = lineInfo;
											nodeData4.depth = attr.depth + 1;
											nodeData4.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num4));
											this.AddAttributeChunkToList(attr, nodeData4, ref nodeData);
										}
										XmlTextReaderImpl.NodeData nodeData5 = new XmlTextReaderImpl.NodeData();
										nodeData5.lineInfo = lineInfo2;
										nodeData5.depth = attr.depth + 1;
										nodeData5.SetNamedNode(XmlNodeType.EntityReference, this.ps.entity.Name);
										this.AddAttributeChunkToList(attr, nodeData5, ref nodeData);
										this.fullAttrCleanup = true;
									}
									num = this.ps.charPos;
									break;
								default:
									goto IL_04DB;
								}
								IL_04E7:
								array = this.ps.chars;
								continue;
								IL_04DB:
								num = this.ps.charPos;
								goto IL_04E7;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c == '<')
						{
							this.Throw(num, "'{0}', hexadecimal value {1}, is an invalid attribute character.", XmlException.BuildCharExceptionArgs('<', '\0'));
							goto IL_054A;
						}
						if (c != '>')
						{
							goto IL_04F8;
						}
					}
					num++;
					continue;
					IL_04F8:
					if (num != this.ps.charsUsed)
					{
						if (XmlCharType.IsHighSurrogate((int)array[num]))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_054A;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)array[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(array, this.ps.charsUsed, num);
					}
					IL_054A:
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r')
							{
								this.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							if (!this.InEntity)
							{
								if (this.fragmentType == XmlNodeType.Attribute)
								{
									break;
								}
								this.Throw("There is an unclosed literal string.");
							}
							if (this.HandleEntityEnd(true))
							{
								this.Throw("An internal error has occurred.");
							}
							if (entityId == this.ps.entityId)
							{
								num2 = this.stringBuilder.Length;
								lineInfo.Set(this.ps.LineNo, this.ps.LinePos);
							}
						}
					}
					num = this.ps.charPos;
					array = this.ps.chars;
				}
				else
				{
					num++;
				}
			}
			if (entityId != this.ps.entityId)
			{
				this.Throw("Entity replacement text must nest properly within markup declarations.");
			}
			IL_063F:
			if (attr.nextAttrValueChunk != null)
			{
				int num5 = this.stringBuilder.Length - num2;
				if (num5 > 0)
				{
					XmlTextReaderImpl.NodeData nodeData6 = new XmlTextReaderImpl.NodeData();
					nodeData6.lineInfo = lineInfo;
					nodeData6.depth = attr.depth + 1;
					nodeData6.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num5));
					this.AddAttributeChunkToList(attr, nodeData6, ref nodeData);
				}
			}
			this.ps.charPos = num + 1;
			attr.SetValue(this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001F58F File Offset: 0x0001D78F
		private void AddAttributeChunkToList(XmlTextReaderImpl.NodeData attr, XmlTextReaderImpl.NodeData chunk, ref XmlTextReaderImpl.NodeData lastChunk)
		{
			if (lastChunk == null)
			{
				lastChunk = chunk;
				attr.nextAttrValueChunk = chunk;
				return;
			}
			lastChunk.nextAttrValueChunk = chunk;
			lastChunk = chunk;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		private bool ParseText()
		{
			int num = 0;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				int num2;
				int num3;
				while (!this.ParseText(out num2, out num3, ref num))
				{
				}
			}
			else
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				int num2;
				int num3;
				if (this.ParseText(out num2, out num3, ref num))
				{
					if (num3 - num2 != 0)
					{
						XmlNodeType textNodeType = this.GetTextNodeType(num);
						if (textNodeType != XmlNodeType.None)
						{
							this.curNode.SetValueNode(textNodeType, this.ps.chars, num2, num3 - num2);
							return true;
						}
					}
				}
				else if (this.v1Compat)
				{
					do
					{
						if (num3 - num2 > 0)
						{
							this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
						}
					}
					while (!this.ParseText(out num2, out num3, ref num));
					if (num3 - num2 > 0)
					{
						this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
					}
					XmlNodeType textNodeType2 = this.GetTextNodeType(num);
					if (textNodeType2 != XmlNodeType.None)
					{
						this.curNode.SetValueNode(textNodeType2, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						return true;
					}
					this.stringBuilder.Length = 0;
				}
				else
				{
					if (num > 32)
					{
						this.curNode.SetValueNode(XmlNodeType.Text, this.ps.chars, num2, num3 - num2);
						this.nextParsingFunction = this.parsingFunction;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
						return true;
					}
					if (num3 - num2 > 0)
					{
						this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
					}
					bool flag;
					do
					{
						flag = this.ParseText(out num2, out num3, ref num);
						if (num3 - num2 > 0)
						{
							this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
						}
					}
					while (!flag && num <= 32 && this.stringBuilder.Length < 4096);
					XmlNodeType xmlNodeType = ((this.stringBuilder.Length < 4096) ? this.GetTextNodeType(num) : XmlNodeType.Text);
					if (xmlNodeType != XmlNodeType.None)
					{
						this.curNode.SetValueNode(xmlNodeType, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						if (!flag)
						{
							this.nextParsingFunction = this.parsingFunction;
							this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
						}
						return true;
					}
					this.stringBuilder.Length = 0;
					if (!flag)
					{
						while (!this.ParseText(out num2, out num3, ref num))
						{
						}
					}
				}
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReportEndEntity)
			{
				this.SetupEndEntityNodeInContent();
				this.parsingFunction = this.nextParsingFunction;
				return true;
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextNextParsingFunction;
				this.ParseEntityReference();
				return true;
			}
			return false;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001F838 File Offset: 0x0001DA38
		private bool ParseText(out int startPos, out int endPos, ref int outOrChars)
		{
			char[] array = this.ps.chars;
			int num = this.ps.charPos;
			int num2 = 0;
			int num3 = -1;
			int num4 = outOrChars;
			char c;
			int num7;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)(c = array[num])] & 64) == 0)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							num++;
							continue;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							break;
						case '\r':
							if (array[num + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (num - this.ps.charPos > 0)
									{
										if (num2 == 0)
										{
											num2 = 1;
											num3 = num;
										}
										else
										{
											this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
											num3 = num - num2;
											num2++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_0366;
								}
								if (!this.ps.eolNormalized)
								{
									array[num] = '\n';
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c == '&')
							{
								int num6;
								XmlTextReaderImpl.EntityType entityType;
								int num5;
								if ((num5 = this.ParseCharRefInline(num, out num6, out entityType)) > 0)
								{
									if (num2 > 0)
									{
										this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
									}
									num3 = num - num2;
									num2 += num5 - num - num6;
									num = num5;
									if (!this.xmlCharType.IsWhiteSpace(array[num5 - num6]) || (this.v1Compat && entityType == XmlTextReaderImpl.EntityType.CharacterDec))
									{
										num4 |= 255;
										continue;
									}
									continue;
								}
								else
								{
									if (num > this.ps.charPos)
									{
										goto IL_042F;
									}
									switch (this.HandleEntityReference(false, XmlTextReaderImpl.EntityExpandType.All, out num))
									{
									case XmlTextReaderImpl.EntityType.CharacterDec:
										if (!this.v1Compat)
										{
											goto IL_0221;
										}
										num4 |= 255;
										break;
									case XmlTextReaderImpl.EntityType.CharacterHex:
									case XmlTextReaderImpl.EntityType.CharacterNamed:
										goto IL_0221;
									case XmlTextReaderImpl.EntityType.Expanded:
									case XmlTextReaderImpl.EntityType.Skipped:
									case XmlTextReaderImpl.EntityType.FakeExpanded:
										goto IL_0249;
									case XmlTextReaderImpl.EntityType.Unexpanded:
										goto IL_01F4;
									default:
										goto IL_0249;
									}
									IL_0255:
									array = this.ps.chars;
									continue;
									IL_0249:
									num = this.ps.charPos;
									goto IL_0255;
									IL_0221:
									if (!this.xmlCharType.IsWhiteSpace(this.ps.chars[num - 1]))
									{
										num4 |= 255;
										goto IL_0255;
									}
									goto IL_0255;
								}
							}
							break;
						}
					}
					else
					{
						if (c == '<')
						{
							goto IL_042F;
						}
						if (c == ']')
						{
							if (this.ps.charsUsed - num >= 3 || this.ps.isEof)
							{
								if (array[num + 1] == ']' && array[num + 2] == '>')
								{
									this.Throw(num, "']]>' is not allowed in character data.");
								}
								num4 |= 93;
								num++;
								continue;
							}
							goto IL_0366;
						}
					}
					if (num != this.ps.charsUsed)
					{
						char c2 = array[num];
						if (XmlCharType.IsHighSurrogate((int)c2))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_0366;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)array[num]))
							{
								num++;
								num4 |= (int)c2;
								continue;
							}
						}
						num7 = num - this.ps.charPos;
						if (this.ZeroEndingStream(num))
						{
							goto Block_29;
						}
						this.ThrowInvalidChar(this.ps.chars, this.ps.charsUsed, this.ps.charPos + num7);
					}
					IL_0366:
					if (num > this.ps.charPos)
					{
						goto IL_042F;
					}
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r' && this.ps.chars[this.ps.charPos] != ']')
							{
								this.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							if (!this.InEntity)
							{
								goto IL_0423;
							}
							if (this.HandleEntityEnd(true))
							{
								goto Block_36;
							}
						}
					}
					num = this.ps.charPos;
					array = this.ps.chars;
				}
				else
				{
					num4 |= (int)c;
					num++;
				}
			}
			IL_01F4:
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.EntityReference;
			goto IL_0423;
			Block_29:
			array = this.ps.chars;
			num = this.ps.charPos + num7;
			goto IL_042F;
			Block_36:
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ReportEndEntity;
			IL_0423:
			startPos = (endPos = num);
			return true;
			IL_042F:
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
			}
			startPos = this.ps.charPos;
			endPos = num - num2;
			this.ps.charPos = num;
			outOrChars = num4;
			return c == '<';
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		private void FinishPartialValue()
		{
			this.curNode.CopyTo(this.readValueOffset, this.stringBuilder);
			int num = 0;
			int num2;
			int num3;
			while (!this.ParseText(out num2, out num3, ref num))
			{
				this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
			}
			this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
			this.curNode.SetValue(this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001FD48 File Offset: 0x0001DF48
		private void FinishOtherValueIterator()
		{
			switch (this.parsingFunction)
			{
			case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
				break;
			case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
				if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
				{
					this.FinishPartialValue();
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
					return;
				}
				if (this.readValueOffset > 0)
				{
					this.curNode.SetValue(this.curNode.StringValue.Substring(this.readValueOffset));
					this.readValueOffset = 0;
					return;
				}
				break;
			case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
			case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
				switch (this.incReadState)
				{
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue:
					if (this.readValueOffset > 0)
					{
						this.curNode.SetValue(this.curNode.StringValue.Substring(this.readValueOffset));
						this.readValueOffset = 0;
						return;
					}
					break;
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue:
					this.FinishPartialValue();
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue;
					return;
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End:
					this.curNode.SetValue(string.Empty);
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001FE34 File Offset: 0x0001E034
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SkipPartialTextValue()
		{
			int num = 0;
			this.parsingFunction = this.nextParsingFunction;
			int num2;
			int num3;
			while (!this.ParseText(out num2, out num3, ref num))
			{
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001FE5D File Offset: 0x0001E05D
		private void FinishReadValueChunk()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				this.SkipPartialTextValue();
				return;
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001FE90 File Offset: 0x0001E090
		private void FinishReadContentAsBinary()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
			{
				this.SkipPartialTextValue();
			}
			else
			{
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.nextNextParsingFunction;
			}
			if (this.incReadState != XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
			{
				while (this.MoveToNextContentNode(true))
				{
				}
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		private void FinishReadElementContentAsBinary()
		{
			this.FinishReadContentAsBinary();
			if (this.curNode.type != XmlNodeType.EndElement)
			{
				this.Throw("'{0}' is an invalid XmlNodeType.", this.curNode.type.ToString());
			}
			this.outerReader.Read();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001FF34 File Offset: 0x0001E134
		private bool ParseRootLevelWhitespace()
		{
			XmlNodeType whitespaceType = this.GetWhitespaceType();
			if (whitespaceType == XmlNodeType.None)
			{
				this.EatWhitespaces(null);
				if (this.ps.chars[this.ps.charPos] == '<' || this.ps.charsUsed - this.ps.charPos == 0 || this.ZeroEndingStream(this.ps.charPos))
				{
					return false;
				}
			}
			else
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				this.EatWhitespaces(this.stringBuilder);
				if (this.ps.chars[this.ps.charPos] == '<' || this.ps.charsUsed - this.ps.charPos == 0 || this.ZeroEndingStream(this.ps.charPos))
				{
					if (this.stringBuilder.Length > 0)
					{
						this.curNode.SetValueNode(whitespaceType, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						return true;
					}
					return false;
				}
			}
			if (this.xmlCharType.IsCharData(this.ps.chars[this.ps.charPos]))
			{
				this.Throw("Data at the root level is invalid.");
			}
			else
			{
				this.ThrowInvalidChar(this.ps.chars, this.ps.charsUsed, this.ps.charPos);
			}
			return false;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000200A4 File Offset: 0x0001E2A4
		private void ParseEntityReference()
		{
			this.ps.charPos = this.ps.charPos + 1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			this.curNode.SetNamedNode(XmlNodeType.EntityReference, this.ParseEntityName());
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000200F4 File Offset: 0x0001E2F4
		private XmlTextReaderImpl.EntityType HandleEntityReference(bool isInAttributeValue, XmlTextReaderImpl.EntityExpandType expandType, out int charRefEndPos)
		{
			if (this.ps.charPos + 1 == this.ps.charsUsed && this.ReadData() == 0)
			{
				this.Throw("Unexpected end of file has occurred.");
			}
			if (this.ps.chars[this.ps.charPos + 1] == '#')
			{
				XmlTextReaderImpl.EntityType entityType;
				charRefEndPos = this.ParseNumericCharRef(expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null, out entityType);
				return entityType;
			}
			charRefEndPos = this.ParseNamedCharRef(expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null);
			if (charRefEndPos >= 0)
			{
				return XmlTextReaderImpl.EntityType.CharacterNamed;
			}
			if (expandType == XmlTextReaderImpl.EntityExpandType.OnlyCharacter || (this.entityHandling != EntityHandling.ExpandEntities && (!isInAttributeValue || !this.validatingReaderCompatFlag)))
			{
				return XmlTextReaderImpl.EntityType.Unexpanded;
			}
			this.ps.charPos = this.ps.charPos + 1;
			int linePos = this.ps.LinePos;
			int num;
			try
			{
				num = this.ParseName();
			}
			catch (XmlException)
			{
				this.Throw("An error occurred while parsing EntityName.", this.ps.LineNo, linePos);
				return XmlTextReaderImpl.EntityType.Skipped;
			}
			if (this.ps.chars[num] != ';')
			{
				this.ThrowUnexpectedToken(num, ";");
			}
			int linePos2 = this.ps.LinePos;
			string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			this.ps.charPos = num + 1;
			charRefEndPos = -1;
			XmlTextReaderImpl.EntityType entityType2 = this.HandleGeneralEntityReference(text, isInAttributeValue, false, linePos2);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
			return entityType2;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00020280 File Offset: 0x0001E480
		private XmlTextReaderImpl.EntityType HandleGeneralEntityReference(string name, bool isInAttributeValue, bool pushFakeEntityIfNullResolver, int entityStartLinePos)
		{
			IDtdEntityInfo dtdEntityInfo = null;
			if (this.dtdInfo == null && this.fragmentParserContext != null && this.fragmentParserContext.HasDtdInfo && this.dtdProcessing == DtdProcessing.Parse)
			{
				this.ParseDtdFromParserContext();
			}
			if (this.dtdInfo == null || (dtdEntityInfo = this.dtdInfo.LookupEntity(name)) == null)
			{
				if (this.disableUndeclaredEntityCheck)
				{
					dtdEntityInfo = new SchemaEntity(new XmlQualifiedName(name), false)
					{
						Text = string.Empty
					};
				}
				else
				{
					this.Throw("Reference to undeclared entity '{0}'.", name, this.ps.LineNo, entityStartLinePos);
				}
			}
			if (dtdEntityInfo.IsUnparsedEntity)
			{
				if (this.disableUndeclaredEntityCheck)
				{
					dtdEntityInfo = new SchemaEntity(new XmlQualifiedName(name), false)
					{
						Text = string.Empty
					};
				}
				else
				{
					this.Throw("Reference to unparsed entity '{0}'.", name, this.ps.LineNo, entityStartLinePos);
				}
			}
			if (this.standalone && dtdEntityInfo.IsDeclaredInExternal)
			{
				this.Throw("Standalone document declaration must have a value of 'no' because an external entity '{0}' is referenced.", dtdEntityInfo.Name, this.ps.LineNo, entityStartLinePos);
			}
			if (dtdEntityInfo.IsExternal)
			{
				if (isInAttributeValue)
				{
					this.Throw("External entity '{0}' reference cannot appear in the attribute value.", name, this.ps.LineNo, entityStartLinePos);
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				if (this.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
				{
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				if (this.IsResolverNull)
				{
					if (pushFakeEntityIfNullResolver)
					{
						this.PushExternalEntity(dtdEntityInfo);
						this.curNode.entityId = this.ps.entityId;
						return XmlTextReaderImpl.EntityType.FakeExpanded;
					}
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				else
				{
					this.PushExternalEntity(dtdEntityInfo);
					this.curNode.entityId = this.ps.entityId;
					if (!isInAttributeValue || !this.validatingReaderCompatFlag)
					{
						return XmlTextReaderImpl.EntityType.Expanded;
					}
					return XmlTextReaderImpl.EntityType.ExpandedInAttribute;
				}
			}
			else
			{
				if (this.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
				{
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				this.PushInternalEntity(dtdEntityInfo);
				this.curNode.entityId = this.ps.entityId;
				if (!isInAttributeValue || !this.validatingReaderCompatFlag)
				{
					return XmlTextReaderImpl.EntityType.Expanded;
				}
				return XmlTextReaderImpl.EntityType.ExpandedInAttribute;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00020443 File Offset: 0x0001E643
		private bool InEntity
		{
			get
			{
				return this.parsingStatesStackTop >= 0;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00020454 File Offset: 0x0001E654
		private bool HandleEntityEnd(bool checkEntityNesting)
		{
			if (this.parsingStatesStackTop == -1)
			{
				this.Throw("An internal error has occurred.");
			}
			if (this.ps.entityResolvedManually)
			{
				this.index--;
				if (checkEntityNesting && this.ps.entityId != this.nodes[this.index].entityId)
				{
					this.Throw("Incomplete entity contents.");
				}
				this.lastEntity = this.ps.entity;
				this.PopEntity();
				return true;
			}
			if (checkEntityNesting && this.ps.entityId != this.nodes[this.index].entityId)
			{
				this.Throw("Incomplete entity contents.");
			}
			this.PopEntity();
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			return false;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0002052C File Offset: 0x0001E72C
		private void SetupEndEntityNodeInContent()
		{
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.curNode = this.nodes[this.index];
			this.curNode.SetNamedNode(XmlNodeType.EndEntity, this.lastEntity.Name);
			this.curNode.lineInfo.Set(this.ps.lineNo, this.ps.LinePos - 1);
			if (this.index == 0 && this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ElementContent)
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000205C8 File Offset: 0x0001E7C8
		private void SetupEndEntityNodeInAttribute()
		{
			this.curNode = this.nodes[this.index + this.attrCount + 1];
			XmlTextReaderImpl.NodeData nodeData = this.curNode;
			nodeData.lineInfo.linePos = nodeData.lineInfo.linePos + this.curNode.localName.Length;
			this.curNode.type = XmlNodeType.EndEntity;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00020622 File Offset: 0x0001E822
		private bool ParsePI()
		{
			return this.ParsePI(null);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0002062C File Offset: 0x0001E82C
		private bool ParsePI(StringBuilder piInDtdStringBuilder)
		{
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			}
			int num = this.ParseName();
			string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			if (string.Compare(text, "xml", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.Throw(text.Equals("xml") ? "Unexpected XML declaration. The XML declaration must be the first node in the document, and no white space characters are allowed to appear before it." : "'{0}' is an invalid name for processing instructions.", text);
			}
			this.ps.charPos = num;
			if (piInDtdStringBuilder == null)
			{
				if (!this.ignorePIs && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
				{
					this.curNode.SetNamedNode(XmlNodeType.ProcessingInstruction, text);
				}
			}
			else
			{
				piInDtdStringBuilder.Append(text);
			}
			char c = this.ps.chars[this.ps.charPos];
			if (this.EatWhitespaces(piInDtdStringBuilder) == 0)
			{
				if (this.ps.charsUsed - this.ps.charPos < 2)
				{
					this.ReadData();
				}
				if (c != '?' || this.ps.chars[this.ps.charPos + 1] != '>')
				{
					this.Throw("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(this.ps.chars, this.ps.charsUsed, this.ps.charPos));
				}
			}
			int num2;
			int num3;
			if (this.ParsePIValue(out num2, out num3))
			{
				if (piInDtdStringBuilder == null)
				{
					if (this.ignorePIs)
					{
						return false;
					}
					if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
					{
						this.curNode.SetValue(this.ps.chars, num2, num3 - num2);
					}
				}
				else
				{
					piInDtdStringBuilder.Append(this.ps.chars, num2, num3 - num2);
				}
			}
			else
			{
				StringBuilder stringBuilder;
				if (piInDtdStringBuilder == null)
				{
					if (this.ignorePIs || this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
					{
						while (!this.ParsePIValue(out num2, out num3))
						{
						}
						return false;
					}
					stringBuilder = this.stringBuilder;
				}
				else
				{
					stringBuilder = piInDtdStringBuilder;
				}
				do
				{
					stringBuilder.Append(this.ps.chars, num2, num3 - num2);
				}
				while (!this.ParsePIValue(out num2, out num3));
				stringBuilder.Append(this.ps.chars, num2, num3 - num2);
				if (piInDtdStringBuilder == null)
				{
					this.curNode.SetValue(this.stringBuilder.ToString());
					this.stringBuilder.Length = 0;
				}
			}
			return true;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00020884 File Offset: 0x0001EA84
		private bool ParsePIValue(out int outStartPos, out int outEndPos)
		{
			if (this.ps.charsUsed - this.ps.charPos < 2 && this.ReadData() == 0)
			{
				this.Throw(this.ps.charsUsed, "Unexpected end of file while parsing {0} has occurred.", "PI");
			}
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = 0;
			int num3 = -1;
			for (;;)
			{
				char c;
				if ((this.xmlCharType.charProperties[(int)(c = chars[num])] & 64) == 0 || c == '?')
				{
					char c2 = chars[num];
					if (c2 <= '&')
					{
						switch (c2)
						{
						case '\t':
							break;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							goto IL_01F0;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (num - this.ps.charPos > 0)
									{
										if (num2 == 0)
										{
											num2 = 1;
											num3 = num;
										}
										else
										{
											this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
											num3 = num - num2;
											num2++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_0247;
								}
								if (!this.ps.eolNormalized)
								{
									chars[num] = '\n';
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c2 != '&')
							{
								goto IL_01F0;
							}
							break;
						}
					}
					else if (c2 != '<')
					{
						if (c2 != '?')
						{
							if (c2 != ']')
							{
								goto IL_01F0;
							}
						}
						else
						{
							if (chars[num + 1] == '>')
							{
								break;
							}
							if (num + 1 != this.ps.charsUsed)
							{
								num++;
								continue;
							}
							goto IL_0247;
						}
					}
					num++;
					continue;
					IL_01F0:
					if (num == this.ps.charsUsed)
					{
						goto IL_0247;
					}
					if (XmlCharType.IsHighSurrogate((int)chars[num]))
					{
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_0247;
						}
						num++;
						if (XmlCharType.IsLowSurrogate((int)chars[num]))
						{
							num++;
							continue;
						}
					}
					this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
				}
				else
				{
					num++;
				}
			}
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num + 2;
			return true;
			IL_0247:
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num;
			return false;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00020B10 File Offset: 0x0001ED10
		private bool ParseComment()
		{
			if (this.ignoreComments)
			{
				XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
				this.ParseCDataOrComment(XmlNodeType.Comment);
				this.parsingMode = parsingMode;
				return false;
			}
			this.ParseCDataOrComment(XmlNodeType.Comment);
			return true;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00020B4B File Offset: 0x0001ED4B
		private void ParseCData()
		{
			this.ParseCDataOrComment(XmlNodeType.CDATA);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00020B54 File Offset: 0x0001ED54
		private void ParseCDataOrComment(XmlNodeType type)
		{
			int num;
			int num2;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				while (!this.ParseCDataOrComment(type, out num, out num2))
				{
				}
				return;
			}
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			if (this.ParseCDataOrComment(type, out num, out num2))
			{
				this.curNode.SetValueNode(type, this.ps.chars, num, num2 - num);
				return;
			}
			do
			{
				this.stringBuilder.Append(this.ps.chars, num, num2 - num);
			}
			while (!this.ParseCDataOrComment(type, out num, out num2));
			this.stringBuilder.Append(this.ps.chars, num, num2 - num);
			this.curNode.SetValueNode(type, this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00020C2C File Offset: 0x0001EE2C
		private bool ParseCDataOrComment(XmlNodeType type, out int outStartPos, out int outEndPos)
		{
			if (this.ps.charsUsed - this.ps.charPos < 3 && this.ReadData() == 0)
			{
				this.Throw("Unexpected end of file while parsing {0} has occurred.", (type == XmlNodeType.Comment) ? "Comment" : "CDATA");
			}
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = 0;
			int num3 = -1;
			char c = ((type == XmlNodeType.Comment) ? '-' : ']');
			for (;;)
			{
				char c2;
				if ((this.xmlCharType.charProperties[(int)(c2 = chars[num])] & 64) == 0 || c2 == c)
				{
					if (chars[num] == c)
					{
						if (chars[num + 1] == c)
						{
							if (chars[num + 2] == '>')
							{
								break;
							}
							if (num + 2 == this.ps.charsUsed)
							{
								goto IL_027D;
							}
							if (type == XmlNodeType.Comment)
							{
								this.Throw(num, "An XML comment cannot contain '--', and '-' cannot be the last character.");
							}
						}
						else if (num + 1 == this.ps.charsUsed)
						{
							goto IL_027D;
						}
						num++;
					}
					else
					{
						char c3 = chars[num];
						if (c3 <= '&')
						{
							switch (c3)
							{
							case '\t':
								break;
							case '\n':
								num++;
								this.OnNewLine(num);
								continue;
							case '\v':
							case '\f':
								goto IL_022B;
							case '\r':
								if (chars[num + 1] == '\n')
								{
									if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
									{
										if (num - this.ps.charPos > 0)
										{
											if (num2 == 0)
											{
												num2 = 1;
												num3 = num;
											}
											else
											{
												this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
												num3 = num - num2;
												num2++;
											}
										}
										else
										{
											this.ps.charPos = this.ps.charPos + 1;
										}
									}
									num += 2;
								}
								else
								{
									if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
									{
										goto IL_027D;
									}
									if (!this.ps.eolNormalized)
									{
										chars[num] = '\n';
									}
									num++;
								}
								this.OnNewLine(num);
								continue;
							default:
								if (c3 != '&')
								{
									goto IL_022B;
								}
								break;
							}
						}
						else if (c3 != '<' && c3 != ']')
						{
							goto IL_022B;
						}
						num++;
						continue;
						IL_022B:
						if (num == this.ps.charsUsed)
						{
							goto IL_027D;
						}
						if (!XmlCharType.IsHighSurrogate((int)chars[num]))
						{
							goto IL_026A;
						}
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_027D;
						}
						num++;
						if (!XmlCharType.IsLowSurrogate((int)chars[num]))
						{
							goto IL_026A;
						}
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num + 3;
			return true;
			IL_026A:
			this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
			IL_027D:
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num;
			return false;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		private bool ParseDoctypeDecl()
		{
			if (this.dtdProcessing == DtdProcessing.Prohibit)
			{
				this.ThrowWithoutLineInfo(this.v1Compat ? "DTD is prohibited in this XML document." : "For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.");
			}
			while (this.ps.charsUsed - this.ps.charPos < 8)
			{
				if (this.ReadData() == 0)
				{
					this.Throw("Unexpected end of file while parsing {0} has occurred.", "DOCTYPE");
				}
			}
			if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 7, "DOCTYPE"))
			{
				this.ThrowUnexpectedToken((!this.rootElementParsed && this.dtdInfo == null) ? "DOCTYPE" : "<!--");
			}
			if (!this.xmlCharType.IsWhiteSpace(this.ps.chars[this.ps.charPos + 7]))
			{
				this.ThrowExpectingWhitespace(this.ps.charPos + 7);
			}
			if (this.dtdInfo != null)
			{
				this.Throw(this.ps.charPos - 2, "Cannot have multiple DTDs.");
			}
			if (this.rootElementParsed)
			{
				this.Throw(this.ps.charPos - 2, "DTD must be defined before the document root element.");
			}
			this.ps.charPos = this.ps.charPos + 8;
			this.EatWhitespaces(null);
			if (this.dtdProcessing == DtdProcessing.Parse)
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				this.ParseDtd();
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
				return true;
			}
			this.SkipDtd();
			return false;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00021078 File Offset: 0x0001F278
		private void ParseDtd()
		{
			IDtdParser dtdParser = DtdParser.Create();
			this.dtdInfo = dtdParser.ParseInternalDtd(new XmlTextReaderImpl.DtdParserProxy(this), true);
			if ((this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
			this.curNode.SetNamedNode(XmlNodeType.DocumentType, this.dtdInfo.Name.ToString(), string.Empty, null);
			this.curNode.SetValue(this.dtdInfo.InternalDtdSubset);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00021108 File Offset: 0x0001F308
		private void SkipDtd()
		{
			int num2;
			int num = this.ParseQName(out num2);
			this.ps.charPos = num;
			this.EatWhitespaces(null);
			if (this.ps.chars[this.ps.charPos] == 'P')
			{
				while (this.ps.charsUsed - this.ps.charPos < 6)
				{
					if (this.ReadData() == 0)
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 6, "PUBLIC"))
				{
					this.ThrowUnexpectedToken("PUBLIC");
				}
				this.ps.charPos = this.ps.charPos + 6;
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				this.EatWhitespaces(null);
			}
			else if (this.ps.chars[this.ps.charPos] == 'S')
			{
				while (this.ps.charsUsed - this.ps.charPos < 6)
				{
					if (this.ReadData() == 0)
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 6, "SYSTEM"))
				{
					this.ThrowUnexpectedToken("SYSTEM");
				}
				this.ps.charPos = this.ps.charPos + 6;
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				this.EatWhitespaces(null);
			}
			else if (this.ps.chars[this.ps.charPos] != '[' && this.ps.chars[this.ps.charPos] != '>')
			{
				this.Throw("Expecting external ID, '[' or '>'.");
			}
			if (this.ps.chars[this.ps.charPos] == '[')
			{
				this.ps.charPos = this.ps.charPos + 1;
				this.SkipUntil(']', true);
				this.EatWhitespaces(null);
				if (this.ps.chars[this.ps.charPos] != '>')
				{
					this.ThrowUnexpectedToken(">");
				}
			}
			else if (this.ps.chars[this.ps.charPos] == '>')
			{
				this.curNode.SetValue(string.Empty);
			}
			else
			{
				this.Throw("Expecting an internal subset or the end of the DOCTYPE declaration.");
			}
			this.ps.charPos = this.ps.charPos + 1;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000213AC File Offset: 0x0001F5AC
		private void SkipPublicOrSystemIdLiteral()
		{
			char c = this.ps.chars[this.ps.charPos];
			if (c != '"' && c != '\'')
			{
				this.ThrowUnexpectedToken("\"", "'");
			}
			this.ps.charPos = this.ps.charPos + 1;
			this.SkipUntil(c, false);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00021404 File Offset: 0x0001F604
		private void SkipUntil(char stopChar, bool recognizeLiterals)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			char c = '"';
			char[] array = this.ps.chars;
			int num = this.ps.charPos;
			for (;;)
			{
				char c2;
				if ((this.xmlCharType.charProperties[(int)(c2 = array[num])] & 128) == 0 || array[num] == stopChar || c2 == '-' || c2 == '?')
				{
					if (c2 == stopChar && !flag)
					{
						break;
					}
					this.ps.charPos = num;
					if (c2 <= '&')
					{
						switch (c2)
						{
						case '\t':
							break;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							goto IL_02D1;
						case '\r':
							if (array[num + 1] == '\n')
							{
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_032F;
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c2 == '"')
							{
								goto IL_02AC;
							}
							if (c2 != '&')
							{
								goto IL_02D1;
							}
							break;
						}
					}
					else if (c2 <= '-')
					{
						if (c2 == '\'')
						{
							goto IL_02AC;
						}
						if (c2 != '-')
						{
							goto IL_02D1;
						}
						if (flag2)
						{
							if (num + 2 >= this.ps.charsUsed && !this.ps.isEof)
							{
								goto IL_032F;
							}
							if (array[num + 1] == '-' && array[num + 2] == '>')
							{
								flag2 = false;
								num += 2;
								continue;
							}
						}
						num++;
						continue;
					}
					else
					{
						switch (c2)
						{
						case '<':
							if (array[num + 1] == '?')
							{
								if (recognizeLiterals && !flag && !flag2)
								{
									flag3 = true;
									num += 2;
									continue;
								}
							}
							else if (array[num + 1] == '!')
							{
								if (num + 3 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_032F;
								}
								if (array[num + 2] == '-' && array[num + 3] == '-' && recognizeLiterals && !flag && !flag3)
								{
									flag2 = true;
									num += 4;
									continue;
								}
							}
							else if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
							{
								goto IL_032F;
							}
							num++;
							continue;
						case '=':
							goto IL_02D1;
						case '>':
							break;
						case '?':
							if (flag3)
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_032F;
								}
								if (array[num + 1] == '>')
								{
									flag3 = false;
									num++;
									continue;
								}
							}
							num++;
							continue;
						default:
							if (c2 != ']')
							{
								goto IL_02D1;
							}
							break;
						}
					}
					num++;
					continue;
					IL_02AC:
					if (flag)
					{
						if (c == c2)
						{
							flag = false;
						}
					}
					else if (recognizeLiterals && !flag2 && !flag3)
					{
						flag = true;
						c = c2;
					}
					num++;
					continue;
					IL_02D1:
					if (num != this.ps.charsUsed)
					{
						if (XmlCharType.IsHighSurrogate((int)array[num]))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_032F;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)array[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(array, this.ps.charsUsed, num);
					}
					IL_032F:
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r')
							{
								this.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							this.Throw("Unexpected end of file has occurred.");
						}
					}
					array = this.ps.chars;
					num = this.ps.charPos;
				}
				else
				{
					num++;
				}
			}
			this.ps.charPos = num + 1;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000217B4 File Offset: 0x0001F9B4
		private int EatWhitespaces(StringBuilder sb)
		{
			int num = this.ps.charPos;
			int num2 = 0;
			char[] array = this.ps.chars;
			for (;;)
			{
				char c = array[num];
				switch (c)
				{
				case '\t':
					break;
				case '\n':
					num++;
					this.OnNewLine(num);
					continue;
				case '\v':
				case '\f':
					goto IL_00FE;
				case '\r':
					if (array[num + 1] == '\n')
					{
						int num3 = num - this.ps.charPos;
						if (sb != null && !this.ps.eolNormalized)
						{
							if (num3 > 0)
							{
								sb.Append(array, this.ps.charPos, num3);
								num2 += num3;
							}
							this.ps.charPos = num + 1;
						}
						num += 2;
					}
					else
					{
						if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
						{
							goto IL_0155;
						}
						if (!this.ps.eolNormalized)
						{
							array[num] = '\n';
						}
						num++;
					}
					this.OnNewLine(num);
					continue;
				default:
					if (c != ' ')
					{
						goto IL_00FE;
					}
					break;
				}
				num++;
				continue;
				IL_0155:
				int num4 = num - this.ps.charPos;
				if (num4 > 0)
				{
					if (sb != null)
					{
						sb.Append(this.ps.chars, this.ps.charPos, num4);
					}
					this.ps.charPos = num;
					num2 += num4;
				}
				if (this.ReadData() == 0)
				{
					if (this.ps.charsUsed - this.ps.charPos == 0)
					{
						return num2;
					}
					if (this.ps.chars[this.ps.charPos] != '\r')
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				num = this.ps.charPos;
				array = this.ps.chars;
				continue;
				IL_00FE:
				if (num != this.ps.charsUsed)
				{
					break;
				}
				goto IL_0155;
			}
			int num5 = num - this.ps.charPos;
			if (num5 > 0)
			{
				if (sb != null)
				{
					sb.Append(this.ps.chars, this.ps.charPos, num5);
				}
				this.ps.charPos = num;
				num2 += num5;
			}
			return num2;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000219BE File Offset: 0x0001FBBE
		private int ParseCharRefInline(int startPos, out int charCount, out XmlTextReaderImpl.EntityType entityType)
		{
			if (this.ps.chars[startPos + 1] == '#')
			{
				return this.ParseNumericCharRefInline(startPos, true, null, out charCount, out entityType);
			}
			charCount = 1;
			entityType = XmlTextReaderImpl.EntityType.CharacterNamed;
			return this.ParseNamedCharRefInline(startPos, true, null);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000219F0 File Offset: 0x0001FBF0
		private int ParseNumericCharRef(bool expand, StringBuilder internalSubsetBuilder, out XmlTextReaderImpl.EntityType entityType)
		{
			int num2;
			int num;
			while ((num = this.ParseNumericCharRefInline(this.ps.charPos, expand, internalSubsetBuilder, out num2, out entityType)) == -2)
			{
				if (this.ReadData() == 0)
				{
					this.Throw("Unexpected end of file while parsing {0} has occurred.");
				}
			}
			if (expand)
			{
				this.ps.charPos = num - num2;
			}
			return num;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00021A40 File Offset: 0x0001FC40
		private int ParseNumericCharRefInline(int startPos, bool expand, StringBuilder internalSubsetBuilder, out int charCount, out XmlTextReaderImpl.EntityType entityType)
		{
			int num = 0;
			string text = null;
			char[] chars = this.ps.chars;
			int num2 = startPos + 2;
			charCount = 0;
			int num3 = 0;
			try
			{
				if (chars[num2] == 'x')
				{
					num2++;
					num3 = num2;
					text = "Invalid syntax for a hexadecimal numeric entity reference.";
					for (;;)
					{
						char c = chars[num2];
						checked
						{
							if (c >= '0' && c <= '9')
							{
								num = num * 16 + (int)c - 48;
							}
							else if (c >= 'a' && c <= 'f')
							{
								num = num * 16 + 10 + (int)c - 97;
							}
							else
							{
								if (c < 'A' || c > 'F')
								{
									break;
								}
								num = num * 16 + 10 + (int)c - 65;
							}
						}
						num2++;
					}
					entityType = XmlTextReaderImpl.EntityType.CharacterHex;
				}
				else
				{
					if (num2 >= this.ps.charsUsed)
					{
						entityType = XmlTextReaderImpl.EntityType.Skipped;
						return -2;
					}
					num3 = num2;
					text = "Invalid syntax for a decimal numeric entity reference.";
					while (chars[num2] >= '0' && chars[num2] <= '9')
					{
						num = checked(num * 10 + (int)chars[num2] - 48);
						num2++;
					}
					entityType = XmlTextReaderImpl.EntityType.CharacterDec;
				}
			}
			catch (OverflowException ex)
			{
				this.ps.charPos = num2;
				entityType = XmlTextReaderImpl.EntityType.Skipped;
				this.Throw("Invalid value of a character entity reference.", null, ex);
			}
			if (chars[num2] != ';' || num3 == num2)
			{
				if (num2 == this.ps.charsUsed)
				{
					return -2;
				}
				this.Throw(num2, text);
			}
			if (num <= 65535)
			{
				char c2 = (char)num;
				if (!this.xmlCharType.IsCharData(c2) && ((this.v1Compat && this.normalize) || (!this.v1Compat && this.checkCharacters)))
				{
					this.Throw((this.ps.chars[startPos + 2] == 'x') ? (startPos + 3) : (startPos + 2), "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(c2, '\0'));
				}
				if (expand)
				{
					if (internalSubsetBuilder != null)
					{
						internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num2 - this.ps.charPos + 1);
					}
					chars[num2] = c2;
				}
				charCount = 1;
				return num2 + 1;
			}
			char c3;
			char c4;
			XmlCharType.SplitSurrogateChar(num, out c3, out c4);
			if (this.normalize && (!XmlCharType.IsHighSurrogate((int)c4) || !XmlCharType.IsLowSurrogate((int)c3)))
			{
				this.Throw((this.ps.chars[startPos + 2] == 'x') ? (startPos + 3) : (startPos + 2), "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(c4, c3));
			}
			if (expand)
			{
				if (internalSubsetBuilder != null)
				{
					internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num2 - this.ps.charPos + 1);
				}
				chars[num2 - 1] = c4;
				chars[num2] = c3;
			}
			charCount = 2;
			return num2 + 1;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00021CC8 File Offset: 0x0001FEC8
		private int ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
		{
			int num2;
			int num;
			for (;;)
			{
				num = (num2 = this.ParseNamedCharRefInline(this.ps.charPos, expand, internalSubsetBuilder));
				if (num2 != -2)
				{
					break;
				}
				if (this.ReadData() == 0)
				{
					return -1;
				}
			}
			if (num2 == -1)
			{
				return -1;
			}
			if (expand)
			{
				this.ps.charPos = num - 1;
			}
			return num;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00021D14 File Offset: 0x0001FF14
		private int ParseNamedCharRefInline(int startPos, bool expand, StringBuilder internalSubsetBuilder)
		{
			int num = startPos + 1;
			char[] chars = this.ps.chars;
			char c = chars[num];
			if (c <= 'g')
			{
				if (c != 'a')
				{
					if (c == 'g')
					{
						if (this.ps.charsUsed - num < 3)
						{
							return -2;
						}
						if (chars[num + 1] == 't' && chars[num + 2] == ';')
						{
							num += 3;
							char c2 = '>';
							goto IL_0175;
						}
						return -1;
					}
				}
				else
				{
					num++;
					if (chars[num] == 'm')
					{
						if (this.ps.charsUsed - num < 3)
						{
							return -2;
						}
						if (chars[num + 1] == 'p' && chars[num + 2] == ';')
						{
							num += 3;
							char c2 = '&';
							goto IL_0175;
						}
						return -1;
					}
					else if (chars[num] == 'p')
					{
						if (this.ps.charsUsed - num < 4)
						{
							return -2;
						}
						if (chars[num + 1] == 'o' && chars[num + 2] == 's' && chars[num + 3] == ';')
						{
							num += 4;
							char c2 = '\'';
							goto IL_0175;
						}
						return -1;
					}
					else
					{
						if (num < this.ps.charsUsed)
						{
							return -1;
						}
						return -2;
					}
				}
			}
			else if (c != 'l')
			{
				if (c == 'q')
				{
					if (this.ps.charsUsed - num < 5)
					{
						return -2;
					}
					if (chars[num + 1] == 'u' && chars[num + 2] == 'o' && chars[num + 3] == 't' && chars[num + 4] == ';')
					{
						num += 5;
						char c2 = '"';
						goto IL_0175;
					}
					return -1;
				}
			}
			else
			{
				if (this.ps.charsUsed - num < 3)
				{
					return -2;
				}
				if (chars[num + 1] == 't' && chars[num + 2] == ';')
				{
					num += 3;
					char c2 = '<';
					goto IL_0175;
				}
				return -1;
			}
			return -1;
			IL_0175:
			if (expand)
			{
				if (internalSubsetBuilder != null)
				{
					internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
				}
				char c2;
				this.ps.chars[num - 1] = c2;
			}
			return num;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00021ED8 File Offset: 0x000200D8
		private int ParseName()
		{
			int num;
			return this.ParseQName(false, 0, out num);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00021EEF File Offset: 0x000200EF
		private int ParseQName(out int colonPos)
		{
			return this.ParseQName(true, 0, out colonPos);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00021EFC File Offset: 0x000200FC
		private int ParseQName(bool isQName, int startOffset, out int colonPos)
		{
			int num = -1;
			int num2 = this.ps.charPos + startOffset;
			for (;;)
			{
				char[] array = this.ps.chars;
				if ((this.xmlCharType.charProperties[(int)array[num2]] & 4) != 0)
				{
					num2++;
				}
				else
				{
					if (num2 + 1 >= this.ps.charsUsed)
					{
						if (this.ReadDataInName(ref num2))
						{
							continue;
						}
						this.Throw(num2, "Unexpected end of file while parsing {0} has occurred.", "Name");
					}
					if (array[num2] != ':' || this.supportNamespaces)
					{
						this.Throw(num2, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(array, this.ps.charsUsed, num2));
					}
				}
				for (;;)
				{
					if ((this.xmlCharType.charProperties[(int)array[num2]] & 8) != 0)
					{
						num2++;
					}
					else if (array[num2] == ':')
					{
						if (this.supportNamespaces)
						{
							break;
						}
						num = num2 - this.ps.charPos;
						num2++;
					}
					else
					{
						if (num2 != this.ps.charsUsed)
						{
							goto IL_0135;
						}
						if (!this.ReadDataInName(ref num2))
						{
							goto IL_0124;
						}
						array = this.ps.chars;
					}
				}
				if (num != -1 || !isQName)
				{
					this.Throw(num2, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				}
				num = num2 - this.ps.charPos;
				num2++;
			}
			IL_0124:
			this.Throw(num2, "Unexpected end of file while parsing {0} has occurred.", "Name");
			IL_0135:
			colonPos = ((num == -1) ? (-1) : (this.ps.charPos + num));
			return num2;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00022058 File Offset: 0x00020258
		private bool ReadDataInName(ref int pos)
		{
			int num = pos - this.ps.charPos;
			bool flag = this.ReadData() != 0;
			pos = this.ps.charPos + num;
			return flag;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0002208C File Offset: 0x0002028C
		private string ParseEntityName()
		{
			int num;
			try
			{
				num = this.ParseName();
			}
			catch (XmlException)
			{
				this.Throw("An error occurred while parsing EntityName.");
				return null;
			}
			if (this.ps.chars[num] != ';')
			{
				this.Throw("An error occurred while parsing EntityName.");
			}
			string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			this.ps.charPos = num + 1;
			return text;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002211C File Offset: 0x0002031C
		private XmlTextReaderImpl.NodeData AddNode(int nodeIndex, int nodeDepth)
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[nodeIndex];
			if (nodeData != null)
			{
				nodeData.depth = nodeDepth;
				return nodeData;
			}
			return this.AllocNode(nodeIndex, nodeDepth);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00022148 File Offset: 0x00020348
		private XmlTextReaderImpl.NodeData AllocNode(int nodeIndex, int nodeDepth)
		{
			if (nodeIndex >= this.nodes.Length - 1)
			{
				XmlTextReaderImpl.NodeData[] array = new XmlTextReaderImpl.NodeData[this.nodes.Length * 2];
				Array.Copy(this.nodes, 0, array, 0, this.nodes.Length);
				this.nodes = array;
			}
			XmlTextReaderImpl.NodeData nodeData = this.nodes[nodeIndex];
			if (nodeData == null)
			{
				nodeData = new XmlTextReaderImpl.NodeData();
				this.nodes[nodeIndex] = nodeData;
			}
			nodeData.depth = nodeDepth;
			return nodeData;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000221B2 File Offset: 0x000203B2
		private XmlTextReaderImpl.NodeData AddAttributeNoChecks(string name, int attrDepth)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, attrDepth);
			nodeData.SetNamedNode(XmlNodeType.Attribute, this.nameTable.Add(name));
			this.attrCount++;
			return nodeData;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000221EC File Offset: 0x000203EC
		private XmlTextReaderImpl.NodeData AddAttribute(int endNamePos, int colonPos)
		{
			if (colonPos == -1 || !this.supportNamespaces)
			{
				string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, endNamePos - this.ps.charPos);
				return this.AddAttribute(text, string.Empty, text);
			}
			this.attrNeedNamespaceLookup = true;
			int charPos = this.ps.charPos;
			int num = colonPos - charPos;
			if (num == this.lastPrefix.Length && XmlConvert.StrEqual(this.ps.chars, charPos, num, this.lastPrefix))
			{
				return this.AddAttribute(this.nameTable.Add(this.ps.chars, colonPos + 1, endNamePos - colonPos - 1), this.lastPrefix, null);
			}
			string text2 = this.nameTable.Add(this.ps.chars, charPos, num);
			this.lastPrefix = text2;
			return this.AddAttribute(this.nameTable.Add(this.ps.chars, colonPos + 1, endNamePos - colonPos - 1), text2, null);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000222F4 File Offset: 0x000204F4
		private XmlTextReaderImpl.NodeData AddAttribute(string localName, string prefix, string nameWPrefix)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, this.index + 1);
			nodeData.SetNamedNode(XmlNodeType.Attribute, localName, prefix, nameWPrefix);
			int num = 1 << (int)localName[0];
			if ((this.attrHashtable & num) == 0)
			{
				this.attrHashtable |= num;
			}
			else if (this.attrDuplWalkCount < 250)
			{
				this.attrDuplWalkCount++;
				for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
				{
					if (Ref.Equal(this.nodes[i].localName, nodeData.localName))
					{
						this.attrDuplWalkCount = 250;
						break;
					}
				}
			}
			this.attrCount++;
			return nodeData;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x000223C5 File Offset: 0x000205C5
		private void PopElementContext()
		{
			this.namespaceManager.PopScope();
			if (this.curNode.xmlContextPushed)
			{
				this.PopXmlContext();
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000223E6 File Offset: 0x000205E6
		private void OnNewLine(int pos)
		{
			this.ps.lineNo = this.ps.lineNo + 1;
			this.ps.lineStartPos = pos - 1;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00022408 File Offset: 0x00020608
		private void OnEof()
		{
			this.curNode = this.nodes[0];
			this.curNode.Clear(XmlNodeType.None);
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.Eof;
			this.readState = ReadState.EndOfFile;
			this.reportedEncoding = null;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00022468 File Offset: 0x00020668
		private string LookupNamespace(XmlTextReaderImpl.NodeData node)
		{
			string text = this.namespaceManager.LookupNamespace(node.prefix);
			if (text != null)
			{
				return text;
			}
			this.Throw("'{0}' is an undeclared prefix.", node.prefix, node.LineNo, node.LinePos);
			return null;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000224AC File Offset: 0x000206AC
		private void AddNamespace(string prefix, string uri, XmlTextReaderImpl.NodeData attr)
		{
			if (uri == "http://www.w3.org/2000/xmlns/")
			{
				if (Ref.Equal(prefix, this.XmlNs))
				{
					this.Throw("Prefix \"xmlns\" is reserved for use by XML.", attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
				}
				else
				{
					this.Throw("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", prefix, attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
				}
			}
			else if (uri == "http://www.w3.org/XML/1998/namespace" && !Ref.Equal(prefix, this.Xml) && !this.v1Compat)
			{
				this.Throw("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", prefix, attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
			}
			if (uri.Length == 0 && prefix.Length > 0)
			{
				this.Throw("Invalid namespace declaration.", attr.lineInfo.lineNo, attr.lineInfo.linePos);
			}
			try
			{
				this.namespaceManager.AddNamespace(prefix, uri);
			}
			catch (ArgumentException ex)
			{
				this.ReThrow(ex, attr.lineInfo.lineNo, attr.lineInfo.linePos);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000225D4 File Offset: 0x000207D4
		private void ResetAttributes()
		{
			if (this.fullAttrCleanup)
			{
				this.FullAttributeCleanup();
			}
			this.curAttrIndex = -1;
			this.attrCount = 0;
			this.attrHashtable = 0;
			this.attrDuplWalkCount = 0;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00022600 File Offset: 0x00020800
		private void FullAttributeCleanup()
		{
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				nodeData.nextAttrValueChunk = null;
				nodeData.IsDefaultAttribute = false;
			}
			this.fullAttrCleanup = false;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0002264A File Offset: 0x0002084A
		private void PushXmlContext()
		{
			this.xmlContext = new XmlTextReaderImpl.XmlContext(this.xmlContext);
			this.curNode.xmlContextPushed = true;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00022669 File Offset: 0x00020869
		private void PopXmlContext()
		{
			this.xmlContext = this.xmlContext.previousContext;
			this.curNode.xmlContextPushed = false;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00022688 File Offset: 0x00020888
		private XmlNodeType GetWhitespaceType()
		{
			if (this.whitespaceHandling != WhitespaceHandling.None)
			{
				if (this.xmlContext.xmlSpace == XmlSpace.Preserve)
				{
					return XmlNodeType.SignificantWhitespace;
				}
				if (this.whitespaceHandling == WhitespaceHandling.All)
				{
					return XmlNodeType.Whitespace;
				}
			}
			return XmlNodeType.None;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000226B0 File Offset: 0x000208B0
		private XmlNodeType GetTextNodeType(int orChars)
		{
			if (orChars > 32)
			{
				return XmlNodeType.Text;
			}
			return this.GetWhitespaceType();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000226C0 File Offset: 0x000208C0
		private void PushExternalEntityOrSubset(string publicId, string systemId, Uri baseUri, string entityName)
		{
			Uri uri;
			if (!string.IsNullOrEmpty(publicId))
			{
				try
				{
					uri = this.xmlResolver.ResolveUri(baseUri, publicId);
					if (this.OpenAndPush(uri))
					{
						return;
					}
				}
				catch (Exception)
				{
				}
			}
			uri = this.xmlResolver.ResolveUri(baseUri, systemId);
			try
			{
				if (this.OpenAndPush(uri))
				{
					return;
				}
			}
			catch (Exception ex)
			{
				if (this.v1Compat)
				{
					throw;
				}
				string message = ex.Message;
				this.Throw(new XmlException((entityName == null) ? "An error has occurred while opening external DTD '{0}': {1}" : "An error has occurred while opening external entity '{0}': {1}", new string[]
				{
					uri.ToString(),
					message
				}, ex, 0, 0));
			}
			if (entityName == null)
			{
				this.ThrowWithoutLineInfo("Cannot resolve external DTD subset - public ID = '{0}', system ID = '{1}'.", new string[]
				{
					(publicId != null) ? publicId : string.Empty,
					systemId
				}, null);
				return;
			}
			this.Throw((this.dtdProcessing == DtdProcessing.Ignore) ? "Cannot resolve entity reference '{0}' because the DTD has been ignored. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method." : "Cannot resolve entity reference '{0}'.", entityName);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000227BC File Offset: 0x000209BC
		private bool OpenAndPush(Uri uri)
		{
			if (this.xmlResolver.SupportsType(uri, typeof(TextReader)))
			{
				TextReader textReader = (TextReader)this.xmlResolver.GetEntity(uri, null, typeof(TextReader));
				if (textReader == null)
				{
					return false;
				}
				this.PushParsingState();
				this.InitTextReaderInput(uri.ToString(), uri, textReader);
			}
			else
			{
				Stream stream = (Stream)this.xmlResolver.GetEntity(uri, null, typeof(Stream));
				if (stream == null)
				{
					return false;
				}
				this.PushParsingState();
				this.InitStreamInput(uri, stream, null);
			}
			return true;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0002284C File Offset: 0x00020A4C
		private bool PushExternalEntity(IDtdEntityInfo entity)
		{
			if (!this.IsResolverNull)
			{
				Uri uri = null;
				if (!string.IsNullOrEmpty(entity.BaseUriString))
				{
					uri = this.xmlResolver.ResolveUri(null, entity.BaseUriString);
				}
				this.PushExternalEntityOrSubset(entity.PublicId, entity.SystemId, uri, entity.Name);
				this.RegisterEntity(entity);
				int charPos = this.ps.charPos;
				if (this.v1Compat)
				{
					this.EatWhitespaces(null);
				}
				if (!this.ParseXmlDeclaration(true))
				{
					this.ps.charPos = charPos;
				}
				return true;
			}
			Encoding encoding = this.ps.encoding;
			this.PushParsingState();
			this.InitStringInput(entity.SystemId, encoding, string.Empty);
			this.RegisterEntity(entity);
			this.RegisterConsumedCharacters(0L, true);
			return false;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0002290C File Offset: 0x00020B0C
		private void PushInternalEntity(IDtdEntityInfo entity)
		{
			Encoding encoding = this.ps.encoding;
			this.PushParsingState();
			this.InitStringInput((entity.DeclaredUriString != null) ? entity.DeclaredUriString : string.Empty, encoding, entity.Text ?? string.Empty);
			this.RegisterEntity(entity);
			this.ps.lineNo = entity.LineNumber;
			this.ps.lineStartPos = -entity.LinePosition - 1;
			this.ps.eolNormalized = true;
			this.RegisterConsumedCharacters((long)entity.Text.Length, true);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000229A4 File Offset: 0x00020BA4
		private void PopEntity()
		{
			if (this.ps.stream != null)
			{
				this.ps.stream.Close();
			}
			this.UnregisterEntity();
			this.PopParsingState();
			this.curNode.entityId = this.ps.entityId;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000229F0 File Offset: 0x00020BF0
		private void RegisterEntity(IDtdEntityInfo entity)
		{
			if (this.currentEntities != null && this.currentEntities.ContainsKey(entity))
			{
				this.Throw(entity.IsParameterEntity ? "Parameter entity '{0}' references itself." : "General entity '{0}' references itself.", entity.Name, this.parsingStatesStack[this.parsingStatesStackTop].LineNo, this.parsingStatesStack[this.parsingStatesStackTop].LinePos);
			}
			this.ps.entity = entity;
			int num = this.nextEntityId;
			this.nextEntityId = num + 1;
			this.ps.entityId = num;
			if (entity != null)
			{
				if (this.currentEntities == null)
				{
					this.currentEntities = new Dictionary<IDtdEntityInfo, IDtdEntityInfo>();
				}
				this.currentEntities.Add(entity, entity);
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00022AAA File Offset: 0x00020CAA
		private void UnregisterEntity()
		{
			if (this.ps.entity != null)
			{
				this.currentEntities.Remove(this.ps.entity);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00022AD0 File Offset: 0x00020CD0
		private void PushParsingState()
		{
			if (this.parsingStatesStack == null)
			{
				this.parsingStatesStack = new XmlTextReaderImpl.ParsingState[2];
			}
			else if (this.parsingStatesStackTop + 1 == this.parsingStatesStack.Length)
			{
				XmlTextReaderImpl.ParsingState[] array = new XmlTextReaderImpl.ParsingState[this.parsingStatesStack.Length * 2];
				Array.Copy(this.parsingStatesStack, 0, array, 0, this.parsingStatesStack.Length);
				this.parsingStatesStack = array;
			}
			this.parsingStatesStackTop++;
			this.parsingStatesStack[this.parsingStatesStackTop] = this.ps;
			this.ps.Clear();
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00022B64 File Offset: 0x00020D64
		private void PopParsingState()
		{
			this.ps.Close(true);
			XmlTextReaderImpl.ParsingState[] array = this.parsingStatesStack;
			int num = this.parsingStatesStackTop;
			this.parsingStatesStackTop = num - 1;
			this.ps = array[num];
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00022BA0 File Offset: 0x00020DA0
		private int IncrementalRead()
		{
			int num = 0;
			int num3;
			int num4;
			int num5;
			int num7;
			for (;;)
			{
				int num2 = this.incReadLeftEndPos - this.incReadLeftStartPos;
				if (num2 > 0)
				{
					try
					{
						num3 = this.incReadDecoder.Decode(this.ps.chars, this.incReadLeftStartPos, num2);
					}
					catch (XmlException ex)
					{
						this.ReThrow(ex, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						return 0;
					}
					if (num3 < num2)
					{
						break;
					}
					this.incReadLeftStartPos = 0;
					this.incReadLeftEndPos = 0;
					this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num3;
					if (this.incReadDecoder.IsFull)
					{
						return num3;
					}
				}
				num4 = 0;
				num5 = 0;
				int num10;
				for (;;)
				{
					switch (this.incReadState)
					{
					case XmlTextReaderImpl.IncrementalReadState.Text:
					case XmlTextReaderImpl.IncrementalReadState.StartTag:
					case XmlTextReaderImpl.IncrementalReadState.Attributes:
					case XmlTextReaderImpl.IncrementalReadState.AttributeValue:
						goto IL_01D7;
					case XmlTextReaderImpl.IncrementalReadState.PI:
						if (this.ParsePIValue(out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 2;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.CDATA:
						if (this.ParseCDataOrComment(XmlNodeType.CDATA, out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 3;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.Comment:
						if (this.ParseCDataOrComment(XmlNodeType.Comment, out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 3;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.ReadData:
						if (this.ReadData() == 0)
						{
							this.ThrowUnclosedElements();
						}
						this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						num4 = this.ps.charPos;
						num5 = num4;
						goto IL_01D7;
					case XmlTextReaderImpl.IncrementalReadState.EndElement:
						goto IL_017A;
					case XmlTextReaderImpl.IncrementalReadState.End:
						return num;
					default:
						goto IL_01D7;
					}
					IL_06A4:
					int num6 = num5 - num4;
					if (num6 <= 0)
					{
						continue;
					}
					try
					{
						num7 = this.incReadDecoder.Decode(this.ps.chars, num4, num6);
					}
					catch (XmlException ex2)
					{
						this.ReThrow(ex2, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						return 0;
					}
					num += num7;
					if (this.incReadDecoder.IsFull)
					{
						goto Block_54;
					}
					continue;
					IL_01D7:
					char[] array = this.ps.chars;
					num4 = this.ps.charPos;
					num5 = num4;
					int num8;
					for (;;)
					{
						this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
						if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
						{
							char c;
							while ((this.xmlCharType.charProperties[(int)(c = array[num5])] & 128) != 0)
							{
								if (c == '/')
								{
									break;
								}
								num5++;
							}
						}
						else
						{
							while ((this.xmlCharType.charProperties[(int)array[num5]] & 128) != 0)
							{
								num5++;
							}
						}
						if (array[num5] == '&' || array[num5] == '\t')
						{
							num5++;
						}
						else
						{
							if (num5 - num4 > 0)
							{
								break;
							}
							char c2 = array[num5];
							if (c2 <= '"')
							{
								if (c2 == '\n')
								{
									num5++;
									this.OnNewLine(num5);
									continue;
								}
								if (c2 == '\r')
								{
									if (array[num5 + 1] == '\n')
									{
										num5 += 2;
									}
									else
									{
										if (num5 + 1 >= this.ps.charsUsed)
										{
											goto IL_0691;
										}
										num5++;
									}
									this.OnNewLine(num5);
									continue;
								}
								if (c2 != '"')
								{
									goto IL_067A;
								}
							}
							else if (c2 <= '/')
							{
								if (c2 != '\'')
								{
									if (c2 != '/')
									{
										goto IL_067A;
									}
									if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
									{
										if (this.ps.charsUsed - num5 < 2)
										{
											goto IL_0691;
										}
										if (array[num5 + 1] == '>')
										{
											this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
											this.incReadDepth--;
										}
									}
									num5++;
									continue;
								}
							}
							else if (c2 != '<')
							{
								if (c2 != '>')
								{
									goto IL_067A;
								}
								if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
								{
									this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
								}
								num5++;
								continue;
							}
							else
							{
								if (this.incReadState != XmlTextReaderImpl.IncrementalReadState.Text)
								{
									num5++;
									continue;
								}
								if (this.ps.charsUsed - num5 < 2)
								{
									goto IL_0691;
								}
								char c3 = array[num5 + 1];
								if (c3 != '!')
								{
									if (c3 != '/')
									{
										if (c3 == '?')
										{
											goto Block_31;
										}
										int num9;
										num8 = this.ParseQName(true, 1, out num9);
										if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos + 1, num8 - this.ps.charPos - 1, this.curNode.localName) && (this.ps.chars[num8] == '>' || this.ps.chars[num8] == '/' || this.xmlCharType.IsWhiteSpace(this.ps.chars[num8])))
										{
											goto IL_0594;
										}
										num5 = num8;
										num4 = this.ps.charPos;
										array = this.ps.chars;
										continue;
									}
									else
									{
										int num11;
										num10 = this.ParseQName(true, 2, out num11);
										if (!XmlConvert.StrEqual(array, this.ps.charPos + 2, num10 - this.ps.charPos - 2, this.curNode.GetNameWPrefix(this.nameTable)) || (this.ps.chars[num10] != '>' && !this.xmlCharType.IsWhiteSpace(this.ps.chars[num10])))
										{
											num5 = num10;
											num4 = this.ps.charPos;
											array = this.ps.chars;
											continue;
										}
										int num12 = this.incReadDepth - 1;
										this.incReadDepth = num12;
										if (num12 > 0)
										{
											num5 = num10 + 1;
											continue;
										}
										goto IL_047C;
									}
								}
								else
								{
									if (this.ps.charsUsed - num5 < 4)
									{
										goto IL_0691;
									}
									if (array[num5 + 2] == '-' && array[num5 + 3] == '-')
									{
										goto Block_34;
									}
									if (this.ps.charsUsed - num5 < 9)
									{
										goto IL_0691;
									}
									if (XmlConvert.StrEqual(array, num5 + 2, 7, "[CDATA["))
									{
										goto Block_36;
									}
									continue;
								}
							}
							XmlTextReaderImpl.IncrementalReadState incrementalReadState = this.incReadState;
							if (incrementalReadState != XmlTextReaderImpl.IncrementalReadState.Attributes)
							{
								if (incrementalReadState == XmlTextReaderImpl.IncrementalReadState.AttributeValue && array[num5] == this.curNode.quoteChar)
								{
									this.incReadState = XmlTextReaderImpl.IncrementalReadState.Attributes;
								}
							}
							else
							{
								this.curNode.quoteChar = array[num5];
								this.incReadState = XmlTextReaderImpl.IncrementalReadState.AttributeValue;
							}
							num5++;
							continue;
							IL_067A:
							if (num5 == this.ps.charsUsed)
							{
								goto IL_0691;
							}
							num5++;
						}
					}
					IL_0698:
					this.ps.charPos = num5;
					goto IL_06A4;
					IL_0691:
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadData;
					goto IL_0698;
					IL_0594:
					this.incReadDepth++;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.Attributes;
					num5 = num8;
					goto IL_0698;
					Block_36:
					num5 += 9;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.CDATA;
					goto IL_0698;
					Block_34:
					num5 += 4;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.Comment;
					goto IL_0698;
					Block_31:
					num5 += 2;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.PI;
					goto IL_0698;
				}
				IL_047C:
				this.ps.charPos = num10;
				if (this.xmlCharType.IsWhiteSpace(this.ps.chars[num10]))
				{
					this.EatWhitespaces(null);
				}
				if (this.ps.chars[this.ps.charPos] != '>')
				{
					this.ThrowUnexpectedToken(">");
				}
				this.ps.charPos = this.ps.charPos + 1;
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.EndElement;
			}
			this.incReadLeftStartPos += num3;
			this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num3;
			return num3;
			IL_017A:
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
			this.nextParsingFunction = ((this.index > 0 || this.fragmentType != XmlNodeType.Document) ? XmlTextReaderImpl.ParsingFunction.ElementContent : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.outerReader.Read();
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.End;
			return num;
			Block_54:
			this.incReadLeftStartPos = num4 + num7;
			this.incReadLeftEndPos = num5;
			this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num7;
			return num;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000232F8 File Offset: 0x000214F8
		private void FinishIncrementalRead()
		{
			this.incReadDecoder = new IncrementalReadDummyDecoder();
			this.IncrementalRead();
			this.incReadDecoder = null;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00023314 File Offset: 0x00021514
		private bool ParseFragmentAttribute()
		{
			if (this.curNode.type == XmlNodeType.None)
			{
				this.curNode.type = XmlNodeType.Attribute;
				this.curAttrIndex = 0;
				this.ParseAttributeValueSlow(this.ps.charPos, ' ', this.curNode);
			}
			else
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
			}
			if (this.ReadAttributeValue())
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.FragmentAttribute;
				return true;
			}
			this.OnEof();
			return false;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00023380 File Offset: 0x00021580
		private bool ParseAttributeValueChunk()
		{
			char[] array = this.ps.chars;
			int num = this.ps.charPos;
			this.curNode = this.AddNode(this.index + this.attrCount + 1, this.index + 2);
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			if (this.emptyEntityInAttributeResolved)
			{
				this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
				this.emptyEntityInAttributeResolved = false;
				return true;
			}
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)array[num]] & 128) == 0)
				{
					char c = array[num];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
						case '\n':
							if (this.normalize)
							{
								array[num] = ' ';
							}
							num++;
							continue;
						case '\v':
						case '\f':
							goto IL_021F;
						case '\r':
							num++;
							continue;
						default:
							if (c != '"')
							{
								if (c != '&')
								{
									goto IL_021F;
								}
								if (num - this.ps.charPos > 0)
								{
									this.stringBuilder.Append(array, this.ps.charPos, num - this.ps.charPos);
								}
								this.ps.charPos = num;
								XmlTextReaderImpl.EntityType entityType = this.HandleEntityReference(true, XmlTextReaderImpl.EntityExpandType.OnlyCharacter, out num);
								if (entityType > XmlTextReaderImpl.EntityType.CharacterNamed)
								{
									if (entityType == XmlTextReaderImpl.EntityType.Unexpanded)
									{
										goto IL_01C5;
									}
								}
								else
								{
									array = this.ps.chars;
									if (this.normalize && this.xmlCharType.IsWhiteSpace(array[this.ps.charPos]) && num - this.ps.charPos == 1)
									{
										array[this.ps.charPos] = ' ';
									}
								}
								array = this.ps.chars;
								continue;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c == '<')
						{
							this.Throw(num, "'{0}', hexadecimal value {1}, is an invalid attribute character.", XmlException.BuildCharExceptionArgs('<', '\0'));
							goto IL_0271;
						}
						if (c != '>')
						{
							goto IL_021F;
						}
					}
					num++;
					continue;
					IL_021F:
					if (num != this.ps.charsUsed)
					{
						if (XmlCharType.IsHighSurrogate((int)array[num]))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_0271;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)array[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(array, this.ps.charsUsed, num);
					}
					IL_0271:
					if (num - this.ps.charPos > 0)
					{
						this.stringBuilder.Append(array, this.ps.charPos, num - this.ps.charPos);
						this.ps.charPos = num;
					}
					if (this.ReadData() == 0)
					{
						if (this.stringBuilder.Length > 0)
						{
							goto IL_02F6;
						}
						if (this.HandleEntityEnd(false))
						{
							goto Block_25;
						}
					}
					num = this.ps.charPos;
					array = this.ps.chars;
				}
				else
				{
					num++;
				}
			}
			IL_01C5:
			if (this.stringBuilder.Length == 0)
			{
				XmlTextReaderImpl.NodeData nodeData = this.curNode;
				nodeData.lineInfo.linePos = nodeData.lineInfo.linePos + 1;
				this.ps.charPos = this.ps.charPos + 1;
				this.curNode.SetNamedNode(XmlNodeType.EntityReference, this.ParseEntityName());
				return true;
			}
			goto IL_02F6;
			Block_25:
			this.SetupEndEntityNodeInAttribute();
			return true;
			IL_02F6:
			if (num - this.ps.charPos > 0)
			{
				this.stringBuilder.Append(array, this.ps.charPos, num - this.ps.charPos);
				this.ps.charPos = num;
			}
			this.curNode.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
			return true;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000236E8 File Offset: 0x000218E8
		private void ParseXmlDeclarationFragment()
		{
			try
			{
				this.ParseXmlDeclaration(false);
			}
			catch (XmlException ex)
			{
				this.ReThrow(ex, ex.LineNumber, ex.LinePosition - 6);
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00023728 File Offset: 0x00021928
		private void ThrowUnexpectedToken(int pos, string expectedToken)
		{
			this.ThrowUnexpectedToken(pos, expectedToken, null);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00023733 File Offset: 0x00021933
		private void ThrowUnexpectedToken(string expectedToken1)
		{
			this.ThrowUnexpectedToken(expectedToken1, null);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002373D File Offset: 0x0002193D
		private void ThrowUnexpectedToken(int pos, string expectedToken1, string expectedToken2)
		{
			this.ps.charPos = pos;
			this.ThrowUnexpectedToken(expectedToken1, expectedToken2);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00023754 File Offset: 0x00021954
		private void ThrowUnexpectedToken(string expectedToken1, string expectedToken2)
		{
			string text = this.ParseUnexpectedToken();
			if (text == null)
			{
				this.Throw("Unexpected end of file has occurred.");
			}
			if (expectedToken2 != null)
			{
				this.Throw("'{0}' is an unexpected token. The expected token is '{1}' or '{2}'.", new string[] { text, expectedToken1, expectedToken2 });
				return;
			}
			this.Throw("'{0}' is an unexpected token. The expected token is '{1}'.", new string[] { text, expectedToken1 });
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000237B0 File Offset: 0x000219B0
		private string ParseUnexpectedToken(int pos)
		{
			this.ps.charPos = pos;
			return this.ParseUnexpectedToken();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000237C4 File Offset: 0x000219C4
		private string ParseUnexpectedToken()
		{
			if (this.ps.charPos == this.ps.charsUsed)
			{
				return null;
			}
			if (this.xmlCharType.IsNCNameSingleChar(this.ps.chars[this.ps.charPos]))
			{
				int num = this.ps.charPos + 1;
				while (this.xmlCharType.IsNCNameSingleChar(this.ps.chars[num]))
				{
					num++;
				}
				return new string(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			}
			return new string(this.ps.chars, this.ps.charPos, 1);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00023884 File Offset: 0x00021A84
		private void ThrowExpectingWhitespace(int pos)
		{
			string text = this.ParseUnexpectedToken(pos);
			if (text == null)
			{
				this.Throw(pos, "Unexpected end of file has occurred.");
				return;
			}
			this.Throw(pos, "'{0}' is an unexpected token. Expecting white space.", text);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000238B8 File Offset: 0x00021AB8
		private int GetIndexOfAttributeWithoutPrefix(string name)
		{
			name = this.nameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, name) && this.nodes[i].prefix.Length == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00023928 File Offset: 0x00021B28
		private int GetIndexOfAttributeWithPrefix(string name)
		{
			name = this.nameTable.Add(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].GetNameWPrefix(this.nameTable), name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00023988 File Offset: 0x00021B88
		private bool ZeroEndingStream(int pos)
		{
			if (this.v1Compat && pos == this.ps.charsUsed - 1 && this.ps.chars[pos] == '\0' && this.ReadData() == 0 && this.ps.isStreamEof)
			{
				this.ps.charsUsed = this.ps.charsUsed - 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000239E4 File Offset: 0x00021BE4
		private void ParseDtdFromParserContext()
		{
			IDtdParser dtdParser = DtdParser.Create();
			this.dtdInfo = dtdParser.ParseFreeFloatingDtd(this.fragmentParserContext.BaseURI, this.fragmentParserContext.DocTypeName, this.fragmentParserContext.PublicId, this.fragmentParserContext.SystemId, this.fragmentParserContext.InternalSubset, new XmlTextReaderImpl.DtdParserProxy(this));
			if ((this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00023A74 File Offset: 0x00021C74
		private bool InitReadContentAsBinary()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadChars, ReadBase64, and ReadBinHex."));
			}
			if (!XmlReader.IsTextualNode(this.curNode.type) && !this.MoveToNextContentNode(false))
			{
				return false;
			}
			this.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary);
			this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			return true;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00023AFC File Offset: 0x00021CFC
		private bool InitReadElementContentAsBinary()
		{
			bool isEmptyElement = this.curNode.IsEmptyElement;
			this.outerReader.Read();
			if (isEmptyElement)
			{
				return false;
			}
			if (!this.MoveToNextContentNode(false))
			{
				if (this.curNode.type != XmlNodeType.EndElement)
				{
					this.Throw("'{0}' is an invalid XmlNodeType.", this.curNode.type.ToString());
				}
				this.outerReader.Read();
				return false;
			}
			this.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary);
			this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			return true;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00023B98 File Offset: 0x00021D98
		private bool MoveToNextContentNode(bool moveIfOnContentNode)
		{
			for (;;)
			{
				switch (this.curNode.type)
				{
				case XmlNodeType.Attribute:
					goto IL_0052;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (!moveIfOnContentNode)
					{
						return true;
					}
					goto IL_006B;
				case XmlNodeType.EntityReference:
					this.outerReader.ResolveEntity();
					goto IL_006B;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_006B;
				}
				break;
				IL_006B:
				moveIfOnContentNode = false;
				if (!this.outerReader.Read())
				{
					return false;
				}
			}
			return false;
			IL_0052:
			return !moveIfOnContentNode;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00023C24 File Offset: 0x00021E24
		private void SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction inReadBinaryFunction)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
			{
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue;
			}
			else
			{
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue;
				this.nextNextParsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.parsingFunction;
			}
			this.readValueOffset = 0;
			this.parsingFunction = inReadBinaryFunction;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00023C74 File Offset: 0x00021E74
		private void SetupFromParserContext(XmlParserContext context, XmlReaderSettings settings)
		{
			XmlNameTable xmlNameTable = settings.NameTable;
			this.nameTableFromSettings = xmlNameTable != null;
			if (context.NamespaceManager != null)
			{
				if (xmlNameTable != null && xmlNameTable != context.NamespaceManager.NameTable)
				{
					throw new XmlException("XmlReaderSettings.XmlNameTable must be the same name table as in XmlParserContext.NameTable or XmlParserContext.NamespaceManager.NameTable, or it must be null.");
				}
				this.namespaceManager = context.NamespaceManager;
				this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
				xmlNameTable = this.namespaceManager.NameTable;
			}
			else if (context.NameTable != null)
			{
				if (xmlNameTable != null && xmlNameTable != context.NameTable)
				{
					throw new XmlException("XmlReaderSettings.XmlNameTable must be the same name table as in XmlParserContext.NameTable or XmlParserContext.NamespaceManager.NameTable, or it must be null.", string.Empty);
				}
				xmlNameTable = context.NameTable;
			}
			else if (xmlNameTable == null)
			{
				xmlNameTable = new NameTable();
			}
			this.nameTable = xmlNameTable;
			if (this.namespaceManager == null)
			{
				this.namespaceManager = new XmlNamespaceManager(xmlNameTable);
			}
			this.xmlContext.xmlSpace = context.XmlSpace;
			this.xmlContext.xmlLang = context.XmlLang;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00023D5E File Offset: 0x00021F5E
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.dtdInfo;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00023D68 File Offset: 0x00021F68
		internal void SetDtdInfo(IDtdInfo newDtdInfo)
		{
			this.dtdInfo = newDtdInfo;
			if (this.dtdInfo != null && (this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
		}

		// Token: 0x1700013E RID: 318
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00023DB5 File Offset: 0x00021FB5
		internal IValidationEventHandling ValidationEventHandling
		{
			set
			{
				this.validationEventHandling = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x00023DBE File Offset: 0x00021FBE
		internal XmlTextReaderImpl.OnDefaultAttributeUseDelegate OnDefaultAttributeUse
		{
			set
			{
				this.onDefaultAttributeUse = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00023DC7 File Offset: 0x00021FC7
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.validatingReaderCompatFlag = value;
				if (value)
				{
					this.nameTable.Add("http://www.w3.org/2001/XMLSchema");
					this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
					this.nameTable.Add("urn:schemas-microsoft-com:datatypes");
				}
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00023E06 File Offset: 0x00022006
		internal XmlNodeType FragmentType
		{
			get
			{
				return this.fragmentType;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00023E0E File Offset: 0x0002200E
		internal void ChangeCurrentNodeType(XmlNodeType newNodeType)
		{
			this.curNode.type = newNodeType;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00023E1C File Offset: 0x0002201C
		internal XmlResolver GetResolver()
		{
			if (this.IsResolverNull)
			{
				return null;
			}
			return this.xmlResolver;
		}

		// Token: 0x17000142 RID: 322
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00023E2E File Offset: 0x0002202E
		internal object InternalSchemaType
		{
			set
			{
				this.curNode.schemaType = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00023E3C File Offset: 0x0002203C
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00023E49 File Offset: 0x00022049
		internal object InternalTypedValue
		{
			get
			{
				return this.curNode.typedValue;
			}
			set
			{
				this.curNode.typedValue = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00023E57 File Offset: 0x00022057
		internal bool StandAlone
		{
			get
			{
				return this.standalone;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001B596 File Offset: 0x00019796
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.namespaceManager;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001B5A6 File Offset: 0x000197A6
		internal bool V1Compat
		{
			get
			{
				return this.v1Compat;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00023E60 File Offset: 0x00022060
		private bool AddDefaultAttributeDtd(IDtdDefaultAttributeInfo defAttrInfo, bool definedInDtd, XmlTextReaderImpl.NodeData[] nameSortedNodeData)
		{
			if (defAttrInfo.Prefix.Length > 0)
			{
				this.attrNeedNamespaceLookup = true;
			}
			string localName = defAttrInfo.LocalName;
			string prefix = defAttrInfo.Prefix;
			if (nameSortedNodeData != null)
			{
				if (Array.BinarySearch<object>(nameSortedNodeData, defAttrInfo, XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.Instance) >= 0)
				{
					return false;
				}
			}
			else
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					if (this.nodes[i].localName == localName && this.nodes[i].prefix == prefix)
					{
						return false;
					}
				}
			}
			XmlTextReaderImpl.NodeData nodeData = this.AddDefaultAttributeInternal(defAttrInfo.LocalName, null, defAttrInfo.Prefix, defAttrInfo.DefaultValueExpanded, defAttrInfo.LineNumber, defAttrInfo.LinePosition, defAttrInfo.ValueLineNumber, defAttrInfo.ValueLinePosition, defAttrInfo.IsXmlAttribute);
			if (this.DtdValidation)
			{
				if (this.onDefaultAttributeUse != null)
				{
					this.onDefaultAttributeUse(defAttrInfo, this);
				}
				nodeData.typedValue = defAttrInfo.DefaultValueTyped;
			}
			return nodeData != null;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00023F54 File Offset: 0x00022154
		internal bool AddDefaultAttributeNonDtd(SchemaAttDef attrDef)
		{
			string text = this.nameTable.Add(attrDef.Name.Name);
			string text2 = this.nameTable.Add(attrDef.Prefix);
			string text3 = this.nameTable.Add(attrDef.Name.Namespace);
			if (text2.Length == 0 && text3.Length > 0)
			{
				text2 = this.namespaceManager.LookupPrefix(text3);
				if (text2 == null)
				{
					text2 = string.Empty;
				}
			}
			for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
			{
				if (this.nodes[i].localName == text && (this.nodes[i].prefix == text2 || (this.nodes[i].ns == text3 && text3 != null)))
				{
					return false;
				}
			}
			XmlTextReaderImpl.NodeData nodeData = this.AddDefaultAttributeInternal(text, text3, text2, attrDef.DefaultValueExpanded, attrDef.LineNumber, attrDef.LinePosition, attrDef.ValueLineNumber, attrDef.ValueLinePosition, attrDef.Reserved > SchemaAttDef.Reserve.None);
			nodeData.schemaType = ((attrDef.SchemaType == null) ? attrDef.Datatype : attrDef.SchemaType);
			nodeData.typedValue = attrDef.DefaultValueTyped;
			return true;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00024078 File Offset: 0x00022278
		private XmlTextReaderImpl.NodeData AddDefaultAttributeInternal(string localName, string ns, string prefix, string value, int lineNo, int linePos, int valueLineNo, int valueLinePos, bool isXmlAttribute)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttribute(localName, prefix, (prefix.Length > 0) ? null : localName);
			if (ns != null)
			{
				nodeData.ns = ns;
			}
			nodeData.SetValue(value);
			nodeData.IsDefaultAttribute = true;
			nodeData.lineInfo.Set(lineNo, linePos);
			nodeData.lineInfo2.Set(valueLineNo, valueLinePos);
			if (nodeData.prefix.Length == 0)
			{
				if (Ref.Equal(nodeData.localName, this.XmlNs))
				{
					this.OnDefaultNamespaceDecl(nodeData);
					if (!this.attrNeedNamespaceLookup && this.nodes[this.index].prefix.Length == 0)
					{
						this.nodes[this.index].ns = this.xmlContext.defaultNamespace;
					}
				}
			}
			else if (Ref.Equal(nodeData.prefix, this.XmlNs))
			{
				this.OnNamespaceDecl(nodeData);
				if (!this.attrNeedNamespaceLookup)
				{
					string localName2 = nodeData.localName;
					for (int i = this.index; i < this.index + this.attrCount + 1; i++)
					{
						if (this.nodes[i].prefix.Equals(localName2))
						{
							this.nodes[i].ns = this.namespaceManager.LookupNamespace(localName2);
						}
					}
				}
			}
			else if (isXmlAttribute)
			{
				this.OnXmlReservedAttribute(nodeData);
			}
			this.fullAttrCleanup = true;
			return nodeData;
		}

		// Token: 0x17000147 RID: 327
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x000241D0 File Offset: 0x000223D0
		internal bool DisableUndeclaredEntityCheck
		{
			set
			{
				this.disableUndeclaredEntityCheck = value;
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000241DC File Offset: 0x000223DC
		private int ReadContentAsBinary(byte[] buffer, int index, int count)
		{
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
			{
				return 0;
			}
			this.incReadDecoder.SetNextOutputBuffer(buffer, index, count);
			int num;
			int num2;
			int num3;
			XmlTextReaderImpl.ParsingFunction parsingFunction;
			for (;;)
			{
				num = 0;
				try
				{
					num = this.curNode.CopyToBinary(this.incReadDecoder, this.readValueOffset);
				}
				catch (XmlException ex)
				{
					this.curNode.AdjustLineInfo(this.readValueOffset, this.ps.eolNormalized, ref this.incReadLineInfo);
					this.ReThrow(ex, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
				}
				this.readValueOffset += num;
				if (this.incReadDecoder.IsFull)
				{
					break;
				}
				if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
				{
					this.curNode.SetValue(string.Empty);
					bool flag = false;
					num2 = 0;
					num3 = 0;
					while (!this.incReadDecoder.IsFull && !flag)
					{
						int num4 = 0;
						this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
						flag = this.ParseText(out num2, out num3, ref num4);
						try
						{
							num = this.incReadDecoder.Decode(this.ps.chars, num2, num3 - num2);
						}
						catch (XmlException ex2)
						{
							this.ReThrow(ex2, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						}
						num2 += num;
					}
					this.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue);
					this.readValueOffset = 0;
					if (this.incReadDecoder.IsFull)
					{
						goto Block_8;
					}
				}
				parsingFunction = this.parsingFunction;
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.nextNextParsingFunction;
				if (!this.MoveToNextContentNode(true))
				{
					goto Block_9;
				}
				this.SetupReadContentAsBinaryState(parsingFunction);
				this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			}
			return this.incReadDecoder.DecodedCount;
			Block_8:
			this.curNode.SetValue(this.ps.chars, num2, num3 - num2);
			XmlTextReaderImpl.AdjustLineInfo(this.ps.chars, num2 - num, num2, this.ps.eolNormalized, ref this.incReadLineInfo);
			this.curNode.SetLineInfo(this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
			return this.incReadDecoder.DecodedCount;
			Block_9:
			this.SetupReadContentAsBinaryState(parsingFunction);
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End;
			return this.incReadDecoder.DecodedCount;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0002445C File Offset: 0x0002265C
		private int ReadElementContentAsBinary(byte[] buffer, int index, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = this.ReadContentAsBinary(buffer, index, count);
			if (num > 0)
			{
				return num;
			}
			if (this.curNode.type != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.curNode.type.ToString(), this);
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
			this.outerReader.Read();
			return 0;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000244D4 File Offset: 0x000226D4
		private void InitBase64Decoder()
		{
			if (this.base64Decoder == null)
			{
				this.base64Decoder = new Base64Decoder();
			}
			else
			{
				this.base64Decoder.Reset();
			}
			this.incReadDecoder = this.base64Decoder;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00024502 File Offset: 0x00022702
		private void InitBinHexDecoder()
		{
			if (this.binHexDecoder == null)
			{
				this.binHexDecoder = new BinHexDecoder();
			}
			else
			{
				this.binHexDecoder.Reset();
			}
			this.incReadDecoder = this.binHexDecoder;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00024530 File Offset: 0x00022730
		private bool UriEqual(Uri uri1, string uri1Str, string uri2Str, XmlResolver resolver)
		{
			if (resolver == null)
			{
				return uri1Str == uri2Str;
			}
			if (uri1 == null)
			{
				uri1 = resolver.ResolveUri(null, uri1Str);
			}
			Uri uri2 = resolver.ResolveUri(null, uri2Str);
			return uri1.Equals(uri2);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00024570 File Offset: 0x00022770
		private void RegisterConsumedCharacters(long characters, bool inEntityReference)
		{
			if (this.maxCharactersInDocument > 0L)
			{
				long num = this.charactersInDocument + characters;
				if (num < this.charactersInDocument)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersInDocument");
				}
				else
				{
					this.charactersInDocument = num;
				}
				if (this.charactersInDocument > this.maxCharactersInDocument)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersInDocument");
				}
			}
			if (this.maxCharactersFromEntities > 0L && inEntityReference)
			{
				long num2 = this.charactersFromEntities + characters;
				if (num2 < this.charactersFromEntities)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersFromEntities");
				}
				else
				{
					this.charactersFromEntities = num2;
				}
				if (this.charactersFromEntities > this.maxCharactersFromEntities)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersFromEntities");
				}
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00024628 File Offset: 0x00022828
		internal unsafe static void AdjustLineInfo(char[] chars, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
		{
			fixed (char* ptr = &chars[startPos])
			{
				XmlTextReaderImpl.AdjustLineInfo(ptr, endPos - startPos, isNormalized, ref lineInfo);
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00024650 File Offset: 0x00022850
		internal unsafe static void AdjustLineInfo(string str, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
		{
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				XmlTextReaderImpl.AdjustLineInfo(ptr + startPos, endPos - startPos, isNormalized, ref lineInfo);
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00024680 File Offset: 0x00022880
		internal unsafe static void AdjustLineInfo(char* pChars, int length, bool isNormalized, ref LineInfo lineInfo)
		{
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				char c = pChars[i];
				if (c != '\n')
				{
					if (c == '\r')
					{
						if (!isNormalized)
						{
							lineInfo.lineNo++;
							num = i;
							if (i + 1 < length && pChars[i + 1] == '\n')
							{
								i++;
								num++;
							}
						}
					}
				}
				else
				{
					lineInfo.lineNo++;
					num = i;
				}
			}
			if (num >= 0)
			{
				lineInfo.linePos = length - num;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000246F8 File Offset: 0x000228F8
		internal static string StripSpaces(string value)
		{
			int length = value.Length;
			if (length <= 0)
			{
				return string.Empty;
			}
			int num = 0;
			StringBuilder stringBuilder = null;
			while (value[num] == ' ')
			{
				num++;
				if (num == length)
				{
					return " ";
				}
			}
			int i;
			for (i = num; i < length; i++)
			{
				if (value[i] == ' ')
				{
					int num2 = i + 1;
					while (num2 < length && value[num2] == ' ')
					{
						num2++;
					}
					if (num2 == length)
					{
						if (stringBuilder == null)
						{
							return value.Substring(num, i - num);
						}
						stringBuilder.Append(value, num, i - num);
						return stringBuilder.ToString();
					}
					else if (num2 > i + 1)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(length);
						}
						stringBuilder.Append(value, num, i - num + 1);
						num = num2;
						i = num2 - 1;
					}
				}
			}
			if (stringBuilder != null)
			{
				if (i > num)
				{
					stringBuilder.Append(value, num, i - num);
				}
				return stringBuilder.ToString();
			}
			if (num != 0)
			{
				return value.Substring(num, length - num);
			}
			return value;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000247E0 File Offset: 0x000229E0
		internal static void StripSpaces(char[] value, int index, ref int len)
		{
			if (len <= 0)
			{
				return;
			}
			int num = index;
			int num2 = index + len;
			while (value[num] == ' ')
			{
				num++;
				if (num == num2)
				{
					len = 1;
					return;
				}
			}
			int num3 = num - index;
			for (int i = num; i < num2; i++)
			{
				char c;
				if ((c = value[i]) == ' ')
				{
					int num4 = i + 1;
					while (num4 < num2 && value[num4] == ' ')
					{
						num4++;
					}
					if (num4 == num2)
					{
						num3 += num4 - i;
						break;
					}
					if (num4 > i + 1)
					{
						num3 += num4 - i - 1;
						i = num4 - 1;
					}
				}
				value[i - num3] = c;
			}
			len -= num3;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00024873 File Offset: 0x00022A73
		internal static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00024886 File Offset: 0x00022A86
		internal static void BlockCopy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
		}

		// Token: 0x040002A1 RID: 673
		private readonly bool useAsync;

		// Token: 0x040002A2 RID: 674
		private XmlTextReaderImpl.LaterInitParam laterInitParam;

		// Token: 0x040002A3 RID: 675
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040002A4 RID: 676
		private XmlTextReaderImpl.ParsingState ps;

		// Token: 0x040002A5 RID: 677
		private XmlTextReaderImpl.ParsingFunction parsingFunction;

		// Token: 0x040002A6 RID: 678
		private XmlTextReaderImpl.ParsingFunction nextParsingFunction;

		// Token: 0x040002A7 RID: 679
		private XmlTextReaderImpl.ParsingFunction nextNextParsingFunction;

		// Token: 0x040002A8 RID: 680
		private XmlTextReaderImpl.NodeData[] nodes;

		// Token: 0x040002A9 RID: 681
		private XmlTextReaderImpl.NodeData curNode;

		// Token: 0x040002AA RID: 682
		private int index;

		// Token: 0x040002AB RID: 683
		private int curAttrIndex = -1;

		// Token: 0x040002AC RID: 684
		private int attrCount;

		// Token: 0x040002AD RID: 685
		private int attrHashtable;

		// Token: 0x040002AE RID: 686
		private int attrDuplWalkCount;

		// Token: 0x040002AF RID: 687
		private bool attrNeedNamespaceLookup;

		// Token: 0x040002B0 RID: 688
		private bool fullAttrCleanup;

		// Token: 0x040002B1 RID: 689
		private XmlTextReaderImpl.NodeData[] attrDuplSortingArray;

		// Token: 0x040002B2 RID: 690
		private XmlNameTable nameTable;

		// Token: 0x040002B3 RID: 691
		private bool nameTableFromSettings;

		// Token: 0x040002B4 RID: 692
		private XmlResolver xmlResolver;

		// Token: 0x040002B5 RID: 693
		private string url = string.Empty;

		// Token: 0x040002B6 RID: 694
		private bool normalize;

		// Token: 0x040002B7 RID: 695
		private bool supportNamespaces = true;

		// Token: 0x040002B8 RID: 696
		private WhitespaceHandling whitespaceHandling;

		// Token: 0x040002B9 RID: 697
		private DtdProcessing dtdProcessing = DtdProcessing.Parse;

		// Token: 0x040002BA RID: 698
		private EntityHandling entityHandling;

		// Token: 0x040002BB RID: 699
		private bool ignorePIs;

		// Token: 0x040002BC RID: 700
		private bool ignoreComments;

		// Token: 0x040002BD RID: 701
		private bool checkCharacters;

		// Token: 0x040002BE RID: 702
		private int lineNumberOffset;

		// Token: 0x040002BF RID: 703
		private int linePositionOffset;

		// Token: 0x040002C0 RID: 704
		private bool closeInput;

		// Token: 0x040002C1 RID: 705
		private long maxCharactersInDocument;

		// Token: 0x040002C2 RID: 706
		private long maxCharactersFromEntities;

		// Token: 0x040002C3 RID: 707
		private bool v1Compat;

		// Token: 0x040002C4 RID: 708
		private XmlNamespaceManager namespaceManager;

		// Token: 0x040002C5 RID: 709
		private string lastPrefix = string.Empty;

		// Token: 0x040002C6 RID: 710
		private XmlTextReaderImpl.XmlContext xmlContext;

		// Token: 0x040002C7 RID: 711
		private XmlTextReaderImpl.ParsingState[] parsingStatesStack;

		// Token: 0x040002C8 RID: 712
		private int parsingStatesStackTop = -1;

		// Token: 0x040002C9 RID: 713
		private string reportedBaseUri;

		// Token: 0x040002CA RID: 714
		private Encoding reportedEncoding;

		// Token: 0x040002CB RID: 715
		private IDtdInfo dtdInfo;

		// Token: 0x040002CC RID: 716
		private XmlNodeType fragmentType = XmlNodeType.Document;

		// Token: 0x040002CD RID: 717
		private XmlParserContext fragmentParserContext;

		// Token: 0x040002CE RID: 718
		private bool fragment;

		// Token: 0x040002CF RID: 719
		private IncrementalReadDecoder incReadDecoder;

		// Token: 0x040002D0 RID: 720
		private XmlTextReaderImpl.IncrementalReadState incReadState;

		// Token: 0x040002D1 RID: 721
		private LineInfo incReadLineInfo;

		// Token: 0x040002D2 RID: 722
		private BinHexDecoder binHexDecoder;

		// Token: 0x040002D3 RID: 723
		private Base64Decoder base64Decoder;

		// Token: 0x040002D4 RID: 724
		private int incReadDepth;

		// Token: 0x040002D5 RID: 725
		private int incReadLeftStartPos;

		// Token: 0x040002D6 RID: 726
		private int incReadLeftEndPos;

		// Token: 0x040002D7 RID: 727
		private int attributeValueBaseEntityId;

		// Token: 0x040002D8 RID: 728
		private bool emptyEntityInAttributeResolved;

		// Token: 0x040002D9 RID: 729
		private IValidationEventHandling validationEventHandling;

		// Token: 0x040002DA RID: 730
		private XmlTextReaderImpl.OnDefaultAttributeUseDelegate onDefaultAttributeUse;

		// Token: 0x040002DB RID: 731
		private bool validatingReaderCompatFlag;

		// Token: 0x040002DC RID: 732
		private bool addDefaultAttributesAndNormalize;

		// Token: 0x040002DD RID: 733
		private StringBuilder stringBuilder;

		// Token: 0x040002DE RID: 734
		private bool rootElementParsed;

		// Token: 0x040002DF RID: 735
		private bool standalone;

		// Token: 0x040002E0 RID: 736
		private int nextEntityId = 1;

		// Token: 0x040002E1 RID: 737
		private XmlTextReaderImpl.ParsingMode parsingMode;

		// Token: 0x040002E2 RID: 738
		private ReadState readState;

		// Token: 0x040002E3 RID: 739
		private IDtdEntityInfo lastEntity;

		// Token: 0x040002E4 RID: 740
		private bool afterResetState;

		// Token: 0x040002E5 RID: 741
		private int documentStartBytePos;

		// Token: 0x040002E6 RID: 742
		private int readValueOffset;

		// Token: 0x040002E7 RID: 743
		private long charactersInDocument;

		// Token: 0x040002E8 RID: 744
		private long charactersFromEntities;

		// Token: 0x040002E9 RID: 745
		private Dictionary<IDtdEntityInfo, IDtdEntityInfo> currentEntities;

		// Token: 0x040002EA RID: 746
		private bool disableUndeclaredEntityCheck;

		// Token: 0x040002EB RID: 747
		private XmlReader outerReader;

		// Token: 0x040002EC RID: 748
		private bool xmlResolverIsSet;

		// Token: 0x040002ED RID: 749
		private string Xml;

		// Token: 0x040002EE RID: 750
		private string XmlNs;

		// Token: 0x040002EF RID: 751
		private Task<Tuple<int, int, int, bool>> parseText_dummyTask = Task.FromResult<Tuple<int, int, int, bool>>(new Tuple<int, int, int, bool>(0, 0, 0, false));

		// Token: 0x02000071 RID: 113
		private enum ParsingFunction
		{
			// Token: 0x040002F1 RID: 753
			ElementContent,
			// Token: 0x040002F2 RID: 754
			NoData,
			// Token: 0x040002F3 RID: 755
			OpenUrl,
			// Token: 0x040002F4 RID: 756
			SwitchToInteractive,
			// Token: 0x040002F5 RID: 757
			SwitchToInteractiveXmlDecl,
			// Token: 0x040002F6 RID: 758
			DocumentContent,
			// Token: 0x040002F7 RID: 759
			MoveToElementContent,
			// Token: 0x040002F8 RID: 760
			PopElementContext,
			// Token: 0x040002F9 RID: 761
			PopEmptyElementContext,
			// Token: 0x040002FA RID: 762
			ResetAttributesRootLevel,
			// Token: 0x040002FB RID: 763
			Error,
			// Token: 0x040002FC RID: 764
			Eof,
			// Token: 0x040002FD RID: 765
			ReaderClosed,
			// Token: 0x040002FE RID: 766
			EntityReference,
			// Token: 0x040002FF RID: 767
			InIncrementalRead,
			// Token: 0x04000300 RID: 768
			FragmentAttribute,
			// Token: 0x04000301 RID: 769
			ReportEndEntity,
			// Token: 0x04000302 RID: 770
			AfterResolveEntityInContent,
			// Token: 0x04000303 RID: 771
			AfterResolveEmptyEntityInContent,
			// Token: 0x04000304 RID: 772
			XmlDeclarationFragment,
			// Token: 0x04000305 RID: 773
			GoToEof,
			// Token: 0x04000306 RID: 774
			PartialTextValue,
			// Token: 0x04000307 RID: 775
			InReadAttributeValue,
			// Token: 0x04000308 RID: 776
			InReadValueChunk,
			// Token: 0x04000309 RID: 777
			InReadContentAsBinary,
			// Token: 0x0400030A RID: 778
			InReadElementContentAsBinary
		}

		// Token: 0x02000072 RID: 114
		private enum ParsingMode
		{
			// Token: 0x0400030C RID: 780
			Full,
			// Token: 0x0400030D RID: 781
			SkipNode,
			// Token: 0x0400030E RID: 782
			SkipContent
		}

		// Token: 0x02000073 RID: 115
		private enum EntityType
		{
			// Token: 0x04000310 RID: 784
			CharacterDec,
			// Token: 0x04000311 RID: 785
			CharacterHex,
			// Token: 0x04000312 RID: 786
			CharacterNamed,
			// Token: 0x04000313 RID: 787
			Expanded,
			// Token: 0x04000314 RID: 788
			Skipped,
			// Token: 0x04000315 RID: 789
			FakeExpanded,
			// Token: 0x04000316 RID: 790
			Unexpanded,
			// Token: 0x04000317 RID: 791
			ExpandedInAttribute
		}

		// Token: 0x02000074 RID: 116
		private enum EntityExpandType
		{
			// Token: 0x04000319 RID: 793
			All,
			// Token: 0x0400031A RID: 794
			OnlyGeneral,
			// Token: 0x0400031B RID: 795
			OnlyCharacter
		}

		// Token: 0x02000075 RID: 117
		private enum IncrementalReadState
		{
			// Token: 0x0400031D RID: 797
			Text,
			// Token: 0x0400031E RID: 798
			StartTag,
			// Token: 0x0400031F RID: 799
			PI,
			// Token: 0x04000320 RID: 800
			CDATA,
			// Token: 0x04000321 RID: 801
			Comment,
			// Token: 0x04000322 RID: 802
			Attributes,
			// Token: 0x04000323 RID: 803
			AttributeValue,
			// Token: 0x04000324 RID: 804
			ReadData,
			// Token: 0x04000325 RID: 805
			EndElement,
			// Token: 0x04000326 RID: 806
			End,
			// Token: 0x04000327 RID: 807
			ReadValueChunk_OnCachedValue,
			// Token: 0x04000328 RID: 808
			ReadValueChunk_OnPartialValue,
			// Token: 0x04000329 RID: 809
			ReadContentAsBinary_OnCachedValue,
			// Token: 0x0400032A RID: 810
			ReadContentAsBinary_OnPartialValue,
			// Token: 0x0400032B RID: 811
			ReadContentAsBinary_End
		}

		// Token: 0x02000076 RID: 118
		private class LaterInitParam
		{
			// Token: 0x0400032C RID: 812
			public bool useAsync;

			// Token: 0x0400032D RID: 813
			public Stream inputStream;

			// Token: 0x0400032E RID: 814
			public byte[] inputBytes;

			// Token: 0x0400032F RID: 815
			public int inputByteCount;

			// Token: 0x04000330 RID: 816
			public Uri inputbaseUri;

			// Token: 0x04000331 RID: 817
			public string inputUriStr;

			// Token: 0x04000332 RID: 818
			public XmlResolver inputUriResolver;

			// Token: 0x04000333 RID: 819
			public XmlParserContext inputContext;

			// Token: 0x04000334 RID: 820
			public TextReader inputTextReader;

			// Token: 0x04000335 RID: 821
			public XmlTextReaderImpl.InitInputType initType = XmlTextReaderImpl.InitInputType.Invalid;
		}

		// Token: 0x02000077 RID: 119
		private enum InitInputType
		{
			// Token: 0x04000337 RID: 823
			UriString,
			// Token: 0x04000338 RID: 824
			Stream,
			// Token: 0x04000339 RID: 825
			TextReader,
			// Token: 0x0400033A RID: 826
			Invalid
		}

		// Token: 0x02000078 RID: 120
		private struct ParsingState
		{
			// Token: 0x060006B4 RID: 1716 RVA: 0x000248A4 File Offset: 0x00022AA4
			internal void Clear()
			{
				this.chars = null;
				this.charPos = 0;
				this.charsUsed = 0;
				this.encoding = null;
				this.stream = null;
				this.decoder = null;
				this.bytes = null;
				this.bytePos = 0;
				this.bytesUsed = 0;
				this.textReader = null;
				this.lineNo = 1;
				this.lineStartPos = -1;
				this.baseUriStr = string.Empty;
				this.baseUri = null;
				this.isEof = false;
				this.isStreamEof = false;
				this.eolNormalized = true;
				this.entityResolvedManually = false;
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00024933 File Offset: 0x00022B33
			internal void Close(bool closeInput)
			{
				if (closeInput)
				{
					if (this.stream != null)
					{
						this.stream.Close();
						return;
					}
					if (this.textReader != null)
					{
						this.textReader.Close();
					}
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0002495F File Offset: 0x00022B5F
			internal int LineNo
			{
				get
				{
					return this.lineNo;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00024967 File Offset: 0x00022B67
			internal int LinePos
			{
				get
				{
					return this.charPos - this.lineStartPos;
				}
			}

			// Token: 0x0400033B RID: 827
			internal char[] chars;

			// Token: 0x0400033C RID: 828
			internal int charPos;

			// Token: 0x0400033D RID: 829
			internal int charsUsed;

			// Token: 0x0400033E RID: 830
			internal Encoding encoding;

			// Token: 0x0400033F RID: 831
			internal bool appendMode;

			// Token: 0x04000340 RID: 832
			internal Stream stream;

			// Token: 0x04000341 RID: 833
			internal Decoder decoder;

			// Token: 0x04000342 RID: 834
			internal byte[] bytes;

			// Token: 0x04000343 RID: 835
			internal int bytePos;

			// Token: 0x04000344 RID: 836
			internal int bytesUsed;

			// Token: 0x04000345 RID: 837
			internal TextReader textReader;

			// Token: 0x04000346 RID: 838
			internal int lineNo;

			// Token: 0x04000347 RID: 839
			internal int lineStartPos;

			// Token: 0x04000348 RID: 840
			internal string baseUriStr;

			// Token: 0x04000349 RID: 841
			internal Uri baseUri;

			// Token: 0x0400034A RID: 842
			internal bool isEof;

			// Token: 0x0400034B RID: 843
			internal bool isStreamEof;

			// Token: 0x0400034C RID: 844
			internal IDtdEntityInfo entity;

			// Token: 0x0400034D RID: 845
			internal int entityId;

			// Token: 0x0400034E RID: 846
			internal bool eolNormalized;

			// Token: 0x0400034F RID: 847
			internal bool entityResolvedManually;
		}

		// Token: 0x02000079 RID: 121
		private class XmlContext
		{
			// Token: 0x060006B8 RID: 1720 RVA: 0x00024976 File Offset: 0x00022B76
			internal XmlContext()
			{
				this.xmlSpace = XmlSpace.None;
				this.xmlLang = string.Empty;
				this.defaultNamespace = string.Empty;
				this.previousContext = null;
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x000249A2 File Offset: 0x00022BA2
			internal XmlContext(XmlTextReaderImpl.XmlContext previousContext)
			{
				this.xmlSpace = previousContext.xmlSpace;
				this.xmlLang = previousContext.xmlLang;
				this.defaultNamespace = previousContext.defaultNamespace;
				this.previousContext = previousContext;
			}

			// Token: 0x04000350 RID: 848
			internal XmlSpace xmlSpace;

			// Token: 0x04000351 RID: 849
			internal string xmlLang;

			// Token: 0x04000352 RID: 850
			internal string defaultNamespace;

			// Token: 0x04000353 RID: 851
			internal XmlTextReaderImpl.XmlContext previousContext;
		}

		// Token: 0x0200007A RID: 122
		private class NoNamespaceManager : XmlNamespaceManager
		{
			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060006BB RID: 1723 RVA: 0x00015451 File Offset: 0x00013651
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x0000A558 File Offset: 0x00008758
			public override void PushScope()
			{
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
			public override bool PopScope()
			{
				return false;
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0000A558 File Offset: 0x00008758
			public override void AddNamespace(string prefix, string uri)
			{
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x0000A558 File Offset: 0x00008758
			public override void RemoveNamespace(string prefix, string uri)
			{
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x00014C6C File Offset: 0x00012E6C
			public override IEnumerator GetEnumerator()
			{
				return null;
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x00014C6C File Offset: 0x00012E6C
			public override IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
			{
				return null;
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x00015451 File Offset: 0x00013651
			public override string LookupNamespace(string prefix)
			{
				return string.Empty;
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x00014C6C File Offset: 0x00012E6C
			public override string LookupPrefix(string uri)
			{
				return null;
			}
		}

		// Token: 0x0200007B RID: 123
		internal class DtdParserProxy : IDtdParserAdapterV1, IDtdParserAdapterWithValidation, IDtdParserAdapter
		{
			// Token: 0x060006C4 RID: 1732 RVA: 0x000249DD File Offset: 0x00022BDD
			internal DtdParserProxy(XmlTextReaderImpl reader)
			{
				this.reader = reader;
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060006C5 RID: 1733 RVA: 0x000249EC File Offset: 0x00022BEC
			XmlNameTable IDtdParserAdapter.NameTable
			{
				get
				{
					return this.reader.DtdParserProxy_NameTable;
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060006C6 RID: 1734 RVA: 0x000249F9 File Offset: 0x00022BF9
			IXmlNamespaceResolver IDtdParserAdapter.NamespaceResolver
			{
				get
				{
					return this.reader.DtdParserProxy_NamespaceResolver;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00024A06 File Offset: 0x00022C06
			Uri IDtdParserAdapter.BaseUri
			{
				get
				{
					return this.reader.DtdParserProxy_BaseUri;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00024A13 File Offset: 0x00022C13
			bool IDtdParserAdapter.IsEof
			{
				get
				{
					return this.reader.DtdParserProxy_IsEof;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00024A20 File Offset: 0x00022C20
			char[] IDtdParserAdapter.ParsingBuffer
			{
				get
				{
					return this.reader.DtdParserProxy_ParsingBuffer;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060006CA RID: 1738 RVA: 0x00024A2D File Offset: 0x00022C2D
			int IDtdParserAdapter.ParsingBufferLength
			{
				get
				{
					return this.reader.DtdParserProxy_ParsingBufferLength;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060006CB RID: 1739 RVA: 0x00024A3A File Offset: 0x00022C3A
			// (set) Token: 0x060006CC RID: 1740 RVA: 0x00024A47 File Offset: 0x00022C47
			int IDtdParserAdapter.CurrentPosition
			{
				get
				{
					return this.reader.DtdParserProxy_CurrentPosition;
				}
				set
				{
					this.reader.DtdParserProxy_CurrentPosition = value;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060006CD RID: 1741 RVA: 0x00024A55 File Offset: 0x00022C55
			int IDtdParserAdapter.EntityStackLength
			{
				get
				{
					return this.reader.DtdParserProxy_EntityStackLength;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060006CE RID: 1742 RVA: 0x00024A62 File Offset: 0x00022C62
			bool IDtdParserAdapter.IsEntityEolNormalized
			{
				get
				{
					return this.reader.DtdParserProxy_IsEntityEolNormalized;
				}
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x00024A6F File Offset: 0x00022C6F
			void IDtdParserAdapter.OnNewLine(int pos)
			{
				this.reader.DtdParserProxy_OnNewLine(pos);
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00024A7D File Offset: 0x00022C7D
			int IDtdParserAdapter.LineNo
			{
				get
				{
					return this.reader.DtdParserProxy_LineNo;
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00024A8A File Offset: 0x00022C8A
			int IDtdParserAdapter.LineStartPosition
			{
				get
				{
					return this.reader.DtdParserProxy_LineStartPosition;
				}
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x00024A97 File Offset: 0x00022C97
			int IDtdParserAdapter.ReadData()
			{
				return this.reader.DtdParserProxy_ReadData();
			}

			// Token: 0x060006D3 RID: 1747 RVA: 0x00024AA4 File Offset: 0x00022CA4
			int IDtdParserAdapter.ParseNumericCharRef(StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNumericCharRef(internalSubsetBuilder);
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x00024AB2 File Offset: 0x00022CB2
			int IDtdParserAdapter.ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNamedCharRef(expand, internalSubsetBuilder);
			}

			// Token: 0x060006D5 RID: 1749 RVA: 0x00024AC1 File Offset: 0x00022CC1
			void IDtdParserAdapter.ParsePI(StringBuilder sb)
			{
				this.reader.DtdParserProxy_ParsePI(sb);
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x00024ACF File Offset: 0x00022CCF
			void IDtdParserAdapter.ParseComment(StringBuilder sb)
			{
				this.reader.DtdParserProxy_ParseComment(sb);
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x00024ADD File Offset: 0x00022CDD
			bool IDtdParserAdapter.PushEntity(IDtdEntityInfo entity, out int entityId)
			{
				return this.reader.DtdParserProxy_PushEntity(entity, out entityId);
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x00024AEC File Offset: 0x00022CEC
			bool IDtdParserAdapter.PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId)
			{
				return this.reader.DtdParserProxy_PopEntity(out oldEntity, out newEntityId);
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x00024AFB File Offset: 0x00022CFB
			bool IDtdParserAdapter.PushExternalSubset(string systemId, string publicId)
			{
				return this.reader.DtdParserProxy_PushExternalSubset(systemId, publicId);
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x00024B0A File Offset: 0x00022D0A
			void IDtdParserAdapter.PushInternalDtd(string baseUri, string internalDtd)
			{
				this.reader.DtdParserProxy_PushInternalDtd(baseUri, internalDtd);
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x00024B19 File Offset: 0x00022D19
			void IDtdParserAdapter.Throw(Exception e)
			{
				this.reader.DtdParserProxy_Throw(e);
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x00024B27 File Offset: 0x00022D27
			void IDtdParserAdapter.OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo)
			{
				this.reader.DtdParserProxy_OnSystemId(systemId, keywordLineInfo, systemLiteralLineInfo);
			}

			// Token: 0x060006DD RID: 1757 RVA: 0x00024B37 File Offset: 0x00022D37
			void IDtdParserAdapter.OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo)
			{
				this.reader.DtdParserProxy_OnPublicId(publicId, keywordLineInfo, publicLiteralLineInfo);
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060006DE RID: 1758 RVA: 0x00024B47 File Offset: 0x00022D47
			bool IDtdParserAdapterWithValidation.DtdValidation
			{
				get
				{
					return this.reader.DtdParserProxy_DtdValidation;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060006DF RID: 1759 RVA: 0x00024B54 File Offset: 0x00022D54
			IValidationEventHandling IDtdParserAdapterWithValidation.ValidationEventHandling
			{
				get
				{
					return this.reader.DtdParserProxy_ValidationEventHandling;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00024B61 File Offset: 0x00022D61
			bool IDtdParserAdapterV1.Normalization
			{
				get
				{
					return this.reader.DtdParserProxy_Normalization;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00024B6E File Offset: 0x00022D6E
			bool IDtdParserAdapterV1.Namespaces
			{
				get
				{
					return this.reader.DtdParserProxy_Namespaces;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00024B7B File Offset: 0x00022D7B
			bool IDtdParserAdapterV1.V1CompatibilityMode
			{
				get
				{
					return this.reader.DtdParserProxy_V1CompatibilityMode;
				}
			}

			// Token: 0x04000354 RID: 852
			private XmlTextReaderImpl reader;
		}

		// Token: 0x0200007C RID: 124
		private class NodeData : IComparable
		{
			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00024B88 File Offset: 0x00022D88
			internal static XmlTextReaderImpl.NodeData None
			{
				get
				{
					if (XmlTextReaderImpl.NodeData.s_None == null)
					{
						XmlTextReaderImpl.NodeData.s_None = new XmlTextReaderImpl.NodeData();
					}
					return XmlTextReaderImpl.NodeData.s_None;
				}
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x00024BA6 File Offset: 0x00022DA6
			internal NodeData()
			{
				this.Clear(XmlNodeType.None);
				this.xmlContextPushed = false;
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00024BBC File Offset: 0x00022DBC
			internal int LineNo
			{
				get
				{
					return this.lineInfo.lineNo;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00024BC9 File Offset: 0x00022DC9
			internal int LinePos
			{
				get
				{
					return this.lineInfo.linePos;
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00024BD6 File Offset: 0x00022DD6
			// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00024BE9 File Offset: 0x00022DE9
			internal bool IsEmptyElement
			{
				get
				{
					return this.type == XmlNodeType.Element && this.isEmptyOrDefault;
				}
				set
				{
					this.isEmptyOrDefault = value;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00024BF2 File Offset: 0x00022DF2
			// (set) Token: 0x060006EA RID: 1770 RVA: 0x00024BE9 File Offset: 0x00022DE9
			internal bool IsDefaultAttribute
			{
				get
				{
					return this.type == XmlNodeType.Attribute && this.isEmptyOrDefault;
				}
				set
				{
					this.isEmptyOrDefault = value;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060006EB RID: 1771 RVA: 0x00024C05 File Offset: 0x00022E05
			internal bool ValueBuffered
			{
				get
				{
					return this.value == null;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060006EC RID: 1772 RVA: 0x00024C10 File Offset: 0x00022E10
			internal string StringValue
			{
				get
				{
					if (this.value == null)
					{
						this.value = new string(this.chars, this.valueStartPos, this.valueLength);
					}
					return this.value;
				}
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x00024C3D File Offset: 0x00022E3D
			internal void TrimSpacesInValue()
			{
				if (this.ValueBuffered)
				{
					XmlTextReaderImpl.StripSpaces(this.chars, this.valueStartPos, ref this.valueLength);
					return;
				}
				this.value = XmlTextReaderImpl.StripSpaces(this.value);
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x00024C70 File Offset: 0x00022E70
			internal void Clear(XmlNodeType type)
			{
				this.type = type;
				this.ClearName();
				this.value = string.Empty;
				this.valueStartPos = -1;
				this.nameWPrefix = string.Empty;
				this.schemaType = null;
				this.typedValue = null;
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x00024CAA File Offset: 0x00022EAA
			internal void ClearName()
			{
				this.localName = string.Empty;
				this.prefix = string.Empty;
				this.ns = string.Empty;
				this.nameWPrefix = string.Empty;
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x00024CD8 File Offset: 0x00022ED8
			internal void SetLineInfo(int lineNo, int linePos)
			{
				this.lineInfo.Set(lineNo, linePos);
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x00024CE7 File Offset: 0x00022EE7
			internal void SetLineInfo2(int lineNo, int linePos)
			{
				this.lineInfo2.Set(lineNo, linePos);
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x00024CF6 File Offset: 0x00022EF6
			internal void SetValueNode(XmlNodeType type, string value)
			{
				this.type = type;
				this.ClearName();
				this.value = value;
				this.valueStartPos = -1;
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x00024D13 File Offset: 0x00022F13
			internal void SetValueNode(XmlNodeType type, char[] chars, int startPos, int len)
			{
				this.type = type;
				this.ClearName();
				this.value = null;
				this.chars = chars;
				this.valueStartPos = startPos;
				this.valueLength = len;
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x00024D3F File Offset: 0x00022F3F
			internal void SetNamedNode(XmlNodeType type, string localName)
			{
				this.SetNamedNode(type, localName, string.Empty, localName);
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x00024D4F File Offset: 0x00022F4F
			internal void SetNamedNode(XmlNodeType type, string localName, string prefix, string nameWPrefix)
			{
				this.type = type;
				this.localName = localName;
				this.prefix = prefix;
				this.nameWPrefix = nameWPrefix;
				this.ns = string.Empty;
				this.value = string.Empty;
				this.valueStartPos = -1;
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x00024D8B File Offset: 0x00022F8B
			internal void SetValue(string value)
			{
				this.valueStartPos = -1;
				this.value = value;
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x00024D9B File Offset: 0x00022F9B
			internal void SetValue(char[] chars, int startPos, int len)
			{
				this.value = null;
				this.chars = chars;
				this.valueStartPos = startPos;
				this.valueLength = len;
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x00024DB9 File Offset: 0x00022FB9
			internal void OnBufferInvalidated()
			{
				if (this.value == null)
				{
					this.value = new string(this.chars, this.valueStartPos, this.valueLength);
				}
				this.valueStartPos = -1;
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x00024DE8 File Offset: 0x00022FE8
			internal void CopyTo(int valueOffset, StringBuilder sb)
			{
				if (this.value == null)
				{
					sb.Append(this.chars, this.valueStartPos + valueOffset, this.valueLength - valueOffset);
					return;
				}
				if (valueOffset <= 0)
				{
					sb.Append(this.value);
					return;
				}
				sb.Append(this.value, valueOffset, this.value.Length - valueOffset);
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x00024E48 File Offset: 0x00023048
			internal int CopyTo(int valueOffset, char[] buffer, int offset, int length)
			{
				if (this.value == null)
				{
					int num = this.valueLength - valueOffset;
					if (num > length)
					{
						num = length;
					}
					XmlTextReaderImpl.BlockCopyChars(this.chars, this.valueStartPos + valueOffset, buffer, offset, num);
					return num;
				}
				int num2 = this.value.Length - valueOffset;
				if (num2 > length)
				{
					num2 = length;
				}
				this.value.CopyTo(valueOffset, buffer, offset, num2);
				return num2;
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x00024EAC File Offset: 0x000230AC
			internal int CopyToBinary(IncrementalReadDecoder decoder, int valueOffset)
			{
				if (this.value == null)
				{
					return decoder.Decode(this.chars, this.valueStartPos + valueOffset, this.valueLength - valueOffset);
				}
				return decoder.Decode(this.value, valueOffset, this.value.Length - valueOffset);
			}

			// Token: 0x060006FC RID: 1788 RVA: 0x00024EF8 File Offset: 0x000230F8
			internal void AdjustLineInfo(int valueOffset, bool isNormalized, ref LineInfo lineInfo)
			{
				if (valueOffset == 0)
				{
					return;
				}
				if (this.valueStartPos != -1)
				{
					XmlTextReaderImpl.AdjustLineInfo(this.chars, this.valueStartPos, this.valueStartPos + valueOffset, isNormalized, ref lineInfo);
					return;
				}
				XmlTextReaderImpl.AdjustLineInfo(this.value, 0, valueOffset, isNormalized, ref lineInfo);
			}

			// Token: 0x060006FD RID: 1789 RVA: 0x00024F32 File Offset: 0x00023132
			internal string GetNameWPrefix(XmlNameTable nt)
			{
				if (this.nameWPrefix != null)
				{
					return this.nameWPrefix;
				}
				return this.CreateNameWPrefix(nt);
			}

			// Token: 0x060006FE RID: 1790 RVA: 0x00024F4C File Offset: 0x0002314C
			internal string CreateNameWPrefix(XmlNameTable nt)
			{
				if (this.prefix.Length == 0)
				{
					this.nameWPrefix = this.localName;
				}
				else
				{
					this.nameWPrefix = nt.Add(this.prefix + ":" + this.localName);
				}
				return this.nameWPrefix;
			}

			// Token: 0x060006FF RID: 1791 RVA: 0x00024F9C File Offset: 0x0002319C
			int IComparable.CompareTo(object obj)
			{
				XmlTextReaderImpl.NodeData nodeData = obj as XmlTextReaderImpl.NodeData;
				if (nodeData == null)
				{
					return 1;
				}
				if (!Ref.Equal(this.localName, nodeData.localName))
				{
					return string.CompareOrdinal(this.localName, nodeData.localName);
				}
				if (Ref.Equal(this.ns, nodeData.ns))
				{
					return 0;
				}
				return string.CompareOrdinal(this.ns, nodeData.ns);
			}

			// Token: 0x04000355 RID: 853
			private static volatile XmlTextReaderImpl.NodeData s_None;

			// Token: 0x04000356 RID: 854
			internal XmlNodeType type;

			// Token: 0x04000357 RID: 855
			internal string localName;

			// Token: 0x04000358 RID: 856
			internal string prefix;

			// Token: 0x04000359 RID: 857
			internal string ns;

			// Token: 0x0400035A RID: 858
			internal string nameWPrefix;

			// Token: 0x0400035B RID: 859
			private string value;

			// Token: 0x0400035C RID: 860
			private char[] chars;

			// Token: 0x0400035D RID: 861
			private int valueStartPos;

			// Token: 0x0400035E RID: 862
			private int valueLength;

			// Token: 0x0400035F RID: 863
			internal LineInfo lineInfo;

			// Token: 0x04000360 RID: 864
			internal LineInfo lineInfo2;

			// Token: 0x04000361 RID: 865
			internal char quoteChar;

			// Token: 0x04000362 RID: 866
			internal int depth;

			// Token: 0x04000363 RID: 867
			private bool isEmptyOrDefault;

			// Token: 0x04000364 RID: 868
			internal int entityId;

			// Token: 0x04000365 RID: 869
			internal bool xmlContextPushed;

			// Token: 0x04000366 RID: 870
			internal XmlTextReaderImpl.NodeData nextAttrValueChunk;

			// Token: 0x04000367 RID: 871
			internal object schemaType;

			// Token: 0x04000368 RID: 872
			internal object typedValue;
		}

		// Token: 0x0200007D RID: 125
		private class DtdDefaultAttributeInfoToNodeDataComparer : IComparer<object>
		{
			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000700 RID: 1792 RVA: 0x00025000 File Offset: 0x00023200
			internal static IComparer<object> Instance
			{
				get
				{
					return XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.s_instance;
				}
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00025008 File Offset: 0x00023208
			public int Compare(object x, object y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y == null)
					{
						return 1;
					}
					XmlTextReaderImpl.NodeData nodeData = x as XmlTextReaderImpl.NodeData;
					string text;
					string text2;
					if (nodeData != null)
					{
						text = nodeData.localName;
						text2 = nodeData.prefix;
					}
					else
					{
						IDtdDefaultAttributeInfo dtdDefaultAttributeInfo = x as IDtdDefaultAttributeInfo;
						if (dtdDefaultAttributeInfo == null)
						{
							throw new XmlException("An XML error has occurred.", string.Empty);
						}
						text = dtdDefaultAttributeInfo.LocalName;
						text2 = dtdDefaultAttributeInfo.Prefix;
					}
					nodeData = y as XmlTextReaderImpl.NodeData;
					string text3;
					string text4;
					if (nodeData != null)
					{
						text3 = nodeData.localName;
						text4 = nodeData.prefix;
					}
					else
					{
						IDtdDefaultAttributeInfo dtdDefaultAttributeInfo2 = y as IDtdDefaultAttributeInfo;
						if (dtdDefaultAttributeInfo2 == null)
						{
							throw new XmlException("An XML error has occurred.", string.Empty);
						}
						text3 = dtdDefaultAttributeInfo2.LocalName;
						text4 = dtdDefaultAttributeInfo2.Prefix;
					}
					int num = string.Compare(text, text3, StringComparison.Ordinal);
					if (num != 0)
					{
						return num;
					}
					return string.Compare(text2, text4, StringComparison.Ordinal);
				}
			}

			// Token: 0x04000369 RID: 873
			private static IComparer<object> s_instance = new XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer();
		}

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x06000705 RID: 1797
		internal delegate void OnDefaultAttributeUseDelegate(IDtdDefaultAttributeInfo defaultAttribute, XmlTextReaderImpl coreReader);
	}
}
