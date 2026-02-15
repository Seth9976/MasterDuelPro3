using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000049 RID: 73
	[RequireComponent(typeof(Rigidbody2D))]
	public class FollowLocationRigidbody2D : MonoBehaviour
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000DDC7 File Offset: 0x0000BFC7
		private void Awake()
		{
			this.ownRigidbody = base.GetComponent<Rigidbody2D>();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		private void FixedUpdate()
		{
			if (this.followFlippedX)
			{
				this.ownRigidbody.rotation = (-this.reference.rotation.eulerAngles.z + 270f) % 360f - 90f;
			}
			else
			{
				this.ownRigidbody.rotation = this.reference.rotation.eulerAngles.z;
			}
			this.ownRigidbody.position = this.reference.position;
		}

		// Token: 0x0400019B RID: 411
		public Transform reference;

		// Token: 0x0400019C RID: 412
		public bool followFlippedX;

		// Token: 0x0400019D RID: 413
		private Rigidbody2D ownRigidbody;
	}
}
