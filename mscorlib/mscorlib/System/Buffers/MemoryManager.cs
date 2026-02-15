using System;

namespace System.Buffers
{
	// Token: 0x02000785 RID: 1925
	public abstract class MemoryManager<T>
	{
		// Token: 0x06003CF1 RID: 15601
		public abstract Span<T> GetSpan();

		// Token: 0x06003CF2 RID: 15602
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000EAC9B File Offset: 0x000E8E9B
		protected internal virtual bool TryGetArray(out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}
	}
}
