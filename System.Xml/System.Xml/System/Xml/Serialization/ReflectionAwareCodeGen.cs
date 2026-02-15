using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x020001DA RID: 474
	internal class ReflectionAwareCodeGen
	{
		// Token: 0x06001844 RID: 6212 RVA: 0x0008DFB9 File Offset: 0x0008C1B9
		internal ReflectionAwareCodeGen(IndentedWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0008DFC8 File Offset: 0x0008C1C8
		internal void WriteReflectionInit(TypeScope scope)
		{
			foreach (object obj in scope.Types)
			{
				Type type = (Type)obj;
				TypeDesc typeDesc = scope.GetTypeDesc(type);
				if (typeDesc.UseReflection)
				{
					this.WriteTypeInfo(scope, typeDesc, type);
				}
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0008E034 File Offset: 0x0008C234
		private string WriteTypeInfo(TypeScope scope, TypeDesc typeDesc, Type type)
		{
			this.InitTheFirstTime();
			string csharpName = typeDesc.CSharpName;
			string text = (string)this.reflectionVariables[csharpName];
			if (text != null)
			{
				return text;
			}
			if (type.IsArray)
			{
				text = this.GenerateVariableName("array", typeDesc.CSharpName);
				TypeDesc arrayElementTypeDesc = typeDesc.ArrayElementTypeDesc;
				if (arrayElementTypeDesc.UseReflection)
				{
					string text2 = this.WriteTypeInfo(scope, arrayElementTypeDesc, scope.GetTypeFromTypeDesc(arrayElementTypeDesc));
					this.writer.WriteLine(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text2,
						".MakeArrayType();"
					}));
				}
				else
				{
					string text3 = this.WriteAssemblyInfo(type);
					this.writer.Write(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text3,
						".GetType("
					}));
					this.WriteQuotedCSharpString(type.FullName);
					this.writer.WriteLine(");");
				}
			}
			else
			{
				text = this.GenerateVariableName("type", typeDesc.CSharpName);
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					string text4 = this.WriteTypeInfo(scope, scope.GetTypeDesc(underlyingType), underlyingType);
					this.writer.WriteLine(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = typeof(System.Nullable<>).MakeGenericType(new ",
						typeof(Type).FullName,
						"[] {",
						text4,
						"});"
					}));
				}
				else
				{
					string text5 = this.WriteAssemblyInfo(type);
					this.writer.Write(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text5,
						".GetType("
					}));
					this.WriteQuotedCSharpString(type.FullName);
					this.writer.WriteLine(");");
				}
			}
			this.reflectionVariables.Add(csharpName, text);
			TypeMapping typeMappingFromTypeDesc = scope.GetTypeMappingFromTypeDesc(typeDesc);
			if (typeMappingFromTypeDesc != null)
			{
				this.WriteMappingInfo(typeMappingFromTypeDesc, text, type);
			}
			if (typeDesc.IsCollection || typeDesc.IsEnumerable)
			{
				TypeDesc arrayElementTypeDesc2 = typeDesc.ArrayElementTypeDesc;
				if (arrayElementTypeDesc2.UseReflection)
				{
					this.WriteTypeInfo(scope, arrayElementTypeDesc2, scope.GetTypeFromTypeDesc(arrayElementTypeDesc2));
				}
				this.WriteCollectionInfo(text, typeDesc, type);
			}
			return text;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0008E2F0 File Offset: 0x0008C4F0
		private void InitTheFirstTime()
		{
			if (this.reflectionVariables == null)
			{
				this.reflectionVariables = new Hashtable();
				this.writer.Write(string.Format(CultureInfo.InvariantCulture, ReflectionAwareCodeGen.helperClassesForUseReflection, new object[]
				{
					"object",
					"string",
					typeof(Type).FullName,
					typeof(FieldInfo).FullName,
					typeof(PropertyInfo).FullName,
					typeof(MemberInfo).FullName,
					typeof(MemberTypes).FullName
				}));
				this.WriteDefaultIndexerInit(typeof(IList), typeof(Array).FullName, false, false);
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0008E3C0 File Offset: 0x0008C5C0
		private void WriteMappingInfo(TypeMapping mapping, string typeVariable, Type type)
		{
			string csharpName = mapping.TypeDesc.CSharpName;
			if (mapping is StructMapping)
			{
				StructMapping structMapping = mapping as StructMapping;
				for (int i = 0; i < structMapping.Members.Length; i++)
				{
					MemberMapping memberMapping = structMapping.Members[i];
					this.WriteMemberInfo(type, csharpName, typeVariable, memberMapping.Name);
					if (memberMapping.CheckShouldPersist)
					{
						string text = "ShouldSerialize" + memberMapping.Name;
						this.WriteMethodInfo(csharpName, typeVariable, text, false, Array.Empty<string>());
					}
					if (memberMapping.CheckSpecified != SpecifiedAccessor.None)
					{
						string text2 = memberMapping.Name + "Specified";
						this.WriteMemberInfo(type, csharpName, typeVariable, text2);
					}
					if (memberMapping.ChoiceIdentifier != null)
					{
						string memberName = memberMapping.ChoiceIdentifier.MemberName;
						this.WriteMemberInfo(type, csharpName, typeVariable, memberName);
					}
				}
				return;
			}
			if (mapping is EnumMapping)
			{
				FieldInfo[] fields = type.GetFields();
				for (int j = 0; j < fields.Length; j++)
				{
					this.WriteMemberInfo(type, csharpName, typeVariable, fields[j].Name);
				}
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0008E4C8 File Offset: 0x0008C6C8
		private void WriteCollectionInfo(string typeVariable, TypeDesc typeDesc, Type type)
		{
			string csharpName = CodeIdentifier.GetCSharpName(type);
			string csharpName2 = typeDesc.ArrayElementTypeDesc.CSharpName;
			bool useReflection = typeDesc.ArrayElementTypeDesc.UseReflection;
			if (typeDesc.IsCollection)
			{
				this.WriteDefaultIndexerInit(type, csharpName, typeDesc.UseReflection, useReflection);
			}
			else if (typeDesc.IsEnumerable)
			{
				if (typeDesc.IsGenericInterface)
				{
					this.WriteMethodInfo(csharpName, typeVariable, "System.Collections.Generic.IEnumerable*", true, Array.Empty<string>());
				}
				else if (!typeDesc.IsPrivateImplementation)
				{
					this.WriteMethodInfo(csharpName, typeVariable, "GetEnumerator", true, Array.Empty<string>());
				}
			}
			this.WriteMethodInfo(csharpName, typeVariable, "Add", false, new string[] { this.GetStringForTypeof(csharpName2, useReflection) });
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0008E570 File Offset: 0x0008C770
		private string WriteAssemblyInfo(Type type)
		{
			string fullName = type.Assembly.FullName;
			string text = (string)this.reflectionVariables[fullName];
			if (text == null)
			{
				int num = fullName.IndexOf(',');
				string text2 = ((num > -1) ? fullName.Substring(0, num) : fullName);
				text = this.GenerateVariableName("assembly", text2);
				this.writer.Write(string.Concat(new string[]
				{
					"static ",
					typeof(Assembly).FullName,
					" ",
					text,
					" = ResolveDynamicAssembly("
				}));
				this.WriteQuotedCSharpString(DynamicAssemblies.GetName(type.Assembly));
				this.writer.WriteLine(");");
				this.reflectionVariables.Add(fullName, text);
			}
			return text;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008E63C File Offset: 0x0008C83C
		private string WriteMemberInfo(Type type, string escapedName, string typeVariable, string memberName)
		{
			MemberInfo[] member = type.GetMember(memberName);
			for (int i = 0; i < member.Length; i++)
			{
				MemberTypes memberType = member[i].MemberType;
				if (memberType == MemberTypes.Property)
				{
					string text = this.GenerateVariableName("prop", memberName);
					this.writer.Write(string.Concat(new string[] { "static XSPropInfo ", text, " = new XSPropInfo(", typeVariable, ", " }));
					this.WriteQuotedCSharpString(memberName);
					this.writer.WriteLine(");");
					this.reflectionVariables.Add(memberName + ":" + escapedName, text);
					return text;
				}
				if (memberType == MemberTypes.Field)
				{
					string text2 = this.GenerateVariableName("field", memberName);
					this.writer.Write(string.Concat(new string[] { "static XSFieldInfo ", text2, " = new XSFieldInfo(", typeVariable, ", " }));
					this.WriteQuotedCSharpString(memberName);
					this.writer.WriteLine(");");
					this.reflectionVariables.Add(memberName + ":" + escapedName, text2);
					return text2;
				}
			}
			throw new InvalidOperationException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[] { member[0].ToString() }));
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0008E78C File Offset: 0x0008C98C
		private string WriteMethodInfo(string escapedName, string typeVariable, string memberName, bool isNonPublic, params string[] paramTypes)
		{
			string text = this.GenerateVariableName("method", memberName);
			this.writer.Write(string.Concat(new string[]
			{
				"static ",
				typeof(MethodInfo).FullName,
				" ",
				text,
				" = ",
				typeVariable,
				".GetMethod("
			}));
			this.WriteQuotedCSharpString(memberName);
			this.writer.Write(", ");
			string fullName = typeof(BindingFlags).FullName;
			this.writer.Write(fullName);
			this.writer.Write(".Public | ");
			this.writer.Write(fullName);
			this.writer.Write(".Instance | ");
			this.writer.Write(fullName);
			this.writer.Write(".Static");
			if (isNonPublic)
			{
				this.writer.Write(" | ");
				this.writer.Write(fullName);
				this.writer.Write(".NonPublic");
			}
			this.writer.Write(", null, ");
			this.writer.Write("new " + typeof(Type).FullName + "[] { ");
			for (int i = 0; i < paramTypes.Length; i++)
			{
				this.writer.Write(paramTypes[i]);
				if (i < paramTypes.Length - 1)
				{
					this.writer.Write(", ");
				}
			}
			this.writer.WriteLine("}, null);");
			this.reflectionVariables.Add(memberName + ":" + escapedName, text);
			return text;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0008E93C File Offset: 0x0008CB3C
		private string WriteDefaultIndexerInit(Type type, string escapedName, bool collectionUseReflection, bool elementUseReflection)
		{
			string text = this.GenerateVariableName("item", escapedName);
			PropertyInfo defaultIndexer = TypeScope.GetDefaultIndexer(type, null);
			this.writer.Write("static XSArrayInfo ");
			this.writer.Write(text);
			this.writer.Write("= new XSArrayInfo(");
			this.writer.Write(this.GetStringForTypeof(CodeIdentifier.GetCSharpName(type), collectionUseReflection));
			this.writer.Write(".GetProperty(");
			this.WriteQuotedCSharpString(defaultIndexer.Name);
			this.writer.Write(",");
			this.writer.Write(this.GetStringForTypeof(CodeIdentifier.GetCSharpName(defaultIndexer.PropertyType), elementUseReflection));
			this.writer.Write(",new ");
			this.writer.Write(typeof(Type[]).FullName);
			this.writer.WriteLine("{typeof(int)}));");
			this.reflectionVariables.Add("0:" + escapedName, text);
			return text;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0008EA3E File Offset: 0x0008CC3E
		private string GenerateVariableName(string prefix, string fullName)
		{
			this.nextReflectionVariableNumber++;
			return prefix + this.nextReflectionVariableNumber.ToString() + "_" + CodeIdentifier.MakeValidInternal(fullName.Replace('.', '_'));
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0008EA74 File Offset: 0x0008CC74
		internal string GetReflectionVariable(string typeFullName, string memberName)
		{
			string text;
			if (memberName == null)
			{
				text = typeFullName;
			}
			else
			{
				text = memberName + ":" + typeFullName;
			}
			return (string)this.reflectionVariables[text];
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0008EAA8 File Offset: 0x0008CCA8
		internal string GetStringForMethodInvoke(string obj, string escapedTypeName, string methodName, bool useReflection, params string[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (useReflection)
			{
				stringBuilder.Append(this.GetReflectionVariable(escapedTypeName, methodName));
				stringBuilder.Append(".Invoke(");
				stringBuilder.Append(obj);
				stringBuilder.Append(", new object[] {");
			}
			else
			{
				stringBuilder.Append(obj);
				stringBuilder.Append(".@");
				stringBuilder.Append(methodName);
				stringBuilder.Append("(");
			}
			for (int i = 0; i < args.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(args[i]);
			}
			if (useReflection)
			{
				stringBuilder.Append("})");
			}
			else
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0008EB68 File Offset: 0x0008CD68
		internal string GetStringForEnumCompare(EnumMapping mapping, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				CodeIdentifier.CheckValidIdentifier(memberName);
				return mapping.TypeDesc.CSharpName + ".@" + memberName;
			}
			string stringForEnumMember = this.GetStringForEnumMember(mapping.TypeDesc.CSharpName, memberName, useReflection);
			return this.GetStringForEnumLongValue(stringForEnumMember, useReflection);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0008EBB4 File Offset: 0x0008CDB4
		internal string GetStringForEnumLongValue(string variable, bool useReflection)
		{
			if (useReflection)
			{
				return typeof(Convert).FullName + ".ToInt64(" + variable + ")";
			}
			return string.Concat(new string[]
			{
				"((",
				typeof(long).FullName,
				")",
				variable,
				")"
			});
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0008EC1D File Offset: 0x0008CE1D
		internal string GetStringForTypeof(string typeFullName, bool useReflection)
		{
			if (useReflection)
			{
				return this.GetReflectionVariable(typeFullName, null);
			}
			return "typeof(" + typeFullName + ")";
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0008EC3C File Offset: 0x0008CE3C
		internal string GetStringForMember(string obj, string memberName, TypeDesc typeDesc)
		{
			if (!typeDesc.UseReflection)
			{
				return obj + ".@" + memberName;
			}
			while (typeDesc != null)
			{
				string csharpName = typeDesc.CSharpName;
				string reflectionVariable = this.GetReflectionVariable(csharpName, memberName);
				if (reflectionVariable != null)
				{
					return reflectionVariable + "[" + obj + "]";
				}
				typeDesc = typeDesc.BaseTypeDesc;
				if (typeDesc != null && !typeDesc.UseReflection)
				{
					return string.Concat(new string[] { "((", typeDesc.CSharpName, ")", obj, ").@", memberName });
				}
			}
			return "[" + obj + "]";
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0008ECDE File Offset: 0x0008CEDE
		internal string GetStringForEnumMember(string typeFullName, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				return typeFullName + ".@" + memberName;
			}
			return this.GetReflectionVariable(typeFullName, memberName) + "[null]";
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0008ED04 File Offset: 0x0008CF04
		internal string GetStringForArrayMember(string arrayName, string subscript, TypeDesc arrayTypeDesc)
		{
			if (!arrayTypeDesc.UseReflection)
			{
				return arrayName + "[" + subscript + "]";
			}
			string text = (arrayTypeDesc.IsCollection ? arrayTypeDesc.CSharpName : typeof(Array).FullName);
			string reflectionVariable = this.GetReflectionVariable(text, "0");
			return string.Concat(new string[] { reflectionVariable, "[", arrayName, ", ", subscript, "]" });
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0008ED87 File Offset: 0x0008CF87
		internal string GetStringForMethod(string obj, string typeFullName, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				return obj + "." + memberName + "(";
			}
			return this.GetReflectionVariable(typeFullName, memberName) + ".Invoke(" + obj + ", new object[]{";
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0008EDB7 File Offset: 0x0008CFB7
		internal string GetStringForCreateInstance(string escapedTypeName, bool useReflection, bool ctorInaccessible, bool cast)
		{
			return this.GetStringForCreateInstance(escapedTypeName, useReflection, ctorInaccessible, cast, string.Empty);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0008EDCC File Offset: 0x0008CFCC
		internal string GetStringForCreateInstance(string escapedTypeName, bool useReflection, bool ctorInaccessible, bool cast, string arg)
		{
			if (!useReflection && !ctorInaccessible)
			{
				return string.Concat(new string[] { "new ", escapedTypeName, "(", arg, ")" });
			}
			return this.GetStringForCreateInstance(this.GetStringForTypeof(escapedTypeName, useReflection), (cast && !useReflection) ? escapedTypeName : null, ctorInaccessible, arg);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0008EE28 File Offset: 0x0008D028
		internal string GetStringForCreateInstance(string type, string cast, bool nonPublic, string arg)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (cast != null && cast.Length > 0)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(cast);
				stringBuilder.Append(")");
			}
			stringBuilder.Append(typeof(Activator).FullName);
			stringBuilder.Append(".CreateInstance(");
			stringBuilder.Append(type);
			stringBuilder.Append(", ");
			string fullName = typeof(BindingFlags).FullName;
			stringBuilder.Append(fullName);
			stringBuilder.Append(".Instance | ");
			stringBuilder.Append(fullName);
			stringBuilder.Append(".Public | ");
			stringBuilder.Append(fullName);
			stringBuilder.Append(".CreateInstance");
			if (nonPublic)
			{
				stringBuilder.Append(" | ");
				stringBuilder.Append(fullName);
				stringBuilder.Append(".NonPublic");
			}
			if (arg == null || arg.Length == 0)
			{
				stringBuilder.Append(", null, new object[0], null)");
			}
			else
			{
				stringBuilder.Append(", null, new object[] { ");
				stringBuilder.Append(arg);
				stringBuilder.Append(" }, null)");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0008EF50 File Offset: 0x0008D150
		internal void WriteLocalDecl(string typeFullName, string variableName, string initValue, bool useReflection)
		{
			if (useReflection)
			{
				typeFullName = "object";
			}
			this.writer.Write(typeFullName);
			this.writer.Write(" ");
			this.writer.Write(variableName);
			if (initValue != null)
			{
				this.writer.Write(" = ");
				if (!useReflection && initValue != "null")
				{
					this.writer.Write("(" + typeFullName + ")");
				}
				this.writer.Write(initValue);
			}
			this.writer.WriteLine(";");
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0008EFEC File Offset: 0x0008D1EC
		internal void WriteCreateInstance(string escapedName, string source, bool useReflection, bool ctorInaccessible)
		{
			this.writer.Write(useReflection ? "object" : escapedName);
			this.writer.Write(" ");
			this.writer.Write(source);
			this.writer.Write(" = ");
			this.writer.Write(this.GetStringForCreateInstance(escapedName, useReflection, ctorInaccessible, !useReflection && ctorInaccessible));
			this.writer.WriteLine(";");
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0008F068 File Offset: 0x0008D268
		internal void WriteInstanceOf(string source, string escapedTypeName, bool useReflection)
		{
			if (!useReflection)
			{
				this.writer.Write(source);
				this.writer.Write(" is ");
				this.writer.Write(escapedTypeName);
				return;
			}
			this.writer.Write(this.GetReflectionVariable(escapedTypeName, null));
			this.writer.Write(".IsAssignableFrom(");
			this.writer.Write(source);
			this.writer.Write(".GetType())");
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0008F0E0 File Offset: 0x0008D2E0
		internal void WriteArrayLocalDecl(string typeName, string variableName, string initValue, TypeDesc arrayTypeDesc)
		{
			if (arrayTypeDesc.UseReflection)
			{
				if (arrayTypeDesc.IsEnumerable)
				{
					typeName = typeof(IEnumerable).FullName;
				}
				else if (arrayTypeDesc.IsCollection)
				{
					typeName = typeof(ICollection).FullName;
				}
				else
				{
					typeName = typeof(Array).FullName;
				}
			}
			this.writer.Write(typeName);
			this.writer.Write(" ");
			this.writer.Write(variableName);
			if (initValue != null)
			{
				this.writer.Write(" = ");
				if (initValue != "null")
				{
					this.writer.Write("(" + typeName + ")");
				}
				this.writer.Write(initValue);
			}
			this.writer.WriteLine(";");
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0008F1C0 File Offset: 0x0008D3C0
		internal void WriteEnumCase(string fullTypeName, ConstantMapping c, bool useReflection)
		{
			this.writer.Write("case ");
			if (useReflection)
			{
				this.writer.Write(c.Value.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				this.writer.Write(fullTypeName);
				this.writer.Write(".@");
				CodeIdentifier.CheckValidIdentifier(c.Name);
				this.writer.Write(c.Name);
			}
			this.writer.Write(": ");
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x0008F248 File Offset: 0x0008D448
		internal void WriteTypeCompare(string variable, string escapedTypeName, bool useReflection)
		{
			this.writer.Write(variable);
			this.writer.Write(" == ");
			this.writer.Write(this.GetStringForTypeof(escapedTypeName, useReflection));
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0008F27C File Offset: 0x0008D47C
		internal void WriteArrayTypeCompare(string variable, string escapedTypeName, string elementTypeName, bool useReflection)
		{
			if (!useReflection)
			{
				this.writer.Write(variable);
				this.writer.Write(" == typeof(");
				this.writer.Write(escapedTypeName);
				this.writer.Write(")");
				return;
			}
			this.writer.Write(variable);
			this.writer.Write(".IsArray ");
			this.writer.Write(" && ");
			this.WriteTypeCompare(variable + ".GetElementType()", elementTypeName, useReflection);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0008F308 File Offset: 0x0008D508
		internal static void WriteQuotedCSharpString(IndentedWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			writer.Write("@\"");
			foreach (char c in value)
			{
				if (c < ' ')
				{
					if (c == '\r')
					{
						writer.Write("\\r");
					}
					else if (c == '\n')
					{
						writer.Write("\\n");
					}
					else if (c == '\t')
					{
						writer.Write("\\t");
					}
					else
					{
						byte b = (byte)c;
						writer.Write("\\x");
						writer.Write("0123456789ABCDEF"[b >> 4]);
						writer.Write("0123456789ABCDEF"[(int)(b & 15)]);
					}
				}
				else if (c == '"')
				{
					writer.Write("\"\"");
				}
				else
				{
					writer.Write(c);
				}
			}
			writer.Write("\"");
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0008F3E6 File Offset: 0x0008D5E6
		internal void WriteQuotedCSharpString(string value)
		{
			ReflectionAwareCodeGen.WriteQuotedCSharpString(this.writer, value);
		}

		// Token: 0x04000A85 RID: 2693
		private const string hexDigits = "0123456789ABCDEF";

		// Token: 0x04000A86 RID: 2694
		private const string arrayMemberKey = "0";

		// Token: 0x04000A87 RID: 2695
		private Hashtable reflectionVariables;

		// Token: 0x04000A88 RID: 2696
		private int nextReflectionVariableNumber;

		// Token: 0x04000A89 RID: 2697
		private IndentedWriter writer;

		// Token: 0x04000A8A RID: 2698
		private static string helperClassesForUseReflection = "\n    sealed class XSFieldInfo {{\n       {3} fieldInfo;\n        public XSFieldInfo({2} t, {1} memberName){{\n            fieldInfo = t.GetField(memberName);\n        }}\n        public {0} this[{0} o] {{\n            get {{\n                return fieldInfo.GetValue(o);\n            }}\n            set {{\n                fieldInfo.SetValue(o, value);\n            }}\n        }}\n\n    }}\n    sealed class XSPropInfo {{\n        {4} propInfo;\n        public XSPropInfo({2} t, {1} memberName){{\n            propInfo = t.GetProperty(memberName);\n        }}\n        public {0} this[{0} o] {{\n            get {{\n                return propInfo.GetValue(o, null);\n            }}\n            set {{\n                propInfo.SetValue(o, value, null);\n            }}\n        }}\n    }}\n    sealed class XSArrayInfo {{\n        {4} propInfo;\n        public XSArrayInfo({4} propInfo){{\n            this.propInfo = propInfo;\n        }}\n        public {0} this[{0} a, int i] {{\n            get {{\n                return propInfo.GetValue(a, new {0}[]{{i}});\n            }}\n            set {{\n                propInfo.SetValue(a, value, new {0}[]{{i}});\n            }}\n        }}\n    }}\n";
	}
}
