using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001C0 RID: 448
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x06000F11 RID: 3857 RVA: 0x000426B1 File Offset: 0x000408B1
		public XmlElementWrapper(XmlElement element)
			: base(element)
		{
			this._element = element;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000426C4 File Offset: 0x000408C4
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000426EF File Offset: 0x000408EF
		[return: Nullable(2)]
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000426FD File Offset: 0x000408FD
		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		// Token: 0x04000813 RID: 2067
		private readonly XmlElement _element;
	}
}
