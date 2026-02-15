using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x0200004B RID: 75
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineFramingTransposer : CinemachineComponentBase
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000D606 File Offset: 0x0000B806
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000D640 File Offset: 0x0000B840
		internal Rect SoftGuideRect
		{
			get
			{
				return new Rect(this.m_ScreenX - this.m_DeadZoneWidth / 2f, this.m_ScreenY - this.m_DeadZoneHeight / 2f, this.m_DeadZoneWidth, this.m_DeadZoneHeight);
			}
			set
			{
				this.m_DeadZoneWidth = Mathf.Clamp(value.width, 0f, 2f);
				this.m_DeadZoneHeight = Mathf.Clamp(value.height, 0f, 2f);
				this.m_ScreenX = Mathf.Clamp(value.x + this.m_DeadZoneWidth / 2f, -0.5f, 1.5f);
				this.m_ScreenY = Mathf.Clamp(value.y + this.m_DeadZoneHeight / 2f, -0.5f, 1.5f);
				this.m_SoftZoneWidth = Mathf.Max(this.m_SoftZoneWidth, this.m_DeadZoneWidth);
				this.m_SoftZoneHeight = Mathf.Max(this.m_SoftZoneHeight, this.m_DeadZoneHeight);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000D708 File Offset: 0x0000B908
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000D790 File Offset: 0x0000B990
		internal Rect HardGuideRect
		{
			get
			{
				Rect r = new Rect(this.m_ScreenX - this.m_SoftZoneWidth / 2f, this.m_ScreenY - this.m_SoftZoneHeight / 2f, this.m_SoftZoneWidth, this.m_SoftZoneHeight);
				r.position += new Vector2(this.m_BiasX * (this.m_SoftZoneWidth - this.m_DeadZoneWidth), this.m_BiasY * (this.m_SoftZoneHeight - this.m_DeadZoneHeight));
				return r;
			}
			set
			{
				this.m_SoftZoneWidth = Mathf.Clamp(value.width, 0f, 2f);
				this.m_SoftZoneHeight = Mathf.Clamp(value.height, 0f, 2f);
				this.m_DeadZoneWidth = Mathf.Min(this.m_DeadZoneWidth, this.m_SoftZoneWidth);
				this.m_DeadZoneHeight = Mathf.Min(this.m_DeadZoneHeight, this.m_SoftZoneHeight);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D804 File Offset: 0x0000BA04
		private void OnValidate()
		{
			this.m_CameraDistance = Mathf.Max(this.m_CameraDistance, 0.01f);
			this.m_DeadZoneDepth = Mathf.Max(this.m_DeadZoneDepth, 0f);
			this.m_GroupFramingSize = Mathf.Max(0.001f, this.m_GroupFramingSize);
			this.m_MaxDollyIn = Mathf.Max(0f, this.m_MaxDollyIn);
			this.m_MaxDollyOut = Mathf.Max(0f, this.m_MaxDollyOut);
			this.m_MinimumDistance = Mathf.Max(0f, this.m_MinimumDistance);
			this.m_MaximumDistance = Mathf.Max(this.m_MinimumDistance, this.m_MaximumDistance);
			this.m_MinimumFOV = Mathf.Max(1f, this.m_MinimumFOV);
			this.m_MaximumFOV = Mathf.Clamp(this.m_MaximumFOV, this.m_MinimumFOV, 179f);
			this.m_MinimumOrthoSize = Mathf.Max(0.01f, this.m_MinimumOrthoSize);
			this.m_MaximumOrthoSize = Mathf.Max(this.m_MinimumOrthoSize, this.m_MaximumOrthoSize);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000C336 File Offset: 0x0000A536
		public override bool IsValid
		{
			get
			{
				return base.enabled && base.FollowTarget != null;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C34E File Offset: 0x0000A54E
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Body;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000771A File Offset: 0x0000591A
		public override bool BodyAppliesAfterAim
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000D90B File Offset: 0x0000BB0B
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000D913 File Offset: 0x0000BB13
		public Vector3 TrackedPoint { get; private set; }

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D91C File Offset: 0x0000BB1C
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			base.OnTargetObjectWarped(target, positionDelta);
			if (target == base.FollowTarget)
			{
				this.m_PreviousCameraPosition += positionDelta;
				this.m_Predictor.ApplyTransformDelta(positionDelta);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D952 File Offset: 0x0000BB52
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			base.ForceCameraPosition(pos, rot);
			this.m_PreviousCameraPosition = pos;
			this.m_prevRotation = rot;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000D96A File Offset: 0x0000BB6A
		public override float GetMaxDampTime()
		{
			return Mathf.Max(this.m_XDamping, Mathf.Max(this.m_YDamping, this.m_ZDamping));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D988 File Offset: 0x0000BB88
		public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime, ref CinemachineVirtualCameraBase.TransitionParams transitionParams)
		{
			if (fromCam != null && transitionParams.m_InheritPosition && !CinemachineCore.Instance.IsLiveInBlend(base.VirtualCamera))
			{
				this.m_PreviousCameraPosition = fromCam.State.RawPosition;
				this.m_prevRotation = fromCam.State.RawOrientation;
				this.m_InheritingPosition = true;
				return true;
			}
			return false;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		private Rect ScreenToOrtho(Rect rScreen, float orthoSize, float aspect)
		{
			return new Rect
			{
				yMax = 2f * orthoSize * (1f - rScreen.yMin - 0.5f),
				yMin = 2f * orthoSize * (1f - rScreen.yMax - 0.5f),
				xMin = 2f * orthoSize * aspect * (rScreen.xMin - 0.5f),
				xMax = 2f * orthoSize * aspect * (rScreen.xMax - 0.5f)
			};
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000DA78 File Offset: 0x0000BC78
		private Vector3 OrthoOffsetToScreenBounds(Vector3 targetPos2D, Rect screenRect)
		{
			Vector3 delta = Vector3.zero;
			if (targetPos2D.x < screenRect.xMin)
			{
				delta.x += targetPos2D.x - screenRect.xMin;
			}
			if (targetPos2D.x > screenRect.xMax)
			{
				delta.x += targetPos2D.x - screenRect.xMax;
			}
			if (targetPos2D.y < screenRect.yMin)
			{
				delta.y += targetPos2D.y - screenRect.yMin;
			}
			if (targetPos2D.y > screenRect.yMax)
			{
				delta.y += targetPos2D.y - screenRect.yMax;
			}
			return delta;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000DB2C File Offset: 0x0000BD2C
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public Bounds LastBounds { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000DB3D File Offset: 0x0000BD3D
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000DB45 File Offset: 0x0000BD45
		public Matrix4x4 LastBoundsMatrix { get; private set; }

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			LensSettings lens = curState.Lens;
			Vector3 followTargetPosition = base.FollowTargetPosition + base.FollowTargetRotation * this.m_TrackedObjectOffset;
			bool previousStateIsValid = deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid;
			if (!previousStateIsValid || base.VirtualCamera.FollowTargetChanged)
			{
				this.m_Predictor.Reset();
			}
			if (!previousStateIsValid)
			{
				this.m_PreviousCameraPosition = curState.RawPosition;
				this.m_prevFOV = (lens.Orthographic ? lens.OrthographicSize : lens.FieldOfView);
				this.m_prevRotation = curState.RawOrientation;
				if (!this.m_InheritingPosition && this.m_CenterOnActivate)
				{
					this.m_PreviousCameraPosition = base.FollowTargetPosition + curState.RawOrientation * Vector3.back * this.m_CameraDistance;
				}
			}
			if (!this.IsValid)
			{
				this.m_InheritingPosition = false;
				return;
			}
			float verticalFOV = lens.FieldOfView;
			ICinemachineTargetGroup group = base.AbstractFollowTargetGroup;
			bool isGroupFraming = group != null && this.m_GroupFramingMode != CinemachineFramingTransposer.FramingMode.None && !group.IsEmpty;
			if (isGroupFraming)
			{
				followTargetPosition = this.ComputeGroupBounds(group, ref curState);
			}
			this.TrackedPoint = followTargetPosition;
			if (this.m_LookaheadTime > 0.0001f)
			{
				this.m_Predictor.Smoothing = this.m_LookaheadSmoothing;
				this.m_Predictor.AddPosition(followTargetPosition, deltaTime, this.m_LookaheadTime);
				Vector3 delta = this.m_Predictor.PredictPositionDelta(this.m_LookaheadTime);
				if (this.m_LookaheadIgnoreY)
				{
					delta = delta.ProjectOntoPlane(curState.ReferenceUp);
				}
				Vector3 p = followTargetPosition + delta;
				if (isGroupFraming)
				{
					Bounds b = this.LastBounds;
					b.center += this.LastBoundsMatrix.MultiplyPoint3x4(delta);
					this.LastBounds = b;
				}
				this.TrackedPoint = p;
			}
			if (!curState.HasLookAt)
			{
				curState.ReferenceLookAt = followTargetPosition;
			}
			float targetDistance = this.m_CameraDistance;
			bool isOrthographic = lens.Orthographic;
			float targetHeight = (isGroupFraming ? this.GetTargetHeight(this.LastBounds.size / this.m_GroupFramingSize) : 0f);
			targetHeight = Mathf.Max(targetHeight, 0.01f);
			if (!isOrthographic && isGroupFraming)
			{
				float boundsDepth = this.LastBounds.extents.z;
				float z = this.LastBounds.center.z;
				if (z > boundsDepth)
				{
					targetHeight = Mathf.Lerp(0f, targetHeight, (z - boundsDepth) / z);
				}
				if (this.m_AdjustmentMode != CinemachineFramingTransposer.AdjustmentMode.ZoomOnly)
				{
					targetDistance = targetHeight / (2f * Mathf.Tan(verticalFOV * 0.017453292f / 2f));
					targetDistance = Mathf.Clamp(targetDistance, this.m_MinimumDistance, this.m_MaximumDistance);
					float targetDelta = targetDistance - this.m_CameraDistance;
					targetDelta = Mathf.Clamp(targetDelta, -this.m_MaxDollyIn, this.m_MaxDollyOut);
					targetDistance = this.m_CameraDistance + targetDelta;
				}
			}
			Quaternion localToWorld = curState.RawOrientation;
			if (previousStateIsValid && this.m_TargetMovementOnly)
			{
				Quaternion q = localToWorld * Quaternion.Inverse(this.m_prevRotation);
				this.m_PreviousCameraPosition = this.TrackedPoint + q * (this.m_PreviousCameraPosition - this.TrackedPoint);
			}
			this.m_prevRotation = localToWorld;
			if (isGroupFraming)
			{
				if (isOrthographic)
				{
					targetHeight = Mathf.Clamp(targetHeight / 2f, this.m_MinimumOrthoSize, this.m_MaximumOrthoSize);
					if (previousStateIsValid)
					{
						targetHeight = this.m_prevFOV + base.VirtualCamera.DetachedFollowTargetDamp(targetHeight - this.m_prevFOV, this.m_ZDamping, deltaTime);
					}
					this.m_prevFOV = targetHeight;
					lens.OrthographicSize = Mathf.Clamp(targetHeight, this.m_MinimumOrthoSize, this.m_MaximumOrthoSize);
					curState.Lens = lens;
				}
				else if (this.m_AdjustmentMode != CinemachineFramingTransposer.AdjustmentMode.DollyOnly)
				{
					float nearBoundsDistance = (Quaternion.Inverse(curState.RawOrientation) * (followTargetPosition - curState.RawPosition)).z;
					float targetFOV = 179f;
					if (nearBoundsDistance > 0.0001f)
					{
						targetFOV = 2f * Mathf.Atan(targetHeight / (2f * nearBoundsDistance)) * 57.29578f;
					}
					targetFOV = Mathf.Clamp(targetFOV, this.m_MinimumFOV, this.m_MaximumFOV);
					if (previousStateIsValid)
					{
						targetFOV = this.m_prevFOV + base.VirtualCamera.DetachedFollowTargetDamp(targetFOV - this.m_prevFOV, this.m_ZDamping, deltaTime);
					}
					this.m_prevFOV = targetFOV;
					lens.FieldOfView = targetFOV;
					curState.Lens = lens;
				}
			}
			Vector3 camPosWorld = this.m_PreviousCameraPosition;
			Quaternion worldToLocal = Quaternion.Inverse(localToWorld);
			Vector3 cameraPos = worldToLocal * camPosWorld;
			Vector3 targetPos = worldToLocal * this.TrackedPoint - cameraPos;
			Vector3 lookAtPos = targetPos;
			Vector3 cameraOffset = Vector3.zero;
			float cameraMin = Mathf.Max(0.01f, targetDistance - this.m_DeadZoneDepth / 2f);
			float cameraMax = Mathf.Max(cameraMin, targetDistance + this.m_DeadZoneDepth / 2f);
			float targetZ = Mathf.Min(targetPos.z, lookAtPos.z);
			if (targetZ < cameraMin)
			{
				cameraOffset.z = targetZ - cameraMin;
			}
			if (targetZ > cameraMax)
			{
				cameraOffset.z = targetZ - cameraMax;
			}
			float screenSize = (lens.Orthographic ? lens.OrthographicSize : (Mathf.Tan(0.5f * verticalFOV * 0.017453292f) * (targetZ - cameraOffset.z)));
			Rect softGuideOrtho = this.ScreenToOrtho(this.SoftGuideRect, screenSize, lens.Aspect);
			if (!previousStateIsValid)
			{
				Rect rect = softGuideOrtho;
				if (this.m_CenterOnActivate && !this.m_InheritingPosition)
				{
					rect = new Rect(rect.center, Vector2.zero);
				}
				cameraOffset += this.OrthoOffsetToScreenBounds(targetPos, rect);
			}
			else
			{
				cameraOffset += this.OrthoOffsetToScreenBounds(targetPos, softGuideOrtho);
				cameraOffset = base.VirtualCamera.DetachedFollowTargetDamp(cameraOffset, new Vector3(this.m_XDamping, this.m_YDamping, this.m_ZDamping), deltaTime);
				if (!this.m_UnlimitedSoftZone && (deltaTime < 0f || base.VirtualCamera.FollowTargetAttachment > 0.9999f))
				{
					Rect hardGuideOrtho = this.ScreenToOrtho(this.HardGuideRect, screenSize, lens.Aspect);
					Vector3 realTargetPos = worldToLocal * followTargetPosition - cameraPos;
					cameraOffset += this.OrthoOffsetToScreenBounds(realTargetPos - cameraOffset, hardGuideOrtho);
				}
			}
			curState.RawPosition = localToWorld * (cameraPos + cameraOffset);
			this.m_PreviousCameraPosition = curState.RawPosition;
			this.m_InheritingPosition = false;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		private float GetTargetHeight(Vector2 boundsSize)
		{
			CameraState cameraState;
			switch (this.m_GroupFramingMode)
			{
			case CinemachineFramingTransposer.FramingMode.Horizontal:
			{
				float x = boundsSize.x;
				cameraState = base.VcamState;
				return x / cameraState.Lens.Aspect;
			}
			case CinemachineFramingTransposer.FramingMode.Vertical:
				return boundsSize.y;
			}
			float x2 = boundsSize.x;
			cameraState = base.VcamState;
			return Mathf.Max(x2 / cameraState.Lens.Aspect, boundsSize.y);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000E228 File Offset: 0x0000C428
		private Vector3 ComputeGroupBounds(ICinemachineTargetGroup group, ref CameraState curState)
		{
			Vector3 cameraPos = curState.RawPosition;
			Vector3 fwd = curState.RawOrientation * Vector3.forward;
			this.LastBoundsMatrix = Matrix4x4.TRS(cameraPos, curState.RawOrientation, Vector3.one);
			Bounds b = group.GetViewSpaceBoundingBox(this.LastBoundsMatrix);
			Vector3 groupCenter = this.LastBoundsMatrix.MultiplyPoint3x4(b.center);
			float boundsDepth = b.extents.z;
			if (!curState.Lens.Orthographic)
			{
				float d = (Quaternion.Inverse(curState.RawOrientation) * (groupCenter - cameraPos)).z;
				cameraPos = groupCenter - fwd * (Mathf.Max(d, boundsDepth) + boundsDepth);
				b = CinemachineFramingTransposer.GetScreenSpaceGroupBoundingBox(group, ref cameraPos, curState.RawOrientation);
				this.LastBoundsMatrix = Matrix4x4.TRS(cameraPos, curState.RawOrientation, Vector3.one);
				groupCenter = this.LastBoundsMatrix.MultiplyPoint3x4(b.center);
			}
			this.LastBounds = b;
			return groupCenter - fwd * boundsDepth;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E32C File Offset: 0x0000C52C
		private static Bounds GetScreenSpaceGroupBoundingBox(ICinemachineTargetGroup group, ref Vector3 pos, Quaternion orientation)
		{
			Matrix4x4 observer = Matrix4x4.TRS(pos, orientation, Vector3.one);
			Vector2 minAngles;
			Vector2 maxAngles;
			Vector2 zRange;
			group.GetViewSpaceAngularBounds(observer, out minAngles, out maxAngles, out zRange);
			Vector2 shift = (minAngles + maxAngles) / 2f;
			Quaternion q = Quaternion.identity.ApplyCameraRotation(new Vector2(-shift.x, shift.y), Vector3.up);
			pos = q * new Vector3(0f, 0f, (zRange.y + zRange.x) / 2f);
			pos.z = 0f;
			pos = observer.MultiplyPoint3x4(pos);
			observer = Matrix4x4.TRS(pos, orientation, Vector3.one);
			group.GetViewSpaceAngularBounds(observer, out minAngles, out maxAngles, out zRange);
			float d = zRange.y + zRange.x;
			Vector2 angles = new Vector2(89.5f, 89.5f);
			if (zRange.x > 0f)
			{
				angles = Vector2.Max(maxAngles, minAngles.Abs());
				angles = Vector2.Min(angles, new Vector2(89.5f, 89.5f));
			}
			angles *= 0.017453292f;
			return new Bounds(new Vector3(0f, 0f, d / 2f), new Vector3(Mathf.Tan(angles.y) * d, Mathf.Tan(angles.x) * d, zRange.y - zRange.x));
		}

		// Token: 0x04000195 RID: 405
		[Tooltip("Offset from the Follow Target object (in target-local co-ordinates).  The camera will attempt to frame the point which is the target's position plus this offset.  Use it to correct for cases when the target's origin is not the point of interest for the camera.")]
		public Vector3 m_TrackedObjectOffset;

		// Token: 0x04000196 RID: 406
		[Tooltip("This setting will instruct the composer to adjust its target offset based on the motion of the target.  The composer will look at a point where it estimates the target will be this many seconds into the future.  Note that this setting is sensitive to noisy animation, and can amplify the noise, resulting in undesirable camera jitter.  If the camera jitters unacceptably when the target is in motion, turn down this setting, or animate the target more smoothly.")]
		[Range(0f, 1f)]
		[Space]
		public float m_LookaheadTime;

		// Token: 0x04000197 RID: 407
		[Tooltip("Controls the smoothness of the lookahead algorithm.  Larger values smooth out jittery predictions and also increase prediction lag")]
		[Range(0f, 30f)]
		public float m_LookaheadSmoothing;

		// Token: 0x04000198 RID: 408
		[Tooltip("If checked, movement along the Y axis will be ignored for lookahead calculations")]
		public bool m_LookaheadIgnoreY;

		// Token: 0x04000199 RID: 409
		[Space]
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain the offset in the X-axis.  Small numbers are more responsive, rapidly translating the camera to keep the target's x-axis offset.  Larger numbers give a more heavy slowly responding camera.  Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_XDamping = 1f;

		// Token: 0x0400019A RID: 410
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain the offset in the Y-axis.  Small numbers are more responsive, rapidly translating the camera to keep the target's y-axis offset.  Larger numbers give a more heavy slowly responding camera.  Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_YDamping = 1f;

		// Token: 0x0400019B RID: 411
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to maintain the offset in the Z-axis.  Small numbers are more responsive, rapidly translating the camera to keep the target's z-axis offset.  Larger numbers give a more heavy slowly responding camera.  Using different settings per axis can yield a wide range of camera behaviors.")]
		public float m_ZDamping = 1f;

		// Token: 0x0400019C RID: 412
		[Tooltip("If set, damping will apply  only to target motion, but not to camera rotation changes.  Turn this on to get an instant response when the rotation changes.  ")]
		public bool m_TargetMovementOnly = true;

		// Token: 0x0400019D RID: 413
		[Space]
		[Range(-0.5f, 1.5f)]
		[Tooltip("Horizontal screen position for target. The camera will move to position the tracked object here.")]
		public float m_ScreenX = 0.5f;

		// Token: 0x0400019E RID: 414
		[Range(-0.5f, 1.5f)]
		[Tooltip("Vertical screen position for target, The camera will move to position the tracked object here.")]
		public float m_ScreenY = 0.5f;

		// Token: 0x0400019F RID: 415
		[Tooltip("The distance along the camera axis that will be maintained from the Follow target")]
		public float m_CameraDistance = 10f;

		// Token: 0x040001A0 RID: 416
		[Space]
		[Range(0f, 2f)]
		[Tooltip("Camera will not move horizontally if the target is within this range of the position.")]
		public float m_DeadZoneWidth;

		// Token: 0x040001A1 RID: 417
		[Range(0f, 2f)]
		[Tooltip("Camera will not move vertically if the target is within this range of the position.")]
		public float m_DeadZoneHeight;

		// Token: 0x040001A2 RID: 418
		[Tooltip("The camera will not move along its z-axis if the Follow target is within this distance of the specified camera distance")]
		[FormerlySerializedAs("m_DistanceDeadZoneSize")]
		public float m_DeadZoneDepth;

		// Token: 0x040001A3 RID: 419
		[Space]
		[Tooltip("If checked, then then soft zone will be unlimited in size.")]
		public bool m_UnlimitedSoftZone;

		// Token: 0x040001A4 RID: 420
		[Range(0f, 2f)]
		[Tooltip("When target is within this region, camera will gradually move horizontally to re-align towards the desired position, depending on the damping speed.")]
		public float m_SoftZoneWidth = 0.8f;

		// Token: 0x040001A5 RID: 421
		[Range(0f, 2f)]
		[Tooltip("When target is within this region, camera will gradually move vertically to re-align towards the desired position, depending on the damping speed.")]
		public float m_SoftZoneHeight = 0.8f;

		// Token: 0x040001A6 RID: 422
		[Range(-0.5f, 0.5f)]
		[Tooltip("A non-zero bias will move the target position horizontally away from the center of the soft zone.")]
		public float m_BiasX;

		// Token: 0x040001A7 RID: 423
		[Range(-0.5f, 0.5f)]
		[Tooltip("A non-zero bias will move the target position vertically away from the center of the soft zone.")]
		public float m_BiasY;

		// Token: 0x040001A8 RID: 424
		[Tooltip("Force target to center of screen when this camera activates.  If false, will clamp target to the edges of the dead zone")]
		public bool m_CenterOnActivate = true;

		// Token: 0x040001A9 RID: 425
		[Space]
		[Tooltip("What screen dimensions to consider when framing.  Can be Horizontal, Vertical, or both")]
		[FormerlySerializedAs("m_FramingMode")]
		public CinemachineFramingTransposer.FramingMode m_GroupFramingMode = CinemachineFramingTransposer.FramingMode.HorizontalAndVertical;

		// Token: 0x040001AA RID: 426
		[Tooltip("How to adjust the camera to get the desired framing.  You can zoom, dolly in/out, or do both.")]
		public CinemachineFramingTransposer.AdjustmentMode m_AdjustmentMode;

		// Token: 0x040001AB RID: 427
		[Tooltip("The bounding box of the targets should occupy this amount of the screen space.  1 means fill the whole screen.  0.5 means fill half the screen, etc.")]
		public float m_GroupFramingSize = 0.8f;

		// Token: 0x040001AC RID: 428
		[Tooltip("The maximum distance toward the target that this behaviour is allowed to move the camera.")]
		public float m_MaxDollyIn = 5000f;

		// Token: 0x040001AD RID: 429
		[Tooltip("The maximum distance away the target that this behaviour is allowed to move the camera.")]
		public float m_MaxDollyOut = 5000f;

		// Token: 0x040001AE RID: 430
		[Tooltip("Set this to limit how close to the target the camera can get.")]
		public float m_MinimumDistance = 1f;

		// Token: 0x040001AF RID: 431
		[Tooltip("Set this to limit how far from the target the camera can get.")]
		public float m_MaximumDistance = 5000f;

		// Token: 0x040001B0 RID: 432
		[Range(1f, 179f)]
		[Tooltip("If adjusting FOV, will not set the FOV lower than this.")]
		public float m_MinimumFOV = 3f;

		// Token: 0x040001B1 RID: 433
		[Range(1f, 179f)]
		[Tooltip("If adjusting FOV, will not set the FOV higher than this.")]
		public float m_MaximumFOV = 60f;

		// Token: 0x040001B2 RID: 434
		[Tooltip("If adjusting Orthographic Size, will not set it lower than this.")]
		public float m_MinimumOrthoSize = 1f;

		// Token: 0x040001B3 RID: 435
		[Tooltip("If adjusting Orthographic Size, will not set it higher than this.")]
		public float m_MaximumOrthoSize = 5000f;

		// Token: 0x040001B4 RID: 436
		private const float kMinimumCameraDistance = 0.01f;

		// Token: 0x040001B5 RID: 437
		private const float kMinimumGroupSize = 0.01f;

		// Token: 0x040001B6 RID: 438
		private Vector3 m_PreviousCameraPosition = Vector3.zero;

		// Token: 0x040001B7 RID: 439
		internal PositionPredictor m_Predictor = new PositionPredictor();

		// Token: 0x040001B9 RID: 441
		private bool m_InheritingPosition;

		// Token: 0x040001BA RID: 442
		private float m_prevFOV;

		// Token: 0x040001BB RID: 443
		private Quaternion m_prevRotation;

		// Token: 0x0200004C RID: 76
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum FramingMode
		{
			// Token: 0x040001BF RID: 447
			Horizontal,
			// Token: 0x040001C0 RID: 448
			Vertical,
			// Token: 0x040001C1 RID: 449
			HorizontalAndVertical,
			// Token: 0x040001C2 RID: 450
			None
		}

		// Token: 0x0200004D RID: 77
		public enum AdjustmentMode
		{
			// Token: 0x040001C4 RID: 452
			ZoomOnly,
			// Token: 0x040001C5 RID: 453
			DollyOnly,
			// Token: 0x040001C6 RID: 454
			DollyThenZoom
		}
	}
}
