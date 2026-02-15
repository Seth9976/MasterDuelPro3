using System;

namespace UnityEngine.InputSystem.Haptics
{
	// Token: 0x0200016F RID: 367
	public interface IDualMotorRumble : IHaptics
	{
		// Token: 0x06000F63 RID: 3939
		void SetMotorSpeeds(float lowFrequency, float highFrequency);
	}
}
