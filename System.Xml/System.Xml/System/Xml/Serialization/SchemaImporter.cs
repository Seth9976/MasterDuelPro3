using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Configuration;
using System.Xml.Serialization.Advanced;
using System.Xml.Serialization.Configuration;
using Microsoft.CSharp;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Describes a schema importer.</summary>
	// Token: 0x02000181 RID: 385
	public abstract class SchemaImporter
	{
		// Token: 0x06001233 RID: 4659 RVA: 0x00056F94 File Offset: 0x00055194
		internal SchemaImporter(XmlSchemas schemas, CodeGenerationOptions options, CodeDomProvider codeProvider, ImportContext context)
		{
			if (!schemas.Contains("http://www.w3.org/2001/XMLSchema"))
			{
				schemas.AddReference(XmlSchemas.XsdSchema);
				schemas.SchemaSet.Add(XmlSchemas.XsdSchema);
			}
			if (!schemas.Contains("http://www.w3.org/XML/1998/namespace"))
			{
				schemas.AddReference(XmlSchemas.XmlSchema);
				schemas.SchemaSet.Add(XmlSchemas.XmlSchema);
			}
			this.schemas = schemas;
			this.options = options;
			this.codeProvider = codeProvider;
			this.context = context;
			this.Schemas.SetCache(this.Context.Cache, this.Context.ShareTypes);
			SchemaImporterExtensionsSection schemaImporterExtensionsSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SchemaImporterExtensionsSectionPath) as SchemaImporterExtensionsSection;
			if (schemaImporterExtensionsSection != null)
			{
				this.extensions = schemaImporterExtensionsSection.SchemaImporterExtensionsInternal;
				return;
			}
			this.extensions = new SchemaImporterExtensionCollection();
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00057062 File Offset: 0x00055262
		internal ImportContext Context
		{
			get
			{
				if (this.context == null)
				{
					this.context = new ImportContext();
				}
				return this.context;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0005707D File Offset: 0x0005527D
		internal CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		/// <summary>Gets a collection of schema importer extensions.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection" /> containing a collection of schema importer extensions.</returns>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00057098 File Offset: 0x00055298
		public SchemaImporterExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new SchemaImporterExtensionCollection();
				}
				return this.extensions;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x000570B3 File Offset: 0x000552B3
		internal Hashtable ImportedElements
		{
			get
			{
				return this.Context.Elements;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x000570C0 File Offset: 0x000552C0
		internal Hashtable ImportedMappings
		{
			get
			{
				return this.Context.Mappings;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x000570CD File Offset: 0x000552CD
		internal CodeIdentifiers TypeIdentifiers
		{
			get
			{
				return this.Context.TypeIdentifiers;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x000570DA File Offset: 0x000552DA
		internal XmlSchemas Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemas();
				}
				return this.schemas;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x000570F5 File Offset: 0x000552F5
		internal TypeScope Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new TypeScope();
				}
				return this.scope;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00057110 File Offset: 0x00055310
		internal NameTable GroupsInUse
		{
			get
			{
				if (this.groupsInUse == null)
				{
					this.groupsInUse = new NameTable();
				}
				return this.groupsInUse;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0005712B File Offset: 0x0005532B
		internal NameTable TypesInUse
		{
			get
			{
				if (this.typesInUse == null)
				{
					this.typesInUse = new NameTable();
				}
				return this.typesInUse;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00057146 File Offset: 0x00055346
		internal CodeGenerationOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00057150 File Offset: 0x00055350
		internal void MakeDerived(StructMapping structMapping, Type baseType, bool baseTypeCanBeIndirect)
		{
			structMapping.ReferencedByTopLevelElement = true;
			if (baseType != null)
			{
				TypeDesc typeDesc = this.Scope.GetTypeDesc(baseType);
				if (typeDesc != null)
				{
					TypeDesc typeDesc2 = structMapping.TypeDesc;
					if (baseTypeCanBeIndirect)
					{
						while (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
						{
							typeDesc2 = typeDesc2.BaseTypeDesc;
						}
					}
					if (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
					{
						throw new InvalidOperationException(Res.GetString("Type {0} cannot derive from {1} because it already has base type {2}.", new object[]
						{
							structMapping.TypeDesc.FullName,
							baseType.FullName,
							typeDesc2.BaseTypeDesc.FullName
						}));
					}
					typeDesc2.BaseTypeDesc = typeDesc;
				}
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000571F7 File Offset: 0x000553F7
		internal string GenerateUniqueTypeName(string typeName)
		{
			typeName = CodeIdentifier.MakeValid(typeName);
			return this.TypeIdentifiers.AddUnique(typeName, typeName);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00057210 File Offset: 0x00055410
		private StructMapping CreateRootMapping()
		{
			TypeDesc typeDesc = this.Scope.GetTypeDesc(typeof(object));
			return new StructMapping
			{
				TypeDesc = typeDesc,
				Members = new MemberMapping[0],
				IncludeInSchema = false,
				TypeName = "anyType",
				Namespace = "http://www.w3.org/2001/XMLSchema"
			};
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00057268 File Offset: 0x00055468
		internal StructMapping GetRootMapping()
		{
			if (this.root == null)
			{
				this.root = this.CreateRootMapping();
			}
			return this.root;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00057284 File Offset: 0x00055484
		internal StructMapping ImportRootMapping()
		{
			if (!this.rootImported)
			{
				this.rootImported = true;
				this.ImportDerivedTypes(XmlQualifiedName.Empty);
			}
			return this.GetRootMapping();
		}

		// Token: 0x06001244 RID: 4676
		internal abstract void ImportDerivedTypes(XmlQualifiedName baseName);

		// Token: 0x06001245 RID: 4677 RVA: 0x000572A8 File Offset: 0x000554A8
		internal void AddReference(XmlQualifiedName name, NameTable references, string error)
		{
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return;
			}
			if (references[name] != null)
			{
				throw new InvalidOperationException(Res.GetString(error, new object[] { name.Name, name.Namespace }));
			}
			references[name] = name;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000572FD File Offset: 0x000554FD
		internal void RemoveReference(XmlQualifiedName name, NameTable references)
		{
			references[name] = null;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00057307 File Offset: 0x00055507
		internal void AddReservedIdentifiersForDataBinding(CodeIdentifiers scope)
		{
			if ((this.options & CodeGenerationOptions.EnableDataBinding) != CodeGenerationOptions.None)
			{
				scope.AddReserved(CodeExporter.PropertyChangedEvent.Name);
				scope.AddReserved(CodeExporter.RaisePropertyChangedEventMethod.Name);
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal SchemaImporter()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040008AF RID: 2223
		private XmlSchemas schemas;

		// Token: 0x040008B0 RID: 2224
		private StructMapping root;

		// Token: 0x040008B1 RID: 2225
		private CodeGenerationOptions options;

		// Token: 0x040008B2 RID: 2226
		private CodeDomProvider codeProvider;

		// Token: 0x040008B3 RID: 2227
		private TypeScope scope;

		// Token: 0x040008B4 RID: 2228
		private ImportContext context;

		// Token: 0x040008B5 RID: 2229
		private bool rootImported;

		// Token: 0x040008B6 RID: 2230
		private NameTable typesInUse;

		// Token: 0x040008B7 RID: 2231
		private NameTable groupsInUse;

		// Token: 0x040008B8 RID: 2232
		private SchemaImporterExtensionCollection extensions;
	}
}
