using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200037A RID: 890
	internal class XPathSingletonIterator : ResetableIterator
	{
		// Token: 0x0600273C RID: 10044 RVA: 0x000DA052 File Offset: 0x000D8252
		public XPathSingletonIterator(XPathNavigator nav)
		{
			this._nav = nav;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000DA061 File Offset: 0x000D8261
		public XPathSingletonIterator(XPathNavigator nav, bool moved)
			: this(nav)
		{
			if (moved)
			{
				this._position = 1;
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000DA074 File Offset: 0x000D8274
		public XPathSingletonIterator(XPathSingletonIterator it)
		{
			this._nav = it._nav.Clone();
			this._position = it._position;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000DA099 File Offset: 0x000D8299
		public override XPathNodeIterator Clone()
		{
			return new XPathSingletonIterator(this);
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x000DA0A1 File Offset: 0x000D82A1
		public override XPathNavigator Current
		{
			get
			{
				return this._nav;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000DA0A9 File Offset: 0x000D82A9
		public override int CurrentPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000DA0B1 File Offset: 0x000D82B1
		public override bool MoveNext()
		{
			if (this._position == 0)
			{
				this._position = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000DA0C5 File Offset: 0x000D82C5
		public override void Reset()
		{
			this._position = 0;
		}

		// Token: 0x040012DA RID: 4826
		private XPathNavigator _nav;

		// Token: 0x040012DB RID: 4827
		private int _position;
	}
}
