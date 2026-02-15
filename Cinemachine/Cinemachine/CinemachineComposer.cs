using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000049 RID: 73
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineComposer : CinemachineComponentBase
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000C9AA File Offset: 0x0000ABAA
		public override bool IsValid
		{
			get
			{
				return base.enabled && base.LookAtTarget != null;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000771A File Offset: 0x0000591A
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Aim;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000C9C2 File Offset: 0x0000ABC2
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000C9CA File Offset: 0x0000ABCA
		public Vector3 TrackedPoint { get; private set; }

		// Token: 0x060001CD RID: 461 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		protected virtual Vector3 GetLookAtPointAndSetTrackedPoint(Vector3 lookAt, Vector3 up, float deltaTime)
		{
			Vector3 pos = lookAt;
			if (base.LookAtTarget != null)
			{
				pos += base.LookAtTargetRotation * this.m_TrackedObjectOffset;
			}
			if (this.m_LookaheadTime < 0.0001f)
			{
				this.TrackedPoint = pos;
			}
			else
			{
				bool resetLookahead = base.VirtualCamera.LookAtTargetChanged || !base.VirtualCamera.PreviousStateIsValid;
				this.m_Predictor.Smoothing = this.m_LookaheadSmoothing;
				this.m_Predictor.AddPosition(pos, resetLookahead ? (-1f) : deltaTime, this.m_LookaheadTime);
				Vector3 delta = this.m_Predictor.PredictPositionDelta(this.m_LookaheadTime);
				if (this.m_LookaheadIgnoreY)
				{
					delta = delta.ProjectOntoPlane(up);
				}
				this.TrackedPoint = pos + delta;
			}
			return pos;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			base.OnTargetObjectWarped(target, positionDelta);
			if (target == base.LookAtTarget)
			{
				this.m_CameraPosPrevFrame += positionDelta;
				this.m_LookAtPrevFrame += positionDelta;
				this.m_Predictor.ApplyTransformDelta(positionDelta);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000CAEF File Offset: 0x0000ACEF
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			base.ForceCameraPosition(pos, rot);
			this.m_CameraPosPrevFrame = pos;
			this.m_CameraOrientationPrevFrame = rot;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CB07 File Offset: 0x0000AD07
		public override float GetMaxDampTime()
		{
			return Mathf.Max(this.m_HorizontalDamping, this.m_VerticalDamping);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CB1A File Offset: 0x0000AD1A
		public override void PrePipelineMutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (this.IsValid && curState.HasLookAt)
			{
				curState.ReferenceLookAt = this.GetLookAtPointAndSetTrackedPoint(curState.ReferenceLookAt, curState.ReferenceUp, deltaTime);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (!this.IsValid || !curState.HasLookAt)
			{
				return;
			}
			if (!(this.TrackedPoint - curState.ReferenceLookAt).AlmostZero())
			{
				Vector3 mid = Vector3.Lerp(curState.CorrectedPosition, curState.ReferenceLookAt, 0.5f);
				Vector3 vector = curState.ReferenceLookAt - mid;
				Vector3 toTracked = this.TrackedPoint - mid;
				if (Vector3.Dot(vector, toTracked) < 0f)
				{
					float t = Vector3.Distance(curState.ReferenceLookAt, mid) / Vector3.Distance(curState.ReferenceLookAt, this.TrackedPoint);
					this.TrackedPoint = Vector3.Lerp(curState.ReferenceLookAt, this.TrackedPoint, t);
				}
			}
			float targetDistance = (this.TrackedPoint - curState.CorrectedPosition).magnitude;
			if (targetDistance < 0.0001f)
			{
				if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
				{
					curState.RawOrientation = this.m_CameraOrientationPrevFrame;
				}
				return;
			}
			this.mCache.UpdateCache(curState.Lens, this.SoftGuideRect, this.HardGuideRect, targetDistance);
			Quaternion rigOrientation = curState.RawOrientation;
			if (deltaTime < 0f || !base.VirtualCamera.PreviousStateIsValid)
			{
				rigOrientation = Quaternion.LookRotation(rigOrientation * Vector3.forward, curState.ReferenceUp);
				Rect rect = this.mCache.mFovSoftGuideRect;
				if (this.m_CenterOnActivate)
				{
					rect = new Rect(rect.center, Vector2.zero);
				}
				this.RotateToScreenBounds(ref curState, rect, curState.ReferenceLookAt, ref rigOrientation, this.mCache.mFov, this.mCache.mFovH, -1f);
			}
			else
			{
				Vector3 dir = this.m_LookAtPrevFrame - this.m_CameraPosPrevFrame;
				if (dir.AlmostZero())
				{
					rigOrientation = Quaternion.LookRotation(this.m_CameraOrientationPrevFrame * Vector3.forward, curState.ReferenceUp);
				}
				else
				{
					dir = Quaternion.Euler(curState.PositionDampingBypass) * dir;
					rigOrientation = Quaternion.LookRotation(dir, curState.ReferenceUp);
					rigOrientation = rigOrientation.ApplyCameraRotation(-this.m_ScreenOffsetPrevFrame, curState.ReferenceUp);
				}
				this.RotateToScreenBounds(ref curState, this.mCache.mFovSoftGuideRect, this.TrackedPoint, ref rigOrientation, this.mCache.mFov, this.mCache.mFovH, deltaTime);
				if (deltaTime < 0f || base.VirtualCamera.LookAtTargetAttachment > 0.9999f)
				{
					this.RotateToScreenBounds(ref curState, this.mCache.mFovHardGuideRect, curState.ReferenceLookAt, ref rigOrientation, this.mCache.mFov, this.mCache.mFovH, -1f);
				}
			}
			this.m_CameraPosPrevFrame = curState.CorrectedPosition;
			this.m_LookAtPrevFrame = this.TrackedPoint;
			this.m_CameraOrientationPrevFrame = rigOrientation.Normalized();
			this.m_ScreenOffsetPrevFrame = this.m_CameraOrientationPrevFrame.GetCameraRotationToTarget(this.m_LookAtPrevFrame - curState.CorrectedPosition, curState.ReferenceUp);
			curState.RawOrientation = this.m_CameraOrientationPrevFrame;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000CE2F File Offset: 0x0000B02F
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000CE68 File Offset: 0x0000B068
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000CF30 File Offset: 0x0000B130
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
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

		// Token: 0x060001D7 RID: 471 RVA: 0x0000D02C File Offset: 0x0000B22C
		private void RotateToScreenBounds(ref CameraState state, Rect screenRect, Vector3 trackedPoint, ref Quaternion rigOrientation, float fov, float fovH, float deltaTime)
		{
			Vector3 targetDir = trackedPoint - state.CorrectedPosition;
			Vector2 rotToRect = rigOrientation.GetCameraRotationToTarget(targetDir, state.ReferenceUp);
			this.ClampVerticalBounds(ref screenRect, targetDir, state.ReferenceUp, fov);
			float min = (screenRect.yMin - 0.5f) * fov;
			float max = (screenRect.yMax - 0.5f) * fov;
			if (rotToRect.x < min)
			{
				rotToRect.x -= min;
			}
			else if (rotToRect.x > max)
			{
				rotToRect.x -= max;
			}
			else
			{
				rotToRect.x = 0f;
			}
			min = (screenRect.xMin - 0.5f) * fovH;
			max = (screenRect.xMax - 0.5f) * fovH;
			if (rotToRect.y < min)
			{
				rotToRect.y -= min;
			}
			else if (rotToRect.y > max)
			{
				rotToRect.y -= max;
			}
			else
			{
				rotToRect.y = 0f;
			}
			if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
			{
				rotToRect.x = base.VirtualCamera.DetachedLookAtTargetDamp(rotToRect.x, this.m_VerticalDamping, deltaTime);
				rotToRect.y = base.VirtualCamera.DetachedLookAtTargetDamp(rotToRect.y, this.m_HorizontalDamping, deltaTime);
			}
			rigOrientation = rigOrientation.ApplyCameraRotation(rotToRect, state.ReferenceUp);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000D194 File Offset: 0x0000B394
		private bool ClampVerticalBounds(ref Rect r, Vector3 dir, Vector3 up, float fov)
		{
			float angle = UnityVectorExtensions.Angle(dir, up);
			float halfFov = fov / 2f + 1f;
			if (angle < halfFov)
			{
				float maxY = 1f - (halfFov - angle) / fov;
				if (r.yMax > maxY)
				{
					r.yMin = Mathf.Min(r.yMin, maxY);
					r.yMax = Mathf.Min(r.yMax, maxY);
					return true;
				}
			}
			if (angle > 180f - halfFov)
			{
				float minY = (angle - (180f - halfFov)) / fov;
				if (minY > r.yMin)
				{
					r.yMin = Mathf.Max(r.yMin, minY);
					r.yMax = Mathf.Max(r.yMax, minY);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000177 RID: 375
		[Tooltip("Target offset from the target object's center in target-local space. Use this to fine-tune the tracking target position when the desired area is not the tracked object's center.")]
		public Vector3 m_TrackedObjectOffset = Vector3.zero;

		// Token: 0x04000178 RID: 376
		[Space]
		[Tooltip("This setting will instruct the composer to adjust its target offset based on the motion of the target.  The composer will look at a point where it estimates the target will be this many seconds into the future.  Note that this setting is sensitive to noisy animation, and can amplify the noise, resulting in undesirable camera jitter.  If the camera jitters unacceptably when the target is in motion, turn down this setting, or animate the target more smoothly.")]
		[Range(0f, 1f)]
		public float m_LookaheadTime;

		// Token: 0x04000179 RID: 377
		[Tooltip("Controls the smoothness of the lookahead algorithm.  Larger values smooth out jittery predictions and also increase prediction lag")]
		[Range(0f, 30f)]
		public float m_LookaheadSmoothing;

		// Token: 0x0400017A RID: 378
		[Tooltip("If checked, movement along the Y axis will be ignored for lookahead calculations")]
		public bool m_LookaheadIgnoreY;

		// Token: 0x0400017B RID: 379
		[Space]
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to follow the target in the screen-horizontal direction. Small numbers are more responsive, rapidly orienting the camera to keep the target in the dead zone. Larger numbers give a more heavy slowly responding camera. Using different vertical and horizontal settings can yield a wide range of camera behaviors.")]
		public float m_HorizontalDamping = 0.5f;

		// Token: 0x0400017C RID: 380
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to follow the target in the screen-vertical direction. Small numbers are more responsive, rapidly orienting the camera to keep the target in the dead zone. Larger numbers give a more heavy slowly responding camera. Using different vertical and horizontal settings can yield a wide range of camera behaviors.")]
		public float m_VerticalDamping = 0.5f;

		// Token: 0x0400017D RID: 381
		[Space]
		[Range(-0.5f, 1.5f)]
		[Tooltip("Horizontal screen position for target. The camera will rotate to position the tracked object here.")]
		public float m_ScreenX = 0.5f;

		// Token: 0x0400017E RID: 382
		[Range(-0.5f, 1.5f)]
		[Tooltip("Vertical screen position for target, The camera will rotate to position the tracked object here.")]
		public float m_ScreenY = 0.5f;

		// Token: 0x0400017F RID: 383
		[Range(0f, 2f)]
		[Tooltip("Camera will not rotate horizontally if the target is within this range of the position.")]
		public float m_DeadZoneWidth;

		// Token: 0x04000180 RID: 384
		[Range(0f, 2f)]
		[Tooltip("Camera will not rotate vertically if the target is within this range of the position.")]
		public float m_DeadZoneHeight;

		// Token: 0x04000181 RID: 385
		[Range(0f, 2f)]
		[Tooltip("When target is within this region, camera will gradually rotate horizontally to re-align towards the desired position, depending on the damping speed.")]
		public float m_SoftZoneWidth = 0.8f;

		// Token: 0x04000182 RID: 386
		[Range(0f, 2f)]
		[Tooltip("When target is within this region, camera will gradually rotate vertically to re-align towards the desired position, depending on the damping speed.")]
		public float m_SoftZoneHeight = 0.8f;

		// Token: 0x04000183 RID: 387
		[Range(-0.5f, 0.5f)]
		[Tooltip("A non-zero bias will move the target position horizontally away from the center of the soft zone.")]
		public float m_BiasX;

		// Token: 0x04000184 RID: 388
		[Range(-0.5f, 0.5f)]
		[Tooltip("A non-zero bias will move the target position vertically away from the center of the soft zone.")]
		public float m_BiasY;

		// Token: 0x04000185 RID: 389
		[Tooltip("Force target to center of screen when this camera activates.  If false, will clamp target to the edges of the dead zone")]
		public bool m_CenterOnActivate = true;

		// Token: 0x04000187 RID: 391
		private Vector3 m_CameraPosPrevFrame = Vector3.zero;

		// Token: 0x04000188 RID: 392
		private Vector3 m_LookAtPrevFrame = Vector3.zero;

		// Token: 0x04000189 RID: 393
		private Vector2 m_ScreenOffsetPrevFrame = Vector2.zero;

		// Token: 0x0400018A RID: 394
		private Quaternion m_CameraOrientationPrevFrame = Quaternion.identity;

		// Token: 0x0400018B RID: 395
		internal PositionPredictor m_Predictor = new PositionPredictor();

		// Token: 0x0400018C RID: 396
		private CinemachineComposer.FovCache mCache;

		// Token: 0x0200004A RID: 74
		private struct FovCache
		{
			// Token: 0x060001DA RID: 474 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
			public void UpdateCache(LensSettings lens, Rect softGuide, Rect hardGuide, float targetDistance)
			{
				bool recalculate = this.mAspect != lens.Aspect || softGuide != this.mSoftGuideRect || hardGuide != this.mHardGuideRect;
				if (lens.Orthographic)
				{
					float orthoOverDistance = Mathf.Abs(lens.OrthographicSize / targetDistance);
					if (this.mOrthoSizeOverDistance == 0f || Mathf.Abs(orthoOverDistance - this.mOrthoSizeOverDistance) / this.mOrthoSizeOverDistance > this.mOrthoSizeOverDistance * 0.01f)
					{
						recalculate = true;
					}
					if (recalculate)
					{
						this.mFov = 114.59156f * Mathf.Atan(orthoOverDistance);
						this.mFovH = 114.59156f * Mathf.Atan(lens.Aspect * orthoOverDistance);
						this.mOrthoSizeOverDistance = orthoOverDistance;
					}
				}
				else
				{
					float verticalFOV = lens.FieldOfView;
					if (this.mFov != verticalFOV)
					{
						recalculate = true;
					}
					if (recalculate)
					{
						this.mFov = verticalFOV;
						double radHFOV = 2.0 * Math.Atan(Math.Tan((double)(this.mFov * 0.017453292f / 2f)) * (double)lens.Aspect);
						this.mFovH = (float)(57.295780181884766 * radHFOV);
						this.mOrthoSizeOverDistance = 0f;
					}
				}
				if (recalculate)
				{
					this.mFovSoftGuideRect = this.ScreenToFOV(softGuide, this.mFov, this.mFovH, lens.Aspect);
					this.mSoftGuideRect = softGuide;
					this.mFovHardGuideRect = this.ScreenToFOV(hardGuide, this.mFov, this.mFovH, lens.Aspect);
					this.mHardGuideRect = hardGuide;
					this.mAspect = lens.Aspect;
				}
			}

			// Token: 0x060001DB RID: 475 RVA: 0x0000D468 File Offset: 0x0000B668
			private Rect ScreenToFOV(Rect rScreen, float fov, float fovH, float aspect)
			{
				Rect r = new Rect(rScreen);
				Matrix4x4 persp = Matrix4x4.Perspective(fov, aspect, 0.0001f, 2f).inverse;
				Vector3 p = persp.MultiplyPoint(new Vector3(0f, r.yMin * 2f - 1f, 0.5f));
				p.z = -p.z;
				float angle = UnityVectorExtensions.SignedAngle(Vector3.forward, p, Vector3.left);
				r.yMin = (fov / 2f + angle) / fov;
				p = persp.MultiplyPoint(new Vector3(0f, r.yMax * 2f - 1f, 0.5f));
				p.z = -p.z;
				angle = UnityVectorExtensions.SignedAngle(Vector3.forward, p, Vector3.left);
				r.yMax = (fov / 2f + angle) / fov;
				p = persp.MultiplyPoint(new Vector3(r.xMin * 2f - 1f, 0f, 0.5f));
				p.z = -p.z;
				angle = UnityVectorExtensions.SignedAngle(Vector3.forward, p, Vector3.up);
				r.xMin = (fovH / 2f + angle) / fovH;
				p = persp.MultiplyPoint(new Vector3(r.xMax * 2f - 1f, 0f, 0.5f));
				p.z = -p.z;
				angle = UnityVectorExtensions.SignedAngle(Vector3.forward, p, Vector3.up);
				r.xMax = (fovH / 2f + angle) / fovH;
				return r;
			}

			// Token: 0x0400018D RID: 397
			public Rect mFovSoftGuideRect;

			// Token: 0x0400018E RID: 398
			public Rect mFovHardGuideRect;

			// Token: 0x0400018F RID: 399
			public float mFovH;

			// Token: 0x04000190 RID: 400
			public float mFov;

			// Token: 0x04000191 RID: 401
			private float mOrthoSizeOverDistance;

			// Token: 0x04000192 RID: 402
			private float mAspect;

			// Token: 0x04000193 RID: 403
			private Rect mSoftGuideRect;

			// Token: 0x04000194 RID: 404
			private Rect mHardGuideRect;
		}
	}
}
