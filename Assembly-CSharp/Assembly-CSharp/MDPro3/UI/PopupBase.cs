using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Servant;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001387 RID: 4999
	public class PopupBase : MonoBehaviour
	{
		// Token: 0x0600909F RID: 37023 RVA: 0x0013D248 File Offset: 0x0013B448
		public virtual void Initialize()
		{
			if (this.shadow != null)
			{
				this.shadow.color = Color.clear;
			}
			float uiScale = Config.GetUIScale(this.maxUIScale);
			this.window.localScale = Vector3.one * uiScale;
			this.hideY = -540f - this.height * uiScale;
			this.window.anchoredPosition = new Vector2(0f, this.hideY);
			UIManager.Translate(base.gameObject);
			if (this.btnConfirm != null && this.btnCancel != null)
			{
				bool @bool = Config.GetBool("Confirm", false);
				float height = this.btnConfirm.GetComponent<RectTransform>().anchoredPosition.y;
				if (!@bool)
				{
					this.btnConfirm.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.buttomOffest, height);
					this.btnCancel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.buttomOffest, height);
				}
			}
			if (this.btnConfirm != null)
			{
				this.btnConfirm.onClick.AddListener(new UnityAction(this.OnConfirm));
			}
			if (this.btnCancel != null)
			{
				this.btnCancel.onClick.AddListener(new UnityAction(this.OnCancel));
			}
			this.lastPopup = Program.instance.ui_.currentPopup;
			Program.instance.ui_.currentPopup = this;
			this.InitializeSelections();
		}

		// Token: 0x060090A0 RID: 37024 RVA: 0x0013D3C8 File Offset: 0x0013B5C8
		public virtual void InitializeSelections()
		{
			if (this.title != null)
			{
				this.title.text = this.selections[0];
			}
		}

		// Token: 0x060090A1 RID: 37025 RVA: 0x0013D3F0 File Offset: 0x0013B5F0
		public virtual void Show()
		{
			this.Initialize();
			this.window.DOAnchorPos(new Vector2(0f, this.showY), this.transitionTime, false).OnComplete(delegate
			{
				Program.instance.currentServant.returnAction = new Action(this.Hide);
			});
			if (this.shadow != null)
			{
				this.shadow.DOFade(0.8f, this.transitionTime);
				this.shadow.raycastTarget = true;
			}
			if (this.defaultSelect != null)
			{
				EventSystem.current.SetSelectedGameObject(this.defaultSelect.gameObject);
			}
		}

		// Token: 0x060090A2 RID: 37026 RVA: 0x0013D48C File Offset: 0x0013B68C
		public virtual void Hide()
		{
			if (this.shadow != null)
			{
				this.shadow.DOFade(0f, this.transitionTime);
			}
			Servant servant = Program.instance.currentServant;
			this.window.DOAnchorPos(new Vector2(0f, this.hideY), this.transitionTime, false).OnComplete(delegate
			{
				global::UnityEngine.Object.Destroy(this.gameObject);
				servant.returnAction = null;
				Program.instance.ui_.currentPopup = this.lastPopup;
				if (this.lastPopup == null && Cursor.lockState == CursorLockMode.Locked)
				{
					Program.instance.currentServant.Select(false);
				}
				Action action = this.whenQuitDo;
				if (action == null)
				{
					return;
				}
				action();
			});
		}

		// Token: 0x060090A3 RID: 37027 RVA: 0x0013D50F File Offset: 0x0013B70F
		public virtual void OnConfirm()
		{
			AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
		}

		// Token: 0x060090A4 RID: 37028 RVA: 0x0013D520 File Offset: 0x0013B720
		public virtual void OnCancel()
		{
			AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
		}

		// Token: 0x0400CF5C RID: 53084
		[Header("Popup Reference")]
		public Image shadow;

		// Token: 0x0400CF5D RID: 53085
		public RectTransform window;

		// Token: 0x0400CF5E RID: 53086
		public Text title;

		// Token: 0x0400CF5F RID: 53087
		public float transitionTime = 0.3f;

		// Token: 0x0400CF60 RID: 53088
		public Button btnConfirm;

		// Token: 0x0400CF61 RID: 53089
		public Button btnCancel;

		// Token: 0x0400CF62 RID: 53090
		public float buttomOffest = 180f;

		// Token: 0x0400CF63 RID: 53091
		public float maxUIScale = 1.5f;

		// Token: 0x0400CF64 RID: 53092
		public float showY;

		// Token: 0x0400CF65 RID: 53093
		public float height = 195f;

		// Token: 0x0400CF66 RID: 53094
		public Selectable defaultSelect;

		// Token: 0x0400CF67 RID: 53095
		[HideInInspector]
		public List<string> selections;

		// Token: 0x0400CF68 RID: 53096
		public Action whenQuitDo;

		// Token: 0x0400CF69 RID: 53097
		public float hideY;

		// Token: 0x0400CF6A RID: 53098
		protected PopupBase lastPopup;
	}
}
