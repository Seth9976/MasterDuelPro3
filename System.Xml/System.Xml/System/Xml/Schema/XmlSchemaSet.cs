using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Xml.Schema
{
	/// <summary>Contains a cache of XML Schema definition language (XSD) schemas.</summary>
	// Token: 0x020002FB RID: 763
	public class XmlSchemaSet
	{
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000C1100 File Offset: 0x000BF300
		internal object InternalSyncObject
		{
			get
			{
				if (this.internalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref this.internalSyncObject, obj, null);
				}
				return this.internalSyncObject;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class.</summary>
		// Token: 0x060021F7 RID: 8695 RVA: 0x000C112F File Offset: 0x000BF32F
		public XmlSchemaSet()
			: this(new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> object to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.XmlNameTable" /> object passed as a parameter is null.</exception>
		// Token: 0x060021F8 RID: 8696 RVA: 0x000C113C File Offset: 0x000BF33C
		public XmlSchemaSet(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
			this.schemas = new SortedList();
			this.schemaLocations = new Hashtable();
			this.chameleonSchemas = new Hashtable();
			this.targetNamespaces = new Hashtable();
			this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallback);
			this.eventHandler = this.internalEventHandler;
			this.readerSettings = new XmlReaderSettings();
			if (this.readerSettings.GetXmlResolver() == null)
			{
				this.readerSettings.XmlResolver = new XmlUrlResolver();
				this.readerSettings.IsXmlResolverSet = false;
			}
			this.readerSettings.NameTable = nameTable;
			this.readerSettings.DtdProcessing = DtdProcessing.Prohibit;
			this.compilationSettings = new XmlSchemaCompilationSettings();
			this.cachedCompiledInfo = new SchemaInfo();
			this.compileAll = true;
		}

		/// <summary>Specifies an event handler for receiving information about XML Schema definition language (XSD) schema validation errors.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060021F9 RID: 8697 RVA: 0x000C1218 File Offset: 0x000BF418
		// (remove) Token: 0x060021FA RID: 8698 RVA: 0x000C126C File Offset: 0x000BF46C
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, this.internalEventHandler);
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
			}
			remove
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
			}
		}

		/// <summary>Gets a value that indicates whether the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> have been compiled.</summary>
		/// <returns>true if the schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> have been compiled since the last time a schema was added or removed from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, false.</returns>
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x000C1299 File Offset: 0x000BF499
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces or locations referenced in include and import elements of a schema.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces or locations referenced in include and import elements of a schema.</returns>
		// Token: 0x17000857 RID: 2135
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x000C12A1 File Offset: 0x000BF4A1
		public XmlResolver XmlResolver
		{
			set
			{
				this.readerSettings.XmlResolver = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> for the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> for the <see cref="T:System.Xml.Schema.XmlSchemaSet" />. The default is an <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> instance with the <see cref="P:System.Xml.Schema.XmlSchemaCompilationSettings.EnableUpaCheck" /> property set to true.</returns>
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000C12AF File Offset: 0x000BF4AF
		// (set) Token: 0x060021FE RID: 8702 RVA: 0x000C12B7 File Offset: 0x000BF4B7
		public XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
			set
			{
				this.compilationSettings = value;
			}
		}

		/// <summary>Gets the number of logical XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The number of logical schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000C12C0 File Offset: 0x000BF4C0
		public int Count
		{
			get
			{
				return this.schemas.Count;
			}
		}

		/// <summary>Gets all the global elements in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The collection of global elements.</returns>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000C12CD File Offset: 0x000BF4CD
		public XmlSchemaObjectTable GlobalElements
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

		/// <summary>Gets all the global attributes in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The collection of global attributes.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000C12E8 File Offset: 0x000BF4E8
		public XmlSchemaObjectTable GlobalAttributes
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

		/// <summary>Gets all of the global simple and complex types in all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The collection of global simple and complex types.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000C1303 File Offset: 0x000BF503
		public XmlSchemaObjectTable GlobalTypes
		{
			get
			{
				if (this.schemaTypes == null)
				{
					this.schemaTypes = new XmlSchemaObjectTable();
				}
				return this.schemaTypes;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x000C131E File Offset: 0x000BF51E
		internal XmlSchemaObjectTable SubstitutionGroups
		{
			get
			{
				if (this.substitutionGroups == null)
				{
					this.substitutionGroups = new XmlSchemaObjectTable();
				}
				return this.substitutionGroups;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x000C1339 File Offset: 0x000BF539
		internal Hashtable SchemaLocations
		{
			get
			{
				return this.schemaLocations;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x000C1341 File Offset: 0x000BF541
		internal XmlSchemaObjectTable TypeExtensions
		{
			get
			{
				if (this.typeExtensions == null)
				{
					this.typeExtensions = new XmlSchemaObjectTable();
				}
				return this.typeExtensions;
			}
		}

		/// <summary>Adds all the XML Schema definition language (XSD) schemas in the given <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <param name="schemas">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">A schema in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object passed as a parameter is null.</exception>
		// Token: 0x06002206 RID: 8710 RVA: 0x000C135C File Offset: 0x000BF55C
		public void Add(XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (this == schemas)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				for (;;)
				{
					Monitor.TryEnter(this.InternalSyncObject, ref flag);
					if (flag)
					{
						Monitor.TryEnter(schemas.InternalSyncObject, ref flag2);
						if (flag2)
						{
							break;
						}
						Monitor.Exit(this.InternalSyncObject);
						flag = false;
						Thread.Yield();
					}
				}
				if (schemas.IsCompiled)
				{
					this.CopyFromCompiledSet(schemas);
				}
				else
				{
					bool flag3 = false;
					foreach (object obj in schemas.SortedSchemas.Values)
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						string text = xmlSchema.TargetNamespace;
						if (text == null)
						{
							text = string.Empty;
						}
						if (!this.schemas.ContainsKey(xmlSchema.SchemaId) && this.FindSchemaByNSAndUrl(xmlSchema.BaseUri, text, null) == null && this.Add(xmlSchema.TargetNamespace, xmlSchema) == null)
						{
							flag3 = true;
							break;
						}
					}
					if (flag3)
					{
						foreach (object obj2 in schemas.SortedSchemas.Values)
						{
							XmlSchema xmlSchema2 = (XmlSchema)obj2;
							this.schemas.Remove(xmlSchema2.SchemaId);
							this.schemaLocations.Remove(xmlSchema2.BaseUri);
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.InternalSyncObject);
				}
				if (flag2)
				{
					Monitor.Exit(schemas.InternalSyncObject);
				}
			}
		}

		/// <summary>Adds the given <see cref="T:System.Xml.Schema.XmlSchema" /> to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is valid. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, then null is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to add to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter is null.</exception>
		// Token: 0x06002207 RID: 8711 RVA: 0x000C1538 File Offset: 0x000BF738
		public XmlSchema Add(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			object obj = this.InternalSyncObject;
			XmlSchema xmlSchema;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schema.SchemaId))
				{
					xmlSchema = schema;
				}
				else
				{
					xmlSchema = this.Add(schema.TargetNamespace, schema);
				}
			}
			return xmlSchema;
		}

		/// <summary>Removes the specified XML Schema definition language (XSD) schema from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object removed from the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> or null if the schema was not found in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to remove from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not a valid schema.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> passed as a parameter is null.</exception>
		// Token: 0x06002208 RID: 8712 RVA: 0x000C15AC File Offset: 0x000BF7AC
		public XmlSchema Remove(XmlSchema schema)
		{
			return this.Remove(schema, true);
		}

		/// <summary>Removes the specified XML Schema definition language (XSD) schema and all the schemas it imports from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Schema.XmlSchema" /> object and all its imports were successfully removed; otherwise, false.</returns>
		/// <param name="schemaToRemove">The <see cref="T:System.Xml.Schema.XmlSchema" /> object to remove from the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> passed as a parameter is null.</exception>
		// Token: 0x06002209 RID: 8713 RVA: 0x000C15B8 File Offset: 0x000BF7B8
		public bool RemoveRecursive(XmlSchema schemaToRemove)
		{
			if (schemaToRemove == null)
			{
				throw new ArgumentNullException("schemaToRemove");
			}
			if (!this.schemas.ContainsKey(schemaToRemove.SchemaId))
			{
				return false;
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schemaToRemove.SchemaId))
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add(this.GetTargetNamespace(schemaToRemove), schemaToRemove);
					for (int i = 0; i < schemaToRemove.ImportedNamespaces.Count; i++)
					{
						string text = (string)schemaToRemove.ImportedNamespaces[i];
						if (hashtable[text] == null)
						{
							hashtable.Add(text, text);
						}
					}
					ArrayList arrayList = new ArrayList();
					for (int j = 0; j < this.schemas.Count; j++)
					{
						XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(j);
						if (xmlSchema != schemaToRemove && !schemaToRemove.ImportedSchemas.Contains(xmlSchema))
						{
							arrayList.Add(xmlSchema);
						}
					}
					for (int k = 0; k < arrayList.Count; k++)
					{
						XmlSchema xmlSchema = (XmlSchema)arrayList[k];
						if (xmlSchema.ImportedNamespaces.Count > 0)
						{
							foreach (object obj2 in hashtable.Keys)
							{
								string text2 = (string)obj2;
								if (xmlSchema.ImportedNamespaces.Contains(text2))
								{
									this.SendValidationEvent(new XmlSchemaException("The schema could not be removed because other schemas in the set have dependencies on this schema or its imports.", string.Empty), XmlSeverityType.Warning);
									return false;
								}
							}
						}
					}
					this.Remove(schemaToRemove, true);
					for (int l = 0; l < schemaToRemove.ImportedSchemas.Count; l++)
					{
						XmlSchema xmlSchema2 = (XmlSchema)schemaToRemove.ImportedSchemas[l];
						this.Remove(xmlSchema2, true);
					}
					return true;
				}
			}
			return false;
		}

		/// <summary>Indicates whether an XML Schema definition language (XSD) schema with the specified target namespace URI is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>true if a schema with the specified target namespace URI is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, false.</returns>
		/// <param name="targetNamespace">The schema targetNamespace property.</param>
		// Token: 0x0600220A RID: 8714 RVA: 0x000C17F4 File Offset: 0x000BF9F4
		public bool Contains(string targetNamespace)
		{
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			return this.targetNamespaces[targetNamespace] != null;
		}

		/// <summary>Indicates whether the specified XML Schema definition language (XSD) <see cref="T:System.Xml.Schema.XmlSchema" /> object is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Schema.XmlSchema" /> object is in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />; otherwise, false.</returns>
		/// <param name="schema">The <see cref="T:System.Xml.Schema.XmlSchema" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> passed as a parameter is null.</exception>
		// Token: 0x0600220B RID: 8715 RVA: 0x000C180F File Offset: 0x000BFA0F
		public bool Contains(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			return this.schemas.ContainsValue(schema);
		}

		/// <summary>Compiles the XML Schema definition language (XSD) schemas added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> into one logical schema.</summary>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An error occurred when validating and compiling the schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</exception>
		// Token: 0x0600220C RID: 8716 RVA: 0x000C182C File Offset: 0x000BFA2C
		public void Compile()
		{
			if (this.isCompiled)
			{
				return;
			}
			if (this.schemas.Count == 0)
			{
				this.ClearTables();
				this.cachedCompiledInfo = new SchemaInfo();
				this.isCompiled = true;
				this.compileAll = false;
				return;
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (!this.isCompiled)
				{
					Compiler compiler = new Compiler(this.nameTable, this.eventHandler, this.schemaForSchema, this.compilationSettings);
					SchemaInfo schemaInfo = new SchemaInfo();
					int i = 0;
					if (!this.compileAll)
					{
						compiler.ImportAllCompiledSchemas(this);
					}
					try
					{
						XmlSchema buildInSchema = Preprocessor.GetBuildInSchema();
						i = 0;
						while (i < this.schemas.Count)
						{
							XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(i);
							Monitor.Enter(xmlSchema);
							if (!xmlSchema.IsPreprocessed)
							{
								this.SendValidationEvent(new XmlSchemaException("All schemas in the set should be successfully preprocessed prior to compilation.", string.Empty), XmlSeverityType.Error);
								this.isCompiled = false;
								return;
							}
							if (!xmlSchema.IsCompiledBySet)
							{
								goto IL_00FD;
							}
							if (this.compileAll)
							{
								if (xmlSchema != buildInSchema)
								{
									goto IL_00FD;
								}
								compiler.Prepare(xmlSchema, false);
							}
							IL_0106:
							i++;
							continue;
							IL_00FD:
							compiler.Prepare(xmlSchema, true);
							goto IL_0106;
						}
						this.isCompiled = compiler.Execute(this, schemaInfo);
						if (this.isCompiled)
						{
							if (!this.compileAll)
							{
								schemaInfo.Add(this.cachedCompiledInfo, this.eventHandler);
							}
							this.compileAll = false;
							this.cachedCompiledInfo = schemaInfo;
						}
					}
					finally
					{
						if (i == this.schemas.Count)
						{
							i--;
						}
						for (int j = i; j >= 0; j--)
						{
							XmlSchema xmlSchema2 = (XmlSchema)this.schemas.GetByIndex(j);
							if (xmlSchema2 == Preprocessor.GetBuildInSchema())
							{
								Monitor.Exit(xmlSchema2);
							}
							else
							{
								xmlSchema2.IsCompiledBySet = this.isCompiled;
								Monitor.Exit(xmlSchema2);
							}
						}
					}
				}
			}
		}

		/// <summary>Reprocesses an XML Schema definition language (XSD) schema that already exists in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object if the schema is a valid schema. If the schema is not valid and a <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified, null is returned and the appropriate validation event is raised. Otherwise, an <see cref="T:System.Xml.Schema.XmlSchemaException" /> is thrown.</returns>
		/// <param name="schema">The schema to reprocess.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The schema is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.Schema.XmlSchema" /> object passed as a parameter does not already exist in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</exception>
		// Token: 0x0600220D RID: 8717 RVA: 0x000C1A3C File Offset: 0x000BFC3C
		public XmlSchema Reprocess(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (!this.schemas.ContainsKey(schema.SchemaId))
			{
				throw new ArgumentException(Res.GetString("Schema does not exist in the set."), "schema");
			}
			XmlSchema xmlSchema = schema;
			object obj = this.InternalSyncObject;
			XmlSchema xmlSchema2;
			lock (obj)
			{
				this.RemoveSchemaFromGlobalTables(schema);
				this.RemoveSchemaFromCaches(schema);
				if (schema.BaseUri != null)
				{
					this.schemaLocations.Remove(schema.BaseUri);
				}
				string text = this.GetTargetNamespace(schema);
				if (this.Schemas(text).Count == 0)
				{
					this.targetNamespaces.Remove(text);
				}
				this.isCompiled = false;
				this.compileAll = true;
				if (schema.ErrorCount != 0)
				{
					xmlSchema2 = xmlSchema;
				}
				else if (this.PreprocessSchema(ref schema, schema.TargetNamespace))
				{
					if (this.targetNamespaces[text] == null)
					{
						this.targetNamespaces.Add(text, text);
					}
					if (this.schemaForSchema == null && text == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
					{
						this.schemaForSchema = schema;
					}
					for (int i = 0; i < schema.ImportedSchemas.Count; i++)
					{
						XmlSchema xmlSchema3 = (XmlSchema)schema.ImportedSchemas[i];
						if (!this.schemas.ContainsKey(xmlSchema3.SchemaId))
						{
							this.schemas.Add(xmlSchema3.SchemaId, xmlSchema3);
						}
						text = this.GetTargetNamespace(xmlSchema3);
						if (this.targetNamespaces[text] == null)
						{
							this.targetNamespaces.Add(text, text);
						}
						if (this.schemaForSchema == null && text == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
						{
							this.schemaForSchema = schema;
						}
					}
					xmlSchema2 = schema;
				}
				else
				{
					xmlSchema2 = xmlSchema;
				}
			}
			return xmlSchema2;
		}

		/// <summary>Copies all the <see cref="T:System.Xml.Schema.XmlSchema" /> objects from the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to the given array, starting at the given index.</summary>
		/// <param name="schemas">The array to copy the objects to.</param>
		/// <param name="index">The index in the array where copying will begin.</param>
		// Token: 0x0600220E RID: 8718 RVA: 0x000C1C4C File Offset: 0x000BFE4C
		public void CopyTo(XmlSchema[] schemas, int index)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (index < 0 || index > schemas.Length - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.schemas.Values.CopyTo(schemas, index);
		}

		/// <summary>Returns a collection of all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing all the schemas that have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />. If no schemas have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />, an empty <see cref="T:System.Collections.ICollection" /> object is returned.</returns>
		// Token: 0x0600220F RID: 8719 RVA: 0x000C1C85 File Offset: 0x000BFE85
		public ICollection Schemas()
		{
			return this.schemas.Values;
		}

		/// <summary>Returns a collection of all the XML Schema definition language (XSD) schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that belong to the given namespace.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing all the schemas that have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> that belong to the given namespace. If no schemas have been added to the <see cref="T:System.Xml.Schema.XmlSchemaSet" />, an empty <see cref="T:System.Collections.ICollection" /> object is returned.</returns>
		/// <param name="targetNamespace">The schema targetNamespace property.</param>
		// Token: 0x06002210 RID: 8720 RVA: 0x000C1C94 File Offset: 0x000BFE94
		public ICollection Schemas(string targetNamespace)
		{
			ArrayList arrayList = new ArrayList();
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			for (int i = 0; i < this.schemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(i);
				if (this.GetTargetNamespace(xmlSchema) == targetNamespace)
				{
					arrayList.Add(xmlSchema);
				}
			}
			return arrayList;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000C1CF1 File Offset: 0x000BFEF1
		private XmlSchema Add(string targetNamespace, XmlSchema schema)
		{
			if (schema == null || schema.ErrorCount != 0)
			{
				return null;
			}
			if (this.PreprocessSchema(ref schema, targetNamespace))
			{
				this.AddSchemaToSet(schema);
				this.isCompiled = false;
				return schema;
			}
			return null;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000C1D1C File Offset: 0x000BFF1C
		internal void Add(string targetNamespace, XmlReader reader, Hashtable validatedNamespaces)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			if (validatedNamespaces[targetNamespace] != null)
			{
				if (this.FindSchemaByNSAndUrl(new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute), targetNamespace, null) != null)
				{
					return;
				}
				throw new XmlSchemaException("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", targetNamespace);
			}
			else
			{
				XmlSchema xmlSchema;
				if (this.IsSchemaLoaded(new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute), targetNamespace, out xmlSchema))
				{
					return;
				}
				xmlSchema = this.ParseSchema(targetNamespace, reader);
				DictionaryEntry[] array = new DictionaryEntry[this.schemaLocations.Count];
				this.schemaLocations.CopyTo(array, 0);
				this.Add(targetNamespace, xmlSchema);
				if (xmlSchema.ImportedSchemas.Count > 0)
				{
					for (int i = 0; i < xmlSchema.ImportedSchemas.Count; i++)
					{
						XmlSchema xmlSchema2 = (XmlSchema)xmlSchema.ImportedSchemas[i];
						string text = xmlSchema2.TargetNamespace;
						if (text == null)
						{
							text = string.Empty;
						}
						if (validatedNamespaces[text] != null && this.FindSchemaByNSAndUrl(xmlSchema2.BaseUri, text, array) == null)
						{
							this.RemoveRecursive(xmlSchema);
							throw new XmlSchemaException("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", text);
						}
					}
				}
				return;
			}
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000C1E2C File Offset: 0x000C002C
		internal XmlSchema FindSchemaByNSAndUrl(Uri schemaUri, string ns, DictionaryEntry[] locationsTable)
		{
			if (schemaUri == null || schemaUri.OriginalString.Length == 0)
			{
				return null;
			}
			XmlSchema xmlSchema = null;
			if (locationsTable == null)
			{
				xmlSchema = (XmlSchema)this.schemaLocations[schemaUri];
			}
			else
			{
				for (int i = 0; i < locationsTable.Length; i++)
				{
					if (schemaUri.Equals(locationsTable[i].Key))
					{
						xmlSchema = (XmlSchema)locationsTable[i].Value;
						break;
					}
				}
			}
			if (xmlSchema != null)
			{
				string text = ((xmlSchema.TargetNamespace == null) ? string.Empty : xmlSchema.TargetNamespace);
				if (text == ns)
				{
					return xmlSchema;
				}
				if (text == string.Empty)
				{
					ChameleonKey chameleonKey = new ChameleonKey(ns, xmlSchema);
					xmlSchema = (XmlSchema)this.chameleonSchemas[chameleonKey];
				}
				else
				{
					xmlSchema = null;
				}
			}
			return xmlSchema;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000C1EF0 File Offset: 0x000C00F0
		private void AddSchemaToSet(XmlSchema schema)
		{
			this.schemas.Add(schema.SchemaId, schema);
			string text = this.GetTargetNamespace(schema);
			if (this.targetNamespaces[text] == null)
			{
				this.targetNamespaces.Add(text, text);
			}
			if (this.schemaForSchema == null && text == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
			{
				this.schemaForSchema = schema;
			}
			for (int i = 0; i < schema.ImportedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)schema.ImportedSchemas[i];
				if (!this.schemas.ContainsKey(xmlSchema.SchemaId))
				{
					this.schemas.Add(xmlSchema.SchemaId, xmlSchema);
				}
				text = this.GetTargetNamespace(xmlSchema);
				if (this.targetNamespaces[text] == null)
				{
					this.targetNamespaces.Add(text, text);
				}
				if (this.schemaForSchema == null && text == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
				{
					this.schemaForSchema = schema;
				}
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000C2014 File Offset: 0x000C0214
		private void ProcessNewSubstitutionGroups(XmlSchemaObjectTable substitutionGroupsTable, bool resolve)
		{
			foreach (object obj in substitutionGroupsTable.Values)
			{
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)obj;
				if (resolve)
				{
					this.ResolveSubstitutionGroup(xmlSchemaSubstitutionGroup, substitutionGroupsTable);
				}
				XmlQualifiedName examplar = xmlSchemaSubstitutionGroup.Examplar;
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup2 = (XmlSchemaSubstitutionGroup)this.substitutionGroups[examplar];
				if (xmlSchemaSubstitutionGroup2 != null)
				{
					for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
					{
						if (!xmlSchemaSubstitutionGroup2.Members.Contains(xmlSchemaSubstitutionGroup.Members[i]))
						{
							xmlSchemaSubstitutionGroup2.Members.Add(xmlSchemaSubstitutionGroup.Members[i]);
						}
					}
				}
				else
				{
					this.AddToTable(this.substitutionGroups, examplar, xmlSchemaSubstitutionGroup);
				}
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000C20F4 File Offset: 0x000C02F4
		private void ResolveSubstitutionGroup(XmlSchemaSubstitutionGroup substitutionGroup, XmlSchemaObjectTable substTable)
		{
			List<XmlSchemaElement> list = null;
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.elements[substitutionGroup.Examplar];
			if (substitutionGroup.Members.Contains(xmlSchemaElement))
			{
				return;
			}
			for (int i = 0; i < substitutionGroup.Members.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)substitutionGroup.Members[i];
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)substTable[xmlSchemaElement2.QualifiedName];
				if (xmlSchemaSubstitutionGroup != null)
				{
					this.ResolveSubstitutionGroup(xmlSchemaSubstitutionGroup, substTable);
					for (int j = 0; j < xmlSchemaSubstitutionGroup.Members.Count; j++)
					{
						XmlSchemaElement xmlSchemaElement3 = (XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[j];
						if (xmlSchemaElement3 != xmlSchemaElement2)
						{
							if (list == null)
							{
								list = new List<XmlSchemaElement>();
							}
							list.Add(xmlSchemaElement3);
						}
					}
				}
			}
			if (list != null)
			{
				for (int k = 0; k < list.Count; k++)
				{
					substitutionGroup.Members.Add(list[k]);
				}
			}
			substitutionGroup.Members.Add(xmlSchemaElement);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000C21F4 File Offset: 0x000C03F4
		internal XmlSchema Remove(XmlSchema schema, bool forceCompile)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schema.SchemaId))
				{
					if (forceCompile)
					{
						this.RemoveSchemaFromGlobalTables(schema);
						this.RemoveSchemaFromCaches(schema);
					}
					this.schemas.Remove(schema.SchemaId);
					if (schema.BaseUri != null)
					{
						this.schemaLocations.Remove(schema.BaseUri);
					}
					string targetNamespace = this.GetTargetNamespace(schema);
					if (this.Schemas(targetNamespace).Count == 0)
					{
						this.targetNamespaces.Remove(targetNamespace);
					}
					if (forceCompile)
					{
						this.isCompiled = false;
						this.compileAll = true;
					}
					return schema;
				}
			}
			return null;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000C22D4 File Offset: 0x000C04D4
		private void ClearTables()
		{
			this.GlobalElements.Clear();
			this.GlobalAttributes.Clear();
			this.GlobalTypes.Clear();
			this.SubstitutionGroups.Clear();
			this.TypeExtensions.Clear();
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000C2310 File Offset: 0x000C0510
		internal bool PreprocessSchema(ref XmlSchema schema, string targetNamespace)
		{
			Preprocessor preprocessor = new Preprocessor(this.nameTable, this.GetSchemaNames(this.nameTable), this.eventHandler, this.compilationSettings);
			preprocessor.XmlResolver = this.readerSettings.GetXmlResolver_CheckConfig();
			preprocessor.ReaderSettings = this.readerSettings;
			preprocessor.SchemaLocations = this.schemaLocations;
			preprocessor.ChameleonSchemas = this.chameleonSchemas;
			bool flag = preprocessor.Execute(schema, targetNamespace, true);
			schema = preprocessor.RootSchema;
			return flag;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000C2388 File Offset: 0x000C0588
		internal XmlSchema ParseSchema(string targetNamespace, XmlReader reader)
		{
			XmlNameTable xmlNameTable = reader.NameTable;
			SchemaNames schemaNames = this.GetSchemaNames(xmlNameTable);
			Parser parser = new Parser(SchemaType.XSD, xmlNameTable, schemaNames, this.eventHandler);
			parser.XmlResolver = this.readerSettings.GetXmlResolver_CheckConfig();
			try
			{
				parser.Parse(reader, targetNamespace);
			}
			catch (XmlSchemaException ex)
			{
				this.SendValidationEvent(ex, XmlSeverityType.Error);
				return null;
			}
			return parser.XmlSchema;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000C23F8 File Offset: 0x000C05F8
		internal void CopyFromCompiledSet(XmlSchemaSet otherSet)
		{
			SortedList sortedSchemas = otherSet.SortedSchemas;
			bool flag = this.schemas.Count == 0;
			ArrayList arrayList = new ArrayList();
			SchemaInfo schemaInfo = new SchemaInfo();
			for (int i = 0; i < sortedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)sortedSchemas.GetByIndex(i);
				Uri baseUri = xmlSchema.BaseUri;
				if (this.schemas.ContainsKey(xmlSchema.SchemaId) || (baseUri != null && baseUri.OriginalString.Length != 0 && this.schemaLocations[baseUri] != null))
				{
					arrayList.Add(xmlSchema);
				}
				else
				{
					this.schemas.Add(xmlSchema.SchemaId, xmlSchema);
					if (baseUri != null && baseUri.OriginalString.Length != 0)
					{
						this.schemaLocations.Add(baseUri, xmlSchema);
					}
					string targetNamespace = this.GetTargetNamespace(xmlSchema);
					if (this.targetNamespaces[targetNamespace] == null)
					{
						this.targetNamespaces.Add(targetNamespace, targetNamespace);
					}
				}
			}
			this.VerifyTables();
			foreach (object obj in otherSet.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (!this.AddToTable(this.elements, xmlSchemaElement.QualifiedName, xmlSchemaElement))
				{
					goto IL_026E;
				}
			}
			foreach (object obj2 in otherSet.GlobalAttributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				if (!this.AddToTable(this.attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute))
				{
					goto IL_026E;
				}
			}
			foreach (object obj3 in otherSet.GlobalTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				if (!this.AddToTable(this.schemaTypes, xmlSchemaType.QualifiedName, xmlSchemaType))
				{
					goto IL_026E;
				}
			}
			this.ProcessNewSubstitutionGroups(otherSet.SubstitutionGroups, false);
			schemaInfo.Add(this.cachedCompiledInfo, this.eventHandler);
			schemaInfo.Add(otherSet.CompiledInfo, this.eventHandler);
			this.cachedCompiledInfo = schemaInfo;
			if (flag)
			{
				this.isCompiled = true;
				this.compileAll = false;
			}
			return;
			IL_026E:
			foreach (object obj4 in sortedSchemas.Values)
			{
				XmlSchema xmlSchema2 = (XmlSchema)obj4;
				if (!arrayList.Contains(xmlSchema2))
				{
					this.Remove(xmlSchema2, false);
				}
			}
			foreach (object obj5 in otherSet.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)obj5;
				if (!arrayList.Contains((XmlSchema)xmlSchemaElement2.Parent))
				{
					this.elements.Remove(xmlSchemaElement2.QualifiedName);
				}
			}
			foreach (object obj6 in otherSet.GlobalAttributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj6;
				if (!arrayList.Contains((XmlSchema)xmlSchemaAttribute2.Parent))
				{
					this.attributes.Remove(xmlSchemaAttribute2.QualifiedName);
				}
			}
			foreach (object obj7 in otherSet.GlobalTypes.Values)
			{
				XmlSchemaType xmlSchemaType2 = (XmlSchemaType)obj7;
				if (!arrayList.Contains((XmlSchema)xmlSchemaType2.Parent))
				{
					this.schemaTypes.Remove(xmlSchemaType2.QualifiedName);
				}
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000C2854 File Offset: 0x000C0A54
		internal SchemaInfo CompiledInfo
		{
			get
			{
				return this.cachedCompiledInfo;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000C285C File Offset: 0x000C0A5C
		internal XmlReaderSettings ReaderSettings
		{
			get
			{
				return this.readerSettings;
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000C2864 File Offset: 0x000C0A64
		internal XmlResolver GetResolver()
		{
			return this.readerSettings.GetXmlResolver_CheckConfig();
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000C2871 File Offset: 0x000C0A71
		internal ValidationEventHandler GetEventHandler()
		{
			return this.eventHandler;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000C2879 File Offset: 0x000C0A79
		internal SchemaNames GetSchemaNames(XmlNameTable nt)
		{
			if (this.nameTable != nt)
			{
				return new SchemaNames(nt);
			}
			if (this.schemaNames == null)
			{
				this.schemaNames = new SchemaNames(this.nameTable);
			}
			return this.schemaNames;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000C28AC File Offset: 0x000C0AAC
		internal bool IsSchemaLoaded(Uri schemaUri, string targetNamespace, out XmlSchema schema)
		{
			schema = null;
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			if (this.GetSchemaByUri(schemaUri, out schema))
			{
				if (!this.schemas.ContainsKey(schema.SchemaId) || (targetNamespace.Length != 0 && !(targetNamespace == schema.TargetNamespace)))
				{
					if (schema.TargetNamespace == null)
					{
						XmlSchema xmlSchema = this.FindSchemaByNSAndUrl(schemaUri, targetNamespace, null);
						if (xmlSchema != null && this.schemas.ContainsKey(xmlSchema.SchemaId))
						{
							schema = xmlSchema;
						}
						else
						{
							schema = this.Add(targetNamespace, schema);
						}
					}
					else if (targetNamespace.Length != 0 && targetNamespace != schema.TargetNamespace)
					{
						this.SendValidationEvent(new XmlSchemaException("The targetNamespace parameter '{0}' should be the same value as the targetNamespace '{1}' of the schema.", new string[] { targetNamespace, schema.TargetNamespace }), XmlSeverityType.Error);
						schema = null;
					}
					else
					{
						this.AddSchemaToSet(schema);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000C2995 File Offset: 0x000C0B95
		internal bool GetSchemaByUri(Uri schemaUri, out XmlSchema schema)
		{
			schema = null;
			if (schemaUri == null || schemaUri.OriginalString.Length == 0)
			{
				return false;
			}
			schema = (XmlSchema)this.schemaLocations[schemaUri];
			return schema != null;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000C29CC File Offset: 0x000C0BCC
		internal string GetTargetNamespace(XmlSchema schema)
		{
			if (schema.TargetNamespace != null)
			{
				return schema.TargetNamespace;
			}
			return string.Empty;
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x000C29E2 File Offset: 0x000C0BE2
		internal SortedList SortedSchemas
		{
			get
			{
				return this.schemas;
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000C29EC File Offset: 0x000C0BEC
		private void RemoveSchemaFromCaches(XmlSchema schema)
		{
			List<XmlSchema> list = new List<XmlSchema>();
			schema.GetExternalSchemasList(list, schema);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].BaseUri != null && list[i].BaseUri.OriginalString.Length != 0)
				{
					this.schemaLocations.Remove(list[i].BaseUri);
				}
				IEnumerable keys = this.chameleonSchemas.Keys;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in keys)
				{
					ChameleonKey chameleonKey = (ChameleonKey)obj;
					if (chameleonKey.chameleonLocation.Equals(list[i].BaseUri) && (chameleonKey.originalSchema == null || chameleonKey.originalSchema == list[i]))
					{
						arrayList.Add(chameleonKey);
					}
				}
				for (int j = 0; j < arrayList.Count; j++)
				{
					this.chameleonSchemas.Remove(arrayList[j]);
				}
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000C2B1C File Offset: 0x000C0D1C
		private void RemoveSchemaFromGlobalTables(XmlSchema schema)
		{
			if (this.schemas.Count == 0)
			{
				return;
			}
			this.VerifyTables();
			foreach (object obj in schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if ((XmlSchemaElement)this.elements[xmlSchemaElement.QualifiedName] == xmlSchemaElement)
				{
					this.elements.Remove(xmlSchemaElement.QualifiedName);
				}
			}
			foreach (object obj2 in schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				if ((XmlSchemaAttribute)this.attributes[xmlSchemaAttribute.QualifiedName] == xmlSchemaAttribute)
				{
					this.attributes.Remove(xmlSchemaAttribute.QualifiedName);
				}
			}
			foreach (object obj3 in schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				if ((XmlSchemaType)this.schemaTypes[xmlSchemaType.QualifiedName] == xmlSchemaType)
				{
					this.schemaTypes.Remove(xmlSchemaType.QualifiedName);
				}
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000C2C98 File Offset: 0x000C0E98
		private bool AddToTable(XmlSchemaObjectTable table, XmlQualifiedName qname, XmlSchemaObject item)
		{
			if (qname.Name.Length == 0)
			{
				return true;
			}
			XmlSchemaObject xmlSchemaObject = table[qname];
			if (xmlSchemaObject == null)
			{
				table.Add(qname, item);
				return true;
			}
			if (xmlSchemaObject == item || xmlSchemaObject.SourceUri == item.SourceUri)
			{
				return true;
			}
			string text = string.Empty;
			if (item is XmlSchemaComplexType)
			{
				text = "The complexType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaSimpleType)
			{
				text = "The simpleType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaElement)
			{
				text = "The global element '{0}' has already been declared.";
			}
			else if (item is XmlSchemaAttribute)
			{
				if (qname.Namespace == "http://www.w3.org/XML/1998/namespace")
				{
					XmlSchemaObject xmlSchemaObject2 = Preprocessor.GetBuildInSchema().Attributes[qname];
					if (xmlSchemaObject == xmlSchemaObject2)
					{
						table.Insert(qname, item);
						return true;
					}
					if (item == xmlSchemaObject2)
					{
						return true;
					}
				}
				text = "The global attribute '{0}' has already been declared.";
			}
			this.SendValidationEvent(new XmlSchemaException(text, qname.ToString()), XmlSeverityType.Error);
			return false;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000C2D74 File Offset: 0x000C0F74
		private void VerifyTables()
		{
			if (this.elements == null)
			{
				this.elements = new XmlSchemaObjectTable();
			}
			if (this.attributes == null)
			{
				this.attributes = new XmlSchemaObjectTable();
			}
			if (this.schemaTypes == null)
			{
				this.schemaTypes = new XmlSchemaObjectTable();
			}
			if (this.substitutionGroups == null)
			{
				this.substitutionGroups = new XmlSchemaObjectTable();
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000C2DCD File Offset: 0x000C0FCD
		private void InternalValidationCallback(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000C2DDE File Offset: 0x000C0FDE
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandler != null)
			{
				this.eventHandler(this, new ValidationEventArgs(e, severity));
				return;
			}
			throw e;
		}

		// Token: 0x04000FCF RID: 4047
		private XmlNameTable nameTable;

		// Token: 0x04000FD0 RID: 4048
		private SchemaNames schemaNames;

		// Token: 0x04000FD1 RID: 4049
		private SortedList schemas;

		// Token: 0x04000FD2 RID: 4050
		private ValidationEventHandler internalEventHandler;

		// Token: 0x04000FD3 RID: 4051
		private ValidationEventHandler eventHandler;

		// Token: 0x04000FD4 RID: 4052
		private bool isCompiled;

		// Token: 0x04000FD5 RID: 4053
		private Hashtable schemaLocations;

		// Token: 0x04000FD6 RID: 4054
		private Hashtable chameleonSchemas;

		// Token: 0x04000FD7 RID: 4055
		private Hashtable targetNamespaces;

		// Token: 0x04000FD8 RID: 4056
		private bool compileAll;

		// Token: 0x04000FD9 RID: 4057
		private SchemaInfo cachedCompiledInfo;

		// Token: 0x04000FDA RID: 4058
		private XmlReaderSettings readerSettings;

		// Token: 0x04000FDB RID: 4059
		private XmlSchema schemaForSchema;

		// Token: 0x04000FDC RID: 4060
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x04000FDD RID: 4061
		internal XmlSchemaObjectTable elements;

		// Token: 0x04000FDE RID: 4062
		internal XmlSchemaObjectTable attributes;

		// Token: 0x04000FDF RID: 4063
		internal XmlSchemaObjectTable schemaTypes;

		// Token: 0x04000FE0 RID: 4064
		internal XmlSchemaObjectTable substitutionGroups;

		// Token: 0x04000FE1 RID: 4065
		private XmlSchemaObjectTable typeExtensions;

		// Token: 0x04000FE2 RID: 4066
		private object internalSyncObject;
	}
}
