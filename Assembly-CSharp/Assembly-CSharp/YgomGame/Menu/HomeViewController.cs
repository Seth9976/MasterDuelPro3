using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Home;
using YgomGame.MDMarkup;
using YgomSystem.ElementSystem;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Menu
{
	// Token: 0x02000A84 RID: 2692
	public class HomeViewController : BaseMenuViewController, ICommonHeaderSupported, IGemSupported, IConfigButtonSupported, IFadeSupported, IHeaderFocusListener
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PushOnHomeViewControler(string prefabname, Dictionary<string, object> args = null)
		{
			return false;
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionEnd(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnHeaderFocusChanged(bool setfocus, ViewController focusVc, ViewController prevVc)
		{
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x000F4B00 File Offset: 0x000F2D00
		public Color FadeColor(ViewController.TransitionType type)
		{
			return default(Color);
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x000029CC File Offset: 0x00000BCC
		public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
		{
			return SystemProgress.ProgressType.None;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsReadyPlayHomeAction()
		{
			return false;
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayHomeAction()
		{
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddInputCallback(SelectionButton button, PadInputDirection direction, SelectionButton target, SelectionButton failedTarget)
		{
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedAction()
		{
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x0000216D File Offset: 0x0000036D
		private void InvitePlatform()
		{
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x0000216D File Offset: 0x0000036D
		private void InviteRoomNotificator(object mes)
		{
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0000216D File Offset: 0x0000036D
		private void InviteTeamNotificator(object mes)
		{
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckParticipationConfirm()
		{
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckLoginBonus()
		{
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckForceNotification()
		{
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateHome()
		{
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDuelpass()
		{
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateWcsf()
		{
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitPopIcons()
		{
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePopIcons()
		{
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenTopicsList()
		{
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTopicsData(List<object> topicsList)
		{
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTopicsButton()
		{
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateTopics(MDMarkupBannerContext context)
		{
			return null;
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateWallpaper()
		{
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateWcsBG(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEventNotify()
		{
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitBadgeSettings()
		{
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateBadge()
		{
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIUserHome(Action onSuccessed = null)
		{
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIEventNotifyGetList(Action onFinished = null)
		{
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPINotificationRead(int id, Action onFinish = null)
		{
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomEntry(int invitedRoomId, Action onFinish = null)
		{
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x0000216A File Offset: 0x0000036A
		private Handle APIRoomEntry(int _id_, int _is_specter_, Dictionary<string, object> _options_)
		{
			return null;
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPILoginBonusGetList(Action onFinish = null)
		{
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIWcsGetParticipation(Action onFinished = null)
		{
		}

		// Token: 0x04008C9C RID: 35996
		public const string PREFAB_PATH = "Home/Home";

		// Token: 0x04008C9D RID: 35997
		private readonly string BANNER_LABEL;

		// Token: 0x04008C9E RID: 35998
		private readonly string BTN_BANNER_LABEL;

		// Token: 0x04008C9F RID: 35999
		private readonly string BTN_NEXT_LABEL;

		// Token: 0x04008CA0 RID: 36000
		private readonly string BTN_PREV_LABEL;

		// Token: 0x04008CA1 RID: 36001
		private readonly string SCROLL_LABEL;

		// Token: 0x04008CA2 RID: 36002
		private readonly string TEMPLATE_LABEL;

		// Token: 0x04008CA3 RID: 36003
		private readonly string BTN_QUEST_LABEL;

		// Token: 0x04008CA4 RID: 36004
		private readonly string BTN_SHOP_LABEL;

		// Token: 0x04008CA5 RID: 36005
		private readonly string BTN_DECK_LABEL;

		// Token: 0x04008CA6 RID: 36006
		private readonly string BTN_DUEL_LABEL;

		// Token: 0x04008CA7 RID: 36007
		private readonly string BTN_PLAYER_LABEL;

		// Token: 0x04008CA8 RID: 36008
		private readonly string BTN_DUELPASS_LABEL;

		// Token: 0x04008CA9 RID: 36009
		private readonly string IMG_LEVEL_LABEL;

		// Token: 0x04008CAA RID: 36010
		private readonly string IMG_RANK_LABEL;

		// Token: 0x04008CAB RID: 36011
		private readonly string IMG_ICON_LABEL;

		// Token: 0x04008CAC RID: 36012
		private readonly string PLATFORM_NAME_LABEL;

		// Token: 0x04008CAD RID: 36013
		private readonly string TXT_LEVEL_LABEL;

		// Token: 0x04008CAE RID: 36014
		private readonly string ROOT_MENU_LABEL;

		// Token: 0x04008CAF RID: 36015
		private readonly string ROOT_MENU_MOBILE_LABEL;

		// Token: 0x04008CB0 RID: 36016
		private readonly string ROOT_WALLPAPER_LABEL;

		// Token: 0x04008CB1 RID: 36017
		private readonly string WALLPAPER_LABEL;

		// Token: 0x04008CB2 RID: 36018
		private readonly string ROOT_PAGE_LABEL;

		// Token: 0x04008CB3 RID: 36019
		private readonly string ROOT_INDICATOR_LABEL;

		// Token: 0x04008CB4 RID: 36020
		private readonly string BTN_BACKKEYSHORTCUT_LABEL;

		// Token: 0x04008CB5 RID: 36021
		private readonly string BTN_TOPICSLIST_LABEL;

		// Token: 0x04008CB6 RID: 36022
		private readonly string E_SpecialBanner;

		// Token: 0x04008CB7 RID: 36023
		private readonly string E_Image;

		// Token: 0x04008CB8 RID: 36024
		private readonly string homeBGMLabel;

		// Token: 0x04008CB9 RID: 36025
		private List<GameObject> topicPages;

		// Token: 0x04008CBA RID: 36026
		private float pastSecTopics;

		// Token: 0x04008CBB RID: 36027
		private float pastSecEventNotify;

		// Token: 0x04008CBC RID: 36028
		private int currentWallpaperID;

		// Token: 0x04008CBD RID: 36029
		private GameObject currentWallpaperGo;

		// Token: 0x04008CBE RID: 36030
		private bool isFirstFade;

		// Token: 0x04008CBF RID: 36031
		private bool shouldCallAPIHome;

		// Token: 0x04008CC0 RID: 36032
		private bool calledPlayHomeAction;

		// Token: 0x04008CC1 RID: 36033
		private HomeBadge homeBadge;

		// Token: 0x04008CC2 RID: 36034
		private readonly HomePopIconWidget popIconWidget;

		// Token: 0x04008CC3 RID: 36035
		private ElementObjectManager menuBtnEom;

		// Token: 0x04008CC4 RID: 36036
		private SlidePagerWidget slidePagerWidget;

		// Token: 0x04008CC5 RID: 36037
		private List<HomeAction> actionList;

		// Token: 0x04008CC6 RID: 36038
		private HomeViewController.TopicsContext m_TopicsContext;

		// Token: 0x04008CC7 RID: 36039
		private int roomId;

		// Token: 0x04008CC8 RID: 36040
		private bool isInviteRoom;

		// Token: 0x04008CC9 RID: 36041
		private int teamId;

		// Token: 0x04008CCA RID: 36042
		private bool isInviteTeam;

		// Token: 0x04008CCB RID: 36043
		private bool isStackWcsBG;

		// Token: 0x02000A85 RID: 2693
		private class TopicsContext
		{
			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x06004EFC RID: 20220 RVA: 0x0000216A File Offset: 0x0000036A
			public List<MDMarkupBannerContext> mmaBannerContexts
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06004EFD RID: 20221 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(List<object> topicList)
			{
			}

			// Token: 0x06004EFE RID: 20222 RVA: 0x0000216A File Offset: 0x0000036A
			public MDMarkupPagerContainer CreateOrReuseMMAPagerContainer()
			{
				return null;
			}

			// Token: 0x06004EFF RID: 20223 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetButtonUrl(int index)
			{
				return null;
			}

			// Token: 0x04008CCC RID: 36044
			private List<MDMarkupBannerContext> m_MMABannerContexts;

			// Token: 0x04008CCD RID: 36045
			private List<string> m_MMAPageBodies;

			// Token: 0x04008CCE RID: 36046
			private MDMarkupPagerContainer m_MMAPagerContainer;

			// Token: 0x04008CCF RID: 36047
			private bool m_MMAPagerDirty;
		}
	}
}
