using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
	// Token: 0x020001F0 RID: 496
	public class ChainInfo
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x000288F4 File Offset: 0x00026AF4
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x000288FC File Offset: 0x00026AFC
		public ClientCard RelatedCard { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00028905 File Offset: 0x00026B05
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0002890D File Offset: 0x00026B0D
		public int ActivatePlayer { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00028916 File Offset: 0x00026B16
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0002891E File Offset: 0x00026B1E
		public int ActivateId { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00028927 File Offset: 0x00026B27
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0002892F File Offset: 0x00026B2F
		public int ActivateController { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00028938 File Offset: 0x00026B38
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00028940 File Offset: 0x00026B40
		public int ActivatePosition { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00028949 File Offset: 0x00026B49
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00028951 File Offset: 0x00026B51
		public int ActivateSequence { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002895A File Offset: 0x00026B5A
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x00028962 File Offset: 0x00026B62
		public CardLocation ActivateLocation { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0002896B File Offset: 0x00026B6B
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x00028973 File Offset: 0x00026B73
		public int ActivateLevel { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0002897C File Offset: 0x00026B7C
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x00028984 File Offset: 0x00026B84
		public int ActivateRank { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002898D File Offset: 0x00026B8D
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00028995 File Offset: 0x00026B95
		public int ActivateType { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002899E File Offset: 0x00026B9E
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x000289A6 File Offset: 0x00026BA6
		public int ActivateRace { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000289AF File Offset: 0x00026BAF
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x000289B7 File Offset: 0x00026BB7
		public int ActivateAttack { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000289C0 File Offset: 0x00026BC0
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x000289C8 File Offset: 0x00026BC8
		public int ActivateDefense { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x000289D1 File Offset: 0x00026BD1
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x000289D9 File Offset: 0x00026BD9
		public bool IsSpecialSummoned { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000289E2 File Offset: 0x00026BE2
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x000289EA File Offset: 0x00026BEA
		public int ActivateDescription { get; private set; }

		// Token: 0x060008ED RID: 2285 RVA: 0x000289F3 File Offset: 0x00026BF3
		public ChainInfo(ClientCard card)
			: this(card, card.Controller, 0)
		{
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00028A04 File Offset: 0x00026C04
		public ChainInfo(ClientCard card, int player, int desc)
		{
			this.RelatedCard = card;
			this.ActivatePlayer = player;
			this.ActivateId = card.Id;
			this.ActivateController = card.Controller;
			this.ActivatePosition = card.Position;
			this.ActivateSequence = card.Sequence;
			this.ActivateLocation = card.Location;
			this.ActivateLevel = card.Level;
			this.ActivateRank = card.Rank;
			this.ActivateType = card.Type;
			this.ActivateRace = card.Race;
			this.ActivateAttack = card.Attack;
			this.ActivateDefense = card.Defense;
			this.ActivateAttack = card.Attack;
			this.ActivateDefense = card.Defense;
			this.IsSpecialSummoned = card.IsSpecialSummoned;
			this.ActivateDescription = desc;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00028AD4 File Offset: 0x00026CD4
		public bool HasPosition(CardPosition position)
		{
			return (this.ActivatePosition & (int)position) != 0;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00028AE1 File Offset: 0x00026CE1
		public bool HasLocation(CardLocation location)
		{
			return (this.ActivateLocation & location) > (CardLocation)0;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00028AEE File Offset: 0x00026CEE
		public bool IsCode(int id)
		{
			return this.RelatedCard != null && this.RelatedCard.IsCode(id);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00028B06 File Offset: 0x00026D06
		public bool IsCode(IList<int> ids)
		{
			return this.RelatedCard != null && this.RelatedCard.IsCode(ids);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00028B1E File Offset: 0x00026D1E
		public bool IsCode(params int[] ids)
		{
			return this.RelatedCard != null && this.RelatedCard.IsCode(ids);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00028B36 File Offset: 0x00026D36
		public bool HasType(CardType type)
		{
			return this.RelatedCard != null && (this.RelatedCard.Type & (int)type) != 0;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00028B52 File Offset: 0x00026D52
		public bool IsSpell()
		{
			return this.HasType(CardType.Spell);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00028B5B File Offset: 0x00026D5B
		public bool IsTrap()
		{
			return this.HasType(CardType.Trap);
		}
	}
}
