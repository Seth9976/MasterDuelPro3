using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200006D RID: 109
	[AddComponentMenu("UI/Slider", 34)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Slider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00014635 File Offset: 0x00012835
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0001463D File Offset: 0x0001283D
		public RectTransform fillRect
		{
			get
			{
				return this.m_FillRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_FillRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00014659 File Offset: 0x00012859
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00014661 File Offset: 0x00012861
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001467D File Offset: 0x0001287D
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00014685 File Offset: 0x00012885
		public Slider.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Slider.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001469B File Offset: 0x0001289B
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x000146A3 File Offset: 0x000128A3
		public float minValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000146C6 File Offset: 0x000128C6
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x000146CE File Offset: 0x000128CE
		public float maxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000146F1 File Offset: 0x000128F1
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x000146F9 File Offset: 0x000128F9
		public bool wholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001471C File Offset: 0x0001291C
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00014738 File Offset: 0x00012938
		public virtual float value
		{
			get
			{
				if (!this.wholeNumbers)
				{
					return this.m_Value;
				}
				return Mathf.Round(this.m_Value);
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00014742 File Offset: 0x00012942
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001474C File Offset: 0x0001294C
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0001477E File Offset: 0x0001297E
		public float normalizedValue
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
			}
			set
			{
				this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00014798 File Offset: 0x00012998
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000147A0 File Offset: 0x000129A0
		public Slider.SliderEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000147A9 File Offset: 0x000129A9
		private float stepSize
		{
			get
			{
				if (!this.wholeNumbers)
				{
					return (this.maxValue - this.minValue) * 0.1f;
				}
				return 1f;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000147CC File Offset: 0x000129CC
		protected Slider()
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000147F5 File Offset: 0x000129F5
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014816 File Offset: 0x00012A16
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00014829 File Offset: 0x00012A29
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.Set(this.m_Value, false);
				this.UpdateVisuals();
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00014850 File Offset: 0x00012A50
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_Value = this.ClampValue(this.m_Value);
			float oldNormalizedValue = this.normalizedValue;
			if (this.m_FillContainerRect != null)
			{
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					oldNormalizedValue = this.m_FillImage.fillAmount;
				}
				else
				{
					oldNormalizedValue = (this.reverseValue ? (1f - this.m_FillRect.anchorMin[(int)this.axis]) : this.m_FillRect.anchorMax[(int)this.axis]);
				}
			}
			else if (this.m_HandleContainerRect != null)
			{
				oldNormalizedValue = (this.reverseValue ? (1f - this.m_HandleRect.anchorMin[(int)this.axis]) : this.m_HandleRect.anchorMin[(int)this.axis]);
			}
			this.UpdateVisuals();
			if (oldNormalizedValue != this.normalizedValue)
			{
				UISystemProfilerApi.AddMarker("Slider.value", this);
				this.onValueChanged.Invoke(this.m_Value);
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00014978 File Offset: 0x00012B78
		private void UpdateCachedReferences()
		{
			if (this.m_FillRect && this.m_FillRect != (RectTransform)base.transform)
			{
				this.m_FillTransform = this.m_FillRect.transform;
				this.m_FillImage = this.m_FillRect.GetComponent<Image>();
				if (this.m_FillTransform.parent != null)
				{
					this.m_FillContainerRect = this.m_FillTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_FillRect = null;
				this.m_FillContainerRect = null;
				this.m_FillImage = null;
			}
			if (this.m_HandleRect && this.m_HandleRect != (RectTransform)base.transform)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
					return;
				}
			}
			else
			{
				this.m_HandleRect = null;
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014A7C File Offset: 0x00012C7C
		private float ClampValue(float input)
		{
			float newValue = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				newValue = Mathf.Round(newValue);
			}
			return newValue;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014AAC File Offset: 0x00012CAC
		protected virtual void Set(float input, bool sendCallback = true)
		{
			float newValue = this.ClampValue(input);
			if (this.m_Value == newValue)
			{
				return;
			}
			this.m_Value = newValue;
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Slider.value", this);
				this.m_OnValueChanged.Invoke(newValue);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014AF2 File Offset: 0x00012CF2
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00014B09 File Offset: 0x00012D09
		private Slider.Axis axis
		{
			get
			{
				if (this.m_Direction != Slider.Direction.LeftToRight && this.m_Direction != Slider.Direction.RightToLeft)
				{
					return Slider.Axis.Vertical;
				}
				return Slider.Axis.Horizontal;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00014B1F File Offset: 0x00012D1F
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Slider.Direction.RightToLeft || this.m_Direction == Slider.Direction.TopToBottom;
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00014B38 File Offset: 0x00012D38
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_FillContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_FillRect, DrivenTransformProperties.Anchors);
				Vector2 anchorMin = Vector2.zero;
				Vector2 anchorMax = Vector2.one;
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					this.m_FillImage.fillAmount = this.normalizedValue;
				}
				else if (this.reverseValue)
				{
					anchorMin[(int)this.axis] = 1f - this.normalizedValue;
				}
				else
				{
					anchorMax[(int)this.axis] = this.normalizedValue;
				}
				this.m_FillRect.anchorMin = anchorMin;
				this.m_FillRect.anchorMax = anchorMax;
			}
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 anchorMin2 = Vector2.zero;
				Vector2 anchorMax2 = Vector2.one;
				anchorMin2[(int)this.axis] = (anchorMax2[(int)this.axis] = (this.reverseValue ? (1f - this.normalizedValue) : this.normalizedValue));
				this.m_HandleRect.anchorMin = anchorMin2;
				this.m_HandleRect.anchorMax = anchorMax2;
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00014C88 File Offset: 0x00012E88
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform clickRect = this.m_HandleContainerRect ?? this.m_FillContainerRect;
			if (clickRect != null && clickRect.rect.size[(int)this.axis] > 0f)
			{
				Vector2 position = Vector2.zero;
				if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position))
				{
					return;
				}
				Vector2 localCursor;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(clickRect, position, cam, out localCursor))
				{
					return;
				}
				localCursor -= clickRect.rect.position;
				float val = Mathf.Clamp01((localCursor - this.m_Offset)[(int)this.axis] / clickRect.rect.size[(int)this.axis]);
				this.normalizedValue = (this.reverseValue ? (1f - val) : val);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013402 File Offset: 0x00011602
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00014D68 File Offset: 0x00012F68
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera))
			{
				Vector2 localMousePos;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out localMousePos))
				{
					this.m_Offset = localMousePos;
					return;
				}
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00014DF2 File Offset: 0x00012FF2
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00014E0C File Offset: 0x0001300C
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00014F78 File Offset: 0x00013178
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00014FA8 File Offset: 0x000131A8
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014FD8 File Offset: 0x000131D8
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00015008 File Offset: 0x00013208
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001379B File Offset: 0x0001199B
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015038 File Offset: 0x00013238
		public void SetDirection(Slider.Direction direction, bool includeRectLayouts)
		{
			Slider.Axis oldAxis = this.axis;
			bool oldReverse = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != oldAxis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != oldReverse)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00006250 File Offset: 0x00004450
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000220 RID: 544
		[SerializeField]
		private RectTransform m_FillRect;

		// Token: 0x04000221 RID: 545
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x04000222 RID: 546
		[Space]
		[SerializeField]
		private Slider.Direction m_Direction;

		// Token: 0x04000223 RID: 547
		[SerializeField]
		private float m_MinValue;

		// Token: 0x04000224 RID: 548
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x04000225 RID: 549
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x04000226 RID: 550
		[SerializeField]
		protected float m_Value;

		// Token: 0x04000227 RID: 551
		[Space]
		[SerializeField]
		private Slider.SliderEvent m_OnValueChanged = new Slider.SliderEvent();

		// Token: 0x04000228 RID: 552
		private Image m_FillImage;

		// Token: 0x04000229 RID: 553
		private Transform m_FillTransform;

		// Token: 0x0400022A RID: 554
		private RectTransform m_FillContainerRect;

		// Token: 0x0400022B RID: 555
		private Transform m_HandleTransform;

		// Token: 0x0400022C RID: 556
		private RectTransform m_HandleContainerRect;

		// Token: 0x0400022D RID: 557
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x0400022E RID: 558
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x0400022F RID: 559
		private bool m_DelayedUpdateVisuals;

		// Token: 0x0200006E RID: 110
		public enum Direction
		{
			// Token: 0x04000231 RID: 561
			LeftToRight,
			// Token: 0x04000232 RID: 562
			RightToLeft,
			// Token: 0x04000233 RID: 563
			BottomToTop,
			// Token: 0x04000234 RID: 564
			TopToBottom
		}

		// Token: 0x0200006F RID: 111
		[Serializable]
		public class SliderEvent : UnityEvent<float>
		{
		}

		// Token: 0x02000070 RID: 112
		private enum Axis
		{
			// Token: 0x04000236 RID: 566
			Horizontal,
			// Token: 0x04000237 RID: 567
			Vertical
		}
	}
}
