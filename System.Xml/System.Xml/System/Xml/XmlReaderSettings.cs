using System;
using System.IO;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlReader" /> object created by the <see cref="Overload:System.Xml.XmlReader.Create" /> method. </summary>
	// Token: 0x02000069 RID: 105
	public sealed class XmlReaderSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlReaderSettings" /> class.</summary>
		// Token: 0x060004CA RID: 1226 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public XmlReaderSettings()
		{
			this.Initialize();
		}

		/// <summary>Gets or sets whether asynchronous <see cref="T:System.Xml.XmlReader" /> methods can be used on a particular <see cref="T:System.Xml.XmlReader" /> instance.</summary>
		/// <returns>true if asynchronous methods can be used; otherwise, false.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00016BCE File Offset: 0x00014DCE
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00016BD6 File Offset: 0x00014DD6
		public bool Async
		{
			get
			{
				return this.useAsync;
			}
			set
			{
				this.CheckReadOnly("Async");
				this.useAsync = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlNameTable" /> used for atomized string comparisons.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNameTable" /> that stores all the atomized strings used by all <see cref="T:System.Xml.XmlReader" /> instances created using this <see cref="T:System.Xml.XmlReaderSettings" /> object.The default is null. The created <see cref="T:System.Xml.XmlReader" /> instance will use a new empty <see cref="T:System.Xml.NameTable" /> if this value is null.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00016BEA File Offset: 0x00014DEA
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00016BF2 File Offset: 0x00014DF2
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
			set
			{
				this.CheckReadOnly("NameTable");
				this.nameTable = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016C06 File Offset: 0x00014E06
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00016C0E File Offset: 0x00014E0E
		internal bool IsXmlResolverSet { get; set; }

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to access external documents.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> used to access external documents. If set to null, an <see cref="T:System.Xml.XmlException" /> is thrown when the <see cref="T:System.Xml.XmlReader" /> tries to access an external resource. The default is a new <see cref="T:System.Xml.XmlUrlResolver" /> with no credentials.</returns>
		// Token: 0x170000C4 RID: 196
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00016C17 File Offset: 0x00014E17
		public XmlResolver XmlResolver
		{
			set
			{
				this.CheckReadOnly("XmlResolver");
				this.xmlResolver = value;
				this.IsXmlResolverSet = true;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00016C32 File Offset: 0x00014E32
		internal XmlResolver GetXmlResolver()
		{
			return this.xmlResolver;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00016C3A File Offset: 0x00014E3A
		internal XmlResolver GetXmlResolver_CheckConfig()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver && !this.IsXmlResolverSet)
			{
				return null;
			}
			return this.xmlResolver;
		}

		/// <summary>Gets or sets line number offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line number offset. The default is 0.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00016C53 File Offset: 0x00014E53
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00016C5B File Offset: 0x00014E5B
		public int LineNumberOffset
		{
			get
			{
				return this.lineNumberOffset;
			}
			set
			{
				this.CheckReadOnly("LineNumberOffset");
				this.lineNumberOffset = value;
			}
		}

		/// <summary>Gets or sets line position offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line position offset. The default is 0.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00016C6F File Offset: 0x00014E6F
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00016C77 File Offset: 0x00014E77
		public int LinePositionOffset
		{
			get
			{
				return this.linePositionOffset;
			}
			set
			{
				this.CheckReadOnly("LinePositionOffset");
				this.linePositionOffset = value;
			}
		}

		/// <summary>Gets or sets the level of conformance which the <see cref="T:System.Xml.XmlReader" /> will comply.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ConformanceLevel" /> values that specifies the level of conformance which the <see cref="T:System.Xml.XmlReader" /> will comply. The default is ConformanceLevel.Document.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00016C8B File Offset: 0x00014E8B
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00016C93 File Offset: 0x00014E93
		public ConformanceLevel ConformanceLevel
		{
			get
			{
				return this.conformanceLevel;
			}
			set
			{
				this.CheckReadOnly("ConformanceLevel");
				if (value > ConformanceLevel.Document)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.conformanceLevel = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do character checking.</summary>
		/// <returns>true to do character checking; otherwise false. The default is true.NoteIf the <see cref="T:System.Xml.XmlReader" /> is processing text data, it always checks that the XML names and text content are valid, regardless of the property setting. Setting <see cref="P:System.Xml.XmlReaderSettings.CheckCharacters" /> to false turns off character checking for character entity references.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016CB6 File Offset: 0x00014EB6
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00016CBE File Offset: 0x00014EBE
		public bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.CheckReadOnly("CheckCharacters");
				this.checkCharacters = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum allowable number of characters XML document. A zero (0) value means no limits on the size of the XML document. A non-zero value specifies the maximum size, in characters.</summary>
		/// <returns>The maximum allowable number of characters in an XML document. The default is 0.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00016CD2 File Offset: 0x00014ED2
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00016CDA File Offset: 0x00014EDA
		public long MaxCharactersInDocument
		{
			get
			{
				return this.maxCharactersInDocument;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersInDocument");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersInDocument = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum allowable number of characters in a document that result from expanding entities.</summary>
		/// <returns>The maximum allowable number of characters from expanded entities. The default is 0.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00016CFE File Offset: 0x00014EFE
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00016D06 File Offset: 0x00014F06
		public long MaxCharactersFromEntities
		{
			get
			{
				return this.maxCharactersFromEntities;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersFromEntities");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersFromEntities = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore insignificant white space.</summary>
		/// <returns>true to ignore white space; otherwise false. The default is false.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00016D2A File Offset: 0x00014F2A
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00016D32 File Offset: 0x00014F32
		public bool IgnoreWhitespace
		{
			get
			{
				return this.ignoreWhitespace;
			}
			set
			{
				this.CheckReadOnly("IgnoreWhitespace");
				this.ignoreWhitespace = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore processing instructions.</summary>
		/// <returns>true to ignore processing instructions; otherwise false. The default is false.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00016D46 File Offset: 0x00014F46
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00016D4E File Offset: 0x00014F4E
		public bool IgnoreProcessingInstructions
		{
			get
			{
				return this.ignorePIs;
			}
			set
			{
				this.CheckReadOnly("IgnoreProcessingInstructions");
				this.ignorePIs = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore comments.</summary>
		/// <returns>true to ignore comments; otherwise false. The default is false.</returns>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00016D62 File Offset: 0x00014F62
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00016D6A File Offset: 0x00014F6A
		public bool IgnoreComments
		{
			get
			{
				return this.ignoreComments;
			}
			set
			{
				this.CheckReadOnly("IgnoreComments");
				this.ignoreComments = value;
			}
		}

		/// <summary>Gets or sets a value that determines the processing of DTDs.</summary>
		/// <returns>One of the values of the <see cref="T:System.Xml.DtdProcessing" /> enumeration that determines the processing of DTDs.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00016D7E File Offset: 0x00014F7E
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00016D86 File Offset: 0x00014F86
		public DtdProcessing DtdProcessing
		{
			get
			{
				return this.dtdProcessing;
			}
			set
			{
				this.CheckReadOnly("DtdProcessing");
				if (value > DtdProcessing.Parse)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.dtdProcessing = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the underlying stream or <see cref="T:System.IO.TextReader" /> should be closed when the reader is closed.</summary>
		/// <returns>true to close the underlying stream or <see cref="T:System.IO.TextReader" /> when the reader is closed; otherwise false. The default is false.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00016DA9 File Offset: 0x00014FA9
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00016DB1 File Offset: 0x00014FB1
		public bool CloseInput
		{
			get
			{
				return this.closeInput;
			}
			set
			{
				this.CheckReadOnly("CloseInput");
				this.closeInput = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> will perform validation or type assignment when reading.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ValidationType" /> values that indicates whether XmlReader will perform validation or type assignment when reading. The default is ValidationType.None.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00016DC5 File Offset: 0x00014FC5
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00016DCD File Offset: 0x00014FCD
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				this.CheckReadOnly("ValidationType");
				if (value > ValidationType.Schema)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationType = value;
			}
		}

		/// <summary>Gets or sets a value indicating the schema validation settings. This setting applies to schema validating <see cref="T:System.Xml.XmlReader" /> objects (<see cref="P:System.Xml.XmlReaderSettings.ValidationType" /> property set to ValidationType.Schema).</summary>
		/// <returns>A set of <see cref="T:System.Xml.Schema.XmlSchemaValidationFlags" /> values. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessIdentityConstraints" /> and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.AllowXmlAttributes" /> are enabled by default. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessInlineSchema" />, <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation" />, and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings" /> are disabled by default.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00016DF0 File Offset: 0x00014FF0
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00016DF8 File Offset: 0x00014FF8
		public XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
			set
			{
				this.CheckReadOnly("ValidationFlags");
				if (value > (XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationFlags = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use when performing schema validation.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use when performing schema validation. The default is an empty <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00016E1C File Offset: 0x0001501C
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00016E37 File Offset: 0x00015037
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet();
				}
				return this.schemas;
			}
			set
			{
				this.CheckReadOnly("Schemas");
				this.schemas = value;
			}
		}

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlReaderSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlReaderSettings" /> object.</returns>
		// Token: 0x060004F0 RID: 1264 RVA: 0x00016E4B File Offset: 0x0001504B
		public XmlReaderSettings Clone()
		{
			XmlReaderSettings xmlReaderSettings = base.MemberwiseClone() as XmlReaderSettings;
			xmlReaderSettings.ReadOnly = false;
			return xmlReaderSettings;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016E5F File Offset: 0x0001505F
		internal ValidationEventHandler GetEventHandler()
		{
			return this.valEventHandler;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016E68 File Offset: 0x00015068
		internal XmlReader CreateReader(Stream input, Uri baseUri, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				if (baseUri == null)
				{
					baseUriString = string.Empty;
				}
				else
				{
					baseUriString = baseUri.ToString();
				}
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, null, 0, this, baseUri, baseUriString, inputContext, this.closeInput);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016ED4 File Offset: 0x000150D4
		internal XmlReader CreateReader(TextReader input, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				baseUriString = string.Empty;
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, this, baseUriString, inputContext);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x170000D3 RID: 211
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00016F23 File Offset: 0x00015123
		internal bool ReadOnly
		{
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016F2C File Offset: 0x0001512C
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("The '{0}' property is read only and cannot be set.", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00016F57 File Offset: 0x00015157
		private void Initialize()
		{
			this.Initialize(null);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00016F60 File Offset: 0x00015160
		private void Initialize(XmlResolver resolver)
		{
			this.nameTable = null;
			if (!XmlReaderSettings.EnableLegacyXmlSettings())
			{
				this.xmlResolver = resolver;
				this.maxCharactersFromEntities = 10000000L;
			}
			else
			{
				this.xmlResolver = ((resolver == null) ? XmlReaderSettings.CreateDefaultResolver() : resolver);
				this.maxCharactersFromEntities = 0L;
			}
			this.lineNumberOffset = 0;
			this.linePositionOffset = 0;
			this.checkCharacters = true;
			this.conformanceLevel = ConformanceLevel.Document;
			this.ignoreWhitespace = false;
			this.ignorePIs = false;
			this.ignoreComments = false;
			this.dtdProcessing = DtdProcessing.Prohibit;
			this.closeInput = false;
			this.maxCharactersInDocument = 0L;
			this.schemas = null;
			this.validationType = ValidationType.None;
			this.validationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints;
			this.validationFlags |= XmlSchemaValidationFlags.AllowXmlAttributes;
			this.useAsync = false;
			this.isReadOnly = false;
			this.IsXmlResolverSet = false;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017029 File Offset: 0x00015229
		private static XmlResolver CreateDefaultResolver()
		{
			return new XmlUrlResolver();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017030 File Offset: 0x00015230
		internal XmlReader AddValidation(XmlReader reader)
		{
			if (this.validationType == ValidationType.Schema)
			{
				XmlResolver xmlResolver = this.GetXmlResolver_CheckConfig();
				if (xmlResolver == null && !this.IsXmlResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
				{
					xmlResolver = new XmlUrlResolver();
				}
				reader = new XsdValidatingReader(reader, xmlResolver, this);
			}
			else if (this.validationType == ValidationType.DTD)
			{
				reader = this.CreateDtdValidatingReader(reader);
			}
			return reader;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00017084 File Offset: 0x00015284
		private XmlValidatingReaderImpl CreateDtdValidatingReader(XmlReader baseReader)
		{
			return new XmlValidatingReaderImpl(baseReader, this.GetEventHandler(), (this.ValidationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) > XmlSchemaValidationFlags.None);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000170A0 File Offset: 0x000152A0
		internal static bool EnableLegacyXmlSettings()
		{
			if (XmlReaderSettings.s_enableLegacyXmlSettings != null)
			{
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			if (!BinaryCompatibility.TargetsAtLeast_Desktop_V4_5_2)
			{
				XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(true);
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(false);
			return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
		}

		// Token: 0x04000261 RID: 609
		private bool useAsync;

		// Token: 0x04000262 RID: 610
		private XmlNameTable nameTable;

		// Token: 0x04000263 RID: 611
		private XmlResolver xmlResolver;

		// Token: 0x04000264 RID: 612
		private int lineNumberOffset;

		// Token: 0x04000265 RID: 613
		private int linePositionOffset;

		// Token: 0x04000266 RID: 614
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000267 RID: 615
		private bool checkCharacters;

		// Token: 0x04000268 RID: 616
		private long maxCharactersInDocument;

		// Token: 0x04000269 RID: 617
		private long maxCharactersFromEntities;

		// Token: 0x0400026A RID: 618
		private bool ignoreWhitespace;

		// Token: 0x0400026B RID: 619
		private bool ignorePIs;

		// Token: 0x0400026C RID: 620
		private bool ignoreComments;

		// Token: 0x0400026D RID: 621
		private DtdProcessing dtdProcessing;

		// Token: 0x0400026E RID: 622
		private ValidationType validationType;

		// Token: 0x0400026F RID: 623
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x04000270 RID: 624
		private XmlSchemaSet schemas;

		// Token: 0x04000271 RID: 625
		private ValidationEventHandler valEventHandler;

		// Token: 0x04000272 RID: 626
		private bool closeInput;

		// Token: 0x04000273 RID: 627
		private bool isReadOnly;

		// Token: 0x04000275 RID: 629
		private static bool? s_enableLegacyXmlSettings;
	}
}
