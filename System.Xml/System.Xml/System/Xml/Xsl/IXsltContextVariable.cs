using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	/// <summary>Provides an interface to a given variable that is defined in the style sheet during runtime execution.</summary>
	// Token: 0x02000203 RID: 515
	public interface IXsltContextVariable
	{
		/// <summary>Gets the <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XML Path Language (XPath) type of the variable.</summary>
		/// <returns>The <see cref="T:System.Xml.XPath.XPathResultType" /> representing the XPath type of the variable.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060019DF RID: 6623
		XPathResultType VariableType { get; }

		/// <summary>Evaluates the variable at runtime and returns an object that represents the value of the variable.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the value of the variable. Possible return types include number, string, Boolean, document fragment, or node set.</returns>
		/// <param name="xsltContext">An <see cref="T:System.Xml.Xsl.XsltContext" /> representing the execution context of the variable. </param>
		// Token: 0x060019E0 RID: 6624
		object Evaluate(XsltContext xsltContext);
	}
}
