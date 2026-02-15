using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MDPro3.UI
{
	// Token: 0x020013AF RID: 5039
	public class SelectionToggle : SelectionButton
	{
		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06009205 RID: 37381 RVA: 0x00145B74 File Offset: 0x00143D74
		private GameObject PartOn
		{
			get
			{
				return this.m_PartOn = ((this.m_PartOn != null) ? this.m_PartOn : base.Manager.GetElement("On"));
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06009206 RID: 37382 RVA: 0x00145BB0 File Offset: 0x00143DB0
		private GameObject PartOff
		{
			get
			{
				return this.m_PartOff = ((this.m_PartOff != null) ? this.m_PartOff : base.Manager.GetElement("Off"));
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06009207 RID: 37383 RVA: 0x00145BEC File Offset: 0x00143DEC
		private CanvasGroup HoverOnCG
		{
			get
			{
				return this.m_HoverOnCG = ((this.m_HoverOnCG != null) ? this.m_HoverOnCG : base.Manager.GetElement<CanvasGroup>("HoverOn"));
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06009208 RID: 37384 RVA: 0x00145C28 File Offset: 0x00143E28
		private DOTweenAnimation HoverOnAnimation
		{
			get
			{
				return this.m_HoverOnAnimation = ((this.m_HoverOnAnimation != null) ? this.m_HoverOnAnimation : base.Manager.GetElement<DOTweenAnimation>("HoverOn"));
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06009209 RID: 37385 RVA: 0x00145C64 File Offset: 0x00143E64
		private CanvasGroup HoverOffCG
		{
			get
			{
				return this.m_HoverOffCG = ((this.m_HoverOffCG != null) ? this.m_HoverOffCG : base.Manager.GetElement<CanvasGroup>("HoverOff"));
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x0600920A RID: 37386 RVA: 0x00145CA0 File Offset: 0x00143EA0
		private DOTweenAnimation HoverOffAnimation
		{
			get
			{
				return this.m_HoverOffAnimation = ((this.m_HoverOffAnimation != null) ? this.m_HoverOffAnimation : base.Manager.GetElement<DOTweenAnimation>("HoverOff"));
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x0600920B RID: 37387 RVA: 0x00145CDC File Offset: 0x00143EDC
		private GameObject IconOn
		{
			get
			{
				return this.m_IconOn = ((this.m_IconOn != null) ? this.m_IconOn : base.Manager.GetElement("IconOn"));
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x0600920C RID: 37388 RVA: 0x00145D18 File Offset: 0x00143F18
		private GameObject IconOff
		{
			get
			{
				return this.m_IconOff = ((this.m_IconOff != null) ? this.m_IconOff : base.Manager.GetElement("IconOff"));
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x0600920D RID: 37389 RVA: 0x00145D54 File Offset: 0x00143F54
		private TextMeshProUGUI TextOn
		{
			get
			{
				return this.m_TextOn = ((this.m_TextOn != null) ? this.m_TextOn : base.Manager.GetElement<TextMeshProUGUI>("TextOn"));
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x0600920E RID: 37390 RVA: 0x00145D90 File Offset: 0x00143F90
		private TextMeshProUGUI TextOff
		{
			get
			{
				return this.m_TextOff = ((this.m_TextOff != null) ? this.m_TextOff : base.Manager.GetElement<TextMeshProUGUI>("TextOff"));
			}
		}

		// Token: 0x0600920F RID: 37391 RVA: 0x00145DCC File Offset: 0x00143FCC
		protected override void OnClick()
		{
			if (this.SetToResponser)
			{
				if (Program.instance.ui_.currentPopupB != null)
				{
					Program.instance.ui_.currentPopupB.lastSelectable = base.Selectable;
				}
				else
				{
					Program.instance.currentServant.lastSelectable = base.Selectable;
				}
			}
			if (!this.isOn)
			{
				if (this.canToggleOffSelf)
				{
					AudioManager.PlaySE(this.SoundLabelClickOn, 1f);
				}
				else
				{
					AudioManager.PlaySE(base.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
				}
				this.SetToggleOn(true);
				return;
			}
			if (this.canToggleOffSelf)
			{
				AudioManager.PlaySE(this.SoundLabelClickOff, 1f);
				this.SetToggleOff(true);
				return;
			}
			AudioManager.PlaySE(base.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
			this.CallSubmitEvent();
		}

		// Token: 0x06009210 RID: 37392 RVA: 0x00145EC4 File Offset: 0x001440C4
		protected override void OnSubmit()
		{
			if (!this.isOn)
			{
				if (this.canToggleOffSelf)
				{
					AudioManager.PlaySE(this.SoundLabelClickOn, 1f);
				}
				else
				{
					AudioManager.PlaySE(base.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
				}
				this.SetToggleOn(true);
				return;
			}
			if (this.canToggleOffSelf)
			{
				AudioManager.PlaySE(this.SoundLabelClickOff, 1f);
				this.SetToggleOff(true);
				return;
			}
			AudioManager.PlaySE(base.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
			this.CallSubmitEvent();
		}

		// Token: 0x06009211 RID: 37393 RVA: 0x00145F6B File Offset: 0x0014416B
		protected override void OnSelect(bool playSE)
		{
			base.OnSelect(playSE);
			if (this.toggleWhenSelected)
			{
				this.SetToggleOn(true);
			}
		}

		// Token: 0x06009212 RID: 37394 RVA: 0x00145F84 File Offset: 0x00144184
		protected virtual void ToggleOn()
		{
			if (this.isOn)
			{
				return;
			}
			if (base.SelectCursorOffset != null)
			{
				base.SelectCursorOffset.alpha = 1f;
				base.SelectCursorOffsetAnimation.DORestart();
			}
			if (this.PartOn != null)
			{
				this.PartOn.SetActive(true);
			}
			if (this.PartOff != null)
			{
				this.PartOff.SetActive(false);
			}
			if (this.HoverOnCG != null)
			{
				this.HoverOnCG.alpha = 1f;
				if (this.HoverOnAnimation != null)
				{
					this.HoverOnAnimation.DORestart();
				}
			}
			if (this.IconOn != null)
			{
				this.IconOn.SetActive(true);
			}
			if (this.IconOff != null)
			{
				this.IconOff.SetActive(false);
			}
		}

		// Token: 0x06009213 RID: 37395 RVA: 0x00146064 File Offset: 0x00144264
		protected virtual void ToggleOff()
		{
			if (base.SelectCursorOffset != null)
			{
				base.SelectCursorOffset.alpha = 1f;
				base.SelectCursorOffsetAnimation.DORestart();
			}
			if (this.PartOn != null)
			{
				this.PartOn.SetActive(false);
			}
			if (this.PartOff != null)
			{
				this.PartOff.SetActive(true);
			}
			if (this.HoverOffCG != null)
			{
				this.HoverOffCG.alpha = 1f;
				if (this.HoverOffAnimation != null)
				{
					this.HoverOffAnimation.DORestart();
				}
			}
			if (this.IconOn != null)
			{
				this.IconOn.SetActive(false);
			}
			if (this.IconOff != null)
			{
				this.IconOff.SetActive(true);
			}
		}

		// Token: 0x06009214 RID: 37396 RVA: 0x0014613C File Offset: 0x0014433C
		protected override void HoverOn()
		{
			if (this.hoverd)
			{
				return;
			}
			base.HoverOn();
			if (this.isOn)
			{
				if (this.HoverOnCG != null)
				{
					this.HoverOnCG.alpha = 1f;
					if (this.HoverOnAnimation != null)
					{
						this.HoverOnAnimation.DORestart();
						return;
					}
				}
			}
			else if (this.HoverOffCG != null)
			{
				this.HoverOffCG.alpha = 1f;
				if (this.HoverOffAnimation != null)
				{
					this.HoverOffAnimation.DORestart();
				}
			}
		}

		// Token: 0x06009215 RID: 37397 RVA: 0x001461D0 File Offset: 0x001443D0
		protected override void HoverOff(bool force = false)
		{
			if (!this.hoverd)
			{
				return;
			}
			base.HoverOff(false);
			if (this.isOn)
			{
				if (this.HoverOnCG != null)
				{
					if (this.HoverOnAnimation != null)
					{
						this.HoverOnAnimation.DOPause();
					}
					this.HoverOnCG.alpha = 0f;
					return;
				}
			}
			else if (this.HoverOffCG != null)
			{
				if (this.HoverOffAnimation != null)
				{
					this.HoverOffAnimation.DOPause();
				}
				this.HoverOffCG.alpha = 0f;
			}
		}

		// Token: 0x06009216 RID: 37398 RVA: 0x00146264 File Offset: 0x00144464
		protected virtual void CallToggleOnEvent()
		{
			UnityEvent onToggleOn = this.toggleEvent.onToggleOn;
			if (onToggleOn == null)
			{
				return;
			}
			onToggleOn.Invoke();
		}

		// Token: 0x06009217 RID: 37399 RVA: 0x0014627B File Offset: 0x0014447B
		protected virtual void CallToggleOffEvent()
		{
			UnityEvent onToggleOff = this.toggleEvent.onToggleOff;
			if (onToggleOff == null)
			{
				return;
			}
			onToggleOff.Invoke();
		}

		// Token: 0x06009218 RID: 37400 RVA: 0x00146292 File Offset: 0x00144492
		protected virtual void CallSubmitEvent()
		{
			UnityEvent onSubmit = this.submitEvent.onSubmit;
			if (onSubmit == null)
			{
				return;
			}
			onSubmit.Invoke();
		}

		// Token: 0x06009219 RID: 37401 RVA: 0x001462AC File Offset: 0x001444AC
		public virtual void SetToggleOn(bool callEvent = true)
		{
			this.ToggleOn();
			this.isOn = true;
			if (this.exclusiveToggle)
			{
				for (int i = 0; i < base.transform.parent.childCount; i++)
				{
					SelectionToggle toggle;
					if (base.transform.parent.GetChild(i).TryGetComponent<SelectionToggle>(out toggle) && toggle != this)
					{
						toggle.SetToggleOff(this.exclusiveCallOffEvent);
					}
				}
			}
			if (callEvent)
			{
				this.CallToggleOnEvent();
			}
		}

		// Token: 0x0600921A RID: 37402 RVA: 0x00146321 File Offset: 0x00144521
		public virtual void SetToggleOff(bool callEvent = true)
		{
			this.isOn = false;
			this.ToggleOff();
			if (callEvent)
			{
				this.CallToggleOffEvent();
			}
		}

		// Token: 0x0600921B RID: 37403 RVA: 0x00146339 File Offset: 0x00144539
		public virtual void SwitchToggle()
		{
			if (this.isOn)
			{
				AudioManager.PlaySE(this.SoundLabelClickOff, 1f);
				this.SetToggleOff(true);
				return;
			}
			AudioManager.PlaySE(this.SoundLabelClickOn, 1f);
			this.SetToggleOn(true);
		}

		// Token: 0x0600921C RID: 37404 RVA: 0x00146372 File Offset: 0x00144572
		public virtual void SetToggleOnEvent(UnityAction call)
		{
			this.toggleEvent.onToggleOn.AddListener(call);
		}

		// Token: 0x0600921D RID: 37405 RVA: 0x00146385 File Offset: 0x00144585
		public virtual void SetToggleOffEvent(UnityAction call)
		{
			this.toggleEvent.onToggleOff.AddListener(call);
		}

		// Token: 0x0600921E RID: 37406 RVA: 0x00146398 File Offset: 0x00144598
		public virtual void SetSubmitEvent(UnityAction call)
		{
			this.submitEvent.onSubmit.AddListener(call);
		}

		// Token: 0x0600921F RID: 37407 RVA: 0x001463AC File Offset: 0x001445AC
		public override void SetButtonText(string title)
		{
			base.SetButtonText(title);
			GameObject buttonTextOn = base.Manager.GetElement("TextOn");
			TextMeshProUGUI textOn;
			if (buttonTextOn != null && buttonTextOn.TryGetComponent<TextMeshProUGUI>(out textOn))
			{
				textOn.text = title;
			}
			GameObject buttonTextOff = base.Manager.GetElement("TextOff");
			TextMeshProUGUI textOff;
			if (buttonTextOff != null && buttonTextOff.TryGetComponent<TextMeshProUGUI>(out textOff))
			{
				textOff.text = title;
			}
		}

		// Token: 0x06009220 RID: 37408 RVA: 0x00146418 File Offset: 0x00144618
		public virtual void SetToggleText(string titleOn, string titleOff)
		{
			GameObject buttonTextOn = base.Manager.GetElement("TextOn");
			TextMeshProUGUI textOn;
			if (buttonTextOn != null && buttonTextOn.TryGetComponent<TextMeshProUGUI>(out textOn))
			{
				textOn.text = titleOn;
			}
			GameObject buttonTextOff = base.Manager.GetElement("TextOff");
			TextMeshProUGUI textOff;
			if (buttonTextOff != null && buttonTextOff.TryGetComponent<TextMeshProUGUI>(out textOff))
			{
				textOff.text = titleOff;
			}
		}

		// Token: 0x06009221 RID: 37409 RVA: 0x0014647C File Offset: 0x0014467C
		public override string GetButtonText()
		{
			string returnValue = base.GetButtonText();
			if (returnValue != string.Empty)
			{
				return returnValue;
			}
			if (this.TextOn != null && this.TextOn.text != string.Empty)
			{
				return this.TextOn.text;
			}
			if (this.TextOff != null && this.TextOff.text != string.Empty)
			{
				return this.TextOff.text;
			}
			return returnValue;
		}

		// Token: 0x0400D09B RID: 53403
		private const string LABEL_GO_ON = "On";

		// Token: 0x0400D09C RID: 53404
		private GameObject m_PartOn;

		// Token: 0x0400D09D RID: 53405
		private const string LABEL_GO_OFF = "Off";

		// Token: 0x0400D09E RID: 53406
		private GameObject m_PartOff;

		// Token: 0x0400D09F RID: 53407
		private const string LABEL_CG_HoverOn = "HoverOn";

		// Token: 0x0400D0A0 RID: 53408
		private CanvasGroup m_HoverOnCG;

		// Token: 0x0400D0A1 RID: 53409
		private DOTweenAnimation m_HoverOnAnimation;

		// Token: 0x0400D0A2 RID: 53410
		private const string LABEL_CG_HoverOff = "HoverOff";

		// Token: 0x0400D0A3 RID: 53411
		private CanvasGroup m_HoverOffCG;

		// Token: 0x0400D0A4 RID: 53412
		private DOTweenAnimation m_HoverOffAnimation;

		// Token: 0x0400D0A5 RID: 53413
		private const string LABEL_GO_ICONON = "IconOn";

		// Token: 0x0400D0A6 RID: 53414
		private GameObject m_IconOn;

		// Token: 0x0400D0A7 RID: 53415
		private const string LABEL_GO_ICONOFF = "IconOff";

		// Token: 0x0400D0A8 RID: 53416
		private GameObject m_IconOff;

		// Token: 0x0400D0A9 RID: 53417
		private const string LABEL_TXT_ON = "TextOn";

		// Token: 0x0400D0AA RID: 53418
		private TextMeshProUGUI m_TextOn;

		// Token: 0x0400D0AB RID: 53419
		private const string LABEL_TXT_OFF = "TextOff";

		// Token: 0x0400D0AC RID: 53420
		private TextMeshProUGUI m_TextOff;

		// Token: 0x0400D0AD RID: 53421
		[Header("Selection Toggle")]
		[SerializeField]
		protected string SoundLabelClickOn;

		// Token: 0x0400D0AE RID: 53422
		[SerializeField]
		protected string SoundLabelClickOff;

		// Token: 0x0400D0AF RID: 53423
		[SerializeField]
		protected SelectionToggleEvent toggleEvent;

		// Token: 0x0400D0B0 RID: 53424
		[SerializeField]
		protected SelectionSubmitEvent submitEvent;

		// Token: 0x0400D0B1 RID: 53425
		[HideInInspector]
		public bool isOn;

		// Token: 0x0400D0B2 RID: 53426
		protected bool exclusiveToggle;

		// Token: 0x0400D0B3 RID: 53427
		protected bool exclusiveCallOffEvent = true;

		// Token: 0x0400D0B4 RID: 53428
		protected bool canToggleOffSelf = true;

		// Token: 0x0400D0B5 RID: 53429
		protected bool toggleWhenSelected;
	}
}
