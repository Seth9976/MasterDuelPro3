using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000297 RID: 663
	internal sealed class SchemaElementDecl : SchemaDeclBase, IDtdAttributeListInfo
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x000B28DC File Offset: 0x000B0ADC
		internal SchemaElementDecl()
		{
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000B28FA File Offset: 0x000B0AFA
		internal SchemaElementDecl(XmlSchemaDatatype dtype)
		{
			base.Datatype = dtype;
			this.contentValidator = ContentValidator.TextOnly;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000B292A File Offset: 0x000B0B2A
		internal SchemaElementDecl(XmlQualifiedName name, string prefix)
			: base(name, prefix)
		{
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000B294A File Offset: 0x000B0B4A
		internal static SchemaElementDecl CreateAnyTypeElementDecl()
		{
			return new SchemaElementDecl
			{
				Datatype = DatatypeImplementation.AnySimpleType.Datatype
			};
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000B2961 File Offset: 0x000B0B61
		bool IDtdAttributeListInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000B296C File Offset: 0x000B0B6C
		IDtdAttributeInfo IDtdAttributeListInfo.LookupAttribute(string prefix, string localName)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(localName, prefix);
			SchemaAttDef schemaAttDef;
			if (this.attdefs.TryGetValue(xmlQualifiedName, out schemaAttDef))
			{
				return schemaAttDef;
			}
			return null;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000B2994 File Offset: 0x000B0B94
		IEnumerable<IDtdDefaultAttributeInfo> IDtdAttributeListInfo.LookupDefaultAttributes()
		{
			return this.defaultAttdefs;
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000B299C File Offset: 0x000B0B9C
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x000B29A4 File Offset: 0x000B0BA4
		internal bool IsIdDeclared
		{
			get
			{
				return this.isIdDeclared;
			}
			set
			{
				this.isIdDeclared = value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x000B2961 File Offset: 0x000B0B61
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x000B29AD File Offset: 0x000B0BAD
		internal bool HasNonCDataAttribute
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
			set
			{
				this.hasNonCDataAttribute = value;
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000B29B6 File Offset: 0x000B0BB6
		internal SchemaElementDecl Clone()
		{
			return (SchemaElementDecl)base.MemberwiseClone();
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x000B29C3 File Offset: 0x000B0BC3
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x000B29CB File Offset: 0x000B0BCB
		internal bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x000B29D4 File Offset: 0x000B0BD4
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x000B29DC File Offset: 0x000B0BDC
		internal bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x000B29E5 File Offset: 0x000B0BE5
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x000B29ED File Offset: 0x000B0BED
		internal XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000B29F6 File Offset: 0x000B0BF6
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x000B29FE File Offset: 0x000B0BFE
		internal bool IsNotationDeclared
		{
			get
			{
				return this.isNotationDeclared;
			}
			set
			{
				this.isNotationDeclared = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x000B2A07 File Offset: 0x000B0C07
		internal bool HasDefaultAttribute
		{
			get
			{
				return this.defaultAttdefs != null;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000B2A12 File Offset: 0x000B0C12
		internal bool HasRequiredAttribute
		{
			get
			{
				return this.hasRequiredAttribute;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x000B2A1A File Offset: 0x000B0C1A
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x000B2A22 File Offset: 0x000B0C22
		internal ContentValidator ContentValidator
		{
			get
			{
				return this.contentValidator;
			}
			set
			{
				this.contentValidator = value;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000B2A2B File Offset: 0x000B0C2B
		// (set) Token: 0x06001E5A RID: 7770 RVA: 0x000B2A33 File Offset: 0x000B0C33
		internal XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x000B2A3C File Offset: 0x000B0C3C
		// (set) Token: 0x06001E5C RID: 7772 RVA: 0x000B2A44 File Offset: 0x000B0C44
		internal CompiledIdentityConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
			set
			{
				this.constraints = value;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x000B2A4D File Offset: 0x000B0C4D
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x000B2A55 File Offset: 0x000B0C55
		internal XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000B2A60 File Offset: 0x000B0C60
		internal void AddAttDef(SchemaAttDef attdef)
		{
			this.attdefs.Add(attdef.Name, attdef);
			if (attdef.Presence == SchemaDeclBase.Use.Required || attdef.Presence == SchemaDeclBase.Use.RequiredFixed)
			{
				this.hasRequiredAttribute = true;
			}
			if (attdef.Presence == SchemaDeclBase.Use.Default || attdef.Presence == SchemaDeclBase.Use.Fixed)
			{
				if (this.defaultAttdefs == null)
				{
					this.defaultAttdefs = new List<IDtdDefaultAttributeInfo>();
				}
				this.defaultAttdefs.Add(attdef);
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000B2AC8 File Offset: 0x000B0CC8
		internal SchemaAttDef GetAttDef(XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef;
			if (this.attdefs.TryGetValue(qname, out schemaAttDef))
			{
				return schemaAttDef;
			}
			return null;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000B2994 File Offset: 0x000B0B94
		internal IList<IDtdDefaultAttributeInfo> DefaultAttDefs
		{
			get
			{
				return this.defaultAttdefs;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x000B2AE8 File Offset: 0x000B0CE8
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttDefs
		{
			get
			{
				return this.attdefs;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x000B2AF0 File Offset: 0x000B0CF0
		internal Dictionary<XmlQualifiedName, XmlQualifiedName> ProhibitedAttributes
		{
			get
			{
				return this.prohibitedAttributes;
			}
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000B2AF8 File Offset: 0x000B0CF8
		internal void CheckAttributes(Hashtable presence, bool standalone)
		{
			foreach (SchemaAttDef schemaAttDef in this.attdefs.Values)
			{
				if (presence[schemaAttDef.Name] == null)
				{
					if (schemaAttDef.Presence == SchemaDeclBase.Use.Required)
					{
						throw new XmlSchemaException("The required attribute '{0}' is missing.", schemaAttDef.Name.ToString());
					}
					if (standalone && schemaAttDef.IsDeclaredInExternal && (schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed))
					{
						throw new XmlSchemaException("The standalone document declaration must have a value of 'no'.", string.Empty);
					}
				}
			}
		}

		// Token: 0x04000CFA RID: 3322
		private Dictionary<XmlQualifiedName, SchemaAttDef> attdefs = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x04000CFB RID: 3323
		private List<IDtdDefaultAttributeInfo> defaultAttdefs;

		// Token: 0x04000CFC RID: 3324
		private bool isIdDeclared;

		// Token: 0x04000CFD RID: 3325
		private bool hasNonCDataAttribute;

		// Token: 0x04000CFE RID: 3326
		private bool isAbstract;

		// Token: 0x04000CFF RID: 3327
		private bool isNillable;

		// Token: 0x04000D00 RID: 3328
		private bool hasRequiredAttribute;

		// Token: 0x04000D01 RID: 3329
		private bool isNotationDeclared;

		// Token: 0x04000D02 RID: 3330
		private Dictionary<XmlQualifiedName, XmlQualifiedName> prohibitedAttributes = new Dictionary<XmlQualifiedName, XmlQualifiedName>();

		// Token: 0x04000D03 RID: 3331
		private ContentValidator contentValidator;

		// Token: 0x04000D04 RID: 3332
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000D05 RID: 3333
		private XmlSchemaDerivationMethod block;

		// Token: 0x04000D06 RID: 3334
		private CompiledIdentityConstraint[] constraints;

		// Token: 0x04000D07 RID: 3335
		private XmlSchemaElement schemaElement;

		// Token: 0x04000D08 RID: 3336
		internal static readonly SchemaElementDecl Empty = new SchemaElementDecl();
	}
}
