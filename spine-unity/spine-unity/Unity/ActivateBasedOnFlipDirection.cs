using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000047 RID: 71
	public class ActivateBasedOnFlipDirection : MonoBehaviour
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		private void Start()
		{
			this.jointsNormalX = this.activeOnNormalX.GetComponentsInChildren<HingeJoint2D>();
			this.jointsFlippedX = this.activeOnFlippedX.GetComponentsInChildren<HingeJoint2D>();
			ISkeletonComponent skeletonComponent2;
			if (!(this.skeletonRenderer != null))
			{
				ISkeletonComponent skeletonComponent = this.skeletonGraphic;
				skeletonComponent2 = skeletonComponent;
			}
			else
			{
				ISkeletonComponent skeletonComponent = this.skeletonRenderer;
				skeletonComponent2 = skeletonComponent;
			}
			this.skeletonComponent = skeletonComponent2;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000DC40 File Offset: 0x0000BE40
		private void FixedUpdate()
		{
			bool isFlippedX = this.skeletonComponent.Skeleton.ScaleX < 0f;
			if (isFlippedX != this.wasFlippedXBefore)
			{
				this.HandleFlip(isFlippedX);
			}
			this.wasFlippedXBefore = isFlippedX;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		private void HandleFlip(bool isFlippedX)
		{
			GameObject gameObjectToActivate = (isFlippedX ? this.activeOnFlippedX : this.activeOnNormalX);
			GameObject gameObjectToDeactivate = (isFlippedX ? this.activeOnNormalX : this.activeOnFlippedX);
			gameObjectToActivate.SetActive(true);
			gameObjectToDeactivate.SetActive(false);
			this.ResetJointPositions(isFlippedX ? this.jointsFlippedX : this.jointsNormalX);
			this.ResetJointPositions(isFlippedX ? this.jointsNormalX : this.jointsFlippedX);
			this.CompensateMovementAfterFlipX(gameObjectToActivate.transform, gameObjectToDeactivate.transform);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		private void ResetJointPositions(HingeJoint2D[] joints)
		{
			foreach (HingeJoint2D joint in joints)
			{
				Transform parent = joint.connectedBody.transform;
				joint.transform.position = parent.TransformPoint(joint.connectedAnchor);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000DD44 File Offset: 0x0000BF44
		private void CompensateMovementAfterFlipX(Transform toActivate, Transform toDeactivate)
		{
			Transform targetLocation = toDeactivate.GetChild(0);
			Transform currentLocation = toActivate.GetChild(0);
			toActivate.position += targetLocation.position - currentLocation.position;
		}

		// Token: 0x04000191 RID: 401
		public SkeletonRenderer skeletonRenderer;

		// Token: 0x04000192 RID: 402
		public SkeletonGraphic skeletonGraphic;

		// Token: 0x04000193 RID: 403
		public GameObject activeOnNormalX;

		// Token: 0x04000194 RID: 404
		public GameObject activeOnFlippedX;

		// Token: 0x04000195 RID: 405
		private HingeJoint2D[] jointsNormalX;

		// Token: 0x04000196 RID: 406
		private HingeJoint2D[] jointsFlippedX;

		// Token: 0x04000197 RID: 407
		private ISkeletonComponent skeletonComponent;

		// Token: 0x04000198 RID: 408
		private bool wasFlippedXBefore;
	}
}
