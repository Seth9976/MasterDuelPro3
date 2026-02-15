using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event.</summary>
	// Token: 0x02000288 RID: 648
	public class ListChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the index of the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added, changed, or removed.</param>
		// Token: 0x06000F62 RID: 3938 RVA: 0x0004203A File Offset: 0x0004023A
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex)
			: this(listChangedType, newIndex, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change, the index of the affected item, and a <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added or changed.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the item.</param>
		// Token: 0x06000F63 RID: 3939 RVA: 0x00042045 File Offset: 0x00040245
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc)
			: this(listChangedType, newIndex)
		{
			this.<PropertyDescriptor>k__BackingField = propDesc;
			this.OldIndex = newIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, removed, or changed.</param>
		// Token: 0x06000F64 RID: 3940 RVA: 0x0004205D File Offset: 0x0004025D
		public ListChangedEventArgs(ListChangedType listChangedType, PropertyDescriptor propDesc)
		{
			this.ListChangedType = listChangedType;
			this.<PropertyDescriptor>k__BackingField = propDesc;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the old and new index of the item that was moved.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The new index of the item that was moved.</param>
		/// <param name="oldIndex">The old index of the item that was moved.</param>
		// Token: 0x06000F65 RID: 3941 RVA: 0x00042073 File Offset: 0x00040273
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.ListChangedType = listChangedType;
			this.NewIndex = newIndex;
			this.OldIndex = oldIndex;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00042090 File Offset: 0x00040290
		public ListChangedType ListChangedType { get; }

		/// <summary>Gets the index of the item affected by the change.</summary>
		/// <returns>The index of the affected by the change.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00042098 File Offset: 0x00040298
		public int NewIndex { get; }

		/// <summary>Gets the old index of an item that has been moved.</summary>
		/// <returns>The old index of the moved item.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x000420A0 File Offset: 0x000402A0
		public int OldIndex { get; }
	}
}
