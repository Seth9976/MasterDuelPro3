using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Drawing
{
	/// <summary>Specifies alignment of content on the drawing surface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003D RID: 61
	[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum ContentAlignment
	{
		/// <summary>Content is vertically aligned at the top, and horizontally aligned on the left.</summary>
		// Token: 0x0400012B RID: 299
		TopLeft = 1,
		/// <summary>Content is vertically aligned at the top, and horizontally aligned at the center.</summary>
		// Token: 0x0400012C RID: 300
		TopCenter,
		/// <summary>Content is vertically aligned at the top, and horizontally aligned on the right.</summary>
		// Token: 0x0400012D RID: 301
		TopRight = 4,
		/// <summary>Content is vertically aligned in the middle, and horizontally aligned on the left.</summary>
		// Token: 0x0400012E RID: 302
		MiddleLeft = 16,
		/// <summary>Content is vertically aligned in the middle, and horizontally aligned at the center.</summary>
		// Token: 0x0400012F RID: 303
		MiddleCenter = 32,
		/// <summary>Content is vertically aligned in the middle, and horizontally aligned on the right.</summary>
		// Token: 0x04000130 RID: 304
		MiddleRight = 64,
		/// <summary>Content is vertically aligned at the bottom, and horizontally aligned on the left.</summary>
		// Token: 0x04000131 RID: 305
		BottomLeft = 256,
		/// <summary>Content is vertically aligned at the bottom, and horizontally aligned at the center.</summary>
		// Token: 0x04000132 RID: 306
		BottomCenter = 512,
		/// <summary>Content is vertically aligned at the bottom, and horizontally aligned on the right.</summary>
		// Token: 0x04000133 RID: 307
		BottomRight = 1024
	}
}
