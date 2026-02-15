using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001CF RID: 463
	[NullableContext(1)]
	[Nullable(0)]
	internal class XContainerWrapper : XObjectWrapper
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00042D78 File Offset: 0x00040F78
		private XContainer Container
		{
			get
			{
				return (XContainer)base.WrappedNode;
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00042C9A File Offset: 0x00040E9A
		public XContainerWrapper(XContainer container)
			: base(container)
		{
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00042D88 File Offset: 0x00040F88
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					if (!this.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>();
						foreach (XNode xnode in this.Container.Nodes())
						{
							this._childNodes.Add(XContainerWrapper.WrapNode(xnode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00042E14 File Offset: 0x00041014
		protected virtual bool HasChildNodes
		{
			get
			{
				return this.Container.LastNode != null;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00042E24 File Offset: 0x00041024
		[Nullable(2)]
		public override IXmlNode ParentNode
		{
			[NullableContext(2)]
			get
			{
				if (this.Container.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Container.Parent);
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00042E48 File Offset: 0x00041048
		internal static IXmlNode WrapNode(XObject node)
		{
			XDocument xdocument = node as XDocument;
			if (xdocument != null)
			{
				return new XDocumentWrapper(xdocument);
			}
			XElement xelement = node as XElement;
			if (xelement != null)
			{
				return new XElementWrapper(xelement);
			}
			XContainer xcontainer = node as XContainer;
			if (xcontainer != null)
			{
				return new XContainerWrapper(xcontainer);
			}
			XProcessingInstruction xprocessingInstruction = node as XProcessingInstruction;
			if (xprocessingInstruction != null)
			{
				return new XProcessingInstructionWrapper(xprocessingInstruction);
			}
			XText xtext = node as XText;
			if (xtext != null)
			{
				return new XTextWrapper(xtext);
			}
			XComment xcomment = node as XComment;
			if (xcomment != null)
			{
				return new XCommentWrapper(xcomment);
			}
			XAttribute xattribute = node as XAttribute;
			if (xattribute != null)
			{
				return new XAttributeWrapper(xattribute);
			}
			XDocumentType xdocumentType = node as XDocumentType;
			if (xdocumentType != null)
			{
				return new XDocumentTypeWrapper(xdocumentType);
			}
			return new XObjectWrapper(node);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00042EEF File Offset: 0x000410EF
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			this.Container.Add(newChild.WrappedNode);
			this._childNodes = null;
			return newChild;
		}

		// Token: 0x0400081B RID: 2075
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _childNodes;
	}
}
