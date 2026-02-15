using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013EE RID: 5102
	public class PlaceSelector : MonoBehaviour
	{
		// Token: 0x060093E3 RID: 37859 RVA: 0x0014F750 File Offset: 0x0014D950
		private void Start()
		{
			BoxCollider collider = base.gameObject.AddComponent<BoxCollider>();
			if (this.p.InLocation(CardLocation.Deck))
			{
				this.highlight = ABLoader.LoadMasterDuelGameObject("eff_duel_highlight10");
				base.transform.localEulerAngles = new Vector3(0f, -19.5f, 0f);
				collider.size = new Vector3(8f, 1f, 10f);
			}
			else if (this.p.InLocation(CardLocation.Extra))
			{
				this.highlight = ABLoader.LoadMasterDuelGameObject("eff_duel_highlight10");
				base.transform.localEulerAngles = new Vector3(0f, 19.5f, 0f);
				collider.size = new Vector3(8f, 1f, 10f);
			}
			else if (this.p.InLocation(CardLocation.MonsterZone))
			{
				this.highlight = ABLoader.LoadMasterDuelGameObject("eff_duel_highlight11");
				collider.size = new Vector3(8f, 1f, 8f);
				this.select = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_mst_001");
				this.selectPush = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_mst_Push_001");
				this.selectCard = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_001");
				this.selectCardPush = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_Push_001");
				this.disable = new GameObject("Disable");
				this.CreateSelectButton();
			}
			else if (this.p.InLocation(CardLocation.SpellZone))
			{
				if (this.p.sequence == 5U)
				{
					this.highlight = ABLoader.LoadMasterDuelGameObject("eff_duel_highlight13");
					collider.size = new Vector3(6f, 1f, 7f);
					this.select = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_001");
					this.selectPush = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_Push_001");
					this.selectCard = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_001");
					this.selectCardPush = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_Push_001");
					this.select.transform.localScale = Vector3.one * 0.8f;
					this.selectPush.transform.localScale = Vector3.one * 0.8f;
				}
				else
				{
					this.highlight = ABLoader.LoadMasterDuelGameObject("eff_duel_highlight12");
					collider.size = new Vector3(8f, 1f, 7f);
					this.select = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_trpmgc_001");
					this.selectPush = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_trpmgc_Push_001");
					this.selectCard = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_001");
					this.selectCardPush = ABLoader.LoadMasterDuelGameObject("fxp_HL_Select_card_Push_001");
				}
				this.selectCard.transform.localScale = Vector3.one * 0.8f;
				this.selectCardPush.transform.localScale = Vector3.one * 0.8f;
				this.disable = new GameObject("Disable");
				this.CreateSelectButton();
			}
			this.highlight.transform.SetParent(base.transform, false);
			base.transform.localPosition = GameCard.GetCardPosition(this.p, null, null);
			if (this.select != null)
			{
				this.select.transform.SetParent(base.transform, false);
				this.selectPush.transform.SetParent(base.transform, false);
				this.selectCard.transform.SetParent(base.transform, false);
				this.selectCardPush.transform.SetParent(base.transform, false);
				this.select.GetComponent<ParticleSystem>().main.playOnAwake = true;
				this.selectPush.GetComponent<ParticleSystem>().main.playOnAwake = true;
				this.selectCard.GetComponent<ParticleSystem>().main.playOnAwake = true;
				this.selectCardPush.GetComponent<ParticleSystem>().main.playOnAwake = true;
				this.select.SetActive(false);
				this.selectPush.SetActive(false);
				this.selectCard.SetActive(false);
				this.selectCardPush.SetActive(false);
			}
			if (this.disable != null)
			{
				this.disable.transform.SetParent(base.transform, false);
				this.disable.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
				this.disable.transform.localScale = new Vector3(3f, 3f, 1f);
				this.disable.AddComponent<SpriteRenderer>().sprite = TextureManager.container.CardAffectDisable;
				this.disable.SetActive(false);
			}
		}

		// Token: 0x060093E4 RID: 37860 RVA: 0x0014FC08 File Offset: 0x0014DE08
		private void Update()
		{
			this.hover = false;
			if (UserInput.HoverObject == base.gameObject)
			{
				this.hover = true;
			}
			if (this.hover)
			{
				this.highlight.SetActive(true);
				if (UserInput.MouseLeftUp)
				{
					this.OnClick();
				}
				if ((this.p.location & 12U) == 0U && !this.countShowing)
				{
					this.countShowing = true;
					Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowLocationCount(this.p);
				}
			}
			else
			{
				if (UserInput.MouseLeftUp)
				{
					this.HideButtons();
				}
				this.highlight.SetActive(false);
				if (this.countShowing)
				{
					this.countShowing = false;
					Program.instance.ocgcore.GetUI<OcgCoreUI>().HidePlaceCount();
				}
			}
			if (UserInput.HoverObject == base.gameObject)
			{
				this.highlight.SetActive(true);
				return;
			}
			this.highlight.SetActive(false);
		}

		// Token: 0x060093E5 RID: 37861 RVA: 0x0014FCF8 File Offset: 0x0014DEF8
		private void OnClick()
		{
			if (this.selecting)
			{
				AudioManager.PlaySE("SE_DUEL_SELECT", 1f);
				if (!this.selected)
				{
					this.selected = true;
					this.select.SetActive(false);
					this.selectPush.SetActive(false);
					this.selectPush.SetActive(true);
				}
				else
				{
					this.selected = false;
					this.select.SetActive(true);
				}
				int selectedCount = 0;
				using (List<PlaceSelector>.Enumerator enumerator = Program.instance.ocgcore.places.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.selected)
						{
							selectedCount++;
						}
					}
				}
				if (selectedCount == OcgCore.ES_min)
				{
					BinaryMaster binaryMaster = new BinaryMaster(null);
					foreach (PlaceSelector place in Program.instance.ocgcore.places)
					{
						if (place.selected)
						{
							byte[] response = new byte[]
							{
								OcgCore.isFirst ? ((byte)place.p.controller) : ((byte)(1U - place.p.controller)),
								(byte)place.p.location,
								(byte)place.p.sequence
							};
							binaryMaster.writer.Write(response);
						}
					}
					Program.instance.ocgcore.SendReturn(binaryMaster.Get(), 0f);
					return;
				}
			}
			else if (this.cardSelecting)
			{
				AudioManager.PlaySE("SE_DUEL_SELECT", 1f);
				if (OcgCore.currentMessage == GameMessage.SelectCounter)
				{
					if (!this.cardUnselectable)
					{
						this.selectButton.Show();
					}
				}
				else if (!this.cardSelected && !this.cardUnselectable && !this.cardPreselected)
				{
					this.selectButton.Show();
				}
				else if (this.cardSelected && !this.cardUnselectable && !this.cardPreselected)
				{
					this.UnselectCardInThisZone();
				}
				GameCard card = this.FindCardInThisPlace();
				if (card != null)
				{
					card.OnClick();
					return;
				}
			}
			else
			{
				if (this.p.InLocation(CardLocation.SpellZone))
				{
					if (this.p.InMyControl())
					{
						OcgCore.HideMyHandCard = true;
						OcgCore.HideOpHandCard = false;
					}
					else
					{
						OcgCore.HideOpHandCard = true;
						OcgCore.HideMyHandCard = false;
					}
				}
				else
				{
					OcgCore.HideMyHandCard = false;
					OcgCore.HideOpHandCard = false;
				}
				if (this.p.InLocation(CardLocation.Onfield))
				{
					GameCard card2 = this.FindCardInThisPlace();
					if (card2 != null)
					{
						card2.OnClick();
					}
					else
					{
						Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Hide();
						Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Hide();
					}
					using (List<GameCard>.Enumerator enumerator2 = OcgCore.cards.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GameCard c = enumerator2.Current;
							if (c != card2)
							{
								c.NotClickThis();
							}
						}
						return;
					}
				}
				AudioManager.PlaySE("SE_DUEL_SELECT", 1f);
				List<GameCard> cards = new List<GameCard>();
				foreach (GameCard card3 in OcgCore.cards)
				{
					if ((card3.p.location & this.p.location) > 0U && card3.p.controller == this.p.controller)
					{
						cards.Add(card3);
					}
				}
				Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Show(cards, (CardLocation)this.p.location, (int)this.p.controller);
				if (!this.buttonsCreated)
				{
					bool spsummmon = false;
					bool activate = false;
					foreach (GameCard card4 in OcgCore.cards)
					{
						if ((card4.p.location & this.p.location) > 0U && card4.p.controller == this.p.controller)
						{
							foreach (GameCard.DuelButtonInfo duelButtonInfo in card4.buttons)
							{
								if (duelButtonInfo.type == ButtonType.Activate)
								{
									activate = true;
								}
								if (duelButtonInfo.type == ButtonType.SpSummon)
								{
									spsummmon = true;
								}
							}
						}
					}
					if (activate)
					{
						int response2 = -1;
						this.buttons.Add(new GameCard.DuelButtonInfo
						{
							response = new List<int> { response2 },
							hint = InterString.Get("发动效果", 0),
							type = ButtonType.Activate
						});
					}
					if (spsummmon)
					{
						int response3 = -2;
						this.buttons.Add(new GameCard.DuelButtonInfo
						{
							response = new List<int> { response3 },
							hint = InterString.Get("特殊召唤", 0),
							type = ButtonType.SpSummon
						});
					}
					this.CreateButtons();
					return;
				}
				if (Program.instance.ocgcore.returnAction == null)
				{
					this.ShowButtons();
				}
			}
		}

		// Token: 0x060093E6 RID: 37862 RVA: 0x00150268 File Offset: 0x0014E468
		private void CreateButtons()
		{
			if (this.buttonsCreated || Program.instance.ocgcore.returnAction != null || this.buttons.Count == 0)
			{
				this.buttons.Clear();
				return;
			}
			for (int i = 0; i < this.buttons.Count; i++)
			{
				DuelButton mono = ABLoader.LoadMasterDuelGameObject("DuelButton").GetComponent<DuelButton>();
				this.buttonObjs.Add(mono);
				mono.response = this.buttons[i].response;
				mono.hint = this.buttons[i].hint;
				mono.type = this.buttons[i].type;
				mono.id = i;
				mono.buttonsCount = this.buttons.Count;
				mono.cookieCard = null;
				mono.location = this.p.location;
				mono.controller = this.p.controller;
				mono.Show();
			}
			this.buttonsCreated = true;
		}

		// Token: 0x060093E7 RID: 37863 RVA: 0x00150374 File Offset: 0x0014E574
		private void CreateSelectButton()
		{
			GameObject obj = ABLoader.LoadMasterDuelGameObject("DuelButton");
			this.selectButton = obj.GetComponent<DuelButton>();
			this.selectButton.response.Add(-3);
			this.selectButton.hint = "";
			this.selectButton.type = ButtonType.Select;
			this.selectButton.id = 0;
			this.selectButton.buttonsCount = 1;
			this.selectButton.cookieCard = null;
			this.selectButton.location = this.p.location;
			this.selectButton.controller = this.p.controller;
			this.selectButton.sequence = this.p.sequence;
			this.selectButton.Hide();
		}

		// Token: 0x060093E8 RID: 37864 RVA: 0x00150438 File Offset: 0x0014E638
		public void ShowButtons()
		{
			foreach (DuelButton duelButton in this.buttonObjs)
			{
				duelButton.Show();
			}
		}

		// Token: 0x060093E9 RID: 37865 RVA: 0x00150488 File Offset: 0x0014E688
		public void HideButtons()
		{
			foreach (DuelButton duelButton in this.buttonObjs)
			{
				duelButton.Hide();
			}
			if (this.selectButton != null)
			{
				this.selectButton.Hide();
			}
		}

		// Token: 0x060093EA RID: 37866 RVA: 0x001504F4 File Offset: 0x0014E6F4
		public void ClearButtons()
		{
			foreach (DuelButton duelButton in this.buttonObjs)
			{
				global::UnityEngine.Object.Destroy(duelButton.gameObject);
			}
			this.buttonObjs.Clear();
			this.buttons.Clear();
			this.buttonsCreated = false;
		}

		// Token: 0x060093EB RID: 37867 RVA: 0x00150568 File Offset: 0x0014E768
		public void StopResponse()
		{
			if (this.selecting)
			{
				this.selecting = false;
				this.select.SetActive(false);
				if (this.selected)
				{
					this.selectPush.SetActive(false);
					this.selectPush.SetActive(true);
					this.selected = false;
				}
			}
			if (this.cardSelecting)
			{
				this.cardSelecting = false;
				this.cardSelected = false;
				this.selectCard.SetActive(false);
				this.cookieCard = null;
				this.cardUnselectable = false;
				this.cardPreselected = false;
			}
		}

		// Token: 0x060093EC RID: 37868 RVA: 0x001505F0 File Offset: 0x0014E7F0
		public void InitializeSelectCardInThisZone(List<GameCard> cards)
		{
			foreach (GameCard card in cards)
			{
				if (card.p.controller == this.p.controller)
				{
					if (card.p.location == this.p.location && card.p.sequence == this.p.sequence)
					{
						this.cardSelecting = true;
						this.cookieCard = card;
						this.ShowSelectCardHighlight();
						break;
					}
				}
				else
				{
					if ((this.p.location & 4U) > 0U && this.p.sequence == 5U && card.p.controller == 1U && (card.p.location & 4U) > 0U && card.p.sequence == 6U)
					{
						this.cardSelecting = true;
						this.cookieCard = card;
						this.ShowSelectCardHighlight();
						break;
					}
					if ((this.p.location & 4U) > 0U && this.p.sequence == 6U && card.p.controller == 1U && (card.p.location & 4U) > 0U && card.p.sequence == 5U)
					{
						this.cardSelecting = true;
						this.cookieCard = card;
						this.ShowSelectCardHighlight();
						break;
					}
				}
			}
			if (this.cardSelecting && OcgCore.currentMessage == GameMessage.SelectSum && OcgCore.cardsMustBeSelected.Contains(this.cookieCard))
			{
				this.cardPreselected = true;
				this.cardSelected = true;
				this.selectCard.SetActive(false);
			}
		}

		// Token: 0x060093ED RID: 37869 RVA: 0x001507B0 File Offset: 0x0014E9B0
		public void SelectCardInThisZone()
		{
			this.cardSelected = true;
			if (OcgCore.currentMessage != GameMessage.SelectCounter)
			{
				this.selectCard.SetActive(false);
			}
			this.selectCardPush.SetActive(false);
			this.selectCardPush.SetActive(true);
			Program.instance.ocgcore.FieldSelectRefresh(this.cookieCard);
		}

		// Token: 0x060093EE RID: 37870 RVA: 0x00150806 File Offset: 0x0014EA06
		public void UnselectCardInThisZone()
		{
			if (OcgCore.currentMessage == GameMessage.SelectCounter)
			{
				return;
			}
			this.cardSelected = false;
			Program.instance.ocgcore.FieldSelectRefresh(this.cookieCard);
		}

		// Token: 0x060093EF RID: 37871 RVA: 0x0015082E File Offset: 0x0014EA2E
		public void CardInThisZoneSelectable()
		{
			this.cardUnselectable = false;
			this.selectCard.SetActive(true);
		}

		// Token: 0x060093F0 RID: 37872 RVA: 0x00150843 File Offset: 0x0014EA43
		public void CardInThisZoneUnselectable()
		{
			this.cardUnselectable = true;
			this.selectCard.SetActive(false);
		}

		// Token: 0x060093F1 RID: 37873 RVA: 0x00150858 File Offset: 0x0014EA58
		public GPS HighlightThisZone(uint place, int min)
		{
			for (int i = 0; i < min; i++)
			{
				uint passController;
				if (this.p.controller == 0U)
				{
					passController = place & 65535U;
				}
				else
				{
					passController = place >> 16;
				}
				if ((passController & 127U) > 0U && (this.p.location & 4U) > 0U && (passController & 127U & (1U << (int)this.p.sequence)) > 0U)
				{
					this.ShowSelectZoneHighlight();
					this.selecting = true;
					return this.p;
				}
				if ((passController & 16128U) > 0U && (this.p.location & 8U) > 0U && ((passController >> 8) & (1U << (int)this.p.sequence)) > 0U)
				{
					this.ShowSelectZoneHighlight();
					this.selecting = true;
					return this.p;
				}
			}
			return null;
		}

		// Token: 0x060093F2 RID: 37874 RVA: 0x0015091C File Offset: 0x0014EB1C
		public void ShowSelectZoneHighlight()
		{
			this.select.SetActive(true);
		}

		// Token: 0x060093F3 RID: 37875 RVA: 0x0015092C File Offset: 0x0014EB2C
		public void ShowSelectCardHighlight()
		{
			this.selectCard.SetActive(true);
			if (((long)this.cookieCard.p.position & 3L) > 0L)
			{
				this.selectCard.transform.localEulerAngles = Vector3.zero;
				this.selectCardPush.transform.localEulerAngles = Vector3.zero;
				return;
			}
			this.selectCard.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			this.selectCardPush.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
		}

		// Token: 0x060093F4 RID: 37876 RVA: 0x001509D0 File Offset: 0x0014EBD0
		public GameCard FindCardInThisPlace()
		{
			if ((this.p.location & 4U) > 0U && this.p.sequence == 5U)
			{
				using (List<GameCard>.Enumerator enumerator = OcgCore.cards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameCard card = enumerator.Current;
						if ((card.p.location & 4U) > 0U)
						{
							if (card.p.controller == 0U && card.p.sequence == 5U && (card.p.location & 128U) == 0U)
							{
								return card;
							}
							if (card.p.controller == 1U && card.p.sequence == 6U && (card.p.location & 128U) == 0U)
							{
								return card;
							}
						}
					}
					goto IL_023F;
				}
			}
			if ((this.p.location & 4U) > 0U && this.p.sequence == 6U)
			{
				using (List<GameCard>.Enumerator enumerator = OcgCore.cards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameCard card2 = enumerator.Current;
						if ((card2.p.location & 4U) > 0U)
						{
							if (card2.p.controller == 0U && card2.p.sequence == 6U && (card2.p.location & 128U) == 0U)
							{
								return card2;
							}
							if (card2.p.controller == 1U && card2.p.sequence == 5U && (card2.p.location & 128U) == 0U)
							{
								return card2;
							}
						}
					}
					goto IL_023F;
				}
			}
			foreach (GameCard card3 in OcgCore.cards)
			{
				if (this.p.controller == card3.p.controller && (card3.p.location & 128U) == 0U && this.p.location == card3.p.location && this.p.sequence == card3.p.sequence)
				{
					return card3;
				}
			}
			IL_023F:
			return null;
		}

		// Token: 0x060093F5 RID: 37877 RVA: 0x00150C48 File Offset: 0x0014EE48
		public void ShowHint(uint location, uint controller)
		{
			if ((location & this.p.location) > 0U && controller == this.p.controller)
			{
				this.hintObj = ABLoader.LoadMasterDuelGameObject("fxp_HL_EXdeck_001");
				this.hintObj.transform.SetParent(base.transform, false);
				int cardCount = Program.instance.ocgcore.GetLocationCardCount((CardLocation)location, controller);
				this.hintObj.transform.localScale = new Vector3(1f, (float)cardCount * 0.1f, 1f);
			}
		}

		// Token: 0x060093F6 RID: 37878 RVA: 0x00150CD3 File Offset: 0x0014EED3
		public void HideHint()
		{
			if (this.hintObj != null)
			{
				global::UnityEngine.Object.Destroy(this.hintObj);
			}
		}

		// Token: 0x060093F7 RID: 37879 RVA: 0x00150CF0 File Offset: 0x0014EEF0
		public void SetDisabled(uint filter)
		{
			if ((this.p.location & 12U) == 0U)
			{
				return;
			}
			if (this.p.location == 4U && (this.p.sequence == 5U || this.p.sequence == 6U))
			{
				return;
			}
			int order = 0;
			if ((this.p.controller != 0U && OcgCore.isFirst) || (this.p.controller == 0U && !OcgCore.isFirst))
			{
				order += 16;
			}
			if (this.p.location == 8U)
			{
				order += 8;
			}
			order += (int)this.p.sequence;
			if (((ulong)filter & (ulong)(1L << (order & 31))) > 0UL)
			{
				this.disable.SetActive(true);
				return;
			}
			this.disable.SetActive(false);
		}

		// Token: 0x060093F8 RID: 37880 RVA: 0x00150DB0 File Offset: 0x0014EFB0
		public bool InTheSameLine(GPS gps)
		{
			return (gps.location & 65U) <= 0U && (this.p.location & 65U) <= 0U && ((this.p.sequence == gps.sequence && this.p.controller == gps.controller) || (this.p.sequence == 4U - gps.sequence && this.p.controller != gps.controller));
		}

		// Token: 0x0400D242 RID: 53826
		public GPS p;

		// Token: 0x0400D243 RID: 53827
		public List<GameCard.DuelButtonInfo> buttons = new List<GameCard.DuelButtonInfo>();

		// Token: 0x0400D244 RID: 53828
		public List<DuelButton> buttonObjs = new List<DuelButton>();

		// Token: 0x0400D245 RID: 53829
		private DuelButton selectButton;

		// Token: 0x0400D246 RID: 53830
		private GameObject highlight;

		// Token: 0x0400D247 RID: 53831
		private GameObject select;

		// Token: 0x0400D248 RID: 53832
		private GameObject selectPush;

		// Token: 0x0400D249 RID: 53833
		private GameObject selectCard;

		// Token: 0x0400D24A RID: 53834
		private GameObject selectCardPush;

		// Token: 0x0400D24B RID: 53835
		private GameObject disable;

		// Token: 0x0400D24C RID: 53836
		public GameCard cookieCard;

		// Token: 0x0400D24D RID: 53837
		private bool hover;

		// Token: 0x0400D24E RID: 53838
		private bool selecting;

		// Token: 0x0400D24F RID: 53839
		private bool selected;

		// Token: 0x0400D250 RID: 53840
		public bool cardSelecting;

		// Token: 0x0400D251 RID: 53841
		public bool cardSelected;

		// Token: 0x0400D252 RID: 53842
		private bool cardPreselected;

		// Token: 0x0400D253 RID: 53843
		private bool cardUnselectable;

		// Token: 0x0400D254 RID: 53844
		private bool countShowing;

		// Token: 0x0400D255 RID: 53845
		private bool buttonsCreated;

		// Token: 0x0400D256 RID: 53846
		private GameObject hintObj;
	}
}
