using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200036D RID: 877
	internal sealed class VariableQuery : ExtensionQuery
	{
		// Token: 0x060026CD RID: 9933 RVA: 0x000D83AF File Offset: 0x000D65AF
		public VariableQuery(string name, string prefix)
			: base(prefix, name)
		{
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000D83B9 File Offset: 0x000D65B9
		private VariableQuery(VariableQuery other)
			: base(other)
		{
			this._variable = other._variable;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000D83D0 File Offset: 0x000D65D0
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				this._variable = this.xsltContext.ResolveVariable(this.prefix, this.name);
				if (this._variable == null)
				{
					throw XPathException.Create("The variable '{0}' is undefined.", base.QName);
				}
			}
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000D8431 File Offset: 0x000D6631
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			return base.ProcessResult(this._variable.Evaluate(this.xsltContext));
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000D8460 File Offset: 0x000D6660
		public override XPathResultType StaticType
		{
			get
			{
				if (this._variable != null)
				{
					return base.GetXPathType(this.Evaluate(null));
				}
				XPathResultType xpathResultType = ((this._variable != null) ? this._variable.VariableType : XPathResultType.Any);
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000D84A1 File Offset: 0x000D66A1
		public override XPathNodeIterator Clone()
		{
			return new VariableQuery(this);
		}

		// Token: 0x0400128F RID: 4751
		private IXsltContextVariable _variable;
	}
}
