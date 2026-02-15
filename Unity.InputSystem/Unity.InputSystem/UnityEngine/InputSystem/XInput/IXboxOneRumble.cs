using System;
using UnityEngine.InputSystem.Haptics;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x020000FD RID: 253
	public interface IXboxOneRumble : IDualMotorRumble, IHaptics
	{
		// Token: 0x06000C8F RID: 3215
		void SetMotorSpeeds(float lowFrequency, float highFrequency, float leftTrigger, float rightTrigger);
	}
}
