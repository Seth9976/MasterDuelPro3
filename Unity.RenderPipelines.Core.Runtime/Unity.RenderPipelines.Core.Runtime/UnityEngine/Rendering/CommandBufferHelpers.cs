using System;
using UnityEngine.VFX;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001F RID: 31
	public struct CommandBufferHelpers
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00005440 File Offset: 0x00003640
		public static RasterCommandBuffer GetRasterCommandBuffer(CommandBuffer baseBuffer)
		{
			CommandBufferHelpers.rasterCmd.m_WrappedCommandBuffer = baseBuffer;
			return CommandBufferHelpers.rasterCmd;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005452 File Offset: 0x00003652
		public static ComputeCommandBuffer GetComputeCommandBuffer(CommandBuffer baseBuffer)
		{
			CommandBufferHelpers.computeCmd.m_WrappedCommandBuffer = baseBuffer;
			return CommandBufferHelpers.computeCmd;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005464 File Offset: 0x00003664
		public static UnsafeCommandBuffer GetUnsafeCommandBuffer(CommandBuffer baseBuffer)
		{
			CommandBufferHelpers.unsafeCmd.m_WrappedCommandBuffer = baseBuffer;
			return CommandBufferHelpers.unsafeCmd;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005476 File Offset: 0x00003676
		public static CommandBuffer GetNativeCommandBuffer(UnsafeCommandBuffer baseBuffer)
		{
			return baseBuffer.m_WrappedCommandBuffer;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000547E File Offset: 0x0000367E
		public static void VFXManager_ProcessCameraCommand(Camera cam, UnsafeCommandBuffer cmd, VFXCameraXRSettings camXRSettings, CullingResults results)
		{
			VFXManager.ProcessCameraCommand(cam, cmd.m_WrappedCommandBuffer, camXRSettings, results);
		}

		// Token: 0x0400009B RID: 155
		internal static RasterCommandBuffer rasterCmd = new RasterCommandBuffer(null, null, false);

		// Token: 0x0400009C RID: 156
		internal static ComputeCommandBuffer computeCmd = new ComputeCommandBuffer(null, null, false);

		// Token: 0x0400009D RID: 157
		internal static UnsafeCommandBuffer unsafeCmd = new UnsafeCommandBuffer(null, null, false);
	}
}
