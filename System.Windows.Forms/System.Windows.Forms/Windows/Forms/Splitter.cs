using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a splitter control that enables the user to resize docked controls. <see cref="T:System.Windows.Forms.Splitter" /> has been replaced by <see cref="T:System.Windows.Forms.SplitContainer" /> and is provided only for compatibility with previous versions.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018C RID: 396
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("SplitterMoved")]
	[Designer("System.Windows.Forms.Design.SplitterDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Dock")]
	public class Splitter : Control
	{
		// Token: 0x04000A2D RID: 2605
		private static Cursor splitter_ns = Cursors.HSplit;

		// Token: 0x04000A2E RID: 2606
		private static Cursor splitter_we = Cursors.VSplit;

		// Token: 0x04000A2F RID: 2607
		private static object SplitterMovedEvent = new object();

		// Token: 0x04000A30 RID: 2608
		private static object SplitterMovingEvent = new object();
	}
}
