using System;
using UnityEngine;

namespace Cinemachine.PostFX
{
	// Token: 0x020000E1 RID: 225
	[SaveDuringPlay]
	[AddComponentMenu("")]
	public class CinemachinePostProcessing : CinemachineExtension
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x0000429A File Offset: 0x0000249A
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
		}
	}
}
