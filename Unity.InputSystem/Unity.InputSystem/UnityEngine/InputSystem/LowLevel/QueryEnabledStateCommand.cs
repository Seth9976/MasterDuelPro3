using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200017C RID: 380
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryEnabledStateCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0004D7D1 File Offset: 0x0004B9D1
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'E', 'N', 'B');
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0004D7E0 File Offset: 0x0004B9E0
		public FourCC typeStatic
		{
			get
			{
				return QueryEnabledStateCommand.Type;
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0004D7E8 File Offset: 0x0004B9E8
		public static QueryEnabledStateCommand Create()
		{
			return new QueryEnabledStateCommand
			{
				baseCommand = new InputDeviceCommand(QueryEnabledStateCommand.Type, 9)
			};
		}

		// Token: 0x04000933 RID: 2355
		internal const int kSize = 9;

		// Token: 0x04000934 RID: 2356
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000935 RID: 2357
		[FieldOffset(8)]
		public bool isEnabled;
	}
}
