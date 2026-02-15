using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000EEA RID: 3818
	public class RitualEffect : SummonEffectBase
	{
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06006F61 RID: 28513 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.SpSummonType spSummonType
		{
			get
			{
				return Engine.SpSummonType.Fusion;
			}
		}

		// Token: 0x06006F62 RID: 28514 RVA: 0x0000216A File Offset: 0x0000036A
		public static RitualEffect Create()
		{
			return null;
		}

		// Token: 0x06006F63 RID: 28515 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool PlayEffect(Action onFinished)
		{
			return false;
		}

		// Token: 0x06006F64 RID: 28516 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(int destCardID, int destCardUniqueID, int[] materialCardIDs, int[] materialUniqueIDs, int materialNum, int destRareID, bool destIsMyself)
		{
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool PlayRitualEffect(int materialNum, Action onFinished)
		{
			return false;
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayMainTimeline(Action onFinished)
		{
		}

		// Token: 0x06006F67 RID: 28519 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCardShowTimeline(PlayableDirector timeline, int materialNum)
		{
		}

		// Token: 0x06006F68 RID: 28520 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupRitualTimeline(PlayableDirector timeline, int materialNum)
		{
		}

		// Token: 0x06006F69 RID: 28521 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupDestCardTexture(ElementObjectManager manager)
		{
		}

		// Token: 0x06006F6A RID: 28522 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Skip()
		{
			return false;
		}

		// Token: 0x06006F6B RID: 28523 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Finish()
		{
		}

		// Token: 0x0400AA45 RID: 43589
		private List<PlayableDirector> showTimeline;

		// Token: 0x0400AA46 RID: 43590
		private const string SUMMON_RITUAL_SHOW1 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitCard/SummonRitualShowUnitCard01";

		// Token: 0x0400AA47 RID: 43591
		private const string SUMMON_RITUAL_SHOW2 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitCard/SummonRitualShowUnitCard02";

		// Token: 0x0400AA48 RID: 43592
		private const string SUMMON_RITUAL_SHOW3 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitCard/SummonRitualShowUnitCard03";

		// Token: 0x0400AA49 RID: 43593
		private const string SUMMON_RITUAL_SHOW4 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitCard/SummonRitualShowUnitCard04";

		// Token: 0x0400AA4A RID: 43594
		private const string SUMMON_RITUAL_SHOW5 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitCard/SummonRitualShowUnitCard05";

		// Token: 0x0400AA4B RID: 43595
		private const string SUMMON_RITUAL_SHOW6 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitCard/SummonRitualShowUnitCard06";

		// Token: 0x0400AA4C RID: 43596
		private const string SUMMON_RITUAL_SHOWPARTS01 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitParts/SummonRitualShowUnitParts01";

		// Token: 0x0400AA4D RID: 43597
		private const string SUMMON_RITUAL_SHOWPARTS02 = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitualShowUnitParts/SummonRitualShowUnitParts02";

		// Token: 0x0400AA4E RID: 43598
		private const string SUMMON_RITUAL = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitual01";

		// Token: 0x0400AA4F RID: 43599
		private const string SUMMON_RITUAL_NOMATERIAL = "Duel/Timeline/Duel/Universal/Summon/SummonRitual/SummonRitual02";
	}
}
