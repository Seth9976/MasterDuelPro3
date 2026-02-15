using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FC RID: 252
	public class MinMaxSlider : BaseField<Vector2>
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00024B7A File Offset: 0x00022D7A
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x00024B82 File Offset: 0x00022D82
		internal VisualElement dragElement { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00024B8B File Offset: 0x00022D8B
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x00024B93 File Offset: 0x00022D93
		internal VisualElement dragMinThumb { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00024B9C File Offset: 0x00022D9C
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00024BA4 File Offset: 0x00022DA4
		internal VisualElement dragMaxThumb { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00024BAD File Offset: 0x00022DAD
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x00024BB5 File Offset: 0x00022DB5
		internal ClampedDragger<float> clampedDragger { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00024BC0 File Offset: 0x00022DC0
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00024BE0 File Offset: 0x00022DE0
		[CreateProperty]
		public float minValue
		{
			get
			{
				return this.value.x;
			}
			set
			{
				float previous = this.minValue;
				base.value = this.ClampValues(new Vector2(value, base.rawValue.y));
				bool flag = !Mathf.Approximately(previous, this.minValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in MinMaxSlider.minValueProperty);
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00024C34 File Offset: 0x00022E34
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00024C54 File Offset: 0x00022E54
		[CreateProperty]
		public float maxValue
		{
			get
			{
				return this.value.y;
			}
			set
			{
				float previous = this.maxValue;
				base.value = this.ClampValues(new Vector2(base.rawValue.x, value));
				bool flag = !Mathf.Approximately(previous, this.maxValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in MinMaxSlider.maxValueProperty);
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00024CA8 File Offset: 0x00022EA8
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x00024CC0 File Offset: 0x00022EC0
		public override Vector2 value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = this.ClampValues(value);
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00024CD1 File Offset: 0x00022ED1
		public override void SetValueWithoutNotify(Vector2 newValue)
		{
			base.SetValueWithoutNotify(this.ClampValues(newValue));
			this.UpdateDragElementPosition();
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00024CEC File Offset: 0x00022EEC
		[CreateProperty(ReadOnly = true)]
		public float range
		{
			get
			{
				return Math.Abs(this.highLimit - this.lowLimit);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00024D10 File Offset: 0x00022F10
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00024D28 File Offset: 0x00022F28
		[CreateProperty]
		public float lowLimit
		{
			get
			{
				return this.m_MinLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MinLimit, value);
				if (flag)
				{
					bool flag2 = value > this.m_MaxLimit;
					if (flag2)
					{
						throw new ArgumentException("lowLimit is greater than highLimit");
					}
					this.m_MinLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
					base.NotifyPropertyChanged(in MinMaxSlider.lowLimitProperty);
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00024DA4 File Offset: 0x00022FA4
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00024DBC File Offset: 0x00022FBC
		[CreateProperty]
		public float highLimit
		{
			get
			{
				return this.m_MaxLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MaxLimit, value);
				if (flag)
				{
					bool flag2 = value < this.m_MinLimit;
					if (flag2)
					{
						throw new ArgumentException("highLimit is smaller than lowLimit");
					}
					this.m_MaxLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
					base.NotifyPropertyChanged(in MinMaxSlider.highLimitProperty);
				}
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00024E38 File Offset: 0x00023038
		public MinMaxSlider()
			: this(null, 0f, 10f, float.MinValue, float.MaxValue)
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00024E58 File Offset: 0x00023058
		public MinMaxSlider(string label, float minValue = 0f, float maxValue = 10f, float minLimit = -3.4028235E+38f, float maxLimit = 3.4028235E+38f)
			: base(label, null)
		{
			this.m_MinLimit = float.MinValue;
			this.m_MaxLimit = float.MaxValue;
			this.lowLimit = minLimit;
			this.highLimit = maxLimit;
			Vector2 clampedValue = this.ClampValues(new Vector2(minValue, maxValue));
			this.minValue = clampedValue.x;
			this.maxValue = clampedValue.y;
			base.AddToClassList(MinMaxSlider.ussClassName);
			base.labelElement.AddToClassList(MinMaxSlider.labelUssClassName);
			base.visualInput.AddToClassList(MinMaxSlider.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.m_DragState = MinMaxSlider.DragState.NoThumb;
			base.visualInput.pickingMode = PickingMode.Position;
			VisualElement trackElement = new VisualElement
			{
				name = "unity-tracker"
			};
			trackElement.AddToClassList(MinMaxSlider.trackerUssClassName);
			base.visualInput.Add(trackElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.AddToClassList(MinMaxSlider.draggerUssClassName);
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			base.visualInput.Add(this.dragElement);
			this.dragMinThumb = new VisualElement
			{
				name = "unity-thumb-min"
			};
			this.dragMaxThumb = new VisualElement
			{
				name = "unity-thumb-max"
			};
			this.dragMinThumb.AddToClassList(MinMaxSlider.minThumbUssClassName);
			this.dragMaxThumb.AddToClassList(MinMaxSlider.maxThumbUssClassName);
			this.dragElement.Add(this.dragMinThumb);
			this.dragElement.Add(this.dragMaxThumb);
			this.clampedDragger = new ClampedDragger<float>(null, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			base.visualInput.AddManipulator(this.clampedDragger);
			this.m_MinLimit = minLimit;
			this.m_MaxLimit = maxLimit;
			base.rawValue = this.ClampValues(new Vector2(minValue, maxValue));
			this.UpdateDragElementPosition();
			base.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnFocusIn), TrickleDown.NoTrickleDown);
			base.RegisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnBlur), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000250B0 File Offset: 0x000232B0
		private Vector2 ClampValues(Vector2 valueToClamp)
		{
			bool flag = this.m_MinLimit > this.m_MaxLimit;
			if (flag)
			{
				this.m_MinLimit = this.m_MaxLimit;
			}
			Vector2 clampedValue = default(Vector2);
			bool flag2 = valueToClamp.y > this.m_MaxLimit;
			if (flag2)
			{
				valueToClamp.y = this.m_MaxLimit;
			}
			clampedValue.x = Mathf.Clamp(valueToClamp.x, this.m_MinLimit, valueToClamp.y);
			clampedValue.y = Mathf.Clamp(valueToClamp.y, valueToClamp.x, this.m_MaxLimit);
			return clampedValue;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00025148 File Offset: 0x00023348
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00025188 File Offset: 0x00023388
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float sliderLeftBorder = this.dragElement.resolvedStyle.borderLeftWidth + this.dragElement.resolvedStyle.marginLeft;
				float sliderRightBorder = this.dragElement.resolvedStyle.borderRightWidth + this.dragElement.resolvedStyle.marginRight;
				float sliderMinWidth = sliderRightBorder + sliderLeftBorder;
				float thumbFullWidth = this.dragMinThumb.resolvedStyle.width + this.dragMaxThumb.resolvedStyle.width + sliderMinWidth;
				float newPositionLeft = Mathf.Round(this.SliderLerpUnclamped(this.dragMinThumb.resolvedStyle.width, base.visualInput.layout.width - this.dragMaxThumb.resolvedStyle.width - sliderMinWidth, this.SliderNormalizeValue(this.minValue, this.lowLimit, this.highLimit)));
				float newPositionRight = Mathf.Round(this.SliderLerpUnclamped(this.dragMinThumb.resolvedStyle.width + sliderMinWidth, base.visualInput.layout.width - this.dragMaxThumb.resolvedStyle.width, this.SliderNormalizeValue(this.maxValue, this.lowLimit, this.highLimit)));
				this.dragElement.style.width = newPositionRight - newPositionLeft;
				this.dragElement.style.left = newPositionLeft;
				this.dragMinThumb.style.left = -this.dragMinThumb.resolvedStyle.width - sliderLeftBorder;
				this.dragMaxThumb.style.right = -this.dragMaxThumb.resolvedStyle.width - sliderRightBorder;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00025354 File Offset: 0x00023554
		internal float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			return Mathf.LerpUnclamped(a, b, interpolant);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00025370 File Offset: 0x00023570
		internal float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002538C File Offset: 0x0002358C
		private float ComputeValueFromPosition(float positionToConvert)
		{
			float interpolant = this.SliderNormalizeValue(positionToConvert, 0f, base.visualInput.layout.width);
			return this.SliderLerpUnclamped(this.lowLimit, this.highLimit, interpolant);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000253D4 File Offset: 0x000235D4
		[EventInterest(new Type[] { typeof(GeometryChangedEvent) })]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<GeometryChangedEvent>.TypeId();
				if (flag2)
				{
					this.UpdateDragElementPosition((GeometryChangedEvent)evt);
				}
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00025418 File Offset: 0x00023618
		private MinMaxSlider.DragState GetNavigationState()
		{
			bool minEnabled = this.dragMinThumb.ClassListContains(MinMaxSlider.movableUssClassName);
			bool maxEnabled = this.dragMaxThumb.ClassListContains(MinMaxSlider.movableUssClassName);
			bool flag = minEnabled;
			MinMaxSlider.DragState dragState;
			if (flag)
			{
				dragState = (maxEnabled ? MinMaxSlider.DragState.MiddleThumb : MinMaxSlider.DragState.MinThumb);
			}
			else
			{
				bool flag2 = maxEnabled;
				if (flag2)
				{
					dragState = MinMaxSlider.DragState.MaxThumb;
				}
				else
				{
					dragState = MinMaxSlider.DragState.NoThumb;
				}
			}
			return dragState;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00025468 File Offset: 0x00023668
		private void SetNavigationState(MinMaxSlider.DragState newState)
		{
			this.dragMinThumb.EnableInClassList(MinMaxSlider.movableUssClassName, newState == MinMaxSlider.DragState.MinThumb || newState == MinMaxSlider.DragState.MiddleThumb);
			this.dragMaxThumb.EnableInClassList(MinMaxSlider.movableUssClassName, newState == MinMaxSlider.DragState.MaxThumb || newState == MinMaxSlider.DragState.MiddleThumb);
			this.dragElement.EnableInClassList(MinMaxSlider.movableUssClassName, newState == MinMaxSlider.DragState.MiddleThumb);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000254C4 File Offset: 0x000236C4
		private void OnFocusIn(FocusInEvent evt)
		{
			bool flag = this.GetNavigationState() == MinMaxSlider.DragState.NoThumb;
			if (flag)
			{
				this.SetNavigationState(MinMaxSlider.DragState.MinThumb);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000254E7 File Offset: 0x000236E7
		private void OnBlur(BlurEvent evt)
		{
			this.SetNavigationState(MinMaxSlider.DragState.NoThumb);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000254F4 File Offset: 0x000236F4
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			MinMaxSlider.DragState newState = this.GetNavigationState() + 1;
			bool flag = newState > MinMaxSlider.DragState.NoThumb;
			if (flag)
			{
				newState = MinMaxSlider.DragState.MinThumb;
			}
			this.SetNavigationState(newState);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00025520 File Offset: 0x00023720
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			MinMaxSlider.DragState moveState = this.GetNavigationState();
			bool flag = moveState == MinMaxSlider.DragState.NoThumb;
			if (!flag)
			{
				bool flag2 = evt.direction != NavigationMoveEvent.Direction.Left && evt.direction != NavigationMoveEvent.Direction.Right;
				if (!flag2)
				{
					this.ComputeValueFromKey(evt.direction == NavigationMoveEvent.Direction.Left, evt.shiftKey, moveState);
					evt.StopPropagation();
					FocusController focusController = this.focusController;
					if (focusController != null)
					{
						focusController.IgnoreEvent(evt);
					}
				}
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00025590 File Offset: 0x00023790
		private void ComputeValueFromKey(bool leftDirection, bool isShift, MinMaxSlider.DragState moveState)
		{
			float delta = BaseSlider<float>.GetClosestPowerOfTen(Mathf.Abs((this.highLimit - this.lowLimit) * 0.01f));
			if (isShift)
			{
				delta *= 10f;
			}
			if (leftDirection)
			{
				delta = -delta;
			}
			switch (moveState)
			{
			case MinMaxSlider.DragState.MinThumb:
			{
				float thumbValue = BaseSlider<float>.RoundToMultipleOf(this.value.x + delta * 0.5001f, Mathf.Abs(delta));
				thumbValue = Math.Clamp(thumbValue, this.lowLimit, this.value.y);
				this.value = new Vector2(thumbValue, this.value.y);
				break;
			}
			case MinMaxSlider.DragState.MaxThumb:
			{
				float thumbValue2 = BaseSlider<float>.RoundToMultipleOf(this.value.y + delta * 0.5001f, Mathf.Abs(delta));
				thumbValue2 = Math.Clamp(thumbValue2, this.value.x, this.highLimit);
				this.value = new Vector2(this.value.x, thumbValue2);
				break;
			}
			case MinMaxSlider.DragState.MiddleThumb:
			{
				float range = this.value.y - this.value.x;
				bool flag = delta > 0f;
				if (flag)
				{
					float thumbValue3 = BaseSlider<float>.RoundToMultipleOf(this.value.y + delta * 0.5001f, Mathf.Abs(delta));
					thumbValue3 = Math.Clamp(thumbValue3, this.value.x, this.highLimit);
					this.value = new Vector2(thumbValue3 - range, thumbValue3);
				}
				else
				{
					float thumbValue4 = BaseSlider<float>.RoundToMultipleOf(this.value.x + delta * 0.5001f, Mathf.Abs(delta));
					thumbValue4 = Math.Clamp(thumbValue4, this.lowLimit, this.value.y);
					this.value = new Vector2(thumbValue4, thumbValue4 + range);
				}
				break;
			}
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002576C File Offset: 0x0002396C
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				float originalPosition = this.m_DragElementStartPos.x;
				float newPosition = originalPosition + this.clampedDragger.delta.x;
				this.ComputeValueFromDraggingThumb(originalPosition, newPosition);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000257BC File Offset: 0x000239BC
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				Vector2 worldStartMousePosition = base.visualInput.LocalToWorld(this.clampedDragger.startMousePosition);
				bool flag2 = this.dragMinThumb.worldBound.Contains(worldStartMousePosition);
				if (flag2)
				{
					this.m_DragState = MinMaxSlider.DragState.MinThumb;
				}
				else
				{
					bool flag3 = this.dragMaxThumb.worldBound.Contains(worldStartMousePosition);
					if (flag3)
					{
						this.m_DragState = MinMaxSlider.DragState.MaxThumb;
					}
					else
					{
						bool flag4 = this.clampedDragger.startMousePosition.x > this.dragElement.layout.xMin && this.clampedDragger.startMousePosition.x < this.dragElement.layout.xMax;
						if (flag4)
						{
							this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
						}
						else
						{
							this.m_DragState = MinMaxSlider.DragState.NoThumb;
						}
					}
				}
				bool flag5 = this.m_DragState == MinMaxSlider.DragState.NoThumb;
				if (flag5)
				{
					float newValue = this.ComputeValueFromPosition(this.clampedDragger.startMousePosition.x);
					bool flag6 = this.clampedDragger.startMousePosition.x < this.dragElement.layout.x;
					if (flag6)
					{
						this.m_DragState = MinMaxSlider.DragState.MinThumb;
						this.value = new Vector2(newValue, this.value.y);
					}
					else
					{
						this.m_DragState = MinMaxSlider.DragState.MaxThumb;
						this.value = new Vector2(this.value.x, newValue);
					}
				}
				this.SetNavigationState(this.m_DragState);
				this.m_ValueStartPos = this.value;
				this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
				this.m_DragElementStartPos = this.clampedDragger.startMousePosition;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00025980 File Offset: 0x00023B80
		private void ComputeValueFromDraggingThumb(float dragElementStartPos, float dragElementEndPos)
		{
			float startPosInValue = this.ComputeValueFromPosition(dragElementStartPos);
			float endPosInValue = this.ComputeValueFromPosition(dragElementEndPos);
			float deltaInValueWorld = endPosInValue - startPosInValue;
			this.SetNavigationState(this.m_DragState);
			switch (this.m_DragState)
			{
			case MinMaxSlider.DragState.MinThumb:
			{
				float newPosition = this.m_ValueStartPos.x + deltaInValueWorld;
				bool flag = newPosition > this.maxValue;
				if (flag)
				{
					newPosition = this.maxValue;
				}
				else
				{
					bool flag2 = newPosition < this.lowLimit;
					if (flag2)
					{
						newPosition = this.lowLimit;
					}
				}
				this.value = new Vector2(newPosition, this.maxValue);
				break;
			}
			case MinMaxSlider.DragState.MaxThumb:
			{
				float newPosition2 = this.m_ValueStartPos.y + deltaInValueWorld;
				bool flag3 = newPosition2 < this.minValue;
				if (flag3)
				{
					newPosition2 = this.minValue;
				}
				else
				{
					bool flag4 = newPosition2 > this.highLimit;
					if (flag4)
					{
						newPosition2 = this.highLimit;
					}
				}
				this.value = new Vector2(this.minValue, newPosition2);
				break;
			}
			case MinMaxSlider.DragState.MiddleThumb:
			{
				Vector2 newValue = this.value;
				newValue.x = this.m_ValueStartPos.x + deltaInValueWorld;
				newValue.y = this.m_ValueStartPos.y + deltaInValueWorld;
				float actualDifference = this.m_ValueStartPos.y - this.m_ValueStartPos.x;
				bool flag5 = newValue.x < this.lowLimit;
				if (flag5)
				{
					newValue.x = this.lowLimit;
					newValue.y = this.lowLimit + actualDifference;
				}
				else
				{
					bool flag6 = newValue.y > this.highLimit;
					if (flag6)
					{
						newValue.y = this.highLimit;
						newValue.x = this.highLimit - actualDifference;
					}
				}
				this.value = newValue;
				break;
			}
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000020EA File Offset: 0x000002EA
		protected override void UpdateMixedValueContent()
		{
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00025B4B File Offset: 0x00023D4B
		internal override void RegisterEditingCallbacks()
		{
			base.visualInput.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.visualInput.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00025B80 File Offset: 0x00023D80
		internal override void UnregisterEditingCallbacks()
		{
			base.visualInput.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			base.visualInput.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x040004BA RID: 1210
		internal static readonly BindingId minValueProperty = "minValue";

		// Token: 0x040004BB RID: 1211
		internal static readonly BindingId maxValueProperty = "maxValue";

		// Token: 0x040004BC RID: 1212
		internal static readonly BindingId rangeProperty = "range";

		// Token: 0x040004BD RID: 1213
		internal static readonly BindingId lowLimitProperty = "lowLimit";

		// Token: 0x040004BE RID: 1214
		internal static readonly BindingId highLimitProperty = "highLimit";

		// Token: 0x040004C3 RID: 1219
		private Vector2 m_DragElementStartPos;

		// Token: 0x040004C4 RID: 1220
		private Vector2 m_ValueStartPos;

		// Token: 0x040004C5 RID: 1221
		private MinMaxSlider.DragState m_DragState;

		// Token: 0x040004C6 RID: 1222
		private float m_MinLimit;

		// Token: 0x040004C7 RID: 1223
		private float m_MaxLimit;

		// Token: 0x040004C8 RID: 1224
		public new static readonly string ussClassName = "unity-min-max-slider";

		// Token: 0x040004C9 RID: 1225
		public new static readonly string labelUssClassName = MinMaxSlider.ussClassName + "__label";

		// Token: 0x040004CA RID: 1226
		public new static readonly string inputUssClassName = MinMaxSlider.ussClassName + "__input";

		// Token: 0x040004CB RID: 1227
		public static readonly string trackerUssClassName = MinMaxSlider.ussClassName + "__tracker";

		// Token: 0x040004CC RID: 1228
		public static readonly string draggerUssClassName = MinMaxSlider.ussClassName + "__dragger";

		// Token: 0x040004CD RID: 1229
		public static readonly string minThumbUssClassName = MinMaxSlider.ussClassName + "__min-thumb";

		// Token: 0x040004CE RID: 1230
		public static readonly string maxThumbUssClassName = MinMaxSlider.ussClassName + "__max-thumb";

		// Token: 0x040004CF RID: 1231
		public static readonly string movableUssClassName = MinMaxSlider.ussClassName + "--movable";

		// Token: 0x020000FD RID: 253
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<MinMaxSlider, MinMaxSlider.UxmlTraits>
		{
		}

		// Token: 0x020000FE RID: 254
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
		{
			// Token: 0x060007DD RID: 2013 RVA: 0x00025CB0 File Offset: 0x00023EB0
			public UxmlTraits()
			{
				this.m_PickingMode.defaultValue = PickingMode.Ignore;
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x00025D60 File Offset: 0x00023F60
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MinMaxSlider slider = (MinMaxSlider)ve;
				slider.lowLimit = this.m_LowLimit.GetValueFromBag(bag, cc);
				slider.highLimit = this.m_HighLimit.GetValueFromBag(bag, cc);
				Vector2 value = new Vector2(this.m_MinValue.GetValueFromBag(bag, cc), this.m_MaxValue.GetValueFromBag(bag, cc));
				slider.value = value;
			}

			// Token: 0x040004D0 RID: 1232
			private UxmlFloatAttributeDescription m_MinValue = new UxmlFloatAttributeDescription
			{
				name = "min-value",
				defaultValue = 0f
			};

			// Token: 0x040004D1 RID: 1233
			private UxmlFloatAttributeDescription m_MaxValue = new UxmlFloatAttributeDescription
			{
				name = "max-value",
				defaultValue = 10f
			};

			// Token: 0x040004D2 RID: 1234
			private UxmlFloatAttributeDescription m_LowLimit = new UxmlFloatAttributeDescription
			{
				name = "low-limit",
				defaultValue = float.MinValue
			};

			// Token: 0x040004D3 RID: 1235
			private UxmlFloatAttributeDescription m_HighLimit = new UxmlFloatAttributeDescription
			{
				name = "high-limit",
				defaultValue = float.MaxValue
			};
		}

		// Token: 0x020000FF RID: 255
		private enum DragState
		{
			// Token: 0x040004D5 RID: 1237
			MinThumb,
			// Token: 0x040004D6 RID: 1238
			MaxThumb,
			// Token: 0x040004D7 RID: 1239
			MiddleThumb,
			// Token: 0x040004D8 RID: 1240
			NoThumb
		}
	}
}
