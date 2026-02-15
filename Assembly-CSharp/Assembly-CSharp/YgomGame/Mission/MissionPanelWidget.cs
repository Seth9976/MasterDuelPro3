using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Mission
{
	// Token: 0x02000A36 RID: 2614
	public class MissionPanelWidget : ElementWidgetBase
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject focusEffect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject hintGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x0000216A File Offset: 0x0000036A
		public ShortcutIcon hintShortcutIconGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionPanelWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ValidSetSpeed()
		{
			return false;
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPlayableSpeed(double speed)
		{
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFocusEffect()
		{
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingFocusEffect()
		{
			return false;
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFocusEffect()
		{
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearFocusEffect()
		{
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayCompleteEffectIn(bool isSkip)
		{
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingCompleteEffectIn()
		{
			return false;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayCompleteEffectOut(bool isSkip)
		{
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCompleteEffect()
		{
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearCompleteEffect()
		{
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingCompleteEffectOut()
		{
			return false;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0000216D File Offset: 0x0000036D
		public void ForceEndCompleteEffectOut()
		{
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEntryEffect()
		{
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingEntryEffect()
		{
			return false;
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0000216D File Offset: 0x0000036D
		public void ForceEndEntryEffect()
		{
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEntryEffect()
		{
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTM(PlayableAsset playableAsset, string label = "in")
		{
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTMWithSkipSwitch(PlayableAsset playableAsset, string label, bool skipOn)
		{
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsPlayingTM(PlayableAsset playableAsset, string label = "in")
		{
			return false;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0000216D File Offset: 0x0000036D
		private void EvaluateHeadTM(PlayableAsset playableAsset, string label = "in")
		{
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0000216D File Offset: 0x0000036D
		private void EvaluateTailTM(PlayableAsset playableAsset, string label = "in")
		{
		}

		// Token: 0x040089DA RID: 35290
		private const string k_ELabelFocusEffect = "FocusEffect";

		// Token: 0x040089DB RID: 35291
		private readonly string k_ELabelTitleText;

		// Token: 0x040089DC RID: 35292
		private readonly string k_ELabelInfoGroup;

		// Token: 0x040089DD RID: 35293
		private readonly string k_ELabelLimitDateGroup;

		// Token: 0x040089DE RID: 35294
		private readonly string k_ELabelLimitDateText;

		// Token: 0x040089DF RID: 35295
		private readonly string k_ELabelHintGroup;

		// Token: 0x040089E0 RID: 35296
		private readonly string k_ELabelHintButton;

		// Token: 0x040089E1 RID: 35297
		private readonly string k_ELabelHintShortcutIconGroup;

		// Token: 0x040089E2 RID: 35298
		private readonly string k_ELabelGoalsPager;

		// Token: 0x040089E3 RID: 35299
		private readonly string k_ELabelLockedFilter;

		// Token: 0x040089E4 RID: 35300
		private readonly string k_ELabelLockedIcon;

		// Token: 0x040089E5 RID: 35301
		private readonly string k_ELabelBadge;

		// Token: 0x040089E6 RID: 35302
		private readonly string k_ELabelSecretIcon;

		// Token: 0x040089E7 RID: 35303
		private const string k_TLabelIn = "in";

		// Token: 0x040089E8 RID: 35304
		private const string k_TLabelOut = "out";

		// Token: 0x040089E9 RID: 35305
		public int missionId;

		// Token: 0x040089EA RID: 35306
		public readonly LabeledPlayableController m_LabeledPlayableControlelr;

		// Token: 0x040089EB RID: 35307
		public readonly PlayableDirector playableDirector;

		// Token: 0x040089EC RID: 35308
		public readonly MissionGoalsPagerWidget goalsPager;

		// Token: 0x040089ED RID: 35309
		public readonly SelectionItem selectionItem;

		// Token: 0x040089EE RID: 35310
		public readonly GameObject titleBase;

		// Token: 0x040089EF RID: 35311
		public readonly TMP_Text titleText;

		// Token: 0x040089F0 RID: 35312
		public readonly GameObject InfoGroup;

		// Token: 0x040089F1 RID: 35313
		public readonly GameObject limitDateGroup;

		// Token: 0x040089F2 RID: 35314
		public readonly TMP_Text limitDateText;

		// Token: 0x040089F3 RID: 35315
		public readonly SelectionButton hintButton;

		// Token: 0x040089F4 RID: 35316
		public readonly GameObject lockedFilter;

		// Token: 0x040089F5 RID: 35317
		public readonly GameObject lockedIcon;

		// Token: 0x040089F6 RID: 35318
		public readonly GameObject badge;

		// Token: 0x040089F7 RID: 35319
		public readonly GameObject secretIcon;

		// Token: 0x040089F8 RID: 35320
		public PlayableAsset tmCompleteMission;

		// Token: 0x040089F9 RID: 35321
		public PlayableAsset tmHideMission;

		// Token: 0x040089FA RID: 35322
		public PlayableAsset tmNewMission;

		// Token: 0x040089FB RID: 35323
		public PlayableAsset tmFocusMission;
	}
}
