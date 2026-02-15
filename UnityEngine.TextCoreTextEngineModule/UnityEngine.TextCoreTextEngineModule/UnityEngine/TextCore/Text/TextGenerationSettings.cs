using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000043 RID: 67
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal class TextGenerationSettings : IEquatable<TextGenerationSettings>
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00020243 File Offset: 0x0001E443
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0002024B File Offset: 0x0001E44B
		public RenderedText renderedText
		{
			get
			{
				return this.m_RenderedText;
			}
			set
			{
				this.m_RenderedText = value;
				this.m_CachedRenderedText = null;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0002025C File Offset: 0x0001E45C
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0002028A File Offset: 0x0001E48A
		public string text
		{
			get
			{
				string text;
				if ((text = this.m_CachedRenderedText) == null)
				{
					text = (this.m_CachedRenderedText = this.renderedText.CreateString());
				}
				return text;
			}
			set
			{
				this.renderedText = new RenderedText(value);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00020390 File Offset: 0x0001E590
		public bool Equals(TextGenerationSettings other)
		{
			bool flag = other == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == other;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool pixelPerPointsEqual = true;
					flag2 = this.m_RenderedText.Equals(other.m_RenderedText) && this.screenRect.Equals(other.screenRect) && this.margins.Equals(other.margins) && pixelPerPointsEqual && object.Equals(this.fontAsset, other.fontAsset) && object.Equals(this.material, other.material) && object.Equals(this.spriteAsset, other.spriteAsset) && object.Equals(this.styleSheet, other.styleSheet) && this.fontStyle == other.fontStyle && object.Equals(this.textSettings, other.textSettings) && this.textAlignment == other.textAlignment && this.overflowMode == other.overflowMode && this.wordWrappingRatio.Equals(other.wordWrappingRatio) && this.color.Equals(other.color) && object.Equals(this.fontColorGradient, other.fontColorGradient) && object.Equals(this.fontColorGradientPreset, other.fontColorGradientPreset) && this.tintSprites == other.tintSprites && this.overrideRichTextColors == other.overrideRichTextColors && this.shouldConvertToLinearSpace == other.shouldConvertToLinearSpace && this.fontSize.Equals(other.fontSize) && this.autoSize == other.autoSize && this.fontSizeMin.Equals(other.fontSizeMin) && this.fontSizeMax.Equals(other.fontSizeMax) && object.Equals(this.fontFeatures, other.fontFeatures) && this.emojiFallbackSupport == other.emojiFallbackSupport && this.richText == other.richText && this.isRightToLeft == other.isRightToLeft && this.extraPadding == other.extraPadding && this.parseControlCharacters == other.parseControlCharacters && this.isOrthographic == other.isOrthographic && this.isPlaceholder == other.isPlaceholder && this.tagNoParsing == other.tagNoParsing && this.characterSpacing.Equals(other.characterSpacing) && this.wordSpacing.Equals(other.wordSpacing) && this.lineSpacing.Equals(other.lineSpacing) && this.paragraphSpacing.Equals(other.paragraphSpacing) && this.lineSpacingMax.Equals(other.lineSpacingMax) && this.textWrappingMode == other.textWrappingMode && this.maxVisibleCharacters == other.maxVisibleCharacters && this.maxVisibleWords == other.maxVisibleWords && this.maxVisibleLines == other.maxVisibleLines && this.firstVisibleCharacter == other.firstVisibleCharacter && this.useMaxVisibleDescender == other.useMaxVisibleDescender && this.fontWeight == other.fontWeight && this.horizontalMapping == other.horizontalMapping && this.verticalMapping == other.verticalMapping && this.uvLineOffset.Equals(other.uvLineOffset) && this.geometrySortingOrder == other.geometrySortingOrder && this.inverseYAxis == other.inverseYAxis && this.charWidthMaxAdj.Equals(other.charWidthMaxAdj) && this.isIMGUI == other.isIMGUI;
				}
			}
			return flag2;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00020770 File Offset: 0x0001E970
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this == obj;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = obj.GetType() != base.GetType();
					flag2 = !flag4 && this.Equals((TextGenerationSettings)obj);
				}
			}
			return flag2;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000207C0 File Offset: 0x0001E9C0
		public override int GetHashCode()
		{
			HashCode hashCode = default(HashCode);
			hashCode.Add<RenderedText>(this.m_RenderedText);
			hashCode.Add<Rect>(this.screenRect);
			hashCode.Add<Vector4>(this.margins);
			hashCode.Add<FontAsset>(this.fontAsset);
			hashCode.Add<Material>(this.material);
			hashCode.Add<SpriteAsset>(this.spriteAsset);
			hashCode.Add<TextStyleSheet>(this.styleSheet);
			hashCode.Add<int>((int)this.fontStyle);
			hashCode.Add<TextSettings>(this.textSettings);
			hashCode.Add<int>((int)this.textAlignment);
			hashCode.Add<int>((int)this.overflowMode);
			hashCode.Add<float>(this.wordWrappingRatio);
			hashCode.Add<Color>(this.color);
			hashCode.Add<TextColorGradient>(this.fontColorGradient);
			hashCode.Add<TextColorGradient>(this.fontColorGradientPreset);
			hashCode.Add<bool>(this.tintSprites);
			hashCode.Add<bool>(this.overrideRichTextColors);
			hashCode.Add<bool>(this.shouldConvertToLinearSpace);
			hashCode.Add<float>(this.fontSize);
			hashCode.Add<bool>(this.autoSize);
			hashCode.Add<float>(this.fontSizeMin);
			hashCode.Add<float>(this.fontSizeMax);
			hashCode.Add<List<OTL_FeatureTag>>(this.fontFeatures);
			hashCode.Add<bool>(this.emojiFallbackSupport);
			hashCode.Add<bool>(this.richText);
			hashCode.Add<bool>(this.isRightToLeft);
			hashCode.Add<float>(this.extraPadding);
			hashCode.Add<bool>(this.parseControlCharacters);
			hashCode.Add<bool>(this.isOrthographic);
			hashCode.Add<bool>(this.isPlaceholder);
			hashCode.Add<bool>(this.tagNoParsing);
			hashCode.Add<float>(this.characterSpacing);
			hashCode.Add<float>(this.wordSpacing);
			hashCode.Add<float>(this.lineSpacing);
			hashCode.Add<float>(this.paragraphSpacing);
			hashCode.Add<float>(this.lineSpacingMax);
			hashCode.Add<int>((int)this.textWrappingMode);
			hashCode.Add<int>(this.maxVisibleCharacters);
			hashCode.Add<int>(this.maxVisibleWords);
			hashCode.Add<int>(this.maxVisibleLines);
			hashCode.Add<int>(this.firstVisibleCharacter);
			hashCode.Add<bool>(this.useMaxVisibleDescender);
			hashCode.Add<int>((int)this.fontWeight);
			hashCode.Add<int>((int)this.horizontalMapping);
			hashCode.Add<int>((int)this.verticalMapping);
			hashCode.Add<float>(this.uvLineOffset);
			hashCode.Add<int>((int)this.geometrySortingOrder);
			hashCode.Add<bool>(this.inverseYAxis);
			hashCode.Add<float>(this.charWidthMaxAdj);
			hashCode.Add<bool>(this.isIMGUI);
			return hashCode.ToHashCode();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00020AA0 File Offset: 0x0001ECA0
		public static bool operator !=(TextGenerationSettings left, TextGenerationSettings right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00020ABC File Offset: 0x0001ECBC
		public override string ToString()
		{
			return string.Format("{0}: {1}\n {2}: {3}\n {4}: {5}\n {6}: {7}\n {8}: {9}\n {10}: {11}\n {12}: {13}\n {14}: {15}\n {16}: {17}\n {18}: {19}\n {20}: {21}\n {22}: {23}\n {24}: {25}\n {26}: {27}\n {28}: {29}\n {30}: {31}\n {32}: {33}\n {34}: {35}\n {36}: {37}\n {38}: {39}\n {40}: {41}\n {42}: {43}\n {44}: {45}\n {46}: {47}\n {48}: {49}\n {50}: {51}\n {52}: {53}\n {54}: {55}\n {56}: {57}\n {58}: {59}\n {60}: {61}\n {62}: {63}\n {64}: {65}\n {66}: {67}\n {68}: {69}\n {70}: {71}\n {72}: {73}\n {74}: {75}\n {76}: {77}\n {78}: {79}\n {80}: {81}\n {82}: {83}\n {84}: {85}\n {86}: {87}\n {88}: {89}\n {90}: {91}\n {92}: {93}\n {94}: {95}\n {96}: {97}\n {98}: {99}\n {100}: {101}", new object[]
			{
				"text", this.text, "screenRect", this.screenRect, "margins", this.margins, "pixelsPerPoint", this.pixelsPerPoint, "fontAsset", this.fontAsset,
				"material", this.material, "spriteAsset", this.spriteAsset, "styleSheet", this.styleSheet, "fontStyle", this.fontStyle, "textSettings", this.textSettings,
				"textAlignment", this.textAlignment, "overflowMode", this.overflowMode, "textWrappingMode", this.textWrappingMode, "wordWrappingRatio", this.wordWrappingRatio, "color", this.color,
				"fontColorGradient", this.fontColorGradient, "fontColorGradientPreset", this.fontColorGradientPreset, "tintSprites", this.tintSprites, "overrideRichTextColors", this.overrideRichTextColors, "shouldConvertToLinearSpace", this.shouldConvertToLinearSpace,
				"fontSize", this.fontSize, "autoSize", this.autoSize, "fontSizeMin", this.fontSizeMin, "fontSizeMax", this.fontSizeMax, "richText", this.richText,
				"isRightToLeft", this.isRightToLeft, "extraPadding", this.extraPadding, "parseControlCharacters", this.parseControlCharacters, "isOrthographic", this.isOrthographic, "tagNoParsing", this.tagNoParsing,
				"characterSpacing", this.characterSpacing, "wordSpacing", this.wordSpacing, "lineSpacing", this.lineSpacing, "paragraphSpacing", this.paragraphSpacing, "lineSpacingMax", this.lineSpacingMax,
				"textWrappingMode", this.textWrappingMode, "maxVisibleCharacters", this.maxVisibleCharacters, "maxVisibleWords", this.maxVisibleWords, "maxVisibleLines", this.maxVisibleLines, "firstVisibleCharacter", this.firstVisibleCharacter,
				"useMaxVisibleDescender", this.useMaxVisibleDescender, "fontWeight", this.fontWeight, "pageToDisplay", this.pageToDisplay, "horizontalMapping", this.horizontalMapping, "verticalMapping", this.verticalMapping,
				"uvLineOffset", this.uvLineOffset, "geometrySortingOrder", this.geometrySortingOrder, "inverseYAxis", this.inverseYAxis, "charWidthMaxAdj", this.charWidthMaxAdj, "inputSource", this.inputSource,
				"isPlaceholder", this.isPlaceholder
			});
		}

		// Token: 0x0400026C RID: 620
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal static Func<bool> IsEditorTextRenderingModeBitmap;

		// Token: 0x0400026D RID: 621
		private RenderedText m_RenderedText;

		// Token: 0x0400026E RID: 622
		private string m_CachedRenderedText;

		// Token: 0x0400026F RID: 623
		public Rect screenRect;

		// Token: 0x04000270 RID: 624
		public Vector4 margins;

		// Token: 0x04000271 RID: 625
		public float pixelsPerPoint = 1f;

		// Token: 0x04000272 RID: 626
		public bool isEditorRenderingModeBitmap = false;

		// Token: 0x04000273 RID: 627
		public FontAsset fontAsset;

		// Token: 0x04000274 RID: 628
		public Material material;

		// Token: 0x04000275 RID: 629
		public SpriteAsset spriteAsset;

		// Token: 0x04000276 RID: 630
		public TextStyleSheet styleSheet;

		// Token: 0x04000277 RID: 631
		public FontStyles fontStyle = FontStyles.Normal;

		// Token: 0x04000278 RID: 632
		public TextSettings textSettings;

		// Token: 0x04000279 RID: 633
		public TextAlignment textAlignment = TextAlignment.TopLeft;

		// Token: 0x0400027A RID: 634
		public TextOverflowMode overflowMode = TextOverflowMode.Overflow;

		// Token: 0x0400027B RID: 635
		public float wordWrappingRatio;

		// Token: 0x0400027C RID: 636
		public Color color = Color.white;

		// Token: 0x0400027D RID: 637
		public TextColorGradient fontColorGradient;

		// Token: 0x0400027E RID: 638
		public TextColorGradient fontColorGradientPreset;

		// Token: 0x0400027F RID: 639
		public bool tintSprites;

		// Token: 0x04000280 RID: 640
		public bool overrideRichTextColors;

		// Token: 0x04000281 RID: 641
		public bool shouldConvertToLinearSpace = true;

		// Token: 0x04000282 RID: 642
		public float fontSize = 18f;

		// Token: 0x04000283 RID: 643
		public bool autoSize;

		// Token: 0x04000284 RID: 644
		public float fontSizeMin;

		// Token: 0x04000285 RID: 645
		public float fontSizeMax;

		// Token: 0x04000286 RID: 646
		public List<OTL_FeatureTag> fontFeatures = new List<OTL_FeatureTag>();

		// Token: 0x04000287 RID: 647
		public bool emojiFallbackSupport = true;

		// Token: 0x04000288 RID: 648
		public bool richText;

		// Token: 0x04000289 RID: 649
		public bool isRightToLeft;

		// Token: 0x0400028A RID: 650
		public float extraPadding = 6f;

		// Token: 0x0400028B RID: 651
		public bool parseControlCharacters = true;

		// Token: 0x0400028C RID: 652
		public bool isOrthographic = true;

		// Token: 0x0400028D RID: 653
		public bool isPlaceholder = false;

		// Token: 0x0400028E RID: 654
		public bool tagNoParsing = false;

		// Token: 0x0400028F RID: 655
		public float characterSpacing;

		// Token: 0x04000290 RID: 656
		public float wordSpacing;

		// Token: 0x04000291 RID: 657
		public float lineSpacing;

		// Token: 0x04000292 RID: 658
		public float paragraphSpacing;

		// Token: 0x04000293 RID: 659
		public float lineSpacingMax;

		// Token: 0x04000294 RID: 660
		public TextWrappingMode textWrappingMode = TextWrappingMode.Normal;

		// Token: 0x04000295 RID: 661
		public int maxVisibleCharacters = 99999;

		// Token: 0x04000296 RID: 662
		public int maxVisibleWords = 99999;

		// Token: 0x04000297 RID: 663
		public int maxVisibleLines = 99999;

		// Token: 0x04000298 RID: 664
		public int firstVisibleCharacter = 0;

		// Token: 0x04000299 RID: 665
		public bool useMaxVisibleDescender;

		// Token: 0x0400029A RID: 666
		public TextFontWeight fontWeight = TextFontWeight.Regular;

		// Token: 0x0400029B RID: 667
		public int pageToDisplay = 1;

		// Token: 0x0400029C RID: 668
		public TextureMapping horizontalMapping = TextureMapping.Character;

		// Token: 0x0400029D RID: 669
		public TextureMapping verticalMapping = TextureMapping.Character;

		// Token: 0x0400029E RID: 670
		public float uvLineOffset;

		// Token: 0x0400029F RID: 671
		public VertexSortingOrder geometrySortingOrder = VertexSortingOrder.Normal;

		// Token: 0x040002A0 RID: 672
		public bool inverseYAxis;

		// Token: 0x040002A1 RID: 673
		public bool isIMGUI;

		// Token: 0x040002A2 RID: 674
		public float charWidthMaxAdj;

		// Token: 0x040002A3 RID: 675
		internal TextInputSource inputSource = TextInputSource.TextString;
	}
}
