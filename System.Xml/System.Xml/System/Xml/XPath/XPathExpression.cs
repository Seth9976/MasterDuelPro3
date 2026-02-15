using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.XPath
{
	/// <summary>Provides a typed class that represents a compiled XPath expression.</summary>
	// Token: 0x0200013A RID: 314
	public abstract class XPathExpression
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x00002127 File Offset: 0x00000327
		internal XPathExpression()
		{
		}

		/// <summary>When overridden in a derived class, specifies the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object to use for namespace resolution.</summary>
		/// <param name="nsResolver">An object that implements the <see cref="T:System.Xml.IXmlNamespaceResolver" /> interface to use for namespace resolution.</param>
		/// <exception cref="T:System.Xml.XPath.XPathException">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object parameter is not derived from <see cref="T:System.Xml.IXmlNamespaceResolver" />. </exception>
		// Token: 0x06000F6F RID: 3951
		public abstract void SetContext(IXmlNamespaceResolver nsResolver);

		/// <summary>Compiles the XPath expression specified and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object representing the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		/// <param name="xpath">An XPath expression.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression parameter is not a valid XPath expression.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000F70 RID: 3952 RVA: 0x0004CFA3 File Offset: 0x0004B1A3
		public static XPathExpression Compile(string xpath)
		{
			return XPathExpression.Compile(xpath, null);
		}

		/// <summary>Compiles the specified XPath expression, with the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified for namespace resolution, and returns an <see cref="T:System.Xml.XPath.XPathExpression" /> object that represents the XPath expression.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathExpression" /> object.</returns>
		/// <param name="xpath">An XPath expression.</param>
		/// <param name="nsResolver">An object that implements the <see cref="T:System.Xml.IXmlNamespaceResolver" /> interface for namespace resolution.</param>
		/// <exception cref="T:System.ArgumentException">The XPath expression parameter is not a valid XPath expression.</exception>
		/// <exception cref="T:System.Xml.XPath.XPathException">The XPath expression is not valid.</exception>
		// Token: 0x06000F71 RID: 3953 RVA: 0x0004CFAC File Offset: 0x0004B1AC
		public static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsResolver)
		{
			bool flag;
			CompiledXpathExpr compiledXpathExpr = new CompiledXpathExpr(new QueryBuilder().Build(xpath, out flag), xpath, flag);
			if (nsResolver != null)
			{
				compiledXpathExpr.SetContext(nsResolver);
			}
			return compiledXpathExpr;
		}
	}
}
