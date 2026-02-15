using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000342 RID: 834
	internal sealed class DescendantOverDescendantQuery : DescendantBaseQuery
	{
		// Token: 0x060025A2 RID: 9634 RVA: 0x000D4B44 File Offset: 0x000D2D44
		public DescendantOverDescendantQuery(Query qyParent, bool matchSelf, string name, string prefix, XPathNodeType typeTest, bool abbrAxis)
			: base(qyParent, name, prefix, typeTest, matchSelf, abbrAxis)
		{
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000D4B55 File Offset: 0x000D2D55
		private DescendantOverDescendantQuery(DescendantOverDescendantQuery other)
			: base(other)
		{
			this._level = other._level;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000D4B6A File Offset: 0x000D2D6A
		public override void Reset()
		{
			this._level = 0;
			base.Reset();
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000D4B7C File Offset: 0x000D2D7C
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				IL_0000:
				if (this._level == 0)
				{
					this.currentNode = this.qyInput.Advance();
					this.position = 0;
					if (this.currentNode == null)
					{
						break;
					}
					if (this.matchSelf && this.matches(this.currentNode))
					{
						goto Block_3;
					}
					this.currentNode = this.currentNode.Clone();
					if (!this.MoveToFirstChild())
					{
						continue;
					}
				}
				else if (!this.MoveUpUntilNext())
				{
					continue;
				}
				while (!this.matches(this.currentNode))
				{
					if (!this.MoveToFirstChild())
					{
						goto IL_0000;
					}
				}
				goto Block_5;
			}
			return null;
			Block_3:
			this.position = 1;
			return this.currentNode;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000D4C29 File Offset: 0x000D2E29
		private bool MoveToFirstChild()
		{
			if (this.currentNode.MoveToFirstChild())
			{
				this._level++;
				return true;
			}
			return false;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000D4C49 File Offset: 0x000D2E49
		private bool MoveUpUntilNext()
		{
			while (!this.currentNode.MoveToNext())
			{
				this._level--;
				if (this._level == 0)
				{
					return false;
				}
				this.currentNode.MoveToParent();
			}
			return true;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000D4C7F File Offset: 0x000D2E7F
		public override XPathNodeIterator Clone()
		{
			return new DescendantOverDescendantQuery(this);
		}

		// Token: 0x0400120B RID: 4619
		private int _level;
	}
}
