using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using Microsoft.CSharp;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Represents a class that can generate proxy code from an XML representation of a data structure.</summary>
	// Token: 0x02000143 RID: 323
	public abstract class CodeExporter
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0004DF88 File Offset: 0x0004C188
		internal CodeExporter(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit, CodeDomProvider codeProvider, CodeGenerationOptions options, Hashtable exportedMappings)
		{
			this.includeMetadata = new CodeAttributeDeclarationCollection();
			base..ctor();
			if (codeNamespace != null)
			{
				CodeGenerator.ValidateIdentifiers(codeNamespace);
			}
			this.codeNamespace = codeNamespace;
			if (codeCompileUnit != null)
			{
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.dll");
				}
				if (!codeCompileUnit.ReferencedAssemblies.Contains("System.Xml.dll"))
				{
					codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
				}
			}
			this.codeCompileUnit = codeCompileUnit;
			this.options = options;
			this.exportedMappings = exportedMappings;
			this.codeProvider = codeProvider;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0004E01D File Offset: 0x0004C21D
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this.codeCompileUnit;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0004E025 File Offset: 0x0004C225
		internal CodeNamespace CodeNamespace
		{
			get
			{
				if (this.codeNamespace == null)
				{
					this.codeNamespace = new CodeNamespace();
				}
				return this.codeNamespace;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0004E040 File Offset: 0x0004C240
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

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0004E05B File Offset: 0x0004C25B
		internal Hashtable ExportedClasses
		{
			get
			{
				if (this.exportedClasses == null)
				{
					this.exportedClasses = new Hashtable();
				}
				return this.exportedClasses;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0004E076 File Offset: 0x0004C276
		internal Hashtable ExportedMappings
		{
			get
			{
				if (this.exportedMappings == null)
				{
					this.exportedMappings = new Hashtable();
				}
				return this.exportedMappings;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0004E091 File Offset: 0x0004C291
		internal bool GenerateProperties
		{
			get
			{
				return (this.options & CodeGenerationOptions.GenerateProperties) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0004E0A0 File Offset: 0x0004C2A0
		internal CodeAttributeDeclaration GeneratedCodeAttribute
		{
			get
			{
				if (this.generatedCodeAttribute == null)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(GeneratedCodeAttribute).FullName);
					Assembly assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						assembly = Assembly.GetExecutingAssembly();
						if (assembly == null)
						{
							assembly = typeof(CodeExporter).Assembly;
						}
					}
					AssemblyName name = assembly.GetName();
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
					string productVersion = CodeExporter.GetProductVersion(assembly);
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression((productVersion == null) ? name.Version.ToString() : productVersion)));
					this.generatedCodeAttribute = codeAttributeDeclaration;
				}
				return this.generatedCodeAttribute;
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0004E15C File Offset: 0x0004C35C
		internal static CodeAttributeDeclaration FindAttributeDeclaration(Type type, CodeAttributeDeclarationCollection metadata)
		{
			foreach (object obj in metadata)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (codeAttributeDeclaration.Name == type.FullName || codeAttributeDeclaration.Name == type.Name)
				{
					return codeAttributeDeclaration;
				}
			}
			return null;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0004E1D8 File Offset: 0x0004C3D8
		private static string GetProductVersion(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (customAttributes[i] is AssemblyInformationalVersionAttribute)
				{
					return ((AssemblyInformationalVersionAttribute)customAttributes[i]).InformationalVersion;
				}
			}
			return null;
		}

		/// <summary>Gets a collection of code attribute metadata that is included when the code is exported.</summary>
		/// <returns>A collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects that represent metadata that is included when the code is exported.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0004E214 File Offset: 0x0004C414
		public CodeAttributeDeclarationCollection IncludeMetadata
		{
			get
			{
				return this.includeMetadata;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0004E21C File Offset: 0x0004C41C
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0004E224 File Offset: 0x0004C424
		internal void CheckScope(TypeScope scope)
		{
			if (this.scope == null)
			{
				this.scope = scope;
				return;
			}
			if (this.scope != scope)
			{
				throw new InvalidOperationException(Res.GetString("Exported mappings must come from the same importer."));
			}
		}

		// Token: 0x06000FDF RID: 4063
		internal abstract void ExportDerivedStructs(StructMapping mapping);

		// Token: 0x06000FE0 RID: 4064
		internal abstract void EnsureTypesExported(Accessor[] accessors, string ns);

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004E24F File Offset: 0x0004C44F
		internal static void AddWarningComment(CodeCommentStatementCollection comments, string text)
		{
			comments.Add(new CodeCommentStatement(Res.GetString("CODEGEN Warning: {0}", new object[] { text }), false));
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0004E274 File Offset: 0x0004C474
		internal void ExportRoot(StructMapping mapping, Type includeType)
		{
			if (!this.rootExported)
			{
				this.rootExported = true;
				this.ExportDerivedStructs(mapping);
				for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
				{
					if (!structMapping.ReferencedByElement && structMapping.IncludeInSchema && !structMapping.IsAnonymousType)
					{
						CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(includeType.FullName);
						codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName)));
						this.includeMetadata.Add(codeAttributeDeclaration);
					}
				}
				Hashtable hashtable = new Hashtable();
				foreach (object obj in this.Scope.TypeMappings)
				{
					TypeMapping typeMapping = (TypeMapping)obj;
					if (typeMapping is ArrayMapping)
					{
						ArrayMapping arrayMapping = (ArrayMapping)typeMapping;
						if (CodeExporter.ShouldInclude(arrayMapping) && !hashtable.Contains(arrayMapping.TypeDesc.FullName))
						{
							CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(includeType.FullName);
							codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(arrayMapping.TypeDesc.FullName)));
							this.includeMetadata.Add(codeAttributeDeclaration2);
							hashtable.Add(arrayMapping.TypeDesc.FullName, string.Empty);
							Accessor[] elements = arrayMapping.Elements;
							this.EnsureTypesExported(elements, arrayMapping.Namespace);
						}
					}
				}
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0004E3FC File Offset: 0x0004C5FC
		private static bool ShouldInclude(ArrayMapping arrayMapping)
		{
			if (arrayMapping.ReferencedByElement)
			{
				return false;
			}
			if (arrayMapping.Next != null)
			{
				return false;
			}
			if (arrayMapping.Elements.Length == 1 && arrayMapping.Elements[0].Mapping.TypeDesc.Kind == TypeKind.Node)
			{
				return false;
			}
			for (int i = 0; i < arrayMapping.Elements.Length; i++)
			{
				if (arrayMapping.Elements[i].Name != arrayMapping.Elements[i].Mapping.DefaultElementName)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004E484 File Offset: 0x0004C684
		internal CodeTypeDeclaration ExportEnum(EnumMapping mapping, Type type)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(mapping.TypeDesc.Name);
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			codeTypeDeclaration.IsEnum = true;
			if (mapping.IsFlags && mapping.Constants.Length > 31)
			{
				codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(typeof(long)));
			}
			codeTypeDeclaration.TypeAttributes |= TypeAttributes.Public;
			this.CodeNamespace.Types.Add(codeTypeDeclaration);
			for (int i = 0; i < mapping.Constants.Length; i++)
			{
				CodeExporter.ExportConstant(codeTypeDeclaration, mapping.Constants[i], type, mapping.IsFlags, 1L << i);
			}
			if (mapping.IsFlags)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(FlagsAttribute).FullName);
				codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			}
			CodeGenerator.ValidateIdentifiers(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0004E574 File Offset: 0x0004C774
		internal void AddTypeMetadata(CodeAttributeDeclarationCollection metadata, Type type, string defaultName, string name, string ns, bool includeInSchema)
		{
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
			if (name == null || name.Length == 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("AnonymousType", new CodePrimitiveExpression(true)));
			}
			else if (defaultName != name)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("TypeName", new CodePrimitiveExpression(name)));
			}
			if (ns != null && ns.Length != 0)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(ns)));
			}
			if (!includeInSchema)
			{
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IncludeInSchema", new CodePrimitiveExpression(false)));
			}
			if (codeAttributeDeclaration.Arguments.Count > 0)
			{
				metadata.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0004E64C File Offset: 0x0004C84C
		internal static void AddIncludeMetadata(CodeAttributeDeclarationCollection metadata, StructMapping mapping, Type type)
		{
			if (mapping.IsAnonymousType)
			{
				return;
			}
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				metadata.Add(new CodeAttributeDeclaration(type.FullName)
				{
					Arguments = 
					{
						new CodeAttributeArgument(new CodeTypeOfExpression(structMapping.TypeDesc.FullName))
					}
				});
				CodeExporter.AddIncludeMetadata(metadata, structMapping, type);
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004E6B4 File Offset: 0x0004C8B4
		internal static void ExportConstant(CodeTypeDeclaration codeClass, ConstantMapping constant, Type type, bool init, long enumValue)
		{
			CodeMemberField codeMemberField = new CodeMemberField(typeof(int).FullName, constant.Name);
			codeMemberField.Comments.Add(new CodeCommentStatement(Res.GetString("<remarks/>"), true));
			if (init)
			{
				codeMemberField.InitExpression = new CodePrimitiveExpression(enumValue);
			}
			codeClass.Members.Add(codeMemberField);
			if (constant.XmlName != constant.Name)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(type.FullName);
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(constant.XmlName)));
				codeMemberField.CustomAttributes.Add(codeAttributeDeclaration);
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0004E764 File Offset: 0x0004C964
		internal static object PromoteType(Type type, object value)
		{
			if (type == typeof(sbyte))
			{
				return ((IConvertible)value).ToInt16(null);
			}
			if (type == typeof(ushort))
			{
				return ((IConvertible)value).ToInt32(null);
			}
			if (type == typeof(uint))
			{
				return ((IConvertible)value).ToInt64(null);
			}
			if (type == typeof(ulong))
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return value;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004E804 File Offset: 0x0004CA04
		internal CodeMemberProperty CreatePropertyDeclaration(CodeMemberField field, string name, string typeName)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Type = new CodeTypeReference(typeName);
			codeMemberProperty.Name = name;
			codeMemberProperty.Attributes = (codeMemberProperty.Attributes & (MemberAttributes)(-61441)) | MemberAttributes.Public;
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement();
			codeMethodReturnStatement.Expression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			codeMemberProperty.GetStatements.Add(codeMethodReturnStatement);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			CodeExpression codeExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), field.Name);
			CodeExpression codeExpression2 = new CodePropertySetValueReferenceExpression();
			codeAssignStatement.Left = codeExpression;
			codeAssignStatement.Right = codeExpression2;
			if (this.EnableDataBinding)
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
				codeMemberProperty.SetStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), CodeExporter.RaisePropertyChangedEventMethod.Name, new CodeExpression[]
				{
					new CodePrimitiveExpression(name)
				}));
			}
			else
			{
				codeMemberProperty.SetStatements.Add(codeAssignStatement);
			}
			return codeMemberProperty;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0004E8EC File Offset: 0x0004CAEC
		internal static string MakeFieldName(string name)
		{
			return CodeIdentifier.MakeCamel(name) + "Field";
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0004E900 File Offset: 0x0004CB00
		internal void AddPropertyChangedNotifier(CodeTypeDeclaration codeClass)
		{
			if (this.EnableDataBinding && codeClass != null)
			{
				if (codeClass.BaseTypes.Count == 0)
				{
					codeClass.BaseTypes.Add(typeof(object));
				}
				codeClass.BaseTypes.Add(new CodeTypeReference(typeof(INotifyPropertyChanged)));
				codeClass.Members.Add(CodeExporter.PropertyChangedEvent);
				codeClass.Members.Add(CodeExporter.RaisePropertyChangedEventMethod);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0004E977 File Offset: 0x0004CB77
		private bool EnableDataBinding
		{
			get
			{
				return (this.options & CodeGenerationOptions.EnableDataBinding) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0004E988 File Offset: 0x0004CB88
		internal static CodeMemberMethod RaisePropertyChangedEventMethod
		{
			get
			{
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = "RaisePropertyChanged";
				codeMemberMethod.Attributes = (MemberAttributes)12290;
				CodeArgumentReferenceExpression codeArgumentReferenceExpression = new CodeArgumentReferenceExpression("propertyName");
				codeMemberMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), codeArgumentReferenceExpression.ParameterName));
				CodeVariableReferenceExpression codeVariableReferenceExpression = new CodeVariableReferenceExpression("propertyChanged");
				codeMemberMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(PropertyChangedEventHandler), codeVariableReferenceExpression.VariableName, new CodeEventReferenceExpression(new CodeThisReferenceExpression(), CodeExporter.PropertyChangedEvent.Name)));
				CodeConditionStatement codeConditionStatement = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeVariableReferenceExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), Array.Empty<CodeStatement>());
				codeMemberMethod.Statements.Add(codeConditionStatement);
				codeConditionStatement.TrueStatements.Add(new CodeDelegateInvokeExpression(codeVariableReferenceExpression, new CodeExpression[]
				{
					new CodeThisReferenceExpression(),
					new CodeObjectCreateExpression(typeof(PropertyChangedEventArgs), new CodeExpression[] { codeArgumentReferenceExpression })
				}));
				return codeMemberMethod;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0004EA80 File Offset: 0x0004CC80
		internal static CodeMemberEvent PropertyChangedEvent
		{
			get
			{
				return new CodeMemberEvent
				{
					Attributes = MemberAttributes.Public,
					Name = "PropertyChanged",
					Type = new CodeTypeReference(typeof(PropertyChangedEventHandler)),
					ImplementationTypes = { typeof(INotifyPropertyChanged) }
				};
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal CodeExporter()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007B6 RID: 1974
		private Hashtable exportedMappings;

		// Token: 0x040007B7 RID: 1975
		private Hashtable exportedClasses;

		// Token: 0x040007B8 RID: 1976
		private CodeNamespace codeNamespace;

		// Token: 0x040007B9 RID: 1977
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x040007BA RID: 1978
		private bool rootExported;

		// Token: 0x040007BB RID: 1979
		private TypeScope scope;

		// Token: 0x040007BC RID: 1980
		private CodeAttributeDeclarationCollection includeMetadata;

		// Token: 0x040007BD RID: 1981
		private CodeGenerationOptions options;

		// Token: 0x040007BE RID: 1982
		private CodeDomProvider codeProvider;

		// Token: 0x040007BF RID: 1983
		private CodeAttributeDeclaration generatedCodeAttribute;
	}
}
