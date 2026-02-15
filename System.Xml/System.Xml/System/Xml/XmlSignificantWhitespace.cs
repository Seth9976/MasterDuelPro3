using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space between markup in a mixed content node or white space within an xml:space= 'preserve' scope. This is also referred to as significant white space.</summary>
	// Token: 0x020000FA RID: 250
	public class XmlSignificantWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlSignificantWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000D43 RID: 3395 RVA: 0x00042EBD File Offset: 0x000410BD
		protected internal XmlSignificantWhitespace(string strData, XmlDocument doc)
			: base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this property returns #significant-whitespace.</returns>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00042EE8 File Offset: 0x000410E8
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this property returns #significant-whitespace.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00042EE8 File Offset: 0x000410E8
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlSignificantWhitespace nodes, this value is XmlNodeType.SignificantWhitespace.</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00042EF5 File Offset: 0x000410F5
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.SignificantWhitespace;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00042EFC File Offset: 0x000410FC
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

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. For significant white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x06000D48 RID: 3400 RVA: 0x00042F55 File Offset: 0x00041155
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateSignificantWhitespace(this.Data);
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting Value to invalid white space characters. </exception>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0003B158 File Offset: 0x00039358
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00042F68 File Offset: 0x00041168
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

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D4B RID: 3403 RVA: 0x00042F8A File Offset: 0x0004118A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D4C RID: 3404 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00042F98 File Offset: 0x00041198
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType xpathNodeType = XPathNodeType.SignificantWhitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref xpathNodeType);
				return xpathNodeType;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0003B131 File Offset: 0x00039331
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
