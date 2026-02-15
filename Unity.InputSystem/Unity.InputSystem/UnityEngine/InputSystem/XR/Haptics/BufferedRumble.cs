using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x020000F5 RID: 245
	public struct BufferedRumble
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0003F737 File Offset: 0x0003D937
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0003F73F File Offset: 0x0003D93F
		public HapticCapabilities capabilities { readonly get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0003F748 File Offset: 0x0003D948
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0003F750 File Offset: 0x0003D950
		private InputDevice device { readonly get; set; }

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003F75C File Offset: 0x0003D95C
		public BufferedRumble(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.device = device;
			GetHapticCapabilitiesCommand command = GetHapticCapabilitiesCommand.Create();
			device.ExecuteCommand<GetHapticCapabilitiesCommand>(ref command);
			this.capabilities = command.capabilities;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0003F79C File Offset: 0x0003D99C
		public void EnqueueRumble(byte[] samples)
		{
			SendBufferedHapticCommand command = SendBufferedHapticCommand.Create(samples);
			this.device.ExecuteCommand<SendBufferedHapticCommand>(ref command);
		}
	}
}
