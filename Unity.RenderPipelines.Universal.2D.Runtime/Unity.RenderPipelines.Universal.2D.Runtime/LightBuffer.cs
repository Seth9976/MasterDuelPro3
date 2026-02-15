using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000043 RID: 67
	internal class LightBuffer
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000E93B File Offset: 0x0000CB3B
		internal GraphicsBuffer graphicsBuffer
		{
			get
			{
				if (this.m_GraphicsBuffer == null)
				{
					this.m_GraphicsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, LightBuffer.kMax, UnsafeUtility.SizeOf<PerLight2D>());
				}
				return this.m_GraphicsBuffer;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000E962 File Offset: 0x0000CB62
		internal NativeArray<int> lightMarkers
		{
			get
			{
				return this.m_Markers;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000E96A File Offset: 0x0000CB6A
		internal NativeArray<PerLight2D> nativeBuffer
		{
			get
			{
				return this.m_NativeBuffer;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000E972 File Offset: 0x0000CB72
		internal void Release()
		{
			this.m_GraphicsBuffer.Release();
			this.m_GraphicsBuffer = null;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000E986 File Offset: 0x0000CB86
		internal void Reset()
		{
			UnsafeUtility.MemClear(this.m_Markers.GetUnsafePtr<int>(), (long)(UnsafeUtility.SizeOf<int>() * LightBuffer.kBatchMax));
			UnsafeUtility.MemClear(this.m_NativeBuffer.GetUnsafePtr<PerLight2D>(), (long)(UnsafeUtility.SizeOf<PerLight2D>() * LightBuffer.kMax));
		}

		// Token: 0x04000154 RID: 340
		internal static readonly int kMax = 16384;

		// Token: 0x04000155 RID: 341
		internal static readonly int kCount = 1;

		// Token: 0x04000156 RID: 342
		internal static readonly int kLightMod = 64;

		// Token: 0x04000157 RID: 343
		internal static readonly int kBatchMax = 256;

		// Token: 0x04000158 RID: 344
		private GraphicsBuffer m_GraphicsBuffer;

		// Token: 0x04000159 RID: 345
		private NativeArray<int> m_Markers = new NativeArray<int>(LightBuffer.kBatchMax, Allocator.Persistent, NativeArrayOptions.ClearMemory);

		// Token: 0x0400015A RID: 346
		private NativeArray<PerLight2D> m_NativeBuffer = new NativeArray<PerLight2D>(LightBuffer.kMax, Allocator.Persistent, NativeArrayOptions.ClearMemory);
	}
}
