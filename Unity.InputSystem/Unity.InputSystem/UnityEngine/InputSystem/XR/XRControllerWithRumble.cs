using System;
using UnityEngine.InputSystem.XR.Haptics;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000E4 RID: 228
	public class XRControllerWithRumble : XRController
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x0003DF4C File Offset: 0x0003C14C
		public void SendImpulse(float amplitude, float duration)
		{
			SendHapticImpulseCommand command = SendHapticImpulseCommand.Create(0, amplitude, duration);
			base.ExecuteCommand<SendHapticImpulseCommand>(ref command);
		}
	}
}
