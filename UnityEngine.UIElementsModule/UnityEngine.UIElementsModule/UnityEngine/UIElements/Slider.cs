using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000141 RID: 321
	public class Slider : BaseSlider<float>
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x0002F9B4 File Offset: 0x0002DBB4
		public Slider()
			: this(null, 0f, 10f, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002F9CF File Offset: 0x0002DBCF
		public Slider(float start, float end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002F9DF File Offset: 0x0002DBDF
		public Slider(string label, float start = 0f, float end = 10f, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(Slider.ussClassName);
			base.labelElement.AddToClassList(Slider.labelUssClassName);
			base.visualInput.AddToClassList(Slider.inputUssClassName);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002FA20 File Offset: 0x0002DC20
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, float startValue)
		{
			double sensitivity = NumericFieldDraggerUtility.CalculateFloatDragSensitivity((double)startValue, (double)base.lowValue, (double)base.highValue);
			float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
			double v = (double)this.value;
			v += (double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity;
			this.value = (float)v;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002FA78 File Offset: 0x0002DC78
		internal override float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			float newValue = Mathf.LerpUnclamped(a, b, interpolant);
			float minDifference = Mathf.Abs((base.highValue - base.lowValue) / (base.dragContainer.resolvedStyle.width - base.dragElement.resolvedStyle.width));
			int numOfDecimalsForMinDifference = ((minDifference == 0f) ? Mathf.Clamp((int)(5.0 - (double)Mathf.Log10(Mathf.Abs(minDifference))), 0, 15) : Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(minDifference))), 0, 15));
			return (float)Math.Round((double)newValue, numOfDecimalsForMinDifference, MidpointRounding.AwayFromZero);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002FB1C File Offset: 0x0002DD1C
		internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			float range = higherValue - lowerValue;
			bool flag = Mathf.Approximately(range, 0f);
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				num = (currentValue - lowerValue) / range;
			}
			return num;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002FB50 File Offset: 0x0002DD50
		internal override float SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002FB74 File Offset: 0x0002DD74
		internal override float ParseStringToValue(string previousValue, string newValue)
		{
			float value;
			bool flag = UINumericFieldsUtils.TryConvertStringToFloat(newValue, previousValue, out value);
			float num;
			if (flag)
			{
				num = value;
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002FB9C File Offset: 0x0002DD9C
		internal override void ComputeValueFromKey(BaseSlider<float>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<float>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<float>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<float>.SliderKey.Highest)
					{
						bool isPageSize = sliderKey == BaseSlider<float>.SliderKey.LowerPage || sliderKey == BaseSlider<float>.SliderKey.HigherPage;
						float delta = BaseSlider<float>.GetClosestPowerOfTen(Mathf.Abs((base.highValue - base.lowValue) * 0.01f));
						bool flag = isPageSize;
						if (flag)
						{
							delta *= this.pageSize;
						}
						else if (isShift)
						{
							delta *= 10f;
						}
						bool flag2 = sliderKey == BaseSlider<float>.SliderKey.Lower || sliderKey == BaseSlider<float>.SliderKey.LowerPage;
						if (flag2)
						{
							delta = -delta;
						}
						this.value = BaseSlider<float>.RoundToMultipleOf(this.value + delta * 0.5001f, Mathf.Abs(delta));
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

		// Token: 0x0400064D RID: 1613
		public new static readonly string ussClassName = "unity-slider";

		// Token: 0x0400064E RID: 1614
		public new static readonly string labelUssClassName = Slider.ussClassName + "__label";

		// Token: 0x0400064F RID: 1615
		public new static readonly string inputUssClassName = Slider.ussClassName + "__input";

		// Token: 0x02000142 RID: 322
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Slider, Slider.UxmlTraits>
		{
		}

		// Token: 0x02000143 RID: 323
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseSlider<float>.UxmlTraits<UxmlFloatAttributeDescription>
		{
			// Token: 0x060009C9 RID: 2505 RVA: 0x0002FCA4 File Offset: 0x0002DEA4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				Slider f = (Slider)ve;
				f.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				f.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				f.direction = this.m_Direction.GetValueFromBag(bag, cc);
				f.pageSize = this.m_PageSize.GetValueFromBag(bag, cc);
				f.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				f.inverted = this.m_Inverted.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x04000650 RID: 1616
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x04000651 RID: 1617
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 10f
			};

			// Token: 0x04000652 RID: 1618
			private UxmlFloatAttributeDescription m_PageSize = new UxmlFloatAttributeDescription
			{
				name = "page-size",
				defaultValue = 0f
			};

			// Token: 0x04000653 RID: 1619
			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			// Token: 0x04000654 RID: 1620
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			// Token: 0x04000655 RID: 1621
			private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
			{
				name = "inverted",
				defaultValue = false
			};
		}
	}
}
