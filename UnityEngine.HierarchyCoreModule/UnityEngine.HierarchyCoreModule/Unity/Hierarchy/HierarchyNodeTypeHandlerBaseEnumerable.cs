using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Unity.Hierarchy
{
	// Token: 0x0200000D RID: 13
	public readonly struct HierarchyNodeTypeHandlerBaseEnumerable
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002819 File Offset: 0x00000A19
		internal HierarchyNodeTypeHandlerBaseEnumerable(Hierarchy hierarchy)
		{
			this.m_Hierarchy = hierarchy;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002823 File Offset: 0x00000A23
		public HierarchyNodeTypeHandlerBaseEnumerable.Enumerator GetEnumerator()
		{
			return new HierarchyNodeTypeHandlerBaseEnumerable.Enumerator(this.m_Hierarchy);
		}

		// Token: 0x0400001E RID: 30
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x0200000E RID: 14
		public struct Enumerator : IDisposable
		{
			// Token: 0x06000034 RID: 52 RVA: 0x00002830 File Offset: 0x00000A30
			internal Enumerator(Hierarchy hierarchy)
			{
				this.m_Handlers = MemoryPool<IntPtr>.Shared.Rent(hierarchy.GetNodeTypeHandlersBaseCount());
				this.m_Count = hierarchy.GetNodeTypeHandlersBaseSpan(this.m_Handlers.Memory.Span);
				this.m_Index = -1;
			}

			// Token: 0x06000035 RID: 53 RVA: 0x0000287A File Offset: 0x00000A7A
			public void Dispose()
			{
				this.m_Handlers.Dispose();
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000036 RID: 54 RVA: 0x0000288C File Offset: 0x00000A8C
			public unsafe HierarchyNodeTypeHandlerBase Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return HierarchyNodeTypeHandlerBase.FromIntPtr(*this.m_Handlers.Memory.Span[this.m_Index]);
				}
			}

			// Token: 0x06000037 RID: 55 RVA: 0x000028C0 File Offset: 0x00000AC0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Count;
			}

			// Token: 0x0400001F RID: 31
			private readonly IMemoryOwner<IntPtr> m_Handlers;

			// Token: 0x04000020 RID: 32
			private readonly int m_Count;

			// Token: 0x04000021 RID: 33
			private int m_Index;
		}
	}
}
