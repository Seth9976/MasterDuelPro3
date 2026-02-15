using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000351 RID: 849
	internal class IteratorFilter : XPathNodeIterator
	{
		// Token: 0x060025FF RID: 9727 RVA: 0x000D5956 File Offset: 0x000D3B56
		internal IteratorFilter(XPathNodeIterator innerIterator, string name)
		{
			this._innerIterator = innerIterator;
			this._name = name;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000D596C File Offset: 0x000D3B6C
		private IteratorFilter(IteratorFilter it)
		{
			this._innerIterator = it._innerIterator.Clone();
			this._name = it._name;
			this._position = it._position;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000D599D File Offset: 0x000D3B9D
		public override XPathNodeIterator Clone()
		{
			return new IteratorFilter(this);
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000D59A5 File Offset: 0x000D3BA5
		public override XPathNavigator Current
		{
			get
			{
				return this._innerIterator.Current;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000D59B2 File Offset: 0x000D3BB2
		public override int CurrentPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000D59BA File Offset: 0x000D3BBA
		public override bool MoveNext()
		{
			while (this._innerIterator.MoveNext())
			{
				if (this._innerIterator.Current.LocalName == this._name)
				{
					this._position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400123E RID: 4670
		private XPathNodeIterator _innerIterator;

		// Token: 0x0400123F RID: 4671
		private string _name;

		// Token: 0x04001240 RID: 4672
		private int _position;
	}
}
