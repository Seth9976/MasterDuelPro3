using System;
using System.Linq;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000072 RID: 114
	public static class ShaderUtilities
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00012848 File Offset: 0x00010A48
		internal static Shader ShaderRef_MobileSDF
		{
			get
			{
				if (ShaderUtilities.k_ShaderRef_MobileSDF == null)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				return ShaderUtilities.k_ShaderRef_MobileSDF;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001286B File Offset: 0x00010A6B
		internal static Shader ShaderRef_MobileBitmap
		{
			get
			{
				if (ShaderUtilities.k_ShaderRef_MobileBitmap == null)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
				return ShaderUtilities.k_ShaderRef_MobileBitmap;
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00012890 File Offset: 0x00010A90
		static ShaderUtilities()
		{
			ShaderUtilities.GetShaderPropertyIDs();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00012918 File Offset: 0x00010B18
		public static void GetShaderPropertyIDs()
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.isInitialized = true;
				ShaderUtilities.ID_MainTex = Shader.PropertyToID("_MainTex");
				ShaderUtilities.ID_FaceTex = Shader.PropertyToID("_FaceTex");
				ShaderUtilities.ID_FaceColor = Shader.PropertyToID("_FaceColor");
				ShaderUtilities.ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
				ShaderUtilities.ID_Shininess = Shader.PropertyToID("_FaceShininess");
				ShaderUtilities.ID_OutlineOffset1 = Shader.PropertyToID("_OutlineOffset1");
				ShaderUtilities.ID_OutlineOffset2 = Shader.PropertyToID("_OutlineOffset2");
				ShaderUtilities.ID_OutlineOffset3 = Shader.PropertyToID("_OutlineOffset3");
				ShaderUtilities.ID_OutlineMode = Shader.PropertyToID("_OutlineMode");
				ShaderUtilities.ID_IsoPerimeter = Shader.PropertyToID("_IsoPerimeter");
				ShaderUtilities.ID_Softness = Shader.PropertyToID("_Softness");
				ShaderUtilities.ID_UnderlayColor = Shader.PropertyToID("_UnderlayColor");
				ShaderUtilities.ID_UnderlayOffsetX = Shader.PropertyToID("_UnderlayOffsetX");
				ShaderUtilities.ID_UnderlayOffsetY = Shader.PropertyToID("_UnderlayOffsetY");
				ShaderUtilities.ID_UnderlayDilate = Shader.PropertyToID("_UnderlayDilate");
				ShaderUtilities.ID_UnderlaySoftness = Shader.PropertyToID("_UnderlaySoftness");
				ShaderUtilities.ID_UnderlayOffset = Shader.PropertyToID("_UnderlayOffset");
				ShaderUtilities.ID_UnderlayIsoPerimeter = Shader.PropertyToID("_UnderlayIsoPerimeter");
				ShaderUtilities.ID_WeightNormal = Shader.PropertyToID("_WeightNormal");
				ShaderUtilities.ID_WeightBold = Shader.PropertyToID("_WeightBold");
				ShaderUtilities.ID_OutlineTex = Shader.PropertyToID("_OutlineTex");
				ShaderUtilities.ID_OutlineWidth = Shader.PropertyToID("_OutlineWidth");
				ShaderUtilities.ID_OutlineSoftness = Shader.PropertyToID("_OutlineSoftness");
				ShaderUtilities.ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
				ShaderUtilities.ID_Outline2Color = Shader.PropertyToID("_Outline2Color");
				ShaderUtilities.ID_Outline2Width = Shader.PropertyToID("_Outline2Width");
				ShaderUtilities.ID_Padding = Shader.PropertyToID("_Padding");
				ShaderUtilities.ID_GradientScale = Shader.PropertyToID("_GradientScale");
				ShaderUtilities.ID_ScaleX = Shader.PropertyToID("_ScaleX");
				ShaderUtilities.ID_ScaleY = Shader.PropertyToID("_ScaleY");
				ShaderUtilities.ID_PerspectiveFilter = Shader.PropertyToID("_PerspectiveFilter");
				ShaderUtilities.ID_Sharpness = Shader.PropertyToID("_Sharpness");
				ShaderUtilities.ID_TextureWidth = Shader.PropertyToID("_TextureWidth");
				ShaderUtilities.ID_TextureHeight = Shader.PropertyToID("_TextureHeight");
				ShaderUtilities.ID_BevelAmount = Shader.PropertyToID("_Bevel");
				ShaderUtilities.ID_LightAngle = Shader.PropertyToID("_LightAngle");
				ShaderUtilities.ID_EnvMap = Shader.PropertyToID("_Cube");
				ShaderUtilities.ID_EnvMatrix = Shader.PropertyToID("_EnvMatrix");
				ShaderUtilities.ID_EnvMatrixRotation = Shader.PropertyToID("_EnvMatrixRotation");
				ShaderUtilities.ID_GlowColor = Shader.PropertyToID("_GlowColor");
				ShaderUtilities.ID_GlowOffset = Shader.PropertyToID("_GlowOffset");
				ShaderUtilities.ID_GlowPower = Shader.PropertyToID("_GlowPower");
				ShaderUtilities.ID_GlowOuter = Shader.PropertyToID("_GlowOuter");
				ShaderUtilities.ID_GlowInner = Shader.PropertyToID("_GlowInner");
				ShaderUtilities.ID_MaskCoord = Shader.PropertyToID("_MaskCoord");
				ShaderUtilities.ID_ClipRect = Shader.PropertyToID("_ClipRect");
				ShaderUtilities.ID_UseClipRect = Shader.PropertyToID("_UseClipRect");
				ShaderUtilities.ID_MaskSoftnessX = Shader.PropertyToID("_MaskSoftnessX");
				ShaderUtilities.ID_MaskSoftnessY = Shader.PropertyToID("_MaskSoftnessY");
				ShaderUtilities.ID_VertexOffsetX = Shader.PropertyToID("_VertexOffsetX");
				ShaderUtilities.ID_VertexOffsetY = Shader.PropertyToID("_VertexOffsetY");
				ShaderUtilities.ID_StencilID = Shader.PropertyToID("_Stencil");
				ShaderUtilities.ID_StencilOp = Shader.PropertyToID("_StencilOp");
				ShaderUtilities.ID_StencilComp = Shader.PropertyToID("_StencilComp");
				ShaderUtilities.ID_StencilReadMask = Shader.PropertyToID("_StencilReadMask");
				ShaderUtilities.ID_StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");
				ShaderUtilities.ID_ShaderFlags = Shader.PropertyToID("_ShaderFlags");
				ShaderUtilities.ID_ScaleRatio_A = Shader.PropertyToID("_ScaleRatioA");
				ShaderUtilities.ID_ScaleRatio_B = Shader.PropertyToID("_ScaleRatioB");
				ShaderUtilities.ID_ScaleRatio_C = Shader.PropertyToID("_ScaleRatioC");
				if (ShaderUtilities.k_ShaderRef_MobileSDF == null)
				{
					ShaderUtilities.k_ShaderRef_MobileSDF = Shader.Find("TextMeshPro/Mobile/Distance Field");
				}
				if (ShaderUtilities.k_ShaderRef_MobileBitmap == null)
				{
					ShaderUtilities.k_ShaderRef_MobileBitmap = Shader.Find("TextMeshPro/Mobile/Bitmap");
				}
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public static void UpdateShaderRatios(Material mat)
		{
			bool isRatioEnabled = !mat.shaderKeywords.Contains(ShaderUtilities.Keyword_Ratios);
			if (!mat.HasProperty(ShaderUtilities.ID_GradientScale) || !mat.HasProperty(ShaderUtilities.ID_FaceDilate))
			{
				return;
			}
			float scale = mat.GetFloat(ShaderUtilities.ID_GradientScale);
			float faceDilate = mat.GetFloat(ShaderUtilities.ID_FaceDilate);
			float outlineThickness = mat.GetFloat(ShaderUtilities.ID_OutlineWidth);
			float outlineSoftness = mat.GetFloat(ShaderUtilities.ID_OutlineSoftness);
			float weight = Mathf.Max(mat.GetFloat(ShaderUtilities.ID_WeightNormal), mat.GetFloat(ShaderUtilities.ID_WeightBold)) / 4f;
			float t = Mathf.Max(1f, weight + faceDilate + outlineThickness + outlineSoftness);
			float ratio_A = (isRatioEnabled ? ((scale - ShaderUtilities.m_clamp) / (scale * t)) : 1f);
			mat.SetFloat(ShaderUtilities.ID_ScaleRatio_A, ratio_A);
			if (mat.HasProperty(ShaderUtilities.ID_GlowOffset))
			{
				float glowOffset = mat.GetFloat(ShaderUtilities.ID_GlowOffset);
				float glowOuter = mat.GetFloat(ShaderUtilities.ID_GlowOuter);
				float range = (weight + faceDilate) * (scale - ShaderUtilities.m_clamp);
				t = Mathf.Max(1f, glowOffset + glowOuter);
				float ratio_B = (isRatioEnabled ? (Mathf.Max(0f, scale - ShaderUtilities.m_clamp - range) / (scale * t)) : 1f);
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_B, ratio_B);
			}
			if (mat.HasProperty(ShaderUtilities.ID_UnderlayOffsetX))
			{
				float underlayOffsetX = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetX);
				float underlayOffsetY = mat.GetFloat(ShaderUtilities.ID_UnderlayOffsetY);
				float underlayDilate = mat.GetFloat(ShaderUtilities.ID_UnderlayDilate);
				float underlaySoftness = mat.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
				float range2 = (weight + faceDilate) * (scale - ShaderUtilities.m_clamp);
				t = Mathf.Max(1f, Mathf.Max(Mathf.Abs(underlayOffsetX), Mathf.Abs(underlayOffsetY)) + underlayDilate + underlaySoftness);
				float ratio_C = (isRatioEnabled ? (Mathf.Max(0f, scale - ShaderUtilities.m_clamp - range2) / (scale * t)) : 1f);
				mat.SetFloat(ShaderUtilities.ID_ScaleRatio_C, ratio_C);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00012EFA File Offset: 0x000110FA
		public static Vector4 GetFontExtent(Material material)
		{
			return Vector4.zero;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00012F04 File Offset: 0x00011104
		public static bool IsMaskingEnabled(Material material)
		{
			return !(material == null) && material.HasProperty(ShaderUtilities.ID_ClipRect) && (material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_SOFT) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_HARD) || material.shaderKeywords.Contains(ShaderUtilities.Keyword_MASK_TEX));
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00012F64 File Offset: 0x00011164
		public static float GetPadding(Material material, bool enableExtraPadding, bool isBold)
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			if (material == null)
			{
				return 0f;
			}
			int extraPadding = (enableExtraPadding ? 4 : 0);
			if (!material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				if (material.HasProperty(ShaderUtilities.ID_Padding))
				{
					extraPadding += (int)material.GetFloat(ShaderUtilities.ID_Padding);
				}
				return (float)extraPadding + 1f;
			}
			if (material.HasProperty(ShaderUtilities.ID_IsoPerimeter))
			{
				return ShaderUtilities.ComputePaddingForProperties(material) + 0.25f + (float)extraPadding;
			}
			Vector4 padding = Vector4.zero;
			Vector4 maxPadding = Vector4.zero;
			float faceDilate = 0f;
			float faceSoftness = 0f;
			float outlineThickness = 0f;
			float scaleRatio_A = 0f;
			float scaleRatio_B = 0f;
			float scaleRatio_C = 0f;
			float glowOffset = 0f;
			float glowOuter = 0f;
			ShaderUtilities.UpdateShaderRatios(material);
			string[] shaderKeywords = material.shaderKeywords;
			if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_A))
			{
				scaleRatio_A = material.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
			}
			if (material.HasProperty(ShaderUtilities.ID_FaceDilate))
			{
				faceDilate = material.GetFloat(ShaderUtilities.ID_FaceDilate) * scaleRatio_A;
			}
			if (material.HasProperty(ShaderUtilities.ID_OutlineSoftness))
			{
				faceSoftness = material.GetFloat(ShaderUtilities.ID_OutlineSoftness) * scaleRatio_A;
			}
			if (material.HasProperty(ShaderUtilities.ID_OutlineWidth))
			{
				outlineThickness = material.GetFloat(ShaderUtilities.ID_OutlineWidth) * scaleRatio_A;
			}
			float uniformPadding = outlineThickness + faceSoftness + faceDilate;
			if (material.HasProperty(ShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(ShaderUtilities.Keyword_Glow))
			{
				if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_B))
				{
					scaleRatio_B = material.GetFloat(ShaderUtilities.ID_ScaleRatio_B);
				}
				glowOffset = material.GetFloat(ShaderUtilities.ID_GlowOffset) * scaleRatio_B;
				glowOuter = material.GetFloat(ShaderUtilities.ID_GlowOuter) * scaleRatio_B;
			}
			uniformPadding = Mathf.Max(uniformPadding, faceDilate + glowOffset + glowOuter);
			if (material.HasProperty(ShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(ShaderUtilities.Keyword_Underlay))
			{
				if (material.HasProperty(ShaderUtilities.ID_ScaleRatio_C))
				{
					scaleRatio_C = material.GetFloat(ShaderUtilities.ID_ScaleRatio_C);
				}
				float offsetX = 0f;
				float offsetY = 0f;
				float dilate = 0f;
				float softness = 0f;
				if (material.HasProperty(ShaderUtilities.ID_UnderlayOffset))
				{
					Vector2 vector = material.GetVector(ShaderUtilities.ID_UnderlayOffset);
					offsetX = vector.x;
					offsetY = vector.y;
					dilate = material.GetFloat(ShaderUtilities.ID_UnderlayDilate);
					softness = material.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
				}
				else if (material.HasProperty(ShaderUtilities.ID_UnderlayOffsetX))
				{
					offsetX = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * scaleRatio_C;
					offsetY = material.GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * scaleRatio_C;
					dilate = material.GetFloat(ShaderUtilities.ID_UnderlayDilate) * scaleRatio_C;
					softness = material.GetFloat(ShaderUtilities.ID_UnderlaySoftness) * scaleRatio_C;
				}
				padding.x = Mathf.Max(padding.x, faceDilate + dilate + softness - offsetX);
				padding.y = Mathf.Max(padding.y, faceDilate + dilate + softness - offsetY);
				padding.z = Mathf.Max(padding.z, faceDilate + dilate + softness + offsetX);
				padding.w = Mathf.Max(padding.w, faceDilate + dilate + softness + offsetY);
			}
			padding.x = Mathf.Max(padding.x, uniformPadding);
			padding.y = Mathf.Max(padding.y, uniformPadding);
			padding.z = Mathf.Max(padding.z, uniformPadding);
			padding.w = Mathf.Max(padding.w, uniformPadding);
			padding.x += (float)extraPadding;
			padding.y += (float)extraPadding;
			padding.z += (float)extraPadding;
			padding.w += (float)extraPadding;
			padding.x = Mathf.Min(padding.x, 1f);
			padding.y = Mathf.Min(padding.y, 1f);
			padding.z = Mathf.Min(padding.z, 1f);
			padding.w = Mathf.Min(padding.w, 1f);
			maxPadding.x = ((maxPadding.x < padding.x) ? padding.x : maxPadding.x);
			maxPadding.y = ((maxPadding.y < padding.y) ? padding.y : maxPadding.y);
			maxPadding.z = ((maxPadding.z < padding.z) ? padding.z : maxPadding.z);
			maxPadding.w = ((maxPadding.w < padding.w) ? padding.w : maxPadding.w);
			float gradientScale = material.GetFloat(ShaderUtilities.ID_GradientScale);
			padding *= gradientScale;
			uniformPadding = Mathf.Max(padding.x, padding.y);
			uniformPadding = Mathf.Max(padding.z, uniformPadding);
			uniformPadding = Mathf.Max(padding.w, uniformPadding);
			return uniformPadding + 1.25f;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00013438 File Offset: 0x00011638
		private static float ComputePaddingForProperties(Material mat)
		{
			Vector4 dilation = mat.GetVector(ShaderUtilities.ID_IsoPerimeter);
			Vector2 outlineOffset = mat.GetVector(ShaderUtilities.ID_OutlineOffset1);
			Vector2 outlineOffset2 = mat.GetVector(ShaderUtilities.ID_OutlineOffset2);
			Vector2 outlineOffset3 = mat.GetVector(ShaderUtilities.ID_OutlineOffset3);
			bool flag = mat.GetFloat(ShaderUtilities.ID_OutlineMode) != 0f;
			Vector4 softness = mat.GetVector(ShaderUtilities.ID_Softness);
			float gradientScale = mat.GetFloat(ShaderUtilities.ID_GradientScale);
			float padding = Mathf.Max(0f, dilation.x + softness.x * 0.5f);
			if (!flag)
			{
				padding = Mathf.Max(padding, dilation.y + softness.y * 0.5f + Mathf.Max(Mathf.Abs(outlineOffset.x), Mathf.Abs(outlineOffset.y)));
				padding = Mathf.Max(padding, dilation.z + softness.z * 0.5f + Mathf.Max(Mathf.Abs(outlineOffset2.x), Mathf.Abs(outlineOffset2.y)));
				padding = Mathf.Max(padding, dilation.w + softness.w * 0.5f + Mathf.Max(Mathf.Abs(outlineOffset3.x), Mathf.Abs(outlineOffset3.y)));
			}
			else
			{
				float offsetOutline = Mathf.Max(Mathf.Abs(outlineOffset.x), Mathf.Abs(outlineOffset.y));
				float offsetOutline2 = Mathf.Max(Mathf.Abs(outlineOffset2.x), Mathf.Abs(outlineOffset2.y));
				padding = Mathf.Max(padding, dilation.y + softness.y * 0.5f + offsetOutline);
				padding = Mathf.Max(padding, dilation.z + softness.z * 0.5f + offsetOutline2);
				float maxOffset = Mathf.Max(offsetOutline, offsetOutline2);
				padding += Mathf.Max(0f, dilation.w + softness.w * 0.5f - Mathf.Max(0f, padding - maxOffset));
			}
			Vector2 underlayOffset = mat.GetVector(ShaderUtilities.ID_UnderlayOffset);
			float underlayDilation = mat.GetFloat(ShaderUtilities.ID_UnderlayDilate);
			float underlaySoftness = mat.GetFloat(ShaderUtilities.ID_UnderlaySoftness);
			padding = Mathf.Max(padding, underlayDilation + underlaySoftness * 0.5f + Mathf.Max(Mathf.Abs(underlayOffset.x), Mathf.Abs(underlayOffset.y)));
			return padding * gradientScale;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000136A4 File Offset: 0x000118A4
		public static float GetPadding(Material[] materials, bool enableExtraPadding, bool isBold)
		{
			if (!ShaderUtilities.isInitialized)
			{
				ShaderUtilities.GetShaderPropertyIDs();
			}
			if (materials == null)
			{
				return 0f;
			}
			int extraPadding = (enableExtraPadding ? 4 : 0);
			if (materials[0].HasProperty(ShaderUtilities.ID_Padding))
			{
				return (float)extraPadding + materials[0].GetFloat(ShaderUtilities.ID_Padding);
			}
			Vector4 padding = Vector4.zero;
			Vector4 maxPadding = Vector4.zero;
			float faceDilate = 0f;
			float faceSoftness = 0f;
			float outlineThickness = 0f;
			float scaleRatio_A = 0f;
			float scaleRatio_B = 0f;
			float scaleRatio_C = 0f;
			float glowOffset = 0f;
			float glowOuter = 0f;
			float uniformPadding;
			for (int i = 0; i < materials.Length; i++)
			{
				ShaderUtilities.UpdateShaderRatios(materials[i]);
				string[] shaderKeywords = materials[i].shaderKeywords;
				if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_A))
				{
					scaleRatio_A = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_A);
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_FaceDilate))
				{
					faceDilate = materials[i].GetFloat(ShaderUtilities.ID_FaceDilate) * scaleRatio_A;
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_OutlineSoftness))
				{
					faceSoftness = materials[i].GetFloat(ShaderUtilities.ID_OutlineSoftness) * scaleRatio_A;
				}
				if (materials[i].HasProperty(ShaderUtilities.ID_OutlineWidth))
				{
					outlineThickness = materials[i].GetFloat(ShaderUtilities.ID_OutlineWidth) * scaleRatio_A;
				}
				uniformPadding = outlineThickness + faceSoftness + faceDilate;
				if (materials[i].HasProperty(ShaderUtilities.ID_GlowOffset) && shaderKeywords.Contains(ShaderUtilities.Keyword_Glow))
				{
					if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_B))
					{
						scaleRatio_B = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_B);
					}
					glowOffset = materials[i].GetFloat(ShaderUtilities.ID_GlowOffset) * scaleRatio_B;
					glowOuter = materials[i].GetFloat(ShaderUtilities.ID_GlowOuter) * scaleRatio_B;
				}
				uniformPadding = Mathf.Max(uniformPadding, faceDilate + glowOffset + glowOuter);
				if (materials[i].HasProperty(ShaderUtilities.ID_UnderlaySoftness) && shaderKeywords.Contains(ShaderUtilities.Keyword_Underlay))
				{
					if (materials[i].HasProperty(ShaderUtilities.ID_ScaleRatio_C))
					{
						scaleRatio_C = materials[i].GetFloat(ShaderUtilities.ID_ScaleRatio_C);
					}
					float offsetX = materials[i].GetFloat(ShaderUtilities.ID_UnderlayOffsetX) * scaleRatio_C;
					float offsetY = materials[i].GetFloat(ShaderUtilities.ID_UnderlayOffsetY) * scaleRatio_C;
					float dilate = materials[i].GetFloat(ShaderUtilities.ID_UnderlayDilate) * scaleRatio_C;
					float softness = materials[i].GetFloat(ShaderUtilities.ID_UnderlaySoftness) * scaleRatio_C;
					padding.x = Mathf.Max(padding.x, faceDilate + dilate + softness - offsetX);
					padding.y = Mathf.Max(padding.y, faceDilate + dilate + softness - offsetY);
					padding.z = Mathf.Max(padding.z, faceDilate + dilate + softness + offsetX);
					padding.w = Mathf.Max(padding.w, faceDilate + dilate + softness + offsetY);
				}
				padding.x = Mathf.Max(padding.x, uniformPadding);
				padding.y = Mathf.Max(padding.y, uniformPadding);
				padding.z = Mathf.Max(padding.z, uniformPadding);
				padding.w = Mathf.Max(padding.w, uniformPadding);
				padding.x += (float)extraPadding;
				padding.y += (float)extraPadding;
				padding.z += (float)extraPadding;
				padding.w += (float)extraPadding;
				padding.x = Mathf.Min(padding.x, 1f);
				padding.y = Mathf.Min(padding.y, 1f);
				padding.z = Mathf.Min(padding.z, 1f);
				padding.w = Mathf.Min(padding.w, 1f);
				maxPadding.x = ((maxPadding.x < padding.x) ? padding.x : maxPadding.x);
				maxPadding.y = ((maxPadding.y < padding.y) ? padding.y : maxPadding.y);
				maxPadding.z = ((maxPadding.z < padding.z) ? padding.z : maxPadding.z);
				maxPadding.w = ((maxPadding.w < padding.w) ? padding.w : maxPadding.w);
			}
			float gradientScale = materials[0].GetFloat(ShaderUtilities.ID_GradientScale);
			padding *= gradientScale;
			uniformPadding = Mathf.Max(padding.x, padding.y);
			uniformPadding = Mathf.Max(padding.z, uniformPadding);
			uniformPadding = Mathf.Max(padding.w, uniformPadding);
			return uniformPadding + 0.25f;
		}

		// Token: 0x04000335 RID: 821
		public static int ID_MainTex;

		// Token: 0x04000336 RID: 822
		public static int ID_FaceTex;

		// Token: 0x04000337 RID: 823
		public static int ID_FaceColor;

		// Token: 0x04000338 RID: 824
		public static int ID_FaceDilate;

		// Token: 0x04000339 RID: 825
		public static int ID_Shininess;

		// Token: 0x0400033A RID: 826
		public static int ID_OutlineOffset1;

		// Token: 0x0400033B RID: 827
		public static int ID_OutlineOffset2;

		// Token: 0x0400033C RID: 828
		public static int ID_OutlineOffset3;

		// Token: 0x0400033D RID: 829
		public static int ID_OutlineMode;

		// Token: 0x0400033E RID: 830
		public static int ID_IsoPerimeter;

		// Token: 0x0400033F RID: 831
		public static int ID_Softness;

		// Token: 0x04000340 RID: 832
		public static int ID_UnderlayColor;

		// Token: 0x04000341 RID: 833
		public static int ID_UnderlayOffsetX;

		// Token: 0x04000342 RID: 834
		public static int ID_UnderlayOffsetY;

		// Token: 0x04000343 RID: 835
		public static int ID_UnderlayDilate;

		// Token: 0x04000344 RID: 836
		public static int ID_UnderlaySoftness;

		// Token: 0x04000345 RID: 837
		public static int ID_UnderlayOffset;

		// Token: 0x04000346 RID: 838
		public static int ID_UnderlayIsoPerimeter;

		// Token: 0x04000347 RID: 839
		public static int ID_WeightNormal;

		// Token: 0x04000348 RID: 840
		public static int ID_WeightBold;

		// Token: 0x04000349 RID: 841
		public static int ID_OutlineTex;

		// Token: 0x0400034A RID: 842
		public static int ID_OutlineWidth;

		// Token: 0x0400034B RID: 843
		public static int ID_OutlineSoftness;

		// Token: 0x0400034C RID: 844
		public static int ID_OutlineColor;

		// Token: 0x0400034D RID: 845
		public static int ID_Outline2Color;

		// Token: 0x0400034E RID: 846
		public static int ID_Outline2Width;

		// Token: 0x0400034F RID: 847
		public static int ID_Padding;

		// Token: 0x04000350 RID: 848
		public static int ID_GradientScale;

		// Token: 0x04000351 RID: 849
		public static int ID_ScaleX;

		// Token: 0x04000352 RID: 850
		public static int ID_ScaleY;

		// Token: 0x04000353 RID: 851
		public static int ID_PerspectiveFilter;

		// Token: 0x04000354 RID: 852
		public static int ID_Sharpness;

		// Token: 0x04000355 RID: 853
		public static int ID_TextureWidth;

		// Token: 0x04000356 RID: 854
		public static int ID_TextureHeight;

		// Token: 0x04000357 RID: 855
		public static int ID_BevelAmount;

		// Token: 0x04000358 RID: 856
		public static int ID_GlowColor;

		// Token: 0x04000359 RID: 857
		public static int ID_GlowOffset;

		// Token: 0x0400035A RID: 858
		public static int ID_GlowPower;

		// Token: 0x0400035B RID: 859
		public static int ID_GlowOuter;

		// Token: 0x0400035C RID: 860
		public static int ID_GlowInner;

		// Token: 0x0400035D RID: 861
		public static int ID_LightAngle;

		// Token: 0x0400035E RID: 862
		public static int ID_EnvMap;

		// Token: 0x0400035F RID: 863
		public static int ID_EnvMatrix;

		// Token: 0x04000360 RID: 864
		public static int ID_EnvMatrixRotation;

		// Token: 0x04000361 RID: 865
		public static int ID_MaskCoord;

		// Token: 0x04000362 RID: 866
		public static int ID_ClipRect;

		// Token: 0x04000363 RID: 867
		public static int ID_MaskSoftnessX;

		// Token: 0x04000364 RID: 868
		public static int ID_MaskSoftnessY;

		// Token: 0x04000365 RID: 869
		public static int ID_VertexOffsetX;

		// Token: 0x04000366 RID: 870
		public static int ID_VertexOffsetY;

		// Token: 0x04000367 RID: 871
		public static int ID_UseClipRect;

		// Token: 0x04000368 RID: 872
		public static int ID_StencilID;

		// Token: 0x04000369 RID: 873
		public static int ID_StencilOp;

		// Token: 0x0400036A RID: 874
		public static int ID_StencilComp;

		// Token: 0x0400036B RID: 875
		public static int ID_StencilReadMask;

		// Token: 0x0400036C RID: 876
		public static int ID_StencilWriteMask;

		// Token: 0x0400036D RID: 877
		public static int ID_ShaderFlags;

		// Token: 0x0400036E RID: 878
		public static int ID_ScaleRatio_A;

		// Token: 0x0400036F RID: 879
		public static int ID_ScaleRatio_B;

		// Token: 0x04000370 RID: 880
		public static int ID_ScaleRatio_C;

		// Token: 0x04000371 RID: 881
		public static string Keyword_Bevel = "BEVEL_ON";

		// Token: 0x04000372 RID: 882
		public static string Keyword_Glow = "GLOW_ON";

		// Token: 0x04000373 RID: 883
		public static string Keyword_Underlay = "UNDERLAY_ON";

		// Token: 0x04000374 RID: 884
		public static string Keyword_Ratios = "RATIOS_OFF";

		// Token: 0x04000375 RID: 885
		public static string Keyword_MASK_SOFT = "MASK_SOFT";

		// Token: 0x04000376 RID: 886
		public static string Keyword_MASK_HARD = "MASK_HARD";

		// Token: 0x04000377 RID: 887
		public static string Keyword_MASK_TEX = "MASK_TEX";

		// Token: 0x04000378 RID: 888
		public static string Keyword_Outline = "OUTLINE_ON";

		// Token: 0x04000379 RID: 889
		public static string ShaderTag_ZTestMode = "unity_GUIZTestMode";

		// Token: 0x0400037A RID: 890
		public static string ShaderTag_CullMode = "_CullMode";

		// Token: 0x0400037B RID: 891
		private static float m_clamp = 1f;

		// Token: 0x0400037C RID: 892
		public static bool isInitialized = false;

		// Token: 0x0400037D RID: 893
		private static Shader k_ShaderRef_MobileSDF;

		// Token: 0x0400037E RID: 894
		private static Shader k_ShaderRef_MobileBitmap;
	}
}
