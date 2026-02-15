using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200031E RID: 798
	internal sealed class XsdBuilder : SchemaBuilder
	{
		// Token: 0x06002434 RID: 9268 RVA: 0x000CC6D8 File Offset: 0x000CA8D8
		internal XsdBuilder(XmlReader reader, XmlNamespaceManager curmgr, XmlSchema schema, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this.reader = reader;
			this.schema = schema;
			this.xso = schema;
			this.namespaceManager = new XsdBuilder.BuilderNamespaceManager(curmgr, reader);
			this.validationEventHandler = eventhandler;
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.stateHistory = new HWStack(10);
			this.currentEntry = XsdBuilder.SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000CC770 File Offset: 0x000CA970
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				this.xso = null;
				this.currentEntry.InitFunc(this, null);
				this.RecordPosition();
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' element is not supported in this context.", xmlQualifiedName.ToString());
			}
			return false;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000CC7D4 File Offset: 0x000CA9D4
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			if (this.currentEntry.Attributes != null)
			{
				for (int i = 0; i < this.currentEntry.Attributes.Length; i++)
				{
					XsdBuilder.XsdAttributeEntry xsdAttributeEntry = this.currentEntry.Attributes[i];
					if (this.schemaNames.TokenToQName[(int)xsdAttributeEntry.Attribute].Equals(xmlQualifiedName))
					{
						try
						{
							xsdAttributeEntry.BuildFunc(this, value);
						}
						catch (XmlSchemaException ex)
						{
							ex.SetSource(this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
							this.SendValidationEvent("The value for the '{0}' attribute is invalid - {1}", new string[] { name, ex.Message }, XmlSeverityType.Error);
						}
						return;
					}
				}
			}
			if (!(ns != this.schemaNames.NsXs) || ns.Length == 0)
			{
				this.SendValidationEvent("The '{0}' attribute is not supported in this context.", xmlQualifiedName.ToString());
				return;
			}
			if (ns == this.schemaNames.NsXmlNs)
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				this.namespaces.Add((name == this.schemaNames.QnXmlNs.Name) ? string.Empty : name, value);
				return;
			}
			XmlAttribute xmlAttribute = new XmlAttribute(prefix, name, ns, this.schema.Document);
			xmlAttribute.Value = value;
			this.unhandledAttributes.Add(xmlAttribute);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000CC95C File Offset: 0x000CAB5C
		internal override bool IsContentParsed()
		{
			return this.currentEntry.ParseContent;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000CC969 File Offset: 0x000CAB69
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			this.markup = markup;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000CC972 File Offset: 0x000CAB72
		internal override void ProcessCData(string value)
		{
			this.SendValidationEvent("The following text is not allowed in this context: '{0}'.", value);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000CC980 File Offset: 0x000CAB80
		internal override void StartChildren()
		{
			if (this.xso != null)
			{
				if (this.namespaces != null && this.namespaces.Count > 0)
				{
					this.xso.Namespaces.Namespaces = this.namespaces;
					this.namespaces = null;
				}
				if (this.unhandledAttributes.Count != 0)
				{
					this.xso.SetUnhandledAttributes((XmlAttribute[])this.unhandledAttributes.ToArray(typeof(XmlAttribute)));
					this.unhandledAttributes.Clear();
				}
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000CCA05 File Offset: 0x000CAC05
		internal override void EndChildren()
		{
			if (this.currentEntry.EndChildFunc != null)
			{
				this.currentEntry.EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000CCA2C File Offset: 0x000CAC2C
		private void Push()
		{
			this.stateHistory.Push();
			this.stateHistory[this.stateHistory.Length - 1] = this.currentEntry;
			this.containerStack.Push(this.GetContainer(this.currentEntry.CurrentState));
			this.currentEntry = this.nextEntry;
			if (this.currentEntry.Name != SchemaNames.Token.XsdAnnotation)
			{
				this.hasChild = false;
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000CCAA1 File Offset: 0x000CACA1
		private void Pop()
		{
			this.currentEntry = (XsdBuilder.XsdEntry)this.stateHistory.Pop();
			this.SetContainer(this.currentEntry.CurrentState, this.containerStack.Pop());
			this.hasChild = true;
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x000CCADC File Offset: 0x000CACDC
		private SchemaNames.Token CurrentElement
		{
			get
			{
				return this.currentEntry.Name;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000CCAE9 File Offset: 0x000CACE9
		private SchemaNames.Token ParentElement
		{
			get
			{
				return ((XsdBuilder.XsdEntry)this.stateHistory[this.stateHistory.Length - 1]).Name;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x000CCB0D File Offset: 0x000CAD0D
		private XmlSchemaObject ParentContainer
		{
			get
			{
				return (XmlSchemaObject)this.containerStack.Peek();
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000CCB20 File Offset: 0x000CAD20
		private XmlSchemaObject GetContainer(XsdBuilder.State state)
		{
			XmlSchemaObject xmlSchemaObject = null;
			switch (state)
			{
			case XsdBuilder.State.Schema:
				xmlSchemaObject = this.schema;
				break;
			case XsdBuilder.State.Annotation:
				xmlSchemaObject = this.annotation;
				break;
			case XsdBuilder.State.Include:
				xmlSchemaObject = this.include;
				break;
			case XsdBuilder.State.Import:
				xmlSchemaObject = this.import;
				break;
			case XsdBuilder.State.Element:
				xmlSchemaObject = this.element;
				break;
			case XsdBuilder.State.Attribute:
				xmlSchemaObject = this.attribute;
				break;
			case XsdBuilder.State.AttributeGroup:
				xmlSchemaObject = this.attributeGroup;
				break;
			case XsdBuilder.State.AttributeGroupRef:
				xmlSchemaObject = this.attributeGroupRef;
				break;
			case XsdBuilder.State.AnyAttribute:
				xmlSchemaObject = this.anyAttribute;
				break;
			case XsdBuilder.State.Group:
				xmlSchemaObject = this.group;
				break;
			case XsdBuilder.State.GroupRef:
				xmlSchemaObject = this.groupRef;
				break;
			case XsdBuilder.State.All:
				xmlSchemaObject = this.all;
				break;
			case XsdBuilder.State.Choice:
				xmlSchemaObject = this.choice;
				break;
			case XsdBuilder.State.Sequence:
				xmlSchemaObject = this.sequence;
				break;
			case XsdBuilder.State.Any:
				xmlSchemaObject = this.anyElement;
				break;
			case XsdBuilder.State.Notation:
				xmlSchemaObject = this.notation;
				break;
			case XsdBuilder.State.SimpleType:
				xmlSchemaObject = this.simpleType;
				break;
			case XsdBuilder.State.ComplexType:
				xmlSchemaObject = this.complexType;
				break;
			case XsdBuilder.State.ComplexContent:
				xmlSchemaObject = this.complexContent;
				break;
			case XsdBuilder.State.ComplexContentRestriction:
				xmlSchemaObject = this.complexContentRestriction;
				break;
			case XsdBuilder.State.ComplexContentExtension:
				xmlSchemaObject = this.complexContentExtension;
				break;
			case XsdBuilder.State.SimpleContent:
				xmlSchemaObject = this.simpleContent;
				break;
			case XsdBuilder.State.SimpleContentExtension:
				xmlSchemaObject = this.simpleContentExtension;
				break;
			case XsdBuilder.State.SimpleContentRestriction:
				xmlSchemaObject = this.simpleContentRestriction;
				break;
			case XsdBuilder.State.SimpleTypeUnion:
				xmlSchemaObject = this.simpleTypeUnion;
				break;
			case XsdBuilder.State.SimpleTypeList:
				xmlSchemaObject = this.simpleTypeList;
				break;
			case XsdBuilder.State.SimpleTypeRestriction:
				xmlSchemaObject = this.simpleTypeRestriction;
				break;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				xmlSchemaObject = this.identityConstraint;
				break;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				xmlSchemaObject = this.xpath;
				break;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				xmlSchemaObject = this.facet;
				break;
			case XsdBuilder.State.AppInfo:
				xmlSchemaObject = this.appInfo;
				break;
			case XsdBuilder.State.Documentation:
				xmlSchemaObject = this.documentation;
				break;
			case XsdBuilder.State.Redefine:
				xmlSchemaObject = this.redefine;
				break;
			}
			return xmlSchemaObject;
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000CCD58 File Offset: 0x000CAF58
		private void SetContainer(XsdBuilder.State state, object container)
		{
			switch (state)
			{
			case XsdBuilder.State.Root:
			case XsdBuilder.State.Schema:
				break;
			case XsdBuilder.State.Annotation:
				this.annotation = (XmlSchemaAnnotation)container;
				return;
			case XsdBuilder.State.Include:
				this.include = (XmlSchemaInclude)container;
				return;
			case XsdBuilder.State.Import:
				this.import = (XmlSchemaImport)container;
				return;
			case XsdBuilder.State.Element:
				this.element = (XmlSchemaElement)container;
				return;
			case XsdBuilder.State.Attribute:
				this.attribute = (XmlSchemaAttribute)container;
				return;
			case XsdBuilder.State.AttributeGroup:
				this.attributeGroup = (XmlSchemaAttributeGroup)container;
				return;
			case XsdBuilder.State.AttributeGroupRef:
				this.attributeGroupRef = (XmlSchemaAttributeGroupRef)container;
				return;
			case XsdBuilder.State.AnyAttribute:
				this.anyAttribute = (XmlSchemaAnyAttribute)container;
				return;
			case XsdBuilder.State.Group:
				this.group = (XmlSchemaGroup)container;
				return;
			case XsdBuilder.State.GroupRef:
				this.groupRef = (XmlSchemaGroupRef)container;
				return;
			case XsdBuilder.State.All:
				this.all = (XmlSchemaAll)container;
				return;
			case XsdBuilder.State.Choice:
				this.choice = (XmlSchemaChoice)container;
				return;
			case XsdBuilder.State.Sequence:
				this.sequence = (XmlSchemaSequence)container;
				return;
			case XsdBuilder.State.Any:
				this.anyElement = (XmlSchemaAny)container;
				return;
			case XsdBuilder.State.Notation:
				this.notation = (XmlSchemaNotation)container;
				return;
			case XsdBuilder.State.SimpleType:
				this.simpleType = (XmlSchemaSimpleType)container;
				return;
			case XsdBuilder.State.ComplexType:
				this.complexType = (XmlSchemaComplexType)container;
				return;
			case XsdBuilder.State.ComplexContent:
				this.complexContent = (XmlSchemaComplexContent)container;
				return;
			case XsdBuilder.State.ComplexContentRestriction:
				this.complexContentRestriction = (XmlSchemaComplexContentRestriction)container;
				return;
			case XsdBuilder.State.ComplexContentExtension:
				this.complexContentExtension = (XmlSchemaComplexContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContent:
				this.simpleContent = (XmlSchemaSimpleContent)container;
				return;
			case XsdBuilder.State.SimpleContentExtension:
				this.simpleContentExtension = (XmlSchemaSimpleContentExtension)container;
				return;
			case XsdBuilder.State.SimpleContentRestriction:
				this.simpleContentRestriction = (XmlSchemaSimpleContentRestriction)container;
				return;
			case XsdBuilder.State.SimpleTypeUnion:
				this.simpleTypeUnion = (XmlSchemaSimpleTypeUnion)container;
				return;
			case XsdBuilder.State.SimpleTypeList:
				this.simpleTypeList = (XmlSchemaSimpleTypeList)container;
				return;
			case XsdBuilder.State.SimpleTypeRestriction:
				this.simpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)container;
				return;
			case XsdBuilder.State.Unique:
			case XsdBuilder.State.Key:
			case XsdBuilder.State.KeyRef:
				this.identityConstraint = (XmlSchemaIdentityConstraint)container;
				return;
			case XsdBuilder.State.Selector:
			case XsdBuilder.State.Field:
				this.xpath = (XmlSchemaXPath)container;
				return;
			case XsdBuilder.State.MinExclusive:
			case XsdBuilder.State.MinInclusive:
			case XsdBuilder.State.MaxExclusive:
			case XsdBuilder.State.MaxInclusive:
			case XsdBuilder.State.TotalDigits:
			case XsdBuilder.State.FractionDigits:
			case XsdBuilder.State.Length:
			case XsdBuilder.State.MinLength:
			case XsdBuilder.State.MaxLength:
			case XsdBuilder.State.Enumeration:
			case XsdBuilder.State.Pattern:
			case XsdBuilder.State.WhiteSpace:
				this.facet = (XmlSchemaFacet)container;
				return;
			case XsdBuilder.State.AppInfo:
				this.appInfo = (XmlSchemaAppInfo)container;
				return;
			case XsdBuilder.State.Documentation:
				this.documentation = (XmlSchemaDocumentation)container;
				return;
			case XsdBuilder.State.Redefine:
				this.redefine = (XmlSchemaRedefine)container;
				break;
			default:
				return;
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000CCFCB File Offset: 0x000CB1CB
		private static void BuildAnnotated_Id(XsdBuilder builder, string value)
		{
			builder.xso.IdAttribute = value;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000CCFD9 File Offset: 0x000CB1D9
		private static void BuildSchema_AttributeFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.AttributeFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "attributeFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000CCFF7 File Offset: 0x000CB1F7
		private static void BuildSchema_ElementFormDefault(XsdBuilder builder, string value)
		{
			builder.schema.ElementFormDefault = (XmlSchemaForm)builder.ParseEnum(value, "elementFormDefault", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000CD015 File Offset: 0x000CB215
		private static void BuildSchema_TargetNamespace(XsdBuilder builder, string value)
		{
			builder.schema.TargetNamespace = value;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000CD023 File Offset: 0x000CB223
		private static void BuildSchema_Version(XsdBuilder builder, string value)
		{
			builder.schema.Version = value;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000CD031 File Offset: 0x000CB231
		private static void BuildSchema_FinalDefault(XsdBuilder builder, string value)
		{
			builder.schema.FinalDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "finalDefault");
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000CD04A File Offset: 0x000CB24A
		private static void BuildSchema_BlockDefault(XsdBuilder builder, string value)
		{
			builder.schema.BlockDefault = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "blockDefault");
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000CD063 File Offset: 0x000CB263
		private static void InitSchema(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = true;
			builder.xso = builder.schema;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000CD078 File Offset: 0x000CB278
		private static void InitInclude(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'include' element cannot appear at this location.", null);
			}
			builder.xso = (builder.include = new XmlSchemaInclude());
			builder.schema.Includes.Add(builder.include);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000CD0C4 File Offset: 0x000CB2C4
		private static void BuildInclude_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.include.SchemaLocation = value;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000CD0D4 File Offset: 0x000CB2D4
		private static void InitImport(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'import' element cannot appear at this location.", null);
			}
			builder.xso = (builder.import = new XmlSchemaImport());
			builder.schema.Includes.Add(builder.import);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000CD120 File Offset: 0x000CB320
		private static void BuildImport_Namespace(XsdBuilder builder, string value)
		{
			builder.import.Namespace = value;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000CD12E File Offset: 0x000CB32E
		private static void BuildImport_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.import.SchemaLocation = value;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000CD13C File Offset: 0x000CB33C
		private static void InitRedefine(XsdBuilder builder, string value)
		{
			if (!builder.canIncludeImport)
			{
				builder.SendValidationEvent("The 'redefine' element cannot appear at this location.", null);
			}
			builder.xso = (builder.redefine = new XmlSchemaRedefine());
			builder.schema.Includes.Add(builder.redefine);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000CD188 File Offset: 0x000CB388
		private static void BuildRedefine_SchemaLocation(XsdBuilder builder, string value)
		{
			builder.redefine.SchemaLocation = value;
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000CD196 File Offset: 0x000CB396
		private static void EndRedefine(XsdBuilder builder)
		{
			builder.canIncludeImport = true;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000CD1A0 File Offset: 0x000CB3A0
		private static void InitAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attribute = new XmlSchemaAttribute());
			if (builder.ParentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attribute);
			}
			else
			{
				builder.AddAttribute(builder.attribute);
			}
			builder.canIncludeImport = false;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000CD1F7 File Offset: 0x000CB3F7
		private static void BuildAttribute_Default(XsdBuilder builder, string value)
		{
			builder.attribute.DefaultValue = value;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000CD205 File Offset: 0x000CB405
		private static void BuildAttribute_Fixed(XsdBuilder builder, string value)
		{
			builder.attribute.FixedValue = value;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000CD213 File Offset: 0x000CB413
		private static void BuildAttribute_Form(XsdBuilder builder, string value)
		{
			builder.attribute.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000CD231 File Offset: 0x000CB431
		private static void BuildAttribute_Use(XsdBuilder builder, string value)
		{
			builder.attribute.Use = (XmlSchemaUse)builder.ParseEnum(value, "use", XsdBuilder.UseStringValues);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000CD24F File Offset: 0x000CB44F
		private static void BuildAttribute_Ref(XsdBuilder builder, string value)
		{
			builder.attribute.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000CD268 File Offset: 0x000CB468
		private static void BuildAttribute_Name(XsdBuilder builder, string value)
		{
			builder.attribute.Name = value;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000CD276 File Offset: 0x000CB476
		private static void BuildAttribute_Type(XsdBuilder builder, string value)
		{
			builder.attribute.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000CD290 File Offset: 0x000CB490
		private static void InitElement(XsdBuilder builder, string value)
		{
			builder.xso = (builder.element = new XmlSchemaElement());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.element);
				return;
			}
			switch (parentElement)
			{
			case SchemaNames.Token.XsdAll:
				builder.all.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdChoice:
				builder.choice.Items.Add(builder.element);
				return;
			case SchemaNames.Token.XsdSequence:
				builder.sequence.Items.Add(builder.element);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000CD339 File Offset: 0x000CB539
		private static void BuildElement_Abstract(XsdBuilder builder, string value)
		{
			builder.element.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000CD352 File Offset: 0x000CB552
		private static void BuildElement_Block(XsdBuilder builder, string value)
		{
			builder.element.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000CD36B File Offset: 0x000CB56B
		private static void BuildElement_Default(XsdBuilder builder, string value)
		{
			builder.element.DefaultValue = value;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000CD379 File Offset: 0x000CB579
		private static void BuildElement_Form(XsdBuilder builder, string value)
		{
			builder.element.Form = (XmlSchemaForm)builder.ParseEnum(value, "form", XsdBuilder.FormStringValues);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000CD397 File Offset: 0x000CB597
		private static void BuildElement_SubstitutionGroup(XsdBuilder builder, string value)
		{
			builder.element.SubstitutionGroup = builder.ParseQName(value, "substitutionGroup");
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000CD3B0 File Offset: 0x000CB5B0
		private static void BuildElement_Final(XsdBuilder builder, string value)
		{
			builder.element.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000CD3C9 File Offset: 0x000CB5C9
		private static void BuildElement_Fixed(XsdBuilder builder, string value)
		{
			builder.element.FixedValue = value;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000CD3D7 File Offset: 0x000CB5D7
		private static void BuildElement_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.element, value);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000CD3E6 File Offset: 0x000CB5E6
		private static void BuildElement_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.element, value);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000CD3F5 File Offset: 0x000CB5F5
		private static void BuildElement_Name(XsdBuilder builder, string value)
		{
			builder.element.Name = value;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000CD403 File Offset: 0x000CB603
		private static void BuildElement_Nillable(XsdBuilder builder, string value)
		{
			builder.element.IsNillable = builder.ParseBoolean(value, "nillable");
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000CD41C File Offset: 0x000CB61C
		private static void BuildElement_Ref(XsdBuilder builder, string value)
		{
			builder.element.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000CD435 File Offset: 0x000CB635
		private static void BuildElement_Type(XsdBuilder builder, string value)
		{
			builder.element.SchemaTypeName = builder.ParseQName(value, "type");
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000CD450 File Offset: 0x000CB650
		private static void InitSimpleType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.simpleType = new XmlSchemaSimpleType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdElement)
			{
				if (parentElement == SchemaNames.Token.XsdSchema)
				{
					builder.canIncludeImport = false;
					builder.schema.Items.Add(builder.simpleType);
					return;
				}
				if (parentElement != SchemaNames.Token.XsdElement)
				{
					return;
				}
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.", null);
				}
				builder.element.SchemaType = builder.simpleType;
				return;
			}
			else
			{
				if (parentElement != SchemaNames.Token.XsdAttribute)
				{
					switch (parentElement)
					{
					case SchemaNames.Token.XsdSimpleContentRestriction:
						if (builder.simpleContentRestriction.BaseType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null || builder.simpleContentRestriction.Facets.Count != 0)
						{
							builder.SendValidationEvent("'simpleType' should be the first child of restriction.", null);
						}
						builder.simpleContentRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeList:
						if (builder.simpleTypeList.ItemType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						builder.simpleTypeList.ItemType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeRestriction:
						if (builder.simpleTypeRestriction.BaseType != null)
						{
							builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
						}
						builder.simpleTypeRestriction.BaseType = builder.simpleType;
						return;
					case SchemaNames.Token.XsdSimpleTypeUnion:
						builder.simpleTypeUnion.BaseTypes.Add(builder.simpleType);
						break;
					case SchemaNames.Token.XsdWhitespace:
						break;
					case SchemaNames.Token.XsdRedefine:
						builder.redefine.Items.Add(builder.simpleType);
						return;
					default:
						return;
					}
					return;
				}
				if (builder.attribute.SchemaType != null)
				{
					builder.SendValidationEvent("'{0}' is a duplicate XSD element.", "simpleType");
				}
				builder.attribute.SchemaType = builder.simpleType;
				return;
			}
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000CD646 File Offset: 0x000CB846
		private static void BuildSimpleType_Name(XsdBuilder builder, string value)
		{
			builder.simpleType.Name = value;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000CD654 File Offset: 0x000CB854
		private static void BuildSimpleType_Final(XsdBuilder builder, string value)
		{
			builder.simpleType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000CD670 File Offset: 0x000CB870
		private static void InitSimpleTypeUnion(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeUnion = new XmlSchemaSimpleTypeUnion());
			builder.simpleType.Content = builder.simpleTypeUnion;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000CD6BC File Offset: 0x000CB8BC
		private static void BuildSimpleTypeUnion_MemberTypes(XsdBuilder builder, string value)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName).DeriveByList(null);
			try
			{
				builder.simpleTypeUnion.MemberTypes = (XmlQualifiedName[])xmlSchemaDatatype.ParseValue(value, builder.nameTable, builder.namespaceManager);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000CD740 File Offset: 0x000CB940
		private static void InitSimpleTypeList(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeList = new XmlSchemaSimpleTypeList());
			builder.simpleType.Content = builder.simpleTypeList;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000CD78B File Offset: 0x000CB98B
		private static void BuildSimpleTypeList_ItemType(XsdBuilder builder, string value)
		{
			builder.simpleTypeList.ItemTypeName = builder.ParseQName(value, "itemType");
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000CD7A4 File Offset: 0x000CB9A4
		private static void InitSimpleTypeRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleType.Content != null)
			{
				builder.SendValidationEvent("'simpleType' should have only one child 'union', 'list', or 'restriction'.", null);
			}
			builder.xso = (builder.simpleTypeRestriction = new XmlSchemaSimpleTypeRestriction());
			builder.simpleType.Content = builder.simpleTypeRestriction;
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000CD7EF File Offset: 0x000CB9EF
		private static void BuildSimpleTypeRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleTypeRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000CD808 File Offset: 0x000CBA08
		private static void InitComplexType(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexType = new XmlSchemaComplexType());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.canIncludeImport = false;
				builder.schema.Items.Add(builder.complexType);
				return;
			}
			if (parentElement == SchemaNames.Token.XsdElement)
			{
				if (builder.element.SchemaType != null)
				{
					builder.SendValidationEvent("The '{0}' element already exists in the content model.", "complexType");
				}
				if (builder.element.Constraints.Count != 0)
				{
					builder.SendValidationEvent("'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.", null);
				}
				builder.element.SchemaType = builder.complexType;
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.complexType);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000CD8C3 File Offset: 0x000CBAC3
		private static void BuildComplexType_Abstract(XsdBuilder builder, string value)
		{
			builder.complexType.IsAbstract = builder.ParseBoolean(value, "abstract");
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000CD8DC File Offset: 0x000CBADC
		private static void BuildComplexType_Block(XsdBuilder builder, string value)
		{
			builder.complexType.Block = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "block");
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000CD8F5 File Offset: 0x000CBAF5
		private static void BuildComplexType_Final(XsdBuilder builder, string value)
		{
			builder.complexType.Final = (XmlSchemaDerivationMethod)builder.ParseBlockFinalEnum(value, "final");
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000CD90E File Offset: 0x000CBB0E
		private static void BuildComplexType_Mixed(XsdBuilder builder, string value)
		{
			builder.complexType.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000CD927 File Offset: 0x000CBB27
		private static void BuildComplexType_Name(XsdBuilder builder, string value)
		{
			builder.complexType.Name = value;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000CD938 File Offset: 0x000CBB38
		private static void InitComplexContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "complexContent");
			}
			builder.xso = (builder.complexContent = new XmlSchemaComplexContent());
			builder.complexType.ContentModel = builder.complexContent;
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000CD9B3 File Offset: 0x000CBBB3
		private static void BuildComplexContent_Mixed(XsdBuilder builder, string value)
		{
			builder.complexContent.IsMixed = builder.ParseBoolean(value, "mixed");
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000CD9CC File Offset: 0x000CBBCC
		private static void InitComplexContentExtension(XsdBuilder builder, string value)
		{
			if (builder.complexContent.Content != null)
			{
				builder.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "extension");
			}
			builder.xso = (builder.complexContentExtension = new XmlSchemaComplexContentExtension());
			builder.complexContent.Content = builder.complexContentExtension;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000CDA1B File Offset: 0x000CBC1B
		private static void BuildComplexContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.complexContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000CDA34 File Offset: 0x000CBC34
		private static void InitComplexContentRestriction(XsdBuilder builder, string value)
		{
			builder.xso = (builder.complexContentRestriction = new XmlSchemaComplexContentRestriction());
			builder.complexContent.Content = builder.complexContentRestriction;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000CDA66 File Offset: 0x000CBC66
		private static void BuildComplexContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.complexContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000CDA80 File Offset: 0x000CBC80
		private static void InitSimpleContent(XsdBuilder builder, string value)
		{
			if (builder.complexType.ContentModel != null || builder.complexType.Particle != null || builder.complexType.Attributes.Count != 0 || builder.complexType.AnyAttribute != null)
			{
				builder.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "simpleContent");
			}
			builder.xso = (builder.simpleContent = new XmlSchemaSimpleContent());
			builder.complexType.ContentModel = builder.simpleContent;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000CDAFC File Offset: 0x000CBCFC
		private static void InitSimpleContentExtension(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("The '{0}' element already exists in the content model.", "extension");
			}
			builder.xso = (builder.simpleContentExtension = new XmlSchemaSimpleContentExtension());
			builder.simpleContent.Content = builder.simpleContentExtension;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000CDB4B File Offset: 0x000CBD4B
		private static void BuildSimpleContentExtension_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentExtension.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000CDB64 File Offset: 0x000CBD64
		private static void InitSimpleContentRestriction(XsdBuilder builder, string value)
		{
			if (builder.simpleContent.Content != null)
			{
				builder.SendValidationEvent("The '{0}' element already exists in the content model.", "restriction");
			}
			builder.xso = (builder.simpleContentRestriction = new XmlSchemaSimpleContentRestriction());
			builder.simpleContent.Content = builder.simpleContentRestriction;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000CDBB3 File Offset: 0x000CBDB3
		private static void BuildSimpleContentRestriction_Base(XsdBuilder builder, string value)
		{
			builder.simpleContentRestriction.BaseTypeName = builder.ParseQName(value, "base");
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000CDBCC File Offset: 0x000CBDCC
		private static void InitAttributeGroup(XsdBuilder builder, string value)
		{
			builder.canIncludeImport = false;
			builder.xso = (builder.attributeGroup = new XmlSchemaAttributeGroup());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.attributeGroup);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.attributeGroup);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000CDC35 File Offset: 0x000CBE35
		private static void BuildAttributeGroup_Name(XsdBuilder builder, string value)
		{
			builder.attributeGroup.Name = value;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000CDC44 File Offset: 0x000CBE44
		private static void InitAttributeGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.attributeGroupRef = new XmlSchemaAttributeGroupRef());
			builder.AddAttribute(builder.attributeGroupRef);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000CDC71 File Offset: 0x000CBE71
		private static void BuildAttributeGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.attributeGroupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000CDC8C File Offset: 0x000CBE8C
		private static void InitAnyAttribute(XsdBuilder builder, string value)
		{
			builder.xso = (builder.anyAttribute = new XmlSchemaAnyAttribute());
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (builder.complexType.ContentModel != null)
					{
						builder.SendValidationEvent("'{0}' and content model are mutually exclusive.", "anyAttribute");
					}
					if (builder.complexType.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexType.AnyAttribute = builder.anyAttribute;
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (builder.complexContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (builder.complexContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.complexContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (builder.simpleContentExtension.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.simpleContentExtension.AnyAttribute = builder.anyAttribute;
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (builder.simpleContentRestriction.AnyAttribute != null)
					{
						builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
					}
					builder.simpleContentRestriction.AnyAttribute = builder.anyAttribute;
					return;
				default:
					return;
				}
			}
			else
			{
				if (builder.attributeGroup.AnyAttribute != null)
				{
					builder.SendValidationEvent("The '{0}' element already exists in the content model.", "anyAttribute");
				}
				builder.attributeGroup.AnyAttribute = builder.anyAttribute;
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000CDE15 File Offset: 0x000CC015
		private static void BuildAnyAttribute_Namespace(XsdBuilder builder, string value)
		{
			builder.anyAttribute.Namespace = value;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000CDE23 File Offset: 0x000CC023
		private static void BuildAnyAttribute_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyAttribute.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000CDE44 File Offset: 0x000CC044
		private static void InitGroup(XsdBuilder builder, string value)
		{
			builder.xso = (builder.group = new XmlSchemaGroup());
			builder.canIncludeImport = false;
			SchemaNames.Token parentElement = builder.ParentElement;
			if (parentElement == SchemaNames.Token.XsdSchema)
			{
				builder.schema.Items.Add(builder.group);
				return;
			}
			if (parentElement != SchemaNames.Token.XsdRedefine)
			{
				return;
			}
			builder.redefine.Items.Add(builder.group);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000CDEAD File Offset: 0x000CC0AD
		private static void BuildGroup_Name(XsdBuilder builder, string value)
		{
			builder.group.Name = value;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000CDEBC File Offset: 0x000CC0BC
		private static void InitGroupRef(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.groupRef = new XmlSchemaGroupRef()));
			builder.AddParticle(builder.groupRef);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000CDEF2 File Offset: 0x000CC0F2
		private static void BuildParticle_MaxOccurs(XsdBuilder builder, string value)
		{
			builder.SetMaxOccurs(builder.particle, value);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000CDF01 File Offset: 0x000CC101
		private static void BuildParticle_MinOccurs(XsdBuilder builder, string value)
		{
			builder.SetMinOccurs(builder.particle, value);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000CDF10 File Offset: 0x000CC110
		private static void BuildGroupRef_Ref(XsdBuilder builder, string value)
		{
			builder.groupRef.RefName = builder.ParseQName(value, "ref");
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000CDF2C File Offset: 0x000CC12C
		private static void InitAll(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.all = new XmlSchemaAll()));
			builder.AddParticle(builder.all);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000CDF64 File Offset: 0x000CC164
		private static void InitChoice(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.choice = new XmlSchemaChoice()));
			builder.AddParticle(builder.choice);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000CDF9C File Offset: 0x000CC19C
		private static void InitSequence(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.sequence = new XmlSchemaSequence()));
			builder.AddParticle(builder.sequence);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000CDFD4 File Offset: 0x000CC1D4
		private static void InitAny(XsdBuilder builder, string value)
		{
			builder.xso = (builder.particle = (builder.anyElement = new XmlSchemaAny()));
			builder.AddParticle(builder.anyElement);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000CE00A File Offset: 0x000CC20A
		private static void BuildAny_Namespace(XsdBuilder builder, string value)
		{
			builder.anyElement.Namespace = value;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000CE018 File Offset: 0x000CC218
		private static void BuildAny_ProcessContents(XsdBuilder builder, string value)
		{
			builder.anyElement.ProcessContents = (XmlSchemaContentProcessing)builder.ParseEnum(value, "processContents", XsdBuilder.ProcessContentsStringValues);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000CE038 File Offset: 0x000CC238
		private static void InitNotation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.notation = new XmlSchemaNotation());
			builder.canIncludeImport = false;
			builder.schema.Items.Add(builder.notation);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000CE077 File Offset: 0x000CC277
		private static void BuildNotation_Name(XsdBuilder builder, string value)
		{
			builder.notation.Name = value;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000CE085 File Offset: 0x000CC285
		private static void BuildNotation_Public(XsdBuilder builder, string value)
		{
			builder.notation.Public = value;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000CE093 File Offset: 0x000CC293
		private static void BuildNotation_System(XsdBuilder builder, string value)
		{
			builder.notation.System = value;
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000CE0A4 File Offset: 0x000CC2A4
		private static void InitFacet(XsdBuilder builder, string value)
		{
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdMinExclusive:
				builder.facet = new XmlSchemaMinExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMinInclusive:
				builder.facet = new XmlSchemaMinInclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxExclusive:
				builder.facet = new XmlSchemaMaxExclusiveFacet();
				break;
			case SchemaNames.Token.XsdMaxInclusive:
				builder.facet = new XmlSchemaMaxInclusiveFacet();
				break;
			case SchemaNames.Token.XsdTotalDigits:
				builder.facet = new XmlSchemaTotalDigitsFacet();
				break;
			case SchemaNames.Token.XsdFractionDigits:
				builder.facet = new XmlSchemaFractionDigitsFacet();
				break;
			case SchemaNames.Token.XsdLength:
				builder.facet = new XmlSchemaLengthFacet();
				break;
			case SchemaNames.Token.XsdMinLength:
				builder.facet = new XmlSchemaMinLengthFacet();
				break;
			case SchemaNames.Token.XsdMaxLength:
				builder.facet = new XmlSchemaMaxLengthFacet();
				break;
			case SchemaNames.Token.XsdEnumeration:
				builder.facet = new XmlSchemaEnumerationFacet();
				break;
			case SchemaNames.Token.XsdPattern:
				builder.facet = new XmlSchemaPatternFacet();
				break;
			case SchemaNames.Token.XsdWhitespace:
				builder.facet = new XmlSchemaWhiteSpaceFacet();
				break;
			}
			builder.xso = builder.facet;
			if (SchemaNames.Token.XsdSimpleTypeRestriction == builder.ParentElement)
			{
				builder.simpleTypeRestriction.Facets.Add(builder.facet);
				return;
			}
			if (builder.simpleContentRestriction.Attributes.Count != 0 || builder.simpleContentRestriction.AnyAttribute != null)
			{
				builder.SendValidationEvent("Facet should go before 'attribute', 'attributeGroup', or 'anyAttribute'.", null);
			}
			builder.simpleContentRestriction.Facets.Add(builder.facet);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000CE232 File Offset: 0x000CC432
		private static void BuildFacet_Fixed(XsdBuilder builder, string value)
		{
			builder.facet.IsFixed = builder.ParseBoolean(value, "fixed");
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000CE24B File Offset: 0x000CC44B
		private static void BuildFacet_Value(XsdBuilder builder, string value)
		{
			builder.facet.Value = value;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000CE25C File Offset: 0x000CC45C
		private static void InitIdentityConstraint(XsdBuilder builder, string value)
		{
			if (!builder.element.RefName.IsEmpty)
			{
				builder.SendValidationEvent("When the ref attribute is present, the type attribute and complexType, simpleType, key, keyref, and unique elements cannot be present.", null);
			}
			switch (builder.CurrentElement)
			{
			case SchemaNames.Token.XsdUnique:
				builder.xso = (builder.identityConstraint = new XmlSchemaUnique());
				break;
			case SchemaNames.Token.XsdKey:
				builder.xso = (builder.identityConstraint = new XmlSchemaKey());
				break;
			case SchemaNames.Token.XsdKeyref:
				builder.xso = (builder.identityConstraint = new XmlSchemaKeyref());
				break;
			}
			builder.element.Constraints.Add(builder.identityConstraint);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000CE2FC File Offset: 0x000CC4FC
		private static void BuildIdentityConstraint_Name(XsdBuilder builder, string value)
		{
			builder.identityConstraint.Name = value;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000CE30A File Offset: 0x000CC50A
		private static void BuildIdentityConstraint_Refer(XsdBuilder builder, string value)
		{
			if (builder.identityConstraint is XmlSchemaKeyref)
			{
				((XmlSchemaKeyref)builder.identityConstraint).Refer = builder.ParseQName(value, "refer");
				return;
			}
			builder.SendValidationEvent("The '{0}' attribute is not supported in this context.", "refer");
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000CE348 File Offset: 0x000CC548
		private static void InitSelector(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.identityConstraint.Selector = builder.xpath;
				return;
			}
			builder.SendValidationEvent("Selector cannot appear twice in one identity constraint.", builder.identityConstraint.Name);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000CE39E File Offset: 0x000CC59E
		private static void BuildSelector_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000CE3AC File Offset: 0x000CC5AC
		private static void InitField(XsdBuilder builder, string value)
		{
			builder.xso = (builder.xpath = new XmlSchemaXPath());
			if (builder.identityConstraint.Selector == null)
			{
				builder.SendValidationEvent("Cannot define fields before selector.", builder.identityConstraint.Name);
			}
			builder.identityConstraint.Fields.Add(builder.xpath);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000CE39E File Offset: 0x000CC59E
		private static void BuildField_XPath(XsdBuilder builder, string value)
		{
			builder.xpath.XPath = value;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000CE408 File Offset: 0x000CC608
		private static void InitAnnotation(XsdBuilder builder, string value)
		{
			if (builder.hasChild && builder.ParentElement != SchemaNames.Token.XsdSchema && builder.ParentElement != SchemaNames.Token.XsdRedefine)
			{
				builder.SendValidationEvent("The 'annotation' element cannot appear at this location.", null);
			}
			builder.xso = (builder.annotation = new XmlSchemaAnnotation());
			builder.ParentContainer.AddAnnotation(builder.annotation);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000CE464 File Offset: 0x000CC664
		private static void InitAppinfo(XsdBuilder builder, string value)
		{
			builder.xso = (builder.appInfo = new XmlSchemaAppInfo());
			builder.annotation.Items.Add(builder.appInfo);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000CE4A8 File Offset: 0x000CC6A8
		private static void BuildAppinfo_Source(XsdBuilder builder, string value)
		{
			builder.appInfo.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000CE4BB File Offset: 0x000CC6BB
		private static void EndAppinfo(XsdBuilder builder)
		{
			builder.appInfo.Markup = builder.markup;
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000CE4D0 File Offset: 0x000CC6D0
		private static void InitDocumentation(XsdBuilder builder, string value)
		{
			builder.xso = (builder.documentation = new XmlSchemaDocumentation());
			builder.annotation.Items.Add(builder.documentation);
			builder.markup = new XmlNode[0];
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000CE514 File Offset: 0x000CC714
		private static void BuildDocumentation_Source(XsdBuilder builder, string value)
		{
			builder.documentation.Source = XsdBuilder.ParseUriReference(value);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000CE528 File Offset: 0x000CC728
		private static void BuildDocumentation_XmlLang(XsdBuilder builder, string value)
		{
			try
			{
				builder.documentation.Language = value;
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(builder.reader.BaseURI, builder.positionInfo.LineNumber, builder.positionInfo.LinePosition);
				builder.SendValidationEvent(ex);
			}
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000CE584 File Offset: 0x000CC784
		private static void EndDocumentation(XsdBuilder builder)
		{
			builder.documentation.Markup = builder.markup;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000CE598 File Offset: 0x000CC798
		private void AddAttribute(XmlSchemaObject value)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement != SchemaNames.Token.xsdAttributeGroup)
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null)
					{
						this.SendValidationEvent("'{0}' and content model are mutually exclusive.", "attribute");
					}
					if (this.complexType.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexType.Attributes.Add(value);
					return;
				}
				switch (parentElement)
				{
				case SchemaNames.Token.XsdComplexContentExtension:
					if (this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdComplexContentRestriction:
					if (this.complexContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.complexContentRestriction.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContent:
					break;
				case SchemaNames.Token.XsdSimpleContentExtension:
					if (this.simpleContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.simpleContentExtension.Attributes.Add(value);
					return;
				case SchemaNames.Token.XsdSimpleContentRestriction:
					if (this.simpleContentRestriction.AnyAttribute != null)
					{
						this.SendValidationEvent("'anyAttribute' must be the last child.", null);
					}
					this.simpleContentRestriction.Attributes.Add(value);
					return;
				default:
					return;
				}
			}
			else
			{
				if (this.attributeGroup.AnyAttribute != null)
				{
					this.SendValidationEvent("'anyAttribute' must be the last child.", null);
				}
				this.attributeGroup.Attributes.Add(value);
			}
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000CE6FC File Offset: 0x000CC8FC
		private void AddParticle(XmlSchemaParticle particle)
		{
			SchemaNames.Token parentElement = this.ParentElement;
			if (parentElement <= SchemaNames.Token.XsdSequence)
			{
				if (parentElement == SchemaNames.Token.XsdGroup)
				{
					if (this.group.Particle != null)
					{
						this.SendValidationEvent("The content model can only have one of the following; 'all', 'choice', or 'sequence'.", "particle");
					}
					this.group.Particle = (XmlSchemaGroupBase)particle;
					return;
				}
				if (parentElement - SchemaNames.Token.XsdChoice > 1)
				{
					return;
				}
				((XmlSchemaGroupBase)this.ParentContainer).Items.Add(particle);
				return;
			}
			else
			{
				if (parentElement == SchemaNames.Token.XsdComplexType)
				{
					if (this.complexType.ContentModel != null || this.complexType.Attributes.Count != 0 || this.complexType.AnyAttribute != null || this.complexType.Particle != null)
					{
						this.SendValidationEvent("The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "complexType");
					}
					this.complexType.Particle = particle;
					return;
				}
				if (parentElement == SchemaNames.Token.XsdComplexContentExtension)
				{
					if (this.complexContentExtension.Particle != null || this.complexContentExtension.Attributes.Count != 0 || this.complexContentExtension.AnyAttribute != null)
					{
						this.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "ComplexContentExtension");
					}
					this.complexContentExtension.Particle = particle;
					return;
				}
				if (parentElement != SchemaNames.Token.XsdComplexContentRestriction)
				{
					return;
				}
				if (this.complexContentRestriction.Particle != null || this.complexContentRestriction.Attributes.Count != 0 || this.complexContentRestriction.AnyAttribute != null)
				{
					this.SendValidationEvent("Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.", "ComplexContentExtension");
				}
				this.complexContentRestriction.Particle = particle;
				return;
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000CE86C File Offset: 0x000CCA6C
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this.currentEntry.NextStates != null)
			{
				for (int i = 0; i < this.currentEntry.NextStates.Length; i++)
				{
					int num = (int)this.currentEntry.NextStates[i];
					if (this.schemaNames.TokenToQName[(int)XsdBuilder.SchemaEntries[num].Name].Equals(qname))
					{
						this.nextEntry = XsdBuilder.SchemaEntries[num];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000CE8DC File Offset: 0x000CCADC
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			return this.CurrentElement == SchemaNames.Token.XsdDocumentation || this.CurrentElement == SchemaNames.Token.XsdAppInfo;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000CE8F4 File Offset: 0x000CCAF4
		private void SetMinOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MinOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", null);
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000CE92C File Offset: 0x000CCB2C
		private void SetMaxOccurs(XmlSchemaParticle particle, string value)
		{
			try
			{
				particle.MaxOccursString = value;
			}
			catch (Exception)
			{
				this.SendValidationEvent("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", null);
			}
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000CE964 File Offset: 0x000CCB64
		private bool ParseBoolean(string value, string attributeName)
		{
			bool flag;
			try
			{
				flag = XmlConvert.ToBoolean(value);
			}
			catch (Exception)
			{
				this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000CE9A0 File Offset: 0x000CCBA0
		private int ParseEnum(string value, string attributeName, string[] values)
		{
			string text = value.Trim();
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] == text)
				{
					return i + 1;
				}
			}
			this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, text, null);
			return 0;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000CE9E0 File Offset: 0x000CCBE0
		private XmlQualifiedName ParseQName(string value, string attributeName)
		{
			XmlQualifiedName xmlQualifiedName;
			try
			{
				value = XmlComplianceUtil.NonCDataNormalize(value);
				string text;
				xmlQualifiedName = XmlQualifiedName.Parse(value, this.namespaceManager, out text);
			}
			catch (Exception)
			{
				this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
				xmlQualifiedName = XmlQualifiedName.Empty;
			}
			return xmlQualifiedName;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000CEA30 File Offset: 0x000CCC30
		private int ParseBlockFinalEnum(string value, string attributeName)
		{
			int num = 0;
			string[] array = XmlConvert.SplitString(value);
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < XsdBuilder.DerivationMethodStrings.Length)
				{
					if (array[i] == XsdBuilder.DerivationMethodStrings[j])
					{
						if ((num & XsdBuilder.DerivationMethodValues[j]) != 0 && (num & XsdBuilder.DerivationMethodValues[j]) != XsdBuilder.DerivationMethodValues[j])
						{
							this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
							return 0;
						}
						num |= XsdBuilder.DerivationMethodValues[j];
						flag = true;
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
					return 0;
				}
				if (num == 255 && value.Length > 4)
				{
					this.SendValidationEvent("'{1}' is an invalid value for the '{0}' attribute.", attributeName, value, null);
					return 0;
				}
			}
			return num;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00035F33 File Offset: 0x00034133
		private static string ParseUriReference(string s)
		{
			return s;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000CEAF8 File Offset: 0x000CCCF8
		private void SendValidationEvent(string code, string arg0, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[] { arg0, arg1, arg2 }, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000CEB45 File Offset: 0x000CCD45
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000CEB75 File Offset: 0x000CCD75
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000CEBA8 File Offset: 0x000CCDA8
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			XmlSchema xmlSchema = this.schema;
			int errorCount = xmlSchema.ErrorCount;
			xmlSchema.ErrorCount = errorCount + 1;
			e.SetSchemaObject(this.schema);
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000CEBF7 File Offset: 0x000CCDF7
		private void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000CEC04 File Offset: 0x000CCE04
		private void RecordPosition()
		{
			this.xso.SourceUri = this.reader.BaseURI;
			this.xso.LineNumber = this.positionInfo.LineNumber;
			this.xso.LinePosition = this.positionInfo.LinePosition;
			if (this.xso != this.schema)
			{
				this.xso.Parent = this.ParentContainer;
			}
		}

		// Token: 0x040010B8 RID: 4280
		private static readonly XsdBuilder.State[] SchemaElement = new XsdBuilder.State[] { XsdBuilder.State.Schema };

		// Token: 0x040010B9 RID: 4281
		private static readonly XsdBuilder.State[] SchemaSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Include,
			XsdBuilder.State.Import,
			XsdBuilder.State.Redefine,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Element,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.Group,
			XsdBuilder.State.Notation
		};

		// Token: 0x040010BA RID: 4282
		private static readonly XsdBuilder.State[] AttributeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x040010BB RID: 4283
		private static readonly XsdBuilder.State[] ElementSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Unique,
			XsdBuilder.State.Key,
			XsdBuilder.State.KeyRef
		};

		// Token: 0x040010BC RID: 4284
		private static readonly XsdBuilder.State[] ComplexTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContent,
			XsdBuilder.State.ComplexContent,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x040010BD RID: 4285
		private static readonly XsdBuilder.State[] SimpleContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleContentRestriction,
			XsdBuilder.State.SimpleContentExtension
		};

		// Token: 0x040010BE RID: 4286
		private static readonly XsdBuilder.State[] SimpleContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x040010BF RID: 4287
		private static readonly XsdBuilder.State[] SimpleContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x040010C0 RID: 4288
		private static readonly XsdBuilder.State[] ComplexContentSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.ComplexContentRestriction,
			XsdBuilder.State.ComplexContentExtension
		};

		// Token: 0x040010C1 RID: 4289
		private static readonly XsdBuilder.State[] ComplexContentExtensionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x040010C2 RID: 4290
		private static readonly XsdBuilder.State[] ComplexContentRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x040010C3 RID: 4291
		private static readonly XsdBuilder.State[] SimpleTypeSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleTypeList,
			XsdBuilder.State.SimpleTypeRestriction,
			XsdBuilder.State.SimpleTypeUnion
		};

		// Token: 0x040010C4 RID: 4292
		private static readonly XsdBuilder.State[] SimpleTypeRestrictionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType,
			XsdBuilder.State.Enumeration,
			XsdBuilder.State.Length,
			XsdBuilder.State.MaxExclusive,
			XsdBuilder.State.MaxInclusive,
			XsdBuilder.State.MaxLength,
			XsdBuilder.State.MinExclusive,
			XsdBuilder.State.MinInclusive,
			XsdBuilder.State.MinLength,
			XsdBuilder.State.Pattern,
			XsdBuilder.State.TotalDigits,
			XsdBuilder.State.FractionDigits,
			XsdBuilder.State.WhiteSpace
		};

		// Token: 0x040010C5 RID: 4293
		private static readonly XsdBuilder.State[] SimpleTypeListSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x040010C6 RID: 4294
		private static readonly XsdBuilder.State[] SimpleTypeUnionSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x040010C7 RID: 4295
		private static readonly XsdBuilder.State[] RedefineSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.AttributeGroup,
			XsdBuilder.State.ComplexType,
			XsdBuilder.State.Group,
			XsdBuilder.State.SimpleType
		};

		// Token: 0x040010C8 RID: 4296
		private static readonly XsdBuilder.State[] AttributeGroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Attribute,
			XsdBuilder.State.AttributeGroupRef,
			XsdBuilder.State.AnyAttribute
		};

		// Token: 0x040010C9 RID: 4297
		private static readonly XsdBuilder.State[] GroupSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.All,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence
		};

		// Token: 0x040010CA RID: 4298
		private static readonly XsdBuilder.State[] AllSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element
		};

		// Token: 0x040010CB RID: 4299
		private static readonly XsdBuilder.State[] ChoiceSequenceSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Element,
			XsdBuilder.State.GroupRef,
			XsdBuilder.State.Choice,
			XsdBuilder.State.Sequence,
			XsdBuilder.State.Any
		};

		// Token: 0x040010CC RID: 4300
		private static readonly XsdBuilder.State[] IdentityConstraintSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.Annotation,
			XsdBuilder.State.Selector,
			XsdBuilder.State.Field
		};

		// Token: 0x040010CD RID: 4301
		private static readonly XsdBuilder.State[] AnnotationSubelements = new XsdBuilder.State[]
		{
			XsdBuilder.State.AppInfo,
			XsdBuilder.State.Documentation
		};

		// Token: 0x040010CE RID: 4302
		private static readonly XsdBuilder.State[] AnnotatedSubelements = new XsdBuilder.State[] { XsdBuilder.State.Annotation };

		// Token: 0x040010CF RID: 4303
		private static readonly XsdBuilder.XsdAttributeEntry[] SchemaAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAttributeFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_AttributeFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaElementFormDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_ElementFormDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaTargetNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_TargetNamespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaVersion, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_Version)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinalDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_FinalDefault)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlockDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSchema_BlockDefault))
		};

		// Token: 0x040010D0 RID: 4304
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Type)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaUse, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttribute_Use))
		};

		// Token: 0x040010D1 RID: 4305
		private static readonly XsdBuilder.XsdAttributeEntry[] ElementAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaDefault, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Default)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaForm, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Form)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNillable, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Nillable)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Ref)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSubstitutionGroup, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_SubstitutionGroup)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildElement_Type))
		};

		// Token: 0x040010D2 RID: 4306
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaAbstract, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Abstract)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBlock, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Block)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Mixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexType_Name))
		};

		// Token: 0x040010D3 RID: 4307
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x040010D4 RID: 4308
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x040010D5 RID: 4309
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x040010D6 RID: 4310
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContent_Mixed))
		};

		// Token: 0x040010D7 RID: 4311
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentExtensionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentExtension_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x040010D8 RID: 4312
		private static readonly XsdBuilder.XsdAttributeEntry[] ComplexContentRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildComplexContentRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x040010D9 RID: 4313
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFinal, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Final)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleType_Name))
		};

		// Token: 0x040010DA RID: 4314
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeRestrictionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaBase, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeRestriction_Base)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x040010DB RID: 4315
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeUnionAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMemberTypes, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeUnion_MemberTypes))
		};

		// Token: 0x040010DC RID: 4316
		private static readonly XsdBuilder.XsdAttributeEntry[] SimpleTypeListAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaItemType, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSimpleTypeList_ItemType))
		};

		// Token: 0x040010DD RID: 4317
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroup_Name))
		};

		// Token: 0x040010DE RID: 4318
		private static readonly XsdBuilder.XsdAttributeEntry[] AttributeGroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAttributeGroupRef_Ref))
		};

		// Token: 0x040010DF RID: 4319
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroup_Name))
		};

		// Token: 0x040010E0 RID: 4320
		private static readonly XsdBuilder.XsdAttributeEntry[] GroupRefAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRef, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildGroupRef_Ref))
		};

		// Token: 0x040010E1 RID: 4321
		private static readonly XsdBuilder.XsdAttributeEntry[] ParticleAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs))
		};

		// Token: 0x040010E2 RID: 4322
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MaxOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaMinOccurs, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildParticle_MinOccurs)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAny_ProcessContents))
		};

		// Token: 0x040010E3 RID: 4323
		private static readonly XsdBuilder.XsdAttributeEntry[] IdentityConstraintAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaRefer, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildIdentityConstraint_Refer))
		};

		// Token: 0x040010E4 RID: 4324
		private static readonly XsdBuilder.XsdAttributeEntry[] SelectorAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildSelector_XPath))
		};

		// Token: 0x040010E5 RID: 4325
		private static readonly XsdBuilder.XsdAttributeEntry[] FieldAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaXPath, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildField_XPath))
		};

		// Token: 0x040010E6 RID: 4326
		private static readonly XsdBuilder.XsdAttributeEntry[] NotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaName, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Name)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaPublic, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_Public)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSystem, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildNotation_System))
		};

		// Token: 0x040010E7 RID: 4327
		private static readonly XsdBuilder.XsdAttributeEntry[] IncludeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildInclude_SchemaLocation))
		};

		// Token: 0x040010E8 RID: 4328
		private static readonly XsdBuilder.XsdAttributeEntry[] ImportAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildImport_SchemaLocation))
		};

		// Token: 0x040010E9 RID: 4329
		private static readonly XsdBuilder.XsdAttributeEntry[] FacetAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaFixed, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Fixed)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaValue, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildFacet_Value))
		};

		// Token: 0x040010EA RID: 4330
		private static readonly XsdBuilder.XsdAttributeEntry[] AnyAttributeAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaNamespace, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_Namespace)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaProcessContents, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnyAttribute_ProcessContents))
		};

		// Token: 0x040010EB RID: 4331
		private static readonly XsdBuilder.XsdAttributeEntry[] DocumentationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_Source)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.XmlLang, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildDocumentation_XmlLang))
		};

		// Token: 0x040010EC RID: 4332
		private static readonly XsdBuilder.XsdAttributeEntry[] AppinfoAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSource, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAppinfo_Source))
		};

		// Token: 0x040010ED RID: 4333
		private static readonly XsdBuilder.XsdAttributeEntry[] RedefineAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id)),
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaSchemaLocation, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildRedefine_SchemaLocation))
		};

		// Token: 0x040010EE RID: 4334
		private static readonly XsdBuilder.XsdAttributeEntry[] AnnotationAttributes = new XsdBuilder.XsdAttributeEntry[]
		{
			new XsdBuilder.XsdAttributeEntry(SchemaNames.Token.SchemaId, new XsdBuilder.XsdBuildFunction(XsdBuilder.BuildAnnotated_Id))
		};

		// Token: 0x040010EF RID: 4335
		private static readonly XsdBuilder.XsdEntry[] SchemaEntries = new XsdBuilder.XsdEntry[]
		{
			new XsdBuilder.XsdEntry(SchemaNames.Token.Empty, XsdBuilder.State.Root, XsdBuilder.SchemaElement, null, null, null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSchema, XsdBuilder.State.Schema, XsdBuilder.SchemaSubelements, XsdBuilder.SchemaAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSchema), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnnotation, XsdBuilder.State.Annotation, XsdBuilder.AnnotationSubelements, XsdBuilder.AnnotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnnotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdInclude, XsdBuilder.State.Include, XsdBuilder.AnnotatedSubelements, XsdBuilder.IncludeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitInclude), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdImport, XsdBuilder.State.Import, XsdBuilder.AnnotatedSubelements, XsdBuilder.ImportAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitImport), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdElement, XsdBuilder.State.Element, XsdBuilder.ElementSubelements, XsdBuilder.ElementAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitElement), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAttribute, XsdBuilder.State.Attribute, XsdBuilder.AttributeSubelements, XsdBuilder.AttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroup, XsdBuilder.AttributeGroupSubelements, XsdBuilder.AttributeGroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.xsdAttributeGroup, XsdBuilder.State.AttributeGroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.AttributeGroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAttributeGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAnyAttribute, XsdBuilder.State.AnyAttribute, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAnyAttribute), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.Group, XsdBuilder.GroupSubelements, XsdBuilder.GroupAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroup), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdGroup, XsdBuilder.State.GroupRef, XsdBuilder.AnnotatedSubelements, XsdBuilder.GroupRefAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitGroupRef), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAll, XsdBuilder.State.All, XsdBuilder.AllSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAll), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdChoice, XsdBuilder.State.Choice, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitChoice), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSequence, XsdBuilder.State.Sequence, XsdBuilder.ChoiceSequenceSubelements, XsdBuilder.ParticleAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSequence), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAny, XsdBuilder.State.Any, XsdBuilder.AnnotatedSubelements, XsdBuilder.AnyAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAny), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdNotation, XsdBuilder.State.Notation, XsdBuilder.AnnotatedSubelements, XsdBuilder.NotationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitNotation), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleType, XsdBuilder.State.SimpleType, XsdBuilder.SimpleTypeSubelements, XsdBuilder.SimpleTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexType, XsdBuilder.State.ComplexType, XsdBuilder.ComplexTypeSubelements, XsdBuilder.ComplexTypeAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexType), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContent, XsdBuilder.State.ComplexContent, XsdBuilder.ComplexContentSubelements, XsdBuilder.ComplexContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentRestriction, XsdBuilder.State.ComplexContentRestriction, XsdBuilder.ComplexContentRestrictionSubelements, XsdBuilder.ComplexContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdComplexContentExtension, XsdBuilder.State.ComplexContentExtension, XsdBuilder.ComplexContentExtensionSubelements, XsdBuilder.ComplexContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitComplexContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContent, XsdBuilder.State.SimpleContent, XsdBuilder.SimpleContentSubelements, XsdBuilder.SimpleContentAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContent), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentExtension, XsdBuilder.State.SimpleContentExtension, XsdBuilder.SimpleContentExtensionSubelements, XsdBuilder.SimpleContentExtensionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentExtension), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleContentRestriction, XsdBuilder.State.SimpleContentRestriction, XsdBuilder.SimpleContentRestrictionSubelements, XsdBuilder.SimpleContentRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleContentRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeUnion, XsdBuilder.State.SimpleTypeUnion, XsdBuilder.SimpleTypeUnionSubelements, XsdBuilder.SimpleTypeUnionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeUnion), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeList, XsdBuilder.State.SimpleTypeList, XsdBuilder.SimpleTypeListSubelements, XsdBuilder.SimpleTypeListAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeList), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSimpleTypeRestriction, XsdBuilder.State.SimpleTypeRestriction, XsdBuilder.SimpleTypeRestrictionSubelements, XsdBuilder.SimpleTypeRestrictionAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSimpleTypeRestriction), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdUnique, XsdBuilder.State.Unique, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKey, XsdBuilder.State.Key, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdKeyref, XsdBuilder.State.KeyRef, XsdBuilder.IdentityConstraintSubelements, XsdBuilder.IdentityConstraintAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitIdentityConstraint), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdSelector, XsdBuilder.State.Selector, XsdBuilder.AnnotatedSubelements, XsdBuilder.SelectorAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitSelector), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdField, XsdBuilder.State.Field, XsdBuilder.AnnotatedSubelements, XsdBuilder.FieldAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitField), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinExclusive, XsdBuilder.State.MinExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinInclusive, XsdBuilder.State.MinInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxExclusive, XsdBuilder.State.MaxExclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxInclusive, XsdBuilder.State.MaxInclusive, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdTotalDigits, XsdBuilder.State.TotalDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdFractionDigits, XsdBuilder.State.FractionDigits, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdLength, XsdBuilder.State.Length, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMinLength, XsdBuilder.State.MinLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdMaxLength, XsdBuilder.State.MaxLength, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdEnumeration, XsdBuilder.State.Enumeration, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdPattern, XsdBuilder.State.Pattern, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdWhitespace, XsdBuilder.State.WhiteSpace, XsdBuilder.AnnotatedSubelements, XsdBuilder.FacetAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitFacet), null, true),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdAppInfo, XsdBuilder.State.AppInfo, null, XsdBuilder.AppinfoAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitAppinfo), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndAppinfo), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdDocumentation, XsdBuilder.State.Documentation, null, XsdBuilder.DocumentationAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitDocumentation), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndDocumentation), false),
			new XsdBuilder.XsdEntry(SchemaNames.Token.XsdRedefine, XsdBuilder.State.Redefine, XsdBuilder.RedefineSubelements, XsdBuilder.RedefineAttributes, new XsdBuilder.XsdInitFunction(XsdBuilder.InitRedefine), new XsdBuilder.XsdEndChildFunction(XsdBuilder.EndRedefine), true)
		};

		// Token: 0x040010F0 RID: 4336
		private static readonly int[] DerivationMethodValues = new int[] { 1, 2, 4, 8, 16, 255 };

		// Token: 0x040010F1 RID: 4337
		private static readonly string[] DerivationMethodStrings = new string[] { "substitution", "extension", "restriction", "list", "union", "#all" };

		// Token: 0x040010F2 RID: 4338
		private static readonly string[] FormStringValues = new string[] { "qualified", "unqualified" };

		// Token: 0x040010F3 RID: 4339
		private static readonly string[] UseStringValues = new string[] { "optional", "prohibited", "required" };

		// Token: 0x040010F4 RID: 4340
		private static readonly string[] ProcessContentsStringValues = new string[] { "skip", "lax", "strict" };

		// Token: 0x040010F5 RID: 4341
		private XmlReader reader;

		// Token: 0x040010F6 RID: 4342
		private PositionInfo positionInfo;

		// Token: 0x040010F7 RID: 4343
		private XsdBuilder.XsdEntry currentEntry;

		// Token: 0x040010F8 RID: 4344
		private XsdBuilder.XsdEntry nextEntry;

		// Token: 0x040010F9 RID: 4345
		private bool hasChild;

		// Token: 0x040010FA RID: 4346
		private HWStack stateHistory = new HWStack(10);

		// Token: 0x040010FB RID: 4347
		private Stack containerStack = new Stack();

		// Token: 0x040010FC RID: 4348
		private XmlNameTable nameTable;

		// Token: 0x040010FD RID: 4349
		private SchemaNames schemaNames;

		// Token: 0x040010FE RID: 4350
		private XmlNamespaceManager namespaceManager;

		// Token: 0x040010FF RID: 4351
		private bool canIncludeImport;

		// Token: 0x04001100 RID: 4352
		private XmlSchema schema;

		// Token: 0x04001101 RID: 4353
		private XmlSchemaObject xso;

		// Token: 0x04001102 RID: 4354
		private XmlSchemaElement element;

		// Token: 0x04001103 RID: 4355
		private XmlSchemaAny anyElement;

		// Token: 0x04001104 RID: 4356
		private XmlSchemaAttribute attribute;

		// Token: 0x04001105 RID: 4357
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001106 RID: 4358
		private XmlSchemaComplexType complexType;

		// Token: 0x04001107 RID: 4359
		private XmlSchemaSimpleType simpleType;

		// Token: 0x04001108 RID: 4360
		private XmlSchemaComplexContent complexContent;

		// Token: 0x04001109 RID: 4361
		private XmlSchemaComplexContentExtension complexContentExtension;

		// Token: 0x0400110A RID: 4362
		private XmlSchemaComplexContentRestriction complexContentRestriction;

		// Token: 0x0400110B RID: 4363
		private XmlSchemaSimpleContent simpleContent;

		// Token: 0x0400110C RID: 4364
		private XmlSchemaSimpleContentExtension simpleContentExtension;

		// Token: 0x0400110D RID: 4365
		private XmlSchemaSimpleContentRestriction simpleContentRestriction;

		// Token: 0x0400110E RID: 4366
		private XmlSchemaSimpleTypeUnion simpleTypeUnion;

		// Token: 0x0400110F RID: 4367
		private XmlSchemaSimpleTypeList simpleTypeList;

		// Token: 0x04001110 RID: 4368
		private XmlSchemaSimpleTypeRestriction simpleTypeRestriction;

		// Token: 0x04001111 RID: 4369
		private XmlSchemaGroup group;

		// Token: 0x04001112 RID: 4370
		private XmlSchemaGroupRef groupRef;

		// Token: 0x04001113 RID: 4371
		private XmlSchemaAll all;

		// Token: 0x04001114 RID: 4372
		private XmlSchemaChoice choice;

		// Token: 0x04001115 RID: 4373
		private XmlSchemaSequence sequence;

		// Token: 0x04001116 RID: 4374
		private XmlSchemaParticle particle;

		// Token: 0x04001117 RID: 4375
		private XmlSchemaAttributeGroup attributeGroup;

		// Token: 0x04001118 RID: 4376
		private XmlSchemaAttributeGroupRef attributeGroupRef;

		// Token: 0x04001119 RID: 4377
		private XmlSchemaNotation notation;

		// Token: 0x0400111A RID: 4378
		private XmlSchemaIdentityConstraint identityConstraint;

		// Token: 0x0400111B RID: 4379
		private XmlSchemaXPath xpath;

		// Token: 0x0400111C RID: 4380
		private XmlSchemaInclude include;

		// Token: 0x0400111D RID: 4381
		private XmlSchemaImport import;

		// Token: 0x0400111E RID: 4382
		private XmlSchemaAnnotation annotation;

		// Token: 0x0400111F RID: 4383
		private XmlSchemaAppInfo appInfo;

		// Token: 0x04001120 RID: 4384
		private XmlSchemaDocumentation documentation;

		// Token: 0x04001121 RID: 4385
		private XmlSchemaFacet facet;

		// Token: 0x04001122 RID: 4386
		private XmlNode[] markup;

		// Token: 0x04001123 RID: 4387
		private XmlSchemaRedefine redefine;

		// Token: 0x04001124 RID: 4388
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001125 RID: 4389
		private ArrayList unhandledAttributes = new ArrayList();

		// Token: 0x04001126 RID: 4390
		private Hashtable namespaces;

		// Token: 0x0200031F RID: 799
		private enum State
		{
			// Token: 0x04001128 RID: 4392
			Root,
			// Token: 0x04001129 RID: 4393
			Schema,
			// Token: 0x0400112A RID: 4394
			Annotation,
			// Token: 0x0400112B RID: 4395
			Include,
			// Token: 0x0400112C RID: 4396
			Import,
			// Token: 0x0400112D RID: 4397
			Element,
			// Token: 0x0400112E RID: 4398
			Attribute,
			// Token: 0x0400112F RID: 4399
			AttributeGroup,
			// Token: 0x04001130 RID: 4400
			AttributeGroupRef,
			// Token: 0x04001131 RID: 4401
			AnyAttribute,
			// Token: 0x04001132 RID: 4402
			Group,
			// Token: 0x04001133 RID: 4403
			GroupRef,
			// Token: 0x04001134 RID: 4404
			All,
			// Token: 0x04001135 RID: 4405
			Choice,
			// Token: 0x04001136 RID: 4406
			Sequence,
			// Token: 0x04001137 RID: 4407
			Any,
			// Token: 0x04001138 RID: 4408
			Notation,
			// Token: 0x04001139 RID: 4409
			SimpleType,
			// Token: 0x0400113A RID: 4410
			ComplexType,
			// Token: 0x0400113B RID: 4411
			ComplexContent,
			// Token: 0x0400113C RID: 4412
			ComplexContentRestriction,
			// Token: 0x0400113D RID: 4413
			ComplexContentExtension,
			// Token: 0x0400113E RID: 4414
			SimpleContent,
			// Token: 0x0400113F RID: 4415
			SimpleContentExtension,
			// Token: 0x04001140 RID: 4416
			SimpleContentRestriction,
			// Token: 0x04001141 RID: 4417
			SimpleTypeUnion,
			// Token: 0x04001142 RID: 4418
			SimpleTypeList,
			// Token: 0x04001143 RID: 4419
			SimpleTypeRestriction,
			// Token: 0x04001144 RID: 4420
			Unique,
			// Token: 0x04001145 RID: 4421
			Key,
			// Token: 0x04001146 RID: 4422
			KeyRef,
			// Token: 0x04001147 RID: 4423
			Selector,
			// Token: 0x04001148 RID: 4424
			Field,
			// Token: 0x04001149 RID: 4425
			MinExclusive,
			// Token: 0x0400114A RID: 4426
			MinInclusive,
			// Token: 0x0400114B RID: 4427
			MaxExclusive,
			// Token: 0x0400114C RID: 4428
			MaxInclusive,
			// Token: 0x0400114D RID: 4429
			TotalDigits,
			// Token: 0x0400114E RID: 4430
			FractionDigits,
			// Token: 0x0400114F RID: 4431
			Length,
			// Token: 0x04001150 RID: 4432
			MinLength,
			// Token: 0x04001151 RID: 4433
			MaxLength,
			// Token: 0x04001152 RID: 4434
			Enumeration,
			// Token: 0x04001153 RID: 4435
			Pattern,
			// Token: 0x04001154 RID: 4436
			WhiteSpace,
			// Token: 0x04001155 RID: 4437
			AppInfo,
			// Token: 0x04001156 RID: 4438
			Documentation,
			// Token: 0x04001157 RID: 4439
			Redefine
		}

		// Token: 0x02000320 RID: 800
		// (Invoke) Token: 0x060024BF RID: 9407
		private delegate void XsdBuildFunction(XsdBuilder builder, string value);

		// Token: 0x02000321 RID: 801
		// (Invoke) Token: 0x060024C1 RID: 9409
		private delegate void XsdInitFunction(XsdBuilder builder, string value);

		// Token: 0x02000322 RID: 802
		// (Invoke) Token: 0x060024C3 RID: 9411
		private delegate void XsdEndChildFunction(XsdBuilder builder);

		// Token: 0x02000323 RID: 803
		private sealed class XsdAttributeEntry
		{
			// Token: 0x060024C4 RID: 9412 RVA: 0x000D000E File Offset: 0x000CE20E
			public XsdAttributeEntry(SchemaNames.Token a, XsdBuilder.XsdBuildFunction build)
			{
				this.Attribute = a;
				this.BuildFunc = build;
			}

			// Token: 0x04001158 RID: 4440
			public SchemaNames.Token Attribute;

			// Token: 0x04001159 RID: 4441
			public XsdBuilder.XsdBuildFunction BuildFunc;
		}

		// Token: 0x02000324 RID: 804
		private sealed class XsdEntry
		{
			// Token: 0x060024C5 RID: 9413 RVA: 0x000D0024 File Offset: 0x000CE224
			public XsdEntry(SchemaNames.Token n, XsdBuilder.State state, XsdBuilder.State[] nextStates, XsdBuilder.XsdAttributeEntry[] attributes, XsdBuilder.XsdInitFunction init, XsdBuilder.XsdEndChildFunction end, bool parseContent)
			{
				this.Name = n;
				this.CurrentState = state;
				this.NextStates = nextStates;
				this.Attributes = attributes;
				this.InitFunc = init;
				this.EndChildFunc = end;
				this.ParseContent = parseContent;
			}

			// Token: 0x0400115A RID: 4442
			public SchemaNames.Token Name;

			// Token: 0x0400115B RID: 4443
			public XsdBuilder.State CurrentState;

			// Token: 0x0400115C RID: 4444
			public XsdBuilder.State[] NextStates;

			// Token: 0x0400115D RID: 4445
			public XsdBuilder.XsdAttributeEntry[] Attributes;

			// Token: 0x0400115E RID: 4446
			public XsdBuilder.XsdInitFunction InitFunc;

			// Token: 0x0400115F RID: 4447
			public XsdBuilder.XsdEndChildFunction EndChildFunc;

			// Token: 0x04001160 RID: 4448
			public bool ParseContent;
		}

		// Token: 0x02000325 RID: 805
		private class BuilderNamespaceManager : XmlNamespaceManager
		{
			// Token: 0x060024C6 RID: 9414 RVA: 0x000D0061 File Offset: 0x000CE261
			public BuilderNamespaceManager(XmlNamespaceManager nsMgr, XmlReader reader)
			{
				this.nsMgr = nsMgr;
				this.reader = reader;
			}

			// Token: 0x060024C7 RID: 9415 RVA: 0x000D0078 File Offset: 0x000CE278
			public override string LookupNamespace(string prefix)
			{
				string text = this.nsMgr.LookupNamespace(prefix);
				if (text == null)
				{
					text = this.reader.LookupNamespace(prefix);
				}
				return text;
			}

			// Token: 0x04001161 RID: 4449
			private XmlNamespaceManager nsMgr;

			// Token: 0x04001162 RID: 4450
			private XmlReader reader;
		}
	}
}
