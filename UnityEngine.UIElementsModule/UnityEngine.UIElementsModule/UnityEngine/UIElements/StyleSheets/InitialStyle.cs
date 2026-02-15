using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005A7 RID: 1447
	internal static class InitialStyle
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x0009B8C8 File Offset: 0x00099AC8
		public static ref ComputedStyle Get()
		{
			return ref InitialStyle.s_InitialStyle;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x0009B8E0 File Offset: 0x00099AE0
		public static ComputedStyle Acquire()
		{
			return InitialStyle.s_InitialStyle.Acquire();
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0009B8FC File Offset: 0x00099AFC
		static InitialStyle()
		{
			InitialStyle.s_InitialStyle.layoutData.Write().alignContent = Align.FlexStart;
			InitialStyle.s_InitialStyle.layoutData.Write().alignItems = Align.Stretch;
			InitialStyle.s_InitialStyle.layoutData.Write().alignSelf = Align.Auto;
			InitialStyle.s_InitialStyle.visualData.Write().backgroundColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().backgroundImage = default(Background);
			InitialStyle.s_InitialStyle.visualData.Write().backgroundPositionX = BackgroundPosition.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().backgroundPositionY = BackgroundPosition.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().backgroundRepeat = BackgroundRepeat.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().backgroundSize = BackgroundSize.Initial();
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomLeftRadius = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderBottomRightRadius = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().borderBottomWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderLeftColor = Color.clear;
			InitialStyle.s_InitialStyle.layoutData.Write().borderLeftWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderRightColor = Color.clear;
			InitialStyle.s_InitialStyle.layoutData.Write().borderRightWidth = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopColor = Color.clear;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopLeftRadius = 0f;
			InitialStyle.s_InitialStyle.visualData.Write().borderTopRightRadius = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().borderTopWidth = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().bottom = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().color = Color.black;
			InitialStyle.s_InitialStyle.rareData.Write().cursor = default(Cursor);
			InitialStyle.s_InitialStyle.layoutData.Write().display = DisplayStyle.Flex;
			InitialStyle.s_InitialStyle.layoutData.Write().flexBasis = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().flexDirection = FlexDirection.Column;
			InitialStyle.s_InitialStyle.layoutData.Write().flexGrow = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().flexShrink = 1f;
			InitialStyle.s_InitialStyle.layoutData.Write().flexWrap = Wrap.NoWrap;
			InitialStyle.s_InitialStyle.inheritedData.Write().fontSize = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().height = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().justifyContent = Justify.FlexStart;
			InitialStyle.s_InitialStyle.layoutData.Write().left = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().letterSpacing = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginBottom = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginLeft = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginRight = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().marginTop = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().maxHeight = StyleKeyword.None.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().maxWidth = StyleKeyword.None.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().minHeight = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.layoutData.Write().minWidth = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.visualData.Write().opacity = 1f;
			InitialStyle.s_InitialStyle.visualData.Write().overflow = OverflowInternal.Visible;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingBottom = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingLeft = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingRight = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().paddingTop = 0f;
			InitialStyle.s_InitialStyle.layoutData.Write().position = Position.Relative;
			InitialStyle.s_InitialStyle.layoutData.Write().right = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.transformData.Write().rotate = StyleKeyword.None.ToRotate();
			InitialStyle.s_InitialStyle.transformData.Write().scale = StyleKeyword.None.ToScale();
			InitialStyle.s_InitialStyle.rareData.Write().textOverflow = TextOverflow.Clip;
			InitialStyle.s_InitialStyle.inheritedData.Write().textShadow = default(TextShadow);
			InitialStyle.s_InitialStyle.layoutData.Write().top = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.transformData.Write().transformOrigin = TransformOrigin.Initial();
			InitialStyle.s_InitialStyle.transitionData.Write().transitionDelay = new List<TimeValue> { 0f };
			InitialStyle.s_InitialStyle.transitionData.Write().transitionDuration = new List<TimeValue> { 0f };
			InitialStyle.s_InitialStyle.transitionData.Write().transitionProperty = new List<StylePropertyName> { "all" };
			InitialStyle.s_InitialStyle.transitionData.Write().transitionTimingFunction = new List<EasingFunction> { EasingMode.Ease };
			InitialStyle.s_InitialStyle.transformData.Write().translate = StyleKeyword.None.ToTranslate();
			InitialStyle.s_InitialStyle.rareData.Write().unityBackgroundImageTintColor = Color.white;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityEditorTextRenderingMode = EditorTextRenderingMode.SDF;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFont = null;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFontDefinition = default(FontDefinition);
			InitialStyle.s_InitialStyle.inheritedData.Write().unityFontStyleAndWeight = FontStyle.Normal;
			InitialStyle.s_InitialStyle.rareData.Write().unityOverflowClipBox = OverflowClipBox.PaddingBox;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityParagraphSpacing = 0f;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceBottom = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceLeft = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceRight = 0;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceScale = 1f;
			InitialStyle.s_InitialStyle.rareData.Write().unitySliceTop = 0;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextAlign = TextAnchor.UpperLeft;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextGenerator = TextGeneratorType.Standard;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextOutlineColor = Color.clear;
			InitialStyle.s_InitialStyle.inheritedData.Write().unityTextOutlineWidth = 0f;
			InitialStyle.s_InitialStyle.rareData.Write().unityTextOverflowPosition = TextOverflowPosition.End;
			InitialStyle.s_InitialStyle.inheritedData.Write().visibility = Visibility.Visible;
			InitialStyle.s_InitialStyle.inheritedData.Write().whiteSpace = WhiteSpace.Normal;
			InitialStyle.s_InitialStyle.layoutData.Write().width = StyleKeyword.Auto.ToLength();
			InitialStyle.s_InitialStyle.inheritedData.Write().wordSpacing = 0f;
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x0009C175 File Offset: 0x0009A375
		public static Align alignContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignContent;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x0009C18B File Offset: 0x0009A38B
		public static Align alignItems
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignItems;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x0009C1A1 File Offset: 0x0009A3A1
		public static Align alignSelf
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().alignSelf;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x0009C1B7 File Offset: 0x0009A3B7
		public static Color backgroundColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundColor;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x0009C1CD File Offset: 0x0009A3CD
		public static Background backgroundImage
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundImage;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x0009C1E3 File Offset: 0x0009A3E3
		public static BackgroundPosition backgroundPositionX
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundPositionX;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x0009C1F9 File Offset: 0x0009A3F9
		public static BackgroundPosition backgroundPositionY
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundPositionY;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x0009C20F File Offset: 0x0009A40F
		public static BackgroundRepeat backgroundRepeat
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundRepeat;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x0009C225 File Offset: 0x0009A425
		public static BackgroundSize backgroundSize
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().backgroundSize;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x0009C23B File Offset: 0x0009A43B
		public static Color borderBottomColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomColor;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x0009C251 File Offset: 0x0009A451
		public static Length borderBottomLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomLeftRadius;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x0009C267 File Offset: 0x0009A467
		public static Length borderBottomRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderBottomRightRadius;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x0009C27D File Offset: 0x0009A47D
		public static float borderBottomWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderBottomWidth;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0009C293 File Offset: 0x0009A493
		public static Color borderLeftColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderLeftColor;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x0009C2A9 File Offset: 0x0009A4A9
		public static float borderLeftWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderLeftWidth;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0009C2BF File Offset: 0x0009A4BF
		public static Color borderRightColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderRightColor;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x0009C2D5 File Offset: 0x0009A4D5
		public static float borderRightWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderRightWidth;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x0009C2EB File Offset: 0x0009A4EB
		public static Color borderTopColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopColor;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x0009C301 File Offset: 0x0009A501
		public static Length borderTopLeftRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopLeftRadius;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x0009C317 File Offset: 0x0009A517
		public static Length borderTopRightRadius
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().borderTopRightRadius;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x0009C32D File Offset: 0x0009A52D
		public static float borderTopWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().borderTopWidth;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x0009C343 File Offset: 0x0009A543
		public static Length bottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().bottom;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x0009C359 File Offset: 0x0009A559
		public static Color color
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().color;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x0009C36F File Offset: 0x0009A56F
		public static Cursor cursor
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().cursor;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x0009C385 File Offset: 0x0009A585
		public static DisplayStyle display
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().display;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x0009C39B File Offset: 0x0009A59B
		public static Length flexBasis
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexBasis;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x0009C3B1 File Offset: 0x0009A5B1
		public static FlexDirection flexDirection
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexDirection;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x0009C3C7 File Offset: 0x0009A5C7
		public static float flexGrow
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexGrow;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x0009C3DD File Offset: 0x0009A5DD
		public static float flexShrink
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexShrink;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x0009C3F3 File Offset: 0x0009A5F3
		public static Wrap flexWrap
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().flexWrap;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x0009C409 File Offset: 0x0009A609
		public static Length fontSize
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().fontSize;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x0009C41F File Offset: 0x0009A61F
		public static Length height
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().height;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x0009C435 File Offset: 0x0009A635
		public static Justify justifyContent
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().justifyContent;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x0009C44B File Offset: 0x0009A64B
		public static Length left
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().left;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x0009C461 File Offset: 0x0009A661
		public static Length letterSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().letterSpacing;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x0009C477 File Offset: 0x0009A677
		public static Length marginBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginBottom;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x0009C48D File Offset: 0x0009A68D
		public static Length marginLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginLeft;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x0009C4A3 File Offset: 0x0009A6A3
		public static Length marginRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginRight;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x0009C4B9 File Offset: 0x0009A6B9
		public static Length marginTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().marginTop;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x0009C4CF File Offset: 0x0009A6CF
		public static Length maxHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().maxHeight;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x0009C4E5 File Offset: 0x0009A6E5
		public static Length maxWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().maxWidth;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x0009C4FB File Offset: 0x0009A6FB
		public static Length minHeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().minHeight;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0009C511 File Offset: 0x0009A711
		public static Length minWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().minWidth;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x0009C527 File Offset: 0x0009A727
		public static float opacity
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().opacity;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0009C53D File Offset: 0x0009A73D
		public static OverflowInternal overflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.visualData.Read().overflow;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x0009C553 File Offset: 0x0009A753
		public static Length paddingBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingBottom;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x0009C569 File Offset: 0x0009A769
		public static Length paddingLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingLeft;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0009C57F File Offset: 0x0009A77F
		public static Length paddingRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingRight;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x0009C595 File Offset: 0x0009A795
		public static Length paddingTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().paddingTop;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x0009C5AB File Offset: 0x0009A7AB
		public static Position position
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().position;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x0009C5C1 File Offset: 0x0009A7C1
		public static Length right
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().right;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0009C5D7 File Offset: 0x0009A7D7
		public static Rotate rotate
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().rotate;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x0009C5ED File Offset: 0x0009A7ED
		public static Scale scale
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().scale;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x0009C603 File Offset: 0x0009A803
		public static TextOverflow textOverflow
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().textOverflow;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x0009C619 File Offset: 0x0009A819
		public static TextShadow textShadow
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().textShadow;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x0009C62F File Offset: 0x0009A82F
		public static Length top
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().top;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x0009C645 File Offset: 0x0009A845
		public static TransformOrigin transformOrigin
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().transformOrigin;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x0009C65B File Offset: 0x0009A85B
		public static List<TimeValue> transitionDelay
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionDelay;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x0009C671 File Offset: 0x0009A871
		public static List<TimeValue> transitionDuration
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionDuration;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x0009C687 File Offset: 0x0009A887
		public static List<StylePropertyName> transitionProperty
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionProperty;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x0009C69D File Offset: 0x0009A89D
		public static List<EasingFunction> transitionTimingFunction
		{
			get
			{
				return InitialStyle.s_InitialStyle.transitionData.Read().transitionTimingFunction;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x0009C6B3 File Offset: 0x0009A8B3
		public static Translate translate
		{
			get
			{
				return InitialStyle.s_InitialStyle.transformData.Read().translate;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x0009C6C9 File Offset: 0x0009A8C9
		public static Color unityBackgroundImageTintColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityBackgroundImageTintColor;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0009C6DF File Offset: 0x0009A8DF
		public static EditorTextRenderingMode unityEditorTextRenderingMode
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityEditorTextRenderingMode;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x0009C6F5 File Offset: 0x0009A8F5
		public static Font unityFont
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFont;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x0009C70B File Offset: 0x0009A90B
		public static FontDefinition unityFontDefinition
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFontDefinition;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x0009C721 File Offset: 0x0009A921
		public static FontStyle unityFontStyleAndWeight
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0009C737 File Offset: 0x0009A937
		public static OverflowClipBox unityOverflowClipBox
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityOverflowClipBox;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x0009C74D File Offset: 0x0009A94D
		public static Length unityParagraphSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityParagraphSpacing;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x0009C763 File Offset: 0x0009A963
		public static int unitySliceBottom
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceBottom;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x0009C779 File Offset: 0x0009A979
		public static int unitySliceLeft
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceLeft;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x0009C78F File Offset: 0x0009A98F
		public static int unitySliceRight
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceRight;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x0009C7A5 File Offset: 0x0009A9A5
		public static float unitySliceScale
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceScale;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x0009C7BB File Offset: 0x0009A9BB
		public static int unitySliceTop
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unitySliceTop;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x0009C7D1 File Offset: 0x0009A9D1
		public static TextAnchor unityTextAlign
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextAlign;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x0009C7E7 File Offset: 0x0009A9E7
		public static TextGeneratorType unityTextGenerator
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextGenerator;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x0009C7FD File Offset: 0x0009A9FD
		public static Color unityTextOutlineColor
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextOutlineColor;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x0009C813 File Offset: 0x0009AA13
		public static float unityTextOutlineWidth
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().unityTextOutlineWidth;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x0009C829 File Offset: 0x0009AA29
		public static TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return InitialStyle.s_InitialStyle.rareData.Read().unityTextOverflowPosition;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x0009C83F File Offset: 0x0009AA3F
		public static Visibility visibility
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().visibility;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x0009C855 File Offset: 0x0009AA55
		public static WhiteSpace whiteSpace
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().whiteSpace;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x0009C86B File Offset: 0x0009AA6B
		public static Length width
		{
			get
			{
				return InitialStyle.s_InitialStyle.layoutData.Read().width;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x0009C881 File Offset: 0x0009AA81
		public static Length wordSpacing
		{
			get
			{
				return InitialStyle.s_InitialStyle.inheritedData.Read().wordSpacing;
			}
		}

		// Token: 0x04001454 RID: 5204
		private static ComputedStyle s_InitialStyle = ComputedStyle.CreateInitial();
	}
}
