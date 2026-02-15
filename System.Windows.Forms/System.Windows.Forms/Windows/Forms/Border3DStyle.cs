using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the style of a three-dimensional border.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000024 RID: 36
	[ComVisible(true)]
	public enum Border3DStyle
	{
		/// <summary>The border has a raised outer edge and no inner edge.</summary>
		// Token: 0x040000D1 RID: 209
		RaisedOuter = 1,
		/// <summary>The border has a sunken outer edge and no inner edge.</summary>
		// Token: 0x040000D2 RID: 210
		SunkenOuter,
		/// <summary>The border has a raised inner edge and no outer edge.</summary>
		// Token: 0x040000D3 RID: 211
		RaisedInner = 4,
		/// <summary>The border has raised inner and outer edges.</summary>
		// Token: 0x040000D4 RID: 212
		Raised,
		/// <summary>The inner and outer edges of the border have an etched appearance.</summary>
		// Token: 0x040000D5 RID: 213
		Etched,
		/// <summary>The border has a sunken inner edge and no outer edge.</summary>
		// Token: 0x040000D6 RID: 214
		SunkenInner = 8,
		/// <summary>The inner and outer edges of the border have a raised appearance.</summary>
		// Token: 0x040000D7 RID: 215
		Bump,
		/// <summary>The border has sunken inner and outer edges.</summary>
		// Token: 0x040000D8 RID: 216
		Sunken,
		/// <summary>The border is drawn outside the specified rectangle, preserving the dimensions of the rectangle for drawing.</summary>
		// Token: 0x040000D9 RID: 217
		Adjust = 8192,
		/// <summary>The border has no three-dimensional effects.</summary>
		// Token: 0x040000DA RID: 218
		Flat = 16394
	}
}
