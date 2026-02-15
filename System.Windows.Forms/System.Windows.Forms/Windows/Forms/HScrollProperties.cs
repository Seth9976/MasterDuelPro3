using System;

namespace System.Windows.Forms
{
	/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.HScrollBar" /></summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C1 RID: 193
	public class HScrollProperties : ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HScrollProperties" /> class. </summary>
		/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
		// Token: 0x06000775 RID: 1909 RVA: 0x00020DDA File Offset: 0x0001EFDA
		public HScrollProperties(ScrollableControl container)
			: base(container)
		{
			this.scroll_bar = container.hscrollbar;
		}
	}
}
