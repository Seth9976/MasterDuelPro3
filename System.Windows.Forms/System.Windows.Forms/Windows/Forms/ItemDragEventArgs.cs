using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemDrag" /> event of the <see cref="T:System.Windows.Forms.ListView" /> and <see cref="T:System.Windows.Forms.TreeView" /> controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000ED RID: 237
	[ComVisible(true)]
	public class ItemDragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemDragEventArgs" /> class with a specified mouse button and the item that is being dragged.</summary>
		/// <param name="button">A bitwise combination of <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicates which mouse buttons were pressed. </param>
		/// <param name="item">The item being dragged. </param>
		// Token: 0x0600087B RID: 2171 RVA: 0x00024A92 File Offset: 0x00022C92
		public ItemDragEventArgs(MouseButtons button, object item)
		{
			this.button = button;
			this.item = item;
		}

		// Token: 0x0400056A RID: 1386
		private MouseButtons button;

		// Token: 0x0400056B RID: 1387
		private object item;
	}
}
