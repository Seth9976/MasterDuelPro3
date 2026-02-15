using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000186 RID: 390
	[StructLayout(LayoutKind.Explicit, Size = 520)]
	internal struct QueryUserIdCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0004DAE1 File Offset: 0x0004BCE1
		public static FourCC Type
		{
			get
			{
				return new FourCC('U', 'S', 'E', 'R');
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0004DAF0 File Offset: 0x0004BCF0
		public unsafe string ReadId()
		{
			fixed (QueryUserIdCommand* ptr = &this)
			{
				return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)(&ptr->idBuffer.FixedElementField)), 256);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0004DB1B File Offset: 0x0004BD1B
		public FourCC typeStatic
		{
			get
			{
				return QueryUserIdCommand.Type;
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004DB24 File Offset: 0x0004BD24
		public static QueryUserIdCommand Create()
		{
			return new QueryUserIdCommand
			{
				baseCommand = new InputDeviceCommand(QueryUserIdCommand.Type, 520)
			};
		}

		// Token: 0x04000951 RID: 2385
		public const int kMaxIdLength = 256;

		// Token: 0x04000952 RID: 2386
		internal const int kSize = 520;

		// Token: 0x04000953 RID: 2387
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000954 RID: 2388
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(8)]
		public QueryUserIdCommand.<idBuffer>e__FixedBuffer idBuffer;

		// Token: 0x02000187 RID: 391
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <idBuffer>e__FixedBuffer
		{
			// Token: 0x04000955 RID: 2389
			public byte FixedElementField;
		}
	}
}
