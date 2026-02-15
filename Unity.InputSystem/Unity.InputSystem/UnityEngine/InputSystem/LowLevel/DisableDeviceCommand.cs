using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct DisableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0004D5B2 File Offset: 0x0004B7B2
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'S', 'B', 'L');
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0004D5C1 File Offset: 0x0004B7C1
		public FourCC typeStatic
		{
			get
			{
				return DisableDeviceCommand.Type;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0004D5C8 File Offset: 0x0004B7C8
		public static DisableDeviceCommand Create()
		{
			return new DisableDeviceCommand
			{
				baseCommand = new InputDeviceCommand(DisableDeviceCommand.Type, 8)
			};
		}

		// Token: 0x0400091A RID: 2330
		internal const int kSize = 8;

		// Token: 0x0400091B RID: 2331
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
