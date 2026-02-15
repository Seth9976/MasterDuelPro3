using System;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000018 RID: 24
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct PointerEvent : IEventProperties
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002B6B File Offset: 0x00000D6B
		public bool isPrimaryPointer
		{
			get
			{
				return this.pointerIndex == 0;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B76 File Offset: 0x00000D76
		public float azimuth
		{
			get
			{
				return InputManagerProvider.TiltToAzimuth(this.tilt);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002B83 File Offset: 0x00000D83
		public float altitude
		{
			get
			{
				return InputManagerProvider.TiltToAltitude(this.tilt);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002B90 File Offset: 0x00000D90
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002B98 File Offset: 0x00000D98
		public DiscreteTime timestamp { readonly get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002BA1 File Offset: 0x00000DA1
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public EventSource eventSource { readonly get; set; }

		// Token: 0x1700002B RID: 43
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002BB2 File Offset: 0x00000DB2
		public uint playerId
		{
			[CompilerGenerated]
			set
			{
				this.<playerId>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002BBB File Offset: 0x00000DBB
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002BC3 File Offset: 0x00000DC3
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x06000061 RID: 97 RVA: 0x00002BCC File Offset: 0x00000DCC
		public override string ToString()
		{
			string pen = ((this.eventSource == EventSource.Pen) ? string.Format(" tilt:({0:f1},{1:f1}) az:{2:f2} al:{3:f2} twist:{4} pressure:{5} isInverted:{6}", new object[]
			{
				this.tilt.x,
				this.tilt.y,
				this.azimuth,
				this.altitude,
				this.twist,
				this.pressure,
				this.isInverted ? 1 : 0
			}) : "");
			string touch = ((this.eventSource == EventSource.Touch) ? string.Format(" finger:{0} tilt:({1:f1},{2:f1}) twist:{3} pressure:{4}", new object[]
			{
				this.pointerIndex,
				this.tilt.x,
				this.tilt.y,
				this.twist,
				this.pressure
			}) : "");
			string dsp = string.Format(" dsp:{0}", this.displayIndex);
			string gen = pen + touch + dsp;
			string text;
			switch (this.type)
			{
			case PointerEvent.Type.PointerMoved:
				text = string.Format("{0} pos:{1} dlt:{2} btns:{3}{4}", new object[] { this.type, this.position, this.deltaPosition, this.buttonsState, gen });
				break;
			case PointerEvent.Type.Scroll:
				text = string.Format("{0} pos:{1} scr:{2}{3}", new object[] { this.type, this.position, this.scroll, gen });
				break;
			case PointerEvent.Type.ButtonPressed:
			case PointerEvent.Type.ButtonReleased:
				text = string.Format("{0} pos:{1} btn:{2} btns:{3} clk:{4}{5}", new object[] { this.type, this.position, this.button, this.buttonsState, this.clickCount, gen });
				break;
			case PointerEvent.Type.State:
				text = string.Format("{0} pos:{1} btns:{2}{3}", new object[] { this.type, this.position, this.buttonsState, gen });
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return text;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E70 File Offset: 0x00001070
		internal static PointerEvent.Button ButtonFromButtonIndex(int index)
		{
			return (PointerEvent.Button)((index <= 31) ? (1 << index) : 0);
		}

		// Token: 0x04000078 RID: 120
		public PointerEvent.Type type;

		// Token: 0x04000079 RID: 121
		public int pointerIndex;

		// Token: 0x0400007A RID: 122
		public Vector2 position;

		// Token: 0x0400007B RID: 123
		public Vector2 deltaPosition;

		// Token: 0x0400007C RID: 124
		public Vector2 scroll;

		// Token: 0x0400007D RID: 125
		public int displayIndex;

		// Token: 0x0400007E RID: 126
		public Vector2 tilt;

		// Token: 0x0400007F RID: 127
		public float twist;

		// Token: 0x04000080 RID: 128
		public float pressure;

		// Token: 0x04000081 RID: 129
		public bool isInverted;

		// Token: 0x04000082 RID: 130
		public PointerEvent.Button button;

		// Token: 0x04000083 RID: 131
		public PointerEvent.ButtonsState buttonsState;

		// Token: 0x04000084 RID: 132
		public int clickCount;

		// Token: 0x02000019 RID: 25
		public enum Type
		{
			// Token: 0x0400008A RID: 138
			PointerMoved = 1,
			// Token: 0x0400008B RID: 139
			Scroll,
			// Token: 0x0400008C RID: 140
			ButtonPressed,
			// Token: 0x0400008D RID: 141
			ButtonReleased,
			// Token: 0x0400008E RID: 142
			State,
			// Token: 0x0400008F RID: 143
			TouchCanceled
		}

		// Token: 0x0200001A RID: 26
		[Flags]
		public enum Button : uint
		{
			// Token: 0x04000091 RID: 145
			None = 0U,
			// Token: 0x04000092 RID: 146
			Primary = 1U,
			// Token: 0x04000093 RID: 147
			FingerInTouch = 1U,
			// Token: 0x04000094 RID: 148
			PenTipInTouch = 1U,
			// Token: 0x04000095 RID: 149
			PenEraserInTouch = 2U,
			// Token: 0x04000096 RID: 150
			PenBarrelButton = 4U,
			// Token: 0x04000097 RID: 151
			MouseLeft = 1U,
			// Token: 0x04000098 RID: 152
			MouseRight = 2U,
			// Token: 0x04000099 RID: 153
			MouseMiddle = 4U,
			// Token: 0x0400009A RID: 154
			MouseForward = 8U,
			// Token: 0x0400009B RID: 155
			MouseBack = 16U
		}

		// Token: 0x0200001B RID: 27
		public struct ButtonsState
		{
			// Token: 0x06000063 RID: 99 RVA: 0x00002E90 File Offset: 0x00001090
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Set(PointerEvent.Button button, bool pressed)
			{
				if (pressed)
				{
					this._state |= (uint)button;
				}
				else
				{
					this._state &= (uint)(~(uint)button);
				}
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00002EC4 File Offset: 0x000010C4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Get(PointerEvent.Button button)
			{
				return (this._state & (uint)button) > 0U;
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00002EE1 File Offset: 0x000010E1
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Reset()
			{
				this._state = 0U;
			}

			// Token: 0x06000066 RID: 102 RVA: 0x00002EEC File Offset: 0x000010EC
			public override string ToString()
			{
				return string.Format("{0:x2}", this._state);
			}

			// Token: 0x0400009C RID: 156
			private uint _state;
		}
	}
}
