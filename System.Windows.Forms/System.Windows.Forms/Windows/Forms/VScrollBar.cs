using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows vertical scroll bar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020C RID: 524
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class VScrollBar : ScrollBar
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VScrollBar" /> class. </summary>
		// Token: 0x0600164D RID: 5709 RVA: 0x0006FB7A File Offset: 0x0006DD7A
		public VScrollBar()
		{
			this.vert = true;
		}

		/// <summary>Gets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.RightToLeft.No" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0003B96F File Offset: 0x00039B6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0006FB89 File Offset: 0x0006DD89
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.VScrollBarDefaultSize;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00020DD2 File Offset: 0x0001EFD2
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}
	}
}
