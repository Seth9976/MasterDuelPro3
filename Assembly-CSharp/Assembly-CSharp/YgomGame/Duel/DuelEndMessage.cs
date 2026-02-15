using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D66 RID: 3430
	public class DuelEndMessage : MonoBehaviour
	{
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060063E1 RID: 25569 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060063E2 RID: 25570 RVA: 0x0000216D File Offset: 0x0000036D
		public GameObject profileCardObj
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060063E3 RID: 25571 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060063E4 RID: 25572 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool IsNextButtonClicked
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsShowing()
		{
			return false;
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(string label)
		{
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlaying(string label)
		{
			return false;
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnNextButton()
		{
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnRetryButton()
		{
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickProfileCardButton(Dictionary<string, object> profileData)
		{
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(DuelClient host, string message, bool dispProfileCard, bool isOnlineMode, bool isReplayMode, string userNameMyself, int iconIDMyself, int frameIDMyself, string onlineIDMyself, bool isSameOSMyself, Dictionary<string, object> profileDataMyself, bool winMyself, string userNameRival, int iconIDRival, int frameIDRival, string onlineIDRival, bool isSameOSRival, Dictionary<string, object> profileDataRival, bool winRival, bool disp, bool showRetryButton, int playeridMyself, int playeridRival, bool showAutoGoNext, bool hidePlayerID)
		{
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMessage(string message, bool disp = true)
		{
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateAutoGoNextMessage(float currentTime)
		{
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectEndMessageBtn()
		{
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectNearestBtnImpl()
		{
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDispMessage(bool disp)
		{
		}

		// Token: 0x04009E97 RID: 40599
		private SelectionButton nextButton;

		// Token: 0x04009E98 RID: 40600
		private TMP_Text nextButtonText;

		// Token: 0x04009E99 RID: 40601
		private GameObject messageBase;

		// Token: 0x04009E9A RID: 40602
		private ExtendedTextMeshProUGUI messageText;

		// Token: 0x04009E9B RID: 40603
		private ExtendedTextMeshProUGUI player0Name;

		// Token: 0x04009E9C RID: 40604
		private ExtendedTextMeshProUGUI player1Name;

		// Token: 0x04009E9D RID: 40605
		private Transform player0Icon;

		// Token: 0x04009E9E RID: 40606
		private Transform player1Icon;

		// Token: 0x04009E9F RID: 40607
		private SelectionButton profileCard0Button;

		// Token: 0x04009EA0 RID: 40608
		private SelectionButton profileCard1Button;

		// Token: 0x04009EA1 RID: 40609
		private GameObject player0PlatformRoot;

		// Token: 0x04009EA2 RID: 40610
		private GameObject player1PlatformRoot;

		// Token: 0x04009EA3 RID: 40611
		private Image player0PlatformIcon;

		// Token: 0x04009EA4 RID: 40612
		private Image player1PlatformIcon;

		// Token: 0x04009EA5 RID: 40613
		private TMP_Text player0PlatformID;

		// Token: 0x04009EA6 RID: 40614
		private TMP_Text player1PlatformID;

		// Token: 0x04009EA7 RID: 40615
		private SelectionButton retryButton;

		// Token: 0x04009EA8 RID: 40616
		private GameObject player0WinIcon;

		// Token: 0x04009EA9 RID: 40617
		private GameObject player1WinIcon;

		// Token: 0x04009EAA RID: 40618
		private GameObject autoGoNextMessageRoot;

		// Token: 0x04009EAB RID: 40619
		private TMP_Text autoGoNextMessageText;

		// Token: 0x04009EAC RID: 40620
		private DuelClient host;

		// Token: 0x04009EAD RID: 40621
		private Dictionary<string, object> profileDataMyself;

		// Token: 0x04009EAE RID: 40622
		private Dictionary<string, object> profileDataRival;

		// Token: 0x04009EAF RID: 40623
		private const string currentPlatformIconPath = "Images/PlatformIcon/<_PLATFORM_>/CurrentPlatformS";
	}
}
