using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000332 RID: 818
	internal sealed class AttributeQuery : BaseAxisQuery
	{
		// Token: 0x0600252F RID: 9519 RVA: 0x000D3D38 File Offset: 0x000D1F38
		public AttributeQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type)
			: base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000D3D45 File Offset: 0x000D1F45
		private AttributeQuery(AttributeQuery other)
			: base(other)
		{
			this._onAttribute = other._onAttribute;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000D3D5A File Offset: 0x000D1F5A
		public override void Reset()
		{
			this._onAttribute = false;
			base.Reset();
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000D3D6C File Offset: 0x000D1F6C
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this._onAttribute)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this._onAttribute = this.currentNode.MoveToFirstAttribute();
				}
				else
				{
					this._onAttribute = this.currentNode.MoveToNextAttribute();
				}
				if (this._onAttribute && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000D3E02 File Offset: 0x000D2002
		public override XPathNodeIterator Clone()
		{
			return new AttributeQuery(this);
		}

		// Token: 0x040011D7 RID: 4567
		private bool _onAttribute;
	}
}
