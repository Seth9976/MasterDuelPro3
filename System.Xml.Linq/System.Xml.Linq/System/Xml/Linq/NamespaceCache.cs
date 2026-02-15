using System;

namespace System.Xml.Linq
{
	// Token: 0x02000015 RID: 21
	internal struct NamespaceCache
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000451C File Offset: 0x0000271C
		public XNamespace Get(string namespaceName)
		{
			if (namespaceName == this._namespaceName)
			{
				return this._ns;
			}
			this._namespaceName = namespaceName;
			this._ns = XNamespace.Get(namespaceName);
			return this._ns;
		}

		// Token: 0x0400002C RID: 44
		private XNamespace _ns;

		// Token: 0x0400002D RID: 45
		private string _namespaceName;
	}
}
