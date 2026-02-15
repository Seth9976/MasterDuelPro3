using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how items align in the <see cref="T:System.Windows.Forms.ListView" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000115 RID: 277
	public enum ListViewAlignment
	{
		/// <summary>When the user moves an item, it remains where it is dropped.</summary>
		// Token: 0x0400072A RID: 1834
		Default,
		/// <summary>Items are aligned to the left of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x0400072B RID: 1835
		Left,
		/// <summary>Items are aligned to the top of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x0400072C RID: 1836
		Top,
		/// <summary>Items are aligned to an invisible grid in the control. When the user moves an item, it moves to the closest juncture in the grid.</summary>
		// Token: 0x0400072D RID: 1837
		SnapToGrid = 5
	}
}
