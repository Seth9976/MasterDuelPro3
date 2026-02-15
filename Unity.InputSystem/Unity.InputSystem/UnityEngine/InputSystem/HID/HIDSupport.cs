using System;
using System.Linq;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x0200014B RID: 331
	public static class HIDSupport
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0004A5CD File Offset: 0x000487CD
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0004A5DC File Offset: 0x000487DC
		public static ReadOnlyArray<HIDSupport.HIDPageUsage> supportedHIDUsages
		{
			get
			{
				return HIDSupport.s_SupportedHIDUsages;
			}
			set
			{
				HIDSupport.s_SupportedHIDUsages = value.ToArray();
				InputSystem.s_Manager.AddAvailableDevicesThatAreNowRecognized();
				for (int i = 0; i < InputSystem.devices.Count; i++)
				{
					InputDevice device = InputSystem.devices[i];
					HID hid = device as HID;
					if (hid != null && !HIDSupport.s_SupportedHIDUsages.Contains(new HIDSupport.HIDPageUsage(hid.hidDescriptor.usagePage, hid.hidDescriptor.usage)))
					{
						InputSystem.RemoveLayout(device.layout);
						i--;
					}
				}
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0004A668 File Offset: 0x00048868
		internal static void Initialize()
		{
			HIDSupport.s_SupportedHIDUsages = new HIDSupport.HIDPageUsage[]
			{
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.Joystick),
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.Gamepad),
				new HIDSupport.HIDPageUsage(HID.GenericDesktop.MultiAxisController)
			};
			InputSystem.RegisterLayout<HID>(null, null);
			InputSystem.onFindLayoutForDevice += HID.OnFindLayoutForDevice;
		}

		// Token: 0x04000854 RID: 2132
		private static HIDSupport.HIDPageUsage[] s_SupportedHIDUsages;

		// Token: 0x0200014C RID: 332
		public struct HIDPageUsage
		{
			// Token: 0x06000E89 RID: 3721 RVA: 0x0004A6C7 File Offset: 0x000488C7
			public HIDPageUsage(HID.UsagePage page, int usage)
			{
				this.page = page;
				this.usage = usage;
			}

			// Token: 0x06000E8A RID: 3722 RVA: 0x0004A6D7 File Offset: 0x000488D7
			public HIDPageUsage(HID.GenericDesktop usage)
			{
				this.page = HID.UsagePage.GenericDesktop;
				this.usage = (int)usage;
			}

			// Token: 0x04000855 RID: 2133
			public HID.UsagePage page;

			// Token: 0x04000856 RID: 2134
			public int usage;
		}
	}
}
