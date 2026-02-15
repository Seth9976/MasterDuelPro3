using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000051 RID: 81
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineHardLockToTarget : CinemachineComponentBase
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000C336 File Offset: 0x0000A536
		public override bool IsValid
		{
			get
			{
				return base.enabled && base.FollowTarget != null;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000C34E File Offset: 0x0000A54E
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Body;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000ED04 File Offset: 0x0000CF04
		public override float GetMaxDampTime()
		{
			return this.m_Damping;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (!this.IsValid)
			{
				return;
			}
			Vector3 dampedPos = base.FollowTargetPosition;
			if (deltaTime >= 0f)
			{
				dampedPos = this.m_PreviousTargetPosition + base.VirtualCamera.DetachedFollowTargetDamp(dampedPos - this.m_PreviousTargetPosition, this.m_Damping, deltaTime);
			}
			this.m_PreviousTargetPosition = dampedPos;
			curState.RawPosition = dampedPos;
		}

		// Token: 0x040001DF RID: 479
		[Tooltip("How much time it takes for the position to catch up to the target's position")]
		public float m_Damping;

		// Token: 0x040001E0 RID: 480
		private Vector3 m_PreviousTargetPosition;
	}
}
