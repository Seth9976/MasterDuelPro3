using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace MDPro3.UI
{
	// Token: 0x020013C1 RID: 5057
	public class SelectionToggle_Deck : SelectionToggle_ScrollRectItem
	{
		// Token: 0x06009286 RID: 37510 RVA: 0x001480B8 File Offset: 0x001462B8
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = false;
			this.manuallySetNavigation = false;
			this.simpleMove = false;
			this.selectedWhenHover = true;
			this.ApplyHideToggle();
			this.ApplyHidePickup(true);
		}

		// Token: 0x06009287 RID: 37511 RVA: 0x001480EC File Offset: 0x001462EC
		public override void Refresh()
		{
			if (this.index == 0)
			{
				base.Manager.GetElement("DeckCaseIcon").SetActive(false);
				base.Manager.GetElement("TextDeckName").SetActive(false);
				base.Manager.GetElement("IconAddDeck").SetActive(true);
				base.Manager.GetElement("SelectedStateToggle").SetActive(false);
				this.SoundLabelPointerEnter = string.Empty;
				this.SoundLabelSelectedGamePad = "SE_MENU_OVERLAP_02";
			}
			else
			{
				base.Manager.GetElement("DeckCaseIcon").SetActive(true);
				base.Manager.GetElement("TextDeckName").SetActive(true);
				base.Manager.GetElement("IconAddDeck").SetActive(false);
				base.Manager.GetElement("SelectedStateToggle").SetActive(this.toggleMode);
				this.SoundLabelPointerEnter = "SE_DECK_CARD_SELECT";
				this.SoundLabelSelectedGamePad = "SE_DECK_CARD_SELECT";
				base.Manager.GetElement("IconOn").SetActive(this.isOn);
				base.Manager.GetElement<TextMeshProUGUI>("TextDeckName").text = this.deckName;
				if (this.forcedPickup && !this.pickuping)
				{
					this.ShowPickup(true);
				}
			}
			this.RefreshDeckCaseAsync();
			this.refreshed = false;
			if (this.pickuping)
			{
				this.StartRefresh();
			}
		}

		// Token: 0x06009288 RID: 37512 RVA: 0x00148250 File Offset: 0x00146450
		protected virtual void StartRefresh()
		{
			if (base.gameObject.activeInHierarchy)
			{
				try
				{
					CancellationTokenSource cts = this.cts;
					if (cts != null)
					{
						cts.Cancel();
					}
					CancellationTokenSource cts2 = this.cts;
					if (cts2 != null)
					{
						cts2.Dispose();
					}
				}
				catch
				{
				}
				this.cts = new CancellationTokenSource();
				this.RefreshAsync();
			}
		}

		// Token: 0x06009289 RID: 37513 RVA: 0x001482B4 File Offset: 0x001464B4
		protected virtual async UniTask RefreshDeckCaseAsync()
		{
			if (this.index != 0)
			{
				await UniTask.WaitWhile(() => Program.instance.deckSelector.inTransition, PlayerLoopTiming.Update, default(CancellationToken), false);
				for (int i = 0; i < base.transform.GetSiblingIndex(); i++)
				{
					await UniTask.Yield(base.destroyCancellationToken, false);
				}
				if (!(base.gameObject == null))
				{
					Sprite sprite = await Program.items.LoadDeckCaseIconAsync(this.deckCase, "_L_SD");
					if (sprite != null)
					{
						base.Manager.GetElement<Image>("DeckImage").sprite = sprite;
					}
				}
			}
		}

		// Token: 0x0600928A RID: 37514 RVA: 0x001482F8 File Offset: 0x001464F8
		protected override async UniTask RefreshAsync()
		{
			if (this.index != 0)
			{
				await UniTask.WaitWhile(() => Program.instance.deckSelector.inTransition, PlayerLoopTiming.Update, default(CancellationToken), false);
				Material pMat = null;
				RawImage cardImage0 = base.Manager.GetElement<RawImage>("CardImage0");
				if (this.card0 != 0)
				{
					RawImage rawImage = cardImage0;
					rawImage.texture = await CardImageLoader.LoadCardAsync(this.card0, true, default(CancellationToken), false);
					rawImage = null;
				}
				else
				{
					if (pMat == null)
					{
						pMat = await ABLoader.LoadProtectorMaterial(this.protector, this.cts.Token);
					}
					cardImage0.texture = null;
					cardImage0.material = pMat;
				}
				RawImage cardImage = base.Manager.GetElement<RawImage>("CardImage1");
				if (this.card1 != 0)
				{
					RawImage rawImage = cardImage;
					rawImage.texture = await CardImageLoader.LoadCardAsync(this.card1, true, default(CancellationToken), false);
					rawImage = null;
				}
				else
				{
					if (pMat == null)
					{
						pMat = await ABLoader.LoadProtectorMaterial(this.protector, this.cts.Token);
					}
					cardImage.texture = null;
					cardImage.material = pMat;
				}
				RawImage cardImage2 = base.Manager.GetElement<RawImage>("CardImage2");
				if (this.card2 != 0)
				{
					RawImage rawImage = cardImage2;
					rawImage.texture = await CardImageLoader.LoadCardAsync(this.card2, true, default(CancellationToken), false);
					rawImage = null;
				}
				else
				{
					if (pMat == null)
					{
						pMat = await ABLoader.LoadProtectorMaterial(this.protector, this.cts.Token);
					}
					cardImage2.texture = null;
					cardImage2.material = pMat;
				}
			}
		}

		// Token: 0x0600928B RID: 37515 RVA: 0x0014833C File Offset: 0x0014653C
		protected override void OnClick()
		{
			if (this.index == 0)
			{
				AudioManager.PlaySE(this.SoundLabelClick, 1f);
				Program.instance.deckSelector.GetUI<DeckSelectorUI>().DeckCreate();
				return;
			}
			if (this.toggleMode)
			{
				this.isOn = !this.isOn;
				if (this.isOn)
				{
					base.Manager.GetElement("IconOn").SetActive(true);
					Program.instance.deckSelector.GetUI<DeckSelectorUI>().superScrollView.items[this.index].args[6] = "1";
				}
				else
				{
					base.Manager.GetElement("IconOn").SetActive(false);
					Program.instance.deckSelector.GetUI<DeckSelectorUI>().superScrollView.items[this.index].args[6] = "0";
				}
				AudioManager.PlaySE(this.isOn ? this.SoundLabelClickOn : this.SoundLabelClickOff, 1f);
				return;
			}
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Config.SetConfigDeck(this.deckName, true);
			if (DeckSelector.condition == DeckSelector.Condition.ForEdit)
			{
				Program.instance.deckEditor.SwitchCondition(DeckEditor.Condition.EditDeck, "", null);
				Program.instance.ShiftToServant(Program.instance.deckEditor);
				return;
			}
			if (DeckSelector.condition == DeckSelector.Condition.MyCard)
			{
				Program.instance.ShiftToServant(Program.instance.online);
				return;
			}
			if (DeckSelector.condition == DeckSelector.Condition.ForDuel)
			{
				Program.instance.ShiftToServant(Program.instance.room);
				return;
			}
			if (DeckSelector.condition == DeckSelector.Condition.ForSolo)
			{
				Program.instance.ShiftToServant(Program.instance.solo);
			}
		}

		// Token: 0x0600928C RID: 37516 RVA: 0x001484EB File Offset: 0x001466EB
		protected override void OnSubmit()
		{
			this.OnClick();
		}

		// Token: 0x0600928D RID: 37517 RVA: 0x001484F4 File Offset: 0x001466F4
		protected override void OnSelect(bool playSE)
		{
			this.HoverOn();
			if (playSE)
			{
				AudioManager.PlaySE(this.SoundLabelSelectedGamePad, 1f);
			}
			Program.instance.currentServant.lastSelectable = base.Selectable;
			Program.instance.deckSelector.lastSelectedDeckItem = this;
			ColorContainerGraphic[] componentsInChildren = base.transform.GetComponentsInChildren<ColorContainerGraphic>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetColor(ColorContainer.SelectMode.Selected, this.hovering ? ColorContainer.StatusMode.Enter : ColorContainer.StatusMode.Normal, base.Selectable.interactable);
			}
		}

		// Token: 0x0600928E RID: 37518 RVA: 0x0014857A File Offset: 0x0014677A
		public virtual void ShowPickup(bool forced = false)
		{
			if (forced)
			{
				this.forcedPickup = true;
			}
			if (this.index == 0)
			{
				return;
			}
			if (this.pickuping)
			{
				return;
			}
			this.pickuping = true;
			this.ApplyShowPickup();
		}

		// Token: 0x0600928F RID: 37519 RVA: 0x001485A8 File Offset: 0x001467A8
		protected virtual void ApplyShowPickup()
		{
			foreach (Tweener tween in this.pickdownTweens)
			{
				if (tween.IsActive())
				{
					tween.Kill(false);
				}
			}
			this.pickdownTweens.Clear();
			TweenerCore<float, float, FloatOptions> tween2 = base.Manager.GetElement<CanvasGroup>("CardPos0").DOFade(1f, 0.2f).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween2);
			TweenerCore<float, float, FloatOptions> tween3 = base.Manager.GetElement<CanvasGroup>("CardPos1").DOFade(1f, 0.22f).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween3);
			TweenerCore<float, float, FloatOptions> tween4 = base.Manager.GetElement<CanvasGroup>("CardPos2").DOFade(1f, 0.24f).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween4);
			TweenerCore<Vector3, Vector3, VectorOptions> tween5 = base.Manager.GetElement<RectTransform>("CardImage0").DOAnchorPos3D(new Vector3(0f, 10f, 0f), 0.2f, false).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween5);
			TweenerCore<Vector3, Vector3, VectorOptions> tween6 = base.Manager.GetElement<RectTransform>("CardImage1").DOAnchorPos3D(new Vector3(0f, 10f, 0f), 0.22f, false).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween6);
			TweenerCore<Vector3, Vector3, VectorOptions> tween7 = base.Manager.GetElement<RectTransform>("CardImage2").DOAnchorPos3D(new Vector3(0f, 10f, 0f), 0.24f, false).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween7);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween8 = base.Manager.GetElement<RectTransform>("CardImage0").DOLocalRotate(Vector3.zero, 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween8);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween9 = base.Manager.GetElement<RectTransform>("CardImage2").DOLocalRotate(Vector3.zero, 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickupTweens.Add(tween9);
			if (!this.refreshed)
			{
				this.StartRefresh();
			}
		}

		// Token: 0x06009290 RID: 37520 RVA: 0x001487F4 File Offset: 0x001469F4
		public virtual void HidePickup(bool forced = false)
		{
			if (this.index == 0)
			{
				return;
			}
			if (forced)
			{
				this.forcedPickup = false;
			}
			if (!this.pickuping)
			{
				return;
			}
			if (this.forcedPickup)
			{
				return;
			}
			this.pickuping = false;
			this.ApplyHidePickup(false);
		}

		// Token: 0x06009291 RID: 37521 RVA: 0x0014882C File Offset: 0x00146A2C
		protected virtual void ApplyHidePickup(bool instant = false)
		{
			foreach (Tweener tween in this.pickupTweens)
			{
				if (tween.IsActive())
				{
					tween.Kill(false);
				}
			}
			this.pickupTweens.Clear();
			TweenerCore<float, float, FloatOptions> tween2 = base.Manager.GetElement<CanvasGroup>("CardPos0").DOFade(0f, instant ? 0f : 0.2f).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween2);
			TweenerCore<float, float, FloatOptions> tween3 = base.Manager.GetElement<CanvasGroup>("CardPos1").DOFade(0f, instant ? 0f : 0.22f).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween3);
			TweenerCore<float, float, FloatOptions> tween4 = base.Manager.GetElement<CanvasGroup>("CardPos2").DOFade(0f, instant ? 0f : 0.24f).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween4);
			TweenerCore<Vector3, Vector3, VectorOptions> tween5 = base.Manager.GetElement<RectTransform>("CardImage0").DOAnchorPos3D(new Vector3(0f, -40f, 0f), 0.2f, false).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween5);
			TweenerCore<Vector3, Vector3, VectorOptions> tween6 = base.Manager.GetElement<RectTransform>("CardImage1").DOAnchorPos3D(new Vector3(0f, -40f, 0f), 0.22f, false).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween6);
			TweenerCore<Vector3, Vector3, VectorOptions> tween7 = base.Manager.GetElement<RectTransform>("CardImage2").DOAnchorPos3D(new Vector3(0f, -40f, 0f), 0.24f, false).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween7);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween8 = base.Manager.GetElement<RectTransform>("CardImage0").DOLocalRotate(new Vector3(0f, 0f, -20f), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween8);
			TweenerCore<Quaternion, Vector3, QuaternionOptions> tween9 = base.Manager.GetElement<RectTransform>("CardImage2").DOLocalRotate(new Vector3(0f, 0f, 20f), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic);
			this.pickdownTweens.Add(tween9);
		}

		// Token: 0x06009292 RID: 37522 RVA: 0x00148AA4 File Offset: 0x00146CA4
		public void ShowToggle()
		{
			if (this.index == 0)
			{
				return;
			}
			this.ApplyShowToggle();
		}

		// Token: 0x06009293 RID: 37523 RVA: 0x00148AB8 File Offset: 0x00146CB8
		private void ApplyShowToggle()
		{
			this.toggleMode = true;
			this.isOn = false;
			Program.instance.deckSelector.GetUI<DeckSelectorUI>().superScrollView.items[this.index].args[6] = "0";
			base.Manager.GetElement("SelectedStateToggle").SetActive(true);
			base.Manager.GetElement("IconOn").SetActive(false);
		}

		// Token: 0x06009294 RID: 37524 RVA: 0x00148B2F File Offset: 0x00146D2F
		public void HideToggle()
		{
			this.ApplyHideToggle();
		}

		// Token: 0x06009295 RID: 37525 RVA: 0x00148B37 File Offset: 0x00146D37
		private void ApplyHideToggle()
		{
			this.toggleMode = false;
			base.Manager.GetElement("SelectedStateToggle").SetActive(false);
			base.Manager.GetElement("IconOn").SetActive(false);
			this.isOn = false;
		}

		// Token: 0x06009296 RID: 37526 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ToggleOnNow()
		{
		}

		// Token: 0x06009297 RID: 37527 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ToggleOffNow()
		{
		}

		// Token: 0x06009298 RID: 37528 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ToggleOn()
		{
		}

		// Token: 0x06009299 RID: 37529 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ToggleOff()
		{
		}

		// Token: 0x0600929A RID: 37530 RVA: 0x00148B73 File Offset: 0x00146D73
		protected override void HoverOn()
		{
			base.HoverOn();
			this.ShowPickup(false);
		}

		// Token: 0x0600929B RID: 37531 RVA: 0x00148B82 File Offset: 0x00146D82
		protected override void HoverOff(bool force = false)
		{
			base.HoverOff(false);
			this.HidePickup(false);
		}

		// Token: 0x0600929C RID: 37532 RVA: 0x00148B92 File Offset: 0x00146D92
		protected override int GetButtonsCount()
		{
			return Program.instance.deckSelector.GetUI<DeckSelectorUI>().superScrollView.items.Count;
		}

		// Token: 0x0600929D RID: 37533 RVA: 0x00148BB2 File Offset: 0x00146DB2
		protected override int GetColumnsCount()
		{
			return Program.instance.deckSelector.GetUI<DeckSelectorUI>().superScrollView.GetColumnCount();
		}

		// Token: 0x0400D0E9 RID: 53481
		[HideInInspector]
		public string deckName;

		// Token: 0x0400D0EA RID: 53482
		[HideInInspector]
		public int deckCase;

		// Token: 0x0400D0EB RID: 53483
		[HideInInspector]
		public int card0;

		// Token: 0x0400D0EC RID: 53484
		[HideInInspector]
		public int card1;

		// Token: 0x0400D0ED RID: 53485
		[HideInInspector]
		public int card2;

		// Token: 0x0400D0EE RID: 53486
		[HideInInspector]
		public string protector;

		// Token: 0x0400D0EF RID: 53487
		[HideInInspector]
		public bool toggleMode;

		// Token: 0x0400D0F0 RID: 53488
		protected bool pickuping;

		// Token: 0x0400D0F1 RID: 53489
		protected bool forcedPickup;

		// Token: 0x0400D0F2 RID: 53490
		protected IEnumerator enumeratorCase;

		// Token: 0x0400D0F3 RID: 53491
		private readonly List<Tweener> pickupTweens = new List<Tweener>();

		// Token: 0x0400D0F4 RID: 53492
		private readonly List<Tweener> pickdownTweens = new List<Tweener>();
	}
}
