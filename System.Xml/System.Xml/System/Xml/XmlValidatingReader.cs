using System;

namespace System.Xml
{
	/// <summary>Represents a reader that provides document type definition (DTD), XML-Data Reduced (XDR) schema, and XML Schema definition language (XSD) validation.</summary>
	// Token: 0x0200009B RID: 155
	[Obsolete("Use XmlReader created by XmlReader.Create() method using appropriate XmlReaderSettings instead. https://go.microsoft.com/fwlink/?linkid=14202")]
	public class XmlValidatingReader : XmlReader
	{
		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0002C8AE File Offset: 0x0002AAAE
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0002C8BB File Offset: 0x0002AABB
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		/// <summary>Gets the namespace Uniform Resource Identifier (URI) (as defined in the World Wide Web Consortium (W3C) Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0002C8D5 File Offset: 0x0002AAD5
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlValidatingReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space between markup in a mixed content model. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0002C8E2 File Offset: 0x0002AAE2
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0002C8EF File Offset: 0x0002AAEF
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002C8FC File Offset: 0x0002AAFC
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlValidatingReader.NodeType" /> equals XmlNodeType.Element) that ends with /&gt;; otherwise, false.</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0002C909 File Offset: 0x0002AB09
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node. This number includes default attributes.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002C916 File Offset: 0x0002AB16
		public override int AttributeCount
		{
			get
			{
				return this.impl.AttributeCount;
			}
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x060007F5 RID: 2037 RVA: 0x0002C923 File Offset: 0x0002AB23
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned. This method does not move the reader.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x060007F6 RID: 2038 RVA: 0x0002C931 File Offset: 0x0002AB31
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlValidatingReader.AttributeCount" />. </exception>
		// Token: 0x060007F7 RID: 2039 RVA: 0x0002C940 File Offset: 0x0002AB40
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the position of the reader does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x060007F8 RID: 2040 RVA: 0x0002C94E File Offset: 0x0002AB4E
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		/// <summary>Moves to the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the position of the reader does not change.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x060007F9 RID: 2041 RVA: 0x0002C95C File Offset: 0x0002AB5C
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x060007FA RID: 2042 RVA: 0x0002C96B File Offset: 0x0002AB6B
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x060007FB RID: 2043 RVA: 0x0002C978 File Offset: 0x0002AB78
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x060007FC RID: 2044 RVA: 0x0002C985 File Offset: 0x0002AB85
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x060007FD RID: 2045 RVA: 0x0002C992 File Offset: 0x0002AB92
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		// Token: 0x060007FE RID: 2046 RVA: 0x0002C99F File Offset: 0x0002AB9F
		public override bool Read()
		{
			return this.impl.Read();
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002C9B9 File Offset: 0x0002ABB9
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>XmlNameTable that enables you to get the atomized version of a string within the node.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002C9C6 File Offset: 0x0002ABC6
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace Uniform Resource Identifier (URI) you want to resolve. To match the default namespace, pass an empty string. </param>
		// Token: 0x06000802 RID: 2050 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an EntityReference node. </exception>
		// Token: 0x06000803 RID: 2051 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to do namespace support; otherwise, false. The default is true.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0002CA09 File Offset: 0x0002AC09
		public bool Namespaces
		{
			get
			{
				return this.impl.Namespaces;
			}
		}

		// Token: 0x04000456 RID: 1110
		private XmlValidatingReaderImpl impl;
	}
}
