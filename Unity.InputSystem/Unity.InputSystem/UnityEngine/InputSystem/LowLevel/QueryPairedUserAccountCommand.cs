using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000181 RID: 385
	[StructLayout(LayoutKind.Explicit, Size = 1040)]
	public struct QueryPairedUserAccountCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0004D92C File Offset: 0x0004BB2C
		public static FourCC Type
		{
			get
			{
				return new FourCC('P', 'A', 'C', 'C');
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0004D93C File Offset: 0x0004BB3C
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0004D968 File Offset: 0x0004BB68
		public unsafe string id
		{
			get
			{
				fixed (byte* ptr = &this.idBuffer.FixedElementField)
				{
					return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)ptr), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentException(string.Format("ID '{0}' exceeds maximum supported length of {1} characters", value, 256), "value");
				}
				fixed (byte* ptr = &this.idBuffer.FixedElementField)
				{
					byte* idBufferPtr = ptr;
					StringHelpers.WriteStringToBuffer(value, new IntPtr((void*)idBufferPtr), 256);
				}
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0004D9D4 File Offset: 0x0004BBD4
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0004DA00 File Offset: 0x0004BC00
		public unsafe string name
		{
			get
			{
				fixed (byte* ptr = &this.nameBuffer.FixedElementField)
				{
					return StringHelpers.ReadStringFromBuffer(new IntPtr((void*)ptr), 256);
				}
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 256)
				{
					throw new ArgumentException(string.Format("Name '{0}' exceeds maximum supported length of {1} characters", value, 256), "value");
				}
				fixed (byte* ptr = &this.nameBuffer.FixedElementField)
				{
					byte* nameBufferPtr = ptr;
					StringHelpers.WriteStringToBuffer(value, new IntPtr((void*)nameBufferPtr), 256);
				}
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0004DA6C File Offset: 0x0004BC6C
		public FourCC typeStatic
		{
			get
			{
				return QueryPairedUserAccountCommand.Type;
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0004DA74 File Offset: 0x0004BC74
		public static QueryPairedUserAccountCommand Create()
		{
			return new QueryPairedUserAccountCommand
			{
				baseCommand = new InputDeviceCommand(QueryPairedUserAccountCommand.Type, 1040)
			};
		}

		// Token: 0x04000940 RID: 2368
		internal const int kMaxNameLength = 256;

		// Token: 0x04000941 RID: 2369
		internal const int kMaxIdLength = 256;

		// Token: 0x04000942 RID: 2370
		internal const int kSize = 1040;

		// Token: 0x04000943 RID: 2371
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x04000944 RID: 2372
		[FieldOffset(8)]
		public ulong handle;

		// Token: 0x04000945 RID: 2373
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(16)]
		internal QueryPairedUserAccountCommand.<nameBuffer>e__FixedBuffer nameBuffer;

		// Token: 0x04000946 RID: 2374
		[FixedBuffer(typeof(byte), 512)]
		[FieldOffset(528)]
		internal QueryPairedUserAccountCommand.<idBuffer>e__FixedBuffer idBuffer;

		// Token: 0x02000182 RID: 386
		[Flags]
		public enum Result : long
		{
			// Token: 0x04000948 RID: 2376
			DevicePairedToUserAccount = 2L,
			// Token: 0x04000949 RID: 2377
			UserAccountSelectionInProgress = 4L,
			// Token: 0x0400094A RID: 2378
			UserAccountSelectionComplete = 8L,
			// Token: 0x0400094B RID: 2379
			UserAccountSelectionCanceled = 16L
		}

		// Token: 0x02000183 RID: 387
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <idBuffer>e__FixedBuffer
		{
			// Token: 0x0400094C RID: 2380
			public byte FixedElementField;
		}

		// Token: 0x02000184 RID: 388
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 512)]
		public struct <nameBuffer>e__FixedBuffer
		{
			// Token: 0x0400094D RID: 2381
			public byte FixedElementField;
		}
	}
}
