using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x0200142F RID: 5167
	public class SidePanel : UIWidget
	{
		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06009608 RID: 38408 RVA: 0x0015B5EC File Offset: 0x001597EC
		protected SelectionButton ButtonBG
		{
			get
			{
				return this.m_ButtonBG = ((this.m_ButtonBG != null) ? this.m_ButtonBG : base.Manager.GetElement<SelectionButton>("BG"));
			}
		}

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06009609 RID: 38409 RVA: 0x0015B628 File Offset: 0x00159828
		protected virtual float TransitionTime
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x0600960A RID: 38410 RVA: 0x0015B630 File Offset: 0x00159830
		protected virtual float Width
		{
			get
			{
				return base.Window.rect.width;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x0600960B RID: 38411 RVA: 0x000029CC File Offset: 0x00000BCC
		protected virtual bool Permanent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x0600960C RID: 38412 RVA: 0x0000763C File Offset: 0x0000583C
		protected virtual bool TakeOverInput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600960D RID: 38413 RVA: 0x0015B650 File Offset: 0x00159850
		protected override void Awake()
		{
			if (this.ButtonBG != null)
			{
				this.ButtonBG.SetClickEvent(delegate
				{
					this.Hide();
				});
			}
			if (this.Permanent)
			{
				base.gameObject.SetActive(false);
				if (this.CG != null)
				{
					this.CG.alpha = 1f;
				}
			}
		}

		// Token: 0x0600960E RID: 38414 RVA: 0x0015B6B4 File Offset: 0x001598B4
		protected virtual void OnEnable()
		{
			UserInput.OnMouseCursorHide += this.OnMouseCursorHide;
		}

		// Token: 0x0600960F RID: 38415 RVA: 0x0015B6C8 File Offset: 0x001598C8
		protected virtual void OnDisable()
		{
			UserInput.OnMouseCursorHide -= this.OnMouseCursorHide;
		}

		// Token: 0x06009610 RID: 38416 RVA: 0x0015B6DC File Offset: 0x001598DC
		protected virtual void Update()
		{
			if (!this.NeedResponse())
			{
				return;
			}
			if (UserInput.WasCancelPressed || UserInput.MouseRightDown)
			{
				this.HideWithSound();
			}
		}

		// Token: 0x06009611 RID: 38417 RVA: 0x0015B6FB File Offset: 0x001598FB
		protected virtual bool NeedResponse()
		{
			return this.showing && this.TakeOverInput && Program.instance.ui_.currentSidePanel == this;
		}

		// Token: 0x06009612 RID: 38418 RVA: 0x0015B726 File Offset: 0x00159926
		protected virtual void OnMouseCursorHide()
		{
			if (this.showing && this.NeedResponse())
			{
				this.Select(false);
			}
		}

		// Token: 0x06009613 RID: 38419 RVA: 0x0015B740 File Offset: 0x00159940
		public virtual void Show()
		{
			if (this.showing || this.shifting)
			{
				return;
			}
			this.showing = true;
			this.shifting = true;
			AudioManager.PlaySE("SE_MENU_SLIDE_01", 1f);
			if (this.Permanent)
			{
				base.gameObject.SetActive(true);
			}
			base.Window.DOAnchorPosX(0f, this.TransitionTime, false).SetEase(Ease.OutQuart).OnComplete(delegate
			{
				this.shifting = false;
				this.ShowCompleteCallback();
			});
			if (base.BG != null)
			{
				base.BG.DOFade(1f, this.TransitionTime);
				base.BG.blocksRaycasts = true;
			}
		}

		// Token: 0x06009614 RID: 38420 RVA: 0x0015B7F0 File Offset: 0x001599F0
		protected virtual void ShowCompleteCallback()
		{
			if (this.TakeOverInput)
			{
				this.lastSidePanel = Program.instance.ui_.currentSidePanel;
				Program.instance.ui_.currentSidePanel = this;
			}
		}

		// Token: 0x06009615 RID: 38421 RVA: 0x0015B820 File Offset: 0x00159A20
		public virtual void Hide()
		{
			if (!this.showing || this.shifting)
			{
				return;
			}
			this.showing = false;
			this.shifting = true;
			base.Window.DOAnchorPosX(this.Width + SafeAreaAdapter.GetSafeAreaRightOffset(), this.TransitionTime, false).SetEase(Ease.OutQuart).OnComplete(delegate
			{
				this.shifting = false;
				this.HideCompleteCallback();
			});
			if (base.BG != null)
			{
				base.BG.DOFade(0f, this.TransitionTime).OnComplete(delegate
				{
					base.BG.blocksRaycasts = false;
				});
			}
		}

		// Token: 0x06009616 RID: 38422 RVA: 0x0015B8BC File Offset: 0x00159ABC
		protected virtual void HideCompleteCallback()
		{
			if (this.TakeOverInput && Program.instance.ui_.currentSidePanel == this)
			{
				Program.instance.ui_.currentSidePanel = null;
				Program.instance.currentServant.Select(false);
			}
			if (this.Permanent)
			{
				base.gameObject.SetActive(false);
				return;
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06009617 RID: 38423 RVA: 0x0015B928 File Offset: 0x00159B28
		protected virtual void HideWithSound()
		{
			AudioManager.PlaySE("SE_MENU_SLIDE_02", 1f);
			this.Hide();
		}

		// Token: 0x0400D45D RID: 54365
		protected SelectionButton m_ButtonBG;

		// Token: 0x0400D45E RID: 54366
		[HideInInspector]
		public bool showing;

		// Token: 0x0400D45F RID: 54367
		protected SidePanel lastSidePanel;

		// Token: 0x0400D460 RID: 54368
		protected bool shifting;
	}
}
