using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI
{
	// Token: 0x02000205 RID: 517
	public class AIUtil
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00030B87 File Offset: 0x0002ED87
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x00030B8F File Offset: 0x0002ED8F
		public Duel Duel { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00030B98 File Offset: 0x0002ED98
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00030BA0 File Offset: 0x0002EDA0
		public ClientField Bot { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00030BA9 File Offset: 0x0002EDA9
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00030BB1 File Offset: 0x0002EDB1
		public ClientField Enemy { get; private set; }

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00030BBA File Offset: 0x0002EDBA
		public AIUtil(Duel duel)
		{
			this.Duel = duel;
			this.Bot = this.Duel.Fields[0];
			this.Enemy = this.Duel.Fields[1];
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00030BF0 File Offset: 0x0002EDF0
		public int GetTotalAttackingMonsterAttack(int player)
		{
			return (from m in this.Duel.Fields[player].GetMonsters()
				where m.IsAttack()
				select m).Sum((ClientCard m) => new int?(m.Attack)).GetValueOrDefault();
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00030C60 File Offset: 0x0002EE60
		public int GetBestPower(ClientField field, bool onlyATK = false)
		{
			int? num = (from card in field.MonsterZone.GetMonsters()
				where !onlyATK || card.IsAttack()
				select card).Max((ClientCard card) => new int?(card.GetDefensePower()));
			if (num == null)
			{
				return -1;
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00030CCD File Offset: 0x0002EECD
		public int GetBestAttack(ClientField field)
		{
			return this.GetBestPower(field, true);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00030CD8 File Offset: 0x0002EED8
		public bool IsOneEnemyBetterThanValue(int value, bool onlyATK)
		{
			return this.Enemy.MonsterZone.GetMonsters().Any((ClientCard card) => card.GetDefensePower() > value && (!onlyATK || card.IsAttack()));
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00030D1C File Offset: 0x0002EF1C
		public bool IsAllEnemyBetterThanValue(int value, bool onlyATK)
		{
			List<ClientCard> monsters = this.Enemy.MonsterZone.GetMonsters();
			return monsters.Count > 0 && monsters.All((ClientCard card) => card.GetDefensePower() > value && (!onlyATK || card.IsAttack()));
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00030D6B File Offset: 0x0002EF6B
		public bool IsEnemyBetter(bool onlyATK, bool all)
		{
			if (all)
			{
				return this.IsAllEnemyBetter(onlyATK);
			}
			return this.IsOneEnemyBetter(onlyATK);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00030D80 File Offset: 0x0002EF80
		public bool IsOneEnemyBetter(bool onlyATK = false)
		{
			int bestBotPower = this.GetBestPower(this.Bot, onlyATK);
			return this.IsOneEnemyBetterThanValue(bestBotPower, onlyATK);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00030DA4 File Offset: 0x0002EFA4
		public bool IsAllEnemyBetter(bool onlyATK = false)
		{
			int bestBotPower = this.GetBestPower(this.Bot, onlyATK);
			return this.IsAllEnemyBetterThanValue(bestBotPower, onlyATK);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00030DC8 File Offset: 0x0002EFC8
		public ClientCard GetBestBotMonster(bool onlyATK = false)
		{
			return (from card in this.Bot.MonsterZone.GetMonsters()
				where !onlyATK || card.IsAttack()
				orderby card.GetDefensePower() descending
				select card).FirstOrDefault<ClientCard>();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00030E2C File Offset: 0x0002F02C
		public ClientCard GetWorstBotMonster(bool onlyATK = false)
		{
			return (from card in this.Bot.MonsterZone.GetMonsters()
				where !onlyATK || card.IsAttack()
				orderby card.GetDefensePower()
				select card).FirstOrDefault<ClientCard>();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00030E90 File Offset: 0x0002F090
		public ClientCard GetOneEnemyBetterThanValue(int value, bool onlyATK = false, bool canBeTarget = false)
		{
			return this.Enemy.MonsterZone.GetMonsters().FirstOrDefault((ClientCard card) => card.GetDefensePower() >= value && (!onlyATK || card.IsAttack()) && (!canBeTarget || !card.IsShouldNotBeTarget()));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00030EDC File Offset: 0x0002F0DC
		public ClientCard GetOneEnemyBetterThanMyBest(bool onlyATK = false, bool canBeTarget = false)
		{
			int bestBotPower = this.GetBestPower(this.Bot, onlyATK);
			return this.GetOneEnemyBetterThanValue(bestBotPower, onlyATK, canBeTarget);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00030F00 File Offset: 0x0002F100
		public ClientCard GetProblematicEnemyCard(int attack = 0, bool canBeTarget = false)
		{
			ClientCard card = this.Enemy.MonsterZone.GetFloodgate(canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.Enemy.SpellZone.GetFloodgate(canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.Enemy.MonsterZone.GetDangerousMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.Enemy.MonsterZone.GetInvincibleMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			if (attack == 0)
			{
				attack = this.GetBestAttack(this.Bot);
			}
			return this.GetOneEnemyBetterThanValue(attack, true, canBeTarget);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00030F84 File Offset: 0x0002F184
		public ClientCard GetProblematicEnemyMonster(int attack = 0, bool canBeTarget = false)
		{
			ClientCard card = this.Enemy.MonsterZone.GetFloodgate(canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.Enemy.MonsterZone.GetDangerousMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.Enemy.MonsterZone.GetInvincibleMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			if (attack == 0)
			{
				attack = this.GetBestAttack(this.Bot);
			}
			return this.GetOneEnemyBetterThanValue(attack, true, canBeTarget);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00030FF0 File Offset: 0x0002F1F0
		public ClientCard GetProblematicEnemySpell()
		{
			return this.Enemy.SpellZone.GetFloodgate(false);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00031004 File Offset: 0x0002F204
		public ClientCard GetBestEnemyCard(bool onlyFaceup = false, bool canBeTarget = false)
		{
			ClientCard card = this.GetBestEnemyMonster(onlyFaceup, canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.GetBestEnemySpell(onlyFaceup);
			if (card != null)
			{
				return card;
			}
			return null;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00031030 File Offset: 0x0002F230
		public ClientCard GetBestEnemyMonster(bool onlyFaceup = false, bool canBeTarget = false)
		{
			ClientCard card = this.GetProblematicEnemyMonster(0, canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.Enemy.MonsterZone.GetHighestAttackMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			List<ClientCard> monsters = this.Enemy.GetMonsters();
			if (monsters.Count > 0 && !onlyFaceup)
			{
				return monsters[0];
			}
			return null;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00031084 File Offset: 0x0002F284
		public ClientCard GetWorstEnemyMonster(bool onlyATK = false)
		{
			return (from card in this.Enemy.MonsterZone.GetMonsters()
				where !onlyATK || card.IsAttack()
				orderby card.GetDefensePower()
				select card).FirstOrDefault<ClientCard>();
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000310E8 File Offset: 0x0002F2E8
		public ClientCard GetBestEnemySpell(bool onlyFaceup = false)
		{
			ClientCard card = this.GetProblematicEnemySpell();
			if (card != null)
			{
				return card;
			}
			List<ClientCard> spells = this.Enemy.GetSpells();
			card = spells.FirstOrDefault((ClientCard ecard) => ecard.IsFaceup() && (ecard.HasType(CardType.Continuous) || ecard.HasType(CardType.Field)));
			if (card != null)
			{
				return card;
			}
			if (spells.Count > 0 && !onlyFaceup)
			{
				return spells[0];
			}
			return null;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0003114D File Offset: 0x0002F34D
		public ClientCard GetPZone(int player, int id)
		{
			if (this.Duel.IsNewRule)
			{
				return this.Duel.Fields[player].SpellZone[id * 4];
			}
			return this.Duel.Fields[player].SpellZone[6 + id];
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00031189 File Offset: 0x0002F389
		public int GetStringId(int id, int option)
		{
			return id * 16 + option;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00031191 File Offset: 0x0002F391
		public bool IsTurn1OrMain2()
		{
			return this.Duel.Turn == 1 || this.Duel.Phase == DuelPhase.Main2;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000311B8 File Offset: 0x0002F3B8
		public int GetBotAvailZonesFromExtraDeck(IList<ClientCard> remove)
		{
			ClientCard[] BotMZone = (ClientCard[])this.Bot.MonsterZone.Clone();
			ClientCard[] EnemyMZone = (ClientCard[])this.Enemy.MonsterZone.Clone();
			for (int i = 0; i < 7; i++)
			{
				if (remove.Contains(BotMZone[i]))
				{
					BotMZone[i] = null;
				}
				if (remove.Contains(EnemyMZone[i]))
				{
					EnemyMZone[i] = null;
				}
			}
			if (!this.Duel.IsNewRule || this.Duel.IsNewRule2020)
			{
				return 31;
			}
			int result = 0;
			if (BotMZone[5] == null && BotMZone[6] == null)
			{
				if (EnemyMZone[5] == null)
				{
					result |= 64;
				}
				if (EnemyMZone[6] == null)
				{
					result |= 32;
				}
			}
			if (BotMZone[0] == null)
			{
				ClientCard clientCard = BotMZone[1];
				if (clientCard == null || !clientCard.HasLinkMarker(CardLinkMarker.Left))
				{
					ClientCard clientCard2 = BotMZone[5];
					if (clientCard2 == null || !clientCard2.HasLinkMarker(CardLinkMarker.BottomLeft))
					{
						ClientCard clientCard3 = EnemyMZone[6];
						if (clientCard3 == null || !clientCard3.HasLinkMarker(CardLinkMarker.TopRight))
						{
							goto IL_00D6;
						}
					}
				}
				result |= 1;
			}
			IL_00D6:
			if (BotMZone[1] == null)
			{
				ClientCard clientCard4 = BotMZone[0];
				if (clientCard4 == null || !clientCard4.HasLinkMarker(CardLinkMarker.Right))
				{
					ClientCard clientCard5 = BotMZone[2];
					if (clientCard5 == null || !clientCard5.HasLinkMarker(CardLinkMarker.Left))
					{
						ClientCard clientCard6 = BotMZone[5];
						if (clientCard6 == null || !clientCard6.HasLinkMarker(CardLinkMarker.Bottom))
						{
							ClientCard clientCard7 = EnemyMZone[6];
							if (clientCard7 == null || !clientCard7.HasLinkMarker(CardLinkMarker.Top))
							{
								goto IL_012C;
							}
						}
					}
				}
				result |= 2;
			}
			IL_012C:
			if (BotMZone[2] == null)
			{
				ClientCard clientCard8 = BotMZone[1];
				if (clientCard8 == null || !clientCard8.HasLinkMarker(CardLinkMarker.Right))
				{
					ClientCard clientCard9 = BotMZone[3];
					if (clientCard9 == null || !clientCard9.HasLinkMarker(CardLinkMarker.Left))
					{
						ClientCard clientCard10 = BotMZone[5];
						if (clientCard10 == null || !clientCard10.HasLinkMarker(CardLinkMarker.BottomRight))
						{
							ClientCard clientCard11 = EnemyMZone[6];
							if (clientCard11 == null || !clientCard11.HasLinkMarker(CardLinkMarker.TopLeft))
							{
								ClientCard clientCard12 = BotMZone[6];
								if (clientCard12 == null || !clientCard12.HasLinkMarker(CardLinkMarker.BottomLeft))
								{
									ClientCard clientCard13 = EnemyMZone[5];
									if (clientCard13 == null || !clientCard13.HasLinkMarker(CardLinkMarker.TopRight))
									{
										goto IL_01A7;
									}
								}
							}
						}
					}
				}
				result |= 4;
			}
			IL_01A7:
			if (BotMZone[3] == null)
			{
				ClientCard clientCard14 = BotMZone[2];
				if (clientCard14 == null || !clientCard14.HasLinkMarker(CardLinkMarker.Right))
				{
					ClientCard clientCard15 = BotMZone[4];
					if (clientCard15 == null || !clientCard15.HasLinkMarker(CardLinkMarker.Left))
					{
						ClientCard clientCard16 = BotMZone[6];
						if (clientCard16 == null || !clientCard16.HasLinkMarker(CardLinkMarker.Bottom))
						{
							ClientCard clientCard17 = EnemyMZone[5];
							if (clientCard17 == null || !clientCard17.HasLinkMarker(CardLinkMarker.Top))
							{
								goto IL_01FD;
							}
						}
					}
				}
				result |= 8;
			}
			IL_01FD:
			if (BotMZone[4] == null)
			{
				ClientCard clientCard18 = BotMZone[3];
				if (clientCard18 == null || !clientCard18.HasLinkMarker(CardLinkMarker.Right))
				{
					ClientCard clientCard19 = BotMZone[6];
					if (clientCard19 == null || !clientCard19.HasLinkMarker(CardLinkMarker.BottomRight))
					{
						ClientCard clientCard20 = EnemyMZone[5];
						if (clientCard20 == null || !clientCard20.HasLinkMarker(CardLinkMarker.TopLeft))
						{
							return result;
						}
					}
				}
				result |= 16;
			}
			return result;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00031405 File Offset: 0x0002F605
		public int GetBotAvailZonesFromExtraDeck(ClientCard remove)
		{
			return this.GetBotAvailZonesFromExtraDeck(new ClientCard[] { remove });
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00031417 File Offset: 0x0002F617
		public int GetBotAvailZonesFromExtraDeck()
		{
			return this.GetBotAvailZonesFromExtraDeck(new List<ClientCard>());
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00031424 File Offset: 0x0002F624
		public bool IsChainTarget(ClientCard card)
		{
			return this.Duel.ChainTargets.Any(new Func<ClientCard, bool>(card.Equals));
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00031442 File Offset: 0x0002F642
		public bool IsChainTargetOnly(ClientCard card)
		{
			return this.Duel.ChainTargetOnly.Count == 1 && card.Equals(this.Duel.ChainTargetOnly[0]);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00031470 File Offset: 0x0002F670
		public bool ChainContainsCard(int id)
		{
			return this.Duel.CurrentChain.Any((ClientCard card) => card.IsCode(id));
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000314A8 File Offset: 0x0002F6A8
		public bool ChainContainsCard(int[] ids)
		{
			return this.Duel.CurrentChain.Any((ClientCard card) => card.IsCode(ids));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000314E0 File Offset: 0x0002F6E0
		public int ChainCountPlayer(int player)
		{
			return this.Duel.CurrentChain.Count((ClientCard card) => card.Controller == player);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00031518 File Offset: 0x0002F718
		public bool ChainContainPlayer(int player)
		{
			return this.Duel.CurrentChain.Any((ClientCard card) => card.Controller == player);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00031550 File Offset: 0x0002F750
		public bool HasChainedTrap(int player)
		{
			return this.Duel.CurrentChain.Any((ClientCard card) => card.Controller == player && card.HasType(CardType.Trap));
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00031586 File Offset: 0x0002F786
		public ClientCard GetLastChainCard()
		{
			return this.Duel.CurrentChain.LastOrDefault<ClientCard>();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00031598 File Offset: 0x0002F798
		public IList<ClientCard> SelectPreferredCards(ClientCard preferred, IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> selected = new List<ClientCard>();
			if (cards.IndexOf(preferred) > 0 && selected.Count < max)
			{
				selected.Add(preferred);
			}
			return selected;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000315C8 File Offset: 0x0002F7C8
		public IList<ClientCard> SelectPreferredCards(int preferred, IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> selected = new List<ClientCard>();
			foreach (ClientCard card in cards)
			{
				if (card.IsCode(preferred) && selected.Count < max)
				{
					selected.Add(card);
				}
			}
			return selected;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0003162C File Offset: 0x0002F82C
		public IList<ClientCard> SelectPreferredCards(IList<ClientCard> preferred, IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> selected = new List<ClientCard>();
			IList<ClientCard> avail = cards.ToList<ClientCard>();
			while (preferred.Count > 0 && avail.IndexOf(preferred[0]) > 0 && selected.Count < max)
			{
				ClientCard card = preferred[0];
				preferred.Remove(card);
				avail.Remove(card);
				selected.Add(card);
			}
			return selected;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0003168C File Offset: 0x0002F88C
		public IList<ClientCard> SelectPreferredCards(IList<int> preferred, IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> selected = new List<ClientCard>();
			foreach (int id in preferred)
			{
				foreach (ClientCard card in cards)
				{
					if (card.IsCode(id) && selected.Count < max && selected.IndexOf(card) <= 0)
					{
						selected.Add(card);
					}
				}
				if (selected.Count >= max)
				{
					break;
				}
			}
			return selected;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00031738 File Offset: 0x0002F938
		public IList<ClientCard> CheckSelectCount(IList<ClientCard> _selected, IList<ClientCard> cards, int min, int max)
		{
			List<ClientCard> selected = _selected.Distinct<ClientCard>().ToList<ClientCard>();
			if (selected.Count < min)
			{
				foreach (ClientCard card in cards)
				{
					if (!selected.Contains(card))
					{
						selected.Add(card);
					}
					if (selected.Count >= max)
					{
						break;
					}
				}
				if (selected.Count < min)
				{
					Logger.WriteErrorLine("Not enough cards to CheckSelectCount, using default");
					return null;
				}
			}
			while (selected.Count > max)
			{
				selected.RemoveAt(selected.Count - 1);
			}
			return selected;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000317D8 File Offset: 0x0002F9D8
		public List<List<ClientCard>> GetXyzMaterials(IList<ClientCard> param_pre_materials, int level, int material_count, bool material_count_above = false, Func<ClientCard, bool> material_func = null)
		{
			List<List<ClientCard>> result = new List<List<ClientCard>>();
			List<ClientCard> pre_materials = ((param_pre_materials != null) ? param_pre_materials.Where((ClientCard card) => card != null && !(card.IsFacedown() & (card.Location == CardLocation.MonsterZone)) && card.Level == level && !card.IsMonsterNotBeXyzMaterial()).ToList<ClientCard>() : null);
			if (pre_materials != null && pre_materials.Count<ClientCard>() < material_count)
			{
				return result;
			}
			Func<ClientCard, bool> default_func = (ClientCard card) => true;
			material_func = material_func ?? default_func;
			int i = 1;
			while ((double)i < Math.Pow(2.0, (double)pre_materials.Count))
			{
				List<ClientCard> temp_materials = new List<ClientCard>();
				char[] reversedBinaryChars = Convert.ToString(i, 2).PadLeft(pre_materials.Count, '0').Reverse<char>()
					.ToArray<char>();
				for (int j = 0; j < pre_materials.Count; j++)
				{
					if (reversedBinaryChars[j] == '1' && material_func(pre_materials[j]))
					{
						temp_materials.Add(pre_materials[j]);
					}
				}
				if (material_count_above ? (temp_materials.Count >= material_count) : (temp_materials.Count == material_count))
				{
					result.Add(temp_materials);
				}
				i++;
			}
			return result;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00031908 File Offset: 0x0002FB08
		public List<List<ClientCard>> GetSynchroMaterials(IList<ClientCard> param_pre_materials, int level, int tuner_count, int n_tuner_count, bool tuner_count_above = false, bool n_tuner_count_above = true, Func<ClientCard, bool> tuner_func = null, Func<ClientCard, bool> n_tuner_func = null)
		{
			List<List<ClientCard>> t_result = new List<List<ClientCard>>();
			List<ClientCard> list;
			if (param_pre_materials == null)
			{
				list = null;
			}
			else
			{
				list = param_pre_materials.Where((ClientCard card) => card != null && !(card.IsFacedown() & (card.Location == CardLocation.MonsterZone)) && card.Level > 0 && !card.IsMonsterNotBeSynchroMaterial()).ToList<ClientCard>();
			}
			List<ClientCard> pre_materials = list;
			int? num = ((pre_materials != null) ? new int?(pre_materials.Count<ClientCard>()) : null);
			int num2 = tuner_count + n_tuner_count;
			if ((num.GetValueOrDefault() < num2) & (num != null))
			{
				return t_result;
			}
			Func<ClientCard, bool> default_func = (ClientCard card) => true;
			tuner_func = tuner_func ?? default_func;
			n_tuner_func = n_tuner_func ?? default_func;
			pre_materials.Sort(new Comparison<ClientCard>(CardContainer.CompareCardLevel));
			Stack<object[]> materials_stack = new Stack<object[]>();
			for (int i = 0; i < pre_materials.Count; i++)
			{
				if (pre_materials[i].Level > level)
				{
					IL_023D:
					while (materials_stack.Count > 0)
					{
						object[] array = materials_stack.Pop();
						int num3 = (int)array[0];
						int index = (int)array[1];
						int sum = (int)array[2];
						List<ClientCard> temp_materials = (List<ClientCard>)array[3];
						if (sum == level)
						{
							t_result.Add(temp_materials);
						}
						else if (sum < level)
						{
							int j = index + 1;
							while (j < pre_materials.Count && pre_materials[j].Level <= level - sum)
							{
								if (j <= index + 1 || pre_materials[j].Level != pre_materials[j - 1].Level)
								{
									List<ClientCard> new_temp_materials = new List<ClientCard>(temp_materials);
									new_temp_materials.Add(pre_materials[j]);
									materials_stack.Push(new object[]
									{
										pre_materials[j].Level,
										j,
										sum + pre_materials[j].Level,
										new_temp_materials
									});
								}
								j++;
							}
						}
					}
					List<List<ClientCard>> result = new List<List<ClientCard>>();
					for (int k = 0; k < t_result.Count; k++)
					{
						List<ClientCard> materials = t_result[k];
						List<ClientCard> tuner_materials = new List<ClientCard>();
						List<ClientCard> n_tuner_materials = new List<ClientCard>();
						foreach (ClientCard material in materials)
						{
							if (material.HasType(CardType.Tuner) && tuner_func(material))
							{
								tuner_materials.Add(material);
							}
							else if (material.Level > 0 && n_tuner_func(material))
							{
								n_tuner_materials.Add(material);
							}
						}
						if ((tuner_count_above ? (tuner_materials.Count >= tuner_count) : (tuner_materials.Count == tuner_count)) && (n_tuner_count_above ? (n_tuner_materials.Count >= n_tuner_count) : (n_tuner_materials.Count == n_tuner_count)))
						{
							result.Add(materials);
						}
					}
					return result;
				}
				materials_stack.Push(new object[]
				{
					pre_materials[i].Level,
					i,
					pre_materials[i].Level,
					new List<ClientCard> { pre_materials[i] }
				});
			}
			goto IL_023D;
		}
	}
}
