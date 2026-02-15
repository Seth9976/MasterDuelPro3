using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200033F RID: 831
	internal class ContextQuery : Query
	{
		// Token: 0x06002590 RID: 9616 RVA: 0x000D4983 File Offset: 0x000D2B83
		public ContextQuery()
		{
			this.count = 0;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000D4992 File Offset: 0x000D2B92
		protected ContextQuery(ContextQuery other)
			: base(other)
		{
			this.contextNode = other.contextNode;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000D439C File Offset: 0x000D259C
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000D49A7 File Offset: 0x000D2BA7
		public override XPathNavigator Current
		{
			get
			{
				return this.contextNode;
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000D49AF File Offset: 0x000D2BAF
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current;
			this.count = 0;
			return this;
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000D49C5 File Offset: 0x000D2BC5
		public override XPathNavigator Advance()
		{
			if (this.count == 0)
			{
				this.count = 1;
				return this.contextNode;
			}
			return null;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000D49DE File Offset: 0x000D2BDE
		public override XPathNodeIterator Clone()
		{
			return new ContextQuery(this);
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x000D4419 File Offset: 0x000D2619
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x0009EA15 File Offset: 0x0009CC15
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04001207 RID: 4615
		protected XPathNavigator contextNode;
	}
}
