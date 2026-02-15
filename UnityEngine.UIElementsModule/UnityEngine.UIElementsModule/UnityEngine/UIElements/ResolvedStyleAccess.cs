using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000344 RID: 836
	internal class ResolvedStyleAccess : IResolvedStyle
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0005FF81 File Offset: 0x0005E181
		public Align alignContent
		{
			get
			{
				return this.ve.computedStyle.alignContent;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0005FF93 File Offset: 0x0005E193
		public Align alignItems
		{
			get
			{
				return this.ve.computedStyle.alignItems;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0005FFA5 File Offset: 0x0005E1A5
		public Align alignSelf
		{
			get
			{
				return this.ve.computedStyle.alignSelf;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x0005FFB7 File Offset: 0x0005E1B7
		public Color backgroundColor
		{
			get
			{
				return this.ve.computedStyle.backgroundColor;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0005FFC9 File Offset: 0x0005E1C9
		public Background backgroundImage
		{
			get
			{
				return this.ve.computedStyle.backgroundImage;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0005FFDB File Offset: 0x0005E1DB
		public BackgroundPosition backgroundPositionX
		{
			get
			{
				return this.ve.computedStyle.backgroundPositionX;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0005FFED File Offset: 0x0005E1ED
		public BackgroundPosition backgroundPositionY
		{
			get
			{
				return this.ve.computedStyle.backgroundPositionY;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0005FFFF File Offset: 0x0005E1FF
		public BackgroundRepeat backgroundRepeat
		{
			get
			{
				return this.ve.computedStyle.backgroundRepeat;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00060011 File Offset: 0x0005E211
		public BackgroundSize backgroundSize
		{
			get
			{
				return this.ve.computedStyle.backgroundSize;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00060023 File Offset: 0x0005E223
		public Color borderBottomColor
		{
			get
			{
				return this.ve.computedStyle.borderBottomColor;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00060038 File Offset: 0x0005E238
		public float borderBottomLeftRadius
		{
			get
			{
				return this.ve.computedStyle.borderBottomLeftRadius.value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00060060 File Offset: 0x0005E260
		public float borderBottomRightRadius
		{
			get
			{
				return this.ve.computedStyle.borderBottomRightRadius.value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00060085 File Offset: 0x0005E285
		public float borderBottomWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderBottom;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00060097 File Offset: 0x0005E297
		public Color borderLeftColor
		{
			get
			{
				return this.ve.computedStyle.borderLeftColor;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x000600A9 File Offset: 0x0005E2A9
		public float borderLeftWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderLeft;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x000600BB File Offset: 0x0005E2BB
		public Color borderRightColor
		{
			get
			{
				return this.ve.computedStyle.borderRightColor;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x000600CD File Offset: 0x0005E2CD
		public float borderRightWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderRight;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x000600DF File Offset: 0x0005E2DF
		public Color borderTopColor
		{
			get
			{
				return this.ve.computedStyle.borderTopColor;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x000600F4 File Offset: 0x0005E2F4
		public float borderTopLeftRadius
		{
			get
			{
				return this.ve.computedStyle.borderTopLeftRadius.value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x0006011C File Offset: 0x0005E31C
		public float borderTopRightRadius
		{
			get
			{
				return this.ve.computedStyle.borderTopRightRadius.value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x00060141 File Offset: 0x0005E341
		public float borderTopWidth
		{
			get
			{
				return this.ve.layoutNode.LayoutBorderTop;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x00060153 File Offset: 0x0005E353
		public float bottom
		{
			get
			{
				return this.ve.layoutNode.LayoutBottom;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x00060165 File Offset: 0x0005E365
		public Color color
		{
			get
			{
				return this.ve.computedStyle.color;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00060177 File Offset: 0x0005E377
		public DisplayStyle display
		{
			get
			{
				return this.ve.computedStyle.display;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00060189 File Offset: 0x0005E389
		public StyleFloat flexBasis
		{
			get
			{
				return new StyleFloat(this.ve.layoutNode.ComputedFlexBasis);
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000601A0 File Offset: 0x0005E3A0
		public FlexDirection flexDirection
		{
			get
			{
				return this.ve.computedStyle.flexDirection;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x000601B2 File Offset: 0x0005E3B2
		public float flexGrow
		{
			get
			{
				return this.ve.computedStyle.flexGrow;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x000601C4 File Offset: 0x0005E3C4
		public float flexShrink
		{
			get
			{
				return this.ve.computedStyle.flexShrink;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x000601D6 File Offset: 0x0005E3D6
		public Wrap flexWrap
		{
			get
			{
				return this.ve.computedStyle.flexWrap;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x000601E8 File Offset: 0x0005E3E8
		public float fontSize
		{
			get
			{
				return this.ve.computedStyle.fontSize.value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0006020D File Offset: 0x0005E40D
		public float height
		{
			get
			{
				return this.ve.layoutNode.LayoutHeight;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0006021F File Offset: 0x0005E41F
		public Justify justifyContent
		{
			get
			{
				return this.ve.computedStyle.justifyContent;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00060231 File Offset: 0x0005E431
		public float left
		{
			get
			{
				return this.ve.layoutNode.LayoutX;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00060244 File Offset: 0x0005E444
		public float letterSpacing
		{
			get
			{
				return this.ve.computedStyle.letterSpacing.value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00060269 File Offset: 0x0005E469
		public float marginBottom
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginBottom;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0006027B File Offset: 0x0005E47B
		public float marginLeft
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginLeft;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0006028D File Offset: 0x0005E48D
		public float marginRight
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginRight;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0006029F File Offset: 0x0005E49F
		public float marginTop
		{
			get
			{
				return this.ve.layoutNode.LayoutMarginTop;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x000602B1 File Offset: 0x0005E4B1
		public StyleFloat maxHeight
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.maxHeight, false);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x000602CF File Offset: 0x0005E4CF
		public StyleFloat maxWidth
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.maxWidth, true);
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x000602ED File Offset: 0x0005E4ED
		public StyleFloat minHeight
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.minHeight, false);
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0006030B File Offset: 0x0005E50B
		public StyleFloat minWidth
		{
			get
			{
				return this.ve.ResolveLengthValue(this.ve.computedStyle.minWidth, true);
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x00060329 File Offset: 0x0005E529
		public float opacity
		{
			get
			{
				return this.ve.computedStyle.opacity;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0006033B File Offset: 0x0005E53B
		public float paddingBottom
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingBottom;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0006034D File Offset: 0x0005E54D
		public float paddingLeft
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingLeft;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0006035F File Offset: 0x0005E55F
		public float paddingRight
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingRight;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00060371 File Offset: 0x0005E571
		public float paddingTop
		{
			get
			{
				return this.ve.layoutNode.LayoutPaddingTop;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x00060383 File Offset: 0x0005E583
		public Position position
		{
			get
			{
				return this.ve.computedStyle.position;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00060395 File Offset: 0x0005E595
		public float right
		{
			get
			{
				return this.ve.layoutNode.LayoutRight;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x000603A7 File Offset: 0x0005E5A7
		public Rotate rotate
		{
			get
			{
				return this.ve.computedStyle.rotate;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x000603B9 File Offset: 0x0005E5B9
		public Scale scale
		{
			get
			{
				return this.ve.computedStyle.scale;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x000603CB File Offset: 0x0005E5CB
		public TextOverflow textOverflow
		{
			get
			{
				return this.ve.computedStyle.textOverflow;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x000603DD File Offset: 0x0005E5DD
		public float top
		{
			get
			{
				return this.ve.layoutNode.LayoutY;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x000603EF File Offset: 0x0005E5EF
		public Vector3 transformOrigin
		{
			get
			{
				return this.ve.ResolveTransformOrigin();
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x000603FC File Offset: 0x0005E5FC
		public IEnumerable<TimeValue> transitionDelay
		{
			get
			{
				return this.ve.computedStyle.transitionDelay;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0006040E File Offset: 0x0005E60E
		public IEnumerable<TimeValue> transitionDuration
		{
			get
			{
				return this.ve.computedStyle.transitionDuration;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00060420 File Offset: 0x0005E620
		public IEnumerable<StylePropertyName> transitionProperty
		{
			get
			{
				return this.ve.computedStyle.transitionProperty;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x00060432 File Offset: 0x0005E632
		public IEnumerable<EasingFunction> transitionTimingFunction
		{
			get
			{
				return this.ve.computedStyle.transitionTimingFunction;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00060444 File Offset: 0x0005E644
		public Vector3 translate
		{
			get
			{
				return this.ve.ResolveTranslate();
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00060451 File Offset: 0x0005E651
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.ve.computedStyle.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x00060463 File Offset: 0x0005E663
		public EditorTextRenderingMode unityEditorTextRenderingMode
		{
			get
			{
				return this.ve.computedStyle.unityEditorTextRenderingMode;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00060475 File Offset: 0x0005E675
		public Font unityFont
		{
			get
			{
				return this.ve.computedStyle.unityFont;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00060487 File Offset: 0x0005E687
		public FontDefinition unityFontDefinition
		{
			get
			{
				return this.ve.computedStyle.unityFontDefinition;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00060499 File Offset: 0x0005E699
		public FontStyle unityFontStyleAndWeight
		{
			get
			{
				return this.ve.computedStyle.unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000604AC File Offset: 0x0005E6AC
		public float unityParagraphSpacing
		{
			get
			{
				return this.ve.computedStyle.unityParagraphSpacing.value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x000604D1 File Offset: 0x0005E6D1
		public int unitySliceBottom
		{
			get
			{
				return this.ve.computedStyle.unitySliceBottom;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000604E3 File Offset: 0x0005E6E3
		public int unitySliceLeft
		{
			get
			{
				return this.ve.computedStyle.unitySliceLeft;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x000604F5 File Offset: 0x0005E6F5
		public int unitySliceRight
		{
			get
			{
				return this.ve.computedStyle.unitySliceRight;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00060507 File Offset: 0x0005E707
		public float unitySliceScale
		{
			get
			{
				return this.ve.computedStyle.unitySliceScale;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x00060519 File Offset: 0x0005E719
		public int unitySliceTop
		{
			get
			{
				return this.ve.computedStyle.unitySliceTop;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0006052B File Offset: 0x0005E72B
		public TextAnchor unityTextAlign
		{
			get
			{
				return this.ve.computedStyle.unityTextAlign;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0006053D File Offset: 0x0005E73D
		public TextGeneratorType unityTextGenerator
		{
			get
			{
				return this.ve.computedStyle.unityTextGenerator;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x0006054F File Offset: 0x0005E74F
		public Color unityTextOutlineColor
		{
			get
			{
				return this.ve.computedStyle.unityTextOutlineColor;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00060561 File Offset: 0x0005E761
		public float unityTextOutlineWidth
		{
			get
			{
				return this.ve.computedStyle.unityTextOutlineWidth;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x00060573 File Offset: 0x0005E773
		public TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return this.ve.computedStyle.unityTextOverflowPosition;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00060585 File Offset: 0x0005E785
		public Visibility visibility
		{
			get
			{
				return this.ve.computedStyle.visibility;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x00060597 File Offset: 0x0005E797
		public WhiteSpace whiteSpace
		{
			get
			{
				return this.ve.computedStyle.whiteSpace;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x000605A9 File Offset: 0x0005E7A9
		public float width
		{
			get
			{
				return this.ve.layoutNode.LayoutWidth;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x000605BC File Offset: 0x0005E7BC
		public float wordSpacing
		{
			get
			{
				return this.ve.computedStyle.wordSpacing.value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x000605E1 File Offset: 0x0005E7E1
		private VisualElement ve { get; }

		// Token: 0x06001847 RID: 6215 RVA: 0x000605E9 File Offset: 0x0005E7E9
		public ResolvedStyleAccess(VisualElement ve)
		{
			this.ve = ve;
		}
	}
}
