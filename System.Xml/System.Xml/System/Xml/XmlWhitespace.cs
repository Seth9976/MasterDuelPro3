using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space in element content.</summary>
	// Token: 0x020000FD RID: 253
	public class XmlWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000D68 RID: 3432 RVA: 0x00042EBD File Offset: 0x000410BD
		protected internal XmlWhitespace(string strData, XmlDocument doc)
			: base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlWhitespace nodes, this property returns #whitespace.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00043139 File Offset: 0x00041339
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlWhitespace nodes, this property returns #whitespace.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00043139 File Offset: 0x00041339
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>For XmlWhitespace nodes, the value is <see cref="F:System.Xml.XmlNodeType.Whitespace" />.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00043146 File Offset: 0x00041346
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Whitespace;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0004314C File Offset: 0x0004134C
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return base.ParentNode;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return this.parentNode;
					}
				}
				XmlNode xmlNode = this.parentNode.parentNode;
				while (xmlNode.IsText)
				{
					xmlNode = xmlNode.parentNode;
				}
				return xmlNode;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting <see cref="P:System.Xml.XmlWhitespace.Value" /> to invalid white space characters. </exception>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0003B158 File Offset: 0x00039358
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00042F68 File Offset: 0x00041168
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				if (base.CheckOnData(value))
				{
					this.Data = value;
					return;
				}
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x06000D6F RID: 3439 RVA: 0x000431A5 File Offset: 0x000413A5
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateWhitespace(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save.</param>
		// Token: 0x06000D70 RID: 3440 RVA: 0x000431B8 File Offset: 0x000413B8
		public override void WriteTo(XmlWriter w)
		{
			w.WriteWhitespace(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save. </param>
		// Token: 0x06000D71 RID: 3441 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000431C8 File Offset: 0x000413C8
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType xpathNodeType = XPathNodeType.Whitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref xpathNodeType);
				return xpathNodeType;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0003B131 File Offset: 0x00039331
		public override XmlNode PreviousText
		{
			get
			{
				if (this.parentNode.IsText)
				{
					return this.parentNode;
				}
				return null;
			}
		}
	}
}
