using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Stats;

namespace YgomGame.Duel
{
	// Token: 0x02000CF0 RID: 3312
	public class CardReportTelopManager : MonoBehaviour
	{
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06005EE3 RID: 24291 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isProcessing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(DuelClient host, Transform parent, UnityAction<CardReportTelopManager> onFinish)
		{
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelClient host, Transform parent, UnityAction<CardReportTelopManager> onFinish)
		{
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddCardReportTelopTask(int cardid)
		{
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideTelopEffect()
		{
		}

		// Token: 0x04009A3C RID: 39484
		private DuelClient m_Host;

		// Token: 0x04009A3D RID: 39485
		private const string PREHAB_PATH = "Prefabs/Duel/UI/CardReportTelop";

		// Token: 0x04009A3E RID: 39486
		private const int MAX_TELOP_COUNT = 5;

		// Token: 0x04009A3F RID: 39487
		private Queue<CardReportTelopManager.CardReportTelopTask> m_TaskQueue;

		// Token: 0x04009A40 RID: 39488
		private List<CardReportTelopController> m_TelopList;

		// Token: 0x04009A41 RID: 39489
		private CardReportTelopManager.CardReportTelopTask m_CurrentTask;

		// Token: 0x04009A42 RID: 39490
		private List<int> m_History;

		// Token: 0x04009A43 RID: 39491
		[SerializeField]
		private int m_Duration;

		// Token: 0x02000CF1 RID: 3313
		private class CardReportTelopTask
		{
			// Token: 0x06005EEA RID: 24298 RVA: 0x00002739 File Offset: 0x00000939
			public CardReportTelopTask(int cardid, bool fortest = false)
			{
			}

			// Token: 0x04009A44 RID: 39492
			public int cardid;

			// Token: 0x04009A45 RID: 39493
			public List<CardStatsData> datas;

			// Token: 0x04009A46 RID: 39494
			public bool testflag;
		}
	}
}
