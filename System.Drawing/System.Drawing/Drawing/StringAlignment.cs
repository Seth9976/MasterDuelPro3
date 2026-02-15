using System;

namespace System.Drawing
{
	/// <summary>Specifies the alignment of a text string relative to its layout rectangle.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000018 RID: 24
	public enum StringAlignment
	{
		/// <summary>Specifies the text be aligned near the layout. In a left-to-right layout, the near position is left. In a right-to-left layout, the near position is right.</summary>
		// Token: 0x040000DA RID: 218
		Near,
		/// <summary>Specifies that text is aligned in the center of the layout rectangle.</summary>
		// Token: 0x040000DB RID: 219
		Center,
		/// <summary>Specifies that text is aligned far from the origin position of the layout rectangle. In a left-to-right layout, the far position is right. In a right-to-left layout, the far position is left.</summary>
		// Token: 0x040000DC RID: 220
		Far
	}
}
