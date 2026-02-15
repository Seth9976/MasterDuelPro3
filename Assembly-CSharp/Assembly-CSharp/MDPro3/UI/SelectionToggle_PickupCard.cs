using System;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace MDPro3.UI
{
	// Token: 0x020013CC RID: 5068
	public class SelectionToggle_PickupCard : SelectionToggle
	{
		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x060092CB RID: 37579 RVA: 0x0014A028 File Offset: 0x00148228
		private CardRawImageHandler ImageCardHandler
		{
			get
			{
				return this.m_ImageCardHandler = ((this.m_ImageCardHandler != null) ? this.m_ImageCardHandler : base.Manager.GetElement<CardRawImageHandler>("ImageCard"));
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x060092CC RID: 37580 RVA: 0x0014A064 File Offset: 0x00148264
		private Image IconLimit
		{
			get
			{
				return this.m_IconLimit = ((this.m_IconLimit != null) ? this.m_IconLimit : base.Manager.GetElement<Image>("IconLimit"));
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x060092CD RID: 37581 RVA: 0x0014A0A0 File Offset: 0x001482A0
		private Image IconAttribute
		{
			get
			{
				return this.m_IconAttribute = ((this.m_IconAttribute != null) ? this.m_IconAttribute : base.Manager.GetElement<Image>("IconAttribute"));
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x060092CE RID: 37582 RVA: 0x0014A0DC File Offset: 0x001482DC
		private Image IconSpellTrapType
		{
			get
			{
				return this.m_IconSpellTrapType = ((this.m_IconSpellTrapType != null) ? this.m_IconSpellTrapType : base.Manager.GetElement<Image>("IconSpellTrapType"));
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x060092CF RID: 37583 RVA: 0x0014A118 File Offset: 0x00148318
		private Image IconRace
		{
			get
			{
				return this.m_IconRace = ((this.m_IconRace != null) ? this.m_IconRace : base.Manager.GetElement<Image>("IconRace"));
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x060092D0 RID: 37584 RVA: 0x0014A154 File Offset: 0x00148354
		private Image IconPool
		{
			get
			{
				return this.m_IconPool = ((this.m_IconPool != null) ? this.m_IconPool : base.Manager.GetElement<Image>("IconPool"));
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x060092D1 RID: 37585 RVA: 0x0014A190 File Offset: 0x00148390
		private Image IconTuner
		{
			get
			{
				return this.m_IconTuner = ((this.m_IconTuner != null) ? this.m_IconTuner : base.Manager.GetElement<Image>("IconTuner"));
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x060092D2 RID: 37586 RVA: 0x0014A1CC File Offset: 0x001483CC
		private Image IconLevel
		{
			get
			{
				return this.m_IconLevel = ((this.m_IconLevel != null) ? this.m_IconLevel : base.Manager.GetElement<Image>("IconLevel"));
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x060092D3 RID: 37587 RVA: 0x0014A208 File Offset: 0x00148408
		private Image IconRank
		{
			get
			{
				return this.m_IconRank = ((this.m_IconRank != null) ? this.m_IconRank : base.Manager.GetElement<Image>("IconRank"));
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x060092D4 RID: 37588 RVA: 0x0014A244 File Offset: 0x00148444
		private Image IconLink
		{
			get
			{
				return this.m_IconLink = ((this.m_IconLink != null) ? this.m_IconLink : base.Manager.GetElement<Image>("IconLink"));
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x060092D5 RID: 37589 RVA: 0x0014A280 File Offset: 0x00148480
		private Image IconPendulumScale
		{
			get
			{
				return this.m_IconPendulumScale = ((this.m_IconPendulumScale != null) ? this.m_IconPendulumScale : base.Manager.GetElement<Image>("IconPendulumScale"));
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x060092D6 RID: 37590 RVA: 0x0014A2BC File Offset: 0x001484BC
		private TextMeshProUGUI TextLevel
		{
			get
			{
				return this.m_TextLevel = ((this.m_TextLevel != null) ? this.m_TextLevel : base.Manager.GetElement<TextMeshProUGUI>("TextLevel"));
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x060092D7 RID: 37591 RVA: 0x0014A2F8 File Offset: 0x001484F8
		private TextMeshProUGUI TextRank
		{
			get
			{
				return this.m_TextRank = ((this.m_TextRank != null) ? this.m_TextRank : base.Manager.GetElement<TextMeshProUGUI>("TextRank"));
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x060092D8 RID: 37592 RVA: 0x0014A334 File Offset: 0x00148534
		private TextMeshProUGUI TextLink
		{
			get
			{
				return this.m_TextLink = ((this.m_TextLink != null) ? this.m_TextLink : base.Manager.GetElement<TextMeshProUGUI>("TextLink"));
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x060092D9 RID: 37593 RVA: 0x0014A370 File Offset: 0x00148570
		private TextMeshProUGUI TextPendulumScale
		{
			get
			{
				return this.m_TextPendulumScale = ((this.m_TextPendulumScale != null) ? this.m_TextPendulumScale : base.Manager.GetElement<TextMeshProUGUI>("TextPendulumScale"));
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x060092DA RID: 37594 RVA: 0x0014A3AC File Offset: 0x001485AC
		private ColorContainerGraphic CursorCardSelect
		{
			get
			{
				return this.m_CursorCardSelect = ((this.m_CursorCardSelect != null) ? this.m_CursorCardSelect : base.Manager.GetElement<ColorContainerGraphic>("CursorCardSelect"));
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x060092DB RID: 37595 RVA: 0x0014A3E8 File Offset: 0x001485E8
		private RawImage ImageBack
		{
			get
			{
				return this.m_ImageBack = ((this.m_ImageBack != null) ? this.m_ImageBack : base.Manager.GetElement<RawImage>("ImageBack"));
			}
		}

		// Token: 0x060092DC RID: 37596 RVA: 0x00149E76 File Offset: 0x00148076
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
		}

		// Token: 0x060092DD RID: 37597 RVA: 0x0014A424 File Offset: 0x00148624
		protected override void OnEnter()
		{
			base.OnEnter();
			this.CursorCardSelect.SetColor(ColorContainer.SelectMode.Selected, ColorContainer.StatusMode.Normal, true);
		}

		// Token: 0x060092DE RID: 37598 RVA: 0x0014A43C File Offset: 0x0014863C
		public void SetCard(Card card, bool save)
		{
			this.ImageCardHandler.SetCard(card);
			this.cardSetted = true;
			this.ImageBack.gameObject.SetActive(false);
			if (save)
			{
				Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
				DeckEditor.Deck.Pickup[this.pickUpIndex] = card.Id;
			}
		}

		// Token: 0x060092DF RID: 37599 RVA: 0x0014A4A8 File Offset: 0x001486A8
		public void ClearCard(bool alsoSelect, bool save)
		{
			this.cardSetted = false;
			this.ImageBack.gameObject.SetActive(true);
			Program.instance.deckBrowser.GetUI<DeckBrowserUI>().DeckView.Depickup(this.pickUpIndex);
			if (alsoSelect)
			{
				this.SetToggleOn(true);
			}
			if (save)
			{
				Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.SetDirty(true);
				DeckEditor.Deck.Pickup[this.pickUpIndex] = 0;
			}
		}

		// Token: 0x060092E0 RID: 37600 RVA: 0x00140C74 File Offset: 0x0013EE74
		protected override void OnClick()
		{
			this.OnSubmit();
		}

		// Token: 0x060092E1 RID: 37601 RVA: 0x0014A52C File Offset: 0x0014872C
		protected override void OnSubmit()
		{
			if (this.isOn)
			{
				AudioManager.PlaySE(base.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
				this.ClearCard(true, true);
				return;
			}
			AudioManager.PlaySE(base.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
			this.SetToggleOn(true);
		}

		// Token: 0x0400D125 RID: 53541
		private const string LABEL_IMAGE_CARD = "ImageCard";

		// Token: 0x0400D126 RID: 53542
		private CardRawImageHandler m_ImageCardHandler;

		// Token: 0x0400D127 RID: 53543
		private const string LABEL_ICON_LIMIT = "IconLimit";

		// Token: 0x0400D128 RID: 53544
		private Image m_IconLimit;

		// Token: 0x0400D129 RID: 53545
		private const string LABEL_ICON_ATTRIBUTE = "IconAttribute";

		// Token: 0x0400D12A RID: 53546
		private Image m_IconAttribute;

		// Token: 0x0400D12B RID: 53547
		private const string LABEL_ICON_SPELL_TRAP_TYPE = "IconSpellTrapType";

		// Token: 0x0400D12C RID: 53548
		private Image m_IconSpellTrapType;

		// Token: 0x0400D12D RID: 53549
		private const string LABEL_ICON_RACE = "IconRace";

		// Token: 0x0400D12E RID: 53550
		private Image m_IconRace;

		// Token: 0x0400D12F RID: 53551
		private const string LABEL_ICON_POOL = "IconPool";

		// Token: 0x0400D130 RID: 53552
		private Image m_IconPool;

		// Token: 0x0400D131 RID: 53553
		private const string LABEL_ICON_TUNER = "IconTuner";

		// Token: 0x0400D132 RID: 53554
		private Image m_IconTuner;

		// Token: 0x0400D133 RID: 53555
		private const string LABEL_ICON_LEVEL = "IconLevel";

		// Token: 0x0400D134 RID: 53556
		private Image m_IconLevel;

		// Token: 0x0400D135 RID: 53557
		private const string LABEL_ICON_RANK = "IconRank";

		// Token: 0x0400D136 RID: 53558
		private Image m_IconRank;

		// Token: 0x0400D137 RID: 53559
		private const string LABEL_ICON_LINK = "IconLink";

		// Token: 0x0400D138 RID: 53560
		private Image m_IconLink;

		// Token: 0x0400D139 RID: 53561
		private const string LABEL_ICON_PENDULUM_SCALE = "IconPendulumScale";

		// Token: 0x0400D13A RID: 53562
		private Image m_IconPendulumScale;

		// Token: 0x0400D13B RID: 53563
		private const string LABEL_TEXT_LEVEL = "TextLevel";

		// Token: 0x0400D13C RID: 53564
		private TextMeshProUGUI m_TextLevel;

		// Token: 0x0400D13D RID: 53565
		private const string LABEL_TEXT_RANK = "TextRank";

		// Token: 0x0400D13E RID: 53566
		private TextMeshProUGUI m_TextRank;

		// Token: 0x0400D13F RID: 53567
		private const string LABEL_TEXT_LINK = "TextLink";

		// Token: 0x0400D140 RID: 53568
		private TextMeshProUGUI m_TextLink;

		// Token: 0x0400D141 RID: 53569
		private const string LABEL_TEXT_PENDULUM_SCALE = "TextPendulumScale";

		// Token: 0x0400D142 RID: 53570
		private TextMeshProUGUI m_TextPendulumScale;

		// Token: 0x0400D143 RID: 53571
		private const string LABEL_CCG_CURSOR_CARD_SELECT = "CursorCardSelect";

		// Token: 0x0400D144 RID: 53572
		private ColorContainerGraphic m_CursorCardSelect;

		// Token: 0x0400D145 RID: 53573
		private const string LABEL_RIMG_BACK = "ImageBack";

		// Token: 0x0400D146 RID: 53574
		private RawImage m_ImageBack;

		// Token: 0x0400D147 RID: 53575
		public int pickUpIndex;

		// Token: 0x0400D148 RID: 53576
		[HideInInspector]
		public bool cardSetted;
	}
}
