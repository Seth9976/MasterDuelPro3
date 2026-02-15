using System;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x02000104 RID: 260
	internal static class XInputSupport
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x0003FB00 File Offset: 0x0003DD00
		public static void Initialize()
		{
			InputSystem.RegisterLayout<XInputController>(null, null);
			InputSystem.RegisterLayout<XInputControllerWindows>(null, new InputDeviceMatcher?(default(InputDeviceMatcher).WithInterface("XInput", true)));
		}
	}
}
