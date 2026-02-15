using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200037A RID: 890
	[Deck("MalissOCG", "AI_MalissOCG", "Normal")]
	public class MalissOCGExecutor : DefaultExecutor
	{
		// Token: 0x060018E7 RID: 6375 RVA: 0x00094FDC File Offset: 0x000931DC
		public MalissOCGExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.GoToBattlePhase, new Func<bool>(this.GoToBattlePhase));
			base.AddExecutor(ExecutorType.Activate, 91800273, new Func<bool>(this.Effect_Enemy_Turn));
			base.AddExecutor(ExecutorType.Activate, 42141493, new Func<bool>(this.Effect_Enemy_Turn));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.Effect_Enemy_Turn));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.Effect_Infinite_Impermanence));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 40366667, new Func<bool>(this.Effect_Enemy_Chain));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(base.DefaultCalledByTheGrave));
			base.AddExecutor(ExecutorType.Activate, 69272449, new Func<bool>(this.Effect_White_Rabbit));
			base.AddExecutor(ExecutorType.Activate, 9763474, new Func<bool>(this.Effect_Haggard_Lizardose));
			base.AddExecutor(ExecutorType.Activate, 59859086);
			base.AddExecutor(ExecutorType.Activate, 52698008);
			base.AddExecutor(ExecutorType.Activate, 92422871);
			base.AddExecutor(ExecutorType.Activate, 46947713);
			base.AddExecutor(ExecutorType.Activate, 9940036, new Func<bool>(this.Effect_Mereologic_Aggregator));
			base.AddExecutor(ExecutorType.Activate, 64211118, new Func<bool>(this.Effect_Firewall_Dragon));
			base.AddExecutor(ExecutorType.Activate, 39138610, new Func<bool>(this.Effect_Allied_Code_Talker_Ignister));
			base.AddExecutor(ExecutorType.Activate, 95454996, new Func<bool>(this.Effect_Maliss_Link));
			base.AddExecutor(ExecutorType.Activate, 68059897, new Func<bool>(this.Effect_Maliss_Link));
			base.AddExecutor(ExecutorType.Activate, 21848500, new Func<bool>(this.Effect_Maliss_Hearts_Crypter));
			base.AddExecutor(ExecutorType.Activate, 30342076);
			base.AddExecutor(ExecutorType.Summon, 32061192, new Func<bool>(this.Summon_Maliss_Dormouse));
			base.AddExecutor(ExecutorType.Summon, 69272449, new Func<bool>(this.Summon_Maliss_White_Rabbit));
			base.AddExecutor(ExecutorType.Summon, 96676583, new Func<bool>(this.Summon_Maliss_Chessy_Cat));
			base.AddExecutor(ExecutorType.Summon, 30118811, new Func<bool>(this.Summon_Backup_Ignister));
			base.AddExecutor(ExecutorType.Activate, 32061192, new Func<bool>(this.Effect_Maliss_Dormouse));
			base.AddExecutor(ExecutorType.SpSummon, 68059897, new Func<bool>(this.SP_Maliss_Link));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet_Maliss));
			base.AddExecutor(ExecutorType.Activate, 96676583, new Func<bool>(this.Effect_Maliss_Chessy_Cat));
			base.AddExecutor(ExecutorType.SpSummon, 59859086, new Func<bool>(this.SP_Splash_Mage));
			base.AddExecutor(ExecutorType.SpSummon, 9763474, new Func<bool>(this.SP_Haggard_Lizardose));
			base.AddExecutor(ExecutorType.SpSummon, 30342076, new Func<bool>(this.SP_Link_Decoder));
			base.AddExecutor(ExecutorType.SpSummon, 52698008, new Func<bool>(this.SP_Cyberse_Wicckid));
			base.AddExecutor(ExecutorType.Activate, 68337209, new Func<bool>(this.Effect_Remove));
			base.AddExecutor(ExecutorType.Activate, 75500286, new Func<bool>(this.Effect_Remove));
			base.AddExecutor(ExecutorType.Activate, 57111661, new Func<bool>(this.Effect_Maliss_TB_11));
			base.AddExecutor(ExecutorType.Activate, 94722358, new Func<bool>(this.Effect_Maliss_MTP_07));
			base.AddExecutor(ExecutorType.Activate, 20938824, new Func<bool>(this.Effect_Maliss_March_Hare));
			base.AddExecutor(ExecutorType.Activate, 30118811);
			base.AddExecutor(ExecutorType.Activate, 3723262, new Func<bool>(this.Effect_Wizard_Ignister));
			base.AddExecutor(ExecutorType.Activate, 93453053, new Func<bool>(this.Effect_Maliss_in_the_Mirror));
			base.AddExecutor(ExecutorType.SpSummon, 21848500, new Func<bool>(this.SP_Maliss_Hearts_Crypter));
			base.AddExecutor(ExecutorType.SpSummon, 95454996, new Func<bool>(this.SP_Maliss_White_Binder));
			base.AddExecutor(ExecutorType.Activate, 20726052, new Func<bool>(this.Effect_Maliss_GWC_06));
			base.AddExecutor(ExecutorType.SpSummon, 64211118, new Func<bool>(this.SP_Firewall_Dragon));
			base.AddExecutor(ExecutorType.SpSummon, 39138610, new Func<bool>(this.SP_Allied_Code_Talker_Ignister));
			base.AddExecutor(ExecutorType.SpellSet, 93453053);
			base.AddExecutor(ExecutorType.SpSummon, 46947713, new Func<bool>(this.SP_Transcode_Talker));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00095444 File Offset: 0x00093644
		public override void OnNewTurn()
		{
			this.Count.AddPhase();
			this.Count.Clear();
			base.OnNewTurn();
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00095464 File Offset: 0x00093664
		public override void OnChaining(int player, ClientCard card)
		{
			if (card.Id == 91800273 || card.Id == 34267821)
			{
				this.Count.AddCard(card.Id);
			}
			else if (player == 0)
			{
				if (card.Location == CardLocation.Removed)
				{
					this.Count.AddCardRemoved(card.Id);
				}
				else
				{
					this.Count.AddCard(card.Id);
				}
			}
			if (player == 1)
			{
				this.Count.AddActivateOppo(card.Id);
			}
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000954E4 File Offset: 0x000936E4
		public override void OnChainEnd()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(91800273) && !this.Count.CheckCard(base.Card.Id))
			{
				this.Count.Dimension_Shifter = 0;
			}
			this.Count.Oppo.Clear();
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00095534 File Offset: 0x00093734
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == base.Util.GetStringId(95454996, 3))
			{
				return true;
			}
			if (desc == base.Util.GetStringId(94722358, 3))
			{
				return base.Enemy.GetMonsters().Count((ClientCard i) => !i.IsShouldNotBeTarget()) + base.Enemy.GetSpells().Count((ClientCard i) => !i.IsShouldNotBeTarget() && (i.HasType((CardType)917504) || i.IsFacedown())) > 0;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000955D8 File Offset: 0x000937D8
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location == CardLocation.MonsterZone)
			{
				if (new List<int> { 68059897, 59859086 }.Contains(cardId))
				{
					base.AI.SendCustomChat(0, Array.Empty<object>());
				}
				if (new List<int> { 52698008, 39138610 }.Contains(cardId))
				{
					if ((64 & available) > 0 && (base.Bot.MonsterZone[3] == null || base.Bot.MonsterZone[4] == null))
					{
						return 64;
					}
					if ((32 & available) > 0 && (base.Bot.MonsterZone[0] == null || base.Bot.MonsterZone[1] == null))
					{
						return 32;
					}
				}
				if (base.Bot.HasInMonstersZone(52698008, false, false, false) && this.Count.CheckCard(52698008))
				{
					int seq = 0;
					for (int i = 0; i < 7; i++)
					{
						if (base.Bot.MonsterZone[i] != null && base.Bot.MonsterZone[i].IsCode(52698008))
						{
							seq = i;
						}
					}
					if (seq == 5)
					{
						if ((2 & available) > 0)
						{
							return 2;
						}
						if ((4 & available) > 0)
						{
							return 4;
						}
					}
					else if (seq == 6)
					{
						if ((8 & available) > 0)
						{
							return 8;
						}
						if ((16 & available) > 0)
						{
							return 16;
						}
					}
				}
				if (cardId == 46947713)
				{
					if ((64 & available) > 0 && base.Bot.MonsterZone[3] == null)
					{
						return 64;
					}
					if ((32 & available) > 0 && base.Bot.MonsterZone[1] == null)
					{
						return 32;
					}
					if ((1 & available) > 0 && base.Bot.MonsterZone[1] == null)
					{
						return 1;
					}
					if ((2 & available) > 0 && base.Bot.MonsterZone[2] == null)
					{
						return 2;
					}
					if ((4 & available) > 0 && base.Bot.MonsterZone[3] == null)
					{
						return 4;
					}
					if ((8 & available) > 0 && base.Bot.MonsterZone[4] == null)
					{
						return 8;
					}
				}
				if (cardId == 39138610)
				{
					MalissOCGExecutor.ZoneData[] array = new MalissOCGExecutor.ZoneData[]
					{
						new MalissOCGExecutor.ZoneData
						{
							Zone = 1,
							CheckZone = new ClientCard[] { base.Bot.MonsterZone[1] }
						},
						new MalissOCGExecutor.ZoneData
						{
							Zone = 2,
							CheckZone = new ClientCard[]
							{
								base.Bot.MonsterZone[0],
								base.Bot.MonsterZone[2]
							}
						},
						new MalissOCGExecutor.ZoneData
						{
							Zone = 4,
							CheckZone = new ClientCard[]
							{
								base.Bot.MonsterZone[1],
								base.Bot.MonsterZone[3]
							}
						},
						new MalissOCGExecutor.ZoneData
						{
							Zone = 8,
							CheckZone = new ClientCard[]
							{
								base.Bot.MonsterZone[2],
								base.Bot.MonsterZone[4]
							}
						},
						new MalissOCGExecutor.ZoneData
						{
							Zone = 16,
							CheckZone = new ClientCard[] { base.Bot.MonsterZone[3] }
						},
						new MalissOCGExecutor.ZoneData
						{
							Zone = 32,
							CheckZone = new ClientCard[]
							{
								base.Bot.MonsterZone[0],
								base.Bot.MonsterZone[1],
								base.Bot.MonsterZone[2]
							}
						},
						new MalissOCGExecutor.ZoneData
						{
							Zone = 64,
							CheckZone = new ClientCard[]
							{
								base.Bot.MonsterZone[2],
								base.Bot.MonsterZone[3],
								base.Bot.MonsterZone[4]
							}
						}
					};
					int maxNullCount = -1;
					int selectedZone = 0;
					foreach (MalissOCGExecutor.ZoneData data in array)
					{
						if ((data.Zone & available) != 0)
						{
							int nullCount = data.CheckZone.Count((ClientCard card) => card == null);
							if (nullCount > maxNullCount)
							{
								maxNullCount = nullCount;
								selectedZone = data.Zone;
							}
						}
					}
					if (maxNullCount >= 0)
					{
						return selectedZone;
					}
				}
				if ((64 & available) > 0)
				{
					return 64;
				}
				if ((32 & available) > 0)
				{
					return 32;
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00095A5C File Offset: 0x00093C5C
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (base.AI.HaveSelectedCards())
			{
				return null;
			}
			ClientCard card = base.Duel.GetCurrentSolvingChainCard();
			if (card == null)
			{
				card = base.Card;
			}
			int id = card.Id;
			if (id <= 52698008)
			{
				if (id <= 20938824)
				{
					if (id <= 9763474)
					{
						if (id != 3723262)
						{
							if (id != 9763474)
							{
								goto IL_3005;
							}
						}
						else
						{
							if (cards.Any((ClientCard i) => i.HasSetcode(447)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447)).ToList<ClientCard>(), cards, min, max);
							}
							goto IL_3005;
						}
					}
					else if (id != 9940036)
					{
						if (id != 20726052)
						{
							if (id != 20938824)
							{
								goto IL_3005;
							}
							if (hint == 506)
							{
								if (cards.Any((ClientCard i) => this.Count.CheckCard(i.Id) && i.IsCode(20938824)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCard(i.Id) && i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => this.Count.CheckCard(i.Id) && !i.HasType(CardType.Link)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCard(i.Id) && !i.HasType(CardType.Link)).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => !i.HasType(CardType.Link)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => !i.HasType(CardType.Link)).ToList<ClientCard>(), cards, min, max);
								}
								goto IL_3005;
							}
							else
							{
								if (hint != 503)
								{
									goto IL_3005;
								}
								if (base.Duel.Player == 1)
								{
									if (base.Bot.GetMonstersInMainZone().Count<ClientCard>() > 3)
									{
										if (cards.Any((ClientCard i) => i.HasType(CardType.Spell) && i.Location == CardLocation.Grave))
										{
											return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasType(CardType.Spell) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
										}
										if (cards.Any((ClientCard i) => !i.HasType(CardType.Link) && i.Location == CardLocation.Grave))
										{
											return base.Util.CheckSelectCount(cards.Where((ClientCard i) => !i.HasType(CardType.Link) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
										}
									}
									else if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasType(CardType.Link) && i.Location == CardLocation.Grave))
									{
										return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasType(CardType.Link) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
									}
								}
								if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && !i.HasType(CardType.Trap) && i.Location == CardLocation.Grave))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && !i.HasType(CardType.Trap) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => !i.HasType(CardType.Trap) && i.Location == CardLocation.Grave))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => !i.HasType(CardType.Trap) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && !i.HasType(CardType.Trap)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && !i.HasType(CardType.Trap)).ToList<ClientCard>(), cards, min, max);
								}
								goto IL_3005;
							}
						}
						else if (hint == 503)
						{
							if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
							}
							goto IL_3005;
						}
						else
						{
							if (hint != 509)
							{
								goto IL_3005;
							}
							if (cards.Any((ClientCard i) => i.IsCode(95454996) && i.Location == CardLocation.Grave))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(95454996) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.HasType(CardType.Link) && !this.Count.CheckCardRemoved(i.Id) && i.Location == CardLocation.Grave))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasType(CardType.Link) && !this.Count.CheckCardRemoved(i.Id) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
							}
							goto IL_3005;
						}
					}
					else
					{
						if (cards.Any((ClientCard i) => i.Controller == 1 && this.Count.CheckActivateOppo(i.Id)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Controller == 1 && this.Count.CheckActivateOppo(i.Id)).ToList<ClientCard>(), cards, min, max);
						}
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Controller == 1).ToList<ClientCard>(), cards, min, max);
					}
				}
				else if (id <= 30118811)
				{
					if (id != 21848500)
					{
						if (id != 30118811)
						{
							goto IL_3005;
						}
						if (hint == 506)
						{
							if (card.Id == 91800273 || card.Id == 34267821)
							{
								if (!base.Bot.Hand.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && !i.IsCode(20938824)) && cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && !i.IsCode(20938824)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && !i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
								}
								if (!base.Bot.Hand.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && !i.IsCode(20938824)) && cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && !i.IsCode(20938824)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && !i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
								}
							}
							if (cards.Any((ClientCard i) => i.IsCode(3723262)) && base.Bot.Hand.Count<ClientCard>() > 0)
							{
								if (!base.Bot.Graveyard.Any((ClientCard i) => i.HasRace(CardRace.Cyberse)))
								{
									if (base.Bot.HasInExtra(30342076))
									{
										if (base.Bot.GetMonsters().Any((ClientCard i) => i.Level <= 4 && i.HasRace(CardRace.Cyberse)) && this.Count.CheckCard(91800273))
										{
											goto IL_2A62;
										}
									}
									if (!base.Bot.HasInExtra(9763474) || this.Count.CheckCard(91800273) || !this.Count.CheckCard(34267821))
									{
										goto IL_2A9B;
									}
									if ((from i in (from i in base.Bot.GetMonsters()
											where i.IsFaceup() && (!i.HasType(CardType.Link) || i.LinkCount < 2)
											select i).ToList<ClientCard>()
										group i by i.Id into i
										select i.First<ClientCard>()).Count<ClientCard>() < 2)
									{
										goto IL_2A9B;
									}
								}
								IL_2A62:
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(3723262)).ToList<ClientCard>(), cards, min, max);
							}
							IL_2A9B:
							if (base.Bot.HasInHand(20938824))
							{
								if (cards.Any((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id)).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
								}
							}
							else if (cards.Any((ClientCard i) => i.IsCode(20938824)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.HasSetcode(447)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447)).ToList<ClientCard>(), cards, min, max);
							}
							goto IL_3005;
						}
						else
						{
							if (hint != 501)
							{
								goto IL_3005;
							}
							if (card.Id == 91800273 || card.Id == 34267821)
							{
								if (cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && !i.IsCode(20938824)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && !i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && !i.IsCode(20938824)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && !i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
								}
							}
							if (base.Bot.HasInHand(20938824))
							{
								if (!base.Bot.Graveyard.Any((ClientCard i) => i.HasSetcode(447)))
								{
									if (cards.Any((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id)))
									{
										return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id)).ToList<ClientCard>(), cards, min, max);
									}
									if (cards.Any((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id)))
									{
										return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824) && this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
									}
									if (cards.Any((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824)))
									{
										return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && !i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
									}
									if (cards.Count((ClientCard i) => i.IsCode(20938824)) > 1)
									{
										return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
									}
								}
							}
							if (cards.Any((ClientCard i) => this.TrashCards(i.Id, CardLocation.Hand)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.TrashCards(i.Id, CardLocation.Hand)).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => !i.HasType(CardType.Monster)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => !i.HasType(CardType.Monster)).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => !i.HasRace(CardRace.Cyberse)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => !i.HasRace(CardRace.Cyberse)).ToList<ClientCard>(), cards, min, max);
							}
							goto IL_3005;
						}
					}
					else
					{
						if (hint != 503)
						{
							goto IL_3005;
						}
						if (cards.Any((ClientCard i) => i.Controller == 1))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Controller == 1).ToList<ClientCard>(), cards, min, max);
						}
						goto IL_3005;
					}
				}
				else if (id != 32061192)
				{
					if (id != 39138610)
					{
						if (id != 52698008)
						{
							goto IL_3005;
						}
					}
					else if (hint == 509)
					{
						if (cards.Any((ClientCard i) => !i.IsCode(95454996)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => !i.IsCode(95454996)).ToList<ClientCard>(), cards, max, max);
						}
						return base.OnSelectCard(cards, max, max, hint, false);
					}
					else
					{
						if (hint != 500)
						{
							goto IL_3005;
						}
						if (cards.Any((ClientCard i) => i.LinkCount < 4))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.LinkCount < 4).ToList<ClientCard>(), cards, max, max);
						}
						goto IL_3005;
					}
				}
				else if (this.Count.CheckSummon())
				{
					if (cards.Any((ClientCard i) => i.Id == 69272449) && !base.Bot.HasInHand(69272449) && this.Check_Maliss_White_Rabbit() && this.Count.CheckCardRemoved(69272449))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
					}
					if (cards.Any((ClientCard i) => i.Id == 96676583) && !base.Bot.HasInHand(96676583) && this.Check_Maliss_Chessy_Cat() && this.Count.CheckCardRemoved(96676583))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
					}
					if (cards.Any((ClientCard i) => i.Id == 20938824) && this.Check_Maliss_March_Hare(CardLocation.Removed))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
					}
					goto IL_3005;
				}
				else
				{
					if (cards.Any((ClientCard i) => i.Id == 69272449) && this.Count.CheckCardRemoved(69272449) && this.Check_Maliss_White_Rabbit())
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
					}
					if (cards.Any((ClientCard i) => i.Id == 96676583) && this.Count.CheckCardRemoved(96676583) && this.Check_Maliss_Chessy_Cat())
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
					}
					if (cards.Any((ClientCard i) => i.Id == 20938824) && this.Check_Maliss_March_Hare(CardLocation.Removed))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
					}
					goto IL_3005;
				}
			}
			else if (id <= 68337209)
			{
				if (id <= 59859086)
				{
					if (id != 57111661)
					{
						if (id != 59859086)
						{
							goto IL_3005;
						}
					}
					else if (hint == 509)
					{
						if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
						{
							if (cards.Any((ClientCard i) => i.Id == 32061192) && this.Count.CheckCardRemoved(32061192))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 69272449) && this.Count.CheckCardRemoved(69272449))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 20938824) && this.Count.CheckCardRemoved(20938824))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)).ToList<ClientCard>(), cards, min, max);
							}
						}
						if (cards.Any((ClientCard i) => i.Id == 32061192) && this.Count.CheckCard(32061192))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 69272449) && this.Count.CheckCard(69272449))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 96676583) && this.Count.CheckCard(96676583))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
						}
						goto IL_3005;
					}
					else
					{
						if (hint != 503)
						{
							goto IL_3005;
						}
						if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447) && i.HasType(CardType.Link)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasType(CardType.Link)).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
						}
						goto IL_3005;
					}
				}
				else if (id != 64211118)
				{
					if (id != 68059897)
					{
						if (id != 68337209)
						{
							goto IL_3005;
						}
						if (this.Count.CheckSummon())
						{
							if (cards.Any((ClientCard i) => i.Id == 32061192 && i.Location == CardLocation.Deck) && !base.Bot.HasInHand(32061192) && this.Check_Maliss_Dormouse() && this.Count.CheckCardRemoved(32061192))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192 && i.Location == CardLocation.Deck).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 69272449 && i.Location == CardLocation.Deck) && !base.Bot.HasInHand(69272449) && this.Check_Maliss_White_Rabbit() && this.Count.CheckCardRemoved(69272449))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449 && i.Location == CardLocation.Deck).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 20938824 && i.Location == CardLocation.Deck) && this.Check_Maliss_March_Hare(CardLocation.Removed) && this.Count.CheckCard(20938824))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 96676583 && i.Location == CardLocation.Deck) && !base.Bot.HasInHand(96676583) && this.Count.CheckCardRemoved(96676583))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583 && i.Location == CardLocation.Deck).ToList<ClientCard>(), cards, min, max);
							}
						}
						else
						{
							if (cards.Any((ClientCard i) => i.Id == 32061192 && i.Location == CardLocation.Deck) && this.Count.CheckCardRemoved(32061192) && this.Check_Maliss_Dormouse())
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 69272449 && i.Location == CardLocation.Deck) && this.Count.CheckCardRemoved(69272449) && this.Check_Maliss_White_Rabbit())
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 20938824 && i.Location == CardLocation.Deck) && this.Check_Maliss_March_Hare(CardLocation.Removed) && this.Count.CheckCard(20938824))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => i.Id == 96676583 && i.Location == CardLocation.Deck) && this.Count.CheckCardRemoved(96676583))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
							}
						}
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Location == CardLocation.Deck).ToList<ClientCard>(), cards, min, max);
					}
					else if (hint == 506)
					{
						List<ClientCard> chk_cards = base.Bot.Graveyard.ToList<ClientCard>();
						chk_cards.AddRange(base.Bot.GetSpells());
						chk_cards.AddRange(base.Bot.Hand);
						if (cards.Any((ClientCard i) => i.IsCode(93453053)) && this.Check_Maliss_in_the_Mirror(CardLocation.Removed))
						{
							if (chk_cards.Any((ClientCard i) => i.HasType(CardType.Trap)) && ((((base.Bot.HasInHand(96676583) && this.Count.CheckSummon()) || base.Bot.HasInMonstersZone(96676583, false, false, false)) && this.Count.CheckCard(96676583)) || (base.Bot.HasInHand(20938824) && this.Count.CheckCard(20938824))) && this.Count.CheckCard(93453053))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(93453053)).ToList<ClientCard>(), cards, min, max);
							}
						}
						if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(93453053)).ToList<ClientCard>(), cards, min, max);
						}
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(68337209)).ToList<ClientCard>(), cards, min, max);
					}
					else
					{
						if (hint != 503)
						{
							goto IL_3005;
						}
						if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
						{
							if (cards.Any((ClientCard i) => i.Id == 96676583) && this.Count.CheckCardRemoved(96676583))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
							}
							if (cards.Any((ClientCard i) => !i.IsCode(96676583) && this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => !i.IsCode(96676583) && this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)).ToList<ClientCard>(), cards, min, max);
							}
						}
						if (cards.Any((ClientCard i) => i.Id == 20938824) && this.Check_Maliss_March_Hare(CardLocation.Removed))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 32061192) && this.Count.CheckCardRemoved(32061192) && this.Check_Maliss_Dormouse())
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 69272449) && this.Count.CheckCardRemoved(69272449) && this.Check_Maliss_White_Rabbit())
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 96676583) && this.Count.CheckCardRemoved(96676583) && this.Check_Maliss_Chessy_Cat())
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
						}
						goto IL_3005;
					}
				}
				else
				{
					if (base.Enemy.GetMonsters().Count((ClientCard i) => !i.IsShouldNotBeTarget() && i.IsFaceup()) + base.Enemy.GetSpells().Count((ClientCard i) => !i.IsShouldNotBeTarget() && i.HasType((CardType)917504)) > 0 && base.Duel.Player == 1)
					{
						if (cards.Any((ClientCard i) => i.IsCode(9940036)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(9940036)).ToList<ClientCard>(), cards, min, max);
						}
					}
					if (cards.Any((ClientCard i) => i.IsCode(92422871)))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(92422871)).ToList<ClientCard>(), cards, min, max);
					}
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Location == CardLocation.Deck).ToList<ClientCard>(), cards, min, max);
				}
			}
			else if (id <= 93453053)
			{
				if (id != 69272449)
				{
					if (id != 75500286)
					{
						if (id != 93453053)
						{
							goto IL_3005;
						}
						if (hint == 503)
						{
							if (!cards.Any((ClientCard i) => i.Location != CardLocation.Grave))
							{
								if (cards.Any((ClientCard i) => i.HasType(CardType.Trap)) && !base.Bot.HasInHandOrInSpellZoneOrInGraveyard(20726052) && !base.Bot.HasInBanished(20726052))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasType(CardType.Trap)).ToList<ClientCard>(), cards, min, max);
								}
								goto IL_3005;
							}
							else
							{
								if (cards.Any((ClientCard i) => i.IsCode(68059897)) && this.Count.CheckCardRemoved(68059897))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(68059897)).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => i.HasType(CardType.Link) && i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasType(CardType.Link) && i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
								}
								if (cards.Any((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id)))
								{
									return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
								}
								goto IL_3005;
							}
						}
						else if (hint == 506)
						{
							if (cards.Any((ClientCard i) => i.IsCode(20726052)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(20726052)).ToList<ClientCard>(), cards, min, max);
							}
							goto IL_3005;
						}
						else
						{
							if (hint == 573 && cards.Contains(base.Util.GetLastChainCard()))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { base.Util.GetLastChainCard() }, cards, min, max);
							}
							goto IL_3005;
						}
					}
					else if (this.Count.CheckSummon())
					{
						if (cards.Any((ClientCard i) => i.Id == 32061192) && !base.Bot.HasInHand(32061192) && this.Check_Maliss_Dormouse() && this.Count.CheckCardRemoved(32061192))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 69272449) && !base.Bot.HasInHand(69272449) && this.Check_Maliss_White_Rabbit() && this.Count.CheckCardRemoved(69272449))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 96676583) && !base.Bot.HasInHand(96676583) && this.Check_Maliss_Chessy_Cat() && this.Count.CheckCardRemoved(96676583))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 20938824) && this.Check_Maliss_March_Hare(CardLocation.Removed))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
						}
						goto IL_3005;
					}
					else
					{
						if (cards.Any((ClientCard i) => i.Id == 32061192) && this.Count.CheckCardRemoved(32061192) && this.Check_Maliss_Dormouse())
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 69272449) && this.Count.CheckCardRemoved(69272449) && this.Check_Maliss_White_Rabbit())
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 96676583) && this.Count.CheckCardRemoved(96676583) && this.Check_Maliss_Chessy_Cat())
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.Id == 20938824) && this.Check_Maliss_March_Hare(CardLocation.Removed))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
						}
						goto IL_3005;
					}
				}
				else
				{
					if (cards.Any((ClientCard i) => i.Id == 57111661) && this.Count.CheckCard(57111661))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 57111661).ToList<ClientCard>(), cards, min, max);
					}
					if (cards.Any((ClientCard i) => i.Id == 20726052) && this.Count.CheckCard(20726052))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20726052).ToList<ClientCard>(), cards, min, max);
					}
					if (cards.Any((ClientCard i) => i.Id == 94722358) && this.Count.CheckCard(94722358))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 94722358).ToList<ClientCard>(), cards, min, max);
					}
					goto IL_3005;
				}
			}
			else if (id != 94722358)
			{
				if (id != 95454996)
				{
					if (id != 96676583)
					{
						goto IL_3005;
					}
					if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
					{
						if (cards.Any((ClientCard i) => i.IsCode(20938824)))
						{
							if (!base.Bot.Graveyard.Any((ClientCard i) => i.HasSetcode(447)) && !base.Bot.Hand.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id)))
							{
								return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
							}
						}
						if (cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && !i.IsCode(20938824)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && !i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && !i.IsCode(20938824)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && !i.IsCode(20938824)).ToList<ClientCard>(), cards, min, max);
						}
						if (cards.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id)))
						{
							return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
						}
					}
					if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.IsCode(93453053)) && this.Check_Maliss_in_the_Mirror(CardLocation.Grave))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.IsCode(93453053)).ToList<ClientCard>(), cards, min, max);
					}
					if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id)))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
					}
					goto IL_3005;
				}
				else if (hint == 503)
				{
					List<ClientCard> result = new List<ClientCard>();
					if (5 - base.Bot.GetMonstersInMainZone().Count > 0 && this.Count.CheckCard(39138610))
					{
						result.AddRange(cards.Where((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id) && i.Controller == 0 && i.HasType(CardType.Link)));
						if (base.Duel.Player == 1)
						{
							result.AddRange(cards.Where((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id) && i.Controller == 0 && i.IsCode(69272449)));
						}
						result.AddRange(cards.Where((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id) && i.Controller == 0 && i.HasType(CardType.Monster)));
						result.AddRange(cards.Where((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id) && i.Controller == 0 && i.HasType(CardType.Spell)));
					}
					result.AddRange(cards.Where((ClientCard i) => i.Controller == 1));
					result.AddRange(cards.Where((ClientCard i) => this.TrashCards(i.Id, CardLocation.Grave)));
					result.AddRange(cards.Where((ClientCard i) => !i.HasSetcode(447) && !i.HasType(CardType.Trap)));
					result.AddRange(cards.Where((ClientCard i) => !i.HasSetcode(447) && i.HasType(CardType.Trap)));
					if (result.Count<ClientCard>() > max)
					{
						result = result.Take(max).ToList<ClientCard>();
					}
					if (result.Count<ClientCard>() > 0)
					{
						return base.Util.CheckSelectCount(result, cards, result.Count<ClientCard>(), result.Count<ClientCard>());
					}
					if (cards.Any((ClientCard i) => this.TrashCards(i.Id, CardLocation.Grave)))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.TrashCards(i.Id, CardLocation.Grave)).ToList<ClientCard>(), cards, min, min);
					}
					return base.Util.CheckSelectCount(cards, cards, min, min);
				}
				else
				{
					if (hint != 510)
					{
						goto IL_3005;
					}
					if (cards.Any((ClientCard i) => i.IsCode(20726052)))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(20726052)).ToList<ClientCard>(), cards, min, max);
					}
					goto IL_3005;
				}
			}
			else if (hint == 506)
			{
				if (base.Duel.Player == 1)
				{
					if (cards.Any((ClientCard i) => i.Id == 20938824) && this.Count.CheckCard(20938824) && this.Check_Maliss_March_Hare(CardLocation.Hand))
					{
						return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 20938824).ToList<ClientCard>(), cards, min, max);
					}
				}
				if (cards.Any((ClientCard i) => i.Id == 32061192) && this.Count.CheckCard(32061192))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 32061192).ToList<ClientCard>(), cards, min, max);
				}
				if (cards.Any((ClientCard i) => i.Id == 69272449) && this.Count.CheckCard(69272449))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 69272449).ToList<ClientCard>(), cards, min, max);
				}
				if (cards.Any((ClientCard i) => i.Id == 96676583) && this.Count.CheckCard(96676583))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Id == 96676583).ToList<ClientCard>(), cards, min, max);
				}
				goto IL_3005;
			}
			else
			{
				if (hint != 503)
				{
					goto IL_3005;
				}
				if (cards.Any((ClientCard i) => i.Controller == 1))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Controller == 1).ToList<ClientCard>(), cards, min, max);
				}
				if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447) && i.HasType(CardType.Link)))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasType(CardType.Link)).ToList<ClientCard>(), cards, min, max);
				}
				if (cards.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id)).ToList<ClientCard>(), cards, min, max);
				}
				goto IL_3005;
			}
			if (hint == 503)
			{
				if (cards.Any((ClientCard i) => this.Count.CheckCard(i.Id) && this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447) && i.Location == CardLocation.Grave))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => this.Count.CheckCard(i.Id) && this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447) && i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
				}
				if (cards.Any((ClientCard i) => i.Location == CardLocation.Grave))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.Location == CardLocation.Grave).ToList<ClientCard>(), cards, min, max);
				}
			}
			else if (hint == 509)
			{
				if (cards.Any((ClientCard i) => i.HasSetcode(447)))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.HasSetcode(447)).ToList<ClientCard>(), cards, min, max);
				}
			}
			else if (hint == 551)
			{
				if (cards.Any((ClientCard i) => i.IsCode(9763474)))
				{
					return base.Util.CheckSelectCount(cards.Where((ClientCard i) => i.IsCode(9763474)).ToList<ClientCard>(), cards, min, max);
				}
			}
			IL_3005:
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00098A7C File Offset: 0x00096C7C
		private bool TrashCards(int code, CardLocation loc)
		{
			if (loc == CardLocation.Grave)
			{
				return new List<int> { 23434538, 34267821, 91800273, 42141493, 10045474, 40366667, 14558127, 24224830, 75500286 }.Contains(code);
			}
			if (loc == CardLocation.Hand)
			{
				if (base.Bot.GetFieldCount() > 0 && code == 42141493)
				{
					return true;
				}
				if (base.Bot.Graveyard.Count > 0 && code == 91800273)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00098B38 File Offset: 0x00096D38
		private bool MonsterRepos()
		{
			return (!base.Enemy.GetMonsters().Any((ClientCard i) => i.IsDefense()) && base.Util.GetTotalAttackingMonsterAttack(0) + base.Card.Attack >= base.Enemy.LifePoints + base.Util.GetTotalAttackingMonsterAttack(1) && base.Card.IsDefense()) || base.Card.IsFacedown();
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00098BC2 File Offset: 0x00096DC2
		private bool SpellSet()
		{
			return base.Card.HasType((CardType)65540);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00098BD4 File Offset: 0x00096DD4
		private bool SpellSet_Maliss()
		{
			if (base.Card.HasType(CardType.Trap) && base.Card.HasSetcode(447))
			{
				return base.Bot.GetMonsters().Any((ClientCard i) => i.HasSetcode(447));
			}
			return false;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00098C34 File Offset: 0x00096E34
		private bool Effect_Enemy_Turn()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Id == 91800273 && base.Duel.Player == 1 && this.Count.CheckCard(base.Card.Id))
			{
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return base.Duel.Player == 1;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00098CB0 File Offset: 0x00096EB0
		private bool Effect_Enemy_Chain()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			return LastChainCard != null && LastChainCard.Controller == 1;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00098CE8 File Offset: 0x00096EE8
		private bool Effect_Infinite_Impermanence()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int this_seq = -1;
				int that_seq = -1;
				for (int i = 0; i < 5; i++)
				{
					if (base.Bot.SpellZone[i] == base.Card)
					{
						this_seq = i;
					}
					if (this.Count.CheckPosition(this_seq))
					{
						return false;
					}
					if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && base.Enemy.SpellZone[i] == LastChainCard)
					{
						that_seq = i;
					}
					else if (base.Duel.Player == 0 && base.Util.GetProblematicEnemySpell() != null && base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFloodgate())
					{
						that_seq = i;
					}
				}
				if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4) || base.Util.IsChainTarget(base.Card) || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(18144506)))
				{
					this.Count.AddPosition(this_seq);
					return true;
				}
			}
			else if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(LastChainCard);
				return true;
			}
			return false;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00098E34 File Offset: 0x00097034
		private bool Effect_Maliss_Removed(int lp = 300)
		{
			int ct = 5 - base.Bot.GetMonstersInMainZone().Count;
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.HasType(CardType.Monster) && !base.Card.IsCode(20938824))
			{
				if (ct - base.Duel.CurrentChain.Count((ClientCard i) => i.HasSetcode(447) && i.Location == CardLocation.Removed && i.HasType(CardType.Monster)) <= 0)
				{
					return false;
				}
			}
			if (base.Bot.LifePoints > lp && base.Card.Location == CardLocation.Removed)
			{
				this.Count.AddCardRemoved(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00098EF0 File Offset: 0x000970F0
		private bool Effect_Maliss_Chessy_Cat()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.ActivateDescription != base.Util.GetStringId(96676583, 0))
			{
				return this.Effect_Maliss_Removed(300);
			}
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				if (base.Bot.Hand.Any((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id) && !i.HasType(CardType.Trap) && (!i.IsCode(93453053) || this.Check_Maliss_in_the_Mirror(CardLocation.Removed))))
				{
					this.Count.AddCard(base.Card.Id);
					return true;
				}
				if (base.Bot.HasInHand(20938824))
				{
					if (!base.Bot.Graveyard.Any((ClientCard i) => i.HasSetcode(447)))
					{
						this.Count.AddCard(base.Card.Id);
						return true;
					}
				}
				return false;
			}
			else
			{
				if (base.Bot.Hand.Any((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id) && !i.HasType(CardType.Trap)))
				{
					this.Count.AddCard(base.Card.Id);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0009902C File Offset: 0x0009722C
		private bool Effect_Maliss_March_Hare()
		{
			if (base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().IsCode(39138610))
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return this.Effect_Maliss_Removed(300);
			}
			if (base.Duel.Player == 1)
			{
				if (!base.Bot.Graveyard.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Link)) || base.Bot.GetMonstersInMainZone().Count<ClientCard>() > 3)
				{
					return false;
				}
			}
			if (base.Bot.HasInMonstersZone(96676583, false, false, false) && this.Count.CheckCard(96676583))
			{
				return false;
			}
			if (this.Check_Maliss_March_Hare(CardLocation.Hand))
			{
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0009912C File Offset: 0x0009732C
		private bool Effect_Maliss_Dormouse()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return this.Effect_Maliss_Removed(300);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0009917A File Offset: 0x0009737A
		private bool Summon_Maliss_Chessy_Cat()
		{
			if (this.Check_Maliss_Chessy_Cat())
			{
				this.Count.AddSummon();
				return true;
			}
			return false;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00099194 File Offset: 0x00097394
		private bool Effect_White_Rabbit()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return this.Effect_Maliss_Removed(300);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000991E2 File Offset: 0x000973E2
		private bool Summon_Maliss_Dormouse()
		{
			if (this.Check_Maliss_Dormouse())
			{
				this.Count.AddSummon();
				return true;
			}
			return false;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000991FA File Offset: 0x000973FA
		private bool Summon_Maliss_White_Rabbit()
		{
			if (this.Check_Maliss_White_Rabbit())
			{
				this.Count.AddSummon();
				return true;
			}
			return false;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00099214 File Offset: 0x00097414
		private bool Check_Maliss_in_the_Mirror(CardLocation loc)
		{
			if (loc != CardLocation.Removed)
			{
				List<ClientCard> monsters = base.Bot.Hand.GetMonsters();
				monsters.AddRange(base.Bot.GetMonsters());
				return monsters.Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id));
			}
			if (!this.Count.CheckCard(34267821) || !this.Count.CheckCardRemoved(20938824))
			{
				return false;
			}
			List<ClientCard> cards = base.Bot.Hand.ToList<ClientCard>();
			cards.AddRange(base.Bot.GetMonsters());
			cards.AddRange(base.Bot.GetSpells());
			cards.AddRange(base.Bot.Graveyard);
			cards.AddRange(base.Bot.Banished);
			return base.Bot.Graveyard.Any(delegate(ClientCard i)
			{
				if (i.HasSetcode(447))
				{
					if (i.HasType(CardType.Monster))
					{
						if (cards.Count((ClientCard j) => j.HasType(CardType.Monster)) < 10)
						{
							return true;
						}
					}
					if (i.HasType(CardType.Spell))
					{
						if (cards.Count((ClientCard j) => j.HasType(CardType.Spell)) < 4)
						{
							return true;
						}
					}
					if (i.HasType(CardType.Trap))
					{
						return cards.Count((ClientCard j) => j.HasType(CardType.Trap)) < 3;
					}
					return false;
				}
				return false;
			});
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00099312 File Offset: 0x00097512
		private bool Check_Maliss_Chessy_Cat()
		{
			return base.Bot.Hand.Any((ClientCard i) => i.HasSetcode(447) && !i.HasType(CardType.Trap) && i != base.Card && this.Count.CheckCardRemoved(i.Id)) && this.Count.CheckCard(96676583);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00099344 File Offset: 0x00097544
		private bool Check_Maliss_White_Rabbit()
		{
			return base.Bot.Graveyard.Count((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Trap)) < 3 && this.Count.CheckCard(69272449);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00099398 File Offset: 0x00097598
		private bool Check_Maliss_Dormouse()
		{
			List<ClientCard> list = base.Bot.Hand.ToList<ClientCard>();
			list.AddRange(base.Bot.GetMonsters());
			list.AddRange(base.Bot.GetSpells());
			list.AddRange(base.Bot.Graveyard);
			list.AddRange(base.Bot.Banished);
			return list.Count((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster)) < 9 && this.Count.CheckCard(32061192);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00099434 File Offset: 0x00097634
		private bool Check_Maliss_March_Hare(CardLocation loc)
		{
			if (loc == CardLocation.Removed)
			{
				if (!this.Count.CheckCard(34267821))
				{
					return false;
				}
				return (base.Bot.Banished.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster)) || this.Count.CheckCard(20938824)) && this.Count.CheckCardRemoved(20938824);
			}
			else
			{
				if (!this.Count.CheckCard(34267821))
				{
					return false;
				}
				return base.Bot.Graveyard.GetMonsters().Any((ClientCard i) => i.HasSetcode(447));
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000994F8 File Offset: 0x000976F8
		private bool Effect_Maliss_TB_11()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() > 1 && (!base.Bot.HasInMonstersZone(68059897, false, false, false) || !base.Bot.HasInMonstersZone(95454996, false, false, false)))
			{
				return false;
			}
			if (base.Bot.GetMonsters().Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id)))
			{
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			if (base.Bot.HasInMonstersZone(68059897, false, false, false) && this.Count.CheckCardRemoved(68059897))
			{
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000995C8 File Offset: 0x000977C8
		private bool Effect_Maliss_MTP_07()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if (!base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player != 0)
			{
				if (base.Enemy.GetMonsters().Count((ClientCard i) => !i.IsShouldNotBeTarget()) + base.Enemy.GetSpells().Count((ClientCard i) => !i.IsShouldNotBeTarget() && (i.HasType((CardType)917504) || i.IsFacedown())) != 0)
				{
					if (base.Bot.GetMonsters().Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Link)))
					{
						if (base.Bot.GetMonsterCount() > 1 && (!base.Bot.HasInMonstersZone(68059897, false, false, false) || !base.Bot.HasInMonstersZone(95454996, false, false, false)))
						{
							return false;
						}
						if (base.Bot.GetMonsters().Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id)) && ((this.Count.CheckCard(20938824) && !base.Bot.HasInHand(20938824) && this.Check_Maliss_March_Hare(CardLocation.Hand)) || (this.Count.CheckSummon() && ((this.Count.CheckCard(32061192) && !base.Bot.HasInMonstersZone(32061192, false, false, false) && this.Check_Maliss_Dormouse()) || (this.Count.CheckCard(69272449) && !base.Bot.HasInMonstersZone(69272449, false, false, false) && this.Check_Maliss_White_Rabbit()) || (this.Count.CheckCard(96676583) && !base.Bot.HasInMonstersZone(96676583, false, false, false) && this.Check_Maliss_Chessy_Cat())))))
						{
							this.Count.AddCard(base.Card.Id);
							return true;
						}
						if (base.Bot.HasInMonstersZone(68059897, false, false, false) && this.Count.CheckCardRemoved(68059897))
						{
							this.Count.AddCard(base.Card.Id);
							return true;
						}
						return false;
					}
				}
			}
			return false;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0009981C File Offset: 0x00097A1C
		private bool Effect_Maliss_GWC_06()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Duel.Player == 0 && base.Bot.HasInGraveyard(95454996))
			{
				return false;
			}
			if (base.Bot.GetMonsters().Any((ClientCard i) => this.Count.CheckCardRemoved(i.Id)))
			{
				if (base.Bot.Graveyard.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Link)))
				{
					this.Count.AddCard(base.Card.Id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000998C4 File Offset: 0x00097AC4
		private bool Effect_Remove()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if ((!base.Bot.HasInMonstersZone(32061192, false, false, false) && this.Count.CheckCard(32061192) && this.Count.CheckCardRemoved(32061192) && this.Check_Maliss_Dormouse()) || (!base.Bot.HasInMonstersZone(69272449, false, false, false) && this.Count.CheckCard(69272449) && this.Count.CheckCardRemoved(69272449) && this.Check_Maliss_White_Rabbit()) || (!base.Bot.HasInMonstersZone(96676583, false, false, false) && this.Count.CheckCard(96676583) && this.Count.CheckCardRemoved(69272449) && this.Check_Maliss_Chessy_Cat()))
			{
				return true;
			}
			if (this.Count.CheckCardRemoved(20938824) && this.Check_Maliss_March_Hare(CardLocation.Removed))
			{
				return base.Bot.Banished.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster));
			}
			return false;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000999FC File Offset: 0x00097BFC
		private bool SP_Splash_Mage()
		{
			if (base.Bot.GetMonsters().Count((ClientCard i) => !i.HasType(CardType.Link) || i.LinkCount < 2) < 2 && (!base.Bot.HasInMonstersZone(68059897, false, false, false) || !this.Count.CheckCardRemoved(68059897) || base.Bot.GetMonsters().Count<ClientCard>() != 2 || !this.Count.CheckCard(95454996)))
			{
				return false;
			}
			bool chk;
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				chk = base.Bot.Graveyard.Any((ClientCard i) => i.HasType(CardType.Monster) && i.HasRace(CardRace.Cyberse) && !i.HasType(CardType.Link));
			}
			else
			{
				chk = true;
			}
			if (chk)
			{
				List<ClientCard> materials = (from card in base.Bot.GetMonsters()
					where card != null && card.IsFaceup() && !card.HasType(CardType.Link)
					select card).ToList<ClientCard>();
				base.AI.SelectMaterials(materials, 0);
			}
			return chk;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00099B2C File Offset: 0x00097D2C
		private bool SP_Cyberse_Wicckid()
		{
			if (!this.Count.CheckCard(34267821))
			{
				return false;
			}
			if (!this.Count.CheckCard(91800273))
			{
				if (!base.Bot.Graveyard.Any((ClientCard i) => i.HasRace(CardRace.Cyberse)))
				{
					return false;
				}
			}
			if (base.Bot.HasInHand(30118811) || !this.Count.CheckCard(30118811))
			{
				return false;
			}
			if (base.Bot.GetMonsters().Any((ClientCard i) => i.IsFaceup() && i.Level <= 4 && i.HasRace(CardRace.Cyberse)) && base.Bot.GetMonsterCount() == 3 && this.Count.CheckCard(30118811) && base.Bot.Hand.Count > 0 && base.Bot.HasInExtra(21848500) && base.Bot.HasInExtra(30342076))
			{
				List<ClientCard> materials = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card.Sequence > 4
					select card).ToList<ClientCard>();
				materials.AddRange((from card in base.Bot.GetMonsters()
					where card.IsFaceup() && !card.HasType(CardType.Link)
					select card).ToList<ClientCard>());
				base.AI.SelectMaterials(materials, 0);
				return true;
			}
			if (base.Bot.GetMonsters().Count((ClientCard i) => !i.HasType(CardType.Link) || i.LinkCount < 2) < 2 || (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].HasType(CardType.Link) && base.Bot.MonsterZone[5].LinkCount > 3) || (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].HasType(CardType.Link) && base.Bot.MonsterZone[6].LinkCount > 3))
			{
				return false;
			}
			if (!base.Bot.HasInHand(30118811) || !this.Count.CheckCard(30118811) || base.Bot.GetMonstersInMainZone().Count<ClientCard>() >= 5)
			{
				if (base.Bot.HasInHand(3723262) && this.Count.CheckCard(3723262))
				{
					if (base.Bot.Graveyard.Any((ClientCard i) => i.HasRace(CardRace.Cyberse) && i.HasAttribute(CardAttribute.Dark)))
					{
						if (base.Bot.Graveyard.Count((ClientCard i) => i.HasRace(CardRace.Cyberse)) > 1 && base.Bot.GetMonstersInMainZone().Count<ClientCard>() < 4)
						{
							goto IL_0397;
						}
					}
				}
				if (base.Bot.HasInHand(20938824) && this.Count.CheckCard(20938824) && this.Check_Maliss_March_Hare(CardLocation.Hand))
				{
					if (base.Bot.Graveyard.Count((ClientCard i) => i.HasRace(CardRace.Cyberse)) > 1 && base.Bot.GetMonstersInMainZone().Count<ClientCard>() < 4)
					{
						goto IL_0397;
					}
				}
				return false;
			}
			IL_0397:
			List<ClientCard> materials2 = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && card.Sequence > 4
				select card).ToList<ClientCard>();
			materials2.AddRange((from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.HasType(CardType.Link)
				select card).ToList<ClientCard>());
			base.AI.SelectMaterials(materials2, 0);
			return true;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00099F4F File Offset: 0x0009814F
		private bool Effect_Haggard_Lizardose()
		{
			return base.Bot.Graveyard.Any((ClientCard i) => i.HasType(CardType.Monster) && i.Attack <= 2000);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00099F80 File Offset: 0x00098180
		private bool SP_Haggard_Lizardose()
		{
			List<ClientCard> cards = (from i in (from i in base.Bot.GetMonsters()
					where i.IsFaceup() && (!i.HasType(CardType.Link) || i.LinkCount < 2)
					select i).ToList<ClientCard>()
				group i by i.Id into i
				select i.First<ClientCard>()).ToList<ClientCard>();
			if (cards.Count < 2)
			{
				return false;
			}
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821) && cards.Any((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id)))
			{
				List<ClientCard> materials2 = cards.Where((ClientCard i) => this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)).ToList<ClientCard>();
				materials2.AddRange(cards.Where((ClientCard i) => !this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)));
				materials2.AddRange(cards.Where((ClientCard i) => !i.HasSetcode(447)));
				base.AI.SelectMaterials(materials2, 0);
				return true;
			}
			if (base.Bot.HasInExtra(59859086))
			{
				return false;
			}
			bool chk;
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				chk = base.Bot.Graveyard.Any((ClientCard i) => i.HasType(CardType.Monster) && i.BaseAttack <= 2000 && this.Count.CheckCardRemoved(i.Id) && this.Count.CheckCard(i.Id) && i.HasSetcode(447));
			}
			else
			{
				chk = base.Bot.GetMonsters().Any((ClientCard i) => i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && i.BaseAttack <= 2000 && i.HasSetcode(447)) || base.Bot.Graveyard.Any((ClientCard i) => i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id) && i.BaseAttack <= 2000 && i.HasSetcode(447));
			}
			if (chk)
			{
				List<ClientCard> materials = cards.Where((ClientCard i) => i.BaseAttack <= 2000 && this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)).ToList<ClientCard>();
				materials.AddRange(cards.Where((ClientCard i) => !materials.Contains(i)));
				base.AI.SelectMaterials(materials, 0);
			}
			return chk;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0009A1AC File Offset: 0x000983AC
		private bool SP_Maliss_Link()
		{
			if (base.Bot.GetMonsters().Any((ClientCard i) => i.HasType(CardType.Link) && i.LinkCount == 2))
			{
				if (base.Bot.GetMonsters().Any((ClientCard i) => !i.HasType(CardType.Link) && i.HasSetcode(447)))
				{
					goto IL_0119;
				}
			}
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				if (base.Bot.HasInExtra(new int[] { 52698008, 59859086 }))
				{
					if (base.Bot.Graveyard.Any((ClientCard i) => i.HasRace(CardRace.Cyberse)))
					{
						return false;
					}
				}
				if (!base.Bot.HasInExtra(9763474) || !base.Bot.Graveyard.Any((ClientCard i) => i.HasSetcode(447) && i.HasType(CardType.Monster) && this.Count.CheckCardRemoved(i.Id)))
				{
					goto IL_0119;
				}
			}
			return false;
			IL_0119:
			List<ClientCard> materials = (from card in base.Bot.GetMonsters()
				where card != null && card.IsFaceup() && card.LinkCount == 2
				select card).ToList<ClientCard>();
			foreach (ClientCard card2 in (from card in base.Bot.GetMonsters()
				where card != null && card.IsFaceup() && card.LinkCount < 2 && card.HasSetcode(447)
				select card).ToList<ClientCard>())
			{
				if (materials.Count == 2)
				{
					break;
				}
				if (card2.LinkCount <= 2)
				{
					materials.Add(card2);
				}
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0009A3A0 File Offset: 0x000985A0
		private bool Effect_Maliss_Link()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (base.Card.IsCode(95454996))
				{
					if (base.ActivateDescription == base.Util.GetStringId(base.Card.Id, 1))
					{
						this.Count.AddCard(base.Card.Id);
						return true;
					}
					if (!base.Bot.Graveyard.Any((ClientCard i) => i.HasSetcode(447) && this.Count.CheckCardRemoved(i.Id)))
					{
						base.Enemy.Graveyard.Count<ClientCard>();
						this.Count.AddCard(base.Card.Id);
						return true;
					}
				}
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return this.Effect_Maliss_Removed(900);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0009A488 File Offset: 0x00098688
		private bool SP_Link_Decoder()
		{
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				if (base.Bot.GetMonsters().Any((ClientCard i) => i.HasSetcode(447) && i.Level <= 4 && this.Count.CheckCardRemoved(i.Id)))
				{
					base.AI.SelectMaterials((from i in base.Bot.GetMonsters()
						where i.HasSetcode(447) && i.Level <= 4 && this.Count.CheckCardRemoved(i.Id)
						select i).ToList<ClientCard>(), 0);
					return true;
				}
				return false;
			}
			else
			{
				if (base.Bot.HasInHand(20938824) && this.Count.CheckCard(20938824) && base.Bot.GetMonsters().Any((ClientCard i) => i.HasSetcode(447) && i.Level <= 4 && this.Count.CheckCardRemoved(i.Id)))
				{
					base.AI.SelectMaterials((from i in base.Bot.GetMonsters()
						where i.HasSetcode(447) && i.Level <= 4 && this.Count.CheckCardRemoved(i.Id)
						select i).ToList<ClientCard>(), 0);
					return true;
				}
				if (base.Bot.GetMonsters().Any((ClientCard i) => i.LinkCount < 3 && i.HasSetcode(447)))
				{
					if (base.Bot.GetMonsters().Count((ClientCard i) => i.LinkCount < 3) >= 3)
					{
						base.AI.SelectMaterials((from i in base.Bot.GetMonsters()
							where i.LinkCount < 3 && i.HasSetcode(447)
							select i).ToList<ClientCard>(), 0);
						return true;
					}
				}
				if (base.Bot.HasInMonstersZone(52698008, false, false, false))
				{
					base.AI.SelectMaterials(52698008, 0);
					return true;
				}
				if (base.Bot.HasInMonstersZone(30118811, false, false, false) && base.Bot.HasInHand(3723262))
				{
					base.AI.SelectMaterials(30118811, 0);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0009A684 File Offset: 0x00098884
		private bool Effect_Maliss_in_the_Mirror()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(93453053, 0))
			{
				ClientCard LastChainCard = base.Util.GetLastChainCard();
				return base.Duel.Player == 1 && LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.MonsterZone;
			}
			return this.Effect_Maliss_Removed(0);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0009A6F4 File Offset: 0x000988F4
		private bool SP_Maliss_Hearts_Crypter()
		{
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				if (!base.Bot.HasInExtra(68059897))
				{
					if (base.Bot.GetMonsters().Count((ClientCard i) => !i.HasType(CardType.Link) || i.LinkCount < 2) >= (base.Bot.GetMonsters().Any((ClientCard i) => i.HasType(CardType.Link) && i.LinkCount == 2) ? 1 : 3))
					{
						base.AI.SelectMaterials((from i in base.Bot.GetMonsters()
							where !i.HasType(CardType.Link) || i.LinkCount < 2
							select i).ToList<ClientCard>(), 0);
						return true;
					}
				}
				return false;
			}
			if (base.Bot.HasInMonstersZone(30342076, false, false, false))
			{
				if (base.Bot.GetMonsters().Count((ClientCard i) => !i.HasType(CardType.Link) || i.LinkCount <= 2) > 2)
				{
					goto IL_015F;
				}
			}
			if (base.Bot.GetMonsters().Count((ClientCard i) => !i.HasType(CardType.Link) || i.LinkCount <= 2) <= 4)
			{
				return false;
			}
			IL_015F:
			base.AI.SelectMaterials((from i in base.Bot.GetMonsters()
				where !i.HasType(CardType.Link) || i.LinkCount < 2
				select i).ToList<ClientCard>(), 0);
			return true;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0009A8A4 File Offset: 0x00098AA4
		private bool SP_Maliss_White_Binder()
		{
			if (base.Bot.GetMonsters().Any((ClientCard i) => i.HasSetcode(447) && i.LinkCount == 3 && this.Count.CheckCardRemoved(i.Id)))
			{
				base.AI.SelectMaterials((from i in base.Bot.GetMonsters()
					where (this.Count.CheckCardRemoved(i.Id) && i.HasSetcode(447)) || i.LinkCount < 3
					select i).ToList<ClientCard>(), 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0009A900 File Offset: 0x00098B00
		private bool Effect_Maliss_Hearts_Crypter()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return this.Effect_Maliss_Removed(900);
			}
			if (base.Enemy.GetMonsters().Count((ClientCard i) => !i.IsShouldNotBeTarget()) + base.Enemy.GetSpells().Count((ClientCard i) => !i.IsShouldNotBeTarget() && (i.HasType((CardType)917504) || i.IsFacedown())) > 0 && base.Duel.LastChainPlayer != 0)
			{
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0009A9C4 File Offset: 0x00098BC4
		private bool SP_Firewall_Dragon()
		{
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				return false;
			}
			List<ClientCard> materials = (from i in base.Bot.GetMonsters()
				where i.IsCode(95454996)
				select i).ToList<ClientCard>();
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.Sequence > 4 && i.HasType(CardType.Link) && i.LinkCount <= 3
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.HasSetcode(447) && !i.HasType(CardType.Link)
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.HasSetcode(447) && i.HasType(CardType.Link)
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.Sequence < 5 && i.HasType(CardType.Link) && i.LinkCount <= 3
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where !i.HasType(CardType.Link)
				select i);
			if (materials.Count > 3)
			{
				materials = materials.Take(3).ToList<ClientCard>();
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0009AB5C File Offset: 0x00098D5C
		private bool SP_Allied_Code_Talker_Ignister()
		{
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				return false;
			}
			if (base.Bot.GetMonsters().Count((ClientCard i) => i.LinkCount <= 3) < 3)
			{
				return false;
			}
			List<ClientCard> materials = (from i in base.Bot.GetMonsters()
				where i.IsCode(95454996)
				select i).ToList<ClientCard>();
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.Sequence > 4 && i.HasType(CardType.Link) && i.LinkCount <= 3
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.HasSetcode(447) && !i.HasType(CardType.Link)
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.HasSetcode(447) && i.HasType(CardType.Link)
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.Sequence < 5 && i.HasType(CardType.Link) && i.LinkCount <= 3
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where !i.HasType(CardType.Link)
				select i);
			if (materials.Count > 3)
			{
				materials = materials.Take(3).ToList<ClientCard>();
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0009AD28 File Offset: 0x00098F28
		private bool Effect_Allied_Code_Talker_Ignister()
		{
			if (base.ActivateDescription != base.Util.GetStringId(39138610, 1) || base.Duel.LastChainPlayer != 1)
			{
				return true;
			}
			if (base.Card.Sequence > 4)
			{
				return base.Bot.GetMonsters().Any((ClientCard i) => i.Sequence < 3 && (!i.HasType(CardType.Link) || (i.LinkCount <= 3 && this.Count.CheckCard(i.Id))));
			}
			return base.Bot.GetMonsters().Any((ClientCard i) => i.Sequence < 5 && (i.Sequence - base.Card.Sequence == 1 || base.Card.Sequence - i.Sequence == 1) && (!i.HasType(CardType.Link) || (i.LinkCount <= 3 && this.Count.CheckCard(i.Id))));
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0009ADA8 File Offset: 0x00098FA8
		private bool Effect_Mereologic_Aggregator()
		{
			if (base.Enemy.GetMonsters().Count((ClientCard i) => !i.IsShouldNotBeTarget()) + base.Enemy.GetSpells().Count((ClientCard i) => !i.IsShouldNotBeTarget() && (i.HasType((CardType)917504) || i.IsFacedown())) > 0)
			{
				ClientCard LastChainCard = base.Util.GetLastChainCard();
				if (LastChainCard != null && LastChainCard.Controller == 1 && (LastChainCard.Location == CardLocation.MonsterZone || LastChainCard.Location == CardLocation.SpellZone))
				{
					base.AI.SelectCard(LastChainCard);
				}
				this.Count.AddCard(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0009AE68 File Offset: 0x00099068
		private bool Effect_Firewall_Dragon()
		{
			if (!this.Count.CheckCard(91800273) && this.Count.CheckCard(34267821))
			{
				return false;
			}
			if (base.Duel.Player != 0)
			{
				return base.Enemy.GetMonsters().Count((ClientCard i) => !i.IsShouldNotBeTarget() && i.IsFaceup()) + base.Enemy.GetSpells().Count((ClientCard i) => !i.IsShouldNotBeTarget() && i.HasType((CardType)917504)) > 0 && base.Duel.LastChainPlayer != 0;
			}
			return true;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0009AF1C File Offset: 0x0009911C
		private bool Summon_Backup_Ignister()
		{
			if (base.Bot.GetMonsters().Any((ClientCard i) => i.HasType(CardType.Link)))
			{
				return false;
			}
			this.Count.AddSummon();
			return true;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0009AF68 File Offset: 0x00099168
		private bool SP_Transcode_Talker()
		{
			if (base.Bot.GetMonsters().Any((ClientCard i) => i.HasSetcode(447)))
			{
				return false;
			}
			if (!base.Bot.GetMonsters().Any((ClientCard i) => !i.HasType(CardType.Link) || i.LinkCount < 2))
			{
				return false;
			}
			if (!base.Bot.GetMonsters().Any((ClientCard i) => i.HasType(CardType.Link) && i.LinkCount == 2))
			{
				return false;
			}
			List<ClientCard> materials = (from i in base.Bot.GetMonsters()
				where i.HasType(CardType.Link) && i.LinkCount == 2
				select i).ToList<ClientCard>();
			materials.AddRange(from i in base.Bot.GetMonsters()
				where i.IsCode(30342076)
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where !i.HasType(CardType.Link) || (i.LinkCount < 2 && i.Sequence == ((materials[0].Sequence > 4) ? ((materials[0].Sequence == 5) ? 1 : 3) : (materials[0].Sequence + 1)))
				select i);
			materials.AddRange(from i in base.Bot.GetMonsters()
				where !i.HasType(CardType.Link) || i.LinkCount < 2
				select i);
			if (materials.Count > 2)
			{
				materials = materials.Take(2).ToList<ClientCard>();
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00056B7D File Offset: 0x00054D7D
		private bool Effect_Wizard_Ignister()
		{
			return base.Card.Location == CardLocation.Hand;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0009B124 File Offset: 0x00099324
		private bool GoToBattlePhase()
		{
			return !base.Enemy.GetMonsters().Any((ClientCard i) => i.IsDefense()) && base.Util.GetTotalAttackingMonsterAttack(0) >= base.Enemy.LifePoints + base.Util.GetTotalAttackingMonsterAttack(1);
		}

		// Token: 0x04001CA2 RID: 7330
		public MalissOCGExecutor.CardCount Count = new MalissOCGExecutor.CardCount();

		// Token: 0x0200037B RID: 891
		public class SetCode
		{
			// Token: 0x04001CA3 RID: 7331
			public const int Maliss = 447;
		}

		// Token: 0x0200037C RID: 892
		public class CardId
		{
			// Token: 0x04001CA4 RID: 7332
			public const int Artifact_Lancea = 34267821;

			// Token: 0x04001CA5 RID: 7333
			public const int Dimension_Shifter = 91800273;

			// Token: 0x04001CA6 RID: 7334
			public const int MaxxG = 23434538;

			// Token: 0x04001CA7 RID: 7335
			public const int Mulcharmy_Fuwalos = 42141493;

			// Token: 0x04001CA8 RID: 7336
			public const int Infinite_Impermanence = 10045474;

			// Token: 0x04001CA9 RID: 7337
			public const int Dominus_Impulse = 40366667;

			// Token: 0x04001CAA RID: 7338
			public const int AshBlossom = 14558127;

			// Token: 0x04001CAB RID: 7339
			public const int CalledbytheGrave = 24224830;

			// Token: 0x04001CAC RID: 7340
			public const int Gold_Sarcophagus = 75500286;

			// Token: 0x04001CAD RID: 7341
			public const int Wizard_Ignister = 3723262;

			// Token: 0x04001CAE RID: 7342
			public const int Backup_Ignister = 30118811;

			// Token: 0x04001CAF RID: 7343
			public const int Maliss_Chessy_Cat = 96676583;

			// Token: 0x04001CB0 RID: 7344
			public const int Maliss_White_Rabbit = 69272449;

			// Token: 0x04001CB1 RID: 7345
			public const int Maliss_Dormouse = 32061192;

			// Token: 0x04001CB2 RID: 7346
			public const int Maliss_March_Hare = 20938824;

			// Token: 0x04001CB3 RID: 7347
			public const int Maliss_in_the_Mirror = 93453053;

			// Token: 0x04001CB4 RID: 7348
			public const int Maliss_in_Underground = 68337209;

			// Token: 0x04001CB5 RID: 7349
			public const int Maliss_GWC_06 = 20726052;

			// Token: 0x04001CB6 RID: 7350
			public const int Maliss_TB_11 = 57111661;

			// Token: 0x04001CB7 RID: 7351
			public const int Maliss_MTP_07 = 94722358;

			// Token: 0x04001CB8 RID: 7352
			public const int Mereologic_Aggregator = 9940036;

			// Token: 0x04001CB9 RID: 7353
			public const int Cyberse_Desavewurm = 92422871;

			// Token: 0x04001CBA RID: 7354
			public const int Allied_Code_Talker_Ignister = 39138610;

			// Token: 0x04001CBB RID: 7355
			public const int Firewall_Dragon = 64211118;

			// Token: 0x04001CBC RID: 7356
			public const int Accesscode_Talker = 86066372;

			// Token: 0x04001CBD RID: 7357
			public const int Maliss_Hearts_Crypter = 21848500;

			// Token: 0x04001CBE RID: 7358
			public const int Maliss_Red_Ransom = 68059897;

			// Token: 0x04001CBF RID: 7359
			public const int Maliss_White_Binder = 95454996;

			// Token: 0x04001CC0 RID: 7360
			public const int Transcode_Talker = 46947713;

			// Token: 0x04001CC1 RID: 7361
			public const int Splash_Mage = 59859086;

			// Token: 0x04001CC2 RID: 7362
			public const int Haggard_Lizardose = 9763474;

			// Token: 0x04001CC3 RID: 7363
			public const int Cyberse_Wicckid = 52698008;

			// Token: 0x04001CC4 RID: 7364
			public const int Link_Decoder = 30342076;
		}

		// Token: 0x0200037D RID: 893
		public class CardCount
		{
			// Token: 0x0600197E RID: 6526 RVA: 0x0009BC5C File Offset: 0x00099E5C
			public void Clear()
			{
				this.Activate.Clear();
				this.ActivateRemoved.Clear();
				this.Position.Clear();
				this.Set.Clear();
				this.Oppo.Clear();
				if (this.Dimension_Shifter > 0)
				{
					this.Dimension_Shifter--;
				}
				if (this.Summon > 0)
				{
					this.Summon--;
				}
			}

			// Token: 0x0600197F RID: 6527 RVA: 0x0009BCCE File Offset: 0x00099ECE
			public void AddActivateOppo(int id)
			{
				this.Oppo.Add(id);
			}

			// Token: 0x06001980 RID: 6528 RVA: 0x0009BCDC File Offset: 0x00099EDC
			public bool CheckActivateOppo(int id)
			{
				return !this.Oppo.Contains(id);
			}

			// Token: 0x06001981 RID: 6529 RVA: 0x0009BCED File Offset: 0x00099EED
			public void AddSummon()
			{
				this.Summon = 1;
			}

			// Token: 0x06001982 RID: 6530 RVA: 0x0009BCF6 File Offset: 0x00099EF6
			public void AddCard(int id)
			{
				if (id == 91800273)
				{
					this.Dimension_Shifter = 2;
					return;
				}
				this.Activate.Add(id);
			}

			// Token: 0x06001983 RID: 6531 RVA: 0x0009BD14 File Offset: 0x00099F14
			public void AddSet(int id)
			{
				this.Set.Add(id);
			}

			// Token: 0x06001984 RID: 6532 RVA: 0x0009BD22 File Offset: 0x00099F22
			public bool CheckSet(int id)
			{
				return !this.Set.Contains(id);
			}

			// Token: 0x06001985 RID: 6533 RVA: 0x0009BD33 File Offset: 0x00099F33
			public void AddCardRemoved(int id)
			{
				this.ActivateRemoved.Add(id);
			}

			// Token: 0x06001986 RID: 6534 RVA: 0x0009BD41 File Offset: 0x00099F41
			public void AddPosition(int id)
			{
				this.Position.Add(id);
			}

			// Token: 0x06001987 RID: 6535 RVA: 0x0009BD4F File Offset: 0x00099F4F
			public void AddPhase()
			{
				this.Phase++;
			}

			// Token: 0x06001988 RID: 6536 RVA: 0x0009BD5F File Offset: 0x00099F5F
			public bool CheckCard(int id)
			{
				if (id == 91800273)
				{
					return this.Dimension_Shifter == 0;
				}
				return !this.Activate.Contains(id);
			}

			// Token: 0x06001989 RID: 6537 RVA: 0x0009BD82 File Offset: 0x00099F82
			public bool CheckCardRemoved(int id)
			{
				return !this.ActivateRemoved.Contains(id);
			}

			// Token: 0x0600198A RID: 6538 RVA: 0x0009BD93 File Offset: 0x00099F93
			public bool CheckPosition(int id)
			{
				return !this.Position.Contains(id);
			}

			// Token: 0x0600198B RID: 6539 RVA: 0x0009BDA4 File Offset: 0x00099FA4
			public int CheckPhase()
			{
				return this.Phase;
			}

			// Token: 0x0600198C RID: 6540 RVA: 0x0009BDAC File Offset: 0x00099FAC
			public bool CheckSummon()
			{
				return this.Summon == 0;
			}

			// Token: 0x04001CC5 RID: 7365
			public int Dimension_Shifter;

			// Token: 0x04001CC6 RID: 7366
			public int Summon;

			// Token: 0x04001CC7 RID: 7367
			public int Phase;

			// Token: 0x04001CC8 RID: 7368
			public List<int> Activate = new List<int>();

			// Token: 0x04001CC9 RID: 7369
			public List<int> ActivateRemoved = new List<int>();

			// Token: 0x04001CCA RID: 7370
			public List<int> Position = new List<int>();

			// Token: 0x04001CCB RID: 7371
			public List<int> Set = new List<int>();

			// Token: 0x04001CCC RID: 7372
			public List<int> Oppo = new List<int>();
		}

		// Token: 0x0200037E RID: 894
		private struct ZoneData
		{
			// Token: 0x04001CCD RID: 7373
			public int Zone;

			// Token: 0x04001CCE RID: 7374
			public ClientCard[] CheckZone;
		}
	}
}
