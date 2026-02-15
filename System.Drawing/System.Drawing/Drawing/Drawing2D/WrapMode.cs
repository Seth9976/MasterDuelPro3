using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how a texture or gradient is tiled when it is smaller than the area being filled.</summary>
	// Token: 0x0200009D RID: 157
	public enum WrapMode
	{
		/// <summary>Tiles the gradient or texture.</summary>
		// Token: 0x04000323 RID: 803
		Tile,
		/// <summary>Reverses the texture or gradient horizontally and then tiles the texture or gradient.</summary>
		// Token: 0x04000324 RID: 804
		TileFlipX,
		/// <summary>Reverses the texture or gradient vertically and then tiles the texture or gradient.</summary>
		// Token: 0x04000325 RID: 805
		TileFlipY,
		/// <summary>Reverses the texture or gradient horizontally and vertically and then tiles the texture or gradient.</summary>
		// Token: 0x04000326 RID: 806
		TileFlipXY,
		/// <summary>The texture or gradient is not tiled.</summary>
		// Token: 0x04000327 RID: 807
		Clamp
	}
}
