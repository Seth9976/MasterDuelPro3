using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000041 RID: 65
	internal static class LayerUtility
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000E1A7 File Offset: 0x0000C3A7
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000E1AE File Offset: 0x0000C3AE
		public static uint maxTextureCount { get; private set; }

		// Token: 0x06000185 RID: 389 RVA: 0x0000E1B6 File Offset: 0x0000C3B6
		public static void InitializeBudget(uint maxTextureCount)
		{
			LayerUtility.maxTextureCount = math.max(4U, maxTextureCount);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		private static bool CanBatchLightsInLayer(int layerIndex1, int layerIndex2, SortingLayer[] sortingLayers, ILight2DCullResult lightCullResult)
		{
			int layerId = sortingLayers[layerIndex1].id;
			int layerId2 = sortingLayers[layerIndex2].id;
			foreach (Light2D light in lightCullResult.visibleLights)
			{
				if (light.IsLitLayer(layerId) != light.IsLitLayer(layerId2))
				{
					return false;
				}
			}
			foreach (ShadowCasterGroup2D shadowCasterGroup2D in lightCullResult.visibleShadows)
			{
				foreach (ShadowCaster2D shadowCaster in shadowCasterGroup2D.GetShadowCasters())
				{
					if (shadowCaster.IsShadowedLayer(layerId) != shadowCaster.IsShadowedLayer(layerId2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		private static int FindUpperBoundInBatch(int startLayerIndex, SortingLayer[] sortingLayers, ILight2DCullResult lightCullResult)
		{
			for (int i = startLayerIndex + 1; i < sortingLayers.Length; i++)
			{
				if (!LayerUtility.CanBatchLightsInLayer(startLayerIndex, i, sortingLayers, lightCullResult))
				{
					return i - 1;
				}
			}
			return sortingLayers.Length - 1;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000E308 File Offset: 0x0000C508
		private static void InitializeBatchInfos(SortingLayer[] cachedSortingLayers)
		{
			int count = cachedSortingLayers.Length;
			bool flag = LayerUtility.s_LayerBatches == null;
			if (LayerUtility.s_LayerBatches == null)
			{
				LayerUtility.s_LayerBatches = new LayerBatch[count];
			}
			if (flag)
			{
				for (int i = 0; i < LayerUtility.s_LayerBatches.Length; i++)
				{
					LayerUtility.s_LayerBatches[i].InitRTIds(i);
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000E358 File Offset: 0x0000C558
		public static LayerBatch[] CalculateBatches(ILight2DCullResult lightCullResult, out int batchCount)
		{
			SortingLayer[] cachedSortingLayers = Light2DManager.GetCachedSortingLayer();
			LayerUtility.InitializeBatchInfos(cachedSortingLayers);
			bool anyNormals = false;
			batchCount = 0;
			int upperLayerInBatch;
			for (int i = 0; i < cachedSortingLayers.Length; i = upperLayerInBatch + 1)
			{
				int layerToRender = cachedSortingLayers[i].id;
				LayerBatch[] array = LayerUtility.s_LayerBatches;
				int num = batchCount;
				batchCount = num + 1;
				ref LayerBatch layerBatch = ref array[num];
				LightStats lightStats = lightCullResult.GetLightStatsByLayer(layerToRender, ref layerBatch);
				upperLayerInBatch = LayerUtility.FindUpperBoundInBatch(i, cachedSortingLayers, lightCullResult);
				short startLayerValue = (short)cachedSortingLayers[i].value;
				short lowerBound = ((i == 0) ? short.MinValue : startLayerValue);
				short endLayerValue = (short)cachedSortingLayers[upperLayerInBatch].value;
				short upperBound = ((upperLayerInBatch == cachedSortingLayers.Length - 1) ? short.MaxValue : endLayerValue);
				SortingLayerRange sortingLayerRange = new SortingLayerRange(lowerBound, upperBound);
				layerBatch.startLayerID = layerToRender;
				layerBatch.endLayerValue = (int)endLayerValue;
				layerBatch.layerRange = sortingLayerRange;
				layerBatch.lightStats = lightStats;
				anyNormals |= layerBatch.lightStats.useNormalMap;
			}
			for (int j = 0; j < batchCount; j++)
			{
				LayerBatch[] array2 = LayerUtility.s_LayerBatches;
				int num2 = j;
				bool hasSpriteMask = SpriteMaskUtility.HasSpriteMaskInLayerRange(array2[num2].layerRange);
				array2[num2].useNormals = array2[num2].lightStats.useNormalMap || (anyNormals && hasSpriteMask);
			}
			LayerUtility.SetupActiveBlendStyles();
			return LayerUtility.s_LayerBatches;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000E490 File Offset: 0x0000C690
		public static void GetFilterSettings(Renderer2DData rendererData, ref LayerBatch layerBatch, short cameraSortingLayerBoundsIndex, out FilteringSettings filterSettings)
		{
			filterSettings = FilteringSettings.defaultValue;
			filterSettings.renderQueueRange = RenderQueueRange.all;
			filterSettings.layerMask = -1;
			filterSettings.renderingLayerMask = uint.MaxValue;
			short upperBound = layerBatch.layerRange.upperBound;
			if (rendererData.useCameraSortingLayerTexture && cameraSortingLayerBoundsIndex >= layerBatch.layerRange.lowerBound && cameraSortingLayerBoundsIndex < layerBatch.layerRange.upperBound)
			{
				upperBound = cameraSortingLayerBoundsIndex;
			}
			filterSettings.sortingLayerRange = new SortingLayerRange(layerBatch.layerRange.lowerBound, upperBound);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000E50C File Offset: 0x0000C70C
		private static void SetupActiveBlendStyles()
		{
			for (int i = 0; i < LayerUtility.s_LayerBatches.Length; i++)
			{
				ref LayerBatch layer = ref LayerUtility.s_LayerBatches[i];
				int size = 0;
				for (int blendStyleIndex = 0; blendStyleIndex < RendererLighting.k_ShapeLightTextureIDs.Length; blendStyleIndex++)
				{
					uint blendStyleMask = 1U << blendStyleIndex;
					if ((layer.lightStats.blendStylesUsed & blendStyleMask) > 0U)
					{
						size++;
					}
				}
				if (layer.activeBlendStylesIndices == null || layer.activeBlendStylesIndices.Length != size)
				{
					layer.activeBlendStylesIndices = new int[size];
				}
				int index = 0;
				for (int blendStyleIndex2 = 0; blendStyleIndex2 < RendererLighting.k_ShapeLightTextureIDs.Length; blendStyleIndex2++)
				{
					uint blendStyleMask2 = 1U << blendStyleIndex2;
					if ((layer.lightStats.blendStylesUsed & blendStyleMask2) > 0U)
					{
						layer.activeBlendStylesIndices[index++] = blendStyleIndex2;
					}
				}
			}
		}

		// Token: 0x0400014A RID: 330
		private static LayerBatch[] s_LayerBatches;
	}
}
