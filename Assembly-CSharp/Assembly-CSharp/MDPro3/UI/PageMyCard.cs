using System;
using System.Collections.Generic;
using MDPro3.Net;
using MDPro3.Servant;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013EC RID: 5100
	public class PageMyCard : MonoBehaviour
	{
		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x060093BE RID: 37822 RVA: 0x0014EDD0 File Offset: 0x0014CFD0
		private ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x060093BF RID: 37823 RVA: 0x0014EE04 File Offset: 0x0014D004
		public GameObject PageLogin
		{
			get
			{
				return this.m_PageLogin = ((this.m_PageLogin != null) ? this.m_PageLogin : this.Manager.GetElement("PageLogin"));
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x060093C0 RID: 37824 RVA: 0x0014EE40 File Offset: 0x0014D040
		private TMP_InputField InputAccount
		{
			get
			{
				return this.m_InputAccount = ((this.m_InputAccount != null) ? this.m_InputAccount : this.Manager.GetNestedElement<TMP_InputField>("PageLogin/InputFieldAccount"));
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x060093C1 RID: 37825 RVA: 0x0014EE7C File Offset: 0x0014D07C
		private TMP_InputField InputPassword
		{
			get
			{
				return this.m_InputPassword = ((this.m_InputPassword != null) ? this.m_InputPassword : this.Manager.GetNestedElement<TMP_InputField>("PageLogin/InputFieldPassword"));
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x060093C2 RID: 37826 RVA: 0x0014EEB8 File Offset: 0x0014D0B8
		private SelectionButton ButtonLogin
		{
			get
			{
				return this.m_ButtonLogin = ((this.m_ButtonLogin != null) ? this.m_ButtonLogin : this.Manager.GetNestedElement<SelectionButton>("PageLogin/ButtonLogin"));
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x060093C3 RID: 37827 RVA: 0x0014EEF4 File Offset: 0x0014D0F4
		public GameObject PageFunction
		{
			get
			{
				return this.m_PageFunction = ((this.m_PageFunction != null) ? this.m_PageFunction : this.Manager.GetElement("PageFunction"));
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x060093C4 RID: 37828 RVA: 0x0014EF30 File Offset: 0x0014D130
		public UserProfile UserProfile
		{
			get
			{
				return this.m_UserProfile = ((this.m_UserProfile != null) ? this.m_UserProfile : this.Manager.GetNestedElement<UserProfile>("PageFunction/UserProfile"));
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x060093C5 RID: 37829 RVA: 0x0014EF6C File Offset: 0x0014D16C
		public SelectionButton_DeckSelector ButtonDeckSelector
		{
			get
			{
				return this.m_ButtonDeckSelector = ((this.m_ButtonDeckSelector != null) ? this.m_ButtonDeckSelector : this.Manager.GetNestedElement<SelectionButton_DeckSelector>("PageFunction/DeckSelector"));
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x060093C6 RID: 37830 RVA: 0x0014EFA8 File Offset: 0x0014D1A8
		public WatchListHandler WatchList
		{
			get
			{
				return this.m_WatchList = ((this.m_WatchList != null) ? this.m_WatchList : this.Manager.GetNestedElement<WatchListHandler>("PageFunction/WatchList"));
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x060093C7 RID: 37831 RVA: 0x0014EFE4 File Offset: 0x0014D1E4
		private TMP_InputField InputDuelist
		{
			get
			{
				return this.m_InputDuelist = ((this.m_InputDuelist != null) ? this.m_InputDuelist : this.Manager.GetNestedElement<TMP_InputField>("PageFunction/InputFieldDuelist"));
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x060093C8 RID: 37832 RVA: 0x0014F020 File Offset: 0x0014D220
		public SelectionButton ButtonAMatch
		{
			get
			{
				return this.m_ButtonAMatch = ((this.m_ButtonAMatch != null) ? this.m_ButtonAMatch : this.Manager.GetNestedElement<SelectionButton>("PageFunction/ButtonAMatch"));
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x060093C9 RID: 37833 RVA: 0x0014F05C File Offset: 0x0014D25C
		public SelectionButton ButtonEMatch
		{
			get
			{
				return this.m_ButtonEMatch = ((this.m_ButtonEMatch != null) ? this.m_ButtonEMatch : this.Manager.GetNestedElement<SelectionButton>("PageFunction/ButtonEMatch"));
			}
		}

		// Token: 0x060093CA RID: 37834 RVA: 0x0014F098 File Offset: 0x0014D298
		public void OnMyCardRegister()
		{
			Application.OpenURL("https://accounts.moecube.com/signup");
		}

		// Token: 0x060093CB RID: 37835 RVA: 0x0014F0A4 File Offset: 0x0014D2A4
		public void OnMyCardLogin()
		{
			Program.instance.online.LoginMyCard(this.InputAccount.text, this.InputPassword.text);
		}

		// Token: 0x060093CC RID: 37836 RVA: 0x0014F0CC File Offset: 0x0014D2CC
		public void OnExitLogin()
		{
			UIManager.ShowPopupYesOrNo(new List<string>
			{
				InterString.Get("退出登录", 0),
				InterString.Get("是否确认退出登录？", 0),
				InterString.Get("确认", 0),
				InterString.Get("取消", 0)
			}, new Action(this.ExitLogin), null);
		}

		// Token: 0x060093CD RID: 37837 RVA: 0x0014F134 File Offset: 0x0014D334
		private void ExitLogin()
		{
			Config.Set("MyCardToken", "0");
			Config.Save();
			MyCard.account = null;
			MyCard.CloseAthleticWatchListWebSocket();
			this.ActivePageLogin();
		}

		// Token: 0x060093CE RID: 37838 RVA: 0x0014F15B File Offset: 0x0014D35B
		public void SelectDefault()
		{
			if (this.PageLogin.activeSelf)
			{
				this.ButtonLogin.GetSelectable().Select();
				return;
			}
			this.ButtonAMatch.GetSelectable().Select();
		}

		// Token: 0x060093CF RID: 37839 RVA: 0x0014F18B File Offset: 0x0014D38B
		public void OnDeckSelect()
		{
			Program.instance.deckSelector.SwitchCondition(DeckSelector.Condition.MyCard);
			Program.instance.ShiftToServant(Program.instance.deckSelector);
		}

		// Token: 0x060093D0 RID: 37840 RVA: 0x0014F1B1 File Offset: 0x0014D3B1
		public void OnEntertainMatch()
		{
			Program.instance.online.EntertainMatch();
		}

		// Token: 0x060093D1 RID: 37841 RVA: 0x0014F1C2 File Offset: 0x0014D3C2
		public void SelectLastWatchItem()
		{
			if (Program.instance.online.lastSelectedWatchItem != null)
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.online.lastSelectedWatchItem.GetSelectable().Select();
			}
		}

		// Token: 0x060093D2 RID: 37842 RVA: 0x0014F1FA File Offset: 0x0014D3FA
		public void OnAthleticMatch()
		{
			Program.instance.online.AthleticMatch();
		}

		// Token: 0x060093D3 RID: 37843 RVA: 0x0014F20B File Offset: 0x0014D40B
		public void ActivePageLogin()
		{
			this.PageLogin.SetActive(true);
			this.PageFunction.SetActive(false);
			this.InputAccount.text = string.Empty;
			this.InputPassword.text = string.Empty;
		}

		// Token: 0x060093D4 RID: 37844 RVA: 0x0014F245 File Offset: 0x0014D445
		public void ActivePageFunction()
		{
			this.PageLogin.SetActive(false);
			this.PageFunction.SetActive(true);
		}

		// Token: 0x0400D21B RID: 53787
		private ElementObjectManager m_Manager;

		// Token: 0x0400D21C RID: 53788
		private const string LABEL_GO_PAGELOGIN = "PageLogin";

		// Token: 0x0400D21D RID: 53789
		private GameObject m_PageLogin;

		// Token: 0x0400D21E RID: 53790
		private const string LABEL_IPT_ACCOUNT = "PageLogin/InputFieldAccount";

		// Token: 0x0400D21F RID: 53791
		private TMP_InputField m_InputAccount;

		// Token: 0x0400D220 RID: 53792
		private const string LABEL_IPT_PASSWORD = "PageLogin/InputFieldPassword";

		// Token: 0x0400D221 RID: 53793
		private TMP_InputField m_InputPassword;

		// Token: 0x0400D222 RID: 53794
		private const string LABEL_SBN_LOGIN = "PageLogin/ButtonLogin";

		// Token: 0x0400D223 RID: 53795
		private SelectionButton m_ButtonLogin;

		// Token: 0x0400D224 RID: 53796
		private const string LABEL_GO_PAGEFUNCTION = "PageFunction";

		// Token: 0x0400D225 RID: 53797
		private GameObject m_PageFunction;

		// Token: 0x0400D226 RID: 53798
		private const string LABEL_MONO_USERPROFILE = "PageFunction/UserProfile";

		// Token: 0x0400D227 RID: 53799
		private UserProfile m_UserProfile;

		// Token: 0x0400D228 RID: 53800
		private const string LABEL_SBN_DECKSELECTOR = "PageFunction/DeckSelector";

		// Token: 0x0400D229 RID: 53801
		private SelectionButton_DeckSelector m_ButtonDeckSelector;

		// Token: 0x0400D22A RID: 53802
		private const string LABEL_MONO_WATCHLIST = "PageFunction/WatchList";

		// Token: 0x0400D22B RID: 53803
		private WatchListHandler m_WatchList;

		// Token: 0x0400D22C RID: 53804
		private const string LABEL_IPT_DUELIST = "PageFunction/InputFieldDuelist";

		// Token: 0x0400D22D RID: 53805
		private TMP_InputField m_InputDuelist;

		// Token: 0x0400D22E RID: 53806
		private const string LABEL_SBN_AMATCH = "PageFunction/ButtonAMatch";

		// Token: 0x0400D22F RID: 53807
		private SelectionButton m_ButtonAMatch;

		// Token: 0x0400D230 RID: 53808
		private const string LABEL_SBN_EMatch = "PageFunction/ButtonEMatch";

		// Token: 0x0400D231 RID: 53809
		private SelectionButton m_ButtonEMatch;
	}
}
