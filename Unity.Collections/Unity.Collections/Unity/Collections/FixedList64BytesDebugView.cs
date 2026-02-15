using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000055 RID: 85
	internal sealed class FixedList64BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00008477 File Offset: 0x00006677
		public FixedList64BytesDebugView(FixedList64Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00008486 File Offset: 0x00006686
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000C6 RID: 198
		private FixedList64Bytes<T> m_List;
	}
}
