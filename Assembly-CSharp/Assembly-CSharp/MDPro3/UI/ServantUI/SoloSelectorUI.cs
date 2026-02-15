using System;
using System.Collections.Generic;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.PropertyOverride;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001484 RID: 5252
	public class SoloSelectorUI : ServantUI
	{
		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x060099CB RID: 39371 RVA: 0x0016EB98 File Offset: 0x0016CD98
		public ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x060099CC RID: 39372 RVA: 0x0016EBD4 File Offset: 0x0016CDD4
		protected TextMeshProUGUI TextOverview
		{
			get
			{
				return this.m_TextOverview = ((this.m_TextOverview != null) ? this.m_TextOverview : base.Manager.GetElement<TextMeshProUGUI>("TextOverview"));
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x060099CD RID: 39373 RVA: 0x0016EC10 File Offset: 0x0016CE10
		protected SelectionButton ButtonDeck
		{
			get
			{
				return this.m_ButtonDeck = ((this.m_ButtonDeck != null) ? this.m_ButtonDeck : base.Manager.GetElement<SelectionButton>("ButtonDeck"));
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x060099CE RID: 39374 RVA: 0x0016EC4C File Offset: 0x0016CE4C
		public SelectionToggle ToggleLockHand
		{
			get
			{
				return this.m_ToggleLockHand = ((this.m_ToggleLockHand != null) ? this.m_ToggleLockHand : base.Manager.GetNestedElement<SelectionToggle>("Settings/ToggleLockHand"));
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x060099CF RID: 39375 RVA: 0x0016EC88 File Offset: 0x0016CE88
		protected SelectionToggle ToggleNoCheck
		{
			get
			{
				return this.m_ToggleNoCheck = ((this.m_ToggleNoCheck != null) ? this.m_ToggleNoCheck : base.Manager.GetNestedElement<SelectionToggle>("Settings/ToggleNoCheck"));
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x060099D0 RID: 39376 RVA: 0x0016ECC4 File Offset: 0x0016CEC4
		protected SelectionToggle ToggleNoShuffle
		{
			get
			{
				return this.m_ToggleNoShuffle = ((this.m_ToggleNoShuffle != null) ? this.m_ToggleNoShuffle : base.Manager.GetNestedElement<SelectionToggle>("Settings/ToggleNoShuffle"));
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x060099D1 RID: 39377 RVA: 0x0016ED00 File Offset: 0x0016CF00
		protected TMP_InputField InputPort
		{
			get
			{
				return this.m_InputPort = ((this.m_InputPort != null) ? this.m_InputPort : base.Manager.GetNestedElement<TMP_InputField>("Settings/InputFieldPort"));
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x060099D2 RID: 39378 RVA: 0x0016ED3C File Offset: 0x0016CF3C
		protected TMP_InputField InputLP
		{
			get
			{
				return this.m_InputLP = ((this.m_InputLP != null) ? this.m_InputLP : base.Manager.GetNestedElement<TMP_InputField>("Settings/InputFieldLP"));
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x060099D3 RID: 39379 RVA: 0x0016ED78 File Offset: 0x0016CF78
		protected TMP_InputField InputHand
		{
			get
			{
				return this.m_InputHand = ((this.m_InputHand != null) ? this.m_InputHand : base.Manager.GetNestedElement<TMP_InputField>("Settings/InputFieldHand"));
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x060099D4 RID: 39380 RVA: 0x0016EDB4 File Offset: 0x0016CFB4
		protected TMP_InputField InputDraw
		{
			get
			{
				return this.m_InputDraw = ((this.m_InputDraw != null) ? this.m_InputDraw : base.Manager.GetNestedElement<TMP_InputField>("Settings/InputFieldDraw"));
			}
		}

		// Token: 0x060099D5 RID: 39381 RVA: 0x0016EDF0 File Offset: 0x0016CFF0
		public override void Initialize(Servant servant)
		{
			base.Initialize(servant);
			this.ButtonDeck.gameObject.SetActive(false);
		}

		// Token: 0x060099D6 RID: 39382 RVA: 0x0016EE0C File Offset: 0x0016D00C
		public override void ShowEvent()
		{
			base.ShowEvent();
			SoloSelector.Condition condition = SoloSelector.condition;
			if (condition != SoloSelector.Condition.ForSolo)
			{
				if (condition == SoloSelector.Condition.ForRoom)
				{
					this.ToggleNoCheck.gameObject.SetActive(false);
					this.ToggleNoShuffle.gameObject.SetActive(false);
					this.InputPort.gameObject.SetActive(false);
					this.InputLP.gameObject.SetActive(false);
					this.InputHand.gameObject.SetActive(false);
					this.InputDraw.gameObject.SetActive(false);
				}
			}
			else
			{
				this.ToggleNoCheck.gameObject.SetActive(true);
				this.ToggleNoShuffle.gameObject.SetActive(true);
				this.InputPort.gameObject.SetActive(true);
				this.InputLP.gameObject.SetActive(true);
				this.InputHand.gameObject.SetActive(true);
				this.InputDraw.gameObject.SetActive(true);
				YgoServer.StopServer();
			}
			this.ButtonDeck.SetButtonText(Config.GetConfigDeckName(true));
		}

		// Token: 0x060099D7 RID: 39383 RVA: 0x0016EF18 File Offset: 0x0016D118
		public void Print()
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			List<string[]> tasks = new List<string[]>();
			for (int i = 0; i < SoloSelector.bots.Count; i++)
			{
				string[] task = new string[] { i.ToString() };
				tasks.Add(task);
			}
			Addressables.LoadAssetAsync<GameObject>("UI/ItemSolo.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				float itemHeight = (PropertyOverrider.NeedMobileLayout() ? 180f : 150f);
				float topPadding = (PropertyOverrider.NeedMobileLayout() ? 148f : 134f);
				float space = itemHeight - (PropertyOverrider.NeedMobileLayout() ? 152f : 122f);
				float bottomPadding = (PropertyOverrider.NeedMobileLayout() ? 64f : 54f) - space;
				this.superScrollView = new SuperScrollView(1, 700f, itemHeight, topPadding, bottomPadding, result.Result, new Action<string[], GameObject>(this.ItemOnListRefresh), this.ScrollRect, 2);
				this.superScrollView.Print(tasks);
				if (this.superScrollView.items.Count > 0)
				{
					SelectionToggle_Solo item0 = this.superScrollView.items[0].gameObject.GetComponent<SelectionToggle_Solo>();
					if (Cursor.lockState == CursorLockMode.Locked)
					{
						item0.GetSelectable().Select();
						return;
					}
					Program.instance.solo.lastSoloItem = item0;
					item0.SetToggleOn(true);
				}
			};
		}

		// Token: 0x060099D8 RID: 39384 RVA: 0x0016EFA0 File Offset: 0x0016D1A0
		private void ItemOnListRefresh(string[] task, GameObject item)
		{
			SelectionToggle_Solo handler = item.GetComponent<SelectionToggle_Solo>();
			handler.index = int.Parse(task[0]);
			handler.botInfo = SoloSelector.bots[handler.index];
			handler.Refresh();
		}

		// Token: 0x060099D9 RID: 39385 RVA: 0x0016EFDE File Offset: 0x0016D1DE
		public bool IsLockHand()
		{
			return this.ToggleLockHand.isOn;
		}

		// Token: 0x060099DA RID: 39386 RVA: 0x0016EFEB File Offset: 0x0016D1EB
		public bool IsNoCheck()
		{
			return this.ToggleNoCheck.isOn;
		}

		// Token: 0x060099DB RID: 39387 RVA: 0x0016EFF8 File Offset: 0x0016D1F8
		public bool IsNoShuffle()
		{
			return this.ToggleNoShuffle.isOn;
		}

		// Token: 0x060099DC RID: 39388 RVA: 0x0016F008 File Offset: 0x0016D208
		public int GetPort()
		{
			int result;
			if (int.TryParse(this.InputPort.text, out result) && result > 0 && result <= 65535)
			{
				return result;
			}
			this.InputPort.text = 7911.ToString();
			return 7911;
		}

		// Token: 0x060099DD RID: 39389 RVA: 0x0016F054 File Offset: 0x0016D254
		public int GetLP()
		{
			int result;
			if (int.TryParse(this.InputLP.text, out result) && result > 0)
			{
				return result;
			}
			this.InputLP.text = 8000.ToString();
			return 8000;
		}

		// Token: 0x060099DE RID: 39390 RVA: 0x0016F098 File Offset: 0x0016D298
		public int GetHand()
		{
			int result;
			if (int.TryParse(this.InputHand.text, out result) && result > 0)
			{
				return result;
			}
			this.InputHand.text = 5.ToString();
			return 5;
		}

		// Token: 0x060099DF RID: 39391 RVA: 0x0016F0D4 File Offset: 0x0016D2D4
		public int GetDraw()
		{
			int result;
			if (int.TryParse(this.InputDraw.text, out result) && result >= 0)
			{
				return result;
			}
			this.InputDraw.text = 1.ToString();
			return 1;
		}

		// Token: 0x060099E0 RID: 39392 RVA: 0x0016F110 File Offset: 0x0016D310
		public void SetOverview(string text, bool activateDeckButton)
		{
			this.TextOverview.text = text;
			this.ButtonDeck.gameObject.SetActive(activateDeckButton);
		}

		// Token: 0x060099E1 RID: 39393 RVA: 0x0016F12F File Offset: 0x0016D32F
		public string GetAIDeck()
		{
			return this.ButtonDeck.GetButtonText();
		}

		// Token: 0x060099E2 RID: 39394 RVA: 0x0016F13C File Offset: 0x0016D33C
		public void OnPlay()
		{
			if (SoloSelector.condition == SoloSelector.Condition.ForSolo)
			{
				Program.instance.solo.StartAIForSolo(this.superScrollView.selected, this.ButtonDeck.gameObject.activeSelf);
				return;
			}
			Program.instance.solo.StartAIForRoom(this.superScrollView.selected, this.ButtonDeck.gameObject.activeSelf);
		}

		// Token: 0x060099E3 RID: 39395 RVA: 0x0016F1A5 File Offset: 0x0016D3A5
		public void SelectLastSoloItem()
		{
			UserInput.NextSelectionIsAxis = true;
			Program.instance.solo.Select(false);
		}

		// Token: 0x060099E4 RID: 39396 RVA: 0x0016F1BD File Offset: 0x0016D3BD
		public void SelectOnRight()
		{
			this.ToggleLockHand.GetSelectable().Select();
		}

		// Token: 0x060099E5 RID: 39397 RVA: 0x0016F1CF File Offset: 0x0016D3CF
		public void OnSelectAIDeck()
		{
			Program.instance.deckSelector.SwitchCondition(DeckSelector.Condition.ForSolo);
			Program.instance.ShiftToServant(Program.instance.deckSelector);
		}

		// Token: 0x0400D783 RID: 55171
		private const string LABEL_SR = "ScrollRect";

		// Token: 0x0400D784 RID: 55172
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D785 RID: 55173
		private const string LABEL_TXT_OVERVIEW = "TextOverview";

		// Token: 0x0400D786 RID: 55174
		private TextMeshProUGUI m_TextOverview;

		// Token: 0x0400D787 RID: 55175
		private const string LABEL_SBN_DECK = "ButtonDeck";

		// Token: 0x0400D788 RID: 55176
		private SelectionButton m_ButtonDeck;

		// Token: 0x0400D789 RID: 55177
		private const string LABEL_STG_LOCKHAND = "Settings/ToggleLockHand";

		// Token: 0x0400D78A RID: 55178
		private SelectionToggle m_ToggleLockHand;

		// Token: 0x0400D78B RID: 55179
		private const string LABEL_STG_NOCHECK = "Settings/ToggleNoCheck";

		// Token: 0x0400D78C RID: 55180
		private SelectionToggle m_ToggleNoCheck;

		// Token: 0x0400D78D RID: 55181
		private const string LABEL_STG_NOSHUFFLE = "Settings/ToggleNoShuffle";

		// Token: 0x0400D78E RID: 55182
		private SelectionToggle m_ToggleNoShuffle;

		// Token: 0x0400D78F RID: 55183
		private const string LABEL_IPT_PORT = "Settings/InputFieldPort";

		// Token: 0x0400D790 RID: 55184
		private TMP_InputField m_InputPort;

		// Token: 0x0400D791 RID: 55185
		private const string LABEL_IPT_LP = "Settings/InputFieldLP";

		// Token: 0x0400D792 RID: 55186
		private TMP_InputField m_InputLP;

		// Token: 0x0400D793 RID: 55187
		private const string LABEL_IPT_HAND = "Settings/InputFieldHand";

		// Token: 0x0400D794 RID: 55188
		private TMP_InputField m_InputHand;

		// Token: 0x0400D795 RID: 55189
		private const string LABEL_IPT_DRAW = "Settings/InputFieldDraw";

		// Token: 0x0400D796 RID: 55190
		private TMP_InputField m_InputDraw;

		// Token: 0x0400D797 RID: 55191
		private const int DEFAULT_PORT = 7911;

		// Token: 0x0400D798 RID: 55192
		private const int DEFAULT_LP = 8000;

		// Token: 0x0400D799 RID: 55193
		private const int DEFAULT_HAND = 5;

		// Token: 0x0400D79A RID: 55194
		private const int DEFAULT_DRAW = 1;

		// Token: 0x0400D79B RID: 55195
		public SuperScrollView superScrollView;
	}
}
