using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.WCS
{
	// Token: 0x020007F3 RID: 2035
	public class WinPredictionPlayersViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int id, bool isSinglePage, Action callback = null)
		{
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int id, bool isSinglePage, List<int> list, Action callback = null)
		{
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x0000216D File Offset: 0x0000036D
		private void Import(int idx, bool isRefresh = false)
		{
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshPage()
		{
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateView()
		{
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickProfileButton(int pcode)
		{
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x0000216D File Offset: 0x0000036D
		private void InActiveAllTemplate(ElementObjectManager eom)
		{
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNextButton()
		{
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPrevButton()
		{
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToNextPage()
		{
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToPrevPage()
		{
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangePage(int dstIdx, int direction = 0)
		{
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayPaging(int direction = 0)
		{
			return null;
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTLabelPagingOut(int direction)
		{
			return null;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTLabelPagingIn(int direction)
		{
			return null;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallEntryAPI()
		{
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetTeamDatas(int idx)
		{
			return null;
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetPlayersDatas(int idx)
		{
			return null;
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetDataRoot()
		{
			return null;
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetQuestions()
		{
			return null;
		}

		// Token: 0x04003815 RID: 14357
		private readonly string k_ELabelPrevButton;

		// Token: 0x04003816 RID: 14358
		private readonly string k_ELabelNextButton;

		// Token: 0x04003817 RID: 14359
		private readonly string k_TLabelPagingNextOut;

		// Token: 0x04003818 RID: 14360
		private readonly string k_TLabelPagingBackOut;

		// Token: 0x04003819 RID: 14361
		private readonly string k_TLabelPagingNextIn;

		// Token: 0x0400381A RID: 14362
		private readonly string k_TLabelPagingBackIn;

		// Token: 0x0400381B RID: 14363
		private readonly string k_ELabelShortcutButtonL1;

		// Token: 0x0400381C RID: 14364
		private readonly string k_ELabelShortcutButtonR1;

		// Token: 0x0400381D RID: 14365
		private SelectionButton m_PrevButton;

		// Token: 0x0400381E RID: 14366
		private SelectionButton m_NextButton;

		// Token: 0x0400381F RID: 14367
		private static readonly string K_ArgSinglePage;

		// Token: 0x04003820 RID: 14368
		private static readonly string k_ArgPageIndex;

		// Token: 0x04003821 RID: 14369
		private static readonly string k_ArgCallback;

		// Token: 0x04003822 RID: 14370
		private static readonly string k_ArgIdOrderList;

		// Token: 0x04003823 RID: 14371
		private readonly string CW_TEAM_NAME;

		// Token: 0x04003824 RID: 14372
		private readonly string CW_PLAYER_QandA;

		// Token: 0x04003825 RID: 14373
		private readonly string CW_PLAYER_ISLEADER;

		// Token: 0x04003826 RID: 14374
		private readonly string CW_PLAYER_ICON;

		// Token: 0x04003827 RID: 14375
		private readonly string CW_PLAYER_ICONFRAME;

		// Token: 0x04003828 RID: 14376
		private readonly string CW_PLAYER_NAME;

		// Token: 0x04003829 RID: 14377
		private readonly string CW_PLAYER_PCODE;

		// Token: 0x0400382A RID: 14378
		private readonly string LABEL_PROFILE_TEMPLATE;

		// Token: 0x0400382B RID: 14379
		private readonly string LABEL_HEADER1_TXT;

		// Token: 0x0400382C RID: 14380
		private readonly string LABEL_TEMPLATE1_TXT;

		// Token: 0x0400382D RID: 14381
		private readonly string LABEL_PLATFORM_PLAYER_NAME;

		// Token: 0x0400382E RID: 14382
		private readonly string LABEL_TEAM_NAME_TXT;

		// Token: 0x0400382F RID: 14383
		private readonly string LABEL_AREA_TXT;

		// Token: 0x04003830 RID: 14384
		private readonly string LABEL_TEAM_ICON;

		// Token: 0x04003831 RID: 14385
		private readonly string LABEL_ICON;

		// Token: 0x04003832 RID: 14386
		private readonly string LABEL_MAIN;

		// Token: 0x04003833 RID: 14387
		private readonly string LABEL_TEAM_AREA;

		// Token: 0x04003834 RID: 14388
		private readonly string LABEL_PLAYER_PROFILE;

		// Token: 0x04003835 RID: 14389
		private int m_PageIdx;

		// Token: 0x04003836 RID: 14390
		private int m_PageCount;

		// Token: 0x04003837 RID: 14391
		private bool isSinglePage;

		// Token: 0x04003838 RID: 14392
		private WinPredictionPlayersViewController.TeamData teamData;

		// Token: 0x04003839 RID: 14393
		private const int TEAM_MEMBER_NUM = 3;

		// Token: 0x0400383A RID: 14394
		private Dictionary<int, ElementObjectManager> playersEom;

		// Token: 0x0400383B RID: 14395
		private Dictionary<int, ElementObjectManager> playersProfileEom;

		// Token: 0x0400383C RID: 14396
		private ElementObjectManager teamArea;

		// Token: 0x0400383D RID: 14397
		private List<int> instancedTemplateNum;

		// Token: 0x0400383E RID: 14398
		private Dictionary<int, List<ElementObjectManager>> templateList;

		// Token: 0x0400383F RID: 14399
		private List<int> dataIdOrderList;

		// Token: 0x04003840 RID: 14400
		private ExtendedScrollRect m_ScrollView;

		// Token: 0x020007F4 RID: 2036
		private class PlayerData
		{
			// Token: 0x04003841 RID: 14401
			public string name;

			// Token: 0x04003842 RID: 14402
			public Dictionary<string, string> QandAList;

			// Token: 0x04003843 RID: 14403
			public int iconId;

			// Token: 0x04003844 RID: 14404
			public int iconFrameId;

			// Token: 0x04003845 RID: 14405
			public bool isReader;

			// Token: 0x04003846 RID: 14406
			public bool isOpenProf;

			// Token: 0x04003847 RID: 14407
			public int pcode;
		}

		// Token: 0x020007F5 RID: 2037
		private class TeamData
		{
			// Token: 0x06003F5F RID: 16223 RVA: 0x00002739 File Offset: 0x00000939
			public TeamData()
			{
			}

			// Token: 0x06003F60 RID: 16224 RVA: 0x00002739 File Offset: 0x00000939
			public TeamData(int id, string teamName)
			{
			}

			// Token: 0x04003848 RID: 14408
			public int id;

			// Token: 0x04003849 RID: 14409
			public string teamName;

			// Token: 0x0400384A RID: 14410
			public List<WinPredictionPlayersViewController.PlayerData> players;
		}
	}
}
