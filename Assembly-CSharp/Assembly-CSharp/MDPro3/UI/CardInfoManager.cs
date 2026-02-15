using System;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x02001361 RID: 4961
	public class CardInfoManager
	{
		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06008FAE RID: 36782 RVA: 0x001384E0 File Offset: 0x001366E0
		protected MaterialSetter PlateTitle
		{
			get
			{
				return this.m_PlateTitle = ((this.m_PlateTitle != null) ? this.m_PlateTitle : this.Manager.GetNestedElement<MaterialSetter>("TitleArea/PlateTitle"));
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06008FAF RID: 36783 RVA: 0x0013851C File Offset: 0x0013671C
		protected TextMeshProUGUI TextCardName
		{
			get
			{
				return this.m_TextCardName = ((this.m_TextCardName != null) ? this.m_TextCardName : this.Manager.GetNestedElement<TextMeshProUGUI>("TitleArea/TextCardName"));
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06008FB0 RID: 36784 RVA: 0x00138558 File Offset: 0x00136758
		protected Image IconAttribute
		{
			get
			{
				return this.m_IconAttribute = ((this.m_IconAttribute != null) ? this.m_IconAttribute : this.Manager.GetNestedElement<Image>("TitleArea/IconAttribute"));
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06008FB1 RID: 36785 RVA: 0x00138594 File Offset: 0x00136794
		protected Image IconLevel
		{
			get
			{
				return this.m_IconLevel = ((this.m_IconLevel != null) ? this.m_IconLevel : this.Manager.GetNestedElement<Image>("ParameterArea/IconLevel"));
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06008FB2 RID: 36786 RVA: 0x001385D0 File Offset: 0x001367D0
		protected TextMeshProUGUI TextLevel
		{
			get
			{
				return this.m_TextLevel = ((this.m_TextLevel != null) ? this.m_TextLevel : this.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconLevel/Text"));
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06008FB3 RID: 36787 RVA: 0x0013860C File Offset: 0x0013680C
		protected Image IconRank
		{
			get
			{
				return this.m_IconRank = ((this.m_IconRank != null) ? this.m_IconRank : this.Manager.GetNestedElement<Image>("ParameterArea/IconRank"));
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06008FB4 RID: 36788 RVA: 0x00138648 File Offset: 0x00136848
		protected TextMeshProUGUI TextRank
		{
			get
			{
				return this.m_TextRank = ((this.m_TextRank != null) ? this.m_TextRank : this.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconRank/Text"));
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06008FB5 RID: 36789 RVA: 0x00138684 File Offset: 0x00136884
		protected Image IconPendulumScale
		{
			get
			{
				return this.m_IconPendulumScale = ((this.m_IconPendulumScale != null) ? this.m_IconPendulumScale : this.Manager.GetNestedElement<Image>("ParameterArea/IconPendulumScale"));
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06008FB6 RID: 36790 RVA: 0x001386C0 File Offset: 0x001368C0
		protected TextMeshProUGUI TextPendulumScale
		{
			get
			{
				return this.m_TextPendulumScale = ((this.m_TextPendulumScale != null) ? this.m_TextPendulumScale : this.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconPendulumScale/Text"));
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06008FB7 RID: 36791 RVA: 0x001386FC File Offset: 0x001368FC
		protected Image IconLink
		{
			get
			{
				return this.m_IconLink = ((this.m_IconLink != null) ? this.m_IconLink : this.Manager.GetNestedElement<Image>("ParameterArea/IconLink"));
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06008FB8 RID: 36792 RVA: 0x00138738 File Offset: 0x00136938
		protected TextMeshProUGUI TextLink
		{
			get
			{
				return this.m_TextLink = ((this.m_TextLink != null) ? this.m_TextLink : this.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconLink/Text"));
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06008FB9 RID: 36793 RVA: 0x00138774 File Offset: 0x00136974
		protected Image IconRace
		{
			get
			{
				return this.m_IconRace = ((this.m_IconRace != null) ? this.m_IconRace : this.Manager.GetNestedElement<Image>("ParameterArea/IconRace"));
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06008FBA RID: 36794 RVA: 0x001387B0 File Offset: 0x001369B0
		protected Image IconTuner
		{
			get
			{
				return this.m_IconTuner = ((this.m_IconTuner != null) ? this.m_IconTuner : this.Manager.GetNestedElement<Image>("ParameterArea/IconTuner"));
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06008FBB RID: 36795 RVA: 0x001387EC File Offset: 0x001369EC
		protected GameObject SpellTrapType
		{
			get
			{
				return this.m_SpellTrapType = ((this.m_SpellTrapType != null) ? this.m_SpellTrapType : this.Manager.GetNestedElement("ParameterArea/SpellTrapType"));
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06008FBC RID: 36796 RVA: 0x00138828 File Offset: 0x00136A28
		protected Image IconSpellTrapType
		{
			get
			{
				return this.m_IconSpellTrapType = ((this.m_IconSpellTrapType != null) ? this.m_IconSpellTrapType : this.Manager.GetNestedElement<Image>("ParameterArea/IconSpellTrapType"));
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06008FBD RID: 36797 RVA: 0x00138864 File Offset: 0x00136A64
		protected TextMeshProUGUI TextSpellTrapType
		{
			get
			{
				return this.m_TextSpellTrapType = ((this.m_TextSpellTrapType != null) ? this.m_TextSpellTrapType : this.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/TextSpellTrapType"));
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06008FBE RID: 36798 RVA: 0x001388A0 File Offset: 0x00136AA0
		protected Image IconAtk
		{
			get
			{
				return this.m_IconAtk = ((this.m_IconAtk != null) ? this.m_IconAtk : this.Manager.GetNestedElement<Image>("ParameterArea/IconAtk"));
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06008FBF RID: 36799 RVA: 0x001388DC File Offset: 0x00136ADC
		protected TextMeshProUGUI TextAtk
		{
			get
			{
				return this.m_TextAtk = ((this.m_TextAtk != null) ? this.m_TextAtk : this.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconAtk/Text"));
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06008FC0 RID: 36800 RVA: 0x00138918 File Offset: 0x00136B18
		protected Image IconDef
		{
			get
			{
				return this.m_IconDef = ((this.m_IconDef != null) ? this.m_IconDef : this.Manager.GetNestedElement<Image>("ParameterArea/IconDef"));
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06008FC1 RID: 36801 RVA: 0x00138954 File Offset: 0x00136B54
		protected TextMeshProUGUI TextDef
		{
			get
			{
				return this.m_TextDef = ((this.m_TextDef != null) ? this.m_TextDef : this.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconDef/Text"));
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06008FC2 RID: 36802 RVA: 0x00138990 File Offset: 0x00136B90
		protected GameObject PendulumDescriptionArea
		{
			get
			{
				return this.m_PendulumDescriptionArea = ((this.m_PendulumDescriptionArea != null) ? this.m_PendulumDescriptionArea : this.Manager.GetNestedElement("DescriptionArea/PendulumDescriptionArea"));
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06008FC3 RID: 36803 RVA: 0x001389CC File Offset: 0x00136BCC
		protected MaterialSetter PlatePendulumDescription
		{
			get
			{
				return this.m_PlatePendulumDescription = ((this.m_PlatePendulumDescription != null) ? this.m_PlatePendulumDescription : this.Manager.GetNestedElement<MaterialSetter>("DescriptionArea/PlatePendulumDescription"));
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06008FC4 RID: 36804 RVA: 0x00138A08 File Offset: 0x00136C08
		protected ScrollRect TextAreaPendulum
		{
			get
			{
				return this.m_TextAreaPendulum = ((this.m_TextAreaPendulum != null) ? this.m_TextAreaPendulum : this.Manager.GetNestedElement<ScrollRect>("DescriptionArea/TextAreaPendulum"));
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06008FC5 RID: 36805 RVA: 0x00138A44 File Offset: 0x00136C44
		protected TextMeshProUGUI TextPendulumDescriptionValue
		{
			get
			{
				return this.m_TextPendulumDescriptionValue = ((this.m_TextPendulumDescriptionValue != null) ? this.m_TextPendulumDescriptionValue : this.Manager.GetNestedElement<TextMeshProUGUI>("DescriptionArea/TextPendulumDescriptionValue"));
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06008FC6 RID: 36806 RVA: 0x00138A80 File Offset: 0x00136C80
		protected MaterialSetter PlateDescription
		{
			get
			{
				return this.m_PlateDescription = ((this.m_PlateDescription != null) ? this.m_PlateDescription : this.Manager.GetNestedElement<MaterialSetter>("DescriptionArea/PlateDescription"));
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06008FC7 RID: 36807 RVA: 0x00138ABC File Offset: 0x00136CBC
		protected TextMeshProUGUI TextDescriptionItem
		{
			get
			{
				return this.m_TextDescriptionItem = ((this.m_TextDescriptionItem != null) ? this.m_TextDescriptionItem : this.Manager.GetNestedElement<TextMeshProUGUI>("DescriptionArea/TextDescriptionItem"));
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06008FC8 RID: 36808 RVA: 0x00138AF8 File Offset: 0x00136CF8
		protected ScrollRect TextArea
		{
			get
			{
				return this.m_TextArea = ((this.m_TextArea != null) ? this.m_TextArea : this.Manager.GetNestedElement<ScrollRect>("DescriptionArea/TextArea"));
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06008FC9 RID: 36809 RVA: 0x00138B34 File Offset: 0x00136D34
		protected TextMeshProUGUI TextDescriptionValue
		{
			get
			{
				return this.m_TextDescriptionValue = ((this.m_TextDescriptionValue != null) ? this.m_TextDescriptionValue : this.Manager.GetNestedElement<TextMeshProUGUI>("DescriptionArea/TextDescriptionValue"));
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06008FCA RID: 36810 RVA: 0x00138B70 File Offset: 0x00136D70
		protected CardRawImageHandler ImageCard
		{
			get
			{
				return this.m_ImageCard = ((this.m_ImageCard != null) ? this.m_ImageCard : this.Manager.GetNestedElement<CardRawImageHandler>("CardArea/ImageCard"));
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06008FCB RID: 36811 RVA: 0x00138BAC File Offset: 0x00136DAC
		protected Image IconLimit
		{
			get
			{
				return this.m_IconLimit = ((this.m_IconLimit != null) ? this.m_IconLimit : this.Manager.GetNestedElement<Image>("CardArea/IconLimit"));
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06008FCC RID: 36812 RVA: 0x00138BE8 File Offset: 0x00136DE8
		protected TextMeshProUGUI TextCardNumValue
		{
			get
			{
				return this.m_TextCardNumValue = ((this.m_TextCardNumValue != null) ? this.m_TextCardNumValue : this.Manager.GetNestedElement<TextMeshProUGUI>("CardArea/TextCardNumValue"));
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06008FCD RID: 36813 RVA: 0x00138C24 File Offset: 0x00136E24
		protected GameObject PoolArea
		{
			get
			{
				return this.m_PoolArea = ((this.m_PoolArea != null) ? this.m_PoolArea : this.Manager.GetNestedElement("CardArea/PoolArea"));
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06008FCE RID: 36814 RVA: 0x00138C60 File Offset: 0x00136E60
		protected Image IconOCG
		{
			get
			{
				return this.m_IconOCG = ((this.m_IconOCG != null) ? this.m_IconOCG : this.Manager.GetNestedElement<Image>("CardArea/IconOCG"));
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06008FCF RID: 36815 RVA: 0x00138C9C File Offset: 0x00136E9C
		protected Image IconTCG
		{
			get
			{
				return this.m_IconTCG = ((this.m_IconTCG != null) ? this.m_IconTCG : this.Manager.GetNestedElement<Image>("CardArea/IconTCG"));
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06008FD0 RID: 36816 RVA: 0x00138CD8 File Offset: 0x00136ED8
		protected Image IconSCCG
		{
			get
			{
				return this.m_IconSCCG = ((this.m_IconSCCG != null) ? this.m_IconSCCG : this.Manager.GetNestedElement<Image>("CardArea/IconSCCG"));
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06008FD1 RID: 36817 RVA: 0x00138D14 File Offset: 0x00136F14
		protected Image IconDIY
		{
			get
			{
				return this.m_IconDIY = ((this.m_IconDIY != null) ? this.m_IconDIY : this.Manager.GetNestedElement<Image>("CardArea/IconDIY"));
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06008FD2 RID: 36818 RVA: 0x00138D50 File Offset: 0x00136F50
		protected Image IconPRE
		{
			get
			{
				return this.m_IconPRE = ((this.m_IconPRE != null) ? this.m_IconPRE : this.Manager.GetNestedElement<Image>("CardArea/IconPRE"));
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06008FD3 RID: 36819 RVA: 0x00138D8C File Offset: 0x00136F8C
		protected GameObject MenuArea
		{
			get
			{
				return this.m_MenuArea = ((this.m_MenuArea != null) ? this.m_MenuArea : this.Manager.GetElement("MenuArea"));
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06008FD4 RID: 36820 RVA: 0x00138DC8 File Offset: 0x00136FC8
		protected SelectionToggle ToggleBookMark
		{
			get
			{
				return this.m_ToggleBookMark = ((this.m_ToggleBookMark != null) ? this.m_ToggleBookMark : this.Manager.GetNestedElement<SelectionToggle>("MenuArea/BookmarkToggleButton"));
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06008FD5 RID: 36821 RVA: 0x00138E04 File Offset: 0x00137004
		protected SelectionButton ButtonRelatedCard
		{
			get
			{
				return this.m_ButtonRelatedCard = ((this.m_ButtonRelatedCard != null) ? this.m_ButtonRelatedCard : this.Manager.GetNestedElement<SelectionButton>("MenuArea/RelatedCardButton"));
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06008FD6 RID: 36822 RVA: 0x00138E40 File Offset: 0x00137040
		public SelectionButton ButtonAddCard
		{
			get
			{
				return this.m_ButtonAddCard = ((this.m_ButtonAddCard != null) ? this.m_ButtonAddCard : this.Manager.GetNestedElement<SelectionButton>("MenuArea/AddCardButton"));
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06008FD7 RID: 36823 RVA: 0x00138E7C File Offset: 0x0013707C
		public SelectionButton ButtonRemoveCard
		{
			get
			{
				return this.m_ButtonRemoveCard = ((this.m_ButtonRemoveCard != null) ? this.m_ButtonRemoveCard : this.Manager.GetNestedElement<SelectionButton>("MenuArea/RemoveCardButton"));
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06008FD8 RID: 36824 RVA: 0x00138EB8 File Offset: 0x001370B8
		protected SelectionToggle_Rarity ToggleRarityR
		{
			get
			{
				return this.m_ToggleRarityR = ((this.m_ToggleRarityR != null) ? this.m_ToggleRarityR : this.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityR"));
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06008FD9 RID: 36825 RVA: 0x00138EF4 File Offset: 0x001370F4
		protected SelectionToggle_Rarity ToggleRarityUR
		{
			get
			{
				return this.m_ToggleRarityUR = ((this.m_ToggleRarityUR != null) ? this.m_ToggleRarityUR : this.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityUR"));
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06008FDA RID: 36826 RVA: 0x00138F30 File Offset: 0x00137130
		protected SelectionToggle_Rarity ToggleRarityGR
		{
			get
			{
				return this.m_ToggleRarityGR = ((this.m_ToggleRarityGR != null) ? this.m_ToggleRarityGR : this.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityGR"));
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06008FDB RID: 36827 RVA: 0x00138F6C File Offset: 0x0013716C
		protected SelectionToggle_Rarity ToggleRarityMR
		{
			get
			{
				return this.m_ToggleRarityMR = ((this.m_ToggleRarityMR != null) ? this.m_ToggleRarityMR : this.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityMR"));
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06008FDC RID: 36828 RVA: 0x00138FA8 File Offset: 0x001371A8
		// (set) Token: 0x06008FDD RID: 36829 RVA: 0x00138FB0 File Offset: 0x001371B0
		public Card Card
		{
			get
			{
				return this._card;
			}
			set
			{
				this._card = value;
				this.ImageCard.SetCard(value);
			}
		}

		// Token: 0x06008FDE RID: 36830 RVA: 0x00138FC5 File Offset: 0x001371C5
		public CardInfoManager(ElementObjectManager manager, bool pendulumTextNeedSplit)
		{
			this.Manager = manager;
			this.pendulumTextNeedSplit = pendulumTextNeedSplit;
			this.Initialize();
		}

		// Token: 0x06008FDF RID: 36831 RVA: 0x00138FE8 File Offset: 0x001371E8
		private void Initialize()
		{
			if (this.MenuArea != null)
			{
				this.ToggleBookMark.SetToggleOnEvent(delegate
				{
					Program.instance.deckEditor.GetUI<DeckEditorUI>().BookmarkCard(this.Card.Id);
				});
				this.ToggleBookMark.SetToggleOffEvent(delegate
				{
					Program.instance.deckEditor.GetUI<DeckEditorUI>().UnbookmarkCard(this.Card.Id);
				});
				this.ButtonAddCard.SetClickEvent(delegate
				{
					Program.instance.deckEditor.GetUI<DeckEditorUI>().AddCard(this.Card);
				});
				this.ButtonRemoveCard.SetClickEvent(delegate
				{
					Program.instance.deckEditor.GetUI<DeckEditorUI>().RemoveCard(this.Card);
				});
			}
		}

		// Token: 0x06008FE0 RID: 36832 RVA: 0x00139060 File Offset: 0x00137260
		private void SetCardData(Card data)
		{
			if (this.Card != null && this.Card.Id == data.Id)
			{
				return;
			}
			this.Card = data;
			this.TextCardName.text = " " + data.Name;
			this.PlateTitle.SetMaterialAction(delegate(Material matetial)
			{
				Color[] colors = CardDescription.GetCardFrameColor(data);
				matetial.SetColor("_Color0", colors[0]);
				matetial.SetColor("_Color1", colors[1]);
			});
			this.IconAttribute.sprite = TextureManager.container.GetCardAttributeIcon(data, false);
			this.IconLimit.sprite = TextureManager.container.GetCardRegulationIcon(data.Id, DeckEditor.banlist);
			this.SetCardCount();
			if (this.PoolArea != null)
			{
				this.IconOCG.gameObject.SetActive((data.Ot & 1) > 0);
				this.IconTCG.gameObject.SetActive((data.Ot & 2) > 0);
				this.IconSCCG.gameObject.SetActive((data.Ot & 8) > 0);
				this.IconDIY.gameObject.SetActive((data.Ot & 4) > 0);
				this.IconPRE.gameObject.SetActive(data.isPre);
			}
			Card.LevelType levelType = data.GetLevelType();
			this.IconLevel.gameObject.SetActive(data.HasType(CardType.Monster) && levelType == Card.LevelType.Level);
			this.TextLevel.text = data.Level.ToString();
			this.IconRank.gameObject.SetActive(levelType == Card.LevelType.Rank);
			this.TextRank.text = data.Level.ToString();
			this.IconLink.gameObject.SetActive(levelType == Card.LevelType.Link);
			this.TextLink.text = data.GetLinkCount().ToString();
			this.IconPendulumScale.gameObject.SetActive(data.HasType(CardType.Pendulum));
			this.TextPendulumScale.text = data.LScale.ToString();
			Sprite raceIcon = TextureManager.container.GetCardRaceIcon(data);
			this.IconRace.gameObject.SetActive(raceIcon != null);
			this.IconRace.sprite = raceIcon;
			this.IconTuner.gameObject.SetActive(data.HasType(CardType.Tuner));
			if (data.HasType(CardType.Spell) || data.HasType(CardType.Trap))
			{
				this.SpellTrapType.SetActive(true);
				this.IconSpellTrapType.sprite = TextureManager.container.GetCardSpellTrapTypeIcon(data);
				this.TextSpellTrapType.text = StringHelper.SecondType((long)data.Type, 0) + StringHelper.MainType((long)data.Type, 0);
				this.IconAtk.gameObject.SetActive(false);
				this.IconDef.gameObject.SetActive(false);
			}
			else
			{
				this.SpellTrapType.SetActive(false);
				this.IconAtk.gameObject.SetActive(true);
				this.TextAtk.text = data.GetAttackString();
				this.IconDef.gameObject.SetActive(levelType != Card.LevelType.Link);
				this.TextDef.text = data.GetDefenseString();
			}
			this.PlateDescription.SetMaterialAction(delegate(Material matetial)
			{
				Color[] colors2 = CardDescription.GetCardFrameColor(data);
				matetial.SetColor("_Color0", colors2[0]);
				matetial.SetColor("_Color1", colors2[1]);
			});
			this.TextDescriptionItem.text = data.GetTypeForUI();
			this.TextDescriptionValue.text = (this.pendulumTextNeedSplit ? data.GetMonsterDescription(false) : data.GetDescription(true));
			if (this.pendulumTextNeedSplit)
			{
				if (data.HasType(CardType.Pendulum))
				{
					this.PendulumDescriptionArea.SetActive(true);
					this.PlatePendulumDescription.SetMaterialAction(delegate(Material matetial)
					{
						Color[] colors3 = CardDescription.GetCardFrameColor(data);
						matetial.SetColor("_Color0", colors3[0]);
						matetial.SetColor("_Color1", colors3[1]);
					});
					this.TextPendulumDescriptionValue.text = data.GetPendulumDescription(false);
				}
				else
				{
					this.PendulumDescriptionArea.SetActive(false);
				}
			}
			if (this.MenuArea != null)
			{
				CardRarity.Rarity rarity = CardRarity.GetRarity(data.Id);
				if (this.ToggleRarityR.rarity == rarity)
				{
					this.ToggleRarityR.SetToggleOn(false);
				}
				else
				{
					this.ToggleRarityR.SetToggleOff(false);
				}
				if (this.ToggleRarityUR.rarity == rarity)
				{
					this.ToggleRarityUR.SetToggleOn(false);
				}
				else
				{
					this.ToggleRarityUR.SetToggleOff(false);
				}
				if (this.ToggleRarityGR.rarity == rarity)
				{
					this.ToggleRarityGR.SetToggleOn(false);
				}
				else
				{
					this.ToggleRarityGR.SetToggleOff(false);
				}
				if (this.ToggleRarityMR.rarity == rarity)
				{
					this.ToggleRarityMR.SetToggleOn(false);
				}
				else
				{
					this.ToggleRarityMR.SetToggleOff(false);
				}
				this.RefreshBookmarkToggle();
			}
		}

		// Token: 0x06008FE1 RID: 36833 RVA: 0x00139591 File Offset: 0x00137791
		public void SetCardCount(string cardCount)
		{
			if (this.TextCardNumValue == null)
			{
				return;
			}
			this.TextCardNumValue.text = cardCount;
		}

		// Token: 0x06008FE2 RID: 36834 RVA: 0x001395B0 File Offset: 0x001377B0
		public void SetCardCount()
		{
			if (this.TextCardNumValue == null || this.Card == null)
			{
				return;
			}
			this.TextCardNumValue.text = Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.GetCardCount(this.Card.Id).ToString();
		}

		// Token: 0x06008FE3 RID: 36835 RVA: 0x0013960B File Offset: 0x0013780B
		public void RefreshRarity(int code)
		{
			this.ImageCard.RefreshRarity(code);
		}

		// Token: 0x06008FE4 RID: 36836 RVA: 0x00139619 File Offset: 0x00137819
		public void RefreshBookmarkToggle()
		{
			if (this.ToggleBookMark == null)
			{
				return;
			}
			if (CardRarity.CardBookmarked(this.Card.Id))
			{
				this.ToggleBookMark.SetToggleOn(false);
				return;
			}
			this.ToggleBookMark.SetToggleOff(false);
		}

		// Token: 0x06008FE5 RID: 36837 RVA: 0x00139655 File Offset: 0x00137855
		public void SetRelatedCardEvent(UnityAction call)
		{
			if (this.ButtonRelatedCard == null)
			{
				return;
			}
			this.ButtonRelatedCard.SetClickEvent(call);
		}

		// Token: 0x0400CE24 RID: 52772
		private const string LABEL_MS_PLATETITLE = "TitleArea/PlateTitle";

		// Token: 0x0400CE25 RID: 52773
		private MaterialSetter m_PlateTitle;

		// Token: 0x0400CE26 RID: 52774
		private const string LABEL_TXT_CARDNAME = "TitleArea/TextCardName";

		// Token: 0x0400CE27 RID: 52775
		private TextMeshProUGUI m_TextCardName;

		// Token: 0x0400CE28 RID: 52776
		private const string LABEL_IMG_ATTRIBUTE = "TitleArea/IconAttribute";

		// Token: 0x0400CE29 RID: 52777
		private Image m_IconAttribute;

		// Token: 0x0400CE2A RID: 52778
		private const string LABEL_IMG_LEVEL = "ParameterArea/IconLevel";

		// Token: 0x0400CE2B RID: 52779
		private Image m_IconLevel;

		// Token: 0x0400CE2C RID: 52780
		private const string LABEL_TXT_LEVEL = "ParameterArea/IconLevel/Text";

		// Token: 0x0400CE2D RID: 52781
		private TextMeshProUGUI m_TextLevel;

		// Token: 0x0400CE2E RID: 52782
		private const string LABEL_IMG_RANK = "ParameterArea/IconRank";

		// Token: 0x0400CE2F RID: 52783
		private Image m_IconRank;

		// Token: 0x0400CE30 RID: 52784
		private const string LABEL_TXT_RANK = "ParameterArea/IconRank/Text";

		// Token: 0x0400CE31 RID: 52785
		private TextMeshProUGUI m_TextRank;

		// Token: 0x0400CE32 RID: 52786
		private const string LABEL_IMG_PENDULUMSCALE = "ParameterArea/IconPendulumScale";

		// Token: 0x0400CE33 RID: 52787
		private Image m_IconPendulumScale;

		// Token: 0x0400CE34 RID: 52788
		private const string LABEL_TXT_PENDULUMSCALE = "ParameterArea/IconPendulumScale/Text";

		// Token: 0x0400CE35 RID: 52789
		private TextMeshProUGUI m_TextPendulumScale;

		// Token: 0x0400CE36 RID: 52790
		private const string LABEL_IMG_LINK = "ParameterArea/IconLink";

		// Token: 0x0400CE37 RID: 52791
		private Image m_IconLink;

		// Token: 0x0400CE38 RID: 52792
		private const string LABEL_TXT_LINK = "ParameterArea/IconLink/Text";

		// Token: 0x0400CE39 RID: 52793
		private TextMeshProUGUI m_TextLink;

		// Token: 0x0400CE3A RID: 52794
		private const string LABEL_IMG_RACE = "ParameterArea/IconRace";

		// Token: 0x0400CE3B RID: 52795
		private Image m_IconRace;

		// Token: 0x0400CE3C RID: 52796
		private const string LABEL_IMG_TUNER = "ParameterArea/IconTuner";

		// Token: 0x0400CE3D RID: 52797
		private Image m_IconTuner;

		// Token: 0x0400CE3E RID: 52798
		private const string LABEL_GO_SPELLTRAPTYPE = "ParameterArea/SpellTrapType";

		// Token: 0x0400CE3F RID: 52799
		private GameObject m_SpellTrapType;

		// Token: 0x0400CE40 RID: 52800
		private const string LABEL_IMG_SPELLTRAPTYPE = "ParameterArea/IconSpellTrapType";

		// Token: 0x0400CE41 RID: 52801
		private Image m_IconSpellTrapType;

		// Token: 0x0400CE42 RID: 52802
		private const string LABEL_TXT_SPELLTRAPTYPE = "ParameterArea/TextSpellTrapType";

		// Token: 0x0400CE43 RID: 52803
		private TextMeshProUGUI m_TextSpellTrapType;

		// Token: 0x0400CE44 RID: 52804
		private const string LABEL_IMG_ATK = "ParameterArea/IconAtk";

		// Token: 0x0400CE45 RID: 52805
		private Image m_IconAtk;

		// Token: 0x0400CE46 RID: 52806
		private const string LABEL_TXT_ATK = "ParameterArea/IconAtk/Text";

		// Token: 0x0400CE47 RID: 52807
		private TextMeshProUGUI m_TextAtk;

		// Token: 0x0400CE48 RID: 52808
		private const string LABEL_IMG_DEF = "ParameterArea/IconDef";

		// Token: 0x0400CE49 RID: 52809
		private Image m_IconDef;

		// Token: 0x0400CE4A RID: 52810
		private const string LABEL_TXT_DEF = "ParameterArea/IconDef/Text";

		// Token: 0x0400CE4B RID: 52811
		private TextMeshProUGUI m_TextDef;

		// Token: 0x0400CE4C RID: 52812
		private const string LABEL_GO_PENDULUMDESCRIPTIONAREA = "DescriptionArea/PendulumDescriptionArea";

		// Token: 0x0400CE4D RID: 52813
		private GameObject m_PendulumDescriptionArea;

		// Token: 0x0400CE4E RID: 52814
		private const string LABEL_MS_PLATEPENDULUMDESCRIPTION = "DescriptionArea/PlatePendulumDescription";

		// Token: 0x0400CE4F RID: 52815
		private MaterialSetter m_PlatePendulumDescription;

		// Token: 0x0400CE50 RID: 52816
		private const string LABEL_SR_PENDULUMAREA = "DescriptionArea/TextAreaPendulum";

		// Token: 0x0400CE51 RID: 52817
		private ScrollRect m_TextAreaPendulum;

		// Token: 0x0400CE52 RID: 52818
		private const string LABEL_TXT_PENDULUMDESCRIPTIONVALUE = "DescriptionArea/TextPendulumDescriptionValue";

		// Token: 0x0400CE53 RID: 52819
		private TextMeshProUGUI m_TextPendulumDescriptionValue;

		// Token: 0x0400CE54 RID: 52820
		private const string LABEL_MS_PLATEDESCRIPTION = "DescriptionArea/PlateDescription";

		// Token: 0x0400CE55 RID: 52821
		private MaterialSetter m_PlateDescription;

		// Token: 0x0400CE56 RID: 52822
		private const string LABEL_TXT_DESCRIPTIONITEM = "DescriptionArea/TextDescriptionItem";

		// Token: 0x0400CE57 RID: 52823
		private TextMeshProUGUI m_TextDescriptionItem;

		// Token: 0x0400CE58 RID: 52824
		private const string LABEL_SR_AREA = "DescriptionArea/TextArea";

		// Token: 0x0400CE59 RID: 52825
		private ScrollRect m_TextArea;

		// Token: 0x0400CE5A RID: 52826
		private const string LABEL_TXT_DESCRIPTIONVALUE = "DescriptionArea/TextDescriptionValue";

		// Token: 0x0400CE5B RID: 52827
		private TextMeshProUGUI m_TextDescriptionValue;

		// Token: 0x0400CE5C RID: 52828
		private const string LABEL_RIMG_CARD = "CardArea/ImageCard";

		// Token: 0x0400CE5D RID: 52829
		private CardRawImageHandler m_ImageCard;

		// Token: 0x0400CE5E RID: 52830
		private const string LABEL_IMG_LIMIT = "CardArea/IconLimit";

		// Token: 0x0400CE5F RID: 52831
		private Image m_IconLimit;

		// Token: 0x0400CE60 RID: 52832
		private const string LABEL_TXT_CARDNUMVALUE = "CardArea/TextCardNumValue";

		// Token: 0x0400CE61 RID: 52833
		private TextMeshProUGUI m_TextCardNumValue;

		// Token: 0x0400CE62 RID: 52834
		private const string LABEL_GO_POOLAREA = "CardArea/PoolArea";

		// Token: 0x0400CE63 RID: 52835
		private GameObject m_PoolArea;

		// Token: 0x0400CE64 RID: 52836
		private const string LABEL_IMG_ICONOCG = "CardArea/IconOCG";

		// Token: 0x0400CE65 RID: 52837
		private Image m_IconOCG;

		// Token: 0x0400CE66 RID: 52838
		private const string LABEL_IMG_ICONTCG = "CardArea/IconTCG";

		// Token: 0x0400CE67 RID: 52839
		private Image m_IconTCG;

		// Token: 0x0400CE68 RID: 52840
		private const string LABEL_IMG_ICONSCCG = "CardArea/IconSCCG";

		// Token: 0x0400CE69 RID: 52841
		private Image m_IconSCCG;

		// Token: 0x0400CE6A RID: 52842
		private const string LABEL_IMG_ICONDIY = "CardArea/IconDIY";

		// Token: 0x0400CE6B RID: 52843
		private Image m_IconDIY;

		// Token: 0x0400CE6C RID: 52844
		private const string LABEL_IMG_ICONPRE = "CardArea/IconPRE";

		// Token: 0x0400CE6D RID: 52845
		private Image m_IconPRE;

		// Token: 0x0400CE6E RID: 52846
		private const string LABEL_GO_MENUAREA = "MenuArea";

		// Token: 0x0400CE6F RID: 52847
		private GameObject m_MenuArea;

		// Token: 0x0400CE70 RID: 52848
		private const string LABEL_STG_BOOKMARK = "MenuArea/BookmarkToggleButton";

		// Token: 0x0400CE71 RID: 52849
		private SelectionToggle m_ToggleBookMark;

		// Token: 0x0400CE72 RID: 52850
		private const string LABEL_SBN_RELATEDCARD = "MenuArea/RelatedCardButton";

		// Token: 0x0400CE73 RID: 52851
		private SelectionButton m_ButtonRelatedCard;

		// Token: 0x0400CE74 RID: 52852
		private const string LABEL_SBN_ADDCARD = "MenuArea/AddCardButton";

		// Token: 0x0400CE75 RID: 52853
		private SelectionButton m_ButtonAddCard;

		// Token: 0x0400CE76 RID: 52854
		private const string LABEL_SBN_REMOVECARD = "MenuArea/RemoveCardButton";

		// Token: 0x0400CE77 RID: 52855
		private SelectionButton m_ButtonRemoveCard;

		// Token: 0x0400CE78 RID: 52856
		private const string LABEL_STG_RARITYR = "MenuArea/ToggleRarityR";

		// Token: 0x0400CE79 RID: 52857
		private SelectionToggle_Rarity m_ToggleRarityR;

		// Token: 0x0400CE7A RID: 52858
		private const string LABEL_STG_RARITYUR = "MenuArea/ToggleRarityUR";

		// Token: 0x0400CE7B RID: 52859
		private SelectionToggle_Rarity m_ToggleRarityUR;

		// Token: 0x0400CE7C RID: 52860
		private const string LABEL_STG_RARITYGR = "MenuArea/ToggleRarityGR";

		// Token: 0x0400CE7D RID: 52861
		private SelectionToggle_Rarity m_ToggleRarityGR;

		// Token: 0x0400CE7E RID: 52862
		private const string LABEL_STG_RARITYMR = "MenuArea/ToggleRarityMR";

		// Token: 0x0400CE7F RID: 52863
		private SelectionToggle_Rarity m_ToggleRarityMR;

		// Token: 0x0400CE80 RID: 52864
		private readonly ElementObjectManager Manager;

		// Token: 0x0400CE81 RID: 52865
		private Card _card;

		// Token: 0x0400CE82 RID: 52866
		private readonly bool pendulumTextNeedSplit = true;
	}
}
