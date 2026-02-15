using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200135F RID: 4959
	public class DuelButton : MonoBehaviour
	{
		// Token: 0x06008FA0 RID: 36768 RVA: 0x00137940 File Offset: 0x00135B40
		private void Start()
		{
			base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
			base.transform.SetParent(Program.instance.ui_.duelButton, false);
			this.RefreshPosition();
			this.text.text = this.hint;
			if (this.hint == string.Empty)
			{
				this.bg.SetActive(false);
			}
			base.transform.localScale = Vector3.zero;
			base.StartCoroutine(this.RefreshIcons());
		}

		// Token: 0x06008FA1 RID: 36769 RVA: 0x001379D6 File Offset: 0x00135BD6
		private IEnumerator RefreshIcons()
		{
			while (TextureManager.container == null)
			{
				yield return null;
			}
			SpriteState spriteState = default(SpriteState);
			switch (this.type)
			{
			case ButtonType.Select:
				base.GetComponent<Image>().sprite = TextureManager.container.select[0];
				spriteState.highlightedSprite = TextureManager.container.select[1];
				spriteState.pressedSprite = TextureManager.container.select[2];
				spriteState.disabledSprite = TextureManager.container.select[3];
				break;
			case ButtonType.Decide:
				base.GetComponent<Image>().sprite = TextureManager.container.decide[0];
				spriteState.highlightedSprite = TextureManager.container.decide[1];
				spriteState.pressedSprite = TextureManager.container.decide[2];
				spriteState.disabledSprite = TextureManager.container.decide[3];
				break;
			case ButtonType.Cancel:
				base.GetComponent<Image>().sprite = TextureManager.container.cancel[0];
				spriteState.highlightedSprite = TextureManager.container.cancel[1];
				spriteState.pressedSprite = TextureManager.container.cancel[2];
				spriteState.disabledSprite = TextureManager.container.cancel[3];
				break;
			case ButtonType.Activate:
				base.GetComponent<Image>().sprite = TextureManager.container.activate[0];
				spriteState.highlightedSprite = TextureManager.container.activate[1];
				spriteState.pressedSprite = TextureManager.container.activate[2];
				spriteState.disabledSprite = TextureManager.container.activate[3];
				break;
			case ButtonType.SetPendulum:
				base.GetComponent<Image>().sprite = TextureManager.container.setPendulum[0];
				spriteState.highlightedSprite = TextureManager.container.setPendulum[1];
				spriteState.pressedSprite = TextureManager.container.setPendulum[2];
				spriteState.disabledSprite = TextureManager.container.setPendulum[3];
				break;
			case ButtonType.Battle:
				base.GetComponent<Image>().sprite = TextureManager.container.battle[0];
				spriteState.highlightedSprite = TextureManager.container.battle[1];
				spriteState.pressedSprite = TextureManager.container.battle[2];
				spriteState.disabledSprite = TextureManager.container.battle[3];
				break;
			case ButtonType.ToAttackPosition:
				base.GetComponent<Image>().sprite = TextureManager.container.toAttack[0];
				spriteState.highlightedSprite = TextureManager.container.toAttack[1];
				spriteState.pressedSprite = TextureManager.container.toAttack[2];
				spriteState.disabledSprite = TextureManager.container.toAttack[3];
				break;
			case ButtonType.ToDefensePosition:
				base.GetComponent<Image>().sprite = TextureManager.container.toDefense[0];
				spriteState.highlightedSprite = TextureManager.container.toDefense[1];
				spriteState.pressedSprite = TextureManager.container.toDefense[2];
				spriteState.disabledSprite = TextureManager.container.toDefense[3];
				break;
			case ButtonType.SpSummon:
				base.GetComponent<Image>().sprite = TextureManager.container.spSummon[0];
				spriteState.highlightedSprite = TextureManager.container.spSummon[1];
				spriteState.pressedSprite = TextureManager.container.spSummon[2];
				spriteState.disabledSprite = TextureManager.container.spSummon[3];
				break;
			case ButtonType.Summon:
				base.GetComponent<Image>().sprite = TextureManager.container.summon[0];
				spriteState.highlightedSprite = TextureManager.container.summon[1];
				spriteState.pressedSprite = TextureManager.container.summon[2];
				spriteState.disabledSprite = TextureManager.container.summon[3];
				break;
			case ButtonType.PenSummon:
				base.GetComponent<Image>().sprite = TextureManager.container.penSummon[0];
				spriteState.highlightedSprite = TextureManager.container.penSummon[1];
				spriteState.pressedSprite = TextureManager.container.penSummon[2];
				spriteState.disabledSprite = TextureManager.container.penSummon[3];
				break;
			case ButtonType.SetSpell:
				base.GetComponent<Image>().sprite = TextureManager.container.setSpell[0];
				spriteState.highlightedSprite = TextureManager.container.setSpell[1];
				spriteState.pressedSprite = TextureManager.container.setSpell[2];
				spriteState.disabledSprite = TextureManager.container.setSpell[3];
				break;
			case ButtonType.SetMonster:
				base.GetComponent<Image>().sprite = TextureManager.container.setMonster[0];
				spriteState.highlightedSprite = TextureManager.container.setMonster[1];
				spriteState.pressedSprite = TextureManager.container.setMonster[2];
				spriteState.disabledSprite = TextureManager.container.setMonster[3];
				break;
			}
			base.GetComponent<Button>().spriteState = spriteState;
			yield break;
		}

		// Token: 0x06008FA2 RID: 36770 RVA: 0x001379E8 File Offset: 0x00135BE8
		private void RefreshPosition()
		{
			if (this.response[0] == -4)
			{
				float middle = Program.instance.ui_.GetComponent<RectTransform>().sizeDelta.x / 2f;
				base.GetComponent<RectTransform>().anchoredPosition = new Vector2(middle + 340f, 620f);
				return;
			}
			if (this.response[0] == -5)
			{
				float middle2 = Program.instance.ui_.GetComponent<RectTransform>().sizeDelta.x / 2f;
				base.GetComponent<RectTransform>().anchoredPosition = new Vector2(middle2 - 340f, 620f);
				return;
			}
			float height = 150f;
			Vector2 uiPoint;
			if (this.cookieCard == null || this.cookieCard.model == null)
			{
				Vector3 position = GameCard.GetCardPosition(new GPS
				{
					location = this.location,
					controller = this.controller,
					sequence = this.sequence
				}, null, null);
				uiPoint = UIManager.WorldToScreenPoint(Program.instance.camera_.cameraMain, position);
				if ((this.location & 65U) > 0U)
				{
					if (this.controller == 0U)
					{
						height = 230f;
					}
					else
					{
						height = -150f;
					}
				}
				else if ((this.location & 8U) > 0U && this.controller != 0U)
				{
					height = -150f;
				}
			}
			else
			{
				uiPoint = UIManager.WorldToScreenPoint(Program.instance.camera_.cameraMain, this.cookieCard.model.transform.position);
				if (this.cookieCard != null && (this.cookieCard.p.location & 2U) > 0U)
				{
					if (this.cookieCard.p.controller == 0U)
					{
						height = 250f;
					}
					else
					{
						height = -200f;
					}
				}
			}
			base.GetComponent<RectTransform>().anchoredPosition = new Vector2(uiPoint.x - (float)((this.buttonsCount - 1) * 80) + (float)(this.id * 160), uiPoint.y + height);
		}

		// Token: 0x06008FA3 RID: 36771 RVA: 0x00137BED File Offset: 0x00135DED
		public void Show()
		{
			this.RefreshPosition();
			if (this.showing)
			{
				return;
			}
			this.showing = true;
			base.transform.DOScale(1f, DuelButton.transitionTime);
		}

		// Token: 0x06008FA4 RID: 36772 RVA: 0x00137C1B File Offset: 0x00135E1B
		public void Hide()
		{
			if (!this.showing)
			{
				return;
			}
			this.showing = false;
			base.transform.DOScale(0f, DuelButton.transitionTime);
		}

		// Token: 0x06008FA5 RID: 36773 RVA: 0x00137C44 File Offset: 0x00135E44
		private void OnClick()
		{
			AudioManager.PlaySE("SE_DUEL_DECIDE", 1f);
			if (this.response[0] >= 0)
			{
				GameMessage currentMessage = OcgCore.currentMessage;
				if (currentMessage - GameMessage.SelectBattleCmd <= 1)
				{
					if (this.response.Count == 1 || this.type != ButtonType.Activate)
					{
						BinaryMaster p = new BinaryMaster(null);
						p.writer.Write(this.response[0]);
						Program.instance.ocgcore.SendReturn(p.Get(), 0f);
						return;
					}
					List<string> selections = new List<string> { InterString.Get("效果选择", 0) };
					List<int> responses = new List<int>();
					for (int i = 0; i < this.cookieCard.effects.Count; i++)
					{
						string desc = this.cookieCard.effects[i].desc;
						if (desc.Length <= 2)
						{
							desc = InterString.Get("发动效果", 0);
						}
						selections.Add(desc);
						responses.Add(this.cookieCard.effects[i].ptr);
					}
					selections.Add(InterString.Get("放弃", 0));
					responses.Add(-233);
					Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowPopupSelection(selections, responses);
					return;
				}
			}
			else if (this.response[0] == -1 || this.response[0] == -2)
			{
				List<GameCard> responseCards = new List<GameCard>();
				foreach (GameCard card in OcgCore.cards)
				{
					if (card.p.controller == this.controller && (card.p.location & this.location) > 0U)
					{
						using (List<GameCard.DuelButtonInfo>.Enumerator enumerator2 = card.buttons.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current.type == this.type)
								{
									responseCards.Add(card);
									break;
								}
							}
						}
					}
				}
				if (this.type == ButtonType.Activate)
				{
					Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowPopupSelectCard(InterString.Get("选择效果发动。", 0), responseCards, 1, 1, true, false);
					return;
				}
				Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowPopupSelectCard(InterString.Get("选择怪兽特殊召唤。", 0), responseCards, 1, 1, true, false);
				return;
			}
			else
			{
				if (this.response[0] == -3)
				{
					using (List<PlaceSelector>.Enumerator enumerator3 = Program.instance.ocgcore.places.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							PlaceSelector place = enumerator3.Current;
							if (place.p.controller == this.controller && place.p.location == this.location && place.p.sequence == this.sequence)
							{
								place.SelectCardInThisZone();
							}
						}
						return;
					}
				}
				if (this.response[0] == -4)
				{
					Program.instance.ocgcore.FieldSelectedSend();
					return;
				}
				if (this.response[0] == -5)
				{
					Program.instance.ocgcore.FieldSelectedCancel();
				}
			}
		}

		// Token: 0x0400CE14 RID: 52756
		public GameCard cookieCard;

		// Token: 0x0400CE15 RID: 52757
		public List<int> response = new List<int>();

		// Token: 0x0400CE16 RID: 52758
		public string hint;

		// Token: 0x0400CE17 RID: 52759
		public ButtonType type;

		// Token: 0x0400CE18 RID: 52760
		public int id;

		// Token: 0x0400CE19 RID: 52761
		public int buttonsCount;

		// Token: 0x0400CE1A RID: 52762
		public uint location;

		// Token: 0x0400CE1B RID: 52763
		public uint controller;

		// Token: 0x0400CE1C RID: 52764
		public uint sequence;

		// Token: 0x0400CE1D RID: 52765
		public TextMeshProUGUI text;

		// Token: 0x0400CE1E RID: 52766
		public GameObject bg;

		// Token: 0x0400CE1F RID: 52767
		private static float transitionTime = 0.1f;

		// Token: 0x0400CE20 RID: 52768
		private bool showing;
	}
}
