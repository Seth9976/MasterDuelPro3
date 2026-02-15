using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the bounds of the control to use when defining a control's size and position.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000026 RID: 38
	[Flags]
	public enum BoundsSpecified
	{
		/// <summary>No bounds are specified.</summary>
		// Token: 0x040000E0 RID: 224
		None = 0,
		/// <summary>The left edge of the control is defined.</summary>
		// Token: 0x040000E1 RID: 225
		X = 1,
		/// <summary>The top edge of the control is defined.</summary>
		// Token: 0x040000E2 RID: 226
		Y = 2,
		/// <summary>Both X and Y coordinates of the control are defined.</summary>
		// Token: 0x040000E3 RID: 227
		Location = 3,
		/// <summary>The width of the control is defined.</summary>
		// Token: 0x040000E4 RID: 228
		Width = 4,
		/// <summary>The height of the control is defined.</summary>
		// Token: 0x040000E5 RID: 229
		Height = 8,
		/// <summary>Both <see cref="P:System.Windows.Forms.Control.Width" /> and <see cref="P:System.Windows.Forms.Control.Height" /> property values of the control are defined.</summary>
		// Token: 0x040000E6 RID: 230
		Size = 12,
		/// <summary>Both <see cref="P:System.Windows.Forms.Control.Location" /> and <see cref="P:System.Windows.Forms.Control.Size" /> property values are defined.</summary>
		// Token: 0x040000E7 RID: 231
		All = 15
	}
}
