using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the namespace scope.</summary>
	// Token: 0x0200013C RID: 316
	public enum XPathNamespaceScope
	{
		/// <summary>Returns all namespaces defined in the scope of the current node. This includes the xmlns:xml namespace which is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x0400079C RID: 1948
		All,
		/// <summary>Returns all namespaces defined in the scope of the current node, excluding the xmlns:xml namespace. The xmlns:xml namespace is always declared implicitly. The order of the namespaces returned is not defined.</summary>
		// Token: 0x0400079D RID: 1949
		ExcludeXml,
		/// <summary>Returns all namespaces that are defined locally at the current node. </summary>
		// Token: 0x0400079E RID: 1950
		Local
	}
}
