using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x0200009C RID: 156
	internal sealed class XmlValidatingReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x0002CA18 File Offset: 0x0002AC18
		internal XmlValidatingReaderImpl(XmlReader reader, ValidationEventHandler settingsEventHandler, bool processIdentityConstraints)
		{
			XmlAsyncCheckReader xmlAsyncCheckReader = reader as XmlAsyncCheckReader;
			if (xmlAsyncCheckReader != null)
			{
				reader = xmlAsyncCheckReader.CoreReader;
			}
			this.outerReader = this;
			this.coreReader = reader;
			this.coreReaderImpl = reader as XmlTextReaderImpl;
			if (this.coreReaderImpl == null)
			{
				XmlTextReader xmlTextReader = reader as XmlTextReader;
				if (xmlTextReader != null)
				{
					this.coreReaderImpl = xmlTextReader.Impl;
				}
			}
			if (this.coreReaderImpl == null)
			{
				throw new ArgumentException(Res.GetString("The XmlReader passed in to construct this XmlValidatingReaderImpl must be an instance of a System.Xml.XmlTextReader."), "reader");
			}
			this.coreReaderImpl.XmlValidatingReaderCompatibilityMode = true;
			this.coreReaderNSResolver = reader as IXmlNamespaceResolver;
			this.processIdentityConstraints = processIdentityConstraints;
			this.schemaCollection = new XmlSchemaCollection(this.coreReader.NameTable);
			this.schemaCollection.XmlResolver = this.GetResolver();
			this.eventHandling = new XmlValidatingReaderImpl.ValidationEventHandling(this);
			if (settingsEventHandler != null)
			{
				this.eventHandling.AddHandler(settingsEventHandler);
			}
			this.coreReaderImpl.ValidationEventHandling = this.eventHandling;
			this.coreReaderImpl.OnDefaultAttributeUse = new XmlTextReaderImpl.OnDefaultAttributeUseDelegate(this.ValidateDefaultAttributeOnUse);
			this.validationType = ValidationType.DTD;
			this.SetupValidation(ValidationType.DTD);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0002CB30 File Offset: 0x0002AD30
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings;
				if (this.coreReaderImpl.V1Compat)
				{
					xmlReaderSettings = null;
				}
				else
				{
					xmlReaderSettings = this.coreReader.Settings;
				}
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				else
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.ValidationType = ValidationType.DTD;
				if (!this.processIdentityConstraints)
				{
					xmlReaderSettings.ValidationFlags &= ~XmlSchemaValidationFlags.ProcessIdentityConstraints;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0002CB92 File Offset: 0x0002AD92
		public override XmlNodeType NodeType
		{
			get
			{
				return this.coreReader.NodeType;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0002CB9F File Offset: 0x0002AD9F
		public override string Name
		{
			get
			{
				return this.coreReader.Name;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0002CBAC File Offset: 0x0002ADAC
		public override string LocalName
		{
			get
			{
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002CBB9 File Offset: 0x0002ADB9
		public override string NamespaceURI
		{
			get
			{
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0002CBC6 File Offset: 0x0002ADC6
		public override string Prefix
		{
			get
			{
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0002CBD3 File Offset: 0x0002ADD3
		public override bool HasValue
		{
			get
			{
				return this.coreReader.HasValue;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0002CBE0 File Offset: 0x0002ADE0
		public override string Value
		{
			get
			{
				return this.coreReader.Value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0002CBED File Offset: 0x0002ADED
		public override int Depth
		{
			get
			{
				return this.coreReader.Depth;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0002CBFA File Offset: 0x0002ADFA
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0002CC07 File Offset: 0x0002AE07
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0002CC14 File Offset: 0x0002AE14
		public override bool IsDefault
		{
			get
			{
				return this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0002CC21 File Offset: 0x0002AE21
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0002CC2E File Offset: 0x0002AE2E
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0002CC3B File Offset: 0x0002AE3B
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0002CC48 File Offset: 0x0002AE48
		public override ReadState ReadState
		{
			get
			{
				if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0002CC60 File Offset: 0x0002AE60
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0002CC6D File Offset: 0x0002AE6D
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReader.NameTable;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0002CC7A File Offset: 0x0002AE7A
		public override int AttributeCount
		{
			get
			{
				return this.coreReader.AttributeCount;
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0002CC87 File Offset: 0x0002AE87
		public override string GetAttribute(string name)
		{
			return this.coreReader.GetAttribute(name);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002CC95 File Offset: 0x0002AE95
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.coreReader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
		public override string GetAttribute(int i)
		{
			return this.coreReader.GetAttribute(i);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002CCB2 File Offset: 0x0002AEB2
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002CCCC File Offset: 0x0002AECC
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			if (!this.coreReader.MoveToAttribute(localName, namespaceURI))
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002CCE7 File Offset: 0x0002AEE7
		public override void MoveToAttribute(int i)
		{
			this.coreReader.MoveToAttribute(i);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002CCFC File Offset: 0x0002AEFC
		public override bool MoveToFirstAttribute()
		{
			if (!this.coreReader.MoveToFirstAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002CD15 File Offset: 0x0002AF15
		public override bool MoveToNextAttribute()
		{
			if (!this.coreReader.MoveToNextAttribute())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002CD2E File Offset: 0x0002AF2E
		public override bool MoveToElement()
		{
			if (!this.coreReader.MoveToElement())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002CD48 File Offset: 0x0002AF48
		public override bool Read()
		{
			switch (this.parsingFunction)
			{
			case XmlValidatingReaderImpl.ParsingFunction.Read:
				break;
			case XmlValidatingReaderImpl.ParsingFunction.Init:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessCoreReaderEvent();
					return true;
				}
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ParseDtdFromContext:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ParseDtdFromParserContext();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.ResolveEntityInternally();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent:
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
				break;
			case XmlValidatingReaderImpl.ParsingFunction.ReaderClosed:
			case XmlValidatingReaderImpl.ParsingFunction.Error:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessCoreReaderEvent();
				return true;
			}
			this.validator.CompleteValidation();
			return false;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		public override void Close()
		{
			this.coreReader.Close();
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ReaderClosed;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002CE08 File Offset: 0x0002B008
		public override string LookupNamespace(string prefix)
		{
			return this.coreReaderImpl.LookupNamespace(prefix);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0002CE16 File Offset: 0x0002B016
		public override bool ReadAttributeValue()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
				this.readBinaryHelper.Finish();
			}
			if (!this.coreReader.ReadAttributeValue())
			{
				return false;
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			return true;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002CE4C File Offset: 0x0002B04C
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002CEA0 File Offset: 0x0002B0A0
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002CF48 File Offset: 0x0002B148
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this.outerReader);
			}
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.InReadBinaryContent;
			return num;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002CF9C File Offset: 0x0002B19C
		public override void ResolveEntity()
		{
			if (this.parsingFunction == XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally)
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Read;
			}
			this.coreReader.ResolveEntity();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002CFB9 File Offset: 0x0002B1B9
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction != XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002CFEF File Offset: 0x0002B1EF
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0002CFFD File Offset: 0x0002B1FD
		public int LineNumber
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LineNumber;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0002D00F File Offset: 0x0002B20F
		public int LinePosition
		{
			get
			{
				return ((IXmlLineInfo)this.coreReader).LinePosition;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002D021 File Offset: 0x0002B221
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001B36B File Offset: 0x0001956B
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002D02A File Offset: 0x0002B22A
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002D033 File Offset: 0x0002B233
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.coreReaderNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002D041 File Offset: 0x0002B241
		internal string LookupPrefix(string namespaceName)
		{
			return this.coreReaderNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0002D04F File Offset: 0x0002B24F
		internal ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0002D057 File Offset: 0x0002B257
		internal XmlSchemaCollection Schemas
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0002D05F File Offset: 0x0002B25F
		internal bool Namespaces
		{
			get
			{
				return this.coreReaderImpl.Namespaces;
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002D06C File Offset: 0x0002B26C
		private void ParseDtdFromParserContext()
		{
			if (this.parserContext.DocTypeName == null || this.parserContext.DocTypeName.Length == 0)
			{
				return;
			}
			IDtdParser dtdParser = DtdParser.Create();
			XmlTextReaderImpl.DtdParserProxy dtdParserProxy = new XmlTextReaderImpl.DtdParserProxy(this.coreReaderImpl);
			IDtdInfo dtdInfo = dtdParser.ParseFreeFloatingDtd(this.parserContext.BaseURI, this.parserContext.DocTypeName, this.parserContext.PublicId, this.parserContext.SystemId, this.parserContext.InternalSubset, dtdParserProxy);
			this.coreReaderImpl.SetDtdInfo(dtdInfo);
			this.ValidateDtd();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002D0FC File Offset: 0x0002B2FC
		private void ValidateDtd()
		{
			IDtdInfo dtdInfo = this.coreReaderImpl.DtdInfo;
			if (dtdInfo != null)
			{
				switch (this.validationType)
				{
				case ValidationType.None:
				case ValidationType.DTD:
					break;
				case ValidationType.Auto:
					this.SetupValidation(ValidationType.DTD);
					break;
				default:
					return;
				}
				this.validator.DtdInfo = dtdInfo;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002D148 File Offset: 0x0002B348
		private void ResolveEntityInternally()
		{
			int depth = this.coreReader.Depth;
			this.outerReader.ResolveEntity();
			while (this.outerReader.Read() && this.coreReader.Depth > depth)
			{
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002D188 File Offset: 0x0002B388
		private void SetupValidation(ValidationType valType)
		{
			this.validator = BaseValidator.CreateInstance(valType, this, this.schemaCollection, this.eventHandling, this.processIdentityConstraints);
			XmlResolver resolver = this.GetResolver();
			this.validator.XmlResolver = resolver;
			if (this.outerReader.BaseURI.Length > 0)
			{
				this.validator.BaseUri = ((resolver == null) ? new Uri(this.outerReader.BaseURI, UriKind.RelativeOrAbsolute) : resolver.ResolveUri(null, this.outerReader.BaseURI));
			}
			this.coreReaderImpl.ValidationEventHandling = ((this.validationType == ValidationType.None) ? null : this.eventHandling);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002D22C File Offset: 0x0002B42C
		private XmlResolver GetResolver()
		{
			XmlResolver resolver = this.coreReaderImpl.GetResolver();
			if (resolver == null && !this.coreReaderImpl.IsResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
			{
				if (XmlValidatingReaderImpl.s_tempResolver == null)
				{
					XmlValidatingReaderImpl.s_tempResolver = new XmlUrlResolver();
				}
				return XmlValidatingReaderImpl.s_tempResolver;
			}
			return resolver;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002D274 File Offset: 0x0002B474
		private void ProcessCoreReaderEvent()
		{
			XmlNodeType nodeType = this.coreReader.NodeType;
			if (nodeType != XmlNodeType.EntityReference)
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.ValidateDtd();
					return;
				}
				if (nodeType == XmlNodeType.Whitespace && (this.coreReader.Depth > 0 || this.coreReaderImpl.FragmentType != XmlNodeType.Document) && this.validator.PreserveWhitespace)
				{
					this.coreReaderImpl.ChangeCurrentNodeType(XmlNodeType.SignificantWhitespace);
				}
			}
			else
			{
				this.parsingFunction = XmlValidatingReaderImpl.ParsingFunction.ResolveEntityInternally;
			}
			this.coreReaderImpl.InternalSchemaType = null;
			this.coreReaderImpl.InternalTypedValue = null;
			this.validator.Validate();
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0002D305 File Offset: 0x0002B505
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0002D30D File Offset: 0x0002B50D
		internal BaseValidator Validator
		{
			get
			{
				return this.validator;
			}
			set
			{
				this.validator = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0002D316 File Offset: 0x0002B516
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.coreReaderImpl.NamespaceManager;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0002D323 File Offset: 0x0002B523
		internal bool StandAlone
		{
			get
			{
				return this.coreReaderImpl.StandAlone;
			}
		}

		// Token: 0x17000198 RID: 408
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0002D330 File Offset: 0x0002B530
		internal object SchemaTypeObject
		{
			set
			{
				this.coreReaderImpl.InternalSchemaType = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0002D33E File Offset: 0x0002B53E
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x0002D34B File Offset: 0x0002B54B
		internal object TypedValueObject
		{
			get
			{
				return this.coreReaderImpl.InternalTypedValue;
			}
			set
			{
				this.coreReaderImpl.InternalTypedValue = value;
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002D359 File Offset: 0x0002B559
		internal bool AddDefaultAttribute(SchemaAttDef attdef)
		{
			return this.coreReaderImpl.AddDefaultAttributeNonDtd(attdef);
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0002D367 File Offset: 0x0002B567
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.coreReaderImpl.DtdInfo;
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002D374 File Offset: 0x0002B574
		internal void ValidateDefaultAttributeOnUse(IDtdDefaultAttributeInfo defaultAttribute, XmlTextReaderImpl coreReader)
		{
			SchemaAttDef schemaAttDef = defaultAttribute as SchemaAttDef;
			if (schemaAttDef == null)
			{
				return;
			}
			if (!schemaAttDef.DefaultValueChecked)
			{
				SchemaInfo schemaInfo = coreReader.DtdInfo as SchemaInfo;
				if (schemaInfo == null)
				{
					return;
				}
				DtdValidator.CheckDefaultValue(schemaAttDef, schemaInfo, this.eventHandling, coreReader.BaseURI);
			}
		}

		// Token: 0x04000457 RID: 1111
		private XmlReader coreReader;

		// Token: 0x04000458 RID: 1112
		private XmlTextReaderImpl coreReaderImpl;

		// Token: 0x04000459 RID: 1113
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x0400045A RID: 1114
		private ValidationType validationType;

		// Token: 0x0400045B RID: 1115
		private BaseValidator validator;

		// Token: 0x0400045C RID: 1116
		private XmlSchemaCollection schemaCollection;

		// Token: 0x0400045D RID: 1117
		private bool processIdentityConstraints;

		// Token: 0x0400045E RID: 1118
		private XmlValidatingReaderImpl.ParsingFunction parsingFunction = XmlValidatingReaderImpl.ParsingFunction.Init;

		// Token: 0x0400045F RID: 1119
		private XmlValidatingReaderImpl.ValidationEventHandling eventHandling;

		// Token: 0x04000460 RID: 1120
		private XmlParserContext parserContext;

		// Token: 0x04000461 RID: 1121
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x04000462 RID: 1122
		private XmlReader outerReader;

		// Token: 0x04000463 RID: 1123
		private static XmlResolver s_tempResolver;

		// Token: 0x0200009D RID: 157
		private enum ParsingFunction
		{
			// Token: 0x04000465 RID: 1125
			Read,
			// Token: 0x04000466 RID: 1126
			Init,
			// Token: 0x04000467 RID: 1127
			ParseDtdFromContext,
			// Token: 0x04000468 RID: 1128
			ResolveEntityInternally,
			// Token: 0x04000469 RID: 1129
			InReadBinaryContent,
			// Token: 0x0400046A RID: 1130
			ReaderClosed,
			// Token: 0x0400046B RID: 1131
			Error,
			// Token: 0x0400046C RID: 1132
			None
		}

		// Token: 0x0200009E RID: 158
		internal class ValidationEventHandling : IValidationEventHandling
		{
			// Token: 0x0600084A RID: 2122 RVA: 0x0002D3B7 File Offset: 0x0002B5B7
			internal ValidationEventHandling(XmlValidatingReaderImpl reader)
			{
				this.reader = reader;
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x0600084B RID: 2123 RVA: 0x0002D3C6 File Offset: 0x0002B5C6
			object IValidationEventHandling.EventHandler
			{
				get
				{
					return this.eventHandler;
				}
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x0002D3CE File Offset: 0x0002B5CE
			void IValidationEventHandling.SendEvent(Exception exception, XmlSeverityType severity)
			{
				if (this.eventHandler != null)
				{
					this.eventHandler(this.reader, new ValidationEventArgs((XmlSchemaException)exception, severity));
					return;
				}
				if (this.reader.ValidationType != ValidationType.None && severity == XmlSeverityType.Error)
				{
					throw exception;
				}
			}

			// Token: 0x0600084D RID: 2125 RVA: 0x0002D408 File Offset: 0x0002B608
			internal void AddHandler(ValidationEventHandler handler)
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, handler);
			}

			// Token: 0x0400046D RID: 1133
			private XmlValidatingReaderImpl reader;

			// Token: 0x0400046E RID: 1134
			private ValidationEventHandler eventHandler;
		}
	}
}
