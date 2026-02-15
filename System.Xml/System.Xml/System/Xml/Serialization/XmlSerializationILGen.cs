using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x020001C7 RID: 455
	internal class XmlSerializationILGen
	{
		// Token: 0x06001637 RID: 5687 RVA: 0x00071A50 File Offset: 0x0006FC50
		internal XmlSerializationILGen(TypeScope[] scopes, string access, string className)
		{
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareILGen();
			this.className = className;
			this.typeAttributes = TypeAttributes.Public;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00071AEE File Offset: 0x0006FCEE
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x00071AF6 File Offset: 0x0006FCF6
		internal int NextMethodNumber
		{
			get
			{
				return this.nextMethodNumber;
			}
			set
			{
				this.nextMethodNumber = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00071AFF File Offset: 0x0006FCFF
		internal ReflectionAwareILGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00071B07 File Offset: 0x0006FD07
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00071B0F File Offset: 0x0006FD0F
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00071B17 File Offset: 0x0006FD17
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00071B1F File Offset: 0x0006FD1F
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x00071B27 File Offset: 0x0006FD27
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00071B2F File Offset: 0x0006FD2F
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00071B37 File Offset: 0x0006FD37
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x00071B3F File Offset: 0x0006FD3F
		internal ModuleBuilder ModuleBuilder
		{
			get
			{
				return this.moduleBuilder;
			}
			set
			{
				this.moduleBuilder = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00071B48 File Offset: 0x0006FD48
		internal TypeAttributes TypeAttributes
		{
			get
			{
				return this.typeAttributes;
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00071B50 File Offset: 0x0006FD50
		internal static Regex NewRegex(string pattern)
		{
			Dictionary<string, Regex> dictionary = XmlSerializationILGen.regexs;
			Regex regex;
			lock (dictionary)
			{
				if (!XmlSerializationILGen.regexs.TryGetValue(pattern, out regex))
				{
					regex = new Regex(pattern);
					XmlSerializationILGen.regexs.Add(pattern, regex);
				}
			}
			return regex;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00071BAC File Offset: 0x0006FDAC
		internal MethodBuilder EnsureMethodBuilder(TypeBuilder typeBuilder, string methodName, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			MethodBuilderInfo methodBuilderInfo;
			if (!this.methodBuilders.TryGetValue(methodName, out methodBuilderInfo))
			{
				methodBuilderInfo = new MethodBuilderInfo(typeBuilder.DefineMethod(methodName, attributes, returnType, parameterTypes), parameterTypes);
				this.methodBuilders.Add(methodName, methodBuilderInfo);
			}
			return methodBuilderInfo.MethodBuilder;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00071BF0 File Offset: 0x0006FDF0
		internal MethodBuilderInfo GetMethodBuilder(string methodName)
		{
			return this.methodBuilders[methodName];
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00071C00 File Offset: 0x0006FE00
		internal void GenerateReferencedMethods()
		{
			while (this.references > 0)
			{
				TypeMapping[] array = this.referencedMethods;
				int num = this.references - 1;
				this.references = num;
				TypeMapping typeMapping = array[num];
				this.GenerateMethod(typeMapping);
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00071C38 File Offset: 0x0006FE38
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00071C98 File Offset: 0x0006FE98
		private TypeMapping[] EnsureArrayIndex(TypeMapping[] a, int index)
		{
			if (a == null)
			{
				return new TypeMapping[32];
			}
			if (index < a.Length)
			{
				return a;
			}
			TypeMapping[] array = new TypeMapping[a.Length + 32];
			Array.Copy(a, array, index);
			return array;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00071CD0 File Offset: 0x0006FED0
		internal FieldBuilder GenerateHashtableGetBegin(string privateName, string publicName, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = serializerContractTypeBuilder.DefineField(privateName, typeof(Hashtable), FieldAttributes.Private);
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			PropertyBuilder propertyBuilder = serializerContractTypeBuilder.DefineProperty(publicName, PropertyAttributes.None, CallingConventions.HasThis, typeof(Hashtable), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(Hashtable), "get_" + publicName, CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			ConstructorInfo constructor = typeof(Hashtable).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			LocalBuilder localBuilder = this.ilg.DeclareLocal(typeof(Hashtable), "_tmp");
			this.ilg.New(constructor);
			this.ilg.Stloc(localBuilder);
			return fieldBuilder;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00071DDC File Offset: 0x0006FFDC
		internal void GenerateHashtableGetEnd(FieldBuilder fieldBuilder)
		{
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			this.ilg.Ldarg(0);
			this.ilg.Ldloc(typeof(Hashtable), "_tmp");
			this.ilg.StoreMember(fieldBuilder);
			this.ilg.EndIf();
			this.ilg.EndIf();
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00071E94 File Offset: 0x00070094
		internal FieldBuilder GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(privateName, publicName, serializerContractTypeBuilder);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				MethodInfo method = typeof(Hashtable).GetMethod("set_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(object),
					typeof(object)
				}, null);
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.ilg.Ldloc(typeof(Hashtable), "_tmp");
						this.ilg.Ldstr(xmlMappings[i].Key);
						this.ilg.Ldstr(methods[i]);
						this.ilg.Call(method);
					}
				}
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00071F6C File Offset: 0x0007016C
		internal void GenerateSupportedTypes(Type[] types, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanSerialize", new Type[] { typeof(Type) }, new string[] { "type" }, CodeGenerator.PublicOverrideMethodAttributes);
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !type.IsGenericType && !type.ContainsGenericParameters)
				{
					hashtable[type] = type;
					this.ilg.Ldarg("type");
					this.ilg.Ldc(type);
					this.ilg.If(Cmp.EqualTo);
					this.ilg.Ldc(true);
					this.ilg.GotoMethodEnd();
					this.ilg.EndIf();
				}
			}
			this.ilg.Ldc(false);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0007208C File Offset: 0x0007028C
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(baseSerializer), TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.BeforeFieldInit, typeof(XmlSerializer), CodeGenerator.EmptyTypeArray);
			ConstructorInfo constructor = this.CreatedTypes[readerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "CreateReader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			ConstructorInfo constructor2 = this.CreatedTypes[writerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "CreateWriter", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor2);
			this.ilg.EndMethod();
			typeBuilder.DefineDefaultConstructor(MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return baseSerializer;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000721C4 File Offset: 0x000703C4
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(text), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, this.CreatedTypes[baseSerializer], CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanDeserialize", new Type[] { typeof(XmlReader) }, new string[] { "xmlReader" }, CodeGenerator.PublicOverrideMethodAttributes);
			if (mapping.Accessor.Any)
			{
				this.ilg.Ldc(true);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			else
			{
				MethodInfo method = typeof(XmlReader).GetMethod("IsStartElement", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(string)
				}, null);
				this.ilg.Ldarg(this.ilg.GetArg("xmlReader"));
				this.ilg.Ldstr(mapping.Accessor.Name);
				this.ilg.Ldstr(mapping.Accessor.Namespace);
				this.ilg.Call(method);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
			if (writeMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(void), "Serialize", new Type[]
				{
					typeof(object),
					typeof(XmlSerializationWriter)
				}, new string[] { "objectToSerialize", "writer" }, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method2 = this.CreatedTypes[writerClass].GetMethod(writeMethod, CodeGenerator.InstanceBindingFlags, null, new Type[] { (mapping is XmlMembersMapping) ? typeof(object[]) : typeof(object) }, null);
				this.ilg.Ldarg("writer");
				this.ilg.Castclass(this.CreatedTypes[writerClass]);
				this.ilg.Ldarg("objectToSerialize");
				if (mapping is XmlMembersMapping)
				{
					this.ilg.ConvertValue(typeof(object), typeof(object[]));
				}
				this.ilg.Call(method2);
				this.ilg.EndMethod();
			}
			if (readMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(object), "Deserialize", new Type[] { typeof(XmlSerializationReader) }, new string[] { "reader" }, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method3 = this.CreatedTypes[readerClass].GetMethod(readMethod, CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg("reader");
				this.ilg.Castclass(this.CreatedTypes[readerClass]);
				this.ilg.Call(method3);
				this.ilg.EndMethod();
			}
			typeBuilder.DefineDefaultConstructor(CodeGenerator.PublicMethodAttributes);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return type.Name;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x000725B8 File Offset: 0x000707B8
		private FieldBuilder GenerateTypedSerializers(Hashtable serializers, TypeBuilder serializerContractTypeBuilder)
		{
			string text = "typedSerializers";
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(text, "TypedSerializers", serializerContractTypeBuilder);
			MethodInfo method = typeof(Hashtable).GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(object)
			}, null);
			foreach (object obj in serializers.Keys)
			{
				string text2 = (string)obj;
				ConstructorInfo constructor = this.CreatedTypes[(string)serializers[text2]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldloc(typeof(Hashtable), "_tmp");
				this.ilg.Ldstr(text2);
				this.ilg.New(constructor);
				this.ilg.Call(method);
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000726D4 File Offset: 0x000708D4
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializer), "GetSerializer", new Type[] { typeof(Type) }, new string[] { "type" }, CodeGenerator.PublicOverrideMethodAttributes);
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !type.IsGenericType && !type.ContainsGenericParameters)
					{
						this.ilg.Ldarg("type");
						this.ilg.Ldc(type);
						this.ilg.If(Cmp.EqualTo);
						ConstructorInfo constructor = this.CreatedTypes[(string)serializers[xmlMappings[i].Key]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.New(constructor);
						this.ilg.Stloc(this.ilg.ReturnLocal);
						this.ilg.Br(this.ilg.ReturnLabel);
						this.ilg.EndIf();
					}
				}
			}
			this.ilg.Load(null);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			this.ilg.Br(this.ilg.ReturnLabel);
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000728A8 File Offset: 0x00070AA8
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, "XmlSerializerContract", TypeAttributes.Public | TypeAttributes.BeforeFieldInit, typeof(XmlSerializerImplementation), CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("Reader", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationReader), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "get_Reader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			ConstructorInfo constructorInfo = this.CreatedTypes[readerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructorInfo);
			this.ilg.EndMethod();
			this.ilg = new CodeGenerator(typeBuilder);
			PropertyBuilder propertyBuilder2 = typeBuilder.DefineProperty("Writer", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationWriter), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "get_Writer", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder2.SetGetMethod(this.ilg.MethodBuilder);
			constructorInfo = this.CreatedTypes[writerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructorInfo);
			this.ilg.EndMethod();
			FieldBuilder fieldBuilder = this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings, typeBuilder);
			FieldBuilder fieldBuilder2 = this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings, typeBuilder);
			FieldBuilder fieldBuilder3 = this.GenerateTypedSerializers(serializers, typeBuilder);
			this.GenerateSupportedTypes(types, typeBuilder);
			this.GenerateGetSerializer(serializers, xmlMappings, typeBuilder);
			ConstructorInfo constructor = typeof(XmlSerializerImplementation).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(void), ".ctor", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicMethodAttributes | MethodAttributes.RTSpecialName | MethodAttributes.SpecialName);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(fieldBuilder);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(fieldBuilder2);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(fieldBuilder3);
			this.ilg.Ldarg(0);
			this.ilg.Call(constructor);
			this.ilg.EndMethod();
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00071A2C File Offset: 0x0006FC2C
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00072B6B File Offset: 0x00070D6B
		internal void ILGenLoad(string source)
		{
			this.ILGenLoad(source, null);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00072B78 File Offset: 0x00070D78
		internal void ILGenLoad(string source, Type type)
		{
			if (source.StartsWith("o.@", StringComparison.Ordinal))
			{
				MemberInfo memberInfo = this.memberInfos[source.Substring(3)];
				this.ilg.LoadMember(this.ilg.GetVariable("o"), memberInfo);
				if (type != null)
				{
					Type type2 = ((memberInfo.MemberType == MemberTypes.Field) ? ((FieldInfo)memberInfo).FieldType : ((PropertyInfo)memberInfo).PropertyType);
					this.ilg.ConvertValue(type2, type);
					return;
				}
			}
			else
			{
				new SourceInfo(source, null, null, null, this.ilg).Load(type);
			}
		}

		// Token: 0x040009D2 RID: 2514
		private int nextMethodNumber;

		// Token: 0x040009D3 RID: 2515
		private Hashtable methodNames = new Hashtable();

		// Token: 0x040009D4 RID: 2516
		private Dictionary<string, MethodBuilderInfo> methodBuilders = new Dictionary<string, MethodBuilderInfo>();

		// Token: 0x040009D5 RID: 2517
		internal Dictionary<string, Type> CreatedTypes = new Dictionary<string, Type>();

		// Token: 0x040009D6 RID: 2518
		internal Dictionary<string, MemberInfo> memberInfos = new Dictionary<string, MemberInfo>();

		// Token: 0x040009D7 RID: 2519
		private ReflectionAwareILGen raCodeGen;

		// Token: 0x040009D8 RID: 2520
		private TypeScope[] scopes;

		// Token: 0x040009D9 RID: 2521
		private TypeDesc stringTypeDesc;

		// Token: 0x040009DA RID: 2522
		private TypeDesc qnameTypeDesc;

		// Token: 0x040009DB RID: 2523
		private string className;

		// Token: 0x040009DC RID: 2524
		private TypeMapping[] referencedMethods;

		// Token: 0x040009DD RID: 2525
		private int references;

		// Token: 0x040009DE RID: 2526
		private Hashtable generatedMethods = new Hashtable();

		// Token: 0x040009DF RID: 2527
		private ModuleBuilder moduleBuilder;

		// Token: 0x040009E0 RID: 2528
		private TypeAttributes typeAttributes;

		// Token: 0x040009E1 RID: 2529
		protected TypeBuilder typeBuilder;

		// Token: 0x040009E2 RID: 2530
		protected CodeGenerator ilg;

		// Token: 0x040009E3 RID: 2531
		private static Dictionary<string, Regex> regexs = new Dictionary<string, Regex>();
	}
}
