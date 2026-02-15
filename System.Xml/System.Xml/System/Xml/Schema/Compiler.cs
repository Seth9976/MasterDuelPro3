using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x0200029F RID: 671
	internal sealed class Compiler : BaseProcessor
	{
		// Token: 0x06001EB9 RID: 7865 RVA: 0x000B4AA4 File Offset: 0x000B2CA4
		public Compiler(XmlNameTable nameTable, ValidationEventHandler eventHandler, XmlSchema schemaForSchema, XmlSchemaCompilationSettings compilationSettings)
			: base(nameTable, null, eventHandler, compilationSettings)
		{
			this.schemaForSchema = schemaForSchema;
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000B4B3C File Offset: 0x000B2D3C
		public bool Execute(XmlSchemaSet schemaSet, SchemaInfo schemaCompiledInfo)
		{
			this.Compile();
			if (!base.HasErrors)
			{
				this.Output(schemaCompiledInfo);
				schemaSet.elements = this.elements;
				schemaSet.attributes = this.attributes;
				schemaSet.schemaTypes = this.schemaTypes;
				schemaSet.substitutionGroups = this.examplars;
			}
			return !base.HasErrors;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000B4B98 File Offset: 0x000B2D98
		internal void Prepare(XmlSchema schema, bool cleanup)
		{
			if (this.schemasToCompile[schema] != null)
			{
				return;
			}
			this.schemasToCompile.Add(schema, schema);
			foreach (object obj in schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (cleanup)
				{
					this.CleanupElement(xmlSchemaElement);
				}
				base.AddToTable(this.elements, xmlSchemaElement.QualifiedName, xmlSchemaElement);
			}
			foreach (object obj2 in schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				if (cleanup)
				{
					this.CleanupAttribute(xmlSchemaAttribute);
				}
				base.AddToTable(this.attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
			}
			foreach (object obj3 in schema.Groups.Values)
			{
				XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)obj3;
				if (cleanup)
				{
					this.CleanupGroup(xmlSchemaGroup);
				}
				base.AddToTable(this.groups, xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
			}
			foreach (object obj4 in schema.AttributeGroups.Values)
			{
				XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)obj4;
				if (cleanup)
				{
					this.CleanupAttributeGroup(xmlSchemaAttributeGroup);
				}
				base.AddToTable(this.attributeGroups, xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
			}
			foreach (object obj5 in schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj5;
				if (cleanup)
				{
					XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType != null)
					{
						this.CleanupComplexType(xmlSchemaComplexType);
					}
					else
					{
						this.CleanupSimpleType(xmlSchemaType as XmlSchemaSimpleType);
					}
				}
				base.AddToTable(this.schemaTypes, xmlSchemaType.QualifiedName, xmlSchemaType);
			}
			foreach (object obj6 in schema.Notations.Values)
			{
				XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)obj6;
				base.AddToTable(this.notations, xmlSchemaNotation.QualifiedName, xmlSchemaNotation);
			}
			foreach (object obj7 in schema.IdentityConstraints.Values)
			{
				XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)obj7;
				base.AddToTable(this.identityConstraints, xmlSchemaIdentityConstraint.QualifiedName, xmlSchemaIdentityConstraint);
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000B4E94 File Offset: 0x000B3094
		private void UpdateSForSSimpleTypes()
		{
			XmlSchemaSimpleType[] builtInTypes = DatatypeImplementation.GetBuiltInTypes();
			int num = builtInTypes.Length - 3;
			for (int i = 12; i < num; i++)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = builtInTypes[i];
				this.schemaForSchema.SchemaTypes.Replace(xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
				this.schemaTypes.Replace(xmlSchemaSimpleType.QualifiedName, xmlSchemaSimpleType);
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000B4EE8 File Offset: 0x000B30E8
		private void Output(SchemaInfo schemaInfo)
		{
			foreach (object obj in this.schemasToCompile.Values)
			{
				string text = ((XmlSchema)obj).TargetNamespace;
				if (text == null)
				{
					text = string.Empty;
				}
				schemaInfo.TargetNamespaces[text] = true;
			}
			foreach (object obj2 in this.elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj2;
				schemaInfo.ElementDecls.Add(xmlSchemaElement.QualifiedName, xmlSchemaElement.ElementDecl);
			}
			foreach (object obj3 in this.attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj3;
				schemaInfo.AttributeDecls.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute.AttDef);
			}
			foreach (object obj4 in this.schemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj4;
				schemaInfo.ElementDeclsByType.Add(xmlSchemaType.QualifiedName, xmlSchemaType.ElementDecl);
			}
			foreach (object obj5 in this.notations.Values)
			{
				XmlSchemaNotation xmlSchemaNotation = (XmlSchemaNotation)obj5;
				SchemaNotation schemaNotation = new SchemaNotation(xmlSchemaNotation.QualifiedName);
				schemaNotation.SystemLiteral = xmlSchemaNotation.System;
				schemaNotation.Pubid = xmlSchemaNotation.Public;
				if (!schemaInfo.Notations.ContainsKey(schemaNotation.Name.Name))
				{
					schemaInfo.Notations.Add(schemaNotation.Name.Name, schemaNotation);
				}
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000B511C File Offset: 0x000B331C
		internal void ImportAllCompiledSchemas(XmlSchemaSet schemaSet)
		{
			SortedList sortedSchemas = schemaSet.SortedSchemas;
			for (int i = 0; i < sortedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)sortedSchemas.GetByIndex(i);
				if (xmlSchema.IsCompiledBySet)
				{
					this.Prepare(xmlSchema, false);
				}
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000B5160 File Offset: 0x000B3360
		internal bool Compile()
		{
			this.schemaTypes.Insert(DatatypeImplementation.QnAnyType, XmlSchemaComplexType.AnyType);
			if (this.schemaForSchema != null)
			{
				this.schemaForSchema.SchemaTypes.Replace(DatatypeImplementation.QnAnyType, XmlSchemaComplexType.AnyType);
				this.UpdateSForSSimpleTypes();
			}
			foreach (object obj in this.groups.Values)
			{
				XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)obj;
				this.CompileGroup(xmlSchemaGroup);
			}
			foreach (object obj2 in this.attributeGroups.Values)
			{
				XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)obj2;
				this.CompileAttributeGroup(xmlSchemaAttributeGroup);
			}
			foreach (object obj3 in this.schemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					this.CompileComplexType(xmlSchemaComplexType);
				}
				else
				{
					this.CompileSimpleType((XmlSchemaSimpleType)xmlSchemaType);
				}
			}
			foreach (object obj4 in this.elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj4;
				if (xmlSchemaElement.ElementDecl == null)
				{
					this.CompileElement(xmlSchemaElement);
				}
			}
			foreach (object obj5 in this.attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj5;
				if (xmlSchemaAttribute.AttDef == null)
				{
					this.CompileAttribute(xmlSchemaAttribute);
				}
			}
			using (IEnumerator enumerator = this.identityConstraints.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj6 = enumerator.Current;
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)obj6;
					if (xmlSchemaIdentityConstraint.CompiledConstraint == null)
					{
						this.CompileIdentityConstraint(xmlSchemaIdentityConstraint);
					}
				}
				goto IL_021C;
			}
			IL_0202:
			XmlSchemaComplexType xmlSchemaComplexType2 = (XmlSchemaComplexType)this.complexTypeStack.Pop();
			this.CompileComplexTypeElements(xmlSchemaComplexType2);
			IL_021C:
			if (this.complexTypeStack.Count <= 0)
			{
				this.ProcessSubstitutionGroups();
				foreach (object obj7 in this.schemaTypes.Values)
				{
					XmlSchemaComplexType xmlSchemaComplexType3 = ((XmlSchemaType)obj7) as XmlSchemaComplexType;
					if (xmlSchemaComplexType3 != null)
					{
						this.CheckParticleDerivation(xmlSchemaComplexType3);
					}
				}
				foreach (object obj8 in this.elements.Values)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)obj8;
					XmlSchemaComplexType xmlSchemaComplexType4 = xmlSchemaElement2.ElementSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType4 != null && xmlSchemaElement2.SchemaTypeName == XmlQualifiedName.Empty)
					{
						this.CheckParticleDerivation(xmlSchemaComplexType4);
					}
				}
				foreach (object obj9 in this.groups.Values)
				{
					XmlSchemaGroup xmlSchemaGroup2 = (XmlSchemaGroup)obj9;
					XmlSchemaGroup redefined = xmlSchemaGroup2.Redefined;
					if (redefined != null)
					{
						this.RecursivelyCheckRedefinedGroups(xmlSchemaGroup2, redefined);
					}
				}
				foreach (object obj10 in this.attributeGroups.Values)
				{
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup2 = (XmlSchemaAttributeGroup)obj10;
					XmlSchemaAttributeGroup redefined2 = xmlSchemaAttributeGroup2.Redefined;
					if (redefined2 != null)
					{
						this.RecursivelyCheckRedefinedAttributeGroups(xmlSchemaAttributeGroup2, redefined2);
					}
				}
				return !base.HasErrors;
			}
			goto IL_0202;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000B557C File Offset: 0x000B377C
		private void CleanupAttribute(XmlSchemaAttribute attribute)
		{
			if (attribute.SchemaType != null)
			{
				this.CleanupSimpleType(attribute.SchemaType);
			}
			attribute.AttDef = null;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000B5599 File Offset: 0x000B3799
		private void CleanupAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			this.CleanupAttributes(attributeGroup.Attributes);
			attributeGroup.AttributeUses.Clear();
			attributeGroup.AttributeWildcard = null;
			if (attributeGroup.Redefined != null)
			{
				this.CleanupAttributeGroup(attributeGroup.Redefined);
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000B55D0 File Offset: 0x000B37D0
		private void CleanupComplexType(XmlSchemaComplexType complexType)
		{
			if (complexType.QualifiedName == DatatypeImplementation.QnAnyType)
			{
				return;
			}
			if (complexType.ContentModel != null)
			{
				if (complexType.ContentModel is XmlSchemaSimpleContent)
				{
					XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)complexType.ContentModel;
					if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentExtension)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content;
						this.CleanupAttributes(xmlSchemaSimpleContentExtension.Attributes);
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content;
						this.CleanupAttributes(xmlSchemaSimpleContentRestriction.Attributes);
					}
				}
				else
				{
					XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)complexType.ContentModel;
					if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentExtension)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content;
						this.CleanupParticle(xmlSchemaComplexContentExtension.Particle);
						this.CleanupAttributes(xmlSchemaComplexContentExtension.Attributes);
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = (XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content;
						this.CleanupParticle(xmlSchemaComplexContentRestriction.Particle);
						this.CleanupAttributes(xmlSchemaComplexContentRestriction.Attributes);
					}
				}
			}
			else
			{
				this.CleanupParticle(complexType.Particle);
				this.CleanupAttributes(complexType.Attributes);
			}
			complexType.LocalElements.Clear();
			complexType.AttributeUses.Clear();
			complexType.SetAttributeWildcard(null);
			complexType.SetContentTypeParticle(XmlSchemaParticle.Empty);
			complexType.ElementDecl = null;
			complexType.HasWildCard = false;
			if (complexType.Redefined != null)
			{
				this.CleanupComplexType(complexType.Redefined as XmlSchemaComplexType);
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000B572D File Offset: 0x000B392D
		private void CleanupSimpleType(XmlSchemaSimpleType simpleType)
		{
			if (simpleType == XmlSchemaType.GetBuiltInSimpleType(simpleType.TypeCode))
			{
				return;
			}
			simpleType.ElementDecl = null;
			if (simpleType.Redefined != null)
			{
				this.CleanupSimpleType(simpleType.Redefined as XmlSchemaSimpleType);
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000B5760 File Offset: 0x000B3960
		private void CleanupElement(XmlSchemaElement element)
		{
			if (element.SchemaType != null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = element.SchemaType as XmlSchemaComplexType;
				if (xmlSchemaComplexType != null)
				{
					this.CleanupComplexType(xmlSchemaComplexType);
				}
				else
				{
					this.CleanupSimpleType((XmlSchemaSimpleType)element.SchemaType);
				}
			}
			for (int i = 0; i < element.Constraints.Count; i++)
			{
				((XmlSchemaIdentityConstraint)element.Constraints[i]).CompiledConstraint = null;
			}
			element.ElementDecl = null;
			element.IsLocalTypeDerivationChecked = false;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000B57DC File Offset: 0x000B39DC
		private void CleanupAttributes(XmlSchemaObjectCollection attributes)
		{
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					this.CleanupAttribute(xmlSchemaAttribute);
				}
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000B5811 File Offset: 0x000B3A11
		private void CleanupGroup(XmlSchemaGroup group)
		{
			this.CleanupParticle(group.Particle);
			group.CanonicalParticle = null;
			if (group.Redefined != null)
			{
				this.CleanupGroup(group.Redefined);
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000B583C File Offset: 0x000B3A3C
		private void CleanupParticle(XmlSchemaParticle particle)
		{
			XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				this.CleanupElement(xmlSchemaElement);
				return;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase = particle as XmlSchemaGroupBase;
			if (xmlSchemaGroupBase != null)
			{
				for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
				{
					this.CleanupParticle((XmlSchemaParticle)xmlSchemaGroupBase.Items[i]);
				}
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000B5894 File Offset: 0x000B3A94
		private void ProcessSubstitutionGroups()
		{
			foreach (object obj in this.elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (!xmlSchemaElement.SubstitutionGroup.IsEmpty)
				{
					XmlSchemaElement xmlSchemaElement2 = this.elements[xmlSchemaElement.SubstitutionGroup] as XmlSchemaElement;
					if (xmlSchemaElement2 == null)
					{
						base.SendValidationEvent("Reference to undeclared substitution group affiliation.", xmlSchemaElement);
					}
					else
					{
						if (!XmlSchemaType.IsDerivedFrom(xmlSchemaElement.ElementSchemaType, xmlSchemaElement2.ElementSchemaType, xmlSchemaElement2.FinalResolved))
						{
							base.SendValidationEvent("'{0}' cannot be a member of substitution group with head element '{1}'.", xmlSchemaElement.QualifiedName.ToString(), xmlSchemaElement2.QualifiedName.ToString(), xmlSchemaElement);
						}
						XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)this.examplars[xmlSchemaElement.SubstitutionGroup];
						if (xmlSchemaSubstitutionGroup == null)
						{
							xmlSchemaSubstitutionGroup = new XmlSchemaSubstitutionGroup();
							xmlSchemaSubstitutionGroup.Examplar = xmlSchemaElement.SubstitutionGroup;
							this.examplars.Add(xmlSchemaElement.SubstitutionGroup, xmlSchemaSubstitutionGroup);
						}
						ArrayList members = xmlSchemaSubstitutionGroup.Members;
						if (!members.Contains(xmlSchemaElement))
						{
							members.Add(xmlSchemaElement);
						}
					}
				}
			}
			foreach (object obj2 in this.examplars.Values)
			{
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup2 = (XmlSchemaSubstitutionGroup)obj2;
				this.CompileSubstitutionGroup(xmlSchemaSubstitutionGroup2);
			}
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000B5A18 File Offset: 0x000B3C18
		private void CompileSubstitutionGroup(XmlSchemaSubstitutionGroup substitutionGroup)
		{
			if (substitutionGroup.IsProcessing && substitutionGroup.Members.Count > 0)
			{
				base.SendValidationEvent("Circular substitution group affiliation.", (XmlSchemaElement)substitutionGroup.Members[0]);
				return;
			}
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.elements[substitutionGroup.Examplar];
			if (substitutionGroup.Members.Contains(xmlSchemaElement))
			{
				return;
			}
			substitutionGroup.IsProcessing = true;
			try
			{
				if (xmlSchemaElement.FinalResolved == XmlSchemaDerivationMethod.All)
				{
					base.SendValidationEvent("Cannot be nominated as the {substitution group affiliation} of any other declaration.", xmlSchemaElement);
				}
				ArrayList arrayList = null;
				for (int i = 0; i < substitutionGroup.Members.Count; i++)
				{
					XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)substitutionGroup.Members[i];
					if ((xmlSchemaElement2.ElementDecl.Block & XmlSchemaDerivationMethod.Substitution) == XmlSchemaDerivationMethod.Empty)
					{
						XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)this.examplars[xmlSchemaElement2.QualifiedName];
						if (xmlSchemaSubstitutionGroup != null)
						{
							this.CompileSubstitutionGroup(xmlSchemaSubstitutionGroup);
							for (int j = 0; j < xmlSchemaSubstitutionGroup.Members.Count; j++)
							{
								if (xmlSchemaSubstitutionGroup.Members[j] != xmlSchemaElement2)
								{
									if (arrayList == null)
									{
										arrayList = new ArrayList();
									}
									arrayList.Add(xmlSchemaSubstitutionGroup.Members[j]);
								}
							}
						}
					}
				}
				if (arrayList != null)
				{
					for (int k = 0; k < arrayList.Count; k++)
					{
						substitutionGroup.Members.Add(arrayList[k]);
					}
				}
				substitutionGroup.Members.Add(xmlSchemaElement);
			}
			finally
			{
				substitutionGroup.IsProcessing = false;
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x000B5BAC File Offset: 0x000B3DAC
		private void RecursivelyCheckRedefinedGroups(XmlSchemaGroup redefinedGroup, XmlSchemaGroup baseGroup)
		{
			if (baseGroup.Redefined != null)
			{
				this.RecursivelyCheckRedefinedGroups(baseGroup, baseGroup.Redefined);
			}
			if (redefinedGroup.SelfReferenceCount == 0)
			{
				if (baseGroup.CanonicalParticle == null)
				{
					baseGroup.CanonicalParticle = this.CannonicalizeParticle(baseGroup.Particle, true);
				}
				if (redefinedGroup.CanonicalParticle == null)
				{
					redefinedGroup.CanonicalParticle = this.CannonicalizeParticle(redefinedGroup.Particle, true);
				}
				this.CompileParticleElements(redefinedGroup.CanonicalParticle);
				this.CompileParticleElements(baseGroup.CanonicalParticle);
				this.CheckParticleDerivation(redefinedGroup.CanonicalParticle, baseGroup.CanonicalParticle);
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000B5C36 File Offset: 0x000B3E36
		private void RecursivelyCheckRedefinedAttributeGroups(XmlSchemaAttributeGroup attributeGroup, XmlSchemaAttributeGroup baseAttributeGroup)
		{
			if (baseAttributeGroup.Redefined != null)
			{
				this.RecursivelyCheckRedefinedAttributeGroups(baseAttributeGroup, baseAttributeGroup.Redefined);
			}
			if (attributeGroup.SelfReferenceCount == 0)
			{
				this.CompileAttributeGroup(baseAttributeGroup);
				this.CompileAttributeGroup(attributeGroup);
				this.CheckAtrributeGroupRestriction(baseAttributeGroup, attributeGroup);
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000B5C6C File Offset: 0x000B3E6C
		private void CompileGroup(XmlSchemaGroup group)
		{
			if (group.IsProcessing)
			{
				base.SendValidationEvent("Circular group reference.", group);
				group.CanonicalParticle = XmlSchemaParticle.Empty;
				return;
			}
			group.IsProcessing = true;
			if (group.CanonicalParticle == null)
			{
				group.CanonicalParticle = this.CannonicalizeParticle(group.Particle, true);
			}
			group.IsProcessing = false;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000B5CC4 File Offset: 0x000B3EC4
		private void CompileSimpleType(XmlSchemaSimpleType simpleType)
		{
			if (simpleType.IsProcessing)
			{
				throw new XmlSchemaException("Circular type reference.", simpleType);
			}
			if (simpleType.ElementDecl != null)
			{
				return;
			}
			simpleType.IsProcessing = true;
			try
			{
				if (simpleType.Content is XmlSchemaSimpleTypeList)
				{
					XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)simpleType.Content;
					simpleType.SetBaseSchemaType(DatatypeImplementation.AnySimpleType);
					XmlSchemaDatatype xmlSchemaDatatype;
					if (xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
					{
						this.CompileSimpleType(xmlSchemaSimpleTypeList.ItemType);
						xmlSchemaSimpleTypeList.BaseItemType = xmlSchemaSimpleTypeList.ItemType;
						xmlSchemaDatatype = xmlSchemaSimpleTypeList.ItemType.Datatype;
					}
					else
					{
						XmlSchemaSimpleType simpleType2 = this.GetSimpleType(xmlSchemaSimpleTypeList.ItemTypeName);
						if (simpleType2 == null)
						{
							throw new XmlSchemaException("Type '{0}' is not declared, or is not a simple type.", xmlSchemaSimpleTypeList.ItemTypeName.ToString(), xmlSchemaSimpleTypeList);
						}
						if ((simpleType2.FinalResolved & XmlSchemaDerivationMethod.List) != XmlSchemaDerivationMethod.Empty)
						{
							base.SendValidationEvent("The base type is the final list.", simpleType);
						}
						xmlSchemaSimpleTypeList.BaseItemType = simpleType2;
						xmlSchemaDatatype = simpleType2.Datatype;
					}
					simpleType.SetDatatype(xmlSchemaDatatype.DeriveByList(simpleType));
					simpleType.SetDerivedBy(XmlSchemaDerivationMethod.List);
				}
				else if (simpleType.Content is XmlSchemaSimpleTypeRestriction)
				{
					XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)simpleType.Content;
					XmlSchemaDatatype xmlSchemaDatatype2;
					if (xmlSchemaSimpleTypeRestriction.BaseTypeName.IsEmpty)
					{
						this.CompileSimpleType(xmlSchemaSimpleTypeRestriction.BaseType);
						simpleType.SetBaseSchemaType(xmlSchemaSimpleTypeRestriction.BaseType);
						xmlSchemaDatatype2 = xmlSchemaSimpleTypeRestriction.BaseType.Datatype;
					}
					else if (simpleType.Redefined != null && xmlSchemaSimpleTypeRestriction.BaseTypeName == simpleType.Redefined.QualifiedName)
					{
						this.CompileSimpleType((XmlSchemaSimpleType)simpleType.Redefined);
						simpleType.SetBaseSchemaType(simpleType.Redefined.BaseXmlSchemaType);
						xmlSchemaDatatype2 = simpleType.Redefined.Datatype;
					}
					else
					{
						if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Equals(DatatypeImplementation.QnAnySimpleType) && Preprocessor.GetParentSchema(simpleType).TargetNamespace != "http://www.w3.org/2001/XMLSchema")
						{
							throw new XmlSchemaException("Restriction of 'anySimpleType' is not allowed.", xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString(), simpleType);
						}
						XmlSchemaSimpleType simpleType3 = this.GetSimpleType(xmlSchemaSimpleTypeRestriction.BaseTypeName);
						if (simpleType3 == null)
						{
							throw new XmlSchemaException("Type '{0}' is not declared, or is not a simple type.", xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString(), xmlSchemaSimpleTypeRestriction);
						}
						if ((simpleType3.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
						{
							base.SendValidationEvent("The base type is final restriction.", simpleType);
						}
						simpleType.SetBaseSchemaType(simpleType3);
						xmlSchemaDatatype2 = simpleType3.Datatype;
					}
					simpleType.SetDatatype(xmlSchemaDatatype2.DeriveByRestriction(xmlSchemaSimpleTypeRestriction.Facets, base.NameTable, simpleType));
					simpleType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
				}
				else
				{
					XmlSchemaSimpleType[] array = this.CompileBaseMemberTypes(simpleType);
					simpleType.SetBaseSchemaType(DatatypeImplementation.AnySimpleType);
					simpleType.SetDatatype(XmlSchemaDatatype.DeriveByUnion(array, simpleType));
					simpleType.SetDerivedBy(XmlSchemaDerivationMethod.Union);
				}
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(simpleType);
				}
				base.SendValidationEvent(ex);
				simpleType.SetDatatype(DatatypeImplementation.AnySimpleType.Datatype);
			}
			finally
			{
				simpleType.ElementDecl = new SchemaElementDecl
				{
					ContentValidator = ContentValidator.TextOnly,
					SchemaType = simpleType,
					Datatype = simpleType.Datatype
				};
				simpleType.IsProcessing = false;
			}
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000B5FD8 File Offset: 0x000B41D8
		private XmlSchemaSimpleType[] CompileBaseMemberTypes(XmlSchemaSimpleType simpleType)
		{
			ArrayList arrayList = new ArrayList();
			XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)simpleType.Content;
			XmlQualifiedName[] memberTypes = xmlSchemaSimpleTypeUnion.MemberTypes;
			if (memberTypes != null)
			{
				for (int i = 0; i < memberTypes.Length; i++)
				{
					XmlSchemaSimpleType simpleType2 = this.GetSimpleType(memberTypes[i]);
					if (simpleType2 == null)
					{
						throw new XmlSchemaException("Type '{0}' is not declared, or is not a simple type.", memberTypes[i].ToString(), xmlSchemaSimpleTypeUnion);
					}
					if (simpleType2.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
					{
						this.CheckUnionType(simpleType2, arrayList, simpleType);
					}
					else
					{
						arrayList.Add(simpleType2);
					}
					if ((simpleType2.FinalResolved & XmlSchemaDerivationMethod.Union) != XmlSchemaDerivationMethod.Empty)
					{
						base.SendValidationEvent("The base type is the final union.", simpleType);
					}
				}
			}
			XmlSchemaObjectCollection baseTypes = xmlSchemaSimpleTypeUnion.BaseTypes;
			if (baseTypes != null)
			{
				for (int j = 0; j < baseTypes.Count; j++)
				{
					XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)baseTypes[j];
					this.CompileSimpleType(xmlSchemaSimpleType);
					if (xmlSchemaSimpleType.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
					{
						this.CheckUnionType(xmlSchemaSimpleType, arrayList, simpleType);
					}
					else
					{
						arrayList.Add(xmlSchemaSimpleType);
					}
				}
			}
			xmlSchemaSimpleTypeUnion.SetBaseMemberTypes(arrayList.ToArray(typeof(XmlSchemaSimpleType)) as XmlSchemaSimpleType[]);
			return xmlSchemaSimpleTypeUnion.BaseMemberTypes;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x000B60F0 File Offset: 0x000B42F0
		private void CheckUnionType(XmlSchemaSimpleType unionMember, ArrayList memberTypeDefinitions, XmlSchemaSimpleType parentType)
		{
			XmlSchemaDatatype datatype = unionMember.Datatype;
			if (unionMember.DerivedBy == XmlSchemaDerivationMethod.Restriction && (datatype.HasLexicalFacets || datatype.HasValueFacets))
			{
				base.SendValidationEvent("It is an error if a union type has a member with variety union and this member cannot be substituted with its own members. This may be due to the fact that the union member is a restriction of a union with facets.", parentType);
				return;
			}
			Datatype_union datatype_union = unionMember.Datatype as Datatype_union;
			memberTypeDefinitions.AddRange(datatype_union.BaseMemberTypes);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x000B6144 File Offset: 0x000B4344
		private void CompileComplexType(XmlSchemaComplexType complexType)
		{
			if (complexType.ElementDecl != null)
			{
				return;
			}
			if (complexType.IsProcessing)
			{
				base.SendValidationEvent("Circular type reference.", complexType);
				return;
			}
			complexType.IsProcessing = true;
			try
			{
				if (complexType.ContentModel != null)
				{
					if (complexType.ContentModel is XmlSchemaSimpleContent)
					{
						XmlSchemaSimpleContent xmlSchemaSimpleContent = (XmlSchemaSimpleContent)complexType.ContentModel;
						complexType.SetContentType(XmlSchemaContentType.TextOnly);
						if (xmlSchemaSimpleContent.Content is XmlSchemaSimpleContentExtension)
						{
							this.CompileSimpleContentExtension(complexType, (XmlSchemaSimpleContentExtension)xmlSchemaSimpleContent.Content);
						}
						else
						{
							this.CompileSimpleContentRestriction(complexType, (XmlSchemaSimpleContentRestriction)xmlSchemaSimpleContent.Content);
						}
					}
					else
					{
						XmlSchemaComplexContent xmlSchemaComplexContent = (XmlSchemaComplexContent)complexType.ContentModel;
						if (xmlSchemaComplexContent.Content is XmlSchemaComplexContentExtension)
						{
							this.CompileComplexContentExtension(complexType, xmlSchemaComplexContent, (XmlSchemaComplexContentExtension)xmlSchemaComplexContent.Content);
						}
						else
						{
							this.CompileComplexContentRestriction(complexType, xmlSchemaComplexContent, (XmlSchemaComplexContentRestriction)xmlSchemaComplexContent.Content);
						}
					}
				}
				else
				{
					complexType.SetBaseSchemaType(XmlSchemaComplexType.AnyType);
					this.CompileLocalAttributes(XmlSchemaComplexType.AnyType, complexType, complexType.Attributes, complexType.AnyAttribute, XmlSchemaDerivationMethod.Restriction);
					complexType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
					complexType.SetContentTypeParticle(this.CompileContentTypeParticle(complexType.Particle));
					complexType.SetContentType(this.GetSchemaContentType(complexType, null, complexType.ContentTypeParticle));
				}
				if (complexType.ContainsIdAttribute(true))
				{
					base.SendValidationEvent("Two distinct members of the attribute uses must not have type definitions which are both xs:ID or are derived from xs:ID.", complexType);
				}
				SchemaElementDecl schemaElementDecl = new SchemaElementDecl();
				schemaElementDecl.ContentValidator = this.CompileComplexContent(complexType);
				schemaElementDecl.SchemaType = complexType;
				schemaElementDecl.IsAbstract = complexType.IsAbstract;
				schemaElementDecl.Datatype = complexType.Datatype;
				schemaElementDecl.Block = complexType.BlockResolved;
				schemaElementDecl.AnyAttribute = complexType.AttributeWildcard;
				foreach (object obj in complexType.AttributeUses.Values)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
					if (xmlSchemaAttribute.Use == XmlSchemaUse.Prohibited)
					{
						if (!schemaElementDecl.ProhibitedAttributes.ContainsKey(xmlSchemaAttribute.QualifiedName))
						{
							schemaElementDecl.ProhibitedAttributes.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute.QualifiedName);
						}
					}
					else if (!schemaElementDecl.AttDefs.ContainsKey(xmlSchemaAttribute.QualifiedName) && xmlSchemaAttribute.AttDef != null && xmlSchemaAttribute.AttDef.Name != XmlQualifiedName.Empty && xmlSchemaAttribute.AttDef != SchemaAttDef.Empty)
					{
						schemaElementDecl.AddAttDef(xmlSchemaAttribute.AttDef);
					}
				}
				complexType.ElementDecl = schemaElementDecl;
			}
			finally
			{
				complexType.IsProcessing = false;
			}
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x000B63E4 File Offset: 0x000B45E4
		private void CompileSimpleContentExtension(XmlSchemaComplexType complexType, XmlSchemaSimpleContentExtension simpleExtension)
		{
			XmlSchemaComplexType xmlSchemaComplexType;
			if (complexType.Redefined != null && simpleExtension.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
				complexType.SetBaseSchemaType(xmlSchemaComplexType);
				complexType.SetDatatype(xmlSchemaComplexType.Datatype);
			}
			else
			{
				XmlSchemaType anySchemaType = this.GetAnySchemaType(simpleExtension.BaseTypeName);
				if (anySchemaType == null)
				{
					base.SendValidationEvent("Type '{0}' is not declared.", simpleExtension.BaseTypeName.ToString(), simpleExtension);
				}
				else
				{
					complexType.SetBaseSchemaType(anySchemaType);
					complexType.SetDatatype(anySchemaType.Datatype);
				}
				xmlSchemaComplexType = anySchemaType as XmlSchemaComplexType;
			}
			if (xmlSchemaComplexType != null)
			{
				if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Extension) != XmlSchemaDerivationMethod.Empty)
				{
					base.SendValidationEvent("The base type is the final extension.", complexType);
				}
				if (xmlSchemaComplexType.ContentType != XmlSchemaContentType.TextOnly)
				{
					base.SendValidationEvent("The content type of the base type must be a simple type definition or it must be mixed, and simpleType child must be present.", complexType);
				}
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Extension);
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, simpleExtension.Attributes, simpleExtension.AnyAttribute, XmlSchemaDerivationMethod.Extension);
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x000B64C8 File Offset: 0x000B46C8
		private void CompileSimpleContentRestriction(XmlSchemaComplexType complexType, XmlSchemaSimpleContentRestriction simpleRestriction)
		{
			XmlSchemaComplexType xmlSchemaComplexType = null;
			XmlSchemaDatatype xmlSchemaDatatype = null;
			if (complexType.Redefined != null && simpleRestriction.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
				xmlSchemaDatatype = xmlSchemaComplexType.Datatype;
			}
			else
			{
				xmlSchemaComplexType = this.GetComplexType(simpleRestriction.BaseTypeName);
				if (xmlSchemaComplexType == null)
				{
					base.SendValidationEvent("Undefined complexType '{0}' is used as a base for complex type restriction.", simpleRestriction.BaseTypeName.ToString(), simpleRestriction);
					return;
				}
				if (xmlSchemaComplexType.ContentType == XmlSchemaContentType.TextOnly)
				{
					if (simpleRestriction.BaseType == null)
					{
						xmlSchemaDatatype = xmlSchemaComplexType.Datatype;
					}
					else
					{
						this.CompileSimpleType(simpleRestriction.BaseType);
						if (!XmlSchemaType.IsDerivedFromDatatype(simpleRestriction.BaseType.Datatype, xmlSchemaComplexType.Datatype, XmlSchemaDerivationMethod.None))
						{
							base.SendValidationEvent("The data type of the simple content is not a valid restriction of the base complex type.", simpleRestriction);
						}
						xmlSchemaDatatype = simpleRestriction.BaseType.Datatype;
					}
				}
				else if (xmlSchemaComplexType.ContentType == XmlSchemaContentType.Mixed && xmlSchemaComplexType.ElementDecl.ContentValidator.IsEmptiable)
				{
					if (simpleRestriction.BaseType != null)
					{
						this.CompileSimpleType(simpleRestriction.BaseType);
						complexType.SetBaseSchemaType(simpleRestriction.BaseType);
						xmlSchemaDatatype = simpleRestriction.BaseType.Datatype;
					}
					else
					{
						base.SendValidationEvent("Simple content restriction must have a simple type child if the content type of the base type is not a simple type definition.", simpleRestriction);
					}
				}
				else
				{
					base.SendValidationEvent("The content type of the base type must be a simple type definition or it must be mixed, and simpleType child must be present.", complexType);
				}
			}
			if (xmlSchemaComplexType != null && xmlSchemaComplexType.ElementDecl != null && (xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("The base type is final restriction.", complexType);
			}
			if (xmlSchemaComplexType != null)
			{
				complexType.SetBaseSchemaType(xmlSchemaComplexType);
			}
			if (xmlSchemaDatatype != null)
			{
				try
				{
					complexType.SetDatatype(xmlSchemaDatatype.DeriveByRestriction(simpleRestriction.Facets, base.NameTable, complexType));
				}
				catch (XmlSchemaException ex)
				{
					if (ex.SourceSchemaObject == null)
					{
						ex.SetSource(complexType);
					}
					base.SendValidationEvent(ex);
					complexType.SetDatatype(DatatypeImplementation.AnySimpleType.Datatype);
				}
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, simpleRestriction.Attributes, simpleRestriction.AnyAttribute, XmlSchemaDerivationMethod.Restriction);
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000B66A0 File Offset: 0x000B48A0
		private void CompileComplexContentExtension(XmlSchemaComplexType complexType, XmlSchemaComplexContent complexContent, XmlSchemaComplexContentExtension complexExtension)
		{
			XmlSchemaComplexType xmlSchemaComplexType;
			if (complexType.Redefined != null && complexExtension.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
			}
			else
			{
				xmlSchemaComplexType = this.GetComplexType(complexExtension.BaseTypeName);
				if (xmlSchemaComplexType == null)
				{
					base.SendValidationEvent("Undefined complexType '{0}' is used as a base for complex type extension.", complexExtension.BaseTypeName.ToString(), complexExtension);
					return;
				}
			}
			if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Extension) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("The base type is the final extension.", complexType);
			}
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, complexExtension.Attributes, complexExtension.AnyAttribute, XmlSchemaDerivationMethod.Extension);
			XmlSchemaParticle contentTypeParticle = xmlSchemaComplexType.ContentTypeParticle;
			XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle(complexExtension.Particle, true);
			if (contentTypeParticle != XmlSchemaParticle.Empty)
			{
				if (xmlSchemaParticle != XmlSchemaParticle.Empty)
				{
					complexType.SetContentTypeParticle(this.CompileContentTypeParticle(new XmlSchemaSequence
					{
						Items = { contentTypeParticle, xmlSchemaParticle }
					}));
				}
				else
				{
					complexType.SetContentTypeParticle(contentTypeParticle);
				}
			}
			else
			{
				complexType.SetContentTypeParticle(xmlSchemaParticle);
			}
			XmlSchemaContentType xmlSchemaContentType = this.GetSchemaContentType(complexType, complexContent, xmlSchemaParticle);
			if (xmlSchemaContentType == XmlSchemaContentType.Empty)
			{
				xmlSchemaContentType = xmlSchemaComplexType.ContentType;
				if (xmlSchemaContentType == XmlSchemaContentType.TextOnly)
				{
					complexType.SetDatatype(xmlSchemaComplexType.Datatype);
				}
			}
			complexType.SetContentType(xmlSchemaContentType);
			if (xmlSchemaComplexType.ContentType != XmlSchemaContentType.Empty && complexType.ContentType != xmlSchemaComplexType.ContentType)
			{
				base.SendValidationEvent("The derived type and the base type must have the same content type.", complexType);
				return;
			}
			complexType.SetBaseSchemaType(xmlSchemaComplexType);
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Extension);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000B67FC File Offset: 0x000B49FC
		private void CompileComplexContentRestriction(XmlSchemaComplexType complexType, XmlSchemaComplexContent complexContent, XmlSchemaComplexContentRestriction complexRestriction)
		{
			XmlSchemaComplexType xmlSchemaComplexType;
			if (complexType.Redefined != null && complexRestriction.BaseTypeName == complexType.Redefined.QualifiedName)
			{
				xmlSchemaComplexType = (XmlSchemaComplexType)complexType.Redefined;
				this.CompileComplexType(xmlSchemaComplexType);
			}
			else
			{
				xmlSchemaComplexType = this.GetComplexType(complexRestriction.BaseTypeName);
				if (xmlSchemaComplexType == null)
				{
					base.SendValidationEvent("Undefined complexType '{0}' is used as a base for complex type restriction.", complexRestriction.BaseTypeName.ToString(), complexRestriction);
					return;
				}
			}
			complexType.SetBaseSchemaType(xmlSchemaComplexType);
			if ((xmlSchemaComplexType.FinalResolved & XmlSchemaDerivationMethod.Restriction) != XmlSchemaDerivationMethod.Empty)
			{
				base.SendValidationEvent("The base type is final restriction.", complexType);
			}
			this.CompileLocalAttributes(xmlSchemaComplexType, complexType, complexRestriction.Attributes, complexRestriction.AnyAttribute, XmlSchemaDerivationMethod.Restriction);
			complexType.SetContentTypeParticle(this.CompileContentTypeParticle(complexRestriction.Particle));
			XmlSchemaContentType schemaContentType = this.GetSchemaContentType(complexType, complexContent, complexType.ContentTypeParticle);
			complexType.SetContentType(schemaContentType);
			if (schemaContentType != XmlSchemaContentType.Empty)
			{
				if (schemaContentType == XmlSchemaContentType.Mixed)
				{
					if (xmlSchemaComplexType.ContentType != XmlSchemaContentType.Mixed)
					{
						base.SendValidationEvent("Invalid content type derivation by restriction. {0}", Res.GetString("If the derived content type is Mixed, then the base content type should also be Mixed according to rule 5.4 of Schema Component Constraint: Derivation Valid (Restriction, Complex)."), complexType);
					}
				}
			}
			else if (xmlSchemaComplexType.ElementDecl != null && !xmlSchemaComplexType.ElementDecl.ContentValidator.IsEmptiable)
			{
				base.SendValidationEvent("Invalid content type derivation by restriction. {0}", Res.GetString("If the derived content type is Empty, then the base content type should also be Empty or Mixed with Emptiable particle according to rule 5.3 of Schema Component Constraint: Derivation Valid (Restriction, Complex)."), complexType);
			}
			complexType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000B6924 File Offset: 0x000B4B24
		private void CheckParticleDerivation(XmlSchemaComplexType complexType)
		{
			XmlSchemaComplexType xmlSchemaComplexType = complexType.BaseXmlSchemaType as XmlSchemaComplexType;
			this.restrictionErrorMsg = null;
			if (xmlSchemaComplexType != null && xmlSchemaComplexType != XmlSchemaComplexType.AnyType && complexType.DerivedBy == XmlSchemaDerivationMethod.Restriction)
			{
				XmlSchemaParticle xmlSchemaParticle = this.CannonicalizePointlessRoot(complexType.ContentTypeParticle);
				XmlSchemaParticle xmlSchemaParticle2 = this.CannonicalizePointlessRoot(xmlSchemaComplexType.ContentTypeParticle);
				if (!this.IsValidRestriction(xmlSchemaParticle, xmlSchemaParticle2))
				{
					if (this.restrictionErrorMsg != null)
					{
						base.SendValidationEvent("Invalid particle derivation by restriction - '{0}'.", this.restrictionErrorMsg, complexType);
						return;
					}
					base.SendValidationEvent("Invalid particle derivation by restriction.", complexType);
					return;
				}
			}
			else if (xmlSchemaComplexType == XmlSchemaComplexType.AnyType)
			{
				foreach (object obj in complexType.LocalElements.Values)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
					if (!xmlSchemaElement.IsLocalTypeDerivationChecked)
					{
						XmlSchemaComplexType xmlSchemaComplexType2 = xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType;
						if (xmlSchemaComplexType2 != null && xmlSchemaElement.SchemaTypeName == XmlQualifiedName.Empty && xmlSchemaElement.RefName == XmlQualifiedName.Empty)
						{
							xmlSchemaElement.IsLocalTypeDerivationChecked = true;
							this.CheckParticleDerivation(xmlSchemaComplexType2);
						}
					}
				}
			}
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000B6A54 File Offset: 0x000B4C54
		private void CheckParticleDerivation(XmlSchemaParticle derivedParticle, XmlSchemaParticle baseParticle)
		{
			this.restrictionErrorMsg = null;
			derivedParticle = this.CannonicalizePointlessRoot(derivedParticle);
			baseParticle = this.CannonicalizePointlessRoot(baseParticle);
			if (!this.IsValidRestriction(derivedParticle, baseParticle))
			{
				if (this.restrictionErrorMsg != null)
				{
					base.SendValidationEvent("Invalid particle derivation by restriction - '{0}'.", this.restrictionErrorMsg, derivedParticle);
					return;
				}
				base.SendValidationEvent("Invalid particle derivation by restriction.", derivedParticle);
			}
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x000B6AAC File Offset: 0x000B4CAC
		private XmlSchemaParticle CompileContentTypeParticle(XmlSchemaParticle particle)
		{
			XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle(particle, true);
			XmlSchemaChoice xmlSchemaChoice = xmlSchemaParticle as XmlSchemaChoice;
			if (xmlSchemaChoice != null && xmlSchemaChoice.Items.Count == 0)
			{
				if (xmlSchemaChoice.MinOccurs != 0m)
				{
					base.SendValidationEvent("Empty choice cannot be satisfied if 'minOccurs' is not equal to 0.", xmlSchemaChoice, XmlSeverityType.Warning);
				}
				return XmlSchemaParticle.Empty;
			}
			return xmlSchemaParticle;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000B6B00 File Offset: 0x000B4D00
		private XmlSchemaParticle CannonicalizeParticle(XmlSchemaParticle particle, bool root)
		{
			if (particle == null || particle.IsEmpty)
			{
				return XmlSchemaParticle.Empty;
			}
			if (particle is XmlSchemaElement)
			{
				return particle;
			}
			if (particle is XmlSchemaGroupRef)
			{
				return this.CannonicalizeGroupRef((XmlSchemaGroupRef)particle, root);
			}
			if (particle is XmlSchemaAll)
			{
				return this.CannonicalizeAll((XmlSchemaAll)particle, root);
			}
			if (particle is XmlSchemaChoice)
			{
				return this.CannonicalizeChoice((XmlSchemaChoice)particle, root);
			}
			if (particle is XmlSchemaSequence)
			{
				return this.CannonicalizeSequence((XmlSchemaSequence)particle, root);
			}
			return particle;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000B6B84 File Offset: 0x000B4D84
		private XmlSchemaParticle CannonicalizeElement(XmlSchemaElement element)
		{
			if (element.RefName.IsEmpty || (element.ElementDecl.Block & XmlSchemaDerivationMethod.Substitution) != XmlSchemaDerivationMethod.Empty)
			{
				return element;
			}
			XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)this.examplars[element.QualifiedName];
			if (xmlSchemaSubstitutionGroup == null)
			{
				return element;
			}
			XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
			for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
			{
				xmlSchemaChoice.Items.Add((XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[i]);
			}
			xmlSchemaChoice.MinOccurs = element.MinOccurs;
			xmlSchemaChoice.MaxOccurs = element.MaxOccurs;
			this.CopyPosition(xmlSchemaChoice, element, false);
			return xmlSchemaChoice;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x000B6C2C File Offset: 0x000B4E2C
		private XmlSchemaParticle CannonicalizeGroupRef(XmlSchemaGroupRef groupRef, bool root)
		{
			XmlSchemaGroup xmlSchemaGroup;
			if (groupRef.Redefined != null)
			{
				xmlSchemaGroup = groupRef.Redefined;
			}
			else
			{
				xmlSchemaGroup = (XmlSchemaGroup)this.groups[groupRef.RefName];
			}
			if (xmlSchemaGroup == null)
			{
				base.SendValidationEvent("Reference to undeclared model group '{0}'.", groupRef.RefName.ToString(), groupRef);
				return XmlSchemaParticle.Empty;
			}
			if (xmlSchemaGroup.CanonicalParticle == null)
			{
				this.CompileGroup(xmlSchemaGroup);
			}
			if (xmlSchemaGroup.CanonicalParticle == XmlSchemaParticle.Empty)
			{
				return XmlSchemaParticle.Empty;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase = (XmlSchemaGroupBase)xmlSchemaGroup.CanonicalParticle;
			if (xmlSchemaGroupBase is XmlSchemaAll)
			{
				if (!root)
				{
					base.SendValidationEvent("The group ref to 'all' is not the root particle, or it is being used as an extension.", "", groupRef);
					return XmlSchemaParticle.Empty;
				}
				if (groupRef.MinOccurs > 1m || groupRef.MaxOccurs != 1m)
				{
					base.SendValidationEvent("The group ref to 'all' must have {min occurs}= 0 or 1 and {max occurs}=1.", groupRef);
					return XmlSchemaParticle.Empty;
				}
			}
			else if (xmlSchemaGroupBase is XmlSchemaChoice && xmlSchemaGroupBase.Items.Count == 0)
			{
				if (groupRef.MinOccurs != 0m)
				{
					base.SendValidationEvent("Empty choice cannot be satisfied if 'minOccurs' is not equal to 0.", groupRef, XmlSeverityType.Warning);
				}
				return XmlSchemaParticle.Empty;
			}
			XmlSchemaGroupBase xmlSchemaGroupBase2 = ((xmlSchemaGroupBase is XmlSchemaSequence) ? new XmlSchemaSequence() : ((xmlSchemaGroupBase is XmlSchemaChoice) ? new XmlSchemaChoice() : new XmlSchemaAll()));
			xmlSchemaGroupBase2.MinOccurs = groupRef.MinOccurs;
			xmlSchemaGroupBase2.MaxOccurs = groupRef.MaxOccurs;
			this.CopyPosition(xmlSchemaGroupBase2, groupRef, true);
			for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
			{
				xmlSchemaGroupBase2.Items.Add(xmlSchemaGroupBase.Items[i]);
			}
			groupRef.SetParticle(xmlSchemaGroupBase2);
			return xmlSchemaGroupBase2;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000B6DBC File Offset: 0x000B4FBC
		private XmlSchemaParticle CannonicalizeAll(XmlSchemaAll all, bool root)
		{
			if (all.Items.Count > 0)
			{
				XmlSchemaAll xmlSchemaAll = new XmlSchemaAll();
				xmlSchemaAll.MinOccurs = all.MinOccurs;
				xmlSchemaAll.MaxOccurs = all.MaxOccurs;
				this.CopyPosition(xmlSchemaAll, all, true);
				for (int i = 0; i < all.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaElement)all.Items[i], false);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						xmlSchemaAll.Items.Add(xmlSchemaParticle);
					}
				}
				all = xmlSchemaAll;
			}
			if (all.Items.Count == 0)
			{
				return XmlSchemaParticle.Empty;
			}
			if (!root)
			{
				base.SendValidationEvent("'all' is not the only particle in a group, or is being used as an extension.", all);
				return XmlSchemaParticle.Empty;
			}
			return all;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000B6E70 File Offset: 0x000B5070
		private XmlSchemaParticle CannonicalizeChoice(XmlSchemaChoice choice, bool root)
		{
			XmlSchemaChoice xmlSchemaChoice = choice;
			if (choice.Items.Count > 0)
			{
				XmlSchemaChoice xmlSchemaChoice2 = new XmlSchemaChoice();
				xmlSchemaChoice2.MinOccurs = choice.MinOccurs;
				xmlSchemaChoice2.MaxOccurs = choice.MaxOccurs;
				this.CopyPosition(xmlSchemaChoice2, choice, true);
				for (int i = 0; i < choice.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaParticle)choice.Items[i], false);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						if (xmlSchemaParticle.MinOccurs == 1m && xmlSchemaParticle.MaxOccurs == 1m && xmlSchemaParticle is XmlSchemaChoice)
						{
							XmlSchemaChoice xmlSchemaChoice3 = xmlSchemaParticle as XmlSchemaChoice;
							for (int j = 0; j < xmlSchemaChoice3.Items.Count; j++)
							{
								xmlSchemaChoice2.Items.Add(xmlSchemaChoice3.Items[j]);
							}
						}
						else
						{
							xmlSchemaChoice2.Items.Add(xmlSchemaParticle);
						}
					}
				}
				choice = xmlSchemaChoice2;
			}
			if (!root && choice.Items.Count == 0)
			{
				if (choice.MinOccurs != 0m)
				{
					base.SendValidationEvent("Empty choice cannot be satisfied if 'minOccurs' is not equal to 0.", xmlSchemaChoice, XmlSeverityType.Warning);
				}
				return XmlSchemaParticle.Empty;
			}
			if (!root && choice.Items.Count == 1 && choice.MinOccurs == 1m && choice.MaxOccurs == 1m)
			{
				return (XmlSchemaParticle)choice.Items[0];
			}
			return choice;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000B6FEC File Offset: 0x000B51EC
		private XmlSchemaParticle CannonicalizeSequence(XmlSchemaSequence sequence, bool root)
		{
			if (sequence.Items.Count > 0)
			{
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				xmlSchemaSequence.MinOccurs = sequence.MinOccurs;
				xmlSchemaSequence.MaxOccurs = sequence.MaxOccurs;
				this.CopyPosition(xmlSchemaSequence, sequence, true);
				for (int i = 0; i < sequence.Items.Count; i++)
				{
					XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeParticle((XmlSchemaParticle)sequence.Items[i], false);
					if (xmlSchemaParticle != XmlSchemaParticle.Empty)
					{
						XmlSchemaSequence xmlSchemaSequence2 = xmlSchemaParticle as XmlSchemaSequence;
						if (xmlSchemaParticle.MinOccurs == 1m && xmlSchemaParticle.MaxOccurs == 1m && xmlSchemaSequence2 != null)
						{
							for (int j = 0; j < xmlSchemaSequence2.Items.Count; j++)
							{
								xmlSchemaSequence.Items.Add(xmlSchemaSequence2.Items[j]);
							}
						}
						else
						{
							xmlSchemaSequence.Items.Add(xmlSchemaParticle);
						}
					}
				}
				sequence = xmlSchemaSequence;
			}
			if (sequence.Items.Count == 0)
			{
				return XmlSchemaParticle.Empty;
			}
			if (!root && sequence.Items.Count == 1 && sequence.MinOccurs == 1m && sequence.MaxOccurs == 1m)
			{
				return (XmlSchemaParticle)sequence.Items[0];
			}
			return sequence;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000B713C File Offset: 0x000B533C
		private XmlSchemaParticle CannonicalizePointlessRoot(XmlSchemaParticle particle)
		{
			if (particle == null)
			{
				return null;
			}
			decimal num = 1m;
			XmlSchemaSequence xmlSchemaSequence;
			XmlSchemaChoice xmlSchemaChoice;
			XmlSchemaAll xmlSchemaAll;
			if ((xmlSchemaSequence = particle as XmlSchemaSequence) != null)
			{
				XmlSchemaObjectCollection items = xmlSchemaSequence.Items;
				if (items.Count == 1 && xmlSchemaSequence.MinOccurs == num && xmlSchemaSequence.MaxOccurs == num)
				{
					return (XmlSchemaParticle)items[0];
				}
			}
			else if ((xmlSchemaChoice = particle as XmlSchemaChoice) != null)
			{
				XmlSchemaObjectCollection items2 = xmlSchemaChoice.Items;
				int count = items2.Count;
				if (count == 1)
				{
					if (xmlSchemaChoice.MinOccurs == num && xmlSchemaChoice.MaxOccurs == num)
					{
						return (XmlSchemaParticle)items2[0];
					}
				}
				else if (count == 0)
				{
					return XmlSchemaParticle.Empty;
				}
			}
			else if ((xmlSchemaAll = particle as XmlSchemaAll) != null)
			{
				XmlSchemaObjectCollection items3 = xmlSchemaAll.Items;
				if (items3.Count == 1 && xmlSchemaAll.MinOccurs == num && xmlSchemaAll.MaxOccurs == num)
				{
					return (XmlSchemaParticle)items3[0];
				}
			}
			return particle;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000B7240 File Offset: 0x000B5440
		private bool IsValidRestriction(XmlSchemaParticle derivedParticle, XmlSchemaParticle baseParticle)
		{
			if (derivedParticle == baseParticle)
			{
				return true;
			}
			if (derivedParticle == null || derivedParticle == XmlSchemaParticle.Empty)
			{
				return this.IsParticleEmptiable(baseParticle);
			}
			if (baseParticle == null || baseParticle == XmlSchemaParticle.Empty)
			{
				return false;
			}
			if (derivedParticle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)derivedParticle;
				derivedParticle = this.CannonicalizeElement(xmlSchemaElement);
			}
			if (baseParticle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)baseParticle;
				XmlSchemaParticle xmlSchemaParticle = this.CannonicalizeElement(xmlSchemaElement2);
				if (xmlSchemaParticle is XmlSchemaChoice)
				{
					return this.IsValidRestriction(derivedParticle, xmlSchemaParticle);
				}
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromElement((XmlSchemaElement)derivedParticle, xmlSchemaElement2);
				}
				this.restrictionErrorMsg = Res.GetString("Only 'element' is valid as derived particle when the base particle is 'element'.");
				return false;
			}
			else if (baseParticle is XmlSchemaAny)
			{
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromAny((XmlSchemaElement)derivedParticle, (XmlSchemaAny)baseParticle);
				}
				if (derivedParticle is XmlSchemaAny)
				{
					return this.IsAnyFromAny((XmlSchemaAny)derivedParticle, (XmlSchemaAny)baseParticle);
				}
				return this.IsGroupBaseFromAny((XmlSchemaGroupBase)derivedParticle, (XmlSchemaAny)baseParticle);
			}
			else if (baseParticle is XmlSchemaAll)
			{
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle);
				}
				if (derivedParticle is XmlSchemaAll)
				{
					if (this.IsGroupBaseFromGroupBase((XmlSchemaGroupBase)derivedParticle, (XmlSchemaGroupBase)baseParticle, true))
					{
						return true;
					}
				}
				else if (derivedParticle is XmlSchemaSequence)
				{
					if (this.IsSequenceFromAll((XmlSchemaSequence)derivedParticle, (XmlSchemaAll)baseParticle))
					{
						return true;
					}
					this.restrictionErrorMsg = Res.GetString("The derived sequence particle at ({0}, {1}) is not a valid restriction of the base all particle at ({2}, {3}) according to Sequence:All -- RecurseUnordered.", new object[]
					{
						derivedParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						derivedParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
					});
				}
				else if (derivedParticle is XmlSchemaChoice || derivedParticle is XmlSchemaAny)
				{
					this.restrictionErrorMsg = Res.GetString("'Choice' or 'any' is forbidden as derived particle when the base particle is 'all'.");
				}
				return false;
			}
			else if (baseParticle is XmlSchemaChoice)
			{
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle);
				}
				if (derivedParticle is XmlSchemaChoice)
				{
					XmlSchemaChoice xmlSchemaChoice = baseParticle as XmlSchemaChoice;
					XmlSchemaChoice xmlSchemaChoice2 = derivedParticle as XmlSchemaChoice;
					if (xmlSchemaChoice.Parent == null || xmlSchemaChoice2.Parent == null)
					{
						return this.IsChoiceFromChoiceSubstGroup(xmlSchemaChoice2, xmlSchemaChoice);
					}
					if (this.IsGroupBaseFromGroupBase(xmlSchemaChoice2, xmlSchemaChoice, false))
					{
						return true;
					}
				}
				else if (derivedParticle is XmlSchemaSequence)
				{
					if (this.IsSequenceFromChoice((XmlSchemaSequence)derivedParticle, (XmlSchemaChoice)baseParticle))
					{
						return true;
					}
					this.restrictionErrorMsg = Res.GetString("The derived sequence particle at ({0}, {1}) is not a valid restriction of the base choice particle at ({2}, {3}) according to Sequence:Choice -- MapAndSum.", new object[]
					{
						derivedParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						derivedParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
						baseParticle.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
					});
				}
				else
				{
					this.restrictionErrorMsg = Res.GetString("'All' or 'any' is forbidden as derived particle when the base particle is 'choice'.");
				}
				return false;
			}
			else
			{
				if (!(baseParticle is XmlSchemaSequence))
				{
					return false;
				}
				if (derivedParticle is XmlSchemaElement)
				{
					return this.IsElementFromGroupBase((XmlSchemaElement)derivedParticle, (XmlSchemaGroupBase)baseParticle);
				}
				if (derivedParticle is XmlSchemaSequence || (derivedParticle is XmlSchemaAll && ((XmlSchemaGroupBase)derivedParticle).Items.Count == 1))
				{
					if (this.IsGroupBaseFromGroupBase((XmlSchemaGroupBase)derivedParticle, (XmlSchemaGroupBase)baseParticle, true))
					{
						return true;
					}
				}
				else
				{
					this.restrictionErrorMsg = Res.GetString("'All', 'any', and 'choice' are forbidden as derived particles when the base particle is 'sequence'.");
				}
				return false;
			}
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000B75A8 File Offset: 0x000B57A8
		private bool IsElementFromElement(XmlSchemaElement derivedElement, XmlSchemaElement baseElement)
		{
			XmlSchemaDerivationMethod xmlSchemaDerivationMethod = ((baseElement.ElementDecl.Block == XmlSchemaDerivationMethod.All) ? (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction) : baseElement.ElementDecl.Block);
			XmlSchemaDerivationMethod xmlSchemaDerivationMethod2 = ((derivedElement.ElementDecl.Block == XmlSchemaDerivationMethod.All) ? (XmlSchemaDerivationMethod.Substitution | XmlSchemaDerivationMethod.Extension | XmlSchemaDerivationMethod.Restriction) : derivedElement.ElementDecl.Block);
			if (!(derivedElement.QualifiedName == baseElement.QualifiedName) || (!baseElement.IsNillable && derivedElement.IsNillable) || (!this.IsValidOccurrenceRangeRestriction(derivedElement, baseElement) || (baseElement.FixedValue != null && !this.IsFixedEqual(baseElement.ElementDecl, derivedElement.ElementDecl))) || (xmlSchemaDerivationMethod2 | xmlSchemaDerivationMethod) != xmlSchemaDerivationMethod2 || derivedElement.ElementSchemaType == null || baseElement.ElementSchemaType == null || !XmlSchemaType.IsDerivedFrom(derivedElement.ElementSchemaType, baseElement.ElementSchemaType, ~(XmlSchemaDerivationMethod.Restriction | XmlSchemaDerivationMethod.List | XmlSchemaDerivationMethod.Union)))
			{
				this.restrictionErrorMsg = Res.GetString("Derived element '{0}' is not a valid restriction of base element '{1}' according to Elt:Elt -- NameAndTypeOK.", new object[] { derivedElement.QualifiedName, baseElement.QualifiedName });
				return false;
			}
			return true;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000B7698 File Offset: 0x000B5898
		private bool IsElementFromAny(XmlSchemaElement derivedElement, XmlSchemaAny baseAny)
		{
			if (!baseAny.Allows(derivedElement.QualifiedName))
			{
				this.restrictionErrorMsg = Res.GetString("The namespace of element '{0}'is not valid with respect to the wildcard's namespace constraint in the base, Elt:Any -- NSCompat Rule 1.", new object[] { derivedElement.QualifiedName.ToString() });
				return false;
			}
			if (!this.IsValidOccurrenceRangeRestriction(derivedElement, baseAny))
			{
				this.restrictionErrorMsg = Res.GetString("The occurrence range of element '{0}'is not a valid restriction of the wildcard's occurrence range in the base, Elt:Any -- NSCompat Rule2.", new object[] { derivedElement.QualifiedName.ToString() });
				return false;
			}
			return true;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000B770C File Offset: 0x000B590C
		private bool IsAnyFromAny(XmlSchemaAny derivedAny, XmlSchemaAny baseAny)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedAny, baseAny))
			{
				this.restrictionErrorMsg = Res.GetString("The derived wildcard's occurrence range is not a valid restriction of the base wildcard's occurrence range, Any:Any -- NSSubset Rule 1.");
				return false;
			}
			if (!NamespaceList.IsSubset(derivedAny.NamespaceList, baseAny.NamespaceList))
			{
				this.restrictionErrorMsg = Res.GetString("The derived wildcard's namespace constraint must be an intensional subset of the base wildcard's namespace constraint, Any:Any -- NSSubset Rule2.");
				return false;
			}
			if (derivedAny.ProcessContentsCorrect < baseAny.ProcessContentsCorrect)
			{
				this.restrictionErrorMsg = Res.GetString("The derived wildcard's 'processContents' must be identical to or stronger than the base wildcard's 'processContents', where 'strict' is stronger than 'lax' and 'lax' is stronger than 'skip', Any:Any -- NSSubset Rule 3.");
				return false;
			}
			return true;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000B777C File Offset: 0x000B597C
		private bool IsGroupBaseFromAny(XmlSchemaGroupBase derivedGroupBase, XmlSchemaAny baseAny)
		{
			decimal num;
			decimal num2;
			this.CalculateEffectiveTotalRange(derivedGroupBase, out num, out num2);
			if (!this.IsValidOccurrenceRangeRestriction(num, num2, baseAny.MinOccurs, baseAny.MaxOccurs))
			{
				this.restrictionErrorMsg = Res.GetString("The derived particle's occurrence range at ({0}, {1}) is not a valid restriction of the base wildcard's occurrence range at ({2}, {3}), NSRecurseCheckCardinality Rule 2.", new object[]
				{
					derivedGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseAny.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseAny.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
				return false;
			}
			string minOccursString = baseAny.MinOccursString;
			baseAny.MinOccurs = 0m;
			for (int i = 0; i < derivedGroupBase.Items.Count; i++)
			{
				if (!this.IsValidRestriction((XmlSchemaParticle)derivedGroupBase.Items[i], baseAny))
				{
					this.restrictionErrorMsg = Res.GetString("Every member of the derived group particle must be a valid restriction of the base wildcard, NSRecurseCheckCardinality Rule 1.");
					baseAny.MinOccursString = minOccursString;
					return false;
				}
			}
			baseAny.MinOccursString = minOccursString;
			return true;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000B7884 File Offset: 0x000B5A84
		private bool IsElementFromGroupBase(XmlSchemaElement derivedElement, XmlSchemaGroupBase baseGroupBase)
		{
			if (baseGroupBase is XmlSchemaSequence)
			{
				if (this.IsGroupBaseFromGroupBase(new XmlSchemaSequence
				{
					MinOccurs = 1m,
					MaxOccurs = 1m,
					Items = { derivedElement }
				}, baseGroupBase, true))
				{
					return true;
				}
				this.restrictionErrorMsg = Res.GetString("The derived element {0} at ({1}, {2}) is not a valid restriction of the base sequence particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.", new object[]
				{
					derivedElement.QualifiedName.ToString(),
					derivedElement.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedElement.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
			}
			else if (baseGroupBase is XmlSchemaChoice)
			{
				if (this.IsGroupBaseFromGroupBase(new XmlSchemaChoice
				{
					MinOccurs = 1m,
					MaxOccurs = 1m,
					Items = { derivedElement }
				}, baseGroupBase, false))
				{
					return true;
				}
				this.restrictionErrorMsg = Res.GetString("The derived element {0} at ({1}, {2}) is not a valid restriction of the base choice particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.", new object[]
				{
					derivedElement.QualifiedName.ToString(),
					derivedElement.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedElement.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
			}
			else if (baseGroupBase is XmlSchemaAll)
			{
				if (this.IsGroupBaseFromGroupBase(new XmlSchemaAll
				{
					MinOccurs = 1m,
					MaxOccurs = 1m,
					Items = { derivedElement }
				}, baseGroupBase, true))
				{
					return true;
				}
				this.restrictionErrorMsg = Res.GetString("The derived element {0} at ({1}, {2}) is not a valid restriction of the base all particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.", new object[]
				{
					derivedElement.QualifiedName.ToString(),
					derivedElement.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					derivedElement.LinePosition.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LineNumber.ToString(NumberFormatInfo.InvariantInfo),
					baseGroupBase.LinePosition.ToString(NumberFormatInfo.InvariantInfo)
				});
			}
			return false;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000B7AD4 File Offset: 0x000B5CD4
		private bool IsChoiceFromChoiceSubstGroup(XmlSchemaChoice derivedChoice, XmlSchemaChoice baseChoice)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedChoice, baseChoice))
			{
				this.restrictionErrorMsg = Res.GetString("The derived particle's range is not a valid restriction of the base particle's range according to All:All,Sequence:Sequence -- Recurse Rule 1 or Choice:Choice -- RecurseLax.");
				return false;
			}
			for (int i = 0; i < derivedChoice.Items.Count; i++)
			{
				if (this.GetMappingParticle((XmlSchemaParticle)derivedChoice.Items[i], baseChoice.Items) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000B7B38 File Offset: 0x000B5D38
		private bool IsGroupBaseFromGroupBase(XmlSchemaGroupBase derivedGroupBase, XmlSchemaGroupBase baseGroupBase, bool skipEmptableOnly)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedGroupBase, baseGroupBase))
			{
				this.restrictionErrorMsg = Res.GetString("The derived particle's range is not a valid restriction of the base particle's range according to All:All,Sequence:Sequence -- Recurse Rule 1 or Choice:Choice -- RecurseLax.");
				return false;
			}
			if (derivedGroupBase.Items.Count > baseGroupBase.Items.Count)
			{
				this.restrictionErrorMsg = Res.GetString("The derived particle cannot have more members than the base particle - All:All,Sequence:Sequence -- Recurse Rule 2 / Choice:Choice -- RecurseLax.");
				return false;
			}
			int num = 0;
			for (int i = 0; i < baseGroupBase.Items.Count; i++)
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)baseGroupBase.Items[i];
				if (num < derivedGroupBase.Items.Count && this.IsValidRestriction((XmlSchemaParticle)derivedGroupBase.Items[num], xmlSchemaParticle))
				{
					num++;
				}
				else if (skipEmptableOnly && !this.IsParticleEmptiable(xmlSchemaParticle))
				{
					if (this.restrictionErrorMsg == null)
					{
						this.restrictionErrorMsg = Res.GetString("All particles in the {particles} of the base particle which are not mapped to by any particle in the {particles} of the derived particle should be emptiable - All:All,Sequence:Sequence -- Recurse Rule 2 / Choice:Choice -- RecurseLax.");
					}
					return false;
				}
			}
			return num >= derivedGroupBase.Items.Count;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x000B7C1C File Offset: 0x000B5E1C
		private bool IsSequenceFromAll(XmlSchemaSequence derivedSequence, XmlSchemaAll baseAll)
		{
			if (!this.IsValidOccurrenceRangeRestriction(derivedSequence, baseAll) || derivedSequence.Items.Count > baseAll.Items.Count)
			{
				return false;
			}
			BitSet bitSet = new BitSet(baseAll.Items.Count);
			for (int i = 0; i < derivedSequence.Items.Count; i++)
			{
				int mappingParticle = this.GetMappingParticle((XmlSchemaParticle)derivedSequence.Items[i], baseAll.Items);
				if (mappingParticle < 0)
				{
					return false;
				}
				if (bitSet[mappingParticle])
				{
					return false;
				}
				bitSet.Set(mappingParticle);
			}
			for (int j = 0; j < baseAll.Items.Count; j++)
			{
				if (!bitSet[j] && !this.IsParticleEmptiable((XmlSchemaParticle)baseAll.Items[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x000B7CE8 File Offset: 0x000B5EE8
		private bool IsSequenceFromChoice(XmlSchemaSequence derivedSequence, XmlSchemaChoice baseChoice)
		{
			decimal num = derivedSequence.MinOccurs * derivedSequence.Items.Count;
			decimal num2;
			if (derivedSequence.MaxOccurs == 79228162514264337593543950335m)
			{
				num2 = decimal.MaxValue;
			}
			else
			{
				num2 = derivedSequence.MaxOccurs * derivedSequence.Items.Count;
			}
			if (!this.IsValidOccurrenceRangeRestriction(num, num2, baseChoice.MinOccurs, baseChoice.MaxOccurs) || derivedSequence.Items.Count > baseChoice.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < derivedSequence.Items.Count; i++)
			{
				if (this.GetMappingParticle((XmlSchemaParticle)derivedSequence.Items[i], baseChoice.Items) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x000B7DBB File Offset: 0x000B5FBB
		private bool IsValidOccurrenceRangeRestriction(XmlSchemaParticle derivedParticle, XmlSchemaParticle baseParticle)
		{
			return this.IsValidOccurrenceRangeRestriction(derivedParticle.MinOccurs, derivedParticle.MaxOccurs, baseParticle.MinOccurs, baseParticle.MaxOccurs);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x000ADEB8 File Offset: 0x000AC0B8
		private bool IsValidOccurrenceRangeRestriction(decimal minOccurs, decimal maxOccurs, decimal baseMinOccurs, decimal baseMaxOccurs)
		{
			return baseMinOccurs <= minOccurs && maxOccurs <= baseMaxOccurs;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000B7DDC File Offset: 0x000B5FDC
		private int GetMappingParticle(XmlSchemaParticle particle, XmlSchemaObjectCollection collection)
		{
			for (int i = 0; i < collection.Count; i++)
			{
				if (this.IsValidRestriction(particle, (XmlSchemaParticle)collection[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000B7E14 File Offset: 0x000B6014
		private bool IsParticleEmptiable(XmlSchemaParticle particle)
		{
			decimal num;
			decimal num2;
			this.CalculateEffectiveTotalRange(particle, out num, out num2);
			return num == 0m;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000B7E38 File Offset: 0x000B6038
		private void CalculateEffectiveTotalRange(XmlSchemaParticle particle, out decimal minOccurs, out decimal maxOccurs)
		{
			XmlSchemaChoice xmlSchemaChoice = particle as XmlSchemaChoice;
			if (particle is XmlSchemaElement || particle is XmlSchemaAny)
			{
				minOccurs = particle.MinOccurs;
				maxOccurs = particle.MaxOccurs;
				return;
			}
			if (xmlSchemaChoice != null)
			{
				if (xmlSchemaChoice.Items.Count == 0)
				{
					minOccurs = (maxOccurs = 0m);
					return;
				}
				minOccurs = decimal.MaxValue;
				maxOccurs = 0m;
				for (int i = 0; i < xmlSchemaChoice.Items.Count; i++)
				{
					decimal num;
					decimal num2;
					this.CalculateEffectiveTotalRange((XmlSchemaParticle)xmlSchemaChoice.Items[i], out num, out num2);
					if (num < minOccurs)
					{
						minOccurs = num;
					}
					if (num2 > maxOccurs)
					{
						maxOccurs = num2;
					}
				}
				minOccurs *= particle.MinOccurs;
				if (maxOccurs != 79228162514264337593543950335m)
				{
					if (particle.MaxOccurs == 79228162514264337593543950335m)
					{
						maxOccurs = decimal.MaxValue;
						return;
					}
					maxOccurs *= particle.MaxOccurs;
					return;
				}
			}
			else
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				if (items.Count == 0)
				{
					minOccurs = (maxOccurs = 0m);
					return;
				}
				minOccurs = 0m;
				maxOccurs = 0m;
				for (int j = 0; j < items.Count; j++)
				{
					decimal num3;
					decimal num4;
					this.CalculateEffectiveTotalRange((XmlSchemaParticle)items[j], out num3, out num4);
					minOccurs += num3;
					if (maxOccurs != 79228162514264337593543950335m)
					{
						if (num4 == 79228162514264337593543950335m)
						{
							maxOccurs = decimal.MaxValue;
						}
						else
						{
							maxOccurs += num4;
						}
					}
				}
				minOccurs *= particle.MinOccurs;
				if (maxOccurs != 79228162514264337593543950335m)
				{
					if (particle.MaxOccurs == 79228162514264337593543950335m)
					{
						maxOccurs = decimal.MaxValue;
						return;
					}
					maxOccurs *= particle.MaxOccurs;
				}
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000B80BE File Offset: 0x000B62BE
		private void PushComplexType(XmlSchemaComplexType complexType)
		{
			this.complexTypeStack.Push(complexType);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000AE1CA File Offset: 0x000AC3CA
		private XmlSchemaContentType GetSchemaContentType(XmlSchemaComplexType complexType, XmlSchemaComplexContent complexContent, XmlSchemaParticle particle)
		{
			if ((complexContent != null && complexContent.IsMixed) || (complexContent == null && complexType.IsMixed))
			{
				return XmlSchemaContentType.Mixed;
			}
			if (particle != null && !particle.IsEmpty)
			{
				return XmlSchemaContentType.ElementOnly;
			}
			return XmlSchemaContentType.Empty;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000B80CC File Offset: 0x000B62CC
		private void CompileAttributeGroup(XmlSchemaAttributeGroup attributeGroup)
		{
			if (attributeGroup.IsProcessing)
			{
				base.SendValidationEvent("Circular attribute group reference.", attributeGroup);
				return;
			}
			if (attributeGroup.AttributeUses.Count > 0)
			{
				return;
			}
			attributeGroup.IsProcessing = true;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = attributeGroup.AnyAttribute;
			try
			{
				for (int i = 0; i < attributeGroup.Attributes.Count; i++)
				{
					XmlSchemaAttribute xmlSchemaAttribute = attributeGroup.Attributes[i] as XmlSchemaAttribute;
					if (xmlSchemaAttribute != null)
					{
						if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
						{
							this.CompileAttribute(xmlSchemaAttribute);
							if (attributeGroup.AttributeUses[xmlSchemaAttribute.QualifiedName] == null)
							{
								attributeGroup.AttributeUses.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
							}
							else
							{
								base.SendValidationEvent("The attribute '{0}' already exists.", xmlSchemaAttribute.QualifiedName.ToString(), xmlSchemaAttribute);
							}
						}
					}
					else
					{
						XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)attributeGroup.Attributes[i];
						XmlSchemaAttributeGroup xmlSchemaAttributeGroup;
						if (attributeGroup.Redefined != null && xmlSchemaAttributeGroupRef.RefName == attributeGroup.Redefined.QualifiedName)
						{
							xmlSchemaAttributeGroup = attributeGroup.Redefined;
						}
						else
						{
							xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)this.attributeGroups[xmlSchemaAttributeGroupRef.RefName];
						}
						if (xmlSchemaAttributeGroup != null)
						{
							this.CompileAttributeGroup(xmlSchemaAttributeGroup);
							foreach (object obj in xmlSchemaAttributeGroup.AttributeUses.Values)
							{
								XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj;
								if (attributeGroup.AttributeUses[xmlSchemaAttribute2.QualifiedName] == null)
								{
									attributeGroup.AttributeUses.Add(xmlSchemaAttribute2.QualifiedName, xmlSchemaAttribute2);
								}
								else
								{
									base.SendValidationEvent("The attribute '{0}' already exists.", xmlSchemaAttribute2.QualifiedName.ToString(), xmlSchemaAttribute2);
								}
							}
							xmlSchemaAnyAttribute = this.CompileAnyAttributeIntersection(xmlSchemaAnyAttribute, xmlSchemaAttributeGroup.AttributeWildcard);
						}
						else
						{
							base.SendValidationEvent("Reference to undeclared attribute group '{0}'.", xmlSchemaAttributeGroupRef.RefName.ToString(), xmlSchemaAttributeGroupRef);
						}
					}
				}
				attributeGroup.AttributeWildcard = xmlSchemaAnyAttribute;
			}
			finally
			{
				attributeGroup.IsProcessing = false;
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x000B82EC File Offset: 0x000B64EC
		private void CompileLocalAttributes(XmlSchemaComplexType baseType, XmlSchemaComplexType derivedType, XmlSchemaObjectCollection attributes, XmlSchemaAnyAttribute anyAttribute, XmlSchemaDerivationMethod derivedBy)
		{
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = ((baseType != null) ? baseType.AttributeWildcard : null);
			for (int i = 0; i < attributes.Count; i++)
			{
				XmlSchemaAttribute xmlSchemaAttribute = attributes[i] as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited)
					{
						this.CompileAttribute(xmlSchemaAttribute);
					}
					if (xmlSchemaAttribute.Use != XmlSchemaUse.Prohibited || (xmlSchemaAttribute.Use == XmlSchemaUse.Prohibited && derivedBy == XmlSchemaDerivationMethod.Restriction && baseType != XmlSchemaComplexType.AnyType))
					{
						if (derivedType.AttributeUses[xmlSchemaAttribute.QualifiedName] == null)
						{
							derivedType.AttributeUses.Add(xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute);
						}
						else
						{
							base.SendValidationEvent("The attribute '{0}' already exists.", xmlSchemaAttribute.QualifiedName.ToString(), xmlSchemaAttribute);
						}
					}
					else
					{
						base.SendValidationEvent("The '{0}' attribute is ignored, because the value of 'prohibited' for attribute use only prevents inheritance of an identically named attribute from the base type definition.", xmlSchemaAttribute.QualifiedName.ToString(), xmlSchemaAttribute, XmlSeverityType.Warning);
					}
				}
				else
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)attributes[i];
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)this.attributeGroups[xmlSchemaAttributeGroupRef.RefName];
					if (xmlSchemaAttributeGroup != null)
					{
						this.CompileAttributeGroup(xmlSchemaAttributeGroup);
						foreach (object obj in xmlSchemaAttributeGroup.AttributeUses.Values)
						{
							XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj;
							if (xmlSchemaAttribute2.Use != XmlSchemaUse.Prohibited || (xmlSchemaAttribute2.Use == XmlSchemaUse.Prohibited && derivedBy == XmlSchemaDerivationMethod.Restriction && baseType != XmlSchemaComplexType.AnyType))
							{
								if (derivedType.AttributeUses[xmlSchemaAttribute2.QualifiedName] == null)
								{
									derivedType.AttributeUses.Add(xmlSchemaAttribute2.QualifiedName, xmlSchemaAttribute2);
								}
								else
								{
									base.SendValidationEvent("The attribute '{0}' already exists.", xmlSchemaAttribute2.QualifiedName.ToString(), xmlSchemaAttributeGroupRef);
								}
							}
							else
							{
								base.SendValidationEvent("The '{0}' attribute is ignored, because the value of 'prohibited' for attribute use only prevents inheritance of an identically named attribute from the base type definition.", xmlSchemaAttribute2.QualifiedName.ToString(), xmlSchemaAttribute2, XmlSeverityType.Warning);
							}
						}
						anyAttribute = this.CompileAnyAttributeIntersection(anyAttribute, xmlSchemaAttributeGroup.AttributeWildcard);
					}
					else
					{
						base.SendValidationEvent("Reference to undeclared attribute group '{0}'.", xmlSchemaAttributeGroupRef.RefName.ToString(), xmlSchemaAttributeGroupRef);
					}
				}
			}
			if (baseType != null)
			{
				if (derivedBy == XmlSchemaDerivationMethod.Extension)
				{
					derivedType.SetAttributeWildcard(this.CompileAnyAttributeUnion(anyAttribute, xmlSchemaAnyAttribute));
					using (IEnumerator enumerator = baseType.AttributeUses.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							XmlSchemaAttribute xmlSchemaAttribute3 = (XmlSchemaAttribute)obj2;
							XmlSchemaAttribute xmlSchemaAttribute4 = (XmlSchemaAttribute)derivedType.AttributeUses[xmlSchemaAttribute3.QualifiedName];
							if (xmlSchemaAttribute4 == null)
							{
								derivedType.AttributeUses.Add(xmlSchemaAttribute3.QualifiedName, xmlSchemaAttribute3);
							}
							else if (xmlSchemaAttribute3.Use != XmlSchemaUse.Prohibited && xmlSchemaAttribute4.AttributeSchemaType != xmlSchemaAttribute3.AttributeSchemaType)
							{
								base.SendValidationEvent("Invalid attribute extension.", xmlSchemaAttribute4);
							}
						}
						return;
					}
				}
				if (anyAttribute != null && (xmlSchemaAnyAttribute == null || !XmlSchemaAnyAttribute.IsSubset(anyAttribute, xmlSchemaAnyAttribute) || !this.IsProcessContentsRestricted(baseType, anyAttribute, xmlSchemaAnyAttribute)))
				{
					base.SendValidationEvent("The base any attribute must be a superset of the derived 'anyAttribute'.", derivedType);
				}
				else
				{
					derivedType.SetAttributeWildcard(anyAttribute);
				}
				foreach (object obj3 in baseType.AttributeUses.Values)
				{
					XmlSchemaAttribute xmlSchemaAttribute5 = (XmlSchemaAttribute)obj3;
					XmlSchemaAttribute xmlSchemaAttribute6 = (XmlSchemaAttribute)derivedType.AttributeUses[xmlSchemaAttribute5.QualifiedName];
					if (xmlSchemaAttribute6 == null)
					{
						derivedType.AttributeUses.Add(xmlSchemaAttribute5.QualifiedName, xmlSchemaAttribute5);
					}
					else if (xmlSchemaAttribute5.Use == XmlSchemaUse.Prohibited && xmlSchemaAttribute6.Use != XmlSchemaUse.Prohibited)
					{
						base.SendValidationEvent("Invalid attribute restriction. Attribute restriction is prohibited in base type.", xmlSchemaAttribute6);
					}
					else if (xmlSchemaAttribute5.Use == XmlSchemaUse.Required && xmlSchemaAttribute6.Use != XmlSchemaUse.Required)
					{
						base.SendValidationEvent("Derived attribute's use has to be required if base attribute's use is required.", xmlSchemaAttribute6);
					}
					else if (xmlSchemaAttribute6.Use != XmlSchemaUse.Prohibited)
					{
						if (xmlSchemaAttribute5.AttributeSchemaType == null || xmlSchemaAttribute6.AttributeSchemaType == null || !XmlSchemaType.IsDerivedFrom(xmlSchemaAttribute6.AttributeSchemaType, xmlSchemaAttribute5.AttributeSchemaType, XmlSchemaDerivationMethod.Empty))
						{
							base.SendValidationEvent("Invalid attribute restriction. Derived attribute's type is not a valid restriction of the base attribute's type.", xmlSchemaAttribute6);
						}
						else if (!this.IsFixedEqual(xmlSchemaAttribute5.AttDef, xmlSchemaAttribute6.AttDef))
						{
							base.SendValidationEvent("Invalid attribute restriction. Derived attribute's fixed value must be the same as the base attribute's fixed value.", xmlSchemaAttribute6);
						}
					}
				}
				using (IEnumerator enumerator = derivedType.AttributeUses.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj4 = enumerator.Current;
						XmlSchemaAttribute xmlSchemaAttribute7 = (XmlSchemaAttribute)obj4;
						if ((XmlSchemaAttribute)baseType.AttributeUses[xmlSchemaAttribute7.QualifiedName] == null && (xmlSchemaAnyAttribute == null || !xmlSchemaAnyAttribute.Allows(xmlSchemaAttribute7.QualifiedName)))
						{
							base.SendValidationEvent("The {base type definition} must have an {attribute wildcard} and the {target namespace} of the R's {attribute declaration} must be valid with respect to that wildcard.", xmlSchemaAttribute7);
						}
					}
					return;
				}
			}
			derivedType.SetAttributeWildcard(anyAttribute);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000B87E8 File Offset: 0x000B69E8
		private void CheckAtrributeGroupRestriction(XmlSchemaAttributeGroup baseAttributeGroup, XmlSchemaAttributeGroup derivedAttributeGroup)
		{
			XmlSchemaAnyAttribute attributeWildcard = baseAttributeGroup.AttributeWildcard;
			XmlSchemaAnyAttribute attributeWildcard2 = derivedAttributeGroup.AttributeWildcard;
			if (attributeWildcard2 != null && (attributeWildcard == null || !XmlSchemaAnyAttribute.IsSubset(attributeWildcard2, attributeWildcard) || !this.IsProcessContentsRestricted(null, attributeWildcard2, attributeWildcard)))
			{
				base.SendValidationEvent("The base any attribute must be a superset of the derived 'anyAttribute'.", derivedAttributeGroup);
			}
			foreach (object obj in baseAttributeGroup.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)derivedAttributeGroup.AttributeUses[xmlSchemaAttribute.QualifiedName];
				if (xmlSchemaAttribute2 != null)
				{
					if (xmlSchemaAttribute.Use == XmlSchemaUse.Prohibited && xmlSchemaAttribute2.Use != XmlSchemaUse.Prohibited)
					{
						base.SendValidationEvent("Invalid attribute restriction. Attribute restriction is prohibited in base type.", xmlSchemaAttribute2);
					}
					else if (xmlSchemaAttribute.Use == XmlSchemaUse.Required && xmlSchemaAttribute2.Use != XmlSchemaUse.Required)
					{
						base.SendValidationEvent("Derived attribute's use has to be required if base attribute's use is required.", xmlSchemaAttribute2);
					}
					else if (xmlSchemaAttribute2.Use != XmlSchemaUse.Prohibited)
					{
						if (xmlSchemaAttribute.AttributeSchemaType == null || xmlSchemaAttribute2.AttributeSchemaType == null || !XmlSchemaType.IsDerivedFrom(xmlSchemaAttribute2.AttributeSchemaType, xmlSchemaAttribute.AttributeSchemaType, XmlSchemaDerivationMethod.Empty))
						{
							base.SendValidationEvent("Invalid attribute restriction. Derived attribute's type is not a valid restriction of the base attribute's type.", xmlSchemaAttribute2);
						}
						else if (!this.IsFixedEqual(xmlSchemaAttribute.AttDef, xmlSchemaAttribute2.AttDef))
						{
							base.SendValidationEvent("Invalid attribute restriction. Derived attribute's fixed value must be the same as the base attribute's fixed value.", xmlSchemaAttribute2);
						}
					}
				}
				else if (xmlSchemaAttribute.Use == XmlSchemaUse.Required)
				{
					base.SendValidationEvent("The base attribute '{0}' whose use = 'required' does not have a corresponding derived attribute while redefining attribute group '{1}'.", xmlSchemaAttribute.QualifiedName.ToString(), baseAttributeGroup.QualifiedName.ToString(), derivedAttributeGroup);
				}
			}
			foreach (object obj2 in derivedAttributeGroup.AttributeUses.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute3 = (XmlSchemaAttribute)obj2;
				if ((XmlSchemaAttribute)baseAttributeGroup.AttributeUses[xmlSchemaAttribute3.QualifiedName] == null && (attributeWildcard == null || !attributeWildcard.Allows(xmlSchemaAttribute3.QualifiedName)))
				{
					base.SendValidationEvent("The {base type definition} must have an {attribute wildcard} and the {target namespace} of the R's {attribute declaration} must be valid with respect to that wildcard.", xmlSchemaAttribute3);
				}
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000B8A10 File Offset: 0x000B6C10
		private bool IsProcessContentsRestricted(XmlSchemaComplexType baseType, XmlSchemaAnyAttribute derivedAttributeWildcard, XmlSchemaAnyAttribute baseAttributeWildcard)
		{
			return baseType == XmlSchemaComplexType.AnyType || derivedAttributeWildcard.ProcessContentsCorrect >= baseAttributeWildcard.ProcessContentsCorrect;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000B8A2D File Offset: 0x000B6C2D
		private XmlSchemaAnyAttribute CompileAnyAttributeUnion(XmlSchemaAnyAttribute a, XmlSchemaAnyAttribute b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Union(a, b, false);
			if (xmlSchemaAnyAttribute == null)
			{
				base.SendValidationEvent("The 'anyAttribute' is not expressible.", a);
			}
			return xmlSchemaAnyAttribute;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000B8A50 File Offset: 0x000B6C50
		private XmlSchemaAnyAttribute CompileAnyAttributeIntersection(XmlSchemaAnyAttribute a, XmlSchemaAnyAttribute b)
		{
			if (a == null)
			{
				return b;
			}
			if (b == null)
			{
				return a;
			}
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = XmlSchemaAnyAttribute.Intersection(a, b, false);
			if (xmlSchemaAnyAttribute == null)
			{
				base.SendValidationEvent("The 'anyAttribute' is not expressible.", a);
			}
			return xmlSchemaAnyAttribute;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000B8A74 File Offset: 0x000B6C74
		private void CompileAttribute(XmlSchemaAttribute xa)
		{
			if (xa.IsProcessing)
			{
				base.SendValidationEvent("Circular attribute reference.", xa);
				return;
			}
			if (xa.AttDef != null)
			{
				return;
			}
			xa.IsProcessing = true;
			try
			{
				SchemaAttDef schemaAttDef;
				if (!xa.RefName.IsEmpty)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)this.attributes[xa.RefName];
					if (xmlSchemaAttribute == null)
					{
						throw new XmlSchemaException("The '{0}' attribute is not declared.", xa.RefName.ToString(), xa);
					}
					this.CompileAttribute(xmlSchemaAttribute);
					if (xmlSchemaAttribute.AttDef == null)
					{
						throw new XmlSchemaException("Reference to invalid attribute '{0}'.", xa.RefName.ToString(), xa);
					}
					schemaAttDef = xmlSchemaAttribute.AttDef.Clone();
					XmlSchemaDatatype datatype = schemaAttDef.Datatype;
					if (datatype != null)
					{
						if (xmlSchemaAttribute.FixedValue == null && xmlSchemaAttribute.DefaultValue == null)
						{
							this.SetDefaultFixed(xa, schemaAttDef);
						}
						else if (xmlSchemaAttribute.FixedValue != null)
						{
							if (xa.DefaultValue != null)
							{
								throw new XmlSchemaException("The default value constraint cannot be present on the '{0}' attribute reference if the fixed value constraint is present on the declaration.", xa.RefName.ToString(), xa);
							}
							if (xa.FixedValue != null)
							{
								object obj = datatype.ParseValue(xa.FixedValue, base.NameTable, new SchemaNamespaceManager(xa), true);
								if (!datatype.IsEqual(schemaAttDef.DefaultValueTyped, obj))
								{
									throw new XmlSchemaException("The fixed value constraint on the '{0}' attribute reference must match the fixed value constraint on the declaration.", xa.RefName.ToString(), xa);
								}
							}
						}
					}
					xa.SetAttributeType(xmlSchemaAttribute.AttributeSchemaType);
				}
				else
				{
					schemaAttDef = new SchemaAttDef(xa.QualifiedName);
					if (xa.SchemaType != null)
					{
						this.CompileSimpleType(xa.SchemaType);
						xa.SetAttributeType(xa.SchemaType);
						schemaAttDef.SchemaType = xa.SchemaType;
						schemaAttDef.Datatype = xa.SchemaType.Datatype;
					}
					else if (!xa.SchemaTypeName.IsEmpty)
					{
						XmlSchemaSimpleType simpleType = this.GetSimpleType(xa.SchemaTypeName);
						if (simpleType == null)
						{
							throw new XmlSchemaException("Type '{0}' is not declared, or is not a simple type.", xa.SchemaTypeName.ToString(), xa);
						}
						xa.SetAttributeType(simpleType);
						schemaAttDef.Datatype = simpleType.Datatype;
						schemaAttDef.SchemaType = simpleType;
					}
					else
					{
						schemaAttDef.SchemaType = DatatypeImplementation.AnySimpleType;
						schemaAttDef.Datatype = DatatypeImplementation.AnySimpleType.Datatype;
						xa.SetAttributeType(DatatypeImplementation.AnySimpleType);
					}
					if (schemaAttDef.Datatype != null)
					{
						schemaAttDef.Datatype.VerifySchemaValid(this.notations, xa);
					}
					this.SetDefaultFixed(xa, schemaAttDef);
				}
				schemaAttDef.SchemaAttribute = xa;
				xa.AttDef = schemaAttDef;
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(xa);
				}
				base.SendValidationEvent(ex);
				xa.AttDef = SchemaAttDef.Empty;
			}
			finally
			{
				xa.IsProcessing = false;
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000B8D20 File Offset: 0x000B6F20
		private void SetDefaultFixed(XmlSchemaAttribute xa, SchemaAttDef decl)
		{
			if (xa.DefaultValue != null || xa.FixedValue != null)
			{
				if (xa.DefaultValue != null)
				{
					decl.Presence = SchemaDeclBase.Use.Default;
					decl.DefaultValueRaw = (decl.DefaultValueExpanded = xa.DefaultValue);
				}
				else
				{
					if (xa.Use == XmlSchemaUse.Required)
					{
						decl.Presence = SchemaDeclBase.Use.RequiredFixed;
					}
					else
					{
						decl.Presence = SchemaDeclBase.Use.Fixed;
					}
					decl.DefaultValueRaw = (decl.DefaultValueExpanded = xa.FixedValue);
				}
				if (decl.Datatype != null)
				{
					if (decl.Datatype.TypeCode == XmlTypeCode.Id)
					{
						base.SendValidationEvent("An attribute or element of type xs:ID or derived from xs:ID, should not have a value constraint.", xa);
						return;
					}
					decl.DefaultValueTyped = decl.Datatype.ParseValue(decl.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xa), true);
					return;
				}
			}
			else
			{
				switch (xa.Use)
				{
				case XmlSchemaUse.None:
				case XmlSchemaUse.Optional:
					decl.Presence = SchemaDeclBase.Use.Implied;
					return;
				case XmlSchemaUse.Prohibited:
					break;
				case XmlSchemaUse.Required:
					decl.Presence = SchemaDeclBase.Use.Required;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000B8E0C File Offset: 0x000B700C
		private void CompileIdentityConstraint(XmlSchemaIdentityConstraint xi)
		{
			if (xi.IsProcessing)
			{
				xi.CompiledConstraint = CompiledIdentityConstraint.Empty;
				base.SendValidationEvent("Circular identity constraint reference.", xi);
				return;
			}
			if (xi.CompiledConstraint != null)
			{
				return;
			}
			xi.IsProcessing = true;
			try
			{
				SchemaNamespaceManager schemaNamespaceManager = new SchemaNamespaceManager(xi);
				CompiledIdentityConstraint compiledIdentityConstraint = new CompiledIdentityConstraint(xi, schemaNamespaceManager);
				if (xi is XmlSchemaKeyref)
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)this.identityConstraints[((XmlSchemaKeyref)xi).Refer];
					if (xmlSchemaIdentityConstraint == null)
					{
						throw new XmlSchemaException("The '{0}' identity constraint is not declared.", ((XmlSchemaKeyref)xi).Refer.ToString(), xi);
					}
					this.CompileIdentityConstraint(xmlSchemaIdentityConstraint);
					if (xmlSchemaIdentityConstraint.CompiledConstraint == null)
					{
						throw new XmlSchemaException("Reference to an invalid identity constraint, '{0}'.", ((XmlSchemaKeyref)xi).Refer.ToString(), xi);
					}
					if (xmlSchemaIdentityConstraint.Fields.Count != xi.Fields.Count)
					{
						throw new XmlSchemaException("Keyref '{0}' has different cardinality as the referred key or unique element.", xi.QualifiedName.ToString(), xi);
					}
					if (xmlSchemaIdentityConstraint.CompiledConstraint.Role == CompiledIdentityConstraint.ConstraintRole.Keyref)
					{
						throw new XmlSchemaException("The '{0}' Keyref can refer to key or unique only.", xi.QualifiedName.ToString(), xi);
					}
				}
				xi.CompiledConstraint = compiledIdentityConstraint;
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(xi);
				}
				base.SendValidationEvent(ex);
				xi.CompiledConstraint = CompiledIdentityConstraint.Empty;
			}
			finally
			{
				xi.IsProcessing = false;
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000B8F88 File Offset: 0x000B7188
		private void CompileElement(XmlSchemaElement xe)
		{
			if (xe.IsProcessing)
			{
				base.SendValidationEvent("Circular element reference.", xe);
				return;
			}
			if (xe.ElementDecl != null)
			{
				return;
			}
			xe.IsProcessing = true;
			SchemaElementDecl schemaElementDecl = null;
			try
			{
				if (!xe.RefName.IsEmpty)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.elements[xe.RefName];
					if (xmlSchemaElement == null)
					{
						throw new XmlSchemaException("The '{0}' element is not declared.", xe.RefName.ToString(), xe);
					}
					this.CompileElement(xmlSchemaElement);
					if (xmlSchemaElement.ElementDecl == null)
					{
						throw new XmlSchemaException("Reference to invalid element '{0}'.", xe.RefName.ToString(), xe);
					}
					xe.SetElementType(xmlSchemaElement.ElementSchemaType);
					schemaElementDecl = xmlSchemaElement.ElementDecl.Clone();
				}
				else
				{
					if (xe.SchemaType != null)
					{
						xe.SetElementType(xe.SchemaType);
					}
					else if (!xe.SchemaTypeName.IsEmpty)
					{
						xe.SetElementType(this.GetAnySchemaType(xe.SchemaTypeName));
						if (xe.ElementSchemaType == null)
						{
							throw new XmlSchemaException("Type '{0}' is not declared.", xe.SchemaTypeName.ToString(), xe);
						}
					}
					else if (!xe.SubstitutionGroup.IsEmpty)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)this.elements[xe.SubstitutionGroup];
						if (xmlSchemaElement2 == null)
						{
							throw new XmlSchemaException("Substitution group refers to '{0}', an undeclared element.", xe.SubstitutionGroup.Name.ToString(CultureInfo.InvariantCulture), xe);
						}
						if (xmlSchemaElement2.IsProcessing)
						{
							return;
						}
						this.CompileElement(xmlSchemaElement2);
						if (xmlSchemaElement2.ElementDecl == null)
						{
							xe.SetElementType(XmlSchemaComplexType.AnyType);
							schemaElementDecl = XmlSchemaComplexType.AnyType.ElementDecl.Clone();
						}
						else
						{
							xe.SetElementType(xmlSchemaElement2.ElementSchemaType);
							schemaElementDecl = xmlSchemaElement2.ElementDecl.Clone();
						}
					}
					else
					{
						xe.SetElementType(XmlSchemaComplexType.AnyType);
						schemaElementDecl = XmlSchemaComplexType.AnyType.ElementDecl.Clone();
					}
					if (schemaElementDecl == null)
					{
						if (xe.ElementSchemaType is XmlSchemaComplexType)
						{
							XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)xe.ElementSchemaType;
							this.CompileComplexType(xmlSchemaComplexType);
							if (xmlSchemaComplexType.ElementDecl != null)
							{
								schemaElementDecl = xmlSchemaComplexType.ElementDecl.Clone();
							}
						}
						else if (xe.ElementSchemaType is XmlSchemaSimpleType)
						{
							XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)xe.ElementSchemaType;
							this.CompileSimpleType(xmlSchemaSimpleType);
							if (xmlSchemaSimpleType.ElementDecl != null)
							{
								schemaElementDecl = xmlSchemaSimpleType.ElementDecl.Clone();
							}
						}
					}
					schemaElementDecl.Name = xe.QualifiedName;
					schemaElementDecl.IsAbstract = xe.IsAbstract;
					XmlSchemaComplexType xmlSchemaComplexType2 = xe.ElementSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType2 != null)
					{
						schemaElementDecl.IsAbstract |= xmlSchemaComplexType2.IsAbstract;
					}
					schemaElementDecl.IsNillable = xe.IsNillable;
					schemaElementDecl.Block |= xe.BlockResolved;
				}
				if (schemaElementDecl.Datatype != null)
				{
					schemaElementDecl.Datatype.VerifySchemaValid(this.notations, xe);
				}
				if ((xe.DefaultValue != null || xe.FixedValue != null) && schemaElementDecl.ContentValidator != null)
				{
					if (schemaElementDecl.ContentValidator.ContentType != XmlSchemaContentType.TextOnly && (schemaElementDecl.ContentValidator.ContentType != XmlSchemaContentType.Mixed || !schemaElementDecl.ContentValidator.IsEmptiable))
					{
						throw new XmlSchemaException("Element's type does not allow fixed or default value constraint.", xe);
					}
					if (xe.DefaultValue != null)
					{
						schemaElementDecl.Presence = SchemaDeclBase.Use.Default;
						schemaElementDecl.DefaultValueRaw = xe.DefaultValue;
					}
					else
					{
						schemaElementDecl.Presence = SchemaDeclBase.Use.Fixed;
						schemaElementDecl.DefaultValueRaw = xe.FixedValue;
					}
					if (schemaElementDecl.Datatype != null)
					{
						if (schemaElementDecl.Datatype.TypeCode == XmlTypeCode.Id)
						{
							base.SendValidationEvent("An attribute or element of type xs:ID or derived from xs:ID, should not have a value constraint.", xe);
						}
						else
						{
							schemaElementDecl.DefaultValueTyped = schemaElementDecl.Datatype.ParseValue(schemaElementDecl.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xe), true);
						}
					}
					else
					{
						schemaElementDecl.DefaultValueTyped = DatatypeImplementation.AnySimpleType.Datatype.ParseValue(schemaElementDecl.DefaultValueRaw, base.NameTable, new SchemaNamespaceManager(xe));
					}
				}
				if (xe.HasConstraints)
				{
					XmlSchemaObjectCollection constraints = xe.Constraints;
					CompiledIdentityConstraint[] array = new CompiledIdentityConstraint[constraints.Count];
					int num = 0;
					for (int i = 0; i < constraints.Count; i++)
					{
						XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)constraints[i];
						this.CompileIdentityConstraint(xmlSchemaIdentityConstraint);
						array[num++] = xmlSchemaIdentityConstraint.CompiledConstraint;
					}
					schemaElementDecl.Constraints = array;
				}
				schemaElementDecl.SchemaElement = xe;
				xe.ElementDecl = schemaElementDecl;
			}
			catch (XmlSchemaException ex)
			{
				if (ex.SourceSchemaObject == null)
				{
					ex.SetSource(xe);
				}
				base.SendValidationEvent(ex);
				xe.ElementDecl = SchemaElementDecl.Empty;
			}
			finally
			{
				xe.IsProcessing = false;
			}
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000B9410 File Offset: 0x000B7610
		private ContentValidator CompileComplexContent(XmlSchemaComplexType complexType)
		{
			if (complexType.ContentType == XmlSchemaContentType.Empty)
			{
				return ContentValidator.Empty;
			}
			if (complexType.ContentType == XmlSchemaContentType.TextOnly)
			{
				return ContentValidator.TextOnly;
			}
			XmlSchemaParticle contentTypeParticle = complexType.ContentTypeParticle;
			if (contentTypeParticle == null || contentTypeParticle == XmlSchemaParticle.Empty)
			{
				if (complexType.ContentType == XmlSchemaContentType.ElementOnly)
				{
					return ContentValidator.Empty;
				}
				return ContentValidator.Mixed;
			}
			else
			{
				this.PushComplexType(complexType);
				if (contentTypeParticle is XmlSchemaAll)
				{
					XmlSchemaAll xmlSchemaAll = (XmlSchemaAll)contentTypeParticle;
					AllElementsContentValidator allElementsContentValidator = new AllElementsContentValidator(complexType.ContentType, xmlSchemaAll.Items.Count, xmlSchemaAll.MinOccurs == 0m);
					for (int i = 0; i < xmlSchemaAll.Items.Count; i++)
					{
						XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaAll.Items[i];
						if (!allElementsContentValidator.AddElement(xmlSchemaElement.QualifiedName, xmlSchemaElement, xmlSchemaElement.MinOccurs == 0m))
						{
							base.SendValidationEvent("The '{0}' element already exists in the content model.", xmlSchemaElement.QualifiedName.ToString(), xmlSchemaElement);
						}
					}
					return allElementsContentValidator;
				}
				ParticleContentValidator particleContentValidator = new ParticleContentValidator(complexType.ContentType, base.CompilationSettings.EnableUpaCheck);
				ContentValidator contentValidator;
				try
				{
					particleContentValidator.Start();
					complexType.HasWildCard = this.BuildParticleContentModel(particleContentValidator, contentTypeParticle);
					contentValidator = particleContentValidator.Finish(true);
				}
				catch (UpaException ex)
				{
					if (ex.Particle1 is XmlSchemaElement)
					{
						if (ex.Particle2 is XmlSchemaElement)
						{
							base.SendValidationEvent("Multiple definition of element '{0}' causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.", ((XmlSchemaElement)ex.Particle1).QualifiedName.ToString(), (XmlSchemaElement)ex.Particle2);
						}
						else
						{
							base.SendValidationEvent("Wildcard '{0}' allows element '{1}', and causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.", ((XmlSchemaAny)ex.Particle2).ResolvedNamespace, ((XmlSchemaElement)ex.Particle1).QualifiedName.ToString(), (XmlSchemaAny)ex.Particle2);
						}
					}
					else if (ex.Particle2 is XmlSchemaElement)
					{
						base.SendValidationEvent("Wildcard '{0}' allows element '{1}', and causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.", ((XmlSchemaAny)ex.Particle1).ResolvedNamespace, ((XmlSchemaElement)ex.Particle2).QualifiedName.ToString(), (XmlSchemaElement)ex.Particle2);
					}
					else
					{
						base.SendValidationEvent("Wildcards '{0}' and '{1}' have not empty intersection, and causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.", ((XmlSchemaAny)ex.Particle1).ResolvedNamespace, ((XmlSchemaAny)ex.Particle2).ResolvedNamespace, (XmlSchemaAny)ex.Particle2);
					}
					contentValidator = XmlSchemaComplexType.AnyTypeContentValidator;
				}
				catch (NotSupportedException)
				{
					base.SendValidationEvent("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.", complexType, XmlSeverityType.Warning);
					contentValidator = XmlSchemaComplexType.AnyTypeContentValidator;
				}
				return contentValidator;
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000B96B8 File Offset: 0x000B78B8
		private bool BuildParticleContentModel(ParticleContentValidator contentValidator, XmlSchemaParticle particle)
		{
			bool flag = false;
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)particle;
				contentValidator.AddName(xmlSchemaElement.QualifiedName, xmlSchemaElement);
			}
			else if (particle is XmlSchemaAny)
			{
				flag = true;
				XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle;
				contentValidator.AddNamespaceList(xmlSchemaAny.NamespaceList, xmlSchemaAny);
			}
			else if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				bool flag2 = particle is XmlSchemaChoice;
				contentValidator.OpenGroup();
				bool flag3 = true;
				for (int i = 0; i < items.Count; i++)
				{
					if (flag3)
					{
						flag3 = false;
					}
					else if (flag2)
					{
						contentValidator.AddChoice();
					}
					else
					{
						contentValidator.AddSequence();
					}
					flag = this.BuildParticleContentModel(contentValidator, (XmlSchemaParticle)items[i]);
				}
				contentValidator.CloseGroup();
			}
			if (!(particle.MinOccurs == 1m) || !(particle.MaxOccurs == 1m))
			{
				if (particle.MinOccurs == 0m && particle.MaxOccurs == 1m)
				{
					contentValidator.AddQMark();
				}
				else if (particle.MinOccurs == 0m && particle.MaxOccurs == 79228162514264337593543950335m)
				{
					contentValidator.AddStar();
				}
				else if (particle.MinOccurs == 1m && particle.MaxOccurs == 79228162514264337593543950335m)
				{
					contentValidator.AddPlus();
				}
				else
				{
					contentValidator.AddLeafRange(particle.MinOccurs, particle.MaxOccurs);
				}
			}
			return flag;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000B9844 File Offset: 0x000B7A44
		private void CompileParticleElements(XmlSchemaComplexType complexType, XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)particle;
				this.CompileElement(xmlSchemaElement);
				if (complexType.LocalElements[xmlSchemaElement.QualifiedName] == null)
				{
					complexType.LocalElements.Add(xmlSchemaElement.QualifiedName, xmlSchemaElement);
					return;
				}
				if (((XmlSchemaElement)complexType.LocalElements[xmlSchemaElement.QualifiedName]).ElementSchemaType != xmlSchemaElement.ElementSchemaType)
				{
					base.SendValidationEvent("Elements with the same name and in the same scope must have the same type.", particle);
					return;
				}
			}
			else if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				for (int i = 0; i < items.Count; i++)
				{
					this.CompileParticleElements(complexType, (XmlSchemaParticle)items[i]);
				}
			}
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000B98F8 File Offset: 0x000B7AF8
		private void CompileParticleElements(XmlSchemaParticle particle)
		{
			if (particle is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)particle;
				this.CompileElement(xmlSchemaElement);
				return;
			}
			if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaObjectCollection items = ((XmlSchemaGroupBase)particle).Items;
				for (int i = 0; i < items.Count; i++)
				{
					this.CompileParticleElements((XmlSchemaParticle)items[i]);
				}
			}
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000B9954 File Offset: 0x000B7B54
		private void CompileComplexTypeElements(XmlSchemaComplexType complexType)
		{
			if (complexType.IsProcessing)
			{
				base.SendValidationEvent("Circular type reference.", complexType);
				return;
			}
			complexType.IsProcessing = true;
			try
			{
				if (complexType.ContentTypeParticle != XmlSchemaParticle.Empty)
				{
					this.CompileParticleElements(complexType, complexType.ContentTypeParticle);
				}
			}
			finally
			{
				complexType.IsProcessing = false;
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000B99B4 File Offset: 0x000B7BB4
		private XmlSchemaSimpleType GetSimpleType(XmlQualifiedName name)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = this.schemaTypes[name] as XmlSchemaSimpleType;
			if (xmlSchemaSimpleType != null)
			{
				this.CompileSimpleType(xmlSchemaSimpleType);
			}
			else
			{
				xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromXsdType(name);
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000B99E8 File Offset: 0x000B7BE8
		private XmlSchemaComplexType GetComplexType(XmlQualifiedName name)
		{
			XmlSchemaComplexType xmlSchemaComplexType = this.schemaTypes[name] as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null)
			{
				this.CompileComplexType(xmlSchemaComplexType);
			}
			return xmlSchemaComplexType;
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000B9A14 File Offset: 0x000B7C14
		private XmlSchemaType GetAnySchemaType(XmlQualifiedName name)
		{
			XmlSchemaType xmlSchemaType = (XmlSchemaType)this.schemaTypes[name];
			if (xmlSchemaType != null)
			{
				if (xmlSchemaType is XmlSchemaComplexType)
				{
					this.CompileComplexType((XmlSchemaComplexType)xmlSchemaType);
				}
				else
				{
					this.CompileSimpleType((XmlSchemaSimpleType)xmlSchemaType);
				}
				return xmlSchemaType;
			}
			return DatatypeImplementation.GetSimpleTypeFromXsdType(name);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000B9A60 File Offset: 0x000B7C60
		private void CopyPosition(XmlSchemaAnnotated to, XmlSchemaAnnotated from, bool copyParent)
		{
			to.SourceUri = from.SourceUri;
			to.LinePosition = from.LinePosition;
			to.LineNumber = from.LineNumber;
			to.SetUnhandledAttributes(from.UnhandledAttributes);
			if (copyParent)
			{
				to.Parent = from.Parent;
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000B9AAC File Offset: 0x000B7CAC
		private bool IsFixedEqual(SchemaDeclBase baseDecl, SchemaDeclBase derivedDecl)
		{
			if (baseDecl.Presence == SchemaDeclBase.Use.Fixed || baseDecl.Presence == SchemaDeclBase.Use.RequiredFixed)
			{
				object defaultValueTyped = baseDecl.DefaultValueTyped;
				object defaultValueTyped2 = derivedDecl.DefaultValueTyped;
				if (derivedDecl.Presence != SchemaDeclBase.Use.Fixed && derivedDecl.Presence != SchemaDeclBase.Use.RequiredFixed)
				{
					return false;
				}
				XmlSchemaDatatype datatype = baseDecl.Datatype;
				XmlSchemaDatatype datatype2 = derivedDecl.Datatype;
				if (datatype.Variety == XmlSchemaDatatypeVariety.Union)
				{
					if (datatype2.Variety == XmlSchemaDatatypeVariety.Union)
					{
						if (!datatype2.IsEqual(defaultValueTyped, defaultValueTyped2))
						{
							return false;
						}
					}
					else
					{
						XsdSimpleValue xsdSimpleValue = baseDecl.DefaultValueTyped as XsdSimpleValue;
						if (!xsdSimpleValue.XmlType.Datatype.IsComparable(datatype2) || !datatype2.IsEqual(xsdSimpleValue.TypedValue, defaultValueTyped2))
						{
							return false;
						}
					}
				}
				else if (!datatype2.IsEqual(defaultValueTyped, defaultValueTyped2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000E3C RID: 3644
		private string restrictionErrorMsg;

		// Token: 0x04000E3D RID: 3645
		private XmlSchemaObjectTable attributes = new XmlSchemaObjectTable();

		// Token: 0x04000E3E RID: 3646
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04000E3F RID: 3647
		private XmlSchemaObjectTable elements = new XmlSchemaObjectTable();

		// Token: 0x04000E40 RID: 3648
		private XmlSchemaObjectTable schemaTypes = new XmlSchemaObjectTable();

		// Token: 0x04000E41 RID: 3649
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();

		// Token: 0x04000E42 RID: 3650
		private XmlSchemaObjectTable notations = new XmlSchemaObjectTable();

		// Token: 0x04000E43 RID: 3651
		private XmlSchemaObjectTable examplars = new XmlSchemaObjectTable();

		// Token: 0x04000E44 RID: 3652
		private XmlSchemaObjectTable identityConstraints = new XmlSchemaObjectTable();

		// Token: 0x04000E45 RID: 3653
		private Stack complexTypeStack = new Stack();

		// Token: 0x04000E46 RID: 3654
		private Hashtable schemasToCompile = new Hashtable();

		// Token: 0x04000E47 RID: 3655
		private Hashtable importedSchemas = new Hashtable();

		// Token: 0x04000E48 RID: 3656
		private XmlSchema schemaForSchema;
	}
}
