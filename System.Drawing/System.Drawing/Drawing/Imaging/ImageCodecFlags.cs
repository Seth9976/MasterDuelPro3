using System;

namespace System.Drawing.Imaging
{
	/// <summary>Provides attributes of an image encoder/decoder (codec).</summary>
	// Token: 0x02000088 RID: 136
	[Flags]
	public enum ImageCodecFlags
	{
		/// <summary>The codec supports encoding (saving).</summary>
		// Token: 0x04000269 RID: 617
		Encoder = 1,
		/// <summary>The codec supports decoding (reading).</summary>
		// Token: 0x0400026A RID: 618
		Decoder = 2,
		/// <summary>The codec supports raster images (bitmaps).</summary>
		// Token: 0x0400026B RID: 619
		SupportBitmap = 4,
		/// <summary>The codec supports vector images (metafiles).</summary>
		// Token: 0x0400026C RID: 620
		SupportVector = 8,
		/// <summary>The encoder requires a seekable output stream.</summary>
		// Token: 0x0400026D RID: 621
		SeekableEncode = 16,
		/// <summary>The decoder has blocking behavior during the decoding process.</summary>
		// Token: 0x0400026E RID: 622
		BlockingDecode = 32,
		/// <summary>The codec is built into GDI+.</summary>
		// Token: 0x0400026F RID: 623
		Builtin = 65536,
		/// <summary>Not used.</summary>
		// Token: 0x04000270 RID: 624
		System = 131072,
		/// <summary>Not used.</summary>
		// Token: 0x04000271 RID: 625
		User = 262144
	}
}
