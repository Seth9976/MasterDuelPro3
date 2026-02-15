using System;

namespace System.Drawing
{
	/// <summary>Specifies how much an image is rotated and the axis used to flip the image.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000016 RID: 22
	public enum RotateFlipType
	{
		/// <summary>Specifies no clockwise rotation and no flipping.</summary>
		// Token: 0x040000C7 RID: 199
		RotateNoneFlipNone,
		/// <summary>Specifies a 90-degree clockwise rotation without flipping.</summary>
		// Token: 0x040000C8 RID: 200
		Rotate90FlipNone,
		/// <summary>Specifies a 180-degree clockwise rotation without flipping.</summary>
		// Token: 0x040000C9 RID: 201
		Rotate180FlipNone,
		/// <summary>Specifies a 270-degree clockwise rotation without flipping.</summary>
		// Token: 0x040000CA RID: 202
		Rotate270FlipNone,
		/// <summary>Specifies no clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x040000CB RID: 203
		RotateNoneFlipX,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x040000CC RID: 204
		Rotate90FlipX,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x040000CD RID: 205
		Rotate180FlipX,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x040000CE RID: 206
		Rotate270FlipX,
		/// <summary>Specifies no clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x040000CF RID: 207
		RotateNoneFlipY = 6,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x040000D0 RID: 208
		Rotate90FlipY,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x040000D1 RID: 209
		Rotate180FlipY = 4,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x040000D2 RID: 210
		Rotate270FlipY,
		/// <summary>Specifies no clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x040000D3 RID: 211
		RotateNoneFlipXY = 2,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x040000D4 RID: 212
		Rotate90FlipXY,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x040000D5 RID: 213
		Rotate180FlipXY = 0,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x040000D6 RID: 214
		Rotate270FlipXY
	}
}
