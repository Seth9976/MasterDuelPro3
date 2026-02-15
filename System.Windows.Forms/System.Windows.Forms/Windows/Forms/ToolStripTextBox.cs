using System;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a text box in a <see cref="T:System.Windows.Forms.ToolStrip" /> that allows the user to enter text.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F1 RID: 497
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripTextBox : ToolStripControlHost
	{
		// Token: 0x04000CAB RID: 3243
		private static object AcceptsTabChangedEvent = new object();

		// Token: 0x04000CAC RID: 3244
		private static object BorderStyleChangedEvent = new object();

		// Token: 0x04000CAD RID: 3245
		private static object HideSelectionChangedEvent = new object();

		// Token: 0x04000CAE RID: 3246
		private static object ModifiedChangedEvent = new object();

		// Token: 0x04000CAF RID: 3247
		private static object MultilineChangedEvent = new object();

		// Token: 0x04000CB0 RID: 3248
		private static object ReadOnlyChangedEvent = new object();

		// Token: 0x04000CB1 RID: 3249
		private static object TextBoxTextAlignChangedEvent = new object();
	}
}
