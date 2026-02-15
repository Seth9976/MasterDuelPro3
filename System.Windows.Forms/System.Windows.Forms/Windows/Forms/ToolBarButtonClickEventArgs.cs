using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolBar.ButtonClick" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B3 RID: 435
	public class ToolBarButtonClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> class.</summary>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked. </param>
		// Token: 0x06001299 RID: 4761 RVA: 0x0005F801 File Offset: 0x0005DA01
		public ToolBarButtonClickEventArgs(ToolBarButton button)
		{
			this.button = button;
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolBarButton" /> that was clicked.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0005F810 File Offset: 0x0005DA10
		public ToolBarButton Button
		{
			get
			{
				return this.button;
			}
		}

		// Token: 0x04000B83 RID: 2947
		private ToolBarButton button;
	}
}
