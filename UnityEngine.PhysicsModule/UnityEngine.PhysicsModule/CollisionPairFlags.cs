using System;

namespace UnityEngine
{
	// Token: 0x02000013 RID: 19
	internal enum CollisionPairFlags : ushort
	{
		// Token: 0x04000043 RID: 67
		RemovedShape = 1,
		// Token: 0x04000044 RID: 68
		RemovedOtherShape,
		// Token: 0x04000045 RID: 69
		ActorPairHasFirstTouch = 4,
		// Token: 0x04000046 RID: 70
		ActorPairLostTouch = 8,
		// Token: 0x04000047 RID: 71
		InternalHasImpulses = 16,
		// Token: 0x04000048 RID: 72
		InternalContactsAreFlipped = 32
	}
}
