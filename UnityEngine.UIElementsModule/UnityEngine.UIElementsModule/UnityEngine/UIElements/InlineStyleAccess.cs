using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.Layout;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D3 RID: 723
	internal class InlineStyleAccess : StyleValueCollection, IStyle
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0005A6F4 File Offset: 0x000588F4
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0005A728 File Offset: 0x00058928
		StyleEnum<Align> IStyle.alignContent
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.AlignContent);
				return new StyleEnum<Align>((Align)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignContent, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.AlignContent = (LayoutAlign)this.ve.computedStyle.alignContent;
				}
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0005A778 File Offset: 0x00058978
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x0005A7AC File Offset: 0x000589AC
		StyleEnum<Align> IStyle.alignItems
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.AlignItems);
				return new StyleEnum<Align>((Align)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignItems, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.AlignItems = (LayoutAlign)this.ve.computedStyle.alignItems;
				}
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0005A7FC File Offset: 0x000589FC
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x0005A830 File Offset: 0x00058A30
		StyleEnum<Align> IStyle.alignSelf
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.AlignSelf);
				return new StyleEnum<Align>((Align)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignSelf, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.AlignSelf = (LayoutAlign)this.ve.computedStyle.alignSelf;
				}
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0005A880 File Offset: 0x00058A80
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0005A8A0 File Offset: 0x00058AA0
		StyleColor IStyle.backgroundColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BackgroundColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0005A8D4 File Offset: 0x00058AD4
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		StyleBackground IStyle.backgroundImage
		{
			get
			{
				return base.GetStyleBackground(StylePropertyId.BackgroundImage);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundImage, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0005A928 File Offset: 0x00058B28
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x0005A948 File Offset: 0x00058B48
		StyleBackgroundPosition IStyle.backgroundPositionX
		{
			get
			{
				return base.GetStyleBackgroundPosition(StylePropertyId.BackgroundPositionX);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundPositionX, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0005A97C File Offset: 0x00058B7C
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x0005A99C File Offset: 0x00058B9C
		StyleBackgroundPosition IStyle.backgroundPositionY
		{
			get
			{
				return base.GetStyleBackgroundPosition(StylePropertyId.BackgroundPositionY);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundPositionY, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0005A9D0 File Offset: 0x00058BD0
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0005A9F0 File Offset: 0x00058BF0
		StyleBackgroundRepeat IStyle.backgroundRepeat
		{
			get
			{
				return base.GetStyleBackgroundRepeat(StylePropertyId.BackgroundRepeat);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundRepeat, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0005AA24 File Offset: 0x00058C24
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0005AA44 File Offset: 0x00058C44
		StyleColor IStyle.borderBottomColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderBottomColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0005AA78 File Offset: 0x00058C78
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x0005AA98 File Offset: 0x00058C98
		StyleLength IStyle.borderBottomLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomLeftRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0005AACC File Offset: 0x00058CCC
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x0005AAEC File Offset: 0x00058CEC
		StyleLength IStyle.borderBottomRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomRightRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0005AB20 File Offset: 0x00058D20
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x0005AB40 File Offset: 0x00058D40
		StyleFloat IStyle.borderBottomWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderBottomWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderBottomWidth = this.ve.computedStyle.borderBottomWidth;
				}
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0005AB94 File Offset: 0x00058D94
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x0005ABB4 File Offset: 0x00058DB4
		StyleColor IStyle.borderLeftColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderLeftColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x0005AC08 File Offset: 0x00058E08
		StyleFloat IStyle.borderLeftWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderLeftWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderLeftWidth = this.ve.computedStyle.borderLeftWidth;
				}
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0005AC5C File Offset: 0x00058E5C
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x0005AC7C File Offset: 0x00058E7C
		StyleColor IStyle.borderRightColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderRightColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0005ACB0 File Offset: 0x00058EB0
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0005ACD0 File Offset: 0x00058ED0
		StyleFloat IStyle.borderRightWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderRightWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderRightWidth = this.ve.computedStyle.borderRightWidth;
				}
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0005AD24 File Offset: 0x00058F24
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x0005AD44 File Offset: 0x00058F44
		StyleColor IStyle.borderTopColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderTopColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0005AD78 File Offset: 0x00058F78
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x0005AD98 File Offset: 0x00058F98
		StyleLength IStyle.borderTopLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopLeftRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0005ADCC File Offset: 0x00058FCC
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x0005ADEC File Offset: 0x00058FEC
		StyleLength IStyle.borderTopRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopRightRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0005AE20 File Offset: 0x00059020
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x0005AE40 File Offset: 0x00059040
		StyleFloat IStyle.borderTopWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderTopWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.layoutNode.BorderTopWidth = this.ve.computedStyle.borderTopWidth;
				}
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0005AE94 File Offset: 0x00059094
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x0005AEB4 File Offset: 0x000590B4
		StyleLength IStyle.bottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Bottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Bottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Bottom = this.ve.computedStyle.bottom.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0005AF08 File Offset: 0x00059108
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x0005AF28 File Offset: 0x00059128
		StyleColor IStyle.color
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.Color);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Color, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0005AF5C File Offset: 0x0005915C
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x0005AF90 File Offset: 0x00059190
		StyleEnum<DisplayStyle> IStyle.display
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.Display);
				return new StyleEnum<DisplayStyle>((DisplayStyle)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<DisplayStyle>(StylePropertyId.Display, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Display = (LayoutDisplay)this.ve.computedStyle.display;
				}
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0005AFE0 File Offset: 0x000591E0
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x0005B000 File Offset: 0x00059200
		StyleLength IStyle.flexBasis
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FlexBasis);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexBasis, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexBasis = this.ve.computedStyle.flexBasis.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0005B054 File Offset: 0x00059254
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x0005B088 File Offset: 0x00059288
		StyleEnum<FlexDirection> IStyle.flexDirection
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.FlexDirection);
				return new StyleEnum<FlexDirection>((FlexDirection)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<FlexDirection>(StylePropertyId.FlexDirection, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexDirection = (LayoutFlexDirection)this.ve.computedStyle.flexDirection;
				}
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0005B0D8 File Offset: 0x000592D8
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x0005B0F8 File Offset: 0x000592F8
		StyleFloat IStyle.flexGrow
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexGrow);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexGrow, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexGrow = this.ve.computedStyle.flexGrow;
				}
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005B148 File Offset: 0x00059348
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x0005B168 File Offset: 0x00059368
		StyleFloat IStyle.flexShrink
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexShrink);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexShrink, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.FlexShrink = this.ve.computedStyle.flexShrink;
				}
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0005B1B8 File Offset: 0x000593B8
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x0005B1EC File Offset: 0x000593EC
		StyleEnum<Wrap> IStyle.flexWrap
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.FlexWrap);
				return new StyleEnum<Wrap>((Wrap)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Wrap>(StylePropertyId.FlexWrap, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Wrap = (LayoutWrap)this.ve.computedStyle.flexWrap;
				}
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0005B23C File Offset: 0x0005943C
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x0005B25C File Offset: 0x0005945C
		StyleLength IStyle.fontSize
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FontSize);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FontSize, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0005B290 File Offset: 0x00059490
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x0005B2B0 File Offset: 0x000594B0
		StyleLength IStyle.height
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Height);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Height, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Height = this.ve.computedStyle.height.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0005B304 File Offset: 0x00059504
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0005B338 File Offset: 0x00059538
		StyleEnum<Justify> IStyle.justifyContent
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.JustifyContent);
				return new StyleEnum<Justify>((Justify)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Justify>(StylePropertyId.JustifyContent, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.JustifyContent = (LayoutJustify)this.ve.computedStyle.justifyContent;
				}
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0005B388 File Offset: 0x00059588
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x0005B3A8 File Offset: 0x000595A8
		StyleLength IStyle.left
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Left);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Left, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Left = this.ve.computedStyle.left.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0005B3FC File Offset: 0x000595FC
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x0005B41C File Offset: 0x0005961C
		StyleLength IStyle.letterSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.LetterSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.LetterSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0005B450 File Offset: 0x00059650
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x0005B470 File Offset: 0x00059670
		StyleLength IStyle.marginBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginBottom = this.ve.computedStyle.marginBottom.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0005B4C4 File Offset: 0x000596C4
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x0005B4E4 File Offset: 0x000596E4
		StyleLength IStyle.marginLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginLeft = this.ve.computedStyle.marginLeft.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0005B538 File Offset: 0x00059738
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x0005B558 File Offset: 0x00059758
		StyleLength IStyle.marginRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginRight = this.ve.computedStyle.marginRight.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0005B5AC File Offset: 0x000597AC
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x0005B5CC File Offset: 0x000597CC
		StyleLength IStyle.marginTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MarginTop = this.ve.computedStyle.marginTop.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0005B620 File Offset: 0x00059820
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x0005B640 File Offset: 0x00059840
		StyleLength IStyle.maxHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxHeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MaxHeight = this.ve.computedStyle.maxHeight.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0005B694 File Offset: 0x00059894
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x0005B6B4 File Offset: 0x000598B4
		StyleLength IStyle.maxWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MaxWidth = this.ve.computedStyle.maxWidth.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0005B708 File Offset: 0x00059908
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x0005B728 File Offset: 0x00059928
		StyleLength IStyle.minHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinHeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MinHeight = this.ve.computedStyle.minHeight.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0005B77C File Offset: 0x0005997C
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x0005B79C File Offset: 0x0005999C
		StyleLength IStyle.minWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.MinWidth = this.ve.computedStyle.minWidth.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0005B7F0 File Offset: 0x000599F0
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x0005B810 File Offset: 0x00059A10
		StyleFloat IStyle.opacity
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.Opacity);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Opacity, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Opacity);
				}
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0005B844 File Offset: 0x00059A44
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x0005B878 File Offset: 0x00059A78
		StyleEnum<Overflow> IStyle.overflow
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.Overflow);
				return new StyleEnum<Overflow>((Overflow)tmp.value, tmp.keyword);
			}
			set
			{
				StyleEnum<OverflowInternal> tmp = new StyleEnum<OverflowInternal>((OverflowInternal)value.value, value.keyword);
				bool flag = this.SetStyleValue<OverflowInternal>(StylePropertyId.Overflow, tmp);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Overflow);
					this.ve.layoutNode.Overflow = (LayoutOverflow)this.ve.computedStyle.overflow;
				}
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0005B8DC File Offset: 0x00059ADC
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x0005B8FC File Offset: 0x00059AFC
		StyleLength IStyle.paddingBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingBottom = this.ve.computedStyle.paddingBottom.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0005B950 File Offset: 0x00059B50
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x0005B970 File Offset: 0x00059B70
		StyleLength IStyle.paddingLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingLeft = this.ve.computedStyle.paddingLeft.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0005B9C4 File Offset: 0x00059BC4
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x0005B9E4 File Offset: 0x00059BE4
		StyleLength IStyle.paddingRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingRight = this.ve.computedStyle.paddingRight.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0005BA38 File Offset: 0x00059C38
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x0005BA58 File Offset: 0x00059C58
		StyleLength IStyle.paddingTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PaddingTop = this.ve.computedStyle.paddingTop.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0005BAAC File Offset: 0x00059CAC
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x0005BAE0 File Offset: 0x00059CE0
		StyleEnum<Position> IStyle.position
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.Position);
				return new StyleEnum<Position>((Position)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Position>(StylePropertyId.Position, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.PositionType = (LayoutPositionType)this.ve.computedStyle.position;
				}
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0005BB30 File Offset: 0x00059D30
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x0005BB50 File Offset: 0x00059D50
		StyleLength IStyle.right
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Right);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Right, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Right = this.ve.computedStyle.right.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0005BBA4 File Offset: 0x00059DA4
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		StyleEnum<TextOverflow> IStyle.textOverflow
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.TextOverflow);
				return new StyleEnum<TextOverflow>((TextOverflow)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextOverflow>(StylePropertyId.TextOverflow, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0005BC0C File Offset: 0x00059E0C
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x0005BC2C File Offset: 0x00059E2C
		StyleLength IStyle.top
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Top);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Top, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Top = this.ve.computedStyle.top.ToLayoutValue();
				}
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0005BC80 File Offset: 0x00059E80
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x0005BCA0 File Offset: 0x00059EA0
		StyleList<TimeValue> IStyle.transitionDelay
		{
			get
			{
				return this.GetStyleList<TimeValue>(StylePropertyId.TransitionDelay);
			}
			set
			{
				bool flag = this.SetStyleValue<TimeValue>(StylePropertyId.TransitionDelay, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005BCD4 File Offset: 0x00059ED4
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x0005BCF4 File Offset: 0x00059EF4
		StyleList<TimeValue> IStyle.transitionDuration
		{
			get
			{
				return this.GetStyleList<TimeValue>(StylePropertyId.TransitionDuration);
			}
			set
			{
				bool flag = this.SetStyleValue<TimeValue>(StylePropertyId.TransitionDuration, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0005BD28 File Offset: 0x00059F28
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x0005BD48 File Offset: 0x00059F48
		StyleList<StylePropertyName> IStyle.transitionProperty
		{
			get
			{
				return this.GetStyleList<StylePropertyName>(StylePropertyId.TransitionProperty);
			}
			set
			{
				bool flag = this.SetStyleValue<StylePropertyName>(StylePropertyId.TransitionProperty, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0005BD7C File Offset: 0x00059F7C
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x0005BD9C File Offset: 0x00059F9C
		StyleList<EasingFunction> IStyle.transitionTimingFunction
		{
			get
			{
				return this.GetStyleList<EasingFunction>(StylePropertyId.TransitionTimingFunction);
			}
			set
			{
				bool flag = this.SetStyleValue<EasingFunction>(StylePropertyId.TransitionTimingFunction, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0005BDCC File Offset: 0x00059FCC
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x0005BDEC File Offset: 0x00059FEC
		StyleColor IStyle.unityBackgroundImageTintColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityBackgroundImageTintColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0005BE20 File Offset: 0x0005A020
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x0005BE54 File Offset: 0x0005A054
		StyleEnum<EditorTextRenderingMode> IStyle.unityEditorTextRenderingMode
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.UnityEditorTextRenderingMode);
				return new StyleEnum<EditorTextRenderingMode>((EditorTextRenderingMode)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<EditorTextRenderingMode>(StylePropertyId.UnityEditorTextRenderingMode, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0005BE88 File Offset: 0x0005A088
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0005BEA8 File Offset: 0x0005A0A8
		StyleFont IStyle.unityFont
		{
			get
			{
				return base.GetStyleFont(StylePropertyId.UnityFont);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFont, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0005BEDC File Offset: 0x0005A0DC
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x0005BEFC File Offset: 0x0005A0FC
		StyleFontDefinition IStyle.unityFontDefinition
		{
			get
			{
				return base.GetStyleFontDefinition(StylePropertyId.UnityFontDefinition);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFontDefinition, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0005BF30 File Offset: 0x0005A130
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0005BF64 File Offset: 0x0005A164
		StyleEnum<FontStyle> IStyle.unityFontStyleAndWeight
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.UnityFontStyleAndWeight);
				return new StyleEnum<FontStyle>((FontStyle)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<FontStyle>(StylePropertyId.UnityFontStyleAndWeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0005BF98 File Offset: 0x0005A198
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0005BFCC File Offset: 0x0005A1CC
		StyleEnum<OverflowClipBox> IStyle.unityOverflowClipBox
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.UnityOverflowClipBox);
				return new StyleEnum<OverflowClipBox>((OverflowClipBox)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<OverflowClipBox>(StylePropertyId.UnityOverflowClipBox, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0005C000 File Offset: 0x0005A200
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0005C020 File Offset: 0x0005A220
		StyleLength IStyle.unityParagraphSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.UnityParagraphSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityParagraphSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0005C054 File Offset: 0x0005A254
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x0005C074 File Offset: 0x0005A274
		StyleInt IStyle.unitySliceBottom
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0005C0A8 File Offset: 0x0005A2A8
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x0005C0C8 File Offset: 0x0005A2C8
		StyleInt IStyle.unitySliceLeft
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0005C0FC File Offset: 0x0005A2FC
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x0005C11C File Offset: 0x0005A31C
		StyleInt IStyle.unitySliceRight
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0005C150 File Offset: 0x0005A350
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0005C170 File Offset: 0x0005A370
		StyleFloat IStyle.unitySliceScale
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.UnitySliceScale);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceScale, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0005C1A4 File Offset: 0x0005A3A4
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0005C1C4 File Offset: 0x0005A3C4
		StyleInt IStyle.unitySliceTop
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0005C1F8 File Offset: 0x0005A3F8
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x0005C22C File Offset: 0x0005A42C
		StyleEnum<TextAnchor> IStyle.unityTextAlign
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.UnityTextAlign);
				return new StyleEnum<TextAnchor>((TextAnchor)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextAnchor>(StylePropertyId.UnityTextAlign, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0005C260 File Offset: 0x0005A460
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x0005C294 File Offset: 0x0005A494
		StyleEnum<TextGeneratorType> IStyle.unityTextGenerator
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.UnityTextGenerator);
				return new StyleEnum<TextGeneratorType>((TextGeneratorType)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextGeneratorType>(StylePropertyId.UnityTextGenerator, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0005C2C8 File Offset: 0x0005A4C8
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0005C2E8 File Offset: 0x0005A4E8
		StyleColor IStyle.unityTextOutlineColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityTextOutlineColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityTextOutlineColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0005C31C File Offset: 0x0005A51C
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0005C33C File Offset: 0x0005A53C
		StyleFloat IStyle.unityTextOutlineWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.UnityTextOutlineWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityTextOutlineWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0005C370 File Offset: 0x0005A570
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x0005C3A4 File Offset: 0x0005A5A4
		StyleEnum<TextOverflowPosition> IStyle.unityTextOverflowPosition
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.UnityTextOverflowPosition);
				return new StyleEnum<TextOverflowPosition>((TextOverflowPosition)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextOverflowPosition>(StylePropertyId.UnityTextOverflowPosition, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0005C3D8 File Offset: 0x0005A5D8
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0005C40C File Offset: 0x0005A60C
		StyleEnum<Visibility> IStyle.visibility
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.Visibility);
				return new StyleEnum<Visibility>((Visibility)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Visibility>(StylePropertyId.Visibility, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint | VersionChangeType.Picking);
				}
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0005C440 File Offset: 0x0005A640
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0005C474 File Offset: 0x0005A674
		StyleEnum<WhiteSpace> IStyle.whiteSpace
		{
			get
			{
				StyleInt tmp = base.GetStyleInt(StylePropertyId.WhiteSpace);
				return new StyleEnum<WhiteSpace>((WhiteSpace)tmp.value, tmp.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<WhiteSpace>(StylePropertyId.WhiteSpace, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0005C4A4 File Offset: 0x0005A6A4
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0005C4C4 File Offset: 0x0005A6C4
		StyleLength IStyle.width
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Width);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Width, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.layoutNode.Width = this.ve.computedStyle.width.ToLayoutValue();
				}
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0005C518 File Offset: 0x0005A718
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0005C538 File Offset: 0x0005A738
		StyleLength IStyle.wordSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.WordSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.WordSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0005C569 File Offset: 0x0005A769
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x0005C571 File Offset: 0x0005A771
		private VisualElement ve { get; set; }

		// Token: 0x06001499 RID: 5273 RVA: 0x0005C57A File Offset: 0x0005A77A
		public InlineStyleAccess(VisualElement ve)
		{
			this.ve = ve;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0005C58C File Offset: 0x0005A78C
		protected override void Finalize()
		{
			try
			{
				StyleValue inlineValue = default(StyleValue);
				bool flag = base.TryGetStyleValue(StylePropertyId.BackgroundImage, ref inlineValue);
				if (flag)
				{
					bool isAllocated = inlineValue.resource.IsAllocated;
					if (isAllocated)
					{
						inlineValue.resource.Free();
					}
				}
				bool flag2 = base.TryGetStyleValue(StylePropertyId.UnityFont, ref inlineValue);
				if (flag2)
				{
					bool isAllocated2 = inlineValue.resource.IsAllocated;
					if (isAllocated2)
					{
						inlineValue.resource.Free();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0005C620 File Offset: 0x0005A820
		public void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			this.m_InlineRule.sheet = sheet;
			this.m_InlineRule.rule = rule;
			this.m_InlineRule.propertyIds = StyleSheetCache.GetPropertyIds(rule);
			this.ApplyInlineStyles(this.ve.computedStyle);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0005C660 File Offset: 0x0005A860
		public bool IsValueSet(StylePropertyId id)
		{
			foreach (StyleValue sv in this.m_Values)
			{
				bool flag = sv.id == id;
				if (flag)
				{
					return true;
				}
			}
			bool flag2 = this.m_ValuesManaged != null;
			if (flag2)
			{
				foreach (StyleValueManaged sv2 in this.m_ValuesManaged)
				{
					bool flag3 = sv2.id == id;
					if (flag3)
					{
						return true;
					}
				}
			}
			if (id <= StylePropertyId.Cursor)
			{
				if (id == StylePropertyId.TextShadow)
				{
					return this.m_HasInlineTextShadow;
				}
				if (id == StylePropertyId.Cursor)
				{
					return this.m_HasInlineCursor;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.Rotate:
					return this.m_HasInlineRotate;
				case StylePropertyId.Scale:
					return this.m_HasInlineScale;
				case StylePropertyId.TransformOrigin:
					return this.m_HasInlineTransformOrigin;
				case StylePropertyId.Translate:
					return this.m_HasInlineTranslate;
				default:
					if (id == StylePropertyId.BackgroundSize)
					{
						return this.m_HasInlineBackgroundSize;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0005C7C4 File Offset: 0x0005A9C4
		public void ApplyInlineStyles(ref ComputedStyle computedStyle)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ref ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle2 = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle parentStyle = ref ptr;
			bool flag = this.m_InlineRule.sheet != null;
			if (flag)
			{
				InlineStyleAccess.s_StylePropertyReader.SetInlineContext(this.m_InlineRule.sheet, this.m_InlineRule.rule.properties, this.m_InlineRule.propertyIds, 1f);
				computedStyle.ApplyProperties(InlineStyleAccess.s_StylePropertyReader, ref parentStyle);
			}
			foreach (StyleValue sv in this.m_Values)
			{
				computedStyle.ApplyStyleValue(sv, ref parentStyle);
			}
			bool flag2 = this.m_ValuesManaged != null;
			if (flag2)
			{
				foreach (StyleValueManaged sv2 in this.m_ValuesManaged)
				{
					computedStyle.ApplyStyleValueManaged(sv2, ref parentStyle);
				}
			}
			bool flag3 = this.ve.style.cursor.keyword != StyleKeyword.Null;
			if (flag3)
			{
				computedStyle.ApplyStyleCursor(this.ve.style.cursor.value);
			}
			bool flag4 = this.ve.style.textShadow.keyword != StyleKeyword.Null;
			if (flag4)
			{
				computedStyle.ApplyStyleTextShadow(this.ve.style.textShadow.value);
			}
			bool hasInlineTransformOrigin = this.m_HasInlineTransformOrigin;
			if (hasInlineTransformOrigin)
			{
				computedStyle.ApplyStyleTransformOrigin(this.ve.style.transformOrigin.value);
			}
			bool hasInlineTranslate = this.m_HasInlineTranslate;
			if (hasInlineTranslate)
			{
				computedStyle.ApplyStyleTranslate(this.ve.style.translate.value);
			}
			bool hasInlineScale = this.m_HasInlineScale;
			if (hasInlineScale)
			{
				computedStyle.ApplyStyleScale(this.ve.style.scale.value);
			}
			bool hasInlineRotate = this.m_HasInlineRotate;
			if (hasInlineRotate)
			{
				computedStyle.ApplyStyleRotate(this.ve.style.rotate.value);
			}
			bool hasInlineBackgroundSize = this.m_HasInlineBackgroundSize;
			if (hasInlineBackgroundSize)
			{
				computedStyle.ApplyStyleBackgroundSize(this.ve.style.backgroundSize.value);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0005CA7C File Offset: 0x0005AC7C
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x0005CAAC File Offset: 0x0005ACAC
		StyleCursor IStyle.cursor
		{
			get
			{
				StyleCursor inlineCursor = default(StyleCursor);
				bool flag = this.TryGetInlineCursor(ref inlineCursor);
				StyleCursor styleCursor;
				if (flag)
				{
					styleCursor = inlineCursor;
				}
				else
				{
					styleCursor = StyleKeyword.Null;
				}
				return styleCursor;
			}
			set
			{
				bool flag = this.SetInlineCursor(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0005CAD8 File Offset: 0x0005ACD8
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x0005CB08 File Offset: 0x0005AD08
		StyleTextShadow IStyle.textShadow
		{
			get
			{
				StyleTextShadow inlineTextShadow = default(StyleTextShadow);
				bool flag = this.TryGetInlineTextShadow(ref inlineTextShadow);
				StyleTextShadow styleTextShadow;
				if (flag)
				{
					styleTextShadow = inlineTextShadow;
				}
				else
				{
					styleTextShadow = StyleKeyword.Null;
				}
				return styleTextShadow;
			}
			set
			{
				bool flag = this.SetInlineTextShadow(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0005CB34 File Offset: 0x0005AD34
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x0005CB64 File Offset: 0x0005AD64
		StyleBackgroundSize IStyle.backgroundSize
		{
			get
			{
				StyleBackgroundSize inlineBackgroundSize = default(StyleBackgroundSize);
				bool flag = this.TryGetInlineBackgroundSize(ref inlineBackgroundSize);
				StyleBackgroundSize styleBackgroundSize;
				if (flag)
				{
					styleBackgroundSize = inlineBackgroundSize;
				}
				else
				{
					styleBackgroundSize = StyleKeyword.Null;
				}
				return styleBackgroundSize;
			}
			set
			{
				bool flag = this.SetInlineBackgroundSize(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0005CB90 File Offset: 0x0005AD90
		private StyleList<T> GetStyleList<T>(StylePropertyId id)
		{
			StyleValueManaged inline = default(StyleValueManaged);
			bool flag = this.TryGetStyleValueManaged(id, ref inline);
			StyleList<T> styleList;
			if (flag)
			{
				styleList = new StyleList<T>(inline.value as List<T>, inline.keyword);
			}
			else
			{
				styleList = StyleKeyword.Null;
			}
			return styleList;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0005CBD8 File Offset: 0x0005ADD8
		private void SetStyleValueManaged(StyleValueManaged value)
		{
			bool flag = this.m_ValuesManaged == null;
			if (flag)
			{
				this.m_ValuesManaged = new List<StyleValueManaged>();
			}
			for (int i = 0; i < this.m_ValuesManaged.Count; i++)
			{
				bool flag2 = this.m_ValuesManaged[i].id == value.id;
				if (flag2)
				{
					bool flag3 = value.keyword == StyleKeyword.Null;
					if (flag3)
					{
						this.m_ValuesManaged.RemoveAt(i);
					}
					else
					{
						this.m_ValuesManaged[i] = value;
					}
					return;
				}
			}
			this.m_ValuesManaged.Add(value);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0005CC78 File Offset: 0x0005AE78
		private bool TryGetStyleValueManaged(StylePropertyId id, ref StyleValueManaged value)
		{
			value.id = StylePropertyId.Unknown;
			bool flag = this.m_ValuesManaged == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (StyleValueManaged inlineStyle in this.m_ValuesManaged)
				{
					bool flag3 = inlineStyle.id == id;
					if (flag3)
					{
						value = inlineStyle;
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0005CD04 File Offset: 0x0005AF04
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0005CD34 File Offset: 0x0005AF34
		StyleTransformOrigin IStyle.transformOrigin
		{
			get
			{
				StyleTransformOrigin inlineTransformOrigin = default(StyleTransformOrigin);
				bool flag = this.TryGetInlineTransformOrigin(ref inlineTransformOrigin);
				StyleTransformOrigin styleTransformOrigin;
				if (flag)
				{
					styleTransformOrigin = inlineTransformOrigin;
				}
				else
				{
					styleTransformOrigin = StyleKeyword.Null;
				}
				return styleTransformOrigin;
			}
			set
			{
				bool flag = this.SetInlineTransformOrigin(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0005CD60 File Offset: 0x0005AF60
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x0005CD90 File Offset: 0x0005AF90
		StyleTranslate IStyle.translate
		{
			get
			{
				StyleTranslate inlineTranslate = default(StyleTranslate);
				bool flag = this.TryGetInlineTranslate(ref inlineTranslate);
				StyleTranslate styleTranslate;
				if (flag)
				{
					styleTranslate = inlineTranslate;
				}
				else
				{
					styleTranslate = StyleKeyword.Null;
				}
				return styleTranslate;
			}
			set
			{
				bool flag = this.SetInlineTranslate(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0005CDBC File Offset: 0x0005AFBC
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x0005CDEC File Offset: 0x0005AFEC
		StyleRotate IStyle.rotate
		{
			get
			{
				StyleRotate inlineRotate = default(StyleRotate);
				bool flag = this.TryGetInlineRotate(ref inlineRotate);
				StyleRotate styleRotate;
				if (flag)
				{
					styleRotate = inlineRotate;
				}
				else
				{
					styleRotate = StyleKeyword.Null;
				}
				return styleRotate;
			}
			set
			{
				bool flag = this.SetInlineRotate(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0005CE18 File Offset: 0x0005B018
		// (set) Token: 0x060014AE RID: 5294 RVA: 0x0005CE48 File Offset: 0x0005B048
		StyleScale IStyle.scale
		{
			get
			{
				StyleScale inlineScale = default(StyleScale);
				bool flag = this.TryGetInlineScale(ref inlineScale);
				StyleScale styleScale;
				if (flag)
				{
					styleScale = inlineScale;
				}
				else
				{
					styleScale = StyleKeyword.Null;
				}
				return styleScale;
			}
			set
			{
				bool flag = this.SetInlineScale(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005CE74 File Offset: 0x0005B074
		private bool SetStyleValue(StylePropertyId id, StyleBackgroundPosition inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.position == inlineValue.value && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.position = inlineValue.value;
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005CF34 File Offset: 0x0005B134
		private bool SetStyleValue(StylePropertyId id, StyleBackgroundRepeat inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.repeat == inlineValue.value && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.repeat = inlineValue.value;
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005CFF4 File Offset: 0x0005B1F4
		private bool SetStyleValue(StylePropertyId id, StyleLength inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.length == inlineValue.ToLength() && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.length = inlineValue.ToLength();
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005D0B0 File Offset: 0x0005B2B0
		private bool SetStyleValue(StylePropertyId id, StyleFloat inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.number == inlineValue.value && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.number = inlineValue.value;
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005D168 File Offset: 0x0005B368
		private bool SetStyleValue(StylePropertyId id, StyleInt inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.number == (float)inlineValue.value && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.number = (float)inlineValue.value;
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005D224 File Offset: 0x0005B424
		private bool SetStyleValue(StylePropertyId id, StyleColor inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.color == inlineValue.value && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.color = inlineValue.value;
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0005D2E4 File Offset: 0x0005B4E4
		private bool SetStyleValue<T>(StylePropertyId id, StyleEnum<T> inlineValue) where T : struct, IConvertible
		{
			StyleValue sv = default(StyleValue);
			int intValue = UnsafeUtility.EnumToInt<T>(inlineValue.value);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.number == (float)intValue && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.number = (float)intValue;
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0005D3A4 File Offset: 0x0005B5A4
		private bool SetStyleValue(StylePropertyId id, StyleBackground inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				VectorImage vectorImage = (sv.resource.IsAllocated ? (sv.resource.Target as VectorImage) : null);
				Sprite sprite = (sv.resource.IsAllocated ? (sv.resource.Target as Sprite) : null);
				Texture2D texture = (sv.resource.IsAllocated ? (sv.resource.Target as Texture2D) : null);
				RenderTexture renderTexture = (sv.resource.IsAllocated ? (sv.resource.Target as RenderTexture) : null);
				bool flag2 = vectorImage == inlineValue.value.vectorImage && texture == inlineValue.value.texture && sprite == inlineValue.value.sprite && renderTexture == inlineValue.value.renderTexture && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = sv.resource.IsAllocated;
				if (isAllocated)
				{
					sv.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.vectorImage != null;
			if (flag4)
			{
				sv.resource = GCHandle.Alloc(inlineValue.value.vectorImage);
			}
			else
			{
				bool flag5 = inlineValue.value.sprite != null;
				if (flag5)
				{
					sv.resource = GCHandle.Alloc(inlineValue.value.sprite);
				}
				else
				{
					bool flag6 = inlineValue.value.texture != null;
					if (flag6)
					{
						sv.resource = GCHandle.Alloc(inlineValue.value.texture);
					}
					else
					{
						bool flag7 = inlineValue.value.renderTexture != null;
						if (flag7)
						{
							sv.resource = GCHandle.Alloc(inlineValue.value.renderTexture);
						}
						else
						{
							sv.resource = default(GCHandle);
						}
					}
				}
			}
			base.SetStyleValue(sv);
			bool flag8 = inlineValue.keyword == StyleKeyword.Null;
			bool flag9;
			if (flag8)
			{
				flag9 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag9 = true;
			}
			return flag9;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0005D65C File Offset: 0x0005B85C
		private bool SetStyleValue(StylePropertyId id, StyleFontDefinition inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				Font font = (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null);
				FontAsset fontAsset = (sv.resource.IsAllocated ? (sv.resource.Target as FontAsset) : null);
				bool flag2 = font == inlineValue.value.font && fontAsset == inlineValue.value.fontAsset && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = sv.resource.IsAllocated;
				if (isAllocated)
				{
					sv.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.font != null;
			if (flag4)
			{
				sv.resource = GCHandle.Alloc(inlineValue.value.font);
			}
			else
			{
				bool flag5 = inlineValue.value.fontAsset != null;
				if (flag5)
				{
					sv.resource = GCHandle.Alloc(inlineValue.value.fontAsset);
				}
				else
				{
					sv.resource = default(GCHandle);
				}
			}
			base.SetStyleValue(sv);
			bool flag6 = inlineValue.keyword == StyleKeyword.Null;
			bool flag7;
			if (flag6)
			{
				flag7 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag7 = true;
			}
			return flag7;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0005D820 File Offset: 0x0005BA20
		private bool SetStyleValue(StylePropertyId id, StyleFont inlineValue)
		{
			StyleValue sv = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref sv);
			if (flag)
			{
				Font font = (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null);
				bool flag2 = font == inlineValue.value && sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = sv.resource.IsAllocated;
				if (isAllocated)
				{
					sv.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			sv.resource = ((inlineValue.value != null) ? GCHandle.Alloc(inlineValue.value) : default(GCHandle));
			base.SetStyleValue(sv);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				flag5 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0005D944 File Offset: 0x0005BB44
		private bool SetStyleValue<T>(StylePropertyId id, StyleList<T> inlineValue)
		{
			StyleValueManaged sv = default(StyleValueManaged);
			bool flag = this.TryGetStyleValueManaged(id, ref sv);
			if (flag)
			{
				bool flag2 = sv.keyword == inlineValue.keyword;
				if (flag2)
				{
					bool flag3 = sv.value == null && inlineValue.value == null;
					if (flag3)
					{
						return false;
					}
					List<T> list = sv.value as List<T>;
					bool flag4 = list != null && inlineValue.value != null && list.SequenceEqual(inlineValue.value);
					if (flag4)
					{
						return false;
					}
				}
			}
			else
			{
				bool flag5 = inlineValue.keyword == StyleKeyword.Null;
				if (flag5)
				{
					return false;
				}
			}
			sv.id = id;
			sv.keyword = inlineValue.keyword;
			bool flag6 = inlineValue.value != null;
			if (flag6)
			{
				bool flag7 = sv.value == null;
				if (flag7)
				{
					sv.value = new List<T>(inlineValue.value);
				}
				else
				{
					List<T> list2 = (List<T>)sv.value;
					list2.Clear();
					list2.AddRange(inlineValue.value);
				}
			}
			else
			{
				sv.value = null;
			}
			this.SetStyleValueManaged(sv);
			bool flag8 = inlineValue.keyword == StyleKeyword.Null;
			bool flag9;
			if (flag8)
			{
				flag9 = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(sv);
				flag9 = true;
			}
			return flag9;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0005DAA4 File Offset: 0x0005BCA4
		private bool SetInlineCursor(StyleCursor inlineValue)
		{
			StyleCursor styleCursor = default(StyleCursor);
			bool flag = this.TryGetInlineCursor(ref styleCursor);
			if (flag)
			{
				bool flag2 = styleCursor.value == inlineValue.value && styleCursor.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleCursor.value = inlineValue.value;
			styleCursor.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineCursor = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Cursor);
			}
			else
			{
				this.m_InlineCursor = styleCursor;
				this.m_HasInlineCursor = true;
				this.ApplyStyleCursor(styleCursor);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0005DB70 File Offset: 0x0005BD70
		private void ApplyStyleCursor(StyleCursor cursor)
		{
			this.ve.computedStyle.ApplyStyleCursor(cursor.value);
			BaseVisualElementPanel elementPanel = this.ve.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) : null) == this.ve;
			if (flag)
			{
				this.ve.elementPanel.cursorManager.SetCursor(cursor.value);
			}
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0005DBDC File Offset: 0x0005BDDC
		private bool SetInlineTextShadow(StyleTextShadow inlineValue)
		{
			StyleTextShadow styleTextShadow = default(StyleTextShadow);
			bool flag = this.TryGetInlineTextShadow(ref styleTextShadow);
			if (flag)
			{
				bool flag2 = styleTextShadow.value == inlineValue.value && styleTextShadow.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleTextShadow.value = inlineValue.value;
			styleTextShadow.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineTextShadow = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.TextShadow);
			}
			else
			{
				this.m_InlineTextShadow = styleTextShadow;
				this.m_HasInlineTextShadow = true;
				this.ApplyStyleTextShadow(styleTextShadow);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0005DCA8 File Offset: 0x0005BEA8
		private void ApplyStyleTextShadow(StyleTextShadow textShadow)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool startedTransition = false;
			ComputedTransitionProperty t;
			bool flag = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.TextShadow, out t);
			if (flag)
			{
				startedTransition = ComputedStyle.StartAnimationInlineTextShadow(this.ve, this.ve.computedStyle, textShadow, t.durationMs, t.delayMs, t.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TextShadow);
			}
			bool flag2 = !startedTransition;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleTextShadow(textShadow.value);
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0005DD70 File Offset: 0x0005BF70
		private bool SetInlineTransformOrigin(StyleTransformOrigin inlineValue)
		{
			StyleTransformOrigin styleTransformOrigin = default(StyleTransformOrigin);
			bool flag = this.TryGetInlineTransformOrigin(ref styleTransformOrigin);
			if (flag)
			{
				bool flag2 = styleTransformOrigin.value == inlineValue.value && styleTransformOrigin.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineTransformOrigin = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.TransformOrigin);
			}
			else
			{
				this.m_InlineTransformOrigin = inlineValue;
				this.m_HasInlineTransformOrigin = true;
				this.ApplyStyleTransformOrigin(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0005DE1C File Offset: 0x0005C01C
		private void ApplyStyleTransformOrigin(StyleTransformOrigin transformOrigin)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool startedTransition = false;
			ComputedTransitionProperty t;
			bool flag = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.TransformOrigin, out t);
			if (flag)
			{
				startedTransition = ComputedStyle.StartAnimationInlineTransformOrigin(this.ve, this.ve.computedStyle, transformOrigin, t.durationMs, t.delayMs, t.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TransformOrigin);
			}
			bool flag2 = !startedTransition;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleTransformOrigin(transformOrigin.value);
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0005DEE4 File Offset: 0x0005C0E4
		private bool SetInlineTranslate(StyleTranslate inlineValue)
		{
			StyleTranslate styleTranslate = default(StyleTranslate);
			bool flag = this.TryGetInlineTranslate(ref styleTranslate);
			if (flag)
			{
				bool flag2 = styleTranslate.value == inlineValue.value && styleTranslate.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineTranslate = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Translate);
			}
			else
			{
				this.m_InlineTranslateOperation = inlineValue;
				this.m_HasInlineTranslate = true;
				this.ApplyStyleTranslate(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0005DF90 File Offset: 0x0005C190
		private void ApplyStyleTranslate(StyleTranslate translate)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool startedTransition = false;
			ComputedTransitionProperty t;
			bool flag = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Translate, out t);
			if (flag)
			{
				startedTransition = ComputedStyle.StartAnimationInlineTranslate(this.ve, this.ve.computedStyle, translate, t.durationMs, t.delayMs, t.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Translate);
			}
			bool flag2 = !startedTransition;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleTranslate(translate.value);
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005E058 File Offset: 0x0005C258
		private bool SetInlineScale(StyleScale inlineValue)
		{
			StyleScale styleScale = default(StyleScale);
			bool flag = this.TryGetInlineScale(ref styleScale);
			if (flag)
			{
				bool flag2 = styleScale.value == inlineValue.value && styleScale.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineScale = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Scale);
			}
			else
			{
				this.m_InlineScale = inlineValue;
				this.m_HasInlineScale = true;
				this.ApplyStyleScale(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005E104 File Offset: 0x0005C304
		private void ApplyStyleScale(StyleScale scale)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool startedTransition = false;
			ComputedTransitionProperty t;
			bool flag = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Scale, out t);
			if (flag)
			{
				startedTransition = ComputedStyle.StartAnimationInlineScale(this.ve, this.ve.computedStyle, scale, t.durationMs, t.delayMs, t.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Scale);
			}
			bool flag2 = !startedTransition;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleScale(scale.value);
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0005E1CC File Offset: 0x0005C3CC
		private bool SetInlineRotate(StyleRotate inlineValue)
		{
			StyleRotate styleRotate = default(StyleRotate);
			bool flag = this.TryGetInlineRotate(ref styleRotate);
			if (flag)
			{
				bool flag2 = styleRotate.value == inlineValue.value && styleRotate.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineRotate = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.Rotate);
			}
			else
			{
				this.m_InlineRotateOperation = inlineValue;
				this.m_HasInlineRotate = true;
				this.ApplyStyleRotate(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0005E278 File Offset: 0x0005C478
		private void ApplyStyleRotate(StyleRotate rotate)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ref ComputedStyle computedStyle2 = ref parent.computedStyle;
			}
			else
			{
				InitialStyle.Get();
			}
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool startedTransition = false;
			ComputedTransitionProperty t;
			bool flag = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Rotate, out t);
			if (flag)
			{
				startedTransition = ComputedStyle.StartAnimationInlineRotate(this.ve, this.ve.computedStyle, rotate, t.durationMs, t.delayMs, t.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Rotate);
			}
			bool flag2 = !startedTransition;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleRotate(rotate.value);
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0005E374 File Offset: 0x0005C574
		private bool SetInlineBackgroundSize(StyleBackgroundSize inlineValue)
		{
			StyleBackgroundSize styleBackgroundSize = default(StyleBackgroundSize);
			bool flag = this.TryGetInlineBackgroundSize(ref styleBackgroundSize);
			if (flag)
			{
				bool flag2 = styleBackgroundSize.value == inlineValue.value && styleBackgroundSize.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool flag5;
			if (flag4)
			{
				this.m_HasInlineBackgroundSize = false;
				flag5 = this.RemoveInlineStyle(StylePropertyId.BackgroundSize);
			}
			else
			{
				this.m_InlineBackgroundSize = inlineValue;
				this.m_HasInlineBackgroundSize = true;
				this.ApplyStyleBackgroundSize(inlineValue);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0005E420 File Offset: 0x0005C620
		private void ApplyStyleBackgroundSize(StyleBackgroundSize backgroundSize)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool startedTransition = false;
			ComputedTransitionProperty t;
			bool flag = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.BackgroundSize, out t);
			if (flag)
			{
				startedTransition = ComputedStyle.StartAnimationInlineBackgroundSize(this.ve, this.ve.computedStyle, backgroundSize, t.durationMs, t.delayMs, t.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TransformOrigin);
			}
			bool flag2 = !startedTransition;
			if (flag2)
			{
				this.ve.computedStyle.ApplyStyleBackgroundSize(backgroundSize.value);
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0005E4E8 File Offset: 0x0005C6E8
		private void ApplyStyleValue(StyleValue value)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ref ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle parentStyle = ref ptr;
			bool startedTransition = false;
			bool flag = StylePropertyUtil.IsAnimatable(value.id);
			if (flag)
			{
				ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
				ComputedTransitionProperty t;
				bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(value.id, out t);
				if (flag2)
				{
					startedTransition = ComputedStyle.StartAnimationInline(this.ve, value.id, this.ve.computedStyle, value, t.durationMs, t.delayMs, t.easingCurve);
				}
				else
				{
					this.ve.styleAnimation.CancelAnimation(value.id);
				}
			}
			bool flag3 = !startedTransition;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleValue(value, ref parentStyle);
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005E5FC File Offset: 0x0005C7FC
		private void ApplyStyleValue(StyleValueManaged value)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ref ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle parentStyle = ref ptr;
			this.ve.computedStyle.ApplyStyleValueManaged(value, ref parentStyle);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005E64C File Offset: 0x0005C84C
		private bool RemoveInlineStyle(StylePropertyId id)
		{
			long rulesHash = this.ve.computedStyle.matchingRulesHash;
			bool flag = rulesHash == 0L;
			bool flag2;
			if (flag)
			{
				this.ApplyFromComputedStyle(id, InitialStyle.Get());
				flag2 = true;
			}
			else
			{
				ComputedStyle baseComputedStyle;
				bool flag3 = StyleCache.TryGetValue(rulesHash, out baseComputedStyle);
				if (flag3)
				{
					this.ApplyFromComputedStyle(id, ref baseComputedStyle);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0005E6A8 File Offset: 0x0005C8A8
		private void ApplyFromComputedStyle(StylePropertyId id, ref ComputedStyle newStyle)
		{
			bool startedTransition = false;
			bool flag = StylePropertyUtil.IsAnimatable(id);
			if (flag)
			{
				ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
				ComputedTransitionProperty t;
				bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(id, out t);
				if (flag2)
				{
					startedTransition = ComputedStyle.StartAnimation(this.ve, id, this.ve.computedStyle, ref newStyle, t.durationMs, t.delayMs, t.easingCurve);
				}
				else
				{
					this.ve.styleAnimation.CancelAnimation(id);
				}
			}
			bool flag3 = !startedTransition;
			if (flag3)
			{
				this.ve.computedStyle.ApplyFromComputedStyle(id, ref newStyle);
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0005E774 File Offset: 0x0005C974
		public bool TryGetInlineCursor(ref StyleCursor value)
		{
			bool hasInlineCursor = this.m_HasInlineCursor;
			bool flag;
			if (hasInlineCursor)
			{
				value = this.m_InlineCursor;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0005E7A4 File Offset: 0x0005C9A4
		public bool TryGetInlineTextShadow(ref StyleTextShadow value)
		{
			bool hasInlineTextShadow = this.m_HasInlineTextShadow;
			bool flag;
			if (hasInlineTextShadow)
			{
				value = this.m_InlineTextShadow;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0005E7D4 File Offset: 0x0005C9D4
		public bool TryGetInlineTransformOrigin(ref StyleTransformOrigin value)
		{
			bool hasInlineTransformOrigin = this.m_HasInlineTransformOrigin;
			bool flag;
			if (hasInlineTransformOrigin)
			{
				value = this.m_InlineTransformOrigin;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0005E804 File Offset: 0x0005CA04
		public bool TryGetInlineTranslate(ref StyleTranslate value)
		{
			bool hasInlineTranslate = this.m_HasInlineTranslate;
			bool flag;
			if (hasInlineTranslate)
			{
				value = this.m_InlineTranslateOperation;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0005E834 File Offset: 0x0005CA34
		public bool TryGetInlineRotate(ref StyleRotate value)
		{
			bool hasInlineRotate = this.m_HasInlineRotate;
			bool flag;
			if (hasInlineRotate)
			{
				value = this.m_InlineRotateOperation;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0005E864 File Offset: 0x0005CA64
		public bool TryGetInlineScale(ref StyleScale value)
		{
			bool hasInlineScale = this.m_HasInlineScale;
			bool flag;
			if (hasInlineScale)
			{
				value = this.m_InlineScale;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0005E894 File Offset: 0x0005CA94
		public bool TryGetInlineBackgroundSize(ref StyleBackgroundSize value)
		{
			bool hasInlineBackgroundSize = this.m_HasInlineBackgroundSize;
			bool flag;
			if (hasInlineBackgroundSize)
			{
				value = this.m_InlineBackgroundSize;
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000B4C RID: 2892
		private static StylePropertyReader s_StylePropertyReader = new StylePropertyReader();

		// Token: 0x04000B4D RID: 2893
		private List<StyleValueManaged> m_ValuesManaged;

		// Token: 0x04000B4F RID: 2895
		private bool m_HasInlineCursor;

		// Token: 0x04000B50 RID: 2896
		private StyleCursor m_InlineCursor;

		// Token: 0x04000B51 RID: 2897
		private bool m_HasInlineTextShadow;

		// Token: 0x04000B52 RID: 2898
		private StyleTextShadow m_InlineTextShadow;

		// Token: 0x04000B53 RID: 2899
		private bool m_HasInlineTransformOrigin;

		// Token: 0x04000B54 RID: 2900
		private StyleTransformOrigin m_InlineTransformOrigin;

		// Token: 0x04000B55 RID: 2901
		private bool m_HasInlineTranslate;

		// Token: 0x04000B56 RID: 2902
		private StyleTranslate m_InlineTranslateOperation;

		// Token: 0x04000B57 RID: 2903
		private bool m_HasInlineRotate;

		// Token: 0x04000B58 RID: 2904
		private StyleRotate m_InlineRotateOperation;

		// Token: 0x04000B59 RID: 2905
		private bool m_HasInlineScale;

		// Token: 0x04000B5A RID: 2906
		private StyleScale m_InlineScale;

		// Token: 0x04000B5B RID: 2907
		private bool m_HasInlineBackgroundSize;

		// Token: 0x04000B5C RID: 2908
		public StyleBackgroundSize m_InlineBackgroundSize;

		// Token: 0x04000B5D RID: 2909
		private InlineStyleAccess.InlineRule m_InlineRule;

		// Token: 0x020002D4 RID: 724
		internal struct InlineRule
		{
			// Token: 0x04000B5E RID: 2910
			public StyleSheet sheet;

			// Token: 0x04000B5F RID: 2911
			public StyleRule rule;

			// Token: 0x04000B60 RID: 2912
			public StylePropertyId[] propertyIds;
		}
	}
}
