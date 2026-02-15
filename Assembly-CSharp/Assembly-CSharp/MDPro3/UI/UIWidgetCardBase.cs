using System;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001435 RID: 5173
	public class UIWidgetCardBase : UIWidget
	{
		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x0600963B RID: 38459 RVA: 0x0015C0E0 File Offset: 0x0015A2E0
		protected MaterialSetter PlateTitle
		{
			get
			{
				return this.m_PlateTitle = ((this.m_PlateTitle != null) ? this.m_PlateTitle : base.Manager.GetNestedElement<MaterialSetter>("TitleArea/PlateTitle"));
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x0600963C RID: 38460 RVA: 0x0015C11C File Offset: 0x0015A31C
		protected TextMeshProUGUI TextCardName
		{
			get
			{
				return this.m_TextCardName = ((this.m_TextCardName != null) ? this.m_TextCardName : base.Manager.GetNestedElement<TextMeshProUGUI>("TitleArea/TextCardName"));
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x0600963D RID: 38461 RVA: 0x0015C158 File Offset: 0x0015A358
		protected Image IconAttribute
		{
			get
			{
				return this.m_IconAttribute = ((this.m_IconAttribute != null) ? this.m_IconAttribute : base.Manager.GetNestedElement<Image>("TitleArea/IconAttribute"));
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x0600963E RID: 38462 RVA: 0x0015C194 File Offset: 0x0015A394
		protected Image IconLevel
		{
			get
			{
				return this.m_IconLevel = ((this.m_IconLevel != null) ? this.m_IconLevel : base.Manager.GetNestedElement<Image>("ParameterArea/IconLevel"));
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x0600963F RID: 38463 RVA: 0x0015C1D0 File Offset: 0x0015A3D0
		protected TextMeshProUGUI TextLevel
		{
			get
			{
				return this.m_TextLevel = ((this.m_TextLevel != null) ? this.m_TextLevel : base.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconLevel/Text"));
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06009640 RID: 38464 RVA: 0x0015C20C File Offset: 0x0015A40C
		protected Image IconRank
		{
			get
			{
				return this.m_IconRank = ((this.m_IconRank != null) ? this.m_IconRank : base.Manager.GetNestedElement<Image>("ParameterArea/IconRank"));
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06009641 RID: 38465 RVA: 0x0015C248 File Offset: 0x0015A448
		protected TextMeshProUGUI TextRank
		{
			get
			{
				return this.m_TextRank = ((this.m_TextRank != null) ? this.m_TextRank : base.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconRank/Text"));
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06009642 RID: 38466 RVA: 0x0015C284 File Offset: 0x0015A484
		protected Image IconPendulumScale
		{
			get
			{
				return this.m_IconPendulumScale = ((this.m_IconPendulumScale != null) ? this.m_IconPendulumScale : base.Manager.GetNestedElement<Image>("ParameterArea/IconPendulumScale"));
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06009643 RID: 38467 RVA: 0x0015C2C0 File Offset: 0x0015A4C0
		protected TextMeshProUGUI TextPendulumScale
		{
			get
			{
				return this.m_TextPendulumScale = ((this.m_TextPendulumScale != null) ? this.m_TextPendulumScale : base.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconPendulumScale/Text"));
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06009644 RID: 38468 RVA: 0x0015C2FC File Offset: 0x0015A4FC
		protected Image IconLink
		{
			get
			{
				return this.m_IconLink = ((this.m_IconLink != null) ? this.m_IconLink : base.Manager.GetNestedElement<Image>("ParameterArea/IconLink"));
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06009645 RID: 38469 RVA: 0x0015C338 File Offset: 0x0015A538
		protected TextMeshProUGUI TextLink
		{
			get
			{
				return this.m_TextLink = ((this.m_TextLink != null) ? this.m_TextLink : base.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconLink/Text"));
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06009646 RID: 38470 RVA: 0x0015C374 File Offset: 0x0015A574
		protected Image IconRace
		{
			get
			{
				return this.m_IconRace = ((this.m_IconRace != null) ? this.m_IconRace : base.Manager.GetNestedElement<Image>("ParameterArea/IconRace"));
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06009647 RID: 38471 RVA: 0x0015C3B0 File Offset: 0x0015A5B0
		protected Image IconTuner
		{
			get
			{
				return this.m_IconTuner = ((this.m_IconTuner != null) ? this.m_IconTuner : base.Manager.GetNestedElement<Image>("ParameterArea/IconTuner"));
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06009648 RID: 38472 RVA: 0x0015C3EC File Offset: 0x0015A5EC
		protected GameObject SpellTrapType
		{
			get
			{
				return this.m_SpellTrapType = ((this.m_SpellTrapType != null) ? this.m_SpellTrapType : base.Manager.GetNestedElement("ParameterArea/SpellTrapType"));
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06009649 RID: 38473 RVA: 0x0015C428 File Offset: 0x0015A628
		protected Image IconSpellTrapType
		{
			get
			{
				return this.m_IconSpellTrapType = ((this.m_IconSpellTrapType != null) ? this.m_IconSpellTrapType : base.Manager.GetNestedElement<Image>("ParameterArea/IconSpellTrapType"));
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x0600964A RID: 38474 RVA: 0x0015C464 File Offset: 0x0015A664
		protected TextMeshProUGUI TextSpellTrapType
		{
			get
			{
				return this.m_TextSpellTrapType = ((this.m_TextSpellTrapType != null) ? this.m_TextSpellTrapType : base.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/TextSpellTrapType"));
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x0600964B RID: 38475 RVA: 0x0015C4A0 File Offset: 0x0015A6A0
		protected Image IconAtk
		{
			get
			{
				return this.m_IconAtk = ((this.m_IconAtk != null) ? this.m_IconAtk : base.Manager.GetNestedElement<Image>("ParameterArea/IconAtk"));
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x0600964C RID: 38476 RVA: 0x0015C4DC File Offset: 0x0015A6DC
		protected TextMeshProUGUI TextAtk
		{
			get
			{
				return this.m_TextAtk = ((this.m_TextAtk != null) ? this.m_TextAtk : base.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconAtk/Text"));
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x0600964D RID: 38477 RVA: 0x0015C518 File Offset: 0x0015A718
		protected Image IconDef
		{
			get
			{
				return this.m_IconDef = ((this.m_IconDef != null) ? this.m_IconDef : base.Manager.GetNestedElement<Image>("ParameterArea/IconDef"));
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x0600964E RID: 38478 RVA: 0x0015C554 File Offset: 0x0015A754
		protected TextMeshProUGUI TextDef
		{
			get
			{
				return this.m_TextDef = ((this.m_TextDef != null) ? this.m_TextDef : base.Manager.GetNestedElement<TextMeshProUGUI>("ParameterArea/IconDef/Text"));
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x0600964F RID: 38479 RVA: 0x0015C590 File Offset: 0x0015A790
		protected GameObject PendulumDescriptionArea
		{
			get
			{
				return this.m_PendulumDescriptionArea = ((this.m_PendulumDescriptionArea != null) ? this.m_PendulumDescriptionArea : base.Manager.GetNestedElement("DescriptionArea/PendulumDescriptionArea"));
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06009650 RID: 38480 RVA: 0x0015C5CC File Offset: 0x0015A7CC
		protected MaterialSetter PlatePendulumDescription
		{
			get
			{
				return this.m_PlatePendulumDescription = ((this.m_PlatePendulumDescription != null) ? this.m_PlatePendulumDescription : base.Manager.GetNestedElement<MaterialSetter>("DescriptionArea/PlatePendulumDescription"));
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06009651 RID: 38481 RVA: 0x0015C608 File Offset: 0x0015A808
		protected ScrollRect TextAreaPendulum
		{
			get
			{
				return this.m_TextAreaPendulum = ((this.m_TextAreaPendulum != null) ? this.m_TextAreaPendulum : base.Manager.GetNestedElement<ScrollRect>("DescriptionArea/TextAreaPendulum"));
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06009652 RID: 38482 RVA: 0x0015C644 File Offset: 0x0015A844
		protected TextMeshProUGUI TextPendulumDescriptionValue
		{
			get
			{
				return this.m_TextPendulumDescriptionValue = ((this.m_TextPendulumDescriptionValue != null) ? this.m_TextPendulumDescriptionValue : base.Manager.GetNestedElement<TextMeshProUGUI>("DescriptionArea/TextPendulumDescriptionValue"));
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06009653 RID: 38483 RVA: 0x0015C680 File Offset: 0x0015A880
		protected MaterialSetter PlateDescription
		{
			get
			{
				return this.m_PlateDescription = ((this.m_PlateDescription != null) ? this.m_PlateDescription : base.Manager.GetNestedElement<MaterialSetter>("DescriptionArea/PlateDescription"));
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06009654 RID: 38484 RVA: 0x0015C6BC File Offset: 0x0015A8BC
		protected TextMeshProUGUI TextDescriptionItem
		{
			get
			{
				return this.m_TextDescriptionItem = ((this.m_TextDescriptionItem != null) ? this.m_TextDescriptionItem : base.Manager.GetNestedElement<TextMeshProUGUI>("DescriptionArea/TextDescriptionItem"));
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06009655 RID: 38485 RVA: 0x0015C6F8 File Offset: 0x0015A8F8
		protected ScrollRect TextArea
		{
			get
			{
				return this.m_TextArea = ((this.m_TextArea != null) ? this.m_TextArea : base.Manager.GetNestedElement<ScrollRect>("DescriptionArea/TextArea"));
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06009656 RID: 38486 RVA: 0x0015C734 File Offset: 0x0015A934
		protected TextMeshProUGUI TextDescriptionValue
		{
			get
			{
				return this.m_TextDescriptionValue = ((this.m_TextDescriptionValue != null) ? this.m_TextDescriptionValue : base.Manager.GetNestedElement<TextMeshProUGUI>("DescriptionArea/TextDescriptionValue"));
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06009657 RID: 38487 RVA: 0x0015C770 File Offset: 0x0015A970
		protected CardRawImageHandler ImageCard
		{
			get
			{
				return this.m_ImageCard = ((this.m_ImageCard != null) ? this.m_ImageCard : base.Manager.GetNestedElement<CardRawImageHandler>("CardArea/ImageCard"));
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06009658 RID: 38488 RVA: 0x0015C7AC File Offset: 0x0015A9AC
		protected Image IconLimit
		{
			get
			{
				return this.m_IconLimit = ((this.m_IconLimit != null) ? this.m_IconLimit : base.Manager.GetNestedElement<Image>("CardArea/IconLimit"));
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06009659 RID: 38489 RVA: 0x0015C7E8 File Offset: 0x0015A9E8
		protected TextMeshProUGUI TextCardNumValue
		{
			get
			{
				return this.m_TextCardNumValue = ((this.m_TextCardNumValue != null) ? this.m_TextCardNumValue : base.Manager.GetNestedElement<TextMeshProUGUI>("CardArea/TextCardNumValue"));
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x0600965A RID: 38490 RVA: 0x0015C824 File Offset: 0x0015AA24
		protected GameObject PoolArea
		{
			get
			{
				return this.m_PoolArea = ((this.m_PoolArea != null) ? this.m_PoolArea : base.Manager.GetNestedElement("CardArea/PoolArea"));
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x0600965B RID: 38491 RVA: 0x0015C860 File Offset: 0x0015AA60
		protected Image IconOCG
		{
			get
			{
				return this.m_IconOCG = ((this.m_IconOCG != null) ? this.m_IconOCG : base.Manager.GetNestedElement<Image>("CardArea/IconOCG"));
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x0600965C RID: 38492 RVA: 0x0015C89C File Offset: 0x0015AA9C
		protected Image IconTCG
		{
			get
			{
				return this.m_IconTCG = ((this.m_IconTCG != null) ? this.m_IconTCG : base.Manager.GetNestedElement<Image>("CardArea/IconTCG"));
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x0600965D RID: 38493 RVA: 0x0015C8D8 File Offset: 0x0015AAD8
		protected Image IconSCCG
		{
			get
			{
				return this.m_IconSCCG = ((this.m_IconSCCG != null) ? this.m_IconSCCG : base.Manager.GetNestedElement<Image>("CardArea/IconSCCG"));
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x0600965E RID: 38494 RVA: 0x0015C914 File Offset: 0x0015AB14
		protected Image IconDIY
		{
			get
			{
				return this.m_IconDIY = ((this.m_IconDIY != null) ? this.m_IconDIY : base.Manager.GetNestedElement<Image>("CardArea/IconDIY"));
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x0600965F RID: 38495 RVA: 0x0015C950 File Offset: 0x0015AB50
		protected Image IconPRE
		{
			get
			{
				return this.m_IconPRE = ((this.m_IconPRE != null) ? this.m_IconPRE : base.Manager.GetNestedElement<Image>("CardArea/IconPRE"));
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06009660 RID: 38496 RVA: 0x0015C98C File Offset: 0x0015AB8C
		protected TextMeshProUGUI TextGP
		{
			get
			{
				return this.m_TextGP = ((this.m_TextGP != null) ? this.m_TextGP : base.Manager.GetNestedElement<TextMeshProUGUI>("CardArea/TextGP"));
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06009661 RID: 38497 RVA: 0x0015C9C8 File Offset: 0x0015ABC8
		protected GameObject MenuArea
		{
			get
			{
				return this.m_MenuArea = ((this.m_MenuArea != null) ? this.m_MenuArea : base.Manager.GetElement("MenuArea"));
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06009662 RID: 38498 RVA: 0x0015CA04 File Offset: 0x0015AC04
		protected SelectionToggle ToggleBookMark
		{
			get
			{
				return this.m_ToggleBookMark = ((this.m_ToggleBookMark != null) ? this.m_ToggleBookMark : base.Manager.GetNestedElement<SelectionToggle>("MenuArea/BookmarkToggleButton"));
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06009663 RID: 38499 RVA: 0x0015CA40 File Offset: 0x0015AC40
		protected SelectionButton ButtonRelatedCard
		{
			get
			{
				return this.m_ButtonRelatedCard = ((this.m_ButtonRelatedCard != null) ? this.m_ButtonRelatedCard : base.Manager.GetNestedElement<SelectionButton>("MenuArea/RelatedCardButton"));
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06009664 RID: 38500 RVA: 0x0015CA7C File Offset: 0x0015AC7C
		public SelectionButton ButtonAddCard
		{
			get
			{
				return this.m_ButtonAddCard = ((this.m_ButtonAddCard != null) ? this.m_ButtonAddCard : base.Manager.GetNestedElement<SelectionButton>("MenuArea/AddCardButton"));
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06009665 RID: 38501 RVA: 0x0015CAB8 File Offset: 0x0015ACB8
		public SelectionButton ButtonRemoveCard
		{
			get
			{
				return this.m_ButtonRemoveCard = ((this.m_ButtonRemoveCard != null) ? this.m_ButtonRemoveCard : base.Manager.GetNestedElement<SelectionButton>("MenuArea/RemoveCardButton"));
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06009666 RID: 38502 RVA: 0x0015CAF4 File Offset: 0x0015ACF4
		protected SelectionToggle_Rarity ToggleRarityR
		{
			get
			{
				return this.m_ToggleRarityR = ((this.m_ToggleRarityR != null) ? this.m_ToggleRarityR : base.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityR"));
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06009667 RID: 38503 RVA: 0x0015CB30 File Offset: 0x0015AD30
		protected SelectionToggle_Rarity ToggleRarityUR
		{
			get
			{
				return this.m_ToggleRarityUR = ((this.m_ToggleRarityUR != null) ? this.m_ToggleRarityUR : base.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityUR"));
			}
		}

		// Token: 0x17001385 RID: 4997
		// (get) Token: 0x06009668 RID: 38504 RVA: 0x0015CB6C File Offset: 0x0015AD6C
		protected SelectionToggle_Rarity ToggleRarityGR
		{
			get
			{
				return this.m_ToggleRarityGR = ((this.m_ToggleRarityGR != null) ? this.m_ToggleRarityGR : base.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityGR"));
			}
		}

		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06009669 RID: 38505 RVA: 0x0015CBA8 File Offset: 0x0015ADA8
		protected SelectionToggle_Rarity ToggleRarityMR
		{
			get
			{
				return this.m_ToggleRarityMR = ((this.m_ToggleRarityMR != null) ? this.m_ToggleRarityMR : base.Manager.GetNestedElement<SelectionToggle_Rarity>("MenuArea/ToggleRarityMR"));
			}
		}

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x0600966A RID: 38506 RVA: 0x0015CBE4 File Offset: 0x0015ADE4
		// (set) Token: 0x0600966B RID: 38507 RVA: 0x0015CBEC File Offset: 0x0015ADEC
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

		// Token: 0x0600966C RID: 38508 RVA: 0x0015CC04 File Offset: 0x0015AE04
		protected override void Awake()
		{
			base.Awake();
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

		// Token: 0x0600966D RID: 38509 RVA: 0x0015CC84 File Offset: 0x0015AE84
		protected virtual void SetCardData(Card data)
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
				if (this.TextGP != null)
				{
					int gp = OnlineService.GetGenesysPoint(data.GetOriginalID());
					string gpString = OnlineService.GetGenesysPointString(data.GetOriginalID());
					this.TextGP.text = string.Format("G:{0}", gpString);
					this.TextGP.color = OnlineService.GetGenesysPointColor(gp);
				}
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
				this.TextSpellTrapType.text = data.GetSpellTrapType(false);
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

		// Token: 0x0600966E RID: 38510 RVA: 0x0015D1F2 File Offset: 0x0015B3F2
		public virtual void SetCardCount(string cardCount)
		{
			this.TextCardNumValue.text = cardCount;
		}

		// Token: 0x0600966F RID: 38511 RVA: 0x0015D200 File Offset: 0x0015B400
		public virtual void SetCardCount()
		{
			if (this.Card == null)
			{
				return;
			}
			this.TextCardNumValue.text = Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.GetCardCount(this.Card.Id).ToString();
		}

		// Token: 0x06009670 RID: 38512 RVA: 0x0015D24D File Offset: 0x0015B44D
		public virtual void RefreshRarity(int code)
		{
			this.ImageCard.RefreshRarity(code);
		}

		// Token: 0x06009671 RID: 38513 RVA: 0x0015D25B File Offset: 0x0015B45B
		public virtual void RefreshBookmarkToggle()
		{
			if (CardRarity.CardBookmarked(this.Card.Id))
			{
				this.ToggleBookMark.SetToggleOn(false);
				return;
			}
			this.ToggleBookMark.SetToggleOff(false);
		}

		// Token: 0x06009672 RID: 38514 RVA: 0x0015D288 File Offset: 0x0015B488
		public virtual void SetRelatedCardEvent(UnityAction call)
		{
			if (this.ButtonRelatedCard == null)
			{
				return;
			}
			this.ButtonRelatedCard.SetClickEvent(call);
		}

		// Token: 0x0400D485 RID: 54405
		private const string LABEL_MS_PLATETITLE = "TitleArea/PlateTitle";

		// Token: 0x0400D486 RID: 54406
		private MaterialSetter m_PlateTitle;

		// Token: 0x0400D487 RID: 54407
		private const string LABEL_TXT_CARDNAME = "TitleArea/TextCardName";

		// Token: 0x0400D488 RID: 54408
		private TextMeshProUGUI m_TextCardName;

		// Token: 0x0400D489 RID: 54409
		private const string LABEL_IMG_ATTRIBUTE = "TitleArea/IconAttribute";

		// Token: 0x0400D48A RID: 54410
		private Image m_IconAttribute;

		// Token: 0x0400D48B RID: 54411
		private const string LABEL_IMG_LEVEL = "ParameterArea/IconLevel";

		// Token: 0x0400D48C RID: 54412
		private Image m_IconLevel;

		// Token: 0x0400D48D RID: 54413
		private const string LABEL_TXT_LEVEL = "ParameterArea/IconLevel/Text";

		// Token: 0x0400D48E RID: 54414
		private TextMeshProUGUI m_TextLevel;

		// Token: 0x0400D48F RID: 54415
		private const string LABEL_IMG_RANK = "ParameterArea/IconRank";

		// Token: 0x0400D490 RID: 54416
		private Image m_IconRank;

		// Token: 0x0400D491 RID: 54417
		private const string LABEL_TXT_RANK = "ParameterArea/IconRank/Text";

		// Token: 0x0400D492 RID: 54418
		private TextMeshProUGUI m_TextRank;

		// Token: 0x0400D493 RID: 54419
		private const string LABEL_IMG_PENDULUMSCALE = "ParameterArea/IconPendulumScale";

		// Token: 0x0400D494 RID: 54420
		private Image m_IconPendulumScale;

		// Token: 0x0400D495 RID: 54421
		private const string LABEL_TXT_PENDULUMSCALE = "ParameterArea/IconPendulumScale/Text";

		// Token: 0x0400D496 RID: 54422
		private TextMeshProUGUI m_TextPendulumScale;

		// Token: 0x0400D497 RID: 54423
		private const string LABEL_IMG_LINK = "ParameterArea/IconLink";

		// Token: 0x0400D498 RID: 54424
		private Image m_IconLink;

		// Token: 0x0400D499 RID: 54425
		private const string LABEL_TXT_LINK = "ParameterArea/IconLink/Text";

		// Token: 0x0400D49A RID: 54426
		private TextMeshProUGUI m_TextLink;

		// Token: 0x0400D49B RID: 54427
		private const string LABEL_IMG_RACE = "ParameterArea/IconRace";

		// Token: 0x0400D49C RID: 54428
		private Image m_IconRace;

		// Token: 0x0400D49D RID: 54429
		private const string LABEL_IMG_TUNER = "ParameterArea/IconTuner";

		// Token: 0x0400D49E RID: 54430
		private Image m_IconTuner;

		// Token: 0x0400D49F RID: 54431
		private const string LABEL_GO_SPELLTRAPTYPE = "ParameterArea/SpellTrapType";

		// Token: 0x0400D4A0 RID: 54432
		private GameObject m_SpellTrapType;

		// Token: 0x0400D4A1 RID: 54433
		private const string LABEL_IMG_SPELLTRAPTYPE = "ParameterArea/IconSpellTrapType";

		// Token: 0x0400D4A2 RID: 54434
		private Image m_IconSpellTrapType;

		// Token: 0x0400D4A3 RID: 54435
		private const string LABEL_TXT_SPELLTRAPTYPE = "ParameterArea/TextSpellTrapType";

		// Token: 0x0400D4A4 RID: 54436
		private TextMeshProUGUI m_TextSpellTrapType;

		// Token: 0x0400D4A5 RID: 54437
		private const string LABEL_IMG_ATK = "ParameterArea/IconAtk";

		// Token: 0x0400D4A6 RID: 54438
		private Image m_IconAtk;

		// Token: 0x0400D4A7 RID: 54439
		private const string LABEL_TXT_ATK = "ParameterArea/IconAtk/Text";

		// Token: 0x0400D4A8 RID: 54440
		private TextMeshProUGUI m_TextAtk;

		// Token: 0x0400D4A9 RID: 54441
		private const string LABEL_IMG_DEF = "ParameterArea/IconDef";

		// Token: 0x0400D4AA RID: 54442
		private Image m_IconDef;

		// Token: 0x0400D4AB RID: 54443
		private const string LABEL_TXT_DEF = "ParameterArea/IconDef/Text";

		// Token: 0x0400D4AC RID: 54444
		private TextMeshProUGUI m_TextDef;

		// Token: 0x0400D4AD RID: 54445
		private const string LABEL_GO_PENDULUMDESCRIPTIONAREA = "DescriptionArea/PendulumDescriptionArea";

		// Token: 0x0400D4AE RID: 54446
		private GameObject m_PendulumDescriptionArea;

		// Token: 0x0400D4AF RID: 54447
		private const string LABEL_MS_PLATEPENDULUMDESCRIPTION = "DescriptionArea/PlatePendulumDescription";

		// Token: 0x0400D4B0 RID: 54448
		private MaterialSetter m_PlatePendulumDescription;

		// Token: 0x0400D4B1 RID: 54449
		private const string LABEL_SR_PENDULUMAREA = "DescriptionArea/TextAreaPendulum";

		// Token: 0x0400D4B2 RID: 54450
		private ScrollRect m_TextAreaPendulum;

		// Token: 0x0400D4B3 RID: 54451
		private const string LABEL_TXT_PENDULUMDESCRIPTIONVALUE = "DescriptionArea/TextPendulumDescriptionValue";

		// Token: 0x0400D4B4 RID: 54452
		private TextMeshProUGUI m_TextPendulumDescriptionValue;

		// Token: 0x0400D4B5 RID: 54453
		private const string LABEL_MS_PLATEDESCRIPTION = "DescriptionArea/PlateDescription";

		// Token: 0x0400D4B6 RID: 54454
		private MaterialSetter m_PlateDescription;

		// Token: 0x0400D4B7 RID: 54455
		private const string LABEL_TXT_DESCRIPTIONITEM = "DescriptionArea/TextDescriptionItem";

		// Token: 0x0400D4B8 RID: 54456
		private TextMeshProUGUI m_TextDescriptionItem;

		// Token: 0x0400D4B9 RID: 54457
		private const string LABEL_SR_AREA = "DescriptionArea/TextArea";

		// Token: 0x0400D4BA RID: 54458
		private ScrollRect m_TextArea;

		// Token: 0x0400D4BB RID: 54459
		private const string LABEL_TXT_DESCRIPTIONVALUE = "DescriptionArea/TextDescriptionValue";

		// Token: 0x0400D4BC RID: 54460
		private TextMeshProUGUI m_TextDescriptionValue;

		// Token: 0x0400D4BD RID: 54461
		private const string LABEL_RIMG_CARD = "CardArea/ImageCard";

		// Token: 0x0400D4BE RID: 54462
		private CardRawImageHandler m_ImageCard;

		// Token: 0x0400D4BF RID: 54463
		private const string LABEL_IMG_LIMIT = "CardArea/IconLimit";

		// Token: 0x0400D4C0 RID: 54464
		private Image m_IconLimit;

		// Token: 0x0400D4C1 RID: 54465
		private const string LABEL_TXT_CARDNUMVALUE = "CardArea/TextCardNumValue";

		// Token: 0x0400D4C2 RID: 54466
		private TextMeshProUGUI m_TextCardNumValue;

		// Token: 0x0400D4C3 RID: 54467
		private const string LABEL_GO_POOLAREA = "CardArea/PoolArea";

		// Token: 0x0400D4C4 RID: 54468
		private GameObject m_PoolArea;

		// Token: 0x0400D4C5 RID: 54469
		private const string LABEL_IMG_ICONOCG = "CardArea/IconOCG";

		// Token: 0x0400D4C6 RID: 54470
		private Image m_IconOCG;

		// Token: 0x0400D4C7 RID: 54471
		private const string LABEL_IMG_ICONTCG = "CardArea/IconTCG";

		// Token: 0x0400D4C8 RID: 54472
		private Image m_IconTCG;

		// Token: 0x0400D4C9 RID: 54473
		private const string LABEL_IMG_ICONSCCG = "CardArea/IconSCCG";

		// Token: 0x0400D4CA RID: 54474
		private Image m_IconSCCG;

		// Token: 0x0400D4CB RID: 54475
		private const string LABEL_IMG_ICONDIY = "CardArea/IconDIY";

		// Token: 0x0400D4CC RID: 54476
		private Image m_IconDIY;

		// Token: 0x0400D4CD RID: 54477
		private const string LABEL_IMG_ICONPRE = "CardArea/IconPRE";

		// Token: 0x0400D4CE RID: 54478
		private Image m_IconPRE;

		// Token: 0x0400D4CF RID: 54479
		private const string LABEL_TXT_GP = "CardArea/TextGP";

		// Token: 0x0400D4D0 RID: 54480
		private TextMeshProUGUI m_TextGP;

		// Token: 0x0400D4D1 RID: 54481
		private const string LABEL_GO_MENUAREA = "MenuArea";

		// Token: 0x0400D4D2 RID: 54482
		private GameObject m_MenuArea;

		// Token: 0x0400D4D3 RID: 54483
		private const string LABEL_STG_BOOKMARK = "MenuArea/BookmarkToggleButton";

		// Token: 0x0400D4D4 RID: 54484
		private SelectionToggle m_ToggleBookMark;

		// Token: 0x0400D4D5 RID: 54485
		private const string LABEL_SBN_RELATEDCARD = "MenuArea/RelatedCardButton";

		// Token: 0x0400D4D6 RID: 54486
		private SelectionButton m_ButtonRelatedCard;

		// Token: 0x0400D4D7 RID: 54487
		private const string LABEL_SBN_ADDCARD = "MenuArea/AddCardButton";

		// Token: 0x0400D4D8 RID: 54488
		private SelectionButton m_ButtonAddCard;

		// Token: 0x0400D4D9 RID: 54489
		private const string LABEL_SBN_REMOVECARD = "MenuArea/RemoveCardButton";

		// Token: 0x0400D4DA RID: 54490
		private SelectionButton m_ButtonRemoveCard;

		// Token: 0x0400D4DB RID: 54491
		private const string LABEL_STG_RARITYR = "MenuArea/ToggleRarityR";

		// Token: 0x0400D4DC RID: 54492
		private SelectionToggle_Rarity m_ToggleRarityR;

		// Token: 0x0400D4DD RID: 54493
		private const string LABEL_STG_RARITYUR = "MenuArea/ToggleRarityUR";

		// Token: 0x0400D4DE RID: 54494
		private SelectionToggle_Rarity m_ToggleRarityUR;

		// Token: 0x0400D4DF RID: 54495
		private const string LABEL_STG_RARITYGR = "MenuArea/ToggleRarityGR";

		// Token: 0x0400D4E0 RID: 54496
		private SelectionToggle_Rarity m_ToggleRarityGR;

		// Token: 0x0400D4E1 RID: 54497
		private const string LABEL_STG_RARITYMR = "MenuArea/ToggleRarityMR";

		// Token: 0x0400D4E2 RID: 54498
		private SelectionToggle_Rarity m_ToggleRarityMR;

		// Token: 0x0400D4E3 RID: 54499
		private Card _card;

		// Token: 0x0400D4E4 RID: 54500
		protected bool pendulumTextNeedSplit = true;
	}
}
