using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x0200000F RID: 15
	public interface ICameraOverrideStack
	{
		// Token: 0x06000040 RID: 64
		int SetCameraOverride(int overrideId, ICinemachineCamera camA, ICinemachineCamera camB, float weightB, float deltaTime);

		// Token: 0x06000041 RID: 65
		void ReleaseCameraOverride(int overrideId);

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66
		Vector3 DefaultWorldUp { get; }
	}
}
