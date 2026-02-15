using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x02000164 RID: 356
	[StructLayout(LayoutKind.Explicit, Size = 56)]
	internal struct DualSenseHIDUSBOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00046CD1 File Offset: 0x00044ED1
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0004D2A0 File Offset: 0x0004B4A0
		public FourCC typeStatic
		{
			get
			{
				return DualSenseHIDUSBOutputReport.Type;
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0004D2A8 File Offset: 0x0004B4A8
		public static DualSenseHIDUSBOutputReport Create(DualSenseHIDOutputReportPayload payload, int outputReportSize)
		{
			return new DualSenseHIDUSBOutputReport
			{
				baseCommand = new InputDeviceCommand(DualSenseHIDUSBOutputReport.Type, 8 + outputReportSize),
				reportId = 2,
				payload = payload
			};
		}

		// Token: 0x040008E3 RID: 2275
		internal const int kSize = 56;

		// Token: 0x040008E4 RID: 2276
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040008E5 RID: 2277
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x040008E6 RID: 2278
		[FieldOffset(9)]
		public DualSenseHIDOutputReportPayload payload;
	}
}
