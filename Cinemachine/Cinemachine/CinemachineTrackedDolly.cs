using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x0200005B RID: 91
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineTrackedDolly : CinemachineComponentBase
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000FBC2 File Offset: 0x0000DDC2
		public override bool IsValid
		{
			get
			{
				return base.enabled && this.m_Path != null;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000C34E File Offset: 0x0000A54E
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Body;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		public override float GetMaxDampTime()
		{
			Vector3 d2 = this.AngularDamping;
			float num = Mathf.Max(this.m_XDamping, Mathf.Max(this.m_YDamping, this.m_ZDamping));
			float b = Mathf.Max(d2.x, Mathf.Max(d2.y, d2.z));
			return Mathf.Max(num, b);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000FC30 File Offset: 0x0000DE30
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (deltaTime < 0f || !base.VirtualCamera.PreviousStateIsValid)
			{
				this.m_PreviousPathPosition = this.m_PathPosition;
				this.m_PreviousCameraPosition = curState.RawPosition;
				this.m_PreviousOrientation = curState.RawOrientation;
			}
			if (!this.IsValid)
			{
				return;
			}
			if (this.m_AutoDolly.m_Enabled && base.FollowTarget != null)
			{
				float prevPos = this.m_Path.ToNativePathUnits(this.m_PreviousPathPosition, this.m_PositionUnits);
				this.m_PathPosition = this.m_Path.FindClosestPoint(base.FollowTargetPosition, Mathf.FloorToInt(prevPos), (deltaTime < 0f || this.m_AutoDolly.m_SearchRadius <= 0) ? (-1) : this.m_AutoDolly.m_SearchRadius, this.m_AutoDolly.m_SearchResolution);
				this.m_PathPosition = this.m_Path.FromPathNativeUnits(this.m_PathPosition, this.m_PositionUnits);
				this.m_PathPosition += this.m_AutoDolly.m_PositionOffset;
			}
			float newPathPosition = this.m_PathPosition;
			if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
			{
				float maxUnit = this.m_Path.MaxUnit(this.m_PositionUnits);
				if (maxUnit > 0f)
				{
					float prev = this.m_Path.StandardizeUnit(this.m_PreviousPathPosition, this.m_PositionUnits);
					float next = this.m_Path.StandardizeUnit(newPathPosition, this.m_PositionUnits);
					if (this.m_Path.Looped && Mathf.Abs(next - prev) > maxUnit / 2f)
					{
						if (next > prev)
						{
							prev += maxUnit;
						}
						else
						{
							prev -= maxUnit;
						}
					}
					this.m_PreviousPathPosition = prev;
					newPathPosition = next;
				}
				float offset = this.m_PreviousPathPosition - newPathPosition;
				offset = Damper.Damp(offset, this.m_ZDamping, deltaTime);
				newPathPosition = this.m_PreviousPathPosition - offset;
			}
			this.m_PreviousPathPosition = newPathPosition;
			Quaternion newPathOrientation = this.m_Path.EvaluateOrientationAtUnit(newPathPosition, this.m_PositionUnits);
			Vector3 newCameraPos = this.m_Path.EvaluatePositionAtUnit(newPathPosition, this.m_PositionUnits);
			Vector3 offsetX = newPathOrientation * Vector3.right;
			Vector3 offsetY = newPathOrientation * Vector3.up;
			Vector3 offsetZ = newPathOrientation * Vector3.forward;
			newCameraPos += this.m_PathOffset.x * offsetX;
			newCameraPos += this.m_PathOffset.y * offsetY;
			newCameraPos += this.m_PathOffset.z * offsetZ;
			if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
			{
				Vector3 previousCameraPosition = this.m_PreviousCameraPosition;
				Vector3 vector = previousCameraPosition - newCameraPos;
				Vector3 delta = Vector3.Dot(vector, offsetY) * offsetY;
				Vector3 delta2 = vector - delta;
				delta2 = Damper.Damp(delta2, this.m_XDamping, deltaTime);
				delta = Damper.Damp(delta, this.m_YDamping, deltaTime);
				newCameraPos = previousCameraPosition - (delta2 + delta);
			}
			curState.RawPosition = (this.m_PreviousCameraPosition = newCameraPos);
			Quaternion newOrientation = this.GetCameraOrientationAtPathPoint(newPathOrientation, curState.ReferenceUp);
			if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
			{
				Vector3 relative = (Quaternion.Inverse(this.m_PreviousOrientation) * newOrientation).eulerAngles;
				for (int i = 0; i < 3; i++)
				{
					if (relative[i] > 180f)
					{
						ref Vector3 ptr = ref relative;
						int num = i;
						ptr[num] -= 360f;
					}
				}
				relative = Damper.Damp(relative, this.AngularDamping, deltaTime);
				newOrientation = this.m_PreviousOrientation * Quaternion.Euler(relative);
			}
			this.m_PreviousOrientation = newOrientation;
			curState.RawOrientation = newOrientation;
			if (this.m_CameraUp != CinemachineTrackedDolly.CameraUpMode.Default)
			{
				curState.ReferenceUp = curState.RawOrientation * Vector3.up;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00010000 File Offset: 0x0000E200
		private Quaternion GetCameraOrientationAtPathPoint(Quaternion pathOrientation, Vector3 up)
		{
			switch (this.m_CameraUp)
			{
			case CinemachineTrackedDolly.CameraUpMode.Path:
				return pathOrientation;
			case CinemachineTrackedDolly.CameraUpMode.PathNoRoll:
				return Quaternion.LookRotation(pathOrientation * Vector3.forward, up);
			case CinemachineTrackedDolly.CameraUpMode.FollowTarget:
				if (base.FollowTarget != null)
				{
					return base.FollowTargetRotation;
				}
				break;
			case CinemachineTrackedDolly.CameraUpMode.FollowTargetNoRoll:
				if (base.FollowTarget != null)
				{
					return Quaternion.LookRotation(base.FollowTargetRotation * Vector3.forward, up);
				}
				break;
			}
			return Quaternion.LookRotation(base.VirtualCamera.transform.rotation * Vector3.forward, up);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000100A0 File Offset: 0x0000E2A0
		private Vector3 AngularDamping
		{
			get
			{
				switch (this.m_CameraUp)
				{
				case CinemachineTrackedDolly.CameraUpMode.Default:
					return Vector3.zero;
				case CinemachineTrackedDolly.CameraUpMode.PathNoRoll:
				case CinemachineTrackedDolly.CameraUpMode.FollowTargetNoRoll:
					return new Vector3(this.m_PitchDamping, this.m_YawDamping, 0f);
				}
				return new Vector3(this.m_PitchDamping, this.m_YawDamping, this.m_RollDamping);
			}
		}

		// Token: 0x04000206 RID: 518
		[Tooltip("The path to which the camera will be constrained.  This must be non-null.")]
		public CinemachinePathBase m_Path;

		// Token: 0x04000207 RID: 519
		[Tooltip("The position along the path at which the camera will be placed.  This can be animated directly, or set automatically by the Auto-Dolly feature to get as close as possible to the Follow target.  The value is interpreted according to the Position Units setting.")]
		public float m_PathPosition;

		// Token: 0x04000208 RID: 520
		[Tooltip("How to interpret Path Position.  If set to Path Units, values are as follows: 0 represents the first waypoint on the path, 1 is the second, and so on.  Values in-between are points on the path in between the waypoints.  If set to Distance, then Path Position represents distance along the path.")]
		public CinemachinePathBase.PositionUnits m_PositionUnits;

		// Token: 0x04000209 RID: 521
		[Tooltip("Where to put the camera relative to the path position.  X is perpendicular to the path, Y is up, and Z is parallel to the path.  This allows the camera to be offset from the path itself (as if on a tripod, for example).")]
		public Vector3 m_PathOffset = Vector3.zero;

		// Token: 0x0400020A RID: 522
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain its position in a direction perpendicular to the path.  Small numbers are more responsive, rapidly translating the camera to keep the target's x-axis offset.  Larger numbers give a more heavy slowly responding camera. Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_XDamping;

		// Token: 0x0400020B RID: 523
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain its position in the path-local up direction.  Small numbers are more responsive, rapidly translating the camera to keep the target's y-axis offset.  Larger numbers give a more heavy slowly responding camera. Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_YDamping;

		// Token: 0x0400020C RID: 524
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain its position in a direction parallel to the path.  Small numbers are more responsive, rapidly translating the camera to keep the target's z-axis offset.  Larger numbers give a more heavy slowly responding camera. Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_ZDamping = 1f;

		// Token: 0x0400020D RID: 525
		[Tooltip("How to set the virtual camera's Up vector.  This will affect the screen composition, because the camera Aim behaviours will always try to respect the Up direction.")]
		public CinemachineTrackedDolly.CameraUpMode m_CameraUp;

		// Token: 0x0400020E RID: 526
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to track the target rotation's X angle.  Small numbers are more responsive.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_PitchDamping;

		// Token: 0x0400020F RID: 527
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to track the target rotation's Y angle.  Small numbers are more responsive.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_YawDamping;

		// Token: 0x04000210 RID: 528
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to track the target rotation's Z angle.  Small numbers are more responsive.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_RollDamping;

		// Token: 0x04000211 RID: 529
		[Tooltip("Controls how automatic dollying occurs.  A Follow target is necessary to use this feature.")]
		public CinemachineTrackedDolly.AutoDolly m_AutoDolly = new CinemachineTrackedDolly.AutoDolly(false, 0f, 2, 5);

		// Token: 0x04000212 RID: 530
		private float m_PreviousPathPosition;

		// Token: 0x04000213 RID: 531
		private Quaternion m_PreviousOrientation = Quaternion.identity;

		// Token: 0x04000214 RID: 532
		private Vector3 m_PreviousCameraPosition = Vector3.zero;

		// Token: 0x0200005C RID: 92
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum CameraUpMode
		{
			// Token: 0x04000216 RID: 534
			Default,
			// Token: 0x04000217 RID: 535
			Path,
			// Token: 0x04000218 RID: 536
			PathNoRoll,
			// Token: 0x04000219 RID: 537
			FollowTarget,
			// Token: 0x0400021A RID: 538
			FollowTargetNoRoll
		}

		// Token: 0x0200005D RID: 93
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct AutoDolly
		{
			// Token: 0x06000238 RID: 568 RVA: 0x00010156 File Offset: 0x0000E356
			public AutoDolly(bool enabled, float positionOffset, int searchRadius, int stepsPerSegment)
			{
				this.m_Enabled = enabled;
				this.m_PositionOffset = positionOffset;
				this.m_SearchRadius = searchRadius;
				this.m_SearchResolution = stepsPerSegment;
			}

			// Token: 0x0400021B RID: 539
			[Tooltip("If checked, will enable automatic dolly, which chooses a path position that is as close as possible to the Follow target.  Note: this can have significant performance impact")]
			public bool m_Enabled;

			// Token: 0x0400021C RID: 540
			[Tooltip("Offset, in current position units, from the closest point on the path to the follow target")]
			public float m_PositionOffset;

			// Token: 0x0400021D RID: 541
			[Tooltip("Search up to this many waypoints on either side of the current position.  Use 0 for Entire path.")]
			public int m_SearchRadius;

			// Token: 0x0400021E RID: 542
			[FormerlySerializedAs("m_StepsPerSegment")]
			[Tooltip("We search between waypoints by dividing the segment into this many straight pieces.  he higher the number, the more accurate the result, but performance is proportionally slower for higher numbers")]
			public int m_SearchResolution;
		}
	}
}
