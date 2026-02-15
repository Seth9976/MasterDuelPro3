using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	/// <summary>Generates types and attribute declarations from internal type mapping information for XML schema element declarations.</summary>
	// Token: 0x020001AB RID: 427
	public class XmlCodeExporter : CodeExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified namespace. </summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		// Token: 0x0600141D RID: 5149 RVA: 0x00059C3A File Offset: 0x00057E3A
		public XmlCodeExporter(CodeNamespace codeNamespace)
			: base(codeNamespace, null, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified namespace and code compile unit.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A CodeDOM graph container to which used assembly references are automatically added.</param>
		// Token: 0x0600141E RID: 5150 RVA: 0x00059C47 File Offset: 0x00057E47
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit)
			: base(codeNamespace, codeCompileUnit, null, CodeGenerationOptions.GenerateProperties, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified namespace, code compile unit, and code generation options.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">An enumeration value that provides options for generating .NET Framework types from XML schema custom data types.</param>
		// Token: 0x0600141F RID: 5151 RVA: 0x000614E4 File Offset: 0x0005F6E4
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options)
			: base(codeNamespace, codeCompileUnit, null, options, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified .NET Framework namespace, code compile unit containing the graph of the objects, an object representing code generation options, and a collection of mapping objects.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> program graph container to which used assembly references are automatically added.</param>
		/// <param name="options">An enumeration value that provides options for generating .NET Framework types from XML schema custom data types.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x06001420 RID: 5152 RVA: 0x00059C54 File Offset: 0x00057E54
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeGenerationOptions options, Hashtable mappings)
			: base(codeNamespace, codeCompileUnit, null, options, mappings)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> class using the specified .NET Framework namespace, code compile unit containing the graph of the objects, an enumeration specifying code options, and a collection of mapping objects.</summary>
		/// <param name="codeNamespace">The namespace of the types to generate.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" />  program graph container to which used assembly references are automatically added.</param>
		/// <param name="codeProvider">An enumeration value that provides options for generating .NET Framework types from XML schema custom data types.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> that contains special instructions for code creation.</param>
		/// <param name="mappings">A <see cref="T:System.Collections.Hashtable" /> that contains <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		// Token: 0x06001421 RID: 5153 RVA: 0x00059C62 File Offset: 0x00057E62
		public XmlCodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable mappings)
			: base(codeNamespace, codeCompileUnit, codeProvider, options, mappings)
		{
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for an XML schema element. </summary>
		/// <param name="xmlTypeMapping">The internal .NET Framework type mapping information for an XML schema element.</param>
		// Token: 0x06001422 RID: 5154 RVA: 0x000614F1 File Offset: 0x0005F6F1
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			xmlTypeMapping.CheckShallow();
			base.CheckScope(xmlTypeMapping.Scope);
			if (xmlTypeMapping.Accessor.Any)
			{
				throw new InvalidOperationException(Res.GetString("Cannot use wildcards at the top level of a schema."));
			}
			this.ExportElement(xmlTypeMapping.Accessor);
		}

		/// <summary>Generates a .NET Framework type, plus attribute declarations, for each of the parts that belong to a SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="xmlMembersMapping">The internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		// Token: 0x06001423 RID: 5155 RVA: 0x00061530 File Offset: 0x0005F730
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			xmlMembersMapping.CheckShallow();
			base.CheckScope(xmlMembersMapping.Scope);
			for (int i = 0; i < xmlMembersMapping.Count; i++)
			{
				AccessorMapping mapping = xmlMembersMapping[i].Mapping;
				if (mapping.Xmlns == null)
				{
					if (mapping.Attribute != null)
					{
						this.ExportType(mapping.Attribute.Mapping, Accessor.UnescapeName(mapping.Attribute.Name), mapping.Attribute.Namespace, null, false);
					}
					if (mapping.Elements != null)
					{
						for (int j = 0; j < mapping.Elements.Length; j++)
						{
							ElementAccessor elementAccessor = mapping.Elements[j];
							this.ExportType(elementAccessor.Mapping, Accessor.UnescapeName(elementAccessor.Name), elementAccessor.Namespace, null, false);
						}
					}
					if (mapping.Text != null)
					{
						this.ExportType(mapping.Text.Mapping, Accessor.UnescapeName(mapping.Text.Name), mapping.Text.Namespace, null, false);
					}
				}
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0006162D File Offset: 0x0005F82D
		private void ExportElement(ElementAccessor element)
		{
			this.ExportType(element.Mapping, Accessor.UnescapeName(element.Name), element.Namespace, element, true);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0006164E File Offset: 0x0005F84E
		private void ExportType(TypeMapping mapping, string ns)
		{
			this.ExportType(mapping, null, ns, null, true);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0006165C File Offset: 0x0005F85C
		private void ExportType(TypeMapping mapping, string name, string ns, ElementAccessor rootElement, bool checkReference)
		{
			if (mapping.IsReference && mapping.Namespace != "http://schemas.xmlsoap.org/soap/encoding/")
			{
				return;
			}
			if (mapping is StructMapping && checkReference && ((StructMapping)mapping).ReferencedByTopLevelElement && rootElement == null)
			{
				return;
			}
			if (mapping is ArrayMapping && rootElement != null && rootElement.IsTopLevelInSchema && ((ArrayMapping)mapping).TopLevelMapping != null)
			{
				mapping = ((ArrayMapping)mapping).TopLevelMapping;
			}
			CodeTypeDeclaration codeTypeDeclaration = null;
			if (base.ExportedMappings[mapping] == null)
			{
				base.ExportedMappings.Add(mapping, mapping);
				if (mapping.TypeDesc.IsMappedType)
				{
					codeTypeDeclaration = mapping.TypeDesc.ExtendedType.ExportTypeDefinition(base.CodeNamespace, base.CodeCompileUnit);
				}
				else if (mapping is EnumMapping)
				{
					codeTypeDeclaration = base.ExportEnum((EnumMapping)mapping, typeof(XmlEnumAttribute));
				}
				else if (mapping is StructMapping)
				{
					codeTypeDeclaration = this.ExportStruct((StructMapping)mapping);
				}
				else if (mapping is ArrayMapping)
				{
					Accessor[] elements = ((ArrayMapping)mapping).Elements;
					this.EnsureTypesExported(elements, ns);
				}
				if (codeTypeDeclaration != null)
				{
					if (!mapping.TypeDesc.IsMappedType)
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
						base.AddTypeMetadata(codeTypeDeclaration.CustomAttributes, typeof(XmlTypeAttribute), mapping.TypeDesc.Name, Accessor.UnescapeName(mapping.TypeName), mapping.Namespace, mapping.IncludeInSchema);
					}
					else if (CodeExporter.FindAttributeDeclaration(typeof(GeneratedCodeAttribute), codeTypeDeclaration.CustomAttributes) == null)
					{
						codeTypeDeclaration.CustomAttributes.Add(base.GeneratedCodeAttribute);
					}
					base.ExportedClasses.Add(mapping, codeTypeDeclaration);
				}
			}
			else
			{
				codeTypeDeclaration = (CodeTypeDeclaration)base.ExportedClasses[mapping];
			}
			if (codeTypeDeclaration != null && rootElement != null)
			{
				this.AddRootMetadata(codeTypeDeclaration.CustomAttributes, mapping, name, ns, rootElement);
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x000618B8 File Offset: 0x0005FAB8
		private void AddRootMetadata(CodeAttributeDeclarationCollection metadata, TypeMapping typeMapping, string name, string ns, ElementAccessor rootElement)
		{
			string fullName = typeof(XmlRootAttribute).FullName;
			using (IEnumerator enumerator = metadata.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((CodeAttributeDeclaration)enumerator.Current).Name == fullName)
					{
						return;
					}
				}
			}
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(fullName);
			if (typeMapping.TypeDesc.Name != name)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name)));
			}
			if (ns != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (typeMapping.TypeDesc != null && typeMapping.TypeDesc.IsAmbiguousDataType)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(typeMapping.TypeDesc.DataType.Name)));
			}
			if (rootElement.IsNullable != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(rootElement.IsNullable)));
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x000619F8 File Offset: 0x0005FBF8
		private CodeAttributeArgument[] GetDefaultValueArguments(PrimitiveMapping mapping, object value, out CodeExpression initExpression)
		{
			initExpression = null;
			if (value == null)
			{
				return null;
			}
			CodeExpression codeExpression = null;
			Type type = value.GetType();
			CodeAttributeArgument[] array = null;
			if (mapping is EnumMapping)
			{
				if (((EnumMapping)mapping).IsFlags)
				{
					string[] array2 = ((string)value).Split(null);
					for (int i = 0; i < array2.Length; i++)
					{
						if (array2[i].Length != 0)
						{
							CodeExpression codeExpression2 = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(mapping.TypeDesc.FullName), array2[i]);
							if (codeExpression != null)
							{
								codeExpression = new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.BitwiseOr, codeExpression2);
							}
							else
							{
								codeExpression = codeExpression2;
							}
						}
					}
				}
				else
				{
					codeExpression = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(mapping.TypeDesc.FullName), (string)value);
				}
				initExpression = codeExpression;
				array = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression)
				};
			}
			else if (type == typeof(bool) || type == typeof(int) || type == typeof(string) || type == typeof(double))
			{
				initExpression = (codeExpression = new CodePrimitiveExpression(value));
				array = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression)
				};
			}
			else if (type == typeof(short) || type == typeof(long) || type == typeof(float) || type == typeof(byte) || type == typeof(decimal))
			{
				codeExpression = new CodePrimitiveExpression(Convert.ToString(value, NumberFormatInfo.InvariantInfo));
				CodeExpression codeExpression3 = new CodeTypeOfExpression(type.FullName);
				array = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression3),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeCastExpression(type.FullName, new CodePrimitiveExpression(value));
			}
			else if (type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
			{
				value = CodeExporter.PromoteType(type, value);
				codeExpression = new CodePrimitiveExpression(Convert.ToString(value, NumberFormatInfo.InvariantInfo));
				CodeExpression codeExpression3 = new CodeTypeOfExpression(type.FullName);
				array = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression3),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeCastExpression(type.FullName, new CodePrimitiveExpression(value));
			}
			else if (type == typeof(DateTime))
			{
				DateTime dateTime = (DateTime)value;
				string text;
				long num;
				if (mapping.TypeDesc.FormatterName == "Date")
				{
					text = XmlCustomFormatter.FromDate(dateTime);
					num = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).Ticks;
				}
				else if (mapping.TypeDesc.FormatterName == "Time")
				{
					text = XmlCustomFormatter.FromDateTime(dateTime);
					num = dateTime.Ticks;
				}
				else
				{
					text = XmlCustomFormatter.FromDateTime(dateTime);
					num = dateTime.Ticks;
				}
				codeExpression = new CodePrimitiveExpression(text);
				CodeExpression codeExpression3 = new CodeTypeOfExpression(type.FullName);
				array = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression3),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(DateTime)), new CodeExpression[]
				{
					new CodePrimitiveExpression(num)
				});
			}
			else if (type == typeof(Guid))
			{
				codeExpression = new CodePrimitiveExpression(Convert.ToString(value, NumberFormatInfo.InvariantInfo));
				CodeExpression codeExpression3 = new CodeTypeOfExpression(type.FullName);
				array = new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(codeExpression3),
					new CodeAttributeArgument(codeExpression)
				};
				initExpression = new CodeObjectCreateExpression(new CodeTypeReference(typeof(Guid)), new CodeExpression[] { codeExpression });
			}
			if (mapping.TypeDesc.FullName != type.ToString() && !(mapping is EnumMapping))
			{
				initExpression = new CodeCastExpression(mapping.TypeDesc.FullName, initExpression);
			}
			return array;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00061E0C File Offset: 0x0006000C
		private object ImportDefault(TypeMapping mapping, string defaultValue)
		{
			if (defaultValue == null)
			{
				return null;
			}
			if (mapping.IsList)
			{
				string[] array = defaultValue.Trim().Split(null);
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null && array[i].Length > 0)
					{
						num++;
					}
				}
				object[] array2 = new object[num];
				num = 0;
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null && array[j].Length > 0)
					{
						array2[num++] = this.ImportDefaultValue(mapping, array[j]);
					}
				}
				return array2;
			}
			return this.ImportDefaultValue(mapping, defaultValue);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00061EA0 File Offset: 0x000600A0
		private object ImportDefaultValue(TypeMapping mapping, string defaultValue)
		{
			if (defaultValue == null)
			{
				return null;
			}
			if (!(mapping is PrimitiveMapping))
			{
				return DBNull.Value;
			}
			if (!(mapping is EnumMapping))
			{
				PrimitiveMapping primitiveMapping = (PrimitiveMapping)mapping;
				if (!primitiveMapping.TypeDesc.HasCustomFormatter)
				{
					if (primitiveMapping.TypeDesc.FormatterName == "String")
					{
						return defaultValue;
					}
					if (primitiveMapping.TypeDesc.FormatterName == "DateTime")
					{
						return XmlCustomFormatter.ToDateTime(defaultValue);
					}
					Type typeFromHandle = typeof(XmlConvert);
					MethodInfo method = typeFromHandle.GetMethod("To" + primitiveMapping.TypeDesc.FormatterName, new Type[] { typeof(string) });
					if (method != null)
					{
						return method.Invoke(typeFromHandle, new object[] { defaultValue });
					}
				}
				else if (primitiveMapping.TypeDesc.HasDefaultSupport)
				{
					return XmlCustomFormatter.ToDefaultValue(defaultValue, primitiveMapping.TypeDesc.FormatterName);
				}
				return DBNull.Value;
			}
			EnumMapping enumMapping = (EnumMapping)mapping;
			ConstantMapping[] constants = enumMapping.Constants;
			if (enumMapping.IsFlags)
			{
				Hashtable hashtable = new Hashtable();
				string[] array = new string[constants.Length];
				long[] array2 = new long[constants.Length];
				for (int i = 0; i < constants.Length; i++)
				{
					array2[i] = (enumMapping.IsFlags ? (1L << i) : ((long)i));
					array[i] = constants[i].Name;
					hashtable.Add(constants[i].Name, array2[i]);
				}
				return XmlCustomFormatter.FromEnum(XmlCustomFormatter.ToEnum(defaultValue, hashtable, enumMapping.TypeName, true), array, array2, enumMapping.TypeDesc.FullName);
			}
			for (int j = 0; j < constants.Length; j++)
			{
				if (constants[j].XmlName == defaultValue)
				{
					return constants[j].Name;
				}
			}
			throw new InvalidOperationException(Res.GetString("Value '{0}' cannot be converted to {1}.", new object[]
			{
				defaultValue,
				enumMapping.TypeDesc.FullName
			}));
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0006209C File Offset: 0x0006029C
		private void AddDefaultValueAttribute(CodeMemberField field, CodeAttributeDeclarationCollection metadata, object defaultValue, TypeMapping mapping, CodeCommentStatementCollection comments, TypeDesc memberTypeDesc, Accessor accessor, CodeConstructor ctor)
		{
			string text = (accessor.IsFixed ? "fixed" : "default");
			if (!memberTypeDesc.HasDefaultSupport)
			{
				if (comments != null && defaultValue is string)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
					CodeExporter.AddWarningComment(comments, Res.GetString("'{0}' attribute on items of type '{1}' is not supported in this version of the .Net Framework.  Ignoring {0}='{2}' attribute.", new object[]
					{
						text,
						mapping.TypeName,
						defaultValue.ToString()
					}));
				}
				return;
			}
			if (memberTypeDesc.IsArrayLike && accessor is ElementAccessor)
			{
				if (comments != null && defaultValue is string)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
					CodeExporter.AddWarningComment(comments, Res.GetString("'{0}' attribute on array-like elements is not supported in this version of the .Net Framework.  Ignoring {0}='{1}' attribute on element name='{2}'.", new object[]
					{
						text,
						defaultValue.ToString(),
						((ElementAccessor)accessor).Name
					}));
				}
				return;
			}
			if (mapping.TypeDesc.IsMappedType && field != null && defaultValue is string)
			{
				SchemaImporterExtension extension = mapping.TypeDesc.ExtendedType.Extension;
				CodeExpression codeExpression = extension.ImportDefaultValue((string)defaultValue, mapping.TypeDesc.FullName);
				if (codeExpression != null)
				{
					if (ctor != null)
					{
						XmlCodeExporter.AddInitializationStatement(ctor, field, codeExpression);
					}
					else
					{
						field.InitExpression = extension.ImportDefaultValue((string)defaultValue, mapping.TypeDesc.FullName);
					}
				}
				if (comments != null)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, mapping.TypeDesc.FullName);
					if (codeExpression == null)
					{
						CodeExporter.AddWarningComment(comments, Res.GetString("Schema importer extension {0} failed to parse '{1}'='{2}' attribute of type {3} from namespace='{4}'.", new object[]
						{
							extension.GetType().FullName,
							text,
							(string)defaultValue,
							mapping.TypeName,
							mapping.Namespace
						}));
					}
				}
				return;
			}
			object obj = null;
			if (defaultValue is string || defaultValue == null)
			{
				obj = this.ImportDefault(mapping, (string)defaultValue);
			}
			if (obj == null)
			{
				return;
			}
			if (!(mapping is PrimitiveMapping))
			{
				if (comments != null)
				{
					XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
					CodeExporter.AddWarningComment(comments, Res.GetString("'{0}' attribute supported only for primitive types.  Ignoring {0}='{1}' attribute.", new object[]
					{
						text,
						defaultValue.ToString()
					}));
				}
				return;
			}
			PrimitiveMapping primitiveMapping = (PrimitiveMapping)mapping;
			if (comments != null && !primitiveMapping.TypeDesc.HasDefaultSupport && primitiveMapping.TypeDesc.IsMappedType)
			{
				XmlCodeExporter.DropDefaultAttribute(accessor, comments, primitiveMapping.TypeDesc.FullName);
				return;
			}
			if (obj == DBNull.Value)
			{
				if (comments != null)
				{
					CodeExporter.AddWarningComment(comments, Res.GetString("'{0}' attribute on items of type '{1}' is not supported in this version of the .Net Framework.  Ignoring {0}='{2}' attribute.", new object[]
					{
						text,
						primitiveMapping.TypeName,
						defaultValue.ToString()
					}));
				}
				return;
			}
			CodeAttributeArgument[] array = null;
			CodeExpression codeExpression2 = null;
			if (primitiveMapping.IsList)
			{
				object[] array2 = (object[])obj;
				CodeExpression[] array3 = new CodeExpression[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					this.GetDefaultValueArguments(primitiveMapping, array2[i], out array3[i]);
				}
				codeExpression2 = new CodeArrayCreateExpression(field.Type, array3);
			}
			else
			{
				array = this.GetDefaultValueArguments(primitiveMapping, obj, out codeExpression2);
			}
			if (field != null)
			{
				if (ctor != null)
				{
					XmlCodeExporter.AddInitializationStatement(ctor, field, codeExpression2);
				}
				else
				{
					field.InitExpression = codeExpression2;
				}
			}
			if (array != null && primitiveMapping.TypeDesc.HasDefaultSupport && accessor.IsOptional && !accessor.IsFixed)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(DefaultValueAttribute).FullName, array);
				metadata.Add(codeAttributeDeclaration);
				return;
			}
			if (comments != null)
			{
				XmlCodeExporter.DropDefaultAttribute(accessor, comments, memberTypeDesc.FullName);
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0006240C File Offset: 0x0006060C
		private static void AddInitializationStatement(CodeConstructor ctor, CodeMemberField field, CodeExpression init)
		{
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeAssignStatement.Right = init;
			ctor.Statements.Add(codeAssignStatement);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00062449 File Offset: 0x00060649
		private static void DropDefaultAttribute(Accessor accessor, CodeCommentStatementCollection comments, string type)
		{
			if (!accessor.IsFixed && accessor.IsOptional)
			{
				CodeExporter.AddWarningComment(comments, Res.GetString("DefaultValue attribute on members of type {0} is not supported in this version of the .Net Framework.", new object[] { type }));
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00062478 File Offset: 0x00060678
		private CodeTypeDeclaration ExportStruct(StructMapping mapping)
		{
			if (mapping.TypeDesc.IsRoot)
			{
				base.ExportRoot(mapping, typeof(XmlIncludeAttribute));
				return null;
			}
			string name = mapping.TypeDesc.Name;
			string text = ((mapping.TypeDesc.BaseTypeDesc == null || mapping.TypeDesc.BaseTypeDesc.IsRoot) ? string.Empty : mapping.TypeDesc.BaseTypeDesc.FullName);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
			codeTypeDeclaration.IsPartial = base.CodeProvider.Supports(GeneratorSupport.PartialTypes);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			base.CodeNamespace.Types.Add(codeTypeDeclaration);
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (codeConstructor.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			codeTypeDeclaration.Members.Add(codeConstructor);
			if (mapping.TypeDesc.IsAbstract)
			{
				codeConstructor.Attributes |= MemberAttributes.Abstract;
			}
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
			CodeExporter.AddIncludeMetadata(codeTypeDeclaration.CustomAttributes, mapping, typeof(XmlIncludeAttribute));
			if (mapping.IsSequence)
			{
				int num = 0;
				for (int i = 0; i < mapping.Members.Length; i++)
				{
					MemberMapping memberMapping = mapping.Members[i];
					if (memberMapping.IsParticle && memberMapping.SequenceId < 0)
					{
						memberMapping.SequenceId = num++;
					}
				}
			}
			if (base.GenerateProperties)
			{
				for (int j = 0; j < mapping.Members.Length; j++)
				{
					this.ExportProperty(codeTypeDeclaration, mapping.Members[j], mapping.Namespace, mapping.Scope, codeConstructor);
				}
			}
			else
			{
				for (int k = 0; k < mapping.Members.Length; k++)
				{
					this.ExportMember(codeTypeDeclaration, mapping.Members[k], mapping.Namespace, codeConstructor);
				}
			}
			for (int l = 0; l < mapping.Members.Length; l++)
			{
				if (mapping.Members[l].Xmlns == null)
				{
					Accessor[] elements = mapping.Members[l].Elements;
					this.EnsureTypesExported(elements, mapping.Namespace);
					this.EnsureTypesExported(mapping.Members[l].Attribute, mapping.Namespace);
					this.EnsureTypesExported(mapping.Members[l].Text, mapping.Namespace);
				}
			}
			if (mapping.BaseMapping != null)
			{
				this.ExportType(mapping.BaseMapping, null, mapping.Namespace, null, false);
			}
			this.ExportDerivedStructs(mapping);
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			if (codeConstructor.Statements.Count == 0)
			{
				codeTypeDeclaration.Members.Remove(codeConstructor);
			}
			return codeTypeDeclaration;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00062750 File Offset: 0x00060950
		internal override void ExportDerivedStructs(StructMapping mapping)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				this.ExportType(structMapping, mapping.Namespace);
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> declaration to a method return value that corresponds to a &lt;part&gt; element of a non-SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="mapping">The internal .NET Framework type mapping information for an XML schema element.</param>
		/// <param name="ns">The XML namespace of the SOAP message part for which the type mapping information in the member parameter has been generated.</param>
		// Token: 0x06001430 RID: 5168 RVA: 0x00062780 File Offset: 0x00060980
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlTypeMapping mapping, string ns)
		{
			mapping.CheckShallow();
			base.CheckScope(mapping.Scope);
			if (mapping.Mapping is StructMapping || mapping.Mapping is EnumMapping)
			{
				return;
			}
			this.AddRootMetadata(metadata, mapping.Mapping, Accessor.UnescapeName(mapping.Accessor.Name), mapping.Accessor.Namespace, mapping.Accessor);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> declaration to a method parameter or return value that corresponds to a &lt;part&gt; element of a SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single element part of a WSDL message definition.</param>
		/// <param name="ns">The XML namespace of the SOAP message part for which the type mapping information in the member parameter has been generated.</param>
		/// <param name="forceUseMemberName">Flag that helps determine whether to add an initial argument containing the XML element name for the attribute declaration being generated.</param>
		// Token: 0x06001431 RID: 5169 RVA: 0x000627E8 File Offset: 0x000609E8
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, string ns, bool forceUseMemberName)
		{
			this.AddMemberMetadata(null, metadata, member.Mapping, ns, forceUseMemberName, null, null);
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> declaration to a method parameter or return value that corresponds to a &lt;part&gt; element of a SOAP message definition in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="metadata">The collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects for the generated type to which the method adds an attribute declaration.</param>
		/// <param name="member">An internal .NET Framework type mapping for a single element part of a WSDL message definition.</param>
		/// <param name="ns">The XML namespace of the SOAP message part for which the type mapping information in the member parameter has been generated.</param>
		// Token: 0x06001432 RID: 5170 RVA: 0x000627FD File Offset: 0x000609FD
		public void AddMappingMetadata(CodeAttributeDeclarationCollection metadata, XmlMemberMapping member, string ns)
		{
			this.AddMemberMetadata(null, metadata, member.Mapping, ns, false, null, null);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00062814 File Offset: 0x00060A14
		private void ExportArrayElements(CodeAttributeDeclarationCollection metadata, ArrayMapping array, string ns, TypeDesc elementTypeDesc, int nestingLevel)
		{
			for (int i = 0; i < array.Elements.Length; i++)
			{
				ElementAccessor elementAccessor = array.Elements[i];
				TypeMapping mapping = elementAccessor.Mapping;
				string text = Accessor.UnescapeName(elementAccessor.Name);
				bool flag = !elementAccessor.Mapping.TypeDesc.IsArray && text == elementAccessor.Mapping.TypeName;
				bool flag2 = mapping.TypeDesc == elementTypeDesc;
				bool flag3 = elementAccessor.Form == XmlSchemaForm.Unqualified || elementAccessor.Namespace == ns;
				bool flag4 = elementAccessor.IsNullable == mapping.TypeDesc.IsNullable;
				bool flag5 = elementAccessor.Form != XmlSchemaForm.Unqualified;
				if (!flag || !flag2 || !flag3 || !flag4 || !flag5 || nestingLevel > 0)
				{
					this.ExportArrayItem(metadata, flag ? null : text, flag3 ? null : elementAccessor.Namespace, flag2 ? null : mapping.TypeDesc, mapping.TypeDesc, elementAccessor.IsNullable, flag5 ? XmlSchemaForm.None : elementAccessor.Form, nestingLevel);
				}
				if (mapping is ArrayMapping)
				{
					this.ExportArrayElements(metadata, (ArrayMapping)mapping, ns, elementTypeDesc.ArrayElementTypeDesc, nestingLevel + 1);
				}
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00062948 File Offset: 0x00060B48
		private void AddMemberMetadata(CodeMemberField field, CodeAttributeDeclarationCollection metadata, MemberMapping member, string ns, bool forceUseMemberName, CodeCommentStatementCollection comments, CodeConstructor ctor)
		{
			if (member.Xmlns != null)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlNamespaceDeclarationsAttribute).FullName);
				metadata.Add(codeAttributeDeclaration);
				return;
			}
			if (member.Attribute == null)
			{
				if (member.Text != null)
				{
					TypeMapping mapping = member.Text.Mapping;
					this.ExportText(metadata, (mapping.TypeDesc == member.TypeDesc || (member.TypeDesc.IsArrayLike && mapping.TypeDesc == member.TypeDesc.ArrayElementTypeDesc)) ? null : mapping.TypeDesc, mapping.TypeDesc.IsAmbiguousDataType ? mapping.TypeDesc.DataType.Name : null);
				}
				if (member.Elements.Length == 1)
				{
					ElementAccessor elementAccessor = member.Elements[0];
					TypeMapping mapping2 = elementAccessor.Mapping;
					string text = Accessor.UnescapeName(elementAccessor.Name);
					bool flag = text == member.Name && !forceUseMemberName;
					bool flag2 = mapping2 is ArrayMapping;
					bool flag3 = elementAccessor.Namespace == ns;
					bool flag4 = elementAccessor.Form != XmlSchemaForm.Unqualified;
					if (elementAccessor.Any)
					{
						this.ExportAnyElement(metadata, text, elementAccessor.Namespace, member.SequenceId);
					}
					else if (flag2)
					{
						TypeDesc typeDesc = mapping2.TypeDesc;
						TypeDesc typeDesc2 = member.TypeDesc;
						ArrayMapping arrayMapping = (ArrayMapping)mapping2;
						if (!flag || !flag3 || elementAccessor.IsNullable || !flag4 || member.SequenceId != -1)
						{
							this.ExportArray(metadata, flag ? null : text, flag3 ? null : elementAccessor.Namespace, elementAccessor.IsNullable, flag4 ? XmlSchemaForm.None : elementAccessor.Form, member.SequenceId);
						}
						else if (mapping2.TypeDesc.ArrayElementTypeDesc == new TypeScope().GetTypeDesc(typeof(byte)))
						{
							this.ExportArray(metadata, null, null, false, XmlSchemaForm.None, member.SequenceId);
						}
						this.ExportArrayElements(metadata, arrayMapping, elementAccessor.Namespace, member.TypeDesc.ArrayElementTypeDesc, 0);
					}
					else
					{
						bool flag5 = mapping2.TypeDesc == member.TypeDesc || (member.TypeDesc.IsArrayLike && mapping2.TypeDesc == member.TypeDesc.ArrayElementTypeDesc);
						if (member.TypeDesc.IsArrayLike)
						{
							flag = false;
						}
						this.ExportElement(metadata, flag ? null : text, flag3 ? null : elementAccessor.Namespace, flag5 ? null : mapping2.TypeDesc, mapping2.TypeDesc, elementAccessor.IsNullable, flag4 ? XmlSchemaForm.None : elementAccessor.Form, member.SequenceId);
					}
					this.AddDefaultValueAttribute(field, metadata, elementAccessor.Default, mapping2, comments, member.TypeDesc, elementAccessor, ctor);
				}
				else
				{
					for (int i = 0; i < member.Elements.Length; i++)
					{
						ElementAccessor elementAccessor2 = member.Elements[i];
						string text2 = Accessor.UnescapeName(elementAccessor2.Name);
						bool flag6 = elementAccessor2.Namespace == ns;
						if (elementAccessor2.Any)
						{
							this.ExportAnyElement(metadata, text2, elementAccessor2.Namespace, member.SequenceId);
						}
						else
						{
							bool flag7 = elementAccessor2.Form != XmlSchemaForm.Unqualified;
							this.ExportElement(metadata, text2, flag6 ? null : elementAccessor2.Namespace, elementAccessor2.Mapping.TypeDesc, elementAccessor2.Mapping.TypeDesc, elementAccessor2.IsNullable, flag7 ? XmlSchemaForm.None : elementAccessor2.Form, member.SequenceId);
						}
					}
				}
				if (member.ChoiceIdentifier != null)
				{
					metadata.Add(new CodeAttributeDeclaration(typeof(XmlChoiceIdentifierAttribute).FullName)
					{
						Arguments = 
						{
							new CodeAttributeArgument(new CodePrimitiveExpression(member.ChoiceIdentifier.MemberName))
						}
					});
				}
				if (member.Ignore)
				{
					CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
					metadata.Add(codeAttributeDeclaration2);
				}
				return;
			}
			AttributeAccessor attribute = member.Attribute;
			if (attribute.Any)
			{
				this.ExportAnyAttribute(metadata);
				return;
			}
			TypeMapping mapping3 = attribute.Mapping;
			string text3 = Accessor.UnescapeName(attribute.Name);
			bool flag8 = mapping3.TypeDesc == member.TypeDesc || (member.TypeDesc.IsArrayLike && mapping3.TypeDesc == member.TypeDesc.ArrayElementTypeDesc);
			bool flag9 = text3 == member.Name && !forceUseMemberName;
			bool flag10 = attribute.Namespace == ns;
			bool flag11 = attribute.Form != XmlSchemaForm.Qualified;
			this.ExportAttribute(metadata, flag9 ? null : text3, (flag10 || flag11) ? null : attribute.Namespace, flag8 ? null : mapping3.TypeDesc, mapping3.TypeDesc, flag11 ? XmlSchemaForm.None : attribute.Form);
			this.AddDefaultValueAttribute(field, metadata, attribute.Default, mapping3, comments, member.TypeDesc, attribute, ctor);
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00062E44 File Offset: 0x00061044
		private void ExportMember(CodeTypeDeclaration codeClass, MemberMapping member, string ns, CodeConstructor ctor)
		{
			CodeMemberField codeMemberField = new CodeMemberField(member.GetTypeName(base.CodeProvider), member.Name);
			codeMemberField.Attributes = (codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			codeClass.Members.Add(codeMemberField);
			this.AddMemberMetadata(codeMemberField, codeMemberField.CustomAttributes, member, ns, false, codeMemberField.Comments, ctor);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, member.Name + "Specified");
				codeMemberField.Attributes = (codeMemberField.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
				codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
				codeMemberField.CustomAttributes.Add(codeAttributeDeclaration);
				codeClass.Members.Add(codeMemberField);
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00062F58 File Offset: 0x00061158
		private void ExportProperty(CodeTypeDeclaration codeClass, MemberMapping member, string ns, CodeIdentifiers memberScope, CodeConstructor ctor)
		{
			string text = memberScope.AddUnique(CodeExporter.MakeFieldName(member.Name), member);
			string typeName = member.GetTypeName(base.CodeProvider);
			CodeMemberField codeMemberField = new CodeMemberField(typeName, text);
			codeMemberField.Attributes = MemberAttributes.Private;
			codeClass.Members.Add(codeMemberField);
			CodeMemberProperty codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name, typeName);
			codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			this.AddMemberMetadata(codeMemberField, codeMemberProperty.CustomAttributes, member, ns, false, codeMemberProperty.Comments, ctor);
			codeClass.Members.Add(codeMemberProperty);
			if (member.CheckSpecified != SpecifiedAccessor.None)
			{
				codeMemberField = new CodeMemberField(typeof(bool).FullName, text + "Specified");
				codeMemberField.Attributes = MemberAttributes.Private;
				codeClass.Members.Add(codeMemberField);
				codeMemberProperty = base.CreatePropertyDeclaration(codeMemberField, member.Name + "Specified", typeof(bool).FullName);
				codeMemberProperty.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
				codeMemberProperty.CustomAttributes.Add(codeAttributeDeclaration);
				codeClass.Members.Add(codeMemberProperty);
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x000630B0 File Offset: 0x000612B0
		private void ExportText(CodeAttributeDeclarationCollection metadata, TypeDesc typeDesc, string dataType)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlTextAttribute).FullName);
			if (typeDesc != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(typeDesc.FullName)));
			}
			if (dataType != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(dataType)));
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00063118 File Offset: 0x00061318
		private void ExportAttribute(CodeAttributeDeclarationCollection metadata, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, XmlSchemaForm form)
		{
			this.ExportMetadata(metadata, typeof(XmlAttributeAttribute), name, ns, typeDesc, dataTypeDesc, null, form, 0, -1);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00063144 File Offset: 0x00061344
		private void ExportArrayItem(CodeAttributeDeclarationCollection metadata, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, bool isNullable, XmlSchemaForm form, int nestingLevel)
		{
			this.ExportMetadata(metadata, typeof(XmlArrayItemAttribute), name, ns, typeDesc, dataTypeDesc, isNullable ? null : false, form, nestingLevel, -1);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0006317C File Offset: 0x0006137C
		private void ExportElement(CodeAttributeDeclarationCollection metadata, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, bool isNullable, XmlSchemaForm form, int sequenceId)
		{
			this.ExportMetadata(metadata, typeof(XmlElementAttribute), name, ns, typeDesc, dataTypeDesc, isNullable ? true : null, form, 0, sequenceId);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000631B4 File Offset: 0x000613B4
		private void ExportArray(CodeAttributeDeclarationCollection metadata, string name, string ns, bool isNullable, XmlSchemaForm form, int sequenceId)
		{
			this.ExportMetadata(metadata, typeof(XmlArrayAttribute), name, ns, null, null, isNullable ? true : null, form, 0, sequenceId);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x000631E8 File Offset: 0x000613E8
		private void ExportMetadata(CodeAttributeDeclarationCollection metadata, Type attributeType, string name, string ns, TypeDesc typeDesc, TypeDesc dataTypeDesc, object isNullable, XmlSchemaForm form, int nestingLevel, int sequenceId)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(attributeType.FullName);
			if (name != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name)));
			}
			if (typeDesc != null)
			{
				if (isNullable != null && (bool)isNullable && typeDesc.IsValueType && !typeDesc.IsMappedType && base.CodeProvider.Supports(GeneratorSupport.GenericTypeReference))
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression("System.Nullable`1[" + typeDesc.FullName + "]")));
					isNullable = null;
				}
				else
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(typeDesc.FullName)));
				}
			}
			if (form != XmlSchemaForm.None)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Form", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(XmlSchemaForm).FullName), Enum.Format(typeof(XmlSchemaForm), form, "G"))));
				if (form == XmlSchemaForm.Unqualified && ns != null && ns.Length == 0)
				{
					ns = null;
				}
			}
			if (ns != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (dataTypeDesc != null && dataTypeDesc.IsAmbiguousDataType && !dataTypeDesc.IsMappedType)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("DataType", new CodePrimitiveExpression(dataTypeDesc.DataType.Name)));
			}
			if (isNullable != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression((bool)isNullable)));
			}
			if (nestingLevel > 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("NestingLevel", new CodePrimitiveExpression(nestingLevel)));
			}
			if (sequenceId >= 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Order", new CodePrimitiveExpression(sequenceId)));
			}
			if (codeAttributeDeclaration.Arguments.Count == 0 && attributeType == typeof(XmlElementAttribute))
			{
				return;
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00063400 File Offset: 0x00061600
		private void ExportAnyElement(CodeAttributeDeclarationCollection metadata, string name, string ns, int sequenceId)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlAnyElementAttribute).FullName);
			if (name != null && name.Length > 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(name)));
			}
			if (ns != null)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (sequenceId >= 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Order", new CodePrimitiveExpression(sequenceId)));
			}
			metadata.Add(codeAttributeDeclaration);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00063498 File Offset: 0x00061698
		private void ExportAnyAttribute(CodeAttributeDeclarationCollection metadata)
		{
			metadata.Add(new CodeAttributeDeclaration(typeof(XmlAnyAttributeAttribute).FullName));
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x000634B8 File Offset: 0x000616B8
		internal override void EnsureTypesExported(Accessor[] accessors, string ns)
		{
			if (accessors == null)
			{
				return;
			}
			for (int i = 0; i < accessors.Length; i++)
			{
				this.EnsureTypesExported(accessors[i], ns);
			}
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000634E1 File Offset: 0x000616E1
		private void EnsureTypesExported(Accessor accessor, string ns)
		{
			if (accessor == null)
			{
				return;
			}
			this.ExportType(accessor.Mapping, null, ns, null, false);
		}
	}
}
