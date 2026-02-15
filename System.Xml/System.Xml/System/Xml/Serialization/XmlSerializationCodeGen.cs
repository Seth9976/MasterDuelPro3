using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020001C6 RID: 454
	internal class XmlSerializationCodeGen
	{
		// Token: 0x0600161C RID: 5660 RVA: 0x00070A90 File Offset: 0x0006EC90
		internal XmlSerializationCodeGen(IndentedWriter writer, TypeScope[] scopes, string access, string className)
		{
			this.writer = writer;
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareCodeGen(writer);
			this.className = className;
			this.access = access;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x00070B16 File Offset: 0x0006ED16
		internal IndentedWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00070B1E File Offset: 0x0006ED1E
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x00070B26 File Offset: 0x0006ED26
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

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00070B2F File Offset: 0x0006ED2F
		internal ReflectionAwareCodeGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00070B37 File Offset: 0x0006ED37
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00070B3F File Offset: 0x0006ED3F
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00070B47 File Offset: 0x0006ED47
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00070B4F File Offset: 0x0006ED4F
		internal string Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00070B57 File Offset: 0x0006ED57
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x00070B5F File Offset: 0x0006ED5F
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x00070B67 File Offset: 0x0006ED67
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00070B70 File Offset: 0x0006ED70
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

		// Token: 0x0600162A RID: 5674 RVA: 0x00070BA8 File Offset: 0x0006EDA8
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (!mapping.IsSoap && this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00070C10 File Offset: 0x0006EE10
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

		// Token: 0x0600162C RID: 5676 RVA: 0x00070C45 File Offset: 0x0006EE45
		internal void WriteQuotedCSharpString(string value)
		{
			this.raCodeGen.WriteQuotedCSharpString(value);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00070C54 File Offset: 0x0006EE54
		internal void GenerateHashtableGetBegin(string privateName, string publicName)
		{
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = null;");
			this.writer.Write("public override ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(publicName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.WriteLine("get {");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num + 1;
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.WriteLine(" == null) {");
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num + 1;
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" _tmp = new ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.WriteLine("();");
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00070DCC File Offset: 0x0006EFCC
		internal void GenerateHashtableGetEnd(string privateName)
		{
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.Write(" == null) ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = _tmp;");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num - 1;
			this.writer.WriteLine("}");
			this.writer.Write("return ");
			this.writer.Write(privateName);
			this.writer.WriteLine(";");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00070EBC File Offset: 0x0006F0BC
		internal void GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings)
		{
			this.GenerateHashtableGetBegin(privateName, publicName);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.writer.Write("_tmp[");
						this.WriteQuotedCSharpString(xmlMappings[i].Key);
						this.writer.Write("] = ");
						this.WriteQuotedCSharpString(methods[i]);
						this.writer.WriteLine(";");
					}
				}
			}
			this.GenerateHashtableGetEnd(privateName);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00070F48 File Offset: 0x0006F148
		internal void GenerateSupportedTypes(Type[] types)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanSerialize(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
				{
					hashtable[type] = type;
					this.writer.Write("if (type == typeof(");
					this.writer.Write(CodeIdentifier.GetCSharpName(type));
					this.writer.WriteLine(")) return true;");
				}
			}
			this.writer.WriteLine("return false;");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000710A4 File Offset: 0x0006F2A4
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			this.writer.WriteLine();
			this.writer.Write("public abstract class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(baseSerializer));
			this.writer.Write(" : ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.WriteLine(" CreateReader() {");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num + 1;
			this.writer.Write("return new ");
			this.writer.Write(readerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num - 1;
			this.writer.WriteLine("}");
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.WriteLine(" CreateWriter() {");
			IndentedWriter indentedWriter4 = this.writer;
			num = indentedWriter4.Indent;
			indentedWriter4.Indent = num + 1;
			this.writer.Write("return new ");
			this.writer.Write(writerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter5 = this.writer;
			num = indentedWriter5.Indent;
			indentedWriter5.Indent = num - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter6 = this.writer;
			num = indentedWriter6.Indent;
			indentedWriter6.Indent = num - 1;
			this.writer.WriteLine("}");
			return baseSerializer;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000712A8 File Offset: 0x0006F4A8
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			this.writer.WriteLine();
			this.writer.Write("public sealed class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(text));
			this.writer.Write(" : ");
			this.writer.Write(baseSerializer);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.WriteLine();
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanDeserialize(");
			this.writer.Write(typeof(XmlReader).FullName);
			this.writer.WriteLine(" xmlReader) {");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num + 1;
			if (mapping.Accessor.Any)
			{
				this.writer.WriteLine("return true;");
			}
			else
			{
				this.writer.Write("return xmlReader.IsStartElement(");
				this.WriteQuotedCSharpString(mapping.Accessor.Name);
				this.writer.Write(", ");
				this.WriteQuotedCSharpString(mapping.Accessor.Namespace);
				this.writer.WriteLine(");");
			}
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num - 1;
			this.writer.WriteLine("}");
			if (writeMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override void Serialize(object objectToSerialize, ");
				this.writer.Write(typeof(XmlSerializationWriter).FullName);
				this.writer.WriteLine(" writer) {");
				IndentedWriter indentedWriter4 = this.writer;
				num = indentedWriter4.Indent;
				indentedWriter4.Indent = num + 1;
				this.writer.Write("((");
				this.writer.Write(writerClass);
				this.writer.Write(")writer).");
				this.writer.Write(writeMethod);
				this.writer.Write("(");
				if (mapping is XmlMembersMapping)
				{
					this.writer.Write("(object[])");
				}
				this.writer.WriteLine("objectToSerialize);");
				IndentedWriter indentedWriter5 = this.writer;
				num = indentedWriter5.Indent;
				indentedWriter5.Indent = num - 1;
				this.writer.WriteLine("}");
			}
			if (readMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override object Deserialize(");
				this.writer.Write(typeof(XmlSerializationReader).FullName);
				this.writer.WriteLine(" reader) {");
				IndentedWriter indentedWriter6 = this.writer;
				num = indentedWriter6.Indent;
				indentedWriter6.Indent = num + 1;
				this.writer.Write("return ((");
				this.writer.Write(readerClass);
				this.writer.Write(")reader).");
				this.writer.Write(readMethod);
				this.writer.WriteLine("();");
				IndentedWriter indentedWriter7 = this.writer;
				num = indentedWriter7.Indent;
				indentedWriter7.Indent = num - 1;
				this.writer.WriteLine("}");
			}
			IndentedWriter indentedWriter8 = this.writer;
			num = indentedWriter8.Indent;
			indentedWriter8.Indent = num - 1;
			this.writer.WriteLine("}");
			return text;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0007165C File Offset: 0x0006F85C
		private void GenerateTypedSerializers(Hashtable serializers)
		{
			string text = "typedSerializers";
			this.GenerateHashtableGetBegin(text, "TypedSerializers");
			foreach (object obj in serializers.Keys)
			{
				string text2 = (string)obj;
				this.writer.Write("_tmp.Add(");
				this.WriteQuotedCSharpString(text2);
				this.writer.Write(", new ");
				this.writer.Write((string)serializers[text2]);
				this.writer.WriteLine("());");
			}
			this.GenerateHashtableGetEnd("typedSerializers");
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0007171C File Offset: 0x0006F91C
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.Write(" GetSerializer(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
					{
						this.writer.Write("if (type == typeof(");
						this.writer.Write(CodeIdentifier.GetCSharpName(type));
						this.writer.Write(")) return new ");
						this.writer.Write((string)serializers[xmlMappings[i].Key]);
						this.writer.WriteLine("();");
					}
				}
			}
			this.writer.WriteLine("return null;");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000718B4 File Offset: 0x0006FAB4
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			this.writer.WriteLine();
			this.writer.Write("public class XmlSerializerContract : global::");
			this.writer.Write(typeof(XmlSerializerImplementation).FullName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.Write(" Reader { get { return new ");
			this.writer.Write(readerType);
			this.writer.WriteLine("(); } }");
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.Write(" Writer { get { return new ");
			this.writer.Write(writerType);
			this.writer.WriteLine("(); } }");
			this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings);
			this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings);
			this.GenerateTypedSerializers(serializers);
			this.GenerateSupportedTypes(types);
			this.GenerateGetSerializer(serializers, xmlMappings);
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00071A2C File Offset: 0x0006FC2C
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x040009C6 RID: 2502
		private IndentedWriter writer;

		// Token: 0x040009C7 RID: 2503
		private int nextMethodNumber;

		// Token: 0x040009C8 RID: 2504
		private Hashtable methodNames = new Hashtable();

		// Token: 0x040009C9 RID: 2505
		private ReflectionAwareCodeGen raCodeGen;

		// Token: 0x040009CA RID: 2506
		private TypeScope[] scopes;

		// Token: 0x040009CB RID: 2507
		private TypeDesc stringTypeDesc;

		// Token: 0x040009CC RID: 2508
		private TypeDesc qnameTypeDesc;

		// Token: 0x040009CD RID: 2509
		private string access;

		// Token: 0x040009CE RID: 2510
		private string className;

		// Token: 0x040009CF RID: 2511
		private TypeMapping[] referencedMethods;

		// Token: 0x040009D0 RID: 2512
		private int references;

		// Token: 0x040009D1 RID: 2513
		private Hashtable generatedMethods = new Hashtable();
	}
}
