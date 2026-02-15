using System;
using Cinemachine;
using Cinemachine.Utility;
using UnityEngine;

// Token: 0x02000002 RID: 2
[AddComponentMenu("")]
[ExecuteAlways]
[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/api/Cinemachine.CinemachineCameraOffset.html")]
[SaveDuringPlay]
public class CinemachineCameraOffset : CinemachineExtension
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
	{
		if (stage == this.m_ApplyAfter)
		{
			object obj = this.m_PreserveComposition && state.HasLookAt && stage > CinemachineCore.Stage.Body;
			Vector3 screenOffset = Vector2.zero;
			object obj2 = obj;
			if (obj2 != null)
			{
				screenOffset = state.RawOrientation.GetCameraRotationToTarget(state.ReferenceLookAt - state.CorrectedPosition, state.ReferenceUp);
			}
			Vector3 offset = state.RawOrientation * this.m_Offset;
			state.PositionCorrection += offset;
			if (obj2 == null)
			{
				state.ReferenceLookAt += offset;
				return;
			}
			Quaternion q = Quaternion.LookRotation(state.ReferenceLookAt - state.CorrectedPosition, state.ReferenceUp);
			q = q.ApplyCameraRotation(-screenOffset, state.ReferenceUp);
			state.RawOrientation = q;
		}
	}

	// Token: 0x04000001 RID: 1
	[Tooltip("Offset the camera's position by this much (camera space)")]
	public Vector3 m_Offset = Vector3.zero;

	// Token: 0x04000002 RID: 2
	[Tooltip("When to apply the offset")]
	public CinemachineCore.Stage m_ApplyAfter = CinemachineCore.Stage.Aim;

	// Token: 0x04000003 RID: 3
	[Tooltip("If applying offset after aim, re-adjust the aim to preserve the screen position of the LookAt target as much as possible")]
	public bool m_PreserveComposition;
}
