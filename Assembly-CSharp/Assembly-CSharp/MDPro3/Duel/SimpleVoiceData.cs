using System;
using System.IO;
using MDPro3.Duel.YGOSharp;

namespace MDPro3.Duel
{
	// Token: 0x0200151B RID: 5403
	public class SimpleVoiceData
	{
		// Token: 0x06009D20 RID: 40224 RVA: 0x00195578 File Offset: 0x00193778
		public static SimpleVoiceData GetCardEffectSubCategory(BinaryReader r, bool fromHand)
		{
			SimpleVoiceData returnValue = new SimpleVoiceData
			{
				category = 7,
				subCategory = 12
			};
			r.BaseStream.Seek(0L, SeekOrigin.Begin);
			int code = r.ReadInt32();
			GPS gps = r.ReadGPS();
			returnValue.isMe = gps.controller == 0U;
			returnValue.inHand = (gps.location & 2U) > 0U;
			Card c = CardsManager.Get(code, false);
			if (GameCard.InPendulumZoneIf(gps, code) && fromHand)
			{
				returnValue.subCategory = 13;
			}
			else if (GameCard.InPendulumZoneIf(gps, code))
			{
				returnValue.subCategory = 14;
			}
			else if (c.HasType(CardType.Monster))
			{
				returnValue.subCategory = 10;
			}
			if ((gps.location & 4U) == 0U)
			{
				if (c.HasType(CardType.Spell))
				{
					returnValue.subCategory = 1;
					if (c.HasType(CardType.QuickPlay))
					{
						returnValue.subCategory = 2;
					}
					if (c.HasType(CardType.Continuous))
					{
						returnValue.subCategory = 3;
					}
					if (c.HasType(CardType.Equip))
					{
						returnValue.subCategory = 4;
					}
					if (c.HasType(CardType.Ritual))
					{
						returnValue.subCategory = 5;
					}
					if (c.HasType(CardType.Field))
					{
						returnValue.subCategory = 11;
					}
				}
				if (c.HasType(CardType.Trap))
				{
					returnValue.subCategory = 6;
					if (c.HasType(CardType.Continuous))
					{
						returnValue.subCategory = 7;
					}
					if (c.HasType(CardType.Counter))
					{
						returnValue.subCategory = 8;
					}
				}
			}
			return returnValue;
		}

		// Token: 0x0400DB38 RID: 56120
		public int category;

		// Token: 0x0400DB39 RID: 56121
		public int subCategory;

		// Token: 0x0400DB3A RID: 56122
		public bool isMe;

		// Token: 0x0400DB3B RID: 56123
		public bool inHand;
	}
}
