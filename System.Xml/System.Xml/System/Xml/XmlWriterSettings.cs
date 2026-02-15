using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlWriter" /> object created by the <see cref="Overload:System.Xml.XmlWriter.Create" /> method.</summary>
	// Token: 0x020000C5 RID: 197
	public sealed class XmlWriterSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWriterSettings" /> class.</summary>
		// Token: 0x06000967 RID: 2407 RVA: 0x0003493E File Offset: 0x00032B3E
		public XmlWriterSettings()
		{
			this.Initialize();
		}

		/// <summary>Gets or sets a value that indicates whether asynchronous <see cref="T:System.Xml.XmlWriter" /> methods can be used on a particular <see cref="T:System.Xml.XmlWriter" /> instance.</summary>
		/// <returns>true if asynchronous methods can be used; otherwise, false.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00034957 File Offset: 0x00032B57
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0003495F File Offset: 0x00032B5F
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

		/// <summary>Gets or sets the type of text encoding to use.</summary>
		/// <returns>The text encoding to use. The default is Encoding.UTF8.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00034973 File Offset: 0x00032B73
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0003497B File Offset: 0x00032B7B
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.CheckReadOnly("Encoding");
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to omit an XML declaration.</summary>
		/// <returns>true to omit the XML declaration; otherwise, false. The default is false, an XML declaration is written.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0003498F File Offset: 0x00032B8F
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x00034997 File Offset: 0x00032B97
		public bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDecl;
			}
			set
			{
				this.CheckReadOnly("OmitXmlDeclaration");
				this.omitXmlDecl = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize line breaks in the output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.NewLineHandling" /> values. The default is <see cref="F:System.Xml.NewLineHandling.Replace" />.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000349AB File Offset: 0x00032BAB
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x000349B3 File Offset: 0x00032BB3
		public NewLineHandling NewLineHandling
		{
			get
			{
				return this.newLineHandling;
			}
			set
			{
				this.CheckReadOnly("NewLineHandling");
				if (value > NewLineHandling.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.newLineHandling = value;
			}
		}

		/// <summary>Gets or sets the character string to use for line breaks.</summary>
		/// <returns>The character string to use for line breaks. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is \r\n (carriage return, new line).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.NewLineChars" /> is null.</exception>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000349D6 File Offset: 0x00032BD6
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x000349DE File Offset: 0x00032BDE
		public string NewLineChars
		{
			get
			{
				return this.newLineChars;
			}
			set
			{
				this.CheckReadOnly("NewLineChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.newLineChars = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent elements.</summary>
		/// <returns>true to write individual elements on new lines and indent; otherwise, false. The default is false.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00034A00 File Offset: 0x00032C00
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00034A0B File Offset: 0x00032C0B
		public bool Indent
		{
			get
			{
				return this.indent == TriState.True;
			}
			set
			{
				this.CheckReadOnly("Indent");
				this.indent = (value ? TriState.True : TriState.False);
			}
		}

		/// <summary>Gets or sets the character string to use when indenting. This setting is used when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property is set to true.</summary>
		/// <returns>The character string to use when indenting. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is two spaces.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.IndentChars" /> is null.</exception>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00034A25 File Offset: 0x00032C25
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00034A2D File Offset: 0x00032C2D
		public string IndentChars
		{
			get
			{
				return this.indentChars;
			}
			set
			{
				this.CheckReadOnly("IndentChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.indentChars = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to write attributes on a new line.</summary>
		/// <returns>true to write attributes on individual lines; otherwise, false. The default is false.NoteThis setting has no effect when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property value is false.When <see cref="P:System.Xml.XmlWriterSettings.NewLineOnAttributes" /> is set to true, each attribute is pre-pended with a new line and one extra level of indentation.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00034A4F File Offset: 0x00032C4F
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00034A57 File Offset: 0x00032C57
		public bool NewLineOnAttributes
		{
			get
			{
				return this.newLineOnAttributes;
			}
			set
			{
				this.CheckReadOnly("NewLineOnAttributes");
				this.newLineOnAttributes = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlWriter" /> should also close the underlying stream or <see cref="T:System.IO.TextWriter" /> when the <see cref="M:System.Xml.XmlWriter.Close" /> method is called.</summary>
		/// <returns>true to also close the underlying stream or <see cref="T:System.IO.TextWriter" />; otherwise, false. The default is false.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00034A6B File Offset: 0x00032C6B
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00034A73 File Offset: 0x00032C73
		public bool CloseOutput
		{
			get
			{
				return this.closeOutput;
			}
			set
			{
				this.CheckReadOnly("CloseOutput");
				this.closeOutput = value;
			}
		}

		/// <summary>Gets or sets the level of conformance which the <see cref="T:System.Xml.XmlWriter" /> complies with.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ConformanceLevel" /> values. The default is ConformanceLevel.Document.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00034A87 File Offset: 0x00032C87
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00034A8F File Offset: 0x00032C8F
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
		/// <returns>true to do character checking; otherwise, false. The default is true.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00034AB2 File Offset: 0x00032CB2
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00034ABA File Offset: 0x00032CBA
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> should remove duplicate namespace declarations when writing XML content. The default behavior is for the writer to output all namespace declarations that are present in the writer's namespace resolver.</summary>
		/// <returns>The <see cref="T:System.Xml.NamespaceHandling" /> enumeration used to specify whether to remove duplicate namespace declarations in the <see cref="T:System.Xml.XmlWriter" />.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00034ACE File Offset: 0x00032CCE
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00034AD6 File Offset: 0x00032CD6
		public NamespaceHandling NamespaceHandling
		{
			get
			{
				return this.namespaceHandling;
			}
			set
			{
				this.CheckReadOnly("NamespaceHandling");
				if (value > NamespaceHandling.OmitDuplicates)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.namespaceHandling = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> will add closing tags to all unclosed element tags when the <see cref="M:System.Xml.XmlWriter.Close" /> method is called.</summary>
		/// <returns>true if all unclosed element tags will be closed out; otherwise, false. The default value is true. </returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00034AF9 File Offset: 0x00032CF9
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00034B01 File Offset: 0x00032D01
		public bool WriteEndDocumentOnClose
		{
			get
			{
				return this.writeEndDocumentOnClose;
			}
			set
			{
				this.CheckReadOnly("WriteEndDocumentOnClose");
				this.writeEndDocumentOnClose = value;
			}
		}

		/// <summary>Gets the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlOutputMethod" /> values. The default is <see cref="F:System.Xml.XmlOutputMethod.Xml" />.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00034B15 File Offset: 0x00032D15
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00034B1D File Offset: 0x00032D1D
		public XmlOutputMethod OutputMethod
		{
			get
			{
				return this.outputMethod;
			}
			internal set
			{
				this.outputMethod = value;
			}
		}

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlWriterSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlWriterSettings" /> object.</returns>
		// Token: 0x06000984 RID: 2436 RVA: 0x00034B26 File Offset: 0x00032D26
		public XmlWriterSettings Clone()
		{
			XmlWriterSettings xmlWriterSettings = base.MemberwiseClone() as XmlWriterSettings;
			xmlWriterSettings.cdataSections = new List<XmlQualifiedName>(this.cdataSections);
			xmlWriterSettings.isReadOnly = false;
			return xmlWriterSettings;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00034B4B File Offset: 0x00032D4B
		internal List<XmlQualifiedName> CDataSectionElements
		{
			get
			{
				return this.cdataSections;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> do not escape URI attributes.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XmlWriter" /> do not escape URI attributes; otherwise, false.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00034B53 File Offset: 0x00032D53
		public bool DoNotEscapeUriAttributes
		{
			get
			{
				return this.doNotEscapeUriAttributes;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00034B5B File Offset: 0x00032D5B
		internal bool MergeCDataSections
		{
			get
			{
				return this.mergeCDataSections;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00034B63 File Offset: 0x00032D63
		internal string MediaType
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00034B6B File Offset: 0x00032D6B
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00034B73 File Offset: 0x00032D73
		internal string DocTypeSystem
		{
			get
			{
				return this.docTypeSystem;
			}
			set
			{
				this.CheckReadOnly("DocTypeSystem");
				this.docTypeSystem = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00034B87 File Offset: 0x00032D87
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00034B8F File Offset: 0x00032D8F
		internal string DocTypePublic
		{
			get
			{
				return this.docTypePublic;
			}
			set
			{
				this.CheckReadOnly("DocTypePublic");
				this.docTypePublic = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00034BA3 File Offset: 0x00032DA3
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00034BAB File Offset: 0x00032DAB
		internal XmlStandalone Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				this.CheckReadOnly("Standalone");
				this.standalone = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00034BBF File Offset: 0x00032DBF
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00034BC7 File Offset: 0x00032DC7
		internal bool AutoXmlDeclaration
		{
			get
			{
				return this.autoXmlDecl;
			}
			set
			{
				this.CheckReadOnly("AutoXmlDeclaration");
				this.autoXmlDecl = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00034BDB File Offset: 0x00032DDB
		internal TriState IndentInternal
		{
			get
			{
				return this.indent;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00034BE3 File Offset: 0x00032DE3
		internal bool IsQuerySpecific
		{
			get
			{
				return this.cdataSections.Count != 0 || this.docTypePublic != null || this.docTypeSystem != null || this.standalone == XmlStandalone.Yes;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00034C10 File Offset: 0x00032E10
		internal XmlWriter CreateWriter(Stream output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			if (this.Encoding.WebName == "utf-8")
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextUtf8RawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			else
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextEncodedRawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00034D60 File Offset: 0x00032F60
		internal XmlWriter CreateWriter(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			switch (this.OutputMethod)
			{
			case XmlOutputMethod.Xml:
				if (this.Indent)
				{
					xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new XmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Html:
				if (this.Indent)
				{
					xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new HtmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Text:
				xmlWriter = new TextEncodedRawTextWriter(output, this);
				break;
			case XmlOutputMethod.AutoDetect:
				xmlWriter = new XmlAutoDetectWriter(output, this);
				break;
			default:
				return null;
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x170001DB RID: 475
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00034E1E File Offset: 0x0003301E
		internal bool ReadOnly
		{
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00034E27 File Offset: 0x00033027
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("The '{0}' property is read only and cannot be set.", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00034E54 File Offset: 0x00033054
		private void Initialize()
		{
			this.encoding = Encoding.UTF8;
			this.omitXmlDecl = false;
			this.newLineHandling = NewLineHandling.Replace;
			this.newLineChars = Environment.NewLine;
			this.indent = TriState.Unknown;
			this.indentChars = "  ";
			this.newLineOnAttributes = false;
			this.closeOutput = false;
			this.namespaceHandling = NamespaceHandling.Default;
			this.conformanceLevel = ConformanceLevel.Document;
			this.checkCharacters = true;
			this.writeEndDocumentOnClose = true;
			this.outputMethod = XmlOutputMethod.Xml;
			this.cdataSections.Clear();
			this.mergeCDataSections = false;
			this.mediaType = null;
			this.docTypeSystem = null;
			this.docTypePublic = null;
			this.standalone = XmlStandalone.Omit;
			this.doNotEscapeUriAttributes = false;
			this.useAsync = false;
			this.isReadOnly = false;
		}

		// Token: 0x04000576 RID: 1398
		private bool useAsync;

		// Token: 0x04000577 RID: 1399
		private Encoding encoding;

		// Token: 0x04000578 RID: 1400
		private bool omitXmlDecl;

		// Token: 0x04000579 RID: 1401
		private NewLineHandling newLineHandling;

		// Token: 0x0400057A RID: 1402
		private string newLineChars;

		// Token: 0x0400057B RID: 1403
		private TriState indent;

		// Token: 0x0400057C RID: 1404
		private string indentChars;

		// Token: 0x0400057D RID: 1405
		private bool newLineOnAttributes;

		// Token: 0x0400057E RID: 1406
		private bool closeOutput;

		// Token: 0x0400057F RID: 1407
		private NamespaceHandling namespaceHandling;

		// Token: 0x04000580 RID: 1408
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000581 RID: 1409
		private bool checkCharacters;

		// Token: 0x04000582 RID: 1410
		private bool writeEndDocumentOnClose;

		// Token: 0x04000583 RID: 1411
		private XmlOutputMethod outputMethod;

		// Token: 0x04000584 RID: 1412
		private List<XmlQualifiedName> cdataSections = new List<XmlQualifiedName>();

		// Token: 0x04000585 RID: 1413
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000586 RID: 1414
		private bool mergeCDataSections;

		// Token: 0x04000587 RID: 1415
		private string mediaType;

		// Token: 0x04000588 RID: 1416
		private string docTypeSystem;

		// Token: 0x04000589 RID: 1417
		private string docTypePublic;

		// Token: 0x0400058A RID: 1418
		private XmlStandalone standalone;

		// Token: 0x0400058B RID: 1419
		private bool autoXmlDecl;

		// Token: 0x0400058C RID: 1420
		private bool isReadOnly;
	}
}
