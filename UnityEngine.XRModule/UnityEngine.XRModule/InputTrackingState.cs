using System;

namespace UnityEngine.XR
{
	// Token: 0x0200000A RID: 10
	[Flags]
	public enum InputTrackingState : uint
	{
		// Token: 0x04000044 RID: 68
		None = 0U,
		// Token: 0x04000045 RID: 69
		Position = 1U,
		// Token: 0x04000046 RID: 70
		Rotation = 2U,
		// Token: 0x04000047 RID: 71
		Velocity = 4U,
		// Token: 0x04000048 RID: 72
		AngularVelocity = 8U,
		// Token: 0x04000049 RID: 73
		Acceleration = 16U,
		// Token: 0x0400004A RID: 74
		AngularAcceleration = 32U,
		// Token: 0x0400004B RID: 75
		All = 63U
	}
}
