using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x02000053 RID: 83
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineOrbitalTransposer : CinemachineTransposer
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		protected override void OnValidate()
		{
			if (this.m_LegacyRadius != 3.4028235E+38f && this.m_LegacyHeightOffset != 3.4028235E+38f && this.m_LegacyHeadingBias != 3.4028235E+38f)
			{
				this.m_FollowOffset = new Vector3(0f, this.m_LegacyHeightOffset, -this.m_LegacyRadius);
				this.m_LegacyHeightOffset = (this.m_LegacyRadius = float.MaxValue);
				this.m_Heading.m_Bias = this.m_LegacyHeadingBias;
				this.m_XAxis.m_MaxSpeed = this.m_XAxis.m_MaxSpeed / 10f;
				this.m_XAxis.m_AccelTime = this.m_XAxis.m_AccelTime / 10f;
				this.m_XAxis.m_DecelTime = this.m_XAxis.m_DecelTime / 10f;
				this.m_LegacyHeadingBias = float.MaxValue;
				int heading = (int)this.m_Heading.m_Definition;
				if (this.m_RecenterToTargetHeading.LegacyUpgrade(ref heading, ref this.m_Heading.m_VelocityFilterStrength))
				{
					this.m_Heading.m_Definition = (CinemachineOrbitalTransposer.Heading.HeadingDefinition)heading;
				}
			}
			this.m_XAxis.Validate();
			this.m_RecenterToTargetHeading.Validate();
			base.OnValidate();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public float UpdateHeading(float deltaTime, Vector3 up, ref AxisState axis)
		{
			return this.UpdateHeading(deltaTime, up, ref axis, ref this.m_RecenterToTargetHeading, true);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000EF20 File Offset: 0x0000D120
		public float UpdateHeading(float deltaTime, Vector3 up, ref AxisState axis, ref AxisState.Recentering recentering, bool isLive)
		{
			if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
			{
				axis.m_MinValue = -180f;
				axis.m_MaxValue = 180f;
			}
			if (deltaTime < 0f || !base.VirtualCamera.PreviousStateIsValid || !isLive)
			{
				axis.Reset();
				recentering.CancelRecentering();
			}
			else if (axis.Update(deltaTime))
			{
				recentering.CancelRecentering();
			}
			if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
			{
				float value = axis.Value;
				axis.Value = 0f;
				return value;
			}
			float targetHeading = this.GetTargetHeading(axis.Value, base.GetReferenceOrientation(up));
			recentering.DoRecentering(ref axis, deltaTime, targetHeading);
			return axis.Value;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000EFC3 File Offset: 0x0000D1C3
		private void OnEnable()
		{
			this.m_PreviousTarget = null;
			this.m_LastTargetPosition = Vector3.zero;
			this.UpdateInputAxisProvider();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		public void UpdateInputAxisProvider()
		{
			this.m_XAxis.SetInputAxisProvider(0, null);
			if (!this.m_HeadingIsSlave && base.VirtualCamera != null)
			{
				AxisState.IInputAxisProvider provider = base.VirtualCamera.GetInputAxisProvider();
				if (provider != null)
				{
					this.m_XAxis.SetInputAxisProvider(0, provider);
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000F02C File Offset: 0x0000D22C
		public override void OnTargetObjectWarped(Transform target, Vector3 positionDelta)
		{
			base.OnTargetObjectWarped(target, positionDelta);
			if (target == base.FollowTarget)
			{
				this.m_LastTargetPosition += positionDelta;
				this.m_LastCameraPosition += positionDelta;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000F068 File Offset: 0x0000D268
		public override void ForceCameraPosition(Vector3 pos, Quaternion rot)
		{
			base.ForceCameraPosition(pos, rot);
			this.m_LastCameraPosition = pos;
			this.m_XAxis.Value = this.GetAxisClosestValue(pos, base.VirtualCamera.State.ReferenceUp);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000F09C File Offset: 0x0000D29C
		public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime, ref CinemachineVirtualCameraBase.TransitionParams transitionParams)
		{
			this.m_RecenterToTargetHeading.DoRecentering(ref this.m_XAxis, -1f, 0f);
			this.m_RecenterToTargetHeading.CancelRecentering();
			if (fromCam != null && this.m_BindingMode != CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp && transitionParams.m_InheritPosition && !CinemachineCore.Instance.IsLiveInBlend(base.VirtualCamera))
			{
				this.m_XAxis.Value = this.GetAxisClosestValue(fromCam.State.RawPosition, worldUp);
				return true;
			}
			return false;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000F118 File Offset: 0x0000D318
		public float GetAxisClosestValue(Vector3 cameraPos, Vector3 up)
		{
			Quaternion orient = base.GetReferenceOrientation(up);
			if (!(orient * Vector3.forward).ProjectOntoPlane(up).AlmostZero() && base.FollowTarget != null)
			{
				float heading = 0f;
				if (this.m_BindingMode != CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					heading += this.m_Heading.m_Bias;
				}
				orient *= Quaternion.AngleAxis(heading, up);
				Vector3 targetPos = base.FollowTargetPosition;
				Vector3 vector = (targetPos + orient * base.EffectiveOffset - targetPos).ProjectOntoPlane(up);
				Vector3 b = (cameraPos - targetPos).ProjectOntoPlane(up);
				return Vector3.SignedAngle(vector, b, up);
			}
			return this.m_LastHeading;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			base.InitPrevFrameStateInfo(ref curState, deltaTime);
			if (base.FollowTarget != this.m_PreviousTarget)
			{
				this.m_PreviousTarget = base.FollowTarget;
				this.m_TargetRigidBody = ((this.m_PreviousTarget == null) ? null : this.m_PreviousTarget.GetComponent<Rigidbody>());
				this.m_LastTargetPosition = ((this.m_PreviousTarget == null) ? Vector3.zero : this.m_PreviousTarget.position);
				this.mHeadingTracker = null;
			}
			this.m_LastHeading = this.HeadingUpdater(this, deltaTime, curState.ReferenceUp);
			float heading = this.m_LastHeading;
			if (this.IsValid)
			{
				if (this.m_BindingMode != CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
				{
					heading += this.m_Heading.m_Bias;
				}
				Quaternion quaternion = Quaternion.AngleAxis(heading, Vector3.up);
				Vector3 rawOffset = base.EffectiveOffset;
				Vector3 offset = quaternion * rawOffset;
				Vector3 pos;
				Quaternion orient;
				base.TrackTarget(deltaTime, curState.ReferenceUp, offset, out pos, out orient);
				offset = orient * offset;
				curState.ReferenceUp = orient * Vector3.up;
				Vector3 targetPosition = base.FollowTargetPosition;
				pos += base.GetOffsetForMinimumTargetDistance(pos, offset, curState.RawOrientation * Vector3.forward, curState.ReferenceUp, targetPosition);
				curState.RawPosition = pos + offset;
				if (deltaTime >= 0f && base.VirtualCamera.PreviousStateIsValid)
				{
					Vector3 lookAt = targetPosition;
					if (base.LookAtTarget != null)
					{
						lookAt = base.LookAtTargetPosition;
					}
					Vector3 dir0 = this.m_LastCameraPosition - lookAt;
					Vector3 dir = curState.RawPosition - lookAt;
					if (dir0.sqrMagnitude > 0.01f && dir.sqrMagnitude > 0.01f)
					{
						curState.PositionDampingBypass = UnityVectorExtensions.SafeFromToRotation(dir0, dir, curState.ReferenceUp).eulerAngles;
					}
				}
				this.m_LastTargetPosition = targetPosition;
				this.m_LastCameraPosition = curState.RawPosition;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public override Vector3 GetTargetCameraPosition(Vector3 worldUp)
		{
			if (!this.IsValid)
			{
				return Vector3.zero;
			}
			float heading = this.m_LastHeading;
			if (this.m_BindingMode != CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
			{
				heading += this.m_Heading.m_Bias;
			}
			Quaternion orient = Quaternion.AngleAxis(heading, Vector3.up);
			orient = base.GetReferenceOrientation(worldUp) * orient;
			return orient * base.EffectiveOffset + this.m_LastTargetPosition;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000771A File Offset: 0x0000591A
		public override bool RequiresUserInput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000F410 File Offset: 0x0000D610
		private float GetTargetHeading(float currentHeading, Quaternion targetOrientation)
		{
			if (this.m_BindingMode == CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp)
			{
				return 0f;
			}
			if (base.FollowTarget == null)
			{
				return currentHeading;
			}
			CinemachineOrbitalTransposer.Heading.HeadingDefinition headingDef = this.m_Heading.m_Definition;
			if (headingDef == CinemachineOrbitalTransposer.Heading.HeadingDefinition.Velocity && this.m_TargetRigidBody == null)
			{
				headingDef = CinemachineOrbitalTransposer.Heading.HeadingDefinition.PositionDelta;
			}
			Vector3 velocity = Vector3.zero;
			switch (headingDef)
			{
			case CinemachineOrbitalTransposer.Heading.HeadingDefinition.PositionDelta:
				velocity = base.FollowTargetPosition - this.m_LastTargetPosition;
				goto IL_0098;
			case CinemachineOrbitalTransposer.Heading.HeadingDefinition.Velocity:
				velocity = this.m_TargetRigidBody.linearVelocity;
				goto IL_0098;
			case CinemachineOrbitalTransposer.Heading.HeadingDefinition.TargetForward:
				velocity = base.FollowTargetRotation * Vector3.forward;
				goto IL_0098;
			}
			return 0f;
			IL_0098:
			Vector3 up = targetOrientation * Vector3.up;
			velocity = velocity.ProjectOntoPlane(up);
			if (headingDef != CinemachineOrbitalTransposer.Heading.HeadingDefinition.TargetForward)
			{
				int filterSize = this.m_Heading.m_VelocityFilterStrength * 5;
				if (this.mHeadingTracker == null || this.mHeadingTracker.FilterSize != filterSize)
				{
					this.mHeadingTracker = new HeadingTracker(filterSize);
				}
				this.mHeadingTracker.DecayHistory();
				if (!velocity.AlmostZero())
				{
					this.mHeadingTracker.Add(velocity);
				}
				velocity = this.mHeadingTracker.GetReliableHeading();
			}
			if (!velocity.AlmostZero())
			{
				return UnityVectorExtensions.SignedAngle(targetOrientation * Vector3.forward, velocity, up);
			}
			return currentHeading;
		}

		// Token: 0x040001E1 RID: 481
		[Space]
		[OrbitalTransposerHeadingProperty]
		[Tooltip("The definition of Forward.  Camera will follow behind.")]
		public CinemachineOrbitalTransposer.Heading m_Heading = new CinemachineOrbitalTransposer.Heading(CinemachineOrbitalTransposer.Heading.HeadingDefinition.TargetForward, 4, 0f);

		// Token: 0x040001E2 RID: 482
		[Tooltip("Automatic heading recentering.  The settings here defines how the camera will reposition itself in the absence of player input.")]
		public AxisState.Recentering m_RecenterToTargetHeading = new AxisState.Recentering(true, 1f, 2f);

		// Token: 0x040001E3 RID: 483
		[Tooltip("Heading Control.  The settings here control the behaviour of the camera in response to the player's input.")]
		[AxisStateProperty]
		public AxisState m_XAxis = new AxisState(-180f, 180f, true, false, 300f, 0.1f, 0.1f, "Mouse X", true);

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_Radius")]
		private float m_LegacyRadius = float.MaxValue;

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_HeightOffset")]
		private float m_LegacyHeightOffset = float.MaxValue;

		// Token: 0x040001E6 RID: 486
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_HeadingBias")]
		private float m_LegacyHeadingBias = float.MaxValue;

		// Token: 0x040001E7 RID: 487
		[HideInInspector]
		[NoSaveDuringPlay]
		public bool m_HeadingIsSlave;

		// Token: 0x040001E8 RID: 488
		internal CinemachineOrbitalTransposer.UpdateHeadingDelegate HeadingUpdater = (CinemachineOrbitalTransposer orbital, float deltaTime, Vector3 up) => orbital.UpdateHeading(deltaTime, up, ref orbital.m_XAxis, ref orbital.m_RecenterToTargetHeading, CinemachineCore.Instance.IsLive(orbital.VirtualCamera));

		// Token: 0x040001E9 RID: 489
		private Vector3 m_LastTargetPosition = Vector3.zero;

		// Token: 0x040001EA RID: 490
		private HeadingTracker mHeadingTracker;

		// Token: 0x040001EB RID: 491
		private Rigidbody m_TargetRigidBody;

		// Token: 0x040001EC RID: 492
		private Transform m_PreviousTarget;

		// Token: 0x040001ED RID: 493
		private Vector3 m_LastCameraPosition;

		// Token: 0x040001EE RID: 494
		private float m_LastHeading;

		// Token: 0x02000054 RID: 84
		[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
		[Serializable]
		public struct Heading
		{
			// Token: 0x06000216 RID: 534 RVA: 0x0000F5FC File Offset: 0x0000D7FC
			public Heading(CinemachineOrbitalTransposer.Heading.HeadingDefinition def, int filterStrength, float bias)
			{
				this.m_Definition = def;
				this.m_VelocityFilterStrength = filterStrength;
				this.m_Bias = bias;
			}

			// Token: 0x040001EF RID: 495
			[FormerlySerializedAs("m_HeadingDefinition")]
			[Tooltip("How 'forward' is defined.  The camera will be placed by default behind the target.  PositionDelta will consider 'forward' to be the direction in which the target is moving.")]
			public CinemachineOrbitalTransposer.Heading.HeadingDefinition m_Definition;

			// Token: 0x040001F0 RID: 496
			[Range(0f, 10f)]
			[Tooltip("Size of the velocity sampling window for target heading filter.  This filters out irregularities in the target's movement.  Used only if deriving heading from target's movement (PositionDelta or Velocity)")]
			public int m_VelocityFilterStrength;

			// Token: 0x040001F1 RID: 497
			[Range(-180f, 180f)]
			[FormerlySerializedAs("m_HeadingBias")]
			[Tooltip("Where the camera is placed when the X-axis value is zero.  This is a rotation in degrees around the Y axis.  When this value is 0, the camera will be placed behind the target.  Nonzero offsets will rotate the zero position around the target.")]
			public float m_Bias;

			// Token: 0x02000055 RID: 85
			[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
			public enum HeadingDefinition
			{
				// Token: 0x040001F3 RID: 499
				PositionDelta,
				// Token: 0x040001F4 RID: 500
				Velocity,
				// Token: 0x040001F5 RID: 501
				TargetForward,
				// Token: 0x040001F6 RID: 502
				WorldForward
			}
		}

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x06000218 RID: 536
		internal delegate float UpdateHeadingDelegate(CinemachineOrbitalTransposer orbital, float deltaTime, Vector3 up);
	}
}
