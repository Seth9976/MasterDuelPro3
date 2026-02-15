using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace System.Xml.Serialization
{
	/// <summary>Generates types and attribute declarations from internal type mapping information for SOAP-encoded message parts defined in a WSDL document. </summary>
	// Token: 0x0200018B RID: 395
	public class SoapCodeExporter : CodeExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, assuming no code compile unit. </summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		// Token: 0x060012A1 RID: 4769 RVA: 0x00059C3A File Offset: 0x00057E3A
		public SoapCodeExporter(CodeNamespace codeNamespace)
			: base(codeNamespace, null, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code compile unit parameter in addition to a namespace parameter.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		// Token: 0x060012A2 RID: 4770 RVA: 0x00059C47 File Offset: 0x00057E47
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit)
			: base(codeNamespace, codeCompileUnit, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code namespace, a code compile unit, and code generation options.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration that specifies the options with which exported code is generated.</param>
		// Token: 0x060012A3 RID: 4771 RVA: 0x00059C47 File Offset: 0x00057E47
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options)
			: base(codeNamespace, codeCompileUnit, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code namespace, a code compile unit, code generation options, and mappings.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration that specifies the options with which exported code is generated.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x060012A4 RID: 4772 RVA: 0x00059C54 File Offset: 0x00057E54
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options, Hashtable mappings)
			: base(codeNamespace, codeCompileUnit, null, options, mappings)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> class, specifying a code namespace, a code compile unit, a code generator, code generation options, and mappings.</summary>
		/// <param name="codeNamespace">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies the namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that identifies the program graph container to which used assembly references are automatically added.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  that is used to create the code.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration that specifies the options with which exported code is generated.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x060012A5 RID: 4773 RVA: 0x00059C62 File Offset: 0x00057E62
		public SoapCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings)
			: base(codeNamespace, codeCompileUnit, codeProvider, options, mappings)
		{
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for a SOAP header. </summary>
		/// <param name="xmlTypeMapping">Internal .NET Framework type mapping information for a SOAP header element.</param>
		// Token: 0x060012A6 RID: 4774 RVA: 0x00059C71 File Offset: 0x00057E71
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			xmlTypeMapping.CheckShallow();
			base.CheckScope(xmlTypeMapping.Scope);
			this.ExportElement(xmlTypeMapping.Accessor);
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for each of the parts that belong to a SOAP message definition in a WSDL document. </summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		// Token: 0x060012A7 RID: 4775 RVA: 0x00059C94 File Offset: 0x00057E94
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			xmlMembersMapping.CheckShallow();
			base.CheckScope(xmlMembersMapping.Scope);
			for (int i = 0; i < xmlMembersMapping.Count; i++)
			{
				this.ExportElement((ElementAccessor)xmlMembersMapping[i].Accessor);
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00059CDB File Offset: 0x00057EDB
		private void ExportElement(ElementAccessor element)
		{
			this.ExportType(element.Mapping);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00059CEC File Offset: 0x00057EEC
		private void ExportType(TypeMapping mapping)
		{
			if (mapping.IsReference)
			{
				return;
			}
			if (base.ExportedMappings[mapping] == null)
			{
				CodeTypeDeclaration codeTypeDeclaration = null;
				base.ExportedMappings.Add(mapping, mapping);
				if (mapping is EnumMapping)
				{
					codeTypeDeclaration = base.ExportEnum((EnumMapping)mapping, typeof(SoapEnumAttribute));
				}
				else if (mapping is StructMapping)
				{
					codeTypeDeclaration = this.ExportStruct((StructMapping)mapping);
				}
				else if (mapping is ArrayMapping)
				{
					Accessor[] elements = ((ArrayMapping)mapping).Elements;
					this.EnsureTypesExported(elements, null);
				}
				if (codeTypeDeclaration != null)
				{
					codeTypeDeclaration.CustomAttributes.Add(base.GeneratedCodeAttribute);
					codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(SerializableAttribute).FullName));
					if (!codeTypeDeclaration.IsEnum)
					{
						codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName));
						codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DesignerCategoryAttribute).FullName, new CodeAttributeArgument[]
						{
							new CodeAttributeArgument(new CodePrimitiveExpression("code"))
						}));
					}
					base.AddTypeMetadata(codeTypeDeclaration.CustomAttributes, typeof(SoapTypeAttribute), mapping.TypeDesc.Name, Accessor.UnescapeName(mapping.TypeName), mapping.Namespace, mapping.IncludeInSchema);
					base.ExportedClasses.Add(mapping, codeTypeDeclaration);
				}
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00059E54 File Offset: 0x00058054
		private CodeTypeDeclaration ExportStruct(StructMapping mapping)
		{
			if (mapping.TypeDesc.IsRoot)
			{
				base.ExportRoot(mapping, typeof(SoapIncludeAttribute));
				return null;
			}
			if (!mapping.IncludeInSchema)
			{
				return null;
			}
			string name = mapping.TypeDesc.Name;
			string text = ((mapping.TypeDesc.BaseTypeDesc == null) ? string.Empty : mapping.TypeDesc.BaseTypeDesc.Name);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
			codeTypeDeclaration.IsPartial = base.CodeProvider.Supports(GeneratorSupport.PartialTypes);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			base.CodeNamespace.Types.Add(codeTypeDeclaration);
			if (text != null && text.Length > 0)
			{
				codeTypeDeclaration.BaseTypes.Add(text);
			}
			else
			{
				base.AddPropertyChangedNotifier(codeTypeDeclaration);
			}
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			if (mapping.TypeDesc.IsAbstract)
			{
				codeTypeDeclaration.TypeAttributes |= TypeAttributes.Abstract;
			}
			CodeExporter.AddIncludeMetadata(codeTypeDeclaration.CustomAttributes, mapping, typeof(SoapIncludeAttribute));
			if (base.GenerateProperties)
			{
				for (int i = 0; i < mapping.Members.Length; i++)
				{
					this.ExportProperty(codeTypeDeclaration, mapping.Members[i], mapping.Scope);
				}
			}
			else
			{
				for (int j = 0; j < mapping.Members.Length; j++)
				{
					this.ExportMember(codeTypeDeclaration, mapping.Members[j]);
				}
			}
			for (int k = 0; k < mapping.Members.Length; k++)
			{
				Accessor[] elements = mapping.Members[k].Elements;
				this.EnsureTypesExported(elements, null);
			}
			if (mapping.BaseMapping != null)
			{
				this.ExportType(mapping.BaseMapping);
			}
			this.ExportDerivedStructs(mapping);
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0005A010 File Offset: 0x00058210
		internal override void ExportDerivedStructs(StructMapping mapping)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				this.ExportType(structMapping);
			}
		}

		/// <summary>Adds a <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> declaration to a method parameter or return value that corresponds to a part element of a SOAP message definition in a WSDL document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single part of a WSDL message definition.</param>
		/// <param name="forceUseMemberName">true to add an initial argument that contains the XML element name for the attribute declaration that is being generated; otherwise, false.</param>
		// Token: 0x060012AC RID: 4780 RVA: 0x0005A037 File Offset: 0x00058237
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, bool forceUseMemberName)
		{
			this.AddMemberMetadata(metadata, member.Mapping, forceUseMemberName);
		}

		/// <summary>Add a <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> declaration to a method parameter or return value corresponding to a part element of a SOAP message definition in a WSDL document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type, to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single part of a WSDL message definition.</param>
		// Token: 0x060012AD RID: 4781 RVA: 0x0005A047 File Offset: 0x00058247
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member)
		{
			this.AddMemberMetadata(metadata, member.Mapping, false);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0005A058 File Offset: 0x00058258
		private void AddElementMetadata(CodeAttributeDeclarationCollection metadata, string elementName, TypeDesc typeDesc, bool isNullable)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(SoapElementAttribute).FullName);
			if (elementName != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(elementName)));
			}
			if (typeDesc != null && typeDesc.IsAmbiguousDataType)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(typeDesc.DataType.Name)));
			}
			if (isNullable)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(true)));
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0005A0F4 File Offset: 0x000582F4
		private void AddMemberMetadata(CodeAttributeDeclarationCollection metadata, MemberMapping member, bool forceUseMemberName)
		{
			if (member.Elements.Length == 0)
			{
				return;
			}
			ElementAccessor elementAccessor = member.Elements[0];
			TypeMapping mapping = elementAccessor.Mapping;
			string text = Accessor.UnescapeName(elementAccessor.Name);
			bool flag = text == member.Name && !forceUseMemberName;
			if (!flag || mapping.TypeDesc.IsAmbiguousDataType || elementAccessor.IsNullable)
			{
				this.AddElementMetadata(metadata, flag ? null : text, mapping.TypeDesc.IsAmbiguousDataType ? mapping.TypeDesc : null, elementAccessor.IsNullable);
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0005A180 File Offset: 0x00058380
		private void ExportMember(CodeTypeDeclaration codeClass, MemberMapping member)
		{
			CodeMemberField codeMemberField = new CodeMemberField(member.GetTypeName(base.CodeProvider), member.Name);
			codeMemberField.Attributes = (codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			codeClass.Members.Add(codeMemberField);
			this.AddMemberMetadata(codeMemberField.CustomAttributes, member, false);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, member.Name + "Specified");
				codeMemberField.Attributes = (codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
				codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(SoapIgnoreAttribute).FullName);
				codeMemberField.CustomAttributes.Add(codeAttributeDeclaration);
				codeClass.Members.Add(codeMemberField);
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0005A288 File Offset: 0x00058488
		private void ExportProperty(CodeTypeDeclaration codeClass, MemberMapping member, CodeIdentifiers memberScope)
		{
			string text = memberScope.AddUnique(CodeExporter.MakeFieldName(member.Name), member);
			string typeName = member.GetTypeName(base.CodeProvider);
			CodeMemberField codeMemberField = new CodeMemberField(typeName, text);
			codeMemberField.Attributes = MemberAttributes.Private;
			codeClass.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name, typeName);
			codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			this.AddMemberMetadata(codeMemberProperty.CustomAttributes, member, false);
			codeClass.Members.Add(codeMemberProperty);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, text + "Specified");
				codeMemberField.Attributes = MemberAttributes.Private;
				codeClass.Members.Add(codeMemberField);
				codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name + "Specified", typeof(bool).FullName);
				codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(SoapIgnoreAttribute).FullName);
				codeMemberProperty.CustomAttributes.Add(codeAttributeDeclaration);
				codeClass.Members.Add(codeMemberProperty);
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0005A3D4 File Offset: 0x000585D4
		internal override void EnsureTypesExported(Accessor[] accessors, string ns)
		{
			if (accessors == null)
			{
				return;
			}
			for (int i = 0; i < accessors.Length; i++)
			{
				this.ExportType(accessors[i].Mapping);
			}
		}
	}
}
