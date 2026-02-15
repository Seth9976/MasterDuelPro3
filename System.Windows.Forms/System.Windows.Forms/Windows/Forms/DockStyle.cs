using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position and manner in which a control is docked.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006B RID: 107
	[Editor("System.Windows.Forms.Design.DockEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum DockStyle
	{
		/// <summary>The control is not docked.</summary>
		// Token: 0x040002CE RID: 718
		None,
		/// <summary>The control's top edge is docked to the top of its containing control.</summary>
		// Token: 0x040002CF RID: 719
		Top,
		/// <summary>The control's bottom edge is docked to the bottom of its containing control.</summary>
		// Token: 0x040002D0 RID: 720
		Bottom,
		/// <summary>The control's left edge is docked to the left edge of its containing control.</summary>
		// Token: 0x040002D1 RID: 721
		Left,
		/// <summary>The control's right edge is docked to the right edge of its containing control.</summary>
		// Token: 0x040002D2 RID: 722
		Right,
		/// <summary>All the control's edges are docked to the all edges of its containing control and sized appropriately.</summary>
		// Token: 0x040002D3 RID: 723
		Fill
	}
}
