using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000360 RID: 864
	internal sealed class PrecedingQuery : BaseAxisQuery
	{
		// Token: 0x06002671 RID: 9841 RVA: 0x000D6C17 File Offset: 0x000D4E17
		public PrecedingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
			this._ancestorStk = new ClonableStack<XPathNavigator>();
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000D6C2F File Offset: 0x000D4E2F
		private PrecedingQuery(PrecedingQuery other)
			: base(other)
		{
			this._workIterator = Query.Clone(other._workIterator);
			this._ancestorStk = other._ancestorStk.Clone();
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000D6C5A File Offset: 0x000D4E5A
		public override void Reset()
		{
			this._workIterator = null;
			this._ancestorStk.Clear();
			base.Reset();
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000D6C74 File Offset: 0x000D4E74
		public override XPathNavigator Advance()
		{
			if (this._workIterator == null)
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
				do
				{
					xpathNavigator2.MoveTo(xpathNavigator);
				}
				while ((xpathNavigator = this.qyInput.Advance()) != null);
				if (xpathNavigator2.NodeType == XPathNodeType.Attribute || xpathNavigator2.NodeType == XPathNodeType.Namespace)
				{
					xpathNavigator2.MoveToParent();
				}
				do
				{
					this._ancestorStk.Push(xpathNavigator2.Clone());
				}
				while (xpathNavigator2.MoveToParent());
				this._workIterator = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
			}
			while (this._workIterator.MoveNext())
			{
				this.currentNode = this._workIterator.Current;
				if (this.currentNode.IsSamePosition(this._ancestorStk.Peek()))
				{
					this._ancestorStk.Pop();
					if (this._ancestorStk.Count == 0)
					{
						this.currentNode = null;
						this._workIterator = null;
						return null;
					}
				}
				else if (this.matches(this.currentNode))
				{
					this.position++;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000D6D84 File Offset: 0x000D4F84
		public override XPathNodeIterator Clone()
		{
			return new PrecedingQuery(this);
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x000D6D8C File Offset: 0x000D4F8C
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x04001269 RID: 4713
		private XPathNodeIterator _workIterator;

		// Token: 0x0400126A RID: 4714
		private ClonableStack<XPathNavigator> _ancestorStk;
	}
}
