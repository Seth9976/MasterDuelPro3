using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000064 RID: 100
	[AddComponentMenu("UI/Scrollbar", 36)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Scrollbar : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00012F4A File Offset: 0x0001114A
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00012F52 File Offset: 0x00011152
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00012F6E File Offset: 0x0001116E
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00012F76 File Offset: 0x00011176
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00012F8C File Offset: 0x0001118C
		protected Scrollbar()
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00012FB8 File Offset: 0x000111B8
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00012FF1 File Offset: 0x000111F1
		public float value
		{
			get
			{
				float val = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					val = Mathf.Round(val * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return val;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00012FFB File Offset: 0x000111FB
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00013005 File Offset: 0x00011205
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0001300D File Offset: 0x0001120D
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00013028 File Offset: 0x00011228
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00013030 File Offset: 0x00011230
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013053 File Offset: 0x00011253
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0001305B File Offset: 0x0001125B
		public Scrollbar.ScrollEvent onValueChanged
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00013064 File Offset: 0x00011264
		private float stepSize
		{
			get
			{
				if (this.m_NumberOfSteps <= 1)
				{
					return 0.1f;
				}
				return 1f / (float)(this.m_NumberOfSteps - 1);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00013084 File Offset: 0x00011284
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000130A5 File Offset: 0x000112A5
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000130B8 File Offset: 0x000112B8
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.UpdateVisuals();
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000130CF File Offset: 0x000112CF
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
				return;
			}
			this.m_ContainerRect = null;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001310F File Offset: 0x0001130F
		private void Set(float input, bool sendCallback = true)
		{
			float value = this.m_Value;
			this.m_Value = input;
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Scrollbar.value", this);
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001314C File Offset: 0x0001134C
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00013163 File Offset: 0x00011363
		private Scrollbar.Axis axis
		{
			get
			{
				if (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft)
				{
					return Scrollbar.Axis.Vertical;
				}
				return Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00013179 File Offset: 0x00011379
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00013190 File Offset: 0x00011390
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 anchorMin = Vector2.zero;
				Vector2 anchorMax = Vector2.one;
				float movement = Mathf.Clamp01(this.value) * (1f - this.size);
				if (this.reverseValue)
				{
					anchorMin[(int)this.axis] = 1f - movement - this.size;
					anchorMax[(int)this.axis] = 1f - movement;
				}
				else
				{
					anchorMin[(int)this.axis] = movement;
					anchorMax[(int)this.axis] = movement + this.size;
				}
				this.m_HandleRect.anchorMin = anchorMin;
				this.m_HandleRect.anchorMax = anchorMax;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001326C File Offset: 0x0001146C
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 position = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position))
			{
				return;
			}
			this.UpdateDrag(this.m_ContainerRect, position, eventData.pressEventCamera);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000132B8 File Offset: 0x000114B8
		private void UpdateDrag(RectTransform containerRect, Vector2 position, Camera camera)
		{
			Vector2 localCursor;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, position, camera, out localCursor))
			{
				return;
			}
			Vector2 handleCorner = localCursor - this.m_Offset - this.m_ContainerRect.rect.position - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float remainingSize = ((this.axis == Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.width : this.m_ContainerRect.rect.height) * (1f - this.size);
			if (remainingSize <= 0f)
			{
				return;
			}
			this.DoUpdateDrag(handleCorner, remainingSize);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013378 File Offset: 0x00011578
		private void DoUpdateDrag(Vector2 handleCorner, float remainingSize)
		{
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(Mathf.Clamp01(handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.RightToLeft:
				this.Set(Mathf.Clamp01(1f - handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.BottomToTop:
				this.Set(Mathf.Clamp01(handleCorner.y / remainingSize), true);
				return;
			case Scrollbar.Direction.TopToBottom:
				this.Set(Mathf.Clamp01(1f - handleCorner.y / remainingSize), true);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013402 File Offset: 0x00011602
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00013420 File Offset: 0x00011620
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 localMousePos;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out localMousePos))
			{
				this.m_Offset = localMousePos - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000134B5 File Offset: 0x000116B5
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000134D6 File Offset: 0x000116D6
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00013513 File Offset: 0x00011713
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			return this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001352C File Offset: 0x0001172C
		protected IEnumerator ClickRepeat(Vector2 screenPosition, Camera camera)
		{
			while (this.isPointerDownAndNotDragging)
			{
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, screenPosition, camera))
				{
					this.UpdateDrag(this.m_ContainerRect, screenPosition, camera);
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00013549 File Offset: 0x00011749
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001355C File Offset: 0x0001175C
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
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000136DC File Offset: 0x000118DC
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001370C File Offset: 0x0001190C
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001373C File Offset: 0x0001193C
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001376C File Offset: 0x0001196C
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001379B File Offset: 0x0001199B
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000137A4 File Offset: 0x000119A4
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis oldAxis = this.axis;
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

		// Token: 0x06000418 RID: 1048 RVA: 0x00006250 File Offset: 0x00004450
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001EC RID: 492
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x040001ED RID: 493
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x040001EE RID: 494
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Value;

		// Token: 0x040001EF RID: 495
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Size = 0.2f;

		// Token: 0x040001F0 RID: 496
		[Range(0f, 11f)]
		[SerializeField]
		private int m_NumberOfSteps;

		// Token: 0x040001F1 RID: 497
		[Space(6f)]
		[SerializeField]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x040001F2 RID: 498
		private RectTransform m_ContainerRect;

		// Token: 0x040001F3 RID: 499
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x040001F4 RID: 500
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x040001F5 RID: 501
		private Coroutine m_PointerDownRepeat;

		// Token: 0x040001F6 RID: 502
		private bool isPointerDownAndNotDragging;

		// Token: 0x040001F7 RID: 503
		private bool m_DelayedUpdateVisuals;

		// Token: 0x02000065 RID: 101
		public enum Direction
		{
			// Token: 0x040001F9 RID: 505
			LeftToRight,
			// Token: 0x040001FA RID: 506
			RightToLeft,
			// Token: 0x040001FB RID: 507
			BottomToTop,
			// Token: 0x040001FC RID: 508
			TopToBottom
		}

		// Token: 0x02000066 RID: 102
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
		}

		// Token: 0x02000067 RID: 103
		private enum Axis
		{
			// Token: 0x040001FE RID: 510
			Horizontal,
			// Token: 0x040001FF RID: 511
			Vertical
		}
	}
}
