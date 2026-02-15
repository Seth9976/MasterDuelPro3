using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005D RID: 93
	public sealed class ListChangedEventArgs<T> : EventArgs
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00008F82 File Offset: 0x00007182
		public ListChangedEventArgs(int index, T item)
		{
			this.index = index;
			this.item = item;
		}

		// Token: 0x04000137 RID: 311
		public readonly int index;

		// Token: 0x04000138 RID: 312
		public readonly T item;
	}
}
