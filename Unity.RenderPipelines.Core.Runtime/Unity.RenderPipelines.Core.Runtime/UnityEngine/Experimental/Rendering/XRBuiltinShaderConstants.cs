using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200000C RID: 12
	public static class XRBuiltinShaderConstants
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000028C8 File Offset: 0x00000AC8
		public static void UpdateBuiltinShaderConstants(Matrix4x4 viewMatrix, Matrix4x4 projMatrix, bool renderIntoTexture, int viewIndex)
		{
			Matrix4x4 gpuProjMatrix = GL.GetGPUProjectionMatrix(projMatrix, renderIntoTexture);
			Matrix4x4 gpuViewProjMatrix = gpuProjMatrix * viewMatrix;
			XRBuiltinShaderConstants.s_cameraProjMatrix[viewIndex] = projMatrix;
			XRBuiltinShaderConstants.s_projMatrix[viewIndex] = gpuProjMatrix;
			XRBuiltinShaderConstants.s_viewMatrix[viewIndex] = viewMatrix;
			Matrix4x4.Inverse3DAffine(viewMatrix, ref XRBuiltinShaderConstants.s_invViewMatrix[viewIndex]);
			XRBuiltinShaderConstants.s_viewProjMatrix[viewIndex] = gpuViewProjMatrix;
			XRBuiltinShaderConstants.s_invCameraProjMatrix[viewIndex] = Matrix4x4.Inverse(projMatrix);
			XRBuiltinShaderConstants.s_invProjMatrix[viewIndex] = Matrix4x4.Inverse(gpuProjMatrix);
			XRBuiltinShaderConstants.s_invViewProjMatrix[viewIndex] = Matrix4x4.Inverse(gpuViewProjMatrix);
			XRBuiltinShaderConstants.s_worldSpaceCameraPos[viewIndex] = XRBuiltinShaderConstants.s_invViewMatrix[viewIndex].GetColumn(3);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002978 File Offset: 0x00000B78
		public static void SetBuiltinShaderConstants(CommandBuffer cmd)
		{
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoCameraProjection, XRBuiltinShaderConstants.s_cameraProjMatrix);
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoCameraInvProjection, XRBuiltinShaderConstants.s_invCameraProjMatrix);
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixV, XRBuiltinShaderConstants.s_viewMatrix);
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixInvV, XRBuiltinShaderConstants.s_invViewMatrix);
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixP, XRBuiltinShaderConstants.s_projMatrix);
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixInvP, XRBuiltinShaderConstants.s_invProjMatrix);
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixVP, XRBuiltinShaderConstants.s_viewProjMatrix);
			cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixInvVP, XRBuiltinShaderConstants.s_invViewProjMatrix);
			cmd.SetGlobalVectorArray(XRBuiltinShaderConstants.unity_StereoWorldSpaceCameraPos, XRBuiltinShaderConstants.s_worldSpaceCameraPos);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A15 File Offset: 0x00000C15
		public static void SetBuiltinShaderConstants(RasterCommandBuffer cmd)
		{
			XRBuiltinShaderConstants.SetBuiltinShaderConstants(cmd.m_WrappedCommandBuffer);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A24 File Offset: 0x00000C24
		public static void Update(XRPass xrPass, CommandBuffer cmd, bool renderIntoTexture)
		{
			if (xrPass.enabled)
			{
				cmd.SetViewProjectionMatrices(xrPass.GetViewMatrix(0), xrPass.GetProjMatrix(0));
				if (xrPass.singlePassEnabled)
				{
					for (int viewIndex = 0; viewIndex < 2; viewIndex++)
					{
						XRBuiltinShaderConstants.s_cameraProjMatrix[viewIndex] = xrPass.GetProjMatrix(viewIndex);
						XRBuiltinShaderConstants.s_viewMatrix[viewIndex] = xrPass.GetViewMatrix(viewIndex);
						XRBuiltinShaderConstants.s_projMatrix[viewIndex] = GL.GetGPUProjectionMatrix(XRBuiltinShaderConstants.s_cameraProjMatrix[viewIndex], renderIntoTexture);
						XRBuiltinShaderConstants.s_viewProjMatrix[viewIndex] = XRBuiltinShaderConstants.s_projMatrix[viewIndex] * XRBuiltinShaderConstants.s_viewMatrix[viewIndex];
						XRBuiltinShaderConstants.s_invCameraProjMatrix[viewIndex] = Matrix4x4.Inverse(XRBuiltinShaderConstants.s_cameraProjMatrix[viewIndex]);
						Matrix4x4.Inverse3DAffine(XRBuiltinShaderConstants.s_viewMatrix[viewIndex], ref XRBuiltinShaderConstants.s_invViewMatrix[viewIndex]);
						XRBuiltinShaderConstants.s_invProjMatrix[viewIndex] = Matrix4x4.Inverse(XRBuiltinShaderConstants.s_projMatrix[viewIndex]);
						XRBuiltinShaderConstants.s_invViewProjMatrix[viewIndex] = Matrix4x4.Inverse(XRBuiltinShaderConstants.s_viewProjMatrix[viewIndex]);
						XRBuiltinShaderConstants.s_worldSpaceCameraPos[viewIndex] = XRBuiltinShaderConstants.s_invViewMatrix[viewIndex].GetColumn(3);
					}
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoCameraProjection, XRBuiltinShaderConstants.s_cameraProjMatrix);
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoCameraInvProjection, XRBuiltinShaderConstants.s_invCameraProjMatrix);
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixV, XRBuiltinShaderConstants.s_viewMatrix);
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixInvV, XRBuiltinShaderConstants.s_invViewMatrix);
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixP, XRBuiltinShaderConstants.s_projMatrix);
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixInvP, XRBuiltinShaderConstants.s_invProjMatrix);
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixVP, XRBuiltinShaderConstants.s_viewProjMatrix);
					cmd.SetGlobalMatrixArray(XRBuiltinShaderConstants.unity_StereoMatrixInvVP, XRBuiltinShaderConstants.s_invViewProjMatrix);
					cmd.SetGlobalVectorArray(XRBuiltinShaderConstants.unity_StereoWorldSpaceCameraPos, XRBuiltinShaderConstants.s_worldSpaceCameraPos);
				}
			}
		}

		// Token: 0x04000023 RID: 35
		public static readonly int unity_StereoCameraProjection = Shader.PropertyToID("unity_StereoCameraProjection");

		// Token: 0x04000024 RID: 36
		public static readonly int unity_StereoCameraInvProjection = Shader.PropertyToID("unity_StereoCameraInvProjection");

		// Token: 0x04000025 RID: 37
		public static readonly int unity_StereoMatrixV = Shader.PropertyToID("unity_StereoMatrixV");

		// Token: 0x04000026 RID: 38
		public static readonly int unity_StereoMatrixInvV = Shader.PropertyToID("unity_StereoMatrixInvV");

		// Token: 0x04000027 RID: 39
		public static readonly int unity_StereoMatrixP = Shader.PropertyToID("unity_StereoMatrixP");

		// Token: 0x04000028 RID: 40
		public static readonly int unity_StereoMatrixInvP = Shader.PropertyToID("unity_StereoMatrixInvP");

		// Token: 0x04000029 RID: 41
		public static readonly int unity_StereoMatrixVP = Shader.PropertyToID("unity_StereoMatrixVP");

		// Token: 0x0400002A RID: 42
		public static readonly int unity_StereoMatrixInvVP = Shader.PropertyToID("unity_StereoMatrixInvVP");

		// Token: 0x0400002B RID: 43
		public static readonly int unity_StereoWorldSpaceCameraPos = Shader.PropertyToID("unity_StereoWorldSpaceCameraPos");

		// Token: 0x0400002C RID: 44
		private static Matrix4x4[] s_cameraProjMatrix = new Matrix4x4[2];

		// Token: 0x0400002D RID: 45
		private static Matrix4x4[] s_invCameraProjMatrix = new Matrix4x4[2];

		// Token: 0x0400002E RID: 46
		private static Matrix4x4[] s_viewMatrix = new Matrix4x4[2];

		// Token: 0x0400002F RID: 47
		private static Matrix4x4[] s_invViewMatrix = new Matrix4x4[2];

		// Token: 0x04000030 RID: 48
		private static Matrix4x4[] s_projMatrix = new Matrix4x4[2];

		// Token: 0x04000031 RID: 49
		private static Matrix4x4[] s_invProjMatrix = new Matrix4x4[2];

		// Token: 0x04000032 RID: 50
		private static Matrix4x4[] s_viewProjMatrix = new Matrix4x4[2];

		// Token: 0x04000033 RID: 51
		private static Matrix4x4[] s_invViewProjMatrix = new Matrix4x4[2];

		// Token: 0x04000034 RID: 52
		private static Vector4[] s_worldSpaceCameraPos = new Vector4[2];
	}
}
