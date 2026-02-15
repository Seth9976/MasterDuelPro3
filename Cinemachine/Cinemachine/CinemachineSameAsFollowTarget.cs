using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cinemachine
{
	// Token: 0x0200005A RID: 90
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineSameAsFollowTarget : CinemachineComponentBase
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C336 File Offset: 0x0000A536
		public override bool IsValid
		{
			get
			{
				return base.enabled && base.FollowTarget != null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000771A File Offset: 0x0000591A
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Aim;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000FB37 File Offset: 0x0000DD37
		public override float GetMaxDampTime()
		{
			return this.m_Damping;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000FB40 File Offset: 0x0000DD40
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (!this.IsValid)
			{
				return;
			}
			Quaternion dampedOrientation = base.FollowTargetRotation;
			if (deltaTime >= 0f)
			{
				float t = base.VirtualCamera.DetachedFollowTargetDamp(1f, this.m_Damping, deltaTime);
				dampedOrientation = Quaternion.Slerp(this.m_PreviousReferenceOrientation, base.FollowTargetRotation, t);
			}
			this.m_PreviousReferenceOrientation = dampedOrientation;
			curState.RawOrientation = dampedOrientation;
			curState.ReferenceUp = dampedOrientation * Vector3.up;
		}

		// Token: 0x04000204 RID: 516
		[Tooltip("How much time it takes for the aim to catch up to the target's rotation")]
		[FormerlySerializedAs("m_AngularDamping")]
		public float m_Damping;

		// Token: 0x04000205 RID: 517
		private Quaternion m_PreviousReferenceOrientation = Quaternion.identity;
	}
}
