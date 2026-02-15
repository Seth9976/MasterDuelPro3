using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the list changed.</summary>
	// Token: 0x0200028A RID: 650
	public enum ListChangedType
	{
		/// <summary>Much of the list has changed. Any listening controls should refresh all their data from the list.</summary>
		// Token: 0x04000A0C RID: 2572
		Reset,
		/// <summary>An item added to the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was added.</summary>
		// Token: 0x04000A0D RID: 2573
		ItemAdded,
		/// <summary>An item deleted from the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was deleted.</summary>
		// Token: 0x04000A0E RID: 2574
		ItemDeleted,
		/// <summary>An item moved within the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.OldIndex" /> contains the previous index for the item, whereas <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the new index for the item.</summary>
		// Token: 0x04000A0F RID: 2575
		ItemMoved,
		/// <summary>An item changed in the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was changed.</summary>
		// Token: 0x04000A10 RID: 2576
		ItemChanged,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was added, which changed the schema.</summary>
		// Token: 0x04000A11 RID: 2577
		PropertyDescriptorAdded,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was deleted, which changed the schema.</summary>
		// Token: 0x04000A12 RID: 2578
		PropertyDescriptorDeleted,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was changed, which changed the schema.</summary>
		// Token: 0x04000A13 RID: 2579
		PropertyDescriptorChanged
	}
}
