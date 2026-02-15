using System;
using System.Linq;
using WindBot.Game.AI.Enums;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI
{
	// Token: 0x02000229 RID: 553
	public static class CardExtension
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x00032740 File Offset: 0x00030940
		public static bool IsMonsterInvincible(this ClientCard card)
		{
			return !card.IsDisabled() && ((card.Controller == 0 && Enum.IsDefined(typeof(InvincibleBotMonster), card.Id)) || (card.Controller == 1 && Enum.IsDefined(typeof(InvincibleEnemyMonster), card.Id)));
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000327A4 File Offset: 0x000309A4
		public static bool IsMonsterDangerous(this ClientCard card)
		{
			return !card.IsDisabled() && (Enum.IsDefined(typeof(DangerousMonster), card.Id) || (card.HasSetcode(397) && (card.HasType(CardType.Ritual) || card.EquipCards.Count > 0)));
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00032805 File Offset: 0x00030A05
		public static bool IsMonsterHasPreventActivationEffectInBattle(this ClientCard card)
		{
			return !card.IsDisabled() && Enum.IsDefined(typeof(PreventActivationEffectInBattle), card.Id);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003282C File Offset: 0x00030A2C
		public static bool IsShouldNotBeTarget(this ClientCard card)
		{
			if (card.IsDisabled() || card.HasType(CardType.Normal))
			{
				return false;
			}
			if (!Enum.IsDefined(typeof(ShouldNotBeTarget), card.Id))
			{
				return card.Overlays.Any((int code) => code == 91025875);
			}
			return true;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00032898 File Offset: 0x00030A98
		public static bool IsShouldNotBeMonsterTarget(this ClientCard card)
		{
			if (card.IsDisabled() || !Enum.IsDefined(typeof(ShouldNotBeMonsterTarget), card.Id))
			{
				return card.EquipCards.Any((ClientCard c) => c.IsCode(89812483) && !c.IsDisabled());
			}
			return true;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000328F8 File Offset: 0x00030AF8
		public static bool IsShouldNotBeSpellTrapTarget(this ClientCard card)
		{
			if (card.IsDisabled() || !Enum.IsDefined(typeof(ShouldNotBeSpellTrapTarget), card.Id))
			{
				return card.EquipCards.Any((ClientCard c) => c.IsCode(89812483) && !c.IsDisabled());
			}
			return true;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00032955 File Offset: 0x00030B55
		public static bool IsMonsterShouldBeDisabledBeforeItUseEffect(this ClientCard card)
		{
			return !card.IsDisabled() && Enum.IsDefined(typeof(ShouldBeDisabledBeforeItUseEffectMonster), card.Id);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003297B File Offset: 0x00030B7B
		public static bool IsFloodgate(this ClientCard card)
		{
			return Enum.IsDefined(typeof(Floodgate), card.Id);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00032997 File Offset: 0x00030B97
		public static bool IsOneForXyz(this ClientCard card)
		{
			return Enum.IsDefined(typeof(OneForXyz), card.Id);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000329B3 File Offset: 0x00030BB3
		public static bool IsFusionSpell(this ClientCard card)
		{
			return Enum.IsDefined(typeof(FusionSpell), card.Id);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000329CF File Offset: 0x00030BCF
		public static bool IsMonsterNotBeSynchroMaterial(this ClientCard card)
		{
			return Enum.IsDefined(typeof(NotBeSynchroMaterialMonster), card.Id);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000329EB File Offset: 0x00030BEB
		public static bool IsMonsterNotBeXyzMaterial(this ClientCard card)
		{
			return Enum.IsDefined(typeof(NotBeXyzMaterialMonster), card.Id);
		}
	}
}
