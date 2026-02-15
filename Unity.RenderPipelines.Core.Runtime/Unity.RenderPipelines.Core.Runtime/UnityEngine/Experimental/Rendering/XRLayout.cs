using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200000D RID: 13
	public class XRLayout
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public void AddCamera(Camera camera, bool enableXR)
		{
			if (camera == null)
			{
				return;
			}
			bool xrSupported = (camera.cameraType == CameraType.Game || camera.cameraType == CameraType.VR) && camera.targetTexture == null && enableXR;
			if (XRSystem.displayActive && xrSupported)
			{
				XRSystem.SetDisplayZRange(camera.nearClipPlane, camera.farClipPlane);
				XRSystem.CreateDefaultLayout(camera, this);
				return;
			}
			this.AddPass(camera, XRSystem.emptyPass);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D52 File Offset: 0x00000F52
		public void ReconfigurePass(XRPass xrPass, Camera camera)
		{
			if (xrPass.enabled)
			{
				XRSystem.ReconfigurePass(xrPass, camera);
				xrPass.UpdateCombinedOcclusionMesh();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D69 File Offset: 0x00000F69
		public List<ValueTuple<Camera, XRPass>> GetActivePasses()
		{
			return this.m_ActivePasses;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D71 File Offset: 0x00000F71
		internal void AddPass(Camera camera, XRPass xrPass)
		{
			xrPass.UpdateCombinedOcclusionMesh();
			this.m_ActivePasses.Add(new ValueTuple<Camera, XRPass>(camera, xrPass));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D8C File Offset: 0x00000F8C
		internal void Clear()
		{
			for (int i = 0; i < this.m_ActivePasses.Count; i++)
			{
				XRPass xrPass = this.m_ActivePasses[this.m_ActivePasses.Count - i - 1].Item2;
				if (xrPass != XRSystem.emptyPass)
				{
					xrPass.Release();
				}
			}
			this.m_ActivePasses.Clear();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DE8 File Offset: 0x00000FE8
		internal void LogDebugInfo()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("XRSystem setup for frame {0}, active: {1}", Time.frameCount, XRSystem.displayActive);
			sb.AppendLine();
			for (int passIndex = 0; passIndex < this.m_ActivePasses.Count; passIndex++)
			{
				XRPass pass = this.m_ActivePasses[passIndex].Item2;
				for (int viewIndex = 0; viewIndex < pass.viewCount; viewIndex++)
				{
					Rect viewport = pass.GetViewport(viewIndex);
					sb.AppendFormat("XR Pass {0} Cull {1} View {2} Slice {3} : {4} x {5}", new object[]
					{
						pass.multipassId,
						pass.cullingPassId,
						viewIndex,
						pass.GetTextureArraySlice(viewIndex),
						viewport.width,
						viewport.height
					});
					sb.AppendLine();
				}
			}
			Debug.Log(sb);
		}

		// Token: 0x04000035 RID: 53
		private readonly List<ValueTuple<Camera, XRPass>> m_ActivePasses = new List<ValueTuple<Camera, XRPass>>();
	}
}
