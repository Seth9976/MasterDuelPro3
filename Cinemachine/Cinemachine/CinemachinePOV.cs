using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000058 RID: 88
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachinePOV : CinemachineComponentBase
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000F645 File Offset: 0x0000D845
		public override bool IsValid
		{
			get
			{
				return base.enabled;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000771A File Offset: 0x0000591A
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Aim;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000F64D File Offset: 0x0000D84D
		private void OnValidate()
		{
			this.m_VerticalAxis.Validate();
			this.m_VerticalRecentering.Validate();
			this.m_HorizontalAxis.Validate();
			this.m_HorizontalRecentering.Validate();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000F67B File Offset: 0x0000D87B
		private void OnEnable()
		{
			this.UpdateInputAxisProvider();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000F684 File Offset: 0x0000D884
		public void UpdateInputAxisProvider()
		{
			this.m_HorizontalAxis.SetInputAxisProvider(0, null);
			this.m_VerticalAxis.SetInputAxisProvider(1, null);
			if (base.VirtualCamera != null)
			{
				AxisState.IInputAxisProvider provider = base.VirtualCamera.GetInputAxisProvider();
				if (provider != null)
				{
					this.m_HorizontalAxis.SetInputAxisProvider(0, provider);
					this.m_VerticalAxis.SetInputAxisProvider(1, provider);
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000429A File Offset: 0x0000249A
		public override void PrePipelineMutateCameraState(ref CameraState state, float deltaTime)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (!this.IsValid)
			{
				return;
			}
			if (deltaTime >= 0f && (!base.VirtualCamera.PreviousStateIsValid || !CinemachineCore.Instance.IsLive(base.VirtualCamera)))
			{
				deltaTime = -1f;
			}
			if (deltaTime >= 0f)
			{
				if (this.m_HorizontalAxis.Update(deltaTime))
				{
					this.m_HorizontalRecentering.CancelRecentering();
				}
				if (this.m_VerticalAxis.Update(deltaTime))
				{
					this.m_VerticalRecentering.CancelRecentering();
				}
			}
			Vector2 recenterTarget = this.GetRecenterTarget();
			this.m_HorizontalRecentering.DoRecentering(ref this.m_HorizontalAxis, deltaTime, recenterTarget.x);
			this.m_VerticalRecentering.DoRecentering(ref this.m_VerticalAxis, deltaTime, recenterTarget.y);
			Quaternion rot = Quaternion.Euler(this.m_VerticalAxis.Value, this.m_HorizontalAxis.Value, 0f);
			Transform parent = base.VirtualCamera.transform.parent;
			if (parent != null)
			{
				rot = parent.rotation * rot;
			}
			else
			{
				rot = Quaternion.FromToRotation(Vector3.up, curState.ReferenceUp) * rot;
			}
			curState.RawOrientation = rot;
			if (base.VirtualCamera.PreviousStateIsValid)
			{
				curState.PositionDampingBypass = UnityVectorExtensions.SafeFromToRotation(this.m_PreviousCameraRotation * Vector3.forward, rot * Vector3.forward, curState.ReferenceUp).eulerAngles;
			}
			this.m_PreviousCameraRotation = rot;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000F84C File Offset: 0x0000DA4C
		public Vector2 GetRecenterTarget()
		{
			Transform t = null;
			CinemachinePOV.RecenterTargetMode recenterTarget = this.m_RecenterTarget;
			if (recenterTarget != CinemachinePOV.RecenterTargetMode.FollowTargetForward)
			{
				if (recenterTarget == CinemachinePOV.RecenterTargetMode.LookAtTargetForward)
				{
					t = base.VirtualCamera.LookAt;
				}
			}
			else
			{
				t = base.VirtualCamera.Follow;
			}
			if (t != null)
			{
				Vector3 fwd = t.forward;
				Transform parent = base.VirtualCamera.transform.parent;
				if (parent != null)
				{
					fwd = parent.rotation * fwd;
				}
				Vector3 v = Quaternion.FromToRotation(Vector3.forward, fwd).eulerAngles;
				return new Vector2(CinemachinePOV.NormalizeAngle(v.y), CinemachinePOV.NormalizeAngle(v.x));
			}
			return Vector2.zero;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000F8F6 File Offset: 0x0000DAF6
		private static float NormalizeAngle(float angle)
		{
			return (angle + 180f) % 360f - 180f;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000F90B File Offset: 0x0000DB0B
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			this.SetAxesForRotation(rot);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000F914 File Offset: 0x0000DB14
		public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime, ref CinemachineVirtualCameraBase.TransitionParams transitionParams)
		{
			this.m_HorizontalRecentering.DoRecentering(ref this.m_HorizontalAxis, -1f, 0f);
			this.m_VerticalRecentering.DoRecentering(ref this.m_VerticalAxis, -1f, 0f);
			this.m_HorizontalRecentering.CancelRecentering();
			this.m_VerticalRecentering.CancelRecentering();
			if (fromCam != null && transitionParams.m_InheritPosition && !CinemachineCore.Instance.IsLiveInBlend(base.VirtualCamera))
			{
				this.SetAxesForRotation(fromCam.State.RawOrientation);
				return true;
			}
			return false;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000771A File Offset: 0x0000591A
		public override bool RequiresUserInput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		private void SetAxesForRotation(Quaternion targetRot)
		{
			Vector3 up = base.VcamState.ReferenceUp;
			Vector3 fwd = Vector3.forward;
			Transform parent = base.VirtualCamera.transform.parent;
			if (parent != null)
			{
				fwd = parent.rotation * fwd;
			}
			this.m_HorizontalAxis.Value = 0f;
			this.m_HorizontalAxis.Reset();
			Vector3 targetFwd = targetRot * Vector3.forward;
			Vector3 a = fwd.ProjectOntoPlane(up);
			Vector3 b = targetFwd.ProjectOntoPlane(up);
			if (!a.AlmostZero() && !b.AlmostZero())
			{
				this.m_HorizontalAxis.Value = Vector3.SignedAngle(a, b, up);
			}
			this.m_VerticalAxis.Value = 0f;
			this.m_VerticalAxis.Reset();
			fwd = Quaternion.AngleAxis(this.m_HorizontalAxis.Value, up) * fwd;
			Vector3 right = Vector3.Cross(up, fwd);
			if (!right.AlmostZero())
			{
				this.m_VerticalAxis.Value = Vector3.SignedAngle(fwd, targetFwd, right);
			}
		}

		// Token: 0x040001F9 RID: 505
		public CinemachinePOV.RecenterTargetMode m_RecenterTarget;

		// Token: 0x040001FA RID: 506
		[Tooltip("The Vertical axis.  Value is -90..90. Controls the vertical orientation")]
		[AxisStateProperty]
		public AxisState m_VerticalAxis = new AxisState(-70f, 70f, false, false, 300f, 0.1f, 0.1f, "Mouse Y", true);

		// Token: 0x040001FB RID: 507
		[Tooltip("Controls how automatic recentering of the Vertical axis is accomplished")]
		public AxisState.Recentering m_VerticalRecentering = new AxisState.Recentering(false, 1f, 2f);

		// Token: 0x040001FC RID: 508
		[Tooltip("The Horizontal axis.  Value is -180..180.  Controls the horizontal orientation")]
		[AxisStateProperty]
		public AxisState m_HorizontalAxis = new AxisState(-180f, 180f, true, false, 300f, 0.1f, 0.1f, "Mouse X", false);

		// Token: 0x040001FD RID: 509
		[Tooltip("Controls how automatic recentering of the Horizontal axis is accomplished")]
		public AxisState.Recentering m_HorizontalRecentering = new AxisState.Recentering(false, 1f, 2f);

		// Token: 0x040001FE RID: 510
		[HideInInspector]
		[Tooltip("Obsolete - no longer used")]
		public bool m_ApplyBeforeBody;

		// Token: 0x040001FF RID: 511
		private Quaternion m_PreviousCameraRotation;

		// Token: 0x02000059 RID: 89
		public enum RecenterTargetMode
		{
			// Token: 0x04000201 RID: 513
			None,
			// Token: 0x04000202 RID: 514
			FollowTargetForward,
			// Token: 0x04000203 RID: 515
			LookAtTargetForward
		}
	}
}
