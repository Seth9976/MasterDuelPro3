using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An in-memory representation of an XML Schema as specified in the World Wide Web Consortium (W3C) XML Schema Part 1: Structures and XML Schema Part 2: Datatypes specifications.</summary>
	// Token: 0x020002B4 RID: 692
	[XmlRoot("schema", Namespace = "http://www.w3.org/2001/XMLSchema")]
	public class XmlSchema : XmlSchemaObject
	{
		/// <summary>Reads an XML Schema from the supplied <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <param name="reader">The TextReader containing the XML Schema to read. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema syntax errors. </param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x06001FAA RID: 8106 RVA: 0x000BDB77 File Offset: 0x000BBD77
		public static XmlSchema Read(TextReader reader, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(reader), validationEventHandler);
		}

		/// <summary>Reads an XML Schema from the supplied <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <param name="reader">The XmlReader containing the XML Schema to read. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema syntax errors. </param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x06001FAB RID: 8107 RVA: 0x000BDB88 File Offset: 0x000BBD88
		public static XmlSchema Read(XmlReader reader, ValidationEventHandler validationEventHandler)
		{
			XmlNameTable nameTable = reader.NameTable;
			Parser parser = new Parser(SchemaType.XSD, nameTable, new SchemaNames(nameTable), validationEventHandler);
			try
			{
				parser.Parse(reader, null);
			}
			catch (XmlSchemaException ex)
			{
				if (validationEventHandler != null)
				{
					validationEventHandler(null, new ValidationEventArgs(ex));
					return null;
				}
				throw ex;
			}
			return parser.XmlSchema;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x000BDBE8 File Offset: 0x000BBDE8
		internal bool CompileSchema(XmlSchemaCollection xsc, XmlResolver resolver, SchemaInfo schemaInfo, string ns, ValidationEventHandler validationEventHandler, XmlNameTable nameTable, bool CompileContentModel)
		{
			bool flag2;
			lock (this)
			{
				if (!new SchemaCollectionPreprocessor(nameTable, null, validationEventHandler)
				{
					XmlResolver = resolver
				}.Execute(this, ns, true, xsc))
				{
					flag2 = false;
				}
				else
				{
					SchemaCollectionCompiler schemaCollectionCompiler = new SchemaCollectionCompiler(nameTable, validationEventHandler);
					this.isCompiled = schemaCollectionCompiler.Execute(this, schemaInfo, CompileContentModel);
					this.SetIsCompiled(this.isCompiled);
					flag2 = this.isCompiled;
				}
			}
			return flag2;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000BDC6C File Offset: 0x000BBE6C
		internal void CompileSchemaInSet(XmlNameTable nameTable, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			Compiler compiler = new Compiler(nameTable, eventHandler, null, compilationSettings);
			compiler.Prepare(this, true);
			this.isCompiledBySet = compiler.Compile();
		}

		/// <summary>Gets or sets the form for attributes declared in the target namespace of the schema.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaForm" /> value that indicates if attributes from the target namespace are required to be qualified with the namespace prefix. The default is <see cref="F:System.Xml.Schema.XmlSchemaForm.None" />.</returns>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x000BDC97 File Offset: 0x000BBE97
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x000BDC9F File Offset: 0x000BBE9F
		[DefaultValue(XmlSchemaForm.None)]
		[XmlAttribute("attributeFormDefault")]
		public XmlSchemaForm AttributeFormDefault
		{
			get
			{
				return this.attributeFormDefault;
			}
			set
			{
				this.attributeFormDefault = value;
			}
		}

		/// <summary>Gets or sets the blockDefault attribute which sets the default value of the block attribute on element and complex types in the targetNamespace of the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> value representing the different methods for preventing derivation. The default value is XmlSchemaDerivationMethod.None.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x000BDCA8 File Offset: 0x000BBEA8
		// (set) Token: 0x06001FB1 RID: 8113 RVA: 0x000BDCB0 File Offset: 0x000BBEB0
		[XmlAttribute("blockDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod BlockDefault
		{
			get
			{
				return this.blockDefault;
			}
			set
			{
				this.blockDefault = value;
			}
		}

		/// <summary>Gets or sets the finalDefault attribute which sets the default value of the final attribute on elements and complex types in the target namespace of the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> value representing the different methods for preventing derivation. The default value is XmlSchemaDerivationMethod.None.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x000BDCB9 File Offset: 0x000BBEB9
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x000BDCC1 File Offset: 0x000BBEC1
		[XmlAttribute("finalDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod FinalDefault
		{
			get
			{
				return this.finalDefault;
			}
			set
			{
				this.finalDefault = value;
			}
		}

		/// <summary>Gets or sets the form for elements declared in the target namespace of the schema.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaForm" /> value that indicates if elements from the target namespace are required to be qualified with the namespace prefix. The default is <see cref="F:System.Xml.Schema.XmlSchemaForm.None" />.</returns>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x000BDCCA File Offset: 0x000BBECA
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x000BDCD2 File Offset: 0x000BBED2
		[XmlAttribute("elementFormDefault")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm ElementFormDefault
		{
			get
			{
				return this.elementFormDefault;
			}
			set
			{
				this.elementFormDefault = value;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the schema target namespace.</summary>
		/// <returns>The schema target namespace.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x000BDCDB File Offset: 0x000BBEDB
		// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x000BDCE3 File Offset: 0x000BBEE3
		[XmlAttribute("targetNamespace", DataType = "anyURI")]
		public string TargetNamespace
		{
			get
			{
				return this.targetNs;
			}
			set
			{
				this.targetNs = value;
			}
		}

		/// <summary>Gets or sets the version of the schema.</summary>
		/// <returns>The version of the schema. The default value is String.Empty.</returns>
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x000BDCEC File Offset: 0x000BBEEC
		// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x000BDCF4 File Offset: 0x000BBEF4
		[XmlAttribute("version", DataType = "token")]
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Gets the collection of included and imported schemas.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of the included and imported schemas.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x000BDCFD File Offset: 0x000BBEFD
		[XmlElement("include", typeof(XmlSchemaInclude))]
		[XmlElement("import", typeof(XmlSchemaImport))]
		[XmlElement("redefine", typeof(XmlSchemaRedefine))]
		public XmlSchemaObjectCollection Includes
		{
			get
			{
				return this.includes;
			}
		}

		/// <summary>Gets the collection of schema elements in the schema and is used to add new element types at the schema element level.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of schema elements in the schema.</returns>
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x000BDD05 File Offset: 0x000BBF05
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("notation", typeof(XmlSchemaNotation))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x000BDD0D File Offset: 0x000BBF0D
		// (set) Token: 0x06001FBD RID: 8125 RVA: 0x000BDD15 File Offset: 0x000BBF15
		[XmlIgnore]
		internal bool IsCompiledBySet
		{
			get
			{
				return this.isCompiledBySet;
			}
			set
			{
				this.isCompiledBySet = value;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x000BDD1E File Offset: 0x000BBF1E
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x000BDD26 File Offset: 0x000BBF26
		[XmlIgnore]
		internal bool IsPreprocessed
		{
			get
			{
				return this.isPreprocessed;
			}
			set
			{
				this.isPreprocessed = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x000BDD2F File Offset: 0x000BBF2F
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x000BDD37 File Offset: 0x000BBF37
		[XmlIgnore]
		internal bool IsRedefined
		{
			get
			{
				return this.isRedefined;
			}
			set
			{
				this.isRedefined = value;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all the attributes in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the attributes in the schema.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x000BDD40 File Offset: 0x000BBF40
		[XmlIgnore]
		public XmlSchemaObjectTable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectTable();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all the global attribute groups in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the global attribute groups in the schema.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x000BDD5B File Offset: 0x000BBF5B
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				if (this.attributeGroups == null)
				{
					this.attributeGroups = new XmlSchemaObjectTable();
				}
				return this.attributeGroups;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all schema types in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of all schema types in the schema.</returns>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x000BDD76 File Offset: 0x000BBF76
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				if (this.types == null)
				{
					this.types = new XmlSchemaObjectTable();
				}
				return this.types;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all the elements in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the elements in the schema.</returns>
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x000BDD91 File Offset: 0x000BBF91
		[XmlIgnore]
		public XmlSchemaObjectTable Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new XmlSchemaObjectTable();
				}
				return this.elements;
			}
		}

		/// <summary>Gets or sets the string ID.</summary>
		/// <returns>The ID of the string. The default value is String.Empty.</returns>
		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000BDDAC File Offset: 0x000BBFAC
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x000BDDB4 File Offset: 0x000BBFB4
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all the groups in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the groups in the schema.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x000BDDBD File Offset: 0x000BBFBD
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all notations in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all notations in the schema.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x000BDDC5 File Offset: 0x000BBFC5
		[XmlIgnore]
		public XmlSchemaObjectTable Notations
		{
			get
			{
				return this.notations;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x000BDDCD File Offset: 0x000BBFCD
		[XmlIgnore]
		internal XmlSchemaObjectTable IdentityConstraints
		{
			get
			{
				return this.identityConstraints;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x000BDDD5 File Offset: 0x000BBFD5
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x000BDDDD File Offset: 0x000BBFDD
		[XmlIgnore]
		internal Uri BaseUri
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

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x000BDDE6 File Offset: 0x000BBFE6
		[XmlIgnore]
		internal int SchemaId
		{
			get
			{
				if (this.schemaId == -1)
				{
					this.schemaId = Interlocked.Increment(ref XmlSchema.globalIdCounter);
				}
				return this.schemaId;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000BDE07 File Offset: 0x000BC007
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x000BDE0F File Offset: 0x000BC00F
		[XmlIgnore]
		internal bool IsChameleon
		{
			get
			{
				return this.isChameleon;
			}
			set
			{
				this.isChameleon = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x000BDE18 File Offset: 0x000BC018
		[XmlIgnore]
		internal Hashtable Ids
		{
			get
			{
				return this.ids;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x000BDE20 File Offset: 0x000BC020
		[XmlIgnore]
		internal XmlDocument Document
		{
			get
			{
				if (this.document == null)
				{
					this.document = new XmlDocument();
				}
				return this.document;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x000BDE3B File Offset: 0x000BC03B
		// (set) Token: 0x06001FD3 RID: 8147 RVA: 0x000BDE43 File Offset: 0x000BC043
		[XmlIgnore]
		internal int ErrorCount
		{
			get
			{
				return this.errorCount;
			}
			set
			{
				this.errorCount = value;
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000BDE4C File Offset: 0x000BC04C
		internal new XmlSchema Clone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.includes = this.includes;
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.items = this.items;
			xmlSchema.BaseUri = this.BaseUri;
			SchemaCollectionCompiler.Cleanup(xmlSchema);
			return xmlSchema;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000BDEDC File Offset: 0x000BC0DC
		internal XmlSchema DeepClone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.isPreprocessed = this.isPreprocessed;
			for (int i = 0; i < this.items.Count; i++)
			{
				XmlSchemaComplexType xmlSchemaComplexType;
				XmlSchemaObject xmlSchemaObject;
				XmlSchemaElement xmlSchemaElement;
				XmlSchemaGroup xmlSchemaGroup;
				if ((xmlSchemaComplexType = this.items[i] as XmlSchemaComplexType) != null)
				{
					xmlSchemaObject = xmlSchemaComplexType.Clone(this);
				}
				else if ((xmlSchemaElement = this.items[i] as XmlSchemaElement) != null)
				{
					xmlSchemaObject = xmlSchemaElement.Clone(this);
				}
				else if ((xmlSchemaGroup = this.items[i] as XmlSchemaGroup) != null)
				{
					xmlSchemaObject = xmlSchemaGroup.Clone(this);
				}
				else
				{
					xmlSchemaObject = this.items[i].Clone();
				}
				xmlSchema.Items.Add(xmlSchemaObject);
			}
			for (int j = 0; j < this.includes.Count; j++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)this.includes[j].Clone();
				xmlSchema.Includes.Add(xmlSchemaExternal);
			}
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.BaseUri = this.BaseUri;
			return xmlSchema;
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000BE039 File Offset: 0x000BC239
		// (set) Token: 0x06001FD7 RID: 8151 RVA: 0x000BE041 File Offset: 0x000BC241
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x000BE04A File Offset: 0x000BC24A
		internal void SetIsCompiled(bool isCompiled)
		{
			this.isCompiled = isCompiled;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000BE053 File Offset: 0x000BC253
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000BE05C File Offset: 0x000BC25C
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x000BE06B File Offset: 0x000BC26B
		internal ArrayList ImportedSchemas
		{
			get
			{
				if (this.importedSchemas == null)
				{
					this.importedSchemas = new ArrayList();
				}
				return this.importedSchemas;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x000BE086 File Offset: 0x000BC286
		internal ArrayList ImportedNamespaces
		{
			get
			{
				if (this.importedNamespaces == null)
				{
					this.importedNamespaces = new ArrayList();
				}
				return this.importedNamespaces;
			}
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000BE0A4 File Offset: 0x000BC2A4
		internal void GetExternalSchemasList(IList extList, XmlSchema schema)
		{
			if (extList.Contains(schema))
			{
				return;
			}
			extList.Add(schema);
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					this.GetExternalSchemasList(extList, xmlSchemaExternal.Schema);
				}
			}
		}

		// Token: 0x04000ED5 RID: 3797
		private XmlSchemaForm attributeFormDefault;

		// Token: 0x04000ED6 RID: 3798
		private XmlSchemaForm elementFormDefault;

		// Token: 0x04000ED7 RID: 3799
		private XmlSchemaDerivationMethod blockDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x04000ED8 RID: 3800
		private XmlSchemaDerivationMethod finalDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x04000ED9 RID: 3801
		private string targetNs;

		// Token: 0x04000EDA RID: 3802
		private string version;

		// Token: 0x04000EDB RID: 3803
		private XmlSchemaObjectCollection includes = new XmlSchemaObjectCollection();

		// Token: 0x04000EDC RID: 3804
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04000EDD RID: 3805
		private string id;

		// Token: 0x04000EDE RID: 3806
		private XmlAttribute[] moreAttributes;

		// Token: 0x04000EDF RID: 3807
		private bool isCompiled;

		// Token: 0x04000EE0 RID: 3808
		private bool isCompiledBySet;

		// Token: 0x04000EE1 RID: 3809
		private bool isPreprocessed;

		// Token: 0x04000EE2 RID: 3810
		private bool isRedefined;

		// Token: 0x04000EE3 RID: 3811
		private int errorCount;

		// Token: 0x04000EE4 RID: 3812
		private XmlSchemaObjectTable attributes;

		// Token: 0x04000EE5 RID: 3813
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04000EE6 RID: 3814
		private XmlSchemaObjectTable elements = new XmlSchemaObjectTable();

		// Token: 0x04000EE7 RID: 3815
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x04000EE8 RID: 3816
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();

		// Token: 0x04000EE9 RID: 3817
		private XmlSchemaObjectTable notations = new XmlSchemaObjectTable();

		// Token: 0x04000EEA RID: 3818
		private XmlSchemaObjectTable identityConstraints = new XmlSchemaObjectTable();

		// Token: 0x04000EEB RID: 3819
		private static int globalIdCounter = -1;

		// Token: 0x04000EEC RID: 3820
		private ArrayList importedSchemas;

		// Token: 0x04000EED RID: 3821
		private ArrayList importedNamespaces;

		// Token: 0x04000EEE RID: 3822
		private int schemaId = -1;

		// Token: 0x04000EEF RID: 3823
		private Uri baseUri;

		// Token: 0x04000EF0 RID: 3824
		private bool isChameleon;

		// Token: 0x04000EF1 RID: 3825
		private Hashtable ids = new Hashtable();

		// Token: 0x04000EF2 RID: 3826
		private XmlDocument document;
	}
}
