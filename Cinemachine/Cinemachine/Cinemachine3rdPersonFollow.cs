using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000047 RID: 71
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class Cinemachine3rdPersonFollow : CinemachineComponentBase
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		private void OnValidate()
		{
			this.CameraSide = Mathf.Clamp(this.CameraSide, -1f, 1f);
			this.Damping.x = Mathf.Max(0f, this.Damping.x);
			this.Damping.y = Mathf.Max(0f, this.Damping.y);
			this.Damping.z = Mathf.Max(0f, this.Damping.z);
			this.CameraRadius = Mathf.Max(0.001f, this.CameraRadius);
			this.DampingIntoCollision = Mathf.Max(0f, this.DampingIntoCollision);
			this.DampingFromCollision = Mathf.Max(0f, this.DampingFromCollision);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		private void Reset()
		{
			this.ShoulderOffset = new Vector3(0.5f, -0.4f, 0f);
			this.VerticalArmLength = 0.4f;
			this.CameraSide = 1f;
			this.CameraDistance = 2f;
			this.Damping = new Vector3(0.1f, 0.5f, 0.3f);
			this.CameraCollisionFilter = 0;
			this.CameraRadius = 0.2f;
			this.DampingIntoCollision = 0f;
			this.DampingFromCollision = 2f;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C32F File Offset: 0x0000A52F
		private void OnDestroy()
		{
			RuntimeUtility.DestroyScratchCollider();
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000C336 File Offset: 0x0000A536
		public override bool IsValid
		{
			get
			{
				return base.enabled && base.FollowTarget != null;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000C34E File Offset: 0x0000A54E
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Body;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000C354 File Offset: 0x0000A554
		public override float GetMaxDampTime()
		{
			return Mathf.Max(Mathf.Max(this.DampingIntoCollision, this.DampingFromCollision), Mathf.Max(this.Damping.x, Mathf.Max(this.Damping.y, this.Damping.z)));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000C3A2 File Offset: 0x0000A5A2
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (this.IsValid)
			{
				if (!base.VirtualCamera.PreviousStateIsValid)
				{
					deltaTime = -1f;
				}
				this.PositionCamera(ref curState, deltaTime);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			base.OnTargetObjectWarped(target, positionDelta);
			if (target == base.FollowTarget)
			{
				this.m_PreviousFollowTargetPosition += positionDelta;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		private void PositionCamera(ref CameraState curState, float deltaTime)
		{
			Vector3 up = curState.ReferenceUp;
			Vector3 targetPos = base.FollowTargetPosition;
			Quaternion targetRot = base.FollowTargetRotation;
			Vector3 targetForward = targetRot * Vector3.forward;
			Quaternion heading = Cinemachine3rdPersonFollow.GetHeading(targetRot, up);
			if (deltaTime < 0f)
			{
				this.m_DampingCorrection = Vector3.zero;
				this.m_CamPosCollisionCorrection = 0f;
			}
			else
			{
				this.m_DampingCorrection += Quaternion.Inverse(heading) * (this.m_PreviousFollowTargetPosition - targetPos);
				this.m_DampingCorrection -= base.VirtualCamera.DetachedFollowTargetDamp(this.m_DampingCorrection, this.Damping, deltaTime);
			}
			this.m_PreviousFollowTargetPosition = targetPos;
			Vector3 root = targetPos;
			Vector3 vector;
			Vector3 hand;
			this.GetRawRigPositions(root, targetRot, heading, out vector, out hand);
			Vector3 camPos = hand - targetForward * (this.CameraDistance - this.m_DampingCorrection.z);
			float dummy = 0f;
			Vector3 collidedHand = this.ResolveCollisions(root, hand, -1f, this.CameraRadius * 1.05f, ref dummy);
			camPos = this.ResolveCollisions(collidedHand, camPos, deltaTime, this.CameraRadius, ref this.m_CamPosCollisionCorrection);
			curState.RawPosition = camPos;
			curState.RawOrientation = targetRot;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000C528 File Offset: 0x0000A728
		public void GetRigPositions(out Vector3 root, out Vector3 shoulder, out Vector3 hand)
		{
			Vector3 up = base.VirtualCamera.State.ReferenceUp;
			Quaternion targetRot = base.FollowTargetRotation;
			Quaternion heading = Cinemachine3rdPersonFollow.GetHeading(targetRot, up);
			root = this.m_PreviousFollowTargetPosition;
			this.GetRawRigPositions(root, targetRot, heading, out shoulder, out hand);
			float dummy = 0f;
			hand = this.ResolveCollisions(root, hand, -1f, this.CameraRadius * 1.05f, ref dummy);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		internal static Quaternion GetHeading(Quaternion targetRot, Vector3 up)
		{
			Vector3 targetForward = targetRot * Vector3.forward;
			Vector3 planeForward = Vector3.Cross(up, Vector3.Cross(targetForward.ProjectOntoPlane(up), up));
			if (planeForward.AlmostZero())
			{
				planeForward = Vector3.Cross(targetRot * Vector3.right, up);
			}
			return Quaternion.LookRotation(planeForward, up);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		private void GetRawRigPositions(Vector3 root, Quaternion targetRot, Quaternion heading, out Vector3 shoulder, out Vector3 hand)
		{
			Vector3 shoulderOffset = this.ShoulderOffset;
			shoulderOffset.x = Mathf.Lerp(-shoulderOffset.x, shoulderOffset.x, this.CameraSide);
			shoulderOffset.x += this.m_DampingCorrection.x;
			shoulderOffset.y += this.m_DampingCorrection.y;
			shoulder = root + heading * shoulderOffset;
			hand = shoulder + targetRot * new Vector3(0f, this.VerticalArmLength, 0f);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C698 File Offset: 0x0000A898
		private Vector3 ResolveCollisions(Vector3 root, Vector3 tip, float deltaTime, float cameraRadius, ref float collisionCorrection)
		{
			if (this.CameraCollisionFilter.value == 0)
			{
				return tip;
			}
			Vector3 dir = tip - root;
			float len = dir.magnitude;
			if (len < 0.0001f)
			{
				return tip;
			}
			dir /= len;
			Vector3 result = tip;
			float desiredCorrection = 0f;
			RaycastHit hitInfo;
			if (RuntimeUtility.SphereCastIgnoreTag(root, cameraRadius, dir, out hitInfo, len, this.CameraCollisionFilter, in this.IgnoreTag))
			{
				desiredCorrection = (hitInfo.point + hitInfo.normal * cameraRadius - tip).magnitude;
			}
			collisionCorrection += ((deltaTime < 0f) ? (desiredCorrection - collisionCorrection) : Damper.Damp(desiredCorrection - collisionCorrection, (desiredCorrection > collisionCorrection) ? this.DampingIntoCollision : this.DampingFromCollision, deltaTime));
			if (collisionCorrection > 0.0001f)
			{
				result -= dir * collisionCorrection;
			}
			return result;
		}

		// Token: 0x04000163 RID: 355
		[Tooltip("How responsively the camera tracks the target.  Each axis (camera-local) can have its own setting.  Value is the approximate time it takes the camera to catch up to the target's new position.  Smaller values give a more rigid effect, larger values give a squishier one")]
		public Vector3 Damping;

		// Token: 0x04000164 RID: 356
		[Header("Rig")]
		[Tooltip("Position of the shoulder pivot relative to the Follow target origin.  This offset is in target-local space")]
		public Vector3 ShoulderOffset;

		// Token: 0x04000165 RID: 357
		[Tooltip("Vertical offset of the hand in relation to the shoulder.  Arm length will affect the follow target's screen position when the camera rotates vertically")]
		public float VerticalArmLength;

		// Token: 0x04000166 RID: 358
		[Tooltip("Specifies which shoulder (left, right, or in-between) the camera is on")]
		[Range(0f, 1f)]
		public float CameraSide;

		// Token: 0x04000167 RID: 359
		[Tooltip("How far behind the hand the camera will be placed")]
		public float CameraDistance;

		// Token: 0x04000168 RID: 360
		[Header("Obstacles")]
		[Tooltip("Camera will avoid obstacles on these layers")]
		public LayerMask CameraCollisionFilter;

		// Token: 0x04000169 RID: 361
		[TagField]
		[Tooltip("Obstacles with this tag will be ignored.  It is a good idea to set this field to the target's tag")]
		public string IgnoreTag = string.Empty;

		// Token: 0x0400016A RID: 362
		[Tooltip("Specifies how close the camera can get to obstacles")]
		[Range(0f, 1f)]
		public float CameraRadius;

		// Token: 0x0400016B RID: 363
		[Range(0f, 10f)]
		[Tooltip("How gradually the camera moves to correct for occlusions.  Higher numbers will move the camera more gradually.")]
		public float DampingIntoCollision;

		// Token: 0x0400016C RID: 364
		[Range(0f, 10f)]
		[Tooltip("How gradually the camera returns to its normal position after having been corrected by the built-in collision resolution system.  Higher numbers will move the camera more gradually back to normal.")]
		public float DampingFromCollision;

		// Token: 0x0400016D RID: 365
		private Vector3 m_PreviousFollowTargetPosition;

		// Token: 0x0400016E RID: 366
		private Vector3 m_DampingCorrection;

		// Token: 0x0400016F RID: 367
		private float m_CamPosCollisionCorrection;
	}
}
