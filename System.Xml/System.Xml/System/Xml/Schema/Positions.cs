using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200021D RID: 541
	internal class Positions
	{
		// Token: 0x06001A98 RID: 6808 RVA: 0x0009A7EC File Offset: 0x000989EC
		public int Add(int symbol, object particle)
		{
			return this.positions.Add(new Position(symbol, particle));
		}

		// Token: 0x170005FE RID: 1534
		public Position this[int pos]
		{
			get
			{
				return (Position)this.positions[pos];
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0009A818 File Offset: 0x00098A18
		public int Count
		{
			get
			{
				return this.positions.Count;
			}
		}

		// Token: 0x04000B66 RID: 2918
		private ArrayList positions = new ArrayList();
	}
}
