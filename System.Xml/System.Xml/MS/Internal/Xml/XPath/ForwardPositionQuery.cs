using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200034A RID: 842
	internal class ForwardPositionQuery : CacheOutputQuery
	{
		// Token: 0x060025DB RID: 9691 RVA: 0x000D4C87 File Offset: 0x000D2E87
		public ForwardPositionQuery(Query input)
			: base(input)
		{
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000D4C90 File Offset: 0x000D2E90
		protected ForwardPositionQuery(ForwardPositionQuery other)
			: base(other)
		{
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000D54A4 File Offset: 0x000D36A4
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.input.Advance()) != null)
			{
				this.outputBuffer.Add(xpathNavigator.Clone());
			}
			return this;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000D54DC File Offset: 0x000D36DC
		public override XPathNodeIterator Clone()
		{
			return new ForwardPositionQuery(this);
		}
	}
}
