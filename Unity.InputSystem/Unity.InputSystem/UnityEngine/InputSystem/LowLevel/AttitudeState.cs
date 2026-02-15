using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001A0 RID: 416
	internal struct AttitudeState : IInputStateTypeInfo
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0004DF5F File Offset: 0x0004C15F
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('A', 'T', 'T', 'D');
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0004DF6E File Offset: 0x0004C16E
		public FourCC format
		{
			get
			{
				return AttitudeState.kFormat;
			}
		}

		// Token: 0x040009B2 RID: 2482
		[InputControl(displayName = "Attitude", processors = "CompensateRotation", noisy = true)]
		public Quaternion attitude;
	}
}
