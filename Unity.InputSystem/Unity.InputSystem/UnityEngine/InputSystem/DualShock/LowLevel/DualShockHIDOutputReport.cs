using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x0200016A RID: 362
	[StructLayout(LayoutKind.Explicit, Size = 40)]
	internal struct DualShockHIDOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00046CD1 File Offset: 0x00044ED1
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0004D359 File Offset: 0x0004B559
		public FourCC typeStatic
		{
			get
			{
				return DualShockHIDOutputReport.Type;
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0004D360 File Offset: 0x0004B560
		public void SetMotorSpeeds(float lowFreq, float highFreq)
		{
			this.flags |= 1;
			this.lowFrequencyMotorSpeed = (byte)Mathf.Clamp(lowFreq * 255f, 0f, 255f);
			this.highFrequencyMotorSpeed = (byte)Mathf.Clamp(highFreq * 255f, 0f, 255f);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0004D3B8 File Offset: 0x0004B5B8
		public void SetColor(Color color)
		{
			this.flags |= 2;
			this.redColor = (byte)Mathf.Clamp(color.r * 255f, 0f, 255f);
			this.greenColor = (byte)Mathf.Clamp(color.g * 255f, 0f, 255f);
			this.blueColor = (byte)Mathf.Clamp(color.b * 255f, 0f, 255f);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0004D43C File Offset: 0x0004B63C
		public static DualShockHIDOutputReport Create(int outputReportSize)
		{
			return new DualShockHIDOutputReport
			{
				baseCommand = new InputDeviceCommand(DualShockHIDOutputReport.Type, 8 + outputReportSize),
				reportId = 5
			};
		}

		// Token: 0x04000907 RID: 2311
		internal const int kSize = 40;

		// Token: 0x04000908 RID: 2312
		internal const int kReportId = 5;

		// Token: 0x04000909 RID: 2313
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400090A RID: 2314
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x0400090B RID: 2315
		[FieldOffset(9)]
		public byte flags;

		// Token: 0x0400090C RID: 2316
		[FixedBuffer(typeof(byte), 2)]
		[FieldOffset(10)]
		public DualShockHIDOutputReport.<unknown1>e__FixedBuffer unknown1;

		// Token: 0x0400090D RID: 2317
		[FieldOffset(12)]
		public byte highFrequencyMotorSpeed;

		// Token: 0x0400090E RID: 2318
		[FieldOffset(13)]
		public byte lowFrequencyMotorSpeed;

		// Token: 0x0400090F RID: 2319
		[FieldOffset(14)]
		public byte redColor;

		// Token: 0x04000910 RID: 2320
		[FieldOffset(15)]
		public byte greenColor;

		// Token: 0x04000911 RID: 2321
		[FieldOffset(16)]
		public byte blueColor;

		// Token: 0x04000912 RID: 2322
		[FixedBuffer(typeof(byte), 23)]
		[FieldOffset(17)]
		public DualShockHIDOutputReport.<unknown2>e__FixedBuffer unknown2;

		// Token: 0x0200016B RID: 363
		[Flags]
		public enum Flags
		{
			// Token: 0x04000914 RID: 2324
			Rumble = 1,
			// Token: 0x04000915 RID: 2325
			Color = 2
		}

		// Token: 0x0200016C RID: 364
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 2)]
		public struct <unknown1>e__FixedBuffer
		{
			// Token: 0x04000916 RID: 2326
			public byte FixedElementField;
		}

		// Token: 0x0200016D RID: 365
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 23)]
		public struct <unknown2>e__FixedBuffer
		{
			// Token: 0x04000917 RID: 2327
			public byte FixedElementField;
		}
	}
}
