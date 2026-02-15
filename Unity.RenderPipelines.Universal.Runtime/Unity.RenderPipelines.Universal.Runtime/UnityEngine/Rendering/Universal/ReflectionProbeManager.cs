using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000155 RID: 341
	internal struct ReflectionProbeManager : IDisposable
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00023E9F File Offset: 0x0002209F
		public RenderTexture atlasRT
		{
			get
			{
				return this.m_AtlasTexture0;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00023EA7 File Offset: 0x000220A7
		public RTHandle atlasRTHandle
		{
			get
			{
				return this.m_AtlasTexture0Handle;
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00023EB0 File Offset: 0x000220B0
		public static ReflectionProbeManager Create()
		{
			ReflectionProbeManager instance = default(ReflectionProbeManager);
			instance.Init();
			return instance;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00023ED0 File Offset: 0x000220D0
		private void Init()
		{
			int maxProbes = UniversalRenderPipeline.maxVisibleReflectionProbes;
			this.m_Resolution = 1;
			GraphicsFormat format = GraphicsFormat.B10G11R11_UFloatPack32;
			if (!SystemInfo.IsFormatSupported(format, GraphicsFormatUsage.Render))
			{
				format = GraphicsFormat.R16G16B16A16_SFloat;
			}
			this.m_AtlasTexture0 = new RenderTexture(new RenderTextureDescriptor
			{
				width = this.m_Resolution.x,
				height = this.m_Resolution.y,
				volumeDepth = 1,
				dimension = TextureDimension.Tex2D,
				graphicsFormat = format,
				useMipMap = false,
				msaaSamples = 1
			});
			this.m_AtlasTexture0.name = "URP Reflection Probe Atlas";
			this.m_AtlasTexture0.filterMode = FilterMode.Bilinear;
			this.m_AtlasTexture0.hideFlags = HideFlags.HideAndDontSave;
			this.m_AtlasTexture0.Create();
			this.m_AtlasTexture0Handle = RTHandles.Alloc(this.m_AtlasTexture0, true);
			this.m_AtlasTexture1 = new RenderTexture(this.m_AtlasTexture0.descriptor);
			this.m_AtlasTexture1.name = "URP Reflection Probe Atlas";
			this.m_AtlasTexture1.filterMode = FilterMode.Bilinear;
			this.m_AtlasTexture1.hideFlags = HideFlags.HideAndDontSave;
			this.m_AtlasAllocator = new BuddyAllocator(math.floorlog2(SystemInfo.maxTextureSize) - 2, 2, Allocator.Persistent);
			this.m_Cache = new Dictionary<int, ReflectionProbeManager.CachedProbe>(maxProbes);
			this.m_WarningCache = new Dictionary<int, int>(maxProbes);
			this.m_NeedsUpdate = new List<int>(maxProbes);
			this.m_NeedsRemove = new List<int>(maxProbes);
			this.m_BoxMax = new Vector4[maxProbes];
			this.m_BoxMin = new Vector4[maxProbes];
			this.m_ProbePosition = new Vector4[maxProbes];
			this.m_MipScaleOffset = new Vector4[maxProbes * 7];
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00024060 File Offset: 0x00022260
		public unsafe void UpdateGpuData(CommandBuffer cmd, ref CullingResults cullResults)
		{
			NativeArray<VisibleReflectionProbe> probes = cullResults.visibleReflectionProbes;
			int probeCount = math.min(probes.Length, UniversalRenderPipeline.maxVisibleReflectionProbes);
			int frameIndex = Time.renderedFrameCount;
			foreach (KeyValuePair<int, ReflectionProbeManager.CachedProbe> keyValuePair in this.m_Cache)
			{
				int num;
				ReflectionProbeManager.CachedProbe cachedProbe5;
				keyValuePair.Deconstruct(out num, out cachedProbe5);
				int id = num;
				ReflectionProbeManager.CachedProbe cachedProbe = cachedProbe5;
				if (Math.Abs(cachedProbe.lastUsed - frameIndex) > 1 || !cachedProbe.texture || cachedProbe.size != cachedProbe.texture.width)
				{
					this.m_NeedsRemove.Add(id);
					for (int i = 0; i < 7; i++)
					{
						if (*((ref cachedProbe.dataIndices.FixedElementField) + (IntPtr)i * 4) != -1)
						{
							this.m_AtlasAllocator.Free(new BuddyAllocation(*((ref cachedProbe.levels.FixedElementField) + (IntPtr)i * 4), *((ref cachedProbe.dataIndices.FixedElementField) + (IntPtr)i * 4)));
						}
					}
				}
			}
			foreach (int probeIndex in this.m_NeedsRemove)
			{
				this.m_Cache.Remove(probeIndex);
			}
			this.m_NeedsRemove.Clear();
			foreach (KeyValuePair<int, int> keyValuePair2 in this.m_WarningCache)
			{
				int num;
				int num2;
				keyValuePair2.Deconstruct(out num, out num2);
				int id2 = num;
				if (Math.Abs(num2 - frameIndex) > 1)
				{
					this.m_NeedsRemove.Add(id2);
				}
			}
			foreach (int probeIndex2 in this.m_NeedsRemove)
			{
				this.m_WarningCache.Remove(probeIndex2);
			}
			this.m_NeedsRemove.Clear();
			bool showFullWarning = false;
			int2 requiredAtlasSize = math.int2(0, 0);
			for (int probeIndex3 = 0; probeIndex3 < probeCount; probeIndex3++)
			{
				VisibleReflectionProbe probe = probes[probeIndex3];
				Texture texture = probe.texture;
				int id3 = probe.reflectionProbe.GetInstanceID();
				ReflectionProbeManager.CachedProbe cachedProbe2;
				bool wasCached = this.m_Cache.TryGetValue(id3, out cachedProbe2);
				if (texture)
				{
					if (!wasCached)
					{
						cachedProbe2.size = texture.width;
						int mipCount = math.ceillog2(cachedProbe2.size * 4) + 1;
						int level = this.m_AtlasAllocator.levelCount + 2 - mipCount;
						cachedProbe2.mipCount = math.min(mipCount, 7);
						cachedProbe2.texture = texture;
						int mip;
						for (mip = 0; mip < cachedProbe2.mipCount; mip++)
						{
							int mipLevel = math.min(level + mip, this.m_AtlasAllocator.levelCount - 1);
							BuddyAllocation allocation;
							if (!this.m_AtlasAllocator.TryAllocate(mipLevel, out allocation))
							{
								break;
							}
							*((ref cachedProbe2.levels.FixedElementField) + (IntPtr)mip * 4) = allocation.level;
							*((ref cachedProbe2.dataIndices.FixedElementField) + (IntPtr)mip * 4) = allocation.index;
							int4 scaleOffset = (int4)(this.GetScaleOffset(mipLevel, allocation.index, true, false) * this.m_Resolution.xyxy);
							requiredAtlasSize = math.max(requiredAtlasSize, scaleOffset.zw + scaleOffset.xy);
						}
						if (mip < cachedProbe2.mipCount)
						{
							if (!this.m_WarningCache.ContainsKey(id3))
							{
								showFullWarning = true;
							}
							this.m_WarningCache[id3] = frameIndex;
							for (int j = 0; j < mip; j++)
							{
								this.m_AtlasAllocator.Free(new BuddyAllocation(*((ref cachedProbe2.levels.FixedElementField) + (IntPtr)j * 4), *((ref cachedProbe2.dataIndices.FixedElementField) + (IntPtr)j * 4)));
							}
							for (int k = 0; k < 7; k++)
							{
								*((ref cachedProbe2.dataIndices.FixedElementField) + (IntPtr)k * 4) = -1;
							}
							goto IL_04A1;
						}
						while (mip < 7)
						{
							*((ref cachedProbe2.dataIndices.FixedElementField) + (IntPtr)mip * 4) = -1;
							mip++;
						}
					}
					if ((!wasCached || cachedProbe2.updateCount != texture.updateCount) | (cachedProbe2.hdrData != probe.hdrData))
					{
						cachedProbe2.updateCount = texture.updateCount;
						this.m_NeedsUpdate.Add(id3);
					}
					if (probe.reflectionProbe.refreshMode == ReflectionProbeRefreshMode.EveryFrame)
					{
						cachedProbe2.lastUsed = -1;
					}
					else
					{
						cachedProbe2.lastUsed = frameIndex;
					}
					cachedProbe2.hdrData = probe.hdrData;
					this.m_Cache[id3] = cachedProbe2;
				}
				IL_04A1:;
			}
			if (math.any(this.m_Resolution < requiredAtlasSize))
			{
				requiredAtlasSize = math.max(this.m_Resolution, math.ceilpow2(requiredAtlasSize));
				RenderTextureDescriptor desc = this.m_AtlasTexture0.descriptor;
				desc.width = requiredAtlasSize.x;
				desc.height = requiredAtlasSize.y;
				this.m_AtlasTexture1.width = requiredAtlasSize.x;
				this.m_AtlasTexture1.height = requiredAtlasSize.y;
				this.m_AtlasTexture1.Create();
				if (this.m_AtlasTexture0.width != 1)
				{
					if (SystemInfo.copyTextureSupport != CopyTextureSupport.None)
					{
						Graphics.CopyTexture(this.m_AtlasTexture0, 0, 0, 0, 0, this.m_Resolution.x, this.m_Resolution.y, this.m_AtlasTexture1, 0, 0, 0, 0);
					}
					else
					{
						Graphics.Blit(this.m_AtlasTexture0, this.m_AtlasTexture1, this.m_Resolution / requiredAtlasSize, Vector2.zero);
					}
				}
				this.m_AtlasTexture0.Release();
				RenderTexture atlasTexture = this.m_AtlasTexture1;
				RenderTexture atlasTexture2 = this.m_AtlasTexture0;
				this.m_AtlasTexture0 = atlasTexture;
				this.m_AtlasTexture1 = atlasTexture2;
				this.m_Resolution = requiredAtlasSize;
			}
			int skipCount = 0;
			for (int probeIndex4 = 0; probeIndex4 < probeCount; probeIndex4++)
			{
				VisibleReflectionProbe probe2 = probes[probeIndex4];
				int id4 = probe2.reflectionProbe.GetInstanceID();
				int dataIndex = probeIndex4 - skipCount;
				ReflectionProbeManager.CachedProbe cachedProbe3;
				if (!this.m_Cache.TryGetValue(id4, out cachedProbe3) || !probe2.texture)
				{
					skipCount++;
				}
				else
				{
					this.m_BoxMax[dataIndex] = new Vector4(probe2.bounds.max.x, probe2.bounds.max.y, probe2.bounds.max.z, probe2.blendDistance);
					this.m_BoxMin[dataIndex] = new Vector4(probe2.bounds.min.x, probe2.bounds.min.y, probe2.bounds.min.z, (float)probe2.importance);
					this.m_ProbePosition[dataIndex] = new Vector4(probe2.localToWorldMatrix.m03, probe2.localToWorldMatrix.m13, probe2.localToWorldMatrix.m23, (float)((probe2.isBoxProjection ? 1 : (-1)) * cachedProbe3.mipCount));
					for (int l = 0; l < cachedProbe3.mipCount; l++)
					{
						this.m_MipScaleOffset[dataIndex * 7 + l] = this.GetScaleOffset(*((ref cachedProbe3.levels.FixedElementField) + (IntPtr)l * 4), *((ref cachedProbe3.dataIndices.FixedElementField) + (IntPtr)l * 4), false, false);
					}
				}
			}
			if (showFullWarning)
			{
				Debug.LogWarning("A number of reflection probes have been skipped due to the reflection probe atlas being full.\nTo fix this, you can decrease the number or resolution of probes.");
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.UpdateReflectionProbeAtlas)))
			{
				cmd.SetRenderTarget(this.m_AtlasTexture0);
				foreach (int probeId in this.m_NeedsUpdate)
				{
					ReflectionProbeManager.CachedProbe cachedProbe4 = this.m_Cache[probeId];
					for (int mip2 = 0; mip2 < cachedProbe4.mipCount; mip2++)
					{
						int level2 = *((ref cachedProbe4.levels.FixedElementField) + (IntPtr)mip2 * 4);
						int dataIndex2 = *((ref cachedProbe4.dataIndices.FixedElementField) + (IntPtr)mip2 * 4);
						float4 scaleBias = this.GetScaleOffset(level2, dataIndex2, true, !SystemInfo.graphicsUVStartsAtTop);
						int sizeWithoutPadding = (1 << this.m_AtlasAllocator.levelCount + 1 - level2) - 2;
						Blitter.BlitCubeToOctahedral2DQuadWithPadding(cmd, cachedProbe4.texture, new Vector2((float)sizeWithoutPadding, (float)sizeWithoutPadding), scaleBias, mip2, true, 2, new Vector4?(cachedProbe4.hdrData));
					}
				}
				cmd.SetGlobalVectorArray(ReflectionProbeManager.ShaderProperties.BoxMin, this.m_BoxMin);
				cmd.SetGlobalVectorArray(ReflectionProbeManager.ShaderProperties.BoxMax, this.m_BoxMax);
				cmd.SetGlobalVectorArray(ReflectionProbeManager.ShaderProperties.ProbePosition, this.m_ProbePosition);
				cmd.SetGlobalVectorArray(ReflectionProbeManager.ShaderProperties.MipScaleOffset, this.m_MipScaleOffset);
				cmd.SetGlobalFloat(ReflectionProbeManager.ShaderProperties.Count, (float)(probeCount - skipCount));
				cmd.SetGlobalTexture(ReflectionProbeManager.ShaderProperties.Atlas, this.m_AtlasTexture0);
			}
			this.m_NeedsUpdate.Clear();
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00024A38 File Offset: 0x00022C38
		private float4 GetScaleOffset(int level, int dataIndex, bool includePadding, bool yflip)
		{
			int size = 1 << this.m_AtlasAllocator.levelCount + 1 - level;
			uint2 @uint = SpaceFillingCurves.DecodeMorton2D((uint)dataIndex);
			float2 scale = (float)(size - (includePadding ? 0 : 2)) / this.m_Resolution;
			float2 bias = (@uint * (float)size + (float)(includePadding ? 0 : 1)) / this.m_Resolution;
			if (yflip)
			{
				bias.y = 1f - bias.y - scale.y;
			}
			return math.float4(scale, bias);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00024ACC File Offset: 0x00022CCC
		public void Dispose()
		{
			if (this.m_AtlasTexture0)
			{
				this.m_AtlasTexture0.Release();
				this.m_AtlasTexture0Handle.Release();
			}
			Object.DestroyImmediate(this.m_AtlasTexture0);
			Object.DestroyImmediate(this.m_AtlasTexture1);
			this = default(ReflectionProbeManager);
		}

		// Token: 0x040007DA RID: 2010
		private int2 m_Resolution;

		// Token: 0x040007DB RID: 2011
		private RenderTexture m_AtlasTexture0;

		// Token: 0x040007DC RID: 2012
		private RenderTexture m_AtlasTexture1;

		// Token: 0x040007DD RID: 2013
		private RTHandle m_AtlasTexture0Handle;

		// Token: 0x040007DE RID: 2014
		private BuddyAllocator m_AtlasAllocator;

		// Token: 0x040007DF RID: 2015
		private Dictionary<int, ReflectionProbeManager.CachedProbe> m_Cache;

		// Token: 0x040007E0 RID: 2016
		private Dictionary<int, int> m_WarningCache;

		// Token: 0x040007E1 RID: 2017
		private List<int> m_NeedsUpdate;

		// Token: 0x040007E2 RID: 2018
		private List<int> m_NeedsRemove;

		// Token: 0x040007E3 RID: 2019
		private Vector4[] m_BoxMax;

		// Token: 0x040007E4 RID: 2020
		private Vector4[] m_BoxMin;

		// Token: 0x040007E5 RID: 2021
		private Vector4[] m_ProbePosition;

		// Token: 0x040007E6 RID: 2022
		private Vector4[] m_MipScaleOffset;

		// Token: 0x040007E7 RID: 2023
		private const int k_MaxMipCount = 7;

		// Token: 0x040007E8 RID: 2024
		private const string k_ReflectionProbeAtlasName = "URP Reflection Probe Atlas";

		// Token: 0x02000156 RID: 342
		private struct CachedProbe
		{
			// Token: 0x040007E9 RID: 2025
			public uint updateCount;

			// Token: 0x040007EA RID: 2026
			public Hash128 imageContentsHash;

			// Token: 0x040007EB RID: 2027
			public int size;

			// Token: 0x040007EC RID: 2028
			public int mipCount;

			// Token: 0x040007ED RID: 2029
			[FixedBuffer(typeof(int), 7)]
			public ReflectionProbeManager.CachedProbe.<dataIndices>e__FixedBuffer dataIndices;

			// Token: 0x040007EE RID: 2030
			[FixedBuffer(typeof(int), 7)]
			public ReflectionProbeManager.CachedProbe.<levels>e__FixedBuffer levels;

			// Token: 0x040007EF RID: 2031
			public Texture texture;

			// Token: 0x040007F0 RID: 2032
			public int lastUsed;

			// Token: 0x040007F1 RID: 2033
			public Vector4 hdrData;

			// Token: 0x02000157 RID: 343
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 28)]
			public struct <dataIndices>e__FixedBuffer
			{
				// Token: 0x040007F2 RID: 2034
				public int FixedElementField;
			}

			// Token: 0x02000158 RID: 344
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 28)]
			public struct <levels>e__FixedBuffer
			{
				// Token: 0x040007F3 RID: 2035
				public int FixedElementField;
			}
		}

		// Token: 0x02000159 RID: 345
		private static class ShaderProperties
		{
			// Token: 0x040007F4 RID: 2036
			public static readonly int BoxMin = Shader.PropertyToID("urp_ReflProbes_BoxMin");

			// Token: 0x040007F5 RID: 2037
			public static readonly int BoxMax = Shader.PropertyToID("urp_ReflProbes_BoxMax");

			// Token: 0x040007F6 RID: 2038
			public static readonly int ProbePosition = Shader.PropertyToID("urp_ReflProbes_ProbePosition");

			// Token: 0x040007F7 RID: 2039
			public static readonly int MipScaleOffset = Shader.PropertyToID("urp_ReflProbes_MipScaleOffset");

			// Token: 0x040007F8 RID: 2040
			public static readonly int Count = Shader.PropertyToID("urp_ReflProbes_Count");

			// Token: 0x040007F9 RID: 2041
			public static readonly int Atlas = Shader.PropertyToID("urp_ReflProbes_Atlas");
		}
	}
}
