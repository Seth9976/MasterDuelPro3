using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML document. </summary>
	// Token: 0x0200000D RID: 13
	public class XDocument : XContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class. </summary>
		// Token: 0x06000049 RID: 73 RVA: 0x00003651 File Offset: 0x00001851
		public XDocument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class from an existing <see cref="T:System.Xml.Linq.XDocument" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XDocument" /> object that will be copied.</param>
		// Token: 0x0600004A RID: 74 RVA: 0x00003659 File Offset: 0x00001859
		public XDocument(XDocument other)
			: base(other)
		{
			if (other._declaration != null)
			{
				this._declaration = new XDeclaration(other._declaration);
			}
		}

		/// <summary>Gets or sets the XML declaration for this document.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDeclaration" /> that contains the XML declaration for this document.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000367B File Offset: 0x0000187B
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003683 File Offset: 0x00001883
		public XDeclaration Declaration
		{
			get
			{
				return this._declaration;
			}
			set
			{
				this._declaration = value;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocument" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Document" />.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000368C File Offset: 0x0000188C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Document;
			}
		}

		/// <summary>Gets the root element of the XML Tree for this document.</summary>
		/// <returns>The root <see cref="T:System.Xml.Linq.XElement" /> of the XML tree.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003690 File Offset: 0x00001890
		public XElement Root
		{
			get
			{
				return this.GetFirstNode<XElement>();
			}
		}

		/// <summary>Write this document to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004F RID: 79 RVA: 0x00003698 File Offset: 0x00001898
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this._declaration != null && this._declaration.Standalone == "yes")
			{
				writer.WriteStartDocument(true);
			}
			else if (this._declaration != null && this._declaration.Standalone == "no")
			{
				writer.WriteStartDocument(false);
			}
			else
			{
				writer.WriteStartDocument();
			}
			base.WriteContentTo(writer);
			writer.WriteEndDocument();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003716 File Offset: 0x00001916
		internal override void AddAttribute(XAttribute a)
		{
			throw new ArgumentException("An attribute cannot be added to content.");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003716 File Offset: 0x00001916
		internal override void AddAttributeSkipNotify(XAttribute a)
		{
			throw new ArgumentException("An attribute cannot be added to content.");
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003722 File Offset: 0x00001922
		internal override XNode CloneNode()
		{
			return new XDocument(this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000372C File Offset: 0x0000192C
		private T GetFirstNode<T>() where T : XNode
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				T t;
				for (;;)
				{
					xnode = xnode.next;
					t = xnode as T;
					if (t != null)
					{
						break;
					}
					if (xnode == this.content)
					{
						goto IL_0035;
					}
				}
				return t;
			}
			IL_0035:
			return default(T);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003778 File Offset: 0x00001978
		internal static bool IsWhitespace(string s)
		{
			foreach (char c in s)
			{
				if (c != ' ' && c != '\t' && c != '\r' && c != '\n')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000037B8 File Offset: 0x000019B8
		internal override void ValidateNode(XNode node, XNode previous)
		{
			XmlNodeType nodeType = node.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				this.ValidateDocument(previous, XmlNodeType.DocumentType, XmlNodeType.None);
				return;
			case XmlNodeType.Attribute:
				return;
			case XmlNodeType.Text:
				this.ValidateString(((XText)node).Value);
				return;
			case XmlNodeType.CDATA:
				throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.CDATA));
			default:
				if (nodeType == XmlNodeType.Document)
				{
					throw new ArgumentException(global::SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.Document));
				}
				if (nodeType != XmlNodeType.DocumentType)
				{
					return;
				}
				this.ValidateDocument(previous, XmlNodeType.None, XmlNodeType.Element);
				return;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003844 File Offset: 0x00001A44
		private void ValidateDocument(XNode previous, XmlNodeType allowBefore, XmlNodeType allowAfter)
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				if (previous == null)
				{
					allowBefore = allowAfter;
				}
				for (;;)
				{
					xnode = xnode.next;
					XmlNodeType nodeType = xnode.NodeType;
					if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.DocumentType)
					{
						if (nodeType != allowBefore)
						{
							break;
						}
						allowBefore = XmlNodeType.None;
					}
					if (xnode == previous)
					{
						allowBefore = allowAfter;
					}
					if (xnode == this.content)
					{
						return;
					}
				}
				throw new InvalidOperationException("This operation would create an incorrectly structured document.");
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000389F File Offset: 0x00001A9F
		internal override void ValidateString(string s)
		{
			if (!XDocument.IsWhitespace(s))
			{
				throw new ArgumentException("Non-whitespace characters cannot be added to content.");
			}
		}

		// Token: 0x04000016 RID: 22
		private XDeclaration _declaration;
	}
}
