using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x02001434 RID: 5172
	public class UIWidget : MonoBehaviour
	{
		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x0600962E RID: 38446 RVA: 0x0015BE90 File Offset: 0x0015A090
		protected ElementObjectManager Manager
		{
			get
			{
				return this.manager = ((this.manager != null) ? this.manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x0600962F RID: 38447 RVA: 0x0015BEC4 File Offset: 0x0015A0C4
		protected virtual CanvasGroup CG
		{
			get
			{
				return this.cg = ((this.cg != null) ? this.cg : base.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06009630 RID: 38448 RVA: 0x0015BEF8 File Offset: 0x0015A0F8
		protected RectTransform Rect
		{
			get
			{
				return this.rect = ((this.rect != null) ? this.rect : base.GetComponent<RectTransform>());
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06009631 RID: 38449 RVA: 0x0015BF2C File Offset: 0x0015A12C
		protected RectTransform Root
		{
			get
			{
				return this.m_Root = ((this.m_Root != null) ? this.m_Root : this.Manager.GetElement<RectTransform>("Root"));
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06009632 RID: 38450 RVA: 0x0015BF68 File Offset: 0x0015A168
		protected CanvasGroup RootCG
		{
			get
			{
				return this.m_RootCG = ((this.m_RootCG != null) ? this.m_RootCG : this.Root.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06009633 RID: 38451 RVA: 0x0015BFA0 File Offset: 0x0015A1A0
		protected RectTransform Window
		{
			get
			{
				return this.m_Window = ((this.m_Window != null) ? this.m_Window : this.Manager.GetElement<RectTransform>("Window"));
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06009634 RID: 38452 RVA: 0x0015BFDC File Offset: 0x0015A1DC
		protected CanvasGroup WindowCG
		{
			get
			{
				return this.m_WindowCG = ((this.m_WindowCG != null) ? this.m_WindowCG : this.Window.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06009635 RID: 38453 RVA: 0x0015C014 File Offset: 0x0015A214
		protected CanvasGroup BG
		{
			get
			{
				return this.m_BG = ((this.m_BG != null) ? this.m_BG : this.Manager.GetElement<CanvasGroup>("BG"));
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06009636 RID: 38454 RVA: 0x0015C050 File Offset: 0x0015A250
		protected CanvasGroup ButtonGroup
		{
			get
			{
				return this.m_ButtonGroup = ((this.m_ButtonGroup != null) ? this.m_ButtonGroup : this.Manager.GetElement<CanvasGroup>("ButtonGroup"));
			}
		}

		// Token: 0x06009637 RID: 38455 RVA: 0x0015C08C File Offset: 0x0015A28C
		protected virtual void Awake()
		{
			if (this.needTranslate)
			{
				UIManager.Translate(base.gameObject);
			}
		}

		// Token: 0x06009638 RID: 38456 RVA: 0x0015C0A1 File Offset: 0x0015A2A1
		public void SetResponse(bool response)
		{
			this.responseInput = response;
		}

		// Token: 0x06009639 RID: 38457 RVA: 0x0015C0AA File Offset: 0x0015A2AA
		public virtual void Select(bool forced = false)
		{
			if ((forced || UserInput.NeedDefaultSelect()) && this.defaultSelectable != null)
			{
				this.defaultSelectable.Select();
			}
		}

		// Token: 0x0400D475 RID: 54389
		private ElementObjectManager manager;

		// Token: 0x0400D476 RID: 54390
		private CanvasGroup cg;

		// Token: 0x0400D477 RID: 54391
		private RectTransform rect;

		// Token: 0x0400D478 RID: 54392
		private const string LABEL_RT_ROOT = "Root";

		// Token: 0x0400D479 RID: 54393
		private RectTransform m_Root;

		// Token: 0x0400D47A RID: 54394
		private CanvasGroup m_RootCG;

		// Token: 0x0400D47B RID: 54395
		private const string LABEL_RT_WINDOW = "Window";

		// Token: 0x0400D47C RID: 54396
		private RectTransform m_Window;

		// Token: 0x0400D47D RID: 54397
		private CanvasGroup m_WindowCG;

		// Token: 0x0400D47E RID: 54398
		protected const string LABEL_CG_BG = "BG";

		// Token: 0x0400D47F RID: 54399
		private CanvasGroup m_BG;

		// Token: 0x0400D480 RID: 54400
		private const string LABEL_CG_BUTTONGROUP = "ButtonGroup";

		// Token: 0x0400D481 RID: 54401
		private CanvasGroup m_ButtonGroup;

		// Token: 0x0400D482 RID: 54402
		[SerializeField]
		protected Selectable defaultSelectable;

		// Token: 0x0400D483 RID: 54403
		[SerializeField]
		protected bool needTranslate = true;

		// Token: 0x0400D484 RID: 54404
		[HideInInspector]
		public bool responseInput;
	}
}
