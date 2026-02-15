using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000034 RID: 52
	internal class Light2DCullResult : ILight2DCullResult
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000BAAB File Offset: 0x00009CAB
		public List<Light2D> visibleLights
		{
			get
			{
				return this.m_VisibleLights;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000BAB3 File Offset: 0x00009CB3
		public HashSet<ShadowCasterGroup2D> visibleShadows
		{
			get
			{
				return this.m_VisibleShadows;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000BABB File Offset: 0x00009CBB
		public bool IsSceneLit()
		{
			return Light2DManager.lights.Count > 0;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000BACC File Offset: 0x00009CCC
		public LightStats GetLightStatsByLayer(int layerID, ref LayerBatch layer)
		{
			layer.lights.Clear();
			layer.shadowLights.Clear();
			layer.shadowCasters.Clear();
			LightStats returnStats = default(LightStats);
			foreach (Light2D light in this.visibleLights)
			{
				if (light.IsLitLayer(layerID))
				{
					if (light.normalMapQuality != Light2D.NormalMapQuality.Disabled)
					{
						returnStats.totalNormalMapUsage++;
					}
					if (light.volumeIntensity > 0f && light.volumetricEnabled)
					{
						returnStats.totalVolumetricUsage++;
					}
					if (light.volumeIntensity > 0f && light.volumetricEnabled && RendererLighting.CanCastShadows(light, layerID))
					{
						returnStats.totalVolumetricShadowUsage++;
					}
					returnStats.blendStylesUsed |= 1U << light.blendStyleIndex;
					if (light.lightType != Light2D.LightType.Global)
					{
						returnStats.blendStylesWithLights |= 1U << light.blendStyleIndex;
					}
					bool isShadowed = false;
					if (RendererLighting.CanCastShadows(light, layerID))
					{
						foreach (ShadowCasterGroup2D group in this.visibleShadows)
						{
							List<ShadowCaster2D> shadowCasters = group.GetShadowCasters();
							if (shadowCasters != null)
							{
								foreach (ShadowCaster2D shadowCaster in shadowCasters)
								{
									if (shadowCaster.IsLit(light) && shadowCaster.IsShadowedLayer(layerID))
									{
										isShadowed = true;
										returnStats.totalShadows++;
										if (!layer.shadowCasters.Contains(group))
										{
											layer.shadowCasters.Add(group);
										}
									}
								}
							}
						}
					}
					if (isShadowed)
					{
						returnStats.totalShadowLights++;
						layer.shadowLights.Add(light);
					}
					else
					{
						returnStats.totalLights++;
						layer.lights.Add(light);
					}
				}
			}
			return returnStats;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000BD18 File Offset: 0x00009F18
		public void SetupCulling(ref ScriptableCullingParameters cullingParameters, Camera camera)
		{
			this.m_VisibleLights.Clear();
			foreach (Light2D light in Light2DManager.lights)
			{
				if ((camera.cullingMask & (1 << light.gameObject.layer)) != 0)
				{
					if (light.lightType == Light2D.LightType.Global)
					{
						this.m_VisibleLights.Add(light);
					}
					else
					{
						Vector3 position = light.boundingSphere.position;
						bool culled = false;
						for (int i = 0; i < cullingParameters.cullingPlaneCount; i++)
						{
							Plane plane = cullingParameters.GetCullingPlane(i);
							if (math.dot(position, plane.normal) + plane.distance < -light.boundingSphere.radius)
							{
								culled = true;
								break;
							}
						}
						if (!culled)
						{
							this.m_VisibleLights.Add(light);
						}
					}
				}
			}
			this.m_VisibleLights.Sort((Light2D l1, Light2D l2) => l1.lightOrder - l2.lightOrder);
			this.m_VisibleShadows.Clear();
			if (ShadowCasterGroup2DManager.shadowCasterGroups != null)
			{
				foreach (ShadowCasterGroup2D group in ShadowCasterGroup2DManager.shadowCasterGroups)
				{
					List<ShadowCaster2D> shadowCasters = group.GetShadowCasters();
					if (shadowCasters != null)
					{
						foreach (ShadowCaster2D shadowCaster in shadowCasters)
						{
							foreach (Light2D light2 in this.m_VisibleLights)
							{
								if (shadowCaster.IsLit(light2) && !this.m_VisibleShadows.Contains(group))
								{
									this.m_VisibleShadows.Add(group);
									break;
								}
							}
							if (this.m_VisibleShadows.Contains(group))
							{
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x0400010C RID: 268
		private List<Light2D> m_VisibleLights = new List<Light2D>();

		// Token: 0x0400010D RID: 269
		private HashSet<ShadowCasterGroup2D> m_VisibleShadows = new HashSet<ShadowCasterGroup2D>();
	}
}
