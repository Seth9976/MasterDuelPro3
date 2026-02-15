using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a shortcut menu. </summary>
	// Token: 0x0200004C RID: 76
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("Opening")]
	public class ContextMenuStrip : ToolStripDropDownMenu
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000989C File Offset: 0x00007A9C
		internal void SetSourceControl(Control source_control)
		{
			this.source_control = source_control;
			this.container = source_control;
		}

		// Token: 0x04000190 RID: 400
		private Control source_control;

		// Token: 0x04000191 RID: 401
		internal Control container;
	}
}
