using System;
using System.Collections;

namespace System.Xml
{
	/// <summary>Represents a collection of nodes that can be accessed by name or index.</summary>
	// Token: 0x020000EE RID: 238
	public class XmlNamedNodeMap : IEnumerable
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x0003F72F File Offset: 0x0003D92F
		internal XmlNamedNodeMap(XmlNode parent)
		{
			this.parent = parent;
		}

		/// <summary>Retrieves an <see cref="T:System.Xml.XmlNode" /> specified by name.</summary>
		/// <returns>An XmlNode with the specified name or null if a matching node is not found.</returns>
		/// <param name="name">The qualified name of the node to retrieve. It is matched against the <see cref="P:System.Xml.XmlNode.Name" /> property of the matching node.</param>
		// Token: 0x06000C58 RID: 3160 RVA: 0x0003F740 File Offset: 0x0003D940
		public virtual XmlNode GetNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return (XmlNode)this.nodes[num];
			}
			return null;
		}

		/// <summary>Adds an <see cref="T:System.Xml.XmlNode" /> using its <see cref="P:System.Xml.XmlNode.Name" /> property.</summary>
		/// <returns>If the <paramref name="node" /> replaces an existing node with the same name, the old node is returned; otherwise, null is returned.</returns>
		/// <param name="node">An XmlNode to store in the XmlNamedNodeMap. If a node with that name is already present in the map, it is replaced by the new one.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> was created from a different <see cref="T:System.Xml.XmlDocument" /> than the one that created the XmlNamedNodeMap; or the XmlNamedNodeMap is read-only.</exception>
		// Token: 0x06000C59 RID: 3161 RVA: 0x0003F76C File Offset: 0x0003D96C
		public virtual XmlNode SetNamedItem(XmlNode node)
		{
			if (node == null)
			{
				return null;
			}
			int num = this.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				this.AddNode(node);
				return null;
			}
			return this.ReplaceNodeAt(num, node);
		}

		/// <summary>Gets the number of nodes in the XmlNamedNodeMap.</summary>
		/// <returns>The number of nodes.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0003F7A7 File Offset: 0x0003D9A7
		public virtual int Count
		{
			get
			{
				return this.nodes.Count;
			}
		}

		/// <summary>Provides support for the "foreach" style iteration over the collection of nodes in the XmlNamedNodeMap.</summary>
		/// <returns>An enumerator object.</returns>
		// Token: 0x06000C5B RID: 3163 RVA: 0x0003F7B4 File Offset: 0x0003D9B4
		public virtual IEnumerator GetEnumerator()
		{
			return this.nodes.GetEnumerator();
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0003F7C4 File Offset: 0x0003D9C4
		internal int FindNodeOffset(string name)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (name == xmlNode.Name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0003F808 File Offset: 0x0003DA08
		internal int FindNodeOffset(string localName, string namespaceURI)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (xmlNode.LocalName == localName && xmlNode.NamespaceURI == namespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0003F85C File Offset: 0x0003DA5C
		internal virtual XmlNode AddNode(XmlNode node)
		{
			XmlNode xmlNode;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				xmlNode = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				xmlNode = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, xmlNode, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0003F8DC File Offset: 0x0003DADC
		internal virtual XmlNode AddNodeForLoad(XmlNode node, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(node, this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return node;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0003F924 File Offset: 0x0003DB24
		internal virtual XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = (XmlNode)this.nodes[i];
			string value = xmlNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(xmlNode, this.parent, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.RemoveAt(i);
			xmlNode.SetParent(null);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return xmlNode;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0003F993 File Offset: 0x0003DB93
		internal XmlNode ReplaceNodeAt(int i, XmlNode node)
		{
			XmlNode xmlNode = this.RemoveNodeAt(i);
			this.InsertNodeAt(i, node);
			return xmlNode;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0003F9A8 File Offset: 0x0003DBA8
		internal virtual XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode xmlNode;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				xmlNode = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				xmlNode = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, xmlNode, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Insert(i, node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x04000657 RID: 1623
		internal XmlNode parent;

		// Token: 0x04000658 RID: 1624
		internal XmlNamedNodeMap.SmallXmlNodeList nodes;

		// Token: 0x020000EF RID: 239
		internal struct SmallXmlNodeList
		{
			// Token: 0x170002D7 RID: 727
			// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0003FA2C File Offset: 0x0003DC2C
			public int Count
			{
				get
				{
					if (this.field == null)
					{
						return 0;
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList.Count;
					}
					return 1;
				}
			}

			// Token: 0x170002D8 RID: 728
			public object this[int index]
			{
				get
				{
					if (this.field == null)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList[index];
					}
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.field;
				}
			}

			// Token: 0x06000C65 RID: 3173 RVA: 0x0003FAA8 File Offset: 0x0003DCA8
			public void Add(object value)
			{
				if (this.field == null)
				{
					if (value == null)
					{
						this.field = new ArrayList { null };
						return;
					}
					this.field = value;
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Add(value);
						return;
					}
					this.field = new ArrayList { this.field, value };
					return;
				}
			}

			// Token: 0x06000C66 RID: 3174 RVA: 0x0003FB18 File Offset: 0x0003DD18
			public void RemoveAt(int index)
			{
				if (this.field == null)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					arrayList.RemoveAt(index);
					return;
				}
				if (index != 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.field = null;
			}

			// Token: 0x06000C67 RID: 3175 RVA: 0x0003FB64 File Offset: 0x0003DD64
			public void Insert(int index, object value)
			{
				if (this.field == null)
				{
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.Add(value);
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Insert(index, value);
						return;
					}
					if (index == 0)
					{
						this.field = new ArrayList { value, this.field };
						return;
					}
					if (index == 1)
					{
						this.field = new ArrayList { this.field, value };
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x06000C68 RID: 3176 RVA: 0x0003FC00 File Offset: 0x0003DE00
			public IEnumerator GetEnumerator()
			{
				if (this.field == null)
				{
					return XmlDocument.EmptyEnumerator;
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					return arrayList.GetEnumerator();
				}
				return new XmlNamedNodeMap.SmallXmlNodeList.SingleObjectEnumerator(this.field);
			}

			// Token: 0x04000659 RID: 1625
			private object field;

			// Token: 0x020000F0 RID: 240
			private class SingleObjectEnumerator : IEnumerator
			{
				// Token: 0x06000C69 RID: 3177 RVA: 0x0003FC3C File Offset: 0x0003DE3C
				public SingleObjectEnumerator(object value)
				{
					this.loneValue = value;
				}

				// Token: 0x170002D9 RID: 729
				// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0003FC52 File Offset: 0x0003DE52
				public object Current
				{
					get
					{
						if (this.position != 0)
						{
							throw new InvalidOperationException();
						}
						return this.loneValue;
					}
				}

				// Token: 0x06000C6B RID: 3179 RVA: 0x0003FC68 File Offset: 0x0003DE68
				public bool MoveNext()
				{
					if (this.position < 0)
					{
						this.position = 0;
						return true;
					}
					this.position = 1;
					return false;
				}

				// Token: 0x06000C6C RID: 3180 RVA: 0x0003FC84 File Offset: 0x0003DE84
				public void Reset()
				{
					this.position = -1;
				}

				// Token: 0x0400065A RID: 1626
				private object loneValue;

				// Token: 0x0400065B RID: 1627
				private int position = -1;
			}
		}
	}
}
