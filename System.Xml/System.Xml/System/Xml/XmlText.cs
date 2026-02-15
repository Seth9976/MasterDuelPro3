using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the text content of an element or attribute.</summary>
	// Token: 0x020000FB RID: 251
	public class XmlText : XmlCharacterData
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x00042FB2 File Offset: 0x000411B2
		internal XmlText(string strData)
			: this(strData, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlText" /> class.</summary>
		/// <param name="strData">The content of the node; see the <see cref="P:System.Xml.XmlText.Value" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000D51 RID: 3409 RVA: 0x0003B0A0 File Offset: 0x000392A0
		protected internal XmlText(string strData, XmlDocument doc)
			: base(strData, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00042FBC File Offset: 0x000411BC
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For text nodes, this property returns #text.</returns>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00042FBC File Offset: 0x000411BC
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strTextName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For text nodes, this value is XmlNodeType.Text.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00042FCC File Offset: 0x000411CC
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return null;
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

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x06000D56 RID: 3414 RVA: 0x00043020 File Offset: 0x00041220
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateTextNode(this.Data);
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The content of the text node.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0003B158 File Offset: 0x00039358
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00043034 File Offset: 0x00041234
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
				XmlNode parentNode = this.parentNode;
				if (parentNode != null && parentNode.NodeType == XmlNodeType.Attribute)
				{
					XmlUnspecifiedAttribute xmlUnspecifiedAttribute = parentNode as XmlUnspecifiedAttribute;
					if (xmlUnspecifiedAttribute != null && !xmlUnspecifiedAttribute.Specified)
					{
						xmlUnspecifiedAttribute.SetSpecified(true);
					}
				}
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D59 RID: 3417 RVA: 0x00042F8A File Offset: 0x0004118A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. XmlText nodes do not have children, so this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D5A RID: 3418 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0003B0B7 File Offset: 0x000392B7
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003B131 File Offset: 0x00039331
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
