using System;

namespace System.Xml
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x0200012A RID: 298
	public enum XmlNamespaceScope
	{
		/// <summary>All namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04000750 RID: 1872
		All,
		/// <summary>All namespaces defined in the scope of the current node, excluding the xmlns:xml namespace, which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x04000751 RID: 1873
		ExcludeXml,
		/// <summary>All namespaces that are defined locally at the current node.</summary>
		// Token: 0x04000752 RID: 1874
		Local
	}
}
