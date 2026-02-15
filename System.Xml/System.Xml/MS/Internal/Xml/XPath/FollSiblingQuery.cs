using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000348 RID: 840
	internal sealed class FollSiblingQuery : BaseAxisQuery
	{
		// Token: 0x060025CF RID: 9679 RVA: 0x000D5114 File Offset: 0x000D3314
		public FollSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType type)
			: base(qyInput, name, prefix, type)
		{
			this._elementStk = new ClonableStack<XPathNavigator>();
			this._parentStk = new List<XPathNavigator>();
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000D5137 File Offset: 0x000D3337
		private FollSiblingQuery(FollSiblingQuery other)
			: base(other)
		{
			this._elementStk = other._elementStk.Clone();
			this._parentStk = new List<XPathNavigator>(other._parentStk);
			this._nextInput = Query.Clone(other._nextInput);
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000D5173 File Offset: 0x000D3373
		public override void Reset()
		{
			this._elementStk.Clear();
			this._parentStk.Clear();
			this._nextInput = null;
			base.Reset();
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000D5198 File Offset: 0x000D3398
		private bool Visited(XPathNavigator nav)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToParent();
			for (int i = 0; i < this._parentStk.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(this._parentStk[i]))
				{
					return true;
				}
			}
			this._parentStk.Add(xpathNavigator);
			return false;
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000D51EC File Offset: 0x000D33EC
		private XPathNavigator FetchInput()
		{
			XPathNavigator xpathNavigator;
			for (;;)
			{
				xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					break;
				}
				if (!this.Visited(xpathNavigator))
				{
					goto Block_1;
				}
			}
			return null;
			Block_1:
			return xpathNavigator.Clone();
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000D521C File Offset: 0x000D341C
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this.currentNode == null)
				{
					if (this._nextInput == null)
					{
						this._nextInput = this.FetchInput();
					}
					if (this._elementStk.Count == 0)
					{
						if (this._nextInput == null)
						{
							break;
						}
						this.currentNode = this._nextInput;
						this._nextInput = this.FetchInput();
					}
					else
					{
						this.currentNode = this._elementStk.Pop();
					}
				}
				while (this.currentNode.IsDescendant(this._nextInput))
				{
					this._elementStk.Push(this.currentNode);
					this.currentNode = this._nextInput;
					this._nextInput = this.qyInput.Advance();
					if (this._nextInput != null)
					{
						this._nextInput = this._nextInput.Clone();
					}
				}
				while (this.currentNode.MoveToNext())
				{
					if (this.matches(this.currentNode))
					{
						goto Block_6;
					}
				}
				this.currentNode = null;
			}
			return null;
			Block_6:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000D5323 File Offset: 0x000D3523
		public override XPathNodeIterator Clone()
		{
			return new FollSiblingQuery(this);
		}

		// Token: 0x04001214 RID: 4628
		private ClonableStack<XPathNavigator> _elementStk;

		// Token: 0x04001215 RID: 4629
		private List<XPathNavigator> _parentStk;

		// Token: 0x04001216 RID: 4630
		private XPathNavigator _nextInput;
	}
}
