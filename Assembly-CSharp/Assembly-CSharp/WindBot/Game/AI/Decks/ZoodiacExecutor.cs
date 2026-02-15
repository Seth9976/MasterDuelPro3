using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200045A RID: 1114
	[Deck("Zoodiac", "AI_Zoodiac", "Normal")]
	internal class ZoodiacExecutor : DefaultExecutor
	{
		// Token: 0x060024AE RID: 9390 RVA: 0x000F01C4 File Offset: 0x000EE3C4
		public ZoodiacExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Activate, 99330325, new Func<bool>(base.DefaultInterruptedKaijuSlumber));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.SpSummon, 55063751, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 29726552, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 36956512, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 28674152, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.SpSummon, 63941210, new Func<bool>(base.DefaultKaijuSpsummon));
			base.AddExecutor(ExecutorType.Activate, 73628505);
			base.AddExecutor(ExecutorType.Activate, 47679935);
			base.AddExecutor(ExecutorType.Activate, 57103969, new Func<bool>(this.FireFormationTenkiEffect));
			base.AddExecutor(ExecutorType.Activate, 46060017, new Func<bool>(this.ZoodiacBarrageEffect));
			base.AddExecutor(ExecutorType.Activate, 581014, new Func<bool>(this.DaigustoEmeralEffect));
			base.AddExecutor(ExecutorType.SpSummon, 65367484, new Func<bool>(this.PhotonThrasherSummon));
			base.AddExecutor(ExecutorType.SpSummon, 84013237, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningSummon));
			base.AddExecutor(ExecutorType.SpSummon, 56832966);
			base.AddExecutor(ExecutorType.Activate, 56832966, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningEffect));
			base.AddExecutor(ExecutorType.Activate, 75286621, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.RatpierMaterialEffect));
			base.AddExecutor(ExecutorType.Activate, 48905153, new Func<bool>(this.DridentEffect));
			base.AddExecutor(ExecutorType.Activate, 85115440, new Func<bool>(this.BroadbullEffect));
			base.AddExecutor(ExecutorType.Activate, 11510448, new Func<bool>(this.TigermortarEffect));
			base.AddExecutor(ExecutorType.Activate, 41375811, new Func<bool>(this.ChakanineEffect));
			base.AddExecutor(ExecutorType.SpSummon, 41375811, new Func<bool>(this.ChakanineSummon));
			base.AddExecutor(ExecutorType.SpSummon, 11510448, new Func<bool>(this.TigermortarSummon));
			base.AddExecutor(ExecutorType.SpSummon, 85115440, new Func<bool>(this.BroadbullSummon));
			base.AddExecutor(ExecutorType.SpSummon, 48905153, new Func<bool>(this.DridentSummon));
			base.AddExecutor(ExecutorType.Summon, 78872731);
			base.AddExecutor(ExecutorType.Activate, 78872731, new Func<bool>(this.RatpierEffect));
			base.AddExecutor(ExecutorType.Summon, 77150143);
			base.AddExecutor(ExecutorType.Activate, 77150143, new Func<bool>(this.RatpierEffect));
			base.AddExecutor(ExecutorType.Summon, 86120751);
			base.AddExecutor(ExecutorType.Activate, 86120751, new Func<bool>(this.AleisterTheInvokerEffect));
			base.AddExecutor(ExecutorType.SpSummon, 581014, new Func<bool>(this.DaigustoEmeralSummon));
			base.AddExecutor(ExecutorType.SpSummon, 85115440, new Func<bool>(this.BroadbullXYZSummon));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.MonsterRebornEffect));
			base.AddExecutor(ExecutorType.SpSummon, 65367484);
			base.AddExecutor(ExecutorType.Summon, 31755044);
			base.AddExecutor(ExecutorType.Activate, 74063034, new Func<bool>(this.InvocationEffect));
			base.AddExecutor(ExecutorType.Activate, 31755044, new Func<bool>(this.WhiptailEffect));
			base.AddExecutor(ExecutorType.Activate, 73881652, new Func<bool>(this.ZoodiacComboEffect));
			base.AddExecutor(ExecutorType.SpellSet, 73881652);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000F055F File Offset: 0x000EE75F
		public override void OnNewTurn()
		{
			this.TigermortarSpsummoned = false;
			this.ChakanineSpsummoned = false;
			this.BroadbullSpsummoned = false;
			base.OnNewTurn();
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000F057C File Offset: 0x000EE77C
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && attacker.HasType(CardType.Fusion) && base.Bot.HasInHand(86120751))
			{
				attacker.RealPower += 1000;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000F05BC File Offset: 0x000EE7BC
		private bool PhotonThrasherSummon()
		{
			return base.Bot.HasInHand(86120751) && !base.Bot.HasInHand(78872731) && !base.Bot.HasInHand(77150143);
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000F05F8 File Offset: 0x000EE7F8
		private bool AleisterTheInvokerEffect()
		{
			return base.Card.Location != CardLocation.Hand || (!base.DefaultCheckWhetherCardIsNegated(base.Card) && (base.Duel.Phase == DuelPhase.BattleStep || base.Duel.Phase == DuelPhase.BattleStart || base.Duel.Phase == DuelPhase.Damage) && (base.Duel.Player == 0 || base.Util.IsOneEnemyBetter(false)));
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000F0670 File Offset: 0x000EE870
		private bool InvocationEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			IList<ClientCard> materials0 = base.Bot.Graveyard;
			IList<ClientCard> materials = base.Enemy.Graveyard;
			IList<ClientCard> mats = new List<ClientCard>();
			ClientCard aleister = this.GetAleisterInGrave();
			if (aleister != null)
			{
				mats.Add(aleister);
			}
			ClientCard mat = null;
			foreach (ClientCard card in materials0)
			{
				if (card.HasAttribute(CardAttribute.Light))
				{
					mat = card;
					break;
				}
			}
			foreach (ClientCard card2 in materials)
			{
				if (card2.HasAttribute(CardAttribute.Light))
				{
					mat = card2;
					break;
				}
			}
			if (mat != null)
			{
				mats.Add(mat);
				base.AI.SelectCard(75286621);
				base.AI.SelectMaterials(mats, 0);
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			foreach (ClientCard card3 in materials0)
			{
				if (card3.HasAttribute(CardAttribute.Earth))
				{
					mat = card3;
					break;
				}
			}
			foreach (ClientCard card4 in materials)
			{
				if (card4.HasAttribute(CardAttribute.Earth))
				{
					mat = card4;
					break;
				}
			}
			if (mat != null)
			{
				mats.Add(mat);
				base.AI.SelectCard(48791583);
				base.AI.SelectMaterials(mats, 0);
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			return false;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000F0850 File Offset: 0x000EEA50
		private ClientCard GetAleisterInGrave()
		{
			foreach (ClientCard card in base.Enemy.Graveyard)
			{
				if (card.IsCode(86120751))
				{
					return card;
				}
			}
			foreach (ClientCard card2 in base.Bot.Graveyard)
			{
				if (card2.IsCode(86120751))
				{
					return card2;
				}
			}
			return null;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000F08FC File Offset: 0x000EEAFC
		private bool ChakanineSummon()
		{
			if (base.Bot.HasInMonstersZone(78872731, false, false, false) && !this.ChakanineSpsummoned)
			{
				base.AI.SelectMaterials(78872731, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.ChakanineSpsummoned = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(85115440, false, false, false) && !this.ChakanineSpsummoned)
			{
				base.AI.SelectMaterials(85115440, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.ChakanineSpsummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000F09A8 File Offset: 0x000EEBA8
		private bool ChakanineEffect()
		{
			if (base.Bot.HasInGraveyard(31755044) || base.Bot.HasInGraveyard(77150143))
			{
				base.AI.SelectCard(new int[] { 85115440, 11510448, 41375811, 77150143, 78872731, 31755044 });
				base.AI.SelectNextCard(new int[] { 31755044, 77150143 });
				return true;
			}
			return false;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000F0A1C File Offset: 0x000EEC1C
		private bool TigermortarSummon()
		{
			if (base.Bot.HasInMonstersZone(41375811, false, false, false) && !this.TigermortarSpsummoned)
			{
				base.AI.SelectMaterials(41375811, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.TigermortarSpsummoned = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(78872731, false, false, false) && !this.TigermortarSpsummoned)
			{
				base.AI.SelectMaterials(78872731, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.TigermortarSpsummoned = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(77150143, false, false, false) && !this.TigermortarSpsummoned && base.Bot.HasInGraveyard(new int[] { 31755044, 78872731 }))
			{
				base.AI.SelectMaterials(77150143, 0);
				base.AI.SelectYesNo(true);
				this.TigermortarSpsummoned = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(31755044, false, false, false) && !this.TigermortarSpsummoned && base.Bot.HasInGraveyard(78872731))
			{
				base.AI.SelectMaterials(31755044, 0);
				base.AI.SelectYesNo(true);
				this.TigermortarSpsummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000F0B83 File Offset: 0x000EED83
		private bool TigermortarEffect()
		{
			base.AI.SelectCard(41375811);
			base.AI.SelectNextCard(11510448);
			base.AI.SelectThirdCard(new int[] { 78872731, 31755044, 77150143 });
			return true;
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000F0BC4 File Offset: 0x000EEDC4
		private bool BroadbullSummon()
		{
			if (base.Bot.HasInMonstersZone(11510448, false, false, false) && !this.BroadbullSpsummoned)
			{
				base.AI.SelectMaterials(11510448, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.BroadbullSpsummoned = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(41375811, false, false, false) && !this.BroadbullSpsummoned)
			{
				base.AI.SelectMaterials(41375811, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.BroadbullSpsummoned = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(78872731, false, false, false) && !this.BroadbullSpsummoned)
			{
				base.AI.SelectMaterials(78872731, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.BroadbullSpsummoned = true;
				return true;
			}
			if (base.Bot.HasInMonstersZone(77150143, false, false, false) && !this.BroadbullSpsummoned)
			{
				base.AI.SelectMaterials(77150143, 0);
				base.AI.SelectYesNo(true);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.BroadbullSpsummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000F0D10 File Offset: 0x000EEF10
		private bool BroadbullEffect()
		{
			base.AI.SelectCard(new int[] { 11510448, 41375811, 48905153, 86120751, 65367484 });
			if (base.Bot.HasInHand(31755044) && !base.Bot.HasInHand(78872731))
			{
				base.AI.SelectNextCard(78872731);
			}
			else
			{
				base.AI.SelectNextCard(31755044);
			}
			return true;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000F0D80 File Offset: 0x000EEF80
		private bool BroadbullXYZSummon()
		{
			base.AI.SelectYesNo(false);
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			base.AI.SelectMaterials(new int[] { 78872731, 65367484, 31755044, 86120751 }, 0);
			return true;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000F0DB8 File Offset: 0x000EEFB8
		private bool DridentSummon()
		{
			base.AI.SelectMaterials(new int[] { 85115440, 11510448, 41375811, 77150143 }, 0);
			return true;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000F0DD8 File Offset: 0x000EEFD8
		private bool RatpierMaterialEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(78872731, 1))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000F0E04 File Offset: 0x000EF004
		private bool WhiptailEffect()
		{
			if (base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			ClientCard target = null;
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.IsFaceup() && monster.IsCode(48905153) && !monster.HasXyzMaterial())
				{
					target = monster;
					break;
				}
			}
			if (target == null)
			{
				base.AI.SelectCard(new int[] { 48905153 });
			}
			return true;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000F0EC8 File Offset: 0x000EF0C8
		private bool RatpierEffect()
		{
			base.AI.SelectCard(new int[] { 73881652, 77150143, 46060017 });
			return true;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000F0EE8 File Offset: 0x000EF0E8
		private bool DridentEffect()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			ClientCard target = base.Util.GetBestEnemyCard(true, false);
			if (target == null)
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 85115440, 11510448, 41375811, 77150143, 78872731, 31755044 });
			base.AI.SelectNextCard(target);
			return true;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000F0F40 File Offset: 0x000EF140
		private bool DaigustoEmeralSummon()
		{
			base.AI.SelectMaterials(new int[] { 65367484, 86120751 }, 0);
			return base.Bot.GetGraveyardMonsters().Count >= 3;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000F0F7A File Offset: 0x000EF17A
		private bool DaigustoEmeralEffect()
		{
			base.AI.SelectCard(new int[] { 78872731, 86120751, 31755044 });
			base.AI.SelectNextCard(new int[] { 78872731, 581014 });
			return true;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000F0FBC File Offset: 0x000EF1BC
		private bool FireFormationTenkiEffect()
		{
			if (base.Bot.HasInHand(46060017) || base.Bot.HasInSpellZone(46060017, false, false) || base.Bot.HasInHand(78872731))
			{
				base.AI.SelectCard(31755044);
			}
			else
			{
				base.AI.SelectCard(78872731);
			}
			base.AI.SelectYesNo(true);
			return true;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000F1030 File Offset: 0x000EF230
		private bool ZoodiacBarrageEffect()
		{
			foreach (ClientCard spell in base.Bot.GetSpells())
			{
				if (spell.IsCode(46060017) && !base.Card.Equals(spell))
				{
					return false;
				}
			}
			base.AI.SelectCard(new int[] { 57103969, 47679935, 46060017 });
			base.AI.SelectNextCard(new int[] { 78872731, 31755044, 77150143 });
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000F10E8 File Offset: 0x000EF2E8
		private bool ZoodiacComboEffect()
		{
			if (base.Duel.CurrentChain.Count > 0)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Grave)
			{
				base.AI.SelectCard(48905153);
				base.AI.SelectNextCard(new int[] { 31755044, 78872731, 77150143 });
			}
			return true;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000F1146 File Offset: 0x000EF346
		private bool MonsterRebornEffect()
		{
			base.AI.SelectCard(new int[] { 78872731, 31755044, 75286621, 63941210, 48791583, 11510448, 41375811, 85115440 });
			return true;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000F0198 File Offset: 0x000EE398
		private bool MonsterRepos()
		{
			return (!base.Card.IsCode(56832966) || !base.Card.IsAttack()) && base.DefaultMonsterRepos();
		}

		// Token: 0x0400267A RID: 9850
		private bool TigermortarSpsummoned;

		// Token: 0x0400267B RID: 9851
		private bool ChakanineSpsummoned;

		// Token: 0x0400267C RID: 9852
		private bool BroadbullSpsummoned;

		// Token: 0x0200045B RID: 1115
		public class CardId
		{
			// Token: 0x0400267D RID: 9853
			public const int JizukirutheStarDestroyingKaiju = 63941210;

			// Token: 0x0400267E RID: 9854
			public const int GadarlatheMysteryDustKaiju = 36956512;

			// Token: 0x0400267F RID: 9855
			public const int GamecieltheSeaTurtleKaiju = 55063751;

			// Token: 0x04002680 RID: 9856
			public const int RadiantheMultidimensionalKaiju = 28674152;

			// Token: 0x04002681 RID: 9857
			public const int KumongoustheStickyStringKaiju = 29726552;

			// Token: 0x04002682 RID: 9858
			public const int PhotonThrasher = 65367484;

			// Token: 0x04002683 RID: 9859
			public const int Thoroughblade = 77150143;

			// Token: 0x04002684 RID: 9860
			public const int Whiptail = 31755044;

			// Token: 0x04002685 RID: 9861
			public const int Ratpier = 78872731;

			// Token: 0x04002686 RID: 9862
			public const int AleisterTheInvoker = 86120751;

			// Token: 0x04002687 RID: 9863
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04002688 RID: 9864
			public const int DarkHole = 53129443;

			// Token: 0x04002689 RID: 9865
			public const int Terraforming = 73628505;

			// Token: 0x0400268A RID: 9866
			public const int Invocation = 74063034;

			// Token: 0x0400268B RID: 9867
			public const int MonsterReborn = 83764718;

			// Token: 0x0400268C RID: 9868
			public const int InterruptedKaijuSlumber = 99330325;

			// Token: 0x0400268D RID: 9869
			public const int ZoodiacBarrage = 46060017;

			// Token: 0x0400268E RID: 9870
			public const int FireFormationTenki = 57103969;

			// Token: 0x0400268F RID: 9871
			public const int MagicalMeltdown = 47679935;

			// Token: 0x04002690 RID: 9872
			public const int ZoodiacCombo = 73881652;

			// Token: 0x04002691 RID: 9873
			public const int InvokedMechaba = 75286621;

			// Token: 0x04002692 RID: 9874
			public const int InvokedMagellanica = 48791583;

			// Token: 0x04002693 RID: 9875
			public const int NumberS39UtopiatheLightning = 56832966;

			// Token: 0x04002694 RID: 9876
			public const int Number39Utopia = 84013237;

			// Token: 0x04002695 RID: 9877
			public const int DaigustoEmeral = 581014;

			// Token: 0x04002696 RID: 9878
			public const int Tigermortar = 11510448;

			// Token: 0x04002697 RID: 9879
			public const int Chakanine = 41375811;

			// Token: 0x04002698 RID: 9880
			public const int Drident = 48905153;

			// Token: 0x04002699 RID: 9881
			public const int Broadbull = 85115440;
		}
	}
}
