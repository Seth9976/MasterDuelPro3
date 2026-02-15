using System;

namespace System.Net
{
	/// <summary>Represents the file compression and decompression encoding format to be used to compress the data received in response to an <see cref="T:System.Net.HttpWebRequest" />.</summary>
	// Token: 0x02000401 RID: 1025
	[Flags]
	public enum DecompressionMethods
	{
		/// <summary>Do not use compression.</summary>
		// Token: 0x0400102E RID: 4142
		None = 0,
		/// <summary>Use the gZip compression-decompression algorithm.</summary>
		// Token: 0x0400102F RID: 4143
		GZip = 1,
		/// <summary>Use the deflate compression-decompression algorithm.</summary>
		// Token: 0x04001030 RID: 4144
		Deflate = 2
	}
}
