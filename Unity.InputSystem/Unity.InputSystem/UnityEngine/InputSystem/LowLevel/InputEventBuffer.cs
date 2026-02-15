using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001B4 RID: 436
	public struct InputEventBuffer : IEnumerable<InputEventPtr>, IEnumerable, IDisposable, ICloneable
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0004EB1B File Offset: 0x0004CD1B
		public int eventCount
		{
			get
			{
				return this.m_EventCount;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0004EB23 File Offset: 0x0004CD23
		public long sizeInBytes
		{
			get
			{
				return this.m_SizeInBytes;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0004EB2B File Offset: 0x0004CD2B
		public long capacityInBytes
		{
			get
			{
				if (!this.m_Buffer.IsCreated)
				{
					return 0L;
				}
				return (long)this.m_Buffer.Length;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0004EB49 File Offset: 0x0004CD49
		public NativeArray<byte> data
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0004EB51 File Offset: 0x0004CD51
		public unsafe InputEventPtr bufferPtr
		{
			get
			{
				return (InputEvent*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer);
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004EB64 File Offset: 0x0004CD64
		public unsafe InputEventBuffer(InputEvent* eventPtr, int eventCount, int sizeInBytes = -1, int capacityInBytes = -1)
		{
			this = default(InputEventBuffer);
			if (eventPtr == null && eventCount != 0)
			{
				throw new ArgumentException("eventPtr is NULL but eventCount is != 0", "eventCount");
			}
			if (capacityInBytes != 0 && capacityInBytes < sizeInBytes)
			{
				throw new ArgumentException(string.Format("capacity({0}) cannot be smaller than size({1})", capacityInBytes, sizeInBytes), "capacityInBytes");
			}
			if (eventPtr != null)
			{
				if (capacityInBytes < 0)
				{
					capacityInBytes = sizeInBytes;
				}
				this.m_Buffer = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)eventPtr, (capacityInBytes > 0) ? capacityInBytes : 0, Allocator.None);
				this.m_SizeInBytes = ((sizeInBytes >= 0) ? ((long)sizeInBytes) : (-1L));
				this.m_EventCount = eventCount;
				this.m_WeOwnTheBuffer = false;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0004EC00 File Offset: 0x0004CE00
		public InputEventBuffer(NativeArray<byte> buffer, int eventCount, int sizeInBytes = -1, bool transferNativeArrayOwnership = false)
		{
			if (eventCount > 0 && !buffer.IsCreated)
			{
				throw new ArgumentException("buffer has no data but eventCount is > 0", "eventCount");
			}
			if (sizeInBytes > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("sizeInBytes");
			}
			this.m_Buffer = buffer;
			this.m_WeOwnTheBuffer = transferNativeArrayOwnership;
			this.m_SizeInBytes = (long)((sizeInBytes >= 0) ? sizeInBytes : buffer.Length);
			this.m_EventCount = eventCount;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0004EC6C File Offset: 0x0004CE6C
		public unsafe void AppendEvent(InputEvent* eventPtr, int capacityIncrementInBytes = 2048, Allocator allocator = Allocator.Persistent)
		{
			if (eventPtr == null)
			{
				throw new ArgumentNullException("eventPtr");
			}
			uint eventSizeInBytes = eventPtr->sizeInBytes;
			UnsafeUtility.MemCpy((void*)this.AllocateEvent((int)eventSizeInBytes, capacityIncrementInBytes, allocator), (void*)eventPtr, (long)((ulong)eventSizeInBytes));
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004ECA4 File Offset: 0x0004CEA4
		public unsafe InputEvent* AllocateEvent(int sizeInBytes, int capacityIncrementInBytes = 2048, Allocator allocator = Allocator.Persistent)
		{
			if (sizeInBytes < 20)
			{
				throw new ArgumentException(string.Format("sizeInBytes must be >= sizeof(InputEvent) == {0} (was {1})", 20, sizeInBytes), "sizeInBytes");
			}
			int alignedSizeInBytes = sizeInBytes.AlignToMultipleOf(4);
			long necessaryCapacity = this.m_SizeInBytes + (long)alignedSizeInBytes;
			if (this.capacityInBytes < necessaryCapacity)
			{
				long newCapacity = necessaryCapacity.AlignToMultipleOf((long)capacityIncrementInBytes);
				if (newCapacity > 2147483647L)
				{
					throw new NotImplementedException("NativeArray long support");
				}
				NativeArray<byte> newBuffer = new NativeArray<byte>((int)newCapacity, allocator, NativeArrayOptions.ClearMemory);
				if (this.m_Buffer.IsCreated)
				{
					UnsafeUtility.MemCpy(newBuffer.GetUnsafePtr<byte>(), NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer), this.sizeInBytes);
					if (this.m_WeOwnTheBuffer)
					{
						this.m_Buffer.Dispose();
					}
				}
				this.m_Buffer = newBuffer;
				this.m_WeOwnTheBuffer = true;
			}
			InputEvent* eventPtr = (InputEvent*)((byte*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.m_Buffer) + this.m_SizeInBytes);
			eventPtr->sizeInBytes = (uint)sizeInBytes;
			this.m_SizeInBytes += (long)alignedSizeInBytes;
			this.m_EventCount++;
			return eventPtr;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004EDA0 File Offset: 0x0004CFA0
		public unsafe bool Contains(InputEvent* eventPtr)
		{
			if (eventPtr == null)
			{
				return false;
			}
			if (this.sizeInBytes == 0L)
			{
				return false;
			}
			void* bufferPtr = NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<byte>(this.data);
			return eventPtr >= (InputEvent*)bufferPtr && (this.sizeInBytes == -1L || eventPtr < (InputEvent*)((byte*)bufferPtr + this.sizeInBytes));
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0004EDE9 File Offset: 0x0004CFE9
		public void Reset()
		{
			this.m_EventCount = 0;
			if (this.m_SizeInBytes != -1L)
			{
				this.m_SizeInBytes = 0L;
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0004EE04 File Offset: 0x0004D004
		internal unsafe void AdvanceToNextEvent(ref InputEvent* currentReadPos, ref InputEvent* currentWritePos, ref int numEventsRetainedInBuffer, ref int numRemainingEvents, bool leaveEventInBuffer)
		{
			InputEvent* newReadPos = currentReadPos;
			if (numRemainingEvents > 1)
			{
				newReadPos = InputEvent.GetNextInMemory(currentReadPos);
			}
			if (leaveEventInBuffer)
			{
				uint numBytes = currentReadPos.sizeInBytes;
				if (currentReadPos != currentWritePos)
				{
					UnsafeUtility.MemMove(currentWritePos, currentReadPos, (long)((ulong)numBytes));
				}
				currentWritePos += (IntPtr)((UIntPtr)numBytes.AlignToMultipleOf(4U));
				numEventsRetainedInBuffer++;
			}
			currentReadPos = newReadPos;
			numRemainingEvents--;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004EE5D File Offset: 0x0004D05D
		public IEnumerator<InputEventPtr> GetEnumerator()
		{
			return new InputEventBuffer.Enumerator(this);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004EE6F File Offset: 0x0004D06F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004EE77 File Offset: 0x0004D077
		public void Dispose()
		{
			if (!this.m_WeOwnTheBuffer)
			{
				return;
			}
			this.m_Buffer.Dispose();
			this.m_WeOwnTheBuffer = false;
			this.m_SizeInBytes = 0L;
			this.m_EventCount = 0;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004EEA4 File Offset: 0x0004D0A4
		public InputEventBuffer Clone()
		{
			InputEventBuffer clone = default(InputEventBuffer);
			if (this.m_Buffer.IsCreated)
			{
				clone.m_Buffer = new NativeArray<byte>(this.m_Buffer.Length, Allocator.Persistent, NativeArrayOptions.ClearMemory);
				clone.m_Buffer.CopyFrom(this.m_Buffer);
				clone.m_WeOwnTheBuffer = true;
			}
			clone.m_SizeInBytes = this.m_SizeInBytes;
			clone.m_EventCount = this.m_EventCount;
			return clone;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004EF14 File Offset: 0x0004D114
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x040009F4 RID: 2548
		public const long BufferSizeUnknown = -1L;

		// Token: 0x040009F5 RID: 2549
		private NativeArray<byte> m_Buffer;

		// Token: 0x040009F6 RID: 2550
		private long m_SizeInBytes;

		// Token: 0x040009F7 RID: 2551
		private int m_EventCount;

		// Token: 0x040009F8 RID: 2552
		private bool m_WeOwnTheBuffer;

		// Token: 0x020001B5 RID: 437
		private struct Enumerator : IEnumerator<InputEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x06001047 RID: 4167 RVA: 0x0004EF21 File Offset: 0x0004D121
			public Enumerator(InputEventBuffer buffer)
			{
				this.m_Buffer = buffer.bufferPtr;
				this.m_EventCount = buffer.m_EventCount;
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001048 RID: 4168 RVA: 0x0004EF50 File Offset: 0x0004D150
			public bool MoveNext()
			{
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				if (this.m_CurrentEvent == null)
				{
					this.m_CurrentEvent = this.m_Buffer;
					return this.m_CurrentEvent != null;
				}
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				this.m_CurrentEvent = InputEvent.GetNextInMemory(this.m_CurrentEvent);
				return true;
			}

			// Token: 0x06001049 RID: 4169 RVA: 0x0004EFC1 File Offset: 0x0004D1C1
			public void Reset()
			{
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x0600104A RID: 4170 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x17000499 RID: 1177
			// (get) Token: 0x0600104B RID: 4171 RVA: 0x0004EFD2 File Offset: 0x0004D1D2
			public InputEventPtr Current
			{
				get
				{
					return this.m_CurrentEvent;
				}
			}

			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x0600104C RID: 4172 RVA: 0x0004EFDF File Offset: 0x0004D1DF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040009F9 RID: 2553
			private unsafe readonly InputEvent* m_Buffer;

			// Token: 0x040009FA RID: 2554
			private readonly int m_EventCount;

			// Token: 0x040009FB RID: 2555
			private unsafe InputEvent* m_CurrentEvent;

			// Token: 0x040009FC RID: 2556
			private int m_CurrentIndex;
		}
	}
}
