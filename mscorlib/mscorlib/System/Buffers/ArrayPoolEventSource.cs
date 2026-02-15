using System;
using System.Diagnostics.Tracing;

namespace System.Buffers
{
	// Token: 0x0200077E RID: 1918
	[EventSource(Guid = "0866B2B8-5CEF-5DB9-2612-0C0FFD814A44", Name = "System.Buffers.ArrayPoolEventSource")]
	internal sealed class ArrayPoolEventSource : EventSource
	{
		// Token: 0x06003CDD RID: 15581 RVA: 0x000EA668 File Offset: 0x000E8868
		private ArrayPoolEventSource()
			: base(new Guid(140948152, 23791, 23993, 38, 18, 12, 15, 253, 129, 74, 68), "System.Buffers.ArrayPoolEventSource")
		{
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000EA6AC File Offset: 0x000E88AC
		[Event(1, Level = EventLevel.Verbose)]
		internal unsafe void BufferRented(int bufferId, int bufferSize, int poolId, int bucketId)
		{
			EventSource.EventData* ptr;
			checked
			{
				ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
				ptr->Size = 4;
			}
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr->Reserved = 0;
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[1].Reserved = 0;
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[2].Reserved = 0;
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			ptr[3].Reserved = 0;
			base.WriteEventCore(1, 4, ptr);
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000EA790 File Offset: 0x000E8990
		[Event(2, Level = EventLevel.Informational)]
		internal unsafe void BufferAllocated(int bufferId, int bufferSize, int poolId, int bucketId, ArrayPoolEventSource.BufferAllocatedReason reason)
		{
			EventSource.EventData* ptr;
			checked
			{
				ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData)];
				ptr->Size = 4;
			}
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr->Reserved = 0;
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[1].Reserved = 0;
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[2].Reserved = 0;
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			ptr[3].Reserved = 0;
			ptr[4].Size = 4;
			ptr[4].DataPointer = (IntPtr)((void*)(&reason));
			ptr[4].Reserved = 0;
			base.WriteEventCore(2, 5, ptr);
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000EA8AD File Offset: 0x000E8AAD
		[Event(3, Level = EventLevel.Verbose)]
		internal void BufferReturned(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(3, bufferId, bufferSize, poolId);
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000EA8B9 File Offset: 0x000E8AB9
		[Event(4, Level = EventLevel.Informational)]
		internal void BufferTrimmed(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(4, bufferId, bufferSize, poolId);
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000EA8C5 File Offset: 0x000E8AC5
		[Event(5, Level = EventLevel.Informational)]
		internal void BufferTrimPoll(int milliseconds, int pressure)
		{
			base.WriteEvent(5, milliseconds, pressure);
		}

		// Token: 0x04001F57 RID: 8023
		internal static readonly ArrayPoolEventSource Log = new ArrayPoolEventSource();

		// Token: 0x0200077F RID: 1919
		internal enum BufferAllocatedReason
		{
			// Token: 0x04001F59 RID: 8025
			Pooled,
			// Token: 0x04001F5A RID: 8026
			OverMaximumSize,
			// Token: 0x04001F5B RID: 8027
			PoolExhausted
		}
	}
}
