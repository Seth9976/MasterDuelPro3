using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position of the image on the control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D2 RID: 210
	public enum ImageLayout
	{
		/// <summary>The image is left-aligned at the top across the control's client rectangle.</summary>
		// Token: 0x040004FE RID: 1278
		None,
		/// <summary>The image is tiled across the control's client rectangle.</summary>
		// Token: 0x040004FF RID: 1279
		Tile,
		/// <summary>The image is centered within the control's client rectangle.</summary>
		// Token: 0x04000500 RID: 1280
		Center,
		/// <summary>The image is streched across the control's client rectangle.</summary>
		// Token: 0x04000501 RID: 1281
		Stretch,
		/// <summary>The image is enlarged within the control's client rectangle.</summary>
		// Token: 0x04000502 RID: 1282
		Zoom
	}
}
