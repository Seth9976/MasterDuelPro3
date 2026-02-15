using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001A4 RID: 420
	[StructLayout(LayoutKind.Explicit, Size = 560)]
	internal struct TouchscreenState : IInputStateTypeInfo
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x0004E1CB File Offset: 0x0004C3CB
		public static FourCC Format
		{
			get
			{
				return new FourCC('T', 'S', 'C', 'R');
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0004E1DC File Offset: 0x0004C3DC
		public unsafe TouchState* primaryTouch
		{
			get
			{
				fixed (byte* ptr = &this.primaryTouchData.FixedElementField)
				{
					return (TouchState*)ptr;
				}
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0004E1F8 File Offset: 0x0004C3F8
		public unsafe TouchState* touches
		{
			get
			{
				fixed (byte* ptr = &this.touchData.FixedElementField)
				{
					return (TouchState*)ptr;
				}
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0004E213 File Offset: 0x0004C413
		public FourCC format
		{
			get
			{
				return TouchscreenState.Format;
			}
		}

		// Token: 0x040009C8 RID: 2504
		public const int MaxTouches = 10;

		// Token: 0x040009C9 RID: 2505
		[FixedBuffer(typeof(byte), 56)]
		[InputControl(name = "primaryTouch", displayName = "Primary Touch", layout = "Touch", synthetic = true)]
		[InputControl(name = "primaryTouch/tap", usage = "PrimaryAction")]
		[InputControl(name = "position", useStateFrom = "primaryTouch/position")]
		[InputControl(name = "delta", useStateFrom = "primaryTouch/delta", layout = "Delta")]
		[InputControl(name = "pressure", useStateFrom = "primaryTouch/pressure")]
		[InputControl(name = "radius", useStateFrom = "primaryTouch/radius")]
		[InputControl(name = "press", useStateFrom = "primaryTouch/phase", layout = "TouchPress", synthetic = true, usages = new string[] { })]
		[InputControl(name = "displayIndex", useStateFrom = "primaryTouch/displayIndex", format = "BYTE")]
		[FieldOffset(0)]
		public TouchscreenState.<primaryTouchData>e__FixedBuffer primaryTouchData;

		// Token: 0x040009CA RID: 2506
		internal const int kTouchDataOffset = 56;

		// Token: 0x040009CB RID: 2507
		[FixedBuffer(typeof(byte), 560)]
		[InputControl(layout = "Touch", name = "touch", displayName = "Touch", arraySize = 10)]
		[FieldOffset(56)]
		public TouchscreenState.<touchData>e__FixedBuffer touchData;

		// Token: 0x020001A5 RID: 421
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 56)]
		public struct <primaryTouchData>e__FixedBuffer
		{
			// Token: 0x040009CC RID: 2508
			public byte FixedElementField;
		}

		// Token: 0x020001A6 RID: 422
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 560)]
		public struct <touchData>e__FixedBuffer
		{
			// Token: 0x040009CD RID: 2509
			public byte FixedElementField;
		}
	}
}
