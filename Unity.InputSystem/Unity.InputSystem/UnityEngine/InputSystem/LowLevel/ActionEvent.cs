using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001A7 RID: 423
	[StructLayout(LayoutKind.Explicit, Size = 37)]
	internal struct ActionEvent : IInputEventTypeInfo
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0004E21A File Offset: 0x0004C41A
		public static FourCC Type
		{
			get
			{
				return new FourCC('A', 'C', 'T', 'N');
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0004E229 File Offset: 0x0004C429
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x0004E231 File Offset: 0x0004C431
		public double startTime
		{
			get
			{
				return this.m_StartTime;
			}
			set
			{
				this.m_StartTime = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0004E23A File Offset: 0x0004C43A
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x0004E242 File Offset: 0x0004C442
		public InputActionPhase phase
		{
			get
			{
				return (InputActionPhase)this.m_Phase;
			}
			set
			{
				this.m_Phase = (byte)value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0004E24C File Offset: 0x0004C44C
		public unsafe byte* valueData
		{
			get
			{
				fixed (byte* ptr = &this.m_ValueData.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0004E267 File Offset: 0x0004C467
		public int valueSizeInBytes
		{
			get
			{
				return (int)(this.baseEvent.sizeInBytes - 20U - 16U);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0004E27A File Offset: 0x0004C47A
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x0004E282 File Offset: 0x0004C482
		public int stateIndex
		{
			get
			{
				return (int)this.m_StateIndex;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new NotSupportedException("State count cannot exceed byte.MaxValue");
				}
				this.m_StateIndex = (byte)value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0004E2A3 File Offset: 0x0004C4A3
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x0004E2AB File Offset: 0x0004C4AB
		public int controlIndex
		{
			get
			{
				return (int)this.m_ControlIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new NotSupportedException("Control count cannot exceed ushort.MaxValue");
				}
				this.m_ControlIndex = (ushort)value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0004E2CC File Offset: 0x0004C4CC
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
		public int bindingIndex
		{
			get
			{
				return (int)this.m_BindingIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new NotSupportedException("Binding count cannot exceed ushort.MaxValue");
				}
				this.m_BindingIndex = (ushort)value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0004E2F5 File Offset: 0x0004C4F5
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0004E30C File Offset: 0x0004C50C
		public int interactionIndex
		{
			get
			{
				if (this.m_InteractionIndex == 65535)
				{
					return -1;
				}
				return (int)this.m_InteractionIndex;
			}
			set
			{
				if (value == -1)
				{
					this.m_InteractionIndex = ushort.MaxValue;
					return;
				}
				if (value < 0 || value >= 65535)
				{
					throw new NotSupportedException("Interaction count cannot exceed ushort.MaxValue-1");
				}
				this.m_InteractionIndex = (ushort)value;
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004E340 File Offset: 0x0004C540
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (ActionEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0004E356 File Offset: 0x0004C556
		public FourCC typeStatic
		{
			get
			{
				return ActionEvent.Type;
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004E35D File Offset: 0x0004C55D
		public static int GetEventSizeWithValueSize(int valueSizeInBytes)
		{
			return 36 + valueSizeInBytes;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0004E364 File Offset: 0x0004C564
		public unsafe static ActionEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<ActionEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into ActionEvent", ptr.type));
			}
			return (ActionEvent*)ptr.data;
		}

		// Token: 0x040009CE RID: 2510
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040009CF RID: 2511
		[FieldOffset(20)]
		private ushort m_ControlIndex;

		// Token: 0x040009D0 RID: 2512
		[FieldOffset(22)]
		private ushort m_BindingIndex;

		// Token: 0x040009D1 RID: 2513
		[FieldOffset(24)]
		private ushort m_InteractionIndex;

		// Token: 0x040009D2 RID: 2514
		[FieldOffset(26)]
		private byte m_StateIndex;

		// Token: 0x040009D3 RID: 2515
		[FieldOffset(27)]
		private byte m_Phase;

		// Token: 0x040009D4 RID: 2516
		[FieldOffset(28)]
		private double m_StartTime;

		// Token: 0x040009D5 RID: 2517
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(36)]
		public ActionEvent.<m_ValueData>e__FixedBuffer m_ValueData;

		// Token: 0x020001A8 RID: 424
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <m_ValueData>e__FixedBuffer
		{
			// Token: 0x040009D6 RID: 2518
			public byte FixedElementField;
		}
	}
}
