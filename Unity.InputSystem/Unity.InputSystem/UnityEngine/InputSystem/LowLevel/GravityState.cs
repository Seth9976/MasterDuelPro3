using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200019F RID: 415
	internal struct GravityState : IInputStateTypeInfo
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0004DF49 File Offset: 0x0004C149
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('G', 'R', 'V', ' ');
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0004DF58 File Offset: 0x0004C158
		public FourCC format
		{
			get
			{
				return GravityState.kFormat;
			}
		}

		// Token: 0x040009B1 RID: 2481
		[InputControl(displayName = "Gravity", processors = "CompensateDirection", noisy = true)]
		public Vector3 gravity;
	}
}
