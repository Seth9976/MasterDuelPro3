using System;

namespace System.Xml
{
	/// <summary>Represents an entity reference node.</summary>
	// Token: 0x020000E7 RID: 231
	public class XmlEntityReference : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlEntityReference" /> class.</summary>
		/// <param name="name">The name of the entity reference; see the <see cref="P:System.Xml.XmlEntityReference.Name" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000C05 RID: 3077 RVA: 0x0003D934 File Offset: 0x0003BB34
		protected internal XmlEntityReference(string name, XmlDocument doc)
			: base(doc)
		{
			if (!doc.IsLoading && name.Length > 0 && name[0] == '#')
			{
				throw new ArgumentException(Res.GetString("Cannot create an 'EntityReference' node with a name starting with '#'."));
			}
			this.name = doc.NameTable.Add(name);
			doc.fEntRefNodesPresent = true;
		}

		/// <summary>Gets the name of the node.</summary>
		/// <returns>The name of the entity referenced.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0003D98D File Offset: 0x0003BB8D
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlEntityReference nodes, this property returns the name of the entity referenced.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0003D98D File Offset: 0x0003BB8D
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value of the node. For XmlEntityReference nodes, this property returns null.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting the property. </exception>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00014C6C File Offset: 0x00012E6C
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0003D995 File Offset: 0x0003BB95
		public override string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("'EntityReference' nodes have no support for setting value."));
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>The node type. For XmlEntityReference nodes, the value is XmlNodeType.EntityReference.</returns>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0003D9A6 File Offset: 0x0003BBA6
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.EntityReference;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For XmlEntityReference nodes, this method always returns an entity reference node with no children. The replacement text is set when the node is inserted into a parent. </param>
		// Token: 0x06000C0B RID: 3083 RVA: 0x0003D9A9 File Offset: 0x0003BBA9
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateEntityReference(this.name);
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlEntityReference nodes are read-only, this property always returns true.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0003D9BC File Offset: 0x0003BBBC
		internal override void SetParent(XmlNode node)
		{
			base.SetParent(node);
			if (this.LastNode == null && node != null && node != this.OwnerDocument)
			{
				new XmlLoader().ExpandEntityReference(this);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0003D9E4 File Offset: 0x0003BBE4
		internal override void SetParentForLoad(XmlNode node)
		{
			this.SetParent(node);
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0003D9ED File Offset: 0x0003BBED
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0003D9F5 File Offset: 0x0003BBF5
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

		// Token: 0x06000C12 RID: 3090 RVA: 0x0003DA00 File Offset: 0x0003BC00
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

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C13 RID: 3091 RVA: 0x0003DA52 File Offset: 0x0003BC52
		public override void WriteTo(XmlWriter w)
		{
			w.WriteEntityRef(this.name);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C14 RID: 3092 RVA: 0x0003DA60 File Offset: 0x0003BC60
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				((XmlNode)obj).WriteTo(w);
			}
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
		public override string BaseURI
		{
			get
			{
				return this.OwnerDocument.BaseURI;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0003DAC4 File Offset: 0x0003BCC4
		private string ConstructBaseURI(string baseURI, string systemId)
		{
			if (baseURI == null)
			{
				return systemId;
			}
			int num = baseURI.LastIndexOf('/') + 1;
			string text = baseURI;
			if (num > 0 && num < baseURI.Length)
			{
				text = baseURI.Substring(0, num);
			}
			else if (num == 0)
			{
				text += "\\";
			}
			return text + systemId.Replace('\\', '/');
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0003DB1C File Offset: 0x0003BD1C
		internal string ChildBaseURI
		{
			get
			{
				XmlEntity entityNode = this.OwnerDocument.GetEntityNode(this.name);
				if (entityNode == null)
				{
					return string.Empty;
				}
				if (entityNode.SystemId != null && entityNode.SystemId.Length > 0)
				{
					return this.ConstructBaseURI(entityNode.BaseURI, entityNode.SystemId);
				}
				return entityNode.BaseURI;
			}
		}

		// Token: 0x04000641 RID: 1601
		private string name;

		// Token: 0x04000642 RID: 1602
		private XmlLinkedNode lastChild;
	}
}
