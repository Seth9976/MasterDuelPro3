using System;

namespace System.Drawing
{
	/// <summary>Specifies the display and layout information for text strings.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000019 RID: 25
	[Flags]
	public enum StringFormatFlags
	{
		/// <summary>Text is displayed from right to left.</summary>
		// Token: 0x040000DE RID: 222
		DirectionRightToLeft = 1,
		/// <summary>Text is vertically aligned.</summary>
		// Token: 0x040000DF RID: 223
		DirectionVertical = 2,
		/// <summary>Parts of characters are allowed to overhang the string's layout rectangle. By default, characters are repositioned to avoid any overhang.</summary>
		// Token: 0x040000E0 RID: 224
		FitBlackBox = 4,
		/// <summary>Control characters such as the left-to-right mark are shown in the output with a representative glyph.</summary>
		// Token: 0x040000E1 RID: 225
		DisplayFormatControl = 32,
		/// <summary>Fallback to alternate fonts for characters not supported in the requested font is disabled. Any missing characters are displayed with the fonts missing glyph, usually an open square.</summary>
		// Token: 0x040000E2 RID: 226
		NoFontFallback = 1024,
		/// <summary>Includes the trailing space at the end of each line. By default the boundary rectangle returned by the <see cref="Overload:System.Drawing.Graphics.MeasureString" /> method excludes the space at the end of each line. Set this flag to include that space in measurement.</summary>
		// Token: 0x040000E3 RID: 227
		MeasureTrailingSpaces = 2048,
		/// <summary>Text wrapping between lines when formatting within a rectangle is disabled. This flag is implied when a point is passed instead of a rectangle, or when the specified rectangle has a zero line length.</summary>
		// Token: 0x040000E4 RID: 228
		NoWrap = 4096,
		/// <summary>Only entire lines are laid out in the formatting rectangle. By default layout continues until the end of the text, or until no more lines are visible as a result of clipping, whichever comes first. Note that the default settings allow the last line to be partially obscured by a formatting rectangle that is not a whole multiple of the line height. To ensure that only whole lines are seen, specify this value and be careful to provide a formatting rectangle at least as tall as the height of one line.</summary>
		// Token: 0x040000E5 RID: 229
		LineLimit = 8192,
		/// <summary>Overhanging parts of glyphs, and unwrapped text reaching outside the formatting rectangle are allowed to show. By default all text and glyph parts reaching outside the formatting rectangle are clipped.</summary>
		// Token: 0x040000E6 RID: 230
		NoClip = 16384
	}
}
