using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Cinemachine
{
	// Token: 0x02000032 RID: 50
	[AddComponentMenu("")]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachinePixelPerfect.html")]
	public class CinemachinePixelPerfect : CinemachineExtension
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00008FB8 File Offset: 0x000071B8
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage != CinemachineCore.Stage.Body)
			{
				return;
			}
			CinemachineBrain brain = CinemachineCore.Instance.FindPotentialTargetBrain(vcam);
			if (brain == null || !brain.IsLive(vcam, false))
			{
				return;
			}
			PixelPerfectCamera pixelPerfectCamera;
			brain.TryGetComponent<PixelPerfectCamera>(out pixelPerfectCamera);
			if (pixelPerfectCamera == null || !pixelPerfectCamera.isActiveAndEnabled)
			{
				return;
			}
			LensSettings lens = state.Lens;
			lens.OrthographicSize = pixelPerfectCamera.CorrectCinemachineOrthoSize(lens.OrthographicSize);
			state.Lens = lens;
		}
	}
}
