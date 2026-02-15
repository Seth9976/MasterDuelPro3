using System;
using YgomGame.Card;

namespace YgomGame.Common
{
	// Token: 0x0200101B RID: 4123
	public class JsonPackDrawAnalyzer : JsonObjectAanalyzerBase
	{
		// Token: 0x06007BF3 RID: 31731 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetMrk(object drawData)
		{
			return 0;
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetPremium(object drawData)
		{
			return 0;
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsNew(object drawData)
		{
			return false;
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x000029CC File Offset: 0x00000BCC
		public CardCollectionInfo.Rarity BackSideRarity(object drawData)
		{
			return (CardCollectionInfo.Rarity)0;
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] GetFoundedSecrets(object drawData)
		{
			return null;
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFoundedSecrets(object drawData)
		{
			return false;
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] GetExtendedSecrets(object drawData)
		{
			return null;
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsExtendedSecrets(object drawData)
		{
			return false;
		}
	}
}
