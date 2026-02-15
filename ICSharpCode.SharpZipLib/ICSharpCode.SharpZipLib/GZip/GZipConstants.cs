using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x0200008F RID: 143
	public sealed class GZipConstants
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00017E84 File Offset: 0x00016084
		public static Encoding Encoding
		{
			get
			{
				Encoding encoding;
				try
				{
					encoding = Encoding.GetEncoding(1252);
				}
				catch
				{
					encoding = Encoding.ASCII;
				}
				return encoding;
			}
		}

		// Token: 0x040003B9 RID: 953
		public const byte ID1 = 31;

		// Token: 0x040003BA RID: 954
		public const byte ID2 = 139;

		// Token: 0x040003BB RID: 955
		public const byte CompressionMethodDeflate = 8;
	}
}
