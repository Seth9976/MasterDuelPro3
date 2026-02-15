using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	/// <summary>Represents a collection of attributes that can be accessed by name or index.</summary>
	// Token: 0x020000DA RID: 218
	public sealed class XmlAttributeCollection : XmlNamedNodeMap, ICollection, IEnumerable
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0003AAF8 File Offset: 0x00038CF8
		internal XmlAttributeCollection(XmlNode parent)
			: base(parent)
		{
		}

		/// <summary>Gets the attribute with the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> at the specified index.</returns>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index being passed in is out of range. </exception>
		// Token: 0x17000245 RID: 581
		[IndexerName("ItemOf")]
		public XmlAttribute this[int i]
		{
			get
			{
				XmlAttribute xmlAttribute;
				try
				{
					xmlAttribute = (XmlAttribute)this.nodes[i];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new IndexOutOfRangeException(Res.GetString("The index being passed in is out of range."));
				}
				return xmlAttribute;
			}
		}

		/// <summary>Gets the attribute with the specified name.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> with the specified name. If the attribute does not exist, this property returns null.</returns>
		/// <param name="name">The qualified name of the attribute. </param>
		// Token: 0x17000246 RID: 582
		[IndexerName("ItemOf")]
		public XmlAttribute this[string name]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(name);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && name == xmlAttribute.Name)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> with the specified local name and namespace URI. If the attribute does not exist, this property returns null.</returns>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		// Token: 0x17000247 RID: 583
		[IndexerName("ItemOf")]
		public XmlAttribute this[string localName, string namespaceURI]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(localName);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && localName == xmlAttribute.LocalName && namespaceURI == xmlAttribute.NamespaceURI)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0003AC04 File Offset: 0x00038E04
		internal int FindNodeOffsetNS(XmlAttribute node)
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.LocalName == node.LocalName && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Adds a <see cref="T:System.Xml.XmlNode" /> using its <see cref="P:System.Xml.XmlNode.Name" /> property </summary>
		/// <returns>If the <paramref name="node" /> replaces an existing node with the same name, the old node is returned; otherwise, the added node is returned.</returns>
		/// <param name="node">An attribute node to store in this collection. The node will later be accessible using the name of the node. If a node with that name is already present in the collection, it is replaced by the new one; otherwise, the node is appended to the end of the collection. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="node" /> was created from a different <see cref="T:System.Xml.XmlDocument" /> than the one that created this collection.This XmlAttributeCollection is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="node" /> is an <see cref="T:System.Xml.XmlAttribute" /> that is already an attribute of another <see cref="T:System.Xml.XmlElement" /> object. To re-use attributes in other elements, you must clone the XmlAttribute objects you want to re-use. </exception>
		// Token: 0x06000AEB RID: 2795 RVA: 0x0003AC70 File Offset: 0x00038E70
		public override XmlNode SetNamedItem(XmlNode node)
		{
			if (node != null && !(node is XmlAttribute))
			{
				throw new ArgumentException(Res.GetString("An 'Attributes' collection can only contain 'Attribute' objects."));
			}
			int num = base.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				return this.InternalAppendAttribute((XmlAttribute)node);
			}
			XmlNode xmlNode = base.RemoveNodeAt(num);
			this.InsertNodeAt(num, node);
			return xmlNode;
		}

		/// <summary>Inserts the specified attribute as the last node in the collection.</summary>
		/// <returns>The XmlAttribute to append to the collection.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="node" /> was created from a document different from the one that created this collection. </exception>
		// Token: 0x06000AEC RID: 2796 RVA: 0x0003ACCC File Offset: 0x00038ECC
		public XmlAttribute Append(XmlAttribute node)
		{
			XmlDocument ownerDocument = node.OwnerDocument;
			if (ownerDocument == null || !ownerDocument.IsLoading)
			{
				if (ownerDocument != null && ownerDocument != this.parent.OwnerDocument)
				{
					throw new ArgumentException(Res.GetString("The named node is from a different document context."));
				}
				if (node.OwnerElement != null)
				{
					this.Detach(node);
				}
				this.AddNode(node);
			}
			else
			{
				base.AddNodeForLoad(node, ownerDocument);
				this.InsertParentIntoElementIdAttrMap(node);
			}
			return node;
		}

		/// <summary>Removes the specified attribute from the collection.</summary>
		/// <returns>The node removed or null if it is not found in the collection.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to remove. </param>
		// Token: 0x06000AED RID: 2797 RVA: 0x0003AD38 File Offset: 0x00038F38
		public XmlAttribute Remove(XmlAttribute node)
		{
			int count = this.nodes.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.nodes[i] == node)
				{
					this.RemoveNodeAt(i);
					return node;
				}
			}
			return null;
		}

		/// <summary>Removes the attribute corresponding to the specified index from the collection.</summary>
		/// <returns>Returns null if there is no attribute at the specified index.</returns>
		/// <param name="i">The index of the node to remove. The first node has index 0. </param>
		// Token: 0x06000AEE RID: 2798 RVA: 0x0003AD77 File Offset: 0x00038F77
		public XmlAttribute RemoveAt(int i)
		{
			if (i < 0 || i >= this.Count)
			{
				return null;
			}
			return (XmlAttribute)this.RemoveNodeAt(i);
		}

		/// <summary>Removes all attributes from the collection.</summary>
		// Token: 0x06000AEF RID: 2799 RVA: 0x0003AD94 File Offset: 0x00038F94
		public void RemoveAll()
		{
			int i = this.Count;
			while (i > 0)
			{
				i--;
				this.RemoveAt(i);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.XmlAttributeCollection.CopyTo(System.Xml.XmlAttribute[],System.Int32)" />.</summary>
		/// <param name="array">The array that is the destination of the objects copied from this collection. </param>
		/// <param name="index">The index in the array where copying begins. </param>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0003ADBC File Offset: 0x00038FBC
		void ICollection.CopyTo(Array array, int index)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array.SetValue(this.nodes[i], index);
				i++;
				index++;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>Returns true if the collection is synchronized.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Returns the <see cref="T:System.Object" /> that is the root of the collection.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00035F33 File Offset: 0x00034133
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System.Collections.ICollection.Count" />.</summary>
		/// <returns>Returns an int that contains the count of the attributes.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0003ADF4 File Offset: 0x00038FF4
		int ICollection.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0003ADFC File Offset: 0x00038FFC
		internal override XmlNode AddNode(XmlNode node)
		{
			this.RemoveDuplicateAttribute((XmlAttribute)node);
			XmlNode xmlNode = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return xmlNode;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003AE1E File Offset: 0x0003901E
		internal override XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode xmlNode = base.InsertNodeAt(i, node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return xmlNode;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003AE34 File Offset: 0x00039034
		internal override XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = base.RemoveNodeAt(i);
			this.RemoveParentFromElementIdAttrMap((XmlAttribute)xmlNode);
			XmlAttribute defaultAttribute = this.parent.OwnerDocument.GetDefaultAttribute((XmlElement)this.parent, xmlNode.Prefix, xmlNode.LocalName, xmlNode.NamespaceURI);
			if (defaultAttribute != null)
			{
				this.InsertNodeAt(i, defaultAttribute);
			}
			return xmlNode;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003AE90 File Offset: 0x00039090
		internal void Detach(XmlAttribute attr)
		{
			attr.OwnerElement.Attributes.Remove(attr);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0003AEA4 File Offset: 0x000390A4
		internal void InsertParentIntoElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.AddElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0003AF30 File Offset: 0x00039130
		internal void RemoveParentFromElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.RemoveElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003AFBC File Offset: 0x000391BC
		internal int RemoveDuplicateAttribute(XmlAttribute attr)
		{
			int num = base.FindNodeOffset(attr.LocalName, attr.NamespaceURI);
			if (num != -1)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[num];
				base.RemoveNodeAt(num);
				this.RemoveParentFromElementIdAttrMap(xmlAttribute);
			}
			return num;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0003B004 File Offset: 0x00039204
		internal bool PrepareParentInElementIdAttrMap(string attrPrefix, string attrLocalName)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
			return idinfoByElement != null && idinfoByElement.Prefix == attrPrefix && idinfoByElement.LocalName == attrLocalName;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0003B058 File Offset: 0x00039258
		internal void ResetParentInElementIdAttrMap(string oldVal, string newVal)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			XmlDocument ownerDocument = this.parent.OwnerDocument;
			ownerDocument.RemoveElementWithId(oldVal, xmlElement);
			ownerDocument.AddElementWithId(newVal, xmlElement);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0003B08B File Offset: 0x0003928B
		internal XmlAttribute InternalAppendAttribute(XmlAttribute node)
		{
			XmlNode xmlNode = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap(node);
			return (XmlAttribute)xmlNode;
		}
	}
}
