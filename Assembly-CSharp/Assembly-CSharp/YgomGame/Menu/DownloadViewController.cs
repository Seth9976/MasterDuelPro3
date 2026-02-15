using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;
using YgomGame.Card;
using YgomGame.Download;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A55 RID: 2645
	public class DownloadViewController : BaseMenuViewController
	{
		// Token: 0x06004D47 RID: 19783 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator WaitLoadCardData()
		{
			return null;
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckCardBrowser()
		{
			return false;
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeCardBrowserUI()
		{
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFooter(bool set)
		{
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0000216D File Offset: 0x0000036D
		private void clickBackAreaButton()
		{
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0000216D File Offset: 0x0000036D
		private void AutoFlip()
		{
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartDownload()
		{
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0000216D File Offset: 0x0000036D
		public void FlipCard()
		{
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x0000216D File Offset: 0x0000036D
		private void FadeCard()
		{
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x0000216D File Offset: 0x0000036D
		private void changeCardIDRange()
		{
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeOneBGCard()
		{
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0000216D File Offset: 0x0000036D
		private void CardDetail(int mrk)
		{
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0000216D File Offset: 0x0000036D
		private void endEvent()
		{
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartTimeLine()
		{
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetEventCallBackTimeLine()
		{
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0000216A File Offset: 0x0000036A
		private EventPlayableAsset GetEventPlayableAsset(PlayableDirector timeline)
		{
			return null;
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator AsyncNewCardCheck(int mrk)
		{
			return null;
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator AsyncWaitAndStart()
		{
			return null;
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0000216D File Offset: 0x0000036D
		private void setMrkList()
		{
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool checkCardMrk(int id)
		{
			return false;
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPathByCardId(int mrk)
		{
			return null;
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool checkCardMrkTest(int id)
		{
			return false;
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool checkAvoidCard(int id)
		{
			return false;
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x000029CC File Offset: 0x00000BCC
		private int randomMRK(int start, int end)
		{
			return 0;
		}

		// Token: 0x04008AE1 RID: 35553
		private readonly string TXT_DL_LABEL;

		// Token: 0x04008AE2 RID: 35554
		private readonly string TXT_DL_STATE_LABEL;

		// Token: 0x04008AE3 RID: 35555
		private readonly string BTN_OK_LABEL;

		// Token: 0x04008AE4 RID: 35556
		private readonly string IMG_PROGRESS_GAUGE;

		// Token: 0x04008AE5 RID: 35557
		private readonly string FLIP_BUTTON_LABEL;

		// Token: 0x04008AE6 RID: 35558
		private readonly string BACK_AREA_BUTTON_LABEL;

		// Token: 0x04008AE7 RID: 35559
		private readonly string CARD_BUTTON_LABEL;

		// Token: 0x04008AE8 RID: 35560
		private readonly string TEXT_NEXT_LABEL;

		// Token: 0x04008AE9 RID: 35561
		private readonly string TEXT_DETAIL_LABEL;

		// Token: 0x04008AEA RID: 35562
		private readonly string IMAGE_LABEL;

		// Token: 0x04008AEB RID: 35563
		private readonly string CARDBROWSER;

		// Token: 0x04008AEC RID: 35564
		private readonly string FOOTER_SC_ICON_0;

		// Token: 0x04008AED RID: 35565
		private readonly string FOOTER_SC_ICON_1;

		// Token: 0x04008AEE RID: 35566
		private readonly string ANDROID_BACK_KEY_LABEL;

		// Token: 0x04008AEF RID: 35567
		private readonly string FOOTER;

		// Token: 0x04008AF0 RID: 35568
		private readonly string ROOT;

		// Token: 0x04008AF1 RID: 35569
		private GameObject Root;

		// Token: 0x04008AF2 RID: 35570
		private GameObject Footer;

		// Token: 0x04008AF3 RID: 35571
		private TextMeshProUGUI DownloadingText;

		// Token: 0x04008AF4 RID: 35572
		private TextMeshProUGUI DownloadingStateText;

		// Token: 0x04008AF5 RID: 35573
		private SelectionButton btnOK;

		// Token: 0x04008AF6 RID: 35574
		private Image progressGaugeImage;

		// Token: 0x04008AF7 RID: 35575
		private TextMeshProUGUI textNext;

		// Token: 0x04008AF8 RID: 35576
		private TextMeshProUGUI textDetail;

		// Token: 0x04008AF9 RID: 35577
		private Image BGimage;

		// Token: 0x04008AFA RID: 35578
		private ShortcutIcon shortCutIcon0;

		// Token: 0x04008AFB RID: 35579
		private ShortcutIcon shortCutIcon1;

		// Token: 0x04008AFC RID: 35580
		private SelectionButtonUntouchable backBtn;

		// Token: 0x04008AFD RID: 35581
		private DownloadController downloadController;

		// Token: 0x04008AFE RID: 35582
		private bool isDownloading;

		// Token: 0x04008AFF RID: 35583
		private bool endFlag;

		// Token: 0x04008B00 RID: 35584
		private PlayableDirector downloadTransition;

		// Token: 0x04008B01 RID: 35585
		private EventPlayableAsset eventPlayableAsset;

		// Token: 0x04008B02 RID: 35586
		private double TIME_OPEN;

		// Token: 0x04008B03 RID: 35587
		private bool BGcardSetCompletedFlag;

		// Token: 0x04008B04 RID: 35588
		private int m_oldCardIndex;

		// Token: 0x04008B05 RID: 35589
		private int pickCount;

		// Token: 0x04008B06 RID: 35590
		private Renderer renderer0;

		// Token: 0x04008B07 RID: 35591
		private Renderer renderer1;

		// Token: 0x04008B08 RID: 35592
		private ElementObjectManager effEom;

		// Token: 0x04008B09 RID: 35593
		private ElementObjectManager eom;

		// Token: 0x04008B0A RID: 35594
		private ElementObject cardfront;

		// Token: 0x04008B0B RID: 35595
		private List<UnityAction<Texture2D>> onFinishList;

		// Token: 0x04008B0C RID: 35596
		private SelectionButton cardButton;

		// Token: 0x04008B0D RID: 35597
		private SelectionButton BackAreaButton;

		// Token: 0x04008B0E RID: 35598
		private CardIllustManager cardIllustManager;

		// Token: 0x04008B0F RID: 35599
		private int mrk;

		// Token: 0x04008B10 RID: 35600
		private List<int> mrkList;

		// Token: 0x04008B11 RID: 35601
		private bool startDownloadingFlag;

		// Token: 0x04008B12 RID: 35602
		private bool FlipCardActiveFlag;

		// Token: 0x04008B13 RID: 35603
		private bool timeLineStartFrag;

		// Token: 0x04008B14 RID: 35604
		private bool isCardBrowserOpen;

		// Token: 0x04008B15 RID: 35605
		private float flipTime;

		// Token: 0x04008B16 RID: 35606
		private bool flipingFlag;

		// Token: 0x04008B17 RID: 35607
		private bool openningCardBrowser;

		// Token: 0x04008B18 RID: 35608
		private List<int> avoidList;

		// Token: 0x04008B19 RID: 35609
		private int MIN_NUM;

		// Token: 0x04008B1A RID: 35610
		private int startNum;

		// Token: 0x04008B1B RID: 35611
		private int endNum;

		// Token: 0x04008B1C RID: 35612
		private int MAX_STARTNUM;

		// Token: 0x04008B1D RID: 35613
		private int MAX_ENDNUM;

		// Token: 0x04008B1E RID: 35614
		private int plusNum;

		// Token: 0x04008B1F RID: 35615
		private int mustStart;

		// Token: 0x04008B20 RID: 35616
		private int mustEnd;

		// Token: 0x04008B21 RID: 35617
		private bool skipDownload;

		// Token: 0x04008B22 RID: 35618
		private float autofliptime;

		// Token: 0x04008B23 RID: 35619
		private int flipCountNum;

		// Token: 0x04008B24 RID: 35620
		private bool isCancel;

		// Token: 0x04008B25 RID: 35621
		private bool rebootFlag;

		// Token: 0x04008B26 RID: 35622
		private bool errCheckBGCardFrag;

		// Token: 0x04008B27 RID: 35623
		private List<int> builtinList;

		// Token: 0x04008B28 RID: 35624
		private int SelectorGoThroughPriority;

		// Token: 0x04008B29 RID: 35625
		private ViewController cardBrowserVC;

		// Token: 0x02000A56 RID: 2646
		public static class CreateRandom
		{
			// Token: 0x06004D64 RID: 19812 RVA: 0x0000216A File Offset: 0x0000036A
			public static global::System.Random Create()
			{
				return null;
			}

			// Token: 0x04008B2A RID: 35626
			private static global::System.Random random;
		}
	}
}
