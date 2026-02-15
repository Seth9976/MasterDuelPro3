using System;

namespace System.Windows.Forms
{
	/// <summary>Provides basic properties for the <see cref="T:System.Windows.Forms.VScrollBar" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020D RID: 525
	public class VScrollProperties : ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.VScrollProperties" /> class. </summary>
		/// <param name="container">A <see cref="T:System.Windows.Forms.ScrollableControl" /> that contains the scroll bar.</param>
		// Token: 0x06001651 RID: 5713 RVA: 0x0006FB95 File Offset: 0x0006DD95
		public VScrollProperties(ScrollableControl container)
			: base(container)
		{
			this.scroll_bar = container.vscrollbar;
		}
	}
}
