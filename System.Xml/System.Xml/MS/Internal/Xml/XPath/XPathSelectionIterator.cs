using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000378 RID: 888
	internal class XPathSelectionIterator : ResetableIterator
	{
		// Token: 0x06002730 RID: 10032 RVA: 0x000D9F39 File Offset: 0x000D8139
		internal XPathSelectionIterator(XPathNavigator nav, Query query)
		{
			this._nav = nav.Clone();
			this._query = query;
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000D9F54 File Offset: 0x000D8154
		protected XPathSelectionIterator(XPathSelectionIterator it)
		{
			this._nav = it._nav.Clone();
			this._query = (Query)it._query.Clone();
			this._position = it._position;
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000D9F8F File Offset: 0x000D818F
		public override void Reset()
		{
			this._query.Reset();
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000D9F9C File Offset: 0x000D819C
		public override bool MoveNext()
		{
			XPathNavigator xpathNavigator = this._query.Advance();
			if (xpathNavigator != null)
			{
				this._position++;
				if (!this._nav.MoveTo(xpathNavigator))
				{
					this._nav = xpathNavigator.Clone();
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000D9FE3 File Offset: 0x000D81E3
		public override int Count
		{
			get
			{
				return this._query.Count;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000D9FF0 File Offset: 0x000D81F0
		public override XPathNavigator Current
		{
			get
			{
				return this._nav;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000D9FF8 File Offset: 0x000D81F8
		public override int CurrentPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000DA000 File Offset: 0x000D8200
		public override XPathNodeIterator Clone()
		{
			return new XPathSelectionIterator(this);
		}

		// Token: 0x040012D7 RID: 4823
		private XPathNavigator _nav;

		// Token: 0x040012D8 RID: 4824
		private Query _query;

		// Token: 0x040012D9 RID: 4825
		private int _position;
	}
}
