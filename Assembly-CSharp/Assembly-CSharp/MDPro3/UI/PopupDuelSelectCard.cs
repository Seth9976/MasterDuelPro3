using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001390 RID: 5008
	public class PopupDuelSelectCard : PopupDuel
	{
		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x060090CF RID: 37071 RVA: 0x0013E39E File Offset: 0x0013C59E
		// (set) Token: 0x060090D0 RID: 37072 RVA: 0x0013E3A6 File Offset: 0x0013C5A6
		public int SelectedCount
		{
			get
			{
				return this.m_selectedCount;
			}
			set
			{
				this.m_selectedCount = value;
				this.Refresh();
			}
		}

		// Token: 0x060090D1 RID: 37073 RVA: 0x0013E3B8 File Offset: 0x0013C5B8
		public override void InitializeSelections()
		{
			this.core = Program.instance.ocgcore;
			if (OcgCore.currentMessage == GameMessage.ConfirmCards)
			{
				this.btnConfirm.gameObject.SetActive(false);
				this.btnCancel.gameObject.SetActive(false);
				this.btnFinish.gameObject.SetActive(true);
			}
			else
			{
				this.btnCancel.GetComponent<ButtonPress>().SetInteractable(this.exitable);
				this.btnConfirm.GetComponent<ButtonPress>().SetInteractable(this.sendable);
			}
			if (this.cards.Count <= 4)
			{
				this.baseRect.sizeDelta = new Vector2(650f, 420f);
				this.scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2((float)(150 * this.cards.Count), 240f);
			}
			else if (this.cards.Count >= 5 && this.cards.Count <= 7)
			{
				this.baseRect.sizeDelta = new Vector2((float)(150 * this.cards.Count), 420f);
				this.scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2((float)(150 * this.cards.Count), 240f);
			}
			else
			{
				this.baseRect.sizeDelta = new Vector2(950f, 420f);
				this.scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(950f, 240f);
			}
			this.scrollView.content.sizeDelta = new Vector2((float)(150 * this.cards.Count), 220f);
			Addressables.LoadAssetAsync<GameObject>("UI/PopupDuelSelectCardItem.prefab").Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				for (int i = 0; i < this.cards.Count; i++)
				{
					GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(result.Result);
					gameObject.transform.SetParent(this.scrollView.content, false);
					gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)(i * 150), -220f);
					PopupDuelSelectCardItem mono = gameObject.GetComponent<PopupDuelSelectCardItem>();
					this.monos.Add(mono);
					mono.id = i;
					mono.card = this.cards[i];
					mono.cards = this.cards;
					mono.manager = this;
				}
				if (OcgCore.currentMessage == GameMessage.SelectSum)
				{
					foreach (GameCard card in OcgCore.cardsMustBeSelected)
					{
						foreach (PopupDuelSelectCardItem mono2 in this.monos)
						{
							if (mono2.card == card)
							{
								mono2.PreSelectThis();
								break;
							}
						}
					}
					foreach (PopupDuelSelectCardItem mono3 in this.monos)
					{
						if (!mono3.selected)
						{
							if (OcgCore.CheckSelectableInSum(OcgCore.cardsInSelection, mono3.card, OcgCore.cardsMustBeSelected, this.max + OcgCore.cardsMustBeSelected.Count))
							{
								mono3.SelectableThis();
							}
							else
							{
								mono3.UnselectableThis();
							}
						}
					}
					this.title.text = string.Concat(new string[]
					{
						this.hint,
						"-",
						OcgCore.GetSelectLevelSum(this.GetSelected())[0].ToString(),
						"/",
						OcgCore.ES_level.ToString()
					});
					return;
				}
				if (OcgCore.currentMessage == GameMessage.SortCard || OcgCore.currentMessage == GameMessage.SortChain)
				{
					this.order = true;
					this.title.text = this.hint;
					return;
				}
				if (OcgCore.currentMessage == GameMessage.SelectCard)
				{
					this.title.text = this.hint + "-0/" + this.max.ToString();
					return;
				}
				this.title.text = this.hint;
			};
		}

		// Token: 0x060090D2 RID: 37074 RVA: 0x0013E58C File Offset: 0x0013C78C
		public override void Show()
		{
			base.Show();
			Program.instance.currentServant.returnAction = new Action(this.OnCancel);
			if (!this.exitable)
			{
				Program.instance.currentServant.returnAction = new Action(this.FieldView);
			}
			if (OcgCore.currentMessage == GameMessage.ConfirmCards)
			{
				Program.instance.currentServant.returnAction = new Action(this.OnFinish);
			}
		}

		// Token: 0x060090D3 RID: 37075 RVA: 0x0013E604 File Offset: 0x0013C804
		private void Refresh()
		{
			if (OcgCore.currentMessage == GameMessage.SelectSum)
			{
				int[] sum = OcgCore.GetSelectLevelSum(this.GetSelected());
				if ((OcgCore.ES_overFlow && (OcgCore.ES_level <= sum[0] || OcgCore.ES_level <= sum[1])) || (!OcgCore.ES_overFlow && (OcgCore.ES_level == sum[0] || OcgCore.ES_level == sum[1])))
				{
					this.btnConfirm.interactable = true;
				}
				else
				{
					this.btnConfirm.interactable = false;
				}
				if (!OcgCore.ES_overFlow)
				{
					List<GameCard> selected = new List<GameCard>();
					foreach (PopupDuelSelectCardItem mono in this.monos)
					{
						if (mono.selected)
						{
							selected.Add(mono.card);
						}
					}
					foreach (PopupDuelSelectCardItem mono2 in this.monos)
					{
						if (!mono2.selected)
						{
							if (OcgCore.CheckSelectableInSum(OcgCore.cardsInSelection, mono2.card, selected, this.max + OcgCore.cardsMustBeSelected.Count))
							{
								mono2.SelectableThis();
							}
							else
							{
								mono2.UnselectableThis();
							}
						}
					}
				}
				int[] selectedSum = OcgCore.GetSelectLevelSum(this.GetSelected());
				if (!OcgCore.ES_overFlow)
				{
					if (selectedSum[0] == OcgCore.ES_level || selectedSum[1] == OcgCore.ES_level)
					{
						this.btnConfirm.GetComponent<ButtonPress>().SetInteractable(true);
					}
					else
					{
						this.btnConfirm.GetComponent<ButtonPress>().SetInteractable(false);
					}
				}
				else if (selectedSum[0] > OcgCore.ES_level || selectedSum[1] > OcgCore.ES_level)
				{
					this.btnConfirm.GetComponent<ButtonPress>().SetInteractable(true);
				}
				else
				{
					this.btnConfirm.GetComponent<ButtonPress>().SetInteractable(false);
				}
				this.title.text = string.Concat(new string[]
				{
					this.hint,
					"-",
					selectedSum[0].ToString(),
					"/",
					OcgCore.ES_level.ToString()
				});
				return;
			}
			if (OcgCore.currentMessage != GameMessage.ConfirmCards)
			{
				if (this.SelectedCount >= this.min)
				{
					this.btnConfirm.GetComponent<ButtonPress>().SetInteractable(true);
				}
				else
				{
					this.btnConfirm.GetComponent<ButtonPress>().SetInteractable(false);
				}
				if (this.SelectedCount >= this.max)
				{
					using (List<PopupDuelSelectCardItem>.Enumerator enumerator = this.monos.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PopupDuelSelectCardItem mono3 = enumerator.Current;
							if (!mono3.selected)
							{
								mono3.UnselectableThis();
							}
						}
						goto IL_02BC;
					}
				}
				foreach (PopupDuelSelectCardItem popupDuelSelectCardItem in this.monos)
				{
					popupDuelSelectCardItem.SelectableThis();
				}
				IL_02BC:
				if (OcgCore.currentMessage == GameMessage.SelectCard)
				{
					this.title.text = string.Concat(new string[]
					{
						this.hint,
						"-",
						this.GetSelected().Count.ToString(),
						"/",
						this.max.ToString()
					});
				}
			}
		}

		// Token: 0x060090D4 RID: 37076 RVA: 0x0013E960 File Offset: 0x0013CB60
		public void RemoveOrder(int i)
		{
			foreach (PopupDuelSelectCardItem popupDuelSelectCardItem in this.monos)
			{
				popupDuelSelectCardItem.RemoveOrder(i);
			}
		}

		// Token: 0x060090D5 RID: 37077 RVA: 0x0013E9B4 File Offset: 0x0013CBB4
		private List<GameCard> GetSelected()
		{
			List<GameCard> list = new List<GameCard>();
			foreach (PopupDuelSelectCardItem mono in this.monos)
			{
				if (mono.selected)
				{
					list.Add(mono.card);
				}
			}
			return list;
		}

		// Token: 0x060090D6 RID: 37078 RVA: 0x0013EA1C File Offset: 0x0013CC1C
		private bool CheckSelectable(GameCard card, List<GameCard> addedCards = null)
		{
			bool returnValue = false;
			int[] sum = OcgCore.GetSelectLevelSum(this.GetSelected());
			if (addedCards != null)
			{
				foreach (GameCard c in addedCards)
				{
					sum[0] += c.levelForSelect_1;
					sum[1] += c.levelForSelect_2;
				}
			}
			if (sum[0] + card.levelForSelect_1 == OcgCore.ES_level || sum[1] + card.levelForSelect_2 == OcgCore.ES_level)
			{
				return true;
			}
			List<GameCard> newAddedCards = new List<GameCard>();
			if (addedCards != null)
			{
				foreach (GameCard c2 in addedCards)
				{
					newAddedCards.Add(c2);
				}
			}
			newAddedCards.Add(card);
			foreach (PopupDuelSelectCardItem mono in this.monos)
			{
				if (!mono.selected && !newAddedCards.Contains(mono.card))
				{
					returnValue = this.CheckSelectable(mono.card, newAddedCards);
					if (returnValue)
					{
						return true;
					}
				}
			}
			return returnValue;
		}

		// Token: 0x060090D7 RID: 37079 RVA: 0x0013EB80 File Offset: 0x0013CD80
		public override void OnConfirm()
		{
			base.OnConfirm();
			GameMessage currentMessage = OcgCore.currentMessage;
			BinaryMaster binaryMaster;
			switch (currentMessage)
			{
			case GameMessage.SelectBattleCmd:
			case GameMessage.SelectIdleCmd:
			{
				PopupDuelSelectCardItem selectedCard = null;
				foreach (PopupDuelSelectCardItem mono in this.monos)
				{
					if (mono.selected)
					{
						selectedCard = mono;
						break;
					}
				}
				if (selectedCard == null)
				{
					goto IL_0548;
				}
				int response = 0;
				bool needSend = true;
				if (this.hint == InterString.Get("选择效果发动。", 0))
				{
					if (selectedCard.card.effects.Count == 1)
					{
						response = selectedCard.card.effects[0].ptr;
					}
					else
					{
						List<string> selections = new List<string> { InterString.Get("效果选择", 0) };
						List<int> responses = new List<int>();
						for (int i = 0; i < selectedCard.card.effects.Count; i++)
						{
							string desc = selectedCard.card.effects[i].desc;
							if (desc.Length <= 2)
							{
								desc = InterString.Get("发动效果", 0);
							}
							selections.Add(desc);
							responses.Add(selectedCard.card.effects[i].ptr);
						}
						Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowPopupSelection(selections, responses);
						needSend = false;
					}
				}
				else
				{
					foreach (GameCard.DuelButtonInfo btn in selectedCard.card.buttons)
					{
						if (btn.type == ButtonType.SpSummon)
						{
							response = btn.response[0];
						}
					}
				}
				if (needSend)
				{
					binaryMaster = new BinaryMaster(null);
					binaryMaster.writer.Write(response);
					base.SendReturn(binaryMaster.Get());
					goto IL_0548;
				}
				goto IL_0548;
			}
			case GameMessage.SelectEffectYn:
				binaryMaster = new BinaryMaster(null);
				binaryMaster.writer.Write(1);
				base.SendReturn(binaryMaster.Get());
				goto IL_0548;
			case GameMessage.SelectYesNo:
			case GameMessage.SelectOption:
			case (GameMessage)17:
			case GameMessage.SelectPlace:
			case GameMessage.SelectPosition:
			case GameMessage.SelectCounter:
			case GameMessage.SelectDisfield:
				goto IL_0548;
			case GameMessage.SelectCard:
			case GameMessage.SelectTribute:
			case GameMessage.SelectSum:
			case GameMessage.SelectUnselect:
				break;
			case GameMessage.SelectChain:
			{
				using (List<PopupDuelSelectCardItem>.Enumerator enumerator = this.monos.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PopupDuelSelectCardItem mono2 = enumerator.Current;
						if (mono2.selected)
						{
							if (mono2.card.effects.Count == 1)
							{
								binaryMaster = new BinaryMaster(null);
								binaryMaster.writer.Write(mono2.card.effects[0].ptr);
								base.SendReturn(binaryMaster.Get());
							}
							else
							{
								List<string> selections2 = new List<string> { InterString.Get("效果选择", 0) };
								List<int> responses2 = new List<int>();
								for (int j = 0; j < mono2.card.effects.Count; j++)
								{
									string desc2 = mono2.card.effects[j].desc;
									if (desc2.Length <= 2)
									{
										desc2 = InterString.Get("发动效果", 0);
									}
									selections2.Add(desc2);
									responses2.Add(mono2.card.effects[j].ptr);
								}
								Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowPopupSelection(selections2, responses2);
							}
						}
					}
					goto IL_0548;
				}
				break;
			}
			case GameMessage.SortChain:
			case GameMessage.SortCard:
			{
				byte[] bytes = new byte[this.monos.Count];
				for (int k = 0; k < this.monos.Count; k++)
				{
					bytes[k] = (byte)(this.monos[k].GetOrder() - 1);
				}
				binaryMaster = new BinaryMaster(null);
				binaryMaster.writer.Write(bytes);
				base.SendReturn(binaryMaster.Get());
				goto IL_0548;
			}
			default:
				if (currentMessage != GameMessage.AnnounceCard)
				{
					goto IL_0548;
				}
				foreach (PopupDuelSelectCardItem mono3 in this.monos)
				{
					if (mono3.selected)
					{
						binaryMaster = new BinaryMaster(null);
						binaryMaster.writer.Write(mono3.card.GetData().Id);
						base.SendReturn(binaryMaster.Get());
					}
				}
				Program.instance.ocgcore.ClearAnnounceCards();
				goto IL_0548;
			}
			int count = 0;
			using (List<PopupDuelSelectCardItem>.Enumerator enumerator = this.monos.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.selected)
					{
						count++;
					}
				}
			}
			binaryMaster = new BinaryMaster(null);
			if (OcgCore.currentMessage == GameMessage.SelectUnselect && count == 0)
			{
				binaryMaster.writer.Write(-1);
			}
			else
			{
				binaryMaster.writer.Write((byte)count);
				foreach (PopupDuelSelectCardItem mono4 in this.monos)
				{
					if (mono4.selected)
					{
						binaryMaster.writer.Write((byte)mono4.card.selectPtr);
					}
				}
			}
			base.SendReturn(binaryMaster.Get());
			IL_0548:
			AudioManager.PlaySE("SE_DUEL_DECIDE", 1f);
			this.Hide();
		}

		// Token: 0x060090D8 RID: 37080 RVA: 0x0013F180 File Offset: 0x0013D380
		public override void OnCancel()
		{
			base.OnCancel();
			if (!this.exitable)
			{
				return;
			}
			AudioManager.PlaySE("SE_DUEL_CANCEL", 1f);
			GameMessage currentMessage = OcgCore.currentMessage;
			BinaryMaster binaryMaster;
			switch (currentMessage)
			{
			case GameMessage.SelectBattleCmd:
			case GameMessage.SelectIdleCmd:
				goto IL_011D;
			case GameMessage.SelectEffectYn:
				binaryMaster = new BinaryMaster(null);
				binaryMaster.writer.Write(0);
				base.SendReturn(binaryMaster.Get());
				goto IL_011D;
			case GameMessage.SelectYesNo:
			case GameMessage.SelectOption:
			case GameMessage.SelectCard:
			case GameMessage.SelectChain:
			case (GameMessage)17:
			case GameMessage.SelectPlace:
			case GameMessage.SelectPosition:
			case GameMessage.SelectTribute:
				break;
			default:
				if (currentMessage != GameMessage.SelectUnselect)
				{
					if (currentMessage == GameMessage.AnnounceCard)
					{
						List<string> ss = new List<string>
						{
							InterString.Get("请输入关键字：", 0),
							InterString.Get("搜索", 0),
							string.Empty,
							string.Empty
						};
						this.whenQuitDo = delegate
						{
							Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowPopupInput(ss, new Action<string>(Program.instance.ocgcore.OnAnnounceCard), null, InputValidation.ValidationType.None);
						};
						Program.instance.ocgcore.ClearAnnounceCards();
						goto IL_011D;
					}
				}
				break;
			}
			binaryMaster = new BinaryMaster(null);
			binaryMaster.writer.Write(-1);
			base.SendReturn(binaryMaster.Get());
			IL_011D:
			this.Hide();
		}

		// Token: 0x060090D9 RID: 37081 RVA: 0x0013F2B0 File Offset: 0x0013D4B0
		public void OnFinish()
		{
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, this.transitionTime).OnComplete(delegate
			{
				Program.instance.ocgcore.messageDispatcher.playerResponed = true;
			});
			AudioManager.PlaySE("SE_DUEL_DECIDE", 1f);
			this.Hide();
		}

		// Token: 0x060090DA RID: 37082 RVA: 0x0013F32C File Offset: 0x0013D52C
		public override void Hide()
		{
			global::UnityEngine.Object.Destroy(this.arrow);
			if (this.shadow != null)
			{
				this.shadow.DOFade(0f, this.transitionTime);
			}
			this.window.DOAnchorPos(new Vector2(0f, -1100f), this.transitionTime, false).OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
				Program.instance.ocgcore.returnAction = null;
				Action whenQuitDo = this.whenQuitDo;
				if (whenQuitDo == null)
				{
					return;
				}
				whenQuitDo();
			});
			Program.instance.ocgcore.currentPopup = null;
		}

		// Token: 0x0400CF8C RID: 53132
		[Header("Popup Duel SelectCard Reference")]
		public ScrollRect scrollView;

		// Token: 0x0400CF8D RID: 53133
		public RectTransform baseRect;

		// Token: 0x0400CF8E RID: 53134
		public Button btnFinish;

		// Token: 0x0400CF8F RID: 53135
		public string hint;

		// Token: 0x0400CF90 RID: 53136
		public List<GameCard> cards;

		// Token: 0x0400CF91 RID: 53137
		public int min;

		// Token: 0x0400CF92 RID: 53138
		public int max;

		// Token: 0x0400CF93 RID: 53139
		public bool sendable;

		// Token: 0x0400CF94 RID: 53140
		public bool order;

		// Token: 0x0400CF95 RID: 53141
		public int currentSort;

		// Token: 0x0400CF96 RID: 53142
		public GameObject arrow;

		// Token: 0x0400CF97 RID: 53143
		private int m_selectedCount;

		// Token: 0x0400CF98 RID: 53144
		private OcgCore core;

		// Token: 0x0400CF99 RID: 53145
		public List<PopupDuelSelectCardItem> monos = new List<PopupDuelSelectCardItem>();
	}
}
