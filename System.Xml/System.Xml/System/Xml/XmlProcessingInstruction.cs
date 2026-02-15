using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a processing instruction, which XML defines to keep processor-specific information in the text of the document.</summary>
	// Token: 0x020000F9 RID: 249
	public class XmlProcessingInstruction : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlProcessingInstruction" /> class.</summary>
		/// <param name="target">The target of the processing instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Target" /> property.</param>
		/// <param name="data">The content of the instruction; see the <see cref="P:System.Xml.XmlProcessingInstruction.Data" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00042E0E File Offset: 0x0004100E
		protected internal XmlProcessingInstruction(string target, string data, XmlDocument doc)
			: base(doc)
		{
			this.target = target;
			this.data = data;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00042E25 File Offset: 0x00041025
		public override string Name
		{
			get
			{
				if (this.target != null)
				{
					return this.target;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For processing instruction nodes, this property returns the target of the processing instruction.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0003B627 File Offset: 0x00039827
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The entire content of the processing instruction, excluding the target.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00042E3B File Offset: 0x0004103B
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00042E43 File Offset: 0x00041043
		public override string Value
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets or sets the content of the processing instruction, excluding the target.</summary>
		/// <returns>The content of the processing instruction, excluding the target.</returns>
		// Token: 0x1700032C RID: 812
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x00042E4C File Offset: 0x0004104C
		public string Data
		{
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

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00042E3B File Offset: 0x0004103B
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x00042E43 File Offset: 0x00041043
		public override string InnerText
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlProcessingInstruction nodes, this value is XmlNodeType.ProcessingInstruction.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00042E8D File Offset: 0x0004108D
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The duplicate node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		// Token: 0x06000D3E RID: 3390 RVA: 0x00042E90 File Offset: 0x00041090
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateProcessingInstruction(this.target, this.data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D3F RID: 3391 RVA: 0x00042EA9 File Offset: 0x000410A9
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.target, this.data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because ProcessingInstruction nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x06000D40 RID: 3392 RVA: 0x0000A558 File Offset: 0x00008758
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0003B627 File Offset: 0x00039827
		internal override string XPLocalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00042E8D File Offset: 0x0004108D
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x04000682 RID: 1666
		private string target;

		// Token: 0x04000683 RID: 1667
		private string data;
	}
}
