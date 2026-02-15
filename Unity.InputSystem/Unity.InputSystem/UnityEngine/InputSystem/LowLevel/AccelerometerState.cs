using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200019D RID: 413
	internal struct AccelerometerState : IInputStateTypeInfo
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0004DF1D File Offset: 0x0004C11D
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'C', 'C', 'L');
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0004DF2C File Offset: 0x0004C12C
		public FourCC format
		{
			get
			{
				return AccelerometerState.kFormat;
			}
		}

		// Token: 0x040009AF RID: 2479
		[InputControl(displayName = "Acceleration", processors = "CompensateDirection", noisy = true)]
		public Vector3 acceleration;
	}
}
