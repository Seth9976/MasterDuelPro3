using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001AD RID: 429
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceResetEvent : IInputEventTypeInfo
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0004E614 File Offset: 0x0004C814
		public FourCC typeStatic
		{
			get
			{
				return 1146245972;
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0004E620 File Offset: 0x0004C820
		public static DeviceResetEvent Create(int deviceId, bool hardReset = false, double time = -1.0)
		{
			DeviceResetEvent inputEvent = new DeviceResetEvent
			{
				baseEvent = new InputEvent(1146245972, 20, deviceId, time)
			};
			inputEvent.hardReset = hardReset;
			return inputEvent;
		}

		// Token: 0x040009E1 RID: 2529
		public const int Type = 1146245972;

		// Token: 0x040009E2 RID: 2530
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040009E3 RID: 2531
		[FieldOffset(8)]
		public bool hardReset;
	}
}
