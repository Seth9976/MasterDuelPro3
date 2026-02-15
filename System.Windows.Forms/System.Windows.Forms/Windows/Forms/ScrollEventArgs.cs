using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the Scroll event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017E RID: 382
	[ComVisible(true)]
	public class ScrollEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" /> and <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
		/// <param name="newValue">The new value for the scroll bar. </param>
		// Token: 0x06000E8D RID: 3725 RVA: 0x00042C0C File Offset: 0x00040E0C
		public ScrollEventArgs(ScrollEventType type, int newValue)
			: this(type, -1, newValue, ScrollOrientation.HorizontalScroll)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollEventArgs" /> class using the given values for the <see cref="P:System.Windows.Forms.ScrollEventArgs.Type" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.OldValue" />, <see cref="P:System.Windows.Forms.ScrollEventArgs.NewValue" />, and <see cref="P:System.Windows.Forms.ScrollEventArgs.ScrollOrientation" /> properties.</summary>
		/// <param name="type">One of the <see cref="T:System.Windows.Forms.ScrollEventType" /> values. </param>
		/// <param name="oldValue">The old value for the scroll bar. </param>
		/// <param name="newValue">The new value for the scroll bar. </param>
		/// <param name="scroll">One of the <see cref="T:System.Windows.Forms.ScrollOrientation" /> values. </param>
		// Token: 0x06000E8E RID: 3726 RVA: 0x00042C18 File Offset: 0x00040E18
		public ScrollEventArgs(ScrollEventType type, int oldValue, int newValue, ScrollOrientation scroll)
		{
			this.new_value = newValue;
			this.old_value = oldValue;
			this.scroll_orientation = scroll;
			this.type = type;
		}

		/// <summary>Gets or sets the new <see cref="P:System.Windows.Forms.ScrollBar.Value" /> of the scroll bar.</summary>
		/// <returns>The numeric value that the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property will be changed to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00042C3D File Offset: 0x00040E3D
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x00042C45 File Offset: 0x00040E45
		public int NewValue
		{
			get
			{
				return this.new_value;
			}
			set
			{
				this.new_value = value;
			}
		}

		// Token: 0x04000952 RID: 2386
		private ScrollEventType type;

		// Token: 0x04000953 RID: 2387
		private int new_value;

		// Token: 0x04000954 RID: 2388
		private int old_value;

		// Token: 0x04000955 RID: 2389
		private ScrollOrientation scroll_orientation;
	}
}
