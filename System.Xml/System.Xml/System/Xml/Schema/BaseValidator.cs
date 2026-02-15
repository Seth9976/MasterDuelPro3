using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x0200020E RID: 526
	internal class BaseValidator
	{
		// Token: 0x06001A2B RID: 6699 RVA: 0x00098F70 File Offset: 0x00097170
		public BaseValidator(BaseValidator other)
		{
			this.reader = other.reader;
			this.schemaCollection = other.schemaCollection;
			this.eventHandling = other.eventHandling;
			this.nameTable = other.nameTable;
			this.schemaNames = other.schemaNames;
			this.positionInfo = other.positionInfo;
			this.xmlResolver = other.xmlResolver;
			this.baseUri = other.baseUri;
			this.elementName = other.elementName;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00098FEF File Offset: 0x000971EF
		public BaseValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
		{
			this.reader = reader;
			this.schemaCollection = schemaCollection;
			this.eventHandling = eventHandling;
			this.nameTable = reader.NameTable;
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
			this.elementName = new XmlQualifiedName();
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0009902F File Offset: 0x0009722F
		public XmlValidatingReaderImpl Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00099037 File Offset: 0x00097237
		public XmlSchemaCollection SchemaCollection
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x0009903F File Offset: 0x0009723F
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00099048 File Offset: 0x00097248
		public SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames != null)
				{
					return this.schemaNames;
				}
				if (this.schemaCollection != null)
				{
					this.schemaNames = this.schemaCollection.GetSchemaNames(this.nameTable);
				}
				else
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0009909C File Offset: 0x0009729C
		public PositionInfo PositionInfo
		{
			get
			{
				return this.positionInfo;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000990A4 File Offset: 0x000972A4
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x000990AC File Offset: 0x000972AC
		public XmlResolver XmlResolver
		{
			get
			{
				return this.xmlResolver;
			}
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x000990B5 File Offset: 0x000972B5
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x000990BD File Offset: 0x000972BD
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x000990C6 File Offset: 0x000972C6
		public ValidationEventHandler EventHandler
		{
			get
			{
				return (ValidationEventHandler)this.eventHandling.EventHandler;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x000990D8 File Offset: 0x000972D8
		public SchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x000990E0 File Offset: 0x000972E0
		public IDtdInfo DtdInfo
		{
			set
			{
				SchemaInfo schemaInfo = value as SchemaInfo;
				if (schemaInfo == null)
				{
					throw new XmlException("An internal error has occurred.", string.Empty);
				}
				this.schemaInfo = schemaInfo;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0000A558 File Offset: 0x00008758
		public virtual void Validate()
		{
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0000A558 File Offset: 0x00008758
		public virtual void CompleteValidation()
		{
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual object FindId(string name)
		{
			return null;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00099110 File Offset: 0x00097310
		public void ValidateText()
		{
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return;
				}
				ContentValidator contentValidator = this.context.ElementDecl.ContentValidator;
				XmlSchemaContentType contentType = contentValidator.ContentType;
				if (contentType == XmlSchemaContentType.ElementOnly)
				{
					ArrayList arrayList = contentValidator.ExpectedElements(this.context, false);
					if (arrayList == null)
					{
						this.SendValidationEvent("The element {0} cannot contain text.", XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace));
					}
					else
					{
						this.SendValidationEvent("The element {0} cannot contain text. List of possible elements expected: {1}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace),
							XmlSchemaValidator.PrintExpectedElements(arrayList, false)
						});
					}
				}
				else if (contentType == XmlSchemaContentType.Empty)
				{
					this.SendValidationEvent("The element cannot contain text. Content model is empty.", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00099220 File Offset: 0x00097420
		public void ValidateWhitespace()
		{
			if (this.context.NeedValidateChildren)
			{
				int contentType = (int)this.context.ElementDecl.ContentValidator.ContentType;
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				if (contentType == 1)
				{
					this.SendValidationEvent("The element cannot contain white space. Content model is empty.", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000992B0 File Offset: 0x000974B0
		private void SaveTextValue(string value)
		{
			if (this.textString.Length == 0)
			{
				this.textString = value;
				return;
			}
			if (!this.hasSibling)
			{
				this.textValue.Append(this.textString);
				this.hasSibling = true;
			}
			this.textValue.Append(value);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00099300 File Offset: 0x00097500
		protected void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0009930E File Offset: 0x0009750E
		protected void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0009933E File Offset: 0x0009753E
		protected void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, arg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0009936E File Offset: 0x0009756E
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00099378 File Offset: 0x00097578
		protected void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000993A9 File Offset: 0x000975A9
		protected void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000993DA File Offset: 0x000975DA
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandling != null)
			{
				this.eventHandling.SendEvent(e, severity);
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000993F8 File Offset: 0x000975F8
		protected static void ProcessEntity(SchemaInfo sinfo, string name, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNumber, int linePosition)
		{
			XmlSchemaException ex = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				ex = new XmlSchemaException("Reference to an undeclared entity, '{0}'.", name, baseUri, lineNumber, linePosition);
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				ex = new XmlSchemaException("Reference to an unparsed entity, '{0}'.", name, baseUri, lineNumber, linePosition);
			}
			if (ex == null)
			{
				return;
			}
			if (eventhandler != null)
			{
				eventhandler(sender, new ValidationEventArgs(ex));
				return;
			}
			throw ex;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00099468 File Offset: 0x00097668
		protected static void ProcessEntity(SchemaInfo sinfo, string name, IValidationEventHandling eventHandling, string baseUriStr, int lineNumber, int linePosition)
		{
			string text = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				text = "Reference to an undeclared entity, '{0}'.";
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				text = "Reference to an unparsed entity, '{0}'.";
			}
			if (text == null)
			{
				return;
			}
			XmlSchemaException ex = new XmlSchemaException(text, name, baseUriStr, lineNumber, linePosition);
			if (eventHandling != null)
			{
				eventHandling.SendEvent(ex, XmlSeverityType.Error);
				return;
			}
			throw ex;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000994C8 File Offset: 0x000976C8
		public static BaseValidator CreateInstance(ValidationType valType, XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling, bool processIdentityConstraints)
		{
			switch (valType)
			{
			case ValidationType.None:
				return new BaseValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Auto:
				return new AutoValidator(reader, schemaCollection, eventHandling);
			case ValidationType.DTD:
				return new DtdValidator(reader, eventHandling, processIdentityConstraints);
			case ValidationType.XDR:
				return new XdrValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Schema:
				return new XsdValidator(reader, schemaCollection, eventHandling);
			default:
				return null;
			}
		}

		// Token: 0x04000B21 RID: 2849
		private XmlSchemaCollection schemaCollection;

		// Token: 0x04000B22 RID: 2850
		private IValidationEventHandling eventHandling;

		// Token: 0x04000B23 RID: 2851
		private XmlNameTable nameTable;

		// Token: 0x04000B24 RID: 2852
		private SchemaNames schemaNames;

		// Token: 0x04000B25 RID: 2853
		private PositionInfo positionInfo;

		// Token: 0x04000B26 RID: 2854
		private XmlResolver xmlResolver;

		// Token: 0x04000B27 RID: 2855
		private Uri baseUri;

		// Token: 0x04000B28 RID: 2856
		protected SchemaInfo schemaInfo;

		// Token: 0x04000B29 RID: 2857
		protected XmlValidatingReaderImpl reader;

		// Token: 0x04000B2A RID: 2858
		protected XmlQualifiedName elementName;

		// Token: 0x04000B2B RID: 2859
		protected ValidationState context;

		// Token: 0x04000B2C RID: 2860
		protected StringBuilder textValue;

		// Token: 0x04000B2D RID: 2861
		protected string textString;

		// Token: 0x04000B2E RID: 2862
		protected bool hasSibling;

		// Token: 0x04000B2F RID: 2863
		protected bool checkDatatype;
	}
}
