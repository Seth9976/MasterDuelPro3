using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000060 RID: 96
	[AddComponentMenu("UI/Scroll Rect", 37)]
	[SelectionBase]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class ScrollRect : UIBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutGroup, ILayoutController
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001135F File Offset: 0x0000F55F
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00011367 File Offset: 0x0000F567
		public RectTransform content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00011370 File Offset: 0x0000F570
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00011378 File Offset: 0x0000F578
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00011381 File Offset: 0x0000F581
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00011389 File Offset: 0x0000F589
		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00011392 File Offset: 0x0000F592
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0001139A File Offset: 0x0000F59A
		public ScrollRect.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000398 RID: 920 RVA: 0x000113A3 File Offset: 0x0000F5A3
		// (set) Token: 0x06000399 RID: 921 RVA: 0x000113AB File Offset: 0x0000F5AB
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000113B4 File Offset: 0x0000F5B4
		// (set) Token: 0x0600039B RID: 923 RVA: 0x000113BC File Offset: 0x0000F5BC
		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000113C5 File Offset: 0x0000F5C5
		// (set) Token: 0x0600039D RID: 925 RVA: 0x000113CD File Offset: 0x0000F5CD
		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000113D6 File Offset: 0x0000F5D6
		// (set) Token: 0x0600039F RID: 927 RVA: 0x000113DE File Offset: 0x0000F5DE
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000113E7 File Offset: 0x0000F5E7
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000113EF File Offset: 0x0000F5EF
		public RectTransform viewport
		{
			get
			{
				return this.m_Viewport;
			}
			set
			{
				this.m_Viewport = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000113FE File Offset: 0x0000F5FE
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00011408 File Offset: 0x0000F608
		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_Horizontal && this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001147C File Offset: 0x0000F67C
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00011484 File Offset: 0x0000F684
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_Vertical && this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000114F8 File Offset: 0x0000F6F8
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00011500 File Offset: 0x0000F700
		public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility
		{
			get
			{
				return this.m_HorizontalScrollbarVisibility;
			}
			set
			{
				this.m_HorizontalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001150F File Offset: 0x0000F70F
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00011517 File Offset: 0x0000F717
		public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility
		{
			get
			{
				return this.m_VerticalScrollbarVisibility;
			}
			set
			{
				this.m_VerticalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00011526 File Offset: 0x0000F726
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0001152E File Offset: 0x0000F72E
		public float horizontalScrollbarSpacing
		{
			get
			{
				return this.m_HorizontalScrollbarSpacing;
			}
			set
			{
				this.m_HorizontalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001153D File Offset: 0x0000F73D
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00011545 File Offset: 0x0000F745
		public float verticalScrollbarSpacing
		{
			get
			{
				return this.m_VerticalScrollbarSpacing;
			}
			set
			{
				this.m_VerticalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00011554 File Offset: 0x0000F754
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0001155C File Offset: 0x0000F75C
		public ScrollRect.ScrollRectEvent onValueChanged
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00011568 File Offset: 0x0000F768
		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = this.m_Viewport;
				}
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.transform;
				}
				return this.m_ViewRect;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000115B4 File Offset: 0x0000F7B4
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000115BC File Offset: 0x0000F7BC
		public Vector2 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000115C5 File Offset: 0x0000F7C5
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000115E8 File Offset: 0x0000F7E8
		protected ScrollRect()
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011670 File Offset: 0x0000F870
		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Prelayout)
			{
				this.UpdateCachedData();
			}
			if (executing == CanvasUpdate.PostLayout)
			{
				this.UpdateBounds();
				this.UpdateScrollbars(Vector2.zero);
				this.UpdatePrevData();
				this.m_HasRebuiltLayout = true;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000116A0 File Offset: 0x0000F8A0
		private void UpdateCachedData()
		{
			Transform transform = base.transform;
			this.m_HorizontalScrollbarRect = ((this.m_HorizontalScrollbar == null) ? null : (this.m_HorizontalScrollbar.transform as RectTransform));
			this.m_VerticalScrollbarRect = ((this.m_VerticalScrollbar == null) ? null : (this.m_VerticalScrollbar.transform as RectTransform));
			bool flag = this.viewRect.parent == transform;
			bool hScrollbarIsChild = !this.m_HorizontalScrollbarRect || this.m_HorizontalScrollbarRect.parent == transform;
			bool vScrollbarIsChild = !this.m_VerticalScrollbarRect || this.m_VerticalScrollbarRect.parent == transform;
			bool allAreChildren = flag && hScrollbarIsChild && vScrollbarIsChild;
			this.m_HSliderExpand = allAreChildren && this.m_HorizontalScrollbarRect && this.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.m_VSliderExpand = allAreChildren && this.m_VerticalScrollbarRect && this.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.m_HSliderHeight = ((this.m_HorizontalScrollbarRect == null) ? 0f : this.m_HorizontalScrollbarRect.rect.height);
			this.m_VSliderWidth = ((this.m_VerticalScrollbarRect == null) ? 0f : this.m_VerticalScrollbarRect.rect.width);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011800 File Offset: 0x0000FA00
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Horizontal && this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_Vertical && this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			this.SetDirty();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00011884 File Offset: 0x0000FA84
		protected override void OnDisable()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_Dragging = false;
			this.m_Scrolling = false;
			this.m_HasRebuiltLayout = false;
			this.m_Tracker.Clear();
			this.m_Velocity = Vector2.zero;
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00011925 File Offset: 0x0000FB25
		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001193D File Offset: 0x0000FB3D
		private void EnsureLayoutHasRebuilt()
		{
			if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00011953 File Offset: 0x0000FB53
		public virtual void StopMovement()
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011960 File Offset: 0x0000FB60
		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 delta = data.scrollDelta;
			delta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
				{
					delta.y = delta.x;
				}
				delta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
				{
					delta.x = delta.y;
				}
				delta.y = 0f;
			}
			if (data.IsScrolling())
			{
				this.m_Scrolling = true;
			}
			Vector2 position = this.m_Content.anchoredPosition;
			position += delta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRect.MovementType.Clamped)
			{
				position += this.CalculateOffset(position - this.m_Content.anchoredPosition);
			}
			this.SetContentAnchoredPosition(position);
			this.UpdateBounds();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00011A7D File Offset: 0x0000FC7D
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011A94 File Offset: 0x0000FC94
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
			this.m_ContentStartPosition = this.m_Content.anchoredPosition;
			this.m_Dragging = true;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011AFA File Offset: 0x0000FCFA
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Dragging = false;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011B0C File Offset: 0x0000FD0C
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.m_Dragging)
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			Vector2 localCursor;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out localCursor))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 pointerDelta = localCursor - this.m_PointerStartLocalCursor;
			Vector2 position = this.m_ContentStartPosition + pointerDelta;
			Vector2 offset = this.CalculateOffset(position - this.m_Content.anchoredPosition);
			position += offset;
			if (this.m_MovementType == ScrollRect.MovementType.Elastic)
			{
				if (offset.x != 0f)
				{
					position.x -= ScrollRect.RubberDelta(offset.x, this.m_ViewBounds.size.x);
				}
				if (offset.y != 0f)
				{
					position.y -= ScrollRect.RubberDelta(offset.y, this.m_ViewBounds.size.y);
				}
			}
			this.SetContentAnchoredPosition(position);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011C0C File Offset: 0x0000FE0C
		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.anchoredPosition.x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.anchoredPosition.y;
			}
			if (position != this.m_Content.anchoredPosition)
			{
				this.m_Content.anchoredPosition = position;
				this.UpdateBounds();
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011C7C File Offset: 0x0000FE7C
		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float deltaTime = Time.unscaledDeltaTime;
			Vector2 offset = this.CalculateOffset(Vector2.zero);
			if (deltaTime > 0f)
			{
				if (!this.m_Dragging && (offset != Vector2.zero || this.m_Velocity != Vector2.zero))
				{
					Vector2 position = this.m_Content.anchoredPosition;
					for (int axis = 0; axis < 2; axis++)
					{
						if (this.m_MovementType == ScrollRect.MovementType.Elastic && offset[axis] != 0f)
						{
							float speed = this.m_Velocity[axis];
							float smoothTime = this.m_Elasticity;
							if (this.m_Scrolling)
							{
								smoothTime *= 3f;
							}
							position[axis] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[axis], this.m_Content.anchoredPosition[axis] + offset[axis], ref speed, smoothTime, float.PositiveInfinity, deltaTime);
							if (Mathf.Abs(speed) < 1f)
							{
								speed = 0f;
							}
							this.m_Velocity[axis] = speed;
						}
						else if (this.m_Inertia)
						{
							ref Vector2 ptr = ref this.m_Velocity;
							int num = axis;
							ptr[num] *= Mathf.Pow(this.m_DecelerationRate, deltaTime);
							if (Mathf.Abs(this.m_Velocity[axis]) < 1f)
							{
								this.m_Velocity[axis] = 0f;
							}
							ptr = ref position;
							num = axis;
							ptr[num] += this.m_Velocity[axis] * deltaTime;
						}
						else
						{
							this.m_Velocity[axis] = 0f;
						}
					}
					if (this.m_MovementType == ScrollRect.MovementType.Clamped)
					{
						offset = this.CalculateOffset(position - this.m_Content.anchoredPosition);
						position += offset;
					}
					this.SetContentAnchoredPosition(position);
				}
				if (this.m_Dragging && this.m_Inertia)
				{
					Vector3 newVelocity = (this.m_Content.anchoredPosition - this.m_PrevPosition) / deltaTime;
					this.m_Velocity = Vector3.Lerp(this.m_Velocity, newVelocity, deltaTime * 10f);
				}
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
			{
				this.UpdateScrollbars(offset);
				UISystemProfilerApi.AddMarker("ScrollRect.value", this);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
			this.UpdateScrollbarVisibility();
			this.m_Scrolling = false;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00011F54 File Offset: 0x00010154
		protected void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.zero;
			}
			else
			{
				this.m_PrevPosition = this.m_Content.anchoredPosition;
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.m_ContentBounds;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011FA8 File Offset: 0x000101A8
		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				if (this.m_ContentBounds.size.x > 0f)
				{
					this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
				}
				else
				{
					this.m_HorizontalScrollbar.size = 1f;
				}
				this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
			}
			if (this.m_VerticalScrollbar)
			{
				if (this.m_ContentBounds.size.y > 0f)
				{
					this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
				}
				else
				{
					this.m_VerticalScrollbar.size = 1f;
				}
				this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x000120BD File Offset: 0x000102BD
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x000120D0 File Offset: 0x000102D0
		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x000120EC File Offset: 0x000102EC
		// (set) Token: 0x060003CA RID: 970 RVA: 0x000121B3 File Offset: 0x000103B3
		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x || Mathf.Approximately(this.m_ContentBounds.size.x, this.m_ViewBounds.size.x))
				{
					return (float)((this.m_ViewBounds.min.x > this.m_ContentBounds.min.x) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000121C0 File Offset: 0x000103C0
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00012287 File Offset: 0x00010487
		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y || Mathf.Approximately(this.m_ContentBounds.size.y, this.m_ViewBounds.size.y))
				{
					return (float)((this.m_ViewBounds.min.y > this.m_ContentBounds.min.y) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000121B3 File Offset: 0x000103B3
		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012287 File Offset: 0x00010487
		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012294 File Offset: 0x00010494
		protected virtual void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float hiddenLength = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
			float contentBoundsMinPosition = this.m_ViewBounds.min[axis] - value * hiddenLength;
			float newAnchoredPosition = this.m_Content.anchoredPosition[axis] + contentBoundsMinPosition - this.m_ContentBounds.min[axis];
			Vector3 anchoredPosition = this.m_Content.anchoredPosition;
			if (Mathf.Abs(anchoredPosition[axis] - newAnchoredPosition) > 0.01f)
			{
				anchoredPosition[axis] = newAnchoredPosition;
				this.m_Content.anchoredPosition = anchoredPosition;
				this.m_Velocity[axis] = 0f;
				this.UpdateBounds();
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012379 File Offset: 0x00010579
		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000123A4 File Offset: 0x000105A4
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000123AC File Offset: 0x000105AC
		private bool hScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.x > this.m_ViewBounds.size.x + 0.01f;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000123DF File Offset: 0x000105DF
		private bool vScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.y > this.m_ViewBounds.size.y + 0.01f;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float minWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float preferredWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float preferredHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00012412 File Offset: 0x00010612
		public virtual int layoutPriority
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012418 File Offset: 0x00010618
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.UpdateCachedData();
			if (this.m_HSliderExpand || this.m_VSliderExpand)
			{
				this.m_Tracker.Add(this, this.viewRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.viewRect.anchorMin = Vector2.zero;
				this.viewRect.anchorMax = Vector2.one;
				this.viewRect.sizeDelta = Vector2.zero;
				this.viewRect.anchoredPosition = Vector2.zero;
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_HSliderExpand && this.hScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(this.viewRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded && this.viewRect.sizeDelta.x == 0f && this.viewRect.sizeDelta.y < 0f)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001267C File Offset: 0x0001087C
		public virtual void SetLayoutVertical()
		{
			this.UpdateScrollbarLayout();
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000126D6 File Offset: 0x000108D6
		private void UpdateScrollbarVisibility()
		{
			ScrollRect.UpdateOneScrollbarVisibility(this.vScrollingNeeded, this.m_Vertical, this.m_VerticalScrollbarVisibility, this.m_VerticalScrollbar);
			ScrollRect.UpdateOneScrollbarVisibility(this.hScrollingNeeded, this.m_Horizontal, this.m_HorizontalScrollbarVisibility, this.m_HorizontalScrollbar);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012714 File Offset: 0x00010914
		private static void UpdateOneScrollbarVisibility(bool xScrollingNeeded, bool xAxisEnabled, ScrollRect.ScrollbarVisibility scrollbarVisibility, Scrollbar scrollbar)
		{
			if (scrollbar)
			{
				if (scrollbarVisibility == ScrollRect.ScrollbarVisibility.Permanent)
				{
					if (scrollbar.gameObject.activeSelf != xAxisEnabled)
					{
						scrollbar.gameObject.SetActive(xAxisEnabled);
						return;
					}
				}
				else if (scrollbar.gameObject.activeSelf != xScrollingNeeded)
				{
					scrollbar.gameObject.SetActive(xScrollingNeeded);
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012764 File Offset: 0x00010964
		private void UpdateScrollbarLayout()
		{
			if (this.m_VSliderExpand && this.m_HorizontalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_HorizontalScrollbarRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.SizeDeltaX);
				this.m_HorizontalScrollbarRect.anchorMin = new Vector2(0f, this.m_HorizontalScrollbarRect.anchorMin.y);
				this.m_HorizontalScrollbarRect.anchorMax = new Vector2(1f, this.m_HorizontalScrollbarRect.anchorMax.y);
				this.m_HorizontalScrollbarRect.anchoredPosition = new Vector2(0f, this.m_HorizontalScrollbarRect.anchoredPosition.y);
				if (this.vScrollingNeeded)
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
				else
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(0f, this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
			}
			if (this.m_HSliderExpand && this.m_VerticalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_VerticalScrollbarRect, DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaY);
				this.m_VerticalScrollbarRect.anchorMin = new Vector2(this.m_VerticalScrollbarRect.anchorMin.x, 0f);
				this.m_VerticalScrollbarRect.anchorMax = new Vector2(this.m_VerticalScrollbarRect.anchorMax.x, 1f);
				this.m_VerticalScrollbarRect.anchoredPosition = new Vector2(this.m_VerticalScrollbarRect.anchoredPosition.x, 0f);
				if (this.hScrollingNeeded)
				{
					this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
					return;
				}
				this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, 0f);
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001296C File Offset: 0x00010B6C
		protected void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 contentSize = this.m_ContentBounds.size;
			Vector3 contentPos = this.m_ContentBounds.center;
			Vector2 contentPivot = this.m_Content.pivot;
			ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref contentPivot, ref contentSize, ref contentPos);
			this.m_ContentBounds.size = contentSize;
			this.m_ContentBounds.center = contentPos;
			if (this.movementType == ScrollRect.MovementType.Clamped)
			{
				Vector2 delta = Vector2.zero;
				if (this.m_ViewBounds.max.x > this.m_ContentBounds.max.x)
				{
					delta.x = Math.Min(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				else if (this.m_ViewBounds.min.x < this.m_ContentBounds.min.x)
				{
					delta.x = Math.Max(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				if (this.m_ViewBounds.min.y < this.m_ContentBounds.min.y)
				{
					delta.y = Math.Max(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				else if (this.m_ViewBounds.max.y > this.m_ContentBounds.max.y)
				{
					delta.y = Math.Min(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				if (delta.sqrMagnitude > 1E-45f)
				{
					contentPos = this.m_Content.anchoredPosition + delta;
					if (!this.m_Horizontal)
					{
						contentPos.x = this.m_Content.anchoredPosition.x;
					}
					if (!this.m_Vertical)
					{
						contentPos.y = this.m_Content.anchoredPosition.y;
					}
					ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref contentPivot, ref contentSize, ref contentPos);
				}
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012C68 File Offset: 0x00010E68
		internal static void AdjustBounds(ref Bounds viewBounds, ref Vector2 contentPivot, ref Vector3 contentSize, ref Vector3 contentPos)
		{
			Vector3 excess = viewBounds.size - contentSize;
			if (excess.x > 0f)
			{
				contentPos.x -= excess.x * (contentPivot.x - 0.5f);
				contentSize.x = viewBounds.size.x;
			}
			if (excess.y > 0f)
			{
				contentPos.y -= excess.y * (contentPivot.y - 0.5f);
				contentSize.y = viewBounds.size.y;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00012D00 File Offset: 0x00010F00
		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			this.m_Content.GetWorldCorners(this.m_Corners);
			Matrix4x4 viewWorldToLocalMatrix = this.viewRect.worldToLocalMatrix;
			return ScrollRect.InternalGetBounds(this.m_Corners, ref viewWorldToLocalMatrix);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00012D50 File Offset: 0x00010F50
		internal static Bounds InternalGetBounds(Vector3[] corners, ref Matrix4x4 viewWorldToLocalMatrix)
		{
			Vector3 vMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector = viewWorldToLocalMatrix.MultiplyPoint3x4(corners[i]);
				vMin = Vector3.Min(vector, vMin);
				vMax = Vector3.Max(vector, vMax);
			}
			Bounds bounds = new Bounds(vMin, Vector3.zero);
			bounds.Encapsulate(vMax);
			return bounds;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012DC7 File Offset: 0x00010FC7
		private Vector2 CalculateOffset(Vector2 delta)
		{
			return ScrollRect.InternalCalculateOffset(ref this.m_ViewBounds, ref this.m_ContentBounds, this.m_Horizontal, this.m_Vertical, this.m_MovementType, ref delta);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00012DF0 File Offset: 0x00010FF0
		internal static Vector2 InternalCalculateOffset(ref Bounds viewBounds, ref Bounds contentBounds, bool horizontal, bool vertical, ScrollRect.MovementType movementType, ref Vector2 delta)
		{
			Vector2 offset = Vector2.zero;
			if (movementType == ScrollRect.MovementType.Unrestricted)
			{
				return offset;
			}
			Vector2 min = contentBounds.min;
			Vector2 max = contentBounds.max;
			if (horizontal)
			{
				min.x += delta.x;
				max.x += delta.x;
				float maxOffset = viewBounds.max.x - max.x;
				float minOffset = viewBounds.min.x - min.x;
				if (minOffset < -0.001f)
				{
					offset.x = minOffset;
				}
				else if (maxOffset > 0.001f)
				{
					offset.x = maxOffset;
				}
			}
			if (vertical)
			{
				min.y += delta.y;
				max.y += delta.y;
				float maxOffset2 = viewBounds.max.y - max.y;
				float minOffset2 = viewBounds.min.y - min.y;
				if (maxOffset2 > 0.001f)
				{
					offset.y = maxOffset2;
				}
				else if (minOffset2 < -0.001f)
				{
					offset.y = minOffset2;
				}
			}
			return offset;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012F09 File Offset: 0x00011109
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012F1F File Offset: 0x0001111F
		protected void SetDirtyCaching()
		{
			if (!this.IsActive())
			{
				return;
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			this.m_ViewRect = null;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00006250 File Offset: 0x00004450
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001BF RID: 447
		[SerializeField]
		private RectTransform m_Content;

		// Token: 0x040001C0 RID: 448
		[SerializeField]
		private bool m_Horizontal = true;

		// Token: 0x040001C1 RID: 449
		[SerializeField]
		private bool m_Vertical = true;

		// Token: 0x040001C2 RID: 450
		[SerializeField]
		private ScrollRect.MovementType m_MovementType = ScrollRect.MovementType.Elastic;

		// Token: 0x040001C3 RID: 451
		[SerializeField]
		private float m_Elasticity = 0.1f;

		// Token: 0x040001C4 RID: 452
		[SerializeField]
		private bool m_Inertia = true;

		// Token: 0x040001C5 RID: 453
		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		// Token: 0x040001C6 RID: 454
		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		// Token: 0x040001C7 RID: 455
		[SerializeField]
		private RectTransform m_Viewport;

		// Token: 0x040001C8 RID: 456
		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		// Token: 0x040001C9 RID: 457
		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		// Token: 0x040001CA RID: 458
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_HorizontalScrollbarVisibility;

		// Token: 0x040001CB RID: 459
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_VerticalScrollbarVisibility;

		// Token: 0x040001CC RID: 460
		[SerializeField]
		private float m_HorizontalScrollbarSpacing;

		// Token: 0x040001CD RID: 461
		[SerializeField]
		private float m_VerticalScrollbarSpacing;

		// Token: 0x040001CE RID: 462
		[SerializeField]
		private ScrollRect.ScrollRectEvent m_OnValueChanged = new ScrollRect.ScrollRectEvent();

		// Token: 0x040001CF RID: 463
		private Vector2 m_PointerStartLocalCursor = Vector2.zero;

		// Token: 0x040001D0 RID: 464
		protected Vector2 m_ContentStartPosition = Vector2.zero;

		// Token: 0x040001D1 RID: 465
		private RectTransform m_ViewRect;

		// Token: 0x040001D2 RID: 466
		protected Bounds m_ContentBounds;

		// Token: 0x040001D3 RID: 467
		private Bounds m_ViewBounds;

		// Token: 0x040001D4 RID: 468
		private Vector2 m_Velocity;

		// Token: 0x040001D5 RID: 469
		private bool m_Dragging;

		// Token: 0x040001D6 RID: 470
		private bool m_Scrolling;

		// Token: 0x040001D7 RID: 471
		private Vector2 m_PrevPosition = Vector2.zero;

		// Token: 0x040001D8 RID: 472
		private Bounds m_PrevContentBounds;

		// Token: 0x040001D9 RID: 473
		private Bounds m_PrevViewBounds;

		// Token: 0x040001DA RID: 474
		[NonSerialized]
		private bool m_HasRebuiltLayout;

		// Token: 0x040001DB RID: 475
		private bool m_HSliderExpand;

		// Token: 0x040001DC RID: 476
		private bool m_VSliderExpand;

		// Token: 0x040001DD RID: 477
		private float m_HSliderHeight;

		// Token: 0x040001DE RID: 478
		private float m_VSliderWidth;

		// Token: 0x040001DF RID: 479
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040001E0 RID: 480
		private RectTransform m_HorizontalScrollbarRect;

		// Token: 0x040001E1 RID: 481
		private RectTransform m_VerticalScrollbarRect;

		// Token: 0x040001E2 RID: 482
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x040001E3 RID: 483
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x02000061 RID: 97
		public enum MovementType
		{
			// Token: 0x040001E5 RID: 485
			Unrestricted,
			// Token: 0x040001E6 RID: 486
			Elastic,
			// Token: 0x040001E7 RID: 487
			Clamped
		}

		// Token: 0x02000062 RID: 98
		public enum ScrollbarVisibility
		{
			// Token: 0x040001E9 RID: 489
			Permanent,
			// Token: 0x040001EA RID: 490
			AutoHide,
			// Token: 0x040001EB RID: 491
			AutoHideAndExpandViewport
		}

		// Token: 0x02000063 RID: 99
		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
		}
	}
}
