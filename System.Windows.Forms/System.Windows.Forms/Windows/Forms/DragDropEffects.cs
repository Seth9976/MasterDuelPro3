using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the possible effects of a drag-and-drop operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006F RID: 111
	[Flags]
	public enum DragDropEffects
	{
		/// <summary>The drop target does not accept the data.</summary>
		// Token: 0x040002DF RID: 735
		None = 0,
		/// <summary>The data from the drag source is copied to the drop target.</summary>
		// Token: 0x040002E0 RID: 736
		Copy = 1,
		/// <summary>The data from the drag source is moved to the drop target.</summary>
		// Token: 0x040002E1 RID: 737
		Move = 2,
		/// <summary>The data from the drag source is linked to the drop target.</summary>
		// Token: 0x040002E2 RID: 738
		Link = 4,
		/// <summary>The target can be scrolled while dragging to locate a drop position that is not currently visible in the target.</summary>
		// Token: 0x040002E3 RID: 739
		Scroll = -2147483648,
		/// <summary>The combination of the <see cref="F:System.Windows.DragDropEffects.Copy" />, <see cref="F:System.Windows.Forms.DragDropEffects.Move" />, and <see cref="F:System.Windows.Forms.DragDropEffects.Scroll" /> effects.</summary>
		// Token: 0x040002E4 RID: 740
		All = -2147483645
	}
}
