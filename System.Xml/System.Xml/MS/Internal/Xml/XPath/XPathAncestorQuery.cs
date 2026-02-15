using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200036E RID: 878
	internal sealed class XPathAncestorQuery : CacheAxisQuery
	{
		// Token: 0x060026D3 RID: 9939 RVA: 0x000D84A9 File Offset: 0x000D66A9
		public XPathAncestorQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest, bool matchSelf)
			: base(qyInput, name, prefix, typeTest)
		{
			this._matchSelf = matchSelf;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x000D84BE File Offset: 0x000D66BE
		private XPathAncestorQuery(XPathAncestorQuery other)
			: base(other)
		{
			this._matchSelf = other._matchSelf;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000D84D4 File Offset: 0x000D66D4
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator = null;
			XPathNavigator xpathNavigator2;
			while ((xpathNavigator2 = this.qyInput.Advance()) != null)
			{
				if (!this._matchSelf || !this.matches(xpathNavigator2) || Query.Insert(this.outputBuffer, xpathNavigator2))
				{
					if (xpathNavigator == null || !xpathNavigator.MoveTo(xpathNavigator2))
					{
						xpathNavigator = xpathNavigator2.Clone();
					}
					while (xpathNavigator.MoveToParent() && (!this.matches(xpathNavigator) || Query.Insert(this.outputBuffer, xpathNavigator)))
					{
					}
				}
			}
			return this;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000D8550 File Offset: 0x000D6750
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorQuery(this);
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000D8558 File Offset: 0x000D6758
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x000D6C0D File Offset: 0x000D4E0D
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x04001290 RID: 4752
		private bool _matchSelf;
	}
}
