using System;

namespace System.Xml
{
	/// <summary>Represents an entity declaration, such as &lt;!ENTITY... &gt;.</summary>
	// Token: 0x020000E6 RID: 230
	public class XmlEntity : XmlNode
	{
		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003D834 File Offset: 0x0003BA34
		internal XmlEntity(string name, string strdata, string publicId, string systemId, string notationName, XmlDocument doc)
			: base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
			this.notationName = notationName;
			this.unparsedReplacementStr = strdata;
			this.childrenFoliating = false;
		}

		/// <summary>Creates a duplicate of this node. Entity nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlEntity" /> object throws an exception.</summary>
		/// <returns>Returns a copy of the <see cref="T:System.Xml.XmlNode" /> from which the method is called.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
		/// <exception cref="T:System.InvalidOperationException">Entity nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlEntity" /> object throws an exception.</exception>
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0003D881 File Offset: 0x0003BA81
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("'Entity' and 'Notation' nodes cannot be cloned."));
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlEntity nodes are read-only, this property always returns true.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the name of the node.</summary>
		/// <returns>The name of the entity.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0003D892 File Offset: 0x0003BA92
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the name of the node without the namespace prefix.</summary>
		/// <returns>For XmlEntity nodes, this property returns the name of the entity.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0003D892 File Offset: 0x0003BA92
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the concatenated values of the entity node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0003D7B6 File Offset: 0x0003B9B6
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0003D89A File Offset: 0x0003BA9A
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("The 'InnerText' of an 'Entity' node is read-only and cannot be set."));
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0003D8AB File Offset: 0x0003BAAB
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x0003D8D5 File Offset: 0x0003BAD5
		internal override XmlLinkedNode LastNode
		{
			get
			{
				if (this.lastChild == null && !this.childrenFoliating)
				{
					this.childrenFoliating = true;
					new XmlLoader().ExpandEntity(this);
				}
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0003D8DE File Offset: 0x0003BADE
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.Element || type == XmlNodeType.ProcessingInstruction || type == XmlNodeType.Comment || type == XmlNodeType.CDATA || type == XmlNodeType.Whitespace || type == XmlNodeType.SignificantWhitespace || type == XmlNodeType.EntityReference;
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>The node type. For XmlEntity nodes, the value is XmlNodeType.Entity.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0003D904 File Offset: 0x0003BB04
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Entity;
			}
		}

		/// <summary>Gets the value of the system identifier on the entity declaration.</summary>
		/// <returns>The system identifier on the entity. If there is no system identifier, null is returned.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0003D907 File Offset: 0x0003BB07
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Gets the markup representing this node and all its children.</summary>
		/// <returns>For XmlEntity nodes, String.Empty is returned.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00015451 File Offset: 0x00013651
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the markup representing the children of this node.</summary>
		/// <returns>For XmlEntity nodes, String.Empty is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x170002B4 RID: 692
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0003D90F File Offset: 0x0003BB0F
		public override string InnerXml
		{
			set
			{
				throw new InvalidOperationException(Res.GetString("Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children."));
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlEntity nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C01 RID: 3073 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteTo(XmlWriter w)
		{
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. For XmlEntity nodes, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000C02 RID: 3074 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0003D920 File Offset: 0x0003BB20
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0003D928 File Offset: 0x0003BB28
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x04000639 RID: 1593
		private string publicId;

		// Token: 0x0400063A RID: 1594
		private string systemId;

		// Token: 0x0400063B RID: 1595
		private string notationName;

		// Token: 0x0400063C RID: 1596
		private string name;

		// Token: 0x0400063D RID: 1597
		private string unparsedReplacementStr;

		// Token: 0x0400063E RID: 1598
		private string baseURI;

		// Token: 0x0400063F RID: 1599
		private XmlLinkedNode lastChild;

		// Token: 0x04000640 RID: 1600
		private bool childrenFoliating;
	}
}
