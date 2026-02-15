using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace MDPro3.UI
{
	// Token: 0x020013C5 RID: 5061
	public class SelectionToggle_DeckOnline : SelectionToggle_Deck
	{
		// Token: 0x060092A7 RID: 37543 RVA: 0x001493E0 File Offset: 0x001475E0
		public override void Refresh()
		{
			base.Manager.GetElement("DeckCaseIcon").SetActive(true);
			base.Manager.GetElement("TextDeckName").SetActive(true);
			base.Manager.GetElement("IconAddDeck").SetActive(false);
			base.Manager.GetElement("SelectedStateToggle").SetActive(false);
			base.Manager.GetElement<TextMeshProUGUI>("TextDeckName").text = this.deckName;
			base.Manager.GetElement<TextMeshProUGUI>("TextDeckAuthor").text = "by " + this.deckAuthor;
			base.Manager.GetElement<TextMeshProUGUI>("TextDeckDate").text = this.lastDate;
			base.Manager.GetElement<TextMeshProUGUI>("TextDeckLike").text = this.like.ToString();
			this.RefreshDeckCaseAsync();
			this.refreshed = false;
			if (this.pickuping)
			{
				this.StartRefresh();
			}
		}

		// Token: 0x060092A8 RID: 37544 RVA: 0x001494DC File Offset: 0x001476DC
		protected override async UniTask RefreshAsync()
		{
			await UniTask.WaitWhile(() => Program.instance.onlineDeckViewer.inTransition, PlayerLoopTiming.Update, default(CancellationToken), false);
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

		// Token: 0x060092A9 RID: 37545 RVA: 0x00149520 File Offset: 0x00147720
		protected override async UniTask RefreshDeckCaseAsync()
		{
			await UniTask.WaitWhile(() => Program.instance.deckSelector.inTransition, PlayerLoopTiming.Update, default(CancellationToken), false);
			for (int i = 0; i < base.transform.GetSiblingIndex(); i++)
			{
				await UniTask.Yield();
			}
			Sprite sprite = await Program.items.LoadDeckCaseIconAsync(this.deckCase, "_L_SD");
			if (sprite != null)
			{
				base.Manager.GetElement<Image>("DeckImage").sprite = sprite;
			}
		}

		// Token: 0x060092AA RID: 37546 RVA: 0x00149564 File Offset: 0x00147764
		protected override void OnClick()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			DeckEditor.onlineDeckID = this.deckId;
			Program.instance.deckEditor.SwitchCondition(DeckEditor.Condition.OnlineDeck, this.deckName, null);
			Program.instance.ShiftToServant(Program.instance.deckEditor);
		}

		// Token: 0x060092AB RID: 37547 RVA: 0x001495B8 File Offset: 0x001477B8
		protected override void OnSelect(bool playSE)
		{
			this.HoverOn();
			if (playSE)
			{
				AudioManager.PlaySE(this.SoundLabelSelectedGamePad, 1f);
			}
			Program.instance.currentServant.lastSelectable = base.Selectable;
			Program.instance.onlineDeckViewer.lastSelectedDeckItem = this;
			base.SetColor(ColorContainer.SelectMode.Selected, this.hovering ? ColorContainer.StatusMode.Enter : ColorContainer.StatusMode.Normal, base.Selectable.interactable);
		}

		// Token: 0x060092AC RID: 37548 RVA: 0x00149621 File Offset: 0x00147821
		public override void ShowPickup(bool forced = false)
		{
			if (forced)
			{
				this.forcedPickup = true;
			}
			if (this.pickuping)
			{
				return;
			}
			this.pickuping = true;
			this.ApplyShowPickup();
		}

		// Token: 0x060092AD RID: 37549 RVA: 0x00149643 File Offset: 0x00147843
		public override void HidePickup(bool forced = false)
		{
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

		// Token: 0x060092AE RID: 37550 RVA: 0x0014966F File Offset: 0x0014786F
		protected override int GetButtonsCount()
		{
			return Program.instance.onlineDeckViewer.GetUI<OnlineDeckViewerUI>().superScrollView.items.Count;
		}

		// Token: 0x060092AF RID: 37551 RVA: 0x0014968F File Offset: 0x0014788F
		protected override int GetColumnsCount()
		{
			return Program.instance.onlineDeckViewer.GetUI<OnlineDeckViewerUI>().superScrollView.GetColumnCount();
		}

		// Token: 0x0400D109 RID: 53513
		[HideInInspector]
		public string deckAuthor;

		// Token: 0x0400D10A RID: 53514
		[HideInInspector]
		public string deckId;

		// Token: 0x0400D10B RID: 53515
		[HideInInspector]
		public string lastDate;

		// Token: 0x0400D10C RID: 53516
		[HideInInspector]
		public int like;
	}
}
