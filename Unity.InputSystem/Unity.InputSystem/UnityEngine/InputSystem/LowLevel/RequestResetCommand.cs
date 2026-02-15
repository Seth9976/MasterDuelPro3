using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000188 RID: 392
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestResetCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0004DB50 File Offset: 0x0004BD50
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'S', 'E', 'T');
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0004DB5F File Offset: 0x0004BD5F
		public FourCC typeStatic
		{
			get
			{
				return RequestResetCommand.Type;
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0004DB68 File Offset: 0x0004BD68
		public static RequestResetCommand Create()
		{
			return new RequestResetCommand
			{
				baseCommand = new InputDeviceCommand(RequestResetCommand.Type, 8)
			};
		}

		// Token: 0x04000956 RID: 2390
		internal const int kSize = 8;

		// Token: 0x04000957 RID: 2391
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
