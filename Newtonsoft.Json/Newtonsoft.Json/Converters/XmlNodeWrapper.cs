using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C3 RID: 451
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x000427A8 File Offset: 0x000409A8
		[NullableContext(1)]
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x000427B7 File Offset: 0x000409B7
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x000427BF File Offset: 0x000409BF
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x000427CC File Offset: 0x000409CC
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x000427DC File Offset: 0x000409DC
		[Nullable(1)]
		public List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(xmlNode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0004288C File Offset: 0x00040A8C
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0004289C File Offset: 0x00040A9C
		[NullableContext(1)]
		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == XmlNodeType.DocumentType)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != XmlNodeType.XmlDeclaration)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x000428EC File Offset: 0x00040AEC
		[Nullable(1)]
		public List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(xmlAttribute));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00042994 File Offset: 0x00040B94
		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this._node as XmlElement;
				if (xmlElement != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x000429D0 File Offset: 0x00040BD0
		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute = this._node as XmlAttribute;
				XmlNode xmlNode = ((xmlAttribute != null) ? xmlAttribute.OwnerElement : this._node.ParentNode);
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00042A0B File Offset: 0x00040C0B
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00042A18 File Offset: 0x00040C18
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00042A28 File Offset: 0x00040C28
		[NullableContext(1)]
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00042A5D File Offset: 0x00040C5D
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x04000816 RID: 2070
		[Nullable(1)]
		private readonly XmlNode _node;

		// Token: 0x04000817 RID: 2071
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _childNodes;

		// Token: 0x04000818 RID: 2072
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _attributes;
	}
}
