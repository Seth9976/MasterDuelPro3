using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200005E RID: 94
	public static class TMP_MaterialManager
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0001005F File Offset: 0x0000E25F
		static TMP_MaterialManager()
		{
			Canvas.willRenderCanvases += TMP_MaterialManager.OnPreRender;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001009A File Offset: 0x0000E29A
		private static void OnPreRender()
		{
			if (TMP_MaterialManager.isFallbackListDirty)
			{
				TMP_MaterialManager.CleanupFallbackMaterials();
				TMP_MaterialManager.isFallbackListDirty = false;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000100B0 File Offset: 0x0000E2B0
		public static Material GetStencilMaterial(Material baseMaterial, int stencilID)
		{
			if (!baseMaterial.HasProperty(ShaderUtilities.ID_StencilID))
			{
				Debug.LogWarning("Selected Shader does not support Stencil Masking. Please select the Distance Field or Mobile Distance Field Shader.");
				return baseMaterial;
			}
			int baseMaterialID = baseMaterial.GetInstanceID();
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				if (TMP_MaterialManager.m_materialList[i].baseMaterial.GetInstanceID() == baseMaterialID && TMP_MaterialManager.m_materialList[i].stencilID == stencilID)
				{
					TMP_MaterialManager.m_materialList[i].count++;
					return TMP_MaterialManager.m_materialList[i].stencilMaterial;
				}
			}
			Material stencilMaterial = new Material(baseMaterial);
			stencilMaterial.hideFlags = HideFlags.HideAndDontSave;
			stencilMaterial.shaderKeywords = baseMaterial.shaderKeywords;
			ShaderUtilities.GetShaderPropertyIDs();
			stencilMaterial.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			stencilMaterial.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			TMP_MaterialManager.MaskingMaterial temp = new TMP_MaterialManager.MaskingMaterial();
			temp.baseMaterial = baseMaterial;
			temp.stencilMaterial = stencilMaterial;
			temp.stencilID = stencilID;
			temp.count = 1;
			TMP_MaterialManager.m_materialList.Add(temp);
			return stencilMaterial;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public static void ReleaseStencilMaterial(Material stencilMaterial)
		{
			int stencilMaterialID = stencilMaterial.GetInstanceID();
			int i = 0;
			while (i < TMP_MaterialManager.m_materialList.Count)
			{
				if (TMP_MaterialManager.m_materialList[i].stencilMaterial.GetInstanceID() == stencilMaterialID)
				{
					if (TMP_MaterialManager.m_materialList[i].count > 1)
					{
						TMP_MaterialManager.m_materialList[i].count--;
						return;
					}
					global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
					TMP_MaterialManager.m_materialList.RemoveAt(i);
					stencilMaterial = null;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010248 File Offset: 0x0000E448
		public static Material GetBaseMaterial(Material stencilMaterial)
		{
			int index = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (index == -1)
			{
				return null;
			}
			return TMP_MaterialManager.m_materialList[index].baseMaterial;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001028F File Offset: 0x0000E48F
		public static Material SetStencil(Material material, int stencilID)
		{
			material.SetFloat(ShaderUtilities.ID_StencilID, (float)stencilID);
			if (stencilID == 0)
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 8f);
			}
			else
			{
				material.SetFloat(ShaderUtilities.ID_StencilComp, 4f);
			}
			return material;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000102C4 File Offset: 0x0000E4C4
		public static void AddMaskingMaterial(Material baseMaterial, Material stencilMaterial, int stencilID)
		{
			int index = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (index == -1)
			{
				TMP_MaterialManager.MaskingMaterial temp = new TMP_MaterialManager.MaskingMaterial();
				temp.baseMaterial = baseMaterial;
				temp.stencilMaterial = stencilMaterial;
				temp.stencilID = stencilID;
				temp.count = 1;
				TMP_MaterialManager.m_materialList.Add(temp);
				return;
			}
			stencilMaterial = TMP_MaterialManager.m_materialList[index].stencilMaterial;
			TMP_MaterialManager.m_materialList[index].count++;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001035C File Offset: 0x0000E55C
		public static void RemoveStencilMaterial(Material stencilMaterial)
		{
			int index = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.stencilMaterial == stencilMaterial);
			if (index != -1)
			{
				TMP_MaterialManager.m_materialList.RemoveAt(index);
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001039C File Offset: 0x0000E59C
		public static void ReleaseBaseMaterial(Material baseMaterial)
		{
			int index = TMP_MaterialManager.m_materialList.FindIndex((TMP_MaterialManager.MaskingMaterial item) => item.baseMaterial == baseMaterial);
			if (index == -1)
			{
				Debug.Log("No Masking Material exists for " + baseMaterial.name);
				return;
			}
			if (TMP_MaterialManager.m_materialList[index].count > 1)
			{
				TMP_MaterialManager.m_materialList[index].count--;
				Debug.Log(string.Concat(new string[]
				{
					"Removed (1) reference to ",
					TMP_MaterialManager.m_materialList[index].stencilMaterial.name,
					". There are ",
					TMP_MaterialManager.m_materialList[index].count.ToString(),
					" references left."
				}));
				return;
			}
			Debug.Log("Removed last reference to " + TMP_MaterialManager.m_materialList[index].stencilMaterial.name + " with ID " + TMP_MaterialManager.m_materialList[index].stencilMaterial.GetInstanceID().ToString());
			global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[index].stencilMaterial);
			TMP_MaterialManager.m_materialList.RemoveAt(index);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000104D8 File Offset: 0x0000E6D8
		public static void ClearMaterials()
		{
			if (TMP_MaterialManager.m_materialList.Count == 0)
			{
				Debug.Log("Material List has already been cleared.");
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_materialList.Count; i++)
			{
				global::UnityEngine.Object.DestroyImmediate(TMP_MaterialManager.m_materialList[i].stencilMaterial);
			}
			TMP_MaterialManager.m_materialList.Clear();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00010530 File Offset: 0x0000E730
		public static int GetStencilID(GameObject obj)
		{
			int count = 0;
			Transform transform = obj.transform;
			Transform stopAfter = TMP_MaterialManager.FindRootSortOverrideCanvas(transform);
			if (transform == stopAfter)
			{
				return count;
			}
			Transform t = transform.parent;
			List<Mask> components = TMP_ListPool<Mask>.Get();
			while (t != null)
			{
				t.GetComponents<Mask>(components);
				for (int i = 0; i < components.Count; i++)
				{
					Mask mask = components[i];
					if (mask != null && mask.MaskEnabled() && mask.graphic.IsActive())
					{
						count++;
						break;
					}
				}
				if (t == stopAfter)
				{
					break;
				}
				t = t.parent;
			}
			TMP_ListPool<Mask>.Release(components);
			return Mathf.Min((1 << count) - 1, 255);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000105EC File Offset: 0x0000E7EC
		public static Material GetMaterialForRendering(MaskableGraphic graphic, Material baseMaterial)
		{
			if (baseMaterial == null)
			{
				return null;
			}
			List<IMaterialModifier> modifiers = TMP_ListPool<IMaterialModifier>.Get();
			graphic.GetComponents<IMaterialModifier>(modifiers);
			Material result = baseMaterial;
			for (int i = 0; i < modifiers.Count; i++)
			{
				result = modifiers[i].GetModifiedMaterial(result);
			}
			TMP_ListPool<IMaterialModifier>.Release(modifiers);
			return result;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001063C File Offset: 0x0000E83C
		private static Transform FindRootSortOverrideCanvas(Transform start)
		{
			List<Canvas> canvasList = TMP_ListPool<Canvas>.Get();
			start.GetComponentsInParent<Canvas>(false, canvasList);
			Canvas canvas = null;
			for (int i = 0; i < canvasList.Count; i++)
			{
				canvas = canvasList[i];
				if (canvas.overrideSorting)
				{
					break;
				}
			}
			TMP_ListPool<Canvas>.Release(canvasList);
			if (!(canvas != null))
			{
				return null;
			}
			return canvas.transform;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00010694 File Offset: 0x0000E894
		internal static Material GetFallbackMaterial(TMP_FontAsset fontAsset, Material sourceMaterial, int atlasIndex)
		{
			long instanceID = (long)sourceMaterial.GetInstanceID();
			Texture tex = fontAsset.atlasTextures[atlasIndex];
			int texID = tex.GetInstanceID();
			long key = (instanceID << 32) | (long)((ulong)texID);
			TMP_MaterialManager.FallbackMaterial fallback;
			if (!TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallback))
			{
				Material fallbackMaterial = new Material(sourceMaterial);
				fallbackMaterial.SetTexture(ShaderUtilities.ID_MainTex, tex);
				fallbackMaterial.hideFlags = HideFlags.HideAndDontSave;
				fallback = new TMP_MaterialManager.FallbackMaterial();
				fallback.fallbackID = key;
				fallback.sourceMaterial = fontAsset.material;
				fallback.sourceMaterialCRC = sourceMaterial.ComputeCRC();
				fallback.fallbackMaterial = fallbackMaterial;
				fallback.count = 0;
				TMP_MaterialManager.m_fallbackMaterials.Add(key, fallback);
				TMP_MaterialManager.m_fallbackMaterialLookup.Add(fallbackMaterial.GetInstanceID(), key);
				return fallbackMaterial;
			}
			int sourceMaterialCRC = sourceMaterial.ComputeCRC();
			if (sourceMaterialCRC == fallback.sourceMaterialCRC)
			{
				return fallback.fallbackMaterial;
			}
			TMP_MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallback.fallbackMaterial);
			fallback.sourceMaterialCRC = sourceMaterialCRC;
			return fallback.fallbackMaterial;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00010774 File Offset: 0x0000E974
		public static Material GetFallbackMaterial(Material sourceMaterial, Material targetMaterial)
		{
			long instanceID = (long)sourceMaterial.GetInstanceID();
			Texture tex = targetMaterial.GetTexture(ShaderUtilities.ID_MainTex);
			int texID = tex.GetInstanceID();
			long key = (instanceID << 32) | (long)((ulong)texID);
			TMP_MaterialManager.FallbackMaterial fallback;
			if (!TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallback))
			{
				Material fallbackMaterial;
				if (sourceMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && targetMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
				{
					fallbackMaterial = new Material(sourceMaterial);
					fallbackMaterial.hideFlags = HideFlags.HideAndDontSave;
					fallbackMaterial.SetTexture(ShaderUtilities.ID_MainTex, tex);
					fallbackMaterial.SetFloat(ShaderUtilities.ID_GradientScale, targetMaterial.GetFloat(ShaderUtilities.ID_GradientScale));
					fallbackMaterial.SetFloat(ShaderUtilities.ID_TextureWidth, targetMaterial.GetFloat(ShaderUtilities.ID_TextureWidth));
					fallbackMaterial.SetFloat(ShaderUtilities.ID_TextureHeight, targetMaterial.GetFloat(ShaderUtilities.ID_TextureHeight));
					fallbackMaterial.SetFloat(ShaderUtilities.ID_WeightNormal, targetMaterial.GetFloat(ShaderUtilities.ID_WeightNormal));
					fallbackMaterial.SetFloat(ShaderUtilities.ID_WeightBold, targetMaterial.GetFloat(ShaderUtilities.ID_WeightBold));
				}
				else
				{
					fallbackMaterial = new Material(targetMaterial);
					fallbackMaterial.hideFlags = HideFlags.HideAndDontSave;
				}
				fallback = new TMP_MaterialManager.FallbackMaterial();
				fallback.fallbackID = key;
				fallback.sourceMaterial = sourceMaterial;
				fallback.sourceMaterialCRC = sourceMaterial.ComputeCRC();
				fallback.fallbackMaterial = fallbackMaterial;
				fallback.count = 0;
				TMP_MaterialManager.m_fallbackMaterials.Add(key, fallback);
				TMP_MaterialManager.m_fallbackMaterialLookup.Add(fallbackMaterial.GetInstanceID(), key);
				return fallbackMaterial;
			}
			int sourceMaterialCRC = sourceMaterial.ComputeCRC();
			if (sourceMaterialCRC == fallback.sourceMaterialCRC)
			{
				return fallback.fallbackMaterial;
			}
			TMP_MaterialManager.CopyMaterialPresetProperties(sourceMaterial, fallback.fallbackMaterial);
			fallback.sourceMaterialCRC = sourceMaterialCRC;
			return fallback.fallbackMaterial;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000108F8 File Offset: 0x0000EAF8
		public static void AddFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int sourceID = targetMaterial.GetInstanceID();
			long key;
			TMP_MaterialManager.FallbackMaterial fallback;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(sourceID, out key) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallback))
			{
				fallback.count++;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00010944 File Offset: 0x0000EB44
		public static void RemoveFallbackMaterialReference(Material targetMaterial)
		{
			if (targetMaterial == null)
			{
				return;
			}
			int sourceID = targetMaterial.GetInstanceID();
			long key;
			TMP_MaterialManager.FallbackMaterial fallback;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(sourceID, out key) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallback))
			{
				fallback.count--;
				if (fallback.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallback);
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000109A4 File Offset: 0x0000EBA4
		public static void CleanupFallbackMaterials()
		{
			if (TMP_MaterialManager.m_fallbackCleanupList.Count == 0)
			{
				return;
			}
			for (int i = 0; i < TMP_MaterialManager.m_fallbackCleanupList.Count; i++)
			{
				TMP_MaterialManager.FallbackMaterial fallback = TMP_MaterialManager.m_fallbackCleanupList[i];
				if (fallback.count < 1)
				{
					Material mat = fallback.fallbackMaterial;
					TMP_MaterialManager.m_fallbackMaterials.Remove(fallback.fallbackID);
					TMP_MaterialManager.m_fallbackMaterialLookup.Remove(mat.GetInstanceID());
					global::UnityEngine.Object.DestroyImmediate(mat);
				}
			}
			TMP_MaterialManager.m_fallbackCleanupList.Clear();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00010A24 File Offset: 0x0000EC24
		public static void ReleaseFallbackMaterial(Material fallbackMaterial)
		{
			if (fallbackMaterial == null)
			{
				return;
			}
			int materialID = fallbackMaterial.GetInstanceID();
			long key;
			TMP_MaterialManager.FallbackMaterial fallback;
			if (TMP_MaterialManager.m_fallbackMaterialLookup.TryGetValue(materialID, out key) && TMP_MaterialManager.m_fallbackMaterials.TryGetValue(key, out fallback))
			{
				fallback.count--;
				if (fallback.count < 1)
				{
					TMP_MaterialManager.m_fallbackCleanupList.Add(fallback);
				}
			}
			TMP_MaterialManager.isFallbackListDirty = true;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00010A88 File Offset: 0x0000EC88
		public static void CopyMaterialPresetProperties(Material source, Material destination)
		{
			if (!source.HasProperty(ShaderUtilities.ID_GradientScale) || !destination.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				return;
			}
			Texture dst_texture = destination.GetTexture(ShaderUtilities.ID_MainTex);
			float dst_gradientScale = destination.GetFloat(ShaderUtilities.ID_GradientScale);
			float dst_texWidth = destination.GetFloat(ShaderUtilities.ID_TextureWidth);
			float dst_texHeight = destination.GetFloat(ShaderUtilities.ID_TextureHeight);
			float dst_weightNormal = destination.GetFloat(ShaderUtilities.ID_WeightNormal);
			float dst_weightBold = destination.GetFloat(ShaderUtilities.ID_WeightBold);
			destination.shader = source.shader;
			destination.CopyPropertiesFromMaterial(source);
			destination.shaderKeywords = source.shaderKeywords;
			destination.SetTexture(ShaderUtilities.ID_MainTex, dst_texture);
			destination.SetFloat(ShaderUtilities.ID_GradientScale, dst_gradientScale);
			destination.SetFloat(ShaderUtilities.ID_TextureWidth, dst_texWidth);
			destination.SetFloat(ShaderUtilities.ID_TextureHeight, dst_texHeight);
			destination.SetFloat(ShaderUtilities.ID_WeightNormal, dst_weightNormal);
			destination.SetFloat(ShaderUtilities.ID_WeightBold, dst_weightBold);
		}

		// Token: 0x0400022C RID: 556
		private static List<TMP_MaterialManager.MaskingMaterial> m_materialList = new List<TMP_MaterialManager.MaskingMaterial>();

		// Token: 0x0400022D RID: 557
		private static Dictionary<long, TMP_MaterialManager.FallbackMaterial> m_fallbackMaterials = new Dictionary<long, TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x0400022E RID: 558
		private static Dictionary<int, long> m_fallbackMaterialLookup = new Dictionary<int, long>();

		// Token: 0x0400022F RID: 559
		private static List<TMP_MaterialManager.FallbackMaterial> m_fallbackCleanupList = new List<TMP_MaterialManager.FallbackMaterial>();

		// Token: 0x04000230 RID: 560
		private static bool isFallbackListDirty;

		// Token: 0x0200005F RID: 95
		private class FallbackMaterial
		{
			// Token: 0x04000231 RID: 561
			public long fallbackID;

			// Token: 0x04000232 RID: 562
			public Material sourceMaterial;

			// Token: 0x04000233 RID: 563
			internal int sourceMaterialCRC;

			// Token: 0x04000234 RID: 564
			public Material fallbackMaterial;

			// Token: 0x04000235 RID: 565
			public int count;
		}

		// Token: 0x02000060 RID: 96
		private class MaskingMaterial
		{
			// Token: 0x04000236 RID: 566
			public Material baseMaterial;

			// Token: 0x04000237 RID: 567
			public Material stencilMaterial;

			// Token: 0x04000238 RID: 568
			public int count;

			// Token: 0x04000239 RID: 569
			public int stencilID;
		}
	}
}
