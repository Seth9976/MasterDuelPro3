using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005DB RID: 1499
	public struct StyleValues
	{
		// Token: 0x17000A73 RID: 2675
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x000A702A File Offset: 0x000A522A
		public float top
		{
			set
			{
				this.SetValue(StylePropertyId.Top, value);
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x000A703A File Offset: 0x000A523A
		public float left
		{
			set
			{
				this.SetValue(StylePropertyId.Left, value);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x000A704A File Offset: 0x000A524A
		public float width
		{
			set
			{
				this.SetValue(StylePropertyId.Width, value);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (set) Token: 0x06002881 RID: 10369 RVA: 0x000A705A File Offset: 0x000A525A
		public float height
		{
			set
			{
				this.SetValue(StylePropertyId.Height, value);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (set) Token: 0x06002882 RID: 10370 RVA: 0x000A706A File Offset: 0x000A526A
		public float right
		{
			set
			{
				this.SetValue(StylePropertyId.Right, value);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (set) Token: 0x06002883 RID: 10371 RVA: 0x000A707A File Offset: 0x000A527A
		public float bottom
		{
			set
			{
				this.SetValue(StylePropertyId.Bottom, value);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (set) Token: 0x06002884 RID: 10372 RVA: 0x000A708A File Offset: 0x000A528A
		public Color color
		{
			set
			{
				this.SetValue(StylePropertyId.Color, value);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (set) Token: 0x06002885 RID: 10373 RVA: 0x000A709A File Offset: 0x000A529A
		public Color backgroundColor
		{
			set
			{
				this.SetValue(StylePropertyId.BackgroundColor, value);
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (set) Token: 0x06002886 RID: 10374 RVA: 0x000A70AA File Offset: 0x000A52AA
		public Color unityBackgroundImageTintColor
		{
			set
			{
				this.SetValue(StylePropertyId.UnityBackgroundImageTintColor, value);
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (set) Token: 0x06002887 RID: 10375 RVA: 0x000A70BA File Offset: 0x000A52BA
		public Color borderColor
		{
			set
			{
				this.SetValue(StylePropertyId.BorderColor, value);
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (set) Token: 0x06002888 RID: 10376 RVA: 0x000A70CA File Offset: 0x000A52CA
		public float marginLeft
		{
			set
			{
				this.SetValue(StylePropertyId.MarginLeft, value);
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (set) Token: 0x06002889 RID: 10377 RVA: 0x000A70DA File Offset: 0x000A52DA
		public float marginTop
		{
			set
			{
				this.SetValue(StylePropertyId.MarginTop, value);
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (set) Token: 0x0600288A RID: 10378 RVA: 0x000A70EA File Offset: 0x000A52EA
		public float marginRight
		{
			set
			{
				this.SetValue(StylePropertyId.MarginRight, value);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (set) Token: 0x0600288B RID: 10379 RVA: 0x000A70FA File Offset: 0x000A52FA
		public float marginBottom
		{
			set
			{
				this.SetValue(StylePropertyId.MarginBottom, value);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x000A710A File Offset: 0x000A530A
		public float paddingLeft
		{
			set
			{
				this.SetValue(StylePropertyId.PaddingLeft, value);
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000A711C File Offset: 0x000A531C
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x000A7146 File Offset: 0x000A5346
		public float paddingTop
		{
			get
			{
				return this.Values().GetStyleFloat(StylePropertyId.PaddingTop).value;
			}
			set
			{
				this.SetValue(StylePropertyId.PaddingTop, value);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x000A7156 File Offset: 0x000A5356
		public float paddingRight
		{
			set
			{
				this.SetValue(StylePropertyId.PaddingRight, value);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (set) Token: 0x06002890 RID: 10384 RVA: 0x000A7166 File Offset: 0x000A5366
		public float paddingBottom
		{
			set
			{
				this.SetValue(StylePropertyId.PaddingBottom, value);
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x000A7176 File Offset: 0x000A5376
		public float borderLeftWidth
		{
			set
			{
				this.SetValue(StylePropertyId.BorderLeftWidth, value);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x000A7186 File Offset: 0x000A5386
		public float borderRightWidth
		{
			set
			{
				this.SetValue(StylePropertyId.BorderRightWidth, value);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (set) Token: 0x06002893 RID: 10387 RVA: 0x000A7196 File Offset: 0x000A5396
		public float borderTopWidth
		{
			set
			{
				this.SetValue(StylePropertyId.BorderTopWidth, value);
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x000A71A6 File Offset: 0x000A53A6
		public float borderBottomWidth
		{
			set
			{
				this.SetValue(StylePropertyId.BorderBottomWidth, value);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x000A71B6 File Offset: 0x000A53B6
		public float borderTopLeftRadius
		{
			set
			{
				this.SetValue(StylePropertyId.BorderTopLeftRadius, value);
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (set) Token: 0x06002896 RID: 10390 RVA: 0x000A71C6 File Offset: 0x000A53C6
		public float borderTopRightRadius
		{
			set
			{
				this.SetValue(StylePropertyId.BorderTopRightRadius, value);
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x000A71D6 File Offset: 0x000A53D6
		public float borderBottomLeftRadius
		{
			set
			{
				this.SetValue(StylePropertyId.BorderBottomLeftRadius, value);
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (set) Token: 0x06002898 RID: 10392 RVA: 0x000A71E6 File Offset: 0x000A53E6
		public float borderBottomRightRadius
		{
			set
			{
				this.SetValue(StylePropertyId.BorderBottomRightRadius, value);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x000A71F6 File Offset: 0x000A53F6
		public float opacity
		{
			set
			{
				this.SetValue(StylePropertyId.Opacity, value);
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x000A7206 File Offset: 0x000A5406
		public float flexGrow
		{
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x000A7206 File Offset: 0x000A5406
		public float flexShrink
		{
			set
			{
				this.SetValue(StylePropertyId.FlexGrow, value);
			}
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000A7218 File Offset: 0x000A5418
		internal void SetValue(StylePropertyId id, float value)
		{
			StyleValue sv = default(StyleValue);
			sv.id = id;
			sv.number = value;
			this.Values().SetStyleValue(sv);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000A724C File Offset: 0x000A544C
		internal void SetValue(StylePropertyId id, Color value)
		{
			StyleValue sv = default(StyleValue);
			sv.id = id;
			sv.color = value;
			this.Values().SetStyleValue(sv);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000A7280 File Offset: 0x000A5480
		internal StyleValueCollection Values()
		{
			bool flag = this.m_StyleValues == null;
			if (flag)
			{
				this.m_StyleValues = new StyleValueCollection();
			}
			return this.m_StyleValues;
		}

		// Token: 0x04001576 RID: 5494
		internal StyleValueCollection m_StyleValues;
	}
}
