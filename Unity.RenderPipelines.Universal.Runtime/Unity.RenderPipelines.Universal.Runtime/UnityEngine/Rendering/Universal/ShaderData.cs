using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200018D RID: 397
	internal class ShaderData : IDisposable
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x00002644 File Offset: 0x00000844
		private ShaderData()
		{
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0002823C File Offset: 0x0002643C
		internal static ShaderData instance
		{
			get
			{
				if (ShaderData.m_Instance == null)
				{
					ShaderData.m_Instance = new ShaderData();
				}
				return ShaderData.m_Instance;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00028254 File Offset: 0x00026454
		public void Dispose()
		{
			this.DisposeBuffer(ref this.m_LightDataBuffer);
			this.DisposeBuffer(ref this.m_LightIndicesBuffer);
			this.DisposeBuffer(ref this.m_AdditionalLightShadowParamsStructuredBuffer);
			this.DisposeBuffer(ref this.m_AdditionalLightShadowSliceMatricesStructuredBuffer);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00028286 File Offset: 0x00026486
		internal ComputeBuffer GetLightDataBuffer(int size)
		{
			return this.GetOrUpdateBuffer<ShaderInput.LightData>(ref this.m_LightDataBuffer, size);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00028295 File Offset: 0x00026495
		internal ComputeBuffer GetLightIndicesBuffer(int size)
		{
			return this.GetOrUpdateBuffer<int>(ref this.m_LightIndicesBuffer, size);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000282A4 File Offset: 0x000264A4
		internal ComputeBuffer GetAdditionalLightShadowParamsStructuredBuffer(int size)
		{
			return this.GetOrUpdateBuffer<Vector4>(ref this.m_AdditionalLightShadowParamsStructuredBuffer, size);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000282B3 File Offset: 0x000264B3
		internal ComputeBuffer GetAdditionalLightShadowSliceMatricesStructuredBuffer(int size)
		{
			return this.GetOrUpdateBuffer<Matrix4x4>(ref this.m_AdditionalLightShadowSliceMatricesStructuredBuffer, size);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000282C2 File Offset: 0x000264C2
		private ComputeBuffer GetOrUpdateBuffer<T>(ref ComputeBuffer buffer, int size) where T : struct
		{
			if (buffer == null)
			{
				buffer = new ComputeBuffer(size, Marshal.SizeOf<T>());
			}
			else if (size > buffer.count)
			{
				buffer.Dispose();
				buffer = new ComputeBuffer(size, Marshal.SizeOf<T>());
			}
			return buffer;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000282F7 File Offset: 0x000264F7
		private void DisposeBuffer(ref ComputeBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Dispose();
				buffer = null;
			}
		}

		// Token: 0x040008D4 RID: 2260
		private static ShaderData m_Instance;

		// Token: 0x040008D5 RID: 2261
		private ComputeBuffer m_LightDataBuffer;

		// Token: 0x040008D6 RID: 2262
		private ComputeBuffer m_LightIndicesBuffer;

		// Token: 0x040008D7 RID: 2263
		private ComputeBuffer m_AdditionalLightShadowParamsStructuredBuffer;

		// Token: 0x040008D8 RID: 2264
		private ComputeBuffer m_AdditionalLightShadowSliceMatricesStructuredBuffer;
	}
}
