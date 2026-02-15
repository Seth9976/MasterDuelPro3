using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000339 RID: 825
	internal sealed class CacheChildrenQuery : ChildrenQuery
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x000D442E File Offset: 0x000D262E
		public CacheChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type)
			: base(qyInput, name, prefix, type)
		{
			this._elementStk = new ClonableStack<XPathNavigator>();
			this._positionStk = new ClonableStack<int>();
			this._needInput = true;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000D4458 File Offset: 0x000D2658
		private CacheChildrenQuery(CacheChildrenQuery other)
			: base(other)
		{
			this._nextInput = Query.Clone(other._nextInput);
			this._elementStk = other._elementStk.Clone();
			this._positionStk = other._positionStk.Clone();
			this._needInput = other._needInput;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000D44AB File Offset: 0x000D26AB
		public override void Reset()
		{
			this._nextInput = null;
			this._elementStk.Clear();
			this._positionStk.Clear();
			this._needInput = true;
			base.Reset();
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000D44D8 File Offset: 0x000D26D8
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this._needInput)
				{
					if (this._elementStk.Count == 0)
					{
						this.currentNode = this.GetNextInput();
						if (this.currentNode == null)
						{
							break;
						}
						if (!this.currentNode.MoveToFirstChild())
						{
							continue;
						}
						this.position = 0;
					}
					else
					{
						this.currentNode = this._elementStk.Pop();
						this.position = this._positionStk.Pop();
						if (!this.DecideNextNode())
						{
							continue;
						}
					}
					this._needInput = false;
				}
				else if (!this.currentNode.MoveToNext() || !this.DecideNextNode())
				{
					this._needInput = true;
					continue;
				}
				if (this.matches(this.currentNode))
				{
					goto Block_5;
				}
			}
			return null;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000D45A0 File Offset: 0x000D27A0
		private bool DecideNextNode()
		{
			this._nextInput = this.GetNextInput();
			if (this._nextInput != null && Query.CompareNodes(this.currentNode, this._nextInput) == XmlNodeOrder.After)
			{
				this._elementStk.Push(this.currentNode);
				this._positionStk.Push(this.position);
				this.currentNode = this._nextInput;
				this._nextInput = null;
				if (!this.currentNode.MoveToFirstChild())
				{
					return false;
				}
				this.position = 0;
			}
			return true;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000D4624 File Offset: 0x000D2824
		private XPathNavigator GetNextInput()
		{
			XPathNavigator xpathNavigator;
			if (this._nextInput != null)
			{
				xpathNavigator = this._nextInput;
				this._nextInput = null;
			}
			else
			{
				xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator != null)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
			}
			return xpathNavigator;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000D4660 File Offset: 0x000D2860
		public override XPathNodeIterator Clone()
		{
			return new CacheChildrenQuery(this);
		}

		// Token: 0x040011FC RID: 4604
		private XPathNavigator _nextInput;

		// Token: 0x040011FD RID: 4605
		private ClonableStack<XPathNavigator> _elementStk;

		// Token: 0x040011FE RID: 4606
		private ClonableStack<int> _positionStk;

		// Token: 0x040011FF RID: 4607
		private bool _needInput;
	}
}
