using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Specifies controls that are visible in the designer.</summary>
	// Token: 0x02000395 RID: 917
	[Flags]
	public enum ToolStripItemDesignerAvailability
	{
		/// <summary>Specifies that no controls are visible.</summary>
		// Token: 0x04001CDA RID: 7386
		None = 0,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.ToolStrip" /> is visible.</summary>
		// Token: 0x04001CDB RID: 7387
		ToolStrip = 1,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.MenuStrip" /> is visible.</summary>
		// Token: 0x04001CDC RID: 7388
		MenuStrip = 2,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.ContextMenuStrip" /> is visible.</summary>
		// Token: 0x04001CDD RID: 7389
		ContextMenuStrip = 4,
		/// <summary>Specifies that <see cref="T:System.Windows.Forms.StatusStrip" /> is visible.</summary>
		// Token: 0x04001CDE RID: 7390
		StatusStrip = 8,
		/// <summary>Specifies that all controls are visible.</summary>
		// Token: 0x04001CDF RID: 7391
		All = 15
	}
}
