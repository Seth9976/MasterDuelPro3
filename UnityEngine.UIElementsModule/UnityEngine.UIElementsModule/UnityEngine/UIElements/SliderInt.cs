using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000144 RID: 324
	public class SliderInt : BaseSlider<int>
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x0002FE0A File Offset: 0x0002E00A
		public SliderInt()
			: this(null, 0, 10, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002FE1E File Offset: 0x0002E01E
		public SliderInt(string label, int start = 0, int end = 10, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(SliderInt.ussClassName);
			base.labelElement.AddToClassList(SliderInt.labelUssClassName);
			base.visualInput.AddToClassList(SliderInt.inputUssClassName);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002FE60 File Offset: 0x0002E060
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0002FE78 File Offset: 0x0002E078
		public override float pageSize
		{
			get
			{
				return base.pageSize;
			}
			set
			{
				base.pageSize = (float)Mathf.RoundToInt(value);
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002FE8C File Offset: 0x0002E08C
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, int startValue)
		{
			double sensitivity = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity((long)startValue, (long)base.lowValue, (long)base.highValue);
			float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
			long v = (long)this.value;
			v += (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity);
			this.value = (int)v;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002FEE8 File Offset: 0x0002E0E8
		internal override int SliderLerpUnclamped(int a, int b, float interpolant)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)a, (float)b, interpolant));
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002FF0C File Offset: 0x0002E10C
		internal override float SliderNormalizeValue(int currentValue, int lowerValue, int higherValue)
		{
			bool flag = higherValue - lowerValue == 0;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = ((float)currentValue - (float)lowerValue) / ((float)higherValue - (float)lowerValue);
			}
			return num;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002FF3C File Offset: 0x0002E13C
		internal override int SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002FF60 File Offset: 0x0002E160
		internal override int ParseStringToValue(string previousValue, string newValue)
		{
			int value;
			bool flag = UINumericFieldsUtils.TryConvertStringToInt(newValue, previousValue, out value);
			int num;
			if (flag)
			{
				num = value;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002FF84 File Offset: 0x0002E184
		internal override void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			bool flag = Mathf.Approximately(this.pageSize, 0f);
			if (flag)
			{
				base.ComputeValueAndDirectionFromClick(sliderLength, dragElementLength, dragElementPos, dragElementLastPos);
			}
			else
			{
				float totalRange = sliderLength - dragElementLength;
				bool flag2 = Mathf.Abs(totalRange) < 1E-30f;
				if (!flag2)
				{
					int adjustedPageDirection = (int)this.pageSize;
					bool flag3 = (base.lowValue > base.highValue && !base.inverted) || (base.lowValue < base.highValue && base.inverted) || (base.direction == SliderDirection.Vertical && !base.inverted);
					if (flag3)
					{
						adjustedPageDirection = -adjustedPageDirection;
					}
					bool isPositionDecreasing = dragElementLastPos < dragElementPos;
					bool isPositionIncreasing = dragElementLastPos > dragElementPos + dragElementLength;
					bool isDraggingHighToLow = (base.inverted ? isPositionIncreasing : isPositionDecreasing);
					bool isDraggingLowToHigh = (base.inverted ? isPositionDecreasing : isPositionIncreasing);
					bool flag4 = isDraggingHighToLow && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.LowToHigh;
					if (flag4)
					{
						base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.HighToLow;
						this.value -= adjustedPageDirection;
					}
					else
					{
						bool flag5 = isDraggingLowToHigh && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.HighToLow;
						if (flag5)
						{
							base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.LowToHigh;
							this.value += adjustedPageDirection;
						}
					}
				}
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000300D8 File Offset: 0x0002E2D8
		internal override void ComputeValueFromKey(BaseSlider<int>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<int>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<int>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<int>.SliderKey.Highest)
					{
						bool isPageSize = sliderKey == BaseSlider<int>.SliderKey.LowerPage || sliderKey == BaseSlider<int>.SliderKey.HigherPage;
						float delta = BaseSlider<int>.GetClosestPowerOfTen(Mathf.Abs((float)(base.highValue - base.lowValue) * 0.01f));
						bool flag = delta < 1f;
						if (flag)
						{
							delta = 1f;
						}
						bool flag2 = isPageSize;
						if (flag2)
						{
							delta *= this.pageSize;
						}
						else if (isShift)
						{
							delta *= 10f;
						}
						bool flag3 = sliderKey == BaseSlider<int>.SliderKey.Lower || sliderKey == BaseSlider<int>.SliderKey.LowerPage;
						if (flag3)
						{
							delta = -delta;
						}
						this.value = Mathf.RoundToInt(BaseSlider<int>.RoundToMultipleOf((float)this.value + delta * 0.5001f, Mathf.Abs(delta)));
					}
					else
					{
						this.value = base.highValue;
					}
				}
				else
				{
					this.value = base.lowValue;
				}
			}
		}

		// Token: 0x04000656 RID: 1622
		public new static readonly string ussClassName = "unity-slider-int";

		// Token: 0x04000657 RID: 1623
		public new static readonly string labelUssClassName = SliderInt.ussClassName + "__label";

		// Token: 0x04000658 RID: 1624
		public new static readonly string inputUssClassName = SliderInt.ussClassName + "__input";

		// Token: 0x02000145 RID: 325
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<SliderInt, SliderInt.UxmlTraits>
		{
		}

		// Token: 0x02000146 RID: 326
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseSlider<int>.UxmlTraits<UxmlIntAttributeDescription>
		{
			// Token: 0x060009D8 RID: 2520 RVA: 0x000301FC File Offset: 0x0002E3FC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				SliderInt f = (SliderInt)ve;
				f.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				f.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				f.direction = this.m_Direction.GetValueFromBag(bag, cc);
				f.pageSize = (float)this.m_PageSize.GetValueFromBag(bag, cc);
				f.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				f.inverted = this.m_Inverted.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x04000659 RID: 1625
			private UxmlIntAttributeDescription m_LowValue = new UxmlIntAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x0400065A RID: 1626
			private UxmlIntAttributeDescription m_HighValue = new UxmlIntAttributeDescription
			{
				name = "high-value",
				defaultValue = 10
			};

			// Token: 0x0400065B RID: 1627
			private UxmlIntAttributeDescription m_PageSize = new UxmlIntAttributeDescription
			{
				name = "page-size",
				defaultValue = 0
			};

			// Token: 0x0400065C RID: 1628
			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			// Token: 0x0400065D RID: 1629
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			// Token: 0x0400065E RID: 1630
			private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
			{
				name = "inverted",
				defaultValue = false
			};
		}
	}
}
