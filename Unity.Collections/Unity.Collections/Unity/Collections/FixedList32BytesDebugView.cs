using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000051 RID: 81
	internal sealed class FixedList32BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000079BB File Offset: 0x00005BBB
		public FixedList32BytesDebugView(FixedList32Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000079CA File Offset: 0x00005BCA
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000C2 RID: 194
		private FixedList32Bytes<T> m_List;
	}
}
