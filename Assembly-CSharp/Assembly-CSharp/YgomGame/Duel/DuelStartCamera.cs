using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D98 RID: 3480
	public class DuelStartCamera : IMainCameraOperation
	{
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06006654 RID: 26196 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006655 RID: 26197 RVA: 0x0000216D File Offset: 0x0000036D
		public ChainedBezierMotion motion
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x00002739 File Offset: 0x00000939
		public DuelStartCamera(ChainedBezierMotion motion, Vector3 targetPosition, Quaternion targetRotation)
		{
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x0000216D File Offset: 0x0000036D
		public void LateUpdateOperation(MainCameraOrganizer mainCamera)
		{
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateOperation(MainCameraOrganizer mainCamera)
		{
		}

		// Token: 0x0400A065 RID: 41061
		private float time;
	}
}
