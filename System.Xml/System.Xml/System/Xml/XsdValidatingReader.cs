using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000CA RID: 202
	internal class XsdValidatingReader : XmlReader, IXmlSchemaInfo, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x00035A4C File Offset: 0x00033C4C
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings, XmlSchemaObject partialValidationType)
		{
			this.coreReader = reader;
			this.coreReaderNSResolver = reader as IXmlNamespaceResolver;
			this.lineInfo = reader as IXmlLineInfo;
			this.coreReaderNameTable = this.coreReader.NameTable;
			if (this.coreReaderNSResolver == null)
			{
				this.nsManager = new XmlNamespaceManager(this.coreReaderNameTable);
				this.manageNamespaces = true;
			}
			this.thisNSResolver = this;
			this.xmlResolver = xmlResolver;
			this.processInlineSchema = (readerSettings.ValidationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) > XmlSchemaValidationFlags.None;
			this.Init();
			this.SetupValidator(readerSettings, reader, partialValidationType);
			this.validationEvent = readerSettings.GetEventHandler();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00035AF5 File Offset: 0x00033CF5
		internal XsdValidatingReader(XmlReader reader, XmlResolver xmlResolver, XmlReaderSettings readerSettings)
			: this(reader, xmlResolver, readerSettings, null)
		{
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00035B04 File Offset: 0x00033D04
		private void Init()
		{
			this.validationState = XsdValidatingReader.ValidatingReaderState.Init;
			this.defaultAttributes = new ArrayList();
			this.currentAttrIndex = -1;
			this.attributePSVINodes = new AttributePSVIInfo[8];
			this.valueGetter = new XmlValueGetter(this.GetStringValue);
			XsdValidatingReader.TypeOfString = typeof(string);
			this.xmlSchemaInfo = new XmlSchemaInfo();
			this.NsXmlNs = this.coreReaderNameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXs = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = this.coreReaderNameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = this.coreReaderNameTable.Add("type");
			this.XsiNil = this.coreReaderNameTable.Add("nil");
			this.XsiSchemaLocation = this.coreReaderNameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = this.coreReaderNameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = this.coreReaderNameTable.Add("schema");
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00035C14 File Offset: 0x00033E14
		private void SetupValidator(XmlReaderSettings readerSettings, XmlReader reader, XmlSchemaObject partialValidationType)
		{
			this.validator = new XmlSchemaValidator(this.coreReaderNameTable, readerSettings.Schemas, this.thisNSResolver, readerSettings.ValidationFlags);
			this.validator.XmlResolver = this.xmlResolver;
			this.validator.SourceUri = XmlConvert.ToUri(reader.BaseURI);
			this.validator.ValidationEventSender = this;
			this.validator.ValidationEventHandler += readerSettings.GetEventHandler();
			this.validator.LineInfoProvider = this.lineInfo;
			if (this.validator.ProcessSchemaHints)
			{
				this.validator.SchemaSet.ReaderSettings.DtdProcessing = readerSettings.DtdProcessing;
			}
			this.validator.SetDtdSchemaInfo(reader.DtdInfo);
			if (partialValidationType != null)
			{
				this.validator.Initialize(partialValidationType);
				return;
			}
			this.validator.Initialize();
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00035CF0 File Offset: 0x00033EF0
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = this.coreReader.Settings;
				if (xmlReaderSettings != null)
				{
					xmlReaderSettings = xmlReaderSettings.Clone();
				}
				if (xmlReaderSettings == null)
				{
					xmlReaderSettings = new XmlReaderSettings();
				}
				xmlReaderSettings.Schemas = this.validator.SchemaSet;
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.ValidationFlags = this.validator.ValidationFlags;
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00035D50 File Offset: 0x00033F50
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.NodeType;
				}
				XmlNodeType nodeType = this.coreReader.NodeType;
				if (nodeType == XmlNodeType.Whitespace && (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly || this.validator.CurrentContentType == XmlSchemaContentType.Mixed))
				{
					return XmlNodeType.SignificantWhitespace;
				}
				return nodeType;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00035DA4 File Offset: 0x00033FA4
		public override string Name
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
				{
					return this.coreReader.Name;
				}
				string defaultAttributePrefix = this.validator.GetDefaultAttributePrefix(this.cachedNode.Namespace);
				if (defaultAttributePrefix != null && defaultAttributePrefix.Length != 0)
				{
					return string.Concat(new string[] { defaultAttributePrefix + ":" + this.cachedNode.LocalName });
				}
				return this.cachedNode.LocalName;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00035E18 File Offset: 0x00034018
		public override string LocalName
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.LocalName;
				}
				return this.coreReader.LocalName;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00035E3A File Offset: 0x0003403A
		public override string NamespaceURI
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Namespace;
				}
				return this.coreReader.NamespaceURI;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00035E5C File Offset: 0x0003405C
		public override string Prefix
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Prefix;
				}
				return this.coreReader.Prefix;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00035E7E File Offset: 0x0003407E
		public override bool HasValue
		{
			get
			{
				return this.validationState < XsdValidatingReader.ValidatingReaderState.None || this.coreReader.HasValue;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00035E96 File Offset: 0x00034096
		public override string Value
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.RawValue;
				}
				return this.coreReader.Value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00035EB8 File Offset: 0x000340B8
		public override int Depth
		{
			get
			{
				if (this.validationState < XsdValidatingReader.ValidatingReaderState.None)
				{
					return this.cachedNode.Depth;
				}
				return this.coreReader.Depth;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00035EDA File Offset: 0x000340DA
		public override string BaseURI
		{
			get
			{
				return this.coreReader.BaseURI;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00035EE7 File Offset: 0x000340E7
		public override bool IsEmptyElement
		{
			get
			{
				return this.coreReader.IsEmptyElement;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00035EF4 File Offset: 0x000340F4
		public override bool IsDefault
		{
			get
			{
				return this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute || this.coreReader.IsDefault;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00035F0C File Offset: 0x0003410C
		public override char QuoteChar
		{
			get
			{
				return this.coreReader.QuoteChar;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00035F19 File Offset: 0x00034119
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.coreReader.XmlSpace;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00035F26 File Offset: 0x00034126
		public override string XmlLang
		{
			get
			{
				return this.coreReader.XmlLang;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00035F33 File Offset: 0x00034133
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00035F38 File Offset: 0x00034138
		public override Type ValueType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_0062;
						}
					}
					else
					{
						if (this.attributePSVI != null && this.AttributeSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
						{
							return this.AttributeSchemaInfo.SchemaType.Datatype.ValueType;
						}
						goto IL_0062;
					}
				}
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					return this.xmlSchemaInfo.SchemaType.Datatype.ValueType;
				}
				IL_0062:
				return XsdValidatingReader.TypeOfString;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00035FAE File Offset: 0x000341AE
		public override object ReadContentAsObject()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsObject(true);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00035FD0 File Offset: 0x000341D0
		public override bool ReadContentAsBoolean()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsBoolean");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			bool flag;
			try
			{
				if (xmlSchemaType != null)
				{
					flag = xmlSchemaType.ValueConverter.ToBoolean(obj);
				}
				else
				{
					flag = XmlUntypedConverter.Untyped.ToBoolean(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex3, this);
			}
			return flag;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0003609C File Offset: 0x0003429C
		public override DateTime ReadContentAsDateTime()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDateTime");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			DateTime dateTime;
			try
			{
				if (xmlSchemaType != null)
				{
					dateTime = xmlSchemaType.ValueConverter.ToDateTime(obj);
				}
				else
				{
					dateTime = XmlUntypedConverter.Untyped.ToDateTime(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex3, this);
			}
			return dateTime;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00036168 File Offset: 0x00034368
		public override double ReadContentAsDouble()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDouble");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			double num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDouble(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDouble(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex3, this);
			}
			return num;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00036234 File Offset: 0x00034434
		public override float ReadContentAsFloat()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsFloat");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			float num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToSingle(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToSingle(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex3, this);
			}
			return num;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00036300 File Offset: 0x00034500
		public override decimal ReadContentAsDecimal()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsDecimal");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			decimal num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDecimal(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDecimal(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex3, this);
			}
			return num;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000363CC File Offset: 0x000345CC
		public override int ReadContentAsInt()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsInt");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			int num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt32(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt32(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex3, this);
			}
			return num;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00036498 File Offset: 0x00034698
		public override long ReadContentAsLong()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsLong");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			long num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt64(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt64(obj);
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex3, this);
			}
			return num;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00036564 File Offset: 0x00034764
		public override string ReadContentAsString()
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAsString");
			}
			object obj = this.InternalReadContentAsObject();
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			string text;
			try
			{
				if (xmlSchemaType != null)
				{
					text = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					text = obj as string;
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex3, this);
			}
			return text;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0003662C File Offset: 0x0003482C
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!XmlReader.CanReadContentAs(this.NodeType))
			{
				throw base.CreateReadContentAsException("ReadContentAs");
			}
			string text;
			object obj = this.InternalReadContentAsObject(false, out text);
			XmlSchemaType xmlSchemaType = ((this.NodeType == XmlNodeType.Attribute) ? this.AttributeXmlType : this.ElementXmlType);
			object obj2;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						obj = text;
					}
					obj2 = xmlSchemaType.ValueConverter.ChangeType(obj, returnType);
				}
				else
				{
					obj2 = XmlUntypedConverter.Untyped.ChangeType(obj, returnType, namespaceResolver);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex3, this);
			}
			return obj2;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00036724 File Offset: 0x00034924
		public override object ReadElementContentAsObject()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsObject");
			}
			XmlSchemaType xmlSchemaType;
			return this.InternalReadElementContentAsObject(out xmlSchemaType, true);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00036750 File Offset: 0x00034950
		public override bool ReadElementContentAsBoolean()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsBoolean");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			bool flag;
			try
			{
				if (xmlSchemaType != null)
				{
					flag = xmlSchemaType.ValueConverter.ToBoolean(obj);
				}
				else
				{
					flag = XmlUntypedConverter.Untyped.ToBoolean(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex3, this);
			}
			return flag;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00036804 File Offset: 0x00034A04
		public override DateTime ReadElementContentAsDateTime()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDateTime");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			DateTime dateTime;
			try
			{
				if (xmlSchemaType != null)
				{
					dateTime = xmlSchemaType.ValueConverter.ToDateTime(obj);
				}
				else
				{
					dateTime = XmlUntypedConverter.Untyped.ToDateTime(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex3, this);
			}
			return dateTime;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000368B8 File Offset: 0x00034AB8
		public override double ReadElementContentAsDouble()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDouble");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			double num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDouble(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDouble(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex3, this);
			}
			return num;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0003696C File Offset: 0x00034B6C
		public override float ReadElementContentAsFloat()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsFloat");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			float num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToSingle(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToSingle(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex3, this);
			}
			return num;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00036A20 File Offset: 0x00034C20
		public override decimal ReadElementContentAsDecimal()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsDecimal");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			decimal num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToDecimal(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToDecimal(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex3, this);
			}
			return num;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00036AD4 File Offset: 0x00034CD4
		public override int ReadElementContentAsInt()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsInt");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			int num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt32(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt32(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex3, this);
			}
			return num;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00036B88 File Offset: 0x00034D88
		public override long ReadElementContentAsLong()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsLong");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			long num;
			try
			{
				if (xmlSchemaType != null)
				{
					num = xmlSchemaType.ValueConverter.ToInt64(obj);
				}
				else
				{
					num = XmlUntypedConverter.Untyped.ToInt64(obj);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex3, this);
			}
			return num;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00036C3C File Offset: 0x00034E3C
		public override string ReadElementContentAsString()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAsString");
			}
			XmlSchemaType xmlSchemaType;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType);
			string text;
			try
			{
				if (xmlSchemaType != null)
				{
					text = xmlSchemaType.ValueConverter.ToString(obj);
				}
				else
				{
					text = obj as string;
				}
			}
			catch (InvalidCastException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex, this);
			}
			catch (FormatException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "String", ex3, this);
			}
			return text;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00036CE8 File Offset: 0x00034EE8
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw base.CreateReadElementContentAsException("ReadElementContentAs");
			}
			XmlSchemaType xmlSchemaType;
			string text;
			object obj = this.InternalReadElementContentAsObject(out xmlSchemaType, false, out text);
			object obj2;
			try
			{
				if (xmlSchemaType != null)
				{
					if (returnType == typeof(DateTimeOffset) && xmlSchemaType.Datatype is Datatype_dateTimeBase)
					{
						obj = text;
					}
					obj2 = xmlSchemaType.ValueConverter.ChangeType(obj, returnType, namespaceResolver);
				}
				else
				{
					obj2 = XmlUntypedConverter.Untyped.ChangeType(obj, returnType, namespaceResolver);
				}
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, this);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, this);
			}
			catch (OverflowException ex3)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex3, this);
			}
			return obj2;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00036DC8 File Offset: 0x00034FC8
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00036DD0 File Offset: 0x00034FD0
		public override string GetAttribute(string name)
		{
			string text = this.coreReader.GetAttribute(name);
			if (text == null && this.attributeCount > 0)
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, false);
				if (defaultAttribute != null)
				{
					text = defaultAttribute.RawValue;
				}
			}
			return text;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00036E0C File Offset: 0x0003500C
		public override string GetAttribute(string name, string namespaceURI)
		{
			string attribute = this.coreReader.GetAttribute(name, namespaceURI);
			if (attribute == null && this.attributeCount > 0)
			{
				namespaceURI = ((namespaceURI == null) ? string.Empty : this.coreReaderNameTable.Get(namespaceURI));
				name = this.coreReaderNameTable.Get(name);
				if (name == null || namespaceURI == null)
				{
					return null;
				}
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, namespaceURI, false);
				if (defaultAttribute != null)
				{
					return defaultAttribute.RawValue;
				}
			}
			return attribute;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00036E78 File Offset: 0x00035078
		public override string GetAttribute(int i)
		{
			if (this.attributeCount == 0)
			{
				return null;
			}
			if (i < this.coreReaderAttributeCount)
			{
				return this.coreReader.GetAttribute(i);
			}
			int num = i - this.coreReaderAttributeCount;
			return ((ValidatingReaderNodeData)this.defaultAttributes[num]).RawValue;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00036EC4 File Offset: 0x000350C4
		public override bool MoveToAttribute(string name)
		{
			if (!this.coreReader.MoveToAttribute(name))
			{
				if (this.attributeCount > 0)
				{
					ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, true);
					if (defaultAttribute != null)
					{
						this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
						this.attributePSVI = defaultAttribute.AttInfo;
						this.cachedNode = defaultAttribute;
						goto IL_0057;
					}
				}
				return false;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			this.attributePSVI = this.GetAttributePSVI(name);
			IL_0057:
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00036F4C File Offset: 0x0003514C
		public override bool MoveToAttribute(string name, string ns)
		{
			name = this.coreReaderNameTable.Get(name);
			ns = ((ns != null) ? this.coreReaderNameTable.Get(ns) : string.Empty);
			if (name == null || ns == null)
			{
				return false;
			}
			if (this.coreReader.MoveToAttribute(name, ns))
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.GetAttributePSVI(name, ns);
				}
				else
				{
					this.attributePSVI = null;
				}
			}
			else
			{
				ValidatingReaderNodeData defaultAttribute = this.GetDefaultAttribute(name, ns, true);
				if (defaultAttribute == null)
				{
					return false;
				}
				this.attributePSVI = defaultAttribute.AttInfo;
				this.cachedNode = defaultAttribute;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0003700C File Offset: 0x0003520C
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.currentAttrIndex = i;
			if (i < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToAttribute(i);
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[i];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				int num = i - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[num];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000370D0 File Offset: 0x000352D0
		public override bool MoveToFirstAttribute()
		{
			if (this.coreReader.MoveToFirstAttribute())
			{
				this.currentAttrIndex = 0;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[0];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.defaultAttributes.Count <= 0)
				{
					return false;
				}
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[0];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.currentAttrIndex = 0;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00037184 File Offset: 0x00035384
		public override bool MoveToNextAttribute()
		{
			if (this.currentAttrIndex + 1 < this.coreReaderAttributeCount)
			{
				this.coreReader.MoveToNextAttribute();
				this.currentAttrIndex++;
				if (this.inlineSchemaParser == null)
				{
					this.attributePSVI = this.attributePSVINodes[this.currentAttrIndex];
				}
				else
				{
					this.attributePSVI = null;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnAttribute;
			}
			else
			{
				if (this.currentAttrIndex + 1 >= this.attributeCount)
				{
					return false;
				}
				int num = this.currentAttrIndex + 1;
				this.currentAttrIndex = num;
				int num2 = num - this.coreReaderAttributeCount;
				this.cachedNode = (ValidatingReaderNodeData)this.defaultAttributes[num2];
				this.attributePSVI = this.cachedNode.AttInfo;
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			return true;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00037265 File Offset: 0x00035465
		public override bool MoveToElement()
		{
			if (this.coreReader.MoveToElement() || this.validationState < XsdValidatingReader.ValidatingReaderState.None)
			{
				this.currentAttrIndex = -1;
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return true;
			}
			return false;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00037290 File Offset: 0x00035490
		public override bool Read()
		{
			switch (this.validationState)
			{
			case XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue:
			case XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute:
			case XsdValidatingReader.ValidatingReaderState.OnAttribute:
			case XsdValidatingReader.ValidatingReaderState.ClearAttributes:
				this.ClearAttributesInfo();
				if (this.inlineSchemaParser != null)
				{
					this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
					goto IL_007C;
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				break;
			case XsdValidatingReader.ValidatingReaderState.None:
				return false;
			case XsdValidatingReader.ValidatingReaderState.Init:
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				if (this.coreReader.ReadState == ReadState.Interactive)
				{
					this.ProcessReaderEvent();
					return true;
				}
				break;
			case XsdValidatingReader.ValidatingReaderState.Read:
				break;
			case XsdValidatingReader.ValidatingReaderState.ParseInlineSchema:
				goto IL_007C;
			case XsdValidatingReader.ValidatingReaderState.ReadAhead:
				this.ClearAttributesInfo();
				this.ProcessReaderEvent();
				this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				return true;
			case XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent:
				this.validationState = this.savedState;
				this.readBinaryHelper.Finish();
				return this.Read();
			case XsdValidatingReader.ValidatingReaderState.ReaderClosed:
			case XsdValidatingReader.ValidatingReaderState.EOF:
				return false;
			default:
				return false;
			}
			if (this.coreReader.Read())
			{
				this.ProcessReaderEvent();
				return true;
			}
			this.validator.EndValidation();
			if (this.coreReader.EOF)
			{
				this.validationState = XsdValidatingReader.ValidatingReaderState.EOF;
			}
			return false;
			IL_007C:
			this.ProcessInlineSchema();
			return true;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00037397 File Offset: 0x00035597
		public override bool EOF
		{
			get
			{
				return this.coreReader.EOF;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000373A4 File Offset: 0x000355A4
		public override void Close()
		{
			this.coreReader.Close();
			this.validationState = XsdValidatingReader.ValidatingReaderState.ReaderClosed;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000373B8 File Offset: 0x000355B8
		public override ReadState ReadState
		{
			get
			{
				if (this.validationState != XsdValidatingReader.ValidatingReaderState.Init)
				{
					return this.coreReader.ReadState;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000373D0 File Offset: 0x000355D0
		public override void Skip()
		{
			int depth = this.Depth;
			XmlNodeType nodeType = this.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Attribute)
				{
					goto IL_0081;
				}
				this.MoveToElement();
			}
			if (!this.coreReader.IsEmptyElement)
			{
				bool flag = true;
				if ((this.xmlSchemaInfo.IsUnionType || this.xmlSchemaInfo.IsDefault) && this.coreReader is XsdCachingReader)
				{
					flag = false;
				}
				this.coreReader.Skip();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ReadAhead;
				if (flag)
				{
					this.validator.SkipToEndElement(this.xmlSchemaInfo);
				}
			}
			IL_0081:
			this.Read();
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00037465 File Offset: 0x00035665
		public override XmlNameTable NameTable
		{
			get
			{
				return this.coreReaderNameTable;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0003746D File Offset: 0x0003566D
		public override string LookupNamespace(string prefix)
		{
			return this.thisNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000356D5 File Offset: 0x000338D5
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0003747C File Offset: 0x0003567C
		public override bool ReadAttributeValue()
		{
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper.Finish();
				this.validationState = this.savedState;
			}
			if (this.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
			{
				this.cachedNode = this.CreateDummyTextNode(this.cachedNode.RawValue, this.cachedNode.Depth + 1);
				this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadAttributeValue;
				return true;
			}
			return this.coreReader.ReadAttributeValue();
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000374F8 File Offset: 0x000356F8
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00037564 File Offset: 0x00035764
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000375D0 File Offset: 0x000357D0
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadElementContentAsBase64(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003763C File Offset: 0x0003583C
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			if (this.validationState != XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent)
			{
				this.readBinaryHelper = ReadContentAsBinaryHelper.CreateOrReset(this.readBinaryHelper, this);
				this.savedState = this.validationState;
			}
			this.validationState = this.savedState;
			int num = this.readBinaryHelper.ReadElementContentAsBinHex(buffer, index, count);
			this.savedState = this.validationState;
			this.validationState = XsdValidatingReader.ValidatingReaderState.OnReadBinaryContent;
			return num;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x000376A8 File Offset: 0x000358A8
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.IsDefault;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.IsDefault;
					}
					return false;
				}
				if (!this.coreReader.IsEmptyElement)
				{
					this.GetIsDefault();
				}
				return this.xmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0003770C File Offset: 0x0003590C
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				return (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement) && this.xmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00037738 File Offset: 0x00035938
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							return this.xmlSchemaInfo.Validity;
						}
					}
					else if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.Validity;
					}
					return XmlSchemaValidity.NotKnown;
				}
				if (this.coreReader.IsEmptyElement)
				{
					return this.xmlSchemaInfo.Validity;
				}
				if (this.xmlSchemaInfo.Validity == XmlSchemaValidity.Valid)
				{
					return XmlSchemaValidity.NotKnown;
				}
				return this.xmlSchemaInfo.Validity;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000377B4 File Offset: 0x000359B4
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					if (!this.coreReader.IsEmptyElement)
					{
						this.GetMemberType();
					}
					return this.xmlSchemaInfo.MemberType;
				}
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType != XmlNodeType.EndElement)
					{
						return null;
					}
					return this.xmlSchemaInfo.MemberType;
				}
				else
				{
					if (this.attributePSVI != null)
					{
						return this.AttributeSchemaInfo.MemberType;
					}
					return null;
				}
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0003781C File Offset: 0x00035A1C
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							return null;
						}
					}
					else
					{
						if (this.attributePSVI != null)
						{
							return this.AttributeSchemaInfo.SchemaType;
						}
						return null;
					}
				}
				return this.xmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00037860 File Offset: 0x00035A60
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
				{
					return this.xmlSchemaInfo.SchemaElement;
				}
				return null;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00037882 File Offset: 0x00035A82
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				if (this.NodeType == XmlNodeType.Attribute && this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.SchemaAttribute;
				}
				return null;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000378A2 File Offset: 0x00035AA2
		public int LineNumber
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000378B9 File Offset: 0x00035AB9
		public int LinePosition
		{
			get
			{
				if (this.lineInfo != null)
				{
					return this.lineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000378D0 File Offset: 0x00035AD0
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.GetNamespacesInScope(scope);
			}
			return this.nsManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000378F3 File Offset: 0x00035AF3
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupNamespace(prefix);
			}
			return this.nsManager.LookupNamespace(prefix);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00037916 File Offset: 0x00035B16
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			if (this.coreReaderNSResolver != null)
			{
				return this.coreReaderNSResolver.LookupPrefix(namespaceName);
			}
			return this.nsManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00037939 File Offset: 0x00035B39
		private object GetStringValue()
		{
			return this.coreReader.Value;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00037946 File Offset: 0x00035B46
		private XmlSchemaType ElementXmlType
		{
			get
			{
				return this.xmlSchemaInfo.XmlType;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00037953 File Offset: 0x00035B53
		private XmlSchemaType AttributeXmlType
		{
			get
			{
				if (this.attributePSVI != null)
				{
					return this.AttributeSchemaInfo.XmlType;
				}
				return null;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0003796A File Offset: 0x00035B6A
		private XmlSchemaInfo AttributeSchemaInfo
		{
			get
			{
				return this.attributePSVI.attributeSchemaInfo;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00037978 File Offset: 0x00035B78
		private void ProcessReaderEvent()
		{
			if (this.replayCache)
			{
				return;
			}
			switch (this.coreReader.NodeType)
			{
			case XmlNodeType.Element:
				this.ProcessElementEvent();
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EntityReference:
				throw new InvalidOperationException();
			case XmlNodeType.DocumentType:
				this.validator.SetDtdSchemaInfo(this.coreReader.DtdInfo);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
				return;
			case XmlNodeType.EndElement:
				this.ProcessEndElementEvent();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00037A3C File Offset: 0x00035C3C
		private void ProcessElementEvent()
		{
			if (!this.processInlineSchema || !this.IsXSDRoot(this.coreReader.LocalName, this.coreReader.NamespaceURI) || this.coreReader.Depth <= 0)
			{
				this.atomicValue = null;
				this.originalAtomicValueString = null;
				this.xmlSchemaInfo.Clear();
				if (this.manageNamespaces)
				{
					this.nsManager.PushScope();
				}
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				if (this.coreReader.MoveToFirstAttribute())
				{
					do
					{
						string namespaceURI = this.coreReader.NamespaceURI;
						string localName = this.coreReader.LocalName;
						if (Ref.Equal(namespaceURI, this.NsXsi))
						{
							if (Ref.Equal(localName, this.XsiSchemaLocation))
							{
								text = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNoNamespaceSchemaLocation))
							{
								text2 = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiType))
							{
								text4 = this.coreReader.Value;
							}
							else if (Ref.Equal(localName, this.XsiNil))
							{
								text3 = this.coreReader.Value;
							}
						}
						if (this.manageNamespaces && Ref.Equal(this.coreReader.NamespaceURI, this.NsXmlNs))
						{
							this.nsManager.AddNamespace((this.coreReader.Prefix.Length == 0) ? string.Empty : this.coreReader.LocalName, this.coreReader.Value);
						}
					}
					while (this.coreReader.MoveToNextAttribute());
					this.coreReader.MoveToElement();
				}
				this.validator.ValidateElement(this.coreReader.LocalName, this.coreReader.NamespaceURI, this.xmlSchemaInfo, text4, text3, text, text2);
				this.ValidateAttributes();
				this.validator.ValidateEndOfAttributes(this.xmlSchemaInfo);
				if (this.coreReader.IsEmptyElement)
				{
					this.ProcessEndElementEvent();
				}
				this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
				return;
			}
			this.xmlSchemaInfo.Clear();
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			if (!this.coreReader.IsEmptyElement)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XSD, this.coreReaderNameTable, this.validator.SchemaSet.GetSchemaNames(this.coreReaderNameTable), this.validationEvent);
				this.inlineSchemaParser.StartParsing(this.coreReader, null);
				this.inlineSchemaParser.ParseReaderNode();
				this.validationState = XsdValidatingReader.ValidatingReaderState.ParseInlineSchema;
				return;
			}
			this.validationState = XsdValidatingReader.ValidatingReaderState.ClearAttributes;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00037CC4 File Offset: 0x00035EC4
		private void ProcessEndElementEvent()
		{
			this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
			this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
			if (this.xmlSchemaInfo.IsDefault)
			{
				int depth = this.coreReader.Depth;
				this.coreReader = this.GetCachingReader();
				this.cachingReader.RecordTextNode(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString, depth + 1, 0, 0);
				this.cachingReader.RecordEndElementNode();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
				return;
			}
			if (this.manageNamespaces)
			{
				this.nsManager.PopScope();
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00037D80 File Offset: 0x00035F80
		private void ValidateAttributes()
		{
			this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
			int num = 0;
			bool flag = false;
			if (this.coreReader.MoveToFirstAttribute())
			{
				do
				{
					string localName = this.coreReader.LocalName;
					string namespaceURI = this.coreReader.NamespaceURI;
					AttributePSVIInfo attributePSVIInfo = this.AddAttributePSVI(num);
					attributePSVIInfo.localName = localName;
					attributePSVIInfo.namespaceUri = namespaceURI;
					if (namespaceURI == this.NsXmlNs)
					{
						num++;
					}
					else
					{
						attributePSVIInfo.typedAttributeValue = this.validator.ValidateAttribute(localName, namespaceURI, this.valueGetter, attributePSVIInfo.attributeSchemaInfo);
						if (!flag)
						{
							flag = attributePSVIInfo.attributeSchemaInfo.Validity == XmlSchemaValidity.Invalid;
						}
						num++;
					}
				}
				while (this.coreReader.MoveToNextAttribute());
			}
			this.coreReader.MoveToElement();
			if (flag)
			{
				this.xmlSchemaInfo.Validity = XmlSchemaValidity.Invalid;
			}
			this.validator.GetUnspecifiedDefaultAttributes(this.defaultAttributes, true);
			this.attributeCount += this.defaultAttributes.Count;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00037E89 File Offset: 0x00036089
		private void ClearAttributesInfo()
		{
			this.attributeCount = 0;
			this.coreReaderAttributeCount = 0;
			this.currentAttrIndex = -1;
			this.defaultAttributes.Clear();
			this.attributePSVI = null;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00037EB4 File Offset: 0x000360B4
		private AttributePSVIInfo GetAttributePSVI(string name)
		{
			if (this.inlineSchemaParser != null)
			{
				return null;
			}
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string text3;
			if (text.Length == 0)
			{
				text3 = string.Empty;
			}
			else
			{
				text3 = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetAttributePSVI(text2, text3);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00037F14 File Offset: 0x00036114
		private AttributePSVIInfo GetAttributePSVI(string localName, string ns)
		{
			for (int i = 0; i < this.coreReaderAttributeCount; i++)
			{
				AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[i];
				if (attributePSVIInfo != null && Ref.Equal(localName, attributePSVIInfo.localName) && Ref.Equal(ns, attributePSVIInfo.namespaceUri))
				{
					this.currentAttrIndex = i;
					return attributePSVIInfo;
				}
			}
			return null;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00037F68 File Offset: 0x00036168
		private ValidatingReaderNodeData GetDefaultAttribute(string name, bool updatePosition)
		{
			string text;
			string text2;
			ValidateNames.SplitQName(name, out text, out text2);
			text = this.coreReaderNameTable.Add(text);
			text2 = this.coreReaderNameTable.Add(text2);
			string text3;
			if (text.Length == 0)
			{
				text3 = string.Empty;
			}
			else
			{
				text3 = this.thisNSResolver.LookupNamespace(text);
			}
			return this.GetDefaultAttribute(text2, text3, updatePosition);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00037FC0 File Offset: 0x000361C0
		private ValidatingReaderNodeData GetDefaultAttribute(string attrLocalName, string ns, bool updatePosition)
		{
			for (int i = 0; i < this.defaultAttributes.Count; i++)
			{
				ValidatingReaderNodeData validatingReaderNodeData = (ValidatingReaderNodeData)this.defaultAttributes[i];
				if (Ref.Equal(validatingReaderNodeData.LocalName, attrLocalName) && Ref.Equal(validatingReaderNodeData.Namespace, ns))
				{
					if (updatePosition)
					{
						this.currentAttrIndex = this.coreReader.AttributeCount + i;
					}
					return validatingReaderNodeData;
				}
			}
			return null;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0003802C File Offset: 0x0003622C
		private AttributePSVIInfo AddAttributePSVI(int attIndex)
		{
			AttributePSVIInfo attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo != null)
			{
				attributePSVIInfo.Reset();
				return attributePSVIInfo;
			}
			if (attIndex >= this.attributePSVINodes.Length - 1)
			{
				AttributePSVIInfo[] array = new AttributePSVIInfo[this.attributePSVINodes.Length * 2];
				Array.Copy(this.attributePSVINodes, 0, array, 0, this.attributePSVINodes.Length);
				this.attributePSVINodes = array;
			}
			attributePSVIInfo = this.attributePSVINodes[attIndex];
			if (attributePSVIInfo == null)
			{
				attributePSVIInfo = new AttributePSVIInfo();
				this.attributePSVINodes[attIndex] = attributePSVIInfo;
			}
			return attributePSVIInfo;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000380A3 File Offset: 0x000362A3
		private bool IsXSDRoot(string localName, string ns)
		{
			return Ref.Equal(ns, this.NsXs) && Ref.Equal(localName, this.XsdSchema);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000380C4 File Offset: 0x000362C4
		private void ProcessInlineSchema()
		{
			if (this.coreReader.Read())
			{
				if (this.coreReader.NodeType == XmlNodeType.Element)
				{
					this.attributeCount = (this.coreReaderAttributeCount = this.coreReader.AttributeCount);
				}
				else
				{
					this.ClearAttributesInfo();
				}
				if (!this.inlineSchemaParser.ParseReaderNode())
				{
					this.inlineSchemaParser.FinishParsing();
					XmlSchema xmlSchema = this.inlineSchemaParser.XmlSchema;
					this.validator.AddSchema(xmlSchema);
					this.inlineSchemaParser = null;
					this.validationState = XsdValidatingReader.ValidatingReaderState.Read;
				}
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0003814D File Offset: 0x0003634D
		private object InternalReadContentAsObject()
		{
			return this.InternalReadContentAsObject(false);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00038158 File Offset: 0x00036358
		private object InternalReadContentAsObject(bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadContentAsObject(unwrapTypedValue, out text);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00038170 File Offset: 0x00036370
		private object InternalReadContentAsObject(bool unwrapTypedValue, out string originalStringValue)
		{
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				originalStringValue = this.Value;
				if (this.attributePSVI != null && this.attributePSVI.typedAttributeValue != null)
				{
					if (this.validationState == XsdValidatingReader.ValidatingReaderState.OnDefaultAttribute)
					{
						XmlSchemaAttribute schemaAttribute = this.attributePSVI.attributeSchemaInfo.SchemaAttribute;
						originalStringValue = ((schemaAttribute.DefaultValue != null) ? schemaAttribute.DefaultValue : schemaAttribute.FixedValue);
					}
					return this.ReturnBoxedValue(this.attributePSVI.typedAttributeValue, this.AttributeSchemaInfo.XmlType, unwrapTypedValue);
				}
				return this.Value;
			}
			else if (nodeType == XmlNodeType.EndElement)
			{
				if (this.atomicValue != null)
				{
					originalStringValue = this.originalAtomicValueString;
					return this.atomicValue;
				}
				originalStringValue = string.Empty;
				return string.Empty;
			}
			else
			{
				if (this.validator.CurrentContentType == XmlSchemaContentType.TextOnly)
				{
					object obj = this.ReturnBoxedValue(this.ReadTillEndElement(), this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					originalStringValue = this.originalAtomicValueString;
					return obj;
				}
				XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
				if (xsdCachingReader != null)
				{
					originalStringValue = xsdCachingReader.ReadOriginalContentAsString();
				}
				else
				{
					originalStringValue = base.InternalReadContentAsString();
				}
				return originalStringValue;
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00038276 File Offset: 0x00036476
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType)
		{
			return this.InternalReadElementContentAsObject(out xmlType, false);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00038280 File Offset: 0x00036480
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue)
		{
			string text;
			return this.InternalReadElementContentAsObject(out xmlType, unwrapTypedValue, out text);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00038298 File Offset: 0x00036498
		private object InternalReadElementContentAsObject(out XmlSchemaType xmlType, bool unwrapTypedValue, out string originalString)
		{
			xmlType = null;
			object obj;
			if (this.IsEmptyElement)
			{
				if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
				{
					obj = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
				}
				else
				{
					obj = this.atomicValue;
				}
				originalString = this.originalAtomicValueString;
				xmlType = this.ElementXmlType;
				this.Read();
				return obj;
			}
			this.Read();
			if (this.NodeType == XmlNodeType.EndElement)
			{
				if (this.xmlSchemaInfo.IsDefault)
				{
					if (this.xmlSchemaInfo.ContentType == XmlSchemaContentType.TextOnly)
					{
						obj = this.ReturnBoxedValue(this.atomicValue, this.xmlSchemaInfo.XmlType, unwrapTypedValue);
					}
					else
					{
						obj = this.atomicValue;
					}
					originalString = this.originalAtomicValueString;
				}
				else
				{
					obj = string.Empty;
					originalString = string.Empty;
				}
			}
			else
			{
				if (this.NodeType == XmlNodeType.Element)
				{
					throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
				}
				obj = this.InternalReadContentAsObject(unwrapTypedValue, out originalString);
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this);
				}
			}
			xmlType = this.ElementXmlType;
			this.Read();
			return obj;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000383B0 File Offset: 0x000365B0
		private object ReadTillEndElement()
		{
			if (this.atomicValue == null)
			{
				while (this.coreReader.Read())
				{
					if (!this.replayCache)
					{
						switch (this.coreReader.NodeType)
						{
						case XmlNodeType.Element:
							this.ProcessReaderEvent();
							goto IL_010B;
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
							break;
						case XmlNodeType.EndElement:
							this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
							this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
							if (this.manageNamespaces)
							{
								this.nsManager.PopScope();
								goto IL_010B;
							}
							goto IL_010B;
						}
					}
				}
			}
			else
			{
				if (this.atomicValue == this)
				{
					this.atomicValue = null;
				}
				this.SwitchReader();
			}
			IL_010B:
			return this.atomicValue;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000384D0 File Offset: 0x000366D0
		private void SwitchReader()
		{
			XsdCachingReader xsdCachingReader = this.coreReader as XsdCachingReader;
			if (xsdCachingReader != null)
			{
				this.coreReader = xsdCachingReader.GetCoreReader();
			}
			this.replayCache = false;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00038500 File Offset: 0x00036700
		private void ReadAheadForMemberType()
		{
			while (this.coreReader.Read())
			{
				switch (this.coreReader.NodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
					break;
				case XmlNodeType.EndElement:
					this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
					this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
					if (this.atomicValue == null)
					{
						this.atomicValue = this;
						return;
					}
					if (this.xmlSchemaInfo.IsDefault)
					{
						this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						return;
					}
					return;
				}
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0003861C File Offset: 0x0003681C
		private void GetIsDefault()
		{
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.HasDefaultValue)
			{
				this.coreReader = this.GetCachingReader();
				if (this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
				{
					this.ReadAheadForMemberType();
				}
				else if (this.coreReader.Read())
				{
					switch (this.coreReader.NodeType)
					{
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						this.validator.ValidateText(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						this.validator.ValidateWhitespace(new XmlValueGetter(this.GetStringValue));
						break;
					case XmlNodeType.EndElement:
						this.atomicValue = this.validator.ValidateEndElement(this.xmlSchemaInfo);
						this.originalAtomicValueString = this.GetOriginalAtomicValueStringOfElement();
						if (this.xmlSchemaInfo.IsDefault)
						{
							this.cachingReader.SwitchTextNodeAndEndElement(this.xmlSchemaInfo.XmlType.ValueConverter.ToString(this.atomicValue), this.originalAtomicValueString);
						}
						break;
					}
				}
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00038780 File Offset: 0x00036980
		private void GetMemberType()
		{
			if (this.xmlSchemaInfo.MemberType != null || this.atomicValue == this)
			{
				return;
			}
			if (!(this.coreReader is XsdCachingReader) && this.xmlSchemaInfo.IsUnionType && !this.xmlSchemaInfo.IsNil)
			{
				this.coreReader = this.GetCachingReader();
				this.ReadAheadForMemberType();
				this.cachingReader.SetToReplayMode();
				this.replayCache = true;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000387F0 File Offset: 0x000369F0
		private object ReturnBoxedValue(object typedValue, XmlSchemaType xmlType, bool unWrap)
		{
			if (typedValue != null)
			{
				if (unWrap && xmlType.Datatype.Variety == XmlSchemaDatatypeVariety.List && (xmlType.Datatype as Datatype_List).ItemType.Variety == XmlSchemaDatatypeVariety.Union)
				{
					typedValue = xmlType.ValueConverter.ChangeType(typedValue, xmlType.Datatype.ValueType, this.thisNSResolver);
				}
				return typedValue;
			}
			typedValue = this.validator.GetConcatenatedValue();
			return typedValue;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00038858 File Offset: 0x00036A58
		private XsdCachingReader GetCachingReader()
		{
			if (this.cachingReader == null)
			{
				this.cachingReader = new XsdCachingReader(this.coreReader, this.lineInfo, new CachingEventHandler(this.CachingCallBack));
			}
			else
			{
				this.cachingReader.Reset(this.coreReader);
			}
			this.lineInfo = this.cachingReader;
			return this.cachingReader;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000388B5 File Offset: 0x00036AB5
		internal ValidatingReaderNodeData CreateDummyTextNode(string attributeValue, int depth)
		{
			if (this.textNode == null)
			{
				this.textNode = new ValidatingReaderNodeData(XmlNodeType.Text);
			}
			this.textNode.Depth = depth;
			this.textNode.RawValue = attributeValue;
			return this.textNode;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000388E9 File Offset: 0x00036AE9
		internal void CachingCallBack(XsdCachingReader cachingReader)
		{
			this.coreReader = cachingReader.GetCoreReader();
			this.lineInfo = cachingReader.GetLineInfo();
			this.replayCache = false;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0003890C File Offset: 0x00036B0C
		private string GetOriginalAtomicValueStringOfElement()
		{
			if (!this.xmlSchemaInfo.IsDefault)
			{
				return this.validator.GetConcatenatedValue();
			}
			XmlSchemaElement schemaElement = this.xmlSchemaInfo.SchemaElement;
			if (schemaElement == null)
			{
				return string.Empty;
			}
			if (schemaElement.DefaultValue == null)
			{
				return schemaElement.FixedValue;
			}
			return schemaElement.DefaultValue;
		}

		// Token: 0x040005A7 RID: 1447
		private XmlReader coreReader;

		// Token: 0x040005A8 RID: 1448
		private IXmlNamespaceResolver coreReaderNSResolver;

		// Token: 0x040005A9 RID: 1449
		private IXmlNamespaceResolver thisNSResolver;

		// Token: 0x040005AA RID: 1450
		private XmlSchemaValidator validator;

		// Token: 0x040005AB RID: 1451
		private XmlResolver xmlResolver;

		// Token: 0x040005AC RID: 1452
		private ValidationEventHandler validationEvent;

		// Token: 0x040005AD RID: 1453
		private XsdValidatingReader.ValidatingReaderState validationState;

		// Token: 0x040005AE RID: 1454
		private XmlValueGetter valueGetter;

		// Token: 0x040005AF RID: 1455
		private XmlNamespaceManager nsManager;

		// Token: 0x040005B0 RID: 1456
		private bool manageNamespaces;

		// Token: 0x040005B1 RID: 1457
		private bool processInlineSchema;

		// Token: 0x040005B2 RID: 1458
		private bool replayCache;

		// Token: 0x040005B3 RID: 1459
		private ValidatingReaderNodeData cachedNode;

		// Token: 0x040005B4 RID: 1460
		private AttributePSVIInfo attributePSVI;

		// Token: 0x040005B5 RID: 1461
		private int attributeCount;

		// Token: 0x040005B6 RID: 1462
		private int coreReaderAttributeCount;

		// Token: 0x040005B7 RID: 1463
		private int currentAttrIndex;

		// Token: 0x040005B8 RID: 1464
		private AttributePSVIInfo[] attributePSVINodes;

		// Token: 0x040005B9 RID: 1465
		private ArrayList defaultAttributes;

		// Token: 0x040005BA RID: 1466
		private Parser inlineSchemaParser;

		// Token: 0x040005BB RID: 1467
		private object atomicValue;

		// Token: 0x040005BC RID: 1468
		private XmlSchemaInfo xmlSchemaInfo;

		// Token: 0x040005BD RID: 1469
		private string originalAtomicValueString;

		// Token: 0x040005BE RID: 1470
		private XmlNameTable coreReaderNameTable;

		// Token: 0x040005BF RID: 1471
		private XsdCachingReader cachingReader;

		// Token: 0x040005C0 RID: 1472
		private ValidatingReaderNodeData textNode;

		// Token: 0x040005C1 RID: 1473
		private string NsXmlNs;

		// Token: 0x040005C2 RID: 1474
		private string NsXs;

		// Token: 0x040005C3 RID: 1475
		private string NsXsi;

		// Token: 0x040005C4 RID: 1476
		private string XsiType;

		// Token: 0x040005C5 RID: 1477
		private string XsiNil;

		// Token: 0x040005C6 RID: 1478
		private string XsdSchema;

		// Token: 0x040005C7 RID: 1479
		private string XsiSchemaLocation;

		// Token: 0x040005C8 RID: 1480
		private string XsiNoNamespaceSchemaLocation;

		// Token: 0x040005C9 RID: 1481
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040005CA RID: 1482
		private IXmlLineInfo lineInfo;

		// Token: 0x040005CB RID: 1483
		private ReadContentAsBinaryHelper readBinaryHelper;

		// Token: 0x040005CC RID: 1484
		private XsdValidatingReader.ValidatingReaderState savedState;

		// Token: 0x040005CD RID: 1485
		private static volatile Type TypeOfString;

		// Token: 0x020000CB RID: 203
		private enum ValidatingReaderState
		{
			// Token: 0x040005CF RID: 1487
			None,
			// Token: 0x040005D0 RID: 1488
			Init,
			// Token: 0x040005D1 RID: 1489
			Read,
			// Token: 0x040005D2 RID: 1490
			OnDefaultAttribute = -1,
			// Token: 0x040005D3 RID: 1491
			OnReadAttributeValue = -2,
			// Token: 0x040005D4 RID: 1492
			OnAttribute = 3,
			// Token: 0x040005D5 RID: 1493
			ClearAttributes,
			// Token: 0x040005D6 RID: 1494
			ParseInlineSchema,
			// Token: 0x040005D7 RID: 1495
			ReadAhead,
			// Token: 0x040005D8 RID: 1496
			OnReadBinaryContent,
			// Token: 0x040005D9 RID: 1497
			ReaderClosed,
			// Token: 0x040005DA RID: 1498
			EOF,
			// Token: 0x040005DB RID: 1499
			Error
		}
	}
}
