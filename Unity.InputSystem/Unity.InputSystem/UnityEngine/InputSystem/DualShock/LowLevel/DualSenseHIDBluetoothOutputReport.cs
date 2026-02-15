using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x02000165 RID: 357
	[StructLayout(LayoutKind.Explicit, Size = 86)]
	internal struct DualSenseHIDBluetoothOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00046CD1 File Offset: 0x00044ED1
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0004D2E2 File Offset: 0x0004B4E2
		public FourCC typeStatic
		{
			get
			{
				return DualSenseHIDBluetoothOutputReport.Type;
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0004D2EC File Offset: 0x0004B4EC
		public static DualSenseHIDBluetoothOutputReport Create(DualSenseHIDOutputReportPayload payload, byte outputSequenceId, int outputReportSize)
		{
			return new DualSenseHIDBluetoothOutputReport
			{
				baseCommand = new InputDeviceCommand(DualSenseHIDBluetoothOutputReport.Type, 8 + outputReportSize),
				reportId = 49,
				tag1 = (byte)((outputSequenceId & 15) << 4),
				tag2 = 16,
				payload = payload
			};
		}

		// Token: 0x040008E7 RID: 2279
		internal const int kSize = 86;

		// Token: 0x040008E8 RID: 2280
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040008E9 RID: 2281
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x040008EA RID: 2282
		[FieldOffset(9)]
		public byte tag1;

		// Token: 0x040008EB RID: 2283
		[FieldOffset(10)]
		public byte tag2;

		// Token: 0x040008EC RID: 2284
		[FieldOffset(11)]
		public DualSenseHIDOutputReportPayload payload;

		// Token: 0x040008ED RID: 2285
		[FieldOffset(82)]
		public uint crc32;

		// Token: 0x040008EE RID: 2286
		[FixedBuffer(typeof(byte), 74)]
		[FieldOffset(8)]
		public DualSenseHIDBluetoothOutputReport.<rawData>e__FixedBuffer rawData;

		// Token: 0x02000166 RID: 358
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 74)]
		public struct <rawData>e__FixedBuffer
		{
			// Token: 0x040008EF RID: 2287
			public byte FixedElementField;
		}
	}
}
