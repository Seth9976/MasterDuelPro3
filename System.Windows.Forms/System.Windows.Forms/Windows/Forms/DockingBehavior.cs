using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control should be docked by default when added through a designer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006D RID: 109
	public enum DockingBehavior
	{
		/// <summary>Do not prompt the user for the desired docking behavior.</summary>
		// Token: 0x040002D7 RID: 727
		Never,
		/// <summary>Prompt the user for the desired docking behavior.</summary>
		// Token: 0x040002D8 RID: 728
		Ask,
		/// <summary>Set the control's <see cref="P:System.Windows.Forms.Control.Dock" /> property to <see cref="F:System.Windows.Forms.DockStyle.Fill" />  when it is dropped into a container with no other child controls.</summary>
		// Token: 0x040002D9 RID: 729
		AutoDock
	}
}
