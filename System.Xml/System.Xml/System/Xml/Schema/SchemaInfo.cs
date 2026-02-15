using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x0200029A RID: 666
	internal class SchemaInfo : IDtdInfo
	{
		// Token: 0x06001E8B RID: 7819 RVA: 0x000B2D7C File Offset: 0x000B0F7C
		internal SchemaInfo()
		{
			this.schemaType = SchemaType.None;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x000B2DE3 File Offset: 0x000B0FE3
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x000B2DEB File Offset: 0x000B0FEB
		public XmlQualifiedName DocTypeName
		{
			get
			{
				return this.docTypeName;
			}
			set
			{
				this.docTypeName = value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x000B2DF4 File Offset: 0x000B0FF4
		internal string InternalDtdSubset
		{
			set
			{
				this.internalDtdSubset = value;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000B2DFD File Offset: 0x000B0FFD
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDecls
		{
			get
			{
				return this.elementDecls;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000B2E05 File Offset: 0x000B1005
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> UndeclaredElementDecls
		{
			get
			{
				return this.undeclaredElementDecls;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000B2E0D File Offset: 0x000B100D
		internal Dictionary<XmlQualifiedName, SchemaEntity> GeneralEntities
		{
			get
			{
				if (this.generalEntities == null)
				{
					this.generalEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.generalEntities;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x000B2E28 File Offset: 0x000B1028
		internal Dictionary<XmlQualifiedName, SchemaEntity> ParameterEntities
		{
			get
			{
				if (this.parameterEntities == null)
				{
					this.parameterEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.parameterEntities;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x000B2E43 File Offset: 0x000B1043
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x000B2E4B File Offset: 0x000B104B
		internal SchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000B2E54 File Offset: 0x000B1054
		internal Dictionary<string, bool> TargetNamespaces
		{
			get
			{
				return this.targetNamespaces;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x000B2E5C File Offset: 0x000B105C
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDeclsByType
		{
			get
			{
				return this.elementDeclsByType;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x000B2E64 File Offset: 0x000B1064
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttributeDecls
		{
			get
			{
				return this.attributeDecls;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000B2E6C File Offset: 0x000B106C
		internal Dictionary<string, SchemaNotation> Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new Dictionary<string, SchemaNotation>();
				}
				return this.notations;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x000B2E87 File Offset: 0x000B1087
		// (set) Token: 0x06001E9A RID: 7834 RVA: 0x000B2E8F File Offset: 0x000B108F
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

		// Token: 0x06001E9B RID: 7835 RVA: 0x000B2E98 File Offset: 0x000B1098
		internal SchemaElementDecl GetElementDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl schemaElementDecl;
			if (this.elementDecls.TryGetValue(qname, out schemaElementDecl))
			{
				return schemaElementDecl;
			}
			return null;
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000B2EB8 File Offset: 0x000B10B8
		internal SchemaElementDecl GetTypeDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl schemaElementDecl;
			if (this.elementDeclsByType.TryGetValue(qname, out schemaElementDecl))
			{
				return schemaElementDecl;
			}
			return null;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000B2ED8 File Offset: 0x000B10D8
		internal XmlSchemaElement GetElement(XmlQualifiedName qname)
		{
			SchemaElementDecl elementDecl = this.GetElementDecl(qname);
			if (elementDecl != null)
			{
				return elementDecl.SchemaElement;
			}
			return null;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000B2EF8 File Offset: 0x000B10F8
		internal bool HasSchema(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000B2EF8 File Offset: 0x000B10F8
		internal bool Contains(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000B2F08 File Offset: 0x000B1108
		internal SchemaAttDef GetAttributeXdr(SchemaElementDecl ed, XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef = null;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef == null)
				{
					if (!ed.ContentValidator.IsOpen || qname.Namespace.Length == 0)
					{
						throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
					}
					if (!this.attributeDecls.TryGetValue(qname, out schemaAttDef) && this.targetNamespaces.ContainsKey(qname.Namespace))
					{
						throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
					}
				}
			}
			return schemaAttDef;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000B2F88 File Offset: 0x000B1188
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, XmlSchemaObject partialValidationType, out AttributeMatchState attributeMatchState)
		{
			SchemaAttDef schemaAttDef = null;
			attributeMatchState = AttributeMatchState.UndeclaredAttribute;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef != null)
				{
					attributeMatchState = AttributeMatchState.AttributeFound;
					return schemaAttDef;
				}
				XmlSchemaAnyAttribute anyAttribute = ed.AnyAttribute;
				if (anyAttribute != null)
				{
					if (!anyAttribute.NamespaceList.Allows(qname))
					{
						attributeMatchState = AttributeMatchState.ProhibitedAnyAttribute;
					}
					else if (anyAttribute.ProcessContentsCorrect != XmlSchemaContentProcessing.Skip)
					{
						if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
						{
							if (schemaAttDef.Datatype.TypeCode == XmlTypeCode.Id)
							{
								attributeMatchState = AttributeMatchState.AnyIdAttributeFound;
							}
							else
							{
								attributeMatchState = AttributeMatchState.AttributeFound;
							}
						}
						else if (anyAttribute.ProcessContentsCorrect == XmlSchemaContentProcessing.Lax)
						{
							attributeMatchState = AttributeMatchState.AnyAttributeLax;
						}
					}
					else
					{
						attributeMatchState = AttributeMatchState.AnyAttributeSkip;
					}
				}
				else if (ed.ProhibitedAttributes.ContainsKey(qname))
				{
					attributeMatchState = AttributeMatchState.ProhibitedAttribute;
				}
			}
			else if (partialValidationType != null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = partialValidationType as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (qname.Equals(xmlSchemaAttribute.QualifiedName))
					{
						schemaAttDef = xmlSchemaAttribute.AttDef;
						attributeMatchState = AttributeMatchState.AttributeFound;
					}
					else
					{
						attributeMatchState = AttributeMatchState.AttributeNameMismatch;
					}
				}
				else
				{
					attributeMatchState = AttributeMatchState.ValidateAttributeInvalidCall;
				}
			}
			else if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
			{
				attributeMatchState = AttributeMatchState.AttributeFound;
			}
			else
			{
				attributeMatchState = AttributeMatchState.UndeclaredElementAndAttribute;
			}
			return schemaAttDef;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000B3080 File Offset: 0x000B1280
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, ref bool skip)
		{
			AttributeMatchState attributeMatchState;
			SchemaAttDef attributeXsd = this.GetAttributeXsd(ed, qname, null, out attributeMatchState);
			switch (attributeMatchState)
			{
			case AttributeMatchState.UndeclaredAttribute:
				throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
			case AttributeMatchState.AnyAttributeSkip:
				skip = true;
				break;
			case AttributeMatchState.ProhibitedAnyAttribute:
			case AttributeMatchState.ProhibitedAttribute:
				throw new XmlSchemaException("The '{0}' attribute is not allowed.", qname.ToString());
			}
			return attributeXsd;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000B30E8 File Offset: 0x000B12E8
		internal void Add(SchemaInfo sinfo, ValidationEventHandler eventhandler)
		{
			if (this.schemaType == SchemaType.None)
			{
				this.schemaType = sinfo.SchemaType;
			}
			else if (this.schemaType != sinfo.SchemaType)
			{
				if (eventhandler != null)
				{
					eventhandler(this, new ValidationEventArgs(new XmlSchemaException("Different schema types cannot be mixed.", string.Empty)));
				}
				return;
			}
			foreach (string text in sinfo.TargetNamespaces.Keys)
			{
				if (!this.targetNamespaces.ContainsKey(text))
				{
					this.targetNamespaces.Add(text, true);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair in sinfo.elementDecls)
			{
				if (!this.elementDecls.ContainsKey(keyValuePair.Key))
				{
					this.elementDecls.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair2 in sinfo.elementDeclsByType)
			{
				if (!this.elementDeclsByType.ContainsKey(keyValuePair2.Key))
				{
					this.elementDeclsByType.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			foreach (SchemaAttDef schemaAttDef in sinfo.AttributeDecls.Values)
			{
				if (!this.attributeDecls.ContainsKey(schemaAttDef.Name))
				{
					this.attributeDecls.Add(schemaAttDef.Name, schemaAttDef);
				}
			}
			foreach (SchemaNotation schemaNotation in sinfo.Notations.Values)
			{
				if (!this.Notations.ContainsKey(schemaNotation.Name.Name))
				{
					this.Notations.Add(schemaNotation.Name.Name, schemaNotation);
				}
			}
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000B3348 File Offset: 0x000B1548
		internal void Finish()
		{
			Dictionary<XmlQualifiedName, SchemaElementDecl> dictionary = this.elementDecls;
			for (int i = 0; i < 2; i++)
			{
				foreach (SchemaElementDecl schemaElementDecl in dictionary.Values)
				{
					if (schemaElementDecl.HasNonCDataAttribute)
					{
						this.hasNonCDataAttributes = true;
					}
					if (schemaElementDecl.DefaultAttDefs != null)
					{
						this.hasDefaultAttributes = true;
					}
				}
				dictionary = this.undeclaredElementDecls;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x000B33CC File Offset: 0x000B15CC
		bool IDtdInfo.HasDefaultAttributes
		{
			get
			{
				return this.hasDefaultAttributes;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x000B33D4 File Offset: 0x000B15D4
		bool IDtdInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttributes;
			}
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000B33DC File Offset: 0x000B15DC
		IDtdAttributeListInfo IDtdInfo.LookupAttributeList(string prefix, string localName)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(prefix, localName);
			SchemaElementDecl schemaElementDecl;
			if (!this.elementDecls.TryGetValue(xmlQualifiedName, out schemaElementDecl))
			{
				this.undeclaredElementDecls.TryGetValue(xmlQualifiedName, out schemaElementDecl);
			}
			return schemaElementDecl;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000B3414 File Offset: 0x000B1614
		IDtdEntityInfo IDtdInfo.LookupEntity(string name)
		{
			if (this.generalEntities == null)
			{
				return null;
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name);
			SchemaEntity schemaEntity;
			if (this.generalEntities.TryGetValue(xmlQualifiedName, out schemaEntity))
			{
				return schemaEntity;
			}
			return null;
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x000B2DE3 File Offset: 0x000B0FE3
		XmlQualifiedName IDtdInfo.Name
		{
			get
			{
				return this.docTypeName;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x000B3445 File Offset: 0x000B1645
		string IDtdInfo.InternalDtdSubset
		{
			get
			{
				return this.internalDtdSubset;
			}
		}

		// Token: 0x04000D21 RID: 3361
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04000D22 RID: 3362
		private Dictionary<XmlQualifiedName, SchemaElementDecl> undeclaredElementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04000D23 RID: 3363
		private Dictionary<XmlQualifiedName, SchemaEntity> generalEntities;

		// Token: 0x04000D24 RID: 3364
		private Dictionary<XmlQualifiedName, SchemaEntity> parameterEntities;

		// Token: 0x04000D25 RID: 3365
		private XmlQualifiedName docTypeName = XmlQualifiedName.Empty;

		// Token: 0x04000D26 RID: 3366
		private string internalDtdSubset = string.Empty;

		// Token: 0x04000D27 RID: 3367
		private bool hasNonCDataAttributes;

		// Token: 0x04000D28 RID: 3368
		private bool hasDefaultAttributes;

		// Token: 0x04000D29 RID: 3369
		private Dictionary<string, bool> targetNamespaces = new Dictionary<string, bool>();

		// Token: 0x04000D2A RID: 3370
		private Dictionary<XmlQualifiedName, SchemaAttDef> attributeDecls = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x04000D2B RID: 3371
		private int errorCount;

		// Token: 0x04000D2C RID: 3372
		private SchemaType schemaType;

		// Token: 0x04000D2D RID: 3373
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDeclsByType = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04000D2E RID: 3374
		private Dictionary<string, SchemaNotation> notations;
	}
}
