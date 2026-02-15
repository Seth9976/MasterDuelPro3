using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the center panel of a <see cref="T:System.Windows.Forms.ToolStripContainer" /> control.</summary>
	// Token: 0x020001BC RID: 444
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("Load")]
	[ToolboxItem(false)]
	[Docking(DockingBehavior.Never)]
	[InitializationEvent("Load")]
	[Designer("System.Windows.Forms.Design.ToolStripContentPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolStripContentPanel : Panel
	{
		// Token: 0x04000BBE RID: 3006
		private static object LoadEvent = new object();

		// Token: 0x04000BBF RID: 3007
		private static object RendererChangedEvent = new object();
	}
}
