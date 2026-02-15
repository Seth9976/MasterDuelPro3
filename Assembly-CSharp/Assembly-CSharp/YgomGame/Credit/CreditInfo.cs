using System;

namespace YgomGame.Credit
{
	// Token: 0x02001011 RID: 4113
	[Serializable]
	public class CreditInfo
	{
		// Token: 0x06007BB7 RID: 31671 RVA: 0x00002739 File Offset: 0x00000939
		public CreditInfo()
		{
		}

		// Token: 0x06007BB8 RID: 31672 RVA: 0x00002739 File Offset: 0x00000939
		public CreditInfo(string group, string position, string name)
		{
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x00002739 File Offset: 0x00000939
		public CreditInfo(string group, string position, string name, string name2)
		{
		}

		// Token: 0x0400B386 RID: 45958
		public string group;

		// Token: 0x0400B387 RID: 45959
		public string position;

		// Token: 0x0400B388 RID: 45960
		public string name;

		// Token: 0x0400B389 RID: 45961
		public string name2;

		// Token: 0x0400B38A RID: 45962
		public int nameNum;
	}
}
