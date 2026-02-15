using System;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001F RID: 31
	internal static class MaterialManager
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00004178 File Offset: 0x00002378
		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			bool isMainThread = !JobsUtility.IsExecutingJob;
			int sourceId = sourceMaterial.GetHashCode();
			int texId = targetMaterial.GetHashCode();
			long key = ((long)sourceId << 32) | (long)((ulong)texId);
			Material fallbackMaterial;
			bool flag = MaterialManager.s_FallbackMaterials.TryGetValue(key, out fallbackMaterial);
			if (flag)
			{
				bool flag2 = fallbackMaterial == null;
				if (flag2)
				{
					MaterialManager.s_FallbackMaterials.Remove(key);
				}
				else
				{
					bool flag3 = !isMainThread;
					if (flag3)
					{
						return fallbackMaterial;
					}
					int sourceMaterialCRC = sourceMaterial.ComputeCRC();
					int fallbackMaterialCRC = fallbackMaterial.ComputeCRC();
					bool flag4 = sourceMaterialCRC == fallbackMaterialCRC;
					if (flag4)
					{
						return fallbackMaterial;
					}
					MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallbackMaterial);
					return fallbackMaterial;
				}
			}
			bool flag5 = sourceMaterial.HasProperty(TextShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(TextShaderUtilities.ID_GradientScale);
			if (flag5)
			{
				Texture tex = targetMaterial.GetTexture(TextShaderUtilities.ID_MainTex);
				fallbackMaterial = new Material(sourceMaterial);
				fallbackMaterial.hideFlags = HideFlags.HideAndDontSave;
				fallbackMaterial.SetTexture(TextShaderUtilities.ID_MainTex, tex);
				fallbackMaterial.SetFloat(TextShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(TextShaderUtilities.ID_GradientScale));
				fallbackMaterial.SetFloat(TextShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(TextShaderUtilities.ID_TextureWidth));
				fallbackMaterial.SetFloat(TextShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(TextShaderUtilities.ID_TextureHeight));
				fallbackMaterial.SetFloat(TextShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(TextShaderUtilities.ID_WeightNormal));
				fallbackMaterial.SetFloat(TextShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(TextShaderUtilities.ID_WeightBold));
			}
			else
			{
				fallbackMaterial = new Material(targetMaterial);
			}
			MaterialManager.s_FallbackMaterials.Add(key, fallbackMaterial);
			return fallbackMaterial;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004314 File Offset: 0x00002514
		public static Material GetFallbackMaterial(FontAsset fontAsset, Material sourceMaterial, int atlasIndex)
		{
			bool isMainThread = !JobsUtility.IsExecutingJob;
			int sourceMaterialID = sourceMaterial.GetHashCode();
			Texture tex = fontAsset.atlasTextures[atlasIndex];
			int texID = tex.GetHashCode();
			long key = ((long)sourceMaterialID << 32) | (long)((ulong)texID);
			Material fallbackMaterial;
			bool flag = MaterialManager.s_FallbackMaterials.TryGetValue(key, out fallbackMaterial);
			Material material;
			if (flag)
			{
				bool flag2 = !isMainThread;
				if (flag2)
				{
					material = fallbackMaterial;
				}
				else
				{
					int sourceMaterialCRC = sourceMaterial.ComputeCRC();
					int fallbackMaterialCRC = fallbackMaterial.ComputeCRC();
					bool flag3 = sourceMaterialCRC == fallbackMaterialCRC;
					if (flag3)
					{
						material = fallbackMaterial;
					}
					else
					{
						MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallbackMaterial);
						material = fallbackMaterial;
					}
				}
			}
			else
			{
				fallbackMaterial = new Material(sourceMaterial);
				fallbackMaterial.SetTexture(TextShaderUtilities.ID_MainTex, tex);
				fallbackMaterial.hideFlags = HideFlags.HideAndDontSave;
				MaterialManager.s_FallbackMaterials.Add(key, fallbackMaterial);
				material = fallbackMaterial;
			}
			return material;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000043DC File Offset: 0x000025DC
		private static void CopyMaterialPresetProperties(Material source, Material destination)
		{
			bool flag = !source.HasProperty(TextShaderUtilities.ID_GradientScale) || !destination.HasProperty(TextShaderUtilities.ID_GradientScale);
			if (!flag)
			{
				Texture dst_texture = destination.GetTexture(TextShaderUtilities.ID_MainTex);
				float dst_gradientScale = destination.GetFloat(TextShaderUtilities.ID_GradientScale);
				float dst_texWidth = destination.GetFloat(TextShaderUtilities.ID_TextureWidth);
				float dst_texHeight = destination.GetFloat(TextShaderUtilities.ID_TextureHeight);
				float dst_weightNormal = destination.GetFloat(TextShaderUtilities.ID_WeightNormal);
				float dst_weightBold = destination.GetFloat(TextShaderUtilities.ID_WeightBold);
				destination.shader = source.shader;
				destination.CopyPropertiesFromMaterial(source);
				destination.shaderKeywords = source.shaderKeywords;
				destination.SetTexture(TextShaderUtilities.ID_MainTex, dst_texture);
				destination.SetFloat(TextShaderUtilities.ID_GradientScale, dst_gradientScale);
				destination.SetFloat(TextShaderUtilities.ID_TextureWidth, dst_texWidth);
				destination.SetFloat(TextShaderUtilities.ID_TextureHeight, dst_texHeight);
				destination.SetFloat(TextShaderUtilities.ID_WeightNormal, dst_weightNormal);
				destination.SetFloat(TextShaderUtilities.ID_WeightBold, dst_weightBold);
			}
		}

		// Token: 0x040000A4 RID: 164
		private static Dictionary<long, Material> s_FallbackMaterials = new Dictionary<long, Material>();
	}
}
