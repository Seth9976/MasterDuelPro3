using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, non-cached, forward-only access to XML data.</summary>
	// Token: 0x0200006F RID: 111
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlTextReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream.</summary>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. </exception>
		// Token: 0x06000551 RID: 1361 RVA: 0x00019289 File Offset: 0x00017489
		public XmlTextReader(Stream input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL, stream and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. If <paramref name="url" /> is null, BaseURI is set to String.Empty. </param>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="input" /> or <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000552 RID: 1362 RVA: 0x000192A9 File Offset: 0x000174A9
		public XmlTextReader(string url, Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		// Token: 0x06000553 RID: 1363 RVA: 0x000192CB File Offset: 0x000174CB
		public XmlTextReader(TextReader input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="input">The TextReader containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is null. </exception>
		// Token: 0x06000554 RID: 1364 RVA: 0x000192EB File Offset: 0x000174EB
		public XmlTextReader(TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file.</summary>
		/// <param name="url">The URL for the file containing the XML data. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000555 RID: 1365 RVA: 0x0001930C File Offset: 0x0001750C
		public XmlTextReader(string url)
		{
			this.impl = new XmlTextReaderImpl(url, new NameTable());
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL for the file containing the XML data to read. </param>
		/// <param name="nt">The XmlNameTable to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is null.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000556 RID: 1366 RVA: 0x00019331 File Offset: 0x00017531
		public XmlTextReader(string url, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00019352 File Offset: 0x00017552
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, Name is bk:book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name AttributeThe name of the attribute. DocumentTypeThe document type name. ElementThe tag name. EntityReferenceThe name of the entity referenced. ProcessingInstructionThe target of the processing instruction. XmlDeclarationThe literal string xml. </returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001935F File Offset: 0x0001755F
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.For node types that do not have a name (like Text, Comment, and so on), this property returns String.Empty.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001936C File Offset: 0x0001756C
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		/// <summary>Gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00019379 File Offset: 0x00017579
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00019386 File Offset: 0x00017586
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlTextReader.Value" /> other than String.Empty.</summary>
		/// <returns>true if the node on which the reader is currently positioned can have a Value; otherwise, false.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00019393 File Offset: 0x00017593
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return String.Empty.Node Type Value AttributeThe value of the attribute. CDATAThe content of the CDATA section. CommentThe content of the comment. DocumentTypeThe internal subset. ProcessingInstructionThe entire content, excluding the target. SignificantWhitespaceThe white space within an xml:space= 'preserve' scope. TextThe content of the text node. WhitespaceThe white space between markup. XmlDeclarationThe content of the declaration. </returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000193A0 File Offset: 0x000175A0
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000193AD File Offset: 0x000175AD
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000193BA File Offset: 0x000175BA
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>true if the current node is an element (<see cref="P:System.Xml.XmlTextReader.NodeType" /> equals XmlNodeType.Element) that ends with /&gt;; otherwise, false.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000193C7 File Offset: 0x000175C7
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the DTD or schema.</summary>
		/// <returns>This property always returns false. (<see cref="T:System.Xml.XmlTextReader" /> does not expand default attributes.) </returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x000193D4 File Offset: 0x000175D4
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		/// <summary>Gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000193E1 File Offset: 0x000175E1
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		/// <summary>Gets the current xml:space scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no xml:space scope exists, this property defaults to XmlSpace.None.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x000193EE File Offset: 0x000175EE
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		/// <summary>Gets the current xml:lang scope.</summary>
		/// <returns>The current xml:lang scope.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000193FB File Offset: 0x000175FB
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00019408 File Offset: 0x00017608
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
		// Token: 0x06000566 RID: 1382 RVA: 0x00019415 File Offset: 0x00017615
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. If the attribute is not found, null is returned. This method does not move the reader.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000567 RID: 1383 RVA: 0x00019423 File Offset: 0x00017623
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <returns>The value of the specified attribute.</returns>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlTextReader.AttributeCount" />. </exception>
		// Token: 0x06000568 RID: 1384 RVA: 0x00019432 File Offset: 0x00017632
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x06000569 RID: 1385 RVA: 0x00019440 File Offset: 0x00017640
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		/// <summary>Moves to the attribute with the specified local name and namespace URI.</summary>
		/// <returns>true if the attribute is found; otherwise, false. If false, the reader's position does not change.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x0600056A RID: 1386 RVA: 0x0001944E File Offset: 0x0001764E
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x0600056B RID: 1387 RVA: 0x0001945D File Offset: 0x0001765D
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>true if an attribute exists (the reader moves to the first attribute); otherwise, false (the position of the reader does not change).</returns>
		// Token: 0x0600056C RID: 1388 RVA: 0x0001946B File Offset: 0x0001766B
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>true if there is a next attribute; false if there are no more attributes.</returns>
		// Token: 0x0600056D RID: 1389 RVA: 0x00019478 File Offset: 0x00017678
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>true if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); false if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x0600056E RID: 1390 RVA: 0x00019485 File Offset: 0x00017685
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		/// <summary>Parses the attribute value into one or more Text, EntityReference, or EndEntity nodes.</summary>
		/// <returns>true if there are nodes to return.false if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns true with a single node with a value of String.Empty.</returns>
		// Token: 0x0600056F RID: 1391 RVA: 0x00019492 File Offset: 0x00017692
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>true if the next node was read successfully; false if there are no more nodes to read.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		// Token: 0x06000570 RID: 1392 RVA: 0x0001949F File Offset: 0x0001769F
		public override bool Read()
		{
			return this.impl.Read();
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>true if the reader is positioned at the end of the stream; otherwise, false.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000194AC File Offset: 0x000176AC
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to Closed.</summary>
		// Token: 0x06000572 RID: 1394 RVA: 0x000194B9 File Offset: 0x000176B9
		public override void Close()
		{
			this.impl.Close();
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000194C6 File Offset: 0x000176C6
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		/// <summary>Skips the children of the current node.</summary>
		// Token: 0x06000574 RID: 1396 RVA: 0x000194D3 File Offset: 0x000176D3
		public override void Skip()
		{
			this.impl.Skip();
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The XmlNameTable enabling you to get the atomized version of a string within the node.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000194E0 File Offset: 0x000176E0
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <returns>The namespace URI to which the prefix maps or null if no matching prefix is found.</returns>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. This string does not have to be atomized. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Xml.XmlTextReader.Namespaces" /> property is set to true and the <paramref name="prefix" /> value is null. </exception>
		// Token: 0x06000576 RID: 1398 RVA: 0x000194F0 File Offset: 0x000176F0
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>true if the reader can parse and resolve entities; otherwise, false. The XmlTextReader class always returns true.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Resolves the entity reference for EntityReference nodes.</summary>
		// Token: 0x06000578 RID: 1400 RVA: 0x00019518 File Offset: 0x00017718
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the binary content read methods.</summary>
		/// <returns>true if the binary content read methods are implemented; otherwise false. The <see cref="T:System.Xml.XmlTextReader" /> class always returns true.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlTextReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" />  is not supported in the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x0600057A RID: 1402 RVA: 0x00019525 File Offset: 0x00017725
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBase64(buffer, index, count);
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x0600057B RID: 1403 RVA: 0x00019535 File Offset: 0x00017735
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBase64(buffer, index, count);
		}

		/// <summary>Reads the content and returns the BinHex decoded binary bytes.</summary>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be null.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Xml.XmlTextReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		// Token: 0x0600057C RID: 1404 RVA: 0x00019545 File Offset: 0x00017745
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBinHex(buffer, index, count);
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
		// Token: 0x0600057D RID: 1405 RVA: 0x00019555 File Offset: 0x00017755
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method; otherwise false. The <see cref="T:System.Xml.XmlTextReader" /> class always returns false.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reads the contents of an element or a text node as a string.</summary>
		/// <returns>The contents of the element or text node. This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.Note: The text node can be either an element or an attribute text node.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		/// <exception cref="T:System.InvalidOperationException">An invalid operation was attempted. </exception>
		// Token: 0x0600057F RID: 1407 RVA: 0x00019565 File Offset: 0x00017765
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>true if the class can return line information; otherwise, false.</returns>
		// Token: 0x06000580 RID: 1408 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public bool HasLineInfo()
		{
			return true;
		}

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00019578 File Offset: 0x00017778
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00019585 File Offset: 0x00017785
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains the current in-scope namespaces.</returns>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		// Token: 0x06000583 RID: 1411 RVA: 0x00019592 File Offset: 0x00017792
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupNamespace(System.String)" />.</summary>
		/// <returns>The namespace URI that is mapped to the prefix; null if the prefix is not mapped to a namespace URI.</returns>
		/// <param name="prefix">The prefix whose namespace URI you wish to find.</param>
		// Token: 0x06000584 RID: 1412 RVA: 0x000195A0 File Offset: 0x000177A0
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <returns>The prefix that is mapped to the namespace URI; null if the namespace URI is not mapped to a prefix.</returns>
		/// <param name="namespaceName">The namespace URI whose prefix you wish to find.</param>
		// Token: 0x06000585 RID: 1413 RVA: 0x000195AE File Offset: 0x000177AE
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>true to do namespace support; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property after a read operation has occurred (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is not ReadState.Initial). </exception>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000195BC File Offset: 0x000177BC
		public bool Namespaces
		{
			get
			{
				return this.impl.Namespaces;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize white space and attribute values.</summary>
		/// <returns>true to normalize; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is ReadState.Closed). </exception>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000195C9 File Offset: 0x000177C9
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x000195D6 File Offset: 0x000177D6
		public bool Normalization
		{
			get
			{
				return this.impl.Normalization;
			}
			set
			{
				this.impl.Normalization = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how white space is handled.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WhitespaceHandling" /> values. The default is WhitespaceHandling.All (returns Whitespace and SignificantWhitespace nodes).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Invalid value specified. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is ReadState.Closed). </exception>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000195E4 File Offset: 0x000177E4
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x000195F1 File Offset: 0x000177F1
		public WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.impl.WhitespaceHandling;
			}
			set
			{
				this.impl.WhitespaceHandling = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.DtdProcessing" /> enumeration.</summary>
		/// <returns>The <see cref="T:System.Xml.DtdProcessing" /> enumeration.</returns>
		// Token: 0x17000103 RID: 259
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000195FF File Offset: 0x000177FF
		public DtdProcessing DtdProcessing
		{
			set
			{
				this.impl.DtdProcessing = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the reader handles entities.</summary>
		/// <returns>One of the <see cref="T:System.Xml.EntityHandling" /> values. If no EntityHandling is specified, it defaults to EntityHandling.ExpandCharEntities.</returns>
		// Token: 0x17000104 RID: 260
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001960D File Offset: 0x0001780D
		public EntityHandling EntityHandling
		{
			set
			{
				this.impl.EntityHandling = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used for resolving DTD references.</summary>
		/// <returns>The XmlResolver to use. If set to null, external resources are not resolved.In version 1.1 of the .NET Framework, the caller must be fully trusted in order to specify an XmlResolver.</returns>
		// Token: 0x17000105 RID: 261
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0001961B File Offset: 0x0001781B
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00019629 File Offset: 0x00017829
		internal XmlTextReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00019631 File Offset: 0x00017831
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.impl.NamespaceManager;
			}
		}

		// Token: 0x17000108 RID: 264
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001963E File Offset: 0x0001783E
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.impl.XmlValidatingReaderCompatibilityMode = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001964C File Offset: 0x0001784C
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.impl.DtdInfo;
			}
		}

		// Token: 0x040002A0 RID: 672
		private XmlTextReaderImpl impl;
	}
}
