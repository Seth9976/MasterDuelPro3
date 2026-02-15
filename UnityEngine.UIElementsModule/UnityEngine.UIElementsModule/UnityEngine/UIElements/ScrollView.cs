using System;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013B RID: 315
	public class ScrollView : VisualElement
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0002CA34 File Offset: 0x0002AC34
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0002CA4C File Offset: 0x0002AC4C
		[CreateProperty]
		public ScrollerVisibility horizontalScrollerVisibility
		{
			get
			{
				return this.m_HorizontalScrollerVisibility;
			}
			set
			{
				ScrollerVisibility previous = this.m_HorizontalScrollerVisibility;
				this.m_HorizontalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
				bool flag = previous != this.m_HorizontalScrollerVisibility;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.horizontalScrollerVisibilityProperty);
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0002CA98 File Offset: 0x0002AC98
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0002CAB0 File Offset: 0x0002ACB0
		[CreateProperty]
		public ScrollerVisibility verticalScrollerVisibility
		{
			get
			{
				return this.m_VerticalScrollerVisibility;
			}
			set
			{
				ScrollerVisibility previous = this.m_VerticalScrollerVisibility;
				this.m_VerticalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
				bool flag = previous != this.m_VerticalScrollerVisibility;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.verticalScrollerVisibilityProperty);
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0002CB14 File Offset: 0x0002AD14
		[CreateProperty]
		public long elasticAnimationIntervalMs
		{
			get
			{
				return this.m_ElasticAnimationIntervalMs;
			}
			set
			{
				long previous = this.m_ElasticAnimationIntervalMs;
				this.m_ElasticAnimationIntervalMs = value;
				bool flag = previous != this.m_ElasticAnimationIntervalMs;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.elasticAnimationIntervalMsProperty);
					this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(this.m_ElasticAnimationIntervalMs);
				}
			}
		}

		// Token: 0x1700019B RID: 411
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0002CB76 File Offset: 0x0002AD76
		[Obsolete("showHorizontal is obsolete. Use horizontalScrollerVisibility instead")]
		public bool showHorizontal
		{
			set
			{
				this.m_HorizontalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x1700019C RID: 412
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0002CB85 File Offset: 0x0002AD85
		[Obsolete("showVertical is obsolete. Use verticalScrollerVisibility instead")]
		public bool showVertical
		{
			set
			{
				this.m_VerticalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0002CB94 File Offset: 0x0002AD94
		internal bool needsHorizontal
		{
			get
			{
				return (this.mode != ScrollViewMode.Vertical && this.horizontalScrollerVisibility == ScrollerVisibility.AlwaysVisible) || (this.horizontalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableWidth > 0.001f);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
		internal bool needsVertical
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return (this.mode != ScrollViewMode.Horizontal && this.verticalScrollerVisibility == ScrollerVisibility.AlwaysVisible) || (this.verticalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableHeight > 0.001f);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0002CC04 File Offset: 0x0002AE04
		internal bool isVerticalScrollDisplayed
		{
			get
			{
				return this.verticalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002CC2C File Offset: 0x0002AE2C
		internal bool isHorizontalScrollDisplayed
		{
			get
			{
				return this.horizontalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0002CC54 File Offset: 0x0002AE54
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x0002CC84 File Offset: 0x0002AE84
		[CreateProperty]
		public Vector2 scrollOffset
		{
			get
			{
				return new Vector2(this.horizontalScroller.value, this.verticalScroller.value);
			}
			set
			{
				bool flag = value != this.scrollOffset;
				if (flag)
				{
					this.horizontalScroller.value = value.x;
					this.verticalScroller.value = value.y;
					bool flag2 = base.panel != null;
					if (flag2)
					{
						this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
						this.UpdateContentViewTransform();
					}
					base.NotifyPropertyChanged(in ScrollView.scrollOffsetProperty);
				}
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0002CD00 File Offset: 0x0002AF00
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x0002CD18 File Offset: 0x0002AF18
		[CreateProperty]
		public float horizontalPageSize
		{
			get
			{
				return this.m_HorizontalPageSize;
			}
			set
			{
				float previous = this.m_HorizontalPageSize;
				this.m_HorizontalPageSize = value;
				this.UpdateHorizontalSliderPageSize();
				bool flag = !Mathf.Approximately(previous, this.m_HorizontalPageSize);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.horizontalPageSizeProperty);
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0002CD5C File Offset: 0x0002AF5C
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0002CD74 File Offset: 0x0002AF74
		[CreateProperty]
		public float verticalPageSize
		{
			get
			{
				return this.m_VerticalPageSize;
			}
			set
			{
				float previous = this.m_VerticalPageSize;
				this.m_VerticalPageSize = value;
				this.UpdateVerticalSliderPageSize();
				bool flag = !Mathf.Approximately(previous, this.m_VerticalPageSize);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.verticalPageSizeProperty);
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0002CDB8 File Offset: 0x0002AFB8
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x0002CDD0 File Offset: 0x0002AFD0
		[CreateProperty]
		public float mouseWheelScrollSize
		{
			get
			{
				return this.m_MouseWheelScrollSize;
			}
			set
			{
				float previous = this.m_MouseWheelScrollSize;
				bool flag = Math.Abs(this.m_MouseWheelScrollSize - value) > float.Epsilon;
				if (flag)
				{
					this.m_MouseWheelScrollSizeIsInline = true;
					this.m_MouseWheelScrollSize = value;
					base.NotifyPropertyChanged(in ScrollView.mouseWheelScrollSizeProperty);
				}
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0002CE1C File Offset: 0x0002B01C
		internal float scrollableWidth
		{
			get
			{
				return this.contentContainer.boundingBox.width - this.contentViewport.layout.width;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0002CE58 File Offset: 0x0002B058
		internal float scrollableHeight
		{
			get
			{
				return this.contentContainer.boundingBox.height - this.contentViewport.layout.height;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0002CE91 File Offset: 0x0002B091
		private bool hasInertia
		{
			get
			{
				return this.scrollDecelerationRate > 0f;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0002CEA0 File Offset: 0x0002B0A0
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0002CEB8 File Offset: 0x0002B0B8
		[CreateProperty]
		public float scrollDecelerationRate
		{
			get
			{
				return this.m_ScrollDecelerationRate;
			}
			set
			{
				float previous = this.m_ScrollDecelerationRate;
				this.m_ScrollDecelerationRate = Mathf.Max(0f, value);
				bool flag = !Mathf.Approximately(previous, this.m_ScrollDecelerationRate);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.scrollDecelerationRateProperty);
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0002CF00 File Offset: 0x0002B100
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0002CF18 File Offset: 0x0002B118
		[CreateProperty]
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				float previous = this.m_Elasticity;
				this.m_Elasticity = Mathf.Max(0f, value);
				bool flag = !Mathf.Approximately(previous, this.m_Elasticity);
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.elasticityProperty);
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0002CF60 File Offset: 0x0002B160
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0002CF78 File Offset: 0x0002B178
		[CreateProperty]
		public ScrollView.TouchScrollBehavior touchScrollBehavior
		{
			get
			{
				return this.m_TouchScrollBehavior;
			}
			set
			{
				ScrollView.TouchScrollBehavior previous = this.m_TouchScrollBehavior;
				this.m_TouchScrollBehavior = value;
				bool flag = this.m_TouchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				if (flag)
				{
					this.horizontalScroller.slider.clamped = true;
					this.verticalScroller.slider.clamped = true;
				}
				else
				{
					this.horizontalScroller.slider.clamped = false;
					this.verticalScroller.slider.clamped = false;
				}
				bool flag2 = previous != this.m_TouchScrollBehavior;
				if (flag2)
				{
					base.NotifyPropertyChanged(in ScrollView.touchScrollBehaviorProperty);
				}
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0002D00B File Offset: 0x0002B20B
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0002D014 File Offset: 0x0002B214
		[CreateProperty]
		public ScrollView.NestedInteractionKind nestedInteractionKind
		{
			get
			{
				return this.m_NestedInteractionKind;
			}
			set
			{
				ScrollView.NestedInteractionKind previous = this.m_NestedInteractionKind;
				this.m_NestedInteractionKind = value;
				bool flag = previous != this.m_NestedInteractionKind;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.nestedInteractionKindProperty);
				}
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002D04C File Offset: 0x0002B24C
		private void OnHorizontalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateHorizontalSliderPageSize();
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002D08C File Offset: 0x0002B28C
		private void OnVerticalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateVerticalSliderPageSize();
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002D0CC File Offset: 0x0002B2CC
		private void UpdateHorizontalSliderPageSize()
		{
			float containerWidth = this.horizontalScroller.resolvedStyle.width;
			float horizontalSliderPageSize = this.m_HorizontalPageSize;
			bool flag = containerWidth > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_HorizontalPageSize, -1f);
				if (flag2)
				{
					float sliderDragElementWidth = this.horizontalScroller.slider.dragElement.resolvedStyle.width;
					horizontalSliderPageSize = sliderDragElementWidth * 0.9f;
				}
			}
			bool flag3 = horizontalSliderPageSize >= 0f;
			if (flag3)
			{
				this.horizontalScroller.slider.pageSize = horizontalSliderPageSize;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002D160 File Offset: 0x0002B360
		private void UpdateVerticalSliderPageSize()
		{
			float containerHeight = this.verticalScroller.resolvedStyle.height;
			float verticalSliderPageSize = this.m_VerticalPageSize;
			bool flag = containerHeight > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_VerticalPageSize, -1f);
				if (flag2)
				{
					float sliderDragElementHeight = this.verticalScroller.slider.dragElement.resolvedStyle.height;
					verticalSliderPageSize = sliderDragElementHeight * 0.9f;
				}
			}
			bool flag3 = verticalSliderPageSize >= 0f;
			if (flag3)
			{
				this.verticalScroller.slider.pageSize = verticalSliderPageSize;
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
		internal void UpdateContentViewTransform()
		{
			Vector3 t = this.contentContainer.transform.position;
			Vector2 offset = this.scrollOffset;
			bool needsVertical = this.needsVertical;
			if (needsVertical)
			{
				offset.y += this.contentContainer.resolvedStyle.top;
			}
			t.x = GUIUtility.RoundToPixelGrid(-offset.x);
			t.y = GUIUtility.RoundToPixelGrid(-offset.y);
			this.contentContainer.transform.position = t;
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002D284 File Offset: 0x0002B484
		public void ScrollTo(VisualElement child)
		{
			bool flag = child == null;
			if (flag)
			{
				throw new ArgumentNullException("child");
			}
			bool flag2 = !this.contentContainer.Contains(child);
			if (flag2)
			{
				throw new ArgumentException("Cannot scroll to a VisualElement that's not a child of the ScrollView content-container.");
			}
			this.m_Velocity = Vector2.zero;
			float yDeltaOffset = 0f;
			float xDeltaOffset = 0f;
			bool flag3 = this.scrollableHeight > 0f;
			if (flag3)
			{
				yDeltaOffset = this.GetYDeltaOffset(child);
				this.verticalScroller.value = this.scrollOffset.y + yDeltaOffset;
			}
			bool flag4 = this.scrollableWidth > 0f;
			if (flag4)
			{
				xDeltaOffset = this.GetXDeltaOffset(child);
				this.horizontalScroller.value = this.scrollOffset.x + xDeltaOffset;
			}
			bool flag5 = yDeltaOffset == 0f && xDeltaOffset == 0f;
			if (!flag5)
			{
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002D368 File Offset: 0x0002B568
		private float GetXDeltaOffset(VisualElement child)
		{
			float xTransform = this.contentContainer.transform.position.x * -1f;
			Rect contentWB = this.contentViewport.worldBound;
			float viewMin = contentWB.xMin + xTransform;
			float viewMax = contentWB.xMax + xTransform;
			Rect childWB = child.worldBound;
			float childBoundaryMin = childWB.xMin + xTransform;
			float childBoundaryMax = childWB.xMax + xTransform;
			bool flag = (childBoundaryMin >= viewMin && childBoundaryMax <= viewMax) || float.IsNaN(childBoundaryMin) || float.IsNaN(childBoundaryMax);
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(viewMin, viewMax, childBoundaryMin, childBoundaryMax);
				num = deltaDistance * this.horizontalScroller.highValue / this.scrollableWidth;
			}
			return num;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002D428 File Offset: 0x0002B628
		private float GetYDeltaOffset(VisualElement child)
		{
			float yTransform = this.contentContainer.transform.position.y * -1f;
			Rect contentWB = this.contentViewport.worldBound;
			float viewMin = contentWB.yMin + yTransform;
			float viewMax = contentWB.yMax + yTransform;
			Rect childWB = child.worldBound;
			float childBoundaryMin = childWB.yMin + yTransform;
			float childBoundaryMax = childWB.yMax + yTransform;
			bool flag = (childBoundaryMin >= viewMin && childBoundaryMax <= viewMax) || float.IsNaN(childBoundaryMin) || float.IsNaN(childBoundaryMax);
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(viewMin, viewMax, childBoundaryMin, childBoundaryMax);
				num = deltaDistance * this.verticalScroller.highValue / this.scrollableHeight;
			}
			return num;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		private float GetDeltaDistance(float viewMin, float viewMax, float childBoundaryMin, float childBoundaryMax)
		{
			float viewSize = viewMax - viewMin;
			float childSize = childBoundaryMax - childBoundaryMin;
			bool flag = childSize > viewSize;
			float num;
			if (flag)
			{
				bool flag2 = viewMin > childBoundaryMin && childBoundaryMax > viewMax;
				if (flag2)
				{
					num = 0f;
				}
				else
				{
					num = ((childBoundaryMin > viewMin) ? (childBoundaryMin - viewMin) : (childBoundaryMax - viewMax));
				}
			}
			else
			{
				float deltaDistance = childBoundaryMax - viewMax;
				bool flag3 = deltaDistance < -1f;
				if (flag3)
				{
					deltaDistance = childBoundaryMin - viewMin;
				}
				num = deltaDistance;
			}
			return num;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0002D554 File Offset: 0x0002B754
		public VisualElement contentViewport { get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0002D55C File Offset: 0x0002B75C
		public Scroller horizontalScroller { get; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002D564 File Offset: 0x0002B764
		public Scroller verticalScroller { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0002D56C File Offset: 0x0002B76C
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002D584 File Offset: 0x0002B784
		public ScrollView()
			: this(ScrollViewMode.Vertical)
		{
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002D590 File Offset: 0x0002B790
		public ScrollView(ScrollViewMode scrollViewMode)
		{
			base.AddToClassList(ScrollView.ussClassName);
			this.m_ContentAndVerticalScrollContainer = new VisualElement
			{
				name = "unity-content-and-vertical-scroll-container"
			};
			this.m_ContentAndVerticalScrollContainer.AddToClassList(ScrollView.contentAndVerticalScrollUssClassName);
			base.hierarchy.Add(this.m_ContentAndVerticalScrollContainer);
			this.contentViewport = new VisualElement
			{
				name = "unity-content-viewport"
			};
			this.contentViewport.AddToClassList(ScrollView.viewportUssClassName);
			this.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.contentViewport.pickingMode = PickingMode.Ignore;
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.Add(this.contentViewport);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.disableClipping = true;
			this.m_ContentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_ContentContainer.AddToClassList(ScrollView.contentUssClassName);
			this.m_ContentContainer.usageHints = UsageHints.GroupTransform;
			this.contentViewport.Add(this.m_ContentContainer);
			this.SetScrollViewMode(scrollViewMode);
			this.horizontalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(value, this.scrollOffset.y);
				this.UpdateContentViewTransform();
			}, SliderDirection.Horizontal)
			{
				viewDataKey = "HorizontalScroller"
			};
			this.horizontalScroller.AddToClassList(ScrollView.hScrollerUssClassName);
			this.horizontalScroller.style.display = DisplayStyle.None;
			base.hierarchy.Add(this.horizontalScroller);
			this.verticalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(this.scrollOffset.x, value);
				this.UpdateContentViewTransform();
			}, SliderDirection.Vertical)
			{
				viewDataKey = "VerticalScroller"
			};
			this.horizontalScroller.slider.clampedDragger.draggingEnded += this.UpdateElasticBehaviour;
			this.verticalScroller.slider.clampedDragger.draggingEnded += this.UpdateElasticBehaviour;
			this.horizontalScroller.lowButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.horizontalScroller.highButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.lowButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.highButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.AddToClassList(ScrollView.vScrollerUssClassName);
			this.verticalScroller.style.display = DisplayStyle.None;
			this.m_ContentAndVerticalScrollContainer.Add(this.verticalScroller);
			this.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			base.RegisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScrollWheel), TrickleDown.NoTrickleDown);
			this.verticalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalPageSize = -1f;
			this.verticalPageSize = -1f;
			this.horizontalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnHorizontalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.verticalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnVerticalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.m_CapturedTargetPointerMoveCallback = new EventCallback<PointerMoveEvent>(this.OnPointerMove);
			this.m_CapturedTargetPointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002D9DA File Offset: 0x0002BBDA
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
		[CreateProperty]
		public ScrollViewMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				ScrollViewMode previous = this.m_Mode;
				this.SetScrollViewMode(value);
				bool flag = previous != this.m_Mode;
				if (flag)
				{
					base.NotifyPropertyChanged(in ScrollView.modeProperty);
				}
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002DA20 File Offset: 0x0002BC20
		private void SetScrollViewMode(ScrollViewMode mode)
		{
			this.m_Mode = mode;
			base.RemoveFromClassList(ScrollView.verticalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.horizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.verticalHorizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.scrollVariantUssClassName);
			this.contentContainer.RemoveFromClassList(ScrollView.verticalVariantContentUssClassName);
			this.contentContainer.RemoveFromClassList(ScrollView.horizontalVariantContentUssClassName);
			this.contentContainer.RemoveFromClassList(ScrollView.verticalHorizontalVariantContentUssClassName);
			this.contentViewport.RemoveFromClassList(ScrollView.verticalVariantViewportUssClassName);
			this.contentViewport.RemoveFromClassList(ScrollView.horizontalVariantViewportUssClassName);
			this.contentViewport.RemoveFromClassList(ScrollView.verticalHorizontalVariantViewportUssClassName);
			switch (mode)
			{
			case ScrollViewMode.Vertical:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalVariantUssClassName);
				this.contentViewport.AddToClassList(ScrollView.verticalVariantViewportUssClassName);
				this.contentContainer.AddToClassList(ScrollView.verticalVariantContentUssClassName);
				break;
			case ScrollViewMode.Horizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.horizontalVariantUssClassName);
				this.contentViewport.AddToClassList(ScrollView.horizontalVariantViewportUssClassName);
				this.contentContainer.AddToClassList(ScrollView.horizontalVariantContentUssClassName);
				break;
			case ScrollViewMode.VerticalAndHorizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalHorizontalVariantUssClassName);
				this.contentViewport.AddToClassList(ScrollView.verticalHorizontalVariantViewportUssClassName);
				this.contentContainer.AddToClassList(ScrollView.verticalHorizontalVariantContentUssClassName);
				break;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002DB9C File Offset: 0x0002BD9C
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_AttachedRootVisualContainer = base.GetRootVisualContainer();
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				this.ReadSingleLineHeight();
				bool flag2 = evt.destinationPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
					evt.destinationPanel.visualTree.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnRootPointerUp), TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002DCBC File Offset: 0x0002BEBC
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			IVisualElementScheduledItem scheduledLayoutPassResetItem = this.m_ScheduledLayoutPassResetItem;
			if (scheduledLayoutPassResetItem != null)
			{
				scheduledLayoutPassResetItem.Pause();
			}
			this.ResetLayoutPass();
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.UnregisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				this.m_AttachedRootVisualContainer = null;
				bool flag2 = evt.originPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.contentContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
					evt.originPanel.visualTree.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnRootPointerUp), TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002DDE8 File Offset: 0x0002BFE8
		private void OnPointerCapture(PointerCaptureEvent evt)
		{
			this.m_CapturedTarget = evt.elementTarget;
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_TouchPointerMoveAllowed = true;
				this.m_CapturedTarget.RegisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.RegisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002DE40 File Offset: 0x0002C040
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_CapturedTarget.UnregisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.UnregisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget = null;
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002DEA0 File Offset: 0x0002C0A0
		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool needsVerticalCached = this.needsVertical;
				bool needsHorizontalCached = this.needsHorizontal;
				bool flag2 = this.m_FirstLayoutPass == -1;
				if (flag2)
				{
					this.m_FirstLayoutPass = evt.layoutPass;
				}
				else
				{
					bool flag3 = evt.layoutPass - this.m_FirstLayoutPass > 5;
					if (flag3)
					{
						needsVerticalCached = needsVerticalCached || this.isVerticalScrollDisplayed;
						needsHorizontalCached = needsHorizontalCached || this.isHorizontalScrollDisplayed;
					}
				}
				this.UpdateScrollers(needsHorizontalCached, needsVerticalCached);
				this.UpdateContentViewTransform();
				this.ScheduleResetLayoutPass();
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0002DF4C File Offset: 0x0002C14C
		private void ScheduleResetLayoutPass()
		{
			bool flag = this.m_ScheduledLayoutPassResetItem == null;
			if (flag)
			{
				this.m_ScheduledLayoutPassResetItem = base.schedule.Execute(new Action(this.ResetLayoutPass));
			}
			else
			{
				this.m_ScheduledLayoutPassResetItem.Pause();
				this.m_ScheduledLayoutPassResetItem.Resume();
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0002DFA2 File Offset: 0x0002C1A2
		private void ResetLayoutPass()
		{
			this.m_FirstLayoutPass = -1;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002DFAC File Offset: 0x0002C1AC
		private static float ComputeElasticOffset(float deltaPointer, float initialScrollOffset, float lowLimit, float hardLowLimit, float highLimit, float hardHighLimit)
		{
			initialScrollOffset = Mathf.Max(initialScrollOffset, hardLowLimit * 0.95f);
			initialScrollOffset = Mathf.Min(initialScrollOffset, hardHighLimit * 0.95f);
			bool flag = initialScrollOffset < lowLimit && hardLowLimit < lowLimit;
			float scaleFactor;
			float delta;
			if (flag)
			{
				scaleFactor = lowLimit - hardLowLimit;
				float currentEnergy = (lowLimit - initialScrollOffset) / scaleFactor;
				delta = currentEnergy * scaleFactor / (1f - currentEnergy);
				delta += deltaPointer;
				initialScrollOffset = lowLimit;
			}
			else
			{
				bool flag2 = initialScrollOffset > highLimit && hardHighLimit > highLimit;
				if (flag2)
				{
					scaleFactor = hardHighLimit - highLimit;
					float currentEnergy2 = (initialScrollOffset - highLimit) / scaleFactor;
					delta = -1f * currentEnergy2 * scaleFactor / (1f - currentEnergy2);
					delta += deltaPointer;
					initialScrollOffset = highLimit;
				}
				else
				{
					delta = deltaPointer;
				}
			}
			float newOffset = initialScrollOffset - delta;
			bool flag3 = newOffset < lowLimit;
			float direction;
			if (flag3)
			{
				delta = lowLimit - newOffset;
				initialScrollOffset = lowLimit;
				scaleFactor = lowLimit - hardLowLimit;
				direction = 1f;
			}
			else
			{
				bool flag4 = newOffset <= highLimit;
				if (flag4)
				{
					return newOffset;
				}
				delta = newOffset - highLimit;
				initialScrollOffset = highLimit;
				scaleFactor = hardHighLimit - highLimit;
				direction = -1f;
			}
			bool flag5 = Mathf.Abs(delta) < 1E-30f;
			float num;
			if (flag5)
			{
				num = initialScrollOffset;
			}
			else
			{
				float energy = delta / (delta + scaleFactor);
				energy *= scaleFactor;
				energy *= direction;
				newOffset = initialScrollOffset - energy;
				num = newOffset;
			}
			return num;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002E0DC File Offset: 0x0002C2DC
		private void ComputeInitialSpringBackVelocity()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				bool flag2 = this.scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					this.m_SpringBackVelocity.x = this.m_LowBounds.x - this.scrollOffset.x;
				}
				else
				{
					bool flag3 = this.scrollOffset.x > this.m_HighBounds.x;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = this.m_HighBounds.x - this.scrollOffset.x;
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag4 = this.scrollOffset.y < this.m_LowBounds.y;
				if (flag4)
				{
					this.m_SpringBackVelocity.y = this.m_LowBounds.y - this.scrollOffset.y;
				}
				else
				{
					bool flag5 = this.scrollOffset.y > this.m_HighBounds.y;
					if (flag5)
					{
						this.m_SpringBackVelocity.y = this.m_HighBounds.y - this.scrollOffset.y;
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002E23C File Offset: 0x0002C43C
		private void SpringBack()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				Vector2 newOffset = this.scrollOffset;
				bool flag2 = newOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					newOffset.x = Mathf.SmoothDamp(newOffset.x, this.m_LowBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastHorizontalTouchScroll);
					bool flag3 = Mathf.Abs(this.m_SpringBackVelocity.x) < base.scaledPixelsPerPoint;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				else
				{
					bool flag4 = newOffset.x > this.m_HighBounds.x;
					if (flag4)
					{
						newOffset.x = Mathf.SmoothDamp(newOffset.x, this.m_HighBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastHorizontalTouchScroll);
						bool flag5 = Mathf.Abs(this.m_SpringBackVelocity.x) < base.scaledPixelsPerPoint;
						if (flag5)
						{
							this.m_SpringBackVelocity.x = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag6 = newOffset.y < this.m_LowBounds.y;
				if (flag6)
				{
					newOffset.y = Mathf.SmoothDamp(newOffset.y, this.m_LowBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastVerticalTouchScroll);
					bool flag7 = Mathf.Abs(this.m_SpringBackVelocity.y) < base.scaledPixelsPerPoint;
					if (flag7)
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				else
				{
					bool flag8 = newOffset.y > this.m_HighBounds.y;
					if (flag8)
					{
						newOffset.y = Mathf.SmoothDamp(newOffset.y, this.m_HighBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastVerticalTouchScroll);
						bool flag9 = Mathf.Abs(this.m_SpringBackVelocity.y) < base.scaledPixelsPerPoint;
						if (flag9)
						{
							this.m_SpringBackVelocity.y = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				this.scrollOffset = newOffset;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002E4B8 File Offset: 0x0002C6B8
		internal void ApplyScrollInertia()
		{
			bool flag = this.hasInertia && this.m_Velocity != Vector2.zero;
			if (flag)
			{
				Vector2 additionalOffset = Vector2.zero;
				float cumulativeDeltaTimeCovered = 0f;
				while (cumulativeDeltaTimeCovered < this.elapsedTimeSinceLastVerticalTouchScroll)
				{
					this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, this.k_TouchScrollInertiaBaseTimeInterval);
					cumulativeDeltaTimeCovered += this.k_TouchScrollInertiaBaseTimeInterval;
					additionalOffset += this.m_Velocity * this.k_TouchScrollInertiaBaseTimeInterval;
				}
				float remainingTimeDifference = this.elapsedTimeSinceLastVerticalTouchScroll - cumulativeDeltaTimeCovered;
				bool flag2 = remainingTimeDifference > 0f && remainingTimeDifference < this.k_TouchScrollInertiaBaseTimeInterval;
				if (flag2)
				{
					this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, remainingTimeDifference);
					additionalOffset += this.m_Velocity * remainingTimeDifference;
				}
				float scaledSpeedLimit = base.scaledPixelsPerPoint * this.k_ScaledPixelsPerPointMultiplier;
				bool flag3 = Mathf.Abs(this.m_Velocity.x) <= scaledSpeedLimit || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.x < this.m_LowBounds.x || this.scrollOffset.x > this.m_HighBounds.x));
				if (flag3)
				{
					this.m_Velocity.x = 0f;
				}
				bool flag4 = Mathf.Abs(this.m_Velocity.y) <= scaledSpeedLimit || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.y < this.m_LowBounds.y || this.scrollOffset.y > this.m_HighBounds.y));
				if (flag4)
				{
					this.m_Velocity.y = 0f;
				}
				this.scrollOffset += additionalOffset;
			}
			else
			{
				this.m_Velocity = Vector2.zero;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		private void PostPointerUpAnimation()
		{
			this.elapsedTimeSinceLastVerticalTouchScroll = Time.unscaledTime - this.previousVerticalTouchScrollTimeStamp;
			this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
			this.elapsedTimeSinceLastHorizontalTouchScroll = Time.unscaledTime - this.previousHorizontalTouchScrollTimeStamp;
			this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
			this.ApplyScrollInertia();
			this.SpringBack();
			bool flag = this.m_SpringBackVelocity == Vector2.zero && this.m_Velocity == Vector2.zero;
			if (flag)
			{
				this.m_PostPointerUpAnimation.Pause();
				this.elapsedTimeSinceLastVerticalTouchScroll = 0f;
				this.elapsedTimeSinceLastHorizontalTouchScroll = 0f;
				this.previousVerticalTouchScrollTimeStamp = 0f;
				this.previousHorizontalTouchScrollTimeStamp = 0f;
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002E76C File Offset: 0x0002C96C
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary;
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.invalidPointerId;
				if (flag2)
				{
					this.ReleaseScrolling(evt.pointerId, evt.target);
				}
				IVisualElementScheduledItem postPointerUpAnimation = this.m_PostPointerUpAnimation;
				if (postPointerUpAnimation != null)
				{
					postPointerUpAnimation.Pause();
				}
				bool touchStopsVelocityOnly = Mathf.Abs(this.m_Velocity.x) > 10f || Mathf.Abs(this.m_Velocity.y) > 10f;
				this.m_TouchPointerMoveAllowed = true;
				this.m_StartedMoving = false;
				this.InitTouchScrolling(evt.position);
				bool flag3 = touchStopsVelocityOnly;
				if (flag3)
				{
					this.contentContainer.CapturePointer(evt.pointerId);
					this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
					evt.StopPropagation();
					this.m_TouchStoppedVelocity = true;
				}
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002E86C File Offset: 0x0002CA6C
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary || !this.m_TouchPointerMoveAllowed;
			if (!flag)
			{
				bool isHandledByDraggable = evt.isHandledByDraggable;
				if (isHandledByDraggable)
				{
					this.m_PointerStartPosition = evt.position;
					this.m_StartPosition = this.scrollOffset;
				}
				else
				{
					Vector2 position = evt.position;
					Vector2 delta = position - this.m_PointerStartPosition;
					bool flag2 = this.mode == ScrollViewMode.Horizontal;
					if (flag2)
					{
						delta.y = 0f;
					}
					else
					{
						bool flag3 = this.mode == ScrollViewMode.Vertical;
						if (flag3)
						{
							delta.x = 0f;
						}
					}
					bool flag4 = !this.m_TouchStoppedVelocity && !this.m_StartedMoving && delta.sqrMagnitude < 100f;
					if (!flag4)
					{
						ScrollView.TouchScrollingResult scrollResult = this.ComputeTouchScrolling(evt.position);
						bool flag5 = scrollResult != ScrollView.TouchScrollingResult.Forward;
						if (flag5)
						{
							evt.isHandledByDraggable = true;
							evt.StopPropagation();
							bool flag6 = !this.contentContainer.HasPointerCapture(evt.pointerId);
							if (flag6)
							{
								this.contentContainer.CapturePointer(evt.pointerId);
							}
						}
						else
						{
							this.m_Velocity = Vector2.zero;
						}
					}
				}
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002E9BD File Offset: 0x0002CBBD
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002E9D4 File Offset: 0x0002CBD4
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = this.ReleaseScrolling(evt.pointerId, evt.target);
			if (flag)
			{
				this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				evt.StopPropagation();
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002EA18 File Offset: 0x0002CC18
		internal void InitTouchScrolling(Vector2 position)
		{
			this.m_PointerStartPosition = position;
			this.m_StartPosition = this.scrollOffset;
			this.m_Velocity = Vector2.zero;
			this.m_SpringBackVelocity = Vector2.zero;
			this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
			this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		internal ScrollView.TouchScrollingResult ComputeTouchScrolling(Vector2 position)
		{
			bool flag = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
			Vector2 newScrollOffset;
			if (flag)
			{
				newScrollOffset = this.m_StartPosition - (position - this.m_PointerStartPosition);
				newScrollOffset = Vector2.Max(newScrollOffset, this.m_LowBounds);
				newScrollOffset = Vector2.Min(newScrollOffset, this.m_HighBounds);
			}
			else
			{
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
				if (flag2)
				{
					Vector2 deltaPointer = position - this.m_PointerStartPosition;
					newScrollOffset.x = ScrollView.ComputeElasticOffset(deltaPointer.x, this.m_StartPosition.x, this.m_LowBounds.x, this.m_LowBounds.x - this.contentViewport.resolvedStyle.width, this.m_HighBounds.x, this.m_HighBounds.x + this.contentViewport.resolvedStyle.width);
					newScrollOffset.y = ScrollView.ComputeElasticOffset(deltaPointer.y, this.m_StartPosition.y, this.m_LowBounds.y, this.m_LowBounds.y - this.contentViewport.resolvedStyle.height, this.m_HighBounds.y, this.m_HighBounds.y + this.contentViewport.resolvedStyle.height);
					this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
					this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
				}
				else
				{
					newScrollOffset = this.m_StartPosition - (position - this.m_PointerStartPosition);
				}
			}
			bool flag3 = this.mode == ScrollViewMode.Vertical;
			if (flag3)
			{
				newScrollOffset.x = this.m_LowBounds.x;
			}
			else
			{
				bool flag4 = this.mode == ScrollViewMode.Horizontal;
				if (flag4)
				{
					newScrollOffset.y = this.m_LowBounds.y;
				}
			}
			bool shouldScrollOffsetChange = this.scrollOffset != newScrollOffset;
			bool flag5 = shouldScrollOffsetChange;
			ScrollView.TouchScrollingResult touchScrollingResult;
			if (flag5)
			{
				touchScrollingResult = (this.ApplyTouchScrolling(newScrollOffset) ? ScrollView.TouchScrollingResult.Apply : ScrollView.TouchScrollingResult.Forward);
			}
			else
			{
				touchScrollingResult = ((this.m_StartedMoving && this.nestedInteractionKind != ScrollView.NestedInteractionKind.ForwardScrolling) ? ScrollView.TouchScrollingResult.Block : ScrollView.TouchScrollingResult.Forward);
			}
			return touchScrollingResult;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002ECE4 File Offset: 0x0002CEE4
		private bool ApplyTouchScrolling(Vector2 newScrollOffset)
		{
			this.m_StartedMoving = true;
			bool hasInertia = this.hasInertia;
			if (hasInertia)
			{
				bool flag = newScrollOffset == this.m_LowBounds || newScrollOffset == this.m_HighBounds;
				if (flag)
				{
					this.m_Velocity = Vector2.zero;
					this.scrollOffset = newScrollOffset;
					return false;
				}
				bool flag2 = this.m_LastVelocityLerpTime > 0f;
				if (flag2)
				{
					float deltaTimeSinceLastLerp = Time.unscaledTime - this.m_LastVelocityLerpTime;
					this.m_Velocity = Vector2.Lerp(this.m_Velocity, Vector2.zero, deltaTimeSinceLastLerp * 10f);
				}
				this.m_LastVelocityLerpTime = Time.unscaledTime;
				float deltaTime = this.k_TouchScrollInertiaBaseTimeInterval;
				Vector2 newVelocity = (newScrollOffset - this.scrollOffset) / deltaTime;
				this.m_Velocity = Vector2.Lerp(this.m_Velocity, newVelocity, deltaTime * 10f);
			}
			bool scrollOffsetChanged = this.scrollOffset != newScrollOffset;
			this.scrollOffset = newScrollOffset;
			return scrollOffsetChanged;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002EDE4 File Offset: 0x0002CFE4
		private bool ReleaseScrolling(int pointerId, IEventHandler target)
		{
			this.m_TouchStoppedVelocity = false;
			this.m_StartedMoving = false;
			this.m_TouchPointerMoveAllowed = false;
			bool flag = target != this.contentContainer || !this.contentContainer.HasPointerCapture(pointerId);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
				this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
				bool flag3 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic || this.hasInertia;
				if (flag3)
				{
					this.ExecuteElasticSpringAnimation();
				}
				this.contentContainer.ReleasePointer(pointerId);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002EE74 File Offset: 0x0002D074
		private void ExecuteElasticSpringAnimation()
		{
			this.ComputeInitialSpringBackVelocity();
			bool flag = this.m_PostPointerUpAnimation == null;
			if (flag)
			{
				this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(this.m_ElasticAnimationIntervalMs);
			}
			else
			{
				this.m_PostPointerUpAnimation.Resume();
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002EED0 File Offset: 0x0002D0D0
		private void AdjustScrollers()
		{
			float horizontalFactor = ((this.contentContainer.boundingBox.width > 1E-30f) ? (this.contentViewport.layout.width / this.contentContainer.boundingBox.width) : 1f);
			float verticalFactor = ((this.contentContainer.boundingBox.height > 1E-30f) ? (this.contentViewport.layout.height / this.contentContainer.boundingBox.height) : 1f);
			this.horizontalScroller.Adjust(horizontalFactor);
			this.verticalScroller.Adjust(verticalFactor);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002EF8C File Offset: 0x0002D18C
		internal void UpdateScrollers(bool displayHorizontal, bool displayVertical)
		{
			this.AdjustScrollers();
			this.horizontalScroller.SetEnabled(this.contentContainer.boundingBox.width - this.contentViewport.layout.width > 0f);
			this.verticalScroller.SetEnabled(this.contentContainer.boundingBox.height - this.contentViewport.layout.height > 0f);
			bool newShowHorizontal = displayHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
			bool newShowVertical = displayVertical && this.m_VerticalScrollerVisibility != ScrollerVisibility.Hidden;
			DisplayStyle newHorizontalDisplay = (newShowHorizontal ? DisplayStyle.Flex : DisplayStyle.None);
			DisplayStyle newVerticalDisplay = (newShowVertical ? DisplayStyle.Flex : DisplayStyle.None);
			bool flag = newHorizontalDisplay != this.horizontalScroller.style.display;
			if (flag)
			{
				this.horizontalScroller.style.display = newHorizontalDisplay;
			}
			bool flag2 = newVerticalDisplay != this.verticalScroller.style.display;
			if (flag2)
			{
				this.verticalScroller.style.display = newVerticalDisplay;
			}
			this.verticalScroller.lowValue = 0f;
			this.verticalScroller.highValue = this.scrollableHeight;
			this.horizontalScroller.lowValue = 0f;
			this.horizontalScroller.highValue = this.scrollableWidth;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002F10C File Offset: 0x0002D30C
		private void OnScrollersGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool newShowHorizontal = this.needsHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
				bool flag2 = newShowHorizontal;
				if (flag2)
				{
					this.horizontalScroller.style.marginRight = this.verticalScroller.layout.width;
				}
				this.AdjustScrollers();
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002F194 File Offset: 0x0002D394
		private void OnScrollWheel(WheelEvent evt)
		{
			bool updateContentViewTransform = false;
			bool canUseVerticalScroll = this.mode != ScrollViewMode.Horizontal && this.contentContainer.boundingBox.height - base.layout.height > 0f;
			bool canUseHorizontalScroll = this.mode != ScrollViewMode.Vertical && this.contentContainer.boundingBox.width - base.layout.width > 0f;
			float horizontalScrollDelta = ((canUseHorizontalScroll && !canUseVerticalScroll) ? evt.delta.y : evt.delta.x);
			float mouseScrollFactor = (this.m_MouseWheelScrollSizeIsInline ? this.mouseWheelScrollSize : this.m_SingleLineHeight);
			bool flag = canUseVerticalScroll;
			if (flag)
			{
				float oldVerticalValue = this.verticalScroller.value;
				this.verticalScroller.value += evt.delta.y * ((this.verticalScroller.lowValue < this.verticalScroller.highValue) ? 1f : (-1f)) * mouseScrollFactor;
				bool flag2 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.verticalScroller.value, oldVerticalValue);
				if (flag2)
				{
					evt.StopPropagation();
					updateContentViewTransform = true;
				}
			}
			bool flag3 = canUseHorizontalScroll;
			if (flag3)
			{
				float oldHorizontalValue = this.horizontalScroller.value;
				this.horizontalScroller.value += horizontalScrollDelta * ((this.horizontalScroller.lowValue < this.horizontalScroller.highValue) ? 1f : (-1f)) * mouseScrollFactor;
				bool flag4 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.horizontalScroller.value, oldHorizontalValue);
				if (flag4)
				{
					evt.StopPropagation();
					updateContentViewTransform = true;
				}
			}
			bool flag5 = updateContentViewTransform;
			if (flag5)
			{
				this.UpdateElasticBehaviour();
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002F376 File Offset: 0x0002D576
		private void OnRootCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			this.ReadSingleLineHeight();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002F380 File Offset: 0x0002D580
		private void OnRootPointerUp(PointerUpEvent evt)
		{
			this.m_TouchPointerMoveAllowed = false;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002F38C File Offset: 0x0002D58C
		private void ReadSingleLineHeight()
		{
			VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
			StylePropertyValue customProp;
			bool flag = ((attachedRootVisualContainer != null) ? attachedRootVisualContainer.computedStyle.customProperties : null) != null && this.m_AttachedRootVisualContainer.computedStyle.customProperties.TryGetValue("--unity-metrics-single_line-height", out customProp);
			if (flag)
			{
				Dimension dimension;
				bool flag2 = customProp.sheet.TryReadDimension(customProp.handle, out dimension);
				if (flag2)
				{
					this.m_SingleLineHeight = dimension.value;
				}
			}
			else
			{
				this.m_SingleLineHeight = UIElementsUtility.singleLineHeight;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002F40C File Offset: 0x0002D60C
		private void UpdateElasticBehaviour()
		{
			bool flag = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.ExecuteElasticSpringAnimation();
			}
		}

		// Token: 0x040005EE RID: 1518
		internal static readonly BindingId horizontalScrollerVisibilityProperty = "horizontalScrollerVisibility";

		// Token: 0x040005EF RID: 1519
		internal static readonly BindingId verticalScrollerVisibilityProperty = "verticalScrollerVisibility";

		// Token: 0x040005F0 RID: 1520
		internal static readonly BindingId scrollOffsetProperty = "scrollOffset";

		// Token: 0x040005F1 RID: 1521
		internal static readonly BindingId horizontalPageSizeProperty = "horizontalPageSize";

		// Token: 0x040005F2 RID: 1522
		internal static readonly BindingId verticalPageSizeProperty = "verticalPageSize";

		// Token: 0x040005F3 RID: 1523
		internal static readonly BindingId mouseWheelScrollSizeProperty = "mouseWheelScrollSize";

		// Token: 0x040005F4 RID: 1524
		internal static readonly BindingId scrollDecelerationRateProperty = "scrollDecelerationRate";

		// Token: 0x040005F5 RID: 1525
		internal static readonly BindingId elasticityProperty = "elasticity";

		// Token: 0x040005F6 RID: 1526
		internal static readonly BindingId touchScrollBehaviorProperty = "touchScrollBehavior";

		// Token: 0x040005F7 RID: 1527
		internal static readonly BindingId nestedInteractionKindProperty = "nestedInteractionKind";

		// Token: 0x040005F8 RID: 1528
		internal static readonly BindingId modeProperty = "mode";

		// Token: 0x040005F9 RID: 1529
		internal static readonly BindingId elasticAnimationIntervalMsProperty = "elasticAnimationIntervalMs";

		// Token: 0x040005FA RID: 1530
		private int m_FirstLayoutPass = -1;

		// Token: 0x040005FB RID: 1531
		private ScrollerVisibility m_HorizontalScrollerVisibility;

		// Token: 0x040005FC RID: 1532
		private ScrollerVisibility m_VerticalScrollerVisibility;

		// Token: 0x040005FD RID: 1533
		private long m_ElasticAnimationIntervalMs = 16L;

		// Token: 0x040005FE RID: 1534
		private VisualElement m_AttachedRootVisualContainer;

		// Token: 0x040005FF RID: 1535
		private float m_SingleLineHeight = UIElementsUtility.singleLineHeight;

		// Token: 0x04000600 RID: 1536
		internal bool m_MouseWheelScrollSizeIsInline;

		// Token: 0x04000601 RID: 1537
		private float m_HorizontalPageSize;

		// Token: 0x04000602 RID: 1538
		private float m_VerticalPageSize;

		// Token: 0x04000603 RID: 1539
		private float m_MouseWheelScrollSize = 18f;

		// Token: 0x04000604 RID: 1540
		private static readonly float k_DefaultScrollDecelerationRate = 0.135f;

		// Token: 0x04000605 RID: 1541
		private float m_ScrollDecelerationRate = ScrollView.k_DefaultScrollDecelerationRate;

		// Token: 0x04000606 RID: 1542
		private float k_ScaledPixelsPerPointMultiplier = 10f;

		// Token: 0x04000607 RID: 1543
		private float k_TouchScrollInertiaBaseTimeInterval = 0.004167f;

		// Token: 0x04000608 RID: 1544
		private static readonly float k_DefaultElasticity = 0.1f;

		// Token: 0x04000609 RID: 1545
		private float m_Elasticity = ScrollView.k_DefaultElasticity;

		// Token: 0x0400060A RID: 1546
		private ScrollView.TouchScrollBehavior m_TouchScrollBehavior;

		// Token: 0x0400060B RID: 1547
		private ScrollView.NestedInteractionKind m_NestedInteractionKind;

		// Token: 0x0400060F RID: 1551
		private VisualElement m_ContentContainer;

		// Token: 0x04000610 RID: 1552
		private VisualElement m_ContentAndVerticalScrollContainer;

		// Token: 0x04000611 RID: 1553
		private float previousVerticalTouchScrollTimeStamp = 0f;

		// Token: 0x04000612 RID: 1554
		private float previousHorizontalTouchScrollTimeStamp = 0f;

		// Token: 0x04000613 RID: 1555
		private float elapsedTimeSinceLastVerticalTouchScroll = 0f;

		// Token: 0x04000614 RID: 1556
		private float elapsedTimeSinceLastHorizontalTouchScroll = 0f;

		// Token: 0x04000615 RID: 1557
		public static readonly string ussClassName = "unity-scroll-view";

		// Token: 0x04000616 RID: 1558
		public static readonly string viewportUssClassName = ScrollView.ussClassName + "__content-viewport";

		// Token: 0x04000617 RID: 1559
		public static readonly string horizontalVariantViewportUssClassName = ScrollView.viewportUssClassName + "--horizontal";

		// Token: 0x04000618 RID: 1560
		public static readonly string verticalVariantViewportUssClassName = ScrollView.viewportUssClassName + "--vertical";

		// Token: 0x04000619 RID: 1561
		public static readonly string verticalHorizontalVariantViewportUssClassName = ScrollView.viewportUssClassName + "--vertical-horizontal";

		// Token: 0x0400061A RID: 1562
		public static readonly string contentAndVerticalScrollUssClassName = ScrollView.ussClassName + "__content-and-vertical-scroll-container";

		// Token: 0x0400061B RID: 1563
		public static readonly string contentUssClassName = ScrollView.ussClassName + "__content-container";

		// Token: 0x0400061C RID: 1564
		public static readonly string horizontalVariantContentUssClassName = ScrollView.contentUssClassName + "--horizontal";

		// Token: 0x0400061D RID: 1565
		public static readonly string verticalVariantContentUssClassName = ScrollView.contentUssClassName + "--vertical";

		// Token: 0x0400061E RID: 1566
		public static readonly string verticalHorizontalVariantContentUssClassName = ScrollView.contentUssClassName + "--vertical-horizontal";

		// Token: 0x0400061F RID: 1567
		public static readonly string hScrollerUssClassName = ScrollView.ussClassName + "__horizontal-scroller";

		// Token: 0x04000620 RID: 1568
		public static readonly string vScrollerUssClassName = ScrollView.ussClassName + "__vertical-scroller";

		// Token: 0x04000621 RID: 1569
		public static readonly string horizontalVariantUssClassName = ScrollView.ussClassName + "--horizontal";

		// Token: 0x04000622 RID: 1570
		public static readonly string verticalVariantUssClassName = ScrollView.ussClassName + "--vertical";

		// Token: 0x04000623 RID: 1571
		public static readonly string verticalHorizontalVariantUssClassName = ScrollView.ussClassName + "--vertical-horizontal";

		// Token: 0x04000624 RID: 1572
		public static readonly string scrollVariantUssClassName = ScrollView.ussClassName + "--scroll";

		// Token: 0x04000625 RID: 1573
		private ScrollViewMode m_Mode;

		// Token: 0x04000626 RID: 1574
		private IVisualElementScheduledItem m_ScheduledLayoutPassResetItem;

		// Token: 0x04000627 RID: 1575
		private Vector2 m_StartPosition;

		// Token: 0x04000628 RID: 1576
		private Vector2 m_PointerStartPosition;

		// Token: 0x04000629 RID: 1577
		private Vector2 m_Velocity;

		// Token: 0x0400062A RID: 1578
		private Vector2 m_SpringBackVelocity;

		// Token: 0x0400062B RID: 1579
		private Vector2 m_LowBounds;

		// Token: 0x0400062C RID: 1580
		private Vector2 m_HighBounds;

		// Token: 0x0400062D RID: 1581
		private float m_LastVelocityLerpTime;

		// Token: 0x0400062E RID: 1582
		private bool m_StartedMoving;

		// Token: 0x0400062F RID: 1583
		private bool m_TouchPointerMoveAllowed;

		// Token: 0x04000630 RID: 1584
		private bool m_TouchStoppedVelocity;

		// Token: 0x04000631 RID: 1585
		private VisualElement m_CapturedTarget;

		// Token: 0x04000632 RID: 1586
		private EventCallback<PointerMoveEvent> m_CapturedTargetPointerMoveCallback;

		// Token: 0x04000633 RID: 1587
		private EventCallback<PointerUpEvent> m_CapturedTargetPointerUpCallback;

		// Token: 0x04000634 RID: 1588
		internal IVisualElementScheduledItem m_PostPointerUpAnimation;

		// Token: 0x0200013C RID: 316
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ScrollView, ScrollView.UxmlTraits>
		{
		}

		// Token: 0x0200013D RID: 317
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x060009BC RID: 2492 RVA: 0x0002F710 File Offset: 0x0002D910
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ScrollView scrollView = (ScrollView)ve;
				scrollView.mode = this.m_ScrollViewMode.GetValueFromBag(bag, cc);
				ScrollerVisibility horizontalVisibility = ScrollerVisibility.Auto;
				bool flag = this.m_HorizontalScrollerVisibility.TryGetValueFromBag(bag, cc, ref horizontalVisibility);
				if (flag)
				{
					scrollView.horizontalScrollerVisibility = horizontalVisibility;
				}
				else
				{
					scrollView.showHorizontal = this.m_ShowHorizontal.GetValueFromBag(bag, cc);
				}
				ScrollerVisibility verticalVisibility = ScrollerVisibility.Auto;
				bool flag2 = this.m_VerticalScrollerVisibility.TryGetValueFromBag(bag, cc, ref verticalVisibility);
				if (flag2)
				{
					scrollView.verticalScrollerVisibility = verticalVisibility;
				}
				else
				{
					scrollView.showVertical = this.m_ShowVertical.GetValueFromBag(bag, cc);
				}
				scrollView.nestedInteractionKind = this.m_NestedInteractionKind.GetValueFromBag(bag, cc);
				scrollView.horizontalPageSize = this.m_HorizontalPageSize.GetValueFromBag(bag, cc);
				scrollView.verticalPageSize = this.m_VerticalPageSize.GetValueFromBag(bag, cc);
				scrollView.mouseWheelScrollSize = this.m_MouseWheelScrollSize.GetValueFromBag(bag, cc);
				scrollView.scrollDecelerationRate = this.m_ScrollDecelerationRate.GetValueFromBag(bag, cc);
				scrollView.touchScrollBehavior = this.m_TouchScrollBehavior.GetValueFromBag(bag, cc);
				scrollView.elasticity = this.m_Elasticity.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000635 RID: 1589
			private UxmlEnumAttributeDescription<ScrollViewMode> m_ScrollViewMode = new UxmlEnumAttributeDescription<ScrollViewMode>
			{
				name = "mode",
				defaultValue = ScrollViewMode.Vertical
			};

			// Token: 0x04000636 RID: 1590
			private UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind> m_NestedInteractionKind = new UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind>
			{
				name = "nested-interaction-kind",
				defaultValue = ScrollView.NestedInteractionKind.Default
			};

			// Token: 0x04000637 RID: 1591
			private UxmlBoolAttributeDescription m_ShowHorizontal = new UxmlBoolAttributeDescription
			{
				name = "show-horizontal-scroller"
			};

			// Token: 0x04000638 RID: 1592
			private UxmlBoolAttributeDescription m_ShowVertical = new UxmlBoolAttributeDescription
			{
				name = "show-vertical-scroller"
			};

			// Token: 0x04000639 RID: 1593
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_HorizontalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "horizontal-scroller-visibility"
			};

			// Token: 0x0400063A RID: 1594
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_VerticalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "vertical-scroller-visibility"
			};

			// Token: 0x0400063B RID: 1595
			private UxmlFloatAttributeDescription m_HorizontalPageSize = new UxmlFloatAttributeDescription
			{
				name = "horizontal-page-size",
				defaultValue = -1f
			};

			// Token: 0x0400063C RID: 1596
			private UxmlFloatAttributeDescription m_VerticalPageSize = new UxmlFloatAttributeDescription
			{
				name = "vertical-page-size",
				defaultValue = -1f
			};

			// Token: 0x0400063D RID: 1597
			private UxmlFloatAttributeDescription m_MouseWheelScrollSize = new UxmlFloatAttributeDescription
			{
				name = "mouse-wheel-scroll-size",
				defaultValue = 18f
			};

			// Token: 0x0400063E RID: 1598
			private UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior> m_TouchScrollBehavior = new UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior>
			{
				name = "touch-scroll-type",
				defaultValue = ScrollView.TouchScrollBehavior.Clamped
			};

			// Token: 0x0400063F RID: 1599
			private UxmlFloatAttributeDescription m_ScrollDecelerationRate = new UxmlFloatAttributeDescription
			{
				name = "scroll-deceleration-rate",
				defaultValue = ScrollView.k_DefaultScrollDecelerationRate
			};

			// Token: 0x04000640 RID: 1600
			private UxmlFloatAttributeDescription m_Elasticity = new UxmlFloatAttributeDescription
			{
				name = "elasticity",
				defaultValue = ScrollView.k_DefaultElasticity
			};
		}

		// Token: 0x0200013E RID: 318
		public enum TouchScrollBehavior
		{
			// Token: 0x04000642 RID: 1602
			Unrestricted,
			// Token: 0x04000643 RID: 1603
			Elastic,
			// Token: 0x04000644 RID: 1604
			Clamped
		}

		// Token: 0x0200013F RID: 319
		public enum NestedInteractionKind
		{
			// Token: 0x04000646 RID: 1606
			Default,
			// Token: 0x04000647 RID: 1607
			StopScrolling,
			// Token: 0x04000648 RID: 1608
			ForwardScrolling
		}

		// Token: 0x02000140 RID: 320
		internal enum TouchScrollingResult
		{
			// Token: 0x0400064A RID: 1610
			Apply,
			// Token: 0x0400064B RID: 1611
			Forward,
			// Token: 0x0400064C RID: 1612
			Block
		}
	}
}
