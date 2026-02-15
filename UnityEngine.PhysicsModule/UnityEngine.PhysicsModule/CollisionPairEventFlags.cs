using System;

namespace UnityEngine
{
	// Token: 0x02000014 RID: 20
	internal enum CollisionPairEventFlags : ushort
	{
		// Token: 0x0400004A RID: 74
		SolveContacts = 1,
		// Token: 0x0400004B RID: 75
		ModifyContacts,
		// Token: 0x0400004C RID: 76
		NotifyTouchFound = 4,
		// Token: 0x0400004D RID: 77
		NotifyTouchPersists = 8,
		// Token: 0x0400004E RID: 78
		NotifyTouchLost = 16,
		// Token: 0x0400004F RID: 79
		NotifyTouchCCD = 32,
		// Token: 0x04000050 RID: 80
		NotifyThresholdForceFound = 64,
		// Token: 0x04000051 RID: 81
		NotifyThresholdForcePersists = 128,
		// Token: 0x04000052 RID: 82
		NotifyThresholdForceLost = 256,
		// Token: 0x04000053 RID: 83
		NotifyContactPoint = 512,
		// Token: 0x04000054 RID: 84
		DetectDiscreteContact = 1024,
		// Token: 0x04000055 RID: 85
		DetectCCDContact = 2048,
		// Token: 0x04000056 RID: 86
		PreSolverVelocity = 4096,
		// Token: 0x04000057 RID: 87
		PostSolverVelocity = 8192,
		// Token: 0x04000058 RID: 88
		ContactEventPose = 16384,
		// Token: 0x04000059 RID: 89
		NextFree = 32768,
		// Token: 0x0400005A RID: 90
		ContactDefault = 1025,
		// Token: 0x0400005B RID: 91
		TriggerDefault = 1044
	}
}
