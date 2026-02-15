using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x020000FC RID: 252
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct SendHapticImpulseCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0003F9CF File Offset: 0x0003DBCF
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'I', '0');
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0003F9DE File Offset: 0x0003DBDE
		public FourCC typeStatic
		{
			get
			{
				return SendHapticImpulseCommand.Type;
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		public static SendHapticImpulseCommand Create(int motorChannel, float motorAmplitude, float motorDuration)
		{
			return new SendHapticImpulseCommand
			{
				baseCommand = new InputDeviceCommand(SendHapticImpulseCommand.Type, 20),
				channel = motorChannel,
				amplitude = motorAmplitude,
				duration = motorDuration
			};
		}

		// Token: 0x040005BB RID: 1467
		private const int kSize = 20;

		// Token: 0x040005BC RID: 1468
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x040005BD RID: 1469
		[FieldOffset(8)]
		private int channel;

		// Token: 0x040005BE RID: 1470
		[FieldOffset(12)]
		private float amplitude;

		// Token: 0x040005BF RID: 1471
		[FieldOffset(16)]
		private float duration;
	}
}
