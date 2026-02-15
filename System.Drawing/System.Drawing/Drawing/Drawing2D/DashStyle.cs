using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the style of dashed lines drawn with a <see cref="T:System.Drawing.Pen" /> object.</summary>
	// Token: 0x02000095 RID: 149
	public enum DashStyle
	{
		/// <summary>Specifies a solid line.</summary>
		// Token: 0x040002D4 RID: 724
		Solid,
		/// <summary>Specifies a line consisting of dashes.</summary>
		// Token: 0x040002D5 RID: 725
		Dash,
		/// <summary>Specifies a line consisting of dots.</summary>
		// Token: 0x040002D6 RID: 726
		Dot,
		/// <summary>Specifies a line consisting of a repeating pattern of dash-dot.</summary>
		// Token: 0x040002D7 RID: 727
		DashDot,
		/// <summary>Specifies a line consisting of a repeating pattern of dash-dot-dot.</summary>
		// Token: 0x040002D8 RID: 728
		DashDotDot,
		/// <summary>Specifies a user-defined custom dash style.</summary>
		// Token: 0x040002D9 RID: 729
		Custom
	}
}
