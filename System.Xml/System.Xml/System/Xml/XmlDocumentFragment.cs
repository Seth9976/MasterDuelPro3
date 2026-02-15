using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a lightweight object that is useful for tree insert operations.</summary>
	// Token: 0x020000E2 RID: 226
	public class XmlDocumentFragment : XmlNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentFragment" /> class.</summary>
		/// <param name="ownerDocument">The XML document that is the source of the fragment.</param>
		// Token: 0x06000B9F RID: 2975 RVA: 0x0003CDCE File Offset: 0x0003AFCE
		protected internal XmlDocumentFragment(XmlDocument ownerDocument)
		{
			if (ownerDocument == null)
			{
				throw new ArgumentException(Res.GetString("Cannot create a node without an owner document."));
			}
			this.parentNode = ownerDocument;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlDocumentFragment, the name is #document-fragment.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlDocumentFragment nodes, the local name is #document-fragment.</returns>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlDocumentFragment nodes, this value is XmlNodeType.DocumentFragment.</returns>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0003CDFD File Offset: 0x0003AFFD
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentFragment;
			}
		}

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The parent of this node.For XmlDocumentFragment nodes, this property is always null.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The XmlDocument to which this node belongs.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0003CE01 File Offset: 0x0003B001
		public override XmlDocument OwnerDocument
		{
			get
			{
				return (XmlDocument)this.parentNode;
			}
		}

		/// <summary>Gets or sets the markup representing the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x17000289 RID: 649
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0003CE0E File Offset: 0x0003B00E
		public override string InnerXml
		{
			set
			{
				this.RemoveAll();
				new XmlLoader().ParsePartialContent(this, value, XmlNodeType.Element);
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0003CE24 File Offset: 0x0003B024
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlDocumentFragment xmlDocumentFragment = ownerDocument.CreateDocumentFragment();
			if (deep)
			{
				xmlDocumentFragment.CopyChildren(ownerDocument, this, deep);
			}
			return xmlDocumentFragment;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0003CE4C File Offset: 0x0003B04C
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0003CE54 File Offset: 0x0003B054
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

		// Token: 0x06000BAA RID: 2986 RVA: 0x0003CE60 File Offset: 0x0003B060
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
			case XmlNodeType.XmlDeclaration:
			{
				XmlNode firstChild = this.FirstChild;
				return firstChild == null || firstChild.NodeType != XmlNodeType.XmlDeclaration;
			}
			}
			return false;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0003CED6 File Offset: 0x0003B0D6
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || (refChild == null && this.LastNode == null);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0003CEF2 File Offset: 0x0003B0F2
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || refChild == null || refChild == this.FirstChild;
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000BAD RID: 2989 RVA: 0x0003C915 File Offset: 0x0003AB15
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000BAE RID: 2990 RVA: 0x0003CF10 File Offset: 0x0003B110
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				((XmlNode)obj).WriteTo(w);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x0400062D RID: 1581
		private XmlLinkedNode lastChild;
	}
}
