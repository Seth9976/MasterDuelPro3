using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200017D RID: 381
	[StructLayout(LayoutKind.Explicit, Size = 268)]
	public struct QueryKeyNameCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0004D811 File Offset: 0x0004BA11
		public static FourCC Type
		{
			get
			{
				return new FourCC('K', 'Y', 'C', 'F');
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0004D820 File Offset: 0x0004BA20
		public unsafe string ReadKeyName()
		{
			fixed (QueryKeyNameCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0004D84B File Offset: 0x0004BA4B
		public FourCC typeStatic
		{
			get
			{
				return QueryKeyNameCommand.Type;
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0004D854 File Offset: 0x0004BA54
		public static QueryKeyNameCommand Create(Key key)
		{
			return new QueryKeyNameCommand
			{
				baseCommand = new InputDeviceCommand(QueryKeyNameCommand.Type, 268),
				scanOrKeyCode = (int)key
			};
		}

		// Token: 0x04000936 RID: 2358
		internal const int kMaxNameLength = 256;

		// Token: 0x04000937 RID: 2359
		internal const int kSize = 268;

		// Token: 0x04000938 RID: 2360
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000939 RID: 2361
		[FieldOffset(8)]
		public int scanOrKeyCode;

		// Token: 0x0400093A RID: 2362
		[FixedBuffer(typeof(byte), 256)]
		[FieldOffset(12)]
		public QueryKeyNameCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x0200017E RID: 382
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x0400093B RID: 2363
			public byte FixedElementField;
		}
	}
}
