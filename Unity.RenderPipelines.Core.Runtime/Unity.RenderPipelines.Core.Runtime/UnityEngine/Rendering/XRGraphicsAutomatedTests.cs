using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200021F RID: 543
	public static class XRGraphicsAutomatedTests
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000090C6 File Offset: 0x000072C6
		private static bool activatedFromCommandLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00034FDD File Offset: 0x000331DD
		public static bool enabled { get; } = XRGraphicsAutomatedTests.activatedFromCommandLine;

		// Token: 0x06000E9A RID: 3738 RVA: 0x00034FE4 File Offset: 0x000331E4
		internal static void OverrideLayout(XRLayout layout, Camera camera)
		{
			if (XRGraphicsAutomatedTests.enabled && XRGraphicsAutomatedTests.running)
			{
				Matrix4x4 camProjMatrix = camera.projectionMatrix;
				Matrix4x4 camViewMatrix = camera.worldToCameraMatrix;
				ScriptableCullingParameters cullingParams;
				if (camera.TryGetCullingParameters(false, out cullingParams))
				{
					cullingParams.stereoProjectionMatrix = camProjMatrix;
					cullingParams.stereoViewMatrix = camViewMatrix;
					cullingParams.stereoSeparationDistance = 0f;
					List<ValueTuple<Camera, XRPass>> xrPasses = layout.GetActivePasses();
					for (int passId = 0; passId < xrPasses.Count; passId++)
					{
						XRPass xrPass = xrPasses[passId].Item2;
						xrPass.AssignCullingParams(xrPass.cullingPassId, cullingParams);
						for (int viewId = 0; viewId < xrPass.viewCount; viewId++)
						{
							Matrix4x4 projMatrix = camProjMatrix;
							Matrix4x4 viewMatrix = camViewMatrix;
							bool flag = xrPasses.Count == 2 && passId == 0;
							bool isFirstViewSinglePass = xrPasses.Count == 1 && viewId == 0;
							if (flag || isFirstViewSinglePass)
							{
								FrustumPlanes planes = projMatrix.decomposeProjection;
								planes.left *= 0.44f;
								planes.right *= 0.88f;
								planes.top *= 0.11f;
								planes.bottom *= 0.33f;
								projMatrix = Matrix4x4.Frustum(planes);
								viewMatrix *= Matrix4x4.Translate(new Vector3(0.34f, 0.25f, -0.08f));
							}
							XRView xrView = new XRView(projMatrix, viewMatrix, Matrix4x4.identity, false, xrPass.GetViewport(viewId), null, xrPass.GetTextureArraySlice(viewId));
							xrPass.AssignView(viewId, xrView);
						}
					}
				}
			}
		}

		// Token: 0x04000978 RID: 2424
		public static bool running = false;
	}
}
