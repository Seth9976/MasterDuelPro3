using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001415 RID: 5141
	public class CardActionMenu : UIWidgetCardBase
	{
		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x060094A4 RID: 38052 RVA: 0x001535F0 File Offset: 0x001517F0
		protected SelectionButton ButtonNext
		{
			get
			{
				return this.m_ButtonNext = ((this.m_ButtonNext != null) ? this.m_ButtonNext : base.Manager.GetElement<SelectionButton>("NextButton"));
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x060094A5 RID: 38053 RVA: 0x0015362C File Offset: 0x0015182C
		protected SelectionButton ButtonPrev
		{
			get
			{
				return this.m_ButtonPrev = ((this.m_ButtonPrev != null) ? this.m_ButtonPrev : base.Manager.GetElement<SelectionButton>("PrevButton"));
			}
		}

		// Token: 0x060094A6 RID: 38054 RVA: 0x00153668 File Offset: 0x00151868
		protected override void Awake()
		{
			base.Awake();
			this.CG.alpha = 0f;
			this.CG.blocksRaycasts = false;
			base.ImageCard.GetComponent<Button>().onClick.AddListener(new UnityAction(this.ShowCardExpand));
		}

		// Token: 0x060094A7 RID: 38055 RVA: 0x001536B8 File Offset: 0x001518B8
		private void Update()
		{
			if (!this.showing || this.shifting || UIManager.InputBlocker != null)
			{
				return;
			}
			if (UserInput.WasCancelPressed || UserInput.MouseRightDown)
			{
				this.Hide();
			}
			if (UserInput.WasRightPressed || UserInput.WasRightShoulderPressed)
			{
				this.OnNext();
			}
			if (UserInput.WasLeftPressed || UserInput.WasLeftShoulderPressed)
			{
				this.OnPrev();
			}
			if (UserInput.WasLeftTriggerPressed)
			{
				this.ShowCardExpand();
			}
		}

		// Token: 0x060094A8 RID: 38056 RVA: 0x0015372C File Offset: 0x0015192C
		public void Show(List<Card> cards, int index, object blockMark)
		{
			this.showing = true;
			this.shifting = true;
			this.cards = cards;
			this.cardCodes = null;
			this.index = index;
			this.CG.alpha = 1f;
			this.CG.blocksRaycasts = true;
			AudioManager.PlaySE("SE_DECK_WINDOW_OPEN", 1f);
			UIManager.ShowFPSLeft();
			this.ShowTween(base.Window);
			this.ShowTween(base.ButtonGroup.GetComponent<RectTransform>());
			base.BG.alpha = 0f;
			base.BG.DOFade(1f, 0.25f).OnComplete(delegate
			{
				this.shifting = false;
			});
			this.SetCardData(index);
			this.blockMark = blockMark;
		}

		// Token: 0x060094A9 RID: 38057 RVA: 0x001537F0 File Offset: 0x001519F0
		public void Show(List<int> cardCodes, int index, object blockMark)
		{
			this.showing = true;
			this.shifting = true;
			this.cards = null;
			this.cardCodes = cardCodes;
			this.index = index;
			this.CG.alpha = 1f;
			this.CG.blocksRaycasts = true;
			AudioManager.PlaySE("SE_DECK_WINDOW_OPEN", 1f);
			UIManager.ShowFPSLeft();
			this.ShowTween(base.Window);
			this.ShowTween(base.ButtonGroup.GetComponent<RectTransform>());
			base.BG.alpha = 0f;
			base.BG.DOFade(1f, 0.25f).OnComplete(delegate
			{
				this.shifting = false;
			});
			this.SetCardData(index);
			this.blockMark = blockMark;
		}

		// Token: 0x060094AA RID: 38058 RVA: 0x001538B4 File Offset: 0x00151AB4
		protected void SetCardData(int index)
		{
			Card data;
			if (this.cards != null)
			{
				data = this.cards[index];
			}
			else
			{
				data = CardsManager.Get(this.cardCodes[index], false);
			}
			this.SetCardData(data);
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				this.SelectDefaultButton();
			}
			this.CheckButtonState();
		}

		// Token: 0x060094AB RID: 38059 RVA: 0x00153906 File Offset: 0x00151B06
		public virtual void SelectDefaultButton()
		{
			EventSystem.current.SetSelectedGameObject(base.ButtonAddCard.gameObject);
		}

		// Token: 0x060094AC RID: 38060 RVA: 0x00153920 File Offset: 0x00151B20
		private void ShowTween(RectTransform rect)
		{
			rect.localScale = new Vector3(0.75f, 0.75f, 1f);
			rect.DOScale(1f, 0.25f).SetEase(Ease.OutQuart);
			CanvasGroup component = rect.GetComponent<CanvasGroup>();
			component.alpha = 0f;
			component.DOFade(1f, 0.25f);
		}

		// Token: 0x060094AD RID: 38061 RVA: 0x00153980 File Offset: 0x00151B80
		public void Hide()
		{
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
			UIManager.ShowFPSRight();
			this.HideTween(base.Manager.GetElement<RectTransform>("Window"));
			this.HideTween(base.Manager.GetElement<RectTransform>("ButtonGroup"));
			base.Manager.GetElement<CanvasGroup>("BG").DOFade(0f, 0.2f).OnComplete(delegate
			{
				this.showing = false;
				this.CG.alpha = 0f;
				this.CG.blocksRaycasts = false;
				Program.instance.currentServant.JudgeInputBlockerExitMark(this.blockMark);
				Program.instance.currentServant.Select(false);
			});
		}

		// Token: 0x060094AE RID: 38062 RVA: 0x001539FE File Offset: 0x00151BFE
		private void HideTween(RectTransform rect)
		{
			rect.DOScale(0.75f, 0.2f).SetEase(Ease.InCubic);
			rect.GetComponent<CanvasGroup>().DOFade(0f, 0.2f);
		}

		// Token: 0x060094AF RID: 38063 RVA: 0x00153A30 File Offset: 0x00151C30
		public void OnNext()
		{
			if (this.shifting)
			{
				return;
			}
			if (this.cards != null && this.index == this.cards.Count - 1)
			{
				return;
			}
			if (this.cardCodes != null && this.index == this.cardCodes.Count - 1)
			{
				return;
			}
			this.shifting = true;
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			RectTransform rect = base.Manager.GetElement<RectTransform>("Window");
			rect.anchoredPosition = new Vector2(0f, -32f);
			CanvasGroup cg = rect.GetComponent<CanvasGroup>();
			cg.alpha = 1f;
			DOTween.Sequence().Append(rect.DOAnchorPos(new Vector2(-480f, -32f), 0.1f, false).SetEase(Ease.InCubic)).Join(cg.DOFade(0f, 0.1f).OnComplete(delegate
			{
				rect.anchoredPosition = new Vector2(480f, -32f);
				CardActionMenu <>4__this = this;
				CardActionMenu <>4__this2 = this;
				int num = this.index + 1;
				<>4__this2.index = num;
				<>4__this.SetCardData(num);
			}))
				.Append(rect.DOAnchorPos(new Vector2(0f, -32f), 0.2f, false).SetEase(Ease.OutQuart))
				.Join(cg.DOFade(1f, 0.2f))
				.OnComplete(delegate
				{
					this.shifting = false;
				});
		}

		// Token: 0x060094B0 RID: 38064 RVA: 0x00153B98 File Offset: 0x00151D98
		public void OnPrev()
		{
			if (this.index == 0 || this.shifting)
			{
				return;
			}
			this.shifting = true;
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			RectTransform rect = base.Manager.GetElement<RectTransform>("Window");
			rect.anchoredPosition = new Vector2(0f, -32f);
			CanvasGroup cg = rect.GetComponent<CanvasGroup>();
			cg.alpha = 1f;
			DOTween.Sequence().Append(rect.DOAnchorPos(new Vector2(480f, -32f), 0.1f, false).SetEase(Ease.InCubic)).Join(cg.DOFade(0f, 0.1f).OnComplete(delegate
			{
				rect.anchoredPosition = new Vector2(-480f, -32f);
				CardActionMenu <>4__this = this;
				CardActionMenu <>4__this2 = this;
				int num = this.index - 1;
				<>4__this2.index = num;
				<>4__this.SetCardData(num);
			}))
				.Append(rect.DOAnchorPos(new Vector2(0f, -32f), 0.2f, false).SetEase(Ease.OutQuart))
				.Join(cg.DOFade(1f, 0.2f))
				.OnComplete(delegate
				{
					this.shifting = false;
				});
		}

		// Token: 0x060094B1 RID: 38065 RVA: 0x00153CC9 File Offset: 0x00151EC9
		private void ShowCardExpand()
		{
			UIManager.ShowCardExpand(base.Card);
		}

		// Token: 0x060094B2 RID: 38066 RVA: 0x00153CD6 File Offset: 0x00151ED6
		private int GetCardsCount()
		{
			if (this.cards == null)
			{
				return this.cardCodes.Count;
			}
			return this.cards.Count;
		}

		// Token: 0x060094B3 RID: 38067 RVA: 0x00153CF8 File Offset: 0x00151EF8
		private void CheckButtonState()
		{
			bool nextEnabled = true;
			bool prevEnabled = true;
			int cc = this.GetCardsCount();
			if (cc < 2)
			{
				this.ButtonNext.gameObject.SetActive(false);
				this.ButtonPrev.gameObject.SetActive(false);
				return;
			}
			this.ButtonNext.gameObject.SetActive(true);
			this.ButtonPrev.gameObject.SetActive(true);
			if (this.index == 0)
			{
				prevEnabled = false;
			}
			if (this.index == cc - 1)
			{
				nextEnabled = false;
			}
			this.ButtonNext.SetInteractable(nextEnabled);
			this.ButtonPrev.SetInteractable(prevEnabled);
		}

		// Token: 0x0400D2E8 RID: 53992
		private const string LABEL_SBN_NEXT = "NextButton";

		// Token: 0x0400D2E9 RID: 53993
		private SelectionButton m_ButtonNext;

		// Token: 0x0400D2EA RID: 53994
		private const string LABEL_SBN_PREV = "PrevButton";

		// Token: 0x0400D2EB RID: 53995
		private SelectionButton m_ButtonPrev;

		// Token: 0x0400D2EC RID: 53996
		[HideInInspector]
		public bool showing;

		// Token: 0x0400D2ED RID: 53997
		private List<Card> cards;

		// Token: 0x0400D2EE RID: 53998
		private List<int> cardCodes;

		// Token: 0x0400D2EF RID: 53999
		private int index;

		// Token: 0x0400D2F0 RID: 54000
		private bool shifting;

		// Token: 0x0400D2F1 RID: 54001
		public object blockMark;
	}
}
