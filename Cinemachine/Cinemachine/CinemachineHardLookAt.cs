using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000052 RID: 82
	[DocumentationSorting(DocumentationSortingAttribute.Level.UserRef)]
	[AddComponentMenu("")]
	[SaveDuringPlay]
	public class CinemachineHardLookAt : CinemachineComponentBase
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000C9AA File Offset: 0x0000ABAA
		public override bool IsValid
		{
			get
			{
				return base.enabled && base.LookAtTarget != null;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000771A File Offset: 0x0000591A
		public override CinemachineCore.Stage Stage
		{
			get
			{
				return CinemachineCore.Stage.Aim;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public override void MutateCameraState(ref CameraState curState, float deltaTime)
		{
			if (this.IsValid && curState.HasLookAt)
			{
				Vector3 dir = curState.ReferenceLookAt - curState.CorrectedPosition;
				if (dir.magnitude > 0.0001f)
				{
					if (Vector3.Cross(dir.normalized, curState.ReferenceUp).magnitude < 0.0001f)
					{
						curState.RawOrientation = Quaternion.FromToRotation(Vector3.forward, dir);
						return;
					}
					curState.RawOrientation = Quaternion.LookRotation(dir, curState.ReferenceUp);
				}
			}
		}
	}
}
