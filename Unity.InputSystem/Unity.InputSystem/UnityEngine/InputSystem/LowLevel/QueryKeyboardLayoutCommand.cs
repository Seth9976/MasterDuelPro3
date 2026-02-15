using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200017F RID: 383
	[StructLayout(LayoutKind.Explicit, Size = 264)]
	public struct QueryKeyboardLayoutCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0004D888 File Offset: 0x0004BA88
		public static FourCC Type
		{
			get
			{
				return new FourCC('K', 'B', 'L', 'T');
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0004D898 File Offset: 0x0004BA98
		public unsafe string ReadLayoutName()
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0004D8C4 File Offset: 0x0004BAC4
		public unsafe void WriteLayoutName(string name)
		{
			fixed (QueryKeyboardLayoutCommand* ptr = &this)
			{
				QueryKeyboardLayoutCommand* thisPtr = ptr;
				StringHelpers.WriteStringToBuffer(name, new IntPtr((void*)(&thisPtr->nameBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0004D8F6 File Offset: 0x0004BAF6
		public FourCC typeStatic
		{
			get
			{
				return QueryKeyboardLayoutCommand.Type;
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0004D900 File Offset: 0x0004BB00
		public static QueryKeyboardLayoutCommand Create()
		{
			return new QueryKeyboardLayoutCommand
			{
				baseCommand = new InputDeviceCommand(QueryKeyboardLayoutCommand.Type, 264)
			};
		}

		// Token: 0x0400093C RID: 2364
		internal const int kMaxNameLength = 256;

		// Token: 0x0400093D RID: 2365
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400093E RID: 2366
		[FixedBuffer(typeof(byte), 256)]
		[FieldOffset(8)]
		public QueryKeyboardLayoutCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x02000180 RID: 384
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 256)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x0400093F RID: 2367
			public byte FixedElementField;
		}
	}
}
