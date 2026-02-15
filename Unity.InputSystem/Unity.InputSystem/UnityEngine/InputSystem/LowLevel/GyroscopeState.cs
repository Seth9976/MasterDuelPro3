using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200019E RID: 414
	internal struct GyroscopeState : IInputStateTypeInfo
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0004DF33 File Offset: 0x0004C133
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'Y', 'R', 'O');
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0004DF42 File Offset: 0x0004C142
		public FourCC format
		{
			get
			{
				return GyroscopeState.kFormat;
			}
		}

		// Token: 0x040009B0 RID: 2480
		[InputControl(displayName = "Angular Velocity", processors = "CompensateDirection", noisy = true)]
		public Vector3 angularVelocity;
	}
}
