using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI.Popup
{
	// Token: 0x02001486 RID: 5254
	public class Popup : MonoBehaviour
	{
		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060099E9 RID: 39401 RVA: 0x0016F314 File Offset: 0x0016D514
		protected ElementObjectManager Manager
		{
			get
			{
				if (this.m_Manager == null)
				{
					this.m_Manager = base.GetComponent<ElementObjectManager>();
				}
				return this.m_Manager;
			}
		}

		// Token: 0x060099EA RID: 39402 RVA: 0x0016F338 File Offset: 0x0016D538
		protected virtual void Initialize()
		{
			GameObject closeButton = this.Manager.GetElement("CloseButton");
			if (closeButton != null)
			{
				Button btn;
				if (closeButton.TryGetComponent<Button>(out btn))
				{
					btn.onClick.AddListener(new UnityAction(this.Hide));
				}
				CanvasGroup cg;
				if (closeButton.TryGetComponent<CanvasGroup>(out cg))
				{
					cg.alpha = 0f;
				}
			}
			this.Manager.GetElement<RectTransform>("Content").anchoredPosition = new Vector2(0f, -1080f);
			UIManager.Translate(base.gameObject);
			this.SetButtonsLayout();
			this.lastPopup = Program.instance.ui_.currentPopupB;
			Program.instance.ui_.currentPopupB = this;
			this.InitializeSelections();
		}

		// Token: 0x060099EB RID: 39403 RVA: 0x0016F3F8 File Offset: 0x0016D5F8
		protected virtual void InitializeSelections()
		{
			if (this.Manager.GetElement("TitleText") != null && this.args != null && this.args.Count > 0)
			{
				this.Manager.GetElement<TextMeshProUGUI>("TitleText").text = this.args[0];
			}
		}

		// Token: 0x060099EC RID: 39404 RVA: 0x0016F454 File Offset: 0x0016D654
		public virtual void Show()
		{
			this.Initialize();
			this.Manager.GetElement<RectTransform>("Content").DOAnchorPosY(0f, this.transitionTime, false).SetEase(Ease.OutCubic)
				.OnComplete(delegate
				{
					this.inTransition = false;
					if (Cursor.lockState == CursorLockMode.Locked)
					{
						this.SelectLastSelected();
					}
				});
			if (this.Manager.GetElement("CloseButton") != null)
			{
				this.Manager.GetElement<CanvasGroup>("CloseButton").DOFade(0.8f, this.transitionTime);
			}
		}

		// Token: 0x060099ED RID: 39405 RVA: 0x0016F4DC File Offset: 0x0016D6DC
		public virtual void Hide()
		{
			this.inTransition = true;
			this.Manager.GetElement<RectTransform>("Content").DOAnchorPosY(-1080f, this.transitionTime, false).SetEase(Ease.InCubic)
				.OnComplete(delegate
				{
					global::UnityEngine.Object.Destroy(base.gameObject);
					Program.instance.ui_.currentPopupB = this.lastPopup;
					Action action = this.quitAction;
					if (action != null)
					{
						action();
					}
					if (Cursor.lockState == CursorLockMode.Locked)
					{
						if (this.lastPopup != null)
						{
							this.lastPopup.SelectLastSelected();
							return;
						}
						Program.instance.currentServant.Select(false);
					}
				});
			if (this.Manager.GetElement("CloseButton") != null)
			{
				this.Manager.GetElement<CanvasGroup>("CloseButton").DOFade(0f, this.transitionTime);
			}
		}

		// Token: 0x060099EE RID: 39406 RVA: 0x0016F562 File Offset: 0x0016D762
		protected virtual void OnDecide()
		{
			this.Hide();
		}

		// Token: 0x060099EF RID: 39407 RVA: 0x0016F562 File Offset: 0x0016D762
		protected virtual void OnCancel()
		{
			this.Hide();
		}

		// Token: 0x060099F0 RID: 39408 RVA: 0x0016F56C File Offset: 0x0016D76C
		protected virtual void SetButtonsLayout()
		{
			GameObject btnCancel = this.Manager.GetElement("CancelButton");
			GameObject btnDecide = this.Manager.GetElement("DecideButton");
			if (btnCancel != null)
			{
				btnCancel.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnCancel));
			}
			if (btnDecide != null)
			{
				btnDecide.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnDecide));
			}
			if (btnCancel == null || btnDecide == null)
			{
				return;
			}
			Button buttonCancel = btnCancel.GetComponent<Button>();
			Button buttonDecide = btnDecide.GetComponent<Button>();
			if (Config.GetBool("Confirm", false))
			{
				btnDecide.transform.SetSiblingIndex(1);
				Navigation navigation = buttonCancel.navigation;
				navigation.selectOnRight = null;
				buttonCancel.navigation = navigation;
				navigation = buttonDecide.navigation;
				navigation.selectOnLeft = null;
				buttonDecide.navigation = navigation;
				return;
			}
			Navigation navigation2 = buttonCancel.navigation;
			navigation2.selectOnLeft = null;
			buttonCancel.navigation = navigation2;
			navigation2 = buttonDecide.navigation;
			navigation2.selectOnRight = null;
			buttonDecide.navigation = navigation2;
		}

		// Token: 0x060099F1 RID: 39409 RVA: 0x0016F682 File Offset: 0x0016D882
		protected virtual void SelectLastSelected()
		{
			EventSystem.current.SetSelectedGameObject(this.lastSelectable.gameObject);
		}

		// Token: 0x060099F2 RID: 39410 RVA: 0x0016F699 File Offset: 0x0016D899
		protected virtual void OnEnable()
		{
			UserInput.OnMouseCursorHide += this.SelectLastSelected;
		}

		// Token: 0x060099F3 RID: 39411 RVA: 0x0016F6AD File Offset: 0x0016D8AD
		protected virtual void OnDisable()
		{
			UserInput.OnMouseCursorHide -= this.SelectLastSelected;
		}

		// Token: 0x060099F4 RID: 39412 RVA: 0x0016F6C1 File Offset: 0x0016D8C1
		protected virtual bool NeedResponseInput()
		{
			return !this.inTransition && !(Program.instance.ui_.currentPopupB != this) && !UserInput.InputFieldActivating();
		}

		// Token: 0x060099F5 RID: 39413 RVA: 0x0016F6F0 File Offset: 0x0016D8F0
		protected virtual void Update()
		{
			if (!this.NeedResponseInput())
			{
				return;
			}
			if ((UserInput.MouseRightDown || UserInput.WasCancelPressed) && this.cancelCallHide)
			{
				AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
				this.Hide();
			}
		}

		// Token: 0x0400D79E RID: 55198
		[Header("Popup")]
		public Selectable lastSelectable;

		// Token: 0x0400D79F RID: 55199
		[HideInInspector]
		public List<string> args;

		// Token: 0x0400D7A0 RID: 55200
		[HideInInspector]
		public float transitionTime = 0.2f;

		// Token: 0x0400D7A1 RID: 55201
		public Action quitAction;

		// Token: 0x0400D7A2 RID: 55202
		private ElementObjectManager m_Manager;

		// Token: 0x0400D7A3 RID: 55203
		protected bool inTransition = true;

		// Token: 0x0400D7A4 RID: 55204
		protected bool cancelCallHide = true;

		// Token: 0x0400D7A5 RID: 55205
		private Popup lastPopup;
	}
}
