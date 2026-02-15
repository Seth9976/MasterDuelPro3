using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.Layout;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000345 RID: 837
	public class VisualElement : Focusable, IResolvedStyle, IStylePropertyAnimations, ITransform, ITransitionAnimations, IExperimentalFeatures, IVisualElementScheduler
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x000605FA File Offset: 0x0005E7FA
		Align IResolvedStyle.alignContent
		{
			get
			{
				return this.resolvedStyle.alignContent;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00060607 File Offset: 0x0005E807
		Align IResolvedStyle.alignItems
		{
			get
			{
				return this.resolvedStyle.alignItems;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00060614 File Offset: 0x0005E814
		Align IResolvedStyle.alignSelf
		{
			get
			{
				return this.resolvedStyle.alignSelf;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x00060621 File Offset: 0x0005E821
		Color IResolvedStyle.backgroundColor
		{
			get
			{
				return this.resolvedStyle.backgroundColor;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0006062E File Offset: 0x0005E82E
		Background IResolvedStyle.backgroundImage
		{
			get
			{
				return this.resolvedStyle.backgroundImage;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0006063B File Offset: 0x0005E83B
		BackgroundPosition IResolvedStyle.backgroundPositionX
		{
			get
			{
				return this.resolvedStyle.backgroundPositionX;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00060648 File Offset: 0x0005E848
		BackgroundPosition IResolvedStyle.backgroundPositionY
		{
			get
			{
				return this.resolvedStyle.backgroundPositionY;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00060655 File Offset: 0x0005E855
		BackgroundRepeat IResolvedStyle.backgroundRepeat
		{
			get
			{
				return this.resolvedStyle.backgroundRepeat;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00060662 File Offset: 0x0005E862
		BackgroundSize IResolvedStyle.backgroundSize
		{
			get
			{
				return this.resolvedStyle.backgroundSize;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0006066F File Offset: 0x0005E86F
		Color IResolvedStyle.borderBottomColor
		{
			get
			{
				return this.resolvedStyle.borderBottomColor;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0006067C File Offset: 0x0005E87C
		float IResolvedStyle.borderBottomLeftRadius
		{
			get
			{
				return this.resolvedStyle.borderBottomLeftRadius;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00060689 File Offset: 0x0005E889
		float IResolvedStyle.borderBottomRightRadius
		{
			get
			{
				return this.resolvedStyle.borderBottomRightRadius;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x00060696 File Offset: 0x0005E896
		float IResolvedStyle.borderBottomWidth
		{
			get
			{
				return this.resolvedStyle.borderBottomWidth;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x000606A3 File Offset: 0x0005E8A3
		Color IResolvedStyle.borderLeftColor
		{
			get
			{
				return this.resolvedStyle.borderLeftColor;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x000606B0 File Offset: 0x0005E8B0
		float IResolvedStyle.borderLeftWidth
		{
			get
			{
				return this.resolvedStyle.borderLeftWidth;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x000606BD File Offset: 0x0005E8BD
		Color IResolvedStyle.borderRightColor
		{
			get
			{
				return this.resolvedStyle.borderRightColor;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x000606CA File Offset: 0x0005E8CA
		float IResolvedStyle.borderRightWidth
		{
			get
			{
				return this.resolvedStyle.borderRightWidth;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000606D7 File Offset: 0x0005E8D7
		Color IResolvedStyle.borderTopColor
		{
			get
			{
				return this.resolvedStyle.borderTopColor;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x000606E4 File Offset: 0x0005E8E4
		float IResolvedStyle.borderTopLeftRadius
		{
			get
			{
				return this.resolvedStyle.borderTopLeftRadius;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x000606F1 File Offset: 0x0005E8F1
		float IResolvedStyle.borderTopRightRadius
		{
			get
			{
				return this.resolvedStyle.borderTopRightRadius;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x000606FE File Offset: 0x0005E8FE
		float IResolvedStyle.borderTopWidth
		{
			get
			{
				return this.resolvedStyle.borderTopWidth;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0006070B File Offset: 0x0005E90B
		float IResolvedStyle.bottom
		{
			get
			{
				return this.resolvedStyle.bottom;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00060718 File Offset: 0x0005E918
		Color IResolvedStyle.color
		{
			get
			{
				return this.resolvedStyle.color;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x00060725 File Offset: 0x0005E925
		DisplayStyle IResolvedStyle.display
		{
			get
			{
				return this.resolvedStyle.display;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00060732 File Offset: 0x0005E932
		StyleFloat IResolvedStyle.flexBasis
		{
			get
			{
				return this.resolvedStyle.flexBasis;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0006073F File Offset: 0x0005E93F
		FlexDirection IResolvedStyle.flexDirection
		{
			get
			{
				return this.resolvedStyle.flexDirection;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0006074C File Offset: 0x0005E94C
		float IResolvedStyle.flexGrow
		{
			get
			{
				return this.resolvedStyle.flexGrow;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x00060759 File Offset: 0x0005E959
		float IResolvedStyle.flexShrink
		{
			get
			{
				return this.resolvedStyle.flexShrink;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x00060766 File Offset: 0x0005E966
		Wrap IResolvedStyle.flexWrap
		{
			get
			{
				return this.resolvedStyle.flexWrap;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x00060773 File Offset: 0x0005E973
		float IResolvedStyle.fontSize
		{
			get
			{
				return this.resolvedStyle.fontSize;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x00060780 File Offset: 0x0005E980
		float IResolvedStyle.height
		{
			get
			{
				return this.resolvedStyle.height;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0006078D File Offset: 0x0005E98D
		Justify IResolvedStyle.justifyContent
		{
			get
			{
				return this.resolvedStyle.justifyContent;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0006079A File Offset: 0x0005E99A
		float IResolvedStyle.left
		{
			get
			{
				return this.resolvedStyle.left;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x000607A7 File Offset: 0x0005E9A7
		float IResolvedStyle.letterSpacing
		{
			get
			{
				return this.resolvedStyle.letterSpacing;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x000607B4 File Offset: 0x0005E9B4
		float IResolvedStyle.marginBottom
		{
			get
			{
				return this.resolvedStyle.marginBottom;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x000607C1 File Offset: 0x0005E9C1
		float IResolvedStyle.marginLeft
		{
			get
			{
				return this.resolvedStyle.marginLeft;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x000607CE File Offset: 0x0005E9CE
		float IResolvedStyle.marginRight
		{
			get
			{
				return this.resolvedStyle.marginRight;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x000607DB File Offset: 0x0005E9DB
		float IResolvedStyle.marginTop
		{
			get
			{
				return this.resolvedStyle.marginTop;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x000607E8 File Offset: 0x0005E9E8
		StyleFloat IResolvedStyle.maxHeight
		{
			get
			{
				return this.resolvedStyle.maxHeight;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x000607F5 File Offset: 0x0005E9F5
		StyleFloat IResolvedStyle.maxWidth
		{
			get
			{
				return this.resolvedStyle.maxWidth;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x00060802 File Offset: 0x0005EA02
		StyleFloat IResolvedStyle.minHeight
		{
			get
			{
				return this.resolvedStyle.minHeight;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0006080F File Offset: 0x0005EA0F
		StyleFloat IResolvedStyle.minWidth
		{
			get
			{
				return this.resolvedStyle.minWidth;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0006081C File Offset: 0x0005EA1C
		float IResolvedStyle.opacity
		{
			get
			{
				return this.resolvedStyle.opacity;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x00060829 File Offset: 0x0005EA29
		float IResolvedStyle.paddingBottom
		{
			get
			{
				return this.resolvedStyle.paddingBottom;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x00060836 File Offset: 0x0005EA36
		float IResolvedStyle.paddingLeft
		{
			get
			{
				return this.resolvedStyle.paddingLeft;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00060843 File Offset: 0x0005EA43
		float IResolvedStyle.paddingRight
		{
			get
			{
				return this.resolvedStyle.paddingRight;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00060850 File Offset: 0x0005EA50
		float IResolvedStyle.paddingTop
		{
			get
			{
				return this.resolvedStyle.paddingTop;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0006085D File Offset: 0x0005EA5D
		Position IResolvedStyle.position
		{
			get
			{
				return this.resolvedStyle.position;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0006086A File Offset: 0x0005EA6A
		float IResolvedStyle.right
		{
			get
			{
				return this.resolvedStyle.right;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00060877 File Offset: 0x0005EA77
		Rotate IResolvedStyle.rotate
		{
			get
			{
				return this.resolvedStyle.rotate;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x00060884 File Offset: 0x0005EA84
		Scale IResolvedStyle.scale
		{
			get
			{
				return this.resolvedStyle.scale;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00060891 File Offset: 0x0005EA91
		TextOverflow IResolvedStyle.textOverflow
		{
			get
			{
				return this.resolvedStyle.textOverflow;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0006089E File Offset: 0x0005EA9E
		float IResolvedStyle.top
		{
			get
			{
				return this.resolvedStyle.top;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x000608AB File Offset: 0x0005EAAB
		Vector3 IResolvedStyle.transformOrigin
		{
			get
			{
				return this.resolvedStyle.transformOrigin;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x000608B8 File Offset: 0x0005EAB8
		IEnumerable<TimeValue> IResolvedStyle.transitionDelay
		{
			get
			{
				return this.resolvedStyle.transitionDelay;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x000608C5 File Offset: 0x0005EAC5
		IEnumerable<TimeValue> IResolvedStyle.transitionDuration
		{
			get
			{
				return this.resolvedStyle.transitionDuration;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x000608D2 File Offset: 0x0005EAD2
		IEnumerable<StylePropertyName> IResolvedStyle.transitionProperty
		{
			get
			{
				return this.resolvedStyle.transitionProperty;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x000608DF File Offset: 0x0005EADF
		IEnumerable<EasingFunction> IResolvedStyle.transitionTimingFunction
		{
			get
			{
				return this.resolvedStyle.transitionTimingFunction;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000608EC File Offset: 0x0005EAEC
		Vector3 IResolvedStyle.translate
		{
			get
			{
				return this.resolvedStyle.translate;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000608F9 File Offset: 0x0005EAF9
		Color IResolvedStyle.unityBackgroundImageTintColor
		{
			get
			{
				return this.resolvedStyle.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x00060906 File Offset: 0x0005EB06
		EditorTextRenderingMode IResolvedStyle.unityEditorTextRenderingMode
		{
			get
			{
				return this.resolvedStyle.unityEditorTextRenderingMode;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00060913 File Offset: 0x0005EB13
		Font IResolvedStyle.unityFont
		{
			get
			{
				return this.resolvedStyle.unityFont;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00060920 File Offset: 0x0005EB20
		FontDefinition IResolvedStyle.unityFontDefinition
		{
			get
			{
				return this.resolvedStyle.unityFontDefinition;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0006092D File Offset: 0x0005EB2D
		FontStyle IResolvedStyle.unityFontStyleAndWeight
		{
			get
			{
				return this.resolvedStyle.unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0006093A File Offset: 0x0005EB3A
		float IResolvedStyle.unityParagraphSpacing
		{
			get
			{
				return this.resolvedStyle.unityParagraphSpacing;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00060947 File Offset: 0x0005EB47
		int IResolvedStyle.unitySliceBottom
		{
			get
			{
				return this.resolvedStyle.unitySliceBottom;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00060954 File Offset: 0x0005EB54
		int IResolvedStyle.unitySliceLeft
		{
			get
			{
				return this.resolvedStyle.unitySliceLeft;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00060961 File Offset: 0x0005EB61
		int IResolvedStyle.unitySliceRight
		{
			get
			{
				return this.resolvedStyle.unitySliceRight;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0006096E File Offset: 0x0005EB6E
		float IResolvedStyle.unitySliceScale
		{
			get
			{
				return this.resolvedStyle.unitySliceScale;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0006097B File Offset: 0x0005EB7B
		int IResolvedStyle.unitySliceTop
		{
			get
			{
				return this.resolvedStyle.unitySliceTop;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00060988 File Offset: 0x0005EB88
		TextAnchor IResolvedStyle.unityTextAlign
		{
			get
			{
				return this.resolvedStyle.unityTextAlign;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00060995 File Offset: 0x0005EB95
		TextGeneratorType IResolvedStyle.unityTextGenerator
		{
			get
			{
				return this.resolvedStyle.unityTextGenerator;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x000609A2 File Offset: 0x0005EBA2
		Color IResolvedStyle.unityTextOutlineColor
		{
			get
			{
				return this.resolvedStyle.unityTextOutlineColor;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x000609AF File Offset: 0x0005EBAF
		float IResolvedStyle.unityTextOutlineWidth
		{
			get
			{
				return this.resolvedStyle.unityTextOutlineWidth;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x000609BC File Offset: 0x0005EBBC
		TextOverflowPosition IResolvedStyle.unityTextOverflowPosition
		{
			get
			{
				return this.resolvedStyle.unityTextOverflowPosition;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x000609C9 File Offset: 0x0005EBC9
		Visibility IResolvedStyle.visibility
		{
			get
			{
				return this.resolvedStyle.visibility;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000609D6 File Offset: 0x0005EBD6
		WhiteSpace IResolvedStyle.whiteSpace
		{
			get
			{
				return this.resolvedStyle.whiteSpace;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x000609E3 File Offset: 0x0005EBE3
		float IResolvedStyle.width
		{
			get
			{
				return this.resolvedStyle.width;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000609F0 File Offset: 0x0005EBF0
		float IResolvedStyle.wordSpacing
		{
			get
			{
				return this.resolvedStyle.wordSpacing;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x000609FD File Offset: 0x0005EBFD
		internal bool hasRunningAnimations
		{
			get
			{
				return this.styleAnimation.runningAnimationCount > 0;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00060A0D File Offset: 0x0005EC0D
		internal bool hasCompletedAnimations
		{
			get
			{
				return this.styleAnimation.completedAnimationCount > 0;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00060A1D File Offset: 0x0005EC1D
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x00060A25 File Offset: 0x0005EC25
		int IStylePropertyAnimations.runningAnimationCount { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00060A2E File Offset: 0x0005EC2E
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x00060A36 File Offset: 0x0005EC36
		int IStylePropertyAnimations.completedAnimationCount { get; set; }

		// Token: 0x0600189D RID: 6301 RVA: 0x00060A40 File Offset: 0x0005EC40
		private IStylePropertyAnimationSystem GetStylePropertyAnimationSystem()
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			return (elementPanel != null) ? elementPanel.styleAnimationSystem : null;
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00038529 File Offset: 0x00036729
		internal IStylePropertyAnimations styleAnimation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00060A64 File Offset: 0x0005EC64
		bool IStylePropertyAnimations.Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00060A8C File Offset: 0x0005EC8C
		bool IStylePropertyAnimations.Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00060AB4 File Offset: 0x0005ECB4
		bool IStylePropertyAnimations.Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00060ADC File Offset: 0x0005ECDC
		bool IStylePropertyAnimations.Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00060B04 File Offset: 0x0005ED04
		bool IStylePropertyAnimations.StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00060B2C File Offset: 0x0005ED2C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00060B54 File Offset: 0x0005ED54
		bool IStylePropertyAnimations.Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00060B7C File Offset: 0x0005ED7C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00060BA4 File Offset: 0x0005EDA4
		bool IStylePropertyAnimations.Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00060BCC File Offset: 0x0005EDCC
		bool IStylePropertyAnimations.Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00060BF4 File Offset: 0x0005EDF4
		bool IStylePropertyAnimations.Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00060C1C File Offset: 0x0005EE1C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00060C44 File Offset: 0x0005EE44
		bool IStylePropertyAnimations.Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00060C6C File Offset: 0x0005EE6C
		bool IStylePropertyAnimations.Start(StylePropertyId id, BackgroundPosition from, BackgroundPosition to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00060C94 File Offset: 0x0005EE94
		bool IStylePropertyAnimations.Start(StylePropertyId id, BackgroundRepeat from, BackgroundRepeat to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00060CBC File Offset: 0x0005EEBC
		bool IStylePropertyAnimations.Start(StylePropertyId id, BackgroundSize from, BackgroundSize to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00060CE3 File Offset: 0x0005EEE3
		void IStylePropertyAnimations.CancelAnimation(StylePropertyId id)
		{
			IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
			if (stylePropertyAnimationSystem != null)
			{
				stylePropertyAnimationSystem.CancelAnimation(this, id);
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00060CFC File Offset: 0x0005EEFC
		void IStylePropertyAnimations.CancelAllAnimations()
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
				if (stylePropertyAnimationSystem != null)
				{
					stylePropertyAnimationSystem.CancelAllAnimations(this);
				}
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00060D32 File Offset: 0x0005EF32
		void IStylePropertyAnimations.UpdateAnimation(StylePropertyId id)
		{
			this.GetStylePropertyAnimationSystem().UpdateAnimation(this, id);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00060D44 File Offset: 0x0005EF44
		void IStylePropertyAnimations.GetAllAnimations(List<StylePropertyId> outPropertyIds)
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				this.GetStylePropertyAnimationSystem().GetAllAnimations(this, outPropertyIds);
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00060D78 File Offset: 0x0005EF78
		internal bool TryConvertLengthUnits(StylePropertyId id, ref Length from, ref Length to, int subPropertyIndex = 0)
		{
			bool flag = from.IsAuto() || from.IsNone() || to.IsAuto() || to.IsNone();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = float.IsNaN(from.value) || float.IsNaN(to.value);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = from.unit == to.unit;
					if (flag4)
					{
						flag2 = true;
					}
					else
					{
						bool flag5 = to.unit == LengthUnit.Pixel;
						if (flag5)
						{
							bool flag6 = Mathf.Approximately(from.value, 0f);
							if (flag6)
							{
								from = new Length(0f, LengthUnit.Pixel);
								return true;
							}
							float? parentSize = this.GetParentSizeForLengthConversion(id, subPropertyIndex);
							bool flag7 = parentSize == null || parentSize.Value < 0f;
							if (flag7)
							{
								return false;
							}
							from = new Length(from.value * parentSize.Value / 100f, LengthUnit.Pixel);
						}
						else
						{
							Assert.AreEqual<LengthUnit>(LengthUnit.Percent, to.unit);
							float? parentSize2 = this.GetParentSizeForLengthConversion(id, subPropertyIndex);
							bool flag8 = parentSize2 == null || parentSize2.Value <= 0f;
							if (flag8)
							{
								return false;
							}
							from = new Length(from.value * 100f / parentSize2.Value, LengthUnit.Percent);
						}
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00060EF0 File Offset: 0x0005F0F0
		internal bool TryConvertTransformOriginUnits(ref TransformOrigin from, ref TransformOrigin to)
		{
			Length fromX = from.x;
			Length fromY = from.y;
			Length toX = to.x;
			Length toY = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.TransformOrigin, ref fromX, ref toX, 0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.TryConvertLengthUnits(StylePropertyId.TransformOrigin, ref fromY, ref toY, 1);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					from.x = fromX;
					from.y = fromY;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00060F70 File Offset: 0x0005F170
		internal bool TryConvertTranslateUnits(ref Translate from, ref Translate to)
		{
			Length fromX = from.x;
			Length fromY = from.y;
			Length toX = to.x;
			Length toY = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.Translate, ref fromX, ref toX, 0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.TryConvertLengthUnits(StylePropertyId.Translate, ref fromY, ref toY, 1);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					from.x = fromX;
					from.y = fromY;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00060FF0 File Offset: 0x0005F1F0
		internal bool TryConvertBackgroundSizeUnits(ref BackgroundSize from, ref BackgroundSize to)
		{
			Length fromX = from.x;
			Length fromY = from.y;
			Length toX = to.x;
			Length toY = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.BackgroundSize, ref fromX, ref toX, 0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.TryConvertLengthUnits(StylePropertyId.BackgroundSize, ref fromY, ref toY, 1);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					from.x = fromX;
					from.y = fromY;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00061070 File Offset: 0x0005F270
		private float? GetParentSizeForLengthConversion(StylePropertyId id, int subPropertyIndex = 0)
		{
			if (id <= StylePropertyId.WordSpacing)
			{
				if (id - StylePropertyId.FontSize <= 1 || id == StylePropertyId.UnityParagraphSpacing || id == StylePropertyId.WordSpacing)
				{
					return null;
				}
			}
			else if (id <= StylePropertyId.Translate)
			{
				switch (id)
				{
				case StylePropertyId.Bottom:
				case StylePropertyId.Height:
				case StylePropertyId.MaxHeight:
				case StylePropertyId.MinHeight:
				case StylePropertyId.Top:
				{
					VisualElement parent = this.hierarchy.parent;
					return (parent != null) ? new float?(parent.resolvedStyle.height) : null;
				}
				case StylePropertyId.Display:
				case StylePropertyId.FlexDirection:
				case StylePropertyId.FlexGrow:
				case StylePropertyId.FlexShrink:
				case StylePropertyId.FlexWrap:
				case StylePropertyId.JustifyContent:
				case StylePropertyId.Position:
					break;
				case StylePropertyId.FlexBasis:
				{
					bool flag = this.hierarchy.parent == null;
					if (flag)
					{
						return null;
					}
					FlexDirection flexDirection = this.hierarchy.parent.resolvedStyle.flexDirection;
					FlexDirection flexDirection2 = flexDirection;
					if (flexDirection2 > FlexDirection.ColumnReverse)
					{
						return new float?(this.hierarchy.parent.resolvedStyle.width);
					}
					return new float?(this.hierarchy.parent.resolvedStyle.height);
				}
				case StylePropertyId.Left:
				case StylePropertyId.MarginBottom:
				case StylePropertyId.MarginLeft:
				case StylePropertyId.MarginRight:
				case StylePropertyId.MarginTop:
				case StylePropertyId.MaxWidth:
				case StylePropertyId.MinWidth:
				case StylePropertyId.PaddingBottom:
				case StylePropertyId.PaddingLeft:
				case StylePropertyId.PaddingRight:
				case StylePropertyId.PaddingTop:
				case StylePropertyId.Right:
				case StylePropertyId.Width:
				{
					VisualElement parent2 = this.hierarchy.parent;
					return (parent2 != null) ? new float?(parent2.resolvedStyle.width) : null;
				}
				default:
					if (id - StylePropertyId.TransformOrigin <= 1)
					{
						return new float?((subPropertyIndex == 0) ? this.resolvedStyle.width : this.resolvedStyle.height);
					}
					break;
				}
			}
			else if (id - StylePropertyId.BorderBottomLeftRadius <= 1 || id - StylePropertyId.BorderTopLeftRadius <= 1)
			{
				return new float?(this.resolvedStyle.width);
			}
			return null;
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000612B2 File Offset: 0x0005F4B2
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x000612C7 File Offset: 0x0005F4C7
		internal bool isCompositeRoot
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.CompositeRoot) == VisualElementFlags.CompositeRoot;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.CompositeRoot) : (this.m_Flags & ~VisualElementFlags.CompositeRoot));
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x000612EC File Offset: 0x0005F4EC
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x00061304 File Offset: 0x0005F504
		internal bool areAncestorsAndSelfDisplayed
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.HierarchyDisplayed) == VisualElementFlags.HierarchyDisplayed;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.HierarchyDisplayed) : (this.m_Flags & ~VisualElementFlags.HierarchyDisplayed));
				bool flag = value && (this.renderChainData.pendingRepaint || this.renderChainData.pendingHierarchicalRepaint);
				if (flag)
				{
					this.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00061366 File Offset: 0x0005F566
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x00061370 File Offset: 0x0005F570
		[CreateProperty]
		public string viewDataKey
		{
			get
			{
				return this.m_ViewDataKey;
			}
			set
			{
				bool flag = this.m_ViewDataKey != value;
				if (flag)
				{
					this.m_ViewDataKey = value;
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.ViewData);
					}
					base.NotifyPropertyChanged(in VisualElement.viewDataKeyProperty);
				}
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x000613B8 File Offset: 0x0005F5B8
		internal bool enableViewDataPersistence
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.EnableViewDataPersistence) == VisualElementFlags.EnableViewDataPersistence;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x000613D0 File Offset: 0x0005F5D0
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x00061408 File Offset: 0x0005F608
		[CreateProperty]
		public object userData
		{
			get
			{
				bool flag = this.m_PropertyBag != null;
				object obj;
				if (flag)
				{
					object value;
					this.m_PropertyBag.TryGetValue(VisualElement.userDataPropertyKey, out value);
					obj = value;
				}
				else
				{
					obj = null;
				}
				return obj;
			}
			set
			{
				object previous = this.userData;
				this.SetPropertyInternal(VisualElement.userDataPropertyKey, value);
				bool flag = previous != this.userData;
				if (flag)
				{
					base.NotifyPropertyChanged(in VisualElement.userDataProperty);
				}
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00061448 File Offset: 0x0005F648
		public override bool canGrabFocus
		{
			get
			{
				bool focusDisabledOnComposite = false;
				for (VisualElement p = this.hierarchy.parent; p != null; p = p.parent)
				{
					bool isCompositeRoot = p.isCompositeRoot;
					if (isCompositeRoot)
					{
						focusDisabledOnComposite |= !p.canGrabFocus;
						break;
					}
				}
				return !focusDisabledOnComposite && this.visible && this.resolvedStyle.display != DisplayStyle.None && this.enabledInHierarchy && base.canGrabFocus;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000614C8 File Offset: 0x0005F6C8
		public override FocusController focusController
		{
			get
			{
				IPanel panel = this.panel;
				return (panel != null) ? panel.focusController : null;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x000614EC File Offset: 0x0005F6EC
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x000020EA File Offset: 0x000002EA
		[CreateProperty]
		public bool disablePlayModeTint
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000614FF File Offset: 0x0005F6FF
		internal Color playModeTintColor
		{
			get
			{
				return this.disablePlayModeTint ? Color.white : UIElementsUtility.editorPlayModeTintColor;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00061518 File Offset: 0x0005F718
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x00061568 File Offset: 0x0005F768
		[CreateProperty]
		public UsageHints usageHints
		{
			get
			{
				return (((this.renderHints & RenderHints.GroupTransform) != RenderHints.None) ? UsageHints.GroupTransform : UsageHints.None) | (((this.renderHints & RenderHints.BoneTransform) != RenderHints.None) ? UsageHints.DynamicTransform : UsageHints.None) | (((this.renderHints & RenderHints.MaskContainer) != RenderHints.None) ? UsageHints.MaskContainer : UsageHints.None) | (((this.renderHints & RenderHints.DynamicColor) != RenderHints.None) ? UsageHints.DynamicColor : UsageHints.None);
			}
			set
			{
				bool flag = (value & UsageHints.GroupTransform) > UsageHints.None;
				if (flag)
				{
					this.renderHints |= RenderHints.GroupTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.GroupTransform;
				}
				bool flag2 = (value & UsageHints.DynamicTransform) > UsageHints.None;
				if (flag2)
				{
					this.renderHints |= RenderHints.BoneTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.BoneTransform;
				}
				bool flag3 = (value & UsageHints.MaskContainer) > UsageHints.None;
				if (flag3)
				{
					this.renderHints |= RenderHints.MaskContainer;
				}
				else
				{
					this.renderHints &= ~RenderHints.MaskContainer;
				}
				bool flag4 = (value & UsageHints.DynamicColor) > UsageHints.None;
				if (flag4)
				{
					this.renderHints |= RenderHints.DynamicColor;
				}
				else
				{
					this.renderHints &= ~RenderHints.DynamicColor;
				}
				base.NotifyPropertyChanged(in VisualElement.usageHintsProperty);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00061630 File Offset: 0x0005F830
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x00061648 File Offset: 0x0005F848
		internal RenderHints renderHints
		{
			get
			{
				return this.m_RenderHints;
			}
			set
			{
				RenderHints oldHints = this.m_RenderHints & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
				RenderHints newHints = value & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
				RenderHints changedHints = oldHints ^ newHints;
				bool flag = changedHints > RenderHints.None;
				if (flag)
				{
					RenderHints oldDirty = this.m_RenderHints & RenderHints.DirtyAll;
					RenderHints addDirty = changedHints << 5;
					this.m_RenderHints = newHints | oldDirty | addDirty;
					this.IncrementVersion(VersionChangeType.RenderHints);
				}
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000616A5 File Offset: 0x0005F8A5
		internal void MarkRenderHintsClean()
		{
			this.m_RenderHints &= ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x000616BC File Offset: 0x0005F8BC
		public ITransform transform
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x000616D0 File Offset: 0x0005F8D0
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x000616ED File Offset: 0x0005F8ED
		Vector3 ITransform.position
		{
			get
			{
				return this.resolvedStyle.translate;
			}
			set
			{
				this.style.translate = new Translate(value.x, value.y, value.z);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00061724 File Offset: 0x0005F924
		Vector3 ITransform.scale
		{
			get
			{
				Vector3 s = this.resolvedStyle.scale.value;
				BaseVisualElementPanel elementPanel = this.elementPanel;
				bool flag = elementPanel != null && elementPanel.isFlat;
				if (flag)
				{
					s.z = 1f;
				}
				return s;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00061770 File Offset: 0x0005F970
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x0006177F File Offset: 0x0005F97F
		internal bool isLayoutManual
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.LayoutManual) == VisualElementFlags.LayoutManual;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.LayoutManual) : (this.m_Flags & ~VisualElementFlags.LayoutManual));
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x000617A0 File Offset: 0x0005F9A0
		public float scaledPixelsPerPoint
		{
			get
			{
				bool flag = this.elementPanel == null;
				float num;
				if (flag)
				{
					Debug.LogWarning("Tying to acces dpi setting of a visual element not on a panel");
					num = GUIUtility.pixelsPerPoint;
				}
				else
				{
					num = this.elementPanel.scaledPixelsPerPoint;
				}
				return num;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x000617DE File Offset: 0x0005F9DE
		[Obsolete("scaledPixelsPerPoint_noChecks is deprecated. Use scaledPixelsPerPoint instead.")]
		internal float scaledPixelsPerPoint_noChecks
		{
			get
			{
				BaseVisualElementPanel elementPanel = this.elementPanel;
				return (elementPanel != null) ? elementPanel.scaledPixelsPerPoint : GUIUtility.pixelsPerPoint;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x000617F8 File Offset: 0x0005F9F8
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x00061880 File Offset: 0x0005FA80
		[CreateProperty(ReadOnly = true)]
		public Rect layout
		{
			get
			{
				Rect result = this.m_Layout;
				bool flag = !this.layoutNode.IsUndefined && !this.isLayoutManual;
				if (flag)
				{
					result.x = this.layoutNode.LayoutX;
					result.y = this.layoutNode.LayoutY;
					result.width = this.layoutNode.LayoutWidth;
					result.height = this.layoutNode.LayoutHeight;
				}
				return result;
			}
			internal set
			{
				bool flag = this.isLayoutManual && this.m_Layout == value;
				if (!flag)
				{
					Rect lastLayout = this.layout;
					VersionChangeType changeType = (VersionChangeType)0;
					bool flag2 = !Mathf.Approximately(lastLayout.x, value.x) || !Mathf.Approximately(lastLayout.y, value.y);
					if (flag2)
					{
						changeType |= VersionChangeType.Transform;
					}
					bool flag3 = !Mathf.Approximately(lastLayout.width, value.width) || !Mathf.Approximately(lastLayout.height, value.height);
					if (flag3)
					{
						changeType |= VersionChangeType.Size;
					}
					this.m_Layout = value;
					this.isLayoutManual = true;
					IStyle styleAccess = this.style;
					styleAccess.position = Position.Absolute;
					styleAccess.marginLeft = 0f;
					styleAccess.marginRight = 0f;
					styleAccess.marginBottom = 0f;
					styleAccess.marginTop = 0f;
					styleAccess.left = value.x;
					styleAccess.top = value.y;
					styleAccess.right = float.NaN;
					styleAccess.bottom = float.NaN;
					styleAccess.width = value.width;
					styleAccess.height = value.height;
					bool flag4 = changeType > (VersionChangeType)0;
					if (flag4)
					{
						this.IncrementVersion(changeType);
					}
				}
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00061A18 File Offset: 0x0005FC18
		internal void ClearManualLayout()
		{
			this.isLayoutManual = false;
			IStyle styleAccess = this.style;
			styleAccess.position = StyleKeyword.Null;
			styleAccess.marginLeft = StyleKeyword.Null;
			styleAccess.marginRight = StyleKeyword.Null;
			styleAccess.marginBottom = StyleKeyword.Null;
			styleAccess.marginTop = StyleKeyword.Null;
			styleAccess.left = StyleKeyword.Null;
			styleAccess.top = StyleKeyword.Null;
			styleAccess.right = StyleKeyword.Null;
			styleAccess.bottom = StyleKeyword.Null;
			styleAccess.width = StyleKeyword.Null;
			styleAccess.height = StyleKeyword.Null;
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00061AC4 File Offset: 0x0005FCC4
		[CreateProperty(ReadOnly = true)]
		public Rect contentRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.paddingLeft, this.resolvedStyle.paddingTop, this.resolvedStyle.paddingRight, this.resolvedStyle.paddingBottom);
				return this.paddingRect - spacing;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00061B18 File Offset: 0x0005FD18
		protected Rect paddingRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.borderLeftWidth, this.resolvedStyle.borderTopWidth, this.resolvedStyle.borderRightWidth, this.resolvedStyle.borderBottomWidth);
				return this.rect - spacing;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00061B69 File Offset: 0x0005FD69
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x00061B76 File Offset: 0x0005FD76
		internal bool isBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.BoundingBoxDirty) == VisualElementFlags.BoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.BoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.BoundingBoxDirty));
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x00061B94 File Offset: 0x0005FD94
		internal bool isWorldBoundingBoxDirty
		{
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldBoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.WorldBoundingBoxDirty));
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00061BB3 File Offset: 0x0005FDB3
		internal bool isWorldBoundingBoxOrDependenciesDirty
		{
			get
			{
				return (this.m_Flags & (VisualElementFlags.WorldTransformDirty | VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty)) > (VisualElementFlags)0;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x00061BC4 File Offset: 0x0005FDC4
		internal Rect boundingBox
		{
			get
			{
				bool isBoundingBoxDirty = this.isBoundingBoxDirty;
				if (isBoundingBoxDirty)
				{
					this.UpdateBoundingBox();
					this.isBoundingBoxDirty = false;
				}
				return this.m_BoundingBox;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00061BF8 File Offset: 0x0005FDF8
		internal Rect worldBoundingBox
		{
			get
			{
				bool isWorldBoundingBoxOrDependenciesDirty = this.isWorldBoundingBoxOrDependenciesDirty;
				if (isWorldBoundingBoxOrDependenciesDirty)
				{
					this.UpdateWorldBoundingBox();
					this.isWorldBoundingBoxDirty = false;
				}
				return this.m_WorldBoundingBox;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00061C2C File Offset: 0x0005FE2C
		private Rect boundingBoxInParentSpace
		{
			get
			{
				Rect bb = this.boundingBox;
				this.TransformAlignedRectToParentSpace(ref bb);
				return bb;
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00061C50 File Offset: 0x0005FE50
		internal void UpdateBoundingBox()
		{
			bool flag = float.IsNaN(this.rect.x) || float.IsNaN(this.rect.y) || float.IsNaN(this.rect.width) || float.IsNaN(this.rect.height);
			if (flag)
			{
				this.m_BoundingBox = Rect.zero;
			}
			else
			{
				this.m_BoundingBox = this.rect;
				bool flag2 = !this.ShouldClip() && this.resolvedStyle.display == DisplayStyle.Flex;
				if (flag2)
				{
					int childCount = this.m_Children.Count;
					for (int i = 0; i < childCount; i++)
					{
						bool flag3 = !this.m_Children[i].areAncestorsAndSelfDisplayed;
						if (!flag3)
						{
							Rect childBB = this.m_Children[i].boundingBoxInParentSpace;
							this.m_BoundingBox.xMin = Math.Min(this.m_BoundingBox.xMin, childBB.xMin);
							this.m_BoundingBox.xMax = Math.Max(this.m_BoundingBox.xMax, childBB.xMax);
							this.m_BoundingBox.yMin = Math.Min(this.m_BoundingBox.yMin, childBB.yMin);
							this.m_BoundingBox.yMax = Math.Max(this.m_BoundingBox.yMax, childBB.yMax);
						}
					}
				}
			}
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00061DEB File Offset: 0x0005FFEB
		internal void UpdateWorldBoundingBox()
		{
			this.m_WorldBoundingBox = this.boundingBox;
			VisualElement.TransformAlignedRect(this.worldTransformRef, ref this.m_WorldBoundingBox);
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x00061E0C File Offset: 0x0006000C
		[CreateProperty(ReadOnly = true)]
		public Rect worldBound
		{
			get
			{
				Rect result = this.rect;
				VisualElement.TransformAlignedRect(this.worldTransformRef, ref result);
				return result;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00061E34 File Offset: 0x00060034
		[CreateProperty(ReadOnly = true)]
		public Rect localBound
		{
			get
			{
				Rect r = this.rect;
				this.TransformAlignedRectToParentSpace(ref r);
				return r;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00061E58 File Offset: 0x00060058
		internal Rect rect
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				Rect i = this.layout;
				return new Rect(0f, 0f, i.width, i.height);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00061E8E File Offset: 0x0006008E
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x00061E9B File Offset: 0x0006009B
		internal bool isWorldTransformDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformDirty) == VisualElementFlags.WorldTransformDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformDirty));
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (set) Token: 0x060018E6 RID: 6374 RVA: 0x00061EB9 File Offset: 0x000600B9
		internal bool isWorldTransformInverseDirty
		{
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformInverseDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformInverseDirty));
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x00061ED7 File Offset: 0x000600D7
		internal bool isWorldTransformInverseOrDependenciesDirty
		{
			get
			{
				return (this.m_Flags & (VisualElementFlags.WorldTransformDirty | VisualElementFlags.WorldTransformInverseDirty)) > (VisualElementFlags)0;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00061EE4 File Offset: 0x000600E4
		[CreateProperty(ReadOnly = true)]
		public Matrix4x4 worldTransform
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return this.m_WorldTransformCache;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x00061F10 File Offset: 0x00060110
		internal ref Matrix4x4 worldTransformRef
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return ref this.m_WorldTransformCache;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00061F3C File Offset: 0x0006013C
		internal ref Matrix4x4 worldTransformInverse
		{
			get
			{
				bool isWorldTransformInverseOrDependenciesDirty = this.isWorldTransformInverseOrDependenciesDirty;
				if (isWorldTransformInverseOrDependenciesDirty)
				{
					this.UpdateWorldTransformInverse();
				}
				return ref this.m_WorldTransformInverseCache;
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00061F68 File Offset: 0x00060168
		internal void UpdateWorldTransform()
		{
			bool flag = this.elementPanel != null && !this.elementPanel.duringLayoutPhase;
			if (flag)
			{
				this.isWorldTransformDirty = false;
			}
			bool flag2 = this.hierarchy.parent != null;
			if (flag2)
			{
				bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
				if (hasDefaultRotationAndScale)
				{
					VisualElement.TranslateMatrix34(this.hierarchy.parent.worldTransformRef, this.positionWithLayout, out this.m_WorldTransformCache);
				}
				else
				{
					Matrix4x4 mat;
					this.GetPivotedMatrixWithLayout(out mat);
					VisualElement.MultiplyMatrix34(this.hierarchy.parent.worldTransformRef, ref mat, out this.m_WorldTransformCache);
				}
			}
			else
			{
				this.GetPivotedMatrixWithLayout(out this.m_WorldTransformCache);
			}
			this.isWorldTransformInverseDirty = true;
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00062034 File Offset: 0x00060234
		internal void UpdateWorldTransformInverse()
		{
			Matrix4x4.Inverse3DAffine(this.worldTransform, ref this.m_WorldTransformInverseCache);
			this.isWorldTransformInverseDirty = false;
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x00062051 File Offset: 0x00060251
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x0006205E File Offset: 0x0006025E
		internal bool isWorldClipDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldClipDirty) == VisualElementFlags.WorldClipDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldClipDirty) : (this.m_Flags & ~VisualElementFlags.WorldClipDirty));
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0006207C File Offset: 0x0006027C
		internal Rect worldClip
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClip;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000620B0 File Offset: 0x000602B0
		internal Rect worldClipMinusGroup
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipMinusGroup;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000620E4 File Offset: 0x000602E4
		internal bool worldClipIsInfinite
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipIsInfinite;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00062118 File Offset: 0x00060318
		internal void EnsureWorldTransformAndClipUpToDate()
		{
			bool isWorldTransformDirty = this.isWorldTransformDirty;
			if (isWorldTransformDirty)
			{
				this.UpdateWorldTransform();
			}
			bool isWorldClipDirty = this.isWorldClipDirty;
			if (isWorldClipDirty)
			{
				this.UpdateWorldClip();
				this.isWorldClipDirty = false;
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00062154 File Offset: 0x00060354
		private void UpdateWorldClip()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.m_WorldClip = this.hierarchy.parent.worldClip;
				bool parentWorldClipIsInfinite = this.hierarchy.parent.worldClipIsInfinite;
				bool flag2 = this.hierarchy.parent != this.renderChainData.groupTransformAncestor;
				if (flag2)
				{
					this.m_WorldClipMinusGroup = this.hierarchy.parent.worldClipMinusGroup;
				}
				else
				{
					parentWorldClipIsInfinite = true;
					this.m_WorldClipMinusGroup = VisualElement.s_InfiniteRect;
				}
				bool flag3 = this.ShouldClip();
				if (flag3)
				{
					Rect wb = this.SubstractBorderPadding(this.worldBound);
					this.m_WorldClip = this.CombineClipRects(wb, this.m_WorldClip);
					this.m_WorldClipMinusGroup = (parentWorldClipIsInfinite ? wb : this.CombineClipRects(wb, this.m_WorldClipMinusGroup));
					this.m_WorldClipIsInfinite = false;
				}
				else
				{
					this.m_WorldClipIsInfinite = parentWorldClipIsInfinite;
				}
			}
			else
			{
				this.m_WorldClipMinusGroup = (this.m_WorldClip = ((this.panel != null) ? this.panel.visualTree.rect : VisualElement.s_InfiniteRect));
				this.m_WorldClipIsInfinite = true;
			}
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00062290 File Offset: 0x00060490
		private Rect CombineClipRects(Rect rect, Rect parentRect)
		{
			float x = Mathf.Max(rect.xMin, parentRect.xMin);
			float x2 = Mathf.Min(rect.xMax, parentRect.xMax);
			float y = Mathf.Max(rect.yMin, parentRect.yMin);
			float y2 = Mathf.Min(rect.yMax, parentRect.yMax);
			float width = Mathf.Max(x2 - x, 0f);
			float height = Mathf.Max(y2 - y, 0f);
			return new Rect(x, y, width, height);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00062320 File Offset: 0x00060520
		private Rect SubstractBorderPadding(Rect worldRect)
		{
			float xScale = this.worldTransform.m00;
			float yScale = this.worldTransform.m11;
			worldRect.x += this.resolvedStyle.borderLeftWidth * xScale;
			worldRect.y += this.resolvedStyle.borderTopWidth * yScale;
			worldRect.width -= (this.resolvedStyle.borderLeftWidth + this.resolvedStyle.borderRightWidth) * xScale;
			worldRect.height -= (this.resolvedStyle.borderTopWidth + this.resolvedStyle.borderBottomWidth) * yScale;
			bool flag = this.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
			if (flag)
			{
				worldRect.x += this.resolvedStyle.paddingLeft * xScale;
				worldRect.y += this.resolvedStyle.paddingTop * yScale;
				worldRect.width -= (this.resolvedStyle.paddingLeft + this.resolvedStyle.paddingRight) * xScale;
				worldRect.height -= (this.resolvedStyle.paddingTop + this.resolvedStyle.paddingBottom) * yScale;
			}
			return worldRect;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00062474 File Offset: 0x00060674
		internal static Rect ComputeAAAlignedBound(Rect position, Matrix4x4 mat)
		{
			Rect p = position;
			Vector3 v0 = mat.MultiplyPoint3x4(new Vector3(p.x, p.y, 0f));
			Vector3 v = mat.MultiplyPoint3x4(new Vector3(p.x + p.width, p.y, 0f));
			Vector3 v2 = mat.MultiplyPoint3x4(new Vector3(p.x, p.y + p.height, 0f));
			Vector3 v3 = mat.MultiplyPoint3x4(new Vector3(p.x + p.width, p.y + p.height, 0f));
			return Rect.MinMaxRect(Mathf.Min(v0.x, Mathf.Min(v.x, Mathf.Min(v2.x, v3.x))), Mathf.Min(v0.y, Mathf.Min(v.y, Mathf.Min(v2.y, v3.y))), Mathf.Max(v0.x, Mathf.Max(v.x, Mathf.Max(v2.x, v3.x))), Mathf.Max(v0.y, Mathf.Max(v.y, Mathf.Max(v2.y, v3.y))));
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x000625D0 File Offset: 0x000607D0
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x000625E8 File Offset: 0x000607E8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal PseudoStates pseudoStates
		{
			get
			{
				return this.m_PseudoStates;
			}
			set
			{
				PseudoStates diff = this.m_PseudoStates ^ value;
				bool flag = diff > (PseudoStates)0;
				if (flag)
				{
					bool flag2 = (value & PseudoStates.Root) == PseudoStates.Root;
					if (flag2)
					{
						this.isRootVisualContainer = true;
					}
					bool flag3 = diff != PseudoStates.Root;
					if (flag3)
					{
						PseudoStates added = diff & value;
						PseudoStates removed = diff & this.m_PseudoStates;
						bool flag4 = (this.triggerPseudoMask & added) != (PseudoStates)0 || (this.dependencyPseudoMask & removed) > (PseudoStates)0;
						if (flag4)
						{
							this.IncrementVersion(VersionChangeType.StyleSheet);
						}
					}
					this.m_PseudoStates = value;
				}
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00062675 File Offset: 0x00060875
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0006267D File Offset: 0x0006087D
		internal int containedPointerIds { get; set; }

		// Token: 0x060018FB RID: 6395 RVA: 0x00062688 File Offset: 0x00060888
		internal void UpdateHoverPseudoState()
		{
			bool flag = this.containedPointerIds == 0 || this.panel == null;
			if (flag)
			{
				this.pseudoStates &= ~PseudoStates.Hover;
			}
			else
			{
				bool hovered = false;
				for (int pointerId = 0; pointerId < PointerId.maxPointers; pointerId++)
				{
					bool flag2 = (this.containedPointerIds & (1 << pointerId)) != 0;
					if (flag2)
					{
						IEventHandler capturingElement = this.panel.GetCapturingElement(pointerId);
						bool flag3 = VisualElement.IsPartOfCapturedChain(this, in capturingElement);
						if (flag3)
						{
							hovered = true;
							break;
						}
					}
				}
				bool flag4 = hovered;
				if (flag4)
				{
					this.pseudoStates |= PseudoStates.Hover;
				}
				else
				{
					this.pseudoStates &= ~PseudoStates.Hover;
				}
			}
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0006273C File Offset: 0x0006093C
		private static bool IsPartOfCapturedChain(VisualElement self, in IEventHandler capturingElement)
		{
			bool flag = self == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = capturingElement == null;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = capturingElement == self;
					flag2 = flag4 || self.Contains(capturingElement as VisualElement);
				}
			}
			return flag2;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00062784 File Offset: 0x00060984
		internal void UpdateHoverPseudoStateAfterCaptureChange(int pointerId)
		{
			for (VisualElement ve = this; ve != null; ve = ve.parent)
			{
				ve.UpdateHoverPseudoState();
			}
			BaseVisualElementPanel elementPanel = this.elementPanel;
			VisualElement elementUnderPointer = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(pointerId) : null);
			VisualElement ve2 = elementUnderPointer;
			while (ve2 != null && ve2 != this)
			{
				ve2.UpdateHoverPseudoState();
				ve2 = ve2.parent;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x000627E7 File Offset: 0x000609E7
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x000627F0 File Offset: 0x000609F0
		[CreateProperty]
		public PickingMode pickingMode
		{
			get
			{
				return this.m_PickingMode;
			}
			set
			{
				bool flag = this.m_PickingMode == value;
				if (!flag)
				{
					this.m_PickingMode = value;
					this.IncrementVersion(VersionChangeType.Picking);
					base.NotifyPropertyChanged(in VisualElement.pickingModeProperty);
				}
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0006282C File Offset: 0x00060A2C
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x00062844 File Offset: 0x00060A44
		[CreateProperty]
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.IncrementVersion(VersionChangeType.StyleSheet);
					base.NotifyPropertyChanged(in VisualElement.nameProperty);
				}
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x00062880 File Offset: 0x00060A80
		internal List<string> classList
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
				if (flag)
				{
					this.m_ClassList = ObjectListPool<string>.Get();
				}
				return this.m_ClassList;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x000628B6 File Offset: 0x00060AB6
		internal string fullTypeName
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.typeData.fullTypeName;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x000628C3 File Offset: 0x00060AC3
		internal string typeName
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.typeData.typeName;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x000628D0 File Offset: 0x00060AD0
		internal ref LayoutNode layoutNode
		{
			get
			{
				return ref this.m_LayoutNode;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x000628E8 File Offset: 0x00060AE8
		internal ref ComputedStyle computedStyle
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return ref this.m_Style;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x000628F0 File Offset: 0x00060AF0
		internal bool hasInlineStyle
		{
			get
			{
				return this.inlineStyleAccess != null;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x000628FB File Offset: 0x00060AFB
		// (set) Token: 0x06001909 RID: 6409 RVA: 0x00062910 File Offset: 0x00060B10
		internal bool styleInitialized
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.StyleInitialized) == VisualElementFlags.StyleInitialized;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.StyleInitialized) : (this.m_Flags & ~VisualElementFlags.StyleInitialized));
			}
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00062938 File Offset: 0x00060B38
		private void ChangeIMGUIContainerCount(int delta)
		{
			for (VisualElement ve = this; ve != null; ve = ve.hierarchy.parent)
			{
				ve.imguiContainerDescendantCount += delta;
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00062974 File Offset: 0x00060B74
		public unsafe VisualElement()
		{
			this.m_Children = VisualElement.s_EmptyList;
			this.controlid = (VisualElement.s_NextId += 1U);
			this.hierarchy = new VisualElement.Hierarchy(this);
			this.m_ClassList = VisualElement.s_EmptyClassList;
			this.m_Flags = VisualElementFlags.Init;
			this.enabledSelf = true;
			this.focusable = false;
			this.name = string.Empty;
			*this.layoutNode = LayoutManager.SharedManager.CreateNode();
			this.renderHints = RenderHints.None;
			int defaultActionCategories;
			int defaultActionAtTargetCategories;
			int handleEventTrickleDownCategories;
			int handleEventBubbleUpCategories;
			EventInterestReflectionUtils.GetDefaultEventInterests(base.GetType(), out defaultActionCategories, out defaultActionAtTargetCategories, out handleEventTrickleDownCategories, out handleEventBubbleUpCategories);
			this.m_TrickleDownHandleEventCategories = handleEventTrickleDownCategories;
			this.m_BubbleUpHandleEventCategories = handleEventBubbleUpCategories | defaultActionAtTargetCategories | defaultActionCategories;
			this.UpdateEventInterestSelfCategories();
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00062AB0 File Offset: 0x00060CB0
		~VisualElement()
		{
			LayoutManager.SharedManager.DestroyNode(ref this.m_LayoutNode);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00062AEC File Offset: 0x00060CEC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal virtual Rect GetTooltipRect()
		{
			return this.worldBound;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00062B04 File Offset: 0x00060D04
		internal void SetTooltip(TooltipEvent e)
		{
			VisualElement element = e.currentTarget as VisualElement;
			bool flag = element != null && !string.IsNullOrEmpty(element.tooltip);
			if (flag)
			{
				e.rect = element.GetTooltipRect();
				e.tooltip = element.tooltip;
				e.StopImmediatePropagation();
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00062B5C File Offset: 0x00060D5C
		public sealed override void Focus()
		{
			bool flag = !this.canGrabFocus && this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.Focus();
			}
			else
			{
				base.Focus();
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00062BAC File Offset: 0x00060DAC
		internal void SetPanel(BaseVisualElementPanel p)
		{
			bool flag = this.panel == p;
			if (!flag)
			{
				List<VisualElement> elements = VisualElementListPool.Get(0);
				try
				{
					elements.Add(this);
					this.GatherAllChildren(elements);
					EventDispatcherGate? pDispatcherGate = null;
					bool flag2 = ((p != null) ? p.dispatcher : null) != null;
					if (flag2)
					{
						pDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(p.dispatcher));
					}
					EventDispatcherGate? panelDispatcherGate = null;
					IPanel panel = this.panel;
					bool flag3 = ((panel != null) ? panel.dispatcher : null) != null && this.panel.dispatcher != ((p != null) ? p.dispatcher : null);
					if (flag3)
					{
						panelDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					BaseVisualElementPanel previousPanel = this.elementPanel;
					uint previousHierarchyVersion = ((previousPanel != null) ? previousPanel.hierarchyVersion : 0U);
					EventDispatcherGate? eventDispatcherGate = pDispatcherGate;
					try
					{
						EventDispatcherGate? eventDispatcherGate2 = panelDispatcherGate;
						try
						{
							IPanel panel2 = this.panel;
							if (panel2 != null)
							{
								EventDispatcher dispatcher = panel2.dispatcher;
								if (dispatcher != null)
								{
									dispatcher.m_ClickDetector.Cleanup(elements);
								}
							}
							foreach (VisualElement e in elements)
							{
								e.WillChangePanel(p);
							}
							uint hierarchyVersion = ((previousPanel != null) ? previousPanel.hierarchyVersion : 0U);
							bool flag4 = previousHierarchyVersion != hierarchyVersion;
							if (flag4)
							{
								elements.Clear();
								elements.Add(this);
								this.GatherAllChildren(elements);
							}
							VisualElementFlags flagToAdd = ((p != null) ? VisualElementFlags.NeedsAttachToPanelEvent : ((VisualElementFlags)0));
							foreach (VisualElement e2 in elements)
							{
								e2.elementPanel = p;
								e2.m_Flags |= flagToAdd;
								e2.m_CachedNextParentWithEventInterests = null;
							}
							foreach (VisualElement e3 in elements)
							{
								e3.HasChangedPanel(previousPanel);
							}
						}
						finally
						{
							if (eventDispatcherGate2 != null)
							{
								((IDisposable)eventDispatcherGate2.GetValueOrDefault()).Dispose();
							}
						}
					}
					finally
					{
						if (eventDispatcherGate != null)
						{
							((IDisposable)eventDispatcherGate.GetValueOrDefault()).Dispose();
						}
					}
				}
				finally
				{
					VisualElementListPool.Release(elements);
				}
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00062EA4 File Offset: 0x000610A4
		private void WillChangePanel(BaseVisualElementPanel destinationPanel)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				this.UnregisterRunningAnimations();
				this.CreateBindingRequests();
				this.DetachDataSource();
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == (VisualElementFlags)0;
				if (flag2)
				{
					bool flag3 = this.HasSelfEventInterests(EventBase<DetachFromPanelEvent>.EventCategory);
					if (flag3)
					{
						using (DetachFromPanelEvent e = PanelChangedEventBase<DetachFromPanelEvent>.GetPooled(this.elementPanel, destinationPanel))
						{
							e.elementTarget = this;
							EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(e, this.elementPanel, this);
						}
					}
				}
				this.UnregisterRunningAnimations();
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00062F48 File Offset: 0x00061148
		private void HasChangedPanel(BaseVisualElementPanel prevPanel)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				this.layoutNode.Config = this.elementPanel.layoutConfig;
				this.layoutNode.SoftReset();
				this.RegisterRunningAnimations();
				this.ProcessBindingRequests();
				this.AttachDataSource();
				this.pseudoStates &= ~(PseudoStates.Active | PseudoStates.Hover | PseudoStates.Focus);
				this.m_Flags &= ~VisualElementFlags.HierarchyDisplayed;
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == VisualElementFlags.NeedsAttachToPanelEvent;
				if (flag2)
				{
					bool flag3 = this.HasSelfEventInterests(EventBase<AttachToPanelEvent>.EventCategory);
					if (flag3)
					{
						using (AttachToPanelEvent e = PanelChangedEventBase<AttachToPanelEvent>.GetPooled(prevPanel, this.elementPanel))
						{
							e.elementTarget = this;
							EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(e, this.elementPanel, this);
						}
					}
					this.m_Flags &= ~VisualElementFlags.NeedsAttachToPanelEvent;
				}
			}
			else
			{
				this.layoutNode.Config = LayoutManager.SharedManager.GetDefaultConfig();
			}
			this.styleInitialized = false;
			this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Transform);
			bool flag4 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag4)
			{
				this.IncrementVersion(VersionChangeType.ViewData);
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0006308C File Offset: 0x0006128C
		public sealed override void SendEvent(EventBase e)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, DispatchMode.Default);
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000630A3 File Offset: 0x000612A3
		internal sealed override void SendEvent(EventBase e, DispatchMode dispatchMode)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, dispatchMode);
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000630BA File Offset: 0x000612BA
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void IncrementVersion(VersionChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.OnVersionChanged(this, changeType);
			}
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000630D1 File Offset: 0x000612D1
		internal void InvokeHierarchyChanged(HierarchyChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.InvokeHierarchyChanged(this, changeType);
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000630E8 File Offset: 0x000612E8
		private bool SetEnabledFromHierarchyPrivate(bool state)
		{
			bool initialState = this.enabledInHierarchy;
			bool disable = false;
			if (state)
			{
				bool isParentEnabledInHierarchy = this.isParentEnabledInHierarchy;
				if (isParentEnabledInHierarchy)
				{
					bool enabledSelf = this.enabledSelf;
					if (enabledSelf)
					{
						this.RemoveFromClassList(VisualElement.disabledUssClassName);
					}
					else
					{
						disable = true;
						this.AddToClassList(VisualElement.disabledUssClassName);
					}
				}
				else
				{
					disable = true;
					this.RemoveFromClassList(VisualElement.disabledUssClassName);
				}
			}
			else
			{
				disable = true;
				this.EnableInClassList(VisualElement.disabledUssClassName, this.isParentEnabledInHierarchy);
			}
			bool flag = disable;
			if (flag)
			{
				bool flag2 = this.focusController != null && this.focusController.IsFocused(this);
				if (flag2)
				{
					EventDispatcherGate? dispatcherGate = null;
					IPanel panel = this.panel;
					bool flag3 = ((panel != null) ? panel.dispatcher : null) != null;
					if (flag3)
					{
						dispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate = dispatcherGate;
					try
					{
						base.BlurImmediately();
					}
					finally
					{
						if (eventDispatcherGate != null)
						{
							((IDisposable)eventDispatcherGate.GetValueOrDefault()).Dispose();
						}
					}
				}
				this.pseudoStates |= PseudoStates.Disabled;
			}
			else
			{
				this.pseudoStates &= ~PseudoStates.Disabled;
			}
			return initialState != this.enabledInHierarchy;
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x00063248 File Offset: 0x00061448
		private bool isParentEnabledInHierarchy
		{
			get
			{
				return this.hierarchy.parent == null || this.hierarchy.parent.enabledInHierarchy;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x00063280 File Offset: 0x00061480
		[CreateProperty(ReadOnly = true)]
		public bool enabledInHierarchy
		{
			get
			{
				return (this.pseudoStates & PseudoStates.Disabled) != PseudoStates.Disabled;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x000632A2 File Offset: 0x000614A2
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x000632AC File Offset: 0x000614AC
		[CreateProperty]
		public bool enabledSelf
		{
			get
			{
				return this.m_EnabledSelf;
			}
			set
			{
				bool flag = this.m_EnabledSelf == value;
				if (!flag)
				{
					this.m_EnabledSelf = value;
					base.NotifyPropertyChanged(in VisualElement.enabledSelfProperty);
					this.PropagateEnabledToChildren(value);
				}
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000632E4 File Offset: 0x000614E4
		public void SetEnabled(bool value)
		{
			this.enabledSelf = value;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000632F0 File Offset: 0x000614F0
		private void PropagateEnabledToChildren(bool value)
		{
			bool flag = this.SetEnabledFromHierarchyPrivate(value);
			if (flag)
			{
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_Children[i].PropagateEnabledToChildren(value);
				}
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0006333C File Offset: 0x0006153C
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x00063344 File Offset: 0x00061544
		[CreateProperty]
		public LanguageDirection languageDirection
		{
			get
			{
				return this.m_LanguageDirection;
			}
			set
			{
				bool flag = this.m_LanguageDirection == value;
				if (!flag)
				{
					this.m_LanguageDirection = value;
					this.localLanguageDirection = this.m_LanguageDirection;
					base.NotifyPropertyChanged(in VisualElement.languageDirectionProperty);
				}
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x00063381 File Offset: 0x00061581
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0006338C File Offset: 0x0006158C
		internal LanguageDirection localLanguageDirection
		{
			get
			{
				return this.m_LocalLanguageDirection;
			}
			set
			{
				bool flag = this.m_LocalLanguageDirection == value;
				if (!flag)
				{
					this.m_LocalLanguageDirection = value;
					this.IncrementVersion(VersionChangeType.Layout);
					int count = this.m_Children.Count;
					for (int i = 0; i < count; i++)
					{
						bool flag2 = this.m_Children[i].languageDirection == LanguageDirection.Inherit;
						if (flag2)
						{
							this.m_Children[i].localLanguageDirection = this.m_LocalLanguageDirection;
						}
					}
				}
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00063408 File Offset: 0x00061608
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x00063428 File Offset: 0x00061628
		[CreateProperty]
		public bool visible
		{
			get
			{
				return this.resolvedStyle.visibility == Visibility.Visible;
			}
			set
			{
				bool previous = this.visible;
				this.style.visibility = (value ? Visibility.Visible : Visibility.Hidden);
				bool flag = previous != this.visible;
				if (flag)
				{
					base.NotifyPropertyChanged(in VisualElement.visibleProperty);
				}
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00063471 File Offset: 0x00061671
		public void MarkDirtyRepaint()
		{
			this.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x00063480 File Offset: 0x00061680
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x00063488 File Offset: 0x00061688
		public Action<MeshGenerationContext> generateVisualContent { get; set; }

		// Token: 0x06001927 RID: 6439 RVA: 0x00063494 File Offset: 0x00061694
		internal void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.generateVisualContent != null;
			if (flag)
			{
				try
				{
					using (VisualElement.k_GenerateVisualContentMarker.Auto())
					{
						this.generateVisualContent(mgc);
					}
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0006350C File Offset: 0x0006170C
		internal void GetFullHierarchicalViewDataKey(StringBuilder key)
		{
			bool flag = this.parent != null;
			if (flag)
			{
				this.parent.GetFullHierarchicalViewDataKey(key);
			}
			bool flag2 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag2)
			{
				key.Append("__");
				key.Append(this.viewDataKey);
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00063564 File Offset: 0x00061764
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string GetFullHierarchicalViewDataKey()
		{
			StringBuilder key = new StringBuilder();
			this.GetFullHierarchicalViewDataKey(key);
			return key.ToString();
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0006358C File Offset: 0x0006178C
		internal T GetOrCreateViewData<T>(object existing, string key) where T : class, new()
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load persistent data.");
			ISerializableJsonDictionary viewData = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag = viewData == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T t;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					t = existing as T;
				}
				else
				{
					t = new T();
				}
			}
			else
			{
				string text = "__";
				Type typeFromHandle = typeof(T);
				string keyWithType = key + text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				bool flag3 = !viewData.ContainsKey(keyWithType);
				if (flag3)
				{
					viewData.Set<T>(keyWithType, new T());
				}
				t = viewData.Get<T>(keyWithType);
			}
			return t;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00063664 File Offset: 0x00061864
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void OverwriteFromViewData(object obj, string key)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary viewDataPersistentData = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag2 = viewDataPersistentData == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			if (!flag2)
			{
				string text = "__";
				Type type = obj.GetType();
				string keyWithType = key + text + ((type != null) ? type.ToString() : null);
				bool flag3 = !viewDataPersistentData.ContainsKey(keyWithType);
				if (flag3)
				{
					viewDataPersistentData.Set<object>(keyWithType, obj);
				}
				else
				{
					viewDataPersistentData.Overwrite(obj, keyWithType);
				}
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0006372C File Offset: 0x0006192C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SaveViewData()
		{
			bool flag = this.elementPanel != null && this.elementPanel.saveViewData != null && !string.IsNullOrEmpty(this.viewDataKey) && this.enableViewDataPersistence;
			if (flag)
			{
				this.elementPanel.saveViewData();
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x000020EA File Offset: 0x000002EA
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal virtual void OnViewDataReady()
		{
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0006377C File Offset: 0x0006197C
		public virtual bool ContainsPoint(Vector2 localPoint)
		{
			return this.rect.Contains(localPoint);
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0006379D File Offset: 0x0006199D
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x000637B4 File Offset: 0x000619B4
		internal bool requireMeasureFunction
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.RequireMeasureFunction) == VisualElementFlags.RequireMeasureFunction;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.RequireMeasureFunction) : (this.m_Flags & ~VisualElementFlags.RequireMeasureFunction));
				bool flag = value && !this.layoutNode.IsMeasureDefined;
				if (flag)
				{
					this.AssignMeasureFunction();
				}
				else
				{
					bool flag2 = !value && this.layoutNode.IsMeasureDefined;
					if (flag2)
					{
						this.RemoveMeasureFunction();
					}
				}
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00063826 File Offset: 0x00061A26
		private void AssignMeasureFunction()
		{
			this.layoutNode.SetOwner(this);
			this.layoutNode.Measure = new LayoutMeasureFunction(VisualElement.Measure);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0006384E File Offset: 0x00061A4E
		private void RemoveMeasureFunction()
		{
			this.layoutNode.Measure = null;
			this.layoutNode.SetOwner(null);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0006386C File Offset: 0x00061A6C
		protected internal virtual Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return new Vector2(float.NaN, float.NaN);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00063890 File Offset: 0x00061A90
		internal unsafe static void Measure(VisualElement ve, ref LayoutNode node, float width, LayoutMeasureMode widthMode, float height, LayoutMeasureMode heightMode, out LayoutSize result)
		{
			result = default(LayoutSize);
			Debug.Assert(node.Equals(*ve.layoutNode), "LayoutNode instance mismatch");
			Vector2 size = ve.DoMeasure(width, (VisualElement.MeasureMode)widthMode, height, (VisualElement.MeasureMode)heightMode);
			float ppp = ve.scaledPixelsPerPoint;
			result = new LayoutSize(AlignmentUtils.RoundToPixelGrid(size.x, ppp, 0.02f), AlignmentUtils.RoundToPixelGrid(size.y, ppp, 0.02f));
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00063904 File Offset: 0x00061B04
		private unsafe void FinalizeLayout()
		{
			this.layoutNode.CopyFromComputedStyle(*this.computedStyle);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00063920 File Offset: 0x00061B20
		internal void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			bool flag = this.inlineStyleAccess == null;
			if (flag)
			{
				this.inlineStyleAccess = new InlineStyleAccess(this);
			}
			this.inlineStyleAccess.SetInlineRule(sheet, rule);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00063958 File Offset: 0x00061B58
		internal void SetComputedStyle(ref ComputedStyle newStyle)
		{
			bool flag = this.m_Style.matchingRulesHash == newStyle.matchingRulesHash;
			if (!flag)
			{
				VersionChangeType changes = ComputedStyle.CompareChanges(ref this.m_Style, ref newStyle);
				this.m_Style.CopyFrom(ref newStyle);
				this.FinalizeLayout();
				BaseVisualElementPanel elementPanel = this.elementPanel;
				bool flag2 = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) : null) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.m_Style.cursor);
				}
				this.IncrementVersion(changes);
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000639E4 File Offset: 0x00061BE4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				this.name,
				" ",
				this.layout.ToString(),
				" world rect: ",
				this.worldBound.ToString()
			});
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00063A60 File Offset: 0x00061C60
		internal List<string> GetClassesForIteration()
		{
			return this.m_ClassList;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00063A78 File Offset: 0x00061C78
		public void AddToClassList(string className)
		{
			bool flag = string.IsNullOrEmpty(className);
			if (!flag)
			{
				bool flag2 = this.m_ClassList == VisualElement.s_EmptyClassList;
				if (flag2)
				{
					this.m_ClassList = ObjectListPool<string>.Get();
				}
				else
				{
					bool flag3 = this.m_ClassList.Contains(className);
					if (flag3)
					{
						return;
					}
					bool flag4 = this.m_ClassList.Capacity == this.m_ClassList.Count;
					if (flag4)
					{
						this.m_ClassList.Capacity++;
					}
				}
				this.m_ClassList.Add(className);
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00063B10 File Offset: 0x00061D10
		public void RemoveFromClassList(string className)
		{
			bool flag = this.m_ClassList.Remove(className);
			if (flag)
			{
				bool flag2 = this.m_ClassList.Count == 0;
				if (flag2)
				{
					ObjectListPool<string>.Release(this.m_ClassList);
					this.m_ClassList = VisualElement.s_EmptyClassList;
				}
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00063B64 File Offset: 0x00061D64
		public void EnableInClassList(string className, bool enable)
		{
			if (enable)
			{
				this.AddToClassList(className);
			}
			else
			{
				this.RemoveFromClassList(className);
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00063B8C File Offset: 0x00061D8C
		public bool ClassListContains(string cls)
		{
			for (int i = 0; i < this.m_ClassList.Count; i++)
			{
				bool flag = this.m_ClassList[i].Equals(cls, StringComparison.Ordinal);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00063BD8 File Offset: 0x00061DD8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal object GetProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			bool flag = this.m_PropertyBag != null;
			object obj;
			if (flag)
			{
				object value;
				this.m_PropertyBag.TryGetValue(key, out value);
				obj = value;
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00063C13 File Offset: 0x00061E13
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SetProperty(PropertyName key, object value)
		{
			VisualElement.CheckUserKeyArgument(key);
			this.SetPropertyInternal(key, value);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00063C28 File Offset: 0x00061E28
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool HasProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			Dictionary<PropertyName, object> propertyBag = this.m_PropertyBag;
			return propertyBag != null && propertyBag.ContainsKey(key);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00063C54 File Offset: 0x00061E54
		internal bool ClearProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			Dictionary<PropertyName, object> propertyBag = this.m_PropertyBag;
			return propertyBag != null && propertyBag.Remove(key);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00063C80 File Offset: 0x00061E80
		private static void CheckUserKeyArgument(PropertyName key)
		{
			bool flag = PropertyName.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentNullException("key");
			}
			bool flag2 = key == VisualElement.userDataPropertyKey;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The {0} key is reserved by the system", VisualElement.userDataPropertyKey));
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00063CCC File Offset: 0x00061ECC
		private void SetPropertyInternal(PropertyName key, object value)
		{
			if (this.m_PropertyBag == null)
			{
				this.m_PropertyBag = new Dictionary<PropertyName, object>();
			}
			this.m_PropertyBag[key] = value;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00063CF0 File Offset: 0x00061EF0
		internal void UpdateCursorStyle(long eventType)
		{
			bool flag = this.elementPanel == null;
			if (!flag)
			{
				bool flag2 = eventType == EventBase<MouseCaptureOutEvent>.TypeId();
				if (flag2)
				{
					VisualElement elementUnderPointer = this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId);
					bool flag3 = elementUnderPointer != null;
					if (flag3)
					{
						this.elementPanel.cursorManager.SetCursor(elementUnderPointer.computedStyle.cursor);
					}
					else
					{
						this.elementPanel.cursorManager.ResetCursor();
					}
				}
				else
				{
					IEventHandler capturingElement = this.elementPanel.GetCapturingElement(PointerId.mousePointerId);
					bool flag4 = capturingElement != null && capturingElement != this;
					if (!flag4)
					{
						bool flag5 = eventType == EventBase<MouseOverEvent>.TypeId() && this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) == this;
						if (flag5)
						{
							this.elementPanel.cursorManager.SetCursor(this.computedStyle.cursor);
						}
						else
						{
							bool flag6 = eventType == EventBase<MouseOutEvent>.TypeId() && capturingElement == null;
							if (flag6)
							{
								this.elementPanel.cursorManager.ResetCursor();
							}
						}
					}
				}
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00063E04 File Offset: 0x00062004
		internal VisualElement.RenderTargetMode subRenderTargetMode
		{
			get
			{
				return this.m_SubRenderTargetMode;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00063E1C File Offset: 0x0006201C
		internal Material defaultMaterial
		{
			get
			{
				return this.m_defaultMaterial;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00063E34 File Offset: 0x00062034
		private VisualElementAnimationSystem GetAnimationSystem()
		{
			bool flag = this.elementPanel != null;
			VisualElementAnimationSystem visualElementAnimationSystem;
			if (flag)
			{
				visualElementAnimationSystem = this.elementPanel.GetUpdater(VisualTreeUpdatePhase.Animation) as VisualElementAnimationSystem;
			}
			else
			{
				visualElementAnimationSystem = null;
			}
			return visualElementAnimationSystem;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00063E6C File Offset: 0x0006206C
		internal void RegisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations == null;
			if (flag)
			{
				this.m_RunningAnimations = new List<IValueAnimationUpdate>();
			}
			this.m_RunningAnimations.Add(anim);
			VisualElementAnimationSystem sys = this.GetAnimationSystem();
			bool flag2 = sys != null;
			if (flag2)
			{
				sys.RegisterAnimation(anim);
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00063EBC File Offset: 0x000620BC
		internal void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations != null;
			if (flag)
			{
				this.m_RunningAnimations.Remove(anim);
			}
			VisualElementAnimationSystem sys = this.GetAnimationSystem();
			bool flag2 = sys != null;
			if (flag2)
			{
				sys.UnregisterAnimation(anim);
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00063F00 File Offset: 0x00062100
		private void UnregisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem sys = this.GetAnimationSystem();
				bool flag2 = sys != null;
				if (flag2)
				{
					sys.UnregisterAnimations(this.m_RunningAnimations);
				}
			}
			this.styleAnimation.CancelAllAnimations();
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00063F58 File Offset: 0x00062158
		private void RegisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem sys = this.GetAnimationSystem();
				bool flag2 = sys != null;
				if (flag2)
				{
					sys.RegisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00063FA4 File Offset: 0x000621A4
		private static ValueAnimation<T> StartAnimation<T>(ValueAnimation<T> anim, Func<VisualElement, T> fromValueGetter, T to, int durationMs, Action<VisualElement, T> onValueChanged)
		{
			anim.initialValue = fromValueGetter;
			anim.to = to;
			anim.durationMs = durationMs;
			anim.valueUpdated = onValueChanged;
			anim.Start();
			return anim;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00063FE0 File Offset: 0x000621E0
		private static void AssignStyleValues(VisualElement ve, StyleValues src)
		{
			IStyle s = ve.style;
			bool flag = src.m_StyleValues != null;
			if (flag)
			{
				foreach (StyleValue styleValue in src.m_StyleValues.m_Values)
				{
					StylePropertyId id = styleValue.id;
					StylePropertyId stylePropertyId = id;
					if (stylePropertyId <= StylePropertyId.Width)
					{
						if (stylePropertyId <= StylePropertyId.Color)
						{
							if (stylePropertyId != StylePropertyId.Unknown)
							{
								if (stylePropertyId == StylePropertyId.Color)
								{
									s.color = styleValue.color;
								}
							}
						}
						else if (stylePropertyId != StylePropertyId.FontSize)
						{
							switch (stylePropertyId)
							{
							case StylePropertyId.BorderBottomWidth:
								s.borderBottomWidth = styleValue.number;
								break;
							case StylePropertyId.BorderLeftWidth:
								s.borderLeftWidth = styleValue.number;
								break;
							case StylePropertyId.BorderRightWidth:
								s.borderRightWidth = styleValue.number;
								break;
							case StylePropertyId.BorderTopWidth:
								s.borderTopWidth = styleValue.number;
								break;
							case StylePropertyId.Bottom:
								s.bottom = styleValue.number;
								break;
							case StylePropertyId.FlexGrow:
								s.flexGrow = styleValue.number;
								break;
							case StylePropertyId.FlexShrink:
								s.flexShrink = styleValue.number;
								break;
							case StylePropertyId.Height:
								s.height = styleValue.number;
								break;
							case StylePropertyId.Left:
								s.left = styleValue.number;
								break;
							case StylePropertyId.MarginBottom:
								s.marginBottom = styleValue.number;
								break;
							case StylePropertyId.MarginLeft:
								s.marginLeft = styleValue.number;
								break;
							case StylePropertyId.MarginRight:
								s.marginRight = styleValue.number;
								break;
							case StylePropertyId.MarginTop:
								s.marginTop = styleValue.number;
								break;
							case StylePropertyId.PaddingBottom:
								s.paddingBottom = styleValue.number;
								break;
							case StylePropertyId.PaddingLeft:
								s.paddingLeft = styleValue.number;
								break;
							case StylePropertyId.PaddingRight:
								s.paddingRight = styleValue.number;
								break;
							case StylePropertyId.PaddingTop:
								s.paddingTop = styleValue.number;
								break;
							case StylePropertyId.Right:
								s.right = styleValue.number;
								break;
							case StylePropertyId.Top:
								s.top = styleValue.number;
								break;
							case StylePropertyId.Width:
								s.width = styleValue.number;
								break;
							}
						}
						else
						{
							s.fontSize = styleValue.number;
						}
					}
					else if (stylePropertyId <= StylePropertyId.BorderColor)
					{
						if (stylePropertyId != StylePropertyId.UnityBackgroundImageTintColor)
						{
							if (stylePropertyId == StylePropertyId.BorderColor)
							{
								s.borderLeftColor = styleValue.color;
								s.borderTopColor = styleValue.color;
								s.borderRightColor = styleValue.color;
								s.borderBottomColor = styleValue.color;
							}
						}
						else
						{
							s.unityBackgroundImageTintColor = styleValue.color;
						}
					}
					else if (stylePropertyId != StylePropertyId.BackgroundColor)
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.BorderBottomLeftRadius:
							s.borderBottomLeftRadius = styleValue.number;
							break;
						case StylePropertyId.BorderBottomRightRadius:
							s.borderBottomRightRadius = styleValue.number;
							break;
						case StylePropertyId.BorderTopLeftRadius:
							s.borderTopLeftRadius = styleValue.number;
							break;
						case StylePropertyId.BorderTopRightRadius:
							s.borderTopRightRadius = styleValue.number;
							break;
						case StylePropertyId.Opacity:
							s.opacity = styleValue.number;
							break;
						}
					}
					else
					{
						s.backgroundColor = styleValue.color;
					}
				}
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00064480 File Offset: 0x00062680
		private StyleValues ReadCurrentValues(VisualElement ve, StyleValues targetValuesToRead)
		{
			StyleValues s = default(StyleValues);
			IResolvedStyle src = ve.resolvedStyle;
			bool flag = targetValuesToRead.m_StyleValues != null;
			if (flag)
			{
				foreach (StyleValue styleValue in targetValuesToRead.m_StyleValues.m_Values)
				{
					StylePropertyId id = styleValue.id;
					StylePropertyId stylePropertyId = id;
					if (stylePropertyId <= StylePropertyId.Width)
					{
						if (stylePropertyId != StylePropertyId.Unknown)
						{
							if (stylePropertyId != StylePropertyId.Color)
							{
								switch (stylePropertyId)
								{
								case StylePropertyId.BorderBottomWidth:
									s.borderBottomWidth = src.borderBottomWidth;
									break;
								case StylePropertyId.BorderLeftWidth:
									s.borderLeftWidth = src.borderLeftWidth;
									break;
								case StylePropertyId.BorderRightWidth:
									s.borderRightWidth = src.borderRightWidth;
									break;
								case StylePropertyId.BorderTopWidth:
									s.borderTopWidth = src.borderTopWidth;
									break;
								case StylePropertyId.Bottom:
									s.bottom = src.bottom;
									break;
								case StylePropertyId.FlexGrow:
									s.flexGrow = src.flexGrow;
									break;
								case StylePropertyId.FlexShrink:
									s.flexShrink = src.flexShrink;
									break;
								case StylePropertyId.Height:
									s.height = src.height;
									break;
								case StylePropertyId.Left:
									s.left = src.left;
									break;
								case StylePropertyId.MarginBottom:
									s.marginBottom = src.marginBottom;
									break;
								case StylePropertyId.MarginLeft:
									s.marginLeft = src.marginLeft;
									break;
								case StylePropertyId.MarginRight:
									s.marginRight = src.marginRight;
									break;
								case StylePropertyId.MarginTop:
									s.marginTop = src.marginTop;
									break;
								case StylePropertyId.PaddingBottom:
									s.paddingBottom = src.paddingBottom;
									break;
								case StylePropertyId.PaddingLeft:
									s.paddingLeft = src.paddingLeft;
									break;
								case StylePropertyId.PaddingRight:
									s.paddingRight = src.paddingRight;
									break;
								case StylePropertyId.PaddingTop:
									s.paddingTop = src.paddingTop;
									break;
								case StylePropertyId.Right:
									s.right = src.right;
									break;
								case StylePropertyId.Top:
									s.top = src.top;
									break;
								case StylePropertyId.Width:
									s.width = src.width;
									break;
								}
							}
							else
							{
								s.color = src.color;
							}
						}
					}
					else if (stylePropertyId <= StylePropertyId.BorderColor)
					{
						if (stylePropertyId != StylePropertyId.UnityBackgroundImageTintColor)
						{
							if (stylePropertyId == StylePropertyId.BorderColor)
							{
								s.borderColor = src.borderLeftColor;
							}
						}
						else
						{
							s.unityBackgroundImageTintColor = src.unityBackgroundImageTintColor;
						}
					}
					else if (stylePropertyId != StylePropertyId.BackgroundColor)
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.BorderBottomLeftRadius:
							s.borderBottomLeftRadius = src.borderBottomLeftRadius;
							break;
						case StylePropertyId.BorderBottomRightRadius:
							s.borderBottomRightRadius = src.borderBottomRightRadius;
							break;
						case StylePropertyId.BorderTopLeftRadius:
							s.borderTopLeftRadius = src.borderTopLeftRadius;
							break;
						case StylePropertyId.BorderTopRightRadius:
							s.borderTopRightRadius = src.borderTopRightRadius;
							break;
						case StylePropertyId.Opacity:
							s.opacity = src.opacity;
							break;
						}
					}
					else
					{
						s.backgroundColor = src.backgroundColor;
					}
				}
			}
			return s;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0006484C File Offset: 0x00062A4C
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues to, int durationMs)
		{
			bool flag = to.m_StyleValues == null;
			if (flag)
			{
				to.Values();
			}
			return this.Start((VisualElement e) => this.ReadCurrentValues(e, to), to, durationMs);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000648AC File Offset: 0x00062AAC
		private ValueAnimation<StyleValues> Start(Func<VisualElement, StyleValues> fromValueGetter, StyleValues to, int durationMs)
		{
			return VisualElement.StartAnimation<StyleValues>(ValueAnimation<StyleValues>.Create(this, new Func<StyleValues, StyleValues, float, StyleValues>(Lerp.Interpolate)), fromValueGetter, to, durationMs, new Action<VisualElement, StyleValues>(VisualElement.AssignStyleValues));
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000648E4 File Offset: 0x00062AE4
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x000648EC File Offset: 0x00062AEC
		[CreateProperty]
		public object dataSource
		{
			get
			{
				return this.m_DataSource;
			}
			set
			{
				bool flag = this.m_DataSource == value;
				if (!flag)
				{
					object previous = this.m_DataSource;
					this.m_DataSource = value;
					this.TrackSource(previous, this.m_DataSource);
					this.IncrementVersion(VersionChangeType.DataSource);
					base.NotifyPropertyChanged(in VisualElement.dataSourceProperty);
				}
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0006493D File Offset: 0x00062B3D
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00064948 File Offset: 0x00062B48
		[CreateProperty]
		public PropertyPath dataSourcePath
		{
			get
			{
				return this.m_DataSourcePath;
			}
			set
			{
				bool flag = this.m_DataSourcePath == value;
				if (!flag)
				{
					this.m_DataSourcePath = value;
					this.IncrementVersion(VersionChangeType.DataSource);
					base.NotifyPropertyChanged(in VisualElement.dataSourcePathProperty);
				}
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00064988 File Offset: 0x00062B88
		public bool TryGetBinding(BindingId bindingId, out Binding binding)
		{
			BindingInfo bindingInfo;
			bool flag = DataBindingUtility.TryGetBinding(this, in bindingId, out bindingInfo);
			bool flag2;
			if (flag)
			{
				binding = bindingInfo.binding;
				flag2 = true;
			}
			else
			{
				binding = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000649BC File Offset: 0x00062BBC
		private void ProcessBindingRequests()
		{
			Assert.IsFalse(this.elementPanel == null, null);
			bool flag = DataBindingManager.AnyPendingBindingRequests(this);
			if (flag)
			{
				this.IncrementVersion(VersionChangeType.BindingRegistration);
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000649F0 File Offset: 0x00062BF0
		private void CreateBindingRequests()
		{
			BaseVisualElementPanel p = this.elementPanel;
			Assert.IsFalse(p == null, null);
			p.dataBindingManager.TransferBindingRequests(this);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00064A20 File Offset: 0x00062C20
		private void TrackSource(object previous, object current)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			DataBindingManager manager = ((elementPanel != null) ? elementPanel.dataBindingManager : null);
			bool flag = manager == null;
			if (!flag)
			{
				bool flag2 = (this.m_Flags & VisualElementFlags.DetachedDataSource) == VisualElementFlags.DetachedDataSource;
				if (!flag2)
				{
					BaseVisualElementPanel elementPanel2 = this.elementPanel;
					if (elementPanel2 != null)
					{
						elementPanel2.dataBindingManager.TrackDataSource(previous, current);
					}
				}
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00064A7D File Offset: 0x00062C7D
		private void DetachDataSource()
		{
			this.TrackSource(this.dataSource, null);
			this.m_Flags |= VisualElementFlags.DetachedDataSource;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00064AA0 File Offset: 0x00062CA0
		private void AttachDataSource()
		{
			this.m_Flags &= ~VisualElementFlags.DetachedDataSource;
			this.TrackSource(null, this.dataSource);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00064AC4 File Offset: 0x00062CC4
		private void DirtyNextParentWithEventInterests()
		{
			bool flag = this.m_CachedNextParentWithEventInterests != null && this.m_NextParentCachedVersion == this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion;
			if (flag)
			{
				this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion = (VisualElement.s_NextParentVersion += 1U);
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00064B10 File Offset: 0x00062D10
		internal void SetAsNextParentWithEventInterests()
		{
			bool flag = this.m_NextParentRequiredVersion > 0U;
			if (!flag)
			{
				this.m_NextParentRequiredVersion = (VisualElement.s_NextParentVersion += 1U);
				bool flag2 = this.m_CachedNextParentWithEventInterests != null && this.m_NextParentCachedVersion == this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion;
				if (flag2)
				{
					this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion = (VisualElement.s_NextParentVersion += 1U);
				}
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00064B7C File Offset: 0x00062D7C
		internal bool GetCachedNextParentWithEventInterests(out VisualElement nextParent)
		{
			nextParent = this.m_CachedNextParentWithEventInterests;
			return nextParent != null && nextParent.m_NextParentRequiredVersion == this.m_NextParentCachedVersion;
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00064BAC File Offset: 0x00062DAC
		internal VisualElement nextParentWithEventInterests
		{
			get
			{
				VisualElement nextParent;
				bool cachedNextParentWithEventInterests = this.GetCachedNextParentWithEventInterests(out nextParent);
				VisualElement visualElement;
				if (cachedNextParentWithEventInterests)
				{
					visualElement = nextParent;
				}
				else
				{
					for (VisualElement candidate = this.hierarchy.parent; candidate != null; candidate = candidate.hierarchy.parent)
					{
						bool flag = candidate.m_NextParentRequiredVersion > 0U;
						if (flag)
						{
							this.PropagateCachedNextParentWithEventInterests(candidate, candidate);
							return candidate;
						}
						VisualElement candidateNextParent;
						bool cachedNextParentWithEventInterests2 = candidate.GetCachedNextParentWithEventInterests(out candidateNextParent);
						if (cachedNextParentWithEventInterests2)
						{
							this.PropagateCachedNextParentWithEventInterests(candidateNextParent, candidate);
							return candidateNextParent;
						}
					}
					this.m_CachedNextParentWithEventInterests = null;
					visualElement = null;
				}
				return visualElement;
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00064C40 File Offset: 0x00062E40
		private void PropagateCachedNextParentWithEventInterests(VisualElement nextParent, VisualElement stopParent)
		{
			for (VisualElement ve = this; ve != stopParent; ve = ve.hierarchy.parent)
			{
				ve.m_CachedNextParentWithEventInterests = nextParent;
				ve.m_NextParentCachedVersion = nextParent.m_NextParentRequiredVersion;
			}
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00064C84 File Offset: 0x00062E84
		internal void AddEventCallbackCategories(int eventCategories, TrickleDown trickleDown)
		{
			bool flag = trickleDown == TrickleDown.TrickleDown;
			if (flag)
			{
				this.m_TrickleDownEventCallbackCategories |= eventCategories;
			}
			else
			{
				this.m_BubbleUpEventCallbackCategories |= eventCategories;
			}
			this.UpdateEventInterestSelfCategories();
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00064CC0 File Offset: 0x00062EC0
		internal int eventInterestParentCategories
		{
			get
			{
				bool flag = this.elementPanel == null;
				int num;
				if (flag)
				{
					num = -1;
				}
				else
				{
					bool isEventInterestParentCategoriesDirty = this.isEventInterestParentCategoriesDirty;
					if (isEventInterestParentCategoriesDirty)
					{
						this.UpdateEventInterestParentCategories();
						this.isEventInterestParentCategoriesDirty = false;
					}
					num = this.m_CachedEventInterestParentCategories;
				}
				return num;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00064D04 File Offset: 0x00062F04
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x00064D13 File Offset: 0x00062F13
		internal bool isEventInterestParentCategoriesDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.EventInterestParentCategoriesDirty) == VisualElementFlags.EventInterestParentCategoriesDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.EventInterestParentCategoriesDirty) : (this.m_Flags & ~VisualElementFlags.EventInterestParentCategoriesDirty));
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00064D34 File Offset: 0x00062F34
		private void UpdateEventInterestSelfCategories()
		{
			int value = this.m_TrickleDownHandleEventCategories | this.m_BubbleUpHandleEventCategories | this.m_TrickleDownEventCallbackCategories | this.m_BubbleUpEventCallbackCategories;
			bool flag = this.m_EventInterestSelfCategories != value;
			if (flag)
			{
				int diff = this.m_EventInterestSelfCategories ^ value;
				bool flag2 = (diff & -5537) != 0;
				if (flag2)
				{
					this.SetAsNextParentWithEventInterests();
					this.IncrementVersion(VersionChangeType.EventCallbackCategories);
				}
				else
				{
					this.m_CachedEventInterestParentCategories |= value;
				}
				this.m_EventInterestSelfCategories = value;
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00064DB8 File Offset: 0x00062FB8
		private void UpdateEventInterestParentCategories()
		{
			this.m_CachedEventInterestParentCategories = this.m_EventInterestSelfCategories;
			VisualElement nextParent = this.nextParentWithEventInterests;
			bool flag = nextParent == null;
			if (!flag)
			{
				this.m_CachedEventInterestParentCategories |= nextParent.eventInterestParentCategories;
				bool flag2 = this.hierarchy.parent != null;
				if (flag2)
				{
					for (VisualElement ve = this.hierarchy.parent; ve != nextParent; ve = ve.hierarchy.parent)
					{
						ve.m_CachedEventInterestParentCategories = this.m_CachedEventInterestParentCategories;
						ve.isEventInterestParentCategoriesDirty = false;
					}
				}
			}
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00064E56 File Offset: 0x00063056
		internal bool HasParentEventInterests(EventCategory eventCategory)
		{
			return (this.eventInterestParentCategories & (1 << (int)eventCategory)) != 0;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00064E68 File Offset: 0x00063068
		internal bool HasParentEventInterests(int eventCategories)
		{
			return (this.eventInterestParentCategories & eventCategories) != 0;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00064E75 File Offset: 0x00063075
		internal bool HasSelfEventInterests(EventCategory eventCategory)
		{
			return (this.m_EventInterestSelfCategories & (1 << (int)eventCategory)) != 0;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00064E87 File Offset: 0x00063087
		internal bool HasSelfEventInterests(int eventCategories)
		{
			return (this.m_EventInterestSelfCategories & eventCategories) != 0;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00064E94 File Offset: 0x00063094
		internal bool HasTrickleDownEventInterests(int eventCategories)
		{
			return ((this.m_TrickleDownHandleEventCategories | this.m_TrickleDownEventCallbackCategories) & eventCategories) != 0;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00064EA8 File Offset: 0x000630A8
		internal bool HasBubbleUpEventInterests(int eventCategories)
		{
			return ((this.m_BubbleUpHandleEventCategories | this.m_BubbleUpEventCallbackCategories) & eventCategories) != 0;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00064EBC File Offset: 0x000630BC
		internal bool HasTrickleDownEventCallbacks(int eventCategories)
		{
			return (this.m_TrickleDownEventCallbackCategories & eventCategories) != 0;
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00064EC9 File Offset: 0x000630C9
		internal bool HasBubbleUpEventCallbacks(int eventCategories)
		{
			return (this.m_BubbleUpEventCallbackCategories & eventCategories) != 0;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00064ED6 File Offset: 0x000630D6
		internal bool HasTrickleDownHandleEvent(int eventCategories)
		{
			return (this.m_TrickleDownHandleEventCategories & eventCategories) != 0;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00064EE3 File Offset: 0x000630E3
		internal bool HasBubbleUpHandleEvent(int eventCategories)
		{
			return (this.m_BubbleUpHandleEventCategories & eventCategories) != 0;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00064EF0 File Offset: 0x000630F0
		public IExperimentalFeatures experimental
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00064F04 File Offset: 0x00063104
		ITransitionAnimations IExperimentalFeatures.animation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00064F17 File Offset: 0x00063117
		public VisualElement.Hierarchy hierarchy { get; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00064F1F File Offset: 0x0006311F
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x00064F27 File Offset: 0x00063127
		internal bool isRootVisualContainer { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00064F30 File Offset: 0x00063130
		// (set) Token: 0x06001976 RID: 6518 RVA: 0x00064F45 File Offset: 0x00063145
		internal bool disableClipping
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.DisableClipping) == VisualElementFlags.DisableClipping;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.DisableClipping) : (this.m_Flags & ~VisualElementFlags.DisableClipping));
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00064F6C File Offset: 0x0006316C
		internal bool ShouldClip()
		{
			return this.computedStyle.overflow != OverflowInternal.Visible && !this.disableClipping;
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00064F97 File Offset: 0x00063197
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x00064FAC File Offset: 0x000631AC
		internal bool disableRendering
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.DisableRendering) == VisualElementFlags.DisableRendering;
			}
			set
			{
				VisualElementFlags oldFlags = this.m_Flags;
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.DisableRendering) : (this.m_Flags & ~VisualElementFlags.DisableRendering));
				bool flag = oldFlags != this.m_Flags;
				if (flag)
				{
					this.IncrementVersion(VersionChangeType.DisableRendering);
				}
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600197A RID: 6522 RVA: 0x00065004 File Offset: 0x00063204
		// (remove) Token: 0x0600197B RID: 6523 RVA: 0x0006503C File Offset: 0x0006323C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<VisualElement> elementAdded;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600197C RID: 6524 RVA: 0x00065074 File Offset: 0x00063274
		// (remove) Token: 0x0600197D RID: 6525 RVA: 0x000650AC File Offset: 0x000632AC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<VisualElement> elementRemoved;

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x000650E4 File Offset: 0x000632E4
		public VisualElement parent
		{
			get
			{
				return this.m_LogicalParent;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x000650FC File Offset: 0x000632FC
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x00065104 File Offset: 0x00063304
		internal BaseVisualElementPanel elementPanel
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get;
			private set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x00065110 File Offset: 0x00063310
		[CreateProperty(ReadOnly = true)]
		public IPanel panel
		{
			get
			{
				return this.elementPanel;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00065128 File Offset: 0x00063328
		public virtual VisualElement contentContainer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x0006513B File Offset: 0x0006333B
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x00065143 File Offset: 0x00063343
		[CreateProperty(ReadOnly = true)]
		public VisualTreeAsset visualTreeAssetSource
		{
			get
			{
				return this.m_VisualTreeAssetSource;
			}
			internal set
			{
				this.m_VisualTreeAssetSource = value;
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0006514C File Offset: 0x0006334C
		public void Add(VisualElement child)
		{
			bool flag = child == null;
			if (!flag)
			{
				VisualElement container = this.contentContainer;
				bool flag2 = container == null;
				if (flag2)
				{
					throw new InvalidOperationException("You can't add directly to this VisualElement. Use hierarchy.Add() if you know what you're doing.");
				}
				bool flag3 = container == this;
				if (flag3)
				{
					this.hierarchy.Add(child);
				}
				else if (container != null)
				{
					container.Add(child);
				}
				child.m_LogicalParent = this;
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000651B4 File Offset: 0x000633B4
		public void Insert(int index, VisualElement element)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = this.contentContainer == this;
				if (flag2)
				{
					this.hierarchy.Insert(index, element);
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					if (contentContainer != null)
					{
						contentContainer.Insert(index, element);
					}
				}
				element.m_LogicalParent = this;
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0006520C File Offset: 0x0006340C
		public void Remove(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Remove(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Remove(element);
				}
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00065250 File Offset: 0x00063450
		public void Clear()
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Clear();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Clear();
				}
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00065294 File Offset: 0x00063494
		public VisualElement ElementAt(int index)
		{
			return this[index];
		}

		// Token: 0x170006F0 RID: 1776
		public VisualElement this[int key]
		{
			get
			{
				bool flag = this.contentContainer == this;
				VisualElement visualElement;
				if (flag)
				{
					visualElement = this.hierarchy[key];
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					visualElement = ((contentContainer != null) ? contentContainer[key] : null);
				}
				return visualElement;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x000652F8 File Offset: 0x000634F8
		[CreateProperty(ReadOnly = true)]
		public int childCount
		{
			get
			{
				bool flag = this.contentContainer == this;
				int num;
				if (flag)
				{
					num = this.hierarchy.childCount;
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					num = ((contentContainer != null) ? contentContainer.childCount : 0);
				}
				return num;
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0006533C File Offset: 0x0006353C
		public int IndexOf(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			int num;
			if (flag)
			{
				num = this.hierarchy.IndexOf(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				num = ((contentContainer != null) ? contentContainer.IndexOf(element) : (-1));
			}
			return num;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00065384 File Offset: 0x00063584
		internal VisualElement ElementAtTreePath(List<int> childIndexes)
		{
			VisualElement child = this;
			foreach (int index in childIndexes)
			{
				bool flag = index >= 0 && index < child.hierarchy.childCount;
				if (!flag)
				{
					return null;
				}
				child = child.hierarchy[index];
			}
			return child;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00065414 File Offset: 0x00063614
		internal bool FindElementInTree(VisualElement element, List<int> outChildIndexes)
		{
			VisualElement child = element;
			for (VisualElement hierarchyParent = child.hierarchy.parent; hierarchyParent != null; hierarchyParent = hierarchyParent.hierarchy.parent)
			{
				outChildIndexes.Insert(0, hierarchyParent.hierarchy.IndexOf(child));
				bool flag = hierarchyParent == this;
				if (flag)
				{
					return true;
				}
				child = hierarchyParent;
			}
			outChildIndexes.Clear();
			return false;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00065488 File Offset: 0x00063688
		public IEnumerable<VisualElement> Children()
		{
			bool flag = this.contentContainer == this;
			IEnumerable<VisualElement> enumerable;
			if (flag)
			{
				enumerable = this.hierarchy.Children();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				enumerable = ((contentContainer != null) ? contentContainer.Children() : null) ?? VisualElement.s_EmptyList;
			}
			return enumerable;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000654D4 File Offset: 0x000636D4
		public void BringToFront()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.BringToFront(this);
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00065518 File Offset: 0x00063718
		public void SendToBack()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.SendToBack(this);
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0006555C File Offset: 0x0006375C
		public void PlaceBehind(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceBehind(this, sibling);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000655E0 File Offset: 0x000637E0
		public void RemoveFromHierarchy()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.hierarchy.Remove(this);
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00065624 File Offset: 0x00063824
		public T GetFirstOfType<T>() where T : class
		{
			T casted = this as T;
			bool flag = casted != null;
			T t;
			if (flag)
			{
				t = casted;
			}
			else
			{
				t = this.GetFirstAncestorOfType<T>();
			}
			return t;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0006565C File Offset: 0x0006385C
		public T GetFirstAncestorOfType<T>() where T : class
		{
			for (VisualElement ancestor = this.hierarchy.parent; ancestor != null; ancestor = ancestor.hierarchy.parent)
			{
				T castedAncestor = ancestor as T;
				bool flag = castedAncestor != null;
				if (flag)
				{
					return castedAncestor;
				}
			}
			return default(T);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000656C8 File Offset: 0x000638C8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement GetFirstAncestorWhere(Predicate<VisualElement> predicate)
		{
			for (VisualElement ancestor = this.hierarchy.parent; ancestor != null; ancestor = ancestor.hierarchy.parent)
			{
				bool flag = predicate(ancestor);
				if (flag)
				{
					return ancestor;
				}
			}
			return null;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00065718 File Offset: 0x00063918
		public bool Contains(VisualElement child)
		{
			while (child != null)
			{
				bool flag = child.hierarchy.parent == this;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00065764 File Offset: 0x00063964
		private void GatherAllChildren(List<VisualElement> elements)
		{
			bool flag = this.m_Children.Count > 0;
			if (flag)
			{
				int startIndex = elements.Count;
				elements.AddRange(this.m_Children);
				while (startIndex < elements.Count)
				{
					VisualElement current = elements[startIndex];
					elements.AddRange(current.m_Children);
					startIndex++;
				}
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000657C4 File Offset: 0x000639C4
		public VisualElement FindCommonAncestor(VisualElement other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentNullException("other");
			}
			bool flag2 = this.panel != other.panel;
			VisualElement visualElement;
			if (flag2)
			{
				visualElement = null;
			}
			else
			{
				VisualElement thisSide = this;
				int thisDepth = 0;
				while (thisSide != null)
				{
					thisDepth++;
					thisSide = thisSide.hierarchy.parent;
				}
				VisualElement otherSide = other;
				int otherDepth = 0;
				while (otherSide != null)
				{
					otherDepth++;
					otherSide = otherSide.hierarchy.parent;
				}
				thisSide = this;
				otherSide = other;
				while (thisDepth > otherDepth)
				{
					thisDepth--;
					thisSide = thisSide.hierarchy.parent;
				}
				while (otherDepth > thisDepth)
				{
					otherDepth--;
					otherSide = otherSide.hierarchy.parent;
				}
				while (thisSide != otherSide)
				{
					thisSide = thisSide.hierarchy.parent;
					otherSide = otherSide.hierarchy.parent;
				}
				visualElement = thisSide;
			}
			return visualElement;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x000658D4 File Offset: 0x00063AD4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement GetRoot()
		{
			bool flag = this.panel != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.panel.visualTree;
			}
			else
			{
				VisualElement root = this;
				while (root.m_PhysicalParent != null)
				{
					root = root.m_PhysicalParent;
				}
				visualElement = root;
			}
			return visualElement;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00065920 File Offset: 0x00063B20
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement GetRootVisualContainer()
		{
			VisualElement topMostRootContainer = null;
			for (VisualElement hierarchyParent = this; hierarchyParent != null; hierarchyParent = hierarchyParent.hierarchy.parent)
			{
				bool isRootVisualContainer = hierarchyParent.isRootVisualContainer;
				if (isRootVisualContainer)
				{
					topMostRootContainer = hierarchyParent;
				}
			}
			return topMostRootContainer;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00065964 File Offset: 0x00063B64
		internal VisualElement GetNextElementDepthFirst()
		{
			bool flag = this.m_Children.Count > 0;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.m_Children[0];
			}
			else
			{
				VisualElement p = this.m_PhysicalParent;
				VisualElement c = this;
				while (p != null)
				{
					int i;
					for (i = 0; i < p.m_Children.Count; i++)
					{
						bool flag2 = p.m_Children[i] == c;
						if (flag2)
						{
							break;
						}
					}
					bool flag3 = i < p.m_Children.Count - 1;
					if (flag3)
					{
						return p.m_Children[i + 1];
					}
					c = p;
					p = p.m_PhysicalParent;
				}
				visualElement = null;
			}
			return visualElement;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00065A24 File Offset: 0x00063C24
		internal VisualElement GetPreviousElementDepthFirst()
		{
			bool flag = this.m_PhysicalParent != null;
			VisualElement visualElement;
			if (flag)
			{
				int i;
				for (i = 0; i < this.m_PhysicalParent.m_Children.Count; i++)
				{
					bool flag2 = this.m_PhysicalParent.m_Children[i] == this;
					if (flag2)
					{
						break;
					}
				}
				bool flag3 = i > 0;
				if (flag3)
				{
					VisualElement p = this.m_PhysicalParent.m_Children[i - 1];
					while (p.m_Children.Count > 0)
					{
						p = p.m_Children[p.m_Children.Count - 1];
					}
					visualElement = p;
				}
				else
				{
					visualElement = this.m_PhysicalParent;
				}
			}
			else
			{
				visualElement = null;
			}
			return visualElement;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00065AEC File Offset: 0x00063CEC
		internal VisualElement RetargetElement(VisualElement retargetAgainst)
		{
			bool flag = retargetAgainst == null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this;
			}
			else
			{
				VisualElement retargetRoot = retargetAgainst.m_PhysicalParent ?? retargetAgainst;
				while (retargetRoot.m_PhysicalParent != null && !retargetRoot.isCompositeRoot)
				{
					retargetRoot = retargetRoot.m_PhysicalParent;
				}
				VisualElement retargetCandidate = this;
				VisualElement p = this.m_PhysicalParent;
				while (p != null)
				{
					p = p.m_PhysicalParent;
					bool flag2 = p == retargetRoot;
					if (flag2)
					{
						return retargetCandidate;
					}
					bool flag3 = p != null && p.isCompositeRoot;
					if (flag3)
					{
						retargetCandidate = p;
					}
				}
				visualElement = this;
			}
			return visualElement;
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00065B84 File Offset: 0x00063D84
		private Vector3 positionWithLayout
		{
			get
			{
				return this.ResolveTranslate() + this.layout.min;
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00065BB4 File Offset: 0x00063DB4
		internal void GetPivotedMatrixWithLayout(out Matrix4x4 result)
		{
			Vector3 transformOrigin = this.ResolveTransformOrigin();
			result = Matrix4x4.TRS(this.positionWithLayout + transformOrigin, this.ResolveRotation(), this.ResolveScale());
			VisualElement.TranslateMatrix34InPlace(ref result, -transformOrigin);
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00065BFC File Offset: 0x00063DFC
		internal bool hasDefaultRotationAndScale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.computedStyle.rotate.angle.value == 0f && this.computedStyle.scale.value == Vector3.one;
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00065C50 File Offset: 0x00063E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float Min(float a, float b, float c, float d)
		{
			return Mathf.Min(Mathf.Min(a, b), Mathf.Min(c, d));
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00065C78 File Offset: 0x00063E78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float Max(float a, float b, float c, float d)
		{
			return Mathf.Max(Mathf.Max(a, b), Mathf.Max(c, d));
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00065CA0 File Offset: 0x00063EA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void TransformAlignedRectToParentSpace(ref Rect rect)
		{
			bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
			if (hasDefaultRotationAndScale)
			{
				rect.position += this.positionWithLayout;
			}
			else
			{
				Matrix4x4 i;
				this.GetPivotedMatrixWithLayout(out i);
				rect = VisualElement.CalculateConservativeRect(ref i, rect);
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00065CF8 File Offset: 0x00063EF8
		internal static Rect CalculateConservativeRect(ref Matrix4x4 matrix, Rect rect)
		{
			bool flag = float.IsNaN(rect.height) | float.IsNaN(rect.width) | float.IsNaN(rect.x) | float.IsNaN(rect.y);
			Rect rect2;
			if (flag)
			{
				rect = new Rect(VisualElement.MultiplyMatrix44Point2(ref matrix, rect.position), VisualElement.MultiplyVector2(ref matrix, rect.size));
				VisualElement.OrderMinMaxRect(ref rect);
				rect2 = rect;
			}
			else
			{
				Vector2 topLeft = new Vector2(rect.xMin, rect.yMin);
				Vector2 bottomRight = new Vector2(rect.xMax, rect.yMax);
				Vector2 topRight = new Vector2(rect.xMax, rect.yMin);
				Vector2 bottomLeft = new Vector2(rect.xMin, rect.yMax);
				Vector3 transformedTL = matrix.MultiplyPoint3x4(topLeft);
				Vector3 transformedBR = matrix.MultiplyPoint3x4(bottomRight);
				Vector3 transformedRL = matrix.MultiplyPoint3x4(topRight);
				Vector3 transformedBL = matrix.MultiplyPoint3x4(bottomLeft);
				Vector2 min = new Vector2(VisualElement.Min(transformedTL.x, transformedBR.x, transformedRL.x, transformedBL.x), VisualElement.Min(transformedTL.y, transformedBR.y, transformedRL.y, transformedBL.y));
				Vector2 max = new Vector2(VisualElement.Max(transformedTL.x, transformedBR.x, transformedRL.x, transformedBL.x), VisualElement.Max(transformedTL.y, transformedBR.y, transformedRL.y, transformedBL.y));
				rect2 = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
			}
			return rect2;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00065EC6 File Offset: 0x000640C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void TransformAlignedRect(ref Matrix4x4 matrix, ref Rect rect)
		{
			rect = VisualElement.CalculateConservativeRect(ref matrix, rect);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00065EDC File Offset: 0x000640DC
		internal static void OrderMinMaxRect(ref Rect rect)
		{
			bool flag = rect.width < 0f;
			if (flag)
			{
				rect.x += rect.width;
				rect.width = -rect.width;
			}
			bool flag2 = rect.height < 0f;
			if (flag2)
			{
				rect.y += rect.height;
				rect.height = -rect.height;
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00065F54 File Offset: 0x00064154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MultiplyMatrix44Point2(ref Matrix4x4 lhs, Vector2 point)
		{
			Vector2 res;
			res.x = lhs.m00 * point.x + lhs.m01 * point.y + lhs.m03;
			res.y = lhs.m10 * point.x + lhs.m11 * point.y + lhs.m13;
			return res;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00065FBC File Offset: 0x000641BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MultiplyVector2(ref Matrix4x4 lhs, Vector2 vector)
		{
			Vector2 res;
			res.x = lhs.m00 * vector.x + lhs.m01 * vector.y;
			res.y = lhs.m10 * vector.x + lhs.m11 * vector.y;
			return res;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00066014 File Offset: 0x00064214
		internal static void MultiplyMatrix34(ref Matrix4x4 lhs, ref Matrix4x4 rhs, out Matrix4x4 res)
		{
			res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
			res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
			res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22;
			res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03;
			res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20;
			res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21;
			res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22;
			res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13;
			res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20;
			res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21;
			res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22;
			res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23;
			res.m30 = 0f;
			res.m31 = 0f;
			res.m32 = 0f;
			res.m33 = 1f;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00066297 File Offset: 0x00064497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TranslateMatrix34(ref Matrix4x4 lhs, Vector3 rhs, out Matrix4x4 res)
		{
			res = lhs;
			VisualElement.TranslateMatrix34InPlace(ref res, rhs);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000662B0 File Offset: 0x000644B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TranslateMatrix34InPlace(ref Matrix4x4 lhs, Vector3 rhs)
		{
			lhs.m03 += lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z;
			lhs.m13 += lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z;
			lhs.m23 += lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x00066358 File Offset: 0x00064558
		public IVisualElementScheduler schedule
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0006636C File Offset: 0x0006456C
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action<TimerState> timerUpdateEvent)
		{
			VisualElement.TimerStateScheduledItem item = new VisualElement.TimerStateScheduledItem(this, timerUpdateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			item.Resume();
			return item;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0006639C File Offset: 0x0006459C
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action updateEvent)
		{
			VisualElement.SimpleScheduledItem item = new VisualElement.SimpleScheduledItem(this, updateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			item.Resume();
			return item;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x000663CC File Offset: 0x000645CC
		[CreateProperty]
		public IStyle style
		{
			get
			{
				bool flag = this.inlineStyleAccess == null;
				if (flag)
				{
					this.inlineStyleAccess = new InlineStyleAccess(this);
				}
				return this.inlineStyleAccess;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00066400 File Offset: 0x00064600
		[CreateProperty]
		public IResolvedStyle resolvedStyle
		{
			get
			{
				bool flag = this.resolvedStyleAccess == null;
				if (flag)
				{
					this.resolvedStyleAccess = new ResolvedStyleAccess(this);
				}
				return this.resolvedStyleAccess;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00066434 File Offset: 0x00064634
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement.s_CustomStyleAccess.SetContext(this.computedStyle.customProperties, this.computedStyle.dpiScaling);
				return VisualElement.s_CustomStyleAccess;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0006646C File Offset: 0x0006466C
		[CreateProperty(ReadOnly = true)]
		public VisualElementStyleSheetSet styleSheets
		{
			get
			{
				return new VisualElementStyleSheetSet(this);
			}
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00066474 File Offset: 0x00064674
		internal void AddStyleSheetPath(string sheetPath)
		{
			StyleSheet sheetAsset = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint_noChecks) as StyleSheet;
			bool flag = sheetAsset == null;
			if (flag)
			{
				bool flag2 = !VisualElement.s_InternalStyleSheetPath.IsMatch(sheetPath);
				if (flag2)
				{
					Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				}
			}
			else
			{
				this.styleSheets.Add(sheetAsset);
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000664E4 File Offset: 0x000646E4
		internal StyleFloat ResolveLengthValue(Length length, bool isRow)
		{
			bool flag = length.IsAuto();
			StyleFloat styleFloat;
			if (flag)
			{
				styleFloat = new StyleFloat(StyleKeyword.Auto);
			}
			else
			{
				bool flag2 = length.IsNone();
				if (flag2)
				{
					styleFloat = new StyleFloat(StyleKeyword.None);
				}
				else
				{
					bool flag3 = length.unit != LengthUnit.Percent;
					if (flag3)
					{
						styleFloat = new StyleFloat(length.value);
					}
					else
					{
						VisualElement parent = this.hierarchy.parent;
						bool flag4 = parent == null;
						if (flag4)
						{
							styleFloat = 0f;
						}
						else
						{
							float parentSize = (isRow ? parent.resolvedStyle.width : parent.resolvedStyle.height);
							styleFloat = length.value * parentSize / 100f;
						}
					}
				}
			}
			return styleFloat;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0006659C File Offset: 0x0006479C
		internal Vector3 ResolveTranslate()
		{
			Translate translationOperation = this.computedStyle.translate;
			Length x_cache = translationOperation.x;
			bool flag = x_cache.unit == LengthUnit.Percent;
			float x;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				x = (float.IsNaN(width) ? 0f : (width * x_cache.value / 100f));
			}
			else
			{
				x = x_cache.value;
				x = (float.IsNaN(x) ? 0f : x);
			}
			Length y_cache = translationOperation.y;
			bool flag2 = y_cache.unit == LengthUnit.Percent;
			float y;
			if (flag2)
			{
				float height = this.resolvedStyle.height;
				y = (float.IsNaN(height) ? 0f : (height * y_cache.value / 100f));
			}
			else
			{
				y = y_cache.value;
				y = (float.IsNaN(y) ? 0f : y);
			}
			float z = translationOperation.z;
			z = (float.IsNaN(z) ? 0f : z);
			return new Vector3(x, y, z);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000666B0 File Offset: 0x000648B0
		internal Vector3 ResolveTransformOrigin()
		{
			TransformOrigin transformOrigin = this.computedStyle.transformOrigin;
			Length x_cache = transformOrigin.x;
			bool flag = x_cache.IsNone();
			float x;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				x = (float.IsNaN(width) ? 0f : (width / 2f));
			}
			else
			{
				bool flag2 = x_cache.unit == LengthUnit.Percent;
				if (flag2)
				{
					float width2 = this.resolvedStyle.width;
					x = (float.IsNaN(width2) ? 0f : (width2 * x_cache.value / 100f));
				}
				else
				{
					x = x_cache.value;
				}
			}
			Length y_cache = transformOrigin.y;
			bool flag3 = y_cache.IsNone();
			float y;
			if (flag3)
			{
				float height = this.resolvedStyle.height;
				y = (float.IsNaN(height) ? 0f : (height / 2f));
			}
			else
			{
				bool flag4 = y_cache.unit == LengthUnit.Percent;
				if (flag4)
				{
					float height2 = this.resolvedStyle.height;
					y = (float.IsNaN(height2) ? 0f : (height2 * y_cache.value / 100f));
				}
				else
				{
					y = y_cache.value;
				}
			}
			float z = transformOrigin.z;
			return new Vector3(x, y, z);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00066808 File Offset: 0x00064A08
		private Quaternion ResolveRotation()
		{
			Rotate rotate = this.computedStyle.rotate;
			Vector3 axis = rotate.axis;
			bool flag = float.IsNaN(rotate.angle.value) || float.IsNaN(axis.x) || float.IsNaN(axis.y) || float.IsNaN(axis.z);
			if (flag)
			{
				rotate = Rotate.Initial();
			}
			return rotate.ToQuaternion();
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00066880 File Offset: 0x00064A80
		private Vector3 ResolveScale()
		{
			Vector3 s = this.computedStyle.scale.value;
			BaseVisualElementPanel elementPanel = this.elementPanel;
			bool flag = elementPanel != null && elementPanel.isFlat;
			if (flag)
			{
				s.z = 1f;
			}
			return (float.IsNaN(s.x) || float.IsNaN(s.y) || float.IsNaN(s.z)) ? Vector3.one : s;
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000668FC File Offset: 0x00064AFC
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x0006692C File Offset: 0x00064B2C
		[CreateProperty]
		public string tooltip
		{
			get
			{
				string tooltipText = this.GetProperty(VisualElement.tooltipPropertyKey) as string;
				return tooltipText ?? string.Empty;
			}
			set
			{
				bool flag = !this.HasProperty(VisualElement.tooltipPropertyKey);
				if (flag)
				{
					bool flag2 = string.IsNullOrEmpty(value);
					if (flag2)
					{
						return;
					}
					base.RegisterCallback<TooltipEvent>(new EventCallback<TooltipEvent>(this.SetTooltip), TrickleDown.NoTrickleDown);
				}
				string tooltipText = this.GetProperty(VisualElement.tooltipPropertyKey) as string;
				bool flag3 = string.CompareOrdinal(tooltipText, value) == 0;
				if (!flag3)
				{
					this.SetProperty(VisualElement.tooltipPropertyKey, value);
					base.NotifyPropertyChanged(in VisualElement.tooltipProperty);
				}
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x000669AC File Offset: 0x00064BAC
		private VisualElement.TypeData typeData
		{
			get
			{
				bool flag = this.m_TypeData == null;
				if (flag)
				{
					Type type = base.GetType();
					bool flag2 = !VisualElement.s_TypeData.TryGetValue(type, out this.m_TypeData);
					if (flag2)
					{
						this.m_TypeData = new VisualElement.TypeData(type);
						VisualElement.s_TypeData.Add(type, this.m_TypeData);
					}
				}
				return this.m_TypeData;
			}
		}

		// Token: 0x04000B83 RID: 2947
		private static uint s_NextId;

		// Token: 0x04000B84 RID: 2948
		private static List<string> s_EmptyClassList = new List<string>(0);

		// Token: 0x04000B85 RID: 2949
		internal static readonly PropertyName userDataPropertyKey = new PropertyName("--unity-user-data");

		// Token: 0x04000B86 RID: 2950
		public static readonly string disabledUssClassName = "unity-disabled";

		// Token: 0x04000B87 RID: 2951
		private string m_Name;

		// Token: 0x04000B88 RID: 2952
		private List<string> m_ClassList;

		// Token: 0x04000B89 RID: 2953
		private Dictionary<PropertyName, object> m_PropertyBag;

		// Token: 0x04000B8A RID: 2954
		internal VisualElementFlags m_Flags;

		// Token: 0x04000B8B RID: 2955
		private string m_ViewDataKey;

		// Token: 0x04000B8C RID: 2956
		private RenderHints m_RenderHints;

		// Token: 0x04000B8D RID: 2957
		internal Rect lastLayout;

		// Token: 0x04000B8E RID: 2958
		internal Rect lastPseudoPadding;

		// Token: 0x04000B8F RID: 2959
		internal RenderChainVEData renderChainData;

		// Token: 0x04000B90 RID: 2960
		internal bool shouldCutRenderChain;

		// Token: 0x04000B91 RID: 2961
		internal UIRenderer uiRenderer;

		// Token: 0x04000B92 RID: 2962
		private Rect m_Layout;

		// Token: 0x04000B93 RID: 2963
		private Rect m_BoundingBox;

		// Token: 0x04000B94 RID: 2964
		private const VisualElementFlags worldBoundingBoxDirtyDependencies = VisualElementFlags.WorldTransformDirty | VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty;

		// Token: 0x04000B95 RID: 2965
		private Rect m_WorldBoundingBox;

		// Token: 0x04000B96 RID: 2966
		private const VisualElementFlags worldTransformInverseDirtyDependencies = VisualElementFlags.WorldTransformDirty | VisualElementFlags.WorldTransformInverseDirty;

		// Token: 0x04000B97 RID: 2967
		private Matrix4x4 m_WorldTransformCache = Matrix4x4.identity;

		// Token: 0x04000B98 RID: 2968
		private Matrix4x4 m_WorldTransformInverseCache = Matrix4x4.identity;

		// Token: 0x04000B99 RID: 2969
		private Rect m_WorldClip = Rect.zero;

		// Token: 0x04000B9A RID: 2970
		private Rect m_WorldClipMinusGroup = Rect.zero;

		// Token: 0x04000B9B RID: 2971
		private bool m_WorldClipIsInfinite = false;

		// Token: 0x04000B9C RID: 2972
		internal static readonly Rect s_InfiniteRect = new Rect(-10000f, -10000f, 40000f, 40000f);

		// Token: 0x04000B9D RID: 2973
		internal PseudoStates triggerPseudoMask;

		// Token: 0x04000B9E RID: 2974
		internal PseudoStates dependencyPseudoMask;

		// Token: 0x04000B9F RID: 2975
		private PseudoStates m_PseudoStates;

		// Token: 0x04000BA1 RID: 2977
		private PickingMode m_PickingMode;

		// Token: 0x04000BA2 RID: 2978
		private LayoutNode m_LayoutNode;

		// Token: 0x04000BA3 RID: 2979
		internal ComputedStyle m_Style = InitialStyle.Acquire();

		// Token: 0x04000BA4 RID: 2980
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleVariableContext variableContext = StyleVariableContext.none;

		// Token: 0x04000BA5 RID: 2981
		internal int inheritedStylesHash = 0;

		// Token: 0x04000BA6 RID: 2982
		internal readonly uint controlid;

		// Token: 0x04000BA7 RID: 2983
		internal int imguiContainerDescendantCount = 0;

		// Token: 0x04000BA8 RID: 2984
		private bool m_EnabledSelf;

		// Token: 0x04000BA9 RID: 2985
		private LanguageDirection m_LanguageDirection;

		// Token: 0x04000BAA RID: 2986
		private LanguageDirection m_LocalLanguageDirection;

		// Token: 0x04000BAC RID: 2988
		private static readonly ProfilerMarker k_GenerateVisualContentMarker = new ProfilerMarker("GenerateVisualContent");

		// Token: 0x04000BAD RID: 2989
		private VisualElement.RenderTargetMode m_SubRenderTargetMode = VisualElement.RenderTargetMode.None;

		// Token: 0x04000BAE RID: 2990
		private static Material s_runtimeMaterial;

		// Token: 0x04000BAF RID: 2991
		private Material m_defaultMaterial;

		// Token: 0x04000BB0 RID: 2992
		private List<IValueAnimationUpdate> m_RunningAnimations;

		// Token: 0x04000BB1 RID: 2993
		internal static readonly BindingId childCountProperty = "childCount";

		// Token: 0x04000BB2 RID: 2994
		internal static readonly BindingId contentRectProperty = "contentRect";

		// Token: 0x04000BB3 RID: 2995
		internal static readonly BindingId dataSourcePathProperty = "dataSourcePath";

		// Token: 0x04000BB4 RID: 2996
		internal static readonly BindingId dataSourceProperty = "dataSource";

		// Token: 0x04000BB5 RID: 2997
		internal static readonly BindingId disablePlayModeTintProperty = "disablePlayModeTint";

		// Token: 0x04000BB6 RID: 2998
		internal static readonly BindingId enabledInHierarchyProperty = "enabledInHierarchy";

		// Token: 0x04000BB7 RID: 2999
		internal static readonly BindingId enabledSelfProperty = "enabledSelf";

		// Token: 0x04000BB8 RID: 3000
		internal static readonly BindingId layoutProperty = "layout";

		// Token: 0x04000BB9 RID: 3001
		internal static readonly BindingId languageDirectionProperty = "languageDirection";

		// Token: 0x04000BBA RID: 3002
		internal static readonly BindingId localBoundProperty = "localBound";

		// Token: 0x04000BBB RID: 3003
		internal static readonly BindingId nameProperty = "name";

		// Token: 0x04000BBC RID: 3004
		internal static readonly BindingId panelProperty = "panel";

		// Token: 0x04000BBD RID: 3005
		internal static readonly BindingId pickingModeProperty = "pickingMode";

		// Token: 0x04000BBE RID: 3006
		internal static readonly BindingId styleSheetsProperty = "styleSheets";

		// Token: 0x04000BBF RID: 3007
		internal static readonly BindingId tooltipProperty = "tooltip";

		// Token: 0x04000BC0 RID: 3008
		internal static readonly BindingId usageHintsProperty = "usageHints";

		// Token: 0x04000BC1 RID: 3009
		internal static readonly BindingId userDataProperty = "userData";

		// Token: 0x04000BC2 RID: 3010
		internal static readonly BindingId viewDataKeyProperty = "viewDataKey";

		// Token: 0x04000BC3 RID: 3011
		internal static readonly BindingId visibleProperty = "visible";

		// Token: 0x04000BC4 RID: 3012
		internal static readonly BindingId visualTreeAssetSourceProperty = "visualTreeAssetSource";

		// Token: 0x04000BC5 RID: 3013
		internal static readonly BindingId worldBoundProperty = "worldBound";

		// Token: 0x04000BC6 RID: 3014
		internal static readonly BindingId worldTransformProperty = "worldTransform";

		// Token: 0x04000BC7 RID: 3015
		private object m_DataSource;

		// Token: 0x04000BC8 RID: 3016
		private PropertyPath m_DataSourcePath;

		// Token: 0x04000BC9 RID: 3017
		private List<Binding> m_Bindings;

		// Token: 0x04000BCB RID: 3019
		private readonly int m_TrickleDownHandleEventCategories;

		// Token: 0x04000BCC RID: 3020
		private readonly int m_BubbleUpHandleEventCategories;

		// Token: 0x04000BCD RID: 3021
		private int m_BubbleUpEventCallbackCategories = 0;

		// Token: 0x04000BCE RID: 3022
		private int m_TrickleDownEventCallbackCategories = 0;

		// Token: 0x04000BCF RID: 3023
		private int m_EventInterestSelfCategories = 0;

		// Token: 0x04000BD0 RID: 3024
		private int m_CachedEventInterestParentCategories = 0;

		// Token: 0x04000BD1 RID: 3025
		private static uint s_NextParentVersion;

		// Token: 0x04000BD2 RID: 3026
		private uint m_NextParentCachedVersion;

		// Token: 0x04000BD3 RID: 3027
		private uint m_NextParentRequiredVersion;

		// Token: 0x04000BD4 RID: 3028
		private VisualElement m_CachedNextParentWithEventInterests;

		// Token: 0x04000BD5 RID: 3029
		internal const string k_RootVisualContainerName = "rootVisualContainer";

		// Token: 0x04000BD9 RID: 3033
		private VisualElement m_PhysicalParent;

		// Token: 0x04000BDA RID: 3034
		private VisualElement m_LogicalParent;

		// Token: 0x04000BDD RID: 3037
		private static readonly List<VisualElement> s_EmptyList = new List<VisualElement>();

		// Token: 0x04000BDE RID: 3038
		private List<VisualElement> m_Children;

		// Token: 0x04000BE0 RID: 3040
		private VisualTreeAsset m_VisualTreeAssetSource = null;

		// Token: 0x04000BE1 RID: 3041
		internal static VisualElement.CustomStyleAccess s_CustomStyleAccess = new VisualElement.CustomStyleAccess();

		// Token: 0x04000BE2 RID: 3042
		internal InlineStyleAccess inlineStyleAccess;

		// Token: 0x04000BE3 RID: 3043
		internal ResolvedStyleAccess resolvedStyleAccess;

		// Token: 0x04000BE4 RID: 3044
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal List<StyleSheet> styleSheetList;

		// Token: 0x04000BE5 RID: 3045
		private static readonly Regex s_InternalStyleSheetPath = new Regex("^instanceId:[-0-9]+$", RegexOptions.Compiled);

		// Token: 0x04000BE6 RID: 3046
		internal static readonly PropertyName tooltipPropertyKey = new PropertyName("--unity-tooltip");

		// Token: 0x04000BE7 RID: 3047
		private static readonly Dictionary<Type, VisualElement.TypeData> s_TypeData = new Dictionary<Type, VisualElement.TypeData>();

		// Token: 0x04000BE8 RID: 3048
		private VisualElement.TypeData m_TypeData;

		// Token: 0x02000346 RID: 838
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public class UxmlFactory : UxmlFactory<VisualElement, VisualElement.UxmlTraits>
		{
		}

		// Token: 0x02000347 RID: 839
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public class UxmlTraits : UnityEngine.UIElements.UxmlTraits
		{
			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x060019BF RID: 6591 RVA: 0x00066C02 File Offset: 0x00064E02
			protected UxmlIntAttributeDescription focusIndex { get; } = new UxmlIntAttributeDescription
			{
				name = null,
				obsoleteNames = new string[] { "focus-index", "focusIndex" },
				defaultValue = -1
			};

			// Token: 0x170006FC RID: 1788
			// (get) Token: 0x060019C0 RID: 6592 RVA: 0x00066C0A File Offset: 0x00064E0A
			protected UxmlBoolAttributeDescription focusable { get; } = new UxmlBoolAttributeDescription
			{
				name = "focusable",
				defaultValue = false
			};

			// Token: 0x060019C1 RID: 6593 RVA: 0x00066C14 File Offset: 0x00064E14
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				bool flag = ve == null;
				if (flag)
				{
					throw new ArgumentNullException("ve");
				}
				ve.name = this.m_Name.GetValueFromBag(bag, cc);
				ve.enabledSelf = this.m_EnabledSelf.GetValueFromBag(bag, cc);
				ve.viewDataKey = this.m_ViewDataKey.GetValueFromBag(bag, cc);
				ve.pickingMode = this.m_PickingMode.GetValueFromBag(bag, cc);
				ve.usageHints = this.m_UsageHints.GetValueFromBag(bag, cc);
				ve.tooltip = this.m_Tooltip.GetValueFromBag(bag, cc);
				int index = 0;
				bool flag2 = this.focusIndex.TryGetValueFromBag(bag, cc, ref index);
				if (flag2)
				{
					ve.tabIndex = ((index >= 0) ? index : 0);
					ve.focusable = index >= 0;
				}
				ve.tabIndex = this.m_TabIndex.GetValueFromBag(bag, cc);
				ve.focusable = this.focusable.GetValueFromBag(bag, cc);
				ve.dataSource = this.m_DataSource.GetValueFromBag(bag, cc);
				ve.dataSourcePath = new PropertyPath(this.m_DataSourcePath.GetValueFromBag(bag, cc));
			}

			// Token: 0x04000BE9 RID: 3049
			protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			// Token: 0x04000BEA RID: 3050
			private UxmlBoolAttributeDescription m_EnabledSelf = new UxmlBoolAttributeDescription
			{
				name = "enabled",
				defaultValue = true
			};

			// Token: 0x04000BEB RID: 3051
			private UxmlStringAttributeDescription m_ViewDataKey = new UxmlStringAttributeDescription
			{
				name = "view-data-key"
			};

			// Token: 0x04000BEC RID: 3052
			protected UxmlEnumAttributeDescription<PickingMode> m_PickingMode = new UxmlEnumAttributeDescription<PickingMode>
			{
				name = "picking-mode",
				obsoleteNames = new string[] { "pickingMode" }
			};

			// Token: 0x04000BED RID: 3053
			private UxmlStringAttributeDescription m_Tooltip = new UxmlStringAttributeDescription
			{
				name = "tooltip"
			};

			// Token: 0x04000BEE RID: 3054
			private UxmlEnumAttributeDescription<UsageHints> m_UsageHints = new UxmlEnumAttributeDescription<UsageHints>
			{
				name = "usage-hints"
			};

			// Token: 0x04000BF0 RID: 3056
			private UxmlIntAttributeDescription m_TabIndex = new UxmlIntAttributeDescription
			{
				name = "tabindex",
				defaultValue = 0
			};

			// Token: 0x04000BF2 RID: 3058
			private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
			{
				name = "class"
			};

			// Token: 0x04000BF3 RID: 3059
			private UxmlStringAttributeDescription m_ContentContainer = new UxmlStringAttributeDescription
			{
				name = "content-container",
				obsoleteNames = new string[] { "contentContainer" }
			};

			// Token: 0x04000BF4 RID: 3060
			private UxmlStringAttributeDescription m_Style = new UxmlStringAttributeDescription
			{
				name = "style"
			};

			// Token: 0x04000BF5 RID: 3061
			private UxmlAssetAttributeDescription<Object> m_DataSource = new UxmlAssetAttributeDescription<Object>
			{
				name = "data-source"
			};

			// Token: 0x04000BF6 RID: 3062
			private UxmlStringAttributeDescription m_DataSourcePath = new UxmlStringAttributeDescription
			{
				name = "data-source-path"
			};
		}

		// Token: 0x02000348 RID: 840
		public enum MeasureMode
		{
			// Token: 0x04000BF8 RID: 3064
			Undefined,
			// Token: 0x04000BF9 RID: 3065
			Exactly,
			// Token: 0x04000BFA RID: 3066
			AtMost
		}

		// Token: 0x02000349 RID: 841
		internal enum RenderTargetMode
		{
			// Token: 0x04000BFC RID: 3068
			None,
			// Token: 0x04000BFD RID: 3069
			NoColorConversion,
			// Token: 0x04000BFE RID: 3070
			LinearToGamma,
			// Token: 0x04000BFF RID: 3071
			GammaToLinear
		}

		// Token: 0x0200034A RID: 842
		public struct Hierarchy
		{
			// Token: 0x170006FD RID: 1789
			// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00066F04 File Offset: 0x00065104
			public VisualElement parent
			{
				get
				{
					return this.m_Owner.m_PhysicalParent;
				}
			}

			// Token: 0x170006FE RID: 1790
			// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00066F21 File Offset: 0x00065121
			internal List<VisualElement> children
			{
				get
				{
					return this.m_Owner.m_Children;
				}
			}

			// Token: 0x060019C5 RID: 6597 RVA: 0x00066F2E File Offset: 0x0006512E
			internal Hierarchy(VisualElement element)
			{
				this.m_Owner = element;
			}

			// Token: 0x060019C6 RID: 6598 RVA: 0x00066F38 File Offset: 0x00065138
			public void Add(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot add null child");
				}
				this.Insert(this.childCount, child);
			}

			// Token: 0x060019C7 RID: 6599 RVA: 0x00066F68 File Offset: 0x00065168
			public void Insert(int index, VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot insert null child");
				}
				bool flag2 = index > this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				bool flag3 = child == this.m_Owner;
				if (flag3)
				{
					throw new ArgumentException("Cannot insert element as its own child");
				}
				bool flag4 = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag4)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.RemoveFromHierarchy();
				bool flag5 = this.m_Owner.m_Children == VisualElement.s_EmptyList;
				if (flag5)
				{
					this.m_Owner.m_Children = VisualElementListPool.Get(0);
				}
				bool isMeasureDefined = this.m_Owner.layoutNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					this.m_Owner.RemoveMeasureFunction();
				}
				this.PutChildAtIndex(child, index);
				int imguiContainerCount = child.imguiContainerDescendantCount + (child.isIMGUIContainer ? 1 : 0);
				bool flag6 = imguiContainerCount > 0;
				if (flag6)
				{
					this.m_Owner.ChangeIMGUIContainerCount(imguiContainerCount);
				}
				child.hierarchy.SetParent(this.m_Owner);
				child.PropagateEnabledToChildren(this.m_Owner.enabledInHierarchy);
				bool flag7 = child.languageDirection == LanguageDirection.Inherit;
				if (flag7)
				{
					child.localLanguageDirection = this.m_Owner.localLanguageDirection;
				}
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				child.IncrementVersion(VersionChangeType.Hierarchy);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				Action<VisualElement> elementAdded = this.m_Owner.elementAdded;
				if (elementAdded != null)
				{
					elementAdded(child);
				}
			}

			// Token: 0x060019C8 RID: 6600 RVA: 0x00067100 File Offset: 0x00065300
			public void Remove(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot remove null child");
				}
				bool flag2 = child.hierarchy.parent != this.m_Owner;
				if (flag2)
				{
					throw new ArgumentException("This VisualElement is not my child");
				}
				int index = this.m_Owner.m_Children.IndexOf(child);
				this.RemoveAt(index);
			}

			// Token: 0x060019C9 RID: 6601 RVA: 0x00067164 File Offset: 0x00065364
			public void RemoveAt(int index)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = index < 0 || index >= this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				VisualElement child = this.m_Owner.m_Children[index];
				child.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(index);
				int imguiContainerCount = child.imguiContainerDescendantCount + (child.isIMGUIContainer ? 1 : 0);
				bool flag3 = imguiContainerCount > 0;
				if (flag3)
				{
					this.m_Owner.ChangeIMGUIContainerCount(-imguiContainerCount);
				}
				child.hierarchy.SetParent(null);
				bool flag4 = this.childCount == 0;
				if (flag4)
				{
					this.ReleaseChildList();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
				}
				BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
				if (elementPanel != null)
				{
					elementPanel.OnVersionChanged(child, VersionChangeType.Hierarchy);
				}
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				Action<VisualElement> elementRemoved = this.m_Owner.elementRemoved;
				if (elementRemoved != null)
				{
					elementRemoved(child);
				}
			}

			// Token: 0x060019CA RID: 6602 RVA: 0x000672A4 File Offset: 0x000654A4
			public void Clear()
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = this.childCount > 0;
				if (flag2)
				{
					List<VisualElement> elements = VisualElementListPool.Copy(this.m_Owner.m_Children);
					this.ReleaseChildList();
					this.m_Owner.layoutNode.Clear();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
					foreach (VisualElement e in elements)
					{
						e.InvokeHierarchyChanged(HierarchyChangeType.Remove);
						e.hierarchy.SetParent(null);
						e.m_LogicalParent = null;
						BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
						if (elementPanel != null)
						{
							elementPanel.OnVersionChanged(e, VersionChangeType.Hierarchy);
						}
						Action<VisualElement> elementRemoved = this.m_Owner.elementRemoved;
						if (elementRemoved != null)
						{
							elementRemoved(e);
						}
					}
					bool flag3 = this.m_Owner.imguiContainerDescendantCount > 0;
					if (flag3)
					{
						int totalChange = this.m_Owner.imguiContainerDescendantCount;
						bool isIMGUIContainer = this.m_Owner.isIMGUIContainer;
						if (isIMGUIContainer)
						{
							totalChange--;
						}
						this.m_Owner.ChangeIMGUIContainerCount(-totalChange);
					}
					VisualElementListPool.Release(elements);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x060019CB RID: 6603 RVA: 0x0006742C File Offset: 0x0006562C
			internal void BringToFront(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int index = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = index >= 0 && index < this.childCount - 1;
					if (flag2)
					{
						this.MoveChildElement(child, index, this.childCount);
					}
				}
			}

			// Token: 0x060019CC RID: 6604 RVA: 0x00067484 File Offset: 0x00065684
			internal void SendToBack(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int index = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = index > 0;
					if (flag2)
					{
						this.MoveChildElement(child, index, 0);
					}
				}
			}

			// Token: 0x060019CD RID: 6605 RVA: 0x000674C8 File Offset: 0x000656C8
			internal void PlaceBehind(VisualElement child, VisualElement over)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int currenIndex = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = currenIndex < 0;
					if (!flag2)
					{
						int nextIndex = this.m_Owner.m_Children.IndexOf(over);
						bool flag3 = nextIndex > 0 && currenIndex < nextIndex;
						if (flag3)
						{
							nextIndex--;
						}
						this.MoveChildElement(child, currenIndex, nextIndex);
					}
				}
			}

			// Token: 0x060019CE RID: 6606 RVA: 0x00067534 File Offset: 0x00065734
			private void MoveChildElement(VisualElement child, int currentIndex, int nextIndex)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(currentIndex);
				this.PutChildAtIndex(child, nextIndex);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x170006FF RID: 1791
			// (get) Token: 0x060019CF RID: 6607 RVA: 0x000675A0 File Offset: 0x000657A0
			public int childCount
			{
				get
				{
					return this.m_Owner.m_Children.Count;
				}
			}

			// Token: 0x17000700 RID: 1792
			public VisualElement this[int key]
			{
				get
				{
					return this.m_Owner.m_Children[key];
				}
			}

			// Token: 0x060019D1 RID: 6609 RVA: 0x000675E8 File Offset: 0x000657E8
			public int IndexOf(VisualElement element)
			{
				return this.m_Owner.m_Children.IndexOf(element);
			}

			// Token: 0x060019D2 RID: 6610 RVA: 0x0006760C File Offset: 0x0006580C
			public VisualElement ElementAt(int index)
			{
				return this[index];
			}

			// Token: 0x060019D3 RID: 6611 RVA: 0x00067628 File Offset: 0x00065828
			public IEnumerable<VisualElement> Children()
			{
				return this.m_Owner.m_Children;
			}

			// Token: 0x060019D4 RID: 6612 RVA: 0x00067645 File Offset: 0x00065845
			private void SetParent(VisualElement value)
			{
				this.m_Owner.m_PhysicalParent = value;
				this.m_Owner.m_LogicalParent = value;
				this.m_Owner.DirtyNextParentWithEventInterests();
				this.m_Owner.SetPanel((value != null) ? value.elementPanel : null);
			}

			// Token: 0x060019D5 RID: 6613 RVA: 0x00067684 File Offset: 0x00065884
			private unsafe void PutChildAtIndex(VisualElement child, int index)
			{
				bool flag = index >= this.childCount;
				if (flag)
				{
					this.m_Owner.m_Children.Add(child);
					this.m_Owner.layoutNode.Insert(this.m_Owner.layoutNode.Count, *child.layoutNode);
				}
				else
				{
					this.m_Owner.m_Children.Insert(index, child);
					this.m_Owner.layoutNode.Insert(index, *child.layoutNode);
				}
			}

			// Token: 0x060019D6 RID: 6614 RVA: 0x00067716 File Offset: 0x00065916
			private void RemoveChildAtIndex(int index)
			{
				this.m_Owner.m_Children.RemoveAt(index);
				this.m_Owner.layoutNode.RemoveAt(index);
			}

			// Token: 0x060019D7 RID: 6615 RVA: 0x00067740 File Offset: 0x00065940
			private void ReleaseChildList()
			{
				bool flag = this.m_Owner.m_Children != VisualElement.s_EmptyList;
				if (flag)
				{
					List<VisualElement> children = this.m_Owner.m_Children;
					this.m_Owner.m_Children = VisualElement.s_EmptyList;
					VisualElementListPool.Release(children);
				}
			}

			// Token: 0x060019D8 RID: 6616 RVA: 0x0006778C File Offset: 0x0006598C
			public bool Equals(VisualElement.Hierarchy other)
			{
				return other == this;
			}

			// Token: 0x060019D9 RID: 6617 RVA: 0x000677AC File Offset: 0x000659AC
			public override bool Equals(object obj)
			{
				bool flag = obj == null;
				return !flag && obj is VisualElement.Hierarchy && this.Equals((VisualElement.Hierarchy)obj);
			}

			// Token: 0x060019DA RID: 6618 RVA: 0x000677E4 File Offset: 0x000659E4
			public override int GetHashCode()
			{
				return (this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0;
			}

			// Token: 0x060019DB RID: 6619 RVA: 0x0006780C File Offset: 0x00065A0C
			public static bool operator ==(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return x.m_Owner == y.m_Owner;
			}

			// Token: 0x04000C00 RID: 3072
			private const string k_InvalidHierarchyChangeMsg = "Cannot modify VisualElement hierarchy during layout calculation";

			// Token: 0x04000C01 RID: 3073
			private readonly VisualElement m_Owner;
		}

		// Token: 0x0200034B RID: 843
		private abstract class BaseVisualElementScheduledItem : ScheduledItem, IVisualElementScheduledItem
		{
			// Token: 0x17000701 RID: 1793
			// (get) Token: 0x060019DC RID: 6620 RVA: 0x0006782C File Offset: 0x00065A2C
			// (set) Token: 0x060019DD RID: 6621 RVA: 0x00067834 File Offset: 0x00065A34
			public VisualElement element { get; private set; }

			// Token: 0x17000702 RID: 1794
			// (get) Token: 0x060019DE RID: 6622 RVA: 0x0006783D File Offset: 0x00065A3D
			// (set) Token: 0x060019DF RID: 6623 RVA: 0x00067845 File Offset: 0x00065A45
			public bool isActive { get; private set; }

			// Token: 0x17000703 RID: 1795
			// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0006784E File Offset: 0x00065A4E
			// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00067856 File Offset: 0x00065A56
			public bool isDetaching { get; private set; }

			// Token: 0x060019E2 RID: 6626 RVA: 0x0006785F File Offset: 0x00065A5F
			protected BaseVisualElementScheduledItem(VisualElement handler)
			{
				this.element = handler;
				this.m_OnAttachToPanelCallback = new EventCallback<AttachToPanelEvent>(this.OnElementAttachToPanelCallback);
				this.m_OnDetachFromPanelCallback = new EventCallback<DetachFromPanelEvent>(this.OnElementDetachFromPanelCallback);
			}

			// Token: 0x060019E3 RID: 6627 RVA: 0x0006789C File Offset: 0x00065A9C
			private void SetActive(bool action)
			{
				bool flag = this.isActive != action;
				if (flag)
				{
					this.isActive = action;
					bool isActive = this.isActive;
					if (isActive)
					{
						this.element.RegisterCallback<AttachToPanelEvent>(this.m_OnAttachToPanelCallback, TrickleDown.NoTrickleDown);
						this.element.RegisterCallback<DetachFromPanelEvent>(this.m_OnDetachFromPanelCallback, TrickleDown.NoTrickleDown);
						this.SendActivation();
					}
					else
					{
						this.element.UnregisterCallback<AttachToPanelEvent>(this.m_OnAttachToPanelCallback, TrickleDown.NoTrickleDown);
						this.element.UnregisterCallback<DetachFromPanelEvent>(this.m_OnDetachFromPanelCallback, TrickleDown.NoTrickleDown);
						this.SendDeactivation();
					}
				}
			}

			// Token: 0x060019E4 RID: 6628 RVA: 0x00067930 File Offset: 0x00065B30
			private void SendActivation()
			{
				bool flag = this.CanBeActivated();
				if (flag)
				{
					this.OnPanelActivate();
				}
			}

			// Token: 0x060019E5 RID: 6629 RVA: 0x00067954 File Offset: 0x00065B54
			private void SendDeactivation()
			{
				bool flag = this.CanBeActivated();
				if (flag)
				{
					this.OnPanelDeactivate();
				}
			}

			// Token: 0x060019E6 RID: 6630 RVA: 0x00067978 File Offset: 0x00065B78
			private void OnElementAttachToPanelCallback(AttachToPanelEvent evt)
			{
				bool isActive = this.isActive;
				if (isActive)
				{
					this.SendActivation();
				}
			}

			// Token: 0x060019E7 RID: 6631 RVA: 0x0006799C File Offset: 0x00065B9C
			private void OnElementDetachFromPanelCallback(DetachFromPanelEvent evt)
			{
				bool flag = !this.isActive;
				if (!flag)
				{
					this.isDetaching = true;
					try
					{
						this.SendDeactivation();
					}
					finally
					{
						this.isDetaching = false;
					}
				}
			}

			// Token: 0x060019E8 RID: 6632 RVA: 0x000679E8 File Offset: 0x00065BE8
			public IVisualElementScheduledItem StartingIn(long delayMs)
			{
				base.delayMs = delayMs;
				return this;
			}

			// Token: 0x060019E9 RID: 6633 RVA: 0x00067A04 File Offset: 0x00065C04
			public IVisualElementScheduledItem Until(Func<bool> stopCondition)
			{
				bool flag = stopCondition == null;
				if (flag)
				{
					stopCondition = ScheduledItem.ForeverCondition;
				}
				this.timerUpdateStopCondition = stopCondition;
				return this;
			}

			// Token: 0x060019EA RID: 6634 RVA: 0x00067A30 File Offset: 0x00065C30
			public IVisualElementScheduledItem Every(long intervalMs)
			{
				base.intervalMs = intervalMs;
				bool flag = this.timerUpdateStopCondition == ScheduledItem.OnceCondition;
				if (flag)
				{
					this.timerUpdateStopCondition = ScheduledItem.ForeverCondition;
				}
				return this;
			}

			// Token: 0x060019EB RID: 6635 RVA: 0x00067A6C File Offset: 0x00065C6C
			internal override void OnItemUnscheduled()
			{
				base.OnItemUnscheduled();
				this.isScheduled = false;
				bool flag = !this.isDetaching;
				if (flag)
				{
					this.SetActive(false);
				}
			}

			// Token: 0x060019EC RID: 6636 RVA: 0x00067A9F File Offset: 0x00065C9F
			public void Resume()
			{
				this.SetActive(true);
			}

			// Token: 0x060019ED RID: 6637 RVA: 0x00067AAA File Offset: 0x00065CAA
			public void Pause()
			{
				this.SetActive(false);
			}

			// Token: 0x060019EE RID: 6638 RVA: 0x00067AB8 File Offset: 0x00065CB8
			public void ExecuteLater(long delayMs)
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.Resume();
				}
				base.ResetStartTime();
				this.StartingIn(delayMs);
			}

			// Token: 0x060019EF RID: 6639 RVA: 0x00067AEC File Offset: 0x00065CEC
			public void OnPanelActivate()
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.isScheduled = true;
					base.ResetStartTime();
					this.element.elementPanel.scheduler.Schedule(this);
				}
			}

			// Token: 0x060019F0 RID: 6640 RVA: 0x00067B30 File Offset: 0x00065D30
			public void OnPanelDeactivate()
			{
				bool flag = this.isScheduled;
				if (flag)
				{
					this.isScheduled = false;
					this.element.elementPanel.scheduler.Unschedule(this);
				}
			}

			// Token: 0x060019F1 RID: 6641 RVA: 0x00067B68 File Offset: 0x00065D68
			public bool CanBeActivated()
			{
				return this.element != null && this.element.elementPanel != null && this.element.elementPanel.scheduler != null;
			}

			// Token: 0x04000C03 RID: 3075
			public bool isScheduled = false;

			// Token: 0x04000C06 RID: 3078
			private readonly EventCallback<AttachToPanelEvent> m_OnAttachToPanelCallback;

			// Token: 0x04000C07 RID: 3079
			private readonly EventCallback<DetachFromPanelEvent> m_OnDetachFromPanelCallback;
		}

		// Token: 0x0200034C RID: 844
		private abstract class VisualElementScheduledItem<ActionType> : VisualElement.BaseVisualElementScheduledItem
		{
			// Token: 0x060019F2 RID: 6642 RVA: 0x00067BA5 File Offset: 0x00065DA5
			public VisualElementScheduledItem(VisualElement handler, ActionType upEvent)
				: base(handler)
			{
				this.updateEvent = upEvent;
			}

			// Token: 0x04000C08 RID: 3080
			public ActionType updateEvent;
		}

		// Token: 0x0200034D RID: 845
		private class TimerStateScheduledItem : VisualElement.VisualElementScheduledItem<Action<TimerState>>
		{
			// Token: 0x060019F3 RID: 6643 RVA: 0x00067BB7 File Offset: 0x00065DB7
			public TimerStateScheduledItem(VisualElement handler, Action<TimerState> updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x060019F4 RID: 6644 RVA: 0x00067BC4 File Offset: 0x00065DC4
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent(state);
				}
			}
		}

		// Token: 0x0200034E RID: 846
		private class SimpleScheduledItem : VisualElement.VisualElementScheduledItem<Action>
		{
			// Token: 0x060019F5 RID: 6645 RVA: 0x00067BEB File Offset: 0x00065DEB
			public SimpleScheduledItem(VisualElement handler, Action updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x060019F6 RID: 6646 RVA: 0x00067BF8 File Offset: 0x00065DF8
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent();
				}
			}
		}

		// Token: 0x0200034F RID: 847
		internal class CustomStyleAccess : ICustomStyle
		{
			// Token: 0x060019F7 RID: 6647 RVA: 0x00067C1E File Offset: 0x00065E1E
			public void SetContext(Dictionary<string, StylePropertyValue> customProperties, float dpiScaling)
			{
				this.m_CustomProperties = customProperties;
				this.m_DpiScaling = dpiScaling;
			}

			// Token: 0x060019F8 RID: 6648 RVA: 0x00067C30 File Offset: 0x00065E30
			public bool TryGetValue(CustomStyleProperty<float> property, out float value)
			{
				StylePropertyValue customProp;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out customProp);
				if (flag)
				{
					bool flag2 = customProp.sheet.TryReadFloat(customProp.handle, out value);
					if (flag2)
					{
						return true;
					}
				}
				value = 0f;
				return false;
			}

			// Token: 0x060019F9 RID: 6649 RVA: 0x00067C7C File Offset: 0x00065E7C
			public bool TryGetValue(CustomStyleProperty<int> property, out int value)
			{
				StylePropertyValue customProp;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out customProp);
				if (flag)
				{
					float tmp;
					bool flag2 = customProp.sheet.TryReadFloat(customProp.handle, out tmp);
					if (flag2)
					{
						value = (int)tmp;
						return true;
					}
				}
				value = 0;
				return false;
			}

			// Token: 0x060019FA RID: 6650 RVA: 0x00067CCC File Offset: 0x00065ECC
			public bool TryGetValue(CustomStyleProperty<Color> property, out Color value)
			{
				StylePropertyValue customProp;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out customProp);
				if (flag)
				{
					StyleValueHandle handle = customProp.handle;
					StyleValueType valueType = handle.valueType;
					StyleValueType styleValueType = valueType;
					if (styleValueType != StyleValueType.Color)
					{
						if (styleValueType == StyleValueType.Enum)
						{
							string colorName = customProp.sheet.ReadAsString(handle);
							return StyleSheetColor.TryGetColor(colorName.ToLowerInvariant(), out value);
						}
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(property.name, StyleValueType.Color, customProp);
					}
					else
					{
						bool flag2 = customProp.sheet.TryReadColor(customProp.handle, out value);
						if (flag2)
						{
							return true;
						}
					}
				}
				value = Color.clear;
				return false;
			}

			// Token: 0x060019FB RID: 6651 RVA: 0x00067D84 File Offset: 0x00065F84
			public bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value)
			{
				StylePropertyValue customProp;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out customProp);
				if (flag)
				{
					ImageSource source = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(customProp, this.m_DpiScaling, out source) && source.texture != null;
					if (flag2)
					{
						value = source.texture;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060019FC RID: 6652 RVA: 0x00067DFC File Offset: 0x00065FFC
			public bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value)
			{
				StylePropertyValue customProp;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out customProp);
				if (flag)
				{
					ImageSource source = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(customProp, this.m_DpiScaling, out source) && source.sprite != null;
					if (flag2)
					{
						value = source.sprite;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060019FD RID: 6653 RVA: 0x00067E74 File Offset: 0x00066074
			public bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value)
			{
				StylePropertyValue customProp;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out customProp);
				if (flag)
				{
					ImageSource source = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(customProp, this.m_DpiScaling, out source) && source.vectorImage != null;
					if (flag2)
					{
						value = source.vectorImage;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x060019FE RID: 6654 RVA: 0x00067EEC File Offset: 0x000660EC
			public bool TryGetValue(CustomStyleProperty<string> property, out string value)
			{
				StylePropertyValue customProp;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out customProp);
				bool flag2;
				if (flag)
				{
					value = customProp.sheet.ReadAsString(customProp.handle);
					flag2 = true;
				}
				else
				{
					value = string.Empty;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x060019FF RID: 6655 RVA: 0x00067F44 File Offset: 0x00066144
			private bool TryGetValue(string propertyName, StyleValueType valueType, out StylePropertyValue customProp)
			{
				customProp = default(StylePropertyValue);
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(propertyName, out customProp);
				bool flag3;
				if (flag)
				{
					StyleValueHandle handle = customProp.handle;
					bool flag2 = handle.valueType != valueType;
					if (flag2)
					{
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(propertyName, valueType, customProp);
						flag3 = false;
					}
					else
					{
						flag3 = true;
					}
				}
				else
				{
					flag3 = false;
				}
				return flag3;
			}

			// Token: 0x06001A00 RID: 6656 RVA: 0x00067FAA File Offset: 0x000661AA
			private static void LogCustomPropertyWarning(string propertyName, StyleValueType valueType, StylePropertyValue customProp)
			{
				Debug.LogWarning(string.Format("Trying to read custom property {0} value as {1} while parsed type is {2}", propertyName, valueType, customProp.handle.valueType));
			}

			// Token: 0x04000C09 RID: 3081
			private Dictionary<string, StylePropertyValue> m_CustomProperties;

			// Token: 0x04000C0A RID: 3082
			private float m_DpiScaling;
		}

		// Token: 0x02000350 RID: 848
		internal class TypeData
		{
			// Token: 0x17000704 RID: 1796
			// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00067FD5 File Offset: 0x000661D5
			public Type type { get; }

			// Token: 0x06001A03 RID: 6659 RVA: 0x00067FDD File Offset: 0x000661DD
			public TypeData(Type type)
			{
				this.type = type;
			}

			// Token: 0x17000705 RID: 1797
			// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00068004 File Offset: 0x00066204
			public string fullTypeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_FullTypeName);
					if (flag)
					{
						this.m_FullTypeName = this.type.FullName;
					}
					return this.m_FullTypeName;
				}
			}

			// Token: 0x17000706 RID: 1798
			// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0006803C File Offset: 0x0006623C
			public string typeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_TypeName);
					if (flag)
					{
						bool isGeneric = this.type.IsGenericType;
						this.m_TypeName = this.type.Name;
						bool flag2 = isGeneric;
						if (flag2)
						{
							int genericTypeIndex = this.m_TypeName.IndexOf('`');
							bool flag3 = genericTypeIndex >= 0;
							if (flag3)
							{
								this.m_TypeName = this.m_TypeName.Remove(genericTypeIndex);
							}
						}
					}
					return this.m_TypeName;
				}
			}

			// Token: 0x04000C0C RID: 3084
			private string m_FullTypeName = string.Empty;

			// Token: 0x04000C0D RID: 3085
			private string m_TypeName = string.Empty;
		}
	}
}
