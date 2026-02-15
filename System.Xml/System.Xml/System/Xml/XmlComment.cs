using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the content of an XML comment.</summary>
	// Token: 0x020000DF RID: 223
	public class XmlComment : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlComment" /> class.</summary>
		/// <param name="comment">The content of the comment element.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000B1C RID: 2844 RVA: 0x0003B0A0 File Offset: 0x000392A0
		protected internal XmlComment(string comment, XmlDocument doc)
			: base(comment, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For comment nodes, the value is #comment.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0003B3A4 File Offset: 0x000395A4
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For comment nodes, the value is #comment.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0003B3A4 File Offset: 0x000395A4
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For comment nodes, the value is XmlNodeType.Comment.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0003B3B1 File Offset: 0x000395B1
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because comment nodes do not have children, the cloned node always includes the text content, regardless of the parameter setting. </param>
		// Token: 0x06000B20 RID: 2848 RVA: 0x0003B3B4 File Offset: 0x000395B4
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateComment(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B21 RID: 2849 RVA: 0x0003B3C7 File Offset: 0x000395C7
		public override void WriteTo(XmlWriter w)
		{
			w.WriteComment(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because comment nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B22 RID: 2850 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0003B3B1 File Offset: 0x000395B1
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Comment;
			}
		}
	}
}
