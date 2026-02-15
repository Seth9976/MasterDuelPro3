using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D9B RID: 3483
	public class DuelStatusViewer : MonoBehaviour
	{
		// Token: 0x06006677 RID: 26231 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(DuelGameObjectManager goManager, DuelHUD duelHUD, Transform parent, ElementObjectManager buttonUI, Action<DuelStatusViewer> finishCallback)
		{
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelGameObjectManager goManager, DuelHUD duelHUD, ElementObjectManager buttonUI)
		{
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowStatusDetail(bool show)
		{
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShowStatusDetail(int player, bool show)
		{
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitStatus()
		{
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateStatus(DuelStatusViewer.DuelStatusInfo status, DuelStatusViewer.UpdateStatusType updateType = DuelStatusViewer.UpdateStatusType.None)
		{
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateStatus(SharedDefinition.Location location, DuelStatusViewer.DuelStatusSideInfo info, Engine.Phase phase, DuelStatusViewer.UpdateStatusType updateType = DuelStatusViewer.UpdateStatusType.None)
		{
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x000F5CB4 File Offset: 0x000F3EB4
		public static DuelStatusViewer.DuelStatusInfo GetDuelStatusInfo()
		{
			return default(DuelStatusViewer.DuelStatusInfo);
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x000F5CCC File Offset: 0x000F3ECC
		private static DuelStatusViewer.DuelStatusSideInfo GetDuelStatusInfo(int player)
		{
			return default(DuelStatusViewer.DuelStatusSideInfo);
		}

		// Token: 0x06006684 RID: 26244 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateButton()
		{
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispButton(bool disp)
		{
		}

		// Token: 0x0400A073 RID: 41075
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x0400A074 RID: 41076
		private ElementObjectManager ui;

		// Token: 0x0400A075 RID: 41077
		private DuelGameObjectManager goManager;

		// Token: 0x0400A076 RID: 41078
		private ElementObjectManager buttonUI;

		// Token: 0x0400A077 RID: 41079
		private Selector selector;

		// Token: 0x0400A078 RID: 41080
		private SelectionButton openButton;

		// Token: 0x0400A079 RID: 41081
		private SummonInfoViewer summonInfoViewer;

		// Token: 0x0400A07A RID: 41082
		private bool opening;

		// Token: 0x0400A07B RID: 41083
		private bool reqOpen;

		// Token: 0x0400A07C RID: 41084
		private List<DeckCardPlace> deckCardPlaces;

		// Token: 0x0400A07D RID: 41085
		private List<GraveCardPlace> graveCardPlaces;

		// Token: 0x0400A07E RID: 41086
		private List<HandCardPlace> handCardPlaces;

		// Token: 0x0400A07F RID: 41087
		private bool initialized;

		// Token: 0x0400A080 RID: 41088
		private bool initStatus;

		// Token: 0x0400A081 RID: 41089
		private const string LABEL_ON = "On";

		// Token: 0x0400A082 RID: 41090
		private const string LABEL_OFF = "Off";

		// Token: 0x0400A083 RID: 41091
		private const string prefabPath = "Prefabs/Duel/DuelStatusViewer";

		// Token: 0x02000D9C RID: 3484
		public enum UpdateStatusType
		{
			// Token: 0x0400A085 RID: 41093
			None,
			// Token: 0x0400A086 RID: 41094
			OnTurnChange
		}

		// Token: 0x02000D9D RID: 3485
		public struct DuelStatusSideInfo
		{
			// Token: 0x0400A087 RID: 41095
			public bool canIDoSummon;

			// Token: 0x0400A088 RID: 41096
			public bool canIDoSpecialSummon;

			// Token: 0x0400A089 RID: 41097
			public int canIDoPutMonster;

			// Token: 0x0400A08A RID: 41098
			public int totalAtk;
		}

		// Token: 0x02000D9E RID: 3486
		public struct DuelStatusInfo
		{
			// Token: 0x0400A08B RID: 41099
			public DuelStatusViewer.DuelStatusSideInfo nearStatus;

			// Token: 0x0400A08C RID: 41100
			public DuelStatusViewer.DuelStatusSideInfo farStatus;

			// Token: 0x0400A08D RID: 41101
			public Engine.Phase phase;
		}
	}
}
