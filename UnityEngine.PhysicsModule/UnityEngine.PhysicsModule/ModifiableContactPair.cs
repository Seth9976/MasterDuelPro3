using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/Physics/PhysXContactModification.h")]
	[NativeHeader("Modules/Physics/PhysicsCollisionGeometry.h")]
	public struct ModifiableContactPair
	{
		// Token: 0x04000017 RID: 23
		private IntPtr actor;

		// Token: 0x04000018 RID: 24
		private IntPtr otherActor;

		// Token: 0x04000019 RID: 25
		private IntPtr shape;

		// Token: 0x0400001A RID: 26
		private IntPtr otherShape;

		// Token: 0x0400001B RID: 27
		public Quaternion rotation;

		// Token: 0x0400001C RID: 28
		public Vector3 position;

		// Token: 0x0400001D RID: 29
		public Quaternion otherRotation;

		// Token: 0x0400001E RID: 30
		public Vector3 otherPosition;

		// Token: 0x0400001F RID: 31
		private int numContacts;

		// Token: 0x04000020 RID: 32
		private IntPtr contacts;
	}
}
