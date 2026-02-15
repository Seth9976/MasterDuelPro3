using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.GiveFeedback" /> event, which occurs during a drag operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BC RID: 188
	[ComVisible(true)]
	public class GiveFeedbackEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> class.</summary>
		/// <param name="effect">The type of drag-and-drop operation. Possible values are obtained by applying the bitwise OR (|) operation to the constants defined in the <see cref="T:System.Windows.Forms.DragDropEffects" />. </param>
		/// <param name="useDefaultCursors">true if default pointers are used; otherwise, false. </param>
		// Token: 0x0600075A RID: 1882 RVA: 0x000207EE File Offset: 0x0001E9EE
		public GiveFeedbackEventArgs(DragDropEffects effect, bool useDefaultCursors)
		{
			this.effect = effect;
			this.use_default_cursors = useDefaultCursors;
		}

		/// <summary>Gets or sets whether drag operation should use the default cursors that are associated with drag-drop effects.</summary>
		/// <returns>true if the default pointers are used; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00020804 File Offset: 0x0001EA04
		public bool UseDefaultCursors
		{
			get
			{
				return this.use_default_cursors;
			}
		}

		// Token: 0x040004BE RID: 1214
		internal DragDropEffects effect;

		// Token: 0x040004BF RID: 1215
		internal bool use_default_cursors;
	}
}
