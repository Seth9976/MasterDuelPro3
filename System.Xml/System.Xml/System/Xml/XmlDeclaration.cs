using System;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents the XML declaration node &lt;?xml version='1.0'...?&gt;.</summary>
	// Token: 0x020000E0 RID: 224
	public class XmlDeclaration : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDeclaration" /> class.</summary>
		/// <param name="version">The XML version; see the <see cref="P:System.Xml.XmlDeclaration.Version" /> property.</param>
		/// <param name="encoding">The encoding scheme; see the <see cref="P:System.Xml.XmlDeclaration.Encoding" /> property.</param>
		/// <param name="standalone">Indicates whether the XML document depends on an external DTD; see the <see cref="P:System.Xml.XmlDeclaration.Standalone" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000B24 RID: 2852 RVA: 0x0003B3D8 File Offset: 0x000395D8
		protected internal XmlDeclaration(string version, string encoding, string standalone, XmlDocument doc)
			: base(doc)
		{
			if (!this.IsValidXmlVersion(version))
			{
				throw new ArgumentException(Res.GetString("Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\"."));
			}
			if (standalone != null && standalone.Length > 0 && standalone != "yes" && standalone != "no")
			{
				throw new ArgumentException(Res.GetString("Wrong value for the XML declaration standalone attribute of '{0}'.", new object[] { standalone }));
			}
			this.Encoding = encoding;
			this.Standalone = standalone;
			this.Version = version;
		}

		/// <summary>Gets the XML version of the document.</summary>
		/// <returns>The value is always 1.0.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0003B45B File Offset: 0x0003965B
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x0003B463 File Offset: 0x00039663
		public string Version
		{
			get
			{
				return this.version;
			}
			internal set
			{
				this.version = value;
			}
		}

		/// <summary>Gets or sets the encoding level of the XML document.</summary>
		/// <returns>The valid character encoding name. The most commonly supported character encoding names for XML are the following: Category Encoding Names Unicode UTF-8, UTF-16 ISO 10646 ISO-10646-UCS-2, ISO-10646-UCS-4 ISO 8859 ISO-8859-n (where "n" is a digit from 1 to 9) JIS X-0208-1997 ISO-2022-JP, Shift_JIS, EUC-JP This value is optional. If a value is not set, this property returns String.Empty.If an encoding attribute is not included, UTF-8 encoding is assumed when the document is written or saved out.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0003B46C File Offset: 0x0003966C
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0003B474 File Offset: 0x00039674
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the value of the standalone attribute.</summary>
		/// <returns>Valid values are yes if all entity declarations required by the XML document are contained within the document or no if an external document type definition (DTD) is required. If a standalone attribute is not present in the XML declaration, this property returns String.Empty.</returns>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0003B487 File Offset: 0x00039687
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0003B490 File Offset: 0x00039690
		public string Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				if (value == null)
				{
					this.standalone = string.Empty;
					return;
				}
				if (value.Length == 0 || value == "yes" || value == "no")
				{
					this.standalone = value;
					return;
				}
				throw new ArgumentException(Res.GetString("Wrong value for the XML declaration standalone attribute of '{0}'.", new object[] { value }));
			}
		}

		/// <summary>Gets or sets the value of the XmlDeclaration.</summary>
		/// <returns>The contents of the XmlDeclaration (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0003A74C File Offset: 0x0003894C
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0003A754 File Offset: 0x00038954
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Gets or sets the concatenated values of the XmlDeclaration.</summary>
		/// <returns>The concatenated values of the XmlDeclaration (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0003B4F0 File Offset: 0x000396F0
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0003B584 File Offset: 0x00039784
		public override string InnerText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("version=\"" + this.Version + "\"");
				if (this.Encoding.Length > 0)
				{
					stringBuilder.Append(" encoding=\"");
					stringBuilder.Append(this.Encoding);
					stringBuilder.Append("\"");
				}
				if (this.Standalone.Length > 0)
				{
					stringBuilder.Append(" standalone=\"");
					stringBuilder.Append(this.Standalone);
					stringBuilder.Append("\"");
				}
				return stringBuilder.ToString();
			}
			set
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = this.Encoding;
				string text5 = this.Standalone;
				string text6 = this.Version;
				XmlLoader.ParseXmlDeclarationValue(value, out text, out text2, out text3);
				try
				{
					if (text != null && !this.IsValidXmlVersion(text))
					{
						throw new ArgumentException(Res.GetString("Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\"."));
					}
					this.Version = text;
					if (text2 != null)
					{
						this.Encoding = text2;
					}
					if (text3 != null)
					{
						this.Standalone = text3;
					}
				}
				catch
				{
					this.Encoding = text4;
					this.Standalone = text5;
					this.Version = text6;
					throw;
				}
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlDeclaration nodes, the name is xml.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0003B620 File Offset: 0x00039820
		public override string Name
		{
			get
			{
				return "xml";
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlDeclaration nodes, the local name is xml.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0003B627 File Offset: 0x00039827
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlDeclaration nodes, this value is XmlNodeType.XmlDeclaration.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0003B62F File Offset: 0x0003982F
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because XmlDeclaration nodes do not have children, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x06000B32 RID: 2866 RVA: 0x0003B633 File Offset: 0x00039833
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateXmlDeclaration(this.Version, this.Encoding, this.Standalone);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B33 RID: 2867 RVA: 0x0003B652 File Offset: 0x00039852
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.Name, this.InnerText);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because XmlDeclaration nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000B34 RID: 2868 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003B666 File Offset: 0x00039866
		private bool IsValidXmlVersion(string ver)
		{
			return ver.Length >= 3 && ver[0] == '1' && ver[1] == '.' && XmlCharType.IsOnlyDigits(ver, 2, ver.Length - 2);
		}

		// Token: 0x040005FD RID: 1533
		private string version;

		// Token: 0x040005FE RID: 1534
		private string encoding;

		// Token: 0x040005FF RID: 1535
		private string standalone;
	}
}
