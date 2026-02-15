using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EF RID: 239
	[ComVisible(true)]
	public class KeyEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.KeyEventArgs" /> class.</summary>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> representing the key that was pressed, combined with any modifier flags that indicate which CTRL, SHIFT, and ALT keys were pressed at the same time. Possible values are obtained be applying the bitwise OR (|) operator to constants from the <see cref="T:System.Windows.Forms.Keys" /> enumeration. </param>
		// Token: 0x0600087E RID: 2174 RVA: 0x00024AA8 File Offset: 0x00022CA8
		public KeyEventArgs(Keys keyData)
		{
			this.key_data = keyData;
			this.event_handled = false;
		}

		/// <summary>Gets a value indicating whether the ALT key was pressed.</summary>
		/// <returns>true if the ALT key was pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00024ABE File Offset: 0x00022CBE
		public virtual bool Alt
		{
			get
			{
				return (this.key_data & Keys.Alt) != Keys.None;
			}
		}

		/// <summary>Gets a value indicating whether the CTRL key was pressed.</summary>
		/// <returns>true if the CTRL key was pressed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00024AD1 File Offset: 0x00022CD1
		public bool Control
		{
			get
			{
				return (this.key_data & Keys.Control) != Keys.None;
			}
		}

		/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
		/// <returns>true to bypass the control's default handling; otherwise, false to also pass the event along to the default control handler.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00024AE4 File Offset: 0x00022CE4
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00024AEC File Offset: 0x00022CEC
		public bool Handled
		{
			get
			{
				return this.event_handled;
			}
			set
			{
				this.event_handled = value;
			}
		}

		/// <summary>Gets the keyboard code for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> value that is the key code for the event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00024AF5 File Offset: 0x00022CF5
		public Keys KeyCode
		{
			get
			{
				return this.key_data & Keys.KeyCode;
			}
		}

		/// <summary>Gets the key data for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> representing the key code for the key that was pressed, combined with modifier flags that indicate which combination of CTRL, SHIFT, and ALT keys was pressed at the same time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00024B03 File Offset: 0x00022D03
		public Keys KeyData
		{
			get
			{
				return this.key_data;
			}
		}

		/// <summary>Gets the modifier flags for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event. The flags indicate which combination of CTRL, SHIFT, and ALT keys was pressed.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Keys" /> value representing one or more modifier flags.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00024B0B File Offset: 0x00022D0B
		public Keys Modifiers
		{
			get
			{
				return this.key_data & Keys.Modifiers;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key event should be passed on to the underlying control.</summary>
		/// <returns>true if the key event should not be sent to the control; otherwise, false.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00024B19 File Offset: 0x00022D19
		public bool SuppressKeyPress
		{
			get
			{
				return this.supress_key_press;
			}
		}

		// Token: 0x0400056C RID: 1388
		private Keys key_data;

		// Token: 0x0400056D RID: 1389
		private bool event_handled;

		// Token: 0x0400056E RID: 1390
		private bool supress_key_press;
	}
}
