using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000185 RID: 389
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	internal struct QuerySamplingFrequencyCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0004DAA0 File Offset: 0x0004BCA0
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'M', 'P', 'L');
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0004DAAF File Offset: 0x0004BCAF
		public FourCC typeStatic
		{
			get
			{
				return QuerySamplingFrequencyCommand.Type;
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0004DAB8 File Offset: 0x0004BCB8
		public static QuerySamplingFrequencyCommand Create()
		{
			return new QuerySamplingFrequencyCommand
			{
				baseCommand = new InputDeviceCommand(QuerySamplingFrequencyCommand.Type, 12)
			};
		}

		// Token: 0x0400094E RID: 2382
		internal const int kSize = 12;

		// Token: 0x0400094F RID: 2383
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000950 RID: 2384
		[FieldOffset(8)]
		public float frequency;
	}
}
