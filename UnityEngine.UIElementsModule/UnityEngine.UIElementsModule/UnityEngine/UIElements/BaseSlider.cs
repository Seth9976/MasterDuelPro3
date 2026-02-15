using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007A RID: 122
	public abstract class BaseSlider<TValueType> : BaseField<TValueType>, IValueField<TValueType> where TValueType : IComparable<TValueType>
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000160FA File Offset: 0x000142FA
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00016102 File Offset: 0x00014302
		internal VisualElement dragContainer { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0001610B File Offset: 0x0001430B
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00016113 File Offset: 0x00014313
		internal VisualElement dragElement { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001611C File Offset: 0x0001431C
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00016124 File Offset: 0x00014324
		internal VisualElement trackElement { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001612D File Offset: 0x0001432D
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00016135 File Offset: 0x00014335
		internal VisualElement dragBorderElement { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001613E File Offset: 0x0001433E
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00016146 File Offset: 0x00014346
		internal TextField inputTextField { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001614F File Offset: 0x0001434F
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00016157 File Offset: 0x00014357
		internal VisualElement fillElement { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00016160 File Offset: 0x00014360
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00016178 File Offset: 0x00014378
		[CreateProperty]
		public TValueType lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_LowValue, value);
				if (flag)
				{
					this.m_LowValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.lowValueProperty);
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000161C8 File Offset: 0x000143C8
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000161E0 File Offset: 0x000143E0
		[CreateProperty]
		public TValueType highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_HighValue, value);
				if (flag)
				{
					this.m_HighValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.highValueProperty);
				}
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00016230 File Offset: 0x00014430
		internal void SetHighValueWithoutNotify(TValueType newHighValue)
		{
			this.m_HighValue = newHighValue;
			TValueType newValue = (this.clamped ? this.GetClampedValue(this.value) : this.value);
			this.SetValueWithoutNotify(newValue);
			this.UpdateDragElementPosition();
			base.SaveViewData();
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00016278 File Offset: 0x00014478
		[CreateProperty(ReadOnly = true)]
		public TValueType range
		{
			get
			{
				return this.SliderRange();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00016290 File Offset: 0x00014490
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x000162A8 File Offset: 0x000144A8
		[CreateProperty]
		public virtual float pageSize
		{
			get
			{
				return this.m_PageSize;
			}
			set
			{
				bool flag = this.m_PageSize == value;
				if (!flag)
				{
					this.m_PageSize = value;
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.pageSizeProperty);
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000162D8 File Offset: 0x000144D8
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000162F0 File Offset: 0x000144F0
		[CreateProperty]
		public virtual bool showInputField
		{
			get
			{
				return this.m_ShowInputField;
			}
			set
			{
				bool flag = this.m_ShowInputField != value;
				if (flag)
				{
					this.m_ShowInputField = value;
					this.UpdateTextFieldVisibility();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.showInputFieldProperty);
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001632A File Offset: 0x0001452A
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00016334 File Offset: 0x00014534
		[CreateProperty]
		public bool fill
		{
			get
			{
				return this.m_Fill;
			}
			set
			{
				bool flag = this.m_Fill == value;
				if (!flag)
				{
					this.m_Fill = value;
					if (value)
					{
						this.UpdateDragElementPosition();
					}
					else
					{
						bool flag2 = this.fillElement != null;
						if (flag2)
						{
							this.fillElement.RemoveFromHierarchy();
							this.fillElement = null;
						}
					}
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.fillProperty);
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00016397 File Offset: 0x00014597
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001639F File Offset: 0x0001459F
		internal bool clamped { get; set; } = true;

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000163A8 File Offset: 0x000145A8
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000163B0 File Offset: 0x000145B0
		internal ClampedDragger<TValueType> clampedDragger { get; private set; }

		// Token: 0x06000483 RID: 1155 RVA: 0x000163BC File Offset: 0x000145BC
		private TValueType Clamp(TValueType value, TValueType lowBound, TValueType highBound)
		{
			TValueType result = value;
			bool flag = lowBound.CompareTo(value) > 0;
			if (flag)
			{
				result = lowBound;
			}
			else
			{
				bool flag2 = highBound.CompareTo(value) < 0;
				if (flag2)
				{
					result = highBound;
				}
			}
			return result;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016408 File Offset: 0x00014608
		private TValueType GetClampedValue(TValueType newValue)
		{
			TValueType lowest = this.lowValue;
			TValueType highest = this.highValue;
			bool flag = lowest.CompareTo(highest) > 0;
			if (flag)
			{
				TValueType t = lowest;
				lowest = highest;
				highest = t;
			}
			return this.Clamp(newValue, lowest, highest);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00016450 File Offset: 0x00014650
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00016468 File Offset: 0x00014668
		public override TValueType value
		{
			get
			{
				return base.value;
			}
			set
			{
				TValueType newValue = (this.clamped ? this.GetClampedValue(value) : value);
				base.value = newValue;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, TValueType startValue)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000020EA File Offset: 0x000002EA
		void IValueField<TValueType>.StartDragging()
		{
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000020EA File Offset: 0x000002EA
		void IValueField<TValueType>.StopDragging()
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00016494 File Offset: 0x00014694
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			TValueType clampedValue = (this.clamped ? this.GetClampedValue(newValue) : newValue);
			base.SetValueWithoutNotify(clampedValue);
			this.UpdateDragElementPosition();
			this.UpdateTextFieldValue();
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000164CC File Offset: 0x000146CC
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x000164E4 File Offset: 0x000146E4
		[CreateProperty]
		public SliderDirection direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				SliderDirection previous = this.m_Direction;
				this.m_Direction = value;
				bool flag = this.m_Direction == SliderDirection.Horizontal;
				if (flag)
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
				}
				else
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
				}
				bool flag2 = previous != this.m_Direction;
				if (flag2)
				{
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.directionProperty);
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00016560 File Offset: 0x00014760
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00016578 File Offset: 0x00014778
		[CreateProperty]
		public bool inverted
		{
			get
			{
				return this.m_Inverted;
			}
			set
			{
				bool flag = this.m_Inverted != value;
				if (flag)
				{
					this.m_Inverted = value;
					this.UpdateDragElementPosition();
					base.NotifyPropertyChanged(in BaseSlider<TValueType>.invertedProperty);
				}
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000165B4 File Offset: 0x000147B4
		internal BaseSlider(string label, TValueType start, TValueType end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, null)
		{
			base.AddToClassList(BaseSlider<TValueType>.ussClassName);
			base.labelElement.AddToClassList(BaseSlider<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(BaseSlider<TValueType>.inputUssClassName);
			this.direction = direction;
			this.pageSize = pageSize;
			this.lowValue = start;
			this.highValue = end;
			base.pickingMode = PickingMode.Ignore;
			this.dragContainer = new VisualElement
			{
				name = "unity-drag-container"
			};
			this.dragContainer.AddToClassList(BaseSlider<TValueType>.dragContainerUssClassName);
			this.dragContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			base.visualInput.Add(this.dragContainer);
			this.trackElement = new VisualElement
			{
				name = "unity-tracker",
				usageHints = UsageHints.DynamicColor
			};
			this.trackElement.AddToClassList(BaseSlider<TValueType>.trackerUssClassName);
			this.dragContainer.Add(this.trackElement);
			this.dragBorderElement = new VisualElement
			{
				name = "unity-dragger-border"
			};
			this.dragBorderElement.AddToClassList(BaseSlider<TValueType>.draggerBorderUssClassName);
			this.dragContainer.Add(this.dragBorderElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger",
				usageHints = UsageHints.DynamicTransform
			};
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			this.dragElement.AddToClassList(BaseSlider<TValueType>.draggerUssClassName);
			this.dragContainer.Add(this.dragElement);
			this.clampedDragger = new ClampedDragger<TValueType>(this, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			this.dragContainer.pickingMode = PickingMode.Position;
			this.dragContainer.AddManipulator(this.clampedDragger);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnFocusIn), TrickleDown.NoTrickleDown);
			base.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			this.UpdateTextFieldVisibility();
			FieldMouseDragger<TValueType> mouseDragger = new FieldMouseDragger<TValueType>(this);
			mouseDragger.SetDragZone(base.labelElement);
			base.labelElement.AddToClassList(BaseField<TValueType>.labelDraggerVariantUssClassName);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001683C File Offset: 0x00014A3C
		protected internal static float GetClosestPowerOfTen(float positiveNumber)
		{
			bool flag = positiveNumber <= 0f;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = Mathf.Pow(10f, (float)Mathf.RoundToInt(Mathf.Log10(positiveNumber)));
			}
			return num;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001687C File Offset: 0x00014A7C
		protected internal static float RoundToMultipleOf(float value, float roundingValue)
		{
			bool flag = roundingValue == 0f;
			float num;
			if (flag)
			{
				num = value;
			}
			else
			{
				num = Mathf.Round(value / roundingValue) * roundingValue;
			}
			return num;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000168A8 File Offset: 0x00014AA8
		private void ClampValue()
		{
			this.value = base.rawValue;
		}

		// Token: 0x06000493 RID: 1171
		internal abstract TValueType SliderLerpUnclamped(TValueType a, TValueType b, float interpolant);

		// Token: 0x06000494 RID: 1172
		internal abstract float SliderNormalizeValue(TValueType currentValue, TValueType lowerValue, TValueType higherValue);

		// Token: 0x06000495 RID: 1173
		internal abstract TValueType SliderRange();

		// Token: 0x06000496 RID: 1174
		internal abstract TValueType ParseStringToValue(string previousValue, string newValue);

		// Token: 0x06000497 RID: 1175
		internal abstract void ComputeValueFromKey(BaseSlider<TValueType>.SliderKey sliderKey, bool isShift);

		// Token: 0x06000498 RID: 1176 RVA: 0x000168B8 File Offset: 0x00014AB8
		private TValueType SliderLerpDirectionalUnclamped(TValueType a, TValueType b, float positionInterpolant)
		{
			float directionalInterpolant = ((this.direction == SliderDirection.Vertical) ? (1f - positionInterpolant) : positionInterpolant);
			bool inverted = this.inverted;
			TValueType tvalueType;
			if (inverted)
			{
				tvalueType = this.SliderLerpUnclamped(b, a, directionalInterpolant);
			}
			else
			{
				tvalueType = this.SliderLerpUnclamped(a, b, directionalInterpolant);
			}
			return tvalueType;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00016900 File Offset: 0x00014B00
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				Vector2 delta = this.clampedDragger.delta;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x + delta.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y + delta.y);
				}
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000169B8 File Offset: 0x00014BB8
		private void ComputeValueAndDirectionFromDrag(float sliderLength, float dragElementLength, float dragElementPos)
		{
			float totalRange = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(totalRange) < 1E-30f;
			if (!flag)
			{
				bool clamped = this.clamped;
				float normalizedDragElementPosition;
				if (clamped)
				{
					normalizedDragElementPosition = Mathf.Max(0f, Mathf.Min(dragElementPos, totalRange)) / totalRange;
				}
				else
				{
					normalizedDragElementPosition = dragElementPos / totalRange;
				}
				TValueType oldValue = this.value;
				this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, normalizedDragElementPosition);
				bool flag2 = EqualityComparer<TValueType>.Default.Equals(this.value, oldValue);
				if (flag2)
				{
					this.UpdateDragElementPosition();
				}
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00016A44 File Offset: 0x00014C44
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.None;
				if (flag2)
				{
					bool flag3 = Mathf.Approximately(this.pageSize, 0f);
					if (flag3)
					{
						bool flag4 = this.direction == SliderDirection.Horizontal;
						float sliderLength;
						float dragElementLength;
						float x;
						float y;
						float dragElementStartPos;
						if (flag4)
						{
							sliderLength = this.dragContainer.resolvedStyle.width;
							dragElementLength = this.dragElement.resolvedStyle.width;
							float totalRange = sliderLength - dragElementLength;
							float targetXPos = this.clampedDragger.startMousePosition.x - dragElementLength / 2f;
							x = Mathf.Max(0f, Mathf.Min(targetXPos, totalRange));
							y = this.dragElement.transform.position.y;
							dragElementStartPos = x;
						}
						else
						{
							sliderLength = this.dragContainer.resolvedStyle.height;
							dragElementLength = this.dragElement.resolvedStyle.height;
							float totalRange2 = sliderLength - dragElementLength;
							float targetYPos = this.clampedDragger.startMousePosition.y - dragElementLength / 2f;
							x = this.dragElement.transform.position.x;
							y = Mathf.Max(0f, Mathf.Min(targetYPos, totalRange2));
							dragElementStartPos = y;
						}
						Vector3 pos = new Vector3(x, y, 0f);
						this.dragElement.transform.position = pos;
						this.dragBorderElement.transform.position = pos;
						this.m_DragElementStartPos = new Rect(x, y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.Free;
						this.ComputeValueAndDirectionFromDrag(sliderLength, dragElementLength, dragElementStartPos);
						return;
					}
					this.m_DragElementStartPos = new Rect(this.dragElement.transform.position.x, this.dragElement.transform.position.y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
				}
				bool flag5 = this.direction == SliderDirection.Horizontal;
				if (flag5)
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.dragElement.transform.position.x, this.clampedDragger.lastMousePosition.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.dragElement.transform.position.y, this.clampedDragger.lastMousePosition.y);
				}
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00016D18 File Offset: 0x00014F18
		private void OnKeyDown(KeyDownEvent evt)
		{
			BaseSlider<TValueType>.SliderKey sliderKey = BaseSlider<TValueType>.SliderKey.None;
			bool isHorizontal = this.direction == SliderDirection.Horizontal;
			bool flag = (isHorizontal && evt.keyCode == KeyCode.Home) || (!isHorizontal && evt.keyCode == KeyCode.End);
			if (flag)
			{
				sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Highest : BaseSlider<TValueType>.SliderKey.Lowest);
			}
			else
			{
				bool flag2 = (isHorizontal && evt.keyCode == KeyCode.End) || (!isHorizontal && evt.keyCode == KeyCode.Home);
				if (flag2)
				{
					sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lowest : BaseSlider<TValueType>.SliderKey.Highest);
				}
				else
				{
					bool flag3 = (isHorizontal && evt.keyCode == KeyCode.PageUp) || (!isHorizontal && evt.keyCode == KeyCode.PageDown);
					if (flag3)
					{
						sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.HigherPage : BaseSlider<TValueType>.SliderKey.LowerPage);
					}
					else
					{
						bool flag4 = (isHorizontal && evt.keyCode == KeyCode.PageDown) || (!isHorizontal && evt.keyCode == KeyCode.PageUp);
						if (flag4)
						{
							sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.LowerPage : BaseSlider<TValueType>.SliderKey.HigherPage);
						}
					}
				}
			}
			bool flag5 = sliderKey == BaseSlider<TValueType>.SliderKey.None;
			if (!flag5)
			{
				this.ComputeValueFromKey(sliderKey, evt.shiftKey);
				evt.StopPropagation();
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00016E3C File Offset: 0x0001503C
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			bool flag = !this.dragElement.ClassListContains(BaseSlider<TValueType>.movableUssClassName);
			if (!flag)
			{
				BaseSlider<TValueType>.SliderKey sliderKey = BaseSlider<TValueType>.SliderKey.None;
				bool isHorizontal = this.direction == SliderDirection.Horizontal;
				bool flag2 = evt.direction == (isHorizontal ? NavigationMoveEvent.Direction.Left : NavigationMoveEvent.Direction.Down);
				if (flag2)
				{
					sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Higher : BaseSlider<TValueType>.SliderKey.Lower);
				}
				else
				{
					bool flag3 = evt.direction == (isHorizontal ? NavigationMoveEvent.Direction.Right : NavigationMoveEvent.Direction.Up);
					if (flag3)
					{
						sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lower : BaseSlider<TValueType>.SliderKey.Higher);
					}
				}
				bool flag4 = sliderKey == BaseSlider<TValueType>.SliderKey.None;
				if (!flag4)
				{
					this.ComputeValueFromKey(sliderKey, evt.shiftKey);
					evt.StopPropagation();
					FocusController focusController = this.focusController;
					if (focusController != null)
					{
						focusController.IgnoreEvent(evt);
					}
				}
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00016EEC File Offset: 0x000150EC
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			bool isEditingTextField = this.m_IsEditingTextField;
			if (!isEditingTextField)
			{
				this.dragElement.EnableInClassList(BaseSlider<TValueType>.movableUssClassName, !this.dragElement.ClassListContains(BaseSlider<TValueType>.movableUssClassName));
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00016F2C File Offset: 0x0001512C
		internal virtual void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			float totalRange = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(totalRange) < 1E-30f;
			if (!flag)
			{
				bool isPositionDecreasing = dragElementLastPos < dragElementPos;
				bool isPositionIncreasing = dragElementLastPos > dragElementPos + dragElementLength;
				bool isDraggingHighToLow = (this.inverted ? isPositionIncreasing : isPositionDecreasing);
				bool isDraggingLowToHigh = (this.inverted ? isPositionDecreasing : isPositionIncreasing);
				float adjustedPageSize = (this.inverted ? (-this.pageSize) : this.pageSize);
				bool flag2 = isDraggingHighToLow && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.LowToHigh;
				if (flag2)
				{
					this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.HighToLow;
					float normalizedDragElementPosition = Mathf.Max(0f, Mathf.Min(dragElementPos - adjustedPageSize, totalRange)) / totalRange;
					this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, normalizedDragElementPosition);
				}
				else
				{
					bool flag3 = isDraggingLowToHigh && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.HighToLow;
					if (flag3)
					{
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.LowToHigh;
						float normalizedDragElementPosition2 = Mathf.Max(0f, Mathf.Min(dragElementPos + adjustedPageSize, totalRange)) / totalRange;
						this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, normalizedDragElementPosition2);
					}
				}
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001705C File Offset: 0x0001525C
		public void AdjustDragElement(float factor)
		{
			bool needsElement = factor < 1f;
			bool flag = needsElement;
			if (flag)
			{
				this.dragElement.style.visibility = new StyleEnum<Visibility>(Visibility.Visible, StyleKeyword.Null);
				IStyle inlineStyles = this.dragElement.style;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					float elemMinWidth = ((base.resolvedStyle.minWidth == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minWidth.value);
					inlineStyles.width = Mathf.Round(Mathf.Max(this.dragContainer.layout.width * factor, elemMinWidth));
				}
				else
				{
					float elemMinHeight = ((base.resolvedStyle.minHeight == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minHeight.value);
					inlineStyles.height = Mathf.Round(Mathf.Max(this.dragContainer.layout.height * factor, elemMinHeight));
				}
			}
			else
			{
				this.dragElement.style.visibility = new StyleEnum<Visibility>(Visibility.Hidden, StyleKeyword.Undefined);
			}
			this.dragBorderElement.visible = this.dragElement.visible;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000171B0 File Offset: 0x000153B0
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000171ED File Offset: 0x000153ED
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			this.UpdateDragElementPosition();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00017200 File Offset: 0x00015400
		private bool SameValues(float a, float b, float epsilon)
		{
			return Mathf.Abs(b - a) < epsilon;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00017220 File Offset: 0x00015420
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float normalizedPosition = this.SliderNormalizeValue(this.value, this.lowValue, this.highValue);
				float directionalNormalizedPosition = (this.inverted ? (1f - normalizedPosition) : normalizedPosition);
				float halfPixel = base.scaledPixelsPerPoint * 0.5f;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					float dragElementWidth = this.dragElement.resolvedStyle.width;
					float offsetForThumbFullWidth = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
					float totalWidth = this.dragContainer.layout.width - dragElementWidth + offsetForThumbFullWidth;
					float newLeft = directionalNormalizedPosition * totalWidth;
					bool flag3 = float.IsNaN(newLeft);
					if (flag3)
					{
						return;
					}
					float currentLeft = this.dragElement.transform.position.x;
					bool flag4 = !this.SameValues(currentLeft, newLeft, halfPixel);
					if (flag4)
					{
						Vector3 newPos = new Vector3(newLeft, 0f, 0f);
						this.dragElement.transform.position = newPos;
						this.dragBorderElement.transform.position = newPos;
					}
				}
				else
				{
					float dragElementHeight = this.dragElement.resolvedStyle.height;
					float totalHeight = this.dragContainer.resolvedStyle.height - dragElementHeight;
					float newTop = (1f - directionalNormalizedPosition) * totalHeight;
					bool flag5 = float.IsNaN(newTop);
					if (flag5)
					{
						return;
					}
					float currentTop = this.dragElement.transform.position.y;
					bool flag6 = !this.SameValues(currentTop, newTop, halfPixel);
					if (flag6)
					{
						Vector3 newPos2 = new Vector3(0f, newTop, 0f);
						this.dragElement.transform.position = newPos2;
						this.dragBorderElement.transform.position = newPos2;
					}
				}
				this.UpdateFill(normalizedPosition);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00017414 File Offset: 0x00015614
		private void UpdateFill(float normalizedValue)
		{
			bool flag = !this.fill;
			if (!flag)
			{
				bool flag2 = this.fillElement == null;
				if (flag2)
				{
					this.fillElement = new VisualElement
					{
						name = "unity-fill",
						usageHints = UsageHints.DynamicColor
					};
					this.fillElement.AddToClassList(BaseSlider<TValueType>.fillUssClassName);
					this.trackElement.Add(this.fillElement);
				}
				float inverseNormalizedValue = 1f - normalizedValue;
				Length valuePercent = Length.Percent(inverseNormalizedValue * 100f);
				bool flag3 = this.direction == SliderDirection.Vertical;
				if (flag3)
				{
					this.fillElement.style.right = 0f;
					this.fillElement.style.left = 0f;
					this.fillElement.style.bottom = (this.inverted ? valuePercent : 0f);
					this.fillElement.style.top = (this.inverted ? 0f : valuePercent);
				}
				else
				{
					this.fillElement.style.top = 0f;
					this.fillElement.style.bottom = 0f;
					this.fillElement.style.left = (this.inverted ? valuePercent : 0f);
					this.fillElement.style.right = (this.inverted ? 0f : valuePercent);
				}
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000175D0 File Offset: 0x000157D0
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

		// Token: 0x060004A7 RID: 1191 RVA: 0x000020EA File Offset: 0x000002EA
		[Obsolete("ExecuteDefaultAction override has been removed because default event handling was migrated to HandleEventBubbleUp. Please use HandleEventBubbleUp.", false)]
		[EventInterest(EventInterestOptions.Inherit)]
		protected override void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017614 File Offset: 0x00015814
		private void UpdateTextFieldVisibility()
		{
			bool showInputField = this.showInputField;
			if (showInputField)
			{
				bool flag = this.inputTextField == null;
				if (flag)
				{
					this.inputTextField = new TextField
					{
						name = "unity-text-field"
					};
					this.inputTextField.AddToClassList(BaseSlider<TValueType>.textFieldClassName);
					this.inputTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnTextFieldFocusIn), TrickleDown.NoTrickleDown);
					this.inputTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					base.visualInput.Add(this.inputTextField);
					this.UpdateTextFieldValue();
				}
			}
			else
			{
				bool flag2 = this.inputTextField != null && this.inputTextField.panel != null;
				if (flag2)
				{
					bool flag3 = this.inputTextField.panel != null;
					if (flag3)
					{
						this.inputTextField.RemoveFromHierarchy();
					}
					this.inputTextField.UnregisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.UnregisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnTextFieldFocusIn), TrickleDown.NoTrickleDown);
					this.inputTextField.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					this.inputTextField = null;
				}
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00017764 File Offset: 0x00015964
		private void UpdateTextFieldValue()
		{
			bool flag = this.inputTextField == null || this.m_IsEditingTextField;
			if (!flag)
			{
				this.inputTextField.SetValueWithoutNotify(string.Format(CultureInfo.InvariantCulture, "{0:g7}", this.value));
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000177AF File Offset: 0x000159AF
		private void OnFocusIn(FocusInEvent evt)
		{
			this.dragElement.AddToClassList(BaseSlider<TValueType>.movableUssClassName);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000177C2 File Offset: 0x000159C2
		private void OnFocusOut(FocusOutEvent evt)
		{
			this.dragElement.RemoveFromClassList(BaseSlider<TValueType>.movableUssClassName);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000177D5 File Offset: 0x000159D5
		private void OnTextFieldFocusIn(FocusInEvent evt)
		{
			this.m_IsEditingTextField = true;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000177DF File Offset: 0x000159DF
		private void OnTextFieldFocusOut(FocusOutEvent evt)
		{
			this.m_IsEditingTextField = false;
			this.UpdateTextFieldValue();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000177F0 File Offset: 0x000159F0
		private void OnTextFieldValueChange(ChangeEvent<string> evt)
		{
			TValueType newValue = this.GetClampedValue(this.ParseStringToValue(evt.previousValue, evt.newValue));
			bool flag = !EqualityComparer<TValueType>.Default.Equals(newValue, this.value);
			if (flag)
			{
				this.value = newValue;
				evt.StopPropagation();
				bool flag2 = base.elementPanel != null;
				if (flag2)
				{
					this.OnViewDataReady();
				}
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00017854 File Offset: 0x00015A54
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				VisualElement dragElement = this.dragElement;
				if (dragElement != null)
				{
					dragElement.RemoveFromHierarchy();
				}
			}
			else
			{
				this.dragContainer.Add(this.dragElement);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00017898 File Offset: 0x00015A98
		internal override void RegisterEditingCallbacks()
		{
			base.labelElement.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000178F4 File Offset: 0x00015AF4
		internal override void UnregisterEditingCallbacks()
		{
			base.labelElement.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(base.StartEditing), TrickleDown.TrickleDown);
			this.dragContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(base.EndEditing), TrickleDown.NoTrickleDown);
		}

		// Token: 0x04000288 RID: 648
		internal static readonly BindingId lowValueProperty = "lowValue";

		// Token: 0x04000289 RID: 649
		internal static readonly BindingId highValueProperty = "highValue";

		// Token: 0x0400028A RID: 650
		internal static readonly BindingId rangeProperty = "range";

		// Token: 0x0400028B RID: 651
		internal static readonly BindingId pageSizeProperty = "pageSize";

		// Token: 0x0400028C RID: 652
		internal static readonly BindingId showInputFieldProperty = "showInputField";

		// Token: 0x0400028D RID: 653
		internal static readonly BindingId directionProperty = "direction";

		// Token: 0x0400028E RID: 654
		internal static readonly BindingId invertedProperty = "inverted";

		// Token: 0x0400028F RID: 655
		internal static readonly BindingId fillProperty = "fill";

		// Token: 0x04000296 RID: 662
		private bool m_IsEditingTextField;

		// Token: 0x04000297 RID: 663
		private bool m_Fill;

		// Token: 0x04000298 RID: 664
		[SerializeField]
		[DontCreateProperty]
		private TValueType m_LowValue;

		// Token: 0x04000299 RID: 665
		[DontCreateProperty]
		[SerializeField]
		private TValueType m_HighValue;

		// Token: 0x0400029A RID: 666
		private float m_PageSize;

		// Token: 0x0400029B RID: 667
		private bool m_ShowInputField = false;

		// Token: 0x0400029E RID: 670
		private Rect m_DragElementStartPos;

		// Token: 0x0400029F RID: 671
		private SliderDirection m_Direction;

		// Token: 0x040002A0 RID: 672
		private bool m_Inverted = false;

		// Token: 0x040002A1 RID: 673
		public new static readonly string ussClassName = "unity-base-slider";

		// Token: 0x040002A2 RID: 674
		public new static readonly string labelUssClassName = BaseSlider<TValueType>.ussClassName + "__label";

		// Token: 0x040002A3 RID: 675
		public new static readonly string inputUssClassName = BaseSlider<TValueType>.ussClassName + "__input";

		// Token: 0x040002A4 RID: 676
		public static readonly string horizontalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--horizontal";

		// Token: 0x040002A5 RID: 677
		public static readonly string verticalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--vertical";

		// Token: 0x040002A6 RID: 678
		public static readonly string dragContainerUssClassName = BaseSlider<TValueType>.ussClassName + "__drag-container";

		// Token: 0x040002A7 RID: 679
		public static readonly string trackerUssClassName = BaseSlider<TValueType>.ussClassName + "__tracker";

		// Token: 0x040002A8 RID: 680
		public static readonly string draggerUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger";

		// Token: 0x040002A9 RID: 681
		public static readonly string draggerBorderUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger-border";

		// Token: 0x040002AA RID: 682
		public static readonly string textFieldClassName = BaseSlider<TValueType>.ussClassName + "__text-field";

		// Token: 0x040002AB RID: 683
		public static readonly string fillUssClassName = BaseSlider<TValueType>.ussClassName + "__fill";

		// Token: 0x040002AC RID: 684
		public static readonly string movableUssClassName = BaseSlider<TValueType>.ussClassName + "--movable";

		// Token: 0x0200007B RID: 123
		[Obsolete("UxmlTraits<TValueUxmlAttributeType> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public class UxmlTraits<TValueUxmlAttributeType> : BaseFieldTraits<TValueType, TValueUxmlAttributeType> where TValueUxmlAttributeType : TypedUxmlAttributeDescription<TValueType>, new()
		{
			// Token: 0x060004B3 RID: 1203 RVA: 0x00017ABB File Offset: 0x00015CBB
			public UxmlTraits()
			{
				this.m_PickingMode.defaultValue = PickingMode.Ignore;
			}
		}

		// Token: 0x0200007C RID: 124
		internal enum SliderKey
		{
			// Token: 0x040002AE RID: 686
			None,
			// Token: 0x040002AF RID: 687
			Lowest,
			// Token: 0x040002B0 RID: 688
			LowerPage,
			// Token: 0x040002B1 RID: 689
			Lower,
			// Token: 0x040002B2 RID: 690
			Higher,
			// Token: 0x040002B3 RID: 691
			HigherPage,
			// Token: 0x040002B4 RID: 692
			Highest
		}
	}
}
