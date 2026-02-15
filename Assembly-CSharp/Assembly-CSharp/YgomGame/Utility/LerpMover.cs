using System;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x0200082C RID: 2092
	public class LerpMover : MonoBehaviour
	{
		// Token: 0x06004090 RID: 16528 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x04003989 RID: 14729
		[SerializeField]
		private float lerpTime;

		// Token: 0x0400398A RID: 14730
		private Vector3 preParentPosition;

		// Token: 0x0400398B RID: 14731
		private Quaternion preParentRotation;

		// Token: 0x0400398C RID: 14732
		private Vector3 targetPosition;

		// Token: 0x0400398D RID: 14733
		private Quaternion targetRotation;

		// Token: 0x0400398E RID: 14734
		private Vector3 startPosition;

		// Token: 0x0400398F RID: 14735
		private Quaternion startRotation;

		// Token: 0x04003990 RID: 14736
		private Vector3 currentPosition;

		// Token: 0x04003991 RID: 14737
		private Quaternion currentRotation;

		// Token: 0x04003992 RID: 14738
		private float time;
	}
}
