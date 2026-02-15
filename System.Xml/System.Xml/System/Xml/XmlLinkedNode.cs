using System;

namespace System.Xml
{
	/// <summary>Gets the node immediately preceding or following this node.</summary>
	// Token: 0x020000EA RID: 234
	public abstract class XmlLinkedNode : XmlNode
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x0003DB9F File Offset: 0x0003BD9F
		internal XmlLinkedNode(XmlDocument doc)
			: base(doc)
		{
			this.next = null;
		}

		/// <summary>Gets the node immediately preceding this node.</summary>
		/// <returns>The preceding <see cref="T:System.Xml.XmlNode" /> or null if one does not exist.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0003DBB0 File Offset: 0x0003BDB0
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null)
				{
					XmlNode xmlNode;
					XmlNode nextSibling;
					for (xmlNode = parentNode.FirstChild; xmlNode != null; xmlNode = nextSibling)
					{
						nextSibling = xmlNode.NextSibling;
						if (nextSibling == this)
						{
							break;
						}
					}
					return xmlNode;
				}
				return null;
			}
		}

		/// <summary>Gets the node immediately following this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> immediately following this node or null if one does not exist.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0003DBE4 File Offset: 0x0003BDE4
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null && this.next != parentNode.FirstChild)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x04000648 RID: 1608
		internal XmlLinkedNode next;
	}
}
