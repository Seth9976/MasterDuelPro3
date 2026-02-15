using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows horizontal scroll bar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C0 RID: 192
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class HScrollBar : ScrollBar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HScrollBar" /> class. </summary>
		// Token: 0x06000772 RID: 1906 RVA: 0x00020DB7 File Offset: 0x0001EFB7
		public HScrollBar()
		{
			this.vert = false;
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00020DC6 File Offset: 0x0001EFC6
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.HScrollBarDefaultSize;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00020DD2 File Offset: 0x0001EFD2
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}
	}
}
