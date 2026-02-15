using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an element.</summary>
	// Token: 0x020000E5 RID: 229
	public class XmlElement : XmlLinkedNode
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003D0D4 File Offset: 0x0003B2D4
		internal XmlElement(XmlName name, bool empty, XmlDocument doc)
			: base(doc)
		{
			this.parentNode = null;
			if (!doc.IsLoading)
			{
				XmlDocument.CheckName(name.Prefix);
				XmlDocument.CheckName(name.LocalName);
			}
			if (name.LocalName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The local name for elements or attributes cannot be null or an empty string."));
			}
			this.name = name;
			if (empty)
			{
				this.lastChild = this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlElement" /> class.</summary>
		/// <param name="prefix">The namespace prefix; see the <see cref="P:System.Xml.XmlElement.Prefix" /> property.</param>
		/// <param name="localName">The local name; see the <see cref="P:System.Xml.XmlElement.LocalName" /> property.</param>
		/// <param name="namespaceURI">The namespace URI; see the <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000BC5 RID: 3013 RVA: 0x0003D13B File Offset: 0x0003B33B
		protected internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: this(doc.AddXmlName(prefix, localName, namespaceURI, null), true, doc)
		{
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0003D151 File Offset: 0x0003B351
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0003D159 File Offset: 0x0003B359
		internal XmlName XmlName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself (and its attributes if the node is an XmlElement). </param>
		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003D164 File Offset: 0x0003B364
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			bool isLoading = ownerDocument.IsLoading;
			ownerDocument.IsLoading = true;
			XmlElement xmlElement = ownerDocument.CreateElement(this.Prefix, this.LocalName, this.NamespaceURI);
			ownerDocument.IsLoading = isLoading;
			if (xmlElement.IsEmpty != this.IsEmpty)
			{
				xmlElement.IsEmpty = this.IsEmpty;
			}
			if (this.HasAttributes)
			{
				foreach (object obj in this.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					XmlAttribute xmlAttribute2 = (XmlAttribute)xmlAttribute.CloneNode(true);
					if (xmlAttribute is XmlUnspecifiedAttribute && !xmlAttribute.Specified)
					{
						((XmlUnspecifiedAttribute)xmlAttribute2).SetSpecified(false);
					}
					xmlElement.Attributes.InternalAppendAttribute(xmlAttribute2);
				}
			}
			if (deep)
			{
				xmlElement.CopyChildren(ownerDocument, this, deep);
			}
			return xmlElement;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>The qualified name of the node. For XmlElement nodes, this is the tag name of the element.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0003D25C File Offset: 0x0003B45C
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0003D269 File Offset: 0x0003B469
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If there is no namespace URI, this property returns String.Empty.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0003D276 File Offset: 0x0003B476
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only </exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The namespaceURI of this node is null.The specified prefix is "xml" and the namespaceURI of this node is different from http://www.w3.org/XML/1998/namespace. </exception>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0003D283 File Offset: 0x0003B483
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0003D290 File Offset: 0x0003B490
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For XmlElement nodes, this value is XmlNodeType.Element.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0003D2BB File Offset: 0x0003B4BB
		public override XmlNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The XmlDocument to which this element belongs.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0003D2C3 File Offset: 0x0003B4C3
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null || this.lastChild == this)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				xmlLinkedNode.SetParentForLoad(this);
			}
			else
			{
				XmlLinkedNode xmlLinkedNode2 = this.lastChild;
				xmlLinkedNode.next = xmlLinkedNode2.next;
				xmlLinkedNode2.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				if (xmlLinkedNode2.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
				}
				else
				{
					xmlLinkedNode.SetParentForLoad(this);
				}
			}
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		/// <summary>Gets or sets the tag format of the element.</summary>
		/// <returns>Returns true if the element is to be serialized in the short tag format "&lt;item/&gt;"; false for the long format "&lt;item&gt;&lt;/item&gt;".When setting this property, if set to true, the children of the element are removed and the element is serialized in the short tag format. If set to false, the value of the property is changed (regardless of whether or not the element has content); if the element is empty, it is serialized in the long format.This property is a Microsoft extension to the Document Object Model (DOM).</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0003D36B File Offset: 0x0003B56B
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0003D376 File Offset: 0x0003B576
		public bool IsEmpty
		{
			get
			{
				return this.lastChild == this;
			}
			set
			{
				if (value)
				{
					if (this.lastChild != this)
					{
						this.RemoveAllChildren();
						this.lastChild = this;
						return;
					}
				}
				else if (this.lastChild == this)
				{
					this.lastChild = null;
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0003D3A2 File Offset: 0x0003B5A2
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0003D3B5 File Offset: 0x0003B5B5
		internal override XmlLinkedNode LastNode
		{
			get
			{
				if (this.lastChild != this)
				{
					return this.lastChild;
				}
				return null;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
		internal override bool IsValidChildType(XmlNodeType type)
		{
			switch (type)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return true;
			}
			return false;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</summary>
		/// <returns>
		///   <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</returns>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0003D414 File Offset: 0x0003B614
		public override XmlAttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					object objLock = this.OwnerDocument.objLock;
					lock (objLock)
					{
						if (this.attributes == null)
						{
							this.attributes = new XmlAttributeCollection(this);
						}
					}
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a boolean value indicating whether the current node has any attributes.</summary>
		/// <returns>true if the current node has attributes; otherwise, false.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0003D478 File Offset: 0x0003B678
		public virtual bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		/// <summary>Returns the value for the attribute with the specified name.</summary>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x06000BDA RID: 3034 RVA: 0x0003D494 File Offset: 0x0003B694
		public virtual string GetAttribute(string name)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		/// <summary>Sets the value of the attribute with the specified name.</summary>
		/// <param name="name">The name of the attribute to create or alter. This is a qualified name. If the name contains a colon it is parsed into prefix and local name components. </param>
		/// <param name="value">The value to set for the attribute. </param>
		/// <exception cref="T:System.Xml.XmlException">The specified name contains an invalid character. </exception>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x06000BDB RID: 3035 RVA: 0x0003D4B8 File Offset: 0x0003B6B8
		public virtual void SetAttribute(string name, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(name);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
				return;
			}
			xmlAttribute.Value = value;
		}

		/// <summary>Returns the XmlAttribute with the specified name.</summary>
		/// <returns>The specified XmlAttribute or null if a matching attribute was not found.</returns>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x06000BDC RID: 3036 RVA: 0x0003D4F9 File Offset: 0x0003B6F9
		public virtual XmlAttribute GetAttributeNode(string name)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[name];
			}
			return null;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>If the attribute replaces an existing attribute with the same name, the old XmlAttribute is returned; otherwise, null is returned.</returns>
		/// <param name="newAttr">The XmlAttribute node to add to the attribute collection for this element. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newAttr" /> was created from a different document than the one that created this node. Or this node is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="newAttr" /> is already an attribute of another XmlElement object. You must explicitly clone XmlAttribute nodes to re-use them in other XmlElement objects. </exception>
		// Token: 0x06000BDD RID: 3037 RVA: 0x0003D511 File Offset: 0x0003B711
		public virtual XmlAttribute SetAttributeNode(XmlAttribute newAttr)
		{
			if (newAttr.OwnerElement != null)
			{
				throw new InvalidOperationException(Res.GetString("The 'Attribute' node cannot be inserted because it is already an attribute of another element."));
			}
			return (XmlAttribute)this.Attributes.SetNamedItem(newAttr);
		}

		/// <summary>Returns the value for the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		/// <param name="localName">The local name of the attribute to retrieve. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to retrieve. </param>
		// Token: 0x06000BDE RID: 3038 RVA: 0x0003D53C File Offset: 0x0003B73C
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		/// <summary>Sets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <returns>The attribute value.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <param name="value">The value to set for the attribute. </param>
		// Token: 0x06000BDF RID: 3039 RVA: 0x0003D564 File Offset: 0x0003B764
		public virtual string SetAttribute(string localName, string namespaceURI, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			else
			{
				xmlAttribute.Value = value;
			}
			return value;
		}

		/// <summary>Returns the <see cref="T:System.Xml.XmlAttribute" /> with the specified local name and namespace URI.</summary>
		/// <returns>The specified XmlAttribute or null if a matching attribute was not found.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003D5AE File Offset: 0x0003B7AE
		public virtual XmlAttribute GetAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[localName, namespaceURI];
			}
			return null;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <returns>The XmlAttribute to add.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003D5C8 File Offset: 0x0003B7C8
		public virtual XmlAttribute SetAttributeNode(string localName, string namespaceURI)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			return xmlAttribute;
		}

		/// <summary>Determines whether the current node has an attribute with the specified name.</summary>
		/// <returns>true if the current node has the specified attribute; otherwise, false.</returns>
		/// <param name="name">The name of the attribute to find. This is a qualified name. It is matched against the Name property of the matching node. </param>
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0003D602 File Offset: 0x0003B802
		public virtual bool HasAttribute(string name)
		{
			return this.GetAttributeNode(name) != null;
		}

		/// <summary>Saves the current node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003D610 File Offset: 0x0003B810
		public override void WriteTo(XmlWriter w)
		{
			if (base.GetType() == typeof(XmlElement))
			{
				XmlElement.WriteElementTo(w, this);
				return;
			}
			this.WriteStartElement(w);
			if (this.IsEmpty)
			{
				w.WriteEndElement();
				return;
			}
			this.WriteContentTo(w);
			w.WriteFullEndElement();
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003D660 File Offset: 0x0003B860
		private static void WriteElementTo(XmlWriter writer, XmlElement e)
		{
			XmlNode xmlNode = e;
			XmlNode xmlNode2 = e;
			for (;;)
			{
				e = xmlNode2 as XmlElement;
				if (e != null && e.GetType() == typeof(XmlElement))
				{
					e.WriteStartElement(writer);
					if (e.IsEmpty)
					{
						writer.WriteEndElement();
					}
					else
					{
						if (e.lastChild != null)
						{
							xmlNode2 = e.FirstChild;
							continue;
						}
						writer.WriteFullEndElement();
					}
				}
				else
				{
					xmlNode2.WriteTo(writer);
				}
				while (xmlNode2 != xmlNode && xmlNode2 == xmlNode2.ParentNode.LastChild)
				{
					xmlNode2 = xmlNode2.ParentNode;
					writer.WriteFullEndElement();
				}
				if (xmlNode2 == xmlNode)
				{
					break;
				}
				xmlNode2 = xmlNode2.NextSibling;
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0003D6FC File Offset: 0x0003B8FC
		private void WriteStartElement(XmlWriter w)
		{
			w.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
			if (this.HasAttributes)
			{
				XmlAttributeCollection xmlAttributeCollection = this.Attributes;
				for (int i = 0; i < xmlAttributeCollection.Count; i++)
				{
					xmlAttributeCollection[i].WriteTo(w);
				}
			}
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000BE6 RID: 3046 RVA: 0x0003D750 File Offset: 0x0003B950
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		/// <summary>Removes all specified attributes from the element. Default attributes are not removed.</summary>
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003D777 File Offset: 0x0003B977
		public virtual void RemoveAllAttributes()
		{
			if (this.HasAttributes)
			{
				this.attributes.RemoveAll();
			}
		}

		/// <summary>Removes all specified attributes and children of the current node. Default attributes are not removed.</summary>
		// Token: 0x06000BE8 RID: 3048 RVA: 0x0003D78C File Offset: 0x0003B98C
		public override void RemoveAll()
		{
			base.RemoveAll();
			this.RemoveAllAttributes();
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003D79A File Offset: 0x0003B99A
		internal void RemoveAllChildren()
		{
			base.RemoveAll();
		}

		/// <summary>Gets the post schema validation infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the post schema validation infoset of this node.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0003D151 File Offset: 0x0003B351
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the markup representing just the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170002A6 RID: 678
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0003D7A2 File Offset: 0x0003B9A2
		public override string InnerXml
		{
			set
			{
				this.RemoveAllChildren();
				new XmlLoader().LoadInnerXmlElement(this, value);
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0003D7B6 File Offset: 0x0003B9B6
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0003D7C0 File Offset: 0x0003B9C0
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				XmlLinkedNode lastNode = this.LastNode;
				if (lastNode != null && lastNode.NodeType == XmlNodeType.Text && lastNode.next == lastNode)
				{
					lastNode.Value = value;
					return;
				}
				this.RemoveAllChildren();
				this.AppendChild(this.OwnerDocument.CreateTextNode(value));
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNode" /> immediately following this element.</summary>
		/// <returns>The XmlNode immediately following this element.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003D80A File Offset: 0x0003BA0A
		public override XmlNode NextSibling
		{
			get
			{
				if (this.parentNode != null && this.parentNode.LastNode != this)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0003AA52 File Offset: 0x00038C52
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Element;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0003D82A File Offset: 0x0003BA2A
		internal override string XPLocalName
		{
			get
			{
				return this.LocalName;
			}
		}

		// Token: 0x04000636 RID: 1590
		private XmlName name;

		// Token: 0x04000637 RID: 1591
		private XmlAttributeCollection attributes;

		// Token: 0x04000638 RID: 1592
		private XmlLinkedNode lastChild;
	}
}
