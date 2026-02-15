using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000048 RID: 72
	internal class XmlAsyncCheckReaderWithNS : XmlAsyncCheckReader, IXmlNamespaceResolver
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000DB09 File Offset: 0x0000BD09
		public XmlAsyncCheckReaderWithNS(XmlReader reader)
			: base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000DB1E File Offset: 0x0000BD1E
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000DB3A File Offset: 0x0000BD3A
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x04000174 RID: 372
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
