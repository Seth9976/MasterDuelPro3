using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000008 RID: 8
	internal class ConfigurationXmlDocument : XmlDocument
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000021B0 File Offset: 0x000003B0
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			if (namespaceURI == "http://schemas.microsoft.com/.NetConfiguration/v2.0")
			{
				return base.CreateElement(string.Empty, localName, string.Empty);
			}
			return base.CreateElement(prefix, localName, namespaceURI);
		}
	}
}
