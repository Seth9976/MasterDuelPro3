using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000048 RID: 72
	[RequireComponent(typeof(Rigidbody))]
	public class FollowLocationRigidbody : MonoBehaviour
	{
		// Token: 0x06000290 RID: 656 RVA: 0x0000DD8B File Offset: 0x0000BF8B
		private void Awake()
		{
			this.ownRigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000DD99 File Offset: 0x0000BF99
		private void FixedUpdate()
		{
			this.ownRigidbody.rotation = this.reference.rotation;
			this.ownRigidbody.position = this.reference.position;
		}

		// Token: 0x04000199 RID: 409
		public Transform reference;

		// Token: 0x0400019A RID: 410
		private Rigidbody ownRigidbody;
	}
}
