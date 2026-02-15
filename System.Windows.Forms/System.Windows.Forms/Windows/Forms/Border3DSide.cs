using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the sides of a rectangle to apply a three-dimensional border to.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000023 RID: 35
	[ComVisible(true)]
	[Flags]
	public enum Border3DSide
	{
		/// <summary>A three-dimensional border on the left edge of the rectangle.</summary>
		// Token: 0x040000CA RID: 202
		Left = 1,
		/// <summary>A three-dimensional border on the top edge of the rectangle.</summary>
		// Token: 0x040000CB RID: 203
		Top = 2,
		/// <summary>A three-dimensional border on the right side of the rectangle.</summary>
		// Token: 0x040000CC RID: 204
		Right = 4,
		/// <summary>A three-dimensional border on the bottom side of the rectangle.</summary>
		// Token: 0x040000CD RID: 205
		Bottom = 8,
		/// <summary>The interior of the rectangle is filled with the color defined for three-dimensional controls instead of the background color for the form.</summary>
		// Token: 0x040000CE RID: 206
		Middle = 2048,
		/// <summary>A three-dimensional border on all four sides of the rectangle. The middle of the rectangle is filled with the color defined for three-dimensional controls.</summary>
		// Token: 0x040000CF RID: 207
		All = 2063
	}
}
