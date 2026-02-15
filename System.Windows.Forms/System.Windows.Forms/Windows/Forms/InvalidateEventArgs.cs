using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E3 RID: 227
	public class InvalidateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> class.</summary>
		/// <param name="invalidRect">The <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area. </param>
		// Token: 0x0600086D RID: 2157 RVA: 0x00024A38 File Offset: 0x00022C38
		public InvalidateEventArgs(Rectangle invalidRect)
		{
			this.invalidated_rectangle = invalidRect;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area.</summary>
		/// <returns>The invalidated window area.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00024A47 File Offset: 0x00022C47
		public Rectangle InvalidRect
		{
			get
			{
				return this.invalidated_rectangle;
			}
		}

		// Token: 0x0400055B RID: 1371
		private Rectangle invalidated_rectangle;
	}
}
