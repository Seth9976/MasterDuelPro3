using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using YgomSystem.Timeline;

namespace YgomGame.Duel
{
	// Token: 0x02000D6B RID: 3435
	public class DuelEntryController
	{
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06006413 RID: 25619 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAlive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelEntryController Create(DuelEntryMode mode)
		{
			return null;
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelEntryMode mode)
		{
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBaseTimelineEvent()
		{
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x0000216D File Offset: 0x0000036D
		public void Release()
		{
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DuelEntryPlayCoroutine()
		{
			return null;
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnLoadEnd()
		{
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFirstMoveDecide()
		{
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayTimeline()
		{
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x0000216D File Offset: 0x0000036D
		private void PrepareMatchingUI()
		{
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x0000216D File Offset: 0x0000036D
		private void PrepareLight()
		{
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x0000216D File Offset: 0x0000036D
		private void PrepareMate()
		{
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x0000216D File Offset: 0x0000036D
		private void MateInitialize(Character mateActor, int mateid, string matepath, bool isnear)
		{
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetMateId(bool isnear)
		{
			return 0;
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateLight(int playerId)
		{
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTeamCard()
		{
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetTeamMrk(int myid)
		{
			return 0;
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFlyingCard()
		{
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetExTimelineEvent()
		{
		}

		// Token: 0x04009EE7 RID: 40679
		private const string LABEL_TIMELINE_DUELENTRYPVP = "Duel/Timeline/Duel/Universal/DuelEntry/DuelEntry";

		// Token: 0x04009EE8 RID: 40680
		private const string LABEL_TIMELINE_DUELENTRYPVP_TEAM = "Duel/Timeline/Duel/Universal/DuelEntry/DuelEntryTeam";

		// Token: 0x04009EE9 RID: 40681
		private const string LABEL_TIMELINE_DUELENTRYSOLO = "Prefabs/Solo/SoloTransition/SoloTransitonDuelEntry";

		// Token: 0x04009EEA RID: 40682
		private PlayableDirector m_Director;

		// Token: 0x04009EEB RID: 40683
		private LabeledPlayableController m_LPController;

		// Token: 0x04009EEC RID: 40684
		private Character m_MateActorNear;

		// Token: 0x04009EED RID: 40685
		private Character m_MateActorFar;

		// Token: 0x04009EEE RID: 40686
		private DuelEntryMode m_Mode;

		// Token: 0x04009EEF RID: 40687
		private bool m_MotionPlayed;

		// Token: 0x04009EF0 RID: 40688
		private bool m_DuelStartCameraCreated;

		// Token: 0x04009EF1 RID: 40689
		private bool m_Cancelled;

		// Token: 0x04009EF2 RID: 40690
		private string m_MatepathNear;

		// Token: 0x04009EF3 RID: 40691
		private string m_MatepathFar;

		// Token: 0x04009EF4 RID: 40692
		private string m_AddEventLabel;

		// Token: 0x04009EF5 RID: 40693
		private GameObject m_LightTemplate;

		// Token: 0x04009EF6 RID: 40694
		private DuelEntryController.MatchingUIPrepareFlag m_MatchingUIPreapareFlag;

		// Token: 0x04009EF7 RID: 40695
		private GameObject m_BackPlane;

		// Token: 0x04009EF8 RID: 40696
		public UnityAction onTimelineStop;

		// Token: 0x02000D6C RID: 3436
		private class MatchingUIPrepareFlag
		{
			// Token: 0x17000B3E RID: 2878
			// (get) Token: 0x06006427 RID: 25639 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isReady
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006428 RID: 25640 RVA: 0x0000216D File Offset: 0x0000036D
			public void LightLoaded()
			{
			}

			// Token: 0x06006429 RID: 25641 RVA: 0x0000216D File Offset: 0x0000036D
			public void MateLoaded()
			{
			}

			// Token: 0x0600642A RID: 25642 RVA: 0x0000216D File Offset: 0x0000036D
			public void NoMatching()
			{
			}

			// Token: 0x04009EF9 RID: 40697
			private bool nomatching;

			// Token: 0x04009EFA RID: 40698
			private bool lightready;

			// Token: 0x04009EFB RID: 40699
			private byte mateloadedcount;
		}
	}
}
