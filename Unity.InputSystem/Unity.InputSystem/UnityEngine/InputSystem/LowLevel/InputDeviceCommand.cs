using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000179 RID: 377
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct InputDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0004D6D0 File Offset: 0x0004B8D0
		public int payloadSizeInBytes
		{
			get
			{
				return this.sizeInBytes - 8;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0004D6DC File Offset: 0x0004B8DC
		public unsafe void* payloadPtr
		{
			get
			{
				fixed (InputDeviceCommand* ptr = &this)
				{
					void* thisPtr = (void*)ptr;
					return (void*)((byte*)thisPtr + 8);
				}
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0004D6F1 File Offset: 0x0004B8F1
		public InputDeviceCommand(FourCC type, int sizeInBytes = 8)
		{
			this.type = type;
			this.sizeInBytes = sizeInBytes;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0004D704 File Offset: 0x0004B904
		public unsafe static NativeArray<byte> AllocateNative(FourCC type, int payloadSize)
		{
			int sizeInBytes = payloadSize + 8;
			NativeArray<byte> nativeArray = new NativeArray<byte>(sizeInBytes, Allocator.Temp, NativeArrayOptions.ClearMemory);
			InputDeviceCommand* commandPtr = (InputDeviceCommand*)nativeArray.GetUnsafePtr<byte>();
			commandPtr->type = type;
			commandPtr->sizeInBytes = sizeInBytes;
			return nativeArray;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0004D734 File Offset: 0x0004B934
		public FourCC typeStatic
		{
			get
			{
				return default(FourCC);
			}
		}

		// Token: 0x04000927 RID: 2343
		internal const int kBaseCommandSize = 8;

		// Token: 0x04000928 RID: 2344
		public const int BaseCommandSize = 8;

		// Token: 0x04000929 RID: 2345
		public const long GenericFailure = -1L;

		// Token: 0x0400092A RID: 2346
		public const long GenericSuccess = 1L;

		// Token: 0x0400092B RID: 2347
		[FieldOffset(0)]
		public FourCC type;

		// Token: 0x0400092C RID: 2348
		[FieldOffset(4)]
		public int sizeInBytes;
	}
}
