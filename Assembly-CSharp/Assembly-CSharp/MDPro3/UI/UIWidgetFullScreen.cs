using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001437 RID: 5175
	public class UIWidgetFullScreen : UIWidget
	{
		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x0600967C RID: 38524 RVA: 0x0015D3ED File Offset: 0x0015B5ED
		protected virtual float TransitionTime
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x0600967D RID: 38525 RVA: 0x0015D3F4 File Offset: 0x0015B5F4
		protected virtual Vector3 HideScale
		{
			get
			{
				return new Vector3(0.95f, 0.95f, 1f);
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600967E RID: 38526 RVA: 0x0015D40A File Offset: 0x0015B60A
		protected virtual string Label_SE_Show
		{
			get
			{
				return "SE_DECK_WINDOW_OPEN";
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x0600967F RID: 38527 RVA: 0x0015D411 File Offset: 0x0015B611
		protected virtual string Label_SE_Hide
		{
			get
			{
				return "SE_MENU_CANCEL";
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06009680 RID: 38528 RVA: 0x0000763C File Offset: 0x0000583C
		protected virtual bool DestroyOnHide
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06009681 RID: 38529 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Selectable DefaultSelectable
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06009682 RID: 38530 RVA: 0x0015D418 File Offset: 0x0015B618
		public virtual void Show()
		{
			if (this.showing)
			{
				return;
			}
			this.showing = true;
			this.ShowEvent();
			this.CG.alpha = 0f;
			this.CG.blocksRaycasts = true;
			this.CG.DOFade(1f, this.TransitionTime).SetUpdate(true);
			base.Rect.localScale = this.HideScale;
			base.Rect.DOScale(Vector3.one, this.TransitionTime).SetUpdate(true).SetEase(Ease.OutQuart)
				.OnComplete(new TweenCallback(this.AfterShowEvent));
		}

		// Token: 0x06009683 RID: 38531 RVA: 0x0015D4BC File Offset: 0x0015B6BC
		public virtual void Hide()
		{
			if (!this.showing)
			{
				return;
			}
			this.showing = false;
			this.HideEvent();
			this.CG.DOFade(0f, this.TransitionTime).SetUpdate(true);
			base.Rect.DOScale(this.HideScale, this.TransitionTime).SetUpdate(true).SetEase(Ease.InCubic)
				.OnComplete(delegate
				{
					this.CG.blocksRaycasts = false;
					this.AfterHideEvent();
				});
		}

		// Token: 0x06009684 RID: 38532 RVA: 0x0015D531 File Offset: 0x0015B731
		protected virtual void Update()
		{
			if (!this.NeedResponse())
			{
				return;
			}
			if (UserInput.WasCancelPressed || UserInput.MouseRightDown)
			{
				this.Hide();
			}
		}

		// Token: 0x06009685 RID: 38533 RVA: 0x0015D550 File Offset: 0x0015B750
		protected virtual bool NeedResponse()
		{
			return this.showing && !(UIManager.InputBlocker != this) && !(Program.instance.ui_.currentPopupB != null);
		}

		// Token: 0x06009686 RID: 38534 RVA: 0x0015D585 File Offset: 0x0015B785
		protected virtual void ShowEvent()
		{
			AudioManager.PlaySE(this.Label_SE_Show, 1f);
			this.lastInputBlocker = UIManager.InputBlocker;
			UIManager.InputBlocker = this;
		}

		// Token: 0x06009687 RID: 38535 RVA: 0x0015D5A8 File Offset: 0x0015B7A8
		protected virtual void AfterShowEvent()
		{
			this.Select(false);
		}

		// Token: 0x06009688 RID: 38536 RVA: 0x0015D5B1 File Offset: 0x0015B7B1
		protected virtual void HideEvent()
		{
			AudioManager.PlaySE(this.Label_SE_Hide, 1f);
		}

		// Token: 0x06009689 RID: 38537 RVA: 0x0015D5C4 File Offset: 0x0015B7C4
		protected virtual void AfterHideEvent()
		{
			UIManager.InputBlocker = this.lastInputBlocker;
			if (this.lastInputBlocker == null)
			{
				Program.instance.currentServant.Select(false);
			}
			else
			{
				UIWidgetFullScreen ui = this.lastInputBlocker as UIWidgetFullScreen;
				if (ui != null)
				{
					ui.Select(false);
				}
			}
			if (this.DestroyOnHide)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x0400D4E6 RID: 54502
		[HideInInspector]
		public bool showing;

		// Token: 0x0400D4E7 RID: 54503
		protected MonoBehaviour lastInputBlocker;
	}
}
