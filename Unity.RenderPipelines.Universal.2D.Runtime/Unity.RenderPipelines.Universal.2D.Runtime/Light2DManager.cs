using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000036 RID: 54
	internal static class Light2DManager
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000BF91 File Offset: 0x0000A191
		public static List<Light2D> lights { get; } = new List<Light2D>();

		// Token: 0x0600015A RID: 346 RVA: 0x0000BF98 File Offset: 0x0000A198
		public static void RegisterLight(Light2D light)
		{
			Light2DManager.lights.Add(light);
			Light2DManager.ErrorIfDuplicateGlobalLight(light);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000BFAB File Offset: 0x0000A1AB
		public static void DeregisterLight(Light2D light)
		{
			Light2DManager.lights.Remove(light);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public static void ErrorIfDuplicateGlobalLight(Light2D light)
		{
			if (light.lightType != Light2D.LightType.Global)
			{
				return;
			}
			foreach (int sortingLayer in light.affectedSortingLayers)
			{
				if (Light2DManager.ContainsDuplicateGlobalLight(sortingLayer, light.blendStyleIndex))
				{
					Debug.LogError("More than one global light on layer " + SortingLayer.IDToName(sortingLayer) + " for light blend style index " + light.blendStyleIndex.ToString());
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000C024 File Offset: 0x0000A224
		public static bool GetGlobalColor(int sortingLayerIndex, int blendStyleIndex, out Color color)
		{
			bool foundGlobalColor = false;
			color = Color.black;
			foreach (Light2D light in Light2DManager.lights)
			{
				if (light.lightType == Light2D.LightType.Global && light.blendStyleIndex == blendStyleIndex && light.IsLitLayer(sortingLayerIndex))
				{
					if (true)
					{
						color = light.color * light.intensity;
						return true;
					}
					if (!foundGlobalColor)
					{
						color = light.color * light.intensity;
						foundGlobalColor = true;
					}
				}
			}
			return foundGlobalColor;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		private static bool ContainsDuplicateGlobalLight(int sortingLayerIndex, int blendStyleIndex)
		{
			int globalLightCount = 0;
			foreach (Light2D light in Light2DManager.lights)
			{
				if (light.lightType == Light2D.LightType.Global && light.blendStyleIndex == blendStyleIndex && light.IsLitLayer(sortingLayerIndex))
				{
					if (globalLightCount > 0)
					{
						return true;
					}
					globalLightCount++;
				}
			}
			return false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000C14C File Offset: 0x0000A34C
		public static SortingLayer[] GetCachedSortingLayer()
		{
			if (Light2DManager.s_SortingLayers == null)
			{
				Light2DManager.s_SortingLayers = SortingLayer.layers;
			}
			return Light2DManager.s_SortingLayers;
		}

		// Token: 0x04000110 RID: 272
		private static SortingLayer[] s_SortingLayers;
	}
}
