using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000189 RID: 393
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestSyncCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0004DB90 File Offset: 0x0004BD90
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'Y', 'N', 'C');
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0004DB9F File Offset: 0x0004BD9F
		public FourCC typeStatic
		{
			get
			{
				return RequestSyncCommand.Type;
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0004DBA8 File Offset: 0x0004BDA8
		public static RequestSyncCommand Create()
		{
			return new RequestSyncCommand
			{
				baseCommand = new InputDeviceCommand(RequestSyncCommand.Type, 8)
			};
		}

		// Token: 0x04000958 RID: 2392
		internal const int kSize = 8;

		// Token: 0x04000959 RID: 2393
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
