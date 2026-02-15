using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x020000F9 RID: 249
	[StructLayout(LayoutKind.Explicit, Size = 28)]
	public struct GetHapticCapabilitiesCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0003F8B2 File Offset: 0x0003DAB2
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'C', '0');
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0003F8C1 File Offset: 0x0003DAC1
		public FourCC typeStatic
		{
			get
			{
				return GetHapticCapabilitiesCommand.Type;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0003F8C8 File Offset: 0x0003DAC8
		public HapticCapabilities capabilities
		{
			get
			{
				return new HapticCapabilities(this.numChannels, this.supportsImpulse, this.supportsBuffer, this.frequencyHz, this.maxBufferSize, this.optimalBufferSize);
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0003F8F4 File Offset: 0x0003DAF4
		public static GetHapticCapabilitiesCommand Create()
		{
			return new GetHapticCapabilitiesCommand
			{
				baseCommand = new InputDeviceCommand(GetHapticCapabilitiesCommand.Type, 28)
			};
		}

		// Token: 0x040005AC RID: 1452
		private const int kSize = 28;

		// Token: 0x040005AD RID: 1453
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x040005AE RID: 1454
		[FieldOffset(8)]
		public uint numChannels;

		// Token: 0x040005AF RID: 1455
		[FieldOffset(12)]
		public bool supportsImpulse;

		// Token: 0x040005B0 RID: 1456
		[FieldOffset(13)]
		public bool supportsBuffer;

		// Token: 0x040005B1 RID: 1457
		[FieldOffset(16)]
		public uint frequencyHz;

		// Token: 0x040005B2 RID: 1458
		[FieldOffset(20)]
		public uint maxBufferSize;

		// Token: 0x040005B3 RID: 1459
		[FieldOffset(24)]
		public uint optimalBufferSize;
	}
}
