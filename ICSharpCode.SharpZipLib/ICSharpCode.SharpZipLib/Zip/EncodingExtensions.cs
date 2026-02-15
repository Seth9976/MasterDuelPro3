using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000051 RID: 81
	internal static class EncodingExtensions
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000C282 File Offset: 0x0000A482
		public static bool IsZipUnicode(this Encoding e)
		{
			return e.Equals(StringCodec.UnicodeZipEncoding);
		}
	}
}
