using System;
using System.Collections.Generic;

namespace YgomGame.Card
{
	// Token: 0x020010F0 RID: 4336
	public class CardAlphaSortName
	{
		// Token: 0x06008112 RID: 33042 RVA: 0x00002739 File Offset: 0x00000939
		public CardAlphaSortName(string assetPath)
		{
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSortName(byte[] bindata)
		{
		}

		// Token: 0x06008114 RID: 33044 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetSortName(int mrk)
		{
			return null;
		}

		// Token: 0x06008115 RID: 33045 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFixedSortName(int mrk)
		{
			return null;
		}

		// Token: 0x0400B990 RID: 47504
		private Dictionary<int, CardAlphaSortName.SortNameInfo> m_sortNameInfos;

		// Token: 0x020010F1 RID: 4337
		private class SortNameInfo
		{
			// Token: 0x0400B991 RID: 47505
			public int mrk;

			// Token: 0x0400B992 RID: 47506
			public string sortName;

			// Token: 0x0400B993 RID: 47507
			public string fixedSortName;
		}
	}
}
