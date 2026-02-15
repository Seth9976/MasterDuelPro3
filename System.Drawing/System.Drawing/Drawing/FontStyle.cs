using System;

namespace System.Drawing
{
	/// <summary>Specifies style information applied to text.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000011 RID: 17
	[Flags]
	public enum FontStyle
	{
		/// <summary>Normal text.</summary>
		// Token: 0x040000BF RID: 191
		Regular = 0,
		/// <summary>Bold text.</summary>
		// Token: 0x040000C0 RID: 192
		Bold = 1,
		/// <summary>Italic text.</summary>
		// Token: 0x040000C1 RID: 193
		Italic = 2,
		/// <summary>Underlined text.</summary>
		// Token: 0x040000C2 RID: 194
		Underline = 4,
		/// <summary>Text with a line through the middle.</summary>
		// Token: 0x040000C3 RID: 195
		Strikeout = 8
	}
}
