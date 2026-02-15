using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000343 RID: 835
	internal sealed class DocumentOrderQuery : CacheOutputQuery
	{
		// Token: 0x060025A9 RID: 9641 RVA: 0x000D4C87 File Offset: 0x000D2E87
		public DocumentOrderQuery(Query qyParent)
			: base(qyParent)
		{
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000D4C90 File Offset: 0x000D2E90
		private DocumentOrderQuery(DocumentOrderQuery other)
			: base(other)
		{
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000D4C9C File Offset: 0x000D2E9C
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.input.Advance()) != null)
			{
				Query.Insert(this.outputBuffer, xpathNavigator);
			}
			return this;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000D4CD0 File Offset: 0x000D2ED0
		public override XPathNodeIterator Clone()
		{
			return new DocumentOrderQuery(this);
		}
	}
}
