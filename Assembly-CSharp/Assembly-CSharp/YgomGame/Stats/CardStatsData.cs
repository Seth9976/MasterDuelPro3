using System;

namespace YgomGame.Stats
{
	// Token: 0x020008EA RID: 2282
	public class CardStatsData
	{
		// Token: 0x060042E2 RID: 17122 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetItemString()
		{
			return null;
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetItemUnitString()
		{
			return null;
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetValueString()
		{
			return null;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x000029CC File Offset: 0x00000BCC
		public CardStatsData.CARD_STATS_EFFECT_TYPE GetValueEffectType()
		{
			return CardStatsData.CARD_STATS_EFFECT_TYPE.CARD_STATS_EFFECT_TYPE_0;
		}

		// Token: 0x04008146 RID: 33094
		public int m_Item;

		// Token: 0x04008147 RID: 33095
		public double m_fValue;

		// Token: 0x04008148 RID: 33096
		public string m_Value;

		// Token: 0x04008149 RID: 33097
		public CardStatsData.CARD_STATS_EFFECT_TYPE m_EffectType;

		// Token: 0x020008EB RID: 2283
		public enum CARD_STATS_EFFECT_TYPE
		{
			// Token: 0x0400814B RID: 33099
			CARD_STATS_EFFECT_TYPE_0,
			// Token: 0x0400814C RID: 33100
			CARD_STATS_EFFECT_TYPE_1,
			// Token: 0x0400814D RID: 33101
			CARD_STATS_EFFECT_TYPE_2,
			// Token: 0x0400814E RID: 33102
			CARD_STATS_EFFECT_TYPE_MAX
		}
	}
}
