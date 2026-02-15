using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200035E RID: 862
	internal sealed class ParentQuery : CacheAxisQuery
	{
		// Token: 0x06002667 RID: 9831 RVA: 0x000D6A96 File Offset: 0x000D4C96
		public ParentQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type)
			: base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000D6AA3 File Offset: 0x000D4CA3
		private ParentQuery(ParentQuery other)
			: base(other)
		{
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000D6AAC File Offset: 0x000D4CAC
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.qyInput.Advance()) != null)
			{
				xpathNavigator = xpathNavigator.Clone();
				if (xpathNavigator.MoveToParent() && this.matches(xpathNavigator))
				{
					Query.Insert(this.outputBuffer, xpathNavigator);
				}
			}
			return this;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000D6AF8 File Offset: 0x000D4CF8
		public override XPathNodeIterator Clone()
		{
			return new ParentQuery(this);
		}
	}
}
