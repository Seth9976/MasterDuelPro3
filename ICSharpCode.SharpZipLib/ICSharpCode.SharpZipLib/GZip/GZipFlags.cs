using System;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000090 RID: 144
	[Flags]
	public enum GZipFlags : byte
	{
		// Token: 0x040003BD RID: 957
		FTEXT = 1,
		// Token: 0x040003BE RID: 958
		FHCRC = 2,
		// Token: 0x040003BF RID: 959
		FEXTRA = 4,
		// Token: 0x040003C0 RID: 960
		FNAME = 8,
		// Token: 0x040003C1 RID: 961
		FCOMMENT = 16
	}
}
