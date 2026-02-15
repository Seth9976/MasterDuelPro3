using System;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002B8 RID: 696
	[Deck("Blackwing", "AI_Blackwing", "NotFinished")]
	public class BlackwingExecutor : DefaultExecutor
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x00052718 File Offset: 0x00050918
		public BlackwingExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultRaigeki));
			base.AddExecutor(ExecutorType.Activate, 91351370, new Func<bool>(this.BlackWhirlwindEffect));
			base.AddExecutor(ExecutorType.SpSummon, 81105204);
			base.AddExecutor(ExecutorType.SummonOrSet, 81105204);
			base.AddExecutor(ExecutorType.Summon, 75498415, new Func<bool>(this.SiroccoTheDawnSummon));
			base.AddExecutor(ExecutorType.Summon, 58820853, new Func<bool>(this.ShuraTheBlueFlameSummon));
			base.AddExecutor(ExecutorType.SummonOrSet, 58820853);
			base.AddExecutor(ExecutorType.SpSummon, 49003716);
			base.AddExecutor(ExecutorType.SummonOrSet, 49003716);
			base.AddExecutor(ExecutorType.SummonOrSet, 85215458, new Func<bool>(this.KalutTheMoonShadowSummon));
			base.AddExecutor(ExecutorType.SpSummon, 2009101);
			base.AddExecutor(ExecutorType.SummonOrSet, 2009101);
			base.AddExecutor(ExecutorType.Summon, 22835145, new Func<bool>(this.BlizzardTheFarNorthSummon));
			base.AddExecutor(ExecutorType.MonsterSet, 46710683);
			base.AddExecutor(ExecutorType.SpSummon, 33236860);
			base.AddExecutor(ExecutorType.SpSummon, 69031175);
			base.AddExecutor(ExecutorType.SpSummon, 17377751);
			base.AddExecutor(ExecutorType.SpSummon, 76913983);
			base.AddExecutor(ExecutorType.SpSummon, 9012916);
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 70342110, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 59839761, new Func<bool>(this.DeltaCrowAntiReverseEffect));
			base.AddExecutor(ExecutorType.Activate, 22835145);
			base.AddExecutor(ExecutorType.Activate, 58820853);
			base.AddExecutor(ExecutorType.Activate, 49003716, new Func<bool>(this.BoraTheSpearEffect));
			base.AddExecutor(ExecutorType.Activate, 85215458, new Func<bool>(this.AttackUpEffect));
			base.AddExecutor(ExecutorType.Activate, 75498415, new Func<bool>(this.AttackUpEffect));
			base.AddExecutor(ExecutorType.Activate, 2009101, new Func<bool>(this.GaleTheWhirlwindEffect));
			base.AddExecutor(ExecutorType.Activate, 33236860);
			base.AddExecutor(ExecutorType.Activate, 9012916);
			base.AddExecutor(ExecutorType.Activate, 69031175);
			base.AddExecutor(ExecutorType.Activate, 76913983);
			base.AddExecutor(ExecutorType.Activate, 17377751);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000529AB File Offset: 0x00050BAB
		private bool ShuraTheBlueFlameSummon()
		{
			return base.Bot.HasInMonstersZone(75498415, false, false, false) && base.Bot.GetMonsters().GetHighestAttackMonster(false).Attack < 3800;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000529E4 File Offset: 0x00050BE4
		private bool BlackWhirlwindEffect()
		{
			if (base.Card.Location == CardLocation.Hand && base.Bot.HasInSpellZone(base.Card.Id, false, false))
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(base.Card.Id, 0))
			{
				base.AI.SelectCard(2009101);
			}
			return true;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00052A4C File Offset: 0x00050C4C
		private bool SiroccoTheDawnSummon()
		{
			bool monsterCount = base.Enemy.GetMonsterCount() != 0;
			int AIMonster = base.Bot.GetMonsterCount();
			return monsterCount && AIMonster == 0;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00052A78 File Offset: 0x00050C78
		private bool BoraTheSpearEffect()
		{
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card != null && card.IsCode(new int[] { 81105204, 85215458, 2009101, 49003716, 75498415, 58820853, 22835145 }))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00052AEC File Offset: 0x00050CEC
		private bool KalutTheMoonShadowSummon()
		{
			foreach (ClientCard card in base.Bot.Hand)
			{
				if (card != null && card.IsCode(new int[] { 81105204, 2009101, 49003716, 75498415, 58820853, 22835145 }))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00052B5C File Offset: 0x00050D5C
		private bool BlizzardTheFarNorthSummon()
		{
			foreach (ClientCard card in base.Bot.Graveyard)
			{
				if (card != null && card.IsCode(new int[] { 85215458, 49003716, 58820853, 81105204 }))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00052BCC File Offset: 0x00050DCC
		private bool DeltaCrowAntiReverseEffect()
		{
			int Count = 0;
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card != null && card.IsCode(new int[] { 81105204, 85215458, 2009101, 49003716, 75498415, 58820853, 22835145 }))
				{
					Count++;
				}
			}
			return Count == 3;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00052C48 File Offset: 0x00050E48
		private bool GaleTheWhirlwindEffect()
		{
			if (base.Card.Position == 5)
			{
				base.AI.SelectCard(base.Enemy.GetMonsters().GetHighestAttackMonster(false));
				return true;
			}
			return false;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00052C78 File Offset: 0x00050E78
		private bool AttackUpEffect()
		{
			ClientCard bestMy = base.Bot.GetMonsters().GetHighestAttackMonster(false);
			ClientCard bestEnemyATK = base.Enemy.GetMonsters().GetHighestAttackMonster(false);
			ClientCard bestEnemyDEF = base.Enemy.GetMonsters().GetHighestDefenseMonster(false);
			return bestMy != null && (bestEnemyATK != null || bestEnemyDEF != null) && ((bestEnemyATK != null && bestMy.Attack < bestEnemyATK.Attack) || (bestEnemyDEF != null && bestMy.Attack < bestEnemyDEF.Defense));
		}

		// Token: 0x020002B9 RID: 697
		public class CardId
		{
			// Token: 0x04001569 RID: 5481
			public const int KrisTheCrackOfDawn = 81105204;

			// Token: 0x0400156A RID: 5482
			public const int SiroccoTheDawn = 75498415;

			// Token: 0x0400156B RID: 5483
			public const int ShuraTheBlueFlame = 58820853;

			// Token: 0x0400156C RID: 5484
			public const int BoraTheSpear = 49003716;

			// Token: 0x0400156D RID: 5485
			public const int KalutTheMoonShadow = 85215458;

			// Token: 0x0400156E RID: 5486
			public const int GaleTheWhirlwind = 2009101;

			// Token: 0x0400156F RID: 5487
			public const int BlizzardTheFarNorth = 22835145;

			// Token: 0x04001570 RID: 5488
			public const int MistralTheSilverShield = 46710683;

			// Token: 0x04001571 RID: 5489
			public const int Raigeki = 12580477;

			// Token: 0x04001572 RID: 5490
			public const int DarkHole = 53129443;

			// Token: 0x04001573 RID: 5491
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x04001574 RID: 5492
			public const int BlackWhirlwind = 91351370;

			// Token: 0x04001575 RID: 5493
			public const int MirrorForce = 44095762;

			// Token: 0x04001576 RID: 5494
			public const int DeltaCrowAntiReverse = 59839761;

			// Token: 0x04001577 RID: 5495
			public const int DimensionalPrison = 70342110;

			// Token: 0x04001578 RID: 5496
			public const int SilverwindTheAscendant = 33236860;

			// Token: 0x04001579 RID: 5497
			public const int BlackWingedDragon = 9012916;

			// Token: 0x0400157A RID: 5498
			public const int ArmorMaster = 69031175;

			// Token: 0x0400157B RID: 5499
			public const int ArmedWing = 76913983;

			// Token: 0x0400157C RID: 5500
			public const int GramTheShiningStar = 17377751;
		}
	}
}
