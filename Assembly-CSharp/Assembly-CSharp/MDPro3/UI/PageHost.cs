using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013E8 RID: 5096
	public class PageHost : MonoBehaviour
	{
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06009385 RID: 37765 RVA: 0x0014DD24 File Offset: 0x0014BF24
		private ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06009386 RID: 37766 RVA: 0x0014DD58 File Offset: 0x0014BF58
		private SelectionButton ButtonRegulation
		{
			get
			{
				return this.m_ButtonRegulation = ((this.m_ButtonRegulation != null) ? this.m_ButtonRegulation : this.Manager.GetElement<SelectionButton>("ButtonRegulation"));
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06009387 RID: 37767 RVA: 0x0014DD94 File Offset: 0x0014BF94
		private SelectionButton ButtonPool
		{
			get
			{
				return this.m_ButtonPool = ((this.m_ButtonPool != null) ? this.m_ButtonPool : this.Manager.GetElement<SelectionButton>("ButtonPool"));
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06009388 RID: 37768 RVA: 0x0014DDD0 File Offset: 0x0014BFD0
		private SelectionButton ButtonMode
		{
			get
			{
				return this.m_ButtonMode = ((this.m_ButtonMode != null) ? this.m_ButtonMode : this.Manager.GetElement<SelectionButton>("ButtonMode"));
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06009389 RID: 37769 RVA: 0x0014DE0C File Offset: 0x0014C00C
		private SelectionToggle ToggleCheck
		{
			get
			{
				return this.m_ToggleCheck = ((this.m_ToggleCheck != null) ? this.m_ToggleCheck : this.Manager.GetElement<SelectionToggle>("ToggleCheck"));
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x0600938A RID: 37770 RVA: 0x0014DE48 File Offset: 0x0014C048
		private SelectionToggle ToggleShuffle
		{
			get
			{
				return this.m_ToggleShuffle = ((this.m_ToggleShuffle != null) ? this.m_ToggleShuffle : this.Manager.GetElement<SelectionToggle>("ToggleShuffle"));
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x0600938B RID: 37771 RVA: 0x0014DE84 File Offset: 0x0014C084
		private TMP_InputField InputTime
		{
			get
			{
				return this.m_InputTime = ((this.m_InputTime != null) ? this.m_InputTime : this.Manager.GetElement<TMP_InputField>("InputFieldTime"));
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x0600938C RID: 37772 RVA: 0x0014DEC0 File Offset: 0x0014C0C0
		private TMP_InputField InputLP
		{
			get
			{
				return this.m_InputLP = ((this.m_InputLP != null) ? this.m_InputLP : this.Manager.GetElement<TMP_InputField>("InputFieldLP"));
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x0600938D RID: 37773 RVA: 0x0014DEFC File Offset: 0x0014C0FC
		private TMP_InputField InputHand
		{
			get
			{
				return this.m_InputHand = ((this.m_InputHand != null) ? this.m_InputHand : this.Manager.GetElement<TMP_InputField>("InputFieldHand"));
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x0600938E RID: 37774 RVA: 0x0014DF38 File Offset: 0x0014C138
		private TMP_InputField InputDraw
		{
			get
			{
				return this.m_InputDraw = ((this.m_InputDraw != null) ? this.m_InputDraw : this.Manager.GetElement<TMP_InputField>("InputFieldDraw"));
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x0600938F RID: 37775 RVA: 0x0014DF74 File Offset: 0x0014C174
		private SelectionButton ButtonCreate
		{
			get
			{
				return this.m_ButtonCreate = ((this.m_ButtonCreate != null) ? this.m_ButtonCreate : this.Manager.GetElement<SelectionButton>("ButtonCreate"));
			}
		}

		// Token: 0x06009390 RID: 37776 RVA: 0x0014DFB0 File Offset: 0x0014C1B0
		private void Awake()
		{
			this.ResetArgs();
			SystemEvent.OnLanguageChange += this.ResetArgs;
		}

		// Token: 0x06009391 RID: 37777 RVA: 0x0014DFC9 File Offset: 0x0014C1C9
		private void OnDestroy()
		{
			SystemEvent.OnLanguageChange -= this.ResetArgs;
		}

		// Token: 0x06009392 RID: 37778 RVA: 0x0014DFDC File Offset: 0x0014C1DC
		private void ResetArgs()
		{
			this.ButtonRegulation.SetButtonText(BanlistManager.Banlists[0].Name);
			this.ButtonPool.SetButtonText(StringHelper.GetUnsafe(1481, 0));
			this.ButtonMode.SetButtonText(StringHelper.GetUnsafe(1244, 0));
			this.ToggleCheck.SetToggleOff(true);
			this.ToggleShuffle.SetToggleOff(true);
			this.InputTime.text = 180.ToString();
			this.InputLP.text = 8000.ToString();
			this.InputHand.text = 5.ToString();
			this.InputDraw.text = 1.ToString();
		}

		// Token: 0x06009393 RID: 37779 RVA: 0x0014E0A0 File Offset: 0x0014C2A0
		public void OnRegulation()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("禁限卡表", 0),
				string.Empty
			};
			foreach (Banlist list in BanlistManager.Banlists)
			{
				selections.Add(list.Name);
			}
			UIManager.ShowPopupSelection(selections, new Action(this.ChangeBanlist), null);
		}

		// Token: 0x06009394 RID: 37780 RVA: 0x0014E12C File Offset: 0x0014C32C
		private void ChangeBanlist()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			this.ButtonRegulation.SetButtonText(selected);
		}

		// Token: 0x06009395 RID: 37781 RVA: 0x0014E15C File Offset: 0x0014C35C
		public void OnPool()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("卡片允许", 0),
				string.Empty
			};
			for (int i = 1481; i < 1487; i++)
			{
				selections.Add(StringHelper.GetUnsafe(i, 0));
			}
			UIManager.ShowPopupSelection(selections, new Action(this.ChangePool), null);
		}

		// Token: 0x06009396 RID: 37782 RVA: 0x0014E1C0 File Offset: 0x0014C3C0
		private void ChangePool()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			this.ButtonPool.SetButtonText(selected);
		}

		// Token: 0x06009397 RID: 37783 RVA: 0x0014E1F0 File Offset: 0x0014C3F0
		public void OnMode()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("决斗模式", 0),
				string.Empty
			};
			for (int i = 1244; i < 1247; i++)
			{
				selections.Add(StringHelper.GetUnsafe(i, 0));
			}
			UIManager.ShowPopupSelection(selections, new Action(this.ChangeMode), null);
		}

		// Token: 0x06009398 RID: 37784 RVA: 0x0014E254 File Offset: 0x0014C454
		private void ChangeMode()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			this.Manager.GetElement<SelectionButton>("ButtonMode").SetButtonText(selected);
		}

		// Token: 0x06009399 RID: 37785 RVA: 0x0014E28C File Offset: 0x0014C48C
		public void OnHostCreate()
		{
			Program.instance.online.CreateServer(this.GerHostArgs());
		}

		// Token: 0x0600939A RID: 37786 RVA: 0x0014E2A4 File Offset: 0x0014C4A4
		private List<string> GerHostArgs()
		{
			return new List<string>
			{
				InterString.Get("创建主机", 0),
				this.ButtonRegulation.GetButtonText(),
				this.ButtonPool.GetButtonText(),
				this.ButtonMode.GetButtonText(),
				this.ToggleCheck.isOn ? "T" : "F",
				this.ToggleShuffle.isOn ? "T" : "F",
				this.GetTime().ToString(),
				this.GetLP().ToString(),
				this.GetHand().ToString(),
				this.GetDraw().ToString()
			};
		}

		// Token: 0x0600939B RID: 37787 RVA: 0x0014E388 File Offset: 0x0014C588
		private int GetTime()
		{
			int result;
			if (int.TryParse(this.InputTime.text, out result) && result >= 0)
			{
				return result;
			}
			this.InputTime.text = 180.ToString();
			return 180;
		}

		// Token: 0x0600939C RID: 37788 RVA: 0x0014E3CC File Offset: 0x0014C5CC
		private int GetLP()
		{
			int result;
			if (int.TryParse(this.InputLP.text, out result) && result > 0)
			{
				return result;
			}
			this.InputLP.text = 8000.ToString();
			return 8000;
		}

		// Token: 0x0600939D RID: 37789 RVA: 0x0014E410 File Offset: 0x0014C610
		private int GetHand()
		{
			int result;
			if (int.TryParse(this.InputHand.text, out result) && result > 0)
			{
				return result;
			}
			this.InputHand.text = 5.ToString();
			return 5;
		}

		// Token: 0x0600939E RID: 37790 RVA: 0x0014E44C File Offset: 0x0014C64C
		private int GetDraw()
		{
			int result;
			if (int.TryParse(this.InputDraw.text, out result) && result >= 0)
			{
				return result;
			}
			this.InputDraw.text = 1.ToString();
			return 1;
		}

		// Token: 0x0600939F RID: 37791 RVA: 0x0014E488 File Offset: 0x0014C688
		public void SelectDefault()
		{
			this.ButtonCreate.GetSelectable().Select();
		}

		// Token: 0x0400D1ED RID: 53741
		private ElementObjectManager m_Manager;

		// Token: 0x0400D1EE RID: 53742
		private const string LABEL_SBN_REGULATION = "ButtonRegulation";

		// Token: 0x0400D1EF RID: 53743
		private SelectionButton m_ButtonRegulation;

		// Token: 0x0400D1F0 RID: 53744
		private const string LABEL_SBN_POOL = "ButtonPool";

		// Token: 0x0400D1F1 RID: 53745
		private SelectionButton m_ButtonPool;

		// Token: 0x0400D1F2 RID: 53746
		private const string LABEL_SBN_MODE = "ButtonMode";

		// Token: 0x0400D1F3 RID: 53747
		private SelectionButton m_ButtonMode;

		// Token: 0x0400D1F4 RID: 53748
		private const string LABEL_STG_CHECK = "ToggleCheck";

		// Token: 0x0400D1F5 RID: 53749
		private SelectionToggle m_ToggleCheck;

		// Token: 0x0400D1F6 RID: 53750
		private const string LABEL_STG_SHUFFLE = "ToggleShuffle";

		// Token: 0x0400D1F7 RID: 53751
		private SelectionToggle m_ToggleShuffle;

		// Token: 0x0400D1F8 RID: 53752
		private const string LABEL_IPT_TIME = "InputFieldTime";

		// Token: 0x0400D1F9 RID: 53753
		private TMP_InputField m_InputTime;

		// Token: 0x0400D1FA RID: 53754
		private const string LABEL_IPT_LP = "InputFieldLP";

		// Token: 0x0400D1FB RID: 53755
		private TMP_InputField m_InputLP;

		// Token: 0x0400D1FC RID: 53756
		private const string LABEL_IPT_HAND = "InputFieldHand";

		// Token: 0x0400D1FD RID: 53757
		private TMP_InputField m_InputHand;

		// Token: 0x0400D1FE RID: 53758
		private const string LABEL_IPT_DRAW = "InputFieldDraw";

		// Token: 0x0400D1FF RID: 53759
		private TMP_InputField m_InputDraw;

		// Token: 0x0400D200 RID: 53760
		private const string LABEL_SBN_CREATE = "ButtonCreate";

		// Token: 0x0400D201 RID: 53761
		private SelectionButton m_ButtonCreate;
	}
}
