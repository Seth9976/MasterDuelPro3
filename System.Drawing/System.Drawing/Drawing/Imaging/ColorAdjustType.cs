using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies which GDI+ objects use color adjustment information.</summary>
	// Token: 0x0200007F RID: 127
	public enum ColorAdjustType
	{
		/// <summary>Color adjustment information that is used by all GDI+ objects that do not have their own color adjustment information.</summary>
		// Token: 0x0400022F RID: 559
		Default,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Bitmap" /> objects.</summary>
		// Token: 0x04000230 RID: 560
		Bitmap,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Brush" /> objects.</summary>
		// Token: 0x04000231 RID: 561
		Brush,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Pen" /> objects.</summary>
		// Token: 0x04000232 RID: 562
		Pen,
		/// <summary>Color adjustment information for text.</summary>
		// Token: 0x04000233 RID: 563
		Text,
		/// <summary>The number of types specified.</summary>
		// Token: 0x04000234 RID: 564
		Count,
		/// <summary>The number of types specified.</summary>
		// Token: 0x04000235 RID: 565
		Any
	}
}
