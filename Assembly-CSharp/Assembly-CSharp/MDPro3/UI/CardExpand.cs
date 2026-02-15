using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200141D RID: 5149
	public class CardExpand : UIWidgetFullScreen
	{
		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06009507 RID: 38151 RVA: 0x00155204 File Offset: 0x00153404
		protected CardRawImageHandler ImageCard
		{
			get
			{
				return this.m_ImageCard = ((this.m_ImageCard != null) ? this.m_ImageCard : base.Manager.GetElement<CardRawImageHandler>("ImageCard"));
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06009508 RID: 38152 RVA: 0x00155240 File Offset: 0x00153440
		protected RectTransform CardRect
		{
			get
			{
				return this.m_CardRect = ((this.m_CardRect != null) ? this.m_CardRect : base.Manager.GetElement<RectTransform>("ImageCard"));
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06009509 RID: 38153 RVA: 0x0015527C File Offset: 0x0015347C
		protected TextMeshProUGUI TextShortcut
		{
			get
			{
				return this.m_TextShortcut = ((this.m_TextShortcut != null) ? this.m_TextShortcut : base.Manager.GetElement<TextMeshProUGUI>("TextShortcut"));
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x0600950A RID: 38154 RVA: 0x001552B8 File Offset: 0x001534B8
		protected override string Label_SE_Show
		{
			get
			{
				return "SE_CARDEXPAND_DISPLAY";
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x0600950B RID: 38155 RVA: 0x001552BF File Offset: 0x001534BF
		protected override string Label_SE_Hide
		{
			get
			{
				return "SE_CARDEXPAND_CLOSE";
			}
		}

		// Token: 0x0600950C RID: 38156 RVA: 0x001552C8 File Offset: 0x001534C8
		protected override void Awake()
		{
			this.ImageCard.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Zoom));
			base.BG.GetComponent<SelectionButton>().SetClickEvent(new UnityAction(this.Hide));
			this.TextShortcut.text = InterString.Get("扩大", 0);
		}

		// Token: 0x0600950D RID: 38157 RVA: 0x00155329 File Offset: 0x00153529
		protected override void Update()
		{
			if (!this.NeedResponse())
			{
				return;
			}
			if (UserInput.WasCancelPressed || UserInput.MouseRightDown)
			{
				if (this.expanded)
				{
					this.ZoomOut();
				}
				else
				{
					this.Hide();
				}
			}
			if (UserInput.WasLeftTriggerPressed)
			{
				this.Zoom();
			}
		}

		// Token: 0x0600950E RID: 38158 RVA: 0x00155365 File Offset: 0x00153565
		protected override bool NeedResponse()
		{
			return !this.shifting && base.NeedResponse();
		}

		// Token: 0x0600950F RID: 38159 RVA: 0x00155378 File Offset: 0x00153578
		public void Show(Card data)
		{
			AudioManager.PlaySE("SE_CARDEXPAND_DISPLAY", 1f);
			this.shifting = true;
			this.ImageCard.SetCard(data);
			this.isRushDuelCard = CardRenderer.NeedRushDuelStyle(data.Id);
			this.isPendulumCard = data.HasType(CardType.Pendulum);
			this.Show();
		}

		// Token: 0x06009510 RID: 38160 RVA: 0x001553D0 File Offset: 0x001535D0
		public override void Show()
		{
			if (this.showing)
			{
				return;
			}
			this.showing = true;
			this.ShowEvent();
			base.BG.alpha = 0.3f;
			base.BG.DOFade(1f, 0.1f);
			this.ImageCard.RawImage.color = new Color(1f, 1f, 1f, 0.3f);
			this.ImageCard.RawImage.DOFade(1f, 0.1f);
			this.ImageCard.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
			this.ImageCard.transform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.25f).SetEase(Ease.OutQuart);
			this.ImageCard.transform.localEulerAngles = new Vector3(8f, 12f, 2f);
			this.ImageCard.transform.DOLocalRotate(Vector3.zero, 0.35f, RotateMode.Fast).SetEase(Ease.OutQuart).OnComplete(delegate
			{
				this.shifting = false;
			});
			EventSystem.current.SetSelectedGameObject(this.ImageCard.gameObject);
		}

		// Token: 0x06009511 RID: 38161 RVA: 0x0015551F File Offset: 0x0015371F
		protected override void AfterShowEvent()
		{
			this.Select(true);
		}

		// Token: 0x06009512 RID: 38162 RVA: 0x00155528 File Offset: 0x00153728
		public override void Hide()
		{
			if (this.shifting)
			{
				return;
			}
			this.shifting = true;
			this.HideEvent();
			base.BG.DOFade(0f, 0.21f).OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
				this.AfterHideEvent();
			});
			DOTween.Sequence().AppendInterval(0.05f).Append(this.ImageCard.RawImage.DOFade(0f, 0.15f));
			this.ImageCard.transform.DOScale(new Vector3(1.05f, 1.05f, 1f), 0.2f).SetEase(Ease.InCubic);
			this.ImageCard.transform.DOLocalRotate(new Vector3(-10f, -16f, -3f), 0.2f, RotateMode.Fast).SetEase(Ease.InCubic);
		}

		// Token: 0x06009513 RID: 38163 RVA: 0x00155602 File Offset: 0x00153802
		private void Zoom()
		{
			if (this.expanded)
			{
				this.ZoomOut();
				return;
			}
			this.ZoomIn();
		}

		// Token: 0x06009514 RID: 38164 RVA: 0x00155619 File Offset: 0x00153819
		private float GetZoomPositionY()
		{
			if (this.isRushDuelCard)
			{
				if (this.isPendulumCard)
				{
					return -320f;
				}
				return -150f;
			}
			else
			{
				if (this.isPendulumCard)
				{
					return -213f;
				}
				return -103f;
			}
		}

		// Token: 0x06009515 RID: 38165 RVA: 0x0015564A File Offset: 0x0015384A
		private float GetZoomScale()
		{
			if (this.isRushDuelCard)
			{
				if (this.isPendulumCard)
				{
					return 3.15f;
				}
				return 2.5f;
			}
			else
			{
				if (this.isPendulumCard)
				{
					return 3.3f;
				}
				return 2.82f;
			}
		}

		// Token: 0x06009516 RID: 38166 RVA: 0x0015567C File Offset: 0x0015387C
		private void ZoomIn()
		{
			if (this.shifting)
			{
				return;
			}
			this.shifting = true;
			AudioManager.PlaySE("SE_CARDEXPAND_ZOOMIN", 1f);
			this.expanded = true;
			this.CardRect.DOScale(new Vector3(this.GetZoomScale(), this.GetZoomScale(), 1f), 0.2f).SetEase(Ease.OutCubic);
			this.CardRect.DOLocalMove(new Vector3(0f, this.GetZoomPositionY(), -100f), 0.2f, false).SetEase(Ease.OutCubic);
			this.CardRect.DORotate(new Vector3(4f, 6f, 1f), 0.15f, RotateMode.Fast).OnComplete(delegate
			{
				this.CardRect.DORotate(Vector3.zero, 0.2f, RotateMode.Fast).OnComplete(delegate
				{
					this.shifting = false;
				});
			});
			this.TextShortcut.text = InterString.Get("缩小", 0);
		}

		// Token: 0x06009517 RID: 38167 RVA: 0x00155758 File Offset: 0x00153958
		private void ZoomOut()
		{
			if (this.shifting)
			{
				return;
			}
			this.shifting = true;
			AudioManager.PlaySE("SE_CARDEXPAND_ZOOMOUT", 1f);
			this.expanded = false;
			this.CardRect.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.2f).SetEase(Ease.OutCubic);
			this.CardRect.DOLocalMove(new Vector3(0f, 0f, -100f), 0.2f, false).SetEase(Ease.OutCubic);
			this.CardRect.DORotate(new Vector3(-4f, -6f, -1f), 0.15f, RotateMode.Fast).OnComplete(delegate
			{
				this.CardRect.DORotate(Vector3.zero, 0.2f, RotateMode.Fast).OnComplete(delegate
				{
					this.shifting = false;
				});
			});
			this.TextShortcut.text = InterString.Get("扩大", 0);
		}

		// Token: 0x0400D345 RID: 54085
		private const string LABEL_RIMG_CARD = "ImageCard";

		// Token: 0x0400D346 RID: 54086
		private CardRawImageHandler m_ImageCard;

		// Token: 0x0400D347 RID: 54087
		private RectTransform m_CardRect;

		// Token: 0x0400D348 RID: 54088
		private const string LABEL_TXT_Shortcut = "TextShortcut";

		// Token: 0x0400D349 RID: 54089
		private TextMeshProUGUI m_TextShortcut;

		// Token: 0x0400D34A RID: 54090
		private bool expanded;

		// Token: 0x0400D34B RID: 54091
		private bool isRushDuelCard;

		// Token: 0x0400D34C RID: 54092
		private bool isPendulumCard;

		// Token: 0x0400D34D RID: 54093
		private bool shifting;
	}
}
