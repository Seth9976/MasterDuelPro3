using System;
using System.Collections;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.YGomTMPro;

namespace YgomGame.Pvp
{
	// Token: 0x02000A10 RID: 2576
	public class TestDuelMatchingViewController : BaseMenuViewController
	{
		// Token: 0x06004AC9 RID: 19145 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActiveView(TestDuelMatchingViewController.View state)
		{
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x0000216A File Offset: 0x0000036A
		private string ConvertDispTime(int time)
		{
			return null;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yInit()
		{
			return null;
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yMatch()
		{
			return null;
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x040088EB RID: 35051
		[SerializeField]
		private GameObject DuelStartPrefab;

		// Token: 0x040088EC RID: 35052
		private readonly string BTN_CANCEL_LABEL;

		// Token: 0x040088ED RID: 35053
		private readonly string BTN_BACK_LABEL;

		// Token: 0x040088EE RID: 35054
		private readonly string TXT_TIME_LABEL;

		// Token: 0x040088EF RID: 35055
		private readonly string ROOT_SEARCH_LABEL;

		// Token: 0x040088F0 RID: 35056
		private readonly string ROOT_MATCH_LABEL;

		// Token: 0x040088F1 RID: 35057
		private readonly string ROOT_TIMEOUT_LABEL;

		// Token: 0x040088F2 RID: 35058
		private readonly string IMG_ICON_LABEL;

		// Token: 0x040088F3 RID: 35059
		private readonly int RESEARCH_TIME;

		// Token: 0x040088F4 RID: 35060
		private ExtendedTextMeshProUGUI m_TextTime;

		// Token: 0x040088F5 RID: 35061
		private int m_ElapsedTime;

		// Token: 0x040088F6 RID: 35062
		private GameObject m_rootSearch;

		// Token: 0x040088F7 RID: 35063
		private GameObject m_rootMatch;

		// Token: 0x040088F8 RID: 35064
		private GameObject m_rootTimeout;

		// Token: 0x040088F9 RID: 35065
		private TestDuelMatchingViewController.View m_currentView;

		// Token: 0x02000A11 RID: 2577
		public enum View
		{
			// Token: 0x040088FB RID: 35067
			SEARCHING,
			// Token: 0x040088FC RID: 35068
			MATCHING,
			// Token: 0x040088FD RID: 35069
			TIMEOUT
		}
	}
}
