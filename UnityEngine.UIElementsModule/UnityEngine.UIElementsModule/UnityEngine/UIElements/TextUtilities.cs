using System;
using UnityEngine.TextCore;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200044A RID: 1098
	internal static class TextUtilities
	{
		// Token: 0x06001FCD RID: 8141 RVA: 0x0007545C File Offset: 0x0007365C
		internal static Vector2 MeasureVisualElementTextSize(TextElement te, in RenderedText textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			float measuredWidth = float.NaN;
			float measuredHeight = float.NaN;
			bool flag = !TextUtilities.IsFontAssigned(te);
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(measuredWidth, measuredHeight);
			}
			else
			{
				float pixelsPerPoint = 1f;
				bool flag2 = te.panel != null;
				if (flag2)
				{
					pixelsPerPoint = te.scaledPixelsPerPoint;
				}
				bool flag3 = pixelsPerPoint <= 0f;
				if (flag3)
				{
					vector = Vector2.zero;
				}
				else
				{
					bool flag4 = widthMode != VisualElement.MeasureMode.Exactly || heightMode != VisualElement.MeasureMode.Exactly;
					if (flag4)
					{
						Vector2 size = te.uitkTextHandle.ComputeTextSize(in textToMeasure, width, height);
						measuredWidth = size.x;
						measuredHeight = size.y;
					}
					bool flag5 = widthMode == VisualElement.MeasureMode.Exactly;
					if (flag5)
					{
						measuredWidth = width;
					}
					else
					{
						bool flag6 = widthMode == VisualElement.MeasureMode.AtMost;
						if (flag6)
						{
							measuredWidth = Mathf.Min(measuredWidth, width);
						}
					}
					bool flag7 = heightMode == VisualElement.MeasureMode.Exactly;
					if (flag7)
					{
						measuredHeight = height;
					}
					else
					{
						bool flag8 = heightMode == VisualElement.MeasureMode.AtMost;
						if (flag8)
						{
							measuredHeight = Mathf.Min(measuredHeight, height);
						}
					}
					float roundedWidth = AlignmentUtils.CeilToPixelGrid(measuredWidth, pixelsPerPoint, 0f);
					float roundedHeight = AlignmentUtils.CeilToPixelGrid(measuredHeight, pixelsPerPoint, 0f);
					Vector2 roundedValues = new Vector2(roundedWidth, roundedHeight);
					bool flag9 = TextUtilities.IsAdvancedTextEnabledForElement(te);
					if (flag9)
					{
						te.uitkTextHandle.ATGMeasuredSizes = new Vector2(measuredWidth, measuredHeight);
						te.uitkTextHandle.ATGRoundedSizes = roundedValues;
					}
					else
					{
						te.uitkTextHandle.MeasuredSizes = new Vector2(measuredWidth, measuredHeight);
						te.uitkTextHandle.RoundedSizes = roundedValues;
					}
					vector = roundedValues;
				}
			}
			return vector;
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000755D8 File Offset: 0x000737D8
		internal static FontAsset GetFontAsset(VisualElement ve)
		{
			bool flag = ve.computedStyle.unityFontDefinition.fontAsset != null;
			FontAsset fontAsset;
			if (flag)
			{
				fontAsset = ve.computedStyle.unityFontDefinition.fontAsset;
			}
			else
			{
				TextSettings textSettings = TextUtilities.GetTextSettingsFrom(ve);
				bool flag2 = ve.computedStyle.unityFontDefinition.font != null;
				if (flag2)
				{
					fontAsset = textSettings.GetCachedFontAsset(ve.computedStyle.unityFontDefinition.font, TextShaderUtilities.ShaderRef_MobileSDF);
				}
				else
				{
					bool flag3 = ve.computedStyle.unityFont != null;
					if (flag3)
					{
						fontAsset = textSettings.GetCachedFontAsset(ve.computedStyle.unityFont, TextShaderUtilities.ShaderRef_MobileSDF);
					}
					else
					{
						bool flag4 = textSettings != null;
						if (flag4)
						{
							fontAsset = textSettings.defaultFontAsset;
						}
						else
						{
							fontAsset = null;
						}
					}
				}
			}
			return fontAsset;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000756B0 File Offset: 0x000738B0
		internal static bool IsFontAssigned(VisualElement ve)
		{
			return ve.computedStyle.unityFont != null || !ve.computedStyle.unityFontDefinition.IsEmpty();
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000756F0 File Offset: 0x000738F0
		internal static TextSettings GetTextSettingsFrom(VisualElement ve)
		{
			RuntimePanel runtimePanel = ve.panel as RuntimePanel;
			bool flag = runtimePanel != null;
			TextSettings textSettings;
			if (flag)
			{
				textSettings = runtimePanel.panelSettings.textSettings ?? PanelTextSettings.defaultPanelTextSettings;
			}
			else
			{
				textSettings = PanelTextSettings.defaultPanelTextSettings;
			}
			return textSettings;
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x00075734 File Offset: 0x00073934
		internal static bool IsAdvancedTextEnabledForElement(TextElement te)
		{
			bool flag = te == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool isAdvancedTextGeneratorEnabledOnTextElement = te.computedStyle.unityTextGenerator == TextGeneratorType.Advanced;
				bool isAdvancedTextGeneratorEnabledOnProject = false;
				bool flag3 = te.panel == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					RuntimePanel runtimePanel = te.panel as RuntimePanel;
					bool flag4 = runtimePanel != null;
					if (flag4)
					{
						PanelSettings panelSettings = runtimePanel.panelSettings;
						isAdvancedTextGeneratorEnabledOnProject = ((panelSettings != null) ? panelSettings.m_ICUDataAsset : null) != null;
					}
					flag2 = isAdvancedTextGeneratorEnabledOnTextElement && isAdvancedTextGeneratorEnabledOnProject;
				}
			}
			return flag2;
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000757AC File Offset: 0x000739AC
		internal unsafe static TextCoreSettings GetTextCoreSettingsForElement(VisualElement ve, bool ignoreColors)
		{
			FontAsset fontAsset = TextUtilities.GetFontAsset(ve);
			bool flag = fontAsset == null;
			TextCoreSettings textCoreSettings;
			if (flag)
			{
				textCoreSettings = default(TextCoreSettings);
			}
			else
			{
				IResolvedStyle resolvedStyle = ve.resolvedStyle;
				ComputedStyle computedStyle = *ve.computedStyle;
				TextShadow textShadow = computedStyle.textShadow;
				float factor = TextHandle.ConvertPixelUnitsToTextCoreRelativeUnits(computedStyle.fontSize.value, fontAsset);
				float outlineWidth = Mathf.Clamp(resolvedStyle.unityTextOutlineWidth * factor, 0f, 1f);
				float underlaySoftness = Mathf.Clamp(textShadow.blurRadius * factor, 0f, 1f);
				float underlayOffsetX = ((textShadow.offset.x < 0f) ? Mathf.Max(textShadow.offset.x * factor, -1f) : Mathf.Min(textShadow.offset.x * factor, 1f));
				float underlayOffsetY = ((textShadow.offset.y < 0f) ? Mathf.Max(textShadow.offset.y * factor, -1f) : Mathf.Min(textShadow.offset.y * factor, 1f));
				Vector2 underlayOffset = new Vector2(underlayOffsetX, underlayOffsetY);
				Color faceColor;
				Color outlineColor;
				if (ignoreColors)
				{
					faceColor = Color.white;
					Color underlayColor = Color.white;
					outlineColor = Color.white;
				}
				else
				{
					bool isMultiChannel = ((Texture2D)fontAsset.material.mainTexture).format != TextureFormat.Alpha8;
					faceColor = resolvedStyle.color;
					outlineColor = resolvedStyle.unityTextOutlineColor;
					bool flag2 = outlineWidth < 1E-30f;
					if (flag2)
					{
						outlineColor.a = 0f;
					}
					Color underlayColor = textShadow.color;
					bool flag3 = isMultiChannel;
					if (flag3)
					{
						faceColor = new Color(1f, 1f, 1f, faceColor.a);
					}
					else
					{
						underlayColor.r *= faceColor.a;
						underlayColor.g *= faceColor.a;
						underlayColor.b *= faceColor.a;
						outlineColor.r *= outlineColor.a;
						outlineColor.g *= outlineColor.a;
						outlineColor.b *= outlineColor.a;
					}
				}
				textCoreSettings = new TextCoreSettings
				{
					faceColor = faceColor,
					outlineColor = outlineColor,
					outlineWidth = outlineWidth,
					underlayColor = textShadow.color,
					underlayOffset = underlayOffset,
					underlaySoftness = underlaySoftness
				};
			}
			return textCoreSettings;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x00075A34 File Offset: 0x00073C34
		public static TextWrappingMode toTextWrappingMode(this WhiteSpace whiteSpace)
		{
			if (!true)
			{
			}
			TextWrappingMode textWrappingMode;
			switch (whiteSpace)
			{
			case WhiteSpace.Normal:
				textWrappingMode = TextWrappingMode.Normal;
				break;
			case WhiteSpace.NoWrap:
				textWrappingMode = TextWrappingMode.NoWrap;
				break;
			case WhiteSpace.Pre:
				textWrappingMode = TextWrappingMode.PreserveWhitespaceNoWrap;
				break;
			case WhiteSpace.PreWrap:
				textWrappingMode = TextWrappingMode.PreserveWhitespace;
				break;
			default:
				textWrappingMode = TextWrappingMode.Normal;
				break;
			}
			if (!true)
			{
			}
			return textWrappingMode;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x00075A7C File Offset: 0x00073C7C
		public static WhiteSpace toTextCore(this WhiteSpace whiteSpace)
		{
			if (!true)
			{
			}
			WhiteSpace whiteSpace2;
			switch (whiteSpace)
			{
			case WhiteSpace.Normal:
				whiteSpace2 = WhiteSpace.Normal;
				break;
			case WhiteSpace.NoWrap:
				whiteSpace2 = WhiteSpace.NoWrap;
				break;
			case WhiteSpace.Pre:
				whiteSpace2 = WhiteSpace.Pre;
				break;
			case WhiteSpace.PreWrap:
				whiteSpace2 = WhiteSpace.PreWrap;
				break;
			default:
				whiteSpace2 = WhiteSpace.Normal;
				break;
			}
			if (!true)
			{
			}
			return whiteSpace2;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x00075AC4 File Offset: 0x00073CC4
		public static TextOverflow toTextCore(this TextOverflow textOverflow, OverflowInternal overflow)
		{
			if (!true)
			{
			}
			TextOverflow textOverflow2;
			if (textOverflow == TextOverflow.Ellipsis)
			{
				if (overflow == OverflowInternal.Hidden)
				{
					textOverflow2 = TextOverflow.Ellipsis;
					goto IL_001B;
				}
			}
			textOverflow2 = TextOverflow.Clip;
			IL_001B:
			if (!true)
			{
			}
			return textOverflow2;
		}
	}
}
