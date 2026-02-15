using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001BF RID: 447
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDocumentWrapper : XmlNodeWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x06000F03 RID: 3843 RVA: 0x00042586 File Offset: 0x00040786
		public XmlDocumentWrapper(XmlDocument document)
			: base(document)
		{
			this._document = document;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00042596 File Offset: 0x00040796
		public IXmlNode CreateComment([Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateComment(data));
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x000425A9 File Offset: 0x000407A9
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateTextNode(text));
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x000425BC File Offset: 0x000407BC
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateCDataSection(data));
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000425CF File Offset: 0x000407CF
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateWhitespace(text));
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000425E2 File Offset: 0x000407E2
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateSignificantWhitespace(text));
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000425F5 File Offset: 0x000407F5
		public IXmlNode CreateXmlDeclaration(string version, [Nullable(2)] string encoding, [Nullable(2)] string standalone)
		{
			return new XmlDeclarationWrapper(this._document.CreateXmlDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0004260A File Offset: 0x0004080A
		[NullableContext(2)]
		[return: Nullable(1)]
		public IXmlNode CreateXmlDocumentType([Nullable(1)] string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentTypeWrapper(this._document.CreateDocumentType(name, publicId, systemId, null));
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00042620 File Offset: 0x00040820
		public IXmlNode CreateProcessingInstruction(string target, string data)
		{
			return new XmlNodeWrapper(this._document.CreateProcessingInstruction(target, data));
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00042634 File Offset: 0x00040834
		public IXmlElement CreateElement(string elementName)
		{
			return new XmlElementWrapper(this._document.CreateElement(elementName));
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00042647 File Offset: 0x00040847
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XmlElementWrapper(this._document.CreateElement(qualifiedName, namespaceUri));
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0004265B File Offset: 0x0004085B
		public IXmlNode CreateAttribute(string name, [Nullable(2)] string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(name))
			{
				Value = value
			};
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00042675 File Offset: 0x00040875
		public IXmlNode CreateAttribute(string qualifiedName, [Nullable(2)] string namespaceUri, [Nullable(2)] string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(qualifiedName, namespaceUri))
			{
				Value = value
			};
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00042690 File Offset: 0x00040890
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this._document.DocumentElement == null)
				{
					return null;
				}
				return new XmlElementWrapper(this._document.DocumentElement);
			}
		}

		// Token: 0x04000812 RID: 2066
		private readonly XmlDocument _document;
	}
}
