using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Xml.Serialization.Configuration;
using Microsoft.Win32;

namespace System.Xml.Serialization
{
	// Token: 0x02000151 RID: 337
	internal class TempAssembly
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x00051368 File Offset: 0x0004F568
		private TempAssembly()
		{
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0005137C File Offset: 0x0004F57C
		internal TempAssembly(XmlMapping[] xmlMappings, Type[] types, string defaultNamespace, string location, Evidence evidence)
		{
			bool flag = false;
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				xmlMappings[i].CheckShallow();
				if (xmlMappings[i].IsSoap)
				{
					flag = true;
				}
			}
			bool flag2 = false;
			if (!flag && !TempAssembly.UseLegacySerializerGeneration)
			{
				try
				{
					this.assembly = TempAssembly.GenerateRefEmitAssembly(xmlMappings, types, defaultNamespace, evidence);
					goto IL_005A;
				}
				catch (CodeGeneratorConversionException)
				{
					flag2 = true;
					goto IL_005A;
				}
			}
			flag2 = true;
			IL_005A:
			if (flag2)
			{
				this.assembly = TempAssembly.GenerateAssembly(xmlMappings, types, defaultNamespace, evidence, XmlSerializerCompilerParameters.Create(location), null, this.assemblies);
			}
			this.InitAssemblyMethods(xmlMappings);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0005141C File Offset: 0x0004F61C
		internal TempAssembly(XmlMapping[] xmlMappings, Assembly assembly, XmlSerializerImplementation contract)
		{
			this.assembly = assembly;
			this.InitAssemblyMethods(xmlMappings);
			this.contract = contract;
			this.pregeneratedAssmbly = true;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0005144C File Offset: 0x0004F64C
		internal static bool UseLegacySerializerGeneration
		{
			get
			{
				if (AppSettings.UseLegacySerializerGeneration != null)
				{
					return AppSettings.UseLegacySerializerGeneration.Value;
				}
				XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
				return xmlSerializerSection != null && xmlSerializerSection.UseLegacySerializerGeneration;
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00051491 File Offset: 0x0004F691
		internal TempAssembly(XmlSerializerImplementation contract)
		{
			this.contract = contract;
			this.pregeneratedAssmbly = true;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x000514B2 File Offset: 0x0004F6B2
		internal XmlSerializerImplementation Contract
		{
			get
			{
				if (this.contract == null)
				{
					this.contract = (XmlSerializerImplementation)Activator.CreateInstance(TempAssembly.GetTypeFromAssembly(this.assembly, "XmlSerializerContract"));
				}
				return this.contract;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000514E4 File Offset: 0x0004F6E4
		internal void InitAssemblyMethods(XmlMapping[] xmlMappings)
		{
			this.methods = new TempAssembly.TempMethodDictionary();
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				TempAssembly.TempMethod tempMethod = new TempAssembly.TempMethod();
				tempMethod.isSoap = xmlMappings[i].IsSoap;
				tempMethod.methodKey = xmlMappings[i].Key;
				XmlTypeMapping xmlTypeMapping = xmlMappings[i] as XmlTypeMapping;
				if (xmlTypeMapping != null)
				{
					tempMethod.name = xmlTypeMapping.ElementName;
					tempMethod.ns = xmlTypeMapping.Namespace;
				}
				this.methods.Add(xmlMappings[i].Key, tempMethod);
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00051564 File Offset: 0x0004F764
		internal static Assembly LoadGeneratedAssembly(Type type, string defaultNamespace, out XmlSerializerImplementation contract)
		{
			Assembly assembly = null;
			contract = null;
			string text = null;
			if (UnsafeNativeMethods.IsPackagedProcess.Value)
			{
				return null;
			}
			bool enabled = DiagnosticsSwitches.PregenEventLog.Enabled;
			object[] customAttributes = type.GetCustomAttributes(typeof(XmlSerializerAssemblyAttribute), false);
			if (customAttributes.Length == 0)
			{
				AssemblyName name = TempAssembly.GetName(type.Assembly, true);
				text = Compiler.GetTempAssemblyName(name, defaultNamespace);
				name.Name = text;
				name.CodeBase = null;
				name.CultureInfo = CultureInfo.InvariantCulture;
				try
				{
					assembly = Assembly.Load(name);
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					if (enabled)
					{
						TempAssembly.Log(ex.Message, EventLogEntryType.Information);
					}
					byte[] publicKeyToken = name.GetPublicKeyToken();
					if (publicKeyToken != null && publicKeyToken.Length != 0)
					{
						return null;
					}
					assembly = Assembly.LoadWithPartialName(text, null);
				}
				if (assembly == null)
				{
					if (enabled)
					{
						TempAssembly.Log(Res.GetString("Could not load file or assembly '{0}' or one of its dependencies. The system cannot find the file specified.", new object[] { text }), EventLogEntryType.Information);
					}
					return null;
				}
				if (!TempAssembly.IsSerializerVersionMatch(assembly, type, defaultNamespace, null))
				{
					if (enabled)
					{
						TempAssembly.Log(Res.GetString("Pre-generated serializer '{0}' has expired. You need to re-generate serializer for '{1}'.", new object[] { text, type.FullName }), EventLogEntryType.Error);
					}
					return null;
				}
				goto IL_01D1;
			}
			XmlSerializerAssemblyAttribute xmlSerializerAssemblyAttribute = (XmlSerializerAssemblyAttribute)customAttributes[0];
			if (xmlSerializerAssemblyAttribute.AssemblyName != null && xmlSerializerAssemblyAttribute.CodeBase != null)
			{
				throw new InvalidOperationException(Res.GetString("Invalid XmlSerializerAssemblyAttribute usage. Please use {0} property or {1} property.", new object[] { "AssemblyName", "CodeBase" }));
			}
			if (xmlSerializerAssemblyAttribute.AssemblyName != null)
			{
				text = xmlSerializerAssemblyAttribute.AssemblyName;
				assembly = Assembly.LoadWithPartialName(text, null);
			}
			else if (xmlSerializerAssemblyAttribute.CodeBase != null && xmlSerializerAssemblyAttribute.CodeBase.Length > 0)
			{
				text = xmlSerializerAssemblyAttribute.CodeBase;
				assembly = Assembly.LoadFrom(text);
			}
			else
			{
				text = type.Assembly.FullName;
				assembly = type.Assembly;
			}
			if (assembly == null)
			{
				throw new FileNotFoundException(null, text);
			}
			IL_01D1:
			Type typeFromAssembly = TempAssembly.GetTypeFromAssembly(assembly, "XmlSerializerContract");
			contract = (XmlSerializerImplementation)Activator.CreateInstance(typeFromAssembly);
			if (contract.CanSerialize(type))
			{
				return assembly;
			}
			if (enabled)
			{
				TempAssembly.Log(Res.GetString("Pre-generated serializer '{0}' has expired. You need to re-generate serializer for '{1}'.", new object[] { text, type.FullName }), EventLogEntryType.Error);
			}
			return null;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000517A4 File Offset: 0x0004F9A4
		private static void Log(string message, EventLogEntryType type)
		{
			new EventLogPermission(PermissionState.Unrestricted).Assert();
			EventLog.WriteEntry("XmlSerializer", message, type);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000517BD File Offset: 0x0004F9BD
		private static AssemblyName GetName(Assembly assembly, bool copyName)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			permissionSet.Assert();
			return assembly.GetName(copyName);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000517E0 File Offset: 0x0004F9E0
		private static bool IsSerializerVersionMatch(Assembly serializer, Type type, string defaultNamespace, string location)
		{
			if (serializer == null)
			{
				return false;
			}
			object[] customAttributes = serializer.GetCustomAttributes(typeof(XmlSerializerVersionAttribute), false);
			if (customAttributes.Length != 1)
			{
				return false;
			}
			XmlSerializerVersionAttribute xmlSerializerVersionAttribute = (XmlSerializerVersionAttribute)customAttributes[0];
			return xmlSerializerVersionAttribute.ParentAssemblyId == TempAssembly.GenerateAssemblyId(type) && xmlSerializerVersionAttribute.Namespace == defaultNamespace;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00051840 File Offset: 0x0004FA40
		private static string GenerateAssemblyId(Type type)
		{
			Module[] modules = type.Assembly.GetModules();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < modules.Length; i++)
			{
				arrayList.Add(modules[i].ModuleVersionId.ToString());
			}
			arrayList.Sort();
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < arrayList.Count; j++)
			{
				stringBuilder.Append(arrayList[j].ToString());
				stringBuilder.Append(",");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000518D4 File Offset: 0x0004FAD4
		internal static Assembly GenerateAssembly(XmlMapping[] xmlMappings, Type[] types, string defaultNamespace, Evidence evidence, XmlSerializerCompilerParameters parameters, Assembly assembly, Hashtable assemblies)
		{
			TempAssembly.FileIOPermission.Assert();
			Compiler compiler = new Compiler();
			Assembly assembly3;
			try
			{
				Hashtable hashtable = new Hashtable();
				foreach (XmlMapping xmlMapping in xmlMappings)
				{
					hashtable[xmlMapping.Scope] = xmlMapping;
				}
				TypeScope[] array = new TypeScope[hashtable.Keys.Count];
				hashtable.Keys.CopyTo(array, 0);
				assemblies.Clear();
				Hashtable hashtable2 = new Hashtable();
				TypeScope[] array2 = array;
				int i;
				for (i = 0; i < array2.Length; i++)
				{
					foreach (object obj in array2[i].Types)
					{
						Type type = (Type)obj;
						compiler.AddImport(type, hashtable2);
						Assembly assembly2 = type.Assembly;
						string fullName = assembly2.FullName;
						if (assemblies[fullName] == null && !assembly2.GlobalAssemblyCache)
						{
							assemblies[fullName] = assembly2;
						}
					}
				}
				for (int j = 0; j < types.Length; j++)
				{
					compiler.AddImport(types[j], hashtable2);
				}
				compiler.AddImport(typeof(object).Assembly);
				compiler.AddImport(typeof(XmlSerializer).Assembly);
				IndentedWriter indentedWriter = new IndentedWriter(compiler.Source, false);
				indentedWriter.WriteLine("#if _DYNAMIC_XMLSERIALIZER_COMPILATION");
				indentedWriter.WriteLine("[assembly:System.Security.AllowPartiallyTrustedCallers()]");
				indentedWriter.WriteLine("[assembly:System.Security.SecurityTransparent()]");
				indentedWriter.WriteLine("[assembly:System.Security.SecurityRules(System.Security.SecurityRuleSet.Level1)]");
				indentedWriter.WriteLine("#endif");
				if (types != null && types.Length != 0 && types[0] != null)
				{
					indentedWriter.WriteLine("[assembly:System.Reflection.AssemblyVersionAttribute(\"" + types[0].Assembly.GetName().Version.ToString() + "\")]");
				}
				if (assembly != null && types.Length != 0)
				{
					for (int k = 0; k < types.Length; k++)
					{
						Type type2 = types[k];
						if (!(type2 == null) && DynamicAssemblies.IsTypeDynamic(type2))
						{
							throw new InvalidOperationException(Res.GetString("Cannot pre-generate serialization assembly for type '{0}'. Pre-generation of serialization assemblies is not supported for dynamic types. Save the assembly and load it from disk to use it with XmlSerialization.", new object[] { types[k].FullName }));
						}
					}
					indentedWriter.Write("[assembly:");
					indentedWriter.Write(typeof(XmlSerializerVersionAttribute).FullName);
					indentedWriter.Write("(");
					indentedWriter.Write("ParentAssemblyId=");
					ReflectionAwareCodeGen.WriteQuotedCSharpString(indentedWriter, TempAssembly.GenerateAssemblyId(types[0]));
					indentedWriter.Write(", Version=");
					ReflectionAwareCodeGen.WriteQuotedCSharpString(indentedWriter, "4.0.0.0");
					if (defaultNamespace != null)
					{
						indentedWriter.Write(", Namespace=");
						ReflectionAwareCodeGen.WriteQuotedCSharpString(indentedWriter, defaultNamespace);
					}
					indentedWriter.WriteLine(")]");
				}
				CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
				codeIdentifiers.AddUnique("XmlSerializationWriter", "XmlSerializationWriter");
				codeIdentifiers.AddUnique("XmlSerializationReader", "XmlSerializationReader");
				string text = null;
				if (types != null && types.Length == 1 && types[0] != null)
				{
					text = CodeIdentifier.MakeValid(types[0].Name);
					if (types[0].IsArray)
					{
						text += "Array";
					}
				}
				indentedWriter.WriteLine("namespace Microsoft.Xml.Serialization.GeneratedAssembly {");
				IndentedWriter indentedWriter2 = indentedWriter;
				i = indentedWriter2.Indent;
				indentedWriter2.Indent = i + 1;
				indentedWriter.WriteLine();
				string text2 = "XmlSerializationWriter" + text;
				text2 = codeIdentifiers.AddUnique(text2, text2);
				XmlSerializationWriterCodeGen xmlSerializationWriterCodeGen = new XmlSerializationWriterCodeGen(indentedWriter, array, "public", text2);
				xmlSerializationWriterCodeGen.GenerateBegin();
				string[] array3 = new string[xmlMappings.Length];
				for (int l = 0; l < xmlMappings.Length; l++)
				{
					array3[l] = xmlSerializationWriterCodeGen.GenerateElement(xmlMappings[l]);
				}
				xmlSerializationWriterCodeGen.GenerateEnd();
				indentedWriter.WriteLine();
				string text3 = "XmlSerializationReader" + text;
				text3 = codeIdentifiers.AddUnique(text3, text3);
				XmlSerializationReaderCodeGen xmlSerializationReaderCodeGen = new XmlSerializationReaderCodeGen(indentedWriter, array, "public", text3);
				xmlSerializationReaderCodeGen.GenerateBegin();
				string[] array4 = new string[xmlMappings.Length];
				for (int m = 0; m < xmlMappings.Length; m++)
				{
					array4[m] = xmlSerializationReaderCodeGen.GenerateElement(xmlMappings[m]);
				}
				xmlSerializationReaderCodeGen.GenerateEnd(array4, xmlMappings, types);
				string text4 = xmlSerializationReaderCodeGen.GenerateBaseSerializer("XmlSerializer1", text3, text2, codeIdentifiers);
				Hashtable hashtable3 = new Hashtable();
				for (int n = 0; n < xmlMappings.Length; n++)
				{
					if (hashtable3[xmlMappings[n].Key] == null)
					{
						hashtable3[xmlMappings[n].Key] = xmlSerializationReaderCodeGen.GenerateTypedSerializer(array4[n], array3[n], xmlMappings[n], codeIdentifiers, text4, text3, text2);
					}
				}
				xmlSerializationReaderCodeGen.GenerateSerializerContract("XmlSerializerContract", xmlMappings, types, text3, array4, text2, array3, hashtable3);
				IndentedWriter indentedWriter3 = indentedWriter;
				i = indentedWriter3.Indent;
				indentedWriter3.Indent = i - 1;
				indentedWriter.WriteLine("}");
				assembly3 = compiler.Compile(assembly, defaultNamespace, parameters, evidence);
			}
			finally
			{
				compiler.Close();
			}
			return assembly3;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00051DF0 File Offset: 0x0004FFF0
		internal static Assembly GenerateRefEmitAssembly(XmlMapping[] xmlMappings, Type[] types, string defaultNamespace, Evidence evidence)
		{
			Hashtable hashtable = new Hashtable();
			foreach (XmlMapping xmlMapping in xmlMappings)
			{
				hashtable[xmlMapping.Scope] = xmlMapping;
			}
			TypeScope[] array = new TypeScope[hashtable.Keys.Count];
			hashtable.Keys.CopyTo(array, 0);
			string text = "Microsoft.GeneratedCode";
			AssemblyBuilder assemblyBuilder = CodeGenerator.CreateAssemblyBuilder(AppDomain.CurrentDomain, text);
			ConstructorInfo constructor = typeof(SecurityTransparentAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor, new object[0]));
			ConstructorInfo constructor2 = typeof(AllowPartiallyTrustedCallersAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor2, new object[0]));
			ConstructorInfo constructor3 = typeof(SecurityRulesAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(SecurityRuleSet) }, null);
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor3, new object[] { SecurityRuleSet.Level1 }));
			if (types != null && types.Length != 0 && types[0] != null)
			{
				ConstructorInfo constructor4 = typeof(AssemblyVersionAttribute).GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(string) }, null);
				TempAssembly.FileIOPermission.Assert();
				string text2 = types[0].Assembly.GetName().Version.ToString();
				CodeAccessPermission.RevertAssert();
				assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(constructor4, new object[] { text2 }));
			}
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			codeIdentifiers.AddUnique("XmlSerializationWriter", "XmlSerializationWriter");
			codeIdentifiers.AddUnique("XmlSerializationReader", "XmlSerializationReader");
			string text3 = null;
			if (types != null && types.Length == 1 && types[0] != null)
			{
				text3 = CodeIdentifier.MakeValid(types[0].Name);
				if (types[0].IsArray)
				{
					text3 += "Array";
				}
			}
			ModuleBuilder moduleBuilder = CodeGenerator.CreateModuleBuilder(assemblyBuilder, text);
			string text4 = "XmlSerializationWriter" + text3;
			text4 = codeIdentifiers.AddUnique(text4, text4);
			XmlSerializationWriterILGen xmlSerializationWriterILGen = new XmlSerializationWriterILGen(array, "public", text4);
			xmlSerializationWriterILGen.ModuleBuilder = moduleBuilder;
			xmlSerializationWriterILGen.GenerateBegin();
			string[] array2 = new string[xmlMappings.Length];
			for (int j = 0; j < xmlMappings.Length; j++)
			{
				array2[j] = xmlSerializationWriterILGen.GenerateElement(xmlMappings[j]);
			}
			Type type = xmlSerializationWriterILGen.GenerateEnd();
			string text5 = "XmlSerializationReader" + text3;
			text5 = codeIdentifiers.AddUnique(text5, text5);
			XmlSerializationReaderILGen xmlSerializationReaderILGen = new XmlSerializationReaderILGen(array, "public", text5);
			xmlSerializationReaderILGen.ModuleBuilder = moduleBuilder;
			xmlSerializationReaderILGen.CreatedTypes.Add(type.Name, type);
			xmlSerializationReaderILGen.GenerateBegin();
			string[] array3 = new string[xmlMappings.Length];
			for (int k = 0; k < xmlMappings.Length; k++)
			{
				array3[k] = xmlSerializationReaderILGen.GenerateElement(xmlMappings[k]);
			}
			xmlSerializationReaderILGen.GenerateEnd(array3, xmlMappings, types);
			string text6 = xmlSerializationReaderILGen.GenerateBaseSerializer("XmlSerializer1", text5, text4, codeIdentifiers);
			Hashtable hashtable2 = new Hashtable();
			for (int l = 0; l < xmlMappings.Length; l++)
			{
				if (hashtable2[xmlMappings[l].Key] == null)
				{
					hashtable2[xmlMappings[l].Key] = xmlSerializationReaderILGen.GenerateTypedSerializer(array3[l], array2[l], xmlMappings[l], codeIdentifiers, text6, text5, text4);
				}
			}
			xmlSerializationReaderILGen.GenerateSerializerContract("XmlSerializerContract", xmlMappings, types, text5, array3, text4, array2, hashtable2);
			if (DiagnosticsSwitches.KeepTempFiles.Enabled)
			{
				TempAssembly.FileIOPermission.Assert();
				assemblyBuilder.Save(text + ".dll");
			}
			return type.Assembly;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0005219C File Offset: 0x0005039C
		private static MethodInfo GetMethodFromType(Type type, string methodName, Assembly assembly)
		{
			MethodInfo method = type.GetMethod(methodName);
			if (method != null)
			{
				return method;
			}
			MissingMethodException ex = new MissingMethodException(type.FullName, methodName);
			if (assembly != null)
			{
				throw new InvalidOperationException(Res.GetString("Pre-generated assembly '{0}' CodeBase='{1}' has expired.", new object[] { assembly.FullName, assembly.CodeBase }), ex);
			}
			throw ex;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000521FC File Offset: 0x000503FC
		internal static Type GetTypeFromAssembly(Assembly assembly, string typeName)
		{
			typeName = "Microsoft.Xml.Serialization.GeneratedAssembly." + typeName;
			Type type = assembly.GetType(typeName);
			if (type == null)
			{
				throw new InvalidOperationException(Res.GetString("Invalid serialization assembly: Required type {0} cannot be found in the generated assembly '{1}'.", new object[] { typeName, assembly.FullName }));
			}
			return type;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0005224C File Offset: 0x0005044C
		internal bool CanRead(XmlMapping mapping, XmlReader xmlReader)
		{
			if (mapping == null)
			{
				return false;
			}
			if (mapping.Accessor.Any)
			{
				return true;
			}
			TempAssembly.TempMethod tempMethod = this.methods[mapping.Key];
			return xmlReader.IsStartElement(tempMethod.name, tempMethod.ns);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00052294 File Offset: 0x00050494
		private string ValidateEncodingStyle(string encodingStyle, string methodKey)
		{
			if (encodingStyle != null && encodingStyle.Length > 0)
			{
				if (!this.methods[methodKey].isSoap)
				{
					throw new InvalidOperationException(Res.GetString("The encoding style '{0}' is not valid for this call because this XmlSerializer instance does not support encoding. Use the SoapReflectionImporter to initialize an XmlSerializer that supports encoding.", new object[] { encodingStyle }));
				}
				if (encodingStyle != "http://schemas.xmlsoap.org/soap/encoding/" && encodingStyle != "http://www.w3.org/2003/05/soap-encoding")
				{
					throw new InvalidOperationException(Res.GetString("The encoding style '{0}' is not valid for this call. Valid values are '{1}' for SOAP 1.1 encoding or '{2}' for SOAP 1.2 encoding.", new object[] { encodingStyle, "http://schemas.xmlsoap.org/soap/encoding/", "http://www.w3.org/2003/05/soap-encoding" }));
				}
			}
			else if (this.methods[methodKey].isSoap)
			{
				encodingStyle = "http://schemas.xmlsoap.org/soap/encoding/";
			}
			return encodingStyle;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00052339 File Offset: 0x00050539
		internal static FileIOPermission FileIOPermission
		{
			get
			{
				if (TempAssembly.fileIOPermission == null)
				{
					TempAssembly.fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				}
				return TempAssembly.fileIOPermission;
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00052358 File Offset: 0x00050558
		internal object InvokeReader(XmlMapping mapping, XmlReader xmlReader, XmlDeserializationEvents events, string encodingStyle)
		{
			XmlSerializationReader xmlSerializationReader = null;
			object obj;
			try
			{
				encodingStyle = this.ValidateEncodingStyle(encodingStyle, mapping.Key);
				xmlSerializationReader = this.Contract.Reader;
				xmlSerializationReader.Init(xmlReader, events, encodingStyle, this);
				if (this.methods[mapping.Key].readMethod == null)
				{
					if (this.readerMethods == null)
					{
						this.readerMethods = this.Contract.ReadMethods;
					}
					string text = (string)this.readerMethods[mapping.Key];
					if (text == null)
					{
						throw new InvalidOperationException(Res.GetString("Type '{0}' is not serializable.", new object[] { mapping.Accessor.Name }));
					}
					this.methods[mapping.Key].readMethod = TempAssembly.GetMethodFromType(xmlSerializationReader.GetType(), text, this.pregeneratedAssmbly ? this.assembly : null);
				}
				obj = this.methods[mapping.Key].readMethod.Invoke(xmlSerializationReader, TempAssembly.emptyObjectArray);
			}
			catch (SecurityException ex)
			{
				throw new InvalidOperationException(Res.GetString("One or more assemblies referenced by the XmlSerializer cannot be called from partially trusted code."), ex);
			}
			finally
			{
				if (xmlSerializationReader != null)
				{
					xmlSerializationReader.Dispose();
				}
			}
			return obj;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000524B0 File Offset: 0x000506B0
		internal void InvokeWriter(XmlMapping mapping, XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
		{
			XmlSerializationWriter xmlSerializationWriter = null;
			try
			{
				encodingStyle = this.ValidateEncodingStyle(encodingStyle, mapping.Key);
				xmlSerializationWriter = this.Contract.Writer;
				xmlSerializationWriter.Init(xmlWriter, namespaces, encodingStyle, id, this);
				if (this.methods[mapping.Key].writeMethod == null)
				{
					if (this.writerMethods == null)
					{
						this.writerMethods = this.Contract.WriteMethods;
					}
					string text = (string)this.writerMethods[mapping.Key];
					if (text == null)
					{
						throw new InvalidOperationException(Res.GetString("Type '{0}' is not serializable.", new object[] { mapping.Accessor.Name }));
					}
					this.methods[mapping.Key].writeMethod = TempAssembly.GetMethodFromType(xmlSerializationWriter.GetType(), text, this.pregeneratedAssmbly ? this.assembly : null);
				}
				this.methods[mapping.Key].writeMethod.Invoke(xmlSerializationWriter, new object[] { o });
			}
			catch (SecurityException ex)
			{
				throw new InvalidOperationException(Res.GetString("One or more assemblies referenced by the XmlSerializer cannot be called from partially trusted code."), ex);
			}
			finally
			{
				if (xmlSerializationWriter != null)
				{
					xmlSerializationWriter.Dispose();
				}
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00052610 File Offset: 0x00050810
		internal Assembly GetReferencedAssembly(string name)
		{
			if (this.assemblies == null || name == null)
			{
				return null;
			}
			return (Assembly)this.assemblies[name];
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x00052630 File Offset: 0x00050830
		internal bool NeedAssembyResolve
		{
			get
			{
				return this.assemblies != null && this.assemblies.Count > 0;
			}
		}

		// Token: 0x04000807 RID: 2055
		internal const string GeneratedAssemblyNamespace = "Microsoft.Xml.Serialization.GeneratedAssembly";

		// Token: 0x04000808 RID: 2056
		private Assembly assembly;

		// Token: 0x04000809 RID: 2057
		private bool pregeneratedAssmbly;

		// Token: 0x0400080A RID: 2058
		private XmlSerializerImplementation contract;

		// Token: 0x0400080B RID: 2059
		private Hashtable writerMethods;

		// Token: 0x0400080C RID: 2060
		private Hashtable readerMethods;

		// Token: 0x0400080D RID: 2061
		private TempAssembly.TempMethodDictionary methods;

		// Token: 0x0400080E RID: 2062
		private static object[] emptyObjectArray = new object[0];

		// Token: 0x0400080F RID: 2063
		private Hashtable assemblies = new Hashtable();

		// Token: 0x04000810 RID: 2064
		private static volatile FileIOPermission fileIOPermission;

		// Token: 0x02000152 RID: 338
		internal class TempMethod
		{
			// Token: 0x04000811 RID: 2065
			internal MethodInfo writeMethod;

			// Token: 0x04000812 RID: 2066
			internal MethodInfo readMethod;

			// Token: 0x04000813 RID: 2067
			internal string name;

			// Token: 0x04000814 RID: 2068
			internal string ns;

			// Token: 0x04000815 RID: 2069
			internal bool isSoap;

			// Token: 0x04000816 RID: 2070
			internal string methodKey;
		}

		// Token: 0x02000153 RID: 339
		internal sealed class TempMethodDictionary : DictionaryBase
		{
			// Token: 0x170003BA RID: 954
			internal TempAssembly.TempMethod this[string key]
			{
				get
				{
					return (TempAssembly.TempMethod)base.Dictionary[key];
				}
			}

			// Token: 0x060010C2 RID: 4290 RVA: 0x0005266A File Offset: 0x0005086A
			internal void Add(string key, TempAssembly.TempMethod value)
			{
				base.Dictionary.Add(key, value);
			}
		}
	}
}
