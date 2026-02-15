using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200018B RID: 395
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	public struct SetSamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0004DC21 File Offset: 0x0004BE21
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'S', 'P', 'L');
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0004DC30 File Offset: 0x0004BE30
		public FourCC typeStatic
		{
			get
			{
				return SetSamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0004DC38 File Offset: 0x0004BE38
		public static SetSamplingFrequencyCommand Create(float frequency)
		{
			return new SetSamplingFrequencyCommand
			{
				baseCommand = new InputDeviceCommand(SetSamplingFrequencyCommand.Type, 12),
				frequency = frequency
			};
		}

		// Token: 0x0400095D RID: 2397
		internal const int kSize = 12;

		// Token: 0x0400095E RID: 2398
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400095F RID: 2399
		[FieldOffset(8)]
		public float frequency;
	}
}
