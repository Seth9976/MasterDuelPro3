using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001D2 RID: 466
	[NullableContext(1)]
	[Nullable(0)]
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00042FB8 File Offset: 0x000411B8
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00042B1A File Offset: 0x00040D1A
		public XElementWrapper(XElement element)
			: base(element)
		{
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00042FC8 File Offset: 0x000411C8
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
			this._attributes = null;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00042FF4 File Offset: 0x000411F4
		public override List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.Element.HasAttributes && !this.HasImplicitNamespaceAttribute(this.NamespaceUri))
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>();
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							this._attributes.Add(new XAttributeWrapper(xattribute));
						}
						string namespaceUri = this.NamespaceUri;
						if (this.HasImplicitNamespaceAttribute(namespaceUri))
						{
							this._attributes.Insert(0, new XAttributeWrapper(new XAttribute("xmlns", namespaceUri)));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000430C8 File Offset: 0x000412C8
		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!StringUtils.IsNullOrEmpty(namespaceUri))
			{
				IXmlNode parentNode = this.ParentNode;
				if (namespaceUri != ((parentNode != null) ? parentNode.NamespaceUri : null) && StringUtils.IsNullOrEmpty(this.GetPrefixOfNamespace(namespaceUri)))
				{
					bool flag = false;
					if (this.Element.HasAttributes)
					{
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							if (xattribute.Name.LocalName == "xmlns" && StringUtils.IsNullOrEmpty(xattribute.Name.NamespaceName) && xattribute.Value == namespaceUri)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00043198 File Offset: 0x00041398
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			IXmlNode xmlNode = base.AppendChild(newChild);
			this._attributes = null;
			return xmlNode;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x000431A8 File Offset: 0x000413A8
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x000431B5 File Offset: 0x000413B5
		[Nullable(2)]
		public override string Value
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Value;
			}
			[NullableContext(2)]
			set
			{
				this.Element.Value = value ?? string.Empty;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x000431CC File Offset: 0x000413CC
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000431DE File Offset: 0x000413DE
		[Nullable(2)]
		public override string NamespaceUri
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000431F0 File Offset: 0x000413F0
		[return: Nullable(2)]
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00043203 File Offset: 0x00041403
		public bool IsEmpty
		{
			get
			{
				return this.Element.IsEmpty;
			}
		}

		// Token: 0x0400081D RID: 2077
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _attributes;
	}
}
