using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x0200004A RID: 74
	internal class XmlAsyncCheckReaderWithLineInfoNS : XmlAsyncCheckReaderWithLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000DB84 File Offset: 0x0000BD84
		public XmlAsyncCheckReaderWithLineInfoNS(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000DB99 File Offset: 0x0000BD99
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000DBA7 File Offset: 0x0000BDA7
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000DBB5 File Offset: 0x0000BDB5
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x04000176 RID: 374
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
