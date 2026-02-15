using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200041B RID: 1051
	[Deck("Toadally Awesome", "AI_ToadallyAwesome", "Normal")]
	public class ToadallyAwesomeExecutor : DefaultExecutor
	{
		// Token: 0x0600219D RID: 8605 RVA: 0x000D65E0 File Offset: 0x000D47E0
		public ToadallyAwesomeExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 5133471, new Func<bool>(base.DefaultGalaxyCyclone));
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 29047353, new Func<bool>(this.AquariumStageEffect));
			base.AddExecutor(ExecutorType.Activate, 84206435, new Func<bool>(this.MedallionOfTheIceBarrierEffect));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialEffect));
			base.AddExecutor(ExecutorType.SpSummon, 50088247);
			base.AddExecutor(ExecutorType.Summon, 80250319, new Func<bool>(this.GraydleSlimeJrSummon));
			base.AddExecutor(ExecutorType.SpSummon, 9126351, new Func<bool>(this.SwapFrogSpsummon));
			base.AddExecutor(ExecutorType.Activate, 9126351, new Func<bool>(this.SwapFrogEffect));
			base.AddExecutor(ExecutorType.Activate, 80250319, new Func<bool>(this.GraydleSlimeJrEffect));
			base.AddExecutor(ExecutorType.Activate, 1357146, new Func<bool>(this.RonintoadinEffect));
			base.AddExecutor(ExecutorType.Activate, 50088247);
			base.AddExecutor(ExecutorType.Activate, 46239604);
			base.AddExecutor(ExecutorType.Activate, 33057951, new Func<bool>(this.SurfaceEffect));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.SurfaceEffect));
			base.AddExecutor(ExecutorType.Activate, 96947648, new Func<bool>(this.SalvageEffect));
			base.AddExecutor(ExecutorType.Summon, 9126351);
			base.AddExecutor(ExecutorType.Summon, 90311614, new Func<bool>(this.IceBarrierSummon));
			base.AddExecutor(ExecutorType.Summon, 23950192, new Func<bool>(this.IceBarrierSummon));
			base.AddExecutor(ExecutorType.Activate, 72892473);
			base.AddExecutor(ExecutorType.Summon, 80250319, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 50088247, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 1357146, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 46239604, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.Summon, 50088247, new Func<bool>(this.PriorOfTheIceBarrierSummon));
			base.AddExecutor(ExecutorType.SpSummon, 84224627, new Func<bool>(this.CatSharkSummon));
			base.AddExecutor(ExecutorType.Activate, 84224627, new Func<bool>(this.CatSharkEffect));
			base.AddExecutor(ExecutorType.SpSummon, 36776089, new Func<bool>(this.SkyCavalryCentaureaSummon));
			base.AddExecutor(ExecutorType.Activate, 36776089);
			base.AddExecutor(ExecutorType.SpSummon, 2766877, new Func<bool>(this.DaigustoPhoenixSummon));
			base.AddExecutor(ExecutorType.Activate, 2766877);
			base.AddExecutor(ExecutorType.SpSummon, 90809975);
			base.AddExecutor(ExecutorType.Activate, 90809975, new Func<bool>(this.ToadallyAwesomeEffect));
			base.AddExecutor(ExecutorType.SpSummon, 79606837, new Func<bool>(this.HeraldOfTheArcLightSummon));
			base.AddExecutor(ExecutorType.Activate, 79606837);
			base.AddExecutor(ExecutorType.MonsterSet, 80250319);
			base.AddExecutor(ExecutorType.MonsterSet, 46239604);
			base.AddExecutor(ExecutorType.MonsterSet, 1357146);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.Repos));
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 14087893, new Func<bool>(base.DefaultBookOfMoon));
			base.AddExecutor(ExecutorType.Activate, 97077563, new Func<bool>(this.SurfaceEffect));
			base.AddExecutor(ExecutorType.Activate, 53582587, new Func<bool>(base.DefaultTorrentialTribute));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.OtherSpellEffect));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.OtherTrapEffect));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.OtherMonsterEffect));
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000D69C8 File Offset: 0x000D4BC8
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && attacker.IsCode(36776089) && !attacker.IsDisabled() && attacker.HasXyzMaterial())
			{
				attacker.RealPower = base.Bot.LifePoints + attacker.Attack;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000D6A1C File Offset: 0x000D4C1C
		private bool MedallionOfTheIceBarrierEffect()
		{
			if (base.Bot.HasInHand(new int[] { 23950192, 90311614 }) || base.Bot.HasInMonstersZone(new int[] { 23950192, 90311614 }, false, false, false))
			{
				base.AI.SelectCard(50088247);
			}
			else
			{
				base.AI.SelectCard(new int[] { 23950192, 90311614 });
			}
			return true;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000D6AA6 File Offset: 0x000D4CA6
		private bool SurfaceEffect()
		{
			base.AI.SelectCard(new int[] { 90809975, 79606837, 9126351, 90311614, 23950192, 46239604, 1357146, 80250319 });
			return true;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000D6AC5 File Offset: 0x000D4CC5
		private bool AquariumStageEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return this.SurfaceEffect();
			}
			return true;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000D6AEC File Offset: 0x000D4CEC
		private bool FoolishBurialEffect()
		{
			if (base.Bot.HasInHand(80250319) && !base.Bot.HasInGraveyard(80250319))
			{
				base.AI.SelectCard(80250319);
			}
			else if (base.Bot.HasInGraveyard(1357146) && !base.Bot.HasInGraveyard(46239604))
			{
				base.AI.SelectCard(46239604);
			}
			else if (base.Bot.HasInGraveyard(46239604) && !base.Bot.HasInGraveyard(1357146))
			{
				base.AI.SelectCard(1357146);
			}
			else
			{
				base.AI.SelectCard(new int[] { 80250319, 1357146, 46239604, 23950192, 90311614, 50088247, 9126351 });
			}
			return true;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000D6BBB File Offset: 0x000D4DBB
		private bool SalvageEffect()
		{
			base.AI.SelectCard(new int[] { 9126351, 50088247, 80250319 });
			return true;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000D6BDC File Offset: 0x000D4DDC
		private bool SwapFrogSpsummon()
		{
			if (base.Bot.GetCountCardInZone(base.Bot.Hand, 80250319) >= 2 && !base.Bot.HasInGraveyard(80250319))
			{
				base.AI.SelectCard(80250319);
			}
			else if (base.Bot.HasInGraveyard(1357146) && !base.Bot.HasInGraveyard(46239604))
			{
				base.AI.SelectCard(46239604);
			}
			else if (base.Bot.HasInGraveyard(46239604) && !base.Bot.HasInGraveyard(1357146))
			{
				base.AI.SelectCard(1357146);
			}
			else
			{
				base.AI.SelectCard(new int[] { 1357146, 46239604, 23950192, 90311614, 50088247, 80250319, 9126351 });
			}
			return true;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000D6CB8 File Offset: 0x000D4EB8
		private bool SwapFrogEffect()
		{
			if (base.ActivateDescription == -1)
			{
				return this.FoolishBurialEffect();
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.HasInHand(46239604))
			{
				base.AI.SelectCard(new int[] { 50088247, 80250319, 9126351 });
				return true;
			}
			return false;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000D6D16 File Offset: 0x000D4F16
		private bool GraydleSlimeJrSummon()
		{
			return base.Bot.HasInGraveyard(80250319);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000D6D28 File Offset: 0x000D4F28
		private bool GraydleSlimeJrEffect()
		{
			base.AI.SelectCard(80250319);
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			base.AI.SelectNextCard(new int[] { 9126351, 23950192, 90311614, 1357146, 46239604, 50088247, 80250319 });
			return true;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000D6D63 File Offset: 0x000D4F63
		private bool RonintoadinEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000D6D84 File Offset: 0x000D4F84
		private bool NormalSummon()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Level == 2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000D6DE4 File Offset: 0x000D4FE4
		private bool IceBarrierSummon()
		{
			return base.Bot.GetCountCardInZone(base.Bot.Hand, 50088247) > 0;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000D6E04 File Offset: 0x000D5004
		private bool PriorOfTheIceBarrierSummon()
		{
			return base.Bot.GetCountCardInZone(base.Bot.Hand, 50088247) >= 2;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000D6E28 File Offset: 0x000D5028
		private bool ToadallyAwesomeEffect()
		{
			if (base.Duel.CurrentChain.Count > 0)
			{
				if (base.DefaultCheckWhetherCardIsNegated(base.Card))
				{
					return false;
				}
				List<ClientCard> monsters = base.Bot.GetMonsters();
				IList<int> suitableCost = new int[] { 9126351, 1357146, 80250319, 23950192, 90311614 };
				foreach (ClientCard monster in monsters)
				{
					if (monster.IsCode(suitableCost))
					{
						base.AI.SelectCard(monster);
						return true;
					}
				}
				if (!base.Bot.HasInSpellZone(29047353, true, false))
				{
					foreach (ClientCard monster2 in monsters)
					{
						if (monster2.IsCode(46239604))
						{
							base.AI.SelectCard(monster2);
							return true;
						}
					}
				}
				List<ClientCard> hands = base.Bot.Hand.GetMonsters();
				if (base.Bot.GetCountCardInZone(base.Bot.Hand, 80250319) >= 2)
				{
					foreach (ClientCard monster3 in hands)
					{
						if (monster3.IsCode(80250319))
						{
							base.AI.SelectCard(monster3);
							return true;
						}
					}
				}
				if (base.Bot.HasInGraveyard(1357146) && !base.Bot.HasInGraveyard(46239604) && !base.Bot.HasInGraveyard(9126351))
				{
					foreach (ClientCard monster4 in hands)
					{
						if (monster4.IsCode(46239604))
						{
							base.AI.SelectCard(monster4);
							return true;
						}
					}
				}
				foreach (ClientCard monster5 in hands)
				{
					if (monster5.IsCode(new int[] { 1357146, 46239604 }))
					{
						base.AI.SelectCard(monster5);
						return true;
					}
				}
				using (List<ClientCard>.Enumerator enumerator = hands.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ClientCard monster6 = enumerator.Current;
						base.AI.SelectCard(monster6);
						return true;
					}
				}
				return true;
			}
			else
			{
				if (base.Card.Location == CardLocation.Grave)
				{
					if (!base.Bot.HasInExtra(90809975))
					{
						base.AI.SelectCard(90809975);
					}
					else
					{
						base.AI.SelectCard(new int[] { 9126351, 50088247, 80250319 });
					}
					return true;
				}
				if (base.Duel.Phase != DuelPhase.Standby)
				{
					return true;
				}
				if (base.DefaultCheckWhetherCardIsNegated(base.Card))
				{
					return false;
				}
				this.SelectXYZDetach(base.Card.Overlays);
				if (base.Duel.Player == 0)
				{
					base.AI.SelectNextCard(new int[] { 9126351, 23950192, 90311614, 1357146, 46239604, 80250319 });
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 46239604, 9126351, 1357146, 80250319, 23950192, 90311614 });
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
				}
				return true;
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000D71EC File Offset: 0x000D53EC
		private bool CatSharkSummon()
		{
			if (base.Bot.HasInMonstersZone(90809975, false, false, false) && ((base.Util.IsOneEnemyBetter(true) && !base.Bot.HasInMonstersZone(new int[] { 84224627, 36776089 }, true, true, false)) || !base.Bot.HasInExtra(90809975)))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000D7264 File Offset: 0x000D5464
		private bool CatSharkEffect()
		{
			List<ClientCard> monsters = base.Bot.GetMonsters();
			foreach (ClientCard monster in monsters)
			{
				if (monster.IsCode(90809975) && monster.Attack <= 2200)
				{
					this.SelectXYZDetach(base.Card.Overlays);
					base.AI.SelectNextCard(monster);
					return true;
				}
			}
			foreach (ClientCard monster2 in monsters)
			{
				if (monster2.IsCode(36776089) && monster2.Attack <= 2000)
				{
					this.SelectXYZDetach(base.Card.Overlays);
					base.AI.SelectNextCard(monster2);
					return true;
				}
			}
			foreach (ClientCard monster3 in monsters)
			{
				if (monster3.IsCode(2766877) && monster3.Attack <= 1500)
				{
					this.SelectXYZDetach(base.Card.Overlays);
					base.AI.SelectNextCard(monster3);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000D73E8 File Offset: 0x000D55E8
		private bool SkyCavalryCentaureaSummon()
		{
			int num = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Level == 2)
					{
						num++;
					}
				}
			}
			return base.Util.IsOneEnemyBetter(true) && base.Util.GetBestAttack(base.Enemy) > 2200 && num < 4 && !base.Bot.HasInMonstersZone(new int[] { 36776089 }, true, true, false);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000D7494 File Offset: 0x000D5694
		private bool DaigustoPhoenixSummon()
		{
			if (base.Duel.Turn != 1)
			{
				int attack = 0;
				int defence = 0;
				foreach (ClientCard monster in base.Bot.GetMonsters())
				{
					if (!monster.IsDefense())
					{
						attack += monster.Attack;
					}
				}
				foreach (ClientCard monster2 in base.Enemy.GetMonsters())
				{
					defence += monster2.GetDefensePower();
				}
				if (attack - 2000 - defence > base.Enemy.LifePoints && !base.Util.IsOneEnemyBetter(true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000B3104 File Offset: 0x000B1304
		private bool HeraldOfTheArcLightSummon()
		{
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000D7580 File Offset: 0x000D5780
		private bool Repos()
		{
			return base.Card.IsFacedown() || (base.Card.IsDefense() && !base.Util.IsAllEnemyBetter(true) && base.Card.Attack >= base.Card.Defense);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000D75D4 File Offset: 0x000D57D4
		private bool OtherSpellEffect()
		{
			foreach (CardExecutor exec in base.Executors)
			{
				if (exec.Type == base.Type && exec.CardId == base.Card.Id)
				{
					return false;
				}
			}
			return base.Card.IsSpell();
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000D764C File Offset: 0x000D584C
		private bool OtherTrapEffect()
		{
			foreach (CardExecutor exec in base.Executors)
			{
				if (exec.Type == base.Type && exec.CardId == base.Card.Id)
				{
					return false;
				}
			}
			return base.Card.IsTrap() && base.DefaultTrap();
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000D76D0 File Offset: 0x000D58D0
		private bool OtherMonsterEffect()
		{
			foreach (CardExecutor exec in base.Executors)
			{
				if (exec.Type == base.Type && exec.CardId == base.Card.Id)
				{
					return false;
				}
			}
			return base.Card.IsMonster();
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000D7748 File Offset: 0x000D5948
		private void SelectXYZDetach(List<int> Overlays)
		{
			if (Overlays.Contains(80250319) && base.Bot.HasInHand(80250319) && !base.Bot.HasInGraveyard(80250319))
			{
				base.AI.SelectCard(80250319);
				return;
			}
			if (Overlays.Contains(46239604) && base.Bot.HasInGraveyard(1357146) && !base.Bot.HasInGraveyard(46239604))
			{
				base.AI.SelectCard(46239604);
				return;
			}
			if (Overlays.Contains(1357146) && base.Bot.HasInGraveyard(46239604) && !base.Bot.HasInGraveyard(1357146))
			{
				base.AI.SelectCard(1357146);
				return;
			}
			base.AI.SelectCard(new int[] { 80250319, 1357146, 46239604, 23950192, 90311614, 50088247, 9126351 });
		}

		// Token: 0x0200041C RID: 1052
		public class CardId
		{
			// Token: 0x04002418 RID: 9240
			public const int CryomancerOfTheIceBarrier = 23950192;

			// Token: 0x04002419 RID: 9241
			public const int DewdarkOfTheIceBarrier = 90311614;

			// Token: 0x0400241A RID: 9242
			public const int SwapFrog = 9126351;

			// Token: 0x0400241B RID: 9243
			public const int PriorOfTheIceBarrier = 50088247;

			// Token: 0x0400241C RID: 9244
			public const int Ronintoadin = 1357146;

			// Token: 0x0400241D RID: 9245
			public const int DupeFrog = 46239604;

			// Token: 0x0400241E RID: 9246
			public const int GraydleSlimeJr = 80250319;

			// Token: 0x0400241F RID: 9247
			public const int GalaxyCyclone = 5133471;

			// Token: 0x04002420 RID: 9248
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04002421 RID: 9249
			public const int Surface = 33057951;

			// Token: 0x04002422 RID: 9250
			public const int DarkHole = 53129443;

			// Token: 0x04002423 RID: 9251
			public const int CardDestruction = 72892473;

			// Token: 0x04002424 RID: 9252
			public const int FoolishBurial = 81439173;

			// Token: 0x04002425 RID: 9253
			public const int MonsterReborn = 83764718;

			// Token: 0x04002426 RID: 9254
			public const int MedallionOfTheIceBarrier = 84206435;

			// Token: 0x04002427 RID: 9255
			public const int Salvage = 96947648;

			// Token: 0x04002428 RID: 9256
			public const int AquariumStage = 29047353;

			// Token: 0x04002429 RID: 9257
			public const int HeraldOfTheArcLight = 79606837;

			// Token: 0x0400242A RID: 9258
			public const int ToadallyAwesome = 90809975;

			// Token: 0x0400242B RID: 9259
			public const int SkyCavalryCentaurea = 36776089;

			// Token: 0x0400242C RID: 9260
			public const int DaigustoPhoenix = 2766877;

			// Token: 0x0400242D RID: 9261
			public const int CatShark = 84224627;

			// Token: 0x0400242E RID: 9262
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x0400242F RID: 9263
			public const int BookOfMoon = 14087893;

			// Token: 0x04002430 RID: 9264
			public const int CallOfTheHaunted = 97077563;

			// Token: 0x04002431 RID: 9265
			public const int TorrentialTribute = 53582587;
		}
	}
}
