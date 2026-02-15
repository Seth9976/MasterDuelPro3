using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the image to draw when drawing a menu with the <see cref="M:System.Windows.Forms.ControlPaint.DrawMenuGlyph(System.Drawing.Graphics,System.Drawing.Rectangle,System.Windows.Forms.MenuGlyph)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000136 RID: 310
	public enum MenuGlyph
	{
		/// <summary>Draws a submenu arrow.</summary>
		// Token: 0x040007C6 RID: 1990
		Arrow,
		/// <summary>The minimum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Arrow" /> value).</summary>
		// Token: 0x040007C7 RID: 1991
		Min = 0,
		/// <summary>Draws a menu check mark.</summary>
		// Token: 0x040007C8 RID: 1992
		Checkmark,
		/// <summary>Draws a menu bullet.</summary>
		// Token: 0x040007C9 RID: 1993
		Bullet,
		/// <summary>The maximum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Bullet" /> value).</summary>
		// Token: 0x040007CA RID: 1994
		Max = 2
	}
}
