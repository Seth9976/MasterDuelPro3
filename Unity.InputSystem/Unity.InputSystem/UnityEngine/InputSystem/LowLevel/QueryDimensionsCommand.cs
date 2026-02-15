using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200017B RID: 379
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct QueryDimensionsCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0004D791 File Offset: 0x0004B991
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'I', 'M', 'S');
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0004D7A0 File Offset: 0x0004B9A0
		public FourCC typeStatic
		{
			get
			{
				return QueryDimensionsCommand.Type;
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0004D7A8 File Offset: 0x0004B9A8
		public static QueryDimensionsCommand Create()
		{
			return new QueryDimensionsCommand
			{
				baseCommand = new InputDeviceCommand(QueryDimensionsCommand.Type, 16)
			};
		}

		// Token: 0x04000930 RID: 2352
		internal const int kSize = 16;

		// Token: 0x04000931 RID: 2353
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000932 RID: 2354
		[FieldOffset(8)]
		public Vector2 outDimensions;
	}
}
