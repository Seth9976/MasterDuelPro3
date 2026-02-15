using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cinemachine
{
	// Token: 0x0200000C RID: 12
	[AddComponentMenu("")]
	[ExecuteAlways]
	[SaveDuringPlay]
	[DisallowMultipleComponent]
	public class Cinemachine3rdPersonAim : CinemachineExtension
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000026DF File Offset: 0x000008DF
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000026E7 File Offset: 0x000008E7
		public Vector3 AimTarget { get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x000026F0 File Offset: 0x000008F0
		private void OnValidate()
		{
			this.AimDistance = Mathf.Max(1f, this.AimDistance);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002708 File Offset: 0x00000908
		private void Reset()
		{
			this.AimCollisionFilter = 1;
			this.IgnoreTag = string.Empty;
			this.AimDistance = 200f;
			this.AimTargetReticle = null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002733 File Offset: 0x00000933
		public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
		{
			CinemachineCore.CameraUpdatedEvent.RemoveListener(new UnityAction<CinemachineBrain>(this.DrawReticle));
			CinemachineCore.CameraUpdatedEvent.AddListener(new UnityAction<CinemachineBrain>(this.DrawReticle));
			return false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002764 File Offset: 0x00000964
		private void DrawReticle(CinemachineBrain brain)
		{
			if (!brain.IsLive(base.VirtualCamera, false) || brain.OutputCamera == null)
			{
				CinemachineCore.CameraUpdatedEvent.RemoveListener(new UnityAction<CinemachineBrain>(this.DrawReticle));
				return;
			}
			if (this.AimTargetReticle != null)
			{
				this.AimTargetReticle.position = brain.OutputCamera.WorldToScreenPoint(this.AimTarget);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027D0 File Offset: 0x000009D0
		private Vector3 ComputeLookAtPoint(Vector3 camPos, Transform player)
		{
			float aimDistance = this.AimDistance;
			Quaternion rotation = player.rotation;
			Vector3 fwd = rotation * Vector3.forward;
			Vector3 playerPos = Quaternion.Inverse(rotation) * (player.position - camPos);
			if (playerPos.z > 0f)
			{
				camPos += fwd * playerPos.z;
				aimDistance -= playerPos.z;
			}
			aimDistance = Mathf.Max(1f, aimDistance);
			RaycastHit hitInfo;
			if (!RuntimeUtility.RaycastIgnoreTag(new Ray(camPos, fwd), out hitInfo, aimDistance, this.AimCollisionFilter, in this.IgnoreTag))
			{
				return camPos + fwd * aimDistance;
			}
			return hitInfo.point;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000287C File Offset: 0x00000A7C
		private Vector3 ComputeAimTarget(Vector3 cameraLookAt, Transform player)
		{
			Vector3 playerPos = player.position;
			Vector3 dir = cameraLookAt - playerPos;
			RaycastHit hitInfo;
			if (RuntimeUtility.RaycastIgnoreTag(new Ray(playerPos, dir), out hitInfo, dir.magnitude, this.AimCollisionFilter, in this.IgnoreTag))
			{
				return hitInfo.point;
			}
			return cameraLookAt;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028CC File Offset: 0x00000ACC
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage == CinemachineCore.Stage.Body)
			{
				Transform player = vcam.Follow;
				if (player != null)
				{
					state.ReferenceLookAt = this.ComputeLookAtPoint(state.CorrectedPosition, player);
					this.AimTarget = this.ComputeAimTarget(state.ReferenceLookAt, player);
				}
			}
			if (stage == CinemachineCore.Stage.Finalize)
			{
				Vector3 dir = state.ReferenceLookAt - state.FinalPosition;
				if (dir.sqrMagnitude > 0.01f)
				{
					state.RawOrientation = Quaternion.LookRotation(dir, state.ReferenceUp);
					state.OrientationCorrection = Quaternion.identity;
				}
			}
		}

		// Token: 0x0400001B RID: 27
		[Header("Aim Target Detection")]
		[Tooltip("Objects on these layers will be detected")]
		public LayerMask AimCollisionFilter;

		// Token: 0x0400001C RID: 28
		[TagField]
		[Tooltip("Objects with this tag will be ignored.  It is a good idea to set this field to the target's tag")]
		public string IgnoreTag = string.Empty;

		// Token: 0x0400001D RID: 29
		[Tooltip("How far to project the object detection ray")]
		public float AimDistance;

		// Token: 0x0400001E RID: 30
		[Tooltip("This 2D object will be positioned in the game view over the raycast hit point, if any, or will remain in the center of the screen if no hit point is detected.  May be null, in which case no on-screen indicator will appear")]
		public RectTransform AimTargetReticle;
	}
}
