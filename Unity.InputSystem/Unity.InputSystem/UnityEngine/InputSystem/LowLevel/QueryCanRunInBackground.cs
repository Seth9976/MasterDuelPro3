using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200017A RID: 378
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryCanRunInBackground : IInputDeviceCommandInfo
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0004D74A File Offset: 0x0004B94A
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'R', 'I', 'B');
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0004D759 File Offset: 0x0004B959
		public FourCC typeStatic
		{
			get
			{
				return QueryCanRunInBackground.Type;
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0004D760 File Offset: 0x0004B960
		public static QueryCanRunInBackground Create()
		{
			return new QueryCanRunInBackground
			{
				baseCommand = new InputDeviceCommand(QueryCanRunInBackground.Type, 9),
				canRunInBackground = false
			};
		}

		// Token: 0x0400092D RID: 2349
		internal const int kSize = 9;

		// Token: 0x0400092E RID: 2350
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400092F RID: 2351
		[FieldOffset(8)]
		public bool canRunInBackground;
	}
}
