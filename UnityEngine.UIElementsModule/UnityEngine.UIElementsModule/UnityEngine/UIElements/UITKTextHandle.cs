using System;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.TextCore;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000445 RID: 1093
	internal class UITKTextHandle : TextHandle
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x00072DD0 File Offset: 0x00070FD0
		private List<ValueTuple<int, RichTextTagParser.TagType, string>> Links
		{
			get
			{
				List<ValueTuple<int, RichTextTagParser.TagType, string>> list;
				if ((list = this.m_Links) == null)
				{
					list = (this.m_Links = new List<ValueTuple<int, RichTextTagParser.TagType, string>>());
				}
				return list;
			}
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00072DF8 File Offset: 0x00070FF8
		public void ComputeNativeTextSize(in RenderedText textToMeasure, float width, float height)
		{
			bool flag = !this.ConvertUssToNativeTextGenerationSettings();
			if (!flag)
			{
				this.nativeSettings.text = textToMeasure.CreateString();
				this.nativeSettings.screenWidth = (float.IsNaN(width) ? int.MaxValue : ((int)(width * 64f)));
				this.nativeSettings.screenHeight = (float.IsNaN(height) ? int.MaxValue : ((int)(height * 64f)));
				bool flag2 = this.m_TextElement.enableRichText && !string.IsNullOrEmpty(this.nativeSettings.text);
				if (flag2)
				{
					Panel panel = this.m_TextElement.panel as Panel;
					Color hyperlinkColor = ((panel != null) ? panel.HyperlinkColor : Color.blue);
					RichTextTagParser.CreateTextGenerationSettingsArray(ref this.nativeSettings, this.Links, hyperlinkColor);
				}
				else
				{
					this.nativeSettings.textSpans = null;
				}
				base.preferredSize = TextHandle.TextLib.MeasureText(this.nativeSettings, IntPtr.Zero);
			}
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00072EF4 File Offset: 0x000710F4
		public NativeTextInfo UpdateNative(ref bool success)
		{
			bool flag = !this.ConvertUssToNativeTextGenerationSettings();
			NativeTextInfo nativeTextInfo;
			if (flag)
			{
				success = false;
				nativeTextInfo = default(NativeTextInfo);
			}
			else
			{
				success = true;
				bool flag2 = this.m_TextElement.enableRichText && !string.IsNullOrEmpty(this.nativeSettings.text);
				if (flag2)
				{
					Panel panel = this.m_TextElement.panel as Panel;
					Color hyperlinkColor = ((panel != null) ? panel.HyperlinkColor : Color.blue);
					RichTextTagParser.CreateTextGenerationSettingsArray(ref this.nativeSettings, this.Links, hyperlinkColor);
				}
				else
				{
					this.nativeSettings.textSpans = null;
				}
				bool flag3 = this.nativeSettings.hasLink && this.textGenerationInfo == IntPtr.Zero;
				if (flag3)
				{
					this.textGenerationInfo = TextGenerationInfo.Create();
					if (this.m_ATGTextEventHandler == null)
					{
						this.m_ATGTextEventHandler = new ATGTextEventHandler(this.m_TextElement);
					}
				}
				NativeTextInfo textInfo = TextHandle.TextLib.GenerateText(this.nativeSettings, this.textGenerationInfo);
				this.UpdateATGTextEventHandler(this.nativeSettings);
				nativeTextInfo = textInfo;
			}
			return nativeTextInfo;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0007300C File Offset: 0x0007120C
		private ValueTuple<bool, bool> hasLinkAndHyperlink()
		{
			bool hasLink = false;
			bool hasHyperlink = false;
			bool flag = this.m_Links != null;
			if (flag)
			{
				foreach (ValueTuple<int, RichTextTagParser.TagType, string> valueTuple in this.Links)
				{
					RichTextTagParser.TagType type = valueTuple.Item2;
					hasLink = hasLink || type == RichTextTagParser.TagType.Link;
					hasHyperlink = hasHyperlink || type == RichTextTagParser.TagType.Hyperlink;
					bool flag2 = hasLink && hasHyperlink;
					if (flag2)
					{
						break;
					}
				}
			}
			return new ValueTuple<bool, bool>(hasLink, hasHyperlink);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000730A8 File Offset: 0x000712A8
		internal ValueTuple<RichTextTagParser.TagType, string> ATGFindIntersectingLink(Vector2 point)
		{
			Debug.Assert(base.useAdvancedText);
			bool flag = this.textGenerationInfo == IntPtr.Zero;
			ValueTuple<RichTextTagParser.TagType, string> valueTuple;
			if (flag)
			{
				Debug.LogError("TextGenerationInfo pointer is null.");
				valueTuple = new ValueTuple<RichTextTagParser.TagType, string>(RichTextTagParser.TagType.Unknown, null);
			}
			else
			{
				int id = TextLib.FindIntersectingLink(point, this.textGenerationInfo);
				bool flag2 = id == -1;
				if (flag2)
				{
					valueTuple = new ValueTuple<RichTextTagParser.TagType, string>(RichTextTagParser.TagType.Unknown, null);
				}
				else
				{
					valueTuple = new ValueTuple<RichTextTagParser.TagType, string>(this.m_Links[id].Item2, this.m_Links[id].Item3);
				}
			}
			return valueTuple;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00073138 File Offset: 0x00071338
		private void UpdateATGTextEventHandler(NativeTextGenerationSettings setting)
		{
			bool flag = this.m_ATGTextEventHandler == null;
			if (!flag)
			{
				ValueTuple<bool, bool> valueTuple = this.hasLinkAndHyperlink();
				bool hasLink = valueTuple.Item1;
				bool hasHyperlink = valueTuple.Item2;
				bool flag2 = hasLink;
				if (flag2)
				{
					this.m_ATGTextEventHandler.RegisterLinkTagCallbacks();
				}
				else
				{
					this.m_ATGTextEventHandler.UnRegisterLinkTagCallbacks();
				}
				bool flag3 = hasHyperlink;
				if (flag3)
				{
					this.m_ATGTextEventHandler.RegisterHyperlinkCallbacks();
				}
				else
				{
					this.m_ATGTextEventHandler.UnRegisterHyperlinkCallbacks();
				}
			}
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000731B0 File Offset: 0x000713B0
		internal unsafe bool ConvertUssToNativeTextGenerationSettings()
		{
			FontAsset fa = TextUtilities.GetFontAsset(this.m_TextElement);
			bool flag = fa.atlasPopulationMode == AtlasPopulationMode.Static;
			bool flag2;
			if (flag)
			{
				Debug.LogError("Advanced text system cannot render using static font asset " + fa.name);
				flag2 = false;
			}
			else
			{
				ComputedStyle style = *this.m_TextElement.computedStyle;
				this.nativeSettings.text = ((this.m_TextElement.isElided && !this.TextLibraryCanElide()) ? new RenderedText(this.m_TextElement.elidedText) : this.m_TextElement.renderedText).CreateString();
				this.nativeSettings.fontSize = (int)((style.fontSize.value > 0f) ? (style.fontSize.value * 64f) : (fa.faceInfo.pointSize * 64f));
				this.nativeSettings.wordWrap = style.whiteSpace.toTextCore();
				this.nativeSettings.overflow = style.textOverflow.toTextCore(style.overflow);
				this.nativeSettings.horizontalAlignment = TextGeneratorUtilities.GetHorizontalAlignment(style.unityTextAlign);
				this.nativeSettings.verticalAlignment = TextGeneratorUtilities.GetVerticalAlignment(style.unityTextAlign);
				this.nativeSettings.color = style.color;
				this.nativeSettings.fontAsset = fa.nativeFontAsset;
				this.nativeSettings.languageDirection = this.m_TextElement.localLanguageDirection.toTextCore();
				this.nativeSettings.vertexPadding = (int)(this.GetVertexPadding(fa) * 64f);
				TextSettings textSettings = TextUtilities.GetTextSettingsFrom(this.m_TextElement);
				List<IntPtr> globalFontAssetFallbacks = new List<IntPtr>();
				bool flag3 = textSettings != null && textSettings.fallbackFontAssets != null;
				if (flag3)
				{
					foreach (FontAsset fallback in textSettings.fallbackFontAssets)
					{
						bool flag4 = fallback == null;
						if (!flag4)
						{
							bool flag5 = fallback.atlasPopulationMode == AtlasPopulationMode.Static && fallback.characterTable.Count > 0;
							if (flag5)
							{
								Debug.LogWarning("Advanced text system cannot use static font asset " + fallback.name + " as fallback.");
							}
							else
							{
								globalFontAssetFallbacks.Add(fallback.nativeFontAsset);
							}
						}
					}
				}
				bool flag6 = textSettings != null && textSettings.emojiFallbackTextAssets != null;
				if (flag6)
				{
					foreach (TextAsset textAsset in textSettings.emojiFallbackTextAssets)
					{
						FontAsset fallback2 = (FontAsset)textAsset;
						bool flag7 = fallback2 == null;
						if (!flag7)
						{
							bool flag8 = fallback2.atlasPopulationMode == AtlasPopulationMode.Static && fallback2.characterTable.Count > 0;
							if (flag8)
							{
								Debug.LogWarning("Advanced text system cannot use static font asset " + fallback2.name + " as fallback.");
							}
							else
							{
								globalFontAssetFallbacks.Add(fallback2.nativeFontAsset);
							}
						}
					}
				}
				this.nativeSettings.globalFontAssetFallbacks = globalFontAssetFallbacks.ToArray();
				FontStyles sourcefontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(style.unityFontStyleAndWeight);
				this.nativeSettings.fontStyle = sourcefontStyle & ~FontStyles.Bold;
				this.nativeSettings.fontWeight = (((sourcefontStyle & FontStyles.Bold) == FontStyles.Bold) ? TextFontWeight.Bold : TextFontWeight.Regular);
				Vector2 size = this.m_TextElement.contentRect.size;
				bool flag9 = Mathf.Abs(size.x - this.ATGRoundedSizes.x) < 0.01f && Mathf.Abs(size.y - this.ATGRoundedSizes.y) < 0.01f;
				if (flag9)
				{
					size = this.ATGMeasuredSizes;
				}
				else
				{
					this.ATGRoundedSizes = size;
					this.ATGMeasuredSizes = size;
				}
				this.nativeSettings.screenWidth = (int)(size.x * 64f);
				this.nativeSettings.screenHeight = (int)(size.y * 64f);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00073600 File Offset: 0x00071800
		public UITKTextHandle(TextElement te)
		{
			this.m_TextElement = te;
			this.m_TextEventHandler = new TextEventHandler(te);
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0007361D File Offset: 0x0007181D
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x00073625 File Offset: 0x00071825
		public Vector2 MeasuredSizes { get; set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0007362E File Offset: 0x0007182E
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x00073636 File Offset: 0x00071836
		public Vector2 RoundedSizes { get; set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0007363F File Offset: 0x0007183F
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x00073647 File Offset: 0x00071847
		public Vector2 ATGMeasuredSizes { get; set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x00073650 File Offset: 0x00071850
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x00073658 File Offset: 0x00071858
		public Vector2 ATGRoundedSizes { get; set; }

		// Token: 0x06001F8A RID: 8074 RVA: 0x00073664 File Offset: 0x00071864
		public Vector2 ComputeTextSize(in RenderedText textToMeasure, float width, float height)
		{
			bool flag = TextUtilities.IsAdvancedTextEnabledForElement(this.m_TextElement);
			if (flag)
			{
				this.ComputeNativeTextSize(in textToMeasure, width, height);
			}
			else
			{
				this.ConvertUssToTextGenerationSettings();
				TextHandle.settings.renderedText = textToMeasure;
				TextHandle.settings.screenRect = new Rect(0f, 0f, width, height);
				base.UpdatePreferredValues(TextHandle.settings);
			}
			return base.preferredSize;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000736DA File Offset: 0x000718DA
		public void ComputeSettingsAndUpdate()
		{
			this.UpdateMesh();
			this.HandleATag();
			this.HandleLinkTag();
			this.HandleLinkAndATagCallbacks();
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000736F9 File Offset: 0x000718F9
		public void HandleATag()
		{
			this.m_TextEventHandler.HandleATag();
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00073708 File Offset: 0x00071908
		public void HandleLinkTag()
		{
			this.m_TextEventHandler.HandleLinkTag();
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00073717 File Offset: 0x00071917
		public void HandleLinkAndATagCallbacks()
		{
			this.m_TextEventHandler.HandleLinkAndATagCallbacks();
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00073728 File Offset: 0x00071928
		public void UpdateMesh()
		{
			this.ConvertUssToTextGenerationSettings();
			int hashCode = TextHandle.settings.GetHashCode();
			bool flag = this.m_PreviousGenerationSettingsHash == hashCode;
			if (flag)
			{
				base.AddTextInfoToTemporaryCache(hashCode);
			}
			else
			{
				base.RemoveTextInfoFromTemporaryCache();
				base.UpdateWithHash(hashCode);
			}
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00073770 File Offset: 0x00071970
		public override void AddTextInfoToPermanentCache()
		{
			bool useAdvancedText = base.useAdvancedText;
			if (useAdvancedText)
			{
				bool flag = this.textGenerationInfo == IntPtr.Zero;
				if (flag)
				{
					this.textGenerationInfo = TextGenerationInfo.Create();
				}
				bool success = false;
				this.UpdateNative(ref success);
			}
			else
			{
				this.ConvertUssToTextGenerationSettings();
				base.AddTextInfoToPermanentCache();
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000737C4 File Offset: 0x000719C4
		private unsafe TextOverflowMode GetTextOverflowMode()
		{
			ComputedStyle style = *this.m_TextElement.computedStyle;
			bool flag = style.textOverflow == TextOverflow.Clip;
			TextOverflowMode textOverflowMode;
			if (flag)
			{
				textOverflowMode = TextOverflowMode.Masking;
			}
			else
			{
				bool flag2 = style.textOverflow != TextOverflow.Ellipsis;
				if (flag2)
				{
					textOverflowMode = TextOverflowMode.Overflow;
				}
				else
				{
					bool flag3 = !this.TextLibraryCanElide();
					if (flag3)
					{
						textOverflowMode = TextOverflowMode.Masking;
					}
					else
					{
						bool flag4 = style.overflow == OverflowInternal.Hidden;
						if (flag4)
						{
							textOverflowMode = TextOverflowMode.Ellipsis;
						}
						else
						{
							textOverflowMode = TextOverflowMode.Overflow;
						}
					}
				}
			}
			return textOverflowMode;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00073838 File Offset: 0x00071A38
		internal unsafe virtual bool ConvertUssToTextGenerationSettings()
		{
			ComputedStyle style = *this.m_TextElement.computedStyle;
			TextGenerationSettings tgs = TextHandle.settings;
			tgs.text = string.Empty;
			tgs.isIMGUI = false;
			tgs.textSettings = TextUtilities.GetTextSettingsFrom(this.m_TextElement);
			bool flag = tgs.textSettings == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				tgs.fontAsset = TextUtilities.GetFontAsset(this.m_TextElement);
				bool flag3 = tgs.fontAsset == null;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					tgs.screenRect = new Rect(0f, 0f, this.m_TextElement.contentRect.width, this.m_TextElement.contentRect.height);
					tgs.extraPadding = this.GetVertexPadding(tgs.fontAsset);
					tgs.renderedText = ((this.m_TextElement.isElided && !this.TextLibraryCanElide()) ? new RenderedText(this.m_TextElement.elidedText) : this.m_TextElement.renderedText);
					tgs.isPlaceholder = this.m_TextElement.showPlaceholderText;
					tgs.fontSize = ((style.fontSize.value > 0f) ? style.fontSize.value : tgs.fontAsset.faceInfo.pointSize);
					tgs.fontStyle = TextGeneratorUtilities.LegacyStyleToNewStyle(style.unityFontStyleAndWeight);
					tgs.material = tgs.fontAsset.material;
					tgs.textAlignment = TextGeneratorUtilities.LegacyAlignmentToNewAlignment(style.unityTextAlign);
					tgs.textWrappingMode = style.whiteSpace.toTextWrappingMode();
					tgs.wordWrappingRatio = 0.4f;
					tgs.richText = this.m_TextElement.enableRichText;
					tgs.overflowMode = this.GetTextOverflowMode();
					tgs.characterSpacing = style.letterSpacing.value;
					tgs.wordSpacing = style.wordSpacing.value;
					tgs.paragraphSpacing = style.unityParagraphSpacing.value;
					tgs.color = style.color;
					tgs.color *= this.m_TextElement.playModeTintColor;
					tgs.shouldConvertToLinearSpace = false;
					tgs.parseControlCharacters = this.m_TextElement.parseEscapeSequences;
					tgs.isRightToLeft = this.m_TextElement.localLanguageDirection == LanguageDirection.RTL;
					tgs.inverseYAxis = true;
					tgs.fontFeatures = TextHandle.m_ActiveFontFeatures;
					tgs.emojiFallbackSupport = this.m_TextElement.emojiFallbackSupport;
					Vector2 size = this.m_TextElement.contentRect.size;
					bool flag4 = Mathf.Abs(size.x - this.RoundedSizes.x) < 0.01f && Mathf.Abs(size.y - this.RoundedSizes.y) < 0.01f;
					if (flag4)
					{
						size = this.MeasuredSizes;
					}
					else
					{
						this.RoundedSizes = size;
						this.MeasuredSizes = size;
					}
					tgs.screenRect = new Rect(Vector2.zero, size);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00073B58 File Offset: 0x00071D58
		internal bool TextLibraryCanElide()
		{
			return this.m_TextElement.computedStyle.unityTextOverflowPosition == TextOverflowPosition.End;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00073B80 File Offset: 0x00071D80
		internal unsafe float GetVertexPadding(FontAsset fontAsset)
		{
			ComputedStyle style = *this.m_TextElement.computedStyle;
			float outlineThickness = style.unityTextOutlineWidth / 2f;
			float offsetX = Mathf.Abs(style.textShadow.offset.x);
			float offsetY = Mathf.Abs(style.textShadow.offset.y);
			float blurRadius = Mathf.Abs(style.textShadow.blurRadius);
			bool flag = outlineThickness <= 0f && offsetX <= 0f && offsetY <= 0f && blurRadius <= 0f;
			float num;
			if (flag)
			{
				num = UITKTextHandle.k_MinPadding;
			}
			else
			{
				float horizontalPadding = Mathf.Max(offsetX + blurRadius, outlineThickness);
				float verticalPadding = Mathf.Max(offsetY + blurRadius, outlineThickness);
				float padding = Mathf.Max(horizontalPadding, verticalPadding) + UITKTextHandle.k_MinPadding;
				float factor = TextHandle.ConvertPixelUnitsToTextCoreRelativeUnits(style.fontSize.value, fontAsset);
				int gradientScale = fontAsset.atlasPadding + 1;
				num = Mathf.Min(padding * factor * (float)gradientScale, (float)gradientScale);
			}
			return num;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00073C88 File Offset: 0x00071E88
		internal override bool IsAdvancedTextEnabledForElement()
		{
			bool isExecutingJob = JobsUtility.IsExecutingJob;
			bool flag;
			if (isExecutingJob)
			{
				flag = false;
			}
			else
			{
				bool isEnabled = TextUtilities.IsAdvancedTextEnabledForElement(this.m_TextElement);
				bool flag2 = this.wasAdvancedTextEnabledForElement && !isEnabled && this.textGenerationInfo != IntPtr.Zero;
				if (flag2)
				{
					TextGenerationInfo.Destroy(this.textGenerationInfo);
					this.textGenerationInfo = IntPtr.Zero;
				}
				else
				{
					bool flag3 = !this.wasAdvancedTextEnabledForElement && isEnabled;
					if (flag3)
					{
						TextHandle.s_PermanentCache.RemoveTextInfoFromCache(this);
						TextHandle.s_TemporaryCache.RemoveTextInfoFromCache(this);
					}
				}
				this.wasAdvancedTextEnabledForElement = isEnabled;
				flag = isEnabled;
			}
			return flag;
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00073D23 File Offset: 0x00071F23
		public override bool IsPlaceholder
		{
			get
			{
				return base.useAdvancedText ? this.m_TextElement.showPlaceholderText : base.IsPlaceholder;
			}
		}

		// Token: 0x04000DFA RID: 3578
		private ATGTextEventHandler m_ATGTextEventHandler;

		// Token: 0x04000DFB RID: 3579
		private List<ValueTuple<int, RichTextTagParser.TagType, string>> m_Links;

		// Token: 0x04000E00 RID: 3584
		internal TextEventHandler m_TextEventHandler;

		// Token: 0x04000E01 RID: 3585
		protected TextElement m_TextElement;

		// Token: 0x04000E02 RID: 3586
		internal static readonly float k_MinPadding = 6f;

		// Token: 0x04000E03 RID: 3587
		private bool wasAdvancedTextEnabledForElement;
	}
}
