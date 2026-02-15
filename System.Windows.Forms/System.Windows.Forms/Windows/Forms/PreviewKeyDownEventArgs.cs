using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
	// Token: 0x02000166 RID: 358
	public class PreviewKeyDownEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> class with the specified key. </summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		// Token: 0x06000DCB RID: 3531 RVA: 0x0003BC10 File Offset: 0x00039E10
		public PreviewKeyDownEventArgs(Keys keyData)
		{
			this.key_data = keyData;
		}

		/// <summary>Gets or sets a value indicating whether a key is a regular input key.</summary>
		/// <returns>true if the key is a regular input key; otherwise, false.</returns>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003BC1F File Offset: 0x00039E1F
		public bool IsInputKey
		{
			get
			{
				return this.is_input_key;
			}
		}

		// Token: 0x0400089B RID: 2203
		private Keys key_data;

		// Token: 0x0400089C RID: 2204
		private bool is_input_key;
	}
}
