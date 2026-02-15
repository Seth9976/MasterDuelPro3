using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction to move when getting items with the <see cref="M:System.Windows.Forms.ToolStrip.GetNextItem(System.Windows.Forms.ToolStripItem,System.Windows.Forms.ArrowDirection)" /> method.</summary>
	// Token: 0x0200000F RID: 15
	public enum ArrowDirection
	{
		/// <summary>The direction is left (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
		// Token: 0x0400006B RID: 107
		Left,
		/// <summary>The direction is up (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
		// Token: 0x0400006C RID: 108
		Up,
		/// <summary>The direction is right (<see cref="F:System.Windows.Forms.Orientation.Horizontal" />).</summary>
		// Token: 0x0400006D RID: 109
		Right = 16,
		/// <summary>The direction is down (<see cref="F:System.Windows.Forms.Orientation.Vertical" />).</summary>
		// Token: 0x0400006E RID: 110
		Down
	}
}
