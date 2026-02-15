using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[UsedByNativeCode]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[NativeConditional("ENABLE_VR")]
	[StructLayout(LayoutKind.Sequential)]
	public class InputDevices
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000024F8 File Offset: 0x000006F8
		[RequiredByNativeCode]
		private static void InvokeConnectionEvent(ulong deviceId, ConnectionChangeType change)
		{
			switch (change)
			{
			case ConnectionChangeType.Connected:
			{
				bool flag = InputDevices.deviceConnected != null;
				if (flag)
				{
					InputDevices.deviceConnected(new InputDevice(deviceId));
				}
				break;
			}
			case ConnectionChangeType.Disconnected:
			{
				bool flag2 = InputDevices.deviceDisconnected != null;
				if (flag2)
				{
					InputDevices.deviceDisconnected(new InputDevice(deviceId));
				}
				break;
			}
			case ConnectionChangeType.ConfigChange:
			{
				bool flag3 = InputDevices.deviceConfigChanged != null;
				if (flag3)
				{
					InputDevices.deviceConfigChanged(new InputDevice(deviceId));
				}
				break;
			}
			}
		}

		// Token: 0x04000056 RID: 86
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<InputDevice> deviceConnected;

		// Token: 0x04000057 RID: 87
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<InputDevice> deviceDisconnected;

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<InputDevice> deviceConfigChanged;
	}
}
