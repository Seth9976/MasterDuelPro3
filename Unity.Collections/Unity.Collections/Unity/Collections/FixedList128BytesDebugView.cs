using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000059 RID: 89
	internal sealed class FixedList128BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00008F33 File Offset: 0x00007133
		public FixedList128BytesDebugView(FixedList128Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00008F42 File Offset: 0x00007142
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CA RID: 202
		private FixedList128Bytes<T> m_List;
	}
}
