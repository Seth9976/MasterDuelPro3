using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000349 RID: 841
	internal sealed class FollowingQuery : BaseAxisQuery
	{
		// Token: 0x060025D6 RID: 9686 RVA: 0x000D3D38 File Offset: 0x000D1F38
		public FollowingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000D532B File Offset: 0x000D352B
		private FollowingQuery(FollowingQuery other)
			: base(other)
		{
			this._input = Query.Clone(other._input);
			this._iterator = Query.Clone(other._iterator);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000D5356 File Offset: 0x000D3556
		public override void Reset()
		{
			this._iterator = null;
			base.Reset();
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000D5368 File Offset: 0x000D3568
		public override XPathNavigator Advance()
		{
			if (this._iterator == null)
			{
				this._input = this.qyInput.Advance();
				if (this._input == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator;
				do
				{
					xpathNavigator = this._input.Clone();
					this._input = this.qyInput.Advance();
				}
				while (xpathNavigator.IsDescendant(this._input));
				this._input = xpathNavigator;
				this._iterator = XPathEmptyIterator.Instance;
			}
			while (!this._iterator.MoveNext())
			{
				bool flag;
				if (this._input.NodeType == XPathNodeType.Attribute || this._input.NodeType == XPathNodeType.Namespace)
				{
					this._input.MoveToParent();
					flag = false;
				}
				else
				{
					while (!this._input.MoveToNext())
					{
						if (!this._input.MoveToParent())
						{
							return null;
						}
					}
					flag = true;
				}
				if (base.NameTest)
				{
					this._iterator = this._input.SelectDescendants(base.Name, base.Namespace, flag);
				}
				else
				{
					this._iterator = this._input.SelectDescendants(base.TypeTest, flag);
				}
			}
			this.position++;
			this.currentNode = this._iterator.Current;
			return this.currentNode;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000D549C File Offset: 0x000D369C
		public override XPathNodeIterator Clone()
		{
			return new FollowingQuery(this);
		}

		// Token: 0x04001217 RID: 4631
		private XPathNavigator _input;

		// Token: 0x04001218 RID: 4632
		private XPathNodeIterator _iterator;
	}
}
