using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI
{
	// Token: 0x0200022B RID: 555
	public class CardSelector
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x00032A37 File Offset: 0x00030C37
		public CardSelector(ClientCard card)
		{
			this._type = CardSelector.SelectType.Card;
			this._card = card;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00032A4D File Offset: 0x00030C4D
		public CardSelector(IList<ClientCard> cards)
		{
			this._type = CardSelector.SelectType.Cards;
			this._cards = cards;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00032A63 File Offset: 0x00030C63
		public CardSelector(int cardId)
		{
			this._type = CardSelector.SelectType.Id;
			this._id = cardId;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00032A79 File Offset: 0x00030C79
		public CardSelector(IList<int> ids)
		{
			this._type = CardSelector.SelectType.Ids;
			this._ids = ids;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00032A8F File Offset: 0x00030C8F
		public CardSelector(CardLocation location)
		{
			this._type = CardSelector.SelectType.Location;
			this._location = location;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00032AA8 File Offset: 0x00030CA8
		public IList<ClientCard> Select(IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> result = new List<ClientCard>();
			switch (this._type)
			{
			case CardSelector.SelectType.Card:
				if (cards.Contains(this._card))
				{
					result.Add(this._card);
					goto IL_017F;
				}
				goto IL_017F;
			case CardSelector.SelectType.Cards:
			{
				using (IEnumerator<ClientCard> enumerator = this._cards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientCard card = enumerator.Current;
						if (cards.Contains(card) && !result.Contains(card))
						{
							result.Add(card);
						}
					}
					goto IL_017F;
				}
				break;
			}
			case CardSelector.SelectType.Id:
				break;
			case CardSelector.SelectType.Ids:
				goto IL_00D2;
			case CardSelector.SelectType.Location:
				goto IL_0143;
			default:
				goto IL_017F;
			}
			using (IEnumerator<ClientCard> enumerator = cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card2 = enumerator.Current;
					if (card2.IsCode(this._id))
					{
						result.Add(card2);
					}
				}
				goto IL_017F;
			}
			IL_00D2:
			using (IEnumerator<int> enumerator2 = this._ids.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int id = enumerator2.Current;
					foreach (ClientCard card3 in cards)
					{
						if (card3.IsCode(id) && !result.Contains(card3))
						{
							result.Add(card3);
						}
					}
				}
				goto IL_017F;
			}
			IL_0143:
			foreach (ClientCard card4 in cards)
			{
				if (card4.Location == this._location)
				{
					result.Add(card4);
				}
			}
			IL_017F:
			if (result.Count >= min)
			{
				goto IL_01D8;
			}
			using (IEnumerator<ClientCard> enumerator = cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card5 = enumerator.Current;
					if (!result.Contains(card5))
					{
						result.Add(card5);
					}
					if (result.Count >= min)
					{
						break;
					}
				}
				goto IL_01D8;
			}
			IL_01CA:
			result.RemoveAt(result.Count - 1);
			IL_01D8:
			if (result.Count <= max)
			{
				return result;
			}
			goto IL_01CA;
		}

		// Token: 0x04000E3B RID: 3643
		private CardSelector.SelectType _type;

		// Token: 0x04000E3C RID: 3644
		private ClientCard _card;

		// Token: 0x04000E3D RID: 3645
		private IList<ClientCard> _cards;

		// Token: 0x04000E3E RID: 3646
		private int _id;

		// Token: 0x04000E3F RID: 3647
		private IList<int> _ids;

		// Token: 0x04000E40 RID: 3648
		private CardLocation _location;

		// Token: 0x0200022C RID: 556
		private enum SelectType
		{
			// Token: 0x04000E42 RID: 3650
			Card,
			// Token: 0x04000E43 RID: 3651
			Cards,
			// Token: 0x04000E44 RID: 3652
			Id,
			// Token: 0x04000E45 RID: 3653
			Ids,
			// Token: 0x04000E46 RID: 3654
			Location
		}
	}
}
