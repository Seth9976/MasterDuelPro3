using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005A8 RID: 1448
	[VisibleToOtherModules]
	internal static class ShorthandApplicator
	{
		// Token: 0x06002763 RID: 10083 RVA: 0x0009C898 File Offset: 0x0009AA98
		public static void ApplyBackgroundPosition(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			BackgroundPosition backgroundPositionX;
			BackgroundPosition backgroundPositionY;
			ShorthandApplicator.CompileBackgroundPosition(reader, out backgroundPositionX, out backgroundPositionY);
			computedStyle.visualData.Write().backgroundPositionX = backgroundPositionX;
			computedStyle.visualData.Write().backgroundPositionY = backgroundPositionY;
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x0009C8D4 File Offset: 0x0009AAD4
		public static void ApplyBorderColor(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			Color borderTopColor;
			Color borderRightColor;
			Color borderBottomColor;
			Color borderLeftColor;
			ShorthandApplicator.CompileBoxArea(reader, out borderTopColor, out borderRightColor, out borderBottomColor, out borderLeftColor);
			computedStyle.visualData.Write().borderTopColor = borderTopColor;
			computedStyle.visualData.Write().borderRightColor = borderRightColor;
			computedStyle.visualData.Write().borderBottomColor = borderBottomColor;
			computedStyle.visualData.Write().borderLeftColor = borderLeftColor;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0009C938 File Offset: 0x0009AB38
		public static void ApplyBorderRadius(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			Length borderTopLeftRadius;
			Length borderTopRightRadius;
			Length borderBottomRightRadius;
			Length borderBottomLeftRadius;
			ShorthandApplicator.CompileBorderRadius(reader, out borderTopLeftRadius, out borderTopRightRadius, out borderBottomRightRadius, out borderBottomLeftRadius);
			computedStyle.visualData.Write().borderTopLeftRadius = borderTopLeftRadius;
			computedStyle.visualData.Write().borderTopRightRadius = borderTopRightRadius;
			computedStyle.visualData.Write().borderBottomRightRadius = borderBottomRightRadius;
			computedStyle.visualData.Write().borderBottomLeftRadius = borderBottomLeftRadius;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0009C99C File Offset: 0x0009AB9C
		public static void ApplyBorderWidth(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			float borderTopWidth;
			float borderRightWidth;
			float borderBottomWidth;
			float borderLeftWidth;
			ShorthandApplicator.CompileBoxArea(reader, out borderTopWidth, out borderRightWidth, out borderBottomWidth, out borderLeftWidth);
			computedStyle.layoutData.Write().borderTopWidth = borderTopWidth;
			computedStyle.layoutData.Write().borderRightWidth = borderRightWidth;
			computedStyle.layoutData.Write().borderBottomWidth = borderBottomWidth;
			computedStyle.layoutData.Write().borderLeftWidth = borderLeftWidth;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0009CA00 File Offset: 0x0009AC00
		public static void ApplyFlex(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			float flexGrow;
			float flexShrink;
			Length flexBasis;
			ShorthandApplicator.CompileFlexShorthand(reader, out flexGrow, out flexShrink, out flexBasis);
			computedStyle.layoutData.Write().flexGrow = flexGrow;
			computedStyle.layoutData.Write().flexShrink = flexShrink;
			computedStyle.layoutData.Write().flexBasis = flexBasis;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x0009CA50 File Offset: 0x0009AC50
		public static void ApplyMargin(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			Length marginTop;
			Length marginRight;
			Length marginBottom;
			Length marginLeft;
			ShorthandApplicator.CompileBoxArea(reader, out marginTop, out marginRight, out marginBottom, out marginLeft);
			computedStyle.layoutData.Write().marginTop = marginTop;
			computedStyle.layoutData.Write().marginRight = marginRight;
			computedStyle.layoutData.Write().marginBottom = marginBottom;
			computedStyle.layoutData.Write().marginLeft = marginLeft;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0009CAB4 File Offset: 0x0009ACB4
		public static void ApplyPadding(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			Length paddingTop;
			Length paddingRight;
			Length paddingBottom;
			Length paddingLeft;
			ShorthandApplicator.CompileBoxArea(reader, out paddingTop, out paddingRight, out paddingBottom, out paddingLeft);
			computedStyle.layoutData.Write().paddingTop = paddingTop;
			computedStyle.layoutData.Write().paddingRight = paddingRight;
			computedStyle.layoutData.Write().paddingBottom = paddingBottom;
			computedStyle.layoutData.Write().paddingLeft = paddingLeft;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x0009CB18 File Offset: 0x0009AD18
		public static void ApplyTransition(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			List<TimeValue> transitionDelay;
			List<TimeValue> transitionDuration;
			List<StylePropertyName> transitionProperty;
			List<EasingFunction> transitionTimingFunction;
			ShorthandApplicator.CompileTransition(reader, out transitionDelay, out transitionDuration, out transitionProperty, out transitionTimingFunction);
			computedStyle.transitionData.Write().transitionDelay.CopyFrom(transitionDelay);
			computedStyle.transitionData.Write().transitionDuration.CopyFrom(transitionDuration);
			computedStyle.transitionData.Write().transitionProperty.CopyFrom(transitionProperty);
			computedStyle.transitionData.Write().transitionTimingFunction.CopyFrom(transitionTimingFunction);
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x0009CB94 File Offset: 0x0009AD94
		public static void ApplyUnityBackgroundScaleMode(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			BackgroundPosition backgroundPositionX;
			BackgroundPosition backgroundPositionY;
			BackgroundRepeat backgroundRepeat;
			BackgroundSize backgroundSize;
			ShorthandApplicator.CompileUnityBackgroundScaleMode(reader, out backgroundPositionX, out backgroundPositionY, out backgroundRepeat, out backgroundSize);
			computedStyle.visualData.Write().backgroundPositionX = backgroundPositionX;
			computedStyle.visualData.Write().backgroundPositionY = backgroundPositionY;
			computedStyle.visualData.Write().backgroundRepeat = backgroundRepeat;
			computedStyle.visualData.Write().backgroundSize = backgroundSize;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x0009CBF8 File Offset: 0x0009ADF8
		public static void ApplyUnityTextOutline(StylePropertyReader reader, ref ComputedStyle computedStyle)
		{
			Color unityTextOutlineColor;
			float unityTextOutlineWidth;
			ShorthandApplicator.CompileTextOutline(reader, out unityTextOutlineColor, out unityTextOutlineWidth);
			computedStyle.inheritedData.Write().unityTextOutlineColor = unityTextOutlineColor;
			computedStyle.inheritedData.Write().unityTextOutlineWidth = unityTextOutlineWidth;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0009CC34 File Offset: 0x0009AE34
		private static bool CompileFlexShorthand(StylePropertyReader reader, out float grow, out float shrink, out Length basis)
		{
			grow = 0f;
			shrink = 1f;
			basis = Length.Auto();
			bool valid = false;
			int valueCount = reader.valueCount;
			bool flag = valueCount == 1 && reader.IsValueType(0, StyleValueType.Keyword);
			if (flag)
			{
				bool flag2 = reader.IsKeyword(0, StyleValueKeyword.None);
				if (flag2)
				{
					valid = true;
					grow = 0f;
					shrink = 0f;
					basis = Length.Auto();
				}
				else
				{
					bool flag3 = reader.IsKeyword(0, StyleValueKeyword.Auto);
					if (flag3)
					{
						valid = true;
						grow = 1f;
						shrink = 1f;
						basis = Length.Auto();
					}
				}
			}
			else
			{
				bool flag4 = valueCount <= 3;
				if (flag4)
				{
					valid = true;
					grow = 0f;
					shrink = 1f;
					basis = Length.Percent(0f);
					bool growFound = false;
					bool basisFound = false;
					int i = 0;
					while (i < valueCount && valid)
					{
						StyleValueType valueType = reader.GetValueType(i);
						bool flag5 = valueType == StyleValueType.Dimension || valueType == StyleValueType.Keyword;
						if (flag5)
						{
							bool flag6 = basisFound;
							if (flag6)
							{
								valid = false;
								break;
							}
							basisFound = true;
							bool flag7 = valueType == StyleValueType.Keyword;
							if (flag7)
							{
								bool flag8 = reader.IsKeyword(i, StyleValueKeyword.Auto);
								if (flag8)
								{
									basis = Length.Auto();
								}
							}
							else
							{
								bool flag9 = valueType == StyleValueType.Dimension;
								if (flag9)
								{
									basis = reader.ReadLength(i);
								}
							}
							bool flag10 = growFound && i != valueCount - 1;
							if (flag10)
							{
								valid = false;
							}
						}
						else
						{
							bool flag11 = valueType == StyleValueType.Float;
							if (flag11)
							{
								float value = reader.ReadFloat(i);
								bool flag12 = !growFound;
								if (flag12)
								{
									growFound = true;
									grow = value;
								}
								else
								{
									shrink = value;
								}
							}
							else
							{
								valid = false;
							}
						}
						i++;
					}
				}
			}
			return valid;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0009CE00 File Offset: 0x0009B000
		private static void CompileBorderRadius(StylePropertyReader reader, out Length top, out Length right, out Length bottom, out Length left)
		{
			ShorthandApplicator.CompileBoxArea(reader, out top, out right, out bottom, out left);
			bool flag = top.IsAuto() || top.IsNone();
			if (flag)
			{
				top = 0f;
			}
			bool flag2 = right.IsAuto() || right.IsNone();
			if (flag2)
			{
				right = 0f;
			}
			bool flag3 = bottom.IsAuto() || bottom.IsNone();
			if (flag3)
			{
				bottom = 0f;
			}
			bool flag4 = left.IsAuto() || left.IsNone();
			if (flag4)
			{
				left = 0f;
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0009CEB4 File Offset: 0x0009B0B4
		private static void CompileBackgroundPosition(StylePropertyReader reader, out BackgroundPosition backgroundPositionX, out BackgroundPosition backgroundPositionY)
		{
			int valCount = reader.valueCount;
			StylePropertyValue val = reader.GetValue(0);
			StylePropertyValue val2 = ((valCount > 1) ? reader.GetValue(1) : default(StylePropertyValue));
			StylePropertyValue val3 = ((valCount > 2) ? reader.GetValue(2) : default(StylePropertyValue));
			StylePropertyValue val4 = ((valCount > 3) ? reader.GetValue(3) : default(StylePropertyValue));
			backgroundPositionX = default(BackgroundPosition);
			backgroundPositionY = default(BackgroundPosition);
			bool flag = valCount == 1;
			if (flag)
			{
				BackgroundPositionKeyword keyword = (BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 0);
				bool flag2 = keyword == BackgroundPositionKeyword.Left;
				if (flag2)
				{
					backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Left);
					backgroundPositionY = BackgroundPosition.Initial();
				}
				else
				{
					bool flag3 = keyword == BackgroundPositionKeyword.Right;
					if (flag3)
					{
						backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Right);
						backgroundPositionY = BackgroundPosition.Initial();
					}
					else
					{
						bool flag4 = keyword == BackgroundPositionKeyword.Top;
						if (flag4)
						{
							backgroundPositionX = BackgroundPosition.Initial();
							backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Top);
						}
						else
						{
							bool flag5 = keyword == BackgroundPositionKeyword.Bottom;
							if (flag5)
							{
								backgroundPositionX = BackgroundPosition.Initial();
								backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Bottom);
							}
							else
							{
								bool flag6 = keyword == BackgroundPositionKeyword.Center;
								if (flag6)
								{
									backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Center);
									backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Center);
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag7 = valCount == 2;
				if (flag7)
				{
					bool flag8 = (val.handle.valueType == StyleValueType.Dimension || val.handle.valueType == StyleValueType.Float) && (val.handle.valueType == StyleValueType.Dimension || val.handle.valueType == StyleValueType.Float);
					if (flag8)
					{
						backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Left, val.sheet.ReadDimension(val.handle).ToLength());
						backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Top, val2.sheet.ReadDimension(val2.handle).ToLength());
					}
					else
					{
						bool flag9 = val.handle.valueType == StyleValueType.Enum && val2.handle.valueType == StyleValueType.Enum;
						if (flag9)
						{
							BackgroundPositionKeyword keyword2 = (BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 0);
							BackgroundPositionKeyword keyword3 = (BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 1);
							bool flag10 = keyword3 == BackgroundPositionKeyword.Left;
							if (flag10)
							{
								ShorthandApplicator.<CompileBackgroundPosition>g__SwapKeyword|16_0(ref keyword2, ref keyword3);
							}
							bool flag11 = keyword3 == BackgroundPositionKeyword.Right;
							if (flag11)
							{
								ShorthandApplicator.<CompileBackgroundPosition>g__SwapKeyword|16_0(ref keyword2, ref keyword3);
							}
							bool flag12 = keyword2 == BackgroundPositionKeyword.Top;
							if (flag12)
							{
								ShorthandApplicator.<CompileBackgroundPosition>g__SwapKeyword|16_0(ref keyword2, ref keyword3);
							}
							bool flag13 = keyword2 == BackgroundPositionKeyword.Bottom;
							if (flag13)
							{
								ShorthandApplicator.<CompileBackgroundPosition>g__SwapKeyword|16_0(ref keyword2, ref keyword3);
							}
							backgroundPositionX = new BackgroundPosition(keyword2);
							backgroundPositionY = new BackgroundPosition(keyword3);
						}
					}
				}
				else
				{
					bool flag14 = valCount == 3;
					if (flag14)
					{
						bool flag15 = val.handle.valueType == StyleValueType.Enum && val2.handle.valueType == StyleValueType.Enum && val3.handle.valueType == StyleValueType.Dimension;
						if (flag15)
						{
							backgroundPositionX = new BackgroundPosition((BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 0));
							backgroundPositionY = new BackgroundPosition((BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 1), reader.ReadLength(2));
						}
						else
						{
							bool flag16 = val.handle.valueType == StyleValueType.Enum && val2.handle.valueType == StyleValueType.Dimension && val3.handle.valueType == StyleValueType.Enum;
							if (flag16)
							{
								backgroundPositionX = new BackgroundPosition((BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 0), reader.ReadLength(1));
								backgroundPositionY = new BackgroundPosition((BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 2));
							}
						}
					}
					else
					{
						bool flag17 = valCount == 4;
						if (flag17)
						{
							bool flag18 = val.handle.valueType == StyleValueType.Enum && val2.handle.valueType == StyleValueType.Dimension && val3.handle.valueType == StyleValueType.Enum && val4.handle.valueType == StyleValueType.Dimension;
							if (flag18)
							{
								backgroundPositionX = new BackgroundPosition((BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 0), reader.ReadLength(1));
								backgroundPositionY = new BackgroundPosition((BackgroundPositionKeyword)reader.ReadEnum(StyleEnumType.BackgroundPositionKeyword, 2), reader.ReadLength(3));
							}
						}
					}
				}
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0009D2CC File Offset: 0x0009B4CC
		public static void CompileUnityBackgroundScaleMode(StylePropertyReader reader, out BackgroundPosition backgroundPositionX, out BackgroundPosition backgroundPositionY, out BackgroundRepeat backgroundRepeat, out BackgroundSize backgroundSize)
		{
			ScaleMode scaleMode = (ScaleMode)reader.ReadEnum(StyleEnumType.ScaleMode, 0);
			backgroundPositionX = BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(scaleMode);
			backgroundPositionY = BackgroundPropertyHelper.ConvertScaleModeToBackgroundPosition(scaleMode);
			backgroundRepeat = BackgroundPropertyHelper.ConvertScaleModeToBackgroundRepeat(scaleMode);
			backgroundSize = BackgroundPropertyHelper.ConvertScaleModeToBackgroundSize(scaleMode);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0009D318 File Offset: 0x0009B518
		private static void CompileBoxArea(StylePropertyReader reader, out Length top, out Length right, out Length bottom, out Length left)
		{
			top = 0f;
			right = 0f;
			bottom = 0f;
			left = 0f;
			switch (reader.valueCount)
			{
			case 0:
				break;
			case 1:
				top = (right = (bottom = (left = reader.ReadLength(0))));
				break;
			case 2:
				top = (bottom = reader.ReadLength(0));
				left = (right = reader.ReadLength(1));
				break;
			case 3:
				top = reader.ReadLength(0);
				left = (right = reader.ReadLength(1));
				bottom = reader.ReadLength(2);
				break;
			default:
				top = reader.ReadLength(0);
				right = reader.ReadLength(1);
				bottom = reader.ReadLength(2);
				left = reader.ReadLength(3);
				break;
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0009D460 File Offset: 0x0009B660
		private static void CompileBoxArea(StylePropertyReader reader, out float top, out float right, out float bottom, out float left)
		{
			Length t;
			Length r;
			Length b;
			Length i;
			ShorthandApplicator.CompileBoxArea(reader, out t, out r, out b, out i);
			top = t.value;
			right = r.value;
			bottom = b.value;
			left = i.value;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0009D4A4 File Offset: 0x0009B6A4
		private static void CompileBoxArea(StylePropertyReader reader, out Color top, out Color right, out Color bottom, out Color left)
		{
			top = Color.clear;
			right = Color.clear;
			bottom = Color.clear;
			left = Color.clear;
			switch (reader.valueCount)
			{
			case 0:
				break;
			case 1:
				top = (right = (bottom = (left = reader.ReadColor(0))));
				break;
			case 2:
				top = (bottom = reader.ReadColor(0));
				left = (right = reader.ReadColor(1));
				break;
			case 3:
				top = reader.ReadColor(0);
				left = (right = reader.ReadColor(1));
				bottom = reader.ReadColor(2);
				break;
			default:
				top = reader.ReadColor(0);
				right = reader.ReadColor(1);
				bottom = reader.ReadColor(2);
				left = reader.ReadColor(3);
				break;
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0009D5D8 File Offset: 0x0009B7D8
		private static void CompileTextOutline(StylePropertyReader reader, out Color outlineColor, out float outlineWidth)
		{
			outlineColor = Color.clear;
			outlineWidth = 0f;
			int valueCount = reader.valueCount;
			for (int i = 0; i < valueCount; i++)
			{
				StyleValueType valueType = reader.GetValueType(i);
				bool flag = valueType == StyleValueType.Dimension;
				if (flag)
				{
					outlineWidth = reader.ReadFloat(i);
				}
				else
				{
					bool flag2 = valueType == StyleValueType.Enum || valueType == StyleValueType.Color;
					if (flag2)
					{
						outlineColor = reader.ReadColor(i);
					}
				}
			}
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0009D64C File Offset: 0x0009B84C
		private static void CompileTransition(StylePropertyReader reader, out List<TimeValue> outDelay, out List<TimeValue> outDuration, out List<StylePropertyName> outProperty, out List<EasingFunction> outTimingFunction)
		{
			ShorthandApplicator.s_TransitionDelayList.Clear();
			ShorthandApplicator.s_TransitionDurationList.Clear();
			ShorthandApplicator.s_TransitionPropertyList.Clear();
			ShorthandApplicator.s_TransitionTimingFunctionList.Clear();
			bool isValid = true;
			bool noneFound = false;
			int valueCount = reader.valueCount;
			int transitionCount = 0;
			int i = 0;
			for (;;)
			{
				bool flag = noneFound;
				if (flag)
				{
					break;
				}
				StylePropertyName transitionProperty = InitialStyle.transitionProperty[0];
				TimeValue transitionDuration = InitialStyle.transitionDuration[0];
				TimeValue transitionDelay = InitialStyle.transitionDelay[0];
				EasingFunction transitionTimingFunction = InitialStyle.transitionTimingFunction[0];
				bool durationFound = false;
				bool delayFound = false;
				bool propertyFound = false;
				bool timingFunctionFound = false;
				bool commaFound = false;
				while (i < valueCount && !commaFound)
				{
					StyleValueType valueType = reader.GetValueType(i);
					StyleValueType styleValueType = valueType;
					StyleValueType styleValueType2 = styleValueType;
					if (styleValueType2 <= StyleValueType.Dimension)
					{
						if (styleValueType2 != StyleValueType.Keyword)
						{
							if (styleValueType2 != StyleValueType.Dimension)
							{
								goto IL_01A3;
							}
							TimeValue time = reader.ReadTimeValue(i);
							bool flag2 = !durationFound;
							if (flag2)
							{
								durationFound = true;
								transitionDuration = time;
							}
							else
							{
								bool flag3 = !delayFound;
								if (flag3)
								{
									delayFound = true;
									transitionDelay = time;
								}
								else
								{
									isValid = false;
								}
							}
						}
						else
						{
							bool flag4 = reader.IsKeyword(i, StyleValueKeyword.None) && transitionCount == 0;
							if (flag4)
							{
								noneFound = true;
								propertyFound = true;
								transitionProperty = new StylePropertyName("none");
							}
							else
							{
								isValid = false;
							}
						}
					}
					else if (styleValueType2 != StyleValueType.Enum)
					{
						if (styleValueType2 != StyleValueType.CommaSeparator)
						{
							goto IL_01A3;
						}
						commaFound = true;
						transitionCount++;
					}
					else
					{
						string str = reader.ReadAsString(i);
						int intValue;
						bool flag5 = !timingFunctionFound && StylePropertyUtil.TryGetEnumIntValue(StyleEnumType.EasingMode, str, out intValue);
						if (flag5)
						{
							timingFunctionFound = true;
							transitionTimingFunction = (EasingMode)intValue;
						}
						else
						{
							bool flag6 = !propertyFound;
							if (flag6)
							{
								propertyFound = true;
								transitionProperty = new StylePropertyName(str);
							}
							else
							{
								isValid = false;
							}
						}
					}
					IL_01A7:
					i++;
					continue;
					IL_01A3:
					isValid = false;
					goto IL_01A7;
				}
				ShorthandApplicator.s_TransitionDelayList.Add(transitionDelay);
				ShorthandApplicator.s_TransitionDurationList.Add(transitionDuration);
				ShorthandApplicator.s_TransitionPropertyList.Add(transitionProperty);
				ShorthandApplicator.s_TransitionTimingFunctionList.Add(transitionTimingFunction);
				if (i >= valueCount || !isValid)
				{
					goto IL_0209;
				}
			}
			isValid = false;
			IL_0209:
			bool flag7 = isValid;
			if (flag7)
			{
				outProperty = ShorthandApplicator.s_TransitionPropertyList;
				outDelay = ShorthandApplicator.s_TransitionDelayList;
				outDuration = ShorthandApplicator.s_TransitionDurationList;
				outTimingFunction = ShorthandApplicator.s_TransitionTimingFunctionList;
			}
			else
			{
				outProperty = InitialStyle.transitionProperty;
				outDelay = InitialStyle.transitionDelay;
				outDuration = InitialStyle.transitionDuration;
				outTimingFunction = InitialStyle.transitionTimingFunction;
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0009D8D4 File Offset: 0x0009BAD4
		[CompilerGenerated]
		internal static void <CompileBackgroundPosition>g__SwapKeyword|16_0(ref BackgroundPositionKeyword a, ref BackgroundPositionKeyword b)
		{
			BackgroundPositionKeyword temp = a;
			a = b;
			b = temp;
		}

		// Token: 0x04001455 RID: 5205
		private static List<TimeValue> s_TransitionDelayList = new List<TimeValue>();

		// Token: 0x04001456 RID: 5206
		private static List<TimeValue> s_TransitionDurationList = new List<TimeValue>();

		// Token: 0x04001457 RID: 5207
		private static List<StylePropertyName> s_TransitionPropertyList = new List<StylePropertyName>();

		// Token: 0x04001458 RID: 5208
		private static List<EasingFunction> s_TransitionTimingFunctionList = new List<EasingFunction>();
	}
}
