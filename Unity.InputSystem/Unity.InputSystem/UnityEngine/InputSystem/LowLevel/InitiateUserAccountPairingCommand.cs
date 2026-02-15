using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000175 RID: 373
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct InitiateUserAccountPairingCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0004D68F File Offset: 0x0004B88F
		public static FourCC Type
		{
			get
			{
				return new FourCC('P', 'A', 'I', 'R');
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0004D69E File Offset: 0x0004B89E
		public FourCC typeStatic
		{
			get
			{
				return InitiateUserAccountPairingCommand.Type;
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0004D6A8 File Offset: 0x0004B8A8
		public static InitiateUserAccountPairingCommand Create()
		{
			return new InitiateUserAccountPairingCommand
			{
				baseCommand = new InputDeviceCommand(InitiateUserAccountPairingCommand.Type, 8)
			};
		}

		// Token: 0x04000921 RID: 2337
		internal const int kSize = 8;

		// Token: 0x04000922 RID: 2338
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x02000176 RID: 374
		public enum Result
		{
			// Token: 0x04000924 RID: 2340
			SuccessfullyInitiated = 1,
			// Token: 0x04000925 RID: 2341
			ErrorNotSupported = -1,
			// Token: 0x04000926 RID: 2342
			ErrorAlreadyInProgress = -2
		}
	}
}
