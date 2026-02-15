using System;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates properties related to scrolling. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000182 RID: 386
	public abstract class ScrollProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollProperties" /> class. </summary>
		/// <param name="container">The <see cref="T:System.Windows.Forms.ScrollableControl" /> whose scrolling properties this object describes.</param>
		// Token: 0x06000E93 RID: 3731 RVA: 0x00042C4E File Offset: 0x00040E4E
		protected ScrollProperties(ScrollableControl container)
		{
			this.parentControl = container;
		}

		// Token: 0x04000963 RID: 2403
		private ScrollableControl parentControl;

		// Token: 0x04000964 RID: 2404
		internal ScrollBar scroll_bar;
	}
}
