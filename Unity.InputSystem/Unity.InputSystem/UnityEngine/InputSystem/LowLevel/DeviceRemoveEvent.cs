using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001AC RID: 428
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceRemoveEvent : IInputEventTypeInfo
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0004E5C0 File Offset: 0x0004C7C0
		public FourCC typeStatic
		{
			get
			{
				return 1146242381;
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004E5CC File Offset: 0x0004C7CC
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceRemoveEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004E5E4 File Offset: 0x0004C7E4
		public static DeviceRemoveEvent Create(int deviceId, double time = -1.0)
		{
			return new DeviceRemoveEvent
			{
				baseEvent = new InputEvent(1146242381, 20, deviceId, time)
			};
		}

		// Token: 0x040009DF RID: 2527
		public const int Type = 1146242381;

		// Token: 0x040009E0 RID: 2528
		[FieldOffset(0)]
		public InputEvent baseEvent;
	}
}
