using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, noncached, forward-only access to XML data.</summary>
	// Token: 0x02000068 RID: 104
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XmlReader : IDisposable
	{
		/// <summary>Gets the <see cref="T:System.Xml.XmlReaderSettings" /> object used to create this <see cref="T:System.Xml.XmlReader" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlReaderSettings" /> object used to create this reader instance. If this reader was not created using the <see cref="Overload:System.Xml.XmlReader.Create" /> method, this property returns null.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlReaderSettings Settings
		{
			get
			{
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600044C RID: 1100
		public abstract XmlNodeType NodeType { get; }

		/// <summary>When overridden in a derived class, gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00015409 File Offset: 0x00013609
		public virtual string Name
		{
			get
			{
				if (this.Prefix.Length == 0)
				{
					return this.LocalName;
				}
				return this.NameTable.Add(this.Prefix + ":" + this.LocalName);
			}
		}

		/// <summary>When overridden in a derived class, gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600044E RID: 1102
		public abstract string LocalName { get; }

		/// <summary>When overridden in a derived class, gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600044F RID: 1103
		public abstract string NamespaceURI { get; }

		/// <summary>When overridden in a derived class, gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000450 RID: 1104
		public abstract string Prefix { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlReader.Value" />.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false. If false, the node has a value of String.Empty.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00015440 File Offset: 0x00013640
		public virtual bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.NodeType);
			}
		}

		/// <summary>When overridden in a derived class, gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space between markup in a mixed content model. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000452 RID: 1106
		public abstract string Value { get; }

		/// <summary>When overridden in a derived class, gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000453 RID: 1107
		public abstract int Depth { get; }

		/// <summary>When overridden in a derived class, gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000454 RID: 1108
		public abstract string BaseURI { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlReader.NodeType" /> equals XmlNodeType.Element) that ends with /&gt;; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000455 RID: 1109
		public abstract bool IsEmptyElement { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an attribute that was generated from the default value defined in the DTD or schema.</summary>
		/// <returns>true if the current node is an attribute whose value was generated from the default value defined in the DTD or schema; false if the attribute value was explicitly set.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001544D File Offset: 0x0001364D
		public virtual char QuoteChar
		{
			get
			{
				return '"';
			}
		}

		/// <summary>When overridden in a derived class, gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.None;
			}
		}

		/// <summary>When overridden in a derived class, gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00015451 File Offset: 0x00013651
		public virtual string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the schema information that has been assigned to the current node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the schema information for the current node. Schema information can be set on elements, attributes, or on text nodes with a non-null <see cref="P:System.Xml.XmlReader.ValueType" /> (typed values).If the current node is not one of the above node types, or if the XmlReader instance does not report schema information, this property returns null.If this property is called from an <see cref="T:System.Xml.XmlTextReader" /> or an <see cref="T:System.Xml.XmlValidatingReader" /> object, this property always returns null. These XmlReader implementations do not expose schema information through the SchemaInfo property.NoteIf you have to get the post-schema-validation information set (PSVI) for an element, position the reader on the end tag of the element, rather than on the start tag. You get the PSVI through the SchemaInfo property of a reader. The validating reader that is created through <see cref="Overload:System.Xml.XmlReader.Create" /> with the <see cref="P:System.Xml.XmlReaderSettings.ValidationType" /> property set to <see cref="F:System.Xml.ValidationType.Schema" /> has complete PSVI for an element only when the reader is positioned on the end tag of an element.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00015458 File Offset: 0x00013658
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		/// <summary>Gets The Common Language Runtime (CLR) type for the current node.</summary>
		/// <returns>The CLR type that corresponds to the typed value of the node. The default is System.String.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00015460 File Offset: 0x00013660
		public virtual Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		/// <summary>Reads the text content at the current position as an <see cref="T:System.Object" />.</summary>
		/// <returns>The text content as the most appropriate common language runtime (CLR) object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600045C RID: 1116 RVA: 0x0001546C File Offset: 0x0001366C
		public virtual object ReadContentAsObject()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsString();
		}

		/// <summary>Reads the text content at the current position as a Boolean.</summary>
		/// <returns>The text content as a <see cref="T:System.Boolean" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600045D RID: 1117 RVA: 0x00015488 File Offset: 0x00013688
		public virtual bool ReadContentAsBoolean()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsBoolean");
			}
			bool flag;
			try
			{
				flag = XmlConvert.ToBoolean(this.InternalReadContentAsString());
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", ex, this as IXmlLineInfo);
			}
			return flag;
		}

		/// <summary>Reads the text content at the current position as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The text content as a <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600045E RID: 1118 RVA: 0x000154E4 File Offset: 0x000136E4
		public virtual DateTime ReadContentAsDateTime()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDateTime");
			}
			DateTime dateTime;
			try
			{
				dateTime = XmlConvert.ToDateTime(this.InternalReadContentAsString(), XmlDateTimeSerializationMode.RoundtripKind);
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", ex, this as IXmlLineInfo);
			}
			return dateTime;
		}

		/// <summary>Reads the text content at the current position as a double-precision floating-point number.</summary>
		/// <returns>The text content as a double-precision floating-point number.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600045F RID: 1119 RVA: 0x00015540 File Offset: 0x00013740
		public virtual double ReadContentAsDouble()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDouble");
			}
			double num;
			try
			{
				num = XmlConvert.ToDouble(this.InternalReadContentAsString());
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", ex, this as IXmlLineInfo);
			}
			return num;
		}

		/// <summary>Reads the text content at the current position as a single-precision floating point number.</summary>
		/// <returns>The text content at the current position as a single-precision floating point number.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000460 RID: 1120 RVA: 0x0001559C File Offset: 0x0001379C
		public virtual float ReadContentAsFloat()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsFloat");
			}
			float num;
			try
			{
				num = XmlConvert.ToSingle(this.InternalReadContentAsString());
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", ex, this as IXmlLineInfo);
			}
			return num;
		}

		/// <summary>Reads the text content at the current position as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The text content at the current position as a <see cref="T:System.Decimal" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000461 RID: 1121 RVA: 0x000155F8 File Offset: 0x000137F8
		public virtual decimal ReadContentAsDecimal()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDecimal");
			}
			decimal num;
			try
			{
				num = XmlConvert.ToDecimal(this.InternalReadContentAsString());
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", ex, this as IXmlLineInfo);
			}
			return num;
		}

		/// <summary>Reads the text content at the current position as a 32-bit signed integer.</summary>
		/// <returns>The text content as a 32-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000462 RID: 1122 RVA: 0x00015654 File Offset: 0x00013854
		public virtual int ReadContentAsInt()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsInt");
			}
			int num;
			try
			{
				num = XmlConvert.ToInt32(this.InternalReadContentAsString());
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", ex, this as IXmlLineInfo);
			}
			return num;
		}

		/// <summary>Reads the text content at the current position as a 64-bit signed integer.</summary>
		/// <returns>The text content as a 64-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000463 RID: 1123 RVA: 0x000156B0 File Offset: 0x000138B0
		public virtual long ReadContentAsLong()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsLong");
			}
			long num;
			try
			{
				num = XmlConvert.ToInt64(this.InternalReadContentAsString());
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", ex, this as IXmlLineInfo);
			}
			return num;
		}

		/// <summary>Reads the text content at the current position as a <see cref="T:System.String" /> object.</summary>
		/// <returns>The text content as a <see cref="T:System.String" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000464 RID: 1124 RVA: 0x0001570C File Offset: 0x0001390C
		public virtual string ReadContentAsString()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsString");
			}
			return this.InternalReadContentAsString();
		}

		/// <summary>Reads the content as an object of the type specified.</summary>
		/// <returns>The concatenated text content or attribute value converted to the requested type.</returns>
		/// <param name="returnType">The type of the value to be returned.Note   With the release of the .NET Framework 3.5, the value of the <paramref name="returnType" /> parameter can now be the <see cref="T:System.DateTimeOffset" /> type.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion. For example, this can be used when converting an <see cref="T:System.Xml.XmlQualifiedName" /> object to an xs:string.This value can be null.</param>
		/// <exception cref="T:System.FormatException">The content is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="returnType" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not a supported node type. See the table below for details.</exception>
		/// <exception cref="T:System.OverflowException">Read Decimal.MaxValue.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000465 RID: 1125 RVA: 0x00015728 File Offset: 0x00013928
		public virtual object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAs");
			}
			string text = this.InternalReadContentAsString();
			if (returnType == typeof(string))
			{
				return text;
			}
			object obj;
			try
			{
				obj = XmlUntypedConverter.Untyped.ChangeType(text, returnType, (namespaceResolver == null) ? (this as IXmlNamespaceResolver) : namespaceResolver);
			}
			catch (FormatException ex)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex, this as IXmlLineInfo);
			}
			catch (InvalidCastException ex2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), ex2, this as IXmlLineInfo);
			}
			return obj;
		}

		/// <summary>Reads the current element and returns the contents as an <see cref="T:System.Object" />.</summary>
		/// <returns>A boxed common language runtime (CLR) object of the most appropriate type. The <see cref="P:System.Xml.XmlReader.ValueType" /> property determines the appropriate CLR type. If the content is typed as a list type, this method returns an array of boxed objects of the appropriate type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000466 RID: 1126 RVA: 0x000157D0 File Offset: 0x000139D0
		public virtual object ReadElementContentAsObject()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsObject"))
			{
				object obj = this.ReadContentAsObject();
				this.FinishReadElementContentAsXxx();
				return obj;
			}
			return string.Empty;
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as an <see cref="T:System.Object" />.</summary>
		/// <returns>A boxed common language runtime (CLR) object of the most appropriate type. The <see cref="P:System.Xml.XmlReader.ValueType" /> property determines the appropriate CLR type. If the content is typed as a list type, this method returns an array of boxed objects of the appropriate type.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000467 RID: 1127 RVA: 0x000157F1 File Offset: 0x000139F1
		public virtual object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsObject();
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.Boolean" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.Boolean" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.Boolean" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000468 RID: 1128 RVA: 0x00015801 File Offset: 0x00013A01
		public virtual bool ReadElementContentAsBoolean()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsBoolean"))
			{
				bool flag = this.ReadContentAsBoolean();
				this.FinishReadElementContentAsXxx();
				return flag;
			}
			return XmlConvert.ToBoolean(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a <see cref="T:System.Boolean" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.Boolean" /> object.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000469 RID: 1129 RVA: 0x00015827 File Offset: 0x00013A27
		public virtual bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsBoolean();
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.DateTime" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600046A RID: 1130 RVA: 0x00015837 File Offset: 0x00013A37
		public virtual DateTime ReadElementContentAsDateTime()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDateTime"))
			{
				DateTime dateTime = this.ReadContentAsDateTime();
				this.FinishReadElementContentAsXxx();
				return dateTime;
			}
			return XmlConvert.ToDateTime(string.Empty, XmlDateTimeSerializationMode.RoundtripKind);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The element contents as a <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600046B RID: 1131 RVA: 0x0001585E File Offset: 0x00013A5E
		public virtual DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDateTime();
		}

		/// <summary>Reads the current element and returns the contents as a double-precision floating-point number.</summary>
		/// <returns>The element content as a double-precision floating-point number.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a double-precision floating-point number.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600046C RID: 1132 RVA: 0x0001586E File Offset: 0x00013A6E
		public virtual double ReadElementContentAsDouble()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDouble"))
			{
				double num = this.ReadContentAsDouble();
				this.FinishReadElementContentAsXxx();
				return num;
			}
			return XmlConvert.ToDouble(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a double-precision floating-point number.</summary>
		/// <returns>The element content as a double-precision floating-point number.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600046D RID: 1133 RVA: 0x00015894 File Offset: 0x00013A94
		public virtual double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDouble();
		}

		/// <summary>Reads the current element and returns the contents as single-precision floating-point number.</summary>
		/// <returns>The element content as a single-precision floating point number.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a single-precision floating-point number.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600046E RID: 1134 RVA: 0x000158A4 File Offset: 0x00013AA4
		public virtual float ReadElementContentAsFloat()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsFloat"))
			{
				float num = this.ReadContentAsFloat();
				this.FinishReadElementContentAsXxx();
				return num;
			}
			return XmlConvert.ToSingle(string.Empty);
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.Decimal" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.Decimal" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600046F RID: 1135 RVA: 0x000158CA File Offset: 0x00013ACA
		public virtual decimal ReadElementContentAsDecimal()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDecimal"))
			{
				decimal num = this.ReadContentAsDecimal();
				this.FinishReadElementContentAsXxx();
				return num;
			}
			return XmlConvert.ToDecimal(string.Empty);
		}

		/// <summary>Reads the current element and returns the contents as a 32-bit signed integer.</summary>
		/// <returns>The element content as a 32-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 32-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000470 RID: 1136 RVA: 0x000158F0 File Offset: 0x00013AF0
		public virtual int ReadElementContentAsInt()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsInt"))
			{
				int num = this.ReadContentAsInt();
				this.FinishReadElementContentAsXxx();
				return num;
			}
			return XmlConvert.ToInt32(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a 32-bit signed integer.</summary>
		/// <returns>The element content as a 32-bit signed integer.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 32-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000471 RID: 1137 RVA: 0x00015916 File Offset: 0x00013B16
		public virtual int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsInt();
		}

		/// <summary>Reads the current element and returns the contents as a 64-bit signed integer.</summary>
		/// <returns>The element content as a 64-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 64-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000472 RID: 1138 RVA: 0x00015926 File Offset: 0x00013B26
		public virtual long ReadElementContentAsLong()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsLong"))
			{
				long num = this.ReadContentAsLong();
				this.FinishReadElementContentAsXxx();
				return num;
			}
			return XmlConvert.ToInt64(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a 64-bit signed integer.</summary>
		/// <returns>The element content as a 64-bit signed integer.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 64-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000473 RID: 1139 RVA: 0x0001594C File Offset: 0x00013B4C
		public virtual long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsLong();
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.String" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.String" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.String" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000474 RID: 1140 RVA: 0x0001595C File Offset: 0x00013B5C
		public virtual string ReadElementContentAsString()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsString"))
			{
				string text = this.ReadContentAsString();
				this.FinishReadElementContentAsXxx();
				return text;
			}
			return string.Empty;
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a <see cref="T:System.String" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.String" /> object.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.String" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000475 RID: 1141 RVA: 0x0001597D File Offset: 0x00013B7D
		public virtual string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsString();
		}

		/// <summary>Reads the element content as the requested type.</summary>
		/// <returns>The element content converted to the requested typed object.</returns>
		/// <param name="returnType">The type of the value to be returned.Note   With the release of the .NET Framework 3.5, the value of the <paramref name="returnType" /> parameter can now be the <see cref="T:System.DateTimeOffset" /> type.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.OverflowException">Read Decimal.MaxValue.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000476 RID: 1142 RVA: 0x00015990 File Offset: 0x00013B90
		public virtual object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAs"))
			{
				object obj = this.ReadContentAs(returnType, namespaceResolver);
				this.FinishReadElementContentAsXxx();
				return obj;
			}
			if (!(returnType == typeof(string)))
			{
				return XmlUntypedConverter.Untyped.ChangeType(string.Empty, returnType, namespaceResolver);
			}
			return string.Empty;
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the element content as the requested type.</summary>
		/// <returns>The element content converted to the requested typed object.</returns>
		/// <param name="returnType">The type of the value to be returned.Note   With the release of the .NET Framework 3.5, the value of the <paramref name="returnType" /> parameter can now be the <see cref="T:System.DateTimeOffset" /> type.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with null arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.OverflowException">Read Decimal.MaxValue.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000477 RID: 1143 RVA: 0x000159E2 File Offset: 0x00013BE2
		public virtual object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAs(returnType, namespaceResolver);
		}

		/// <summary>When overridden in a derived class, gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000478 RID: 1144
		public abstract int AttributeCount { get; }

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.Name" />.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found or the value is String.Empty, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000479 RID: 1145
		public abstract string GetAttribute(string name);

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" />.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found or the value is String.Empty, null is returned. This method does not move the reader.</returns>
		/// <param name="name">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600047A RID: 1146
		public abstract string GetAttribute(string name, string namespaceURI);

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute. This method does not move the reader.</returns>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.)</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="i" /> is out of range. It must be non-negative and less than the size of the attribute collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600047B RID: 1147
		public abstract string GetAttribute(int i);

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="i">The index of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B4 RID: 180
		public virtual string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.Name" />.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B5 RID: 181
		public virtual string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" />.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B6 RID: 182
		public virtual string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		/// <summary>When overridden in a derived class, moves to the attribute with the specified <see cref="P:System.Xml.XmlReader.Name" />.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The qualified name of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x0600047F RID: 1151
		public abstract bool MoveToAttribute(string name);

		/// <summary>When overridden in a derived class, moves to the attribute with the specified <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" />.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are null.</exception>
		// Token: 0x06000480 RID: 1152
		public abstract bool MoveToAttribute(string name, string ns);

		/// <summary>When overridden in a derived class, moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameter has a negative value.</exception>
		// Token: 0x06000481 RID: 1153 RVA: 0x00015A14 File Offset: 0x00013C14
		public virtual void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.MoveToElement();
			this.MoveToFirstAttribute();
			for (int j = 0; j < i; j++)
			{
				this.MoveToNextAttribute();
			}
		}

		/// <summary>When overridden in a derived class, moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000482 RID: 1154
		public abstract bool MoveToFirstAttribute();

		/// <summary>When overridden in a derived class, moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000483 RID: 1155
		public abstract bool MoveToNextAttribute();

		/// <summary>When overridden in a derived class, moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000484 RID: 1156
		public abstract bool MoveToElement();

		/// <summary>When overridden in a derived class, parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000485 RID: 1157
		public abstract bool ReadAttributeValue();

		/// <summary>When overridden in a derived class, reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000486 RID: 1158
		public abstract bool Read();

		/// <summary>When overridden in a derived class, gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000487 RID: 1159
		public abstract bool EOF { get; }

		/// <summary>When overridden in a derived class, changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to Closed.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000488 RID: 1160 RVA: 0x0000A558 File Offset: 0x00008758
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000489 RID: 1161
		public abstract ReadState ReadState { get; }

		/// <summary>Skips the children of the current node.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600048A RID: 1162 RVA: 0x00015A5A File Offset: 0x00013C5A
		public virtual void Skip()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return;
			}
			this.SkipSubtree();
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The XmlNameTable enabling you to get the atomized version of a string within the node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600048B RID: 1163
		public abstract XmlNameTable NameTable { get; }

		/// <summary>When overridden in a derived class, resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. </param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600048C RID: 1164
		public abstract string LookupNamespace(string prefix);

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool CanResolveEntity
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, resolves the entity reference for EntityReference nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an EntityReference node; this implementation of the reader cannot resolve entities (<see cref="P:System.Xml.XmlReader.CanResolveEntity" /> returns false).</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600048E RID: 1166
		public abstract void ResolveEntity();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool CanReadBinaryContent
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" /> is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000490 RID: 1168 RVA: 0x00015A6D File Offset: 0x00013C6D
		public virtual int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[] { "ReadContentAsBase64" }));
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000491 RID: 1169 RVA: 0x00015A8C File Offset: 0x00013C8C
		public virtual int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[] { "ReadElementContentAsBase64" }));
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" /> is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000492 RID: 1170 RVA: 0x00015AAB File Offset: 0x00013CAB
		public virtual int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[] { "ReadContentAsBinHex" }));
		}

		/// <summary>Reads the element and decodes the BinHex content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000493 RID: 1171 RVA: 0x00015ACA File Offset: 0x00013CCA
		public virtual int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[] { "ReadElementContentAsBinHex" }));
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XmlReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reads large streams of text embedded in an XML document.</summary>
		/// <returns>The number of characters read into the buffer. The value zero is returned when there is no more text content.</returns>
		/// <param name="buffer">The array of characters that serves as the buffer to which the text contents are written. This value cannot be null.</param>
		/// <param name="index">The offset within the buffer where the <see cref="T:System.Xml.XmlReader" /> can start to copy the results.</param>
		/// <param name="count">The maximum number of characters to copy into the buffer. The actual number of characters copied is returned from this method.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node does not have a value (<see cref="P:System.Xml.XmlReader.HasValue" /> is false).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer, or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML data is not well-formed.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000495 RID: 1173 RVA: 0x00015AE9 File Offset: 0x00013CE9
		public virtual int ReadValueChunk(char[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("ReadValueChunk method is not supported on this XmlReader. Use CanReadValueChunk property to find out if an XmlReader implements it."));
		}

		/// <summary>When overridden in a derived class, reads the contents of an element or text node as a string.</summary>
		/// <returns>The contents of the element or an empty string.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000496 RID: 1174 RVA: 0x00015AFC File Offset: 0x00013CFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadString()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			this.MoveToElement();
			if (this.NodeType == XmlNodeType.Element)
			{
				if (this.IsEmptyElement)
				{
					return string.Empty;
				}
				if (!this.Read())
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				if (this.NodeType == XmlNodeType.EndElement)
				{
					return string.Empty;
				}
			}
			string text = string.Empty;
			while (XmlReader.IsTextualNode(this.NodeType))
			{
				text += this.Value;
				if (!this.Read())
				{
					break;
				}
			}
			return text;
		}

		/// <summary>Checks whether the current node is a content (non-white space text, CDATA, Element, EndElement, EntityReference, or EndEntity) node. If the node is not a content node, the reader skips ahead to the next content node or end of file. It skips over nodes of the following type: ProcessingInstruction, DocumentType, Comment, Whitespace, or SignificantWhitespace.</summary>
		/// <returns>The <see cref="P:System.Xml.XmlReader.NodeType" /> of the current node found by the method or XmlNodeType.None if the reader has reached the end of the input stream.</returns>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000497 RID: 1175 RVA: 0x00015B8C File Offset: 0x00013D8C
		public virtual XmlNodeType MoveToContent()
		{
			for (;;)
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.EntityReference:
					goto IL_0033;
				case XmlNodeType.Attribute:
					goto IL_002C;
				default:
					if (nodeType - XmlNodeType.EndElement <= 1)
					{
						goto IL_0033;
					}
					if (!this.Read())
					{
						goto Block_2;
					}
					break;
				}
			}
			IL_002C:
			this.MoveToElement();
			IL_0033:
			return this.NodeType;
			Block_2:
			return this.NodeType;
		}

		/// <summary>Checks that the current node is an element and advances the reader to the next node.</summary>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML was encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000498 RID: 1176 RVA: 0x00015BE4 File Offset: 0x00013DE4
		public virtual void ReadStartElement()
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			this.Read();
		}

		/// <summary>Checks that the current content node is an element with the given <see cref="P:System.Xml.XmlReader.Name" /> and advances the reader to the next node.</summary>
		/// <param name="name">The qualified name of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML was encountered in the input stream. -or- The <see cref="P:System.Xml.XmlReader.Name" /> of the element does not match the given <paramref name="name" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000499 RID: 1177 RVA: 0x00015C28 File Offset: 0x00013E28
		public virtual void ReadStartElement(string name)
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.Name == name)
			{
				this.Read();
				return;
			}
			throw new XmlException("Element '{0}' was not found.", name, this as IXmlLineInfo);
		}

		/// <summary>Checks that the current content node is an element with the given <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> and advances the reader to the next node.</summary>
		/// <param name="localname">The local name of the element.</param>
		/// <param name="ns">The namespace URI of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML was encountered in the input stream.-or-The <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element found do not match the given arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600049A RID: 1178 RVA: 0x00015C8C File Offset: 0x00013E8C
		public virtual void ReadStartElement(string localname, string ns)
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName == localname && this.NamespaceURI == ns)
			{
				this.Read();
				return;
			}
			throw new XmlException("Element '{0}' with namespace name '{1}' was not found.", new string[] { localname, ns }, this as IXmlLineInfo);
		}

		/// <summary>Reads a text-only element.</summary>
		/// <returns>The text contained in the element that was read. An empty string if the element is empty (&lt;item&gt;&lt;/item&gt; or &lt;item/&gt;).</returns>
		/// <exception cref="T:System.Xml.XmlException">The next content node is not a start tag; or the element found does not contain a simple text value.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600049B RID: 1179 RVA: 0x00015D0C File Offset: 0x00013F0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString()
		{
			string text = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				this.Read();
				text = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("Unexpected node type {0}. {1} method can only be called on elements with simple or empty content.", new string[]
					{
						this.NodeType.ToString(),
						"ReadElementString"
					}, this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return text;
		}

		/// <summary>Checks that the <see cref="P:System.Xml.XmlReader.Name" /> property of the element found matches the given string before reading a text-only element.</summary>
		/// <returns>The text contained in the element that was read. An empty string if the element is empty (&lt;item&gt;&lt;/item&gt; or &lt;item/&gt;).</returns>
		/// <param name="name">The name to check.</param>
		/// <exception cref="T:System.Xml.XmlException">If the next content node is not a start tag; if the element Name does not match the given argument; or if the element found does not contain a simple text value.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600049C RID: 1180 RVA: 0x00015DB4 File Offset: 0x00013FB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString(string name)
		{
			string text = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.Name != name)
			{
				throw new XmlException("Element '{0}' was not found.", name, this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				text = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return text;
		}

		/// <summary>Checks that the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element found matches the given strings before reading a text-only element.</summary>
		/// <returns>The text contained in the element that was read. An empty string if the element is empty (&lt;item&gt;&lt;/item&gt; or &lt;item/&gt;).</returns>
		/// <param name="localname">The local name to check.</param>
		/// <param name="ns">The namespace URI to check.</param>
		/// <exception cref="T:System.Xml.XmlException">If the next content node is not a start tag; if the element LocalName or NamespaceURI do not match the given arguments; or if the element found does not contain a simple text value.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600049D RID: 1181 RVA: 0x00015E64 File Offset: 0x00014064
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString(string localname, string ns)
		{
			string text = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName != localname || this.NamespaceURI != ns)
			{
				throw new XmlException("Element '{0}' with namespace name '{1}' was not found.", new string[] { localname, ns }, this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				text = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return text;
		}

		/// <summary>Checks that the current content node is an end tag and advances the reader to the next node.</summary>
		/// <exception cref="T:System.Xml.XmlException">The current node is not an end tag or if incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600049E RID: 1182 RVA: 0x00015F30 File Offset: 0x00014130
		public virtual void ReadEndElement()
		{
			if (this.MoveToContent() != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			this.Read();
		}

		/// <summary>Calls <see cref="M:System.Xml.XmlReader.MoveToContent" /> and tests if the current content node is a start tag or empty element tag.</summary>
		/// <returns>true if <see cref="M:System.Xml.XmlReader.MoveToContent" /> finds a start tag or empty element tag; false if a node type other than XmlNodeType.Element was found.</returns>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600049F RID: 1183 RVA: 0x00015F73 File Offset: 0x00014173
		public virtual bool IsStartElement()
		{
			return this.MoveToContent() == XmlNodeType.Element;
		}

		/// <summary>Calls <see cref="M:System.Xml.XmlReader.MoveToContent" /> and tests if the current content node is a start tag or empty element tag and if the <see cref="P:System.Xml.XmlReader.Name" /> property of the element found matches the given argument.</summary>
		/// <returns>true if the resulting node is an element and the Name property matches the specified string. false if a node type other than XmlNodeType.Element was found or if the element Name property does not match the specified string.</returns>
		/// <param name="name">The string matched against the Name property of the element found.</param>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060004A0 RID: 1184 RVA: 0x00015F7E File Offset: 0x0001417E
		public virtual bool IsStartElement(string name)
		{
			return this.MoveToContent() == XmlNodeType.Element && this.Name == name;
		}

		/// <summary>Calls <see cref="M:System.Xml.XmlReader.MoveToContent" /> and tests if the current content node is a start tag or empty element tag and if the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element found match the given strings.</summary>
		/// <returns>true if the resulting node is an element. false if a node type other than XmlNodeType.Element was found or if the LocalName and NamespaceURI properties of the element do not match the specified strings.</returns>
		/// <param name="localname">The string to match against the LocalName property of the element found.</param>
		/// <param name="ns">The string to match against the NamespaceURI property of the element found.</param>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060004A1 RID: 1185 RVA: 0x00015F97 File Offset: 0x00014197
		public virtual bool IsStartElement(string localname, string ns)
		{
			return this.MoveToContent() == XmlNodeType.Element && this.LocalName == localname && this.NamespaceURI == ns;
		}

		/// <summary>Reads until an element with the specified qualified name is found.</summary>
		/// <returns>true if a matching element is found; otherwise false and the <see cref="T:System.Xml.XmlReader" /> is in an end of file state.</returns>
		/// <param name="name">The qualified name of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x060004A2 RID: 1186 RVA: 0x00015FC0 File Offset: 0x000141C0
		public virtual bool ReadToFollowing(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			name = this.NameTable.Add(name);
			while (this.Read())
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Reads until an element with the specified local name and namespace URI is found.</summary>
		/// <returns>true if a matching element is found; otherwise false and the <see cref="T:System.Xml.XmlReader" /> is in an end of file state.</returns>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are null.</exception>
		// Token: 0x060004A3 RID: 1187 RVA: 0x00016018 File Offset: 0x00014218
		public virtual bool ReadToFollowing(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.Read())
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Advances the <see cref="T:System.Xml.XmlReader" /> to the next descendant element with the specified qualified name.</summary>
		/// <returns>true if a matching descendant element is found; otherwise false. If a matching child element is not found, the <see cref="T:System.Xml.XmlReader" /> is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is XmlNodeType.EndElement) of the element.If the <see cref="T:System.Xml.XmlReader" /> is not positioned on an element when <see cref="M:System.Xml.XmlReader.ReadToDescendant(System.String)" /> was called, this method returns false and the position of the <see cref="T:System.Xml.XmlReader" /> is not changed.</returns>
		/// <param name="name">The qualified name of the element you wish to move to.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x060004A4 RID: 1188 RVA: 0x00016098 File Offset: 0x00014298
		public virtual bool ReadToDescendant(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			int num = this.Depth;
			if (this.NodeType != XmlNodeType.Element)
			{
				if (this.ReadState != ReadState.Initial)
				{
					return false;
				}
				num--;
			}
			else if (this.IsEmptyElement)
			{
				return false;
			}
			name = this.NameTable.Add(name);
			while (this.Read() && this.Depth > num)
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Advances the <see cref="T:System.Xml.XmlReader" /> to the next descendant element with the specified local name and namespace URI.</summary>
		/// <returns>true if a matching descendant element is found; otherwise false. If a matching child element is not found, the <see cref="T:System.Xml.XmlReader" /> is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is XmlNodeType.EndElement) of the element.If the <see cref="T:System.Xml.XmlReader" /> is not positioned on an element when <see cref="M:System.Xml.XmlReader.ReadToDescendant(System.String,System.String)" /> was called, this method returns false and the position of the <see cref="T:System.Xml.XmlReader" /> is not changed.</returns>
		/// <param name="localName">The local name of the element you wish to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the element you wish to move to.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are null.</exception>
		// Token: 0x060004A5 RID: 1189 RVA: 0x00016124 File Offset: 0x00014324
		public virtual bool ReadToDescendant(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			int num = this.Depth;
			if (this.NodeType != XmlNodeType.Element)
			{
				if (this.ReadState != ReadState.Initial)
				{
					return false;
				}
				num--;
			}
			else if (this.IsEmptyElement)
			{
				return false;
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.Read() && this.Depth > num)
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Advances the XmlReader to the next sibling element with the specified qualified name.</summary>
		/// <returns>true if a matching sibling element is found; otherwise false. If a matching sibling element is not found, the XmlReader is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is XmlNodeType.EndElement) of the parent element.</returns>
		/// <param name="name">The qualified name of the sibling element you wish to move to.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x060004A6 RID: 1190 RVA: 0x000161D8 File Offset: 0x000143D8
		public virtual bool ReadToNextSibling(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			name = this.NameTable.Add(name);
			while (this.SkipSubtree())
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
				if (nodeType == XmlNodeType.EndElement || this.EOF)
				{
					break;
				}
			}
			return false;
		}

		/// <summary>Advances the XmlReader to the next sibling element with the specified local name and namespace URI.</summary>
		/// <returns>true if a matching sibling element is found; otherwise, false. If a matching sibling element is not found, the XmlReader is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is XmlNodeType.EndElement) of the parent element.</returns>
		/// <param name="localName">The local name of the sibling element you wish to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the sibling element you wish to move to.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are null.</exception>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0001623C File Offset: 0x0001443C
		public virtual bool ReadToNextSibling(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.SkipSubtree())
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
				if (nodeType == XmlNodeType.EndElement || this.EOF)
				{
					break;
				}
			}
			return false;
		}

		/// <summary>When overridden in a derived class, reads all the content, including markup, as a string.</summary>
		/// <returns>All the XML content, including markup, in the current node. If the current node has no children, an empty string is returned.If the current node is neither an element nor attribute, an empty string is returned.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML was not well-formed, or an error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060004A8 RID: 1192 RVA: 0x000162CC File Offset: 0x000144CC
		public virtual string ReadInnerXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = this.CreateWriterForInnerOuterXml(stringWriter);
			try
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					((XmlTextWriter)xmlWriter).QuoteChar = this.QuoteChar;
					this.WriteAttributeValue(xmlWriter);
				}
				if (this.NodeType == XmlNodeType.Element)
				{
					this.WriteNode(xmlWriter, false);
				}
			}
			finally
			{
				xmlWriter.Close();
			}
			return stringWriter.ToString();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001636C File Offset: 0x0001456C
		private void WriteNode(XmlWriter xtw, bool defattr)
		{
			int num = ((this.NodeType == XmlNodeType.None) ? (-1) : this.Depth);
			while (this.Read() && num < this.Depth)
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Element:
					xtw.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
					((XmlTextWriter)xtw).QuoteChar = this.QuoteChar;
					xtw.WriteAttributes(this, defattr);
					if (this.IsEmptyElement)
					{
						xtw.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					xtw.WriteString(this.Value);
					break;
				case XmlNodeType.CDATA:
					xtw.WriteCData(this.Value);
					break;
				case XmlNodeType.EntityReference:
					xtw.WriteEntityRef(this.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					xtw.WriteProcessingInstruction(this.Name, this.Value);
					break;
				case XmlNodeType.Comment:
					xtw.WriteComment(this.Value);
					break;
				case XmlNodeType.DocumentType:
					xtw.WriteDocType(this.Name, this.GetAttribute("PUBLIC"), this.GetAttribute("SYSTEM"), this.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					xtw.WriteWhitespace(this.Value);
					break;
				case XmlNodeType.EndElement:
					xtw.WriteFullEndElement();
					break;
				}
			}
			if (num == this.Depth && this.NodeType == XmlNodeType.EndElement)
			{
				this.Read();
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000164E8 File Offset: 0x000146E8
		private void WriteAttributeValue(XmlWriter xtw)
		{
			string name = this.Name;
			while (this.ReadAttributeValue())
			{
				if (this.NodeType == XmlNodeType.EntityReference)
				{
					xtw.WriteEntityRef(this.Name);
				}
				else
				{
					xtw.WriteString(this.Value);
				}
			}
			this.MoveToAttribute(name);
		}

		/// <summary>When overridden in a derived class, reads the content, including markup, representing this node and all its children.</summary>
		/// <returns>If the reader is positioned on an element or an attribute node, this method returns all the XML content, including markup, of the current node and all its children; otherwise, it returns an empty string.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML was not well-formed, or an error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060004AB RID: 1195 RVA: 0x00016534 File Offset: 0x00014734
		public virtual string ReadOuterXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = this.CreateWriterForInnerOuterXml(stringWriter);
			try
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					xmlWriter.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
					this.WriteAttributeValue(xmlWriter);
					xmlWriter.WriteEndAttribute();
				}
				else
				{
					xmlWriter.WriteNode(this, false);
				}
			}
			finally
			{
				xmlWriter.Close();
			}
			return stringWriter.ToString();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000165DC File Offset: 0x000147DC
		private XmlWriter CreateWriterForInnerOuterXml(StringWriter sw)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(sw);
			this.SetNamespacesFlag(xmlTextWriter);
			return xmlTextWriter;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000165F8 File Offset: 0x000147F8
		private void SetNamespacesFlag(XmlTextWriter xtw)
		{
			XmlTextReader xmlTextReader = this as XmlTextReader;
			if (xmlTextReader != null)
			{
				xtw.Namespaces = xmlTextReader.Namespaces;
				return;
			}
			XmlValidatingReader xmlValidatingReader = this as XmlValidatingReader;
			if (xmlValidatingReader != null)
			{
				xtw.Namespaces = xmlValidatingReader.Namespaces;
			}
		}

		/// <summary>Returns a new XmlReader instance that can be used to read the current node, and all its descendants.</summary>
		/// <returns>A new XmlReader instance set to ReadState.Initial. A call to the <see cref="M:System.Xml.XmlReader.Read" /> method positions the new XmlReader on the node that was current before the call to ReadSubtree method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The XmlReader is not positioned on an element when this method is called.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060004AE RID: 1198 RVA: 0x00016632 File Offset: 0x00014832
		public virtual XmlReader ReadSubtree()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("ReadSubtree() can be called only if the reader is on an element node."));
			}
			return new XmlSubtreeReader(this);
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>true if the current node has attributes; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00016653 File Offset: 0x00014853
		public virtual bool HasAttributes
		{
			get
			{
				return this.AttributeCount > 0;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Xml.XmlReader" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060004B0 RID: 1200 RVA: 0x0001665E File Offset: 0x0001485E
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Xml.XmlReader" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060004B1 RID: 1201 RVA: 0x00016667 File Offset: 0x00014867
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.ReadState != ReadState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00014C6C File Offset: 0x00012E6C
		internal virtual XmlNamespaceManager NamespaceManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001667B File Offset: 0x0001487B
		internal static bool IsTextualNode(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.IsTextualNodeBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001668F File Offset: 0x0001488F
		internal static bool CanReadContentAs(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.CanReadContentAsBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000166A3 File Offset: 0x000148A3
		internal static bool HasValueInternal(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.HasValueBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000166B8 File Offset: 0x000148B8
		private bool SkipSubtree()
		{
			this.MoveToElement();
			if (this.NodeType == XmlNodeType.Element && !this.IsEmptyElement)
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
				return this.NodeType == XmlNodeType.EndElement && this.Read();
			}
			return this.Read();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00016710 File Offset: 0x00014910
		internal void CheckElement(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			if (this.NodeType != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName != localName || this.NamespaceURI != namespaceURI)
			{
				throw new XmlException("Element '{0}' with namespace name '{1}' was not found.", new string[] { localName, namespaceURI }, this as IXmlLineInfo);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000167AB File Offset: 0x000149AB
		internal Exception CreateReadContentAsException(string methodName)
		{
			return XmlReader.CreateReadContentAsException(methodName, this.NodeType, this as IXmlLineInfo);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000167BF File Offset: 0x000149BF
		internal Exception CreateReadElementContentAsException(string methodName)
		{
			return XmlReader.CreateReadElementContentAsException(methodName, this.NodeType, this as IXmlLineInfo);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000167D3 File Offset: 0x000149D3
		internal bool CanReadContentAs()
		{
			return XmlReader.CanReadContentAs(this.NodeType);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000167E0 File Offset: 0x000149E0
		internal static Exception CreateReadContentAsException(string methodName, XmlNodeType nodeType, IXmlLineInfo lineInfo)
		{
			string text = "The {0} method is not supported on node type {1}. If you want to read typed content of an element, use the ReadElementContentAs method.";
			object[] array = new string[]
			{
				methodName,
				nodeType.ToString()
			};
			return new InvalidOperationException(XmlReader.AddLineInfo(Res.GetString(text, array), lineInfo));
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00016820 File Offset: 0x00014A20
		internal static Exception CreateReadElementContentAsException(string methodName, XmlNodeType nodeType, IXmlLineInfo lineInfo)
		{
			string text = "The {0} method is not supported on node type {1}.";
			object[] array = new string[]
			{
				methodName,
				nodeType.ToString()
			};
			return new InvalidOperationException(XmlReader.AddLineInfo(Res.GetString(text, array), lineInfo));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00016860 File Offset: 0x00014A60
		private static string AddLineInfo(string message, IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				string[] array = new string[]
				{
					lineInfo.LineNumber.ToString(CultureInfo.InvariantCulture),
					lineInfo.LinePosition.ToString(CultureInfo.InvariantCulture)
				};
				string text = message;
				string text2 = " ";
				string text3 = "Line {0}, position {1}.";
				object[] array2 = array;
				message = text + text2 + Res.GetString(text3, array2);
			}
			return message;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000168C0 File Offset: 0x00014AC0
		internal string InternalReadContentAsString()
		{
			string text = string.Empty;
			StringBuilder stringBuilder = null;
			do
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Attribute:
					goto IL_0055;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (text.Length == 0)
					{
						text = this.Value;
						goto IL_009B;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append(text);
					}
					stringBuilder.Append(this.Value);
					goto IL_009B;
				case XmlNodeType.EntityReference:
					if (this.CanResolveEntity)
					{
						this.ResolveEntity();
						goto IL_009B;
					}
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_009B;
				}
				break;
				IL_009B:;
			}
			while ((this.AttributeCount != 0) ? this.ReadAttributeValue() : this.Read());
			goto IL_00B6;
			IL_0055:
			return this.Value;
			IL_00B6:
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00016990 File Offset: 0x00014B90
		private bool SetupReadElementContentAsXxx(string methodName)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.CreateReadElementContentAsException(methodName);
			}
			bool isEmptyElement = this.IsEmptyElement;
			this.Read();
			if (isEmptyElement)
			{
				return false;
			}
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.EndElement)
			{
				this.Read();
				return false;
			}
			if (nodeType == XmlNodeType.Element)
			{
				throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this as IXmlLineInfo);
			}
			return true;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000169F0 File Offset: 0x00014BF0
		private void FinishReadElementContentAsXxx()
		{
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString());
			}
			this.Read();
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00016A30 File Offset: 0x00014C30
		internal bool IsDefaultInternal
		{
			get
			{
				if (this.IsDefault)
				{
					return true;
				}
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				return schemaInfo != null && schemaInfo.IsDefault;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00014C6C File Offset: 0x00012E6C
		internal virtual IDtdInfo DtdInfo
		{
			get
			{
				return null;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance using the specified stream, base URI, and <see cref="T:System.Xml.XmlReaderSettings" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object to read XML data.</returns>
		/// <param name="input">The stream containing the XML data. The <see cref="T:System.Xml.XmlReader" /> scans the first bytes of the stream looking for a byte order mark or other sign of encoding. When encoding is determined, the encoding is used to continue reading the stream, and processing continues parsing the input as a stream of (Unicode) characters.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlReaderSettings" /> object used to configure the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be null.</param>
		/// <param name="baseUri">The base URI for the entity or document being read. This value can be null.Security Note   The base URI is used to resolve the relative URI of the XML document. Do not use a base URI from an untrusted source.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is null.</exception>
		// Token: 0x060004C3 RID: 1219 RVA: 0x00016A5C File Offset: 0x00014C5C
		public static XmlReader Create(Stream input, XmlReaderSettings settings, string baseUri)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, null, baseUri, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance with the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object to read the XML data.</returns>
		/// <param name="input">The <see cref="T:System.IO.TextReader" /> from which to read the XML data. Because a <see cref="T:System.IO.TextReader" /> returns a stream of Unicode characters, the encoding specified in the XML declaration is not used by the <see cref="T:System.Xml.XmlReader" /> to decode the data stream.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is null.</exception>
		// Token: 0x060004C4 RID: 1220 RVA: 0x00016A72 File Offset: 0x00014C72
		public static XmlReader Create(TextReader input)
		{
			return XmlReader.Create(input, null, string.Empty);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance using the specified <see cref="T:System.IO.TextReader" />, <see cref="T:System.Xml.XmlReaderSettings" />, and base URI.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object to read XML data.</returns>
		/// <param name="input">The <see cref="T:System.IO.TextReader" /> from which to read the XML data. Because a <see cref="T:System.IO.TextReader" /> returns a stream of Unicode characters, the encoding specified in the XML declaration is not used by the <see cref="T:System.Xml.XmlReader" /> to decode the data stream.</param>
		/// <param name="settings">The <see cref="T:System.Xml.XmlReaderSettings" /> object used to configure the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be null.</param>
		/// <param name="baseUri">The base URI for the entity or document being read. This value can be null.Security Note   The base URI is used to resolve the relative URI of the XML document. Do not use a base URI from an untrusted source.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is null.</exception>
		// Token: 0x060004C5 RID: 1221 RVA: 0x00016A80 File Offset: 0x00014C80
		public static XmlReader Create(TextReader input, XmlReaderSettings settings, string baseUri)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, baseUri, null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00016A98 File Offset: 0x00014C98
		internal static XmlReader CreateSqlReader(Stream input, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			byte[] array = new byte[XmlReader.CalcBufferSize(input)];
			int num = 0;
			int num2;
			do
			{
				num2 = input.Read(array, num, array.Length - num);
				num += num2;
			}
			while (num2 > 0 && num < 2);
			XmlReader xmlReader;
			if (num >= 2 && array[0] == 223 && array[1] == 255)
			{
				if (inputContext != null)
				{
					throw new ArgumentException(Res.GetString("BinaryXml Parser does not support initialization with XmlParserContext."), "inputContext");
				}
				xmlReader = new XmlSqlBinaryReader(input, array, num, string.Empty, settings.CloseInput, settings);
			}
			else
			{
				xmlReader = new XmlTextReaderImpl(input, array, num, settings, null, string.Empty, inputContext, settings.CloseInput);
			}
			if (settings.ValidationType != ValidationType.None)
			{
				xmlReader = settings.AddValidation(xmlReader);
			}
			if (settings.Async)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00016B64 File Offset: 0x00014D64
		internal static int CalcBufferSize(Stream input)
		{
			int num = 4096;
			if (input.CanSeek)
			{
				long length = input.Length;
				if (length < (long)num)
				{
					num = checked((int)length);
				}
				else if (length > 65536L)
				{
					num = 8192;
				}
			}
			return num;
		}

		// Token: 0x0400025E RID: 606
		private static uint IsTextualNodeBitmap = 24600U;

		// Token: 0x0400025F RID: 607
		private static uint CanReadContentAsBitmap = 123324U;

		// Token: 0x04000260 RID: 608
		private static uint HasValueBitmap = 157084U;
	}
}
