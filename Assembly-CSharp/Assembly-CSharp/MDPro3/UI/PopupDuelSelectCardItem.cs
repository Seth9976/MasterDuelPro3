using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001393 RID: 5011
	public class PopupDuelSelectCardItem : MonoBehaviour
	{
		// Token: 0x060090E4 RID: 37092 RVA: 0x0013F6A0 File Offset: 0x0013D8A0
		private void Start()
		{
			this.RefreshCard(this.card.GetData().Id);
			this.card.forSelect = true;
			if ((this.card.p.location & 2048U) > 0U)
			{
				base.GetComponent<Image>().color = Color.black;
				this.head.color = Color.black;
			}
			else if (this.card.p.controller != 0U)
			{
				base.GetComponent<Image>().color = PopupDuelSelectCardItem.opColor;
				this.head.color = PopupDuelSelectCardItem.opColor;
			}
			bool showHead = false;
			if (this.id == 0)
			{
				showHead = true;
			}
			else if (this.card.p.location != this.cards[this.id - 1].p.location || this.card.p.controller != this.cards[this.id - 1].p.controller)
			{
				showHead = true;
			}
			if (showHead)
			{
				this.locationIcon.sprite = TextureManager.GetCardLocationIcon(this.card.p);
			}
			else
			{
				this.head.gameObject.SetActive(false);
			}
			bool isEnd = false;
			if (this.id == this.cards.Count - 1)
			{
				isEnd = true;
			}
			else if (this.card.p.location != this.cards[this.id + 1].p.location || this.card.p.controller != this.cards[this.id + 1].p.controller)
			{
				isEnd = true;
			}
			if (isEnd)
			{
				base.GetComponent<RectTransform>().sizeDelta = new Vector2(145f, 180f);
			}
			else
			{
				base.GetComponent<RectTransform>().sizeDelta = new Vector2(180f, 180f);
			}
			if (((long)this.card.p.position & 5L) > 0L)
			{
				this.cardBack.SetActive(false);
			}
			if (this.card.chains.Count > 0)
			{
				this.chain.SetActive(true);
				this.chainText.text = this.card.chains[0].i.ToString();
			}
			else
			{
				this.chain.SetActive(false);
				if (OcgCore.cardsBeTarget.Contains(this.card))
				{
					this.target.SetActive(true);
				}
				else
				{
					this.target.SetActive(false);
				}
			}
			Card origin = CardsManager.Get(this.card.GetData().Id, false);
			if (origin.HasType(CardType.Monster))
			{
				this.levelIcon.sprite = TextureManager.GetCardLevelIcon(this.card.GetData());
				if (this.card.GetData().HasType(CardType.Link))
				{
					this.textLevel.text = this.card.GetData().GetLinkCount().ToString();
				}
				else
				{
					this.textLevel.text = this.card.GetData().Level.ToString();
				}
				if (origin.HasType(CardType.Tuner))
				{
					this.tunerIcon.gameObject.SetActive(true);
				}
			}
			else
			{
				this.levelIcon.sprite = TextureManager.container.typeNone;
				this.textLevel.text = string.Empty;
			}
			if (origin.HasType(CardType.Pendulum))
			{
				this.pendulumIcon.gameObject.SetActive(true);
				this.textPendulum.text = this.card.GetData().LScale.ToString();
			}
			else
			{
				this.pendulumIcon.gameObject.SetActive(false);
				this.textPendulum.text = string.Empty;
			}
			this.button.onClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x060090E5 RID: 37093 RVA: 0x0013FA90 File Offset: 0x0013DC90
		private async UniTask RefreshCard(int code)
		{
			this.cardFace.texture = TextureManager.container.unknownCard.texture;
			this.cardFace.material = MaterialLoader.GetCardMaterial(code, false);
			Material material = this.cardFace.material;
			Texture texture = await CardImageLoader.LoadCardAsync(code, false, base.destroyCancellationToken, false);
			material.mainTexture = texture;
			material = null;
			this.cardFace.texture = this.cardFace.material.mainTexture;
		}

		// Token: 0x060090E6 RID: 37094 RVA: 0x0013FADC File Offset: 0x0013DCDC
		private void OnClick()
		{
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			if ((this.card.p.location & 12U) > 0U && (this.card.p.location & 128U) == 0U)
			{
				if (this.manager.arrow == null)
				{
					this.manager.arrow = ABLoader.LoadMasterDuelGameObject("fxp_arrow_aim_001");
					Program.instance.ocgcore.allGameObjects.Add(this.manager.arrow);
				}
				this.manager.arrow.transform.position = this.card.model.transform.position;
			}
			else if (this.manager.arrow != null)
			{
				this.manager.arrow.SetActive(false);
			}
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Show(this.card, this.cardFace.material, -1, null);
			if (this.selected)
			{
				if (!this.unselectable)
				{
					if (Time.time - this.clickTime >= PopupDuelSelectCardItem.doubleClickTime * Time.timeScale)
					{
						this.UnselectThis();
						return;
					}
					if (this.manager.SelectedCount == 1 && this.manager.min == 1 && this.manager.max == 1)
					{
						this.manager.OnConfirm();
						return;
					}
					this.UnselectThis();
					return;
				}
			}
			else
			{
				if (!this.unselectable)
				{
					this.SelectThis();
					this.clickTime = Time.time;
					return;
				}
				if (this.manager.max == 1 && this.manager.min == 1 && OcgCore.currentMessage != GameMessage.SelectSum)
				{
					foreach (PopupDuelSelectCardItem popupDuelSelectCardItem in this.manager.monos)
					{
						popupDuelSelectCardItem.UnselectThis();
					}
					this.SelectThis();
					this.clickTime = Time.time;
				}
			}
		}

		// Token: 0x060090E7 RID: 37095 RVA: 0x0013FCF4 File Offset: 0x0013DEF4
		private void SelectThis()
		{
			if (this.selected)
			{
				return;
			}
			this.selected = true;
			PopupDuelSelectCard popupDuelSelectCard = this.manager;
			int selectedCount = popupDuelSelectCard.SelectedCount;
			popupDuelSelectCard.SelectedCount = selectedCount + 1;
			if (OcgCore.currentMessage != GameMessage.ConfirmCards)
			{
				if (!this.manager.order)
				{
					this.checkOn.SetActive(true);
					return;
				}
				this.orderBase.SetActive(true);
				this.orderText.text = this.manager.SelectedCount.ToString();
			}
		}

		// Token: 0x060090E8 RID: 37096 RVA: 0x0013FD74 File Offset: 0x0013DF74
		public void RemoveOrder(int i)
		{
			if (!this.selected)
			{
				return;
			}
			int order = int.Parse(this.orderText.text);
			if (order > i)
			{
				this.orderText.text = (order - 1).ToString();
			}
		}

		// Token: 0x060090E9 RID: 37097 RVA: 0x0013FDB5 File Offset: 0x0013DFB5
		public int GetOrder()
		{
			return int.Parse(this.orderText.text);
		}

		// Token: 0x060090EA RID: 37098 RVA: 0x0013FDC8 File Offset: 0x0013DFC8
		private void UnselectThis()
		{
			if (!this.selected || this.unselectable)
			{
				return;
			}
			this.selected = false;
			PopupDuelSelectCard popupDuelSelectCard = this.manager;
			int selectedCount = popupDuelSelectCard.SelectedCount;
			popupDuelSelectCard.SelectedCount = selectedCount - 1;
			if (!this.manager.order)
			{
				this.checkOn.SetActive(false);
				return;
			}
			this.orderBase.SetActive(false);
			this.manager.RemoveOrder(this.GetOrder());
		}

		// Token: 0x060090EB RID: 37099 RVA: 0x0013FE3C File Offset: 0x0013E03C
		public void UnselectableThis()
		{
			this.unselectable = true;
			this.cardFace.color = PopupDuelSelectCardItem.unselectableColor;
			this.cardBack.GetComponent<Image>().color = PopupDuelSelectCardItem.unselectableColor;
			this.levelIcon.color = PopupDuelSelectCardItem.unselectableColor;
			this.pendulumIcon.color = PopupDuelSelectCardItem.unselectableColor;
			this.textLevel.color = PopupDuelSelectCardItem.unselectableColor;
			this.textPendulum.color = PopupDuelSelectCardItem.unselectableColor;
			this.tunerIcon.color = PopupDuelSelectCardItem.unselectableColor;
		}

		// Token: 0x060090EC RID: 37100 RVA: 0x0013FEC8 File Offset: 0x0013E0C8
		public void SelectableThis()
		{
			if (this.preselected)
			{
				return;
			}
			this.unselectable = false;
			this.cardFace.color = Color.white;
			this.cardBack.GetComponent<Image>().color = Color.white;
			this.levelIcon.color = Color.white;
			this.pendulumIcon.color = Color.white;
			this.textLevel.color = Color.white;
			this.textPendulum.color = Color.white;
			this.tunerIcon.color = Color.white;
		}

		// Token: 0x060090ED RID: 37101 RVA: 0x0013FF5A File Offset: 0x0013E15A
		public void PreSelectThis()
		{
			this.preselected = true;
			this.SelectThis();
			this.UnselectableThis();
		}

		// Token: 0x0400CF9E RID: 53150
		public Image head;

		// Token: 0x0400CF9F RID: 53151
		public Image locationIcon;

		// Token: 0x0400CFA0 RID: 53152
		public RawImage cardFace;

		// Token: 0x0400CFA1 RID: 53153
		public GameObject cardBack;

		// Token: 0x0400CFA2 RID: 53154
		public Button button;

		// Token: 0x0400CFA3 RID: 53155
		public Image levelIcon;

		// Token: 0x0400CFA4 RID: 53156
		public TextMeshProUGUI textLevel;

		// Token: 0x0400CFA5 RID: 53157
		public Image pendulumIcon;

		// Token: 0x0400CFA6 RID: 53158
		public TextMeshProUGUI textPendulum;

		// Token: 0x0400CFA7 RID: 53159
		public Image tunerIcon;

		// Token: 0x0400CFA8 RID: 53160
		public GameObject checkOn;

		// Token: 0x0400CFA9 RID: 53161
		public GameObject orderBase;

		// Token: 0x0400CFAA RID: 53162
		public Text orderText;

		// Token: 0x0400CFAB RID: 53163
		public GameObject chain;

		// Token: 0x0400CFAC RID: 53164
		public Text chainText;

		// Token: 0x0400CFAD RID: 53165
		public GameObject target;

		// Token: 0x0400CFAE RID: 53166
		public int id;

		// Token: 0x0400CFAF RID: 53167
		public List<GameCard> cards;

		// Token: 0x0400CFB0 RID: 53168
		public PopupDuelSelectCard manager;

		// Token: 0x0400CFB1 RID: 53169
		public GameCard card;

		// Token: 0x0400CFB2 RID: 53170
		private static Color opColor = new Color(0.9f, 0f, 0f, 1f);

		// Token: 0x0400CFB3 RID: 53171
		public bool selected;

		// Token: 0x0400CFB4 RID: 53172
		public bool unselectable;

		// Token: 0x0400CFB5 RID: 53173
		private static Color unselectableColor = new Color(0.5f, 0.5f, 0.5f, 1f);

		// Token: 0x0400CFB6 RID: 53174
		public bool preselected;

		// Token: 0x0400CFB7 RID: 53175
		private static readonly float doubleClickTime = 0.2f;

		// Token: 0x0400CFB8 RID: 53176
		private float clickTime;
	}
}
