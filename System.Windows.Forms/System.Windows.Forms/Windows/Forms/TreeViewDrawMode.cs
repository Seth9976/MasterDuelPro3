using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the ways a <see cref="T:System.Windows.Forms.TreeView" /> can be drawn.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000205 RID: 517
	public enum TreeViewDrawMode
	{
		/// <summary>The <see cref="T:System.Windows.Forms.TreeView" /> is drawn by the operating system.</summary>
		// Token: 0x04000D59 RID: 3417
		Normal,
		/// <summary>The label portion of the <see cref="T:System.Windows.Forms.TreeView" /> nodes are drawn manually. Other node elements are drawn by the operating system, including icons, checkboxes, plus and minus signs, and lines connecting the nodes.</summary>
		// Token: 0x04000D5A RID: 3418
		OwnerDrawText,
		/// <summary>All elements of a <see cref="T:System.Windows.Forms.TreeView" /> node are drawn manually, including icons, checkboxes, plus and minus signs, and lines connecting the nodes.</summary>
		// Token: 0x04000D5B RID: 3419
		OwnerDrawAll
	}
}
