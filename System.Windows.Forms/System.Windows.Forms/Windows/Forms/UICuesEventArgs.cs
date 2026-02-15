using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020A RID: 522
	public class UICuesEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UICuesEventArgs" /> class with the specified <see cref="T:System.Windows.Forms.UICues" />.</summary>
		/// <param name="uicues">A bitwise combination of the <see cref="T:System.Windows.Forms.UICues" /> values. </param>
		// Token: 0x0600164A RID: 5706 RVA: 0x0006FB6B File Offset: 0x0006DD6B
		public UICuesEventArgs(UICues uicues)
		{
			this.cues = uicues;
		}

		// Token: 0x04000D66 RID: 3430
		private UICues cues;
	}
}
