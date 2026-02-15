using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200004A RID: 74
	public class FollowSkeletonUtilityRootRotation : MonoBehaviour
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000DE63 File Offset: 0x0000C063
		private void Start()
		{
			this.prevLocalEulerAngles = base.transform.localEulerAngles;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000DE78 File Offset: 0x0000C078
		private void FixedUpdate()
		{
			base.transform.rotation = this.reference.rotation;
			bool wasFlippedAroundY = Mathf.Abs(base.transform.localEulerAngles.y - this.prevLocalEulerAngles.y) > 100f;
			bool flag = Mathf.Abs(base.transform.localEulerAngles.x - this.prevLocalEulerAngles.x) > 100f;
			if (wasFlippedAroundY)
			{
				this.CompensatePositionToYRotation();
			}
			if (flag)
			{
				this.CompensatePositionToXRotation();
			}
			this.prevLocalEulerAngles = base.transform.localEulerAngles;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000DF10 File Offset: 0x0000C110
		private void CompensatePositionToYRotation()
		{
			Vector3 newPosition = this.reference.position + (this.reference.position - base.transform.position);
			newPosition.y = base.transform.position.y;
			base.transform.position = newPosition;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000DF6C File Offset: 0x0000C16C
		private void CompensatePositionToXRotation()
		{
			Vector3 newPosition = this.reference.position + (this.reference.position - base.transform.position);
			newPosition.x = base.transform.position.x;
			base.transform.position = newPosition;
		}

		// Token: 0x0400019E RID: 414
		private const float FLIP_ANGLE_THRESHOLD = 100f;

		// Token: 0x0400019F RID: 415
		public Transform reference;

		// Token: 0x040001A0 RID: 416
		private Vector3 prevLocalEulerAngles;
	}
}
