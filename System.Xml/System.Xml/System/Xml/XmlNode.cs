using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a single node in the XML document. </summary>
	// Token: 0x020000F1 RID: 241
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	[DefaultMember("Item")]
	public abstract class XmlNode : ICloneable, IEnumerable, IXPathNavigable
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x00002127 File Offset: 0x00000327
		internal XmlNode()
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0003FC8D File Offset: 0x0003DE8D
		internal XmlNode(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentException(Res.GetString("Cannot create a node without an owner document."));
			}
			this.parentNode = doc;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> for navigating this object.</summary>
		/// <returns>An XPathNavigator object used to navigate the node. The XPathNavigator is positioned on the node from which the method was called. It is not positioned on the root of the document.</returns>
		// Token: 0x06000C6F RID: 3183 RVA: 0x0003FCB0 File Offset: 0x0003DEB0
		public virtual XPathNavigator CreateNavigator()
		{
			XmlDocument xmlDocument = this as XmlDocument;
			if (xmlDocument != null)
			{
				return xmlDocument.CreateNavigator(this);
			}
			return this.OwnerDocument.CreateNavigator(this);
		}

		/// <summary>Selects the first XmlNode that matches the XPath expression.</summary>
		/// <returns>The first XmlNode that matches the XPath query or null if no matching node is found. The XmlNode should not be expected to be connected "live" to the XML document. That is, changes that appear in the XML document may not appear in the XmlNode, and vice versa.</returns>
		/// <param name="xpath">The XPath expression. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression contains a prefix. </exception>
		// Token: 0x06000C70 RID: 3184 RVA: 0x0003FCDC File Offset: 0x0003DEDC
		public XmlNode SelectSingleNode(string xpath)
		{
			XmlNodeList xmlNodeList = this.SelectNodes(xpath);
			if (xmlNodeList == null)
			{
				return null;
			}
			return xmlNodeList[0];
		}

		/// <summary>Selects a list of nodes matching the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a collection of nodes matching the XPath query.</returns>
		/// <param name="xpath">The XPath expression. </param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression contains a prefix. </exception>
		// Token: 0x06000C71 RID: 3185 RVA: 0x0003FD00 File Offset: 0x0003DF00
		public XmlNodeList SelectNodes(string xpath)
		{
			XPathNavigator xpathNavigator = this.CreateNavigator();
			if (xpathNavigator == null)
			{
				return null;
			}
			return new XPathNodeList(xpathNavigator.Select(xpath));
		}

		/// <summary>Gets the qualified name of the node, when overridden in a derived class.</summary>
		/// <returns>The qualified name of the node. The name returned is dependent on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node: Type Name Attribute The qualified name of the attribute. CDATA #cdata-section Comment #comment Document #document DocumentFragment #document-fragment DocumentType The document type name. Element The qualified name of the element. Entity The name of the entity. EntityReference The name of the entity referenced. Notation The notation name. ProcessingInstruction The target of the processing instruction. Text #text Whitespace #whitespace SignificantWhitespace #significant-whitespace XmlDeclaration #xml-declaration </returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C72 RID: 3186
		public abstract string Name { get; }

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node: Type Value Attribute The value of the attribute. CDATASection The content of the CDATA Section. Comment The content of the comment. Document null. DocumentFragment null. DocumentType null. Element null. You can use the <see cref="P:System.Xml.XmlElement.InnerText" /> or <see cref="P:System.Xml.XmlElement.InnerXml" /> properties to access the value of the element node. Entity null. EntityReference null. Notation null. ProcessingInstruction The entire content excluding the target. Text The content of the text node. SignificantWhitespace The white space characters. White space can consist of one or more space characters, carriage returns, line feeds, or tabs. Whitespace The white space characters. White space can consist of one or more space characters, carriage returns, line feeds, or tabs. XmlDeclaration The content of the declaration (that is, everything between &lt;?xml and ?&gt;). </returns>
		/// <exception cref="T:System.ArgumentException">Setting the value of a node that is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting the value of a node that is not supposed to have a value (for example, an Element node). </exception>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00014C6C File Offset: 0x00012E6C
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x0003FD28 File Offset: 0x0003DF28
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Res.GetString("Cannot set a value on node type '{0}'."), this.NodeType.ToString()));
			}
		}

		/// <summary>Gets the type of the current node, when overridden in a derived class.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values.</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C75 RID: 3189
		public abstract XmlNodeType NodeType { get; }

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The XmlNode that is the parent of the current node. If a node has just been created and not yet added to the tree, or if it has been removed from the tree, the parent is null. For all other nodes, the value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. The following table describes the possible return values for the ParentNode property.NodeType Return Value of ParentNode Attribute, Document, DocumentFragment, Entity, Notation Returns null; these nodes do not have parents. CDATA Returns the element or entity reference containing the CDATA section. Comment Returns the element, entity reference, document type, or document containing the comment. DocumentType Returns the document node. Element Returns the parent node of the element. If the element is the root node in the tree, the parent is the document node. EntityReference Returns the element, attribute, or entity reference containing the entity reference. ProcessingInstruction Returns the document, element, document type, or entity reference containing the processing instruction. Text Returns the parent element, attribute, or entity reference containing the text node. </returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0003FD64 File Offset: 0x0003DF64
		public virtual XmlNode ParentNode
		{
			get
			{
				if (this.parentNode.NodeType != XmlNodeType.Document)
				{
					return this.parentNode;
				}
				XmlLinkedNode xmlLinkedNode = this.parentNode.FirstChild as XmlLinkedNode;
				if (xmlLinkedNode != null)
				{
					XmlLinkedNode xmlLinkedNode2 = xmlLinkedNode;
					while (xmlLinkedNode2 != this)
					{
						xmlLinkedNode2 = xmlLinkedNode2.next;
						if (xmlLinkedNode2 == null || xmlLinkedNode2 == xmlLinkedNode)
						{
							goto IL_0045;
						}
					}
					return this.parentNode;
				}
				IL_0045:
				return null;
			}
		}

		/// <summary>Gets all the child nodes of the node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> that contains all the child nodes of the node.If there are no child nodes, this property returns an empty <see cref="T:System.Xml.XmlNodeList" />.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0003FDB7 File Offset: 0x0003DFB7
		public virtual XmlNodeList ChildNodes
		{
			get
			{
				return new XmlChildNodes(this);
			}
		}

		/// <summary>Gets the node immediately preceding this node.</summary>
		/// <returns>The preceding XmlNode. If there is no preceding node, null is returned.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlNode PreviousSibling
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the node immediately following this node.</summary>
		/// <returns>The next XmlNode. If there is no next node, null is returned.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlNode NextSibling
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlAttributeCollection" /> containing the attributes of this node.</summary>
		/// <returns>An XmlAttributeCollection containing the attributes of the node.If the node is of type XmlNodeType.Element, the attributes of the node are returned. Otherwise, this property returns null.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlAttributeCollection Attributes
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.If the node is an <see cref="T:System.Xml.XmlDocument" /> (NodeType equals XmlNodeType.Document), this property returns null.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0003FDBF File Offset: 0x0003DFBF
		public virtual XmlDocument OwnerDocument
		{
			get
			{
				if (this.parentNode.NodeType == XmlNodeType.Document)
				{
					return (XmlDocument)this.parentNode;
				}
				return this.parentNode.OwnerDocument;
			}
		}

		/// <summary>Gets the first child of the node.</summary>
		/// <returns>The first child of the node. If there is no such node, null is returned.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
		public virtual XmlNode FirstChild
		{
			get
			{
				XmlLinkedNode lastNode = this.LastNode;
				if (lastNode != null)
				{
					return lastNode.next;
				}
				return null;
			}
		}

		/// <summary>Gets the last child of the node.</summary>
		/// <returns>The last child of the node. If there is no such node, null is returned.</returns>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0003FE07 File Offset: 0x0003E007
		public virtual XmlNode LastChild
		{
			get
			{
				return this.LastNode;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal virtual bool IsContainer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00014C6C File Offset: 0x00012E6C
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0000A558 File Offset: 0x00008758
		internal virtual XmlLinkedNode LastNode
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0003FE10 File Offset: 0x0003E010
		internal bool AncestorNode(XmlNode node)
		{
			XmlNode xmlNode = this.ParentNode;
			while (xmlNode != null && xmlNode != this)
			{
				if (xmlNode == node)
				{
					return true;
				}
				xmlNode = xmlNode.ParentNode;
			}
			return false;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0003FE3C File Offset: 0x0003E03C
		internal bool IsConnected()
		{
			XmlNode xmlNode = this.ParentNode;
			while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Document)
			{
				xmlNode = xmlNode.ParentNode;
			}
			return xmlNode != null;
		}

		/// <summary>Inserts the specified node immediately before the specified reference node.</summary>
		/// <returns>The node being inserted.</returns>
		/// <param name="newChild">The XmlNode to insert. </param>
		/// <param name="refChild">The XmlNode that is the reference node. The <paramref name="newChild" /> is placed before this node. </param>
		/// <exception cref="T:System.InvalidOperationException">The current node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only. </exception>
		// Token: 0x06000C83 RID: 3203 RVA: 0x0003FE6C File Offset: 0x0003E06C
		public virtual XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Cannot insert a node or any ancestor of that node as a child of itself."));
			}
			if (refChild == null)
			{
				return this.AppendChild(newChild);
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes."));
			}
			if (refChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("The reference node is not a child of this node."));
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			XmlDocument ownerDocument2 = this.OwnerDocument;
			if (ownerDocument != null && ownerDocument != ownerDocument2 && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("The node to be inserted is from a different document context."));
			}
			if (!this.CanInsertBefore(newChild, refChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode firstChild;
				XmlNode xmlNode = (firstChild = newChild.FirstChild);
				if (firstChild != null)
				{
					newChild.RemoveChild(firstChild);
					this.InsertBefore(firstChild, refChild);
					this.InsertAfter(newChild, firstChild);
				}
				return xmlNode;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)refChild;
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			if (xmlLinkedNode2 == this.FirstChild)
			{
				xmlLinkedNode.next = xmlLinkedNode2;
				this.LastNode.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode.IsText && xmlLinkedNode2.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
				}
			}
			else
			{
				XmlLinkedNode xmlLinkedNode3 = (XmlLinkedNode)xmlLinkedNode2.PreviousSibling;
				xmlLinkedNode.next = xmlLinkedNode2;
				xmlLinkedNode3.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode3.IsText)
				{
					if (xmlLinkedNode.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode3, xmlLinkedNode);
						if (xmlLinkedNode2.IsText)
						{
							XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
						}
					}
					else if (xmlLinkedNode2.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode3, xmlLinkedNode2);
					}
				}
				else if (xmlLinkedNode.IsText && xmlLinkedNode2.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, xmlLinkedNode2);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		/// <summary>Inserts the specified node immediately after the specified reference node.</summary>
		/// <returns>The node being inserted.</returns>
		/// <param name="newChild">The XmlNode to insert. </param>
		/// <param name="refChild">The XmlNode that is the reference node. The <paramref name="newNode" /> is placed after the <paramref name="refNode" />. </param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only. </exception>
		// Token: 0x06000C84 RID: 3204 RVA: 0x00040084 File Offset: 0x0003E284
		public virtual XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Cannot insert a node or any ancestor of that node as a child of itself."));
			}
			if (refChild == null)
			{
				return this.PrependChild(newChild);
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes."));
			}
			if (refChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("The reference node is not a child of this node."));
			}
			if (newChild == refChild)
			{
				return newChild;
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			XmlDocument ownerDocument2 = this.OwnerDocument;
			if (ownerDocument != null && ownerDocument != ownerDocument2 && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("The node to be inserted is from a different document context."));
			}
			if (!this.CanInsertAfter(newChild, refChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode xmlNode = refChild;
				XmlNode firstChild = newChild.FirstChild;
				XmlNode nextSibling;
				for (XmlNode xmlNode2 = firstChild; xmlNode2 != null; xmlNode2 = nextSibling)
				{
					nextSibling = xmlNode2.NextSibling;
					newChild.RemoveChild(xmlNode2);
					this.InsertAfter(xmlNode2, xmlNode);
					xmlNode = xmlNode2;
				}
				return firstChild;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)refChild;
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			if (xmlLinkedNode2 == this.LastNode)
			{
				xmlLinkedNode.next = xmlLinkedNode2.next;
				xmlLinkedNode2.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode2.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
				}
			}
			else
			{
				XmlLinkedNode next = xmlLinkedNode2.next;
				xmlLinkedNode.next = next;
				xmlLinkedNode2.next = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (xmlLinkedNode2.IsText)
				{
					if (xmlLinkedNode.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
						if (next.IsText)
						{
							XmlNode.NestTextNodes(xmlLinkedNode, next);
						}
					}
					else if (next.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode2, next);
					}
				}
				else if (xmlLinkedNode.IsText && next.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode, next);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		/// <summary>Removes specified child node.</summary>
		/// <returns>The node removed.</returns>
		/// <param name="oldChild">The node being removed. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="oldChild" /> is not a child of this node. Or this node is read-only. </exception>
		// Token: 0x06000C85 RID: 3205 RVA: 0x000402B0 File Offset: 0x0003E4B0
		public virtual XmlNode RemoveChild(XmlNode oldChild)
		{
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes, so the node to be removed is not its child."));
			}
			if (oldChild.ParentNode != this)
			{
				throw new ArgumentException(Res.GetString("The node to be removed is not a child of this node."));
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)oldChild;
			string value = xmlLinkedNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(xmlLinkedNode, this, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			XmlLinkedNode lastNode = this.LastNode;
			if (xmlLinkedNode == this.FirstChild)
			{
				if (xmlLinkedNode == lastNode)
				{
					this.LastNode = null;
					xmlLinkedNode.next = null;
					xmlLinkedNode.SetParent(null);
				}
				else
				{
					XmlLinkedNode next = xmlLinkedNode.next;
					if (next.IsText && xmlLinkedNode.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode, next);
					}
					lastNode.next = next;
					xmlLinkedNode.next = null;
					xmlLinkedNode.SetParent(null);
				}
			}
			else if (xmlLinkedNode == lastNode)
			{
				XmlLinkedNode xmlLinkedNode2 = (XmlLinkedNode)xmlLinkedNode.PreviousSibling;
				xmlLinkedNode2.next = xmlLinkedNode.next;
				this.LastNode = xmlLinkedNode2;
				xmlLinkedNode.next = null;
				xmlLinkedNode.SetParent(null);
			}
			else
			{
				XmlLinkedNode xmlLinkedNode3 = (XmlLinkedNode)xmlLinkedNode.PreviousSibling;
				XmlLinkedNode next2 = xmlLinkedNode.next;
				if (next2.IsText)
				{
					if (xmlLinkedNode3.IsText)
					{
						XmlNode.NestTextNodes(xmlLinkedNode3, next2);
					}
					else if (xmlLinkedNode.IsText)
					{
						XmlNode.UnnestTextNodes(xmlLinkedNode, next2);
					}
				}
				xmlLinkedNode3.next = next2;
				xmlLinkedNode.next = null;
				xmlLinkedNode.SetParent(null);
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return oldChild;
		}

		/// <summary>Adds the specified node to the beginning of the list of child nodes for this node.</summary>
		/// <returns>The node added.</returns>
		/// <param name="newChild">The node to add. All the contents of the node to be added are moved into the specified location.</param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only. </exception>
		// Token: 0x06000C86 RID: 3206 RVA: 0x00040417 File Offset: 0x0003E617
		public virtual XmlNode PrependChild(XmlNode newChild)
		{
			return this.InsertBefore(newChild, this.FirstChild);
		}

		/// <summary>Adds the specified node to the end of the list of child nodes, of this node.</summary>
		/// <returns>The node added.</returns>
		/// <param name="newChild">The node to add. All the contents of the node to be added are moved into the specified location. </param>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only. </exception>
		// Token: 0x06000C87 RID: 3207 RVA: 0x00040428 File Offset: 0x0003E628
		public virtual XmlNode AppendChild(XmlNode newChild)
		{
			XmlDocument xmlDocument = this.OwnerDocument;
			if (xmlDocument == null)
			{
				xmlDocument = this as XmlDocument;
			}
			if (!this.IsContainer)
			{
				throw new InvalidOperationException(Res.GetString("The current node cannot contain other nodes."));
			}
			if (this == newChild || this.AncestorNode(newChild))
			{
				throw new ArgumentException(Res.GetString("Cannot insert a node or any ancestor of that node as a child of itself."));
			}
			if (newChild.ParentNode != null)
			{
				newChild.ParentNode.RemoveChild(newChild);
			}
			XmlDocument ownerDocument = newChild.OwnerDocument;
			if (ownerDocument != null && ownerDocument != xmlDocument && ownerDocument != this)
			{
				throw new ArgumentException(Res.GetString("The node to be inserted is from a different document context."));
			}
			if (newChild.NodeType == XmlNodeType.DocumentFragment)
			{
				XmlNode firstChild = newChild.FirstChild;
				XmlNode nextSibling;
				for (XmlNode xmlNode = firstChild; xmlNode != null; xmlNode = nextSibling)
				{
					nextSibling = xmlNode.NextSibling;
					newChild.RemoveChild(xmlNode);
					this.AppendChild(xmlNode);
				}
				return firstChild;
			}
			if (!(newChild is XmlLinkedNode) || !this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			if (!this.CanInsertAfter(newChild, this.LastChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			string value = newChild.Value;
			XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(newChild, newChild.ParentNode, this, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.BeforeEvent(eventArgs);
			}
			XmlLinkedNode lastNode = this.LastNode;
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (lastNode == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
			}
			else
			{
				xmlLinkedNode.next = lastNode.next;
				lastNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParent(this);
				if (lastNode.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(lastNode, xmlLinkedNode);
				}
			}
			if (eventArgs != null)
			{
				this.AfterEvent(eventArgs);
			}
			return xmlLinkedNode;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000405D4 File Offset: 0x0003E7D4
		internal virtual XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode lastNode = this.LastNode;
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (lastNode == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				xmlLinkedNode.SetParentForLoad(this);
			}
			else
			{
				xmlLinkedNode.next = lastNode.next;
				lastNode.next = xmlLinkedNode;
				this.LastNode = xmlLinkedNode;
				if (lastNode.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(lastNode, xmlLinkedNode);
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

		// Token: 0x06000C89 RID: 3209 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal virtual bool IsValidChildType(XmlNodeType type)
		{
			return false;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal virtual bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal virtual bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether this node has any child nodes.</summary>
		/// <returns>true if the node has child nodes; otherwise, false.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00040661 File Offset: 0x0003E861
		public virtual bool HasChildNodes
		{
			get
			{
				return this.LastNode != null;
			}
		}

		/// <summary>Creates a duplicate of the node, when overridden in a derived class.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		/// <exception cref="T:System.InvalidOperationException">Calling this method on a node type that cannot be cloned. </exception>
		// Token: 0x06000C8D RID: 3213
		public abstract XmlNode CloneNode(bool deep);

		// Token: 0x06000C8E RID: 3214 RVA: 0x0004066C File Offset: 0x0003E86C
		internal virtual void CopyChildren(XmlDocument doc, XmlNode container, bool deep)
		{
			for (XmlNode xmlNode = container.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.AppendChildForLoad(xmlNode.CloneNode(deep), doc);
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If there is no namespace URI, this property returns String.Empty.</returns>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00015451 File Offset: 0x00013651
		public virtual string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. For example, Prefix is bk for the element &lt;bk:book&gt;. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The specified prefix is "xml" and the namespaceURI of this node is different from "http://www.w3.org/XML/1998/namespace".This node is an attribute and the specified prefix is "xmlns" and the namespaceURI of this node is different from "http://www.w3.org/2000/xmlns/ ".This node is an attribute and the qualifiedName of this node is "xmlns". </exception>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00015451 File Offset: 0x00013651
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0000A558 File Offset: 0x00008758
		public virtual string Prefix
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		/// <summary>Gets the local name of the node, when overridden in a derived class.</summary>
		/// <returns>The name of the node with the prefix removed. For example, LocalName is book for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node: Type Name Attribute The local name of the attribute. CDATA #cdata-section Comment #comment Document #document DocumentFragment #document-fragment DocumentType The document type name. Element The local name of the element. Entity The name of the entity. EntityReference The name of the entity referenced. Notation The notation name. ProcessingInstruction The target of the processing instruction. Text #text Whitespace #whitespace SignificantWhitespace #significant-whitespace XmlDeclaration #xml-declaration </returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C92 RID: 3218
		public abstract string LocalName { get; }

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0004069B File Offset: 0x0003E89B
		public virtual bool IsReadOnly
		{
			get
			{
				XmlDocument ownerDocument = this.OwnerDocument;
				return XmlNode.HasReadOnlyParent(this);
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000406AC File Offset: 0x0003E8AC
		internal static bool HasReadOnlyParent(XmlNode n)
		{
			while (n != null)
			{
				XmlNodeType nodeType = n.NodeType;
				if (nodeType != XmlNodeType.Attribute)
				{
					if (nodeType - XmlNodeType.EntityReference <= 1)
					{
						return true;
					}
					n = n.ParentNode;
				}
				else
				{
					n = ((XmlAttribute)n).OwnerElement;
				}
			}
			return false;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.XmlNode.Clone" />.</summary>
		/// <returns>A copy of the node from which it is called.</returns>
		// Token: 0x06000C95 RID: 3221 RVA: 0x000406E9 File Offset: 0x0003E8E9
		object ICloneable.Clone()
		{
			return this.CloneNode(true);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.XmlNode.GetEnumerator" />.</summary>
		/// <returns>Returns an enumerator for the collection.</returns>
		// Token: 0x06000C96 RID: 3222 RVA: 0x000406F2 File Offset: 0x0003E8F2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		/// <summary>Get an enumerator that iterates through the child nodes in the current node.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the child nodes in the current node.</returns>
		// Token: 0x06000C97 RID: 3223 RVA: 0x000406F2 File Offset: 0x0003E8F2
		public IEnumerator GetEnumerator()
		{
			return new XmlChildEnumerator(this);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000406FC File Offset: 0x0003E8FC
		private void AppendChildText(StringBuilder builder)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.FirstChild == null)
				{
					if (xmlNode.NodeType == XmlNodeType.Text || xmlNode.NodeType == XmlNodeType.CDATA || xmlNode.NodeType == XmlNodeType.Whitespace || xmlNode.NodeType == XmlNodeType.SignificantWhitespace)
					{
						builder.Append(xmlNode.InnerText);
					}
				}
				else
				{
					xmlNode.AppendChildText(builder);
				}
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its child nodes.</summary>
		/// <returns>The concatenated values of the node and all its child nodes.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00040760 File Offset: 0x0003E960
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x000407B4 File Offset: 0x0003E9B4
		public virtual string InnerText
		{
			get
			{
				XmlNode firstChild = this.FirstChild;
				if (firstChild == null)
				{
					return string.Empty;
				}
				if (firstChild.NextSibling == null)
				{
					XmlNodeType nodeType = firstChild.NodeType;
					if (nodeType - XmlNodeType.Text <= 1 || nodeType - XmlNodeType.Whitespace <= 1)
					{
						return firstChild.Value;
					}
				}
				StringBuilder stringBuilder = new StringBuilder();
				this.AppendChildText(stringBuilder);
				return stringBuilder.ToString();
			}
			set
			{
				XmlNode firstChild = this.FirstChild;
				if (firstChild != null && firstChild.NextSibling == null && firstChild.NodeType == XmlNodeType.Text)
				{
					firstChild.Value = value;
					return;
				}
				this.RemoveAll();
				this.AppendChild(this.OwnerDocument.CreateTextNode(value));
			}
		}

		/// <summary>Gets the markup containing this node and all its child nodes.</summary>
		/// <returns>The markup containing this node and all its child nodes.NoteOuterXml does not return default attributes.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00040800 File Offset: 0x0003EA00
		public virtual string OuterXml
		{
			get
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(stringWriter);
				try
				{
					this.WriteTo(xmlDOMTextWriter);
				}
				finally
				{
					xmlDOMTextWriter.Close();
				}
				return stringWriter.ToString();
			}
		}

		/// <summary>Gets or sets the markup representing only the child nodes of this node.</summary>
		/// <returns>The markup of the child nodes of this node.NoteInnerXml does not return default attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property on a node that cannot have child nodes. </exception>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170002EE RID: 750
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0003D90F File Offset: 0x0003BB0F
		public virtual string InnerXml
		{
			set
			{
				throw new InvalidOperationException(Res.GetString("Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children."));
			}
		}

		/// <summary>Gets the post schema validation infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the post schema validation infoset of this node.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00040848 File Offset: 0x0003EA48
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The location from which the node was loaded or String.Empty if the node has no base URI.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00040850 File Offset: 0x0003EA50
		public virtual string BaseURI
		{
			get
			{
				for (XmlNode xmlNode = this.ParentNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
				{
					XmlNodeType nodeType = xmlNode.NodeType;
					if (nodeType == XmlNodeType.EntityReference)
					{
						return ((XmlEntityReference)xmlNode).ChildBaseURI;
					}
					if (nodeType == XmlNodeType.Document || nodeType == XmlNodeType.Entity || nodeType == XmlNodeType.Attribute)
					{
						return xmlNode.BaseURI;
					}
				}
				return string.Empty;
			}
		}

		/// <summary>Saves the current node to the specified <see cref="T:System.Xml.XmlWriter" />, when overridden in a derived class.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C9F RID: 3231
		public abstract void WriteTo(XmlWriter w);

		/// <summary>Saves all the child nodes of the node to the specified <see cref="T:System.Xml.XmlWriter" />, when overridden in a derived class.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000CA0 RID: 3232
		public abstract void WriteContentTo(XmlWriter w);

		/// <summary>Removes all the child nodes and/or attributes of the current node.</summary>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x000408A0 File Offset: 0x0003EAA0
		public virtual void RemoveAll()
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				this.RemoveChild(xmlNode);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000408C8 File Offset: 0x0003EAC8
		internal XmlDocument Document
		{
			get
			{
				if (this.NodeType == XmlNodeType.Document)
				{
					return (XmlDocument)this;
				}
				return this.OwnerDocument;
			}
		}

		/// <summary>Looks up the closest xmlns declaration for the given namespace URI that is in scope for the current node and returns the prefix defined in that declaration.</summary>
		/// <returns>The prefix for the specified namespace URI.</returns>
		/// <param name="namespaceURI">The namespace URI whose prefix you want to find. </param>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x000408E4 File Offset: 0x0003EAE4
		public virtual string GetPrefixOfNamespace(string namespaceURI)
		{
			string prefixOfNamespaceStrict = this.GetPrefixOfNamespaceStrict(namespaceURI);
			if (prefixOfNamespaceStrict == null)
			{
				return string.Empty;
			}
			return prefixOfNamespaceStrict;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00040904 File Offset: 0x0003EB04
		internal string GetPrefixOfNamespaceStrict(string namespaceURI)
		{
			XmlDocument document = this.Document;
			if (document != null)
			{
				namespaceURI = document.NameTable.Add(namespaceURI);
				XmlNode xmlNode = this;
				while (xmlNode != null)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement xmlElement = (XmlElement)xmlNode;
						if (xmlElement.HasAttributes)
						{
							XmlAttributeCollection attributes = xmlElement.Attributes;
							for (int i = 0; i < attributes.Count; i++)
							{
								XmlAttribute xmlAttribute = attributes[i];
								if (xmlAttribute.Prefix.Length == 0)
								{
									if (Ref.Equal(xmlAttribute.LocalName, document.strXmlns) && xmlAttribute.Value == namespaceURI)
									{
										return string.Empty;
									}
								}
								else if (Ref.Equal(xmlAttribute.Prefix, document.strXmlns))
								{
									if (xmlAttribute.Value == namespaceURI)
									{
										return xmlAttribute.LocalName;
									}
								}
								else if (Ref.Equal(xmlAttribute.NamespaceURI, namespaceURI))
								{
									return xmlAttribute.Prefix;
								}
							}
						}
						if (Ref.Equal(xmlNode.NamespaceURI, namespaceURI))
						{
							return xmlNode.Prefix;
						}
						xmlNode = xmlNode.ParentNode;
					}
					else if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (Ref.Equal(document.strReservedXml, namespaceURI))
				{
					return document.strXml;
				}
				if (Ref.Equal(document.strReservedXmlns, namespaceURI))
				{
					return document.strXmlns;
				}
			}
			return null;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00040A63 File Offset: 0x0003EC63
		internal virtual void SetParent(XmlNode node)
		{
			if (node == null)
			{
				this.parentNode = this.OwnerDocument;
				return;
			}
			this.parentNode = node;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003AA52 File Offset: 0x00038C52
		internal virtual void SetParentForLoad(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00040A7C File Offset: 0x0003EC7C
		internal static void SplitName(string name, out string prefix, out string localName)
		{
			int num = name.IndexOf(':');
			if (-1 == num || num == 0 || name.Length - 1 == num)
			{
				prefix = string.Empty;
				localName = name;
				return;
			}
			prefix = name.Substring(0, num);
			localName = name.Substring(num + 1);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00040AC4 File Offset: 0x0003ECC4
		internal virtual XmlNode FindChild(XmlNodeType type)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == type)
				{
					return xmlNode;
				}
			}
			return null;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00040AF0 File Offset: 0x0003ECF0
		internal virtual XmlNodeChangedEventArgs GetEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			if (ownerDocument == null)
			{
				return null;
			}
			if (!ownerDocument.IsLoading && ((newParent != null && newParent.IsReadOnly) || (oldParent != null && oldParent.IsReadOnly)))
			{
				throw new InvalidOperationException(Res.GetString("This node is read-only. It cannot be modified."));
			}
			return ownerDocument.GetEventArgs(node, oldParent, newParent, oldValue, newValue, action);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00040B46 File Offset: 0x0003ED46
		internal virtual void BeforeEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.BeforeEvent(args);
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00040B57 File Offset: 0x0003ED57
		internal virtual void AfterEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				this.OwnerDocument.AfterEvent(args);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00040B68 File Offset: 0x0003ED68
		internal virtual XmlSpace XmlSpace
		{
			get
			{
				XmlNode xmlNode = this;
				for (;;)
				{
					XmlElement xmlElement = xmlNode as XmlElement;
					if (xmlElement != null && xmlElement.HasAttribute("xml:space"))
					{
						string text = XmlConvert.TrimString(xmlElement.GetAttribute("xml:space"));
						if (text == "default")
						{
							break;
						}
						if (text == "preserve")
						{
							return XmlSpace.Preserve;
						}
					}
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						return XmlSpace.None;
					}
				}
				return XmlSpace.Default;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00040BCC File Offset: 0x0003EDCC
		internal virtual string XmlLang
		{
			get
			{
				XmlNode xmlNode = this;
				XmlElement xmlElement;
				for (;;)
				{
					xmlElement = xmlNode as XmlElement;
					if (xmlElement != null && xmlElement.HasAttribute("xml:lang"))
					{
						break;
					}
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						goto Block_3;
					}
				}
				return xmlElement.GetAttribute("xml:lang");
				Block_3:
				return string.Empty;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		internal virtual XPathNodeType XPNodeType
		{
			get
			{
				return (XPathNodeType)(-1);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00015451 File Offset: 0x00013651
		internal virtual string XPLocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal virtual bool IsText
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00014C6C File Offset: 0x00012E6C
		public virtual XmlNode PreviousText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00040C0F File Offset: 0x0003EE0F
		internal static void NestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00040C18 File Offset: 0x0003EE18
		internal static void UnnestTextNodes(XmlNode prevNode, XmlNode nextNode)
		{
			nextNode.parentNode = prevNode.ParentNode;
		}

		// Token: 0x0400065C RID: 1628
		internal XmlNode parentNode;
	}
}
