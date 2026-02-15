using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the button style within a toolbar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B5 RID: 437
	public enum ToolBarButtonStyle
	{
		/// <summary>A standard, three-dimensional button.</summary>
		// Token: 0x04000B85 RID: 2949
		PushButton = 1,
		/// <summary>A toggle button that appears sunken when clicked and retains the sunken appearance until clicked again.</summary>
		// Token: 0x04000B86 RID: 2950
		ToggleButton,
		/// <summary>A space or line between toolbar buttons. The appearance depends on the value of the <see cref="P:System.Windows.Forms.ToolBar.Appearance" /> property.</summary>
		// Token: 0x04000B87 RID: 2951
		Separator,
		/// <summary>A drop-down control that displays a menu or other window when clicked.</summary>
		// Token: 0x04000B88 RID: 2952
		DropDownButton
	}
}
