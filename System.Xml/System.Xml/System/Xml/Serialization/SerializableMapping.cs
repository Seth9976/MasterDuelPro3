using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000171 RID: 369
	internal class SerializableMapping : SpecialMapping
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x00054E6A File Offset: 0x0005306A
		internal SerializableMapping()
		{
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00054E79 File Offset: 0x00053079
		internal SerializableMapping(MethodInfo getSchemaMethod, bool any, string ns)
		{
			this.getSchemaMethod = getSchemaMethod;
			this.any = any;
			base.Namespace = ns;
			this.needSchema = getSchemaMethod != null;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00054EAA File Offset: 0x000530AA
		internal SerializableMapping(XmlQualifiedName xsiType, XmlSchemaSet schemas)
		{
			this.xsiType = xsiType;
			this.schemas = schemas;
			base.TypeName = xsiType.Name;
			base.Namespace = xsiType.Namespace;
			this.needSchema = false;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00054EE8 File Offset: 0x000530E8
		internal void SetBaseMapping(SerializableMapping mapping)
		{
			this.baseMapping = mapping;
			if (this.baseMapping != null)
			{
				this.nextDerivedMapping = this.baseMapping.derivedMappings;
				this.baseMapping.derivedMappings = this;
				if (this == this.nextDerivedMapping)
				{
					throw new InvalidOperationException(Res.GetString("Circular reference in derivation of IXmlSerializable type '{0}'.", new object[] { base.TypeDesc.FullName }));
				}
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00054F50 File Offset: 0x00053150
		internal bool IsAny
		{
			get
			{
				if (this.any)
				{
					return true;
				}
				if (this.getSchemaMethod == null)
				{
					return false;
				}
				if (this.needSchema && typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return false;
				}
				this.RetrieveSerializableSchema();
				return this.any;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00054FAC File Offset: 0x000531AC
		internal string NamespaceList
		{
			get
			{
				this.RetrieveSerializableSchema();
				if (this.namespaces == null)
				{
					if (this.schemas != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (object obj in this.schemas.Schemas())
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							if (xmlSchema.TargetNamespace != null && xmlSchema.TargetNamespace.Length > 0)
							{
								if (stringBuilder.Length > 0)
								{
									stringBuilder.Append(" ");
								}
								stringBuilder.Append(xmlSchema.TargetNamespace);
							}
						}
						this.namespaces = stringBuilder.ToString();
					}
					else
					{
						this.namespaces = string.Empty;
					}
				}
				return this.namespaces;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0005507C File Offset: 0x0005327C
		internal SerializableMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00055084 File Offset: 0x00053284
		internal SerializableMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0005508C File Offset: 0x0005328C
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x00055094 File Offset: 0x00053294
		internal SerializableMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0005509D File Offset: 0x0005329D
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x000550A5 File Offset: 0x000532A5
		internal Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000550AE File Offset: 0x000532AE
		internal XmlSchemaSet Schemas
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schemas;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x000550BC File Offset: 0x000532BC
		internal XmlSchema Schema
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schema;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x000550CC File Offset: 0x000532CC
		internal XmlQualifiedName XsiType
		{
			get
			{
				if (!this.needSchema)
				{
					return this.xsiType;
				}
				if (this.getSchemaMethod == null)
				{
					return null;
				}
				if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return null;
				}
				this.RetrieveSerializableSchema();
				return this.xsiType;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00055122 File Offset: 0x00053322
		internal XmlSchemaType XsdType
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.xsdType;
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00055130 File Offset: 0x00053330
		internal static void ValidationCallbackWithErrorCode(object sender, ValidationEventArgs args)
		{
			if (args.Severity == XmlSeverityType.Error)
			{
				throw new InvalidOperationException(Res.GetString("Schema type information provided by {0} is invalid: {1}", new object[]
				{
					typeof(IXmlSerializable).Name,
					args.Message
				}));
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0005516C File Offset: 0x0005336C
		internal void CheckDuplicateElement(XmlSchemaElement element, string elementNs)
		{
			if (element == null)
			{
				return;
			}
			if (element.Parent == null || !(element.Parent is XmlSchema))
			{
				return;
			}
			XmlSchemaObjectTable xmlSchemaObjectTable;
			if (this.Schema != null && this.Schema.TargetNamespace == elementNs)
			{
				XmlSchemas.Preprocess(this.Schema);
				xmlSchemaObjectTable = this.Schema.Elements;
			}
			else
			{
				if (this.Schemas == null)
				{
					return;
				}
				xmlSchemaObjectTable = this.Schemas.GlobalElements;
			}
			foreach (object obj in xmlSchemaObjectTable.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (xmlSchemaElement.Name == element.Name && xmlSchemaElement.QualifiedName.Namespace == elementNs)
				{
					if (this.Match(xmlSchemaElement, element))
					{
						break;
					}
					throw new InvalidOperationException(Res.GetString("Cannot reconcile schema for '{0}'. Please use [XmlRoot] attribute to change default name or namespace of the top-level element to avoid duplicate element declarations: element name='{1}' namespace='{2}'.", new object[]
					{
						this.getSchemaMethod.DeclaringType.FullName,
						xmlSchemaElement.Name,
						elementNs
					}));
				}
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00055290 File Offset: 0x00053490
		private bool Match(XmlSchemaElement e1, XmlSchemaElement e2)
		{
			return e1.IsNillable == e2.IsNillable && !(e1.RefName != e2.RefName) && e1.SchemaType == e2.SchemaType && !(e1.SchemaTypeName != e2.SchemaTypeName) && !(e1.MinOccurs != e2.MinOccurs) && !(e1.MaxOccurs != e2.MaxOccurs) && e1.IsAbstract == e2.IsAbstract && !(e1.DefaultValue != e2.DefaultValue) && !(e1.SubstitutionGroup != e2.SubstitutionGroup);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0005534C File Offset: 0x0005354C
		private void RetrieveSerializableSchema()
		{
			if (this.needSchema)
			{
				this.needSchema = false;
				if (this.getSchemaMethod != null)
				{
					if (this.schemas == null)
					{
						this.schemas = new XmlSchemaSet();
					}
					object obj = this.getSchemaMethod.Invoke(null, new object[] { this.schemas });
					this.xsiType = XmlQualifiedName.Empty;
					if (obj != null)
					{
						if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
						{
							this.xsdType = (XmlSchemaType)obj;
							this.xsiType = this.xsdType.QualifiedName;
						}
						else
						{
							if (!typeof(XmlQualifiedName).IsAssignableFrom(this.getSchemaMethod.ReturnType))
							{
								throw new InvalidOperationException(Res.GetString("Method {0}.{1}() specified by {2} has invalid signature: return type must be compatible with {3}.", new object[]
								{
									this.type.Name,
									this.getSchemaMethod.Name,
									typeof(XmlSchemaProviderAttribute).Name,
									typeof(XmlQualifiedName).FullName
								}));
							}
							this.xsiType = (XmlQualifiedName)obj;
							if (this.xsiType.IsEmpty)
							{
								throw new InvalidOperationException(Res.GetString("{0}.{1}() must return a valid type name.", new object[]
								{
									this.type.FullName,
									this.getSchemaMethod.Name
								}));
							}
						}
					}
					else
					{
						this.any = true;
					}
					this.schemas.ValidationEventHandler += SerializableMapping.ValidationCallbackWithErrorCode;
					this.schemas.Compile();
					if (!this.xsiType.IsEmpty && this.xsiType.Namespace != "http://www.w3.org/2001/XMLSchema")
					{
						ArrayList arrayList = (ArrayList)this.schemas.Schemas(this.xsiType.Namespace);
						if (arrayList.Count == 0)
						{
							throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[] { this.xsiType.Namespace }));
						}
						if (arrayList.Count > 1)
						{
							throw new InvalidOperationException(Res.GetString("Multiple schemas with targetNamespace='{0}' returned by {1}.{2}().  Please use only the main (parent) schema, and add the others to the schema Includes.", new object[]
							{
								this.xsiType.Namespace,
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name
							}));
						}
						XmlSchema xmlSchema = (XmlSchema)arrayList[0];
						if (xmlSchema == null)
						{
							throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[] { this.xsiType.Namespace }));
						}
						this.xsdType = (XmlSchemaType)xmlSchema.SchemaTypes[this.xsiType];
						if (this.xsdType == null)
						{
							throw new InvalidOperationException(Res.GetString("{0}.{1}() must return a valid type name. Type '{2}' cannot be found in the targetNamespace='{3}'.", new object[]
							{
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name,
								this.xsiType.Name,
								this.xsiType.Namespace
							}));
						}
						this.xsdType = ((this.xsdType.Redefined != null) ? this.xsdType.Redefined : this.xsdType);
						return;
					}
				}
				else
				{
					IXmlSerializable xmlSerializable = (IXmlSerializable)Activator.CreateInstance(this.type);
					this.schema = xmlSerializable.GetSchema();
					if (this.schema != null && (this.schema.Id == null || this.schema.Id.Length == 0))
					{
						throw new InvalidOperationException(Res.GetString("Schema Id is missing. The schema returned from {0}.GetSchema() must have an Id.", new object[] { this.type.FullName }));
					}
				}
			}
		}

		// Token: 0x04000874 RID: 2164
		private XmlSchema schema;

		// Token: 0x04000875 RID: 2165
		private Type type;

		// Token: 0x04000876 RID: 2166
		private bool needSchema = true;

		// Token: 0x04000877 RID: 2167
		private MethodInfo getSchemaMethod;

		// Token: 0x04000878 RID: 2168
		private XmlQualifiedName xsiType;

		// Token: 0x04000879 RID: 2169
		private XmlSchemaType xsdType;

		// Token: 0x0400087A RID: 2170
		private XmlSchemaSet schemas;

		// Token: 0x0400087B RID: 2171
		private bool any;

		// Token: 0x0400087C RID: 2172
		private string namespaces;

		// Token: 0x0400087D RID: 2173
		private SerializableMapping baseMapping;

		// Token: 0x0400087E RID: 2174
		private SerializableMapping derivedMappings;

		// Token: 0x0400087F RID: 2175
		private SerializableMapping nextDerivedMapping;

		// Token: 0x04000880 RID: 2176
		private SerializableMapping next;
	}
}
