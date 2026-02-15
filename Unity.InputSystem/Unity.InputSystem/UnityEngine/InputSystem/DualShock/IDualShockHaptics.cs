using System;
using UnityEngine.InputSystem.Haptics;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x02000161 RID: 353
	public interface IDualShockHaptics : IDualMotorRumble, IHaptics
	{
		// Token: 0x06000F49 RID: 3913
		void SetLightBarColor(Color color);
	}
}
