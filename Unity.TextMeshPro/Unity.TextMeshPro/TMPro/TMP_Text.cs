using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200008B RID: 139
	public abstract class TMP_Text : MaskableGraphic
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000159DD File Offset: 0x00013BDD
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x000159F4 File Offset: 0x00013BF4
		public virtual string text
		{
			get
			{
				if (this.m_IsTextBackingStringDirty)
				{
					return this.InternalTextBackingArrayToString();
				}
				return this.m_text;
			}
			set
			{
				if (!this.m_IsTextBackingStringDirty && this.m_text != null && value != null && this.m_text.Length == value.Length && this.m_text == value)
				{
					return;
				}
				this.m_IsTextBackingStringDirty = false;
				this.m_text = value;
				this.m_inputSource = TMP_Text.TextInputSources.TextString;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00015A5E File Offset: 0x00013C5E
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00015A66 File Offset: 0x00013C66
		public ITextPreprocessor textPreprocessor
		{
			get
			{
				return this.m_TextPreprocessor;
			}
			set
			{
				this.m_TextPreprocessor = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00015A6F File Offset: 0x00013C6F
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00015A77 File Offset: 0x00013C77
		public bool isRightToLeftText
		{
			get
			{
				return this.m_isRightToLeft;
			}
			set
			{
				if (this.m_isRightToLeft == value)
				{
					return;
				}
				this.m_isRightToLeft = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00015A9D File Offset: 0x00013C9D
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00015AA5 File Offset: 0x00013CA5
		public TMP_FontAsset font
		{
			get
			{
				return this.m_fontAsset;
			}
			set
			{
				if (this.m_fontAsset == value)
				{
					return;
				}
				this.m_fontAsset = value;
				this.LoadFontAsset();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00015AD6 File Offset: 0x00013CD6
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00015ADE File Offset: 0x00013CDE
		public virtual Material fontSharedMaterial
		{
			get
			{
				return this.m_sharedMaterial;
			}
			set
			{
				if (this.m_sharedMaterial == value)
				{
					return;
				}
				this.SetSharedMaterial(value);
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00015B09 File Offset: 0x00013D09
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00015B11 File Offset: 0x00013D11
		public virtual Material[] fontSharedMaterials
		{
			get
			{
				return this.GetSharedMaterials();
			}
			set
			{
				this.SetSharedMaterials(value);
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00015B2D File Offset: 0x00013D2D
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00015B3C File Offset: 0x00013D3C
		public Material fontMaterial
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial != null && this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
				{
					return;
				}
				this.m_sharedMaterial = value;
				this.m_padding = this.GetPaddingForMaterial();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00015B91 File Offset: 0x00013D91
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00015B11 File Offset: 0x00013D11
		public virtual Material[] fontMaterials
		{
			get
			{
				return this.GetMaterials(this.m_fontSharedMaterials);
			}
			set
			{
				this.SetSharedMaterials(value);
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00015B9F File Offset: 0x00013D9F
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00015BA7 File Offset: 0x00013DA7
		public override Color color
		{
			get
			{
				return this.m_fontColor;
			}
			set
			{
				if (this.m_fontColor == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_fontColor = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00015BCC File Offset: 0x00013DCC
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00015BD9 File Offset: 0x00013DD9
		public float alpha
		{
			get
			{
				return this.m_fontColor.a;
			}
			set
			{
				if (this.m_fontColor.a == value)
				{
					return;
				}
				this.m_fontColor.a = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00015C03 File Offset: 0x00013E03
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00015C0B File Offset: 0x00013E0B
		public bool enableVertexGradient
		{
			get
			{
				return this.m_enableVertexGradient;
			}
			set
			{
				if (this.m_enableVertexGradient == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableVertexGradient = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00015C2B File Offset: 0x00013E2B
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00015C33 File Offset: 0x00013E33
		public VertexGradient colorGradient
		{
			get
			{
				return this.m_fontColorGradient;
			}
			set
			{
				this.m_havePropertiesChanged = true;
				this.m_fontColorGradient = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00015C49 File Offset: 0x00013E49
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00015C51 File Offset: 0x00013E51
		public TMP_ColorGradient colorGradientPreset
		{
			get
			{
				return this.m_fontColorGradientPreset;
			}
			set
			{
				this.m_havePropertiesChanged = true;
				this.m_fontColorGradientPreset = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00015C67 File Offset: 0x00013E67
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00015C6F File Offset: 0x00013E6F
		public TMP_SpriteAsset spriteAsset
		{
			get
			{
				return this.m_spriteAsset;
			}
			set
			{
				this.m_spriteAsset = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00015C8B File Offset: 0x00013E8B
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00015C93 File Offset: 0x00013E93
		public bool tintAllSprites
		{
			get
			{
				return this.m_tintAllSprites;
			}
			set
			{
				if (this.m_tintAllSprites == value)
				{
					return;
				}
				this.m_tintAllSprites = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00015CB3 File Offset: 0x00013EB3
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00015CBB File Offset: 0x00013EBB
		public TMP_StyleSheet styleSheet
		{
			get
			{
				return this.m_StyleSheet;
			}
			set
			{
				this.m_StyleSheet = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00015CD7 File Offset: 0x00013ED7
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00015D15 File Offset: 0x00013F15
		public TMP_Style textStyle
		{
			get
			{
				this.m_TextStyle = this.GetStyle(this.m_TextStyleHashCode);
				if (this.m_TextStyle == null)
				{
					this.m_TextStyle = TMP_Style.NormalStyle;
					this.m_TextStyleHashCode = this.m_TextStyle.hashCode;
				}
				return this.m_TextStyle;
			}
			set
			{
				this.m_TextStyle = value;
				this.m_TextStyleHashCode = this.m_TextStyle.hashCode;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00015D42 File Offset: 0x00013F42
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00015D4A File Offset: 0x00013F4A
		public bool overrideColorTags
		{
			get
			{
				return this.m_overrideHtmlColors;
			}
			set
			{
				if (this.m_overrideHtmlColors == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_overrideHtmlColors = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00015D6A File Offset: 0x00013F6A
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00015DA2 File Offset: 0x00013FA2
		public Color32 faceColor
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_faceColor;
				}
				this.m_faceColor = this.m_sharedMaterial.GetColor(ShaderUtilities.ID_FaceColor);
				return this.m_faceColor;
			}
			set
			{
				if (this.m_faceColor.Compare(value))
				{
					return;
				}
				this.SetFaceColor(value);
				this.m_havePropertiesChanged = true;
				this.m_faceColor = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00015DD4 File Offset: 0x00013FD4
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00015E0C File Offset: 0x0001400C
		public Color32 outlineColor
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_outlineColor;
				}
				this.m_outlineColor = this.m_sharedMaterial.GetColor(ShaderUtilities.ID_OutlineColor);
				return this.m_outlineColor;
			}
			set
			{
				if (this.m_outlineColor.Compare(value))
				{
					return;
				}
				this.SetOutlineColor(value);
				this.m_havePropertiesChanged = true;
				this.m_outlineColor = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00015E38 File Offset: 0x00014038
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00015E6B File Offset: 0x0001406B
		public float outlineWidth
		{
			get
			{
				if (this.m_sharedMaterial == null)
				{
					return this.m_outlineWidth;
				}
				this.m_outlineWidth = this.m_sharedMaterial.GetFloat(ShaderUtilities.ID_OutlineWidth);
				return this.m_outlineWidth;
			}
			set
			{
				if (this.m_outlineWidth == value)
				{
					return;
				}
				this.SetOutlineThickness(value);
				this.m_havePropertiesChanged = true;
				this.m_outlineWidth = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00015E92 File Offset: 0x00014092
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00015E9A File Offset: 0x0001409A
		public float fontSize
		{
			get
			{
				return this.m_fontSize;
			}
			set
			{
				if (this.m_fontSize == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_fontSize = value;
				if (!this.m_enableAutoSizing)
				{
					this.m_fontSizeBase = this.m_fontSize;
				}
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00015ED4 File Offset: 0x000140D4
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00015EDC File Offset: 0x000140DC
		public FontWeight fontWeight
		{
			get
			{
				return this.m_fontWeight;
			}
			set
			{
				if (this.m_fontWeight == value)
				{
					return;
				}
				this.m_fontWeight = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00015F04 File Offset: 0x00014104
		public float pixelsPerUnit
		{
			get
			{
				Canvas localCanvas = base.canvas;
				if (!localCanvas)
				{
					return 1f;
				}
				if (!this.font)
				{
					return localCanvas.scaleFactor;
				}
				if (this.m_currentFontAsset == null || this.m_currentFontAsset.faceInfo.pointSize <= 0f || this.m_fontSize <= 0f)
				{
					return 1f;
				}
				return this.m_fontSize / this.m_currentFontAsset.faceInfo.pointSize;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00015F8F File Offset: 0x0001418F
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00015F97 File Offset: 0x00014197
		public bool enableAutoSizing
		{
			get
			{
				return this.m_enableAutoSizing;
			}
			set
			{
				if (this.m_enableAutoSizing == value)
				{
					return;
				}
				this.m_enableAutoSizing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00015FB6 File Offset: 0x000141B6
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00015FBE File Offset: 0x000141BE
		public float fontSizeMin
		{
			get
			{
				return this.m_fontSizeMin;
			}
			set
			{
				if (this.m_fontSizeMin == value)
				{
					return;
				}
				this.m_fontSizeMin = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00015FDD File Offset: 0x000141DD
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00015FE5 File Offset: 0x000141E5
		public float fontSizeMax
		{
			get
			{
				return this.m_fontSizeMax;
			}
			set
			{
				if (this.m_fontSizeMax == value)
				{
					return;
				}
				this.m_fontSizeMax = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00016004 File Offset: 0x00014204
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0001600C File Offset: 0x0001420C
		public FontStyles fontStyle
		{
			get
			{
				return this.m_fontStyle;
			}
			set
			{
				if (this.m_fontStyle == value)
				{
					return;
				}
				this.m_fontStyle = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00016032 File Offset: 0x00014232
		public bool isUsingBold
		{
			get
			{
				return this.m_isUsingBold;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001603A File Offset: 0x0001423A
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00016042 File Offset: 0x00014242
		public HorizontalAlignmentOptions horizontalAlignment
		{
			get
			{
				return this.m_HorizontalAlignment;
			}
			set
			{
				if (this.m_HorizontalAlignment == value)
				{
					return;
				}
				this.m_HorizontalAlignment = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00016062 File Offset: 0x00014262
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0001606A File Offset: 0x0001426A
		public VerticalAlignmentOptions verticalAlignment
		{
			get
			{
				return this.m_VerticalAlignment;
			}
			set
			{
				if (this.m_VerticalAlignment == value)
				{
					return;
				}
				this.m_VerticalAlignment = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001608A File Offset: 0x0001428A
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0001609C File Offset: 0x0001429C
		public TextAlignmentOptions alignment
		{
			get
			{
				return (TextAlignmentOptions)(this.m_HorizontalAlignment | (HorizontalAlignmentOptions)this.m_VerticalAlignment);
			}
			set
			{
				HorizontalAlignmentOptions horizontalAlignment = (HorizontalAlignmentOptions)(value & (TextAlignmentOptions)255);
				VerticalAlignmentOptions verticalAlignment = (VerticalAlignmentOptions)(value & (TextAlignmentOptions)65280);
				if (this.m_HorizontalAlignment == horizontalAlignment && this.m_VerticalAlignment == verticalAlignment)
				{
					return;
				}
				this.m_HorizontalAlignment = horizontalAlignment;
				this.m_VerticalAlignment = verticalAlignment;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000160E7 File Offset: 0x000142E7
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000160EF File Offset: 0x000142EF
		public float characterSpacing
		{
			get
			{
				return this.m_characterSpacing;
			}
			set
			{
				if (this.m_characterSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_characterSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00016115 File Offset: 0x00014315
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001611D File Offset: 0x0001431D
		public float wordSpacing
		{
			get
			{
				return this.m_wordSpacing;
			}
			set
			{
				if (this.m_wordSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_wordSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00016143 File Offset: 0x00014343
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001614B File Offset: 0x0001434B
		public float lineSpacing
		{
			get
			{
				return this.m_lineSpacing;
			}
			set
			{
				if (this.m_lineSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_lineSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00016171 File Offset: 0x00014371
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00016179 File Offset: 0x00014379
		public float lineSpacingAdjustment
		{
			get
			{
				return this.m_lineSpacingMax;
			}
			set
			{
				if (this.m_lineSpacingMax == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_lineSpacingMax = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001619F File Offset: 0x0001439F
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000161A7 File Offset: 0x000143A7
		public float paragraphSpacing
		{
			get
			{
				return this.m_paragraphSpacing;
			}
			set
			{
				if (this.m_paragraphSpacing == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_paragraphSpacing = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000161CD File Offset: 0x000143CD
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x000161D5 File Offset: 0x000143D5
		public float characterWidthAdjustment
		{
			get
			{
				return this.m_charWidthMaxAdj;
			}
			set
			{
				if (this.m_charWidthMaxAdj == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_charWidthMaxAdj = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000161FB File Offset: 0x000143FB
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00016203 File Offset: 0x00014403
		public TextWrappingModes textWrappingMode
		{
			get
			{
				return this.m_TextWrappingMode;
			}
			set
			{
				if (this.m_TextWrappingMode == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_TextWrappingMode = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00016229 File Offset: 0x00014429
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00016240 File Offset: 0x00014440
		[Obsolete("The enabledWordWrapping property is now obsolete. Please use the textWrappingMode property instead.")]
		public bool enableWordWrapping
		{
			get
			{
				return this.m_TextWrappingMode == TextWrappingModes.Normal || this.textWrappingMode == TextWrappingModes.PreserveWhitespace;
			}
			set
			{
				TextWrappingModes mode = (value ? TextWrappingModes.Normal : TextWrappingModes.NoWrap);
				if (this.m_TextWrappingMode == mode)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_TextWrappingMode = mode;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00016279 File Offset: 0x00014479
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00016281 File Offset: 0x00014481
		public float wordWrappingRatios
		{
			get
			{
				return this.m_wordWrappingRatios;
			}
			set
			{
				if (this.m_wordWrappingRatios == value)
				{
					return;
				}
				this.m_wordWrappingRatios = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000162A7 File Offset: 0x000144A7
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x000162AF File Offset: 0x000144AF
		public TextOverflowModes overflowMode
		{
			get
			{
				return this.m_overflowMode;
			}
			set
			{
				if (this.m_overflowMode == value)
				{
					return;
				}
				this.m_overflowMode = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000162D5 File Offset: 0x000144D5
		public bool isTextOverflowing
		{
			get
			{
				return this.m_firstOverflowCharacterIndex != -1;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000162E3 File Offset: 0x000144E3
		public int firstOverflowCharacterIndex
		{
			get
			{
				return this.m_firstOverflowCharacterIndex;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000162EB File Offset: 0x000144EB
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x000162F4 File Offset: 0x000144F4
		public TMP_Text linkedTextComponent
		{
			get
			{
				return this.m_linkedTextComponent;
			}
			set
			{
				if (value == null)
				{
					this.ReleaseLinkedTextComponent(this.m_linkedTextComponent);
					this.m_linkedTextComponent = value;
				}
				else
				{
					if (this.IsSelfOrLinkedAncestor(value))
					{
						return;
					}
					this.ReleaseLinkedTextComponent(this.m_linkedTextComponent);
					this.m_linkedTextComponent = value;
					this.m_linkedTextComponent.parentLinkedComponent = this;
				}
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001635B File Offset: 0x0001455B
		public bool isTextTruncated
		{
			get
			{
				return this.m_isTextTruncated;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00016363 File Offset: 0x00014563
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00016378 File Offset: 0x00014578
		[Obsolete("The \"enableKerning\" property has been deprecated. Use the \"fontFeatures\" property to control what features are enabled on the text component.")]
		public bool enableKerning
		{
			get
			{
				return this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.kern);
			}
			set
			{
				if (this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.kern))
				{
					if (value)
					{
						return;
					}
					this.m_ActiveFontFeatures.Remove(OTL_FeatureTag.kern);
					this.m_enableKerning = false;
				}
				else
				{
					if (!value)
					{
						return;
					}
					this.m_ActiveFontFeatures.Add(OTL_FeatureTag.kern);
					this.m_enableKerning = true;
				}
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000163E3 File Offset: 0x000145E3
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000163EB File Offset: 0x000145EB
		public List<OTL_FeatureTag> fontFeatures
		{
			get
			{
				return this.m_ActiveFontFeatures;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_ActiveFontFeatures = value;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001640B File Offset: 0x0001460B
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00016413 File Offset: 0x00014613
		public bool extraPadding
		{
			get
			{
				return this.m_enableExtraPadding;
			}
			set
			{
				if (this.m_enableExtraPadding == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_enableExtraPadding = value;
				this.UpdateMeshPadding();
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00016439 File Offset: 0x00014639
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00016441 File Offset: 0x00014641
		public bool richText
		{
			get
			{
				return this.m_isRichText;
			}
			set
			{
				if (this.m_isRichText == value)
				{
					return;
				}
				this.m_isRichText = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00016467 File Offset: 0x00014667
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0001646F File Offset: 0x0001466F
		public bool emojiFallbackSupport
		{
			get
			{
				return this.m_EmojiFallbackSupport;
			}
			set
			{
				if (this.m_EmojiFallbackSupport == value)
				{
					return;
				}
				this.m_EmojiFallbackSupport = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00016495 File Offset: 0x00014695
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0001649D File Offset: 0x0001469D
		public bool parseCtrlCharacters
		{
			get
			{
				return this.m_parseCtrlCharacters;
			}
			set
			{
				if (this.m_parseCtrlCharacters == value)
				{
					return;
				}
				this.m_parseCtrlCharacters = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000164C3 File Offset: 0x000146C3
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x000164CB File Offset: 0x000146CB
		public bool isOverlay
		{
			get
			{
				return this.m_isOverlay;
			}
			set
			{
				if (this.m_isOverlay == value)
				{
					return;
				}
				this.m_isOverlay = value;
				this.SetShaderDepth();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000164F1 File Offset: 0x000146F1
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x000164F9 File Offset: 0x000146F9
		public bool isOrthographic
		{
			get
			{
				return this.m_isOrthographic;
			}
			set
			{
				if (this.m_isOrthographic == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_isOrthographic = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00016519 File Offset: 0x00014719
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00016521 File Offset: 0x00014721
		public bool enableCulling
		{
			get
			{
				return this.m_isCullingEnabled;
			}
			set
			{
				if (this.m_isCullingEnabled == value)
				{
					return;
				}
				this.m_isCullingEnabled = value;
				this.SetCulling();
				this.m_havePropertiesChanged = true;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00016541 File Offset: 0x00014741
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00016549 File Offset: 0x00014749
		public bool ignoreVisibility
		{
			get
			{
				return this.m_ignoreCulling;
			}
			set
			{
				if (this.m_ignoreCulling == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_ignoreCulling = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00016563 File Offset: 0x00014763
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0001656B File Offset: 0x0001476B
		public TextureMappingOptions horizontalMapping
		{
			get
			{
				return this.m_horizontalMapping;
			}
			set
			{
				if (this.m_horizontalMapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_horizontalMapping = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001658B File Offset: 0x0001478B
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00016593 File Offset: 0x00014793
		public TextureMappingOptions verticalMapping
		{
			get
			{
				return this.m_verticalMapping;
			}
			set
			{
				if (this.m_verticalMapping == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_verticalMapping = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000165B3 File Offset: 0x000147B3
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000165BB File Offset: 0x000147BB
		public float mappingUvLineOffset
		{
			get
			{
				return this.m_uvLineOffset;
			}
			set
			{
				if (this.m_uvLineOffset == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_uvLineOffset = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000165DB File Offset: 0x000147DB
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000165E3 File Offset: 0x000147E3
		public TextRenderFlags renderMode
		{
			get
			{
				return this.m_renderMode;
			}
			set
			{
				if (this.m_renderMode == value)
				{
					return;
				}
				this.m_renderMode = value;
				this.m_havePropertiesChanged = true;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000165FD File Offset: 0x000147FD
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00016605 File Offset: 0x00014805
		public VertexSortingOrder geometrySortingOrder
		{
			get
			{
				return this.m_geometrySortingOrder;
			}
			set
			{
				this.m_geometrySortingOrder = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001661B File Offset: 0x0001481B
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00016623 File Offset: 0x00014823
		public bool isTextObjectScaleStatic
		{
			get
			{
				return this.m_IsTextObjectScaleStatic;
			}
			set
			{
				this.m_IsTextObjectScaleStatic = value;
				if (this.m_IsTextObjectScaleStatic)
				{
					TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
					return;
				}
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00016641 File Offset: 0x00014841
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00016649 File Offset: 0x00014849
		public bool vertexBufferAutoSizeReduction
		{
			get
			{
				return this.m_VertexBufferAutoSizeReduction;
			}
			set
			{
				this.m_VertexBufferAutoSizeReduction = value;
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001665F File Offset: 0x0001485F
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00016667 File Offset: 0x00014867
		public int firstVisibleCharacter
		{
			get
			{
				return this.m_firstVisibleCharacter;
			}
			set
			{
				if (this.m_firstVisibleCharacter == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_firstVisibleCharacter = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00016687 File Offset: 0x00014887
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001668F File Offset: 0x0001488F
		public int maxVisibleCharacters
		{
			get
			{
				return this.m_maxVisibleCharacters;
			}
			set
			{
				if (this.m_maxVisibleCharacters == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleCharacters = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000166AF File Offset: 0x000148AF
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x000166B7 File Offset: 0x000148B7
		public int maxVisibleWords
		{
			get
			{
				return this.m_maxVisibleWords;
			}
			set
			{
				if (this.m_maxVisibleWords == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleWords = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000166D7 File Offset: 0x000148D7
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000166DF File Offset: 0x000148DF
		public int maxVisibleLines
		{
			get
			{
				return this.m_maxVisibleLines;
			}
			set
			{
				if (this.m_maxVisibleLines == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_maxVisibleLines = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000166FF File Offset: 0x000148FF
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00016707 File Offset: 0x00014907
		public bool useMaxVisibleDescender
		{
			get
			{
				return this.m_useMaxVisibleDescender;
			}
			set
			{
				if (this.m_useMaxVisibleDescender == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_useMaxVisibleDescender = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00016727 File Offset: 0x00014927
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0001672F File Offset: 0x0001492F
		public int pageToDisplay
		{
			get
			{
				return this.m_pageToDisplay;
			}
			set
			{
				if (this.m_pageToDisplay == value)
				{
					return;
				}
				this.m_havePropertiesChanged = true;
				this.m_pageToDisplay = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0001674F File Offset: 0x0001494F
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00016757 File Offset: 0x00014957
		public virtual Vector4 margin
		{
			get
			{
				return this.m_margin;
			}
			set
			{
				if (this.m_margin == value)
				{
					return;
				}
				this.m_margin = value;
				this.ComputeMarginSize();
				this.m_havePropertiesChanged = true;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00016782 File Offset: 0x00014982
		public TMP_TextInfo textInfo
		{
			get
			{
				if (this.m_textInfo == null)
				{
					this.m_textInfo = new TMP_TextInfo(this);
				}
				return this.m_textInfo;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001679E File Offset: 0x0001499E
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x000167A6 File Offset: 0x000149A6
		public bool havePropertiesChanged
		{
			get
			{
				return this.m_havePropertiesChanged;
			}
			set
			{
				if (this.m_havePropertiesChanged == value)
				{
					return;
				}
				this.m_havePropertiesChanged = value;
				this.SetAllDirty();
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000167BF File Offset: 0x000149BF
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x000167C7 File Offset: 0x000149C7
		public bool isUsingLegacyAnimationComponent
		{
			get
			{
				return this.m_isUsingLegacyAnimationComponent;
			}
			set
			{
				this.m_isUsingLegacyAnimationComponent = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000167D0 File Offset: 0x000149D0
		public new Transform transform
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = base.GetComponent<Transform>();
				}
				return this.m_transform;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000167F2 File Offset: 0x000149F2
		public new RectTransform rectTransform
		{
			get
			{
				if (this.m_rectTransform == null)
				{
					this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00016814 File Offset: 0x00014A14
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001681C File Offset: 0x00014A1C
		public virtual bool autoSizeTextContainer { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00016825 File Offset: 0x00014A25
		public virtual Mesh mesh
		{
			get
			{
				return this.m_mesh;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001682D File Offset: 0x00014A2D
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00016835 File Offset: 0x00014A35
		public bool isVolumetricText
		{
			get
			{
				return this.m_isVolumetricText;
			}
			set
			{
				if (this.m_isVolumetricText == value)
				{
					return;
				}
				this.m_havePropertiesChanged = value;
				this.m_textInfo.ResetVertexLayout(value);
				this.SetVerticesDirty();
				this.SetLayoutDirty();
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00016860 File Offset: 0x00014A60
		public Bounds bounds
		{
			get
			{
				if (this.m_mesh == null)
				{
					return default(Bounds);
				}
				return this.GetCompoundBounds();
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001688C File Offset: 0x00014A8C
		public Bounds textBounds
		{
			get
			{
				if (this.m_textInfo == null)
				{
					return default(Bounds);
				}
				return this.GetTextBounds();
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060004CA RID: 1226 RVA: 0x000168B4 File Offset: 0x00014AB4
		// (remove) Token: 0x060004CB RID: 1227 RVA: 0x000168E8 File Offset: 0x00014AE8
		public static event Func<int, string, TMP_FontAsset> OnFontAssetRequest;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060004CC RID: 1228 RVA: 0x0001691C File Offset: 0x00014B1C
		// (remove) Token: 0x060004CD RID: 1229 RVA: 0x00016950 File Offset: 0x00014B50
		public static event Func<int, string, TMP_SpriteAsset> OnSpriteAssetRequest;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004CE RID: 1230 RVA: 0x00016984 File Offset: 0x00014B84
		// (remove) Token: 0x060004CF RID: 1231 RVA: 0x000169B8 File Offset: 0x00014BB8
		public static event TMP_Text.MissingCharacterEventCallback OnMissingCharacter;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004D0 RID: 1232 RVA: 0x000169EC File Offset: 0x00014BEC
		// (remove) Token: 0x060004D1 RID: 1233 RVA: 0x00016A24 File Offset: 0x00014C24
		public virtual event Action<TMP_TextInfo> OnPreRenderText = delegate
		{
		};

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00016A5C File Offset: 0x00014C5C
		protected TMP_SpriteAnimator spriteAnimator
		{
			get
			{
				if (this.m_spriteAnimator == null)
				{
					this.m_spriteAnimator = base.GetComponent<TMP_SpriteAnimator>();
					if (this.m_spriteAnimator == null)
					{
						this.m_spriteAnimator = base.gameObject.AddComponent<TMP_SpriteAnimator>();
					}
				}
				return this.m_spriteAnimator;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00016AA8 File Offset: 0x00014CA8
		public float flexibleHeight
		{
			get
			{
				return this.m_flexibleHeight;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public float flexibleWidth
		{
			get
			{
				return this.m_flexibleWidth;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00016AB8 File Offset: 0x00014CB8
		public float minWidth
		{
			get
			{
				return this.m_minWidth;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00016AC0 File Offset: 0x00014CC0
		public float minHeight
		{
			get
			{
				return this.m_minHeight;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00016AC8 File Offset: 0x00014CC8
		public float maxWidth
		{
			get
			{
				return this.m_maxWidth;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00016AD0 File Offset: 0x00014CD0
		public float maxHeight
		{
			get
			{
				return this.m_maxHeight;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00016AD8 File Offset: 0x00014CD8
		protected LayoutElement layoutElement
		{
			get
			{
				if (this.m_LayoutElement == null)
				{
					this.m_LayoutElement = base.GetComponent<LayoutElement>();
				}
				return this.m_LayoutElement;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016AFA File Offset: 0x00014CFA
		public virtual float preferredWidth
		{
			get
			{
				this.m_preferredWidth = this.GetPreferredWidth();
				return this.m_preferredWidth;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016B0E File Offset: 0x00014D0E
		public virtual float preferredHeight
		{
			get
			{
				this.m_preferredHeight = this.GetPreferredHeight();
				return this.m_preferredHeight;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00016B22 File Offset: 0x00014D22
		public virtual float renderedWidth
		{
			get
			{
				return this.GetRenderedWidth();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00016B2A File Offset: 0x00014D2A
		public virtual float renderedHeight
		{
			get
			{
				return this.GetRenderedHeight();
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00016B32 File Offset: 0x00014D32
		public int layoutPriority
		{
			get
			{
				return this.m_layoutPriority;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void LoadFontAsset()
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetSharedMaterial(Material mat)
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00016B3A File Offset: 0x00014D3A
		protected virtual Material GetMaterial(Material mat)
		{
			return null;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetFontBaseMaterial(Material mat)
		{
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00016B3A File Offset: 0x00014D3A
		protected virtual Material[] GetSharedMaterials()
		{
			return null;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetSharedMaterials(Material[] materials)
		{
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016B3A File Offset: 0x00014D3A
		protected virtual Material[] GetMaterials(Material[] mats)
		{
			return null;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015243 File Offset: 0x00013443
		protected virtual Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016B40 File Offset: 0x00014D40
		protected void SetVertexColorGradient(TMP_ColorGradient gradient)
		{
			if (gradient == null)
			{
				return;
			}
			this.m_fontColorGradient.bottomLeft = gradient.bottomLeft;
			this.m_fontColorGradient.bottomRight = gradient.bottomRight;
			this.m_fontColorGradient.topLeft = gradient.topLeft;
			this.m_fontColorGradient.topRight = gradient.topRight;
			this.SetVerticesDirty();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected void SetTextSortingOrder(VertexSortingOrder order)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected void SetTextSortingOrder(int[] order)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetFaceColor(Color32 color)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetOutlineColor(Color32 color)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetOutlineThickness(float thickness)
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetShaderDepth()
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetCulling()
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00002AAB File Offset: 0x00000CAB
		internal virtual void UpdateCulling()
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016BA4 File Offset: 0x00014DA4
		protected virtual float GetPaddingForMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (this.m_sharedMaterial == null)
			{
				return 0f;
			}
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_isSDFShader = this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_WeightNormal);
			return this.m_padding;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016C14 File Offset: 0x00014E14
		protected virtual float GetPaddingForMaterial(Material mat)
		{
			if (mat == null)
			{
				return 0f;
			}
			this.m_padding = ShaderUtilities.GetPadding(mat, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_isSDFShader = mat.HasProperty(ShaderUtilities.ID_WeightNormal);
			return this.m_padding;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016B3A File Offset: 0x00014D3A
		protected virtual Vector3[] GetTextContainerLocalCorners()
		{
			return null;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void ForceMeshUpdate(bool ignoreActiveState = false, bool forceTextReparsing = false)
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void UpdateGeometry(Mesh mesh, int index)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void UpdateVertexData()
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void SetVertices(Vector3[] vertices)
		{
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void UpdateMeshPadding()
		{
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016C70 File Offset: 0x00014E70
		public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
			this.InternalCrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00016C88 File Offset: 0x00014E88
		public override void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			base.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
			this.InternalCrossFadeAlpha(alpha, duration, ignoreTimeScale);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void InternalCrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void InternalCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00016C9C File Offset: 0x00014E9C
		protected void ParseInputText()
		{
			switch (this.m_inputSource)
			{
			case TMP_Text.TextInputSources.TextInputBox:
			case TMP_Text.TextInputSources.TextString:
				this.PopulateTextBackingArray((this.m_TextPreprocessor == null) ? this.m_text : this.m_TextPreprocessor.PreprocessText(this.m_text));
				this.PopulateTextProcessingArray();
				break;
			}
			this.SetArraySizes(this.m_TextProcessingArray);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00016D04 File Offset: 0x00014F04
		private void PopulateTextBackingArray(string sourceText)
		{
			int srcLength = ((sourceText == null) ? 0 : sourceText.Length);
			this.PopulateTextBackingArray(sourceText, 0, srcLength);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00016D28 File Offset: 0x00014F28
		private void PopulateTextBackingArray(string sourceText, int start, int length)
		{
			int writeIndex = 0;
			int readIndex;
			if (sourceText == null)
			{
				readIndex = 0;
				length = 0;
			}
			else
			{
				readIndex = Mathf.Clamp(start, 0, sourceText.Length);
				length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
			}
			if (length >= this.m_TextBackingArray.Capacity)
			{
				this.m_TextBackingArray.Resize(length);
			}
			int end = readIndex + length;
			while (readIndex < end)
			{
				this.m_TextBackingArray[writeIndex] = (uint)sourceText[readIndex];
				writeIndex++;
				readIndex++;
			}
			this.m_TextBackingArray[writeIndex] = 0U;
			this.m_TextBackingArray.Count = writeIndex;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00016DC8 File Offset: 0x00014FC8
		private void PopulateTextBackingArray(StringBuilder sourceText, int start, int length)
		{
			int writeIndex = 0;
			int readIndex;
			if (sourceText == null)
			{
				readIndex = 0;
				length = 0;
			}
			else
			{
				readIndex = Mathf.Clamp(start, 0, sourceText.Length);
				length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
			}
			if (length >= this.m_TextBackingArray.Capacity)
			{
				this.m_TextBackingArray.Resize(length);
			}
			int end = readIndex + length;
			while (readIndex < end)
			{
				this.m_TextBackingArray[writeIndex] = (uint)sourceText[readIndex];
				writeIndex++;
				readIndex++;
			}
			this.m_TextBackingArray[writeIndex] = 0U;
			this.m_TextBackingArray.Count = writeIndex;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00016E68 File Offset: 0x00015068
		private void PopulateTextBackingArray(char[] sourceText, int start, int length)
		{
			int writeIndex = 0;
			int readIndex;
			if (sourceText == null)
			{
				readIndex = 0;
				length = 0;
			}
			else
			{
				readIndex = Mathf.Clamp(start, 0, sourceText.Length);
				length = Mathf.Clamp(length, 0, (start + length < sourceText.Length) ? length : (sourceText.Length - start));
			}
			if (length >= this.m_TextBackingArray.Capacity)
			{
				this.m_TextBackingArray.Resize(length);
			}
			int end = readIndex + length;
			while (readIndex < end)
			{
				this.m_TextBackingArray[writeIndex] = (uint)sourceText[readIndex];
				writeIndex++;
				readIndex++;
			}
			this.m_TextBackingArray[writeIndex] = 0U;
			this.m_TextBackingArray.Count = writeIndex;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00016EFC File Offset: 0x000150FC
		private void PopulateTextProcessingArray()
		{
			TMP_TextProcessingStack<int>.SetDefault(this.m_TextStyleStacks, 0);
			int srcLength = this.m_TextBackingArray.Count;
			int num = srcLength;
			string styleOpeningDefinition = this.textStyle.styleOpeningDefinition;
			int requiredCapacity = num + ((styleOpeningDefinition != null) ? styleOpeningDefinition.Length : 0);
			if (this.m_TextProcessingArray.Length < requiredCapacity)
			{
				this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray, requiredCapacity);
			}
			this.m_TextStyleStackDepth = 0;
			int writeIndex = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertOpeningStyleTag(this.m_TextStyle, ref this.m_TextProcessingArray, ref writeIndex);
			}
			this.tag_NoParsing = false;
			int readIndex = 0;
			while (readIndex < srcLength)
			{
				uint c = this.m_TextBackingArray[readIndex];
				if (c == 0U)
				{
					break;
				}
				if (c != 92U || readIndex >= srcLength - 1)
				{
					goto IL_0329;
				}
				uint num2 = this.m_TextBackingArray[readIndex + 1];
				if (num2 != 85U)
				{
					if (num2 != 92U)
					{
						switch (num2)
						{
						case 110U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0329;
							}
							this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 1,
								unicode = 10U
							};
							readIndex++;
							writeIndex++;
							break;
						case 111U:
						case 112U:
						case 113U:
						case 115U:
							goto IL_0329;
						case 114U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0329;
							}
							this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 1,
								unicode = 13U
							};
							readIndex++;
							writeIndex++;
							break;
						case 116U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0329;
							}
							this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 1,
								unicode = 9U
							};
							readIndex++;
							writeIndex++;
							break;
						case 117U:
							if (srcLength <= readIndex + 5 || !this.IsValidUTF16(this.m_TextBackingArray, readIndex + 2))
							{
								goto IL_0329;
							}
							this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 6,
								unicode = this.GetUTF16(this.m_TextBackingArray, readIndex + 2)
							};
							readIndex += 5;
							writeIndex++;
							break;
						case 118U:
							if (!this.m_parseCtrlCharacters)
							{
								goto IL_0329;
							}
							this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 1,
								unicode = 11U
							};
							readIndex++;
							writeIndex++;
							break;
						default:
							goto IL_0329;
						}
					}
					else
					{
						if (this.m_parseCtrlCharacters)
						{
							readIndex++;
							goto IL_0329;
						}
						goto IL_0329;
					}
				}
				else
				{
					if (srcLength <= readIndex + 9 || !this.IsValidUTF32(this.m_TextBackingArray, readIndex + 2))
					{
						goto IL_0329;
					}
					this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = readIndex,
						length = 10,
						unicode = this.GetUTF32(this.m_TextBackingArray, readIndex + 2)
					};
					readIndex += 9;
					writeIndex++;
				}
				IL_08AF:
				readIndex++;
				continue;
				IL_0329:
				if (c >= 55296U && c <= 56319U && srcLength > readIndex + 1 && this.m_TextBackingArray[readIndex + 1] >= 56320U && this.m_TextBackingArray[readIndex + 1] <= 57343U)
				{
					this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
					{
						elementType = TextProcessingElementType.TextCharacterElement,
						stringIndex = readIndex,
						length = 2,
						unicode = TMP_TextParsingUtilities.ConvertToUTF32(c, this.m_TextBackingArray[readIndex + 1])
					};
					readIndex++;
					writeIndex++;
					goto IL_08AF;
				}
				if (c == 60U && this.m_isRichText)
				{
					MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(this.m_TextBackingArray, readIndex + 1);
					if (markupTagHashCode <= MarkupTag.CR)
					{
						if (markupTagHashCode <= MarkupTag.A)
						{
							if (markupTagHashCode != MarkupTag.NO_PARSE)
							{
								if (markupTagHashCode != MarkupTag.SLASH_NO_PARSE)
								{
									if (markupTagHashCode == MarkupTag.A)
									{
										if (this.m_TextBackingArray.Count > readIndex + 4 && this.m_TextBackingArray[readIndex + 3] == 104U && this.m_TextBackingArray[readIndex + 4] == 114U)
										{
											this.InsertOpeningTextStyle(this.GetStyle(65), ref this.m_TextProcessingArray, ref writeIndex);
										}
									}
								}
								else
								{
									this.tag_NoParsing = false;
								}
							}
							else
							{
								this.tag_NoParsing = true;
							}
						}
						else if (markupTagHashCode != MarkupTag.SLASH_A)
						{
							if (markupTagHashCode != MarkupTag.BR)
							{
								if (markupTagHashCode == MarkupTag.CR)
								{
									if (!this.tag_NoParsing)
									{
										if (writeIndex == this.m_TextProcessingArray.Length)
										{
											this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
										}
										this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
										{
											elementType = TextProcessingElementType.TextCharacterElement,
											stringIndex = readIndex,
											length = 4,
											unicode = 13U
										};
										writeIndex++;
										readIndex += 3;
										goto IL_08AF;
									}
								}
							}
							else if (!this.tag_NoParsing)
							{
								if (writeIndex == this.m_TextProcessingArray.Length)
								{
									this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
								}
								this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
								{
									elementType = TextProcessingElementType.TextCharacterElement,
									stringIndex = readIndex,
									length = 4,
									unicode = 10U
								};
								writeIndex++;
								readIndex += 3;
								goto IL_08AF;
							}
						}
						else
						{
							this.InsertClosingTextStyle(this.GetStyle(65), ref this.m_TextProcessingArray, ref writeIndex);
						}
					}
					else if (markupTagHashCode <= MarkupTag.NBSP)
					{
						if (markupTagHashCode != MarkupTag.SHY)
						{
							if (markupTagHashCode != MarkupTag.ZWJ)
							{
								if (markupTagHashCode == MarkupTag.NBSP)
								{
									if (!this.tag_NoParsing)
									{
										if (writeIndex == this.m_TextProcessingArray.Length)
										{
											this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
										}
										this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
										{
											elementType = TextProcessingElementType.TextCharacterElement,
											stringIndex = readIndex,
											length = 6,
											unicode = 160U
										};
										writeIndex++;
										readIndex += 5;
										goto IL_08AF;
									}
								}
							}
							else if (!this.tag_NoParsing)
							{
								if (writeIndex == this.m_TextProcessingArray.Length)
								{
									this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
								}
								this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
								{
									elementType = TextProcessingElementType.TextCharacterElement,
									stringIndex = readIndex,
									length = 5,
									unicode = 8205U
								};
								writeIndex++;
								readIndex += 4;
								goto IL_08AF;
							}
						}
						else if (!this.tag_NoParsing)
						{
							if (writeIndex == this.m_TextProcessingArray.Length)
							{
								this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
							}
							this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
							{
								elementType = TextProcessingElementType.TextCharacterElement,
								stringIndex = readIndex,
								length = 5,
								unicode = 173U
							};
							writeIndex++;
							readIndex += 4;
							goto IL_08AF;
						}
					}
					else if (markupTagHashCode != MarkupTag.ZWSP)
					{
						if (markupTagHashCode != MarkupTag.STYLE)
						{
							if (markupTagHashCode == MarkupTag.SLASH_STYLE)
							{
								if (!this.tag_NoParsing)
								{
									int closeWriteIndex = writeIndex;
									this.ReplaceClosingStyleTag(ref this.m_TextProcessingArray, ref writeIndex);
									while (closeWriteIndex < writeIndex)
									{
										this.m_TextProcessingArray[closeWriteIndex].stringIndex = readIndex;
										this.m_TextProcessingArray[closeWriteIndex].length = 8;
										closeWriteIndex++;
									}
									readIndex += 7;
									goto IL_08AF;
								}
							}
						}
						else if (!this.tag_NoParsing)
						{
							int openWriteIndex = writeIndex;
							int srcOffset;
							if (this.ReplaceOpeningStyleTag(ref this.m_TextBackingArray, readIndex, out srcOffset, ref this.m_TextProcessingArray, ref writeIndex))
							{
								while (openWriteIndex < writeIndex)
								{
									this.m_TextProcessingArray[openWriteIndex].stringIndex = readIndex;
									this.m_TextProcessingArray[openWriteIndex].length = srcOffset - readIndex + 1;
									openWriteIndex++;
								}
								readIndex = srcOffset;
								goto IL_08AF;
							}
						}
					}
					else if (!this.tag_NoParsing)
					{
						if (writeIndex == this.m_TextProcessingArray.Length)
						{
							this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
						}
						this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
						{
							elementType = TextProcessingElementType.TextCharacterElement,
							stringIndex = readIndex,
							length = 6,
							unicode = 8203U
						};
						writeIndex++;
						readIndex += 5;
						goto IL_08AF;
					}
				}
				if (writeIndex == this.m_TextProcessingArray.Length)
				{
					this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
				}
				this.m_TextProcessingArray[writeIndex] = new TMP_Text.TextProcessingElement
				{
					elementType = TextProcessingElementType.TextCharacterElement,
					stringIndex = readIndex,
					length = 1,
					unicode = c
				};
				writeIndex++;
				goto IL_08AF;
			}
			this.m_TextStyleStackDepth = 0;
			if (this.textStyle.hashCode != -1183493901)
			{
				this.InsertClosingStyleTag(ref this.m_TextProcessingArray, ref writeIndex);
			}
			if (writeIndex == this.m_TextProcessingArray.Length)
			{
				this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref this.m_TextProcessingArray);
			}
			this.m_TextProcessingArray[writeIndex].unicode = 0U;
			this.m_InternalTextProcessingArraySize = writeIndex;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001781C File Offset: 0x00015A1C
		private void SetTextInternal(string sourceText)
		{
			int srcLength = ((sourceText == null) ? 0 : sourceText.Length);
			this.PopulateTextBackingArray(sourceText, 0, srcLength);
			TMP_Text.TextInputSources currentInputSource = this.m_inputSource;
			this.m_inputSource = TMP_Text.TextInputSources.TextString;
			this.PopulateTextProcessingArray();
			this.m_inputSource = currentInputSource;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001785C File Offset: 0x00015A5C
		public void SetText(string sourceText)
		{
			int srcLength = ((sourceText == null) ? 0 : sourceText.Length);
			this.PopulateTextBackingArray(sourceText, 0, srcLength);
			this.m_text = sourceText;
			this.m_inputSource = TMP_Text.TextInputSources.TextString;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000178A8 File Offset: 0x00015AA8
		[Obsolete("Use the SetText(string) function instead.")]
		public void SetText(string sourceText, bool syncTextInputBox = true)
		{
			int srcLength = ((sourceText == null) ? 0 : sourceText.Length);
			this.PopulateTextBackingArray(sourceText, 0, srcLength);
			this.m_text = sourceText;
			this.m_inputSource = TMP_Text.TextInputSources.TextString;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000178F4 File Offset: 0x00015AF4
		public void SetText(string sourceText, float arg0)
		{
			this.SetText(sourceText, arg0, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001792C File Offset: 0x00015B2C
		public void SetText(string sourceText, float arg0, float arg1)
		{
			this.SetText(sourceText, arg0, arg1, 0f, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00017960 File Offset: 0x00015B60
		public void SetText(string sourceText, float arg0, float arg1, float arg2)
		{
			this.SetText(sourceText, arg0, arg1, arg2, 0f, 0f, 0f, 0f, 0f);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017994 File Offset: 0x00015B94
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, 0f, 0f, 0f, 0f);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000179C4 File Offset: 0x00015BC4
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, arg4, 0f, 0f, 0f);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000179F0 File Offset: 0x00015BF0
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4, float arg5)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, arg4, arg5, 0f, 0f);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017A18 File Offset: 0x00015C18
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4, float arg5, float arg6)
		{
			this.SetText(sourceText, arg0, arg1, arg2, arg3, arg4, arg5, arg6, 0f);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00017A40 File Offset: 0x00015C40
		public void SetText(string sourceText, float arg0, float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7)
		{
			int argIndex = 0;
			int padding = 0;
			int decimalPrecision = 0;
			int readFlag = 0;
			int readIndex = 0;
			int writeIndex = 0;
			while (readIndex < sourceText.Length)
			{
				char c = sourceText[readIndex];
				if (c == '{')
				{
					readFlag = 1;
				}
				else if (c == '}')
				{
					switch (argIndex)
					{
					case 0:
						this.AddFloatToInternalTextBackingArray(arg0, padding, decimalPrecision, ref writeIndex);
						break;
					case 1:
						this.AddFloatToInternalTextBackingArray(arg1, padding, decimalPrecision, ref writeIndex);
						break;
					case 2:
						this.AddFloatToInternalTextBackingArray(arg2, padding, decimalPrecision, ref writeIndex);
						break;
					case 3:
						this.AddFloatToInternalTextBackingArray(arg3, padding, decimalPrecision, ref writeIndex);
						break;
					case 4:
						this.AddFloatToInternalTextBackingArray(arg4, padding, decimalPrecision, ref writeIndex);
						break;
					case 5:
						this.AddFloatToInternalTextBackingArray(arg5, padding, decimalPrecision, ref writeIndex);
						break;
					case 6:
						this.AddFloatToInternalTextBackingArray(arg6, padding, decimalPrecision, ref writeIndex);
						break;
					case 7:
						this.AddFloatToInternalTextBackingArray(arg7, padding, decimalPrecision, ref writeIndex);
						break;
					}
					argIndex = 0;
					readFlag = 0;
					padding = 0;
					decimalPrecision = 0;
				}
				else if (readFlag == 1 && c >= '0' && c <= '8')
				{
					argIndex = (int)(c - '0');
					readFlag = 2;
				}
				else
				{
					if (readFlag == 2)
					{
						if (c == ':')
						{
							goto IL_0150;
						}
						if (c == '.')
						{
							readFlag = 3;
							goto IL_0150;
						}
						if (c == '#')
						{
							goto IL_0150;
						}
						if (c == '0')
						{
							padding++;
							goto IL_0150;
						}
						if (c == ',')
						{
							goto IL_0150;
						}
						if (c >= '1' && c <= '9')
						{
							decimalPrecision = (int)(c - '0');
							goto IL_0150;
						}
					}
					if (readFlag == 3 && c == '0')
					{
						decimalPrecision++;
					}
					else
					{
						this.m_TextBackingArray[writeIndex] = (uint)c;
						writeIndex++;
					}
				}
				IL_0150:
				readIndex++;
			}
			this.m_TextBackingArray[writeIndex] = 0U;
			this.m_TextBackingArray.Count = writeIndex;
			this.m_IsTextBackingStringDirty = true;
			this.m_inputSource = TMP_Text.TextInputSources.SetText;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00017BF4 File Offset: 0x00015DF4
		public void SetText(StringBuilder sourceText)
		{
			int srcLength = ((sourceText == null) ? 0 : sourceText.Length);
			this.SetText(sourceText, 0, srcLength);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00017C17 File Offset: 0x00015E17
		private void SetText(StringBuilder sourceText, int start, int length)
		{
			this.PopulateTextBackingArray(sourceText, start, length);
			this.m_IsTextBackingStringDirty = true;
			this.m_inputSource = TMP_Text.TextInputSources.SetTextArray;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00017C4C File Offset: 0x00015E4C
		public void SetText(char[] sourceText)
		{
			int srcLength = ((sourceText == null) ? 0 : sourceText.Length);
			this.SetCharArray(sourceText, 0, srcLength);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00017C6C File Offset: 0x00015E6C
		public void SetText(char[] sourceText, int start, int length)
		{
			this.SetCharArray(sourceText, start, length);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00017C78 File Offset: 0x00015E78
		public void SetCharArray(char[] sourceText)
		{
			int srcLength = ((sourceText == null) ? 0 : sourceText.Length);
			this.SetCharArray(sourceText, 0, srcLength);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017C98 File Offset: 0x00015E98
		public void SetCharArray(char[] sourceText, int start, int length)
		{
			this.PopulateTextBackingArray(sourceText, start, length);
			this.m_IsTextBackingStringDirty = true;
			this.m_inputSource = TMP_Text.TextInputSources.SetTextArray;
			this.PopulateTextProcessingArray();
			this.m_havePropertiesChanged = true;
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017CCC File Offset: 0x00015ECC
		private TMP_Style GetStyle(int hashCode)
		{
			TMP_Style style = null;
			if (this.m_StyleSheet != null)
			{
				style = this.m_StyleSheet.GetStyle(hashCode);
				if (style != null)
				{
					return style;
				}
			}
			if (TMP_Settings.defaultStyleSheet != null)
			{
				style = TMP_Settings.defaultStyleSheet.GetStyle(hashCode);
			}
			return style;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00017D18 File Offset: 0x00015F18
		private void InsertOpeningTextStyle(TMP_Style style, ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex)
		{
			this.m_TextStyleStackDepth++;
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			uint[] styleDefinition = style.styleOpeningTagArray;
			this.InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleDefinition);
			this.m_TextStyleStackDepth--;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017D70 File Offset: 0x00015F70
		private void InsertClosingTextStyle(TMP_Style style, ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex)
		{
			this.m_TextStyleStackDepth++;
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			uint[] styleDefinition = style.styleClosingTagArray;
			this.InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleDefinition);
			this.m_TextStyleStackDepth--;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017DC8 File Offset: 0x00015FC8
		private void InsertTextStyleInTextProcessingArray(ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex, uint[] styleDefinition)
		{
			int styleLength = styleDefinition.Length;
			if (writeIndex + styleLength >= charBuffer.Length)
			{
				this.ResizeInternalArray<TMP_Text.TextProcessingElement>(ref charBuffer, writeIndex + styleLength);
			}
			int i = 0;
			while (i < styleLength)
			{
				uint c = styleDefinition[i];
				if (c == 92U && i + 1 < styleLength)
				{
					uint num = styleDefinition[i + 1];
					if (num <= 92U)
					{
						if (num != 85U)
						{
							if (num == 92U)
							{
								i++;
							}
						}
						else if (i + 9 < styleLength)
						{
							c = this.GetUTF32(styleDefinition, i + 2);
							i += 9;
						}
					}
					else if (num != 110U)
					{
						switch (num)
						{
						case 117U:
							if (i + 5 < styleLength)
							{
								c = this.GetUTF16(styleDefinition, i + 2);
								i += 5;
							}
							break;
						}
					}
					else
					{
						c = 10U;
						i++;
					}
				}
				if (c != 60U)
				{
					goto IL_02A4;
				}
				MarkupTag markupTagHashCode = (MarkupTag)this.GetMarkupTagHashCode(styleDefinition, i + 1);
				if (markupTagHashCode <= MarkupTag.SHY)
				{
					if (markupTagHashCode <= MarkupTag.SLASH_NO_PARSE)
					{
						if (markupTagHashCode == MarkupTag.NO_PARSE)
						{
							this.tag_NoParsing = true;
							goto IL_02A4;
						}
						if (markupTagHashCode != MarkupTag.SLASH_NO_PARSE)
						{
							goto IL_02A4;
						}
						this.tag_NoParsing = false;
						goto IL_02A4;
					}
					else if (markupTagHashCode != MarkupTag.BR)
					{
						if (markupTagHashCode != MarkupTag.CR)
						{
							if (markupTagHashCode != MarkupTag.SHY)
							{
								goto IL_02A4;
							}
							if (this.tag_NoParsing)
							{
								goto IL_02A4;
							}
							charBuffer[writeIndex].unicode = 173U;
							writeIndex++;
							i += 4;
						}
						else
						{
							if (this.tag_NoParsing)
							{
								goto IL_02A4;
							}
							charBuffer[writeIndex].unicode = 13U;
							writeIndex++;
							i += 3;
						}
					}
					else
					{
						if (this.tag_NoParsing)
						{
							goto IL_02A4;
						}
						charBuffer[writeIndex].unicode = 10U;
						writeIndex++;
						i += 3;
					}
				}
				else if (markupTagHashCode <= MarkupTag.NBSP)
				{
					if (markupTagHashCode != MarkupTag.ZWJ)
					{
						if (markupTagHashCode != MarkupTag.NBSP)
						{
							goto IL_02A4;
						}
						if (this.tag_NoParsing)
						{
							goto IL_02A4;
						}
						charBuffer[writeIndex].unicode = 160U;
						writeIndex++;
						i += 5;
					}
					else
					{
						if (this.tag_NoParsing)
						{
							goto IL_02A4;
						}
						charBuffer[writeIndex].unicode = 8205U;
						writeIndex++;
						i += 4;
					}
				}
				else if (markupTagHashCode != MarkupTag.ZWSP)
				{
					if (markupTagHashCode != MarkupTag.STYLE)
					{
						if (markupTagHashCode != MarkupTag.SLASH_STYLE)
						{
							goto IL_02A4;
						}
						if (this.tag_NoParsing)
						{
							goto IL_02A4;
						}
						this.ReplaceClosingStyleTag(ref charBuffer, ref writeIndex);
						i += 7;
					}
					else
					{
						int offset;
						if (this.tag_NoParsing || !this.ReplaceOpeningStyleTag(ref styleDefinition, i, out offset, ref charBuffer, ref writeIndex))
						{
							goto IL_02A4;
						}
						i = offset;
					}
				}
				else
				{
					if (this.tag_NoParsing)
					{
						goto IL_02A4;
					}
					charBuffer[writeIndex].unicode = 8203U;
					writeIndex++;
					i += 5;
				}
				IL_02B9:
				i++;
				continue;
				IL_02A4:
				charBuffer[writeIndex].unicode = c;
				writeIndex++;
				goto IL_02B9;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001809C File Offset: 0x0001629C
		private bool ReplaceOpeningStyleTag(ref TMP_Text.TextBackingContainer sourceText, int srcIndex, out int srcOffset, ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex)
		{
			int styleHashCode = this.GetStyleHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(styleHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStackDepth++;
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			uint[] styleDefinition = style.styleOpeningTagArray;
			this.InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleDefinition);
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00018114 File Offset: 0x00016314
		private bool ReplaceOpeningStyleTag(ref uint[] sourceText, int srcIndex, out int srcOffset, ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex)
		{
			int styleHashCode = this.GetStyleHashCode(ref sourceText, srcIndex + 7, out srcOffset);
			TMP_Style style = this.GetStyle(styleHashCode);
			if (style == null || srcOffset == 0)
			{
				return false;
			}
			this.m_TextStyleStackDepth++;
			this.m_TextStyleStacks[this.m_TextStyleStackDepth].Push(style.hashCode);
			uint[] styleDefinition = style.styleOpeningTagArray;
			this.InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleDefinition);
			this.m_TextStyleStackDepth--;
			return true;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001818C File Offset: 0x0001638C
		private void ReplaceClosingStyleTag(ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex)
		{
			int styleHashCode = this.m_TextStyleStacks[this.m_TextStyleStackDepth + 1].Pop();
			TMP_Style style = this.GetStyle(styleHashCode);
			if (style == null)
			{
				return;
			}
			this.m_TextStyleStackDepth++;
			uint[] styleDefinition = style.styleClosingTagArray;
			this.InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleDefinition);
			this.m_TextStyleStackDepth--;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000181EC File Offset: 0x000163EC
		private void InsertOpeningStyleTag(TMP_Style style, ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex)
		{
			if (style == null)
			{
				return;
			}
			this.m_TextStyleStacks[0].Push(style.hashCode);
			uint[] styleDefinition = style.styleOpeningTagArray;
			this.InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleDefinition);
			this.m_TextStyleStackDepth = 0;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001822C File Offset: 0x0001642C
		private void InsertClosingStyleTag(ref TMP_Text.TextProcessingElement[] charBuffer, ref int writeIndex)
		{
			int styleHashCode = this.m_TextStyleStacks[0].Pop();
			uint[] styleDefinition = this.GetStyle(styleHashCode).styleClosingTagArray;
			this.InsertTextStyleInTextProcessingArray(ref charBuffer, ref writeIndex, styleDefinition);
			this.m_TextStyleStackDepth = 0;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00018268 File Offset: 0x00016468
		private int GetMarkupTagHashCode(uint[] styleDefinition, int readIndex)
		{
			int hashCode = 0;
			int maxReadIndex = readIndex + 16;
			int styleDefinitionLength = styleDefinition.Length;
			while (readIndex < maxReadIndex && readIndex < styleDefinitionLength)
			{
				uint c = styleDefinition[readIndex];
				if (c == 62U || c == 61U || c == 32U)
				{
					return hashCode;
				}
				hashCode = ((hashCode << 5) + hashCode) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast(c);
				readIndex++;
			}
			return hashCode;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000182B4 File Offset: 0x000164B4
		private int GetMarkupTagHashCode(TMP_Text.TextBackingContainer styleDefinition, int readIndex)
		{
			int hashCode = 0;
			int maxReadIndex = readIndex + 16;
			int styleDefinitionLength = styleDefinition.Capacity;
			while (readIndex < maxReadIndex && readIndex < styleDefinitionLength)
			{
				uint c = styleDefinition[readIndex];
				if (c == 62U || c == 61U || c == 32U)
				{
					return hashCode;
				}
				hashCode = ((hashCode << 5) + hashCode) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast(c);
				readIndex++;
			}
			return hashCode;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00018308 File Offset: 0x00016508
		private int GetStyleHashCode(ref uint[] text, int index, out int closeIndex)
		{
			int hashCode = 0;
			closeIndex = 0;
			for (int i = index; i < text.Length; i++)
			{
				if (text[i] != 34U)
				{
					if (text[i] == 62U)
					{
						closeIndex = i;
						break;
					}
					hashCode = ((hashCode << 5) + hashCode) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast((char)text[i]);
				}
			}
			return hashCode;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00018350 File Offset: 0x00016550
		private int GetStyleHashCode(ref TMP_Text.TextBackingContainer text, int index, out int closeIndex)
		{
			int hashCode = 0;
			closeIndex = 0;
			for (int i = index; i < text.Capacity; i++)
			{
				if (text[i] != 34U)
				{
					if (text[i] == 62U)
					{
						closeIndex = i;
						break;
					}
					hashCode = ((hashCode << 5) + hashCode) ^ (int)TMP_TextParsingUtilities.ToUpperASCIIFast((char)text[i]);
				}
			}
			return hashCode;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000183A4 File Offset: 0x000165A4
		private void ResizeInternalArray<T>(ref T[] array)
		{
			int size = Mathf.NextPowerOfTwo(array.Length + 1);
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000183C4 File Offset: 0x000165C4
		private void ResizeInternalArray<T>(ref T[] array, int size)
		{
			size = Mathf.NextPowerOfTwo(size + 1);
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000183D8 File Offset: 0x000165D8
		private void AddFloatToInternalTextBackingArray(float value, int padding, int precision, ref int writeIndex)
		{
			if (value < 0f)
			{
				this.m_TextBackingArray[writeIndex] = 45U;
				writeIndex++;
				value = -value;
			}
			decimal valueD = (decimal)value;
			if (padding == 0 && precision == 0)
			{
				precision = 9;
			}
			else
			{
				valueD += this.k_Power[Mathf.Min(9, precision)];
			}
			long integer = (long)valueD;
			this.AddIntegerToInternalTextBackingArray((double)integer, padding, ref writeIndex);
			if (precision > 0)
			{
				valueD -= integer;
				if (valueD != 0m)
				{
					int num = writeIndex;
					writeIndex = num + 1;
					this.m_TextBackingArray[num] = 46U;
					for (int p = 0; p < precision; p++)
					{
						valueD *= 10m;
						long d = (long)valueD;
						num = writeIndex;
						writeIndex = num + 1;
						this.m_TextBackingArray[num] = (uint)((ushort)(d + 48L));
						valueD -= d;
						if (valueD == 0m)
						{
							p = precision;
						}
					}
				}
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000184E0 File Offset: 0x000166E0
		private void AddIntegerToInternalTextBackingArray(double number, int padding, ref int writeIndex)
		{
			int integralCount = 0;
			int i = writeIndex;
			do
			{
				this.m_TextBackingArray[i++] = (uint)((ushort)(number % 10.0 + 48.0));
				number /= 10.0;
				integralCount++;
			}
			while (number > 0.999999999999999 || integralCount < padding);
			int lastIndex = i;
			while (writeIndex + 1 < i)
			{
				i--;
				uint t = this.m_TextBackingArray[writeIndex];
				this.m_TextBackingArray[writeIndex] = this.m_TextBackingArray[i];
				this.m_TextBackingArray[i] = t;
				writeIndex++;
			}
			writeIndex = lastIndex;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00018588 File Offset: 0x00016788
		private string InternalTextBackingArrayToString()
		{
			char[] array = new char[this.m_TextBackingArray.Count];
			for (int i = 0; i < this.m_TextBackingArray.Capacity; i++)
			{
				char c = (char)this.m_TextBackingArray[i];
				if (c == '\0')
				{
					break;
				}
				array[i] = c;
			}
			this.m_IsTextBackingStringDirty = false;
			return new string(array);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000185DE File Offset: 0x000167DE
		internal virtual int SetArraySizes(TMP_Text.TextProcessingElement[] unicodeChars)
		{
			return 0;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000185E4 File Offset: 0x000167E4
		public Vector2 GetPreferredValues()
		{
			this.m_isPreferredWidthDirty = true;
			float preferredWidth = this.GetPreferredWidth();
			this.m_isPreferredHeightDirty = true;
			float preferredHeight = this.GetPreferredHeight();
			this.m_isPreferredWidthDirty = true;
			this.m_isPreferredHeightDirty = true;
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00018620 File Offset: 0x00016820
		public Vector2 GetPreferredValues(float width, float height)
		{
			this.m_isCalculatingPreferredValues = true;
			this.ParseInputText();
			Vector2 margin = new Vector2(width, height);
			float preferredWidth = this.GetPreferredWidth(margin);
			float preferredHeight = this.GetPreferredHeight(margin);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00018658 File Offset: 0x00016858
		public Vector2 GetPreferredValues(string text)
		{
			this.m_isCalculatingPreferredValues = true;
			this.SetTextInternal(text);
			this.SetArraySizes(this.m_TextProcessingArray);
			Vector2 margin = TMP_Text.k_LargePositiveVector2;
			float preferredWidth = this.GetPreferredWidth(margin);
			float preferredHeight = this.GetPreferredHeight(margin);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001869C File Offset: 0x0001689C
		public Vector2 GetPreferredValues(string text, float width, float height)
		{
			this.m_isCalculatingPreferredValues = true;
			this.SetTextInternal(text);
			this.SetArraySizes(this.m_TextProcessingArray);
			Vector2 margin = new Vector2(width, height);
			float preferredWidth = this.GetPreferredWidth(margin, this.m_TextWrappingMode);
			float preferredHeight = this.GetPreferredHeight(margin);
			return new Vector2(preferredWidth, preferredHeight);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000186E8 File Offset: 0x000168E8
		protected float GetPreferredWidth()
		{
			if (TMP_Settings.instance == null)
			{
				return 0f;
			}
			if (!this.m_isPreferredWidthDirty)
			{
				return this.m_preferredWidth;
			}
			float fontSize = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			Vector2 margin = TMP_Text.k_LargePositiveVector2;
			this.m_isCalculatingPreferredValues = true;
			this.ParseInputText();
			this.m_AutoSizeIterationCount = 0;
			TextWrappingModes wrapMode = ((this.m_TextWrappingMode == TextWrappingModes.Normal || this.m_TextWrappingMode == TextWrappingModes.NoWrap) ? TextWrappingModes.NoWrap : TextWrappingModes.PreserveWhitespaceNoWrap);
			float x = this.CalculatePreferredValues(ref fontSize, margin, false, wrapMode).x;
			this.m_isPreferredWidthDirty = false;
			return x;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00018798 File Offset: 0x00016998
		private float GetPreferredWidth(Vector2 margin)
		{
			float fontSize = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			this.m_AutoSizeIterationCount = 0;
			TextWrappingModes wrapMode = ((this.m_TextWrappingMode == TextWrappingModes.Normal || this.m_TextWrappingMode == TextWrappingModes.NoWrap) ? TextWrappingModes.NoWrap : TextWrappingModes.PreserveWhitespaceNoWrap);
			return this.CalculatePreferredValues(ref fontSize, margin, false, wrapMode).x;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001880C File Offset: 0x00016A0C
		private float GetPreferredWidth(Vector2 margin, TextWrappingModes wrapMode)
		{
			float fontSize = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			this.m_AutoSizeIterationCount = 0;
			return this.CalculatePreferredValues(ref fontSize, margin, false, wrapMode).x;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001886C File Offset: 0x00016A6C
		protected float GetPreferredHeight()
		{
			if (TMP_Settings.instance == null)
			{
				return 0f;
			}
			if (!this.m_isPreferredHeightDirty)
			{
				return this.m_preferredHeight;
			}
			float fontSize = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			Vector2 margin = new Vector2((this.m_marginWidth != 0f) ? this.m_marginWidth : TMP_Text.k_LargePositiveFloat, TMP_Text.k_LargePositiveFloat);
			this.m_isCalculatingPreferredValues = true;
			this.ParseInputText();
			this.m_IsAutoSizePointSizeSet = false;
			this.m_AutoSizeIterationCount = 0;
			float preferredHeight = 0f;
			while (!this.m_IsAutoSizePointSizeSet)
			{
				preferredHeight = this.CalculatePreferredValues(ref fontSize, margin, this.m_enableAutoSizing, this.m_TextWrappingMode).y;
				this.m_AutoSizeIterationCount++;
			}
			this.m_isPreferredHeightDirty = false;
			return preferredHeight;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00018958 File Offset: 0x00016B58
		private float GetPreferredHeight(Vector2 margin)
		{
			float fontSize = (this.m_enableAutoSizing ? this.m_fontSizeMax : this.m_fontSize);
			this.m_minFontSize = this.m_fontSizeMin;
			this.m_maxFontSize = this.m_fontSizeMax;
			this.m_charWidthAdjDelta = 0f;
			this.m_IsAutoSizePointSizeSet = false;
			this.m_AutoSizeIterationCount = 0;
			float preferredHeight = 0f;
			while (!this.m_IsAutoSizePointSizeSet)
			{
				preferredHeight = this.CalculatePreferredValues(ref fontSize, margin, this.m_enableAutoSizing, this.m_TextWrappingMode).y;
				this.m_AutoSizeIterationCount++;
			}
			return preferredHeight;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000189E8 File Offset: 0x00016BE8
		public Vector2 GetRenderedValues()
		{
			return this.GetTextBounds().size;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00018A08 File Offset: 0x00016C08
		public Vector2 GetRenderedValues(bool onlyVisibleCharacters)
		{
			return this.GetTextBounds(onlyVisibleCharacters).size;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018A29 File Offset: 0x00016C29
		private float GetRenderedWidth()
		{
			return this.GetRenderedValues().x;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00018A36 File Offset: 0x00016C36
		protected float GetRenderedWidth(bool onlyVisibleCharacters)
		{
			return this.GetRenderedValues(onlyVisibleCharacters).x;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00018A44 File Offset: 0x00016C44
		private float GetRenderedHeight()
		{
			return this.GetRenderedValues().y;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018A51 File Offset: 0x00016C51
		protected float GetRenderedHeight(bool onlyVisibleCharacters)
		{
			return this.GetRenderedValues(onlyVisibleCharacters).y;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018A60 File Offset: 0x00016C60
		protected virtual Vector2 CalculatePreferredValues(ref float fontSize, Vector2 marginSize, bool isTextAutoSizingEnabled, TextWrappingModes textWrapMode)
		{
			if (this.m_fontAsset == null || this.m_fontAsset.characterLookupTable == null)
			{
				global::UnityEngine.Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + base.GetInstanceID().ToString());
				this.m_IsAutoSizePointSizeSet = true;
				return Vector2.zero;
			}
			if (this.m_TextProcessingArray == null || this.m_TextProcessingArray.Length == 0 || this.m_TextProcessingArray[0].unicode == 0U)
			{
				this.m_IsAutoSizePointSizeSet = true;
				return Vector2.zero;
			}
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(0, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			int totalCharacterCount = this.m_totalCharacterCount;
			if (this.m_internalCharacterInfo == null || totalCharacterCount > this.m_internalCharacterInfo.Length)
			{
				this.m_internalCharacterInfo = new TMP_CharacterInfo[(totalCharacterCount > 1024) ? (totalCharacterCount + 256) : Mathf.NextPowerOfTwo(totalCharacterCount)];
			}
			float baseScale = fontSize / this.m_fontAsset.faceInfo.pointSize * this.m_fontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
			float currentElementScale = baseScale;
			float currentEmScale = fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
			this.m_fontScaleMultiplier = 1f;
			this.m_currentFontSize = fontSize;
			this.m_sizeStack.SetDefault(this.m_currentFontSize);
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_lineJustification = this.m_HorizontalAlignment;
			this.m_lineJustificationStack.SetDefault(this.m_lineJustification);
			this.m_baselineOffset = 0f;
			this.m_baselineOffsetStack.Clear();
			this.m_FXScale = Vector3.one;
			this.m_lineOffset = 0f;
			this.m_lineHeight = -32767f;
			float lineGap = this.m_currentFontAsset.faceInfo.lineHeight - (this.m_currentFontAsset.faceInfo.ascentLine - this.m_currentFontAsset.faceInfo.descentLine);
			this.m_cSpacing = 0f;
			this.m_monoSpacing = 0f;
			this.m_xAdvance = 0f;
			this.tag_LineIndent = 0f;
			this.tag_Indent = 0f;
			this.m_indentStack.SetDefault(0f);
			this.tag_NoParsing = false;
			this.m_characterCount = 0;
			this.m_firstCharacterOfLine = 0;
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_lineNumber = 0;
			this.m_startOfLineAscender = 0f;
			this.m_IsDrivenLineSpacing = false;
			this.m_LastBaseGlyphIndex = int.MinValue;
			float marginWidth = marginSize.x;
			this.m_marginLeft = 0f;
			this.m_marginRight = 0f;
			this.m_width = -1f;
			float widthOfTextArea = marginWidth + 0.0001f - this.m_marginLeft - this.m_marginRight;
			this.m_RenderedWidth = 0f;
			this.m_RenderedHeight = 0f;
			this.m_isCalculatingPreferredValues = true;
			this.m_maxCapHeight = 0f;
			this.m_maxTextAscender = 0f;
			this.m_ElementDescender = 0f;
			bool isMaxVisibleDescenderSet = false;
			bool isFirstWordOfLine = true;
			this.m_isNonBreakingSpace = false;
			bool ignoreNonBreakingSpace = false;
			TMP_Text.CharacterSubstitution characterToSubstitute = new TMP_Text.CharacterSubstitution(-1, 0U);
			bool isSoftHyphenIgnored = false;
			WordWrapState internalWordWrapState = default(WordWrapState);
			WordWrapState internalLineState = default(WordWrapState);
			WordWrapState internalSoftLineBreak = default(WordWrapState);
			this.m_AutoSizeIterationCount++;
			int i = 0;
			while (i < this.m_TextProcessingArray.Length && this.m_TextProcessingArray[i].unicode != 0U)
			{
				uint charCode = this.m_TextProcessingArray[i].unicode;
				if (charCode != 26U)
				{
					if (this.m_isRichText && charCode == 60U)
					{
						this.m_isTextLayoutPhase = true;
						this.m_textElementType = TMP_TextElementType.Character;
						int endTagIndex;
						if (this.ValidateHtmlTag(this.m_TextProcessingArray, i + 1, out endTagIndex))
						{
							i = endTagIndex;
							if (this.m_textElementType == TMP_TextElementType.Character)
							{
								goto IL_1DFE;
							}
						}
					}
					else
					{
						this.m_textElementType = this.m_textInfo.characterInfo[this.m_characterCount].elementType;
						this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
						this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					}
					int prev_MaterialIndex = this.m_currentMaterialIndex;
					bool isUsingAltTypeface = this.m_textInfo.characterInfo[this.m_characterCount].isUsingAlternateTypeface;
					this.m_isTextLayoutPhase = false;
					bool isInjectedCharacter = false;
					if (characterToSubstitute.index == this.m_characterCount)
					{
						charCode = characterToSubstitute.unicode;
						this.m_textElementType = TMP_TextElementType.Character;
						isInjectedCharacter = true;
						if (charCode != 3U)
						{
							if (charCode != 45U)
							{
								if (charCode == 8230U)
								{
									this.m_internalCharacterInfo[this.m_characterCount].textElement = this.m_Ellipsis.character;
									this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
									this.m_internalCharacterInfo[this.m_characterCount].fontAsset = this.m_Ellipsis.fontAsset;
									this.m_internalCharacterInfo[this.m_characterCount].material = this.m_Ellipsis.material;
									this.m_internalCharacterInfo[this.m_characterCount].materialReferenceIndex = this.m_Ellipsis.materialIndex;
									this.m_isTextTruncated = true;
									characterToSubstitute.index = this.m_characterCount + 1;
									characterToSubstitute.unicode = 3U;
								}
							}
						}
						else
						{
							this.m_internalCharacterInfo[this.m_characterCount].textElement = this.m_currentFontAsset.characterLookupTable[3U];
							this.m_isTextTruncated = true;
						}
					}
					if (this.m_characterCount < this.m_firstVisibleCharacter && charCode != 3U)
					{
						this.m_internalCharacterInfo[this.m_characterCount].isVisible = false;
						this.m_internalCharacterInfo[this.m_characterCount].character = '\u200b';
						this.m_internalCharacterInfo[this.m_characterCount].lineNumber = 0;
						this.m_characterCount++;
					}
					else
					{
						float smallCapsMultiplier = 1f;
						if (this.m_textElementType == TMP_TextElementType.Character)
						{
							if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
							{
								if (char.IsLower((char)charCode))
								{
									charCode = (uint)char.ToUpper((char)charCode);
								}
							}
							else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
							{
								if (char.IsUpper((char)charCode))
								{
									charCode = (uint)char.ToLower((char)charCode);
								}
							}
							else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)charCode))
							{
								smallCapsMultiplier = 0.8f;
								charCode = (uint)char.ToUpper((char)charCode);
							}
						}
						float baselineOffset = 0f;
						float elementAscentLine = 0f;
						float elementDescentLine = 0f;
						if (this.m_textElementType == TMP_TextElementType.Sprite)
						{
							TMP_SpriteCharacter sprite = (TMP_SpriteCharacter)this.m_textInfo.characterInfo[this.m_characterCount].textElement;
							this.m_currentSpriteAsset = sprite.textAsset as TMP_SpriteAsset;
							this.m_spriteIndex = (int)sprite.glyphIndex;
							if (sprite == null)
							{
								goto IL_1DFE;
							}
							if (charCode == 60U)
							{
								charCode = (uint)(57344 + this.m_spriteIndex);
							}
							if (this.m_currentSpriteAsset.faceInfo.pointSize > 0f)
							{
								float spriteScale = this.m_currentFontSize / this.m_currentSpriteAsset.faceInfo.pointSize * this.m_currentSpriteAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								currentElementScale = sprite.scale * sprite.glyph.scale * spriteScale;
								elementAscentLine = this.m_currentSpriteAsset.faceInfo.ascentLine;
								elementDescentLine = this.m_currentSpriteAsset.faceInfo.descentLine;
							}
							else
							{
								float spriteScale2 = this.m_currentFontSize / this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								currentElementScale = this.m_currentFontAsset.faceInfo.ascentLine / sprite.glyph.metrics.height * sprite.scale * sprite.glyph.scale * spriteScale2;
								float scaleDelta = spriteScale2 / currentElementScale;
								elementAscentLine = this.m_currentFontAsset.faceInfo.ascentLine * scaleDelta;
								elementDescentLine = this.m_currentFontAsset.faceInfo.descentLine * scaleDelta;
							}
							this.m_cached_TextElement = sprite;
							this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Sprite;
							this.m_internalCharacterInfo[this.m_characterCount].scale = currentElementScale;
							this.m_currentMaterialIndex = prev_MaterialIndex;
						}
						else if (this.m_textElementType == TMP_TextElementType.Character)
						{
							this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].textElement;
							if (this.m_cached_TextElement == null)
							{
								goto IL_1DFE;
							}
							this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
							float adjustedScale;
							if (isInjectedCharacter && this.m_TextProcessingArray[i].unicode == 10U && this.m_characterCount != this.m_firstCharacterOfLine)
							{
								adjustedScale = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize * smallCapsMultiplier / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							}
							else
							{
								adjustedScale = this.m_currentFontSize * smallCapsMultiplier / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							}
							if (isInjectedCharacter && charCode == 8230U)
							{
								elementAscentLine = 0f;
								elementDescentLine = 0f;
							}
							else
							{
								elementAscentLine = this.m_currentFontAsset.m_FaceInfo.ascentLine;
								elementDescentLine = this.m_currentFontAsset.m_FaceInfo.descentLine;
							}
							currentElementScale = adjustedScale * this.m_fontScaleMultiplier * this.m_cached_TextElement.scale;
							this.m_internalCharacterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
						}
						float currentElementUnmodifiedScale = currentElementScale;
						if (charCode == 173U || charCode == 3U)
						{
							currentElementScale = 0f;
						}
						this.m_internalCharacterInfo[this.m_characterCount].character = (char)charCode;
						Glyph altGlyph = this.m_textInfo.characterInfo[this.m_characterCount].alternativeGlyph;
						GlyphMetrics currentGlyphMetrics = ((altGlyph == null) ? this.m_cached_TextElement.m_Glyph.metrics : altGlyph.metrics);
						bool isWhiteSpace = charCode <= 65535U && char.IsWhiteSpace((char)charCode);
						GlyphValueRecord glyphAdjustments = default(GlyphValueRecord);
						float characterSpacingAdjustment = this.m_characterSpacing;
						if (this.m_enableKerning && this.m_textElementType == TMP_TextElementType.Character)
						{
							uint baseGlyphIndex = this.m_cached_TextElement.m_GlyphIndex;
							if (this.m_characterCount < totalCharacterCount - 1 && this.m_textInfo.characterInfo[this.m_characterCount + 1].elementType == TMP_TextElementType.Character)
							{
								uint key = (this.m_textInfo.characterInfo[this.m_characterCount + 1].textElement.m_GlyphIndex << 16) | baseGlyphIndex;
								GlyphPairAdjustmentRecord adjustmentPair;
								if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out adjustmentPair))
								{
									glyphAdjustments = adjustmentPair.firstAdjustmentRecord.glyphValueRecord;
									characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
								}
							}
							if (this.m_characterCount >= 1)
							{
								uint previousGlyphIndex = this.m_textInfo.characterInfo[this.m_characterCount - 1].textElement.m_GlyphIndex;
								uint key2 = (baseGlyphIndex << 16) | previousGlyphIndex;
								GlyphPairAdjustmentRecord adjustmentPair;
								if (this.textInfo.characterInfo[this.m_characterCount - 1].elementType == TMP_TextElementType.Character && this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out adjustmentPair))
								{
									glyphAdjustments += adjustmentPair.secondAdjustmentRecord.glyphValueRecord;
									characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
								}
							}
							this.m_internalCharacterInfo[this.m_characterCount].adjustedHorizontalAdvance = glyphAdjustments.xAdvance;
						}
						bool isBaseGlyph = TMP_TextParsingUtilities.IsBaseGlyph(charCode);
						if (isBaseGlyph)
						{
							this.m_LastBaseGlyphIndex = this.m_characterCount;
						}
						if (this.m_characterCount > 0 && !isBaseGlyph)
						{
							if (this.m_LastBaseGlyphIndex != -2147483648 && this.m_LastBaseGlyphIndex == this.m_characterCount - 1)
							{
								uint baseGlyphIndex2 = this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].textElement.glyph.index;
								uint key3 = (this.m_cached_TextElement.glyphIndex << 16) | baseGlyphIndex2;
								MarkToBaseAdjustmentRecord glyphAdjustmentRecord;
								if (this.m_currentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key3, out glyphAdjustmentRecord))
								{
									float advanceOffset = (this.m_internalCharacterInfo[this.m_LastBaseGlyphIndex].origin - this.m_xAdvance) / currentElementScale;
									glyphAdjustments.xPlacement = advanceOffset + glyphAdjustmentRecord.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord.markPositionAdjustment.xPositionAdjustment;
									glyphAdjustments.yPlacement = glyphAdjustmentRecord.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord.markPositionAdjustment.yPositionAdjustment;
									characterSpacingAdjustment = 0f;
								}
							}
							else
							{
								bool wasLookupApplied = false;
								int characterLookupIndex = this.m_characterCount - 1;
								while (characterLookupIndex >= 0 && characterLookupIndex != this.m_LastBaseGlyphIndex)
								{
									uint baseGlyphIndex3 = this.m_textInfo.characterInfo[characterLookupIndex].textElement.glyph.index;
									uint key4 = (this.m_cached_TextElement.glyphIndex << 16) | baseGlyphIndex3;
									MarkToMarkAdjustmentRecord glyphAdjustmentRecord2;
									if (this.m_currentFontAsset.fontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryGetValue(key4, out glyphAdjustmentRecord2))
									{
										float baseMarkOrigin = (this.m_textInfo.characterInfo[characterLookupIndex].origin - this.m_xAdvance) / currentElementScale;
										float currentBaseline = baselineOffset - this.m_lineOffset + this.m_baselineOffset;
										float baseMarkBaseline = (this.m_internalCharacterInfo[characterLookupIndex].baseLine - currentBaseline) / currentElementScale;
										glyphAdjustments.xPlacement = baseMarkOrigin + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.xPositionAdjustment;
										glyphAdjustments.yPlacement = baseMarkBaseline + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.yPositionAdjustment;
										characterSpacingAdjustment = 0f;
										wasLookupApplied = true;
										break;
									}
									characterLookupIndex--;
								}
								if (this.m_LastBaseGlyphIndex != -2147483648 && !wasLookupApplied)
								{
									uint baseGlyphIndex4 = this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].textElement.glyph.index;
									uint key5 = (this.m_cached_TextElement.glyphIndex << 16) | baseGlyphIndex4;
									MarkToBaseAdjustmentRecord glyphAdjustmentRecord3;
									if (this.m_currentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key5, out glyphAdjustmentRecord3))
									{
										float advanceOffset2 = (this.m_internalCharacterInfo[this.m_LastBaseGlyphIndex].origin - this.m_xAdvance) / currentElementScale;
										glyphAdjustments.xPlacement = advanceOffset2 + glyphAdjustmentRecord3.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.xPositionAdjustment;
										glyphAdjustments.yPlacement = glyphAdjustmentRecord3.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.yPositionAdjustment;
										characterSpacingAdjustment = 0f;
									}
								}
							}
						}
						elementAscentLine += glyphAdjustments.yPlacement;
						elementDescentLine += glyphAdjustments.yPlacement;
						float monoAdvance = 0f;
						if (this.m_monoSpacing != 0f)
						{
							monoAdvance = (this.m_monoSpacing / 2f - (this.m_cached_TextElement.glyph.metrics.width / 2f + this.m_cached_TextElement.glyph.metrics.horizontalBearingX) * currentElementScale) * (1f - this.m_charWidthAdjDelta);
							this.m_xAdvance += monoAdvance;
						}
						float boldSpacingAdjustment = 0f;
						if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAltTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
						{
							boldSpacingAdjustment = this.m_currentFontAsset.boldSpacing;
						}
						this.m_internalCharacterInfo[this.m_characterCount].origin = this.m_xAdvance + glyphAdjustments.xPlacement * currentElementScale;
						this.m_internalCharacterInfo[this.m_characterCount].baseLine = baselineOffset - this.m_lineOffset + this.m_baselineOffset + glyphAdjustments.yPlacement * currentElementScale;
						float elementAscender = ((this.m_textElementType == TMP_TextElementType.Character) ? (elementAscentLine * currentElementScale / smallCapsMultiplier + this.m_baselineOffset) : (elementAscentLine * currentElementScale + this.m_baselineOffset));
						float elementDescender = ((this.m_textElementType == TMP_TextElementType.Character) ? (elementDescentLine * currentElementScale / smallCapsMultiplier + this.m_baselineOffset) : (elementDescentLine * currentElementScale + this.m_baselineOffset));
						float adjustedAscender = elementAscender;
						float adjustedDescender = elementDescender;
						bool isFirstCharacterOfLine = this.m_characterCount == this.m_firstCharacterOfLine;
						if (isFirstCharacterOfLine || !isWhiteSpace)
						{
							if (this.m_baselineOffset != 0f)
							{
								adjustedAscender = Mathf.Max((elementAscender - this.m_baselineOffset) / this.m_fontScaleMultiplier, adjustedAscender);
								adjustedDescender = Mathf.Min((elementDescender - this.m_baselineOffset) / this.m_fontScaleMultiplier, adjustedDescender);
							}
							this.m_maxLineAscender = Mathf.Max(adjustedAscender, this.m_maxLineAscender);
							this.m_maxLineDescender = Mathf.Min(adjustedDescender, this.m_maxLineDescender);
						}
						if (isFirstCharacterOfLine || !isWhiteSpace)
						{
							this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender = adjustedAscender;
							this.m_internalCharacterInfo[this.m_characterCount].adjustedDescender = adjustedDescender;
							this.m_ElementAscender = (this.m_internalCharacterInfo[this.m_characterCount].ascender = elementAscender - this.m_lineOffset);
							this.m_ElementDescender = (this.m_internalCharacterInfo[this.m_characterCount].descender = elementDescender - this.m_lineOffset);
						}
						else
						{
							this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender = this.m_maxLineAscender;
							this.m_internalCharacterInfo[this.m_characterCount].adjustedDescender = this.m_maxLineDescender;
							this.m_ElementAscender = (this.m_internalCharacterInfo[this.m_characterCount].ascender = this.m_maxLineAscender - this.m_lineOffset);
							this.m_ElementDescender = (this.m_internalCharacterInfo[this.m_characterCount].descender = this.m_maxLineDescender - this.m_lineOffset);
						}
						if ((this.m_lineNumber == 0 || this.m_isNewPage) && (isFirstCharacterOfLine || !isWhiteSpace))
						{
							this.m_maxTextAscender = this.m_maxLineAscender;
							this.m_maxCapHeight = Mathf.Max(this.m_maxCapHeight, this.m_currentFontAsset.m_FaceInfo.capLine * currentElementScale / smallCapsMultiplier);
						}
						if (this.m_lineOffset == 0f && (!isWhiteSpace || this.m_characterCount == this.m_firstCharacterOfLine))
						{
							this.m_PageAscender = ((this.m_PageAscender > elementAscender) ? this.m_PageAscender : elementAscender);
						}
						bool isJustifiedOrFlush = (this.m_lineJustification & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush || (this.m_lineJustification & HorizontalAlignmentOptions.Justified) == HorizontalAlignmentOptions.Justified;
						if (charCode == 9U || ((textWrapMode == TextWrappingModes.PreserveWhitespace || textWrapMode == TextWrappingModes.PreserveWhitespaceNoWrap) && (isWhiteSpace || charCode == 8203U)) || (!isWhiteSpace && charCode != 8203U && charCode != 173U && charCode != 3U) || (charCode == 173U && !isSoftHyphenIgnored) || this.m_textElementType == TMP_TextElementType.Sprite)
						{
							widthOfTextArea = ((this.m_width != -1f) ? Mathf.Min(marginWidth + 0.0001f - this.m_marginLeft - this.m_marginRight, this.m_width) : (marginWidth + 0.0001f - this.m_marginLeft - this.m_marginRight));
							float textWidth = Mathf.Abs(this.m_xAdvance) + currentGlyphMetrics.horizontalAdvance * (1f - this.m_charWidthAdjDelta) * ((charCode == 173U) ? currentElementUnmodifiedScale : currentElementScale);
							int characterCount = this.m_characterCount;
							if (isBaseGlyph && textWidth > widthOfTextArea * (isJustifiedOrFlush ? 1.05f : 1f) && textWrapMode != TextWrappingModes.NoWrap && textWrapMode != TextWrappingModes.PreserveWhitespaceNoWrap && this.m_characterCount != this.m_firstCharacterOfLine)
							{
								i = this.RestoreWordWrappingState(ref internalWordWrapState);
								if (this.m_internalCharacterInfo[this.m_characterCount - 1].character == '\u00ad' && !isSoftHyphenIgnored && this.m_overflowMode == TextOverflowModes.Overflow)
								{
									characterToSubstitute.index = this.m_characterCount - 1;
									characterToSubstitute.unicode = 45U;
									i--;
									this.m_characterCount--;
									goto IL_1DFE;
								}
								isSoftHyphenIgnored = false;
								if (this.m_internalCharacterInfo[this.m_characterCount].character == '\u00ad')
								{
									isSoftHyphenIgnored = true;
									goto IL_1DFE;
								}
								if (isTextAutoSizingEnabled && isFirstWordOfLine)
								{
									if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										float adjustedTextWidth = textWidth;
										if (this.m_charWidthAdjDelta > 0f)
										{
											adjustedTextWidth /= 1f - this.m_charWidthAdjDelta;
										}
										float adjustmentDelta = textWidth - (widthOfTextArea - 0.0001f) * (isJustifiedOrFlush ? 1.05f : 1f);
										this.m_charWidthAdjDelta += adjustmentDelta / adjustedTextWidth;
										this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
										return Vector2.zero;
									}
									if (fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										this.m_maxFontSize = fontSize;
										float sizeDelta = Mathf.Max((fontSize - this.m_minFontSize) / 2f, 0.05f);
										fontSize -= sizeDelta;
										fontSize = Mathf.Max((float)((int)(fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
										return Vector2.zero;
									}
								}
								float baselineAdjustmentDelta = this.m_maxLineAscender - this.m_startOfLineAscender;
								if (this.m_lineOffset > 0f && Math.Abs(baselineAdjustmentDelta) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
								{
									this.m_ElementDescender -= baselineAdjustmentDelta;
									this.m_lineOffset += baselineAdjustmentDelta;
								}
								float maxLineAscender = this.m_maxLineAscender;
								float lineOffset = this.m_lineOffset;
								float lineDescender = this.m_maxLineDescender - this.m_lineOffset;
								this.m_ElementDescender = ((this.m_ElementDescender < lineDescender) ? this.m_ElementDescender : lineDescender);
								if (!isMaxVisibleDescenderSet)
								{
									float elementDescender2 = this.m_ElementDescender;
								}
								if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
								{
									isMaxVisibleDescenderSet = true;
								}
								this.m_firstCharacterOfLine = this.m_characterCount;
								this.m_lineVisibleCharacterCount = 0;
								this.SaveWordWrappingState(ref internalLineState, i, this.m_characterCount - 1);
								this.m_lineNumber++;
								float ascender = this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender;
								if (this.m_lineHeight == -32767f)
								{
									this.m_lineOffset += 0f - this.m_maxLineDescender + ascender + (lineGap + this.m_lineSpacingDelta) * baseScale + this.m_lineSpacing * currentEmScale;
									this.m_IsDrivenLineSpacing = false;
								}
								else
								{
									this.m_lineOffset += this.m_lineHeight + this.m_lineSpacing * currentEmScale;
									this.m_IsDrivenLineSpacing = true;
								}
								this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
								this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
								this.m_startOfLineAscender = ascender;
								this.m_xAdvance = 0f + this.tag_Indent;
								isFirstWordOfLine = true;
								goto IL_1DFE;
							}
							else
							{
								this.m_RenderedWidth = Mathf.Max(this.m_RenderedWidth, textWidth + this.m_marginLeft + this.m_marginRight);
								this.m_RenderedHeight = Mathf.Max(this.m_RenderedHeight, this.m_maxTextAscender - this.m_ElementDescender);
							}
						}
						if (this.m_lineOffset > 0f && !TMP_Math.Approximately(this.m_maxLineAscender, this.m_startOfLineAscender) && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
						{
							float offsetDelta = this.m_maxLineAscender - this.m_startOfLineAscender;
							this.m_ElementDescender -= offsetDelta;
							this.m_lineOffset += offsetDelta;
							this.m_startOfLineAscender += offsetDelta;
							internalWordWrapState.lineOffset = this.m_lineOffset;
							internalWordWrapState.startOfLineAscender = this.m_startOfLineAscender;
						}
						if (charCode == 9U)
						{
							float tabSize = this.m_currentFontAsset.faceInfo.tabWidth * (float)this.m_currentFontAsset.tabSize * currentElementScale;
							float tabs = Mathf.Ceil(this.m_xAdvance / tabSize) * tabSize;
							this.m_xAdvance = ((tabs > this.m_xAdvance) ? tabs : (this.m_xAdvance + tabSize));
						}
						else if (this.m_monoSpacing != 0f)
						{
							this.m_xAdvance += (this.m_monoSpacing - monoAdvance + (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment) * currentEmScale + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (isWhiteSpace || charCode == 8203U)
							{
								this.m_xAdvance += this.m_wordSpacing * currentEmScale;
							}
						}
						else
						{
							this.m_xAdvance += ((currentGlyphMetrics.horizontalAdvance * this.m_FXScale.x + glyphAdjustments.xAdvance) * currentElementScale + (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (isWhiteSpace || charCode == 8203U)
							{
								this.m_xAdvance += this.m_wordSpacing * currentEmScale;
							}
						}
						if (charCode == 13U)
						{
							this.m_xAdvance = 0f + this.tag_Indent;
						}
						if (charCode == 10U || charCode == 11U || charCode == 3U || charCode == 8232U || charCode == 8233U || this.m_characterCount == totalCharacterCount - 1)
						{
							float baselineAdjustmentDelta2 = this.m_maxLineAscender - this.m_startOfLineAscender;
							if (this.m_lineOffset > 0f && Math.Abs(baselineAdjustmentDelta2) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
							{
								this.m_ElementDescender -= baselineAdjustmentDelta2;
								this.m_lineOffset += baselineAdjustmentDelta2;
							}
							this.m_isNewPage = false;
							float lineDescender2 = this.m_maxLineDescender - this.m_lineOffset;
							this.m_ElementDescender = ((this.m_ElementDescender < lineDescender2) ? this.m_ElementDescender : lineDescender2);
							if (charCode == 10U || charCode == 11U || (charCode == 45U && isInjectedCharacter) || charCode == 8232U || charCode == 8233U)
							{
								this.SaveWordWrappingState(ref internalLineState, i, this.m_characterCount);
								this.SaveWordWrappingState(ref internalWordWrapState, i, this.m_characterCount);
								this.m_lineNumber++;
								this.m_firstCharacterOfLine = this.m_characterCount + 1;
								float ascender2 = this.m_internalCharacterInfo[this.m_characterCount].adjustedAscender;
								if (this.m_lineHeight == -32767f)
								{
									float lineOffsetDelta = 0f - this.m_maxLineDescender + ascender2 + (lineGap + this.m_lineSpacingDelta) * baseScale + (this.m_lineSpacing + ((charCode == 10U || charCode == 8233U) ? this.m_paragraphSpacing : 0f)) * currentEmScale;
									this.m_lineOffset += lineOffsetDelta;
									this.m_IsDrivenLineSpacing = false;
								}
								else
								{
									this.m_lineOffset += this.m_lineHeight + (this.m_lineSpacing + ((charCode == 10U || charCode == 8233U) ? this.m_paragraphSpacing : 0f)) * currentEmScale;
									this.m_IsDrivenLineSpacing = true;
								}
								this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
								this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
								this.m_startOfLineAscender = ascender2;
								this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
								this.m_characterCount++;
								goto IL_1DFE;
							}
							if (charCode == 3U)
							{
								i = this.m_TextProcessingArray.Length;
							}
						}
						if ((textWrapMode != TextWrappingModes.NoWrap && textWrapMode != TextWrappingModes.PreserveWhitespaceNoWrap) || this.m_overflowMode == TextOverflowModes.Truncate || this.m_overflowMode == TextOverflowModes.Ellipsis)
						{
							bool shouldSaveHardLineBreak = false;
							bool shouldSaveSoftLineBreak = false;
							if ((isWhiteSpace || charCode == 8203U || charCode == 45U || charCode == 173U) && (!this.m_isNonBreakingSpace || ignoreNonBreakingSpace) && charCode != 160U && charCode != 8199U && charCode != 8209U && charCode != 8239U && charCode != 8288U)
							{
								if (charCode != 45U || this.m_characterCount <= 0 || !char.IsWhiteSpace(this.m_textInfo.characterInfo[this.m_characterCount - 1].character))
								{
									isFirstWordOfLine = false;
									shouldSaveHardLineBreak = true;
									internalSoftLineBreak.previous_WordBreak = -1;
								}
							}
							else if (!this.m_isNonBreakingSpace && ((TMP_TextParsingUtilities.IsHangul(charCode) && !TMP_Settings.useModernHangulLineBreakingRules) || TMP_TextParsingUtilities.IsCJK(charCode)))
							{
								bool flag = TMP_Settings.linebreakingRules.leadingCharacters.Contains(charCode);
								bool isNextFollowingCharacter = this.m_characterCount < totalCharacterCount - 1 && TMP_Settings.linebreakingRules.followingCharacters.Contains((uint)this.m_internalCharacterInfo[this.m_characterCount + 1].character);
								if (!flag)
								{
									if (!isNextFollowingCharacter)
									{
										isFirstWordOfLine = false;
										shouldSaveHardLineBreak = true;
									}
									if (isFirstWordOfLine)
									{
										if (isWhiteSpace)
										{
											shouldSaveSoftLineBreak = true;
										}
										shouldSaveHardLineBreak = true;
									}
								}
								else if (isFirstWordOfLine && isFirstCharacterOfLine)
								{
									if (isWhiteSpace)
									{
										shouldSaveSoftLineBreak = true;
									}
									shouldSaveHardLineBreak = true;
								}
							}
							else if (!this.m_isNonBreakingSpace && this.m_characterCount + 1 < totalCharacterCount && TMP_TextParsingUtilities.IsCJK((uint)this.m_textInfo.characterInfo[this.m_characterCount + 1].character))
							{
								shouldSaveHardLineBreak = true;
							}
							else if (isFirstWordOfLine)
							{
								if ((isWhiteSpace && charCode != 160U) || (charCode == 173U && !isSoftHyphenIgnored))
								{
									shouldSaveSoftLineBreak = true;
								}
								shouldSaveHardLineBreak = true;
							}
							if (shouldSaveHardLineBreak)
							{
								this.SaveWordWrappingState(ref internalWordWrapState, i, this.m_characterCount);
							}
							if (shouldSaveSoftLineBreak)
							{
								this.SaveWordWrappingState(ref internalSoftLineBreak, i, this.m_characterCount);
							}
						}
						this.m_characterCount++;
					}
				}
				IL_1DFE:
				i++;
			}
			float fontSizeDelta = this.m_maxFontSize - this.m_minFontSize;
			if (isTextAutoSizingEnabled && fontSizeDelta > 0.051f && fontSize < this.m_fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
			{
				if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
				{
					this.m_charWidthAdjDelta = 0f;
				}
				this.m_minFontSize = fontSize;
				float sizeDelta2 = Mathf.Max((this.m_maxFontSize - fontSize) / 2f, 0.05f);
				fontSize += sizeDelta2;
				fontSize = Mathf.Min((float)((int)(fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMax);
				return Vector2.zero;
			}
			this.m_IsAutoSizePointSizeSet = true;
			this.m_isCalculatingPreferredValues = false;
			this.m_RenderedWidth += ((this.m_margin.x > 0f) ? this.m_margin.x : 0f);
			this.m_RenderedWidth += ((this.m_margin.z > 0f) ? this.m_margin.z : 0f);
			this.m_RenderedHeight += ((this.m_margin.y > 0f) ? this.m_margin.y : 0f);
			this.m_RenderedHeight += ((this.m_margin.w > 0f) ? this.m_margin.w : 0f);
			this.m_RenderedWidth = (float)((int)(this.m_RenderedWidth * 100f + 1f)) / 100f;
			this.m_RenderedHeight = (float)((int)(this.m_RenderedHeight * 100f + 1f)) / 100f;
			return new Vector2(this.m_RenderedWidth, this.m_RenderedHeight);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001AA68 File Offset: 0x00018C68
		protected virtual Bounds GetCompoundBounds()
		{
			return default(Bounds);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001AA7E File Offset: 0x00018C7E
		internal virtual Rect GetCanvasSpaceClippingRect()
		{
			return Rect.zero;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001AA88 File Offset: 0x00018C88
		protected Bounds GetTextBounds()
		{
			if (this.m_textInfo == null || this.m_textInfo.characterCount > this.m_textInfo.characterInfo.Length)
			{
				return default(Bounds);
			}
			Extents extent = new Extents(TMP_Text.k_LargePositiveVector2, TMP_Text.k_LargeNegativeVector2);
			int i = 0;
			while (i < this.m_textInfo.characterCount && i < this.m_textInfo.characterInfo.Length)
			{
				if (this.m_textInfo.characterInfo[i].isVisible)
				{
					extent.min.x = Mathf.Min(extent.min.x, this.m_textInfo.characterInfo[i].origin);
					extent.min.y = Mathf.Min(extent.min.y, this.m_textInfo.characterInfo[i].descender);
					extent.max.x = Mathf.Max(extent.max.x, this.m_textInfo.characterInfo[i].xAdvance);
					extent.max.y = Mathf.Max(extent.max.y, this.m_textInfo.characterInfo[i].ascender);
				}
				i++;
			}
			Vector2 size;
			size.x = extent.max.x - extent.min.x;
			size.y = extent.max.y - extent.min.y;
			return new Bounds((extent.min + extent.max) / 2f, size);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001AC48 File Offset: 0x00018E48
		protected Bounds GetTextBounds(bool onlyVisibleCharacters)
		{
			if (this.m_textInfo == null)
			{
				return default(Bounds);
			}
			Extents extent = new Extents(TMP_Text.k_LargePositiveVector2, TMP_Text.k_LargeNegativeVector2);
			int i = 0;
			while (i < this.m_textInfo.characterCount && ((i <= this.maxVisibleCharacters && this.m_textInfo.characterInfo[i].lineNumber <= this.m_maxVisibleLines) || !onlyVisibleCharacters))
			{
				if (!onlyVisibleCharacters || this.m_textInfo.characterInfo[i].isVisible)
				{
					extent.min.x = Mathf.Min(extent.min.x, this.m_textInfo.characterInfo[i].origin);
					extent.min.y = Mathf.Min(extent.min.y, this.m_textInfo.characterInfo[i].descender);
					extent.max.x = Mathf.Max(extent.max.x, this.m_textInfo.characterInfo[i].xAdvance);
					extent.max.y = Mathf.Max(extent.max.y, this.m_textInfo.characterInfo[i].ascender);
				}
				i++;
			}
			Vector2 size;
			size.x = extent.max.x - extent.min.x;
			size.y = extent.max.y - extent.min.y;
			return new Bounds((extent.min + extent.max) / 2f, size);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001AE14 File Offset: 0x00019014
		protected void AdjustLineOffset(int startIndex, int endIndex, float offset)
		{
			Vector3 vertexOffset = new Vector3(0f, offset, 0f);
			for (int i = startIndex; i <= endIndex; i++)
			{
				TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
				int num = i;
				characterInfo[num].bottomLeft = characterInfo[num].bottomLeft - vertexOffset;
				TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
				int num2 = i;
				characterInfo2[num2].topLeft = characterInfo2[num2].topLeft - vertexOffset;
				TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
				int num3 = i;
				characterInfo3[num3].topRight = characterInfo3[num3].topRight - vertexOffset;
				TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
				int num4 = i;
				characterInfo4[num4].bottomRight = characterInfo4[num4].bottomRight - vertexOffset;
				TMP_CharacterInfo[] characterInfo5 = this.m_textInfo.characterInfo;
				int num5 = i;
				characterInfo5[num5].ascender = characterInfo5[num5].ascender - vertexOffset.y;
				TMP_CharacterInfo[] characterInfo6 = this.m_textInfo.characterInfo;
				int num6 = i;
				characterInfo6[num6].baseLine = characterInfo6[num6].baseLine - vertexOffset.y;
				TMP_CharacterInfo[] characterInfo7 = this.m_textInfo.characterInfo;
				int num7 = i;
				characterInfo7[num7].descender = characterInfo7[num7].descender - vertexOffset.y;
				if (this.m_textInfo.characterInfo[i].isVisible)
				{
					TMP_CharacterInfo[] characterInfo8 = this.m_textInfo.characterInfo;
					int num8 = i;
					characterInfo8[num8].vertex_BL.position = characterInfo8[num8].vertex_BL.position - vertexOffset;
					TMP_CharacterInfo[] characterInfo9 = this.m_textInfo.characterInfo;
					int num9 = i;
					characterInfo9[num9].vertex_TL.position = characterInfo9[num9].vertex_TL.position - vertexOffset;
					TMP_CharacterInfo[] characterInfo10 = this.m_textInfo.characterInfo;
					int num10 = i;
					characterInfo10[num10].vertex_TR.position = characterInfo10[num10].vertex_TR.position - vertexOffset;
					TMP_CharacterInfo[] characterInfo11 = this.m_textInfo.characterInfo;
					int num11 = i;
					characterInfo11[num11].vertex_BR.position = characterInfo11[num11].vertex_BR.position - vertexOffset;
				}
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001B00C File Offset: 0x0001920C
		protected void ResizeLineExtents(int size)
		{
			size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size + 1));
			TMP_LineInfo[] temp_lineInfo = new TMP_LineInfo[size];
			for (int i = 0; i < size; i++)
			{
				if (i < this.m_textInfo.lineInfo.Length)
				{
					temp_lineInfo[i] = this.m_textInfo.lineInfo[i];
				}
				else
				{
					temp_lineInfo[i].lineExtents.min = TMP_Text.k_LargePositiveVector2;
					temp_lineInfo[i].lineExtents.max = TMP_Text.k_LargeNegativeVector2;
					temp_lineInfo[i].ascender = TMP_Text.k_LargeNegativeFloat;
					temp_lineInfo[i].descender = TMP_Text.k_LargePositiveFloat;
				}
			}
			this.m_textInfo.lineInfo = temp_lineInfo;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016B3A File Offset: 0x00014D3A
		public virtual TMP_TextInfo GetTextInfo(string text)
		{
			return null;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void ComputeMarginSize()
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001B0CC File Offset: 0x000192CC
		internal void InsertNewLine(int i, float baseScale, float currentElementScale, float currentEmScale, float boldSpacingAdjustment, float characterSpacingAdjustment, float width, float lineGap, ref bool isMaxVisibleDescenderSet, ref float maxVisibleDescender)
		{
			float baselineAdjustmentDelta = this.m_maxLineAscender - this.m_startOfLineAscender;
			if (this.m_lineOffset > 0f && Math.Abs(baselineAdjustmentDelta) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
			{
				this.AdjustLineOffset(this.m_firstCharacterOfLine, this.m_characterCount, baselineAdjustmentDelta);
				this.m_ElementDescender -= baselineAdjustmentDelta;
				this.m_lineOffset += baselineAdjustmentDelta;
			}
			float lineAscender = this.m_maxLineAscender - this.m_lineOffset;
			float lineDescender = this.m_maxLineDescender - this.m_lineOffset;
			this.m_ElementDescender = ((this.m_ElementDescender < lineDescender) ? this.m_ElementDescender : lineDescender);
			if (!isMaxVisibleDescenderSet)
			{
				maxVisibleDescender = this.m_ElementDescender;
			}
			if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
			{
				isMaxVisibleDescenderSet = true;
			}
			this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex = this.m_firstCharacterOfLine;
			this.m_textInfo.lineInfo[this.m_lineNumber].firstVisibleCharacterIndex = (this.m_firstVisibleCharacterOfLine = ((this.m_firstCharacterOfLine > this.m_firstVisibleCharacterOfLine) ? this.m_firstCharacterOfLine : this.m_firstVisibleCharacterOfLine));
			this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex = (this.m_lastCharacterOfLine = ((this.m_characterCount - 1 > 0) ? (this.m_characterCount - 1) : 0));
			this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex = (this.m_lastVisibleCharacterOfLine = ((this.m_lastVisibleCharacterOfLine < this.m_firstVisibleCharacterOfLine) ? this.m_firstVisibleCharacterOfLine : this.m_lastVisibleCharacterOfLine));
			this.m_textInfo.lineInfo[this.m_lineNumber].characterCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex + 1;
			this.m_textInfo.lineInfo[this.m_lineNumber].visibleCharacterCount = this.m_lineVisibleCharacterCount;
			this.m_textInfo.lineInfo[this.m_lineNumber].visibleSpaceCount = this.m_lineVisibleSpaceCount;
			this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_firstVisibleCharacterOfLine].bottomLeft.x, lineDescender);
			this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].topRight.x, lineAscender);
			this.m_textInfo.lineInfo[this.m_lineNumber].length = this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max.x;
			this.m_textInfo.lineInfo[this.m_lineNumber].width = width;
			float maxAdvanceOffset = (this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].adjustedHorizontalAdvance * currentElementScale + (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
			float adjustedHorizontalAdvance = (this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance + (this.m_isRightToLeft ? maxAdvanceOffset : (-maxAdvanceOffset)));
			this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance = adjustedHorizontalAdvance;
			this.m_textInfo.lineInfo[this.m_lineNumber].baseline = 0f - this.m_lineOffset;
			this.m_textInfo.lineInfo[this.m_lineNumber].ascender = lineAscender;
			this.m_textInfo.lineInfo[this.m_lineNumber].descender = lineDescender;
			this.m_textInfo.lineInfo[this.m_lineNumber].lineHeight = lineAscender - lineDescender + lineGap * baseScale;
			this.m_firstCharacterOfLine = this.m_characterCount;
			this.m_lineVisibleCharacterCount = 0;
			this.m_lineVisibleSpaceCount = 0;
			this.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, i, this.m_characterCount - 1);
			this.m_lineNumber++;
			if (this.m_lineNumber >= this.m_textInfo.lineInfo.Length)
			{
				this.ResizeLineExtents(this.m_lineNumber);
			}
			if (this.m_lineHeight == -32767f)
			{
				float ascender = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
				float lineOffsetDelta = 0f - this.m_maxLineDescender + ascender + (lineGap + this.m_lineSpacingDelta) * baseScale + this.m_lineSpacing * currentEmScale;
				this.m_lineOffset += lineOffsetDelta;
				this.m_startOfLineAscender = ascender;
			}
			else
			{
				this.m_lineOffset += this.m_lineHeight + this.m_lineSpacing * currentEmScale;
			}
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_xAdvance = 0f + this.tag_Indent;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001B648 File Offset: 0x00019848
		internal void SaveWordWrappingState(ref WordWrapState state, int index, int count)
		{
			state.currentFontAsset = this.m_currentFontAsset;
			state.currentSpriteAsset = this.m_currentSpriteAsset;
			state.currentMaterial = this.m_currentMaterial;
			state.currentMaterialIndex = this.m_currentMaterialIndex;
			state.previous_WordBreak = index;
			state.total_CharacterCount = count;
			state.visible_CharacterCount = this.m_lineVisibleCharacterCount;
			state.visibleSpaceCount = this.m_lineVisibleSpaceCount;
			state.visible_LinkCount = this.m_textInfo.linkCount;
			state.firstCharacterIndex = this.m_firstCharacterOfLine;
			state.firstVisibleCharacterIndex = this.m_firstVisibleCharacterOfLine;
			state.lastVisibleCharIndex = this.m_lastVisibleCharacterOfLine;
			state.fontStyle = this.m_FontStyleInternal;
			state.italicAngle = this.m_ItalicAngle;
			state.fontScaleMultiplier = this.m_fontScaleMultiplier;
			state.currentFontSize = this.m_currentFontSize;
			state.xAdvance = this.m_xAdvance;
			state.maxCapHeight = this.m_maxCapHeight;
			state.maxAscender = this.m_maxTextAscender;
			state.maxDescender = this.m_ElementDescender;
			state.startOfLineAscender = this.m_startOfLineAscender;
			state.maxLineAscender = this.m_maxLineAscender;
			state.maxLineDescender = this.m_maxLineDescender;
			state.pageAscender = this.m_PageAscender;
			state.preferredWidth = this.m_preferredWidth;
			state.preferredHeight = this.m_preferredHeight;
			state.renderedWidth = this.m_RenderedWidth;
			state.renderedHeight = this.m_RenderedHeight;
			state.meshExtents = this.m_meshExtents;
			state.lineNumber = this.m_lineNumber;
			state.lineOffset = this.m_lineOffset;
			state.baselineOffset = this.m_baselineOffset;
			state.isDrivenLineSpacing = this.m_IsDrivenLineSpacing;
			state.lastBaseGlyphIndex = this.m_LastBaseGlyphIndex;
			state.cSpace = this.m_cSpacing;
			state.mSpace = this.m_monoSpacing;
			state.horizontalAlignment = this.m_lineJustification;
			state.marginLeft = this.m_marginLeft;
			state.marginRight = this.m_marginRight;
			state.vertexColor = this.m_htmlColor;
			state.underlineColor = this.m_underlineColor;
			state.strikethroughColor = this.m_strikethroughColor;
			state.highlightState = this.m_HighlightState;
			state.isNonBreakingSpace = this.m_isNonBreakingSpace;
			state.tagNoParsing = this.tag_NoParsing;
			state.fxRotation = this.m_FXRotation;
			state.fxScale = this.m_FXScale;
			state.basicStyleStack = this.m_fontStyleStack;
			state.italicAngleStack = this.m_ItalicAngleStack;
			state.colorStack = this.m_colorStack;
			state.underlineColorStack = this.m_underlineColorStack;
			state.strikethroughColorStack = this.m_strikethroughColorStack;
			state.highlightStateStack = this.m_HighlightStateStack;
			state.colorGradientStack = this.m_colorGradientStack;
			state.sizeStack = this.m_sizeStack;
			state.indentStack = this.m_indentStack;
			state.fontWeightStack = this.m_FontWeightStack;
			state.baselineStack = this.m_baselineOffsetStack;
			state.actionStack = this.m_actionStack;
			state.materialReferenceStack = TMP_Text.m_materialReferenceStack;
			state.lineJustificationStack = this.m_lineJustificationStack;
			state.spriteAnimationID = this.m_spriteAnimationID;
			if (this.m_lineNumber < this.m_textInfo.lineInfo.Length)
			{
				state.lineInfo = this.m_textInfo.lineInfo[this.m_lineNumber];
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001B968 File Offset: 0x00019B68
		internal int RestoreWordWrappingState(ref WordWrapState state)
		{
			int previous_WordBreak = state.previous_WordBreak;
			this.m_currentFontAsset = state.currentFontAsset;
			this.m_currentSpriteAsset = state.currentSpriteAsset;
			this.m_currentMaterial = state.currentMaterial;
			this.m_currentMaterialIndex = state.currentMaterialIndex;
			this.m_characterCount = state.total_CharacterCount + 1;
			this.m_lineVisibleCharacterCount = state.visible_CharacterCount;
			this.m_lineVisibleSpaceCount = state.visibleSpaceCount;
			this.m_textInfo.linkCount = state.visible_LinkCount;
			this.m_firstCharacterOfLine = state.firstCharacterIndex;
			this.m_firstVisibleCharacterOfLine = state.firstVisibleCharacterIndex;
			this.m_lastVisibleCharacterOfLine = state.lastVisibleCharIndex;
			this.m_FontStyleInternal = state.fontStyle;
			this.m_ItalicAngle = state.italicAngle;
			this.m_fontScaleMultiplier = state.fontScaleMultiplier;
			this.m_currentFontSize = state.currentFontSize;
			this.m_xAdvance = state.xAdvance;
			this.m_maxCapHeight = state.maxCapHeight;
			this.m_maxTextAscender = state.maxAscender;
			this.m_ElementDescender = state.maxDescender;
			this.m_startOfLineAscender = state.startOfLineAscender;
			this.m_maxLineAscender = state.maxLineAscender;
			this.m_maxLineDescender = state.maxLineDescender;
			this.m_PageAscender = state.pageAscender;
			this.m_preferredWidth = state.preferredWidth;
			this.m_preferredHeight = state.preferredHeight;
			this.m_RenderedWidth = state.renderedWidth;
			this.m_RenderedHeight = state.renderedHeight;
			this.m_meshExtents = state.meshExtents;
			this.m_lineNumber = state.lineNumber;
			this.m_lineOffset = state.lineOffset;
			this.m_baselineOffset = state.baselineOffset;
			this.m_IsDrivenLineSpacing = state.isDrivenLineSpacing;
			this.m_LastBaseGlyphIndex = state.lastBaseGlyphIndex;
			this.m_cSpacing = state.cSpace;
			this.m_monoSpacing = state.mSpace;
			this.m_lineJustification = state.horizontalAlignment;
			this.m_marginLeft = state.marginLeft;
			this.m_marginRight = state.marginRight;
			this.m_htmlColor = state.vertexColor;
			this.m_underlineColor = state.underlineColor;
			this.m_strikethroughColor = state.strikethroughColor;
			this.m_HighlightState = state.highlightState;
			this.m_isNonBreakingSpace = state.isNonBreakingSpace;
			this.tag_NoParsing = state.tagNoParsing;
			this.m_FXRotation = state.fxRotation;
			this.m_FXScale = state.fxScale;
			this.m_fontStyleStack = state.basicStyleStack;
			this.m_ItalicAngleStack = state.italicAngleStack;
			this.m_colorStack = state.colorStack;
			this.m_underlineColorStack = state.underlineColorStack;
			this.m_strikethroughColorStack = state.strikethroughColorStack;
			this.m_HighlightStateStack = state.highlightStateStack;
			this.m_colorGradientStack = state.colorGradientStack;
			this.m_sizeStack = state.sizeStack;
			this.m_indentStack = state.indentStack;
			this.m_FontWeightStack = state.fontWeightStack;
			this.m_baselineOffsetStack = state.baselineStack;
			this.m_actionStack = state.actionStack;
			TMP_Text.m_materialReferenceStack = state.materialReferenceStack;
			this.m_lineJustificationStack = state.lineJustificationStack;
			this.m_spriteAnimationID = state.spriteAnimationID;
			if (this.m_lineNumber < this.m_textInfo.lineInfo.Length)
			{
				this.m_textInfo.lineInfo[this.m_lineNumber] = state.lineInfo;
			}
			return previous_WordBreak;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001BC90 File Offset: 0x00019E90
		protected virtual void SaveGlyphVertexInfo(float padding, float style_padding, Color32 vertexColor)
		{
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.position = this.m_textInfo.characterInfo[this.m_characterCount].topLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.position = this.m_textInfo.characterInfo[this.m_characterCount].topRight;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomRight;
			vertexColor.a = ((this.m_fontColor32.a < vertexColor.a) ? this.m_fontColor32.a : vertexColor.a);
			bool isColorGlyph = (this.m_currentFontAsset.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536;
			if (!this.m_enableVertexGradient || isColorGlyph)
			{
				vertexColor = (isColorGlyph ? new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, vertexColor.a) : vertexColor);
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = vertexColor;
			}
			else if (!this.m_overrideHtmlColors && this.m_colorStack.index > 1)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = vertexColor;
			}
			else if (this.m_fontColorGradientPreset != null)
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_fontColorGradientPreset.bottomLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_fontColorGradientPreset.topLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_fontColorGradientPreset.topRight * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_fontColorGradientPreset.bottomRight * vertexColor;
			}
			else
			{
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_fontColorGradient.bottomLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_fontColorGradient.topLeft * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_fontColorGradient.topRight * vertexColor;
				this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_fontColorGradient.bottomRight * vertexColor;
			}
			if (this.m_colorGradientPreset != null && !isColorGlyph)
			{
				if (this.m_colorGradientPresetIsTinted)
				{
					TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
					int characterCount = this.m_characterCount;
					characterInfo[characterCount].vertex_BL.color = characterInfo[characterCount].vertex_BL.color * this.m_colorGradientPreset.bottomLeft;
					TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
					int characterCount2 = this.m_characterCount;
					characterInfo2[characterCount2].vertex_TL.color = characterInfo2[characterCount2].vertex_TL.color * this.m_colorGradientPreset.topLeft;
					TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
					int characterCount3 = this.m_characterCount;
					characterInfo3[characterCount3].vertex_TR.color = characterInfo3[characterCount3].vertex_TR.color * this.m_colorGradientPreset.topRight;
					TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
					int characterCount4 = this.m_characterCount;
					characterInfo4[characterCount4].vertex_BR.color = characterInfo4[characterCount4].vertex_BR.color * this.m_colorGradientPreset.bottomRight;
				}
				else
				{
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = this.m_colorGradientPreset.bottomLeft.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = this.m_colorGradientPreset.topLeft.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = this.m_colorGradientPreset.topRight.MinAlpha(vertexColor);
					this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = this.m_colorGradientPreset.bottomRight.MinAlpha(vertexColor);
				}
			}
			if (!this.m_isSDFShader)
			{
				style_padding = 0f;
			}
			Glyph altGlyph = this.m_textInfo.characterInfo[this.m_characterCount].alternativeGlyph;
			GlyphRect glyphRect = ((altGlyph == null) ? this.m_cached_TextElement.m_Glyph.glyphRect : altGlyph.glyphRect);
			Vector2 uv0;
			uv0.x = ((float)glyphRect.x - padding - style_padding) / (float)this.m_currentFontAsset.m_AtlasWidth;
			uv0.y = ((float)glyphRect.y - padding - style_padding) / (float)this.m_currentFontAsset.m_AtlasHeight;
			Vector2 uv;
			uv.x = uv0.x;
			uv.y = ((float)glyphRect.y + padding + style_padding + (float)glyphRect.height) / (float)this.m_currentFontAsset.m_AtlasHeight;
			Vector2 uv2;
			uv2.x = ((float)glyphRect.x + padding + style_padding + (float)glyphRect.width) / (float)this.m_currentFontAsset.m_AtlasWidth;
			uv2.y = uv.y;
			Vector2 uv3;
			uv3.x = uv2.x;
			uv3.y = uv0.y;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.uv = uv0;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.uv = uv;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.uv = uv2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.uv = uv3;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		protected virtual void SaveSpriteVertexInfo(Color32 vertexColor)
		{
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.position = this.m_textInfo.characterInfo[this.m_characterCount].topLeft;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.position = this.m_textInfo.characterInfo[this.m_characterCount].topRight;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.position = this.m_textInfo.characterInfo[this.m_characterCount].bottomRight;
			if (this.m_tintAllSprites)
			{
				this.m_tintSprite = true;
			}
			Color32 spriteColor = (this.m_tintSprite ? this.m_spriteColor.Multiply(vertexColor) : this.m_spriteColor);
			spriteColor.a = ((spriteColor.a < this.m_fontColor32.a) ? ((spriteColor.a < vertexColor.a) ? spriteColor.a : vertexColor.a) : this.m_fontColor32.a);
			Color32 c0 = spriteColor;
			Color32 c = spriteColor;
			Color32 c2 = spriteColor;
			Color32 c3 = spriteColor;
			if (this.m_enableVertexGradient)
			{
				if (this.m_fontColorGradientPreset != null)
				{
					c0 = (this.m_tintSprite ? c0.Multiply(this.m_fontColorGradientPreset.bottomLeft) : c0);
					c = (this.m_tintSprite ? c.Multiply(this.m_fontColorGradientPreset.topLeft) : c);
					c2 = (this.m_tintSprite ? c2.Multiply(this.m_fontColorGradientPreset.topRight) : c2);
					c3 = (this.m_tintSprite ? c3.Multiply(this.m_fontColorGradientPreset.bottomRight) : c3);
				}
				else
				{
					c0 = (this.m_tintSprite ? c0.Multiply(this.m_fontColorGradient.bottomLeft) : c0);
					c = (this.m_tintSprite ? c.Multiply(this.m_fontColorGradient.topLeft) : c);
					c2 = (this.m_tintSprite ? c2.Multiply(this.m_fontColorGradient.topRight) : c2);
					c3 = (this.m_tintSprite ? c3.Multiply(this.m_fontColorGradient.bottomRight) : c3);
				}
			}
			if (this.m_colorGradientPreset != null)
			{
				c0 = (this.m_tintSprite ? c0.Multiply(this.m_colorGradientPreset.bottomLeft) : c0);
				c = (this.m_tintSprite ? c.Multiply(this.m_colorGradientPreset.topLeft) : c);
				c2 = (this.m_tintSprite ? c2.Multiply(this.m_colorGradientPreset.topRight) : c2);
				c3 = (this.m_tintSprite ? c3.Multiply(this.m_colorGradientPreset.bottomRight) : c3);
			}
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.color = c0;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.color = c;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.color = c2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.color = c3;
			GlyphRect glyphRect = this.m_cached_TextElement.m_Glyph.glyphRect;
			Vector2 uv0 = new Vector2((float)glyphRect.x / (float)this.m_currentSpriteAsset.spriteSheet.width, (float)glyphRect.y / (float)this.m_currentSpriteAsset.spriteSheet.height);
			Vector2 uv = new Vector2(uv0.x, (float)(glyphRect.y + glyphRect.height) / (float)this.m_currentSpriteAsset.spriteSheet.height);
			Vector2 uv2 = new Vector2((float)(glyphRect.x + glyphRect.width) / (float)this.m_currentSpriteAsset.spriteSheet.width, uv.y);
			Vector2 uv3 = new Vector2(uv2.x, uv0.y);
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BL.uv = uv0;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TL.uv = uv;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_TR.uv = uv2;
			this.m_textInfo.characterInfo[this.m_characterCount].vertex_BR.uv = uv3;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001CA18 File Offset: 0x0001AC18
		protected virtual void FillCharacterVertexBuffers(int i)
		{
			int materialIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			int index_X4 = this.m_textInfo.meshInfo[materialIndex].vertexCount;
			if (index_X4 >= this.m_textInfo.meshInfo[materialIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((index_X4 + 4) / 4));
			}
			TMP_CharacterInfo[] characterInfoArray = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialIndex].vertices[index_X4] = characterInfoArray[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[1 + index_X4] = characterInfoArray[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[2 + index_X4] = characterInfoArray[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[3 + index_X4] = characterInfoArray[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialIndex].uvs0[index_X4] = characterInfoArray[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[1 + index_X4] = characterInfoArray[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[2 + index_X4] = characterInfoArray[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[3 + index_X4] = characterInfoArray[i].vertex_BR.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs2[index_X4] = characterInfoArray[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[1 + index_X4] = characterInfoArray[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[2 + index_X4] = characterInfoArray[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[3 + index_X4] = characterInfoArray[i].vertex_BR.uv2;
			this.m_textInfo.meshInfo[materialIndex].colors32[index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_BL.color.GammaToLinear() : characterInfoArray[i].vertex_BL.color);
			this.m_textInfo.meshInfo[materialIndex].colors32[1 + index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_TL.color.GammaToLinear() : characterInfoArray[i].vertex_TL.color);
			this.m_textInfo.meshInfo[materialIndex].colors32[2 + index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_TR.color.GammaToLinear() : characterInfoArray[i].vertex_TR.color);
			this.m_textInfo.meshInfo[materialIndex].colors32[3 + index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_BR.color.GammaToLinear() : characterInfoArray[i].vertex_BR.color);
			this.m_textInfo.meshInfo[materialIndex].vertexCount = index_X4 + 4;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001CE34 File Offset: 0x0001B034
		protected virtual void FillCharacterVertexBuffers(int i, bool isVolumetric)
		{
			int materialIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			int index_X4 = this.m_textInfo.meshInfo[materialIndex].vertexCount;
			if (index_X4 >= this.m_textInfo.meshInfo[materialIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((index_X4 + (isVolumetric ? 8 : 4)) / 4));
			}
			TMP_CharacterInfo[] characterInfoArray = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialIndex].vertices[index_X4] = characterInfoArray[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[1 + index_X4] = characterInfoArray[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[2 + index_X4] = characterInfoArray[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[3 + index_X4] = characterInfoArray[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialIndex].uvs0[index_X4] = characterInfoArray[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[1 + index_X4] = characterInfoArray[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[2 + index_X4] = characterInfoArray[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[3 + index_X4] = characterInfoArray[i].vertex_BR.uv;
			if (isVolumetric)
			{
				this.m_textInfo.meshInfo[materialIndex].uvs0[4 + index_X4] = characterInfoArray[i].vertex_BL.uv;
				this.m_textInfo.meshInfo[materialIndex].uvs0[5 + index_X4] = characterInfoArray[i].vertex_TL.uv;
				this.m_textInfo.meshInfo[materialIndex].uvs0[6 + index_X4] = characterInfoArray[i].vertex_TR.uv;
				this.m_textInfo.meshInfo[materialIndex].uvs0[7 + index_X4] = characterInfoArray[i].vertex_BR.uv;
			}
			this.m_textInfo.meshInfo[materialIndex].uvs2[index_X4] = characterInfoArray[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[1 + index_X4] = characterInfoArray[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[2 + index_X4] = characterInfoArray[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[3 + index_X4] = characterInfoArray[i].vertex_BR.uv2;
			if (isVolumetric)
			{
				this.m_textInfo.meshInfo[materialIndex].uvs2[4 + index_X4] = characterInfoArray[i].vertex_BL.uv2;
				this.m_textInfo.meshInfo[materialIndex].uvs2[5 + index_X4] = characterInfoArray[i].vertex_TL.uv2;
				this.m_textInfo.meshInfo[materialIndex].uvs2[6 + index_X4] = characterInfoArray[i].vertex_TR.uv2;
				this.m_textInfo.meshInfo[materialIndex].uvs2[7 + index_X4] = characterInfoArray[i].vertex_BR.uv2;
			}
			this.m_textInfo.meshInfo[materialIndex].colors32[index_X4] = characterInfoArray[i].vertex_BL.color;
			this.m_textInfo.meshInfo[materialIndex].colors32[1 + index_X4] = characterInfoArray[i].vertex_TL.color;
			this.m_textInfo.meshInfo[materialIndex].colors32[2 + index_X4] = characterInfoArray[i].vertex_TR.color;
			this.m_textInfo.meshInfo[materialIndex].colors32[3 + index_X4] = characterInfoArray[i].vertex_BR.color;
			if (isVolumetric)
			{
				Color32 backColor = new Color32(byte.MaxValue, byte.MaxValue, 128, byte.MaxValue);
				this.m_textInfo.meshInfo[materialIndex].colors32[4 + index_X4] = backColor;
				this.m_textInfo.meshInfo[materialIndex].colors32[5 + index_X4] = backColor;
				this.m_textInfo.meshInfo[materialIndex].colors32[6 + index_X4] = backColor;
				this.m_textInfo.meshInfo[materialIndex].colors32[7 + index_X4] = backColor;
			}
			this.m_textInfo.meshInfo[materialIndex].vertexCount = index_X4 + ((!isVolumetric) ? 4 : 8);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		protected virtual void FillSpriteVertexBuffers(int i)
		{
			int materialIndex = this.m_textInfo.characterInfo[i].materialReferenceIndex;
			int index_X4 = this.m_textInfo.meshInfo[materialIndex].vertexCount;
			if (index_X4 >= this.m_textInfo.meshInfo[materialIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[materialIndex].ResizeMeshInfo(Mathf.NextPowerOfTwo((index_X4 + 4) / 4));
			}
			TMP_CharacterInfo[] characterInfoArray = this.m_textInfo.characterInfo;
			this.m_textInfo.characterInfo[i].vertexIndex = index_X4;
			this.m_textInfo.meshInfo[materialIndex].vertices[index_X4] = characterInfoArray[i].vertex_BL.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[1 + index_X4] = characterInfoArray[i].vertex_TL.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[2 + index_X4] = characterInfoArray[i].vertex_TR.position;
			this.m_textInfo.meshInfo[materialIndex].vertices[3 + index_X4] = characterInfoArray[i].vertex_BR.position;
			this.m_textInfo.meshInfo[materialIndex].uvs0[index_X4] = characterInfoArray[i].vertex_BL.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[1 + index_X4] = characterInfoArray[i].vertex_TL.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[2 + index_X4] = characterInfoArray[i].vertex_TR.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs0[3 + index_X4] = characterInfoArray[i].vertex_BR.uv;
			this.m_textInfo.meshInfo[materialIndex].uvs2[index_X4] = characterInfoArray[i].vertex_BL.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[1 + index_X4] = characterInfoArray[i].vertex_TL.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[2 + index_X4] = characterInfoArray[i].vertex_TR.uv2;
			this.m_textInfo.meshInfo[materialIndex].uvs2[3 + index_X4] = characterInfoArray[i].vertex_BR.uv2;
			this.m_textInfo.meshInfo[materialIndex].colors32[index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_BL.color.GammaToLinear() : characterInfoArray[i].vertex_BL.color);
			this.m_textInfo.meshInfo[materialIndex].colors32[1 + index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_TL.color.GammaToLinear() : characterInfoArray[i].vertex_TL.color);
			this.m_textInfo.meshInfo[materialIndex].colors32[2 + index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_TR.color.GammaToLinear() : characterInfoArray[i].vertex_TR.color);
			this.m_textInfo.meshInfo[materialIndex].colors32[3 + index_X4] = (this.m_ConvertToLinearSpace ? characterInfoArray[i].vertex_BR.color.GammaToLinear() : characterInfoArray[i].vertex_BR.color);
			this.m_textInfo.meshInfo[materialIndex].vertexCount = index_X4 + 4;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001D818 File Offset: 0x0001BA18
		protected virtual void DrawUnderlineMesh(Vector3 start, Vector3 end, ref int index, float startScale, float endScale, float maxScale, float sdfScale, Color32 underlineColor)
		{
			this.GetUnderlineSpecialCharacter(this.m_fontAsset);
			if (this.m_Underline.character == null)
			{
				if (!TMP_Settings.warningsDisabled)
				{
					global::UnityEngine.Debug.LogWarning("Unable to add underline or strikethrough since the character [0x5F] used by these features is not present in the Font Asset assigned to this text object.", this);
				}
				return;
			}
			int underlineMaterialIndex = this.m_Underline.materialIndex;
			int verticesCount = index + 12;
			if (verticesCount > this.m_textInfo.meshInfo[underlineMaterialIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[underlineMaterialIndex].ResizeMeshInfo(verticesCount / 4);
			}
			start.y = Mathf.Min(start.y, end.y);
			end.y = Mathf.Min(start.y, end.y);
			GlyphMetrics underlineGlyphMetrics = this.m_Underline.character.glyph.metrics;
			GlyphRect underlineGlyphRect = this.m_Underline.character.glyph.glyphRect;
			float segmentWidth = underlineGlyphMetrics.width / 2f * maxScale;
			if (end.x - start.x < underlineGlyphMetrics.width * maxScale)
			{
				segmentWidth = (end.x - start.x) / 2f;
			}
			float startPadding = this.m_padding * startScale / maxScale;
			float endPadding = this.m_padding * endScale / maxScale;
			float underlineThickness = this.m_Underline.fontAsset.faceInfo.underlineThickness;
			Vector3[] vertices = this.m_textInfo.meshInfo[underlineMaterialIndex].vertices;
			vertices[index] = start + new Vector3(0f, 0f - (underlineThickness + this.m_padding) * maxScale, 0f);
			vertices[index + 1] = start + new Vector3(0f, this.m_padding * maxScale, 0f);
			vertices[index + 2] = vertices[index + 1] + new Vector3(segmentWidth, 0f, 0f);
			vertices[index + 3] = vertices[index] + new Vector3(segmentWidth, 0f, 0f);
			vertices[index + 4] = vertices[index + 3];
			vertices[index + 5] = vertices[index + 2];
			vertices[index + 6] = end + new Vector3(-segmentWidth, this.m_padding * maxScale, 0f);
			vertices[index + 7] = end + new Vector3(-segmentWidth, -(underlineThickness + this.m_padding) * maxScale, 0f);
			vertices[index + 8] = vertices[index + 7];
			vertices[index + 9] = vertices[index + 6];
			vertices[index + 10] = end + new Vector3(0f, this.m_padding * maxScale, 0f);
			vertices[index + 11] = end + new Vector3(0f, -(underlineThickness + this.m_padding) * maxScale, 0f);
			Vector4[] uvs = this.m_textInfo.meshInfo[underlineMaterialIndex].uvs0;
			int atlasWidth = this.m_Underline.fontAsset.atlasWidth;
			int atlasHeight = this.m_Underline.fontAsset.atlasHeight;
			float xScale = Mathf.Abs(sdfScale);
			Vector4 uv0 = new Vector4(((float)underlineGlyphRect.x - startPadding) / (float)atlasWidth, ((float)underlineGlyphRect.y - this.m_padding) / (float)atlasHeight, 0f, xScale);
			Vector4 uv = new Vector4(uv0.x, ((float)(underlineGlyphRect.y + underlineGlyphRect.height) + this.m_padding) / (float)atlasHeight, 0f, xScale);
			Vector4 uv2 = new Vector4(((float)underlineGlyphRect.x - startPadding + (float)underlineGlyphRect.width / 2f) / (float)atlasWidth, uv.y, 0f, xScale);
			Vector4 uv3 = new Vector4(uv2.x, uv0.y, 0f, xScale);
			Vector4 uv4 = new Vector4(((float)underlineGlyphRect.x + endPadding + (float)underlineGlyphRect.width / 2f) / (float)atlasWidth, uv.y, 0f, xScale);
			Vector4 uv5 = new Vector4(uv4.x, uv0.y, 0f, xScale);
			Vector4 uv6 = new Vector4(((float)underlineGlyphRect.x + endPadding + (float)underlineGlyphRect.width) / (float)atlasWidth, uv.y, 0f, xScale);
			Vector4 uv7 = new Vector4(uv6.x, uv0.y, 0f, xScale);
			uvs[index] = uv0;
			uvs[1 + index] = uv;
			uvs[2 + index] = uv2;
			uvs[3 + index] = uv3;
			uvs[4 + index] = new Vector4(uv2.x - uv2.x * 0.001f, uv0.y, 0f, xScale);
			uvs[5 + index] = new Vector4(uv2.x - uv2.x * 0.001f, uv.y, 0f, xScale);
			uvs[6 + index] = new Vector4(uv2.x + uv2.x * 0.001f, uv.y, 0f, xScale);
			uvs[7 + index] = new Vector4(uv2.x + uv2.x * 0.001f, uv0.y, 0f, xScale);
			uvs[8 + index] = uv5;
			uvs[9 + index] = uv4;
			uvs[10 + index] = uv6;
			uvs[11 + index] = uv7;
			float max_UvX = (vertices[index + 2].x - start.x) / (end.x - start.x);
			Vector2[] uvs2 = this.m_textInfo.meshInfo[underlineMaterialIndex].uvs2;
			uvs2[index] = new Vector2(0f, 0f);
			uvs2[1 + index] = new Vector2(0f, 1f);
			uvs2[2 + index] = new Vector2(max_UvX, 1f);
			uvs2[3 + index] = new Vector2(max_UvX, 0f);
			float min_UvX = (vertices[index + 4].x - start.x) / (end.x - start.x);
			max_UvX = (vertices[index + 6].x - start.x) / (end.x - start.x);
			uvs2[4 + index] = new Vector2(min_UvX, 0f);
			uvs2[5 + index] = new Vector2(min_UvX, 1f);
			uvs2[6 + index] = new Vector2(max_UvX, 1f);
			uvs2[7 + index] = new Vector2(max_UvX, 0f);
			min_UvX = (vertices[index + 8].x - start.x) / (end.x - start.x);
			uvs2[8 + index] = new Vector2(min_UvX, 0f);
			uvs2[9 + index] = new Vector2(min_UvX, 1f);
			uvs2[10 + index] = new Vector2(1f, 1f);
			uvs2[11 + index] = new Vector2(1f, 0f);
			underlineColor.a = ((this.m_fontColor32.a < underlineColor.a) ? this.m_fontColor32.a : underlineColor.a);
			Color32[] colors = this.m_textInfo.meshInfo[underlineMaterialIndex].colors32;
			colors[index] = underlineColor;
			colors[1 + index] = underlineColor;
			colors[2 + index] = underlineColor;
			colors[3 + index] = underlineColor;
			colors[4 + index] = underlineColor;
			colors[5 + index] = underlineColor;
			colors[6 + index] = underlineColor;
			colors[7 + index] = underlineColor;
			colors[8 + index] = underlineColor;
			colors[9 + index] = underlineColor;
			colors[10 + index] = underlineColor;
			colors[11 + index] = underlineColor;
			index += 12;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001E090 File Offset: 0x0001C290
		protected virtual void DrawTextHighlight(Vector3 start, Vector3 end, ref int index, Color32 highlightColor)
		{
			if (this.m_Underline.character == null)
			{
				this.GetUnderlineSpecialCharacter(this.m_fontAsset);
				if (this.m_Underline.character == null)
				{
					if (!TMP_Settings.warningsDisabled)
					{
						global::UnityEngine.Debug.LogWarning("Unable to add highlight since the primary Font Asset doesn't contain the underline character.", this);
					}
					return;
				}
			}
			int underlineMaterialIndex = this.m_Underline.materialIndex;
			int verticesCount = index + 4;
			if (verticesCount > this.m_textInfo.meshInfo[underlineMaterialIndex].vertices.Length)
			{
				this.m_textInfo.meshInfo[underlineMaterialIndex].ResizeMeshInfo(verticesCount / 4);
			}
			Vector3[] vertices = this.m_textInfo.meshInfo[underlineMaterialIndex].vertices;
			vertices[index] = start;
			vertices[index + 1] = new Vector3(start.x, end.y, 0f);
			vertices[index + 2] = end;
			vertices[index + 3] = new Vector3(end.x, start.y, 0f);
			Vector4[] uvs = this.m_textInfo.meshInfo[underlineMaterialIndex].uvs0;
			int atlasWidth = this.m_Underline.fontAsset.atlasWidth;
			int atlasHeight = this.m_Underline.fontAsset.atlasHeight;
			GlyphRect glyphRect = this.m_Underline.character.glyph.glyphRect;
			Vector2 uvGlyphCenter = new Vector2(((float)glyphRect.x + (float)glyphRect.width / 2f) / (float)atlasWidth, ((float)glyphRect.y + (float)glyphRect.height / 2f) / (float)atlasHeight);
			Vector2 uvTexelSize = new Vector2(1f / (float)atlasWidth, 1f / (float)atlasHeight);
			uvs[index] = uvGlyphCenter - uvTexelSize;
			uvs[index + 1] = uvGlyphCenter + new Vector2(-uvTexelSize.x, uvTexelSize.y);
			uvs[index + 2] = uvGlyphCenter + uvTexelSize;
			uvs[index + 3] = uvGlyphCenter + new Vector2(uvTexelSize.x, -uvTexelSize.y);
			Vector2[] uvs2 = this.m_textInfo.meshInfo[underlineMaterialIndex].uvs2;
			Vector2 customUV = new Vector2(0f, 1f);
			uvs2[index] = customUV;
			uvs2[index + 1] = customUV;
			uvs2[index + 2] = customUV;
			uvs2[index + 3] = customUV;
			highlightColor.a = ((this.m_fontColor32.a < highlightColor.a) ? this.m_fontColor32.a : highlightColor.a);
			Color32[] colors = this.m_textInfo.meshInfo[underlineMaterialIndex].colors32;
			colors[index] = highlightColor;
			colors[index + 1] = highlightColor;
			colors[index + 2] = highlightColor;
			colors[index + 3] = highlightColor;
			index += 4;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001E374 File Offset: 0x0001C574
		protected void LoadDefaultSettings()
		{
			if (this.m_fontSize == -99f || this.m_isWaitingOnResourceLoad)
			{
				this.m_rectTransform = this.rectTransform;
				if (TMP_Settings.autoSizeTextContainer)
				{
					this.autoSizeTextContainer = true;
				}
				else if (base.GetType() == typeof(TextMeshPro))
				{
					if (this.m_rectTransform.sizeDelta == new Vector2(100f, 100f))
					{
						this.m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProTextContainerSize;
					}
				}
				else if (this.m_rectTransform.sizeDelta == new Vector2(100f, 100f))
				{
					this.m_rectTransform.sizeDelta = TMP_Settings.defaultTextMeshProUITextContainerSize;
				}
				this.m_TextWrappingMode = TMP_Settings.textWrappingMode;
				this.m_ActiveFontFeatures = new List<OTL_FeatureTag>(TMP_Settings.fontFeatures);
				this.m_enableExtraPadding = TMP_Settings.enableExtraPadding;
				this.m_tintAllSprites = TMP_Settings.enableTintAllSprites;
				this.m_parseCtrlCharacters = TMP_Settings.enableParseEscapeCharacters;
				this.m_fontSize = (this.m_fontSizeBase = TMP_Settings.defaultFontSize);
				this.m_fontSizeMin = this.m_fontSize * TMP_Settings.defaultTextAutoSizingMinRatio;
				this.m_fontSizeMax = this.m_fontSize * TMP_Settings.defaultTextAutoSizingMaxRatio;
				this.m_isWaitingOnResourceLoad = false;
				this.raycastTarget = TMP_Settings.enableRaycastTarget;
				this.m_IsTextObjectScaleStatic = TMP_Settings.isTextObjectScaleStatic;
			}
			else
			{
				if (this.m_textAlignment < (TextAlignmentOptions)255)
				{
					this.m_textAlignment = TMP_Compatibility.ConvertTextAlignmentEnumValues(this.m_textAlignment);
				}
				if (this.m_ActiveFontFeatures.Count == 1 && this.m_ActiveFontFeatures[0] == (OTL_FeatureTag)0U)
				{
					this.m_ActiveFontFeatures.Clear();
					if (this.m_enableKerning)
					{
						this.m_ActiveFontFeatures.Add(OTL_FeatureTag.kern);
					}
				}
			}
			if (this.m_textAlignment != TextAlignmentOptions.Converted)
			{
				this.m_HorizontalAlignment = (HorizontalAlignmentOptions)(this.m_textAlignment & (TextAlignmentOptions)255);
				this.m_VerticalAlignment = (VerticalAlignmentOptions)(this.m_textAlignment & (TextAlignmentOptions)65280);
				this.m_textAlignment = TextAlignmentOptions.Converted;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001E55C File Offset: 0x0001C75C
		protected void GetSpecialCharacters(TMP_FontAsset fontAsset)
		{
			this.GetEllipsisSpecialCharacter(fontAsset);
			this.GetUnderlineSpecialCharacter(fontAsset);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001E56C File Offset: 0x0001C76C
		protected void GetEllipsisSpecialCharacter(TMP_FontAsset fontAsset)
		{
			bool isUsingAlternativeTypeface;
			TMP_Character character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(8230U, fontAsset, false, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
			if (character == null && fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0)
			{
				character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(8230U, fontAsset, fontAsset.m_FallbackFontAssetTable, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
			}
			if (character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
			{
				character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(8230U, fontAsset, TMP_Settings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
			}
			if (character == null && TMP_Settings.defaultFontAsset != null)
			{
				character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(8230U, TMP_Settings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
			}
			if (character != null)
			{
				this.m_Ellipsis = new TMP_Text.SpecialCharacter(character, 0);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001E644 File Offset: 0x0001C844
		protected void GetUnderlineSpecialCharacter(TMP_FontAsset fontAsset)
		{
			bool isUsingAlternativeTypeface;
			TMP_Character character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(95U, fontAsset, false, FontStyles.Normal, FontWeight.Regular, out isUsingAlternativeTypeface);
			if (character != null)
			{
				this.m_Underline = new TMP_Text.SpecialCharacter(character, 0);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001E674 File Offset: 0x0001C874
		protected void ReplaceTagWithCharacter(int[] chars, int insertionIndex, int tagLength, char c)
		{
			chars[insertionIndex] = (int)c;
			for (int i = insertionIndex + tagLength; i < chars.Length; i++)
			{
				chars[i - 3] = chars[i];
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		protected TMP_FontAsset GetFontAssetForWeight(int fontWeight)
		{
			bool flag = (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic || (this.m_fontStyle & FontStyles.Italic) == FontStyles.Italic;
			int weightIndex = fontWeight / 100;
			TMP_FontAsset fontAsset;
			if (flag)
			{
				fontAsset = this.m_currentFontAsset.fontWeightTable[weightIndex].italicTypeface;
			}
			else
			{
				fontAsset = this.m_currentFontAsset.fontWeightTable[weightIndex].regularTypeface;
			}
			return fontAsset;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001E700 File Offset: 0x0001C900
		internal TMP_TextElement GetTextElement(uint unicode, TMP_FontAsset fontAsset, FontStyles fontStyle, FontWeight fontWeight, out bool isUsingAlternativeTypeface)
		{
			TMP_Character character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			if (character != null)
			{
				return character;
			}
			if (fontAsset.m_FallbackFontAssetTable != null && fontAsset.m_FallbackFontAssetTable.Count > 0)
			{
				character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, fontAsset.m_FallbackFontAssetTable, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			}
			if (character != null)
			{
				fontAsset.AddCharacterToLookupCache(unicode, character);
				return character;
			}
			if (fontAsset.instanceID != this.m_fontAsset.instanceID)
			{
				character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_fontAsset, false, fontStyle, fontWeight, out isUsingAlternativeTypeface);
				if (character != null)
				{
					this.m_currentMaterialIndex = 0;
					this.m_currentMaterial = TMP_Text.m_materialReferences[0].material;
					fontAsset.AddCharacterToLookupCache(unicode, character);
					return character;
				}
				if (this.m_fontAsset.m_FallbackFontAssetTable != null && this.m_fontAsset.m_FallbackFontAssetTable.Count > 0)
				{
					character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, this.m_fontAsset.m_FallbackFontAssetTable, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
				}
				if (character != null)
				{
					fontAsset.AddCharacterToLookupCache(unicode, character);
					return character;
				}
			}
			if (this.m_spriteAsset != null)
			{
				TMP_SpriteCharacter spriteCharacter = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, this.m_spriteAsset, true);
				if (spriteCharacter != null)
				{
					return spriteCharacter;
				}
			}
			if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
			{
				character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(unicode, fontAsset, TMP_Settings.fallbackFontAssets, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			}
			if (character != null)
			{
				fontAsset.AddCharacterToLookupCache(unicode, character);
				return character;
			}
			if (TMP_Settings.defaultFontAsset != null)
			{
				character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, TMP_Settings.defaultFontAsset, true, fontStyle, fontWeight, out isUsingAlternativeTypeface);
			}
			if (character != null)
			{
				fontAsset.AddCharacterToLookupCache(unicode, character);
				return character;
			}
			if (TMP_Settings.defaultSpriteAsset != null)
			{
				TMP_SpriteCharacter spriteCharacter2 = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset(unicode, TMP_Settings.defaultSpriteAsset, true);
				if (spriteCharacter2 != null)
				{
					return spriteCharacter2;
				}
			}
			return null;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void SetActiveSubMeshes(bool state)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected virtual void DestroySubMeshObjects()
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void ClearMesh()
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void ClearMesh(bool uploadGeometry)
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001E894 File Offset: 0x0001CA94
		public virtual string GetParsedText()
		{
			if (this.m_textInfo == null)
			{
				return string.Empty;
			}
			int characterCount = this.m_textInfo.characterCount;
			char[] buffer = new char[characterCount];
			int i = 0;
			while (i < characterCount && i < this.m_textInfo.characterInfo.Length)
			{
				buffer[i] = this.m_textInfo.characterInfo[i].character;
				i++;
			}
			return new string(buffer);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001E8FD File Offset: 0x0001CAFD
		internal bool IsSelfOrLinkedAncestor(TMP_Text targetTextComponent)
		{
			return targetTextComponent == null || (this.parentLinkedComponent != null && this.parentLinkedComponent.IsSelfOrLinkedAncestor(targetTextComponent)) || base.GetInstanceID() == targetTextComponent.GetInstanceID();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001E93C File Offset: 0x0001CB3C
		internal void ReleaseLinkedTextComponent(TMP_Text targetTextComponent)
		{
			if (targetTextComponent == null)
			{
				return;
			}
			TMP_Text childLinkedComponent = targetTextComponent.linkedTextComponent;
			if (childLinkedComponent != null)
			{
				this.ReleaseLinkedTextComponent(childLinkedComponent);
			}
			targetTextComponent.text = string.Empty;
			targetTextComponent.firstVisibleCharacter = 0;
			targetTextComponent.linkedTextComponent = null;
			targetTextComponent.parentLinkedComponent = null;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001E98A File Offset: 0x0001CB8A
		protected void DoMissingGlyphCallback(int unicode, int stringIndex, TMP_FontAsset fontAsset)
		{
			TMP_Text.MissingCharacterEventCallback onMissingCharacter = TMP_Text.OnMissingCharacter;
			if (onMissingCharacter == null)
			{
				return;
			}
			onMissingCharacter(unicode, stringIndex, this.m_text, fontAsset, this);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		protected Vector2 PackUV(float x, float y, float scale)
		{
			Vector2 output;
			output.x = (float)((int)(x * 511f));
			output.y = (float)((int)(y * 511f));
			output.x = output.x * 4096f + output.y;
			output.y = scale;
			return output;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		protected float PackUV(float x, float y)
		{
			float num = (float)((double)((int)(x * 511f)));
			double y2 = (double)((int)(y * 511f));
			return (float)((double)num * 4096.0 + y2);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00002AAB File Offset: 0x00000CAB
		internal virtual void InternalUpdate()
		{
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001EA28 File Offset: 0x0001CC28
		protected uint HexToInt(char hex)
		{
			switch (hex)
			{
			case '0':
				return 0U;
			case '1':
				return 1U;
			case '2':
				return 2U;
			case '3':
				return 3U;
			case '4':
				return 4U;
			case '5':
				return 5U;
			case '6':
				return 6U;
			case '7':
				return 7U;
			case '8':
				return 8U;
			case '9':
				return 9U;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				break;
			case 'A':
				return 10U;
			case 'B':
				return 11U;
			case 'C':
				return 12U;
			case 'D':
				return 13U;
			case 'E':
				return 14U;
			case 'F':
				return 15U;
			default:
				switch (hex)
				{
				case 'a':
					return 10U;
				case 'b':
					return 11U;
				case 'c':
					return 12U;
				case 'd':
					return 13U;
				case 'e':
					return 14U;
				case 'f':
					return 15U;
				}
				break;
			}
			return 15U;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001EAF8 File Offset: 0x0001CCF8
		private bool IsValidUTF16(TMP_Text.TextBackingContainer text, int index)
		{
			for (int i = 0; i < 4; i++)
			{
				uint c = text[index + i];
				if ((c < 48U || c > 57U) && (c < 97U || c > 102U) && (c < 65U || c > 70U))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001EB3D File Offset: 0x0001CD3D
		private uint GetUTF16(uint[] text, int i)
		{
			return 0U + (this.HexToInt((char)text[i]) << 12) + (this.HexToInt((char)text[i + 1]) << 8) + (this.HexToInt((char)text[i + 2]) << 4) + this.HexToInt((char)text[i + 3]);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		private uint GetUTF16(TMP_Text.TextBackingContainer text, int i)
		{
			return 0U + (this.HexToInt((char)text[i]) << 12) + (this.HexToInt((char)text[i + 1]) << 8) + (this.HexToInt((char)text[i + 2]) << 4) + this.HexToInt((char)text[i + 3]);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		private bool IsValidUTF32(TMP_Text.TextBackingContainer text, int index)
		{
			for (int i = 0; i < 8; i++)
			{
				uint c = text[index + i];
				if ((c < 48U || c > 57U) && (c < 97U || c > 102U) && (c < 65U || c > 70U))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001EC20 File Offset: 0x0001CE20
		private uint GetUTF32(uint[] text, int i)
		{
			return 0U + (this.HexToInt((char)text[i]) << 28) + (this.HexToInt((char)text[i + 1]) << 24) + (this.HexToInt((char)text[i + 2]) << 20) + (this.HexToInt((char)text[i + 3]) << 16) + (this.HexToInt((char)text[i + 4]) << 12) + (this.HexToInt((char)text[i + 5]) << 8) + (this.HexToInt((char)text[i + 6]) << 4) + this.HexToInt((char)text[i + 7]);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		private uint GetUTF32(TMP_Text.TextBackingContainer text, int i)
		{
			return 0U + (this.HexToInt((char)text[i]) << 28) + (this.HexToInt((char)text[i + 1]) << 24) + (this.HexToInt((char)text[i + 2]) << 20) + (this.HexToInt((char)text[i + 3]) << 16) + (this.HexToInt((char)text[i + 4]) << 12) + (this.HexToInt((char)text[i + 5]) << 8) + (this.HexToInt((char)text[i + 6]) << 4) + this.HexToInt((char)text[i + 7]);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001ED58 File Offset: 0x0001CF58
		protected Color32 HexCharsToColor(char[] hexChars, int tagCount)
		{
			if (tagCount == 4)
			{
				byte b9 = (byte)(this.HexToInt(hexChars[1]) * 16U + this.HexToInt(hexChars[1]));
				byte g = (byte)(this.HexToInt(hexChars[2]) * 16U + this.HexToInt(hexChars[2]));
				byte b = (byte)(this.HexToInt(hexChars[3]) * 16U + this.HexToInt(hexChars[3]));
				return new Color32(b9, g, b, byte.MaxValue);
			}
			if (tagCount == 5)
			{
				byte b10 = (byte)(this.HexToInt(hexChars[1]) * 16U + this.HexToInt(hexChars[1]));
				byte g2 = (byte)(this.HexToInt(hexChars[2]) * 16U + this.HexToInt(hexChars[2]));
				byte b2 = (byte)(this.HexToInt(hexChars[3]) * 16U + this.HexToInt(hexChars[3]));
				byte a = (byte)(this.HexToInt(hexChars[4]) * 16U + this.HexToInt(hexChars[4]));
				return new Color32(b10, g2, b2, a);
			}
			if (tagCount == 7)
			{
				byte b11 = (byte)(this.HexToInt(hexChars[1]) * 16U + this.HexToInt(hexChars[2]));
				byte g3 = (byte)(this.HexToInt(hexChars[3]) * 16U + this.HexToInt(hexChars[4]));
				byte b3 = (byte)(this.HexToInt(hexChars[5]) * 16U + this.HexToInt(hexChars[6]));
				return new Color32(b11, g3, b3, byte.MaxValue);
			}
			if (tagCount == 9)
			{
				byte b12 = (byte)(this.HexToInt(hexChars[1]) * 16U + this.HexToInt(hexChars[2]));
				byte g4 = (byte)(this.HexToInt(hexChars[3]) * 16U + this.HexToInt(hexChars[4]));
				byte b4 = (byte)(this.HexToInt(hexChars[5]) * 16U + this.HexToInt(hexChars[6]));
				byte a2 = (byte)(this.HexToInt(hexChars[7]) * 16U + this.HexToInt(hexChars[8]));
				return new Color32(b12, g4, b4, a2);
			}
			if (tagCount == 10)
			{
				byte b13 = (byte)(this.HexToInt(hexChars[7]) * 16U + this.HexToInt(hexChars[7]));
				byte g5 = (byte)(this.HexToInt(hexChars[8]) * 16U + this.HexToInt(hexChars[8]));
				byte b5 = (byte)(this.HexToInt(hexChars[9]) * 16U + this.HexToInt(hexChars[9]));
				return new Color32(b13, g5, b5, byte.MaxValue);
			}
			if (tagCount == 11)
			{
				byte b14 = (byte)(this.HexToInt(hexChars[7]) * 16U + this.HexToInt(hexChars[7]));
				byte g6 = (byte)(this.HexToInt(hexChars[8]) * 16U + this.HexToInt(hexChars[8]));
				byte b6 = (byte)(this.HexToInt(hexChars[9]) * 16U + this.HexToInt(hexChars[9]));
				byte a3 = (byte)(this.HexToInt(hexChars[10]) * 16U + this.HexToInt(hexChars[10]));
				return new Color32(b14, g6, b6, a3);
			}
			if (tagCount == 13)
			{
				byte b15 = (byte)(this.HexToInt(hexChars[7]) * 16U + this.HexToInt(hexChars[8]));
				byte g7 = (byte)(this.HexToInt(hexChars[9]) * 16U + this.HexToInt(hexChars[10]));
				byte b7 = (byte)(this.HexToInt(hexChars[11]) * 16U + this.HexToInt(hexChars[12]));
				return new Color32(b15, g7, b7, byte.MaxValue);
			}
			if (tagCount == 15)
			{
				byte b16 = (byte)(this.HexToInt(hexChars[7]) * 16U + this.HexToInt(hexChars[8]));
				byte g8 = (byte)(this.HexToInt(hexChars[9]) * 16U + this.HexToInt(hexChars[10]));
				byte b8 = (byte)(this.HexToInt(hexChars[11]) * 16U + this.HexToInt(hexChars[12]));
				byte a4 = (byte)(this.HexToInt(hexChars[13]) * 16U + this.HexToInt(hexChars[14]));
				return new Color32(b16, g8, b8, a4);
			}
			return new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
		protected Color32 HexCharsToColor(char[] hexChars, int startIndex, int length)
		{
			if (length == 7)
			{
				byte b3 = (byte)(this.HexToInt(hexChars[startIndex + 1]) * 16U + this.HexToInt(hexChars[startIndex + 2]));
				byte g = (byte)(this.HexToInt(hexChars[startIndex + 3]) * 16U + this.HexToInt(hexChars[startIndex + 4]));
				byte b = (byte)(this.HexToInt(hexChars[startIndex + 5]) * 16U + this.HexToInt(hexChars[startIndex + 6]));
				return new Color32(b3, g, b, byte.MaxValue);
			}
			if (length == 9)
			{
				byte b4 = (byte)(this.HexToInt(hexChars[startIndex + 1]) * 16U + this.HexToInt(hexChars[startIndex + 2]));
				byte g2 = (byte)(this.HexToInt(hexChars[startIndex + 3]) * 16U + this.HexToInt(hexChars[startIndex + 4]));
				byte b2 = (byte)(this.HexToInt(hexChars[startIndex + 5]) * 16U + this.HexToInt(hexChars[startIndex + 6]));
				byte a = (byte)(this.HexToInt(hexChars[startIndex + 7]) * 16U + this.HexToInt(hexChars[startIndex + 8]));
				return new Color32(b4, g2, b2, a);
			}
			return TMP_Text.s_colorWhite;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001F1BC File Offset: 0x0001D3BC
		private int GetAttributeParameters(char[] chars, int startIndex, int length, ref float[] parameters)
		{
			int endIndex = startIndex;
			int attributeCount = 0;
			while (endIndex < startIndex + length)
			{
				parameters[attributeCount] = this.ConvertToFloat(chars, startIndex, length, out endIndex);
				length -= endIndex - startIndex + 1;
				startIndex = endIndex + 1;
				attributeCount++;
			}
			return attributeCount;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		protected float ConvertToFloat(char[] chars, int startIndex, int length)
		{
			int lastIndex;
			return this.ConvertToFloat(chars, startIndex, length, out lastIndex);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001F210 File Offset: 0x0001D410
		protected float ConvertToFloat(char[] chars, int startIndex, int length, out int lastIndex)
		{
			if (startIndex == 0)
			{
				lastIndex = 0;
				return -32768f;
			}
			int endIndex = startIndex + length;
			bool isIntegerValue = true;
			float decimalPointMultiplier = 0f;
			int valueSignMultiplier = 1;
			if (chars[startIndex] == '+')
			{
				valueSignMultiplier = 1;
				startIndex++;
			}
			else if (chars[startIndex] == '-')
			{
				valueSignMultiplier = -1;
				startIndex++;
			}
			float value = 0f;
			for (int i = startIndex; i < endIndex; i++)
			{
				uint c = (uint)chars[i];
				if ((c >= 48U && c <= 57U) || c == 46U)
				{
					if (c == 46U)
					{
						isIntegerValue = false;
						decimalPointMultiplier = 0.1f;
					}
					else if (isIntegerValue)
					{
						value = value * 10f + (float)((ulong)(c - 48U) * (ulong)((long)valueSignMultiplier));
					}
					else
					{
						value += (c - 48U) * decimalPointMultiplier * (float)valueSignMultiplier;
						decimalPointMultiplier *= 0.1f;
					}
				}
				else if (c == 44U)
				{
					if (i + 1 < endIndex && chars[i + 1] == ' ')
					{
						lastIndex = i + 1;
					}
					else
					{
						lastIndex = i;
					}
					if (value > 32767f)
					{
						return -32768f;
					}
					return value;
				}
			}
			lastIndex = endIndex;
			if (value > 32767f)
			{
				return -32768f;
			}
			return value;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001F31C File Offset: 0x0001D51C
		private void ClearMarkupTagAttributes()
		{
			int length = TMP_Text.m_xmlAttribute.Length;
			for (int i = 0; i < length; i++)
			{
				TMP_Text.m_xmlAttribute[i] = default(RichTextTagAttribute);
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001F350 File Offset: 0x0001D550
		internal bool ValidateHtmlTag(TMP_Text.TextProcessingElement[] chars, int startIndex, out int endIndex)
		{
			int tagCharCount = 0;
			byte attributeFlag = 0;
			int attributeIndex = 0;
			this.ClearMarkupTagAttributes();
			TagValueType tagValueType = TagValueType.None;
			TagUnitType tagUnitType = TagUnitType.Pixels;
			endIndex = startIndex;
			bool isTagSet = false;
			bool isValidHtmlTag = false;
			int i = startIndex;
			while (i < chars.Length && chars[i].unicode != 0U && tagCharCount < TMP_Text.m_htmlTag.Length && chars[i].unicode != 60U)
			{
				uint unicode = chars[i].unicode;
				if (unicode == 62U)
				{
					isValidHtmlTag = true;
					endIndex = i;
					TMP_Text.m_htmlTag[tagCharCount] = '\0';
					break;
				}
				TMP_Text.m_htmlTag[tagCharCount] = (char)unicode;
				tagCharCount++;
				if (attributeFlag == 1)
				{
					if (tagValueType == TagValueType.None)
					{
						if (unicode == 43U || unicode == 45U || unicode == 46U || (unicode >= 48U && unicode <= 57U))
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.NumericalValue);
							TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = tagCharCount - 1;
							RichTextTagAttribute[] xmlAttribute = TMP_Text.m_xmlAttribute;
							int num = attributeIndex;
							xmlAttribute[num].valueLength = xmlAttribute[num].valueLength + 1;
						}
						else if (unicode == 35U)
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.ColorValue);
							TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = tagCharCount - 1;
							RichTextTagAttribute[] xmlAttribute2 = TMP_Text.m_xmlAttribute;
							int num2 = attributeIndex;
							xmlAttribute2[num2].valueLength = xmlAttribute2[num2].valueLength + 1;
						}
						else if (unicode == 34U)
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.StringValue);
							TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = tagCharCount;
						}
						else
						{
							tagUnitType = TagUnitType.Pixels;
							tagValueType = (TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.StringValue);
							TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = tagCharCount - 1;
							TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode = ((TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode << 5) + TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode) ^ (int)TMP_TextUtilities.ToUpperFast((char)unicode);
							RichTextTagAttribute[] xmlAttribute3 = TMP_Text.m_xmlAttribute;
							int num3 = attributeIndex;
							xmlAttribute3[num3].valueLength = xmlAttribute3[num3].valueLength + 1;
						}
					}
					else if (tagValueType == TagValueType.NumericalValue)
					{
						if (unicode == 112U || unicode == 101U || unicode == 37U || unicode == 32U)
						{
							attributeFlag = 2;
							tagValueType = TagValueType.None;
							if (unicode != 37U)
							{
								if (unicode == 101U)
								{
									tagUnitType = (TMP_Text.m_xmlAttribute[attributeIndex].unitType = TagUnitType.FontUnits);
								}
								else
								{
									tagUnitType = (TMP_Text.m_xmlAttribute[attributeIndex].unitType = TagUnitType.Pixels);
								}
							}
							else
							{
								tagUnitType = (TMP_Text.m_xmlAttribute[attributeIndex].unitType = TagUnitType.Percentage);
							}
							attributeIndex++;
							TMP_Text.m_xmlAttribute[attributeIndex].nameHashCode = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.None;
							TMP_Text.m_xmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
							TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueLength = 0;
						}
						else
						{
							RichTextTagAttribute[] xmlAttribute4 = TMP_Text.m_xmlAttribute;
							int num4 = attributeIndex;
							xmlAttribute4[num4].valueLength = xmlAttribute4[num4].valueLength + 1;
						}
					}
					else if (tagValueType == TagValueType.ColorValue)
					{
						if (unicode != 32U)
						{
							RichTextTagAttribute[] xmlAttribute5 = TMP_Text.m_xmlAttribute;
							int num5 = attributeIndex;
							xmlAttribute5[num5].valueLength = xmlAttribute5[num5].valueLength + 1;
						}
						else
						{
							attributeFlag = 2;
							tagValueType = TagValueType.None;
							tagUnitType = TagUnitType.Pixels;
							attributeIndex++;
							TMP_Text.m_xmlAttribute[attributeIndex].nameHashCode = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.None;
							TMP_Text.m_xmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
							TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueLength = 0;
						}
					}
					else if (tagValueType == TagValueType.StringValue)
					{
						if (unicode != 34U)
						{
							TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode = ((TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode << 5) + TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode) ^ (int)TMP_TextUtilities.ToUpperFast((char)unicode);
							RichTextTagAttribute[] xmlAttribute6 = TMP_Text.m_xmlAttribute;
							int num6 = attributeIndex;
							xmlAttribute6[num6].valueLength = xmlAttribute6[num6].valueLength + 1;
						}
						else
						{
							attributeFlag = 2;
							tagValueType = TagValueType.None;
							tagUnitType = TagUnitType.Pixels;
							attributeIndex++;
							TMP_Text.m_xmlAttribute[attributeIndex].nameHashCode = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.None;
							TMP_Text.m_xmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
							TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = 0;
							TMP_Text.m_xmlAttribute[attributeIndex].valueLength = 0;
						}
					}
				}
				if (unicode == 61U)
				{
					attributeFlag = 1;
				}
				if (attributeFlag == 0 && unicode == 32U)
				{
					if (isTagSet)
					{
						return false;
					}
					isTagSet = true;
					attributeFlag = 2;
					tagValueType = TagValueType.None;
					tagUnitType = TagUnitType.Pixels;
					attributeIndex++;
					TMP_Text.m_xmlAttribute[attributeIndex].nameHashCode = 0;
					TMP_Text.m_xmlAttribute[attributeIndex].valueType = TagValueType.None;
					TMP_Text.m_xmlAttribute[attributeIndex].unitType = TagUnitType.Pixels;
					TMP_Text.m_xmlAttribute[attributeIndex].valueHashCode = 0;
					TMP_Text.m_xmlAttribute[attributeIndex].valueStartIndex = 0;
					TMP_Text.m_xmlAttribute[attributeIndex].valueLength = 0;
				}
				if (attributeFlag == 0)
				{
					TMP_Text.m_xmlAttribute[attributeIndex].nameHashCode = ((TMP_Text.m_xmlAttribute[attributeIndex].nameHashCode << 5) + TMP_Text.m_xmlAttribute[attributeIndex].nameHashCode) ^ (int)TMP_TextUtilities.ToUpperFast((char)unicode);
				}
				if (attributeFlag == 2 && unicode == 32U)
				{
					attributeFlag = 0;
				}
				i++;
			}
			if (!isValidHtmlTag)
			{
				return false;
			}
			if (this.tag_NoParsing && TMP_Text.m_xmlAttribute[0].nameHashCode != -294095813)
			{
				return false;
			}
			if (TMP_Text.m_xmlAttribute[0].nameHashCode == -294095813)
			{
				this.tag_NoParsing = false;
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && tagCharCount == 4)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && tagCharCount == 5)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && tagCharCount == 7)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			if (TMP_Text.m_htmlTag[0] == '#' && tagCharCount == 9)
			{
				this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
				this.m_colorStack.Add(this.m_htmlColor);
				return true;
			}
			MarkupTag nameHashCode2 = (MarkupTag)TMP_Text.m_xmlAttribute[0].nameHashCode;
			if (nameHashCode2 <= MarkupTag.SLASH_STRIKETHROUGH)
			{
				if (nameHashCode2 <= MarkupTag.LINE_INDENT)
				{
					if (nameHashCode2 <= MarkupTag.SLASH_INDENT)
					{
						if (nameHashCode2 <= MarkupTag.SLASH_MARGIN)
						{
							if (nameHashCode2 <= MarkupTag.FONT_WEIGHT)
							{
								if (nameHashCode2 == MarkupTag.GRADIENT)
								{
									int gradientPresetHashCode = TMP_Text.m_xmlAttribute[0].valueHashCode;
									TMP_ColorGradient tempColorGradientPreset;
									if (MaterialReferenceManager.TryGetColorGradientPreset(gradientPresetHashCode, out tempColorGradientPreset))
									{
										this.m_colorGradientPreset = tempColorGradientPreset;
									}
									else
									{
										if (tempColorGradientPreset == null)
										{
											tempColorGradientPreset = Resources.Load<TMP_ColorGradient>(TMP_Settings.defaultColorGradientPresetsPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
										}
										if (tempColorGradientPreset == null)
										{
											return false;
										}
										MaterialReferenceManager.AddColorGradientPreset(gradientPresetHashCode, tempColorGradientPreset);
										this.m_colorGradientPreset = tempColorGradientPreset;
									}
									this.m_colorGradientPresetIsTinted = false;
									int j = 1;
									while (j < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[j].nameHashCode != 0)
									{
										if (TMP_Text.m_xmlAttribute[j].nameHashCode == 2960519)
										{
											this.m_colorGradientPresetIsTinted = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[j].valueStartIndex, TMP_Text.m_xmlAttribute[j].valueLength) != 0f;
										}
										j++;
									}
									this.m_colorGradientStack.Add(this.m_colorGradientPreset);
									return true;
								}
								if (nameHashCode2 != MarkupTag.FONT_WEIGHT)
								{
									return false;
								}
								float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
								if (value == -32768f)
								{
									return false;
								}
								int num7 = (int)value;
								if (num7 <= 400)
								{
									if (num7 <= 200)
									{
										if (num7 != 100)
										{
											if (num7 == 200)
											{
												this.m_FontWeightInternal = FontWeight.ExtraLight;
											}
										}
										else
										{
											this.m_FontWeightInternal = FontWeight.Thin;
										}
									}
									else if (num7 != 300)
									{
										if (num7 == 400)
										{
											this.m_FontWeightInternal = FontWeight.Regular;
										}
									}
									else
									{
										this.m_FontWeightInternal = FontWeight.Light;
									}
								}
								else if (num7 <= 600)
								{
									if (num7 != 500)
									{
										if (num7 == 600)
										{
											this.m_FontWeightInternal = FontWeight.SemiBold;
										}
									}
									else
									{
										this.m_FontWeightInternal = FontWeight.Medium;
									}
								}
								else if (num7 != 700)
								{
									if (num7 != 800)
									{
										if (num7 == 900)
										{
											this.m_FontWeightInternal = FontWeight.Black;
										}
									}
									else
									{
										this.m_FontWeightInternal = FontWeight.Heavy;
									}
								}
								else
								{
									this.m_FontWeightInternal = FontWeight.Bold;
								}
								this.m_FontWeightStack.Add(this.m_FontWeightInternal);
								return true;
							}
							else
							{
								if (nameHashCode2 == MarkupTag.SLASH_GRADIENT)
								{
									this.m_colorGradientPreset = this.m_colorGradientStack.Remove();
									return true;
								}
								if (nameHashCode2 == MarkupTag.ACTION)
								{
									int actionID = TMP_Text.m_xmlAttribute[0].valueHashCode;
									if (this.m_isTextLayoutPhase)
									{
										this.m_actionStack.Add(actionID);
										global::UnityEngine.Debug.Log("Action ID: [" + actionID.ToString() + "] First character index: " + this.m_characterCount.ToString());
									}
									return true;
								}
								if (nameHashCode2 != MarkupTag.SLASH_MARGIN)
								{
									return false;
								}
								this.m_marginLeft = 0f;
								this.m_marginRight = 0f;
								return true;
							}
						}
						else if (nameHashCode2 <= MarkupTag.CHARACTER_SPACE)
						{
							if (nameHashCode2 == MarkupTag.SLASH_MONOSPACE)
							{
								this.m_monoSpacing = 0f;
								this.m_duoSpace = false;
								return true;
							}
							if (nameHashCode2 != MarkupTag.CHARACTER_SPACE)
							{
								return false;
							}
							float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
							if (value == -32768f)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_cSpacing = value * (this.m_isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_cSpacing = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
								break;
							case TagUnitType.Percentage:
								return false;
							}
							return true;
						}
						else if (nameHashCode2 != MarkupTag.INDENT)
						{
							if (nameHashCode2 == MarkupTag.LOWERCASE)
							{
								this.m_FontStyleInternal |= FontStyles.LowerCase;
								this.m_fontStyleStack.Add(FontStyles.LowerCase);
								return true;
							}
							if (nameHashCode2 != MarkupTag.SLASH_INDENT)
							{
								return false;
							}
							this.tag_Indent = this.m_indentStack.Remove();
							return true;
						}
						else
						{
							float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
							if (value == -32768f)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.tag_Indent = value * (this.m_isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.tag_Indent = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
								break;
							case TagUnitType.Percentage:
								this.tag_Indent = this.m_marginWidth * value / 100f;
								break;
							}
							this.m_indentStack.Add(this.tag_Indent);
							this.m_xAdvance = this.tag_Indent;
							return true;
						}
					}
					else if (nameHashCode2 <= MarkupTag.SLASH_ACTION)
					{
						if (nameHashCode2 <= MarkupTag.SLASH_CHARACTER_SPACE)
						{
							if (nameHashCode2 == MarkupTag.SLASH_LOWERCASE)
							{
								if ((this.m_fontStyle & FontStyles.LowerCase) != FontStyles.LowerCase && this.m_fontStyleStack.Remove(FontStyles.LowerCase) == 0)
								{
									this.m_FontStyleInternal &= ~FontStyles.LowerCase;
								}
								return true;
							}
							if (nameHashCode2 != MarkupTag.SLASH_CHARACTER_SPACE)
							{
								return false;
							}
							if (!this.m_isTextLayoutPhase)
							{
								return true;
							}
							if (this.m_characterCount > 0)
							{
								this.m_xAdvance -= this.m_cSpacing;
								this.m_textInfo.characterInfo[this.m_characterCount - 1].xAdvance = this.m_xAdvance;
							}
							this.m_cSpacing = 0f;
							return true;
						}
						else if (nameHashCode2 != MarkupTag.MARGIN)
						{
							if (nameHashCode2 != MarkupTag.MONOSPACE)
							{
								if (nameHashCode2 != MarkupTag.SLASH_ACTION)
								{
									return false;
								}
								if (this.m_isTextLayoutPhase)
								{
									global::UnityEngine.Debug.Log("Action ID: [" + this.m_actionStack.CurrentItem().ToString() + "] Last character index: " + (this.m_characterCount - 1).ToString());
								}
								this.m_actionStack.Remove();
								return true;
							}
							else
							{
								float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
								if (value == -32768f)
								{
									return false;
								}
								switch (TMP_Text.m_xmlAttribute[0].unitType)
								{
								case TagUnitType.Pixels:
									this.m_monoSpacing = value * (this.m_isOrthographic ? 1f : 0.1f);
									break;
								case TagUnitType.FontUnits:
									this.m_monoSpacing = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
									break;
								case TagUnitType.Percentage:
									return false;
								}
								if (TMP_Text.m_xmlAttribute[1].nameHashCode == 582810522)
								{
									this.m_duoSpace = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength) != 0f;
								}
								return true;
							}
						}
						else
						{
							TagValueType valueType = TMP_Text.m_xmlAttribute[0].valueType;
							float value;
							if (valueType == TagValueType.None)
							{
								int k = 1;
								while (k < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[k].nameHashCode != 0)
								{
									MarkupTag markupTag = (MarkupTag)TMP_Text.m_xmlAttribute[k].nameHashCode;
									if (markupTag != MarkupTag.LEFT)
									{
										if (markupTag == MarkupTag.RIGHT)
										{
											value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[k].valueStartIndex, TMP_Text.m_xmlAttribute[k].valueLength);
											if (value == -32768f)
											{
												return false;
											}
											switch (TMP_Text.m_xmlAttribute[k].unitType)
											{
											case TagUnitType.Pixels:
												this.m_marginRight = value * (this.m_isOrthographic ? 1f : 0.1f);
												break;
											case TagUnitType.FontUnits:
												this.m_marginRight = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
												break;
											case TagUnitType.Percentage:
												this.m_marginRight = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * value / 100f;
												break;
											}
											this.m_marginRight = ((this.m_marginRight >= 0f) ? this.m_marginRight : 0f);
										}
									}
									else
									{
										value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[k].valueStartIndex, TMP_Text.m_xmlAttribute[k].valueLength);
										if (value == -32768f)
										{
											return false;
										}
										switch (TMP_Text.m_xmlAttribute[k].unitType)
										{
										case TagUnitType.Pixels:
											this.m_marginLeft = value * (this.m_isOrthographic ? 1f : 0.1f);
											break;
										case TagUnitType.FontUnits:
											this.m_marginLeft = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
											break;
										case TagUnitType.Percentage:
											this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * value / 100f;
											break;
										}
										this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
									}
									k++;
								}
								return true;
							}
							if (valueType != TagValueType.NumericalValue)
							{
								return false;
							}
							value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
							if (value == -32768f)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_marginLeft = value * (this.m_isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_marginLeft = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
								break;
							case TagUnitType.Percentage:
								this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * value / 100f;
								break;
							}
							this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
							this.m_marginRight = this.m_marginLeft;
							return true;
						}
					}
					else if (nameHashCode2 <= MarkupTag.ROTATE)
					{
						if (nameHashCode2 == MarkupTag.SLASH_MATERIAL)
						{
							MaterialReference materialReference = TMP_Text.m_materialReferenceStack.Remove();
							this.m_currentMaterial = materialReference.material;
							this.m_currentMaterialIndex = materialReference.index;
							return true;
						}
						if (nameHashCode2 != MarkupTag.ROTATE)
						{
							return false;
						}
						float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
						if (value == -32768f)
						{
							return false;
						}
						this.m_FXRotation = Quaternion.Euler(0f, 0f, value);
						return true;
					}
					else if (nameHashCode2 != MarkupTag.SPRITE)
					{
						if (nameHashCode2 == MarkupTag.SLASH_TABLE)
						{
							return false;
						}
						if (nameHashCode2 != MarkupTag.LINE_INDENT)
						{
							return false;
						}
						float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
						if (value == -32768f)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.tag_LineIndent = value * (this.m_isOrthographic ? 1f : 0.1f);
							break;
						case TagUnitType.FontUnits:
							this.tag_LineIndent = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
							break;
						case TagUnitType.Percentage:
							this.tag_LineIndent = this.m_marginWidth * value / 100f;
							break;
						}
						this.m_xAdvance += this.tag_LineIndent;
						return true;
					}
					else
					{
						int spriteAssetHashCode = TMP_Text.m_xmlAttribute[0].valueHashCode;
						this.m_spriteIndex = -1;
						TMP_SpriteAsset tempSpriteAsset;
						if (TMP_Text.m_xmlAttribute[0].valueType == TagValueType.None || TMP_Text.m_xmlAttribute[0].valueType == TagValueType.NumericalValue)
						{
							if (this.m_spriteAsset != null)
							{
								this.m_currentSpriteAsset = this.m_spriteAsset;
							}
							else if (this.m_defaultSpriteAsset != null)
							{
								this.m_currentSpriteAsset = this.m_defaultSpriteAsset;
							}
							else if (this.m_defaultSpriteAsset == null)
							{
								if (TMP_Settings.defaultSpriteAsset != null)
								{
									this.m_defaultSpriteAsset = TMP_Settings.defaultSpriteAsset;
								}
								else
								{
									this.m_defaultSpriteAsset = Resources.Load<TMP_SpriteAsset>("Sprite Assets/Default Sprite Asset");
								}
								this.m_currentSpriteAsset = this.m_defaultSpriteAsset;
							}
							if (this.m_currentSpriteAsset == null)
							{
								return false;
							}
						}
						else if (MaterialReferenceManager.TryGetSpriteAsset(spriteAssetHashCode, out tempSpriteAsset))
						{
							this.m_currentSpriteAsset = tempSpriteAsset;
						}
						else
						{
							if (tempSpriteAsset == null)
							{
								Func<int, string, TMP_SpriteAsset> onSpriteAssetRequest = TMP_Text.OnSpriteAssetRequest;
								tempSpriteAsset = ((onSpriteAssetRequest != null) ? onSpriteAssetRequest(spriteAssetHashCode, new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength)) : null);
								if (tempSpriteAsset == null)
								{
									tempSpriteAsset = Resources.Load<TMP_SpriteAsset>(TMP_Settings.defaultSpriteAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
								}
							}
							if (tempSpriteAsset == null)
							{
								return false;
							}
							MaterialReferenceManager.AddSpriteAsset(spriteAssetHashCode, tempSpriteAsset);
							this.m_currentSpriteAsset = tempSpriteAsset;
						}
						if (TMP_Text.m_xmlAttribute[0].valueType == TagValueType.NumericalValue)
						{
							int index = (int)this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
							if (index == -32768)
							{
								return false;
							}
							if (index > this.m_currentSpriteAsset.spriteCharacterTable.Count - 1)
							{
								return false;
							}
							this.m_spriteIndex = index;
						}
						this.m_spriteColor = TMP_Text.s_colorWhite;
						this.m_tintSprite = false;
						int l = 0;
						while (l < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[l].nameHashCode != 0)
						{
							int nameHashCode = TMP_Text.m_xmlAttribute[l].nameHashCode;
							int index2 = 0;
							MarkupTag markupTag = (MarkupTag)nameHashCode;
							if (markupTag <= MarkupTag.NAME)
							{
								if (markupTag != MarkupTag.ANIM)
								{
									if (markupTag != MarkupTag.NAME)
									{
										goto IL_2E79;
									}
									this.m_currentSpriteAsset = TMP_SpriteAsset.SearchForSpriteByHashCode(this.m_currentSpriteAsset, TMP_Text.m_xmlAttribute[l].valueHashCode, true, out index2);
									if (index2 == -1)
									{
										return false;
									}
									this.m_spriteIndex = index2;
								}
								else
								{
									if (this.GetAttributeParameters(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[l].valueStartIndex, TMP_Text.m_xmlAttribute[l].valueLength, ref TMP_Text.m_attributeParameterValues) != 3)
									{
										return false;
									}
									this.m_spriteIndex = (int)TMP_Text.m_attributeParameterValues[0];
									if (this.m_isTextLayoutPhase)
									{
										this.spriteAnimator.DoSpriteAnimation(this.m_characterCount, this.m_currentSpriteAsset, this.m_spriteIndex, (int)TMP_Text.m_attributeParameterValues[1], (int)TMP_Text.m_attributeParameterValues[2]);
									}
								}
							}
							else if (markupTag != MarkupTag.TINT)
							{
								if (markupTag != MarkupTag.COLOR)
								{
									if (markupTag != MarkupTag.INDEX)
									{
										goto IL_2E79;
									}
									index2 = (int)this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
									if (index2 == -32768)
									{
										return false;
									}
									if (index2 > this.m_currentSpriteAsset.spriteCharacterTable.Count - 1)
									{
										return false;
									}
									this.m_spriteIndex = index2;
								}
								else
								{
									this.m_spriteColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[l].valueStartIndex, TMP_Text.m_xmlAttribute[l].valueLength);
								}
							}
							else
							{
								this.m_tintSprite = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[l].valueStartIndex, TMP_Text.m_xmlAttribute[l].valueLength) != 0f;
							}
							IL_2E84:
							l++;
							continue;
							IL_2E79:
							if (nameHashCode != -991527447)
							{
								return false;
							}
							goto IL_2E84;
						}
						if (this.m_spriteIndex == -1)
						{
							return false;
						}
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentSpriteAsset.material, this.m_currentSpriteAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						this.m_textElementType = TMP_TextElementType.Sprite;
						return true;
					}
				}
				else
				{
					if (nameHashCode2 <= MarkupTag.MARGIN_LEFT)
					{
						if (nameHashCode2 <= MarkupTag.SLASH_FONT_WEIGHT)
						{
							if (nameHashCode2 <= MarkupTag.SLASH_ALLCAPS)
							{
								if (nameHashCode2 != MarkupTag.LINE_HEIGHT)
								{
									if (nameHashCode2 != MarkupTag.SLASH_ALLCAPS)
									{
										return false;
									}
								}
								else
								{
									float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
									if (value == -32768f)
									{
										return false;
									}
									switch (tagUnitType)
									{
									case TagUnitType.Pixels:
										this.m_lineHeight = value * (this.m_isOrthographic ? 1f : 0.1f);
										break;
									case TagUnitType.FontUnits:
										this.m_lineHeight = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
										break;
									case TagUnitType.Percentage:
									{
										float fontScale = this.m_currentFontSize / this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
										this.m_lineHeight = this.m_fontAsset.faceInfo.lineHeight * value / 100f * fontScale;
										break;
									}
									}
									return true;
								}
							}
							else
							{
								if (nameHashCode2 == MarkupTag.SMALLCAPS)
								{
									this.m_FontStyleInternal |= FontStyles.SmallCaps;
									this.m_fontStyleStack.Add(FontStyles.SmallCaps);
									return true;
								}
								if (nameHashCode2 == MarkupTag.SLASH_ROTATE)
								{
									this.m_FXRotation = Quaternion.identity;
									return true;
								}
								if (nameHashCode2 != MarkupTag.SLASH_FONT_WEIGHT)
								{
									return false;
								}
								this.m_FontWeightStack.Remove();
								if (this.m_FontStyleInternal == FontStyles.Bold)
								{
									this.m_FontWeightInternal = FontWeight.Bold;
								}
								else
								{
									this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
								}
								return true;
							}
						}
						else if (nameHashCode2 <= MarkupTag.MARGIN_RIGHT)
						{
							if (nameHashCode2 != MarkupTag.SLASH_UPPERCASE)
							{
								if (nameHashCode2 != MarkupTag.MARGIN_RIGHT)
								{
									return false;
								}
								float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
								if (value == -32768f)
								{
									return false;
								}
								switch (tagUnitType)
								{
								case TagUnitType.Pixels:
									this.m_marginRight = value * (this.m_isOrthographic ? 1f : 0.1f);
									break;
								case TagUnitType.FontUnits:
									this.m_marginRight = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
									break;
								case TagUnitType.Percentage:
									this.m_marginRight = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * value / 100f;
									break;
								}
								this.m_marginRight = ((this.m_marginRight >= 0f) ? this.m_marginRight : 0f);
								return true;
							}
						}
						else
						{
							if (nameHashCode2 == MarkupTag.NO_PARSE)
							{
								this.tag_NoParsing = true;
								return true;
							}
							if (nameHashCode2 == MarkupTag.UPPERCASE)
							{
								goto IL_2F2C;
							}
							if (nameHashCode2 != MarkupTag.MARGIN_LEFT)
							{
								return false;
							}
							float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
							if (value == -32768f)
							{
								return false;
							}
							switch (tagUnitType)
							{
							case TagUnitType.Pixels:
								this.m_marginLeft = value * (this.m_isOrthographic ? 1f : 0.1f);
								break;
							case TagUnitType.FontUnits:
								this.m_marginLeft = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
								break;
							case TagUnitType.Percentage:
								this.m_marginLeft = (this.m_marginWidth - ((this.m_width != -1f) ? this.m_width : 0f)) * value / 100f;
								break;
							}
							this.m_marginLeft = ((this.m_marginLeft >= 0f) ? this.m_marginLeft : 0f);
							return true;
						}
						if ((this.m_fontStyle & FontStyles.UpperCase) != FontStyles.UpperCase && this.m_fontStyleStack.Remove(FontStyles.UpperCase) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.UpperCase;
						}
						return true;
					}
					if (nameHashCode2 <= MarkupTag.STRIKETHROUGH)
					{
						if (nameHashCode2 <= MarkupTag.A)
						{
							if (nameHashCode2 == MarkupTag.SLASH_VERTICAL_OFFSET)
							{
								this.m_baselineOffset = 0f;
								return true;
							}
							if (nameHashCode2 != MarkupTag.A)
							{
								return false;
							}
							if (this.m_isTextLayoutPhase && !this.m_isCalculatingPreferredValues && TMP_Text.m_xmlAttribute[1].nameHashCode == 2535353)
							{
								int index3 = this.m_textInfo.linkCount;
								if (index3 + 1 > this.m_textInfo.linkInfo.Length)
								{
									TMP_TextInfo.Resize<TMP_LinkInfo>(ref this.m_textInfo.linkInfo, index3 + 1);
								}
								this.m_textInfo.linkInfo[index3].textComponent = this;
								this.m_textInfo.linkInfo[index3].hashCode = 2535353;
								this.m_textInfo.linkInfo[index3].linkTextfirstCharacterIndex = this.m_characterCount;
								this.m_textInfo.linkInfo[index3].SetLinkID(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
							}
							return true;
						}
						else
						{
							if (nameHashCode2 == MarkupTag.BOLD)
							{
								this.m_FontStyleInternal |= FontStyles.Bold;
								this.m_fontStyleStack.Add(FontStyles.Bold);
								this.m_FontWeightInternal = FontWeight.Bold;
								return true;
							}
							if (nameHashCode2 == MarkupTag.ITALIC)
							{
								this.m_FontStyleInternal |= FontStyles.Italic;
								this.m_fontStyleStack.Add(FontStyles.Italic);
								if (TMP_Text.m_xmlAttribute[1].nameHashCode == 75347905)
								{
									this.m_ItalicAngle = (int)this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
									if (this.m_ItalicAngle < -180 || this.m_ItalicAngle > 180)
									{
										return false;
									}
								}
								else
								{
									this.m_ItalicAngle = (int)this.m_currentFontAsset.italicStyle;
								}
								this.m_ItalicAngleStack.Add(this.m_ItalicAngle);
								return true;
							}
							if (nameHashCode2 != MarkupTag.STRIKETHROUGH)
							{
								return false;
							}
							this.m_FontStyleInternal |= FontStyles.Strikethrough;
							this.m_fontStyleStack.Add(FontStyles.Strikethrough);
							if (TMP_Text.m_xmlAttribute[1].nameHashCode == 81999901)
							{
								this.m_strikethroughColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
								this.m_strikethroughColor.a = ((this.m_htmlColor.a < this.m_strikethroughColor.a) ? this.m_htmlColor.a : this.m_strikethroughColor.a);
							}
							else
							{
								this.m_strikethroughColor = this.m_htmlColor;
							}
							this.m_strikethroughColorStack.Add(this.m_strikethroughColor);
							return true;
						}
					}
					else if (nameHashCode2 <= MarkupTag.SLASH_BOLD)
					{
						if (nameHashCode2 == MarkupTag.UNDERLINE)
						{
							this.m_FontStyleInternal |= FontStyles.Underline;
							this.m_fontStyleStack.Add(FontStyles.Underline);
							if (TMP_Text.m_xmlAttribute[1].nameHashCode == 81999901)
							{
								this.m_underlineColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength);
								this.m_underlineColor.a = ((this.m_htmlColor.a < this.m_underlineColor.a) ? this.m_htmlColor.a : this.m_underlineColor.a);
							}
							else
							{
								this.m_underlineColor = this.m_htmlColor;
							}
							this.m_underlineColorStack.Add(this.m_underlineColor);
							return true;
						}
						if (nameHashCode2 == MarkupTag.SLASH_ITALIC)
						{
							if ((this.m_fontStyle & FontStyles.Italic) != FontStyles.Italic)
							{
								this.m_ItalicAngle = this.m_ItalicAngleStack.Remove();
								if (this.m_fontStyleStack.Remove(FontStyles.Italic) == 0)
								{
									this.m_FontStyleInternal &= ~FontStyles.Italic;
								}
							}
							return true;
						}
						if (nameHashCode2 != MarkupTag.SLASH_BOLD)
						{
							return false;
						}
						if ((this.m_fontStyle & FontStyles.Bold) != FontStyles.Bold && this.m_fontStyleStack.Remove(FontStyles.Bold) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.Bold;
							this.m_FontWeightInternal = this.m_FontWeightStack.Peek();
						}
						return true;
					}
					else
					{
						if (nameHashCode2 == MarkupTag.SLASH_A)
						{
							if (this.m_isTextLayoutPhase && !this.m_isCalculatingPreferredValues)
							{
								int index4 = this.m_textInfo.linkCount;
								this.m_textInfo.linkInfo[index4].linkTextLength = this.m_characterCount - this.m_textInfo.linkInfo[index4].linkTextfirstCharacterIndex;
								this.m_textInfo.linkCount++;
							}
							return true;
						}
						if (nameHashCode2 == MarkupTag.SLASH_UNDERLINE)
						{
							if ((this.m_fontStyle & FontStyles.Underline) != FontStyles.Underline && this.m_fontStyleStack.Remove(FontStyles.Underline) == 0)
							{
								this.m_FontStyleInternal &= ~FontStyles.Underline;
							}
							this.m_underlineColor = this.m_underlineColorStack.Remove();
							return true;
						}
						if (nameHashCode2 != MarkupTag.SLASH_STRIKETHROUGH)
						{
							return false;
						}
						if ((this.m_fontStyle & FontStyles.Strikethrough) != FontStyles.Strikethrough && this.m_fontStyleStack.Remove(FontStyles.Strikethrough) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.Strikethrough;
						}
						this.m_strikethroughColor = this.m_strikethroughColorStack.Remove();
						return true;
					}
				}
			}
			else if (nameHashCode2 <= MarkupTag.SLASH_SIZE)
			{
				if (nameHashCode2 <= MarkupTag.PAGE)
				{
					if (nameHashCode2 <= MarkupTag.SLASH_SUPERSCRIPT)
					{
						if (nameHashCode2 <= MarkupTag.SUBSCRIPT)
						{
							if (nameHashCode2 != MarkupTag.POSITION)
							{
								if (nameHashCode2 != MarkupTag.SUBSCRIPT)
								{
									return false;
								}
								this.m_fontScaleMultiplier *= ((this.m_currentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.subscriptSize : 1f);
								this.m_baselineOffsetStack.Push(this.m_baselineOffset);
								float fontScale = this.m_currentFontSize / this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								this.m_baselineOffset += this.m_currentFontAsset.faceInfo.subscriptOffset * fontScale * this.m_fontScaleMultiplier;
								this.m_fontStyleStack.Add(FontStyles.Subscript);
								this.m_FontStyleInternal |= FontStyles.Subscript;
								return true;
							}
							else
							{
								float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
								if (value == -32768f)
								{
									return false;
								}
								switch (tagUnitType)
								{
								case TagUnitType.Pixels:
									this.m_xAdvance = value * (this.m_isOrthographic ? 1f : 0.1f);
									return true;
								case TagUnitType.FontUnits:
									this.m_xAdvance = value * this.m_currentFontSize * (this.m_isOrthographic ? 1f : 0.1f);
									return true;
								case TagUnitType.Percentage:
									this.m_xAdvance = this.m_marginWidth * value / 100f;
									return true;
								default:
									return false;
								}
							}
						}
						else
						{
							if (nameHashCode2 == MarkupTag.SUPERSCRIPT)
							{
								this.m_fontScaleMultiplier *= ((this.m_currentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.superscriptSize : 1f);
								this.m_baselineOffsetStack.Push(this.m_baselineOffset);
								float fontScale = this.m_currentFontSize / this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								this.m_baselineOffset += this.m_currentFontAsset.faceInfo.superscriptOffset * fontScale * this.m_fontScaleMultiplier;
								this.m_fontStyleStack.Add(FontStyles.Superscript);
								this.m_FontStyleInternal |= FontStyles.Superscript;
								return true;
							}
							if (nameHashCode2 == MarkupTag.SLASH_SUBSCRIPT)
							{
								if ((this.m_FontStyleInternal & FontStyles.Subscript) == FontStyles.Subscript)
								{
									if (this.m_fontScaleMultiplier < 1f)
									{
										this.m_baselineOffset = this.m_baselineOffsetStack.Pop();
										this.m_fontScaleMultiplier /= ((this.m_currentFontAsset.faceInfo.subscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.subscriptSize : 1f);
									}
									if (this.m_fontStyleStack.Remove(FontStyles.Subscript) == 0)
									{
										this.m_FontStyleInternal &= ~FontStyles.Subscript;
									}
								}
								return true;
							}
							if (nameHashCode2 != MarkupTag.SLASH_SUPERSCRIPT)
							{
								return false;
							}
							if ((this.m_FontStyleInternal & FontStyles.Superscript) == FontStyles.Superscript)
							{
								if (this.m_fontScaleMultiplier < 1f)
								{
									this.m_baselineOffset = this.m_baselineOffsetStack.Pop();
									this.m_fontScaleMultiplier /= ((this.m_currentFontAsset.faceInfo.superscriptSize > 0f) ? this.m_currentFontAsset.faceInfo.superscriptSize : 1f);
								}
								if (this.m_fontStyleStack.Remove(FontStyles.Superscript) == 0)
								{
									this.m_FontStyleInternal &= ~FontStyles.Superscript;
								}
							}
							return true;
						}
					}
					else if (nameHashCode2 <= MarkupTag.FONT)
					{
						if (nameHashCode2 == MarkupTag.SLASH_POSITION)
						{
							this.m_isIgnoringAlignment = false;
							return true;
						}
						if (nameHashCode2 != MarkupTag.FONT)
						{
							return false;
						}
						int fontHashCode = TMP_Text.m_xmlAttribute[0].valueHashCode;
						int materialAttributeHashCode = TMP_Text.m_xmlAttribute[1].nameHashCode;
						int materialHashCode = TMP_Text.m_xmlAttribute[1].valueHashCode;
						if (fontHashCode == -620974005)
						{
							this.m_currentFontAsset = TMP_Text.m_materialReferences[0].fontAsset;
							this.m_currentMaterial = TMP_Text.m_materialReferences[0].material;
							this.m_currentMaterialIndex = 0;
							TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[0]);
							return true;
						}
						TMP_FontAsset tempFont;
						MaterialReferenceManager.TryGetFontAsset(fontHashCode, out tempFont);
						if (tempFont == null)
						{
							Func<int, string, TMP_FontAsset> onFontAssetRequest = TMP_Text.OnFontAssetRequest;
							tempFont = ((onFontAssetRequest != null) ? onFontAssetRequest(fontHashCode, new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength)) : null);
							if (tempFont == null)
							{
								tempFont = Resources.Load<TMP_FontAsset>(TMP_Settings.defaultFontAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
							}
							if (tempFont == null)
							{
								return false;
							}
							MaterialReferenceManager.AddFontAsset(tempFont);
						}
						if (materialAttributeHashCode == 0 && materialHashCode == 0)
						{
							this.m_currentMaterial = tempFont.material;
							this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tempFont, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
							TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
						}
						else
						{
							if (materialAttributeHashCode != 825491659)
							{
								return false;
							}
							Material tempMaterial;
							if (MaterialReferenceManager.TryGetMaterial(materialHashCode, out tempMaterial))
							{
								this.m_currentMaterial = tempMaterial;
								this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tempFont, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
								TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
							}
							else
							{
								tempMaterial = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[1].valueStartIndex, TMP_Text.m_xmlAttribute[1].valueLength));
								if (tempMaterial == null)
								{
									return false;
								}
								MaterialReferenceManager.AddFontMaterial(materialHashCode, tempMaterial);
								this.m_currentMaterial = tempMaterial;
								this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, tempFont, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
								TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
							}
						}
						this.m_currentFontAsset = tempFont;
						return true;
					}
					else
					{
						if (nameHashCode2 == MarkupTag.LINK)
						{
							if (this.m_isTextLayoutPhase && !this.m_isCalculatingPreferredValues)
							{
								int index5 = this.m_textInfo.linkCount;
								if (index5 + 1 > this.m_textInfo.linkInfo.Length)
								{
									TMP_TextInfo.Resize<TMP_LinkInfo>(ref this.m_textInfo.linkInfo, index5 + 1);
								}
								this.m_textInfo.linkInfo[index5].textComponent = this;
								this.m_textInfo.linkInfo[index5].hashCode = TMP_Text.m_xmlAttribute[0].valueHashCode;
								this.m_textInfo.linkInfo[index5].linkTextfirstCharacterIndex = this.m_characterCount;
								this.m_textInfo.linkInfo[index5].linkIdFirstCharacterIndex = startIndex + TMP_Text.m_xmlAttribute[0].valueStartIndex;
								this.m_textInfo.linkInfo[index5].linkIdLength = TMP_Text.m_xmlAttribute[0].valueLength;
								this.m_textInfo.linkInfo[index5].SetLinkID(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
							}
							return true;
						}
						if (nameHashCode2 == MarkupTag.MARK)
						{
							this.m_FontStyleInternal |= FontStyles.Highlight;
							this.m_fontStyleStack.Add(FontStyles.Highlight);
							Color32 highlightColor = new Color32(byte.MaxValue, byte.MaxValue, 0, 64);
							TMP_Offset highlightPadding = TMP_Offset.zero;
							int m = 0;
							while (m < TMP_Text.m_xmlAttribute.Length && TMP_Text.m_xmlAttribute[m].nameHashCode != 0)
							{
								MarkupTag markupTag = (MarkupTag)TMP_Text.m_xmlAttribute[m].nameHashCode;
								if (markupTag != MarkupTag.PADDING)
								{
									if (markupTag != MarkupTag.MARK)
									{
										if (markupTag == MarkupTag.COLOR)
										{
											highlightColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[m].valueStartIndex, TMP_Text.m_xmlAttribute[m].valueLength);
										}
									}
									else if (TMP_Text.m_xmlAttribute[m].valueType == TagValueType.ColorValue)
									{
										highlightColor = this.HexCharsToColor(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
									}
								}
								else
								{
									if (this.GetAttributeParameters(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[m].valueStartIndex, TMP_Text.m_xmlAttribute[m].valueLength, ref TMP_Text.m_attributeParameterValues) != 4)
									{
										return false;
									}
									highlightPadding = new TMP_Offset(TMP_Text.m_attributeParameterValues[0], TMP_Text.m_attributeParameterValues[1], TMP_Text.m_attributeParameterValues[2], TMP_Text.m_attributeParameterValues[3]);
									highlightPadding *= this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
								}
								m++;
							}
							highlightColor.a = ((this.m_htmlColor.a < highlightColor.a) ? this.m_htmlColor.a : highlightColor.a);
							this.m_HighlightState = new HighlightState(highlightColor, highlightPadding);
							this.m_HighlightStateStack.Push(this.m_HighlightState);
							return true;
						}
						if (nameHashCode2 != MarkupTag.PAGE)
						{
							return false;
						}
						if (this.m_overflowMode == TextOverflowModes.Page)
						{
							this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
							this.m_lineOffset = 0f;
							this.m_pageNumber++;
							this.m_isNewPage = true;
						}
						return true;
					}
				}
				else if (nameHashCode2 <= MarkupTag.TH)
				{
					if (nameHashCode2 <= MarkupTag.SIZE)
					{
						if (nameHashCode2 == MarkupTag.NO_BREAK)
						{
							this.m_isNonBreakingSpace = true;
							return true;
						}
						if (nameHashCode2 != MarkupTag.SIZE)
						{
							return false;
						}
						float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
						if (value == -32768f)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							if (TMP_Text.m_htmlTag[5] == '+')
							{
								this.m_currentFontSize = this.m_fontSize + value;
								this.m_sizeStack.Add(this.m_currentFontSize);
								return true;
							}
							if (TMP_Text.m_htmlTag[5] == '-')
							{
								this.m_currentFontSize = this.m_fontSize + value;
								this.m_sizeStack.Add(this.m_currentFontSize);
								return true;
							}
							this.m_currentFontSize = value;
							this.m_sizeStack.Add(this.m_currentFontSize);
							return true;
						case TagUnitType.FontUnits:
							this.m_currentFontSize = this.m_fontSize * value;
							this.m_sizeStack.Add(this.m_currentFontSize);
							return true;
						case TagUnitType.Percentage:
							this.m_currentFontSize = this.m_fontSize * value / 100f;
							this.m_sizeStack.Add(this.m_currentFontSize);
							return true;
						default:
							return false;
						}
					}
					else
					{
						if (nameHashCode2 == MarkupTag.TR)
						{
							return false;
						}
						if (nameHashCode2 == MarkupTag.TD)
						{
							return false;
						}
						if (nameHashCode2 != MarkupTag.TH)
						{
							return false;
						}
						return false;
					}
				}
				else if (nameHashCode2 <= MarkupTag.SLASH_MARK)
				{
					if (nameHashCode2 == MarkupTag.SLASH_NO_BREAK)
					{
						this.m_isNonBreakingSpace = false;
						return true;
					}
					if (nameHashCode2 != MarkupTag.SLASH_MARK)
					{
						return false;
					}
					if ((this.m_fontStyle & FontStyles.Highlight) != FontStyles.Highlight)
					{
						this.m_HighlightStateStack.Remove();
						this.m_HighlightState = this.m_HighlightStateStack.current;
						if (this.m_fontStyleStack.Remove(FontStyles.Highlight) == 0)
						{
							this.m_FontStyleInternal &= ~FontStyles.Highlight;
						}
					}
					return true;
				}
				else
				{
					if (nameHashCode2 == MarkupTag.SLASH_LINK)
					{
						if (this.m_isTextLayoutPhase && !this.m_isCalculatingPreferredValues && this.m_textInfo.linkCount < this.m_textInfo.linkInfo.Length)
						{
							this.m_textInfo.linkInfo[this.m_textInfo.linkCount].linkTextLength = this.m_characterCount - this.m_textInfo.linkInfo[this.m_textInfo.linkCount].linkTextfirstCharacterIndex;
							this.m_textInfo.linkCount++;
						}
						return true;
					}
					if (nameHashCode2 == MarkupTag.SLASH_FONT)
					{
						MaterialReference materialReference2 = TMP_Text.m_materialReferenceStack.Remove();
						this.m_currentFontAsset = materialReference2.fontAsset;
						this.m_currentMaterial = materialReference2.material;
						this.m_currentMaterialIndex = materialReference2.index;
						return true;
					}
					if (nameHashCode2 != MarkupTag.SLASH_SIZE)
					{
						return false;
					}
					this.m_currentFontSize = this.m_sizeStack.Remove();
					return true;
				}
			}
			else if (nameHashCode2 <= MarkupTag.SLASH_TH)
			{
				if (nameHashCode2 <= MarkupTag.SLASH_LINE_INDENT)
				{
					if (nameHashCode2 <= MarkupTag.ALPHA)
					{
						if (nameHashCode2 == MarkupTag.ALIGN)
						{
							MarkupTag markupTag = (MarkupTag)TMP_Text.m_xmlAttribute[0].valueHashCode;
							if (markupTag <= MarkupTag.LEFT)
							{
								if (markupTag == MarkupTag.CENTER)
								{
									this.m_lineJustification = HorizontalAlignmentOptions.Center;
									this.m_lineJustificationStack.Add(this.m_lineJustification);
									return true;
								}
								if (markupTag == MarkupTag.LEFT)
								{
									this.m_lineJustification = HorizontalAlignmentOptions.Left;
									this.m_lineJustificationStack.Add(this.m_lineJustification);
									return true;
								}
							}
							else
							{
								if (markupTag == MarkupTag.FLUSH)
								{
									this.m_lineJustification = HorizontalAlignmentOptions.Flush;
									this.m_lineJustificationStack.Add(this.m_lineJustification);
									return true;
								}
								if (markupTag == MarkupTag.RIGHT)
								{
									this.m_lineJustification = HorizontalAlignmentOptions.Right;
									this.m_lineJustificationStack.Add(this.m_lineJustification);
									return true;
								}
								if (markupTag == MarkupTag.JUSTIFIED)
								{
									this.m_lineJustification = HorizontalAlignmentOptions.Justified;
									this.m_lineJustificationStack.Add(this.m_lineJustification);
									return true;
								}
							}
							return false;
						}
						if (nameHashCode2 != MarkupTag.ALPHA)
						{
							return false;
						}
						if (TMP_Text.m_xmlAttribute[0].valueLength != 3)
						{
							return false;
						}
						this.m_htmlColor.a = (byte)(this.HexToInt(TMP_Text.m_htmlTag[7]) * 16U + this.HexToInt(TMP_Text.m_htmlTag[8]));
						return true;
					}
					else if (nameHashCode2 != MarkupTag.COLOR)
					{
						if (nameHashCode2 == MarkupTag.CLASS)
						{
							return false;
						}
						if (nameHashCode2 != MarkupTag.SLASH_LINE_INDENT)
						{
							return false;
						}
						this.tag_LineIndent = 0f;
						return true;
					}
					else
					{
						if (TMP_Text.m_htmlTag[6] == '#' && tagCharCount == 10)
						{
							this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (TMP_Text.m_htmlTag[6] == '#' && tagCharCount == 11)
						{
							this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (TMP_Text.m_htmlTag[6] == '#' && tagCharCount == 13)
						{
							this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						if (TMP_Text.m_htmlTag[6] == '#' && tagCharCount == 15)
						{
							this.m_htmlColor = this.HexCharsToColor(TMP_Text.m_htmlTag, tagCharCount);
							this.m_colorStack.Add(this.m_htmlColor);
							return true;
						}
						int num7 = TMP_Text.m_xmlAttribute[0].valueHashCode;
						if (num7 <= 2457214)
						{
							if (num7 <= -1108587920)
							{
								if (num7 == -1250222130)
								{
									this.m_htmlColor = new Color32(160, 32, 240, byte.MaxValue);
									this.m_colorStack.Add(this.m_htmlColor);
									return true;
								}
								if (num7 == -1108587920)
								{
									this.m_htmlColor = new Color32(byte.MaxValue, 128, 0, byte.MaxValue);
									this.m_colorStack.Add(this.m_htmlColor);
									return true;
								}
							}
							else
							{
								if (num7 == -882444668)
								{
									this.m_htmlColor = Color.yellow;
									this.m_colorStack.Add(this.m_htmlColor);
									return true;
								}
								if (num7 == 91635)
								{
									this.m_htmlColor = Color.red;
									this.m_colorStack.Add(this.m_htmlColor);
									return true;
								}
								if (num7 == 2457214)
								{
									this.m_htmlColor = Color.blue;
									this.m_colorStack.Add(this.m_htmlColor);
									return true;
								}
							}
						}
						else if (num7 <= 81074727)
						{
							if (num7 == 2638345)
							{
								this.m_htmlColor = new Color32(128, 128, 128, byte.MaxValue);
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num7 == 81074727)
							{
								this.m_htmlColor = Color.black;
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
						}
						else
						{
							if (num7 == 87065851)
							{
								this.m_htmlColor = Color.green;
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num7 == 105680263)
							{
								this.m_htmlColor = Color.white;
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
							if (num7 == 341063360)
							{
								this.m_htmlColor = new Color32(173, 216, 230, byte.MaxValue);
								this.m_colorStack.Add(this.m_htmlColor);
								return true;
							}
						}
						return false;
					}
				}
				else if (nameHashCode2 <= MarkupTag.SCALE)
				{
					if (nameHashCode2 != MarkupTag.SPACE)
					{
						if (nameHashCode2 != MarkupTag.SCALE)
						{
							return false;
						}
						float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
						if (value == -32768f)
						{
							return false;
						}
						this.m_FXScale = new Vector3(value, 1f, 1f);
						return true;
					}
					else
					{
						float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
						if (value == -32768f)
						{
							return false;
						}
						switch (tagUnitType)
						{
						case TagUnitType.Pixels:
							this.m_xAdvance += value * (this.m_isOrthographic ? 1f : 0.1f);
							return true;
						case TagUnitType.FontUnits:
							this.m_xAdvance += value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
							return true;
						case TagUnitType.Percentage:
							return false;
						default:
							return false;
						}
					}
				}
				else if (nameHashCode2 != MarkupTag.WIDTH)
				{
					if (nameHashCode2 == MarkupTag.SLASH_TR)
					{
						return false;
					}
					if (nameHashCode2 != MarkupTag.SLASH_TH)
					{
						return false;
					}
					return false;
				}
				else
				{
					float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
					if (value == -32768f)
					{
						return false;
					}
					switch (tagUnitType)
					{
					case TagUnitType.Pixels:
						this.m_width = value * (this.m_isOrthographic ? 1f : 0.1f);
						break;
					case TagUnitType.FontUnits:
						return false;
					case TagUnitType.Percentage:
						this.m_width = this.m_marginWidth * value / 100f;
						break;
					}
					return true;
				}
			}
			else if (nameHashCode2 <= MarkupTag.TABLE)
			{
				if (nameHashCode2 <= MarkupTag.SLASH_SMALLCAPS)
				{
					if (nameHashCode2 == MarkupTag.SLASH_TD)
					{
						return false;
					}
					if (nameHashCode2 != MarkupTag.SLASH_SMALLCAPS)
					{
						return false;
					}
					if ((this.m_fontStyle & FontStyles.SmallCaps) != FontStyles.SmallCaps && this.m_fontStyleStack.Remove(FontStyles.SmallCaps) == 0)
					{
						this.m_FontStyleInternal &= ~FontStyles.SmallCaps;
					}
					return true;
				}
				else
				{
					if (nameHashCode2 == MarkupTag.SLASH_LINE_HEIGHT)
					{
						this.m_lineHeight = -32767f;
						return true;
					}
					if (nameHashCode2 != MarkupTag.ALLCAPS)
					{
						if (nameHashCode2 != MarkupTag.TABLE)
						{
							return false;
						}
						return false;
					}
				}
			}
			else if (nameHashCode2 <= MarkupTag.SLASH_ALIGN)
			{
				if (nameHashCode2 != MarkupTag.MATERIAL)
				{
					if (nameHashCode2 == MarkupTag.SLASH_COLOR)
					{
						this.m_htmlColor = this.m_colorStack.Remove();
						return true;
					}
					if (nameHashCode2 != MarkupTag.SLASH_ALIGN)
					{
						return false;
					}
					this.m_lineJustification = this.m_lineJustificationStack.Remove();
					return true;
				}
				else
				{
					int materialHashCode = TMP_Text.m_xmlAttribute[0].valueHashCode;
					if (materialHashCode == -620974005)
					{
						this.m_currentMaterial = TMP_Text.m_materialReferences[0].material;
						this.m_currentMaterialIndex = 0;
						TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[0]);
						return true;
					}
					Material tempMaterial;
					if (MaterialReferenceManager.TryGetMaterial(materialHashCode, out tempMaterial))
					{
						this.m_currentMaterial = tempMaterial;
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
					}
					else
					{
						tempMaterial = Resources.Load<Material>(TMP_Settings.defaultFontAssetPath + new string(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength));
						if (tempMaterial == null)
						{
							return false;
						}
						MaterialReferenceManager.AddFontMaterial(materialHashCode, tempMaterial);
						this.m_currentMaterial = tempMaterial;
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						TMP_Text.m_materialReferenceStack.Add(TMP_Text.m_materialReferences[this.m_currentMaterialIndex]);
					}
					return true;
				}
			}
			else
			{
				if (nameHashCode2 == MarkupTag.SLASH_WIDTH)
				{
					this.m_width = -1f;
					return true;
				}
				if (nameHashCode2 == MarkupTag.SLASH_SCALE)
				{
					this.m_FXScale = Vector3.one;
					return true;
				}
				if (nameHashCode2 != MarkupTag.VERTICAL_OFFSET)
				{
					return false;
				}
				float value = this.ConvertToFloat(TMP_Text.m_htmlTag, TMP_Text.m_xmlAttribute[0].valueStartIndex, TMP_Text.m_xmlAttribute[0].valueLength);
				if (value == -32768f)
				{
					return false;
				}
				switch (tagUnitType)
				{
				case TagUnitType.Pixels:
					this.m_baselineOffset = value * (this.m_isOrthographic ? 1f : 0.1f);
					return true;
				case TagUnitType.FontUnits:
					this.m_baselineOffset = value * (this.m_isOrthographic ? 1f : 0.1f) * this.m_currentFontSize;
					return true;
				case TagUnitType.Percentage:
					return false;
				default:
					return false;
				}
			}
			IL_2F2C:
			this.m_FontStyleInternal |= FontStyles.UpperCase;
			this.m_fontStyleStack.Add(FontStyles.UpperCase);
			return true;
		}

		// Token: 0x04000433 RID: 1075
		[SerializeField]
		[TextArea(5, 10)]
		protected string m_text;

		// Token: 0x04000434 RID: 1076
		private bool m_IsTextBackingStringDirty;

		// Token: 0x04000435 RID: 1077
		[SerializeField]
		protected ITextPreprocessor m_TextPreprocessor;

		// Token: 0x04000436 RID: 1078
		[SerializeField]
		protected bool m_isRightToLeft;

		// Token: 0x04000437 RID: 1079
		[SerializeField]
		protected TMP_FontAsset m_fontAsset;

		// Token: 0x04000438 RID: 1080
		protected TMP_FontAsset m_currentFontAsset;

		// Token: 0x04000439 RID: 1081
		protected bool m_isSDFShader;

		// Token: 0x0400043A RID: 1082
		[SerializeField]
		protected Material m_sharedMaterial;

		// Token: 0x0400043B RID: 1083
		protected Material m_currentMaterial;

		// Token: 0x0400043C RID: 1084
		protected static MaterialReference[] m_materialReferences = new MaterialReference[4];

		// Token: 0x0400043D RID: 1085
		protected static Dictionary<int, int> m_materialReferenceIndexLookup = new Dictionary<int, int>();

		// Token: 0x0400043E RID: 1086
		protected static TMP_TextProcessingStack<MaterialReference> m_materialReferenceStack = new TMP_TextProcessingStack<MaterialReference>(new MaterialReference[16]);

		// Token: 0x0400043F RID: 1087
		protected int m_currentMaterialIndex;

		// Token: 0x04000440 RID: 1088
		[SerializeField]
		protected Material[] m_fontSharedMaterials;

		// Token: 0x04000441 RID: 1089
		[SerializeField]
		protected Material m_fontMaterial;

		// Token: 0x04000442 RID: 1090
		[SerializeField]
		protected Material[] m_fontMaterials;

		// Token: 0x04000443 RID: 1091
		protected bool m_isMaterialDirty;

		// Token: 0x04000444 RID: 1092
		[SerializeField]
		protected Color32 m_fontColor32 = Color.white;

		// Token: 0x04000445 RID: 1093
		[SerializeField]
		protected Color m_fontColor = Color.white;

		// Token: 0x04000446 RID: 1094
		protected static Color32 s_colorWhite = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04000447 RID: 1095
		protected Color32 m_underlineColor = TMP_Text.s_colorWhite;

		// Token: 0x04000448 RID: 1096
		protected Color32 m_strikethroughColor = TMP_Text.s_colorWhite;

		// Token: 0x04000449 RID: 1097
		internal HighlightState m_HighlightState = new HighlightState(TMP_Text.s_colorWhite, TMP_Offset.zero);

		// Token: 0x0400044A RID: 1098
		internal bool m_ConvertToLinearSpace;

		// Token: 0x0400044B RID: 1099
		[SerializeField]
		protected bool m_enableVertexGradient;

		// Token: 0x0400044C RID: 1100
		[SerializeField]
		protected ColorMode m_colorMode = ColorMode.FourCornersGradient;

		// Token: 0x0400044D RID: 1101
		[SerializeField]
		protected VertexGradient m_fontColorGradient = new VertexGradient(Color.white);

		// Token: 0x0400044E RID: 1102
		[SerializeField]
		protected TMP_ColorGradient m_fontColorGradientPreset;

		// Token: 0x0400044F RID: 1103
		[SerializeField]
		protected TMP_SpriteAsset m_spriteAsset;

		// Token: 0x04000450 RID: 1104
		[SerializeField]
		protected bool m_tintAllSprites;

		// Token: 0x04000451 RID: 1105
		protected bool m_tintSprite;

		// Token: 0x04000452 RID: 1106
		protected Color32 m_spriteColor;

		// Token: 0x04000453 RID: 1107
		[SerializeField]
		protected TMP_StyleSheet m_StyleSheet;

		// Token: 0x04000454 RID: 1108
		internal TMP_Style m_TextStyle;

		// Token: 0x04000455 RID: 1109
		[SerializeField]
		protected int m_TextStyleHashCode;

		// Token: 0x04000456 RID: 1110
		[SerializeField]
		protected bool m_overrideHtmlColors;

		// Token: 0x04000457 RID: 1111
		[SerializeField]
		protected Color32 m_faceColor = Color.white;

		// Token: 0x04000458 RID: 1112
		protected Color32 m_outlineColor = Color.black;

		// Token: 0x04000459 RID: 1113
		protected float m_outlineWidth;

		// Token: 0x0400045A RID: 1114
		protected Vector3 m_currentEnvMapRotation;

		// Token: 0x0400045B RID: 1115
		protected bool m_hasEnvMapProperty;

		// Token: 0x0400045C RID: 1116
		[SerializeField]
		protected float m_fontSize = -99f;

		// Token: 0x0400045D RID: 1117
		protected float m_currentFontSize;

		// Token: 0x0400045E RID: 1118
		[SerializeField]
		protected float m_fontSizeBase = 36f;

		// Token: 0x0400045F RID: 1119
		protected TMP_TextProcessingStack<float> m_sizeStack = new TMP_TextProcessingStack<float>(16);

		// Token: 0x04000460 RID: 1120
		[SerializeField]
		protected FontWeight m_fontWeight = FontWeight.Regular;

		// Token: 0x04000461 RID: 1121
		protected FontWeight m_FontWeightInternal = FontWeight.Regular;

		// Token: 0x04000462 RID: 1122
		protected TMP_TextProcessingStack<FontWeight> m_FontWeightStack = new TMP_TextProcessingStack<FontWeight>(8);

		// Token: 0x04000463 RID: 1123
		[SerializeField]
		protected bool m_enableAutoSizing;

		// Token: 0x04000464 RID: 1124
		protected float m_maxFontSize;

		// Token: 0x04000465 RID: 1125
		protected float m_minFontSize;

		// Token: 0x04000466 RID: 1126
		protected int m_AutoSizeIterationCount;

		// Token: 0x04000467 RID: 1127
		protected int m_AutoSizeMaxIterationCount = 100;

		// Token: 0x04000468 RID: 1128
		protected bool m_IsAutoSizePointSizeSet;

		// Token: 0x04000469 RID: 1129
		[SerializeField]
		protected float m_fontSizeMin;

		// Token: 0x0400046A RID: 1130
		[SerializeField]
		protected float m_fontSizeMax;

		// Token: 0x0400046B RID: 1131
		[SerializeField]
		protected FontStyles m_fontStyle;

		// Token: 0x0400046C RID: 1132
		protected FontStyles m_FontStyleInternal;

		// Token: 0x0400046D RID: 1133
		protected TMP_FontStyleStack m_fontStyleStack;

		// Token: 0x0400046E RID: 1134
		protected bool m_isUsingBold;

		// Token: 0x0400046F RID: 1135
		[SerializeField]
		protected HorizontalAlignmentOptions m_HorizontalAlignment = HorizontalAlignmentOptions.Left;

		// Token: 0x04000470 RID: 1136
		[SerializeField]
		protected VerticalAlignmentOptions m_VerticalAlignment = VerticalAlignmentOptions.Top;

		// Token: 0x04000471 RID: 1137
		[SerializeField]
		[FormerlySerializedAs("m_lineJustification")]
		protected TextAlignmentOptions m_textAlignment = TextAlignmentOptions.Converted;

		// Token: 0x04000472 RID: 1138
		protected HorizontalAlignmentOptions m_lineJustification;

		// Token: 0x04000473 RID: 1139
		protected TMP_TextProcessingStack<HorizontalAlignmentOptions> m_lineJustificationStack = new TMP_TextProcessingStack<HorizontalAlignmentOptions>(new HorizontalAlignmentOptions[16]);

		// Token: 0x04000474 RID: 1140
		protected Vector3[] m_textContainerLocalCorners = new Vector3[4];

		// Token: 0x04000475 RID: 1141
		[SerializeField]
		protected float m_characterSpacing;

		// Token: 0x04000476 RID: 1142
		protected float m_cSpacing;

		// Token: 0x04000477 RID: 1143
		protected float m_monoSpacing;

		// Token: 0x04000478 RID: 1144
		protected bool m_duoSpace;

		// Token: 0x04000479 RID: 1145
		[SerializeField]
		protected float m_wordSpacing;

		// Token: 0x0400047A RID: 1146
		[SerializeField]
		protected float m_lineSpacing;

		// Token: 0x0400047B RID: 1147
		protected float m_lineSpacingDelta;

		// Token: 0x0400047C RID: 1148
		protected float m_lineHeight = -32767f;

		// Token: 0x0400047D RID: 1149
		protected bool m_IsDrivenLineSpacing;

		// Token: 0x0400047E RID: 1150
		[SerializeField]
		protected float m_lineSpacingMax;

		// Token: 0x0400047F RID: 1151
		[SerializeField]
		protected float m_paragraphSpacing;

		// Token: 0x04000480 RID: 1152
		[SerializeField]
		protected float m_charWidthMaxAdj;

		// Token: 0x04000481 RID: 1153
		protected float m_charWidthAdjDelta;

		// Token: 0x04000482 RID: 1154
		[SerializeField]
		[FormerlySerializedAs("m_enableWordWrapping")]
		protected TextWrappingModes m_TextWrappingMode;

		// Token: 0x04000483 RID: 1155
		protected bool m_isCharacterWrappingEnabled;

		// Token: 0x04000484 RID: 1156
		protected bool m_isNonBreakingSpace;

		// Token: 0x04000485 RID: 1157
		protected bool m_isIgnoringAlignment;

		// Token: 0x04000486 RID: 1158
		[SerializeField]
		protected float m_wordWrappingRatios = 0.4f;

		// Token: 0x04000487 RID: 1159
		[SerializeField]
		protected TextOverflowModes m_overflowMode;

		// Token: 0x04000488 RID: 1160
		protected int m_firstOverflowCharacterIndex = -1;

		// Token: 0x04000489 RID: 1161
		[SerializeField]
		protected TMP_Text m_linkedTextComponent;

		// Token: 0x0400048A RID: 1162
		[SerializeField]
		internal TMP_Text parentLinkedComponent;

		// Token: 0x0400048B RID: 1163
		protected bool m_isTextTruncated;

		// Token: 0x0400048C RID: 1164
		[SerializeField]
		protected bool m_enableKerning;

		// Token: 0x0400048D RID: 1165
		protected int m_LastBaseGlyphIndex;

		// Token: 0x0400048E RID: 1166
		[SerializeField]
		protected List<OTL_FeatureTag> m_ActiveFontFeatures = new List<OTL_FeatureTag> { (OTL_FeatureTag)0U };

		// Token: 0x0400048F RID: 1167
		[SerializeField]
		protected bool m_enableExtraPadding;

		// Token: 0x04000490 RID: 1168
		[SerializeField]
		protected bool checkPaddingRequired;

		// Token: 0x04000491 RID: 1169
		[SerializeField]
		protected bool m_isRichText = true;

		// Token: 0x04000492 RID: 1170
		[SerializeField]
		private bool m_EmojiFallbackSupport = true;

		// Token: 0x04000493 RID: 1171
		[SerializeField]
		protected bool m_parseCtrlCharacters = true;

		// Token: 0x04000494 RID: 1172
		protected bool m_isOverlay;

		// Token: 0x04000495 RID: 1173
		[SerializeField]
		protected bool m_isOrthographic;

		// Token: 0x04000496 RID: 1174
		[SerializeField]
		protected bool m_isCullingEnabled;

		// Token: 0x04000497 RID: 1175
		protected bool m_isMaskingEnabled;

		// Token: 0x04000498 RID: 1176
		protected bool isMaskUpdateRequired;

		// Token: 0x04000499 RID: 1177
		protected bool m_ignoreCulling = true;

		// Token: 0x0400049A RID: 1178
		[SerializeField]
		protected TextureMappingOptions m_horizontalMapping;

		// Token: 0x0400049B RID: 1179
		[SerializeField]
		protected TextureMappingOptions m_verticalMapping;

		// Token: 0x0400049C RID: 1180
		[SerializeField]
		protected float m_uvLineOffset;

		// Token: 0x0400049D RID: 1181
		protected TextRenderFlags m_renderMode = TextRenderFlags.Render;

		// Token: 0x0400049E RID: 1182
		[SerializeField]
		protected VertexSortingOrder m_geometrySortingOrder;

		// Token: 0x0400049F RID: 1183
		[SerializeField]
		protected bool m_IsTextObjectScaleStatic;

		// Token: 0x040004A0 RID: 1184
		[SerializeField]
		protected bool m_VertexBufferAutoSizeReduction;

		// Token: 0x040004A1 RID: 1185
		protected int m_firstVisibleCharacter;

		// Token: 0x040004A2 RID: 1186
		protected int m_maxVisibleCharacters = 99999;

		// Token: 0x040004A3 RID: 1187
		protected int m_maxVisibleWords = 99999;

		// Token: 0x040004A4 RID: 1188
		protected int m_maxVisibleLines = 99999;

		// Token: 0x040004A5 RID: 1189
		[SerializeField]
		protected bool m_useMaxVisibleDescender = true;

		// Token: 0x040004A6 RID: 1190
		[SerializeField]
		protected int m_pageToDisplay = 1;

		// Token: 0x040004A7 RID: 1191
		protected bool m_isNewPage;

		// Token: 0x040004A8 RID: 1192
		[SerializeField]
		protected Vector4 m_margin = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x040004A9 RID: 1193
		protected float m_marginLeft;

		// Token: 0x040004AA RID: 1194
		protected float m_marginRight;

		// Token: 0x040004AB RID: 1195
		protected float m_marginWidth;

		// Token: 0x040004AC RID: 1196
		protected float m_marginHeight;

		// Token: 0x040004AD RID: 1197
		protected float m_width = -1f;

		// Token: 0x040004AE RID: 1198
		protected TMP_TextInfo m_textInfo;

		// Token: 0x040004AF RID: 1199
		protected bool m_havePropertiesChanged;

		// Token: 0x040004B0 RID: 1200
		[SerializeField]
		protected bool m_isUsingLegacyAnimationComponent;

		// Token: 0x040004B1 RID: 1201
		protected Transform m_transform;

		// Token: 0x040004B2 RID: 1202
		protected RectTransform m_rectTransform;

		// Token: 0x040004B3 RID: 1203
		protected Vector2 m_PreviousRectTransformSize;

		// Token: 0x040004B4 RID: 1204
		protected Vector2 m_PreviousPivotPosition;

		// Token: 0x040004B6 RID: 1206
		protected bool m_autoSizeTextContainer;

		// Token: 0x040004B7 RID: 1207
		protected Mesh m_mesh;

		// Token: 0x040004B8 RID: 1208
		[SerializeField]
		protected bool m_isVolumetricText;

		// Token: 0x040004BD RID: 1213
		protected TMP_SpriteAnimator m_spriteAnimator;

		// Token: 0x040004BE RID: 1214
		protected float m_flexibleHeight = -1f;

		// Token: 0x040004BF RID: 1215
		protected float m_flexibleWidth = -1f;

		// Token: 0x040004C0 RID: 1216
		protected float m_minWidth;

		// Token: 0x040004C1 RID: 1217
		protected float m_minHeight;

		// Token: 0x040004C2 RID: 1218
		protected float m_maxWidth;

		// Token: 0x040004C3 RID: 1219
		protected float m_maxHeight;

		// Token: 0x040004C4 RID: 1220
		protected LayoutElement m_LayoutElement;

		// Token: 0x040004C5 RID: 1221
		protected float m_preferredWidth;

		// Token: 0x040004C6 RID: 1222
		protected float m_RenderedWidth;

		// Token: 0x040004C7 RID: 1223
		protected bool m_isPreferredWidthDirty;

		// Token: 0x040004C8 RID: 1224
		protected float m_preferredHeight;

		// Token: 0x040004C9 RID: 1225
		protected float m_RenderedHeight;

		// Token: 0x040004CA RID: 1226
		protected bool m_isPreferredHeightDirty;

		// Token: 0x040004CB RID: 1227
		protected bool m_isCalculatingPreferredValues;

		// Token: 0x040004CC RID: 1228
		protected int m_layoutPriority;

		// Token: 0x040004CD RID: 1229
		protected bool m_isLayoutDirty;

		// Token: 0x040004CE RID: 1230
		protected bool m_isAwake;

		// Token: 0x040004CF RID: 1231
		internal bool m_isWaitingOnResourceLoad;

		// Token: 0x040004D0 RID: 1232
		internal TMP_Text.TextInputSources m_inputSource;

		// Token: 0x040004D1 RID: 1233
		protected float m_fontScaleMultiplier;

		// Token: 0x040004D2 RID: 1234
		private static char[] m_htmlTag = new char[128];

		// Token: 0x040004D3 RID: 1235
		private static RichTextTagAttribute[] m_xmlAttribute = new RichTextTagAttribute[8];

		// Token: 0x040004D4 RID: 1236
		private static float[] m_attributeParameterValues = new float[16];

		// Token: 0x040004D5 RID: 1237
		protected float tag_LineIndent;

		// Token: 0x040004D6 RID: 1238
		protected float tag_Indent;

		// Token: 0x040004D7 RID: 1239
		protected TMP_TextProcessingStack<float> m_indentStack = new TMP_TextProcessingStack<float>(new float[16]);

		// Token: 0x040004D8 RID: 1240
		protected bool tag_NoParsing;

		// Token: 0x040004D9 RID: 1241
		protected bool m_isTextLayoutPhase;

		// Token: 0x040004DA RID: 1242
		protected Quaternion m_FXRotation;

		// Token: 0x040004DB RID: 1243
		protected Vector3 m_FXScale;

		// Token: 0x040004DC RID: 1244
		internal TMP_Text.TextProcessingElement[] m_TextProcessingArray = new TMP_Text.TextProcessingElement[8];

		// Token: 0x040004DD RID: 1245
		internal int m_InternalTextProcessingArraySize;

		// Token: 0x040004DE RID: 1246
		private TMP_CharacterInfo[] m_internalCharacterInfo;

		// Token: 0x040004DF RID: 1247
		protected int m_totalCharacterCount;

		// Token: 0x040004E0 RID: 1248
		internal static WordWrapState m_SavedWordWrapState = default(WordWrapState);

		// Token: 0x040004E1 RID: 1249
		internal static WordWrapState m_SavedLineState = default(WordWrapState);

		// Token: 0x040004E2 RID: 1250
		internal static WordWrapState m_SavedEllipsisState = default(WordWrapState);

		// Token: 0x040004E3 RID: 1251
		internal static WordWrapState m_SavedLastValidState = default(WordWrapState);

		// Token: 0x040004E4 RID: 1252
		internal static WordWrapState m_SavedSoftLineBreakState = default(WordWrapState);

		// Token: 0x040004E5 RID: 1253
		internal static TMP_TextProcessingStack<WordWrapState> m_EllipsisInsertionCandidateStack = new TMP_TextProcessingStack<WordWrapState>(8, 8);

		// Token: 0x040004E6 RID: 1254
		protected int m_characterCount;

		// Token: 0x040004E7 RID: 1255
		protected int m_firstCharacterOfLine;

		// Token: 0x040004E8 RID: 1256
		protected int m_firstVisibleCharacterOfLine;

		// Token: 0x040004E9 RID: 1257
		protected int m_lastCharacterOfLine;

		// Token: 0x040004EA RID: 1258
		protected int m_lastVisibleCharacterOfLine;

		// Token: 0x040004EB RID: 1259
		protected int m_lineNumber;

		// Token: 0x040004EC RID: 1260
		protected int m_lineVisibleCharacterCount;

		// Token: 0x040004ED RID: 1261
		protected int m_lineVisibleSpaceCount;

		// Token: 0x040004EE RID: 1262
		protected int m_pageNumber;

		// Token: 0x040004EF RID: 1263
		protected float m_PageAscender;

		// Token: 0x040004F0 RID: 1264
		protected float m_maxTextAscender;

		// Token: 0x040004F1 RID: 1265
		protected float m_maxCapHeight;

		// Token: 0x040004F2 RID: 1266
		protected float m_ElementAscender;

		// Token: 0x040004F3 RID: 1267
		protected float m_ElementDescender;

		// Token: 0x040004F4 RID: 1268
		protected float m_maxLineAscender;

		// Token: 0x040004F5 RID: 1269
		protected float m_maxLineDescender;

		// Token: 0x040004F6 RID: 1270
		protected float m_startOfLineAscender;

		// Token: 0x040004F7 RID: 1271
		protected float m_startOfLineDescender;

		// Token: 0x040004F8 RID: 1272
		protected float m_lineOffset;

		// Token: 0x040004F9 RID: 1273
		protected Extents m_meshExtents;

		// Token: 0x040004FA RID: 1274
		protected Color32 m_htmlColor = new Color(255f, 255f, 255f, 128f);

		// Token: 0x040004FB RID: 1275
		protected TMP_TextProcessingStack<Color32> m_colorStack = new TMP_TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x040004FC RID: 1276
		protected TMP_TextProcessingStack<Color32> m_underlineColorStack = new TMP_TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x040004FD RID: 1277
		protected TMP_TextProcessingStack<Color32> m_strikethroughColorStack = new TMP_TextProcessingStack<Color32>(new Color32[16]);

		// Token: 0x040004FE RID: 1278
		protected TMP_TextProcessingStack<HighlightState> m_HighlightStateStack = new TMP_TextProcessingStack<HighlightState>(new HighlightState[16]);

		// Token: 0x040004FF RID: 1279
		protected TMP_ColorGradient m_colorGradientPreset;

		// Token: 0x04000500 RID: 1280
		protected TMP_TextProcessingStack<TMP_ColorGradient> m_colorGradientStack = new TMP_TextProcessingStack<TMP_ColorGradient>(new TMP_ColorGradient[16]);

		// Token: 0x04000501 RID: 1281
		protected bool m_colorGradientPresetIsTinted;

		// Token: 0x04000502 RID: 1282
		protected float m_tabSpacing;

		// Token: 0x04000503 RID: 1283
		protected float m_spacing;

		// Token: 0x04000504 RID: 1284
		protected TMP_TextProcessingStack<int>[] m_TextStyleStacks = new TMP_TextProcessingStack<int>[8];

		// Token: 0x04000505 RID: 1285
		protected int m_TextStyleStackDepth;

		// Token: 0x04000506 RID: 1286
		protected TMP_TextProcessingStack<int> m_ItalicAngleStack = new TMP_TextProcessingStack<int>(new int[16]);

		// Token: 0x04000507 RID: 1287
		protected int m_ItalicAngle;

		// Token: 0x04000508 RID: 1288
		protected TMP_TextProcessingStack<int> m_actionStack = new TMP_TextProcessingStack<int>(new int[16]);

		// Token: 0x04000509 RID: 1289
		protected float m_padding;

		// Token: 0x0400050A RID: 1290
		protected float m_baselineOffset;

		// Token: 0x0400050B RID: 1291
		protected TMP_TextProcessingStack<float> m_baselineOffsetStack = new TMP_TextProcessingStack<float>(new float[16]);

		// Token: 0x0400050C RID: 1292
		protected float m_xAdvance;

		// Token: 0x0400050D RID: 1293
		protected TMP_TextElementType m_textElementType;

		// Token: 0x0400050E RID: 1294
		protected TMP_TextElement m_cached_TextElement;

		// Token: 0x0400050F RID: 1295
		protected TMP_Text.SpecialCharacter m_Ellipsis;

		// Token: 0x04000510 RID: 1296
		protected TMP_Text.SpecialCharacter m_Underline;

		// Token: 0x04000511 RID: 1297
		protected TMP_SpriteAsset m_defaultSpriteAsset;

		// Token: 0x04000512 RID: 1298
		protected TMP_SpriteAsset m_currentSpriteAsset;

		// Token: 0x04000513 RID: 1299
		protected int m_spriteCount;

		// Token: 0x04000514 RID: 1300
		protected int m_spriteIndex;

		// Token: 0x04000515 RID: 1301
		protected int m_spriteAnimationID;

		// Token: 0x04000516 RID: 1302
		private static ProfilerMarker k_ParseTextMarker = new ProfilerMarker("TMP Parse Text");

		// Token: 0x04000517 RID: 1303
		private static ProfilerMarker k_InsertNewLineMarker = new ProfilerMarker("TMP.InsertNewLine");

		// Token: 0x04000518 RID: 1304
		protected bool m_ignoreActiveState;

		// Token: 0x04000519 RID: 1305
		private TMP_Text.TextBackingContainer m_TextBackingArray = new TMP_Text.TextBackingContainer(4);

		// Token: 0x0400051A RID: 1306
		private readonly decimal[] k_Power = new decimal[] { 0.5m, 0.05m, 0.005m, 0.0005m, 0.00005m, 0.000005m, 0.0000005m, 0.00000005m, 0.000000005m, 0.0000000005m };

		// Token: 0x0400051B RID: 1307
		protected static Vector2 k_LargePositiveVector2 = new Vector2(2.1474836E+09f, 2.1474836E+09f);

		// Token: 0x0400051C RID: 1308
		protected static Vector2 k_LargeNegativeVector2 = new Vector2(-2.1474836E+09f, -2.1474836E+09f);

		// Token: 0x0400051D RID: 1309
		protected static float k_LargePositiveFloat = 32767f;

		// Token: 0x0400051E RID: 1310
		protected static float k_LargeNegativeFloat = -32767f;

		// Token: 0x0400051F RID: 1311
		protected static int k_LargePositiveInt = int.MaxValue;

		// Token: 0x04000520 RID: 1312
		protected static int k_LargeNegativeInt = -2147483647;

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x0600056C RID: 1388
		public delegate void MissingCharacterEventCallback(int unicode, int stringIndex, string text, TMP_FontAsset fontAsset, TMP_Text textComponent);

		// Token: 0x0200008D RID: 141
		protected struct CharacterSubstitution
		{
			// Token: 0x0600056F RID: 1391 RVA: 0x00022FC0 File Offset: 0x000211C0
			public CharacterSubstitution(int index, uint unicode)
			{
				this.index = index;
				this.unicode = unicode;
			}

			// Token: 0x04000521 RID: 1313
			public int index;

			// Token: 0x04000522 RID: 1314
			public uint unicode;
		}

		// Token: 0x0200008E RID: 142
		internal enum TextInputSources
		{
			// Token: 0x04000524 RID: 1316
			TextInputBox,
			// Token: 0x04000525 RID: 1317
			SetText,
			// Token: 0x04000526 RID: 1318
			SetTextArray,
			// Token: 0x04000527 RID: 1319
			TextString
		}

		// Token: 0x0200008F RID: 143
		[DebuggerDisplay("Unicode ({unicode})  '{(char)unicode}'")]
		internal struct TextProcessingElement
		{
			// Token: 0x04000528 RID: 1320
			public TextProcessingElementType elementType;

			// Token: 0x04000529 RID: 1321
			public uint unicode;

			// Token: 0x0400052A RID: 1322
			public int stringIndex;

			// Token: 0x0400052B RID: 1323
			public int length;
		}

		// Token: 0x02000090 RID: 144
		protected struct SpecialCharacter
		{
			// Token: 0x06000570 RID: 1392 RVA: 0x00022FD0 File Offset: 0x000211D0
			public SpecialCharacter(TMP_Character character, int materialIndex)
			{
				this.character = character;
				this.fontAsset = character.textAsset as TMP_FontAsset;
				this.material = ((this.fontAsset != null) ? this.fontAsset.material : null);
				this.materialIndex = materialIndex;
			}

			// Token: 0x0400052C RID: 1324
			public TMP_Character character;

			// Token: 0x0400052D RID: 1325
			public TMP_FontAsset fontAsset;

			// Token: 0x0400052E RID: 1326
			public Material material;

			// Token: 0x0400052F RID: 1327
			public int materialIndex;
		}

		// Token: 0x02000091 RID: 145
		private struct TextBackingContainer
		{
			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000571 RID: 1393 RVA: 0x0002301E File Offset: 0x0002121E
			public uint[] Text
			{
				get
				{
					return this.m_Array;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x00023026 File Offset: 0x00021226
			public int Capacity
			{
				get
				{
					return this.m_Array.Length;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000573 RID: 1395 RVA: 0x00023030 File Offset: 0x00021230
			// (set) Token: 0x06000574 RID: 1396 RVA: 0x00023038 File Offset: 0x00021238
			public int Count
			{
				get
				{
					return this.m_Index;
				}
				set
				{
					this.m_Index = value;
				}
			}

			// Token: 0x1700016A RID: 362
			public uint this[int index]
			{
				get
				{
					return this.m_Array[index];
				}
				set
				{
					if (index >= this.m_Array.Length)
					{
						this.Resize(index);
					}
					this.m_Array[index] = value;
				}
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00023068 File Offset: 0x00021268
			public TextBackingContainer(int size)
			{
				this.m_Array = new uint[size];
				this.m_Index = 0;
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x0002307D File Offset: 0x0002127D
			public void Resize(int size)
			{
				size = Mathf.NextPowerOfTwo(size + 1);
				Array.Resize<uint>(ref this.m_Array, size);
			}

			// Token: 0x04000530 RID: 1328
			private uint[] m_Array;

			// Token: 0x04000531 RID: 1329
			private int m_Index;
		}
	}
}
