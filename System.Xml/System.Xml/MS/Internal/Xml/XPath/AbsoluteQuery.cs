using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200032F RID: 815
	internal sealed class AbsoluteQuery : ContextQuery
	{
		// Token: 0x06002528 RID: 9512 RVA: 0x000D3CF9 File Offset: 0x000D1EF9
		public AbsoluteQuery()
		{
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000D3D01 File Offset: 0x000D1F01
		private AbsoluteQuery(AbsoluteQuery other)
			: base(other)
		{
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000D3D0A File Offset: 0x000D1F0A
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current.Clone();
			this.contextNode.MoveToRoot();
			this.count = 0;
			return this;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000D3D30 File Offset: 0x000D1F30
		public override XPathNodeIterator Clone()
		{
			return new AbsoluteQuery(this);
		}
	}
}
