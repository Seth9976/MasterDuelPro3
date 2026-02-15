using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies how and if a drag-and-drop operation should continue.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006E RID: 110
	[ComVisible(true)]
	public enum DragAction
	{
		/// <summary>The operation will continue.</summary>
		// Token: 0x040002DB RID: 731
		Continue,
		/// <summary>The operation will stop with a drop.</summary>
		// Token: 0x040002DC RID: 732
		Drop,
		/// <summary>The operation is canceled with no drop message.</summary>
		// Token: 0x040002DD RID: 733
		Cancel
	}
}
