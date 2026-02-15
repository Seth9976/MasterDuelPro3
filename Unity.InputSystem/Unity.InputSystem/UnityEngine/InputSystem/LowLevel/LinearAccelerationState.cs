using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001A1 RID: 417
	internal struct LinearAccelerationState : IInputStateTypeInfo
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0004DF75 File Offset: 0x0004C175
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('L', 'A', 'A', 'C');
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0004DF84 File Offset: 0x0004C184
		public FourCC format
		{
			get
			{
				return LinearAccelerationState.kFormat;
			}
		}

		// Token: 0x040009B3 RID: 2483
		[InputControl(displayName = "Acceleration", processors = "CompensateDirection", noisy = true)]
		public Vector3 acceleration;
	}
}
