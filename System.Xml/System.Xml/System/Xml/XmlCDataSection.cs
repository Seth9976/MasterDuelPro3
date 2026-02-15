using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a CDATA section.</summary>
	// Token: 0x020000DB RID: 219
	public class XmlCDataSection : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCDataSection" /> class.</summary>
		/// <param name="data">
		///   <see cref="T:System.String" /> that contains character data.</param>
		/// <param name="doc">
		///   <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x06000AFE RID: 2814 RVA: 0x0003B0A0 File Offset: 0x000392A0
		protected internal XmlCDataSection(string data, XmlDocument doc)
			: base(data, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For CDATA nodes, the name is #cdata-section.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0003B0AA File Offset: 0x000392AA
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For CDATA nodes, the local name is #cdata-section.</returns>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0003B0AA File Offset: 0x000392AA
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For CDATA nodes, the value is XmlNodeType.CDATA.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0003B0B7 File Offset: 0x000392B7
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0003B0BC File Offset: 0x000392BC
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
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because CDATA nodes do not have children, regardless of the parameter setting, the cloned node will always include the data content. </param>
		// Token: 0x06000B03 RID: 2819 RVA: 0x0003B110 File Offset: 0x00039310
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateCDataSection(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B04 RID: 2820 RVA: 0x0003B123 File Offset: 0x00039323
		public override void WriteTo(XmlWriter w)
		{
			w.WriteCData(this.Data);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B05 RID: 2821 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0003B0B7 File Offset: 0x000392B7
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0003B131 File Offset: 0x00039331
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
