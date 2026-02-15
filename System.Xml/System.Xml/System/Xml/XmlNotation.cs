using System;

namespace System.Xml
{
	/// <summary>Represents a notation declaration, such as &lt;!NOTATION... &gt;.</summary>
	// Token: 0x020000F8 RID: 248
	public class XmlNotation : XmlNode
	{
		// Token: 0x06000D2B RID: 3371 RVA: 0x00042DDB File Offset: 0x00040FDB
		internal XmlNotation(string name, string publicId, string systemId, XmlDocument doc)
			: base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
		}

		/// <summary>Gets the name of the current node.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00042E06 File Offset: 0x00041006
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the name of the current node without the namespace prefix.</summary>
		/// <returns>For XmlNotation nodes, this property returns the name of the notation.</returns>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00042E06 File Offset: 0x00041006
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For XmlNotation nodes, the value is XmlNodeType.Notation.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0000A2FC File Offset: 0x000084FC
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Notation;
			}
		}

		/// <summary>Creates a duplicate of this node. Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</summary>
		/// <returns>Returns a <see cref="T:System.Xml.XmlNode" /> copy of the node from which the method is called.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself.</param>
		/// <exception cref="T:System.InvalidOperationException">Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</exception>
		// Token: 0x06000D2F RID: 3375 RVA: 0x0003D881 File Offset: 0x0003BA81
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("'Entity' and 'Notation' nodes cannot be cloned."));
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>true if the node is read-only; otherwise false.Because XmlNotation nodes are read-only, this property always returns true.</returns>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the markup representing this node and all its children.</summary>
		/// <returns>For XmlNotation nodes, String.Empty is returned.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00015451 File Offset: 0x00013651
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the markup representing the children of this node.</summary>
		/// <returns>For XmlNotation nodes, String.Empty is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x17000328 RID: 808
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x0003D90F File Offset: 0x0003BB0F
		public override string InnerXml
		{
			set
			{
				throw new InvalidOperationException(Res.GetString("Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children."));
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on XmlNotation nodes.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D33 RID: 3379 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteTo(XmlWriter w)
		{
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on XmlNotation nodes.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D34 RID: 3380 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x0400067F RID: 1663
		private string publicId;

		// Token: 0x04000680 RID: 1664
		private string systemId;

		// Token: 0x04000681 RID: 1665
		private string name;
	}
}
