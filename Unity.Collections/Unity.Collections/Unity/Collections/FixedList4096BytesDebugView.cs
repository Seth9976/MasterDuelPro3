using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000061 RID: 97
	internal sealed class FixedList4096BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x0600039A RID: 922 RVA: 0x0000A4AB File Offset: 0x000086AB
		public FixedList4096BytesDebugView(FixedList4096Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000A4BA File Offset: 0x000086BA
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D2 RID: 210
		private FixedList4096Bytes<T> m_List;
	}
}
