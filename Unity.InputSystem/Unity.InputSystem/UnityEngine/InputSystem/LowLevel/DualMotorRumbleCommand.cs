using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200018F RID: 399
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct DualMotorRumbleCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0004DD50 File Offset: 0x0004BF50
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'M', 'B', 'L');
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0004DD5F File Offset: 0x0004BF5F
		public FourCC typeStatic
		{
			get
			{
				return DualMotorRumbleCommand.Type;
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0004DD68 File Offset: 0x0004BF68
		public static DualMotorRumbleCommand Create(float lowFrequency, float highFrequency)
		{
			return new DualMotorRumbleCommand
			{
				baseCommand = new InputDeviceCommand(DualMotorRumbleCommand.Type, 16),
				lowFrequencyMotorSpeed = lowFrequency,
				highFrequencyMotorSpeed = highFrequency
			};
		}

		// Token: 0x04000985 RID: 2437
		internal const int kSize = 16;

		// Token: 0x04000986 RID: 2438
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000987 RID: 2439
		[FieldOffset(8)]
		public float lowFrequencyMotorSpeed;

		// Token: 0x04000988 RID: 2440
		[FieldOffset(12)]
		public float highFrequencyMotorSpeed;
	}
}
