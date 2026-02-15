using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an attribute. Valid and default values for the attribute are defined in a document type definition (DTD) or schema.</summary>
	// Token: 0x020000D9 RID: 217
	public class XmlAttribute : XmlNode
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x0003A618 File Offset: 0x00038818
		internal XmlAttribute(XmlName name, XmlDocument doc)
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
				throw new ArgumentException(Res.GetString("The attribute local name cannot be empty."));
			}
			this.name = name;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0003A675 File Offset: 0x00038875
		internal int LocalNameHash
		{
			get
			{
				return this.name.HashCode;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlAttribute" /> class.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace uniform resource identifier (URI).</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000ABE RID: 2750 RVA: 0x0003A682 File Offset: 0x00038882
		protected internal XmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: this(doc.AddAttrXmlName(prefix, localName, namespaceURI, null), doc)
		{
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0003A697 File Offset: 0x00038897
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0003A69F File Offset: 0x0003889F
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
		/// <returns>The duplicate node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself </param>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x0003A6A8 File Offset: 0x000388A8
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlAttribute.CopyChildren(ownerDocument, this, true);
			return xmlAttribute;
		}

		/// <summary>Gets the parent of this node. For XmlAttribute nodes, this property always returns null.</summary>
		/// <returns>For XmlAttribute nodes, this property always returns null.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>The qualified name of the attribute node.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0003A6DD File Offset: 0x000388DD
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>The name of the attribute node with the prefix removed. In the following example &lt;book bk:genre= 'novel'&gt;, the LocalName of the attribute is genre.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0003A6EA File Offset: 0x000388EA
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If the attribute is not explicitly given a namespace, this property returns String.Empty.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0003A6F7 File Offset: 0x000388F7
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only.</exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The namespaceURI of this node is null.The specified prefix is "xml", and the namespaceURI of this node is different from "http://www.w3.org/XML/1998/namespace".This node is an attribute, the specified prefix is "xmlns", and the namespaceURI of this node is different from "http://www.w3.org/2000/xmlns/".This node is an attribute, and the qualifiedName of this node is "xmlns" [Namespaces].</exception>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0003A704 File Offset: 0x00038904
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0003A711 File Offset: 0x00038911
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddAttrXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type for XmlAttribute nodes is XmlNodeType.Attribute.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0003A73C File Offset: 0x0003893C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>An XML document to which this node belongs.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0003A73F File Offset: 0x0003893F
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. For XmlAttribute nodes, this property is the value of attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The node is read-only and a set operation is called.</exception>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0003A74C File Offset: 0x0003894C
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x0003A754 File Offset: 0x00038954
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Gets the post-schema-validation-infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> containing the post-schema-validation-infoset of this node.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0003A697 File Offset: 0x00038897
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children. For attribute nodes, this property has the same functionality as the <see cref="P:System.Xml.XmlAttribute.Value" /> property.</returns>
		// Token: 0x17000239 RID: 569
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0003A760 File Offset: 0x00038960
		public override string InnerText
		{
			set
			{
				if (this.PrepareOwnerElementInElementIdAttrMap())
				{
					string innerText = base.InnerText;
					base.InnerText = value;
					this.ResetOwnerElementInElementIdAttrMap(innerText);
					return;
				}
				base.InnerText = value;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0003A794 File Offset: 0x00038994
		internal bool PrepareOwnerElementInElementIdAttrMap()
		{
			if (this.OwnerDocument.DtdSchemaInfo != null)
			{
				XmlElement ownerElement = this.OwnerElement;
				if (ownerElement != null)
				{
					return ownerElement.Attributes.PrepareParentInElementIdAttrMap(this.Prefix, this.LocalName);
				}
			}
			return false;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0003A7D4 File Offset: 0x000389D4
		internal void ResetOwnerElementInElementIdAttrMap(string oldInnerText)
		{
			XmlElement ownerElement = this.OwnerElement;
			if (ownerElement != null)
			{
				ownerElement.Attributes.ResetParentInElementIdAttrMap(oldInnerText, this.InnerText);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0003A800 File Offset: 0x00038A00
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null)
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

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0003A892 File Offset: 0x00038A92
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0003A89A File Offset: 0x00038A9A
		internal override XmlLinkedNode LastNode
		{
			get
			{
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0003A8A3 File Offset: 0x00038AA3
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.EntityReference;
		}

		/// <summary>Gets a value indicating whether the attribute value was explicitly set.</summary>
		/// <returns>true if this attribute was explicitly given a value in the original instance document; otherwise, false. A value of false indicates that the value of the attribute came from the DTD.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public virtual bool Specified
		{
			get
			{
				return true;
			}
		}

		/// <summary>Inserts the specified node immediately before the specified reference node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> inserted.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to insert.</param>
		/// <param name="refChild">The <see cref="T:System.Xml.XmlNode" /> that is the reference node. The <paramref name="newChild" /> is placed before this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The current node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only.</exception>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x0003A8B0 File Offset: 0x00038AB0
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.InsertBefore(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.InsertBefore(newChild, refChild);
			}
			return xmlNode;
		}

		/// <summary>Inserts the specified node immediately after the specified reference node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> inserted.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to insert.</param>
		/// <param name="refChild">The <see cref="T:System.Xml.XmlNode" /> that is the reference node. The <paramref name="newChild" /> is placed after the <paramref name="refChild" />.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only.</exception>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0003A8E8 File Offset: 0x00038AE8
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.InsertAfter(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.InsertAfter(newChild, refChild);
			}
			return xmlNode;
		}

		/// <summary>Removes the specified child node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> removed.</returns>
		/// <param name="oldChild">The <see cref="T:System.Xml.XmlNode" /> to remove.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="oldChild" /> is not a child of this node. Or this node is read-only.</exception>
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0003A920 File Offset: 0x00038B20
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.RemoveChild(oldChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.RemoveChild(oldChild);
			}
			return xmlNode;
		}

		/// <summary>Adds the specified node to the beginning of the list of child nodes for this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> added.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to add. If it is an <see cref="T:System.Xml.XmlDocumentFragment" />, the entire contents of the document fragment are moved into the child list of this node.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.</exception>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x0003A958 File Offset: 0x00038B58
		public override XmlNode PrependChild(XmlNode newChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.PrependChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.PrependChild(newChild);
			}
			return xmlNode;
		}

		/// <summary>Adds the specified node to the end of the list of child nodes, of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> added.</returns>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to add.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.</exception>
		// Token: 0x06000ADA RID: 2778 RVA: 0x0003A990 File Offset: 0x00038B90
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode xmlNode;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				xmlNode = base.AppendChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				xmlNode = base.AppendChild(newChild);
			}
			return xmlNode;
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> to which the attribute belongs.</summary>
		/// <returns>The XmlElement that the attribute belongs to or null if this attribute is not part of an XmlElement.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0003A9C6 File Offset: 0x00038BC6
		public virtual XmlElement OwnerElement
		{
			get
			{
				return this.parentNode as XmlElement;
			}
		}

		/// <summary>Sets the value of the attribute.</summary>
		/// <returns>The attribute value.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed.</exception>
		// Token: 0x1700023E RID: 574
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0003A9D3 File Offset: 0x00038BD3
		public override string InnerXml
		{
			set
			{
				this.RemoveAll();
				new XmlLoader().LoadInnerXmlAttribute(this, value);
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save.</param>
		// Token: 0x06000ADD RID: 2781 RVA: 0x0003A9E7 File Offset: 0x00038BE7
		public override void WriteTo(XmlWriter w)
		{
			w.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			this.WriteContentTo(w);
			w.WriteEndAttribute();
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save.</param>
		// Token: 0x06000ADE RID: 2782 RVA: 0x0003AA10 File Offset: 0x00038C10
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the node.</summary>
		/// <returns>The location from which the node was loaded or String.Empty if the node has no base URI. Attribute nodes have the same base URI as their owner element. If an attribute node does not have an owner element, BaseURI returns String.Empty.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0003AA37 File Offset: 0x00038C37
		public override string BaseURI
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0003AA52 File Offset: 0x00038C52
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0003AA5B File Offset: 0x00038C5B
		internal override XmlSpace XmlSpace
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlSpace;
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0003AA72 File Offset: 0x00038C72
		internal override string XmlLang
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlLang;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0003AA8D File Offset: 0x00038C8D
		internal override XPathNodeType XPNodeType
		{
			get
			{
				if (this.IsNamespace)
				{
					return XPathNodeType.Namespace;
				}
				return XPathNodeType.Attribute;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0003AA9A File Offset: 0x00038C9A
		internal override string XPLocalName
		{
			get
			{
				if (this.name.Prefix.Length == 0 && this.name.LocalName == "xmlns")
				{
					return string.Empty;
				}
				return this.name.LocalName;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0003AAD6 File Offset: 0x00038CD6
		internal bool IsNamespace
		{
			get
			{
				return Ref.Equal(this.name.NamespaceURI, this.name.OwnerDocument.strReservedXmlns);
			}
		}

		// Token: 0x040005F6 RID: 1526
		private XmlName name;

		// Token: 0x040005F7 RID: 1527
		private XmlLinkedNode lastChild;
	}
}
