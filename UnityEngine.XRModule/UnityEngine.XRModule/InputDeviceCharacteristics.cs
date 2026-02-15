using System;

namespace UnityEngine.XR
{
	// Token: 0x02000009 RID: 9
	[Flags]
	public enum InputDeviceCharacteristics : uint
	{
		// Token: 0x04000037 RID: 55
		None = 0U,
		// Token: 0x04000038 RID: 56
		HeadMounted = 1U,
		// Token: 0x04000039 RID: 57
		Camera = 2U,
		// Token: 0x0400003A RID: 58
		HeldInHand = 4U,
		// Token: 0x0400003B RID: 59
		HandTracking = 8U,
		// Token: 0x0400003C RID: 60
		EyeTracking = 16U,
		// Token: 0x0400003D RID: 61
		TrackedDevice = 32U,
		// Token: 0x0400003E RID: 62
		Controller = 64U,
		// Token: 0x0400003F RID: 63
		TrackingReference = 128U,
		// Token: 0x04000040 RID: 64
		Left = 256U,
		// Token: 0x04000041 RID: 65
		Right = 512U,
		// Token: 0x04000042 RID: 66
		Simulated6DOF = 1024U
	}
}
