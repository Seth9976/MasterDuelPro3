using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200033D RID: 829
	internal class CompiledXpathExpr : XPathExpression
	{
		// Token: 0x06002586 RID: 9606 RVA: 0x000D48C4 File Offset: 0x000D2AC4
		internal CompiledXpathExpr(Query query, string expression, bool needContext)
		{
			this._query = query;
			this._expr = expression;
			this._needContext = needContext;
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x000D48E1 File Offset: 0x000D2AE1
		internal Query QueryTree
		{
			get
			{
				if (this._needContext)
				{
					throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
				}
				return this._query;
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000D48FC File Offset: 0x000D2AFC
		public override void SetContext(IXmlNamespaceResolver nsResolver)
		{
			XsltContext xsltContext = nsResolver as XsltContext;
			if (xsltContext == null)
			{
				if (nsResolver == null)
				{
					nsResolver = new XmlNamespaceManager(new NameTable());
				}
				xsltContext = new CompiledXpathExpr.UndefinedXsltContext(nsResolver);
			}
			this._query.SetXsltContext(xsltContext);
			this._needContext = false;
		}

		// Token: 0x04001203 RID: 4611
		private Query _query;

		// Token: 0x04001204 RID: 4612
		private string _expr;

		// Token: 0x04001205 RID: 4613
		private bool _needContext;

		// Token: 0x0200033E RID: 830
		private class UndefinedXsltContext : XsltContext
		{
			// Token: 0x06002589 RID: 9609 RVA: 0x000D493C File Offset: 0x000D2B3C
			public UndefinedXsltContext(IXmlNamespaceResolver nsResolver)
				: base(false)
			{
				this._nsResolver = nsResolver;
			}

			// Token: 0x170008D2 RID: 2258
			// (get) Token: 0x0600258A RID: 9610 RVA: 0x00015451 File Offset: 0x00013651
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x0600258B RID: 9611 RVA: 0x000D494C File Offset: 0x000D2B4C
			public override string LookupNamespace(string prefix)
			{
				if (prefix.Length == 0)
				{
					return string.Empty;
				}
				string text = this._nsResolver.LookupNamespace(prefix);
				if (text == null)
				{
					throw XPathException.Create("Namespace prefix '{0}' is not defined.", prefix);
				}
				return text;
			}

			// Token: 0x0600258C RID: 9612 RVA: 0x000D4977 File Offset: 0x000D2B77
			public override IXsltContextVariable ResolveVariable(string prefix, string name)
			{
				throw XPathException.Create("XsltContext is needed for this query because of an unknown function.");
			}

			// Token: 0x0600258D RID: 9613 RVA: 0x000D4977 File Offset: 0x000D2B77
			public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
			{
				throw XPathException.Create("XsltContext is needed for this query because of an unknown function.");
			}

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x0600258E RID: 9614 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
			public override bool Whitespace
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600258F RID: 9615 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
			public override bool PreserveWhitespace(XPathNavigator node)
			{
				return false;
			}

			// Token: 0x04001206 RID: 4614
			private IXmlNamespaceResolver _nsResolver;
		}
	}
}
