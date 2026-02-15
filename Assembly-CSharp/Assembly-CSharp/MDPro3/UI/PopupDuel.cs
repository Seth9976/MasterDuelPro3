using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200138A RID: 5002
	public class PopupDuel : PopupBase
	{
		// Token: 0x060090AD RID: 37037 RVA: 0x0013D654 File Offset: 0x0013B854
		public override void Initialize()
		{
			base.Initialize();
			Program.instance.ocgcore.allGameObjects.Add(base.gameObject);
			if (this.btnHide != null)
			{
				this.btnHide.onClick.AddListener(new UnityAction(this.FieldView));
			}
			Program.instance.ocgcore.currentPopup = this;
			if (!this.exitable)
			{
				if (this.btnHide != null)
				{
					Program.instance.ocgcore.returnAction = new Action(this.FieldView);
				}
				else
				{
					Program.instance.ocgcore.returnAction = delegate
					{
					};
				}
			}
			float uiScale = Config.GetUIScale(this.maxUIScale);
			this.hideY = -540f - this.height * uiScale - this.extraHeight;
			this.window.anchoredPosition = new Vector2(0f, this.hideY);
		}

		// Token: 0x060090AE RID: 37038 RVA: 0x0013D760 File Offset: 0x0013B960
		public virtual void FieldView()
		{
			if (this.hided)
			{
				this.hided = false;
				this.window.DOAnchorPosY(this.showY, this.transitionTime, false);
				AudioManager.PlaySE("SE_MENU_SLIDE_03", 1f);
				this.btnHide.transform.GetChild(0).gameObject.SetActive(false);
				return;
			}
			this.hided = true;
			float scale = this.window.transform.localScale.x;
			float targetY = -540f - this.height * scale;
			this.window.DOAnchorPosY(targetY, this.transitionTime, false);
			AudioManager.PlaySE("SE_MENU_SLIDE_04", 1f);
			this.btnHide.transform.GetChild(0).gameObject.SetActive(true);
		}

		// Token: 0x060090AF RID: 37039 RVA: 0x0013D82C File Offset: 0x0013BA2C
		public override void Show()
		{
			this.Initialize();
			this.window.DOAnchorPos(new Vector2(0f, this.showY), this.transitionTime, false);
			if (this.shadow != null)
			{
				this.shadow.DOFade(0.8f, this.transitionTime);
				this.shadow.raycastTarget = true;
			}
		}

		// Token: 0x060090B0 RID: 37040 RVA: 0x0013D894 File Offset: 0x0013BA94
		public override void Hide()
		{
			if (this.shadow != null)
			{
				this.shadow.DOFade(0f, this.transitionTime);
			}
			this.window.DOAnchorPos(new Vector2(0f, this.hideY), this.transitionTime, false).OnComplete(delegate
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

		// Token: 0x060090B1 RID: 37041 RVA: 0x0013D90A File Offset: 0x0013BB0A
		public override void OnConfirm()
		{
			AudioManager.PlaySE("SE_DUEL_DECIDE", 1f);
		}

		// Token: 0x060090B2 RID: 37042 RVA: 0x0013D91B File Offset: 0x0013BB1B
		public override void OnCancel()
		{
			AudioManager.PlaySE("SE_DUEL_CANCEL", 1f);
		}

		// Token: 0x060090B3 RID: 37043 RVA: 0x0013D92C File Offset: 0x0013BB2C
		public void OnDestroy()
		{
			Program.instance.ocgcore.returnAction = null;
		}

		// Token: 0x060090B4 RID: 37044 RVA: 0x0013D93E File Offset: 0x0013BB3E
		protected void SendReturn(byte[] buffer)
		{
			Program.instance.ocgcore.SendReturn(buffer, this.transitionTime);
		}

		// Token: 0x0400CF6E RID: 53102
		[Header("Popup Duel Reference")]
		public Button btnHide;

		// Token: 0x0400CF6F RID: 53103
		[HideInInspector]
		public bool exitable;

		// Token: 0x0400CF70 RID: 53104
		private bool hided;

		// Token: 0x0400CF71 RID: 53105
		public float extraHeight = 65f;
	}
}
