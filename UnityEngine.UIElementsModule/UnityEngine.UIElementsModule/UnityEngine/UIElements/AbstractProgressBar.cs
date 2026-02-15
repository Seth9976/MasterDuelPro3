using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000127 RID: 295
	public abstract class AbstractProgressBar : BindableElement, INotifyValueChanged<float>
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0002B2D9 File Offset: 0x000294D9
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0002B2E8 File Offset: 0x000294E8
		[CreateProperty]
		public string title
		{
			get
			{
				return this.m_Title.text;
			}
			set
			{
				string previous = this.title;
				this.m_Title.text = value;
				bool flag = string.CompareOrdinal(previous, this.title) != 0;
				if (flag)
				{
					base.NotifyPropertyChanged(in AbstractProgressBar.titleProperty);
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0002B329 File Offset: 0x00029529
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0002B334 File Offset: 0x00029534
		[CreateProperty]
		public float lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				float previous = this.lowValue;
				this.m_LowValue = value;
				this.SetProgress(this.m_Value);
				bool flag = !Mathf.Approximately(previous, this.lowValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in AbstractProgressBar.lowValueProperty);
				}
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0002B37C File Offset: 0x0002957C
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0002B384 File Offset: 0x00029584
		[CreateProperty]
		public float highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				float previous = this.highValue;
				this.m_HighValue = value;
				this.SetProgress(this.m_Value);
				bool flag = !Mathf.Approximately(previous, this.highValue);
				if (flag)
				{
					base.NotifyPropertyChanged(in AbstractProgressBar.highValueProperty);
				}
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002B3CC File Offset: 0x000295CC
		public AbstractProgressBar()
		{
			base.AddToClassList(AbstractProgressBar.ussClassName);
			VisualElement container = new VisualElement
			{
				name = AbstractProgressBar.ussClassName
			};
			this.m_Background = new VisualElement();
			this.m_Background.AddToClassList(AbstractProgressBar.backgroundUssClassName);
			container.Add(this.m_Background);
			this.m_Progress = new VisualElement();
			this.m_Progress.AddToClassList(AbstractProgressBar.progressUssClassName);
			this.m_Background.Add(this.m_Progress);
			VisualElement titleContainer = new VisualElement();
			titleContainer.AddToClassList(AbstractProgressBar.titleContainerUssClassName);
			this.m_Background.Add(titleContainer);
			this.m_Title = new Label();
			this.m_Title.AddToClassList(AbstractProgressBar.titleUssClassName);
			titleContainer.Add(this.m_Title);
			container.AddToClassList(AbstractProgressBar.containerUssClassName);
			base.hierarchy.Add(container);
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002B4D9 File Offset: 0x000296D9
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			this.SetProgress(this.value);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002B4EC File Offset: 0x000296EC
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0002B504 File Offset: 0x00029704
		[CreateProperty]
		public virtual float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !EqualityComparer<float>.Default.Equals(this.m_Value, value);
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<float> evt = ChangeEvent<float>.GetPooled(this.m_Value, value))
						{
							evt.elementTarget = this;
							this.SetValueWithoutNotify(value);
							this.SendEvent(evt);
							base.NotifyPropertyChanged(in AbstractProgressBar.valueProperty);
						}
					}
					else
					{
						this.SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002B598 File Offset: 0x00029798
		public void SetValueWithoutNotify(float newValue)
		{
			this.m_Value = newValue;
			this.SetProgress(this.value);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002B5B0 File Offset: 0x000297B0
		private void SetProgress(float p)
		{
			bool flag = p < this.lowValue;
			float right;
			if (flag)
			{
				right = this.lowValue;
			}
			else
			{
				bool flag2 = p > this.highValue;
				if (flag2)
				{
					right = this.highValue;
				}
				else
				{
					right = p;
				}
			}
			right = this.CalculateProgressWidth(right);
			bool flag3 = right >= 0f;
			if (flag3)
			{
				this.m_Progress.style.right = right;
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002B624 File Offset: 0x00029824
		private float CalculateProgressWidth(float width)
		{
			bool flag = this.m_Background == null || this.m_Progress == null;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = float.IsNaN(this.m_Background.layout.width);
				if (flag2)
				{
					num = 0f;
				}
				else
				{
					float maxWidth = this.m_Background.layout.width - 2f;
					num = maxWidth - Mathf.Max(maxWidth * width / this.highValue, 1f);
				}
			}
			return num;
		}

		// Token: 0x040005A7 RID: 1447
		internal static readonly BindingId titleProperty = "title";

		// Token: 0x040005A8 RID: 1448
		internal static readonly BindingId lowValueProperty = "lowValue";

		// Token: 0x040005A9 RID: 1449
		internal static readonly BindingId highValueProperty = "highValue";

		// Token: 0x040005AA RID: 1450
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x040005AB RID: 1451
		public static readonly string ussClassName = "unity-progress-bar";

		// Token: 0x040005AC RID: 1452
		public static readonly string containerUssClassName = AbstractProgressBar.ussClassName + "__container";

		// Token: 0x040005AD RID: 1453
		public static readonly string titleUssClassName = AbstractProgressBar.ussClassName + "__title";

		// Token: 0x040005AE RID: 1454
		public static readonly string titleContainerUssClassName = AbstractProgressBar.ussClassName + "__title-container";

		// Token: 0x040005AF RID: 1455
		public static readonly string progressUssClassName = AbstractProgressBar.ussClassName + "__progress";

		// Token: 0x040005B0 RID: 1456
		public static readonly string backgroundUssClassName = AbstractProgressBar.ussClassName + "__background";

		// Token: 0x040005B1 RID: 1457
		private readonly VisualElement m_Background;

		// Token: 0x040005B2 RID: 1458
		private readonly VisualElement m_Progress;

		// Token: 0x040005B3 RID: 1459
		private readonly Label m_Title;

		// Token: 0x040005B4 RID: 1460
		private float m_LowValue;

		// Token: 0x040005B5 RID: 1461
		private float m_HighValue = 100f;

		// Token: 0x040005B6 RID: 1462
		private float m_Value;

		// Token: 0x02000128 RID: 296
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x0600091C RID: 2332 RVA: 0x0002B768 File Offset: 0x00029968
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				AbstractProgressBar bar = ve as AbstractProgressBar;
				bar.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				bar.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				bar.value = this.m_Value.GetValueFromBag(bag, cc);
				bar.title = this.m_Title.GetValueFromBag(bag, cc);
			}

			// Token: 0x040005B7 RID: 1463
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				defaultValue = 0f
			};

			// Token: 0x040005B8 RID: 1464
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 100f
			};

			// Token: 0x040005B9 RID: 1465
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value",
				defaultValue = 0f
			};

			// Token: 0x040005BA RID: 1466
			private UxmlStringAttributeDescription m_Title = new UxmlStringAttributeDescription
			{
				name = "title",
				defaultValue = string.Empty
			};
		}
	}
}
