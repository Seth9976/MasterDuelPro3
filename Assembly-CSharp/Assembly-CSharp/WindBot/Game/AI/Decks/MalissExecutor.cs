using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000356 RID: 854
	[Deck("Maliss", "AI_Maliss", "Normal")]
	public class MalissExecutor : DefaultExecutor
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x0008A5B4 File Offset: 0x000887B4
		public MalissExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, 20726052, new Func<bool>(this.SpellSetCheck));
			base.AddExecutor(ExecutorType.SpellSet, 94722358, new Func<bool>(this.SpellSetCheck));
			base.AddExecutor(ExecutorType.Activate, 86066372, new Func<bool>(this.Accesscode_OnSummon_AtkUp));
			base.AddExecutor(ExecutorType.Activate, 86066372, new Func<bool>(this.Accesscode_Destroy_Ignition));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 4280258, new Func<bool>(this.DontSelfNG));
			base.AddExecutor(ExecutorType.Activate, 40366667, new Func<bool>(this.DontSelfNG));
			base.AddExecutor(ExecutorType.Activate, 39138610, new Func<bool>(this.Allied_NegateBanish));
			base.AddExecutor(ExecutorType.Activate, 5043010, new Func<bool>(this.FirewallBounce_OnOppSummon));
			base.AddExecutor(ExecutorType.Activate, 94722358, new Func<bool>(this.MTP07_OppTurn_RemoveEnemyOnly));
			base.AddExecutor(ExecutorType.Activate, 21848500, new Func<bool>(this.HC_Quick_ReturnBanished_AndBanishField));
			base.AddExecutor(ExecutorType.Activate, 20726052, new Func<bool>(this.GWC06_OppTurn_ReviveWB_HC));
			base.AddExecutor(ExecutorType.Activate, 29301450, new Func<bool>(this.ActLittleKnight));
			base.AddExecutor(ExecutorType.Activate, 68059897, new Func<bool>(this.RR_SS_FromBanished));
			base.AddExecutor(ExecutorType.Activate, 21848500, new Func<bool>(this.HC_OnBanished_SpecialSummon));
			base.AddExecutor(ExecutorType.Activate, 93453053, new Func<bool>(this.Mirror_Banish));
			base.AddExecutor(ExecutorType.Summon, 32061192, new Func<bool>(this.Step1_Dormouse_NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 69272449, new Func<bool>(this.Step1_WhiteRabbit_NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 96676583, new Func<bool>(this.TwoCC_NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 96676583, new Func<bool>(this.Emergency_NormalCat));
			base.AddExecutor(ExecutorType.Summon, 20938824, new Func<bool>(this.NSMH));
			base.AddExecutor(ExecutorType.Summon, 30118811, new Func<bool>(this.NSBackup_L));
			base.AddExecutor(ExecutorType.Summon, 30118811, new Func<bool>(this.NSBackup));
			base.AddExecutor(ExecutorType.Activate, 32061192, new Func<bool>(this.Dormouse_SS_FromBanished));
			base.AddExecutor(ExecutorType.Activate, 32061192, new Func<bool>(this.Dormouse_ForMH));
			base.AddExecutor(ExecutorType.Activate, 32061192, new Func<bool>(this.Dormouse_Banish_Anytime));
			base.AddExecutor(ExecutorType.Activate, 69272449, new Func<bool>(this.Step1_WhiteRabbit_SS_FromBanished));
			base.AddExecutor(ExecutorType.Activate, 69272449, new Func<bool>(this.Step1_WhiteRabbit_SetTrapOnSummon));
			base.AddExecutor(ExecutorType.Activate, 20938824, new Func<bool>(this.Step1_MH_FromHand));
			base.AddExecutor(ExecutorType.Activate, 20938824, new Func<bool>(this.returnFromBanish));
			base.AddExecutor(ExecutorType.SpSummon, 24842059, new Func<bool>(this.LinguribohMHLine));
			base.AddExecutor(ExecutorType.SpSummon, 30342076, new Func<bool>(this.Step1_SSLinkDecoder));
			base.AddExecutor(ExecutorType.SpSummon, 68059897, new Func<bool>(this.Step2N_LinkSummon_RedRansom));
			base.AddExecutor(ExecutorType.SpSummon, 68059897, new Func<bool>(this.Step2_LinkSummon_RedRansom));
			base.AddExecutor(ExecutorType.Activate, 68059897, new Func<bool>(this.Step2_RedRansom_Search));
			base.AddExecutor(ExecutorType.Activate, 96676583, new Func<bool>(this.AnyDraw));
			base.AddExecutor(ExecutorType.SpSummon, 4280258, new Func<bool>(this.Link_Apo));
			base.AddExecutor(ExecutorType.SpSummon, 52698008, new Func<bool>(this.Step2N_RRtoWicckid));
			base.AddExecutor(ExecutorType.SpSummon, 30342076, new Func<bool>(this.Step_SummonLinkDecoderToWicckid));
			base.AddExecutor(ExecutorType.Activate, 52698008, new Func<bool>(this.Wicckid_SearchTuner));
			base.AddExecutor(ExecutorType.SpSummon, 59859086, new Func<bool>(this.Step_SplashToWB));
			base.AddExecutor(ExecutorType.Activate, 59859086, new Func<bool>(this.Step2N_SplashMage_ReviveP));
			base.AddExecutor(ExecutorType.Activate, 30118811, new Func<bool>(this.Flow3_BackupIgnister_AfterMakeIt3));
			base.AddExecutor(ExecutorType.Activate, 30118811, new Func<bool>(this.OneBody_Backup_SearchWizard));
			base.AddExecutor(ExecutorType.SpSummon, 95454996, new Func<bool>(this.Step2N_LinkSummon_WB));
			base.AddExecutor(ExecutorType.Activate, 95454996, new Func<bool>(this.WB_OnSummon_BanishGY));
			base.AddExecutor(ExecutorType.Activate, 20726052, new Func<bool>(this.GWC06_MyTurn_Extend));
			base.AddExecutor(ExecutorType.Activate, 95454996, new Func<bool>(this.WB_SetMalissTrap));
			base.AddExecutor(ExecutorType.Activate, 3723262, new Func<bool>(this.Step2_Fallback_Wizard_AfterSplashNegated));
			base.AddExecutor(ExecutorType.Activate, 30118811, new Func<bool>(this.Step2_Fallback_Backup_AfterSplashNegated));
			base.AddExecutor(ExecutorType.Activate, 3723262, new Func<bool>(this.Flow3_WizardIgnister_AfterMakeIt3));
			base.AddExecutor(ExecutorType.Activate, 95454996, new Func<bool>(this.WB_OnBanished_SelfSS));
			base.AddExecutor(ExecutorType.Activate, 20938824, new Func<bool>(this.ssFromHandMH));
			base.AddExecutor(ExecutorType.SpSummon, 21848500, new Func<bool>(this.Step_LinkSummon_HeartsCrypter));
			base.AddExecutor(ExecutorType.SpSummon, 39138610, new Func<bool>(this.Flow3_Link_Allied));
			base.AddExecutor(ExecutorType.Activate, 39138610, new Func<bool>(this.Allied_OnSummonTrigger));
			base.AddExecutor(ExecutorType.Activate, 94722358, new Func<bool>(this.MTP07_ForMH));
			base.AddExecutor(ExecutorType.SpSummon, 5043010, new Func<bool>(this.Flow3_Link_Firewall));
			base.AddExecutor(ExecutorType.SpSummon, 95454996, new Func<bool>(this.Step_WicckidPlusOneToWB));
			base.AddExecutor(ExecutorType.Activate, 96676583, new Func<bool>(this.ChessyCat_SS_FromBanished));
			base.AddExecutor(ExecutorType.SpSummon, 52698008, new Func<bool>(this.Step_RRtoWicckid));
			base.AddExecutor(ExecutorType.Activate, 68337209, new Func<bool>(this.Flow3_UnderGround_Available_SSAnyPawn));
			base.AddExecutor(ExecutorType.Activate, 30342076, new Func<bool>(this.LinkDecoder_ReviveFromGY));
			base.AddExecutor(ExecutorType.SpSummon, 46947713, new Func<bool>(this.SummonTranscode));
			base.AddExecutor(ExecutorType.Activate, 46947713, new Func<bool>(this.Transcode_ReviveLink3OrLower));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.Terra_GrabUnderground));
			base.AddExecutor(ExecutorType.Activate, 75500286, new Func<bool>(this.GoldSarc_StartPiece));
			base.AddExecutor(ExecutorType.Activate, 68337209, new Func<bool>(this.Underground_ActivateStarter));
			base.AddExecutor(ExecutorType.SpSummon, 59859086, new Func<bool>(this.Step_SplashToRR));
			base.AddExecutor(ExecutorType.Activate, 59859086, new Func<bool>(this.Step2_SplashMage_ReviveP));
			base.AddExecutor(ExecutorType.SpSummon, 98978921);
			base.AddExecutor(ExecutorType.Summon, 14558127, new Func<bool>(this.Emergency_NS));
			base.AddExecutor(ExecutorType.Summon, 23434538, new Func<bool>(this.Emergency_NS));
			base.AddExecutor(ExecutorType.SpSummon, 24842059, new Func<bool>(this.OneBody_Link1_Linguriboh));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.OneBody_Link1_Almiraj));
			base.AddExecutor(ExecutorType.Activate, 30118811, new Func<bool>(this.OneBody_Backup_SS));
			base.AddExecutor(ExecutorType.Activate, 30118811, new Func<bool>(this.OneBody_Backup_SearchWizard));
			base.AddExecutor(ExecutorType.Activate, 3723262, new Func<bool>(this.OneBody_Wizard_SS));
			base.AddExecutor(ExecutorType.SpSummon, 86066372, new Func<bool>(this.Flow3_Link_Accesscode));
			base.AddExecutor(ExecutorType.SpSummon, 24842059, new Func<bool>(this.T3Allow));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.T3Allow));
			base.AddExecutor(ExecutorType.SpSummon, 46947713, new Func<bool>(this.EmerTranscode));
			base.AddExecutor(ExecutorType.SpSummon, 39138610, new Func<bool>(this.Emer_Allied));
			base.AddExecutor(ExecutorType.SpSummon, 39138610, new Func<bool>(this.Emer_Allied2));
			base.AddExecutor(ExecutorType.SpSummon, 29301450, new Func<bool>(this.SummonLittleKnightFast));
			base.AddExecutor(ExecutorType.SpSummon, 29301450, new Func<bool>(this.SPEmer));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0008B0C9 File Offset: 0x000892C9
		private static bool IsEmzSeq(int seq)
		{
			return seq >= 5;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0008B0D2 File Offset: 0x000892D2
		private static int BitOfSeq(int seq)
		{
			return 1 << seq;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0008B0DA File Offset: 0x000892DA
		private static int LowestBit(int m)
		{
			return m & -m;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0008B0E0 File Offset: 0x000892E0
		public List<T> ShuffleList<T>(List<T> list)
		{
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(list.Count);
				int nextIndex = (index + Program.Rand.Next(list.Count - 1)) % list.Count;
				T tempCard = list[index];
				list[index] = list[nextIndex];
				list[nextIndex] = tempCard;
			}
			return list;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0008B150 File Offset: 0x00089350
		public List<ClientCard> ShuffleCardList(List<ClientCard> list)
		{
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				ClientCard temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
			return list;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0008B1A0 File Offset: 0x000893A0
		public int CheckRemainInDeck(int id)
		{
			for (int count = 1; count < 4; count++)
			{
				if (this.DeckCountTable[count].Contains(id))
				{
					return base.Bot.GetRemainingCount(id, count);
				}
			}
			return 0;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0008B1DC File Offset: 0x000893DC
		public bool MonsterRepos()
		{
			if (base.Card.Attack + 1 <= 1)
			{
				return !base.Card.IsDefense();
			}
			int bestAttack = 0;
			foreach (ClientCard clientCard in base.Bot.GetMonsters())
			{
				int attack = clientCard.Attack;
				if (attack >= bestAttack)
				{
					bestAttack = attack;
				}
			}
			bool enemyBetter = base.Util.IsAllEnemyBetterThanValue(bestAttack, true);
			return (base.Card.IsAttack() && enemyBetter) || (base.Card.IsDefense() && !enemyBetter);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0008B28C File Offset: 0x0008948C
		public bool CheckAtAdvantage()
		{
			if (this.GetProblematicEnemyMonster(0, false, false, (CardType)0) == null)
			{
				if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0008B2DC File Offset: 0x000894DC
		public bool CheckInDanger()
		{
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				int totalAtk = 0;
				foreach (ClientCard i in base.Enemy.GetMonsters())
				{
					if (i.IsAttack() && !i.Attacked)
					{
						totalAtk += i.Attack;
					}
				}
				if (totalAtk >= base.Bot.LifePoints)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0008B37C File Offset: 0x0008957C
		private int GetMyLinkedMMZMask()
		{
			int mask = 0;
			foreach (ClientCard i in base.Bot.GetMonsters())
			{
				if (i != null && i.IsFaceup() && i.HasType(CardType.Link))
				{
					mask |= i.GetLinkedZones();
				}
			}
			mask &= 31;
			return mask;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0008B3F8 File Offset: 0x000895F8
		private bool IsPawnId(int id)
		{
			return id == 32061192 || id == 69272449 || id == 96676583 || id == 20938824;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0008B41C File Offset: 0x0008961C
		private int GetQueenLinkedMMZMask()
		{
			int mask = 0;
			foreach (ClientCard i in base.Bot.GetMonsters())
			{
				if (i != null && i.IsFaceup() && (i.IsCode(68059897) || i.IsCode(95454996) || i.IsCode(21848500)))
				{
					mask |= i.GetLinkedZones();
				}
			}
			mask &= 31;
			return mask;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0008B4B0 File Offset: 0x000896B0
		private int LinkValOf(ClientCard c)
		{
			if (!c.HasType(CardType.Link))
			{
				return 1;
			}
			return Math.Max(1, c.LinkCount);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0008B4CD File Offset: 0x000896CD
		private bool IsOneVal(ClientCard c)
		{
			return !c.HasType(CardType.Link) || Math.Max(1, c.LinkCount) == 1;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0008B4ED File Offset: 0x000896ED
		private bool IsMaliss(ClientCard c)
		{
			return c.HasSetcode(447);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0008B4FC File Offset: 0x000896FC
		private int ScoreForBanishedMaliss(ClientCard c)
		{
			if (c.IsCode(93453053))
			{
				return 100;
			}
			if (c.IsCode(94722358))
			{
				return 95;
			}
			if (c.IsCode(20726052))
			{
				return 90;
			}
			if (c.IsCode(68337209))
			{
				return 85;
			}
			if (c.IsCode(20938824))
			{
				return 80;
			}
			if (c.IsCode(96676583))
			{
				return 75;
			}
			if (c.IsCode(69272449))
			{
				return 70;
			}
			if (c.IsCode(32061192))
			{
				return 65;
			}
			return 50;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0008B58C File Offset: 0x0008978C
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player != 0 || location != CardLocation.MonsterZone)
			{
				this.SelectSTPlace(base.Card, true, null);
				return base.OnSelectPlace(cardId, player, location, available);
			}
			int MAIN_MASK = 31;
			if (this.IsPawnId(cardId))
			{
				int queenChoices = this.GetQueenLinkedMMZMask() & available & MAIN_MASK;
				if (queenChoices != 0)
				{
					int pick = MalissExecutor.FirstBitFromOrder(queenChoices, new int[] { 4, 2, 8, 1, 16 });
					base.AI.SelectPlace(pick);
					return pick;
				}
			}
			if (cardId == 39138610)
			{
				ClientCard fw = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(5043010));
				int emzAvail = available & 96;
				if (fw != null && fw.IsFaceup())
				{
					bool firewallCenter = fw.Controller == 0 && fw.Location == CardLocation.MonsterZone && fw.Sequence == 2;
					int linkedChoices = fw.GetLinkedZones() & available;
					int linkedEmzChoices = linkedChoices & 96;
					if (linkedEmzChoices != 0)
					{
						int pick2;
						if (firewallCenter && (linkedEmzChoices & 32) != 0 && (linkedEmzChoices & 64) != 0)
						{
							int leftFree = 0;
							if ((available & 1) != 0)
							{
								leftFree++;
							}
							if ((available & 2) != 0)
							{
								leftFree++;
							}
							int rightFree = 0;
							if ((available & 8) != 0)
							{
								rightFree++;
							}
							if ((available & 16) != 0)
							{
								rightFree++;
							}
							if (leftFree > rightFree)
							{
								pick2 = 32;
							}
							else if (rightFree > leftFree)
							{
								pick2 = 64;
							}
							else
							{
								pick2 = MalissExecutor.FirstBitFromOrder(linkedEmzChoices, new int[] { 32, 64 });
							}
						}
						else
						{
							pick2 = MalissExecutor.FirstBitFromOrder(linkedEmzChoices, new int[] { 32, 64 });
						}
						base.AI.SelectPlace(pick2);
						return pick2;
					}
					if (emzAvail != 0)
					{
						int pick2;
						if (firewallCenter && (emzAvail & 32) != 0 && (emzAvail & 64) != 0)
						{
							int leftFree2 = 0;
							if ((available & 1) != 0)
							{
								leftFree2++;
							}
							if ((available & 2) != 0)
							{
								leftFree2++;
							}
							int rightFree2 = 0;
							if ((available & 8) != 0)
							{
								rightFree2++;
							}
							if ((available & 16) != 0)
							{
								rightFree2++;
							}
							if (leftFree2 > rightFree2)
							{
								pick2 = 32;
							}
							else if (rightFree2 > leftFree2)
							{
								pick2 = 64;
							}
							else
							{
								pick2 = MalissExecutor.FirstBitFromOrder(emzAvail, new int[] { 32, 64 });
							}
						}
						else
						{
							pick2 = MalissExecutor.FirstBitFromOrder(emzAvail, new int[] { 32, 64 });
						}
						base.AI.SelectPlace(pick2);
						return pick2;
					}
					if (linkedChoices != 0)
					{
						int pick2 = MalissExecutor.FirstBitFromOrder(linkedChoices, new int[] { 4, 2, 8, 1, 16 });
						base.AI.SelectPlace(pick2);
						return pick2;
					}
				}
				int emzOnly = available & 96;
				if (emzOnly != 0)
				{
					int pick3 = MalissExecutor.FirstBitFromOrder(emzOnly, new int[] { 32, 64 });
					base.AI.SelectPlace(pick3);
					return pick3;
				}
				return this.PreferSafeSummonZones(available);
			}
			else if (cardId == 52698008)
			{
				int picked = this.ChooseAndRememberWicckidEmz(available);
				if (picked != 0)
				{
					return picked;
				}
				return 0;
			}
			else if (cardId == 46947713)
			{
				int wanted = ((this._forceTranscodeBit != 0) ? this._forceTranscodeBit : this._wicckidEmzBit);
				if (wanted != 0 && (available & wanted) != 0)
				{
					return wanted;
				}
				int anyEmz = available & 96;
				if (anyEmz == 0)
				{
					return 0;
				}
				if ((anyEmz & 32) == 0)
				{
					return 64;
				}
				return 32;
			}
			else
			{
				if (cardId == 68059897 && this._rrSelfSSPlacing)
				{
					int prefer = 10;
					int wmask = this.GetLinkedMaskFor(this.GetWicckid());
					int choices = available & prefer & ~wmask;
					if (choices != 0)
					{
						int pick4 = MalissExecutor.FirstBitFromOrder(choices, new int[] { 2, 8 });
						base.AI.SelectPlace(pick4);
						this._rrSelfSSPlacing = false;
						return pick4;
					}
				}
				if (cardId == 30342076)
				{
					ClientCard trans = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(46947713));
					int tmask = this.GetLinkedMaskFor(trans) & 31;
					int safe = available & 31 & ~tmask;
					if (safe != 0)
					{
						int pick5 = MalissExecutor.FirstBitFromOrder(safe, new int[] { 4, 2, 8, 1, 16 });
						base.AI.SelectPlace(pick5);
						return pick5;
					}
				}
				if (cardId == 5043010)
				{
					ClientCard trans2 = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(46947713));
					int underTrans = 0;
					if (trans2 != null)
					{
						int num = this.GetLinkedMaskFor(trans2) & 31;
						if ((num & 2) != 0)
						{
							underTrans |= 2;
						}
						if ((num & 8) != 0)
						{
							underTrans |= 8;
						}
					}
					int choices2 = available & underTrans;
					if (choices2 != 0)
					{
						int pick6 = MalissExecutor.FirstBitFromOrder(choices2, new int[] { 2, 8 });
						base.AI.SelectPlace(pick6);
						return pick6;
					}
				}
				if (cardId == 46947713 || cardId == 86066372 || cardId == 39138610 || cardId == 95454996 || cardId == 21848500)
				{
					return this.PreferSafeSummonZones(available);
				}
				int linked = this.GetMyLinkedMMZMask() & available & 31;
				int unlinked = available & 31 & ~linked;
				int choose;
				if (this.avoidLinkedZones && unlinked != 0)
				{
					choose = MalissExecutor.LowestBit(unlinked);
				}
				else if (linked != 0)
				{
					choose = MalissExecutor.LowestBit(linked);
				}
				else
				{
					choose = MalissExecutor.LowestBit(available & 31);
				}
				base.AI.SelectPlace(choose);
				return choose;
			}
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0008BAAC File Offset: 0x00089CAC
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null)
			{
				if (base.Duel.Turn == 1 || base.Duel.Phase >= DuelPhase.Main2)
				{
					bool turnDefense = false;
					if (cardData.Attack <= cardData.Defense)
					{
						turnDefense = true;
					}
					if (turnDefense)
					{
						return CardPosition.FaceUpDefence;
					}
				}
				if (base.Duel.Player == 1 && (cardData.Defense >= cardData.Attack || base.Util.IsOneEnemyBetterThanValue(cardData.Attack, true)))
				{
					return CardPosition.FaceUpDefence;
				}
				int cardAttack = cardData.Attack;
				int bestBotAttack = Math.Max(base.Util.GetBestAttack(base.Bot), cardAttack);
				if (base.Util.IsAllEnemyBetterThanValue(bestBotAttack, true))
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0008BB68 File Offset: 0x00089D68
		public bool AshBlossomActivate()
		{
			return !this.CheckWhetherNegated(true, false, (CardType)0) && this.CheckLastChainShouldNegated() && (base.Duel.LastChainPlayer != 1 || !base.Util.GetLastChainCard().IsCode(23434538) || !this.CheckAtAdvantage() || base.Duel.Turn <= 1) && base.DefaultAshBlossomAndJoyousSpring();
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0008BBCD File Offset: 0x00089DCD
		public bool MaxxCActivate()
		{
			return !this.CheckWhetherNegated(true, false, (CardType)0) && base.Duel.LastChainPlayer != 0 && base.DefaultMaxxC();
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0008BBF0 File Offset: 0x00089DF0
		public bool InfiniteImpermanenceActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
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
								this.infiniteImpermanenceList.Add(j);
								break;
							}
						}
					}
					if (base.Card.Location == CardLocation.Hand)
					{
						this.SelectSTPlace(base.Card, true, null);
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
					ClientCard target = this.GetProblematicEnemyMonster(0, true, false, (CardType)0);
					base.Enemy.GetMonsters();
					base.AI.SelectCard(target);
					this.infiniteImpermanenceList.Add(this_seq);
					return true;
				}
			}
			if (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
			{
				return false;
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				for (int l = 0; l < 5; l++)
				{
					if (base.Bot.SpellZone[l] == base.Card)
					{
						this.infiniteImpermanenceList.Add(l);
						break;
					}
				}
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				this.SelectSTPlace(base.Card, true, null);
			}
			if (LastChainCard != null)
			{
				base.AI.SelectCard(LastChainCard);
			}
			else
			{
				List<ClientCard> monsters = base.Enemy.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters.Reverse();
				foreach (ClientCard card in monsters)
				{
					if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
					{
						base.AI.SelectCard(card);
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0008BF3C File Offset: 0x0008A13C
		public bool CrossoutDesignatorActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0) || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard() != null)
			{
				int code = base.Util.GetLastChainCard().Id;
				int alias = base.Util.GetLastChainCard().Alias;
				if (alias != 0 && alias - code < 10)
				{
					code = alias;
				}
				if (code == 0)
				{
					return false;
				}
				if (base.DefaultCheckWhetherCardIdIsNegated(code))
				{
					return false;
				}
				if (this.CheckRemainInDeck(code) > 0)
				{
					if (base.Card.Location != CardLocation.SpellZone)
					{
						this.SelectSTPlace(null, true, null);
					}
					base.AI.SelectAnnounceID(code);
					this.currentNegateCardList.AddRange(base.Enemy.MonsterZone.Where((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(code)));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0008C040 File Offset: 0x0008A240
		public bool CalledbytheGraveActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0) || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				if (base.Util.GetLastChainCard().IsMonster())
				{
					int code = base.Util.GetLastChainCard().GetOriginCode();
					if (code == 0)
					{
						return false;
					}
					if (base.DefaultCheckWhetherCardIdIsNegated(code))
					{
						return false;
					}
					if (base.Util.GetLastChainCard().IsCode(23434538) && this.CheckAtAdvantage() && base.Duel.Turn > 1)
					{
						return false;
					}
					ClientCard graveTarget = base.Enemy.Graveyard.GetFirstMatchingCard((ClientCard card) => card.IsMonster() && card.GetOriginCode() == code);
					if (graveTarget != null)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						base.AI.SelectCard(graveTarget);
						this.currentDestroyCardList.Add(graveTarget);
						return true;
					}
				}
				foreach (ClientCard graveCard in base.Enemy.Graveyard)
				{
					if (base.Duel.ChainTargets.Contains(graveCard) && graveCard.IsMonster())
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						int id = graveCard.Id;
						base.AI.SelectCard(graveCard);
						this.currentDestroyCardList.Add(graveCard);
						return true;
					}
				}
				if (!base.Duel.ChainTargets.Contains(base.Card))
				{
					goto IL_0224;
				}
				List<ClientCard> enemyMonsters = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsMonster()).ToList<ClientCard>();
				if (enemyMonsters.Count > 0)
				{
					enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyMonsters.Reverse();
					int code3 = enemyMonsters[0].Id;
					base.AI.SelectCard(code3);
					this.currentDestroyCardList.Add(enemyMonsters[0]);
					return true;
				}
			}
			IL_0224:
			if (base.Duel.LastChainPlayer == 1)
			{
				return false;
			}
			List<ClientCard> targets = this.GetDangerousCardinEnemyGrave(true);
			if (targets.Count > 0)
			{
				int code2 = targets[0].Id;
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(code2);
				this.currentDestroyCardList.Add(targets[0]);
				return true;
			}
			return false;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0008C2EC File Offset: 0x0008A4EC
		public bool SpellSetCheck()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (new List<int>().Contains(base.Card.Id) && base.Bot.HasInSpellZone(base.Card.Id, false, false))
			{
				return false;
			}
			if (!base.Card.IsTrap() && !base.Card.HasType(CardType.QuickPlay))
			{
				return false;
			}
			List<int> avoid_list = new List<int>();
			int setFornfiniteImpermanence = 0;
			for (int i = 0; i < 5; i++)
			{
				if (base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFaceup() && base.Bot.SpellZone[4 - i] == null)
				{
					avoid_list.Add(4 - i);
					setFornfiniteImpermanence += (int)Math.Pow(2.0, (double)(4 - i));
				}
			}
			if (!base.Bot.HasInHand(10045474))
			{
				this.SelectSTPlace(null, false, null);
				if (base.Card.IsCode(94722358))
				{
					this.mtp07SetThisTurn = true;
				}
				if (base.Card.IsCode(20726052))
				{
					this.gwc06SetThisTurn = true;
				}
				return true;
			}
			if (base.Card.IsCode(10045474))
			{
				base.AI.SelectPlace(setFornfiniteImpermanence);
				return true;
			}
			this.SelectSTPlace(base.Card, false, avoid_list);
			return true;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0008C460 File Offset: 0x0008A660
		public List<ClientCard> GetDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			List<ClientCard> list = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && (card.HasSetcode(283) || card.HasSetcode(219) || card.HasSetcode(413) || card.HasSetcode(6) || card.HasSetcode(277))).ToList<ClientCard>();
			List<int> dangerMonsterIdList = new List<int> { 99937011, 63542003, 9411399, 28954097, 30680659, 32731036 };
			list.AddRange(base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => dangerMonsterIdList.Contains(card.Id)));
			return list;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0008C50C File Offset: 0x0008A70C
		public bool CheckWhetherNegated(bool disablecheck = true, bool toFieldCheck = false, CardType type = (CardType)0)
		{
			bool isMonster = type == (CardType)0 && base.Card.IsMonster();
			isMonster |= (type & CardType.Monster) > (CardType)0;
			bool flag = (type == (CardType)0 && (base.Card.IsSpell() || base.Card.IsTrap())) | ((type & CardType.Spell) != (CardType)0 || (type & CardType.Trap) > (CardType)0);
			bool isCounter = (type & CardType.Counter) > (CardType)0;
			if (flag && toFieldCheck && this.CheckSpellWillBeNegate(isCounter, null))
			{
				return true;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return true;
			}
			if (isMonster && (toFieldCheck || base.Card.Location == CardLocation.MonsterZone))
			{
				if (((toFieldCheck && (type & CardType.Link) != (CardType)0) || base.Card.IsDefense()) && (base.Enemy.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card)) || base.Bot.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card))))
				{
					return true;
				}
				if (base.Enemy.HasInSpellZone(82732705, true, false))
				{
					return true;
				}
			}
			return disablecheck && ((base.Card.Location == CardLocation.MonsterZone || base.Card.Location == CardLocation.SpellZone) && base.Card.IsDisabled()) && base.Card.IsFaceup();
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00037BCF File Offset: 0x00035DCF
		public bool CheckNumber41(ClientCard card)
		{
			return card != null && card.IsFaceup() && card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled();
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0008C64C File Offset: 0x0008A84C
		public void SelectSTPlace(ClientCard card = null, bool avoidImpermanence = false, List<int> avoidList = null)
		{
			if (card == null)
			{
				card = base.Card;
			}
			List<int> list = new List<int>();
			for (int seq = 0; seq < 5; seq++)
			{
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoidImpermanence || !this.infiniteImpermanenceList.Contains(seq)) && (avoidList == null || !avoidList.Contains(seq)))
				{
					list.Add(seq);
				}
			}
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(list.Count);
				int nextIndex = (index + Program.Rand.Next(list.Count - 1)) % list.Count;
				int tempInt = list[index];
				list[index] = list[nextIndex];
				list[nextIndex] = tempInt;
			}
			if (avoidImpermanence)
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && !c.IsDisabled()))
				{
					foreach (int seq2 in list)
					{
						ClientCard enemySpell = base.Enemy.SpellZone[4 - seq2];
						if (enemySpell == null || !enemySpell.IsFacedown())
						{
							int zone = (int)Math.Pow(2.0, (double)seq2);
							base.AI.SelectPlace(zone);
							return;
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int seq3 = enumerator.Current;
					int zone2 = (int)Math.Pow(2.0, (double)seq3);
					base.AI.SelectPlace(zone2);
					return;
				}
			}
			base.AI.SelectPlace(0);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0008C838 File Offset: 0x0008AA38
		public bool CheckSpellWillBeNegate(bool isCounter = false, ClientCard target = null)
		{
			if (target == null)
			{
				target = base.Card;
			}
			if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(99916754, true, false, false) && !isCounter)
			{
				return true;
			}
			if (target.IsSpell())
			{
				if (base.Enemy.HasInMonstersZone(33198837, true, false, false))
				{
					return true;
				}
				if (base.Enemy.HasInSpellZone(61740673, true, false) || base.Bot.HasInSpellZone(61740673, true, false))
				{
					return true;
				}
				if (base.Enemy.HasInMonstersZone(37267041, true, false, false) || base.Bot.HasInMonstersZone(37267041, true, false, false))
				{
					return true;
				}
			}
			if (target.IsTrap() && (base.Enemy.HasInSpellZone(51452091, true, false) || base.Bot.HasInSpellZone(51452091, true, false)))
			{
				return true;
			}
			if (target.Location == CardLocation.SpellZone && (target.IsSpell() || target.IsTrap()))
			{
				int selfSeq = -1;
				for (int i = 0; i < 5; i++)
				{
					if (base.Bot.SpellZone[i] == base.Card)
					{
						selfSeq = i;
					}
				}
				if (this.infiniteImpermanenceList.Contains(selfSeq))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0008C974 File Offset: 0x0008AB74
		public bool CheckLastChainShouldNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			return lastcard != null && lastcard.Controller == 1 && (!lastcard.IsMonster() || !lastcard.HasSetcode(74) || base.Duel.Phase != DuelPhase.Standby) && !this.notToNegateIdList.Contains(lastcard.Id) && !base.DefaultCheckWhetherCardIsNegated(lastcard) && (base.Duel.Turn != 1 || !lastcard.IsCode(23434538));
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0008C9FC File Offset: 0x0008ABFC
		public ClientCard GetProblematicEnemyMonster(int attack = 0, bool canBeTarget = false, bool ignoreCurrentDestroy = false, CardType selfType = (CardType)0)
		{
			ClientCard floodagateCard = (from c in base.Enemy.GetMonsters()
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (floodagateCard != null)
			{
				return floodagateCard;
			}
			ClientCard dangerCard = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.IsMonsterDangerous() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (dangerCard != null)
			{
				return dangerCard;
			}
			ClientCard invincibleCard = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.IsMonsterInvincible() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (invincibleCard != null)
			{
				return invincibleCard;
			}
			ClientCard equippedCard = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.EquipCards.Count > 0 && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (equippedCard != null)
			{
				return equippedCard;
			}
			ClientCard enemyExtraMonster = (from c in base.Enemy.MonsterZone
				where c != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && (c.HasType((CardType)8396992) || (c.HasType(CardType.Link) && c.LinkCount >= 2)) && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (enemyExtraMonster != null)
			{
				return enemyExtraMonster;
			}
			if (attack >= 0)
			{
				if (attack == 0)
				{
					attack = base.Util.GetBestAttack(base.Bot);
				}
				ClientCard betterCard = (from card in base.Enemy.MonsterZone
					where card != null && card.GetDefensePower() >= attack && card.GetDefensePower() > 0 && card.IsAttack() && this.CheckCanBeTargeted(card, canBeTarget, selfType) && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
					orderby card.Attack descending
					select card).FirstOrDefault<ClientCard>();
				if (betterCard != null)
				{
					return betterCard;
				}
			}
			return null;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0008CC28 File Offset: 0x0008AE28
		public bool CheckCanBeTargeted(ClientCard card, bool canBeTarget, CardType selfType)
		{
			if (card == null)
			{
				return true;
			}
			if (canBeTarget)
			{
				if (card.IsShouldNotBeTarget())
				{
					return false;
				}
				if ((selfType & CardType.Monster) > (CardType)0 && card.IsShouldNotBeMonsterTarget())
				{
					return false;
				}
				if ((selfType & CardType.Spell) > (CardType)0 && card.IsShouldNotBeSpellTrapTarget())
				{
					return false;
				}
				if ((selfType & CardType.Trap) > (CardType)0 && card.IsShouldNotBeSpellTrapTarget() && !card.IsDisabled())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0008CC80 File Offset: 0x0008AE80
		public bool CheckShouldNotIgnore(ClientCard cards, bool ignore = false)
		{
			return !ignore || (!this.currentDestroyCardList.Contains(cards) && !this.currentNegateCardList.Contains(cards));
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0008CCA8 File Offset: 0x0008AEA8
		public List<ClientCard> GetProblematicEnemyCardList(bool canBeTarget = false, bool ignoreSpells = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			List<ClientCard> floodagateList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !this.currentDestroyCardList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (floodagateList.Count > 0)
			{
				resultList.AddRange(floodagateList);
			}
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)).ToList<ClientCard>();
			if (problemEnemySpellList.Count > 0)
			{
				resultList.AddRange(this.ShuffleList<ClientCard>(problemEnemySpellList));
			}
			List<ClientCard> dangerList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsMonsterDangerous() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (dangerList.Count > 0 && (base.Duel.Player == 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
			{
				resultList.AddRange(dangerList);
			}
			List<ClientCard> invincibleList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsMonsterInvincible() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (invincibleList.Count > 0)
			{
				resultList.AddRange(invincibleList);
			}
			List<ClientCard> enemyMonsters = (from c in base.Enemy.GetMonsters()
				where !this.currentDestroyCardList.Contains(c)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (enemyMonsters.Count > 0)
			{
				foreach (ClientCard target in enemyMonsters)
				{
					if ((target.HasType((CardType)8396992) || (target.HasType(CardType.Link) && target.LinkCount >= 2)) && !resultList.Contains(target) && this.CheckCanBeTargeted(target, canBeTarget, selfType))
					{
						resultList.Add(target);
					}
				}
			}
			List<ClientCard> spells = (from c in base.Enemy.GetSpells()
				where c.IsFaceup() && !this.currentDestroyCardList.Contains(c) && c.HasType((CardType)17694720) && this.CheckCanBeTargeted(c, canBeTarget, selfType) && !this.notToDestroySpellTrap.Contains(c.Id)
				select c).ToList<ClientCard>();
			if (spells.Count > 0 && !ignoreSpells)
			{
				resultList.AddRange(this.ShuffleList<ClientCard>(spells));
			}
			return resultList;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0008CF8C File Offset: 0x0008B18C
		public List<ClientCard> GetNormalEnemyTargetList(bool canBeTarget = true, bool ignoreCurrentDestroy = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> targetList = this.GetProblematicEnemyCardList(canBeTarget, false, selfType);
			List<ClientCard> enemyMonster = (from card in base.Enemy.GetMonsters()
				where card.IsFaceup() && !targetList.Contains(card) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
				select card).ToList<ClientCard>();
			enemyMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			enemyMonster.Reverse();
			targetList.AddRange(enemyMonster);
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && this.enemyPlaceThisTurn.Contains(card)
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && !this.enemyPlaceThisTurn.Contains(card)
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetMonsters()
				where card.IsFacedown() && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
				select card).ToList<ClientCard>()));
			return targetList;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0008D0A0 File Offset: 0x0008B2A0
		public List<ClientCard> GetMonsterListForTargetNegate(bool canBeTarget = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return resultList;
			}
			ClientCard target = base.Enemy.MonsterZone.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterShouldBeDisabledBeforeItUseEffect() && card.IsFaceup() && !card.IsShouldNotBeTarget() && this.CheckCanBeTargeted(card, canBeTarget, selfType) && !this.currentNegateCardList.Contains(card));
			if (target != null)
			{
				resultList.Add(target);
			}
			foreach (ClientCard chainingCard in base.Duel.CurrentChain)
			{
				if (chainingCard.Location == CardLocation.MonsterZone && chainingCard.Controller == 1 && !chainingCard.IsDisabled() && this.CheckCanBeTargeted(chainingCard, canBeTarget, selfType) && !this.currentNegateCardList.Contains(chainingCard))
				{
					resultList.Add(chainingCard);
				}
			}
			return resultList;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0008D18C File Offset: 0x0008B38C
		public ClientCard GetBestEnemyMonster(bool onlyFaceup = false, bool canBeTarget = false)
		{
			ClientCard card = this.GetProblematicEnemyMonster(0, canBeTarget, false, (CardType)0);
			if (card != null)
			{
				return card;
			}
			card = base.Enemy.MonsterZone.GetHighestAttackMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			if (monsters.Count > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(monsters)[0];
			}
			return null;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0008D1E8 File Offset: 0x0008B3E8
		public ClientCard GetBestEnemySpell(bool onlyFaceup = false, bool canBeTarget = false)
		{
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (problemEnemySpellList.Count > 0)
			{
				return this.ShuffleCardList(problemEnemySpellList)[0];
			}
			List<ClientCard> spells = (from card in base.Enemy.GetSpells()
				where !card.IsFaceup() || !card.IsCode(15693423)
				select card).ToList<ClientCard>();
			List<ClientCard> faceUpList = spells.Where((ClientCard ecard) => ecard.IsFaceup() && (ecard.HasType(CardType.Continuous) || ecard.HasType(CardType.Field) || ecard.HasType(CardType.Pendulum))).ToList<ClientCard>();
			if (faceUpList.Count > 0)
			{
				return this.ShuffleCardList(faceUpList)[0];
			}
			if (spells.Count > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(spells)[0];
			}
			return null;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0008D2D0 File Offset: 0x0008B4D0
		public ClientCard GetBestEnemyCard(bool onlyFaceup = false, bool canBeTarget = false, bool checkGrave = false)
		{
			ClientCard card = this.GetBestEnemyMonster(onlyFaceup, canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.GetBestEnemySpell(onlyFaceup, canBeTarget);
			if (card != null)
			{
				return card;
			}
			if (!checkGrave || base.Enemy.Graveyard.Count <= 0)
			{
				return null;
			}
			List<ClientCard> graveMonsterList = base.Enemy.Graveyard.GetMatchingCards((ClientCard c) => c.IsMonster()).ToList<ClientCard>();
			if (graveMonsterList.Count > 0)
			{
				graveMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				graveMonsterList.Reverse();
				return graveMonsterList[0];
			}
			return this.ShuffleCardList(base.Enemy.Graveyard.ToList<ClientCard>())[0];
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0008D38E File Offset: 0x0008B58E
		private int LinkVal(ClientCard c)
		{
			if (c == null || !c.HasType(CardType.Link))
			{
				return 1;
			}
			return Math.Max(1, c.LinkCount);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0008D3B0 File Offset: 0x0008B5B0
		private bool IsInEMZ(ClientCard c)
		{
			ClientCard[] mz = base.Bot.MonsterZone;
			return (mz.Length > 5 && mz[5] == c) || (mz.Length > 6 && mz[6] == c);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0008D3E8 File Offset: 0x0008B5E8
		private bool HasFreeEMZ()
		{
			ClientCard[] mz = base.Bot.MonsterZone;
			bool flag = mz.Length > 5 && mz[5] == null;
			bool slot6Free = mz.Length > 6 && mz[6] == null;
			return flag || slot6Free;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0008D424 File Offset: 0x0008B624
		public override void OnChainSolved(int chainIndex)
		{
			ClientCard currentCard = base.Duel.GetCurrentSolvingChainCard();
			base.Duel.GetCurrentSolvingChainCard();
			base.Duel.IsCurrentSolvingChainNegated();
			if (currentCard != null && !base.Duel.IsCurrentSolvingChainNegated() && currentCard.Controller == 1)
			{
				if (currentCard.IsCode(34267821))
				{
					this.enemyActivateLancea = true;
				}
				if (currentCard.IsCode(23434538))
				{
					this.enemyActivateMaxxC = true;
				}
				if (currentCard.IsCode(42141493))
				{
					this.enemyActivateFuwalos = true;
				}
				if (currentCard.IsCode(94145021))
				{
					this.enemyActivateLockBird = true;
				}
				if (currentCard.IsCode(10045474))
				{
					for (int i = 0; i < 5; i++)
					{
						if (base.Enemy.SpellZone[i] == currentCard)
						{
							this.infiniteImpermanenceList.Add(4 - i);
							break;
						}
					}
				}
				ClientCard last = base.Duel.GetCurrentSolvingChainCard();
				if (last != null)
				{
					if (last.IsSpell() && (last.HasType(CardType.Field) || last.HasType(CardType.Continuous) || last.HasType(CardType.Equip)))
					{
						this._oppJustActivatedPersistentSpell = true;
					}
					this._prefWindowTTL = Math.Max(this._prefWindowTTL, 2);
				}
			}
			if (currentCard != null && currentCard.Controller == 0 && currentCard.IsCode(59859086) && base.Duel.IsCurrentSolvingChainNegated())
			{
				this.splashNegatedThisTurn = true;
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0008D584 File Offset: 0x0008B784
		public override void OnChainEnd()
		{
			this.escapeTargetList.Clear();
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			this._oppJustActivatedPersistentSpell = false;
			int curMon = base.Enemy.GetMonsterCount();
			if (curMon > this._enemyMonsterCountSnap)
			{
				this._oppJustSummoned = true;
			}
			this._enemyMonsterCountSnap = curMon;
			int curFD = base.Enemy.SpellZone.Count((ClientCard c) => c != null && c.IsFacedown());
			if (curFD > this._enemyFacedownSTSnap)
			{
				this._oppJustSet = true;
			}
			this._enemyFacedownSTSnap = curFD;
			for (int idx = this.enemyPlaceThisTurn.Count - 1; idx >= 0; idx--)
			{
				ClientCard checkTarget = this.enemyPlaceThisTurn[idx];
				if (checkTarget == null || (checkTarget.Location != CardLocation.SpellZone && checkTarget.Location != CardLocation.MonsterZone))
				{
					this.enemyPlaceThisTurn.RemoveAt(idx);
				}
			}
			base.OnChainEnd();
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0008D678 File Offset: 0x0008B878
		public override void OnNewTurn()
		{
			if (base.Duel.Player == 0)
			{
				this.myTurnCount++;
			}
			this.enemyActivateLancea = false;
			this.enemyActivateFuwalos = false;
			this.enemyActivateMaxxC = false;
			this.enemyActivateLockBird = false;
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			if (this.dimensionShifterCount > 0)
			{
				this.dimensionShifterCount--;
			}
			this.infiniteImpermanenceList.Clear();
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.sendToGYThisTurn.Clear();
			this.activatedCardIdList.Clear();
			this.enemyPlaceThisTurn.Clear();
			this.summonThisTurn.Clear();
			this.usedNormalSummon = false;
			this.ssChessyCat = false;
			this.ssDormouse = false;
			this.ssMarchHare = false;
			this.ssWhiteRabbit = false;
			this.ActiveMarchHare = false;
			this.ActiveUnderground = false;
			this.step1Done = false;
			this.step2Done = false;
			this.lastRevivedIdBySplash = 0;
			this.mtp07SetThisTurn = false;
			this.gwc06SetThisTurn = false;
			this.splashNegatedThisTurn = false;
			this.ssRRThisTurn = false;
			this.ssWBThisTurn = false;
			this.ssHCThisTurn = false;
			this._didSplashToRR = (this._didRRtoWicckid = (this._didSummonToWicckidArrow = (this._didWBFromWicckid = false)));
			this._finishPlanDecided = false;
			this._preferWicckidArrows = false;
			this._rrSelfSSPlacing = false;
			this._forceTranscodeBit = 0;
			this._oppJustActivatedPersistentSpell = false;
			this._oppJustSummoned = false;
			this._oppJustSet = false;
			this._enemyMonsterCountSnap = base.Enemy.GetMonsterCount();
			this._enemyFacedownSTSnap = base.Enemy.SpellZone.Count((ClientCard c) => c != null && c.IsFacedown());
			this._prefWindowTTL = 0;
			this.fullBoard1 = false;
			this.Allied_End = false;
			this.nsplan = false;
			this.nsBackupplan = false;
			this.NSDorMouse = false;
			this.nsLanceaplan = false;
			base.OnNewTurn();
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0008D868 File Offset: 0x0008BA68
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == base.Util.GetStringId(95454996, 0))
			{
				return base.Bot.Graveyard.Count > 0 || base.Enemy.Graveyard.Count > 0;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000675A8 File Offset: 0x000657A8
		private bool DontSelfNG()
		{
			return base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0008D8BC File Offset: 0x0008BABC
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			ClientCard solving = base.Duel.GetCurrentSolvingChainCard();
			if (cards != null && cards.Count > 0 && solving != null)
			{
				if (solving.IsCode(68059897))
				{
					List<ClientCard> searchPool = cards.Where((ClientCard c) => c != null && (c.IsCode(68337209) || c.IsCode(93453053))).ToList<ClientCard>();
					if (searchPool.Count > 0)
					{
						bool flag = this.ShouldSearchUnderground();
						int chooseId = 0;
						if (flag)
						{
							if (searchPool.Any((ClientCard c) => c.IsCode(68337209)))
							{
								chooseId = 68337209;
								goto IL_00E6;
							}
						}
						if (searchPool.Any((ClientCard c) => c.IsCode(93453053)))
						{
							chooseId = 93453053;
						}
						IL_00E6:
						if (chooseId != 0)
						{
							ClientCard pick = searchPool.First((ClientCard c) => c.IsCode(chooseId));
							return new List<ClientCard> { pick };
						}
					}
				}
				if (hint == 510 && solving.IsCode(95454996))
				{
					ClientCard pick2 = cards.FirstOrDefault((ClientCard c) => c.Id == 20726052 && c.Location == CardLocation.Deck);
					if (pick2 == null)
					{
						pick2 = cards.FirstOrDefault((ClientCard c) => c.Id == 20726052);
					}
					if (pick2 == null)
					{
						pick2 = cards.FirstOrDefault((ClientCard c) => c.Id == 94722358);
					}
					if (pick2 != null)
					{
						if (pick2.Id == 20726052)
						{
							this.gwc06SetThisTurn = true;
						}
						else if (pick2.Id == 94722358)
						{
							this.mtp07SetThisTurn = true;
						}
						return new List<ClientCard> { pick2 };
					}
				}
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0008DABE File Offset: 0x0008BCBE
		private int GetMMZCount()
		{
			return base.Bot.MonsterZone.Take(5).Count((ClientCard c) => c != null);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0008DAF5 File Offset: 0x0008BCF5
		private bool HasFreeMMZ()
		{
			return this.GetMMZCount() < 5;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0008DB00 File Offset: 0x0008BD00
		private bool HaveTwoBodies()
		{
			return base.Bot.GetMonsterCount() >= 2;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0008DB13 File Offset: 0x0008BD13
		private bool ShouldFastEndToSPLK()
		{
			return this.enemyActivateMaxxC || this.enemyActivateFuwalos;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0008DB25 File Offset: 0x0008BD25
		private bool Step1Complete()
		{
			return base.Bot.HasInMonstersZone(32061192, false, false, false) && base.Bot.HasInMonstersZone(69272449, false, false, false);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0008DB54 File Offset: 0x0008BD54
		private bool CanStartStep1()
		{
			return !this.enemyActivateLancea && !this.ShouldFastEndToSPLK() && !this.HaveTwoBodies() && this.HasFreeMMZ() && (base.Bot.HasInHand(32061192) || base.Bot.HasInHand(69272449) || base.Bot.HasInHand(75500286) || base.Bot.HasInHand(73628505) || base.Bot.HasInHand(68337209));
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0008DBE1 File Offset: 0x0008BDE1
		private bool CanContinueStep1()
		{
			return !this.enemyActivateLancea && !this.HaveTwoBodies() && this.HasFreeMMZ();
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0008DC00 File Offset: 0x0008BE00
		private int PickMalissTrapToSet()
		{
			int pref;
			if (base.Duel.Player == 0 && base.Bot.HasInMonstersZone(32061192, false, false, false) && base.Bot.HasInMonstersZone(69272449, false, false, false))
			{
				pref = 20726052;
			}
			else if (base.Duel.Player == 0 && (base.Bot.HasInMonstersZoneOrInGraveyard(68059897) || base.Bot.HasInBanished(68059897)))
			{
				pref = 20726052;
			}
			else if (base.Duel.Player == 0 && base.Bot.HasInHand(20938824) && !this.ActiveMarchHare && !this.ssWhiteRabbit)
			{
				pref = 20726052;
			}
			else if (base.Duel.Player == 0 && this.nsBackupplan)
			{
				pref = 20726052;
			}
			else if (base.Duel.Player == 1)
			{
				pref = 94722358;
			}
			else
			{
				pref = 94722358;
			}
			if (this.CheckRemainInDeck(pref) > 0)
			{
				return pref;
			}
			return 0;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0008DD08 File Offset: 0x0008BF08
		private bool ActLittleKnight()
		{
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(29301450, 0))
			{
				List<ClientCard> problemCardList = this.GetProblematicEnemyCardList(true, false, CardType.Monster);
				problemCardList.AddRange(this.GetDangerousCardinEnemyGrave(false));
				problemCardList.AddRange(this.GetNormalEnemyTargetList(true, true, CardType.Monster));
				problemCardList.AddRange(from card in base.Enemy.Graveyard
					where card.HasType(CardType.Monster)
					orderby card.Attack descending
					select card);
				problemCardList.AddRange(base.Enemy.Graveyard.Where((ClientCard card) => !card.HasType(CardType.Monster)));
				if (problemCardList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(problemCardList);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			else if (base.ActivateDescription == base.Util.GetStringId(29301450, 1))
			{
				ClientCard selfMonster = null;
				foreach (ClientCard target in base.Bot.GetMonsters())
				{
					if (base.Duel.ChainTargets.Contains(target) && !this.escapeTargetList.Contains(target))
					{
						selfMonster = target;
						break;
					}
				}
				if (selfMonster == null && base.Duel.Player == 1)
				{
					selfMonster = (from card in base.Bot.GetMonsters()
						where card.IsAttack()
						orderby card.Attack
						select card).FirstOrDefault<ClientCard>();
					if (!base.Util.IsOneEnemyBetterThanValue(selfMonster.Attack, true))
					{
						selfMonster = null;
					}
				}
				if (selfMonster != null)
				{
					ClientCard nextMonster = null;
					List<ClientCard> selfTargetList = (from card in base.Bot.GetMonsters()
						where card != selfMonster
						select card).ToList<ClientCard>();
					if (base.Enemy.GetMonsterCount() == 0 && selfTargetList.Count<ClientCard>() > 0)
					{
						selfTargetList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
						nextMonster = selfTargetList[0];
						this.escapeTargetList.Add(nextMonster);
					}
					if (base.Enemy.GetMonsterCount() > 0)
					{
						nextMonster = this.GetBestEnemyMonster(true, true);
						this.currentDestroyCardList.Add(nextMonster);
					}
					if (nextMonster != null)
					{
						base.AI.SelectCard(selfMonster);
						base.AI.SelectNextCard(nextMonster);
						this.escapeTargetList.Add(selfMonster);
						this.activatedCardIdList.Add(base.Card.Id + 1);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0008E034 File Offset: 0x0008C234
		public int CompareUsableAttack(ClientCard cardA, ClientCard cardB)
		{
			if (cardA == null && cardB == null)
			{
				return 0;
			}
			if (cardA == null)
			{
				return -1;
			}
			if (cardB == null)
			{
				return 1;
			}
			int powerA = ((cardA.IsDefense() && this.summonThisTurn.Contains(cardA)) ? 0 : cardA.Attack);
			int powerB = ((cardB.IsDefense() && this.summonThisTurn.Contains(cardB)) ? 0 : cardB.Attack);
			if (powerA < powerB)
			{
				return -1;
			}
			if (powerA == powerB)
			{
				return CardContainer.CompareCardLevel(cardA, cardB);
			}
			return 1;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0008E0A6 File Offset: 0x0008C2A6
		private bool Step1_Dormouse_NormalSummon()
		{
			if (!this.CanStartStep1())
			{
				return false;
			}
			if (this.usedNormalSummon)
			{
				return false;
			}
			this.usedNormalSummon = true;
			this.NSDorMouse = true;
			return true;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0008E0CC File Offset: 0x0008C2CC
		private bool Dormouse_ForMH()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!this.CanContinueStep1())
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int pick;
			if (this.goldstart || this.undergroundstart)
			{
				pick = ((this.CheckRemainInDeck(69272449) > 0) ? 69272449 : 96676583);
			}
			else
			{
				pick = ((this.CheckRemainInDeck(20938824) > 0) ? 20938824 : ((this.CheckRemainInDeck(69272449) > 0) ? 69272449 : 96676583));
			}
			if (pick == 0)
			{
				return false;
			}
			base.AI.SelectCard(pick);
			return true;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0008E18C File Offset: 0x0008C38C
		private bool Step1_WhiteRabbit_SS_FromBanished()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Card.Id != 69272449)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			if (base.Bot.LifePoints <= 300)
			{
				return false;
			}
			this.ssWhiteRabbit = true;
			return true;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0008E1FC File Offset: 0x0008C3FC
		private bool Step1_WhiteRabbit_SetTrapOnSummon()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Id != 69272449)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int trapToSet = this.PickMalissTrapToSet();
			if (trapToSet == 0)
			{
				return false;
			}
			if (trapToSet == 20726052)
			{
				this.gwc06SetThisTurn = true;
			}
			if (trapToSet == 94722358)
			{
				this.mtp07SetThisTurn = true;
			}
			base.AI.SelectCard(trapToSet);
			this.SelectSafeSTZoneAwayFromImperm();
			if (this.Step1Complete())
			{
				this.step1Done = true;
			}
			return true;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0008E29E File Offset: 0x0008C49E
		private bool Step1_WhiteRabbit_NormalSummon()
		{
			if (!this.CanStartStep1())
			{
				return false;
			}
			if (base.Bot.HasInHand(32061192))
			{
				return false;
			}
			if (this.usedNormalSummon)
			{
				return false;
			}
			this.usedNormalSummon = true;
			return true;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0008E2D0 File Offset: 0x0008C4D0
		private bool Dormouse_SS_FromBanished()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Id != 32061192)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Bot.LifePoints <= 300)
			{
				return false;
			}
			this.ssDormouse = true;
			return true;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0008E340 File Offset: 0x0008C540
		private bool ChessyCat_SS_FromBanished()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Id != 96676583)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Bot.LifePoints <= 300)
			{
				return false;
			}
			this.ssChessyCat = true;
			return true;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0008E3B0 File Offset: 0x0008C5B0
		private bool SummonLittleKnightFast()
		{
			if (!base.Bot.HasInMonstersZone(98978921, false, false, false) && !base.Bot.HasInMonstersZone(24842059, false, false, false))
			{
				return false;
			}
			if (!this.HaveTwoBodies())
			{
				return false;
			}
			List<ClientCard> mats = (from c in base.Bot.GetMonsters()
				where c != null && c.IsFaceup() && c.HasType(CardType.Effect)
				orderby c.Attack
				select c).Take(2).ToList<ClientCard>();
			if (mats.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this.step1Done = true;
			return true;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0008E474 File Offset: 0x0008C674
		private bool SPEmer()
		{
			if (base.Bot.HasInMonstersZone(5043010, false, false, false) || base.Bot.HasInMonstersZone(39138610, false, false, false) || base.Bot.HasInMonstersZone(4280258, false, false, false) || base.Bot.HasInMonstersZone(86066372, false, false, false))
			{
				return false;
			}
			if (!this.HaveTwoBodies())
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(2, (ClientCard m) => m.HasType(CardType.Effect), 2, 2, new int[] { 4280258, 39138610, 86066372 }, false);
			if (mats.Count != 2)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0008E538 File Offset: 0x0008C738
		private bool GoldSarc_StartPiece()
		{
			if (this.CheckSpellWillBeNegate(false, null))
			{
				return false;
			}
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Bot.HasInHand(32061192) || base.Bot.HasInHand(69272449))
			{
				return false;
			}
			int pick;
			if (!base.Bot.HasInMonstersZone(32061192, false, false, false) && this.CheckRemainInDeck(32061192) > 0 && !this.ssDormouse)
			{
				pick = 32061192;
			}
			else if (!base.Bot.HasInMonstersZone(69272449, false, false, false) && this.CheckRemainInDeck(69272449) > 0 && !this.ssWhiteRabbit)
			{
				pick = 69272449;
			}
			else
			{
				if (!this.madeIt3 || this.ssChessyCat)
				{
					return false;
				}
				pick = 96676583;
			}
			if (pick == 0)
			{
				return false;
			}
			base.AI.SelectCard(pick);
			if (base.Card.Location == CardLocation.Hand)
			{
				this.SelectSTPlace(base.Card, true, null);
			}
			this.goldstart = true;
			return true;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0008E638 File Offset: 0x0008C838
		private bool ExistsForUnderground(int id)
		{
			return this.CheckRemainInDeck(id) > 0 || base.Bot.HasInHand(id) || base.Bot.HasInGraveyard(id);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0008E660 File Offset: 0x0008C860
		private bool Underground_ActivateStarter()
		{
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() != 0)
			{
				return false;
			}
			if (this.step1Done)
			{
				return false;
			}
			int pick = 0;
			if (this.ExistsForUnderground(32061192))
			{
				pick = 32061192;
			}
			else if (this.ExistsForUnderground(69272449))
			{
				pick = 69272449;
			}
			else if (this.ExistsForUnderground(96676583))
			{
				pick = 96676583;
			}
			if (pick == 0)
			{
				return false;
			}
			base.AI.SelectYesNo(true);
			base.AI.SelectCard(pick);
			this.ActiveUnderground = true;
			this.undergroundstart = true;
			return true;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0008E6FC File Offset: 0x0008C8FC
		private bool Terra_GrabUnderground()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (this.CheckSpellWillBeNegate(false, null))
			{
				return false;
			}
			if (this.ActiveUnderground)
			{
				return false;
			}
			if (base.Bot.HasInHand(68337209) || base.Bot.HasInSpellZone(68337209, false, false))
			{
				return false;
			}
			base.AI.SelectCard(68337209);
			if (base.Card.Location == CardLocation.Hand)
			{
				this.SelectSTPlace(base.Card, true, null);
			}
			return true;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0008E789 File Offset: 0x0008C989
		private bool HaveBackupOrWizardInHand()
		{
			return base.Bot.HasInHand(30118811) || base.Bot.HasInHand(3723262);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0008E7AF File Offset: 0x0008C9AF
		private bool HaveMHInHand()
		{
			return base.Bot.HasInHand(20938824);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0008E7C1 File Offset: 0x0008C9C1
		private bool IsMalissBody(ClientCard c)
		{
			return c != null && c.IsFaceup() && c.HasSetcode(447) && c.IsCode(new int[] { 32061192, 69272449, 96676583, 20938824 });
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0008E7F4 File Offset: 0x0008C9F4
		private bool Emergency_NormalCat()
		{
			if (base.Bot.GetMonsterCount() != 0)
			{
				return false;
			}
			if (this.usedNormalSummon)
			{
				return false;
			}
			if (base.Bot.HasInHand(32061192) || base.Bot.HasInHand(69272449) || base.Bot.HasInHand(68337209) || base.Bot.HasInHand(73628505) || base.Bot.HasInHand(75500286))
			{
				return false;
			}
			if (!this.HaveMHInHand())
			{
				return false;
			}
			this.usedNormalSummon = true;
			return true;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0008E888 File Offset: 0x0008CA88
		private bool OneBody_Link1_Linguriboh()
		{
			return this.HaveBackupOrWizardInHand() && !base.Bot.HasInMonstersZone(24842059, false, false, false) && base.Bot.GetMonsterCount() == 1;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0008E8BC File Offset: 0x0008CABC
		private bool OneBody_Link1_Almiraj()
		{
			return this.HaveBackupOrWizardInHand() && !base.Bot.HasInMonstersZone(60303245, false, false, false) && base.Bot.GetMonsterCount() == 1;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0008E8F0 File Offset: 0x0008CAF0
		private bool OneBody_Backup_SS()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(24842059, false, false, false) && !base.Bot.HasInMonstersZone(59859086, false, false, false))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			this.avoidLinkedZones = true;
			return true;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0008E95C File Offset: 0x0008CB5C
		private bool OneBody_Backup_SearchWizard()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!base.Card.IsCode(30118811))
			{
				return false;
			}
			if (base.Bot.Hand.Count == 0)
			{
				return false;
			}
			bool haveWizard = base.Bot.HasInHand(3723262);
			int searchId = 0;
			if (haveWizard && this.CheckRemainInDeck(20938824) > 0 && base.Bot.Hand.Count > 0)
			{
				searchId = 20938824;
			}
			else if (this.CheckRemainInDeck(32061192) > 0 && this.nsplan && base.Bot.HasInMonstersZone(60303245, false, false, false))
			{
				searchId = 32061192;
			}
			else if (this.CheckRemainInDeck(69272449) > 0 && this.nsBackupplan)
			{
				searchId = 69272449;
			}
			else if (!haveWizard && this.CheckRemainInDeck(3723262) > 0 && base.Bot.Hand.Count > 0)
			{
				searchId = 3723262;
			}
			else
			{
				if (this.CheckRemainInDeck(69272449) <= 0)
				{
					return false;
				}
				searchId = 69272449;
			}
			base.AI.SelectCard(searchId);
			List<ClientCard> hand = base.Bot.Hand.Where((ClientCard h) => h != null).ToList<ClientCard>();
			List<ClientCard> candidates = hand.Where((ClientCard h) => h.Id != 3723262).ToList<ClientCard>();
			List<ClientCard> discards = new List<ClientCard>(hand.Count);
			if (searchId == 32061192)
			{
				discards.AddRange(candidates.Where((ClientCard c) => c.Id == 32061192));
			}
			List<ClientCard> othersExcludingTarget = delegate
			{
				if (searchId == 20938824)
				{
					return candidates.Where((ClientCard c) => c.Id != 20938824);
				}
				return candidates;
			}().ToList<ClientCard>();
			IEnumerable<IGrouping<int, ClientCard>> dupGroups = from c in othersExcludingTarget
				group c by c.Id into g
				where g.Count<ClientCard>() >= 2
				select g;
			discards.AddRange(dupGroups.SelectMany((IGrouping<int, ClientCard> g) => g));
			int[] array = new int[] { 27204311, 34267821, 73628505, 75500286 };
			for (int i = 0; i < array.Length; i++)
			{
				int id = array[i];
				Func<IGrouping<int, ClientCard>, bool> <>9__11;
				discards.AddRange(othersExcludingTarget.Where(delegate(ClientCard c)
				{
					if (c.Id == id)
					{
						IEnumerable<IGrouping<int, ClientCard>> dupGroups2 = dupGroups;
						Func<IGrouping<int, ClientCard>, bool> func;
						if ((func = <>9__11) == null)
						{
							func = (<>9__11 = (IGrouping<int, ClientCard> g) => g.Key != id);
						}
						return dupGroups2.All(func);
					}
					return false;
				}));
			}
			HashSet<ClientCard> already = new HashSet<ClientCard>(discards);
			discards.AddRange(othersExcludingTarget.Where((ClientCard c) => !already.Contains(c)));
			discards = discards.Where((ClientCard c) => c != null).Distinct<ClientCard>().ToList<ClientCard>();
			if (searchId == 32061192)
			{
				base.AI.SelectNextCard(searchId);
			}
			if (searchId == 69272449)
			{
				base.AI.SelectNextCard(searchId);
			}
			else
			{
				if (discards == null)
				{
					return false;
				}
				base.AI.SelectNextCard(discards);
			}
			this.avoidLinkedZones = true;
			if (base.Bot.HasInMonstersZone(24842059, false, false, false))
			{
				this.blockWicckid = true;
			}
			if (this.GetMMZCount() >= 5 && base.Bot.HasInHand(3723262))
			{
				this.fullBoard1 = true;
			}
			return true;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0008ED40 File Offset: 0x0008CF40
		private bool OneBody_Wizard_SS()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(24842059, false, false, false) && !base.Bot.HasInMonstersZone(60303245, false, false, false))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			ClientCard revive = this.PickGYCybersePriority();
			if (revive == null)
			{
				return false;
			}
			this.avoidLinkedZones = true;
			this.blockWicckid = true;
			base.AI.SelectCard(revive);
			return true;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0008EDC8 File Offset: 0x0008CFC8
		private ClientCard PickGYCybersePriority()
		{
			ClientCard i = this.PickGYMalissPriority();
			if (i != null)
			{
				return i;
			}
			return base.Bot.Graveyard.GetMatchingCards((ClientCard c) => c != null && c.IsMonster() && c.HasRace(CardRace.Cyberse) && c.Level <= 4).ToList<ClientCard>().FirstOrDefault<ClientCard>();
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0008EE1C File Offset: 0x0008D01C
		private ClientCard PickGYMalissPriority()
		{
			int[] array = new int[] { 32061192, 69272449, 96676583, 20938824 };
			for (int i = 0; i < array.Length; i++)
			{
				int id = array[i];
				ClientCard c = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(id) && g.IsMonster());
				if (c != null)
				{
					return c;
				}
			}
			return null;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0008EE7C File Offset: 0x0008D07C
		private bool TwoCC_NormalSummon()
		{
			if (this.usedNormalSummon)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() != 0)
			{
				return false;
			}
			if (base.Bot.HasInHand(32061192) || base.Bot.HasInHand(69272449) || base.Bot.HasInHand(75500286) || base.Bot.HasInHand(68337209))
			{
				return false;
			}
			if (base.Bot.Hand.GetMatchingCards((ClientCard c) => c != null && c.IsCode(96676583)).Count >= 2)
			{
				this.usedNormalSummon = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0008EF30 File Offset: 0x0008D130
		private bool IsMalissCost(ClientCard card)
		{
			return card != null && (card.IsCode(96676583) || (card.IsCode(69272449) && this.NSDorMouse && !this.ssWhiteRabbit) || (card.IsCode(69272449) && this.ssWhiteRabbit) || (card.IsCode(32061192) && this.ssDormouse) || (card.IsCode(20938824) && this.ActiveMarchHare));
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0008EFB6 File Offset: 0x0008D1B6
		private bool IsMalissCost2(ClientCard card)
		{
			return card != null && (card.IsCode(96676583) || card.IsCode(69272449) || card.IsCode(20938824) || card.IsCode(68337209));
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0008EFF4 File Offset: 0x0008D1F4
		private bool AnyDraw()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Id != 96676583)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			ClientCard target = base.Bot.Hand.FirstOrDefault((ClientCard c) => c != null && c.IsCode(93453053));
			if (target == null)
			{
				target = base.Bot.Hand.FirstOrDefault(new Func<ClientCard, bool>(this.IsMalissCost));
			}
			if (target == null)
			{
				IGrouping<int, ClientCard> malissDupGroup = (from c in base.Bot.Hand.Where(new Func<ClientCard, bool>(this.IsMalissCost2))
					group c by c.Id).FirstOrDefault((IGrouping<int, ClientCard> g) => g.Count<ClientCard>() >= 2);
				if (malissDupGroup != null)
				{
					target = malissDupGroup.First<ClientCard>();
				}
			}
			if (target == null)
			{
				return false;
			}
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0008F120 File Offset: 0x0008D320
		private int PickTB11CostCandidateId()
		{
			List<ClientCard> field = (from c in base.Bot.GetMonsters()
				where c != null && c.IsFaceup() && c.HasSetcode(447)
				select c).ToList<ClientCard>();
			field = field.Where((ClientCard c) => (!c.IsCode(69272449) || !this.ssWhiteRabbit) && (!c.IsCode(96676583) || !this.ssChessyCat) && (!c.IsCode(20938824) || !this.ssMarchHare) && (!c.IsCode(32061192) || !this.ssDormouse) && (!c.IsCode(68059897) || !this.ssRRThisTurn) && (!c.IsCode(95454996) || !this.ssWBThisTurn) && (!c.IsCode(21848500) || !this.ssHCThisTurn)).ToList<ClientCard>();
			int[] array = new int[] { 69272449, 96676583, 20938824, 32061192, 68059897, 95454996, 21848500 };
			for (int i = 0; i < array.Length; i++)
			{
				int id = array[i];
				if (field.Any((ClientCard c) => c.IsCode(id)))
				{
					return id;
				}
			}
			return 0;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
		private int PickPFromGYForSplash()
		{
			if (this.enemyActivateLancea)
			{
				foreach (int id in new int[] { 32061192, 69272449, 96676583, 20938824, 30118811, 3723262 })
				{
					if (base.Bot.HasInGraveyard(id))
					{
						return id;
					}
				}
			}
			foreach (int id2 in new int[] { 32061192, 69272449, 96676583, 20938824 })
			{
				if (base.Bot.HasInGraveyard(id2))
				{
					return id2;
				}
			}
			return 0;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0008F246 File Offset: 0x0008D446
		private bool HaveUndergroundOnHandOrField()
		{
			return base.Bot.HasInHand(68337209) || base.Bot.HasInSpellZone(68337209, false, false);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0008F26E File Offset: 0x0008D46E
		private bool ShouldSearchUnderground()
		{
			return !this.ActiveUnderground && !this.HaveUndergroundOnHandOrField() && this.CheckRemainInDeck(68337209) > 0;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0008F294 File Offset: 0x0008D494
		private bool Step2_RedRansom_Search()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			int chooseId = 0;
			if (this.ShouldSearchUnderground())
			{
				chooseId = 68337209;
			}
			else if (this.CheckRemainInDeck(93453053) > 0)
			{
				chooseId = 93453053;
			}
			if (chooseId == 0)
			{
				return false;
			}
			this.step2Done = true;
			this.avoidLinkedZones = false;
			this.coreSetupComplete = true;
			return true;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0008F2F8 File Offset: 0x0008D4F8
		private List<ClientCard> PickMaterialsForRedRansom()
		{
			ClientCard splash = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(59859086));
			if (splash == null)
			{
				return new List<ClientCard>();
			}
			ClientCard revived = null;
			if (this.lastRevivedIdBySplash != 0)
			{
				revived = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(this.lastRevivedIdBySplash));
			}
			if (revived == null)
			{
				revived = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(447) && c != splash && !c.HasType(CardType.Link));
			}
			if (revived == null)
			{
				return new List<ClientCard>();
			}
			return new List<ClientCard> { splash, revived };
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0008F3C0 File Offset: 0x0008D5C0
		private bool Step2_SplashMage_ReviveP()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (this.step2Done)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(59859086, false, false, false))
			{
				return false;
			}
			int pick = this.PickPFromGYForSplash();
			if (pick == 0)
			{
				return false;
			}
			base.AI.SelectCard(pick);
			this.lastRevivedIdBySplash = pick;
			return true;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0008F424 File Offset: 0x0008D624
		private bool Step2N_SplashMage_ReviveP()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(59859086, false, false, false))
			{
				return false;
			}
			int pick = this.PickPFromGYForSplash();
			if (pick == 0)
			{
				return false;
			}
			base.AI.SelectCard(pick);
			this.lastRevivedIdBySplash = pick;
			return true;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0008F480 File Offset: 0x0008D680
		private bool Step2_LinkSummon_RedRansom()
		{
			if (this.step2Done)
			{
				return false;
			}
			List<ClientCard> mats = this.PickMaterialsForRedRansom();
			if (mats.Count != 2)
			{
				return false;
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck(mats) == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this.madeIt3 = true;
			return true;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0008F4D0 File Offset: 0x0008D6D0
		private bool Step2N_LinkSummon_WB()
		{
			List<ClientCard> mats = this.PickMaterialsForRedRansom();
			if (mats.Count != 2)
			{
				return false;
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck(mats) == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0008F50D File Offset: 0x0008D70D
		private bool Step2N_LinkSummon_RedRansom()
		{
			if (!base.Bot.HasInMonstersZone(20938824, false, false, false) || base.Bot.GetMonsterCount() < 3)
			{
				return false;
			}
			this.madeIt3 = true;
			return true;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0008F53C File Offset: 0x0008D73C
		private bool Step2_Fallback_Wizard_AfterSplashNegated()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!this.splashNegatedThisTurn)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			ClientCard revive = this.PickGYMalissPriority();
			if (revive == null)
			{
				return false;
			}
			this.avoidLinkedZones = true;
			this.blockWicckid = true;
			base.AI.SelectCard(revive);
			return true;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0008F5A0 File Offset: 0x0008D7A0
		private bool Step2_Fallback_Backup_AfterSplashNegated()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!this.splashNegatedThisTurn)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (this.GetMMZCount() >= 5)
			{
				return false;
			}
			int want = ((!base.Bot.HasInHand(3723262) && this.CheckRemainInDeck(3723262) > 0) ? 3723262 : ((this.CheckRemainInDeck(20938824) > 0) ? 20938824 : 0));
			if (want == 0)
			{
				return false;
			}
			base.AI.SelectCard(want);
			ClientCard discard = base.Bot.Hand.FirstOrDefault((ClientCard h) => h != null && h.Id != 3723262 && h != base.Card);
			if (discard != null)
			{
				base.AI.SelectNextCard(discard);
			}
			this.avoidLinkedZones = true;
			this.blockWicckid = true;
			return true;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0008F246 File Offset: 0x0008D446
		private bool HaveUG()
		{
			return base.Bot.HasInHand(68337209) || base.Bot.HasInSpellZone(68337209, false, false);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0008F674 File Offset: 0x0008D874
		private bool HasSelfSSAvailable(int id)
		{
			if (id == 32061192)
			{
				return !this.ssDormouse;
			}
			if (id == 69272449)
			{
				return !this.ssWhiteRabbit;
			}
			if (id == 96676583)
			{
				return !this.ssChessyCat;
			}
			return id != 20938824 || !this.ActiveMarchHare;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0008F6CC File Offset: 0x0008D8CC
		private int PickUG_DHG_DormouseFirst()
		{
			foreach (int id in new int[] { 32061192, 69272449, 96676583, 20938824 })
			{
				if (this.HasSelfSSAvailable(id) && this.ExistsForUnderground(id))
				{
					return id;
				}
			}
			return 0;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0008F714 File Offset: 0x0008D914
		private bool Flow3_UnderGround_Available_SSAnyPawn()
		{
			if (!this.step2Done)
			{
				return false;
			}
			if (!this.HaveUG())
			{
				return false;
			}
			if (base.Card.Id != 68337209)
			{
				return false;
			}
			if (this.resultSuccessFlag)
			{
				return false;
			}
			if (this.GetMMZCount() >= 5)
			{
				base.AI.SelectYesNo(false);
				return true;
			}
			if (this.enemyActivateLancea)
			{
				base.AI.SelectYesNo(false);
				return true;
			}
			int pick;
			if (this.CheckRemainInDeck(93453053) > 0 && this.nsplan)
			{
				pick = 93453053;
			}
			else if (this.CheckRemainInDeck(93453053) > 0 && this.NSDorMouse)
			{
				pick = 93453053;
			}
			else
			{
				pick = this.PickUG_DHG_DormouseFirst();
			}
			if (pick == 0)
			{
				base.AI.SelectYesNo(false);
				return true;
			}
			base.AI.SelectYesNo(true);
			base.AI.SelectCard(pick);
			this.resultSuccessFlag = true;
			return true;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0008F7F8 File Offset: 0x0008D9F8
		private bool Mirror_Banish()
		{
			if (this.DescIs(93453053, new int[] { 1 }))
			{
				ClientCard gy = this.PickMirrorGYTargetForSearch();
				if (gy == null)
				{
					return false;
				}
				base.AI.SelectCard(gy);
				int[] wants = this.Mirror_SearchOrderForType(false, gy.IsMonster());
				base.AI.SelectNextCard(wants);
				return true;
			}
			else
			{
				if (this.CheckSpellWillBeNegate(false, null))
				{
					return false;
				}
				if (this.CheckWhetherNegated(true, false, (CardType)0))
				{
					return false;
				}
				ClientCard cost = this.PickMirrorCostCandidate();
				if (cost == null)
				{
					return false;
				}
				foreach (ClientCard i in base.Enemy.GetMonsters())
				{
					if (i.IsMonsterShouldBeDisabledBeforeItUseEffect() && !i.IsDisabled() && base.Duel.LastChainPlayer != 0)
					{
						if (base.Card.Location == CardLocation.Hand)
						{
							this.SelectSTPlace(base.Card, true, null);
						}
						base.AI.SelectCard(i);
						base.AI.SelectNextCard(cost);
						return true;
					}
				}
				ClientCard LastChainCard = base.Util.GetLastChainCard();
				if (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
				{
					return false;
				}
				if (base.Card.Location == CardLocation.Hand)
				{
					this.SelectSTPlace(base.Card, true, null);
				}
				if (LastChainCard != null)
				{
					base.AI.SelectCard(LastChainCard);
				}
				else
				{
					List<ClientCard> monsters = base.Enemy.GetMonsters();
					monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					monsters.Reverse();
					foreach (ClientCard card in monsters)
					{
						if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
						{
							base.AI.SelectCard(card);
						}
					}
				}
				base.AI.SelectNextCard(cost);
				return true;
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0008FA14 File Offset: 0x0008DC14
		private bool Flow3_Link_Accesscode()
		{
			if (base.Bot.HasInExtra(39138610))
			{
				return false;
			}
			if (this.BlockAccesscodeOnT1())
			{
				return false;
			}
			int[] avoid = new int[] { 39138610, 5043010 };
			List<ClientCard> mats = this.PickLinkMatsMinCount(4, (ClientCard m) => m.HasType(CardType.Effect), 2, 4, avoid, false);
			if (mats.Count < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0008FA9A File Offset: 0x0008DC9A
		private bool Flow3_BackupIgnister_AfterMakeIt3()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!this.madeIt3)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			this.avoidLinkedZones = true;
			return true;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0008FAD4 File Offset: 0x0008DCD4
		private bool Flow3_WizardIgnister_AfterMakeIt3()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (!this.madeIt3)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			ClientCard revive = this.PickGYMalissPriority();
			if (revive == null)
			{
				return false;
			}
			this.avoidLinkedZones = true;
			base.AI.SelectCard(revive);
			return true;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0008FB30 File Offset: 0x0008DD30
		private bool RR_SS_FromBanished()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Id != 68059897)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			if (this.GetMMZCount() >= 5)
			{
				return false;
			}
			if (base.Bot.LifePoints <= 900)
			{
				return false;
			}
			int pickId = 0;
			foreach (int id in new int[] { 96676583, 20938824, 69272449, 32061192 })
			{
				if (this.CheckRemainInDeck(id) > 0 && this.PawnSelfSS_AvailableId(id))
				{
					pickId = id;
					break;
				}
			}
			bool canBanish = pickId != 0;
			base.AI.SelectYesNo(canBanish);
			if (canBanish)
			{
				base.AI.SelectCard(pickId);
			}
			this._rrSelfSSPlacing = true;
			this.ssRRThisTurn = true;
			return true;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0008FC08 File Offset: 0x0008DE08
		private bool Wicckid_SearchTuner()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (this.CheckRemainInDeck(30118811) <= 0)
			{
				return false;
			}
			ClientCard cost = this.PickGYCyberseForWicckidCost_Safe();
			if (cost == null)
			{
				return false;
			}
			base.AI.SelectCard(cost);
			base.AI.SelectNextCard(30118811);
			this.avoidLinkedZones = false;
			return true;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0008FC6A File Offset: 0x0008DE6A
		private bool LinkDecoder_ReviveFromGY()
		{
			return !base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id) && base.Card.Location == CardLocation.Grave && !this.Allied_End;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0008FCA0 File Offset: 0x0008DEA0
		private bool Transcode_ReviveLink3OrLower()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			ClientCard clientCard;
			if ((clientCard = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c.IsCode(52698008))) == null)
			{
				if ((clientCard = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c.IsCode(59859086))) == null)
				{
					if ((clientCard = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c.IsCode(68059897))) == null)
					{
						if ((clientCard = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c.IsCode(95454996))) == null)
						{
							clientCard = (from c in base.Bot.Graveyard.GetMatchingCards((ClientCard c) => c.IsMonster() && c.HasType(CardType.Link) && c.LinkCount <= 3 && !c.IsCode(46947713))
								orderby c.Attack descending
								select c).FirstOrDefault<ClientCard>();
						}
					}
				}
			}
			ClientCard prefer = clientCard;
			if (prefer == null)
			{
				return false;
			}
			base.AI.SelectCard(prefer);
			this.avoidLinkedZones = false;
			return true;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0008FE1C File Offset: 0x0008E01C
		private bool Allied_NegateBanish()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0) || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			ClientCard allied = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(39138610));
			if (allied == null || allied.IsDisabled())
			{
				return false;
			}
			if (!base.Bot.GetMonsters().Any((ClientCard m) => m != null && m.HasType(CardType.Link) && !m.IsCode(39138610)))
			{
				return false;
			}
			List<int> cheapLinks = new List<int> { 52698008, 95454996, 46947713, 68059897 };
			base.AI.SelectCard(cheapLinks.ToArray());
			return true;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0008FF07 File Offset: 0x0008E107
		private ClientCard GetWicckid()
		{
			return base.Bot.MonsterZone.GetFirstMatchingFaceupCard((ClientCard c) => c != null && c.IsCode(52698008));
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0008FF38 File Offset: 0x0008E138
		private int GetLinkedMaskFor(ClientCard link)
		{
			if (link == null || !link.IsFaceup() || !link.HasType(CardType.Link))
			{
				return 0;
			}
			return link.GetLinkedZones() & 31;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0008FF60 File Offset: 0x0008E160
		private bool PawnSelfSS_AvailableId(int id)
		{
			if (id == 32061192)
			{
				return !this.ssDormouse;
			}
			if (id == 69272449)
			{
				return !this.ssWhiteRabbit;
			}
			if (id == 96676583)
			{
				return !this.ssChessyCat;
			}
			return id == 20938824 && !this.ActiveMarchHare;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0008FFB6 File Offset: 0x0008E1B6
		private bool QueenSelfSS_AvailableId(int id)
		{
			if (id == 21848500)
			{
				return !this.ssHCThisTurn;
			}
			if (id == 95454996)
			{
				return !this.ssWBThisTurn;
			}
			return id == 68059897 && !this.ssRRThisTurn;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0008FFF0 File Offset: 0x0008E1F0
		private bool Dormouse_Banish_Anytime()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Id != 32061192)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (!this.HasFreeMMZ())
			{
				return false;
			}
			int pick = 0;
			if (this.CheckRemainInDeck(69272449) > 0 && this.PawnSelfSS_AvailableId(69272449))
			{
				pick = 69272449;
			}
			else if (this.CheckRemainInDeck(96676583) > 0 && this.PawnSelfSS_AvailableId(96676583))
			{
				pick = 96676583;
			}
			if (pick == 0)
			{
				return false;
			}
			base.AI.SelectCard(pick);
			return true;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000900B2 File Offset: 0x0008E2B2
		private bool IsCyberse(ClientCard c)
		{
			return c != null && c.HasType(CardType.Monster) && c.HasRace(CardRace.Cyberse);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000900CD File Offset: 0x0008E2CD
		private bool RR_HOPT_Spent_ThisTurn()
		{
			return this.ssRRThisTurn;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000900D5 File Offset: 0x0008E2D5
		private bool RR_CanStillSS_ThisTurn()
		{
			return !this.RR_HOPT_Spent_ThisTurn();
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000900E4 File Offset: 0x0008E2E4
		private int Score_WicckidCost(ClientCard c)
		{
			if (c == null)
			{
				return int.MinValue;
			}
			if (c.IsCode(68059897) && this.RR_HOPT_Spent_ThisTurn())
			{
				return -999;
			}
			if (c.IsCode(68059897) && this.RR_CanStillSS_ThisTurn())
			{
				return 1000;
			}
			if (c.IsCode(24842059) || c.IsCode(60303245))
			{
				return 120;
			}
			if (c.IsCode(32061192) && this.ssDormouse)
			{
				return 90;
			}
			if (c.IsCode(69272449) && this.ssWhiteRabbit)
			{
				return 70;
			}
			if (c.IsCode(96676583) && this.ssChessyCat)
			{
				return 60;
			}
			if (c.IsCode(20938824) && this.ssMarchHare)
			{
				return 60;
			}
			if (c.IsCode(30118811))
			{
				return 20;
			}
			if (c.IsCode(3723262))
			{
				return 20;
			}
			return 30;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000901D0 File Offset: 0x0008E3D0
		private ClientCard PickGYCyberseForWicckidCost_Safe()
		{
			List<ClientCard> gy = (base.Bot.Graveyard ?? new List<ClientCard>()).Where(new Func<ClientCard, bool>(this.IsCyberse)).ToList<ClientCard>();
			if (gy.Count == 0)
			{
				return null;
			}
			ClientCard best = null;
			int bestScore = int.MinValue;
			foreach (ClientCard c in gy)
			{
				int sc = this.Score_WicckidCost(c);
				if (sc > bestScore)
				{
					best = c;
					bestScore = sc;
				}
			}
			return best;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0009026C File Offset: 0x0008E46C
		private bool BlockAccesscodeOnT1()
		{
			return base.Duel.Player == 0 && base.Duel.Turn == 1;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0009028C File Offset: 0x0008E48C
		private bool Allied_OnSummonTrigger()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(39138610)) == null)
			{
				return false;
			}
			if (!base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsMonster() && c.HasRace(CardRace.Cyberse) && c.Attack == 2300))
			{
				return false;
			}
			List<int> prefer = new List<int> { 68059897, 95454996, 46947713 };
			base.AI.SelectCard(prefer.ToArray());
			return true;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00090350 File Offset: 0x0008E550
		private bool ShouldSummonTranscode()
		{
			bool flag = base.Bot.HasInGraveyard(59859086);
			bool haveRR = base.Bot.HasInMonstersZone(68059897, false, false, false);
			bool haveL = base.Bot.HasInMonstersZone(30342076, false, false, false) || base.Bot.HasInGraveyard(30342076);
			return flag && (haveRR || haveL);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000903B4 File Offset: 0x0008E5B4
		private bool Step_SplashToRR()
		{
			if (this._didSplashToRR)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(30342076, false, false, false))
			{
				return false;
			}
			if (!base.Bot.HasInExtra(68059897))
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(2, (ClientCard m) => m.HasType(CardType.Effect), 2, 2, new int[] { 52698008 }, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this._didSplashToRR = true;
			return true;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00090450 File Offset: 0x0008E650
		private bool Step_SplashToWB()
		{
			if (this._didSplashToRR)
			{
				return false;
			}
			if (!base.Bot.HasInExtra(95454996))
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(2, (ClientCard m) => m.HasType(CardType.Effect), 2, 2, new int[] { 52698008 }, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this._didSplashToRR = true;
			return true;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000904D4 File Offset: 0x0008E6D4
		private bool Step_RRtoWicckid()
		{
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (this.blockWicckid)
			{
				return true;
			}
			if (!this._didSplashToRR || this._didRRtoWicckid)
			{
				return false;
			}
			if (!base.Bot.HasInExtra(52698008))
			{
				return false;
			}
			ClientCard rr = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(68059897));
			if (rr == null)
			{
				return false;
			}
			ClientCard buddy = base.Bot.GetMonsters().FirstOrDefault((ClientCard m) => m != null && m != rr && this.IsInEMZ(m)) ?? base.Bot.MonsterZone.FirstOrDefault((ClientCard m) => m != null && m != rr);
			if (buddy == null)
			{
				return false;
			}
			if (!this.HasFreeEMZ() && !this.IsInEMZ(rr) && !this.IsInEMZ(buddy))
			{
				return false;
			}
			base.AI.SelectCard(52698008);
			base.AI.SelectMaterials(new List<ClientCard> { rr, buddy }, 0);
			this.wantLinkedToWicckid = true;
			this._preferWicckidArrows = true;
			this._didRRtoWicckid = true;
			return true;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00090618 File Offset: 0x0008E818
		private bool Step2N_RRtoWicckid()
		{
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (this.blockWicckid)
			{
				return false;
			}
			ClientCard rr = base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(68059897));
			if (rr == null)
			{
				return false;
			}
			ClientCard buddy = base.Bot.GetMonsters().FirstOrDefault((ClientCard m) => m != null && m != rr && this.IsInEMZ(m)) ?? base.Bot.MonsterZone.FirstOrDefault((ClientCard m) => m != null && m != rr);
			if (buddy == null)
			{
				return false;
			}
			if (!this.HasFreeEMZ() && !this.IsInEMZ(rr) && !this.IsInEMZ(buddy))
			{
				return false;
			}
			base.AI.SelectCard(52698008);
			base.AI.SelectMaterials(new List<ClientCard> { rr, buddy }, 0);
			this.wantLinkedToWicckid = true;
			this._preferWicckidArrows = true;
			this._didRRtoWicckid = true;
			return true;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00090738 File Offset: 0x0008E938
		private bool Step_SummonLinkDecoderToWicckid()
		{
			if (!this._didRRtoWicckid || this._didSummonToWicckidArrow)
			{
				return false;
			}
			if (base.Bot.MonsterZone.GetFirstMatchingCard((ClientCard m) => m != null && m.IsCode(52698008)) == null)
			{
				return false;
			}
			this._preferWicckidArrows = true;
			this.avoidLinkedZones = false;
			return true;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0009079C File Offset: 0x0008E99C
		private bool Step1_SSLinkDecoder()
		{
			return (base.Bot.HasInMonstersZone(32061192, false, false, false) || base.Bot.HasInMonstersZone(69272449, false, false, false) || base.Bot.HasInMonstersZone(96676583, false, false, false) || base.Bot.HasInMonstersZone(30118811, false, false, false)) && base.Bot.GetMonsterCount() == 1 && base.Bot.HasInHand(20938824);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00090820 File Offset: 0x0008EA20
		private bool IsMalissMonster(ClientCard c)
		{
			return c != null && c.IsMonster() && c.HasSetcode(447);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0009083C File Offset: 0x0008EA3C
		private bool CanMakeLinkNWithFlexibleTwo(ClientCard a, ClientCard b, int target)
		{
			int a2 = 1;
			int aL = ((a != null && a.HasType(CardType.Link)) ? Math.Max(1, a.LinkCount) : 1);
			int b2 = 1;
			int bL = ((b != null && b.HasType(CardType.Link)) ? Math.Max(1, b.LinkCount) : 1);
			return a2 + b2 == target || a2 + bL == target || aL + b2 == target || aL + bL == target;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000908A8 File Offset: 0x0008EAA8
		private bool Step_WicckidPlusOneToWB()
		{
			if (!base.Bot.HasInExtra(95454996))
			{
				return false;
			}
			ClientCard wic = this.GetWicckid();
			if (wic != null)
			{
				List<ClientCard> partners = (from m in base.Bot.GetMonsters()
					where m != null && m.IsFaceup() && m != wic && m.HasType(CardType.Effect)
					select m).ToList<ClientCard>();
				Func<ClientCard, bool> okPair = (ClientCard p) => (this.IsMalissMonster(p) || this.IsMalissMonster(wic)) && this.CanMakeLinkNWithFlexibleTwo(wic, p, 3);
				ClientCard p2 = partners.FirstOrDefault((ClientCard p) => !p.IsCode(68059897) && okPair(p)) ?? partners.FirstOrDefault((ClientCard p) => p.IsCode(68059897) && okPair(p));
				if (p2 != null)
				{
					base.AI.SelectMaterials(new List<ClientCard> { wic, p2 }, 0);
					this._didWBFromWicckid = true;
					this.EnsureFinishPlanAfterWB();
					return true;
				}
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(3, (ClientCard m) => m.HasType(CardType.Effect), 2, 3, new int[] { 4280258, 5043010, 39138610 }, true);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this._didWBFromWicckid = true;
			this.EnsureFinishPlanAfterWB();
			return true;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000909EC File Offset: 0x0008EBEC
		private void EnsureFinishPlanAfterWB()
		{
			if (this._finishPlanDecided)
			{
				return;
			}
			int free = base.Bot.MonsterZone.Count((ClientCard m) => m != null && !m.IsCode(68059897));
			bool flag = base.Bot.HasInExtra(5043010);
			bool canHC = base.Bot.HasInExtra(21848500);
			bool canAll = base.Bot.HasInExtra(39138610);
			bool reachAllied = this.CanReachAlliedNow();
			if (flag && canHC && canAll && free >= 3 && reachAllied)
			{
				this._finishPlan = MalissExecutor.FinishPlan.FW_HC_Allied;
			}
			else if (canHC && canAll && free >= 2 && reachAllied)
			{
				this._finishPlan = MalissExecutor.FinishPlan.HC_Allied;
			}
			else
			{
				this._finishPlan = MalissExecutor.FinishPlan.AlliedOnly;
			}
			this._finishPlanDecided = true;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00090AB4 File Offset: 0x0008ECB4
		private bool ssFromHandMH()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (this.GetMMZCount() > 3)
			{
				return false;
			}
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Duel.Player != 0)
			{
				if (base.Bot.HasInGraveyard(95454996) && !this.ssWBThisTurn)
				{
					ClientCard Target = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(95454996));
					if (Target == null)
					{
						return false;
					}
					base.AI.SelectCard(Target);
				}
				else if (base.Bot.HasInGraveyard(21848500) && !this.ssHCThisTurn)
				{
					ClientCard Target2 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(21848500));
					if (Target2 == null)
					{
						return false;
					}
					base.AI.SelectCard(Target2);
				}
				else
				{
					if (!base.Bot.HasInGraveyard(68059897) || this.ssRRThisTurn)
					{
						return false;
					}
					ClientCard Target3 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(68059897));
					if (Target3 == null)
					{
						return false;
					}
					base.AI.SelectCard(Target3);
				}
				this.ssMarchHare = true;
				return this.DontSelfNG();
			}
			List<ClientCard> gy = base.Bot.Graveyard.GetMatchingCards((ClientCard c) => c != null && c.HasSetcode(447) && c != base.Card).ToList<ClientCard>();
			ClientCard pick = delegate(IEnumerable<ClientCard> src)
			{
				int[] array = new int[] { 95454996, 21848500, 68059897 };
				for (int i = 0; i < array.Length; i++)
				{
					int id = array[i];
					ClientCard c = src.FirstOrDefault((ClientCard x) => x.HasType(CardType.Monster) && x.Id == id && this.QueenSelfSS_AvailableId(x.Id));
					if (c != null)
					{
						return c;
					}
				}
				return src.FirstOrDefault((ClientCard x) => x.HasType(CardType.Monster) && this.QueenSelfSS_AvailableId(x.Id));
			}(gy);
			if (pick == null)
			{
				return false;
			}
			base.AI.SelectCard(pick);
			this.ssMarchHare = true;
			return this.DontSelfNG();
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00090C88 File Offset: 0x0008EE88
		private bool Step1_MH_FromHand()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() > 1)
			{
				return false;
			}
			if (this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(69272449, false, false, false) && base.Bot.HasInGraveyard(94722358))
			{
				ClientCard target = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(94722358));
				if (target == null)
				{
					return false;
				}
				base.AI.SelectCard(target);
				this.ssMarchHare = true;
				return this.DontSelfNG();
			}
			else if (base.Bot.HasInMonstersZone(30342076, false, false, false) && base.Bot.HasInGraveyard(32061192))
			{
				ClientCard target2 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(32061192));
				if (target2 == null)
				{
					return false;
				}
				base.AI.SelectCard(target2);
				this.ssMarchHare = true;
				return this.DontSelfNG();
			}
			else if (base.Bot.HasInMonstersZone(30342076, false, false, false) && base.Bot.HasInGraveyard(69272449))
			{
				ClientCard target3 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(69272449));
				if (target3 == null)
				{
					return false;
				}
				base.AI.SelectCard(target3);
				this.ssMarchHare = true;
				return this.DontSelfNG();
			}
			else if (base.Bot.HasInMonstersZone(30342076, false, false, false) && base.Bot.HasInGraveyard(96676583))
			{
				ClientCard target4 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(96676583));
				if (target4 == null)
				{
					return false;
				}
				base.AI.SelectCard(target4);
				this.ssMarchHare = true;
				return this.DontSelfNG();
			}
			else
			{
				if (!base.Bot.HasInMonstersZone(24842059, false, false, false) || !base.Bot.HasInGraveyard(20938824))
				{
					return false;
				}
				ClientCard target5 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(20938824));
				if (target5 == null)
				{
					return false;
				}
				base.AI.SelectCard(target5);
				this.ssMarchHare = true;
				return this.DontSelfNG();
			}
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00090F28 File Offset: 0x0008F128
		private bool returnFromBanish()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			ClientCard mh = base.Bot.Banished.GetFirstMatchingCard((ClientCard c) => c.IsFaceup() && c.IsCode(20938824));
			if (mh == null)
			{
				return false;
			}
			base.AI.SelectCard(mh);
			this.ActiveMarchHare = true;
			return true;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00090FA4 File Offset: 0x0008F1A4
		private bool WB_OnSummon_BanishGY()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			List<ClientCard> picks = new List<ClientCard>();
			if (base.Duel.Player == 0)
			{
				int freeMMZ = Math.Max(0, 5 - this.GetMMZCount());
				bool canUseOwnPawn = base.Bot.Graveyard.GetMatchingCards((ClientCard g) => this.IsMalissPawn(g)).Count > 1;
				if (!this.ActiveMarchHare && canUseOwnPawn)
				{
					ClientCard mh = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(20938824));
					if (mh != null)
					{
						picks.Add(mh);
					}
				}
				if (freeMMZ > 0 && !this.ssRRThisTurn && !this.Allied_End)
				{
					ClientCard rr = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(68059897));
					if (rr != null && !picks.Contains(rr) && !this.ShouldSkipBanishing(rr))
					{
						picks.Add(rr);
						freeMMZ--;
					}
				}
				if (freeMMZ > 0 && !this.ssHCThisTurn && !this.Allied_End)
				{
					ClientCard hc = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(21848500));
					if (hc != null && !picks.Contains(hc) && !this.ShouldSkipBanishing(hc))
					{
						picks.Add(hc);
						freeMMZ--;
					}
				}
				if (freeMMZ > 0 && !this.ssWhiteRabbit && !this.Allied_End && canUseOwnPawn)
				{
					ClientCard wr = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(69272449));
					if (wr != null && !picks.Contains(wr) && !this.ShouldSkipBanishing(wr))
					{
						picks.Add(wr);
						freeMMZ--;
					}
				}
				if (freeMMZ > 0 && !this.ssDormouse && !this.Allied_End && canUseOwnPawn)
				{
					ClientCard dm = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(32061192));
					if (dm != null && !picks.Contains(dm) && !this.ShouldSkipBanishing(dm))
					{
						picks.Add(dm);
						freeMMZ--;
					}
				}
				if (freeMMZ > 0 && !this.ssChessyCat && !this.Allied_End && canUseOwnPawn)
				{
					ClientCard cc = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(96676583));
					if (cc != null && !picks.Contains(cc) && !this.ShouldSkipBanishing(cc))
					{
						picks.Add(cc);
						freeMMZ--;
					}
				}
				if (freeMMZ >= 0 && canUseOwnPawn)
				{
					ClientCard target = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(96676583) || g.IsCode(32061192));
					picks.Add(target);
				}
				picks = picks.Where((ClientCard c) => c != null).Distinct<ClientCard>().Take(3)
					.ToList<ClientCard>();
				using (List<ClientCard>.Enumerator enumerator = this.PickEnemyGYThreats(3 - picks.Count).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientCard t = enumerator.Current;
						if (picks.Count >= 3)
						{
							break;
						}
						if (!picks.Contains(t))
						{
							picks.Add(t);
						}
					}
					goto IL_05A3;
				}
			}
			int freeMMZ2 = Math.Max(0, 5 - this.GetMMZCount());
			if (!base.Bot.HasInSpellZone(94722358, false, false) && freeMMZ2 > 0 && !this.ssWhiteRabbit)
			{
				ClientCard wr2 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(69272449));
				if (wr2 != null)
				{
					picks.Add(wr2);
					freeMMZ2--;
				}
			}
			if (picks.Count >= 0 && !base.Bot.HasInHand(20938824) && !this.ActiveMarchHare)
			{
				ClientCard mh2 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(20938824));
				if (mh2 != null)
				{
					picks.Add(mh2);
				}
			}
			if (freeMMZ2 > 0 && !this.ssRRThisTurn)
			{
				ClientCard rr2 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(68059897));
				if (rr2 != null && !picks.Contains(rr2) && !this.ShouldSkipBanishing(rr2))
				{
					picks.Add(rr2);
					freeMMZ2--;
				}
			}
			if (freeMMZ2 > 0 && !this.ssHCThisTurn)
			{
				ClientCard hc2 = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard g) => g.IsCode(21848500));
				if (hc2 != null && !picks.Contains(hc2) && !this.ShouldSkipBanishing(hc2))
				{
					picks.Add(hc2);
					freeMMZ2--;
				}
			}
			foreach (ClientCard t2 in this.PickEnemyGYThreats(3 - picks.Count))
			{
				if (picks.Count >= 3)
				{
					break;
				}
				if (!picks.Contains(t2))
				{
					picks.Add(t2);
				}
			}
			IL_05A3:
			if (picks.Count == 0)
			{
				return false;
			}
			if (picks.Count > 3)
			{
				picks = picks.Take(3).ToList<ClientCard>();
			}
			if (picks.Count < 3)
			{
				foreach (ClientCard t3 in this.PickEnemyGYThreats(3 - picks.Count))
				{
					if (!picks.Contains(t3))
					{
						picks.Add(t3);
					}
				}
			}
			base.AI.SelectCard(picks);
			return true;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00091600 File Offset: 0x0008F800
		private bool IsMalissPawn(ClientCard c)
		{
			return c != null && (c.IsCode(96676583) || c.IsCode(69272449) || c.IsCode(32061192) || c.IsCode(20938824));
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0009163C File Offset: 0x0008F83C
		private List<ClientCard> PickEnemyGYThreats(int need)
		{
			List<ClientCard> result = new List<ClientCard>();
			if (need <= 0)
			{
				return result;
			}
			foreach (ClientCard c in this.GetDangerousCardinEnemyGrave(false))
			{
				if (result.Count >= need)
				{
					break;
				}
				if (!result.Contains(c))
				{
					result.Add(c);
				}
			}
			if (result.Count < need)
			{
				int i = base.Enemy.Graveyard.Count - 1;
				while (i >= 0 && result.Count < need)
				{
					ClientCard c2 = base.Enemy.Graveyard[i];
					if (c2 != null && !result.Contains(c2))
					{
						result.Add(c2);
					}
					i--;
				}
			}
			if (result.Count < need)
			{
				foreach (ClientCard c3 in base.Enemy.Graveyard)
				{
					if (result.Count >= need)
					{
						break;
					}
					if (!result.Contains(c3))
					{
						result.Add(c3);
					}
				}
			}
			return result;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00091770 File Offset: 0x0008F970
		private bool WB_OnBanished_SelfSS()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			if (this.GetMMZCount() >= 5)
			{
				return false;
			}
			if (base.Bot.LifePoints <= 900)
			{
				return false;
			}
			base.AI.SelectYesNo(true);
			this.ssWBThisTurn = true;
			return true;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000917D8 File Offset: 0x0008F9D8
		private int PickMalissTrapForWB()
		{
			if (this.CheckRemainInDeck(20726052) > 0 || base.Bot.HasInGraveyard(20726052))
			{
				return 20726052;
			}
			if (this.CheckRemainInDeck(94722358) > 0 || base.Bot.HasInGraveyard(94722358))
			{
				return 94722358;
			}
			return 0;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00091834 File Offset: 0x0008FA34
		private bool WB_SetMalissTrap()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.ActivateDescription != base.Util.GetStringId(95454996, 1))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			this.SelectSafeSTZoneAwayFromImperm();
			return true;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00091888 File Offset: 0x0008FA88
		private List<ClientCard> PickLinkMatsMinCount(int targetLink, Func<ClientCard, bool> isEligible, int minCount, int maxCount, IEnumerable<int> avoidIds = null, bool requireMaliss = false)
		{
			List<ClientCard> all = (from m in base.Bot.GetMonsters()
				where m != null && m.IsFaceup() && isEligible(m)
				select m).ToList<ClientCard>();
			if (all.Count < minCount)
			{
				return new List<ClientCard>();
			}
			HashSet<int> avoid = new HashSet<int>(avoidIds ?? Enumerable.Empty<int>());
			Func<ClientCard, int> <>9__5;
			Func<IEnumerable<ClientCard>, List<ClientCard>> OrderForFirst = delegate(IEnumerable<ClientCard> src)
			{
				IOrderedEnumerable<ClientCard> orderedEnumerable = src.OrderByDescending(new Func<ClientCard, int>(this.LinkValOf));
				Func<ClientCard, int> func;
				if ((func = <>9__5) == null)
				{
					func = (<>9__5 = delegate(ClientCard m)
					{
						if (!avoid.Contains(m.Id))
						{
							return 0;
						}
						return 1;
					});
				}
				return orderedEnumerable.ThenBy(func).ThenBy((ClientCard m) => m.Attack).ToList<ClientCard>();
			};
			Func<ClientCard, int> <>9__7;
			Func<ClientCard, int> <>9__9;
			Func<IEnumerable<ClientCard>, List<ClientCard>> OrderForLater = delegate(IEnumerable<ClientCard> src)
			{
				Func<ClientCard, int> func2;
				if ((func2 = <>9__7) == null)
				{
					func2 = (<>9__7 = delegate(ClientCard m)
					{
						if (!this.IsOneVal(m))
						{
							return 1;
						}
						return 0;
					});
				}
				IOrderedEnumerable<ClientCard> orderedEnumerable2 = src.OrderBy(func2).ThenBy(delegate(ClientCard m)
				{
					if (!m.HasType(CardType.Link))
					{
						return -1;
					}
					if (Math.Max(1, m.LinkCount) != 1)
					{
						return 1;
					}
					return 0;
				}).ThenBy(new Func<ClientCard, int>(this.LinkValOf));
				Func<ClientCard, int> func3;
				if ((func3 = <>9__9) == null)
				{
					func3 = (<>9__9 = delegate(ClientCard m)
					{
						if (!avoid.Contains(m.Id))
						{
							return 0;
						}
						return 1;
					});
				}
				return orderedEnumerable2.ThenBy(func3).ThenBy((ClientCard m) => m.Attack).ToList<ClientCard>();
			};
			List<ClientCard> poolPreferred = all.Where((ClientCard m) => !avoid.Contains(m.Id)).ToList<ClientCard>();
			List<ClientCard> poolFallback = all.ToList<ClientCard>();
			int firstMaxAllowed = targetLink - Math.Max(0, minCount - 1);
			Func<List<ClientCard>, List<ClientCard>> TryPick = delegate(List<ClientCard> pool)
			{
				List<ClientCard> chosen = new List<ClientCard>();
				Func<List<ClientCard>, int, bool> Dfs = null;
				Dfs = delegate(List<ClientCard> avail, int sum)
				{
					if (sum > targetLink || chosen.Count > maxCount)
					{
						return false;
					}
					if (chosen.Count >= minCount && sum == targetLink)
					{
						return (!requireMaliss || chosen.Any(new Func<ClientCard, bool>(this.IsMalissBody))) && this.Util.GetBotAvailZonesFromExtraDeck(chosen) != 0;
					}
					int need = Math.Max(0, minCount - chosen.Count);
					if (sum + need > targetLink)
					{
						return false;
					}
					List<ClientCard> ordered = ((chosen.Count == 0) ? OrderForFirst(avail) : OrderForLater(avail));
					int i;
					int i2;
					for (i = 0; i < ordered.Count; i = i2 + 1)
					{
						ClientCard j = ordered[i];
						List<ClientCard> nextAvail = ordered.Where((ClientCard x, int idx) => idx != i).ToList<ClientCard>();
						int lv = this.LinkValOf(j);
						int[] array;
						if (chosen.Count != 0)
						{
							if (lv <= 1)
							{
								(array = new int[1])[0] = 1;
							}
							else
							{
								int[] array2 = new int[2];
								array2[0] = 1;
								array = array2;
								array2[1] = lv;
							}
						}
						else if (lv <= 1)
						{
							(array = new int[1])[0] = 1;
						}
						else
						{
							int[] array3 = new int[2];
							array3[0] = Math.Min(lv, firstMaxAllowed);
							array = array3;
							array3[1] = 1;
						}
						foreach (int v in array.Distinct<int>())
						{
							int newSum = sum + v;
							if (newSum <= targetLink)
							{
								int remMin = Math.Max(0, minCount - (chosen.Count + 1));
								if (newSum + remMin <= targetLink)
								{
									chosen.Add(j);
									if (Dfs(nextAvail, newSum))
									{
										return true;
									}
									chosen.RemoveAt(chosen.Count - 1);
								}
							}
						}
						i2 = i;
					}
					return false;
				};
				if (Dfs(OrderForFirst(pool), 0))
				{
					return chosen;
				}
				return null;
			};
			List<ClientCard> pick = TryPick(poolPreferred);
			if (pick != null && pick.Count > 0)
			{
				return pick;
			}
			pick = TryPick(poolFallback);
			return pick ?? new List<ClientCard>();
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000919B0 File Offset: 0x0008FBB0
		private bool Link_Apo()
		{
			if (!base.Bot.HasInMonstersZone(68059897, false, false, false) || !base.Bot.HasInMonstersZone(30342076, false, false, false))
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(4, (ClientCard m) => m.HasType(CardType.Monster), 2, 2, new int[] { 46947713, 39138610, 86066372 }, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this.blockWicckid = true;
			return true;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00091A44 File Offset: 0x0008FC44
		private bool Flow3_Link_Firewall()
		{
			if (!base.Bot.HasInMonstersZone(21848500, false, false, false) || !base.Bot.HasInMonstersZone(30342076, false, false, false) || !base.Bot.HasInMonstersZone(52698008, false, false, false))
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(4, (ClientCard m) => m.HasType(CardType.Monster), 2, 2, new int[] { 52698008, 4280258, 46947713, 39138610, 86066372 }, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00091AE8 File Offset: 0x0008FCE8
		private bool Step_LinkSummon_HeartsCrypter()
		{
			if (base.Bot.HasInMonstersZone(95454996, false, false, false) && base.Bot.HasInMonstersZone(68059897, false, false, false) && base.Bot.HasInMonstersZone(4280258, false, false, false) && base.Bot.GetMonsterCount() < 5)
			{
				return false;
			}
			List<ClientCard> cand = (from c in base.Bot.GetMonsters()
				where c != null && c.IsFaceup() && c.HasType(CardType.Effect)
				select c).ToList<ClientCard>();
			if (cand.Count < 3)
			{
				return false;
			}
			HashSet<int> avoid = new HashSet<int> { 52698008, 4280258, 39138610, 86066372, 5043010, 46947713 };
			List<ClientCard> ordered = cand.OrderBy(delegate(ClientCard m)
			{
				if (!avoid.Contains(m.Id))
				{
					return 0;
				}
				return 2;
			}).ThenBy(delegate(ClientCard m)
			{
				if (!m.HasType(CardType.Link))
				{
					return 0;
				}
				return 1;
			}).ThenBy((ClientCard m) => m.Attack)
				.ToList<ClientCard>();
			List<ClientCard> mats = ordered.Take(3).ToList<ClientCard>();
			if (!mats.Any(new Func<ClientCard, bool>(this.IsMaliss)))
			{
				ClientCard maliss = ordered.FirstOrDefault(new Func<ClientCard, bool>(this.IsMaliss));
				if (maliss == null)
				{
					return false;
				}
				mats[2] = maliss;
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck(mats) == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00091CA0 File Offset: 0x0008FEA0
		private bool HC_Quick_ReturnBanished_AndBanishField()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.ActivateDescription != base.Util.GetStringId(21848500, 0))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			this.RefreshNoChainWindows();
			List<ClientCard> banishedMaliss = this.GetBanishedMaliss();
			if (banishedMaliss.Count == 0)
			{
				return false;
			}
			bool haveReturn = banishedMaliss.Count > 0;
			bool mustNow = this.GetProblematicEnemyCardList(true, false, (CardType)7).Count > 0;
			if (base.Duel.Player == 0)
			{
				if (!haveReturn && !mustNow)
				{
					return false;
				}
			}
			else if (!mustNow && !this.IsPreferredRemovalTiming())
			{
				return false;
			}
			if (!haveReturn)
			{
				return false;
			}
			ClientCard ret = this.PickBanishedMalissForHC(banishedMaliss);
			if (ret == null)
			{
				return false;
			}
			List<ClientCard> fieldTargets = this.GetProblematicEnemyCardList(true, false, (CardType)7);
			if (fieldTargets.Count == 0)
			{
				ClientCard any = this.GetBestEnemyCard(false, true, false);
				if (any != null)
				{
					fieldTargets.Add(any);
				}
			}
			if (fieldTargets.Count == 0)
			{
				return false;
			}
			base.AI.SelectCard(ret);
			base.AI.SelectNextCard(fieldTargets);
			this.ConsumePreferredWindow();
			return this.DontSelfNG();
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00091DBD File Offset: 0x0008FFBD
		private List<ClientCard> GetBanishedMaliss()
		{
			return base.Bot.Banished.GetMatchingCards((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(447)).ToList<ClientCard>();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00091DF4 File Offset: 0x0008FFF4
		private ClientCard PickBanishedMalissForHC(List<ClientCard> cand)
		{
			cand = cand.Where((ClientCard c) => !c.IsCode(95454996) && !c.IsCode(68059897)).ToList<ClientCard>();
			if (cand.Count == 0)
			{
				return null;
			}
			return cand.OrderByDescending(new Func<ClientCard, int>(this.ScoreForBanishedMaliss)).First<ClientCard>();
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00091E50 File Offset: 0x00090050
		private bool HC_OnBanished_SpecialSummon()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			if (this.GetMMZCount() >= 5)
			{
				return false;
			}
			if (base.Bot.LifePoints <= 900)
			{
				return false;
			}
			base.AI.SelectYesNo(true);
			this.ssHCThisTurn = true;
			return true;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00091EB6 File Offset: 0x000900B6
		private bool HasMalissLinkFaceup()
		{
			return base.Bot.GetMonsters().Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(447) && c.HasType(CardType.Link));
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00091EE8 File Offset: 0x000900E8
		private bool CanReachAlliedNow()
		{
			return this.PickLinkMatsMinCount(5, (ClientCard m) => m.HasType(CardType.Effect), 3, 5, new int[] { 4280258, 5043010, 86066372 }, false).Count > 0;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00091F38 File Offset: 0x00090138
		private bool Flow3_Link_Allied()
		{
			if (!base.Bot.HasInMonstersZoneOrInGraveyard(68059897) || !base.Bot.HasInMonstersZoneOrInGraveyard(95454996))
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(5, (ClientCard m) => m.HasType(CardType.Effect), 3, 5, new int[] { 4280258, 5043010, 86066372 }, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this.Allied_End = true;
			return true;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00091FC8 File Offset: 0x000901C8
		private bool Emer_Allied()
		{
			if (!this.enemyActivateLancea)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(46947713, false, false, false) || !base.Bot.HasInMonstersZone(59859086, false, false, false))
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(5, (ClientCard m) => m.HasType(CardType.Effect), 3, 3, null, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this.Allied_End = true;
			return true;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00092058 File Offset: 0x00090258
		private bool Emer_Allied2()
		{
			List<ClientCard> myMonsters = (from m in base.Bot.GetMonsters()
				where m != null
				select m).ToList<ClientCard>();
			if (myMonsters.Count != 3)
			{
				return false;
			}
			if (myMonsters.Where((ClientCard m) => m.HasType(CardType.Link) && m.LinkCount == 3).ToList<ClientCard>().Count != 1)
			{
				return false;
			}
			if (!base.Bot.HasInGraveyard(68059897) && !base.Bot.HasInGraveyard(95454996) && !base.Bot.HasInGraveyard(46947713))
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(5, (ClientCard m) => m.HasType(CardType.Effect), 3, 3, null, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this.Allied_End = true;
			return true;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00092160 File Offset: 0x00090360
		private ClientCard FindGWC06TargetByOrder(params int[] ids)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				int id = ids[i];
				ClientCard gy = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(id));
				if (gy != null)
				{
					return gy;
				}
				ClientCard ban = base.Bot.Banished.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(id) && c.IsFaceup());
				if (ban != null)
				{
					return ban;
				}
			}
			return null;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000921D4 File Offset: 0x000903D4
		private int PickGWC06CostCandidateId()
		{
			if (base.Bot.HasInMonstersZone(95454996, false, false, false) && !this.ssWBThisTurn)
			{
				return 95454996;
			}
			if (base.Bot.HasInMonstersZone(68059897, false, false, false) && !this.ssRRThisTurn)
			{
				return 68059897;
			}
			int card = this.PickTB11CostCandidateId();
			if (card != 0)
			{
				return card;
			}
			return 0;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00092234 File Offset: 0x00090434
		private ClientCard PickGWC06TargetExtend()
		{
			if (base.Duel.Turn > 2)
			{
				return this.FindGWC06TargetByOrder(new int[] { 21848500, 68059897, 95454996 });
			}
			return this.FindGWC06TargetByOrder(new int[] { 95454996, 68059897, 21848500, 69272449, 32061192, 96676583, 20938824 });
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00092274 File Offset: 0x00090474
		private bool GWC06_MyTurn_Extend()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.Duel.Player == 0 && this.GetMMZCount() >= 4 && base.Bot.HasInMonstersZone(5043010, false, false, false) && (base.Bot.HasInMonstersZoneOrInGraveyard(68059897) || base.Bot.HasInMonstersZoneOrInGraveyard(95454996) || base.Bot.HasInMonstersZoneOrInGraveyard(21848500)))
			{
				return false;
			}
			if (base.Duel.Player != 0)
			{
				return false;
			}
			if (base.Duel.Phase != DuelPhase.Main1 && base.Duel.Phase != DuelPhase.Main2)
			{
				return false;
			}
			if (this.CheckSpellWillBeNegate(false, null))
			{
				return false;
			}
			if (this.GetMMZCount() >= 5)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(95454996, false, false, false) && (!base.Bot.HasInMonstersZone(68059897, false, false, false) || base.Bot.GetMonsterCount() != 1))
			{
				return false;
			}
			ClientCard target = this.PickGWC06TargetExtend();
			if (target == null)
			{
				return false;
			}
			if (!this.gwc06SetThisTurn)
			{
				base.AI.SelectCard(target);
				return this.DontSelfNG();
			}
			int costId = this.PickGWC06CostCandidateId();
			if (costId == 0)
			{
				return false;
			}
			base.AI.SelectCard(costId);
			base.AI.SelectNextCard(target);
			return this.DontSelfNG();
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000923CC File Offset: 0x000905CC
		private bool GWC06_OppTurn_ReviveWB_HC()
		{
			if (base.Duel.Player != 1)
			{
				return false;
			}
			if (!base.Bot.HasInSpellZone(20726052, false, false))
			{
				return false;
			}
			if (this.CheckSpellWillBeNegate(false, null))
			{
				return false;
			}
			if (this.GetMMZCount() >= 5)
			{
				return false;
			}
			ClientCard target = this.FindGWC06TargetByOrder(new int[] { 95454996, 21848500 });
			if (target == null)
			{
				return false;
			}
			if (!this.gwc06SetThisTurn)
			{
				base.AI.SelectCard(target);
				return this.DontSelfNG();
			}
			int costId = this.PickGWC06CostCandidateId();
			if (costId == 0)
			{
				return false;
			}
			base.AI.SelectCard(costId);
			base.AI.SelectNextCard(target);
			return this.DontSelfNG();
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00092480 File Offset: 0x00090680
		private static int FirstBit(int mask)
		{
			for (int i = 0; i < 32; i++)
			{
				int b = 1 << i;
				if ((mask & b) != 0)
				{
					return b;
				}
			}
			return 0;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000924AC File Offset: 0x000906AC
		private static int FirstBitFromOrder(int mask, int[] order)
		{
			foreach (int b in order)
			{
				if ((mask & b) != 0)
				{
					return b;
				}
			}
			return MalissExecutor.FirstBit(mask);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000924DC File Offset: 0x000906DC
		private int PreferSafeSummonZones(int available)
		{
			int MAIN_MASK = 31;
			int emzMask = available & ~MAIN_MASK;
			if (emzMask != 0)
			{
				return MalissExecutor.FirstBit(emzMask);
			}
			int enemyPointed = 0;
			try
			{
				enemyPointed = base.Enemy.GetLinkedZones();
			}
			catch
			{
			}
			int safeMain = available & MAIN_MASK & ~enemyPointed;
			if (safeMain != 0)
			{
				return MalissExecutor.FirstBitFromOrder(safeMain, new int[] { 4, 2, 8, 1, 16 });
			}
			return MalissExecutor.FirstBit(available);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00092548 File Offset: 0x00090748
		private int PickMTP07CostCandidateId()
		{
			return this.PickTB11CostCandidateId();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00092550 File Offset: 0x00090750
		private ClientCard PickMTP07EnemyRemovalTarget()
		{
			List<ClientCard> list = this.GetProblematicEnemyCardList(true, false, CardType.Trap);
			if (list.Count > 0)
			{
				return list[0];
			}
			ClientCard i = this.GetBestEnemyMonster(false, true);
			if (i != null)
			{
				return i;
			}
			ClientCard s = this.GetBestEnemySpell(false, true);
			if (s != null)
			{
				return s;
			}
			ClientCard clientCard;
			if ((clientCard = base.Enemy.GetMonsters().FirstOrDefault((ClientCard c) => c != null)) == null)
			{
				clientCard = base.Enemy.GetSpells().FirstOrDefault((ClientCard c) => c != null);
			}
			return clientCard;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000925F8 File Offset: 0x000907F8
		private bool MTP07_ForMH()
		{
			if (base.Bot.GetMonsterCount() != 1 || !base.Bot.HasInMonstersZone(69272449, false, false, false))
			{
				return false;
			}
			if (this.CheckSpellWillBeNegate(false, null))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			int searchId = this.PickMTP07SearchId();
			if (searchId == 0)
			{
				return false;
			}
			if (this.mtp07SetThisTurn)
			{
				int costId = this.PickMTP07CostCandidateId();
				if (costId == 0)
				{
					return false;
				}
				base.AI.SelectCard(costId);
			}
			base.AI.SelectNextCard(searchId);
			return this.DontSelfNG();
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00092684 File Offset: 0x00090884
		private bool MTP07_OppTurn_RemoveEnemyOnly()
		{
			if (base.Duel.Player != 1)
			{
				return false;
			}
			if (!this.HasMalissLinkFaceup())
			{
				return false;
			}
			if (this.CheckSpellWillBeNegate(false, null))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			List<ClientCard> urgent = this.GetProblematicEnemyCardList(true, false, CardType.Trap);
			if (urgent.Count == 0 && !this.IsPreferredRemovalTiming())
			{
				return false;
			}
			bool preBattle = base.Duel.Phase == DuelPhase.Main1 && base.Enemy.GetMonsterCount() > 0;
			if (urgent.Count == 0 && (!this.IsPreferredRemovalTiming() && !preBattle))
			{
				return false;
			}
			int searchId = this.PickMTP07SearchId();
			if (searchId == 0)
			{
				return false;
			}
			ClientCard target;
			if (urgent.Count > 0)
			{
				target = urgent[0];
			}
			else if (preBattle)
			{
				target = (from c in base.Enemy.MonsterZone
					where c != null && c.IsFaceup()
					orderby c.Attack descending
					select c).FirstOrDefault<ClientCard>();
			}
			else
			{
				target = this.PickMTP07EnemyRemovalTarget();
			}
			if (target == null)
			{
				return false;
			}
			if (!this.mtp07SetThisTurn)
			{
				base.AI.SelectCard(searchId);
				base.AI.SelectNextCard(target);
				this.ConsumePreferredWindow();
				return this.DontSelfNG();
			}
			int costId = this.PickMTP07CostCandidateId();
			if (costId == 0)
			{
				return false;
			}
			base.AI.SelectCard(costId);
			base.AI.SelectNextCard(searchId);
			base.AI.SelectNextCard(target);
			this.ConsumePreferredWindow();
			return this.DontSelfNG();
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0009280C File Offset: 0x00090A0C
		private bool Emergency_NS()
		{
			if (this.usedNormalSummon)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() != 0)
			{
				return false;
			}
			if (base.Bot.HasInHand(32061192) || base.Bot.HasInHand(69272449) || base.Bot.HasInHand(96676583) || base.Bot.HasInHand(75500286) || base.Bot.HasInHand(68337209))
			{
				return false;
			}
			if (!base.Bot.HasInHand(30118811))
			{
				return false;
			}
			this.usedNormalSummon = true;
			this.nsplan = true;
			return true;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000928B1 File Offset: 0x00090AB1
		private bool IsWicInEMZ(ClientCard wic)
		{
			return wic != null && wic.Location == CardLocation.MonsterZone && (wic.Sequence == MalissExecutor.EMZ_LEFT || wic.Sequence == MalissExecutor.EMZ_RIGHT);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000928DD File Offset: 0x00090ADD
		private IEnumerable<int> GetWicDownSeq(ClientCard wic)
		{
			if (!this.IsWicInEMZ(wic))
			{
				yield break;
			}
			if (wic.Sequence == MalissExecutor.EMZ_LEFT)
			{
				yield return 1;
				yield return 2;
			}
			if (wic.Sequence == MalissExecutor.EMZ_RIGHT)
			{
				yield return 3;
				yield return 4;
			}
			yield break;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000928F4 File Offset: 0x00090AF4
		private bool IsUnderWic(ClientCard wic, ClientCard m)
		{
			return wic != null && m != null && m.Controller == wic.Controller && m.Location == CardLocation.MonsterZone && m.Sequence >= 0 && m.Sequence <= 4 && this.GetWicDownSeq(wic).Contains(m.Sequence);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0009294C File Offset: 0x00090B4C
		private ClientCard PickUnderlingForTranscode(ClientCard wic, IList<ClientCard> pool)
		{
			if (!this.IsWicInEMZ(wic))
			{
				return null;
			}
			List<ClientCard> underlings = pool.Where((ClientCard x) => x != null && x != wic && x.IsFaceup() && x.Location == CardLocation.MonsterZone && x.Controller == wic.Controller && x.Sequence >= 0 && x.Sequence <= 4 && this.IsUnderWic(wic, x) && (x.HasType(CardType.Link) || x.HasType(CardType.Effect)) && !x.HasType(CardType.Token)).ToList<ClientCard>();
			if (underlings.Count == 0)
			{
				return null;
			}
			ClientCard preferLD = underlings.FirstOrDefault((ClientCard x) => x.IsCode(30342076));
			if (preferLD != null)
			{
				return preferLD;
			}
			return underlings.OrderByDescending(delegate(ClientCard x)
			{
				if (!x.HasType(CardType.Link))
				{
					return 1;
				}
				return 2;
			}).ThenByDescending((ClientCard x) => x.Attack).FirstOrDefault<ClientCard>();
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00092A18 File Offset: 0x00090C18
		private bool SummonTranscode()
		{
			if (!this.enemyActivateLancea)
			{
				return false;
			}
			ClientCard wic = base.Bot.GetMonsters().FirstOrDefault((ClientCard x) => x != null && x.IsFaceup() && x.IsCode(52698008));
			if (!this.IsWicInEMZ(wic))
			{
				return false;
			}
			ClientCard under = this.PickUnderlingForTranscode(wic, base.Bot.GetMonsters());
			if (under == null)
			{
				return false;
			}
			this.SelectLinkMaterialsPair(wic, under);
			int emz = this.EmzBitFor(wic);
			if (emz != 0)
			{
				base.AI.SelectPlace(emz);
			}
			return true;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00092AA4 File Offset: 0x00090CA4
		private void SelectLinkMaterialsPair(ClientCard a, ClientCard b)
		{
			try
			{
				List<ClientCard> mats = new List<ClientCard> { a, b };
				base.AI.SelectMaterials(mats, 0);
				return;
			}
			catch
			{
			}
			base.AI.SelectCard(a);
			base.AI.SelectNextCard(b);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00092B00 File Offset: 0x00090D00
		private int EmzBitFor(ClientCard link)
		{
			if (link == null || link.Location != CardLocation.MonsterZone)
			{
				return 0;
			}
			if (link.Sequence == MalissExecutor.EMZ_LEFT)
			{
				return 1 << MalissExecutor.EMZ_LEFT;
			}
			if (link.Sequence == MalissExecutor.EMZ_RIGHT)
			{
				return 1 << MalissExecutor.EMZ_RIGHT;
			}
			return 0;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00092B4C File Offset: 0x00090D4C
		private int ChooseAndRememberWicckidEmz(int available)
		{
			int emzAvail = available & 96;
			if (emzAvail == 0)
			{
				return 0;
			}
			int best = 0;
			int bestScore = int.MinValue;
			foreach (int emz in new int[] { 32, 64 })
			{
				if ((emzAvail & emz) != 0)
				{
					int score = 0;
					int num = this.DownBitOfEmz(emz);
					if (num == 2 && this.IsMainFreeSeq(1))
					{
						score += 10;
					}
					if (num == 8 && this.IsMainFreeSeq(3))
					{
						score += 10;
					}
					if (score > bestScore)
					{
						bestScore = score;
						best = emz;
					}
				}
			}
			if (best == 0)
			{
				best = (((emzAvail & 32) != 0) ? 32 : 64);
			}
			this._wicckidEmzBit = best;
			return best;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00092BF0 File Offset: 0x00090DF0
		private bool IsMainFreeSeq(int seq)
		{
			return !base.Bot.MonsterZone.GetMonsters().Any((ClientCard m) => m != null && m.Controller == 0 && m.Sequence == seq);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00092C2E File Offset: 0x00090E2E
		private int DownBitOfEmz(int emzBit)
		{
			if (emzBit == 32)
			{
				return 2;
			}
			if (emzBit == 64)
			{
				return 8;
			}
			return 0;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00092C3F File Offset: 0x00090E3F
		private bool IsPreferredRemovalTiming()
		{
			return base.Duel.Player == 1 && (this._prefWindowTTL > 0 || (this._oppJustActivatedPersistentSpell || this._oppJustSummoned || this._oppJustSet));
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00092C77 File Offset: 0x00090E77
		private void ConsumePreferredWindow()
		{
			this._prefWindowTTL = 0;
			this._oppJustActivatedPersistentSpell = false;
			this._oppJustSummoned = false;
			this._oppJustSet = false;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00092C98 File Offset: 0x00090E98
		private void RefreshNoChainWindows()
		{
			bool oppMain = base.Duel.Player == 1 && (base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2);
			int curMon = base.Enemy.GetMonsterCount();
			if (curMon > this._enemyMonsterCountSnap)
			{
				this._oppJustSummoned = true;
				if (oppMain)
				{
					this._prefWindowTTL = Math.Max(this._prefWindowTTL, 2);
				}
			}
			this._enemyMonsterCountSnap = curMon;
			int curFD = base.Enemy.SpellZone.Count((ClientCard c) => c != null && c.IsFacedown());
			if (curFD > this._enemyFacedownSTSnap)
			{
				this._oppJustSet = true;
				if (oppMain)
				{
					this._prefWindowTTL = Math.Max(this._prefWindowTTL, 2);
				}
			}
			this._enemyFacedownSTSnap = curFD;
			if (oppMain)
			{
				if (this._prefWindowTTL > 0)
				{
					this._prefWindowTTL--;
				}
				if (this._prefWindowTTL == 0)
				{
					this._oppJustActivatedPersistentSpell = false;
					this._oppJustSummoned = false;
					this._oppJustSet = false;
					return;
				}
			}
			else
			{
				this._prefWindowTTL = 0;
				this._oppJustActivatedPersistentSpell = false;
				this._oppJustSummoned = false;
				this._oppJustSet = false;
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00092DC4 File Offset: 0x00090FC4
		private bool FirewallBounce_OnOppSummon()
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(base.Card.Id))
			{
				return false;
			}
			if (base.ActivateDescription != base.Util.GetStringId(5043010, 0))
			{
				return false;
			}
			if (base.Duel.LastSummonPlayer != 1)
			{
				return false;
			}
			List<ClientCard> picks = new List<ClientCard>();
			List<ClientCard> negateList = this.GetMonsterListForTargetNegate(false, (CardType)0);
			if (negateList != null)
			{
				foreach (ClientCard c in negateList)
				{
					if (c != null && c.Controller == 1 && c.IsMonster() && c.IsFaceup() && !picks.Contains(c))
					{
						picks.Add(c);
					}
				}
			}
			foreach (ClientCard i in from x in base.Enemy.GetMonsters()
				orderby x.Attack descending
				select x)
			{
				if (i != null && i.IsMonster() && i.IsFaceup() && !picks.Contains(i))
				{
					picks.Add(i);
				}
			}
			if (picks.Count == 0)
			{
				return false;
			}
			base.AI.SelectCard(picks);
			return true;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00092F2C File Offset: 0x0009112C
		private bool Accesscode_OnSummon_AtkUp()
		{
			List<ClientCard> list = new List<ClientCard>();
			int[] array = new int[] { 21848500, 95454996, 68059897, 46947713 };
			for (int i = 0; i < array.Length; i++)
			{
				int id = array[i];
				ClientCard hit = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(id));
				if (hit != null)
				{
					list.Add(hit);
				}
			}
			base.AI.SelectCard(list);
			return true;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00092FA8 File Offset: 0x000911A8
		private bool Accesscode_Destroy_Ignition()
		{
			if (base.ActivateDescription != base.Util.GetStringId(86066372, 1))
			{
				return false;
			}
			if (base.Enemy.GetFieldCount() == 0)
			{
				return false;
			}
			IEnumerable<ClientCard> enumerable = base.Bot.GetGraveyardMonsters();
			IEnumerable<ClientCard> enumerable2 = (enumerable ?? Enumerable.Empty<ClientCard>()).Where((ClientCard c) => c.HasType(CardType.Link));
			enumerable = base.Bot.GetMonsters();
			List<ClientCard> costList = (from c in enumerable2.Concat((enumerable ?? Enumerable.Empty<ClientCard>()).Where((ClientCard c) => c.HasType(CardType.Link) && c != base.Card))
				orderby c.Location != CardLocation.Grave, c.LinkCount, c.Attack
				select c).ToList<ClientCard>();
			if (costList.Count == 0)
			{
				return false;
			}
			List<ClientCard> targets = new List<ClientCard>();
			targets.AddRange(from s in base.Enemy.GetSpells()
				where s != null && s.IsFacedown()
				select s);
			targets.AddRange(from s in base.Enemy.GetSpells()
				where s != null && !s.IsFacedown()
				select s);
			targets.AddRange(from m in base.Enemy.GetMonsters()
				orderby m.Attack descending
				select m);
			if (targets.Count == 0)
			{
				return false;
			}
			base.AI.SelectCard(costList);
			base.AI.SelectNextCard(targets);
			return true;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0009318C File Offset: 0x0009138C
		private bool AlreadySSFromBanishThisTurn(ClientCard c)
		{
			if (c == null)
			{
				return false;
			}
			if (c.Controller != 0)
			{
				return false;
			}
			if (c.IsCode(32061192))
			{
				return this.ssDormouse;
			}
			if (c.IsCode(69272449))
			{
				return this.ssWhiteRabbit;
			}
			if (c.IsCode(96676583))
			{
				return this.ssChessyCat;
			}
			if (c.IsCode(68059897))
			{
				return this.ssRRThisTurn;
			}
			if (c.IsCode(21848500))
			{
				return this.ssHCThisTurn;
			}
			return c.IsCode(95454996) && this.ssWBThisTurn;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00093221 File Offset: 0x00091421
		private bool ShouldSkipBanishing(ClientCard c)
		{
			return c == null || (c.Controller == 0 && (this.AlreadySSFromBanishThisTurn(c) || (c.IsCode(20938824) && this.ActiveMarchHare)));
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00093258 File Offset: 0x00091458
		private bool DescIs(int cardId, params int[] idx)
		{
			if (base.ActivateDescription == -1)
			{
				return true;
			}
			foreach (int i in idx)
			{
				if (base.ActivateDescription == base.Util.GetStringId(cardId, i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0009329C File Offset: 0x0009149C
		private ClientCard PickMirrorCostCandidate()
		{
			int[] array = new int[] { 69272449, 96676583, 32061192, 20938824 };
			for (int i = 0; i < array.Length; i++)
			{
				int id = array[i];
				ClientCard h = base.Bot.Hand.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(id));
				if (h != null)
				{
					return h;
				}
			}
			ClientCard fieldP = (from c in base.Bot.GetMonsters()
				where c != null && c.HasSetcode(447) && !c.HasType(CardType.Link)
				orderby c.Attack
				select c).FirstOrDefault<ClientCard>();
			if (fieldP != null)
			{
				return fieldP;
			}
			int[] avoid = new int[] { 21848500, 95454996, 68059897 };
			return (from c in base.Bot.GetMonsters()
				where c != null && c.HasSetcode(447) && c.HasType(CardType.Link) && !avoid.Contains(c.Id)
				orderby c.Attack
				select c).FirstOrDefault<ClientCard>();
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000933C0 File Offset: 0x000915C0
		private int[] Mirror_SearchOrderForType(bool isTrap, bool isMon)
		{
			if (isTrap)
			{
				return new int[] { 20726052, 94722358 };
			}
			if (!isMon)
			{
				return new int[] { 20938824, 32061192, 69272449, 96676583 };
			}
			if (!base.Bot.HasInHand(69272449) && this.NSDorMouse)
			{
				return new int[] { 69272449, 20938824, 32061192, 96676583 };
			}
			return new int[] { 20938824, 32061192, 69272449, 96676583 };
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0009343C File Offset: 0x0009163C
		private ClientCard PickMirrorGYTargetForSearch()
		{
			if (this.NSDorMouse && !this.ssRRThisTurn)
			{
				ClientCard rr = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(68059897));
				if (rr != null)
				{
					return rr;
				}
			}
			int[] array = new int[] { 20938824, 32061192, 69272449, 96676583 };
			for (int j = 0; j < array.Length; j++)
			{
				int id = array[j];
				ClientCard i = base.Bot.Graveyard.GetFirstMatchingCard((ClientCard c) => c != null && c.IsCode(id));
				if (i != null && this.CheckRemainInDeck(id) > 0)
				{
					return i;
				}
			}
			return null;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000934F4 File Offset: 0x000916F4
		private int PickMTP07SearchId()
		{
			if (this.CheckRemainInDeck(20938824) > 0)
			{
				return 20938824;
			}
			foreach (int id in new int[] { 32061192, 69272449, 96676583 })
			{
				if (this.CheckRemainInDeck(id) > 0)
				{
					return id;
				}
			}
			return 0;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00093548 File Offset: 0x00091748
		private void SelectSafeSTZoneAwayFromImperm()
		{
			List<int> safeCols = (from seq in Enumerable.Range(0, 5)
				where base.Bot.SpellZone[seq] == null && !this.infiniteImpermanenceList.Contains(seq)
				select seq).ToList<int>();
			if (safeCols.Count == 0)
			{
				safeCols = (from seq in Enumerable.Range(0, 5)
					where base.Bot.SpellZone[seq] == null
					select seq).ToList<int>();
			}
			int mask = 0;
			foreach (int seq2 in safeCols)
			{
				mask |= 1 << seq2;
			}
			base.AI.SelectPlace(mask);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000935EC File Offset: 0x000917EC
		private bool T3Allow()
		{
			return base.Duel.Player == 0 && this.myTurnCount >= 2 && this.HaveBackupOrWizardInHand() && base.Bot.HasInHand(20938824);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00093628 File Offset: 0x00091828
		private bool NSBackup()
		{
			if (base.Bot.GetMonsterCount() != 0)
			{
				return false;
			}
			if (this.usedNormalSummon)
			{
				return false;
			}
			if (base.Bot.HasInHand(32061192) || base.Bot.HasInHand(69272449) || base.Bot.HasInHand(96676583) || base.Bot.HasInHand(68337209) || base.Bot.HasInHand(73628505) || base.Bot.HasInHand(75500286))
			{
				return false;
			}
			this.nsBackupplan = true;
			return true;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000936C4 File Offset: 0x000918C4
		private bool NSBackup_L()
		{
			if (base.Bot.GetMonsterCount() != 0)
			{
				return false;
			}
			if (this.usedNormalSummon)
			{
				return false;
			}
			if (base.Bot.HasInHand(32061192) || base.Bot.HasInHand(69272449) || base.Bot.HasInHand(96676583) || base.Bot.HasInHand(20938824) || base.Bot.HasInHand(68337209) || base.Bot.HasInHand(73628505) || base.Bot.HasInHand(75500286))
			{
				return false;
			}
			this.nsLanceaplan = true;
			return true;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00093774 File Offset: 0x00091974
		private bool NSMH()
		{
			return base.Bot.Hand.GetMatchingCards((ClientCard c) => c != null && c.IsCode(20938824)).Count >= 2 && base.Bot.GetMonsterCount() == 0 && !this.usedNormalSummon && !base.Bot.HasInHand(32061192) && !base.Bot.HasInHand(69272449) && !base.Bot.HasInHand(96676583) && !base.Bot.HasInHand(68337209) && !base.Bot.HasInHand(73628505) && !base.Bot.HasInHand(75500286);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00093842 File Offset: 0x00091A42
		private bool LinguribohMHLine()
		{
			return base.Bot.HasInHand(20938824) && base.Bot.HasInMonstersZone(20938824, false, false, false) && base.Bot.GetMonsterCount() == 1;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00093880 File Offset: 0x00091A80
		private bool EmerTranscode()
		{
			if (!this.enemyActivateLancea)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() < 3)
			{
				return false;
			}
			List<ClientCard> mats = this.PickLinkMatsMinCount(3, (ClientCard m) => m.HasType(CardType.Effect), 2, 2, null, false);
			if (mats.Count == 0)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x04001B49 RID: 6985
		private const int SetcodeMaliss = 447;

		// Token: 0x04001B4A RID: 6986
		private const int SetcodeTimeLord = 74;

		// Token: 0x04001B4B RID: 6987
		private const int SetcodePhantom = 219;

		// Token: 0x04001B4C RID: 6988
		private const int SetcodeOrcust = 283;

		// Token: 0x04001B4D RID: 6989
		private const int SetcodeHorus = 413;

		// Token: 0x04001B4E RID: 6990
		private const int SetcodeDarkWorld = 6;

		// Token: 0x04001B4F RID: 6991
		private const int SetcodeSkyStriker = 277;

		// Token: 0x04001B50 RID: 6992
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 96676583, 20938824, 69272449, 68337209, 30118811, 32061192, 14558127, 10045474, 40366667 }
			},
			{
				2,
				new List<int> { 23434538, 24224830 }
			},
			{
				1,
				new List<int> { 75500286, 73628505, 20726052, 34267821, 94722358, 65681983, 93453053, 3723262, 27204311 }
			}
		};

		// Token: 0x04001B51 RID: 6993
		private List<int> notToNegateIdList = new List<int> { 58699500, 20343502, 19403423 };

		// Token: 0x04001B52 RID: 6994
		private List<int> notToDestroySpellTrap = new List<int> { 50005218, 6767771 };

		// Token: 0x04001B53 RID: 6995
		private List<int> targetNegateIdList = new List<int>
		{
			97268402, 10045474, 52038441, 78474168, 74003290, 67037924, 9753964, 66192538, 23204029, 73445448,
			35103106, 30286474, 45002991, 5795980, 38511382, 53742162, 30430448
		};

		// Token: 0x04001B54 RID: 6996
		private bool usedNormalSummon;

		// Token: 0x04001B55 RID: 6997
		private bool ssDormouse;

		// Token: 0x04001B56 RID: 6998
		private bool ssWhiteRabbit;

		// Token: 0x04001B57 RID: 6999
		private bool ssChessyCat;

		// Token: 0x04001B58 RID: 7000
		private bool ssMarchHare;

		// Token: 0x04001B59 RID: 7001
		private bool ActiveMarchHare;

		// Token: 0x04001B5A RID: 7002
		private bool ssRRThisTurn;

		// Token: 0x04001B5B RID: 7003
		private bool ssWBThisTurn;

		// Token: 0x04001B5C RID: 7004
		private bool ssHCThisTurn;

		// Token: 0x04001B5D RID: 7005
		private bool enemyActivateLancea;

		// Token: 0x04001B5E RID: 7006
		private bool enemyActivateFuwalos;

		// Token: 0x04001B5F RID: 7007
		private bool ActiveUnderground;

		// Token: 0x04001B60 RID: 7008
		private bool blockWicckid;

		// Token: 0x04001B61 RID: 7009
		private bool mtp07SetThisTurn;

		// Token: 0x04001B62 RID: 7010
		private bool gwc06SetThisTurn;

		// Token: 0x04001B63 RID: 7011
		private bool splashNegatedThisTurn;

		// Token: 0x04001B64 RID: 7012
		private bool Allied_End;

		// Token: 0x04001B65 RID: 7013
		private bool fullBoard1;

		// Token: 0x04001B66 RID: 7014
		private bool goldstart;

		// Token: 0x04001B67 RID: 7015
		private bool undergroundstart;

		// Token: 0x04001B68 RID: 7016
		private bool nsplan;

		// Token: 0x04001B69 RID: 7017
		private bool nsBackupplan;

		// Token: 0x04001B6A RID: 7018
		private bool NSDorMouse;

		// Token: 0x04001B6B RID: 7019
		private bool nsLanceaplan;

		// Token: 0x04001B6C RID: 7020
		private int myTurnCount;

		// Token: 0x04001B6D RID: 7021
		private bool avoidLinkedZones;

		// Token: 0x04001B6E RID: 7022
		private bool wantLinkedToWicckid;

		// Token: 0x04001B6F RID: 7023
		private int? _wicckidEmzIndex;

		// Token: 0x04001B70 RID: 7024
		private int _transcodeZoneMask;

		// Token: 0x04001B71 RID: 7025
		private const int MZ0 = 1;

		// Token: 0x04001B72 RID: 7026
		private const int MZ1 = 2;

		// Token: 0x04001B73 RID: 7027
		private const int MZ2 = 4;

		// Token: 0x04001B74 RID: 7028
		private const int MZ3 = 8;

		// Token: 0x04001B75 RID: 7029
		private const int MZ4 = 16;

		// Token: 0x04001B76 RID: 7030
		private const int EMZ_L = 32;

		// Token: 0x04001B77 RID: 7031
		private const int EMZ_R = 64;

		// Token: 0x04001B78 RID: 7032
		private const int EMZ_ALL = 96;

		// Token: 0x04001B79 RID: 7033
		private int _wicckidEmzBit;

		// Token: 0x04001B7A RID: 7034
		private int _forceTranscodeBit;

		// Token: 0x04001B7B RID: 7035
		private bool step1Done;

		// Token: 0x04001B7C RID: 7036
		private bool step2Done;

		// Token: 0x04001B7D RID: 7037
		private int lastRevivedIdBySplash;

		// Token: 0x04001B7E RID: 7038
		private bool coreSetupComplete;

		// Token: 0x04001B7F RID: 7039
		private bool madeIt3;

		// Token: 0x04001B80 RID: 7040
		private bool resultSuccessFlag;

		// Token: 0x04001B81 RID: 7041
		private bool _didSplashToRR;

		// Token: 0x04001B82 RID: 7042
		private bool _didRRtoWicckid;

		// Token: 0x04001B83 RID: 7043
		private bool _didSummonToWicckidArrow;

		// Token: 0x04001B84 RID: 7044
		private bool _didWBFromWicckid;

		// Token: 0x04001B85 RID: 7045
		private bool _finishPlanDecided;

		// Token: 0x04001B86 RID: 7046
		private bool _preferWicckidArrows;

		// Token: 0x04001B87 RID: 7047
		private bool _rrSelfSSPlacing;

		// Token: 0x04001B88 RID: 7048
		private MalissExecutor.FinishPlan _finishPlan;

		// Token: 0x04001B89 RID: 7049
		private bool _oppJustActivatedPersistentSpell;

		// Token: 0x04001B8A RID: 7050
		private bool _oppJustSummoned;

		// Token: 0x04001B8B RID: 7051
		private bool _oppJustSet;

		// Token: 0x04001B8C RID: 7052
		private int _prefWindowTTL;

		// Token: 0x04001B8D RID: 7053
		private int _enemyMonsterCountSnap;

		// Token: 0x04001B8E RID: 7054
		private int _enemyFacedownSTSnap;

		// Token: 0x04001B8F RID: 7055
		private int _totalAttack;

		// Token: 0x04001B90 RID: 7056
		private int _totalBotAttack;

		// Token: 0x04001B91 RID: 7057
		private bool enemyActivateMaxxC;

		// Token: 0x04001B92 RID: 7058
		private bool enemyActivateLockBird;

		// Token: 0x04001B93 RID: 7059
		private int dimensionShifterCount;

		// Token: 0x04001B94 RID: 7060
		private bool enemyActivateInfiniteImpermanenceFromHand;

		// Token: 0x04001B95 RID: 7061
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x04001B96 RID: 7062
		private List<ClientCard> currentNegateCardList = new List<ClientCard>();

		// Token: 0x04001B97 RID: 7063
		private List<ClientCard> currentDestroyCardList = new List<ClientCard>();

		// Token: 0x04001B98 RID: 7064
		private List<ClientCard> sendToGYThisTurn = new List<ClientCard>();

		// Token: 0x04001B99 RID: 7065
		private List<int> activatedCardIdList = new List<int>();

		// Token: 0x04001B9A RID: 7066
		private List<ClientCard> enemyPlaceThisTurn = new List<ClientCard>();

		// Token: 0x04001B9B RID: 7067
		private List<ClientCard> escapeTargetList = new List<ClientCard>();

		// Token: 0x04001B9C RID: 7068
		private List<ClientCard> summonThisTurn = new List<ClientCard>();

		// Token: 0x04001B9D RID: 7069
		private static readonly int EMZ_LEFT = 5;

		// Token: 0x04001B9E RID: 7070
		private static readonly int EMZ_RIGHT = 6;

		// Token: 0x04001B9F RID: 7071
		private static readonly int[] PreferCenterMainSeq = new int[] { 2, 1, 3, 0, 4 };

		// Token: 0x02000357 RID: 855
		public class CardId
		{
			// Token: 0x04001BA0 RID: 7072
			public const int DominusImpulse = 40366667;

			// Token: 0x04001BA1 RID: 7073
			public const int TERRAFORMING = 73628505;

			// Token: 0x04001BA2 RID: 7074
			public const int GoldSarcophagus = 75500286;

			// Token: 0x04001BA3 RID: 7075
			public const int BackupIgnister = 30118811;

			// Token: 0x04001BA4 RID: 7076
			public const int WizardIgnister = 3723262;

			// Token: 0x04001BA5 RID: 7077
			public const int MalissP_Dormouse = 32061192;

			// Token: 0x04001BA6 RID: 7078
			public const int MalissP_WhiteRabbit = 69272449;

			// Token: 0x04001BA7 RID: 7079
			public const int MalissP_ChessyCat = 96676583;

			// Token: 0x04001BA8 RID: 7080
			public const int MalissP_MarchHare = 20938824;

			// Token: 0x04001BA9 RID: 7081
			public const int MalissC_GWC06 = 20726052;

			// Token: 0x04001BAA RID: 7082
			public const int MalissC_MTP07 = 94722358;

			// Token: 0x04001BAB RID: 7083
			public const int MalissQ_RedRansom = 68059897;

			// Token: 0x04001BAC RID: 7084
			public const int MalissQ_WhiteBinder = 95454996;

			// Token: 0x04001BAD RID: 7085
			public const int MalissQ_HeartsCrypter = 21848500;

			// Token: 0x04001BAE RID: 7086
			public const int MalissInTheMirror = 93453053;

			// Token: 0x04001BAF RID: 7087
			public const int MalissInUnderground = 68337209;

			// Token: 0x04001BB0 RID: 7088
			public const int Linguriboh = 24842059;

			// Token: 0x04001BB1 RID: 7089
			public const int LinkDecoder = 30342076;

			// Token: 0x04001BB2 RID: 7090
			public const int SP_LITTLE_KNIGHT = 29301450;

			// Token: 0x04001BB3 RID: 7091
			public const int SALAMANGREAT_ALMIRAJ = 60303245;

			// Token: 0x04001BB4 RID: 7092
			public const int SplashMage = 59859086;

			// Token: 0x04001BB5 RID: 7093
			public const int CyberseWicckid = 52698008;

			// Token: 0x04001BB6 RID: 7094
			public const int TranscodeTalker = 46947713;

			// Token: 0x04001BB7 RID: 7095
			public const int AlliedCodeTalkerIgnister = 39138610;

			// Token: 0x04001BB8 RID: 7096
			public const int FirewallDragon = 5043010;

			// Token: 0x04001BB9 RID: 7097
			public const int LinkSpider = 98978921;

			// Token: 0x04001BBA RID: 7098
			public const int HaggardLizardose = 9763474;

			// Token: 0x04001BBB RID: 7099
			public const int AccesscodeTalker = 86066372;

			// Token: 0x04001BBC RID: 7100
			public const int Apollousa = 4280258;

			// Token: 0x04001BBD RID: 7101
			public const int Lancea = 34267821;

			// Token: 0x04001BBE RID: 7102
			public const int Fuwalos = 42141493;

			// Token: 0x04001BBF RID: 7103
			public const int NaturalExterio = 99916754;

			// Token: 0x04001BC0 RID: 7104
			public const int NaturalBeast = 33198837;

			// Token: 0x04001BC1 RID: 7105
			public const int ImperialOrder = 61740673;

			// Token: 0x04001BC2 RID: 7106
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x04001BC3 RID: 7107
			public const int RoyalDecree = 51452091;

			// Token: 0x04001BC4 RID: 7108
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x04001BC5 RID: 7109
			public const int InspectorBoarder = 15397015;

			// Token: 0x04001BC6 RID: 7110
			public const int SkillDrain = 82732705;

			// Token: 0x04001BC7 RID: 7111
			public const int DivineArsenalAAZEUS_SkyThunder = 90448279;

			// Token: 0x04001BC8 RID: 7112
			public const int DimensionShifter = 91800273;

			// Token: 0x04001BC9 RID: 7113
			public const int MacroCosmos = 30241314;

			// Token: 0x04001BCA RID: 7114
			public const int DimensionalFissure = 81674782;

			// Token: 0x04001BCB RID: 7115
			public const int BanisheroftheRadiance = 94853057;

			// Token: 0x04001BCC RID: 7116
			public const int BanisheroftheLight = 61528025;

			// Token: 0x04001BCD RID: 7117
			public const int KashtiraAriseHeart = 48626373;

			// Token: 0x04001BCE RID: 7118
			public const int GhostMournerMoonlitChill = 52038441;

			// Token: 0x04001BCF RID: 7119
			public const int NibiruThePrimalBeing = 27204311;
		}

		// Token: 0x02000358 RID: 856
		private enum FinishPlan
		{
			// Token: 0x04001BD1 RID: 7121
			FW_HC_Allied,
			// Token: 0x04001BD2 RID: 7122
			HC_Allied,
			// Token: 0x04001BD3 RID: 7123
			AlliedOnly
		}
	}
}
