using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000106 RID: 262
	internal struct AdditionalLightsShadowAtlasLayout
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x00016E48 File Offset: 0x00015048
		public AdditionalLightsShadowAtlasLayout(UniversalLightData lightData, UniversalShadowData shadowData, UniversalCameraData cameraData)
		{
			bool useStructuredBuffer = RenderingUtils.useStructuredBuffer;
			NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
			int numberOfVisibleLights = visibleLights.Length;
			if (AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas == null)
			{
				AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas = new List<RectInt>();
			}
			if (AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests == null)
			{
				AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests = new List<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest>();
			}
			if (AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance == null || AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance.Length < numberOfVisibleLights)
			{
				AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance = new float[numberOfVisibleLights];
			}
			if (AdditionalLightsShadowAtlasLayout.s_CompareShadowResolutionRequest == null)
			{
				AdditionalLightsShadowAtlasLayout.s_CompareShadowResolutionRequest = AdditionalLightsShadowAtlasLayout.CreateCompareShadowResolutionRequesPredicate();
			}
			if (!useStructuredBuffer)
			{
				int newCapacity = UniversalRenderPipeline.maxVisibleAdditionalLights;
				if (AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.Capacity < newCapacity)
				{
					AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.Capacity = newCapacity;
				}
				if (AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests.Count < numberOfVisibleLights)
				{
					AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests.Capacity = numberOfVisibleLights;
					int diff = numberOfVisibleLights - AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests.Count + 1;
					for (int i = 0; i < diff; i++)
					{
						AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests.Add(default(AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest));
					}
				}
			}
			AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.Clear();
			ushort totalShadowResolutionRequestsCount = 0;
			for (int visibleLightIndex = 0; visibleLightIndex < visibleLights.Length; visibleLightIndex++)
			{
				if (visibleLightIndex == lightData.mainLightIndex)
				{
					AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[visibleLightIndex] = float.MaxValue;
				}
				else
				{
					ref VisibleLight ptr = ref visibleLights.UnsafeElementAt(visibleLightIndex);
					Light light = ptr.light;
					LightType lightType = ptr.lightType;
					LightShadows lightShadows = light.shadows;
					float shadowStrength = light.shadowStrength;
					if (!ShadowUtils.IsValidShadowCastingLight(lightData, visibleLightIndex, lightType, lightShadows, shadowStrength))
					{
						AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[visibleLightIndex] = float.MaxValue;
					}
					else
					{
						bool softShadows = lightShadows == LightShadows.Soft;
						bool pointLightShadow = lightType == LightType.Point;
						ushort visibleLightIndexUshort = (ushort)visibleLightIndex;
						ushort requestedResolution = (ushort)shadowData.resolution[visibleLightIndex];
						int shadowSlicesCountForThisLight = ShadowUtils.GetPunctualLightShadowSlicesCount(in lightType);
						ushort perLightShadowSliceIndex = 0;
						while ((int)perLightShadowSliceIndex < shadowSlicesCountForThisLight)
						{
							if ((int)totalShadowResolutionRequestsCount >= AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests.Count)
							{
								AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests.Add(default(AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest));
							}
							AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest request = AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests[(int)totalShadowResolutionRequestsCount];
							request.visibleLightIndex = visibleLightIndexUshort;
							request.perLightShadowSliceIndex = perLightShadowSliceIndex;
							request.requestedResolution = requestedResolution;
							request.softShadow = softShadows;
							request.pointLightShadow = pointLightShadow;
							AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests[(int)totalShadowResolutionRequestsCount] = request;
							totalShadowResolutionRequestsCount += 1;
							perLightShadowSliceIndex += 1;
						}
						AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[visibleLightIndex] = (cameraData.worldSpaceCameraPos - light.transform.position).sqrMagnitude;
					}
				}
			}
			if (AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests == null || AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests.Length < (int)totalShadowResolutionRequestsCount)
			{
				AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests = new AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest[(int)totalShadowResolutionRequestsCount];
			}
			for (int j = 0; j < (int)totalShadowResolutionRequestsCount; j++)
			{
				AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests[j] = AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests[j];
			}
			using (new ProfilingScope(Sorting.s_QuickSortSampler))
			{
				Sorting.QuickSort<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest>(AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests, 0, (int)(totalShadowResolutionRequestsCount - 1), AdditionalLightsShadowAtlasLayout.s_CompareShadowResolutionRequest);
			}
			this.m_SortedShadowResolutionRequests = new NativeArray<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest>(AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests, Allocator.Temp);
			int totalShadowSlicesCount = (useStructuredBuffer ? ((int)totalShadowResolutionRequestsCount) : Math.Min((int)totalShadowResolutionRequestsCount, UniversalRenderPipeline.maxVisibleAdditionalLights));
			int atlasSize = shadowData.additionalLightsShadowmapWidth;
			bool allShadowsAfterStartIndexHaveEnoughResolution = false;
			int estimatedScaleFactor = 1;
			while (!allShadowsAfterStartIndexHaveEnoughResolution && totalShadowSlicesCount > 0)
			{
				AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest request2 = this.m_SortedShadowResolutionRequests[totalShadowSlicesCount - 1];
				estimatedScaleFactor = AdditionalLightsShadowAtlasLayout.EstimateScaleFactorNeededToFitAllShadowsInAtlas(in this.m_SortedShadowResolutionRequests, totalShadowSlicesCount, atlasSize);
				if ((int)request2.requestedResolution >= estimatedScaleFactor * ShadowUtils.MinimalPunctualLightShadowResolution(request2.softShadow))
				{
					allShadowsAfterStartIndexHaveEnoughResolution = true;
				}
				else
				{
					int num = totalShadowSlicesCount;
					LightType lightType2 = (request2.pointLightShadow ? LightType.Point : LightType.Spot);
					totalShadowSlicesCount = num - ShadowUtils.GetPunctualLightShadowSlicesCount(in lightType2);
				}
			}
			for (int sortedArrayIndex = totalShadowSlicesCount; sortedArrayIndex < this.m_SortedShadowResolutionRequests.Length; sortedArrayIndex++)
			{
				this.m_SortedShadowResolutionRequests[sortedArrayIndex] = default(AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest);
			}
			this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex = new NativeArray<int>(visibleLights.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int visibleLightIndex2 = 0; visibleLightIndex2 < this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex.Length; visibleLightIndex2++)
			{
				this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[visibleLightIndex2] = -1;
			}
			for (int sortedArrayIndex2 = totalShadowSlicesCount - 1; sortedArrayIndex2 >= 0; sortedArrayIndex2--)
			{
				int visibleLightIndex3 = (int)AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests[sortedArrayIndex2].visibleLightIndex;
				this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[visibleLightIndex3] = sortedArrayIndex2;
			}
			bool allShadowSlicesFitInAtlas = false;
			bool tooManyShadows = false;
			int shadowSlicesScaleFactor = estimatedScaleFactor;
			while (!allShadowSlicesFitInAtlas && !tooManyShadows)
			{
				AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.Clear();
				AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.Add(new RectInt(0, 0, atlasSize, atlasSize));
				allShadowSlicesFitInAtlas = true;
				for (int shadowRequestIndex = 0; shadowRequestIndex < totalShadowSlicesCount; shadowRequestIndex++)
				{
					int resolution = (int)this.m_SortedShadowResolutionRequests[shadowRequestIndex].requestedResolution / shadowSlicesScaleFactor;
					if (resolution < ShadowUtils.MinimalPunctualLightShadowResolution(this.m_SortedShadowResolutionRequests[shadowRequestIndex].softShadow))
					{
						tooManyShadows = true;
						break;
					}
					bool foundSpaceInAtlas = false;
					for (int unusedAtlasSquareAreaIndex = 0; unusedAtlasSquareAreaIndex < AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.Count; unusedAtlasSquareAreaIndex++)
					{
						RectInt atlasArea = AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas[unusedAtlasSquareAreaIndex];
						int atlasAreaWidth = atlasArea.width;
						if (atlasAreaWidth >= resolution)
						{
							int atlasAreaHeight = atlasArea.height;
							int atlasAreaX = atlasArea.x;
							int atlasAreaY = atlasArea.y;
							ref AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest ptr2 = ref this.m_SortedShadowResolutionRequests.UnsafeElementAtMutable(shadowRequestIndex);
							ptr2.offsetX = (ushort)atlasAreaX;
							ptr2.offsetY = (ushort)atlasAreaY;
							ptr2.allocatedResolution = (ushort)resolution;
							AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.RemoveAt(unusedAtlasSquareAreaIndex);
							int remainingShadowRequestsCount = totalShadowSlicesCount - shadowRequestIndex - 1;
							int newSquareAreasCount = 0;
							int newSquareAreaWidth = resolution;
							int newSquareAreaHeight = resolution;
							int newSquareAreaX = atlasAreaX;
							int newSquareAreaY = atlasAreaY;
							while (newSquareAreasCount < remainingShadowRequestsCount)
							{
								newSquareAreaX += newSquareAreaWidth;
								if (newSquareAreaX + newSquareAreaWidth > atlasAreaX + atlasAreaWidth)
								{
									newSquareAreaX = atlasAreaX;
									newSquareAreaY += newSquareAreaHeight;
									if (newSquareAreaY + newSquareAreaHeight > atlasAreaY + atlasAreaHeight)
									{
										break;
									}
								}
								AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas.Insert(unusedAtlasSquareAreaIndex + newSquareAreasCount, new RectInt(newSquareAreaX, newSquareAreaY, newSquareAreaWidth, newSquareAreaHeight));
								newSquareAreasCount++;
							}
							foundSpaceInAtlas = true;
							break;
						}
					}
					if (!foundSpaceInAtlas)
					{
						allShadowSlicesFitInAtlas = false;
						break;
					}
				}
				if (!allShadowSlicesFitInAtlas && !tooManyShadows)
				{
					shadowSlicesScaleFactor *= 2;
				}
			}
			this.m_TooManyShadowMaps = tooManyShadows;
			this.m_ShadowSlicesScaleFactor = shadowSlicesScaleFactor;
			this.m_TotalShadowSlicesCount = totalShadowSlicesCount;
			this.m_TotalShadowResolutionRequestCount = (int)totalShadowResolutionRequestsCount;
			this.m_AtlasSize = atlasSize;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00017420 File Offset: 0x00015620
		public int GetTotalShadowSlicesCount()
		{
			return this.m_TotalShadowSlicesCount;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00017428 File Offset: 0x00015628
		public int GetTotalShadowResolutionRequestCount()
		{
			return this.m_TotalShadowResolutionRequestCount;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00017430 File Offset: 0x00015630
		public bool HasTooManyShadowMaps()
		{
			return this.m_TooManyShadowMaps;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00017438 File Offset: 0x00015638
		public int GetShadowSlicesScaleFactor()
		{
			return this.m_ShadowSlicesScaleFactor;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00017440 File Offset: 0x00015640
		public int GetAtlasSize()
		{
			return this.m_AtlasSize;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00017448 File Offset: 0x00015648
		public bool HasSpaceForLight(int originalVisibleLightIndex)
		{
			return this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[originalVisibleLightIndex] != -1;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001745C File Offset: 0x0001565C
		public AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest GetSortedShadowResolutionRequest(int sortedShadowResolutionRequestIndex)
		{
			return this.m_SortedShadowResolutionRequests[sortedShadowResolutionRequestIndex];
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001746C File Offset: 0x0001566C
		public AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest GetSliceShadowResolutionRequest(int originalVisibleLightIndex, int sliceIndex)
		{
			int sortedShadowResolutionRequestIndex = this.m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex[originalVisibleLightIndex];
			return this.m_SortedShadowResolutionRequests[sortedShadowResolutionRequestIndex + sliceIndex];
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00017494 File Offset: 0x00015694
		public static void ClearStaticCaches()
		{
			AdditionalLightsShadowAtlasLayout.s_UnusedAtlasSquareAreas = null;
			AdditionalLightsShadowAtlasLayout.s_ShadowResolutionRequests = null;
			AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance = null;
			AdditionalLightsShadowAtlasLayout.s_CompareShadowResolutionRequest = null;
			AdditionalLightsShadowAtlasLayout.s_SortedShadowResolutionRequests = null;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000174B4 File Offset: 0x000156B4
		private static int EstimateScaleFactorNeededToFitAllShadowsInAtlas(in NativeArray<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest> shadowResolutionRequests, int endIndex, int atlasSize)
		{
			long totalTexelsInShadowAtlas = (long)(atlasSize * atlasSize);
			long totalTexelsInShadowRequests = 0L;
			for (int shadowRequestIndex = 0; shadowRequestIndex < endIndex; shadowRequestIndex++)
			{
				long num = totalTexelsInShadowRequests;
				NativeArray<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest> nativeArray = shadowResolutionRequests;
				ushort requestedResolution = nativeArray[shadowRequestIndex].requestedResolution;
				nativeArray = shadowResolutionRequests;
				totalTexelsInShadowRequests = num + (long)(requestedResolution * nativeArray[shadowRequestIndex].requestedResolution);
			}
			int estimatedScaleFactor = 1;
			while (totalTexelsInShadowRequests > totalTexelsInShadowAtlas * (long)estimatedScaleFactor * (long)estimatedScaleFactor)
			{
				estimatedScaleFactor *= 2;
			}
			return estimatedScaleFactor;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00017517 File Offset: 0x00015717
		private static Func<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, int> CreateCompareShadowResolutionRequesPredicate()
		{
			return delegate(AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest curr, AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest other)
			{
				if (curr.requestedResolution <= other.requestedResolution && (curr.requestedResolution != other.requestedResolution || curr.softShadow || !other.softShadow) && (curr.requestedResolution != other.requestedResolution || curr.softShadow != other.softShadow || AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[(int)curr.visibleLightIndex] >= AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[(int)other.visibleLightIndex]) && (curr.requestedResolution != other.requestedResolution || curr.softShadow != other.softShadow || AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[(int)curr.visibleLightIndex] != AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[(int)other.visibleLightIndex] || curr.visibleLightIndex >= other.visibleLightIndex) && (curr.requestedResolution != other.requestedResolution || curr.softShadow != other.softShadow || AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[(int)curr.visibleLightIndex] != AdditionalLightsShadowAtlasLayout.s_VisibleLightIndexToCameraSquareDistance[(int)other.visibleLightIndex] || curr.visibleLightIndex != other.visibleLightIndex || curr.perLightShadowSliceIndex >= other.perLightShadowSliceIndex))
				{
					return 1;
				}
				return -1;
			};
		}

		// Token: 0x04000584 RID: 1412
		private static List<RectInt> s_UnusedAtlasSquareAreas;

		// Token: 0x04000585 RID: 1413
		private static List<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest> s_ShadowResolutionRequests;

		// Token: 0x04000586 RID: 1414
		private static float[] s_VisibleLightIndexToCameraSquareDistance;

		// Token: 0x04000587 RID: 1415
		private static Func<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest, int> s_CompareShadowResolutionRequest;

		// Token: 0x04000588 RID: 1416
		private static AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest[] s_SortedShadowResolutionRequests;

		// Token: 0x04000589 RID: 1417
		private NativeArray<AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest> m_SortedShadowResolutionRequests;

		// Token: 0x0400058A RID: 1418
		private NativeArray<int> m_VisibleLightIndexToSortedShadowResolutionRequestsFirstSliceIndex;

		// Token: 0x0400058B RID: 1419
		private int m_TotalShadowSlicesCount;

		// Token: 0x0400058C RID: 1420
		private int m_TotalShadowResolutionRequestCount;

		// Token: 0x0400058D RID: 1421
		private bool m_TooManyShadowMaps;

		// Token: 0x0400058E RID: 1422
		private int m_ShadowSlicesScaleFactor;

		// Token: 0x0400058F RID: 1423
		private int m_AtlasSize;

		// Token: 0x02000107 RID: 263
		internal struct ShadowResolutionRequest
		{
			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600061E RID: 1566 RVA: 0x00017538 File Offset: 0x00015738
			// (set) Token: 0x0600061F RID: 1567 RVA: 0x00017550 File Offset: 0x00015750
			public bool softShadow
			{
				get
				{
					return this.m_ShadowProperties.HasFlag(AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.SoftShadow);
				}
				set
				{
					if (value)
					{
						this.m_ShadowProperties |= AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.SoftShadow;
						return;
					}
					this.m_ShadowProperties &= ~AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.SoftShadow;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000620 RID: 1568 RVA: 0x00017576 File Offset: 0x00015776
			// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001758E File Offset: 0x0001578E
			public bool pointLightShadow
			{
				get
				{
					return this.m_ShadowProperties.HasFlag(AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.PointLightShadow);
				}
				set
				{
					if (value)
					{
						this.m_ShadowProperties |= AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.PointLightShadow;
						return;
					}
					this.m_ShadowProperties &= ~AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions.PointLightShadow;
				}
			}

			// Token: 0x04000590 RID: 1424
			public ushort visibleLightIndex;

			// Token: 0x04000591 RID: 1425
			public ushort perLightShadowSliceIndex;

			// Token: 0x04000592 RID: 1426
			public ushort requestedResolution;

			// Token: 0x04000593 RID: 1427
			public ushort offsetX;

			// Token: 0x04000594 RID: 1428
			public ushort offsetY;

			// Token: 0x04000595 RID: 1429
			public ushort allocatedResolution;

			// Token: 0x04000596 RID: 1430
			private AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest.SettingsOptions m_ShadowProperties;

			// Token: 0x02000108 RID: 264
			[Flags]
			private enum SettingsOptions : ushort
			{
				// Token: 0x04000598 RID: 1432
				None = 0,
				// Token: 0x04000599 RID: 1433
				SoftShadow = 1,
				// Token: 0x0400059A RID: 1434
				PointLightShadow = 2,
				// Token: 0x0400059B RID: 1435
				All = 65535
			}
		}
	}
}
