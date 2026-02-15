using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Provides text manipulation methods that are used by several classes.</summary>
	// Token: 0x020000DC RID: 220
	public abstract class XmlCharacterData : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCharacterData" /> class.</summary>
		/// <param name="data">String that contains character data to be added to document.</param>
		/// <param name="doc">
		///   <see cref="T:System.Xml.XmlDocument" /> to contain character data.</param>
		// Token: 0x06000B09 RID: 2825 RVA: 0x0003B148 File Offset: 0x00039348
		protected internal XmlCharacterData(string data, XmlDocument doc)
			: base(doc)
		{
			this.data = data;
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value of the node.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0003B158 File Offset: 0x00039358
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x0003B160 File Offset: 0x00039360
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all the children of the node.</summary>
		/// <returns>The concatenated values of the node and all the children of the node.</returns>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0003B169 File Offset: 0x00039369
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x0003B171 File Offset: 0x00039371
		public override string InnerText
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		/// <summary>Contains the data of the node.</summary>
		/// <returns>The data of the node.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0003B17A File Offset: 0x0003937A
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x0003B190 File Offset: 0x00039390
		public virtual string Data
		{
			get
			{
				if (this.data != null)
				{
					return this.data;
				}
				return string.Empty;
			}
			set
			{
				XmlNode parentNode = this.ParentNode;
				XmlNodeChangedEventArgs eventArgs = this.GetEventArgs(this, parentNode, parentNode, this.data, value, XmlNodeChangedAction.Change);
				if (eventArgs != null)
				{
					this.BeforeEvent(eventArgs);
				}
				this.data = value;
				if (eventArgs != null)
				{
					this.AfterEvent(eventArgs);
				}
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0003B1D4 File Offset: 0x000393D4
		internal bool CheckOnData(string data)
		{
			return XmlCharType.Instance.IsOnlyWhitespace(data);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0003B1F0 File Offset: 0x000393F0
		internal bool DecideXPNodeTypeForTextNodes(XmlNode node, ref XPathNodeType xnt)
		{
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				if (nodeType <= XmlNodeType.EntityReference)
				{
					if (nodeType - XmlNodeType.Text <= 1)
					{
						xnt = XPathNodeType.Text;
						return false;
					}
					if (nodeType != XmlNodeType.EntityReference)
					{
						return false;
					}
					if (!this.DecideXPNodeTypeForTextNodes(node.FirstChild, ref xnt))
					{
						return false;
					}
				}
				else if (nodeType != XmlNodeType.Whitespace)
				{
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						return false;
					}
					xnt = XPathNodeType.SignificantWhitespace;
				}
				node = node.NextSibling;
			}
			return true;
		}

		// Token: 0x040005F8 RID: 1528
		private string data;
	}
}
