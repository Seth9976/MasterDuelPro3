using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000E91 RID: 3729
	public class FusionEffect : SummonEffectBase
	{
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06006C4F RID: 27727 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.SpSummonType spSummonType
		{
			get
			{
				return Engine.SpSummonType.Fusion;
			}
		}

		// Token: 0x06006C50 RID: 27728 RVA: 0x0000216A File Offset: 0x0000036A
		public static FusionEffect Create()
		{
			return null;
		}

		// Token: 0x06006C51 RID: 27729 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(int destCardID, int destCardUniqueID, int[] materialCardIDs, int[] materialUniqueIDs, int materialNum, int destRareID, bool destIsMyself)
		{
		}

		// Token: 0x06006C52 RID: 27730 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool PlayEffect(Action onFinished)
		{
			return false;
		}

		// Token: 0x06006C53 RID: 27731 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool PlayFusionEffect(int materialNum, Action onFinished)
		{
			return false;
		}

		// Token: 0x06006C54 RID: 27732 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCardShowTimeline(PlayableDirector timeline, int materialNum)
		{
		}

		// Token: 0x06006C55 RID: 27733 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupFusionTimeline(PlayableDirector timeline, int materialNum)
		{
		}

		// Token: 0x06006C56 RID: 27734 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayMainTimeline(int materialNum, Action onFinished)
		{
		}

		// Token: 0x06006C57 RID: 27735 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupDestCardTexture(ElementObjectManager manager)
		{
		}

		// Token: 0x06006C58 RID: 27736 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Skip()
		{
			return false;
		}

		// Token: 0x06006C59 RID: 27737 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Finish()
		{
		}

		// Token: 0x0400A796 RID: 42902
		private static string[] indexLabel;

		// Token: 0x0400A797 RID: 42903
		private PlayableDirector showCardTimeline;

		// Token: 0x0400A798 RID: 42904
		private List<PlayableDirector> showCardTimelineUnits;

		// Token: 0x0400A799 RID: 42905
		private PlayableDirector bgTimeline;

		// Token: 0x0400A79A RID: 42906
		private Action onFinishCallback;

		// Token: 0x0400A79B RID: 42907
		private const string SUMMON_FUSION0 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusion00_01/SummonFusion00_01";

		// Token: 0x0400A79C RID: 42908
		private const string SUMMON_FUSION1 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusion01_01/SummonFusion01_01";

		// Token: 0x0400A79D RID: 42909
		private const string SUMMON_FUSION2 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusion02_01/SummonFusion02_01";

		// Token: 0x0400A79E RID: 42910
		private const string SUMMON_FUSION3 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusion03_01/SummonFusion03_01";

		// Token: 0x0400A79F RID: 42911
		private const string SUMMON_FUSION4 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusion04_01/SummonFusion04_01";

		// Token: 0x0400A7A0 RID: 42912
		private const string SUMMON_FUSION5 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusion05_01/SummonFusion05_01";

		// Token: 0x0400A7A1 RID: 42913
		private const string SUMMON_FUSION6 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionNum/FusionNum";

		// Token: 0x0400A7A2 RID: 42914
		private const string SUMMON_FUSION_NUM = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionNum/FusionNum";

		// Token: 0x0400A7A3 RID: 42915
		private const string SUMMON_FUSION_SHOW1 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard01";

		// Token: 0x0400A7A4 RID: 42916
		private const string SUMMON_FUSION_SHOW2 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard02";

		// Token: 0x0400A7A5 RID: 42917
		private const string SUMMON_FUSION_SHOW3 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard03";

		// Token: 0x0400A7A6 RID: 42918
		private const string SUMMON_FUSION_SHOW4 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard04";

		// Token: 0x0400A7A7 RID: 42919
		private const string SUMMON_FUSION_SHOW5 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard05";

		// Token: 0x0400A7A8 RID: 42920
		private const string SUMMON_FUSION_SHOW6 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard06";

		// Token: 0x0400A7A9 RID: 42921
		private const string SUMMON_FUSION_SHOW7 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard07";

		// Token: 0x0400A7AA RID: 42922
		private const string SUMMON_FUSION_SHOW8 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCard08";

		// Token: 0x0400A7AB RID: 42923
		private const string SUMMON_FUSION_SHOW9 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitCard/SummonFusionShowUnitCardNum";

		// Token: 0x0400A7AC RID: 42924
		private const string SUMMON_FUSION_SHOWPARTS01 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitParts/SummonFusionShowUnitParts01";

		// Token: 0x0400A7AD RID: 42925
		private const string SUMMON_FUSION_SHOWPARTS02 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionShowUnitParts/SummonFusionShowUnitParts02";

		// Token: 0x0400A7AE RID: 42926
		private const string SUMMON_FUSION_BG = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionBG/SummonFusionBG";

		// Token: 0x0400A7AF RID: 42927
		private const string SUMMON_FUSION_BG0 = "Duel/Timeline/Duel/Universal/Summon/SummonFusion/SummonFusionBG/SummonFusionBG00_01";
	}
}
