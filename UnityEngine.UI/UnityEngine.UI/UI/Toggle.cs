using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000075 RID: 117
	[AddComponentMenu("UI/Toggle", 30)]
	[RequireComponent(typeof(RectTransform))]
	public class Toggle : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00015F6E File Offset: 0x0001416E
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00015F76 File Offset: 0x00014176
		public ToggleGroup group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				this.SetToggleGroup(value, true);
				this.PlayEffect(true);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00015F87 File Offset: 0x00014187
		protected Toggle()
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00015FA1 File Offset: 0x000141A1
		protected override void OnDestroy()
		{
			if (this.m_Group != null)
			{
				this.m_Group.EnsureValidState();
			}
			base.OnDestroy();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015FC2 File Offset: 0x000141C2
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetToggleGroup(this.m_Group, false);
			this.PlayEffect(true);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00015FDE File Offset: 0x000141DE
		protected override void OnDisable()
		{
			this.SetToggleGroup(null, false);
			base.OnDisable();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015FF0 File Offset: 0x000141F0
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				bool oldValue = !Mathf.Approximately(this.graphic.canvasRenderer.GetColor().a, 0f);
				if (this.m_IsOn != oldValue)
				{
					this.m_IsOn = oldValue;
					this.Set(!oldValue, true);
				}
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00016050 File Offset: 0x00014250
		private void SetToggleGroup(ToggleGroup newGroup, bool setMemberValue)
		{
			if (this.m_Group != null)
			{
				this.m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				this.m_Group = newGroup;
			}
			if (newGroup != null && this.IsActive())
			{
				newGroup.RegisterToggle(this);
			}
			if (newGroup != null && this.isOn && this.IsActive())
			{
				newGroup.NotifyToggleOn(this, true);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000160BA File Offset: 0x000142BA
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x000160C2 File Offset: 0x000142C2
		public bool isOn
		{
			get
			{
				return this.m_IsOn;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000160CC File Offset: 0x000142CC
		public void SetIsOnWithoutNotify(bool value)
		{
			this.Set(value, false);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000160D8 File Offset: 0x000142D8
		private void Set(bool value, bool sendCallback = true)
		{
			if (this.m_IsOn == value)
			{
				return;
			}
			this.m_IsOn = value;
			if (this.m_Group != null && this.m_Group.isActiveAndEnabled && this.IsActive() && (this.m_IsOn || (!this.m_Group.AnyTogglesOn() && !this.m_Group.allowSwitchOff)))
			{
				this.m_IsOn = true;
				this.m_Group.NotifyToggleOn(this, sendCallback);
			}
			this.PlayEffect(this.toggleTransition == Toggle.ToggleTransition.None);
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Toggle.value", this);
				this.onValueChanged.Invoke(this.m_IsOn);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001617D File Offset: 0x0001437D
		private void PlayEffect(bool instant)
		{
			if (this.graphic == null)
			{
				return;
			}
			this.graphic.CrossFadeAlpha(this.m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, true);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000161BD File Offset: 0x000143BD
		protected override void Start()
		{
			this.PlayEffect(true);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000161C6 File Offset: 0x000143C6
		private void InternalToggle()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.isOn = !this.isOn;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000161E8 File Offset: 0x000143E8
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.InternalToggle();
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000161F9 File Offset: 0x000143F9
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.InternalToggle();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00006250 File Offset: 0x00004450
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400024E RID: 590
		public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

		// Token: 0x0400024F RID: 591
		public Graphic graphic;

		// Token: 0x04000250 RID: 592
		[SerializeField]
		private ToggleGroup m_Group;

		// Token: 0x04000251 RID: 593
		public Toggle.ToggleEvent onValueChanged = new Toggle.ToggleEvent();

		// Token: 0x04000252 RID: 594
		[Tooltip("Is the toggle currently on or off?")]
		[SerializeField]
		private bool m_IsOn;

		// Token: 0x02000076 RID: 118
		public enum ToggleTransition
		{
			// Token: 0x04000254 RID: 596
			None,
			// Token: 0x04000255 RID: 597
			Fade
		}

		// Token: 0x02000077 RID: 119
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
		}
	}
}
