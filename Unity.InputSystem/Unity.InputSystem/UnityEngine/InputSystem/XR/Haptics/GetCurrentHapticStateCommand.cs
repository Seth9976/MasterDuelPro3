using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x020000F7 RID: 247
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct GetCurrentHapticStateCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0003F7F0 File Offset: 0x0003D9F0
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'S', '0');
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0003F7FF File Offset: 0x0003D9FF
		public FourCC typeStatic
		{
			get
			{
				return GetCurrentHapticStateCommand.Type;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0003F806 File Offset: 0x0003DA06
		public HapticState currentState
		{
			get
			{
				return new HapticState(this.samplesQueued, this.samplesAvailable);
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0003F81C File Offset: 0x0003DA1C
		public static GetCurrentHapticStateCommand Create()
		{
			return new GetCurrentHapticStateCommand
			{
				baseCommand = new InputDeviceCommand(GetCurrentHapticStateCommand.Type, 16)
			};
		}

		// Token: 0x040005A2 RID: 1442
		private const int kSize = 16;

		// Token: 0x040005A3 RID: 1443
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x040005A4 RID: 1444
		[FieldOffset(8)]
		public uint samplesQueued;

		// Token: 0x040005A5 RID: 1445
		[FieldOffset(12)]
		public uint samplesAvailable;
	}
}
