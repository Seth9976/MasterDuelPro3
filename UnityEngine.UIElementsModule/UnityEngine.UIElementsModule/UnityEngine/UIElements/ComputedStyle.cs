using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Layout;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C5 RID: 709
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct ComputedStyle
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0004ECFF File Offset: 0x0004CEFF
		public int customPropertiesCount
		{
			get
			{
				Dictionary<string, StylePropertyValue> dictionary = this.customProperties;
				return (dictionary != null) ? dictionary.Count : 0;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0004ED13 File Offset: 0x0004CF13
		public bool hasTransition
		{
			get
			{
				ComputedTransitionProperty[] array = this.computedTransitions;
				return array != null && array.Length != 0;
			}
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004ED28 File Offset: 0x0004CF28
		public void FinalizeApply(ref ComputedStyle parentStyle)
		{
			bool flag = this.fontSize.unit == LengthUnit.Percent;
			if (flag)
			{
				float parentSize = parentStyle.fontSize.value;
				float computedSize = parentSize * this.fontSize.value / 100f;
				this.inheritedData.Write().fontSize = new Length(computedSize);
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004ED8C File Offset: 0x0004CF8C
		private bool ApplyGlobalKeyword(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StyleValueHandle handle = reader.GetValue(0).handle;
			bool flag = handle.valueType == StyleValueType.Keyword;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)handle.valueIndex;
				StyleValueKeyword styleValueKeyword = valueIndex;
				if (styleValueKeyword == StyleValueKeyword.Initial)
				{
					this.ApplyInitialValue(reader);
					return true;
				}
				if (styleValueKeyword == StyleValueKeyword.Unset)
				{
					this.ApplyUnsetValue(reader, ref parentStyle);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0004EDF0 File Offset: 0x0004CFF0
		private bool ApplyGlobalKeyword(StylePropertyId id, StyleKeyword keyword, ref ComputedStyle parentStyle)
		{
			bool flag = keyword == StyleKeyword.Initial;
			bool flag2;
			if (flag)
			{
				this.ApplyInitialValue(id);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0004EE18 File Offset: 0x0004D018
		private void RemoveCustomStyleProperty(StylePropertyReader reader)
		{
			string name = reader.property.name;
			bool flag = this.customProperties == null || !this.customProperties.ContainsKey(name);
			if (!flag)
			{
				this.customProperties.Remove(name);
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0004EE60 File Offset: 0x0004D060
		private void ApplyCustomStyleProperty(StylePropertyReader reader)
		{
			this.dpiScaling = reader.dpiScaling;
			bool flag = this.customProperties == null;
			if (flag)
			{
				this.customProperties = new Dictionary<string, StylePropertyValue>();
			}
			StyleProperty styleProperty = reader.property;
			StylePropertyValue customProp = reader.GetValue(0);
			this.customProperties[styleProperty.name] = customProp;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0004EEB6 File Offset: 0x0004D0B6
		private void ApplyAllPropertyInitial()
		{
			this.CopyFrom(InitialStyle.Get());
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004EEC5 File Offset: 0x0004D0C5
		private void ResetComputedTransitions()
		{
			this.computedTransitions = null;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004EED0 File Offset: 0x0004D0D0
		public static bool StartAnimationInlineTextShadow(VisualElement element, ref ComputedStyle computedStyle, StyleTextShadow textShadow, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			TextShadow to = ((textShadow.keyword == StyleKeyword.Initial) ? InitialStyle.textShadow : textShadow.value);
			return element.styleAnimation.Start(StylePropertyId.TextShadow, computedStyle.inheritedData.Read().textShadow, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004EF24 File Offset: 0x0004D124
		public static bool StartAnimationInlineRotate(VisualElement element, ref ComputedStyle computedStyle, StyleRotate rotate, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Rotate to = ((rotate.keyword == StyleKeyword.Initial) ? InitialStyle.rotate : rotate.value);
			bool result = element.styleAnimation.Start(StylePropertyId.Rotate, computedStyle.transformData.Read().rotate, to, durationMs, delayMs, easingCurve);
			bool flag = result && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return result;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004EFA0 File Offset: 0x0004D1A0
		public static bool StartAnimationInlineTranslate(VisualElement element, ref ComputedStyle computedStyle, StyleTranslate translate, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Translate to = ((translate.keyword == StyleKeyword.Initial) ? InitialStyle.translate : translate.value);
			bool result = element.styleAnimation.Start(StylePropertyId.Translate, computedStyle.transformData.Read().translate, to, durationMs, delayMs, easingCurve);
			bool flag = result && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return result;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004F01C File Offset: 0x0004D21C
		public static bool StartAnimationInlineScale(VisualElement element, ref ComputedStyle computedStyle, StyleScale scale, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			Scale to = ((scale.keyword == StyleKeyword.Initial) ? InitialStyle.scale : scale.value);
			bool result = element.styleAnimation.Start(StylePropertyId.Scale, computedStyle.transformData.Read().scale, to, durationMs, delayMs, easingCurve);
			bool flag = result && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return result;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004F098 File Offset: 0x0004D298
		public static bool StartAnimationInlineTransformOrigin(VisualElement element, ref ComputedStyle computedStyle, StyleTransformOrigin transformOrigin, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			TransformOrigin to = ((transformOrigin.keyword == StyleKeyword.Initial) ? InitialStyle.transformOrigin : transformOrigin.value);
			bool result = element.styleAnimation.Start(StylePropertyId.TransformOrigin, computedStyle.transformData.Read().transformOrigin, to, durationMs, delayMs, easingCurve);
			bool flag = result && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
			if (flag)
			{
				element.usageHints |= UsageHints.DynamicTransform;
			}
			return result;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004F114 File Offset: 0x0004D314
		public static bool StartAnimationInlineBackgroundSize(VisualElement element, ref ComputedStyle computedStyle, StyleBackgroundSize backgroundSize, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			BackgroundSize to = ((backgroundSize.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundSize : backgroundSize.value);
			return element.styleAnimation.Start(StylePropertyId.BackgroundSize, computedStyle.visualData.Read().backgroundSize, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004F165 File Offset: 0x0004D365
		public Align alignContent
		{
			get
			{
				return this.layoutData.Read().alignContent;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0004F177 File Offset: 0x0004D377
		public Align alignItems
		{
			get
			{
				return this.layoutData.Read().alignItems;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0004F189 File Offset: 0x0004D389
		public Align alignSelf
		{
			get
			{
				return this.layoutData.Read().alignSelf;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0004F19B File Offset: 0x0004D39B
		public Color backgroundColor
		{
			get
			{
				return this.visualData.Read().backgroundColor;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0004F1AD File Offset: 0x0004D3AD
		public Background backgroundImage
		{
			get
			{
				return this.visualData.Read().backgroundImage;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0004F1BF File Offset: 0x0004D3BF
		public BackgroundPosition backgroundPositionX
		{
			get
			{
				return this.visualData.Read().backgroundPositionX;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004F1D1 File Offset: 0x0004D3D1
		public BackgroundPosition backgroundPositionY
		{
			get
			{
				return this.visualData.Read().backgroundPositionY;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0004F1E3 File Offset: 0x0004D3E3
		public BackgroundRepeat backgroundRepeat
		{
			get
			{
				return this.visualData.Read().backgroundRepeat;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0004F1F5 File Offset: 0x0004D3F5
		public BackgroundSize backgroundSize
		{
			get
			{
				return this.visualData.Read().backgroundSize;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0004F207 File Offset: 0x0004D407
		public Color borderBottomColor
		{
			get
			{
				return this.visualData.Read().borderBottomColor;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0004F219 File Offset: 0x0004D419
		public Length borderBottomLeftRadius
		{
			get
			{
				return this.visualData.Read().borderBottomLeftRadius;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x0004F22B File Offset: 0x0004D42B
		public Length borderBottomRightRadius
		{
			get
			{
				return this.visualData.Read().borderBottomRightRadius;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0004F23D File Offset: 0x0004D43D
		public float borderBottomWidth
		{
			get
			{
				return this.layoutData.Read().borderBottomWidth;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0004F24F File Offset: 0x0004D44F
		public Color borderLeftColor
		{
			get
			{
				return this.visualData.Read().borderLeftColor;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0004F261 File Offset: 0x0004D461
		public float borderLeftWidth
		{
			get
			{
				return this.layoutData.Read().borderLeftWidth;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0004F273 File Offset: 0x0004D473
		public Color borderRightColor
		{
			get
			{
				return this.visualData.Read().borderRightColor;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0004F285 File Offset: 0x0004D485
		public float borderRightWidth
		{
			get
			{
				return this.layoutData.Read().borderRightWidth;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0004F297 File Offset: 0x0004D497
		public Color borderTopColor
		{
			get
			{
				return this.visualData.Read().borderTopColor;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004F2A9 File Offset: 0x0004D4A9
		public Length borderTopLeftRadius
		{
			get
			{
				return this.visualData.Read().borderTopLeftRadius;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0004F2BB File Offset: 0x0004D4BB
		public Length borderTopRightRadius
		{
			get
			{
				return this.visualData.Read().borderTopRightRadius;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004F2CD File Offset: 0x0004D4CD
		public float borderTopWidth
		{
			get
			{
				return this.layoutData.Read().borderTopWidth;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0004F2DF File Offset: 0x0004D4DF
		public Length bottom
		{
			get
			{
				return this.layoutData.Read().bottom;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0004F2F1 File Offset: 0x0004D4F1
		public Color color
		{
			get
			{
				return this.inheritedData.Read().color;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0004F303 File Offset: 0x0004D503
		public Cursor cursor
		{
			get
			{
				return this.rareData.Read().cursor;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0004F315 File Offset: 0x0004D515
		public DisplayStyle display
		{
			get
			{
				return this.layoutData.Read().display;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0004F327 File Offset: 0x0004D527
		public Length flexBasis
		{
			get
			{
				return this.layoutData.Read().flexBasis;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0004F339 File Offset: 0x0004D539
		public FlexDirection flexDirection
		{
			get
			{
				return this.layoutData.Read().flexDirection;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0004F34B File Offset: 0x0004D54B
		public float flexGrow
		{
			get
			{
				return this.layoutData.Read().flexGrow;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0004F35D File Offset: 0x0004D55D
		public float flexShrink
		{
			get
			{
				return this.layoutData.Read().flexShrink;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0004F36F File Offset: 0x0004D56F
		public Wrap flexWrap
		{
			get
			{
				return this.layoutData.Read().flexWrap;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0004F381 File Offset: 0x0004D581
		public Length fontSize
		{
			get
			{
				return this.inheritedData.Read().fontSize;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0004F393 File Offset: 0x0004D593
		public Length height
		{
			get
			{
				return this.layoutData.Read().height;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0004F3A5 File Offset: 0x0004D5A5
		public Justify justifyContent
		{
			get
			{
				return this.layoutData.Read().justifyContent;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0004F3B7 File Offset: 0x0004D5B7
		public Length left
		{
			get
			{
				return this.layoutData.Read().left;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0004F3C9 File Offset: 0x0004D5C9
		public Length letterSpacing
		{
			get
			{
				return this.inheritedData.Read().letterSpacing;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0004F3DB File Offset: 0x0004D5DB
		public Length marginBottom
		{
			get
			{
				return this.layoutData.Read().marginBottom;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0004F3ED File Offset: 0x0004D5ED
		public Length marginLeft
		{
			get
			{
				return this.layoutData.Read().marginLeft;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0004F3FF File Offset: 0x0004D5FF
		public Length marginRight
		{
			get
			{
				return this.layoutData.Read().marginRight;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0004F411 File Offset: 0x0004D611
		public Length marginTop
		{
			get
			{
				return this.layoutData.Read().marginTop;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0004F423 File Offset: 0x0004D623
		public Length maxHeight
		{
			get
			{
				return this.layoutData.Read().maxHeight;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0004F435 File Offset: 0x0004D635
		public Length maxWidth
		{
			get
			{
				return this.layoutData.Read().maxWidth;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0004F447 File Offset: 0x0004D647
		public Length minHeight
		{
			get
			{
				return this.layoutData.Read().minHeight;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0004F459 File Offset: 0x0004D659
		public Length minWidth
		{
			get
			{
				return this.layoutData.Read().minWidth;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0004F46B File Offset: 0x0004D66B
		public float opacity
		{
			get
			{
				return this.visualData.Read().opacity;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x0004F47D File Offset: 0x0004D67D
		public OverflowInternal overflow
		{
			get
			{
				return this.visualData.Read().overflow;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x0004F48F File Offset: 0x0004D68F
		public Length paddingBottom
		{
			get
			{
				return this.layoutData.Read().paddingBottom;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x0004F4A1 File Offset: 0x0004D6A1
		public Length paddingLeft
		{
			get
			{
				return this.layoutData.Read().paddingLeft;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0004F4B3 File Offset: 0x0004D6B3
		public Length paddingRight
		{
			get
			{
				return this.layoutData.Read().paddingRight;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0004F4C5 File Offset: 0x0004D6C5
		public Length paddingTop
		{
			get
			{
				return this.layoutData.Read().paddingTop;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0004F4D7 File Offset: 0x0004D6D7
		public Position position
		{
			get
			{
				return this.layoutData.Read().position;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0004F4E9 File Offset: 0x0004D6E9
		public Length right
		{
			get
			{
				return this.layoutData.Read().right;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0004F4FB File Offset: 0x0004D6FB
		public Rotate rotate
		{
			get
			{
				return this.transformData.Read().rotate;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0004F50D File Offset: 0x0004D70D
		public Scale scale
		{
			get
			{
				return this.transformData.Read().scale;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0004F51F File Offset: 0x0004D71F
		public TextOverflow textOverflow
		{
			get
			{
				return this.rareData.Read().textOverflow;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0004F531 File Offset: 0x0004D731
		public TextShadow textShadow
		{
			get
			{
				return this.inheritedData.Read().textShadow;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0004F543 File Offset: 0x0004D743
		public Length top
		{
			get
			{
				return this.layoutData.Read().top;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0004F555 File Offset: 0x0004D755
		public TransformOrigin transformOrigin
		{
			get
			{
				return this.transformData.Read().transformOrigin;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0004F567 File Offset: 0x0004D767
		public List<TimeValue> transitionDelay
		{
			get
			{
				return this.transitionData.Read().transitionDelay;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0004F579 File Offset: 0x0004D779
		public List<TimeValue> transitionDuration
		{
			get
			{
				return this.transitionData.Read().transitionDuration;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0004F58B File Offset: 0x0004D78B
		public List<StylePropertyName> transitionProperty
		{
			get
			{
				return this.transitionData.Read().transitionProperty;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0004F59D File Offset: 0x0004D79D
		public List<EasingFunction> transitionTimingFunction
		{
			get
			{
				return this.transitionData.Read().transitionTimingFunction;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0004F5AF File Offset: 0x0004D7AF
		public Translate translate
		{
			get
			{
				return this.transformData.Read().translate;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0004F5C1 File Offset: 0x0004D7C1
		public Color unityBackgroundImageTintColor
		{
			get
			{
				return this.rareData.Read().unityBackgroundImageTintColor;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0004F5D3 File Offset: 0x0004D7D3
		public EditorTextRenderingMode unityEditorTextRenderingMode
		{
			get
			{
				return this.inheritedData.Read().unityEditorTextRenderingMode;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0004F5E5 File Offset: 0x0004D7E5
		public Font unityFont
		{
			get
			{
				return this.inheritedData.Read().unityFont;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0004F5F7 File Offset: 0x0004D7F7
		public FontDefinition unityFontDefinition
		{
			get
			{
				return this.inheritedData.Read().unityFontDefinition;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0004F609 File Offset: 0x0004D809
		public FontStyle unityFontStyleAndWeight
		{
			get
			{
				return this.inheritedData.Read().unityFontStyleAndWeight;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0004F61B File Offset: 0x0004D81B
		public OverflowClipBox unityOverflowClipBox
		{
			get
			{
				return this.rareData.Read().unityOverflowClipBox;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0004F62D File Offset: 0x0004D82D
		public Length unityParagraphSpacing
		{
			get
			{
				return this.inheritedData.Read().unityParagraphSpacing;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0004F63F File Offset: 0x0004D83F
		public int unitySliceBottom
		{
			get
			{
				return this.rareData.Read().unitySliceBottom;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x0004F651 File Offset: 0x0004D851
		public int unitySliceLeft
		{
			get
			{
				return this.rareData.Read().unitySliceLeft;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0004F663 File Offset: 0x0004D863
		public int unitySliceRight
		{
			get
			{
				return this.rareData.Read().unitySliceRight;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0004F675 File Offset: 0x0004D875
		public float unitySliceScale
		{
			get
			{
				return this.rareData.Read().unitySliceScale;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0004F687 File Offset: 0x0004D887
		public int unitySliceTop
		{
			get
			{
				return this.rareData.Read().unitySliceTop;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0004F699 File Offset: 0x0004D899
		public TextAnchor unityTextAlign
		{
			get
			{
				return this.inheritedData.Read().unityTextAlign;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0004F6AB File Offset: 0x0004D8AB
		public TextGeneratorType unityTextGenerator
		{
			get
			{
				return this.inheritedData.Read().unityTextGenerator;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x0004F6BD File Offset: 0x0004D8BD
		public Color unityTextOutlineColor
		{
			get
			{
				return this.inheritedData.Read().unityTextOutlineColor;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0004F6CF File Offset: 0x0004D8CF
		public float unityTextOutlineWidth
		{
			get
			{
				return this.inheritedData.Read().unityTextOutlineWidth;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0004F6E1 File Offset: 0x0004D8E1
		public TextOverflowPosition unityTextOverflowPosition
		{
			get
			{
				return this.rareData.Read().unityTextOverflowPosition;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0004F6F3 File Offset: 0x0004D8F3
		public Visibility visibility
		{
			get
			{
				return this.inheritedData.Read().visibility;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0004F705 File Offset: 0x0004D905
		public WhiteSpace whiteSpace
		{
			get
			{
				return this.inheritedData.Read().whiteSpace;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004F717 File Offset: 0x0004D917
		public Length width
		{
			get
			{
				return this.layoutData.Read().width;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0004F729 File Offset: 0x0004D929
		public Length wordSpacing
		{
			get
			{
				return this.inheritedData.Read().wordSpacing;
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004F73C File Offset: 0x0004D93C
		public static ComputedStyle Create(ref ComputedStyle parentStyle)
		{
			ref ComputedStyle initialStyle = ref InitialStyle.Get();
			ComputedStyle cs = new ComputedStyle
			{
				dpiScaling = 1f
			};
			cs.inheritedData = parentStyle.inheritedData.Acquire();
			cs.layoutData = initialStyle.layoutData.Acquire();
			cs.rareData = initialStyle.rareData.Acquire();
			cs.transformData = initialStyle.transformData.Acquire();
			cs.transitionData = initialStyle.transitionData.Acquire();
			cs.visualData = initialStyle.visualData.Acquire();
			return cs;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004F7D8 File Offset: 0x0004D9D8
		public static ComputedStyle CreateInitial()
		{
			ComputedStyle cs = new ComputedStyle
			{
				dpiScaling = 1f
			};
			cs.inheritedData = StyleDataRef<InheritedData>.Create();
			cs.layoutData = StyleDataRef<LayoutData>.Create();
			cs.rareData = StyleDataRef<RareData>.Create();
			cs.transformData = StyleDataRef<TransformData>.Create();
			cs.transitionData = StyleDataRef<TransitionData>.Create();
			cs.visualData = StyleDataRef<VisualData>.Create();
			return cs;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004F84C File Offset: 0x0004DA4C
		public ComputedStyle Acquire()
		{
			this.inheritedData.Acquire();
			this.layoutData.Acquire();
			this.rareData.Acquire();
			this.transformData.Acquire();
			this.transitionData.Acquire();
			this.visualData.Acquire();
			return this;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004F8AC File Offset: 0x0004DAAC
		public void Release()
		{
			this.inheritedData.Release();
			this.layoutData.Release();
			this.rareData.Release();
			this.transformData.Release();
			this.transitionData.Release();
			this.visualData.Release();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004F904 File Offset: 0x0004DB04
		public void CopyFrom(ref ComputedStyle other)
		{
			this.inheritedData.CopyFrom(other.inheritedData);
			this.layoutData.CopyFrom(other.layoutData);
			this.rareData.CopyFrom(other.rareData);
			this.transformData.CopyFrom(other.transformData);
			this.transitionData.CopyFrom(other.transitionData);
			this.visualData.CopyFrom(other.visualData);
			this.customProperties = other.customProperties;
			this.matchingRulesHash = other.matchingRulesHash;
			this.dpiScaling = other.dpiScaling;
			this.computedTransitions = other.computedTransitions;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004F9B0 File Offset: 0x0004DBB0
		public void ApplyProperties(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StylePropertyId id = reader.propertyId;
			while (reader.property != null)
			{
				bool flag = this.ApplyGlobalKeyword(reader, ref parentStyle);
				if (!flag)
				{
					StylePropertyId stylePropertyId = id;
					StylePropertyId stylePropertyId2 = stylePropertyId;
					if (stylePropertyId2 <= StylePropertyId.Width)
					{
						if (stylePropertyId2 <= StylePropertyId.Unknown)
						{
							if (stylePropertyId2 != StylePropertyId.Custom)
							{
								if (stylePropertyId2 != StylePropertyId.Unknown)
								{
									goto IL_0C09;
								}
							}
							else
							{
								this.ApplyCustomStyleProperty(reader);
							}
						}
						else
						{
							switch (stylePropertyId2)
							{
							case StylePropertyId.Color:
								this.inheritedData.Write().color = reader.ReadColor(0);
								break;
							case StylePropertyId.FontSize:
								this.inheritedData.Write().fontSize = reader.ReadLength(0);
								break;
							case StylePropertyId.LetterSpacing:
								this.inheritedData.Write().letterSpacing = reader.ReadLength(0);
								break;
							case StylePropertyId.TextShadow:
								this.inheritedData.Write().textShadow = reader.ReadTextShadow(0);
								break;
							case StylePropertyId.UnityEditorTextRenderingMode:
								this.inheritedData.Write().unityEditorTextRenderingMode = (EditorTextRenderingMode)reader.ReadEnum(StyleEnumType.EditorTextRenderingMode, 0);
								break;
							case StylePropertyId.UnityFont:
								this.inheritedData.Write().unityFont = reader.ReadFont(0);
								break;
							case StylePropertyId.UnityFontDefinition:
								this.inheritedData.Write().unityFontDefinition = reader.ReadFontDefinition(0);
								break;
							case StylePropertyId.UnityFontStyleAndWeight:
								this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)reader.ReadEnum(StyleEnumType.FontStyle, 0);
								break;
							case StylePropertyId.UnityParagraphSpacing:
								this.inheritedData.Write().unityParagraphSpacing = reader.ReadLength(0);
								break;
							case StylePropertyId.UnityTextAlign:
								this.inheritedData.Write().unityTextAlign = (TextAnchor)reader.ReadEnum(StyleEnumType.TextAnchor, 0);
								break;
							case StylePropertyId.UnityTextGenerator:
								this.inheritedData.Write().unityTextGenerator = (TextGeneratorType)reader.ReadEnum(StyleEnumType.TextGeneratorType, 0);
								break;
							case StylePropertyId.UnityTextOutlineColor:
								this.inheritedData.Write().unityTextOutlineColor = reader.ReadColor(0);
								break;
							case StylePropertyId.UnityTextOutlineWidth:
								this.inheritedData.Write().unityTextOutlineWidth = reader.ReadFloat(0);
								break;
							case StylePropertyId.Visibility:
								this.inheritedData.Write().visibility = (Visibility)reader.ReadEnum(StyleEnumType.Visibility, 0);
								break;
							case StylePropertyId.WhiteSpace:
								this.inheritedData.Write().whiteSpace = (WhiteSpace)reader.ReadEnum(StyleEnumType.WhiteSpace, 0);
								break;
							case StylePropertyId.WordSpacing:
								this.inheritedData.Write().wordSpacing = reader.ReadLength(0);
								break;
							default:
								switch (stylePropertyId2)
								{
								case StylePropertyId.AlignContent:
									this.layoutData.Write().alignContent = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.AlignItems:
									this.layoutData.Write().alignItems = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.AlignSelf:
									this.layoutData.Write().alignSelf = (Align)reader.ReadEnum(StyleEnumType.Align, 0);
									break;
								case StylePropertyId.BorderBottomWidth:
									this.layoutData.Write().borderBottomWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderLeftWidth:
									this.layoutData.Write().borderLeftWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderRightWidth:
									this.layoutData.Write().borderRightWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.BorderTopWidth:
									this.layoutData.Write().borderTopWidth = reader.ReadFloat(0);
									break;
								case StylePropertyId.Bottom:
									this.layoutData.Write().bottom = reader.ReadLength(0);
									break;
								case StylePropertyId.Display:
									this.layoutData.Write().display = (DisplayStyle)reader.ReadEnum(StyleEnumType.DisplayStyle, 0);
									break;
								case StylePropertyId.FlexBasis:
									this.layoutData.Write().flexBasis = reader.ReadLength(0);
									break;
								case StylePropertyId.FlexDirection:
									this.layoutData.Write().flexDirection = (FlexDirection)reader.ReadEnum(StyleEnumType.FlexDirection, 0);
									break;
								case StylePropertyId.FlexGrow:
									this.layoutData.Write().flexGrow = reader.ReadFloat(0);
									break;
								case StylePropertyId.FlexShrink:
									this.layoutData.Write().flexShrink = reader.ReadFloat(0);
									break;
								case StylePropertyId.FlexWrap:
									this.layoutData.Write().flexWrap = (Wrap)reader.ReadEnum(StyleEnumType.Wrap, 0);
									break;
								case StylePropertyId.Height:
									this.layoutData.Write().height = reader.ReadLength(0);
									break;
								case StylePropertyId.JustifyContent:
									this.layoutData.Write().justifyContent = (Justify)reader.ReadEnum(StyleEnumType.Justify, 0);
									break;
								case StylePropertyId.Left:
									this.layoutData.Write().left = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginBottom:
									this.layoutData.Write().marginBottom = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginLeft:
									this.layoutData.Write().marginLeft = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginRight:
									this.layoutData.Write().marginRight = reader.ReadLength(0);
									break;
								case StylePropertyId.MarginTop:
									this.layoutData.Write().marginTop = reader.ReadLength(0);
									break;
								case StylePropertyId.MaxHeight:
									this.layoutData.Write().maxHeight = reader.ReadLength(0);
									break;
								case StylePropertyId.MaxWidth:
									this.layoutData.Write().maxWidth = reader.ReadLength(0);
									break;
								case StylePropertyId.MinHeight:
									this.layoutData.Write().minHeight = reader.ReadLength(0);
									break;
								case StylePropertyId.MinWidth:
									this.layoutData.Write().minWidth = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingBottom:
									this.layoutData.Write().paddingBottom = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingLeft:
									this.layoutData.Write().paddingLeft = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingRight:
									this.layoutData.Write().paddingRight = reader.ReadLength(0);
									break;
								case StylePropertyId.PaddingTop:
									this.layoutData.Write().paddingTop = reader.ReadLength(0);
									break;
								case StylePropertyId.Position:
									this.layoutData.Write().position = (Position)reader.ReadEnum(StyleEnumType.Position, 0);
									break;
								case StylePropertyId.Right:
									this.layoutData.Write().right = reader.ReadLength(0);
									break;
								case StylePropertyId.Top:
									this.layoutData.Write().top = reader.ReadLength(0);
									break;
								case StylePropertyId.Width:
									this.layoutData.Write().width = reader.ReadLength(0);
									break;
								default:
									goto IL_0C09;
								}
								break;
							}
						}
					}
					else if (stylePropertyId2 <= StylePropertyId.UnityTextOutline)
					{
						switch (stylePropertyId2)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = reader.ReadCursor(0);
							break;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = (TextOverflow)reader.ReadEnum(StyleEnumType.TextOverflow, 0);
							break;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = reader.ReadColor(0);
							break;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)reader.ReadEnum(StyleEnumType.OverflowClipBox, 0);
							break;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = reader.ReadInt(0);
							break;
						case StylePropertyId.UnitySliceScale:
							this.rareData.Write().unitySliceScale = reader.ReadFloat(0);
							break;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = reader.ReadInt(0);
							break;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)reader.ReadEnum(StyleEnumType.TextOverflowPosition, 0);
							break;
						default:
							switch (stylePropertyId2)
							{
							case StylePropertyId.All:
								break;
							case StylePropertyId.BackgroundPosition:
								ShorthandApplicator.ApplyBackgroundPosition(reader, ref this);
								break;
							case StylePropertyId.BorderColor:
								ShorthandApplicator.ApplyBorderColor(reader, ref this);
								break;
							case StylePropertyId.BorderRadius:
								ShorthandApplicator.ApplyBorderRadius(reader, ref this);
								break;
							case StylePropertyId.BorderWidth:
								ShorthandApplicator.ApplyBorderWidth(reader, ref this);
								break;
							case StylePropertyId.Flex:
								ShorthandApplicator.ApplyFlex(reader, ref this);
								break;
							case StylePropertyId.Margin:
								ShorthandApplicator.ApplyMargin(reader, ref this);
								break;
							case StylePropertyId.Padding:
								ShorthandApplicator.ApplyPadding(reader, ref this);
								break;
							case StylePropertyId.Transition:
								ShorthandApplicator.ApplyTransition(reader, ref this);
								break;
							case StylePropertyId.UnityBackgroundScaleMode:
								ShorthandApplicator.ApplyUnityBackgroundScaleMode(reader, ref this);
								break;
							case StylePropertyId.UnityTextOutline:
								ShorthandApplicator.ApplyUnityTextOutline(reader, ref this);
								break;
							default:
								goto IL_0C09;
							}
							break;
						}
					}
					else
					{
						switch (stylePropertyId2)
						{
						case StylePropertyId.Rotate:
							this.transformData.Write().rotate = reader.ReadRotate(0);
							break;
						case StylePropertyId.Scale:
							this.transformData.Write().scale = reader.ReadScale(0);
							break;
						case StylePropertyId.TransformOrigin:
							this.transformData.Write().transformOrigin = reader.ReadTransformOrigin(0);
							break;
						case StylePropertyId.Translate:
							this.transformData.Write().translate = reader.ReadTranslate(0);
							break;
						default:
							switch (stylePropertyId2)
							{
							case StylePropertyId.TransitionDelay:
								reader.ReadListTimeValue(this.transitionData.Write().transitionDelay, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionDuration:
								reader.ReadListTimeValue(this.transitionData.Write().transitionDuration, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionProperty:
								reader.ReadListStylePropertyName(this.transitionData.Write().transitionProperty, 0);
								this.ResetComputedTransitions();
								break;
							case StylePropertyId.TransitionTimingFunction:
								reader.ReadListEasingFunction(this.transitionData.Write().transitionTimingFunction, 0);
								this.ResetComputedTransitions();
								break;
							default:
								switch (stylePropertyId2)
								{
								case StylePropertyId.BackgroundColor:
									this.visualData.Write().backgroundColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BackgroundImage:
									this.visualData.Write().backgroundImage = reader.ReadBackground(0);
									break;
								case StylePropertyId.BackgroundPositionX:
									this.visualData.Write().backgroundPositionX = reader.ReadBackgroundPositionX(0);
									break;
								case StylePropertyId.BackgroundPositionY:
									this.visualData.Write().backgroundPositionY = reader.ReadBackgroundPositionY(0);
									break;
								case StylePropertyId.BackgroundRepeat:
									this.visualData.Write().backgroundRepeat = reader.ReadBackgroundRepeat(0);
									break;
								case StylePropertyId.BackgroundSize:
									this.visualData.Write().backgroundSize = reader.ReadBackgroundSize(0);
									break;
								case StylePropertyId.BorderBottomColor:
									this.visualData.Write().borderBottomColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderBottomLeftRadius:
									this.visualData.Write().borderBottomLeftRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderBottomRightRadius:
									this.visualData.Write().borderBottomRightRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderLeftColor:
									this.visualData.Write().borderLeftColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderRightColor:
									this.visualData.Write().borderRightColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderTopColor:
									this.visualData.Write().borderTopColor = reader.ReadColor(0);
									break;
								case StylePropertyId.BorderTopLeftRadius:
									this.visualData.Write().borderTopLeftRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.BorderTopRightRadius:
									this.visualData.Write().borderTopRightRadius = reader.ReadLength(0);
									break;
								case StylePropertyId.Opacity:
									this.visualData.Write().opacity = reader.ReadFloat(0);
									break;
								case StylePropertyId.Overflow:
									this.visualData.Write().overflow = (OverflowInternal)reader.ReadEnum(StyleEnumType.OverflowInternal, 0);
									break;
								default:
									goto IL_0C09;
								}
								break;
							}
							break;
						}
					}
					goto IL_0C22;
					IL_0C09:
					Debug.LogAssertion(string.Format("Unknown property id {0}", id));
				}
				IL_0C22:
				id = reader.MoveNextProperty();
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000505F8 File Offset: 0x0004E7F8
		public void ApplyStyleValue(StyleValue sv, ref ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv.id, sv.keyword, ref parentStyle);
			if (!flag)
			{
				StylePropertyId id = sv.id;
				StylePropertyId stylePropertyId = id;
				if (stylePropertyId <= StylePropertyId.Width)
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.Color:
						this.inheritedData.Write().color = sv.color;
						return;
					case StylePropertyId.FontSize:
						this.inheritedData.Write().fontSize = sv.length;
						return;
					case StylePropertyId.LetterSpacing:
						this.inheritedData.Write().letterSpacing = sv.length;
						return;
					case StylePropertyId.TextShadow:
						break;
					case StylePropertyId.UnityEditorTextRenderingMode:
						this.inheritedData.Write().unityEditorTextRenderingMode = (EditorTextRenderingMode)sv.number;
						return;
					case StylePropertyId.UnityFont:
						this.inheritedData.Write().unityFont = (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null);
						return;
					case StylePropertyId.UnityFontDefinition:
						this.inheritedData.Write().unityFontDefinition = (sv.resource.IsAllocated ? FontDefinition.FromObject(sv.resource.Target) : default(FontDefinition));
						return;
					case StylePropertyId.UnityFontStyleAndWeight:
						this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)sv.number;
						return;
					case StylePropertyId.UnityParagraphSpacing:
						this.inheritedData.Write().unityParagraphSpacing = sv.length;
						return;
					case StylePropertyId.UnityTextAlign:
						this.inheritedData.Write().unityTextAlign = (TextAnchor)sv.number;
						return;
					case StylePropertyId.UnityTextGenerator:
						this.inheritedData.Write().unityTextGenerator = (TextGeneratorType)sv.number;
						return;
					case StylePropertyId.UnityTextOutlineColor:
						this.inheritedData.Write().unityTextOutlineColor = sv.color;
						return;
					case StylePropertyId.UnityTextOutlineWidth:
						this.inheritedData.Write().unityTextOutlineWidth = sv.number;
						return;
					case StylePropertyId.Visibility:
						this.inheritedData.Write().visibility = (Visibility)sv.number;
						return;
					case StylePropertyId.WhiteSpace:
						this.inheritedData.Write().whiteSpace = (WhiteSpace)sv.number;
						return;
					case StylePropertyId.WordSpacing:
						this.inheritedData.Write().wordSpacing = sv.length;
						return;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.AlignContent:
						{
							this.layoutData.Write().alignContent = (Align)sv.number;
							bool flag2 = sv.keyword == StyleKeyword.Auto;
							if (flag2)
							{
								this.layoutData.Write().alignContent = Align.Auto;
							}
							return;
						}
						case StylePropertyId.AlignItems:
						{
							this.layoutData.Write().alignItems = (Align)sv.number;
							bool flag3 = sv.keyword == StyleKeyword.Auto;
							if (flag3)
							{
								this.layoutData.Write().alignItems = Align.Auto;
							}
							return;
						}
						case StylePropertyId.AlignSelf:
						{
							this.layoutData.Write().alignSelf = (Align)sv.number;
							bool flag4 = sv.keyword == StyleKeyword.Auto;
							if (flag4)
							{
								this.layoutData.Write().alignSelf = Align.Auto;
							}
							return;
						}
						case StylePropertyId.BorderBottomWidth:
							this.layoutData.Write().borderBottomWidth = sv.number;
							return;
						case StylePropertyId.BorderLeftWidth:
							this.layoutData.Write().borderLeftWidth = sv.number;
							return;
						case StylePropertyId.BorderRightWidth:
							this.layoutData.Write().borderRightWidth = sv.number;
							return;
						case StylePropertyId.BorderTopWidth:
							this.layoutData.Write().borderTopWidth = sv.number;
							return;
						case StylePropertyId.Bottom:
							this.layoutData.Write().bottom = sv.length;
							return;
						case StylePropertyId.Display:
						{
							this.layoutData.Write().display = (DisplayStyle)sv.number;
							bool flag5 = sv.keyword == StyleKeyword.None;
							if (flag5)
							{
								this.layoutData.Write().display = DisplayStyle.None;
							}
							return;
						}
						case StylePropertyId.FlexBasis:
							this.layoutData.Write().flexBasis = sv.length;
							return;
						case StylePropertyId.FlexDirection:
							this.layoutData.Write().flexDirection = (FlexDirection)sv.number;
							return;
						case StylePropertyId.FlexGrow:
							this.layoutData.Write().flexGrow = sv.number;
							return;
						case StylePropertyId.FlexShrink:
							this.layoutData.Write().flexShrink = sv.number;
							return;
						case StylePropertyId.FlexWrap:
							this.layoutData.Write().flexWrap = (Wrap)sv.number;
							return;
						case StylePropertyId.Height:
							this.layoutData.Write().height = sv.length;
							return;
						case StylePropertyId.JustifyContent:
							this.layoutData.Write().justifyContent = (Justify)sv.number;
							return;
						case StylePropertyId.Left:
							this.layoutData.Write().left = sv.length;
							return;
						case StylePropertyId.MarginBottom:
							this.layoutData.Write().marginBottom = sv.length;
							return;
						case StylePropertyId.MarginLeft:
							this.layoutData.Write().marginLeft = sv.length;
							return;
						case StylePropertyId.MarginRight:
							this.layoutData.Write().marginRight = sv.length;
							return;
						case StylePropertyId.MarginTop:
							this.layoutData.Write().marginTop = sv.length;
							return;
						case StylePropertyId.MaxHeight:
							this.layoutData.Write().maxHeight = sv.length;
							return;
						case StylePropertyId.MaxWidth:
							this.layoutData.Write().maxWidth = sv.length;
							return;
						case StylePropertyId.MinHeight:
							this.layoutData.Write().minHeight = sv.length;
							return;
						case StylePropertyId.MinWidth:
							this.layoutData.Write().minWidth = sv.length;
							return;
						case StylePropertyId.PaddingBottom:
							this.layoutData.Write().paddingBottom = sv.length;
							return;
						case StylePropertyId.PaddingLeft:
							this.layoutData.Write().paddingLeft = sv.length;
							return;
						case StylePropertyId.PaddingRight:
							this.layoutData.Write().paddingRight = sv.length;
							return;
						case StylePropertyId.PaddingTop:
							this.layoutData.Write().paddingTop = sv.length;
							return;
						case StylePropertyId.Position:
							this.layoutData.Write().position = (Position)sv.number;
							return;
						case StylePropertyId.Right:
							this.layoutData.Write().right = sv.length;
							return;
						case StylePropertyId.Top:
							this.layoutData.Write().top = sv.length;
							return;
						case StylePropertyId.Width:
							this.layoutData.Write().width = sv.length;
							return;
						}
						break;
					}
				}
				else
				{
					switch (stylePropertyId)
					{
					case StylePropertyId.TextOverflow:
						this.rareData.Write().textOverflow = (TextOverflow)sv.number;
						return;
					case StylePropertyId.UnityBackgroundImageTintColor:
						this.rareData.Write().unityBackgroundImageTintColor = sv.color;
						return;
					case StylePropertyId.UnityOverflowClipBox:
						this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)sv.number;
						return;
					case StylePropertyId.UnitySliceBottom:
						this.rareData.Write().unitySliceBottom = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceLeft:
						this.rareData.Write().unitySliceLeft = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceRight:
						this.rareData.Write().unitySliceRight = (int)sv.number;
						return;
					case StylePropertyId.UnitySliceScale:
						this.rareData.Write().unitySliceScale = sv.number;
						return;
					case StylePropertyId.UnitySliceTop:
						this.rareData.Write().unitySliceTop = (int)sv.number;
						return;
					case StylePropertyId.UnityTextOverflowPosition:
						this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)sv.number;
						return;
					default:
						switch (stylePropertyId)
						{
						case StylePropertyId.BackgroundColor:
							this.visualData.Write().backgroundColor = sv.color;
							return;
						case StylePropertyId.BackgroundImage:
							this.visualData.Write().backgroundImage = (sv.resource.IsAllocated ? Background.FromObject(sv.resource.Target) : default(Background));
							return;
						case StylePropertyId.BackgroundPositionX:
							this.visualData.Write().backgroundPositionX = sv.position;
							return;
						case StylePropertyId.BackgroundPositionY:
							this.visualData.Write().backgroundPositionY = sv.position;
							return;
						case StylePropertyId.BackgroundRepeat:
							this.visualData.Write().backgroundRepeat = sv.repeat;
							return;
						case StylePropertyId.BorderBottomColor:
							this.visualData.Write().borderBottomColor = sv.color;
							return;
						case StylePropertyId.BorderBottomLeftRadius:
							this.visualData.Write().borderBottomLeftRadius = sv.length;
							return;
						case StylePropertyId.BorderBottomRightRadius:
							this.visualData.Write().borderBottomRightRadius = sv.length;
							return;
						case StylePropertyId.BorderLeftColor:
							this.visualData.Write().borderLeftColor = sv.color;
							return;
						case StylePropertyId.BorderRightColor:
							this.visualData.Write().borderRightColor = sv.color;
							return;
						case StylePropertyId.BorderTopColor:
							this.visualData.Write().borderTopColor = sv.color;
							return;
						case StylePropertyId.BorderTopLeftRadius:
							this.visualData.Write().borderTopLeftRadius = sv.length;
							return;
						case StylePropertyId.BorderTopRightRadius:
							this.visualData.Write().borderTopRightRadius = sv.length;
							return;
						case StylePropertyId.Opacity:
							this.visualData.Write().opacity = sv.number;
							return;
						case StylePropertyId.Overflow:
							this.visualData.Write().overflow = (OverflowInternal)sv.number;
							return;
						}
						break;
					}
				}
				Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
			}
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0005103C File Offset: 0x0004F23C
		public void ApplyStyleValueManaged(StyleValueManaged sv, ref ComputedStyle parentStyle)
		{
			bool flag = this.ApplyGlobalKeyword(sv.id, sv.keyword, ref parentStyle);
			if (!flag)
			{
				switch (sv.id)
				{
				case StylePropertyId.TransitionDelay:
				{
					bool flag2 = sv.value == null;
					if (flag2)
					{
						this.transitionData.Write().transitionDelay.CopyFrom(InitialStyle.transitionDelay);
					}
					else
					{
						this.transitionData.Write().transitionDelay = sv.value as List<TimeValue>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionDuration:
				{
					bool flag3 = sv.value == null;
					if (flag3)
					{
						this.transitionData.Write().transitionDuration.CopyFrom(InitialStyle.transitionDuration);
					}
					else
					{
						this.transitionData.Write().transitionDuration = sv.value as List<TimeValue>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionProperty:
				{
					bool flag4 = sv.value == null;
					if (flag4)
					{
						this.transitionData.Write().transitionProperty.CopyFrom(InitialStyle.transitionProperty);
					}
					else
					{
						this.transitionData.Write().transitionProperty = sv.value as List<StylePropertyName>;
					}
					this.ResetComputedTransitions();
					break;
				}
				case StylePropertyId.TransitionTimingFunction:
				{
					bool flag5 = sv.value == null;
					if (flag5)
					{
						this.transitionData.Write().transitionTimingFunction.CopyFrom(InitialStyle.transitionTimingFunction);
					}
					else
					{
						this.transitionData.Write().transitionTimingFunction = sv.value as List<EasingFunction>;
					}
					this.ResetComputedTransitions();
					break;
				}
				default:
					Debug.LogAssertion(string.Format("Unexpected property id {0}", sv.id));
					break;
				}
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000511F1 File Offset: 0x0004F3F1
		public void ApplyStyleCursor(Cursor cursor)
		{
			this.rareData.Write().cursor = cursor;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00051205 File Offset: 0x0004F405
		public void ApplyStyleTextShadow(TextShadow st)
		{
			this.inheritedData.Write().textShadow = st;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0005121C File Offset: 0x0004F41C
		public void ApplyFromComputedStyle(StylePropertyId id, ref ComputedStyle other)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					this.inheritedData.Write().color = other.inheritedData.Read().color;
					return;
				case StylePropertyId.FontSize:
					this.inheritedData.Write().fontSize = other.inheritedData.Read().fontSize;
					return;
				case StylePropertyId.LetterSpacing:
					this.inheritedData.Write().letterSpacing = other.inheritedData.Read().letterSpacing;
					return;
				case StylePropertyId.TextShadow:
					this.inheritedData.Write().textShadow = other.inheritedData.Read().textShadow;
					return;
				case StylePropertyId.UnityEditorTextRenderingMode:
					this.inheritedData.Write().unityEditorTextRenderingMode = other.inheritedData.Read().unityEditorTextRenderingMode;
					return;
				case StylePropertyId.UnityFont:
					this.inheritedData.Write().unityFont = other.inheritedData.Read().unityFont;
					return;
				case StylePropertyId.UnityFontDefinition:
					this.inheritedData.Write().unityFontDefinition = other.inheritedData.Read().unityFontDefinition;
					return;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.Write().unityFontStyleAndWeight = other.inheritedData.Read().unityFontStyleAndWeight;
					return;
				case StylePropertyId.UnityParagraphSpacing:
					this.inheritedData.Write().unityParagraphSpacing = other.inheritedData.Read().unityParagraphSpacing;
					return;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.Write().unityTextAlign = other.inheritedData.Read().unityTextAlign;
					return;
				case StylePropertyId.UnityTextGenerator:
					this.inheritedData.Write().unityTextGenerator = other.inheritedData.Read().unityTextGenerator;
					return;
				case StylePropertyId.UnityTextOutlineColor:
					this.inheritedData.Write().unityTextOutlineColor = other.inheritedData.Read().unityTextOutlineColor;
					return;
				case StylePropertyId.UnityTextOutlineWidth:
					this.inheritedData.Write().unityTextOutlineWidth = other.inheritedData.Read().unityTextOutlineWidth;
					return;
				case StylePropertyId.Visibility:
					this.inheritedData.Write().visibility = other.inheritedData.Read().visibility;
					return;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.Write().whiteSpace = other.inheritedData.Read().whiteSpace;
					return;
				case StylePropertyId.WordSpacing:
					this.inheritedData.Write().wordSpacing = other.inheritedData.Read().wordSpacing;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						this.layoutData.Write().alignContent = other.layoutData.Read().alignContent;
						return;
					case StylePropertyId.AlignItems:
						this.layoutData.Write().alignItems = other.layoutData.Read().alignItems;
						return;
					case StylePropertyId.AlignSelf:
						this.layoutData.Write().alignSelf = other.layoutData.Read().alignSelf;
						return;
					case StylePropertyId.BorderBottomWidth:
						this.layoutData.Write().borderBottomWidth = other.layoutData.Read().borderBottomWidth;
						return;
					case StylePropertyId.BorderLeftWidth:
						this.layoutData.Write().borderLeftWidth = other.layoutData.Read().borderLeftWidth;
						return;
					case StylePropertyId.BorderRightWidth:
						this.layoutData.Write().borderRightWidth = other.layoutData.Read().borderRightWidth;
						return;
					case StylePropertyId.BorderTopWidth:
						this.layoutData.Write().borderTopWidth = other.layoutData.Read().borderTopWidth;
						return;
					case StylePropertyId.Bottom:
						this.layoutData.Write().bottom = other.layoutData.Read().bottom;
						return;
					case StylePropertyId.Display:
						this.layoutData.Write().display = other.layoutData.Read().display;
						return;
					case StylePropertyId.FlexBasis:
						this.layoutData.Write().flexBasis = other.layoutData.Read().flexBasis;
						return;
					case StylePropertyId.FlexDirection:
						this.layoutData.Write().flexDirection = other.layoutData.Read().flexDirection;
						return;
					case StylePropertyId.FlexGrow:
						this.layoutData.Write().flexGrow = other.layoutData.Read().flexGrow;
						return;
					case StylePropertyId.FlexShrink:
						this.layoutData.Write().flexShrink = other.layoutData.Read().flexShrink;
						return;
					case StylePropertyId.FlexWrap:
						this.layoutData.Write().flexWrap = other.layoutData.Read().flexWrap;
						return;
					case StylePropertyId.Height:
						this.layoutData.Write().height = other.layoutData.Read().height;
						return;
					case StylePropertyId.JustifyContent:
						this.layoutData.Write().justifyContent = other.layoutData.Read().justifyContent;
						return;
					case StylePropertyId.Left:
						this.layoutData.Write().left = other.layoutData.Read().left;
						return;
					case StylePropertyId.MarginBottom:
						this.layoutData.Write().marginBottom = other.layoutData.Read().marginBottom;
						return;
					case StylePropertyId.MarginLeft:
						this.layoutData.Write().marginLeft = other.layoutData.Read().marginLeft;
						return;
					case StylePropertyId.MarginRight:
						this.layoutData.Write().marginRight = other.layoutData.Read().marginRight;
						return;
					case StylePropertyId.MarginTop:
						this.layoutData.Write().marginTop = other.layoutData.Read().marginTop;
						return;
					case StylePropertyId.MaxHeight:
						this.layoutData.Write().maxHeight = other.layoutData.Read().maxHeight;
						return;
					case StylePropertyId.MaxWidth:
						this.layoutData.Write().maxWidth = other.layoutData.Read().maxWidth;
						return;
					case StylePropertyId.MinHeight:
						this.layoutData.Write().minHeight = other.layoutData.Read().minHeight;
						return;
					case StylePropertyId.MinWidth:
						this.layoutData.Write().minWidth = other.layoutData.Read().minWidth;
						return;
					case StylePropertyId.PaddingBottom:
						this.layoutData.Write().paddingBottom = other.layoutData.Read().paddingBottom;
						return;
					case StylePropertyId.PaddingLeft:
						this.layoutData.Write().paddingLeft = other.layoutData.Read().paddingLeft;
						return;
					case StylePropertyId.PaddingRight:
						this.layoutData.Write().paddingRight = other.layoutData.Read().paddingRight;
						return;
					case StylePropertyId.PaddingTop:
						this.layoutData.Write().paddingTop = other.layoutData.Read().paddingTop;
						return;
					case StylePropertyId.Position:
						this.layoutData.Write().position = other.layoutData.Read().position;
						return;
					case StylePropertyId.Right:
						this.layoutData.Write().right = other.layoutData.Read().right;
						return;
					case StylePropertyId.Top:
						this.layoutData.Write().top = other.layoutData.Read().top;
						return;
					case StylePropertyId.Width:
						this.layoutData.Write().width = other.layoutData.Read().width;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = other.rareData.Read().cursor;
							return;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = other.rareData.Read().textOverflow;
							return;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = other.rareData.Read().unityBackgroundImageTintColor;
							return;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = other.rareData.Read().unityOverflowClipBox;
							return;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = other.rareData.Read().unitySliceBottom;
							return;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = other.rareData.Read().unitySliceLeft;
							return;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = other.rareData.Read().unitySliceRight;
							return;
						case StylePropertyId.UnitySliceScale:
							this.rareData.Write().unitySliceScale = other.rareData.Read().unitySliceScale;
							return;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = other.rareData.Read().unitySliceTop;
							return;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = other.rareData.Read().unityTextOverflowPosition;
							return;
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.Rotate:
					this.transformData.Write().rotate = other.transformData.Read().rotate;
					return;
				case StylePropertyId.Scale:
					this.transformData.Write().scale = other.transformData.Read().scale;
					return;
				case StylePropertyId.TransformOrigin:
					this.transformData.Write().transformOrigin = other.transformData.Read().transformOrigin;
					return;
				case StylePropertyId.Translate:
					this.transformData.Write().translate = other.transformData.Read().translate;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.TransitionDelay:
						this.transitionData.Write().transitionDelay.CopyFrom(other.transitionData.Read().transitionDelay);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionDuration:
						this.transitionData.Write().transitionDuration.CopyFrom(other.transitionData.Read().transitionDuration);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionProperty:
						this.transitionData.Write().transitionProperty.CopyFrom(other.transitionData.Read().transitionProperty);
						this.ResetComputedTransitions();
						return;
					case StylePropertyId.TransitionTimingFunction:
						this.transitionData.Write().transitionTimingFunction.CopyFrom(other.transitionData.Read().transitionTimingFunction);
						this.ResetComputedTransitions();
						return;
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
							this.visualData.Write().backgroundColor = other.visualData.Read().backgroundColor;
							return;
						case StylePropertyId.BackgroundImage:
							this.visualData.Write().backgroundImage = other.visualData.Read().backgroundImage;
							return;
						case StylePropertyId.BackgroundPositionX:
							this.visualData.Write().backgroundPositionX = other.visualData.Read().backgroundPositionX;
							return;
						case StylePropertyId.BackgroundPositionY:
							this.visualData.Write().backgroundPositionY = other.visualData.Read().backgroundPositionY;
							return;
						case StylePropertyId.BackgroundRepeat:
							this.visualData.Write().backgroundRepeat = other.visualData.Read().backgroundRepeat;
							return;
						case StylePropertyId.BackgroundSize:
							this.visualData.Write().backgroundSize = other.visualData.Read().backgroundSize;
							return;
						case StylePropertyId.BorderBottomColor:
							this.visualData.Write().borderBottomColor = other.visualData.Read().borderBottomColor;
							return;
						case StylePropertyId.BorderBottomLeftRadius:
							this.visualData.Write().borderBottomLeftRadius = other.visualData.Read().borderBottomLeftRadius;
							return;
						case StylePropertyId.BorderBottomRightRadius:
							this.visualData.Write().borderBottomRightRadius = other.visualData.Read().borderBottomRightRadius;
							return;
						case StylePropertyId.BorderLeftColor:
							this.visualData.Write().borderLeftColor = other.visualData.Read().borderLeftColor;
							return;
						case StylePropertyId.BorderRightColor:
							this.visualData.Write().borderRightColor = other.visualData.Read().borderRightColor;
							return;
						case StylePropertyId.BorderTopColor:
							this.visualData.Write().borderTopColor = other.visualData.Read().borderTopColor;
							return;
						case StylePropertyId.BorderTopLeftRadius:
							this.visualData.Write().borderTopLeftRadius = other.visualData.Read().borderTopLeftRadius;
							return;
						case StylePropertyId.BorderTopRightRadius:
							this.visualData.Write().borderTopRightRadius = other.visualData.Read().borderTopRightRadius;
							return;
						case StylePropertyId.Opacity:
							this.visualData.Write().opacity = other.visualData.Read().opacity;
							return;
						case StylePropertyId.Overflow:
							this.visualData.Write().overflow = other.visualData.Read().overflow;
							return;
						}
						break;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00052020 File Offset: 0x00050220
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Length newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.UnityParagraphSpacing)
			{
				if (stylePropertyId2 == StylePropertyId.FontSize)
				{
					this.inheritedData.Write().fontSize = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.LetterSpacing)
				{
					this.inheritedData.Write().letterSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.UnityParagraphSpacing)
				{
					this.inheritedData.Write().unityParagraphSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.WordSpacing)
				{
					this.inheritedData.Write().wordSpacing = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.Bottom:
					this.layoutData.Write().bottom = newValue;
					ve.layoutNode.Bottom = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Display:
				case StylePropertyId.FlexDirection:
				case StylePropertyId.FlexGrow:
				case StylePropertyId.FlexShrink:
				case StylePropertyId.FlexWrap:
				case StylePropertyId.JustifyContent:
				case StylePropertyId.Position:
					break;
				case StylePropertyId.FlexBasis:
					this.layoutData.Write().flexBasis = newValue;
					ve.layoutNode.FlexBasis = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Height:
					this.layoutData.Write().height = newValue;
					ve.layoutNode.Height = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Left:
					this.layoutData.Write().left = newValue;
					ve.layoutNode.Left = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginBottom:
					this.layoutData.Write().marginBottom = newValue;
					ve.layoutNode.MarginBottom = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginLeft:
					this.layoutData.Write().marginLeft = newValue;
					ve.layoutNode.MarginLeft = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginRight:
					this.layoutData.Write().marginRight = newValue;
					ve.layoutNode.MarginRight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MarginTop:
					this.layoutData.Write().marginTop = newValue;
					ve.layoutNode.MarginTop = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MaxHeight:
					this.layoutData.Write().maxHeight = newValue;
					ve.layoutNode.MaxHeight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MaxWidth:
					this.layoutData.Write().maxWidth = newValue;
					ve.layoutNode.MaxWidth = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MinHeight:
					this.layoutData.Write().minHeight = newValue;
					ve.layoutNode.MinHeight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.MinWidth:
					this.layoutData.Write().minWidth = newValue;
					ve.layoutNode.MinWidth = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingBottom:
					this.layoutData.Write().paddingBottom = newValue;
					ve.layoutNode.PaddingBottom = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingLeft:
					this.layoutData.Write().paddingLeft = newValue;
					ve.layoutNode.PaddingLeft = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingRight:
					this.layoutData.Write().paddingRight = newValue;
					ve.layoutNode.PaddingRight = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.PaddingTop:
					this.layoutData.Write().paddingTop = newValue;
					ve.layoutNode.PaddingTop = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Right:
					this.layoutData.Write().right = newValue;
					ve.layoutNode.Right = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Top:
					this.layoutData.Write().top = newValue;
					ve.layoutNode.Top = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.Width:
					this.layoutData.Write().width = newValue;
					ve.layoutNode.Width = newValue.ToLayoutValue();
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				default:
					switch (stylePropertyId2)
					{
					case StylePropertyId.BorderBottomLeftRadius:
						this.visualData.Write().borderBottomLeftRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.visualData.Write().borderBottomRightRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.visualData.Write().borderTopLeftRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.visualData.Write().borderTopRightRadius = newValue;
						ve.IncrementVersion(VersionChangeType.BorderRadius | VersionChangeType.Repaint);
						return;
					}
					break;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Length' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000525D4 File Offset: 0x000507D4
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, float newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.FlexShrink)
			{
				if (stylePropertyId2 == StylePropertyId.UnityTextOutlineWidth)
				{
					this.inheritedData.Write().unityTextOutlineWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.BorderBottomWidth:
					this.layoutData.Write().borderBottomWidth = newValue;
					ve.layoutNode.BorderBottomWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderLeftWidth:
					this.layoutData.Write().borderLeftWidth = newValue;
					ve.layoutNode.BorderLeftWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderRightWidth:
					this.layoutData.Write().borderRightWidth = newValue;
					ve.layoutNode.BorderRightWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.BorderTopWidth:
					this.layoutData.Write().borderTopWidth = newValue;
					ve.layoutNode.BorderTopWidth = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					return;
				case StylePropertyId.FlexGrow:
					this.layoutData.Write().flexGrow = newValue;
					ve.layoutNode.FlexGrow = newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				case StylePropertyId.FlexShrink:
					this.layoutData.Write().flexShrink = newValue;
					ve.layoutNode.FlexShrink = newValue;
					ve.IncrementVersion(VersionChangeType.Layout);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.UnitySliceScale)
				{
					this.rareData.Write().unitySliceScale = newValue;
					ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.Opacity)
				{
					this.visualData.Write().opacity = newValue;
					ve.IncrementVersion(VersionChangeType.Opacity);
					return;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'float' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000527E8 File Offset: 0x000509E8
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, int newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.Display)
			{
				if (stylePropertyId2 <= StylePropertyId.Visibility)
				{
					if (stylePropertyId2 == StylePropertyId.UnityFontStyleAndWeight)
					{
						bool flag = this.inheritedData.Read().unityFontStyleAndWeight != (FontStyle)newValue;
						if (flag)
						{
							this.inheritedData.Write().unityFontStyleAndWeight = (FontStyle)newValue;
							ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						}
						return;
					}
					if (stylePropertyId2 == StylePropertyId.UnityTextAlign)
					{
						bool flag2 = this.inheritedData.Read().unityTextAlign != (TextAnchor)newValue;
						if (flag2)
						{
							this.inheritedData.Write().unityTextAlign = (TextAnchor)newValue;
							ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
						}
						return;
					}
					if (stylePropertyId2 == StylePropertyId.Visibility)
					{
						bool flag3 = this.inheritedData.Read().visibility != (Visibility)newValue;
						if (flag3)
						{
							this.inheritedData.Write().visibility = (Visibility)newValue;
							ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint | VersionChangeType.Picking);
						}
						return;
					}
				}
				else
				{
					if (stylePropertyId2 == StylePropertyId.WhiteSpace)
					{
						bool flag4 = this.inheritedData.Read().whiteSpace != (WhiteSpace)newValue;
						if (flag4)
						{
							this.inheritedData.Write().whiteSpace = (WhiteSpace)newValue;
							ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet);
						}
						return;
					}
					switch (stylePropertyId2)
					{
					case StylePropertyId.AlignContent:
					{
						bool flag5 = this.layoutData.Read().alignContent != (Align)newValue;
						if (flag5)
						{
							this.layoutData.Write().alignContent = (Align)newValue;
							ve.layoutNode.AlignContent = (LayoutAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
						}
						return;
					}
					case StylePropertyId.AlignItems:
					{
						bool flag6 = this.layoutData.Read().alignItems != (Align)newValue;
						if (flag6)
						{
							this.layoutData.Write().alignItems = (Align)newValue;
							ve.layoutNode.AlignItems = (LayoutAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
						}
						return;
					}
					case StylePropertyId.AlignSelf:
					{
						bool flag7 = this.layoutData.Read().alignSelf != (Align)newValue;
						if (flag7)
						{
							this.layoutData.Write().alignSelf = (Align)newValue;
							ve.layoutNode.AlignSelf = (LayoutAlign)newValue;
							ve.IncrementVersion(VersionChangeType.Layout);
						}
						return;
					}
					default:
						if (stylePropertyId2 == StylePropertyId.Display)
						{
							bool flag8 = this.layoutData.Read().display != (DisplayStyle)newValue;
							if (flag8)
							{
								this.layoutData.Write().display = (DisplayStyle)newValue;
								ve.layoutNode.Display = (LayoutDisplay)newValue;
								ve.IncrementVersion(VersionChangeType.Layout);
							}
							return;
						}
						break;
					}
				}
			}
			else if (stylePropertyId2 <= StylePropertyId.JustifyContent)
			{
				if (stylePropertyId2 == StylePropertyId.FlexDirection)
				{
					bool flag9 = this.layoutData.Read().flexDirection != (FlexDirection)newValue;
					if (flag9)
					{
						this.layoutData.Write().flexDirection = (FlexDirection)newValue;
						ve.layoutNode.FlexDirection = (LayoutFlexDirection)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
				if (stylePropertyId2 == StylePropertyId.FlexWrap)
				{
					bool flag10 = this.layoutData.Read().flexWrap != (Wrap)newValue;
					if (flag10)
					{
						this.layoutData.Write().flexWrap = (Wrap)newValue;
						ve.layoutNode.Wrap = (LayoutWrap)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
				if (stylePropertyId2 == StylePropertyId.JustifyContent)
				{
					bool flag11 = this.layoutData.Read().justifyContent != (Justify)newValue;
					if (flag11)
					{
						this.layoutData.Write().justifyContent = (Justify)newValue;
						ve.layoutNode.JustifyContent = (LayoutJustify)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.Position)
				{
					bool flag12 = this.layoutData.Read().position != (Position)newValue;
					if (flag12)
					{
						this.layoutData.Write().position = (Position)newValue;
						ve.layoutNode.PositionType = (LayoutPositionType)newValue;
						ve.IncrementVersion(VersionChangeType.Layout);
					}
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.TextOverflow:
				{
					bool flag13 = this.rareData.Read().textOverflow != (TextOverflow)newValue;
					if (flag13)
					{
						this.rareData.Write().textOverflow = (TextOverflow)newValue;
						ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					}
					return;
				}
				case StylePropertyId.UnityBackgroundImageTintColor:
				case StylePropertyId.UnitySliceScale:
					break;
				case StylePropertyId.UnityOverflowClipBox:
				{
					bool flag14 = this.rareData.Read().unityOverflowClipBox != (OverflowClipBox)newValue;
					if (flag14)
					{
						this.rareData.Write().unityOverflowClipBox = (OverflowClipBox)newValue;
						ve.IncrementVersion(VersionChangeType.Repaint);
					}
					return;
				}
				case StylePropertyId.UnitySliceBottom:
					this.rareData.Write().unitySliceBottom = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceLeft:
					this.rareData.Write().unitySliceLeft = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceRight:
					this.rareData.Write().unitySliceRight = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnitySliceTop:
					this.rareData.Write().unitySliceTop = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
					return;
				case StylePropertyId.UnityTextOverflowPosition:
				{
					bool flag15 = this.rareData.Read().unityTextOverflowPosition != (TextOverflowPosition)newValue;
					if (flag15)
					{
						this.rareData.Write().unityTextOverflowPosition = (TextOverflowPosition)newValue;
						ve.IncrementVersion(VersionChangeType.Repaint);
					}
					return;
				}
				default:
					if (stylePropertyId2 == StylePropertyId.Overflow)
					{
						bool flag16 = this.visualData.Read().overflow != (OverflowInternal)newValue;
						if (flag16)
						{
							this.visualData.Write().overflow = (OverflowInternal)newValue;
							ve.layoutNode.Overflow = (LayoutOverflow)newValue;
							ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Overflow);
						}
						return;
					}
					break;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'int' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00052DE0 File Offset: 0x00050FE0
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, BackgroundPosition newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundPositionX)
			{
				if (stylePropertyId2 != StylePropertyId.BackgroundPositionY)
				{
					throw new ArgumentException("Invalid animation property id. Can't apply value of type 'BackgroundPosition' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
				}
				bool flag = this.visualData.Read().backgroundPositionY != newValue;
				if (flag)
				{
					this.visualData.Write().backgroundPositionY = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
				}
			}
			else
			{
				bool flag2 = this.visualData.Read().backgroundPositionX != newValue;
				if (flag2)
				{
					this.visualData.Write().backgroundPositionX = newValue;
					ve.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00052EA4 File Offset: 0x000510A4
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, BackgroundRepeat newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundRepeat)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'BackgroundRepeat' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.visualData.Read().backgroundRepeat != newValue;
			if (flag)
			{
				this.visualData.Write().backgroundRepeat = newValue;
				ve.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00052F24 File Offset: 0x00051124
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, BackgroundSize newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundSize)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'BackgroundSize' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.visualData.Write().backgroundSize = newValue;
			ve.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00052F88 File Offset: 0x00051188
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Color newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 <= StylePropertyId.UnityTextOutlineColor)
			{
				if (stylePropertyId2 == StylePropertyId.Color)
				{
					this.inheritedData.Write().color = newValue;
					ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Color);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.UnityTextOutlineColor)
				{
					this.inheritedData.Write().unityTextOutlineColor = newValue;
					ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
					return;
				}
			}
			else
			{
				if (stylePropertyId2 == StylePropertyId.UnityBackgroundImageTintColor)
				{
					this.rareData.Write().unityBackgroundImageTintColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
				if (stylePropertyId2 == StylePropertyId.BackgroundColor)
				{
					this.visualData.Write().backgroundColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
				switch (stylePropertyId2)
				{
				case StylePropertyId.BorderBottomColor:
					this.visualData.Write().borderBottomColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderLeftColor:
					this.visualData.Write().borderLeftColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderRightColor:
					this.visualData.Write().borderRightColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				case StylePropertyId.BorderTopColor:
					this.visualData.Write().borderTopColor = newValue;
					ve.IncrementVersion(VersionChangeType.Color);
					return;
				}
			}
			throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Color' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00053130 File Offset: 0x00051330
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Background newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.BackgroundImage)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Background' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.visualData.Read().backgroundImage != newValue;
			if (flag)
			{
				this.visualData.Write().backgroundImage = newValue;
				ve.IncrementVersion(VersionChangeType.Repaint);
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000531B0 File Offset: 0x000513B0
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Font newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.UnityFont)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Font' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.inheritedData.Read().unityFont != newValue;
			if (flag)
			{
				this.inheritedData.Write().unityFont = newValue;
				ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00053230 File Offset: 0x00051430
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, FontDefinition newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.UnityFontDefinition)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'FontDefinition' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			bool flag = this.inheritedData.Read().unityFontDefinition != newValue;
			if (flag)
			{
				this.inheritedData.Write().unityFontDefinition = newValue;
				ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Repaint);
			}
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000532B0 File Offset: 0x000514B0
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, TextShadow newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.TextShadow)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'TextShadow' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.inheritedData.Write().textShadow = newValue;
			ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Repaint);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00053314 File Offset: 0x00051514
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Translate newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Translate)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Translate' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().translate = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00053378 File Offset: 0x00051578
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, TransformOrigin newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.TransformOrigin)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'TransformOrigin' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().transformOrigin = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000533DC File Offset: 0x000515DC
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Rotate newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Rotate)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Rotate' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().rotate = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00053440 File Offset: 0x00051640
		public void ApplyPropertyAnimation(VisualElement ve, StylePropertyId id, Scale newValue)
		{
			StylePropertyId stylePropertyId = id;
			StylePropertyId stylePropertyId2 = stylePropertyId;
			if (stylePropertyId2 != StylePropertyId.Scale)
			{
				throw new ArgumentException("Invalid animation property id. Can't apply value of type 'Scale' to property '" + id.ToString() + "'. Please make sure that this property is animatable.", "id");
			}
			this.transformData.Write().scale = newValue;
			ve.IncrementVersion(VersionChangeType.Transform);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000534A4 File Offset: 0x000516A4
		public static bool StartAnimation(VisualElement element, StylePropertyId id, ref ComputedStyle oldStyle, ref ComputedStyle newStyle, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
				{
					bool result = element.styleAnimation.Start(StylePropertyId.Color, oldStyle.inheritedData.Read().color, newStyle.inheritedData.Read().color, durationMs, delayMs, easingCurve);
					bool flag = result && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return result;
				}
				case StylePropertyId.FontSize:
					return element.styleAnimation.Start(StylePropertyId.FontSize, oldStyle.inheritedData.Read().fontSize, newStyle.inheritedData.Read().fontSize, durationMs, delayMs, easingCurve);
				case StylePropertyId.LetterSpacing:
					return element.styleAnimation.Start(StylePropertyId.LetterSpacing, oldStyle.inheritedData.Read().letterSpacing, newStyle.inheritedData.Read().letterSpacing, durationMs, delayMs, easingCurve);
				case StylePropertyId.TextShadow:
					return element.styleAnimation.Start(StylePropertyId.TextShadow, oldStyle.inheritedData.Read().textShadow, newStyle.inheritedData.Read().textShadow, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityEditorTextRenderingMode:
				case StylePropertyId.UnityTextGenerator:
					break;
				case StylePropertyId.UnityFont:
					return element.styleAnimation.Start(StylePropertyId.UnityFont, oldStyle.inheritedData.Read().unityFont, newStyle.inheritedData.Read().unityFont, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityFontDefinition:
					return element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, oldStyle.inheritedData.Read().unityFontDefinition, newStyle.inheritedData.Read().unityFontDefinition, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityFontStyleAndWeight:
					return element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)oldStyle.inheritedData.Read().unityFontStyleAndWeight, (int)newStyle.inheritedData.Read().unityFontStyleAndWeight, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityParagraphSpacing:
					return element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, oldStyle.inheritedData.Read().unityParagraphSpacing, newStyle.inheritedData.Read().unityParagraphSpacing, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextAlign:
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)oldStyle.inheritedData.Read().unityTextAlign, (int)newStyle.inheritedData.Read().unityTextAlign, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextOutlineColor:
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, oldStyle.inheritedData.Read().unityTextOutlineColor, newStyle.inheritedData.Read().unityTextOutlineColor, durationMs, delayMs, easingCurve);
				case StylePropertyId.UnityTextOutlineWidth:
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, oldStyle.inheritedData.Read().unityTextOutlineWidth, newStyle.inheritedData.Read().unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				case StylePropertyId.Visibility:
					return element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)oldStyle.inheritedData.Read().visibility, (int)newStyle.inheritedData.Read().visibility, durationMs, delayMs, easingCurve);
				case StylePropertyId.WhiteSpace:
					return element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)oldStyle.inheritedData.Read().whiteSpace, (int)newStyle.inheritedData.Read().whiteSpace, durationMs, delayMs, easingCurve);
				case StylePropertyId.WordSpacing:
					return element.styleAnimation.Start(StylePropertyId.WordSpacing, oldStyle.inheritedData.Read().wordSpacing, newStyle.inheritedData.Read().wordSpacing, durationMs, delayMs, easingCurve);
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)oldStyle.layoutData.Read().alignContent, (int)newStyle.layoutData.Read().alignContent, durationMs, delayMs, easingCurve);
					case StylePropertyId.AlignItems:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)oldStyle.layoutData.Read().alignItems, (int)newStyle.layoutData.Read().alignItems, durationMs, delayMs, easingCurve);
					case StylePropertyId.AlignSelf:
						return element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)oldStyle.layoutData.Read().alignSelf, (int)newStyle.layoutData.Read().alignSelf, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderBottomWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, oldStyle.layoutData.Read().borderBottomWidth, newStyle.layoutData.Read().borderBottomWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderLeftWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, oldStyle.layoutData.Read().borderLeftWidth, newStyle.layoutData.Read().borderLeftWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderRightWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderRightWidth, oldStyle.layoutData.Read().borderRightWidth, newStyle.layoutData.Read().borderRightWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.BorderTopWidth:
						return element.styleAnimation.Start(StylePropertyId.BorderTopWidth, oldStyle.layoutData.Read().borderTopWidth, newStyle.layoutData.Read().borderTopWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.Bottom:
						return element.styleAnimation.Start(StylePropertyId.Bottom, oldStyle.layoutData.Read().bottom, newStyle.layoutData.Read().bottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.Display:
						return element.styleAnimation.StartEnum(StylePropertyId.Display, (int)oldStyle.layoutData.Read().display, (int)newStyle.layoutData.Read().display, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexBasis:
						return element.styleAnimation.Start(StylePropertyId.FlexBasis, oldStyle.layoutData.Read().flexBasis, newStyle.layoutData.Read().flexBasis, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexDirection:
						return element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)oldStyle.layoutData.Read().flexDirection, (int)newStyle.layoutData.Read().flexDirection, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexGrow:
						return element.styleAnimation.Start(StylePropertyId.FlexGrow, oldStyle.layoutData.Read().flexGrow, newStyle.layoutData.Read().flexGrow, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexShrink:
						return element.styleAnimation.Start(StylePropertyId.FlexShrink, oldStyle.layoutData.Read().flexShrink, newStyle.layoutData.Read().flexShrink, durationMs, delayMs, easingCurve);
					case StylePropertyId.FlexWrap:
						return element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)oldStyle.layoutData.Read().flexWrap, (int)newStyle.layoutData.Read().flexWrap, durationMs, delayMs, easingCurve);
					case StylePropertyId.Height:
						return element.styleAnimation.Start(StylePropertyId.Height, oldStyle.layoutData.Read().height, newStyle.layoutData.Read().height, durationMs, delayMs, easingCurve);
					case StylePropertyId.JustifyContent:
						return element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)oldStyle.layoutData.Read().justifyContent, (int)newStyle.layoutData.Read().justifyContent, durationMs, delayMs, easingCurve);
					case StylePropertyId.Left:
						return element.styleAnimation.Start(StylePropertyId.Left, oldStyle.layoutData.Read().left, newStyle.layoutData.Read().left, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginBottom:
						return element.styleAnimation.Start(StylePropertyId.MarginBottom, oldStyle.layoutData.Read().marginBottom, newStyle.layoutData.Read().marginBottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginLeft:
						return element.styleAnimation.Start(StylePropertyId.MarginLeft, oldStyle.layoutData.Read().marginLeft, newStyle.layoutData.Read().marginLeft, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginRight:
						return element.styleAnimation.Start(StylePropertyId.MarginRight, oldStyle.layoutData.Read().marginRight, newStyle.layoutData.Read().marginRight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MarginTop:
						return element.styleAnimation.Start(StylePropertyId.MarginTop, oldStyle.layoutData.Read().marginTop, newStyle.layoutData.Read().marginTop, durationMs, delayMs, easingCurve);
					case StylePropertyId.MaxHeight:
						return element.styleAnimation.Start(StylePropertyId.MaxHeight, oldStyle.layoutData.Read().maxHeight, newStyle.layoutData.Read().maxHeight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MaxWidth:
						return element.styleAnimation.Start(StylePropertyId.MaxWidth, oldStyle.layoutData.Read().maxWidth, newStyle.layoutData.Read().maxWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.MinHeight:
						return element.styleAnimation.Start(StylePropertyId.MinHeight, oldStyle.layoutData.Read().minHeight, newStyle.layoutData.Read().minHeight, durationMs, delayMs, easingCurve);
					case StylePropertyId.MinWidth:
						return element.styleAnimation.Start(StylePropertyId.MinWidth, oldStyle.layoutData.Read().minWidth, newStyle.layoutData.Read().minWidth, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingBottom:
						return element.styleAnimation.Start(StylePropertyId.PaddingBottom, oldStyle.layoutData.Read().paddingBottom, newStyle.layoutData.Read().paddingBottom, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingLeft:
						return element.styleAnimation.Start(StylePropertyId.PaddingLeft, oldStyle.layoutData.Read().paddingLeft, newStyle.layoutData.Read().paddingLeft, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingRight:
						return element.styleAnimation.Start(StylePropertyId.PaddingRight, oldStyle.layoutData.Read().paddingRight, newStyle.layoutData.Read().paddingRight, durationMs, delayMs, easingCurve);
					case StylePropertyId.PaddingTop:
						return element.styleAnimation.Start(StylePropertyId.PaddingTop, oldStyle.layoutData.Read().paddingTop, newStyle.layoutData.Read().paddingTop, durationMs, delayMs, easingCurve);
					case StylePropertyId.Position:
						return element.styleAnimation.StartEnum(StylePropertyId.Position, (int)oldStyle.layoutData.Read().position, (int)newStyle.layoutData.Read().position, durationMs, delayMs, easingCurve);
					case StylePropertyId.Right:
						return element.styleAnimation.Start(StylePropertyId.Right, oldStyle.layoutData.Read().right, newStyle.layoutData.Read().right, durationMs, delayMs, easingCurve);
					case StylePropertyId.Top:
						return element.styleAnimation.Start(StylePropertyId.Top, oldStyle.layoutData.Read().top, newStyle.layoutData.Read().top, durationMs, delayMs, easingCurve);
					case StylePropertyId.Width:
						return element.styleAnimation.Start(StylePropertyId.Width, oldStyle.layoutData.Read().width, newStyle.layoutData.Read().width, durationMs, delayMs, easingCurve);
					default:
						switch (id)
						{
						case StylePropertyId.TextOverflow:
							return element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)oldStyle.rareData.Read().textOverflow, (int)newStyle.rareData.Read().textOverflow, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityBackgroundImageTintColor:
						{
							bool result2 = element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, oldStyle.rareData.Read().unityBackgroundImageTintColor, newStyle.rareData.Read().unityBackgroundImageTintColor, durationMs, delayMs, easingCurve);
							bool flag2 = result2 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag2)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return result2;
						}
						case StylePropertyId.UnityOverflowClipBox:
							return element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)oldStyle.rareData.Read().unityOverflowClipBox, (int)newStyle.rareData.Read().unityOverflowClipBox, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceBottom:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, oldStyle.rareData.Read().unitySliceBottom, newStyle.rareData.Read().unitySliceBottom, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceLeft:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, oldStyle.rareData.Read().unitySliceLeft, newStyle.rareData.Read().unitySliceLeft, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceRight:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceRight, oldStyle.rareData.Read().unitySliceRight, newStyle.rareData.Read().unitySliceRight, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceScale:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceScale, oldStyle.rareData.Read().unitySliceScale, newStyle.rareData.Read().unitySliceScale, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnitySliceTop:
							return element.styleAnimation.Start(StylePropertyId.UnitySliceTop, oldStyle.rareData.Read().unitySliceTop, newStyle.rareData.Read().unitySliceTop, durationMs, delayMs, easingCurve);
						case StylePropertyId.UnityTextOverflowPosition:
							return element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)oldStyle.rareData.Read().unityTextOverflowPosition, (int)newStyle.rareData.Read().unityTextOverflowPosition, durationMs, delayMs, easingCurve);
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.All:
					return ComputedStyle.StartAnimationAllProperty(element, ref oldStyle, ref newStyle, durationMs, delayMs, easingCurve);
				case StylePropertyId.BackgroundPosition:
				{
					bool result3 = false;
					result3 |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, oldStyle.visualData.Read().backgroundPositionX, newStyle.visualData.Read().backgroundPositionX, durationMs, delayMs, easingCurve);
					return result3 | element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, oldStyle.visualData.Read().backgroundPositionY, newStyle.visualData.Read().backgroundPositionY, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.BorderColor:
				{
					bool result4 = false;
					result4 |= element.styleAnimation.Start(StylePropertyId.BorderTopColor, oldStyle.visualData.Read().borderTopColor, newStyle.visualData.Read().borderTopColor, durationMs, delayMs, easingCurve);
					result4 |= element.styleAnimation.Start(StylePropertyId.BorderRightColor, oldStyle.visualData.Read().borderRightColor, newStyle.visualData.Read().borderRightColor, durationMs, delayMs, easingCurve);
					result4 |= element.styleAnimation.Start(StylePropertyId.BorderBottomColor, oldStyle.visualData.Read().borderBottomColor, newStyle.visualData.Read().borderBottomColor, durationMs, delayMs, easingCurve);
					result4 |= element.styleAnimation.Start(StylePropertyId.BorderLeftColor, oldStyle.visualData.Read().borderLeftColor, newStyle.visualData.Read().borderLeftColor, durationMs, delayMs, easingCurve);
					bool flag3 = result4 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag3)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return result4;
				}
				case StylePropertyId.BorderRadius:
				{
					bool result5 = false;
					result5 |= element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, oldStyle.visualData.Read().borderTopLeftRadius, newStyle.visualData.Read().borderTopLeftRadius, durationMs, delayMs, easingCurve);
					result5 |= element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, oldStyle.visualData.Read().borderTopRightRadius, newStyle.visualData.Read().borderTopRightRadius, durationMs, delayMs, easingCurve);
					result5 |= element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, oldStyle.visualData.Read().borderBottomRightRadius, newStyle.visualData.Read().borderBottomRightRadius, durationMs, delayMs, easingCurve);
					return result5 | element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, oldStyle.visualData.Read().borderBottomLeftRadius, newStyle.visualData.Read().borderBottomLeftRadius, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.BorderWidth:
				{
					bool result6 = false;
					result6 |= element.styleAnimation.Start(StylePropertyId.BorderTopWidth, oldStyle.layoutData.Read().borderTopWidth, newStyle.layoutData.Read().borderTopWidth, durationMs, delayMs, easingCurve);
					result6 |= element.styleAnimation.Start(StylePropertyId.BorderRightWidth, oldStyle.layoutData.Read().borderRightWidth, newStyle.layoutData.Read().borderRightWidth, durationMs, delayMs, easingCurve);
					result6 |= element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, oldStyle.layoutData.Read().borderBottomWidth, newStyle.layoutData.Read().borderBottomWidth, durationMs, delayMs, easingCurve);
					return result6 | element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, oldStyle.layoutData.Read().borderLeftWidth, newStyle.layoutData.Read().borderLeftWidth, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Flex:
				{
					bool result7 = false;
					result7 |= element.styleAnimation.Start(StylePropertyId.FlexGrow, oldStyle.layoutData.Read().flexGrow, newStyle.layoutData.Read().flexGrow, durationMs, delayMs, easingCurve);
					result7 |= element.styleAnimation.Start(StylePropertyId.FlexShrink, oldStyle.layoutData.Read().flexShrink, newStyle.layoutData.Read().flexShrink, durationMs, delayMs, easingCurve);
					return result7 | element.styleAnimation.Start(StylePropertyId.FlexBasis, oldStyle.layoutData.Read().flexBasis, newStyle.layoutData.Read().flexBasis, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Margin:
				{
					bool result8 = false;
					result8 |= element.styleAnimation.Start(StylePropertyId.MarginTop, oldStyle.layoutData.Read().marginTop, newStyle.layoutData.Read().marginTop, durationMs, delayMs, easingCurve);
					result8 |= element.styleAnimation.Start(StylePropertyId.MarginRight, oldStyle.layoutData.Read().marginRight, newStyle.layoutData.Read().marginRight, durationMs, delayMs, easingCurve);
					result8 |= element.styleAnimation.Start(StylePropertyId.MarginBottom, oldStyle.layoutData.Read().marginBottom, newStyle.layoutData.Read().marginBottom, durationMs, delayMs, easingCurve);
					return result8 | element.styleAnimation.Start(StylePropertyId.MarginLeft, oldStyle.layoutData.Read().marginLeft, newStyle.layoutData.Read().marginLeft, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Padding:
				{
					bool result9 = false;
					result9 |= element.styleAnimation.Start(StylePropertyId.PaddingTop, oldStyle.layoutData.Read().paddingTop, newStyle.layoutData.Read().paddingTop, durationMs, delayMs, easingCurve);
					result9 |= element.styleAnimation.Start(StylePropertyId.PaddingRight, oldStyle.layoutData.Read().paddingRight, newStyle.layoutData.Read().paddingRight, durationMs, delayMs, easingCurve);
					result9 |= element.styleAnimation.Start(StylePropertyId.PaddingBottom, oldStyle.layoutData.Read().paddingBottom, newStyle.layoutData.Read().paddingBottom, durationMs, delayMs, easingCurve);
					return result9 | element.styleAnimation.Start(StylePropertyId.PaddingLeft, oldStyle.layoutData.Read().paddingLeft, newStyle.layoutData.Read().paddingLeft, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Transition:
					break;
				case StylePropertyId.UnityBackgroundScaleMode:
				{
					bool result10 = false;
					result10 |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, oldStyle.visualData.Read().backgroundPositionX, newStyle.visualData.Read().backgroundPositionX, durationMs, delayMs, easingCurve);
					result10 |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, oldStyle.visualData.Read().backgroundPositionY, newStyle.visualData.Read().backgroundPositionY, durationMs, delayMs, easingCurve);
					result10 |= element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, oldStyle.visualData.Read().backgroundRepeat, newStyle.visualData.Read().backgroundRepeat, durationMs, delayMs, easingCurve);
					return result10 | element.styleAnimation.Start(StylePropertyId.BackgroundSize, oldStyle.visualData.Read().backgroundSize, newStyle.visualData.Read().backgroundSize, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutline:
				{
					bool result11 = false;
					result11 |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, oldStyle.inheritedData.Read().unityTextOutlineColor, newStyle.inheritedData.Read().unityTextOutlineColor, durationMs, delayMs, easingCurve);
					return result11 | element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, oldStyle.inheritedData.Read().unityTextOutlineWidth, newStyle.inheritedData.Read().unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.Rotate:
					{
						bool result12 = element.styleAnimation.Start(StylePropertyId.Rotate, oldStyle.transformData.Read().rotate, newStyle.transformData.Read().rotate, durationMs, delayMs, easingCurve);
						bool flag4 = result12 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag4)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return result12;
					}
					case StylePropertyId.Scale:
					{
						bool result13 = element.styleAnimation.Start(StylePropertyId.Scale, oldStyle.transformData.Read().scale, newStyle.transformData.Read().scale, durationMs, delayMs, easingCurve);
						bool flag5 = result13 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag5)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return result13;
					}
					case StylePropertyId.TransformOrigin:
					{
						bool result14 = element.styleAnimation.Start(StylePropertyId.TransformOrigin, oldStyle.transformData.Read().transformOrigin, newStyle.transformData.Read().transformOrigin, durationMs, delayMs, easingCurve);
						bool flag6 = result14 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag6)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return result14;
					}
					case StylePropertyId.Translate:
					{
						bool result15 = element.styleAnimation.Start(StylePropertyId.Translate, oldStyle.transformData.Read().translate, newStyle.transformData.Read().translate, durationMs, delayMs, easingCurve);
						bool flag7 = result15 && (element.usageHints & UsageHints.DynamicTransform) == UsageHints.None;
						if (flag7)
						{
							element.usageHints |= UsageHints.DynamicTransform;
						}
						return result15;
					}
					default:
						switch (id)
						{
						case StylePropertyId.BackgroundColor:
						{
							bool result16 = element.styleAnimation.Start(StylePropertyId.BackgroundColor, oldStyle.visualData.Read().backgroundColor, newStyle.visualData.Read().backgroundColor, durationMs, delayMs, easingCurve);
							bool flag8 = result16 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag8)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return result16;
						}
						case StylePropertyId.BackgroundImage:
							return element.styleAnimation.Start(StylePropertyId.BackgroundImage, oldStyle.visualData.Read().backgroundImage, newStyle.visualData.Read().backgroundImage, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundPositionX:
							return element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, oldStyle.visualData.Read().backgroundPositionX, newStyle.visualData.Read().backgroundPositionX, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundPositionY:
							return element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, oldStyle.visualData.Read().backgroundPositionY, newStyle.visualData.Read().backgroundPositionY, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundRepeat:
							return element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, oldStyle.visualData.Read().backgroundRepeat, newStyle.visualData.Read().backgroundRepeat, durationMs, delayMs, easingCurve);
						case StylePropertyId.BackgroundSize:
							return element.styleAnimation.Start(StylePropertyId.BackgroundSize, oldStyle.visualData.Read().backgroundSize, newStyle.visualData.Read().backgroundSize, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderBottomColor:
						{
							bool result17 = element.styleAnimation.Start(StylePropertyId.BorderBottomColor, oldStyle.visualData.Read().borderBottomColor, newStyle.visualData.Read().borderBottomColor, durationMs, delayMs, easingCurve);
							bool flag9 = result17 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag9)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return result17;
						}
						case StylePropertyId.BorderBottomLeftRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, oldStyle.visualData.Read().borderBottomLeftRadius, newStyle.visualData.Read().borderBottomLeftRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderBottomRightRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, oldStyle.visualData.Read().borderBottomRightRadius, newStyle.visualData.Read().borderBottomRightRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderLeftColor:
						{
							bool result18 = element.styleAnimation.Start(StylePropertyId.BorderLeftColor, oldStyle.visualData.Read().borderLeftColor, newStyle.visualData.Read().borderLeftColor, durationMs, delayMs, easingCurve);
							bool flag10 = result18 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag10)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return result18;
						}
						case StylePropertyId.BorderRightColor:
						{
							bool result19 = element.styleAnimation.Start(StylePropertyId.BorderRightColor, oldStyle.visualData.Read().borderRightColor, newStyle.visualData.Read().borderRightColor, durationMs, delayMs, easingCurve);
							bool flag11 = result19 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag11)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return result19;
						}
						case StylePropertyId.BorderTopColor:
						{
							bool result20 = element.styleAnimation.Start(StylePropertyId.BorderTopColor, oldStyle.visualData.Read().borderTopColor, newStyle.visualData.Read().borderTopColor, durationMs, delayMs, easingCurve);
							bool flag12 = result20 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
							if (flag12)
							{
								element.usageHints |= UsageHints.DynamicColor;
							}
							return result20;
						}
						case StylePropertyId.BorderTopLeftRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, oldStyle.visualData.Read().borderTopLeftRadius, newStyle.visualData.Read().borderTopLeftRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.BorderTopRightRadius:
							return element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, oldStyle.visualData.Read().borderTopRightRadius, newStyle.visualData.Read().borderTopRightRadius, durationMs, delayMs, easingCurve);
						case StylePropertyId.Opacity:
							return element.styleAnimation.Start(StylePropertyId.Opacity, oldStyle.visualData.Read().opacity, newStyle.visualData.Read().opacity, durationMs, delayMs, easingCurve);
						case StylePropertyId.Overflow:
							return element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)oldStyle.visualData.Read().overflow, (int)newStyle.visualData.Read().overflow, durationMs, delayMs, easingCurve);
						}
						break;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00055210 File Offset: 0x00053410
		public static bool StartAnimationAllProperty(VisualElement element, ref ComputedStyle oldStyle, ref ComputedStyle newStyle, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			bool result = false;
			UsageHints usageHints = UsageHints.None;
			bool flag = !oldStyle.inheritedData.Equals(newStyle.inheritedData);
			if (flag)
			{
				readonly ref InheritedData oldData = ref oldStyle.inheritedData.Read();
				readonly ref InheritedData newData = ref newStyle.inheritedData.Read();
				bool flag2 = oldData.color != newData.color;
				if (flag2)
				{
					bool partialResult = element.styleAnimation.Start(StylePropertyId.Color, oldData.color, newData.color, durationMs, delayMs, easingCurve);
					bool flag3 = partialResult;
					if (flag3)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					result = result || partialResult;
				}
				bool flag4 = oldData.fontSize != newData.fontSize;
				if (flag4)
				{
					result |= element.styleAnimation.Start(StylePropertyId.FontSize, oldData.fontSize, newData.fontSize, durationMs, delayMs, easingCurve);
				}
				bool flag5 = oldData.letterSpacing != newData.letterSpacing;
				if (flag5)
				{
					result |= element.styleAnimation.Start(StylePropertyId.LetterSpacing, oldData.letterSpacing, newData.letterSpacing, durationMs, delayMs, easingCurve);
				}
				bool flag6 = oldData.textShadow != newData.textShadow;
				if (flag6)
				{
					result |= element.styleAnimation.Start(StylePropertyId.TextShadow, oldData.textShadow, newData.textShadow, durationMs, delayMs, easingCurve);
				}
				bool flag7 = oldData.unityFont != newData.unityFont;
				if (flag7)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnityFont, oldData.unityFont, newData.unityFont, durationMs, delayMs, easingCurve);
				}
				bool flag8 = oldData.unityFontDefinition != newData.unityFontDefinition;
				if (flag8)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, oldData.unityFontDefinition, newData.unityFontDefinition, durationMs, delayMs, easingCurve);
				}
				bool flag9 = oldData.unityFontStyleAndWeight != newData.unityFontStyleAndWeight;
				if (flag9)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)oldData.unityFontStyleAndWeight, (int)newData.unityFontStyleAndWeight, durationMs, delayMs, easingCurve);
				}
				bool flag10 = oldData.unityParagraphSpacing != newData.unityParagraphSpacing;
				if (flag10)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, oldData.unityParagraphSpacing, newData.unityParagraphSpacing, durationMs, delayMs, easingCurve);
				}
				bool flag11 = oldData.unityTextAlign != newData.unityTextAlign;
				if (flag11)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)oldData.unityTextAlign, (int)newData.unityTextAlign, durationMs, delayMs, easingCurve);
				}
				bool flag12 = oldData.unityTextOutlineColor != newData.unityTextOutlineColor;
				if (flag12)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, oldData.unityTextOutlineColor, newData.unityTextOutlineColor, durationMs, delayMs, easingCurve);
				}
				bool flag13 = oldData.unityTextOutlineWidth != newData.unityTextOutlineWidth;
				if (flag13)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, oldData.unityTextOutlineWidth, newData.unityTextOutlineWidth, durationMs, delayMs, easingCurve);
				}
				bool flag14 = oldData.visibility != newData.visibility;
				if (flag14)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)oldData.visibility, (int)newData.visibility, durationMs, delayMs, easingCurve);
				}
				bool flag15 = oldData.whiteSpace != newData.whiteSpace;
				if (flag15)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)oldData.whiteSpace, (int)newData.whiteSpace, durationMs, delayMs, easingCurve);
				}
				bool flag16 = oldData.wordSpacing != newData.wordSpacing;
				if (flag16)
				{
					result |= element.styleAnimation.Start(StylePropertyId.WordSpacing, oldData.wordSpacing, newData.wordSpacing, durationMs, delayMs, easingCurve);
				}
			}
			bool flag17 = !oldStyle.layoutData.Equals(newStyle.layoutData);
			if (flag17)
			{
				readonly ref LayoutData oldData2 = ref oldStyle.layoutData.Read();
				readonly ref LayoutData newData2 = ref newStyle.layoutData.Read();
				bool flag18 = oldData2.alignContent != newData2.alignContent;
				if (flag18)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)oldData2.alignContent, (int)newData2.alignContent, durationMs, delayMs, easingCurve);
				}
				bool flag19 = oldData2.alignItems != newData2.alignItems;
				if (flag19)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)oldData2.alignItems, (int)newData2.alignItems, durationMs, delayMs, easingCurve);
				}
				bool flag20 = oldData2.alignSelf != newData2.alignSelf;
				if (flag20)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)oldData2.alignSelf, (int)newData2.alignSelf, durationMs, delayMs, easingCurve);
				}
				bool flag21 = oldData2.borderBottomWidth != newData2.borderBottomWidth;
				if (flag21)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, oldData2.borderBottomWidth, newData2.borderBottomWidth, durationMs, delayMs, easingCurve);
				}
				bool flag22 = oldData2.borderLeftWidth != newData2.borderLeftWidth;
				if (flag22)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, oldData2.borderLeftWidth, newData2.borderLeftWidth, durationMs, delayMs, easingCurve);
				}
				bool flag23 = oldData2.borderRightWidth != newData2.borderRightWidth;
				if (flag23)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderRightWidth, oldData2.borderRightWidth, newData2.borderRightWidth, durationMs, delayMs, easingCurve);
				}
				bool flag24 = oldData2.borderTopWidth != newData2.borderTopWidth;
				if (flag24)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderTopWidth, oldData2.borderTopWidth, newData2.borderTopWidth, durationMs, delayMs, easingCurve);
				}
				bool flag25 = oldData2.bottom != newData2.bottom;
				if (flag25)
				{
					result |= element.styleAnimation.Start(StylePropertyId.Bottom, oldData2.bottom, newData2.bottom, durationMs, delayMs, easingCurve);
				}
				bool flag26 = oldData2.display != newData2.display;
				if (flag26)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.Display, (int)oldData2.display, (int)newData2.display, durationMs, delayMs, easingCurve);
				}
				bool flag27 = oldData2.flexBasis != newData2.flexBasis;
				if (flag27)
				{
					result |= element.styleAnimation.Start(StylePropertyId.FlexBasis, oldData2.flexBasis, newData2.flexBasis, durationMs, delayMs, easingCurve);
				}
				bool flag28 = oldData2.flexDirection != newData2.flexDirection;
				if (flag28)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)oldData2.flexDirection, (int)newData2.flexDirection, durationMs, delayMs, easingCurve);
				}
				bool flag29 = oldData2.flexGrow != newData2.flexGrow;
				if (flag29)
				{
					result |= element.styleAnimation.Start(StylePropertyId.FlexGrow, oldData2.flexGrow, newData2.flexGrow, durationMs, delayMs, easingCurve);
				}
				bool flag30 = oldData2.flexShrink != newData2.flexShrink;
				if (flag30)
				{
					result |= element.styleAnimation.Start(StylePropertyId.FlexShrink, oldData2.flexShrink, newData2.flexShrink, durationMs, delayMs, easingCurve);
				}
				bool flag31 = oldData2.flexWrap != newData2.flexWrap;
				if (flag31)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)oldData2.flexWrap, (int)newData2.flexWrap, durationMs, delayMs, easingCurve);
				}
				bool flag32 = oldData2.height != newData2.height;
				if (flag32)
				{
					result |= element.styleAnimation.Start(StylePropertyId.Height, oldData2.height, newData2.height, durationMs, delayMs, easingCurve);
				}
				bool flag33 = oldData2.justifyContent != newData2.justifyContent;
				if (flag33)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)oldData2.justifyContent, (int)newData2.justifyContent, durationMs, delayMs, easingCurve);
				}
				bool flag34 = oldData2.left != newData2.left;
				if (flag34)
				{
					result |= element.styleAnimation.Start(StylePropertyId.Left, oldData2.left, newData2.left, durationMs, delayMs, easingCurve);
				}
				bool flag35 = oldData2.marginBottom != newData2.marginBottom;
				if (flag35)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MarginBottom, oldData2.marginBottom, newData2.marginBottom, durationMs, delayMs, easingCurve);
				}
				bool flag36 = oldData2.marginLeft != newData2.marginLeft;
				if (flag36)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MarginLeft, oldData2.marginLeft, newData2.marginLeft, durationMs, delayMs, easingCurve);
				}
				bool flag37 = oldData2.marginRight != newData2.marginRight;
				if (flag37)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MarginRight, oldData2.marginRight, newData2.marginRight, durationMs, delayMs, easingCurve);
				}
				bool flag38 = oldData2.marginTop != newData2.marginTop;
				if (flag38)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MarginTop, oldData2.marginTop, newData2.marginTop, durationMs, delayMs, easingCurve);
				}
				bool flag39 = oldData2.maxHeight != newData2.maxHeight;
				if (flag39)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MaxHeight, oldData2.maxHeight, newData2.maxHeight, durationMs, delayMs, easingCurve);
				}
				bool flag40 = oldData2.maxWidth != newData2.maxWidth;
				if (flag40)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MaxWidth, oldData2.maxWidth, newData2.maxWidth, durationMs, delayMs, easingCurve);
				}
				bool flag41 = oldData2.minHeight != newData2.minHeight;
				if (flag41)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MinHeight, oldData2.minHeight, newData2.minHeight, durationMs, delayMs, easingCurve);
				}
				bool flag42 = oldData2.minWidth != newData2.minWidth;
				if (flag42)
				{
					result |= element.styleAnimation.Start(StylePropertyId.MinWidth, oldData2.minWidth, newData2.minWidth, durationMs, delayMs, easingCurve);
				}
				bool flag43 = oldData2.paddingBottom != newData2.paddingBottom;
				if (flag43)
				{
					result |= element.styleAnimation.Start(StylePropertyId.PaddingBottom, oldData2.paddingBottom, newData2.paddingBottom, durationMs, delayMs, easingCurve);
				}
				bool flag44 = oldData2.paddingLeft != newData2.paddingLeft;
				if (flag44)
				{
					result |= element.styleAnimation.Start(StylePropertyId.PaddingLeft, oldData2.paddingLeft, newData2.paddingLeft, durationMs, delayMs, easingCurve);
				}
				bool flag45 = oldData2.paddingRight != newData2.paddingRight;
				if (flag45)
				{
					result |= element.styleAnimation.Start(StylePropertyId.PaddingRight, oldData2.paddingRight, newData2.paddingRight, durationMs, delayMs, easingCurve);
				}
				bool flag46 = oldData2.paddingTop != newData2.paddingTop;
				if (flag46)
				{
					result |= element.styleAnimation.Start(StylePropertyId.PaddingTop, oldData2.paddingTop, newData2.paddingTop, durationMs, delayMs, easingCurve);
				}
				bool flag47 = oldData2.position != newData2.position;
				if (flag47)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.Position, (int)oldData2.position, (int)newData2.position, durationMs, delayMs, easingCurve);
				}
				bool flag48 = oldData2.right != newData2.right;
				if (flag48)
				{
					result |= element.styleAnimation.Start(StylePropertyId.Right, oldData2.right, newData2.right, durationMs, delayMs, easingCurve);
				}
				bool flag49 = oldData2.top != newData2.top;
				if (flag49)
				{
					result |= element.styleAnimation.Start(StylePropertyId.Top, oldData2.top, newData2.top, durationMs, delayMs, easingCurve);
				}
				bool flag50 = oldData2.width != newData2.width;
				if (flag50)
				{
					result |= element.styleAnimation.Start(StylePropertyId.Width, oldData2.width, newData2.width, durationMs, delayMs, easingCurve);
				}
			}
			bool flag51 = !oldStyle.rareData.Equals(newStyle.rareData);
			if (flag51)
			{
				readonly ref RareData oldData3 = ref oldStyle.rareData.Read();
				readonly ref RareData newData3 = ref newStyle.rareData.Read();
				bool flag52 = oldData3.textOverflow != newData3.textOverflow;
				if (flag52)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)oldData3.textOverflow, (int)newData3.textOverflow, durationMs, delayMs, easingCurve);
				}
				bool flag53 = oldData3.unityBackgroundImageTintColor != newData3.unityBackgroundImageTintColor;
				if (flag53)
				{
					bool partialResult2 = element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, oldData3.unityBackgroundImageTintColor, newData3.unityBackgroundImageTintColor, durationMs, delayMs, easingCurve);
					bool flag54 = partialResult2;
					if (flag54)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					result = result || partialResult2;
				}
				bool flag55 = oldData3.unityOverflowClipBox != newData3.unityOverflowClipBox;
				if (flag55)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)oldData3.unityOverflowClipBox, (int)newData3.unityOverflowClipBox, durationMs, delayMs, easingCurve);
				}
				bool flag56 = oldData3.unitySliceBottom != newData3.unitySliceBottom;
				if (flag56)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, oldData3.unitySliceBottom, newData3.unitySliceBottom, durationMs, delayMs, easingCurve);
				}
				bool flag57 = oldData3.unitySliceLeft != newData3.unitySliceLeft;
				if (flag57)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, oldData3.unitySliceLeft, newData3.unitySliceLeft, durationMs, delayMs, easingCurve);
				}
				bool flag58 = oldData3.unitySliceRight != newData3.unitySliceRight;
				if (flag58)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnitySliceRight, oldData3.unitySliceRight, newData3.unitySliceRight, durationMs, delayMs, easingCurve);
				}
				bool flag59 = oldData3.unitySliceScale != newData3.unitySliceScale;
				if (flag59)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnitySliceScale, oldData3.unitySliceScale, newData3.unitySliceScale, durationMs, delayMs, easingCurve);
				}
				bool flag60 = oldData3.unitySliceTop != newData3.unitySliceTop;
				if (flag60)
				{
					result |= element.styleAnimation.Start(StylePropertyId.UnitySliceTop, oldData3.unitySliceTop, newData3.unitySliceTop, durationMs, delayMs, easingCurve);
				}
				bool flag61 = oldData3.unityTextOverflowPosition != newData3.unityTextOverflowPosition;
				if (flag61)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)oldData3.unityTextOverflowPosition, (int)newData3.unityTextOverflowPosition, durationMs, delayMs, easingCurve);
				}
			}
			bool flag62 = !oldStyle.transformData.Equals(newStyle.transformData);
			if (flag62)
			{
				readonly ref TransformData oldData4 = ref oldStyle.transformData.Read();
				readonly ref TransformData newData4 = ref newStyle.transformData.Read();
				bool flag63 = oldData4.rotate != newData4.rotate;
				if (flag63)
				{
					bool partialResult3 = element.styleAnimation.Start(StylePropertyId.Rotate, oldData4.rotate, newData4.rotate, durationMs, delayMs, easingCurve);
					bool flag64 = partialResult3;
					if (flag64)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					result = result || partialResult3;
				}
				bool flag65 = oldData4.scale != newData4.scale;
				if (flag65)
				{
					bool partialResult4 = element.styleAnimation.Start(StylePropertyId.Scale, oldData4.scale, newData4.scale, durationMs, delayMs, easingCurve);
					bool flag66 = partialResult4;
					if (flag66)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					result = result || partialResult4;
				}
				bool flag67 = oldData4.transformOrigin != newData4.transformOrigin;
				if (flag67)
				{
					bool partialResult5 = element.styleAnimation.Start(StylePropertyId.TransformOrigin, oldData4.transformOrigin, newData4.transformOrigin, durationMs, delayMs, easingCurve);
					bool flag68 = partialResult5;
					if (flag68)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					result = result || partialResult5;
				}
				bool flag69 = oldData4.translate != newData4.translate;
				if (flag69)
				{
					bool partialResult6 = element.styleAnimation.Start(StylePropertyId.Translate, oldData4.translate, newData4.translate, durationMs, delayMs, easingCurve);
					bool flag70 = partialResult6;
					if (flag70)
					{
						usageHints |= UsageHints.DynamicTransform;
					}
					result = result || partialResult6;
				}
			}
			bool flag71 = !oldStyle.visualData.Equals(newStyle.visualData);
			if (flag71)
			{
				readonly ref VisualData oldData5 = ref oldStyle.visualData.Read();
				readonly ref VisualData newData5 = ref newStyle.visualData.Read();
				bool flag72 = oldData5.backgroundColor != newData5.backgroundColor;
				if (flag72)
				{
					bool partialResult7 = element.styleAnimation.Start(StylePropertyId.BackgroundColor, oldData5.backgroundColor, newData5.backgroundColor, durationMs, delayMs, easingCurve);
					bool flag73 = partialResult7;
					if (flag73)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					result = result || partialResult7;
				}
				bool flag74 = oldData5.backgroundImage != newData5.backgroundImage;
				if (flag74)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BackgroundImage, oldData5.backgroundImage, newData5.backgroundImage, durationMs, delayMs, easingCurve);
				}
				bool flag75 = oldData5.backgroundPositionX != newData5.backgroundPositionX;
				if (flag75)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, oldData5.backgroundPositionX, newData5.backgroundPositionX, durationMs, delayMs, easingCurve);
				}
				bool flag76 = oldData5.backgroundPositionY != newData5.backgroundPositionY;
				if (flag76)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, oldData5.backgroundPositionY, newData5.backgroundPositionY, durationMs, delayMs, easingCurve);
				}
				bool flag77 = oldData5.backgroundRepeat != newData5.backgroundRepeat;
				if (flag77)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, oldData5.backgroundRepeat, newData5.backgroundRepeat, durationMs, delayMs, easingCurve);
				}
				bool flag78 = oldData5.backgroundSize != newData5.backgroundSize;
				if (flag78)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BackgroundSize, oldData5.backgroundSize, newData5.backgroundSize, durationMs, delayMs, easingCurve);
				}
				bool flag79 = oldData5.borderBottomColor != newData5.borderBottomColor;
				if (flag79)
				{
					bool partialResult8 = element.styleAnimation.Start(StylePropertyId.BorderBottomColor, oldData5.borderBottomColor, newData5.borderBottomColor, durationMs, delayMs, easingCurve);
					bool flag80 = partialResult8;
					if (flag80)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					result = result || partialResult8;
				}
				bool flag81 = oldData5.borderBottomLeftRadius != newData5.borderBottomLeftRadius;
				if (flag81)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, oldData5.borderBottomLeftRadius, newData5.borderBottomLeftRadius, durationMs, delayMs, easingCurve);
				}
				bool flag82 = oldData5.borderBottomRightRadius != newData5.borderBottomRightRadius;
				if (flag82)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, oldData5.borderBottomRightRadius, newData5.borderBottomRightRadius, durationMs, delayMs, easingCurve);
				}
				bool flag83 = oldData5.borderLeftColor != newData5.borderLeftColor;
				if (flag83)
				{
					bool partialResult9 = element.styleAnimation.Start(StylePropertyId.BorderLeftColor, oldData5.borderLeftColor, newData5.borderLeftColor, durationMs, delayMs, easingCurve);
					bool flag84 = partialResult9;
					if (flag84)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					result = result || partialResult9;
				}
				bool flag85 = oldData5.borderRightColor != newData5.borderRightColor;
				if (flag85)
				{
					bool partialResult10 = element.styleAnimation.Start(StylePropertyId.BorderRightColor, oldData5.borderRightColor, newData5.borderRightColor, durationMs, delayMs, easingCurve);
					bool flag86 = partialResult10;
					if (flag86)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					result = result || partialResult10;
				}
				bool flag87 = oldData5.borderTopColor != newData5.borderTopColor;
				if (flag87)
				{
					bool partialResult11 = element.styleAnimation.Start(StylePropertyId.BorderTopColor, oldData5.borderTopColor, newData5.borderTopColor, durationMs, delayMs, easingCurve);
					bool flag88 = partialResult11;
					if (flag88)
					{
						usageHints |= UsageHints.DynamicColor;
					}
					result = result || partialResult11;
				}
				bool flag89 = oldData5.borderTopLeftRadius != newData5.borderTopLeftRadius;
				if (flag89)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, oldData5.borderTopLeftRadius, newData5.borderTopLeftRadius, durationMs, delayMs, easingCurve);
				}
				bool flag90 = oldData5.borderTopRightRadius != newData5.borderTopRightRadius;
				if (flag90)
				{
					result |= element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, oldData5.borderTopRightRadius, newData5.borderTopRightRadius, durationMs, delayMs, easingCurve);
				}
				bool flag91 = oldData5.opacity != newData5.opacity;
				if (flag91)
				{
					result |= element.styleAnimation.Start(StylePropertyId.Opacity, oldData5.opacity, newData5.opacity, durationMs, delayMs, easingCurve);
				}
				bool flag92 = oldData5.overflow != newData5.overflow;
				if (flag92)
				{
					result |= element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)oldData5.overflow, (int)newData5.overflow, durationMs, delayMs, easingCurve);
				}
			}
			bool flag93 = usageHints > UsageHints.None;
			if (flag93)
			{
				element.usageHints |= usageHints;
			}
			return result;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00056754 File Offset: 0x00054954
		public static bool StartAnimationInline(VisualElement element, StylePropertyId id, ref ComputedStyle computedStyle, StyleValue sv, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			if (id <= StylePropertyId.Width)
			{
				switch (id)
				{
				case StylePropertyId.Color:
				{
					Color to = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.color : sv.color);
					bool result = element.styleAnimation.Start(StylePropertyId.Color, computedStyle.inheritedData.Read().color, to, durationMs, delayMs, easingCurve);
					bool flag = result && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return result;
				}
				case StylePropertyId.FontSize:
				{
					Length to2 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.fontSize : sv.length);
					return element.styleAnimation.Start(StylePropertyId.FontSize, computedStyle.inheritedData.Read().fontSize, to2, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.LetterSpacing:
				{
					Length to3 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.letterSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.LetterSpacing, computedStyle.inheritedData.Read().letterSpacing, to3, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.TextShadow:
				case StylePropertyId.UnityEditorTextRenderingMode:
				case StylePropertyId.UnityTextGenerator:
					break;
				case StylePropertyId.UnityFont:
				{
					Font to4 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFont : (sv.resource.IsAllocated ? (sv.resource.Target as Font) : null));
					return element.styleAnimation.Start(StylePropertyId.UnityFont, computedStyle.inheritedData.Read().unityFont, to4, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityFontDefinition:
				{
					FontDefinition to5 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFontDefinition : (sv.resource.IsAllocated ? FontDefinition.FromObject(sv.resource.Target) : default(FontDefinition)));
					return element.styleAnimation.Start(StylePropertyId.UnityFontDefinition, computedStyle.inheritedData.Read().unityFontDefinition, to5, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityFontStyleAndWeight:
				{
					FontStyle to6 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityFontStyleAndWeight : ((FontStyle)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityFontStyleAndWeight, (int)computedStyle.inheritedData.Read().unityFontStyleAndWeight, (int)to6, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityParagraphSpacing:
				{
					Length to7 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityParagraphSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.UnityParagraphSpacing, computedStyle.inheritedData.Read().unityParagraphSpacing, to7, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextAlign:
				{
					TextAnchor to8 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextAlign : ((TextAnchor)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextAlign, (int)computedStyle.inheritedData.Read().unityTextAlign, (int)to8, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutlineColor:
				{
					Color to9 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOutlineColor : sv.color);
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineColor, computedStyle.inheritedData.Read().unityTextOutlineColor, to9, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOutlineWidth:
				{
					float to10 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOutlineWidth : sv.number);
					return element.styleAnimation.Start(StylePropertyId.UnityTextOutlineWidth, computedStyle.inheritedData.Read().unityTextOutlineWidth, to10, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.Visibility:
				{
					Visibility to11 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.visibility : ((Visibility)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.Visibility, (int)computedStyle.inheritedData.Read().visibility, (int)to11, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.WhiteSpace:
				{
					WhiteSpace to12 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.whiteSpace : ((WhiteSpace)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.WhiteSpace, (int)computedStyle.inheritedData.Read().whiteSpace, (int)to12, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.WordSpacing:
				{
					Length to13 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.wordSpacing : sv.length);
					return element.styleAnimation.Start(StylePropertyId.WordSpacing, computedStyle.inheritedData.Read().wordSpacing, to13, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
					{
						Align to14 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignContent : ((Align)sv.number));
						bool flag2 = sv.keyword == StyleKeyword.Auto;
						if (flag2)
						{
							to14 = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignContent, (int)computedStyle.layoutData.Read().alignContent, (int)to14, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.AlignItems:
					{
						Align to15 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignItems : ((Align)sv.number));
						bool flag3 = sv.keyword == StyleKeyword.Auto;
						if (flag3)
						{
							to15 = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignItems, (int)computedStyle.layoutData.Read().alignItems, (int)to15, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.AlignSelf:
					{
						Align to16 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.alignSelf : ((Align)sv.number));
						bool flag4 = sv.keyword == StyleKeyword.Auto;
						if (flag4)
						{
							to16 = Align.Auto;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.AlignSelf, (int)computedStyle.layoutData.Read().alignSelf, (int)to16, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomWidth:
					{
						float to17 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomWidth, computedStyle.layoutData.Read().borderBottomWidth, to17, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderLeftWidth:
					{
						float to18 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderLeftWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderLeftWidth, computedStyle.layoutData.Read().borderLeftWidth, to18, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderRightWidth:
					{
						float to19 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderRightWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderRightWidth, computedStyle.layoutData.Read().borderRightWidth, to19, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopWidth:
					{
						float to20 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopWidth : sv.number);
						return element.styleAnimation.Start(StylePropertyId.BorderTopWidth, computedStyle.layoutData.Read().borderTopWidth, to20, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Bottom:
					{
						Length to21 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.bottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Bottom, computedStyle.layoutData.Read().bottom, to21, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Display:
					{
						DisplayStyle to22 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.display : ((DisplayStyle)sv.number));
						bool flag5 = sv.keyword == StyleKeyword.None;
						if (flag5)
						{
							to22 = DisplayStyle.None;
						}
						return element.styleAnimation.StartEnum(StylePropertyId.Display, (int)computedStyle.layoutData.Read().display, (int)to22, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexBasis:
					{
						Length to23 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexBasis : sv.length);
						return element.styleAnimation.Start(StylePropertyId.FlexBasis, computedStyle.layoutData.Read().flexBasis, to23, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexDirection:
					{
						FlexDirection to24 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexDirection : ((FlexDirection)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.FlexDirection, (int)computedStyle.layoutData.Read().flexDirection, (int)to24, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexGrow:
					{
						float to25 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexGrow : sv.number);
						return element.styleAnimation.Start(StylePropertyId.FlexGrow, computedStyle.layoutData.Read().flexGrow, to25, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexShrink:
					{
						float to26 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexShrink : sv.number);
						return element.styleAnimation.Start(StylePropertyId.FlexShrink, computedStyle.layoutData.Read().flexShrink, to26, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.FlexWrap:
					{
						Wrap to27 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.flexWrap : ((Wrap)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.FlexWrap, (int)computedStyle.layoutData.Read().flexWrap, (int)to27, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Height:
					{
						Length to28 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.height : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Height, computedStyle.layoutData.Read().height, to28, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.JustifyContent:
					{
						Justify to29 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.justifyContent : ((Justify)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.JustifyContent, (int)computedStyle.layoutData.Read().justifyContent, (int)to29, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Left:
					{
						Length to30 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.left : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Left, computedStyle.layoutData.Read().left, to30, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginBottom:
					{
						Length to31 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginBottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginBottom, computedStyle.layoutData.Read().marginBottom, to31, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginLeft:
					{
						Length to32 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginLeft : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginLeft, computedStyle.layoutData.Read().marginLeft, to32, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginRight:
					{
						Length to33 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginRight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginRight, computedStyle.layoutData.Read().marginRight, to33, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MarginTop:
					{
						Length to34 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.marginTop : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MarginTop, computedStyle.layoutData.Read().marginTop, to34, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MaxHeight:
					{
						Length to35 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.maxHeight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MaxHeight, computedStyle.layoutData.Read().maxHeight, to35, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MaxWidth:
					{
						Length to36 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.maxWidth : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MaxWidth, computedStyle.layoutData.Read().maxWidth, to36, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MinHeight:
					{
						Length to37 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.minHeight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MinHeight, computedStyle.layoutData.Read().minHeight, to37, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.MinWidth:
					{
						Length to38 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.minWidth : sv.length);
						return element.styleAnimation.Start(StylePropertyId.MinWidth, computedStyle.layoutData.Read().minWidth, to38, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingBottom:
					{
						Length to39 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingBottom : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingBottom, computedStyle.layoutData.Read().paddingBottom, to39, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingLeft:
					{
						Length to40 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingLeft : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingLeft, computedStyle.layoutData.Read().paddingLeft, to40, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingRight:
					{
						Length to41 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingRight : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingRight, computedStyle.layoutData.Read().paddingRight, to41, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.PaddingTop:
					{
						Length to42 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.paddingTop : sv.length);
						return element.styleAnimation.Start(StylePropertyId.PaddingTop, computedStyle.layoutData.Read().paddingTop, to42, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Position:
					{
						Position to43 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.position : ((Position)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.Position, (int)computedStyle.layoutData.Read().position, (int)to43, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Right:
					{
						Length to44 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.right : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Right, computedStyle.layoutData.Read().right, to44, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Top:
					{
						Length to45 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.top : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Top, computedStyle.layoutData.Read().top, to45, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Width:
					{
						Length to46 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.width : sv.length);
						return element.styleAnimation.Start(StylePropertyId.Width, computedStyle.layoutData.Read().width, to46, durationMs, delayMs, easingCurve);
					}
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.TextOverflow:
				{
					TextOverflow to47 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.textOverflow : ((TextOverflow)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.TextOverflow, (int)computedStyle.rareData.Read().textOverflow, (int)to47, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityBackgroundImageTintColor:
				{
					Color to48 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityBackgroundImageTintColor : sv.color);
					bool result2 = element.styleAnimation.Start(StylePropertyId.UnityBackgroundImageTintColor, computedStyle.rareData.Read().unityBackgroundImageTintColor, to48, durationMs, delayMs, easingCurve);
					bool flag6 = result2 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
					if (flag6)
					{
						element.usageHints |= UsageHints.DynamicColor;
					}
					return result2;
				}
				case StylePropertyId.UnityOverflowClipBox:
				{
					OverflowClipBox to49 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityOverflowClipBox : ((OverflowClipBox)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityOverflowClipBox, (int)computedStyle.rareData.Read().unityOverflowClipBox, (int)to49, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceBottom:
				{
					int to50 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceBottom : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceBottom, computedStyle.rareData.Read().unitySliceBottom, to50, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceLeft:
				{
					int to51 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceLeft : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceLeft, computedStyle.rareData.Read().unitySliceLeft, to51, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceRight:
				{
					int to52 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceRight : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceRight, computedStyle.rareData.Read().unitySliceRight, to52, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceScale:
				{
					float to53 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceScale : sv.number);
					return element.styleAnimation.Start(StylePropertyId.UnitySliceScale, computedStyle.rareData.Read().unitySliceScale, to53, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnitySliceTop:
				{
					int to54 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unitySliceTop : ((int)sv.number));
					return element.styleAnimation.Start(StylePropertyId.UnitySliceTop, computedStyle.rareData.Read().unitySliceTop, to54, durationMs, delayMs, easingCurve);
				}
				case StylePropertyId.UnityTextOverflowPosition:
				{
					TextOverflowPosition to55 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.unityTextOverflowPosition : ((TextOverflowPosition)sv.number));
					return element.styleAnimation.StartEnum(StylePropertyId.UnityTextOverflowPosition, (int)computedStyle.rareData.Read().unityTextOverflowPosition, (int)to55, durationMs, delayMs, easingCurve);
				}
				default:
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
					{
						Color to56 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundColor : sv.color);
						bool result3 = element.styleAnimation.Start(StylePropertyId.BackgroundColor, computedStyle.visualData.Read().backgroundColor, to56, durationMs, delayMs, easingCurve);
						bool flag7 = result3 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag7)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return result3;
					}
					case StylePropertyId.BackgroundImage:
					{
						Background to57 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundImage : (sv.resource.IsAllocated ? Background.FromObject(sv.resource.Target) : default(Background)));
						return element.styleAnimation.Start(StylePropertyId.BackgroundImage, computedStyle.visualData.Read().backgroundImage, to57, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BackgroundPositionX:
					{
						BackgroundPosition to58 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundPositionX : sv.position);
						return element.styleAnimation.Start(StylePropertyId.BackgroundPositionX, computedStyle.visualData.Read().backgroundPositionX, to58, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BackgroundPositionY:
					{
						BackgroundPosition to59 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundPositionY : sv.position);
						return element.styleAnimation.Start(StylePropertyId.BackgroundPositionY, computedStyle.visualData.Read().backgroundPositionY, to59, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BackgroundRepeat:
					{
						BackgroundRepeat to60 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.backgroundRepeat : sv.repeat);
						return element.styleAnimation.Start(StylePropertyId.BackgroundRepeat, computedStyle.visualData.Read().backgroundRepeat, to60, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomColor:
					{
						Color to61 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomColor : sv.color);
						bool result4 = element.styleAnimation.Start(StylePropertyId.BorderBottomColor, computedStyle.visualData.Read().borderBottomColor, to61, durationMs, delayMs, easingCurve);
						bool flag8 = result4 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag8)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return result4;
					}
					case StylePropertyId.BorderBottomLeftRadius:
					{
						Length to62 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomLeftRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomLeftRadius, computedStyle.visualData.Read().borderBottomLeftRadius, to62, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderBottomRightRadius:
					{
						Length to63 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderBottomRightRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderBottomRightRadius, computedStyle.visualData.Read().borderBottomRightRadius, to63, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderLeftColor:
					{
						Color to64 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderLeftColor : sv.color);
						bool result5 = element.styleAnimation.Start(StylePropertyId.BorderLeftColor, computedStyle.visualData.Read().borderLeftColor, to64, durationMs, delayMs, easingCurve);
						bool flag9 = result5 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag9)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return result5;
					}
					case StylePropertyId.BorderRightColor:
					{
						Color to65 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderRightColor : sv.color);
						bool result6 = element.styleAnimation.Start(StylePropertyId.BorderRightColor, computedStyle.visualData.Read().borderRightColor, to65, durationMs, delayMs, easingCurve);
						bool flag10 = result6 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag10)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return result6;
					}
					case StylePropertyId.BorderTopColor:
					{
						Color to66 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopColor : sv.color);
						bool result7 = element.styleAnimation.Start(StylePropertyId.BorderTopColor, computedStyle.visualData.Read().borderTopColor, to66, durationMs, delayMs, easingCurve);
						bool flag11 = result7 && (element.usageHints & UsageHints.DynamicColor) == UsageHints.None;
						if (flag11)
						{
							element.usageHints |= UsageHints.DynamicColor;
						}
						return result7;
					}
					case StylePropertyId.BorderTopLeftRadius:
					{
						Length to67 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopLeftRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderTopLeftRadius, computedStyle.visualData.Read().borderTopLeftRadius, to67, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.BorderTopRightRadius:
					{
						Length to68 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.borderTopRightRadius : sv.length);
						return element.styleAnimation.Start(StylePropertyId.BorderTopRightRadius, computedStyle.visualData.Read().borderTopRightRadius, to68, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Opacity:
					{
						float to69 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.opacity : sv.number);
						return element.styleAnimation.Start(StylePropertyId.Opacity, computedStyle.visualData.Read().opacity, to69, durationMs, delayMs, easingCurve);
					}
					case StylePropertyId.Overflow:
					{
						OverflowInternal to70 = ((sv.keyword == StyleKeyword.Initial) ? InitialStyle.overflow : ((OverflowInternal)sv.number));
						return element.styleAnimation.StartEnum(StylePropertyId.Overflow, (int)computedStyle.visualData.Read().overflow, (int)to70, durationMs, delayMs, easingCurve);
					}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00057E80 File Offset: 0x00056080
		public void ApplyStyleTransformOrigin(TransformOrigin st)
		{
			this.transformData.Write().transformOrigin = st;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00057E94 File Offset: 0x00056094
		public void ApplyStyleTranslate(Translate translateValue)
		{
			this.transformData.Write().translate = translateValue;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00057EA8 File Offset: 0x000560A8
		public void ApplyStyleRotate(Rotate rotateValue)
		{
			this.transformData.Write().rotate = rotateValue;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00057EBC File Offset: 0x000560BC
		public void ApplyStyleScale(Scale scaleValue)
		{
			this.transformData.Write().scale = scaleValue;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00057ED0 File Offset: 0x000560D0
		public void ApplyStyleBackgroundSize(BackgroundSize backgroundSizeValue)
		{
			this.visualData.Write().backgroundSize = backgroundSizeValue;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00057EE4 File Offset: 0x000560E4
		public void ApplyInitialValue(StylePropertyReader reader)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				if (stylePropertyId != StylePropertyId.All)
				{
					this.ApplyInitialValue(reader.propertyId);
				}
				else
				{
					this.ApplyAllPropertyInitial();
				}
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00057F30 File Offset: 0x00056130
		public void ApplyInitialValue(StylePropertyId id)
		{
			if (id <= StylePropertyId.UnityTextOverflowPosition)
			{
				switch (id)
				{
				case StylePropertyId.Color:
					this.inheritedData.Write().color = InitialStyle.color;
					return;
				case StylePropertyId.FontSize:
					this.inheritedData.Write().fontSize = InitialStyle.fontSize;
					return;
				case StylePropertyId.LetterSpacing:
					this.inheritedData.Write().letterSpacing = InitialStyle.letterSpacing;
					return;
				case StylePropertyId.TextShadow:
					this.inheritedData.Write().textShadow = InitialStyle.textShadow;
					return;
				case StylePropertyId.UnityEditorTextRenderingMode:
					this.inheritedData.Write().unityEditorTextRenderingMode = InitialStyle.unityEditorTextRenderingMode;
					return;
				case StylePropertyId.UnityFont:
					this.inheritedData.Write().unityFont = InitialStyle.unityFont;
					return;
				case StylePropertyId.UnityFontDefinition:
					this.inheritedData.Write().unityFontDefinition = InitialStyle.unityFontDefinition;
					return;
				case StylePropertyId.UnityFontStyleAndWeight:
					this.inheritedData.Write().unityFontStyleAndWeight = InitialStyle.unityFontStyleAndWeight;
					return;
				case StylePropertyId.UnityParagraphSpacing:
					this.inheritedData.Write().unityParagraphSpacing = InitialStyle.unityParagraphSpacing;
					return;
				case StylePropertyId.UnityTextAlign:
					this.inheritedData.Write().unityTextAlign = InitialStyle.unityTextAlign;
					return;
				case StylePropertyId.UnityTextGenerator:
					this.inheritedData.Write().unityTextGenerator = InitialStyle.unityTextGenerator;
					return;
				case StylePropertyId.UnityTextOutlineColor:
					this.inheritedData.Write().unityTextOutlineColor = InitialStyle.unityTextOutlineColor;
					return;
				case StylePropertyId.UnityTextOutlineWidth:
					this.inheritedData.Write().unityTextOutlineWidth = InitialStyle.unityTextOutlineWidth;
					return;
				case StylePropertyId.Visibility:
					this.inheritedData.Write().visibility = InitialStyle.visibility;
					return;
				case StylePropertyId.WhiteSpace:
					this.inheritedData.Write().whiteSpace = InitialStyle.whiteSpace;
					return;
				case StylePropertyId.WordSpacing:
					this.inheritedData.Write().wordSpacing = InitialStyle.wordSpacing;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.AlignContent:
						this.layoutData.Write().alignContent = InitialStyle.alignContent;
						return;
					case StylePropertyId.AlignItems:
						this.layoutData.Write().alignItems = InitialStyle.alignItems;
						return;
					case StylePropertyId.AlignSelf:
						this.layoutData.Write().alignSelf = InitialStyle.alignSelf;
						return;
					case StylePropertyId.BorderBottomWidth:
						this.layoutData.Write().borderBottomWidth = InitialStyle.borderBottomWidth;
						return;
					case StylePropertyId.BorderLeftWidth:
						this.layoutData.Write().borderLeftWidth = InitialStyle.borderLeftWidth;
						return;
					case StylePropertyId.BorderRightWidth:
						this.layoutData.Write().borderRightWidth = InitialStyle.borderRightWidth;
						return;
					case StylePropertyId.BorderTopWidth:
						this.layoutData.Write().borderTopWidth = InitialStyle.borderTopWidth;
						return;
					case StylePropertyId.Bottom:
						this.layoutData.Write().bottom = InitialStyle.bottom;
						return;
					case StylePropertyId.Display:
						this.layoutData.Write().display = InitialStyle.display;
						return;
					case StylePropertyId.FlexBasis:
						this.layoutData.Write().flexBasis = InitialStyle.flexBasis;
						return;
					case StylePropertyId.FlexDirection:
						this.layoutData.Write().flexDirection = InitialStyle.flexDirection;
						return;
					case StylePropertyId.FlexGrow:
						this.layoutData.Write().flexGrow = InitialStyle.flexGrow;
						return;
					case StylePropertyId.FlexShrink:
						this.layoutData.Write().flexShrink = InitialStyle.flexShrink;
						return;
					case StylePropertyId.FlexWrap:
						this.layoutData.Write().flexWrap = InitialStyle.flexWrap;
						return;
					case StylePropertyId.Height:
						this.layoutData.Write().height = InitialStyle.height;
						return;
					case StylePropertyId.JustifyContent:
						this.layoutData.Write().justifyContent = InitialStyle.justifyContent;
						return;
					case StylePropertyId.Left:
						this.layoutData.Write().left = InitialStyle.left;
						return;
					case StylePropertyId.MarginBottom:
						this.layoutData.Write().marginBottom = InitialStyle.marginBottom;
						return;
					case StylePropertyId.MarginLeft:
						this.layoutData.Write().marginLeft = InitialStyle.marginLeft;
						return;
					case StylePropertyId.MarginRight:
						this.layoutData.Write().marginRight = InitialStyle.marginRight;
						return;
					case StylePropertyId.MarginTop:
						this.layoutData.Write().marginTop = InitialStyle.marginTop;
						return;
					case StylePropertyId.MaxHeight:
						this.layoutData.Write().maxHeight = InitialStyle.maxHeight;
						return;
					case StylePropertyId.MaxWidth:
						this.layoutData.Write().maxWidth = InitialStyle.maxWidth;
						return;
					case StylePropertyId.MinHeight:
						this.layoutData.Write().minHeight = InitialStyle.minHeight;
						return;
					case StylePropertyId.MinWidth:
						this.layoutData.Write().minWidth = InitialStyle.minWidth;
						return;
					case StylePropertyId.PaddingBottom:
						this.layoutData.Write().paddingBottom = InitialStyle.paddingBottom;
						return;
					case StylePropertyId.PaddingLeft:
						this.layoutData.Write().paddingLeft = InitialStyle.paddingLeft;
						return;
					case StylePropertyId.PaddingRight:
						this.layoutData.Write().paddingRight = InitialStyle.paddingRight;
						return;
					case StylePropertyId.PaddingTop:
						this.layoutData.Write().paddingTop = InitialStyle.paddingTop;
						return;
					case StylePropertyId.Position:
						this.layoutData.Write().position = InitialStyle.position;
						return;
					case StylePropertyId.Right:
						this.layoutData.Write().right = InitialStyle.right;
						return;
					case StylePropertyId.Top:
						this.layoutData.Write().top = InitialStyle.top;
						return;
					case StylePropertyId.Width:
						this.layoutData.Write().width = InitialStyle.width;
						return;
					default:
						switch (id)
						{
						case StylePropertyId.Cursor:
							this.rareData.Write().cursor = InitialStyle.cursor;
							return;
						case StylePropertyId.TextOverflow:
							this.rareData.Write().textOverflow = InitialStyle.textOverflow;
							return;
						case StylePropertyId.UnityBackgroundImageTintColor:
							this.rareData.Write().unityBackgroundImageTintColor = InitialStyle.unityBackgroundImageTintColor;
							return;
						case StylePropertyId.UnityOverflowClipBox:
							this.rareData.Write().unityOverflowClipBox = InitialStyle.unityOverflowClipBox;
							return;
						case StylePropertyId.UnitySliceBottom:
							this.rareData.Write().unitySliceBottom = InitialStyle.unitySliceBottom;
							return;
						case StylePropertyId.UnitySliceLeft:
							this.rareData.Write().unitySliceLeft = InitialStyle.unitySliceLeft;
							return;
						case StylePropertyId.UnitySliceRight:
							this.rareData.Write().unitySliceRight = InitialStyle.unitySliceRight;
							return;
						case StylePropertyId.UnitySliceScale:
							this.rareData.Write().unitySliceScale = InitialStyle.unitySliceScale;
							return;
						case StylePropertyId.UnitySliceTop:
							this.rareData.Write().unitySliceTop = InitialStyle.unitySliceTop;
							return;
						case StylePropertyId.UnityTextOverflowPosition:
							this.rareData.Write().unityTextOverflowPosition = InitialStyle.unityTextOverflowPosition;
							return;
						}
						break;
					}
					break;
				}
			}
			else if (id <= StylePropertyId.Translate)
			{
				switch (id)
				{
				case StylePropertyId.All:
					return;
				case StylePropertyId.BackgroundPosition:
					this.visualData.Write().backgroundPositionX = InitialStyle.backgroundPositionX;
					this.visualData.Write().backgroundPositionY = InitialStyle.backgroundPositionY;
					return;
				case StylePropertyId.BorderColor:
					this.visualData.Write().borderTopColor = InitialStyle.borderTopColor;
					this.visualData.Write().borderRightColor = InitialStyle.borderRightColor;
					this.visualData.Write().borderBottomColor = InitialStyle.borderBottomColor;
					this.visualData.Write().borderLeftColor = InitialStyle.borderLeftColor;
					return;
				case StylePropertyId.BorderRadius:
					this.visualData.Write().borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
					this.visualData.Write().borderTopRightRadius = InitialStyle.borderTopRightRadius;
					this.visualData.Write().borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
					this.visualData.Write().borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
					return;
				case StylePropertyId.BorderWidth:
					this.layoutData.Write().borderTopWidth = InitialStyle.borderTopWidth;
					this.layoutData.Write().borderRightWidth = InitialStyle.borderRightWidth;
					this.layoutData.Write().borderBottomWidth = InitialStyle.borderBottomWidth;
					this.layoutData.Write().borderLeftWidth = InitialStyle.borderLeftWidth;
					return;
				case StylePropertyId.Flex:
					this.layoutData.Write().flexGrow = InitialStyle.flexGrow;
					this.layoutData.Write().flexShrink = InitialStyle.flexShrink;
					this.layoutData.Write().flexBasis = InitialStyle.flexBasis;
					return;
				case StylePropertyId.Margin:
					this.layoutData.Write().marginTop = InitialStyle.marginTop;
					this.layoutData.Write().marginRight = InitialStyle.marginRight;
					this.layoutData.Write().marginBottom = InitialStyle.marginBottom;
					this.layoutData.Write().marginLeft = InitialStyle.marginLeft;
					return;
				case StylePropertyId.Padding:
					this.layoutData.Write().paddingTop = InitialStyle.paddingTop;
					this.layoutData.Write().paddingRight = InitialStyle.paddingRight;
					this.layoutData.Write().paddingBottom = InitialStyle.paddingBottom;
					this.layoutData.Write().paddingLeft = InitialStyle.paddingLeft;
					return;
				case StylePropertyId.Transition:
					this.transitionData.Write().transitionDelay.CopyFrom(InitialStyle.transitionDelay);
					this.transitionData.Write().transitionDuration.CopyFrom(InitialStyle.transitionDuration);
					this.transitionData.Write().transitionProperty.CopyFrom(InitialStyle.transitionProperty);
					this.transitionData.Write().transitionTimingFunction.CopyFrom(InitialStyle.transitionTimingFunction);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.UnityBackgroundScaleMode:
					this.visualData.Write().backgroundPositionX = InitialStyle.backgroundPositionX;
					this.visualData.Write().backgroundPositionY = InitialStyle.backgroundPositionY;
					this.visualData.Write().backgroundRepeat = InitialStyle.backgroundRepeat;
					this.visualData.Write().backgroundSize = InitialStyle.backgroundSize;
					return;
				case StylePropertyId.UnityTextOutline:
					this.inheritedData.Write().unityTextOutlineColor = InitialStyle.unityTextOutlineColor;
					this.inheritedData.Write().unityTextOutlineWidth = InitialStyle.unityTextOutlineWidth;
					return;
				default:
					switch (id)
					{
					case StylePropertyId.Rotate:
						this.transformData.Write().rotate = InitialStyle.rotate;
						return;
					case StylePropertyId.Scale:
						this.transformData.Write().scale = InitialStyle.scale;
						return;
					case StylePropertyId.TransformOrigin:
						this.transformData.Write().transformOrigin = InitialStyle.transformOrigin;
						return;
					case StylePropertyId.Translate:
						this.transformData.Write().translate = InitialStyle.translate;
						return;
					}
					break;
				}
			}
			else
			{
				switch (id)
				{
				case StylePropertyId.TransitionDelay:
					this.transitionData.Write().transitionDelay.CopyFrom(InitialStyle.transitionDelay);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionDuration:
					this.transitionData.Write().transitionDuration.CopyFrom(InitialStyle.transitionDuration);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionProperty:
					this.transitionData.Write().transitionProperty.CopyFrom(InitialStyle.transitionProperty);
					this.ResetComputedTransitions();
					return;
				case StylePropertyId.TransitionTimingFunction:
					this.transitionData.Write().transitionTimingFunction.CopyFrom(InitialStyle.transitionTimingFunction);
					this.ResetComputedTransitions();
					return;
				default:
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
						this.visualData.Write().backgroundColor = InitialStyle.backgroundColor;
						return;
					case StylePropertyId.BackgroundImage:
						this.visualData.Write().backgroundImage = InitialStyle.backgroundImage;
						return;
					case StylePropertyId.BackgroundPositionX:
						this.visualData.Write().backgroundPositionX = InitialStyle.backgroundPositionX;
						return;
					case StylePropertyId.BackgroundPositionY:
						this.visualData.Write().backgroundPositionY = InitialStyle.backgroundPositionY;
						return;
					case StylePropertyId.BackgroundRepeat:
						this.visualData.Write().backgroundRepeat = InitialStyle.backgroundRepeat;
						return;
					case StylePropertyId.BackgroundSize:
						this.visualData.Write().backgroundSize = InitialStyle.backgroundSize;
						return;
					case StylePropertyId.BorderBottomColor:
						this.visualData.Write().borderBottomColor = InitialStyle.borderBottomColor;
						return;
					case StylePropertyId.BorderBottomLeftRadius:
						this.visualData.Write().borderBottomLeftRadius = InitialStyle.borderBottomLeftRadius;
						return;
					case StylePropertyId.BorderBottomRightRadius:
						this.visualData.Write().borderBottomRightRadius = InitialStyle.borderBottomRightRadius;
						return;
					case StylePropertyId.BorderLeftColor:
						this.visualData.Write().borderLeftColor = InitialStyle.borderLeftColor;
						return;
					case StylePropertyId.BorderRightColor:
						this.visualData.Write().borderRightColor = InitialStyle.borderRightColor;
						return;
					case StylePropertyId.BorderTopColor:
						this.visualData.Write().borderTopColor = InitialStyle.borderTopColor;
						return;
					case StylePropertyId.BorderTopLeftRadius:
						this.visualData.Write().borderTopLeftRadius = InitialStyle.borderTopLeftRadius;
						return;
					case StylePropertyId.BorderTopRightRadius:
						this.visualData.Write().borderTopRightRadius = InitialStyle.borderTopRightRadius;
						return;
					case StylePropertyId.Opacity:
						this.visualData.Write().opacity = InitialStyle.opacity;
						return;
					case StylePropertyId.Overflow:
						this.visualData.Write().overflow = InitialStyle.overflow;
						return;
					}
					break;
				}
			}
			Debug.LogAssertion(string.Format("Unexpected property id {0}", id));
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00058D18 File Offset: 0x00056F18
		public void ApplyUnsetValue(StylePropertyReader reader, ref ComputedStyle parentStyle)
		{
			StylePropertyId propertyId = reader.propertyId;
			StylePropertyId stylePropertyId = propertyId;
			if (stylePropertyId != StylePropertyId.Custom)
			{
				this.ApplyUnsetValue(reader.propertyId, ref parentStyle);
			}
			else
			{
				this.RemoveCustomStyleProperty(reader);
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00058D50 File Offset: 0x00056F50
		public void ApplyUnsetValue(StylePropertyId id, ref ComputedStyle parentStyle)
		{
			switch (id)
			{
			case StylePropertyId.Color:
				this.inheritedData.Write().color = parentStyle.color;
				break;
			case StylePropertyId.FontSize:
				this.inheritedData.Write().fontSize = parentStyle.fontSize;
				break;
			case StylePropertyId.LetterSpacing:
				this.inheritedData.Write().letterSpacing = parentStyle.letterSpacing;
				break;
			case StylePropertyId.TextShadow:
				this.inheritedData.Write().textShadow = parentStyle.textShadow;
				break;
			case StylePropertyId.UnityEditorTextRenderingMode:
				this.inheritedData.Write().unityEditorTextRenderingMode = parentStyle.unityEditorTextRenderingMode;
				break;
			case StylePropertyId.UnityFont:
				this.inheritedData.Write().unityFont = parentStyle.unityFont;
				break;
			case StylePropertyId.UnityFontDefinition:
				this.inheritedData.Write().unityFontDefinition = parentStyle.unityFontDefinition;
				break;
			case StylePropertyId.UnityFontStyleAndWeight:
				this.inheritedData.Write().unityFontStyleAndWeight = parentStyle.unityFontStyleAndWeight;
				break;
			case StylePropertyId.UnityParagraphSpacing:
				this.inheritedData.Write().unityParagraphSpacing = parentStyle.unityParagraphSpacing;
				break;
			case StylePropertyId.UnityTextAlign:
				this.inheritedData.Write().unityTextAlign = parentStyle.unityTextAlign;
				break;
			case StylePropertyId.UnityTextGenerator:
				this.inheritedData.Write().unityTextGenerator = parentStyle.unityTextGenerator;
				break;
			case StylePropertyId.UnityTextOutlineColor:
				this.inheritedData.Write().unityTextOutlineColor = parentStyle.unityTextOutlineColor;
				break;
			case StylePropertyId.UnityTextOutlineWidth:
				this.inheritedData.Write().unityTextOutlineWidth = parentStyle.unityTextOutlineWidth;
				break;
			case StylePropertyId.Visibility:
				this.inheritedData.Write().visibility = parentStyle.visibility;
				break;
			case StylePropertyId.WhiteSpace:
				this.inheritedData.Write().whiteSpace = parentStyle.whiteSpace;
				break;
			case StylePropertyId.WordSpacing:
				this.inheritedData.Write().wordSpacing = parentStyle.wordSpacing;
				break;
			default:
				this.ApplyInitialValue(id);
				break;
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00058F60 File Offset: 0x00057160
		public static VersionChangeType CompareChanges(ref ComputedStyle x, ref ComputedStyle y)
		{
			VersionChangeType changes = VersionChangeType.Styles;
			bool flag = !x.layoutData.ReferenceEquals(y.layoutData);
			if (flag)
			{
				bool flag2 = x.display != y.display || x.flexGrow != y.flexGrow || x.flexShrink != y.flexShrink || x.flexWrap != y.flexWrap || x.flexDirection != y.flexDirection || x.justifyContent != y.justifyContent || x.bottom != y.bottom || x.left != y.left || x.right != y.right || x.top != y.top || x.height != y.height || x.width != y.width || x.paddingBottom != y.paddingBottom || x.paddingLeft != y.paddingLeft || x.paddingRight != y.paddingRight || x.paddingTop != y.paddingTop || x.marginBottom != y.marginBottom || x.marginLeft != y.marginLeft || x.marginRight != y.marginRight || x.marginTop != y.marginTop || x.position != y.position || x.alignContent != y.alignContent || x.alignItems != y.alignItems || x.alignSelf != y.alignSelf || x.flexBasis != y.flexBasis || x.maxHeight != y.maxHeight || x.maxWidth != y.maxWidth || x.minHeight != y.minHeight || x.minWidth != y.minWidth;
				if (flag2)
				{
					changes |= VersionChangeType.Layout;
				}
				bool flag3 = x.borderBottomWidth != y.borderBottomWidth || x.borderLeftWidth != y.borderLeftWidth || x.borderRightWidth != y.borderRightWidth || x.borderTopWidth != y.borderTopWidth;
				if (flag3)
				{
					changes |= VersionChangeType.Layout | VersionChangeType.BorderWidth | VersionChangeType.Repaint;
				}
			}
			bool flag4 = !x.inheritedData.ReferenceEquals(y.inheritedData);
			if (flag4)
			{
				bool flag5 = x.color != y.color;
				if (flag5)
				{
					changes |= VersionChangeType.Color;
				}
				bool flag6 = (changes & (VersionChangeType.Layout | VersionChangeType.Repaint)) == (VersionChangeType)0 && (x.unityFont != y.unityFont || x.unityTextGenerator != y.unityTextGenerator || x.fontSize != y.fontSize || x.unityFontDefinition != y.unityFontDefinition || x.unityFontStyleAndWeight != y.unityFontStyleAndWeight || x.unityTextOutlineWidth != y.unityTextOutlineWidth || x.letterSpacing != y.letterSpacing || x.wordSpacing != y.wordSpacing || x.unityEditorTextRenderingMode != y.unityEditorTextRenderingMode || x.unityParagraphSpacing != y.unityParagraphSpacing);
				if (flag6)
				{
					changes |= VersionChangeType.Layout | VersionChangeType.Repaint;
				}
				bool flag7 = (changes & VersionChangeType.Repaint) == (VersionChangeType)0 && (x.textShadow != y.textShadow || x.unityTextAlign != y.unityTextAlign || x.unityTextOutlineColor != y.unityTextOutlineColor);
				if (flag7)
				{
					changes |= VersionChangeType.Repaint;
				}
				bool flag8 = x.visibility != y.visibility;
				if (flag8)
				{
					changes |= VersionChangeType.Repaint | VersionChangeType.Picking;
				}
				bool flag9 = x.whiteSpace != y.whiteSpace;
				if (flag9)
				{
					changes |= VersionChangeType.Layout;
				}
			}
			bool flag10 = !x.transformData.ReferenceEquals(y.transformData);
			if (flag10)
			{
				bool flag11 = x.scale != y.scale || x.rotate != y.rotate || x.translate != y.translate || x.transformOrigin != y.transformOrigin;
				if (flag11)
				{
					changes |= VersionChangeType.Transform;
				}
			}
			bool flag12 = !x.transitionData.ReferenceEquals(y.transitionData);
			if (flag12)
			{
				bool flag13 = !ComputedTransitionUtils.SameTransitionProperty(ref x, ref y);
				if (flag13)
				{
					changes |= VersionChangeType.TransitionProperty;
				}
			}
			bool flag14 = !x.visualData.ReferenceEquals(y.visualData);
			if (flag14)
			{
				bool flag15 = (changes & VersionChangeType.Color) == (VersionChangeType)0 && (x.backgroundColor != y.backgroundColor || x.borderBottomColor != y.borderBottomColor || x.borderLeftColor != y.borderLeftColor || x.borderRightColor != y.borderRightColor || x.borderTopColor != y.borderTopColor);
				if (flag15)
				{
					changes |= VersionChangeType.Color;
				}
				bool flag16 = (changes & VersionChangeType.Repaint) == (VersionChangeType)0 && (x.backgroundImage != y.backgroundImage || x.backgroundPositionX != y.backgroundPositionX || x.backgroundPositionY != y.backgroundPositionY || x.backgroundRepeat != y.backgroundRepeat || x.backgroundSize != y.backgroundSize);
				if (flag16)
				{
					changes |= VersionChangeType.Repaint;
				}
				bool flag17 = x.borderBottomLeftRadius != y.borderBottomLeftRadius || x.borderBottomRightRadius != y.borderBottomRightRadius || x.borderTopLeftRadius != y.borderTopLeftRadius || x.borderTopRightRadius != y.borderTopRightRadius;
				if (flag17)
				{
					changes |= VersionChangeType.BorderRadius | VersionChangeType.Repaint;
				}
				bool flag18 = x.opacity != y.opacity;
				if (flag18)
				{
					changes |= VersionChangeType.Opacity;
				}
				bool flag19 = x.overflow != y.overflow;
				if (flag19)
				{
					changes |= VersionChangeType.Layout | VersionChangeType.Overflow;
				}
			}
			bool flag20 = !x.rareData.ReferenceEquals(y.rareData);
			if (flag20)
			{
				bool flag21 = x.textOverflow != y.textOverflow || x.unitySliceScale != y.unitySliceScale;
				if (flag21)
				{
					changes |= VersionChangeType.Layout | VersionChangeType.Repaint;
				}
				bool flag22 = x.unityBackgroundImageTintColor != y.unityBackgroundImageTintColor;
				if (flag22)
				{
					changes |= VersionChangeType.Color;
				}
				bool flag23 = (changes & VersionChangeType.Repaint) == (VersionChangeType)0 && (x.unityOverflowClipBox != y.unityOverflowClipBox || x.unitySliceBottom != y.unitySliceBottom || x.unitySliceLeft != y.unitySliceLeft || x.unitySliceRight != y.unitySliceRight || x.unitySliceTop != y.unitySliceTop || x.unityTextOverflowPosition != y.unityTextOverflowPosition);
				if (flag23)
				{
					changes |= VersionChangeType.Repaint;
				}
			}
			return changes;
		}

		// Token: 0x04000B03 RID: 2819
		public StyleDataRef<InheritedData> inheritedData;

		// Token: 0x04000B04 RID: 2820
		public StyleDataRef<LayoutData> layoutData;

		// Token: 0x04000B05 RID: 2821
		public StyleDataRef<RareData> rareData;

		// Token: 0x04000B06 RID: 2822
		public StyleDataRef<TransformData> transformData;

		// Token: 0x04000B07 RID: 2823
		public StyleDataRef<TransitionData> transitionData;

		// Token: 0x04000B08 RID: 2824
		public StyleDataRef<VisualData> visualData;

		// Token: 0x04000B09 RID: 2825
		public Dictionary<string, StylePropertyValue> customProperties;

		// Token: 0x04000B0A RID: 2826
		public long matchingRulesHash;

		// Token: 0x04000B0B RID: 2827
		public float dpiScaling;

		// Token: 0x04000B0C RID: 2828
		public ComputedTransitionProperty[] computedTransitions;
	}
}
