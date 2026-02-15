using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000355 RID: 853
	internal sealed class MergeFilterQuery : CacheOutputQuery
	{
		// Token: 0x06002631 RID: 9777 RVA: 0x000D635E File Offset: 0x000D455E
		public MergeFilterQuery(Query input, Query child)
			: base(input)
		{
			this._child = child;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000D636E File Offset: 0x000D456E
		private MergeFilterQuery(MergeFilterQuery other)
			: base(other)
		{
			this._child = Query.Clone(other._child);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000D6388 File Offset: 0x000D4588
		public override void SetXsltContext(XsltContext xsltContext)
		{
			base.SetXsltContext(xsltContext);
			this._child.SetXsltContext(xsltContext);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000D63A0 File Offset: 0x000D45A0
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.Evaluate(nodeIterator);
			while (this.input.Advance() != null)
			{
				this._child.Evaluate(this.input);
				XPathNavigator xpathNavigator;
				while ((xpathNavigator = this._child.Advance()) != null)
				{
					Query.Insert(this.outputBuffer, xpathNavigator);
				}
			}
			return this;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000D63F5 File Offset: 0x000D45F5
		public override XPathNodeIterator Clone()
		{
			return new MergeFilterQuery(this);
		}

		// Token: 0x04001248 RID: 4680
		private Query _child;
	}
}
