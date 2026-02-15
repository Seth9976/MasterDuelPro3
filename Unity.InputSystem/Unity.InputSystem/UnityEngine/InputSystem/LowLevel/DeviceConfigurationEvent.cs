using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001AB RID: 427
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceConfigurationEvent : IInputEventTypeInfo
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0004E56C File Offset: 0x0004C76C
		public FourCC typeStatic
		{
			get
			{
				return 1145259591;
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0004E578 File Offset: 0x0004C778
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceConfigurationEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004E590 File Offset: 0x0004C790
		public static DeviceConfigurationEvent Create(int deviceId, double time)
		{
			return new DeviceConfigurationEvent
			{
				baseEvent = new InputEvent(1145259591, 20, deviceId, time)
			};
		}

		// Token: 0x040009DD RID: 2525
		public const int Type = 1145259591;

		// Token: 0x040009DE RID: 2526
		[FieldOffset(0)]
		public InputEvent baseEvent;
	}
}
