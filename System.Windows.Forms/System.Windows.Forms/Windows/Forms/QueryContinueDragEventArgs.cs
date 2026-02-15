using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016B RID: 363
	[ComVisible(true)]
	public class QueryContinueDragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> class.</summary>
		/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys. </param>
		/// <param name="escapePressed">true if the ESC key was pressed; otherwise, false. </param>
		/// <param name="action">A <see cref="T:System.Windows.Forms.DragAction" /> value. </param>
		// Token: 0x06000DFD RID: 3581 RVA: 0x0003F1CA File Offset: 0x0003D3CA
		public QueryContinueDragEventArgs(int keyState, bool escapePressed, DragAction action)
		{
			this.key_state = keyState;
			this.escape_pressed = escapePressed;
			this.drag_action = action;
		}

		/// <summary>Gets or sets the status of a drag-and-drop operation.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DragAction" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0003F1E7 File Offset: 0x0003D3E7
		public DragAction Action
		{
			get
			{
				return this.drag_action;
			}
		}

		// Token: 0x040008E2 RID: 2274
		internal int key_state;

		// Token: 0x040008E3 RID: 2275
		internal bool escape_pressed;

		// Token: 0x040008E4 RID: 2276
		internal DragAction drag_action;
	}
}
