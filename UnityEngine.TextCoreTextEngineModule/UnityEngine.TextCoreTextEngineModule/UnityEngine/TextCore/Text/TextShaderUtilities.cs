using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000067 RID: 103
	[ExcludeFromDocs]
	public static class TextShaderUtilities
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0002F93C File Offset: 0x0002DB3C
		internal static Shader ShaderRef_MobileSDF
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_MobileSDF == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field SSD");
					bool flag2 = TextShaderUtilities.k_ShaderRef_MobileSDF == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("Text/Mobile/Distance Field SSD");
					}
					bool flag3 = TextShaderUtilities.k_ShaderRef_MobileSDF == null;
					if (flag3)
					{
						TextShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("Hidden/TextCore/Distance Field SSD");
					}
				}
				return TextShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		internal static Shader ShaderRef_MobileSDF_IMGUI
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_MobileSDF_IMGUI == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_MobileSDF_IMGUI = Shader.Find("Text/Mobile/Distance Field SSD");
					bool flag2 = TextShaderUtilities.k_ShaderRef_MobileSDF_IMGUI == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_MobileSDF_IMGUI = Shader.Find("Hidden/TextCore/Distance Field SSD");
					}
				}
				return TextShaderUtilities.k_ShaderRef_MobileSDF_IMGUI;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0002FA00 File Offset: 0x0002DC00
		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_MobileBitmap == null;
				if (flag)
				{
					bool flag2 = TextShaderUtilities.k_ShaderRef_MobileBitmap == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("Text/Bitmap");
					}
					bool flag3 = TextShaderUtilities.k_ShaderRef_MobileBitmap == null;
					if (flag3)
					{
						TextShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("Hidden/Internal-GUITextureClipText");
					}
				}
				return TextShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0002FA64 File Offset: 0x0002DC64
		internal static Shader ShaderRef_Sprite
		{
			get
			{
				bool flag = TextShaderUtilities.k_ShaderRef_Sprite == null;
				if (flag)
				{
					TextShaderUtilities.k_ShaderRef_Sprite = Shader.Find("TextMeshPro/Sprite");
					bool flag2 = TextShaderUtilities.k_ShaderRef_Sprite == null;
					if (flag2)
					{
						TextShaderUtilities.k_ShaderRef_Sprite = Shader.Find("Text/Sprite");
					}
					bool flag3 = TextShaderUtilities.k_ShaderRef_Sprite == null;
					if (flag3)
					{
						TextShaderUtilities.k_ShaderRef_Sprite = Shader.Find("Hidden/TextCore/Sprite");
					}
				}
				return TextShaderUtilities.k_ShaderRef_Sprite;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0002FAD8 File Offset: 0x0002DCD8
		static TextShaderUtilities()
		{
			TextShaderUtilities.GetShaderPropertyIDs();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0002FB60 File Offset: 0x0002DD60
		internal static void GetShaderPropertyIDs()
		{
			bool flag = !TextShaderUtilities.isInitialized;
			if (flag)
			{
				TextShaderUtilities.isInitialized = true;
				TextShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				TextShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				TextShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				TextShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				TextShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				TextShaderUtilities.ID_OutlineOffset1 = Shader.PropertyToID("_OutlineOffset1");
				TextShaderUtilities.ID_OutlineOffset2 = Shader.PropertyToID("_OutlineOffset2");
				TextShaderUtilities.ID_OutlineOffset3 = Shader.PropertyToID("_OutlineOffset3");
				TextShaderUtilities.ID_OutlineMode = Shader.PropertyToID("_OutlineMode");
				TextShaderUtilities.ID_IsoPerimeter = Shader.PropertyToID("_IsoPerimeter");
				TextShaderUtilities.ID_Softness = Shader.PropertyToID("_Softness");
				TextShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				TextShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				TextShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				TextShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				TextShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				TextShaderUtilities.ID_UnderlayOffset = Shader.PropertyToID("_UnderlayOffset");
				TextShaderUtilities.ID_UnderlayIsoPerimeter = Shader.PropertyToID("_UnderlayIsoPerimeter");
				TextShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				TextShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				TextShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				TextShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				TextShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				TextShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				TextShaderUtilities.ID_Outline2Color = Shader.PropertyToID("_Outline2Color");
				TextShaderUtilities.ID_Outline2Width = Shader.PropertyToID("_Outline2Width");
				TextShaderUtilities.ID_Padding = Shader.PropertyToID("_Padding");
				TextShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				TextShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				TextShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				TextShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				TextShaderUtilities.ID_Sharpness = Shader.PropertyToID("_Sharpness");
				TextShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				TextShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				TextShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				TextShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				TextShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				TextShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				TextShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				TextShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				TextShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				TextShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				TextShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				TextShaderUtilities.ID_GlowInner = Shader.PropertyToID("_GlowInner");
				TextShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				TextShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				TextShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				TextShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				TextShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				TextShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				TextShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				TextShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				TextShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				TextShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				TextShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				TextShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				TextShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				TextShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				TextShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				TextShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
			}
		}

		// Token: 0x04000454 RID: 1108
		public static int ID_MainTex;

		// Token: 0x04000455 RID: 1109
		public static int ID_FaceTex;

		// Token: 0x04000456 RID: 1110
		public static int ID_FaceColor;

		// Token: 0x04000457 RID: 1111
		public static int ID_FaceDilate;

		// Token: 0x04000458 RID: 1112
		public static int ID_Shininess;

		// Token: 0x04000459 RID: 1113
		public static int ID_OutlineOffset1;

		// Token: 0x0400045A RID: 1114
		public static int ID_OutlineOffset2;

		// Token: 0x0400045B RID: 1115
		public static int ID_OutlineOffset3;

		// Token: 0x0400045C RID: 1116
		public static int ID_OutlineMode;

		// Token: 0x0400045D RID: 1117
		public static int ID_IsoPerimeter;

		// Token: 0x0400045E RID: 1118
		public static int ID_Softness;

		// Token: 0x0400045F RID: 1119
		public static int ID_UnderlayColor;

		// Token: 0x04000460 RID: 1120
		public static int ID_UnderlayOffsetX;

		// Token: 0x04000461 RID: 1121
		public static int ID_UnderlayOffsetY;

		// Token: 0x04000462 RID: 1122
		public static int ID_UnderlayDilate;

		// Token: 0x04000463 RID: 1123
		public static int ID_UnderlaySoftness;

		// Token: 0x04000464 RID: 1124
		public static int ID_UnderlayOffset;

		// Token: 0x04000465 RID: 1125
		public static int ID_UnderlayIsoPerimeter;

		// Token: 0x04000466 RID: 1126
		public static int ID_WeightNormal;

		// Token: 0x04000467 RID: 1127
		public static int ID_WeightBold;

		// Token: 0x04000468 RID: 1128
		public static int ID_OutlineTex;

		// Token: 0x04000469 RID: 1129
		public static int ID_OutlineWidth;

		// Token: 0x0400046A RID: 1130
		public static int ID_OutlineSoftness;

		// Token: 0x0400046B RID: 1131
		public static int ID_OutlineColor;

		// Token: 0x0400046C RID: 1132
		public static int ID_Outline2Color;

		// Token: 0x0400046D RID: 1133
		public static int ID_Outline2Width;

		// Token: 0x0400046E RID: 1134
		public static int ID_Padding;

		// Token: 0x0400046F RID: 1135
		public static int ID_GradientScale;

		// Token: 0x04000470 RID: 1136
		public static int ID_ScaleX;

		// Token: 0x04000471 RID: 1137
		public static int ID_ScaleY;

		// Token: 0x04000472 RID: 1138
		public static int ID_PerspectiveFilter;

		// Token: 0x04000473 RID: 1139
		public static int ID_Sharpness;

		// Token: 0x04000474 RID: 1140
		public static int ID_TextureWidth;

		// Token: 0x04000475 RID: 1141
		public static int ID_TextureHeight;

		// Token: 0x04000476 RID: 1142
		public static int ID_BevelAmount;

		// Token: 0x04000477 RID: 1143
		public static int ID_GlowColor;

		// Token: 0x04000478 RID: 1144
		public static int ID_GlowOffset;

		// Token: 0x04000479 RID: 1145
		public static int ID_GlowPower;

		// Token: 0x0400047A RID: 1146
		public static int ID_GlowOuter;

		// Token: 0x0400047B RID: 1147
		public static int ID_GlowInner;

		// Token: 0x0400047C RID: 1148
		public static int ID_LightAngle;

		// Token: 0x0400047D RID: 1149
		public static int ID_EnvMap;

		// Token: 0x0400047E RID: 1150
		public static int ID_EnvMatrix;

		// Token: 0x0400047F RID: 1151
		public static int ID_EnvMatrixRotation;

		// Token: 0x04000480 RID: 1152
		public static int ID_MaskCoord;

		// Token: 0x04000481 RID: 1153
		public static int ID_ClipRect;

		// Token: 0x04000482 RID: 1154
		public static int ID_MaskSoftnessX;

		// Token: 0x04000483 RID: 1155
		public static int ID_MaskSoftnessY;

		// Token: 0x04000484 RID: 1156
		public static int ID_VertexOffsetX;

		// Token: 0x04000485 RID: 1157
		public static int ID_VertexOffsetY;

		// Token: 0x04000486 RID: 1158
		public static int ID_UseClipRect;

		// Token: 0x04000487 RID: 1159
		public static int ID_StencilID;

		// Token: 0x04000488 RID: 1160
		public static int ID_StencilOp;

		// Token: 0x04000489 RID: 1161
		public static int ID_StencilComp;

		// Token: 0x0400048A RID: 1162
		public static int ID_StencilReadMask;

		// Token: 0x0400048B RID: 1163
		public static int ID_StencilWriteMask;

		// Token: 0x0400048C RID: 1164
		public static int ID_ShaderFlags;

		// Token: 0x0400048D RID: 1165
		public static int ID_ScaleRatio_A;

		// Token: 0x0400048E RID: 1166
		public static int ID_ScaleRatio_B;

		// Token: 0x0400048F RID: 1167
		public static int ID_ScaleRatio_C;

		// Token: 0x04000490 RID: 1168
		public static string Keyword_Bevel = "BEVEL_ON";

		// Token: 0x04000491 RID: 1169
		public static string Keyword_Glow = "GLOW_ON";

		// Token: 0x04000492 RID: 1170
		public static string Keyword_Underlay = "UNDERLAY_ON";

		// Token: 0x04000493 RID: 1171
		public static string Keyword_Ratios = "RATIOS_OFF";

		// Token: 0x04000494 RID: 1172
		public static string Keyword_MASK_SOFT = "MASK_SOFT";

		// Token: 0x04000495 RID: 1173
		public static string Keyword_MASK_HARD = "MASK_HARD";

		// Token: 0x04000496 RID: 1174
		public static string Keyword_MASK_TEX = "MASK_TEX";

		// Token: 0x04000497 RID: 1175
		public static string Keyword_Outline = "OUTLINE_ON";

		// Token: 0x04000498 RID: 1176
		public static string ShaderTag_ZTestMode = "unity_GUIZTestMode";

		// Token: 0x04000499 RID: 1177
		public static string ShaderTag_CullMode = "_CullMode";

		// Token: 0x0400049A RID: 1178
		private static float m_clamp = 1f;

		// Token: 0x0400049B RID: 1179
		public static bool isInitialized = false;

		// Token: 0x0400049C RID: 1180
		private static Shader k_ShaderRef_MobileSDF;

		// Token: 0x0400049D RID: 1181
		private static Shader k_ShaderRef_MobileSDF_IMGUI;

		// Token: 0x0400049E RID: 1182
		private static Shader k_ShaderRef_MobileBitmap;

		// Token: 0x0400049F RID: 1183
		private static Shader k_ShaderRef_Sprite;
	}
}
