using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000026 RID: 38
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineFollowZoom.html")]
	public class CinemachineFollowZoom : CinemachineExtension
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00006EB4 File Offset: 0x000050B4
		private void OnValidate()
		{
			this.m_Width = Mathf.Max(0f, this.m_Width);
			this.m_MaxFOV = Mathf.Clamp(this.m_MaxFOV, 1f, 179f);
			this.m_MinFOV = Mathf.Clamp(this.m_MinFOV, 1f, this.m_MaxFOV);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006F0E File Offset: 0x0000510E
		public override float GetMaxDampTime()
		{
			return this.m_Damping;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006F18 File Offset: 0x00005118
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			CinemachineFollowZoom.VcamExtraState extra = base.GetExtraState<CinemachineFollowZoom.VcamExtraState>(vcam);
			if (deltaTime < 0f || !base.VirtualCamera.PreviousStateIsValid)
			{
				extra.m_previousFrameZoom = state.Lens.FieldOfView;
			}
			if (stage == CinemachineCore.Stage.Body)
			{
				float targetWidth = Mathf.Max(this.m_Width, 0f);
				float fov = 179f;
				float d = Vector3.Distance(state.CorrectedPosition, state.ReferenceLookAt);
				if (d > 0.0001f)
				{
					float minW = d * 2f * Mathf.Tan(this.m_MinFOV * 0.017453292f / 2f);
					float maxW = d * 2f * Mathf.Tan(this.m_MaxFOV * 0.017453292f / 2f);
					targetWidth = Mathf.Clamp(targetWidth, minW, maxW);
					if (deltaTime >= 0f && this.m_Damping > 0f && base.VirtualCamera.PreviousStateIsValid)
					{
						float currentWidth = d * 2f * Mathf.Tan(extra.m_previousFrameZoom * 0.017453292f / 2f);
						float delta = targetWidth - currentWidth;
						delta = base.VirtualCamera.DetachedLookAtTargetDamp(delta, this.m_Damping, deltaTime);
						targetWidth = currentWidth + delta;
					}
					fov = 2f * Mathf.Atan(targetWidth / (2f * d)) * 57.29578f;
				}
				LensSettings lens = state.Lens;
				lens.FieldOfView = (extra.m_previousFrameZoom = Mathf.Clamp(fov, this.m_MinFOV, this.m_MaxFOV));
				state.Lens = lens;
			}
		}

		// Token: 0x040000BC RID: 188
		[Tooltip("The shot width to maintain, in world units, at target distance.")]
		public float m_Width = 2f;

		// Token: 0x040000BD RID: 189
		[Range(0f, 20f)]
		[Tooltip("Increase this value to soften the aggressiveness of the follow-zoom.  Small numbers are more responsive, larger numbers give a more heavy slowly responding camera.")]
		public float m_Damping = 1f;

		// Token: 0x040000BE RID: 190
		[Range(1f, 179f)]
		[Tooltip("Lower limit for the FOV that this behaviour will generate.")]
		public float m_MinFOV = 3f;

		// Token: 0x040000BF RID: 191
		[Range(1f, 179f)]
		[Tooltip("Upper limit for the FOV that this behaviour will generate.")]
		public float m_MaxFOV = 60f;

		// Token: 0x02000027 RID: 39
		private class VcamExtraState
		{
			// Token: 0x040000C0 RID: 192
			public float m_previousFrameZoom;
		}
	}
}
