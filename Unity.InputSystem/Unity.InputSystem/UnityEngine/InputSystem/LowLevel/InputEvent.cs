using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;
using UnityEngineInternal.Input;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001B3 RID: 435
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	public struct InputEvent
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0004E812 File Offset: 0x0004CA12
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x0004E824 File Offset: 0x0004CA24
		public FourCC type
		{
			get
			{
				return new FourCC((int)this.m_Event.type);
			}
			set
			{
				this.m_Event.type = (NativeInputEventType)value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0004E837 File Offset: 0x0004CA37
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0004E844 File Offset: 0x0004CA44
		public uint sizeInBytes
		{
			get
			{
				return (uint)this.m_Event.sizeInBytes;
			}
			set
			{
				if (value > 65535U)
				{
					throw new ArgumentException("Maximum event size is " + ushort.MaxValue.ToString(), "value");
				}
				this.m_Event.sizeInBytes = (ushort)value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0004E888 File Offset: 0x0004CA88
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x0004E89E File Offset: 0x0004CA9E
		public int eventId
		{
			get
			{
				return (int)((long)this.m_Event.eventId & 2147483647L);
			}
			set
			{
				this.m_Event.eventId = value | (int)((long)this.m_Event.eventId & (long)((ulong)int.MinValue));
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0004E8C1 File Offset: 0x0004CAC1
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x0004E8CE File Offset: 0x0004CACE
		public int deviceId
		{
			get
			{
				return (int)this.m_Event.deviceId;
			}
			set
			{
				this.m_Event.deviceId = (ushort)value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0004E8DD File Offset: 0x0004CADD
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x0004E8F0 File Offset: 0x0004CAF0
		public double time
		{
			get
			{
				return this.m_Event.time - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			set
			{
				this.m_Event.time = value + InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0004E904 File Offset: 0x0004CB04
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x0004E911 File Offset: 0x0004CB11
		internal double internalTime
		{
			get
			{
				return this.m_Event.time;
			}
			set
			{
				this.m_Event.time = value;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004E920 File Offset: 0x0004CB20
		public InputEvent(FourCC type, int sizeInBytes, int deviceId, double time = -1.0)
		{
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			this.m_Event.type = (NativeInputEventType)type;
			this.m_Event.sizeInBytes = (ushort)sizeInBytes;
			this.m_Event.deviceId = (ushort)deviceId;
			this.m_Event.time = time;
			this.m_Event.eventId = 0;
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0004E98A File Offset: 0x0004CB8A
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0004E9A8 File Offset: 0x0004CBA8
		public bool handled
		{
			get
			{
				return ((long)this.m_Event.eventId & (long)((ulong)int.MinValue)) == (long)((ulong)int.MinValue);
			}
			set
			{
				if (value)
				{
					this.m_Event.eventId = (int)((long)this.m_Event.eventId | (long)((ulong)int.MinValue));
					return;
				}
				this.m_Event.eventId = (int)((long)this.m_Event.eventId & 2147483647L);
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004E9F8 File Offset: 0x0004CBF8
		public override string ToString()
		{
			return string.Format("id={0} type={1} device={2} size={3} time={4}", new object[] { this.eventId, this.type, this.deviceId, this.sizeInBytes, this.time });
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004EA5C File Offset: 0x0004CC5C
		internal unsafe static InputEvent* GetNextInMemory(InputEvent* currentPtr)
		{
			uint alignedSizeInBytes = currentPtr->sizeInBytes.AlignToMultipleOf(4U);
			return currentPtr + alignedSizeInBytes / (uint)sizeof(InputEvent);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004EA7C File Offset: 0x0004CC7C
		internal unsafe static InputEvent* GetNextInMemoryChecked(InputEvent* currentPtr, ref InputEventBuffer buffer)
		{
			uint alignedSizeInBytes = currentPtr->sizeInBytes.AlignToMultipleOf(4U);
			InputEvent* nextPtr = currentPtr + alignedSizeInBytes / (uint)sizeof(InputEvent);
			if (!buffer.Contains(nextPtr))
			{
				throw new InvalidOperationException(string.Format("Event '{0}' is last event in given buffer with size {1}", new InputEventPtr(currentPtr), buffer.sizeInBytes));
			}
			return nextPtr;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004EACC File Offset: 0x0004CCCC
		public unsafe static bool Equals(InputEvent* first, InputEvent* second)
		{
			return first == second || (first != null && second != null && first->m_Event.sizeInBytes == second->m_Event.sizeInBytes && UnsafeUtility.MemCmp((void*)first, (void*)second, (long)((ulong)first->m_Event.sizeInBytes)) == 0);
		}

		// Token: 0x040009EE RID: 2542
		private const uint kHandledMask = 2147483648U;

		// Token: 0x040009EF RID: 2543
		private const uint kIdMask = 2147483647U;

		// Token: 0x040009F0 RID: 2544
		internal const int kBaseEventSize = 20;

		// Token: 0x040009F1 RID: 2545
		public const int InvalidEventId = 0;

		// Token: 0x040009F2 RID: 2546
		internal const int kAlignment = 4;

		// Token: 0x040009F3 RID: 2547
		[FieldOffset(0)]
		private NativeInputEvent m_Event;
	}
}
