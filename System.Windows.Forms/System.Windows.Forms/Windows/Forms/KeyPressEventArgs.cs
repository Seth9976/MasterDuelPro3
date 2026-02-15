using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F1 RID: 241
	[ComVisible(true)]
	public class KeyPressEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> class.</summary>
		/// <param name="keyChar">The ASCII character corresponding to the key the user pressed. </param>
		// Token: 0x06000889 RID: 2185 RVA: 0x00024B21 File Offset: 0x00022D21
		public KeyPressEventArgs(char keyChar)
		{
			this.key_char = keyChar;
			this.event_handled = false;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event was handled.</summary>
		/// <returns>true if the event is handled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00024B37 File Offset: 0x00022D37
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00024B3F File Offset: 0x00022D3F
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

		/// <summary>Gets or sets the character corresponding to the key pressed.</summary>
		/// <returns>The ASCII character that is composed. For example, if the user presses SHIFT + K, this property returns an uppercase K.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00024B48 File Offset: 0x00022D48
		public char KeyChar
		{
			get
			{
				return this.key_char;
			}
		}

		// Token: 0x0400056F RID: 1391
		private char key_char;

		// Token: 0x04000570 RID: 1392
		private bool event_handled;
	}
}
