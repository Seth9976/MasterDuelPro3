using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Threading;

namespace System.Xml.Serialization
{
	/// <summary>Serializes and deserializes objects into and from XML documents. The <see cref="T:System.Xml.Serialization.XmlSerializer" /> enables you to control how objects are encoded into XML.</summary>
	// Token: 0x020001DF RID: 479
	public class XmlSerializer
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x00094BE8 File Offset: 0x00092DE8
		private static XmlSerializerNamespaces DefaultNamespaces
		{
			get
			{
				if (XmlSerializer.defaultNamespaces == null)
				{
					XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
					xmlSerializerNamespaces.AddInternal("xsi", "http://www.w3.org/2001/XMLSchema-instance");
					xmlSerializerNamespaces.AddInternal("xsd", "http://www.w3.org/2001/XMLSchema");
					if (XmlSerializer.defaultNamespaces == null)
					{
						XmlSerializer.defaultNamespaces = xmlSerializerNamespaces;
					}
				}
				return XmlSerializer.defaultNamespaces;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</summary>
		// Token: 0x060018B8 RID: 6328 RVA: 0x00002127 File Offset: 0x00000327
		protected XmlSerializer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of type <see cref="T:System.Object" /> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object" />. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter. </param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize. </param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties. </param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document. </param>
		// Token: 0x060018B9 RID: 6329 RVA: 0x00094C3C File Offset: 0x00092E3C
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace)
			: this(type, overrides, extraTypes, root, defaultNamespace, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize an XML document into object of the specified type. It also specifies the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that represents the XML root element. </param>
		// Token: 0x060018BA RID: 6330 RVA: 0x00094C4C File Offset: 0x00092E4C
		public XmlSerializer(Type type, XmlRootAttribute root)
			: this(type, null, new Type[0], root, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into object of a specified type. If a property or field returns an array, the <paramref name="extraTypes" /> parameter specifies objects that can be inserted into the array.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize. </param>
		// Token: 0x060018BB RID: 6331 RVA: 0x00094C5F File Offset: 0x00092E5F
		public XmlSerializer(Type type, Type[] extraTypes)
			: this(type, null, extraTypes, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes.</summary>
		/// <param name="type">The type of the object to serialize. </param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" />. </param>
		// Token: 0x060018BC RID: 6332 RVA: 0x00094C6D File Offset: 0x00092E6D
		public XmlSerializer(Type type, XmlAttributeOverrides overrides)
			: this(type, overrides, new Type[0], null, null, null)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class using an object that maps one type to another.</summary>
		/// <param name="xmlTypeMapping">An <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> that maps one type to another. </param>
		// Token: 0x060018BD RID: 6333 RVA: 0x00094C80 File Offset: 0x00092E80
		public XmlSerializer(XmlTypeMapping xmlTypeMapping)
		{
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping);
			this.mapping = xmlTypeMapping;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		// Token: 0x060018BE RID: 6334 RVA: 0x00094C9B File Offset: 0x00092E9B
		public XmlSerializer(Type type)
			: this(type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Specifies the default namespace for all the XML elements.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="defaultNamespace">The default namespace to use for all the XML elements. </param>
		// Token: 0x060018BF RID: 6335 RVA: 0x00094CA8 File Offset: 0x00092EA8
		public XmlSerializer(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.mapping = XmlSerializer.GetKnownMapping(type, defaultNamespace);
			if (this.mapping != null)
			{
				this.primitiveType = type;
				return;
			}
			this.tempAssembly = XmlSerializer.cache[defaultNamespace, type];
			if (this.tempAssembly == null)
			{
				TempAssemblyCache tempAssemblyCache = XmlSerializer.cache;
				lock (tempAssemblyCache)
				{
					this.tempAssembly = XmlSerializer.cache[defaultNamespace, type];
					if (this.tempAssembly == null)
					{
						XmlSerializerImplementation xmlSerializerImplementation;
						Assembly assembly = TempAssembly.LoadGeneratedAssembly(type, defaultNamespace, out xmlSerializerImplementation);
						if (assembly == null)
						{
							XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(defaultNamespace);
							this.mapping = xmlReflectionImporter.ImportTypeMapping(type, null, defaultNamespace);
							this.tempAssembly = XmlSerializer.GenerateTempAssembly(this.mapping, type, defaultNamespace);
						}
						else
						{
							this.mapping = XmlReflectionImporter.GetTopLevelMapping(type, defaultNamespace);
							this.tempAssembly = new TempAssembly(new XmlMapping[] { this.mapping }, assembly, xmlSerializerImplementation);
						}
					}
					XmlSerializer.cache.Add(defaultNamespace, type, this.tempAssembly);
				}
			}
			if (this.mapping == null)
			{
				this.mapping = XmlReflectionImporter.GetTopLevelMapping(type, defaultNamespace);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of type <see cref="T:System.Object" /> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object" />. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <param name="location">The location of the types.</param>
		// Token: 0x060018C0 RID: 6336 RVA: 0x00094DE0 File Offset: 0x00092FE0
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location)
			: this(type, overrides, extraTypes, root, defaultNamespace, location, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML document instances, and deserialize XML document instances into objects of the specified type. This overload allows you to supply other types that can be encountered during a serialization or deserialization operation, as well as a default namespace for all XML elements, the class to use as the XML root element, its location, and credentials required for access.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <param name="location">The location of the types.</param>
		/// <param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence" /> class that contains credentials required to access types.</param>
		// Token: 0x060018C1 RID: 6337 RVA: 0x00094DF4 File Offset: 0x00092FF4
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use a XmlSerializer constructor overload which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(overrides, defaultNamespace);
			if (extraTypes != null)
			{
				for (int i = 0; i < extraTypes.Length; i++)
				{
					xmlReflectionImporter.IncludeType(extraTypes[i]);
				}
			}
			this.mapping = xmlReflectionImporter.ImportTypeMapping(type, root, defaultNamespace);
			if (location != null || evidence != null)
			{
				this.DemandForUserLocationOrEvidence();
			}
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(this.mapping, type, defaultNamespace, location, evidence);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0000A558 File Offset: 0x00008758
		private void DemandForUserLocationOrEvidence()
		{
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00094E75 File Offset: 0x00093075
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping)
		{
			return XmlSerializer.GenerateTempAssembly(xmlMapping, null, null);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00094E7F File Offset: 0x0009307F
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping, Type type, string defaultNamespace)
		{
			if (xmlMapping == null)
			{
				throw new ArgumentNullException("xmlMapping");
			}
			return new TempAssembly(new XmlMapping[] { xmlMapping }, new Type[] { type }, defaultNamespace, null, null);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00094EAB File Offset: 0x000930AB
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping, Type type, string defaultNamespace, string location, Evidence evidence)
		{
			return new TempAssembly(new XmlMapping[] { xmlMapping }, new Type[] { type }, defaultNamespace, location, evidence);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		// Token: 0x060018C6 RID: 6342 RVA: 0x00094ECA File Offset: 0x000930CA
		public void Serialize(TextWriter textWriter, object o)
		{
			this.Serialize(textWriter, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter" /> and references the specified namespaces.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> that contains namespaces for the generated XML document. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018C7 RID: 6343 RVA: 0x00094ED8 File Offset: 0x000930D8
		public void Serialize(TextWriter textWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(textWriter)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018C8 RID: 6344 RVA: 0x00094F03 File Offset: 0x00093103
		public void Serialize(Stream stream, object o)
		{
			this.Serialize(stream, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream" />that references the specified namespaces.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018C9 RID: 6345 RVA: 0x00094F10 File Offset: 0x00093110
		public void Serialize(Stream stream, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018CA RID: 6346 RVA: 0x00094F3C File Offset: 0x0009313C
		public void Serialize(XmlWriter xmlWriter, object o)
		{
			this.Serialize(xmlWriter, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" /> and references the specified namespaces.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018CB RID: 6347 RVA: 0x00094F47 File Offset: 0x00093147
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(xmlWriter, o, namespaces, null);
		}

		/// <summary>Serializes the specified object and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" /> and references the specified namespaces and encoding style.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The object to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <param name="encodingStyle">The encoding style of the serialized XML. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018CC RID: 6348 RVA: 0x00094F53 File Offset: 0x00093153
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle)
		{
			this.Serialize(xmlWriter, o, namespaces, encodingStyle, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" />, XML namespaces, and encoding. </summary>
		/// <param name="xmlWriter">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML document.</param>
		/// <param name="o">The object to serialize.</param>
		/// <param name="namespaces">An instance of the XmlSerializaerNamespaces that contains namespaces and prefixes to use.</param>
		/// <param name="encodingStyle">The encoding used in the document.</param>
		/// <param name="id">For SOAP encoded messages, the base used to generate id attributes. </param>
		// Token: 0x060018CD RID: 6349 RVA: 0x00094F64 File Offset: 0x00093164
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
		{
			try
			{
				if (this.primitiveType != null)
				{
					if (encodingStyle != null && encodingStyle.Length > 0)
					{
						throw new InvalidOperationException(Res.GetString("The encoding style '{0}' is not valid for this call because this XmlSerializer instance does not support encoding. Use the SoapReflectionImporter to initialize an XmlSerializer that supports encoding.", new object[] { encodingStyle }));
					}
					this.SerializePrimitive(xmlWriter, o, namespaces);
				}
				else
				{
					if (this.tempAssembly == null || this.typedSerializer)
					{
						XmlSerializationWriter xmlSerializationWriter = this.CreateWriter();
						xmlSerializationWriter.Init(xmlWriter, (namespaces == null || namespaces.Count == 0) ? XmlSerializer.DefaultNamespaces : namespaces, encodingStyle, id, this.tempAssembly);
						try
						{
							this.Serialize(o, xmlSerializationWriter);
							goto IL_00B8;
						}
						finally
						{
							xmlSerializationWriter.Dispose();
						}
					}
					this.tempAssembly.InvokeWriter(this.mapping, xmlWriter, o, (namespaces == null || namespaces.Count == 0) ? XmlSerializer.DefaultNamespaces : namespaces, encodingStyle, id);
				}
				IL_00B8:;
			}
			catch (Exception innerException)
			{
				if (innerException is ThreadAbortException || innerException is StackOverflowException || innerException is OutOfMemoryException)
				{
					throw;
				}
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				throw new InvalidOperationException(Res.GetString("There was an error generating the XML document."), innerException);
			}
			xmlWriter.Flush();
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> that contains the XML document to deserialize. </param>
		// Token: 0x060018CE RID: 6350 RVA: 0x00095088 File Offset: 0x00093288
		public object Deserialize(Stream stream)
		{
			return this.Deserialize(new XmlTextReader(stream)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="textReader">The <see cref="T:System.IO.TextReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018CF RID: 6351 RVA: 0x000950BC File Offset: 0x000932BC
		public object Deserialize(TextReader textReader)
		{
			return this.Deserialize(new XmlTextReader(textReader)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="xmlReader">The <see cref="T:System.xml.XmlReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018D0 RID: 6352 RVA: 0x000950ED File Offset: 0x000932ED
		public object Deserialize(XmlReader xmlReader)
		{
			return this.Deserialize(xmlReader, null);
		}

		/// <summary>Deserializes an XML document contained by the specified <see cref="T:System.Xml.XmlReader" /> and allows the overriding of events that occur during deserialization.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> that contains the document to deserialize.</param>
		/// <param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents" /> class. </param>
		// Token: 0x060018D1 RID: 6353 RVA: 0x000950F7 File Offset: 0x000932F7
		public object Deserialize(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			return this.Deserialize(xmlReader, null, events);
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader" /> and encoding style.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="xmlReader">The <see cref="T:System.xml.XmlReader" /> that contains the XML document to deserialize. </param>
		/// <param name="encodingStyle">The encoding style of the serialized XML. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x060018D2 RID: 6354 RVA: 0x00095102 File Offset: 0x00093302
		public object Deserialize(XmlReader xmlReader, string encodingStyle)
		{
			return this.Deserialize(xmlReader, encodingStyle, this.events);
		}

		/// <summary>Deserializes the object using the data contained by the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The object being deserialized.</returns>
		/// <param name="xmlReader">An instance of the <see cref="T:System.Xml.XmlReader" /> class used to read the document.</param>
		/// <param name="encodingStyle">The encoding used.</param>
		/// <param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents" /> class. </param>
		// Token: 0x060018D3 RID: 6355 RVA: 0x00095114 File Offset: 0x00093314
		public object Deserialize(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events)
		{
			events.sender = this;
			object obj;
			try
			{
				if (this.primitiveType != null)
				{
					if (encodingStyle != null && encodingStyle.Length > 0)
					{
						throw new InvalidOperationException(Res.GetString("The encoding style '{0}' is not valid for this call because this XmlSerializer instance does not support encoding. Use the SoapReflectionImporter to initialize an XmlSerializer that supports encoding.", new object[] { encodingStyle }));
					}
					obj = this.DeserializePrimitive(xmlReader, events);
				}
				else
				{
					if (this.tempAssembly == null || this.typedSerializer)
					{
						XmlSerializationReader xmlSerializationReader = this.CreateReader();
						xmlSerializationReader.Init(xmlReader, events, encodingStyle, this.tempAssembly);
						try
						{
							return this.Deserialize(xmlSerializationReader);
						}
						finally
						{
							xmlSerializationReader.Dispose();
						}
					}
					obj = this.tempAssembly.InvokeReader(this.mapping, xmlReader, events, encodingStyle);
				}
			}
			catch (Exception innerException)
			{
				if (innerException is ThreadAbortException || innerException is StackOverflowException || innerException is OutOfMemoryException)
				{
					throw;
				}
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				if (xmlReader is IXmlLineInfo)
				{
					IXmlLineInfo xmlLineInfo = (IXmlLineInfo)xmlReader;
					throw new InvalidOperationException(Res.GetString("There is an error in XML document ({0}, {1}).", new object[]
					{
						xmlLineInfo.LineNumber.ToString(CultureInfo.InvariantCulture),
						xmlLineInfo.LinePosition.ToString(CultureInfo.InvariantCulture)
					}), innerException);
				}
				throw new InvalidOperationException(Res.GetString("There is an error in the XML document."), innerException);
			}
			return obj;
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can deserialize a specified XML document.</summary>
		/// <returns>true if this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can deserialize the object that the <see cref="T:System.Xml.XmlReader" /> points to; otherwise, false.</returns>
		/// <param name="xmlReader">An <see cref="T:System.Xml.XmlReader" /> that points to the document to deserialize. </param>
		// Token: 0x060018D4 RID: 6356 RVA: 0x0009526C File Offset: 0x0009346C
		public virtual bool CanDeserialize(XmlReader xmlReader)
		{
			if (this.primitiveType != null)
			{
				TypeDesc typeDesc = (TypeDesc)TypeScope.PrimtiveTypes[this.primitiveType];
				return xmlReader.IsStartElement(typeDesc.DataType.Name, string.Empty);
			}
			return this.tempAssembly != null && this.tempAssembly.CanRead(this.mapping, xmlReader);
		}

		/// <summary>Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects created from an array of <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> objects.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> that maps one type to another. </param>
		// Token: 0x060018D5 RID: 6357 RVA: 0x000952D0 File Offset: 0x000934D0
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings)
		{
			return XmlSerializer.FromMappings(mappings, null);
		}

		/// <summary>Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class from the specified mappings.</summary>
		/// <returns>An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the deserialized object.</param>
		// Token: 0x060018D6 RID: 6358 RVA: 0x000952DC File Offset: 0x000934DC
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Type type)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return new XmlSerializer[0];
			}
			XmlSerializerImplementation xmlSerializerImplementation = null;
			if (!(((type == null) ? null : TempAssembly.LoadGeneratedAssembly(type, null, out xmlSerializerImplementation)) == null))
			{
				XmlSerializer[] array = new XmlSerializer[mappings.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (XmlSerializer)xmlSerializerImplementation.TypedSerializers[mappings[i].Key];
				}
				return array;
			}
			if (XmlMapping.IsShallow(mappings))
			{
				return new XmlSerializer[0];
			}
			if (type == null)
			{
				TempAssembly tempAssembly = new TempAssembly(mappings, new Type[] { type }, null, null, null);
				XmlSerializer[] array2 = new XmlSerializer[mappings.Length];
				xmlSerializerImplementation = tempAssembly.Contract;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = (XmlSerializer)xmlSerializerImplementation.TypedSerializers[mappings[j].Key];
					array2[j].SetTempAssembly(tempAssembly, mappings[j]);
				}
				return array2;
			}
			return XmlSerializer.GetSerializersFromCache(mappings, type);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000953D0 File Offset: 0x000935D0
		private static XmlSerializer[] GetSerializersFromCache(XmlMapping[] mappings, Type type)
		{
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			Hashtable hashtable = null;
			Hashtable hashtable2 = XmlSerializer.xmlSerializerTable;
			lock (hashtable2)
			{
				hashtable = XmlSerializer.xmlSerializerTable[type] as Hashtable;
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					XmlSerializer.xmlSerializerTable[type] = hashtable;
				}
			}
			hashtable2 = hashtable;
			lock (hashtable2)
			{
				Hashtable hashtable3 = new Hashtable();
				for (int i = 0; i < mappings.Length; i++)
				{
					XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey = new XmlSerializer.XmlSerializerMappingKey(mappings[i]);
					array[i] = hashtable[xmlSerializerMappingKey] as XmlSerializer;
					if (array[i] == null)
					{
						hashtable3.Add(xmlSerializerMappingKey, i);
					}
				}
				if (hashtable3.Count > 0)
				{
					XmlMapping[] array2 = new XmlMapping[hashtable3.Count];
					int num = 0;
					foreach (object obj in hashtable3.Keys)
					{
						XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey2 = (XmlSerializer.XmlSerializerMappingKey)obj;
						array2[num++] = xmlSerializerMappingKey2.Mapping;
					}
					TempAssembly tempAssembly = new TempAssembly(array2, new Type[] { type }, null, null, null);
					XmlSerializerImplementation contract = tempAssembly.Contract;
					foreach (object obj2 in hashtable3.Keys)
					{
						XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey3 = (XmlSerializer.XmlSerializerMappingKey)obj2;
						num = (int)hashtable3[xmlSerializerMappingKey3];
						array[num] = (XmlSerializer)contract.TypedSerializers[xmlSerializerMappingKey3.Mapping.Key];
						array[num].SetTempAssembly(tempAssembly, xmlSerializerMappingKey3.Mapping);
						hashtable[xmlSerializerMappingKey3] = array[num];
					}
				}
			}
			return array;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class created from mappings of one XML type to another.</summary>
		/// <returns>An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects used to map one type to another.</param>
		/// <param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence" /> class that contains host and assembly data presented to the common language runtime policy system.</param>
		// Token: 0x060018D8 RID: 6360 RVA: 0x0009560C File Offset: 0x0009380C
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of FromMappings which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Evidence evidence)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return new XmlSerializer[0];
			}
			if (XmlMapping.IsShallow(mappings))
			{
				return new XmlSerializer[0];
			}
			XmlSerializerImplementation contract = new TempAssembly(mappings, new Type[0], null, null, evidence).Contract;
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (XmlSerializer)contract.TypedSerializers[mappings[i].Key];
			}
			return array;
		}

		/// <summary>Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that contains serializers for the supplied types and mappings.</returns>
		/// <param name="types">A collection of types.</param>
		/// <param name="mappings">A collection of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects used to convert one type to another.</param>
		// Token: 0x060018D9 RID: 6361 RVA: 0x00095680 File Offset: 0x00093880
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings)
		{
			return XmlSerializer.GenerateSerializer(types, mappings, new CompilerParameters
			{
				TempFiles = new TempFileCollection(),
				GenerateInMemory = false,
				IncludeDebugInformation = false
			});
		}

		/// <summary>Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings and compiler settings and options. </summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that contains special versions of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="types">An array of type <see cref="T:System.Type" /> that contains objects used to serialize and deserialize data.</param>
		/// <param name="mappings">An array of type <see cref="T:System.Xml.Serialization.XmlMapping" /> that maps the XML data to the type data.</param>
		/// <param name="parameters">An instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class that represents the parameters used to invoke a compiler.</param>
		// Token: 0x060018DA RID: 6362 RVA: 0x000956B4 File Offset: 0x000938B4
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings, CompilerParameters parameters)
		{
			if (types == null || types.Length == 0)
			{
				return null;
			}
			if (mappings == null)
			{
				throw new ArgumentNullException("mappings");
			}
			if (XmlMapping.IsShallow(mappings))
			{
				throw new InvalidOperationException(Res.GetString("This mapping was not crated by reflection importer and cannot be used in this context."));
			}
			Assembly assembly = null;
			foreach (Type type in types)
			{
				if (DynamicAssemblies.IsTypeDynamic(type))
				{
					throw new InvalidOperationException(Res.GetString("Cannot pre-generate serialization assembly for type '{0}'. Pre-generation of serialization assemblies is not supported for dynamic types. Save the assembly and load it from disk to use it with XmlSerialization.", new object[] { type.FullName }));
				}
				if (assembly == null)
				{
					assembly = type.Assembly;
				}
				else if (type.Assembly != assembly)
				{
					throw new ArgumentException(Res.GetString("Cannot pre-generate serializer for multiple assemblies. Type '{0}' does not belong to assembly {1}.", new object[] { type.FullName, assembly.Location }), "types");
				}
			}
			return TempAssembly.GenerateAssembly(mappings, types, null, null, XmlSerializerCompilerParameters.Create(parameters, true), assembly, new Hashtable());
		}

		/// <summary>Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects created from an array of types.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects.</returns>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects. </param>
		// Token: 0x060018DB RID: 6363 RVA: 0x00095794 File Offset: 0x00093994
		public static XmlSerializer[] FromTypes(Type[] types)
		{
			if (types == null)
			{
				return new XmlSerializer[0];
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter();
			XmlTypeMapping[] array = new XmlTypeMapping[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = xmlReflectionImporter.ImportTypeMapping(types[i]);
			}
			XmlMapping[] array2 = array;
			return XmlSerializer.FromMappings(array2);
		}

		/// <summary>Returns the name of the assembly that contains one or more versions of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> especially created to serialize or deserialize the specified type.</summary>
		/// <returns>The name of the assembly that contains an <see cref="T:System.Xml.Serialization.XmlSerializer" /> for the type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> you are deserializing.</param>
		// Token: 0x060018DC RID: 6364 RVA: 0x000957DC File Offset: 0x000939DC
		public static string GetXmlSerializerAssemblyName(Type type)
		{
			return XmlSerializer.GetXmlSerializerAssemblyName(type, null);
		}

		/// <summary>Returns the name of the assembly that contains the serializer for the specified type in the specified namespace.</summary>
		/// <returns>The name of the assembly that contains specially built serializers.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> you are interested in.</param>
		/// <param name="defaultNamespace">The namespace of the type.</param>
		// Token: 0x060018DD RID: 6365 RVA: 0x000957E5 File Offset: 0x000939E5
		public static string GetXmlSerializerAssemblyName(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return Compiler.GetTempAssemblyName(type.Assembly.GetName(), defaultNamespace);
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML node of unknown type during deserialization.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060018DE RID: 6366 RVA: 0x0009580C File Offset: 0x00093A0C
		// (remove) Token: 0x060018DF RID: 6367 RVA: 0x0009582A File Offset: 0x00093A2A
		public event XmlNodeEventHandler UnknownNode
		{
			add
			{
				this.events.OnUnknownNode = (XmlNodeEventHandler)Delegate.Combine(this.events.OnUnknownNode, value);
			}
			remove
			{
				this.events.OnUnknownNode = (XmlNodeEventHandler)Delegate.Remove(this.events.OnUnknownNode, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML attribute of unknown type during deserialization.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060018E0 RID: 6368 RVA: 0x00095848 File Offset: 0x00093A48
		// (remove) Token: 0x060018E1 RID: 6369 RVA: 0x00095866 File Offset: 0x00093A66
		public event XmlAttributeEventHandler UnknownAttribute
		{
			add
			{
				this.events.OnUnknownAttribute = (XmlAttributeEventHandler)Delegate.Combine(this.events.OnUnknownAttribute, value);
			}
			remove
			{
				this.events.OnUnknownAttribute = (XmlAttributeEventHandler)Delegate.Remove(this.events.OnUnknownAttribute, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML element of unknown type during deserialization.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060018E2 RID: 6370 RVA: 0x00095884 File Offset: 0x00093A84
		// (remove) Token: 0x060018E3 RID: 6371 RVA: 0x000958A2 File Offset: 0x00093AA2
		public event XmlElementEventHandler UnknownElement
		{
			add
			{
				this.events.OnUnknownElement = (XmlElementEventHandler)Delegate.Combine(this.events.OnUnknownElement, value);
			}
			remove
			{
				this.events.OnUnknownElement = (XmlElementEventHandler)Delegate.Remove(this.events.OnUnknownElement, value);
			}
		}

		/// <summary>Occurs during deserialization of a SOAP-encoded XML stream, when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters a recognized type that is not used or is unreferenced.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060018E4 RID: 6372 RVA: 0x000958C0 File Offset: 0x00093AC0
		// (remove) Token: 0x060018E5 RID: 6373 RVA: 0x000958DE File Offset: 0x00093ADE
		public event UnreferencedObjectEventHandler UnreferencedObject
		{
			add
			{
				this.events.OnUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Combine(this.events.OnUnreferencedObject, value);
			}
			remove
			{
				this.events.OnUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Remove(this.events.OnUnreferencedObject, value);
			}
		}

		/// <summary>Returns an object used to read the XML document to be serialized.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> used to read the XML document.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x060018E6 RID: 6374 RVA: 0x00002CE0 File Offset: 0x00000EE0
		protected virtual XmlSerializationReader CreateReader()
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.Xml.Serialization.XmlSerializationReader" />.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x060018E7 RID: 6375 RVA: 0x00002CE0 File Offset: 0x00000EE0
		protected virtual object Deserialize(XmlSerializationReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns a writer used to serialize the object.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> class.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x060018E8 RID: 6376 RVA: 0x00002CE0 File Offset: 0x00000EE0
		protected virtual XmlSerializationWriter CreateWriter()
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.Serialization.XmlSerializationWriter" />.</summary>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="writer">The <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> used to write the XML document. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x060018E9 RID: 6377 RVA: 0x00002CE0 File Offset: 0x00000EE0
		protected virtual void Serialize(object o, XmlSerializationWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000958FC File Offset: 0x00093AFC
		internal void SetTempAssembly(TempAssembly tempAssembly, XmlMapping mapping)
		{
			this.tempAssembly = tempAssembly;
			this.mapping = mapping;
			this.typedSerializer = true;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00095914 File Offset: 0x00093B14
		private static XmlTypeMapping GetKnownMapping(Type type, string ns)
		{
			if (ns != null && ns != string.Empty)
			{
				return null;
			}
			TypeDesc typeDesc = (TypeDesc)TypeScope.PrimtiveTypes[type];
			if (typeDesc == null)
			{
				return null;
			}
			XmlTypeMapping xmlTypeMapping = new XmlTypeMapping(null, new ElementAccessor
			{
				Name = typeDesc.DataType.Name
			});
			xmlTypeMapping.SetKeyInternal(XmlMapping.GenerateKey(type, null, null));
			return xmlTypeMapping;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00095978 File Offset: 0x00093B78
		private void SerializePrimitive(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
		{
			XmlSerializationPrimitiveWriter xmlSerializationPrimitiveWriter = new XmlSerializationPrimitiveWriter();
			xmlSerializationPrimitiveWriter.Init(xmlWriter, namespaces, null, null, null);
			switch (Type.GetTypeCode(this.primitiveType))
			{
			case TypeCode.Boolean:
				xmlSerializationPrimitiveWriter.Write_boolean(o);
				return;
			case TypeCode.Char:
				xmlSerializationPrimitiveWriter.Write_char(o);
				return;
			case TypeCode.SByte:
				xmlSerializationPrimitiveWriter.Write_byte(o);
				return;
			case TypeCode.Byte:
				xmlSerializationPrimitiveWriter.Write_unsignedByte(o);
				return;
			case TypeCode.Int16:
				xmlSerializationPrimitiveWriter.Write_short(o);
				return;
			case TypeCode.UInt16:
				xmlSerializationPrimitiveWriter.Write_unsignedShort(o);
				return;
			case TypeCode.Int32:
				xmlSerializationPrimitiveWriter.Write_int(o);
				return;
			case TypeCode.UInt32:
				xmlSerializationPrimitiveWriter.Write_unsignedInt(o);
				return;
			case TypeCode.Int64:
				xmlSerializationPrimitiveWriter.Write_long(o);
				return;
			case TypeCode.UInt64:
				xmlSerializationPrimitiveWriter.Write_unsignedLong(o);
				return;
			case TypeCode.Single:
				xmlSerializationPrimitiveWriter.Write_float(o);
				return;
			case TypeCode.Double:
				xmlSerializationPrimitiveWriter.Write_double(o);
				return;
			case TypeCode.Decimal:
				xmlSerializationPrimitiveWriter.Write_decimal(o);
				return;
			case TypeCode.DateTime:
				xmlSerializationPrimitiveWriter.Write_dateTime(o);
				return;
			case TypeCode.String:
				xmlSerializationPrimitiveWriter.Write_string(o);
				return;
			}
			if (this.primitiveType == typeof(XmlQualifiedName))
			{
				xmlSerializationPrimitiveWriter.Write_QName(o);
				return;
			}
			if (this.primitiveType == typeof(byte[]))
			{
				xmlSerializationPrimitiveWriter.Write_base64Binary(o);
				return;
			}
			if (this.primitiveType == typeof(Guid))
			{
				xmlSerializationPrimitiveWriter.Write_guid(o);
				return;
			}
			if (this.primitiveType == typeof(TimeSpan))
			{
				xmlSerializationPrimitiveWriter.Write_TimeSpan(o);
				return;
			}
			throw new InvalidOperationException(Res.GetString("The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.", new object[] { this.primitiveType.FullName }));
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00095B04 File Offset: 0x00093D04
		private object DeserializePrimitive(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			XmlSerializationPrimitiveReader xmlSerializationPrimitiveReader = new XmlSerializationPrimitiveReader();
			xmlSerializationPrimitiveReader.Init(xmlReader, events, null, null);
			switch (Type.GetTypeCode(this.primitiveType))
			{
			case TypeCode.Boolean:
				return xmlSerializationPrimitiveReader.Read_boolean();
			case TypeCode.Char:
				return xmlSerializationPrimitiveReader.Read_char();
			case TypeCode.SByte:
				return xmlSerializationPrimitiveReader.Read_byte();
			case TypeCode.Byte:
				return xmlSerializationPrimitiveReader.Read_unsignedByte();
			case TypeCode.Int16:
				return xmlSerializationPrimitiveReader.Read_short();
			case TypeCode.UInt16:
				return xmlSerializationPrimitiveReader.Read_unsignedShort();
			case TypeCode.Int32:
				return xmlSerializationPrimitiveReader.Read_int();
			case TypeCode.UInt32:
				return xmlSerializationPrimitiveReader.Read_unsignedInt();
			case TypeCode.Int64:
				return xmlSerializationPrimitiveReader.Read_long();
			case TypeCode.UInt64:
				return xmlSerializationPrimitiveReader.Read_unsignedLong();
			case TypeCode.Single:
				return xmlSerializationPrimitiveReader.Read_float();
			case TypeCode.Double:
				return xmlSerializationPrimitiveReader.Read_double();
			case TypeCode.Decimal:
				return xmlSerializationPrimitiveReader.Read_decimal();
			case TypeCode.DateTime:
				return xmlSerializationPrimitiveReader.Read_dateTime();
			case TypeCode.String:
				return xmlSerializationPrimitiveReader.Read_string();
			}
			object obj;
			if (this.primitiveType == typeof(XmlQualifiedName))
			{
				obj = xmlSerializationPrimitiveReader.Read_QName();
			}
			else if (this.primitiveType == typeof(byte[]))
			{
				obj = xmlSerializationPrimitiveReader.Read_base64Binary();
			}
			else if (this.primitiveType == typeof(Guid))
			{
				obj = xmlSerializationPrimitiveReader.Read_guid();
			}
			else
			{
				if (!(this.primitiveType == typeof(TimeSpan)) || !LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					throw new InvalidOperationException(Res.GetString("The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.", new object[] { this.primitiveType.FullName }));
				}
				obj = xmlSerializationPrimitiveReader.Read_TimeSpan();
			}
			return obj;
		}

		// Token: 0x04000A92 RID: 2706
		private TempAssembly tempAssembly;

		// Token: 0x04000A93 RID: 2707
		private bool typedSerializer;

		// Token: 0x04000A94 RID: 2708
		private Type primitiveType;

		// Token: 0x04000A95 RID: 2709
		private XmlMapping mapping;

		// Token: 0x04000A96 RID: 2710
		private XmlDeserializationEvents events;

		// Token: 0x04000A97 RID: 2711
		private static TempAssemblyCache cache = new TempAssemblyCache();

		// Token: 0x04000A98 RID: 2712
		private static volatile XmlSerializerNamespaces defaultNamespaces;

		// Token: 0x04000A99 RID: 2713
		private static Hashtable xmlSerializerTable = new Hashtable();

		// Token: 0x020001E0 RID: 480
		private class XmlSerializerMappingKey
		{
			// Token: 0x060018EF RID: 6383 RVA: 0x00095CF3 File Offset: 0x00093EF3
			public XmlSerializerMappingKey(XmlMapping mapping)
			{
				this.Mapping = mapping;
			}

			// Token: 0x060018F0 RID: 6384 RVA: 0x00095D04 File Offset: 0x00093F04
			public override bool Equals(object obj)
			{
				XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey = obj as XmlSerializer.XmlSerializerMappingKey;
				return xmlSerializerMappingKey != null && !(this.Mapping.Key != xmlSerializerMappingKey.Mapping.Key) && !(this.Mapping.ElementName != xmlSerializerMappingKey.Mapping.ElementName) && !(this.Mapping.Namespace != xmlSerializerMappingKey.Mapping.Namespace) && this.Mapping.IsSoap == xmlSerializerMappingKey.Mapping.IsSoap;
			}

			// Token: 0x060018F1 RID: 6385 RVA: 0x00095D98 File Offset: 0x00093F98
			public override int GetHashCode()
			{
				int num = (this.Mapping.IsSoap ? 0 : 1);
				if (this.Mapping.Key != null)
				{
					num ^= this.Mapping.Key.GetHashCode();
				}
				if (this.Mapping.ElementName != null)
				{
					num ^= this.Mapping.ElementName.GetHashCode();
				}
				if (this.Mapping.Namespace != null)
				{
					num ^= this.Mapping.Namespace.GetHashCode();
				}
				return num;
			}

			// Token: 0x04000A9A RID: 2714
			public XmlMapping Mapping;
		}
	}
}
