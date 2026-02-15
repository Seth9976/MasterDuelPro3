using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200034F RID: 847
	internal sealed class GroupQuery : BaseAxisQuery
	{
		// Token: 0x060025F3 RID: 9715 RVA: 0x000D57FE File Offset: 0x000D39FE
		public GroupQuery(Query qy)
			: base(qy)
		{
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000D5807 File Offset: 0x000D3A07
		private GroupQuery(GroupQuery other)
			: base(other)
		{
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000D5810 File Offset: 0x000D3A10
		public override XPathNavigator Advance()
		{
			this.currentNode = this.qyInput.Advance();
			if (this.currentNode != null)
			{
				this.position++;
			}
			return this.currentNode;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000D583F File Offset: 0x000D3A3F
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.qyInput.Evaluate(nodeIterator);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000D584D File Offset: 0x000D3A4D
		public override XPathNodeIterator Clone()
		{
			return new GroupQuery(this);
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060025F8 RID: 9720 RVA: 0x000D5855 File Offset: 0x000D3A55
		public override XPathResultType StaticType
		{
			get
			{
				return this.qyInput.StaticType;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override QueryProps Properties
		{
			get
			{
				return QueryProps.Position;
			}
		}
	}
}
