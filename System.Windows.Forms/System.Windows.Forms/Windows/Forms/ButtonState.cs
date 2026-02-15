using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the appearance of a button.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002B RID: 43
	[Flags]
	public enum ButtonState
	{
		/// <summary>The button has its normal appearance (three-dimensional).</summary>
		// Token: 0x04000102 RID: 258
		Normal = 0,
		/// <summary>The button is inactive (grayed).</summary>
		// Token: 0x04000103 RID: 259
		Inactive = 256,
		/// <summary>The button appears pressed.</summary>
		// Token: 0x04000104 RID: 260
		Pushed = 512,
		/// <summary>The button has a checked or latched appearance. Use this appearance to show that a toggle button has been pressed.</summary>
		// Token: 0x04000105 RID: 261
		Checked = 1024,
		/// <summary>The button has a flat, two-dimensional appearance.</summary>
		// Token: 0x04000106 RID: 262
		Flat = 16384,
		/// <summary>All flags except Normal are set.</summary>
		// Token: 0x04000107 RID: 263
		All = 18176
	}
}
