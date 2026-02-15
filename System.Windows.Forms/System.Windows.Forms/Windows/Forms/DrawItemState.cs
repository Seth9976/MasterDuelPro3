using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the state of an item that is being drawn.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000074 RID: 116
	[Flags]
	public enum DrawItemState
	{
		/// <summary>The item currently has no state.</summary>
		// Token: 0x040002F3 RID: 755
		None = 0,
		/// <summary>The item is selected.</summary>
		// Token: 0x040002F4 RID: 756
		Selected = 1,
		/// <summary>The item is grayed. Only menu controls use this value.</summary>
		// Token: 0x040002F5 RID: 757
		Grayed = 2,
		/// <summary>The item is unavailable.</summary>
		// Token: 0x040002F6 RID: 758
		Disabled = 4,
		/// <summary>The item is checked. Only menu controls use this value.</summary>
		// Token: 0x040002F7 RID: 759
		Checked = 8,
		/// <summary>The item has focus.</summary>
		// Token: 0x040002F8 RID: 760
		Focus = 16,
		/// <summary>The item is in its default visual state.</summary>
		// Token: 0x040002F9 RID: 761
		Default = 32,
		/// <summary>The item is being hot-tracked, that is, the item is highlighted as the mouse pointer passes over it.</summary>
		// Token: 0x040002FA RID: 762
		HotLight = 64,
		/// <summary>The item is inactive.</summary>
		// Token: 0x040002FB RID: 763
		Inactive = 128,
		/// <summary>The item displays without a keyboard accelerator.</summary>
		// Token: 0x040002FC RID: 764
		NoAccelerator = 256,
		/// <summary>The item displays without the visual cue that indicates it has focus.</summary>
		// Token: 0x040002FD RID: 765
		NoFocusRect = 512,
		/// <summary>The item is the editing portion of a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
		// Token: 0x040002FE RID: 766
		ComboBoxEdit = 4096
	}
}
