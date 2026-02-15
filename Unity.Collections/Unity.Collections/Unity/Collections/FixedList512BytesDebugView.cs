using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200005D RID: 93
	internal sealed class FixedList512BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x0600034C RID: 844 RVA: 0x000099EF File Offset: 0x00007BEF
		public FixedList512BytesDebugView(FixedList512Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000099FE File Offset: 0x00007BFE
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CE RID: 206
		private FixedList512Bytes<T> m_List;
	}
}
