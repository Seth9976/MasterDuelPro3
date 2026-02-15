using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200005E RID: 94
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineTransposer : CinemachineComponentBase
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00010175 File Offset: 0x0000E375
		protected virtual void OnValidate()
		{
			this.m_FollowOffset = this.EffectiveOffset;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00010183 File Offset: 0x0000E383
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0001018B File Offset: 0x0000E38B
		public bool HideOffsetInInspector { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00010194 File Offset: 0x0000E394
		public Vector3 EffectiveOffset
		{
			get
			{
				Vector3 offset = this.m_FollowOffset;
				if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					offset.x = 0f;
					offset.z = -Mathf.Abs(offset.z);
				}
				return offset;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000C336 File Offset: 0x0000A536
		public override bool IsValid
		{
			get
			{
				return base.enabled && base.FollowTarget != null;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000C34E File Offset: 0x0000A54E
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Body;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public override float GetMaxDampTime()
		{
			Vector3 d = this.Damping;
			Vector3 d2 = this.AngularDamping;
			float num = Mathf.Max(d.x, Mathf.Max(d.y, d.z));
			float b = Mathf.Max(d2.x, Mathf.Max(d2.y, d2.z));
			return Mathf.Max(num, b);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00010230 File Offset: 0x0000E430
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			this.InitPrevFrameStateInfo(ref curState, deltaTime);
			if (this.IsValid)
			{
				Vector3 offset = this.EffectiveOffset;
				Vector3 pos;
				Quaternion orient;
				this.TrackTarget(deltaTime, curState.ReferenceUp, offset, out pos, out orient);
				offset = orient * offset;
				curState.ReferenceUp = orient * Vector3.up;
				Vector3 targetPosition = base.FollowTargetPosition;
				pos += this.GetOffsetForMinimumTargetDistance(pos, offset, curState.RawOrientation * Vector3.forward, curState.ReferenceUp, targetPosition);
				curState.RawPosition = pos + offset;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000102B9 File Offset: 0x0000E4B9
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			base.OnTargetObjectWarped(target, positionDelta);
			if (target == base.FollowTarget)
			{
				this.m_PreviousTargetPosition += positionDelta;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000102E4 File Offset: 0x0000E4E4
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			base.ForceCameraPosition(pos, rot);
			Quaternion targetRot = ((this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp) ? rot : this.GetReferenceOrientation(base.VirtualCamera.State.ReferenceUp));
			this.m_PreviousTargetPosition = pos - targetRot * this.EffectiveOffset;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00010334 File Offset: 0x0000E534
		protected void InitPrevFrameStateInfo(ref CameraState curState, float deltaTime)
		{
			bool prevStateValid = deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid;
			if (this.m_previousTarget != base.FollowTarget || !prevStateValid)
			{
				this.m_previousTarget = base.FollowTarget;
				this.m_targetOrientationOnAssign = base.FollowTargetRotation;
			}
			if (!prevStateValid)
			{
				this.m_PreviousTargetPosition = base.FollowTargetPosition;
				this.m_PreviousReferenceOrientation = this.GetReferenceOrientation(curState.ReferenceUp);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000103A8 File Offset: 0x0000E5A8
		protected void TrackTarget(float deltaTime, Vector3 up, Vector3 desiredCameraOffset, out Vector3 outTargetPosition, out Quaternion outTargetOrient)
		{
			Quaternion targetOrientation = this.GetReferenceOrientation(up);
			Quaternion dampedOrientation = targetOrientation;
			bool prevStateValid = deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid;
			if (prevStateValid)
			{
				if (this.m_AngularDampingMode == CinemachineTransposer.AngularDampingMode.Quaternion && this.m_BindingMode == CinemachineTransposer.BindingMode.LockToTarget)
				{
					float t = base.VirtualCamera.DetachedFollowTargetDamp(1f, this.m_AngularDamping, deltaTime);
					dampedOrientation = Quaternion.Slerp(this.m_PreviousReferenceOrientation, targetOrientation, t);
				}
				else if (this.m_BindingMode != CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					Vector3 relative = (Quaternion.Inverse(this.m_PreviousReferenceOrientation) * targetOrientation).eulerAngles;
					for (int i = 0; i < 3; i++)
					{
						if (relative[i] > 180f)
						{
							ref Vector3 ptr = ref relative;
							int num = i;
							ptr[num] -= 360f;
						}
						if (Mathf.Abs(relative[i]) < 0.01f)
						{
							relative[i] = 0f;
						}
					}
					relative = base.VirtualCamera.DetachedFollowTargetDamp(relative, this.AngularDamping, deltaTime);
					dampedOrientation = this.m_PreviousReferenceOrientation * Quaternion.Euler(relative);
				}
			}
			this.m_PreviousReferenceOrientation = dampedOrientation;
			Vector3 targetPosition = base.FollowTargetPosition;
			Vector3 currentPosition = this.m_PreviousTargetPosition;
			Vector3 previousOffset = (prevStateValid ? this.m_PreviousOffset : desiredCameraOffset);
			if ((desiredCameraOffset - previousOffset).sqrMagnitude > 0.01f)
			{
				Quaternion q = UnityVectorExtensions.SafeFromToRotation(this.m_PreviousOffset.ProjectOntoPlane(up), desiredCameraOffset.ProjectOntoPlane(up), up);
				currentPosition = targetPosition + q * (this.m_PreviousTargetPosition - targetPosition);
			}
			this.m_PreviousOffset = desiredCameraOffset;
			Vector3 positionDelta = targetPosition - currentPosition;
			if (prevStateValid)
			{
				Quaternion dampingSpace;
				if (desiredCameraOffset.AlmostZero())
				{
					dampingSpace = base.VcamState.RawOrientation;
				}
				else
				{
					dampingSpace = Quaternion.LookRotation(dampedOrientation * desiredCameraOffset, up);
				}
				Vector3 localDelta = Quaternion.Inverse(dampingSpace) * positionDelta;
				localDelta = base.VirtualCamera.DetachedFollowTargetDamp(localDelta, this.Damping, deltaTime);
				positionDelta = dampingSpace * localDelta;
			}
			currentPosition += positionDelta;
			outTargetPosition = (this.m_PreviousTargetPosition = currentPosition);
			outTargetOrient = dampedOrientation;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000105D4 File Offset: 0x0000E7D4
		protected Vector3 GetOffsetForMinimumTargetDistance(Vector3 dampedTargetPos, Vector3 cameraOffset, Vector3 cameraFwd, Vector3 up, Vector3 actualTargetPos)
		{
			Vector3 posOffset = Vector3.zero;
			if (base.VirtualCamera.FollowTargetAttachment > 0.9999f)
			{
				cameraOffset = cameraOffset.ProjectOntoPlane(up);
				float minDistance = cameraOffset.magnitude * 0.2f;
				if (minDistance > 0f)
				{
					actualTargetPos = actualTargetPos.ProjectOntoPlane(up);
					dampedTargetPos = dampedTargetPos.ProjectOntoPlane(up);
					Vector3 cameraPos = dampedTargetPos + cameraOffset;
					float d = Vector3.Dot(actualTargetPos - cameraPos, (dampedTargetPos - cameraPos).normalized);
					if (d < minDistance)
					{
						Vector3 dir = actualTargetPos - dampedTargetPos;
						float len = dir.magnitude;
						if (len < 0.01f)
						{
							dir = -cameraFwd.ProjectOntoPlane(up);
						}
						else
						{
							dir /= len;
						}
						posOffset = dir * (minDistance - d);
					}
					this.m_PreviousTargetPosition += posOffset;
				}
			}
			return posOffset;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000106B4 File Offset: 0x0000E8B4
		protected Vector3 Damping
		{
			get
			{
				if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					return new Vector3(0f, this.m_YDamping, this.m_ZDamping);
				}
				return new Vector3(this.m_XDamping, this.m_YDamping, this.m_ZDamping);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000106F0 File Offset: 0x0000E8F0
		protected Vector3 AngularDamping
		{
			get
			{
				switch (this.m_BindingMode)
				{
				case CinemachineTransposer.BindingMode.LockToTargetOnAssign:
				case CinemachineTransposer.BindingMode.WorldSpace:
				case CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp:
					return Vector3.zero;
				case CinemachineTransposer.BindingMode.LockToTargetWithWorldUp:
					return new Vector3(0f, this.m_YawDamping, 0f);
				case CinemachineTransposer.BindingMode.LockToTargetNoRoll:
					return new Vector3(this.m_PitchDamping, this.m_YawDamping, 0f);
				}
				return new Vector3(this.m_PitchDamping, this.m_YawDamping, this.m_RollDamping);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001076E File Offset: 0x0000E96E
		public virtual Vector3 GetTargetCameraPosition(Vector3 worldUp)
		{
			if (!this.IsValid)
			{
				return Vector3.zero;
			}
			return base.FollowTargetPosition + this.GetReferenceOrientation(worldUp) * this.EffectiveOffset;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0001079C File Offset: 0x0000E99C
		public Quaternion GetReferenceOrientation(Vector3 worldUp)
		{
			if (this.m_BindingMode == CinemachineTransposer.BindingMode.WorldSpace)
			{
				return Quaternion.identity;
			}
			if (base.FollowTarget != null)
			{
				Quaternion targetOrientation = base.FollowTarget.rotation;
				switch (this.m_BindingMode)
				{
				case CinemachineTransposer.BindingMode.LockToTargetOnAssign:
					return this.m_targetOrientationOnAssign;
				case CinemachineTransposer.BindingMode.LockToTargetWithWorldUp:
				{
					Vector3 fwd = (targetOrientation * Vector3.forward).ProjectOntoPlane(worldUp);
					if (!fwd.AlmostZero())
					{
						return Quaternion.LookRotation(fwd, worldUp);
					}
					break;
				}
				case CinemachineTransposer.BindingMode.LockToTargetNoRoll:
					return Quaternion.LookRotation(targetOrientation * Vector3.forward, worldUp);
				case CinemachineTransposer.BindingMode.LockToTarget:
					return targetOrientation;
				case CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp:
				{
					Vector3 fwd2 = (base.FollowTargetPosition - base.VcamState.RawPosition).ProjectOntoPlane(worldUp);
					if (!fwd2.AlmostZero())
					{
						return Quaternion.LookRotation(fwd2, worldUp);
					}
					break;
				}
				}
			}
			return this.m_PreviousReferenceOrientation.normalized;
		}

		// Token: 0x0400021F RID: 543
		[Tooltip("The coordinate space to use when interpreting the offset from the target.  This is also used to set the camera's Up vector, which will be maintained when aiming the camera.")]
		public CinemachineTransposer.BindingMode m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;

		// Token: 0x04000220 RID: 544
		[Tooltip("The distance vector that the transposer will attempt to maintain from the Follow target")]
		public Vector3 m_FollowOffset = Vector3.back * 10f;

		// Token: 0x04000221 RID: 545
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain the offset in the X-axis.  Small numbers are more responsive, rapidly translating the camera to keep the target's x-axis offset.  Larger numbers give a more heavy slowly responding camera. Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_XDamping = 1f;

		// Token: 0x04000222 RID: 546
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain the offset in the Y-axis.  Small numbers are more responsive, rapidly translating the camera to keep the target's y-axis offset.  Larger numbers give a more heavy slowly responding camera. Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_YDamping = 1f;

		// Token: 0x04000223 RID: 547
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain the offset in the Z-axis.  Small numbers are more responsive, rapidly translating the camera to keep the target's z-axis offset.  Larger numbers give a more heavy slowly responding camera. Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_ZDamping = 1f;

		// Token: 0x04000224 RID: 548
		public CinemachineTransposer.AngularDampingMode m_AngularDampingMode;

		// Token: 0x04000225 RID: 549
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to track the target rotation's X angle.  Small numbers are more responsive.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_PitchDamping;

		// Token: 0x04000226 RID: 550
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to track the target rotation's Y angle.  Small numbers are more responsive.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_YawDamping;

		// Token: 0x04000227 RID: 551
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to track the target rotation's Z angle.  Small numbers are more responsive.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_RollDamping;

		// Token: 0x04000228 RID: 552
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to track the target's orientation.  Small numbers are more responsive.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_AngularDamping;

		// Token: 0x0400022A RID: 554
		private Vector3 m_PreviousTargetPosition = Vector3.zero;

		// Token: 0x0400022B RID: 555
		private Quaternion m_PreviousReferenceOrientation = Quaternion.identity;

		// Token: 0x0400022C RID: 556
		private Quaternion m_targetOrientationOnAssign = Quaternion.identity;

		// Token: 0x0400022D RID: 557
		private Vector3 m_PreviousOffset;

		// Token: 0x0400022E RID: 558
		private Transform m_previousTarget;

		// Token: 0x0200005F RID: 95
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum BindingMode
		{
			// Token: 0x04000230 RID: 560
			LockToTargetOnAssign,
			// Token: 0x04000231 RID: 561
			LockToTargetWithWorldUp,
			// Token: 0x04000232 RID: 562
			LockToTargetNoRoll,
			// Token: 0x04000233 RID: 563
			LockToTarget,
			// Token: 0x04000234 RID: 564
			WorldSpace,
			// Token: 0x04000235 RID: 565
			SimpleFollowWithWorldUp
		}

		// Token: 0x02000060 RID: 96
		public enum AngularDampingMode
		{
			// Token: 0x04000237 RID: 567
			Euler,
			// Token: 0x04000238 RID: 568
			Quaternion
		}
	}
}
