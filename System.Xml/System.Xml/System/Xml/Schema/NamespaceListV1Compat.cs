using System;

namespace System.Xml.Schema
{
	// Token: 0x0200028A RID: 650
	internal class NamespaceListV1Compat : NamespaceList
	{
		// Token: 0x06001D57 RID: 7511 RVA: 0x000A7034 File Offset: 0x000A5234
		public NamespaceListV1Compat(string namespaces, string targetNamespace)
			: base(namespaces, targetNamespace)
		{
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000A703E File Offset: 0x000A523E
		public override bool Allows(string ns)
		{
			if (base.Type == NamespaceList.ListType.Other)
			{
				return ns != base.Excluded;
			}
			return base.Allows(ns);
		}
	}
}
