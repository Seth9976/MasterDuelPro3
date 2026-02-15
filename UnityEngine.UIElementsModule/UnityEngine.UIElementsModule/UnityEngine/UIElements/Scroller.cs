using System;
using System.Diagnostics;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000135 RID: 309
	public class Scroller : VisualElement
	{
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600094F RID: 2383 RVA: 0x0002C314 File Offset: 0x0002A514
		// (remove) Token: 0x06000950 RID: 2384 RVA: 0x0002C34C File Offset: 0x0002A54C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<float> valueChanged;

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0002C381 File Offset: 0x0002A581
		public Slider slider { get; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0002C389 File Offset: 0x0002A589
		public RepeatButton lowButton { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0002C391 File Offset: 0x0002A591
		public RepeatButton highButton { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0002C39C File Offset: 0x0002A59C
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0002C3BC File Offset: 0x0002A5BC
		[CreateProperty]
		public float value
		{
			get
			{
				return this.slider.value;
			}
			set
			{
				float previous = this.slider.value;
				this.slider.value = value;
				bool flag = !Mathf.Approximately(previous, this.slider.value);
				if (flag)
				{
					base.NotifyPropertyChanged(in Scroller.valueProperty);
				}
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0002C408 File Offset: 0x0002A608
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0002C428 File Offset: 0x0002A628
		[CreateProperty]
		public float lowValue
		{
			get
			{
				return this.slider.lowValue;
			}
			set
			{
				float previous = this.slider.lowValue;
				this.slider.lowValue = value;
				bool flag = !Mathf.Approximately(previous, this.slider.lowValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in Scroller.lowValueProperty);
				}
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0002C474 File Offset: 0x0002A674
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0002C494 File Offset: 0x0002A694
		[CreateProperty]
		public float highValue
		{
			get
			{
				return this.slider.highValue;
			}
			set
			{
				float previous = this.slider.highValue;
				this.slider.highValue = value;
				bool flag = !Mathf.Approximately(previous, this.slider.highValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in Scroller.highValueProperty);
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002C4E0 File Offset: 0x0002A6E0
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0002C504 File Offset: 0x0002A704
		[CreateProperty]
		public SliderDirection direction
		{
			get
			{
				return (base.resolvedStyle.flexDirection == FlexDirection.Row) ? SliderDirection.Horizontal : SliderDirection.Vertical;
			}
			set
			{
				SliderDirection previous = this.slider.direction;
				this.slider.direction = value;
				this.slider.inverted = value == SliderDirection.Vertical;
				bool flag = value == SliderDirection.Horizontal;
				if (flag)
				{
					base.style.flexDirection = FlexDirection.Row;
					base.AddToClassList(Scroller.horizontalVariantUssClassName);
					base.RemoveFromClassList(Scroller.verticalVariantUssClassName);
				}
				else
				{
					base.style.flexDirection = FlexDirection.Column;
					base.AddToClassList(Scroller.verticalVariantUssClassName);
					base.RemoveFromClassList(Scroller.horizontalVariantUssClassName);
				}
				bool flag2 = previous != this.slider.direction;
				if (flag2)
				{
					base.NotifyPropertyChanged(in Scroller.directionProperty);
				}
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002C5BE File Offset: 0x0002A7BE
		public Scroller()
			: this(0f, 0f, null, SliderDirection.Vertical)
		{
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002C5D4 File Offset: 0x0002A7D4
		public Scroller(float lowValue, float highValue, Action<float> valueChanged, SliderDirection direction = SliderDirection.Vertical)
		{
			base.AddToClassList(Scroller.ussClassName);
			this.slider = new Scroller.ScrollerSlider(lowValue, highValue, direction, 20f)
			{
				name = "unity-slider",
				viewDataKey = "Slider"
			};
			this.slider.AddToClassList(Scroller.sliderUssClassName);
			this.slider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnSliderValueChange));
			this.lowButton = new RepeatButton(new Action(this.ScrollPageUp), 250L, 30L)
			{
				name = "unity-low-button"
			};
			this.lowButton.AddToClassList(Scroller.lowButtonUssClassName);
			base.Add(this.lowButton);
			this.highButton = new RepeatButton(new Action(this.ScrollPageDown), 250L, 30L)
			{
				name = "unity-high-button"
			};
			this.highButton.AddToClassList(Scroller.highButtonUssClassName);
			base.Add(this.highButton);
			base.Add(this.slider);
			this.direction = direction;
			this.valueChanged = valueChanged;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002C6FB File Offset: 0x0002A8FB
		public void Adjust(float factor)
		{
			base.SetEnabled(factor < 1f);
			this.slider.AdjustDragElement(factor);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002C71A File Offset: 0x0002A91A
		private void OnSliderValueChange(ChangeEvent<float> evt)
		{
			this.value = evt.newValue;
			Action<float> action = this.valueChanged;
			if (action != null)
			{
				action(this.slider.value);
			}
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002C753 File Offset: 0x0002A953
		public void ScrollPageUp()
		{
			this.ScrollPageUp(1f);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002C762 File Offset: 0x0002A962
		public void ScrollPageDown()
		{
			this.ScrollPageDown(1f);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002C774 File Offset: 0x0002A974
		public void ScrollPageUp(float factor)
		{
			this.value -= factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		public void ScrollPageDown(float factor)
		{
			this.value += factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x040005D4 RID: 1492
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x040005D5 RID: 1493
		internal static readonly BindingId lowValueProperty = "lowValue";

		// Token: 0x040005D6 RID: 1494
		internal static readonly BindingId highValueProperty = "highValue";

		// Token: 0x040005D7 RID: 1495
		internal static readonly BindingId directionProperty = "direction";

		// Token: 0x040005DC RID: 1500
		public static readonly string ussClassName = "unity-scroller";

		// Token: 0x040005DD RID: 1501
		public static readonly string horizontalVariantUssClassName = Scroller.ussClassName + "--horizontal";

		// Token: 0x040005DE RID: 1502
		public static readonly string verticalVariantUssClassName = Scroller.ussClassName + "--vertical";

		// Token: 0x040005DF RID: 1503
		public static readonly string sliderUssClassName = Scroller.ussClassName + "__slider";

		// Token: 0x040005E0 RID: 1504
		public static readonly string lowButtonUssClassName = Scroller.ussClassName + "__low-button";

		// Token: 0x040005E1 RID: 1505
		public static readonly string highButtonUssClassName = Scroller.ussClassName + "__high-button";

		// Token: 0x02000136 RID: 310
		private class ScrollerSlider : Slider
		{
			// Token: 0x06000965 RID: 2405 RVA: 0x0002C8CB File Offset: 0x0002AACB
			public ScrollerSlider(float start, float end, SliderDirection direction, float pageSize)
				: base(start, end, direction, pageSize)
			{
			}

			// Token: 0x06000966 RID: 2406 RVA: 0x0002C8DC File Offset: 0x0002AADC
			internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
			{
				return Mathf.Clamp(base.SliderNormalizeValue(currentValue, lowerValue, higherValue), 0f, 1f);
			}
		}

		// Token: 0x02000137 RID: 311
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Scroller, Scroller.UxmlTraits>
		{
		}

		// Token: 0x02000138 RID: 312
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000968 RID: 2408 RVA: 0x0002C910 File Offset: 0x0002AB10
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Scroller scroller = (Scroller)ve;
				scroller.slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				scroller.slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				scroller.direction = this.m_Direction.GetValueFromBag(bag, cc);
				scroller.value = this.m_Value.GetValueFromBag(bag, cc);
			}

			// Token: 0x040005E2 RID: 1506
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				obsoleteNames = new string[] { "lowValue" }
			};

			// Token: 0x040005E3 RID: 1507
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				obsoleteNames = new string[] { "highValue" }
			};

			// Token: 0x040005E4 RID: 1508
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Vertical
			};

			// Token: 0x040005E5 RID: 1509
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value"
			};
		}
	}
}
