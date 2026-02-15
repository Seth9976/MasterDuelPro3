using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolTip.Popup" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000163 RID: 355
	public class PopupEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.PopupEventArgs" /> class.</summary>
		/// <param name="associatedWindow">The <see cref="T:System.Windows.Forms.IWin32Window" /> that the ToolTip is bound to.</param>
		/// <param name="associatedControl">The <see cref="T:System.Windows.Forms.Control" /> that the ToolTip is being created for.</param>
		/// <param name="isBalloon">true to indicate that the associated ToolTip window has a balloon-style appearance; otherwise, false to indicate that the ToolTip window has a standard rectangular appearance.</param>
		/// <param name="size">The <see cref="T:System.Drawing.Size" /> of the ToolTip.</param>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003BBDA File Offset: 0x00039DDA
		public PopupEventArgs(IWin32Window associatedWindow, Control associatedControl, bool isBalloon, Size size)
		{
			this.associated_window = associatedWindow;
			this.associated_control = associatedControl;
			this.is_balloon = isBalloon;
			this.tool_tip_size = size;
		}

		/// <summary>Gets or sets the size of the ToolTip.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolTip" /> window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0003BBFF File Offset: 0x00039DFF
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x0003BC07 File Offset: 0x00039E07
		public Size ToolTipSize
		{
			get
			{
				return this.tool_tip_size;
			}
			set
			{
				this.tool_tip_size = value;
			}
		}

		// Token: 0x04000893 RID: 2195
		private Control associated_control;

		// Token: 0x04000894 RID: 2196
		private IWin32Window associated_window;

		// Token: 0x04000895 RID: 2197
		private bool is_balloon;

		// Token: 0x04000896 RID: 2198
		private Size tool_tip_size;
	}
}
