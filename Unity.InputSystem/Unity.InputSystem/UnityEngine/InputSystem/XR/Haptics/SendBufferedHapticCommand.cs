using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x020000FA RID: 250
	[StructLayout(LayoutKind.Explicit, Size = 1040)]
	public struct SendBufferedHapticCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0003F91D File Offset: 0x0003DB1D
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'U', '0');
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0003F92C File Offset: 0x0003DB2C
		public FourCC typeStatic
		{
			get
			{
				return SendBufferedHapticCommand.Type;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003F934 File Offset: 0x0003DB34
		public unsafe static SendBufferedHapticCommand Create(byte[] rumbleBuffer)
		{
			if (rumbleBuffer == null)
			{
				throw new ArgumentNullException("rumbleBuffer");
			}
			int rumbleBufferSize = Mathf.Min(1024, rumbleBuffer.Length);
			SendBufferedHapticCommand newCommand = new SendBufferedHapticCommand
			{
				baseCommand = new InputDeviceCommand(SendBufferedHapticCommand.Type, 1040),
				bufferSize = rumbleBufferSize
			};
			SendBufferedHapticCommand* commandPtr = &newCommand;
			fixed (byte[] array = rumbleBuffer)
			{
				byte* src;
				if (rumbleBuffer == null || array.Length == 0)
				{
					src = null;
				}
				else
				{
					src = &array[0];
				}
				for (int cpyIndex = 0; cpyIndex < rumbleBufferSize; cpyIndex++)
				{
					*((ref commandPtr->buffer.FixedElementField) + cpyIndex) = src[cpyIndex];
				}
			}
			return newCommand;
		}

		// Token: 0x040005B4 RID: 1460
		private const int kMaxHapticBufferSize = 1024;

		// Token: 0x040005B5 RID: 1461
		private const int kSize = 1040;

		// Token: 0x040005B6 RID: 1462
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x040005B7 RID: 1463
		[FieldOffset(8)]
		private int channel;

		// Token: 0x040005B8 RID: 1464
		[FieldOffset(12)]
		private int bufferSize;

		// Token: 0x040005B9 RID: 1465
		[FixedBuffer(typeof(byte), 1024)]
		[FieldOffset(16)]
		private SendBufferedHapticCommand.<buffer>e__FixedBuffer buffer;

		// Token: 0x020000FB RID: 251
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1024)]
		public struct <buffer>e__FixedBuffer
		{
			// Token: 0x040005BA RID: 1466
			public byte FixedElementField;
		}
	}
}
