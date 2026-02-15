using System;
using Cinemachine.Utility;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200004E RID: 78
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineGroupComposer : CinemachineComposer
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		private void OnValidate()
		{
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000E683 File Offset: 0x0000C883
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000E68B File Offset: 0x0000C88B
		public Bounds LastBounds { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000E694 File Offset: 0x0000C894
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000E69C File Offset: 0x0000C89C
		public Matrix4x4 LastBoundsMatrix { get; private set; }

		// Token: 0x060001FA RID: 506 RVA: 0x0000E6A5 File Offset: 0x0000C8A5
		public override float GetMaxDampTime()
		{
			return Mathf.Max(base.GetMaxDampTime(), this.m_FrameDamping);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			ICinemachineTargetGroup group = base.AbstractLookAtTargetGroup;
			if (group == null)
			{
				base.MutateCameraState(ref curState, deltaTime);
				return;
			}
			if (!this.IsValid || !curState.HasLookAt)
			{
				this.m_prevFramingDistance = 0f;
				this.m_prevFOV = 0f;
				return;
			}
			bool isOrthographic = curState.Lens.Orthographic;
			bool canMoveCamera = !isOrthographic && this.m_AdjustmentMode > CinemachineGroupComposer.AdjustmentMode.ZoomOnly;
			Vector3 up = curState.ReferenceUp;
			Vector3 cameraPos = curState.RawPosition;
			Vector3 groupCenter = group.Sphere.position;
			Vector3 fwd = groupCenter - cameraPos;
			float d = fwd.magnitude;
			if (d < 0.0001f)
			{
				return;
			}
			fwd /= d;
			this.LastBoundsMatrix = Matrix4x4.TRS(cameraPos, Quaternion.LookRotation(fwd, up), Vector3.one);
			Bounds b;
			if (isOrthographic)
			{
				b = group.GetViewSpaceBoundingBox(this.LastBoundsMatrix);
				groupCenter = this.LastBoundsMatrix.MultiplyPoint3x4(b.center);
				fwd = (groupCenter - cameraPos).normalized;
				this.LastBoundsMatrix = Matrix4x4.TRS(cameraPos, Quaternion.LookRotation(fwd, up), Vector3.one);
				b = group.GetViewSpaceBoundingBox(this.LastBoundsMatrix);
				this.LastBounds = b;
			}
			else
			{
				b = CinemachineGroupComposer.GetScreenSpaceGroupBoundingBox(group, this.LastBoundsMatrix, out fwd);
				this.LastBoundsMatrix = Matrix4x4.TRS(cameraPos, Quaternion.LookRotation(fwd, up), Vector3.one);
				this.LastBounds = b;
				groupCenter = cameraPos + fwd * b.center.z;
			}
			float boundsDepth = b.extents.z;
			float targetHeight = this.GetTargetHeight(b.size / this.m_GroupFramingSize);
			if (isOrthographic)
			{
				targetHeight = Mathf.Clamp(targetHeight / 2f, this.m_MinimumOrthoSize, this.m_MaximumOrthoSize);
				if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
				{
					targetHeight = this.m_prevFOV + base.VirtualCamera.DetachedLookAtTargetDamp(targetHeight - this.m_prevFOV, this.m_FrameDamping, deltaTime);
				}
				this.m_prevFOV = targetHeight;
				LensSettings lens = curState.Lens;
				lens.OrthographicSize = Mathf.Clamp(targetHeight, this.m_MinimumOrthoSize, this.m_MaximumOrthoSize);
				curState.Lens = lens;
			}
			else
			{
				float z = b.center.z;
				if (z > boundsDepth)
				{
					targetHeight = Mathf.Lerp(0f, targetHeight, (z - boundsDepth) / z);
				}
				if (canMoveCamera)
				{
					float targetDelta = Mathf.Clamp(boundsDepth + targetHeight / (2f * Mathf.Tan(curState.Lens.FieldOfView * 0.017453292f / 2f)), boundsDepth + this.m_MinimumDistance, boundsDepth + this.m_MaximumDistance) - Vector3.Distance(curState.RawPosition, groupCenter);
					targetDelta = Mathf.Clamp(targetDelta, -this.m_MaxDollyIn, this.m_MaxDollyOut);
					if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
					{
						float delta = targetDelta - this.m_prevFramingDistance;
						delta = base.VirtualCamera.DetachedLookAtTargetDamp(delta, this.m_FrameDamping, deltaTime);
						targetDelta = this.m_prevFramingDistance + delta;
					}
					this.m_prevFramingDistance = targetDelta;
					curState.PositionCorrection -= fwd * targetDelta;
					cameraPos -= fwd * targetDelta;
				}
				if (this.m_AdjustmentMode != CinemachineGroupComposer.AdjustmentMode.DollyOnly)
				{
					float nearBoundsDistance = (groupCenter - cameraPos).magnitude - boundsDepth;
					float targetFOV = 179f;
					if (nearBoundsDistance > 0.0001f)
					{
						targetFOV = 2f * Mathf.Atan(targetHeight / (2f * nearBoundsDistance)) * 57.29578f;
					}
					targetFOV = Mathf.Clamp(targetFOV, this.m_MinimumFOV, this.m_MaximumFOV);
					if (deltaTime >= 0f && this.m_prevFOV != 0f && base.VirtualCamera.PreviousStateIsValid)
					{
						targetFOV = this.m_prevFOV + base.VirtualCamera.DetachedLookAtTargetDamp(targetFOV - this.m_prevFOV, this.m_FrameDamping, deltaTime);
					}
					this.m_prevFOV = targetFOV;
					LensSettings lens2 = curState.Lens;
					lens2.FieldOfView = targetFOV;
					curState.Lens = lens2;
				}
			}
			curState.ReferenceLookAt = this.GetLookAtPointAndSetTrackedPoint(groupCenter, curState.ReferenceUp, deltaTime);
			base.MutateCameraState(ref curState, deltaTime);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		private float GetTargetHeight(Vector2 boundsSize)
		{
			CameraState cameraState;
			switch (this.m_FramingMode)
			{
			case CinemachineGroupComposer.FramingMode.Horizontal:
			{
				float num = Mathf.Max(0.0001f, boundsSize.x);
				cameraState = base.VcamState;
				return num / cameraState.Lens.Aspect;
			}
			case CinemachineGroupComposer.FramingMode.Vertical:
				return Mathf.Max(0.0001f, boundsSize.y);
			}
			float num2 = Mathf.Max(0.0001f, boundsSize.x);
			cameraState = base.VcamState;
			return Mathf.Max(num2 / cameraState.Lens.Aspect, Mathf.Max(0.0001f, boundsSize.y));
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000EB88 File Offset: 0x0000CD88
		private static Bounds GetScreenSpaceGroupBoundingBox(ICinemachineTargetGroup group, Matrix4x4 observer, out Vector3 newFwd)
		{
			Vector2 minAngles;
			Vector2 maxAngles;
			Vector2 zRange;
			group.GetViewSpaceAngularBounds(observer, out minAngles, out maxAngles, out zRange);
			Vector2 shift = (minAngles + maxAngles) / 2f;
			newFwd = Quaternion.identity.ApplyCameraRotation(new Vector2(-shift.x, shift.y), Vector3.up) * Vector3.forward;
			newFwd = observer.MultiplyVector(newFwd);
			float d = zRange.y + zRange.x;
			Vector2 angles = Vector2.Min(maxAngles - shift, new Vector2(89.5f, 89.5f)) * 0.017453292f;
			return new Bounds(new Vector3(0f, 0f, d / 2f), new Vector3(Mathf.Tan(angles.y) * d, Mathf.Tan(angles.x) * d, zRange.y - zRange.x));
		}

		// Token: 0x040001C7 RID: 455
		[Space]
		[Tooltip("The bounding box of the targets should occupy this amount of the screen space.  1 means fill the whole screen.  0.5 means fill half the screen, etc.")]
		public float m_GroupFramingSize = 0.8f;

		// Token: 0x040001C8 RID: 456
		[Tooltip("What screen dimensions to consider when framing.  Can be Horizontal, Vertical, or both")]
		public CinemachineGroupComposer.FramingMode m_FramingMode = CinemachineGroupComposer.FramingMode.HorizontalAndVertical;

		// Token: 0x040001C9 RID: 457
		[Range(0f, 20f)]
		[Tooltip("How aggressively the camera tries to frame the group. Small numbers are more responsive, rapidly adjusting the camera to keep the group in the frame.  Larger numbers give a more heavy slowly responding camera.")]
		public float m_FrameDamping = 2f;

		// Token: 0x040001CA RID: 458
		[Tooltip("How to adjust the camera to get the desired framing.  You can zoom, dolly in/out, or do both.")]
		public CinemachineGroupComposer.AdjustmentMode m_AdjustmentMode;

		// Token: 0x040001CB RID: 459
		[Tooltip("The maximum distance toward the target that this behaviour is allowed to move the camera.")]
		public float m_MaxDollyIn = 5000f;

		// Token: 0x040001CC RID: 460
		[Tooltip("The maximum distance away the target that this behaviour is allowed to move the camera.")]
		public float m_MaxDollyOut = 5000f;

		// Token: 0x040001CD RID: 461
		[Tooltip("Set this to limit how close to the target the camera can get.")]
		public float m_MinimumDistance = 1f;

		// Token: 0x040001CE RID: 462
		[Tooltip("Set this to limit how far from the target the camera can get.")]
		public float m_MaximumDistance = 5000f;

		// Token: 0x040001CF RID: 463
		[Range(1f, 179f)]
		[Tooltip("If adjusting FOV, will not set the FOV lower than this.")]
		public float m_MinimumFOV = 3f;

		// Token: 0x040001D0 RID: 464
		[Range(1f, 179f)]
		[Tooltip("If adjusting FOV, will not set the FOV higher than this.")]
		public float m_MaximumFOV = 60f;

		// Token: 0x040001D1 RID: 465
		[Tooltip("If adjusting Orthographic Size, will not set it lower than this.")]
		public float m_MinimumOrthoSize = 1f;

		// Token: 0x040001D2 RID: 466
		[Tooltip("If adjusting Orthographic Size, will not set it higher than this.")]
		public float m_MaximumOrthoSize = 5000f;

		// Token: 0x040001D3 RID: 467
		private float m_prevFramingDistance;

		// Token: 0x040001D4 RID: 468
		private float m_prevFOV;

		// Token: 0x0200004F RID: 79
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		public enum FramingMode
		{
			// Token: 0x040001D8 RID: 472
			Horizontal,
			// Token: 0x040001D9 RID: 473
			Vertical,
			// Token: 0x040001DA RID: 474
			HorizontalAndVertical
		}

		// Token: 0x02000050 RID: 80
		public enum AdjustmentMode
		{
			// Token: 0x040001DC RID: 476
			ZoomOnly,
			// Token: 0x040001DD RID: 477
			DollyOnly,
			// Token: 0x040001DE RID: 478
			DollyThenZoom
		}
	}
}
