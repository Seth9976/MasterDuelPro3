using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000F30 RID: 3888
	public class SynchroEffect : SummonEffectBase
	{
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600726F RID: 29295 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.SpSummonType spSummonType
		{
			get
			{
				return Engine.SpSummonType.Fusion;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06007270 RID: 29296 RVA: 0x000029CC File Offset: 0x00000BCC
		private int tunerLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06007271 RID: 29297 RVA: 0x000029CC File Offset: 0x00000BCC
		private int notTunerLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007272 RID: 29298 RVA: 0x0000216A File Offset: 0x0000036A
		public static SynchroEffect Create()
		{
			return null;
		}

		// Token: 0x06007273 RID: 29299 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(int destCardID, int destCardUniqueID, int[] materialCardIDs, int[] materialUniqueIDs, int materialNum, int destRareID, bool destIsMyself)
		{
		}

		// Token: 0x06007274 RID: 29300 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool PlayEffect(Action onFinished)
		{
			return false;
		}

		// Token: 0x06007275 RID: 29301 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddTunerNum()
		{
		}

		// Token: 0x06007276 RID: 29302 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddTunerLevel(int uniqueID, int level)
		{
		}

		// Token: 0x06007277 RID: 29303 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddNotTunerLevel(int uniqueID, int level)
		{
		}

		// Token: 0x06007278 RID: 29304 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetTuningInfo()
		{
		}

		// Token: 0x06007279 RID: 29305 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool PlaySynchroEffect(int materialNum, int tunerNum, int notTunerLevel, int tunerLevel, Action onFinished)
		{
			return false;
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCardShowTimeline(PlayableDirector timeline, int materialNum, int tunerNum)
		{
		}

		// Token: 0x0600727B RID: 29307 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSynchroTimeline(PlayableDirector timeline, int notTunerLevel, int tunerLevel)
		{
		}

		// Token: 0x0600727C RID: 29308 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupDestCardTexture(ElementObjectManager manager)
		{
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Skip()
		{
			return false;
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Finish()
		{
		}

		// Token: 0x0400AC0B RID: 44043
		private List<PlayableDirector> showTimeline;

		// Token: 0x0400AC0C RID: 44044
		private int tunerNum;

		// Token: 0x0400AC0D RID: 44045
		private Dictionary<int, int> tunerLevelList;

		// Token: 0x0400AC0E RID: 44046
		private Dictionary<int, int> notTunerLevelList;

		// Token: 0x0400AC0F RID: 44047
		private const string SUMMON_SYNCHRO_SHOW2 = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroShowUnitCard/SummonSynchroShowUnitCard02";

		// Token: 0x0400AC10 RID: 44048
		private const string SUMMON_SYNCHRO_SHOW3 = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroShowUnitCard/SummonSynchroShowUnitCard03";

		// Token: 0x0400AC11 RID: 44049
		private const string SUMMON_SYNCHRO_SHOW4 = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroShowUnitCard/SummonSynchroShowUnitCard04";

		// Token: 0x0400AC12 RID: 44050
		private const string SUMMON_SYNCHRO_SHOW5 = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroShowUnitCard/SummonSynchroShowUnitCard05";

		// Token: 0x0400AC13 RID: 44051
		private const string SUMMON_SYNCHRO_SHOW6 = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroShowUnitCard/SummonSynchroShowUnitCard06";

		// Token: 0x0400AC14 RID: 44052
		private const string SUMMON_SYNCHRO_SHOWPARTS1 = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroShowUnitParts/SummonSynchroShowUnitParts01";

		// Token: 0x0400AC15 RID: 44053
		private const string SUMMON_SYNCHRO_SHOWPARTS2 = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroShowUnitParts/SummonSynchroShowUnitParts02";

		// Token: 0x0400AC16 RID: 44054
		private const string SUMMON_SYNCHRO = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroMain01/SummonSynchro01";

		// Token: 0x0400AC17 RID: 44055
		private const string SUMMON_SYNCHRO_NOMATERIAL = "Duel/Timeline/Duel/Universal/Summon/SummonSynchro/SummonSynchroMain02/SummonSynchro02";
	}
}
