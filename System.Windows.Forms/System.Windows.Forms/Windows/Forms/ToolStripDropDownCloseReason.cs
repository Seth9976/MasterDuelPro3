using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the reason that a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed.</summary>
	// Token: 0x020001C0 RID: 448
	public enum ToolStripDropDownCloseReason
	{
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because another application has received the focus.</summary>
		// Token: 0x04000BD6 RID: 3030
		AppFocusChange,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because an application was launched.</summary>
		// Token: 0x04000BD7 RID: 3031
		AppClicked,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because one of its items was clicked.</summary>
		// Token: 0x04000BD8 RID: 3032
		ItemClicked,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because of keyboard activity, such as the ESC key being pressed.</summary>
		// Token: 0x04000BD9 RID: 3033
		Keyboard,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because the <see cref="M:System.Windows.Forms.ToolStripDropDown.Close" /> method was called.</summary>
		// Token: 0x04000BDA RID: 3034
		CloseCalled
	}
}
