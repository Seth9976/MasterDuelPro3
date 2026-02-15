using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000402 RID: 1026
	[Deck("ThunderDragon", "AI_ThunderDragon", "Normal")]
	internal class ThunderDragonExecutor : DefaultExecutor
	{
		// Token: 0x0600208E RID: 8334 RVA: 0x000CC530 File Offset: 0x000CA730
		public ThunderDragonExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.Impermanence_activate));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.GEffect));
			base.AddExecutor(ExecutorType.Activate, 4280258);
			base.AddExecutor(ExecutorType.Activate, 98127546);
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomEffect));
			base.AddExecutor(ExecutorType.Activate, 32731036, new Func<bool>(this.TheBystialLubellionEffect));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveEffect));
			base.AddExecutor(ExecutorType.Activate, 86066372, new Func<bool>(this.AccesscodeTalkerEffect));
			base.AddExecutor(ExecutorType.Activate, 34090915, new Func<bool>(this.BrandedRegainedEffect));
			base.AddExecutor(ExecutorType.Activate, 83152482, new Func<bool>(this.UnionCarrierEffect));
			base.AddExecutor(ExecutorType.SpSummon, 32731036, new Func<bool>(this.TheBystialLubellionSummon));
			base.AddExecutor(ExecutorType.Activate, 75500286, new Func<bool>(this.GoldSarcophagusEffect));
			base.AddExecutor(ExecutorType.Activate, 31786629, new Func<bool>(this.NormalThunderDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 20318029, new Func<bool>(this.ThunderDragonmatrixEffect));
			base.AddExecutor(ExecutorType.SpSummon, 73539069, new Func<bool>(this.StrikerDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 15291624, new Func<bool>(this.ThunderDragonColossusSummon_2));
			base.AddExecutor(ExecutorType.SpSummon, 50277355, new Func<bool>(this.CrossSheepSummon));
			base.AddExecutor(ExecutorType.Activate, 50277355, new Func<bool>(this.CrossSheepEffect));
			base.AddExecutor(ExecutorType.Activate, 90488465);
			base.AddExecutor(ExecutorType.Activate, 41685633, new Func<bool>(this.ThunderDragonTitanEffect));
			base.AddExecutor(ExecutorType.Activate, 15291624, new Func<bool>(this.ThunderDragonColossusEffect));
			base.AddExecutor(ExecutorType.SpSummon, 15291624, new Func<bool>(this.ThunderDragonColossusSummon));
			base.AddExecutor(ExecutorType.Summon, 92998610, new Func<bool>(this.AloofLupineSummon));
			base.AddExecutor(ExecutorType.Activate, 92998610, new Func<bool>(this.AloofLupineEffect));
			base.AddExecutor(ExecutorType.Summon, 44586426, new Func<bool>(this.BatterymanSolarSummon));
			base.AddExecutor(ExecutorType.Activate, 44586426, new Func<bool>(this.BatterymanSolarEffect));
			base.AddExecutor(ExecutorType.SpSummon, 99234526, new Func<bool>(this.WhiteDragonWyverbursterSummon));
			base.AddExecutor(ExecutorType.Activate, 99234526);
			base.AddExecutor(ExecutorType.SpSummon, 61901281, new Func<bool>(this.BlackDragonCollapserpentSummon));
			base.AddExecutor(ExecutorType.Activate, 61901281);
			base.AddExecutor(ExecutorType.SpSummon, 83152482, new Func<bool>(this.UnionCarrierSummon));
			base.AddExecutor(ExecutorType.Activate, 1475311, new Func<bool>(this.AllureofDarknessEffect));
			base.AddExecutor(ExecutorType.Activate, 83107873, new Func<bool>(this.ThunderDragonhawkEffect));
			base.AddExecutor(ExecutorType.Activate, 99266988, new Func<bool>(this.ChaosSpaceEffect));
			base.AddExecutor(ExecutorType.Activate, 33854624, new Func<bool>(this.BystialMagnamhutEffect));
			base.AddExecutor(ExecutorType.Activate, 6637331, new Func<bool>(this.BystialDruiswurmEffect));
			base.AddExecutor(ExecutorType.SpSummon, 5206415, new Func<bool>(this.ThunderDragonlordSummon));
			base.AddExecutor(ExecutorType.Activate, 70369116, new Func<bool>(this.PredaplantVerteAnacondaEffect));
			base.AddExecutor(ExecutorType.MonsterSet, 20318029, new Func<bool>(this.ThunderDragonmatrixSet));
			base.AddExecutor(ExecutorType.Activate, 95238394, new Func<bool>(this.ThunderDragonFusionEffect));
			base.AddExecutor(ExecutorType.Activate, 56713174, new Func<bool>(this.ThunderDragondarkEffect));
			base.AddExecutor(ExecutorType.Activate, 29596581, new Func<bool>(this.ThunderDragonroarEffect));
			base.AddExecutor(ExecutorType.Activate, 5206415, new Func<bool>(this.ThunderDragonlordEffect));
			base.AddExecutor(ExecutorType.SpSummon, 90488465, new Func<bool>(this.TheChaosCreatorSummon));
			base.AddExecutor(ExecutorType.SpSummon, 98127546, new Func<bool>(this.UnderworldGoddessoftheClosedWorldSummon));
			base.AddExecutor(ExecutorType.SpSummon, 4280258, new Func<bool>(this.BowoftheGoddessSummon));
			base.AddExecutor(ExecutorType.SpSummon, 83152482, new Func<bool>(this.UnionCarrierSummon_2));
			base.AddExecutor(ExecutorType.Activate, 65741786, new Func<bool>(this.IPEffect));
			base.AddExecutor(ExecutorType.SpSummon, 65741786, new Func<bool>(this.IPSummon));
			base.AddExecutor(ExecutorType.Activate, 38342335, new Func<bool>(this.KnightmareUnicornEffect));
			base.AddExecutor(ExecutorType.SpSummon, 38342335, new Func<bool>(this.KnightmareUnicornSummon));
			base.AddExecutor(ExecutorType.SpSummon, 86066372, new Func<bool>(this.BowoftheGoddessSummon));
			base.AddExecutor(ExecutorType.Activate, 21887175, new Func<bool>(this.MekkKnightCrusadiaAvramaxEffect));
			base.AddExecutor(ExecutorType.SpSummon, 21887175, new Func<bool>(this.MekkKnightCrusadiaAvramaxSummon));
			base.AddExecutor(ExecutorType.SpSummon, 70369116, new Func<bool>(this.PredaplantVerteAnacondaSummon));
			base.AddExecutor(ExecutorType.Activate, 41999284);
			base.AddExecutor(ExecutorType.SpSummon, 41999284);
			base.AddExecutor(ExecutorType.SpSummon, 21044178);
			base.AddExecutor(ExecutorType.Activate, 21044178, new Func<bool>(this.GEffect));
			base.AddExecutor(ExecutorType.Activate, 99266988, new Func<bool>(this.ChaosSpaceEffect_2));
			base.AddExecutor(ExecutorType.Activate, 20318029, new Func<bool>(this.ThunderDragonmatrixEffect_2));
			base.AddExecutor(ExecutorType.Summon, 20318029, new Func<bool>(this.ThunderDragonmatrixSummon));
			base.AddExecutor(ExecutorType.SpSummon, 61901281, new Func<bool>(this.BlackDragonCollapserpentSummon_2));
			base.AddExecutor(ExecutorType.Summon, 29596581, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Summon, 56713174, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Summon, 31786629, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Summon, 76218313, new Func<bool>(this.ThunderDragonmatrixSummon));
			base.AddExecutor(ExecutorType.Summon, 92998610, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Summon, 14558127, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Summon, 23434538, new Func<bool>(this.DefaultSummon));
			base.AddExecutor(ExecutorType.Activate, 56713174, new Func<bool>(this.ThunderDragondarkEffect_2));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
			base.AddExecutor(ExecutorType.Activate, 1475311, new Func<bool>(this.AllureofDarknessEffect_2));
			base.AddExecutor(ExecutorType.Activate, 83152482, new Func<bool>(this.UnionCarrierEffect_2));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000CCD18 File Offset: 0x000CAF18
		public int CheckRemainInDeck(int id)
		{
			if (id <= 33854624)
			{
				if (id <= 20318029)
				{
					if (id <= 6637331)
					{
						if (id == 1475311)
						{
							return base.Bot.GetRemainingCount(1475311, 3);
						}
						if (id == 5206415)
						{
							return base.Bot.GetRemainingCount(5206415, 1);
						}
						if (id == 6637331)
						{
							return base.Bot.GetRemainingCount(6637331, 2);
						}
					}
					else
					{
						if (id == 10045474)
						{
							return base.Bot.GetRemainingCount(10045474, 2);
						}
						if (id == 14558127)
						{
							return base.Bot.GetRemainingCount(14558127, 2);
						}
						if (id == 20318029)
						{
							return base.Bot.GetRemainingCount(20318029, 3);
						}
					}
				}
				else if (id <= 29596581)
				{
					if (id == 23434538)
					{
						return base.Bot.GetRemainingCount(23434538, 3);
					}
					if (id == 24224830)
					{
						return base.Bot.GetRemainingCount(24224830, 2);
					}
					if (id == 29596581)
					{
						return base.Bot.GetRemainingCount(29596581, 2);
					}
				}
				else
				{
					if (id == 31786629)
					{
						return base.Bot.GetRemainingCount(31786629, 3);
					}
					if (id == 32731036)
					{
						return base.Bot.GetRemainingCount(32731036, 2);
					}
					if (id == 33854624)
					{
						return base.Bot.GetRemainingCount(33854624, 2);
					}
				}
			}
			else if (id <= 76218313)
			{
				if (id <= 56713174)
				{
					if (id == 34090915)
					{
						return base.Bot.GetRemainingCount(34090915, 1);
					}
					if (id == 44586426)
					{
						return base.Bot.GetRemainingCount(44586426, 3);
					}
					if (id == 56713174)
					{
						return base.Bot.GetRemainingCount(56713174, 3);
					}
				}
				else
				{
					if (id == 61901281)
					{
						return base.Bot.GetRemainingCount(61901281, 2);
					}
					if (id == 75500286)
					{
						return base.Bot.GetRemainingCount(75500286, 1);
					}
					if (id == 76218313)
					{
						return base.Bot.GetRemainingCount(76218313, 1);
					}
				}
			}
			else if (id <= 92998610)
			{
				if (id == 83107873)
				{
					return base.Bot.GetRemainingCount(83107873, 2);
				}
				if (id == 90488465)
				{
					return base.Bot.GetRemainingCount(90488465, 1);
				}
				if (id == 92998610)
				{
					return base.Bot.GetRemainingCount(92998610, 2);
				}
			}
			else
			{
				if (id == 95238394)
				{
					return base.Bot.GetRemainingCount(95238394, 2);
				}
				if (id == 99234526)
				{
					return base.Bot.GetRemainingCount(99234526, 2);
				}
				if (id == 99266988)
				{
					return base.Bot.GetRemainingCount(99266988, 3);
				}
			}
			return 0;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000CD040 File Offset: 0x000CB240
		public override void OnNewTurn()
		{
			this.handActivated = false;
			this.isSummoned = false;
			this.No_SpSummon = false;
			this.activate_ThunderDragonFusion = false;
			this.activate_ThunderDragondark = false;
			this.activate_ThunderDragonroar = false;
			this.activate_ThunderDragonhawk = false;
			this.activate_ThunderDragonmatrix = false;
			this.activate_TheBystialLubellion_hand = false;
			this.activate_BystialMagnamhut_hand = false;
			this.activate_BystialDruiswurm_hand = false;
			this.activate_ChaosSpace_grave = false;
			this.summon_WhiteDragonWyverburster = false;
			this.summon_BlackDragonCollapserpent = false;
			this.summon_TheBystialLubellion = false;
			this.summon_UnionCarrier = false;
			for (int i = 0; i < this.selectAtt.Count; i++)
			{
				this.selectAtt[i] = false;
			}
			base.OnNewTurn();
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000CD0E8 File Offset: 0x000CB2E8
		private bool IsAvailableZone(int seq)
		{
			ClientCard card = base.Bot.MonsterZone[seq];
			return (seq != 5 || base.Bot.MonsterZone[6] == null || base.Bot.MonsterZone[6].Controller != 0) && (seq != 6 || base.Bot.MonsterZone[5] == null || base.Bot.MonsterZone[5].Controller != 0) && (card == null || (card.Controller == 0 && !card.IsFacedown() && (card.IsDisabled() || (card.Id != 15291624 && card.Id != 41685633 && card.Id != 98127546 && card.Id != 21887175 && card.Id != 86066372 && (card.Id != 4280258 || card.Attack <= 800) && (card.Id != 83152482 || !this.summon_UnionCarrier)))));
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000CD1EC File Offset: 0x000CB3EC
		private bool IsAvailableLinkZone()
		{
			int zones = 0;
			foreach (ClientCard card2 in (from card in base.Bot.GetMonstersInMainZone()
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>())
			{
				zones |= card2.GetLinkedZones();
			}
			ClientCard e_card = base.Bot.MonsterZone[5];
			if (e_card != null && e_card.IsFaceup() && e_card.HasType(CardType.Link))
			{
				if (e_card.Controller == 0)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.BottomLeft))
					{
						zones |= 1;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Bottom))
					{
						zones |= 2;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.BottomRight))
					{
						zones |= 4;
					}
				}
				if (e_card.Controller == 1)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.TopLeft))
					{
						zones |= 4;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Top))
					{
						zones |= 2;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.TopRight))
					{
						zones |= 1;
					}
				}
			}
			e_card = base.Bot.MonsterZone[6];
			if (e_card != null && e_card.IsFaceup() && e_card.HasType(CardType.Link))
			{
				if (e_card.Controller == 0)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.BottomLeft))
					{
						zones |= 4;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Bottom))
					{
						zones |= 8;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.BottomRight))
					{
						zones |= 16;
					}
				}
				if (e_card.Controller == 1)
				{
					if (e_card.HasLinkMarker(CardLinkMarker.TopLeft))
					{
						zones |= 16;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.Top))
					{
						zones |= 8;
					}
					if (e_card.HasLinkMarker(CardLinkMarker.TopRight))
					{
						zones |= 4;
					}
				}
			}
			zones &= 127;
			return ((zones & 1) > 0 && this.IsAvailableZone(0)) || ((zones & 2) > 0 && this.IsAvailableZone(1)) || ((zones & 4) > 0 && this.IsAvailableZone(2)) || ((zones & 8) > 0 && this.IsAvailableZone(3)) || ((zones & 16) > 0 && this.IsAvailableZone(4)) || this.IsAvailableZone(5) || this.IsAvailableZone(6);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000CD404 File Offset: 0x000CB604
		private void ResetFlag()
		{
			for (int i = 0; i < this.selectFlag.Count; i++)
			{
				this.selectFlag[i] = false;
			}
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000CD434 File Offset: 0x000CB634
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location == CardLocation.MonsterZone)
			{
				if (this.place_CrossSheep)
				{
					this.place_CrossSheep = false;
					if ((32 & available) > 0)
					{
						return 32;
					}
					if ((64 & available) > 0)
					{
						return 64;
					}
				}
				if (this.place_ThunderDragonColossus)
				{
					this.place_ThunderDragonColossus = false;
					if (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].IsCode(50277355))
					{
						if ((1 & available) > 0)
						{
							return 1;
						}
						if ((4 & available) > 0)
						{
							return 4;
						}
					}
					if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].IsCode(50277355))
					{
						if ((4 & available) > 0)
						{
							return 4;
						}
						if ((16 & available) > 0)
						{
							return 16;
						}
					}
					if ((4 & available) > 0)
					{
						return 4;
					}
					if ((1 & available) > 0)
					{
						return 1;
					}
					if ((16 & available) > 0)
					{
						return 16;
					}
				}
				if (this.place_Link_4)
				{
					this.place_Link_4 = false;
					if ((32 & available) > 0)
					{
						return 32;
					}
					if ((64 & available) > 0)
					{
						return 64;
					}
				}
				if (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].IsCode(50277355))
				{
					if ((1 & available) > 0 && base.Bot.MonsterZone[2] != null && base.Bot.MonsterZone[2].HasType(CardType.Fusion) && base.Bot.MonsterZone[2].IsFaceup())
					{
						return 1;
					}
					if ((4 & available) > 0 && base.Bot.MonsterZone[0] != null && base.Bot.MonsterZone[0].HasType(CardType.Fusion) && base.Bot.MonsterZone[0].IsFaceup())
					{
						return 4;
					}
				}
				if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].IsCode(50277355))
				{
					if ((4 & available) > 0 && base.Bot.MonsterZone[4] != null && base.Bot.MonsterZone[4].HasType(CardType.Fusion) && base.Bot.MonsterZone[4].IsFaceup())
					{
						return 4;
					}
					if ((16 & available) > 0 && base.Bot.MonsterZone[2] != null && base.Bot.MonsterZone[2].HasType(CardType.Fusion) && base.Bot.MonsterZone[2].IsFaceup())
					{
						return 16;
					}
				}
				return base.OnSelectPlace(cardId, player, location, available);
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000CD6BC File Offset: 0x000CB8BC
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (this.selectFlag.Count((bool flag) => flag) > 1)
			{
				if (this.selectFlag.Count((bool flag) => flag) == 2 && this.selectFlag[1] && !this.selectFlag[6])
				{
					this.selectFlag[1] = false;
				}
				else
				{
					if (this.selectFlag.Count((bool flag) => flag) != 2 || !this.selectFlag[6])
					{
						this.ResetFlag();
						return null;
					}
					this.selectFlag[6] = false;
				}
			}
			if (this.selectFlag[0])
			{
				this.selectFlag[0] = false;
				if (cards.Any((ClientCard card) => card != null && card.Controller != 0))
				{
					return null;
				}
				if (cards.Count <= 1)
				{
					return null;
				}
				if (base.Bot.HasInHand(99266988) && !this.activate_ChaosSpace_hand)
				{
					return base.Util.CheckSelectCount(cards, cards, max, max);
				}
				if (base.Bot.HasInHand(92998610) && !this.isSummoned && this.GetRemainingThunderCount(true) > 0)
				{
					return base.Util.CheckSelectCount(cards, cards, max, max);
				}
				if (base.Bot.HasInHand(95238394) && !this.activate_ThunderDragonFusion)
				{
					return base.Util.CheckSelectCount(cards, cards, min, min);
				}
				if (this.HasInZoneNoActivate(61901281, CardLocation.Hand, false) || this.HasInZoneNoActivate(6637331, CardLocation.Hand, false) || this.HasInZoneNoActivate(33854624, CardLocation.Hand, false) || this.HasInZoneNoActivate(32731036, CardLocation.Hand, false))
				{
					return base.Util.CheckSelectCount(cards, cards, min, min);
				}
				if (this.HasInZoneNoActivate(83107873, CardLocation.Hand, false))
				{
					return base.Util.CheckSelectCount(cards, cards, min, min);
				}
				return base.Util.CheckSelectCount(cards, cards, max, max);
			}
			else if (this.selectFlag[2])
			{
				this.selectFlag[2] = false;
				this.selectFlag[3] = true;
				List<ClientCard> res = new List<ClientCard>();
				if (cards.Any((ClientCard card) => card != null && card.IsCode(29596581)) && !this.activate_ThunderDragonroar)
				{
					res.AddRange(cards.Where((ClientCard card) => card != null && card.IsCode(29596581)).ToList<ClientCard>());
				}
				if (cards.Any((ClientCard card) => card != null && card.IsCode(56713174)) && !this.activate_ThunderDragondark)
				{
					res.AddRange(cards.Where((ClientCard card) => card != null && card.IsCode(56713174)).ToList<ClientCard>());
				}
				if (cards.Any((ClientCard card) => card != null && card.IsCode(56713174)) && !this.activate_ThunderDragondark)
				{
					res.AddRange(cards.Where((ClientCard card) => card != null && card.IsCode(56713174)).ToList<ClientCard>());
				}
				if (cards.Any((ClientCard card) => card != null && card.IsCode(31786629)))
				{
					res.AddRange(cards.Where((ClientCard card) => card != null && card.IsCode(31786629)).ToList<ClientCard>());
				}
				if (cards.Any((ClientCard card) => card != null && card.IsCode(20318029)))
				{
					res.AddRange(cards.Where((ClientCard card) => card != null && card.IsCode(20318029)).ToList<ClientCard>());
				}
				if (res.Count <= 0)
				{
					return null;
				}
				return base.Util.CheckSelectCount(res, cards, min, max);
			}
			else if (this.selectFlag[3])
			{
				this.selectFlag[3] = false;
				List<ClientCard> res2 = new List<ClientCard>();
				if (cards.Any((ClientCard card) => card != null && (card.Id == 61901281 || card.Id == 90488465)))
				{
					if (!this.summon_BlackDragonCollapserpent && cards.Any((ClientCard card) => card != null && card.IsCode(61901281) && !base.Bot.HasInHand(61901281)))
					{
						IList<ClientCard> cards_ = cards.Where((ClientCard card) => card != null && card.IsCode(61901281)).ToList<ClientCard>();
						IList<ClientCard> cards_2 = cards.Where((ClientCard card) => card != null && !card.IsCode(61901281)).ToList<ClientCard>();
						res2.AddRange(cards_);
						res2.AddRange(cards_2);
						return base.Util.CheckSelectCount(res2, cards, min, max);
					}
					res2 = cards.ToList<ClientCard>();
					res2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
					res2.Reverse();
					return base.Util.CheckSelectCount(res2, cards, min, max);
				}
				else
				{
					if (!cards.Any((ClientCard card) => card != null && (card.Id == 99234526 || card.Id == 5206415 || card.Id == 32731036)))
					{
						return null;
					}
					if (!this.summon_WhiteDragonWyverburster && cards.Any((ClientCard card) => card != null && card.IsCode(99234526) && (base.Bot.HasInExtra(73539069) || base.Bot.HasInExtra(83152482)) && !base.Bot.HasInHand(99234526)))
					{
						IList<ClientCard> cards_3 = cards.Where((ClientCard card) => card != null && card.IsCode(99234526)).ToList<ClientCard>();
						IList<ClientCard> cards_4 = cards.Where((ClientCard card) => card != null && !card.IsCode(99234526)).ToList<ClientCard>();
						res2.AddRange(cards_3);
						res2.AddRange(cards_4);
						return base.Util.CheckSelectCount(res2, cards, min, max);
					}
					if (!this.activate_TheBystialLubellion_hand && (this.HasInZoneNoActivate(33854624, CardLocation.Deck, false) || (this.HasInZoneNoActivate(6637331, CardLocation.Deck, false) && !base.Bot.HasInHand(32731036))))
					{
						if (cards.Any((ClientCard card) => card != null && card.IsCode(32731036)))
						{
							IList<ClientCard> cards_5 = cards.Where((ClientCard card) => card != null && card.IsCode(32731036)).ToList<ClientCard>();
							IList<ClientCard> cards_6 = cards.Where((ClientCard card) => card != null && !card.IsCode(32731036)).ToList<ClientCard>();
							res2.AddRange(cards_5);
							res2.AddRange(cards_6);
							return base.Util.CheckSelectCount(res2, cards, min, max);
						}
					}
					if (!this.summon_WhiteDragonWyverburster && !base.Bot.HasInHand(99234526))
					{
						if (cards.Any((ClientCard card) => card != null && card.IsCode(99234526)))
						{
							IList<ClientCard> cards_7 = cards.Where((ClientCard card) => card != null && card.IsCode(99234526)).ToList<ClientCard>();
							IList<ClientCard> cards_8 = cards.Where((ClientCard card) => card != null && !card.IsCode(99234526)).ToList<ClientCard>();
							res2.AddRange(cards_7);
							res2.AddRange(cards_8);
							return base.Util.CheckSelectCount(res2, cards, min, max);
						}
					}
					res2 = cards.ToList<ClientCard>();
					res2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
					return base.Util.CheckSelectCount(res2, cards, min, max);
				}
			}
			else if (this.selectFlag[4])
			{
				this.selectFlag[4] = false;
				if (cards.Count < 2)
				{
					return null;
				}
				List<ClientCard> copy_cards = new List<ClientCard>(cards);
				copy_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				new List<ClientCard>();
				for (int i = 0; i < copy_cards.Count; i++)
				{
					if (((copy_cards[i].Id == 29596581 && this.HasInZoneNoActivate(29596581, CardLocation.MonsterZone, false) && base.Bot.GetMonstersInMainZone().Count < 5) || (copy_cards[i].Id == 56713174 && this.HasInZoneNoActivate(56713174, CardLocation.MonsterZone, false)) || (copy_cards[i].Id == 20318029 && this.HasInZoneNoActivate(20318029, CardLocation.MonsterZone, false))) && i > 0)
					{
						ClientCard temp = copy_cards[0];
						copy_cards[0] = copy_cards[i];
						copy_cards[i] = temp;
					}
				}
				return base.Util.CheckSelectCount(copy_cards, cards, min, max);
			}
			else if (this.selectFlag[5])
			{
				this.selectFlag[5] = false;
				List<ClientCard> copy_cards2 = new List<ClientCard>(cards);
				copy_cards2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				List<ClientCard> res3 = new List<ClientCard>();
				res3.AddRange(copy_cards2.Where((ClientCard card) => card != null && card.Location == CardLocation.Grave));
				res3.AddRange(copy_cards2.Where((ClientCard card) => card != null && card.Location != CardLocation.Grave));
				if (res3.Count <= 0)
				{
					return null;
				}
				CardAttribute att = (CardAttribute)res3[0].Attribute;
				if (this.GetAttIndex(att) > 0)
				{
					this.selectAtt[this.GetAttIndex(att)] = true;
				}
				return base.Util.CheckSelectCount(res3, cards, min, max);
			}
			else
			{
				if (this.selectFlag[6])
				{
					this.selectFlag[6] = false;
					if (min == 1 && max == 1)
					{
						List<ClientCard> list = new List<ClientCard>(cards);
						list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						List<ClientCard> res4 = new List<ClientCard>();
						List<ClientCard> pre_res = new List<ClientCard>();
						foreach (ClientCard card6 in list)
						{
							if (card6 != null)
							{
								if (card6.Id == 29596581 && !this.activate_ThunderDragonroar && base.Bot.GetMonstersInMainZone().Count < 5)
								{
									res4.Add(card6);
								}
								else if (card6.Id == 56713174 && !this.activate_ThunderDragondark)
								{
									res4.Add(card6);
								}
								else
								{
									pre_res.Add(card6);
								}
							}
						}
						res4.Reverse();
						res4.AddRange(pre_res);
						if (res4.Count >= 0)
						{
							return base.Util.CheckSelectCount(res4, cards, min, max);
						}
						return null;
					}
					else if (min == 2 && max == 2)
					{
						List<ClientCard> res5 = new List<ClientCard>();
						if (!this.activate_ThunderDragonroar && base.Bot.GetMonstersInMainZone().Count < 5)
						{
							foreach (ClientCard card2 in cards)
							{
								if (card2.Id == 29596581)
								{
									if (res5.Count((ClientCard _card) => _card != null && _card.Id == 29596581) <= 0)
									{
										res5.Add(card2);
									}
								}
							}
						}
						if (!this.activate_ThunderDragondark)
						{
							foreach (ClientCard card3 in cards)
							{
								if (card3.Id == 56713174)
								{
									if (res5.Count((ClientCard _card) => _card != null && _card.Id == 56713174) <= 0)
									{
										res5.Add(card3);
									}
								}
							}
						}
						if (!this.activate_ThunderDragonhawk && !this.GetZoneRepeatCardsId(0, base.Bot.Hand, false).Contains(-1))
						{
							foreach (ClientCard card4 in cards)
							{
								if (card4.Id == 83107873)
								{
									if (res5.Count((ClientCard _card) => _card != null && _card.Id == 83107873) <= 0)
									{
										res5.Add(card4);
									}
								}
							}
						}
						if (this.HasInZoneNoActivate(20318029, CardLocation.Deck, false))
						{
							foreach (ClientCard card5 in cards)
							{
								if (card5.Id == 20318029)
								{
									if (res5.Count((ClientCard _card) => _card != null && _card.Id == 20318029) <= 0)
									{
										res5.Add(card5);
									}
								}
							}
						}
						List<ClientCard> scards = cards.Where((ClientCard card) => card != null && card.Id != 99266988 && card.Id != 95238394).ToList<ClientCard>();
						if (scards.Count > 0)
						{
							res5.AddRange(scards);
						}
						List<ClientCard> mcards = cards.Where((ClientCard card) => card != null && !card.HasRace(CardRace.Thunder)).ToList<ClientCard>();
						mcards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						mcards.Reverse();
						if (mcards.Count > 0)
						{
							res5.AddRange(mcards);
						}
						if (res5.Count > 0)
						{
							return base.Util.CheckSelectCount(res5, cards, min, max);
						}
						return null;
					}
				}
				if (hint == 511)
				{
					List<ClientCard> res6 = new List<ClientCard>();
					List<ClientCard> banish = cards.Where((ClientCard card) => card != null && card.Location == CardLocation.Removed).ToList<ClientCard>();
					if (banish.Count > 0)
					{
						res6.AddRange(banish);
					}
					List<ClientCard> grave_ = cards.Where((ClientCard card) => card != null && card.Location == CardLocation.Grave && card.Id != 29596581 && card.Id != 56713174).ToList<ClientCard>();
					List<ClientCard> grave_2 = cards.Where((ClientCard card) => card != null && card.Location == CardLocation.Grave && (card.Id == 29596581 || card.Id == 56713174)).ToList<ClientCard>();
					if (grave_.Count > 0)
					{
						res6.AddRange(grave_);
					}
					if (grave_2.Count > 0)
					{
						res6.AddRange(grave_2);
					}
					List<ClientCard> monsters = cards.Where((ClientCard card) => card != null && card.Location == CardLocation.MonsterZone).ToList<ClientCard>();
					monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					if (monsters.Count > 0)
					{
						res6.AddRange(monsters);
					}
					if (res6.Count > 0)
					{
						return base.Util.CheckSelectCount(res6, cards, min, max);
					}
					return null;
				}
				else if (base.Duel.Phase == DuelPhase.End && hint == 506)
				{
					List<ClientCard> res7 = new List<ClientCard>();
					List<ClientCard> cards_9 = cards.Where((ClientCard card) => card != null && card.Id == 6637331).ToList<ClientCard>();
					List<ClientCard> cards_10 = cards.Where((ClientCard card) => card != null && (card.Id == 99234526 || card.Id == 61901281)).ToList<ClientCard>();
					List<ClientCard> cards_11 = cards.Where((ClientCard card) => card != null && card.Id != 6637331 && card.Id != 99234526 && card.Id != 61901281).ToList<ClientCard>();
					if (cards_9.Count > 0)
					{
						res7.AddRange(cards_9);
					}
					if (cards_10.Count > 0)
					{
						res7.AddRange(cards_10);
					}
					if (cards_11.Count > 0)
					{
						res7.AddRange(cards_11);
					}
					if (res7.Count > 0)
					{
						return base.Util.CheckSelectCount(res7, cards, min, max);
					}
					return null;
				}
				else if (hint == 574)
				{
					if (!cards.Any((ClientCard card) => card != null && card.Location == CardLocation.Removed))
					{
						return null;
					}
					this.selectFlag[1] = true;
					List<ClientCard> res8 = new List<ClientCard>();
					List<ClientCard> cards_12 = cards.Where((ClientCard card) => card != null && card.Controller == 0 && (card.IsCode(15291624) || card.IsCode(41685633))).ToList<ClientCard>();
					if (cards_12.Count > 0)
					{
						res8.AddRange(cards_12);
					}
					List<ClientCard> cards_13 = cards.Where((ClientCard card) => card != null && card.Controller == 0 && !card.IsCode(15291624) && !card.IsCode(41685633)).ToList<ClientCard>();
					if (cards_13.Count > 0)
					{
						res8.AddRange(cards_13);
					}
					List<ClientCard> cards_14 = cards.Where((ClientCard card) => card != null && card.Controller == 1).ToList<ClientCard>();
					if (cards_14.Count > 0)
					{
						res8.AddRange(cards_14);
					}
					if (res8.Count > 0)
					{
						return base.Util.CheckSelectCount(res8, cards, min, max);
					}
					return null;
				}
				else
				{
					if (!this.selectFlag[1])
					{
						return base.OnSelectCard(cards, min, max, hint, cancelable);
					}
					this.selectFlag[1] = false;
					List<ClientCard> res9 = new List<ClientCard>(cards);
					res9.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					if (res9.Count <= 0)
					{
						return null;
					}
					if (res9[0].Attack < res9[res9.Count - 1].Attack)
					{
						res9.Reverse();
					}
					return base.Util.CheckSelectCount(res9, cards, min, max);
				}
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000CE8B8 File Offset: 0x000CCAB8
		private bool SpellSet()
		{
			if (!base.Bot.HasInHand(23434538) && base.Bot.HasInHand(1475311))
			{
				return base.Bot.GetSpellCountWithoutField() < 4 && base.Card.Id != 1475311;
			}
			return base.Card.HasType(CardType.QuickPlay) || base.Card.HasType(CardType.Trap) || base.Card.Id == 95238394;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000CE944 File Offset: 0x000CCB44
		private bool BrandedRegainedEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			base.AI.SelectCard(new int[] { 6637331, 33854624, 32731036 });
			return true;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000CE994 File Offset: 0x000CCB94
		private int GetAttIndex(CardAttribute att)
		{
			if (att <= CardAttribute.Wind)
			{
				switch (att)
				{
				case CardAttribute.Earth:
					return 0;
				case CardAttribute.Water:
					return 1;
				case (CardAttribute)3:
					break;
				case CardAttribute.Fire:
					return 2;
				default:
					if (att == CardAttribute.Wind)
					{
						return 3;
					}
					break;
				}
			}
			else
			{
				if (att == CardAttribute.Light)
				{
					return 4;
				}
				if (att == CardAttribute.Dark)
				{
					return 5;
				}
				if (att == CardAttribute.Divine)
				{
					return 6;
				}
			}
			return -1;
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000CE9E4 File Offset: 0x000CCBE4
		public bool Impermanence_activate()
		{
			foreach (ClientCard i in base.Enemy.GetMonsters())
			{
				if (i.IsMonsterShouldBeDisabledBeforeItUseEffect() && !i.IsDisabled() && base.Duel.LastChainPlayer != 0)
				{
					if (base.Card.Location == CardLocation.SpellZone)
					{
						for (int j = 0; j < 5; j++)
						{
							if (base.Bot.SpellZone[j] == base.Card)
							{
								this.Impermanence_list.Add(j);
								break;
							}
						}
					}
					if (base.Card.Location == CardLocation.Hand)
					{
						base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
					}
					base.AI.SelectCard(i);
					return true;
				}
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int this_seq = -1;
				int that_seq = -1;
				for (int k = 0; k < 5; k++)
				{
					if (base.Bot.SpellZone[k] == base.Card)
					{
						this_seq = k;
					}
					if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && base.Enemy.SpellZone[k] == LastChainCard)
					{
						that_seq = k;
					}
					else if (base.Duel.Player == 0 && base.Util.GetProblematicEnemySpell() != null && base.Enemy.SpellZone[k] != null && base.Enemy.SpellZone[k].IsFloodgate())
					{
						that_seq = k;
					}
				}
				if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4) || base.Util.IsChainTarget(base.Card) || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(18144506)))
				{
					List<ClientCard> monsters = base.Enemy.GetMonsters();
					monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					monsters.Reverse();
					foreach (ClientCard card in monsters)
					{
						if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
						{
							base.AI.SelectCard(card);
							this.Impermanence_list.Add(this_seq);
							return true;
						}
					}
				}
			}
			if (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
			{
				return false;
			}
			if (this.is_should_not_negate() && LastChainCard.Location == CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				for (int l = 0; l < 5; l++)
				{
					if (base.Bot.SpellZone[l] == base.Card)
					{
						this.Impermanence_list.Add(l);
						break;
					}
				}
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			if (LastChainCard != null)
			{
				base.AI.SelectCard(LastChainCard);
			}
			else
			{
				List<ClientCard> monsters2 = base.Enemy.GetMonsters();
				monsters2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters2.Reverse();
				foreach (ClientCard card2 in monsters2)
				{
					if (card2.IsFaceup() && !card2.IsShouldNotBeTarget() && !card2.IsShouldNotBeSpellTrapTarget())
					{
						base.AI.SelectCard(card2);
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000CEDB8 File Offset: 0x000CCFB8
		public int SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false)
		{
			List<int> list = new List<int> { 0, 1, 2, 3, 4 };
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				int temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
			foreach (int seq in list)
			{
				int zone = (int)Math.Pow(2.0, (double)seq);
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoid_Impermanence || !this.Impermanence_list.Contains(seq)))
				{
					return zone;
				}
			}
			return 0;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000CEEB8 File Offset: 0x000CD0B8
		public bool is_should_not_negate()
		{
			ClientCard last_card = base.Util.GetLastChainCard();
			return last_card != null && last_card.Controller == 1 && last_card.IsCode(this.should_not_negate);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000CEEF0 File Offset: 0x000CD0F0
		private bool MekkKnightCrusadiaAvramaxEffect()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return true;
			}
			List<ClientCard> cards = base.Enemy.GetMonsters();
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			cards.AddRange(base.Enemy.GetSpells());
			if (cards.Count <= 0)
			{
				return false;
			}
			base.AI.SelectCard(cards);
			return true;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000CEF5B File Offset: 0x000CD15B
		private bool ThunderDragonColossusEffect()
		{
			this.selectFlag[6] = true;
			return true;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000CEF6C File Offset: 0x000CD16C
		private IList<CardAttribute> GetAttUsed()
		{
			IList<CardAttribute> attributes = new List<CardAttribute>();
			for (int i = 0; i < this.selectAtt.Count; i++)
			{
				if (this.selectAtt[i])
				{
					attributes.Add((CardAttribute)Math.Pow(2.0, (double)i));
				}
			}
			if (attributes.Count > 0)
			{
				return attributes;
			}
			return null;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000CEFC8 File Offset: 0x000CD1C8
		private int GetRemainingThunderCount(bool isOnlyTunder = false)
		{
			int remaining = 18;
			if (isOnlyTunder)
			{
				remaining -= 4;
			}
			remaining -= base.Bot.Hand.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && !card.IsExtraCard() && !(isOnlyTunder & !this.Card.HasSetcode(284)));
			remaining -= base.Bot.SpellZone.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && !card.IsExtraCard() && !(isOnlyTunder & !this.Card.HasSetcode(284)));
			remaining -= base.Bot.MonsterZone.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && !card.IsExtraCard() && !(isOnlyTunder & !this.Card.HasSetcode(284)));
			remaining -= base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && !card.IsExtraCard() && !(isOnlyTunder & !this.Card.HasSetcode(284)));
			remaining -= base.Bot.Banished.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && !card.IsExtraCard() && !(isOnlyTunder & !this.Card.HasSetcode(284)));
			if (remaining >= 0)
			{
				return remaining;
			}
			return 0;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000CF09C File Offset: 0x000CD29C
		private int GetLinkMark(int cardId)
		{
			if (cardId == 41999284 || cardId == 73539069)
			{
				return 1;
			}
			if (cardId == 70369116 || cardId == 50277355 || cardId == 65741786 || cardId == 83152482)
			{
				return 2;
			}
			if (cardId == 38342335)
			{
				return 3;
			}
			if (cardId == 4280258 || cardId == 86066372 || cardId == 21887175)
			{
				return 4;
			}
			if (cardId == 98127546)
			{
				return 5;
			}
			return 1;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000CF10C File Offset: 0x000CD30C
		private bool AshBlossomEffect()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.CurrentChain.Count > 0 && base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000CF141 File Offset: 0x000CD341
		public int CompareCardLink(ClientCard cardA, ClientCard cardB)
		{
			if (cardA.LinkCount < cardB.LinkCount)
			{
				return -1;
			}
			if (cardA.LinkCount == cardB.LinkCount)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000CF164 File Offset: 0x000CD364
		private IList<ClientCard> CardsIdToClientCards(IList<int> cardsId, IList<ClientCard> cardsList, bool uniqueId = true, bool alias = true)
		{
			if ((cardsList != null && cardsList.Count<ClientCard>() <= 0) || (cardsId != null && cardsId.Count<int>() <= 0))
			{
				return new List<ClientCard>();
			}
			List<ClientCard> res = new List<ClientCard>();
			using (IEnumerator<int> enumerator = cardsId.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int cardid = enumerator.Current;
					List<ClientCard> cards = cardsList.Where((ClientCard card) => card != null && (card.Id == cardid || ((card.Alias != 0 && cardid == card.Alias) & alias))).ToList<ClientCard>();
					if (cards == null || cards.Count > 0)
					{
						cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						if (uniqueId)
						{
							res.Add(cards.First<ClientCard>());
						}
						else
						{
							res.AddRange(cards);
						}
					}
				}
			}
			return res;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000CF240 File Offset: 0x000CD440
		private bool IPEffect()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if (!base.Bot.HasInExtra(38342335) && !base.Bot.HasInExtra(21887175))
			{
				return false;
			}
			int[] materials = new int[] { 70369116, 83152482, 50277355 };
			if (!base.Bot.HasInExtra(21887175))
			{
				if (base.Bot.HasInExtra(38342335))
				{
					if (base.Bot.Hand.Count <= 0)
					{
						return false;
					}
					List<ClientCard> monsters = base.Enemy.GetMonsters();
					monsters.AddRange(base.Enemy.GetSpells());
					if (monsters.Count((ClientCard card) => card4 != null && !card4.IsShouldNotBeTarget()) <= 0)
					{
						return false;
					}
					List<ClientCard> materials_2 = new List<ClientCard>();
					List<ClientCard> resMaterials = new List<ClientCard>();
					using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ClientCard card4 = enumerator.Current;
							if (card4 != null && (card4.Id != 83152482 || !this.summon_UnionCarrier) && (this.GetLinkMark(card4.Id) < 3 || (card4.Id == 4280258 && card4.Attack <= 800)) && card4.Id != 41685633 && card4.Id != 15291624 && card4.IsFaceup() && materials_2.Count((ClientCard _card) => _card != null && _card.Id == card4.Id) <= 0)
							{
								materials_2.Add(card4);
							}
						}
					}
					int link_count = 0;
					materials_2.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					materials_2.Sort(new Comparison<ClientCard>(this.CompareCardLink));
					materials_2.Reverse();
					if (materials_2.Count <= 0)
					{
						return false;
					}
					foreach (ClientCard card3 in materials_2)
					{
						if (!resMaterials.Contains(card3))
						{
							resMaterials.Add(card3);
							link_count += (card3.HasType(CardType.Link) ? card3.LinkCount : 1);
							if (link_count >= 3)
							{
								break;
							}
						}
					}
					if (link_count >= 3)
					{
						base.AI.SelectCard(38342335);
						base.AI.SelectMaterials(resMaterials, 0);
						return true;
					}
				}
				return false;
			}
			List<ClientCard> i = new List<ClientCard>();
			IList<ClientCard> pre_m = this.CardsIdToClientCards(materials, (from card in base.Bot.GetMonsters()
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>(), true, true);
			if (pre_m != null && pre_m.Count <= 0)
			{
				return false;
			}
			int link_count2 = 0;
			foreach (ClientCard card2 in pre_m)
			{
				i.Add(card2);
				link_count2 += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
				if (link_count2 >= 4)
				{
					break;
				}
			}
			if (link_count2 < 4)
			{
				return false;
			}
			base.AI.SelectCard(21887175);
			base.AI.SelectMaterials(i, 0);
			return true;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000CF5E8 File Offset: 0x000CD7E8
		private bool AccesscodeTalkerEffect()
		{
			if (base.ActivateDescription != base.Util.GetStringId(86066372, 1))
			{
				List<ClientCard> cards = base.Bot.GetGraveyardMonsters();
				cards.Sort(new Comparison<ClientCard>(this.CompareCardLink));
				cards.Reverse();
				base.AI.SelectCard(cards);
				return true;
			}
			if (base.Card.IsDisabled())
			{
				return false;
			}
			if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasType(CardType.Link)) <= 0)
			{
				return false;
			}
			IList<CardAttribute> attributes = this.GetAttUsed();
			if (attributes == null || attributes.Count <= 0)
			{
				this.ResetFlag();
				this.selectFlag[5] = true;
				return true;
			}
			if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasType(CardType.Link) && !attributes.Contains((CardAttribute)card.Attribute)) <= 0)
			{
				return false;
			}
			this.ResetFlag();
			this.selectFlag[5] = true;
			return true;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000CF6F8 File Offset: 0x000CD8F8
		private bool CalledbytheGraveEffect()
		{
			ClientCard card = base.Util.GetLastChainCard();
			if (card == null)
			{
				return false;
			}
			int id = card.Id;
			List<ClientCard> g_cards = (from g_card in base.Enemy.GetGraveyardMonsters()
				where g_card != null && g_card.Id == id
				select g_card).ToList<ClientCard>();
			if (base.Duel.LastChainPlayer != 0 && card != null)
			{
				if (base.Card.Location == CardLocation.Hand)
				{
					base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				}
				if (card.Location == CardLocation.Grave && card.HasType(CardType.Monster))
				{
					base.AI.SelectCard(card);
				}
				else
				{
					if (g_cards.Count<ClientCard>() <= 0 || !card.HasType(CardType.Monster))
					{
						return false;
					}
					base.AI.SelectCard(g_cards);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000CF7C8 File Offset: 0x000CD9C8
		private bool MekkKnightCrusadiaAvramaxSummon()
		{
			List<int> materials_1 = new List<int> { 70369116, 50277355, 65741786 };
			List<int> materials_2 = new List<int> { 38342335 };
			List<int> materials_3 = new List<int> { 73539069, 41999284, 21044178 };
			if (base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && card.Id == 4280258 && card.Attack <= 800) > 0)
			{
				materials_3.Add(4280258);
			}
			if (!this.summon_UnionCarrier)
			{
				materials_1.Add(83152482);
			}
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials_1) && card.IsFaceup()) >= 2)
			{
				base.AI.SelectMaterials(materials_1, 0);
				this.place_Link_4 = true;
				return true;
			}
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials_2) && card.IsFaceup()) > 0 && base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials_3) && card.IsFaceup()) > 0)
			{
				materials_2.AddRange(materials_3);
				base.AI.SelectMaterials(materials_2, 0);
				this.place_Link_4 = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000CF948 File Offset: 0x000CDB48
		private bool GEffect()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player != 0;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000CF968 File Offset: 0x000CDB68
		private bool ThunderDragonColossusSummon_2()
		{
			return this.handActivated && this.activate_ThunderDragonmatrix && this.ThunderDragonColossusSummon();
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000CF984 File Offset: 0x000CDB84
		private bool ThunderDragonTitanEffect()
		{
			if (base.ActivateDescription != base.Util.GetStringId(41685633, 0))
			{
				this.selectFlag[6] = true;
				return true;
			}
			List<ClientCard> res = new List<ClientCard>();
			List<ClientCard> mcards = base.Enemy.GetMonsters();
			List<ClientCard> scards = base.Enemy.GetSpells();
			if (mcards.Count <= 0 && scards.Count <= 0)
			{
				return false;
			}
			if (base.Duel.CurrentChain.Count((ClientCard card) => card != null && card.Controller == 1) > 0)
			{
				foreach (ClientCard card2 in base.Duel.CurrentChain)
				{
					if (card2 != null && card2.Controller == 1 && (card2.Location == CardLocation.MonsterZone || card2.Location == CardLocation.SpellZone) && !card2.IsDisabled() && (card2.HasType(CardType.Monster) || card2.HasType(CardType.Field) || card2.HasType(CardType.Continuous) || card2.HasType(CardType.Equip)))
					{
						res.Add(card2);
					}
				}
			}
			mcards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			mcards.Reverse();
			res.AddRange(mcards);
			res.AddRange(scards);
			base.AI.SelectCard(res);
			return true;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000CFAFC File Offset: 0x000CDCFC
		private bool PredaplantVerteAnacondaEffect()
		{
			if (base.ActivateDescription != base.Util.GetStringId(70369116, 1))
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (this.CheckRemainInDeck(95238394) <= 0)
			{
				return false;
			}
			if (base.Bot.GetMonstersInMainZone().Count > 4)
			{
				if (base.Bot.GetMonstersInMainZone().Count((ClientCard card) => card != null && !card.IsExtraCard() && card.HasSetcode(284) && card.HasType(CardType.Monster) && card.IsFaceup()) <= 0)
				{
					return false;
				}
			}
			List<ClientCard> g_card = base.Bot.Graveyard.ToList<ClientCard>();
			List<ClientCard> b_card = base.Bot.Banished.ToList<ClientCard>();
			g_card.AddRange(b_card);
			int count = 0;
			int Lcount = 0;
			foreach (ClientCard card2 in g_card)
			{
				if (card2 != null)
				{
					if (card2.HasType(CardType.Monster) && card2.HasSetcode(284))
					{
						count++;
					}
					if (card2.IsCode(31786629))
					{
						Lcount++;
					}
				}
			}
			if (base.Bot.HasInExtra(15291624) && Lcount > 0)
			{
				if (g_card.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)) > 1)
				{
					base.AI.SelectCard(95238394);
					base.AI.SelectNextCard(new int[] { 15291624, 41685633 });
					this.No_SpSummon = true;
					return true;
				}
			}
			if (count >= 3 && base.Bot.HasInExtra(41685633))
			{
				base.AI.SelectCard(95238394);
				base.AI.SelectNextCard(new int[] { 41685633, 15291624 });
				this.No_SpSummon = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000CFCF4 File Offset: 0x000CDEF4
		private bool CrossSheepEffect()
		{
			if (base.Bot.HasInExtra(83152482) && (base.Bot.HasInGraveyard(92998610) || base.Bot.HasInGraveyard(23434538)))
			{
				base.AI.SelectCard(new int[] { 92998610, 23434538 });
			}
			else if (this.HasInZoneNoActivate(20318029, CardLocation.Grave, false))
			{
				base.AI.SelectCard(20318029);
			}
			else if (base.Bot.HasInExtra(41999284))
			{
				base.AI.SelectCard(new int[] { 20318029, 76218313 });
			}
			else
			{
				base.AI.SelectCard(new int[] { 44586426, 14558127, 23434538 });
			}
			return true;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000CFDD0 File Offset: 0x000CDFD0
		private bool KnightmareUnicornEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			List<ClientCard> cards = new List<ClientCard>();
			cards.AddRange(base.Enemy.SpellZone);
			cards.AddRange(base.Enemy.MonsterZone);
			cards = cards.Where((ClientCard card) => card != null && !card.IsShouldNotBeTarget()).ToList<ClientCard>();
			if (cards.Count <= 0)
			{
				return false;
			}
			List<int> disCardId = new List<int>();
			IList<int> repeatId = this.GetZoneRepeatCardsId(0, base.Bot.Hand, false);
			if (!repeatId.Contains(-1))
			{
				disCardId.AddRange(repeatId);
			}
			foreach (ClientCard card2 in base.Bot.Hand)
			{
				if (card2 != null && card2.HasSetcode(284) && card2.HasType(CardType.Monster))
				{
					disCardId.Add(card2.Id);
				}
			}
			base.AI.SelectCard(disCardId);
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			base.AI.SelectNextCard(cards);
			return true;
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000CFF10 File Offset: 0x000CE110
		private bool ThunderDragonlordEffect()
		{
			if (base.Duel.Phase == DuelPhase.End)
			{
				int count = base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder));
				if ((base.Bot.HasInGraveyard(29596581) || (base.Bot.HasInGraveyard(56713174) && count > 1)) && this.CheckRemainInDeck(95238394) > 0)
				{
					base.AI.SelectCard(95238394);
				}
				else if (!base.Bot.HasInGraveyard(29596581) && this.CheckRemainInDeck(29596581) > 0)
				{
					base.AI.SelectCard(29596581);
				}
				else if (!base.Bot.HasInGraveyard(56713174) && this.CheckRemainInDeck(56713174) > 0)
				{
					base.AI.SelectCard(56713174);
				}
				else
				{
					base.AI.SelectCard(new int[] { 20318029, 31786629, 44586426 });
				}
				return true;
			}
			if (base.Duel.Phase == DuelPhase.Standby)
			{
				List<ClientCard> Thundercards = base.Bot.Graveyard.Where((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)).ToList<ClientCard>();
				List<ClientCard> NoThundercards = base.Bot.Graveyard.Where((ClientCard card) => card != null && !card.HasRace(CardRace.Thunder) && !card.IsCode(95238394) && !card.IsCode(99266988)).ToList<ClientCard>();
				if (this.HasInZoneNoActivate(29596581, CardLocation.Grave, false) && base.Bot.GetMonstersInMainZone().Count < 5)
				{
					base.AI.SelectCard(29596581);
				}
				else if (this.HasInZoneNoActivate(56713174, CardLocation.Grave, false))
				{
					base.AI.SelectCard(56713174);
				}
				else if (this.HasInZoneNoActivate(83107873, CardLocation.Grave, false) && !this.GetZoneRepeatCardsId(0, base.Bot.Hand, false).Contains(-1))
				{
					base.AI.SelectCard(83107873);
				}
				else if (this.HasInZoneNoActivate(20318029, CardLocation.Grave, false))
				{
					base.AI.SelectCard(20318029);
				}
				else if (Thundercards.Count > 0)
				{
					base.AI.SelectCard(Thundercards);
				}
				else
				{
					base.AI.SelectCard(20318029);
				}
				List<ClientCard> Spellcards = (from card in base.Bot.GetGraveyardSpells()
					where card != null && !card.IsCode(95238394) && !card.IsCode(99266988)
					select card).ToList<ClientCard>();
				if (Spellcards.Count > 0)
				{
					base.AI.SelectNextCard(Spellcards);
				}
				else if (NoThundercards.Count > 0)
				{
					base.AI.SelectNextCard(Spellcards);
				}
				else if (Thundercards.Count > 0)
				{
					base.AI.SelectNextCard(Thundercards);
				}
				else
				{
					base.AI.SelectNextCard(99266988);
				}
				base.AI.SelectThirdCard(new int[] { 15291624, 41685633, 5206415, 90488465 });
				return true;
			}
			return false;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000D0238 File Offset: 0x000CE438
		private bool PredaplantVerteAnacondaSummon()
		{
			if (this.CheckRemainInDeck(95238394) <= 0)
			{
				return false;
			}
			List<ClientCard> g_card = base.Bot.Graveyard.ToList<ClientCard>();
			List<ClientCard> b_card = base.Bot.Banished.ToList<ClientCard>();
			g_card.AddRange(b_card);
			int count = 0;
			int Lcount = 0;
			foreach (ClientCard card2 in g_card)
			{
				if (card2 != null)
				{
					if (card2.HasType(CardType.Monster) && card2.HasSetcode(284))
					{
						count++;
					}
					if (card2.IsCode(31786629))
					{
						Lcount++;
					}
				}
			}
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			if (count < 3 || !base.Bot.HasInExtra(41685633))
			{
				if (base.Bot.HasInExtra(15291624) && Lcount > 0)
				{
					if (g_card.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)) > 1)
					{
						goto IL_0102;
					}
				}
				return false;
			}
			IL_0102:
			List<ClientCard> cards = (from card in base.Bot.GetMonsters()
				where card != null && card.IsFaceup() && this.GetLinkMark(card.Id) < 3 && !card.HasType(CardType.Normal) && !card.IsCode(15291624) && !card.IsCode(41685633) && !card.IsCode(5206415) && (!card.IsCode(83152482) || !this.summon_UnionCarrier)
				select card).ToList<ClientCard>();
			if (cards.Count < 2)
			{
				return false;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			if (cards.Count((ClientCard card) => card != null && card.Id == 65741786) > 0 && cards.Count <= 2)
			{
				return false;
			}
			base.AI.SelectMaterials(cards, 0);
			return true;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000D03E0 File Offset: 0x000CE5E0
		private bool AllureofDarknessEffect_2()
		{
			return !base.Bot.HasInHand(23434538) && base.Bot.Hand.Count <= 3 && base.Bot.Deck.Count > 2;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000D041C File Offset: 0x000CE61C
		private bool KnightmareUnicornSummon()
		{
			if (base.Bot.Hand.Count <= 0)
			{
				return false;
			}
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			monsters.AddRange(base.Enemy.GetSpells());
			if (monsters.Count((ClientCard card) => card3 != null && !card3.IsShouldNotBeTarget()) <= 0)
			{
				return false;
			}
			List<ClientCard> tmepMaterials = new List<ClientCard>();
			List<ClientCard> resMaterials = new List<ClientCard>();
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card3 = enumerator.Current;
					if (card3 != null && (card3.Id != 83152482 || !this.summon_UnionCarrier) && (this.GetLinkMark(card3.Id) < 3 || (card3.Id == 4280258 && card3.Attack <= 800)) && card3.Id != 41685633 && card3.Id != 15291624 && card3.IsFaceup() && tmepMaterials.Count((ClientCard _card) => _card != null && _card.Id == card3.Id) <= 0)
					{
						tmepMaterials.Add(card3);
					}
				}
			}
			int link_count = 0;
			tmepMaterials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			List<ClientCard> materials = new List<ClientCard>();
			List<ClientCard> link_materials = tmepMaterials.Where((ClientCard card) => card != null && card.LinkCount == 2).ToList<ClientCard>();
			List<ClientCard> normal_materials = tmepMaterials.Where((ClientCard card) => card != null && card.LinkCount != 2).ToList<ClientCard>();
			if (link_materials.Count<ClientCard>() >= 1)
			{
				link_materials.InsertRange(1, normal_materials);
				materials.AddRange(link_materials);
			}
			else
			{
				materials.AddRange(normal_materials);
				materials.AddRange(link_materials);
			}
			if (materials.Count((ClientCard card) => card != null && card.LinkCount >= 2) > 1)
			{
				if (materials.Count((ClientCard card) => card != null && card.LinkCount < 2) < 1)
				{
					return false;
				}
			}
			foreach (ClientCard card2 in materials)
			{
				if (!resMaterials.Contains(card2) && card2.LinkCount < 3)
				{
					resMaterials.Add(card2);
					link_count += (card2.HasType(CardType.Link) ? card2.LinkCount : 1);
					if (link_count >= 3)
					{
						break;
					}
				}
			}
			if (link_count >= 3)
			{
				base.AI.SelectMaterials(resMaterials, 0);
				return true;
			}
			return false;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000D0728 File Offset: 0x000CE928
		private bool UnderworldGoddessoftheClosedWorldSummon()
		{
			if (base.Duel.Turn == 0 || base.Enemy.GetMonsterCount() <= 0)
			{
				return false;
			}
			if (base.Util.GetBestAttack(base.Bot) >= base.Util.GetBestAttack(base.Enemy) && base.Enemy.MonsterZone.GetDangerousMonster(false) == null)
			{
				return false;
			}
			List<ClientCard> e_materials = new List<ClientCard>();
			List<ClientCard> m_materials = new List<ClientCard>();
			List<ClientCard> resMaterials = new List<ClientCard>();
			foreach (ClientCard card in base.Enemy.GetMonsters())
			{
				if (card != null && card.HasType(CardType.Effect) && card.IsFaceup())
				{
					e_materials.Add(card);
				}
			}
			if (e_materials.Count<ClientCard>() <= 0)
			{
				return false;
			}
			foreach (ClientCard card2 in base.Bot.GetMonsters())
			{
				if (card2 != null && (card2.Id != 83152482 || !this.summon_UnionCarrier) && this.GetLinkMark(card2.Id) < 3 && card2.Id != 41685633 && card2.Id != 15291624 && card2.IsFaceup() && card2.HasType(CardType.Effect))
				{
					m_materials.Add(card2);
				}
			}
			if (m_materials.Count<ClientCard>() < 3)
			{
				return false;
			}
			int link_count = 0;
			int e_link_count = 0;
			e_materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			e_materials.Reverse();
			foreach (ClientCard card3 in e_materials)
			{
				if (!resMaterials.Contains(card3))
				{
					resMaterials.Add(card3);
				}
				e_link_count += (card3.HasType(CardType.Link) ? ((card3.LinkCount == 2) ? 2 : 1) : 1);
				if (e_link_count >= 1)
				{
					break;
				}
			}
			if (e_link_count <= 0)
			{
				return false;
			}
			link_count += e_link_count;
			foreach (ClientCard card4 in m_materials)
			{
				if (e_link_count <= 1)
				{
					if (!resMaterials.Contains(card4) && card4.LinkCount < 3)
					{
						resMaterials.Add(card4);
						link_count += (card4.HasType(CardType.Link) ? card4.LinkCount : 1);
						if (link_count >= 5)
						{
							break;
						}
					}
				}
				else
				{
					resMaterials.Add(card4);
					link_count++;
					if (link_count >= 5)
					{
						break;
					}
				}
			}
			if (link_count >= 5)
			{
				base.AI.SelectMaterials(resMaterials, 0);
				this.place_Link_4 = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000D0A04 File Offset: 0x000CEC04
		private bool BowoftheGoddessSummon()
		{
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			if (base.Card.Id == 86066372)
			{
				if (base.Duel.Turn == 0 || base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() <= 0)
				{
					return false;
				}
			}
			else if (base.Duel.Turn > 0 && base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() > 0 && (base.Bot.HasInExtra(98127546) || base.Bot.HasInExtra(21887175) || base.Bot.HasInExtra(86066372)))
			{
				return false;
			}
			List<ClientCard> tempmaterials = new List<ClientCard>();
			List<ClientCard> resMaterials = new List<ClientCard>();
			foreach (ClientCard card2 in base.Bot.GetMonsters())
			{
				if (card2 != null && (card2.Id != 83152482 || !this.summon_UnionCarrier) && this.GetLinkMark(card2.Id) < 4 && card2.Id != 41685633 && card2.Id != 15291624 && card2.IsFaceup() && !card2.HasType(CardType.Token))
				{
					tempmaterials.Add(card2);
				}
			}
			int link_count = 0;
			List<ClientCard> materials = new List<ClientCard>();
			List<ClientCard> link_materials = tempmaterials.Where((ClientCard card) => card != null && (card.LinkCount == 3 || card.LinkCount == 2)).ToList<ClientCard>();
			List<ClientCard> normal_materials = tempmaterials.Where((ClientCard card) => card != null && card.LinkCount != 3 && card.LinkCount != 2).ToList<ClientCard>();
			normal_materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			if (link_materials.Count <= 0 && base.Card.Id == 86066372)
			{
				return false;
			}
			if (link_materials.Count((ClientCard card) => card != null && card.LinkCount == 3) > 0 && normal_materials.Count<ClientCard>() > 0)
			{
				int index = -1;
				for (int i = 0; i < link_materials.Count<ClientCard>(); i++)
				{
					if (link_materials[i] != null && link_materials[i].LinkCount == 3)
					{
						if (i > 0)
						{
							ClientCard temp = link_materials[0];
							link_materials[0] = link_materials[i];
							link_materials[i] = temp;
						}
						index = i;
						break;
					}
				}
				resMaterials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				if (index >= 0)
				{
					link_materials.InsertRange(index + 1, normal_materials);
				}
				materials.AddRange(link_materials);
			}
			else
			{
				link_materials.Sort(new Comparison<ClientCard>(this.CompareCardLink));
				materials.AddRange(link_materials);
				materials.AddRange(normal_materials);
			}
			using (List<ClientCard>.Enumerator enumerator = materials.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card = enumerator.Current;
					if (!resMaterials.Contains(card) && card.LinkCount < 4)
					{
						if (base.Card.Id == 4280258 && resMaterials.Count((ClientCard _card) => _card != null && _card.Id == card.Id) > 0)
						{
							break;
						}
						resMaterials.Add(card);
						link_count += (card.HasType(CardType.Link) ? card.LinkCount : 1);
						if (link_count >= 4)
						{
							break;
						}
					}
				}
			}
			resMaterials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			if (link_count >= 4)
			{
				base.AI.SelectMaterials(resMaterials, 0);
				this.place_Link_4 = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000D0DE8 File Offset: 0x000CEFE8
		private bool TheChaosCreatorSummon()
		{
			IList<int> cardsid = new List<int>();
			if (this.HasInZoneNoActivate(29596581, CardLocation.Grave, false) && base.Bot.GetMonstersInMainZone().Count < 4)
			{
				cardsid.Add(29596581);
			}
			if (this.HasInZoneNoActivate(56713174, CardLocation.Grave, false))
			{
				cardsid.Add(56713174);
			}
			if (this.HasInZoneNoActivate(20318029, CardLocation.Grave, false))
			{
				cardsid.Add(20318029);
			}
			if (base.Bot.HasInGraveyard(99266988) && !this.activate_ChaosSpace_grave)
			{
				cardsid.Add(61901281);
				cardsid.Add(99234526);
				cardsid.Add(32731036);
				cardsid.Add(5206415);
			}
			if (!base.Bot.HasInSpellZone(34090915, true, true) || base.Bot.GetCountCardInZone(base.Bot.GetGraveyardMonsters(), 33854624) + base.Bot.GetCountCardInZone(base.Bot.GetGraveyardMonsters(), 6637331) > 1)
			{
				cardsid.Add(33854624);
				cardsid.Add(6637331);
			}
			List<ClientCard> list = (from card in base.Bot.GetGraveyardMonsters()
				where card != null && (card.HasAttribute(CardAttribute.Dark) || card.HasAttribute(CardAttribute.Light))
				select card).ToList<ClientCard>();
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard card2 in list)
			{
				if (card2 != null)
				{
					cardsid.Add(card2.Id);
				}
			}
			base.AI.SelectCard(cardsid);
			base.AI.SelectCard(cardsid);
			return true;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000D0FB0 File Offset: 0x000CF1B0
		private bool ThunderDragonColossusSummon()
		{
			this.ResetFlag();
			this.selectFlag[4] = true;
			this.place_ThunderDragonColossus = true;
			return true;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000D0FD0 File Offset: 0x000CF1D0
		private bool ThunderDragonmatrixSet()
		{
			if (this.handActivated && base.Bot.HasInExtra(15291624))
			{
				if (base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && card.IsFaceup() && card.HasType(CardType.Effect)) <= 0)
				{
					this.isSummoned = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000D1034 File Offset: 0x000CF234
		private bool IPSummon()
		{
			if (base.Duel.Turn > 0 && base.Duel.Phase < DuelPhase.Main2)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() <= 2)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(15291624, false, false, false) && base.Bot.GetMonsterCount() <= 3)
			{
				return false;
			}
			if (!base.Bot.HasInExtra(38342335) && !base.Bot.HasInExtra(4280258) && !base.Bot.HasInExtra(21887175) && !base.Bot.HasInExtra(86066372) && !base.Bot.HasInExtra(98127546))
			{
				return false;
			}
			List<ClientCard> cards = base.Bot.GetMonsters().Where(delegate(ClientCard card)
			{
				if (card != null && this.GetLinkMark(card.Id) < 3 && card.Id != 41685633 && card.Id != 15291624 && !card.HasType(CardType.Link) && card.Attack <= 2500)
				{
					return card.EquipCards.Count((ClientCard ecard) => ecard != null && ecard.Id == 76218313 && !ecard.IsDisabled()) <= 0;
				}
				return false;
			}).ToList<ClientCard>();
			if (cards.Count < 2)
			{
				return false;
			}
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			List<int> cardsId = new List<int>();
			if (this.HasInZoneNoActivate(29596581, CardLocation.MonsterZone, false))
			{
				cardsId.Add(29596581);
			}
			if (this.HasInZoneNoActivate(56713174, CardLocation.MonsterZone, false))
			{
				cardsId.Add(56713174);
			}
			if (this.HasInZoneNoActivate(20318029, CardLocation.MonsterZone, false))
			{
				cardsId.Add(20318029);
			}
			foreach (ClientCard card2 in cards)
			{
				if (card2 != null)
				{
					cardsId.Add(card2.Id);
				}
			}
			base.AI.SelectMaterials(cardsId, 0);
			return true;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000D11E8 File Offset: 0x000CF3E8
		private bool ThunderDragonlordSummon()
		{
			if (base.Bot.GetMonstersInMainZone().Count > 4)
			{
				if (base.Bot.GetMonstersInMainZone().Count((ClientCard card) => card != null && card.Level <= 8 && card.HasType(CardType.Tuner) && !card.IsExtraCard() && card.IsFaceup()) <= 0)
				{
					return false;
				}
			}
			IList<int> cardsId = new List<int>();
			if (this.HasInZoneNoActivate(29596581, CardLocation.Hand, false) || this.HasInZoneNoActivate(29596581, CardLocation.MonsterZone, true))
			{
				cardsId.Add(29596581);
			}
			if (this.HasInZoneNoActivate(56713174, CardLocation.Hand, false) || this.HasInZoneNoActivate(56713174, CardLocation.MonsterZone, true))
			{
				cardsId.Add(56713174);
			}
			if (this.HasInZoneNoActivate(20318029, CardLocation.Hand, false) || this.HasInZoneNoActivate(20318029, CardLocation.MonsterZone, true))
			{
				cardsId.Add(20318029);
			}
			List<ClientCard> list = base.Bot.Hand.Where((ClientCard card) => card != null).ToList<ClientCard>();
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
			List<ClientCard> monsterCards = base.Bot.GetMonsters().ToList<ClientCard>();
			monsterCards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			foreach (ClientCard card3 in list)
			{
				if (card3 != null && card3.HasRace(CardRace.Thunder) && card3.Level <= 8)
				{
					cardsId.Add(card3.Id);
				}
			}
			foreach (ClientCard card2 in monsterCards)
			{
				if (card2 != null && card2.HasRace(CardRace.Thunder) && card2.Level <= 8 && card2.Id != 15291624 && card2.IsFaceup())
				{
					cardsId.Add(card2.Id);
				}
			}
			if (cardsId.Count <= 0)
			{
				return false;
			}
			base.AI.SelectCard(cardsId);
			return true;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000D1414 File Offset: 0x000CF614
		private bool UnionCarrierEffect()
		{
			return base.Bot.HasInMonstersZone(15291624, false, false, false) && this.UnionCarrierEffect_2();
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000D1434 File Offset: 0x000CF634
		private bool UnionCarrierEffect_2()
		{
			IList<int> cardsId = new List<int>();
			cardsId.Add(15291624);
			cardsId.Add(90488465);
			List<ClientCard> cards_ = (from card in base.Bot.GetMonsters()
				where card != null && card.IsFaceup() && (card.HasAttribute(CardAttribute.Dark) || card.HasRace(CardRace.Dragon))
				select card).ToList<ClientCard>();
			if (cards_.Count <= 0)
			{
				List<ClientCard> monsters = base.Bot.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters.Reverse();
				using (List<ClientCard>.Enumerator enumerator = monsters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientCard card3 = enumerator.Current;
						if (card3 != null && !cardsId.Contains(card3.Id))
						{
							cardsId.Add(card3.Id);
						}
					}
					goto IL_0126;
				}
			}
			cards_.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards_.Reverse();
			foreach (ClientCard card2 in cards_)
			{
				if (card2 != null && !cardsId.Contains(card2.Id))
				{
					cardsId.Add(card2.Id);
				}
			}
			IL_0126:
			base.AI.SelectCard(cardsId);
			base.AI.SelectNextCard(new int[] { 76218313, 29596581, 56713174, 20318029, 31786629 });
			return true;
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000D15AC File Offset: 0x000CF7AC
		private bool StrikerDragonSummon()
		{
			if ((this.summon_WhiteDragonWyverburster && this.summon_BlackDragonCollapserpent) || this.CheckRemainInDeck(99234526) <= 0 || this.CheckRemainInDeck(61901281) <= 0)
			{
				return false;
			}
			return base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.HasRace(CardRace.Dragon) && card.Level > 1) > 0;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000D161C File Offset: 0x000CF81C
		private bool DefaultSummon()
		{
			if (this.No_SpSummon)
			{
				return false;
			}
			if (base.Card.Id == 14558127 || base.Card.Id == 23434538)
			{
				return base.Bot.GetMonsterCount() < 2 && !this.handActivated && ((this.HasInZoneNoActivate(56713174, CardLocation.MonsterZone, true) || this.HasInZoneNoActivate(29596581, CardLocation.MonsterZone, true)) && (base.Bot.HasInExtra(70369116) || base.Bot.HasInExtra(65741786) || base.Bot.HasInExtra(50277355)));
			}
			if (base.Card.Level == 1)
			{
				if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.LinkCount <= 2) <= 0)
				{
					return false;
				}
			}
			else if (base.Bot.ExtraDeck.Count((ClientCard card) => card != null && card.LinkCount == 2) <= 0)
			{
				return false;
			}
			if ((base.Card.Id == 29596581 || base.Card.Id == 56713174 || base.Card.Id == 31786629) && (!base.Bot.HasInExtra(15291624) || !this.handActivated))
			{
				return false;
			}
			if (base.Card.Level > 4)
			{
				List<ClientCard> cards = (from card in base.Bot.GetMonsters()
					where card != null && this.GetLinkMark(card.Id) < 3 && card.Id != 41685633 && card.Id != 15291624 && card.Id != 65741786 && card.Id != 83152482
					select card).ToList<ClientCard>();
				if (cards.Count <= 0)
				{
					return false;
				}
				cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				if (this.handActivated && cards[0].Attack >= base.Card.Attack && !base.Bot.HasInExtra(15291624))
				{
					return false;
				}
				base.AI.SelectCard(cards);
			}
			this.isSummoned = true;
			return true;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000D1828 File Offset: 0x000CFA28
		private bool CheckThunderRemove()
		{
			return (this.HasInZoneNoActivate(56713174, CardLocation.Hand, false) && this.GetRemainingThunderCount(false) > 0) || (this.HasInZoneNoActivate(29596581, CardLocation.Hand, false) && this.GetRemainingThunderCount(false) > 0) || base.Bot.Hand.Any((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && !card.IsOriginalCode(92998610) && (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false) || this.HasInZoneNoActivate(29596581, CardLocation.Deck, false)));
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000D188C File Offset: 0x000CFA8C
		private bool ThunderDragonhawkEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.DefaultCheckWhetherCardIsNegated(base.Card))
				{
					return false;
				}
				List<ClientCard> banish_cards = new List<ClientCard>();
				List<ClientCard> grave_cards = new List<ClientCard>();
				foreach (ClientCard card4 in base.Bot.Banished)
				{
					if (card4 != null && card4.HasType(CardType.Monster) && card4.HasSetcode(284))
					{
						banish_cards.Add(card4);
					}
				}
				foreach (ClientCard card2 in base.Bot.Graveyard)
				{
					if (card2 != null && card2.HasType(CardType.Monster) && card2.HasSetcode(284))
					{
						grave_cards.Add(card2);
					}
				}
				banish_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				banish_cards.Reverse();
				grave_cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				grave_cards.Reverse();
				banish_cards.AddRange(grave_cards);
				List<ClientCard> res = new List<ClientCard>();
				foreach (ClientCard card3 in banish_cards)
				{
					if (!this.activate_ThunderDragonroar && card3 != null && card3.Id == 29596581)
					{
						res.Add(card3);
					}
					else if (!this.activate_ThunderDragondark && card3 != null && card3.Id == 56713174)
					{
						res.Add(card3);
					}
				}
				res.AddRange(banish_cards);
				base.AI.SelectCard(res);
				this.handActivated = true;
				this.activate_ThunderDragonhawk = true;
				return true;
			}
			else
			{
				this.activate_ThunderDragonhawk = true;
				List<int> cardsid = new List<int> { 76218313 };
				cardsid.AddRange(this.GetZoneRepeatCardsId(0, base.Bot.Hand, false));
				List<ClientCard> resCards = new List<ClientCard>();
				using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientCard card = enumerator.Current;
						if (card != null && cardsid.Contains(card.Id) && resCards.Count((ClientCard _card) => _card != null && _card.Id == card.Id) <= 0)
						{
							resCards.Add(card);
						}
					}
				}
				if (resCards.Count<ClientCard>() <= 0)
				{
					return false;
				}
				base.AI.SelectCard(resCards);
				return true;
			}
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000D1B44 File Offset: 0x000CFD44
		private bool BlackDragonCollapserpentSummon_2()
		{
			if (base.Bot.HasInGraveyard(99234526) && base.Bot.HasInGraveyard(99266988) && !this.activate_ChaosSpace_grave)
			{
				base.AI.SelectCard(99234526);
			}
			else if (base.Bot.HasInGraveyard(5206415) && base.Bot.HasInGraveyard(99266988) && !this.activate_ChaosSpace_grave)
			{
				base.AI.SelectCard(5206415);
			}
			else if (this.HasInZoneNoActivate(20318029, CardLocation.Grave, false))
			{
				base.AI.SelectCard(20318029);
			}
			else if (this.HasInZoneNoActivate(83107873, CardLocation.Grave, false) && !this.GetZoneRepeatCardsId(0, base.Bot.Hand, false).Contains(-1))
			{
				base.AI.SelectCard(83107873);
			}
			else
			{
				base.AI.SelectCard(new int[] { 99234526, 44586426 });
			}
			this.summon_BlackDragonCollapserpent = true;
			return true;
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000D1C5C File Offset: 0x000CFE5C
		private bool BlackDragonCollapserpentSummon()
		{
			return (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Light)) > 1 || !base.Bot.HasInGraveyard(32731036) || this.CheckRemainInDeck(34090915) <= 0 || this.summon_TheBystialLubellion) && this.BlackDragonCollapserpentSummon_2();
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000D1CCC File Offset: 0x000CFECC
		private bool GoldSarcophagusEffect()
		{
			if (this.GetRemainingThunderCount(false) <= 0)
			{
				return false;
			}
			if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false) && base.Bot.GetMonstersInMainZone().Count < 5)
			{
				base.AI.SelectCard(29596581);
			}
			else if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false))
			{
				base.AI.SelectCard(56713174);
			}
			else if (this.HasInZoneNoActivate(20318029, CardLocation.Deck, false))
			{
				base.AI.SelectCard(20318029);
			}
			else
			{
				base.AI.SelectCard(20318029);
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			return true;
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000D1D92 File Offset: 0x000CFF92
		private bool UnionCarrierSummon()
		{
			return this.CheckRemainInDeck(76218313) > 0 && base.Bot.HasInMonstersZone(15291624, false, false, true) && this.UnionCarrierSummon_2();
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000D1DC0 File Offset: 0x000CFFC0
		private bool LinkCheck(bool exZone_1)
		{
			int exSq;
			int linkSq_;
			int linkSq_2;
			if (exZone_1)
			{
				exSq = 5;
				linkSq_ = 0;
				linkSq_2 = 2;
			}
			else
			{
				exSq = 6;
				linkSq_ = 2;
				linkSq_2 = 4;
			}
			if (base.Bot.MonsterZone[exSq] != null && base.Bot.HasInMonstersZone(15291624, false, false, true))
			{
				CardRace linkRace = (CardRace)base.Bot.MonsterZone[exSq].Race;
				CardAttribute linkAtt = (CardAttribute)base.Bot.MonsterZone[exSq].Attribute;
				int linkRaceCount = base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsCode(15291624) && card.HasRace(linkRace));
				int linkAttCount = base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsCode(15291624) && card.HasAttribute(linkAtt));
				if (base.Bot.MonsterZone[exSq].Id == 50277355 || base.Bot.MonsterZone[exSq].Id == 70369116 || base.Bot.MonsterZone[exSq].Id == 65741786 || base.Bot.MonsterZone[exSq].Id == 21887175)
				{
					if (base.Bot.MonsterZone[linkSq_] != null && base.Bot.MonsterZone[linkSq_].Id == 15291624)
					{
						if (base.Bot.MonsterZone[linkSq_2] != null)
						{
							CardRace race2 = (CardRace)base.Bot.MonsterZone[linkSq_2].Race;
							CardAttribute att2 = (CardAttribute)base.Bot.MonsterZone[linkSq_2].Attribute;
							int num = base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsCode(15291624) && card.HasRace(race2));
							int attCount = base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsCode(15291624) && card.HasAttribute(att2));
							if (num < 2 && attCount < 2 && linkRaceCount < 2 && linkAttCount < 2)
							{
								return false;
							}
						}
					}
					else if (base.Bot.MonsterZone[linkSq_2] != null && base.Bot.MonsterZone[linkSq_2].Id == 15291624 && base.Bot.MonsterZone[linkSq_] != null)
					{
						CardRace race = (CardRace)base.Bot.MonsterZone[linkSq_].Race;
						CardAttribute att = (CardAttribute)base.Bot.MonsterZone[linkSq_].Attribute;
						int num2 = base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsCode(15291624) && card.HasRace(race));
						int attCount2 = base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsCode(15291624) && card.HasAttribute(att));
						if (num2 < 2 && attCount2 < 2 && linkRaceCount < 2 && linkAttCount < 2)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000D2088 File Offset: 0x000D0288
		private bool UnionCarrierSummon_2()
		{
			if (base.Bot.GetMonsterCount() <= 2 && (base.Bot.HasInMonstersZone(15291624, false, false, false) || base.Bot.HasInMonstersZone(41685633, false, false, false)))
			{
				return false;
			}
			IEnumerable<ClientCard> enumerable = (from card in base.Bot.GetMonsters()
				where card != null && card.HasAttribute(CardAttribute.Dark) && card.IsFaceup() && !card.IsOriginalCode(15291624) && this.GetLinkMark(card.Id) < 3
				select card).ToList<ClientCard>();
			List<ClientCard> attLightCards = (from card in base.Bot.GetMonsters()
				where card != null && card.HasAttribute(CardAttribute.Light) && card.IsFaceup() && this.GetLinkMark(card.Id) < 3
				select card).ToList<ClientCard>();
			List<ClientCard> attEarthCards = (from card in base.Bot.GetMonsters()
				where card != null && card.HasAttribute(CardAttribute.Earth) && card.IsFaceup() && this.GetLinkMark(card.Id) < 3
				select card).ToList<ClientCard>();
			List<ClientCard> raceThunderCards = (from card in base.Bot.GetMonsters()
				where card != null && card.HasRace(CardRace.Thunder) && card.IsFaceup() && !card.IsOriginalCode(15291624) && this.GetLinkMark(card.Id) < 3
				select card).ToList<ClientCard>();
			List<ClientCard> raceDragonCards = (from card in base.Bot.GetMonsters()
				where card != null && card.HasRace(CardRace.Dragon) && card.IsFaceup() && this.GetLinkMark(card.Id) < 3
				select card).ToList<ClientCard>();
			List<ClientCard> raceBeastCards = (from card in base.Bot.GetMonsters()
				where card != null && card.HasRace(CardRace.Beast) && card.IsFaceup() && this.GetLinkMark(card.Id) < 3
				select card).ToList<ClientCard>();
			if (enumerable.Count<ClientCard>() < 2 && attLightCards.Count<ClientCard>() < 2 && attEarthCards.Count<ClientCard>() < 2 && raceThunderCards.Count<ClientCard>() < 2 && raceDragonCards.Count<ClientCard>() < 2 && raceBeastCards.Count<ClientCard>() < 2)
			{
				return false;
			}
			if (!this.LinkCheck(false) || !this.LinkCheck(true))
			{
				return false;
			}
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].Controller == 0 && this.GetLinkMark(base.Bot.MonsterZone[6].Id) > 1)
			{
				return false;
			}
			int[] materials = new int[]
			{
				73539069, 44586427, 44586426, 20318029, 31786629, 99234526, 83107873, 23434538, 92998610, 50277355,
				29596581, 56713174, 61901281, 76218313, 33854624, 6637331, 90488465, 41999284, 32731036, 5206415,
				70369116, 65741786
			};
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(materials)) >= 2)
			{
				base.AI.SelectMaterials(materials, 0);
				this.summon_UnionCarrier = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000D2294 File Offset: 0x000D0494
		private bool BatterymanSolarSummon()
		{
			if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false) || this.HasInZoneNoActivate(56713174, CardLocation.Deck, false) || this.HasInZoneNoActivate(20318029, CardLocation.Deck, false) || this.HasInZoneNoActivate(61901281, CardLocation.Hand, false) || this.HasInZoneNoActivate(99234526, CardLocation.Hand, false) || this.HasInZoneNoActivate(6637331, CardLocation.Hand, false) || this.HasInZoneNoActivate(33854624, CardLocation.Hand, false) || base.Bot.HasInHand(90488465))
			{
				this.isSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000D2328 File Offset: 0x000D0528
		private bool ThunderDragonroarEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false))
				{
					base.AI.SelectCard(56713174);
				}
				else if (this.HasInZoneNoActivate(20318029, CardLocation.Deck, false))
				{
					base.AI.SelectCard(20318029);
				}
				else if (this.HasInZoneNoActivate(31786629, CardLocation.Deck, false))
				{
					base.AI.SelectCard(31786629);
				}
				else
				{
					base.AI.SelectCard(new int[] { 31786629, 56713174, 31786629, 20318029 });
				}
				this.activate_ThunderDragonroar = true;
				return true;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (this.handActivated)
			{
				return false;
			}
			this.handActivated = true;
			this.activate_ThunderDragonroar = true;
			if (this.HasInZoneNoActivate(83107873, CardLocation.Grave, false) || this.HasInZoneNoActivate(83107873, CardLocation.Removed, false))
			{
				base.AI.SelectCard(83107873);
			}
			else
			{
				base.AI.SelectCard(new int[] { 20318029, 56713174, 83107873 });
			}
			return true;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000D2444 File Offset: 0x000D0644
		private bool S_SpSummon()
		{
			if (base.Duel.Player == 0)
			{
				if (base.Duel.CurrentChain.Count > 0)
				{
					return false;
				}
				List<ClientCard> cards = new List<ClientCard>();
				if (this.HasInZoneNoActivate(29596581, CardLocation.Grave, false) && base.Bot.GetMonstersInMainZone().Count < 4)
				{
					using (IEnumerator<ClientCard> enumerator = base.Bot.Graveyard.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ClientCard card_ = enumerator.Current;
							if (card_ != null && card_.Id == 29596581)
							{
								cards.Add(card_);
							}
						}
						goto IL_02D3;
					}
				}
				if (this.HasInZoneNoActivate(56713174, CardLocation.Grave, false))
				{
					using (IEnumerator<ClientCard> enumerator = base.Bot.Graveyard.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ClientCard card_2 = enumerator.Current;
							if (card_2 != null && card_2.Id == 56713174)
							{
								cards.Add(card_2);
							}
						}
						goto IL_02D3;
					}
				}
				if (this.HasInZoneNoActivate(20318029, CardLocation.Grave, false))
				{
					using (IEnumerator<ClientCard> enumerator = base.Bot.Graveyard.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ClientCard card_3 = enumerator.Current;
							if (card_3 != null && card_3.Id == 20318029)
							{
								cards.Add(card_3);
							}
						}
						goto IL_02D3;
					}
				}
				if (base.Bot.HasInGraveyard(99266988) && !this.activate_ChaosSpace_grave)
				{
					using (IEnumerator<ClientCard> enumerator = base.Bot.Graveyard.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ClientCard card_4 = enumerator.Current;
							if (card_4 != null && this.NotSpSummonCardsId.Contains(card_4.Id))
							{
								cards.Add(card_4);
							}
						}
						goto IL_02D3;
					}
				}
				foreach (ClientCard card_5 in base.Enemy.Graveyard)
				{
					if (card_5 != null && (card_5.HasAttribute(CardAttribute.Light) || card_5.HasAttribute(CardAttribute.Dark)))
					{
						cards.Add(card_5);
					}
				}
				foreach (ClientCard card_6 in base.Bot.Graveyard)
				{
					if (card_6 != null && (card_6.HasAttribute(CardAttribute.Light) || card_6.HasAttribute(CardAttribute.Dark)) && !card_6.IsCode(32731036) && !card_6.HasRace(CardRace.Thunder))
					{
						cards.Add(card_6);
					}
				}
				foreach (ClientCard card_7 in base.Bot.Graveyard)
				{
					if (card_7 != null && (card_7.HasAttribute(CardAttribute.Light) || card_7.HasAttribute(CardAttribute.Dark)) && card_7.HasRace(CardRace.Thunder))
					{
						cards.Add(card_7);
					}
				}
				IL_02D3:
				base.AI.SelectCard(cards);
				return true;
			}
			else
			{
				if (base.Duel.Phase < DuelPhase.Battle && base.Duel.CurrentChain.Count <= 0)
				{
					return false;
				}
				ClientCard card = base.Util.GetLastChainCard();
				if (card != null && card.Controller != 0 && card.Location == CardLocation.Grave && (card.HasAttribute(CardAttribute.Dark) || card.HasAttribute(CardAttribute.Light)))
				{
					base.AI.SelectCard(card);
				}
				else
				{
					if (base.Duel.CurrentChain.Count > 0)
					{
						if (base.Duel.CurrentChain.Count((ClientCard _card) => _card != null && (_card.Id == 6637331 || _card.Id == 33854624)) <= 0 && base.Duel.LastChainPlayer == 1)
						{
							if (base.Enemy.Graveyard.Count((ClientCard _card) => _card != null && (_card.HasAttribute(CardAttribute.Dark) || _card.HasAttribute(CardAttribute.Light))) > 0)
							{
								List<ClientCard> graveCards = base.Enemy.GetGraveyardMonsters();
								graveCards.Reverse();
								base.AI.SelectCard(graveCards);
								return true;
							}
						}
					}
					if (base.Duel.CurrentChain.Count > 0)
					{
						if (base.Duel.CurrentChain.Count((ClientCard _card) => _card != null && _card.Controller == 0 && (_card.Id == 6637331 || _card.Id == 33854624)) > 0)
						{
							return false;
						}
					}
					List<ClientCard> res = new List<ClientCard>();
					List<ClientCard> pre_res = new List<ClientCard>();
					foreach (ClientCard mcard in base.Enemy.Graveyard)
					{
						if (mcard != null && (mcard.HasAttribute(CardAttribute.Dark) || mcard.HasAttribute(CardAttribute.Light)))
						{
							res.Add(mcard);
						}
					}
					foreach (ClientCard mcard2 in base.Bot.Graveyard)
					{
						if (mcard2 != null && (mcard2.HasAttribute(CardAttribute.Dark) || mcard2.HasAttribute(CardAttribute.Light)))
						{
							if (mcard2.Id == 29596581 && !this.activate_ThunderDragonroar && base.Bot.GetMonstersInMainZone().Count < 5)
							{
								res.Add(mcard2);
							}
							else if (mcard2.Id == 56713174 && !this.activate_ThunderDragondark)
							{
								res.Add(mcard2);
							}
							else
							{
								pre_res.Add(mcard2);
							}
						}
					}
					if (res.Count<ClientCard>() <= 0)
					{
						return false;
					}
					if (pre_res.Count > 0)
					{
						res.AddRange(pre_res);
					}
					base.AI.SelectCard(res);
				}
				return true;
			}
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000D2A48 File Offset: 0x000D0C48
		private bool BystialDruiswurmEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return this.S_SpSummon();
			}
			List<ClientCard> cards = base.Enemy.GetMonsters();
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			cards.Reverse();
			base.AI.SelectCard(cards);
			return true;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000D2A9B File Offset: 0x000D0C9B
		private bool BystialMagnamhutEffect()
		{
			return base.Card.Location != CardLocation.Hand || this.S_SpSummon();
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000D2AB4 File Offset: 0x000D0CB4
		private bool CrossSheepSummon()
		{
			if (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].Controller == 0 && this.GetLinkMark(base.Bot.MonsterZone[5].Id) > 1)
			{
				return false;
			}
			if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].Controller == 0 && this.GetLinkMark(base.Bot.MonsterZone[6].Id) > 1)
			{
				return false;
			}
			if (!this.handActivated)
			{
				if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasSetcode(284) && card.HasType(CardType.Monster)) + base.Bot.MonsterZone.Count((ClientCard card) => card != null && card.HasSetcode(284) && card.IsFaceup() && card.HasType(CardType.Monster)) + base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasSetcode(284) && card.HasType(CardType.Monster)) + base.Bot.Banished.Count((ClientCard card) => card != null && card.HasSetcode(284) && card.IsFaceup() && card.HasType(CardType.Monster)) < 2)
				{
					return false;
				}
			}
			if (base.Bot.HasInMonstersZone(15291624, false, false, true) || base.Bot.HasInMonstersZone(41685633, false, false, true))
			{
				bool isShoudlSummon_ = false;
				int light_count = base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Light));
				int dark_count = base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Dark));
				if (this.HasInZoneNoActivate(99234526, CardLocation.Hand, false) && dark_count > 0)
				{
					isShoudlSummon_ = true;
				}
				else if (this.HasInZoneNoActivate(61901281, CardLocation.Hand, false) && light_count > 0)
				{
					isShoudlSummon_ = true;
				}
				else if ((this.HasInZoneNoActivate(33854624, CardLocation.Hand, false) || this.HasInZoneNoActivate(6637331, CardLocation.Hand, false)) && (dark_count > 0 || light_count > 0))
				{
					isShoudlSummon_ = true;
				}
				else if (this.HasInZoneNoActivate(83107873, CardLocation.Hand, false))
				{
					List<ClientCard> list = base.Bot.GetMonsters().ToList<ClientCard>();
					List<ClientCard> grave = base.Bot.Graveyard.ToList<ClientCard>();
					List<ClientCard> banish = base.Bot.Banished.ToList<ClientCard>();
					list.AddRange(grave);
					list.AddRange(banish);
					isShoudlSummon_ = list.Count((ClientCard card) => card != null && card.HasType(CardType.Monster) && card.HasSetcode(284) && !card.IsCode(15291624) && !card.IsCode(41685633)) > 0;
				}
				else if (base.Bot.HasInHand(90488465) && light_count > 0 && dark_count > 0)
				{
					isShoudlSummon_ = true;
				}
				else if (base.Bot.HasInHand(5206415))
				{
					if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasType(CardType.Monster) && card.HasSetcode(284)) > 1)
					{
						isShoudlSummon_ = true;
					}
				}
				if (!isShoudlSummon_)
				{
					return false;
				}
			}
			if (!this.IsAvailableLinkZone())
			{
				return false;
			}
			IList<int> cardsid = this.GetZoneRepeatCardsId(0, base.Bot.MonsterZone, true);
			if (!cardsid.Contains(-1) && base.Bot.MonsterZone.Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsOriginalCode(15291624) && this.GetLinkMark(card.Id) <= 1) - cardsid.Count<int>() < 2)
			{
				return false;
			}
			if (cardsid.Contains(-1) && base.Bot.MonsterZone.Count((ClientCard card) => card != null && card.IsFaceup() && !card.IsOriginalCode(15291624) && !card.IsOriginalCode(41685633) && this.GetLinkMark(card.Id) <= 1) < 2)
			{
				return false;
			}
			bool isShoudlSummon_2 = false;
			foreach (ClientCard card3 in base.Bot.GetMonsters())
			{
				if (card3 != null && card3.IsFaceup() && this.SpSummonCardsId.Contains(card3.Id))
				{
					isShoudlSummon_2 = true;
					break;
				}
			}
			if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasType(CardType.Monster) && card.Level <= 4 && !card.IsCode(61901281) && !card.IsCode(99234526)) > 0)
			{
				isShoudlSummon_2 = true;
			}
			if (!isShoudlSummon_2)
			{
				return false;
			}
			List<ClientCard> cards = base.Bot.GetMonsters();
			if (cards.Count < 2)
			{
				return false;
			}
			cards.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			HashSet<int> MaterialsIdSet = new HashSet<int>();
			foreach (ClientCard card2 in cards)
			{
				if (card2 != null && (card2.Id != 83152482 || !this.summon_UnionCarrier) && this.GetLinkMark(card2.Id) <= 1 && card2.Id != 15291624 && card2.Id != 41685633)
				{
					if (card2.EquipCards != null)
					{
						if (card2.EquipCards == null)
						{
							continue;
						}
						if (card2.EquipCards.Count((ClientCard ecard) => ecard != null && ecard.Id == 76218313) > 0)
						{
							continue;
						}
					}
					MaterialsIdSet.Add(card2.Id);
				}
			}
			if (MaterialsIdSet.Count<int>() < 2)
			{
				return false;
			}
			List<int> material = new List<int>();
			if (this.HasInZoneNoActivate(29596581, CardLocation.MonsterZone, false))
			{
				material.Add(29596581);
			}
			if (this.HasInZoneNoActivate(56713174, CardLocation.MonsterZone, false))
			{
				material.Add(56713174);
			}
			if (this.HasInZoneNoActivate(20318029, CardLocation.MonsterZone, false))
			{
				material.Add(20318029);
			}
			IList<int> materials = MaterialsIdSet.ToList<int>();
			material.AddRange(materials);
			base.AI.SelectMaterials(material, 0);
			this.place_CrossSheep = true;
			return true;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000D30A8 File Offset: 0x000D12A8
		private bool ThunderDragonmatrixEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				this.activate_ThunderDragonmatrix = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000D30C4 File Offset: 0x000D12C4
		private bool IsShouldChainTunder()
		{
			ClientCard card = base.Util.GetLastChainCard();
			return card != null && card.Controller != 0 && base.Bot.HasInMonstersZone(41685633, true, false, true) && !card.IsDisabled() && (card.HasType(CardType.Monster) || card.HasType(CardType.Continuous) || card.HasType(CardType.Equip) || card.HasType(CardType.Field)) && (card.Location == CardLocation.MonsterZone || card.Location == CardLocation.SpellZone);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000D314C File Offset: 0x000D134C
		private bool ThunderDragonmatrixEffect_2()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Duel.Player == 0)
			{
				if (this.IsShouldChainTunder())
				{
					this.activate_ThunderDragondark = true;
					this.handActivated = true;
					return true;
				}
				if (base.Duel.CurrentChain.Count > 0)
				{
					return false;
				}
				List<ClientCard> cards = base.Bot.Graveyard.ToList<ClientCard>();
				cards.AddRange(base.Bot.Banished.ToList<ClientCard>());
				if (!this.handActivated)
				{
					if (this.HasInZoneNoActivate(83107873, CardLocation.Hand, false) && base.Bot.GetMonstersInMainZone().Count < 5)
					{
						if (cards.Count((ClientCard card) => card != null && card.HasSetcode(284) && card.HasType(CardType.Monster) && !card.IsCode(83107873) && !card.IsExtraCard() && !card.IsCode(5206415)) > 0)
						{
							return false;
						}
					}
					this.activate_ThunderDragonmatrix = true;
					this.handActivated = true;
					return true;
				}
				return false;
			}
			else
			{
				if (this.IsShouldChainTunder())
				{
					base.AI.SelectCard(new int[] { 15291624, 41685633 });
					this.activate_ThunderDragonmatrix = true;
					this.handActivated = true;
					return true;
				}
				if (base.Duel.Phase == DuelPhase.Battle)
				{
					if (base.Bot.HasInMonstersZone(41685633, true, false, true) && base.Enemy.GetMonsterCount() > 0)
					{
						base.AI.SelectCard(new int[] { 15291624, 41685633 });
						this.activate_ThunderDragonmatrix = true;
						this.handActivated = true;
						return true;
					}
				}
				else if (base.Duel.Phase == DuelPhase.BattleStep)
				{
					if (base.Bot.BattlingMonster != null && base.Bot.BattlingMonster.HasRace(CardRace.Thunder) && !base.Bot.BattlingMonster.IsShouldNotBeTarget())
					{
						base.AI.SelectCard(base.Bot.BattlingMonster);
						this.activate_ThunderDragonmatrix = true;
						this.handActivated = true;
						return true;
					}
				}
				else if (base.Duel.Phase == DuelPhase.End)
				{
					base.AI.SelectCard(new int[] { 15291624, 41685633 });
					this.activate_ThunderDragonmatrix = true;
					this.handActivated = true;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000D339C File Offset: 0x000D159C
		private bool WhiteDragonWyverbursterSummon()
		{
			if (this.HasInZoneNoActivate(29596581, CardLocation.Grave, false) && base.Bot.GetMonstersInMainZone().Count < 5)
			{
				base.AI.SelectCard(29596581);
			}
			else if (this.HasInZoneNoActivate(56713174, CardLocation.Grave, false))
			{
				base.AI.SelectCard(56713174);
			}
			else
			{
				List<int> cardsid = new List<int>();
				cardsid.Add(61901281);
				cardsid.Add(90488465);
				foreach (ClientCard card in base.Bot.Graveyard)
				{
					if (card != null && !card.HasSetcode(284) && card.HasAttribute(CardAttribute.Dark))
					{
						cardsid.Add(card.Id);
					}
				}
				foreach (ClientCard card2 in base.Bot.Graveyard)
				{
					if (card2 != null && card2.HasSetcode(284) && card2.HasAttribute(CardAttribute.Dark))
					{
						cardsid.Add(card2.Id);
					}
				}
				base.AI.SelectCard(cardsid);
			}
			this.summon_WhiteDragonWyverburster = true;
			return true;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000D34FC File Offset: 0x000D16FC
		private bool HasInZoneNoActivate(int cardId, CardLocation location, bool isFaceUp = false)
		{
			switch (location)
			{
			case CardLocation.Deck:
				if (this.CheckRemainInDeck(cardId) <= 0)
				{
					return false;
				}
				goto IL_0075;
			case CardLocation.Hand:
				if (!base.Bot.HasInHand(cardId))
				{
					return false;
				}
				goto IL_0075;
			case (CardLocation)3:
				break;
			case CardLocation.MonsterZone:
				if (!base.Bot.HasInMonstersZone(cardId, false, false, isFaceUp))
				{
					return false;
				}
				goto IL_0075;
			default:
				if (location != CardLocation.Grave)
				{
					if (location == CardLocation.Removed)
					{
						if (!base.Bot.HasInBanished(cardId))
						{
							return false;
						}
						goto IL_0075;
					}
				}
				else
				{
					if (!base.Bot.HasInGraveyard(cardId))
					{
						return false;
					}
					goto IL_0075;
				}
				break;
			}
			return false;
			IL_0075:
			if (cardId <= 32731036)
			{
				if (cardId <= 20318029)
				{
					if (cardId == 6637331)
					{
						return !this.activate_BystialDruiswurm_hand;
					}
					if (cardId == 20318029)
					{
						return !this.activate_ThunderDragonmatrix;
					}
				}
				else
				{
					if (cardId == 29596581)
					{
						return !this.activate_ThunderDragonroar;
					}
					if (cardId == 32731036)
					{
						return !this.activate_TheBystialLubellion_hand;
					}
				}
			}
			else if (cardId <= 56713174)
			{
				if (cardId == 33854624)
				{
					return !this.activate_BystialMagnamhut_hand;
				}
				if (cardId == 56713174)
				{
					return !this.activate_ThunderDragondark;
				}
			}
			else
			{
				if (cardId == 61901281)
				{
					return !this.summon_BlackDragonCollapserpent;
				}
				if (cardId == 83107873)
				{
					return !this.activate_ThunderDragonhawk;
				}
				if (cardId == 99234526)
				{
					return !this.summon_WhiteDragonWyverburster;
				}
			}
			return false;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000D3648 File Offset: 0x000D1848
		private bool AloofLupineEffect()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				int[] ids = new int[] { 83107873, 15291624, 33854624, 6637331, 61901281, 99234526, 90488465, 32731036 };
				base.AI.SelectCard(ids);
				return true;
			}
			if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)) <= 0)
			{
				return false;
			}
			bool _ThunderDragonroar = false;
			bool _ThunderDragondark = false;
			bool _ThunderDragonmatrix = false;
			if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && (!card.IsCode(83107873) || !this.activate_ThunderDragonhawk)) <= 0 && this.GetRemainingThunderCount(true) <= 0)
			{
				return false;
			}
			if (this.HasInZoneNoActivate(29596581, CardLocation.Hand, false) && base.Bot.GetMonstersInMainZone().Count<ClientCard>() < 5 && this.GetRemainingThunderCount(false) > 0)
			{
				base.AI.SelectCard(29596581);
				_ThunderDragonroar = true;
			}
			else if (this.HasInZoneNoActivate(56713174, CardLocation.Hand, false) && this.GetRemainingThunderCount(false) > 0)
			{
				base.AI.SelectCard(56713174);
				_ThunderDragondark = true;
			}
			else if (this.HasInZoneNoActivate(20318029, CardLocation.Hand, false) && this.GetRemainingThunderCount(false) > 0)
			{
				base.AI.SelectCard(20318029);
				_ThunderDragonmatrix = true;
			}
			else if (base.Bot.GetCountCardInZone(base.Bot.Hand, 31786629) > 1)
			{
				base.AI.SelectCard(31786629);
			}
			else
			{
				IList<ClientCard> cards = base.Bot.Hand.Where((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)).ToList<ClientCard>();
				if (cards.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(cards);
				}
				else
				{
					base.AI.SelectCard(new int[] { 5206415, 44586426, 90488465, 31786629 });
				}
			}
			if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false) && !_ThunderDragonroar && base.Bot.GetMonstersInMainZone().Count < 5 && !base.Bot.HasInMonstersZone(29596581, false, false, true))
			{
				base.AI.SelectNextCard(29596581);
			}
			else if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false) && !_ThunderDragondark && !base.Bot.HasInMonstersZone(56713174, false, false, true))
			{
				base.AI.SelectNextCard(56713174);
			}
			else if (this.HasInZoneNoActivate(20318029, CardLocation.Deck, false) && !_ThunderDragonmatrix && !base.Bot.HasInMonstersZone(20318029, false, false, true))
			{
				base.AI.SelectNextCard(20318029);
			}
			else if (base.Bot.HasInGraveyard(90488465) && !this.activate_ChaosSpace_grave && this.CheckRemainInDeck(5206415) > 0)
			{
				base.AI.SelectNextCard(5206415);
			}
			else
			{
				base.AI.SelectNextCard(31786629);
			}
			return true;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000D3938 File Offset: 0x000D1B38
		private bool ThunderDragonmatrixSummon()
		{
			if (this.No_SpSummon)
			{
				return false;
			}
			if (base.Bot.HasInExtra(41999284))
			{
				if (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].Id == 83152482 && base.Bot.MonsterZone[5].Controller == 0 && base.Bot.MonsterZone[1] != null)
				{
					return false;
				}
				if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].Id == 83152482 && base.Bot.MonsterZone[6].Controller == 0 && base.Bot.MonsterZone[3] != null)
				{
					return false;
				}
				this.isSummoned = true;
				return true;
			}
			else
			{
				if (base.Bot.GetMonsters().Count((ClientCard card) => card != null && card.Id != 41685633 && card.Id != 15291624 && this.GetLinkMark(card.Id) < 3 && card.IsFaceup()) < 1)
				{
					return false;
				}
				if (base.Card.Id == 20318029 && (base.Bot.HasInExtra(50277355) || base.Bot.HasInExtra(65741786)) && base.Bot.GetMonsterCount() > 0 && !base.Bot.HasInMonstersZone(15291624, false, false, false) && !base.Bot.HasInMonstersZone(41685633, false, false, false))
				{
					this.isSummoned = true;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000D3AA0 File Offset: 0x000D1CA0
		private bool TheBystialLubellionEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return base.Card.Location == CardLocation.MonsterZone;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (this.HasInZoneNoActivate(33854624, CardLocation.Deck, false) && !base.Bot.HasInHand(33854624))
			{
				base.AI.SelectCard(33854624);
			}
			else if (this.HasInZoneNoActivate(6637331, CardLocation.Deck, false) && !base.Bot.HasInHand(6637331))
			{
				base.AI.SelectCard(6637331);
			}
			else
			{
				base.AI.SelectCard(new int[] { 6637331, 33854624 });
			}
			this.activate_TheBystialLubellion_hand = true;
			return true;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000D3B6C File Offset: 0x000D1D6C
		private bool ThunderDragonFusionEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Dark)) > 0)
				{
					if (base.Bot.Graveyard.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Light)) > 0 && this.CheckRemainInDeck(90488465) > 0)
					{
						base.AI.SelectCard(90488465);
						return true;
					}
				}
				if (base.Bot.HasInGraveyardOrInBanished(29596581) || base.Bot.HasInGraveyardOrInBanished(56713174) || base.Bot.HasInGraveyardOrInBanished(5206415) || base.Bot.HasInGraveyardOrInBanished(20318029) || (base.Bot.HasInGraveyardOrInBanished(31786629) && this.CheckRemainInDeck(83107873) > 0))
				{
					base.AI.SelectCard(83107873);
				}
				else if ((this.HasInZoneNoActivate(6637331, CardLocation.Hand, false) || this.HasInZoneNoActivate(33854624, CardLocation.Hand, false) || this.HasInZoneNoActivate(99234526, CardLocation.Hand, false) || this.HasInZoneNoActivate(61901281, CardLocation.Hand, false) || this.HasInZoneNoActivate(90488465, CardLocation.Hand, false)) && this.CheckRemainInDeck(44586426) > 0)
				{
					base.AI.SelectCard(44586426);
				}
				else if (base.Bot.HasInMonstersZone(41685633, true, false, true) && this.CheckRemainInDeck(31786629) > 1)
				{
					base.AI.SelectCard(31786629);
				}
				else if (!this.HasInZoneNoActivate(29596581, CardLocation.Deck, false))
				{
					base.AI.SelectCard(29596581);
				}
				else if (this.handActivated && this.CheckRemainInDeck(5206415) > 0)
				{
					base.AI.SelectCard(5206415);
				}
				else
				{
					base.AI.SelectCard(new int[] { 90488465, 56713174, 5206415 });
				}
				return true;
			}
			if (base.Bot.GetMonstersInMainZone().Count > 4)
			{
				if (base.Bot.GetMonstersInMainZone().Count((ClientCard card) => card != null && !card.IsExtraCard() && card.HasSetcode(284) && card.HasType(CardType.Monster) && card.IsFaceup()) <= 0)
				{
					return false;
				}
			}
			List<ClientCard> cards = base.Bot.Graveyard.ToList<ClientCard>();
			IList<ClientCard> banish = base.Bot.Banished;
			cards.AddRange(banish);
			if (base.Bot.HasInExtra(15291624) && base.Bot.GetCountCardInZone(cards, 31786629) >= 1)
			{
				if (base.Bot.GetCountCardInZone(cards, 31786629) + cards.Count((ClientCard card) => card != null && card.HasSetcode(284) && card.HasType(CardType.Monster) && !card.IsCode(31786629)) > 1)
				{
					base.AI.SelectCard(new int[] { 15291624, 41685633 });
					return true;
				}
			}
			if (base.Bot.HasInExtra(41685633))
			{
				if (cards.Count((ClientCard card) => card != null && card.HasSetcode(284) && card.HasType(CardType.Monster)) >= 3)
				{
					base.AI.SelectCard(new int[] { 15291624, 41685633 });
					return true;
				}
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			return false;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000D3F0C File Offset: 0x000D210C
		private bool TheBystialLubellionSummon()
		{
			if (base.Bot.HasInGraveyard(32731036) && base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand && this.activate_TheBystialLubellion_hand && !base.Bot.HasInGraveyard(32731036))
			{
				this.summon_TheBystialLubellion = true;
				return true;
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				this.summon_TheBystialLubellion = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000D3F88 File Offset: 0x000D2188
		private bool CheckHandThunder()
		{
			return this.HasInZoneNoActivate(29596581, CardLocation.Hand, false) || this.HasInZoneNoActivate(56713174, CardLocation.Hand, false) || this.HasInZoneNoActivate(83107873, CardLocation.Hand, false) || base.Bot.HasInHand(31786629);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000D3FE0 File Offset: 0x000D21E0
		private bool ChaosSpaceEffect_2()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if ((this.CheckRemainInDeck(95238394) > 0 || base.Bot.HasInHandOrInSpellZone(95238394)) && !base.Bot.HasInExtra(41685633))
				{
					base.AI.SelectCard(new int[] { 41685633, 15291624, 61901281, 99234526, 32731036 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 15291624, 99234526, 61901281, 32731036 });
				}
				this.activate_ChaosSpace_grave = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000D4074 File Offset: 0x000D2274
		private bool ChaosSpaceEffect()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				if (base.Bot.GetCountCardInZone(base.Bot.Hand, 20318029) > 1 && !this.activate_ThunderDragonmatrix && !this.CheckHandThunder())
				{
					if (base.Bot.Hand.Any((ClientCard card) => card != null && (card.HasAttribute(CardAttribute.Dark) || card.HasAttribute(CardAttribute.Light)) && !card.IsCode(29596581) && !card.IsCode(56713174) && !card.IsCode(83107873) && !card.IsCode(31786629)))
					{
						return false;
					}
				}
				this.ResetFlag();
				this.selectFlag[2] = true;
				this.activate_ChaosSpace_hand = true;
				if (base.Card.Location == CardLocation.Hand)
				{
					base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000D4138 File Offset: 0x000D2338
		private bool AloofLupineSummon()
		{
			if (base.Bot.Hand.Count <= 1 || !base.Bot.Hand.Any((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && card != base.Card))
			{
				return false;
			}
			if (this.CheckThunderRemove())
			{
				this.isSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000D418C File Offset: 0x000D238C
		private IList<int> GetZoneRepeatCardsId(int att, IList<ClientCard> zoneCards, bool isFaceUp = false)
		{
			if (zoneCards.Count <= 0)
			{
				return new List<int> { -1 };
			}
			IList<ClientCard> cards = zoneCards;
			if (att > 0)
			{
				cards = cards.Where((ClientCard card) => card != null && card.HasAttribute((CardAttribute)att)).ToList<ClientCard>();
			}
			if (cards.Count <= 0)
			{
				return new List<int> { -1 };
			}
			IList<int> cardsid = new List<int>();
			IList<int> res = new List<int>();
			foreach (ClientCard card2 in cards)
			{
				if (card2 != null && (card2.IsFaceup() || !isFaceUp))
				{
					cardsid.Add(card2.Id);
				}
			}
			for (int i = 0; i < cardsid.Count; i++)
			{
				if (res.Count < 0 || !res.Contains(cardsid[i]))
				{
					int times = 0;
					for (int j = 0; j < cardsid.Count; j++)
					{
						if (times > 1)
						{
							res.Add(cardsid[i]);
							break;
						}
						if (cardsid[i] == cardsid[j])
						{
							times++;
						}
					}
				}
			}
			if (res.Count <= 0)
			{
				return new List<int> { -1 };
			}
			return res;
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000D42E4 File Offset: 0x000D24E4
		private bool AllureofDarknessEffect()
		{
			if (base.Bot.Deck.Count <= 2)
			{
				return false;
			}
			if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Dark)) <= 0)
			{
				return false;
			}
			if (this.HasInZoneNoActivate(29596581, CardLocation.Hand, false) && base.Bot.GetMonstersInMainZone().Count < 5)
			{
				base.AI.SelectCard(29596581);
			}
			else if (this.HasInZoneNoActivate(56713174, CardLocation.Hand, false))
			{
				base.AI.SelectCard(56713174);
			}
			else
			{
				List<int> cardsid = new List<int>();
				IList<int> cardsid_ = this.GetZoneRepeatCardsId(32, base.Bot.Hand, false).ToList<int>();
				IList<int> cardsid_2 = new List<int>();
				if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false) && base.Bot.GetMonstersInMainZone().Count < 5)
				{
					cardsid.Add(29596581);
				}
				if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false))
				{
					cardsid.Add(56713174);
				}
				if (this.HasInZoneNoActivate(32731036, CardLocation.Hand, false))
				{
					cardsid_2.Add(6637331);
					cardsid_2.Add(33854624);
				}
				if (!base.Bot.HasInExtra(41999284) || this.isSummoned)
				{
					cardsid_2.Add(76218313);
				}
				if (!this.HasInZoneNoActivate(61901281, CardLocation.Hand, false))
				{
					cardsid_2.Add(61901281);
				}
				if (this.isSummoned)
				{
					cardsid_2.Add(92998610);
				}
				cardsid.AddRange(cardsid_);
				cardsid.AddRange(cardsid_2);
				base.AI.SelectCard(cardsid);
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				base.AI.SelectPlace(this.SelectSTPlace(base.Card, true));
			}
			return true;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000D44BC File Offset: 0x000D26BC
		private bool ThunderDragondarkEffect_2()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Duel.Player == 0)
			{
				if (this.IsShouldChainTunder())
				{
					this.activate_ThunderDragondark = true;
					this.handActivated = true;
					return true;
				}
				if (base.Duel.CurrentChain.Count > 0 || base.Duel.Phase < DuelPhase.Main1)
				{
					return false;
				}
				if (this.handActivated || (base.Bot.HasInHand(31786629) && this.CheckRemainInDeck(31786629) > 0) || !base.Bot.HasInExtra(15291624))
				{
					return false;
				}
				if (!this.isSummoned && (base.Bot.HasInHand(44586426) || base.Bot.HasInHand(92998610)))
				{
					return false;
				}
				this.activate_ThunderDragondark = true;
				this.handActivated = true;
				return true;
			}
			else
			{
				if (this.IsShouldChainTunder() || (base.Duel.Phase == DuelPhase.End && base.Bot.HasInMonstersZone(41685633, true, false, true) && base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() > 0) || (!base.Bot.HasInMonstersZone(41685633, true, false, true) && !base.Bot.HasInGraveyard(56713174)))
				{
					this.activate_ThunderDragondark = true;
					this.handActivated = true;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000D4634 File Offset: 0x000D2834
		private bool ThunderDragondarkEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			if (base.Duel.Player == 0)
			{
				if (this.handActivated && this.CheckRemainInDeck(5206415) > 0)
				{
					if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)) > 0)
					{
						if (!base.Bot.HasInMonstersZone(15291624, false, false, false))
						{
							if (this.isSummoned)
							{
								goto IL_00D3;
							}
							if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)) <= 1)
							{
								goto IL_00D3;
							}
						}
						base.AI.SelectCard(5206415);
						goto IL_0285;
					}
				}
				IL_00D3:
				if (this.HasInZoneNoActivate(83107873, CardLocation.Deck, false) && !base.Bot.HasInHand(83107873))
				{
					base.AI.SelectCard(83107873);
				}
				else
				{
					if (this.handActivated)
					{
						if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder) && card.Level < 8) > 0 && base.Bot.HasInMonstersZone(15291624, false, false, false) && this.CheckRemainInDeck(5206415) > 0)
						{
							base.AI.SelectCard(5206415);
							goto IL_0285;
						}
					}
					if (this.HasInZoneNoActivate(20318029, CardLocation.Deck, false))
					{
						base.AI.SelectCard(20318029);
					}
					else if (this.HasInZoneNoActivate(29596581, CardLocation.Hand, false) && this.handActivated && this.CheckRemainInDeck(5206415) > 0)
					{
						base.AI.SelectCard(5206415);
					}
					else if (this.CheckRemainInDeck(5206415) > 0 && this.CheckRemainInDeck(31786629) > 1)
					{
						base.AI.SelectCard(5206415);
					}
					else if (this.handActivated && base.Bot.HasInHand(5206415) && this.CheckRemainInDeck(29596581) > 0)
					{
						base.AI.SelectCard(29596581);
					}
					else if (this.CheckRemainInDeck(31786629) > 1 && !this.handActivated)
					{
						base.AI.SelectCard(31786629);
					}
					else
					{
						base.AI.SelectCard(new int[] { 20318029, 56713174, 29596581, 31786629 });
					}
				}
				IL_0285:
				this.activate_ThunderDragondark = true;
				return true;
			}
			if (!base.Bot.HasInHand(20318029) && this.HasInZoneNoActivate(20318029, CardLocation.Deck, false))
			{
				if (!base.Bot.HasInMonstersZone(41685633, true, false, true))
				{
					if (!base.Bot.HasInMonstersZone(15291624, true, false, true))
					{
						goto IL_031D;
					}
					if (base.Bot.GetGraveyardMonsters().Count((ClientCard card) => card != null && card.HasRace(CardRace.Thunder)) >= 2)
					{
						goto IL_031D;
					}
				}
				base.AI.SelectCard(20318029);
				goto IL_0339;
			}
			IL_031D:
			base.AI.SelectCard(new int[] { 95238394, 83107873, 29596581, 31786629 });
			IL_0339:
			this.activate_ThunderDragondark = true;
			return true;
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000D4984 File Offset: 0x000D2B84
		private bool NormalThunderDragonEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			this.handActivated = true;
			this.ResetFlag();
			this.selectFlag[0] = true;
			return true;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000D49B4 File Offset: 0x000D2BB4
		private bool BatterymanSolarEffect()
		{
			if (this.HasInZoneNoActivate(6637331, CardLocation.Hand, false) || this.HasInZoneNoActivate(33854624, CardLocation.Hand, false))
			{
				if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false))
				{
					base.AI.SelectCard(29596581);
				}
				else if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false) && !base.Bot.HasInMonstersZone(56713174, false, false, true))
				{
					base.AI.SelectCard(56713174);
				}
				else if (this.HasInZoneNoActivate(20318029, CardLocation.Deck, false))
				{
					base.AI.SelectCard(20318029);
				}
				else if (this.CheckRemainInDeck(31786629) > 0)
				{
					base.AI.SelectCard(31786629);
				}
				else if (this.CheckRemainInDeck(90488465) > 0)
				{
					base.AI.SelectCard(90488465);
				}
				else
				{
					base.AI.SelectCard(20318029);
				}
			}
			else if (this.HasInZoneNoActivate(99234526, CardLocation.Hand, false))
			{
				if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false))
				{
					base.AI.SelectCard(29596581);
				}
				else if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false))
				{
					base.AI.SelectCard(56713174);
				}
				else if (this.CheckRemainInDeck(90488465) > 0)
				{
					base.AI.SelectCard(90488465);
				}
				else
				{
					base.AI.SelectCard(20318029);
				}
			}
			else if (base.Bot.HasInHand(99266988) && !this.activate_ChaosSpace_hand)
			{
				if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Dark)) <= 0)
				{
					if (base.Bot.Hand.Count((ClientCard card) => card != null && card.HasAttribute(CardAttribute.Light)) <= 0)
					{
						return true;
					}
				}
				if (this.HasInZoneNoActivate(99234526, CardLocation.Deck, false) && this.HasInZoneNoActivate(61901281, CardLocation.Deck, false))
				{
					if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false))
					{
						base.AI.SelectCard(29596581);
					}
					else if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false))
					{
						base.AI.SelectCard(56713174);
					}
					else
					{
						base.AI.SelectCard(new int[] { 20318029, 83107873, 31786629 });
					}
				}
			}
			else if (this.HasInZoneNoActivate(61901281, CardLocation.Hand, false))
			{
				base.AI.SelectCard(new int[] { 20318029, 83107873 });
			}
			else if (this.HasInZoneNoActivate(29596581, CardLocation.Deck, false))
			{
				base.AI.SelectCard(29596581);
			}
			else if (this.HasInZoneNoActivate(56713174, CardLocation.Deck, false))
			{
				base.AI.SelectCard(56713174);
			}
			else
			{
				base.AI.SelectCard(new int[] { 20318029, 83107873 });
			}
			return true;
		}

		// Token: 0x04002324 RID: 8996
		private const int THUNDER_COUNTD = 18;

		// Token: 0x04002325 RID: 8997
		private List<bool> selectFlag = new List<bool> { false, false, false, false, false, false, false };

		// Token: 0x04002326 RID: 8998
		private List<bool> selectAtt = new List<bool> { false, false, false, false, false, false, false };

		// Token: 0x04002327 RID: 8999
		private bool isSummoned;

		// Token: 0x04002328 RID: 9000
		private bool handActivated;

		// Token: 0x04002329 RID: 9001
		private bool place_CrossSheep;

		// Token: 0x0400232A RID: 9002
		private bool place_ThunderDragonColossus;

		// Token: 0x0400232B RID: 9003
		private bool place_Link_4;

		// Token: 0x0400232C RID: 9004
		private bool summon_WhiteDragonWyverburster;

		// Token: 0x0400232D RID: 9005
		private bool summon_BlackDragonCollapserpent;

		// Token: 0x0400232E RID: 9006
		private bool summon_UnionCarrier;

		// Token: 0x0400232F RID: 9007
		private bool summon_TheBystialLubellion;

		// Token: 0x04002330 RID: 9008
		private bool activate_ThunderDragonFusion;

		// Token: 0x04002331 RID: 9009
		private bool activate_ThunderDragondark;

		// Token: 0x04002332 RID: 9010
		private bool activate_ThunderDragonroar;

		// Token: 0x04002333 RID: 9011
		private bool activate_ThunderDragonhawk;

		// Token: 0x04002334 RID: 9012
		private bool activate_ThunderDragonmatrix;

		// Token: 0x04002335 RID: 9013
		private bool activate_TheBystialLubellion_hand;

		// Token: 0x04002336 RID: 9014
		private bool activate_BystialMagnamhut_hand;

		// Token: 0x04002337 RID: 9015
		private bool activate_BystialDruiswurm_hand;

		// Token: 0x04002338 RID: 9016
		private bool activate_ChaosSpace_grave;

		// Token: 0x04002339 RID: 9017
		private bool activate_ChaosSpace_hand;

		// Token: 0x0400233A RID: 9018
		private bool No_SpSummon;

		// Token: 0x0400233B RID: 9019
		private List<int> SpSummonCardsId = new List<int> { 20318029, 44586426, 14558127, 23434538, 76218313, 92998610 };

		// Token: 0x0400233C RID: 9020
		private List<int> NotSpSummonCardsId = new List<int> { 5206415, 32731036, 90488465, 61901281, 99234526 };

		// Token: 0x0400233D RID: 9021
		private List<int> Impermanence_list = new List<int>();

		// Token: 0x0400233E RID: 9022
		private List<int> should_not_negate = new List<int> { 81275020, 28985331 };

		// Token: 0x02000403 RID: 1027
		public class CardId
		{
			// Token: 0x0400233F RID: 9023
			public const int ThunderDragonlord = 5206415;

			// Token: 0x04002340 RID: 9024
			public const int TheBystialLubellion = 32731036;

			// Token: 0x04002341 RID: 9025
			public const int TheChaosCreator = 90488465;

			// Token: 0x04002342 RID: 9026
			public const int BystialDruiswurm = 6637331;

			// Token: 0x04002343 RID: 9027
			public const int BystialMagnamhut = 33854624;

			// Token: 0x04002344 RID: 9028
			public const int ThunderDragonroar = 29596581;

			// Token: 0x04002345 RID: 9029
			public const int ThunderDragonhawk = 83107873;

			// Token: 0x04002346 RID: 9030
			public const int NormalThunderDragon = 31786629;

			// Token: 0x04002347 RID: 9031
			public const int ThunderDragondark = 56713174;

			// Token: 0x04002348 RID: 9032
			public const int BlackDragonCollapserpent = 61901281;

			// Token: 0x04002349 RID: 9033
			public const int WhiteDragonWyverburster = 99234526;

			// Token: 0x0400234A RID: 9034
			public const int AloofLupine = 92998610;

			// Token: 0x0400234B RID: 9035
			public const int BatterymanSolar = 44586426;

			// Token: 0x0400234C RID: 9036
			public const int AshBlossom = 14558127;

			// Token: 0x0400234D RID: 9037
			public const int G = 23434538;

			// Token: 0x0400234E RID: 9038
			public const int DragonBusterDestructionSword = 76218313;

			// Token: 0x0400234F RID: 9039
			public const int ThunderDragonmatrix = 20318029;

			// Token: 0x04002350 RID: 9040
			public const int AllureofDarkness = 1475311;

			// Token: 0x04002351 RID: 9041
			public const int GoldSarcophagus = 75500286;

			// Token: 0x04002352 RID: 9042
			public const int ThunderDragonFusion = 95238394;

			// Token: 0x04002353 RID: 9043
			public const int ChaosSpace = 99266988;

			// Token: 0x04002354 RID: 9044
			public const int CalledbytheGrave = 24224830;

			// Token: 0x04002355 RID: 9045
			public const int BrandedRegained = 34090915;

			// Token: 0x04002356 RID: 9046
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x04002357 RID: 9047
			public const int BatterymanToken = 44586427;

			// Token: 0x04002358 RID: 9048
			public const int ThunderDragonTitan = 41685633;

			// Token: 0x04002359 RID: 9049
			public const int ThunderDragonColossus = 15291624;

			// Token: 0x0400235A RID: 9050
			public const int AbyssDweller = 21044178;

			// Token: 0x0400235B RID: 9051
			public const int UnderworldGoddessoftheClosedWorld = 98127546;

			// Token: 0x0400235C RID: 9052
			public const int MekkKnightCrusadiaAvramax = 21887175;

			// Token: 0x0400235D RID: 9053
			public const int AccesscodeTalker = 86066372;

			// Token: 0x0400235E RID: 9054
			public const int BowoftheGoddess = 4280258;

			// Token: 0x0400235F RID: 9055
			public const int KnightmareUnicorn = 38342335;

			// Token: 0x04002360 RID: 9056
			public const int UnionCarrier = 83152482;

			// Token: 0x04002361 RID: 9057
			public const int IP = 65741786;

			// Token: 0x04002362 RID: 9058
			public const int CrossSheep = 50277355;

			// Token: 0x04002363 RID: 9059
			public const int PredaplantVerteAnaconda = 70369116;

			// Token: 0x04002364 RID: 9060
			public const int StrikerDragon = 73539069;

			// Token: 0x04002365 RID: 9061
			public const int Linkuriboh = 41999284;
		}

		// Token: 0x02000404 RID: 1028
		private enum Select
		{
			// Token: 0x04002367 RID: 9063
			NormalThunderDragon,
			// Token: 0x04002368 RID: 9064
			TheChaosCreator,
			// Token: 0x04002369 RID: 9065
			ChaosSpace_1,
			// Token: 0x0400236A RID: 9066
			ChaosSpace_2,
			// Token: 0x0400236B RID: 9067
			ThunderDragonColossus,
			// Token: 0x0400236C RID: 9068
			AccesscodeTalker,
			// Token: 0x0400236D RID: 9069
			DestroyReplace
		}
	}
}
