using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CheckedListBox.ItemCheck" /> event of the <see cref="T:System.Windows.Forms.CheckedListBox" /> and <see cref="T:System.Windows.Forms.ListView" /> controls. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E9 RID: 233
	[ComVisible(true)]
	public class ItemCheckEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> class.</summary>
		/// <param name="index">The zero-based index of the item to change. </param>
		/// <param name="newCheckValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether to change the check box for the item to be checked, unchecked, or indeterminate. </param>
		/// <param name="currentValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether the check box for the item is currently checked, unchecked, or indeterminate. </param>
		// Token: 0x06000875 RID: 2165 RVA: 0x00024A66 File Offset: 0x00022C66
		public ItemCheckEventArgs(int index, CheckState newCheckValue, CheckState currentValue)
		{
			this.index = index;
			this.newValue = newCheckValue;
			this.currentValue = currentValue;
		}

		// Token: 0x04000566 RID: 1382
		private CheckState currentValue;

		// Token: 0x04000567 RID: 1383
		private int index;

		// Token: 0x04000568 RID: 1384
		private CheckState newValue;
	}
}
