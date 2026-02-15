using System;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200009B RID: 155
	internal sealed class ArraySliceDebugView<T> where T : struct
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0001A2C5 File Offset: 0x000184C5
		public ArraySliceDebugView(ArraySlice<T> slice)
		{
			this.m_Slice = slice;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001A2D4 File Offset: 0x000184D4
		public T[] Items
		{
			get
			{
				return this.m_Slice.ToArray();
			}
		}

		// Token: 0x040002F4 RID: 756
		private ArraySlice<T> m_Slice;
	}
}
