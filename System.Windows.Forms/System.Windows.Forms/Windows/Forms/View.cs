using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how list items are displayed in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020F RID: 527
	public enum View
	{
		/// <summary>Each item appears as a full-sized icon with a label below it.</summary>
		// Token: 0x04000D6F RID: 3439
		LargeIcon,
		/// <summary>Each item appears on a separate line with further information about each item arranged in columns. The left-most column contains a small icon and label, and subsequent columns contain sub items as specified by the application. A column displays a header which can display a caption for the column. The user can resize each column at run time.</summary>
		// Token: 0x04000D70 RID: 3440
		Details,
		/// <summary>Each item appears as a small icon with a label to its right.</summary>
		// Token: 0x04000D71 RID: 3441
		SmallIcon,
		/// <summary>Each item appears as a small icon with a label to its right. Items are arranged in columns with no column headers.</summary>
		// Token: 0x04000D72 RID: 3442
		List,
		/// <summary>Each item appears as a full-sized icon with the item label and subitem information to the right of it. The subitem information that appears is specified by the application. This view is available only on Windows XP and the Windows Server 2003 family. On earlier operating systems, this value is ignored and the <see cref="T:System.Windows.Forms.ListView" /> control displays in the <see cref="F:System.Windows.Forms.View.LargeIcon" /> view.</summary>
		// Token: 0x04000D73 RID: 3443
		Tile
	}
}
