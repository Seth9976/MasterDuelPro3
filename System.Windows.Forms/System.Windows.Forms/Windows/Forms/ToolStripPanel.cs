using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Creates a container within which other controls can share horizontal or vertical space.</summary>
	// Token: 0x020001E6 RID: 486
	[ComVisible(true)]
	[ToolboxBitmap("")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolStripPanel : ContainerControl
	{
		// Token: 0x04000C87 RID: 3207
		private static object RendererChangedEvent = new object();
	}
}
