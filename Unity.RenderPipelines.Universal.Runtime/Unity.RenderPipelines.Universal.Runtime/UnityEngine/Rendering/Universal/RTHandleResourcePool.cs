using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000154 RID: 340
	internal class RTHandleResourcePool
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00023913 File Offset: 0x00021B13
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0002391A File Offset: 0x00021B1A
		internal int staleResourceCapacity
		{
			get
			{
				return RTHandleResourcePool.s_StaleResourceMaxCapacity;
			}
			set
			{
				if (RTHandleResourcePool.s_StaleResourceMaxCapacity != value)
				{
					RTHandleResourcePool.s_StaleResourceMaxCapacity = value;
					this.Cleanup();
				}
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00023930 File Offset: 0x00021B30
		internal bool AddResourceToPool(in TextureDesc texDesc, RTHandle resource, int currentFrameIndex)
		{
			if (RTHandleResourcePool.s_CurrentStaleResourceCount >= RTHandleResourcePool.s_StaleResourceMaxCapacity)
			{
				return false;
			}
			int hashCode = this.GetHashCodeWithNameHash(in texDesc);
			SortedList<int, ValueTuple<RTHandle, int>> list;
			if (!this.m_ResourcePool.TryGetValue(hashCode, out list))
			{
				list = new SortedList<int, ValueTuple<RTHandle, int>>(RTHandleResourcePool.s_StaleResourceMaxCapacity);
				this.m_ResourcePool.Add(hashCode, list);
			}
			list.Add(resource.GetInstanceID(), new ValueTuple<RTHandle, int>(resource, currentFrameIndex));
			RTHandleResourcePool.s_CurrentStaleResourceCount++;
			return true;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0002399C File Offset: 0x00021B9C
		internal bool TryGetResource(in TextureDesc texDesc, out RTHandle resource, bool usepool = true)
		{
			int hashCode = this.GetHashCodeWithNameHash(in texDesc);
			SortedList<int, ValueTuple<RTHandle, int>> list;
			if (usepool && this.m_ResourcePool.TryGetValue(hashCode, out list) && list.Count > 0)
			{
				resource = list.Values[list.Count - 1].Item1;
				list.RemoveAt(list.Count - 1);
				RTHandleResourcePool.s_CurrentStaleResourceCount--;
				return true;
			}
			resource = null;
			return false;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00023A08 File Offset: 0x00021C08
		internal void Cleanup()
		{
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<RTHandle, int>>> kvp in this.m_ResourcePool)
			{
				foreach (KeyValuePair<int, ValueTuple<RTHandle, int>> res in kvp.Value)
				{
					res.Value.Item1.Release();
				}
			}
			this.m_ResourcePool.Clear();
			RTHandleResourcePool.s_CurrentStaleResourceCount = 0;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00023AAC File Offset: 0x00021CAC
		protected static bool ShouldReleaseResource(int lastUsedFrameIndex, int currentFrameIndex)
		{
			return lastUsedFrameIndex + RTHandleResourcePool.s_StaleResourceLifetime < currentFrameIndex;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00023AB8 File Offset: 0x00021CB8
		internal void PurgeUnusedResources(int currentFrameIndex)
		{
			this.m_RemoveList.Clear();
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<RTHandle, int>>> kvp in this.m_ResourcePool)
			{
				SortedList<int, ValueTuple<RTHandle, int>> list = kvp.Value;
				IList<int> keys = list.Keys;
				IList<ValueTuple<RTHandle, int>> values = list.Values;
				for (int i = 0; i < list.Count; i++)
				{
					ValueTuple<RTHandle, int> value = values[i];
					if (RTHandleResourcePool.ShouldReleaseResource(value.Item2, currentFrameIndex))
					{
						value.Item1.Release();
						this.m_RemoveList.Add(keys[i]);
						RTHandleResourcePool.s_CurrentStaleResourceCount--;
					}
				}
				foreach (int key in this.m_RemoveList)
				{
					list.Remove(key);
				}
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00023BCC File Offset: 0x00021DCC
		internal void LogDebugInfo()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("RTHandleResourcePool for frame {0}, Total stale resources {1}", Time.frameCount, RTHandleResourcePool.s_CurrentStaleResourceCount);
			sb.AppendLine();
			foreach (KeyValuePair<int, SortedList<int, ValueTuple<RTHandle, int>>> kvp in this.m_ResourcePool)
			{
				SortedList<int, ValueTuple<RTHandle, int>> list = kvp.Value;
				IList<int> keys = list.Keys;
				IList<ValueTuple<RTHandle, int>> values = list.Values;
				for (int i = 0; i < list.Count; i++)
				{
					ValueTuple<RTHandle, int> value = values[i];
					sb.AppendFormat("Resrouce in pool: Name {0} Last active frame index {1} Size {2} x {3} x {4}", new object[]
					{
						value.Item1.name,
						value.Item2,
						value.Item1.rt.descriptor.width,
						value.Item1.rt.descriptor.height,
						value.Item1.rt.descriptor.volumeDepth
					});
					sb.AppendLine();
				}
			}
			Debug.Log(sb);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00023D2C File Offset: 0x00021F2C
		internal int GetHashCodeWithNameHash(in TextureDesc texDesc)
		{
			TextureDesc textureDesc = texDesc;
			return textureDesc.GetHashCode() * 23 + texDesc.name.GetHashCode();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00023D5C File Offset: 0x00021F5C
		internal static TextureDesc CreateTextureDesc(RenderTextureDescriptor desc, TextureSizeMode textureSizeMode = TextureSizeMode.Explicit, int anisoLevel = 1, float mipMapBias = 0f, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Clamp, string name = "")
		{
			GraphicsFormat format = ((desc.depthStencilFormat != GraphicsFormat.None) ? desc.depthStencilFormat : desc.graphicsFormat);
			return new TextureDesc(desc.width, desc.height, false, false)
			{
				sizeMode = textureSizeMode,
				slices = desc.volumeDepth,
				format = format,
				filterMode = filterMode,
				wrapMode = wrapMode,
				dimension = desc.dimension,
				enableRandomWrite = desc.enableRandomWrite,
				useMipMap = desc.useMipMap,
				autoGenerateMips = desc.autoGenerateMips,
				isShadowMap = (desc.shadowSamplingMode != ShadowSamplingMode.None),
				anisoLevel = anisoLevel,
				mipMapBias = mipMapBias,
				msaaSamples = (MSAASamples)desc.msaaSamples,
				bindTextureMS = desc.bindMS,
				useDynamicScale = desc.useDynamicScale,
				memoryless = RenderTextureMemoryless.None,
				vrUsage = VRTextureUsage.None,
				name = name
			};
		}

		// Token: 0x040007D5 RID: 2005
		[TupleElementNames(new string[] { "resource", "frameIndex" })]
		protected Dictionary<int, SortedList<int, ValueTuple<RTHandle, int>>> m_ResourcePool = new Dictionary<int, SortedList<int, ValueTuple<RTHandle, int>>>();

		// Token: 0x040007D6 RID: 2006
		protected List<int> m_RemoveList = new List<int>(32);

		// Token: 0x040007D7 RID: 2007
		protected static int s_CurrentStaleResourceCount = 0;

		// Token: 0x040007D8 RID: 2008
		protected static int s_StaleResourceLifetime = 3;

		// Token: 0x040007D9 RID: 2009
		protected static int s_StaleResourceMaxCapacity = 32;
	}
}
