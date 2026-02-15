using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.XR;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001EC RID: 492
	internal static class XRSystemUniversal
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00039268 File Offset: 0x00037468
		internal static void BeginLateLatching(Camera camera, XRPassUniversal xrPass)
		{
			XRDisplaySubsystem xrDisplay = XRSystem.GetActiveDisplay();
			if (xrDisplay != null && xrPass.viewCount == 2)
			{
				xrDisplay.BeginRecordingIfLateLatched(camera);
				xrPass.isLateLatchEnabled = true;
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00039298 File Offset: 0x00037498
		internal static void EndLateLatching(Camera camera, XRPassUniversal xrPass)
		{
			XRDisplaySubsystem xrDisplay = XRSystem.GetActiveDisplay();
			if (xrDisplay != null && xrPass.isLateLatchEnabled)
			{
				xrDisplay.EndRecordingIfLateLatched(camera);
				xrPass.isLateLatchEnabled = false;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000392C4 File Offset: 0x000374C4
		internal static void UnmarkShaderProperties(RasterCommandBuffer cmd, XRPassUniversal xrPass)
		{
			if (xrPass.isLateLatchEnabled && xrPass.hasMarkedLateLatch)
			{
				cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.View);
				cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.InverseView);
				cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.ViewProjection);
				cmd.UnmarkLateLatchMatrix(CameraLateLatchMatrixType.InverseViewProjection);
				xrPass.hasMarkedLateLatch = false;
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000392FC File Offset: 0x000374FC
		internal static void MarkShaderProperties(RasterCommandBuffer cmd, XRPassUniversal xrPass, bool renderIntoTexture)
		{
			if (xrPass.isLateLatchEnabled && xrPass.canMarkLateLatch)
			{
				cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.View, XRBuiltinShaderConstants.unity_StereoMatrixV);
				cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.InverseView, XRBuiltinShaderConstants.unity_StereoMatrixInvV);
				cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.ViewProjection, XRBuiltinShaderConstants.unity_StereoMatrixVP);
				cmd.MarkLateLatchMatrixShaderPropertyID(CameraLateLatchMatrixType.InverseViewProjection, XRBuiltinShaderConstants.unity_StereoMatrixInvVP);
				for (int viewIndex = 0; viewIndex < 2; viewIndex++)
				{
					XRSystemUniversal.s_projMatrix[viewIndex] = GL.GetGPUProjectionMatrix(xrPass.GetProjMatrix(viewIndex), renderIntoTexture);
				}
				cmd.SetLateLatchProjectionMatrices(XRSystemUniversal.s_projMatrix);
				xrPass.hasMarkedLateLatch = true;
			}
		}

		// Token: 0x04000C0D RID: 3085
		private static Matrix4x4[] s_projMatrix = new Matrix4x4[2];
	}
}
