using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E7 RID: 231
	public class ItemChangedEventArgs : EventArgs
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x00024A4F File Offset: 0x00022C4F
		internal ItemChangedEventArgs(int index)
		{
			this.index = index;
		}

		/// <summary>Indicates the position of the item being changed within the list.</summary>
		/// <returns>The zero-based index to the item being changed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00024A5E File Offset: 0x00022C5E
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04000565 RID: 1381
		private int index;
	}
}
