using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x02000088 RID: 136
	public struct PathOptions : IPlugOptions
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public void Reset()
		{
			this.mode = PathMode.Ignore;
			this.orientType = OrientType.None;
			this.lockPositionAxis = (this.lockRotationAxis = AxisConstraint.None);
			this.isClosedPath = false;
			this.lookAtPosition = Vector3.zero;
			this.lookAtTransform = null;
			this.lookAhead = 0f;
			this.hasCustomForwardDirection = false;
			this.forward = Quaternion.identity;
			this.useLocalPosition = false;
			this.parent = null;
			this.isRigidbody = (this.isRigidbody2D = false);
			this.stableZRotation = false;
			this.startupRot = Quaternion.identity;
			this.startupZRot = 0f;
			this.addedExtraStartWp = (this.addedExtraEndWp = false);
		}

		// Token: 0x0400016C RID: 364
		public PathMode mode;

		// Token: 0x0400016D RID: 365
		public OrientType orientType;

		// Token: 0x0400016E RID: 366
		public AxisConstraint lockPositionAxis;

		// Token: 0x0400016F RID: 367
		public AxisConstraint lockRotationAxis;

		// Token: 0x04000170 RID: 368
		public bool isClosedPath;

		// Token: 0x04000171 RID: 369
		public Vector3 lookAtPosition;

		// Token: 0x04000172 RID: 370
		public Transform lookAtTransform;

		// Token: 0x04000173 RID: 371
		public float lookAhead;

		// Token: 0x04000174 RID: 372
		public bool hasCustomForwardDirection;

		// Token: 0x04000175 RID: 373
		public Quaternion forward;

		// Token: 0x04000176 RID: 374
		public bool useLocalPosition;

		// Token: 0x04000177 RID: 375
		public Transform parent;

		// Token: 0x04000178 RID: 376
		public bool isRigidbody;

		// Token: 0x04000179 RID: 377
		public bool isRigidbody2D;

		// Token: 0x0400017A RID: 378
		public bool stableZRotation;

		// Token: 0x0400017B RID: 379
		internal Quaternion startupRot;

		// Token: 0x0400017C RID: 380
		internal float startupZRot;

		// Token: 0x0400017D RID: 381
		internal bool addedExtraStartWp;

		// Token: 0x0400017E RID: 382
		internal bool addedExtraEndWp;
	}
}
