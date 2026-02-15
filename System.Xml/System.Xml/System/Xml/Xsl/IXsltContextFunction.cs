using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>Provides an interface to a given function defined in the Extensible Stylesheet Language for Transformations (XSLT) style sheet during runtime execution.</summary>
	// Token: 0x02000202 RID: 514
	public interface IXsltContextFunction
	{
		/// <summary>Gets the <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XPath type returned by the function.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XPath type returned by the function </returns>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060019DD RID: 6621
		XPathResultType ReturnType { get; }

		/// <summary>Provides the method to invoke the function with the given arguments in the given context.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the function.</returns>
		/// <param name="xsltContext">The XSLT context for the function call. </param>
		/// <param name="args">The arguments of the function call. Each argument is an element in the array. </param>
		/// <param name="docContext">The context node for the function call. </param>
		// Token: 0x060019DE RID: 6622
		object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext);
	}
}
