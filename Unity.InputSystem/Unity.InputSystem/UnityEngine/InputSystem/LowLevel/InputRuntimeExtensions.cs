using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001C8 RID: 456
	internal static class InputRuntimeExtensions
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x000513B4 File Offset: 0x0004F5B4
		public unsafe static long DeviceCommand<TCommand>(this IInputRuntime runtime, int deviceId, ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			if (runtime == null)
			{
				throw new ArgumentNullException("runtime");
			}
			return runtime.DeviceCommand(deviceId, (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command));
		}
	}
}
