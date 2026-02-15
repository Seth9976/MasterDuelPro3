using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> and <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000055 RID: 85
	public class ControlEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ControlEventArgs" /> class for the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to store in this event. </param>
		// Token: 0x06000437 RID: 1079 RVA: 0x00010882 File Offset: 0x0000EA82
		public ControlEventArgs(Control control)
		{
			this.control = control;
		}

		/// <summary>Gets the control object used by this event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> used by this event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00010891 File Offset: 0x0000EA91
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x0400022B RID: 555
		private Control control;
	}
}
