using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000341 RID: 833
	internal class DescendantQuery : DescendantBaseQuery
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x000D4A24 File Offset: 0x000D2C24
		internal DescendantQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis)
			: base(qyParent, Name, Prefix, Type, matchSelf, abbrAxis)
		{
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000D4A35 File Offset: 0x000D2C35
		public DescendantQuery(DescendantQuery other)
			: base(other)
		{
			this._nodeIterator = Query.Clone(other._nodeIterator);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000D4A4F File Offset: 0x000D2C4F
		public override void Reset()
		{
			this._nodeIterator = null;
			base.Reset();
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000D4A60 File Offset: 0x000D2C60
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this._nodeIterator == null)
				{
					this.position = 0;
					XPathNavigator xpathNavigator = this.qyInput.Advance();
					if (xpathNavigator == null)
					{
						break;
					}
					if (base.NameTest)
					{
						if (base.TypeTest == XPathNodeType.ProcessingInstruction)
						{
							this._nodeIterator = new IteratorFilter(xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf), base.Name);
						}
						else
						{
							this._nodeIterator = xpathNavigator.SelectDescendants(base.Name, base.Namespace, this.matchSelf);
						}
					}
					else
					{
						this._nodeIterator = xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf);
					}
				}
				if (this._nodeIterator.MoveNext())
				{
					goto Block_4;
				}
				this._nodeIterator = null;
			}
			return null;
			Block_4:
			this.position++;
			this.currentNode = this._nodeIterator.Current;
			return this.currentNode;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000D4B3C File Offset: 0x000D2D3C
		public override XPathNodeIterator Clone()
		{
			return new DescendantQuery(this);
		}

		// Token: 0x0400120A RID: 4618
		private XPathNodeIterator _nodeIterator;
	}
}
