using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002D RID: 45
	public class ConstantBuffer<CBType> : ConstantBufferBase where CBType : struct
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x000068D9 File Offset: 0x00004AD9
		public ConstantBuffer()
		{
			this.m_GPUConstantBuffer = new ComputeBuffer(1, UnsafeUtility.SizeOf<CBType>(), ComputeBufferType.Constant);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000690A File Offset: 0x00004B0A
		public void UpdateData(CommandBuffer cmd, in CBType data)
		{
			this.m_Data[0] = data;
			cmd.SetBufferData(this.m_GPUConstantBuffer, this.m_Data);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00006930 File Offset: 0x00004B30
		public void UpdateData(in CBType data)
		{
			this.m_Data[0] = data;
			this.m_GPUConstantBuffer.SetData(this.m_Data);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00006955 File Offset: 0x00004B55
		public void SetGlobal(CommandBuffer cmd, int shaderId)
		{
			this.m_GlobalBindings.Add(shaderId);
			cmd.SetGlobalConstantBuffer(this.m_GPUConstantBuffer, shaderId, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000697D File Offset: 0x00004B7D
		public void SetGlobal(int shaderId)
		{
			this.m_GlobalBindings.Add(shaderId);
			Shader.SetGlobalConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000069A4 File Offset: 0x00004BA4
		public void Set(CommandBuffer cmd, ComputeShader cs, int shaderId)
		{
			cmd.SetComputeConstantBufferParam(cs, shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000069C0 File Offset: 0x00004BC0
		public void Set(ComputeShader cs, int shaderId)
		{
			cs.SetConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000069DB File Offset: 0x00004BDB
		public void Set(Material mat, int shaderId)
		{
			mat.SetConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000069F6 File Offset: 0x00004BF6
		public void Set(MaterialPropertyBlock mpb, int shaderId)
		{
			mpb.SetConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00006A11 File Offset: 0x00004C11
		public void PushGlobal(CommandBuffer cmd, in CBType data, int shaderId)
		{
			this.UpdateData(cmd, in data);
			this.SetGlobal(cmd, shaderId);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00006A23 File Offset: 0x00004C23
		public void PushGlobal(in CBType data, int shaderId)
		{
			this.UpdateData(in data);
			this.SetGlobal(shaderId);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00006A34 File Offset: 0x00004C34
		public override void Release()
		{
			foreach (int num in this.m_GlobalBindings)
			{
				Shader.SetGlobalConstantBuffer(num, null, 0, 0);
			}
			this.m_GlobalBindings.Clear();
			CoreUtils.SafeRelease(this.m_GPUConstantBuffer);
		}

		// Token: 0x040000AA RID: 170
		private HashSet<int> m_GlobalBindings = new HashSet<int>();

		// Token: 0x040000AB RID: 171
		private CBType[] m_Data = new CBType[1];

		// Token: 0x040000AC RID: 172
		private ComputeBuffer m_GPUConstantBuffer;
	}
}
