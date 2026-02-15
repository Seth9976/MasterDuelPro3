using System;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005DD RID: 1501
	internal static class Lerp
	{
		// Token: 0x060028A0 RID: 10400 RVA: 0x000A72B4 File Offset: 0x000A54B4
		public static float Interpolate(float start, float end, float ratio)
		{
			return Mathf.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000A72D0 File Offset: 0x000A54D0
		public static Color Interpolate(Color start, Color end, float ratio)
		{
			return Color.LerpUnclamped(start, end, ratio);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000A72EC File Offset: 0x000A54EC
		internal static StyleValues Interpolate(StyleValues start, StyleValues end, float ratio)
		{
			StyleValues result = default(StyleValues);
			bool flag = end.m_StyleValues != null;
			if (flag)
			{
				foreach (StyleValue endValue in end.m_StyleValues.m_Values)
				{
					StyleValue startValue = default(StyleValue);
					bool flag2 = !start.m_StyleValues.TryGetStyleValue(endValue.id, ref startValue);
					if (flag2)
					{
						throw new ArgumentException("Start StyleValues must contain the same values as end values. Missing property:" + endValue.id.ToString());
					}
					StylePropertyId id = endValue.id;
					StylePropertyId stylePropertyId = id;
					if (stylePropertyId <= StylePropertyId.UnityTextAlign)
					{
						if (stylePropertyId <= StylePropertyId.Color)
						{
							if (stylePropertyId - StylePropertyId.Custom <= 1)
							{
								goto IL_02A5;
							}
							if (stylePropertyId != StylePropertyId.Color)
							{
								goto IL_02A5;
							}
							goto IL_0280;
						}
						else
						{
							if (stylePropertyId == StylePropertyId.FontSize)
							{
								goto IL_025B;
							}
							switch (stylePropertyId)
							{
							case StylePropertyId.UnityFont:
							case StylePropertyId.UnityFontDefinition:
							case StylePropertyId.UnityFontStyleAndWeight:
							case StylePropertyId.UnityParagraphSpacing:
							case StylePropertyId.UnityTextAlign:
								goto IL_02A5;
							default:
								goto IL_02A5;
							}
						}
					}
					else if (stylePropertyId <= StylePropertyId.Width)
					{
						if (stylePropertyId - StylePropertyId.Visibility <= 1)
						{
							goto IL_02A5;
						}
						switch (stylePropertyId)
						{
						case StylePropertyId.AlignContent:
						case StylePropertyId.AlignItems:
						case StylePropertyId.AlignSelf:
						case StylePropertyId.Display:
						case StylePropertyId.FlexDirection:
						case StylePropertyId.FlexWrap:
						case StylePropertyId.JustifyContent:
						case StylePropertyId.Position:
							goto IL_02A5;
						case StylePropertyId.BorderBottomWidth:
						case StylePropertyId.BorderLeftWidth:
						case StylePropertyId.BorderRightWidth:
						case StylePropertyId.BorderTopWidth:
						case StylePropertyId.Bottom:
						case StylePropertyId.FlexBasis:
						case StylePropertyId.FlexGrow:
						case StylePropertyId.FlexShrink:
						case StylePropertyId.Height:
						case StylePropertyId.Left:
						case StylePropertyId.MarginBottom:
						case StylePropertyId.MarginLeft:
						case StylePropertyId.MarginRight:
						case StylePropertyId.MarginTop:
						case StylePropertyId.MaxHeight:
						case StylePropertyId.MaxWidth:
						case StylePropertyId.MinHeight:
						case StylePropertyId.MinWidth:
						case StylePropertyId.PaddingBottom:
						case StylePropertyId.PaddingLeft:
						case StylePropertyId.PaddingRight:
						case StylePropertyId.PaddingTop:
						case StylePropertyId.Right:
						case StylePropertyId.Top:
						case StylePropertyId.Width:
							goto IL_025B;
						default:
							goto IL_02A5;
						}
					}
					else
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.Cursor:
						case StylePropertyId.TextOverflow:
						case StylePropertyId.UnityOverflowClipBox:
						case StylePropertyId.UnitySliceBottom:
						case StylePropertyId.UnitySliceLeft:
						case StylePropertyId.UnitySliceRight:
						case StylePropertyId.UnitySliceScale:
						case StylePropertyId.UnitySliceTop:
						case StylePropertyId.UnityTextOverflowPosition:
							goto IL_02A5;
						case StylePropertyId.UnityBackgroundImageTintColor:
							goto IL_0280;
						default:
							switch (stylePropertyId)
							{
							case StylePropertyId.BackgroundPosition:
							case StylePropertyId.BorderRadius:
							case StylePropertyId.BorderWidth:
							case StylePropertyId.Flex:
							case StylePropertyId.Margin:
							case StylePropertyId.Padding:
							case StylePropertyId.Transition:
							case StylePropertyId.UnityBackgroundScaleMode:
								goto IL_02A5;
							case StylePropertyId.BorderColor:
								goto IL_0280;
							default:
								switch (stylePropertyId)
								{
								case StylePropertyId.BackgroundColor:
									goto IL_0280;
								case StylePropertyId.BackgroundImage:
								case StylePropertyId.BackgroundPositionX:
								case StylePropertyId.BackgroundPositionY:
								case StylePropertyId.BackgroundRepeat:
								case StylePropertyId.BackgroundSize:
								case StylePropertyId.BorderBottomColor:
								case StylePropertyId.BorderLeftColor:
								case StylePropertyId.BorderRightColor:
								case StylePropertyId.BorderTopColor:
								case StylePropertyId.Overflow:
									goto IL_02A5;
								case StylePropertyId.BorderBottomLeftRadius:
								case StylePropertyId.BorderBottomRightRadius:
								case StylePropertyId.BorderTopLeftRadius:
								case StylePropertyId.BorderTopRightRadius:
								case StylePropertyId.Opacity:
									goto IL_025B;
								default:
									goto IL_02A5;
								}
								break;
							}
							break;
						}
					}
					continue;
					IL_025B:
					result.SetValue(endValue.id, Lerp.Interpolate(startValue.number, endValue.number, ratio));
					continue;
					IL_0280:
					result.SetValue(endValue.id, Lerp.Interpolate(startValue.color, endValue.color, ratio));
					continue;
					IL_02A5:
					throw new ArgumentException("Style Value can't be animated");
				}
			}
			return result;
		}
	}
}
