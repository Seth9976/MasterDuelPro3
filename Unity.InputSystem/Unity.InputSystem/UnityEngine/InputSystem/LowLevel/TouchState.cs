using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001A3 RID: 419
	[StructLayout(LayoutKind.Explicit, Size = 56)]
	public struct TouchState : IInputStateTypeInfo
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0004DF8B File Offset: 0x0004C18B
		public static FourCC Format
		{
			get
			{
				return new FourCC('T', 'O', 'U', 'C');
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0004DF9A File Offset: 0x0004C19A
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0004DFA2 File Offset: 0x0004C1A2
		public TouchPhase phase
		{
			get
			{
				return (TouchPhase)this.phaseId;
			}
			set
			{
				this.phaseId = (byte)value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0004DFAC File Offset: 0x0004C1AC
		public bool isNoneEndedOrCanceled
		{
			get
			{
				return this.phase == TouchPhase.None || this.phase == TouchPhase.Ended || this.phase == TouchPhase.Canceled;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0004DFCA File Offset: 0x0004C1CA
		public bool isInProgress
		{
			get
			{
				return this.phase == TouchPhase.Began || this.phase == TouchPhase.Moved || this.phase == TouchPhase.Stationary;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0004DFE9 File Offset: 0x0004C1E9
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x0004DFF6 File Offset: 0x0004C1F6
		public bool isPrimaryTouch
		{
			get
			{
				return (this.flags & 8) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 8;
					return;
				}
				this.flags &= 247;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0004E01E File Offset: 0x0004C21E
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x0004E02C File Offset: 0x0004C22C
		internal bool isOrphanedPrimaryTouch
		{
			get
			{
				return (this.flags & 64) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 64;
					return;
				}
				this.flags &= 191;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0004E055 File Offset: 0x0004C255
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x0004E062 File Offset: 0x0004C262
		public bool isIndirectTouch
		{
			get
			{
				return (this.flags & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 1;
					return;
				}
				this.flags &= 254;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0004E08A File Offset: 0x0004C28A
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x0004E092 File Offset: 0x0004C292
		public bool isTap
		{
			get
			{
				return this.isTapPress;
			}
			set
			{
				this.isTapPress = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0004E09B File Offset: 0x0004C29B
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x0004E0A9 File Offset: 0x0004C2A9
		internal bool isTapPress
		{
			get
			{
				return (this.flags & 16) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 16;
					return;
				}
				this.flags &= 239;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0004E0D2 File Offset: 0x0004C2D2
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x0004E0E0 File Offset: 0x0004C2E0
		internal bool isTapRelease
		{
			get
			{
				return (this.flags & 32) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 32;
					return;
				}
				this.flags &= 223;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0004E109 File Offset: 0x0004C309
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x0004E11A File Offset: 0x0004C31A
		internal bool beganInSameFrame
		{
			get
			{
				return (this.flags & 128) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 128;
					return;
				}
				this.flags &= 127;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0004E143 File Offset: 0x0004C343
		public FourCC format
		{
			get
			{
				return TouchState.Format;
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0004E14C File Offset: 0x0004C34C
		public override string ToString()
		{
			return string.Format("{{ id={0} phase={1} pos={2} delta={3} pressure={4} radius={5} primary={6} }}", new object[] { this.touchId, this.phase, this.position, this.delta, this.pressure, this.radius, this.isPrimaryTouch });
		}

		// Token: 0x040009BB RID: 2491
		internal const int kSizeInBytes = 56;

		// Token: 0x040009BC RID: 2492
		[InputControl(displayName = "Touch ID", layout = "Integer", synthetic = true, dontReset = true)]
		[FieldOffset(0)]
		public int touchId;

		// Token: 0x040009BD RID: 2493
		[InputControl(displayName = "Position", dontReset = true)]
		[FieldOffset(4)]
		public Vector2 position;

		// Token: 0x040009BE RID: 2494
		[InputControl(displayName = "Delta", layout = "Delta")]
		[FieldOffset(12)]
		public Vector2 delta;

		// Token: 0x040009BF RID: 2495
		[InputControl(displayName = "Pressure", layout = "Axis")]
		[FieldOffset(20)]
		public float pressure;

		// Token: 0x040009C0 RID: 2496
		[InputControl(displayName = "Radius")]
		[FieldOffset(24)]
		public Vector2 radius;

		// Token: 0x040009C1 RID: 2497
		[InputControl(name = "phase", displayName = "Touch Phase", layout = "TouchPhase", synthetic = true)]
		[InputControl(name = "press", displayName = "Touch Contact?", layout = "TouchPress", useStateFrom = "phase")]
		[FieldOffset(32)]
		public byte phaseId;

		// Token: 0x040009C2 RID: 2498
		[InputControl(name = "tapCount", displayName = "Tap Count", layout = "Integer")]
		[FieldOffset(33)]
		public byte tapCount;

		// Token: 0x040009C3 RID: 2499
		[InputControl(name = "displayIndex", displayName = "Display Index", layout = "Integer")]
		[FieldOffset(34)]
		public byte displayIndex;

		// Token: 0x040009C4 RID: 2500
		[InputControl(name = "indirectTouch", displayName = "Indirect Touch?", layout = "Button", bit = 0U, synthetic = true)]
		[InputControl(name = "tap", displayName = "Tap", layout = "Button", bit = 4U)]
		[FieldOffset(35)]
		public byte flags;

		// Token: 0x040009C5 RID: 2501
		[FieldOffset(36)]
		internal uint updateStepCount;

		// Token: 0x040009C6 RID: 2502
		[InputControl(displayName = "Start Time", layout = "Double", synthetic = true)]
		[FieldOffset(40)]
		public double startTime;

		// Token: 0x040009C7 RID: 2503
		[InputControl(displayName = "Start Position", synthetic = true)]
		[FieldOffset(48)]
		public Vector2 startPosition;
	}
}
