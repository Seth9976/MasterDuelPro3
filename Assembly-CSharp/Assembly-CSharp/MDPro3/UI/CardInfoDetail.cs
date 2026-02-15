using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.Popup;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200141E RID: 5150
	public class CardInfoDetail : UIWidgetFullScreen
	{
		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x0600951F RID: 38175 RVA: 0x001558AC File Offset: 0x00153AAC
		private SelectionButton ButtonNext
		{
			get
			{
				return this.m_ButtonNext = ((this.m_ButtonNext != null) ? this.m_ButtonNext : base.Manager.GetElement<SelectionButton>("NextButton"));
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06009520 RID: 38176 RVA: 0x001558E8 File Offset: 0x00153AE8
		private SelectionButton ButtonPrev
		{
			get
			{
				return this.m_ButtonPrev = ((this.m_ButtonPrev != null) ? this.m_ButtonPrev : base.Manager.GetElement<SelectionButton>("PrevButton"));
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06009521 RID: 38177 RVA: 0x00155924 File Offset: 0x00153B24
		private CardRawImageHandler ImageCard
		{
			get
			{
				return this.m_ImageCard = ((this.m_ImageCard != null) ? this.m_ImageCard : base.Manager.GetElement<CardRawImageHandler>("ImageCard"));
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06009522 RID: 38178 RVA: 0x00155960 File Offset: 0x00153B60
		protected Image IconLimit
		{
			get
			{
				return this.m_IconLimit = ((this.m_IconLimit != null) ? this.m_IconLimit : base.Manager.GetElement<Image>("IconLimit"));
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06009523 RID: 38179 RVA: 0x0015599C File Offset: 0x00153B9C
		protected MaterialSetter PlateTitle
		{
			get
			{
				return this.m_PlateTitle = ((this.m_PlateTitle != null) ? this.m_PlateTitle : base.Manager.GetElement<MaterialSetter>("PlateTitle"));
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06009524 RID: 38180 RVA: 0x001559D8 File Offset: 0x00153BD8
		protected TextMeshProUGUI TextCardName
		{
			get
			{
				return this.m_TextCardName = ((this.m_TextCardName != null) ? this.m_TextCardName : base.Manager.GetElement<TextMeshProUGUI>("TextCardName"));
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06009525 RID: 38181 RVA: 0x00155A14 File Offset: 0x00153C14
		private Image IconAttribute
		{
			get
			{
				return this.m_IconAttribute = ((this.m_IconAttribute != null) ? this.m_IconAttribute : base.Manager.GetElement<Image>("IconAttribute"));
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06009526 RID: 38182 RVA: 0x00155A50 File Offset: 0x00153C50
		protected MaterialSetter PlateParamator
		{
			get
			{
				return this.m_PlateParamator = ((this.m_PlateParamator != null) ? this.m_PlateParamator : base.Manager.GetElement<MaterialSetter>("PlateParamator"));
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06009527 RID: 38183 RVA: 0x00155A8C File Offset: 0x00153C8C
		private GameObject ParamatorAreaTop
		{
			get
			{
				return this.m_ParamatorAreaTop = ((this.m_ParamatorAreaTop != null) ? this.m_ParamatorAreaTop : base.Manager.GetElement("ParamatorAreaTop"));
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06009528 RID: 38184 RVA: 0x00155AC8 File Offset: 0x00153CC8
		private GameObject ParamatorAreaBottom
		{
			get
			{
				return this.m_ParamatorAreaBottom = ((this.m_ParamatorAreaBottom != null) ? this.m_ParamatorAreaBottom : base.Manager.GetElement("ParamatorAreaBottom"));
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06009529 RID: 38185 RVA: 0x00155B04 File Offset: 0x00153D04
		protected Image IconLevel
		{
			get
			{
				return this.m_IconLevel = ((this.m_IconLevel != null) ? this.m_IconLevel : base.Manager.GetElement<Image>("IconLevel"));
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x0600952A RID: 38186 RVA: 0x00155B40 File Offset: 0x00153D40
		protected TextMeshProUGUI TextLevel
		{
			get
			{
				return this.m_TextLevel = ((this.m_TextLevel != null) ? this.m_TextLevel : base.Manager.GetElement<TextMeshProUGUI>("TextLevel"));
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x0600952B RID: 38187 RVA: 0x00155B7C File Offset: 0x00153D7C
		protected Image IconRank
		{
			get
			{
				return this.m_IconRank = ((this.m_IconRank != null) ? this.m_IconRank : base.Manager.GetElement<Image>("IconRank"));
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x0600952C RID: 38188 RVA: 0x00155BB8 File Offset: 0x00153DB8
		protected TextMeshProUGUI TextRank
		{
			get
			{
				return this.m_TextRank = ((this.m_TextRank != null) ? this.m_TextRank : base.Manager.GetElement<TextMeshProUGUI>("TextRank"));
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x0600952D RID: 38189 RVA: 0x00155BF4 File Offset: 0x00153DF4
		protected Image IconPendulumScale
		{
			get
			{
				return this.m_IconPendulumScale = ((this.m_IconPendulumScale != null) ? this.m_IconPendulumScale : base.Manager.GetElement<Image>("IconPendulumScale"));
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x0600952E RID: 38190 RVA: 0x00155C30 File Offset: 0x00153E30
		protected TextMeshProUGUI TextPendulumScale
		{
			get
			{
				return this.m_TextPendulumScale = ((this.m_TextPendulumScale != null) ? this.m_TextPendulumScale : base.Manager.GetElement<TextMeshProUGUI>("TextPendulumScale"));
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x0600952F RID: 38191 RVA: 0x00155C6C File Offset: 0x00153E6C
		protected Image IconLink
		{
			get
			{
				return this.m_IconLink = ((this.m_IconLink != null) ? this.m_IconLink : base.Manager.GetElement<Image>("IconLink"));
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06009530 RID: 38192 RVA: 0x00155CA8 File Offset: 0x00153EA8
		protected TextMeshProUGUI TextLink
		{
			get
			{
				return this.m_TextLink = ((this.m_TextLink != null) ? this.m_TextLink : base.Manager.GetElement<TextMeshProUGUI>("TextLink"));
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06009531 RID: 38193 RVA: 0x00155CE4 File Offset: 0x00153EE4
		private GameObject RaceGroup
		{
			get
			{
				return this.m_RaceGroup = ((this.m_RaceGroup != null) ? this.m_RaceGroup : base.Manager.GetElement("RaceGroup"));
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06009532 RID: 38194 RVA: 0x00155D20 File Offset: 0x00153F20
		protected Image IconRace
		{
			get
			{
				return this.m_IconRace = ((this.m_IconRace != null) ? this.m_IconRace : base.Manager.GetElement<Image>("IconRace"));
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06009533 RID: 38195 RVA: 0x00155D5C File Offset: 0x00153F5C
		private GameObject TunerGroup
		{
			get
			{
				return this.m_TunerGroup = ((this.m_TunerGroup != null) ? this.m_TunerGroup : base.Manager.GetElement("TunerGroup"));
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06009534 RID: 38196 RVA: 0x00155D98 File Offset: 0x00153F98
		protected Image IconTuner
		{
			get
			{
				return this.m_IconTuner = ((this.m_IconTuner != null) ? this.m_IconTuner : base.Manager.GetElement<Image>("IconTuner"));
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06009535 RID: 38197 RVA: 0x00155DD4 File Offset: 0x00153FD4
		protected GameObject SpellTrapType
		{
			get
			{
				return this.m_SpellTrapType = ((this.m_SpellTrapType != null) ? this.m_SpellTrapType : base.Manager.GetElement("SpellTrapType"));
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06009536 RID: 38198 RVA: 0x00155E10 File Offset: 0x00154010
		protected Image IconSpellTrapType
		{
			get
			{
				return this.m_IconSpellTrapType = ((this.m_IconSpellTrapType != null) ? this.m_IconSpellTrapType : base.Manager.GetElement<Image>("IconSpellTrapType"));
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06009537 RID: 38199 RVA: 0x00155E4C File Offset: 0x0015404C
		protected TextMeshProUGUI TextSpellTrapType
		{
			get
			{
				return this.m_TextSpellTrapType = ((this.m_TextSpellTrapType != null) ? this.m_TextSpellTrapType : base.Manager.GetElement<TextMeshProUGUI>("TextSpellTrapType"));
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06009538 RID: 38200 RVA: 0x00155E88 File Offset: 0x00154088
		protected TextMeshProUGUI TextAtk
		{
			get
			{
				return this.m_TextAtk = ((this.m_TextAtk != null) ? this.m_TextAtk : base.Manager.GetElement<TextMeshProUGUI>("TextAtk"));
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06009539 RID: 38201 RVA: 0x00155EC4 File Offset: 0x001540C4
		protected Image IconDef
		{
			get
			{
				return this.m_IconDef = ((this.m_IconDef != null) ? this.m_IconDef : base.Manager.GetElement<Image>("IconDef"));
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x0600953A RID: 38202 RVA: 0x00155F00 File Offset: 0x00154100
		protected TextMeshProUGUI TextDef
		{
			get
			{
				return this.m_TextDef = ((this.m_TextDef != null) ? this.m_TextDef : base.Manager.GetElement<TextMeshProUGUI>("TextDef"));
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x0600953B RID: 38203 RVA: 0x00155F3C File Offset: 0x0015413C
		private RectTransform PoolGroup
		{
			get
			{
				return this.m_PoolGroup = ((this.m_PoolGroup != null) ? this.m_PoolGroup : base.Manager.GetElement<RectTransform>("PoolGroup"));
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x0600953C RID: 38204 RVA: 0x00155F78 File Offset: 0x00154178
		protected Image IconOCG
		{
			get
			{
				return this.m_IconOCG = ((this.m_IconOCG != null) ? this.m_IconOCG : base.Manager.GetElement<Image>("IconOCG"));
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x0600953D RID: 38205 RVA: 0x00155FB4 File Offset: 0x001541B4
		protected Image IconTCG
		{
			get
			{
				return this.m_IconTCG = ((this.m_IconTCG != null) ? this.m_IconTCG : base.Manager.GetElement<Image>("IconTCG"));
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x0600953E RID: 38206 RVA: 0x00155FF0 File Offset: 0x001541F0
		protected Image IconCCG
		{
			get
			{
				return this.m_IconCCG = ((this.m_IconCCG != null) ? this.m_IconCCG : base.Manager.GetElement<Image>("IconCCG"));
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x0600953F RID: 38207 RVA: 0x0015602C File Offset: 0x0015422C
		protected Image IconDIY
		{
			get
			{
				return this.m_IconDIY = ((this.m_IconDIY != null) ? this.m_IconDIY : base.Manager.GetElement<Image>("IconDIY"));
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06009540 RID: 38208 RVA: 0x00156068 File Offset: 0x00154268
		protected Image IconBETA
		{
			get
			{
				return this.m_IconBETA = ((this.m_IconBETA != null) ? this.m_IconBETA : base.Manager.GetNestedElement<Image>("IconBETA"));
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06009541 RID: 38209 RVA: 0x001560A4 File Offset: 0x001542A4
		protected TextMeshProUGUI TextGP
		{
			get
			{
				return this.m_TextGP = ((this.m_TextGP != null) ? this.m_TextGP : base.Manager.GetElement<TextMeshProUGUI>("TextGP"));
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06009542 RID: 38210 RVA: 0x001560E0 File Offset: 0x001542E0
		private GameObject PendulumDescriptionArea
		{
			get
			{
				return this.m_PendulumDescriptionArea = ((this.m_PendulumDescriptionArea != null) ? this.m_PendulumDescriptionArea : base.Manager.GetElement("PendulumDescriptionArea"));
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06009543 RID: 38211 RVA: 0x0015611C File Offset: 0x0015431C
		private MaterialSetter PlatePendulumDescription
		{
			get
			{
				return this.m_PlatePendulumDescription = ((this.m_PlatePendulumDescription != null) ? this.m_PlatePendulumDescription : base.Manager.GetElement<MaterialSetter>("PlatePendulumDescription"));
			}
		}

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06009544 RID: 38212 RVA: 0x00156158 File Offset: 0x00154358
		private TextMeshProUGUI TextPendulumDescriptionValue
		{
			get
			{
				return this.m_TextPendulumDescriptionValue = ((this.m_TextPendulumDescriptionValue != null) ? this.m_TextPendulumDescriptionValue : base.Manager.GetElement<TextMeshProUGUI>("TextPendulumDescriptionValue"));
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06009545 RID: 38213 RVA: 0x00156194 File Offset: 0x00154394
		private MaterialSetter PlateDescription
		{
			get
			{
				return this.m_PlateDescription = ((this.m_PlateDescription != null) ? this.m_PlateDescription : base.Manager.GetElement<MaterialSetter>("PlateDescription"));
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06009546 RID: 38214 RVA: 0x001561D0 File Offset: 0x001543D0
		private TextMeshProUGUI TextDescriptionItem
		{
			get
			{
				return this.m_TextDescriptionItem = ((this.m_TextDescriptionItem != null) ? this.m_TextDescriptionItem : base.Manager.GetElement<TextMeshProUGUI>("TextDescriptionItem"));
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06009547 RID: 38215 RVA: 0x0015620C File Offset: 0x0015440C
		private TextMeshProUGUI TextDescriptionValue
		{
			get
			{
				return this.m_TextDescriptionValue = ((this.m_TextDescriptionValue != null) ? this.m_TextDescriptionValue : base.Manager.GetElement<TextMeshProUGUI>("TextDescriptionValue"));
			}
		}

		// Token: 0x06009548 RID: 38216 RVA: 0x00156248 File Offset: 0x00154448
		public void Show(Card data)
		{
			this.cards = null;
			this.SetData(data);
			this.ButtonNext.gameObject.SetActive(false);
			this.ButtonPrev.gameObject.SetActive(false);
			UIManager.ShowFPSLeft();
			this.Show();
		}

		// Token: 0x06009549 RID: 38217 RVA: 0x00156288 File Offset: 0x00154488
		public void Show(List<int> cards, int index)
		{
			this.cards = cards;
			this.cardIndex = index;
			this.SetData(CardsManager.Get(cards[index], false));
			this.ButtonNext.gameObject.SetActive(true);
			this.ButtonPrev.gameObject.SetActive(true);
			this.SetButtons();
			UIManager.ShowFPSLeft();
			this.Show();
		}

		// Token: 0x0600954A RID: 38218 RVA: 0x001562E9 File Offset: 0x001544E9
		public override void Hide()
		{
			base.Hide();
			if (Program.instance.currentServant != Program.instance.ocgcore)
			{
				UIManager.ShowFPSRight();
			}
		}

		// Token: 0x0600954B RID: 38219 RVA: 0x00156314 File Offset: 0x00154514
		private void SetData(Card data)
		{
			this.card = data;
			this.ImageCard.SetCard(data);
			this.IconLimit.sprite = TextureManager.container.GetCardRegulationIcon(data.Id, DeckEditor.banlist);
			this.IconLimit.sprite = TextureManager.container.GetCardRegulationIcon(data.Id, DeckEditor.banlist);
			Color[] colors = CardDescription.GetCardFrameColor(data);
			Action<Material> plateLoad = delegate(Material matetial)
			{
				matetial.SetColor("_Color0", colors[0]);
				matetial.SetColor("_Color1", colors[1]);
			};
			this.PlateTitle.SetMaterialAction(plateLoad);
			this.PlateParamator.SetMaterialAction(plateLoad);
			this.PlateDescription.SetMaterialAction(plateLoad);
			this.TextCardName.text = " " + data.Name;
			this.IconAttribute.sprite = TextureManager.container.GetCardAttributeIcon(data, false);
			if (data.HasType(CardType.Monster))
			{
				this.IconLevel.gameObject.SetActive(true);
				this.IconRank.gameObject.SetActive(true);
				this.IconLink.gameObject.SetActive(true);
				this.IconPendulumScale.gameObject.SetActive(true);
				this.RaceGroup.SetActive(true);
				this.TunerGroup.SetActive(true);
				this.ParamatorAreaBottom.SetActive(true);
				Card.LevelType levelType = data.GetLevelType();
				this.IconLevel.gameObject.SetActive(data.HasType(CardType.Monster) && levelType == Card.LevelType.Level);
				this.TextLevel.text = data.Level.ToString();
				this.IconRank.gameObject.SetActive(levelType == Card.LevelType.Rank);
				this.TextRank.text = data.Level.ToString();
				this.IconLink.gameObject.SetActive(levelType == Card.LevelType.Link);
				this.TextLink.text = data.GetLinkCount().ToString();
				this.IconPendulumScale.gameObject.SetActive(data.HasType(CardType.Pendulum));
				this.TextPendulumScale.text = data.LScale.ToString();
				this.IconRace.sprite = TextureManager.container.GetCardRaceIcon(data);
				this.IconTuner.gameObject.SetActive(data.HasType(CardType.Tuner));
				this.SpellTrapType.SetActive(false);
				this.TextAtk.text = data.GetAttackString();
				this.IconDef.gameObject.SetActive(levelType != Card.LevelType.Link);
				this.TextDef.text = data.GetDefenseString();
				this.PoolGroup.SetParent(this.ParamatorAreaBottom.transform, false);
			}
			else
			{
				this.IconLevel.gameObject.SetActive(false);
				this.IconRank.gameObject.SetActive(false);
				this.IconLink.gameObject.SetActive(false);
				this.IconPendulumScale.gameObject.SetActive(false);
				this.RaceGroup.SetActive(false);
				this.TunerGroup.SetActive(false);
				this.ParamatorAreaBottom.SetActive(false);
				this.SpellTrapType.SetActive(true);
				this.IconSpellTrapType.sprite = TextureManager.container.GetCardSpellTrapTypeIcon(data);
				this.TextSpellTrapType.text = data.GetSpellTrapType(false);
				this.PoolGroup.SetParent(this.ParamatorAreaTop.transform, false);
			}
			this.IconOCG.gameObject.SetActive((data.Ot & 1) > 0);
			this.IconTCG.gameObject.SetActive((data.Ot & 2) > 0);
			this.IconCCG.gameObject.SetActive((data.Ot & 8) > 0);
			this.IconDIY.gameObject.SetActive((data.Ot & 4) > 0);
			this.IconBETA.gameObject.SetActive(data.isPre);
			int gp = OnlineService.GetGenesysPoint(data.GetOriginalID());
			string gpString = OnlineService.GetGenesysPointString(data.GetOriginalID());
			this.TextGP.text = string.Format("G:{0}", gpString);
			this.TextGP.color = OnlineService.GetGenesysPointColor(gp);
			if (data.HasType(CardType.Pendulum))
			{
				this.PendulumDescriptionArea.SetActive(true);
				this.PlatePendulumDescription.SetMaterialAction(plateLoad);
				this.TextPendulumDescriptionValue.text = data.GetPendulumDescription(false);
			}
			else
			{
				this.PendulumDescriptionArea.SetActive(false);
			}
			this.TextDescriptionItem.text = data.GetTypeForUI() + data.GetSetNameWithBracket() + data.GetIdWithBracket();
			this.TextDescriptionValue.text = data.GetMonsterDescription(false);
		}

		// Token: 0x0600954C RID: 38220 RVA: 0x001567A0 File Offset: 0x001549A0
		private void SetButtons()
		{
			this.ButtonNext.SetInteractable(this.CanNext());
			this.ButtonPrev.SetInteractable(this.CanPrev());
		}

		// Token: 0x0600954D RID: 38221 RVA: 0x001567C4 File Offset: 0x001549C4
		private bool CanNext()
		{
			return this.cards != null && this.cardIndex < this.cards.Count - 1;
		}

		// Token: 0x0600954E RID: 38222 RVA: 0x001567E8 File Offset: 0x001549E8
		private bool CanPrev()
		{
			return this.cards != null && this.cardIndex != 0;
		}

		// Token: 0x0600954F RID: 38223 RVA: 0x001567FF File Offset: 0x001549FF
		public void ShowCardExpand()
		{
			UIManager.ShowCardExpand(this.card);
		}

		// Token: 0x06009550 RID: 38224 RVA: 0x0015680C File Offset: 0x00154A0C
		public void OnNext()
		{
			if (this.shifting)
			{
				return;
			}
			if (!this.CanNext())
			{
				return;
			}
			this.shifting = true;
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			base.WindowCG.alpha = 1f;
			DOTween.Sequence().SetUpdate(true).Append(base.Window.DOAnchorPos(new Vector2(-480f, -32f), 0.1f, false).SetEase(Ease.InCubic))
				.Join(base.WindowCG.DOFade(0f, 0.1f).OnComplete(delegate
				{
					base.Window.anchoredPosition = new Vector2(480f, 0f);
					List<int> list = this.cards;
					int num = this.cardIndex + 1;
					this.cardIndex = num;
					this.SetData(CardsManager.Get(list[num], false));
					this.SetButtons();
				}))
				.Append(base.Window.DOAnchorPos(Vector2.zero, 0.2f, false).SetEase(Ease.OutQuart))
				.Join(base.WindowCG.DOFade(1f, 0.2f))
				.OnComplete(delegate
				{
					this.shifting = false;
				});
		}

		// Token: 0x06009551 RID: 38225 RVA: 0x00156900 File Offset: 0x00154B00
		public void OnPrev()
		{
			if (this.shifting)
			{
				return;
			}
			if (!this.CanPrev())
			{
				return;
			}
			this.shifting = true;
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			base.WindowCG.alpha = 1f;
			DOTween.Sequence().SetUpdate(true).Append(base.Window.DOAnchorPos(new Vector2(480f, -32f), 0.1f, false).SetEase(Ease.InCubic))
				.Join(base.WindowCG.DOFade(0f, 0.1f).OnComplete(delegate
				{
					base.Window.anchoredPosition = new Vector2(-480f, -32f);
					List<int> list = this.cards;
					int num = this.cardIndex - 1;
					this.cardIndex = num;
					this.SetData(CardsManager.Get(list[num], false));
					this.SetButtons();
				}))
				.Append(base.Window.DOAnchorPos(new Vector2(0f, -32f), 0.2f, false).SetEase(Ease.OutQuart))
				.Join(base.WindowCG.DOFade(1f, 0.2f))
				.OnComplete(delegate
				{
					this.shifting = false;
				});
		}

		// Token: 0x06009552 RID: 38226 RVA: 0x00156A00 File Offset: 0x00154C00
		protected override void Update()
		{
			if (!this.NeedResponse())
			{
				return;
			}
			if (UserInput.WasCancelPressed || UserInput.MouseRightDown)
			{
				this.Hide();
			}
			if (UserInput.WasLeftTriggerPressed)
			{
				this.ShowCardExpand();
			}
			if (UserInput.WasLeftShoulderPressed || UserInput.WasLeftPressed)
			{
				this.OnPrev();
			}
			if (UserInput.WasRightShoulderPressed || UserInput.WasRightPressed)
			{
				this.OnNext();
			}
		}

		// Token: 0x06009553 RID: 38227 RVA: 0x00156A60 File Offset: 0x00154C60
		public void OnCardPictureSave()
		{
			if (Program.instance.ocgcore.showing || (Program.instance.deckEditor.showing && DeckEditor.condition == DeckEditor.Condition.ChangeSide))
			{
				this.SaveShowingCard();
				return;
			}
			UIManager.ShowPopupSelection(new List<string>
			{
				InterString.Get("保存选项", 0),
				string.Empty,
				InterString.Get("当前卡片卡图", 0),
				InterString.Get("当前卡组卡图", 0),
				InterString.Get("所有衍生物卡图", 0),
				InterString.Get("所有卡图", 0)
			}, new Action(this.CardPictureSaveOption), null);
		}

		// Token: 0x06009554 RID: 38228 RVA: 0x00156B18 File Offset: 0x00154D18
		private void CardPictureSaveOption()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			if (selected == InterString.Get("当前卡片卡图", 0))
			{
				this.SaveShowingCard();
				return;
			}
			if (selected == InterString.Get("当前卡组卡图", 0))
			{
				this.SaveDeckCards();
				return;
			}
			if (selected == InterString.Get("所有衍生物卡图", 0))
			{
				this.SaveAllTokens();
				return;
			}
			if (selected == InterString.Get("所有卡图", 0))
			{
				this.SaveAllCards();
			}
		}

		// Token: 0x06009555 RID: 38229 RVA: 0x00156BA4 File Offset: 0x00154DA4
		private void SaveShowingCard()
		{
			RawImage rawImage = this.ImageCard.RawImage;
			Texture texture = rawImage.texture;
			if (texture == null)
			{
				texture = rawImage.material.mainTexture;
			}
			if (texture is RenderTexture)
			{
				this.SaveShowingCardAsyncIfVideo(this.card.Id);
				return;
			}
			if (this.SaveCardPicture(this.card.Id, (Texture2D)texture))
			{
				string fullPath = "Picture/CardGenerated/" + this.card.Id.ToString() + ".png";
				MessageManager.Toast(InterString.Get("卡图已保存于：[?]", fullPath, 0));
				return;
			}
			MessageManager.Toast(InterString.Get("没有写入权限，无法保存。", 0));
		}

		// Token: 0x06009556 RID: 38230 RVA: 0x00156C50 File Offset: 0x00154E50
		private async UniTask SaveShowingCardAsyncIfVideo(int code)
		{
			UniTask<bool>.Awaiter awaiter = this.SaveCardAsync(code, default(CancellationToken)).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				UniTask<bool>.Awaiter awaiter2;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<bool>.Awaiter);
			}
			if (awaiter.GetResult())
			{
				MessageManager.Toast(InterString.Get("卡图已保存于：[?]", "Picture/CardGenerated/" + this.card.Id.ToString() + ".png", 0));
			}
			else
			{
				MessageManager.Toast(InterString.Get("没有写入权限，无法保存。", 0));
			}
		}

		// Token: 0x06009557 RID: 38231 RVA: 0x00156C9B File Offset: 0x00154E9B
		private void SaveDeckCards()
		{
			this.cts = new CancellationTokenSource();
			this.SaveCardsAsync(Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.GetAllCardCodes(), this.cts.Token);
		}

		// Token: 0x06009558 RID: 38232 RVA: 0x00156CD4 File Offset: 0x00154ED4
		private void SaveAllTokens()
		{
			List<Card> allCards = CardsManager.GetAllCards();
			List<int> tokens = new List<int>();
			foreach (Card card in allCards)
			{
				if (card.HasType(CardType.Token))
				{
					tokens.Add(card.Id);
				}
			}
			this.cts = new CancellationTokenSource();
			this.SaveCardsAsync(tokens, this.cts.Token);
		}

		// Token: 0x06009559 RID: 38233 RVA: 0x00156D5C File Offset: 0x00154F5C
		private void SaveAllCards()
		{
			this.cts = new CancellationTokenSource();
			this.SaveCardsAsync(CardsManager.GetAllCardCodes(), this.cts.Token);
		}

		// Token: 0x0600955A RID: 38234 RVA: 0x00156D80 File Offset: 0x00154F80
		private bool SaveCardPicture(int code, Texture2D tex)
		{
			if (!Directory.Exists("Picture/CardGenerated/"))
			{
				Directory.CreateDirectory("Picture/CardGenerated/");
			}
			bool flag;
			try
			{
				int[] size = Settings.Data.SavedCardSize;
				if (size.Length > 1 && size[0] > 0 && size[1] > 0 && (size[0] != tex.width || size[1] != tex.height))
				{
					tex = TextureManager.ResizeTexture2D(tex, size[0], size[1]);
				}
				byte[] pic;
				string fullPath;
				if (Settings.Data.SavedCardFormat.ToLower() == ".png")
				{
					pic = tex.EncodeToPNG();
					fullPath = "Picture/CardGenerated/" + code.ToString() + ".png";
				}
				else
				{
					pic = tex.EncodeToJPG(85);
					fullPath = "Picture/CardGenerated/" + code.ToString() + ".jpg";
				}
				File.WriteAllBytes(fullPath, pic);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600955B RID: 38235 RVA: 0x00156E64 File Offset: 0x00155064
		private async UniTask SaveCardsAsync(List<int> cards, CancellationToken token)
		{
			float time = Time.time;
			GameObject pop = await Addressables.InstantiateAsync("Popup/PopupProgress.prefab", null, false, true).WithCancellation(token, false, false);
			pop.transform.SetParent(Program.instance.ui_.popup, false);
			PopupProgress popupProgress = pop.GetComponent<PopupProgress>();
			popupProgress.args = new List<string> { InterString.Get("卡图保存中", 0) };
			popupProgress.cancelAction = new Action(this.StopSaving);
			popupProgress.text.text = string.Empty;
			popupProgress.progressBar.value = 0f;
			popupProgress.Show();
			await UniTask.WaitForSeconds(popupProgress.transitionTime, false, PlayerLoopTiming.Update, token, false);
			int errorCount = 0;
			this.errorLog = string.Empty;
			string errorLogPath = "Picture/CardGenerated/MissingAndFailedCards.txt";
			if (File.Exists(errorLogPath))
			{
				File.Delete(errorLogPath);
			}
			for (int i = 0; i < cards.Count; i++)
			{
				UniTask<bool>.Awaiter awaiter = this.SaveCardAsync(cards[i], token).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					UniTask<bool>.Awaiter awaiter2;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<bool>.Awaiter);
				}
				if (!awaiter.GetResult())
				{
					errorCount++;
					this.errorLog = this.errorLog + cards[i].ToString() + "\r\n";
				}
				popupProgress.text.text = string.Concat(new string[]
				{
					i.ToString(),
					"/",
					cards.Count.ToString(),
					"\r\n",
					InterString.Get("错误：", 0),
					errorCount.ToString()
				});
				popupProgress.progressBar.value = (float)i / (float)cards.Count;
				if (cards.Count <= 100)
				{
					await UniTask.Yield(token, false);
				}
			}
			popupProgress.Hide();
			if (errorCount > 0)
			{
				File.WriteAllText(errorLogPath, this.errorLog);
			}
		}

		// Token: 0x0600955C RID: 38236 RVA: 0x00156EB8 File Offset: 0x001550B8
		private async UniTask<bool> SaveCardAsync(int code, CancellationToken token)
		{
			string format = Settings.Data.SavedCardFormat;
			if (format != ".png")
			{
				format = ".jpg";
			}
			bool flag;
			if (File.Exists("Picture/CardGenerated/" + code.ToString() + format))
			{
				flag = true;
			}
			else
			{
				Texture tex = await CardImageLoader.LoadCardAsync(code, false, token, true);
				if (!this.SaveCardPicture(code, (Texture2D)tex) || !CardImageLoader.lastCardFoundArt || !CardImageLoader.lastCardRenderSucceed)
				{
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600955D RID: 38237 RVA: 0x00156F0C File Offset: 0x0015510C
		private void StopSaving()
		{
			CancellationTokenSource cancellationTokenSource = this.cts;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			CancellationTokenSource cancellationTokenSource2 = this.cts;
			if (cancellationTokenSource2 != null)
			{
				cancellationTokenSource2.Dispose();
			}
			if (!string.IsNullOrEmpty(this.errorLog))
			{
				File.WriteAllText("Picture/CardGenerated/MissingAndFailedCards.txt", this.errorLog);
			}
		}

		// Token: 0x0400D34E RID: 54094
		private const string LABEL_SBN_NEXT = "NextButton";

		// Token: 0x0400D34F RID: 54095
		private SelectionButton m_ButtonNext;

		// Token: 0x0400D350 RID: 54096
		private const string LABEL_SBN_PREV = "PrevButton";

		// Token: 0x0400D351 RID: 54097
		private SelectionButton m_ButtonPrev;

		// Token: 0x0400D352 RID: 54098
		private const string LABEL_MONO_IMAGECARD = "ImageCard";

		// Token: 0x0400D353 RID: 54099
		private CardRawImageHandler m_ImageCard;

		// Token: 0x0400D354 RID: 54100
		private const string LABEL_IMG_LIMIT = "IconLimit";

		// Token: 0x0400D355 RID: 54101
		private Image m_IconLimit;

		// Token: 0x0400D356 RID: 54102
		private const string LABEL_MS_PLATE_TITLE = "PlateTitle";

		// Token: 0x0400D357 RID: 54103
		private MaterialSetter m_PlateTitle;

		// Token: 0x0400D358 RID: 54104
		private const string LABEL_TXT_CARDNAME = "TextCardName";

		// Token: 0x0400D359 RID: 54105
		private TextMeshProUGUI m_TextCardName;

		// Token: 0x0400D35A RID: 54106
		private const string LABEL_IMG_ATTRIBUTE = "IconAttribute";

		// Token: 0x0400D35B RID: 54107
		private Image m_IconAttribute;

		// Token: 0x0400D35C RID: 54108
		private const string LABEL_MS_PLATE_PARAMATOR = "PlateParamator";

		// Token: 0x0400D35D RID: 54109
		private MaterialSetter m_PlateParamator;

		// Token: 0x0400D35E RID: 54110
		private const string LABEL_GO_PARAMATOR_AREA_TOP = "ParamatorAreaTop";

		// Token: 0x0400D35F RID: 54111
		private GameObject m_ParamatorAreaTop;

		// Token: 0x0400D360 RID: 54112
		private const string LABEL_GO_PARAMATOR_AREA_BOTTOM = "ParamatorAreaBottom";

		// Token: 0x0400D361 RID: 54113
		private GameObject m_ParamatorAreaBottom;

		// Token: 0x0400D362 RID: 54114
		private const string LABEL_IMG_LEVEL = "IconLevel";

		// Token: 0x0400D363 RID: 54115
		private Image m_IconLevel;

		// Token: 0x0400D364 RID: 54116
		private const string LABEL_TXT_LEVEL = "TextLevel";

		// Token: 0x0400D365 RID: 54117
		private TextMeshProUGUI m_TextLevel;

		// Token: 0x0400D366 RID: 54118
		private const string LABEL_IMG_RANK = "IconRank";

		// Token: 0x0400D367 RID: 54119
		private Image m_IconRank;

		// Token: 0x0400D368 RID: 54120
		private const string LABEL_TXT_RANK = "TextRank";

		// Token: 0x0400D369 RID: 54121
		private TextMeshProUGUI m_TextRank;

		// Token: 0x0400D36A RID: 54122
		private const string LABEL_IMG_PENDULUMSCALE = "IconPendulumScale";

		// Token: 0x0400D36B RID: 54123
		private Image m_IconPendulumScale;

		// Token: 0x0400D36C RID: 54124
		private const string LABEL_TXT_PENDULUMSCALE = "TextPendulumScale";

		// Token: 0x0400D36D RID: 54125
		private TextMeshProUGUI m_TextPendulumScale;

		// Token: 0x0400D36E RID: 54126
		private const string LABEL_IMG_LINK = "IconLink";

		// Token: 0x0400D36F RID: 54127
		private Image m_IconLink;

		// Token: 0x0400D370 RID: 54128
		private const string LABEL_TXT_LINK = "TextLink";

		// Token: 0x0400D371 RID: 54129
		private TextMeshProUGUI m_TextLink;

		// Token: 0x0400D372 RID: 54130
		private const string LABEL_GO_RACE_GROUP = "RaceGroup";

		// Token: 0x0400D373 RID: 54131
		private GameObject m_RaceGroup;

		// Token: 0x0400D374 RID: 54132
		private const string LABEL_IMG_RACE = "IconRace";

		// Token: 0x0400D375 RID: 54133
		private Image m_IconRace;

		// Token: 0x0400D376 RID: 54134
		private const string LABEL_GO_TUNER_GROUP = "TunerGroup";

		// Token: 0x0400D377 RID: 54135
		private GameObject m_TunerGroup;

		// Token: 0x0400D378 RID: 54136
		private const string LABEL_IMG_TUNER = "IconTuner";

		// Token: 0x0400D379 RID: 54137
		private Image m_IconTuner;

		// Token: 0x0400D37A RID: 54138
		private const string LABEL_GO_SPELLTRAPTYPE = "SpellTrapType";

		// Token: 0x0400D37B RID: 54139
		private GameObject m_SpellTrapType;

		// Token: 0x0400D37C RID: 54140
		private const string LABEL_IMG_SPELLTRAPTYPE = "IconSpellTrapType";

		// Token: 0x0400D37D RID: 54141
		private Image m_IconSpellTrapType;

		// Token: 0x0400D37E RID: 54142
		private const string LABEL_TXT_SPELLTRAPTYPE = "TextSpellTrapType";

		// Token: 0x0400D37F RID: 54143
		private TextMeshProUGUI m_TextSpellTrapType;

		// Token: 0x0400D380 RID: 54144
		private const string LABEL_TXT_ATK = "TextAtk";

		// Token: 0x0400D381 RID: 54145
		private TextMeshProUGUI m_TextAtk;

		// Token: 0x0400D382 RID: 54146
		private const string LABEL_IMG_DEF = "IconDef";

		// Token: 0x0400D383 RID: 54147
		private Image m_IconDef;

		// Token: 0x0400D384 RID: 54148
		private const string LABEL_TXT_DEF = "TextDef";

		// Token: 0x0400D385 RID: 54149
		private TextMeshProUGUI m_TextDef;

		// Token: 0x0400D386 RID: 54150
		private const string LABEL_RT_POOL_GROUP = "PoolGroup";

		// Token: 0x0400D387 RID: 54151
		private RectTransform m_PoolGroup;

		// Token: 0x0400D388 RID: 54152
		private const string LABEL_IMG_ICON_OCG = "IconOCG";

		// Token: 0x0400D389 RID: 54153
		private Image m_IconOCG;

		// Token: 0x0400D38A RID: 54154
		private const string LABEL_IMG_ICON_TCG = "IconTCG";

		// Token: 0x0400D38B RID: 54155
		private Image m_IconTCG;

		// Token: 0x0400D38C RID: 54156
		private const string LABEL_IMG_ICON_CCG = "IconCCG";

		// Token: 0x0400D38D RID: 54157
		private Image m_IconCCG;

		// Token: 0x0400D38E RID: 54158
		private const string LABEL_IMG_ICON_DIY = "IconDIY";

		// Token: 0x0400D38F RID: 54159
		private Image m_IconDIY;

		// Token: 0x0400D390 RID: 54160
		private const string LABEL_IMG_ICON_BETA = "IconBETA";

		// Token: 0x0400D391 RID: 54161
		private Image m_IconBETA;

		// Token: 0x0400D392 RID: 54162
		private const string LABEL_TXT_GP = "TextGP";

		// Token: 0x0400D393 RID: 54163
		private TextMeshProUGUI m_TextGP;

		// Token: 0x0400D394 RID: 54164
		private const string LABEL_GO_PENDULUM_DESCRIPTION_AREA = "PendulumDescriptionArea";

		// Token: 0x0400D395 RID: 54165
		private GameObject m_PendulumDescriptionArea;

		// Token: 0x0400D396 RID: 54166
		private const string LABEL_MS_PLATE_PENDULUM_DESCRIPTION = "PlatePendulumDescription";

		// Token: 0x0400D397 RID: 54167
		private MaterialSetter m_PlatePendulumDescription;

		// Token: 0x0400D398 RID: 54168
		private const string LABEL_TXT_PENDULUM_DESCRIPTION_VALUE = "TextPendulumDescriptionValue";

		// Token: 0x0400D399 RID: 54169
		private TextMeshProUGUI m_TextPendulumDescriptionValue;

		// Token: 0x0400D39A RID: 54170
		private const string LABEL_MS_PLATE_DESCRIPTION = "PlateDescription";

		// Token: 0x0400D39B RID: 54171
		private MaterialSetter m_PlateDescription;

		// Token: 0x0400D39C RID: 54172
		private const string LABEL_TXT_DESCRIPTION_ITEM = "TextDescriptionItem";

		// Token: 0x0400D39D RID: 54173
		private TextMeshProUGUI m_TextDescriptionItem;

		// Token: 0x0400D39E RID: 54174
		private const string LABEL_TXT_DESCRIPTION_VALUE = "TextDescriptionValue";

		// Token: 0x0400D39F RID: 54175
		private TextMeshProUGUI m_TextDescriptionValue;

		// Token: 0x0400D3A0 RID: 54176
		private Card card;

		// Token: 0x0400D3A1 RID: 54177
		private List<int> cards;

		// Token: 0x0400D3A2 RID: 54178
		private int cardIndex;

		// Token: 0x0400D3A3 RID: 54179
		private bool shifting;

		// Token: 0x0400D3A4 RID: 54180
		private string errorLog;

		// Token: 0x0400D3A5 RID: 54181
		private CancellationTokenSource cts;
	}
}
