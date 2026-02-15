using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001BA RID: 442
	internal struct InputEventStream
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0004F5A3 File Offset: 0x0004D7A3
		public bool isOpen
		{
			get
			{
				return this.m_IsOpen;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0004F5AB File Offset: 0x0004D7AB
		public int remainingEventCount
		{
			get
			{
				return this.m_RemainingNativeEventCount + this.m_RemainingAppendEventCount;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0004F5BA File Offset: 0x0004D7BA
		public int numEventsRetainedInBuffer
		{
			get
			{
				return this.m_NumEventsRetainedInBuffer;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004F5C2 File Offset: 0x0004D7C2
		public unsafe InputEvent* currentEventPtr
		{
			get
			{
				if (this.m_RemainingNativeEventCount > 0)
				{
					return this.m_CurrentNativeEventReadPtr;
				}
				if (this.m_RemainingAppendEventCount <= 0)
				{
					return null;
				}
				return this.m_CurrentAppendEventReadPtr;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0004F5E6 File Offset: 0x0004D7E6
		public unsafe uint numBytesRetainedInBuffer
		{
			get
			{
				return (uint)((long)((byte*)this.m_CurrentNativeEventWritePtr - (byte*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_NativeBuffer.data)));
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0004F604 File Offset: 0x0004D804
		public unsafe InputEventStream(ref InputEventBuffer eventBuffer, int maxAppendedEvents)
		{
			this.m_CurrentNativeEventWritePtr = (this.m_CurrentNativeEventReadPtr = (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(eventBuffer.data));
			this.m_NativeBuffer = eventBuffer;
			this.m_RemainingNativeEventCount = this.m_NativeBuffer.eventCount;
			this.m_NumEventsRetainedInBuffer = 0;
			this.m_CurrentAppendEventReadPtr = (this.m_CurrentAppendEventWritePtr = null);
			this.m_AppendBuffer = default(InputEventBuffer);
			this.m_RemainingAppendEventCount = 0;
			this.m_MaxAppendedEvents = maxAppendedEvents;
			this.m_IsOpen = true;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0004F684 File Offset: 0x0004D884
		public unsafe void Close(ref InputEventBuffer eventBuffer)
		{
			if (this.m_NumEventsRetainedInBuffer > 0)
			{
				void* bufferPtr = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_NativeBuffer.data);
				long newBufferSize = (long)((byte*)this.m_CurrentNativeEventWritePtr - (byte*)bufferPtr);
				this.m_NativeBuffer = new InputEventBuffer((InputEvent*)bufferPtr, this.m_NumEventsRetainedInBuffer, (int)newBufferSize, (int)this.m_NativeBuffer.capacityInBytes);
			}
			else
			{
				this.m_NativeBuffer.Reset();
			}
			if (this.m_AppendBuffer.data.IsCreated)
			{
				this.m_AppendBuffer.Dispose();
			}
			eventBuffer = this.m_NativeBuffer;
			this.m_IsOpen = false;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0004F718 File Offset: 0x0004D918
		public void CleanUpAfterException()
		{
			if (!this.isOpen)
			{
				return;
			}
			this.m_NativeBuffer.Reset();
			if (this.m_AppendBuffer.data.IsCreated)
			{
				this.m_AppendBuffer.Dispose();
			}
			this.m_IsOpen = false;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0004F760 File Offset: 0x0004D960
		public unsafe void Write(InputEvent* eventPtr)
		{
			if (this.m_AppendBuffer.eventCount >= this.m_MaxAppendedEvents)
			{
				Debug.LogError("Maximum number of queued events exceeded. Set the 'maxQueuedEventsPerUpdate' setting to a higher value if you need to queue more events than this. " + string.Format("Current limit is '{0}'.", this.m_MaxAppendedEvents));
				return;
			}
			bool isCreated = this.m_AppendBuffer.data.IsCreated;
			byte* oldBufferPtr = (byte*)this.m_AppendBuffer.bufferPtr.data;
			this.m_AppendBuffer.AppendEvent(eventPtr, 2048, Allocator.Temp);
			if (!isCreated)
			{
				this.m_CurrentAppendEventWritePtr = (this.m_CurrentAppendEventReadPtr = (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_AppendBuffer.data));
			}
			else
			{
				byte* newBufferPtr = (byte*)this.m_AppendBuffer.bufferPtr.data;
				if (oldBufferPtr != newBufferPtr)
				{
					long currentWriteOffset = (long)((byte*)this.m_CurrentAppendEventWritePtr - (byte*)oldBufferPtr);
					long currentReadOffset = (long)((byte*)this.m_CurrentAppendEventReadPtr - (byte*)oldBufferPtr);
					this.m_CurrentAppendEventWritePtr = (InputEvent*)(newBufferPtr + currentWriteOffset);
					this.m_CurrentAppendEventReadPtr = (InputEvent*)(newBufferPtr + currentReadOffset);
				}
			}
			this.m_RemainingAppendEventCount++;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0004F85C File Offset: 0x0004DA5C
		public unsafe InputEvent* Advance(bool leaveEventInBuffer)
		{
			if (this.m_RemainingNativeEventCount > 0)
			{
				this.m_NativeBuffer.AdvanceToNextEvent(ref this.m_CurrentNativeEventReadPtr, ref this.m_CurrentNativeEventWritePtr, ref this.m_NumEventsRetainedInBuffer, ref this.m_RemainingNativeEventCount, leaveEventInBuffer);
			}
			else if (this.m_RemainingAppendEventCount > 0)
			{
				int numEventRetained = 0;
				this.m_AppendBuffer.AdvanceToNextEvent(ref this.m_CurrentAppendEventReadPtr, ref this.m_CurrentAppendEventWritePtr, ref numEventRetained, ref this.m_RemainingAppendEventCount, false);
			}
			return this.currentEventPtr;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0004F8CC File Offset: 0x0004DACC
		public unsafe InputEvent* Peek()
		{
			if (this.m_RemainingNativeEventCount > 1)
			{
				return InputEvent.GetNextInMemory(this.m_CurrentNativeEventReadPtr);
			}
			if (this.m_RemainingNativeEventCount == 1)
			{
				if (this.m_RemainingAppendEventCount <= 0)
				{
					return null;
				}
				return this.m_CurrentAppendEventReadPtr;
			}
			else
			{
				if (this.m_RemainingAppendEventCount > 1)
				{
					return InputEvent.GetNextInMemory(this.m_CurrentAppendEventReadPtr);
				}
				return null;
			}
		}

		// Token: 0x04000A02 RID: 2562
		private InputEventBuffer m_NativeBuffer;

		// Token: 0x04000A03 RID: 2563
		private unsafe InputEvent* m_CurrentNativeEventReadPtr;

		// Token: 0x04000A04 RID: 2564
		private unsafe InputEvent* m_CurrentNativeEventWritePtr;

		// Token: 0x04000A05 RID: 2565
		private int m_RemainingNativeEventCount;

		// Token: 0x04000A06 RID: 2566
		private readonly int m_MaxAppendedEvents;

		// Token: 0x04000A07 RID: 2567
		private InputEventBuffer m_AppendBuffer;

		// Token: 0x04000A08 RID: 2568
		private unsafe InputEvent* m_CurrentAppendEventReadPtr;

		// Token: 0x04000A09 RID: 2569
		private unsafe InputEvent* m_CurrentAppendEventWritePtr;

		// Token: 0x04000A0A RID: 2570
		private int m_RemainingAppendEventCount;

		// Token: 0x04000A0B RID: 2571
		private int m_NumEventsRetainedInBuffer;

		// Token: 0x04000A0C RID: 2572
		private bool m_IsOpen;
	}
}
