using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x020001FE RID: 510
	public static class NormalReconstruction
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x0003EC85 File Offset: 0x0003CE85
		public static void SetupProperties(CommandBuffer cmd, in CameraData cameraData)
		{
			NormalReconstruction.SetupProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), in cameraData);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003EC94 File Offset: 0x0003CE94
		public static void SetupProperties(RasterCommandBuffer cmd, in CameraData cameraData)
		{
			CameraData cameraData2 = cameraData;
			UniversalCameraData universalCameraData = cameraData2.universalCameraData;
			NormalReconstruction.SetupProperties(cmd, in universalCameraData);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0003ECB8 File Offset: 0x0003CEB8
		public static void SetupProperties(CommandBuffer cmd, UniversalCameraData cameraData)
		{
			NormalReconstruction.SetupProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), in cameraData);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0003ECC8 File Offset: 0x0003CEC8
		public static void SetupProperties(RasterCommandBuffer cmd, in UniversalCameraData cameraData)
		{
			int eyeCount = ((cameraData.xr.enabled && cameraData.xr.singlePassEnabled) ? 2 : 1);
			for (int eyeIndex = 0; eyeIndex < eyeCount; eyeIndex++)
			{
				Matrix4x4 view = cameraData.GetViewMatrix(eyeIndex);
				Matrix4x4 proj = cameraData.GetProjectionMatrix(eyeIndex);
				NormalReconstruction.s_NormalReconstructionMatrix[eyeIndex] = proj * view;
				Matrix4x4 cview = view;
				cview.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
				Matrix4x4 cviewProjInv = (proj * cview).inverse;
				NormalReconstruction.s_NormalReconstructionMatrix[eyeIndex] = cviewProjInv;
			}
			cmd.SetGlobalMatrixArray(NormalReconstruction.s_NormalReconstructionMatrixID, NormalReconstruction.s_NormalReconstructionMatrix);
		}

		// Token: 0x04000CEB RID: 3307
		private static readonly int s_NormalReconstructionMatrixID = Shader.PropertyToID("_NormalReconstructionMatrix");

		// Token: 0x04000CEC RID: 3308
		private static Matrix4x4[] s_NormalReconstructionMatrix = new Matrix4x4[2];
	}
}
