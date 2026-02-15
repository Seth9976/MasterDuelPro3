using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001CB RID: 459
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00042B0D File Offset: 0x00040D0D
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00042B1A File Offset: 0x00040D1A
		public XDocumentWrapper(XDocument document)
			: base(document)
		{
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00042B24 File Offset: 0x00040D24
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				List<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != XmlNodeType.XmlDeclaration))
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00042B75 File Offset: 0x00040D75
		protected override bool HasChildNodes
		{
			get
			{
				return base.HasChildNodes || this.Document.Declaration != null;
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00042B8F File Offset: 0x00040D8F
		public IXmlNode CreateComment([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00042B9C File Offset: 0x00040D9C
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00042BA9 File Offset: 0x00040DA9
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00042B9C File Offset: 0x00040D9C
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00042B9C File Offset: 0x00040D9C
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00042BB6 File Offset: 0x00040DB6
		public IXmlNode CreateXmlDeclaration(string version, [Nullable(2)] string encoding, [Nullable(2)] string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00042BC5 File Offset: 0x00040DC5
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDocumentType([Nullable(1)] string name, string publicId, string systemId, string internalSubset)
		{
			return new XDocumentTypeWrapper(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00042BD6 File Offset: 0x00040DD6
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00042BE4 File Offset: 0x00040DE4
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00042BF6 File Offset: 0x00040DF6
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XElementWrapper(new XElement(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00042C0E File Offset: 0x00040E0E
		public IXmlNode CreateAttribute(string name, string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00042C21 File Offset: 0x00040E21
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, string value)
		{
			return new XAttributeWrapper(new XAttribute(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00042C3A File Offset: 0x00040E3A
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00042C5C File Offset: 0x00040E5C
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper.Declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
