using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.DragDrop" />, <see cref="E:System.Windows.Forms.Control.DragEnter" />, or <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000070 RID: 112
	[ComVisible(true)]
	public class DragEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DragEventArgs" /> class.</summary>
		/// <param name="data">The data associated with this event. </param>
		/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys. </param>
		/// <param name="x">The x-coordinate of the mouse cursor in pixels. </param>
		/// <param name="y">The y-coordinate of the mouse cursor in pixels. </param>
		/// <param name="allowedEffect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
		/// <param name="effect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
		// Token: 0x060004F9 RID: 1273 RVA: 0x00013586 File Offset: 0x00011786
		public DragEventArgs(IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffect, DragDropEffects effect)
		{
			this.x = x;
			this.y = y;
			this.keystate = keyState;
			this.allowed_effect = allowedEffect;
			this.current_effect = effect;
			this.data_object = data;
		}

		/// <summary>Gets which drag-and-drop operations are allowed by the originator (or source) of the drag event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000135BB File Offset: 0x000117BB
		public DragDropEffects AllowedEffect
		{
			get
			{
				return this.allowed_effect;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.IDataObject" /> that contains the data associated with this event.</summary>
		/// <returns>The data associated with this event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000135C3 File Offset: 0x000117C3
		public IDataObject Data
		{
			get
			{
				return this.data_object;
			}
		}

		/// <summary>Gets or sets the target drop effect in a drag-and-drop operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000135CB File Offset: 0x000117CB
		public DragDropEffects Effect
		{
			get
			{
				return this.current_effect;
			}
		}

		// Token: 0x040002E5 RID: 741
		internal int x;

		// Token: 0x040002E6 RID: 742
		internal int y;

		// Token: 0x040002E7 RID: 743
		internal int keystate;

		// Token: 0x040002E8 RID: 744
		internal DragDropEffects allowed_effect;

		// Token: 0x040002E9 RID: 745
		internal DragDropEffects current_effect;

		// Token: 0x040002EA RID: 746
		internal IDataObject data_object;
	}
}
