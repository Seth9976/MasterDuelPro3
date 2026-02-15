using System;
using System.Collections.Generic;
using MDPro3.Duel;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001470 RID: 5232
	public class OcgCoreUI : ServantUI
	{
		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06009809 RID: 38921 RVA: 0x0016528C File Offset: 0x0016348C
		public TextMeshProUGUI TextPlayer0Name
		{
			get
			{
				return this.m_TextPlayer0Name = ((this.m_TextPlayer0Name != null) ? this.m_TextPlayer0Name : base.Manager.GetElement<TextMeshProUGUI>("TextPlayer0Name"));
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x0600980A RID: 38922 RVA: 0x001652C8 File Offset: 0x001634C8
		public TextMeshProUGUI TextPlayer1Name
		{
			get
			{
				return this.m_TextPlayer1Name = ((this.m_TextPlayer1Name != null) ? this.m_TextPlayer1Name : base.Manager.GetElement<TextMeshProUGUI>("TextPlayer1Name"));
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x0600980B RID: 38923 RVA: 0x00165304 File Offset: 0x00163504
		public TextMeshProUGUI TextPlayer0LP
		{
			get
			{
				return this.m_TextPlayer0LP = ((this.m_TextPlayer0LP != null) ? this.m_TextPlayer0LP : base.Manager.GetElement<TextMeshProUGUI>("TextPlayer0LP"));
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x0600980C RID: 38924 RVA: 0x00165340 File Offset: 0x00163540
		public TextMeshProUGUI TextPlayer1LP
		{
			get
			{
				return this.m_TextPlayer1LP = ((this.m_TextPlayer1LP != null) ? this.m_TextPlayer1LP : base.Manager.GetElement<TextMeshProUGUI>("TextPlayer1LP"));
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x0600980D RID: 38925 RVA: 0x0016537C File Offset: 0x0016357C
		public Image AvatarPlayer0
		{
			get
			{
				return this.m_AvatarPlayer0 = ((this.m_AvatarPlayer0 != null) ? this.m_AvatarPlayer0 : base.Manager.GetElement<Image>("AvatarPlayer0"));
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x0600980E RID: 38926 RVA: 0x001653B8 File Offset: 0x001635B8
		public Image AvatarPlayer1
		{
			get
			{
				return this.m_AvatarPlayer1 = ((this.m_AvatarPlayer1 != null) ? this.m_AvatarPlayer1 : base.Manager.GetElement<Image>("AvatarPlayer1"));
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x0600980F RID: 38927 RVA: 0x001653F4 File Offset: 0x001635F4
		public GameObject Hint
		{
			get
			{
				return this.m_Hint = ((this.m_Hint != null) ? this.m_Hint : base.Manager.GetElement("Hint"));
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06009810 RID: 38928 RVA: 0x00165430 File Offset: 0x00163630
		public TextMeshProUGUI TextHint
		{
			get
			{
				return this.m_TextHint = ((this.m_TextHint != null) ? this.m_TextHint : base.Manager.GetElement<TextMeshProUGUI>("TextHint"));
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06009811 RID: 38929 RVA: 0x0016546C File Offset: 0x0016366C
		public RectTransform RectPopup
		{
			get
			{
				return this.m_RectPopup = ((this.m_RectPopup != null) ? this.m_RectPopup : base.Manager.GetElement<RectTransform>("Popup"));
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06009812 RID: 38930 RVA: 0x001654A8 File Offset: 0x001636A8
		private RectTransform RectPlaceCount
		{
			get
			{
				return this.m_RectPlaceCount = ((this.m_RectPlaceCount != null) ? this.m_RectPlaceCount : base.Manager.GetElement<RectTransform>("PlaceCount"));
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06009813 RID: 38931 RVA: 0x001654E4 File Offset: 0x001636E4
		private TextMeshProUGUI TextPlaceCount
		{
			get
			{
				return this.m_TextPlaceCount = ((this.m_TextPlaceCount != null) ? this.m_TextPlaceCount : base.Manager.GetElement<TextMeshProUGUI>("TextPlaceCount"));
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06009814 RID: 38932 RVA: 0x00165520 File Offset: 0x00163720
		public CardDescription CardDescription
		{
			get
			{
				return this.m_CardDescription = ((this.m_CardDescription != null) ? this.m_CardDescription : base.Manager.GetElement<CardDescription>("CardDescription"));
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06009815 RID: 38933 RVA: 0x0016555C File Offset: 0x0016375C
		public CardList CardList
		{
			get
			{
				return this.m_CardList = ((this.m_CardList != null) ? this.m_CardList : base.Manager.GetElement<CardList>("CardList"));
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06009816 RID: 38934 RVA: 0x00165598 File Offset: 0x00163798
		public DuelLog DuelLog
		{
			get
			{
				return this.m_DuelLog = ((this.m_DuelLog != null) ? this.m_DuelLog : base.Manager.GetElement<DuelLog>("DuelLog"));
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06009817 RID: 38935 RVA: 0x001655D4 File Offset: 0x001637D4
		public DuelErrorLog DuelErrorLog
		{
			get
			{
				return this.m_DuelErrorLog = ((this.m_DuelErrorLog != null) ? this.m_DuelErrorLog : base.Manager.GetElement<DuelErrorLog>("DuelErrorLog"));
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06009818 RID: 38936 RVA: 0x00165610 File Offset: 0x00163810
		public GameObject Buttons
		{
			get
			{
				return this.m_Buttons = ((this.m_Buttons != null) ? this.m_Buttons : base.Manager.GetElement("Buttons"));
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06009819 RID: 38937 RVA: 0x0016564C File Offset: 0x0016384C
		private Button ButtonStop
		{
			get
			{
				return this.m_ButtonStop = ((this.m_ButtonStop != null) ? this.m_ButtonStop : base.Manager.GetElement<Button>("ButtonStop"));
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x0600981A RID: 38938 RVA: 0x00165688 File Offset: 0x00163888
		private Button ButtonPlay
		{
			get
			{
				return this.m_ButtonPlay = ((this.m_ButtonPlay != null) ? this.m_ButtonPlay : base.Manager.GetElement<Button>("ButtonPlay"));
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x0600981B RID: 38939 RVA: 0x001656C4 File Offset: 0x001638C4
		private Button ButtonAcc
		{
			get
			{
				return this.m_ButtonAcc = ((this.m_ButtonAcc != null) ? this.m_ButtonAcc : base.Manager.GetElement<Button>("ButtonAcc"));
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x0600981C RID: 38940 RVA: 0x00165700 File Offset: 0x00163900
		private Button ButtonNor
		{
			get
			{
				return this.m_ButtonNor = ((this.m_ButtonNor != null) ? this.m_ButtonNor : base.Manager.GetElement<Button>("ButtonNor"));
			}
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600981D RID: 38941 RVA: 0x0016573C File Offset: 0x0016393C
		public Button ButtonTiming
		{
			get
			{
				return this.m_ButtonTiming = ((this.m_ButtonTiming != null) ? this.m_ButtonTiming : base.Manager.GetElement<Button>("ButtonTiming"));
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x0600981E RID: 38942 RVA: 0x00165778 File Offset: 0x00163978
		public Button ButtonLog
		{
			get
			{
				return this.m_ButtonLog = ((this.m_ButtonLog != null) ? this.m_ButtonLog : base.Manager.GetElement<Button>("ButtonLog"));
			}
		}

		// Token: 0x0600981F RID: 38943 RVA: 0x001657B4 File Offset: 0x001639B4
		public override void Show(bool cover)
		{
			this.ShowEvent();
		}

		// Token: 0x06009820 RID: 38944 RVA: 0x001657BC File Offset: 0x001639BC
		public override void ShowEvent()
		{
			base.ShowEvent();
			this.CloseHint();
			this.ButtonStop.gameObject.SetActive(true);
			this.ButtonPlay.gameObject.SetActive(false);
			this.ButtonAcc.gameObject.SetActive(true);
			this.ButtonNor.gameObject.SetActive(false);
			this.ButtonTiming.gameObject.SetActive(OcgCore.condition == OcgCore.Condition.Duel);
		}

		// Token: 0x06009821 RID: 38945 RVA: 0x00165831 File Offset: 0x00163A31
		public void SetHint(string hint)
		{
			this.Hint.SetActive(true);
			this.TextHint.text = hint;
		}

		// Token: 0x06009822 RID: 38946 RVA: 0x0016584B File Offset: 0x00163A4B
		public void CloseHint()
		{
			this.Hint.SetActive(false);
		}

		// Token: 0x06009823 RID: 38947 RVA: 0x0016585C File Offset: 0x00163A5C
		public void ShowLocationCount(GPS p)
		{
			Vector2 position = UIManager.WorldToScreenPoint(Program.instance.camera_.cameraMain, GameCard.GetCardPosition(p, null, null));
			if ((p.location & 65U) > 0U && p.controller == 0U)
			{
				position.y += 80f;
			}
			else if ((p.location & 65U) > 0U && p.controller == 1U)
			{
				position.y -= 50f;
			}
			else if ((p.location & 48U) > 0U && p.controller == 0U)
			{
				position.x -= 10f;
				position.y -= 10f;
				this.RectPlaceCount.localScale = new Vector3(-1f, 1f, 1f);
			}
			else if ((p.location & 48U) > 0U && p.controller == 1U)
			{
				position.x += 10f;
				position.y -= 10f;
				this.RectPlaceCount.localScale = new Vector3(1f, 1f, 1f);
			}
			if ((p.controller == 0U && (p.location & 64U) > 0U) || (p.controller == 1U && (p.location & 1U) > 0U))
			{
				position.x += 20f;
				this.RectPlaceCount.localScale = new Vector3(1f, 1f, 1f);
			}
			else if ((p.controller == 1U && (p.location & 64U) > 0U) || (p.controller == 0U && (p.location & 1U) > 0U))
			{
				position.x -= 20f;
				this.RectPlaceCount.localScale = new Vector3(-1f, 1f, 1f);
			}
			this.TextPlaceCount.rectTransform.localScale = this.RectPlaceCount.localScale;
			this.RectPlaceCount.anchoredPosition = position;
			this.RectPlaceCount.gameObject.SetActive(true);
			this.TextPlaceCount.text = Program.instance.ocgcore.GetLocationCardCount((CardLocation)p.location, p.controller).ToString();
		}

		// Token: 0x06009824 RID: 38948 RVA: 0x00165A98 File Offset: 0x00163C98
		public void HidePlaceCount()
		{
			if (this.RectPlaceCount.gameObject.activeSelf)
			{
				this.RectPlaceCount.gameObject.SetActive(false);
			}
		}

		// Token: 0x06009825 RID: 38949 RVA: 0x00165AC0 File Offset: 0x00163CC0
		public void ShowPopupYesOrNo(List<string> selections, Action confirmAction, Action cancelAction)
		{
			Addressables.InstantiateAsync("UI/PopupDuelYesOrNo.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(this.RectPopup, false);
				PopupDuelYesOrNo component = result.Result.GetComponent<PopupDuelYesOrNo>();
				component.exitable = false;
				component.selections = selections;
				component.confirmAction = confirmAction;
				component.cancelAction = cancelAction;
				component.Show();
			};
		}

		// Token: 0x06009826 RID: 38950 RVA: 0x00165B10 File Offset: 0x00163D10
		public void ShowPopupPhase(List<string> selections)
		{
			Addressables.InstantiateAsync("UI/PopupDuelPhase.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(this.RectPopup, false);
				PopupDuelPhase component = result.Result.GetComponent<PopupDuelPhase>();
				component.exitable = true;
				component.selections = selections;
				component.Show();
			};
		}

		// Token: 0x06009827 RID: 38951 RVA: 0x00165B54 File Offset: 0x00163D54
		public void ShowPopupSelectCard(string hint, List<GameCard> cards, int min, int max, bool exitable, bool sendable)
		{
			if (string.IsNullOrEmpty(hint))
			{
				hint = InterString.Get("请选择卡片", 0);
			}
			Addressables.InstantiateAsync("UI/PopupDuelSelectCard.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(this.RectPopup, false);
				PopupDuelSelectCard component = result.Result.GetComponent<PopupDuelSelectCard>();
				component.exitable = exitable;
				component.hint = hint;
				component.cards = cards;
				component.min = min;
				component.max = max;
				component.sendable = sendable;
				component.Show();
			};
		}

		// Token: 0x06009828 RID: 38952 RVA: 0x00165BDC File Offset: 0x00163DDC
		public void ShowPopupPosition(int code, int count, int option1 = 1, int option2 = 2)
		{
			Addressables.InstantiateAsync("UI/PopupDuelPosition.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(this.RectPopup, false);
				PopupDuelPosition component = result.Result.GetComponent<PopupDuelPosition>();
				component.exitable = false;
				component.count = count;
				component.code = code;
				component.option1 = option1;
				component.option2 = option2;
				component.Show();
			};
		}

		// Token: 0x06009829 RID: 38953 RVA: 0x00165C34 File Offset: 0x00163E34
		public void ShowPopupSelection(List<string> selections, List<int> responses)
		{
			Addressables.InstantiateAsync("UI/PopupDuelSelection.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(this.RectPopup, false);
				PopupDuelSelection component = result.Result.GetComponent<PopupDuelSelection>();
				component.exitable = false;
				component.selections = selections;
				component.responses = responses;
				component.Show();
			};
		}

		// Token: 0x0600982A RID: 38954 RVA: 0x00165C80 File Offset: 0x00163E80
		public void ShowPopupInput(List<string> selections, Action<string> confirmAction, Action cancelAction, InputValidation.ValidationType type = InputValidation.ValidationType.None)
		{
			Addressables.InstantiateAsync("UI/PopupDuelInput.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(this.RectPopup, false);
				PopupDuelInput component = result.Result.GetComponent<PopupDuelInput>();
				component.selections = selections;
				component.confirmAction = confirmAction;
				component.cancelAction = cancelAction;
				component.validationType = type;
				component.Show();
			};
		}

		// Token: 0x0600982B RID: 38955 RVA: 0x00165CD8 File Offset: 0x00163ED8
		public void ShowSaveReplay()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("保存回放", 0),
				InterString.Get("保存", 0),
				InterString.Get("放弃", 0),
				Tools.GetTimeString()
			};
			this.ShowPopupInput(selections, new Action<string>(this.OnSaveReplay), new Action(this.OnGiveUpReplay), InputValidation.ValidationType.Path);
		}

		// Token: 0x0600982C RID: 38956 RVA: 0x00165D4C File Offset: 0x00163F4C
		public void OnForcedSaveReplay()
		{
			List<string> selections = new List<string>
			{
				InterString.Get("保存回放", 0),
				InterString.Get("保存", 0),
				InterString.Get("放弃", 0),
				Tools.GetTimeString()
			};
			this.ShowPopupInput(selections, new Action<string>(this.OnSaveReplay), new Action(this.OnGiveUpReplay), InputValidation.ValidationType.Path);
		}

		// Token: 0x0600982D RID: 38957 RVA: 0x00165DBD File Offset: 0x00163FBD
		private void OnSaveReplay(string replayName)
		{
			TcpHelper.SaveRecord(replayName);
			Program.instance.ocgcore.returnAction = null;
			Program.instance.ocgcore.OnDuelResultConfirmed(false);
		}

		// Token: 0x0600982E RID: 38958 RVA: 0x00165DE5 File Offset: 0x00163FE5
		private void OnGiveUpReplay()
		{
			Program.instance.ocgcore.returnAction = null;
			Program.instance.ocgcore.OnDuelResultConfirmed(false);
		}

		// Token: 0x0600982F RID: 38959 RVA: 0x00165E07 File Offset: 0x00164007
		public void OnStop()
		{
			OcgCore.pause = true;
			this.ButtonStop.gameObject.SetActive(false);
			this.ButtonPlay.gameObject.SetActive(true);
		}

		// Token: 0x06009830 RID: 38960 RVA: 0x00165E31 File Offset: 0x00164031
		public void OnPlay()
		{
			OcgCore.pause = false;
			this.ButtonStop.gameObject.SetActive(true);
			this.ButtonPlay.gameObject.SetActive(false);
		}

		// Token: 0x06009831 RID: 38961 RVA: 0x00165E5C File Offset: 0x0016405C
		public void OnAcc()
		{
			OcgCore.Accing = true;
			float num;
			switch (OcgCore.condition)
			{
			case OcgCore.Condition.Duel:
				num = Config.GetFloat("DuelAcc", 2f);
				break;
			case OcgCore.Condition.Watch:
				num = Config.GetFloat("WatchAcc", 2f);
				break;
			case OcgCore.Condition.Replay:
				num = Config.GetFloat("ReplayAcc", 2f);
				break;
			default:
				num = 2f;
				break;
			}
			float targetSpeed = num;
			Program.instance.TimeScale = targetSpeed;
			this.ButtonAcc.gameObject.SetActive(false);
			this.ButtonNor.gameObject.SetActive(true);
			Program.instance.ocgcore.SetBgTimeScale(1f / targetSpeed);
		}

		// Token: 0x06009832 RID: 38962 RVA: 0x00165F14 File Offset: 0x00164114
		public void OnNor()
		{
			OcgCore.Accing = false;
			float targetSpeed = 1f;
			Program.instance.TimeScale = targetSpeed;
			this.ButtonAcc.gameObject.SetActive(true);
			this.ButtonNor.gameObject.SetActive(false);
			Program.instance.ocgcore.SetBgTimeScale(targetSpeed);
		}

		// Token: 0x06009833 RID: 38963 RVA: 0x00165F6A File Offset: 0x0016416A
		public void OnTiming()
		{
			OcgCore.chainCondition = (OcgCore.chainCondition + 1) % (OcgCore.ChainCondition)3;
			this.SetTimingIcon();
		}

		// Token: 0x06009834 RID: 38964 RVA: 0x00165F80 File Offset: 0x00164180
		private void SetTimingIcon()
		{
			SpriteState state = this.ButtonTiming.spriteState;
			switch (OcgCore.chainCondition)
			{
			case OcgCore.ChainCondition.No:
				this.ButtonTiming.GetComponent<Image>().sprite = TextureManager.container.offTiming[0];
				state.highlightedSprite = TextureManager.container.offTiming[1];
				state.pressedSprite = TextureManager.container.offTiming[2];
				break;
			case OcgCore.ChainCondition.All:
				this.ButtonTiming.GetComponent<Image>().sprite = TextureManager.container.onTiming[0];
				state.highlightedSprite = TextureManager.container.onTiming[1];
				state.pressedSprite = TextureManager.container.onTiming[2];
				break;
			case OcgCore.ChainCondition.Smart:
				this.ButtonTiming.GetComponent<Image>().sprite = TextureManager.container.autoTiming[0];
				state.highlightedSprite = TextureManager.container.autoTiming[1];
				state.pressedSprite = TextureManager.container.autoTiming[2];
				break;
			}
			this.ButtonTiming.spriteState = state;
		}

		// Token: 0x06009835 RID: 38965 RVA: 0x00166090 File Offset: 0x00164290
		public void OnLog(bool silent = false)
		{
			if (this.DuelLog.showing)
			{
				this.DuelLog.Hide(silent);
				this.ButtonLog.GetComponent<Image>().sprite = TextureManager.container.onLog[0];
				SpriteState state = this.ButtonLog.spriteState;
				state.highlightedSprite = TextureManager.container.onLog[1];
				state.pressedSprite = TextureManager.container.onLog[2];
				state.disabledSprite = TextureManager.container.onLog[3];
				this.ButtonLog.spriteState = state;
				return;
			}
			this.DuelLog.Show();
			this.ButtonLog.GetComponent<Image>().sprite = TextureManager.container.offLog[0];
			SpriteState state2 = this.ButtonLog.spriteState;
			state2.highlightedSprite = TextureManager.container.offLog[1];
			state2.pressedSprite = TextureManager.container.offLog[2];
			state2.disabledSprite = TextureManager.container.offLog[3];
			this.ButtonLog.spriteState = state2;
			this.CardList.Hide();
		}

		// Token: 0x06009836 RID: 38966 RVA: 0x001661A7 File Offset: 0x001643A7
		public void OnSetting()
		{
			Program.instance.ShowSubServant(Program.instance.setting);
		}

		// Token: 0x06009837 RID: 38967 RVA: 0x001661BD File Offset: 0x001643BD
		public void SwitchBgDetail(bool show)
		{
			if (show)
			{
				this.ShowBgDetail();
				return;
			}
			this.HideBgDetail();
		}

		// Token: 0x06009838 RID: 38968 RVA: 0x001661D0 File Offset: 0x001643D0
		private void ShowBgDetail()
		{
			OcgCore core = Program.instance.ocgcore;
			GameObject info = Program.instance.ocgcore.messageDispatcher.duel.duelBGManager.fieldSummonRightInfo;
			if (this.bgDetailShowing)
			{
				return;
			}
			this.bgDetailShowing = true;
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.ShowHiddenLabel();
			}
			if (info != null)
			{
				CameraManager.DuelOverlay3DPlus();
				info.SetActive(true);
				ElementObjectManager component = info.GetComponent<ElementObjectManager>();
				ElementObjectManager nearManager = component.GetElement<ElementObjectManager>("RootNear");
				ElementObjectManager element = component.GetElement<ElementObjectManager>("RootFar");
				nearManager.GetElement<TextMeshPro>("TextSummon").text = OcgCore.mySummonCount.ToString();
				nearManager.GetElement<TextMeshPro>("TextSpSummon").text = OcgCore.mySpSummonCount.ToString();
				element.GetElement<TextMeshPro>("TextSummon").text = OcgCore.opSummonCount.ToString();
				element.GetElement<TextMeshPro>("TextSpSummon").text = OcgCore.opSpSummonCount.ToString();
				nearManager.GetElement<TextMeshPro>("TextTotalAtk").text = core.GetAllAtk(true).ToString();
				element.GetElement<TextMeshPro>("TextTotalAtk").text = core.GetAllAtk(false).ToString();
				component.GetElement<TextMeshPro>("GraveNear").text = core.GetLocationCardCount(CardLocation.Grave, 0U).ToString();
				component.GetElement<TextMeshPro>("GraveFar").text = core.GetLocationCardCount(CardLocation.Grave, 1U).ToString();
				component.GetElement<TextMeshPro>("ExcludeNear").text = core.GetLocationCardCount(CardLocation.Removed, 0U).ToString();
				component.GetElement<TextMeshPro>("ExcludeFar").text = core.GetLocationCardCount(CardLocation.Removed, 1U).ToString();
				component.GetElement<TextMeshPro>("DeckNear").text = core.GetLocationCardCount(CardLocation.Deck, 0U).ToString();
				component.GetElement<TextMeshPro>("DeckFar").text = core.GetLocationCardCount(CardLocation.Deck, 1U).ToString();
				component.GetElement<TextMeshPro>("ExtraNear").text = core.GetLocationCardCount(CardLocation.Extra, 0U).ToString();
				component.GetElement<TextMeshPro>("ExtraFar").text = core.GetLocationCardCount(CardLocation.Extra, 1U).ToString();
				component.GetElement<TextMeshPro>("HandNear").text = core.GetLocationCardCount(CardLocation.Hand, 0U).ToString();
				component.GetElement<TextMeshPro>("HandFar").text = core.GetLocationCardCount(CardLocation.Hand, 1U).ToString();
			}
		}

		// Token: 0x06009839 RID: 38969 RVA: 0x00166484 File Offset: 0x00164684
		private void HideBgDetail()
		{
			GameObject info = Program.instance.ocgcore.messageDispatcher.duel.duelBGManager.fieldSummonRightInfo;
			if (!this.bgDetailShowing)
			{
				return;
			}
			this.bgDetailShowing = false;
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.HideHiddenLabel();
			}
			if (info != null)
			{
				CameraManager.DuelOverlay3DMinus();
				info.SetActive(false);
			}
		}

		// Token: 0x0600983A RID: 38970 RVA: 0x0012A6B8 File Offset: 0x001288B8
		public void ToChat()
		{
			if (OcgCore.condition == OcgCore.Condition.Replay || OcgCore.inPuzzle)
			{
				return;
			}
			Program.instance.ui_.chatPanel.Switch();
		}

		// Token: 0x0400D620 RID: 54816
		private const string LABEL_TXT_PLAYER0NAME = "TextPlayer0Name";

		// Token: 0x0400D621 RID: 54817
		private TextMeshProUGUI m_TextPlayer0Name;

		// Token: 0x0400D622 RID: 54818
		private const string LABEL_TXT_PLAYER1NAME = "TextPlayer1Name";

		// Token: 0x0400D623 RID: 54819
		private TextMeshProUGUI m_TextPlayer1Name;

		// Token: 0x0400D624 RID: 54820
		private const string LABEL_TXT_PLAYER0LP = "TextPlayer0LP";

		// Token: 0x0400D625 RID: 54821
		private TextMeshProUGUI m_TextPlayer0LP;

		// Token: 0x0400D626 RID: 54822
		private const string LABEL_TXT_PLAYER1LP = "TextPlayer1LP";

		// Token: 0x0400D627 RID: 54823
		private TextMeshProUGUI m_TextPlayer1LP;

		// Token: 0x0400D628 RID: 54824
		private const string LABEL_IMG_AVATARPLAYER0 = "AvatarPlayer0";

		// Token: 0x0400D629 RID: 54825
		private Image m_AvatarPlayer0;

		// Token: 0x0400D62A RID: 54826
		private const string LABEL_IMG_AVATARPLAYER1 = "AvatarPlayer1";

		// Token: 0x0400D62B RID: 54827
		private Image m_AvatarPlayer1;

		// Token: 0x0400D62C RID: 54828
		private const string LABEL_GO_HINT = "Hint";

		// Token: 0x0400D62D RID: 54829
		private GameObject m_Hint;

		// Token: 0x0400D62E RID: 54830
		private const string LABEL_TXT_HINT = "TextHint";

		// Token: 0x0400D62F RID: 54831
		private TextMeshProUGUI m_TextHint;

		// Token: 0x0400D630 RID: 54832
		private const string LABEL_RT_POPUP = "Popup";

		// Token: 0x0400D631 RID: 54833
		private RectTransform m_RectPopup;

		// Token: 0x0400D632 RID: 54834
		private const string LABEL_RT_PLACECOUNT = "PlaceCount";

		// Token: 0x0400D633 RID: 54835
		private RectTransform m_RectPlaceCount;

		// Token: 0x0400D634 RID: 54836
		private const string LABEL_TXT_PLACECOUNT = "TextPlaceCount";

		// Token: 0x0400D635 RID: 54837
		private TextMeshProUGUI m_TextPlaceCount;

		// Token: 0x0400D636 RID: 54838
		private const string LABEL_MONO_CARD_DESCRIPTION = "CardDescription";

		// Token: 0x0400D637 RID: 54839
		private CardDescription m_CardDescription;

		// Token: 0x0400D638 RID: 54840
		private const string LABEL_MONO_CARD_LIST = "CardList";

		// Token: 0x0400D639 RID: 54841
		private CardList m_CardList;

		// Token: 0x0400D63A RID: 54842
		private const string LABEL_MONO_DUEL_LOG = "DuelLog";

		// Token: 0x0400D63B RID: 54843
		private DuelLog m_DuelLog;

		// Token: 0x0400D63C RID: 54844
		private const string LABEL_MONO_DUEL_ERROR_LOG = "DuelErrorLog";

		// Token: 0x0400D63D RID: 54845
		private DuelErrorLog m_DuelErrorLog;

		// Token: 0x0400D63E RID: 54846
		private const string LABEL_GO_BUTTONS = "Buttons";

		// Token: 0x0400D63F RID: 54847
		private GameObject m_Buttons;

		// Token: 0x0400D640 RID: 54848
		private const string LABEL_BTN_STOP = "ButtonStop";

		// Token: 0x0400D641 RID: 54849
		private Button m_ButtonStop;

		// Token: 0x0400D642 RID: 54850
		private const string LABEL_BTN_PLAY = "ButtonPlay";

		// Token: 0x0400D643 RID: 54851
		private Button m_ButtonPlay;

		// Token: 0x0400D644 RID: 54852
		private const string LABEL_BTN_ACC = "ButtonAcc";

		// Token: 0x0400D645 RID: 54853
		private Button m_ButtonAcc;

		// Token: 0x0400D646 RID: 54854
		private const string LABEL_BTN_NOR = "ButtonNor";

		// Token: 0x0400D647 RID: 54855
		private Button m_ButtonNor;

		// Token: 0x0400D648 RID: 54856
		private const string LABEL_BTN_TIMING = "ButtonTiming";

		// Token: 0x0400D649 RID: 54857
		private Button m_ButtonTiming;

		// Token: 0x0400D64A RID: 54858
		private const string LABEL_BTN_LOG = "ButtonLog";

		// Token: 0x0400D64B RID: 54859
		private Button m_ButtonLog;

		// Token: 0x0400D64C RID: 54860
		private bool bgDetailShowing;
	}
}
