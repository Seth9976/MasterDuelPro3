using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000172 RID: 370
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct EnableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0004D5F0 File Offset: 0x0004B7F0
		public static FourCC Type
		{
			get
			{
				return new FourCC('E', 'N', 'B', 'L');
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0004D5FF File Offset: 0x0004B7FF
		public FourCC typeStatic
		{
			get
			{
				return EnableDeviceCommand.Type;
			}
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0004D608 File Offset: 0x0004B808
		public static EnableDeviceCommand Create()
		{
			return new EnableDeviceCommand
			{
				baseCommand = new InputDeviceCommand(EnableDeviceCommand.Type, 8)
			};
		}

		// Token: 0x0400091C RID: 2332
		internal const int kSize = 8;

		// Token: 0x0400091D RID: 2333
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
