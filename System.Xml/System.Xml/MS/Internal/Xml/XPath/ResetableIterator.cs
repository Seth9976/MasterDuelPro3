using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000366 RID: 870
	internal abstract class ResetableIterator : XPathNodeIterator
	{
		// Token: 0x06002694 RID: 9876 RVA: 0x000D79CB File Offset: 0x000D5BCB
		public ResetableIterator()
		{
			this.count = -1;
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000D79DA File Offset: 0x000D5BDA
		protected ResetableIterator(ResetableIterator other)
		{
			this.count = other.count;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000D79EE File Offset: 0x000D5BEE
		protected void ResetCount()
		{
			this.count = -1;
		}

		// Token: 0x06002697 RID: 9879
		public abstract void Reset();

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002698 RID: 9880
		public abstract override int CurrentPosition { get; }
	}
}
