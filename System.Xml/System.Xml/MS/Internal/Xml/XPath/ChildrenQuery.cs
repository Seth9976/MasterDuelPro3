using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200033B RID: 827
	internal class ChildrenQuery : BaseAxisQuery
	{
		// Token: 0x0600257B RID: 9595 RVA: 0x000D475A File Offset: 0x000D295A
		public ChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type)
			: base(qyInput, name, prefix, type)
		{
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000D4772 File Offset: 0x000D2972
		protected ChildrenQuery(ChildrenQuery other)
			: base(other)
		{
			this._iterator = Query.Clone(other._iterator);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000D4797 File Offset: 0x000D2997
		public override void Reset()
		{
			this._iterator = XPathEmptyIterator.Instance;
			base.Reset();
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000D47AC File Offset: 0x000D29AC
		public override XPathNavigator Advance()
		{
			while (!this._iterator.MoveNext())
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				if (base.NameTest)
				{
					if (base.TypeTest == XPathNodeType.ProcessingInstruction)
					{
						this._iterator = new IteratorFilter(xpathNavigator.SelectChildren(base.TypeTest), base.Name);
					}
					else
					{
						this._iterator = xpathNavigator.SelectChildren(base.Name, base.Namespace);
					}
				}
				else
				{
					this._iterator = xpathNavigator.SelectChildren(base.TypeTest);
				}
				this.position = 0;
			}
			this.position++;
			this.currentNode = this._iterator.Current;
			return this.currentNode;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000D4864 File Offset: 0x000D2A64
		public override XPathNodeIterator Clone()
		{
			return new ChildrenQuery(this);
		}

		// Token: 0x04001202 RID: 4610
		private XPathNodeIterator _iterator = XPathEmptyIterator.Instance;
	}
}
