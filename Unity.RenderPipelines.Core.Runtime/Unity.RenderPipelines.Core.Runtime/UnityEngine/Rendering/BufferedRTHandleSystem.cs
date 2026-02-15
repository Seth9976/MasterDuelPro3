using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200018E RID: 398
	public class BufferedRTHandleSystem : IDisposable
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002771A File Offset: 0x0002591A
		public int maxWidth
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxWidth();
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00027727 File Offset: 0x00025927
		public int maxHeight
		{
			get
			{
				return this.m_RTHandleSystem.GetMaxHeight();
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00027734 File Offset: 0x00025934
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleSystem.rtHandleProperties;
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00027741 File Offset: 0x00025941
		public RTHandle GetFrameRT(int bufferId, int frameIndex)
		{
			if (!this.m_RTHandles.ContainsKey(bufferId))
			{
				return null;
			}
			return this.m_RTHandles[bufferId][frameIndex];
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00027764 File Offset: 0x00025964
		public void ClearBuffers(CommandBuffer cmd)
		{
			foreach (KeyValuePair<int, RTHandle[]> rtHandle in this.m_RTHandles)
			{
				for (int i = 0; i < rtHandle.Value.Length; i++)
				{
					CoreUtils.SetRenderTarget(cmd, rtHandle.Value[i], ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				}
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000277DC File Offset: 0x000259DC
		public void AllocBuffer(int bufferId, Func<RTHandleSystem, int, RTHandle> allocator, int bufferCount)
		{
			RTHandle[] buffer = new RTHandle[bufferCount];
			this.m_RTHandles.Add(bufferId, buffer);
			buffer[0] = allocator(this.m_RTHandleSystem, 0);
			int i = 1;
			int c = buffer.Length;
			while (i < c)
			{
				buffer[i] = allocator(this.m_RTHandleSystem, i);
				this.m_RTHandleSystem.SwitchResizeMode(buffer[i], RTHandleSystem.ResizeMode.OnDemand);
				i++;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002783C File Offset: 0x00025A3C
		public void AllocBuffer(int bufferId, int bufferCount, ref RenderTextureDescriptor descriptor, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			BufferedRTHandleSystem.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			RTHandle[] buffer = new RTHandle[bufferCount];
			this.m_RTHandles.Add(bufferId, buffer);
			CS$<>8__locals1.format = RTHandles.GetFormat(descriptor.graphicsFormat, descriptor.depthStencilFormat);
			buffer[0] = this.<AllocBuffer>g__Alloc|12_0(ref descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name, ref CS$<>8__locals1);
			int i = 1;
			int c = buffer.Length;
			while (i < c)
			{
				buffer[i] = this.<AllocBuffer>g__Alloc|12_0(ref descriptor, filterMode, wrapMode, isShadowMap, anisoLevel, mipMapBias, name, ref CS$<>8__locals1);
				this.m_RTHandleSystem.SwitchResizeMode(buffer[i], RTHandleSystem.ResizeMode.OnDemand);
				i++;
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000278CC File Offset: 0x00025ACC
		public void ReleaseBuffer(int bufferId)
		{
			RTHandle[] buffers;
			if (this.m_RTHandles.TryGetValue(bufferId, out buffers))
			{
				foreach (RTHandle rt in buffers)
				{
					this.m_RTHandleSystem.Release(rt);
				}
			}
			this.m_RTHandles.Remove(bufferId);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00027916 File Offset: 0x00025B16
		public void SwapAndSetReferenceSize(int width, int height)
		{
			this.Swap();
			this.m_RTHandleSystem.SetReferenceSize(width, height);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002792B File Offset: 0x00025B2B
		public void ResetReferenceSize(int width, int height)
		{
			this.m_RTHandleSystem.ResetReferenceSize(width, height);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002793A File Offset: 0x00025B3A
		public int GetNumFramesAllocated(int bufferId)
		{
			if (!this.m_RTHandles.ContainsKey(bufferId))
			{
				return 0;
			}
			return this.m_RTHandles[bufferId].Length;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002795C File Offset: 0x00025B5C
		public Vector2 CalculateRatioAgainstMaxSize(int width, int height)
		{
			RTHandleSystem rthandleSystem = this.m_RTHandleSystem;
			Vector2Int vector2Int = new Vector2Int(width, height);
			return rthandleSystem.CalculateRatioAgainstMaxSize(in vector2Int);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00027980 File Offset: 0x00025B80
		private void Swap()
		{
			foreach (KeyValuePair<int, RTHandle[]> item in this.m_RTHandles)
			{
				if (item.Value.Length > 1)
				{
					RTHandle nextFirst = item.Value[item.Value.Length - 1];
					int i = 0;
					int c = item.Value.Length - 1;
					while (i < c)
					{
						item.Value[i + 1] = item.Value[i];
						i++;
					}
					item.Value[0] = nextFirst;
					this.m_RTHandleSystem.SwitchResizeMode(item.Value[0], RTHandleSystem.ResizeMode.Auto);
					this.m_RTHandleSystem.SwitchResizeMode(item.Value[1], RTHandleSystem.ResizeMode.OnDemand);
				}
				else
				{
					this.m_RTHandleSystem.SwitchResizeMode(item.Value[0], RTHandleSystem.ResizeMode.Auto);
				}
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00027A6C File Offset: 0x00025C6C
		private void Dispose(bool disposing)
		{
			if (!this.m_DisposedValue)
			{
				if (disposing)
				{
					this.ReleaseAll();
					this.m_RTHandleSystem.Dispose();
					this.m_RTHandleSystem = null;
				}
				this.m_DisposedValue = true;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00027A98 File Offset: 0x00025C98
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00027AA4 File Offset: 0x00025CA4
		public void ReleaseAll()
		{
			foreach (KeyValuePair<int, RTHandle[]> item in this.m_RTHandles)
			{
				int i = 0;
				int c = item.Value.Length;
				while (i < c)
				{
					this.m_RTHandleSystem.Release(item.Value[i]);
					i++;
				}
			}
			this.m_RTHandles.Clear();
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00027B48 File Offset: 0x00025D48
		[CompilerGenerated]
		private RTHandle <AllocBuffer>g__Alloc|12_0(ref RenderTextureDescriptor d, FilterMode fMode, TextureWrapMode wMode, bool isShadow, int aniso, float mipBias, string n, ref BufferedRTHandleSystem.<>c__DisplayClass12_0 A_8)
		{
			return this.m_RTHandleSystem.Alloc(d.width, d.height, A_8.format, d.volumeDepth, fMode, wMode, d.dimension, d.enableRandomWrite, d.useMipMap, d.autoGenerateMips, isShadow, aniso, mipBias, (MSAASamples)d.msaaSamples, d.bindMS, d.useDynamicScale, d.useDynamicScaleExplicit, d.memoryless, d.vrUsage, n);
		}

		// Token: 0x0400079C RID: 1948
		private Dictionary<int, RTHandle[]> m_RTHandles = new Dictionary<int, RTHandle[]>();

		// Token: 0x0400079D RID: 1949
		private RTHandleSystem m_RTHandleSystem = new RTHandleSystem();

		// Token: 0x0400079E RID: 1950
		private bool m_DisposedValue;
	}
}
